namespace OphiussaServerManager
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtPublicIP = new System.Windows.Forms.TextBox();
            this.txtLocalIP = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtVersion = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.NewTab = new System.Windows.Forms.TabPage();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serverMonitorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.routingFirewallToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshPublicIPToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshLocalIPToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblAutoBackup = new System.Windows.Forms.Label();
            this.btRun1 = new System.Windows.Forms.Button();
            this.btDisable1 = new System.Windows.Forms.Button();
            this.btDisable2 = new System.Windows.Forms.Button();
            this.btRun2 = new System.Windows.Forms.Button();
            this.lblAutoUpdate = new System.Windows.Forms.Label();
            this.timerCheckTask = new System.Windows.Forms.Timer(this.components);
            this.updateSteamCMDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.testsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.SteelBlue;
            this.label1.ForeColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(12, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "My Public IP";
            // 
            // txtPublicIP
            // 
            this.txtPublicIP.Location = new System.Drawing.Point(84, 32);
            this.txtPublicIP.Name = "txtPublicIP";
            this.txtPublicIP.ReadOnly = true;
            this.txtPublicIP.Size = new System.Drawing.Size(100, 20);
            this.txtPublicIP.TabIndex = 1;
            this.txtPublicIP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPublicIP.TextChanged += new System.EventHandler(this.txtPublicIP_TextChanged);
            // 
            // txtLocalIP
            // 
            this.txtLocalIP.Location = new System.Drawing.Point(84, 58);
            this.txtLocalIP.Name = "txtLocalIP";
            this.txtLocalIP.ReadOnly = true;
            this.txtLocalIP.Size = new System.Drawing.Size(100, 20);
            this.txtLocalIP.TabIndex = 3;
            this.txtLocalIP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtLocalIP.TextChanged += new System.EventHandler(this.txtLocalIP_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.SteelBlue;
            this.label2.ForeColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(12, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "My Host IP";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.SteelBlue;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Location = new System.Drawing.Point(0, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(800, 83);
            this.label3.TabIndex = 4;
            // 
            // txtVersion
            // 
            this.txtVersion.BackColor = System.Drawing.Color.SteelBlue;
            this.txtVersion.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtVersion.ForeColor = System.Drawing.Color.Transparent;
            this.txtVersion.Location = new System.Drawing.Point(84, 84);
            this.txtVersion.Name = "txtVersion";
            this.txtVersion.ReadOnly = true;
            this.txtVersion.Size = new System.Drawing.Size(100, 13);
            this.txtVersion.TabIndex = 6;
            this.txtVersion.TabStop = false;
            this.txtVersion.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.SteelBlue;
            this.label4.ForeColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(12, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Version";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.NewTab);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 107);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(800, 506);
            this.tabControl1.TabIndex = 7;
            this.tabControl1.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabControl1_Selecting);
            this.tabControl1.Click += new System.EventHandler(this.tabControl1_Click);
            // 
            // NewTab
            // 
            this.NewTab.ForeColor = System.Drawing.Color.IndianRed;
            this.NewTab.Location = new System.Drawing.Point(4, 22);
            this.NewTab.Name = "NewTab";
            this.NewTab.Padding = new System.Windows.Forms.Padding(3);
            this.NewTab.Size = new System.Drawing.Size(792, 480);
            this.NewTab.TabIndex = 0;
            this.NewTab.Text = "+";
            this.NewTab.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolsToolStripMenuItem,
            this.serverMonitorToolStripMenuItem,
            this.settingsToolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 11;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshPublicIPToolStripMenuItem1,
            this.refreshLocalIPToolStripMenuItem1,
            this.toolStripMenuItem2,
            this.updateSteamCMDToolStripMenuItem,
            this.toolStripMenuItem3,
            this.exitToolStripMenuItem1});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // serverMonitorToolStripMenuItem
            // 
            this.serverMonitorToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.serverMonitorToolStripMenuItem.Name = "serverMonitorToolStripMenuItem";
            this.serverMonitorToolStripMenuItem.Size = new System.Drawing.Size(97, 20);
            this.serverMonitorToolStripMenuItem.Text = "Server Monitor";
            this.serverMonitorToolStripMenuItem.Click += new System.EventHandler(this.serverMonitorToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem1
            // 
            this.settingsToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.routingFirewallToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.testsToolStripMenuItem});
            this.settingsToolStripMenuItem1.Name = "settingsToolStripMenuItem1";
            this.settingsToolStripMenuItem1.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem1.Text = "Settings";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(177, 6);
            // 
            // routingFirewallToolStripMenuItem
            // 
            this.routingFirewallToolStripMenuItem.Name = "routingFirewallToolStripMenuItem";
            this.routingFirewallToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.routingFirewallToolStripMenuItem.Text = "Port Foward";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // refreshPublicIPToolStripMenuItem1
            // 
            this.refreshPublicIPToolStripMenuItem1.Name = "refreshPublicIPToolStripMenuItem1";
            this.refreshPublicIPToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.refreshPublicIPToolStripMenuItem1.Text = "Refresh Public IP";
            this.refreshPublicIPToolStripMenuItem1.Click += new System.EventHandler(this.refreshPublicIPToolStripMenuItem1_Click);
            // 
            // refreshLocalIPToolStripMenuItem1
            // 
            this.refreshLocalIPToolStripMenuItem1.Name = "refreshLocalIPToolStripMenuItem1";
            this.refreshLocalIPToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.refreshLocalIPToolStripMenuItem1.Text = "Refresh Local IP";
            this.refreshLocalIPToolStripMenuItem1.Click += new System.EventHandler(this.refreshLocalIPToolStripMenuItem1_Click);
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
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.SteelBlue;
            this.label5.ForeColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(549, 35);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Auto-Backup";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.SteelBlue;
            this.label6.ForeColor = System.Drawing.Color.Transparent;
            this.label6.Location = new System.Drawing.Point(549, 62);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Auto-Update";
            // 
            // lblAutoBackup
            // 
            this.lblAutoBackup.AutoSize = true;
            this.lblAutoBackup.BackColor = System.Drawing.Color.SteelBlue;
            this.lblAutoBackup.ForeColor = System.Drawing.Color.Transparent;
            this.lblAutoBackup.Location = new System.Drawing.Point(622, 35);
            this.lblAutoBackup.Name = "lblAutoBackup";
            this.lblAutoBackup.Size = new System.Drawing.Size(38, 13);
            this.lblAutoBackup.TabIndex = 14;
            this.lblAutoBackup.Text = "Ready";
            // 
            // btRun1
            // 
            this.btRun1.BackColor = System.Drawing.Color.DarkGreen;
            this.btRun1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btRun1.ForeColor = System.Drawing.Color.White;
            this.btRun1.Location = new System.Drawing.Point(666, 31);
            this.btRun1.Name = "btRun1";
            this.btRun1.Size = new System.Drawing.Size(49, 21);
            this.btRun1.TabIndex = 15;
            this.btRun1.Text = "Run";
            this.btRun1.UseVisualStyleBackColor = false;
            this.btRun1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // btDisable1
            // 
            this.btDisable1.BackColor = System.Drawing.Color.Goldenrod;
            this.btDisable1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btDisable1.ForeColor = System.Drawing.Color.White;
            this.btDisable1.Location = new System.Drawing.Point(721, 31);
            this.btDisable1.Name = "btDisable1";
            this.btDisable1.Size = new System.Drawing.Size(56, 21);
            this.btDisable1.TabIndex = 16;
            this.btDisable1.Text = "Disable";
            this.btDisable1.UseVisualStyleBackColor = false;
            // 
            // btDisable2
            // 
            this.btDisable2.BackColor = System.Drawing.Color.Goldenrod;
            this.btDisable2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btDisable2.ForeColor = System.Drawing.Color.White;
            this.btDisable2.Location = new System.Drawing.Point(721, 58);
            this.btDisable2.Name = "btDisable2";
            this.btDisable2.Size = new System.Drawing.Size(56, 21);
            this.btDisable2.TabIndex = 19;
            this.btDisable2.Text = "Disable";
            this.btDisable2.UseVisualStyleBackColor = false;
            // 
            // btRun2
            // 
            this.btRun2.BackColor = System.Drawing.Color.DarkGreen;
            this.btRun2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btRun2.ForeColor = System.Drawing.Color.White;
            this.btRun2.Location = new System.Drawing.Point(666, 58);
            this.btRun2.Name = "btRun2";
            this.btRun2.Size = new System.Drawing.Size(49, 21);
            this.btRun2.TabIndex = 18;
            this.btRun2.Text = "Run";
            this.btRun2.UseVisualStyleBackColor = false;
            this.btRun2.Click += new System.EventHandler(this.button4_Click);
            // 
            // lblAutoUpdate
            // 
            this.lblAutoUpdate.AutoSize = true;
            this.lblAutoUpdate.BackColor = System.Drawing.Color.SteelBlue;
            this.lblAutoUpdate.ForeColor = System.Drawing.Color.Transparent;
            this.lblAutoUpdate.Location = new System.Drawing.Point(622, 62);
            this.lblAutoUpdate.Name = "lblAutoUpdate";
            this.lblAutoUpdate.Size = new System.Drawing.Size(38, 13);
            this.lblAutoUpdate.TabIndex = 17;
            this.lblAutoUpdate.Text = "Ready";
            // 
            // timerCheckTask
            // 
            this.timerCheckTask.Enabled = true;
            this.timerCheckTask.Interval = 500;
            this.timerCheckTask.Tick += new System.EventHandler(this.timerCheckTask_Tick);
            // 
            // updateSteamCMDToolStripMenuItem
            // 
            this.updateSteamCMDToolStripMenuItem.Name = "updateSteamCMDToolStripMenuItem";
            this.updateSteamCMDToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.updateSteamCMDToolStripMenuItem.Text = "Update SteamCMD";
            this.updateSteamCMDToolStripMenuItem.Click += new System.EventHandler(this.updateSteamCMDToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(177, 6);
            // 
            // testsToolStripMenuItem
            // 
            this.testsToolStripMenuItem.Name = "testsToolStripMenuItem";
            this.testsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.testsToolStripMenuItem.Text = "Tests";
            this.testsToolStripMenuItem.Click += new System.EventHandler(this.testsToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 613);
            this.Controls.Add(this.btDisable2);
            this.Controls.Add(this.btRun2);
            this.Controls.Add(this.lblAutoUpdate);
            this.Controls.Add(this.btDisable1);
            this.Controls.Add(this.btRun1);
            this.Controls.Add(this.lblAutoBackup);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.txtVersion);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtLocalIP);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtPublicIP);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ophiussa Server Manager";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPublicIP;
        private System.Windows.Forms.TextBox txtLocalIP;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtVersion;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage NewTab;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem serverMonitorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem routingFirewallToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem refreshPublicIPToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem refreshLocalIPToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblAutoBackup;
        private System.Windows.Forms.Button btRun1;
        private System.Windows.Forms.Button btDisable1;
        private System.Windows.Forms.Button btDisable2;
        private System.Windows.Forms.Button btRun2;
        private System.Windows.Forms.Label lblAutoUpdate;
        private System.Windows.Forms.Timer timerCheckTask;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem updateSteamCMDToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testsToolStripMenuItem;
    }
}

