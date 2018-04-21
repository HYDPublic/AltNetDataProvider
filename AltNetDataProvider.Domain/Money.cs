using System;

namespace AltNetDataProvider.Domain
{
    public class Money
    {
        public Money(string currency, decimal amount)
        {
            if (string.IsNullOrWhiteSpace(currency))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(currency));
            Currency = currency;
            Amount = amount;
        }

        public string Currency { get; }
        public decimal Amount { get;}
    }
}