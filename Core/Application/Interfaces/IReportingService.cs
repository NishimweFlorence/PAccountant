using System;
using System.Threading.Tasks;
using Application.DTO;

namespace Application.Interfaces
{
    public interface IReportingService
    {
        Task<IncomeStatementDTO> GetIncomeStatementAsync(DateTime startDate, DateTime endDate);
        Task<BalanceSheetDTO> GetBalanceSheetAsync(DateTime asOfDate);
        Task<byte[]> ExportIncomeStatementToExcelAsync(DateTime startDate, DateTime endDate);
        Task<byte[]> ExportBalanceSheetToExcelAsync(DateTime asOfDate);
    }
}
