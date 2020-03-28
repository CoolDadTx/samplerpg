using System;

namespace SampleRpg.Engine.Models
{
    public class Monster : LivingEntity
    {
        public string ImageName { get; set; }

        public string ImagePath => $"pack://application:,,,/Resources/Images/Monsters/{ImageName}";

        public int RewardXP { get; set; }

        //TODO: Make this a range...
        public int MinimumDamage { get; set; }
        public int MaximumDamage { get; set; }
    }
}
