using System;

using SampleRpg.Engine.Factories;
using SampleRpg.Engine.Models;

//TODO: Should ViewModels be in the UI because VMs are generally tied to the UI being rendered
namespace SampleRpg.Engine.ViewModels
{    
    public class GameSession : NotifyPropertyChangedObject
    {
        public GameSession ()
        {
            CurrentLocation = CurrentWorld.LocationAt(0, 0);

            CurrentPlayer = new Player() { Name = "Test", CharacterClass = "Fighter", HitPoints = 10, Gold = 1000 };
            CurrentPlayer.Inventory.Add(ItemFactory.CreateGameItem(1001));
            CurrentPlayer.Inventory.Add(ItemFactory.CreateGameItem(1002));
        }

        //TODO: Temporary init
        public Player CurrentPlayer { get; set; }

        //TODO: Needed?
        public Location CurrentLocation 
        {
            get => _location; 
            set 
            {
                System.Diagnostics.Debug.WriteLine($"Location set to ({value.XCoordinate}, {value.YCoordinate}) '{value.Name}'");
                if (_location != value)
                {                    
                    _location = value;
                    OnPropertyChanged(nameof(CurrentLocation));
                    OnPropertyChanged(nameof(CanMoveNorth));
                    OnPropertyChanged(nameof(CanMoveSouth));
                    OnPropertyChanged(nameof(CanMoveEast));
                    OnPropertyChanged(nameof(CanMoveWest));
                };
            }
        }

        public World CurrentWorld { get; set; } = WorldFactory.CreateWorld();
                
        public bool CanMoveNorth => CurrentWorld.GetLocationToNorth(CurrentLocation) != null;
        public bool CanMoveSouth => CurrentWorld.GetLocationToSouth(CurrentLocation) != null;
        public bool CanMoveEast => CurrentWorld.GetLocationToEast(CurrentLocation) != null;
        public bool CanMoveWest => CurrentWorld.GetLocationToWest(CurrentLocation) != null;

        //TODO: Should this be elsewhere?
        public void MoveNorth ()
        {
            var location = CurrentWorld.GetLocationToNorth(CurrentLocation);
            if (location != null)
                CurrentLocation = location;
        }
        public void MoveSouth ()
        {
            var location = CurrentWorld.GetLocationToSouth(CurrentLocation);
            if (location != null)
                CurrentLocation = location;
        }
        public void MoveEast ()
        {            
            var location = CurrentWorld.GetLocationToEast(CurrentLocation);
            if (location != null)
                CurrentLocation = location;
        }
        public void MoveWest ()
        {
            var location = CurrentWorld.GetLocationToWest(CurrentLocation);
            if (location != null)
                CurrentLocation = location;
        }

        private Location _location;
    }
}
