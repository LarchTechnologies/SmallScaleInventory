using InventoryManagement.Exceptions;
using InventoryManagement.Logging;

namespace InventoryManagement.Helpers;

/// <summary>
/// All message boxes go through this class so wording, titles and icons are
/// consistent, and so every unexpected error is logged before it is shown.
/// </summary>
public static class MessageHelper
{
    public const string ApplicationTitle = "Small Scale Inventory";

    public static void ShowInfo(string message) =>
        MessageBox.Show(message, ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);

    public static void ShowSuccess(string message) =>
        MessageBox.Show(message, ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);

    public static void ShowWarning(string message) =>
        MessageBox.Show(message, ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);

    public static void ShowError(string message) =>
        MessageBox.Show(message, ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);

    /// <summary>Yes/No question. Returns true when the user answers Yes.</summary>
    public static bool Confirm(string question, bool defaultToNo = false) =>
        MessageBox.Show(
            question,
            ApplicationTitle,
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question,
            defaultToNo ? MessageBoxDefaultButton.Button2 : MessageBoxDefaultButton.Button1) == DialogResult.Yes;

    /// <summary>
    /// Central handler used by every form's catch block.
    ///  - Validation / business errors: friendly warning (not logged as errors).
    ///  - Database errors: friendly error (already logged with details by the data layer).
    ///  - Anything else: logged with stack trace, generic message shown.
    /// SQL exception text is never shown directly.
    /// </summary>
    public static void HandleException(Exception exception, string action)
    {
        switch (exception)
        {
            case ValidationException validation:
                ShowWarning(validation.Message);
                break;

            case RecordNotFoundException notFound:
                ShowWarning(notFound.Message);
                break;

            case DatabaseUnavailableException unavailable:
                ShowError(unavailable.Message + Environment.NewLine + Environment.NewLine + "Details were written to: " + AppLogger.LogFilePath);
                break;

            case InventoryException inventory:
                ShowError(inventory.Message);
                break;

            default:
                AppLogger.Error($"Unexpected error while {action}.", exception);
                ShowError($"An unexpected error occurred while {action}.{Environment.NewLine}{Environment.NewLine}Technical details were written to the application log:{Environment.NewLine}{AppLogger.LogFilePath}");
                break;
        }
    }
}
