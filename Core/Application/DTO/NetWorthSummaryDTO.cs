namespace Application.DTO
{
    /// <summary>
    /// Aggregated Net Worth snapshot.
    /// Net Worth = (Accounts + Assets + LoansGiven) - Liabilities
    /// </summary>
    public class NetWorthSummaryDTO
    {
        // ── Totals ──────────────────────────────────────────────────────────
        public decimal TotalAccounts    { get; set; }
        public decimal TotalAssets      { get; set; }
        public decimal TotalLoansGiven  { get; set; }
        public decimal TotalLiabilities { get; set; }

        public decimal TotalValue => TotalAccounts + TotalAssets + TotalLoansGiven;
        public decimal NetWorth   => TotalValue - TotalLiabilities;

        // ── Per-entity breakdowns (for Dashboard tables) ─────────────────
        public List<AccountSummaryDTO>   Accounts    { get; set; } = new();
        public List<AssetSummaryDTO>     Assets      { get; set; } = new();
        public List<LoanSummaryDTO>      LoansGiven  { get; set; } = new();
        public List<LiabilitySummaryDTO> Liabilities { get; set; } = new();
    }

    public class AccountSummaryDTO
    {
        public int     Id       { get; set; }
        public string  Name     { get; set; } = string.Empty;
        public string  Type     { get; set; } = string.Empty;
        public float?  AccountNumber { get; set; }
        public decimal Balance  { get; set; }
        public string  Currency { get; set; } = string.Empty;
        public string  Status   { get; set; } = string.Empty;
    }

    public class AssetSummaryDTO
    {
        public int     Id           { get; set; }
        public string  Name         { get; set; } = string.Empty;
        public string  Category     { get; set; } = string.Empty;
        public decimal CurrentValue { get; set; }
        public string  Currency     { get; set; } = string.Empty;
    }

    public class LoanSummaryDTO
    {
        public int     Id          { get; set; }
        public string  Borrower    { get; set; } = string.Empty;
        public decimal AmountLent  { get; set; }
        public decimal AmountToPay { get; set; }
        public string  Status      { get; set; } = string.Empty;
        public string  Currency    { get; set; } = string.Empty;
        public DateTime LoanDate   { get; set; }
    }

    public class LiabilitySummaryDTO
    {
        public int     Id             { get; set; }
        public string  LenderName     { get; set; } = string.Empty;
        public string  Type           { get; set; } = string.Empty;
        public decimal OriginalAmount { get; set; }
        public decimal CurrentAmount  { get; set; }
        public DateTime DueDate       { get; set; }
        public string  Currency       { get; set; } = string.Empty;
    }
}
