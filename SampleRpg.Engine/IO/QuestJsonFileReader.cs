using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using SampleRpg.Engine.Actions;
using SampleRpg.Engine.Models;

namespace SampleRpg.Engine.IO
{
    public class QuestJsonFileReader
    {
        public QuestJsonFileReader ( string filename )
        {
            _filename = Path.GetFullPath(filename);               
        }

        public IEnumerable<Quest> Read ()
        {            
            var reader = new JsonFileReader(_filename);
            var dataItems = reader.ReadArray<QuestModel>();
            foreach (var dataItem in dataItems)
            {
                var quest = dataItem.ToQuest();
                if (quest != null)
                    yield return quest;
            };
        }

        #region Private Members

        private sealed class QuestModel
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public string Description { get; set; }

            public RewardModel Reward { get; set; } = new RewardModel();

            public IEnumerable<ItemQuantityModel> RequiredItems { get; set; } = Enumerable.Empty<ItemQuantityModel>();

            public Quest ToQuest ()
            {
                var quest = new Quest(Id, Name, Description, Reward?.Xp ?? 0, Reward?.Gold ?? 0);
                if (Reward?.Items?.Any() ?? false)
                    quest.RewardItems.AddRange(Reward.Items.Select(i => i.ToItemQuantity()));

                if (RequiredItems?.Any() ?? false)
                    quest.ItemsToComplete.AddRange(RequiredItems.Select(i => i.ToItemQuantity()));

                return quest;
            }
        }

        private sealed class RewardModel
        {
            public int Xp { get; set; }

            public int Gold { get; set; }

            public IEnumerable<ItemQuantityModel> Items { get; set; } = Enumerable.Empty<ItemQuantityModel>();               
        }

        private readonly string _filename;

        #endregion
    }
}
