/* =============================================================================
   StoredProcedures.sql
   Step 3 of 4 - Creates / updates every stored procedure used by the
   application. Run it INSIDE the inventory database AFTER Tables.sql:
       sqlcmd -S "(localdb)\MSSQLLocalDB" -d InventoryManagementDB -i StoredProcedures.sql

   All objects use CREATE OR ALTER, so the script can be re-run at any time
   (for example after upgrading the application) without losing data.

   Business error numbers raised with THROW (translated to friendly C#
   exceptions by Data/SqlErrorTranslator.cs):
       50001  Duplicate item code
       50002  Item not found
       50003  Item is inactive
       50004  General validation error (required field, negative value ...)
       50010  Insufficient stock (checked by usp_Stock_PostTransaction)
       50011  Insufficient stock (safety trigger on StockTransactions)
       50012  Opening stock would make current stock negative
       50020  Invalid date range
       50030  Invalid quantity
       50031  Invalid transaction date
       50032  Unknown / inactive transaction type
   ============================================================================= */

SET NOCOUNT ON;
GO

IF DB_NAME() IN (N'master', N'model', N'msdb', N'tempdb')
BEGIN
    DECLARE @CurrentDatabase SYSNAME = DB_NAME();
    RAISERROR(N'StoredProcedures.sql is running in the system database [%s]. Connect to InventoryManagementDB and run it again.', 16, 1, @CurrentDatabase);
    SET NOEXEC ON;
END
GO

/* =============================================================================
   Helper: builds a safe "contains" LIKE pattern from free search text.
   Wildcard characters typed by the user (%, _, [) are escaped so they are
   searched literally. Returns NULL when there is nothing to search for.
   ============================================================================= */
CREATE OR ALTER FUNCTION dbo.fn_BuildContainsPattern (@SearchText NVARCHAR(200))
RETURNS NVARCHAR(700)
AS
BEGIN
    DECLARE @Trimmed NVARCHAR(200) = NULLIF(LTRIM(RTRIM(@SearchText)), N'');

    IF @Trimmed IS NULL
        RETURN NULL;

    RETURN N'%'
         + REPLACE(REPLACE(REPLACE(@Trimmed, N'[', N'[[]'), N'%', N'[%]'), N'_', N'[_]')
         + N'%';
END;
GO

/* =============================================================================
   ITEM MASTER
   ============================================================================= */

CREATE OR ALTER PROCEDURE dbo.usp_Item_GetAll
    @SearchText      NVARCHAR(200) = NULL,
    @IncludeInactive BIT           = 1
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Pattern NVARCHAR(700) = dbo.fn_BuildContainsPattern(@SearchText);

    SELECT ItemId, ItemCode, ItemName, Category, Unit, MinimumStock, OpeningStock, IsActive,
           CreatedDate, ModifiedDate, TotalIn, TotalOut, CurrentStock, StockStatus
    FROM dbo.vw_ItemStock
    WHERE (@IncludeInactive = 1 OR IsActive = 1)
      AND (@Pattern IS NULL
           OR ItemCode LIKE @Pattern
           OR ItemName LIKE @Pattern
           OR Category LIKE @Pattern)
    ORDER BY ItemCode;
END;
GO

CREATE OR ALTER PROCEDURE dbo.usp_Item_GetById
    @ItemId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT ItemId, ItemCode, ItemName, Category, Unit, MinimumStock, OpeningStock, IsActive,
           CreatedDate, ModifiedDate, TotalIn, TotalOut, CurrentStock, StockStatus
    FROM dbo.vw_ItemStock
    WHERE ItemId = @ItemId;
END;
GO

CREATE OR ALTER PROCEDURE dbo.usp_Item_GetCategories
AS
BEGIN
    SET NOCOUNT ON;

    SELECT DISTINCT Category
    FROM dbo.Items
    WHERE Category IS NOT NULL AND Category <> N''
    ORDER BY Category;
END;
GO

CREATE OR ALTER PROCEDURE dbo.usp_Item_GetUnits
AS
BEGIN
    SET NOCOUNT ON;

    SELECT DISTINCT Unit
    FROM dbo.Items
    ORDER BY Unit;
