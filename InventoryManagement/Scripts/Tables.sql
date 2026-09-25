/* =============================================================================
   Tables.sql
   Step 2 of 4 - Creates tables, constraints, indexes, the stock view and the
   integrity triggers inside InventoryManagementDB.

   Run this script INSIDE the inventory database:
       sqlcmd -S "(localdb)\MSSQLLocalDB" -d InventoryManagementDB -i Tables.sql

   The script is idempotent (safe to run again). Existing tables and data are
   never dropped; missing objects are created and views/triggers are refreshed.

   Stock design:
     * Items.OpeningStock is the quantity on hand when the item was set up.
     * Every later stock change is a row in dbo.StockTransactions.
     * Current stock is NEVER stored. It is always derived:
           CurrentStock = OpeningStock + SUM(inward qty) - SUM(outward qty)
       The single definition of this formula is the view dbo.vw_ItemStock.
     * dbo.TransactionTypes tells whether a transaction type adds (+1) or
       removes (-1) stock, so new types (ADJ_IN, TRANSFER_OUT ...) can be
       added later without changing any table or report.
   ============================================================================= */

SET NOCOUNT ON;
GO

/* Safety guard: refuse to create tables in a system database. */
IF DB_NAME() IN (N'master', N'model', N'msdb', N'tempdb')
BEGIN
    DECLARE @CurrentDatabase SYSNAME = DB_NAME();
    RAISERROR(N'Tables.sql is running in the system database [%s]. Connect to InventoryManagementDB (sqlcmd -d InventoryManagementDB) and run it again.', 16, 1, @CurrentDatabase);
    SET NOEXEC ON;
END
GO

/* -----------------------------------------------------------------------------
   TransactionTypes - lookup table for stock movement types
   ----------------------------------------------------------------------------- */
