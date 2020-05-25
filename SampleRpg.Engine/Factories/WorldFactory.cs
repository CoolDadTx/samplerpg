using System;
using System.IO;

using SampleRpg.Engine.IO;
using SampleRpg.Engine.Models;

namespace SampleRpg.Engine.Factories
{
    internal static class WorldFactory
    {
        public static World CreateWorld ()
        {
            var world = new World();

            if (!File.Exists(s_dataFile))
                throw new FileNotFoundException("World file not found");

            var reader = new WorldJsonFileReader(s_dataFile);            
            
            foreach (var location in reader.Read())
            {
                world.AddLocation(location);
            };                        

            return world;
        }
        
        #region Private Members

        private const string s_dataFile = @".\data\world.json";
        #endregion
    }
}
