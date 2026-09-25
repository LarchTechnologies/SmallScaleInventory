using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using InventoryManagement.Data;
using InventoryManagement.Exceptions;
using InventoryManagement.Logging;
using InventoryManagement.Models;
using Microsoft.Data.SqlClient;

namespace InventoryManagement.Helpers;

/// <summary>State of the configured database, as found at start-up.</summary>
public enum DatabaseState
{
    /// <summary>Server reachable, database exists and contains every required object.</summary>
    Ready,

    /// <summary>SQL Server / LocalDB cannot be reached (or login failed).</summary>
    ServerUnavailable,

    /// <summary>The server is reachable but the database does not exist yet.</summary>
    DatabaseMissing,

    /// <summary>The database exists but tables, views or procedures are missing.</summary>
    SchemaIncomplete,
}

/// <summary>Result of <see cref="DatabaseInitializer.CheckStatus"/>.</summary>
public sealed class DatabaseStatus
{
    public DatabaseState State { get; init; }

    public string Message { get; init; } = string.Empty;

    public IReadOnlyList<string> MissingObjects { get; init; } = Array.Empty<string>();

    public Exception? Error { get; init; }
}

/// <summary>
/// Checks at start-up whether the configured database exists and is complete,
/// and - ONLY when the user confirms - creates it by running the scripts in the
/// Scripts folder (Tables.sql, StoredProcedures.sql and optionally SeedData.sql).
/// It never drops or overwrites existing data.
/// </summary>
public sealed class DatabaseInitializer
{
    public const string TablesScript = "Tables.sql";
    public const string StoredProceduresScript = "StoredProcedures.sql";
    public const string SeedDataScript = "SeedData.sql";

    private const int ScriptTimeoutSeconds = 120;

    private static readonly string[] RequiredTables = { "TransactionTypes", "Items", "StockTransactions" };
    private static readonly string[] RequiredViews = { "vw_ItemStock" };
    private static readonly string[] RequiredFunctions = { "fn_BuildContainsPattern" };
    private static readonly string[] RequiredTriggers = { "TR_StockTransactions_PreventNegativeStock", "TR_Items_PreventNegativeStock" };

    private static readonly Regex GoSeparator = new(@"^\s*GO\s*(?:--.*)?$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
    private static readonly Regex SafeDatabaseName = new(@"^[A-Za-z0-9_\-]{1,128}$", RegexOptions.Compiled);

    private readonly DatabaseConnection _database;
    private readonly string _scriptsDirectory;

    public DatabaseInitializer(DatabaseConnection database, string? scriptsDirectory = null)
    {
        _database = database ?? throw new ArgumentNullException(nameof(database));
        _scriptsDirectory = string.IsNullOrWhiteSpace(scriptsDirectory)
            ? Path.Combine(AppContext.BaseDirectory, "Scripts")
            : scriptsDirectory;
    }

    public string ScriptsDirectory => _scriptsDirectory;

    public string DatabaseName => _database.DatabaseName;

    public string DataSource => _database.DataSource;

    /// <summary>Connects to the server and reports what (if anything) is missing.</summary>
    public DatabaseStatus CheckStatus()
    {
        bool databaseExists;
        try
        {
            databaseExists = DatabaseExists();
        }
        catch (InventoryException ex)
        {
            AppLogger.Error($"Database server check failed for {_database.Description}.", ex.InnerException ?? ex);
            return new DatabaseStatus { State = DatabaseState.ServerUnavailable, Message = ex.Message, Error = ex };
        }

        if (!databaseExists)
        {
            return new DatabaseStatus
            {
                State = DatabaseState.DatabaseMissing,
                Message = $"The database '{_database.DatabaseName}' was not found on {_database.DataSource}.",
            };
        }

        try
        {
            IReadOnlyList<string> missing = FindMissingObjects();
            if (missing.Count > 0)
            {
                return new DatabaseStatus
                {
                    State = DatabaseState.SchemaIncomplete,
                    Message = $"The database '{_database.DatabaseName}' exists but {missing.Count} required object(s) are missing.",
                    MissingObjects = missing,
                };
            }
        }
        catch (InventoryException ex)
        {
            AppLogger.Error($"Schema check failed for {_database.Description}.", ex.InnerException ?? ex);
            return new DatabaseStatus { State = DatabaseState.ServerUnavailable, Message = ex.Message, Error = ex };
        }

        return new DatabaseStatus { State = DatabaseState.Ready, Message = "Database is ready." };
    }

    /// <summary>Creates the (empty) database named in the connection string. Equivalent to Scripts/Database.sql.</summary>
    public void CreateDatabase()
    {
        string name = _database.DatabaseName;
        if (!SafeDatabaseName.IsMatch(name))
        {
            throw new InventoryException(
                $"The database name '{name}' may only contain letters, digits, '_' and '-'. Create it manually with Scripts\\Database.sql.");
        }

        // Database names cannot be passed as parameters to CREATE DATABASE, so
        // QUOTENAME builds a safely delimited identifier on the server.
        // READ_COMMITTED_SNAPSHOT: readers do not wait for writers (same setting as Scripts/Database.sql).
        const string sql =
            "IF DB_ID(@Name) IS NULL " +
            "BEGIN " +
            "    DECLARE @Sql NVARCHAR(400) = N'CREATE DATABASE ' + QUOTENAME(@Name); EXEC (@Sql); " +
            "    SET @Sql = N'ALTER DATABASE ' + QUOTENAME(@Name) + N' SET READ_COMMITTED_SNAPSHOT ON'; EXEC (@Sql); " +
            "END";

        ExecuteText(_database.ForDatabase("master"), sql, command =>
            command.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 128) { Value = name }));