IF OBJECT_ID(N'dbo.TransactionTypes', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.TransactionTypes
    (
        TransactionType VARCHAR(20)   NOT NULL CONSTRAINT PK_TransactionTypes PRIMARY KEY,
        Description     NVARCHAR(100) NOT NULL,
        Direction       SMALLINT      NOT NULL,
        IsActive        BIT           NOT NULL CONSTRAINT DF_TransactionTypes_IsActive DEFAULT (1),
        CONSTRAINT CK_TransactionTypes_Direction CHECK (Direction IN (1, -1))
    );
    PRINT N'Table dbo.TransactionTypes created.';
END
GO

/* Version 1 transaction types (reference data, required by the application). */
IF NOT EXISTS (SELECT 1 FROM dbo.TransactionTypes WHERE TransactionType = 'IN')
    INSERT INTO dbo.TransactionTypes (TransactionType, Description, Direction) VALUES ('IN', N'Stock In', 1);

IF NOT EXISTS (SELECT 1 FROM dbo.TransactionTypes WHERE TransactionType = 'OUT')
    INSERT INTO dbo.TransactionTypes (TransactionType, Description, Direction) VALUES ('OUT', N'Stock Out', -1);
GO

/* -----------------------------------------------------------------------------
   Items - item / product master
   ----------------------------------------------------------------------------- */
IF OBJECT_ID(N'dbo.Items', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Items
    (
        ItemId       INT IDENTITY(1, 1) NOT NULL CONSTRAINT PK_Items PRIMARY KEY,
        ItemCode     NVARCHAR(50)       NOT NULL,
        ItemName     NVARCHAR(200)      NOT NULL,
        Category     NVARCHAR(100)      NULL,
        Unit         NVARCHAR(50)       NOT NULL,
        MinimumStock DECIMAL(18, 2)     NOT NULL CONSTRAINT DF_Items_MinimumStock DEFAULT (0),
        OpeningStock DECIMAL(18, 2)     NOT NULL CONSTRAINT DF_Items_OpeningStock DEFAULT (0),
        IsActive     BIT                NOT NULL CONSTRAINT DF_Items_IsActive DEFAULT (1),
        CreatedDate  DATETIME2(0)       NOT NULL CONSTRAINT DF_Items_CreatedDate DEFAULT (SYSDATETIME()),
        ModifiedDate DATETIME2(0)       NULL,
        CONSTRAINT UQ_Items_ItemCode          UNIQUE (ItemCode),
        CONSTRAINT CK_Items_ItemCode_NotBlank CHECK (LEN(ItemCode) > 0),
        CONSTRAINT CK_Items_ItemName_NotBlank CHECK (LEN(ItemName) > 0),
        CONSTRAINT CK_Items_Unit_NotBlank     CHECK (LEN(Unit) > 0),
        CONSTRAINT CK_Items_MinimumStock      CHECK (MinimumStock >= 0),
        CONSTRAINT CK_Items_OpeningStock      CHECK (OpeningStock >= 0)
    );
    PRINT N'Table dbo.Items created.';
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_Items_Category' AND object_id = OBJECT_ID(N'dbo.Items'))
    CREATE NONCLUSTERED INDEX IX_Items_Category ON dbo.Items (Category);
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_Items_ItemName' AND object_id = OBJECT_ID(N'dbo.Items'))
    CREATE NONCLUSTERED INDEX IX_Items_ItemName ON dbo.Items (ItemName);
GO

/* -----------------------------------------------------------------------------
   StockTransactions - every stock movement (IN, OUT and future types)
   ----------------------------------------------------------------------------- */
IF OBJECT_ID(N'dbo.StockTransactions', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.StockTransactions
    (
        TransactionId   BIGINT IDENTITY(1, 1) NOT NULL CONSTRAINT PK_StockTransactions PRIMARY KEY,
        ItemId          INT                   NOT NULL,
        TransactionType VARCHAR(20)           NOT NULL,
        Quantity        DECIMAL(18, 2)        NOT NULL,
        TransactionDate DATETIME2(0)          NOT NULL,
        ReferenceNo     NVARCHAR(100)         NULL,
        Reason          NVARCHAR(200)         NULL,
        Remarks         NVARCHAR(500)         NULL,
        CreatedDate     DATETIME2(0)          NOT NULL CONSTRAINT DF_StockTransactions_CreatedDate DEFAULT (SYSDATETIME()),
        CONSTRAINT FK_StockTransactions_Items
            FOREIGN KEY (ItemId) REFERENCES dbo.Items (ItemId),
        CONSTRAINT FK_StockTransactions_TransactionTypes
            FOREIGN KEY (TransactionType) REFERENCES dbo.TransactionTypes (TransactionType),
        CONSTRAINT CK_StockTransactions_Quantity CHECK (Quantity > 0)
    );
    PRINT N'Table dbo.StockTransactions created.';
END
GO

/* Used by stock calculation per item and by item-filtered reports. */
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_StockTransactions_ItemId_TransactionDate' AND object_id = OBJECT_ID(N'dbo.StockTransactions'))
    CREATE NONCLUSTERED INDEX IX_StockTransactions_ItemId_TransactionDate
        ON dbo.StockTransactions (ItemId, TransactionDate)
        INCLUDE (TransactionType, Quantity);
GO

/* Used by date-range reports (summary, movement, dashboard "today" figures). */
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_StockTransactions_TransactionDate' AND object_id = OBJECT_ID(N'dbo.StockTransactions'))
    CREATE NONCLUSTERED INDEX IX_StockTransactions_TransactionDate
        ON dbo.StockTransactions (TransactionDate)
        INCLUDE (ItemId, TransactionType, Quantity);
GO

/* -----------------------------------------------------------------------------
   vw_ItemStock - THE single definition of current stock and stock status.
   Every screen and report that shows current stock reads from this view.
   ----------------------------------------------------------------------------- */
CREATE OR ALTER VIEW dbo.vw_ItemStock
AS
SELECT
    i.ItemId,
    i.ItemCode,
    i.ItemName,
    i.Category,
    i.Unit,
    i.MinimumStock,
    i.OpeningStock,
    i.IsActive,
    i.CreatedDate,
    i.ModifiedDate,
    CAST(ISNULL(t.TotalIn, 0)  AS DECIMAL(18, 2)) AS TotalIn,
    CAST(ISNULL(t.TotalOut, 0) AS DECIMAL(18, 2)) AS TotalOut,
    CAST(s.CurrentStock AS DECIMAL(18, 2))        AS CurrentStock,
    CAST(CASE
             WHEN s.CurrentStock <= 0             THEN 'OUT OF STOCK'
             WHEN s.CurrentStock <= i.MinimumStock THEN 'LOW STOCK'
             ELSE 'NORMAL'
         END AS VARCHAR(20))                      AS StockStatus
FROM dbo.Items AS i
LEFT JOIN
(
    SELECT
        st.ItemId,
        SUM(CASE WHEN tt.Direction = 1  THEN st.Quantity ELSE 0 END) AS TotalIn,
        SUM(CASE WHEN tt.Direction = -1 THEN st.Quantity ELSE 0 END) AS TotalOut
    FROM dbo.StockTransactions AS st
    INNER JOIN dbo.TransactionTypes AS tt ON tt.TransactionType = st.TransactionType
    GROUP BY st.ItemId
) AS t ON t.ItemId = i.ItemId
CROSS APPLY (SELECT i.OpeningStock + ISNULL(t.TotalIn, 0) - ISNULL(t.TotalOut, 0) AS CurrentStock) AS s;
GO

/* -----------------------------------------------------------------------------
   Integrity triggers (defence in depth).
   The stored procedures already refuse negative stock with friendly messages.
   These triggers guarantee that NO statement - including manual SQL typed in
   SSMS - can leave an item with negative stock.
   ----------------------------------------------------------------------------- */
CREATE OR ALTER TRIGGER dbo.TR_StockTransactions_PreventNegativeStock
ON dbo.StockTransactions
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS
    (
        SELECT 1
        FROM dbo.vw_ItemStock AS v
        WHERE v.CurrentStock < 0
          AND v.ItemId IN (SELECT ItemId FROM inserted UNION SELECT ItemId FROM deleted)
    )
    BEGIN
        THROW 50011, N'Insufficient stock. This change would make the stock quantity negative and has been cancelled.', 1;
    END
END;
GO

CREATE OR ALTER TRIGGER dbo.TR_Items_PreventNegativeStock
ON dbo.Items
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    IF UPDATE(OpeningStock)
       AND EXISTS
       (
           SELECT 1
           FROM dbo.vw_ItemStock AS v
           INNER JOIN inserted AS ins ON ins.ItemId = v.ItemId
           WHERE v.CurrentStock < 0
       )
    BEGIN
        THROW 50012, N'Opening stock cannot be reduced below the quantity that has already been issued.', 1;
    END
END;
GO

SET NOEXEC OFF;
GO
PRINT N'Tables.sql completed.';
GO
