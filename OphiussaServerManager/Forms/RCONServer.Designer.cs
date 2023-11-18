namespace OphiussaServerManager.Forms
{
    partial class RCONServer
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewLogsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearLogsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.confirmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.consoleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoscrollToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serverToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveWorldToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.confirmToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.destroyWildDinosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.confirmToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.lblPlayers = new System.Windows.Forms.ToolStripMenuItem();
            this.playersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblStatus = new System.Windows.Forms.ToolStripMenuItem();
            this.statusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lbPlayers = new System.Windows.Forms.ListBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtChat = new System.Windows.Forms.RichTextBox();
            this.timerPlayers = new System.Windows.Forms.Timer(this.components);
            this.timersChat = new System.Windows.Forms.Timer(this.components);
            this.txtProfiles = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtInvalid = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.chatToPlayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renamePlayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renameTribeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.viewProfileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewTribeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.copyIDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyPlayerIDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timerConnection = new System.Windows.Forms.Timer(this.components);
            this.timerUpdatePlayersFromDisk = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.consoleToolStripMenuItem,
            this.serverToolStripMenuItem,
            this.lblPlayers,
            this.playersToolStripMenuItem,
            this.lblStatus,
            this.statusToolStripMenuItem,
            this.toolStripTextBox1,
            this.toolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 27);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewLogsToolStripMenuItem,
            this.clearLogsToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 23);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // viewLogsToolStripMenuItem
            // 
            this.viewLogsToolStripMenuItem.Name = "viewLogsToolStripMenuItem";
            this.viewLogsToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.viewLogsToolStripMenuItem.Text = "View Logs...";
            // 
            // clearLogsToolStripMenuItem
            // 
            this.clearLogsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.confirmToolStripMenuItem});
            this.clearLogsToolStripMenuItem.Name = "clearLogsToolStripMenuItem";
            this.clearLogsToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.clearLogsToolStripMenuItem.Text = "Clear Logs";
            // 
            // confirmToolStripMenuItem
            // 
            this.confirmToolStripMenuItem.Name = "confirmToolStripMenuItem";
            this.confirmToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.confirmToolStripMenuItem.Text = "Confirm";
            // 
            // consoleToolStripMenuItem
            // 
            this.consoleToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.autoscrollToolStripMenuItem});
            this.consoleToolStripMenuItem.Name = "consoleToolStripMenuItem";
            this.consoleToolStripMenuItem.Size = new System.Drawing.Size(62, 23);
            this.consoleToolStripMenuItem.Text = "Console";
            // 
            // autoscrollToolStripMenuItem
            // 
            this.autoscrollToolStripMenuItem.Name = "autoscrollToolStripMenuItem";
            this.autoscrollToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.autoscrollToolStripMenuItem.Text = "Auto-scroll";
            // 
            // serverToolStripMenuItem
            // 
            this.serverToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveWorldToolStripMenuItem,
            this.destroyWildDinosToolStripMenuItem});
            this.serverToolStripMenuItem.Name = "serverToolStripMenuItem";
            this.serverToolStripMenuItem.Size = new System.Drawing.Size(51, 23);
            this.serverToolStripMenuItem.Text = "Server";
            // 
            // saveWorldToolStripMenuItem
            // 
            this.saveWorldToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.confirmToolStripMenuItem1});
            this.saveWorldToolStripMenuItem.Name = "saveWorldToolStripMenuItem";
            this.saveWorldToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.saveWorldToolStripMenuItem.Text = "Save World";
            // 
            // confirmToolStripMenuItem1
            // 
            this.confirmToolStripMenuItem1.Name = "confirmToolStripMenuItem1";
            this.confirmToolStripMenuItem1.Size = new System.Drawing.Size(118, 22);
            this.confirmToolStripMenuItem1.Text = "Confirm";
            // 
            // destroyWildDinosToolStripMenuItem
            // 
            this.destroyWildDinosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.confirmToolStripMenuItem2});
            this.destroyWildDinosToolStripMenuItem.Name = "destroyWildDinosToolStripMenuItem";
            this.destroyWildDinosToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.destroyWildDinosToolStripMenuItem.Text = "Destroy Wild Dinos";
            // 
            // confirmToolStripMenuItem2
            // 
            this.confirmToolStripMenuItem2.Name = "confirmToolStripMenuItem2";
            this.confirmToolStripMenuItem2.Size = new System.Drawing.Size(118, 22);
            this.confirmToolStripMenuItem2.Text = "Confirm";
            // 
            // lblPlayers
            // 
            this.lblPlayers.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.lblPlayers.Name = "lblPlayers";
            this.lblPlayers.Size = new System.Drawing.Size(81, 23);
            this.lblPlayers.Text = "PlayersDesc";
            // 
            // playersToolStripMenuItem
            // 
            this.playersToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.playersToolStripMenuItem.Name = "playersToolStripMenuItem";
            this.playersToolStripMenuItem.Size = new System.Drawing.Size(56, 23);
            this.playersToolStripMenuItem.Text = "Players";
            // 
            // lblStatus
            // 
            this.lblStatus.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.lblStatus.ForeColor = System.Drawing.Color.IndianRed;
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(76, 23);
            this.lblStatus.Text = "StatusDesc";
            // 
            // statusToolStripMenuItem
            // 
            this.statusToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.statusToolStripMenuItem.Name = "statusToolStripMenuItem";
            this.statusToolStripMenuItem.Size = new System.Drawing.Size(54, 23);
            this.statusToolStripMenuItem.Text = "Status:";
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripTextBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Size = new System.Drawing.Size(100, 23);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(93, 23);
            this.toolStripMenuItem1.Text = "Admin Name:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.comboBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 416);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 34);
            this.panel1.TabIndex = 1;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(171, 6);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(617, 20);
            this.textBox1.TabIndex = 2;
            this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Mode";
            // 
            // comboBox1
            // 
            this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Command",
            "Global",
            "Broadcast"});
            this.comboBox1.Location = new System.Drawing.Point(44, 6);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtInvalid);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.txtProfiles);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.lbPlayers);
            this.panel2.Controls.Add(this.textBox2);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(606, 27);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(194, 389);
            this.panel2.TabIndex = 2;
            // 
            // lbPlayers
            // 
            this.lbPlayers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbPlayers.ContextMenuStrip = this.contextMenuStrip1;
            this.lbPlayers.FormattingEnabled = true;
            this.lbPlayers.Location = new System.Drawing.Point(6, 81);
            this.lbPlayers.MultiColumn = true;
            this.lbPlayers.Name = "lbPlayers";
            this.lbPlayers.Size = new System.Drawing.Size(182, 303);
            this.lbPlayers.TabIndex = 4;
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox2.Location = new System.Drawing.Point(37, 3);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(151, 20);
            this.textBox2.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Filter";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.txtChat);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 27);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(606, 389);
            this.panel3.TabIndex = 3;
            // 
            // txtChat
            // 
            this.txtChat.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtChat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtChat.Location = new System.Drawing.Point(0, 0);
            this.txtChat.Name = "txtChat";
            this.txtChat.Size = new System.Drawing.Size(606, 389);
            this.txtChat.TabIndex = 0;
            this.txtChat.Text = "";
            // 
            // timerPlayers
            // 
            this.timerPlayers.Enabled = true;
            this.timerPlayers.Interval = 5000;
            this.timerPlayers.Tick += new System.EventHandler(this.timerPlayers_Tick);
            // 
            // timersChat
            // 
            this.timersChat.Enabled = true;
            this.timersChat.Interval = 1000;
            this.timersChat.Tick += new System.EventHandler(this.timersChat_Tick);
            // 
            // txtProfiles
            // 
            this.txtProfiles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtProfiles.Enabled = false;
            this.txtProfiles.Location = new System.Drawing.Point(49, 29);
            this.txtProfiles.Name = "txtProfiles";
            this.txtProfiles.Size = new System.Drawing.Size(139, 20);
            this.txtProfiles.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Profiles";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // txtInvalid
            // 
            this.txtInvalid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtInvalid.Enabled = false;
            this.txtInvalid.Location = new System.Drawing.Point(49, 55);
            this.txtInvalid.Name = "txtInvalid";
            this.txtInvalid.Size = new System.Drawing.Size(139, 20);
            this.txtInvalid.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Invalid";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.chatToPlayerToolStripMenuItem,
            this.renamePlayerToolStripMenuItem,
            this.renameTribeToolStripMenuItem,
            this.toolStripMenuItem2,
            this.viewProfileToolStripMenuItem,
            this.viewTribeToolStripMenuItem,
            this.toolStripMenuItem3,
            this.copyIDToolStripMenuItem,
            this.copyPlayerIDToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 170);
            // 
            // chatToPlayerToolStripMenuItem
            // 
            this.chatToPlayerToolStripMenuItem.Name = "chatToPlayerToolStripMenuItem";
            this.chatToPlayerToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.chatToPlayerToolStripMenuItem.Text = "Chat To Player";
            this.chatToPlayerToolStripMenuItem.Click += new System.EventHandler(this.chatToPlayerToolStripMenuItem_Click);
            // 
            // renamePlayerToolStripMenuItem
            // 
            this.renamePlayerToolStripMenuItem.Name = "renamePlayerToolStripMenuItem";
            this.renamePlayerToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.renamePlayerToolStripMenuItem.Text = "Rename Player";
            this.renamePlayerToolStripMenuItem.Click += new System.EventHandler(this.renamePlayerToolStripMenuItem_Click);
            // 
            // renameTribeToolStripMenuItem
            // 
            this.renameTribeToolStripMenuItem.Name = "renameTribeToolStripMenuItem";
            this.renameTribeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.renameTribeToolStripMenuItem.Text = "Rename Tribe";
            this.renameTribeToolStripMenuItem.Click += new System.EventHandler(this.renameTribeToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(149, 6);
            // 
            // viewProfileToolStripMenuItem
            // 
            this.viewProfileToolStripMenuItem.Name = "viewProfileToolStripMenuItem";
            this.viewProfileToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.viewProfileToolStripMenuItem.Text = "View Profile";
            this.viewProfileToolStripMenuItem.Click += new System.EventHandler(this.viewProfileToolStripMenuItem_Click);
            // 
            // viewTribeToolStripMenuItem
            // 
            this.viewTribeToolStripMenuItem.Name = "viewTribeToolStripMenuItem";
            this.viewTribeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.viewTribeToolStripMenuItem.Text = "View Tribe";
            this.viewTribeToolStripMenuItem.Click += new System.EventHandler(this.viewTribeToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(149, 6);
            // 
            // copyIDToolStripMenuItem
            // 
            this.copyIDToolStripMenuItem.Name = "copyIDToolStripMenuItem";
            this.copyIDToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.copyIDToolStripMenuItem.Text = "Copy ID";
            this.copyIDToolStripMenuItem.Click += new System.EventHandler(this.copyIDToolStripMenuItem_Click);
            // 
            // copyPlayerIDToolStripMenuItem
            // 
            this.copyPlayerIDToolStripMenuItem.Name = "copyPlayerIDToolStripMenuItem";
            this.copyPlayerIDToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.copyPlayerIDToolStripMenuItem.Text = "Copy Player ID";
            this.copyPlayerIDToolStripMenuItem.Click += new System.EventHandler(this.copyPlayerIDToolStripMenuItem_Click);
            // 
            // timerConnection
            // 
            this.timerConnection.Enabled = true;
            this.timerConnection.Interval = 1000;
            this.timerConnection.Tick += new System.EventHandler(this.timerConnection_Tick);
            // 
            // timerUpdatePlayersFromDisk
            // 
            this.timerUpdatePlayersFromDisk.Enabled = true;
            this.timerUpdatePlayersFromDisk.Interval = 10000;
            this.timerUpdatePlayersFromDisk.Tick += new System.EventHandler(this.timerUpdatePlayersFromDisk_Tick);
            // 
            // RCONServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.ForeColor = System.Drawing.Color.SteelBlue;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "RCONServer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RCON";
            this.Load += new System.EventHandler(this.RCONServer_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewLogsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearLogsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem confirmToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem consoleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem autoscrollToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem serverToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveWorldToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem confirmToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem destroyWildDinosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem confirmToolStripMenuItem2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox lbPlayers;
        private System.Windows.Forms.ToolStripMenuItem lblPlayers;
        private System.Windows.Forms.ToolStripMenuItem playersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lblStatus;
        private System.Windows.Forms.ToolStripMenuItem statusToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RichTextBox txtChat;
        private System.Windows.Forms.Timer timerPlayers;
        private System.Windows.Forms.Timer timersChat;
        private System.Windows.Forms.TextBox txtProfiles;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtInvalid;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem chatToPlayerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renamePlayerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renameTribeToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem viewProfileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewTribeToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem copyIDToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyPlayerIDToolStripMenuItem;
        private System.Windows.Forms.Timer timerConnection;
        private System.Windows.Forms.Timer timerUpdatePlayersFromDisk;
    }
}