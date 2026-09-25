using InventoryManagement.Models;

namespace InventoryManagement.Services;

/// <summary>
/// The only stock arithmetic in C#. It is used for the live PREVIEW on the
/// Stock IN / Stock OUT screens ("Current + Quantity = New Stock").
/// The authoritative stock figure is always calculated by SQL Server
/// (view dbo.vw_ItemStock) when the transaction is saved.
/// </summary>
public static class StockCalculator
{
    /// <summary>Stock after applying a movement: current + direction x quantity.</summary>
    public static decimal ApplyMovement(decimal currentStock, decimal quantity, string transactionType) =>
        currentStock + (TransactionTypes.GetDirection(transactionType) * quantity);

    /// <summary>True when an outward movement of <paramref name="quantity"/> would make stock negative.</summary>
    public static bool WouldGoNegative(decimal currentStock, decimal quantity, string transactionType) =>
        ApplyMovement(currentStock, quantity, transactionType) < 0;
}
