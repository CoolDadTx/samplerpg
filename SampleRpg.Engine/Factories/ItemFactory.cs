using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using SampleRpg.Engine.Models;

namespace SampleRpg.Engine.Factories
{
    public static class ItemFactory
    {
        static ItemFactory()
        {
            s_items = new List<GameItem>() {
                new Weapon() { ItemTypeId = 1001, Name = "Pointy Stick", Price = 1, MinimumDamage = 1, MaximumDamage = 2 },
                new Weapon() { ItemTypeId = 1002, Name = "Rusty Sword", Price = 5, MinimumDamage = 1, MaximumDamage = 3 },

                new GameItem() { ItemTypeId = 9001, Name = "Snake Fang", Price = 5 },
                new GameItem() { ItemTypeId = 9002, Name = "Snakeskin", Price = 2 },
                new GameItem() { ItemTypeId = 9003, Name = "Rat tail", Price = 1 },
                new GameItem() { ItemTypeId = 9004, Name = "Rat fur", Price = 2 },
                new GameItem() { ItemTypeId = 9005, Name = "Spider fang", Price = 1 },
                new GameItem() { ItemTypeId = 9006, Name = "Spider silk", Price = 2 },
            };                
        }

        public static GameItem CreateGameItem ( int id )
        {
            var item = s_items.FirstOrDefault(i => i.ItemTypeId == id);
            
            return item?.Clone(true);
        }

        private static List<GameItem> s_items;
    }
}
