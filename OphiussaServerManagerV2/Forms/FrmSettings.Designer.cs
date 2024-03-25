namespace OphiussaServerManagerV2
{
    partial class FrmSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSettings));
            this.expandCollapsePanel1 = new MakarovDev.ExpandCollapsePanel.ExpandCollapsePanel();
            this.btBackupFolder = new System.Windows.Forms.Button();
            this.txtBackupFolder = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btDefaultInstallFolder = new System.Windows.Forms.Button();
            this.txtDefaultInstallationFolder = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btDataFolder = new System.Windows.Forms.Button();
            this.txtDataFolder = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtGUID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.expandCollapsePanel2 = new MakarovDev.ExpandCollapsePanel.ExpandCollapsePanel();
            this.button5 = new System.Windows.Forms.Button();
            this.txtCurseForgeKey = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.txtSteamWebApiKey = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.fd = new System.Windows.Forms.FolderBrowserDialog();
            this.expandCollapsePanel5 = new MakarovDev.ExpandCollapsePanel.ExpandCollapsePanel();
            this.label16 = new System.Windows.Forms.Label();
            this.txtMaxFiles = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txtMaxDays = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.chkEnableLogs = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.txtSteamCmd = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.expandCollapsePanel1.SuspendLayout();
            this.expandCollapsePanel2.SuspendLayout();
            this.expandCollapsePanel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // expandCollapsePanel1
            // 
            this.expandCollapsePanel1.ButtonSize = MakarovDev.ExpandCollapsePanel.ExpandCollapseButton.ExpandButtonSize.Small;
            this.expandCollapsePanel1.ButtonStyle = MakarovDev.ExpandCollapsePanel.ExpandCollapseButton.ExpandButtonStyle.Classic;
            this.expandCollapsePanel1.Controls.Add(this.button1);
            this.expandCollapsePanel1.Controls.Add(this.txtSteamCmd);
            this.expandCollapsePanel1.Controls.Add(this.label7);
            this.expandCollapsePanel1.Controls.Add(this.btBackupFolder);
            this.expandCollapsePanel1.Controls.Add(this.txtBackupFolder);
            this.expandCollapsePanel1.Controls.Add(this.label4);
            this.expandCollapsePanel1.Controls.Add(this.btDefaultInstallFolder);
            this.expandCollapsePanel1.Controls.Add(this.txtDefaultInstallationFolder);
            this.expandCollapsePanel1.Controls.Add(this.label3);
            this.expandCollapsePanel1.Controls.Add(this.btDataFolder);
            this.expandCollapsePanel1.Controls.Add(this.txtDataFolder);
            this.expandCollapsePanel1.Controls.Add(this.label2);
            this.expandCollapsePanel1.Controls.Add(this.txtGUID);
            this.expandCollapsePanel1.Controls.Add(this.label1);
            this.expandCollapsePanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.expandCollapsePanel1.ExpandedHeight = 0;
            this.expandCollapsePanel1.IsExpanded = true;
            this.expandCollapsePanel1.Location = new System.Drawing.Point(0, 0);
            this.expandCollapsePanel1.Name = "expandCollapsePanel1";
            this.expandCollapsePanel1.Size = new System.Drawing.Size(800, 164);
            this.expandCollapsePanel1.TabIndex = 0;
            this.expandCollapsePanel1.Text = "Folders Settings";
            this.expandCollapsePanel1.UseAnimation = true;
            // 
            // btBackupFolder
            // 
            this.btBackupFolder.Location = new System.Drawing.Point(567, 103);
            this.btBackupFolder.Name = "btBackupFolder";
            this.btBackupFolder.Size = new System.Drawing.Size(28, 23);
            this.btBackupFolder.TabIndex = 11;
            this.btBackupFolder.Text = "...";
            this.btBackupFolder.UseVisualStyleBackColor = true;
            this.btBackupFolder.Click += new System.EventHandler(this.btBackupFolder_Click);
            // 
            // txtBackupFolder
            // 
            this.txtBackupFolder.Location = new System.Drawing.Point(164, 104);
            this.txtBackupFolder.Name = "txtBackupFolder";
            this.txtBackupFolder.Size = new System.Drawing.Size(397, 21);
            this.txtBackupFolder.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 107);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 15);
            this.label4.TabIndex = 9;
            this.label4.Text = "Backup folder";
            // 
            // btDefaultInstallFolder
            // 
            this.btDefaultInstallFolder.Location = new System.Drawing.Point(567, 77);
            this.btDefaultInstallFolder.Name = "btDefaultInstallFolder";
            this.btDefaultInstallFolder.Size = new System.Drawing.Size(28, 23);
            this.btDefaultInstallFolder.TabIndex = 8;
            this.btDefaultInstallFolder.Text = "...";
            this.btDefaultInstallFolder.UseVisualStyleBackColor = true;
            this.btDefaultInstallFolder.Click += new System.EventHandler(this.btDefaultInstallFolder_Click);
            // 
            // txtDefaultInstallationFolder
            // 
            this.txtDefaultInstallationFolder.Location = new System.Drawing.Point(164, 78);
            this.txtDefaultInstallationFolder.Name = "txtDefaultInstallationFolder";
            this.txtDefaultInstallationFolder.Size = new System.Drawing.Size(397, 21);
            this.txtDefaultInstallationFolder.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(146, 15);
            this.label3.TabIndex = 6;
            this.label3.Text = "Default Installation Folder";
            // 
            // btDataFolder
            // 
            this.btDataFolder.Location = new System.Drawing.Point(567, 51);
            this.btDataFolder.Name = "btDataFolder";
            this.btDataFolder.Size = new System.Drawing.Size(28, 23);
            this.btDataFolder.TabIndex = 5;
            this.btDataFolder.Text = "...";
            this.btDataFolder.UseVisualStyleBackColor = true;
            this.btDataFolder.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtDataFolder
            // 
            this.txtDataFolder.Location = new System.Drawing.Point(164, 52);
            this.txtDataFolder.Name = "txtDataFolder";
            this.txtDataFolder.Size = new System.Drawing.Size(397, 21);
            this.txtDataFolder.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Data Folder";
            // 
            // txtGUID
            // 
            this.txtGUID.Enabled = false;
            this.txtGUID.Location = new System.Drawing.Point(164, 26);
            this.txtGUID.Name = "txtGUID";
            this.txtGUID.Size = new System.Drawing.Size(397, 21);
            this.txtGUID.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "GUID";
            // 
            // expandCollapsePanel2
            // 
            this.expandCollapsePanel2.ButtonSize = MakarovDev.ExpandCollapsePanel.ExpandCollapseButton.ExpandButtonSize.Small;
            this.expandCollapsePanel2.ButtonStyle = MakarovDev.ExpandCollapsePanel.ExpandCollapseButton.ExpandButtonStyle.Classic;
            this.expandCollapsePanel2.Controls.Add(this.button5);
            this.expandCollapsePanel2.Controls.Add(this.txtCurseForgeKey);
            this.expandCollapsePanel2.Controls.Add(this.label6);
            this.expandCollapsePanel2.Controls.Add(this.button4);
            this.expandCollapsePanel2.Controls.Add(this.txtSteamWebApiKey);
            this.expandCollapsePanel2.Controls.Add(this.label5);
            this.expandCollapsePanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.expandCollapsePanel2.ExpandedHeight = 0;
            this.expandCollapsePanel2.IsExpanded = true;
            this.expandCollapsePanel2.Location = new System.Drawing.Point(0, 164);
            this.expandCollapsePanel2.Name = "expandCollapsePanel2";
            this.expandCollapsePanel2.Size = new System.Drawing.Size(800, 88);
            this.expandCollapsePanel2.TabIndex = 1;
            this.expandCollapsePanel2.Text = "API Settings";
            this.expandCollapsePanel2.UseAnimation = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(567, 53);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(28, 23);
            this.button5.TabIndex = 17;
            this.button5.Text = "...";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // txtCurseForgeKey
            // 
            this.txtCurseForgeKey.Location = new System.Drawing.Point(164, 54);
            this.txtCurseForgeKey.Name = "txtCurseForgeKey";
            this.txtCurseForgeKey.PasswordChar = '*';
            this.txtCurseForgeKey.Size = new System.Drawing.Size(397, 21);
            this.txtCurseForgeKey.TabIndex = 16;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 57);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(118, 15);
            this.label6.TabIndex = 15;
            this.label6.Text = "Curse Forge API Key";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(567, 27);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(28, 23);
            this.button4.TabIndex = 14;
            this.button4.Text = "...";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // txtSteamWebApiKey
            // 
            this.txtSteamWebApiKey.Location = new System.Drawing.Point(164, 28);
            this.txtSteamWebApiKey.Name = "txtSteamWebApiKey";
            this.txtSteamWebApiKey.PasswordChar = '*';
            this.txtSteamWebApiKey.Size = new System.Drawing.Size(397, 21);
            this.txtSteamWebApiKey.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(112, 15);
            this.label5.TabIndex = 12;
            this.label5.Text = "Steam WebAPI Key";
            // 
            // expandCollapsePanel5
            // 
            this.expandCollapsePanel5.ButtonSize = MakarovDev.ExpandCollapsePanel.ExpandCollapseButton.ExpandButtonSize.Small;
            this.expandCollapsePanel5.ButtonStyle = MakarovDev.ExpandCollapsePanel.ExpandCollapseButton.ExpandButtonStyle.Classic;
            this.expandCollapsePanel5.Controls.Add(this.label16);
            this.expandCollapsePanel5.Controls.Add(this.txtMaxFiles);
            this.expandCollapsePanel5.Controls.Add(this.label17);
            this.expandCollapsePanel5.Controls.Add(this.label15);
            this.expandCollapsePanel5.Controls.Add(this.txtMaxDays);
            this.expandCollapsePanel5.Controls.Add(this.label14);
            this.expandCollapsePanel5.Controls.Add(this.chkEnableLogs);
            this.expandCollapsePanel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.expandCollapsePanel5.ExpandedHeight = 0;
            this.expandCollapsePanel5.IsExpanded = true;
            this.expandCollapsePanel5.Location = new System.Drawing.Point(0, 252);
            this.expandCollapsePanel5.Name = "expandCollapsePanel5";
            this.expandCollapsePanel5.Size = new System.Drawing.Size(800, 122);
            this.expandCollapsePanel5.TabIndex = 6;
            this.expandCollapsePanel5.Text = "Logging";
            this.expandCollapsePanel5.UseAnimation = true;
            this.expandCollapsePanel5.Paint += new System.Windows.Forms.PaintEventHandler(this.expandCollapsePanel5_Paint);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(215, 94);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(29, 15);
            this.label16.TabIndex = 20;
            this.label16.Text = "files";
            // 
            // txtMaxFiles
            // 
            this.txtMaxFiles.Location = new System.Drawing.Point(140, 91);
            this.txtMaxFiles.Name = "txtMaxFiles";
            this.txtMaxFiles.Size = new System.Drawing.Size(69, 21);
            this.txtMaxFiles.TabIndex = 19;
            this.txtMaxFiles.Text = "30";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(12, 94);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(122, 15);
            this.label17.TabIndex = 18;
            this.label17.Text = "Max Number of Logs";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(215, 68);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(32, 15);
            this.label15.TabIndex = 17;
            this.label15.Text = "days";
            // 
            // txtMaxDays
            // 
            this.txtMaxDays.Location = new System.Drawing.Point(140, 65);
            this.txtMaxDays.Name = "txtMaxDays";
            this.txtMaxDays.Size = new System.Drawing.Size(69, 21);
            this.txtMaxDays.TabIndex = 16;
            this.txtMaxDays.Text = "30";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(12, 68);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(100, 15);
            this.label14.TabIndex = 15;
            this.label14.Text = "Delete Logs After";
            // 
            // chkEnableLogs
            // 
            this.chkEnableLogs.AutoSize = true;
            this.chkEnableLogs.Checked = true;
            this.chkEnableLogs.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEnableLogs.Location = new System.Drawing.Point(12, 46);
            this.chkEnableLogs.Name = "chkEnableLogs";
            this.chkEnableLogs.Size = new System.Drawing.Size(95, 19);
            this.chkEnableLogs.TabIndex = 2;
            this.chkEnableLogs.Text = "Enable Logs";
            this.chkEnableLogs.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(567, 130);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(28, 23);
            this.button1.TabIndex = 14;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // txtSteamCmd
            // 
            this.txtSteamCmd.Location = new System.Drawing.Point(164, 131);
            this.txtSteamCmd.Name = "txtSteamCmd";
            this.txtSteamCmd.Size = new System.Drawing.Size(397, 21);
            this.txtSteamCmd.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 134);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(112, 15);
            this.label7.TabIndex = 12;
            this.label7.Text = "Steam CMD Folder";
            // 
            // FrmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.expandCollapsePanel5);
            this.Controls.Add(this.expandCollapsePanel2);
            this.Controls.Add(this.expandCollapsePanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmSettings_FormClosing);
            this.Load += new System.EventHandler(this.FrmSettings_Load);
            this.expandCollapsePanel1.ResumeLayout(false);
            this.expandCollapsePanel1.PerformLayout();
            this.expandCollapsePanel2.ResumeLayout(false);
            this.expandCollapsePanel2.PerformLayout();
            this.expandCollapsePanel5.ResumeLayout(false);
            this.expandCollapsePanel5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MakarovDev.ExpandCollapsePanel.ExpandCollapsePanel expandCollapsePanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btDataFolder;
        private System.Windows.Forms.TextBox txtDataFolder;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtGUID;
        private System.Windows.Forms.Button btBackupFolder;
        private System.Windows.Forms.TextBox txtBackupFolder;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btDefaultInstallFolder;
        private System.Windows.Forms.TextBox txtDefaultInstallationFolder;
        private System.Windows.Forms.Label label3;
        private MakarovDev.ExpandCollapsePanel.ExpandCollapsePanel expandCollapsePanel2;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox txtSteamWebApiKey;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TextBox txtCurseForgeKey;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.FolderBrowserDialog fd;
        private MakarovDev.ExpandCollapsePanel.ExpandCollapsePanel expandCollapsePanel5;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtMaxFiles;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtMaxDays;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.CheckBox chkEnableLogs;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtSteamCmd;
        private System.Windows.Forms.Label label7;
    }
}