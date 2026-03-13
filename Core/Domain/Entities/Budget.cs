using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Budget
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public int? TransactionCategoryId { get; set; }
        public virtual TransactionCategory? TransactionCategory { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
