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
            var herbalistHut = new Location() { XCoordinate = 0, YCoordinate = 1, Name = "Herbalist's Hut", Description = "Small hut of herbs", ImageName = "pack://application:,,,/Resources/Images/Locations/HerbalistsHut.png" };
            herbalistHut.AvailableQuests.Add(QuestFactory.FindQuest(1));

            var world = new World();

            world.AddLocation(-2, -1, "Farmer's Field", "Rows of corn", "pack://application:,,,/Resources/Images/Locations/FarmFields.png");
            world.AddLocation(-1, -1, "Farmer's House", "House of neighbor", "pack://application:,,,/Resources/Images/Locations/FarmHouse.png");
            world.AddLocation(0, -1, "Home", "This is home", "pack://application:,,,/Resources/Images/Locations/Home.png");
            world.AddLocation(-1, 0, "Trading Shop", "Shop to buy stuff", "pack://application:,,,/Resources/Images/Locations/Trader.png");
            world.AddLocation(0, 0, "Town Square", "Center of town", "pack://application:,,,/Resources/Images/Locations/TownSquare.png");
            world.AddLocation(1, 0, "Town Gate", "Gate out of town", "pack://application:,,,/Resources/Images/Locations/TownGate.png");
            world.AddLocation(2, 0, "Spider Forest", "Spiders live in forest", "pack://application:,,,/Resources/Images/Locations/SpiderForest.png");
            world.AddLocation(herbalistHut);
            world.AddLocation(0, 2, "Herbalist's Garden", "Garden of herbalist", "pack://application:,,,/Resources/Images/Locations/HerbalistsGarden.png");
                        
            return world;
        }
    }
}
