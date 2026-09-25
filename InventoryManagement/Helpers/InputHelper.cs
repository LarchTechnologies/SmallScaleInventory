using System.Globalization;

namespace InventoryManagement.Helpers;

/// <summary>Keyboard / input helpers for text boxes.</summary>
public static class InputHelper
{
    /// <summary>
    /// Restricts a text box to digits, one decimal separator and the group
    /// separator. The value is validated again when it is parsed and by the service.
    /// </summary>
    public static void AllowDecimalOnly(TextBox textBox)
    {
        textBox.KeyPress += (_, e) =>
        {
            if (char.IsControl(e.KeyChar) || char.IsDigit(e.KeyChar))
            {
                return;
            }

            NumberFormatInfo format = CultureInfo.CurrentCulture.NumberFormat;
            string key = e.KeyChar.ToString();

            bool isDecimalSeparator = key == format.NumberDecimalSeparator || e.KeyChar == '.';
            if (isDecimalSeparator && !textBox.Text.Contains(format.NumberDecimalSeparator) && !textBox.Text.Contains('.'))
            {
                return;
            }

            if (key == format.NumberGroupSeparator)
            {
                return;
            }

            e.Handled = true;
        };
    }

    /// <summary>Parses a decimal from a text box. Returns false when the text is empty or not a number.</summary>
    public static bool TryGetDecimal(TextBox textBox, out decimal value) =>
        ValidationHelper.TryParseDecimal(textBox.Text, out value);

    /// <summary>Formats a quantity for editing (no thousands separator, e.g. 1250.5 -> "1250.50").</summary>
    public static string FormatForEditing(decimal value) =>
        value.ToString("0.00", CultureInfo.CurrentCulture);
}
