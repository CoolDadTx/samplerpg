using System;
using System.Collections.Generic;

using P3Net.Kraken.Diagnostics;

namespace SampleRpg.Engine.Models
{
    public class Quest
    {
        #region Construction

        //TODO: Use a Definition class or build so we don't have to pass all these parameters
        public Quest ( int id, string name, string description, int rewardXp = 0, int rewardGold = 0 )
        {
            Verify.Argument(nameof(id)).WithValue(id).IsGreaterThanZero();
            Verify.Argument(nameof(name)).WithValue(name).IsNotNullOrEmpty();
            Verify.Argument(nameof(rewardXp)).WithValue(rewardXp).IsGreaterThanOrEqualToZero();
            Verify.Argument(nameof(rewardGold)).WithValue(rewardGold).IsGreaterThanOrEqualToZero();

            Id = id;
            Name = name;
            Description = description;

            RewardXp = rewardXp;
            RewardGold = rewardGold;
        }
        #endregion

        //TODO: Should use a string so we can guarantee uniqueness
        public int Id { get; }

        public string Name { get; }

        public string Description { get; }

        //TODO: Make this a characteristic of the quest logic and not actual data
        public List<ItemQuantity> ItemsToComplete { get; } = new List<ItemQuantity>();

        //TODO: Make this a characteristic of the quest completion logic
        public int RewardXp { get; }
        public int RewardGold { get; }
        public List<ItemQuantity> RewardItems { get; } = new List<ItemQuantity>();
    }
}
