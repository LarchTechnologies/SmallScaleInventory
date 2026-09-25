namespace InventoryManagement.Forms
{
    partial class DashboardForm
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
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.tlpCards = new System.Windows.Forms.TableLayoutPanel();
            this.cardTotalItems = new InventoryManagement.Controls.StatCard();
            this.cardActiveItems = new InventoryManagement.Controls.StatCard();
            this.cardTotalStock = new InventoryManagement.Controls.StatCard();
            this.cardLowStock = new InventoryManagement.Controls.StatCard();
            this.cardTodayIn = new InventoryManagement.Controls.StatCard();
            this.cardTodayOut = new InventoryManagement.Controls.StatCard();
            this.flpQuickActions = new System.Windows.Forms.FlowLayoutPanel();
            this.btnQuickStockIn = new System.Windows.Forms.Button();
            this.btnQuickStockOut = new System.Windows.Forms.Button();
            this.btnQuickItemMaster = new System.Windows.Forms.Button();
            this.btnQuickCurrentStock = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lblLastUpdated = new System.Windows.Forms.Label();
            this.pnlLowStock = new System.Windows.Forms.Panel();
            this.dgvLowStock = new System.Windows.Forms.DataGridView();
            this.lblNoLowStock = new System.Windows.Forms.Label();
            this.pnlLowStockHeader = new System.Windows.Forms.Panel();
            this.lblLowStockTitle = new System.Windows.Forms.Label();
            this.flpLegend = new System.Windows.Forms.FlowLayoutPanel();
            this.lblLegendNormal = new System.Windows.Forms.Label();
            this.lblLegendLow = new System.Windows.Forms.Label();
            this.lblLegendOut = new System.Windows.Forms.Label();
            this.tlpMain.SuspendLayout();
            this.tlpCards.SuspendLayout();
            this.flpQuickActions.SuspendLayout();
            this.pnlLowStock.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLowStock)).BeginInit();
            this.pnlLowStockHeader.SuspendLayout();
            this.flpLegend.SuspendLayout();
            this.SuspendLayout();
            //
            // tlpMain
            //
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.tlpCards, 0, 0);
            this.tlpMain.Controls.Add(this.flpQuickActions, 0, 1);
            this.tlpMain.Controls.Add(this.pnlLowStock, 0, 2);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 3;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 240F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 54F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Size = new System.Drawing.Size(1000, 640);
            this.tlpMain.TabIndex = 0;
            //
            // tlpCards
            //
            this.tlpCards.ColumnCount = 3;
            this.tlpCards.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tlpCards.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tlpCards.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.34F));
            this.tlpCards.Controls.Add(this.cardTotalItems, 0, 0);
            this.tlpCards.Controls.Add(this.cardActiveItems, 1, 0);
            this.tlpCards.Controls.Add(this.cardTotalStock, 2, 0);
            this.tlpCards.Controls.Add(this.cardLowStock, 0, 1);
            this.tlpCards.Controls.Add(this.cardTodayIn, 1, 1);
            this.tlpCards.Controls.Add(this.cardTodayOut, 2, 1);
            this.tlpCards.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpCards.Margin = new System.Windows.Forms.Padding(0);
            this.tlpCards.Name = "tlpCards";
            this.tlpCards.RowCount = 2;
            this.tlpCards.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpCards.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpCards.TabIndex = 0;
            //
            // cardTotalItems
            //
            this.cardTotalItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardTotalItems.Margin = new System.Windows.Forms.Padding(0, 0, 12, 12);
            this.cardTotalItems.Name = "cardTotalItems";
            this.cardTotalItems.TabIndex = 0;
            this.cardTotalItems.Title = "TOTAL ITEMS";
            //
            // cardActiveItems
            //
            this.cardActiveItems.AccentColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(58)))), ((int)(((byte)(237)))));
            this.cardActiveItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardActiveItems.Margin = new System.Windows.Forms.Padding(0, 0, 12, 12);
            this.cardActiveItems.Name = "cardActiveItems";
            this.cardActiveItems.TabIndex = 1;
            this.cardActiveItems.Title = "ACTIVE ITEMS";
            //
            // cardTotalStock
            //
            this.cardTotalStock.AccentColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(165)))), ((int)(((byte)(233)))));
            this.cardTotalStock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardTotalStock.Margin = new System.Windows.Forms.Padding(0, 0, 0, 12);
            this.cardTotalStock.Name = "cardTotalStock";
            this.cardTotalStock.TabIndex = 2;
            this.cardTotalStock.Title = "TOTAL STOCK QTY";
            //
            // cardLowStock
            //
            this.cardLowStock.AccentColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(119)))), ((int)(((byte)(6)))));
            this.cardLowStock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardLowStock.Margin = new System.Windows.Forms.Padding(0, 0, 12, 12);
            this.cardLowStock.Name = "cardLowStock";
            this.cardLowStock.TabIndex = 3;
            this.cardLowStock.Title = "LOW STOCK ITEMS";
            //
            // cardTodayIn
            //
            this.cardTodayIn.AccentColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.cardTodayIn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardTodayIn.Margin = new System.Windows.Forms.Padding(0, 0, 12, 12);
            this.cardTodayIn.Name = "cardTodayIn";
            this.cardTodayIn.TabIndex = 4;
            this.cardTodayIn.Title = "TODAY'S STOCK IN";
            //
            // cardTodayOut
            //
            this.cardTodayOut.AccentColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.cardTodayOut.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardTodayOut.Margin = new System.Windows.Forms.Padding(0, 0, 0, 12);
            this.cardTodayOut.Name = "cardTodayOut";
            this.cardTodayOut.TabIndex = 5;
            this.cardTodayOut.Title = "TODAY'S STOCK OUT";
            //
            // flpQuickActions
            //
            this.flpQuickActions.Controls.Add(this.btnQuickStockIn);
            this.flpQuickActions.Controls.Add(this.btnQuickStockOut);
            this.flpQuickActions.Controls.Add(this.btnQuickItemMaster);
            this.flpQuickActions.Controls.Add(this.btnQuickCurrentStock);
            this.flpQuickActions.Controls.Add(this.btnRefresh);
            this.flpQuickActions.Controls.Add(this.lblLastUpdated);
            this.flpQuickActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpQuickActions.Margin = new System.Windows.Forms.Padding(0);
            this.flpQuickActions.Name = "flpQuickActions";
            this.flpQuickActions.TabIndex = 1;
            this.flpQuickActions.WrapContents = false;
            //
            // btnQuickStockIn
            //
            this.btnQuickStockIn.Margin = new System.Windows.Forms.Padding(0, 4, 10, 0);
            this.btnQuickStockIn.Name = "btnQuickStockIn";
            this.btnQuickStockIn.Size = new System.Drawing.Size(130, 36);
            this.btnQuickStockIn.TabIndex = 0;
            this.btnQuickStockIn.Text = "+ Stock IN";
            //
            // btnQuickStockOut
            //
            this.btnQuickStockOut.Margin = new System.Windows.Forms.Padding(0, 4, 10, 0);
            this.btnQuickStockOut.Name = "btnQuickStockOut";
            this.btnQuickStockOut.Size = new System.Drawing.Size(130, 36);
            this.btnQuickStockOut.TabIndex = 1;
            this.btnQuickStockOut.Text = "− Stock OUT";
            //
            // btnQuickItemMaster
            //
            this.btnQuickItemMaster.Margin = new System.Windows.Forms.Padding(0, 4, 10, 0);
            this.btnQuickItemMaster.Name = "btnQuickItemMaster";
            this.btnQuickItemMaster.Size = new System.Drawing.Size(130, 36);
            this.btnQuickItemMaster.TabIndex = 2;
            this.btnQuickItemMaster.Text = "Item Master";
            //
            // btnQuickCurrentStock
            //
            this.btnQuickCurrentStock.Margin = new System.Windows.Forms.Padding(0, 4, 10, 0);
            this.btnQuickCurrentStock.Name = "btnQuickCurrentStock";
            this.btnQuickCurrentStock.Size = new System.Drawing.Size(150, 36);
            this.btnQuickCurrentStock.TabIndex = 3;
            this.btnQuickCurrentStock.Text = "Current Stock Report";
            //
            // btnRefresh
            //
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(0, 4, 10, 0);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(100, 36);
            this.btnRefresh.TabIndex = 4;
            this.btnRefresh.Text = "Refresh";
            //
            // lblLastUpdated
            //
            this.lblLastUpdated.AutoSize = true;
            this.lblLastUpdated.Margin = new System.Windows.Forms.Padding(6, 14, 0, 0);
            this.lblLastUpdated.Name = "lblLastUpdated";
            this.lblLastUpdated.TabIndex = 5;
            //
            // pnlLowStock
            //
            this.pnlLowStock.BackColor = System.Drawing.Color.White;
            this.pnlLowStock.Controls.Add(this.dgvLowStock);
            this.pnlLowStock.Controls.Add(this.lblNoLowStock);
            this.pnlLowStock.Controls.Add(this.pnlLowStockHeader);
            this.pnlLowStock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLowStock.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.pnlLowStock.Name = "pnlLowStock";
            this.pnlLowStock.Padding = new System.Windows.Forms.Padding(16, 8, 16, 12);
            this.pnlLowStock.TabIndex = 2;
            //
            // dgvLowStock
            //
            this.dgvLowStock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLowStock.Name = "dgvLowStock";
            this.dgvLowStock.TabIndex = 2;
            //
            // lblNoLowStock
            //
            this.lblNoLowStock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNoLowStock.Name = "lblNoLowStock";
            this.lblNoLowStock.TabIndex = 1;
            this.lblNoLowStock.Text = "All active items are above their minimum stock level.";
            this.lblNoLowStock.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblNoLowStock.Visible = false;
            //
            // pnlLowStockHeader
            //
            this.pnlLowStockHeader.Controls.Add(this.lblLowStockTitle);
            this.pnlLowStockHeader.Controls.Add(this.flpLegend);
            this.pnlLowStockHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlLowStockHeader.Name = "pnlLowStockHeader";
            this.pnlLowStockHeader.Size = new System.Drawing.Size(900, 40);
            this.pnlLowStockHeader.TabIndex = 0;
            //
            // lblLowStockTitle
            //
            this.lblLowStockTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLowStockTitle.Name = "lblLowStockTitle";
            this.lblLowStockTitle.TabIndex = 0;
            this.lblLowStockTitle.Text = "Low Stock Items (Current Stock <= Minimum Stock)";
            this.lblLowStockTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // flpLegend
            //
            this.flpLegend.Controls.Add(this.lblLegendNormal);
            this.flpLegend.Controls.Add(this.lblLegendLow);
            this.flpLegend.Controls.Add(this.lblLegendOut);
            this.flpLegend.Dock = System.Windows.Forms.DockStyle.Right;
            this.flpLegend.Name = "flpLegend";
            this.flpLegend.Size = new System.Drawing.Size(390, 40);
            this.flpLegend.TabIndex = 1;
            this.flpLegend.WrapContents = false;
            //
            // lblLegendNormal
            //
            this.lblLegendNormal.Margin = new System.Windows.Forms.Padding(0, 8, 8, 0);
            this.lblLegendNormal.Name = "lblLegendNormal";
            this.lblLegendNormal.Size = new System.Drawing.Size(100, 24);
            this.lblLegendNormal.TabIndex = 0;
            this.lblLegendNormal.Text = "NORMAL";
            this.lblLegendNormal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // lblLegendLow
            //
            this.lblLegendLow.Margin = new System.Windows.Forms.Padding(0, 8, 8, 0);
            this.lblLegendLow.Name = "lblLegendLow";
            this.lblLegendLow.Size = new System.Drawing.Size(110, 24);
            this.lblLegendLow.TabIndex = 1;
            this.lblLegendLow.Text = "LOW STOCK";
            this.lblLegendLow.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // lblLegendOut
            //
            this.lblLegendOut.Margin = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.lblLegendOut.Name = "lblLegendOut";
            this.lblLegendOut.Size = new System.Drawing.Size(130, 24);
            this.lblLegendOut.TabIndex = 2;
            this.lblLegendOut.Text = "OUT OF STOCK";
            this.lblLegendOut.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // DashboardForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 640);
            this.Controls.Add(this.tlpMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "DashboardForm";
            this.Text = "Dashboard";
            this.tlpMain.ResumeLayout(false);
            this.tlpCards.ResumeLayout(false);
            this.flpQuickActions.ResumeLayout(false);
            this.flpQuickActions.PerformLayout();
            this.pnlLowStock.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLowStock)).EndInit();
            this.pnlLowStockHeader.ResumeLayout(false);
            this.flpLegend.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.TableLayoutPanel tlpCards;
        private InventoryManagement.Controls.StatCard cardTotalItems;
        private InventoryManagement.Controls.StatCard cardActiveItems;
        private InventoryManagement.Controls.StatCard cardTotalStock;
        private InventoryManagement.Controls.StatCard cardLowStock;
        private InventoryManagement.Controls.StatCard cardTodayIn;
        private InventoryManagement.Controls.StatCard cardTodayOut;
        private System.Windows.Forms.FlowLayoutPanel flpQuickActions;
        private System.Windows.Forms.Button btnQuickStockIn;
        private System.Windows.Forms.Button btnQuickStockOut;
        private System.Windows.Forms.Button btnQuickItemMaster;
        private System.Windows.Forms.Button btnQuickCurrentStock;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label lblLastUpdated;
        private System.Windows.Forms.Panel pnlLowStock;
        private System.Windows.Forms.DataGridView dgvLowStock;
        private System.Windows.Forms.Label lblNoLowStock;
        private System.Windows.Forms.Panel pnlLowStockHeader;
        private System.Windows.Forms.Label lblLowStockTitle;
        private System.Windows.Forms.FlowLayoutPanel flpLegend;
        private System.Windows.Forms.Label lblLegendNormal;
        private System.Windows.Forms.Label lblLegendLow;
        private System.Windows.Forms.Label lblLegendOut;
    }
}
