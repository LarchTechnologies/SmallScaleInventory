using InventoryManagement.Exceptions;
using InventoryManagement.Logging;
using Microsoft.Data.SqlClient;

namespace InventoryManagement.Data;

/// <summary>
/// Converts a <see cref="SqlException"/> into a friendly <see cref="InventoryException"/>.
/// SQL exception text is never shown to the user; unexpected errors are logged
/// with full technical details.
/// </summary>
internal static class SqlErrorTranslator
{
    public static InventoryException Translate(SqlException exception, string operation)
    {
        int number = exception.Number;

        // Business rules raised on purpose by the stored procedures: their
        // messages are written for end users, so they are shown as-is.
        if (number is >= DatabaseErrorNumbers.FirstBusinessError and <= DatabaseErrorNumbers.LastBusinessError)
        {
            return TranslateBusinessError(exception);
        }

        InventoryException translated = number switch
        {
            DatabaseErrorNumbers.CannotOpenDatabase => new DatabaseUnavailableException(
                "The inventory database could not be opened. It may not have been created yet - see README.md, section 14 'Setup for beginners'.",
                exception),
            DatabaseErrorNumbers.LoginFailed => new DatabaseUnavailableException(
                "Login to the inventory database failed. Please check the connection string in App.config.",
                exception),
            DatabaseErrorNumbers.Timeout => new DataAccessException(
                "The database did not respond in time. Please try again.",
                exception),
            DatabaseErrorNumbers.Deadlock => new DataAccessException(
                "The database was busy with another operation. Please try again.",
                exception),
            DatabaseErrorNumbers.StoredProcedureNotFound or DatabaseErrorNumbers.InvalidObjectName => new DataAccessException(
                "A required database object is missing. Run Scripts\\Tables.sql and Scripts\\StoredProcedures.sql (see README.md, section 20 'Troubleshooting').",
                exception),
            DatabaseErrorNumbers.UniqueIndexViolation or DatabaseErrorNumbers.UniqueConstraintViolation =>
                new DuplicateItemCodeException(null, exception),
            DatabaseErrorNumbers.ConstraintViolation => new ValidationException(
                "The data entered breaks a database rule (for example a quantity that is not greater than zero).",
                null,
                exception),
            DatabaseErrorNumbers.ArithmeticOverflow => new ValidationException(
                "A number entered is too large.",
                null,
                exception),
            _ when IsConnectionError(exception) => new DatabaseUnavailableException(null, exception),
            _ => new DataAccessException(null, exception),
        };

        if (translated is DatabaseUnavailableException or DataAccessException)
        {
            AppLogger.Error($"Database error {number} while {operation}.", exception);
        }

        return translated;
    }

    private static InventoryException TranslateBusinessError(SqlException exception)
    {
        string message = exception.Message;

        return exception.Number switch
        {
            DatabaseErrorNumbers.DuplicateItemCode => new DuplicateItemCodeException(message, exception),
            DatabaseErrorNumbers.ItemNotFound => new RecordNotFoundException(message, exception),
            DatabaseErrorNumbers.InsufficientStock
                or DatabaseErrorNumbers.InsufficientStockTrigger
                or DatabaseErrorNumbers.OpeningStockBelowIssued => new InsufficientStockException(message, exception),
            DatabaseErrorNumbers.InvalidQuantity => new ValidationException(message, "Quantity", exception),
            DatabaseErrorNumbers.InvalidTransactionDate => new ValidationException(message, "TransactionDate", exception),
            _ => new ValidationException(message, null, exception),
        };
    }

    private static bool IsConnectionError(SqlException exception)
    {
        if (DatabaseErrorNumbers.ConnectionErrors.Contains(exception.Number))
        {
            return true;
        }

        string text = exception.Message;
        return text.Contains("network-related", StringComparison.OrdinalIgnoreCase)
            || text.Contains("Local Database Runtime", StringComparison.OrdinalIgnoreCase)
            || text.Contains("server was not found", StringComparison.OrdinalIgnoreCase);
    }
}
