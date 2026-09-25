namespace InventoryManagement.Forms
{
    partial class CurrentStockForm
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
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblCategory = new System.Windows.Forms.Label();
            this.cboCategory = new System.Windows.Forms.ComboBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cboStatus = new System.Windows.Forms.ComboBox();
            this.chkIncludeInactive = new System.Windows.Forms.CheckBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.pnlGrid = new System.Windows.Forms.Panel();
            this.dgvReport = new System.Windows.Forms.DataGridView();
            this.pnlFooter = new System.Windows.Forms.FlowLayoutPanel();
            this.lblRowCount = new System.Windows.Forms.Label();
            this.lblLowCount = new System.Windows.Forms.Label();
            this.lblOutCount = new System.Windows.Forms.Label();
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
            this.flpFilters.Controls.Add(this.lblSearch);
            this.flpFilters.Controls.Add(this.txtSearch);
            this.flpFilters.Controls.Add(this.lblCategory);
            this.flpFilters.Controls.Add(this.cboCategory);
            this.flpFilters.Controls.Add(this.lblStatus);
            this.flpFilters.Controls.Add(this.cboStatus);
            this.flpFilters.Controls.Add(this.chkIncludeInactive);
            this.flpFilters.Controls.Add(this.btnRefresh);
            this.flpFilters.Controls.Add(this.btnPrint);
            this.flpFilters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpFilters.Name = "flpFilters";
            this.flpFilters.TabIndex = 0;
            this.flpFilters.SetFlowBreak(this.cboStatus, true);
            //
            // lblSearch
            //
            this.lblSearch.AutoSize = true;
            this.lblSearch.Margin = new System.Windows.Forms.Padding(0, 9, 6, 0);
            this.lblSearch.Text = "Search";
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.TabIndex = 0;
            //
            // txtSearch
            //
            this.txtSearch.Margin = new System.Windows.Forms.Padding(0, 5, 16, 4);
            this.txtSearch.MaxLength = 200;
            this.txtSearch.PlaceholderText = "Code, name or category";
            this.txtSearch.Size = new System.Drawing.Size(220, 25);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.TabIndex = 1;
            //
            // lblCategory
            //
            this.lblCategory.AutoSize = true;
            this.lblCategory.Margin = new System.Windows.Forms.Padding(0, 9, 6, 0);
            this.lblCategory.Text = "Category";
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.TabIndex = 2;
            //
            // cboCategory
            //
            this.cboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCategory.FormattingEnabled = true;
            this.cboCategory.Margin = new System.Windows.Forms.Padding(0, 5, 16, 4);
            this.cboCategory.Size = new System.Drawing.Size(160, 25);
            this.cboCategory.Name = "cboCategory";
            this.cboCategory.TabIndex = 3;
            //
            // lblStatus
            //
            this.lblStatus.AutoSize = true;
            this.lblStatus.Margin = new System.Windows.Forms.Padding(0, 9, 6, 0);
            this.lblStatus.Text = "Status";
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.TabIndex = 4;
            //
            // cboStatus
            //
            this.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStatus.FormattingEnabled = true;
            this.cboStatus.Margin = new System.Windows.Forms.Padding(0, 5, 16, 4);
            this.cboStatus.Size = new System.Drawing.Size(140, 25);
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.TabIndex = 5;
            //
            // chkIncludeInactive
            //
            this.chkIncludeInactive.AutoSize = true;
            this.chkIncludeInactive.Margin = new System.Windows.Forms.Padding(0, 9, 16, 0);
            this.chkIncludeInactive.Text = "Include inactive items";
            this.chkIncludeInactive.UseVisualStyleBackColor = true;
            this.chkIncludeInactive.Name = "chkIncludeInactive";
            this.chkIncludeInactive.TabIndex = 6;
            //
            // btnRefresh
            //
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(0, 3, 8, 0);
            this.btnRefresh.Size = new System.Drawing.Size(100, 32);
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.TabIndex = 7;
            //
            // btnPrint
            //
            this.btnPrint.Margin = new System.Windows.Forms.Padding(0, 3, 8, 0);
            this.btnPrint.Size = new System.Drawing.Size(100, 32);
            this.btnPrint.Text = "Print...";
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.TabIndex = 8;
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
            this.pnlFooter.Controls.Add(this.lblLowCount);
            this.pnlFooter.Controls.Add(this.lblOutCount);
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
            // lblLowCount
            //
            this.lblLowCount.AutoSize = true;
            this.lblLowCount.Margin = new System.Windows.Forms.Padding(0, 0, 28, 0);
            this.lblLowCount.Name = "lblLowCount";
            this.lblLowCount.TabIndex = 1;
            this.lblLowCount.Text = "";
            //
            // lblOutCount
            //
            this.lblOutCount.AutoSize = true;
            this.lblOutCount.Margin = new System.Windows.Forms.Padding(0, 0, 28, 0);
            this.lblOutCount.Name = "lblOutCount";
            this.lblOutCount.TabIndex = 2;
            this.lblOutCount.Text = "";
            //
            // CurrentStockForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 640);
            this.Controls.Add(this.tlpMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CurrentStockForm";
            this.Text = "Current Stock Report";
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
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.ComboBox cboCategory;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cboStatus;
        private System.Windows.Forms.CheckBox chkIncludeInactive;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Panel pnlGrid;
        private System.Windows.Forms.DataGridView dgvReport;
        private System.Windows.Forms.FlowLayoutPanel pnlFooter;
        private System.Windows.Forms.Label lblRowCount;
        private System.Windows.Forms.Label lblLowCount;
        private System.Windows.Forms.Label lblOutCount;
        private System.Windows.Forms.ToolTip toolTip;
    }
}
