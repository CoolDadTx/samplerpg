﻿using System;
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

            CreateWeapon(1501, "Snake fangs", 0, 0, 2);
            CreateWeapon(1502, "Rat claws", 0, 0, 2);
            CreateWeapon(1503, "Spider fangs", 0, 0, 4);

            CreateMiscellaneous(9001, "Snake Fang", 1);
            CreateMiscellaneous(9002, "Snakeskin", 2);
            CreateMiscellaneous(9003, "Rat tail", 1);
            CreateMiscellaneous(9004, "Rat fur", 2);
            CreateMiscellaneous(9005, "Spider fang", 1);
            CreateMiscellaneous(9006, "Spider silk", 2);

            CreateHealing(2001, "Granola bar", 5, 1, 2);
            CreateMiscellaneous(3001, "Oats", 1);
            CreateMiscellaneous(3002, "Honey", 2);
            CreateMiscellaneous(3003, "Raisins", 2);
        }

        public static string GetItemName ( int id ) => s_items.FirstOrDefault(i => i.Id == id)?.Name ?? "";
        
        public static GameItem NewItem ( int id )
        {
            var item = s_items.FirstOrDefault(i => i.Id == id);
            
            return item?.Clone(true);
        }

        //TODO: Should probably be using a builder here...
        public static GameItem CreateHealing ( int id, string name, int price, int minHeal, int maxHeal )
        {
            var item = new GameItem(GameItemCategory.Consumable, id, name, price);
            item.Action = new HealCommand(item, minHeal, maxHeal);

            s_items.Add(item);
            return item;
        }

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
            weapon.Action = new WeaponAttackCommand(weapon, minDamage, maxDamage);

            s_items.Add(weapon);
            return weapon;
        }

        private static readonly List<GameItem> s_items = new List<GameItem>();
    }
}
