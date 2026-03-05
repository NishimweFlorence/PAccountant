using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Transaction
    {
        public int Id { get; set; }
        public int IdTransactionCategory { get; set; }       
        public DateTime TransactionDate { get; set; }
        public decimal Amount { get; set; }
        public Currency Currency { get; set; } = null!; 
        public int AccountId { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual TransactionCategory Category { get; set; } = null!;
        public virtual Account Account { get; set; } = null!;
    }
}