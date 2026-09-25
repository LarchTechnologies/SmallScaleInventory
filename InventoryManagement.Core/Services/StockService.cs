using InventoryManagement.Data;
using InventoryManagement.Exceptions;
using InventoryManagement.Helpers;
using InventoryManagement.Logging;
using InventoryManagement.Models;
using InventoryManagement.Models.Reports;

namespace InventoryManagement.Services;

/// <summary>
/// Business logic for Stock IN and Stock OUT.
///
/// Validation happens in three layers:
///   1. The form checks the input can be parsed (quick feedback).
///   2. This service applies the business rules (quantity &gt; 0, valid date,
///      item exists and is active, OUT quantity &lt;= available stock).
///   3. The stored procedure repeats the checks INSIDE a SQL transaction with
///      the item row locked, so concurrent users can never drive stock negative.
/// </summary>
public sealed class StockService
{
    public const int DefaultRecentTransactionCount = 15;

    private readonly StockRepository _stockRepository;
    private readonly ItemRepository _itemRepository;

    public StockService(StockRepository stockRepository, ItemRepository itemRepository)
    {
        _stockRepository = stockRepository ?? throw new ArgumentNullException(nameof(stockRepository));
        _itemRepository = itemRepository ?? throw new ArgumentNullException(nameof(itemRepository));
    }

    /// <summary>Current stock of an item as calculated by the database.</summary>
    public decimal GetCurrentStock(int itemId)
    {
        RequireItemSelected(itemId);
        return _stockRepository.GetCurrentStock(itemId)
            ?? throw new RecordNotFoundException("The selected item no longer exists.");
    }

    /// <summary>Records a Stock IN (receipt). Current stock increases by the quantity.</summary>
    public StockOperationResult AddStock(StockTransaction transaction)
    {
        ArgumentNullException.ThrowIfNull(transaction);

        transaction.TransactionType = TransactionTypes.In;
        Item item = ValidateTransaction(transaction);

        StockOperationResult result = _stockRepository.AddStock(transaction);

        AppLogger.Info($"Stock IN saved: {item.ItemCode} +{transaction.Quantity:N2} {item.Unit} (Txn {result.TransactionId}), new stock {result.NewStock:N2}.");
        return result;
    }

    /// <summary>
    /// Records a Stock OUT (issue). Refused with <see cref="InsufficientStockException"/>
    /// when the quantity exceeds the available stock.
    /// </summary>
    public StockOperationResult IssueStock(StockTransaction transaction)
    {
        ArgumentNullException.ThrowIfNull(transaction);

        transaction.TransactionType = TransactionTypes.Out;
        Item item = ValidateTransaction(transaction);

        // Early, friendly check. The authoritative check is repeated by
        // usp_Stock_PostTransaction under a row lock inside the SQL transaction.
        if (StockCalculator.WouldGoNegative(item.CurrentStock, transaction.Quantity, TransactionTypes.Out))
        {
            throw InsufficientStockException.ForAvailable(item.CurrentStock, item.Unit);
        }

        StockOperationResult result = _stockRepository.IssueStock(transaction);

        AppLogger.Info($"Stock OUT saved: {item.ItemCode} -{transaction.Quantity:N2} {item.Unit} (Txn {result.TransactionId}), new stock {result.NewStock:N2}.");
        return result;
    }

    /// <summary>Latest transactions of one type (newest first) for the entry screens.</summary>
    public IReadOnlyList<StockMovementReportRow> GetRecentTransactions(string transactionType, int count = DefaultRecentTransactionCount)
    {
        if (!TransactionTypes.IsValid(transactionType))
        {
            throw new ValidationException("Unknown transaction type.");
        }

        return _stockRepository.GetRecentTransactions(transactionType, count);
    }

    /// <summary>Applies all business rules and returns the (fresh) item from the database.</summary>
    private Item ValidateTransaction(StockTransaction transaction)
    {
        RequireItemSelected(transaction.ItemId);
        ValidationHelper.RequireValidQuantity(transaction.Quantity);
        ValidationHelper.RequireValidTransactionDate(transaction.TransactionDate);

        transaction.ReferenceNo = ValidationHelper.OptionalText(transaction.ReferenceNo, "Reference number", FieldLengths.ReferenceNo, nameof(StockTransaction.ReferenceNo));
        transaction.Reason = ValidationHelper.OptionalText(transaction.Reason, "Reason", FieldLengths.Reason, nameof(StockTransaction.Reason));
        transaction.Remarks = ValidationHelper.OptionalText(transaction.Remarks, "Remarks", FieldLengths.Remarks, nameof(StockTransaction.Remarks));

        Item item = _itemRepository.GetById(transaction.ItemId)
            ?? throw new RecordNotFoundException("The selected item no longer exists.");

        if (!item.IsActive)
        {
            throw new ValidationException("The selected item is inactive. Activate it in Item Master before posting stock.", "ItemId");
        }

        return item;
    }

    private static void RequireItemSelected(int itemId)
    {
        if (itemId <= 0)
        {
            throw new ValidationException("Please select an item.", "ItemId");
        }
    }
}
