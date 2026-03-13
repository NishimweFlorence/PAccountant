using System;
using System.Collections.Generic;

namespace Domain.ValueObjects
{
    public class AssetType : IEquatable<AssetType>
    {
        public string Name { get; private set; }

        private AssetType() { Name = string.Empty; }

        public AssetType(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Asset type name cannot be null or empty.", nameof(name));
            
            Name = name;
        }

        public override bool Equals(object? obj) => obj is AssetType other && Name == other.Name;

        public override int GetHashCode() => Name.GetHashCode();

        public static AssetType Land => new AssetType("Land");
        public static AssetType Vehicle => new AssetType("Vehicle");
        public static AssetType Stock => new AssetType("Stock");
        public static AssetType Bond => new AssetType("Bond");
        public static AssetType Building => new AssetType("Building");
        public static AssetType Other => new AssetType("Other");

        public static AssetType FromString(string type)
        {
            if (string.IsNullOrWhiteSpace(type)) return Other;

            return type.ToLower() switch
            {
                "land" => Land,
                "vehicle" => Vehicle,
                "stock" => Stock,
                "bond" => Bond,
                "building" => Building,
                _ => new AssetType(type)
            };
        }

        public bool Equals(AssetType? other) => other != null && Name == other.Name;

        public override string ToString() => Name;

        public static bool operator ==(AssetType? left, AssetType? right) => EqualityComparer<AssetType>.Default.Equals(left, right);
        public static bool operator !=(AssetType? left, AssetType? right) => !(left == right);
    }
}
