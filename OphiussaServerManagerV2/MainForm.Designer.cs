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
            ControlRenderer controlRenderer1 = new ControlRenderer();
            MSColorTable msColorTable1 = new MSColorTable();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.osmPanel1 = new OSMPanel();
            this.txtPublicIP = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtLocalIP = new System.Windows.Forms.TextBox();
            this.osmMenuStrip1 = new OSMMenuStrip();
            this.toolsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshPublicIPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshLocalIPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createDesktopIconToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.updateSteamCMDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.pluginsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.addNewServerToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.osmPanel2 = new OSMPanel();
            this.tabControlExtra1 = new TradeWright.UI.Forms.TabControlExtra();
            this.osmPanel1.SuspendLayout();
            this.osmMenuStrip1.SuspendLayout();
            this.osmPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Blue");
            this.imageList1.Images.SetKeyName(1, "Red");
            this.imageList1.Images.SetKeyName(2, "Green");
            // 
            // osmPanel1
            // 
            this.osmPanel1.BackColor = System.Drawing.Color.Transparent;
            this.osmPanel1.Controls.Add(this.txtPublicIP);
            this.osmPanel1.Controls.Add(this.label1);
            this.osmPanel1.Controls.Add(this.label2);
            this.osmPanel1.Controls.Add(this.txtLocalIP);
            this.osmPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.osmPanel1.Location = new System.Drawing.Point(0, 24);
            this.osmPanel1.Name = "osmPanel1";
            this.osmPanel1.Padding = new System.Windows.Forms.Padding(5);
            this.osmPanel1.Size = new System.Drawing.Size(1016, 71);
            this.osmPanel1.TabIndex = 1;
            this.osmPanel1.Text = "osmPanel1";
            // 
            // txtPublicIP
            // 
            this.txtPublicIP.Location = new System.Drawing.Point(75, 28);
            this.txtPublicIP.Name = "txtPublicIP";
            this.txtPublicIP.PasswordChar = '*';
            this.txtPublicIP.ReadOnly = true;
            this.txtPublicIP.Size = new System.Drawing.Size(100, 20);
            this.txtPublicIP.TabIndex = 3;
            this.txtPublicIP.DoubleClick += new System.EventHandler(this.txtPublicIP_DoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(9, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Local IP";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(9, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Public IP";
            // 
            // txtLocalIP
            // 
            this.txtLocalIP.Location = new System.Drawing.Point(75, 2);
            this.txtLocalIP.Name = "txtLocalIP";
            this.txtLocalIP.ReadOnly = true;
            this.txtLocalIP.Size = new System.Drawing.Size(100, 20);
            this.txtLocalIP.TabIndex = 1;
            // 
            // osmMenuStrip1
            // 
            this.osmMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolsToolStripMenuItem1,
            this.settingsToolStripMenuItem2,
            this.addNewServerToolStripMenuItem1});
            this.osmMenuStrip1.Location = new System.Drawing.Point(0, 0);
            this.osmMenuStrip1.Name = "osmMenuStrip1";
            controlRenderer1.ColorTable = msColorTable1;
            controlRenderer1.RoundedEdges = true;
            this.osmMenuStrip1.Renderer = controlRenderer1;
            this.osmMenuStrip1.Size = new System.Drawing.Size(1016, 24);
            this.osmMenuStrip1.TabIndex = 0;
            this.osmMenuStrip1.Text = "osmMenuStrip1";
            // 
            // toolsToolStripMenuItem1
            // 
            this.toolsToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshPublicIPToolStripMenuItem,
            this.refreshLocalIPToolStripMenuItem,
            this.createDesktopIconToolStripMenuItem,
            this.toolStripMenuItem2,
            this.updateSteamCMDToolStripMenuItem,
            this.toolStripMenuItem3,
            this.exitToolStripMenuItem1});
            this.toolsToolStripMenuItem1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.toolsToolStripMenuItem1.Name = "toolsToolStripMenuItem1";
            this.toolsToolStripMenuItem1.Size = new System.Drawing.Size(46, 20);
            this.toolsToolStripMenuItem1.Text = "Tools";
            // 
            // refreshPublicIPToolStripMenuItem
            // 
            this.refreshPublicIPToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.refreshPublicIPToolStripMenuItem.Name = "refreshPublicIPToolStripMenuItem";
            this.refreshPublicIPToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.refreshPublicIPToolStripMenuItem.Text = "Refresh Public IP";
            this.refreshPublicIPToolStripMenuItem.Click += new System.EventHandler(this.refreshPublicIPToolStripMenuItem_Click);
            // 
            // refreshLocalIPToolStripMenuItem
            // 
            this.refreshLocalIPToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.refreshLocalIPToolStripMenuItem.Name = "refreshLocalIPToolStripMenuItem";
            this.refreshLocalIPToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.refreshLocalIPToolStripMenuItem.Text = "Refresh Local IP";
            this.refreshLocalIPToolStripMenuItem.Click += new System.EventHandler(this.refreshLocalIPToolStripMenuItem_Click);
            // 
            // createDesktopIconToolStripMenuItem
            // 
            this.createDesktopIconToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
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
            this.updateSteamCMDToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
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
            this.exitToolStripMenuItem1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.exitToolStripMenuItem1.Name = "exitToolStripMenuItem1";
            this.exitToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.exitToolStripMenuItem1.Text = "Exit";
            this.exitToolStripMenuItem1.Click += new System.EventHandler(this.exitToolStripMenuItem1_Click);
            // 
            // settingsToolStripMenuItem2
            // 
            this.settingsToolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pluginsToolStripMenuItem,
            this.settingsToolStripMenuItem1});
            this.settingsToolStripMenuItem2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.settingsToolStripMenuItem2.Name = "settingsToolStripMenuItem2";
            this.settingsToolStripMenuItem2.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem2.Text = "Settings";
            // 
            // pluginsToolStripMenuItem
            // 
            this.pluginsToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.pluginsToolStripMenuItem.Name = "pluginsToolStripMenuItem";
            this.pluginsToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.pluginsToolStripMenuItem.Text = "Plugins";
            this.pluginsToolStripMenuItem.Click += new System.EventHandler(this.pluginsToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem1
            // 
            this.settingsToolStripMenuItem1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.settingsToolStripMenuItem1.Name = "settingsToolStripMenuItem1";
            this.settingsToolStripMenuItem1.Size = new System.Drawing.Size(116, 22);
            this.settingsToolStripMenuItem1.Text = "Settings";
            this.settingsToolStripMenuItem1.Click += new System.EventHandler(this.settingsToolStripMenuItem1_Click);
            // 
            // addNewServerToolStripMenuItem1
            // 
            this.addNewServerToolStripMenuItem1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.addNewServerToolStripMenuItem1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.addNewServerToolStripMenuItem1.Image = global::OphiussaServerManagerV2.Properties.Resources.add_icon_icon__1_;
            this.addNewServerToolStripMenuItem1.Name = "addNewServerToolStripMenuItem1";
            this.addNewServerToolStripMenuItem1.Size = new System.Drawing.Size(119, 20);
            this.addNewServerToolStripMenuItem1.Text = "Add New Server";
            this.addNewServerToolStripMenuItem1.Click += new System.EventHandler(this.addNewServerToolStripMenuItem_Click);
            // 
            // osmPanel2
            // 
            this.osmPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.osmPanel2.BackColor = System.Drawing.Color.Transparent;
            this.osmPanel2.Controls.Add(this.tabControlExtra1);
            this.osmPanel2.Location = new System.Drawing.Point(0, 101);
            this.osmPanel2.Name = "osmPanel2";
            this.osmPanel2.Padding = new System.Windows.Forms.Padding(5);
            this.osmPanel2.Size = new System.Drawing.Size(1016, 560);
            this.osmPanel2.TabIndex = 10;
            this.osmPanel2.Text = "osmPanel2";
            // 
            // tabControlExtra1
            // 
            this.tabControlExtra1.Cursor = System.Windows.Forms.Cursors.Arrow;
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
            this.tabControlExtra1.Location = new System.Drawing.Point(5, 5);
            this.tabControlExtra1.Name = "tabControlExtra1";
            this.tabControlExtra1.SelectedIndex = 0;
            this.tabControlExtra1.Size = new System.Drawing.Size(1006, 550);
            this.tabControlExtra1.TabIndex = 9;
            this.tabControlExtra1.TabImageClick += new System.EventHandler<System.Windows.Forms.TabControlEventArgs>(this.tabControlExtra1_TabImageClick);
            this.tabControlExtra1.TabClosing += new System.EventHandler<System.Windows.Forms.TabControlCancelEventArgs>(this.tabControlExtra1_TabClosing);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 673);
            this.Controls.Add(this.osmPanel1);
            this.Controls.Add(this.osmMenuStrip1);
            this.Controls.Add(this.osmPanel2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ophiussa Server Manager";
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.osmPanel1.ResumeLayout(false);
            this.osmPanel1.PerformLayout();
            this.osmMenuStrip1.ResumeLayout(false);
            this.osmMenuStrip1.PerformLayout();
            this.osmPanel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem pluginsToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPublicIP;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtLocalIP;
        private TradeWright.UI.Forms.TabControlExtra tabControlExtra1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripMenuItem refreshPublicIPToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshLocalIPToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createDesktopIconToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem updateSteamCMDToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem1; 
        private OSMMenuStrip osmMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem addNewServerToolStripMenuItem1;
        private OSMPanel osmPanel1;
        private OSMPanel osmPanel2;
    }
}

