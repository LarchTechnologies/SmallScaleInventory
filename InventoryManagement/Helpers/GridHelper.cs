using InventoryManagement.Models;

namespace InventoryManagement.Helpers;

/// <summary>Column definitions and data binding helpers for DataGridView.</summary>
public static class GridHelper
{
    /// <summary>Adds a text column bound to <paramref name="propertyName"/>.</summary>
    public static DataGridViewTextBoxColumn AddTextColumn(DataGridView grid, string propertyName, string header, float fillWeight = 100, int minimumWidth = 60)
    {
        var column = new DataGridViewTextBoxColumn
        {
            Name = "col" + propertyName,
            DataPropertyName = propertyName,
            HeaderText = header,
            FillWeight = fillWeight,
            MinimumWidth = minimumWidth,
            SortMode = DataGridViewColumnSortMode.Automatic,
        };
        grid.Columns.Add(column);
        return column;
    }

    /// <summary>Adds a right-aligned quantity column formatted as 1,234.50.</summary>
    public static DataGridViewTextBoxColumn AddQuantityColumn(DataGridView grid, string propertyName, string header, float fillWeight = 70)
    {
        DataGridViewTextBoxColumn column = AddTextColumn(grid, propertyName, header, fillWeight, 80);
        column.DefaultCellStyle.Format = UiFormats.Quantity;
        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
        return column;
    }

    public static DataGridViewTextBoxColumn AddDateColumn(DataGridView grid, string propertyName, string header, bool includeTime = false, float fillWeight = 70)
    {
        DataGridViewTextBoxColumn column = AddTextColumn(grid, propertyName, header, fillWeight, 90);
        column.DefaultCellStyle.Format = includeTime ? UiFormats.DateTime : UiFormats.Date;
        return column;
    }

    /// <summary>Adds a centred column (status, type, code values).</summary>
    public static DataGridViewTextBoxColumn AddCenteredColumn(DataGridView grid, string propertyName, string header, float fillWeight = 60)
    {
        DataGridViewTextBoxColumn column = AddTextColumn(grid, propertyName, header, fillWeight, 70);
        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
        return column;
    }

    /// <summary>Binds rows through a sortable list so column-header sorting works.</summary>
    public static void Bind<T>(DataGridView grid, IEnumerable<T> rows)
    {
        grid.DataSource = new SortableBindingList<T>(rows);
    }

    /// <summary>Returns the object bound to the currently selected row.</summary>
    public static T? GetSelected<T>(DataGridView grid) where T : class =>
        grid.CurrentRow?.DataBoundItem as T;

    /// <summary>Colours the cells of a stock-status column (green / amber / red).</summary>
    public static void EnableStockStatusColors(DataGridView grid, string statusColumnName)
    {
        grid.CellFormatting += (_, e) =>
        {
            if (e.RowIndex < 0 || e.CellStyle is null || grid.Columns[e.ColumnIndex].Name != statusColumnName)
            {
                return;
            }

            (Color back, Color fore) = UiTheme.GetStatusColors(e.Value as string);
            e.CellStyle.BackColor = back;
            e.CellStyle.ForeColor = fore;
            e.CellStyle.SelectionBackColor = back;
            e.CellStyle.SelectionForeColor = fore;
            e.CellStyle.Font = UiTheme.BoldFont;
            e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        };
    }

    /// <summary>Shows IN in green and OUT in red in a transaction-type column.</summary>
    public static void EnableTransactionTypeColors(DataGridView grid, string typeColumnName)
    {
        grid.CellFormatting += (_, e) =>
        {
            if (e.RowIndex < 0 || e.CellStyle is null || grid.Columns[e.ColumnIndex].Name != typeColumnName)
            {
                return;
            }

            bool isInward = string.Equals(e.Value as string, TransactionTypes.In, StringComparison.OrdinalIgnoreCase);
            Color color = isInward ? UiTheme.StatusNormalText : UiTheme.StatusOutText;
            e.CellStyle.ForeColor = color;
            e.CellStyle.SelectionForeColor = color;
            e.CellStyle.Font = UiTheme.BoldFont;
        };
    }
}
