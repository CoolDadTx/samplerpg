using System;
using System.Collections.Generic;
using System.IO;

using SampleRpg.Engine.Actions;
using SampleRpg.Engine.Models;

namespace SampleRpg.Engine.IO
{
    public class ItemJsonFileReader
    {
        public ItemJsonFileReader ( string filename )
        {
            _filename = Path.GetFullPath(filename);               
        }

        public IEnumerable<GameItem> Read ()
        {            
            var reader = new JsonFileReader(_filename);
            var dataItems = reader.ReadArray<ItemModel>();
            foreach (var dataItem in dataItems)
            {
                var gameItem = dataItem.ToGameItem();
                if (gameItem != null)
                    yield return gameItem;
            };
        }

        #region Private Members

        private sealed class ItemModel
        {
            public string Category { get; set; }
            public int Id { get; set; }

            public string Name { get; set; }

            public int Price { get; set; }

            //Weapons
            public int MinimumDamage { get; set; }
            public int MaximumDamage { get; set; }

            //Healing
            public int MinimumHeal { get; set; }
            public int MaximumHeal { get; set; }

            public GameItem ToGameItem ()
            {
                if (!Enum.TryParse<GameItemCategory>(Category, true, out var category))
                    category = GameItemCategory.Miscellaneous;

                var item = new GameItem(category, Id, Name, Price, category == GameItemCategory.Weapon);
                switch (item.Category)
                {
                    case GameItemCategory.Weapon: item.Action = new WeaponAttackCommand(item, MinimumDamage, MaximumDamage); break;
                    case GameItemCategory.Healing: item.Action = new HealCommand(item, MinimumHeal, MaximumHeal); break;
                };

                return item;
            }
        }

        private readonly string _filename;

        #endregion
    }
}
