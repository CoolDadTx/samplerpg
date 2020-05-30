using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using SampleRpg.Engine.Models;

namespace SampleRpg.Engine.IO
{
    public class RecipeJsonFileReader
    {
        public RecipeJsonFileReader ( string filename )
        {
            _filename = Path.GetFullPath(filename);               
        }

        public IEnumerable<Recipe> Read ()
        {            
            var reader = new JsonFileReader(_filename);
            var dataItems = reader.ReadArray<RecipeModel>();
            foreach (var dataItem in dataItems)
            {
                var recipe = dataItem.ToRecipe();
                if (recipe != null)
                    yield return recipe;
            };
        }

        #region Private Members

        private sealed class RecipeModel
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public IEnumerable<ItemQuantityModel> Ingredients { get; set; } = Enumerable.Empty<ItemQuantityModel>();

            public IEnumerable<ItemQuantityModel> Output { get; set; } = Enumerable.Empty<ItemQuantityModel>();

            public Recipe ToRecipe ()
            {
                var item = new Recipe(Id, Name);

                if (Ingredients?.Any() ?? false)
                    foreach (var child in Ingredients)
                        item.AddIngredient(child.Id, child.Quantity);

                if (Output?.Any() ?? false)
                    foreach (var child in Output)
                        item.AddOutput(child.Id, child.Quantity);

                return item;
            }
        }

        private readonly string _filename;

        #endregion
    }
}
