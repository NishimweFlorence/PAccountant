using System;

namespace Domain.Entities
{
    public class CustomLookup
    {
        public int Id { get; set; }
        public required string Category { get; set; } // e.g., "AccountType", "AssetType", "ExpenseType"
        public required string Name { get; set; }
        
        // Audit and link
        public int UserId { get; set; }
        public User? User { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
