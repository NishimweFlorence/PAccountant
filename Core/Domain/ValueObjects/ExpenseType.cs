using System;
using System.Collections.Generic;

namespace Domain.ValueObjects
{
    public class ExpenseType : IEquatable<ExpenseType>
    {
        public string Name { get; private set; }

        private ExpenseType() { Name = string.Empty; }

        public ExpenseType(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Expense type name cannot be null or empty.", nameof(name));
            
            Name = name;
        }

        public override bool Equals(object? obj) => obj is ExpenseType other && Equals(other);

        public override int GetHashCode() => Name.GetHashCode();

        public static ExpenseType FoodAndDrinks => new ExpenseType("Food & Drinks");
        public static ExpenseType Transport => new ExpenseType("Transport");
        public static ExpenseType GiftGiven => new ExpenseType("Gift Given");
        public static ExpenseType Other => new ExpenseType("Other");

        public static ExpenseType FromString(string type)
        {
            return type.ToLower() switch
            {
                "food & drinks" or "food" or "drinks" => FoodAndDrinks,
                "transport" => Transport,
                "gift given" or "gift" => GiftGiven,
                _ => new ExpenseType(type)
            };
        }

        public bool Equals(ExpenseType? other) => 
            other != null && Name.Equals(other.Name, StringComparison.OrdinalIgnoreCase);

        public override string ToString() => Name;

        public static bool operator ==(ExpenseType? left, ExpenseType? right) => 
            EqualityComparer<ExpenseType>.Default.Equals(left, right);

        public static bool operator !=(ExpenseType? left, ExpenseType? right) => !(left == right);
    }
}
