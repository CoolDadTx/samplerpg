using System;

namespace SampleRpg.Engine.Actions
{
    public class ActionCommandEventArgs : EventArgs
    {
        public ActionCommandEventArgs ( string message )
        {
            Message = message ?? "";
        }

        public string Message { get; }
    }
}
