namespace InventoryManagement.Exceptions;

/// <summary>An item with the same item code already exists.</summary>
public sealed class DuplicateItemCodeException : ValidationException
{
    public const string DefaultMessage = "Item code already exists.";

    public DuplicateItemCodeException(string? userMessage = null, Exception? innerException = null)
        : base(string.IsNullOrWhiteSpace(userMessage) ? DefaultMessage : userMessage, "ItemCode", innerException)
    {
    }
}
