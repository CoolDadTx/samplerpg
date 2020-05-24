using System;

namespace SampleRpg.Engine.Factories
{
    internal class ItemModel
    {
        public string Category { get; set; }
        public int Id { get; set; }

        public string Name { get; set; }

        public int Price { get; set; }

        //Weapons
        public int MinimumDamage { get; set; }
        public int MaximumDamage { get; set; }

        //Healing
        public int MinimumHeal { get; set; }
        public int MaximumHeal { get; set; }
    }
}
