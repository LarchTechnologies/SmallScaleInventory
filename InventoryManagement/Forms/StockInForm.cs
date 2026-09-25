using InventoryManagement.Controls;
using InventoryManagement.Helpers;

namespace InventoryManagement.Forms;

/// <summary>
/// Stock IN screen: Current Stock + Add Quantity = New Stock.
/// The entry logic is shared with Stock OUT in <see cref="StockEntryControl"/>.
/// </summary>
public partial class StockInForm : Form, IRefreshablePage
{
    private readonly AppServices _services;

    /// <summary>Designer support only.</summary>
    public StockInForm()
    {
        InitializeComponent();
        _services = null!;
    }

    public StockInForm(AppServices services)
    {
        InitializeComponent();
        _services = services ?? throw new ArgumentNullException(nameof(services));
        UiTheme.ApplyPage(this);
    }

    public void RefreshData() => stockEntry.ReloadData();

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        if (_services is not null)
        {
            stockEntry.Initialize(_services, StockEntryMode.StockIn);
        }
    }
}
