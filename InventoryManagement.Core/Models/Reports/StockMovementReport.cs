namespace InventoryManagement.Models.Reports;

/// <summary>Result of the Stock Movement report: the rows plus their totals.</summary>
public sealed class StockMovementReport
{
    public StockMovementReport(IReadOnlyList<StockMovementReportRow> rows)
    {
        Rows = rows;
        TotalIn = rows.Where(r => r.Direction > 0).Sum(r => r.Quantity);
        TotalOut = rows.Where(r => r.Direction < 0).Sum(r => r.Quantity);
    }

    public IReadOnlyList<StockMovementReportRow> Rows { get; }

    public decimal TotalIn { get; }

    public decimal TotalOut { get; }

    public decimal NetMovement => TotalIn - TotalOut;
}
