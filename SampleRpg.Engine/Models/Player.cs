using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace SampleRpg.Engine.Models
{
    //TODO: Move to root of project instead of "models" namespace    
    /// <summary>Represents a playable character.</summary>
    public class Player : LivingEntity
    {
        #region Construction

        public Player ( string name, string characterClass, int hp, int gold = 0 ) : base(name, hp, gold)
        {
            CharacterClass = characterClass;
        }
        #endregion

        public event EventHandler LeveledUp;

        //TODO: Make this a separate type
        public string CharacterClass
        {
            get => _class ?? "";
            private set => SetProperty(ref _class, value ?? "", nameof(CharacterClass));
        }        

        public int ExperiencePoints
        {
            get => _xp;
            private set 
            {
                SetProperty(ref _xp, value, nameof(ExperiencePoints));

                CheckForLevelUp();
            }
        }

        //TODO: How does this benefit us over the setter?
        public void AddXP ( int xp )
        {
            if (xp <= 0)
                return;

            ExperiencePoints += xp;
        }

        //TODO: Consider moving completed quests into separate list so we don't go through them again
        public ObservableCollection<QuestStatus> Quests { get; } = new ObservableCollection<QuestStatus>();

        //
        //Recipes
        public ObservableCollection<Recipe> Recipes { get; } = new ObservableCollection<Recipe>();

        public void LearnRecipe ( Recipe recipe )
        {
            if (!Recipes.Any(r => r.Id == recipe.Id))
                Recipes.Add(recipe);
        }
        #region Private Members        
        
        private void CheckForLevelUp ()
        {
            var newLevel = (ExperiencePoints / 100) + 1;
            
            //TODO: This only levels up once, handle multiples
            for (var next = Level + 1; next <= newLevel; ++next)            
            {
                Level = next;

                //Randomly increase HPs
                var hp = Rng.Between(1, 10);
                MaximumHitPoints += hp;
                HealAll();
                LeveledUp?.Invoke(this, EventArgs.Empty);
            };
        }

        private string _class;
        private int _xp;
                        
        #endregion
    }
}
