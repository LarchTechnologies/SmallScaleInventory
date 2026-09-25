using System.Data;
using Microsoft.Data.SqlClient;

namespace InventoryManagement.Data;

/// <summary>
/// Factory methods for strongly typed <see cref="SqlParameter"/>s.
/// All user input reaches SQL Server ONLY through these parameters - SQL text
/// is never built by concatenating user input.
/// </summary>
internal static class DbParameters
{
    /// <summary>DECIMAL(18,2) - the type of every quantity column.</summary>
    public const byte QuantityPrecision = 18;
    public const byte QuantityScale = 2;

    public static SqlParameter Int(string name, int? value) =>
        Create(name, SqlDbType.Int, value);

    public static SqlParameter Bit(string name, bool? value) =>
        Create(name, SqlDbType.Bit, value);

    public static SqlParameter Quantity(string name, decimal? value)
    {
        SqlParameter parameter = Create(name, SqlDbType.Decimal, value);
        parameter.Precision = QuantityPrecision;
        parameter.Scale = QuantityScale;
        return parameter;
    }

    /// <summary>Unicode text. Null, empty or whitespace is sent as SQL NULL.</summary>
    public static SqlParameter NVarChar(string name, string? value, int size)
    {
        var parameter = new SqlParameter(name, SqlDbType.NVarChar, size)
        {
            Value = string.IsNullOrWhiteSpace(value) ? DBNull.Value : value.Trim(),
        };
        return parameter;
    }

    /// <summary>Non-Unicode code values such as transaction types. Null/empty = SQL NULL.</summary>
    public static SqlParameter VarChar(string name, string? value, int size)
    {
        var parameter = new SqlParameter(name, SqlDbType.VarChar, size)
        {
            Value = string.IsNullOrWhiteSpace(value) ? DBNull.Value : value.Trim(),
        };
        return parameter;
    }

    /// <summary>SQL DATE (time part removed).</summary>
    public static SqlParameter Date(string name, DateTime? value) =>
        Create(name, SqlDbType.Date, value?.Date);

    /// <summary>SQL DATETIME2(0) (whole seconds).</summary>
    public static SqlParameter DateTime2(string name, DateTime? value)
    {
        SqlParameter parameter = Create(name, SqlDbType.DateTime2, value);
        parameter.Scale = 0;
        return parameter;
    }

    public static SqlParameter OutputInt(string name) => Output(name, SqlDbType.Int);

    public static SqlParameter OutputBigInt(string name) => Output(name, SqlDbType.BigInt);

    public static SqlParameter OutputQuantity(string name)
    {
        SqlParameter parameter = Output(name, SqlDbType.Decimal);
        parameter.Precision = QuantityPrecision;
        parameter.Scale = QuantityScale;
        return parameter;
    }

    private static SqlParameter Output(string name, SqlDbType type) =>
        new(name, type) { Direction = ParameterDirection.Output };

    private static SqlParameter Create(string name, SqlDbType type, object? value) =>
        new(name, type) { Value = value ?? DBNull.Value };
}
