namespace OphiussaServerManagerV2
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btDisable2 = new System.Windows.Forms.Button();
            this.btRun2 = new System.Windows.Forms.Button();
            this.lblAutoUpdate = new System.Windows.Forms.Label();
            this.btDisable1 = new System.Windows.Forms.Button();
            this.btRun1 = new System.Windows.Forms.Button();
            this.lblAutoBackup = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPublicIP = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtLocalIP = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshPublicIPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshLocalIPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createDesktopIconToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.updateSteamCMDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pluginsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.branchesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addNewServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControlExtra1 = new TradeWright.UI.Forms.TabControlExtra();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.timerCheckStatus = new System.Windows.Forms.Timer(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.serverMonitorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.panel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.SteelBlue;
            this.panel1.Controls.Add(this.btDisable2);
            this.panel1.Controls.Add(this.btRun2);
            this.panel1.Controls.Add(this.lblAutoUpdate);
            this.panel1.Controls.Add(this.btDisable1);
            this.panel1.Controls.Add(this.btRun1);
            this.panel1.Controls.Add(this.lblAutoBackup);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.txtPublicIP);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtLocalIP);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1016, 68);
            this.panel1.TabIndex = 0;
            // 
            // btDisable2
            // 
            this.btDisable2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btDisable2.BackColor = System.Drawing.Color.Goldenrod;
            this.btDisable2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btDisable2.ForeColor = System.Drawing.Color.White;
            this.btDisable2.Location = new System.Drawing.Point(948, 34);
            this.btDisable2.Name = "btDisable2";
            this.btDisable2.Size = new System.Drawing.Size(56, 21);
            this.btDisable2.TabIndex = 27;
            this.btDisable2.Text = "Disable";
            this.btDisable2.UseVisualStyleBackColor = false;
            this.btDisable2.Click += new System.EventHandler(this.btDisable2_Click);
            // 
            // btRun2
            // 
            this.btRun2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btRun2.BackColor = System.Drawing.Color.DarkGreen;
            this.btRun2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btRun2.ForeColor = System.Drawing.Color.White;
            this.btRun2.Location = new System.Drawing.Point(893, 34);
            this.btRun2.Name = "btRun2";
            this.btRun2.Size = new System.Drawing.Size(49, 21);
            this.btRun2.TabIndex = 26;
            this.btRun2.Text = "Run";
            this.btRun2.UseVisualStyleBackColor = false;
            this.btRun2.Click += new System.EventHandler(this.button4_Click);
            // 
            // lblAutoUpdate
            // 
            this.lblAutoUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAutoUpdate.AutoSize = true;
            this.lblAutoUpdate.BackColor = System.Drawing.Color.SteelBlue;
            this.lblAutoUpdate.ForeColor = System.Drawing.Color.Transparent;
            this.lblAutoUpdate.Location = new System.Drawing.Point(849, 38);
            this.lblAutoUpdate.Name = "lblAutoUpdate";
            this.lblAutoUpdate.Size = new System.Drawing.Size(38, 13);
            this.lblAutoUpdate.TabIndex = 25;
            this.lblAutoUpdate.Text = "Ready";
            // 
            // btDisable1
            // 
            this.btDisable1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btDisable1.BackColor = System.Drawing.Color.Goldenrod;
            this.btDisable1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btDisable1.ForeColor = System.Drawing.Color.White;
            this.btDisable1.Location = new System.Drawing.Point(948, 7);
            this.btDisable1.Name = "btDisable1";
            this.btDisable1.Size = new System.Drawing.Size(56, 21);
            this.btDisable1.TabIndex = 24;
            this.btDisable1.Text = "Disable";
            this.btDisable1.UseVisualStyleBackColor = false;
            this.btDisable1.Click += new System.EventHandler(this.btDisable1_Click);
            // 
            // btRun1
            // 
            this.btRun1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btRun1.BackColor = System.Drawing.Color.DarkGreen;
            this.btRun1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btRun1.ForeColor = System.Drawing.Color.White;
            this.btRun1.Location = new System.Drawing.Point(893, 7);
            this.btRun1.Name = "btRun1";
            this.btRun1.Size = new System.Drawing.Size(49, 21);
            this.btRun1.TabIndex = 23;
            this.btRun1.Text = "Run";
            this.btRun1.UseVisualStyleBackColor = false;
            this.btRun1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // lblAutoBackup
            // 
            this.lblAutoBackup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAutoBackup.AutoSize = true;
            this.lblAutoBackup.BackColor = System.Drawing.Color.SteelBlue;
            this.lblAutoBackup.ForeColor = System.Drawing.Color.Transparent;
            this.lblAutoBackup.Location = new System.Drawing.Point(849, 11);
            this.lblAutoBackup.Name = "lblAutoBackup";
            this.lblAutoBackup.Size = new System.Drawing.Size(38, 13);
            this.lblAutoBackup.TabIndex = 22;
            this.lblAutoBackup.Text = "Ready";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.SteelBlue;
            this.label6.ForeColor = System.Drawing.Color.Transparent;
            this.label6.Location = new System.Drawing.Point(776, 38);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 13);
            this.label6.TabIndex = 21;
            this.label6.Text = "Auto-Update";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.SteelBlue;
            this.label5.ForeColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(776, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 13);
            this.label5.TabIndex = 20;
            this.label5.Text = "Auto-Backup";
            // 
            // txtPublicIP
            // 
            this.txtPublicIP.Location = new System.Drawing.Point(64, 34);
            this.txtPublicIP.Name = "txtPublicIP";
            this.txtPublicIP.PasswordChar = '*';
            this.txtPublicIP.ReadOnly = true;
            this.txtPublicIP.Size = new System.Drawing.Size(100, 20);
            this.txtPublicIP.TabIndex = 3;
            this.txtPublicIP.DoubleClick += new System.EventHandler(this.txtPublicIP_DoubleClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(12, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Public IP";
            // 
            // txtLocalIP
            // 
            this.txtLocalIP.Location = new System.Drawing.Point(64, 8);
            this.txtLocalIP.Name = "txtLocalIP";
            this.txtLocalIP.ReadOnly = true;
            this.txtLocalIP.Size = new System.Drawing.Size(100, 20);
            this.txtLocalIP.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Local IP";
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolsToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.addNewServerToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1016, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.serverMonitorToolStripMenuItem,
            this.toolStripMenuItem1,
            this.refreshPublicIPToolStripMenuItem,
            this.refreshLocalIPToolStripMenuItem,
            this.createDesktopIconToolStripMenuItem,
            this.toolStripMenuItem2,
            this.updateSteamCMDToolStripMenuItem,
            this.toolStripMenuItem3,
            this.exitToolStripMenuItem1});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // refreshPublicIPToolStripMenuItem
            // 
            this.refreshPublicIPToolStripMenuItem.Name = "refreshPublicIPToolStripMenuItem";
            this.refreshPublicIPToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.refreshPublicIPToolStripMenuItem.Text = "Refresh Public IP";
            this.refreshPublicIPToolStripMenuItem.Click += new System.EventHandler(this.refreshPublicIPToolStripMenuItem_Click);
            // 
            // refreshLocalIPToolStripMenuItem
            // 
            this.refreshLocalIPToolStripMenuItem.Name = "refreshLocalIPToolStripMenuItem";
            this.refreshLocalIPToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.refreshLocalIPToolStripMenuItem.Text = "Refresh Local IP";
            this.refreshLocalIPToolStripMenuItem.Click += new System.EventHandler(this.refreshLocalIPToolStripMenuItem_Click);
            // 
            // createDesktopIconToolStripMenuItem
            // 
            this.createDesktopIconToolStripMenuItem.Name = "createDesktopIconToolStripMenuItem";
            this.createDesktopIconToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.createDesktopIconToolStripMenuItem.Text = "Create Desktop Icon";
            this.createDesktopIconToolStripMenuItem.Click += new System.EventHandler(this.createDesktopIconToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(177, 6);
            // 
            // updateSteamCMDToolStripMenuItem
            // 
            this.updateSteamCMDToolStripMenuItem.Name = "updateSteamCMDToolStripMenuItem";
            this.updateSteamCMDToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.updateSteamCMDToolStripMenuItem.Text = "Update Steam CMD";
            this.updateSteamCMDToolStripMenuItem.Click += new System.EventHandler(this.updateSteamCMDToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(177, 6);
            // 
            // exitToolStripMenuItem1
            // 
            this.exitToolStripMenuItem1.Name = "exitToolStripMenuItem1";
            this.exitToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.exitToolStripMenuItem1.Text = "Exit";
            this.exitToolStripMenuItem1.Click += new System.EventHandler(this.exitToolStripMenuItem1_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pluginsToolStripMenuItem,
            this.settingsToolStripMenuItem1,
            this.branchesToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // pluginsToolStripMenuItem
            // 
            this.pluginsToolStripMenuItem.Name = "pluginsToolStripMenuItem";
            this.pluginsToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.pluginsToolStripMenuItem.Text = "Plugins";
            this.pluginsToolStripMenuItem.Click += new System.EventHandler(this.pluginsToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem1
            // 
            this.settingsToolStripMenuItem1.Name = "settingsToolStripMenuItem1";
            this.settingsToolStripMenuItem1.Size = new System.Drawing.Size(122, 22);
            this.settingsToolStripMenuItem1.Text = "Settings";
            this.settingsToolStripMenuItem1.Click += new System.EventHandler(this.settingsToolStripMenuItem1_Click);
            // 
            // branchesToolStripMenuItem
            // 
            this.branchesToolStripMenuItem.Name = "branchesToolStripMenuItem";
            this.branchesToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.branchesToolStripMenuItem.Text = "Branches";
            this.branchesToolStripMenuItem.Click += new System.EventHandler(this.branchesToolStripMenuItem_Click);
            // 
            // addNewServerToolStripMenuItem
            // 
            this.addNewServerToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.addNewServerToolStripMenuItem.Image = global::OphiussaServerManagerV2.Properties.Resources.add_icon_icon__1_;
            this.addNewServerToolStripMenuItem.Name = "addNewServerToolStripMenuItem";
            this.addNewServerToolStripMenuItem.Size = new System.Drawing.Size(119, 20);
            this.addNewServerToolStripMenuItem.Text = "Add New Server";
            this.addNewServerToolStripMenuItem.Click += new System.EventHandler(this.addNewServerToolStripMenuItem_Click);
            // 
            // tabControlExtra1
            // 
            this.tabControlExtra1.DisplayStyle = TradeWright.UI.Forms.TabStyle.Angled;
            // 
            // 
            // 
            this.tabControlExtra1.DisplayStyleProvider.BlendStyle = TradeWright.UI.Forms.BlendStyle.Normal;
            this.tabControlExtra1.DisplayStyleProvider.BorderColorDisabled = System.Drawing.SystemColors.ControlLight;
            this.tabControlExtra1.DisplayStyleProvider.BorderColorFocused = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(157)))), ((int)(((byte)(185)))));
            this.tabControlExtra1.DisplayStyleProvider.BorderColorHighlighted = System.Drawing.SystemColors.ControlDark;
            this.tabControlExtra1.DisplayStyleProvider.BorderColorSelected = System.Drawing.SystemColors.ControlDark;
            this.tabControlExtra1.DisplayStyleProvider.BorderColorUnselected = System.Drawing.SystemColors.ControlDark;
            this.tabControlExtra1.DisplayStyleProvider.CloserButtonFillColorFocused = System.Drawing.Color.Empty;
            this.tabControlExtra1.DisplayStyleProvider.CloserButtonFillColorFocusedActive = System.Drawing.Color.Empty;
            this.tabControlExtra1.DisplayStyleProvider.CloserButtonFillColorHighlighted = System.Drawing.Color.Empty;
            this.tabControlExtra1.DisplayStyleProvider.CloserButtonFillColorHighlightedActive = System.Drawing.Color.Empty;
            this.tabControlExtra1.DisplayStyleProvider.CloserButtonFillColorSelected = System.Drawing.Color.Empty;
            this.tabControlExtra1.DisplayStyleProvider.CloserButtonFillColorSelectedActive = System.Drawing.Color.Empty;
            this.tabControlExtra1.DisplayStyleProvider.CloserButtonFillColorUnselected = System.Drawing.Color.Empty;
            this.tabControlExtra1.DisplayStyleProvider.CloserButtonOutlineColorFocused = System.Drawing.Color.Empty;
            this.tabControlExtra1.DisplayStyleProvider.CloserButtonOutlineColorFocusedActive = System.Drawing.Color.Empty;
            this.tabControlExtra1.DisplayStyleProvider.CloserButtonOutlineColorHighlighted = System.Drawing.Color.Empty;
            this.tabControlExtra1.DisplayStyleProvider.CloserButtonOutlineColorHighlightedActive = System.Drawing.Color.Empty;
            this.tabControlExtra1.DisplayStyleProvider.CloserButtonOutlineColorSelected = System.Drawing.Color.Empty;
            this.tabControlExtra1.DisplayStyleProvider.CloserButtonOutlineColorSelectedActive = System.Drawing.Color.Empty;
            this.tabControlExtra1.DisplayStyleProvider.CloserButtonOutlineColorUnselected = System.Drawing.Color.Empty;
            this.tabControlExtra1.DisplayStyleProvider.CloserColorFocused = System.Drawing.SystemColors.ControlDark;
            this.tabControlExtra1.DisplayStyleProvider.CloserColorFocusedActive = System.Drawing.SystemColors.ControlDark;
            this.tabControlExtra1.DisplayStyleProvider.CloserColorHighlighted = System.Drawing.SystemColors.ControlDark;
            this.tabControlExtra1.DisplayStyleProvider.CloserColorHighlightedActive = System.Drawing.SystemColors.ControlDark;
            this.tabControlExtra1.DisplayStyleProvider.CloserColorSelected = System.Drawing.SystemColors.ControlDark;
            this.tabControlExtra1.DisplayStyleProvider.CloserColorSelectedActive = System.Drawing.SystemColors.ControlDark;
            this.tabControlExtra1.DisplayStyleProvider.CloserColorUnselected = System.Drawing.Color.Empty;
            this.tabControlExtra1.DisplayStyleProvider.FocusTrack = false;
            this.tabControlExtra1.DisplayStyleProvider.HotTrack = true;
            this.tabControlExtra1.DisplayStyleProvider.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.tabControlExtra1.DisplayStyleProvider.Opacity = 1F;
            this.tabControlExtra1.DisplayStyleProvider.Overlap = 7;
            this.tabControlExtra1.DisplayStyleProvider.Padding = new System.Drawing.Point(10, 3);
            this.tabControlExtra1.DisplayStyleProvider.PageBackgroundColorDisabled = System.Drawing.SystemColors.Control;
            this.tabControlExtra1.DisplayStyleProvider.PageBackgroundColorFocused = System.Drawing.SystemColors.ControlLight;
            this.tabControlExtra1.DisplayStyleProvider.PageBackgroundColorHighlighted = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(244)))), ((int)(((byte)(252)))));
            this.tabControlExtra1.DisplayStyleProvider.PageBackgroundColorSelected = System.Drawing.SystemColors.ControlLightLight;
            this.tabControlExtra1.DisplayStyleProvider.PageBackgroundColorUnselected = System.Drawing.SystemColors.Control;
            this.tabControlExtra1.DisplayStyleProvider.Radius = 10;
            this.tabControlExtra1.DisplayStyleProvider.SelectedTabIsLarger = true;
            this.tabControlExtra1.DisplayStyleProvider.ShowTabCloser = true;
            this.tabControlExtra1.DisplayStyleProvider.TabColorDisabled1 = System.Drawing.SystemColors.Control;
            this.tabControlExtra1.DisplayStyleProvider.TabColorDisabled2 = System.Drawing.SystemColors.Control;
            this.tabControlExtra1.DisplayStyleProvider.TabColorFocused1 = System.Drawing.SystemColors.ControlLight;
            this.tabControlExtra1.DisplayStyleProvider.TabColorFocused2 = System.Drawing.SystemColors.ControlLight;
            this.tabControlExtra1.DisplayStyleProvider.TabColorHighLighted1 = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(244)))), ((int)(((byte)(252)))));
            this.tabControlExtra1.DisplayStyleProvider.TabColorHighLighted2 = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(237)))), ((int)(((byte)(252)))));
            this.tabControlExtra1.DisplayStyleProvider.TabColorSelected1 = System.Drawing.SystemColors.ControlLightLight;
            this.tabControlExtra1.DisplayStyleProvider.TabColorSelected2 = System.Drawing.SystemColors.ControlLightLight;
            this.tabControlExtra1.DisplayStyleProvider.TabColorUnSelected1 = System.Drawing.SystemColors.Control;
            this.tabControlExtra1.DisplayStyleProvider.TabColorUnSelected2 = System.Drawing.SystemColors.Control;
            this.tabControlExtra1.DisplayStyleProvider.TabPageMargin = new System.Windows.Forms.Padding(1);
            this.tabControlExtra1.DisplayStyleProvider.TextColorDisabled = System.Drawing.SystemColors.ControlDark;
            this.tabControlExtra1.DisplayStyleProvider.TextColorFocused = System.Drawing.SystemColors.ControlText;
            this.tabControlExtra1.DisplayStyleProvider.TextColorHighlighted = System.Drawing.SystemColors.ControlText;
            this.tabControlExtra1.DisplayStyleProvider.TextColorSelected = System.Drawing.SystemColors.ControlText;
            this.tabControlExtra1.DisplayStyleProvider.TextColorUnselected = System.Drawing.SystemColors.ControlText;
            this.tabControlExtra1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlExtra1.HotTrack = true;
            this.tabControlExtra1.ImageList = this.imageList1;
            this.tabControlExtra1.Location = new System.Drawing.Point(0, 92);
            this.tabControlExtra1.Name = "tabControlExtra1";
            this.tabControlExtra1.SelectedIndex = 0;
            this.tabControlExtra1.Size = new System.Drawing.Size(1016, 581);
            this.tabControlExtra1.TabIndex = 9;
            this.tabControlExtra1.TabImageClick += new System.EventHandler<System.Windows.Forms.TabControlEventArgs>(this.tabControlExtra1_TabImageClick);
            this.tabControlExtra1.TabClosing += new System.EventHandler<System.Windows.Forms.TabControlCancelEventArgs>(this.tabControlExtra1_TabClosing);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Blue");
            this.imageList1.Images.SetKeyName(1, "Red");
            this.imageList1.Images.SetKeyName(2, "Green");
            // 
            // timerCheckStatus
            // 
            this.timerCheckStatus.Enabled = true;
            this.timerCheckStatus.Interval = 500;
            this.timerCheckStatus.Tick += new System.EventHandler(this.timerCheckStatus_Tick);
            // 
            // serverMonitorToolStripMenuItem
            // 
            this.serverMonitorToolStripMenuItem.Name = "serverMonitorToolStripMenuItem";
            this.serverMonitorToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.serverMonitorToolStripMenuItem.Text = "Server Monitor";
            this.serverMonitorToolStripMenuItem.Click += new System.EventHandler(this.serverMonitorToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(177, 6);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 673);
            this.Controls.Add(this.tabControlExtra1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ophiussa Server Manager";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem pluginsToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPublicIP;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtLocalIP;
        private TradeWright.UI.Forms.TabControlExtra tabControlExtra1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripMenuItem addNewServerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshPublicIPToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshLocalIPToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createDesktopIconToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem updateSteamCMDToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem branchesToolStripMenuItem;
        private System.Windows.Forms.Button btDisable2;
        private System.Windows.Forms.Button btRun2;
        private System.Windows.Forms.Label lblAutoUpdate;
        private System.Windows.Forms.Button btDisable1;
        private System.Windows.Forms.Button btRun1;
        private System.Windows.Forms.Label lblAutoBackup;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Timer timerCheckStatus;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem serverMonitorToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
    }
}

