using InventoryManagement.Models;
using InventoryManagement.Models.Reports;

namespace InventoryManagement.Data;

/// <summary>
/// Read-only access for reports and the dashboard. Every figure is calculated
/// by SQL Server from the Items and StockTransactions tables.
/// To add a report: create a usp_Report_* procedure, a row model in
/// Models/Reports, a method here and a method in ReportService.
/// </summary>
public sealed class ReportRepository
{
    private readonly StoredProcedureExecutor _executor;

    public ReportRepository(DatabaseConnection database)
    {
        _executor = new StoredProcedureExecutor(database);
    }

    public IReadOnlyList<CurrentStockReportRow> GetCurrentStock(CurrentStockReportFilter filter)
    {
        return _executor.Query(
            StoredProcedures.ReportCurrentStock,
            RowMappers.MapCurrentStockRow,
            DbParameters.NVarChar("@SearchText", filter.SearchText, FieldLengths.SearchText),
            DbParameters.NVarChar("@Category", filter.Category, FieldLengths.Category),
            DbParameters.VarChar("@StockStatus", filter.StockStatus, 20),
            DbParameters.Bit("@IncludeInactive", filter.IncludeInactive));
    }

    public IReadOnlyList<StockSummaryReportRow> GetStockSummary(StockSummaryReportFilter filter)
    {
        return _executor.Query(
            StoredProcedures.ReportStockSummary,
            RowMappers.MapStockSummaryRow,
            DbParameters.Date("@FromDate", filter.FromDate),
            DbParameters.Date("@ToDate", filter.ToDate),
            DbParameters.Int("@ItemId", NullIfZero(filter.ItemId)),
            DbParameters.Bit("@IncludeInactive", filter.IncludeInactive));
    }

    public IReadOnlyList<StockMovementReportRow> GetStockMovement(StockMovementReportFilter filter)
    {
        return _executor.Query(
            StoredProcedures.ReportStockMovement,
            RowMappers.MapMovementRow,
            DbParameters.Date("@FromDate", filter.FromDate),
            DbParameters.Date("@ToDate", filter.ToDate),
            DbParameters.Int("@ItemId", NullIfZero(filter.ItemId)),
            DbParameters.VarChar("@TransactionType", filter.TransactionType, FieldLengths.TransactionType),
            DbParameters.NVarChar("@SearchText", filter.SearchText, FieldLengths.SearchText));
    }

    public DashboardSummary GetDashboardSummary()
    {
        return _executor.QuerySingleOrDefault(StoredProcedures.DashboardGetSummary, RowMappers.MapDashboardSummary)
            ?? new DashboardSummary();
    }

    public IReadOnlyList<CurrentStockReportRow> GetLowStockItems()
    {
        return _executor.Query(StoredProcedures.DashboardGetLowStockItems, RowMappers.MapCurrentStockRow);
    }

    private static int? NullIfZero(int? value) => value is null or 0 ? null : value;
}
