using InventoryManagement.Logging;

namespace InventoryManagement.Helpers;

/// <summary>
/// Runs at start-up before the main window opens. Uses DatabaseInitializer to
/// find out whether the database is ready and - only after asking the user -
/// creates the database, installs missing objects or loads demo data.
/// Nothing is ever created silently.
/// </summary>
public static class DatabaseStartupCheck
{
    private const int MaxMissingObjectsListed = 8;

    /// <summary>Returns true when the application can start.</summary>
    public static bool EnsureDatabaseReady(DatabaseInitializer initializer)
    {
        while (true)
        {
            DatabaseStatus status;
            using (new WaitCursorScope())
            {
                status = initializer.CheckStatus();
            }

            AppLogger.Info($"Database status: {status.State}. {status.Message}");

            switch (status.State)
            {
                case DatabaseState.Ready:
                    return true;

                case DatabaseState.ServerUnavailable:
                    if (!AskRetryAfterConnectionFailure(initializer, status))
                    {
                        return false;
                    }

                    break;

                case DatabaseState.DatabaseMissing:
                    if (!OfferToCreateDatabase(initializer))
                    {
                        return false;
                    }

                    break;

                case DatabaseState.SchemaIncomplete:
                    if (!OfferToRepairSchema(initializer, status))
                    {
                        return false;
                    }

                    break;

                default:
                    return false;
            }
        }
    }

    private static bool AskRetryAfterConnectionFailure(DatabaseInitializer initializer, DatabaseStatus status)
    {
        string message =
            status.Message + Environment.NewLine + Environment.NewLine +
            $"Server:   {initializer.DataSource}" + Environment.NewLine +
            $"Database: {initializer.DatabaseName}" + Environment.NewLine + Environment.NewLine +
            "Please check that SQL Server Express LocalDB is installed and running " +
            "(open a Command Prompt and run: sqllocaldb info MSSQLLocalDB)." + Environment.NewLine +
            "The connection string is in InventoryManagement.dll.config (App.config)." + Environment.NewLine + Environment.NewLine +
            $"Log file: {AppLogger.LogFilePath}" + Environment.NewLine + Environment.NewLine +
            "Click Retry to try again or Cancel to exit.";

        return MessageBox.Show(message, MessageHelper.ApplicationTitle, MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
            == DialogResult.Retry;
    }

    private static bool OfferToCreateDatabase(DatabaseInitializer initializer)
    {
        bool create = MessageHelper.Confirm(
            $"The database '{initializer.DatabaseName}' was not found on {initializer.DataSource}." + Environment.NewLine + Environment.NewLine +
            "Do you want to create it now?" + Environment.NewLine + Environment.NewLine +
            "This creates an empty database and runs Tables.sql and StoredProcedures.sql " +
            $"from:{Environment.NewLine}{initializer.ScriptsDirectory}" + Environment.NewLine + Environment.NewLine +
            "Choose No to exit and set up the database manually (see README.md, section 14 'Setup for beginners').");

        if (!create)
        {
            return false;
        }

        try
        {
            using (new WaitCursorScope())
            {
                initializer.CreateDatabase();
                initializer.InstallOrUpgradeSchema();
            }
        }
        catch (Exception ex)
        {
            MessageHelper.HandleException(ex, "creating the database");
            return false;
        }

        OfferDemoData(initializer);
        MessageHelper.ShowSuccess($"The database '{initializer.DatabaseName}' is ready.");
        return true;
    }

    private static bool OfferToRepairSchema(DatabaseInitializer initializer, DatabaseStatus status)
    {
        IEnumerable<string> listed = status.MissingObjects.Take(MaxMissingObjectsListed);
        string more = status.MissingObjects.Count > MaxMissingObjectsListed
            ? $"{Environment.NewLine}  ... and {status.MissingObjects.Count - MaxMissingObjectsListed} more"
            : string.Empty;

        bool repair = MessageHelper.Confirm(
            status.Message + Environment.NewLine + Environment.NewLine +
            "Missing:" + Environment.NewLine + "  " + string.Join(Environment.NewLine + "  ", listed) + more + Environment.NewLine + Environment.NewLine +
            "Install the missing objects now? This runs Tables.sql and StoredProcedures.sql. Existing data is kept." + Environment.NewLine + Environment.NewLine +
            "Choose No to exit.");

        if (!repair)
        {
            return false;
        }

        try
        {
            using (new WaitCursorScope())
            {
                initializer.InstallOrUpgradeSchema();
            }

            return true;
        }
        catch (Exception ex)
        {
            MessageHelper.HandleException(ex, "installing the database objects");
            return false;
        }
    }

    private static void OfferDemoData(DatabaseInitializer initializer)
    {
        bool loadDemo = MessageHelper.Confirm(
            "Do you want to load DEMO data (5 sample items and a few stock transactions)?" + Environment.NewLine + Environment.NewLine +
            "Recommended for trying out the application. Demo transactions use reference numbers starting with 'DEMO-' " +
            "and can be removed later with Scripts\\RemoveDemoData.sql." + Environment.NewLine + Environment.NewLine +
            "Choose No to start with an empty database.",
            defaultToNo: true);

        if (!loadDemo)
        {
            return;
        }

        try
        {
            using (new WaitCursorScope())
            {
                initializer.LoadDemoData();
            }
        }
        catch (Exception ex)
        {
            MessageHelper.HandleException(ex, "loading the demo data");
        }
    }
}
