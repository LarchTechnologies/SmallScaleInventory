using InventoryManagement.Data;
using InventoryManagement.Helpers;
using InventoryManagement.Services;

namespace InventoryManagement;

/// <summary>
/// Composition root: creates the database connection, repositories and
/// services once at start-up and hands them to the forms.
///
///   Forms  ->  Services  ->  Repositories  ->  SQL Server LocalDB
///
/// Forms only ever talk to the services exposed here.
/// </summary>
public sealed class AppServices
{
    private AppServices(DatabaseConnection database)
    {
        Database = database;

        var itemRepository = new ItemRepository(database);
        var stockRepository = new StockRepository(database);
        var reportRepository = new ReportRepository(database);

        Items = new ItemService(itemRepository);
        Stock = new StockService(stockRepository, itemRepository);
        Reports = new ReportService(reportRepository);
        DatabaseInitializer = new DatabaseInitializer(database);
    }

    /// <summary>Connection settings (server / database) read from App.config.</summary>
    public DatabaseConnection Database { get; }

    public ItemService Items { get; }

    public StockService Stock { get; }

    public ReportService Reports { get; }

    public DatabaseInitializer DatabaseInitializer { get; }

    /// <summary>Builds all services using the "InventoryDb" connection string in App.config.</summary>
    public static AppServices CreateFromConfiguration() =>
        new(DatabaseConnection.FromConfiguration());
}
