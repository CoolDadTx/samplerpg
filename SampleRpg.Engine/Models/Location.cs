using System;
using System.Collections.Generic;
using System.Linq;

using P3Net.Kraken;

using SampleRpg.Engine.Factories;

namespace SampleRpg.Engine.Models
{    
    public class Location
    {
        #region Construction

        //TODO: Make a factory to create one of these or use a definition object - too many parameters
        public Location ( int x, int y, string name, string description = null, string imageName = null )
        {
            XCoordinate = x;
            YCoordinate = y;
            Name = name;
            Description = description ?? name;
            ImageName = (imageName ?? name).RemoveAll(' ');
        }
        #endregion

        public int XCoordinate { get; }
        public int YCoordinate { get; }

        public string Name { get; }
        public string Description { get; }

        public string ImageName { get; }

        public string ImagePath => $"pack://application:,,,/Resources/Images/Locations/{ImageName}";

        public Trader TraderHere { get; set; }

        public Location AddEncounter ( int monsterId, int percentage )
        {
            var existing = Encounters.FirstOrDefault(x => x.MonsterId == monsterId);
            if (existing != null)
                existing.Percentage = percentage;
            else
                Encounters.Add(new Encounter() { MonsterId = monsterId, Percentage = percentage });

            return this;
        }

        //TODO: Should use just quest Id
        public Location AddQuest ( Quest quest )
        {
            var existing = AvailableQuests.FirstOrDefault(x => x.Id == quest.Id);
            if (existing == null)
                AvailableQuests.Add(quest);

            return this;
        }        

        public Monster GetEncounter ()
        {
            if (!Encounters.Any())
                return null;

            var totalChance = Encounters.Sum(x => x.Percentage);
            var result = Rng.Between(1, totalChance);

            var total = 0;
            foreach (var encounter in Encounters)
            {
                total += encounter.Percentage;
                if (result <= total)
                    return MonsterFactory.Get(encounter.MonsterId);
            };

            return null;
        }        

        public IEnumerable<Quest> GetQuests () => AvailableQuests;

        //TODO: Query for traders at location instead of making it part of location
        private List<Encounter> Encounters { get; } = new List<Encounter>();

        //TODO: Query for quests at location instead of making it part of location
        private List<Quest> AvailableQuests { get; } = new List<Quest>();
    }
}
