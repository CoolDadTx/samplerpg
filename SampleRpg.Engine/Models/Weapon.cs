using System;

namespace SampleRpg.Engine.Models
{    
    public class Weapon : GameItem
    {
        //TODO: Don't agree that all weapons are unique
        public Weapon ( int id, string name, int price, int minDamage, int maxDamage ) : base(id, name, price, true)
        {
            MinimumDamage = minDamage;
            MaximumDamage = maxDamage;
        }

        public int MinimumDamage { get; private set; }
        
        public int MaximumDamage { get; private set; }

        protected override GameItem CreateCopy () => new Weapon(ItemTypeId, Name, Price, MinimumDamage, MaximumDamage);

        protected override void CopyItem ( GameItem item, bool deepCopy )
        {
            base.CopyItem(item, deepCopy);

            if (item is Weapon wpn)
            {
                wpn.MinimumDamage = MinimumDamage;
                wpn.MaximumDamage = MaximumDamage;
            };                        
        }
    }
}
