using System;
using System.Collections.Generic;

namespace Application.DTO
{
    public class IncomeStatementDTO
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal TotalExpenses { get; set; }
        public decimal NetIncome => TotalRevenue - TotalExpenses;
        public List<IncomeStatementLineDTO> RevenueLines { get; set; } = new List<IncomeStatementLineDTO>();
        public List<IncomeStatementLineDTO> ExpenseLines { get; set; } = new List<IncomeStatementLineDTO>();
    }

    public class IncomeStatementLineDTO
    {
        public string CategoryName { get; set; } = string.Empty;
        public decimal Amount { get; set; }
    }

    public class BalanceSheetDTO
    {
        public DateTime AsOfDate { get; set; }
        public decimal TotalAssets { get; set; }
        public decimal TotalLiabilities { get; set; }
        public decimal TotalEquity { get; set; }
        
        public List<BalanceSheetLineDTO> Assets { get; set; } = new List<BalanceSheetLineDTO>();
        public List<BalanceSheetLineDTO> Liabilities { get; set; } = new List<BalanceSheetLineDTO>();
        public List<BalanceSheetLineDTO> Equity { get; set; } = new List<BalanceSheetLineDTO>();
    }

    public class BalanceSheetLineDTO
    {
        public string Name { get; set; } = string.Empty;
        public decimal Amount { get; set; }
    }
}
