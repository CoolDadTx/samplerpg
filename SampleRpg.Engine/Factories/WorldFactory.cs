using System;
using System.Collections.Generic;
using System.Text;
using SampleRpg.Engine.Models;

namespace SampleRpg.Engine.Factories
{
    //TODO: Is this better handled using a reader type
    internal static class WorldFactory
    {
        public static World CreateWorld ()
        {
            var world = new World();

            world.AddLocation(-2, -1, "Farmer's Field", "Rows of corn", "pack://application:,,,/Resources/Images/Locations/FarmFields.png")
                 .AddEncounter(2, 100);

            world.AddLocation(-1, -1, "Farmer's House", "House of neighbor", "pack://application:,,,/Resources/Images/Locations/FarmHouse.png");
            world.AddLocation(0, -1, "Home", "This is home", "pack://application:,,,/Resources/Images/Locations/Home.png");
            world.AddLocation(-1, 0, "Trading Shop", "Shop to buy stuff", "pack://application:,,,/Resources/Images/Locations/Trader.png");
            world.AddLocation(0, 0, "Town Square", "Center of town", "pack://application:,,,/Resources/Images/Locations/TownSquare.png");
            
            world.AddLocation(1, 0, "Town Gate", "Gate out of town", "pack://application:,,,/Resources/Images/Locations/TownGate.png");

            world.AddLocation(2, 0, "Spider Forest", "Spiders live in forest", "pack://application:,,,/Resources/Images/Locations/SpiderForest.png")
                 .AddEncounter(3, 100);

            world.AddLocation(0, 1, "Herbalist's Hut", "Small hut of herbs", "pack://application:,,,/Resources/Images/Locations/HerbalistsHut.png")
                 .AddQuest(QuestFactory.FindQuest(1));

            world.AddLocation(0, 2, "Herbalist's Garden", "Garden of herbalist", "pack://application:,,,/Resources/Images/Locations/HerbalistsGarden.png")
                 .AddEncounter(1, 100);
                        
            return world;
        }
    }
}
