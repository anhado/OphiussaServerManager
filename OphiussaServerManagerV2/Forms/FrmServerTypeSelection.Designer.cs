namespace OphiussaServerManagerV2
{
    partial class FrmServerTypeSelection
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
            this.fdDiag = new System.Windows.Forms.FolderBrowserDialog();
            this.osmThemeContainer1 = new OSMThemeContainer();
            this.osmButton_13 = new OSMButton_1();
            this.osmButton_12 = new OSMButton_1();
            this.osmButton_11 = new OSMButton_1();
            this.osmLabel3 = new OSMLabel();
            this.txtDirName = new OSMTextBox_Small();
            this.chkUsedInstall = new OSMCheckBox();
            this.osmLabel2 = new OSMLabel();
            this.cboServerType = new OSMComboBox();
            this.txtDir = new OSMTextBox_Small();
            this.osmLabel1 = new OSMLabel();
            this.osmControlBox1 = new OSMControlBox();
            this.osmThemeContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // osmThemeContainer1
            // 
            this.osmThemeContainer1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.osmThemeContainer1.Controls.Add(this.osmButton_13);
            this.osmThemeContainer1.Controls.Add(this.osmButton_12);
            this.osmThemeContainer1.Controls.Add(this.osmButton_11);
            this.osmThemeContainer1.Controls.Add(this.osmLabel3);
            this.osmThemeContainer1.Controls.Add(this.txtDirName);
            this.osmThemeContainer1.Controls.Add(this.chkUsedInstall);
            this.osmThemeContainer1.Controls.Add(this.osmLabel2);
            this.osmThemeContainer1.Controls.Add(this.cboServerType);
            this.osmThemeContainer1.Controls.Add(this.txtDir);
            this.osmThemeContainer1.Controls.Add(this.osmLabel1);
            this.osmThemeContainer1.Controls.Add(this.osmControlBox1);
            this.osmThemeContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.osmThemeContainer1.DrawBottomBar = true;
            this.osmThemeContainer1.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.osmThemeContainer1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.osmThemeContainer1.Location = new System.Drawing.Point(0, 0);
            this.osmThemeContainer1.Name = "osmThemeContainer1";
            this.osmThemeContainer1.Padding = new System.Windows.Forms.Padding(3, 28, 3, 28);
            this.osmThemeContainer1.Size = new System.Drawing.Size(736, 235);
            this.osmThemeContainer1.TabIndex = 20;
            this.osmThemeContainer1.Text = "Add New Server";
            this.osmThemeContainer1.TextBottom = null;
            // 
            // osmButton_13
            // 
            this.osmButton_13.BackColor = System.Drawing.Color.Transparent;
            this.osmButton_13.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.osmButton_13.Image = null;
            this.osmButton_13.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.osmButton_13.Location = new System.Drawing.Point(679, 70);
            this.osmButton_13.Name = "osmButton_13";
            this.osmButton_13.Size = new System.Drawing.Size(51, 28);
            this.osmButton_13.TabIndex = 21;
            this.osmButton_13.Text = "...";
            this.osmButton_13.TextAlignment = System.Drawing.StringAlignment.Center;
            this.osmButton_13.Click += new System.EventHandler(this.button1_Click);
            // 
            // osmButton_12
            // 
            this.osmButton_12.BackColor = System.Drawing.Color.Transparent;
            this.osmButton_12.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.osmButton_12.Image = null;
            this.osmButton_12.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.osmButton_12.Location = new System.Drawing.Point(564, 159);
            this.osmButton_12.Name = "osmButton_12";
            this.osmButton_12.Size = new System.Drawing.Size(166, 40);
            this.osmButton_12.TabIndex = 20;
            this.osmButton_12.Text = "Add";
            this.osmButton_12.TextAlignment = System.Drawing.StringAlignment.Center;
            this.osmButton_12.Click += new System.EventHandler(this.btAdd_Click);
            // 
            // osmButton_11
            // 
            this.osmButton_11.BackColor = System.Drawing.Color.Transparent;
            this.osmButton_11.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.osmButton_11.Image = null;
            this.osmButton_11.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.osmButton_11.Location = new System.Drawing.Point(18, 159);
            this.osmButton_11.Name = "osmButton_11";
            this.osmButton_11.Size = new System.Drawing.Size(166, 40);
            this.osmButton_11.TabIndex = 19;
            this.osmButton_11.Text = "Cancel";
            this.osmButton_11.TextAlignment = System.Drawing.StringAlignment.Center;
            this.osmButton_11.Click += new System.EventHandler(this.osmButton_11_Click);
            // 
            // osmLabel3
            // 
            this.osmLabel3.AutoSize = true;
            this.osmLabel3.BackColor = System.Drawing.Color.Transparent;
            this.osmLabel3.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.osmLabel3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.osmLabel3.Location = new System.Drawing.Point(14, 129);
            this.osmLabel3.Name = "osmLabel3";
            this.osmLabel3.Size = new System.Drawing.Size(114, 20);
            this.osmLabel3.TabIndex = 17;
            this.osmLabel3.Text = "Directory Name";
            // 
            // txtDirName
            // 
            this.txtDirName.BackColor = System.Drawing.Color.Transparent;
            this.txtDirName.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtDirName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtDirName.Location = new System.Drawing.Point(133, 125);
            this.txtDirName.MaxLength = 32767;
            this.txtDirName.Multiline = false;
            this.txtDirName.Name = "txtDirName";
            this.txtDirName.ReadOnly = false;
            this.txtDirName.Size = new System.Drawing.Size(540, 28);
            this.txtDirName.TabIndex = 16;
            this.txtDirName.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtDirName.UseSystemPasswordChar = false;
            this.txtDirName.Value = "";
            // 
            // chkUsedInstall
            // 
            this.chkUsedInstall.BackColor = System.Drawing.Color.Transparent;
            this.chkUsedInstall.Checked = false;
            this.chkUsedInstall.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.chkUsedInstall.Location = new System.Drawing.Point(133, 104);
            this.chkUsedInstall.Name = "chkUsedInstall";
            this.chkUsedInstall.Size = new System.Drawing.Size(305, 15);
            this.chkUsedInstall.TabIndex = 5;
            this.chkUsedInstall.Text = "Use Existing Installation Folder";
            this.chkUsedInstall.CheckedChanged += new OSMCheckBox.CheckedChangedEventHandler(this.chkUsedInstall_CheckedChanged);
            // 
            // osmLabel2
            // 
            this.osmLabel2.AutoSize = true;
            this.osmLabel2.BackColor = System.Drawing.Color.Transparent;
            this.osmLabel2.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.osmLabel2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.osmLabel2.Location = new System.Drawing.Point(14, 74);
            this.osmLabel2.Name = "osmLabel2";
            this.osmLabel2.Size = new System.Drawing.Size(113, 20);
            this.osmLabel2.TabIndex = 4;
            this.osmLabel2.Text = "Install Directory";
            // 
            // cboServerType
            // 
            this.cboServerType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.cboServerType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboServerType.DropDownHeight = 100;
            this.cboServerType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboServerType.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboServerType.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cboServerType.FormattingEnabled = true;
            this.cboServerType.HoverSelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            this.cboServerType.IntegralHeight = false;
            this.cboServerType.ItemHeight = 20;
            this.cboServerType.Location = new System.Drawing.Point(130, 38);
            this.cboServerType.Name = "cboServerType";
            this.cboServerType.Size = new System.Drawing.Size(308, 26);
            this.cboServerType.StartIndex = 0;
            this.cboServerType.TabIndex = 3;
            // 
            // txtDir
            // 
            this.txtDir.BackColor = System.Drawing.Color.Transparent;
            this.txtDir.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtDir.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtDir.Location = new System.Drawing.Point(133, 70);
            this.txtDir.MaxLength = 32767;
            this.txtDir.Multiline = false;
            this.txtDir.Name = "txtDir";
            this.txtDir.ReadOnly = false;
            this.txtDir.Size = new System.Drawing.Size(540, 28);
            this.txtDir.TabIndex = 2;
            this.txtDir.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtDir.UseSystemPasswordChar = false;
            this.txtDir.Value = "";
            // 
            // osmLabel1
            // 
            this.osmLabel1.AutoSize = true;
            this.osmLabel1.BackColor = System.Drawing.Color.Transparent;
            this.osmLabel1.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.osmLabel1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.osmLabel1.Location = new System.Drawing.Point(14, 39);
            this.osmLabel1.Name = "osmLabel1";
            this.osmLabel1.Size = new System.Drawing.Size(85, 20);
            this.osmLabel1.TabIndex = 1;
            this.osmLabel1.Text = "Server Type";
            // 
            // osmControlBox1
            // 
            this.osmControlBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.osmControlBox1.BackColor = System.Drawing.Color.Transparent;
            this.osmControlBox1.Location = new System.Drawing.Point(659, 0);
            this.osmControlBox1.MinimizeBox = false;
            this.osmControlBox1.Name = "osmControlBox1";
            this.osmControlBox1.Size = new System.Drawing.Size(77, 19);
            this.osmControlBox1.TabIndex = 0;
            this.osmControlBox1.Text = "osmControlBox1";
            // 
            // FrmServerTypeSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(736, 235);
            this.Controls.Add(this.osmThemeContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmServerTypeSelection";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add New Server";
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.Load += new System.EventHandler(this.FrmServerTypeSelection_Load);
            this.osmThemeContainer1.ResumeLayout(false);
            this.osmThemeContainer1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.FolderBrowserDialog fdDiag;
        private OSMThemeContainer osmThemeContainer1;
        private OSMControlBox osmControlBox1;
        private OSMLabel osmLabel2;
        private OSMComboBox cboServerType;
        private OSMTextBox_Small txtDir;
        private OSMLabel osmLabel1;
        private OSMCheckBox chkUsedInstall;
        private OSMLabel osmLabel3;
        private OSMTextBox_Small txtDirName;
        private OSMButton_1 osmButton_13;
        private OSMButton_1 osmButton_12;
        private OSMButton_1 osmButton_11;
    }
}