namespace Domain.Entities
{
    public class TransactionCategory
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Type { get; set; }
       



        /// Audit fields
        
        // public DateTime CreatedAt { get; set; } = DateTime.Now;
        // public DateTime UpdatedAt { get; set; } = DateTime.Now;
        // public int? CreatedById { get; set; }
        // public int? UpdatedById { get; set; }
    }
}
