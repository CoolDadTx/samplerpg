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
                case 1: return new Monster("Snake", 4, 5, imageName: "Snake.png", gold: 1)
                                    .SetDamage(1, 2)
                                    .AddLoot(9001, 25)
                                    .AddLoot(9002, 75);
                case 2: return new Monster("Rat", 5, 5, imageName: "Rat.png", gold: 1) 
                                    .SetDamage(1, 2)
                                    .AddLoot(9003, 25)
                                    .AddLoot(9004, 75);

                case 3: return new Monster("Giant Spider", 10, 10, imageName: "GiantSpider.png", gold: 3)
                                    .SetDamage(1, 4)
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
