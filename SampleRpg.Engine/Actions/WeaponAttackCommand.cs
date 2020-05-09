using System;

using P3Net.Kraken.Diagnostics;

using SampleRpg.Engine.Models;

namespace SampleRpg.Engine.Actions
{
    public class WeaponAttackCommand : ActionCommand
    {
        #region Construction

        //TODO: Doesn't make sense to create a command with a specific weapon, should be parameterized data to execute
        public WeaponAttackCommand ( GameItem weapon, int minDamage, int maxDamage )
        {
            Verify.Argument(nameof(weapon)).WithValue(weapon).IsNotNull().And.Is(x => x.IsWeapon(), "Must be a weapon");
            Verify.Argument(nameof(minDamage)).WithValue(minDamage).IsGreaterThanOrEqualToZero();
            Verify.Argument(nameof(maxDamage)).WithValue(maxDamage).IsGreaterThanOrEqualTo(minDamage);

            _weapon = weapon;
            _minDmg = minDamage;
            _maxDmg = maxDamage;
        }
        #endregion

        protected override void ExecuteCore ( LivingEntity source, LivingEntity target )
        {
            var sourceName = (source is Player) ? "You" : $"The {source.Name}";
            var targetName = (target is Player) ? "You" : $"The {target.Name}";
                        
            var dmg = Rng.Between(_minDmg, _maxDmg);
            if (dmg == 0)
                OnExecuted(new ActionCommandEventArgs($"{sourceName} missed"));
            else
            {
                OnExecuted(new ActionCommandEventArgs($"{sourceName} hit {targetName} for {dmg} damage"));
                target.TakeDamage(dmg);
            };
        }

        #region Private Members

        private readonly GameItem _weapon;
        private readonly int _minDmg, _maxDmg;
        #endregion
    }
}
