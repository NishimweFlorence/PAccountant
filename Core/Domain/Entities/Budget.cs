using System;

namespace Domain.Entities
{
    public class Budget
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public decimal Amount { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation property (if Category exists in Domain.Entities)
        // public TransactionCategory Category { get; set; }
    }
}
