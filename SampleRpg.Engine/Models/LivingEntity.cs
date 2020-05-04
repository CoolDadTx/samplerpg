using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SampleRpg.Engine.Models
{
    //TODO: Don't agree with putting the interface here, shouldn't this be on the VM only
    public abstract class LivingEntity : NotifyPropertyChangedObject
    {
        //TODO: Not maintainable, should use a DTO or something so ctors are not so large...
        //TODO: Changed rules, maxHitPoints is max HPs and can reduce HP by taking damage after that...
        protected LivingEntity ( string name, int hp, int gold = 0, int level = 1 )
        {
            Name = name;
            CurrentHitPoints = MaximumHitPoints = hp;
            Gold = gold;
            Level = level;
        }

        public event EventHandler Died;

        //TODO: Doesn't make sense to expose this from player...
        public event EventHandler<string> ActionPerformed;

        public string Name
        {
            get => _name ?? "";
            set 
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        //TODO: How does locking down this property actually help anything?
        public int CurrentHitPoints
        {
            get => _currentHP;
            private set 
            {
                if (_currentHP != value)
                {
                    _currentHP = value;
                    OnPropertyChanged(nameof(CurrentHitPoints));
                };
            }
        }

        public bool IsDead => CurrentHitPoints <= 0;

        //TODO: Need to add range checking
        //TODO: How does locking down this property actually help anything?
        public int MaximumHitPoints
        {
            get => _maxHP;
            protected set {
                if (_maxHP != value)
                {
                    _maxHP = value;
                    OnPropertyChanged(nameof(MaximumHitPoints));
                };
            }
        }

        public GameItem CurrentWeapon
        {
            get => _weapon;
            set
            {
                if (_weapon == value)
                    return;

                if (_weapon != null)
                    _weapon.Action.Executed -= OnActionPerformed;

                _weapon = value;
                if (_weapon != null)
                    _weapon.Action.Executed += OnActionPerformed;

                OnPropertyChanged(nameof(CurrentWeapon));
            }
        }

        public void UseCurrentWeapon ( LivingEntity target ) => CurrentWeapon?.PerformAction(this, target);
        
        //TODO: Need to add range checking
        //TODO: How does locking down this property actually help anything?
        public int Gold
        {
            get => _gold;
            private set {
                if (_gold != value)
                {
                    _gold = value;
                    OnPropertyChanged(nameof(Gold));
                };
            }
        }

        //TODO: Make this calculated based upon XP but cached
        //TODO: Shouldn't be settable really...
        public int Level
        {
            get => _level;
            protected set => SetProperty(ref _level, value, nameof(Level));
        }

        //TODO: Convert to Inventory type
        public ObservableCollection<InventoryItem> Inventory { get; } = new ObservableCollection<InventoryItem>();

        //TODO: This doesn't work if value is added to Inventory directly...
        //TODO: Put in Inventory class
        public void AddToInventory ( GameItem item, int quantity = 1 )
        {
            if (item.IsUnique)
                Inventory.Add(new InventoryItem() { Item = item, Quantity = 1 });
            else //Increment the existing inventory or add a new one
            {
                var existing = FindInventoryItem(item.Id);
                if (existing == null)
                {
                    existing = new InventoryItem() { Item = item, Quantity = quantity };
                    Inventory.Add(existing);
                } else
                    existing.Quantity += quantity;
            };

            if (item.IsWeapon())
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

            if (existing.Item.IsWeapon())
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
        public IEnumerable<GameItem> Weapons => Inventory.Where(i => i.Item.IsWeapon()).Select(i => i.Item);

        //TODO: Shouldn't this be part of HP property instead, what do we really gain here?                
        public void Heal ( int hitPoints )
        {
            CurrentHitPoints = Math.Max(CurrentHitPoints + hitPoints, MaximumHitPoints);
        }

        public void HealAll () => CurrentHitPoints = MaximumHitPoints;

        public void TakeDamage ( int damage )
        {
            CurrentHitPoints = Math.Max(0, CurrentHitPoints - damage);

            if (IsDead)
                OnDied();            
        }

        //TODO: How is this better than just using the setter?
        public void AddGold ( int gold ) => Gold += gold;
        public void RemoveGold ( int gold )
        {
            if (Gold < gold)
                throw new ArgumentOutOfRangeException($"Insufficient gold, {Name} only has {Gold} gold");

            Gold -= gold;
        }

        protected virtual void OnDied () => Died?.Invoke(this, EventArgs.Empty);

        #region Private Members

        private void OnActionPerformed ( object sender, string message ) => ActionPerformed?.Invoke(this, message);

        private InventoryItem FindInventoryItem ( int id ) => Inventory.FirstOrDefault(x => x.Item.Id == id);

        private string _name;

        private int _currentHP, _maxHP;
        private int _gold;

        private int _level;

        private GameItem _weapon;
        #endregion
    }
}
