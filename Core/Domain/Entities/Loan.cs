using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Loan
    {
        public int Id { get; set; }
        public virtual Account Account { get; set; } = null!;
        public int AccountId { get; set; }
        public string Borrower {get;set;} = null!;
        public decimal Amount {get;set;}
        public decimal InterestRate {get;set;}
        public decimal AmountToPay {get;set;}
        public Currency Currency {get;set;} = null!;
        public DateTime LoanDate {get;set;}
        public string Description {get;set;} = null!;
        public string Status {get;set;} = null!;

        //audit fields
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int CreatedById { get; set; }
        public int UpdatedById { get; set; }
    }
}