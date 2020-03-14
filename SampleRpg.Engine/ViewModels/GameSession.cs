using System;
using System.ComponentModel;
using SampleRpg.Engine.Factories;
using SampleRpg.Engine.Models;

//TODO: Should ViewModels be in the UI because VMs are generally tied to the UI being rendered
namespace SampleRpg.Engine.ViewModels
{    
    public class GameSession : INotifyPropertyChanged
    {
        public GameSession ()
        {
            CurrentLocation = CurrentWorld.LocationAt(0, 0);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        //TODO: Temporary init
        public Player CurrentPlayer { get; set; } = new Player() { Name = "Test", CharacterClass = "Fighter", HitPoints = 10, Gold = 1000 };

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

        public World CurrentWorld { get; set; } = new WorldFactory().CreateWorld();
                
        public bool CanMoveNorth => CurrentWorld.GetLocationToNorth(CurrentLocation) != null;
        public bool CanMoveSouth => CurrentWorld.GetLocationToSouth(CurrentLocation) != null;
        public bool CanMoveEast => CurrentWorld.GetLocationToEast(CurrentLocation) != null;
        public bool CanMoveWest => CurrentWorld.GetLocationToWest(CurrentLocation) != null;

        //TODO: Should this be elsewhere?
        public void MoveNorth () => CurrentLocation = CurrentWorld.GetLocationToNorth(CurrentLocation);
        public void MoveSouth () => CurrentLocation = CurrentWorld.GetLocationToSouth(CurrentLocation);
        public void MoveEast () => CurrentLocation = CurrentWorld.GetLocationToEast(CurrentLocation);
        public void MoveWest () => CurrentLocation = CurrentWorld.GetLocationToWest(CurrentLocation);

        protected virtual void OnPropertyChanged ( string name ) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        private Location _location;
    }
}
