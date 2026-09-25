namespace InventoryManagement.Exceptions;

/// <summary>The requested record (for example an item) no longer exists.</summary>
public sealed class RecordNotFoundException : InventoryException
{
    public RecordNotFoundException(string userMessage, Exception? innerException = null)
        : base(userMessage, innerException)
    {
    }
}
