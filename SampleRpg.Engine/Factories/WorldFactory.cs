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
            return new World()
                        .AddLocation(-2, -1, "Farmer's Field", "Rows of corn", "pack://application:,,,/Resources/Images/Locations/FarmFields.png")
                        .AddLocation(-1, -1, "Farmer's House", "House of neighbor", "pack://application:,,,/Resources/Images/Locations/FarmHouse.png")
                        .AddLocation(0, -1, "Home", "This is home", "pack://application:,,,/Resources/Images/Locations/Home.png")
                        .AddLocation(-1, 0, "Trading Shop", "Shop to buy stuff", "pack://application:,,,/Resources/Images/Locations/Trader.png")
                        .AddLocation(0, 0, "Town Square", "Center of town", "pack://application:,,,/Resources/Images/Locations/TownSquare.png")
                        .AddLocation(1, 0, "Town Gate", "Gate out of town", "pack://application:,,,/Resources/Images/Locations/TownGate.png")
                        .AddLocation(2, 0, "Spider Forest", "Spiders live in forest", "pack://application:,,,/Resources/Images/Locations/SpiderForest.png")
                        .AddLocation(0, 1, "Herbalist's Hut", "Small hut of herbs", "pack://application:,,,/Resources/Images/Locations/HerbalistsHut.png")
                        .AddLocation(0, 2, "Herbalist's Garden", "Garden of herbalist", "pack://application:,,,/Resources/Images/Locations/HerbalistsGarden.png")
                        ;
        }
    }
}
