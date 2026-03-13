using Application.DTO;

namespace Application.Interfaces
{
    public interface INetWorthService
    {
        /// <summary>
        /// Returns a full Net Worth snapshot:
        ///   Total Value  = Accounts + Assets + LoansGiven
        ///   Net Worth    = Total Value − Liabilities
        /// </summary>
        Task<NetWorthSummaryDTO> GetNetWorthSummaryAsync();
        Task<List<NetWorthTrendPointDTO>> GetNetWorthTrendsAsync(int months);
    }
}
