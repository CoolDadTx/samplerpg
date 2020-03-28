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
        public ObservableCollection<GameItem> Inventory { get; set; } = new ObservableCollection<GameItem>();

        //TODO: This doesn't work if value is added to Inventory directly...
        //TODO: Put in Inventory class
        public void AddToInventory ( GameItem item )
        {
            //TODO: Doesn't handle adding multiple items of same type
            Inventory.Add(item);

            if (item is Weapon)
                OnPropertyChanged(nameof(Weapons));
        }

        //TODO: Put in Inventory class
        public void RemoveFromInventory ( int id, int count = 1 )
        {
            //TODO: Doesn't work if item isn't same instance
            while (count-- > 0)
            {
                var item = Inventory.FirstOrDefault(i => i.ItemTypeId == id);
                if (item != null)
                    Inventory.Remove(item);
            };
            OnPropertyChanged(nameof(Weapons));
        }

        //TODO: Shouldn't be an attribute of player
        public bool HasAllItems ( IEnumerable<ItemQuantity> items )
        {
            foreach (var item in items)
            {
                if (Inventory.Where(x => x.ItemTypeId == item.ItemId).Count() < item.Quantity)
                    return false;
            };

            return true;
        }

        //TODO: Why are we using List<T> here?
        public IEnumerable<Weapon> Weapons => Inventory.OfType<Weapon>();

        #region Private Members

        private string _name;

        private int _currentHP, _maxHP;
        private int _gold;
        #endregion
    }
}
