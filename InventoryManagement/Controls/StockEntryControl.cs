using InventoryManagement.Exceptions;
using InventoryManagement.Helpers;
using InventoryManagement.Models;
using InventoryManagement.Models.Reports;
using InventoryManagement.Services;

namespace InventoryManagement.Controls;

/// <summary>
/// The shared entry screen used by both Stock IN and Stock OUT.
///
///   Stock IN : Current Stock   + Add Quantity   = New Stock
///   Stock OUT: Available Stock - Issue Quantity = Remaining Stock
///
/// The live calculation uses <see cref="StockCalculator"/> - the same code the
/// service uses - so the preview always matches what will be saved. The
/// authoritative checks run in StockService and in the stored procedure.
/// </summary>
public partial class StockEntryControl : UserControl
{
    private const string NoValue = "—";

    private AppServices? _services;
    private StockEntryMode _mode = StockEntryMode.StockIn;
    private Item? _selectedItem;
    private decimal? _currentStock;

    /// <summary>Designer-friendly constructor; call <see cref="Initialize"/> before use.</summary>
    public StockEntryControl()
    {
        InitializeComponent();
        ApplyTheme();
        WireEvents();
    }

    /// <summary>Configures the control as the Stock IN or Stock OUT screen and loads data.</summary>
    public void Initialize(AppServices services, StockEntryMode mode)
    {
        _services = services ?? throw new ArgumentNullException(nameof(services));
        _mode = mode ?? throw new ArgumentNullException(nameof(mode));

        ApplyModeTexts();
        ReloadData();
    }

    /// <summary>Reloads the item list and recent transactions (F5).</summary>
    public void ReloadData()
    {
        if (_services is null)
        {
            return;
        }

        LoadItems();
        LoadRecentTransactions();
        ClearForm();
    }

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
        if (keyData == (Keys.Control | Keys.S))
        {
            SaveTransaction();
            return true;
        }

