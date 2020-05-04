﻿using System;
using System.Linq;

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
            CurrentPlayer.AddToInventory(ItemFactory.NewItem(1001));
            CurrentPlayer.AddToInventory(ItemFactory.NewItem(2001));
        }

        public event EventHandler<GameMessageEventArgs> MessageRaised;

        public Player CurrentPlayer
        {
            get => _player;
            set 
            {
                if (_player != value)
                {
                    HandleEvents(_player, false);
                    
                    _player = value;

                    HandleEvents(_player, true);

                    OnPropertyChanged(nameof(CurrentPlayer));
                };
            }
        }

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

        public Monster CurrentMonster
        {
            get => _monster;
            set 
            {
                if (_monster != value)
                {
                    HandleEvents(_monster, false);

                    _monster = value;

                    if (_monster != null)
                    {
                        HandleEvents(_monster, true);
                        OnMessageRaised($"You see a {CurrentMonster.Name} here!");
                    };

                    OnPropertyChanged(nameof(CurrentMonster));
                    OnPropertyChanged(nameof(CanAttack));
                };
            }
        }
        public bool CanAttack => CurrentMonster != null && CurrentPlayer.CurrentWeapon != null;

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

        public void UseSlot1 () => CurrentPlayer?.UseSlot1(CurrentPlayer);
        
        //TODO: Should this be elsewhere?
        public void Attack ()
        {
            //TODO: Implement a combat round loop - initiative, attacks, apply effects, etc
            if (CurrentPlayer.CurrentWeapon == null)
            {
                OnMessageRaised("You must have a weapon selected");
                return;
            };

            var target = CurrentMonster;

            CurrentPlayer.UseCurrentWeapon(target);

            if (target.IsDead)
            {              
                //TODO: Why? - clear encounter otherwise can keep attacking
                //CurrentLocation.GetEncounter();
                CurrentMonster = null;
            } else
            {
                CurrentMonster.UseCurrentWeapon(CurrentPlayer);              
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
                    CurrentPlayer.Quests.Add(new QuestStatus(quest));
            };
        }

        //TODO: Make this a behavior of Location
        private void CheckForEncounters ()
        {                     
            CurrentMonster = CurrentLocation.GetEncounter();
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
                CurrentPlayer.AddXP(quest.RewardXp);
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
                        var newItem = ItemFactory.NewItem(item.ItemId);
                        itemName = newItem.Name;

                        CurrentPlayer.AddToInventory(newItem);                        
                    };
                    OnMessageRaised($"You received {item.Quantity} {itemName}");
                };

                //Done
                status.IsCompleted = true;
            };
        }

        #region Event Handlers

        private void HandleEvents ( Player player, bool subscribe )
        {
            if (player == null)
                return;

            if (subscribe)
            {
                player.WeaponEquipped += OnPlayerWeaponEquipped;
                player.Died += OnPlayerDied;
                player.LeveledUp += OnPlayerLevelUp;
                player.ActionPerformed += OnPlayerActionPerformed;
            } else
            {
                player.WeaponEquipped -= OnPlayerWeaponEquipped;
                player.Died -= OnPlayerDied;
                player.LeveledUp -= OnPlayerLevelUp;
                player.ActionPerformed -= OnPlayerActionPerformed;
            };
        }

        private void HandleEvents ( Monster monster, bool subscribe )
        {
            if (monster == null)
                return;

            if (subscribe)
            {
                monster.Died += OnMonsterDied;
                monster.ActionPerformed += OnMonsterActionPerformed;
            } else
            {
                monster.Died -= OnMonsterDied;
                monster.ActionPerformed -= OnMonsterActionPerformed;
            };
        }

        private void OnMonsterActionPerformed ( object sender, string message ) => OnMessageRaised(message);

        private void OnMonsterDied ( object sender, EventArgs e )
        {
            var monster = CurrentMonster;

            OnMessageRaised($"You killed {monster.Name}");

            OnMessageRaised($"You receive {monster.RewardXP} experience");
            CurrentPlayer.AddXP(monster.RewardXP);

            OnMessageRaised($"You receive {monster.Gold} gold");
            CurrentPlayer.AddGold(monster.Gold);
            
            foreach (var item in monster.Inventory)
            {
                CurrentPlayer.AddToInventory(item.Item, item.Quantity);
                OnMessageRaised($"You receive {item.Quantity} {item.Item.Name}");
            };
        }

        private void OnPlayerActionPerformed ( object sender, string message ) => OnMessageRaised(message);

        private void OnPlayerWeaponEquipped ( object sender, EventArgs e)
        {
            OnPropertyChanged(nameof(CanAttack));
        }

        private void OnPlayerDied ( object sender, EventArgs e )
        {
            OnMessageRaised("You are dead");

            //TODO: Restart
            CurrentLocation = CurrentWorld.LocationAt(0, -1);
            CurrentPlayer.HealAll();
        }

        private void OnPlayerLevelUp ( object sender, EventArgs e )
        {
            OnMessageRaised($"You have gained a new level (Level {CurrentPlayer.Level})");
        }
        #endregion

        private Location _location;
        private Monster _monster;
        private Trader _trader;
        private Player _player;
    }
}
