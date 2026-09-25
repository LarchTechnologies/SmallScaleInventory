namespace InventoryManagement.Forms
{
    partial class StockOutForm
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
            this.stockEntry = new InventoryManagement.Controls.StockEntryControl();
            this.SuspendLayout();
            //
            // stockEntry
            //
            this.stockEntry.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stockEntry.Location = new System.Drawing.Point(0, 0);
            this.stockEntry.Name = "stockEntry";
            this.stockEntry.Size = new System.Drawing.Size(1000, 640);
            this.stockEntry.TabIndex = 0;
            //
            // StockOutForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 640);
            this.Controls.Add(this.stockEntry);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "StockOutForm";
            this.Text = "Stock OUT";
            this.ResumeLayout(false);
        }

        #endregion

        private InventoryManagement.Controls.StockEntryControl stockEntry;
    }
}
