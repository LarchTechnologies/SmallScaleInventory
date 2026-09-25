namespace InventoryManagement.Models.Reports;

/// <summary>Filters for the Current Stock report. Null / empty means "all".</summary>
public sealed class CurrentStockReportFilter
{
    public string? SearchText { get; set; }

    public string? Category { get; set; }

    /// <summary>One of <see cref="StockStatus"/> or null for all.</summary>
    public string? StockStatus { get; set; }

    public bool IncludeInactive { get; set; }
}
