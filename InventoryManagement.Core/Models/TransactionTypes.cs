namespace InventoryManagement.Models;

/// <summary>
/// Transaction type codes stored in StockTransactions.TransactionType and
/// defined in the lookup table dbo.TransactionTypes.
/// Future types (ADJ_IN, ADJ_OUT, TRANSFER_IN, TRANSFER_OUT ...) are added by
/// inserting a row into dbo.TransactionTypes and a constant here.
/// </summary>
public static class TransactionTypes
{
    public const string In = "IN";

    public const string Out = "OUT";

    /// <summary>Transaction types available in Version 1.</summary>
    public static IReadOnlyList<string> All { get; } = new[] { In, Out };

    /// <summary>
    /// +1 when the type adds stock, -1 when it removes stock.
    /// Mirrors the Direction column of dbo.TransactionTypes.
    /// </summary>
    public static int GetDirection(string transactionType) => transactionType switch
    {
        In => 1,
        Out => -1,
        _ => throw new ArgumentOutOfRangeException(nameof(transactionType), transactionType, "Unknown transaction type."),
    };

    public static bool IsValid(string? transactionType) =>
        transactionType is not null && All.Contains(transactionType);
}
