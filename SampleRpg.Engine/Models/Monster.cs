using System;
using System.Collections.Generic;

using P3Net.Kraken;

using SampleRpg.Engine.Factories;

namespace SampleRpg.Engine.Models
{
    //TODO: Consider creating Kind class that defines the attributes of the kind
    //and then Monster becomes an instance such that HPs and other attributes can vary
    //TODO: Consider creating a MonsterInstance that represents a single monster and this is just the definition
    public class Monster : LivingEntity
    {
        public Monster ( int id, string name, int hp, int xp, string imageName = null, int gold = 0 ) : base(name, hp, gold)
        {
            Id = id;
            RewardXP = xp;

            ImagePath = (imageName ?? name).RemoveAll(' ').ToLower();
        }

        public int Id { get; }

        public string ImagePath { get; set; }

        public int RewardXP { get; private set; }

        public IEnumerable<IdPercentage> LootTable => _lootTable;

        public Monster AddToLootTable ( IdPercentage item )
        {
            _lootTable.Add(item);

            return this;
        }

        //TODO: Use MonsterInstance instead
        public virtual Monster CreateInstance ()
        {
            var instance = new Monster(Id, Name, MaximumHitPoints, RewardXP, ImagePath, Gold);

            //Figure out loot
            foreach (var loot in _lootTable)
            {
                if (Rng.Between(1, 100) <= loot.Percentage)
                    instance.AddToInventory(ItemFactory.NewItem(loot.Id));                
            };

            instance.CurrentWeapon = CurrentWeapon;

            return instance;
        }

        #region Private Members

        private readonly List<IdPercentage> _lootTable = new List<IdPercentage>();
        #endregion
    }
}
