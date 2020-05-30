using System;

using SampleRpg.Engine.Models;

namespace SampleRpg.Engine.IO
{
    public class ItemQuantityModel
    {
        public int Id { get; set; }

        public int Quantity { get; set; }

        public ItemQuantity ToItemQuantity () => new ItemQuantity() { ItemId = Id, Quantity = Quantity };
    }
}
