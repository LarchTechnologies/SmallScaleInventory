using InventoryManagement.Models;
using InventoryManagement.Models.Reports;
using Microsoft.Data.SqlClient;

namespace InventoryManagement.Data;

/// <summary>Maps result set rows to models. Shared by the repositories so no mapping is duplicated.</summary>
internal static class RowMappers
{
    public static Item MapItem(SqlDataReader reader) => new()
    {
        ItemId = reader.GetInt("ItemId"),
        ItemCode = reader.GetText("ItemCode"),
        ItemName = reader.GetText("ItemName"),
        Category = reader.GetNullableText("Category"),
        Unit = reader.GetText("Unit"),
        MinimumStock = reader.GetDecimalValue("MinimumStock"),
        OpeningStock = reader.GetDecimalValue("OpeningStock"),
        IsActive = reader.GetBool("IsActive"),
        CreatedDate = reader.GetDate("CreatedDate"),
        ModifiedDate = reader.GetNullableDate("ModifiedDate"),
        TotalIn = reader.GetDecimalValue("TotalIn"),
        TotalOut = reader.GetDecimalValue("TotalOut"),
        CurrentStock = reader.GetDecimalValue("CurrentStock"),
        StockStatus = reader.GetText("StockStatus"),
    };

    public static CurrentStockReportRow MapCurrentStockRow(SqlDataReader reader) => new()
    {
        ItemId = reader.GetInt("ItemId"),
        ItemCode = reader.GetText("ItemCode"),
        ItemName = reader.GetText("ItemName"),
        Category = reader.GetNullableText("Category"),
        Unit = reader.GetText("Unit"),
        OpeningStock = reader.GetDecimalValue("OpeningStock"),
        TotalIn = reader.GetDecimalValue("TotalIn"),
        TotalOut = reader.GetDecimalValue("TotalOut"),
        CurrentStock = reader.GetDecimalValue("CurrentStock"),
        MinimumStock = reader.GetDecimalValue("MinimumStock"),
        StockStatus = reader.GetText("StockStatus"),
        IsActive = reader.GetBool("IsActive"),
    };

    public static StockSummaryReportRow MapStockSummaryRow(SqlDataReader reader) => new()
    {
        ItemId = reader.GetInt("ItemId"),
        ItemCode = reader.GetText("ItemCode"),
        ItemName = reader.GetText("ItemName"),
        Category = reader.GetNullableText("Category"),
        Unit = reader.GetText("Unit"),
        OpeningStock = reader.GetDecimalValue("OpeningStock"),
        TotalIn = reader.GetDecimalValue("TotalIn"),
        TotalOut = reader.GetDecimalValue("TotalOut"),
        ClosingStock = reader.GetDecimalValue("ClosingStock"),
    };

    public static StockMovementReportRow MapMovementRow(SqlDataReader reader) => new()
    {
        TransactionId = reader.GetLong("TransactionId"),
        TransactionDate = reader.GetDate("TransactionDate"),
        ItemId = reader.GetInt("ItemId"),
        ItemCode = reader.GetText("ItemCode"),
        ItemName = reader.GetText("ItemName"),
        Unit = reader.GetText("Unit"),
        TransactionType = reader.GetText("TransactionType"),
        Direction = reader.GetInt("Direction"),
        Quantity = reader.GetDecimalValue("Quantity"),
        ReferenceNo = reader.GetNullableText("ReferenceNo"),
        Reason = reader.GetNullableText("Reason"),
        Remarks = reader.GetNullableText("Remarks"),
        CreatedDate = reader.GetDate("CreatedDate"),
    };

    public static DashboardSummary MapDashboardSummary(SqlDataReader reader) => new()
    {
        TotalItems = reader.GetInt("TotalItems"),
        ActiveItems = reader.GetInt("ActiveItems"),
        TotalStockQuantity = reader.GetDecimalValue("TotalStockQuantity"),
        LowStockItems = reader.GetInt("LowStockItems"),
        OutOfStockItems = reader.GetInt("OutOfStockItems"),
        TodayInQuantity = reader.GetDecimalValue("TodayInQuantity"),
        TodayInCount = reader.GetInt("TodayInCount"),
        TodayOutQuantity = reader.GetDecimalValue("TodayOutQuantity"),
        TodayOutCount = reader.GetInt("TodayOutCount"),
    };
}