END;
GO

CREATE OR ALTER PROCEDURE dbo.usp_Item_Insert
    @ItemCode     NVARCHAR(50),
    @ItemName     NVARCHAR(200),
    @Category     NVARCHAR(100)  = NULL,
    @Unit         NVARCHAR(50),
    @MinimumStock DECIMAL(18, 2) = 0,
    @OpeningStock DECIMAL(18, 2) = 0,
    @IsActive     BIT            = 1,
    @ItemId       INT            OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    /* Normalise input exactly like the C# ItemService does. */
    SET @ItemCode = UPPER(LTRIM(RTRIM(@ItemCode)));
    SET @ItemName = LTRIM(RTRIM(@ItemName));
    SET @Category = NULLIF(LTRIM(RTRIM(@Category)), N'');
    SET @Unit     = UPPER(LTRIM(RTRIM(@Unit)));

    IF @ItemCode IS NULL OR @ItemCode = N'' THROW 50004, N'Item code is required.', 1;
    IF @ItemName IS NULL OR @ItemName = N'' THROW 50004, N'Item name is required.', 1;
    IF @Unit IS NULL OR @Unit = N''         THROW 50004, N'Unit is required.', 1;
    IF @MinimumStock IS NULL OR @MinimumStock < 0 THROW 50004, N'Minimum stock cannot be negative.', 1;
    IF @OpeningStock IS NULL OR @OpeningStock < 0 THROW 50004, N'Opening stock cannot be negative.', 1;

    BEGIN TRY
        BEGIN TRANSACTION;

        /* UPDLOCK + HOLDLOCK closes the race between "check" and "insert". */
        IF EXISTS (SELECT 1 FROM dbo.Items WITH (UPDLOCK, HOLDLOCK) WHERE ItemCode = @ItemCode)
            THROW 50001, N'Item code already exists.', 1;

        INSERT INTO dbo.Items (ItemCode, ItemName, Category, Unit, MinimumStock, OpeningStock, IsActive, CreatedDate)
        VALUES (@ItemCode, @ItemName, @Category, @Unit, @MinimumStock, @OpeningStock, ISNULL(@IsActive, 1), SYSDATETIME());

        SET @ItemId = CAST(SCOPE_IDENTITY() AS INT);

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        IF ERROR_NUMBER() IN (2601, 2627) THROW 50001, N'Item code already exists.', 1;
        THROW;
    END CATCH
END;
GO