        return base.ProcessCmdKey(ref msg, keyData);
    }

    // ------------------------------------------------------------------ setup

    private void ApplyTheme()
    {
        BackColor = UiTheme.ContentBack;
        tlpRoot.BackColor = UiTheme.ContentBack;

        UiTheme.StyleCard(pnlEntry);
        UiTheme.StyleCard(pnlCalculation);
        UiTheme.StyleCard(pnlRecent);

        UiTheme.StyleSectionTitle(lblEntryTitle);
        UiTheme.StyleSectionTitle(lblCalculationTitle);
        UiTheme.StyleSectionTitle(lblRecentTitle);

        foreach (Label caption in new[] { lblItem, lblItemInfoCaption, lblQuantity, lblDate, lblReferenceNo, lblReason, lblRemarks })
        {
            caption.ForeColor = UiTheme.TextMuted;
        }

        lblItemInfo.ForeColor = UiTheme.TextPrimary;
        lblUnit.ForeColor = UiTheme.TextMuted;
        lblCalculationItem.ForeColor = UiTheme.TextMuted;
        lblCurrentCaption.ForeColor = UiTheme.TextMuted;
        lblQuantityCaption.ForeColor = UiTheme.TextMuted;
        pnlDivider.BackColor = UiTheme.Border;

        UiTheme.StyleSecondaryButton(btnClear);
        UiTheme.StyleGrid(dgvRecent);

        InputHelper.AllowDecimalOnly(txtQuantity);
        txtReferenceNo.MaxLength = FieldLengths.ReferenceNo;
        cboReason.MaxLength = FieldLengths.Reason;
        txtRemarks.MaxLength = FieldLengths.Remarks;

        GridHelper.AddTextColumn(dgvRecent, nameof(StockMovementReportRow.TransactionId), "Txn No", 50);
        GridHelper.AddDateColumn(dgvRecent, nameof(StockMovementReportRow.TransactionDate), "Date", includeTime: true, fillWeight: 85);
        GridHelper.AddTextColumn(dgvRecent, nameof(StockMovementReportRow.ItemCode), "Item Code", 60);
        GridHelper.AddTextColumn(dgvRecent, nameof(StockMovementReportRow.ItemName), "Item Name", 120);
        GridHelper.AddQuantityColumn(dgvRecent, nameof(StockMovementReportRow.Quantity), "Quantity", 60);
        GridHelper.AddTextColumn(dgvRecent, nameof(StockMovementReportRow.Unit), "Unit", 40, 50);
        GridHelper.AddTextColumn(dgvRecent, nameof(StockMovementReportRow.ReferenceNo), "Reference No", 70);
        GridHelper.AddTextColumn(dgvRecent, nameof(StockMovementReportRow.Reason), "Reason", 80);
    }

    private void WireEvents()
    {
        cboItem.SelectedIndexChanged += (_, _) => RefreshSelectedItem();
        cboItem.Leave += (_, _) => RefreshSelectedItem();
        cboItem.TextChanged += (_, _) => ClearSelectionIfTextChanged();
        txtQuantity.TextChanged += (_, _) => UpdateCalculation();

        btnSave.Click += (_, _) => SaveTransaction();
        btnClear.Click += (_, _) => ClearForm();

        // Enter moves to the next field in the single-line inputs.
        foreach (Control input in new Control[] { cboItem, txtQuantity, dtpDate, txtReferenceNo, cboReason })
        {
            input.KeyDown += MoveToNextFieldOnEnter;
        }
    }

    private void ApplyModeTexts()
    {
        lblEntryTitle.Text = _mode.EntryTitle;
        lblCurrentCaption.Text = _mode.CurrentStockCaption;
        lblQuantityCaption.Text = _mode.QuantityCaption;
        lblNewCaption.Text = _mode.NewStockCaption;
        lblRecentTitle.Text = _mode.RecentTitle;
        lblQuantity.Text = _mode.QuantityCaption + " *";
        btnSave.Text = _mode.SaveButtonText;

        if (_mode.IsOutward)
        {
            UiTheme.StyleDangerButton(btnSave);
            lblItemInfoCaption.Text = "Available";
        }
        else
        {
            UiTheme.StyleSuccessButton(btnSave);
            lblItemInfoCaption.Text = "Current stock";
        }

        ComboBoxHelper.BindSuggestions(cboReason, _mode.Reasons);
    }

    // ------------------------------------------------------------------ data

    private void LoadItems()
    {
        try
        {
            IReadOnlyList<Item> items = _services!.Items.GetActiveItems();
            ComboBoxHelper.BindItems(cboItem, items);
        }
        catch (Exception ex)
        {
            MessageHelper.HandleException(ex, "loading the item list");
        }
    }

    private void LoadRecentTransactions()
    {
        try
        {
            IReadOnlyList<StockMovementReportRow> rows =
                _services!.Stock.GetRecentTransactions(_mode.TransactionType, AppSettings.RecentTransactionCount);
            GridHelper.Bind(dgvRecent, rows);
        }
        catch (Exception ex)
        {
            MessageHelper.HandleException(ex, "loading recent transactions");
        }
    }

    /// <summary>Resolves the selected / typed item and reads its current stock from the database.</summary>
    private void RefreshSelectedItem(bool forceReload = false)
    {
        if (_services is null)
        {
            return;
        }

        Item? item = ComboBoxHelper.GetSelectedItem(cboItem);
        if (!forceReload && item?.ItemId == _selectedItem?.ItemId)
        {
            return;
        }

        _selectedItem = item;
        _currentStock = null;

        if (item is not null)
        {
            try
            {
                _currentStock = _services.Stock.GetCurrentStock(item.ItemId);
            }
            catch (Exception ex)
            {
                MessageHelper.HandleException(ex, "reading the current stock");
            }
        }

        UpdateItemDisplay();
        UpdateCalculation();
    }

    /// <summary>
    /// While the user edits the item text, forget the previous item so the
    /// calculation never shows figures for an item that is no longer selected.
    /// (Typed text is resolved when the field is left or on Save.)
    /// </summary>
    private void ClearSelectionIfTextChanged()
    {
        if (_selectedItem is null ||
            string.Equals(cboItem.Text.Trim(), _selectedItem.DisplayName, StringComparison.CurrentCultureIgnoreCase))
        {
            return;
        }

        _selectedItem = null;
        _currentStock = null;
        UpdateItemDisplay();
        UpdateCalculation();
    }

    // ------------------------------------------------------------------ display

    private void UpdateItemDisplay()
    {
        if (_selectedItem is null)
        {
            lblItemInfo.Text = "Select an item";
            lblItemInfo.ForeColor = UiTheme.TextMuted;
            lblUnit.Text = string.Empty;
            lblCalculationItem.Text = "No item selected";
            return;
        }

        string unit = _selectedItem.Unit;
        lblUnit.Text = unit;
        lblCalculationItem.Text = _selectedItem.DisplayName +
            (string.IsNullOrWhiteSpace(_selectedItem.Category) ? string.Empty : "  •  " + _selectedItem.Category);

        if (_currentStock is decimal current)
        {
            string status = current <= 0 ? StockStatus.OutOfStock
                : current <= _selectedItem.MinimumStock ? StockStatus.Low
                : StockStatus.Normal;

            lblItemInfo.Text = $"{UiFormats.FormatQuantity(current, unit)}   (minimum {UiFormats.FormatQuantity(_selectedItem.MinimumStock, unit)})   {status}";
            lblItemInfo.ForeColor = UiTheme.GetStatusColors(status).Fore;
        }
        else
        {
            lblItemInfo.Text = "Current stock not available";
            lblItemInfo.ForeColor = UiTheme.TextMuted;
        }
    }

    private void UpdateCalculation()
    {
        string unit = _selectedItem?.Unit ?? string.Empty;
        bool hasQuantity = InputHelper.TryGetDecimal(txtQuantity, out decimal quantity) && quantity > 0;

        lblCurrentValue.Text = _currentStock is decimal current ? UiFormats.FormatQuantity(current, unit) : NoValue;
        lblQuantityValue.Text = hasQuantity ? $"{_mode.QuantitySign} {UiFormats.FormatQuantity(quantity, unit)}" : NoValue;
        lblQuantityValue.ForeColor = _mode.AccentColor;

        HideWarning();

        if (_currentStock is not decimal available || !hasQuantity)
        {
            lblNewValue.Text = NoValue;
            lblNewValue.ForeColor = UiTheme.TextPrimary;
            return;
        }

        decimal newStock = StockCalculator.ApplyMovement(available, quantity, _mode.TransactionType);

        if (StockCalculator.WouldGoNegative(available, quantity, _mode.TransactionType))
        {
            // Stock can never go negative: show why the entry will be refused.
            lblNewValue.Text = UiFormats.FormatQuantity(newStock, unit);
            lblNewValue.ForeColor = UiTheme.Danger;
            ShowWarning(InsufficientStockException.ForAvailable(available, unit).Message, StockStatus.OutOfStock);
            return;
        }

        lblNewValue.Text = UiFormats.FormatQuantity(newStock, unit);
        lblNewValue.ForeColor = _mode.IsOutward ? UiTheme.TextPrimary : UiTheme.Success;

        if (_selectedItem is not null && _mode.IsOutward && newStock <= _selectedItem.MinimumStock)
        {
            ShowWarning(
                $"After this issue the stock will be at or below the minimum level of {UiFormats.FormatQuantity(_selectedItem.MinimumStock, unit)}.",
                StockStatus.Low);
        }
    }

    private void ShowWarning(string message, string status)
    {
        (Color back, Color fore) = UiTheme.GetStatusColors(status);
        lblWarning.BackColor = back;
        lblWarning.ForeColor = fore;
        lblWarning.Text = message;
        lblWarning.Visible = true;
    }

    private void HideWarning()
    {
        lblWarning.Visible = false;
        lblWarning.Text = string.Empty;
    }

    // ------------------------------------------------------------------ actions

    private void SaveTransaction()
    {
        if (_services is null)
        {
            return;
        }

        Item? item = ComboBoxHelper.GetSelectedItem(cboItem);
        if (item is null)
        {
            MessageHelper.ShowWarning("Please select an item from the list (type the item code or part of the name).");
            cboItem.Focus();
            return;
        }

        if (!InputHelper.TryGetDecimal(txtQuantity, out decimal quantity))
        {
            MessageHelper.ShowWarning("Please enter a valid quantity greater than zero.");
            txtQuantity.Focus();
            return;
        }

        var transaction = new StockTransaction
        {
            ItemId = item.ItemId,
            TransactionType = _mode.TransactionType,
            Quantity = quantity,
            TransactionDate = CombineWithCurrentTime(dtpDate.Value),
            ReferenceNo = txtReferenceNo.Text,
            Reason = cboReason.Text,
            Remarks = txtRemarks.Text,
        };

        StockOperationResult result;
        try
        {
            using (new WaitCursorScope())
            {
                result = _mode.IsOutward
                    ? _services.Stock.IssueStock(transaction)
                    : _services.Stock.AddStock(transaction);
            }
        }
        catch (InsufficientStockException ex)
        {
            // Someone else may have issued stock meanwhile: show the fresh figure.
            RefreshSelectedItem(forceReload: true);
            MessageHelper.ShowWarning(ex.Message);
            txtQuantity.Focus();
            txtQuantity.SelectAll();
            return;
        }
        catch (ValidationException ex)
        {
            MessageHelper.ShowWarning(ex.Message);
            FocusField(ex.FieldName);
            return;
        }
        catch (Exception ex)
        {
            MessageHelper.HandleException(ex, "saving the stock transaction");
            return;
        }

        // Show the refreshed stock figures, confirm, then reset the form.
        _currentStock = result.NewStock;
        UpdateItemDisplay();
        UpdateCalculation();

        MessageHelper.ShowSuccess(
            _mode.SuccessMessage + Environment.NewLine + Environment.NewLine +
            $"Item: {item.DisplayName}" + Environment.NewLine +
            $"{_mode.CurrentStockCaption}: {UiFormats.FormatQuantity(result.PreviousStock, item.Unit)}" + Environment.NewLine +
            $"{_mode.QuantityCaption}: {UiFormats.FormatQuantity(quantity, item.Unit)}" + Environment.NewLine +
            $"{_mode.NewStockCaption}: {UiFormats.FormatQuantity(result.NewStock, item.Unit)}" + Environment.NewLine +
            $"Transaction No: {result.TransactionId}");

        LoadItems();
        LoadRecentTransactions();
        ClearForm();
    }

    private void ClearForm()
    {
        _selectedItem = null;
        _currentStock = null;

        cboItem.SelectedIndex = -1;
        cboItem.Text = string.Empty;
        txtQuantity.Clear();

        dtpDate.MinDate = ValidationHelper.MinimumDate;
        dtpDate.MaxDate = DateTime.Today.AddDays(1).AddSeconds(-1);
        dtpDate.Value = DateTime.Today;

        txtReferenceNo.Clear();
        cboReason.Text = string.Empty;
        txtRemarks.Clear();

        UpdateItemDisplay();
        UpdateCalculation();

        if (Visible)
        {
            cboItem.Focus();
        }
    }

    private void FocusField(string? fieldName)
    {
        Control target = fieldName switch
        {
            nameof(StockTransaction.Quantity) => txtQuantity,
            nameof(StockTransaction.TransactionDate) => dtpDate,
            nameof(StockTransaction.ReferenceNo) => txtReferenceNo,
            nameof(StockTransaction.Reason) => cboReason,
            nameof(StockTransaction.Remarks) => txtRemarks,
            _ => cboItem,
        };
        target.Focus();
    }

    private void MoveToNextFieldOnEnter(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter && e.Modifiers == Keys.None && sender is Control control)
        {
            if (control is ComboBox { DroppedDown: true })
            {
                return;
            }

            e.SuppressKeyPress = true;
            SelectNextControl(control, forward: true, tabStopOnly: true, nested: true, wrap: false);
        }
    }

    /// <summary>
    /// The picker supplies the date; the current time of day is added so
    /// entries made on the same day keep their order. Truncated to whole
    /// seconds to match the DATETIME2(0) column (no rounding into the next day).
    /// </summary>
    private static DateTime CombineWithCurrentTime(DateTime date)
    {
        DateTime value = date.Date + DateTime.Now.TimeOfDay;
        return new DateTime(value.Ticks - (value.Ticks % TimeSpan.TicksPerSecond), DateTimeKind.Unspecified);
    }
}
