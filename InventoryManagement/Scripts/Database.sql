/* =============================================================================
   Database.sql
   Step 1 of 4 - Creates the InventoryManagementDB database on SQL Server LocalDB.

   Run this script while connected to the server (any database, e.g. master):
       sqlcmd -S "(localdb)\MSSQLLocalDB" -i Database.sql

   The script is safe to run more than once: it does nothing when the database
   already exists. It never drops or overwrites data.

   NOTE: The application's DatabaseInitializer can perform this step for you.
   It creates the database named in App.config (Initial Catalog) after asking
   for your confirmation.
   ============================================================================= */

SET NOCOUNT ON;

IF DB_ID(N'InventoryManagementDB') IS NULL
BEGIN
    PRINT N'Creating database InventoryManagementDB ...';
    CREATE DATABASE InventoryManagementDB;

    /* Readers see the last committed data instead of waiting for writers.
       Stock postings are still serialized per item by the row lock taken in
       usp_Stock_PostTransaction, so stock checks remain exact. */
    ALTER DATABASE InventoryManagementDB SET READ_COMMITTED_SNAPSHOT ON;

    PRINT N'Database InventoryManagementDB created.';
END
ELSE
BEGIN
    PRINT N'Database InventoryManagementDB already exists. Nothing to do.';
END
GO
