using InventoryManagement.Helpers;
using InventoryManagement.Models;
using InventoryManagement.Models.Reports;

namespace InventoryManagement.Forms;

/// <summary>
/// Current Stock report. For every item:
///   Current Stock = Opening Stock + Total IN - Total OUT
/// Status: OUT OF STOCK (current &lt;= 0), LOW STOCK (current &lt;= minimum), NORMAL.
/// All figures are calculated by usp_Report_CurrentStock from the transactions.
/// </summary>
public partial class CurrentStockForm : Form, IRefreshablePage
{
    private const string AllCategoriesText = "(All categories)";
    private const string AllStatusesText = "(All statuses)";

    private readonly AppServices _services;
    private readonly System.Windows.Forms.Timer _searchDelay = new() { Interval = 400 };
    private bool _initialising = true;

    /// <summary>Designer support only.</summary>
    public CurrentStockForm()
    {
        InitializeComponent();
        _services = null!;
    }

    public CurrentStockForm(AppServices services)
    {
        InitializeComponent();
        _services = services ?? throw new ArgumentNullException(nameof(services));

        ReportFormStyler.Apply(this, tlpMain, pnlFilters, pnlGrid, dgvReport, btnRefresh, btnPrint,
            new[] { lblSearch, lblCategory, lblStatus }, lblRowCount);
        ConfigureGrid();

        // The designer's component container disposes the timer with the form.
        components ??= new System.ComponentModel.Container();
        components.Add(_searchDelay);

        // Search as you type (short delay), other filters apply immediately.
        _searchDelay.Tick += (_, _) =>
        {
            _searchDelay.Stop();
            LoadReport();
        };
        txtSearch.TextChanged += (_, _) =>
        {
            _searchDelay.Stop();
            _searchDelay.Start();
        };
        txtSearch.KeyDown += (_, e) =>
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                _searchDelay.Stop();
                LoadReport();
            }
        };
        cboCategory.SelectedIndexChanged += (_, _) => LoadReport();
        cboStatus.SelectedIndexChanged += (_, _) => LoadReport();
        chkIncludeInactive.CheckedChanged += (_, _) => LoadReport();
        btnRefresh.Click += (_, _) => RefreshData();
        btnPrint.Click += (_, _) => Print();

        toolTip.SetToolTip(btnPrint, "Print preview of the rows currently shown (you can also save as PDF).");
    }

    public void RefreshData()
    {
        LoadCategories();
        LoadReport();
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        if (_services is null)
        {
            return;
        }

        var statuses = new List<ComboOption> { new(null, AllStatusesText) };
        statuses.AddRange(StockStatus.All.Select(s => new ComboOption(s, s)));
        ComboBoxHelper.BindOptions(cboStatus, statuses);

        LoadCategories();
        _initialising = false;
        LoadReport();
    }

    private void LoadCategories()
    {
        try
        {
            string? selected = (ComboBoxHelper.GetSelectedOption(cboCategory)?.Value) as string;

            var options = new List<ComboOption> { new(null, AllCategoriesText) };
            options.AddRange(_services.Items.GetCategories().Select(c => new ComboOption(c, c)));

            bool previous = _initialising;
            _initialising = true;
            ComboBoxHelper.BindOptions(cboCategory, options);
            ComboOption? keep = options.FirstOrDefault(o => Equals(o.Value, selected));
            if (keep is not null)
            {
                cboCategory.SelectedItem = keep;
            }

            _initialising = previous;
        }
        catch (Exception ex)
        {
            MessageHelper.HandleException(ex, "loading categories");
        }
    }

    private void LoadReport()
    {
        if (_initialising || _services is null)
        {
            return;
        }

        var filter = new CurrentStockReportFilter
        {
            SearchText = txtSearch.Text,
            Category = ComboBoxHelper.GetSelectedOption(cboCategory)?.Value as string,
            StockStatus = ComboBoxHelper.GetSelectedOption(cboStatus)?.Value as string,
            IncludeInactive = chkIncludeInactive.Checked,
        };

        try
        {
            IReadOnlyList<CurrentStockReportRow> rows;
            using (new WaitCursorScope())
            {
                rows = _services.Reports.GetCurrentStockReport(filter);
            }

            GridHelper.Bind(dgvReport, rows);

            int low = rows.Count(r => r.StockStatus == StockStatus.Low);
            int outOfStock = rows.Count(r => r.StockStatus == StockStatus.OutOfStock);
            lblRowCount.Text = rows.Count == 1 ? "1 item" : $"{rows.Count:N0} items";
            lblLowCount.Text = $"{StockStatus.Low}: {low:N0}";
            lblOutCount.Text = $"{StockStatus.OutOfStock}: {outOfStock:N0}";
        }
        catch (Exception ex)
        {
            MessageHelper.HandleException(ex, "loading the current stock report");
        }
    }

    private void ConfigureGrid()
    {
        GridHelper.AddTextColumn(dgvReport, nameof(CurrentStockReportRow.ItemCode), "Item Code", 70);
        GridHelper.AddTextColumn(dgvReport, nameof(CurrentStockReportRow.ItemName), "Item Name", 140);
        GridHelper.AddTextColumn(dgvReport, nameof(CurrentStockReportRow.Category), "Category", 85);
        GridHelper.AddTextColumn(dgvReport, nameof(CurrentStockReportRow.Unit), "Unit", 45, 50);
        GridHelper.AddQuantityColumn(dgvReport, nameof(CurrentStockReportRow.OpeningStock), "Opening");
        GridHelper.AddQuantityColumn(dgvReport, nameof(CurrentStockReportRow.TotalIn), "Total IN");
        GridHelper.AddQuantityColumn(dgvReport, nameof(CurrentStockReportRow.TotalOut), "Total OUT");
        GridHelper.AddQuantityColumn(dgvReport, nameof(CurrentStockReportRow.CurrentStock), "Current Stock", 80);
        GridHelper.AddQuantityColumn(dgvReport, nameof(CurrentStockReportRow.MinimumStock), "Min Stock");
        GridHelper.AddCenteredColumn(dgvReport, nameof(CurrentStockReportRow.StockStatus), "Status", 85);
        GridHelper.EnableStockStatusColors(dgvReport, "col" + nameof(CurrentStockReportRow.StockStatus));

        ReportFormStyler.GreyOutInactiveRows(dgvReport, row => row is CurrentStockReportRow { IsActive: false });
        StyleCountLabels();
    }

    private void StyleCountLabels()
    {
        lblLowCount.ForeColor = UiTheme.StatusLowText;
        lblOutCount.ForeColor = UiTheme.StatusOutText;
        lblLowCount.Font = UiTheme.BoldFont;
        lblOutCount.Font = UiTheme.BoldFont;
    }

    private void Print()
    {
        var lines = new List<string>
        {
            "Current Stock = Opening Stock + Total IN - Total OUT",
            $"Category: {cboCategory.Text}    Status: {cboStatus.Text}    Inactive items: {(chkIncludeInactive.Checked ? "included" : "excluded")}",
        };

        if (!string.IsNullOrWhiteSpace(txtSearch.Text))
        {
            lines.Add($"Search: {txtSearch.Text.Trim()}");
        }

        GridPrinter.ShowPreview(this, dgvReport, "Current Stock Report", lines,
            $"{lblRowCount.Text}    {lblLowCount.Text}    {lblOutCount.Text}");
    }
}
