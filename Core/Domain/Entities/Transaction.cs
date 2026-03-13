using Domain.ValueObjects;
using System.Collections.Generic;
using System;

namespace Domain.Entities
{
    public class Transaction
    {
        public int Id { get; set; }
        public DateTime TransactionDate { get; set; }
        public string? Description { get; set; }
        public string? ReferenceNumber { get; set; }
        public int? BudgetId { get; set; }
        public Currency Currency { get; set; } = null!; 
        public DateTime CreatedAt { get; set; }

        public virtual Budget? Budget { get; set; }
        public virtual ICollection<TransactionEntry> Entries { get; set; } = new List<TransactionEntry>();
    }
}