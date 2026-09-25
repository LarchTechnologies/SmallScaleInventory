namespace InventoryManagement.Models.Reports;

/// <summary>One transaction line of the Stock Movement report.</summary>
public sealed class StockMovementReportRow
{
    public long TransactionId { get; init; }

    public DateTime TransactionDate { get; init; }

    public int ItemId { get; init; }

    public string ItemCode { get; init; } = string.Empty;

    public string ItemName { get; init; } = string.Empty;

    public string Unit { get; init; } = string.Empty;

    public string TransactionType { get; init; } = string.Empty;

    /// <summary>+1 inward, -1 outward (from dbo.TransactionTypes).</summary>
    public int Direction { get; init; }

    public decimal Quantity { get; init; }

    public string? ReferenceNo { get; init; }

    public string? Reason { get; init; }

    public string? Remarks { get; init; }

    public DateTime CreatedDate { get; init; }
}
