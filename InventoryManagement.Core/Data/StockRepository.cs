using InventoryManagement.Models;
using InventoryManagement.Models.Reports;
using Microsoft.Data.SqlClient;

namespace InventoryManagement.Data;

/// <summary>
/// Database access for stock movements.
/// Stock is changed ONLY through usp_Stock_In / usp_Stock_Out, which run the
/// availability check and the INSERT inside one SQL transaction while the
/// item row is locked (see Scripts/StoredProcedures.sql).
/// </summary>
public sealed class StockRepository
{
    private readonly StoredProcedureExecutor _executor;

    public StockRepository(DatabaseConnection database)
    {
        _executor = new StoredProcedureExecutor(database);
    }

    /// <summary>Current stock of one item, or null when the item does not exist.</summary>
    public decimal? GetCurrentStock(int itemId)
    {
        return _executor.QuerySingleOrDefault<decimal?>(
            StoredProcedures.StockGetCurrentStock,
            r => r.GetDecimalValue("CurrentStock"),
            DbParameters.Int("@ItemId", itemId));
    }

    /// <summary>Posts a Stock IN transaction (usp_Stock_In).</summary>
    public StockOperationResult AddStock(StockTransaction transaction) =>
        Post(StoredProcedures.StockIn, transaction);

    /// <summary>Posts a Stock OUT transaction (usp_Stock_Out). Fails with InsufficientStockException when stock is too low.</summary>
    public StockOperationResult IssueStock(StockTransaction transaction) =>
        Post(StoredProcedures.StockOut, transaction);

    /// <summary>Most recent transactions, newest first. transactionType null = all types.</summary>
    public IReadOnlyList<StockMovementReportRow> GetRecentTransactions(string? transactionType, int count)
    {
        return _executor.Query(
            StoredProcedures.StockGetRecentTransactions,
            RowMappers.MapMovementRow,
            DbParameters.VarChar("@TransactionType", transactionType, FieldLengths.TransactionType),
            DbParameters.Int("@Top", count));
    }

    private StockOperationResult Post(string procedureName, StockTransaction transaction)
    {
        SqlParameter transactionId = DbParameters.OutputBigInt("@TransactionId");
        SqlParameter previousStock = DbParameters.OutputQuantity("@PreviousStock");
        SqlParameter newStock = DbParameters.OutputQuantity("@NewStock");

        _executor.Execute(
            procedureName,
            DbParameters.Int("@ItemId", transaction.ItemId),
            DbParameters.Quantity("@Quantity", transaction.Quantity),
            DbParameters.DateTime2("@TransactionDate", transaction.TransactionDate),
            DbParameters.NVarChar("@ReferenceNo", transaction.ReferenceNo, FieldLengths.ReferenceNo),
            DbParameters.NVarChar("@Reason", transaction.Reason, FieldLengths.Reason),
            DbParameters.NVarChar("@Remarks", transaction.Remarks, FieldLengths.Remarks),
            transactionId,
            previousStock,
            newStock);

        return new StockOperationResult
        {
            TransactionId = (long)transactionId.Value,
            PreviousStock = previousStock.GetOutputQuantity(),
            NewStock = newStock.GetOutputQuantity(),
        };
    }
}
