namespace InventoryManagement.Models.Reports;

/// <summary>
/// One row of the Stock Summary report for a date range.
/// ClosingStock = OpeningStock + TotalIn - TotalOut (calculated by the database).
/// </summary>
public sealed class StockSummaryReportRow
{
    public int ItemId { get; init; }

    public string ItemCode { get; init; } = string.Empty;

    public string ItemName { get; init; } = string.Empty;

    public string? Category { get; init; }

    public string Unit { get; init; } = string.Empty;

    /// <summary>Stock at the start of the From Date.</summary>
    public decimal OpeningStock { get; init; }

    public decimal TotalIn { get; init; }

    public decimal TotalOut { get; init; }

    /// <summary>Stock at the end of the To Date.</summary>
    public decimal ClosingStock { get; init; }
}
