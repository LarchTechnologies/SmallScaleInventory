namespace InventoryManagement.Models;

/// <summary>Figures shown on the dashboard cards (usp_Dashboard_GetSummary).</summary>
public sealed class DashboardSummary
{
    public int TotalItems { get; init; }

    public int ActiveItems { get; init; }

    /// <summary>Sum of current stock of all active items (all units added together).</summary>
    public decimal TotalStockQuantity { get; init; }

    /// <summary>Active items where CurrentStock &lt;= MinimumStock (includes out of stock).</summary>
    public int LowStockItems { get; init; }

    public int OutOfStockItems { get; init; }

    public decimal TodayInQuantity { get; init; }

    public int TodayInCount { get; init; }

    public decimal TodayOutQuantity { get; init; }

    public int TodayOutCount { get; init; }
}
