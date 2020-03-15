using System;
using System.Collections.Generic;
using System.Text;

namespace SampleRpg.Engine.Models
{
    public class Quest
    {
        //TODO: Should use a string so we can guarantee uniqueness
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        //TODO: Make this a characteristic of the quest logic and not actual data
        public List<ItemQuantity> ItemsToComplete { get; } = new List<ItemQuantity>();

        //TODO: Make this a characteristic of the quest completion logic
        public int RewardXp { get; set; }
        public int RewardGold { get; set; }
        public List<ItemQuantity> RewardItems { get; } = new List<ItemQuantity>();
    }
}
