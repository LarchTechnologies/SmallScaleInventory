# Small Scale Inventory – Inventory Management System

A complete, offline **Windows desktop inventory system** for small businesses, workshops and stores.
It is written in **C# / Windows Forms** and stores its data in **SQL Server Express LocalDB**. All data
access goes through **ADO.NET and stored procedures**.

It keeps an **item master**, records **Stock IN** and **Stock OUT** movements, shows a live **dashboard**
and produces three reports: **Current Stock**, **Stock Summary** and **Stock Movement**. Current stock is
always *calculated from the transaction history* and is never stored as a value that someone can edit.
Stock can never become negative.

---

## Table of contents

1. [Features](#1-features)
2. [Technology stack and target framework](#2-technology-stack-and-target-framework)
3. [Architecture](#3-architecture)
4. [Folder structure](#4-folder-structure)
5. [Database architecture](#5-database-architecture)
6. [How current stock is calculated](#6-how-current-stock-is-calculated)
7. [Stock IN flow](#7-stock-in-flow)
8. [Stock OUT flow](#8-stock-out-flow)
9. [How the reports are calculated](#9-how-the-reports-are-calculated)
10. [Transaction safety and concurrency](#10-transaction-safety-and-concurrency)
11. [Validation](#11-validation)
12. [Error handling and logging](#12-error-handling-and-logging)
13. [Connection string and settings](#13-connection-string-and-settings)
14. [Setup for beginners (step by step)](#14-setup-for-beginners-step-by-step)
15. [Running the application](#15-running-the-application)
16. [Using the application](#16-using-the-application)
17. [Demo data](#17-demo-data)
18. [Automated tests](#18-automated-tests)
19. [Manual testing checklist](#19-manual-testing-checklist)
20. [Troubleshooting](#20-troubleshooting)
21. [Future extensions](#21-future-extensions)

---

## 1. Features

| Area | What you get |
|---|---|
| **Dashboard** | Six cards: Total Items, Active Items, Total Stock Qty, Low Stock Items, Today's IN and Today's OUT. A low-stock grid lists items where *CurrentStock ≤ MinimumStock* (code, name, current, minimum, unit, status), with colour indicators for NORMAL, LOW STOCK and OUT OF STOCK. Quick buttons open Stock IN, Stock OUT, Item Master and the Current Stock report. |
| **Item Master** | Add, edit, clear, search, and activate/deactivate items. Fields are code, name, category, unit, minimum stock, opening stock and active. The grid shows current stock and a colour-coded status. Duplicate item codes are rejected (case-insensitive). Items are deactivated, never deleted, so their history is kept. |
| **Stock IN** | Searchable item combo box, quantity, date, reference no., reason and remarks. A live preview shows **Current + Add = New stock**. Save shows a success message, refreshes the screen and clears the form. The last 15 IN entries are listed below. |
| **Stock OUT** | The available stock appears as soon as an item is selected. A live preview shows **Available − Issue = Remaining**. An issue larger than the available stock is blocked with *"Insufficient stock. Available quantity: X"*, both in C# and inside the database transaction. |
| **Current Stock report** | Code, name, category, unit, opening, total IN, total OUT, current, minimum and status. It has a search box, category filter, status filter, "include inactive" option and a refresh button. Click a column header to sort. Print preview is available. |
| **Stock Summary report** | Choose a From/To date range (with presets). For each item it shows opening, IN, OUT and closing, where **Closing = Opening + IN − OUT**. Everything is computed from the transaction table. |
| **Stock Movement report** | Filters: from/to date, item, transaction type and free-text search. Columns: TxnId, date, code, name, type, quantity, reference, reason and remarks. The footer shows **Total IN, Total OUT and Net**. Sortable, with print preview. |
| **Database Information** | Shows the server, database, authentication, connection string name, configuration file, SQL Server version and edition, database creation date, number of items and transactions, last transaction, log file and connection status. Buttons: *Test Connection*, *Open Log Folder* and *Open SQL Scripts Folder*. |
| **About** | Version information and keyboard shortcuts. |
| **Keyboard** | `Ctrl+D` Dashboard, `F2` Item Master, `F3` Stock IN, `F4` Stock OUT, `F5` refresh the current page, `F6` / `F7` / `F8` the three reports, and `Ctrl+S` save on entry screens. In Item Master, `Esc` cancels editing. |

Every menu entry and button opens real, working functionality. There are no placeholders.

---

## 2. Technology stack and target framework

| Component | Choice | Why |
|---|---|---|
| Language | C# (the code stays compatible with C# 12) | |
| UI | Windows Forms | Simple, fast, and supported by the Visual Studio designer |
| Runtime | **.NET 10 (`net10.0-windows`)** | The current Long-Term-Support release of .NET |
| Database | **SQL Server Express LocalDB** `(localdb)\MSSQLLocalDB` | Free, runs locally with no server administration, and uses the same engine as full SQL Server |
| Data access | ADO.NET with **Microsoft.Data.SqlClient 6.1.1** and stored procedures only | Microsoft's supported SQL Server provider |
| Configuration | `App.config` read with **System.Configuration.ConfigurationManager 9.0.4** | The classic `<connectionStrings>` section |
| Tests | xUnit 2.9 | Unit tests always run; integration tests run only against a real SQL Server |

These are the only third-party (NuGet) packages, and both are Microsoft packages. There is no ORM, no UI
toolkit and no logging framework.

**IDE:** Visual Studio 2026 (or Visual Studio 2022 with the .NET 10 SDK installed). Use the
**".NET desktop development"** workload, which also installs SQL Server Express LocalDB.

**Offline use:** the application needs no network at run time. The first build downloads the NuGet packages
once, unless they are already in your local NuGet cache. After that, building and running both work fully
offline.

**Need .NET 8 instead?** All code is compatible with .NET 8 (LTS). Change `net10.0-windows` to
`net8.0-windows` in `InventoryManagement/InventoryManagement.csproj`, and `net10.0` to `net8.0` in
`InventoryManagement.Core.csproj` and `InventoryManagement.Tests.csproj`.

---

## 3. Architecture

The solution uses a strict layered design. Each layer only talks to the layer directly below it:

```
┌──────────────────────────────────────────────────────────────┐
│  UI  (InventoryManagement – Windows Forms)                   │
│  Forms, user controls, UI helpers. No SQL, no business rules.│
└──────────────▲───────────────────────────────────────────────┘
               │ calls
┌──────────────┴───────────────────────────────────────────────┐
│  Services  (InventoryManagement.Core/Services)               │
│  ItemService, StockService, ReportService, StockCalculator   │
│  Validation, business rules, friendly error messages.        │
└──────────────▲───────────────────────────────────────────────┘
               │ calls
┌──────────────┴───────────────────────────────────────────────┐
│  Repositories  (InventoryManagement.Core/Data)               │
│  ItemRepository, StockRepository, ReportRepository           │
│  Build parameters, call stored procedures, map rows.         │
│  StoredProcedureExecutor = the ONE place that opens          │
│  connections, runs commands and translates SqlExceptions.    │
└──────────────▲───────────────────────────────────────────────┘
               │ ADO.NET, parameterised stored procedure calls
┌──────────────┴───────────────────────────────────────────────┐
│  SQL Server LocalDB  (InventoryManagementDB)                 │
│  Tables, view vw_ItemStock, triggers, 17 stored procedures.  │
│  Final guard for every rule (constraints + transactions).    │
└──────────────────────────────────────────────────────────────┘
```

* **InventoryManagement.Core** (a class library) contains everything that is not UI: models, services,
  repositories, validation, exceptions, logging and database initialisation. It has **no reference to
  Windows Forms**, so business logic cannot leak into forms and can be unit tested.
* **InventoryManagement** (a WinForms app) contains only forms, controls and UI helpers. `AppServices` creates
  the services once at startup and hands them to each page. This is simple dependency passing with no
  container.
* **InventoryManagement.Tests** (xUnit) contains unit tests and optional database integration tests.

Design rules applied throughout:

* **No SQL in forms.** The application code runs only stored procedures, and their names are constants in
  `Data/StoredProcedures.cs`. The only inline SQL is a few fixed, read-only system queries in
  `DatabaseInitializer`, such as "does the database exist?", "which required objects are missing?", row
  counts and the server version. Two small fixed
  statements create the database, and they run only after you confirm it. None of these queries contain any
  user input.
* **Parameters everywhere.** Every value is passed as a typed `SqlParameter` (see `Data/DbParameters.cs`).
  SQL is never built by string concatenation from user input.
* **No duplication.** Connection, command and error handling live in `StoredProcedureExecutor`. Stock
  arithmetic in C# lives in `StockCalculator`. Stock arithmetic in SQL lives in one view, `vw_ItemStock`.
* **Constants instead of magic strings:** `TransactionTypes.In/Out`, `StockStatus.Normal/Low/OutOfStock`,
  `FieldLengths.*`, `DatabaseErrorNumbers.*` and `StoredProcedures.*`.
* **`using`** is used for every connection, command, reader, font and dialog.

---

## 4. Folder structure

```
SmallScaleInventory/
├── InventoryManagement.sln              Visual Studio solution (3 projects + solution folders)
├── Directory.Build.props                Shared settings (C# version, nullable, version number)
├── README.md                            This file
│
├── InventoryManagement/                 ── WINDOWS FORMS APPLICATION (UI only) ──
│   ├── Program.cs                       Entry point: logging, global exception handlers, DB check, MainForm
│   ├── AppServices.cs                   Creates DatabaseConnection + services once from App.config
│   ├── App.config                       ★ Connection string "InventoryDb" and app settings
│   ├── Forms/
│   │   ├── MainForm(.Designer).cs       Shell: sidebar navigation, header, status bar, shortcuts
│   │   ├── DashboardForm                Cards + low-stock grid
│   │   ├── ItemMasterForm               Item add / edit / search / activate-deactivate
│   │   ├── StockInForm / StockOutForm   Thin pages hosting the shared StockEntryControl
│   │   ├── CurrentStockForm             Report 1
│   │   ├── StockSummaryForm             Report 2
│   │   ├── StockMovementForm            Report 3
│   │   ├── DatabaseInfoForm             Settings → Database Information
│   │   ├── AboutForm                    Settings → About (dialog)
│   │   ├── AppPage.cs                   Page enum + INavigationHost / IRefreshablePage
│   │   └── ReportFormStyler.cs          Shared look of the three report pages
│   ├── Controls/
│   │   ├── StockEntryControl            ONE control used by both Stock IN and Stock OUT
│   │   ├── StatCard                     Dashboard card
│   │   └── NavButton                    Sidebar button
│   ├── Helpers/                         UiTheme, MessageHelper, GridHelper, GridPrinter,
│   │                                    DatabaseStartupCheck, ComboBoxHelper, InputHelper, ...
│   └── Scripts/                         ★ SQL scripts (copied next to the .exe on build)
│       ├── Database.sql                 1. creates InventoryManagementDB
│       ├── Tables.sql                   2. tables, indexes, view, triggers
│       ├── StoredProcedures.sql         3. all stored procedures
│       ├── SeedData.sql                 4. OPTIONAL demo data
│       ├── RemoveDemoData.sql           removes the demo data again
│       └── setup-database.cmd           runs 1-3 (and optionally 4) with sqlcmd
│
├── InventoryManagement.Core/            ── BUSINESS + DATA LAYER (no WinForms) ──
│   ├── Models/                          Item, StockTransaction, DashboardSummary, DatabaseInfo,
│   │   │                                TransactionTypes, StockStatus, FieldLengths, ...
│   │   └── Reports/                     Report row + filter classes
│   ├── Services/                        ItemService, StockService, ReportService, StockCalculator
│   ├── Data/                            DatabaseConnection ★, StoredProcedureExecutor, repositories,
│   │                                    RowMappers, DbParameters, SqlErrorTranslator,
│   │                                    StoredProcedures (names), DatabaseErrorNumbers
│   ├── Exceptions/                      ValidationException, DuplicateItemCodeException,
│   │                                    InsufficientStockException, DatabaseUnavailableException, ...
│   ├── Helpers/                         ValidationHelper, DatabaseInitializer, AppSettings
│   └── Logging/AppLogger.cs             Thread-safe file logger → Logs/application.log
│
└── InventoryManagement.Tests/           ── xUnit TESTS ──
    ├── ValidationHelperTests.cs, StockCalculatorTests.cs, ItemValidationTests.cs, SqlScriptTests.cs
    └── Integration/                     Real-database tests (opt-in, see section 18)
```

---

## 5. Database architecture

### Tables and relationships

```
TransactionTypes (lookup)           Items                                StockTransactions
────────────────────────            ─────────────────────────            ──────────────────────────────
TransactionType  PK  ◄──┐           ItemId        INT IDENTITY PK ◄──┐   TransactionId   BIGINT IDENTITY PK
Description             │           ItemCode      NVARCHAR(50) UNIQUE│   ItemId          INT  FK → Items
Direction  (+1 / -1)    │           ItemName      NVARCHAR(200)      └── TransactionType VARCHAR(20) FK ──┐
IsActive                │           Category      NVARCHAR(100) NULL     Quantity        DECIMAL(18,2) >0 │
                        │           Unit          NVARCHAR(50)           TransactionDate DATETIME2(0)     │
 rows: IN  (+1)         │           MinimumStock  DECIMAL(18,2) =0 ≥0    ReferenceNo     NVARCHAR(100)    │
       OUT (−1)         │           OpeningStock  DECIMAL(18,2) =0 ≥0    Reason          NVARCHAR(200)    │
                        │           IsActive      BIT = 1                Remarks         NVARCHAR(500)    │
                        │           CreatedDate   DATETIME2(0)           CreatedDate     DATETIME2(0)     │
                        │           ModifiedDate  DATETIME2(0) NULL                                       │
                        └─────────────────────────────────────────────────────────────────────────────────┘
```

* **One Items row to many StockTransactions rows.** The foreign key blocks deleting an item that has
  history, which is why items are deactivated instead of deleted.
* **A single common transaction table for all movement types.** The `TransactionTypes` lookup gives
  every type a *direction* (+1 adds stock, −1 removes stock). All calculations use `Direction`, never the
  literal text 'IN'/'OUT'. To add `ADJUSTMENT_IN/OUT`, `TRANSFER_IN` or `TRANSFER_OUT` later, you insert a
  lookup row; no table changes are needed (see [section 21](#21-future-extensions)).
* **Constraints:** `UQ_Items_ItemCode` (unique code), CHECKs for non-blank code/name/unit, minimum and
  opening ≥ 0, and quantity > 0.
* **Indexes:**
  * `UQ_Items_ItemCode` (unique)
  * `IX_StockTransactions_ItemId_TransactionDate` INCLUDE (TransactionType, Quantity), used for per-item
    stock and item-filtered reports
  * `IX_StockTransactions_TransactionDate` INCLUDE (ItemId, TransactionType, Quantity), used for date-range
    reports and today's dashboard totals
  * `IX_Items_Category` and `IX_Items_ItemName`, used for item search and the category filter
* **View `vw_ItemStock`** is the single source of truth for TotalIn, TotalOut, CurrentStock and
  StockStatus per item.
* **Triggers** `TR_StockTransactions_PreventNegativeStock` and `TR_Items_PreventNegativeStock` are a last line
  of defence. Even a manual `INSERT`/`UPDATE`/`DELETE` in SSMS cannot leave an item with negative stock.
* **Scalar function `fn_BuildContainsPattern`** turns search text into a safe `LIKE` pattern. It escapes
  `%`, `_` and `[`, so they are searched literally.
* **READ_COMMITTED_SNAPSHOT** is switched on when the database is created. Readers, such as reports and the
  dashboard, see the last committed data instead of waiting for writers.

### Stored procedures (17)

| Group | Procedures |
|---|---|
| Items | `usp_Item_GetAll`, `usp_Item_GetById`, `usp_Item_GetCategories`, `usp_Item_GetUnits`, `usp_Item_Insert`, `usp_Item_Update`, `usp_Item_SetActive` |
| Stock | `usp_Stock_In`, `usp_Stock_Out` (both call `usp_Stock_PostTransaction`), `usp_Stock_GetCurrentStock`, `usp_Stock_GetRecentTransactions` |
| Reports | `usp_Report_CurrentStock`, `usp_Report_StockSummary`, `usp_Report_StockMovement` |
| Dashboard | `usp_Dashboard_GetSummary`, `usp_Dashboard_GetLowStockItems` |

Business errors are raised with `THROW` and fixed error numbers. `SqlErrorTranslator` converts each number
into a specific C# exception with a friendly message:

| Number | Meaning | C# exception |
|---|---|---|
| 50001 | Item code already exists | `DuplicateItemCodeException` |
| 50002 | Item not found | `RecordNotFoundException` |
| 50003 | Item is inactive | `ValidationException` |
| 50004 | Invalid input (blank / negative) | `ValidationException` |
| 50010 | Insufficient stock | `InsufficientStockException` |
| 50011 / 50012 | Negative stock blocked by a trigger / opening stock below issued quantity | `InsufficientStockException` |
| 50020 | Invalid date range | `ValidationException` |
| 50030 | Invalid quantity (≤ 0) | `ValidationException` |
| 50031 | Invalid or future transaction date | `ValidationException` |
| 50032 | Unknown transaction type | `ValidationException` |

All scripts are **idempotent**: they use `IF NOT EXISTS` and `CREATE OR ALTER`, so re-running them updates
objects without losing data. `Tables.sql`, `StoredProcedures.sql`, `SeedData.sql` and `RemoveDemoData.sql`
refuse to run if they are accidentally executed inside `master`, `tempdb`, `model` or `msdb`.

---

## 6. How current stock is calculated

Current stock is **derived, never stored**. There is no editable "CurrentStock" column anywhere:

```
CurrentStock = OpeningStock + Σ(quantities of IN-direction transactions) − Σ(quantities of OUT-direction transactions)
```

It is computed in one place, the view `dbo.vw_ItemStock`, and every procedure that needs stock reads that
view. The status is also derived:

| Condition | Status |
|---|---|
| CurrentStock ≤ 0 | `OUT OF STOCK` (red) |
| 0 < CurrentStock ≤ MinimumStock | `LOW STOCK` (amber) |
| CurrentStock > MinimumStock | `NORMAL` (green) |

**Why derived?** The transaction table is a complete audit trail. Stock can never drift out of sync with its
history, no one can "type in" a stock figure, and any report for any date range can be rebuilt from the same
rows.

**Example: RM-001 Raw Material A (unit KG, minimum 100, opening 500)**

| Step | Transaction | Stock afterwards |
|---|---|---|
| Opening | – | 500 |
| Purchase | IN 200 | 700 |
| Issue to production | OUT 150 | 550 |
| Issue to production | OUT 120 | 430 |
| Morning delivery | IN 50 | **480** |

`500 + (200 + 50) − (150 + 120) = 480`. That is above the minimum of 100, so the status is **NORMAL**.
If a further OUT of 400 were posted, stock would be 80, and 80 ≤ 100 gives **LOW STOCK**. Trying to issue
500 would be blocked with *"Insufficient stock. Available quantity: 480.00 KG"*.

**What about the opening stock field?** Opening stock is the quantity on hand *when the item was set up*. It
can be corrected later, but never to a value that would make current stock negative. For example, you
cannot set opening stock to 100 if 300 have already been issued and only 150 received. Both the procedure
and a trigger enforce this.

---

## 7. Stock IN flow

```
StockInForm ──► StockEntryControl (mode = In)
  1. User picks an item (searchable combo) → StockService.GetCurrentStock → preview "Current + Add = New"
  2. User enters qty, date, reference, reason, remarks and clicks Save (or Ctrl+S)
  3. StockService.AddStock(request)
        • ValidationHelper: item selected, qty > 0 (max 2 decimals), valid date, text lengths
        • re-reads the item: must exist and be active
  4. StockRepository.Post → EXEC usp_Stock_In @ItemId, @Quantity, @TransactionDate, @ReferenceNo,
                                              @Reason, @Remarks, OUTPUT @TransactionId, @PreviousStock, @NewStock
  5. usp_Stock_In → usp_Stock_PostTransaction (inside BEGIN TRAN … COMMIT)
  6. UI shows "Stock added successfully." with item, current, added, new stock and the Transaction No,
     then refreshes the recent list and clears the form
```

## 8. Stock OUT flow

```
StockOutForm ──► StockEntryControl (mode = Out)
  1. On item change → available stock is shown immediately; preview "Available − Issue = Remaining"
     (if the issue is larger than the available stock, Remaining turns red and the warning
      "Insufficient stock. Available quantity: X" appears; Save is then refused)
  2. Save → StockService.IssueStock(request)
        • same validation as IN
        • re-reads the item and checks StockCalculator.WouldGoNegative(available, qty)
          → InsufficientStockException "Insufficient stock. Available quantity: X"
  3. EXEC usp_Stock_Out … → usp_Stock_PostTransaction:
        BEGIN TRANSACTION
          lock the item row (UPDLOCK, HOLDLOCK)        ← no other posting for this item can run now
          read available stock from vw_ItemStock
          IF qty > available → THROW 50010 'Insufficient stock. Available quantity: X'  (→ ROLLBACK)
          INSERT StockTransactions (…)
          trigger re-checks that no stock is negative
        COMMIT
  4. Success message, refresh, clear
```

The C# check gives an instant, friendly message. The **database check is the one that counts**: it runs
under a lock inside the transaction, so it is still correct when two users issue the same item at the
same moment.

---

## 9. How the reports are calculated

All report figures come from the database (`usp_Report_*`), so the C# code never recalculates them.

**Current Stock** (`usp_Report_CurrentStock`) reads `vw_ItemStock`. Opening + TotalIn − TotalOut =
Current, with the status rules above. It supports optional search, category, status and include-inactive
filters.

**Stock Summary** (`usp_Report_StockSummary @FromDate, @ToDate`) takes, for each item:

```
Period Opening = Items.OpeningStock + IN before FromDate − OUT before FromDate
Period IN      = IN  with FromDate ≤ date < ToDate + 1 day
Period OUT     = OUT with FromDate ≤ date < ToDate + 1 day
Closing        = Period Opening + Period IN − Period OUT
```

For example, RM-001 with the demo data and a period covering the last 5 days gives: opening 550 (500 + 200
− 150 before the period), IN 50, OUT 120, closing **480**. That matches the current stock, as it should when
the period ends today. The `To` date is *inclusive*: the whole day is counted.

**Stock Movement** (`usp_Report_StockMovement`) lists every transaction in the range, filtered by item, type
and search text (reference/reason/remarks/code/name). The footer totals are calculated by
`StockMovementReport` from the rows shown: **Total IN** (sum of +1 direction), **Total OUT** (sum of −1
direction) and **Net** = IN − OUT.

Reports also have: sortable columns (click the header), a 2-row filter bar, a footer with row count and
totals, and **Print** (a print preview with landscape layout, title, filters and page numbers).

---

## 10. Transaction safety and concurrency

* `usp_Stock_PostTransaction` uses `SET XACT_ABORT ON`, `BEGIN TRY / BEGIN TRANSACTION … COMMIT`, and
  `ROLLBACK` in `BEGIN CATCH`. A failed posting leaves no partial data.
* The item row is locked with `UPDLOCK, HOLDLOCK` **before** stock is read. Postings for the same item are
  therefore processed one at a time, and the "check available, then insert" step cannot be interleaved
  by another user. This is verified by an integration test in which 10 parallel issues of 10 against a stock
  of 50 produce **exactly 5 successes** and a final stock of 0.
* `usp_Item_Insert` and `usp_Item_Update` use the same lock pattern for the duplicate-code check. The unique
  constraint is the final guard.
* Triggers reject any change that would make stock negative, even changes made outside the application.

---

## 11. Validation

Rules are checked **twice**: in the service layer, which gives immediate friendly messages and highlights the
field, and in the database, which is authoritative.

| Rule | Service (C#) | Database |
|---|---|---|
| Item code, name, unit required | `ValidationHelper.RequireText` | procedure checks + CHECK constraints |
| Max lengths (code 50, name 200, category 100, unit 50, ref 100, reason 200, remarks 500) | `FieldLengths` + `RequireText/OptionalText` | column sizes |
| Minimum / opening stock ≥ 0 | `RequireNonNegative` | procedure + CHECK |
| Quantity > 0, at most 2 decimals, ≤ 999,999,999.99 | `RequireValidQuantity`, `RequireDecimalPlaces` | procedure + CHECK `Quantity > 0` |
| OUT quantity ≤ available stock | `StockCalculator.WouldGoNegative` | locked check in the transaction + trigger |
| No duplicate item code (case-insensitive, codes are stored in UPPER case) | code normalised (trim + upper case) | locked check + UNIQUE constraint (error 50001 → `DuplicateItemCodeException`) |
| Valid transaction date (not before 2000-01-01, not in the future) | `RequireValidTransactionDate` | procedure (error 50031) |
| Report dates valid (2000-01-01 … 2099-12-31) and From Date ≤ To Date | `RequireValidDateRange` | procedure (error 50020) |
| Posting to an inactive item is refused | `StockService` | procedure (error 50003) |

Validation errors show a warning message box, and an error icon appears next to the offending field.

---

## 12. Error handling and logging

* **Central translation:** every `SqlException` passes through `SqlErrorTranslator` in the data layer and is
  replaced by an application exception with a friendly message. **Raw SQL error text is never shown to
  the user.**
  * Connection problems (LocalDB not running, network errors) produce
    *"Unable to connect to the inventory database. Please verify SQL Server LocalDB is running."*
  * "Cannot open database" and "login failed" produce a message that points to the setup steps or to the
    connection string.
  * A missing procedure or table produces a message suggesting that you run the setup scripts.
  * A timeout or deadlock produces "please try again".
* **Central display:** `MessageHelper.HandleException` chooses the icon and text for each exception type and
  logs anything unexpected.
* **Global handlers:** `Application.ThreadException` and `AppDomain.UnhandledException` are set in
  `Program.cs`. Any unexpected error is logged and shown as a friendly message instead of crashing the
  program.
* **Log file:** `Logs\application.log` is written next to the `.exe`. The folder can be changed with the
  `LogDirectory` setting. It records application start/stop and version, the connection target, connection
  failures, database checks and setup actions, operation failures (with the operation name) and unexpected
  exceptions with full stack traces. Each line has the format
  `2026-09-25 10:15:02.123 [ERROR] message`. At 5 MB the file is renamed to `application.previous.log` and a new log is started. Use
  *Database Information → Open Log Folder* to open it.

---

## 13. Connection string and settings

The connection string is configured in **exactly one place**, `InventoryManagement/App.config`:

```xml
<connectionStrings>
  <add name="InventoryDb"
       connectionString="Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=InventoryManagementDB;Integrated Security=True;TrustServerCertificate=True;Connect Timeout=30;Application Name=SmallScaleInventory"
       providerName="Microsoft.Data.SqlClient" />
</connectionStrings>
```

It is read only by `InventoryManagement.Core/Data/DatabaseConnection.cs` (`DatabaseConnection.FromConfiguration()`).
No other file contains a connection string.

* When you build, `App.config` is copied to **`bin\Debug\net10.0-windows\InventoryManagement.dll.config`**.
  To change the database of an already-built application, edit that file.
* **SQL Server Express instead of LocalDB:** `Data Source=.\SQLEXPRESS;Initial Catalog=InventoryManagementDB;Integrated Security=True;TrustServerCertificate=True`
* **SQL login instead of Windows login:** replace `Integrated Security=True` with `User ID=...;Password=...`.

Other settings (`<appSettings>`):

| Key | Default | Meaning |
|---|---|---|
| `CommandTimeoutSeconds` | 30 | Timeout for each database command |
| `LogDirectory` | `Logs` | Log folder, relative to the app folder or an absolute path |
| `RecentTransactionCount` | 15 | Rows in the "recent transactions" list on Stock IN/OUT |

---

## 14. Setup for beginners (step by step)

### Step 1 – Install the tools

1. Install **Visual Studio 2026 Community** (free), or Visual Studio 2022 17.14+ with the **.NET 10 SDK**.
2. In the Visual Studio Installer, tick the **".NET desktop development"** workload. It includes
   *SQL Server Express LocalDB*.
   *Without Visual Studio:* download the "SQL Server Express" installer from Microsoft, choose
   **Download Media → LocalDB**, and run `SqlLocalDB.msi`.
3. Optional but recommended: **SQL Server Management Studio (SSMS)** or the `sqlcmd` tool, to look at the
   data.

### Step 2 – Check that LocalDB works

Open **Command Prompt** and run:

```cmd
sqllocaldb info
sqllocaldb info MSSQLLocalDB
sqllocaldb start MSSQLLocalDB
```

`info MSSQLLocalDB` should show `State: Running` after the `start` command. If the instance does not exist,
create it with `sqllocaldb create MSSQLLocalDB -s`.

### Step 3 – Create the database (choose ONE option)

**Option A: let the application do it (easiest).** Just run the application (step 5). If the database is
missing, it **asks you** whether to create it and tells you which scripts it will run. It then offers to load
the demo data. Nothing is created without your confirmation.

**Option B: setup script.** Double-click `InventoryManagement\Scripts\setup-database.cmd`, or run it from a
Command Prompt:

```cmd
cd InventoryManagement\Scripts
setup-database.cmd                       REM uses (localdb)\MSSQLLocalDB
setup-database.cmd ".\SQLEXPRESS"        REM optional: another server
```

It starts LocalDB if needed, runs `Database.sql`, `Tables.sql` and `StoredProcedures.sql`, and then asks
whether to load `SeedData.sql`. It needs `sqlcmd`, which is installed with Visual Studio / SSMS / SQL Server.

**Option C: manually, in SSMS.** Connect to server `(localdb)\MSSQLLocalDB` with Windows Authentication,
then open and execute the scripts **in this order**:

| # | Script | Run in database |
|---|---|---|
| 1 | `Database.sql` | master (the script creates `InventoryManagementDB`) |
| 2 | `Tables.sql` | **InventoryManagementDB** |
| 3 | `StoredProcedures.sql` | **InventoryManagementDB** |
| 4 | `SeedData.sql` *(optional demo data)* | **InventoryManagementDB** |

Or with sqlcmd:

```cmd
sqlcmd -S "(localdb)\MSSQLLocalDB" -E -i Database.sql
sqlcmd -S "(localdb)\MSSQLLocalDB" -E -d InventoryManagementDB -i Tables.sql
sqlcmd -S "(localdb)\MSSQLLocalDB" -E -d InventoryManagementDB -i StoredProcedures.sql
sqlcmd -S "(localdb)\MSSQLLocalDB" -E -d InventoryManagementDB -i SeedData.sql
```

### Step 4 – Configure (usually nothing to do)

The default connection string already points to `(localdb)\MSSQLLocalDB` / `InventoryManagementDB`. You
only need to change `App.config` for another server or database name (see
[section 13](#13-connection-string-and-settings)).

### Step 5 – Open, build and run

1. Open **`InventoryManagement.sln`** in Visual Studio.
2. Make sure **InventoryManagement** is the startup project (right-click → *Set as Startup Project*).
3. Press **F5**. The first build restores the two NuGet packages.

### Step 6 – Verify

* The window opens on the **Dashboard**, and the status bar shows *Database: (localdb)\MSSQLLocalDB /
  InventoryManagementDB*.
* With demo data you should see Total Items 5, Low Stock Items 3, Today's IN 50.00 and Today's OUT 115.00.
* **Settings → Database Information → Test Connection** reports success.
* Run through the checklist in [section 19](#19-manual-testing-checklist).

---

## 15. Running the application

* **From Visual Studio:** F5 (debug) or Ctrl+F5.
* **From the command line:**
  ```cmd
  dotnet build InventoryManagement.sln -c Release
  dotnet run --project InventoryManagement -c Release
  ```
* **Publish a folder you can copy to another PC** (which needs LocalDB and the .NET 10 Desktop Runtime):
  ```cmd
  dotnet publish InventoryManagement -c Release -o publish
  ```
  Copy the `publish` folder. The `Scripts` folder is included, so the first-run database creation works.

**Startup check.** On every start, `DatabaseInitializer.CheckStatus()` checks what state the database is in:

| Situation | What happens |
|---|---|
| Server unreachable | Error message with server/database names, a hint to run `sqllocaldb info MSSQLLocalDB`, and the log path. Choose **Retry** or **Cancel** (exit). |
| Database missing | Asks *"Do you want to create it now?"*. **Yes** creates it and runs the scripts, then offers demo data. **No** exits. |
| Tables/procedures missing | Lists what is missing and asks whether to install it. Existing data is kept. |
| Ready | The main window opens. |

---

## 16. Using the application

### Navigation

The left sidebar has DASHBOARD; MASTER → Item Master; STOCK → Stock IN, Stock OUT; REPORTS → Current Stock,
Stock Summary, Stock Movement; SETTINGS → Database Information, About. The window has a minimum size of
1200 × 720 and can be resized or maximised.

### Item Master

* **Add:** fill in the fields and click **Add Item**. The code is stored in upper case. Category and unit
  combo boxes suggest existing values, but you can also type new ones.
* **Edit:** select a row and click **Edit Selected**, or double-click the row, or press Enter. The form
  switches to edit mode, and the button becomes **Save Changes** (`Ctrl+S`). **Clear**, or `Esc`, cancels.
* **Search:** type in the search box (code, name or category) and press Enter or click **Search**. Tick
  *Show inactive items* to include them (inactive rows are shown in grey).
* **Deactivate/Activate:** a single toggle button, which asks for confirmation. Inactive items keep their
  history but cannot receive new stock movements and are hidden from the IN/OUT item lists.

### Stock IN / Stock OUT

Type part of a code or name in the item box to filter it. The preview panel updates as you type the quantity.
The date defaults to today, and future dates cannot be selected (they are also rejected by the service and
the database). The *Recent
transactions* list at the bottom shows the latest entries of that type.

### Reports

Filters are on two rows. Click **Refresh** (or `F5`) to re-run the report, click a column header to sort,
and click **Print** for a print preview, from which you can print or save as PDF with *Microsoft Print to PDF*.
Stock Summary and Stock Movement have quick date presets: Today, Last 7 days, This month, Last month and This year.
Changing a date manually switches the preset to Custom.

---

## 17. Demo data

`SeedData.sql` adds 5 items and 14 transactions dated relative to **today**, so the dashboard has "today"
figures immediately:

| Code | Name | Unit | Min | Opening | Movements | Current | Status |
|---|---|---|---|---|---|---|---|
| RM-001 | Raw Material A | KG | 100 | 500 | +200 −150 −120 +50 | 480 | NORMAL |
| RM-002 | Raw Material B | KG | 50 | 250 | +100 −80 −40 | 230 | NORMAL |
| FG-001 | Finished Product A | NOS | 20 | 100 | +60 −70 −75 | 15 | LOW STOCK |
| PK-001 | Carton Box (Large) | NOS | 200 | 300 | −150 −60 | 90 | LOW STOCK |
| CN-001 | Machine Oil | LTR | 10 | 20 | −12.5 −7.5 | 0 | OUT OF STOCK |

Every demo transaction has a reference number starting with **`DEMO-`**. The script is safe to re-run: it
never duplicates items or demo transactions.

**How to load demo data:** accept the prompt when the application creates the database, answer *Y* in
`setup-database.cmd`, or run `SeedData.sql` against `InventoryManagementDB`.

**How to remove demo data:** run `Scripts\RemoveDemoData.sql` against `InventoryManagementDB`:

```cmd
sqlcmd -S "(localdb)\MSSQLLocalDB" -E -d InventoryManagementDB -i RemoveDemoData.sql
```

It deletes all `DEMO-` transactions and then deletes the 5 demo items, but only if no other transactions
use them. If you have already posted your own OUT transactions against a demo item, removing its demo INs
could make stock negative. In that case the whole script is rolled back and nothing is removed.

**Start completely fresh:** drop the database (in SSMS: right-click → Delete, or
`sqlcmd -S "(localdb)\MSSQLLocalDB" -E -Q "DROP DATABASE InventoryManagementDB"`), then start the
application and decline the demo data.

---

## 18. Automated tests

```cmd
dotnet test InventoryManagement.Tests
```

**Unit tests (66, no database needed)** cover:

* `ValidationHelperTests`: required text, lengths, non-negative numbers, quantities and decimals, number
  parsing, transaction dates (today valid, future and too-old rejected) and date ranges
* `StockCalculatorTests`: IN/OUT arithmetic, negative-stock detection, the README stock example, the
  insufficient-stock message, transaction type directions and the movement-report IN/OUT totals
* `ItemValidationTests`: code/unit normalisation, required fields, negative minimum/opening stock, code
  length and the duplicate-code message
* `SqlScriptTests`: the `GO` batch splitter; every stored procedure name used in C# is created in
  `StoredProcedures.sql`; the required tables, view and triggers exist; stock posting uses a transaction
  with `UPDLOCK, HOLDLOCK`; and the required demo items are in `SeedData.sql`

**Integration tests (11, opt-in)** run against a real SQL Server. They are **skipped** unless the environment
variable `INVENTORY_TEST_SQLSERVER` holds a connection string *without* a database name:

```cmd
set INVENTORY_TEST_SQLSERVER=Data Source=(localdb)\MSSQLLocalDB;Integrated Security=True;TrustServerCertificate=True
dotnet test InventoryManagement.Tests
```

They create a separate database, **`InventoryManagementDB_Tests`**, install the scripts, run the tests and
drop that database afterwards. Your real `InventoryManagementDB` is never touched. The tests cover:

* Schema readiness
* Duplicate codes (case-insensitive)
* Stock IN
* OUT down to exactly zero
* Insufficient stock leaving stock unchanged
* 10 concurrent issues giving exactly 5 successes
* An inactive item being blocked
* The current stock report
* Summary closing = opening + IN − OUT
* Movement totals and the type filter
* Demo data loading

---

## 19. Manual testing checklist

### Item tests
- [ ] Add item `TEST-001` / "Test Item" / unit `PCS` / min 10 / opening 50. It appears in the grid with current 50 and status NORMAL.
- [ ] Add another item with code `test-001`. Expected: *"Item code already exists."*
- [ ] Leave code, name or unit empty. Expected: a validation message and an error icon on the field.
- [ ] Enter a negative minimum or opening stock, or text in a number field. Expected: a validation message.
- [ ] Edit `TEST-001` (Edit Selected → change name → Save Changes). The grid shows the new name.
- [ ] Search "test". Only matching items are shown. Clear the search to show everything again.
- [ ] Deactivate `TEST-001` (confirm). Its row turns grey (with *Show inactive items* ticked), and it no longer appears in the Stock IN/OUT item lists. Activate it again.

### Stock IN tests
- [ ] Select `TEST-001`. Current = 50 is shown. Enter 25. The preview shows 50 + 25 = 75.
- [ ] Save. A success message appears, the form clears, and the entry appears in *Recent transactions*.
- [ ] Try quantity 0, −5, `abc` or 1.234. Each is rejected.
- [ ] Save without selecting an item. Expected: *"Please select an item."*

### Stock OUT tests
- [ ] Select `TEST-001`. Available = 75 appears immediately.
- [ ] Issue 30. The preview shows 75 − 30 = 45. Save, and it succeeds.
- [ ] Issue 100. Expected: *"Insufficient stock. Available quantity: 45.00 PCS"*, and nothing is saved.
- [ ] Issue exactly 45. It succeeds, stock = 0, status OUT OF STOCK, and the item appears on the dashboard low-stock grid.
- [ ] Run two copies of the app and issue the last units at the same time. Only one succeeds.

### Report tests
- [ ] Current Stock: `TEST-001` shows opening 50, IN 25, OUT 75, current 0, OUT OF STOCK. The category and status filters and the search work, and column sorting works.
- [ ] Stock Summary (Today): `TEST-001` shows opening 50, IN 25, OUT 75, closing 0. Choose a range that ends yesterday: opening 50, IN 0, OUT 0, closing 50 (opening stock counts from the beginning of time).
- [ ] Stock Summary with From > To. Expected: *"From Date cannot be later than To Date."*
- [ ] Stock Movement (Today, item `TEST-001`): 3 rows. Total IN 25, Total OUT 75, Net −50. Filter by type OUT to get 2 rows.
- [ ] Print preview works on all three reports.
- [ ] Dashboard: the cards change after IN/OUT postings (Today's IN/OUT, Low Stock Items).

### Error handling tests
- [ ] Run `sqllocaldb stop MSSQLLocalDB -k` while the app is open, then press F5. You get a friendly *"Unable to connect…"* message, with no SQL error text. An entry appears in `Logs\application.log`.
- [ ] Start the app with a wrong database name in the `.dll.config`. It asks before creating anything.

---

## 20. Troubleshooting

| Problem | Cause / solution |
|---|---|
| **"Unable to connect to the inventory database. Please verify SQL Server LocalDB is running."** | Run `sqllocaldb info MSSQLLocalDB`. If it is stopped, run `sqllocaldb start MSSQLLocalDB`. Check the server name in `InventoryManagement.dll.config`. Details are in `Logs\application.log`. |
| **LocalDB is not installed** (`'sqllocaldb' is not recognized`) | Install the ".NET desktop development" workload in the Visual Studio Installer, or install `SqlLocalDB.msi` (step 1). |
| **LocalDB instance missing or corrupt** | `sqllocaldb create MSSQLLocalDB -s`. If it is broken: `sqllocaldb stop MSSQLLocalDB -k`, `sqllocaldb delete MSSQLLocalDB`, then `sqllocaldb create MSSQLLocalDB -s`. **Warning:** deleting the instance does not delete database files, but you will have to re-attach or re-create the database. |
| **LocalDB is very slow on first start** | The first connection after a reboot can take 10–30 seconds while LocalDB starts. Click Retry. |
| **Database does not exist** | Let the application create it (answer Yes), run `setup-database.cmd`, or run the scripts manually (step 3). |
| **"Cannot open database … Login failed for user"** | The database is missing, or your Windows user has no access to it. If it was created by another Windows user, create it again under your own account (LocalDB instances are per user). |
| **"A required database object is missing…" / stored procedure not found** | The scripts were not fully executed. Run `Tables.sql` and `StoredProcedures.sql` against `InventoryManagementDB`, or restart the app and accept the *install missing objects* prompt. |
| **Scripts ran against `master` by mistake** | The scripts detect this and stop with a message. Select `InventoryManagementDB` (or use `-d InventoryManagementDB`) and run them again. |
| **Cannot connect to SQL Express / another server** | Check the instance name (`.\SQLEXPRESS`), that the SQL Server service is running, and that `TrustServerCertificate=True` is present. |
| **"Item code already exists."** | Codes are unique and not case-sensitive. Choose another code, or edit or reactivate the existing item (tick *Show inactive items*). |
| **"Insufficient stock. Available quantity: X"** | You tried to issue more than is on hand. Post a Stock IN first or reduce the quantity. |
| **"Opening stock cannot be less than …"** | You tried to lower the opening stock below what has already been issued. |
| **The build fails restoring packages** | The first build needs internet access to nuget.org (or the packages in your offline cache). |
| **Designer error when opening a form** | Build the solution once (Ctrl+Shift+B), then reopen the form. |

---

## 21. Future extensions

The V1 structure was chosen so that these can be added without rewriting anything. None of them are
implemented in V1, and there are no placeholder menu items.

| Extension | How it fits |
|---|---|
| **Stock adjustment** | Insert `ADJUSTMENT_IN` (+1) / `ADJUSTMENT_OUT` (−1) into `TransactionTypes` and add a thin procedure that calls `usp_Stock_PostTransaction`. All stock calculations and reports pick the new types up automatically because they use `Direction`. |
| **Transfers, warehouses, locations** | Add `Warehouses`/`Locations` tables and a `LocationId` column on `StockTransactions`. Add `TRANSFER_OUT`/`TRANSFER_IN` types and post both rows in one transaction. Group `vw_ItemStock` by location. |
| **Purchase / purchase orders / suppliers** | `Suppliers`, `PurchaseOrders` and `PurchaseOrderLines` tables. Receiving goods posts IN transactions with the PO number as `ReferenceNo`. |
| **Sales / invoices / customers** | `Customers`, `SalesInvoices` and `InvoiceLines` tables. Dispatch posts OUT transactions and reuses the insufficient-stock check. |
| **Item categories table** | Replace the free-text `Category` with a `Categories` table and a `CategoryId` foreign key. |
| **Barcode** | Add a `Barcode` column (unique) to `Items`. A USB scanner types into the existing searchable item box. |
| **Excel export / import** | Add an export button to the report pages that writes the grid's data source (CSV needs no library). |
| **Login, users and roles** | `Users`/`Roles` tables and a login form before `MainForm`. Hide sidebar entries by role. |
| **Audit trail** | Add a `CreatedBy` column to transactions (the transaction table is already an append-only history) and an `ItemAudit` table filled by the item procedures. |

Each extension follows the same pattern: SQL script → stored procedure → repository method → service
method with validation → form.

---

*Small Scale Inventory v1.0.0 – Larch Technologies.*
