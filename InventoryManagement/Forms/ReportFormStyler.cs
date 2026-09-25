using InventoryManagement.Helpers;

namespace InventoryManagement.Forms;

/// <summary>Shared look for the three report pages (filter card, grid card, buttons).</summary>
internal static class ReportFormStyler
{
    public static void Apply(
        Form form,
        TableLayoutPanel layout,
        Panel filterCard,
        Panel gridCard,
        DataGridView grid,
        Button primaryButton,
        Button printButton,
        IEnumerable<Label> filterCaptions,
        Label rowCountLabel)
    {
        UiTheme.ApplyPage(form);
        layout.BackColor = UiTheme.ContentBack;
        UiTheme.StyleCard(filterCard);
        UiTheme.StyleCard(gridCard);
        UiTheme.StyleGrid(grid);
        UiTheme.StylePrimaryButton(primaryButton);
        UiTheme.StyleSecondaryButton(printButton);

        foreach (Label caption in filterCaptions)
        {
            caption.ForeColor = UiTheme.TextMuted;
        }

        rowCountLabel.Font = UiTheme.BoldFont;
    }

    /// <summary>Shows inactive items in grey (except the coloured status column).</summary>
    public static void GreyOutInactiveRows(DataGridView grid, Func<object?, bool> isInactive)
    {
        grid.CellFormatting += (_, e) =>
        {
            if (e.RowIndex < 0 || e.CellStyle is null || !grid.Columns[e.ColumnIndex].Name.StartsWith("col", StringComparison.Ordinal))
            {
                return;
            }

            if (grid.Columns[e.ColumnIndex].Name.EndsWith("StockStatus", StringComparison.Ordinal))
            {
                return;
            }

            if (isInactive(grid.Rows[e.RowIndex].DataBoundItem))
            {
                e.CellStyle.ForeColor = UiTheme.InactiveText;
                e.CellStyle.SelectionForeColor = UiTheme.InactiveText;
            }
        };
    }

    /// <summary>Filter description lines printed above report tables.</summary>
    public static string DescribePeriod(DateTimePicker from, DateTimePicker to) =>
        $"Period: {from.Value.ToString(UiFormats.Date)} to {to.Value.ToString(UiFormats.Date)}";
}