CREATE OR ALTER PROCEDURE dbo.usp_Item_Update
    @ItemId       INT,
    @ItemCode     NVARCHAR(50),
    @ItemName     NVARCHAR(200),
    @Category     NVARCHAR(100)  = NULL,
    @Unit         NVARCHAR(50),
    @MinimumStock DECIMAL(18, 2) = 0,
    @OpeningStock DECIMAL(18, 2) = 0,
    @IsActive     BIT            = 1
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE @TotalIn  DECIMAL(18, 2);
    DECLARE @TotalOut DECIMAL(18, 2);
    DECLARE @Message  NVARCHAR(400);

    SET @ItemCode = UPPER(LTRIM(RTRIM(@ItemCode)));
    SET @ItemName = LTRIM(RTRIM(@ItemName));
    SET @Category = NULLIF(LTRIM(RTRIM(@Category)), N'');
    SET @Unit     = UPPER(LTRIM(RTRIM(@Unit)));

    IF @ItemCode IS NULL OR @ItemCode = N'' THROW 50004, N'Item code is required.', 1;
    IF @ItemName IS NULL OR @ItemName = N'' THROW 50004, N'Item name is required.', 1;
    IF @Unit IS NULL OR @Unit = N''         THROW 50004, N'Unit is required.', 1;
    IF @MinimumStock IS NULL OR @MinimumStock < 0 THROW 50004, N'Minimum stock cannot be negative.', 1;
    IF @OpeningStock IS NULL OR @OpeningStock < 0 THROW 50004, N'Opening stock cannot be negative.', 1;

    BEGIN TRY
        BEGIN TRANSACTION;

        /* Lock the item row so no stock posting can run while it is edited. */
        IF NOT EXISTS (SELECT 1 FROM dbo.Items WITH (UPDLOCK, HOLDLOCK) WHERE ItemId = @ItemId)
            THROW 50002, N'The selected item no longer exists.', 1;

        IF EXISTS (SELECT 1 FROM dbo.Items WITH (UPDLOCK, HOLDLOCK) WHERE ItemCode = @ItemCode AND ItemId <> @ItemId)
            THROW 50001, N'Item code already exists.', 1;

        SELECT @TotalIn = TotalIn, @TotalOut = TotalOut
        FROM dbo.vw_ItemStock
        WHERE ItemId = @ItemId;

        IF @OpeningStock + @TotalIn - @TotalOut < 0
        BEGIN
            SET @Message = CONCAT(N'Opening stock cannot be less than ',
                                  CONVERT(NVARCHAR(40), CAST(@TotalOut - @TotalIn AS DECIMAL(18, 2))),
                                  N' because that quantity has already been issued.');
            THROW 50012, @Message, 1;
        END

        UPDATE dbo.Items
        SET ItemCode     = @ItemCode,
            ItemName     = @ItemName,
            Category     = @Category,
            Unit         = @Unit,
            MinimumStock = @MinimumStock,
            OpeningStock = @OpeningStock,
            IsActive     = ISNULL(@IsActive, IsActive),
            ModifiedDate = SYSDATETIME()
        WHERE ItemId = @ItemId;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        IF ERROR_NUMBER() IN (2601, 2627) THROW 50001, N'Item code already exists.', 1;
        THROW;
    END CATCH
END;
GO

CREATE OR ALTER PROCEDURE dbo.usp_Item_SetActive
    @ItemId   INT,
    @IsActive BIT
AS
BEGIN
    SET NOCOUNT ON;

    IF @IsActive IS NULL THROW 50004, N'Active status is required.', 1;

    UPDATE dbo.Items
    SET IsActive     = @IsActive,
        ModifiedDate = SYSDATETIME()
    WHERE ItemId = @ItemId;

    IF @@ROWCOUNT = 0
        THROW 50002, N'The selected item no longer exists.', 1;
END;
GO

/* =============================================================================
   STOCK OPERATIONS
   ============================================================================= */

CREATE OR ALTER PROCEDURE dbo.usp_Stock_GetCurrentStock
    @ItemId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT ItemId, CurrentStock
    FROM dbo.vw_ItemStock
    WHERE ItemId = @ItemId;
END;
GO

/* -----------------------------------------------------------------------------
   usp_Stock_PostTransaction - the ONE place where stock is changed.

   1. Validates quantity, date and transaction type.
   2. BEGIN TRANSACTION.
   3. Locks the item row (UPDLOCK, HOLDLOCK). Every posting for the same item
      waits here, so two users can never both issue the "last" units.
   4. Reads available stock from dbo.vw_ItemStock while holding the lock.
   5. For outward types: refuses the posting when Quantity > available stock.
   6. Inserts the StockTransactions row.
   7. COMMIT. Any error -> ROLLBACK, nothing is saved.
   ----------------------------------------------------------------------------- */
