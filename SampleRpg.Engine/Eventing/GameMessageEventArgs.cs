using System;
using System.Collections.Generic;
using System.Text;

namespace SampleRpg.Engine.Eventing
{
    public class GameMessageEventArgs : EventArgs
    {
        public GameMessageEventArgs ( string message )
        {
            Message = message ?? "";
        }

        public string Message { get; private set; }
    }
}
