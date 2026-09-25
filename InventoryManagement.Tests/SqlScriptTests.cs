using System.Text.RegularExpressions;
using InventoryManagement.Data;
using InventoryManagement.Helpers;

namespace InventoryManagement.Tests;

/// <summary>
/// Keeps the SQL scripts and the C# code in step: every stored procedure the
/// code calls must be defined in StoredProcedures.sql, and the scripts must
/// split into batches the way DatabaseInitializer runs them.
/// </summary>
public class SqlScriptTests
{
    private static readonly string ScriptsDirectory = Path.Combine(AppContext.BaseDirectory, "Scripts");

    private static string ReadScript(string name) => File.ReadAllText(Path.Combine(ScriptsDirectory, name));

    [Fact]
    public void SplitSqlBatches_SplitsOnGoLines()
    {
        const string script = "SELECT 1;\nGO\nSELECT 2;\n  go  \nSELECT 3;\nGO -- end of batch\n\nGO\n";

        IReadOnlyList<string> batches = DatabaseInitializer.SplitSqlBatches(script);

        Assert.Equal(3, batches.Count);
        Assert.Contains("SELECT 2;", batches[1]);
    }

    [Fact]
    public void SplitSqlBatches_DoesNotSplitInsideStatements()
    {
        const string script = "SELECT 'GO' AS Word;\nGOTO_Label:\nPRINT N'Ready to GO';\nGO";

        IReadOnlyList<string> batches = DatabaseInitializer.SplitSqlBatches(script);

        Assert.Single(batches);
    }

    [Fact]
    public void EveryStoredProcedureUsedByCode_IsDefinedInScript()
    {
        string script = ReadScript(DatabaseInitializer.StoredProceduresScript);
        var defined = Regex.Matches(script, @"CREATE\s+OR\s+ALTER\s+PROCEDURE\s+(dbo\.\w+)", RegexOptions.IgnoreCase)
            .Select(m => m.Groups[1].Value)
            .ToHashSet(StringComparer.OrdinalIgnoreCase);

        foreach (string procedure in StoredProcedures.All)
        {
            Assert.True(defined.Contains(procedure), $"{procedure} is not defined in StoredProcedures.sql");
        }
    }

    [Theory]
    [InlineData("Tables.sql", "dbo.Items")]
    [InlineData("Tables.sql", "dbo.StockTransactions")]
    [InlineData("Tables.sql", "dbo.TransactionTypes")]
    [InlineData("Tables.sql", "dbo.vw_ItemStock")]
    public void Tables_DefinesRequiredObjects(string scriptName, string objectName)
    {
        Assert.Contains(objectName, ReadScript(scriptName), StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void StockOut_UsesTransactionAndLocking()
    {
        string script = ReadScript(DatabaseInitializer.StoredProceduresScript);

        Assert.Contains("BEGIN TRANSACTION", script, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("ROLLBACK", script, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("UPDLOCK", script, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("Insufficient stock. Available quantity:", script);
    }

    [Theory]
    [InlineData("RM-001", "Raw Material A")]
    [InlineData("RM-002", "Raw Material B")]
    [InlineData("FG-001", "Finished Product A")]
    public void SeedData_ContainsRequiredDemoItems(string code, string name)
    {
        string seed = ReadScript(DatabaseInitializer.SeedDataScript);
        Assert.Contains(code, seed);
        Assert.Contains(name, seed);
    }

    [Theory]
    [InlineData("Database.sql")]
    [InlineData("Tables.sql")]
    [InlineData("StoredProcedures.sql")]
    [InlineData("SeedData.sql")]
    [InlineData("RemoveDemoData.sql")]
    public void Scripts_SplitIntoBatches(string scriptName)
    {
        Assert.NotEmpty(DatabaseInitializer.SplitSqlBatches(ReadScript(scriptName)));
    }
}
