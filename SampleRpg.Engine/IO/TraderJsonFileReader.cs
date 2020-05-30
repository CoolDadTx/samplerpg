using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using SampleRpg.Engine.Factories;
using SampleRpg.Engine.Models;

namespace SampleRpg.Engine.IO
{
    public class TraderJsonFileReader
    {
        public TraderJsonFileReader ( string filename )
        {
            _filename = Path.GetFullPath(filename);               
        }

        public IEnumerable<Trader> Read ()
        {            
            var reader = new JsonFileReader(_filename);
            var dataItems = reader.ReadArray<TraderModel>();
            foreach (var dataItem in dataItems)
            {
                var item = dataItem.ToTrader();
                if (item != null)
                    yield return item;
            };
        }

        #region Private Members

        private sealed class TraderModel
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public int Gold { get; set; }

            public IEnumerable<ItemQuantityModel> Inventory { get; set; } = Enumerable.Empty<ItemQuantityModel>();

            public Trader ToTrader ()
            {
                var item = new Trader(Id, Name, Gold);

                //TOD: Should not create item yet
                if (Inventory?.Any() ?? false)
                    foreach (var loot in Inventory)
                        item.AddToInventory(ItemFactory.NewItem(loot.Id), loot.Quantity);

                return item;
            }
        }

        private readonly string _filename;

        #endregion
    }
}
