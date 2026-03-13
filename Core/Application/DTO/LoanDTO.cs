namespace Application.DTO
{
    public class LoanCreateDTO
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string Borrower {get;set;} = string.Empty;
        public decimal Amount {get;set;}
        public decimal InterestRate {get;set;}
        public decimal AmountToPay {get;set;}
        public string Currency {get;set;} = string.Empty;
        public DateTime? LoanDate {get;set;}
        public string Description {get;set;} = string.Empty;
        public string Status {get;set;} = string.Empty;
    }

    public class LoanUpdateDTO
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string Borrower {get;set;} = string.Empty;
        public decimal Amount {get;set;}
        public decimal InterestRate {get;set;}
        public decimal AmountToPay {get;set;}
        public string Currency {get;set;} = string.Empty;
        public DateTime? LoanDate {get;set;}
        public string Description {get;set;} = string.Empty;
        public string Status {get;set;} = string.Empty;
    }

    public class LoanDeleteDTO
    {
        public int Id { get; set; }
    }
}
