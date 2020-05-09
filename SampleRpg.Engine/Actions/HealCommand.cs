using System;

using P3Net.Kraken.Diagnostics;

using SampleRpg.Engine.Models;

namespace SampleRpg.Engine.Actions
{
    public class HealCommand : ActionCommand
    {
        #region Construction

        //TODO: Heal value should be an attribute of the item
        public HealCommand ( GameItem item, int minHeal, int maxHeal )
        {
            Verify.Argument(nameof(item)).WithValue(item).IsNotNull();
            Verify.Argument(nameof(minHeal)).WithValue(minHeal).IsGreaterThanOrEqualToZero();
            Verify.Argument(nameof(maxHeal)).WithValue(maxHeal).IsGreaterThanOrEqualTo(minHeal);

            _item = item;
            _minHeal = minHeal;
            _maxHeal = maxHeal;
        }
        #endregion

        protected override void ExecuteCore ( LivingEntity source, LivingEntity target )
        {
            var targetName = (target is Player) ? "You are" : $"The {target.Name} is";

            var hp = Rng.Between(_minHeal, _maxHeal);
            OnExecuted(new ActionCommandEventArgs($"{targetName} healed for {hp} points"));
            target.Heal(hp);

            //One use item
            target.RemoveFromInventory(_item.Id, 1);
        }

        #region Private Members

        private readonly GameItem _item;
        private readonly int _minHeal, _maxHeal;
        #endregion
    }
}
