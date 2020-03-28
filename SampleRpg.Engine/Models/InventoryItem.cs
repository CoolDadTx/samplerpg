using System;

namespace SampleRpg.Engine.Models
{    
    public class InventoryItem : NotifyPropertyChangedObject
    {
        public GameItem Item
        {
            get => _item;
            set {
                if (_item != value)
                {
                    _item = value;
                    OnPropertyChanged(nameof(Item));
                };
            }            
        }

        //TODO: Range checking
        public int Quantity
        {
            get => _quantity;
            set {
                if (_quantity != value)
                {
                    _quantity = value;
                    OnPropertyChanged(nameof(Quantity));
                };
            }
        }

        #region Private Members

        private GameItem _item;
        private int _quantity;
        #endregion
    }
}
