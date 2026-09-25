using InventoryManagement.Exceptions;
using InventoryManagement.Helpers;
using InventoryManagement.Models.Reports;

namespace InventoryManagement.Forms;

/// <summary>
/// Stock Summary report for a date range. For every item:
///   Opening = Opening Stock + IN - OUT of all transactions BEFORE the From date
///   IN / OUT = transactions from the From date to the To date (inclusive)
///   Closing = Opening + IN - OUT
/// Calculated by usp_Report_StockSummary from the transaction table.
/// </summary>
public partial class StockSummaryForm : Form, IRefreshablePage
{
    private readonly AppServices _services;

    /// <summary>Designer support only.</summary>
    public StockSummaryForm()
    {
        InitializeComponent();
        _services = null!;
    }

    public StockSummaryForm(AppServices services)
    {
        InitializeComponent();
        _services = services ?? throw new ArgumentNullException(nameof(services));

        ReportFormStyler.Apply(this, tlpMain, pnlFilters, pnlGrid, dgvReport, btnGenerate, btnPrint,
            new[] { lblPeriod, lblFrom, lblTo, lblItem }, lblRowCount);
        UiTheme.StyleMutedLabel(lblTotals);
        ConfigureGrid();

        btnGenerate.Click += (_, _) => LoadReport();
        btnPrint.Click += (_, _) => Print();
        chkIncludeInactive.CheckedChanged += (_, _) => LoadReport();

        toolTip.SetToolTip(btnGenerate, "Calculate the summary for the selected period.");
        toolTip.SetToolTip(btnPrint, "Print preview of the report (you can also save as PDF).");
    }

    public void RefreshData()
    {
        LoadItemFilter();
        LoadReport();
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        if (_services is null)
        {
            return;
        }

        DateRangePresets.ConfigurePicker(dtpFrom);
        DateRangePresets.ConfigurePicker(dtpTo);
        DateRangePresets.Attach(cboPeriod, dtpFrom, dtpTo, DateRangePresets.ThisMonth);

        LoadItemFilter();
        LoadReport();
    }

    private void LoadItemFilter()
    {
        try
        {
            int? selected = ComboBoxHelper.GetSelectedItemFilter(cboItem);
            ComboBoxHelper.BindItemFilter(cboItem, _services.Items.GetAllItems());
            SelectItemFilter(cboItem, selected);
        }
        catch (Exception ex)
        {
            MessageHelper.HandleException(ex, "loading the item list");
        }
    }

    private void LoadReport()
    {
        // Ignore events raised while the filters are still being set up.
        if (_services is null || cboPeriod.Items.Count == 0 || cboItem.DataSource is null)
        {
            return;
        }

        ComboOption? itemOption = ComboBoxHelper.GetSelectedOption(cboItem);
        if (itemOption is null)
        {
            MessageHelper.ShowWarning($"Please select an item from the list, or choose '{ComboBoxHelper.AllItemsText}'.");
            cboItem.Focus();
            return;
        }

        var filter = new StockSummaryReportFilter
        {
            FromDate = dtpFrom.Value.Date,
            ToDate = dtpTo.Value.Date,
            ItemId = ComboBoxHelper.GetSelectedItemFilter(cboItem),
            IncludeInactive = chkIncludeInactive.Checked,
        };

        try
        {
            IReadOnlyList<StockSummaryReportRow> rows;
            using (new WaitCursorScope())
            {
                rows = _services.Reports.GetStockSummaryReport(filter);
            }

            GridHelper.Bind(dgvReport, rows);

            lblRowCount.Text = rows.Count == 1 ? "1 item" : $"{rows.Count:N0} items";
            lblTotals.Text =
                $"Totals (all units)   Opening: {UiFormats.FormatQuantity(rows.Sum(r => r.OpeningStock))}" +
                $"    IN: {UiFormats.FormatQuantity(rows.Sum(r => r.TotalIn))}" +
                $"    OUT: {UiFormats.FormatQuantity(rows.Sum(r => r.TotalOut))}" +
                $"    Closing: {UiFormats.FormatQuantity(rows.Sum(r => r.ClosingStock))}";
        }
        catch (ValidationException ex)
        {
            MessageHelper.ShowWarning(ex.Message);
            dtpFrom.Focus();
        }
        catch (Exception ex)
        {
            MessageHelper.HandleException(ex, "loading the stock summary report");
        }
    }

    private void ConfigureGrid()
    {
        GridHelper.AddTextColumn(dgvReport, nameof(StockSummaryReportRow.ItemCode), "Item Code", 70);
        GridHelper.AddTextColumn(dgvReport, nameof(StockSummaryReportRow.ItemName), "Item Name", 150);
        GridHelper.AddTextColumn(dgvReport, nameof(StockSummaryReportRow.Category), "Category", 85);
        GridHelper.AddTextColumn(dgvReport, nameof(StockSummaryReportRow.Unit), "Unit", 45, 50);
        GridHelper.AddQuantityColumn(dgvReport, nameof(StockSummaryReportRow.OpeningStock), "Opening", 75);
        GridHelper.AddQuantityColumn(dgvReport, nameof(StockSummaryReportRow.TotalIn), "IN", 70);
        GridHelper.AddQuantityColumn(dgvReport, nameof(StockSummaryReportRow.TotalOut), "OUT", 70);
        GridHelper.AddQuantityColumn(dgvReport, nameof(StockSummaryReportRow.ClosingStock), "Closing", 75).DefaultCellStyle.Font = UiTheme.BoldFont;

        dgvReport.Columns["col" + nameof(StockSummaryReportRow.OpeningStock)]!.ToolTipText = "Stock at the start of the From date";
        dgvReport.Columns["col" + nameof(StockSummaryReportRow.ClosingStock)]!.ToolTipText = "Closing = Opening + IN - OUT";
    }

    private void Print()
    {
        GridPrinter.ShowPreview(this, dgvReport, "Stock Summary Report", new[]
        {
            ReportFormStyler.DescribePeriod(dtpFrom, dtpTo),
            $"Item: {cboItem.Text}    Inactive items: {(chkIncludeInactive.Checked ? "included" : "excluded")}",
            "Closing = Opening + IN - OUT (Opening = stock at the start of the From date)",
        }, $"{lblRowCount.Text}    {lblTotals.Text}");
    }

    internal static void SelectItemFilter(ComboBox combo, int? itemId)
    {
        if (itemId is null || combo.DataSource is not IEnumerable<ComboOption> options)
        {
            return;
        }

        ComboOption? match = options.FirstOrDefault(o => o.Value is int id && id == itemId);
        if (match is not null)
        {
            combo.SelectedItem = match;
        }
    }
}
