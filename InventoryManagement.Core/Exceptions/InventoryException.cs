namespace InventoryManagement.Exceptions;

/// <summary>
/// Base class for all expected application errors.
/// The <see cref="Exception.Message"/> of an InventoryException is always
/// written for the end user and is safe to show in a message box.
/// Technical details live in <see cref="Exception.InnerException"/> and go to the log.
/// </summary>
public class InventoryException : Exception
{
    public InventoryException(string userMessage)
        : base(userMessage)
    {
    }

    public InventoryException(string userMessage, Exception? innerException)
        : base(userMessage, innerException)
    {
    }
}
