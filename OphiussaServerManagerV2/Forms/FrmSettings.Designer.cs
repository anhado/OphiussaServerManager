﻿namespace OphiussaServerManagerV2
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
            this.fd = new System.Windows.Forms.FolderBrowserDialog();
            this.expandCollapsePanel5 = new MakarovDev.ExpandCollapsePanel.ExpandCollapsePanel();
            this.label16 = new System.Windows.Forms.Label();
            this.txtMaxFiles = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txtMaxDays = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.chkEnableLogs = new System.Windows.Forms.CheckBox();
            this.expandCollapsePanel2 = new MakarovDev.ExpandCollapsePanel.ExpandCollapsePanel();
            this.button6 = new System.Windows.Forms.Button();
            this.txtCurseForgeKey = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.txtSteamKey = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.lblWarningSteam = new System.Windows.Forms.Label();
            this.chkUpdateOnStart = new System.Windows.Forms.CheckBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.chkAnonymous = new System.Windows.Forms.CheckBox();
            this.expandCollapsePanel3 = new MakarovDev.ExpandCollapsePanel.ExpandCollapsePanel();
            this.chkEnableAutoUpdate = new System.Windows.Forms.CheckBox();
            this.chkEnableAutoBackup = new System.Windows.Forms.CheckBox();
            this.chkUseSmartCopy = new System.Windows.Forms.CheckBox();
            this.txtBackupInterval = new System.Windows.Forms.MaskedTextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtUpdateInterval = new System.Windows.Forms.MaskedTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtBackupDays = new System.Windows.Forms.TextBox();
            this.tbDeleteDays = new System.Windows.Forms.TrackBar();
            this.chkDeleteOld = new System.Windows.Forms.CheckBox();
            this.chkUpdateSequencial = new System.Windows.Forms.CheckBox();
            this.expandCollapsePanel1.SuspendLayout();
            this.expandCollapsePanel5.SuspendLayout();
            this.expandCollapsePanel2.SuspendLayout();
            this.expandCollapsePanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbDeleteDays)).BeginInit();
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
            this.expandCollapsePanel1.Size = new System.Drawing.Size(908, 164);
            this.expandCollapsePanel1.TabIndex = 0;
            this.expandCollapsePanel1.Text = "Folders Settings";
            this.expandCollapsePanel1.UseAnimation = true;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
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
            this.txtSteamCmd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            // btBackupFolder
            // 
            this.btBackupFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
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
            this.txtBackupFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.btDefaultInstallFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
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
            this.txtDefaultInstallationFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.btDataFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
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
            this.txtDataFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.txtGUID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.expandCollapsePanel5.Location = new System.Drawing.Point(0, 164);
            this.expandCollapsePanel5.Name = "expandCollapsePanel5";
            this.expandCollapsePanel5.Size = new System.Drawing.Size(908, 122);
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
            // expandCollapsePanel2
            // 
            this.expandCollapsePanel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.expandCollapsePanel2.ButtonSize = MakarovDev.ExpandCollapsePanel.ExpandCollapseButton.ExpandButtonSize.Small;
            this.expandCollapsePanel2.ButtonStyle = MakarovDev.ExpandCollapsePanel.ExpandCollapseButton.ExpandButtonStyle.Classic;
            this.expandCollapsePanel2.Controls.Add(this.button6);
            this.expandCollapsePanel2.Controls.Add(this.txtCurseForgeKey);
            this.expandCollapsePanel2.Controls.Add(this.label18);
            this.expandCollapsePanel2.Controls.Add(this.button5);
            this.expandCollapsePanel2.Controls.Add(this.txtSteamKey);
            this.expandCollapsePanel2.Controls.Add(this.label19);
            this.expandCollapsePanel2.Controls.Add(this.lblWarningSteam);
            this.expandCollapsePanel2.Controls.Add(this.chkUpdateOnStart);
            this.expandCollapsePanel2.Controls.Add(this.txtPassword);
            this.expandCollapsePanel2.Controls.Add(this.label5);
            this.expandCollapsePanel2.Controls.Add(this.txtUserName);
            this.expandCollapsePanel2.Controls.Add(this.label6);
            this.expandCollapsePanel2.Controls.Add(this.chkAnonymous);
            this.expandCollapsePanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.expandCollapsePanel2.ExpandedHeight = 0;
            this.expandCollapsePanel2.IsExpanded = true;
            this.expandCollapsePanel2.Location = new System.Drawing.Point(0, 286);
            this.expandCollapsePanel2.Name = "expandCollapsePanel2";
            this.expandCollapsePanel2.Size = new System.Drawing.Size(908, 172);
            this.expandCollapsePanel2.TabIndex = 7;
            this.expandCollapsePanel2.Text = "Steam && Curseforge Settings";
            this.expandCollapsePanel2.UseAnimation = false;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(418, 136);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(99, 23);
            this.button6.TabIndex = 16;
            this.button6.Text = "GetApi Key";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button5_Click);
            // 
            // txtCurseForgeKey
            // 
            this.txtCurseForgeKey.Location = new System.Drawing.Point(131, 137);
            this.txtCurseForgeKey.Name = "txtCurseForgeKey";
            this.txtCurseForgeKey.Size = new System.Drawing.Size(281, 21);
            this.txtCurseForgeKey.TabIndex = 15;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(16, 140);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(115, 15);
            this.label18.TabIndex = 14;
            this.label18.Text = "CurseForge API Key";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(418, 109);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(99, 23);
            this.button5.TabIndex = 13;
            this.button5.Text = "Get WebApi Key";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button4_Click);
            // 
            // txtSteamKey
            // 
            this.txtSteamKey.Location = new System.Drawing.Point(131, 110);
            this.txtSteamKey.Name = "txtSteamKey";
            this.txtSteamKey.Size = new System.Drawing.Size(281, 21);
            this.txtSteamKey.TabIndex = 9;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(16, 113);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(109, 15);
            this.label19.TabIndex = 8;
            this.label19.Text = "Steam WebKeyAPI";
            // 
            // lblWarningSteam
            // 
            this.lblWarningSteam.AutoSize = true;
            this.lblWarningSteam.ForeColor = System.Drawing.Color.Crimson;
            this.lblWarningSteam.Location = new System.Drawing.Point(11, 65);
            this.lblWarningSteam.Name = "lblWarningSteam";
            this.lblWarningSteam.Size = new System.Drawing.Size(398, 15);
            this.lblWarningSteam.TabIndex = 7;
            this.lblWarningSteam.Text = "ATTENTION: THIS DO NOT SUPPORT TWO FACTOR AUTHENTICATOR";
            // 
            // chkUpdateOnStart
            // 
            this.chkUpdateOnStart.AutoSize = true;
            this.chkUpdateOnStart.Checked = true;
            this.chkUpdateOnStart.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUpdateOnStart.Location = new System.Drawing.Point(195, 43);
            this.chkUpdateOnStart.Name = "chkUpdateOnStart";
            this.chkUpdateOnStart.Size = new System.Drawing.Size(192, 19);
            this.chkUpdateOnStart.TabIndex = 6;
            this.chkUpdateOnStart.Text = "Update SteamCMD on Startup";
            this.chkUpdateOnStart.UseVisualStyleBackColor = true;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(312, 83);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(100, 21);
            this.txtPassword.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(238, 86);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 15);
            this.label5.TabIndex = 4;
            this.label5.Text = "Password:";
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(131, 83);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(100, 21);
            this.txtUserName.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 86);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 15);
            this.label6.TabIndex = 2;
            this.label6.Text = "Username:";
            // 
            // chkAnonymous
            // 
            this.chkAnonymous.AutoSize = true;
            this.chkAnonymous.Checked = true;
            this.chkAnonymous.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAnonymous.Location = new System.Drawing.Point(11, 43);
            this.chkAnonymous.Name = "chkAnonymous";
            this.chkAnonymous.Size = new System.Drawing.Size(178, 19);
            this.chkAnonymous.TabIndex = 1;
            this.chkAnonymous.Text = "Use Anonymous connection";
            this.chkAnonymous.UseVisualStyleBackColor = true;
            // 
            // expandCollapsePanel3
            // 
            this.expandCollapsePanel3.ButtonSize = MakarovDev.ExpandCollapsePanel.ExpandCollapseButton.ExpandButtonSize.Normal;
            this.expandCollapsePanel3.ButtonStyle = MakarovDev.ExpandCollapsePanel.ExpandCollapseButton.ExpandButtonStyle.Circle;
            this.expandCollapsePanel3.Controls.Add(this.chkUpdateSequencial);
            this.expandCollapsePanel3.Controls.Add(this.chkUseSmartCopy);
            this.expandCollapsePanel3.Controls.Add(this.txtBackupInterval);
            this.expandCollapsePanel3.Controls.Add(this.label13);
            this.expandCollapsePanel3.Controls.Add(this.txtUpdateInterval);
            this.expandCollapsePanel3.Controls.Add(this.label8);
            this.expandCollapsePanel3.Controls.Add(this.txtBackupDays);
            this.expandCollapsePanel3.Controls.Add(this.tbDeleteDays);
            this.expandCollapsePanel3.Controls.Add(this.chkDeleteOld);
            this.expandCollapsePanel3.Controls.Add(this.chkEnableAutoBackup);
            this.expandCollapsePanel3.Controls.Add(this.chkEnableAutoUpdate);
            this.expandCollapsePanel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.expandCollapsePanel3.ExpandedHeight = 0;
            this.expandCollapsePanel3.IsExpanded = true;
            this.expandCollapsePanel3.Location = new System.Drawing.Point(0, 458);
            this.expandCollapsePanel3.Name = "expandCollapsePanel3";
            this.expandCollapsePanel3.Size = new System.Drawing.Size(908, 179);
            this.expandCollapsePanel3.TabIndex = 8;
            this.expandCollapsePanel3.Text = "Auto Update && Backup Settings";
            this.expandCollapsePanel3.UseAnimation = true;
            // 
            // chkEnableAutoUpdate
            // 
            this.chkEnableAutoUpdate.AutoSize = true;
            this.chkEnableAutoUpdate.Checked = true;
            this.chkEnableAutoUpdate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEnableAutoUpdate.Location = new System.Drawing.Point(12, 118);
            this.chkEnableAutoUpdate.Name = "chkEnableAutoUpdate";
            this.chkEnableAutoUpdate.Size = new System.Drawing.Size(135, 19);
            this.chkEnableAutoUpdate.TabIndex = 3;
            this.chkEnableAutoUpdate.Text = "Enable Auto Update";
            this.chkEnableAutoUpdate.UseVisualStyleBackColor = true;
            // 
            // chkEnableAutoBackup
            // 
            this.chkEnableAutoBackup.AutoSize = true;
            this.chkEnableAutoBackup.Checked = true;
            this.chkEnableAutoBackup.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEnableAutoBackup.Location = new System.Drawing.Point(12, 41);
            this.chkEnableAutoBackup.Name = "chkEnableAutoBackup";
            this.chkEnableAutoBackup.Size = new System.Drawing.Size(136, 19);
            this.chkEnableAutoBackup.TabIndex = 4;
            this.chkEnableAutoBackup.Text = "Enable Auto Backup";
            this.chkEnableAutoBackup.UseVisualStyleBackColor = true;
            // 
            // chkUseSmartCopy
            // 
            this.chkUseSmartCopy.AutoSize = true;
            this.chkUseSmartCopy.Checked = true;
            this.chkUseSmartCopy.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUseSmartCopy.Location = new System.Drawing.Point(169, 118);
            this.chkUseSmartCopy.Name = "chkUseSmartCopy";
            this.chkUseSmartCopy.Size = new System.Drawing.Size(148, 19);
            this.chkUseSmartCopy.TabIndex = 20;
            this.chkUseSmartCopy.Text = "Use Smart cache copy";
            this.chkUseSmartCopy.UseVisualStyleBackColor = true;
            // 
            // txtBackupInterval
            // 
            this.txtBackupInterval.Location = new System.Drawing.Point(109, 66);
            this.txtBackupInterval.Mask = "00:00";
            this.txtBackupInterval.Name = "txtBackupInterval";
            this.txtBackupInterval.Size = new System.Drawing.Size(100, 20);
            this.txtBackupInterval.TabIndex = 18;
            this.txtBackupInterval.Text = "0100";
            this.txtBackupInterval.ValidatingType = typeof(System.DateTime);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(13, 69);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(90, 15);
            this.label13.TabIndex = 19;
            this.label13.Text = "Backup Interval";
            // 
            // txtUpdateInterval
            // 
            this.txtUpdateInterval.Location = new System.Drawing.Point(109, 143);
            this.txtUpdateInterval.Mask = "00:00";
            this.txtUpdateInterval.Name = "txtUpdateInterval";
            this.txtUpdateInterval.Size = new System.Drawing.Size(100, 20);
            this.txtUpdateInterval.TabIndex = 13;
            this.txtUpdateInterval.Text = "0100";
            this.txtUpdateInterval.ValidatingType = typeof(System.DateTime);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 146);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(89, 15);
            this.label8.TabIndex = 17;
            this.label8.Text = "Update Interval";
            // 
            // txtBackupDays
            // 
            this.txtBackupDays.Location = new System.Drawing.Point(478, 90);
            this.txtBackupDays.Name = "txtBackupDays";
            this.txtBackupDays.ReadOnly = true;
            this.txtBackupDays.Size = new System.Drawing.Size(35, 20);
            this.txtBackupDays.TabIndex = 16;
            // 
            // tbDeleteDays
            // 
            this.tbDeleteDays.Location = new System.Drawing.Point(169, 92);
            this.tbDeleteDays.Maximum = 90;
            this.tbDeleteDays.Name = "tbDeleteDays";
            this.tbDeleteDays.Size = new System.Drawing.Size(303, 45);
            this.tbDeleteDays.TabIndex = 14;
            this.tbDeleteDays.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbDeleteDays.Value = 15;
            this.tbDeleteDays.Scroll += new System.EventHandler(this.tbDeleteDays_Scroll);
            // 
            // chkDeleteOld
            // 
            this.chkDeleteOld.AutoSize = true;
            this.chkDeleteOld.Checked = true;
            this.chkDeleteOld.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDeleteOld.Location = new System.Drawing.Point(12, 92);
            this.chkDeleteOld.Name = "chkDeleteOld";
            this.chkDeleteOld.Size = new System.Drawing.Size(151, 19);
            this.chkDeleteOld.TabIndex = 15;
            this.chkDeleteOld.Text = "Delete old Backup files";
            this.chkDeleteOld.UseVisualStyleBackColor = true;
            // 
            // chkUpdateSequencial
            // 
            this.chkUpdateSequencial.AutoSize = true;
            this.chkUpdateSequencial.Checked = true;
            this.chkUpdateSequencial.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUpdateSequencial.Location = new System.Drawing.Point(337, 118);
            this.chkUpdateSequencial.Name = "chkUpdateSequencial";
            this.chkUpdateSequencial.Size = new System.Drawing.Size(175, 19);
            this.chkUpdateSequencial.TabIndex = 21;
            this.chkUpdateSequencial.Text = "Update Servers Sequencial";
            this.chkUpdateSequencial.UseVisualStyleBackColor = true;
            // 
            // FrmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(908, 636);
            this.Controls.Add(this.expandCollapsePanel3);
            this.Controls.Add(this.expandCollapsePanel2);
            this.Controls.Add(this.expandCollapsePanel5);
            this.Controls.Add(this.expandCollapsePanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(924, 675);
            this.MinimumSize = new System.Drawing.Size(924, 675);
            this.Name = "FrmSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmSettings_FormClosing);
            this.Load += new System.EventHandler(this.FrmSettings_Load);
            this.expandCollapsePanel1.ResumeLayout(false);
            this.expandCollapsePanel1.PerformLayout();
            this.expandCollapsePanel5.ResumeLayout(false);
            this.expandCollapsePanel5.PerformLayout();
            this.expandCollapsePanel2.ResumeLayout(false);
            this.expandCollapsePanel2.PerformLayout();
            this.expandCollapsePanel3.ResumeLayout(false);
            this.expandCollapsePanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbDeleteDays)).EndInit();
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
        private MakarovDev.ExpandCollapsePanel.ExpandCollapsePanel expandCollapsePanel2;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.TextBox txtCurseForgeKey;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TextBox txtSteamKey;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label lblWarningSteam;
        private System.Windows.Forms.CheckBox chkUpdateOnStart;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox chkAnonymous;
        private MakarovDev.ExpandCollapsePanel.ExpandCollapsePanel expandCollapsePanel3;
        private System.Windows.Forms.CheckBox chkEnableAutoBackup;
        private System.Windows.Forms.CheckBox chkEnableAutoUpdate;
        private System.Windows.Forms.CheckBox chkUseSmartCopy;
        private System.Windows.Forms.MaskedTextBox txtBackupInterval;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.MaskedTextBox txtUpdateInterval;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtBackupDays;
        private System.Windows.Forms.TrackBar tbDeleteDays;
        private System.Windows.Forms.CheckBox chkDeleteOld;
        private System.Windows.Forms.CheckBox chkUpdateSequencial;
    }
}