using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SampleRpg.Engine
{
    public abstract class NotifyPropertyChangedObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged ( string name ) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        protected void SetProperty<T> ( ref T field, T value, [CallerMemberName] string name = "") where T : IEquatable<T>
        {
            if ((field == null && value != null) || !field.Equals(value))
            {
                field = value;
                OnPropertyChanged(name);
            };
        }
    }
}
