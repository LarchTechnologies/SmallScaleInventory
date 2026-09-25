namespace InventoryManagement.Models;

/// <summary>
/// Maximum text lengths - identical to the column sizes in Scripts/Tables.sql.
/// Used by validation, SQL parameters and the MaxLength of UI text boxes.
/// </summary>
public static class FieldLengths
{
    public const int ItemCode = 50;
    public const int ItemName = 200;
    public const int Category = 100;
    public const int Unit = 50;
    public const int ReferenceNo = 100;
    public const int Reason = 200;
    public const int Remarks = 500;
    public const int TransactionType = 20;
    public const int SearchText = 200;
}
