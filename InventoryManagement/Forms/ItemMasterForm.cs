using InventoryManagement.Exceptions;
using InventoryManagement.Helpers;
using InventoryManagement.Models;

namespace InventoryManagement.Forms;

/// <summary>
/// Item Master: add, edit, search and activate / deactivate items.
///
///   Add    - creates a new item from the fields (NEW ITEM mode).
///   Edit   - loads the selected grid row into the fields; press again
///            ("Save Changes") to store the changes.
///   Clear  - resets the fields to NEW ITEM mode.
///   Search - filters the grid by code, name or category.
///   Deactivate / Activate - inactive items cannot receive stock entries
///            but keep their history (items are never deleted).
/// </summary>
public partial class ItemMasterForm : Form, IRefreshablePage
{
    private const string EditSelectedText = "Edit Selected";
    private const string SaveChangesText = "Save Changes";

    private readonly AppServices _services;
    private Item? _editingItem;

    /// <summary>Designer support only.</summary>
    public ItemMasterForm()
    {
        InitializeComponent();
        _services = null!;
    }

    public ItemMasterForm(AppServices services)
    {
        InitializeComponent();
        _services = services ?? throw new ArgumentNullException(nameof(services));

        ApplyTheme();
        ConfigureGrid();
        WireEvents();
    }

    private bool IsEditing => _editingItem is not null;

    public void RefreshData()
    {
        LoadLookups();
        LoadItems(keepItemId: _editingItem?.ItemId);
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        if (_services is not null)
        {
            LoadLookups();
            LoadItems();
            SetNewMode();
        }
    }

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
        if (keyData == (Keys.Control | Keys.S))
        {
            if (IsEditing)
            {
                SaveChanges();
            }
            else
            {
                AddItem();
            }

            return true;
        }

        if (keyData == Keys.Escape && IsEditing)
        {
            SetNewMode();
            return true;
        }

