namespace InventoryManagement.Models;

/// <summary>Details shown on the Settings &gt; Database Information screen.</summary>
public sealed class DatabaseInfo
{
    public string DataSource { get; init; } = string.Empty;

    public string DatabaseName { get; init; } = string.Empty;

    public string AuthenticationMode { get; init; } = string.Empty;

    public string ServerVersion { get; init; } = string.Empty;

    public string ServerEdition { get; init; } = string.Empty;

    public DateTime? DatabaseCreatedDate { get; init; }

    public int ItemCount { get; init; }

    public long TransactionCount { get; init; }

    public DateTime? LastTransactionDate { get; init; }
}
