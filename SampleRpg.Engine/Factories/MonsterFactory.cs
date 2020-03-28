using System;

using SampleRpg.Engine.Models;

namespace SampleRpg.Engine.Factories
{
    public static class MonsterFactory
    {
        //TODO: Create helper method for creating monsters
        public static Monster Get ( int id )
        {            
            switch (id)
            {
                case 1: return new Monster() { Name = "Snake", ImageName = "Snake.png", MaximumHitPoints = 4, CurrentHitPoints = 4, RewardXP = 5, Gold = 1
                                               , MinimumDamage = 1, MaximumDamage = 2 } 
                                    .AddLoot(9001, 25)
                                    .AddLoot(9002, 75);
                case 2: return new Monster() { Name = "Rat", ImageName = "Rat.png", MaximumHitPoints = 5, CurrentHitPoints = 5, RewardXP = 5, Gold = 1
                                                 , MinimumDamage = 1, MaximumDamage = 2 }
                                    .AddLoot(9003, 25)
                                    .AddLoot(9004, 75);

                case 3: return new Monster() { Name = "Giant Spider", ImageName = "GiantSpider.png", MaximumHitPoints = 10, CurrentHitPoints = 10, RewardXP = 10, Gold = 3
                                                , MinimumDamage = 1, MaximumDamage = 4 }
                                    .AddLoot(9005, 25)
                                    .AddLoot(9006, 75);

                default: throw new ArgumentOutOfRangeException(nameof(id), "Unknown monster");
            }
        }

        //TODO: Candidate for extension method
        private static Monster AddLoot ( this Monster monster, int itemId, int percentage )
        {
            if (Rng.Between(1, 100) <= percentage)
                monster.AddToInventory(ItemFactory.CreateGameItem(itemId));

            return monster;
        }    
        
    }
}
