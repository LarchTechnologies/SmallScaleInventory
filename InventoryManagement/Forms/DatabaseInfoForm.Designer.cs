namespace InventoryManagement.Forms
{
    partial class DatabaseInfoForm
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
            this.pnlConnection = new System.Windows.Forms.Panel();
            this.tlpInfo = new System.Windows.Forms.TableLayoutPanel();
            this.lblConnectionTitle = new System.Windows.Forms.Label();
            this.lblServerCaption = new System.Windows.Forms.Label();
            this.lblServer = new System.Windows.Forms.Label();
            this.lblDatabaseCaption = new System.Windows.Forms.Label();
            this.lblDatabase = new System.Windows.Forms.Label();
            this.lblAuthenticationCaption = new System.Windows.Forms.Label();
            this.lblAuthentication = new System.Windows.Forms.Label();
            this.lblConnectionNameCaption = new System.Windows.Forms.Label();
            this.lblConnectionName = new System.Windows.Forms.Label();
            this.lblConfigFileCaption = new System.Windows.Forms.Label();
            this.lblConfigFile = new System.Windows.Forms.Label();
            this.lblServerVersionCaption = new System.Windows.Forms.Label();
            this.lblServerVersion = new System.Windows.Forms.Label();
            this.lblServerEditionCaption = new System.Windows.Forms.Label();
            this.lblServerEdition = new System.Windows.Forms.Label();
            this.lblCreatedCaption = new System.Windows.Forms.Label();
            this.lblCreated = new System.Windows.Forms.Label();
            this.lblItemsCaption = new System.Windows.Forms.Label();
            this.lblItems = new System.Windows.Forms.Label();
            this.lblTransactionsCaption = new System.Windows.Forms.Label();
            this.lblTransactions = new System.Windows.Forms.Label();
            this.lblLastTransactionCaption = new System.Windows.Forms.Label();
            this.lblLastTransaction = new System.Windows.Forms.Label();
            this.lblLogFileCaption = new System.Windows.Forms.Label();
            this.lblLogFile = new System.Windows.Forms.Label();
            this.lblConnectionStatusCaption = new System.Windows.Forms.Label();
            this.lblConnectionStatus = new System.Windows.Forms.Label();
            this.flpActions = new System.Windows.Forms.FlowLayoutPanel();
            this.btnTestConnection = new System.Windows.Forms.Button();
            this.btnOpenLogFolder = new System.Windows.Forms.Button();
            this.btnOpenScriptsFolder = new System.Windows.Forms.Button();
            this.pnlHelp = new System.Windows.Forms.Panel();
            this.txtHelp = new System.Windows.Forms.TextBox();
            this.lblHelpTitle = new System.Windows.Forms.Label();
            this.tlpMain.SuspendLayout();
            this.pnlConnection.SuspendLayout();
            this.tlpInfo.SuspendLayout();
            this.flpActions.SuspendLayout();
            this.pnlHelp.SuspendLayout();
            this.SuspendLayout();
            //
            // tlpMain
            //
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.pnlConnection, 0, 0);
            this.tlpMain.Controls.Add(this.flpActions, 0, 1);
            this.tlpMain.Controls.Add(this.pnlHelp, 0, 2);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 3;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 440F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 54F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Size = new System.Drawing.Size(1000, 700);
            this.tlpMain.TabIndex = 0;
            //
            // pnlConnection
            //
            this.pnlConnection.BackColor = System.Drawing.Color.White;
            this.pnlConnection.Controls.Add(this.tlpInfo);
            this.pnlConnection.Controls.Add(this.lblConnectionTitle);
            this.pnlConnection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlConnection.Margin = new System.Windows.Forms.Padding(0);
            this.pnlConnection.Name = "pnlConnection";
            this.pnlConnection.Padding = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.pnlConnection.TabIndex = 0;
            //
            // tlpInfo
            //
            this.tlpInfo.ColumnCount = 2;
            this.tlpInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 180F));
            this.tlpInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpInfo.Controls.Add(this.lblServerCaption, 0, 0);
            this.tlpInfo.Controls.Add(this.lblServer, 1, 0);
            this.tlpInfo.Controls.Add(this.lblDatabaseCaption, 0, 1);
            this.tlpInfo.Controls.Add(this.lblDatabase, 1, 1);
            this.tlpInfo.Controls.Add(this.lblAuthenticationCaption, 0, 2);
            this.tlpInfo.Controls.Add(this.lblAuthentication, 1, 2);
            this.tlpInfo.Controls.Add(this.lblConnectionNameCaption, 0, 3);
            this.tlpInfo.Controls.Add(this.lblConnectionName, 1, 3);
            this.tlpInfo.Controls.Add(this.lblConfigFileCaption, 0, 4);
            this.tlpInfo.Controls.Add(this.lblConfigFile, 1, 4);
            this.tlpInfo.Controls.Add(this.lblServerVersionCaption, 0, 5);
            this.tlpInfo.Controls.Add(this.lblServerVersion, 1, 5);
            this.tlpInfo.Controls.Add(this.lblServerEditionCaption, 0, 6);
            this.tlpInfo.Controls.Add(this.lblServerEdition, 1, 6);
            this.tlpInfo.Controls.Add(this.lblCreatedCaption, 0, 7);
            this.tlpInfo.Controls.Add(this.lblCreated, 1, 7);
            this.tlpInfo.Controls.Add(this.lblItemsCaption, 0, 8);
            this.tlpInfo.Controls.Add(this.lblItems, 1, 8);
            this.tlpInfo.Controls.Add(this.lblTransactionsCaption, 0, 9);
            this.tlpInfo.Controls.Add(this.lblTransactions, 1, 9);
            this.tlpInfo.Controls.Add(this.lblLastTransactionCaption, 0, 10);
            this.tlpInfo.Controls.Add(this.lblLastTransaction, 1, 10);
            this.tlpInfo.Controls.Add(this.lblLogFileCaption, 0, 11);
            this.tlpInfo.Controls.Add(this.lblLogFile, 1, 11);
            this.tlpInfo.Controls.Add(this.lblConnectionStatusCaption, 0, 12);
            this.tlpInfo.Controls.Add(this.lblConnectionStatus, 1, 12);
            this.tlpInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpInfo.Name = "tlpInfo";
            this.tlpInfo.RowCount = 14;
            this.tlpInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tlpInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tlpInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tlpInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tlpInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tlpInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tlpInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tlpInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tlpInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tlpInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tlpInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tlpInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tlpInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tlpInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpInfo.TabIndex = 1;
            //
            // lblConnectionTitle
            //
            this.lblConnectionTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblConnectionTitle.Name = "lblConnectionTitle";
            this.lblConnectionTitle.Size = new System.Drawing.Size(900, 36);
            this.lblConnectionTitle.TabIndex = 0;
            this.lblConnectionTitle.Text = "Database Connection";
            this.lblConnectionTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblServerCaption
            //
            this.lblServerCaption.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblServerCaption.AutoSize = true;
            this.lblServerCaption.Name = "lblServerCaption";
            this.lblServerCaption.TabIndex = 0;
            this.lblServerCaption.Text = "Server";
            //
            // lblServer
            //
            this.lblServer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblServer.AutoEllipsis = true;
            this.lblServer.Name = "lblServer";
            this.lblServer.Size = new System.Drawing.Size(600, 22);
            this.lblServer.TabIndex = 1;
            this.lblServer.Text = "—";
            this.lblServer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblDatabaseCaption
            //
            this.lblDatabaseCaption.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblDatabaseCaption.AutoSize = true;
            this.lblDatabaseCaption.Name = "lblDatabaseCaption";
            this.lblDatabaseCaption.TabIndex = 2;
            this.lblDatabaseCaption.Text = "Database";
            //
            // lblDatabase
            //
            this.lblDatabase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDatabase.AutoEllipsis = true;
            this.lblDatabase.Name = "lblDatabase";
            this.lblDatabase.Size = new System.Drawing.Size(600, 22);
            this.lblDatabase.TabIndex = 3;
            this.lblDatabase.Text = "—";
            this.lblDatabase.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblAuthenticationCaption
            //
            this.lblAuthenticationCaption.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblAuthenticationCaption.AutoSize = true;
            this.lblAuthenticationCaption.Name = "lblAuthenticationCaption";
            this.lblAuthenticationCaption.TabIndex = 4;
            this.lblAuthenticationCaption.Text = "Authentication";
            //
            // lblAuthentication
            //
            this.lblAuthentication.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAuthentication.AutoEllipsis = true;
            this.lblAuthentication.Name = "lblAuthentication";
            this.lblAuthentication.Size = new System.Drawing.Size(600, 22);
            this.lblAuthentication.TabIndex = 5;
            this.lblAuthentication.Text = "—";
            this.lblAuthentication.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblConnectionNameCaption
            //
            this.lblConnectionNameCaption.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblConnectionNameCaption.AutoSize = true;
            this.lblConnectionNameCaption.Name = "lblConnectionNameCaption";
            this.lblConnectionNameCaption.TabIndex = 6;
            this.lblConnectionNameCaption.Text = "Connection string";
            //
            // lblConnectionName
            //
            this.lblConnectionName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblConnectionName.AutoEllipsis = true;
            this.lblConnectionName.Name = "lblConnectionName";
            this.lblConnectionName.Size = new System.Drawing.Size(600, 22);
            this.lblConnectionName.TabIndex = 7;
            this.lblConnectionName.Text = "—";
            this.lblConnectionName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblConfigFileCaption
            //
            this.lblConfigFileCaption.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblConfigFileCaption.AutoSize = true;
            this.lblConfigFileCaption.Name = "lblConfigFileCaption";
            this.lblConfigFileCaption.TabIndex = 8;
            this.lblConfigFileCaption.Text = "Configuration file";
            //
            // lblConfigFile
            //
            this.lblConfigFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblConfigFile.AutoEllipsis = true;
            this.lblConfigFile.Name = "lblConfigFile";
            this.lblConfigFile.Size = new System.Drawing.Size(600, 22);
            this.lblConfigFile.TabIndex = 9;
            this.lblConfigFile.Text = "—";
            this.lblConfigFile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblServerVersionCaption
            //
            this.lblServerVersionCaption.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblServerVersionCaption.AutoSize = true;
            this.lblServerVersionCaption.Name = "lblServerVersionCaption";
            this.lblServerVersionCaption.TabIndex = 10;
            this.lblServerVersionCaption.Text = "SQL Server version";
            //
            // lblServerVersion
            //
            this.lblServerVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblServerVersion.AutoEllipsis = true;
            this.lblServerVersion.Name = "lblServerVersion";
            this.lblServerVersion.Size = new System.Drawing.Size(600, 22);
            this.lblServerVersion.TabIndex = 11;
            this.lblServerVersion.Text = "—";
            this.lblServerVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblServerEditionCaption
            //
            this.lblServerEditionCaption.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblServerEditionCaption.AutoSize = true;
            this.lblServerEditionCaption.Name = "lblServerEditionCaption";
            this.lblServerEditionCaption.TabIndex = 12;
            this.lblServerEditionCaption.Text = "SQL Server edition";
            //
            // lblServerEdition
            //
            this.lblServerEdition.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblServerEdition.AutoEllipsis = true;
            this.lblServerEdition.Name = "lblServerEdition";
            this.lblServerEdition.Size = new System.Drawing.Size(600, 22);
            this.lblServerEdition.TabIndex = 13;
            this.lblServerEdition.Text = "—";
            this.lblServerEdition.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblCreatedCaption
            //
            this.lblCreatedCaption.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblCreatedCaption.AutoSize = true;
            this.lblCreatedCaption.Name = "lblCreatedCaption";
            this.lblCreatedCaption.TabIndex = 14;
            this.lblCreatedCaption.Text = "Database created";
            //
            // lblCreated
            //
            this.lblCreated.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCreated.AutoEllipsis = true;
            this.lblCreated.Name = "lblCreated";
            this.lblCreated.Size = new System.Drawing.Size(600, 22);
            this.lblCreated.TabIndex = 15;
            this.lblCreated.Text = "—";
            this.lblCreated.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblItemsCaption
            //
            this.lblItemsCaption.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblItemsCaption.AutoSize = true;
            this.lblItemsCaption.Name = "lblItemsCaption";
            this.lblItemsCaption.TabIndex = 16;
            this.lblItemsCaption.Text = "Items";
            //
            // lblItems
            //
            this.lblItems.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblItems.AutoEllipsis = true;
            this.lblItems.Name = "lblItems";
            this.lblItems.Size = new System.Drawing.Size(600, 22);
            this.lblItems.TabIndex = 17;
            this.lblItems.Text = "—";
            this.lblItems.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblTransactionsCaption
            //
            this.lblTransactionsCaption.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblTransactionsCaption.AutoSize = true;
            this.lblTransactionsCaption.Name = "lblTransactionsCaption";
            this.lblTransactionsCaption.TabIndex = 18;
            this.lblTransactionsCaption.Text = "Stock transactions";
            //
            // lblTransactions
            //
            this.lblTransactions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTransactions.AutoEllipsis = true;
            this.lblTransactions.Name = "lblTransactions";
            this.lblTransactions.Size = new System.Drawing.Size(600, 22);
            this.lblTransactions.TabIndex = 19;
            this.lblTransactions.Text = "—";
            this.lblTransactions.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblLastTransactionCaption
            //
            this.lblLastTransactionCaption.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblLastTransactionCaption.AutoSize = true;
            this.lblLastTransactionCaption.Name = "lblLastTransactionCaption";
            this.lblLastTransactionCaption.TabIndex = 20;
            this.lblLastTransactionCaption.Text = "Last transaction";
            //
            // lblLastTransaction
            //
            this.lblLastTransaction.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLastTransaction.AutoEllipsis = true;
            this.lblLastTransaction.Name = "lblLastTransaction";
            this.lblLastTransaction.Size = new System.Drawing.Size(600, 22);
            this.lblLastTransaction.TabIndex = 21;
            this.lblLastTransaction.Text = "—";
            this.lblLastTransaction.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblLogFileCaption
            //
            this.lblLogFileCaption.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblLogFileCaption.AutoSize = true;
            this.lblLogFileCaption.Name = "lblLogFileCaption";
            this.lblLogFileCaption.TabIndex = 22;
            this.lblLogFileCaption.Text = "Application log";
            //
            // lblLogFile
            //
            this.lblLogFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLogFile.AutoEllipsis = true;
            this.lblLogFile.Name = "lblLogFile";
            this.lblLogFile.Size = new System.Drawing.Size(600, 22);
            this.lblLogFile.TabIndex = 23;
            this.lblLogFile.Text = "—";
            this.lblLogFile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblConnectionStatusCaption
            //
            this.lblConnectionStatusCaption.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblConnectionStatusCaption.AutoSize = true;
            this.lblConnectionStatusCaption.Name = "lblConnectionStatusCaption";
            this.lblConnectionStatusCaption.TabIndex = 24;
            this.lblConnectionStatusCaption.Text = "Status";
            //
            // lblConnectionStatus
            //
            this.lblConnectionStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblConnectionStatus.AutoEllipsis = true;
            this.lblConnectionStatus.Name = "lblConnectionStatus";
            this.lblConnectionStatus.Size = new System.Drawing.Size(600, 22);
            this.lblConnectionStatus.TabIndex = 25;
            this.lblConnectionStatus.Text = "—";
            this.lblConnectionStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // flpActions
            //
            this.flpActions.Controls.Add(this.btnTestConnection);
            this.flpActions.Controls.Add(this.btnOpenLogFolder);
            this.flpActions.Controls.Add(this.btnOpenScriptsFolder);
            this.flpActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpActions.Margin = new System.Windows.Forms.Padding(0);
            this.flpActions.Name = "flpActions";
            this.flpActions.TabIndex = 1;
            this.flpActions.WrapContents = false;
            //
            // btnTestConnection
            //
            this.btnTestConnection.Margin = new System.Windows.Forms.Padding(0, 10, 10, 0);
            this.btnTestConnection.Name = "btnTestConnection";
            this.btnTestConnection.Size = new System.Drawing.Size(150, 36);
            this.btnTestConnection.TabIndex = 0;
            this.btnTestConnection.Text = "Test Connection";
            //
            // btnOpenLogFolder
            //
            this.btnOpenLogFolder.Margin = new System.Windows.Forms.Padding(0, 10, 10, 0);
            this.btnOpenLogFolder.Name = "btnOpenLogFolder";
            this.btnOpenLogFolder.Size = new System.Drawing.Size(150, 36);
            this.btnOpenLogFolder.TabIndex = 1;
            this.btnOpenLogFolder.Text = "Open Log Folder";
            //
            // btnOpenScriptsFolder
            //
            this.btnOpenScriptsFolder.Margin = new System.Windows.Forms.Padding(0, 10, 10, 0);
            this.btnOpenScriptsFolder.Name = "btnOpenScriptsFolder";
            this.btnOpenScriptsFolder.Size = new System.Drawing.Size(190, 36);
            this.btnOpenScriptsFolder.TabIndex = 2;
            this.btnOpenScriptsFolder.Text = "Open SQL Scripts Folder";
            //
            // pnlHelp
            //
            this.pnlHelp.BackColor = System.Drawing.Color.White;
            this.pnlHelp.Controls.Add(this.txtHelp);
            this.pnlHelp.Controls.Add(this.lblHelpTitle);
            this.pnlHelp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlHelp.Margin = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.pnlHelp.Name = "pnlHelp";
            this.pnlHelp.Padding = new System.Windows.Forms.Padding(20, 10, 20, 12);
            this.pnlHelp.TabIndex = 2;
            //
            // txtHelp
            //
            this.txtHelp.BackColor = System.Drawing.Color.White;
            this.txtHelp.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtHelp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtHelp.Multiline = true;
            this.txtHelp.Name = "txtHelp";
            this.txtHelp.ReadOnly = true;
            this.txtHelp.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtHelp.TabIndex = 1;
            this.txtHelp.TabStop = false;
            //
            // lblHelpTitle
            //
            this.lblHelpTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblHelpTitle.Name = "lblHelpTitle";
            this.lblHelpTitle.Size = new System.Drawing.Size(900, 34);
            this.lblHelpTitle.TabIndex = 0;
            this.lblHelpTitle.Text = "Changing the database connection";
            this.lblHelpTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // DatabaseInfoForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 700);
            this.Controls.Add(this.tlpMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "DatabaseInfoForm";
            this.Text = "Database Information";
            this.tlpMain.ResumeLayout(false);
            this.pnlConnection.ResumeLayout(false);
            this.tlpInfo.ResumeLayout(false);
            this.tlpInfo.PerformLayout();
            this.flpActions.ResumeLayout(false);
            this.pnlHelp.ResumeLayout(false);
            this.pnlHelp.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.Panel pnlConnection;
        private System.Windows.Forms.TableLayoutPanel tlpInfo;
        private System.Windows.Forms.Label lblConnectionTitle;
        private System.Windows.Forms.Label lblServerCaption;
        private System.Windows.Forms.Label lblServer;
        private System.Windows.Forms.Label lblDatabaseCaption;
        private System.Windows.Forms.Label lblDatabase;
        private System.Windows.Forms.Label lblAuthenticationCaption;
        private System.Windows.Forms.Label lblAuthentication;
        private System.Windows.Forms.Label lblConnectionNameCaption;
        private System.Windows.Forms.Label lblConnectionName;
        private System.Windows.Forms.Label lblConfigFileCaption;
        private System.Windows.Forms.Label lblConfigFile;
        private System.Windows.Forms.Label lblServerVersionCaption;
        private System.Windows.Forms.Label lblServerVersion;
        private System.Windows.Forms.Label lblServerEditionCaption;
        private System.Windows.Forms.Label lblServerEdition;
        private System.Windows.Forms.Label lblCreatedCaption;
        private System.Windows.Forms.Label lblCreated;
        private System.Windows.Forms.Label lblItemsCaption;
        private System.Windows.Forms.Label lblItems;
        private System.Windows.Forms.Label lblTransactionsCaption;
        private System.Windows.Forms.Label lblTransactions;
        private System.Windows.Forms.Label lblLastTransactionCaption;
        private System.Windows.Forms.Label lblLastTransaction;
        private System.Windows.Forms.Label lblLogFileCaption;
        private System.Windows.Forms.Label lblLogFile;
        private System.Windows.Forms.Label lblConnectionStatusCaption;
        private System.Windows.Forms.Label lblConnectionStatus;
        private System.Windows.Forms.FlowLayoutPanel flpActions;
        private System.Windows.Forms.Button btnTestConnection;
        private System.Windows.Forms.Button btnOpenLogFolder;
        private System.Windows.Forms.Button btnOpenScriptsFolder;
        private System.Windows.Forms.Panel pnlHelp;
        private System.Windows.Forms.TextBox txtHelp;
        private System.Windows.Forms.Label lblHelpTitle;
    }
}
