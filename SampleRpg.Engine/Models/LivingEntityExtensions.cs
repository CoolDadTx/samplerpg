using System;
using System.Collections.Generic;

namespace SampleRpg.Engine.Models
{
    public static class LivingEntityExtensions
    {
        public static void RemoveItemsFromInventory<T> ( this T source, IEnumerable<ItemQuantity> items ) where T: LivingEntity
        {
            foreach (var item in items)
            {
                source.RemoveFromInventory(item.ItemId, item.Quantity);
            };
        }

        public static T SetWeapon<T> ( this T source, GameItem weapon ) where T: LivingEntity
        {
            source.CurrentWeapon = weapon;

            return source;
        }
    }
}
