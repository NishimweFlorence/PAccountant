using System;
using System.Collections.Generic;

namespace Domain.ValueObjects
{
    public class TransactionType : IEquatable<TransactionType>
    {
        public string Name { get; private set; }
        public int Effect { get; private set; } // 1 for Income, -1 for Expense

        private TransactionType() { Name = string.Empty; Effect = 0; }

        public TransactionType(string name, int effect)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Transaction type name cannot be null or empty.", nameof(name));
            
            Name = name;
            Effect = effect;
        }

        public override bool Equals(object? obj)
        {
            if (obj is TransactionType other)
            {
                return Name == other.Name;
            }
            return false;
        }

        public override int GetHashCode() => Name.GetHashCode();

        public static TransactionType Income => new TransactionType("Income", 1);
        public static TransactionType Expense => new TransactionType("Expense", -1);

        public static TransactionType FromString(string type)
        {
            return type.ToLower() switch
            {
                "income" => Income,
                "expense" => Expense,
                _ => new TransactionType(type, 0) // Default or unknown
            };
        }

        public bool Equals(TransactionType? other) => 
            other != null && Name == other.Name;

        public override string ToString() => Name;

        public static bool operator ==(TransactionType? left, TransactionType? right) => 
            EqualityComparer<TransactionType>.Default.Equals(left, right);

        public static bool operator !=(TransactionType? left, TransactionType? right) => !(left == right);
    }
}
