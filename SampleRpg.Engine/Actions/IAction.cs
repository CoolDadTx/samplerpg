using System;
using System.Collections.Generic;
using System.Text;

using SampleRpg.Engine.Models;

namespace SampleRpg.Engine.Actions
{
    public interface IAction
    {
        //TODO: Use EventArgs-derived
        event EventHandler<string> Executed;

        void Execute ( LivingEntity actor, LivingEntity target );
    }
}
