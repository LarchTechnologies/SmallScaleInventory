using System.Data;
using Microsoft.Data.SqlClient;

namespace InventoryManagement.Data;

/// <summary>
/// The one place that opens connections, creates commands and executes stored
/// procedures. Repositories use it so that no ADO.NET plumbing is duplicated
/// and every connection, command and reader is disposed correctly.
/// </summary>
internal sealed class StoredProcedureExecutor
{
    private readonly DatabaseConnection _database;

    public StoredProcedureExecutor(DatabaseConnection database)
    {
        _database = database ?? throw new ArgumentNullException(nameof(database));
    }

    /// <summary>Executes a procedure and maps every row of the first result set.</summary>
    public List<T> Query<T>(string procedureName, Func<SqlDataReader, T> map, params SqlParameter[] parameters)
    {
        return Run(procedureName, parameters, command =>
        {
            var rows = new List<T>();
            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                rows.Add(map(reader));
            }

            return rows;
        });
    }

    /// <summary>Executes a procedure and maps the first row, or returns default when there is none.</summary>
    public T? QuerySingleOrDefault<T>(string procedureName, Func<SqlDataReader, T> map, params SqlParameter[] parameters)
    {
        return Run(procedureName, parameters, command =>
        {
            using SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);
            return reader.Read() ? map(reader) : default;
        });
    }

    /// <summary>Executes a procedure that returns no rows. Output parameters are filled in afterwards.</summary>
    public void Execute(string procedureName, params SqlParameter[] parameters)
    {
        Run(procedureName, parameters, command => command.ExecuteNonQuery());
    }

    private TResult Run<TResult>(string procedureName, SqlParameter[] parameters, Func<SqlCommand, TResult> action)
    {
        using SqlConnection connection = _database.OpenConnection();
        using var command = new SqlCommand(procedureName, connection)
        {
            CommandType = CommandType.StoredProcedure,
            CommandTimeout = _database.CommandTimeoutSeconds,
        };
        command.Parameters.AddRange(parameters);

        try
        {
            return action(command);
        }
        catch (SqlException ex)
        {
            throw SqlErrorTranslator.Translate(ex, $"executing {procedureName}");
        }
    }
}
