using System;
using System.Collections.Generic;
using System.Linq;

namespace SampleRpg.Engine.Models
{
    public static class LocationExtensions
    {
        public static Location AddEncounter ( this Location source, int monsterId, int percentage )
        {
            var existing = source.Encounters.FirstOrDefault(x => x.MonsterId == monsterId);
            if (existing != null)
                existing.Percentage = percentage;
            else
                source.Encounters.Add(new Encounter() { MonsterId = monsterId, Percentage = percentage });

            return source;
        }

        //TODO: Should use just quest Id
        public static Location AddQuest ( this Location source, Quest quest )
        {
            var existing = source.AvailableQuests.FirstOrDefault(x => x.Id == quest.Id);
            if (existing == null)
                source.AvailableQuests.Add(quest);

            return source;
        }
    }
}
