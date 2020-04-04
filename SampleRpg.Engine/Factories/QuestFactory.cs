using System;
using System.Collections.Generic;
using System.Linq;

using SampleRpg.Engine.Models;

namespace SampleRpg.Engine.Factories
{
    public static class QuestFactory
    {
        static QuestFactory()
        {
            var quest = new Quest(1, "Clear the herb garden", "Defeat the snakes in the Herbalist's garden", rewardXp: 25, rewardGold: 10);
            quest.ItemsToComplete.Add(new ItemQuantity() { ItemId = 9002, Quantity = 2 });
            quest.RewardItems.Add(new ItemQuantity() { ItemId = 1002, Quantity = 1 });

            s_quests = new List<Quest>() {
                quest
            };
        }

        public static Quest FindQuest ( int id ) => s_quests.FirstOrDefault(i => i.Id == id);

        private static readonly List<Quest> s_quests;
    }
}
