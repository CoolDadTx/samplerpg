using System;

using P3Net.Kraken;
using P3Net.Kraken.Diagnostics;

namespace SampleRpg.Engine.Models
{
    //TODO: Consider creating Kind class that defines the attributes of the kind
    //and then Monster becomes an instance such that HPs and other attributes can vary
    public class Monster : LivingEntity
    {
        public Monster ( string name, int hp, int xp, string imageName = null, int gold = 0 ): base(name, hp, gold)
        {
            RewardXP = xp;

            ImageName = (imageName ?? name).RemoveAll(' ').ToLower() + ".png";
        }

        public string ImageName { get; private set; }

        public string ImagePath => $"pack://application:,,,/Resources/Images/Monsters/{ImageName}";

        public int RewardXP { get; private set; }
    }
}
