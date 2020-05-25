using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

using SampleRpg.Engine.IO;
using SampleRpg.Engine.Models;

namespace SampleRpg.Engine.Factories
{
    public static class MonsterFactory
    {
        //TODO: Create helper method for creating monsters
        public static Monster Get ( int id )
        {
            var monster = Monsters.FirstOrDefault(m => m.Id == id);
            if (monster == null)
                throw new ArgumentOutOfRangeException(nameof(id), "Unknown monster");

            //TODO: Should this be handled at a higher level to allow for more flexibility
            return monster.CreateInstance();
        }

        #region Private Members

        private static List<Monster> LoadMonsters ()
        {
            if (File.Exists(s_filePath))
            {
                var reader = new MonsterJsonFileReader(s_filePath);

                return reader.Read().ToList();
            } else
                Trace.TraceWarning($"Monsters file '{s_filePath}' not found");

            return new List<Monster>();
        }

        private const string s_filePath = @".\data\monsters.json";

        private static List<Monster> Monsters => s_monsters.Value;
        private static readonly Lazy<List<Monster>> s_monsters = new Lazy<List<Monster>>(LoadMonsters);
        #endregion
    }
}
