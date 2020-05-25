using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

using P3Net.Kraken.IO;

using SampleRpg.Engine.Factories;
using SampleRpg.Engine.Models;

namespace SampleRpg.Engine.IO
{
    public class WorldJsonFileReader
    {
        public WorldJsonFileReader ( string filename )
        {
            _filename = Path.GetFullPath(filename);               
        }

        public IEnumerable<Location> Read ()
        {            
            var basePath = Path.GetDirectoryName(_filename);

            var reader = new JsonFileReader(_filename);
            var dataItems = reader.ReadArray<LocationModel>();
            foreach (var dataItem in dataItems)
            {
                var location = dataItem.ToLocation();
                if (location != null)
                {
                    //Images are relative to JSON file so fix the path
                    if (!Path.IsPathRooted(location.ImagePath))
                        location.ImagePath = Path.Combine(basePath, location.ImagePath);

                    yield return location;
                };
            };
        }

        #region Private Members
                
        private sealed class EncounterModel
        {
            public int Id { get; set; }
            public int Chance { get; set; }
        }

        private sealed class QuestModel
        {
            public int Id { get; set; }
        }

        private sealed class LocationModel
        {
            public string Name { get; set; }

            //TODO: Switch to a coordinate system so we can add things at a particular coordinate rather than tying to a specific location object
            public int X { get; set; }
            public int Y { get; set; }
            public string Image { get; set; }

            public string Description { get; set; }

            public IEnumerable<EncounterModel> Monsters { get; set; } = Enumerable.Empty<EncounterModel>();
            public IEnumerable<QuestModel> Quests { get; set; } = Enumerable.Empty<QuestModel>();
            public string Trader { get; set; }

            public Location ToLocation ()
            {
                var location = new Location(X, Y, Name, Description, FilePaths.NormalizePath(Image));
                if (Monsters?.Any() ?? false)
                {
                    foreach (var encounter in Monsters)
                    {
                        var availableMonster = MonsterFactory.Get(encounter.Id);
                        if (availableMonster != null)
                            location.AddEncounter(encounter.Id, encounter.Chance);
                        else
                            Trace.TraceWarning($"Location({Name}) has monster {encounter.Id} that could not be found");
                    };
                };

                if (Quests?.Any() ?? false)
                {
                    foreach (var quest in Quests)
                    {
                        var availableQuest = QuestFactory.FindQuest(quest.Id);
                        if (availableQuest != null)
                            location.AddQuest(availableQuest);
                        else
                            Trace.TraceWarning($"Location({Name}) has quest {quest.Id} that could not be found");
                    };
                };

                if (!String.IsNullOrEmpty(Trader))
                {
                    var availableTader = TraderFactory.GetTrader(Trader);
                    if (availableTader != null)
                        location.TraderHere = availableTader;
                    else
                        Trace.TraceWarning($"Location({Name}) has tracer '{Trader}' that could not be found");
                };

                return location;
            }
        }
        

        private readonly string _filename;

        #endregion
    }
}
