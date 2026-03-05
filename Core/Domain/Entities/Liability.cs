using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Liability 
    {
        public int Id { get; set; }
        public string Type { get; set; } = null!;
        public string LenderName { get; set; } = null!;
        public decimal OriginalAmount { get; set; }
        public decimal CurrentAmount { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public Currency Currency { get; set; } = Currency.RWF;
    }
}