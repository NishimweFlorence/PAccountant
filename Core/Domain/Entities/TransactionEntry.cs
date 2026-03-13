using System;

namespace Domain.Entities
{
    public class TransactionEntry
    {
        public int Id { get; set; }
        public int TransactionId { get; set; }
        public int? AccountId { get; set; }
        public int? CategoryId { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }

        public virtual Transaction Transaction { get; set; } = null!;
        public virtual Account? Account { get; set; }
        public virtual TransactionCategory? Category { get; set; }
    }
}
