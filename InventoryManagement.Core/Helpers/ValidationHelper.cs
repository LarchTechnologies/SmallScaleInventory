using System.Globalization;
using InventoryManagement.Exceptions;

namespace InventoryManagement.Helpers;

/// <summary>
/// Business validation rules shared by all services.
/// Every method throws a <see cref="ValidationException"/> with a message that
/// can be shown to the user. The same rules are enforced again by the stored
/// procedures and table constraints, so the UI is never the only safeguard.
/// </summary>
public static class ValidationHelper
{
    /// <summary>Quantities are stored as DECIMAL(18,2).</summary>
    public const int QuantityDecimalPlaces = 2;

    /// <summary>Upper limit for a single quantity (keeps totals far away from DECIMAL(18,2) overflow).</summary>
    public const decimal MaximumQuantity = 999_999_999.99m;

    public const string InvalidQuantityMessage = "Please enter a valid quantity.";

    /// <summary>Oldest transaction / report date accepted (matches the stored procedures).</summary>
    public static readonly DateTime MinimumDate = new(2000, 1, 1);

    /// <summary>Latest report date accepted.</summary>
    public static readonly DateTime MaximumDate = new(2099, 12, 31);

    /// <summary>Returns the trimmed text or throws when it is empty or too long.</summary>
    public static string RequireText(string? value, string fieldLabel, int maxLength, string? fieldName = null)
    {
        string trimmed = value?.Trim() ?? string.Empty;

        if (trimmed.Length == 0)
        {
            throw new ValidationException($"{fieldLabel} is required.", fieldName);
        }

        EnsureMaxLength(trimmed, fieldLabel, maxLength, fieldName);
        return trimmed;
    }

    /// <summary>Returns the trimmed text, or null when empty. Throws when too long.</summary>
    public static string? OptionalText(string? value, string fieldLabel, int maxLength, string? fieldName = null)
    {
        string trimmed = value?.Trim() ?? string.Empty;

        if (trimmed.Length == 0)
        {
            return null;
        }

        EnsureMaxLength(trimmed, fieldLabel, maxLength, fieldName);
        return trimmed;
    }

    /// <summary>Value must be zero or more, with at most 2 decimals.</summary>
    public static void RequireNonNegative(decimal value, string fieldLabel, string? fieldName = null)
    {
        if (value < 0)
        {
            throw new ValidationException($"{fieldLabel} cannot be negative.", fieldName);
        }

        if (value > MaximumQuantity)
        {
            throw new ValidationException($"{fieldLabel} is too large.", fieldName);
        }

        RequireDecimalPlaces(value, fieldLabel, fieldName);
    }

    /// <summary>Quantity of a stock movement: greater than zero, at most 2 decimals.</summary>
    public static void RequireValidQuantity(decimal quantity, string fieldName = "Quantity")
    {
        if (quantity <= 0)
        {
            throw new ValidationException($"{InvalidQuantityMessage} Quantity must be greater than zero.", fieldName);
        }

        if (quantity > MaximumQuantity)
        {
            throw new ValidationException($"{InvalidQuantityMessage} Quantity is too large.", fieldName);
        }

        RequireDecimalPlaces(quantity, "Quantity", fieldName);
    }

    public static void RequireDecimalPlaces(decimal value, string fieldLabel, string? fieldName = null)
    {
        if (decimal.Round(value, QuantityDecimalPlaces) != value)
        {
            throw new ValidationException($"{fieldLabel} can have at most {QuantityDecimalPlaces} decimal places.", fieldName);
        }
    }

    /// <summary>A stock transaction date: not before 2000-01-01 and not in the future.</summary>
    public static void RequireValidTransactionDate(DateTime transactionDate, string fieldName = "TransactionDate")
    {
        if (transactionDate == default || transactionDate < MinimumDate)
        {
            throw new ValidationException("Please enter a valid transaction date.", fieldName);
        }

        if (transactionDate.Date > DateTime.Today)
        {
            throw new ValidationException("Transaction date cannot be in the future.", fieldName);
        }
    }

    /// <summary>A report date range: both dates valid and From &lt;= To.</summary>
    public static void RequireValidDateRange(DateTime fromDate, DateTime toDate)
    {
        if (fromDate == default || fromDate < MinimumDate || fromDate > MaximumDate)
        {
            throw new ValidationException("Please enter a valid From Date.", "FromDate");
        }

        if (toDate == default || toDate < MinimumDate || toDate > MaximumDate)
        {
            throw new ValidationException("Please enter a valid To Date.", "ToDate");
        }

        if (fromDate.Date > toDate.Date)
        {
            throw new ValidationException("From Date cannot be later than To Date.", "FromDate");
        }
    }

    /// <summary>
    /// Parses a number typed by the user. Accepts the current culture format
    /// (e.g. "1,250.50") and falls back to the invariant format ("1250.50").
    /// </summary>
    public static bool TryParseDecimal(string? text, out decimal value)
    {
        value = 0m;
        if (string.IsNullOrWhiteSpace(text))
        {
            return false;
        }

        string trimmed = text.Trim();
        return decimal.TryParse(trimmed, NumberStyles.Number, CultureInfo.CurrentCulture, out value)
            || decimal.TryParse(trimmed, NumberStyles.Number, CultureInfo.InvariantCulture, out value);
    }

    private static void EnsureMaxLength(string value, string fieldLabel, int maxLength, string? fieldName)
    {
        if (value.Length > maxLength)
        {
            throw new ValidationException($"{fieldLabel} cannot be longer than {maxLength} characters.", fieldName);
        }
    }
}
