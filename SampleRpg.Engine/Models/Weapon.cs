using System;
using System.Collections.Generic;
using System.Text;

namespace SampleRpg.Engine.Models
{    
    public class Weapon : GameItem
    {
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
