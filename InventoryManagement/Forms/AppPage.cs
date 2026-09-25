namespace InventoryManagement.Forms;

/// <summary>Pages that can be shown in the main window's content area.</summary>
public enum AppPage
{
    Dashboard,
    ItemMaster,
    StockIn,
    StockOut,
    CurrentStock,
    StockSummary,
    StockMovement,
    DatabaseInformation,
}

/// <summary>Implemented by MainForm so pages (e.g. the dashboard) can open other pages.</summary>
public interface INavigationHost
{
    void Navigate(AppPage page);
}

/// <summary>Pages that can reload their data when the user presses F5.</summary>
public interface IRefreshablePage
{
    void RefreshData();
}
