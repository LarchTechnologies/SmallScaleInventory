namespace InventoryManagement.Forms
{
    partial class MainForm
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
            this.pnlSidebar = new System.Windows.Forms.Panel();
            this.flpNav = new System.Windows.Forms.FlowLayoutPanel();
            this.lblSectionDashboard = new System.Windows.Forms.Label();
            this.btnNavDashboard = new InventoryManagement.Controls.NavButton();
            this.lblSectionMaster = new System.Windows.Forms.Label();
            this.btnNavItemMaster = new InventoryManagement.Controls.NavButton();
            this.lblSectionStock = new System.Windows.Forms.Label();
            this.btnNavStockIn = new InventoryManagement.Controls.NavButton();
            this.btnNavStockOut = new InventoryManagement.Controls.NavButton();
            this.lblSectionReports = new System.Windows.Forms.Label();
            this.btnNavCurrentStock = new InventoryManagement.Controls.NavButton();
            this.btnNavStockSummary = new InventoryManagement.Controls.NavButton();
            this.btnNavStockMovement = new InventoryManagement.Controls.NavButton();
            this.lblSectionSettings = new System.Windows.Forms.Label();
            this.btnNavDatabaseInfo = new InventoryManagement.Controls.NavButton();
            this.btnNavAbout = new InventoryManagement.Controls.NavButton();
            this.lblVersion = new System.Windows.Forms.Label();
            this.pnlBrand = new System.Windows.Forms.Panel();
            this.lblBrandSubtitle = new System.Windows.Forms.Label();
            this.lblBrandTitle = new System.Windows.Forms.Label();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblPageSubtitle = new System.Windows.Forms.Label();
            this.lblPageTitle = new System.Windows.Forms.Label();
            this.lblToday = new System.Windows.Forms.Label();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.lblStatusDatabase = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblStatusSpring = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblStatusMessage = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.pnlSidebar.SuspendLayout();
            this.flpNav.SuspendLayout();
            this.pnlBrand.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            //
            // pnlSidebar
            //
            this.pnlSidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.pnlSidebar.Controls.Add(this.flpNav);
            this.pnlSidebar.Controls.Add(this.lblVersion);
            this.pnlSidebar.Controls.Add(this.pnlBrand);
            this.pnlSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSidebar.Location = new System.Drawing.Point(0, 0);
            this.pnlSidebar.Name = "pnlSidebar";
            this.pnlSidebar.Size = new System.Drawing.Size(232, 738);
            this.pnlSidebar.TabIndex = 2;
            //
            // flpNav
            //
            this.flpNav.AutoScroll = true;
            this.flpNav.Controls.Add(this.lblSectionDashboard);
            this.flpNav.Controls.Add(this.btnNavDashboard);
            this.flpNav.Controls.Add(this.lblSectionMaster);
            this.flpNav.Controls.Add(this.btnNavItemMaster);
            this.flpNav.Controls.Add(this.lblSectionStock);
            this.flpNav.Controls.Add(this.btnNavStockIn);
            this.flpNav.Controls.Add(this.btnNavStockOut);
            this.flpNav.Controls.Add(this.lblSectionReports);
            this.flpNav.Controls.Add(this.btnNavCurrentStock);
            this.flpNav.Controls.Add(this.btnNavStockSummary);
            this.flpNav.Controls.Add(this.btnNavStockMovement);
            this.flpNav.Controls.Add(this.lblSectionSettings);
            this.flpNav.Controls.Add(this.btnNavDatabaseInfo);
            this.flpNav.Controls.Add(this.btnNavAbout);
            this.flpNav.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpNav.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpNav.Location = new System.Drawing.Point(0, 76);
            this.flpNav.Margin = new System.Windows.Forms.Padding(0);
            this.flpNav.Name = "flpNav";
            this.flpNav.Padding = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.flpNav.Size = new System.Drawing.Size(232, 632);
            this.flpNav.TabIndex = 1;
            this.flpNav.WrapContents = false;
            //
            // lblSectionDashboard
            //
            this.lblSectionDashboard.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblSectionDashboard.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblSectionDashboard.Margin = new System.Windows.Forms.Padding(0);
            this.lblSectionDashboard.Name = "lblSectionDashboard";
            this.lblSectionDashboard.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.lblSectionDashboard.Size = new System.Drawing.Size(232, 34);
            this.lblSectionDashboard.TabIndex = 0;
            this.lblSectionDashboard.Text = "DASHBOARD";
            this.lblSectionDashboard.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            //
            // btnNavDashboard
            //
            this.btnNavDashboard.Margin = new System.Windows.Forms.Padding(0);
            this.btnNavDashboard.Name = "btnNavDashboard";
            this.btnNavDashboard.Size = new System.Drawing.Size(232, 40);
            this.btnNavDashboard.TabIndex = 1;
            this.btnNavDashboard.Text = "Dashboard";
            this.toolTip.SetToolTip(this.btnNavDashboard, "Dashboard (Ctrl+D)");
            //
            // lblSectionMaster
            //
            this.lblSectionMaster.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblSectionMaster.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblSectionMaster.Margin = new System.Windows.Forms.Padding(0);
            this.lblSectionMaster.Name = "lblSectionMaster";
            this.lblSectionMaster.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.lblSectionMaster.Size = new System.Drawing.Size(232, 34);
            this.lblSectionMaster.TabIndex = 2;
            this.lblSectionMaster.Text = "MASTER";
            this.lblSectionMaster.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            //
            // btnNavItemMaster
            //
            this.btnNavItemMaster.Margin = new System.Windows.Forms.Padding(0);
            this.btnNavItemMaster.Name = "btnNavItemMaster";
            this.btnNavItemMaster.Size = new System.Drawing.Size(232, 40);
            this.btnNavItemMaster.TabIndex = 3;
            this.btnNavItemMaster.Text = "Item Master";
            this.toolTip.SetToolTip(this.btnNavItemMaster, "Item Master (F2)");
            //
            // lblSectionStock
            //
            this.lblSectionStock.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblSectionStock.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblSectionStock.Margin = new System.Windows.Forms.Padding(0);
            this.lblSectionStock.Name = "lblSectionStock";
            this.lblSectionStock.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.lblSectionStock.Size = new System.Drawing.Size(232, 34);
            this.lblSectionStock.TabIndex = 4;
            this.lblSectionStock.Text = "STOCK";
            this.lblSectionStock.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            //
            // btnNavStockIn
            //
            this.btnNavStockIn.Margin = new System.Windows.Forms.Padding(0);
            this.btnNavStockIn.Name = "btnNavStockIn";
            this.btnNavStockIn.Size = new System.Drawing.Size(232, 40);
            this.btnNavStockIn.TabIndex = 5;
            this.btnNavStockIn.Text = "Stock IN";
            this.toolTip.SetToolTip(this.btnNavStockIn, "Stock IN (F3)");
            //
            // btnNavStockOut
            //
            this.btnNavStockOut.Margin = new System.Windows.Forms.Padding(0);
            this.btnNavStockOut.Name = "btnNavStockOut";
            this.btnNavStockOut.Size = new System.Drawing.Size(232, 40);
            this.btnNavStockOut.TabIndex = 6;
            this.btnNavStockOut.Text = "Stock OUT";
            this.toolTip.SetToolTip(this.btnNavStockOut, "Stock OUT (F4)");
            //
            // lblSectionReports
            //
            this.lblSectionReports.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblSectionReports.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblSectionReports.Margin = new System.Windows.Forms.Padding(0);
            this.lblSectionReports.Name = "lblSectionReports";
            this.lblSectionReports.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.lblSectionReports.Size = new System.Drawing.Size(232, 34);
            this.lblSectionReports.TabIndex = 7;
            this.lblSectionReports.Text = "REPORTS";
            this.lblSectionReports.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            //
            // btnNavCurrentStock
            //
            this.btnNavCurrentStock.Margin = new System.Windows.Forms.Padding(0);
            this.btnNavCurrentStock.Name = "btnNavCurrentStock";
            this.btnNavCurrentStock.Size = new System.Drawing.Size(232, 40);
            this.btnNavCurrentStock.TabIndex = 8;
            this.btnNavCurrentStock.Text = "Current Stock";
            this.toolTip.SetToolTip(this.btnNavCurrentStock, "Current Stock (F6)");
            //
            // btnNavStockSummary
            //
            this.btnNavStockSummary.Margin = new System.Windows.Forms.Padding(0);
            this.btnNavStockSummary.Name = "btnNavStockSummary";
            this.btnNavStockSummary.Size = new System.Drawing.Size(232, 40);
            this.btnNavStockSummary.TabIndex = 9;
            this.btnNavStockSummary.Text = "Stock Summary";
            this.toolTip.SetToolTip(this.btnNavStockSummary, "Stock Summary (F7)");
            //
            // btnNavStockMovement
            //
            this.btnNavStockMovement.Margin = new System.Windows.Forms.Padding(0);
            this.btnNavStockMovement.Name = "btnNavStockMovement";
            this.btnNavStockMovement.Size = new System.Drawing.Size(232, 40);
            this.btnNavStockMovement.TabIndex = 10;
            this.btnNavStockMovement.Text = "Stock Movement";
            this.toolTip.SetToolTip(this.btnNavStockMovement, "Stock Movement (F8)");
            //
            // lblSectionSettings
            //
            this.lblSectionSettings.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblSectionSettings.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblSectionSettings.Margin = new System.Windows.Forms.Padding(0);
            this.lblSectionSettings.Name = "lblSectionSettings";
            this.lblSectionSettings.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.lblSectionSettings.Size = new System.Drawing.Size(232, 34);
            this.lblSectionSettings.TabIndex = 11;
            this.lblSectionSettings.Text = "SETTINGS";
            this.lblSectionSettings.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            //
            // btnNavDatabaseInfo
            //
            this.btnNavDatabaseInfo.Margin = new System.Windows.Forms.Padding(0);
            this.btnNavDatabaseInfo.Name = "btnNavDatabaseInfo";
            this.btnNavDatabaseInfo.Size = new System.Drawing.Size(232, 40);
            this.btnNavDatabaseInfo.TabIndex = 12;
            this.btnNavDatabaseInfo.Text = "Database Information";
            //
            // btnNavAbout
            //
            this.btnNavAbout.Margin = new System.Windows.Forms.Padding(0);
            this.btnNavAbout.Name = "btnNavAbout";
            this.btnNavAbout.Size = new System.Drawing.Size(232, 40);
            this.btnNavAbout.TabIndex = 13;
            this.btnNavAbout.Text = "About";
            //
            // lblVersion
            //
            this.lblVersion.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblVersion.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblVersion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblVersion.Location = new System.Drawing.Point(0, 708);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.lblVersion.Size = new System.Drawing.Size(232, 30);
            this.lblVersion.TabIndex = 2;
            this.lblVersion.Text = "Version 1.0.0";
            this.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // pnlBrand
            //
            this.pnlBrand.Controls.Add(this.lblBrandSubtitle);
            this.pnlBrand.Controls.Add(this.lblBrandTitle);
            this.pnlBrand.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBrand.Location = new System.Drawing.Point(0, 0);
            this.pnlBrand.Name = "pnlBrand";
            this.pnlBrand.Padding = new System.Windows.Forms.Padding(20, 16, 8, 8);
            this.pnlBrand.Size = new System.Drawing.Size(232, 76);
            this.pnlBrand.TabIndex = 0;
            //
            // lblBrandSubtitle
            //
            this.lblBrandSubtitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblBrandSubtitle.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.lblBrandSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblBrandSubtitle.Location = new System.Drawing.Point(20, 42);
            this.lblBrandSubtitle.Name = "lblBrandSubtitle";
            this.lblBrandSubtitle.Size = new System.Drawing.Size(204, 20);
            this.lblBrandSubtitle.TabIndex = 1;
            this.lblBrandSubtitle.Text = "Inventory Management System";
            //
            // lblBrandTitle
            //
            this.lblBrandTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblBrandTitle.Font = new System.Drawing.Font("Segoe UI", 12.75F, System.Drawing.FontStyle.Bold);
            this.lblBrandTitle.ForeColor = System.Drawing.Color.White;
            this.lblBrandTitle.Location = new System.Drawing.Point(20, 16);
            this.lblBrandTitle.Name = "lblBrandTitle";
            this.lblBrandTitle.Size = new System.Drawing.Size(204, 26);
            this.lblBrandTitle.TabIndex = 0;
            this.lblBrandTitle.Text = "Small Scale Inventory";
            //
            // pnlHeader
            //
            this.pnlHeader.BackColor = System.Drawing.Color.White;
            this.pnlHeader.Controls.Add(this.lblPageSubtitle);
            this.pnlHeader.Controls.Add(this.lblPageTitle);
            this.pnlHeader.Controls.Add(this.lblToday);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(232, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(24, 10, 24, 8);
            this.pnlHeader.Size = new System.Drawing.Size(1052, 70);
            this.pnlHeader.TabIndex = 1;
            //
            // lblPageSubtitle
            //
            this.lblPageSubtitle.AutoEllipsis = true;
            this.lblPageSubtitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblPageSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblPageSubtitle.Location = new System.Drawing.Point(24, 42);
            this.lblPageSubtitle.Name = "lblPageSubtitle";
            this.lblPageSubtitle.Size = new System.Drawing.Size(784, 20);
            this.lblPageSubtitle.TabIndex = 1;
            //
            // lblPageTitle
            //
            this.lblPageTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblPageTitle.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold);
            this.lblPageTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblPageTitle.Location = new System.Drawing.Point(24, 10);
            this.lblPageTitle.Name = "lblPageTitle";
            this.lblPageTitle.Size = new System.Drawing.Size(784, 32);
            this.lblPageTitle.TabIndex = 0;
            this.lblPageTitle.Text = "Dashboard";
            //
            // lblToday
            //
            this.lblToday.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblToday.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblToday.Location = new System.Drawing.Point(808, 10);
            this.lblToday.Name = "lblToday";
            this.lblToday.Size = new System.Drawing.Size(220, 52);
            this.lblToday.TabIndex = 2;
            this.lblToday.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // pnlContent
            //
            this.pnlContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(232, 70);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Padding = new System.Windows.Forms.Padding(20, 16, 20, 16);
            this.pnlContent.Size = new System.Drawing.Size(1052, 668);
            this.pnlContent.TabIndex = 0;
            //
            // statusStrip
            //
            this.statusStrip.BackColor = System.Drawing.Color.White;
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatusDatabase,
            this.lblStatusSpring,
            this.lblStatusMessage});
            this.statusStrip.Location = new System.Drawing.Point(0, 738);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1284, 24);
            this.statusStrip.TabIndex = 3;
            //
            // lblStatusDatabase
            //
            this.lblStatusDatabase.Name = "lblStatusDatabase";
            this.lblStatusDatabase.Text = "Database:";
            //
            // lblStatusSpring
            //
            this.lblStatusSpring.Name = "lblStatusSpring";
            this.lblStatusSpring.Spring = true;
            //
            // lblStatusMessage
            //
            this.lblStatusMessage.Name = "lblStatusMessage";
            this.lblStatusMessage.Text = "Ready";
            //
            // MainForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 762);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.pnlSidebar);
            this.Controls.Add(this.statusStrip);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(1200, 720);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Small Scale Inventory";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.pnlSidebar.ResumeLayout(false);
            this.flpNav.ResumeLayout(false);
            this.pnlBrand.ResumeLayout(false);
            this.pnlHeader.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel pnlSidebar;
        private System.Windows.Forms.FlowLayoutPanel flpNav;
        private System.Windows.Forms.Label lblSectionDashboard;
        private InventoryManagement.Controls.NavButton btnNavDashboard;
        private System.Windows.Forms.Label lblSectionMaster;
        private InventoryManagement.Controls.NavButton btnNavItemMaster;
        private System.Windows.Forms.Label lblSectionStock;
        private InventoryManagement.Controls.NavButton btnNavStockIn;
        private InventoryManagement.Controls.NavButton btnNavStockOut;
        private System.Windows.Forms.Label lblSectionReports;
        private InventoryManagement.Controls.NavButton btnNavCurrentStock;
        private InventoryManagement.Controls.NavButton btnNavStockSummary;
        private InventoryManagement.Controls.NavButton btnNavStockMovement;
        private System.Windows.Forms.Label lblSectionSettings;
        private InventoryManagement.Controls.NavButton btnNavDatabaseInfo;
        private InventoryManagement.Controls.NavButton btnNavAbout;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Panel pnlBrand;
        private System.Windows.Forms.Label lblBrandSubtitle;
        private System.Windows.Forms.Label lblBrandTitle;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblPageSubtitle;
        private System.Windows.Forms.Label lblPageTitle;
        private System.Windows.Forms.Label lblToday;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusDatabase;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusSpring;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusMessage;
        private System.Windows.Forms.ToolTip toolTip;
    }
}
