using System;

namespace Application.DTO
{
    public class IncomeCreateDTO
    {
        public decimal Amount { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string Description { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty; // Value object name
        public int? AccountId { get; set; }
        public int? AssetId { get; set; }
        public string Currency { get; set; } = "RWF";
    }

    public class IncomeReadDTO
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public int? AccountId { get; set; }
        public string? AccountName { get; set; }
        public int? AssetId { get; set; }
        public string? AssetName { get; set; }
        public string Currency { get; set; } = string.Empty;
        public int? TransactionId { get; set; }
    }
}
