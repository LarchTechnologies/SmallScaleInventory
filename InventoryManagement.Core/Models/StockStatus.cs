namespace InventoryManagement.Models;

/// <summary>
/// Stock status values produced by the database view dbo.vw_ItemStock:
///   CurrentStock &lt;= 0             -> OUT OF STOCK
///   CurrentStock &lt;= MinimumStock  -> LOW STOCK
///   otherwise                     -> NORMAL
/// </summary>
public static class StockStatus
{
    public const string Normal = "NORMAL";

    public const string Low = "LOW STOCK";

    public const string OutOfStock = "OUT OF STOCK";

    public static IReadOnlyList<string> All { get; } = new[] { Normal, Low, OutOfStock };
}
