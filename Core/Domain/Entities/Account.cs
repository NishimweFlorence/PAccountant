using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Account
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? AccountNumber { get; set; }
        public AccountType? Type { get; set; }
        public decimal? Balance { get; set;}
        public Currency? Currency { get; set;}
        public string? Status { get; set;}
        public DateTime CreatedAt { get; set; }

        public virtual ICollection<Loan> Loans { get; set; } = new List<Loan>();
        public virtual ICollection<TransactionEntry> TransactionEntries { get; set; } = new List<TransactionEntry>();

        /// Audit fields
        
        // public DateTime CreatedAt { get; set; } = DateTime.Now;
        // public DateTime UpdatedAt { get; set; } = DateTime.Now;
        // public int? CreatedById { get; set; }
        // public int? UpdatedById { get; set; }
    }
}
