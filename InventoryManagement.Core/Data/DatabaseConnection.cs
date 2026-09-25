using System.Configuration;
using InventoryManagement.Exceptions;
using Microsoft.Data.SqlClient;

namespace InventoryManagement.Data;

/// <summary>
/// Holds the SQL Server connection settings and opens connections.
/// The connection string is read ONCE from App.config (&lt;connectionStrings&gt;
/// entry "InventoryDb"); no other class knows the connection string.
/// </summary>
public sealed class DatabaseConnection
{
    /// <summary>Name of the connection string entry in App.config.</summary>
    public const string DefaultConnectionStringName = "InventoryDb";

    /// <summary>Optional App.config appSettings key for the SQL command timeout.</summary>
    public const string CommandTimeoutSettingKey = "CommandTimeoutSeconds";

    private const int DefaultCommandTimeoutSeconds = 30;

    public DatabaseConnection(string connectionString, int commandTimeoutSeconds = DefaultCommandTimeoutSeconds)
    {
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new ArgumentException("A connection string is required.", nameof(connectionString));
        }

        // SqlConnectionStringBuilder validates the syntax and exposes the parts we display.
        var builder = new SqlConnectionStringBuilder(connectionString);

        ConnectionString = builder.ConnectionString;
        DataSource = builder.DataSource;
        DatabaseName = builder.InitialCatalog;
        UsesIntegratedSecurity = builder.IntegratedSecurity;
        CommandTimeoutSeconds = commandTimeoutSeconds > 0 ? commandTimeoutSeconds : DefaultCommandTimeoutSeconds;
    }

    public string ConnectionString { get; }

    /// <summary>Server / instance, e.g. (localdb)\MSSQLLocalDB.</summary>
    public string DataSource { get; }

    /// <summary>Database (Initial Catalog), e.g. InventoryManagementDB.</summary>
    public string DatabaseName { get; }

    public bool UsesIntegratedSecurity { get; }

    public int CommandTimeoutSeconds { get; }

    /// <summary>Server and database only - safe to show and to log (no credentials).</summary>
    public string Description => $"{DataSource} / {DatabaseName}";

    /// <summary>Creates the connection from the "InventoryDb" entry in App.config.</summary>
    public static DatabaseConnection FromConfiguration(string connectionStringName = DefaultConnectionStringName)
    {
        ConnectionStringSettings? settings = ConfigurationManager.ConnectionStrings[connectionStringName];

        if (settings is null || string.IsNullOrWhiteSpace(settings.ConnectionString))
        {
            throw new InventoryException(
                $"The connection string '{connectionStringName}' was not found in App.config. " +
                "Add it to the <connectionStrings> section (see README.md, section 13 'Connection string and settings').");
        }

        int timeout = int.TryParse(ConfigurationManager.AppSettings[CommandTimeoutSettingKey], out int configured)
            ? configured
            : DefaultCommandTimeoutSeconds;

        try
        {
            var connection = new DatabaseConnection(settings.ConnectionString, timeout);

            if (string.IsNullOrWhiteSpace(connection.DatabaseName))
            {
                throw new InventoryException(
                    $"The connection string '{connectionStringName}' must specify the database name (Initial Catalog=InventoryManagementDB).");
            }

            return connection;
        }
        catch (ArgumentException ex)
        {
            throw new InventoryException($"The connection string '{connectionStringName}' in App.config is not valid.", ex);
        }
    }

    /// <summary>Returns a copy of these settings pointing at another database (used for "master").</summary>
    public DatabaseConnection ForDatabase(string databaseName)
    {
        var builder = new SqlConnectionStringBuilder(ConnectionString) { InitialCatalog = databaseName };
        return new DatabaseConnection(builder.ConnectionString, CommandTimeoutSeconds);
    }

    /// <summary>
    /// Opens a new connection. Callers must dispose it (use a using statement).
    /// Connection failures are translated into a friendly <see cref="InventoryException"/>.
    /// </summary>
    public SqlConnection OpenConnection()
    {
        var connection = new SqlConnection(ConnectionString);
        try
        {
            connection.Open();
            return connection;
        }
        catch (SqlException ex)
        {
            connection.Dispose();
            throw SqlErrorTranslator.Translate(ex, $"opening connection to {Description}");
        }
        catch (InvalidOperationException ex)
        {
            connection.Dispose();
            throw new DatabaseUnavailableException(null, ex);
        }
    }
}
