using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SampleRpg.Engine.Models
{
    //TODO: Move to root of project instead of "models" namespace    
    /// <summary>Represents a playable character.</summary>
    public class Player : LivingEntity
    {
        //TODO: Make this a separate type
        public string CharacterClass
        {
            get => _class ?? "";
            set => SetProperty(ref _class, value ?? "", nameof(CharacterClass));
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

        //TODO: Consider moving completed quests into separate list so we don't go through them again
        public ObservableCollection<QuestStatus> Quests { get; } = new ObservableCollection<QuestStatus>();
                
        #region Private Members        
        
        private string _class;

        private int _xp;
        private int _level = 1;
                
        #endregion
    }
}
