using System;

namespace SampleRpg.Engine.Models
{
    //TODO: Move to root of project instead of "models" namespace
    public class GameItem : ICloneable<GameItem>
    {
        #region Construction

        //TODO: Should use a builder - unrealistic to have all this in a single constructor, maybe a simpler dictionary-based set of properties?
        public GameItem ( GameItemCategory category, int id, string name, int price, bool isUnique = false,
                          int minDamage = 0, int maxDamage = 0)
        {
            Category = category;
            Id = id;
            Name = name;
            Price = price;
            IsUnique = isUnique;
        }
        #endregion

        public int Id { get; protected set; }

        //TODO: Could it be multiple?
        public GameItemCategory Category { get; }

        public string Name { get; protected set; }

        public int Price { get; protected set; }

        public bool IsUnique { get; protected set; }

        //Weapons
        public int MinimumDamage { get; private set; }
        public int MaximumDamage { get; private set; }

        public virtual GameItem Clone ( bool deepClone )
        {
            return new GameItem(Category, Id, Name, Price, IsUnique, MinimumDamage, MaximumDamage);
        }
    }
}
