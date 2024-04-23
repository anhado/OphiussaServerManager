namespace VRisingPlugin.Forms
{
    partial class FrmVRising
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmVRising));
            this.button1 = new System.Windows.Forms.Button();
            this.automaticManagement1 = new OphiussaServerManager.Components.AutomaticManagement();
            this.profileHeader1 = new OphiussaFramework.Components.ProfileHeader();
            this.expandCollapsePanel1 = new MakarovDev.ExpandCollapsePanel.ExpandCollapsePanel();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.button4 = new System.Windows.Forms.Button();
            this.txtCommand = new System.Windows.Forms.TextBox();
            this.btProcessorAffinity = new System.Windows.Forms.Button();
            this.txtAffinity = new System.Windows.Forms.TextBox();
            this.label34 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.cboPriority = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.exTrackBar5 = new OphiussaFramework.Components.exTrackBar();
            this.exTrackBar4 = new OphiussaFramework.Components.exTrackBar();
            this.exTrackBar3 = new OphiussaFramework.Components.exTrackBar();
            this.exTrackBar2 = new OphiussaFramework.Components.exTrackBar();
            this.exTrackBar1 = new OphiussaFramework.Components.exTrackBar();
            this.chkSecure = new System.Windows.Forms.CheckBox();
            this.chkListOnEOS = new System.Windows.Forms.CheckBox();
            this.chkListOnSteam = new System.Windows.Forms.CheckBox();
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
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.expandCollapsePanel1.SuspendLayout();
            this.groupBox12.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1192, 135);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Open Command Builder";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // automaticManagement1
            // 
            this.automaticManagement1.ForeColor = System.Drawing.Color.SteelBlue;
            this.automaticManagement1.Location = new System.Drawing.Point(5, 793);
            this.automaticManagement1.Name = "automaticManagement1";
            this.automaticManagement1.Size = new System.Drawing.Size(800, 347);
            this.automaticManagement1.TabIndex = 2;
            // 
            // profileHeader1
            // 
            this.profileHeader1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.profileHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.profileHeader1.Location = new System.Drawing.Point(0, 0);
            this.profileHeader1.Name = "profileHeader1";
            this.profileHeader1.RconEnabled = true;
            this.profileHeader1.Size = new System.Drawing.Size(1279, 168);
            this.profileHeader1.TabIndex = 0;
            this.profileHeader1.ClickReload += new System.EventHandler(this.profileHeader1_ClickReload);
            this.profileHeader1.ClickSync += new System.EventHandler(this.profileHeader1_ClickSync);
            this.profileHeader1.ClickSave += new System.EventHandler(this.profileHeader1_ClickSave);
            this.profileHeader1.ClickUpgrade += new System.EventHandler(this.profileHeader1_ClickUpgrade);
            this.profileHeader1.ClickStartStop += new System.EventHandler(this.profileHeader1_ClickStartStop);
            this.profileHeader1.ClickRCON += new System.EventHandler(this.profileHeader1_ClickRCON);
            // 
            // expandCollapsePanel1
            // 
            this.expandCollapsePanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.expandCollapsePanel1.ButtonSize = MakarovDev.ExpandCollapsePanel.ExpandCollapseButton.ExpandButtonSize.Small;
            this.expandCollapsePanel1.ButtonStyle = MakarovDev.ExpandCollapsePanel.ExpandCollapseButton.ExpandButtonStyle.Classic;
            this.expandCollapsePanel1.Controls.Add(this.groupBox12);
            this.expandCollapsePanel1.Controls.Add(this.groupBox3);
            this.expandCollapsePanel1.Controls.Add(this.groupBox2);
            this.expandCollapsePanel1.Controls.Add(this.groupBox1);
            this.expandCollapsePanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.expandCollapsePanel1.ExpandedHeight = 511;
            this.expandCollapsePanel1.IsExpanded = true;
            this.expandCollapsePanel1.Location = new System.Drawing.Point(0, 168);
            this.expandCollapsePanel1.Name = "expandCollapsePanel1";
            this.expandCollapsePanel1.Size = new System.Drawing.Size(1279, 619);
            this.expandCollapsePanel1.TabIndex = 4;
            this.expandCollapsePanel1.Text = "Administration";
            this.expandCollapsePanel1.UseAnimation = false;
            // 
            // groupBox12
            // 
            this.groupBox12.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.groupBox12.Controls.Add(this.button4);
            this.groupBox12.Controls.Add(this.txtCommand);
            this.groupBox12.Controls.Add(this.btProcessorAffinity);
            this.groupBox12.Controls.Add(this.txtAffinity);
            this.groupBox12.Controls.Add(this.label34);
            this.groupBox12.Controls.Add(this.label33);
            this.groupBox12.Controls.Add(this.cboPriority);
            this.groupBox12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.groupBox12.Location = new System.Drawing.Point(4, 483);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(1287, 127);
            this.groupBox12.TabIndex = 23;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "Command Line";
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button4.Location = new System.Drawing.Point(1199, 46);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(61, 23);
            this.button4.TabIndex = 72;
            this.button4.Text = "Refresh";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // txtCommand
            // 
            this.txtCommand.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCommand.Enabled = false;
            this.txtCommand.Location = new System.Drawing.Point(5, 46);
            this.txtCommand.Multiline = true;
            this.txtCommand.Name = "txtCommand";
            this.txtCommand.Size = new System.Drawing.Size(1188, 72);
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
            this.groupBox3.Controls.Add(this.checkBox3);
            this.groupBox3.Controls.Add(this.checkBox2);
            this.groupBox3.Controls.Add(this.checkBox1);
            this.groupBox3.Controls.Add(this.exTrackBar5);
            this.groupBox3.Controls.Add(this.exTrackBar4);
            this.groupBox3.Controls.Add(this.exTrackBar3);
            this.groupBox3.Controls.Add(this.exTrackBar2);
            this.groupBox3.Controls.Add(this.exTrackBar1);
            this.groupBox3.Controls.Add(this.chkSecure);
            this.groupBox3.Controls.Add(this.chkListOnEOS);
            this.groupBox3.Controls.Add(this.chkListOnSteam);
            this.groupBox3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.groupBox3.Location = new System.Drawing.Point(4, 196);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1270, 281);
            this.groupBox3.TabIndex = 18;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Server Settings";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(12, 205);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(141, 19);
            this.checkBox1.TabIndex = 25;
            this.checkBox1.Text = "Compress Save Files";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // exTrackBar5
            // 
            this.exTrackBar5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.exTrackBar5.DisableTextBox = false;
            this.exTrackBar5.DisableTrackBar = false;
            this.exTrackBar5.Location = new System.Drawing.Point(12, 173);
            this.exTrackBar5.Maximum = 1440;
            this.exTrackBar5.Minimum = 1;
            this.exTrackBar5.Name = "exTrackBar5";
            this.exTrackBar5.Scale = 1F;
            this.exTrackBar5.Size = new System.Drawing.Size(1248, 26);
            this.exTrackBar5.TabIndex = 24;
            this.exTrackBar5.Text = "Auto Save Interval";
            this.exTrackBar5.TickFrequency = 1;
            this.exTrackBar5.Units = "Minutes";
            this.exTrackBar5.Value = 30F;
            // 
            // exTrackBar4
            // 
            this.exTrackBar4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.exTrackBar4.DisableTextBox = false;
            this.exTrackBar4.DisableTrackBar = false;
            this.exTrackBar4.Location = new System.Drawing.Point(13, 141);
            this.exTrackBar4.Maximum = 120;
            this.exTrackBar4.Minimum = 1;
            this.exTrackBar4.Name = "exTrackBar4";
            this.exTrackBar4.Scale = 1F;
            this.exTrackBar4.Size = new System.Drawing.Size(1248, 26);
            this.exTrackBar4.TabIndex = 23;
            this.exTrackBar4.Text = "Auto Save Count";
            this.exTrackBar4.TickFrequency = 1;
            this.exTrackBar4.Units = "Files";
            this.exTrackBar4.Value = 30F;
            // 
            // exTrackBar3
            // 
            this.exTrackBar3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.exTrackBar3.DisableTextBox = false;
            this.exTrackBar3.DisableTrackBar = false;
            this.exTrackBar3.Location = new System.Drawing.Point(13, 109);
            this.exTrackBar3.Maximum = 120;
            this.exTrackBar3.Minimum = 1;
            this.exTrackBar3.Name = "exTrackBar3";
            this.exTrackBar3.Scale = 1F;
            this.exTrackBar3.Size = new System.Drawing.Size(1248, 26);
            this.exTrackBar3.TabIndex = 22;
            this.exTrackBar3.Text = "Server FPS";
            this.exTrackBar3.TickFrequency = 1;
            this.exTrackBar3.Units = "FPS";
            this.exTrackBar3.Value = 30F;
            // 
            // exTrackBar2
            // 
            this.exTrackBar2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.exTrackBar2.DisableTextBox = false;
            this.exTrackBar2.DisableTrackBar = false;
            this.exTrackBar2.Location = new System.Drawing.Point(12, 77);
            this.exTrackBar2.Maximum = 40;
            this.exTrackBar2.Minimum = 1;
            this.exTrackBar2.Name = "exTrackBar2";
            this.exTrackBar2.Scale = 1F;
            this.exTrackBar2.Size = new System.Drawing.Size(1248, 26);
            this.exTrackBar2.TabIndex = 21;
            this.exTrackBar2.Text = "Max Admins";
            this.exTrackBar2.TickFrequency = 1;
            this.exTrackBar2.Units = "Admin";
            this.exTrackBar2.Value = 4F;
            // 
            // exTrackBar1
            // 
            this.exTrackBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.exTrackBar1.DisableTextBox = false;
            this.exTrackBar1.DisableTrackBar = false;
            this.exTrackBar1.Location = new System.Drawing.Point(12, 45);
            this.exTrackBar1.Maximum = 100;
            this.exTrackBar1.Minimum = 1;
            this.exTrackBar1.Name = "exTrackBar1";
            this.exTrackBar1.Scale = 1F;
            this.exTrackBar1.Size = new System.Drawing.Size(1248, 26);
            this.exTrackBar1.TabIndex = 20;
            this.exTrackBar1.Text = "Max Users";
            this.exTrackBar1.TickFrequency = 1;
            this.exTrackBar1.Units = "Players";
            this.exTrackBar1.Value = 40F;
            // 
            // chkSecure
            // 
            this.chkSecure.AutoSize = true;
            this.chkSecure.Location = new System.Drawing.Point(215, 20);
            this.chkSecure.Name = "chkSecure";
            this.chkSecure.Size = new System.Drawing.Size(65, 19);
            this.chkSecure.TabIndex = 19;
            this.chkSecure.Text = "Secure";
            this.chkSecure.UseVisualStyleBackColor = true;
            // 
            // chkListOnEOS
            // 
            this.chkListOnEOS.AutoSize = true;
            this.chkListOnEOS.Location = new System.Drawing.Point(119, 20);
            this.chkListOnEOS.Name = "chkListOnEOS";
            this.chkListOnEOS.Size = new System.Drawing.Size(90, 19);
            this.chkListOnEOS.TabIndex = 8;
            this.chkListOnEOS.Text = "List on EOS";
            this.chkListOnEOS.UseVisualStyleBackColor = true;
            // 
            // chkListOnSteam
            // 
            this.chkListOnSteam.AutoSize = true;
            this.chkListOnSteam.Location = new System.Drawing.Point(12, 20);
            this.chkListOnSteam.Name = "chkListOnSteam";
            this.chkListOnSteam.Size = new System.Drawing.Size(101, 19);
            this.chkListOnSteam.TabIndex = 7;
            this.chkListOnSteam.Text = "List on Steam";
            this.chkListOnSteam.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.groupBox2.Controls.Add(this.textBox2);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.checkBox4);
            this.groupBox2.Controls.Add(this.txtLocalIP);
            this.groupBox2.Controls.Add(this.txtPeerPort);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.txtServerPort);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.groupBox2.Location = new System.Drawing.Point(4, 112);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1270, 78);
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
            this.label10.Size = new System.Drawing.Size(64, 15);
            this.label10.TabIndex = 8;
            this.label10.Text = "Query Port";
            // 
            // txtServerPort
            // 
            this.txtServerPort.Location = new System.Drawing.Point(115, 46);
            this.txtServerPort.Name = "txtServerPort";
            this.txtServerPort.Size = new System.Drawing.Size(95, 21);
            this.txtServerPort.TabIndex = 7;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(10, 49);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(29, 15);
            this.label11.TabIndex = 6;
            this.label11.Text = "Port";
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
            this.groupBox1.Location = new System.Drawing.Point(4, 29);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1270, 77);
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
            this.txtServerName.Size = new System.Drawing.Size(1143, 21);
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
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(13, 230);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(167, 19);
            this.checkBox2.TabIndex = 26;
            this.checkBox2.Text = "Admin Only Debug Events";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(12, 255);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(93, 19);
            this.checkBox3.TabIndex = 27;
            this.checkBox3.Text = "API Enabled";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Location = new System.Drawing.Point(636, 19);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(55, 19);
            this.checkBox4.TabIndex = 13;
            this.checkBox4.Text = "Rcon";
            this.checkBox4.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(761, 19);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(93, 21);
            this.textBox1.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(697, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 15);
            this.label1.TabIndex = 14;
            this.label1.Text = "Rcon Port";
            // 
            // textBox2
            // 
            this.textBox2.Enabled = false;
            this.textBox2.Location = new System.Drawing.Point(952, 20);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(93, 21);
            this.textBox2.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(860, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 15);
            this.label2.TabIndex = 16;
            this.label2.Text = "Password Port";
            // 
            // FrmVRising
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1279, 1146);
            this.Controls.Add(this.expandCollapsePanel1);
            this.Controls.Add(this.automaticManagement1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.profileHeader1);
            this.ForeColor = System.Drawing.Color.SteelBlue;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmVRising";
            this.Text = "VRising";
            this.Load += new System.EventHandler(this.FrmConfigurationForm_Load);
            this.expandCollapsePanel1.ResumeLayout(false);
            this.expandCollapsePanel1.PerformLayout();
            this.groupBox12.ResumeLayout(false);
            this.groupBox12.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private OphiussaFramework.Components.ProfileHeader profileHeader1;
        private System.Windows.Forms.Button button1;
        private OphiussaServerManager.Components.AutomaticManagement automaticManagement1; 
        private MakarovDev.ExpandCollapsePanel.ExpandCollapsePanel expandCollapsePanel1;
        private System.Windows.Forms.GroupBox groupBox12;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox txtCommand;
        private System.Windows.Forms.Button btProcessorAffinity;
        private System.Windows.Forms.TextBox txtAffinity;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.ComboBox cboPriority;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox chkListOnEOS;
        private System.Windows.Forms.CheckBox chkListOnSteam;
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
        private System.Windows.Forms.CheckBox chkSecure;
        private OphiussaFramework.Components.exTrackBar exTrackBar2;
        private OphiussaFramework.Components.exTrackBar exTrackBar1;
        private OphiussaFramework.Components.exTrackBar exTrackBar3;
        private OphiussaFramework.Components.exTrackBar exTrackBar4;
        private OphiussaFramework.Components.exTrackBar exTrackBar5;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label2;
    }
}