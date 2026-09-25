using InventoryManagement.Models;
using Microsoft.Data.SqlClient;

namespace InventoryManagement.Data;

/// <summary>Database access for the item master (dbo.Items) via stored procedures.</summary>
public sealed class ItemRepository
{
    private readonly StoredProcedureExecutor _executor;

    public ItemRepository(DatabaseConnection database)
    {
        _executor = new StoredProcedureExecutor(database);
    }

    /// <summary>Items with their current stock, optionally filtered by code / name / category text.</summary>
    public IReadOnlyList<Item> GetItems(string? searchText, bool includeInactive)
    {
        return _executor.Query(
            StoredProcedures.ItemGetAll,
            RowMappers.MapItem,
            DbParameters.NVarChar("@SearchText", searchText, FieldLengths.SearchText),
            DbParameters.Bit("@IncludeInactive", includeInactive));
    }

    public Item? GetById(int itemId)
    {
        return _executor.QuerySingleOrDefault(
            StoredProcedures.ItemGetById,
            RowMappers.MapItem,
            DbParameters.Int("@ItemId", itemId));
    }

    public IReadOnlyList<string> GetCategories()
    {
        return _executor.Query(StoredProcedures.ItemGetCategories, r => r.GetText("Category"));
    }

    public IReadOnlyList<string> GetUnits()
    {
        return _executor.Query(StoredProcedures.ItemGetUnits, r => r.GetText("Unit"));
    }

    /// <summary>Inserts a new item and returns its generated ItemId.</summary>
    public int Insert(Item item)
    {
        SqlParameter itemId = DbParameters.OutputInt("@ItemId");

        _executor.Execute(
            StoredProcedures.ItemInsert,
            DbParameters.NVarChar("@ItemCode", item.ItemCode, FieldLengths.ItemCode),
            DbParameters.NVarChar("@ItemName", item.ItemName, FieldLengths.ItemName),
            DbParameters.NVarChar("@Category", item.Category, FieldLengths.Category),
            DbParameters.NVarChar("@Unit", item.Unit, FieldLengths.Unit),
            DbParameters.Quantity("@MinimumStock", item.MinimumStock),
            DbParameters.Quantity("@OpeningStock", item.OpeningStock),
            DbParameters.Bit("@IsActive", item.IsActive),
            itemId);

        return (int)itemId.Value;
    }

    public void Update(Item item)
    {
        _executor.Execute(
            StoredProcedures.ItemUpdate,
            DbParameters.Int("@ItemId", item.ItemId),
            DbParameters.NVarChar("@ItemCode", item.ItemCode, FieldLengths.ItemCode),
            DbParameters.NVarChar("@ItemName", item.ItemName, FieldLengths.ItemName),
            DbParameters.NVarChar("@Category", item.Category, FieldLengths.Category),
            DbParameters.NVarChar("@Unit", item.Unit, FieldLengths.Unit),
            DbParameters.Quantity("@MinimumStock", item.MinimumStock),
            DbParameters.Quantity("@OpeningStock", item.OpeningStock),
            DbParameters.Bit("@IsActive", item.IsActive));
    }

    public void SetActive(int itemId, bool isActive)
    {
        _executor.Execute(
            StoredProcedures.ItemSetActive,
            DbParameters.Int("@ItemId", itemId),
            DbParameters.Bit("@IsActive", isActive));
    }
}
