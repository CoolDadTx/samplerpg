using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

using SampleRpg.Engine.IO;
using SampleRpg.Engine.Models;

namespace SampleRpg.Engine.Factories
{
    public static class ItemFactory
    {
        static ItemFactory()
        {
            s_items = LoadItems();            
        }

        public static string GetItemName ( int id ) => s_items.FirstOrDefault(i => i.Id == id)?.Name ?? "";
        
        public static GameItem NewItem ( int id )
        {
            var item = s_items.FirstOrDefault(i => i.Id == id);
            
            return item?.Clone(true);
        }

        #region Private Members

        private static List<GameItem> LoadItems ( )
        {
            if (File.Exists(s_itemFilePath))                
            {
                var reader = new ItemJsonFileReader(s_itemFilePath);

                return reader.Read().ToList();
            } else
                Trace.TraceWarning($"Items file '{s_itemFilePath}' not found");

            return new List<GameItem>();
        }        

        private const string s_itemFilePath = @".\data\items.json";

        private static readonly List<GameItem> s_items = new List<GameItem>();
        #endregion
    }
}
