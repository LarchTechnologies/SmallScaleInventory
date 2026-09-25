@echo off
REM ===========================================================================
REM  setup-database.cmd
REM  Creates / updates the InventoryManagementDB database with sqlcmd.
REM
REM  Usage (from a Command Prompt, in this Scripts folder):
REM      setup-database.cmd                      uses (localdb)\MSSQLLocalDB
REM      setup-database.cmd ".\SQLEXPRESS"       uses another SQL Server instance
REM
REM  Steps:  1. Database.sql          create the database (if missing)
REM          2. Tables.sql            tables, indexes, view, triggers
REM          3. StoredProcedures.sql  all stored procedures
REM          4. SeedData.sql          OPTIONAL demo data (you are asked)
REM
REM  All scripts are safe to run again; existing data is never dropped.
REM  Requires sqlcmd (installed with SQL Server / SSMS, or "winget install
REM  sqlcmd"). The application can also do steps 1-4 itself after asking you.
REM ===========================================================================
setlocal
set "SERVER=(localdb)\MSSQLLocalDB"
set "DATABASE=InventoryManagementDB"
if not "%~1"=="" set "SERVER=%~1"

cd /d "%~dp0"

where sqlcmd >nul 2>&1
if errorlevel 1 goto :no_sqlcmd

REM Start the LocalDB instance when the default server is used.
if /i "%SERVER%"=="(localdb)\MSSQLLocalDB" call :start_localdb

echo.
echo Server:   %SERVER%
echo Database: %DATABASE%
echo.

echo [1/4] Database.sql
sqlcmd -S "%SERVER%" -E -b -I -i "Database.sql"
if errorlevel 1 goto :failed

echo [2/4] Tables.sql
sqlcmd -S "%SERVER%" -E -b -I -d "%DATABASE%" -i "Tables.sql"
if errorlevel 1 goto :failed

echo [3/4] StoredProcedures.sql
sqlcmd -S "%SERVER%" -E -b -I -d "%DATABASE%" -i "StoredProcedures.sql"
if errorlevel 1 goto :failed

echo.
choice /C YN /N /M "[4/4] Load DEMO data (SeedData.sql)? [Y/N] "
if errorlevel 2 goto :done
sqlcmd -S "%SERVER%" -E -b -I -d "%DATABASE%" -i "SeedData.sql"
if errorlevel 1 goto :failed

:done
echo.
echo Database setup completed successfully.
echo You can now start InventoryManagement.exe.
endlocal
exit /b 0

:start_localdb
where sqllocaldb >nul 2>&1
if errorlevel 1 (
    echo WARNING: sqllocaldb was not found. Is SQL Server Express LocalDB installed?
    exit /b 0
)
sqllocaldb start MSSQLLocalDB >nul 2>&1
if errorlevel 1 (
    echo Creating LocalDB instance MSSQLLocalDB ...
    sqllocaldb create MSSQLLocalDB -s
)
exit /b 0

:no_sqlcmd
echo ERROR: sqlcmd was not found.
echo Install it with "winget install sqlcmd" or together with SQL Server Management Studio,
echo or let the application create the database for you on first start.
endlocal
exit /b 1

:failed
echo.
echo ERROR: The database setup failed. Read the messages above.
echo Common causes: LocalDB not installed or not running, wrong server name, missing permissions.
endlocal
exit /b 1
