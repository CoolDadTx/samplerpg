using System;

namespace SampleRpg.Engine.Models
{
    public static class LivingEntityExtensions
    {
        public static T SetWeapon<T> ( this T source, GameItem weapon ) where T: LivingEntity
        {
            source.CurrentWeapon = weapon;

            return source;
        }
    }
}
