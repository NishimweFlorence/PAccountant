using System;
using System.Collections.Generic;

namespace Domain.ValueObjects
{
    public class ValueChangeType : IEquatable<ValueChangeType>
    {
        public string Name { get; private set; }

        private ValueChangeType() { Name = string.Empty; }

        public ValueChangeType(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Value change type name cannot be null or empty.", nameof(name));
            
            Name = name;
        }

        public override bool Equals(object? obj) => obj is ValueChangeType other && Name == other.Name;

        public override int GetHashCode() => Name.GetHashCode();

        public static ValueChangeType Appreciate => new ValueChangeType("Appreciate");
        public static ValueChangeType Depreciate => new ValueChangeType("Depreciate");
        public static ValueChangeType None => new ValueChangeType("None");

        public static ValueChangeType FromString(string type)
        {
            if (string.IsNullOrWhiteSpace(type)) return None;

            return type.ToLower() switch
            {
                "appreciate" => Appreciate,
                "depreciate" => Depreciate,
                _ => None
            };
        }

        public bool Equals(ValueChangeType? other) => other != null && Name == other.Name;

        public override string ToString() => Name;

        public static bool operator ==(ValueChangeType? left, ValueChangeType? right) => EqualityComparer<ValueChangeType>.Default.Equals(left, right);
        public static bool operator !=(ValueChangeType? left, ValueChangeType? right) => !(left == right);
    }
}
