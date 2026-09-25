using System.Reflection;
using System.Runtime.InteropServices;
using InventoryManagement.Exceptions;
using InventoryManagement.Forms;
using InventoryManagement.Helpers;
using InventoryManagement.Logging;

namespace InventoryManagement;

internal static class Program
{
    /// <summary>Application entry point.</summary>
    [STAThread]
    private static void Main()
    {
        Application.SetHighDpiMode(HighDpiMode.SystemAware);
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.SetDefaultFont(new Font(UiTheme.FontFamilyName, 9F));

        // Centralised handling of anything no form caught.
        Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
        Application.ThreadException += (_, e) => MessageHelper.HandleException(e.Exception, "processing your request");
        AppDomain.CurrentDomain.UnhandledException += (_, e) =>
            AppLogger.Error("Fatal unhandled exception.", e.ExceptionObject as Exception);

        AppLogger.Initialize(AppSettings.LogDirectory);
        AppLogger.Info($"Application starting. Version {GetVersion()}, {RuntimeInformation.FrameworkDescription}, {RuntimeInformation.OSDescription}.");

        AppServices services;
        try
        {
            services = AppServices.CreateFromConfiguration();
        }
        catch (InventoryException ex)
        {
            AppLogger.Error("Configuration error at start-up.", ex);
            MessageHelper.ShowError(ex.Message);
            return;
        }

        AppLogger.Info($"Using database {services.Database.Description}.");

        if (!DatabaseStartupCheck.EnsureDatabaseReady(services.DatabaseInitializer))
        {
            AppLogger.Info("Start-up cancelled because the database is not ready.");
            return;
        }

        Application.Run(new MainForm(services));
        AppLogger.Info("Application closed.");
    }

    public static string GetVersion() =>
        Assembly.GetExecutingAssembly().GetName().Version?.ToString(3) ?? "1.0.0";
}
