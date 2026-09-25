namespace InventoryManagement.Models.Reports;

/// <summary>One row of the Current Stock report (also used for the dashboard low stock list).</summary>
public sealed class CurrentStockReportRow
{
    public int ItemId { get; init; }

    public string ItemCode { get; init; } = string.Empty;

    public string ItemName { get; init; } = string.Empty;

    public string? Category { get; init; }

    public string Unit { get; init; } = string.Empty;

    public decimal OpeningStock { get; init; }

    public decimal TotalIn { get; init; }

    public decimal TotalOut { get; init; }

    public decimal CurrentStock { get; init; }

    public decimal MinimumStock { get; init; }

    public string StockStatus { get; init; } = Models.StockStatus.Normal;

    public bool IsActive { get; init; }
}
