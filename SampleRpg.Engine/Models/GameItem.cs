using System;

namespace SampleRpg.Engine.Models
{
    //TODO: Move to root of project instead of "models" namespace
    public class GameItem : ICloneable<GameItem>
    {
        #region Construction

        //TODO: Should use a definition object so we don't pass all these parameters...
        public GameItem ( int id, string name, int price, bool isUnique = false )
        {
            ItemTypeId = id;
            Name = name;
            Price = price;
            IsUnique = isUnique;
        }
        #endregion

        public int ItemTypeId { get; protected set; }

        public string Name { get; protected set; }

        public int Price { get; protected set; }

        public bool IsUnique { get; protected set; }
        
        public virtual GameItem Clone ( bool deepClone )
        {
            var item = CreateCopy();
            CopyItem(item, deepClone);
            return item;
        }

        //TODO: Clean up this name to be more descriptive
        protected virtual GameItem CreateCopy () => new GameItem(ItemTypeId, Name, Price, IsUnique);
        
        protected virtual void CopyItem ( GameItem item, bool deepCopy )
        {
            item.ItemTypeId = ItemTypeId;
            item.Name = Name;
            item.Price = Price;
            item.IsUnique = IsUnique;
        }
    }
}
