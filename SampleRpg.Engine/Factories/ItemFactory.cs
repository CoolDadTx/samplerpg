using System;
using System.Collections.Generic;
using System.Linq;
using SampleRpg.Engine.Actions;
using SampleRpg.Engine.Models;

namespace SampleRpg.Engine.Factories
{
    public static class ItemFactory
    {
        static ItemFactory()
        {
            CreateWeapon(1001, "Pointy Stick", 1, 1, 2);
            CreateWeapon(1002, "Rusty Sword", 5, 1, 3);

            CreateMiscellaneous(9001, "Snake Fang", 5);
            CreateMiscellaneous(9002, "Snakeskin", 2);
            CreateMiscellaneous(9003, "Rat tail", 1);
            CreateMiscellaneous(9004, "Rat fur", 2);
            CreateMiscellaneous(9005, "Spider fang", 1);
            CreateMiscellaneous(9006, "Spider silk", 2);
        }

        public static GameItem NewItem ( int id )
        {
            var item = s_items.FirstOrDefault(i => i.Id == id);
            
            return item?.Clone(true);
        }

        //TODO: Should probably be using a builder here...
        public static GameItem CreateMiscellaneous ( int id, string name, int price )
        {
            var item = new GameItem(GameItemCategory.Miscellaneous, id, name, price);

            s_items.Add(item);
            return item;
        }

        //TODO: Don't agree weapons are unique
        public static GameItem CreateWeapon ( int id, string name, int price, int minDamage, int maxDamage )
        {
            var weapon = new GameItem(GameItemCategory.Weapon, id, name, price, true);
            weapon.AttackCommand = new WeaponAttackCommand(weapon, minDamage, maxDamage);

            s_items.Add(weapon);
            return weapon;
        }

        private static readonly List<GameItem> s_items = new List<GameItem>();
    }
}
