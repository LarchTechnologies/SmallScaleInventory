namespace InventoryManagement.Forms
{
    partial class AboutForm
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
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblVersion = new System.Windows.Forms.Label();
            this.lblAppName = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblDetails = new System.Windows.Forms.Label();
            this.lblShortcutsTitle = new System.Windows.Forms.Label();
            this.lblShortcuts = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.SuspendLayout();
            //
            // pnlHeader
            //
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.pnlHeader.Controls.Add(this.lblVersion);
            this.pnlHeader.Controls.Add(this.lblAppName);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(24, 18, 24, 12);
            this.pnlHeader.Size = new System.Drawing.Size(540, 92);
            this.pnlHeader.TabIndex = 0;
            //
            // lblVersion
            //
            this.lblVersion.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblVersion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.lblVersion.Location = new System.Drawing.Point(24, 52);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(492, 24);
            this.lblVersion.TabIndex = 1;
            this.lblVersion.Text = "Version 1.0.0";
            //
            // lblAppName
            //
            this.lblAppName.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblAppName.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold);
            this.lblAppName.ForeColor = System.Drawing.Color.White;
            this.lblAppName.Location = new System.Drawing.Point(24, 18);
            this.lblAppName.Name = "lblAppName";
            this.lblAppName.Size = new System.Drawing.Size(492, 34);
            this.lblAppName.TabIndex = 0;
            this.lblAppName.Text = "Small Scale Inventory";
            //
            // lblDescription
            //
            this.lblDescription.Location = new System.Drawing.Point(24, 108);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(492, 44);
            this.lblDescription.TabIndex = 1;
            this.lblDescription.Text = "Inventory management for small businesses: item master, Stock IN, Stock OUT, dashboard and stock reports.";
            //
            // lblDetails
            //
            this.lblDetails.Location = new System.Drawing.Point(24, 156);
            this.lblDetails.Name = "lblDetails";
            this.lblDetails.Size = new System.Drawing.Size(492, 96);
            this.lblDetails.TabIndex = 2;
            //
            // lblShortcutsTitle
            //
            this.lblShortcutsTitle.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblShortcutsTitle.Location = new System.Drawing.Point(24, 256);
            this.lblShortcutsTitle.Name = "lblShortcutsTitle";
            this.lblShortcutsTitle.Size = new System.Drawing.Size(492, 22);
            this.lblShortcutsTitle.TabIndex = 3;
            this.lblShortcutsTitle.Text = "Keyboard shortcuts";
            //
            // lblShortcuts
            //
            this.lblShortcuts.Location = new System.Drawing.Point(24, 280);
            this.lblShortcuts.Name = "lblShortcuts";
            this.lblShortcuts.Size = new System.Drawing.Size(492, 100);
            this.lblShortcuts.TabIndex = 4;
            this.lblShortcuts.Text = "Ctrl+D  Dashboard          F6  Current Stock report\r\nF2      Item Master        F7  Stock Summary report\r\nF3      Stock IN           F8  Stock Movement report\r\nF4      Stock OUT          F5  Refresh current page\r\nCtrl+S  Save on entry screens";
            //
            // btnOk
            //
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(416, 392);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(100, 36);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "OK";
            //
            // AboutForm
            //
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnOk;
            this.ClientSize = new System.Drawing.Size(540, 446);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.lblShortcuts);
            this.Controls.Add(this.lblShortcutsTitle);
            this.Controls.Add(this.lblDetails);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About";
            this.pnlHeader.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label lblAppName;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblDetails;
        private System.Windows.Forms.Label lblShortcutsTitle;
        private System.Windows.Forms.Label lblShortcuts;
        private System.Windows.Forms.Button btnOk;
    }
}
