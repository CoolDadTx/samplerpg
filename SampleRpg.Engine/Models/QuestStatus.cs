using System;

namespace SampleRpg.Engine.Models
{
    public class QuestStatus : NotifyPropertyChangedObject
    {
        #region Construction

        public QuestStatus ( Quest quest )
        {
            Quest = quest;
        }
        #endregion

        //TODO: Just store ID so we can look up "current" quest rules later
        public Quest Quest { get; }

        public bool IsCompleted
        {
            get => _isCompleted;
            set => SetProperty(ref _isCompleted, value, nameof(IsCompleted));
        }

        #region Private Members

        private bool _isCompleted;
        #endregion
    }
}
