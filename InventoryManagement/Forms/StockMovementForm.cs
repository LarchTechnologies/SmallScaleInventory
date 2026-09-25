using InventoryManagement.Exceptions;
using InventoryManagement.Helpers;
using InventoryManagement.Models;
using InventoryManagement.Models.Reports;

namespace InventoryManagement.Forms;

/// <summary>
/// Stock Movement report: every stock transaction for the selected period,
/// optionally filtered by item, transaction type and reference / reason /
/// remarks text, with Total IN, Total OUT and Net movement.
/// </summary>
public partial class StockMovementForm : Form, IRefreshablePage
{
    private const string AllTypesText = "(All types)";

    private readonly AppServices _services;

    /// <summary>Designer support only.</summary>
    public StockMovementForm()
    {
        InitializeComponent();
        _services = null!;
    }

    public StockMovementForm(AppServices services)
    {
        InitializeComponent();
        _services = services ?? throw new ArgumentNullException(nameof(services));

        ReportFormStyler.Apply(this, tlpMain, pnlFilters, pnlGrid, dgvReport, btnGenerate, btnPrint,
            new[] { lblPeriod, lblFrom, lblTo, lblItem, lblType, lblSearch }, lblRowCount);
        lblTotalIn.ForeColor = UiTheme.StatusNormalText;
        lblTotalOut.ForeColor = UiTheme.StatusOutText;
        lblTotalIn.Font = UiTheme.BoldFont;
        lblTotalOut.Font = UiTheme.BoldFont;
        lblNet.Font = UiTheme.BoldFont;
        ConfigureGrid();

        btnGenerate.Click += (_, _) => LoadReport();
        btnPrint.Click += (_, _) => Print();
        cboType.SelectedIndexChanged += (_, _) => LoadReport();
        txtSearch.KeyDown += (_, e) =>
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                LoadReport();
            }
        };

        toolTip.SetToolTip(btnGenerate, "Load the transactions for the selected filters.");
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

        var types = new List<ComboOption> { new(null, AllTypesText) };
        types.AddRange(TransactionTypes.All.Select(t => new ComboOption(t, t)));
        ComboBoxHelper.BindOptions(cboType, types);

        LoadItemFilter();
        LoadReport();
    }

    private void LoadItemFilter()
    {
        try
        {
            int? selected = ComboBoxHelper.GetSelectedItemFilter(cboItem);
            ComboBoxHelper.BindItemFilter(cboItem, _services.Items.GetAllItems());
            StockSummaryForm.SelectItemFilter(cboItem, selected);
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

        if (ComboBoxHelper.GetSelectedOption(cboItem) is null)
        {
            MessageHelper.ShowWarning($"Please select an item from the list, or choose '{ComboBoxHelper.AllItemsText}'.");
            cboItem.Focus();
            return;
        }

        var filter = new StockMovementReportFilter
        {
            FromDate = dtpFrom.Value.Date,
            ToDate = dtpTo.Value.Date,
            ItemId = ComboBoxHelper.GetSelectedItemFilter(cboItem),
            TransactionType = ComboBoxHelper.GetSelectedOption(cboType)?.Value as string,
            SearchText = txtSearch.Text,
        };

        try
        {
            StockMovementReport report;
            using (new WaitCursorScope())
            {
                report = _services.Reports.GetStockMovementReport(filter);
            }

            GridHelper.Bind(dgvReport, report.Rows);

            string unitNote = filter.ItemId is null ? " (all units)" : string.Empty;
            lblRowCount.Text = report.Rows.Count == 1 ? "1 transaction" : $"{report.Rows.Count:N0} transactions";
            lblTotalIn.Text = $"Total IN{unitNote}: {UiFormats.FormatQuantity(report.TotalIn)}";
            lblTotalOut.Text = $"Total OUT{unitNote}: {UiFormats.FormatQuantity(report.TotalOut)}";
            lblNet.Text = $"Net: {UiFormats.FormatQuantity(report.NetMovement)}";
        }
        catch (ValidationException ex)
        {
            MessageHelper.ShowWarning(ex.Message);
            dtpFrom.Focus();
        }
        catch (Exception ex)
        {
            MessageHelper.HandleException(ex, "loading the stock movement report");
        }
    }

    private void ConfigureGrid()
    {
        GridHelper.AddTextColumn(dgvReport, nameof(StockMovementReportRow.TransactionId), "Txn ID", 50);
        GridHelper.AddDateColumn(dgvReport, nameof(StockMovementReportRow.TransactionDate), "Date", includeTime: true, fillWeight: 85);
        GridHelper.AddTextColumn(dgvReport, nameof(StockMovementReportRow.ItemCode), "Item Code", 65);
        GridHelper.AddTextColumn(dgvReport, nameof(StockMovementReportRow.ItemName), "Item Name", 120);
        GridHelper.AddCenteredColumn(dgvReport, nameof(StockMovementReportRow.TransactionType), "Type", 45);
        GridHelper.AddQuantityColumn(dgvReport, nameof(StockMovementReportRow.Quantity), "Quantity", 65);
        GridHelper.AddTextColumn(dgvReport, nameof(StockMovementReportRow.Unit), "Unit", 40, 50);
        GridHelper.AddTextColumn(dgvReport, nameof(StockMovementReportRow.ReferenceNo), "Reference No", 75);
        GridHelper.AddTextColumn(dgvReport, nameof(StockMovementReportRow.Reason), "Reason", 80);
        GridHelper.AddTextColumn(dgvReport, nameof(StockMovementReportRow.Remarks), "Remarks", 100);
        GridHelper.EnableTransactionTypeColors(dgvReport, "col" + nameof(StockMovementReportRow.TransactionType));
    }

    private void Print()
    {
        var lines = new List<string>
        {
            ReportFormStyler.DescribePeriod(dtpFrom, dtpTo),
            $"Item: {cboItem.Text}    Type: {cboType.Text}",
        };

        if (!string.IsNullOrWhiteSpace(txtSearch.Text))
        {
            lines.Add($"Search: {txtSearch.Text.Trim()}");
        }

        GridPrinter.ShowPreview(this, dgvReport, "Stock Movement Report", lines,
            $"{lblRowCount.Text}    {lblTotalIn.Text}    {lblTotalOut.Text}    {lblNet.Text}");
    }
}
