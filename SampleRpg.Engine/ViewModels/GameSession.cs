using System;
using System.Linq;
using System.Runtime.CompilerServices;
using SampleRpg.Engine.Eventing;
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
            
            CurrentPlayer = new Player("Test", "Fighter", 10, gold: 1000);            
            CurrentPlayer.AddToInventory(ItemFactory.CreateGameItem(1001));                        
        }

        public event EventHandler<GameMessageEventArgs> MessageRaised;

        public Player CurrentPlayer
        {
            get => _player;
            set 
            {
                if (_player != value)
                {
                    if (_player != null)
                        _player.Died -= OnPlayerDied;

                    _player = value;

                    if (_player != null)
                        _player.Died += OnPlayerDied;

                    OnPropertyChanged(nameof(CurrentPlayer));
                };
            }
        }

        //TODO: Should be attribute of character
        public Weapon CurrentWeapon { get; set; }

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

                    CompleteQuestsAtLocation();
                    CheckForQuests();
                    CheckForEncounters();

                    CurrentTrader = _location.TraderHere;
                };
            }
        }

        public Monster CurrentEncounter
        {
            get => _monster;
            set 
            {
                if (_monster != value)
                {
                    if (_monster != null)
                        _monster.Died -= OnMonsterDied;

                    _monster = value;

                    if (_monster != null)
                    {
                        _monster.Died += OnMonsterDied;
                        OnMessageRaised($"You see a {CurrentEncounter.Name} here!");
                    };

                    OnPropertyChanged(nameof(CurrentEncounter));
                    OnPropertyChanged(nameof(HasEncounter));
                };
            }
        }
        public bool HasEncounter => CurrentEncounter != null;

        public World CurrentWorld { get; set; } = WorldFactory.CreateWorld();
        public Trader CurrentTrader 
        {
            get => _trader;
            set {
                if (_trader != value)
                {
                    _trader = value;
                    OnPropertyChanged(nameof(CurrentTrader));
                    OnPropertyChanged(nameof(HasTrader));
                };
            }
        }
        public bool HasTrader => CurrentTrader != null;
                
        public bool CanMoveNorth => CurrentWorld.GetLocationToNorth(CurrentLocation) != null;
        public bool CanMoveSouth => CurrentWorld.GetLocationToSouth(CurrentLocation) != null;
        public bool CanMoveEast => CurrentWorld.GetLocationToEast(CurrentLocation) != null;
        public bool CanMoveWest => CurrentWorld.GetLocationToWest(CurrentLocation) != null;

        //TODO: Should this be elsewhere?
        public void Attack ()
        {
            //TODO: Implement a combat round loop - initiative, attacks, apply effects, etc
            if (CurrentWeapon == null)
            {
                OnMessageRaised("You must have a weapon selected");
                return;
            };

            var target = CurrentEncounter;

            var dmg = Rng.Between(CurrentWeapon.MinimumDamage, CurrentWeapon.MaximumDamage);
            if (dmg > 0)
            {
                OnMessageRaised($"You hit {target.Name} for {dmg} damage");
                target.TakeDamage(dmg);                
            } else
            {
                OnMessageRaised("You missed");                
            };

            if (target.IsDead)
            {              
                //TODO: Why? - clear encounter otherwise can keep attacking
                //CurrentLocation.GetEncounter();
                CurrentEncounter = null;
            } else
            {
                AttackPlayer(target);                
            };
        }

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

        protected void OnMessageRaised ( string message ) => MessageRaised?.Invoke(this, new GameMessageEventArgs(message));

        //TODO: Make this a behavior of Location
        private void CheckForQuests ()
        {
            var quests = CurrentLocation.GetQuests();
            foreach (var quest in quests)
            {
                if (!CurrentPlayer.Quests.Any(i => i.Quest.Id == quest.Id))
                    CurrentPlayer.Quests.Add(new QuestStatus() { Quest = quest });
            };
        }

        //TODO: Make this a behavior of Location
        private void CheckForEncounters ()
        {                     
            CurrentEncounter = CurrentLocation.GetEncounter();
        }

        private void AttackPlayer ( Monster monster )
        {
            //TODO: How can monster miss based solely on damage?
            var dmg = Rng.Between(monster.MinimumDamage, monster.MaximumDamage);
            if (dmg <= 0)
            {
                OnMessageRaised("Monster misses");
                return;
            } else
            {
                OnMessageRaised($"You were hit for {dmg} points of damage");
                CurrentPlayer.TakeDamage(dmg);                
            };
        }

        //TODO: Doesn't belong here
        private void CompleteQuestsAtLocation ()
        {
            var quests = CurrentLocation.GetQuests();
            foreach (var quest in quests)
            {
                var status = CurrentPlayer.Quests.FirstOrDefault(q => q.Quest.Id == quest.Id && !q.IsCompleted);
                if (status == null)
                    continue;

                if (!CurrentPlayer.HasAllItems(quest.ItemsToComplete))
                    continue;

                //Remove items
                foreach (var item in quest.ItemsToComplete)
                {
                    CurrentPlayer.RemoveFromInventory(item.ItemId, item.Quantity);
                };

                //Reward                
                CurrentPlayer.ExperiencePoints += quest.RewardXp;
                OnMessageRaised($"You completed '{quest.Name} and received {quest.RewardXp} XP");
                if (quest.RewardGold > 0)
                {
                    CurrentPlayer.AddGold(quest.RewardGold);
                    OnMessageRaised($"You received {quest.RewardGold} gold");
                };
                
                foreach (var item in quest.RewardItems)                
                {
                    var itemName = "";
                        
                    //HACK:
                    var num = item.Quantity;
                    while (num-- >= 0)
                    {
                        var newItem = ItemFactory.CreateGameItem(item.ItemId);
                        itemName = newItem.Name;

                        CurrentPlayer.AddToInventory(newItem);                        
                    };
                    OnMessageRaised($"You received {item.Quantity} {itemName}");
                };

                //Done
                status.IsCompleted = true;
            };
        }

        private void OnMonsterDied ( object sender, EventArgs e )
        {
            var monster = CurrentEncounter;

            OnMessageRaised($"You killed {monster.Name}");

            CurrentPlayer.ExperiencePoints += monster.RewardXP;
            OnMessageRaised($"You receive {monster.RewardXP} experience");

            CurrentPlayer.AddGold(monster.Gold);
            OnMessageRaised($"You receive {monster.Gold} gold");

            foreach (var item in monster.Inventory)
            {
                CurrentPlayer.AddToInventory(item.Item, item.Quantity);
                OnMessageRaised($"You receive {item.Quantity} {item.Item.Name}");
            };
        }

        private void OnPlayerDied ( object sender, EventArgs e )
        {
            OnMessageRaised("You are dead");

            //TODO: Restart
            CurrentLocation = CurrentWorld.LocationAt(0, -1);
            CurrentPlayer.HealAll();
        }

        private Location _location;
        private Monster _monster;
        private Trader _trader;
        private Player _player;
    }
}
