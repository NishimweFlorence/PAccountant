namespace Domain.Entities
{
    public class LoanRepayment
    {
        public int Id { get; set; }

        public int LoanId { get; set; }
        public decimal Amount { get; set; }
        public DateTime RepaymentDate { get; set; }
        public Loan Loan { get; set; }
        public string Note { get; set; }

        //audit fields
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int CreatedById { get; set; }
        public int UpdatedById { get; set; }

    }
}