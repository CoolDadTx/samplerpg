using System;

namespace SampleRpg.Engine.Models
{
    //TODO: Don't agree with putting the interface here, shouldn't this be on the VM only
    /// <summary>Represents a playable character.</summary>
    public class Player : NotifyPropertyChangedObject
    {
        public string Name
        {
            get => _name ?? "";
            set => SetProperty(ref _name, value ?? "", nameof(Name));
        }

        //TODO: Make this a separate type
        public string CharacterClass
        {
            get => _class ?? "";
            set => SetProperty(ref _class, value ?? "", nameof(CharacterClass));
        }

        //TODO: Need to add range checking
        public int HitPoints 
        {
            get => _hp;
            set => SetProperty(ref _hp, value, nameof(HitPoints));
        }

        public int ExperiencePoints
        {
            get => _xp;
            set => SetProperty(ref _xp, value, nameof(ExperiencePoints));
        }

        //TODO: Make this calculated based upon XP but cached
        public int Level
        {
            get => _level;
            set => SetProperty(ref _level, value, nameof(Level));
        }

        public int Gold
        {
            get => _gold;
            set => SetProperty(ref _gold, value, nameof(Gold));
        }

        #region Private Members

        private void SetProperty<T>(ref T field, T value, string name ) where T: IEquatable<T>
        {
            if ((field == null && value != null) || !field.Equals(value))
            {
                field = value;
                OnPropertyChanged(name);
            };
        }

        private string _name;
        private string _class;

        private int _hp;
        private int _xp;
        private int _level = 1;
        private int _gold;
                
        #endregion
    }
}
