using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SampleRpg.Engine.Factories;

namespace SampleRpg.Engine.Models
{    
    public class Location
    {
        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public string ImageName { get; set; }

        public string ImagePath => $"pack://application:,,,/Resources/Images/Locations/{ImageName}";

        //TODO: Could add dups...
        public List<Quest> AvailableQuests { get; } = new List<Quest>();

        //TODO: Could add dups...
        public List<Encounter> Encounters { get; } = new List<Encounter>();
                
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
    }
}
