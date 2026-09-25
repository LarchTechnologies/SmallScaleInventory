using System.Configuration;

namespace InventoryManagement.Helpers;

/// <summary>Typed access to the &lt;appSettings&gt; section of App.config.</summary>
public static class AppSettings
{
    /// <summary>Folder for application.log (relative to the executable or absolute). Default "Logs".</summary>
    public static string LogDirectory => Read("LogDirectory", "Logs");

    /// <summary>Number of recent transactions shown under the Stock IN / OUT screens. Default 15.</summary>
    public static int RecentTransactionCount =>
        int.TryParse(Read("RecentTransactionCount", "15"), out int value) && value is > 0 and <= 500 ? value : 15;

    private static string Read(string key, string defaultValue)
    {
        try
        {
            string? value = ConfigurationManager.AppSettings[key];
            return string.IsNullOrWhiteSpace(value) ? defaultValue : value.Trim();
        }
        catch (ConfigurationErrorsException)
        {
            return defaultValue;
        }
    }
}
