using System.Text;

namespace InventoryManagement.Logging;

/// <summary>
/// Very small thread-safe file logger.
/// Writes to Logs/application.log next to the executable. When that folder is
/// not writable (e.g. installed under Program Files) it falls back to
/// %LocalAppData%\SmallScaleInventory\Logs.
/// Never pass passwords or connection string secrets to this class.
/// </summary>
public static class AppLogger
{
    public const string LogFileName = "application.log";

    private const long MaxFileSizeBytes = 5 * 1024 * 1024;
    private static readonly object SyncRoot = new();
    private static string? _logFilePath;

    /// <summary>Full path of the current log file (after <see cref="Initialize"/>).</summary>
    public static string LogFilePath => _logFilePath ?? Path.Combine(AppContext.BaseDirectory, "Logs", LogFileName);

    public static string LogDirectory => Path.GetDirectoryName(LogFilePath) ?? AppContext.BaseDirectory;

    /// <summary>Chooses the log folder. Call once at application start-up.</summary>
    public static void Initialize(string? preferredDirectory = null)
    {
        string primary = string.IsNullOrWhiteSpace(preferredDirectory)
            ? Path.Combine(AppContext.BaseDirectory, "Logs")
            : Path.IsPathRooted(preferredDirectory)
                ? preferredDirectory
                : Path.Combine(AppContext.BaseDirectory, preferredDirectory);

        string fallback = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "SmallScaleInventory",
            "Logs");

        lock (SyncRoot)
        {
            _logFilePath = TryPrepareDirectory(primary) ?? TryPrepareDirectory(fallback);
        }
    }

    public static void Info(string message) => Write("INFO", message, null);

    public static void Warning(string message, Exception? exception = null) => Write("WARN", message, exception);

    public static void Error(string message, Exception? exception = null) => Write("ERROR", message, exception);

    private static void Write(string level, string message, Exception? exception)
    {
        var line = new StringBuilder()
            .Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"))
            .Append(" [").Append(level).Append("] ")
            .Append(message);

        if (exception is not null)
        {
            line.AppendLine().Append("    ").Append(exception.ToString().Replace(Environment.NewLine, Environment.NewLine + "    "));
        }

        try
        {
            lock (SyncRoot)
            {
                string path = LogFilePath;
                RollFileIfTooLarge(path);
                File.AppendAllText(path, line.AppendLine().ToString(), Encoding.UTF8);
            }
        }
        catch (Exception)
        {
            // Logging must never crash the application.
        }
    }

    private static void RollFileIfTooLarge(string path)
    {
        var info = new FileInfo(path);
        if (!info.Exists || info.Length < MaxFileSizeBytes)
        {
            return;
        }

        string archive = Path.ChangeExtension(path, ".previous.log");
        File.Copy(path, archive, overwrite: true);
        File.Delete(path);
    }

    private static string? TryPrepareDirectory(string directory)
    {
        try
        {
            Directory.CreateDirectory(directory);
            string path = Path.Combine(directory, LogFileName);
            using (File.Open(path, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
            {
            }

            return path;
        }
        catch (Exception)
        {
            return null;
        }
    }
}
