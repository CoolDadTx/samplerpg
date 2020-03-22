using System;

using SampleRpg.Engine.Models;

namespace SampleRpg.Engine.Factories
{
    //TODO: Is this better handled using a reader type
    internal static class WorldFactory
    {
        //TODO: Switch to a coordinate system so we can add things at a particular coordinate rather than
        //tying to a specific location object
        public static World CreateWorld ()
        {
            var world = new World();

            world.AddLocation(-2, -1, "Farmer's Field", "Rows of corn", "FarmFields.png")
                 .AddEncounter(2, 100);

            world.AddLocation(-1, -1, "Farmer's House", "House of neighbor", "FarmHouse.png")
                  .TraderHere = TraderFactory.GetTrader("Ted");

            world.AddLocation(0, -1, "Home", "This is home", "Home.png");
            world.AddLocation(-1, 0, "Trading Shop", "Shop to buy stuff", "Trader.png")
                 .TraderHere = TraderFactory.GetTrader("Susan");

            world.AddLocation(0, 0, "Town Square", "Center of town", "TownSquare.png");
            
            world.AddLocation(1, 0, "Town Gate", "Gate out of town", "TownGate.png");

            world.AddLocation(2, 0, "Spider Forest", "Spiders live in forest", "SpiderForest.png")
                 .AddEncounter(3, 100);

            world.AddLocation(0, 1, "Herbalist's Hut", "Small hut of herbs", "HerbalistsHut.png")
                 .AddQuest(QuestFactory.FindQuest(1))
                 .TraderHere = TraderFactory.GetTrader("Pete");

            world.AddLocation(0, 2, "Herbalist's Garden", "Garden of herbalist", "HerbalistsGarden.png")
                 .AddEncounter(1, 100);
                        
            return world;
        }
    }
}
