using System;
using System.Collections.Generic;
using System.Linq;

using SampleRpg.Engine.Models;

namespace SampleRpg.Engine.Factories
{
    public class TraderFactory
    {
        static TraderFactory ()
        {
            var susan = new Trader() { Name = "Susan" };
            susan.AddToInventory(ItemFactory.CreateGameItem(1001));

            var ted = new Trader() { Name = "Ted" };
            ted.AddToInventory(ItemFactory.CreateGameItem(1001));

            var pete = new Trader() { Name = "Pete" };
            pete.AddToInventory(ItemFactory.CreateGameItem(1001));

            s_traders = new List<Trader>() { susan, ted, pete };                
        }

        public static Trader GetTrader ( string name ) => s_traders.FirstOrDefault(t => String.Compare(t.Name, name, true) == 0);

        private static readonly List<Trader> s_traders;
    }
}
