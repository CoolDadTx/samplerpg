using System;
using System.Collections.Generic;
using System.Text;

namespace SampleRpg.Engine.Models
{
    public class QuestStatus
    {
        //TODO: Just store ID so we can look up "current" quest rules later
        public Quest Quest { get; set; }
        public bool IsCompleted { get; set; }
    }
}
