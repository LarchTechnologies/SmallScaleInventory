namespace InventoryManagement.Models.Reports;

/// <summary>Parameters of the Stock Summary report.</summary>
public sealed class StockSummaryReportFilter
{
    public DateTime FromDate { get; set; } = DateTime.Today;

    public DateTime ToDate { get; set; } = DateTime.Today;

    /// <summary>Null or 0 = all items.</summary>
    public int? ItemId { get; set; }

    public bool IncludeInactive { get; set; }
}
