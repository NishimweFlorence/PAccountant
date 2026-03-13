using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Application.DTO;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using ClosedXML.Excel;

namespace Infrastructure.Services
{
    public class ReportingService : IReportingService
    {
        private readonly ApplicationDbContext _dbContext;

        public ReportingService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IncomeStatementDTO> GetIncomeStatementAsync(DateTime startDate, DateTime endDate)
        {
            var dto = new IncomeStatementDTO { StartDate = startDate, EndDate = endDate };

            var entries = await _dbContext.Transactions
                .Include(t => t.Entries)
                .ThenInclude(e => e.Category)
                .ThenInclude(c => c!.Type)
                .Where(t => t.TransactionDate >= startDate && t.TransactionDate <= endDate)
                .SelectMany(t => t.Entries)
                .Where(e => e.Category != null && e.Category.Type != null)
                .ToListAsync();

            var revenueGroups = entries
                .Where(e => e.Category!.Type!.Name.ToLower() == "income" || e.Category!.Type!.Name.ToLower() == "revenue")
                .GroupBy(e => e.Category!.Name)
                .Select(g => new IncomeStatementLineDTO
                {
                    CategoryName = g.Key ?? "Uncategorized",
                    Amount = g.Sum(e => e.Credit - e.Debit) 
                })
                .Where(l => l.Amount != 0)
                .ToList();

            var expenseGroups = entries
                .Where(e => e.Category!.Type!.Name.ToLower() == "expense")
                .GroupBy(e => e.Category!.Name)
                .Select(g => new IncomeStatementLineDTO
                {
                    CategoryName = g.Key ?? "Uncategorized",
                    Amount = g.Sum(e => e.Debit - e.Credit) 
                })
                .Where(l => l.Amount != 0)
                .ToList();

            dto.RevenueLines = revenueGroups;
            dto.ExpenseLines = expenseGroups;
            dto.TotalRevenue = dto.RevenueLines.Sum(l => l.Amount);
            dto.TotalExpenses = dto.ExpenseLines.Sum(l => l.Amount);

            return dto;
        }

        public async Task<BalanceSheetDTO> GetBalanceSheetAsync(DateTime asOfDate)
        {
            var dto = new BalanceSheetDTO { AsOfDate = asOfDate };

            var assetEntries = await _dbContext.Transactions
                .Where(t => t.TransactionDate <= asOfDate)
                .SelectMany(t => t.Entries)
                .Include(e => e.Account)
                .Include(e => e.Category)
                .ThenInclude(c => c!.Type)
                .ToListAsync();

            var bankAssets = assetEntries
                .Where(e => e.AccountId.HasValue)
                .GroupBy(e => e.Account!.Name)
                .Select(g => new BalanceSheetLineDTO
                {
                    Name = g.Key ?? "Unknown Bank Account",
                    Amount = g.Sum(e => e.Debit - e.Credit) 
                })
                .Where(l => l.Amount != 0)
                .ToList();

            var loanAssets = assetEntries
                .Where(e => e.Category != null && e.Category.Name == "Loan Asset")
                .GroupBy(e => "Loan Receivables")
                .Select(g => new BalanceSheetLineDTO
                {
                    Name = g.Key,
                    Amount = g.Sum(e => e.Debit - e.Credit) 
                })
                .Where(l => l.Amount != 0)
                .ToList();

            dto.Assets.AddRange(bankAssets);
            dto.Assets.AddRange(loanAssets);
            dto.TotalAssets = dto.Assets.Sum(l => l.Amount);

            var liabilityLines = assetEntries
                .Where(e => e.Category != null && e.Category.Type != null && e.Category.Type.Name.ToLower() == "liability")
                .GroupBy(e => e.Category!.Name)
                .Select(g => new BalanceSheetLineDTO
                {
                    Name = g.Key ?? "Liabilities",
                    Amount = g.Sum(e => e.Credit - e.Debit)
                })
                .Where(l => l.Amount != 0)
                .ToList();
            
            dto.Liabilities.AddRange(liabilityLines);
            dto.TotalLiabilities = dto.Liabilities.Sum(l => l.Amount);

            var equityLines = assetEntries
                .Where(e => e.Category != null && (e.Category.Type == null || e.Category.Type.Name.ToLower() == "equity") && e.Category.Name != "Loan Asset")
                .GroupBy(e => e.Category!.Name)
                .Select(g => new BalanceSheetLineDTO
                {
                    Name = g.Key ?? "Equity",
                    Amount = g.Sum(e => e.Credit - e.Debit)
                })
                .Where(l => l.Amount != 0)
                .ToList();

            var incomeStatementToDate = await GetIncomeStatementAsync(DateTime.MinValue, asOfDate);
            if (incomeStatementToDate.NetIncome != 0)
            {
                equityLines.Add(new BalanceSheetLineDTO { Name = "Retained Earnings", Amount = incomeStatementToDate.NetIncome });
            }

            dto.Equity.AddRange(equityLines);
            dto.TotalEquity = dto.Equity.Sum(l => l.Amount);

            return dto;
        }

