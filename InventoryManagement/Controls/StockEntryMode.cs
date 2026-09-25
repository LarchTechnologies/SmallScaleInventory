using InventoryManagement.Helpers;
using InventoryManagement.Models;

namespace InventoryManagement.Controls;

/// <summary>
/// Texts and settings that make <see cref="StockEntryControl"/> behave as the
/// Stock IN screen or the Stock OUT screen. Both screens share the same
/// entry logic, so it exists only once.
/// </summary>
public sealed class StockEntryMode
{
    public static readonly StockEntryMode StockIn = new()
    {
        TransactionType = TransactionTypes.In,
        EntryTitle = "Stock IN Entry",
        CurrentStockCaption = "Current Stock",
        QuantityCaption = "Add Quantity",
        QuantitySign = "+",
        NewStockCaption = "New Stock",
        SaveButtonText = "Save Stock IN",
        SuccessMessage = "Stock added successfully.",
        RecentTitle = "Recent Stock IN entries",
        AccentColor = UiTheme.Success,
        Reasons = new[] { "Purchase", "Production output", "Customer return", "Transfer in", "Stock correction", "Other" },
    };

    public static readonly StockEntryMode StockOut = new()
    {
        TransactionType = TransactionTypes.Out,
        EntryTitle = "Stock OUT Entry",
        CurrentStockCaption = "Available Stock",
        QuantityCaption = "Issue Quantity",
        QuantitySign = "−",
        NewStockCaption = "Remaining Stock",
        SaveButtonText = "Save Stock OUT",
        SuccessMessage = "Stock issued successfully.",
        RecentTitle = "Recent Stock OUT entries",
        AccentColor = UiTheme.Danger,
        Reasons = new[] { "Issue to production", "Sale / dispatch", "Damage / scrap", "Sample", "Return to supplier", "Stock correction", "Other" },
    };

    private StockEntryMode()
    {
    }

    public string TransactionType { get; private init; } = TransactionTypes.In;

    public string EntryTitle { get; private init; } = string.Empty;

    public string CurrentStockCaption { get; private init; } = string.Empty;

    public string QuantityCaption { get; private init; } = string.Empty;

    public string QuantitySign { get; private init; } = string.Empty;

    public string NewStockCaption { get; private init; } = string.Empty;

    public string SaveButtonText { get; private init; } = string.Empty;

    public string SuccessMessage { get; private init; } = string.Empty;

    public string RecentTitle { get; private init; } = string.Empty;

    public Color AccentColor { get; private init; }

    public IReadOnlyList<string> Reasons { get; private init; } = Array.Empty<string>();

    public bool IsOutward => TransactionTypes.GetDirection(TransactionType) < 0;
}
