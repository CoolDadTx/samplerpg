using System;

using SampleRpg.Engine.Models;

namespace SampleRpg.Engine.Actions
{
    public interface IAction
    {
        event EventHandler<ActionCommandEventArgs> Executed;

        void Execute ( LivingEntity actor, LivingEntity target );
    }
}
