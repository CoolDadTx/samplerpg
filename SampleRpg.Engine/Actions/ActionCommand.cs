using System;

using SampleRpg.Engine.Models;

namespace SampleRpg.Engine.Actions
{
    public abstract class ActionCommand : IAction
    {        
        public event EventHandler<ActionCommandEventArgs> Executed;

        public void Execute ( LivingEntity source, LivingEntity target ) => ExecuteCore(source, target);                        

        protected virtual void OnExecuted ( ActionCommandEventArgs e ) => Executed?.Invoke(this, e);

        protected abstract void ExecuteCore ( LivingEntity source, LivingEntity target );
    }
}