        public async Task<byte[]> ExportIncomeStatementToExcelAsync(DateTime startDate, DateTime endDate)
        {
            var data = await GetIncomeStatementAsync(startDate, endDate);
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Income Statement");

            worksheet.Cell(1, 1).Value = "Income Statement";
            worksheet.Cell(2, 1).Value = $"Period: {startDate:d} to {endDate:d}";

            worksheet.Cell(4, 1).Value = "Revenue";
            int row = 5;
            foreach (var item in data.RevenueLines)
            {
                worksheet.Cell(row, 1).Value = item.CategoryName;
                worksheet.Cell(row, 2).Value = item.Amount;
                row++;
            }
            worksheet.Cell(row, 1).Value = "Total Revenue";
            worksheet.Cell(row, 2).Value = data.TotalRevenue;
            
            row += 2;
            worksheet.Cell(row, 1).Value = "Expenses";
            row++;
            foreach (var item in data.ExpenseLines)
            {
                worksheet.Cell(row, 1).Value = item.CategoryName;
                worksheet.Cell(row, 2).Value = item.Amount;
                row++;
            }
            worksheet.Cell(row, 1).Value = "Total Expenses";
            worksheet.Cell(row, 2).Value = data.TotalExpenses;

            row += 2;
            worksheet.Cell(row, 1).Value = "Net Income";
            worksheet.Cell(row, 2).Value = data.NetIncome;

            worksheet.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return stream.ToArray();
        }

        public async Task<byte[]> ExportBalanceSheetToExcelAsync(DateTime asOfDate)
        {
            var data = await GetBalanceSheetAsync(asOfDate);
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Balance Sheet");

            worksheet.Cell(1, 1).Value = "Balance Sheet";
            worksheet.Cell(2, 1).Value = $"As Of: {asOfDate:d}";

            int row = 4;
            worksheet.Cell(row, 1).Value = "Assets";
            row++;
            foreach (var item in data.Assets)
            {
                worksheet.Cell(row, 1).Value = item.Name;
                worksheet.Cell(row, 2).Value = item.Amount;
                row++;
            }
            worksheet.Cell(row, 1).Value = "Total Assets";
            worksheet.Cell(row, 2).Value = data.TotalAssets;

            row += 2;
            worksheet.Cell(row, 1).Value = "Liabilities";
            row++;
            foreach (var item in data.Liabilities)
            {
                worksheet.Cell(row, 1).Value = item.Name;
                worksheet.Cell(row, 2).Value = item.Amount;
                row++;
            }
            worksheet.Cell(row, 1).Value = "Total Liabilities";
            worksheet.Cell(row, 2).Value = data.TotalLiabilities;

            row += 2;
            worksheet.Cell(row, 1).Value = "Equity";
            row++;
            foreach (var item in data.Equity)
            {
                worksheet.Cell(row, 1).Value = item.Name;
                worksheet.Cell(row, 2).Value = item.Amount;
                row++;
            }
            worksheet.Cell(row, 1).Value = "Total Equity";
            worksheet.Cell(row, 2).Value = data.TotalEquity;

            worksheet.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return stream.ToArray();
        }
    }
}
