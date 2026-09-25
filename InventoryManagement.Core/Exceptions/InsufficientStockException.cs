namespace InventoryManagement.Exceptions;

/// <summary>A Stock OUT (or item change) would make stock negative.</summary>
public sealed class InsufficientStockException : ValidationException
{
    public InsufficientStockException(string userMessage, Exception? innerException = null)
        : base(userMessage, "Quantity", innerException)
    {
    }

    public static InsufficientStockException ForAvailable(decimal availableQuantity, string? unit = null)
    {
        string suffix = string.IsNullOrWhiteSpace(unit) ? string.Empty : " " + unit;
        return new InsufficientStockException($"Insufficient stock. Available quantity: {availableQuantity:N2}{suffix}");
    }
}
