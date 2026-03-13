using System;
using System.Collections.Generic;

namespace Domain.ValueObjects
{
    public class AccountType : IEquatable<AccountType>
    {
        public string Name { get; private set; }

        private AccountType() { Name = string.Empty; }

        public AccountType(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Account type name cannot be null or empty.", nameof(name));
            
            Name = name;
        }

        public override bool Equals(object? obj)
        {
            if (obj is AccountType other)
            {
                return Name == other.Name;
            }
            return false;
        }

        public override int GetHashCode() => Name.GetHashCode();

        public static AccountType BankAccount => new AccountType("Bank Account");
        public static AccountType MobileMoney => new AccountType("Mobile Money");
        public static AccountType CashinHand => new AccountType("Cash in Hand");


        public static AccountType FromString(string type)
        {
            return type switch
            {
                "Bank Account" => BankAccount,
                "Mobile Money" => MobileMoney,
                "Cash in Hand" => CashinHand,
                _ => new AccountType(type)
            };
        }

        public static List<AccountType> GetAll() => new()
        {
            BankAccount, MobileMoney, CashinHand
        };

        public bool Equals(AccountType? other) => 
            other != null && Name == other.Name;

        public override string ToString() => Name;

        public static bool operator ==(AccountType? left, AccountType? right) => 
            EqualityComparer<AccountType>.Default.Equals(left, right);

        public static bool operator !=(AccountType? left, AccountType? right) => !(left == right);
    }
}
