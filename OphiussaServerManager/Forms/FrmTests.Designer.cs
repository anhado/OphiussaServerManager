﻿namespace OphiussaServerManager.Forms
{
    partial class FrmTests
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTests));
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
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
            this.exListBox1 = new OphiussaServerManager.exListBox();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "STEAM MOD IDS";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(111, 6);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(590, 20);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = resources.GetString("textBox1.Text");
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(111, 32);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(590, 20);
            this.textBox2.TabIndex = 3;
            this.textBox2.Text = "928597,928621,928603,928539,929169,929578,929420,928501,930494,929785,931119,9298" +
    "68,932975,931047,932770,933355";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "CURSE MOD IDS";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(707, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Get Info";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(707, 30);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "Get Info";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
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
            // 
            // renamePlayerToolStripMenuItem
            // 
            this.renamePlayerToolStripMenuItem.Name = "renamePlayerToolStripMenuItem";
            this.renamePlayerToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.renamePlayerToolStripMenuItem.Text = "Rename Player";
            // 
            // renameTribeToolStripMenuItem
            // 
            this.renameTribeToolStripMenuItem.Name = "renameTribeToolStripMenuItem";
            this.renameTribeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.renameTribeToolStripMenuItem.Text = "Rename Tribe";
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
            // 
            // viewTribeToolStripMenuItem
            // 
            this.viewTribeToolStripMenuItem.Name = "viewTribeToolStripMenuItem";
            this.viewTribeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.viewTribeToolStripMenuItem.Text = "View Tribe";
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
            // 
            // copyPlayerIDToolStripMenuItem
            // 
            this.copyPlayerIDToolStripMenuItem.Name = "copyPlayerIDToolStripMenuItem";
            this.copyPlayerIDToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.copyPlayerIDToolStripMenuItem.Text = "Copy Player ID";
            // 
            // exListBox1
            // 
            this.exListBox1.ContextMenuStrip = this.contextMenuStrip1;
            this.exListBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.exListBox1.FormattingEnabled = true;
            this.exListBox1.ItemHeight = 66;
            this.exListBox1.Location = new System.Drawing.Point(15, 70);
            this.exListBox1.Name = "exListBox1";
            this.exListBox1.Size = new System.Drawing.Size(767, 170);
            this.exListBox1.TabIndex = 6;
            // 
            // FrmTests
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.exListBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmTests";
            this.Text = "Test Form";
            this.Load += new System.EventHandler(this.FrmTests_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private exListBox exListBox1;
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
    }
}