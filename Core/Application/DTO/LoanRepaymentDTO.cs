namespace Application.DTO
{
    public class LoanRepaymentCreateDTO
    {
        public int Id { get; set; }
        public int LoanId { get; set; }
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
        public DateTime? RepaymentDate { get; set; }
        public string Note { get; set; } = string.Empty;
    }

    public class LoanRepaymentUpdateDTO
    {
        public int Id { get; set; }
        public int LoanId { get; set; }
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
        public DateTime? RepaymentDate { get; set; }
        public string Note { get; set; } = string.Empty;
    }

    public class LoanRepaymentDeleteDTO
    {
        public int Id { get; set; }
    }

}