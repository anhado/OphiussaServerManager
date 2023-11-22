namespace OphiussaServerManager.Forms
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.txtDataFolder = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtInstallFolder = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtSteamCmd = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.expandCollapsePanel4 = new MakarovDev.ExpandCollapsePanel.ExpandCollapsePanel();
            this.txtCancelMessage = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtMessage2 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtMessage1 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtGrace = new System.Windows.Forms.TextBox();
            this.tbGracePeriod = new System.Windows.Forms.TrackBar();
            this.chkSendMessage = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.chlPerformPlayer = new System.Windows.Forms.CheckBox();
            this.expandCollapsePanel3 = new MakarovDev.ExpandCollapsePanel.ExpandCollapsePanel();
            this.chkUseSmartCopy = new System.Windows.Forms.CheckBox();
            this.txtBackupInterval = new System.Windows.Forms.MaskedTextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.chkEnableAutoBackup = new System.Windows.Forms.CheckBox();
            this.txtUpdateInterval = new System.Windows.Forms.MaskedTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.chkAutoUpdate = new System.Windows.Forms.CheckBox();
            this.txtWordSave = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBackupDays = new System.Windows.Forms.TextBox();
            this.tbDeleteDays = new System.Windows.Forms.TrackBar();
            this.chkDeleteOld = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.txtBackup = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.chkIncludeSaveGames = new System.Windows.Forms.CheckBox();
            this.expandCollapsePanel2 = new MakarovDev.ExpandCollapsePanel.ExpandCollapsePanel();
            this.chkUpdateMods = new System.Windows.Forms.CheckBox();
            this.chkPerform = new System.Windows.Forms.CheckBox();
            this.chkValidate = new System.Windows.Forms.CheckBox();
            this.expandCollapsePanel1 = new MakarovDev.ExpandCollapsePanel.ExpandCollapsePanel();
            this.chkUpdateOnStart = new System.Windows.Forms.CheckBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkAnonymous = new System.Windows.Forms.CheckBox();
            this.expandCollapsePanel5 = new MakarovDev.ExpandCollapsePanel.ExpandCollapsePanel();
            this.chkEnableLogs = new System.Windows.Forms.CheckBox();
            this.txtMaxDays = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.txtMaxFiles = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.expandCollapsePanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbGracePeriod)).BeginInit();
            this.expandCollapsePanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbDeleteDays)).BeginInit();
            this.expandCollapsePanel2.SuspendLayout();
            this.expandCollapsePanel1.SuspendLayout();
            this.expandCollapsePanel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.txtDataFolder);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.txtInstallFolder);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.txtSteamCmd);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 89);
            this.panel1.TabIndex = 0;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(752, 55);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(35, 23);
            this.button4.TabIndex = 12;
            this.button4.Text = "...";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(752, 29);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(35, 23);
            this.button3.TabIndex = 11;
            this.button3.Text = "...";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(752, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(35, 23);
            this.button2.TabIndex = 10;
            this.button2.Text = "...";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txtDataFolder
            // 
            this.txtDataFolder.Location = new System.Drawing.Point(143, 6);
            this.txtDataFolder.Name = "txtDataFolder";
            this.txtDataFolder.ReadOnly = true;
            this.txtDataFolder.Size = new System.Drawing.Size(603, 20);
            this.txtDataFolder.TabIndex = 9;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(11, 9);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(62, 13);
            this.label12.TabIndex = 8;
            this.label12.Text = "Data Folder";
            // 
            // txtInstallFolder
            // 
            this.txtInstallFolder.Location = new System.Drawing.Point(143, 31);
            this.txtInstallFolder.Name = "txtInstallFolder";
            this.txtInstallFolder.ReadOnly = true;
            this.txtInstallFolder.Size = new System.Drawing.Size(603, 20);
            this.txtInstallFolder.TabIndex = 7;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(11, 34);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(126, 13);
            this.label11.TabIndex = 6;
            this.label11.Text = "Default Installation Folder";
            // 
            // txtSteamCmd
            // 
            this.txtSteamCmd.Location = new System.Drawing.Point(143, 57);
            this.txtSteamCmd.Name = "txtSteamCmd";
            this.txtSteamCmd.ReadOnly = true;
            this.txtSteamCmd.Size = new System.Drawing.Size(603, 20);
            this.txtSteamCmd.TabIndex = 5;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(11, 60);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(105, 13);
            this.label10.TabIndex = 4;
            this.label10.Text = "SteamCMD Location";
            // 
            // expandCollapsePanel4
            // 
            this.expandCollapsePanel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.expandCollapsePanel4.ButtonSize = MakarovDev.ExpandCollapsePanel.ExpandCollapseButton.ExpandButtonSize.Small;
            this.expandCollapsePanel4.ButtonStyle = MakarovDev.ExpandCollapsePanel.ExpandCollapseButton.ExpandButtonStyle.Classic;
            this.expandCollapsePanel4.Controls.Add(this.txtCancelMessage);
            this.expandCollapsePanel4.Controls.Add(this.label9);
            this.expandCollapsePanel4.Controls.Add(this.txtMessage2);
            this.expandCollapsePanel4.Controls.Add(this.label8);
            this.expandCollapsePanel4.Controls.Add(this.txtMessage1);
            this.expandCollapsePanel4.Controls.Add(this.label6);
            this.expandCollapsePanel4.Controls.Add(this.txtGrace);
            this.expandCollapsePanel4.Controls.Add(this.tbGracePeriod);
            this.expandCollapsePanel4.Controls.Add(this.chkSendMessage);
            this.expandCollapsePanel4.Controls.Add(this.label7);
            this.expandCollapsePanel4.Controls.Add(this.chlPerformPlayer);
            this.expandCollapsePanel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.expandCollapsePanel4.ExpandedHeight = 0;
            this.expandCollapsePanel4.IsExpanded = true;
            this.expandCollapsePanel4.Location = new System.Drawing.Point(0, 522);
            this.expandCollapsePanel4.Name = "expandCollapsePanel4";
            this.expandCollapsePanel4.Size = new System.Drawing.Size(800, 174);
            this.expandCollapsePanel4.TabIndex = 4;
            this.expandCollapsePanel4.Text = "Shutdown Options";
            this.expandCollapsePanel4.UseAnimation = false;
            // 
            // txtCancelMessage
            // 
            this.txtCancelMessage.Location = new System.Drawing.Point(116, 141);
            this.txtCancelMessage.Name = "txtCancelMessage";
            this.txtCancelMessage.Size = new System.Drawing.Size(589, 21);
            this.txtCancelMessage.TabIndex = 14;
            this.txtCancelMessage.Text = "Server shutdown has been cancelled.";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(11, 144);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(99, 15);
            this.label9.TabIndex = 13;
            this.label9.Text = "Cancel Message";
            // 
            // txtMessage2
            // 
            this.txtMessage2.Location = new System.Drawing.Point(116, 114);
            this.txtMessage2.Name = "txtMessage2";
            this.txtMessage2.Size = new System.Drawing.Size(589, 21);
            this.txtMessage2.TabIndex = 12;
            this.txtMessage2.Text = "Server shutdown required. Server is about to shutdown, performing a world save.";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(11, 117);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(68, 15);
            this.label8.TabIndex = 11;
            this.label8.Text = "Message 2";
            // 
            // txtMessage1
            // 
            this.txtMessage1.Location = new System.Drawing.Point(116, 90);
            this.txtMessage1.Name = "txtMessage1";
            this.txtMessage1.Size = new System.Drawing.Size(589, 21);
            this.txtMessage1.TabIndex = 10;
            this.txtMessage1.Text = "Server shutdown required. Server will shutdown in {minutes} minutes. Please logou" +
    "t before shutdown to prevent character corruption.";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 93);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 15);
            this.label6.TabIndex = 9;
            this.label6.Text = "Message 1";
            // 
            // txtGrace
            // 
            this.txtGrace.Location = new System.Drawing.Point(425, 63);
            this.txtGrace.Name = "txtGrace";
            this.txtGrace.ReadOnly = true;
            this.txtGrace.Size = new System.Drawing.Size(35, 21);
            this.txtGrace.TabIndex = 8;
            // 
            // tbGracePeriod
            // 
            this.tbGracePeriod.Location = new System.Drawing.Point(116, 65);
            this.tbGracePeriod.Maximum = 60;
            this.tbGracePeriod.Name = "tbGracePeriod";
            this.tbGracePeriod.Size = new System.Drawing.Size(303, 45);
            this.tbGracePeriod.TabIndex = 7;
            this.tbGracePeriod.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbGracePeriod.Value = 15;
            this.tbGracePeriod.Scroll += new System.EventHandler(this.tbGracePeriod_Scroll);
            // 
            // chkSendMessage
            // 
            this.chkSendMessage.AutoSize = true;
            this.chkSendMessage.Checked = true;
            this.chkSendMessage.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSendMessage.Location = new System.Drawing.Point(266, 43);
            this.chkSendMessage.Name = "chkSendMessage";
            this.chkSendMessage.Size = new System.Drawing.Size(264, 19);
            this.chkSendMessage.TabIndex = 6;
            this.chkSendMessage.Text = "Send Shutdown Messages to Game Cliente";
            this.chkSendMessage.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 65);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(79, 15);
            this.label7.TabIndex = 2;
            this.label7.Text = "Grace Period";
            // 
            // chlPerformPlayer
            // 
            this.chlPerformPlayer.AutoSize = true;
            this.chlPerformPlayer.Checked = true;
            this.chlPerformPlayer.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chlPerformPlayer.Location = new System.Drawing.Point(11, 43);
            this.chlPerformPlayer.Name = "chlPerformPlayer";
            this.chlPerformPlayer.Size = new System.Drawing.Size(179, 19);
            this.chlPerformPlayer.TabIndex = 1;
            this.chlPerformPlayer.Text = "Perfom Online Player Check";
            this.chlPerformPlayer.UseVisualStyleBackColor = true;
            // 
            // expandCollapsePanel3
            // 
            this.expandCollapsePanel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.expandCollapsePanel3.ButtonSize = MakarovDev.ExpandCollapsePanel.ExpandCollapseButton.ExpandButtonSize.Small;
            this.expandCollapsePanel3.ButtonStyle = MakarovDev.ExpandCollapsePanel.ExpandCollapseButton.ExpandButtonStyle.Classic;
            this.expandCollapsePanel3.Controls.Add(this.chkUseSmartCopy);
            this.expandCollapsePanel3.Controls.Add(this.txtBackupInterval);
            this.expandCollapsePanel3.Controls.Add(this.label13);
            this.expandCollapsePanel3.Controls.Add(this.chkEnableAutoBackup);
            this.expandCollapsePanel3.Controls.Add(this.txtUpdateInterval);
            this.expandCollapsePanel3.Controls.Add(this.label5);
            this.expandCollapsePanel3.Controls.Add(this.chkAutoUpdate);
            this.expandCollapsePanel3.Controls.Add(this.txtWordSave);
            this.expandCollapsePanel3.Controls.Add(this.label3);
            this.expandCollapsePanel3.Controls.Add(this.txtBackupDays);
            this.expandCollapsePanel3.Controls.Add(this.tbDeleteDays);
            this.expandCollapsePanel3.Controls.Add(this.chkDeleteOld);
            this.expandCollapsePanel3.Controls.Add(this.button1);
            this.expandCollapsePanel3.Controls.Add(this.txtBackup);
            this.expandCollapsePanel3.Controls.Add(this.label4);
            this.expandCollapsePanel3.Controls.Add(this.chkIncludeSaveGames);
            this.expandCollapsePanel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.expandCollapsePanel3.ExpandedHeight = 0;
            this.expandCollapsePanel3.IsExpanded = true;
            this.expandCollapsePanel3.Location = new System.Drawing.Point(0, 269);
            this.expandCollapsePanel3.Name = "expandCollapsePanel3";
            this.expandCollapsePanel3.Size = new System.Drawing.Size(800, 253);
            this.expandCollapsePanel3.TabIndex = 3;
            this.expandCollapsePanel3.Text = "Backup && Update Settings";
            this.expandCollapsePanel3.UseAnimation = false;
            // 
            // chkUseSmartCopy
            // 
            this.chkUseSmartCopy.AutoSize = true;
            this.chkUseSmartCopy.Checked = true;
            this.chkUseSmartCopy.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUseSmartCopy.Location = new System.Drawing.Point(231, 193);
            this.chkUseSmartCopy.Name = "chkUseSmartCopy";
            this.chkUseSmartCopy.Size = new System.Drawing.Size(148, 19);
            this.chkUseSmartCopy.TabIndex = 12;
            this.chkUseSmartCopy.Text = "Use Smart cache copy";
            this.chkUseSmartCopy.UseVisualStyleBackColor = true;
            // 
            // txtBackupInterval
            // 
            this.txtBackupInterval.Location = new System.Drawing.Point(116, 62);
            this.txtBackupInterval.Mask = "00:00";
            this.txtBackupInterval.Name = "txtBackupInterval";
            this.txtBackupInterval.Size = new System.Drawing.Size(100, 21);
            this.txtBackupInterval.TabIndex = 9;
            this.txtBackupInterval.Text = "0100";
            this.txtBackupInterval.ValidatingType = typeof(System.DateTime);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(11, 65);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(90, 15);
            this.label13.TabIndex = 10;
            this.label13.Text = "Backup Interval";
            // 
            // chkEnableAutoBackup
            // 
            this.chkEnableAutoBackup.AutoSize = true;
            this.chkEnableAutoBackup.Checked = true;
            this.chkEnableAutoBackup.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEnableAutoBackup.Location = new System.Drawing.Point(11, 37);
            this.chkEnableAutoBackup.Name = "chkEnableAutoBackup";
            this.chkEnableAutoBackup.Size = new System.Drawing.Size(136, 19);
            this.chkEnableAutoBackup.TabIndex = 11;
            this.chkEnableAutoBackup.Text = "Enable Auto Backup";
            this.chkEnableAutoBackup.UseVisualStyleBackColor = true;
            // 
            // txtUpdateInterval
            // 
            this.txtUpdateInterval.Location = new System.Drawing.Point(116, 218);
            this.txtUpdateInterval.Mask = "00:00";
            this.txtUpdateInterval.Name = "txtUpdateInterval";
            this.txtUpdateInterval.Size = new System.Drawing.Size(100, 21);
            this.txtUpdateInterval.TabIndex = 4;
            this.txtUpdateInterval.Text = "0100";
            this.txtUpdateInterval.ValidatingType = typeof(System.DateTime);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 221);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 15);
            this.label5.TabIndex = 6;
            this.label5.Text = "Update Interval";
            // 
            // chkAutoUpdate
            // 
            this.chkAutoUpdate.AutoSize = true;
            this.chkAutoUpdate.Checked = true;
            this.chkAutoUpdate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoUpdate.Location = new System.Drawing.Point(11, 193);
            this.chkAutoUpdate.Name = "chkAutoUpdate";
            this.chkAutoUpdate.Size = new System.Drawing.Size(135, 19);
            this.chkAutoUpdate.TabIndex = 8;
            this.chkAutoUpdate.Text = "Enable Auto Update";
            this.chkAutoUpdate.UseVisualStyleBackColor = true;
            // 
            // txtWordSave
            // 
            this.txtWordSave.Location = new System.Drawing.Point(131, 166);
            this.txtWordSave.Name = "txtWordSave";
            this.txtWordSave.Size = new System.Drawing.Size(615, 21);
            this.txtWordSave.TabIndex = 7;
            this.txtWordSave.Text = "A world save is about to be performed, you may experience some lag during this pr" +
    "ocess. Please be patient.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 169);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 15);
            this.label3.TabIndex = 6;
            this.label3.Text = "WorldSave Message";
            // 
            // txtBackupDays
            // 
            this.txtBackupDays.Location = new System.Drawing.Point(711, 139);
            this.txtBackupDays.Name = "txtBackupDays";
            this.txtBackupDays.ReadOnly = true;
            this.txtBackupDays.Size = new System.Drawing.Size(35, 21);
            this.txtBackupDays.TabIndex = 5;
            // 
            // tbDeleteDays
            // 
            this.tbDeleteDays.Location = new System.Drawing.Point(402, 141);
            this.tbDeleteDays.Maximum = 90;
            this.tbDeleteDays.Name = "tbDeleteDays";
            this.tbDeleteDays.Size = new System.Drawing.Size(303, 45);
            this.tbDeleteDays.TabIndex = 4;
            this.tbDeleteDays.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbDeleteDays.Value = 15;
            this.tbDeleteDays.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // chkDeleteOld
            // 
            this.chkDeleteOld.AutoSize = true;
            this.chkDeleteOld.Checked = true;
            this.chkDeleteOld.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDeleteOld.Location = new System.Drawing.Point(11, 141);
            this.chkDeleteOld.Name = "chkDeleteOld";
            this.chkDeleteOld.Size = new System.Drawing.Size(151, 19);
            this.chkDeleteOld.TabIndex = 4;
            this.chkDeleteOld.Text = "Delete old Backup files";
            this.chkDeleteOld.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(711, 88);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(35, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtBackup
            // 
            this.txtBackup.Location = new System.Drawing.Point(116, 89);
            this.txtBackup.Name = "txtBackup";
            this.txtBackup.Size = new System.Drawing.Size(589, 21);
            this.txtBackup.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 15);
            this.label4.TabIndex = 2;
            this.label4.Text = "Backup Directory";
            // 
            // chkIncludeSaveGames
            // 
            this.chkIncludeSaveGames.AutoSize = true;
            this.chkIncludeSaveGames.Checked = true;
            this.chkIncludeSaveGames.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIncludeSaveGames.Location = new System.Drawing.Point(11, 116);
            this.chkIncludeSaveGames.Name = "chkIncludeSaveGames";
            this.chkIncludeSaveGames.Size = new System.Drawing.Size(385, 19);
            this.chkIncludeSaveGames.TabIndex = 1;
            this.chkIncludeSaveGames.Text = "Include SaveGames Folder when performing a WorldSave backup";
            this.chkIncludeSaveGames.UseVisualStyleBackColor = true;
            // 
            // expandCollapsePanel2
            // 
            this.expandCollapsePanel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.expandCollapsePanel2.ButtonSize = MakarovDev.ExpandCollapsePanel.ExpandCollapseButton.ExpandButtonSize.Small;
            this.expandCollapsePanel2.ButtonStyle = MakarovDev.ExpandCollapsePanel.ExpandCollapseButton.ExpandButtonStyle.Classic;
            this.expandCollapsePanel2.Controls.Add(this.chkUpdateMods);
            this.expandCollapsePanel2.Controls.Add(this.chkPerform);
            this.expandCollapsePanel2.Controls.Add(this.chkValidate);
            this.expandCollapsePanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.expandCollapsePanel2.ExpandedHeight = 0;
            this.expandCollapsePanel2.IsExpanded = true;
            this.expandCollapsePanel2.Location = new System.Drawing.Point(0, 179);
            this.expandCollapsePanel2.Name = "expandCollapsePanel2";
            this.expandCollapsePanel2.Size = new System.Drawing.Size(800, 90);
            this.expandCollapsePanel2.TabIndex = 2;
            this.expandCollapsePanel2.Text = "Server Sartup Options";
            this.expandCollapsePanel2.UseAnimation = false;
            // 
            // chkUpdateMods
            // 
            this.chkUpdateMods.AutoSize = true;
            this.chkUpdateMods.Checked = true;
            this.chkUpdateMods.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUpdateMods.Location = new System.Drawing.Point(11, 66);
            this.chkUpdateMods.Name = "chkUpdateMods";
            this.chkUpdateMods.Size = new System.Drawing.Size(220, 19);
            this.chkUpdateMods.TabIndex = 3;
            this.chkUpdateMods.Text = "Update mods when updating server";
            this.chkUpdateMods.UseVisualStyleBackColor = true;
            // 
            // chkPerform
            // 
            this.chkPerform.AutoSize = true;
            this.chkPerform.Location = new System.Drawing.Point(266, 41);
            this.chkPerform.Name = "chkPerform";
            this.chkPerform.Size = new System.Drawing.Size(278, 19);
            this.chkPerform.TabIndex = 2;
            this.chkPerform.Text = "Perform server and mod update on server start";
            this.chkPerform.UseVisualStyleBackColor = true;
            // 
            // chkValidate
            // 
            this.chkValidate.AutoSize = true;
            this.chkValidate.Checked = true;
            this.chkValidate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkValidate.Location = new System.Drawing.Point(11, 41);
            this.chkValidate.Name = "chkValidate";
            this.chkValidate.Size = new System.Drawing.Size(188, 19);
            this.chkValidate.TabIndex = 1;
            this.chkValidate.Text = "Validate profile on Server start";
            this.chkValidate.UseVisualStyleBackColor = true;
            // 
            // expandCollapsePanel1
            // 
            this.expandCollapsePanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.expandCollapsePanel1.ButtonSize = MakarovDev.ExpandCollapsePanel.ExpandCollapseButton.ExpandButtonSize.Small;
            this.expandCollapsePanel1.ButtonStyle = MakarovDev.ExpandCollapsePanel.ExpandCollapseButton.ExpandButtonStyle.Classic;
            this.expandCollapsePanel1.Controls.Add(this.chkUpdateOnStart);
            this.expandCollapsePanel1.Controls.Add(this.txtPassword);
            this.expandCollapsePanel1.Controls.Add(this.label2);
            this.expandCollapsePanel1.Controls.Add(this.txtUserName);
            this.expandCollapsePanel1.Controls.Add(this.label1);
            this.expandCollapsePanel1.Controls.Add(this.chkAnonymous);
            this.expandCollapsePanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.expandCollapsePanel1.ExpandedHeight = 0;
            this.expandCollapsePanel1.IsExpanded = true;
            this.expandCollapsePanel1.Location = new System.Drawing.Point(0, 89);
            this.expandCollapsePanel1.Name = "expandCollapsePanel1";
            this.expandCollapsePanel1.Size = new System.Drawing.Size(800, 90);
            this.expandCollapsePanel1.TabIndex = 1;
            this.expandCollapsePanel1.Text = "Steam Settings";
            this.expandCollapsePanel1.UseAnimation = false;
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
            this.txtPassword.Location = new System.Drawing.Point(266, 62);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(100, 21);
            this.txtPassword.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(192, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "Password:";
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(85, 62);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(100, 21);
            this.txtUserName.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Username:";
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
            this.chkAnonymous.CheckedChanged += new System.EventHandler(this.chkAnonymous_CheckedChanged);
            // 
            // expandCollapsePanel5
            // 
            this.expandCollapsePanel5.ButtonSize = MakarovDev.ExpandCollapsePanel.ExpandCollapseButton.ExpandButtonSize.Normal;
            this.expandCollapsePanel5.ButtonStyle = MakarovDev.ExpandCollapsePanel.ExpandCollapseButton.ExpandButtonStyle.Circle;
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
            this.expandCollapsePanel5.Location = new System.Drawing.Point(0, 696);
            this.expandCollapsePanel5.Name = "expandCollapsePanel5";
            this.expandCollapsePanel5.Size = new System.Drawing.Size(800, 122);
            this.expandCollapsePanel5.TabIndex = 5;
            this.expandCollapsePanel5.Text = "Logging";
            this.expandCollapsePanel5.UseAnimation = true;
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
            // txtMaxDays
            // 
            this.txtMaxDays.Location = new System.Drawing.Point(140, 65);
            this.txtMaxDays.Name = "txtMaxDays";
            this.txtMaxDays.Size = new System.Drawing.Size(69, 20);
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
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(215, 68);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(32, 15);
            this.label15.TabIndex = 17;
            this.label15.Text = "days";
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
            this.txtMaxFiles.Size = new System.Drawing.Size(69, 20);
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
            // FrmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(800, 902);
            this.Controls.Add(this.expandCollapsePanel5);
            this.Controls.Add(this.expandCollapsePanel4);
            this.Controls.Add(this.expandCollapsePanel3);
            this.Controls.Add(this.expandCollapsePanel2);
            this.Controls.Add(this.expandCollapsePanel1);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.Color.SteelBlue;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmSettings_FormClosing);
            this.Load += new System.EventHandler(this.Settings_Load);
            this.Leave += new System.EventHandler(this.FrmSettings_Leave);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.expandCollapsePanel4.ResumeLayout(false);
            this.expandCollapsePanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbGracePeriod)).EndInit();
            this.expandCollapsePanel3.ResumeLayout(false);
            this.expandCollapsePanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbDeleteDays)).EndInit();
            this.expandCollapsePanel2.ResumeLayout(false);
            this.expandCollapsePanel2.PerformLayout();
            this.expandCollapsePanel1.ResumeLayout(false);
            this.expandCollapsePanel1.PerformLayout();
            this.expandCollapsePanel5.ResumeLayout(false);
            this.expandCollapsePanel5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private MakarovDev.ExpandCollapsePanel.ExpandCollapsePanel expandCollapsePanel1;
        private System.Windows.Forms.CheckBox chkAnonymous;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label2;
        private MakarovDev.ExpandCollapsePanel.ExpandCollapsePanel expandCollapsePanel2;
        private System.Windows.Forms.CheckBox chkValidate;
        private System.Windows.Forms.CheckBox chkPerform;
        private System.Windows.Forms.CheckBox chkUpdateMods;
        private MakarovDev.ExpandCollapsePanel.ExpandCollapsePanel expandCollapsePanel3;
        private System.Windows.Forms.TextBox txtBackup;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkIncludeSaveGames;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox chkDeleteOld;
        private System.Windows.Forms.TrackBar tbDeleteDays;
        private System.Windows.Forms.TextBox txtBackupDays;
        private System.Windows.Forms.TextBox txtWordSave;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkAutoUpdate;
        private System.Windows.Forms.MaskedTextBox txtUpdateInterval;
        private MakarovDev.ExpandCollapsePanel.ExpandCollapsePanel expandCollapsePanel4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox chlPerformPlayer;
        private System.Windows.Forms.TextBox txtGrace;
        private System.Windows.Forms.TrackBar tbGracePeriod;
        private System.Windows.Forms.CheckBox chkSendMessage;
        private System.Windows.Forms.TextBox txtMessage1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtCancelMessage;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtMessage2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtSteamCmd;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtDataFolder;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtInstallFolder;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.CheckBox chkUpdateOnStart;
        private System.Windows.Forms.MaskedTextBox txtBackupInterval;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.CheckBox chkEnableAutoBackup;
        private System.Windows.Forms.CheckBox chkUseSmartCopy;
        private MakarovDev.ExpandCollapsePanel.ExpandCollapsePanel expandCollapsePanel5;
        private System.Windows.Forms.CheckBox chkEnableLogs;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtMaxFiles;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtMaxDays;
        private System.Windows.Forms.Label label14;
    }
}