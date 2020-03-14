using System;
using System.Collections.Generic;
using System.Linq;

namespace SampleRpg.Engine.Models
{
    //TODO: Wonder if this should be a relatively static data with a builder type instead?
    public class World
    {
        public World AddLocation ( int x, int y, string name, string description, string image )
        {            
            _locations.Add(new Location() {
                XCoordinate = x, 
                YCoordinate = y,
                Name = name,
                Description = description,
                ImageName = image
            });

            return this;
        }

        public Location GetLocationToNorth ( Location location ) => LocationAt(location.XCoordinate, location.YCoordinate + 1);
        public Location GetLocationToSouth ( Location location ) => LocationAt(location.XCoordinate, location.YCoordinate - 1);
        public Location GetLocationToEast ( Location location ) => LocationAt(location.XCoordinate + 1, location.YCoordinate);
        public Location GetLocationToWest ( Location location ) => LocationAt(location.XCoordinate - 1, location.YCoordinate);

        public Location LocationAt ( int x, int y ) => _locations.FirstOrDefault(loc => loc.XCoordinate == x && loc.YCoordinate == y);
                
        private readonly List<Location> _locations = new List<Location>();
    }
}
