namespace Application.DTO
{
    public class LoanRepaymentCreateDTO
    {
        public int Id { get; set; }
        public int LoanId { get; set; }
        public decimal Amount { get; set; }
        public DateTime? RepaymentDate { get; set; }
        public string Note { get; set; }
    }

    public class LoanRepaymentUpdateDTO
    {
        public int Id { get; set; }
        public int LoanId { get; set; }
        public decimal Amount { get; set; }
        public DateTime? RepaymentDate { get; set; }
        public string Note { get; set; }
    }

    public class LoanRepaymentDeleteDTO
    {
        public int Id { get; set; }
    }

}