        AppLogger.Info($"Database '{name}' created on {_database.DataSource}.");
    }

    /// <summary>Runs Tables.sql and StoredProcedures.sql (both idempotent and non-destructive).</summary>
    public void InstallOrUpgradeSchema()
    {
        ExecuteScriptFile(TablesScript);
        ExecuteScriptFile(StoredProceduresScript);
        AppLogger.Info($"Database objects installed/updated in {_database.Description}.");
    }

    /// <summary>Runs SeedData.sql (optional demo data).</summary>
    public void LoadDemoData()
    {
        ExecuteScriptFile(SeedDataScript);
        AppLogger.Info($"Demo data loaded into {_database.Description}.");
    }

    /// <summary>Information for the Settings &gt; Database Information screen.</summary>
    public DatabaseInfo GetDatabaseInfo()
    {
        const string sql = @"
SELECT
    CAST(SERVERPROPERTY('ProductVersion') AS NVARCHAR(128)) AS ServerVersion,
    CAST(SERVERPROPERTY('Edition') AS NVARCHAR(128))        AS ServerEdition,
    (SELECT create_date FROM sys.databases WHERE name = DB_NAME()) AS DatabaseCreatedDate,
    (SELECT COUNT(*) FROM dbo.Items)                         AS ItemCount,
    (SELECT COUNT_BIG(*) FROM dbo.StockTransactions)         AS TransactionCount,
    (SELECT MAX(TransactionDate) FROM dbo.StockTransactions) AS LastTransactionDate;";

        try
        {
            using SqlConnection connection = _database.OpenConnection();
            using var command = new SqlCommand(sql, connection) { CommandTimeout = _database.CommandTimeoutSeconds };
            using SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);
            reader.Read();

            return new DatabaseInfo
            {
                DataSource = _database.DataSource,
                DatabaseName = _database.DatabaseName,
                AuthenticationMode = _database.UsesIntegratedSecurity ? "Windows authentication (Integrated Security)" : "SQL Server authentication",
                ServerVersion = reader.GetText("ServerVersion"),
                ServerEdition = reader.GetText("ServerEdition"),
                DatabaseCreatedDate = reader.GetNullableDate("DatabaseCreatedDate"),
                ItemCount = reader.GetInt("ItemCount"),
                TransactionCount = reader.GetLong("TransactionCount"),
                LastTransactionDate = reader.GetNullableDate("LastTransactionDate"),
            };
        }
        catch (SqlException ex)
        {
            throw SqlErrorTranslator.Translate(ex, "reading database information");
        }
    }

    /// <summary>
    /// Splits a script into batches on lines that contain only "GO"
    /// (the batch separator understood by SSMS and sqlcmd).
    /// </summary>
    internal static IReadOnlyList<string> SplitSqlBatches(string script)
    {
        var batches = new List<string>();
        var current = new StringBuilder();

        using var reader = new StringReader(script);
        string? line;
        while ((line = reader.ReadLine()) is not null)
        {
            if (GoSeparator.IsMatch(line))
            {
                AddBatch(batches, current);
                continue;
            }

            current.AppendLine(line);
        }

        AddBatch(batches, current);
        return batches;
    }

    private static void AddBatch(List<string> batches, StringBuilder current)
    {
        string batch = current.ToString();
        if (!string.IsNullOrWhiteSpace(batch))
        {
            batches.Add(batch);
        }

        current.Clear();
    }

    private bool DatabaseExists()
    {
        bool exists = false;
        ExecuteText(
            _database.ForDatabase("master"),
            "SELECT CASE WHEN DB_ID(@Name) IS NULL THEN 0 ELSE 1 END;",
            command => command.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 128) { Value = _database.DatabaseName }),
            command => exists = Convert.ToInt32(command.ExecuteScalar()) == 1);
        return exists;
    }

    private IReadOnlyList<string> FindMissingObjects()
    {
        var existing = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        ExecuteText(
            _database,
            "SELECT s.name + '.' + o.name FROM sys.objects AS o INNER JOIN sys.schemas AS s ON s.schema_id = o.schema_id WHERE o.type IN ('U', 'V', 'P', 'FN', 'TR');",
            configure: null,
            run: command =>
            {
                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    existing.Add(reader.GetString(0));
                }
            });

        IEnumerable<string> required = RequiredTables
            .Concat(RequiredViews)
            .Concat(RequiredFunctions)
            .Concat(RequiredTriggers)
            .Select(name => "dbo." + name)
            .Concat(StoredProcedures.All);

        return required.Where(name => !existing.Contains(name)).ToList();
    }

    private void ExecuteScriptFile(string fileName)
    {
        string path = Path.Combine(_scriptsDirectory, fileName);
        if (!File.Exists(path))
        {
            throw new InventoryException($"The setup script '{path}' was not found. Reinstall the application or run the scripts manually (see README.md).");
        }

        IReadOnlyList<string> batches = SplitSqlBatches(File.ReadAllText(path));
        AppLogger.Info($"Running {fileName} ({batches.Count} batches) against {_database.Description}.");

        try
        {
            using SqlConnection connection = _database.OpenConnection();
            connection.InfoMessage += (_, e) => AppLogger.Info($"[{fileName}] {e.Message}");

            foreach (string batch in batches)
            {
                using var command = new SqlCommand(batch, connection) { CommandTimeout = ScriptTimeoutSeconds };
                command.ExecuteNonQuery();
            }
        }
        catch (SqlException ex)
        {
            AppLogger.Error($"Setup script {fileName} failed.", ex);
            throw new DataAccessException($"The setup script {fileName} failed. Technical details were written to the application log ({AppLogger.LogFilePath}).", ex);
        }
    }

    private static void ExecuteText(
        DatabaseConnection target,
        string sql,
        Action<SqlCommand>? configure,
        Action<SqlCommand>? run = null)
    {
        try
        {
            using SqlConnection connection = target.OpenConnection();
            using var command = new SqlCommand(sql, connection) { CommandTimeout = target.CommandTimeoutSeconds };
            configure?.Invoke(command);

            if (run is null)
            {
                command.ExecuteNonQuery();
            }
            else
            {
                run(command);
            }
        }
        catch (SqlException ex)
        {
            throw SqlErrorTranslator.Translate(ex, "checking the database");
        }
    }
}
