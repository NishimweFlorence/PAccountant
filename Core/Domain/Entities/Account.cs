namespace Domain.Entities
{
    public class Account
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Type { get; set; }
        public decimal? balance { get; set;}
        public string? Currency { get; set;}
        public string? Status { get; set;}
        public DateTime CreatedAt { get; set; }

        public virtual ICollection<Loan> Loans { get; set; } = new List<Loan>();

        /// Audit fields
        
        // public DateTime CreatedAt { get; set; } = DateTime.Now;
        // public DateTime UpdatedAt { get; set; } = DateTime.Now;
        // public int? CreatedById { get; set; }
        // public int? UpdatedById { get; set; }
    }
}
