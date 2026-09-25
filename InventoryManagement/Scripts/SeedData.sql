/* =============================================================================
   SeedData.sql  (OPTIONAL)
   Step 4 of 4 - Loads demo items and demo stock movements so that every
   screen and report has something to show immediately.

   Run it INSIDE the inventory database AFTER Tables.sql and StoredProcedures.sql:
       sqlcmd -S "(localdb)\MSSQLLocalDB" -d InventoryManagementDB -i SeedData.sql

   * Safe to run more than once: items are only added when their code does not
     exist, and transactions are only added when no demo transactions exist.
   * Every demo transaction has a ReferenceNo starting with "DEMO-".
   * Transaction dates are relative to TODAY, so the dashboard shows "today"
     figures right after seeding.
   * To remove the demo data again run RemoveDemoData.sql.
   * To skip demo data completely simply do not run this script (the
     application asks before loading it).
   ============================================================================= */

SET NOCOUNT ON;
SET XACT_ABORT ON;
GO

IF DB_NAME() IN (N'master', N'model', N'msdb', N'tempdb')
BEGIN
    DECLARE @CurrentDatabase SYSNAME = DB_NAME();
    RAISERROR(N'SeedData.sql is running in the system database [%s]. Connect to InventoryManagementDB and run it again.', 16, 1, @CurrentDatabase);
    SET NOEXEC ON;
END
GO

BEGIN TRANSACTION;

/* ---------------------------------------------------------------- Items --- */
DECLARE @DemoItems TABLE
(
    ItemCode     NVARCHAR(50),
    ItemName     NVARCHAR(200),
    Category     NVARCHAR(100),
    Unit         NVARCHAR(50),
    MinimumStock DECIMAL(18, 2),
    OpeningStock DECIMAL(18, 2)
);

INSERT INTO @DemoItems (ItemCode, ItemName, Category, Unit, MinimumStock, OpeningStock)
VALUES
    (N'RM-001', N'Raw Material A',     N'Raw Materials',    N'KG',  100, 500),
    (N'RM-002', N'Raw Material B',     N'Raw Materials',    N'KG',   50, 250),
    (N'FG-001', N'Finished Product A', N'Finished Goods',   N'NOS',  20, 100),
    (N'PK-001', N'Carton Box (Large)', N'Packing Material', N'NOS', 200, 300),
    (N'CN-001', N'Machine Oil',        N'Consumables',      N'LTR',  10,  20);

INSERT INTO dbo.Items (ItemCode, ItemName, Category, Unit, MinimumStock, OpeningStock, IsActive, CreatedDate)
SELECT d.ItemCode, d.ItemName, d.Category, d.Unit, d.MinimumStock, d.OpeningStock, 1,
       DATEADD(DAY, -30, SYSDATETIME())
FROM @DemoItems AS d
WHERE NOT EXISTS (SELECT 1 FROM dbo.Items AS i WHERE i.ItemCode = d.ItemCode);

PRINT CONCAT(N'Demo items added: ', @@ROWCOUNT);

/* --------------------------------------------------------- Transactions --- */
IF NOT EXISTS (SELECT 1 FROM dbo.StockTransactions WHERE ReferenceNo LIKE N'DEMO-%')
BEGIN
    DECLARE @Today DATETIME2(0) = CAST(CAST(SYSDATETIME() AS DATE) AS DATETIME2(0));

    DECLARE @DemoTransactions TABLE
    (
        Seq             INT,
        ItemCode        NVARCHAR(50),
        TransactionType VARCHAR(20),
        Quantity        DECIMAL(18, 2),
        DaysAgo         INT,
        HourOfDay       INT,
        ReferenceNo     NVARCHAR(100),
        Reason          NVARCHAR(200),
        Remarks         NVARCHAR(500)
    );

    INSERT INTO @DemoTransactions (Seq, ItemCode, TransactionType, Quantity, DaysAgo, HourOfDay, ReferenceNo, Reason, Remarks)
    VALUES
        ( 1, N'RM-001', 'IN',  200.00, 20, 10, N'DEMO-GRN-1001', N'Purchase',             N'Supplier delivery'),
        ( 2, N'RM-002', 'IN',  100.00, 18, 11, N'DEMO-GRN-1002', N'Purchase',             N'Supplier delivery'),
        ( 3, N'RM-001', 'OUT', 150.00, 15,  9, N'DEMO-ISS-2001', N'Issue to production',  N'Batch 15'),
        ( 4, N'FG-001', 'IN',   60.00, 12, 16, N'DEMO-PRD-3001', N'Production output',    N'Batch 15 finished'),
        ( 5, N'RM-002', 'OUT',  80.00, 10,  9, N'DEMO-ISS-2002', N'Issue to production',  N'Batch 16'),
        ( 6, N'PK-001', 'OUT', 150.00,  9, 14, N'DEMO-ISS-2003', N'Issue to packing',     NULL),
        ( 7, N'FG-001', 'OUT',  70.00,  7, 15, N'DEMO-DSP-4001', N'Sale / dispatch',      N'Customer order'),
        ( 8, N'CN-001', 'OUT',  12.50,  6, 10, N'DEMO-ISS-2004', N'Maintenance',          N'Machine servicing'),
        ( 9, N'RM-001', 'OUT', 120.00,  3,  9, N'DEMO-ISS-2005', N'Issue to production',  N'Batch 17'),
        (10, N'PK-001', 'OUT',  60.00,  2, 13, N'DEMO-ISS-2006', N'Issue to packing',     NULL),
        (11, N'CN-001', 'OUT',   7.50,  1, 10, N'DEMO-ISS-2007', N'Maintenance',          N'Oil change'),
        (12, N'RM-001', 'IN',   50.00,  0,  8, N'DEMO-GRN-1003', N'Purchase',             N'Morning delivery'),
        (13, N'RM-002', 'OUT',  40.00,  0,  8, N'DEMO-ISS-2008', N'Issue to production',  N'Batch 18'),
        (14, N'FG-001', 'OUT',  75.00,  0,  8, N'DEMO-DSP-4002', N'Sale / dispatch',      N'Customer order');

    INSERT INTO dbo.StockTransactions (ItemId, TransactionType, Quantity, TransactionDate, ReferenceNo, Reason, Remarks, CreatedDate)
    SELECT i.ItemId, d.TransactionType, d.Quantity,
           DATEADD(HOUR, d.HourOfDay, DATEADD(DAY, -d.DaysAgo, @Today)),
           d.ReferenceNo, d.Reason, d.Remarks, SYSDATETIME()
    FROM @DemoTransactions AS d
    INNER JOIN dbo.Items AS i ON i.ItemCode = d.ItemCode
    ORDER BY d.Seq;

    PRINT CONCAT(N'Demo transactions added: ', @@ROWCOUNT);
END
ELSE
BEGIN
    PRINT N'Demo transactions already exist. Skipped.';
END

COMMIT TRANSACTION;
GO

/* Expected current stock after seeding (with no other transactions):
     RM-001  500 + 200 + 50 - 150 - 120 = 480.00 KG   NORMAL
     RM-002  250 + 100 - 80 - 40        = 230.00 KG   NORMAL
     FG-001  100 + 60 - 70 - 75         =  15.00 NOS  LOW STOCK    (min 20)
     PK-001  300 - 150 - 60             =  90.00 NOS  LOW STOCK    (min 200)
     CN-001  20 - 12.50 - 7.50          =   0.00 LTR  OUT OF STOCK
*/

SET NOEXEC OFF;
GO
PRINT N'SeedData.sql completed.';
GO
