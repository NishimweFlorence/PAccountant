using System;
using System.Collections.Generic;

namespace Domain.ValueObjects
{
    public class Currency : IEquatable<Currency>
    {
        public string Code { get; private set; } // e.g., RWF, USD, EUR
        public string Symbol { get; private set; } // e.g., Frw, $, €

        private Currency() { InitializedToEmpty(); }

        private void InitializedToEmpty()
        {
            Code = string.Empty;
            Symbol = string.Empty;
        }

        public Currency(string code, string symbol)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentException("Currency code cannot be null or empty.", nameof(code));
            
            if (string.IsNullOrWhiteSpace(symbol))
                throw new ArgumentException("Currency symbol cannot be null or empty.", nameof(symbol));

            Code = code.ToUpperInvariant();
            Symbol = symbol;
        }

        public override bool Equals(object? obj)
        {
            if (obj is Currency other)
            {
                return Code == other.Code;
            }
            return false;
        }

        public override int GetHashCode() => Code.GetHashCode();

        public static Currency RWF => new Currency("RWF", "Frw");
        public static Currency USD => new Currency("USD", "$");
        public static Currency EUR => new Currency("EUR", "€");

        public static Currency FromCode(string code)
        {
            return code.ToUpper() switch
            {
                "USD" => USD,
                "EUR" => EUR,
                "RWF" => RWF,
                _ => new Currency(code.ToUpper(), string.Empty) // Dynamic creation if not predefined
            };
        }

        public bool Equals(Currency? other) => 
            other != null && Code == other.Code; // Only compare Code for IEquatable

        public override string ToString() => $"{Symbol} ({Code})";

        public static bool operator ==(Currency? left, Currency? right) => 
            EqualityComparer<Currency>.Default.Equals(left, right);

        public static bool operator !=(Currency? left, Currency? right) => !(left == right);
    }
}
