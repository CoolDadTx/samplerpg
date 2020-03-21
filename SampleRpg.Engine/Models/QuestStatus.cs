using System;
using System.Collections.Generic;
using System.Text;

namespace SampleRpg.Engine.Models
{
    public class QuestStatus : NotifyPropertyChangedObject
    {
        //TODO: Just store ID so we can look up "current" quest rules later
        public Quest Quest 
        {
            get => _quest;
            set
            {
                if (_quest != value)
                {
                    _quest = value;
                    OnPropertyChanged(nameof(Quest));
                };
            }
        }

        public bool IsCompleted
        {
            get => _isCompleted;
            set => SetProperty(ref _isCompleted, value, nameof(IsCompleted));
        }

        private Quest _quest;
        private bool _isCompleted;
    }
}
