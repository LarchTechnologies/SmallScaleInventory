using InventoryManagement.Exceptions;
using InventoryManagement.Helpers;
using InventoryManagement.Models;
using InventoryManagement.Models.Reports;

namespace InventoryManagement.Tests.Integration;

/// <summary>
/// End-to-end checks of services + stored procedures on a real SQL Server.
/// Each test uses its own item codes so the tests are independent.
/// </summary>
[Collection(DatabaseCollection.Name)]
public class InventoryIntegrationTests
{
    private readonly TestDatabaseFixture _db;

    public InventoryIntegrationTests(TestDatabaseFixture db)
    {
        _db = db;
    }

    [DatabaseFact]
    public void Database_IsReportedReady()
    {
        Assert.Equal(DatabaseState.Ready, _db.Initializer.CheckStatus().State);
    }

    [DatabaseFact]
    public void CreateItem_ThenDuplicateCode_IsRejected()
    {
        int id = _db.Items.CreateItem(NewItem("IT-DUP", opening: 10m));
        Assert.True(id > 0);

        var duplicate = NewItem("it-dup", opening: 0m); // same code, different case
        Assert.Throws<DuplicateItemCodeException>(() => _db.Items.CreateItem(duplicate));
    }

    [DatabaseFact]
    public void StockIn_IncreasesStock()
    {
        int id = _db.Items.CreateItem(NewItem("IT-IN", opening: 500m));

        StockOperationResult result = _db.Stock.AddStock(Movement(id, 20m));

        Assert.Equal(500m, result.PreviousStock);
        Assert.Equal(520m, result.NewStock);
        Assert.Equal(520m, _db.Stock.GetCurrentStock(id));
    }

    [DatabaseFact]
    public void StockOut_DecreasesStock_AndCanReachZero()
    {
        int id = _db.Items.CreateItem(NewItem("IT-OUT", opening: 50m));

        Assert.Equal(30m, _db.Stock.IssueStock(Movement(id, 20m)).NewStock);
        Assert.Equal(0m, _db.Stock.IssueStock(Movement(id, 30m)).NewStock);
    }

    [DatabaseFact]
    public void StockOut_MoreThanAvailable_IsBlocked_AndNothingIsSaved()
    {
        int id = _db.Items.CreateItem(NewItem("IT-NEG", opening: 10m));

        var ex = Assert.Throws<InsufficientStockException>(() => _db.Stock.IssueStock(Movement(id, 11m)));

        Assert.StartsWith("Insufficient stock. Available quantity:", ex.Message);
        Assert.Equal(10m, _db.Stock.GetCurrentStock(id));
    }

    [DatabaseFact]
    public void ConcurrentStockOut_NeverGoesNegative()
    {
        int id = _db.Items.CreateItem(NewItem("IT-CONC", opening: 50m));
        int succeeded = 0;
        int refused = 0;

        // 10 parallel issues of 10 against a stock of 50: exactly 5 may succeed.
        Parallel.For(0, 10, new ParallelOptions { MaxDegreeOfParallelism = 10 }, _ =>
        {
            try
            {
                _db.Stock.IssueStock(Movement(id, 10m));
                Interlocked.Increment(ref succeeded);
            }
            catch (InsufficientStockException)
            {
                Interlocked.Increment(ref refused);
            }
        });

        Assert.Equal(5, succeeded);
        Assert.Equal(5, refused);
        Assert.Equal(0m, _db.Stock.GetCurrentStock(id));
    }

    [DatabaseFact]
    public void InactiveItem_CannotReceiveStock()
    {
        int id = _db.Items.CreateItem(NewItem("IT-INACT", opening: 5m));
        _db.Items.SetItemActive(id, false);

        Assert.Throws<ValidationException>(() => _db.Stock.AddStock(Movement(id, 1m)));

        _db.Items.SetItemActive(id, true);
        Assert.Equal(6m, _db.Stock.AddStock(Movement(id, 1m)).NewStock);
    }

    [DatabaseFact]
    public void CurrentStockReport_ShowsTotalsAndStatus()
    {
        int id = _db.Items.CreateItem(NewItem("IT-RPT", opening: 100m, minimum: 80m));
        _db.Stock.AddStock(Movement(id, 10m));
        _db.Stock.IssueStock(Movement(id, 40m));

        CurrentStockReportRow row = _db.Reports
            .GetCurrentStockReport(new CurrentStockReportFilter { SearchText = "IT-RPT" })
            .Single();

        Assert.Equal(100m, row.OpeningStock);
        Assert.Equal(10m, row.TotalIn);
        Assert.Equal(40m, row.TotalOut);
        Assert.Equal(70m, row.CurrentStock);
        Assert.Equal(StockStatus.Low, row.StockStatus);
    }

    [DatabaseFact]
    public void StockSummary_ClosingEqualsOpeningPlusInMinusOut()
    {
        int id = _db.Items.CreateItem(NewItem("IT-SUM", opening: 200m));
        DateTime today = DateTime.Today;

        _db.Stock.AddStock(Movement(id, 50m, today.AddDays(-10)));   // before the period
        _db.Stock.IssueStock(Movement(id, 20m, today.AddDays(-10))); // before the period
        _db.Stock.AddStock(Movement(id, 30m, today.AddDays(-2)));
        _db.Stock.IssueStock(Movement(id, 45m, today));

        StockSummaryReportRow row = _db.Reports.GetStockSummaryReport(new StockSummaryReportFilter
        {
            FromDate = today.AddDays(-5),
            ToDate = today,
            ItemId = id,
        }).Single();

        Assert.Equal(230m, row.OpeningStock);  // 200 + 50 - 20
        Assert.Equal(30m, row.TotalIn);
        Assert.Equal(45m, row.TotalOut);
        Assert.Equal(215m, row.ClosingStock);  // 230 + 30 - 45
        Assert.Equal(_db.Stock.GetCurrentStock(id), row.ClosingStock);
    }

    [DatabaseFact]
    public void StockMovement_FiltersByTypeAndTotals()
    {
        int id = _db.Items.CreateItem(NewItem("IT-MOV", opening: 100m));
        _db.Stock.AddStock(Movement(id, 15m));
        _db.Stock.AddStock(Movement(id, 5m));
        _db.Stock.IssueStock(Movement(id, 8m));

        var filter = new StockMovementReportFilter { FromDate = DateTime.Today, ToDate = DateTime.Today, ItemId = id };
        StockMovementReport all = _db.Reports.GetStockMovementReport(filter);

        Assert.Equal(3, all.Rows.Count);
        Assert.Equal(20m, all.TotalIn);
        Assert.Equal(8m, all.TotalOut);

        filter.TransactionType = TransactionTypes.Out;
        StockMovementReport outOnly = _db.Reports.GetStockMovementReport(filter);
        Assert.Single(outOnly.Rows);
        Assert.Equal(0m, outOnly.TotalIn);
    }

    [DatabaseFact]
    public void DemoData_Loads()
    {
        _db.Initializer.LoadDemoData();
        Assert.Contains(_db.Items.GetAllItems(), i => i.ItemCode == "RM-001");
    }

    private static Item NewItem(string code, decimal opening, decimal minimum = 0m) => new()
    {
        ItemCode = code,
        ItemName = "Test item " + code,
        Category = "Tests",
        Unit = "KG",
        MinimumStock = minimum,
        OpeningStock = opening,
    };

    private static StockTransaction Movement(int itemId, decimal quantity, DateTime? date = null) => new()
    {
        ItemId = itemId,
        Quantity = quantity,
        TransactionDate = date ?? DateTime.Now.AddSeconds(-1),
        ReferenceNo = "TEST",
    };
}
