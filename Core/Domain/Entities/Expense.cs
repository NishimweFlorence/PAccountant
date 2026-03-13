using Domain.ValueObjects;
using System;

namespace Domain.Entities
{
    public class Expense
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; } = string.Empty;
        public ExpenseType Type { get; set; } = null!;
        
        public int? AccountId { get; set; }
        public virtual Account? Account { get; set; }
        
        public int? AssetId { get; set; }
        public virtual Asset? Asset { get; set; }
        
        public int? LiabilityId { get; set; }
        public virtual Liability? Liability { get; set; }
        
        public Currency Currency { get; set; } = null!;
        
        public int? TransactionId { get; set; }
        public virtual Transaction? Transaction { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