CREATE OR ALTER PROCEDURE dbo.usp_Stock_PostTransaction
    @ItemId          INT,
    @TransactionType VARCHAR(20),
    @Quantity        DECIMAL(18, 2),
    @TransactionDate DATETIME2(0),
    @ReferenceNo     NVARCHAR(100)  = NULL,
    @Reason          NVARCHAR(200)  = NULL,
    @Remarks         NVARCHAR(500)  = NULL,
    @TransactionId   BIGINT         OUTPUT,
    @PreviousStock   DECIMAL(18, 2) OUTPUT,
    @NewStock        DECIMAL(18, 2) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE @Direction SMALLINT;
    DECLARE @IsActive  BIT;
    DECLARE @Unit      NVARCHAR(50);
    DECLARE @Available DECIMAL(18, 2);
    DECLARE @Message   NVARCHAR(400);

    SET @ReferenceNo = NULLIF(LTRIM(RTRIM(@ReferenceNo)), N'');
    SET @Reason      = NULLIF(LTRIM(RTRIM(@Reason)), N'');
    SET @Remarks     = NULLIF(LTRIM(RTRIM(@Remarks)), N'');

    IF @Quantity IS NULL OR @Quantity <= 0
        THROW 50030, N'Please enter a valid quantity. Quantity must be greater than zero.', 1;

    IF @TransactionDate IS NULL
        THROW 50031, N'Transaction date is required.', 1;

    IF @TransactionDate < '2000-01-01'
        THROW 50031, N'Transaction date is not valid.', 1;

    IF CAST(@TransactionDate AS DATE) > CAST(SYSDATETIME() AS DATE)
        THROW 50031, N'Transaction date cannot be in the future.', 1;

    SELECT @Direction = Direction
    FROM dbo.TransactionTypes
    WHERE TransactionType = @TransactionType AND IsActive = 1;

    IF @Direction IS NULL
        THROW 50032, N'Unknown transaction type.', 1;

    BEGIN TRY
        BEGIN TRANSACTION;

        SELECT @IsActive = IsActive, @Unit = Unit
        FROM dbo.Items WITH (UPDLOCK, HOLDLOCK)
        WHERE ItemId = @ItemId;

        IF @IsActive IS NULL
            THROW 50002, N'The selected item does not exist.', 1;

        IF @IsActive = 0
            THROW 50003, N'The selected item is inactive. Activate it in Item Master before posting stock.', 1;

        SELECT @Available = CurrentStock
        FROM dbo.vw_ItemStock
        WHERE ItemId = @ItemId;

        IF @Direction = -1 AND @Quantity > @Available
        BEGIN
            SET @Message = CONCAT(N'Insufficient stock. Available quantity: ',
                                  CONVERT(NVARCHAR(40), @Available), N' ', @Unit);
            THROW 50010, @Message, 1;
        END

        INSERT INTO dbo.StockTransactions
            (ItemId, TransactionType, Quantity, TransactionDate, ReferenceNo, Reason, Remarks, CreatedDate)
        VALUES
            (@ItemId, @TransactionType, @Quantity, @TransactionDate, @ReferenceNo, @Reason, @Remarks, SYSDATETIME());

        SET @TransactionId = CAST(SCOPE_IDENTITY() AS BIGINT);
        SET @PreviousStock = @Available;
        SET @NewStock      = @Available + (@Direction * @Quantity);

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

/* Stock IN: thin, readable wrapper around the common posting procedure. */
CREATE OR ALTER PROCEDURE dbo.usp_Stock_In
    @ItemId          INT,
    @Quantity        DECIMAL(18, 2),
    @TransactionDate DATETIME2(0),
    @ReferenceNo     NVARCHAR(100)  = NULL,
    @Reason          NVARCHAR(200)  = NULL,
    @Remarks         NVARCHAR(500)  = NULL,
    @TransactionId   BIGINT         OUTPUT,
    @PreviousStock   DECIMAL(18, 2) OUTPUT,
    @NewStock        DECIMAL(18, 2) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    EXEC dbo.usp_Stock_PostTransaction
        @ItemId          = @ItemId,
        @TransactionType = 'IN',
        @Quantity        = @Quantity,
        @TransactionDate = @TransactionDate,
        @ReferenceNo     = @ReferenceNo,
        @Reason          = @Reason,
        @Remarks         = @Remarks,
        @TransactionId   = @TransactionId OUTPUT,
        @PreviousStock   = @PreviousStock OUTPUT,
        @NewStock        = @NewStock OUTPUT;
END;
GO

/* Stock OUT: the availability check happens inside usp_Stock_PostTransaction,
   under the item lock and inside the same transaction as the INSERT. */
