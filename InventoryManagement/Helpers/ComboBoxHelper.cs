using InventoryManagement.Models;

namespace InventoryManagement.Helpers;

/// <summary>A value/text pair for simple drop-downs ("(All items)", "IN", ...).</summary>
public sealed record ComboOption(object? Value, string Text)
{
    public override string ToString() => Text;
}

/// <summary>
/// Helpers for searchable combo boxes. The user can type part of an item code
/// or name; suggestions appear and the typed text is resolved to an item.
/// </summary>
public static class ComboBoxHelper
{
    public const string AllItemsText = "(All items)";

    /// <summary>Binds items to a searchable (type-ahead) combo box.</summary>
    public static void BindItems(ComboBox combo, IEnumerable<Item> items)
    {
        MakeSearchable(combo);
        combo.DataSource = null;
        combo.DisplayMember = nameof(Item.DisplayName);
        combo.ValueMember = nameof(Item.ItemId);
        combo.DataSource = items.ToList();
        combo.SelectedIndex = -1;
        combo.Text = string.Empty;
    }

    /// <summary>Binds "(All items)" + items for report filters. Value is the ItemId (0 = all).</summary>
    public static void BindItemFilter(ComboBox combo, IEnumerable<Item> items)
    {
        var options = new List<ComboOption> { new(0, AllItemsText) };
        options.AddRange(items.Select(i => new ComboOption(i.ItemId, i.IsActive ? i.DisplayName : i.DisplayName + " (inactive)")));
        MakeSearchable(combo);
        BindOptions(combo, options);
    }

    /// <summary>Binds a list of options to a combo box and selects the first.</summary>
    public static void BindOptions(ComboBox combo, IReadOnlyList<ComboOption> options)
    {
        combo.DataSource = null;
        combo.DisplayMember = nameof(ComboOption.Text);
        combo.ValueMember = nameof(ComboOption.Value);
        combo.DataSource = options.ToList();
        if (options.Count > 0)
        {
            combo.SelectedIndex = 0;
        }
    }

    /// <summary>
    /// Returns the item matching the combo selection or typed text
    /// (full "CODE - Name" text or just the item code), or null.
    /// </summary>
    public static Item? GetSelectedItem(ComboBox combo)
    {
        if (combo.SelectedItem is Item selected &&
            string.Equals(combo.Text.Trim(), selected.DisplayName, StringComparison.CurrentCultureIgnoreCase))
        {
            return selected;
        }

        string text = combo.Text.Trim();
        if (text.Length == 0 || combo.DataSource is not IEnumerable<Item> items)
        {
            return null;
        }

        Item? match = items.FirstOrDefault(i =>
            string.Equals(i.DisplayName, text, StringComparison.CurrentCultureIgnoreCase) ||
            string.Equals(i.ItemCode, text, StringComparison.CurrentCultureIgnoreCase));

        if (match is not null && !ReferenceEquals(combo.SelectedItem, match))
        {
            combo.SelectedItem = match;
        }

        return match;
    }

    /// <summary>Selected ItemId of an item filter combo; null means all items.</summary>
    public static int? GetSelectedItemFilter(ComboBox combo)
    {
        ComboOption? option = GetSelectedOption(combo);
        return option?.Value is int id && id > 0 ? id : null;
    }

    /// <summary>Selected option, resolving typed text when necessary.</summary>
    public static ComboOption? GetSelectedOption(ComboBox combo)
    {
        if (combo.SelectedItem is ComboOption selected &&
            (combo.DropDownStyle == ComboBoxStyle.DropDownList ||
             string.Equals(combo.Text.Trim(), selected.Text, StringComparison.CurrentCultureIgnoreCase)))
        {
            return selected;
        }

        if (combo.DataSource is not IEnumerable<ComboOption> options)
        {
            return null;
        }

        string text = combo.Text.Trim();
        if (text.Length == 0)
        {
            return options.FirstOrDefault();
        }

        ComboOption? match = options.FirstOrDefault(o =>
            string.Equals(o.Text, text, StringComparison.CurrentCultureIgnoreCase) ||
            o.Text.StartsWith(text + " - ", StringComparison.CurrentCultureIgnoreCase));

        if (match is not null)
        {
            combo.SelectedItem = match;
        }

        return match;
    }

    /// <summary>Fills an editable combo box with suggestion values (categories, units, reasons).</summary>
    public static void BindSuggestions(ComboBox combo, IEnumerable<string> values)
    {
        string current = combo.Text;
        combo.DataSource = null;
        combo.Items.Clear();
        combo.Items.AddRange(values.Where(v => !string.IsNullOrWhiteSpace(v)).Distinct().Cast<object>().ToArray());
        combo.DropDownStyle = ComboBoxStyle.DropDown;
        combo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        combo.AutoCompleteSource = AutoCompleteSource.ListItems;
        combo.Text = current;
    }

    private static void MakeSearchable(ComboBox combo)
    {
        combo.DropDownStyle = ComboBoxStyle.DropDown;
        combo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        combo.AutoCompleteSource = AutoCompleteSource.ListItems;
        combo.MaxDropDownItems = 15;
    }
}
