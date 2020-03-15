using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SampleRpg.Engine.Models;

namespace SampleRpg.Engine.Factories
{
    public static class QuestFactory
    {
        static QuestFactory()
        {
            var quest = new Quest() { Id = 1, Name = "Clear the herb garden", Description = "Defeat the snakes in the Herbalist's garden", RewardXp = 25, RewardGold = 10 };
            quest.ItemsToComplete.Add(new ItemQuantity() { ItemId = 9001, Quantity = 5 });
            quest.RewardItems.Add(new ItemQuantity() { ItemId = 1002, Quantity = 1 });

            s_quests = new List<Quest>() {
                quest
            };
        }

        public static Quest FindQuest ( int id ) => s_quests.FirstOrDefault(i => i.Id == id);

        private static readonly List<Quest> s_quests;
    }
}
