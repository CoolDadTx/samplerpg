using System;

namespace SampleRpg.Engine.Models
{
    public class Trader : LivingEntity
    {
        public Trader ( string name, int gold = Int32.MaxValue ) : base(name, Int32.MaxValue, gold)
        {
        }
    }
}