CREATE OR ALTER PROCEDURE dbo.usp_Stock_Out
    @ItemId          INT,
    @Quantity        DECIMAL(18, 2),
    @TransactionDate DATETIME2(0),
    @ReferenceNo     NVARCHAR(100)  = NULL,
    @Reason          NVARCHAR(200)  = NULL,
    @Remarks         NVARCHAR(500)  = NULL,
    @TransactionId   BIGINT         OUTPUT,
    @PreviousStock   DECIMAL(18, 2) OUTPUT,
    @NewStock        DECIMAL(18, 2) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    EXEC dbo.usp_Stock_PostTransaction
        @ItemId          = @ItemId,
        @TransactionType = 'OUT',
        @Quantity        = @Quantity,
        @TransactionDate = @TransactionDate,
        @ReferenceNo     = @ReferenceNo,
        @Reason          = @Reason,
        @Remarks         = @Remarks,
        @TransactionId   = @TransactionId OUTPUT,
        @PreviousStock   = @PreviousStock OUTPUT,
        @NewStock        = @NewStock OUTPUT;
END;
GO

/* Latest postings, shown under the Stock IN / Stock OUT entry screens. */
CREATE OR ALTER PROCEDURE dbo.usp_Stock_GetRecentTransactions
    @TransactionType VARCHAR(20) = NULL,
    @Top             INT         = 20
AS
BEGIN
    SET NOCOUNT ON;

    IF @Top IS NULL OR @Top < 1 SET @Top = 20;
    IF @Top > 500 SET @Top = 500;

    SELECT TOP (@Top)
        st.TransactionId, st.TransactionDate, st.ItemId, i.ItemCode, i.ItemName, i.Unit,
        st.TransactionType, tt.Direction, st.Quantity, st.ReferenceNo, st.Reason, st.Remarks, st.CreatedDate
    FROM dbo.StockTransactions AS st
    INNER JOIN dbo.Items AS i ON i.ItemId = st.ItemId
    INNER JOIN dbo.TransactionTypes AS tt ON tt.TransactionType = st.TransactionType
    WHERE (@TransactionType IS NULL OR st.TransactionType = @TransactionType)
    ORDER BY st.TransactionId DESC;
END;
GO

/* =============================================================================
   REPORTS
   ============================================================================= */

/* Report 1 - Current Stock (reads the single stock definition vw_ItemStock). */
CREATE OR ALTER PROCEDURE dbo.usp_Report_CurrentStock
    @SearchText      NVARCHAR(200) = NULL,
    @Category        NVARCHAR(100) = NULL,
    @StockStatus     VARCHAR(20)   = NULL,
    @IncludeInactive BIT           = 0
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Pattern NVARCHAR(700) = dbo.fn_BuildContainsPattern(@SearchText);
    SET @Category    = NULLIF(LTRIM(RTRIM(@Category)), N'');
    SET @StockStatus = NULLIF(LTRIM(RTRIM(@StockStatus)), '');

    SELECT ItemId, ItemCode, ItemName, Category, Unit, OpeningStock, TotalIn, TotalOut,
           CurrentStock, MinimumStock, StockStatus, IsActive
    FROM dbo.vw_ItemStock
    WHERE (@IncludeInactive = 1 OR IsActive = 1)
      AND (@Category IS NULL OR Category = @Category)
      AND (@StockStatus IS NULL OR StockStatus = @StockStatus)
      AND (@Pattern IS NULL OR ItemCode LIKE @Pattern OR ItemName LIKE @Pattern)
    ORDER BY ItemCode;
END;
GO

/* -----------------------------------------------------------------------------
   Report 2 - Stock Summary for a date range.
     Opening (period) = Items.OpeningStock + inward before FromDate - outward before FromDate
     Total IN / OUT   = movements with FromDate <= TransactionDate < ToDate + 1 day
     Closing          = Opening + Total IN - Total OUT
   ----------------------------------------------------------------------------- */
CREATE OR ALTER PROCEDURE dbo.usp_Report_StockSummary
    @FromDate        DATE,
    @ToDate          DATE,
    @ItemId          INT = NULL,
    @IncludeInactive BIT = 0
