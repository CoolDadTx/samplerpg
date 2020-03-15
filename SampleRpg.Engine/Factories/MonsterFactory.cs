using System;
using System.Collections.Generic;
using System.Text;
using SampleRpg.Engine.Models;

namespace SampleRpg.Engine.Factories
{
    public static class MonsterFactory
    {
        public static Monster Get ( int id )
        {
            switch (id)
            {
                case 1: return new Monster() { Name = "Snake", ImageName = "Snake.png", MaximumHitPoints = 4, HitPoints = 4, RewardXP = 5, RewardGold = 1 }
                                    .AddLoot(9001, 25)
                                    .AddLoot(9002, 75);
                case 2: return new Monster() { Name = "Snake", ImageName = "Snake.png", MaximumHitPoints = 4, HitPoints = 4, RewardXP = 5, RewardGold = 1 }
                                    .AddLoot(9003, 25)
                                    .AddLoot(9004, 75);

                case 3: return new Monster() { Name = "Snake", ImageName = "Snake.png", MaximumHitPoints = 4, HitPoints = 4, RewardXP = 5, RewardGold = 1 }
                                    .AddLoot(9005, 25)
                                    .AddLoot(9006, 75);

                default: throw new ArgumentOutOfRangeException(nameof(id), "Unknown monster");
            }
        }

        //TODO: Candidate for extension method
        private static Monster AddLoot ( this Monster monster, int itemId, int percentage )
        {
            if (Rng.Between(1, 100) <= percentage)
                monster.Inventory.Add(new ItemQuantity() { ItemId = itemId, Quantity = 1 });

            return monster;
        }    
        
    }
}
