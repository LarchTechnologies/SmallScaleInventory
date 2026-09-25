using InventoryManagement.Exceptions;
using InventoryManagement.Models;
using InventoryManagement.Models.Reports;
using InventoryManagement.Services;

namespace InventoryManagement.Tests;

public class StockCalculatorTests
{
    [Fact]
    public void StockIn_AddsQuantity()
    {
        // Current 500 + IN 20 = 520
        Assert.Equal(520m, StockCalculator.ApplyMovement(500m, 20m, TransactionTypes.In));
    }

    [Fact]
    public void StockOut_SubtractsQuantity()
    {
        // Available 500 - OUT 20 = 480
        Assert.Equal(480m, StockCalculator.ApplyMovement(500m, 20m, TransactionTypes.Out));
    }

    [Theory]
    [InlineData(10, 11, true)]
    [InlineData(10, 10, false)]
    [InlineData(0, 0.01, true)]
    public void WouldGoNegative_ForStockOut(decimal available, decimal issue, bool expected)
    {
        Assert.Equal(expected, StockCalculator.WouldGoNegative(available, issue, TransactionTypes.Out));
    }

    [Fact]
    public void StockIn_NeverGoesNegative()
    {
        Assert.False(StockCalculator.WouldGoNegative(0m, 5m, TransactionTypes.In));
    }

    [Fact]
    public void UnknownTransactionType_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => StockCalculator.ApplyMovement(1m, 1m, "TRANSFER"));
    }

    [Fact]
    public void CurrentStockFormula_MatchesReadmeExample()
    {
        // README section 6 example (RM-001): Opening 500, IN 200, OUT 150, OUT 120, IN 50 => 480
        decimal stock = 500m;
        stock = StockCalculator.ApplyMovement(stock, 200m, TransactionTypes.In);
        stock = StockCalculator.ApplyMovement(stock, 150m, TransactionTypes.Out);
        stock = StockCalculator.ApplyMovement(stock, 120m, TransactionTypes.Out);
        stock = StockCalculator.ApplyMovement(stock, 50m, TransactionTypes.In);
        Assert.Equal(480m, stock);
    }

    [Fact]
    public void InsufficientStockMessage_ShowsAvailableQuantity()
    {
        using var culture = new TestCulture();
        Assert.Equal("Insufficient stock. Available quantity: 25.00", InsufficientStockException.ForAvailable(25m).Message);
        Assert.Equal("Insufficient stock. Available quantity: 1,250.50 KG", InsufficientStockException.ForAvailable(1250.5m, "KG").Message);
    }

    [Fact]
    public void TransactionTypes_Directions()
    {
        Assert.Equal(1, TransactionTypes.GetDirection(TransactionTypes.In));
        Assert.Equal(-1, TransactionTypes.GetDirection(TransactionTypes.Out));
        Assert.True(TransactionTypes.IsValid("IN"));
        Assert.True(TransactionTypes.IsValid("OUT"));
        Assert.False(TransactionTypes.IsValid(null));
        Assert.False(TransactionTypes.IsValid("ADJUSTMENT"));
    }

    [Fact]
    public void MovementReport_TotalsInAndOutSeparately()
    {
        var rows = new List<StockMovementReportRow>
        {
            new() { TransactionType = TransactionTypes.In, Direction = 1, Quantity = 100m },
            new() { TransactionType = TransactionTypes.In, Direction = 1, Quantity = 50.5m },
            new() { TransactionType = TransactionTypes.Out, Direction = -1, Quantity = 30m },
        };

        var report = new StockMovementReport(rows);

        Assert.Equal(150.5m, report.TotalIn);
        Assert.Equal(30m, report.TotalOut);
        Assert.Equal(120.5m, report.NetMovement);
    }
}
