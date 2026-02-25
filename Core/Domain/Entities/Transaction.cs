namespace Domain.Entities
{
    public class Transaction
    {
        public int Id { get; set; }
        public int IdTransactionCategory { get; set; }       
        public DateTime TransactionDate { get; set; }
         public decimal Amount { get; set; }
          public string Currency { get; set; } 
        public int AccountId { get; set; }
         public DateTime CreatedAt { get; set; }


    }
}