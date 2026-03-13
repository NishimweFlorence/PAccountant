using System;
using System.Collections.Generic;

namespace Application.DTO
{
    public class TransactionEntryDTO
    {
        public int? AccountId { get; set; }
        public int? CategoryId { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
    }

    public class CreateTransactionDTO
    {
        public DateTime TransactionDate { get; set; }
        public string? Description { get; set; }
        public string? ReferenceNumber { get; set; }
        public int? BudgetId { get; set; }
        public string Currency { get; set; } = "Frw";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public List<TransactionEntryDTO> Entries { get; set; } = new List<TransactionEntryDTO>();
    }

    public class UpdateTransactionDTO
    {
        public DateTime TransactionDate { get; set; }
        public string? Description { get; set; }
        public string? ReferenceNumber { get; set; }
        public int? BudgetId { get; set; }
        public string Currency { get; set; } = "Frw";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public List<TransactionEntryDTO> Entries { get; set; } = new List<TransactionEntryDTO>();
    }
}
