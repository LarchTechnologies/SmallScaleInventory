namespace InventoryManagement.Data;

/// <summary>
/// Names of all stored procedures used by the application (Scripts/StoredProcedures.sql).
/// Keeping them here avoids magic strings and lets DatabaseInitializer verify
/// that the database contains everything the code needs.
/// </summary>
public static class StoredProcedures
{
    public const string ItemGetAll = "dbo.usp_Item_GetAll";
    public const string ItemGetById = "dbo.usp_Item_GetById";
    public const string ItemGetCategories = "dbo.usp_Item_GetCategories";
    public const string ItemGetUnits = "dbo.usp_Item_GetUnits";
    public const string ItemInsert = "dbo.usp_Item_Insert";
    public const string ItemUpdate = "dbo.usp_Item_Update";
    public const string ItemSetActive = "dbo.usp_Item_SetActive";

    public const string StockGetCurrentStock = "dbo.usp_Stock_GetCurrentStock";
    public const string StockPostTransaction = "dbo.usp_Stock_PostTransaction";
    public const string StockIn = "dbo.usp_Stock_In";
    public const string StockOut = "dbo.usp_Stock_Out";
    public const string StockGetRecentTransactions = "dbo.usp_Stock_GetRecentTransactions";

    public const string ReportCurrentStock = "dbo.usp_Report_CurrentStock";
    public const string ReportStockSummary = "dbo.usp_Report_StockSummary";
    public const string ReportStockMovement = "dbo.usp_Report_StockMovement";

    public const string DashboardGetSummary = "dbo.usp_Dashboard_GetSummary";
    public const string DashboardGetLowStockItems = "dbo.usp_Dashboard_GetLowStockItems";

    /// <summary>Every procedure the application calls.</summary>
    public static IReadOnlyList<string> All { get; } = new[]
    {
        ItemGetAll, ItemGetById, ItemGetCategories, ItemGetUnits, ItemInsert, ItemUpdate, ItemSetActive,
        StockGetCurrentStock, StockPostTransaction, StockIn, StockOut, StockGetRecentTransactions,
        ReportCurrentStock, ReportStockSummary, ReportStockMovement,
        DashboardGetSummary, DashboardGetLowStockItems,
    };
}