AS
BEGIN
    SET NOCOUNT ON;

    IF @FromDate IS NULL OR @ToDate IS NULL
        THROW 50020, N'Please select both From Date and To Date.', 1;

    IF @FromDate > @ToDate
        THROW 50020, N'From Date cannot be later than To Date.', 1;

    DECLARE @FromDateTime    DATETIME2(0) = CAST(@FromDate AS DATETIME2(0));
    DECLARE @ToDateExclusive DATETIME2(0) = DATEADD(DAY, 1, CAST(@ToDate AS DATETIME2(0)));

    SELECT
        i.ItemId,
        i.ItemCode,
        i.ItemName,
        i.Category,
        i.Unit,
        CAST(i.OpeningStock + ISNULL(m.InBefore, 0) - ISNULL(m.OutBefore, 0) AS DECIMAL(18, 2)) AS OpeningStock,
        CAST(ISNULL(m.InPeriod, 0)  AS DECIMAL(18, 2)) AS TotalIn,
        CAST(ISNULL(m.OutPeriod, 0) AS DECIMAL(18, 2)) AS TotalOut,
        CAST(i.OpeningStock + ISNULL(m.InBefore, 0) - ISNULL(m.OutBefore, 0)
             + ISNULL(m.InPeriod, 0) - ISNULL(m.OutPeriod, 0) AS DECIMAL(18, 2)) AS ClosingStock
    FROM dbo.Items AS i
    LEFT JOIN
    (
        SELECT
            st.ItemId,
            SUM(CASE WHEN tt.Direction = 1  AND st.TransactionDate <  @FromDateTime THEN st.Quantity ELSE 0 END) AS InBefore,
            SUM(CASE WHEN tt.Direction = -1 AND st.TransactionDate <  @FromDateTime THEN st.Quantity ELSE 0 END) AS OutBefore,
            SUM(CASE WHEN tt.Direction = 1  AND st.TransactionDate >= @FromDateTime THEN st.Quantity ELSE 0 END) AS InPeriod,
            SUM(CASE WHEN tt.Direction = -1 AND st.TransactionDate >= @FromDateTime THEN st.Quantity ELSE 0 END) AS OutPeriod
        FROM dbo.StockTransactions AS st
        INNER JOIN dbo.TransactionTypes AS tt ON tt.TransactionType = st.TransactionType
        WHERE st.TransactionDate < @ToDateExclusive
          AND (@ItemId IS NULL OR st.ItemId = @ItemId)
        GROUP BY st.ItemId
    ) AS m ON m.ItemId = i.ItemId
    WHERE (@ItemId IS NULL OR i.ItemId = @ItemId)
      AND (@IncludeInactive = 1 OR i.IsActive = 1 OR @ItemId IS NOT NULL)
    ORDER BY i.ItemCode
    OPTION (RECOMPILE);
END;
GO

/* Report 3 - Stock Movement (detailed transaction list). */
CREATE OR ALTER PROCEDURE dbo.usp_Report_StockMovement
    @FromDate        DATE,
    @ToDate          DATE,
    @ItemId          INT           = NULL,
    @TransactionType VARCHAR(20)   = NULL,
    @SearchText      NVARCHAR(200) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF @FromDate IS NULL OR @ToDate IS NULL
        THROW 50020, N'Please select both From Date and To Date.', 1;

    IF @FromDate > @ToDate
        THROW 50020, N'From Date cannot be later than To Date.', 1;

    DECLARE @FromDateTime    DATETIME2(0)  = CAST(@FromDate AS DATETIME2(0));
    DECLARE @ToDateExclusive DATETIME2(0)  = DATEADD(DAY, 1, CAST(@ToDate AS DATETIME2(0)));
    DECLARE @Pattern         NVARCHAR(700) = dbo.fn_BuildContainsPattern(@SearchText);
    SET @TransactionType = NULLIF(LTRIM(RTRIM(@TransactionType)), '');

    SELECT
        st.TransactionId, st.TransactionDate, st.ItemId, i.ItemCode, i.ItemName, i.Unit,
        st.TransactionType, tt.Direction, st.Quantity, st.ReferenceNo, st.Reason, st.Remarks, st.CreatedDate
    FROM dbo.StockTransactions AS st
    INNER JOIN dbo.Items AS i ON i.ItemId = st.ItemId
    INNER JOIN dbo.TransactionTypes AS tt ON tt.TransactionType = st.TransactionType
    WHERE st.TransactionDate >= @FromDateTime
      AND st.TransactionDate <  @ToDateExclusive
      AND (@ItemId IS NULL OR st.ItemId = @ItemId)
      AND (@TransactionType IS NULL OR st.TransactionType = @TransactionType)
      AND (@Pattern IS NULL
           OR st.ReferenceNo LIKE @Pattern
           OR st.Reason      LIKE @Pattern
           OR st.Remarks     LIKE @Pattern
           OR i.ItemCode     LIKE @Pattern
           OR i.ItemName     LIKE @Pattern)
    ORDER BY st.TransactionDate, st.TransactionId
    OPTION (RECOMPILE);
