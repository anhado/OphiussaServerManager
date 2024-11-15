﻿namespace OphiussaFramework.Forms {
    partial class FrmProgress {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkForceUpdateMods = new System.Windows.Forms.CheckBox();
            this.btClose = new System.Windows.Forms.Button();
            this.btUpdate = new System.Windows.Forms.Button();
            this.chkStartServer = new System.Windows.Forms.CheckBox();
            this.chkUpdateCache = new System.Windows.Forms.CheckBox();
            this.timer_updateBox = new System.Windows.Forms.Timer(this.components);
            this.chkShowSteam = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(0, 65);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(402, 305);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chkShowSteam);
            this.panel1.Controls.Add(this.chkForceUpdateMods);
            this.panel1.Controls.Add(this.btClose);
            this.panel1.Controls.Add(this.btUpdate);
            this.panel1.Controls.Add(this.chkStartServer);
            this.panel1.Controls.Add(this.chkUpdateCache);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(402, 65);
            this.panel1.TabIndex = 3;
            // 
            // chkForceUpdateMods
            // 
            this.chkForceUpdateMods.AutoSize = true;
            this.chkForceUpdateMods.Checked = true;
            this.chkForceUpdateMods.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkForceUpdateMods.Location = new System.Drawing.Point(143, 12);
            this.chkForceUpdateMods.Name = "chkForceUpdateMods";
            this.chkForceUpdateMods.Size = new System.Drawing.Size(115, 17);
            this.chkForceUpdateMods.TabIndex = 4;
            this.chkForceUpdateMods.Text = "Force Mod Update";
            this.chkForceUpdateMods.UseVisualStyleBackColor = true;
            this.chkForceUpdateMods.Visible = false;
            // 
            // btClose
            // 
            this.btClose.Location = new System.Drawing.Point(315, 35);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(75, 23);
            this.btClose.TabIndex = 3;
            this.btClose.Text = "Close";
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // btUpdate
            // 
            this.btUpdate.Location = new System.Drawing.Point(315, 8);
            this.btUpdate.Name = "btUpdate";
            this.btUpdate.Size = new System.Drawing.Size(75, 23);
            this.btUpdate.TabIndex = 2;
            this.btUpdate.Text = "Update";
            this.btUpdate.UseVisualStyleBackColor = true;
            this.btUpdate.Click += new System.EventHandler(this.btUpdate_Click);
            // 
            // chkStartServer
            // 
            this.chkStartServer.AutoSize = true;
            this.chkStartServer.Location = new System.Drawing.Point(12, 35);
            this.chkStartServer.Name = "chkStartServer";
            this.chkStartServer.Size = new System.Drawing.Size(132, 17);
            this.chkStartServer.TabIndex = 1;
            this.chkStartServer.Text = "Start Server in the end";
            this.chkStartServer.UseVisualStyleBackColor = true;
            // 
            // chkUpdateCache
            // 
            this.chkUpdateCache.AutoSize = true;
            this.chkUpdateCache.Checked = true;
            this.chkUpdateCache.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUpdateCache.Location = new System.Drawing.Point(12, 12);
            this.chkUpdateCache.Name = "chkUpdateCache";
            this.chkUpdateCache.Size = new System.Drawing.Size(125, 17);
            this.chkUpdateCache.TabIndex = 0;
            this.chkUpdateCache.Text = "Force Update Cache";
            this.chkUpdateCache.UseVisualStyleBackColor = true;
            // 
            // timer_updateBox
            // 
            this.timer_updateBox.Enabled = true;
            this.timer_updateBox.Tick += new System.EventHandler(this.timer_updateBox_Tick);
            // 
            // chkShowSteam
            // 
            this.chkShowSteam.AutoSize = true;
            this.chkShowSteam.Checked = true;
            this.chkShowSteam.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowSteam.Location = new System.Drawing.Point(143, 35);
            this.chkShowSteam.Name = "chkShowSteam";
            this.chkShowSteam.Size = new System.Drawing.Size(113, 17);
            this.chkShowSteam.TabIndex = 5;
            this.chkShowSteam.Text = "Show Steam CMD";
            this.chkShowSteam.UseVisualStyleBackColor = true;
            // 
            // FrmProgress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(402, 370);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.panel1);
            this.Name = "FrmProgress";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Install/Update Server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmProgress_FormClosing);
            this.Load += new System.EventHandler(this.FrmProgress_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chkForceUpdateMods;
        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.Button btUpdate;
        private System.Windows.Forms.CheckBox chkStartServer;
        private System.Windows.Forms.CheckBox chkUpdateCache;
        private System.Windows.Forms.Timer timer_updateBox;
        private System.Windows.Forms.CheckBox chkShowSteam;
    }
}