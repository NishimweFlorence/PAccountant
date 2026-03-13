using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Account
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Type { get; set; }
        public float? AccountNumber{ get; set;}
        public decimal? Balance { get; set;}
        public Currency? Currency { get; set;}
        public string? Status { get; set;}
        public DateTime CreatedAt { get; set; }

        public virtual ICollection<Loan> Loans { get; set; } = new List<Loan>();
        public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

        /// Audit fields
        
        // public DateTime CreatedAt { get; set; } = DateTime.Now;
        // public DateTime UpdatedAt { get; set; } = DateTime.Now;
        // public int? CreatedById { get; set; }
        // public int? UpdatedById { get; set; }
    }
}
