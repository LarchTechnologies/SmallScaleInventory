namespace InventoryManagement.Exceptions;

/// <summary>
/// An unexpected database error (missing stored procedure, timeout, constraint
/// violation that was not anticipated ...). The user sees a generic message;
/// the original SqlException is kept as InnerException for the log.
/// </summary>
public sealed class DataAccessException : InventoryException
{
    public const string DefaultMessage =
        "The operation could not be completed because of a database error. Technical details were written to the application log.";

    public DataAccessException(string? userMessage = null, Exception? innerException = null)
        : base(string.IsNullOrWhiteSpace(userMessage) ? DefaultMessage : userMessage, innerException)
    {
    }
}
