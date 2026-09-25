namespace InventoryManagement.Forms
{
    partial class StockMovementForm
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
            this.pnlFilters = new System.Windows.Forms.Panel();
            this.flpFilters = new System.Windows.Forms.FlowLayoutPanel();
            this.lblPeriod = new System.Windows.Forms.Label();
            this.cboPeriod = new System.Windows.Forms.ComboBox();
            this.lblFrom = new System.Windows.Forms.Label();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.lblTo = new System.Windows.Forms.Label();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.lblItem = new System.Windows.Forms.Label();
            this.cboItem = new System.Windows.Forms.ComboBox();
            this.lblType = new System.Windows.Forms.Label();
            this.cboType = new System.Windows.Forms.ComboBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.pnlGrid = new System.Windows.Forms.Panel();
            this.dgvReport = new System.Windows.Forms.DataGridView();
            this.pnlFooter = new System.Windows.Forms.FlowLayoutPanel();
            this.lblRowCount = new System.Windows.Forms.Label();
            this.lblTotalIn = new System.Windows.Forms.Label();
            this.lblTotalOut = new System.Windows.Forms.Label();
            this.lblNet = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.tlpMain.SuspendLayout();
            this.pnlFilters.SuspendLayout();
            this.flpFilters.SuspendLayout();
            this.pnlGrid.SuspendLayout();
            this.pnlFooter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).BeginInit();
            this.SuspendLayout();
            //
            // tlpMain
            //
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.pnlFilters, 0, 0);
            this.tlpMain.Controls.Add(this.pnlGrid, 0, 1);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 2;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 98F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Size = new System.Drawing.Size(1000, 640);
            this.tlpMain.TabIndex = 0;
            //
            // pnlFilters
            //
            this.pnlFilters.BackColor = System.Drawing.Color.White;
            this.pnlFilters.Controls.Add(this.flpFilters);
            this.pnlFilters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFilters.Margin = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.pnlFilters.Name = "pnlFilters";
            this.pnlFilters.Padding = new System.Windows.Forms.Padding(16, 10, 16, 6);
            this.pnlFilters.TabIndex = 0;
            //
            // flpFilters
            //
            this.flpFilters.Controls.Add(this.lblPeriod);
            this.flpFilters.Controls.Add(this.cboPeriod);
            this.flpFilters.Controls.Add(this.lblFrom);
            this.flpFilters.Controls.Add(this.dtpFrom);
            this.flpFilters.Controls.Add(this.lblTo);
            this.flpFilters.Controls.Add(this.dtpTo);
            this.flpFilters.Controls.Add(this.lblItem);
            this.flpFilters.Controls.Add(this.cboItem);
            this.flpFilters.Controls.Add(this.lblType);
            this.flpFilters.Controls.Add(this.cboType);
            this.flpFilters.Controls.Add(this.lblSearch);
            this.flpFilters.Controls.Add(this.txtSearch);
            this.flpFilters.Controls.Add(this.btnGenerate);
            this.flpFilters.Controls.Add(this.btnPrint);
            this.flpFilters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpFilters.Name = "flpFilters";
            this.flpFilters.TabIndex = 0;
            this.flpFilters.SetFlowBreak(this.cboItem, true);
            //
            // lblPeriod
            //
            this.lblPeriod.AutoSize = true;
            this.lblPeriod.Margin = new System.Windows.Forms.Padding(0, 9, 6, 0);
            this.lblPeriod.Text = "Period";
            this.lblPeriod.Name = "lblPeriod";
            this.lblPeriod.TabIndex = 0;
            //
            // cboPeriod
            //
            this.cboPeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPeriod.FormattingEnabled = true;
            this.cboPeriod.Margin = new System.Windows.Forms.Padding(0, 5, 16, 4);
            this.cboPeriod.Size = new System.Drawing.Size(120, 25);
            this.cboPeriod.Name = "cboPeriod";
            this.cboPeriod.TabIndex = 1;
            //
            // lblFrom
            //
            this.lblFrom.AutoSize = true;
            this.lblFrom.Margin = new System.Windows.Forms.Padding(0, 9, 6, 0);
            this.lblFrom.Text = "From";
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.TabIndex = 2;
            //
            // dtpFrom
            //
            this.dtpFrom.CustomFormat = "dd-MMM-yyyy";
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFrom.Margin = new System.Windows.Forms.Padding(0, 5, 16, 4);
            this.dtpFrom.Size = new System.Drawing.Size(130, 25);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.TabIndex = 3;
            //
            // lblTo
            //
            this.lblTo.AutoSize = true;
            this.lblTo.Margin = new System.Windows.Forms.Padding(0, 9, 6, 0);
            this.lblTo.Text = "To";
            this.lblTo.Name = "lblTo";
            this.lblTo.TabIndex = 4;
            //
            // dtpTo
            //
            this.dtpTo.CustomFormat = "dd-MMM-yyyy";
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTo.Margin = new System.Windows.Forms.Padding(0, 5, 16, 4);
            this.dtpTo.Size = new System.Drawing.Size(130, 25);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.TabIndex = 5;
            //
            // lblItem
            //
            this.lblItem.AutoSize = true;
            this.lblItem.Margin = new System.Windows.Forms.Padding(0, 9, 6, 0);
            this.lblItem.Text = "Item";
            this.lblItem.Name = "lblItem";
            this.lblItem.TabIndex = 6;
            //
            // cboItem
            //
            this.cboItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.cboItem.FormattingEnabled = true;
            this.cboItem.Margin = new System.Windows.Forms.Padding(0, 5, 16, 4);
            this.cboItem.Size = new System.Drawing.Size(230, 25);
            this.cboItem.Name = "cboItem";
            this.cboItem.TabIndex = 7;
            //
            // lblType
            //
            this.lblType.AutoSize = true;
            this.lblType.Margin = new System.Windows.Forms.Padding(0, 9, 6, 0);
            this.lblType.Text = "Type";
            this.lblType.Name = "lblType";
            this.lblType.TabIndex = 8;
            //
            // cboType
            //
            this.cboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboType.FormattingEnabled = true;
            this.cboType.Margin = new System.Windows.Forms.Padding(0, 5, 16, 4);
            this.cboType.Size = new System.Drawing.Size(100, 25);
            this.cboType.Name = "cboType";
            this.cboType.TabIndex = 9;
            //
            // lblSearch
            //
            this.lblSearch.AutoSize = true;
            this.lblSearch.Margin = new System.Windows.Forms.Padding(0, 9, 6, 0);
            this.lblSearch.Text = "Search";
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.TabIndex = 10;
            //
            // txtSearch
            //
            this.txtSearch.Margin = new System.Windows.Forms.Padding(0, 5, 16, 4);
            this.txtSearch.MaxLength = 200;
            this.txtSearch.PlaceholderText = "Reference, reason or remarks";
            this.txtSearch.Size = new System.Drawing.Size(220, 25);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.TabIndex = 11;
            //
            // btnGenerate
            //
            this.btnGenerate.Margin = new System.Windows.Forms.Padding(0, 3, 8, 0);
            this.btnGenerate.Size = new System.Drawing.Size(100, 32);
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.TabIndex = 12;
            //
            // btnPrint
            //
            this.btnPrint.Margin = new System.Windows.Forms.Padding(0, 3, 8, 0);
            this.btnPrint.Size = new System.Drawing.Size(100, 32);
            this.btnPrint.Text = "Print...";
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.TabIndex = 13;
            //
            // pnlGrid
            //
            this.pnlGrid.BackColor = System.Drawing.Color.White;
            this.pnlGrid.Controls.Add(this.dgvReport);
            this.pnlGrid.Controls.Add(this.pnlFooter);
            this.pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGrid.Margin = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.pnlGrid.Name = "pnlGrid";
            this.pnlGrid.Padding = new System.Windows.Forms.Padding(16, 12, 16, 8);
            this.pnlGrid.TabIndex = 1;
            //
            // dgvReport
            //
            this.dgvReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvReport.Name = "dgvReport";
            this.dgvReport.TabIndex = 0;
            //
            // pnlFooter
            //
            this.pnlFooter.Controls.Add(this.lblRowCount);
            this.pnlFooter.Controls.Add(this.lblTotalIn);
            this.pnlFooter.Controls.Add(this.lblTotalOut);
            this.pnlFooter.Controls.Add(this.lblNet);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.pnlFooter.Size = new System.Drawing.Size(900, 40);
            this.pnlFooter.TabIndex = 1;
            this.pnlFooter.WrapContents = false;
            //
            // lblRowCount
            //
            this.lblRowCount.AutoSize = true;
            this.lblRowCount.Margin = new System.Windows.Forms.Padding(0, 0, 28, 0);
            this.lblRowCount.Name = "lblRowCount";
            this.lblRowCount.TabIndex = 0;
            this.lblRowCount.Text = "0 transactions";
            //
            // lblTotalIn
            //
            this.lblTotalIn.AutoSize = true;
            this.lblTotalIn.Margin = new System.Windows.Forms.Padding(0, 0, 28, 0);
            this.lblTotalIn.Name = "lblTotalIn";
            this.lblTotalIn.TabIndex = 1;
            this.lblTotalIn.Text = "Total IN: 0.00";
            //
            // lblTotalOut
            //
            this.lblTotalOut.AutoSize = true;
            this.lblTotalOut.Margin = new System.Windows.Forms.Padding(0, 0, 28, 0);
            this.lblTotalOut.Name = "lblTotalOut";
            this.lblTotalOut.TabIndex = 2;
            this.lblTotalOut.Text = "Total OUT: 0.00";
            //
            // lblNet
            //
            this.lblNet.AutoSize = true;
            this.lblNet.Margin = new System.Windows.Forms.Padding(0, 0, 28, 0);
            this.lblNet.Name = "lblNet";
            this.lblNet.TabIndex = 3;
            this.lblNet.Text = "Net: 0.00";
            //
            // StockMovementForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 640);
            this.Controls.Add(this.tlpMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "StockMovementForm";
            this.Text = "Stock Movement Report";
            this.tlpMain.ResumeLayout(false);
            this.pnlFilters.ResumeLayout(false);
            this.flpFilters.ResumeLayout(false);
            this.flpFilters.PerformLayout();
            this.pnlGrid.ResumeLayout(false);
            this.pnlFooter.ResumeLayout(false);
            this.pnlFooter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.Panel pnlFilters;
        private System.Windows.Forms.FlowLayoutPanel flpFilters;
        private System.Windows.Forms.Label lblPeriod;
        private System.Windows.Forms.ComboBox cboPeriod;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.Label lblItem;
        private System.Windows.Forms.ComboBox cboItem;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.ComboBox cboType;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Panel pnlGrid;
        private System.Windows.Forms.DataGridView dgvReport;
        private System.Windows.Forms.FlowLayoutPanel pnlFooter;
        private System.Windows.Forms.Label lblRowCount;
        private System.Windows.Forms.Label lblTotalIn;
        private System.Windows.Forms.Label lblTotalOut;
        private System.Windows.Forms.Label lblNet;
        private System.Windows.Forms.ToolTip toolTip;
    }
}
