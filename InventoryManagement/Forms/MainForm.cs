using InventoryManagement.Controls;
using InventoryManagement.Helpers;
using InventoryManagement.Logging;

namespace InventoryManagement.Forms;

/// <summary>
/// Application shell: sidebar navigation, page header, content area and
/// status bar. Each page is a normal Form hosted inside the content panel,
/// so every screen can also be opened and edited separately in the designer.
/// </summary>
public partial class MainForm : Form, INavigationHost
{
    private static readonly IReadOnlyDictionary<AppPage, (string Title, string Subtitle)> PageTexts =
        new Dictionary<AppPage, (string, string)>
        {
            [AppPage.Dashboard] = ("Dashboard", "Stock overview, today's activity and items that need attention"),
            [AppPage.ItemMaster] = ("Item Master", "Create, edit, search and activate / deactivate items"),
            [AppPage.StockIn] = ("Stock IN", "Record received stock: Current Stock + Add Quantity = New Stock"),
            [AppPage.StockOut] = ("Stock OUT", "Record issued stock: Available Stock - Issue Quantity = Remaining Stock"),
            [AppPage.CurrentStock] = ("Current Stock Report", "Opening + Total IN - Total OUT = Current Stock, with status for every item"),
            [AppPage.StockSummary] = ("Stock Summary Report", "Opening, IN, OUT and Closing stock per item for a date range"),
            [AppPage.StockMovement] = ("Stock Movement Report", "Every stock transaction with filters and IN / OUT totals"),
            [AppPage.DatabaseInformation] = ("Database Information", "Connection details and database status"),
        };

    private readonly AppServices _services;
    private readonly List<NavButton> _navButtons;
    private Form? _currentPage;
    private AppPage? _currentPageType;

    /// <summary>Designer support only.</summary>
    public MainForm()
    {
        InitializeComponent();
        _services = null!;
        _navButtons = new List<NavButton>();
    }

    public MainForm(AppServices services)
    {
        InitializeComponent();
        _services = services ?? throw new ArgumentNullException(nameof(services));

        _navButtons = new List<NavButton>
        {
            ConfigureNavButton(btnNavDashboard, AppPage.Dashboard),
            ConfigureNavButton(btnNavItemMaster, AppPage.ItemMaster),
            ConfigureNavButton(btnNavStockIn, AppPage.StockIn),
            ConfigureNavButton(btnNavStockOut, AppPage.StockOut),
            ConfigureNavButton(btnNavCurrentStock, AppPage.CurrentStock),
            ConfigureNavButton(btnNavStockSummary, AppPage.StockSummary),
            ConfigureNavButton(btnNavStockMovement, AppPage.StockMovement),
            ConfigureNavButton(btnNavDatabaseInfo, AppPage.DatabaseInformation),
        };

        // "About" is a dialog, not a page.
        btnNavAbout.Click += (_, _) => ShowAbout();

        lblVersion.Text = $"Version {Program.GetVersion()}";
        lblToday.Text = DateTime.Today.ToString("dddd, dd MMMM yyyy");
        lblStatusDatabase.Text = $"Database: {_services.Database.Description}";
        Text = MessageHelper.ApplicationTitle;
    }

    /// <summary>Opens a page in the content area (used by the sidebar and the dashboard shortcuts).</summary>
    public void Navigate(AppPage page)
    {
        if (_currentPageType == page && _currentPage is { IsDisposed: false })
        {
            RefreshCurrentPage();
            return;
        }

        Form newPage;
        try
        {
            newPage = CreatePage(page);
        }
        catch (Exception ex)
        {
            MessageHelper.HandleException(ex, "opening the page");
            return;
        }

        pnlContent.SuspendLayout();
        try
        {
            Form? previous = _currentPage;

            newPage.TopLevel = false;
            newPage.FormBorderStyle = FormBorderStyle.None;
            newPage.Dock = DockStyle.Fill;
            pnlContent.Controls.Add(newPage);
            newPage.Show();
            newPage.BringToFront();

            _currentPage = newPage;
            _currentPageType = page;

            if (previous is not null)
            {
                pnlContent.Controls.Remove(previous);
                previous.Dispose();
            }
        }
        finally
        {
            pnlContent.ResumeLayout(true);
        }

        (string title, string subtitle) = PageTexts[page];
        lblPageTitle.Text = title;
        lblPageSubtitle.Text = subtitle;
        lblStatusMessage.Text = $"{title}  •  F5 to refresh";

        foreach (NavButton button in _navButtons)
        {
            button.IsActive = button.Page == page;
        }

        AppLogger.Info($"Opened page: {title}.");
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        if (_services is not null)
        {
            Navigate(AppPage.Dashboard);
        }
    }

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
        AppPage? target = keyData switch
        {
            Keys.Control | Keys.D => AppPage.Dashboard,
            Keys.F2 => AppPage.ItemMaster,
            Keys.F3 => AppPage.StockIn,
            Keys.F4 => AppPage.StockOut,
            Keys.F6 => AppPage.CurrentStock,
            Keys.F7 => AppPage.StockSummary,
            Keys.F8 => AppPage.StockMovement,
            _ => null,
        };

        if (target is AppPage page && _services is not null)
        {
            Navigate(page);
            return true;
        }

        if (keyData == Keys.F5)
        {
            RefreshCurrentPage();
            return true;
        }

        return base.ProcessCmdKey(ref msg, keyData);
    }

    private Form CreatePage(AppPage page) => page switch
    {
        AppPage.Dashboard => new DashboardForm(_services, this),
        AppPage.ItemMaster => new ItemMasterForm(_services),
        AppPage.StockIn => new StockInForm(_services),
        AppPage.StockOut => new StockOutForm(_services),
        AppPage.CurrentStock => new CurrentStockForm(_services),
        AppPage.StockSummary => new StockSummaryForm(_services),
        AppPage.StockMovement => new StockMovementForm(_services),
        AppPage.DatabaseInformation => new DatabaseInfoForm(_services),
        _ => throw new ArgumentOutOfRangeException(nameof(page), page, "Unknown page."),
    };

    private void RefreshCurrentPage()
    {
        if (_currentPage is IRefreshablePage refreshable)
        {
            refreshable.RefreshData();
            lblStatusMessage.Text = $"Refreshed at {DateTime.Now:HH:mm:ss}";
        }
    }

    private NavButton ConfigureNavButton(NavButton button, AppPage page)
    {
        button.Page = page;
        button.Click += (_, _) => Navigate(page);
        return button;
    }

    private void ShowAbout()
    {
        using var about = new AboutForm(_services);
        about.ShowDialog(this);
    }
}
