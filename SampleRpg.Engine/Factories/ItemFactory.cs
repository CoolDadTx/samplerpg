using System;
using System.Collections.Generic;
using System.Linq;

using SampleRpg.Engine.Actions;
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
            var items = new List<GameItem>();

            var filePath = FilePaths.GetDataFilePath(s_itemFilePath);
            if (FilePaths.FileExists(filePath))
            {
                var reader = new DataFileReader(filePath);
                
                //TODO: Should be a better way to do this
                var dataItems = reader.ReadAll<ItemModel>();
                foreach (var dataItem in dataItems)
                {
                    var item = ToGameItem(dataItem);
                    if (item != null)
                        items.Add(item);
                };
            };

            return items;
        }

        private static GameItem ToGameItem ( ItemModel model )
        {
            if (!Enum.TryParse<GameItemCategory>(model.Category, true, out var category))
                category = GameItemCategory.Miscellaneous;                    

            var item = new GameItem(category, model.Id, model.Name, model.Price, category == GameItemCategory.Weapon);
            switch (item.Category)
            {
                case GameItemCategory.Weapon: item.Action = new WeaponAttackCommand(item, model.MinimumDamage, model.MaximumDamage); break;
                case GameItemCategory.Healing: item.Action = new HealCommand(item, model.MinimumHeal, model.MaximumHeal); break;                   
            };

            return item;
        }

        private const string s_itemFilePath = "items.json";

        private static readonly List<GameItem> s_items = new List<GameItem>();
        #endregion
    }
}
