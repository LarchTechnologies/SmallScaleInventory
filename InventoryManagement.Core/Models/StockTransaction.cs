namespace InventoryManagement.Models;

/// <summary>
/// A single stock movement (table dbo.StockTransactions).
/// Used both as the input for Stock IN / Stock OUT and as a row in lists.
/// </summary>
public class StockTransaction
{
    public long TransactionId { get; set; }

    public int ItemId { get; set; }

    /// <summary>One of the codes in <see cref="TransactionTypes"/> (IN, OUT ...).</summary>
    public string TransactionType { get; set; } = TransactionTypes.In;

    public decimal Quantity { get; set; }

    public DateTime TransactionDate { get; set; } = DateTime.Now;

    public string? ReferenceNo { get; set; }

    public string? Reason { get; set; }

    public string? Remarks { get; set; }

    public DateTime CreatedDate { get; set; }
}
