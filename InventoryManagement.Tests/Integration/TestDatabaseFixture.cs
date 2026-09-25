using InventoryManagement.Data;
using InventoryManagement.Helpers;
using InventoryManagement.Services;
using Microsoft.Data.SqlClient;

namespace InventoryManagement.Tests.Integration;

/// <summary>
/// Creates a fresh test database from the real SQL scripts (through
/// DatabaseInitializer, exactly like the application) and drops it afterwards.
/// </summary>
public sealed class TestDatabaseFixture : IDisposable
{
    public const string DatabaseName = "InventoryManagementDB_Tests";

    public TestDatabaseFixture()
    {
        string? server = Environment.GetEnvironmentVariable(DatabaseFactAttribute.EnvironmentVariable);
        if (string.IsNullOrWhiteSpace(server))
        {
            return; // Tests are skipped.
        }

        var builder = new SqlConnectionStringBuilder(server) { InitialCatalog = DatabaseName };
        Database = new DatabaseConnection(builder.ConnectionString);

        DropDatabase();

        var initializer = new DatabaseInitializer(Database, Path.Combine(AppContext.BaseDirectory, "Scripts"));
        initializer.CreateDatabase();
        initializer.InstallOrUpgradeSchema();

        var itemRepository = new ItemRepository(Database);
        Items = new ItemService(itemRepository);
        Stock = new StockService(new StockRepository(Database), itemRepository);
        Reports = new ReportService(new ReportRepository(Database));
        Initializer = initializer;
    }

    public DatabaseConnection Database { get; } = null!;

    public DatabaseInitializer Initializer { get; } = null!;

    public ItemService Items { get; } = null!;

    public StockService Stock { get; } = null!;

    public ReportService Reports { get; } = null!;

    public void Dispose()
    {
        if (Database is not null)
        {
            DropDatabase();
        }
    }

    private void DropDatabase()
    {
        SqlConnection.ClearAllPools();

        using SqlConnection connection = Database.ForDatabase("master").OpenConnection();
        using var command = new SqlCommand(
            $"IF DB_ID(N'{DatabaseName}') IS NOT NULL BEGIN ALTER DATABASE [{DatabaseName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE; DROP DATABASE [{DatabaseName}]; END",
            connection);
        command.ExecuteNonQuery();
    }
}

[CollectionDefinition(Name)]
public sealed class DatabaseCollection : ICollectionFixture<TestDatabaseFixture>
{
    public const string Name = "SQL Server database";
}
