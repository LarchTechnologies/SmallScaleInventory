namespace InventoryManagement.Helpers;

/// <summary>Display formats used across all screens and printouts.</summary>
public static class UiFormats
{
    /// <summary>Quantities: thousands separator, always two decimals (1,250.50).</summary>
    public const string Quantity = "N2";

    public const string Date = "dd-MMM-yyyy";

    public const string DateTime = "dd-MMM-yyyy HH:mm";

    public static string FormatQuantity(decimal value, string? unit = null) =>
        string.IsNullOrWhiteSpace(unit) ? value.ToString(Quantity) : $"{value.ToString(Quantity)} {unit}";
}
