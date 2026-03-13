using Domain.ValueObjects;

namespace Domain.Entities
{
    public class TransactionCategory
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public TransactionType? Type { get; set; }
       
        public virtual ICollection<TransactionEntry> TransactionEntries { get; set; } = new List<TransactionEntry>();

        /// Audit fields
        
        // public DateTime CreatedAt { get; set; } = DateTime.Now;
        // public DateTime UpdatedAt { get; set; } = DateTime.Now;
        // public int? CreatedById { get; set; }
        // public int? UpdatedById { get; set; }
    }
}
