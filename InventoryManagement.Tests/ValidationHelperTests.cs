using InventoryManagement.Exceptions;
using InventoryManagement.Helpers;

namespace InventoryManagement.Tests;

public class ValidationHelperTests
{
    [Fact]
    public void RequireText_TrimsValue()
    {
        Assert.Equal("RM-001", ValidationHelper.RequireText("  RM-001 ", "Item code", 50));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void RequireText_EmptyValue_Throws(string? value)
    {
        var ex = Assert.Throws<ValidationException>(() => ValidationHelper.RequireText(value, "Item code", 50, "ItemCode"));
        Assert.Equal("Item code is required.", ex.Message);
        Assert.Equal("ItemCode", ex.FieldName);
    }

    [Fact]
    public void RequireText_TooLong_Throws()
    {
        Assert.Throws<ValidationException>(() => ValidationHelper.RequireText(new string('X', 51), "Item code", 50));
    }

    [Fact]
    public void OptionalText_Empty_ReturnsNull()
    {
        Assert.Null(ValidationHelper.OptionalText("   ", "Category", 100));
        Assert.Equal("Raw", ValidationHelper.OptionalText(" Raw ", "Category", 100));
    }

    [Theory]
    [InlineData("0")]
    [InlineData("100")]
    [InlineData("12.50")]
    public void RequireNonNegative_ValidValues_DoNotThrow(string value)
    {
        ValidationHelper.RequireNonNegative(decimal.Parse(value, System.Globalization.CultureInfo.InvariantCulture), "Minimum stock");
    }

    [Theory]
    [InlineData("-0.01")]
    [InlineData("-5")]
    [InlineData("1.005")]
    public void RequireNonNegative_InvalidValues_Throw(string value)
    {
        Assert.Throws<ValidationException>(() =>
            ValidationHelper.RequireNonNegative(decimal.Parse(value, System.Globalization.CultureInfo.InvariantCulture), "Minimum stock"));
    }

    [Theory]
    [InlineData("0")]
    [InlineData("-1")]
    [InlineData("0.001")]
    [InlineData("1000000000")]
    public void RequireValidQuantity_InvalidQuantity_Throws(string value)
    {
        var ex = Assert.Throws<ValidationException>(() =>
            ValidationHelper.RequireValidQuantity(decimal.Parse(value, System.Globalization.CultureInfo.InvariantCulture)));
        Assert.Equal("Quantity", ex.FieldName);
    }

    [Theory]
    [InlineData("0.01")]
    [InlineData("1")]
    [InlineData("250.75")]
    public void RequireValidQuantity_ValidQuantity_DoesNotThrow(string value)
    {
        ValidationHelper.RequireValidQuantity(decimal.Parse(value, System.Globalization.CultureInfo.InvariantCulture));
    }

    [Fact]
    public void RequireValidTransactionDate_Today_IsValid()
    {
        ValidationHelper.RequireValidTransactionDate(DateTime.Today);
        ValidationHelper.RequireValidTransactionDate(DateTime.Now);
    }

    [Fact]
    public void RequireValidTransactionDate_Future_Throws()
    {
        var ex = Assert.Throws<ValidationException>(() => ValidationHelper.RequireValidTransactionDate(DateTime.Today.AddDays(1)));
        Assert.Contains("future", ex.Message);
    }

    [Fact]
    public void RequireValidTransactionDate_TooOld_Throws()
    {
        Assert.Throws<ValidationException>(() => ValidationHelper.RequireValidTransactionDate(new DateTime(1999, 12, 31)));
        Assert.Throws<ValidationException>(() => ValidationHelper.RequireValidTransactionDate(default));
    }

    [Fact]
    public void RequireValidDateRange_FromAfterTo_Throws()
    {
        var ex = Assert.Throws<ValidationException>(() =>
            ValidationHelper.RequireValidDateRange(new DateTime(2025, 2, 1), new DateTime(2025, 1, 31)));
        Assert.Equal("From Date cannot be later than To Date.", ex.Message);
    }

    [Fact]
    public void RequireValidDateRange_SameDay_IsValid()
    {
        ValidationHelper.RequireValidDateRange(new DateTime(2025, 1, 31), new DateTime(2025, 1, 31));
    }

    [Theory]
    [InlineData("1250.50", 1250.50)]
    [InlineData("1,250.50", 1250.50)]
    [InlineData(" 7 ", 7)]
    public void TryParseDecimal_ValidText_Parses(string text, double expected)
    {
        using var culture = new TestCulture();
        Assert.True(ValidationHelper.TryParseDecimal(text, out decimal value));
        Assert.Equal((decimal)expected, value);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("abc")]
    [InlineData("12..5")]
    public void TryParseDecimal_InvalidText_ReturnsFalse(string? text)
    {
        using var culture = new TestCulture();
        Assert.False(ValidationHelper.TryParseDecimal(text, out _));
    }
}
