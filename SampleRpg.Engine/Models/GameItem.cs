using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace SampleRpg.Engine.Models
{
    //TODO: Move to root of project instead of "models" namespace
    public class GameItem : ICloneable<GameItem>
    {
        public int ItemTypeId { get; set; }

        public string Name { get; set; }

        public int Price { get; set; }
        
        public virtual GameItem Clone ( bool deepClone )
        {
            var item = CreateCopy();
            CopyItem(item, deepClone);
            return item;
        }

        //TODO: Clean up this name to be more descriptive
        protected virtual GameItem CreateCopy () => new GameItem();
        
        protected virtual void CopyItem ( GameItem item, bool deepCopy )
        {
            item.ItemTypeId = ItemTypeId;
            item.Name = Name;
            item.Price = Price;
        }
    }
}
