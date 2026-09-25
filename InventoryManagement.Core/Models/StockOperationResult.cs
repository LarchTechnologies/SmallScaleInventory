namespace InventoryManagement.Models;

/// <summary>Outcome of a successful Stock IN / Stock OUT posting.</summary>
public sealed class StockOperationResult
{
    public long TransactionId { get; init; }

    /// <summary>Stock before the posting (read inside the database transaction).</summary>
    public decimal PreviousStock { get; init; }

    /// <summary>Stock after the posting.</summary>
    public decimal NewStock { get; init; }
}
