using InventoryManagement.Controls;
using InventoryManagement.Helpers;

namespace InventoryManagement.Forms;

/// <summary>
/// Stock OUT screen: Available Stock - Issue Quantity = Remaining Stock.
/// An issue larger than the available stock is refused (stock never goes negative).
/// The entry logic is shared with Stock IN in <see cref="StockEntryControl"/>.
/// </summary>
public partial class StockOutForm : Form, IRefreshablePage
{
    private readonly AppServices _services;

    /// <summary>Designer support only.</summary>
    public StockOutForm()
    {
        InitializeComponent();
        _services = null!;
    }

    public StockOutForm(AppServices services)
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
            stockEntry.Initialize(_services, StockEntryMode.StockOut);
        }
    }
}
