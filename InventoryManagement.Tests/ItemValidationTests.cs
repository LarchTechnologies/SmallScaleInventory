using InventoryManagement.Exceptions;
using InventoryManagement.Models;
using InventoryManagement.Services;

namespace InventoryManagement.Tests;

public class ItemValidationTests
{
    private static Item ValidItem() => new()
    {
        ItemCode = " rm-001 ",
        ItemName = "  Raw Material A ",
        Category = "  ",
        Unit = " kg",
        MinimumStock = 100m,
        OpeningStock = 500m,
    };

    [Fact]
    public void Normalise_TrimsAndUppercasesCodeAndUnit()
    {
        Item item = ValidItem();

        ItemService.NormaliseAndValidate(item);

        Assert.Equal("RM-001", item.ItemCode);
        Assert.Equal("Raw Material A", item.ItemName);
        Assert.Null(item.Category);
        Assert.Equal("KG", item.Unit);
    }

    [Theory]
    [InlineData(nameof(Item.ItemCode))]
    [InlineData(nameof(Item.ItemName))]
    [InlineData(nameof(Item.Unit))]
    public void RequiredField_Missing_ThrowsWithFieldName(string field)
    {
        Item item = ValidItem();
        typeof(Item).GetProperty(field)!.SetValue(item, " ");

        var ex = Assert.Throws<ValidationException>(() => ItemService.NormaliseAndValidate(item));
        Assert.Equal(field, ex.FieldName);
    }

    [Fact]
    public void NegativeMinimumStock_Throws()
    {
        Item item = ValidItem();
        item.MinimumStock = -1m;

        var ex = Assert.Throws<ValidationException>(() => ItemService.NormaliseAndValidate(item));
        Assert.Equal(nameof(Item.MinimumStock), ex.FieldName);
    }

    [Fact]
    public void NegativeOpeningStock_Throws()
    {
        Item item = ValidItem();
        item.OpeningStock = -0.5m;

        var ex = Assert.Throws<ValidationException>(() => ItemService.NormaliseAndValidate(item));
        Assert.Equal(nameof(Item.OpeningStock), ex.FieldName);
    }

    [Fact]
    public void ItemCodeLongerThanColumn_Throws()
    {
        Item item = ValidItem();
        item.ItemCode = new string('A', FieldLengths.ItemCode + 1);

        Assert.Throws<ValidationException>(() => ItemService.NormaliseAndValidate(item));
    }

    [Fact]
    public void DuplicateItemCodeException_HasFriendlyMessage()
    {
        var ex = new DuplicateItemCodeException();
        Assert.Equal("Item code already exists.", ex.Message);
        Assert.Equal(nameof(Item.ItemCode), ex.FieldName);
    }
}
