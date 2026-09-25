namespace InventoryManagement.Controls
{
    partial class StockEntryControl
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

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.tlpRoot = new System.Windows.Forms.TableLayoutPanel();
            this.pnlEntry = new System.Windows.Forms.Panel();
            this.tlpEntry = new System.Windows.Forms.TableLayoutPanel();
            this.lblEntryTitle = new System.Windows.Forms.Label();
            this.lblItem = new System.Windows.Forms.Label();
            this.cboItem = new System.Windows.Forms.ComboBox();
            this.lblItemInfoCaption = new System.Windows.Forms.Label();
            this.lblItemInfo = new System.Windows.Forms.Label();
            this.lblQuantity = new System.Windows.Forms.Label();
            this.flpQuantity = new System.Windows.Forms.FlowLayoutPanel();
            this.txtQuantity = new System.Windows.Forms.TextBox();
            this.lblUnit = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.lblReferenceNo = new System.Windows.Forms.Label();
            this.txtReferenceNo = new System.Windows.Forms.TextBox();
            this.lblReason = new System.Windows.Forms.Label();
            this.cboReason = new System.Windows.Forms.ComboBox();
            this.lblRemarks = new System.Windows.Forms.Label();
            this.txtRemarks = new System.Windows.Forms.TextBox();
            this.flpButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.pnlCalculation = new System.Windows.Forms.Panel();
            this.tlpCalculation = new System.Windows.Forms.TableLayoutPanel();
            this.lblCalculationTitle = new System.Windows.Forms.Label();
            this.lblCalculationItem = new System.Windows.Forms.Label();
            this.lblCurrentCaption = new System.Windows.Forms.Label();
            this.lblCurrentValue = new System.Windows.Forms.Label();
            this.lblQuantityCaption = new System.Windows.Forms.Label();
            this.lblQuantityValue = new System.Windows.Forms.Label();
            this.pnlDivider = new System.Windows.Forms.Panel();
            this.lblNewCaption = new System.Windows.Forms.Label();
            this.lblNewValue = new System.Windows.Forms.Label();
            this.lblWarning = new System.Windows.Forms.Label();
            this.pnlRecent = new System.Windows.Forms.Panel();
            this.dgvRecent = new System.Windows.Forms.DataGridView();
            this.lblRecentTitle = new System.Windows.Forms.Label();
            this.tlpRoot.SuspendLayout();
            this.pnlEntry.SuspendLayout();
            this.tlpEntry.SuspendLayout();
            this.flpQuantity.SuspendLayout();
            this.flpButtons.SuspendLayout();
            this.pnlCalculation.SuspendLayout();
            this.tlpCalculation.SuspendLayout();
            this.pnlRecent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecent)).BeginInit();
            this.SuspendLayout();
            //
            // tlpRoot
            //
            this.tlpRoot.ColumnCount = 2;
            this.tlpRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 58F));
            this.tlpRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 42F));
            this.tlpRoot.Controls.Add(this.pnlEntry, 0, 0);
            this.tlpRoot.Controls.Add(this.pnlCalculation, 1, 0);
            this.tlpRoot.Controls.Add(this.pnlRecent, 0, 1);
            this.tlpRoot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpRoot.Location = new System.Drawing.Point(0, 0);
            this.tlpRoot.Name = "tlpRoot";
            this.tlpRoot.RowCount = 2;
            this.tlpRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 404F));
            this.tlpRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpRoot.Size = new System.Drawing.Size(980, 640);
            this.tlpRoot.TabIndex = 0;
            this.tlpRoot.SetColumnSpan(this.pnlRecent, 2);
            //
            // pnlEntry
            //
            this.pnlEntry.BackColor = System.Drawing.Color.White;
            this.pnlEntry.Controls.Add(this.tlpEntry);
            this.pnlEntry.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlEntry.Margin = new System.Windows.Forms.Padding(0, 0, 8, 8);
            this.pnlEntry.Name = "pnlEntry";
            this.pnlEntry.Padding = new System.Windows.Forms.Padding(20, 12, 20, 8);
            this.pnlEntry.TabIndex = 0;
            //
            // tlpEntry
            //
            this.tlpEntry.ColumnCount = 2;
            this.tlpEntry.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tlpEntry.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpEntry.Controls.Add(this.lblEntryTitle, 0, 0);
            this.tlpEntry.Controls.Add(this.lblItem, 0, 1);
            this.tlpEntry.Controls.Add(this.cboItem, 1, 1);
            this.tlpEntry.Controls.Add(this.lblItemInfoCaption, 0, 2);
            this.tlpEntry.Controls.Add(this.lblItemInfo, 1, 2);
            this.tlpEntry.Controls.Add(this.lblQuantity, 0, 3);
            this.tlpEntry.Controls.Add(this.flpQuantity, 1, 3);
            this.tlpEntry.Controls.Add(this.lblDate, 0, 4);
            this.tlpEntry.Controls.Add(this.dtpDate, 1, 4);
            this.tlpEntry.Controls.Add(this.lblReferenceNo, 0, 5);
            this.tlpEntry.Controls.Add(this.txtReferenceNo, 1, 5);
            this.tlpEntry.Controls.Add(this.lblReason, 0, 6);
            this.tlpEntry.Controls.Add(this.cboReason, 1, 6);
            this.tlpEntry.Controls.Add(this.lblRemarks, 0, 7);
            this.tlpEntry.Controls.Add(this.txtRemarks, 1, 7);
            this.tlpEntry.Controls.Add(this.flpButtons, 1, 8);
            this.tlpEntry.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpEntry.Name = "tlpEntry";
            this.tlpEntry.RowCount = 10;
            this.tlpEntry.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tlpEntry.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tlpEntry.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpEntry.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tlpEntry.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tlpEntry.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tlpEntry.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tlpEntry.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 62F));
            this.tlpEntry.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpEntry.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpEntry.TabIndex = 0;
            this.tlpEntry.SetColumnSpan(this.lblEntryTitle, 2);
            //
            // lblEntryTitle
            //
            this.lblEntryTitle.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblEntryTitle.AutoSize = true;
            this.lblEntryTitle.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.lblEntryTitle.Name = "lblEntryTitle";
            this.lblEntryTitle.TabIndex = 0;
            this.lblEntryTitle.Text = "Stock Entry";
            //
            // lblItem
            //
            this.lblItem.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblItem.AutoSize = true;
            this.lblItem.Name = "lblItem";
            this.lblItem.TabIndex = 1;
            this.lblItem.Text = "Item *";
            //
            // cboItem
            //
            this.cboItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cboItem.FormattingEnabled = true;
            this.cboItem.MaxDropDownItems = 15;
            this.cboItem.Name = "cboItem";
            this.cboItem.TabIndex = 2;
            //
            // lblItemInfoCaption
            //
            this.lblItemInfoCaption.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblItemInfoCaption.AutoSize = true;
            this.lblItemInfoCaption.Name = "lblItemInfoCaption";
            this.lblItemInfoCaption.TabIndex = 3;
            this.lblItemInfoCaption.Text = "Item details";
            //
            // lblItemInfo
            //
            this.lblItemInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblItemInfo.AutoEllipsis = true;
            this.lblItemInfo.Name = "lblItemInfo";
            this.lblItemInfo.Size = new System.Drawing.Size(300, 20);
            this.lblItemInfo.TabIndex = 4;
            this.lblItemInfo.Text = "Select an item";
            this.lblItemInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblQuantity
            //
            this.lblQuantity.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblQuantity.AutoSize = true;
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.TabIndex = 5;
            this.lblQuantity.Text = "Quantity *";
            //
            // flpQuantity
            //
            this.flpQuantity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.flpQuantity.Controls.Add(this.txtQuantity);
            this.flpQuantity.Controls.Add(this.lblUnit);
            this.flpQuantity.Margin = new System.Windows.Forms.Padding(0);
            this.flpQuantity.Name = "flpQuantity";
            this.flpQuantity.Size = new System.Drawing.Size(300, 32);
            this.flpQuantity.TabIndex = 6;
            this.flpQuantity.WrapContents = false;
            //
            // txtQuantity
            //
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(160, 25);
            this.txtQuantity.TabIndex = 0;
            this.txtQuantity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            //
            // lblUnit
            //
            this.lblUnit.AutoSize = true;
            this.lblUnit.Margin = new System.Windows.Forms.Padding(6, 7, 3, 0);
            this.lblUnit.Name = "lblUnit";
            this.lblUnit.TabIndex = 1;
            //
            // lblDate
            //
            this.lblDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblDate.AutoSize = true;
            this.lblDate.Name = "lblDate";
            this.lblDate.TabIndex = 7;
            this.lblDate.Text = "Date *";
            //
            // dtpDate
            //
            this.dtpDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.CustomFormat = "dd-MMM-yyyy";
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(160, 25);
            this.dtpDate.TabIndex = 8;
            //
            // lblReferenceNo
            //
            this.lblReferenceNo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblReferenceNo.AutoSize = true;
            this.lblReferenceNo.Name = "lblReferenceNo";
            this.lblReferenceNo.TabIndex = 9;
            this.lblReferenceNo.Text = "Reference No";
            //
            // txtReferenceNo
            //
            this.txtReferenceNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtReferenceNo.MaxLength = 100;
            this.txtReferenceNo.Name = "txtReferenceNo";
            this.txtReferenceNo.PlaceholderText = "e.g. GRN-1001, PO-2045, ISS-301";
            this.txtReferenceNo.TabIndex = 10;
            //
            // lblReason
            //
            this.lblReason.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblReason.AutoSize = true;
            this.lblReason.Name = "lblReason";
            this.lblReason.TabIndex = 11;
            this.lblReason.Text = "Reason";
            //
            // cboReason
            //
            this.cboReason.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cboReason.FormattingEnabled = true;
            this.cboReason.MaxLength = 200;
            this.cboReason.Name = "cboReason";
            this.cboReason.TabIndex = 12;
            //
            // lblRemarks
            //
            this.lblRemarks.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.lblRemarks.AutoSize = true;
            this.lblRemarks.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
            this.lblRemarks.Name = "lblRemarks";
            this.lblRemarks.TabIndex = 13;
            this.lblRemarks.Text = "Remarks";
            //
            // txtRemarks
            //
            this.txtRemarks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRemarks.MaxLength = 500;
            this.txtRemarks.Multiline = true;
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtRemarks.TabIndex = 14;
            //
            // flpButtons
            //
            this.flpButtons.Controls.Add(this.btnSave);
            this.flpButtons.Controls.Add(this.btnClear);
            this.flpButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpButtons.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.flpButtons.Name = "flpButtons";
            this.flpButtons.TabIndex = 15;
            this.flpButtons.WrapContents = false;
            //
            // btnSave
            //
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(150, 36);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save";
            //
            // btnClear
            //
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(100, 36);
            this.btnClear.TabIndex = 1;
            this.btnClear.Text = "Clear";
            //
            // pnlCalculation
            //
            this.pnlCalculation.BackColor = System.Drawing.Color.White;
            this.pnlCalculation.Controls.Add(this.tlpCalculation);
            this.pnlCalculation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCalculation.Margin = new System.Windows.Forms.Padding(8, 0, 0, 8);
            this.pnlCalculation.Name = "pnlCalculation";
            this.pnlCalculation.Padding = new System.Windows.Forms.Padding(20, 12, 20, 12);
            this.pnlCalculation.TabIndex = 1;
            //
            // tlpCalculation
            //
            this.tlpCalculation.ColumnCount = 2;
            this.tlpCalculation.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpCalculation.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpCalculation.Controls.Add(this.lblCalculationTitle, 0, 0);
            this.tlpCalculation.Controls.Add(this.lblCalculationItem, 0, 1);
            this.tlpCalculation.Controls.Add(this.lblCurrentCaption, 0, 2);
            this.tlpCalculation.Controls.Add(this.lblCurrentValue, 1, 2);
            this.tlpCalculation.Controls.Add(this.lblQuantityCaption, 0, 3);
            this.tlpCalculation.Controls.Add(this.lblQuantityValue, 1, 3);
            this.tlpCalculation.Controls.Add(this.pnlDivider, 0, 4);
            this.tlpCalculation.Controls.Add(this.lblNewCaption, 0, 5);
            this.tlpCalculation.Controls.Add(this.lblNewValue, 1, 5);
            this.tlpCalculation.Controls.Add(this.lblWarning, 0, 6);
            this.tlpCalculation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpCalculation.Name = "tlpCalculation";
            this.tlpCalculation.RowCount = 7;
            this.tlpCalculation.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tlpCalculation.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tlpCalculation.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpCalculation.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpCalculation.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 14F));
            this.tlpCalculation.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpCalculation.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpCalculation.TabIndex = 0;
            this.tlpCalculation.SetColumnSpan(this.lblCalculationTitle, 2);
            this.tlpCalculation.SetColumnSpan(this.lblCalculationItem, 2);
            this.tlpCalculation.SetColumnSpan(this.pnlDivider, 2);
            this.tlpCalculation.SetColumnSpan(this.lblWarning, 2);
            //
            // lblCalculationTitle
            //
            this.lblCalculationTitle.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblCalculationTitle.AutoSize = true;
            this.lblCalculationTitle.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.lblCalculationTitle.Name = "lblCalculationTitle";
            this.lblCalculationTitle.TabIndex = 0;
            this.lblCalculationTitle.Text = "Stock Calculation";
            //
            // lblCalculationItem
            //
            this.lblCalculationItem.AutoEllipsis = true;
            this.lblCalculationItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCalculationItem.Name = "lblCalculationItem";
            this.lblCalculationItem.TabIndex = 1;
            this.lblCalculationItem.Text = "No item selected";
            this.lblCalculationItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblCurrentCaption
            //
            this.lblCurrentCaption.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblCurrentCaption.AutoSize = true;
            this.lblCurrentCaption.Name = "lblCurrentCaption";
            this.lblCurrentCaption.TabIndex = 2;
            this.lblCurrentCaption.Text = "Current Stock";
            //
            // lblCurrentValue
            //
            this.lblCurrentValue.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblCurrentValue.AutoSize = true;
            this.lblCurrentValue.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblCurrentValue.Name = "lblCurrentValue";
            this.lblCurrentValue.TabIndex = 3;
            this.lblCurrentValue.Text = "—";
            //
            // lblQuantityCaption
            //
            this.lblQuantityCaption.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblQuantityCaption.AutoSize = true;
            this.lblQuantityCaption.Name = "lblQuantityCaption";
            this.lblQuantityCaption.TabIndex = 4;
            this.lblQuantityCaption.Text = "Quantity";
            //
            // lblQuantityValue
            //
            this.lblQuantityValue.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblQuantityValue.AutoSize = true;
            this.lblQuantityValue.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblQuantityValue.Name = "lblQuantityValue";
            this.lblQuantityValue.TabIndex = 5;
            this.lblQuantityValue.Text = "—";
            //
            // pnlDivider
            //
            this.pnlDivider.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlDivider.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.pnlDivider.Name = "pnlDivider";
            this.pnlDivider.Size = new System.Drawing.Size(300, 2);
            this.pnlDivider.TabIndex = 6;
            //
            // lblNewCaption
            //
            this.lblNewCaption.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblNewCaption.AutoSize = true;
            this.lblNewCaption.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblNewCaption.Name = "lblNewCaption";
            this.lblNewCaption.TabIndex = 7;
            this.lblNewCaption.Text = "New Stock";
            //
            // lblNewValue
            //
            this.lblNewValue.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblNewValue.AutoSize = true;
            this.lblNewValue.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblNewValue.Name = "lblNewValue";
            this.lblNewValue.TabIndex = 8;
            this.lblNewValue.Text = "—";
            //
            // lblWarning
            //
            this.lblWarning.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblWarning.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
            this.lblWarning.Name = "lblWarning";
            this.lblWarning.Padding = new System.Windows.Forms.Padding(8);
            this.lblWarning.TabIndex = 9;
            this.lblWarning.Visible = false;
            //
            // pnlRecent
            //
            this.pnlRecent.BackColor = System.Drawing.Color.White;
            this.pnlRecent.Controls.Add(this.dgvRecent);
            this.pnlRecent.Controls.Add(this.lblRecentTitle);
            this.pnlRecent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRecent.Margin = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.pnlRecent.Name = "pnlRecent";
            this.pnlRecent.Padding = new System.Windows.Forms.Padding(16, 8, 16, 12);
            this.pnlRecent.TabIndex = 2;
            //
            // dgvRecent
            //
            this.dgvRecent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRecent.Name = "dgvRecent";
            this.dgvRecent.TabIndex = 1;
            this.dgvRecent.TabStop = false;
            //
            // lblRecentTitle
            //
            this.lblRecentTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblRecentTitle.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.lblRecentTitle.Name = "lblRecentTitle";
            this.lblRecentTitle.Size = new System.Drawing.Size(300, 34);
            this.lblRecentTitle.TabIndex = 0;
            this.lblRecentTitle.Text = "Recent entries";
            this.lblRecentTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // StockEntryControl
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlpRoot);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.Name = "StockEntryControl";
            this.Size = new System.Drawing.Size(980, 640);
            this.tlpRoot.ResumeLayout(false);
            this.pnlEntry.ResumeLayout(false);
            this.tlpEntry.ResumeLayout(false);
            this.tlpEntry.PerformLayout();
            this.flpQuantity.ResumeLayout(false);
            this.flpQuantity.PerformLayout();
            this.flpButtons.ResumeLayout(false);
            this.pnlCalculation.ResumeLayout(false);
            this.tlpCalculation.ResumeLayout(false);
            this.tlpCalculation.PerformLayout();
            this.pnlRecent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecent)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpRoot;
        private System.Windows.Forms.Panel pnlEntry;
        private System.Windows.Forms.TableLayoutPanel tlpEntry;
        private System.Windows.Forms.Label lblEntryTitle;
        private System.Windows.Forms.Label lblItem;
        private System.Windows.Forms.ComboBox cboItem;
        private System.Windows.Forms.Label lblItemInfoCaption;
        private System.Windows.Forms.Label lblItemInfo;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.FlowLayoutPanel flpQuantity;
        private System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.Label lblUnit;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label lblReferenceNo;
        private System.Windows.Forms.TextBox txtReferenceNo;
        private System.Windows.Forms.Label lblReason;
        private System.Windows.Forms.ComboBox cboReason;
        private System.Windows.Forms.Label lblRemarks;
        private System.Windows.Forms.TextBox txtRemarks;
        private System.Windows.Forms.FlowLayoutPanel flpButtons;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Panel pnlCalculation;
        private System.Windows.Forms.TableLayoutPanel tlpCalculation;
        private System.Windows.Forms.Label lblCalculationTitle;
        private System.Windows.Forms.Label lblCalculationItem;
        private System.Windows.Forms.Label lblCurrentCaption;
        private System.Windows.Forms.Label lblCurrentValue;
        private System.Windows.Forms.Label lblQuantityCaption;
        private System.Windows.Forms.Label lblQuantityValue;
        private System.Windows.Forms.Panel pnlDivider;
        private System.Windows.Forms.Label lblNewCaption;
        private System.Windows.Forms.Label lblNewValue;
        private System.Windows.Forms.Label lblWarning;
        private System.Windows.Forms.Panel pnlRecent;
        private System.Windows.Forms.DataGridView dgvRecent;
        private System.Windows.Forms.Label lblRecentTitle;
    }
}
