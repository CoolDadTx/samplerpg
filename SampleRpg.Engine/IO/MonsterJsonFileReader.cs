using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

using SampleRpg.Engine.Factories;
using SampleRpg.Engine.Models;

namespace SampleRpg.Engine.IO
{
    public class MonsterJsonFileReader
    {
        public MonsterJsonFileReader ( string filename )
        {
            _filename = Path.GetFullPath(filename);               
        }

        public IEnumerable<Monster> Read ()
        {
            var basePath = Path.GetDirectoryName(_filename);

            var reader = new JsonFileReader(_filename);
            var dataItems = reader.ReadArray<MonsterModel>();
            foreach (var dataItem in dataItems)
            {
                var monster = dataItem.ToMonster();
                if (monster != null)
                {
                    //Images are relative to JSON file so fix the path
                    if (!Path.IsPathRooted(monster.ImagePath))
                        monster.ImagePath = Path.Combine(basePath, monster.ImagePath);

                    yield return monster;
                };
            };
        }

        #region Private Members

        private sealed class LootModel
        {
            public int Id { get; set; }
            public int Chance { get; set; }
        }

        private sealed class MonsterModel
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public int HP { get; set; }

            public int XP { get; set; }

            public string Image { get; set; }

            public int Gold { get; set; }

            public IEnumerable<LootModel> Loot { get; set; } = Enumerable.Empty<LootModel>();

            public int Weapon { get; set; }

            public Monster ToMonster ()
            {
                var monster = new Monster(Id, Name, HP, XP, Image, Gold);

                if (Loot?.Any() ?? false)
                {
                    foreach (var loot in Loot)
                        monster.AddToLootTable(new IdPercentage() { Id = loot.Id, Percentage = loot.Chance });
                };

                if (Weapon > 0)
                {
                    //TODO: Should defer this
                    var weapon = ItemFactory.NewItem(Weapon);
                    if (weapon != null)
                        monster.CurrentWeapon = weapon;
                    else
                        Trace.TraceWarning($"Monster {Id}: Weapon {Weapon} not found");                        
                };

                return monster;
            }
        }

        private readonly string _filename;

        #endregion
    }
}
