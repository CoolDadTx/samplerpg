using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SampleRpg.Engine.Models
{
    //TODO: Don't agree with putting the interface here, shouldn't this be on the VM only
    public abstract class LivingEntity : NotifyPropertyChangedObject
    {
        public string Name
        {
            get => _name ?? "";
            set 
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        //TODO: Need to add range checking
        public int CurrentHitPoints
        {
            get => _currentHP;
            set 
            {
                if (_currentHP != value)
                {
                    _currentHP = value;
                    OnPropertyChanged(nameof(CurrentHitPoints));
                };
            }
        }

        //TODO: Need to add range checking
        public int MaximumHitPoints
        {
            get => _maxHP;
            set {
                if (_maxHP != value)
                {
                    _maxHP = value;
                    OnPropertyChanged(nameof(MaximumHitPoints));
                };
            }
        }

        //TODO: Need to add range checking
        public int Gold
        {
            get => _gold;
            set {
                if (_gold != value)
                {
                    _gold = value;
                    OnPropertyChanged(nameof(Gold));
                };
            }
        }

        //TODO: Convert to Inventory type
        public ObservableCollection<InventoryItem> Inventory { get; set; } = new ObservableCollection<InventoryItem>();

        //TODO: This doesn't work if value is added to Inventory directly...
        //TODO: Put in Inventory class
        public void AddToInventory ( GameItem item, int quantity = 1 )
        {
            if (item.IsUnique)
                Inventory.Add(new InventoryItem() { Item = item, Quantity = 1 });
            else //Increment the existing inventory or add a new one
            {
                var existing = FindInventoryItem(item.ItemTypeId);
                if (existing == null)
                {
                    existing = new InventoryItem() { Item = item, Quantity = quantity };
                    Inventory.Add(existing);
                } else
                    existing.Quantity += quantity;
            };

            if (item is Weapon)
                OnPropertyChanged(nameof(Weapons));
        }

        //TODO: Put in Inventory class
        public void RemoveFromInventory ( int id, int count = 1 )
        {
            var existing = FindInventoryItem(id);
            if (existing == null)
                return;

            existing.Quantity -= count;
            if (existing.Quantity <= 0)
                Inventory.Remove(existing);

            if (existing.Item is Weapon)
                OnPropertyChanged(nameof(Weapons));
        }

        //TODO: Shouldn't be an attribute of player
        public bool HasAllItems ( IEnumerable<ItemQuantity> items )
        {            
            foreach (var item in items)
            {
                var existing = FindInventoryItem(item.ItemId);
                if ((existing?.Quantity ?? 0) < item.Quantity)                
                    return false;
            };

            return true;
        }

        //TODO: Why are we using List<T> here?
        public IEnumerable<Weapon> Weapons => Inventory.Select(i => i.Item).OfType<Weapon>();

        #region Private Members

        private InventoryItem FindInventoryItem ( int id ) => Inventory.FirstOrDefault(x => x.Item.ItemTypeId == id);

        private string _name;

        private int _currentHP, _maxHP;
        private int _gold;
        #endregion
    }
}
