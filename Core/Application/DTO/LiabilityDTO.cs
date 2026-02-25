namespace Application.DTO
{
    public class CreateLiabilityDTO
    {
     
        public string Type { get; set; }
        public string LenderName { get; set; }
        public decimal OriginalAmount { get; set; }
        public decimal CurrentAmount { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime CreatedAt { get; set; }
         public string Currency { get; set; } = "Frw";
    }
    public class UpdateLiabilityDTO
    {
         public string Type { get; set; }
        public decimal OriginalAmount { get; set; }
        public decimal CurrentAmount { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Currency { get; set; } = "Frw";
    }
    }
