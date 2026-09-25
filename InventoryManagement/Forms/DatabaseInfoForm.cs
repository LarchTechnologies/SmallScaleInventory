using System.Diagnostics;
using System.Reflection;
using InventoryManagement.Data;
using InventoryManagement.Helpers;
using InventoryManagement.Logging;
using InventoryManagement.Models;

namespace InventoryManagement.Forms;

/// <summary>
/// Settings &gt; Database Information: shows which server / database the
/// application uses (from App.config), tests the connection and gives
/// quick access to the log and SQL script folders.
/// </summary>
public partial class DatabaseInfoForm : Form, IRefreshablePage
{
    private readonly AppServices _services;

    /// <summary>Designer support only.</summary>
    public DatabaseInfoForm()
    {
        InitializeComponent();
        _services = null!;
    }

    public DatabaseInfoForm(AppServices services)
    {
        InitializeComponent();
        _services = services ?? throw new ArgumentNullException(nameof(services));

        ApplyTheme();

        btnTestConnection.Click += (_, _) => TestConnection(showResult: true);
        btnOpenLogFolder.Click += (_, _) => OpenFolder(AppLogger.LogDirectory);
        btnOpenScriptsFolder.Click += (_, _) => OpenFolder(_services.DatabaseInitializer.ScriptsDirectory);
    }

    public void RefreshData() => TestConnection(showResult: false);

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        if (_services is null)
        {
            return;
        }

        ShowConfiguration();
        txtHelp.Text = BuildHelpText();
        TestConnection(showResult: false);
    }

    private void ShowConfiguration()
    {
        DatabaseConnection database = _services.Database;
        lblServer.Text = database.DataSource;
        lblDatabase.Text = database.DatabaseName;
        lblAuthentication.Text = database.UsesIntegratedSecurity
            ? "Windows authentication (Integrated Security)"
            : "SQL Server authentication";
        lblConnectionName.Text = $"\"{DatabaseConnection.DefaultConnectionStringName}\" in the <connectionStrings> section";
        lblConfigFile.Text = GetConfigurationFilePath();
        lblLogFile.Text = AppLogger.LogFilePath;
    }

    private void TestConnection(bool showResult)
    {
        DatabaseInfo info;
        try
        {
            using (new WaitCursorScope())
            {
                info = _services.DatabaseInitializer.GetDatabaseInfo();
            }
        }
        catch (Exception ex)
        {
            SetStatus(false, "Not connected - see the application log for details");
            ClearServerDetails();

            if (showResult)
            {
                MessageHelper.HandleException(ex, "testing the database connection");
            }

            return;
        }

        lblServerVersion.Text = info.ServerVersion;
        lblServerEdition.Text = info.ServerEdition;
        lblCreated.Text = info.DatabaseCreatedDate?.ToString(UiFormats.DateTime) ?? "—";
        lblItems.Text = info.ItemCount.ToString("N0");
        lblTransactions.Text = info.TransactionCount.ToString("N0");
        lblLastTransaction.Text = info.LastTransactionDate?.ToString(UiFormats.DateTime) ?? "No transactions yet";
        SetStatus(true, $"Connected (checked at {DateTime.Now:HH:mm:ss})");

        if (showResult)
        {
            MessageHelper.ShowSuccess($"Connection successful.\n\nServer: {info.DataSource}\nDatabase: {info.DatabaseName}\nSQL Server {info.ServerVersion} ({info.ServerEdition})");
        }
    }

    private void SetStatus(bool connected, string text)
    {
        lblConnectionStatus.Text = text;
        lblConnectionStatus.ForeColor = connected ? UiTheme.StatusNormalText : UiTheme.StatusOutText;
    }

    private void ClearServerDetails()
    {
        foreach (Label label in new[] { lblServerVersion, lblServerEdition, lblCreated, lblItems, lblTransactions, lblLastTransaction })
        {
            label.Text = "—";
        }
    }

    private static void OpenFolder(string folder)
    {
        try
        {
            Directory.CreateDirectory(folder);
            using Process? _ = Process.Start(new ProcessStartInfo { FileName = folder, UseShellExecute = true });
        }
        catch (Exception ex)
        {
            AppLogger.Warning($"Could not open folder '{folder}'.", ex);
            MessageHelper.ShowWarning($"The folder could not be opened:\n{folder}");
        }
    }

    private static string GetConfigurationFilePath()
    {
        string? location = Assembly.GetEntryAssembly()?.Location;
        return string.IsNullOrEmpty(location)
            ? Path.Combine(AppContext.BaseDirectory, "InventoryManagement.dll.config")
            : location + ".config";
    }

    private string BuildHelpText()
    {
        string nl = Environment.NewLine;
        return
            "The connection string is read from App.config (named \"" + DatabaseConnection.DefaultConnectionStringName + "\"). " +
            "After building, Visual Studio copies App.config next to the program as InventoryManagement.dll.config." + nl + nl +
            "To use a different server or database:" + nl +
            "  1. Close the application." + nl +
            "  2. Edit the connection string - in App.config (then rebuild) or directly in:" + nl +
            "     " + GetConfigurationFilePath() + nl +
            "  3. Start the application again. If the database does not exist, you will be asked before it is created." + nl + nl +
            "Default (SQL Server Express LocalDB):" + nl +
            "  Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=InventoryManagementDB;Integrated Security=True;TrustServerCertificate=True" + nl + nl +
            "Useful LocalDB commands (Command Prompt):" + nl +
            "  sqllocaldb info                  - list LocalDB instances" + nl +
            "  sqllocaldb info MSSQLLocalDB     - show instance state" + nl +
            "  sqllocaldb start MSSQLLocalDB    - start the instance" + nl + nl +
            "The SQL scripts (Database.sql, Tables.sql, StoredProcedures.sql, SeedData.sql, RemoveDemoData.sql) are in:" + nl +
            "  " + _services.DatabaseInitializer.ScriptsDirectory;
    }

    private void ApplyTheme()
    {
        UiTheme.ApplyPage(this);
        tlpMain.BackColor = UiTheme.ContentBack;
        flpActions.BackColor = UiTheme.ContentBack;
        UiTheme.StyleCard(pnlConnection);
        UiTheme.StyleCard(pnlHelp);
        UiTheme.StyleSectionTitle(lblConnectionTitle);
        UiTheme.StyleSectionTitle(lblHelpTitle);
        UiTheme.StylePrimaryButton(btnTestConnection);
        UiTheme.StyleSecondaryButton(btnOpenLogFolder);
        UiTheme.StyleSecondaryButton(btnOpenScriptsFolder);
        txtHelp.Font = new Font("Consolas", 9.75F);
        txtHelp.ForeColor = UiTheme.TextPrimary;

        foreach (Control control in tlpInfo.Controls)
        {
            if (control is Label label && label.Name.EndsWith("Caption", StringComparison.Ordinal))
            {
                label.ForeColor = UiTheme.TextMuted;
            }
        }

        lblConnectionStatus.Font = UiTheme.BoldFont;
    }
}
