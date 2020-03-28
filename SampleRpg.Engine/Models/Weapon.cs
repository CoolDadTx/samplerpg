using System;

namespace SampleRpg.Engine.Models
{    
    public class Weapon : GameItem
    {
        public Weapon ()
        {
            //TODO: Don't agree with this
            IsUnique = true;
        }

        public int MinimumDamage { get; set; }
        
        public int MaximumDamage { get; set; }

        protected override GameItem CreateCopy () => new Weapon();

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
