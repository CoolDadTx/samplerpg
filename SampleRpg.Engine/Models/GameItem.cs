using System;
using SampleRpg.Engine.Actions;

namespace SampleRpg.Engine.Models
{
    //TODO: Move to root of project instead of "models" namespace
    public class GameItem : ICloneable<GameItem>
    {
        #region Construction

        //TODO: Should use a builder - unrealistic to have all this in a single constructor, maybe a simpler dictionary-based set of properties?
        //TODO: This doesn't make sense to have a command passed that is specific to a weapon, maybe generalize to "Use"??
        public GameItem ( GameItemCategory category, int id, string name, int price, bool isUnique = false, WeaponAttackCommand attackCommand = null )
        {
            Category = category;
            Id = id;
            Name = name;
            Price = price;
            IsUnique = isUnique;

            AttackCommand = attackCommand;
        }
        #endregion

        public int Id { get; protected set; }

        //TODO: Could it be multiple?
        public GameItemCategory Category { get; }

        public WeaponAttackCommand AttackCommand { get; set; }

        public string Name { get; protected set; }

        public int Price { get; protected set; }

        public bool IsUnique { get; protected set; }

        public virtual GameItem Clone ( bool deepClone )
        {
            return new GameItem(Category, Id, Name, Price, IsUnique, AttackCommand);
        }

        //TODO: This doesn't make sense - only weapons should expose commands for this and params may differ
        public void PerformAction ( LivingEntity source, LivingEntity target ) => AttackCommand.Execute(source, target);        
    }
}
