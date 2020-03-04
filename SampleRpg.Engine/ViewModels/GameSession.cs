using System;

using SampleRpg.Engine.Models;

//TODO: Should ViewModels be in the UI because VMs are generally tied to the UI being rendered
namespace SampleRpg.Engine.ViewModels
{    
    public class GameSession
    {
        //TODO: Temporary init
        public Player CurrentPlayer { get; set; } = new Player() { Name = "Test", CharacterClass = "Fighter", HitPoints = 10, Gold = 1000 };
    }
}
