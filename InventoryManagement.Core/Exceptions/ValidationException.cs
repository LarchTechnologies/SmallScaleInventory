namespace InventoryManagement.Exceptions;

/// <summary>Invalid user input (required field missing, negative quantity, bad date range ...).</summary>
public class ValidationException : InventoryException
{
    public ValidationException(string userMessage, string? fieldName = null, Exception? innerException = null)
        : base(userMessage, innerException)
    {
        FieldName = fieldName;
    }

    /// <summary>Optional name of the offending field so the UI can focus it.</summary>
    public string? FieldName { get; }
}
