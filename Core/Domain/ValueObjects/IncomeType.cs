using System;
using System.Collections.Generic;

namespace Domain.ValueObjects
{
    public class IncomeType : IEquatable<IncomeType>
    {
        public string Name { get; private set; }

        private IncomeType() { Name = string.Empty; }

        public IncomeType(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Income type name cannot be null or empty.", nameof(name));
            
            Name = name;
        }

        public override bool Equals(object? obj) => obj is IncomeType other && Equals(other);

        public override int GetHashCode() => Name.GetHashCode();

        public static IncomeType Salary => new IncomeType("Salary");
        public static IncomeType ProfitOnBond => new IncomeType("Profit on Bond");
        public static IncomeType Gift => new IncomeType("Gift");
        public static IncomeType Other => new IncomeType("Other");

        public static IncomeType FromString(string type)
        {
            return type.ToLower() switch
            {
                "salary" => Salary,
                "profit on bond" or "profitonbond" => ProfitOnBond,
                "gift" => Gift,
                _ => new IncomeType(type)
            };
        }

        public bool Equals(IncomeType? other) => 
            other != null && Name.Equals(other.Name, StringComparison.OrdinalIgnoreCase);

        public override string ToString() => Name;

        public static bool operator ==(IncomeType? left, IncomeType? right) => 
            EqualityComparer<IncomeType>.Default.Equals(left, right);

        public static bool operator !=(IncomeType? left, IncomeType? right) => !(left == right);
    }
}
