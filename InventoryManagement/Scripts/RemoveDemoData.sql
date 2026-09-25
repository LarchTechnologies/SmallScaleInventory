/* =============================================================================
   RemoveDemoData.sql  (OPTIONAL)
   Removes the demo data created by SeedData.sql.

       sqlcmd -S "(localdb)\MSSQLLocalDB" -d InventoryManagementDB -i RemoveDemoData.sql

   * Deletes every transaction whose ReferenceNo starts with "DEMO-".
   * Deletes the demo items (RM-001, RM-002, FG-001, PK-001, CN-001) only when
     they have no remaining (real) transactions. Items that you have started
     using for real business are kept.
   * Runs in a single transaction: if anything fails nothing is removed.
     (Example: you issued real stock from a demo item and removing the demo
     IN transactions would make that stock negative - the safety trigger will
     refuse and the whole script is rolled back.)
   ============================================================================= */

SET NOCOUNT ON;
SET XACT_ABORT ON;
GO

IF DB_NAME() IN (N'master', N'model', N'msdb', N'tempdb')
BEGIN
    DECLARE @CurrentDatabase SYSNAME = DB_NAME();
    RAISERROR(N'RemoveDemoData.sql is running in the system database [%s]. Connect to InventoryManagementDB and run it again.', 16, 1, @CurrentDatabase);
    SET NOEXEC ON;
END
GO

BEGIN TRANSACTION;

DELETE FROM dbo.StockTransactions
WHERE ReferenceNo LIKE N'DEMO-%';

PRINT CONCAT(N'Demo transactions removed: ', @@ROWCOUNT);

DELETE i
FROM dbo.Items AS i
WHERE i.ItemCode IN (N'RM-001', N'RM-002', N'FG-001', N'PK-001', N'CN-001')
  AND NOT EXISTS (SELECT 1 FROM dbo.StockTransactions AS st WHERE st.ItemId = i.ItemId);

PRINT CONCAT(N'Demo items removed: ', @@ROWCOUNT);

COMMIT TRANSACTION;
GO

SET NOEXEC OFF;
GO
