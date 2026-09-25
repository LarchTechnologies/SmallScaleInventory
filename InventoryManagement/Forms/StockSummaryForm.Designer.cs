namespace InventoryManagement.Forms
{
    partial class StockSummaryForm
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
            this.chkIncludeInactive = new System.Windows.Forms.CheckBox();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.pnlGrid = new System.Windows.Forms.Panel();
            this.dgvReport = new System.Windows.Forms.DataGridView();
            this.pnlFooter = new System.Windows.Forms.FlowLayoutPanel();
            this.lblRowCount = new System.Windows.Forms.Label();
            this.lblTotals = new System.Windows.Forms.Label();
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
            this.flpFilters.Controls.Add(this.chkIncludeInactive);
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
            // chkIncludeInactive
            //
            this.chkIncludeInactive.AutoSize = true;
            this.chkIncludeInactive.Margin = new System.Windows.Forms.Padding(0, 9, 16, 0);
            this.chkIncludeInactive.Text = "Include inactive items";
            this.chkIncludeInactive.UseVisualStyleBackColor = true;
            this.chkIncludeInactive.Name = "chkIncludeInactive";
            this.chkIncludeInactive.TabIndex = 8;
            //
            // btnGenerate
            //
            this.btnGenerate.Margin = new System.Windows.Forms.Padding(0, 3, 8, 0);
            this.btnGenerate.Size = new System.Drawing.Size(100, 32);
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.TabIndex = 9;
            //
            // btnPrint
            //
            this.btnPrint.Margin = new System.Windows.Forms.Padding(0, 3, 8, 0);
            this.btnPrint.Size = new System.Drawing.Size(100, 32);
            this.btnPrint.Text = "Print...";
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.TabIndex = 10;
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
            this.pnlFooter.Controls.Add(this.lblTotals);
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
            this.lblRowCount.Text = "0 items";
            //
            // lblTotals
            //
            this.lblTotals.AutoSize = true;
            this.lblTotals.Margin = new System.Windows.Forms.Padding(0, 0, 28, 0);
            this.lblTotals.Name = "lblTotals";
            this.lblTotals.TabIndex = 1;
            this.lblTotals.Text = "";
            //
            // StockSummaryForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 640);
            this.Controls.Add(this.tlpMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "StockSummaryForm";
            this.Text = "Stock Summary Report";
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
        private System.Windows.Forms.CheckBox chkIncludeInactive;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Panel pnlGrid;
        private System.Windows.Forms.DataGridView dgvReport;
        private System.Windows.Forms.FlowLayoutPanel pnlFooter;
        private System.Windows.Forms.Label lblRowCount;
        private System.Windows.Forms.Label lblTotals;
        private System.Windows.Forms.ToolTip toolTip;
    }
}
