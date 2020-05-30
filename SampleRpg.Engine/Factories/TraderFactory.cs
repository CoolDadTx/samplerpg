using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

using SampleRpg.Engine.IO;
using SampleRpg.Engine.Models;

namespace SampleRpg.Engine.Factories
{
    public class TraderFactory
    {
        static TraderFactory ()
        {
            s_traders = LoadItems();
        }

        public static Trader GetTrader ( int id ) => s_traders.FirstOrDefault(t => t.Id == id);

        private static List<Trader> LoadItems ()
        {
            if (File.Exists(s_itemFilePath))
            {
                var reader = new TraderJsonFileReader(s_itemFilePath);

                return reader.Read().ToList();
            } else
                Trace.TraceWarning($"Trader file '{s_itemFilePath}' not found");

            return new List<Trader>();
        }

        private const string s_itemFilePath = @".\data\traders.json";

        private static readonly List<Trader> s_traders;
    }
}
