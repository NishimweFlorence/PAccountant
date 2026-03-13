using System;

namespace Application.DTO
{
    public class BudgetDisplayDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public decimal SpentAmount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public decimal RemainingAmount => Amount - SpentAmount;
        public double PercentageUsed => Amount > 0 ? (double)(SpentAmount / Amount) * 100 : 0;
    }

    public class BudgetCreateDTO
    {
        public string Name { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? CategoryId { get; set; }
    }

    public class BudgetUpdateDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? CategoryId { get; set; }
    }
}
