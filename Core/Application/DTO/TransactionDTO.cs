using System;

namespace Application.DTO
{
    public class CreateTransactionDTO
    {
        public int IdTransactionCategory { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "Frw";
        public int AccountId { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class UpdateTransactionDTO
    {
        public int IdTransactionCategory { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "Frw";
        public int AccountId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