END;
GO

/* =============================================================================
   DASHBOARD
   ============================================================================= */

CREATE OR ALTER PROCEDURE dbo.usp_Dashboard_GetSummary
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @TodayStart    DATETIME2(0) = CAST(CAST(SYSDATETIME() AS DATE) AS DATETIME2(0));
    DECLARE @TomorrowStart DATETIME2(0) = DATEADD(DAY, 1, @TodayStart);

    SELECT
        (SELECT COUNT(*) FROM dbo.Items)                                   AS TotalItems,
        (SELECT COUNT(*) FROM dbo.Items WHERE IsActive = 1)                AS ActiveItems,
        (SELECT CAST(ISNULL(SUM(CurrentStock), 0) AS DECIMAL(18, 2))
           FROM dbo.vw_ItemStock WHERE IsActive = 1)                       AS TotalStockQuantity,
        (SELECT COUNT(*) FROM dbo.vw_ItemStock
           WHERE IsActive = 1 AND CurrentStock <= MinimumStock)            AS LowStockItems,
        (SELECT COUNT(*) FROM dbo.vw_ItemStock
           WHERE IsActive = 1 AND CurrentStock <= 0)                       AS OutOfStockItems,
        CAST(ISNULL(today.InQuantity, 0)  AS DECIMAL(18, 2))               AS TodayInQuantity,
        ISNULL(today.InCount, 0)                                           AS TodayInCount,
        CAST(ISNULL(today.OutQuantity, 0) AS DECIMAL(18, 2))               AS TodayOutQuantity,
        ISNULL(today.OutCount, 0)                                          AS TodayOutCount
    FROM
    (
        SELECT
            SUM(CASE WHEN tt.Direction = 1  THEN st.Quantity ELSE 0 END) AS InQuantity,
            SUM(CASE WHEN tt.Direction = 1  THEN 1 ELSE 0 END)           AS InCount,
            SUM(CASE WHEN tt.Direction = -1 THEN st.Quantity ELSE 0 END) AS OutQuantity,
            SUM(CASE WHEN tt.Direction = -1 THEN 1 ELSE 0 END)           AS OutCount
        FROM dbo.StockTransactions AS st
        INNER JOIN dbo.TransactionTypes AS tt ON tt.TransactionType = st.TransactionType
        WHERE st.TransactionDate >= @TodayStart
          AND st.TransactionDate <  @TomorrowStart
    ) AS today;
END;
GO

/* Low stock = active items where CurrentStock <= MinimumStock (includes out of stock). */
CREATE OR ALTER PROCEDURE dbo.usp_Dashboard_GetLowStockItems
AS
BEGIN
    SET NOCOUNT ON;

    SELECT ItemId, ItemCode, ItemName, Category, Unit, OpeningStock, TotalIn, TotalOut,
           CurrentStock, MinimumStock, StockStatus, IsActive
    FROM dbo.vw_ItemStock
    WHERE IsActive = 1
      AND CurrentStock <= MinimumStock
    ORDER BY CASE WHEN CurrentStock <= 0 THEN 0 ELSE 1 END, CurrentStock, ItemCode;
END;
GO

SET NOEXEC OFF;
GO
PRINT N'StoredProcedures.sql completed.';
GO
