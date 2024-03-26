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
            this.button1 = new System.Windows.Forms.Button();
            this.txtSteamCmd = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
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
            this.button5 = new System.Windows.Forms.Button();
            this.txtCurseForgeKey = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.txtSteamWebApiKey = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.fd = new System.Windows.Forms.FolderBrowserDialog();
            this.osmTabControl1 = new OSMTabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.osmThemeContainer1 = new OSMThemeContainer();
            this.osmControlBox1 = new OSMControlBox();
            this.chkEnableLogs = new OSMCheckBox();
            this.osmLabel1 = new OSMLabel();
            this.osmLabel2 = new OSMLabel();
            this.txtMaxFiles = new OSMTextBox_Small();
            this.txtMaxDays = new OSMTextBox_Small();
            this.osmLabel3 = new OSMLabel();
            this.osmLabel4 = new OSMLabel();
            this.osmTabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.osmThemeContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(593, 110);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(28, 23);
            this.button1.TabIndex = 14;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // txtSteamCmd
            // 
            this.txtSteamCmd.Location = new System.Drawing.Point(190, 111);
            this.txtSteamCmd.Name = "txtSteamCmd";
            this.txtSteamCmd.Size = new System.Drawing.Size(397, 27);
            this.txtSteamCmd.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 114);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(134, 20);
            this.label7.TabIndex = 12;
            this.label7.Text = "Steam CMD Folder";
            // 
            // btBackupFolder
            // 
            this.btBackupFolder.Location = new System.Drawing.Point(593, 83);
            this.btBackupFolder.Name = "btBackupFolder";
            this.btBackupFolder.Size = new System.Drawing.Size(28, 23);
            this.btBackupFolder.TabIndex = 11;
            this.btBackupFolder.Text = "...";
            this.btBackupFolder.UseVisualStyleBackColor = true;
            this.btBackupFolder.Click += new System.EventHandler(this.btBackupFolder_Click);
            // 
            // txtBackupFolder
            // 
            this.txtBackupFolder.Location = new System.Drawing.Point(190, 84);
            this.txtBackupFolder.Name = "txtBackupFolder";
            this.txtBackupFolder.Size = new System.Drawing.Size(397, 27);
            this.txtBackupFolder.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 20);
            this.label4.TabIndex = 9;
            this.label4.Text = "Backup folder";
            // 
            // btDefaultInstallFolder
            // 
            this.btDefaultInstallFolder.Location = new System.Drawing.Point(593, 57);
            this.btDefaultInstallFolder.Name = "btDefaultInstallFolder";
            this.btDefaultInstallFolder.Size = new System.Drawing.Size(28, 23);
            this.btDefaultInstallFolder.TabIndex = 8;
            this.btDefaultInstallFolder.Text = "...";
            this.btDefaultInstallFolder.UseVisualStyleBackColor = true;
            this.btDefaultInstallFolder.Click += new System.EventHandler(this.btDefaultInstallFolder_Click);
            // 
            // txtDefaultInstallationFolder
            // 
            this.txtDefaultInstallationFolder.Location = new System.Drawing.Point(190, 58);
            this.txtDefaultInstallationFolder.Name = "txtDefaultInstallationFolder";
            this.txtDefaultInstallationFolder.Size = new System.Drawing.Size(397, 27);
            this.txtDefaultInstallationFolder.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(181, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Default Installation Folder";
            // 
            // btDataFolder
            // 
            this.btDataFolder.Location = new System.Drawing.Point(593, 31);
            this.btDataFolder.Name = "btDataFolder";
            this.btDataFolder.Size = new System.Drawing.Size(28, 23);
            this.btDataFolder.TabIndex = 5;
            this.btDataFolder.Text = "...";
            this.btDataFolder.UseVisualStyleBackColor = true;
            this.btDataFolder.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtDataFolder
            // 
            this.txtDataFolder.Location = new System.Drawing.Point(190, 32);
            this.txtDataFolder.Name = "txtDataFolder";
            this.txtDataFolder.Size = new System.Drawing.Size(397, 27);
            this.txtDataFolder.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Data Folder";
            // 
            // txtGUID
            // 
            this.txtGUID.Enabled = false;
            this.txtGUID.Location = new System.Drawing.Point(190, 6);
            this.txtGUID.Name = "txtGUID";
            this.txtGUID.Size = new System.Drawing.Size(397, 27);
            this.txtGUID.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "GUID";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(558, 31);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(28, 23);
            this.button5.TabIndex = 17;
            this.button5.Text = "...";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // txtCurseForgeKey
            // 
            this.txtCurseForgeKey.Location = new System.Drawing.Point(155, 32);
            this.txtCurseForgeKey.Name = "txtCurseForgeKey";
            this.txtCurseForgeKey.PasswordChar = '*';
            this.txtCurseForgeKey.Size = new System.Drawing.Size(397, 27);
            this.txtCurseForgeKey.TabIndex = 16;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 35);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(141, 20);
            this.label6.TabIndex = 15;
            this.label6.Text = "Curse Forge API Key";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(558, 5);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(28, 23);
            this.button4.TabIndex = 14;
            this.button4.Text = "...";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // txtSteamWebApiKey
            // 
            this.txtSteamWebApiKey.Location = new System.Drawing.Point(155, 6);
            this.txtSteamWebApiKey.Name = "txtSteamWebApiKey";
            this.txtSteamWebApiKey.PasswordChar = '*';
            this.txtSteamWebApiKey.Size = new System.Drawing.Size(397, 27);
            this.txtSteamWebApiKey.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(135, 20);
            this.label5.TabIndex = 12;
            this.label5.Text = "Steam WebAPI Key";
            // 
            // osmTabControl1
            // 
            this.osmTabControl1.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.osmTabControl1.Controls.Add(this.tabPage3);
            this.osmTabControl1.Controls.Add(this.tabPage2);
            this.osmTabControl1.Controls.Add(this.tabPage1);
            this.osmTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.osmTabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.osmTabControl1.ItemSize = new System.Drawing.Size(44, 135);
            this.osmTabControl1.Location = new System.Drawing.Point(3, 28);
            this.osmTabControl1.Multiline = true;
            this.osmTabControl1.Name = "osmTabControl1";
            this.osmTabControl1.SelectedIndex = 0;
            this.osmTabControl1.Size = new System.Drawing.Size(779, 311);
            this.osmTabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.osmTabControl1.TabIndex = 7;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.tabPage1.Controls.Add(this.osmLabel4);
            this.tabPage1.Controls.Add(this.osmLabel3);
            this.tabPage1.Controls.Add(this.txtMaxDays);
            this.tabPage1.Controls.Add(this.txtMaxFiles);
            this.tabPage1.Controls.Add(this.osmLabel2);
            this.tabPage1.Controls.Add(this.osmLabel1);
            this.tabPage1.Controls.Add(this.chkEnableLogs);
            this.tabPage1.Location = new System.Drawing.Point(139, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(636, 303);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Logging";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.button5);
            this.tabPage2.Controls.Add(this.txtSteamWebApiKey);
            this.tabPage2.Controls.Add(this.txtCurseForgeKey);
            this.tabPage2.Controls.Add(this.button4);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Location = new System.Drawing.Point(139, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(636, 303);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Api Settings";
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.tabPage3.Controls.Add(this.label1);
            this.tabPage3.Controls.Add(this.button1);
            this.tabPage3.Controls.Add(this.txtGUID);
            this.tabPage3.Controls.Add(this.txtSteamCmd);
            this.tabPage3.Controls.Add(this.label2);
            this.tabPage3.Controls.Add(this.label7);
            this.tabPage3.Controls.Add(this.txtDataFolder);
            this.tabPage3.Controls.Add(this.btBackupFolder);
            this.tabPage3.Controls.Add(this.btDataFolder);
            this.tabPage3.Controls.Add(this.txtBackupFolder);
            this.tabPage3.Controls.Add(this.label3);
            this.tabPage3.Controls.Add(this.label4);
            this.tabPage3.Controls.Add(this.txtDefaultInstallationFolder);
            this.tabPage3.Controls.Add(this.btDefaultInstallFolder);
            this.tabPage3.Location = new System.Drawing.Point(139, 4);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(636, 303);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Folders";
            // 
            // osmThemeContainer1
            // 
            this.osmThemeContainer1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.osmThemeContainer1.Controls.Add(this.osmControlBox1);
            this.osmThemeContainer1.Controls.Add(this.osmTabControl1);
            this.osmThemeContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.osmThemeContainer1.DrawBottomBar = true;
            this.osmThemeContainer1.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.osmThemeContainer1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.osmThemeContainer1.Location = new System.Drawing.Point(0, 0);
            this.osmThemeContainer1.Name = "osmThemeContainer1";
            this.osmThemeContainer1.Padding = new System.Windows.Forms.Padding(3, 28, 3, 28);
            this.osmThemeContainer1.Size = new System.Drawing.Size(785, 367);
            this.osmThemeContainer1.TabIndex = 8;
            this.osmThemeContainer1.Text = "Settings";
            this.osmThemeContainer1.TextBottom = null;
            // 
            // osmControlBox1
            // 
            this.osmControlBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.osmControlBox1.BackColor = System.Drawing.Color.Transparent;
            this.osmControlBox1.Location = new System.Drawing.Point(702, 0);
            this.osmControlBox1.MinimizeBox = true;
            this.osmControlBox1.Name = "osmControlBox1";
            this.osmControlBox1.Size = new System.Drawing.Size(77, 19);
            this.osmControlBox1.TabIndex = 8;
            this.osmControlBox1.Text = "osmControlBox1";
            // 
            // chkEnableLogs
            // 
            this.chkEnableLogs.BackColor = System.Drawing.Color.Transparent;
            this.chkEnableLogs.Checked = true;
            this.chkEnableLogs.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.chkEnableLogs.Location = new System.Drawing.Point(6, 6);
            this.chkEnableLogs.Name = "chkEnableLogs";
            this.chkEnableLogs.Size = new System.Drawing.Size(120, 15);
            this.chkEnableLogs.TabIndex = 21;
            this.chkEnableLogs.Text = "Enable Logs";
            // 
            // osmLabel1
            // 
            this.osmLabel1.AutoSize = true;
            this.osmLabel1.BackColor = System.Drawing.Color.Transparent;
            this.osmLabel1.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.osmLabel1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.osmLabel1.Location = new System.Drawing.Point(6, 24);
            this.osmLabel1.Name = "osmLabel1";
            this.osmLabel1.Size = new System.Drawing.Size(125, 20);
            this.osmLabel1.TabIndex = 22;
            this.osmLabel1.Text = "Delete Logs After";
            // 
            // osmLabel2
            // 
            this.osmLabel2.AutoSize = true;
            this.osmLabel2.BackColor = System.Drawing.Color.Transparent;
            this.osmLabel2.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.osmLabel2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.osmLabel2.Location = new System.Drawing.Point(6, 58);
            this.osmLabel2.Name = "osmLabel2";
            this.osmLabel2.Size = new System.Drawing.Size(148, 20);
            this.osmLabel2.TabIndex = 23;
            this.osmLabel2.Text = "Max Number of Logs";
            // 
            // txtMaxFiles
            // 
            this.txtMaxFiles.BackColor = System.Drawing.Color.Transparent;
            this.txtMaxFiles.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtMaxFiles.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtMaxFiles.Location = new System.Drawing.Point(160, 58);
            this.txtMaxFiles.MaxLength = 32767;
            this.txtMaxFiles.Multiline = false;
            this.txtMaxFiles.Name = "txtMaxFiles";
            this.txtMaxFiles.ReadOnly = false;
            this.txtMaxFiles.Size = new System.Drawing.Size(135, 28);
            this.txtMaxFiles.TabIndex = 24;
            this.txtMaxFiles.Text = "30";
            this.txtMaxFiles.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtMaxFiles.UseSystemPasswordChar = false;
            this.txtMaxFiles.Value = "30";
            // 
            // txtMaxDays
            // 
            this.txtMaxDays.BackColor = System.Drawing.Color.Transparent;
            this.txtMaxDays.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtMaxDays.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtMaxDays.Location = new System.Drawing.Point(160, 24);
            this.txtMaxDays.MaxLength = 32767;
            this.txtMaxDays.Multiline = false;
            this.txtMaxDays.Name = "txtMaxDays";
            this.txtMaxDays.ReadOnly = false;
            this.txtMaxDays.Size = new System.Drawing.Size(135, 28);
            this.txtMaxDays.TabIndex = 25;
            this.txtMaxDays.Text = "30";
            this.txtMaxDays.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtMaxDays.UseSystemPasswordChar = false;
            this.txtMaxDays.Value = "30";
            // 
            // osmLabel3
            // 
            this.osmLabel3.AutoSize = true;
            this.osmLabel3.BackColor = System.Drawing.Color.Transparent;
            this.osmLabel3.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.osmLabel3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.osmLabel3.Location = new System.Drawing.Point(301, 32);
            this.osmLabel3.Name = "osmLabel3";
            this.osmLabel3.Size = new System.Drawing.Size(39, 20);
            this.osmLabel3.TabIndex = 26;
            this.osmLabel3.Text = "days";
            // 
            // osmLabel4
            // 
            this.osmLabel4.AutoSize = true;
            this.osmLabel4.BackColor = System.Drawing.Color.Transparent;
            this.osmLabel4.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.osmLabel4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.osmLabel4.Location = new System.Drawing.Point(301, 66);
            this.osmLabel4.Name = "osmLabel4";
            this.osmLabel4.Size = new System.Drawing.Size(36, 20);
            this.osmLabel4.TabIndex = 27;
            this.osmLabel4.Text = "files";
            // 
            // FrmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(785, 367);
            this.Controls.Add(this.osmThemeContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmSettings_FormClosing);
            this.Load += new System.EventHandler(this.FrmSettings_Load);
            this.osmTabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.osmThemeContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
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
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox txtSteamWebApiKey;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TextBox txtCurseForgeKey;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.FolderBrowserDialog fd;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtSteamCmd;
        private System.Windows.Forms.Label label7;
        private OSMTabControl osmTabControl1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage1;
        private OSMThemeContainer osmThemeContainer1;
        private OSMControlBox osmControlBox1;
        private OSMLabel osmLabel2;
        private OSMLabel osmLabel1;
        private OSMCheckBox chkEnableLogs;
        private OSMLabel osmLabel4;
        private OSMLabel osmLabel3;
        private OSMTextBox_Small txtMaxDays;
        private OSMTextBox_Small txtMaxFiles;
    }
}