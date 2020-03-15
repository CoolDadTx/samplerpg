using System;
using System.ComponentModel;

namespace SampleRpg.Engine
{
    public abstract class NotifyPropertyChangedObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged ( string name ) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
