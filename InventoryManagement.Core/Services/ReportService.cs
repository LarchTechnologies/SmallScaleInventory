using InventoryManagement.Data;
using InventoryManagement.Exceptions;
using InventoryManagement.Helpers;
using InventoryManagement.Models;
using InventoryManagement.Models.Reports;

namespace InventoryManagement.Services;

/// <summary>
/// Reports and dashboard figures. Validates report parameters and delegates the
/// calculation to SQL Server via <see cref="ReportRepository"/>; no report total
/// is ever calculated from values that happen to be displayed in the UI.
/// </summary>
public sealed class ReportService
{
    private readonly ReportRepository _reportRepository;

    public ReportService(ReportRepository reportRepository)
    {
        _reportRepository = reportRepository ?? throw new ArgumentNullException(nameof(reportRepository));
    }

    /// <summary>Report 1 - Current Stock.</summary>
    public IReadOnlyList<CurrentStockReportRow> GetCurrentStockReport(CurrentStockReportFilter filter)
    {
        ArgumentNullException.ThrowIfNull(filter);

        if (!string.IsNullOrWhiteSpace(filter.StockStatus) && !StockStatus.All.Contains(filter.StockStatus))
        {
            throw new ValidationException("Please select a valid stock status.");
        }

        filter.SearchText = ValidationHelper.OptionalText(filter.SearchText, "Search text", FieldLengths.SearchText);
        filter.Category = ValidationHelper.OptionalText(filter.Category, "Category", FieldLengths.Category);

        return _reportRepository.GetCurrentStock(filter);
    }

    /// <summary>Report 2 - Stock Summary: opening, IN, OUT and closing per item for a date range.</summary>
    public IReadOnlyList<StockSummaryReportRow> GetStockSummaryReport(StockSummaryReportFilter filter)
    {
        ArgumentNullException.ThrowIfNull(filter);

        ValidationHelper.RequireValidDateRange(filter.FromDate, filter.ToDate);
        return _reportRepository.GetStockSummary(filter);
    }

    /// <summary>Report 3 - Stock Movement: every transaction in the range, with IN / OUT totals.</summary>
    public StockMovementReport GetStockMovementReport(StockMovementReportFilter filter)
    {
        ArgumentNullException.ThrowIfNull(filter);

        ValidationHelper.RequireValidDateRange(filter.FromDate, filter.ToDate);

        if (!string.IsNullOrWhiteSpace(filter.TransactionType) && !TransactionTypes.IsValid(filter.TransactionType))
        {
            throw new ValidationException("Please select a valid transaction type.");
        }

        filter.SearchText = ValidationHelper.OptionalText(filter.SearchText, "Search text", FieldLengths.SearchText);

        return new StockMovementReport(_reportRepository.GetStockMovement(filter));
    }

    public DashboardSummary GetDashboardSummary() => _reportRepository.GetDashboardSummary();

    /// <summary>Active items where current stock &lt;= minimum stock, out of stock first.</summary>
    public IReadOnlyList<CurrentStockReportRow> GetLowStockItems() => _reportRepository.GetLowStockItems();
}
