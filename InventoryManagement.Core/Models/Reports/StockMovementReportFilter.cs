namespace InventoryManagement.Models.Reports;

/// <summary>Parameters of the Stock Movement report.</summary>
public sealed class StockMovementReportFilter
{
    public DateTime FromDate { get; set; } = DateTime.Today;

    public DateTime ToDate { get; set; } = DateTime.Today;

    /// <summary>Null or 0 = all items.</summary>
    public int? ItemId { get; set; }

    /// <summary>One of <see cref="TransactionTypes"/> or null for all.</summary>
    public string? TransactionType { get; set; }

    /// <summary>Optional text searched in reference, reason, remarks, item code and name.</summary>
    public string? SearchText { get; set; }
}
