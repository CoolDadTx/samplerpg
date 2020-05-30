using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

using SampleRpg.Engine.IO;
using SampleRpg.Engine.Models;

namespace SampleRpg.Engine.Factories
{
    public static class QuestFactory
    {
        static QuestFactory()
        {
            s_quests = LoadItems();
        }

        public static Quest FindQuest ( int id ) => s_quests.FirstOrDefault(i => i.Id == id);

        private static List<Quest> LoadItems ()
        {
            if (File.Exists(s_itemFilePath))
            {
                var reader = new QuestJsonFileReader(s_itemFilePath);

                return reader.Read().ToList();
            } else
                Trace.TraceWarning($"Quest file '{s_itemFilePath}' not found");

            return new List<Quest>();
        }

        private const string s_itemFilePath = @".\data\quests.json";

        private static readonly List<Quest> s_quests;
    }
}
