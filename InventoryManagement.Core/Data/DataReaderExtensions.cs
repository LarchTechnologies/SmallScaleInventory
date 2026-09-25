using System.Globalization;
using Microsoft.Data.SqlClient;

namespace InventoryManagement.Data;

/// <summary>Column-name based, null-safe helpers for reading values from a <see cref="SqlDataReader"/>.</summary>
internal static class DataReaderExtensions
{
    public static string GetText(this SqlDataReader reader, string column)
    {
        object value = reader[column];
        return value is DBNull ? string.Empty : (string)value;
    }

    public static string? GetNullableText(this SqlDataReader reader, string column)
    {
        object value = reader[column];
        return value is DBNull ? null : (string)value;
    }

    public static int GetInt(this SqlDataReader reader, string column) =>
        Convert.ToInt32(reader[column], CultureInfo.InvariantCulture);

    public static long GetLong(this SqlDataReader reader, string column) =>
        Convert.ToInt64(reader[column], CultureInfo.InvariantCulture);

    public static decimal GetDecimalValue(this SqlDataReader reader, string column)
    {
        object value = reader[column];
        return value is DBNull ? 0m : Convert.ToDecimal(value, CultureInfo.InvariantCulture);
    }

    public static bool GetBool(this SqlDataReader reader, string column)
    {
        object value = reader[column];
        return value is not DBNull && (bool)value;
    }

    public static DateTime GetDate(this SqlDataReader reader, string column) =>
        (DateTime)reader[column];

    public static DateTime? GetNullableDate(this SqlDataReader reader, string column)
    {
        object value = reader[column];
        return value is DBNull ? null : (DateTime)value;
    }

    public static decimal GetOutputQuantity(this SqlParameter parameter) =>
        parameter.Value is DBNull or null ? 0m : Convert.ToDecimal(parameter.Value, CultureInfo.InvariantCulture);
}
