using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SampleRpg.Engine.Models
{
    //TODO: Why not derive from GameItem?
    public class Recipe
    {
        #region Construction

        public Recipe ( int id, string name )
        {
            Id = id;
            Name = name ?? "";
        }
        #endregion

        public int Id { get; }

        public string Name { get; }

        public List<ItemQuantity> Ingredients { get; } = new List<ItemQuantity>();
        public List<ItemQuantity> Outputs { get; } = new List<ItemQuantity>();

        //TODO: Should this be in Ingredients class
        public void AddIngredient ( int itemId, int quantity )
        {
            var existing = Ingredients.FirstOrDefault(i => i.ItemId == itemId);
            if (existing == null)
            {
                existing = new ItemQuantity() { ItemId = itemId };
                Ingredients.Add(existing);
            };

            existing.Quantity += quantity;
        }

        public void AddOutput ( int itemId, int quantity )
        {
            var existing = Outputs.FirstOrDefault(i => i.ItemId == itemId);
            if (existing == null)
            {
                existing = new ItemQuantity() { ItemId = itemId };
                Outputs.Add(existing);
            };

            existing.Quantity += quantity;
        }
    }
}
