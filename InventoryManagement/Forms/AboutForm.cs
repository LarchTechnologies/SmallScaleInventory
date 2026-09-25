using System.Runtime.InteropServices;
using InventoryManagement.Helpers;

namespace InventoryManagement.Forms;

/// <summary>Settings &gt; About dialog.</summary>
public partial class AboutForm : Form
{
    /// <summary>Designer support only.</summary>
    public AboutForm()
    {
        InitializeComponent();
    }

    public AboutForm(AppServices services)
        : this()
    {
        ArgumentNullException.ThrowIfNull(services);

        UiTheme.StylePrimaryButton(btnOk);
        lblDescription.ForeColor = UiTheme.TextPrimary;
        lblDetails.ForeColor = UiTheme.TextMuted;
        lblShortcuts.Font = new Font("Consolas", 9F);
        lblShortcuts.ForeColor = UiTheme.TextMuted;

        Text = "About " + MessageHelper.ApplicationTitle;
        lblAppName.Text = MessageHelper.ApplicationTitle;
        lblVersion.Text = $"Version {Program.GetVersion()}";
        lblDetails.Text =
            $"Runtime: {RuntimeInformation.FrameworkDescription}" + Environment.NewLine +
            "User interface: Windows Forms" + Environment.NewLine +
            "Database: SQL Server (ADO.NET + stored procedures)" + Environment.NewLine +
            $"Connected to: {services.Database.Description}";
    }
}
