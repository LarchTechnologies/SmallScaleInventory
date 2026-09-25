namespace InventoryManagement.Models;

/// <summary>
/// An item / product in the item master (table dbo.Items).
/// The stock figures (TotalIn, TotalOut, CurrentStock, StockStatus) are
/// read-only values calculated by the database view dbo.vw_ItemStock;
/// they are never written back.
/// </summary>
public class Item
{
    public int ItemId { get; set; }

    public string ItemCode { get; set; } = string.Empty;

    public string ItemName { get; set; } = string.Empty;

    public string? Category { get; set; }

    public string Unit { get; set; } = string.Empty;

    public decimal MinimumStock { get; set; }

    public decimal OpeningStock { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    // ---- Calculated by the database (dbo.vw_ItemStock) ----------------------

    public decimal TotalIn { get; set; }

    public decimal TotalOut { get; set; }

    public decimal CurrentStock { get; set; }

    public string StockStatus { get; set; } = Models.StockStatus.Normal;

    // ---- Display helpers ----------------------------------------------------

    public string StatusText => IsActive ? "Active" : "Inactive";

    public string DisplayName => $"{ItemCode} - {ItemName}";

    public override string ToString() => DisplayName;
}
