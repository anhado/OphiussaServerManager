namespace OphiussaServerManager.Forms
{
    partial class FrmValheim
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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtBuild = new System.Windows.Forms.TextBox();
            this.label35 = new System.Windows.Forms.Label();
            this.txtVersion = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtServerType = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btStart = new System.Windows.Forms.Button();
            this.btUpdate = new System.Windows.Forms.Button();
            this.btChooseFolder = new System.Windows.Forms.Button();
            this.txtLocation = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btSave = new System.Windows.Forms.Button();
            this.btSync = new System.Windows.Forms.Button();
            this.txtProfileName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtProfileID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.fbd = new System.Windows.Forms.FolderBrowserDialog();
            this.timerGetProcess = new System.Windows.Forms.Timer(this.components);
            this.expandCollapsePanel4 = new MakarovDev.ExpandCollapsePanel.ExpandCollapsePanel();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.chkRestart2 = new System.Windows.Forms.CheckBox();
            this.chkRestart1 = new System.Windows.Forms.CheckBox();
            this.chkUpdate1 = new System.Windows.Forms.CheckBox();
            this.chkUpdate2 = new System.Windows.Forms.CheckBox();
            this.chkSat1 = new System.Windows.Forms.CheckBox();
            this.chkFri1 = new System.Windows.Forms.CheckBox();
            this.chkSat2 = new System.Windows.Forms.CheckBox();
            this.chkThu1 = new System.Windows.Forms.CheckBox();
            this.txtShutdow2 = new System.Windows.Forms.MaskedTextBox();
            this.chkWed1 = new System.Windows.Forms.CheckBox();
            this.chkFri2 = new System.Windows.Forms.CheckBox();
            this.chkTue1 = new System.Windows.Forms.CheckBox();
            this.chkShutdown2 = new System.Windows.Forms.CheckBox();
            this.chkMon1 = new System.Windows.Forms.CheckBox();
            this.chkThu2 = new System.Windows.Forms.CheckBox();
            this.chkSun1 = new System.Windows.Forms.CheckBox();
            this.chkSun2 = new System.Windows.Forms.CheckBox();
            this.txtShutdow1 = new System.Windows.Forms.MaskedTextBox();
            this.chkWed2 = new System.Windows.Forms.CheckBox();
            this.chkRestartIfShutdown = new System.Windows.Forms.CheckBox();
            this.chkMon2 = new System.Windows.Forms.CheckBox();
            this.chkAutoUpdate = new System.Windows.Forms.CheckBox();
            this.chkTue2 = new System.Windows.Forms.CheckBox();
            this.chkIncludeAutoBackup = new System.Windows.Forms.CheckBox();
            this.chkShutdown1 = new System.Windows.Forms.CheckBox();
            this.rbOnLogin = new System.Windows.Forms.RadioButton();
            this.rbOnBoot = new System.Windows.Forms.RadioButton();
            this.chkAutoStart = new System.Windows.Forms.CheckBox();
            this.expandCollapsePanel2 = new MakarovDev.ExpandCollapsePanel.ExpandCollapsePanel();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.txtSubBackups = new System.Windows.Forms.TextBox();
            this.tbSubBackups = new System.Windows.Forms.TrackBar();
            this.label17 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txtFirstBackup = new System.Windows.Forms.TextBox();
            this.tbFirstBackup = new System.Windows.Forms.TrackBar();
            this.label14 = new System.Windows.Forms.Label();
            this.txtBackupToKeep = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtLogLocation = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtAutoSavePeriod = new System.Windows.Forms.TextBox();
            this.tbAutoSavePeriod = new System.Windows.Forms.TrackBar();
            this.txtSaveLocation = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.expandCollapsePanel3 = new MakarovDev.ExpandCollapsePanel.ExpandCollapsePanel();
            this.groupBox13 = new System.Windows.Forms.GroupBox();
            this.rbRaidsDefault = new System.Windows.Forms.RadioButton();
            this.rbRaidsMuchLess = new System.Windows.Forms.RadioButton();
            this.rbRaidsNone = new System.Windows.Forms.RadioButton();
            this.rbRaidsMuchMore = new System.Windows.Forms.RadioButton();
            this.rbRaidsMore = new System.Windows.Forms.RadioButton();
            this.rbRaidsLess = new System.Windows.Forms.RadioButton();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.rbPortalsNone = new System.Windows.Forms.RadioButton();
            this.rbPortalsVeryHard = new System.Windows.Forms.RadioButton();
            this.rbPortalsHard = new System.Windows.Forms.RadioButton();
            this.rbPortalsCasual = new System.Windows.Forms.RadioButton();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.rbResourcesNone = new System.Windows.Forms.RadioButton();
            this.rbResourcesMuchLess = new System.Windows.Forms.RadioButton();
            this.rbResourcesMost = new System.Windows.Forms.RadioButton();
            this.rbResourcesMuchMore = new System.Windows.Forms.RadioButton();
            this.rbResourcesMore = new System.Windows.Forms.RadioButton();
            this.rbResourcesLess = new System.Windows.Forms.RadioButton();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.rbDeathPenaltyCasual = new System.Windows.Forms.RadioButton();
            this.rbDeathPenaltyHardCore = new System.Windows.Forms.RadioButton();
            this.rbDeathPenaltyHard = new System.Windows.Forms.RadioButton();
            this.rbDeathPenaltyEasy = new System.Windows.Forms.RadioButton();
            this.rbDeathPenaltyVeryEasy = new System.Windows.Forms.RadioButton();
            this.rbDeathPenaltyNone = new System.Windows.Forms.RadioButton();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.rbCombatVeryHard = new System.Windows.Forms.RadioButton();
            this.rbCombatHard = new System.Windows.Forms.RadioButton();
            this.rbCombatEasy = new System.Windows.Forms.RadioButton();
            this.rbCombatVeryEasy = new System.Windows.Forms.RadioButton();
            this.rbCombatNone = new System.Windows.Forms.RadioButton();
            this.expandCollapsePanel1 = new MakarovDev.ExpandCollapsePanel.ExpandCollapsePanel();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.txtCommand = new System.Windows.Forms.TextBox();
            this.btProcessorAffinity = new System.Windows.Forms.Button();
            this.txtAffinity = new System.Windows.Forms.TextBox();
            this.label34 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.cboPriority = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.tbPresetHammer = new System.Windows.Forms.RadioButton();
            this.tbPresetCasual = new System.Windows.Forms.RadioButton();
            this.tbPresetImmersive = new System.Windows.Forms.RadioButton();
            this.tbPresetHardcore = new System.Windows.Forms.RadioButton();
            this.tbPresetHard = new System.Windows.Forms.RadioButton();
            this.tbPresetEasy = new System.Windows.Forms.RadioButton();
            this.tbPresetNormal = new System.Windows.Forms.RadioButton();
            this.txtWorldName = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.cLBKeys = new System.Windows.Forms.CheckedListBox();
            this.txtInstanceID = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.chkCrossplay = new System.Windows.Forms.CheckBox();
            this.chkPublic = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtLocalIP = new System.Windows.Forms.ComboBox();
            this.txtPeerPort = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtServerPort = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtServerPWD = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtServerName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cbBranch = new System.Windows.Forms.ComboBox();
            this.label22 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.expandCollapsePanel4.SuspendLayout();
            this.groupBox11.SuspendLayout();
            this.expandCollapsePanel2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbSubBackups)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbFirstBackup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbAutoSavePeriod)).BeginInit();
            this.expandCollapsePanel3.SuspendLayout();
            this.groupBox13.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.expandCollapsePanel1.SuspendLayout();
            this.groupBox12.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.txtBuild);
            this.panel1.Controls.Add(this.label35);
            this.panel1.Controls.Add(this.txtVersion);
            this.panel1.Controls.Add(this.label16);
            this.panel1.Controls.Add(this.txtServerType);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.btStart);
            this.panel1.Controls.Add(this.btUpdate);
            this.panel1.Controls.Add(this.btChooseFolder);
            this.panel1.Controls.Add(this.txtLocation);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.btSave);
            this.panel1.Controls.Add(this.btSync);
            this.panel1.Controls.Add(this.txtProfileName);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtProfileID);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(868, 147);
            this.panel1.TabIndex = 2;
            // 
            // txtBuild
            // 
            this.txtBuild.Location = new System.Drawing.Point(303, 112);
            this.txtBuild.Name = "txtBuild";
            this.txtBuild.ReadOnly = true;
            this.txtBuild.Size = new System.Drawing.Size(80, 20);
            this.txtBuild.TabIndex = 18;
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(214, 115);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(68, 13);
            this.label35.TabIndex = 17;
            this.label35.Text = "Build Version";
            // 
            // txtVersion
            // 
            this.txtVersion.Location = new System.Drawing.Point(108, 112);
            this.txtVersion.Name = "txtVersion";
            this.txtVersion.ReadOnly = true;
            this.txtVersion.Size = new System.Drawing.Size(80, 20);
            this.txtVersion.TabIndex = 16;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(13, 115);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(84, 13);
            this.label16.TabIndex = 15;
            this.label16.Text = "Installed Version";
            // 
            // txtServerType
            // 
            this.txtServerType.Location = new System.Drawing.Point(108, 60);
            this.txtServerType.Name = "txtServerType";
            this.txtServerType.ReadOnly = true;
            this.txtServerType.Size = new System.Drawing.Size(246, 20);
            this.txtServerType.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Server Type";
            // 
            // btStart
            // 
            this.btStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btStart.Location = new System.Drawing.Point(698, 60);
            this.btStart.Name = "btStart";
            this.btStart.Size = new System.Drawing.Size(156, 23);
            this.btStart.TabIndex = 10;
            this.btStart.Text = "Start";
            this.btStart.UseVisualStyleBackColor = true;
            this.btStart.Click += new System.EventHandler(this.btStart_Click);
            // 
            // btUpdate
            // 
            this.btUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btUpdate.Image = global::OphiussaServerManager.Properties.Resources.upgrade__misc_icon_icon;
            this.btUpdate.Location = new System.Drawing.Point(698, 34);
            this.btUpdate.Name = "btUpdate";
            this.btUpdate.Size = new System.Drawing.Size(156, 23);
            this.btUpdate.TabIndex = 9;
            this.btUpdate.Text = "Update/Verify";
            this.btUpdate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btUpdate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btUpdate.UseVisualStyleBackColor = true;
            this.btUpdate.Click += new System.EventHandler(this.btUpdate_Click);
            // 
            // btChooseFolder
            // 
            this.btChooseFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btChooseFolder.Location = new System.Drawing.Point(662, 84);
            this.btChooseFolder.Name = "btChooseFolder";
            this.btChooseFolder.Size = new System.Drawing.Size(22, 23);
            this.btChooseFolder.TabIndex = 8;
            this.btChooseFolder.Text = "...";
            this.btChooseFolder.UseVisualStyleBackColor = true;
            // 
            // txtLocation
            // 
            this.txtLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLocation.Location = new System.Drawing.Point(108, 86);
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.ReadOnly = true;
            this.txtLocation.Size = new System.Drawing.Size(546, 20);
            this.txtLocation.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Installation Folder";
            // 
            // btSave
            // 
            this.btSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btSave.Image = global::OphiussaServerManager.Properties.Resources.save_16x16;
            this.btSave.Location = new System.Drawing.Point(779, 8);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(75, 23);
            this.btSave.TabIndex = 5;
            this.btSave.Text = "Save";
            this.btSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // btSync
            // 
            this.btSync.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btSync.Image = global::OphiussaServerManager.Properties.Resources.Copy_icon_icon;
            this.btSync.Location = new System.Drawing.Point(698, 8);
            this.btSync.Name = "btSync";
            this.btSync.Size = new System.Drawing.Size(75, 23);
            this.btSync.TabIndex = 4;
            this.btSync.Text = "Sync";
            this.btSync.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btSync.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btSync.UseVisualStyleBackColor = true;
            // 
            // txtProfileName
            // 
            this.txtProfileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtProfileName.Location = new System.Drawing.Point(108, 36);
            this.txtProfileName.Name = "txtProfileName";
            this.txtProfileName.Size = new System.Drawing.Size(576, 20);
            this.txtProfileName.TabIndex = 3;
            this.txtProfileName.Validated += new System.EventHandler(this.txtProfileName_Validated);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Profile Name";
            // 
            // txtProfileID
            // 
            this.txtProfileID.Location = new System.Drawing.Point(108, 10);
            this.txtProfileID.Name = "txtProfileID";
            this.txtProfileID.ReadOnly = true;
            this.txtProfileID.Size = new System.Drawing.Size(246, 20);
            this.txtProfileID.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Profile ID";
            // 
            // timerGetProcess
            // 
            this.timerGetProcess.Enabled = true;
            this.timerGetProcess.Interval = 500;
            this.timerGetProcess.Tick += new System.EventHandler(this.timerGetProcess_Tick);
            // 
            // expandCollapsePanel4
            // 
            this.expandCollapsePanel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.expandCollapsePanel4.ButtonSize = MakarovDev.ExpandCollapsePanel.ExpandCollapseButton.ExpandButtonSize.Small;
            this.expandCollapsePanel4.ButtonStyle = MakarovDev.ExpandCollapsePanel.ExpandCollapseButton.ExpandButtonStyle.Classic;
            this.expandCollapsePanel4.Controls.Add(this.groupBox11);
            this.expandCollapsePanel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.expandCollapsePanel4.ExpandedHeight = 800;
            this.expandCollapsePanel4.IsExpanded = true;
            this.expandCollapsePanel4.Location = new System.Drawing.Point(0, 1344);
            this.expandCollapsePanel4.Name = "expandCollapsePanel4";
            this.expandCollapsePanel4.Size = new System.Drawing.Size(868, 226);
            this.expandCollapsePanel4.TabIndex = 22;
            this.expandCollapsePanel4.Text = "Automatic Management";
            this.expandCollapsePanel4.UseAnimation = false;
            // 
            // groupBox11
            // 
            this.groupBox11.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox11.Controls.Add(this.chkRestart2);
            this.groupBox11.Controls.Add(this.chkRestart1);
            this.groupBox11.Controls.Add(this.chkUpdate1);
            this.groupBox11.Controls.Add(this.chkUpdate2);
            this.groupBox11.Controls.Add(this.chkSat1);
            this.groupBox11.Controls.Add(this.chkFri1);
            this.groupBox11.Controls.Add(this.chkSat2);
            this.groupBox11.Controls.Add(this.chkThu1);
            this.groupBox11.Controls.Add(this.txtShutdow2);
            this.groupBox11.Controls.Add(this.chkWed1);
            this.groupBox11.Controls.Add(this.chkFri2);
            this.groupBox11.Controls.Add(this.chkTue1);
            this.groupBox11.Controls.Add(this.chkShutdown2);
            this.groupBox11.Controls.Add(this.chkMon1);
            this.groupBox11.Controls.Add(this.chkThu2);
            this.groupBox11.Controls.Add(this.chkSun1);
            this.groupBox11.Controls.Add(this.chkSun2);
            this.groupBox11.Controls.Add(this.txtShutdow1);
            this.groupBox11.Controls.Add(this.chkWed2);
            this.groupBox11.Controls.Add(this.chkRestartIfShutdown);
            this.groupBox11.Controls.Add(this.chkMon2);
            this.groupBox11.Controls.Add(this.chkAutoUpdate);
            this.groupBox11.Controls.Add(this.chkTue2);
            this.groupBox11.Controls.Add(this.chkIncludeAutoBackup);
            this.groupBox11.Controls.Add(this.chkShutdown1);
            this.groupBox11.Controls.Add(this.rbOnLogin);
            this.groupBox11.Controls.Add(this.rbOnBoot);
            this.groupBox11.Controls.Add(this.chkAutoStart);
            this.groupBox11.Location = new System.Drawing.Point(11, 34);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(844, 181);
            this.groupBox11.TabIndex = 1;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "Server Manager Settings";
            // 
            // chkRestart2
            // 
            this.chkRestart2.AutoSize = true;
            this.chkRestart2.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkRestart2.Enabled = false;
            this.chkRestart2.Location = new System.Drawing.Point(578, 91);
            this.chkRestart2.Name = "chkRestart2";
            this.chkRestart2.Size = new System.Drawing.Size(80, 19);
            this.chkRestart2.TabIndex = 29;
            this.chkRestart2.Text = "the restart";
            this.chkRestart2.UseVisualStyleBackColor = true;
            // 
            // chkRestart1
            // 
            this.chkRestart1.AutoSize = true;
            this.chkRestart1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkRestart1.Enabled = false;
            this.chkRestart1.Location = new System.Drawing.Point(578, 52);
            this.chkRestart1.Name = "chkRestart1";
            this.chkRestart1.Size = new System.Drawing.Size(80, 19);
            this.chkRestart1.TabIndex = 18;
            this.chkRestart1.Text = "the restart";
            this.chkRestart1.UseVisualStyleBackColor = true;
            // 
            // chkUpdate1
            // 
            this.chkUpdate1.AutoSize = true;
            this.chkUpdate1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkUpdate1.Enabled = false;
            this.chkUpdate1.Location = new System.Drawing.Point(461, 52);
            this.chkUpdate1.Name = "chkUpdate1";
            this.chkUpdate1.Size = new System.Drawing.Size(111, 19);
            this.chkUpdate1.TabIndex = 17;
            this.chkUpdate1.Text = "Perform update";
            this.chkUpdate1.UseVisualStyleBackColor = true;
            // 
            // chkUpdate2
            // 
            this.chkUpdate2.AutoSize = true;
            this.chkUpdate2.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkUpdate2.Enabled = false;
            this.chkUpdate2.Location = new System.Drawing.Point(461, 91);
            this.chkUpdate2.Name = "chkUpdate2";
            this.chkUpdate2.Size = new System.Drawing.Size(111, 19);
            this.chkUpdate2.TabIndex = 28;
            this.chkUpdate2.Text = "Perform update";
            this.chkUpdate2.UseVisualStyleBackColor = true;
            // 
            // chkSat1
            // 
            this.chkSat1.AutoSize = true;
            this.chkSat1.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.chkSat1.Enabled = false;
            this.chkSat1.Location = new System.Drawing.Point(426, 38);
            this.chkSat1.Name = "chkSat1";
            this.chkSat1.Size = new System.Drawing.Size(29, 33);
            this.chkSat1.TabIndex = 16;
            this.chkSat1.Text = "Sat";
            this.chkSat1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkSat1.UseVisualStyleBackColor = true;
            // 
            // chkFri1
            // 
            this.chkFri1.AutoSize = true;
            this.chkFri1.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.chkFri1.Enabled = false;
            this.chkFri1.Location = new System.Drawing.Point(395, 38);
            this.chkFri1.Name = "chkFri1";
            this.chkFri1.Size = new System.Drawing.Size(25, 33);
            this.chkFri1.TabIndex = 15;
            this.chkFri1.Text = "Fri";
            this.chkFri1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkFri1.UseVisualStyleBackColor = true;
            // 
            // chkSat2
            // 
            this.chkSat2.AutoSize = true;
            this.chkSat2.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.chkSat2.Enabled = false;
            this.chkSat2.Location = new System.Drawing.Point(426, 77);
            this.chkSat2.Name = "chkSat2";
            this.chkSat2.Size = new System.Drawing.Size(29, 33);
            this.chkSat2.TabIndex = 27;
            this.chkSat2.Text = "Sat";
            this.chkSat2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkSat2.UseVisualStyleBackColor = true;
            // 
            // chkThu1
            // 
            this.chkThu1.AutoSize = true;
            this.chkThu1.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.chkThu1.Enabled = false;
            this.chkThu1.Location = new System.Drawing.Point(357, 38);
            this.chkThu1.Name = "chkThu1";
            this.chkThu1.Size = new System.Drawing.Size(32, 33);
            this.chkThu1.TabIndex = 14;
            this.chkThu1.Text = "Thu";
            this.chkThu1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkThu1.UseVisualStyleBackColor = true;
            // 
            // txtShutdow2
            // 
            this.txtShutdow2.Enabled = false;
            this.txtShutdow2.Location = new System.Drawing.Point(142, 89);
            this.txtShutdow2.Mask = "00:00";
            this.txtShutdow2.Name = "txtShutdow2";
            this.txtShutdow2.Size = new System.Drawing.Size(59, 21);
            this.txtShutdow2.TabIndex = 20;
            this.txtShutdow2.Text = "0100";
            this.txtShutdow2.ValidatingType = typeof(System.DateTime);
            // 
            // chkWed1
            // 
            this.chkWed1.AutoSize = true;
            this.chkWed1.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.chkWed1.Enabled = false;
            this.chkWed1.Location = new System.Drawing.Point(315, 38);
            this.chkWed1.Name = "chkWed1";
            this.chkWed1.Size = new System.Drawing.Size(36, 33);
            this.chkWed1.TabIndex = 13;
            this.chkWed1.Text = "Wed";
            this.chkWed1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkWed1.UseVisualStyleBackColor = true;
            // 
            // chkFri2
            // 
            this.chkFri2.AutoSize = true;
            this.chkFri2.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.chkFri2.Enabled = false;
            this.chkFri2.Location = new System.Drawing.Point(395, 77);
            this.chkFri2.Name = "chkFri2";
            this.chkFri2.Size = new System.Drawing.Size(25, 33);
            this.chkFri2.TabIndex = 26;
            this.chkFri2.Text = "Fri";
            this.chkFri2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkFri2.UseVisualStyleBackColor = true;
            // 
            // chkTue1
            // 
            this.chkTue1.AutoSize = true;
            this.chkTue1.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.chkTue1.Enabled = false;
            this.chkTue1.Location = new System.Drawing.Point(277, 38);
            this.chkTue1.Name = "chkTue1";
            this.chkTue1.Size = new System.Drawing.Size(32, 33);
            this.chkTue1.TabIndex = 12;
            this.chkTue1.Text = "Tue";
            this.chkTue1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkTue1.UseVisualStyleBackColor = true;
            // 
            // chkShutdown2
            // 
            this.chkShutdown2.AutoSize = true;
            this.chkShutdown2.Location = new System.Drawing.Point(6, 91);
            this.chkShutdown2.Name = "chkShutdown2";
            this.chkShutdown2.Size = new System.Drawing.Size(123, 19);
            this.chkShutdown2.TabIndex = 19;
            this.chkShutdown2.Text = "Shutdow server at";
            this.chkShutdown2.UseVisualStyleBackColor = true;
            this.chkShutdown2.CheckedChanged += new System.EventHandler(this.chkShutdown2_CheckedChanged);
            // 
            // chkMon1
            // 
            this.chkMon1.AutoSize = true;
            this.chkMon1.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.chkMon1.Enabled = false;
            this.chkMon1.Location = new System.Drawing.Point(235, 38);
            this.chkMon1.Name = "chkMon1";
            this.chkMon1.Size = new System.Drawing.Size(36, 33);
            this.chkMon1.TabIndex = 11;
            this.chkMon1.Text = "Mon";
            this.chkMon1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkMon1.UseVisualStyleBackColor = true;
            // 
            // chkThu2
            // 
            this.chkThu2.AutoSize = true;
            this.chkThu2.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.chkThu2.Enabled = false;
            this.chkThu2.Location = new System.Drawing.Point(357, 77);
            this.chkThu2.Name = "chkThu2";
            this.chkThu2.Size = new System.Drawing.Size(32, 33);
            this.chkThu2.TabIndex = 25;
            this.chkThu2.Text = "Thu";
            this.chkThu2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkThu2.UseVisualStyleBackColor = true;
            // 
            // chkSun1
            // 
            this.chkSun1.AutoSize = true;
            this.chkSun1.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.chkSun1.Enabled = false;
            this.chkSun1.Location = new System.Drawing.Point(207, 38);
            this.chkSun1.Name = "chkSun1";
            this.chkSun1.Size = new System.Drawing.Size(33, 33);
            this.chkSun1.TabIndex = 10;
            this.chkSun1.Text = "Sun";
            this.chkSun1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkSun1.UseVisualStyleBackColor = true;
            // 
            // chkSun2
            // 
            this.chkSun2.AutoSize = true;
            this.chkSun2.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.chkSun2.Enabled = false;
            this.chkSun2.Location = new System.Drawing.Point(207, 77);
            this.chkSun2.Name = "chkSun2";
            this.chkSun2.Size = new System.Drawing.Size(33, 33);
            this.chkSun2.TabIndex = 21;
            this.chkSun2.Text = "Sun";
            this.chkSun2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkSun2.UseVisualStyleBackColor = true;
            // 
            // txtShutdow1
            // 
            this.txtShutdow1.Enabled = false;
            this.txtShutdow1.Location = new System.Drawing.Point(142, 50);
            this.txtShutdow1.Mask = "00:00";
            this.txtShutdow1.Name = "txtShutdow1";
            this.txtShutdow1.Size = new System.Drawing.Size(59, 21);
            this.txtShutdow1.TabIndex = 9;
            this.txtShutdow1.Text = "0100";
            this.txtShutdow1.ValidatingType = typeof(System.DateTime);
            // 
            // chkWed2
            // 
            this.chkWed2.AutoSize = true;
            this.chkWed2.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.chkWed2.Enabled = false;
            this.chkWed2.Location = new System.Drawing.Point(315, 77);
            this.chkWed2.Name = "chkWed2";
            this.chkWed2.Size = new System.Drawing.Size(36, 33);
            this.chkWed2.TabIndex = 24;
            this.chkWed2.Text = "Wed";
            this.chkWed2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkWed2.UseVisualStyleBackColor = true;
            // 
            // chkRestartIfShutdown
            // 
            this.chkRestartIfShutdown.AutoSize = true;
            this.chkRestartIfShutdown.Location = new System.Drawing.Point(248, 142);
            this.chkRestartIfShutdown.Name = "chkRestartIfShutdown";
            this.chkRestartIfShutdown.Size = new System.Drawing.Size(166, 19);
            this.chkRestartIfShutdown.TabIndex = 7;
            this.chkRestartIfShutdown.Text = "Restart server if shutdown";
            this.chkRestartIfShutdown.UseVisualStyleBackColor = true;
            // 
            // chkMon2
            // 
            this.chkMon2.AutoSize = true;
            this.chkMon2.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.chkMon2.Enabled = false;
            this.chkMon2.Location = new System.Drawing.Point(235, 77);
            this.chkMon2.Name = "chkMon2";
            this.chkMon2.Size = new System.Drawing.Size(36, 33);
            this.chkMon2.TabIndex = 22;
            this.chkMon2.Text = "Mon";
            this.chkMon2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkMon2.UseVisualStyleBackColor = true;
            // 
            // chkAutoUpdate
            // 
            this.chkAutoUpdate.AutoSize = true;
            this.chkAutoUpdate.Location = new System.Drawing.Point(6, 143);
            this.chkAutoUpdate.Name = "chkAutoUpdate";
            this.chkAutoUpdate.Size = new System.Drawing.Size(236, 19);
            this.chkAutoUpdate.TabIndex = 6;
            this.chkAutoUpdate.Text = "Include server in the Auto-Update cycle";
            this.chkAutoUpdate.UseVisualStyleBackColor = true;
            // 
            // chkTue2
            // 
            this.chkTue2.AutoSize = true;
            this.chkTue2.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.chkTue2.Enabled = false;
            this.chkTue2.Location = new System.Drawing.Point(277, 77);
            this.chkTue2.Name = "chkTue2";
            this.chkTue2.Size = new System.Drawing.Size(32, 33);
            this.chkTue2.TabIndex = 23;
            this.chkTue2.Text = "Tue";
            this.chkTue2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkTue2.UseVisualStyleBackColor = true;
            // 
            // chkIncludeAutoBackup
            // 
            this.chkIncludeAutoBackup.AutoSize = true;
            this.chkIncludeAutoBackup.Location = new System.Drawing.Point(6, 118);
            this.chkIncludeAutoBackup.Name = "chkIncludeAutoBackup";
            this.chkIncludeAutoBackup.Size = new System.Drawing.Size(237, 19);
            this.chkIncludeAutoBackup.TabIndex = 5;
            this.chkIncludeAutoBackup.Text = "Include server in the Auto-Backup cycle";
            this.chkIncludeAutoBackup.UseVisualStyleBackColor = true;
            // 
            // chkShutdown1
            // 
            this.chkShutdown1.AutoSize = true;
            this.chkShutdown1.Location = new System.Drawing.Point(6, 52);
            this.chkShutdown1.Name = "chkShutdown1";
            this.chkShutdown1.Size = new System.Drawing.Size(123, 19);
            this.chkShutdown1.TabIndex = 3;
            this.chkShutdown1.Text = "Shutdow server at";
            this.chkShutdown1.UseVisualStyleBackColor = true;
            this.chkShutdown1.CheckedChanged += new System.EventHandler(this.chkShutdown1_CheckedChanged);
            // 
            // rbOnLogin
            // 
            this.rbOnLogin.AutoSize = true;
            this.rbOnLogin.Enabled = false;
            this.rbOnLogin.Location = new System.Drawing.Point(220, 20);
            this.rbOnLogin.Name = "rbOnLogin";
            this.rbOnLogin.Size = new System.Drawing.Size(73, 19);
            this.rbOnLogin.TabIndex = 2;
            this.rbOnLogin.Text = "on Login";
            this.rbOnLogin.UseVisualStyleBackColor = true;
            // 
            // rbOnBoot
            // 
            this.rbOnBoot.AutoSize = true;
            this.rbOnBoot.Checked = true;
            this.rbOnBoot.Enabled = false;
            this.rbOnBoot.Location = new System.Drawing.Point(143, 20);
            this.rbOnBoot.Name = "rbOnBoot";
            this.rbOnBoot.Size = new System.Drawing.Size(67, 19);
            this.rbOnBoot.TabIndex = 1;
            this.rbOnBoot.TabStop = true;
            this.rbOnBoot.Text = "on Boot";
            this.rbOnBoot.UseVisualStyleBackColor = true;
            // 
            // chkAutoStart
            // 
            this.chkAutoStart.AutoSize = true;
            this.chkAutoStart.Location = new System.Drawing.Point(7, 20);
            this.chkAutoStart.Name = "chkAutoStart";
            this.chkAutoStart.Size = new System.Drawing.Size(115, 19);
            this.chkAutoStart.TabIndex = 0;
            this.chkAutoStart.Text = "Auto-Start server";
            this.chkAutoStart.UseVisualStyleBackColor = true;
            this.chkAutoStart.CheckedChanged += new System.EventHandler(this.chkAutoStart_CheckedChanged);
            // 
            // expandCollapsePanel2
            // 
            this.expandCollapsePanel2.ButtonSize = MakarovDev.ExpandCollapsePanel.ExpandCollapseButton.ExpandButtonSize.Small;
            this.expandCollapsePanel2.ButtonStyle = MakarovDev.ExpandCollapsePanel.ExpandCollapseButton.ExpandButtonStyle.Classic;
            this.expandCollapsePanel2.Controls.Add(this.groupBox5);
            this.expandCollapsePanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.expandCollapsePanel2.ExpandedHeight = 296;
            this.expandCollapsePanel2.IsExpanded = true;
            this.expandCollapsePanel2.Location = new System.Drawing.Point(0, 1048);
            this.expandCollapsePanel2.Name = "expandCollapsePanel2";
            this.expandCollapsePanel2.Size = new System.Drawing.Size(868, 296);
            this.expandCollapsePanel2.TabIndex = 20;
            this.expandCollapsePanel2.Text = "Saves && Backups";
            this.expandCollapsePanel2.UseAnimation = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.groupBox5.Controls.Add(this.button3);
            this.groupBox5.Controls.Add(this.button1);
            this.groupBox5.Controls.Add(this.label15);
            this.groupBox5.Controls.Add(this.txtSubBackups);
            this.groupBox5.Controls.Add(this.tbSubBackups);
            this.groupBox5.Controls.Add(this.label17);
            this.groupBox5.Controls.Add(this.label13);
            this.groupBox5.Controls.Add(this.txtFirstBackup);
            this.groupBox5.Controls.Add(this.tbFirstBackup);
            this.groupBox5.Controls.Add(this.label14);
            this.groupBox5.Controls.Add(this.txtBackupToKeep);
            this.groupBox5.Controls.Add(this.label8);
            this.groupBox5.Controls.Add(this.txtLogLocation);
            this.groupBox5.Controls.Add(this.label20);
            this.groupBox5.Controls.Add(this.label7);
            this.groupBox5.Controls.Add(this.txtAutoSavePeriod);
            this.groupBox5.Controls.Add(this.tbAutoSavePeriod);
            this.groupBox5.Controls.Add(this.txtSaveLocation);
            this.groupBox5.Controls.Add(this.label9);
            this.groupBox5.Controls.Add(this.label19);
            this.groupBox5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.groupBox5.Location = new System.Drawing.Point(4, 44);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(861, 249);
            this.groupBox5.TabIndex = 19;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Saves && Backups";
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Location = new System.Drawing.Point(785, 89);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(30, 23);
            this.button3.TabIndex = 31;
            this.button3.Text = "...";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(785, 63);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(30, 23);
            this.button1.TabIndex = 30;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(782, 219);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(51, 15);
            this.label15.TabIndex = 29;
            this.label15.Text = "Minutes";
            // 
            // txtSubBackups
            // 
            this.txtSubBackups.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSubBackups.Enabled = false;
            this.txtSubBackups.Location = new System.Drawing.Point(784, 195);
            this.txtSubBackups.Name = "txtSubBackups";
            this.txtSubBackups.Size = new System.Drawing.Size(49, 21);
            this.txtSubBackups.TabIndex = 28;
            // 
            // tbSubBackups
            // 
            this.tbSubBackups.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSubBackups.Location = new System.Drawing.Point(115, 191);
            this.tbSubBackups.Maximum = 1440;
            this.tbSubBackups.Minimum = 1;
            this.tbSubBackups.Name = "tbSubBackups";
            this.tbSubBackups.Size = new System.Drawing.Size(648, 45);
            this.tbSubBackups.TabIndex = 27;
            this.tbSubBackups.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbSubBackups.Value = 1;
            this.tbSubBackups.Scroll += new System.EventHandler(this.tbSubBackups_Scroll);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(9, 201);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(123, 15);
            this.label17.TabIndex = 26;
            this.label17.Text = "Subsequent Backups";
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(783, 168);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(51, 15);
            this.label13.TabIndex = 25;
            this.label13.Text = "Minutes";
            // 
            // txtFirstBackup
            // 
            this.txtFirstBackup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFirstBackup.Enabled = false;
            this.txtFirstBackup.Location = new System.Drawing.Point(785, 144);
            this.txtFirstBackup.Name = "txtFirstBackup";
            this.txtFirstBackup.Size = new System.Drawing.Size(49, 21);
            this.txtFirstBackup.TabIndex = 24;
            // 
            // tbFirstBackup
            // 
            this.tbFirstBackup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFirstBackup.Location = new System.Drawing.Point(116, 140);
            this.tbFirstBackup.Maximum = 720;
            this.tbFirstBackup.Minimum = 1;
            this.tbFirstBackup.Name = "tbFirstBackup";
            this.tbFirstBackup.Size = new System.Drawing.Size(648, 45);
            this.tbFirstBackup.TabIndex = 23;
            this.tbFirstBackup.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbFirstBackup.Value = 1;
            this.tbFirstBackup.Scroll += new System.EventHandler(this.tbFirstBackup_Scroll);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(10, 150);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(87, 15);
            this.label14.TabIndex = 22;
            this.label14.Text = "First automatic";
            // 
            // txtBackupToKeep
            // 
            this.txtBackupToKeep.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBackupToKeep.Location = new System.Drawing.Point(115, 116);
            this.txtBackupToKeep.Name = "txtBackupToKeep";
            this.txtBackupToKeep.Size = new System.Drawing.Size(113, 21);
            this.txtBackupToKeep.TabIndex = 21;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 119);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(97, 15);
            this.label8.TabIndex = 20;
            this.label8.Text = "Backups to keep";
            // 
            // txtLogLocation
            // 
            this.txtLogLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLogLocation.Location = new System.Drawing.Point(116, 90);
            this.txtLogLocation.Name = "txtLogLocation";
            this.txtLogLocation.ReadOnly = true;
            this.txtLogLocation.Size = new System.Drawing.Size(662, 21);
            this.txtLogLocation.TabIndex = 9;
            // 
            // label20
            // 
            this.label20.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(783, 41);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(51, 15);
            this.label20.TabIndex = 19;
            this.label20.Text = "Minutes";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 93);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(101, 15);
            this.label7.TabIndex = 8;
            this.label7.Text = "Log File Location";
            // 
            // txtAutoSavePeriod
            // 
            this.txtAutoSavePeriod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAutoSavePeriod.Enabled = false;
            this.txtAutoSavePeriod.Location = new System.Drawing.Point(785, 17);
            this.txtAutoSavePeriod.Name = "txtAutoSavePeriod";
            this.txtAutoSavePeriod.Size = new System.Drawing.Size(49, 21);
            this.txtAutoSavePeriod.TabIndex = 18;
            // 
            // tbAutoSavePeriod
            // 
            this.tbAutoSavePeriod.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbAutoSavePeriod.Location = new System.Drawing.Point(116, 13);
            this.tbAutoSavePeriod.Maximum = 120;
            this.tbAutoSavePeriod.Minimum = 1;
            this.tbAutoSavePeriod.Name = "tbAutoSavePeriod";
            this.tbAutoSavePeriod.Size = new System.Drawing.Size(648, 45);
            this.tbAutoSavePeriod.TabIndex = 5;
            this.tbAutoSavePeriod.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbAutoSavePeriod.Value = 1;
            this.tbAutoSavePeriod.Scroll += new System.EventHandler(this.tbAutoSavePeriod_Scroll);
            // 
            // txtSaveLocation
            // 
            this.txtSaveLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSaveLocation.Location = new System.Drawing.Point(116, 64);
            this.txtSaveLocation.Name = "txtSaveLocation";
            this.txtSaveLocation.ReadOnly = true;
            this.txtSaveLocation.Size = new System.Drawing.Size(662, 21);
            this.txtSaveLocation.TabIndex = 6;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(11, 67);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(84, 15);
            this.label9.TabIndex = 4;
            this.label9.Text = "Save Location";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(10, 23);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(100, 15);
            this.label19.TabIndex = 4;
            this.label19.Text = "Auto Save Period";
            // 
            // expandCollapsePanel3
            // 
            this.expandCollapsePanel3.ButtonSize = MakarovDev.ExpandCollapsePanel.ExpandCollapseButton.ExpandButtonSize.Small;
            this.expandCollapsePanel3.ButtonStyle = MakarovDev.ExpandCollapsePanel.ExpandCollapseButton.ExpandButtonStyle.Classic;
            this.expandCollapsePanel3.Controls.Add(this.groupBox13);
            this.expandCollapsePanel3.Controls.Add(this.groupBox9);
            this.expandCollapsePanel3.Controls.Add(this.groupBox8);
            this.expandCollapsePanel3.Controls.Add(this.groupBox7);
            this.expandCollapsePanel3.Controls.Add(this.groupBox6);
            this.expandCollapsePanel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.expandCollapsePanel3.ExpandedHeight = 0;
            this.expandCollapsePanel3.IsExpanded = true;
            this.expandCollapsePanel3.Location = new System.Drawing.Point(0, 737);
            this.expandCollapsePanel3.Name = "expandCollapsePanel3";
            this.expandCollapsePanel3.Size = new System.Drawing.Size(868, 311);
            this.expandCollapsePanel3.TabIndex = 21;
            this.expandCollapsePanel3.Text = "Modifiers";
            this.expandCollapsePanel3.UseAnimation = true;
            // 
            // groupBox13
            // 
            this.groupBox13.Controls.Add(this.rbRaidsDefault);
            this.groupBox13.Controls.Add(this.rbRaidsMuchLess);
            this.groupBox13.Controls.Add(this.rbRaidsNone);
            this.groupBox13.Controls.Add(this.rbRaidsMuchMore);
            this.groupBox13.Controls.Add(this.rbRaidsMore);
            this.groupBox13.Controls.Add(this.rbRaidsLess);
            this.groupBox13.ForeColor = System.Drawing.Color.SteelBlue;
            this.groupBox13.Location = new System.Drawing.Point(4, 200);
            this.groupBox13.Name = "groupBox13";
            this.groupBox13.Size = new System.Drawing.Size(776, 50);
            this.groupBox13.TabIndex = 20;
            this.groupBox13.TabStop = false;
            this.groupBox13.Text = "Raids";
            // 
            // rbRaidsDefault
            // 
            this.rbRaidsDefault.AutoSize = true;
            this.rbRaidsDefault.Location = new System.Drawing.Point(6, 20);
            this.rbRaidsDefault.Name = "rbRaidsDefault";
            this.rbRaidsDefault.Size = new System.Drawing.Size(64, 19);
            this.rbRaidsDefault.TabIndex = 23;
            this.rbRaidsDefault.TabStop = true;
            this.rbRaidsDefault.Text = "Default";
            this.rbRaidsDefault.UseVisualStyleBackColor = true;
            // 
            // rbRaidsMuchLess
            // 
            this.rbRaidsMuchLess.AutoSize = true;
            this.rbRaidsMuchLess.Location = new System.Drawing.Point(188, 20);
            this.rbRaidsMuchLess.Name = "rbRaidsMuchLess";
            this.rbRaidsMuchLess.Size = new System.Drawing.Size(85, 19);
            this.rbRaidsMuchLess.TabIndex = 22;
            this.rbRaidsMuchLess.TabStop = true;
            this.rbRaidsMuchLess.Text = "Much Less";
            this.rbRaidsMuchLess.UseVisualStyleBackColor = true;
            // 
            // rbRaidsNone
            // 
            this.rbRaidsNone.AutoSize = true;
            this.rbRaidsNone.Location = new System.Drawing.Point(97, 20);
            this.rbRaidsNone.Name = "rbRaidsNone";
            this.rbRaidsNone.Size = new System.Drawing.Size(55, 19);
            this.rbRaidsNone.TabIndex = 21;
            this.rbRaidsNone.TabStop = true;
            this.rbRaidsNone.Text = "None";
            this.rbRaidsNone.UseVisualStyleBackColor = true;
            this.rbRaidsNone.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // rbRaidsMuchMore
            // 
            this.rbRaidsMuchMore.AutoSize = true;
            this.rbRaidsMuchMore.Location = new System.Drawing.Point(449, 20);
            this.rbRaidsMuchMore.Name = "rbRaidsMuchMore";
            this.rbRaidsMuchMore.Size = new System.Drawing.Size(88, 19);
            this.rbRaidsMuchMore.TabIndex = 20;
            this.rbRaidsMuchMore.TabStop = true;
            this.rbRaidsMuchMore.Text = "Much More";
            this.rbRaidsMuchMore.UseVisualStyleBackColor = true;
            // 
            // rbRaidsMore
            // 
            this.rbRaidsMore.AutoSize = true;
            this.rbRaidsMore.Location = new System.Drawing.Point(355, 20);
            this.rbRaidsMore.Name = "rbRaidsMore";
            this.rbRaidsMore.Size = new System.Drawing.Size(54, 19);
            this.rbRaidsMore.TabIndex = 19;
            this.rbRaidsMore.TabStop = true;
            this.rbRaidsMore.Text = "More";
            this.rbRaidsMore.UseVisualStyleBackColor = true;
            // 
            // rbRaidsLess
            // 
            this.rbRaidsLess.AutoSize = true;
            this.rbRaidsLess.Location = new System.Drawing.Point(279, 20);
            this.rbRaidsLess.Name = "rbRaidsLess";
            this.rbRaidsLess.Size = new System.Drawing.Size(51, 19);
            this.rbRaidsLess.TabIndex = 18;
            this.rbRaidsLess.TabStop = true;
            this.rbRaidsLess.Text = "Less";
            this.rbRaidsLess.UseVisualStyleBackColor = true;
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.rbPortalsNone);
            this.groupBox9.Controls.Add(this.rbPortalsVeryHard);
            this.groupBox9.Controls.Add(this.rbPortalsHard);
            this.groupBox9.Controls.Add(this.rbPortalsCasual);
            this.groupBox9.ForeColor = System.Drawing.Color.SteelBlue;
            this.groupBox9.Location = new System.Drawing.Point(4, 256);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(776, 50);
            this.groupBox9.TabIndex = 19;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Portals";
            // 
            // rbPortalsNone
            // 
            this.rbPortalsNone.AutoSize = true;
            this.rbPortalsNone.Location = new System.Drawing.Point(6, 20);
            this.rbPortalsNone.Name = "rbPortalsNone";
            this.rbPortalsNone.Size = new System.Drawing.Size(64, 19);
            this.rbPortalsNone.TabIndex = 24;
            this.rbPortalsNone.TabStop = true;
            this.rbPortalsNone.Text = "Default";
            this.rbPortalsNone.UseVisualStyleBackColor = true;
            // 
            // rbPortalsVeryHard
            // 
            this.rbPortalsVeryHard.AutoSize = true;
            this.rbPortalsVeryHard.Location = new System.Drawing.Point(279, 20);
            this.rbPortalsVeryHard.Name = "rbPortalsVeryHard";
            this.rbPortalsVeryHard.Size = new System.Drawing.Size(78, 19);
            this.rbPortalsVeryHard.TabIndex = 19;
            this.rbPortalsVeryHard.TabStop = true;
            this.rbPortalsVeryHard.Text = "Very Hard";
            this.rbPortalsVeryHard.UseVisualStyleBackColor = true;
            // 
            // rbPortalsHard
            // 
            this.rbPortalsHard.AutoSize = true;
            this.rbPortalsHard.Location = new System.Drawing.Point(188, 20);
            this.rbPortalsHard.Name = "rbPortalsHard";
            this.rbPortalsHard.Size = new System.Drawing.Size(52, 19);
            this.rbPortalsHard.TabIndex = 18;
            this.rbPortalsHard.TabStop = true;
            this.rbPortalsHard.Text = "Hard";
            this.rbPortalsHard.UseVisualStyleBackColor = true;
            // 
            // rbPortalsCasual
            // 
            this.rbPortalsCasual.AutoSize = true;
            this.rbPortalsCasual.Location = new System.Drawing.Point(97, 20);
            this.rbPortalsCasual.Name = "rbPortalsCasual";
            this.rbPortalsCasual.Size = new System.Drawing.Size(63, 19);
            this.rbPortalsCasual.TabIndex = 17;
            this.rbPortalsCasual.TabStop = true;
            this.rbPortalsCasual.Text = "Casual";
            this.rbPortalsCasual.UseVisualStyleBackColor = true;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.rbResourcesNone);
            this.groupBox8.Controls.Add(this.rbResourcesMuchLess);
            this.groupBox8.Controls.Add(this.rbResourcesMost);
            this.groupBox8.Controls.Add(this.rbResourcesMuchMore);
            this.groupBox8.Controls.Add(this.rbResourcesMore);
            this.groupBox8.Controls.Add(this.rbResourcesLess);
            this.groupBox8.ForeColor = System.Drawing.Color.SteelBlue;
            this.groupBox8.Location = new System.Drawing.Point(4, 144);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(776, 50);
            this.groupBox8.TabIndex = 18;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Resources";
            // 
            // rbResourcesNone
            // 
            this.rbResourcesNone.AutoSize = true;
            this.rbResourcesNone.Location = new System.Drawing.Point(6, 20);
            this.rbResourcesNone.Name = "rbResourcesNone";
            this.rbResourcesNone.Size = new System.Drawing.Size(64, 19);
            this.rbResourcesNone.TabIndex = 23;
            this.rbResourcesNone.TabStop = true;
            this.rbResourcesNone.Text = "Default";
            this.rbResourcesNone.UseVisualStyleBackColor = true;
            // 
            // rbResourcesMuchLess
            // 
            this.rbResourcesMuchLess.AutoSize = true;
            this.rbResourcesMuchLess.Location = new System.Drawing.Point(97, 20);
            this.rbResourcesMuchLess.Name = "rbResourcesMuchLess";
            this.rbResourcesMuchLess.Size = new System.Drawing.Size(85, 19);
            this.rbResourcesMuchLess.TabIndex = 22;
            this.rbResourcesMuchLess.TabStop = true;
            this.rbResourcesMuchLess.Text = "Much Less";
            this.rbResourcesMuchLess.UseVisualStyleBackColor = true;
            // 
            // rbResourcesMost
            // 
            this.rbResourcesMost.AutoSize = true;
            this.rbResourcesMost.Location = new System.Drawing.Point(449, 20);
            this.rbResourcesMost.Name = "rbResourcesMost";
            this.rbResourcesMost.Size = new System.Drawing.Size(52, 19);
            this.rbResourcesMost.TabIndex = 21;
            this.rbResourcesMost.TabStop = true;
            this.rbResourcesMost.Text = "Most";
            this.rbResourcesMost.UseVisualStyleBackColor = true;
            // 
            // rbResourcesMuchMore
            // 
            this.rbResourcesMuchMore.AutoSize = true;
            this.rbResourcesMuchMore.Location = new System.Drawing.Point(355, 20);
            this.rbResourcesMuchMore.Name = "rbResourcesMuchMore";
            this.rbResourcesMuchMore.Size = new System.Drawing.Size(88, 19);
            this.rbResourcesMuchMore.TabIndex = 20;
            this.rbResourcesMuchMore.TabStop = true;
            this.rbResourcesMuchMore.Text = "Much More";
            this.rbResourcesMuchMore.UseVisualStyleBackColor = true;
            // 
            // rbResourcesMore
            // 
            this.rbResourcesMore.AutoSize = true;
            this.rbResourcesMore.Location = new System.Drawing.Point(279, 20);
            this.rbResourcesMore.Name = "rbResourcesMore";
            this.rbResourcesMore.Size = new System.Drawing.Size(54, 19);
            this.rbResourcesMore.TabIndex = 19;
            this.rbResourcesMore.TabStop = true;
            this.rbResourcesMore.Text = "More";
            this.rbResourcesMore.UseVisualStyleBackColor = true;
            // 
            // rbResourcesLess
            // 
            this.rbResourcesLess.AutoSize = true;
            this.rbResourcesLess.Location = new System.Drawing.Point(188, 20);
            this.rbResourcesLess.Name = "rbResourcesLess";
            this.rbResourcesLess.Size = new System.Drawing.Size(51, 19);
            this.rbResourcesLess.TabIndex = 18;
            this.rbResourcesLess.TabStop = true;
            this.rbResourcesLess.Text = "Less";
            this.rbResourcesLess.UseVisualStyleBackColor = true;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.rbDeathPenaltyCasual);
            this.groupBox7.Controls.Add(this.rbDeathPenaltyHardCore);
            this.groupBox7.Controls.Add(this.rbDeathPenaltyHard);
            this.groupBox7.Controls.Add(this.rbDeathPenaltyEasy);
            this.groupBox7.Controls.Add(this.rbDeathPenaltyVeryEasy);
            this.groupBox7.Controls.Add(this.rbDeathPenaltyNone);
            this.groupBox7.ForeColor = System.Drawing.Color.SteelBlue;
            this.groupBox7.Location = new System.Drawing.Point(4, 88);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(776, 50);
            this.groupBox7.TabIndex = 17;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Death Penalty";
            // 
            // rbDeathPenaltyCasual
            // 
            this.rbDeathPenaltyCasual.AutoSize = true;
            this.rbDeathPenaltyCasual.Location = new System.Drawing.Point(97, 22);
            this.rbDeathPenaltyCasual.Name = "rbDeathPenaltyCasual";
            this.rbDeathPenaltyCasual.Size = new System.Drawing.Size(63, 19);
            this.rbDeathPenaltyCasual.TabIndex = 22;
            this.rbDeathPenaltyCasual.TabStop = true;
            this.rbDeathPenaltyCasual.Text = "Casual";
            this.rbDeathPenaltyCasual.UseVisualStyleBackColor = true;
            // 
            // rbDeathPenaltyHardCore
            // 
            this.rbDeathPenaltyHardCore.AutoSize = true;
            this.rbDeathPenaltyHardCore.Location = new System.Drawing.Point(449, 20);
            this.rbDeathPenaltyHardCore.Name = "rbDeathPenaltyHardCore";
            this.rbDeathPenaltyHardCore.Size = new System.Drawing.Size(76, 19);
            this.rbDeathPenaltyHardCore.TabIndex = 21;
            this.rbDeathPenaltyHardCore.TabStop = true;
            this.rbDeathPenaltyHardCore.Text = "Hardcore";
            this.rbDeathPenaltyHardCore.UseVisualStyleBackColor = true;
            // 
            // rbDeathPenaltyHard
            // 
            this.rbDeathPenaltyHard.AutoSize = true;
            this.rbDeathPenaltyHard.Location = new System.Drawing.Point(355, 20);
            this.rbDeathPenaltyHard.Name = "rbDeathPenaltyHard";
            this.rbDeathPenaltyHard.Size = new System.Drawing.Size(52, 19);
            this.rbDeathPenaltyHard.TabIndex = 20;
            this.rbDeathPenaltyHard.TabStop = true;
            this.rbDeathPenaltyHard.Text = "Hard";
            this.rbDeathPenaltyHard.UseVisualStyleBackColor = true;
            // 
            // rbDeathPenaltyEasy
            // 
            this.rbDeathPenaltyEasy.AutoSize = true;
            this.rbDeathPenaltyEasy.Location = new System.Drawing.Point(279, 22);
            this.rbDeathPenaltyEasy.Name = "rbDeathPenaltyEasy";
            this.rbDeathPenaltyEasy.Size = new System.Drawing.Size(51, 19);
            this.rbDeathPenaltyEasy.TabIndex = 19;
            this.rbDeathPenaltyEasy.TabStop = true;
            this.rbDeathPenaltyEasy.Text = "Easy";
            this.rbDeathPenaltyEasy.UseVisualStyleBackColor = true;
            // 
            // rbDeathPenaltyVeryEasy
            // 
            this.rbDeathPenaltyVeryEasy.AutoSize = true;
            this.rbDeathPenaltyVeryEasy.Location = new System.Drawing.Point(188, 22);
            this.rbDeathPenaltyVeryEasy.Name = "rbDeathPenaltyVeryEasy";
            this.rbDeathPenaltyVeryEasy.Size = new System.Drawing.Size(77, 19);
            this.rbDeathPenaltyVeryEasy.TabIndex = 18;
            this.rbDeathPenaltyVeryEasy.TabStop = true;
            this.rbDeathPenaltyVeryEasy.Text = "Very Easy";
            this.rbDeathPenaltyVeryEasy.UseVisualStyleBackColor = true;
            // 
            // rbDeathPenaltyNone
            // 
            this.rbDeathPenaltyNone.AutoSize = true;
            this.rbDeathPenaltyNone.Location = new System.Drawing.Point(6, 22);
            this.rbDeathPenaltyNone.Name = "rbDeathPenaltyNone";
            this.rbDeathPenaltyNone.Size = new System.Drawing.Size(64, 19);
            this.rbDeathPenaltyNone.TabIndex = 17;
            this.rbDeathPenaltyNone.TabStop = true;
            this.rbDeathPenaltyNone.Text = "Default";
            this.rbDeathPenaltyNone.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.rbCombatVeryHard);
            this.groupBox6.Controls.Add(this.rbCombatHard);
            this.groupBox6.Controls.Add(this.rbCombatEasy);
            this.groupBox6.Controls.Add(this.rbCombatVeryEasy);
            this.groupBox6.Controls.Add(this.rbCombatNone);
            this.groupBox6.ForeColor = System.Drawing.Color.SteelBlue;
            this.groupBox6.Location = new System.Drawing.Point(4, 32);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(776, 50);
            this.groupBox6.TabIndex = 16;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Combat";
            // 
            // rbCombatVeryHard
            // 
            this.rbCombatVeryHard.AutoSize = true;
            this.rbCombatVeryHard.Location = new System.Drawing.Point(355, 22);
            this.rbCombatVeryHard.Name = "rbCombatVeryHard";
            this.rbCombatVeryHard.Size = new System.Drawing.Size(78, 19);
            this.rbCombatVeryHard.TabIndex = 21;
            this.rbCombatVeryHard.TabStop = true;
            this.rbCombatVeryHard.Text = "Very Hard";
            this.rbCombatVeryHard.UseVisualStyleBackColor = true;
            // 
            // rbCombatHard
            // 
            this.rbCombatHard.AutoSize = true;
            this.rbCombatHard.Location = new System.Drawing.Point(279, 22);
            this.rbCombatHard.Name = "rbCombatHard";
            this.rbCombatHard.Size = new System.Drawing.Size(52, 19);
            this.rbCombatHard.TabIndex = 20;
            this.rbCombatHard.TabStop = true;
            this.rbCombatHard.Text = "Hard";
            this.rbCombatHard.UseVisualStyleBackColor = true;
            // 
            // rbCombatEasy
            // 
            this.rbCombatEasy.AutoSize = true;
            this.rbCombatEasy.Location = new System.Drawing.Point(188, 22);
            this.rbCombatEasy.Name = "rbCombatEasy";
            this.rbCombatEasy.Size = new System.Drawing.Size(51, 19);
            this.rbCombatEasy.TabIndex = 19;
            this.rbCombatEasy.TabStop = true;
            this.rbCombatEasy.Text = "Easy";
            this.rbCombatEasy.UseVisualStyleBackColor = true;
            // 
            // rbCombatVeryEasy
            // 
            this.rbCombatVeryEasy.AutoSize = true;
            this.rbCombatVeryEasy.Location = new System.Drawing.Point(97, 20);
            this.rbCombatVeryEasy.Name = "rbCombatVeryEasy";
            this.rbCombatVeryEasy.Size = new System.Drawing.Size(77, 19);
            this.rbCombatVeryEasy.TabIndex = 18;
            this.rbCombatVeryEasy.TabStop = true;
            this.rbCombatVeryEasy.Text = "Very Easy";
            this.rbCombatVeryEasy.UseVisualStyleBackColor = true;
            // 
            // rbCombatNone
            // 
            this.rbCombatNone.AutoSize = true;
            this.rbCombatNone.Location = new System.Drawing.Point(6, 22);
            this.rbCombatNone.Name = "rbCombatNone";
            this.rbCombatNone.Size = new System.Drawing.Size(64, 19);
            this.rbCombatNone.TabIndex = 17;
            this.rbCombatNone.TabStop = true;
            this.rbCombatNone.Text = "Default";
            this.rbCombatNone.UseVisualStyleBackColor = true;
            // 
            // expandCollapsePanel1
            // 
            this.expandCollapsePanel1.ButtonSize = MakarovDev.ExpandCollapsePanel.ExpandCollapseButton.ExpandButtonSize.Small;
            this.expandCollapsePanel1.ButtonStyle = MakarovDev.ExpandCollapsePanel.ExpandCollapseButton.ExpandButtonStyle.Classic;
            this.expandCollapsePanel1.Controls.Add(this.groupBox12);
            this.expandCollapsePanel1.Controls.Add(this.groupBox3);
            this.expandCollapsePanel1.Controls.Add(this.groupBox2);
            this.expandCollapsePanel1.Controls.Add(this.groupBox1);
            this.expandCollapsePanel1.Controls.Add(this.groupBox4);
            this.expandCollapsePanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.expandCollapsePanel1.ExpandedHeight = 456;
            this.expandCollapsePanel1.IsExpanded = true;
            this.expandCollapsePanel1.Location = new System.Drawing.Point(0, 147);
            this.expandCollapsePanel1.Name = "expandCollapsePanel1";
            this.expandCollapsePanel1.Size = new System.Drawing.Size(868, 590);
            this.expandCollapsePanel1.TabIndex = 3;
            this.expandCollapsePanel1.Text = "Administration";
            this.expandCollapsePanel1.UseAnimation = true;
            // 
            // groupBox12
            // 
            this.groupBox12.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox12.Controls.Add(this.button2);
            this.groupBox12.Controls.Add(this.txtCommand);
            this.groupBox12.Controls.Add(this.btProcessorAffinity);
            this.groupBox12.Controls.Add(this.txtAffinity);
            this.groupBox12.Controls.Add(this.label34);
            this.groupBox12.Controls.Add(this.label33);
            this.groupBox12.Controls.Add(this.cboPriority);
            this.groupBox12.ForeColor = System.Drawing.Color.SteelBlue;
            this.groupBox12.Location = new System.Drawing.Point(4, 451);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(878, 127);
            this.groupBox12.TabIndex = 23;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "Command Line";
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(805, 46);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(61, 23);
            this.button2.TabIndex = 72;
            this.button2.Text = "Refresh";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txtCommand
            // 
            this.txtCommand.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCommand.Enabled = false;
            this.txtCommand.Location = new System.Drawing.Point(5, 46);
            this.txtCommand.Multiline = true;
            this.txtCommand.Name = "txtCommand";
            this.txtCommand.Size = new System.Drawing.Size(795, 72);
            this.txtCommand.TabIndex = 71;
            // 
            // btProcessorAffinity
            // 
            this.btProcessorAffinity.Location = new System.Drawing.Point(513, 18);
            this.btProcessorAffinity.Name = "btProcessorAffinity";
            this.btProcessorAffinity.Size = new System.Drawing.Size(30, 23);
            this.btProcessorAffinity.TabIndex = 70;
            this.btProcessorAffinity.Text = "...";
            this.btProcessorAffinity.UseVisualStyleBackColor = true;
            this.btProcessorAffinity.Click += new System.EventHandler(this.btProcessorAffinity_Click);
            // 
            // txtAffinity
            // 
            this.txtAffinity.Enabled = false;
            this.txtAffinity.Location = new System.Drawing.Point(299, 20);
            this.txtAffinity.Name = "txtAffinity";
            this.txtAffinity.Size = new System.Drawing.Size(208, 21);
            this.txtAffinity.TabIndex = 69;
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(217, 22);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(76, 15);
            this.label34.TabIndex = 68;
            this.label34.Text = "Affinity - CPU";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(4, 22);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(44, 15);
            this.label33.TabIndex = 67;
            this.label33.Text = "Priority";
            // 
            // cboPriority
            // 
            this.cboPriority.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPriority.FormattingEnabled = true;
            this.cboPriority.Location = new System.Drawing.Point(70, 19);
            this.cboPriority.Name = "cboPriority";
            this.cboPriority.Size = new System.Drawing.Size(132, 21);
            this.cboPriority.TabIndex = 15;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.groupBox3.Controls.Add(this.groupBox10);
            this.groupBox3.Controls.Add(this.txtWorldName);
            this.groupBox3.Controls.Add(this.label23);
            this.groupBox3.Controls.Add(this.label21);
            this.groupBox3.Controls.Add(this.cLBKeys);
            this.groupBox3.Controls.Add(this.txtInstanceID);
            this.groupBox3.Controls.Add(this.label18);
            this.groupBox3.Controls.Add(this.chkCrossplay);
            this.groupBox3.Controls.Add(this.chkPublic);
            this.groupBox3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.groupBox3.Location = new System.Drawing.Point(4, 272);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(861, 173);
            this.groupBox3.TabIndex = 18;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Server Settings";
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.tbPresetHammer);
            this.groupBox10.Controls.Add(this.tbPresetCasual);
            this.groupBox10.Controls.Add(this.tbPresetImmersive);
            this.groupBox10.Controls.Add(this.tbPresetHardcore);
            this.groupBox10.Controls.Add(this.tbPresetHard);
            this.groupBox10.Controls.Add(this.tbPresetEasy);
            this.groupBox10.Controls.Add(this.tbPresetNormal);
            this.groupBox10.ForeColor = System.Drawing.Color.SteelBlue;
            this.groupBox10.Location = new System.Drawing.Point(8, 113);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(671, 50);
            this.groupBox10.TabIndex = 18;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Preset";
            // 
            // tbPresetHammer
            // 
            this.tbPresetHammer.AutoSize = true;
            this.tbPresetHammer.Location = new System.Drawing.Point(436, 22);
            this.tbPresetHammer.Name = "tbPresetHammer";
            this.tbPresetHammer.Size = new System.Drawing.Size(74, 19);
            this.tbPresetHammer.TabIndex = 23;
            this.tbPresetHammer.TabStop = true;
            this.tbPresetHammer.Text = "Hammer";
            this.tbPresetHammer.UseVisualStyleBackColor = true;
            // 
            // tbPresetCasual
            // 
            this.tbPresetCasual.AutoSize = true;
            this.tbPresetCasual.Location = new System.Drawing.Point(78, 22);
            this.tbPresetCasual.Name = "tbPresetCasual";
            this.tbPresetCasual.Size = new System.Drawing.Size(63, 19);
            this.tbPresetCasual.TabIndex = 22;
            this.tbPresetCasual.TabStop = true;
            this.tbPresetCasual.Text = "Casual";
            this.tbPresetCasual.UseVisualStyleBackColor = true;
            // 
            // tbPresetImmersive
            // 
            this.tbPresetImmersive.AutoSize = true;
            this.tbPresetImmersive.Location = new System.Drawing.Point(348, 22);
            this.tbPresetImmersive.Name = "tbPresetImmersive";
            this.tbPresetImmersive.Size = new System.Drawing.Size(82, 19);
            this.tbPresetImmersive.TabIndex = 21;
            this.tbPresetImmersive.TabStop = true;
            this.tbPresetImmersive.Text = "Immersive";
            this.tbPresetImmersive.UseVisualStyleBackColor = true;
            // 
            // tbPresetHardcore
            // 
            this.tbPresetHardcore.AutoSize = true;
            this.tbPresetHardcore.Location = new System.Drawing.Point(266, 22);
            this.tbPresetHardcore.Name = "tbPresetHardcore";
            this.tbPresetHardcore.Size = new System.Drawing.Size(76, 19);
            this.tbPresetHardcore.TabIndex = 20;
            this.tbPresetHardcore.TabStop = true;
            this.tbPresetHardcore.Text = "Hardcore";
            this.tbPresetHardcore.UseVisualStyleBackColor = true;
            // 
            // tbPresetHard
            // 
            this.tbPresetHard.AutoSize = true;
            this.tbPresetHard.Location = new System.Drawing.Point(208, 22);
            this.tbPresetHard.Name = "tbPresetHard";
            this.tbPresetHard.Size = new System.Drawing.Size(52, 19);
            this.tbPresetHard.TabIndex = 19;
            this.tbPresetHard.TabStop = true;
            this.tbPresetHard.Text = "Hard";
            this.tbPresetHard.UseVisualStyleBackColor = true;
            // 
            // tbPresetEasy
            // 
            this.tbPresetEasy.AutoSize = true;
            this.tbPresetEasy.Location = new System.Drawing.Point(151, 22);
            this.tbPresetEasy.Name = "tbPresetEasy";
            this.tbPresetEasy.Size = new System.Drawing.Size(51, 19);
            this.tbPresetEasy.TabIndex = 18;
            this.tbPresetEasy.TabStop = true;
            this.tbPresetEasy.Text = "Easy";
            this.tbPresetEasy.UseVisualStyleBackColor = true;
            // 
            // tbPresetNormal
            // 
            this.tbPresetNormal.AutoSize = true;
            this.tbPresetNormal.Location = new System.Drawing.Point(6, 22);
            this.tbPresetNormal.Name = "tbPresetNormal";
            this.tbPresetNormal.Size = new System.Drawing.Size(66, 19);
            this.tbPresetNormal.TabIndex = 17;
            this.tbPresetNormal.TabStop = true;
            this.tbPresetNormal.Text = "Normal";
            this.tbPresetNormal.UseVisualStyleBackColor = true;
            // 
            // txtWorldName
            // 
            this.txtWorldName.Location = new System.Drawing.Point(115, 87);
            this.txtWorldName.Name = "txtWorldName";
            this.txtWorldName.Size = new System.Drawing.Size(120, 21);
            this.txtWorldName.TabIndex = 14;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(10, 90);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(76, 15);
            this.label23.TabIndex = 13;
            this.label23.Text = "World Name";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(257, 21);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(48, 15);
            this.label21.TabIndex = 12;
            this.label21.Text = "Set Key";
            // 
            // cLBKeys
            // 
            this.cLBKeys.FormattingEnabled = true;
            this.cLBKeys.Items.AddRange(new object[] {
            "No Build Cost",
            "Player Events",
            "Passive Mobs",
            "No Map"});
            this.cLBKeys.Location = new System.Drawing.Point(321, 21);
            this.cLBKeys.Name = "cLBKeys";
            this.cLBKeys.Size = new System.Drawing.Size(120, 84);
            this.cLBKeys.TabIndex = 11;
            // 
            // txtInstanceID
            // 
            this.txtInstanceID.Location = new System.Drawing.Point(115, 64);
            this.txtInstanceID.Name = "txtInstanceID";
            this.txtInstanceID.Size = new System.Drawing.Size(121, 21);
            this.txtInstanceID.TabIndex = 10;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(10, 67);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(66, 15);
            this.label18.TabIndex = 9;
            this.label18.Text = "Instance Id";
            // 
            // chkCrossplay
            // 
            this.chkCrossplay.AutoSize = true;
            this.chkCrossplay.Location = new System.Drawing.Point(12, 45);
            this.chkCrossplay.Name = "chkCrossplay";
            this.chkCrossplay.Size = new System.Drawing.Size(79, 19);
            this.chkCrossplay.TabIndex = 8;
            this.chkCrossplay.Text = "Crossplay";
            this.chkCrossplay.UseVisualStyleBackColor = true;
            // 
            // chkPublic
            // 
            this.chkPublic.AutoSize = true;
            this.chkPublic.Location = new System.Drawing.Point(12, 20);
            this.chkPublic.Name = "chkPublic";
            this.chkPublic.Size = new System.Drawing.Size(60, 19);
            this.chkPublic.TabIndex = 7;
            this.chkPublic.Text = "Public";
            this.chkPublic.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.groupBox2.Controls.Add(this.txtLocalIP);
            this.groupBox2.Controls.Add(this.txtPeerPort);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.txtServerPort);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.groupBox2.Location = new System.Drawing.Point(4, 188);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(861, 78);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Networking";
            // 
            // txtLocalIP
            // 
            this.txtLocalIP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.txtLocalIP.FormattingEnabled = true;
            this.txtLocalIP.Location = new System.Drawing.Point(115, 19);
            this.txtLocalIP.Name = "txtLocalIP";
            this.txtLocalIP.Size = new System.Drawing.Size(515, 21);
            this.txtLocalIP.TabIndex = 12;
            // 
            // txtPeerPort
            // 
            this.txtPeerPort.Enabled = false;
            this.txtPeerPort.Location = new System.Drawing.Point(321, 46);
            this.txtPeerPort.Name = "txtPeerPort";
            this.txtPeerPort.Size = new System.Drawing.Size(93, 21);
            this.txtPeerPort.TabIndex = 9;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(257, 49);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(58, 15);
            this.label10.TabIndex = 8;
            this.label10.Text = "Peer Port";
            // 
            // txtServerPort
            // 
            this.txtServerPort.Location = new System.Drawing.Point(115, 46);
            this.txtServerPort.Name = "txtServerPort";
            this.txtServerPort.Size = new System.Drawing.Size(95, 21);
            this.txtServerPort.TabIndex = 7;
            this.txtServerPort.TextChanged += new System.EventHandler(this.txtServerPort_TextChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(10, 49);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(67, 15);
            this.label11.TabIndex = 6;
            this.label11.Text = "Server Port";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(10, 23);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(51, 15);
            this.label12.TabIndex = 4;
            this.label12.Text = "Local IP";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.groupBox1.Controls.Add(this.txtServerPWD);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtServerName);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.groupBox1.Location = new System.Drawing.Point(4, 105);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(861, 77);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Name and Password";
            // 
            // txtServerPWD
            // 
            this.txtServerPWD.Location = new System.Drawing.Point(115, 46);
            this.txtServerPWD.Name = "txtServerPWD";
            this.txtServerPWD.PasswordChar = '*';
            this.txtServerPWD.Size = new System.Drawing.Size(95, 21);
            this.txtServerPWD.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 49);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(99, 15);
            this.label6.TabIndex = 6;
            this.label6.Text = "Server Password";
            // 
            // txtServerName
            // 
            this.txtServerName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtServerName.Location = new System.Drawing.Point(115, 20);
            this.txtServerName.Name = "txtServerName";
            this.txtServerName.Size = new System.Drawing.Size(734, 21);
            this.txtServerName.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 15);
            this.label5.TabIndex = 4;
            this.label5.Text = "Server Name";
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.groupBox4.Controls.Add(this.cbBranch);
            this.groupBox4.Controls.Add(this.label22);
            this.groupBox4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.groupBox4.Location = new System.Drawing.Point(4, 43);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(861, 56);
            this.groupBox4.TabIndex = 15;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Server Details";
            // 
            // cbBranch
            // 
            this.cbBranch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbBranch.FormattingEnabled = true;
            this.cbBranch.Location = new System.Drawing.Point(115, 20);
            this.cbBranch.Name = "cbBranch";
            this.cbBranch.Size = new System.Drawing.Size(734, 21);
            this.cbBranch.TabIndex = 14;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(10, 23);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(46, 15);
            this.label22.TabIndex = 4;
            this.label22.Text = "Branch";
            // 
            // FrmValheim
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(885, 1100);
            this.Controls.Add(this.expandCollapsePanel4);
            this.Controls.Add(this.expandCollapsePanel2);
            this.Controls.Add(this.expandCollapsePanel3);
            this.Controls.Add(this.expandCollapsePanel1);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.Color.SteelBlue;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmValheim";
            this.Text = "FrmValheim";
            this.Load += new System.EventHandler(this.FrmValheim_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.expandCollapsePanel4.ResumeLayout(false);
            this.expandCollapsePanel4.PerformLayout();
            this.groupBox11.ResumeLayout(false);
            this.groupBox11.PerformLayout();
            this.expandCollapsePanel2.ResumeLayout(false);
            this.expandCollapsePanel2.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbSubBackups)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbFirstBackup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbAutoSavePeriod)).EndInit();
            this.expandCollapsePanel3.ResumeLayout(false);
            this.expandCollapsePanel3.PerformLayout();
            this.groupBox13.ResumeLayout(false);
            this.groupBox13.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.expandCollapsePanel1.ResumeLayout(false);
            this.expandCollapsePanel1.PerformLayout();
            this.groupBox12.ResumeLayout(false);
            this.groupBox12.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtBuild;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.TextBox txtVersion;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtServerType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btStart;
        private System.Windows.Forms.Button btUpdate;
        private System.Windows.Forms.Button btChooseFolder;
        private System.Windows.Forms.TextBox txtLocation;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.Button btSync;
        private System.Windows.Forms.TextBox txtProfileName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtProfileID;
        private System.Windows.Forms.Label label1;
        private MakarovDev.ExpandCollapsePanel.ExpandCollapsePanel expandCollapsePanel1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox cbBranch;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox txtLocalIP;
        private System.Windows.Forms.TextBox txtPeerPort;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtServerPort;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtServerPWD;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtServerName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtSaveLocation;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox chkPublic;
        private System.Windows.Forms.TextBox txtLogLocation;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txtAutoSavePeriod;
        private System.Windows.Forms.TrackBar tbAutoSavePeriod;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txtBackupToKeep;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtSubBackups;
        private System.Windows.Forms.TrackBar tbSubBackups;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtFirstBackup;
        private System.Windows.Forms.TrackBar tbFirstBackup;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.CheckBox chkCrossplay;
        private System.Windows.Forms.TextBox txtInstanceID;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.CheckedListBox cLBKeys;
        private System.Windows.Forms.TextBox txtWorldName;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.RadioButton rbCombatVeryHard;
        private System.Windows.Forms.RadioButton rbCombatHard;
        private System.Windows.Forms.RadioButton rbCombatEasy;
        private System.Windows.Forms.RadioButton rbCombatVeryEasy;
        private System.Windows.Forms.RadioButton rbCombatNone;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.RadioButton rbDeathPenaltyCasual;
        private System.Windows.Forms.RadioButton rbDeathPenaltyHardCore;
        private System.Windows.Forms.RadioButton rbDeathPenaltyHard;
        private System.Windows.Forms.RadioButton rbDeathPenaltyEasy;
        private System.Windows.Forms.RadioButton rbDeathPenaltyVeryEasy;
        private System.Windows.Forms.RadioButton rbDeathPenaltyNone;
        private MakarovDev.ExpandCollapsePanel.ExpandCollapsePanel expandCollapsePanel3;
        private MakarovDev.ExpandCollapsePanel.ExpandCollapsePanel expandCollapsePanel2;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.RadioButton rbResourcesMuchLess;
        private System.Windows.Forms.RadioButton rbResourcesMost;
        private System.Windows.Forms.RadioButton rbResourcesMuchMore;
        private System.Windows.Forms.RadioButton rbResourcesMore;
        private System.Windows.Forms.RadioButton rbResourcesLess;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.RadioButton rbPortalsVeryHard;
        private System.Windows.Forms.RadioButton rbPortalsHard;
        private System.Windows.Forms.RadioButton rbPortalsCasual;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.RadioButton tbPresetCasual;
        private System.Windows.Forms.RadioButton tbPresetImmersive;
        private System.Windows.Forms.RadioButton tbPresetHardcore;
        private System.Windows.Forms.RadioButton tbPresetHard;
        private System.Windows.Forms.RadioButton tbPresetEasy;
        private System.Windows.Forms.RadioButton tbPresetNormal;
        private System.Windows.Forms.RadioButton tbPresetHammer;
        private MakarovDev.ExpandCollapsePanel.ExpandCollapsePanel expandCollapsePanel4;
        private System.Windows.Forms.GroupBox groupBox11;
        private System.Windows.Forms.CheckBox chkRestart2;
        private System.Windows.Forms.CheckBox chkRestart1;
        private System.Windows.Forms.CheckBox chkUpdate1;
        private System.Windows.Forms.CheckBox chkUpdate2;
        private System.Windows.Forms.CheckBox chkSat1;
        private System.Windows.Forms.CheckBox chkFri1;
        private System.Windows.Forms.CheckBox chkSat2;
        private System.Windows.Forms.CheckBox chkThu1;
        private System.Windows.Forms.MaskedTextBox txtShutdow2;
        private System.Windows.Forms.CheckBox chkWed1;
        private System.Windows.Forms.CheckBox chkFri2;
        private System.Windows.Forms.CheckBox chkTue1;
        private System.Windows.Forms.CheckBox chkShutdown2;
        private System.Windows.Forms.CheckBox chkMon1;
        private System.Windows.Forms.CheckBox chkThu2;
        private System.Windows.Forms.CheckBox chkSun1;
        private System.Windows.Forms.CheckBox chkSun2;
        private System.Windows.Forms.MaskedTextBox txtShutdow1;
        private System.Windows.Forms.CheckBox chkWed2;
        private System.Windows.Forms.CheckBox chkRestartIfShutdown;
        private System.Windows.Forms.CheckBox chkMon2;
        private System.Windows.Forms.CheckBox chkAutoUpdate;
        private System.Windows.Forms.CheckBox chkTue2;
        private System.Windows.Forms.CheckBox chkIncludeAutoBackup;
        private System.Windows.Forms.CheckBox chkShutdown1;
        private System.Windows.Forms.RadioButton rbOnLogin;
        private System.Windows.Forms.RadioButton rbOnBoot;
        private System.Windows.Forms.CheckBox chkAutoStart;
        private System.Windows.Forms.GroupBox groupBox12;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox txtCommand;
        private System.Windows.Forms.Button btProcessorAffinity;
        private System.Windows.Forms.TextBox txtAffinity;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.ComboBox cboPriority;
        private System.Windows.Forms.RadioButton rbPortalsNone;
        private System.Windows.Forms.RadioButton rbResourcesNone;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.FolderBrowserDialog fbd;
        private System.Windows.Forms.Timer timerGetProcess;
        private System.Windows.Forms.GroupBox groupBox13;
        private System.Windows.Forms.RadioButton rbRaidsDefault;
        private System.Windows.Forms.RadioButton rbRaidsMuchLess;
        private System.Windows.Forms.RadioButton rbRaidsMuchMore;
        private System.Windows.Forms.RadioButton rbRaidsMore;
        private System.Windows.Forms.RadioButton rbRaidsLess;
        private System.Windows.Forms.RadioButton rbRaidsNone;
    }
}