namespace InventoryManagement.Forms
{
    partial class ItemMasterForm
    {
        /// <summary>Required designer variable.</summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>Clean up any resources being used.</summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.pnlEditor = new System.Windows.Forms.Panel();
            this.tlpEditor = new System.Windows.Forms.TableLayoutPanel();
            this.lblItemCode = new System.Windows.Forms.Label();
            this.txtItemCode = new System.Windows.Forms.TextBox();
            this.lblCategory = new System.Windows.Forms.Label();
            this.cboCategory = new System.Windows.Forms.ComboBox();
            this.lblItemName = new System.Windows.Forms.Label();
            this.txtItemName = new System.Windows.Forms.TextBox();
            this.lblUnit = new System.Windows.Forms.Label();
            this.cboUnit = new System.Windows.Forms.ComboBox();
            this.lblMinimumStock = new System.Windows.Forms.Label();
            this.txtMinimumStock = new System.Windows.Forms.TextBox();
            this.lblOpeningStock = new System.Windows.Forms.Label();
            this.txtOpeningStock = new System.Windows.Forms.TextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.chkIsActive = new System.Windows.Forms.CheckBox();
            this.flpButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnToggleActive = new System.Windows.Forms.Button();
            this.pnlEditorHeader = new System.Windows.Forms.Panel();
            this.lblEditorTitle = new System.Windows.Forms.Label();
            this.lblMode = new System.Windows.Forms.Label();
            this.pnlList = new System.Windows.Forms.Panel();
            this.dgvItems = new System.Windows.Forms.DataGridView();
            this.pnlListToolbar = new System.Windows.Forms.Panel();
            this.flpSearch = new System.Windows.Forms.FlowLayoutPanel();
            this.lblListTitle = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.chkShowInactive = new System.Windows.Forms.CheckBox();
            this.lblCount = new System.Windows.Forms.Label();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.tlpMain.SuspendLayout();
            this.pnlEditor.SuspendLayout();
            this.tlpEditor.SuspendLayout();
            this.flpButtons.SuspendLayout();
            this.pnlEditorHeader.SuspendLayout();
            this.pnlList.SuspendLayout();
            this.pnlListToolbar.SuspendLayout();
            this.flpSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            //
            // tlpMain
            //
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.pnlEditor, 0, 0);
            this.tlpMain.Controls.Add(this.pnlList, 0, 1);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 2;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 270F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Size = new System.Drawing.Size(1000, 640);
            this.tlpMain.TabIndex = 0;
            //
            // pnlEditor
            //
            this.pnlEditor.BackColor = System.Drawing.Color.White;
            this.pnlEditor.Controls.Add(this.tlpEditor);
            this.pnlEditor.Controls.Add(this.pnlEditorHeader);
            this.pnlEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlEditor.Margin = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.pnlEditor.Name = "pnlEditor";
            this.pnlEditor.Padding = new System.Windows.Forms.Padding(20, 10, 28, 8);
            this.pnlEditor.TabIndex = 0;
            //
            // tlpEditor
            //
            this.tlpEditor.ColumnCount = 4;
            this.tlpEditor.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 125F));
            this.tlpEditor.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpEditor.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 145F));
            this.tlpEditor.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpEditor.Controls.Add(this.lblItemCode, 0, 0);
            this.tlpEditor.Controls.Add(this.txtItemCode, 1, 0);
            this.tlpEditor.Controls.Add(this.lblCategory, 2, 0);
            this.tlpEditor.Controls.Add(this.cboCategory, 3, 0);
            this.tlpEditor.Controls.Add(this.lblItemName, 0, 1);
            this.tlpEditor.Controls.Add(this.txtItemName, 1, 1);
            this.tlpEditor.Controls.Add(this.lblUnit, 0, 2);
            this.tlpEditor.Controls.Add(this.cboUnit, 1, 2);
            this.tlpEditor.Controls.Add(this.lblMinimumStock, 2, 2);
            this.tlpEditor.Controls.Add(this.txtMinimumStock, 3, 2);
            this.tlpEditor.Controls.Add(this.lblOpeningStock, 0, 3);
            this.tlpEditor.Controls.Add(this.txtOpeningStock, 1, 3);
            this.tlpEditor.Controls.Add(this.lblStatus, 2, 3);
            this.tlpEditor.Controls.Add(this.chkIsActive, 3, 3);
            this.tlpEditor.Controls.Add(this.flpButtons, 0, 4);
            this.tlpEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpEditor.Name = "tlpEditor";
            this.tlpEditor.RowCount = 6;
            this.tlpEditor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tlpEditor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tlpEditor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tlpEditor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tlpEditor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.tlpEditor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpEditor.TabIndex = 1;
            this.tlpEditor.SetColumnSpan(this.txtItemName, 3);
            this.tlpEditor.SetColumnSpan(this.flpButtons, 4);
            //
            // lblItemCode
            //
            this.lblItemCode.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblItemCode.AutoSize = true;
            this.lblItemCode.Name = "lblItemCode";
            this.lblItemCode.TabIndex = 0;
            this.lblItemCode.Text = "Item Code *";
            //
            // txtItemCode
            //
            this.txtItemCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtItemCode.Name = "txtItemCode";
            this.txtItemCode.TabIndex = 1;
            this.txtItemCode.MaxLength = 50;
            //
            // lblCategory
            //
            this.lblCategory.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblCategory.AutoSize = true;
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.TabIndex = 2;
            this.lblCategory.Text = "Category";
            //
            // cboCategory
            //
            this.cboCategory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cboCategory.FormattingEnabled = true;
            this.cboCategory.MaxLength = 100;
            this.cboCategory.Name = "cboCategory";
            this.cboCategory.TabIndex = 3;
            //
            // lblItemName
            //
            this.lblItemName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblItemName.AutoSize = true;
            this.lblItemName.Name = "lblItemName";
            this.lblItemName.TabIndex = 4;
            this.lblItemName.Text = "Item Name *";
            //
            // txtItemName
            //
            this.txtItemName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtItemName.Name = "txtItemName";
            this.txtItemName.TabIndex = 5;
            this.txtItemName.MaxLength = 200;
            //
            // lblUnit
            //
            this.lblUnit.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblUnit.AutoSize = true;
            this.lblUnit.Name = "lblUnit";
            this.lblUnit.TabIndex = 6;
            this.lblUnit.Text = "Unit *";
            //
            // cboUnit
            //
            this.cboUnit.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cboUnit.FormattingEnabled = true;
            this.cboUnit.MaxLength = 50;
            this.cboUnit.Name = "cboUnit";
            this.cboUnit.Size = new System.Drawing.Size(160, 25);
            this.cboUnit.TabIndex = 7;
            //
            // lblMinimumStock
            //
            this.lblMinimumStock.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblMinimumStock.AutoSize = true;
            this.lblMinimumStock.Name = "lblMinimumStock";
            this.lblMinimumStock.TabIndex = 8;
            this.lblMinimumStock.Text = "Minimum Stock";
            //
            // txtMinimumStock
            //
            this.txtMinimumStock.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtMinimumStock.Name = "txtMinimumStock";
            this.txtMinimumStock.TabIndex = 9;
            this.txtMinimumStock.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtMinimumStock.Size = new System.Drawing.Size(160, 25);
            //
            // lblOpeningStock
            //
            this.lblOpeningStock.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblOpeningStock.AutoSize = true;
            this.lblOpeningStock.Name = "lblOpeningStock";
            this.lblOpeningStock.TabIndex = 10;
            this.lblOpeningStock.Text = "Opening Stock";
            //
            // txtOpeningStock
            //
            this.txtOpeningStock.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtOpeningStock.Name = "txtOpeningStock";
            this.txtOpeningStock.TabIndex = 11;
            this.txtOpeningStock.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtOpeningStock.Size = new System.Drawing.Size(160, 25);
            //
            // lblStatus
            //
            this.lblStatus.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblStatus.AutoSize = true;
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.TabIndex = 12;
            this.lblStatus.Text = "Status";
            //
            // chkIsActive
            //
            this.chkIsActive.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkIsActive.AutoSize = true;
            this.chkIsActive.Checked = true;
            this.chkIsActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIsActive.Name = "chkIsActive";
            this.chkIsActive.TabIndex = 13;
            this.chkIsActive.Text = "Active";
            this.chkIsActive.UseVisualStyleBackColor = true;
            //
            // flpButtons
            //
            this.flpButtons.Controls.Add(this.btnAdd);
            this.flpButtons.Controls.Add(this.btnEdit);
            this.flpButtons.Controls.Add(this.btnClear);
            this.flpButtons.Controls.Add(this.btnToggleActive);
            this.flpButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpButtons.Margin = new System.Windows.Forms.Padding(125, 4, 0, 0);
            this.flpButtons.Name = "flpButtons";
            this.flpButtons.TabIndex = 14;
            this.flpButtons.WrapContents = false;
            //
            // btnAdd
            //
            this.btnAdd.Margin = new System.Windows.Forms.Padding(0, 4, 10, 0);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(120, 36);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Add Item";
            //
            // btnEdit
            //
            this.btnEdit.Margin = new System.Windows.Forms.Padding(0, 4, 10, 0);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(130, 36);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.Text = "Save Changes";
            //
            // btnClear
            //
            this.btnClear.Margin = new System.Windows.Forms.Padding(0, 4, 10, 0);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(100, 36);
            this.btnClear.TabIndex = 2;
            this.btnClear.Text = "Clear";
            //
            // btnToggleActive
            //
            this.btnToggleActive.Margin = new System.Windows.Forms.Padding(0, 4, 10, 0);
            this.btnToggleActive.Name = "btnToggleActive";
            this.btnToggleActive.Size = new System.Drawing.Size(120, 36);
            this.btnToggleActive.TabIndex = 3;
            this.btnToggleActive.Text = "Deactivate";
            //
            // pnlEditorHeader
            //
            this.pnlEditorHeader.Controls.Add(this.lblEditorTitle);
            this.pnlEditorHeader.Controls.Add(this.lblMode);
            this.pnlEditorHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlEditorHeader.Name = "pnlEditorHeader";
            this.pnlEditorHeader.Size = new System.Drawing.Size(900, 38);
            this.pnlEditorHeader.TabIndex = 0;
            //
            // lblEditorTitle
            //
            this.lblEditorTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEditorTitle.Name = "lblEditorTitle";
            this.lblEditorTitle.TabIndex = 0;
            this.lblEditorTitle.Text = "Item Details";
            this.lblEditorTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblMode
            //
            this.lblMode.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblMode.Name = "lblMode";
            this.lblMode.Size = new System.Drawing.Size(320, 38);
            this.lblMode.TabIndex = 1;
            this.lblMode.Text = "NEW ITEM";
            this.lblMode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // pnlList
            //
            this.pnlList.BackColor = System.Drawing.Color.White;
            this.pnlList.Controls.Add(this.dgvItems);
            this.pnlList.Controls.Add(this.pnlListToolbar);
            this.pnlList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlList.Margin = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.pnlList.Name = "pnlList";
            this.pnlList.Padding = new System.Windows.Forms.Padding(16, 8, 16, 12);
            this.pnlList.TabIndex = 1;
            //
            // dgvItems
            //
            this.dgvItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvItems.Name = "dgvItems";
            this.dgvItems.TabIndex = 1;
            //
            // pnlListToolbar
            //
            this.pnlListToolbar.Controls.Add(this.flpSearch);
            this.pnlListToolbar.Controls.Add(this.lblCount);
            this.pnlListToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlListToolbar.Name = "pnlListToolbar";
            this.pnlListToolbar.Size = new System.Drawing.Size(900, 46);
            this.pnlListToolbar.TabIndex = 0;
            //
            // flpSearch
            //
            this.flpSearch.Controls.Add(this.lblListTitle);
            this.flpSearch.Controls.Add(this.txtSearch);
            this.flpSearch.Controls.Add(this.btnSearch);
            this.flpSearch.Controls.Add(this.chkShowInactive);
            this.flpSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpSearch.Name = "flpSearch";
            this.flpSearch.TabIndex = 0;
            this.flpSearch.WrapContents = false;
            //
            // lblListTitle
            //
            this.lblListTitle.Margin = new System.Windows.Forms.Padding(0, 4, 16, 0);
            this.lblListTitle.Name = "lblListTitle";
            this.lblListTitle.Size = new System.Drawing.Size(70, 34);
            this.lblListTitle.TabIndex = 0;
            this.lblListTitle.Text = "Items";
            this.lblListTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // txtSearch
            //
            this.txtSearch.Margin = new System.Windows.Forms.Padding(0, 9, 8, 0);
            this.txtSearch.MaxLength = 200;
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.PlaceholderText = "Search by code, name or category";
            this.txtSearch.Size = new System.Drawing.Size(280, 25);
            this.txtSearch.TabIndex = 1;
            //
            // btnSearch
            //
            this.btnSearch.Margin = new System.Windows.Forms.Padding(0, 5, 16, 0);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(90, 32);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "Search";
            //
            // chkShowInactive
            //
            this.chkShowInactive.AutoSize = true;
            this.chkShowInactive.Checked = true;
            this.chkShowInactive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowInactive.Margin = new System.Windows.Forms.Padding(0, 12, 0, 0);
            this.chkShowInactive.Name = "chkShowInactive";
            this.chkShowInactive.TabIndex = 3;
            this.chkShowInactive.Text = "Show inactive items";
            this.chkShowInactive.UseVisualStyleBackColor = true;
            //
            // lblCount
            //
            this.lblCount.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(220, 46);
            this.lblCount.TabIndex = 1;
            this.lblCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // errorProvider
            //
            this.errorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider.ContainerControl = this;
            //
            // ItemMasterForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 640);
            this.Controls.Add(this.tlpMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ItemMasterForm";
            this.Text = "Item Master";
            this.tlpMain.ResumeLayout(false);
            this.pnlEditor.ResumeLayout(false);
            this.tlpEditor.ResumeLayout(false);
            this.tlpEditor.PerformLayout();
            this.flpButtons.ResumeLayout(false);
            this.pnlEditorHeader.ResumeLayout(false);
            this.pnlList.ResumeLayout(false);
            this.pnlListToolbar.ResumeLayout(false);
            this.flpSearch.ResumeLayout(false);
            this.flpSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.Panel pnlEditor;
        private System.Windows.Forms.TableLayoutPanel tlpEditor;
        private System.Windows.Forms.Label lblItemCode;
        private System.Windows.Forms.TextBox txtItemCode;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.ComboBox cboCategory;
        private System.Windows.Forms.Label lblItemName;
        private System.Windows.Forms.TextBox txtItemName;
        private System.Windows.Forms.Label lblUnit;
        private System.Windows.Forms.ComboBox cboUnit;
        private System.Windows.Forms.Label lblMinimumStock;
        private System.Windows.Forms.TextBox txtMinimumStock;
        private System.Windows.Forms.Label lblOpeningStock;
        private System.Windows.Forms.TextBox txtOpeningStock;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.CheckBox chkIsActive;
        private System.Windows.Forms.FlowLayoutPanel flpButtons;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnToggleActive;
        private System.Windows.Forms.Panel pnlEditorHeader;
        private System.Windows.Forms.Label lblEditorTitle;
        private System.Windows.Forms.Label lblMode;
        private System.Windows.Forms.Panel pnlList;
        private System.Windows.Forms.DataGridView dgvItems;
        private System.Windows.Forms.Panel pnlListToolbar;
        private System.Windows.Forms.FlowLayoutPanel flpSearch;
        private System.Windows.Forms.Label lblListTitle;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.CheckBox chkShowInactive;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.ToolTip toolTip;
    }
}
