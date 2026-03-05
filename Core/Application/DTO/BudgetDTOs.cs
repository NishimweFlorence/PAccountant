using System;

namespace Application.DTO
{
    public class BudgetDisplayDTO
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public decimal Amount { get; set; }
        public decimal SpentAmount { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public decimal RemainingAmount => Amount - SpentAmount;
        public double PercentageUsed => Amount > 0 ? (double)(SpentAmount / Amount) * 100 : 0;
    }

    public class BudgetCreateDTO
    {
        public int CategoryId { get; set; }
        public decimal Amount { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }

    public class BudgetUpdateDTO
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }
}
