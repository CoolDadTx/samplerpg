using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SampleRpg.Engine.Models
{
    public class Trader : NotifyPropertyChangedObject
    {
        public string Name { get; set; }

        //TODO: Make inventory class - needed by player anyway
        public ObservableCollection<GameItem> Inventory { get; } = new ObservableCollection<GameItem>();

        public void AddToInventory ( GameItem item ) => Inventory.Add(item);
        public void RemoveFromInventory ( GameItem item ) => Inventory.Remove(item);
    }
}
