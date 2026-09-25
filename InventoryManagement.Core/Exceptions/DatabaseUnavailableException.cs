namespace InventoryManagement.Exceptions;

/// <summary>SQL Server LocalDB cannot be reached, the database cannot be opened or login failed.</summary>
public sealed class DatabaseUnavailableException : InventoryException
{
    public const string DefaultMessage =
        "Unable to connect to the inventory database. Please verify SQL Server LocalDB is running.";

    public DatabaseUnavailableException(string? userMessage = null, Exception? innerException = null)
        : base(string.IsNullOrWhiteSpace(userMessage) ? DefaultMessage : userMessage, innerException)
    {
    }
}