        return base.ProcessCmdKey(ref msg, keyData);
    }

    // ------------------------------------------------------------------ setup

    private void ApplyTheme()
    {
        UiTheme.ApplyPage(this);
        tlpMain.BackColor = UiTheme.ContentBack;
        UiTheme.StyleCard(pnlEditor);
        UiTheme.StyleCard(pnlList);
        UiTheme.StyleSectionTitle(lblEditorTitle);
        UiTheme.StyleSectionTitle(lblListTitle);
        UiTheme.StyleMutedLabel(lblCount);

        foreach (Label caption in new[] { lblItemCode, lblItemName, lblCategory, lblUnit, lblMinimumStock, lblOpeningStock, lblStatus })
        {
            caption.ForeColor = UiTheme.TextMuted;
        }

        UiTheme.StylePrimaryButton(btnAdd);
        UiTheme.StyleSuccessButton(btnEdit);
        UiTheme.StyleSecondaryButton(btnClear);
        UiTheme.StyleSecondaryButton(btnToggleActive);
        UiTheme.StyleSecondaryButton(btnSearch);

        txtItemCode.MaxLength = FieldLengths.ItemCode;
        txtItemCode.CharacterCasing = CharacterCasing.Upper;
        txtItemName.MaxLength = FieldLengths.ItemName;
        cboCategory.MaxLength = FieldLengths.Category;
        cboUnit.MaxLength = FieldLengths.Unit;
        txtSearch.MaxLength = FieldLengths.SearchText;
        InputHelper.AllowDecimalOnly(txtMinimumStock);
        InputHelper.AllowDecimalOnly(txtOpeningStock);

        toolTip.SetToolTip(txtOpeningStock, "Stock on hand when the item is created. Later changes should be made through Stock IN / Stock OUT.");
        toolTip.SetToolTip(txtMinimumStock, "An item is LOW STOCK when its current stock is at or below this level.");
        toolTip.SetToolTip(btnToggleActive, "Inactive items are hidden from Stock IN / OUT but keep their history.");
        toolTip.SetToolTip(dgvItems, "Double-click a row (or press Enter) to edit the item.");
    }

    private void ConfigureGrid()
    {
        UiTheme.StyleGrid(dgvItems);
        GridHelper.AddTextColumn(dgvItems, nameof(Item.ItemCode), "Item Code", 70);
        GridHelper.AddTextColumn(dgvItems, nameof(Item.ItemName), "Item Name", 150);
        GridHelper.AddTextColumn(dgvItems, nameof(Item.Category), "Category", 90);
        GridHelper.AddTextColumn(dgvItems, nameof(Item.Unit), "Unit", 45, 50);
        GridHelper.AddQuantityColumn(dgvItems, nameof(Item.MinimumStock), "Min Stock", 60);
        GridHelper.AddQuantityColumn(dgvItems, nameof(Item.OpeningStock), "Opening", 60);
        GridHelper.AddQuantityColumn(dgvItems, nameof(Item.CurrentStock), "Current Stock", 70);
        GridHelper.AddCenteredColumn(dgvItems, nameof(Item.StockStatus), "Stock Status", 80);
        GridHelper.AddCenteredColumn(dgvItems, nameof(Item.StatusText), "Active", 50);
        GridHelper.EnableStockStatusColors(dgvItems, "col" + nameof(Item.StockStatus));

        // Inactive items are shown in grey.
        dgvItems.CellFormatting += (_, e) =>
        {
            if (e.RowIndex >= 0 && e.CellStyle is not null &&
                dgvItems.Rows[e.RowIndex].DataBoundItem is Item { IsActive: false } &&
                dgvItems.Columns[e.ColumnIndex].Name != "col" + nameof(Item.StockStatus))
            {
                e.CellStyle.ForeColor = UiTheme.InactiveText;
                e.CellStyle.SelectionForeColor = UiTheme.InactiveText;
            }
        };
    }

    private void WireEvents()
    {
        btnAdd.Click += (_, _) => AddItem();
        btnEdit.Click += (_, _) =>
        {
            if (IsEditing)
            {
                SaveChanges();
            }
            else
            {
                LoadSelectedItemForEditing();
            }
        };
        btnClear.Click += (_, _) => SetNewMode();
        btnToggleActive.Click += (_, _) => ToggleActive();
        btnSearch.Click += (_, _) => LoadItems();
        chkShowInactive.CheckedChanged += (_, _) => LoadItems();

        txtSearch.KeyDown += (_, e) =>
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                LoadItems();
            }
        };

        dgvItems.CellDoubleClick += (_, e) =>
        {
            if (e.RowIndex >= 0)
            {
                LoadSelectedItemForEditing();
            }
        };
        dgvItems.KeyDown += (_, e) =>
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                LoadSelectedItemForEditing();
            }
        };
        dgvItems.SelectionChanged += (_, _) => UpdateButtons();

        foreach (Control input in new Control[] { txtItemCode, txtItemName, cboCategory, cboUnit, txtMinimumStock, txtOpeningStock })
        {
            input.TextChanged += (_, _) => errorProvider.SetError(input, string.Empty);
        }
    }

    // ------------------------------------------------------------------ data

    private void LoadLookups()
    {
        try
        {
            ComboBoxHelper.BindSuggestions(cboCategory, _services.Items.GetCategories());
            ComboBoxHelper.BindSuggestions(cboUnit, _services.Items.GetUnits());
        }
        catch (Exception ex)
        {
            MessageHelper.HandleException(ex, "loading categories and units");
        }
    }

    private void LoadItems(int? keepItemId = null)
    {
        try
        {
            IReadOnlyList<Item> items;
            using (new WaitCursorScope())
            {
                items = _services.Items.SearchItems(txtSearch.Text, chkShowInactive.Checked);
            }

            GridHelper.Bind(dgvItems, items);
            lblCount.Text = items.Count == 1 ? "1 item" : $"{items.Count:N0} items";

            if (keepItemId is int id)
            {
                SelectRow(id);
            }
        }
        catch (Exception ex)
        {
            MessageHelper.HandleException(ex, "loading items");
        }

        UpdateButtons();
    }

    private void SelectRow(int itemId)
    {
        foreach (DataGridViewRow row in dgvItems.Rows)
        {
            if (row.DataBoundItem is Item item && item.ItemId == itemId)
            {
                row.Selected = true;
                dgvItems.CurrentCell = row.Cells[0];
                return;
            }
        }
    }

    // ------------------------------------------------------------------ actions

    private void AddItem()
    {
        if (IsEditing)
        {
            return;
        }

        Item? item = ReadItemFromFields(new Item { IsActive = chkIsActive.Checked });
        if (item is null)
        {
            return;
        }

        try
        {
            using (new WaitCursorScope())
            {
                _services.Items.CreateItem(item);
            }
        }
        catch (Exception ex)
        {
            HandleSaveError(ex, "adding the item");
            return;
        }

        MessageHelper.ShowSuccess($"Item '{item.ItemCode}' was added successfully.");
        LoadLookups();
        LoadItems(keepItemId: item.ItemId);
        SetNewMode();
    }

    private void SaveChanges()
    {
        if (_editingItem is null)
        {
            return;
        }

        var changes = new Item
        {
            ItemId = _editingItem.ItemId,
            IsActive = _editingItem.IsActive,
        };

        Item? item = ReadItemFromFields(changes);
        if (item is null)
        {
            return;
        }

        try
        {
            using (new WaitCursorScope())
            {
                _services.Items.UpdateItem(item);
            }
        }
        catch (Exception ex)
        {
            HandleSaveError(ex, "saving the item");
            return;
        }

        MessageHelper.ShowSuccess($"Item '{item.ItemCode}' was updated successfully.");
        LoadLookups();
        LoadItems(keepItemId: item.ItemId);
        SetNewMode();
    }

    private void ToggleActive()
    {
        Item? target = GetToggleTarget();
        if (target is null)
        {
            MessageHelper.ShowWarning("Please select an item in the list first.");
            return;
        }

        bool activate = !target.IsActive;
        string question = activate
            ? $"Activate item '{target.DisplayName}'?\n\nIt will be available again for Stock IN and Stock OUT."
            : $"Deactivate item '{target.DisplayName}'?\n\nIt will no longer appear in Stock IN / Stock OUT. Its stock history is kept and it can be activated again later.";

        if (!MessageHelper.Confirm(question, defaultToNo: !activate))
        {
            return;
        }

        try
        {
            _services.Items.SetItemActive(target.ItemId, activate);
        }
        catch (Exception ex)
        {
            MessageHelper.HandleException(ex, activate ? "activating the item" : "deactivating the item");
            return;
        }

        MessageHelper.ShowSuccess($"Item '{target.ItemCode}' is now {(activate ? "active" : "inactive")}.");
        int id = target.ItemId;
        SetNewMode();
        LoadItems(keepItemId: id);
    }

    private void LoadSelectedItemForEditing()
    {
        Item? selected = GridHelper.GetSelected<Item>(dgvItems);
        if (selected is null)
        {
            MessageHelper.ShowWarning("Please select an item in the list to edit.");
            return;
        }

        Item item;
        try
        {
            // Re-read so the latest values from the database are edited.
            item = _services.Items.GetItem(selected.ItemId);
        }
        catch (Exception ex)
        {
            MessageHelper.HandleException(ex, "loading the item");
            return;
        }

        _editingItem = item;
        errorProvider.Clear();

        txtItemCode.Text = item.ItemCode;
        txtItemName.Text = item.ItemName;
        cboCategory.Text = item.Category ?? string.Empty;
        cboUnit.Text = item.Unit;
        txtMinimumStock.Text = InputHelper.FormatForEditing(item.MinimumStock);
        txtOpeningStock.Text = InputHelper.FormatForEditing(item.OpeningStock);
        chkIsActive.Checked = item.IsActive;
        chkIsActive.Enabled = false;

        lblMode.Text = $"EDITING: {item.ItemCode}   (current stock {UiFormats.FormatQuantity(item.CurrentStock, item.Unit)})";
        lblMode.ForeColor = UiTheme.Primary;
        UpdateButtons();
        txtItemName.Focus();
        txtItemName.SelectAll();
    }

    private void SetNewMode()
    {
        _editingItem = null;
        errorProvider.Clear();

        txtItemCode.Clear();
        txtItemName.Clear();
        cboCategory.Text = string.Empty;
        cboUnit.Text = string.Empty;
        txtMinimumStock.Text = "0";
        txtOpeningStock.Text = "0";
        chkIsActive.Checked = true;
        chkIsActive.Enabled = true;

        lblMode.Text = "NEW ITEM";
        lblMode.ForeColor = UiTheme.TextMuted;
        UpdateButtons();

        if (Visible)
        {
            txtItemCode.Focus();
        }
    }

    private void UpdateButtons()
    {
        btnAdd.Enabled = !IsEditing;
        btnEdit.Text = IsEditing ? SaveChangesText : EditSelectedText;
        btnEdit.Enabled = IsEditing || dgvItems.CurrentRow is not null;

        Item? target = GetToggleTarget();
        btnToggleActive.Enabled = target is not null;
        btnToggleActive.Text = target is { IsActive: false } ? "Activate" : "Deactivate";
    }

    private Item? GetToggleTarget() => _editingItem ?? GridHelper.GetSelected<Item>(dgvItems);

    // ------------------------------------------------------------------ helpers

    /// <summary>
    /// Copies the fields into <paramref name="item"/>. Returns null (after showing
    /// a message) when a number cannot be read. All business rules are checked by
    /// ItemService and again by the stored procedure.
    /// </summary>
    private Item? ReadItemFromFields(Item item)
    {
        errorProvider.Clear();

        if (!InputHelper.TryGetDecimal(txtMinimumStock, out decimal minimumStock) && !string.IsNullOrWhiteSpace(txtMinimumStock.Text))
        {
            ShowFieldError(txtMinimumStock, "Minimum stock must be a valid number (0 or more).");
            return null;
        }

        if (!InputHelper.TryGetDecimal(txtOpeningStock, out decimal openingStock) && !string.IsNullOrWhiteSpace(txtOpeningStock.Text))
        {
            ShowFieldError(txtOpeningStock, "Opening stock must be a valid number (0 or more).");
            return null;
        }

        item.ItemCode = txtItemCode.Text;
        item.ItemName = txtItemName.Text;
        item.Category = cboCategory.Text;
        item.Unit = cboUnit.Text;
        item.MinimumStock = minimumStock;
        item.OpeningStock = openingStock;
        return item;
    }

    private void HandleSaveError(Exception ex, string action)
    {
        if (ex is ValidationException validation)
        {
            Control field = GetFieldControl(validation.FieldName);
            ShowFieldError(field, validation.Message);
            return;
        }

        MessageHelper.HandleException(ex, action);
    }

    private void ShowFieldError(Control field, string message)
    {
        errorProvider.SetError(field, message);
        MessageHelper.ShowWarning(message);
        field.Focus();
    }

    private Control GetFieldControl(string? fieldName) => fieldName switch
    {
        nameof(Item.ItemName) => txtItemName,
        nameof(Item.Category) => cboCategory,
        nameof(Item.Unit) => cboUnit,
        nameof(Item.MinimumStock) => txtMinimumStock,
        nameof(Item.OpeningStock) => txtOpeningStock,
        _ => txtItemCode,
    };
}
