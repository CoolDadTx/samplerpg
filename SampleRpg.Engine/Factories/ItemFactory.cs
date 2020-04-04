using System;
using System.Collections.Generic;
using System.Linq;

using SampleRpg.Engine.Models;

namespace SampleRpg.Engine.Factories
{
    public static class ItemFactory
    {
        static ItemFactory()
        {
            s_items = new List<GameItem>() {
                new Weapon(1001, "Pointy Stick", 1, 1, 2),
                new Weapon(1002, "Rusty Sword", 5, 1, 3),

                new GameItem(9001, "Snake Fang", 5),
                new GameItem(9002, "Snakeskin", 2),
                new GameItem(9003, "Rat tail", 1),
                new GameItem(9004, "Rat fur", 2),
                new GameItem(9005, "Spider fang", 1),
                new GameItem(9006, "Spider silk", 2),
            };                
        }

        public static GameItem CreateGameItem ( int id )
        {
            var item = s_items.FirstOrDefault(i => i.ItemTypeId == id);
            
            return item?.Clone(true);
        }

        private static readonly List<GameItem> s_items;
    }
}
