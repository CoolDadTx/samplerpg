using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SampleRpg.Engine.Models
{
    public class Monster : NotifyPropertyChangedObject
    {
        public string Name { get; set; }
        public string ImageName { get; set; }

        public string ImagePath => $"pack://application:,,,/Resources/Images/Monsters/{ImageName}";

        public int MaximumHitPoints { get; set; }
        public int HitPoints
        {
            get => _hp;
            set => SetProperty(ref _hp, value, nameof(HitPoints));
        }

        public int RewardXP { get; set; }
        public int RewardGold { get; set; }

        //TODO: Make this a range...
        public int MinimumDamage { get; set; }
        public int MaximumDamage { get; set; }

        public ObservableCollection<ItemQuantity> Inventory { get; } = new ObservableCollection<ItemQuantity>();

        private int _hp;
    }
}
