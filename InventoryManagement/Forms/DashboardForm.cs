using InventoryManagement.Helpers;
using InventoryManagement.Models;
using InventoryManagement.Models.Reports;

namespace InventoryManagement.Forms;

/// <summary>
/// Dashboard: six summary cards, quick actions and the low-stock list
/// (active items where Current Stock &lt;= Minimum Stock).
/// </summary>
public partial class DashboardForm : Form, IRefreshablePage
{
    private readonly AppServices _services;
    private readonly INavigationHost? _navigation;

    /// <summary>Designer support only.</summary>
    public DashboardForm()
    {
        InitializeComponent();
        _services = null!;
    }

    public DashboardForm(AppServices services, INavigationHost navigation)
    {
        InitializeComponent();
        _services = services ?? throw new ArgumentNullException(nameof(services));
        _navigation = navigation;

        ApplyTheme();
        ConfigureGrid();

        btnQuickStockIn.Click += (_, _) => _navigation?.Navigate(AppPage.StockIn);
        btnQuickStockOut.Click += (_, _) => _navigation?.Navigate(AppPage.StockOut);
        btnQuickItemMaster.Click += (_, _) => _navigation?.Navigate(AppPage.ItemMaster);
        btnQuickCurrentStock.Click += (_, _) => _navigation?.Navigate(AppPage.CurrentStock);
        btnRefresh.Click += (_, _) => RefreshData();
    }

    public void RefreshData()
    {
        try
        {
            DashboardSummary summary;
            IReadOnlyList<CurrentStockReportRow> lowStock;

            using (new WaitCursorScope())
            {
                summary = _services.Reports.GetDashboardSummary();
                lowStock = _services.Reports.GetLowStockItems();
            }

            ShowSummary(summary);
            GridHelper.Bind(dgvLowStock, lowStock);

            bool anyLowStock = lowStock.Count > 0;
            dgvLowStock.Visible = anyLowStock;
            lblNoLowStock.Visible = !anyLowStock;
            lblLowStockTitle.Text = $"Low Stock Items ({lowStock.Count})  —  Current Stock <= Minimum Stock";
            lblLastUpdated.Text = $"Last updated {DateTime.Now:HH:mm:ss}";
        }
        catch (Exception ex)
        {
            MessageHelper.HandleException(ex, "loading the dashboard");
        }
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        if (_services is not null)
        {
            RefreshData();
        }
    }

    private void ShowSummary(DashboardSummary summary)
    {
        int inactive = summary.TotalItems - summary.ActiveItems;

        cardTotalItems.Value = summary.TotalItems.ToString("N0");
        cardTotalItems.Subtitle = inactive == 1 ? "1 inactive item" : $"{inactive:N0} inactive items";

        cardActiveItems.Value = summary.ActiveItems.ToString("N0");
        cardActiveItems.Subtitle = "Available for stock entry";

        cardTotalStock.Value = UiFormats.FormatQuantity(summary.TotalStockQuantity);
        cardTotalStock.Subtitle = "Sum of current stock of active items (all units)";

        cardLowStock.Value = summary.LowStockItems.ToString("N0");
        cardLowStock.Subtitle = $"including {summary.OutOfStockItems:N0} out of stock";
        cardLowStock.AccentColor = summary.OutOfStockItems > 0 ? UiTheme.Danger
            : summary.LowStockItems > 0 ? UiTheme.Warning
            : UiTheme.Success;

        cardTodayIn.Value = UiFormats.FormatQuantity(summary.TodayInQuantity);
        cardTodayIn.Subtitle = FormatTransactionCount(summary.TodayInCount);

        cardTodayOut.Value = UiFormats.FormatQuantity(summary.TodayOutQuantity);
        cardTodayOut.Subtitle = FormatTransactionCount(summary.TodayOutCount);
    }

    private static string FormatTransactionCount(int count) =>
        count == 1 ? "1 transaction today" : $"{count:N0} transactions today";

    private void ApplyTheme()
    {
        UiTheme.ApplyPage(this);
        tlpMain.BackColor = UiTheme.ContentBack;
        tlpCards.BackColor = UiTheme.ContentBack;
        flpQuickActions.BackColor = UiTheme.ContentBack;

        UiTheme.StyleSuccessButton(btnQuickStockIn);
        UiTheme.StyleDangerButton(btnQuickStockOut);
        UiTheme.StylePrimaryButton(btnQuickItemMaster);
        UiTheme.StyleSecondaryButton(btnQuickCurrentStock);
        UiTheme.StyleSecondaryButton(btnRefresh);
        UiTheme.StyleMutedLabel(lblLastUpdated);

        UiTheme.StyleCard(pnlLowStock);
        UiTheme.StyleSectionTitle(lblLowStockTitle);
        UiTheme.StyleMutedLabel(lblNoLowStock);

        UiTheme.StyleStatusBadge(lblLegendNormal, StockStatus.Normal);
        UiTheme.StyleStatusBadge(lblLegendLow, StockStatus.Low);
        UiTheme.StyleStatusBadge(lblLegendOut, StockStatus.OutOfStock);
    }

    private void ConfigureGrid()
    {
        UiTheme.StyleGrid(dgvLowStock);
        GridHelper.AddTextColumn(dgvLowStock, nameof(CurrentStockReportRow.ItemCode), "Item Code", 70);
        GridHelper.AddTextColumn(dgvLowStock, nameof(CurrentStockReportRow.ItemName), "Item Name", 160);
        GridHelper.AddTextColumn(dgvLowStock, nameof(CurrentStockReportRow.Category), "Category", 90);
        GridHelper.AddQuantityColumn(dgvLowStock, nameof(CurrentStockReportRow.CurrentStock), "Current Stock");
        GridHelper.AddQuantityColumn(dgvLowStock, nameof(CurrentStockReportRow.MinimumStock), "Minimum Stock");
        GridHelper.AddTextColumn(dgvLowStock, nameof(CurrentStockReportRow.Unit), "Unit", 45, 50);
        GridHelper.AddCenteredColumn(dgvLowStock, nameof(CurrentStockReportRow.StockStatus), "Status", 75);
        GridHelper.EnableStockStatusColors(dgvLowStock, "col" + nameof(CurrentStockReportRow.StockStatus));
    }
}
