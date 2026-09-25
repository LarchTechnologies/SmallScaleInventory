using InventoryManagement.Data;
using InventoryManagement.Exceptions;
using InventoryManagement.Helpers;
using InventoryManagement.Logging;
using InventoryManagement.Models;

namespace InventoryManagement.Services;

/// <summary>Business logic for the item master: validation, normalisation and persistence.</summary>
public sealed class ItemService
{
    /// <summary>Common units offered in the Unit drop-down in addition to units already used.</summary>
    public static readonly IReadOnlyList<string> DefaultUnits = new[]
    {
        "NOS", "PCS", "KG", "G", "LTR", "ML", "MTR", "BOX", "SET", "PKT", "ROLL", "BAG",
    };

    private readonly ItemRepository _itemRepository;

    public ItemService(ItemRepository itemRepository)
    {
        _itemRepository = itemRepository ?? throw new ArgumentNullException(nameof(itemRepository));
    }

    public IReadOnlyList<Item> SearchItems(string? searchText, bool includeInactive) =>
        _itemRepository.GetItems(ValidationHelper.OptionalText(searchText, "Search text", FieldLengths.SearchText), includeInactive);

    /// <summary>Active items only - used for Stock IN / Stock OUT selection.</summary>
    public IReadOnlyList<Item> GetActiveItems() => _itemRepository.GetItems(null, includeInactive: false);

    /// <summary>All items including inactive ones - used for report filters.</summary>
    public IReadOnlyList<Item> GetAllItems() => _itemRepository.GetItems(null, includeInactive: true);

    public Item GetItem(int itemId) =>
        _itemRepository.GetById(itemId) ?? throw new RecordNotFoundException("The selected item no longer exists.");

    public IReadOnlyList<string> GetCategories() => _itemRepository.GetCategories();

    /// <summary>Units already used by items plus the common default units, sorted.</summary>
    public IReadOnlyList<string> GetUnits() =>
        DefaultUnits
            .Concat(_itemRepository.GetUnits())
            .Where(u => !string.IsNullOrWhiteSpace(u))
            .Select(u => u.Trim().ToUpperInvariant())
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .OrderBy(u => u, StringComparer.OrdinalIgnoreCase)
            .ToList();

    /// <summary>Validates and inserts a new item. Returns the new ItemId.</summary>
    public int CreateItem(Item item)
    {
        ArgumentNullException.ThrowIfNull(item);

        NormaliseAndValidate(item);
        int itemId = _itemRepository.Insert(item);
        item.ItemId = itemId;

        AppLogger.Info($"Item created: {item.ItemCode} (Id {itemId}).");
        return itemId;
    }

    /// <summary>Validates and updates an existing item.</summary>
    public void UpdateItem(Item item)
    {
        ArgumentNullException.ThrowIfNull(item);

        if (item.ItemId <= 0)
        {
            throw new ValidationException("Please select an item to edit.");
        }

        NormaliseAndValidate(item);
        _itemRepository.Update(item);

        AppLogger.Info($"Item updated: {item.ItemCode} (Id {item.ItemId}).");
    }

    /// <summary>Activates or deactivates an item. Inactive items cannot receive stock postings.</summary>
    public void SetItemActive(int itemId, bool isActive)
    {
        if (itemId <= 0)
        {
            throw new ValidationException("Please select an item first.");
        }

        _itemRepository.SetActive(itemId, isActive);
        AppLogger.Info($"Item Id {itemId} {(isActive ? "activated" : "deactivated")}.");
    }

    /// <summary>
    /// Trims text, upper-cases code and unit, and applies all item rules.
    /// Public static so the rules can be unit tested without a database.
    /// </summary>
    public static void NormaliseAndValidate(Item item)
    {
        item.ItemCode = ValidationHelper.RequireText(item.ItemCode, "Item code", FieldLengths.ItemCode, nameof(Item.ItemCode))
            .ToUpperInvariant();
        item.ItemName = ValidationHelper.RequireText(item.ItemName, "Item name", FieldLengths.ItemName, nameof(Item.ItemName));
        item.Category = ValidationHelper.OptionalText(item.Category, "Category", FieldLengths.Category, nameof(Item.Category));
        item.Unit = ValidationHelper.RequireText(item.Unit, "Unit", FieldLengths.Unit, nameof(Item.Unit))
            .ToUpperInvariant();

        ValidationHelper.RequireNonNegative(item.MinimumStock, "Minimum stock", nameof(Item.MinimumStock));
        ValidationHelper.RequireNonNegative(item.OpeningStock, "Opening stock", nameof(Item.OpeningStock));
    }
}
