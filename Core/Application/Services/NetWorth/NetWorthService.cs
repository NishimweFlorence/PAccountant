using Application.DTO;
using Application.Interfaces;

namespace Application.Services.NetWorth
{
    /// <summary>
    /// Aggregates Account, Asset, Loan, and Liability data to compute Net Worth.
    ///
    /// Formula:
    ///   Total Value  = Σ Account.balance  +  Σ Asset.CurrentValue  +  Σ Loan.AmountToPay (outstanding)
    ///   Net Worth    = Total Value − Σ Liability.CurrentAmount
    /// </summary>
    public class NetWorthService : INetWorthService
    {
        private readonly IAccountService   _accountService;
        private readonly IAssetService     _assetService;
        private readonly ILiabilityService _liabilityService;
        private readonly ILoanService      _loanService;
        private readonly IReportingService _reportingService;

        public NetWorthService(
            IAccountService   accountService,
            IAssetService     assetService,
            ILiabilityService liabilityService,
            ILoanService      loanService,
            IReportingService reportingService)
        {
            _accountService   = accountService;
            _assetService     = assetService;
            _liabilityService = liabilityService;
            _loanService      = loanService;
            _reportingService = reportingService;
        }

        public async Task<NetWorthSummaryDTO> GetNetWorthSummaryAsync()
        {
            // Existing logic for current summary
            var snapshot = await GetNetWorthSummarySnapshotAsync();
            snapshot.Trends = await GetNetWorthTrendsAsync(6); // Default 6 months for dashboard
            return snapshot;
        }

        private async Task<NetWorthSummaryDTO> GetNetWorthSummarySnapshotAsync()
        {
            // ── Accounts ────────────────────────────────────────────────
            var accounts = await _accountService.GetAllAccountsAsync();
            var accountDtos = accounts.Select(a => new AccountSummaryDTO
            {
                Id       = a.Id,
                Name     = a.Name     ?? "Unnamed",
                Type     = a.Type?.Name ?? "—",
                Balance  = a.Balance  ?? 0m,
                Currency = a.Currency?.Code ?? "RWF",
                Status   = a.Status   ?? "Active",
            }).ToList();
            
            // ── Assets ──────────────────────────────────────────────────
            var assets = await _assetService.GetAllAssetsAsync();
            var assetDtos = assets.Select(a => new AssetSummaryDTO
            {
                Id           = a.Id,
                Name         = a.Name,
                Type         = a.Type?.Name ?? "—",
                CurrentValue = a.CurrentValue,
                Currency     = a.Currency?.Code ?? "RWF",
            }).ToList();

            // ── Loans Given ─────────────────────────────────────────────
            var loans = await _loanService.GetAllLoansAsync();
            var loanDtos = loans.Select(l => new LoanSummaryDTO
            {
                Id          = l.Id,
                Borrower    = l.Borrower    ?? "Unknown",
                AmountLent  = l.Amount,
                AmountToPay = l.AmountToPay,
                Status      = l.Status      ?? "Outstanding",
                Currency    = l.Currency ?? "RWF",
                LoanDate    = l.LoanDate    ?? DateTime.MinValue,
            }).ToList();

            // Only count outstanding loans as assets
            var outstandingLoans = loanDtos.Where(l =>
                !string.Equals(l.Status, "Repaid", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(l.Status, "Paid",   StringComparison.OrdinalIgnoreCase));

            // ── Liabilities ─────────────────────────────────────────────
            var liabilities = await _liabilityService.GetAllLiabilitiesAsync();
            var liabilityDtos = liabilities.Select(l => new LiabilitySummaryDTO
            {
                Id             = l.Id,
                LenderName     = l.LenderName     ?? "—",
                Type           = l.Type           ?? "—",
                OriginalAmount = l.OriginalAmount,
                CurrentAmount  = l.CurrentAmount,
                DueDate        = l.DueDate,
                Currency       = l.Currency?.Code  ?? "RWF",
            }).ToList();

            // ── Totals ───────────────────────────────────────────────────
            var totalAccounts    = accountDtos.Sum(a => a.Balance);
            var totalAssets      = assetDtos.Sum(a => a.CurrentValue);
            var totalLoansGiven  = outstandingLoans.Sum(l => l.AmountToPay);
            var totalLiabilities = liabilityDtos.Sum(l => l.CurrentAmount);

            return new NetWorthSummaryDTO
            {
                TotalAccounts    = totalAccounts,
                TotalAssets      = totalAssets,
                TotalLoansGiven  = totalLoansGiven,
                TotalLiabilities = totalLiabilities,
                Accounts         = accountDtos,
                Assets           = assetDtos,
                LoansGiven       = loanDtos,
                Liabilities      = liabilityDtos,
            };
        }

        public async Task<List<NetWorthTrendPointDTO>> GetNetWorthTrendsAsync(int months)
        {
            var trends = new List<NetWorthTrendPointDTO>();
            var today = DateTime.Today;

            // Generate points for the last N months (including current)
            for (int i = months - 1; i >= 0; i--)
            {
                var date = new DateTime(today.Year, today.Month, 1).AddMonths(-i);
                // End of that month
                var endOfMonth = new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
                
                // Use the reporting service to get a balance sheet as of that date
                var balanceSheet = await _reportingService.GetBalanceSheetAsync(endOfMonth);

                trends.Add(new NetWorthTrendPointDTO
                {
                    Month = endOfMonth.ToString("MMM yyyy"),
                    Accounts = balanceSheet.Assets.Sum(a => a.Amount), // This includes bank accounts and loans in the current reporting logic
                    Assets = (await _assetService.GetAllAssetsAsync())
                                .Where(a => a.CreatedAt <= endOfMonth) // Correctly filter assets that existed then
                                .Sum(a => a.CurrentValue), 
                    Liabilities = balanceSheet.TotalLiabilities,
                    NetWorth = balanceSheet.TotalEquity
                });
            }

            return trends;
        }
    }
}
