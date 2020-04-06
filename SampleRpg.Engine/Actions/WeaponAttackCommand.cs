﻿using System;

using P3Net.Kraken.Diagnostics;

using SampleRpg.Engine.Models;

namespace SampleRpg.Engine.Actions
{
    public class WeaponAttackCommand
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

        //TODO: How useful is a string response. Is an event even needed here?
        public event EventHandler<string> Executed;

        public void Execute ( LivingEntity source, LivingEntity target )
        {
            var dmg = Rng.Between(_minDmg, _maxDmg);
            if (dmg == 0)
                RaiseExecuted("You missed");
            else
            {
                RaiseExecuted($"You hit for {dmg} damage");
                target.TakeDamage(dmg);
            };
        }

        #region Private Members

        private void RaiseExecuted ( string result ) => Executed?.Invoke(this, result);

        private readonly GameItem _weapon;
        private readonly int _minDmg, _maxDmg;
        #endregion
    }
}
