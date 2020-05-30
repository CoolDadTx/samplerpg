using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

using SampleRpg.Engine.IO;
using SampleRpg.Engine.Models;

namespace SampleRpg.Engine.Factories
{
    public static class RecipeFactory
    {
        static RecipeFactory ()
        {
            var granolaBar = new Recipe(1, "Granola Bar");
            granolaBar.AddIngredient(3001, 1);
            granolaBar.AddIngredient(3002, 1);
            granolaBar.AddIngredient(3003, 1);
            granolaBar.AddOutput(2001, 1);
            s_recipes.Add(granolaBar);
        }

        public static Recipe GetRecipe ( int id ) => s_recipes.FirstOrDefault(r => r.Id == id);

        private static List<Recipe> LoadItems ()
        {
            if (File.Exists(s_itemFilePath))
            {
                var reader = new RecipeJsonFileReader(s_itemFilePath);

                return reader.Read().ToList();
            } else
                Trace.TraceWarning($"Recipe file '{s_itemFilePath}' not found");

            return new List<Recipe>();
        }

        private const string s_itemFilePath = @".\data\recipes.json";

        private static readonly List<Recipe> s_recipes = new List<Recipe>();
    }
}
