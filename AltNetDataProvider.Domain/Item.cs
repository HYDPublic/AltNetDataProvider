using System;

namespace AltNetDataProvider.Domain
{
    public class Item
    {
        public Item(string itemId, 
            string description, 
            decimal weight, 
            decimal volume, 
            Money unitPrice)
        {
            if (weight <=0)
                throw new ArgumentException("Weight must be positive", nameof(weight));
            if (volume <=0)
                throw new ArgumentException("Volume must be positive", nameof(volume));
            if (string.IsNullOrWhiteSpace(itemId))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(itemId));
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(description));

            ItemId = itemId;
            Description = description;
            Weight = weight;
            Volume = volume;
            UnitPrice = unitPrice ?? throw new ArgumentNullException(nameof(unitPrice));
        }

        public string ItemId { get; }
        public string Description { get; }
        public decimal Weight { get; }
        public decimal Volume { get; }
        public Money UnitPrice { get; }
    }
}