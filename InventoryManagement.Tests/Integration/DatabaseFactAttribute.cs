namespace InventoryManagement.Tests.Integration;

/// <summary>
/// A [Fact] that only runs when INVENTORY_TEST_SQLSERVER is set, for example:
///   set INVENTORY_TEST_SQLSERVER=Data Source=(localdb)\MSSQLLocalDB;Integrated Security=True;TrustServerCertificate=True
/// The tests create (and afterwards drop) their own database
/// "InventoryManagementDB_Tests"; the real InventoryManagementDB is never touched.
/// </summary>
public sealed class DatabaseFactAttribute : FactAttribute
{
    public const string EnvironmentVariable = "INVENTORY_TEST_SQLSERVER";

    public DatabaseFactAttribute()
    {
        if (string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable(EnvironmentVariable)))
        {
            Skip = $"Integration test: set {EnvironmentVariable} to a SQL Server connection string to run it.";
        }
    }
}
