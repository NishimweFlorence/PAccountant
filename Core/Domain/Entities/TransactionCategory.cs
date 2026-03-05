using Domain.ValueObjects;

namespace Domain.Entities
{
    public class TransactionCategory
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public TransactionType? Type { get; set; }
       
        public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
        public virtual ICollection<Budget> Budgets { get; set; } = new List<Budget>();

        /// Audit fields
        
        // public DateTime CreatedAt { get; set; } = DateTime.Now;
        // public DateTime UpdatedAt { get; set; } = DateTime.Now;
        // public int? CreatedById { get; set; }
        // public int? UpdatedById { get; set; }
    }
}
