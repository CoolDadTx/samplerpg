using System;

using SampleRpg.Engine.Factories;

namespace SampleRpg.Engine.Models
{
    public class ItemQuantity
    {
        public int ItemId { get; set; }
        public int Quantity { get; set; }
                
        public override string ToString () => $"({Quantity}) {ItemFactory.GetItemName(ItemId)}";
    }
}
