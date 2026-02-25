namespace Domain.Entities
{
    public class Liability 
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string LenderName { get; set; }
        public decimal OriginalAmount { get; set; }
        public decimal CurrentAmount { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Currency { get; set; } = "Frw";
    }
}