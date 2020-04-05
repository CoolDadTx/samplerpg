using System;

namespace SampleRpg.Engine.Models
{
    public static class WeaponExtensions
    {
        public static bool IsWeapon ( this GameItem source ) => source.Category == GameItemCategory.Weapon;
    }
}
