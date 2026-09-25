namespace InventoryManagement.Data;

/// <summary>
/// Error numbers used by THROW in the stored procedures / triggers, plus the
/// SQL Server system error numbers the application reacts to.
/// </summary>
internal static class DatabaseErrorNumbers
{
    // ---- Business errors raised by Scripts/*.sql ----------------------------
    public const int FirstBusinessError = 50000;
    public const int LastBusinessError = 50999;

    public const int DuplicateItemCode = 50001;
    public const int ItemNotFound = 50002;
    public const int ItemInactive = 50003;
    public const int ValidationFailed = 50004;
    public const int InsufficientStock = 50010;
    public const int InsufficientStockTrigger = 50011;
    public const int OpeningStockBelowIssued = 50012;
    public const int InvalidDateRange = 50020;
    public const int InvalidQuantity = 50030;
    public const int InvalidTransactionDate = 50031;
    public const int UnknownTransactionType = 50032;

    // ---- SQL Server system errors --------------------------------------------
    public const int Timeout = -2;
    public const int CannotOpenDatabase = 4060;
    public const int LoginFailed = 18456;
    public const int Deadlock = 1205;
    public const int InvalidObjectName = 208;
    public const int StoredProcedureNotFound = 2812;
    public const int ConstraintViolation = 547;
    public const int UniqueIndexViolation = 2601;
    public const int UniqueConstraintViolation = 2627;
    public const int ArithmeticOverflow = 8115;

    /// <summary>Network / instance errors meaning "the server cannot be reached".</summary>
    public static readonly int[] ConnectionErrors =
    {
        -1, 2, 26, 40, 50, 53, 121, 233, 1225, 10053, 10054, 10060, 10061, 11001,
    };
}
