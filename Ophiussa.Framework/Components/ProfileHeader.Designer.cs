namespace OphiussaFramework.Components {
    partial class ProfileHeader {
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.timerGetProcess = new System.Windows.Forms.Timer(this.components);
            this.fBD = new System.Windows.Forms.FolderBrowserDialog();
            this.txtDummy = new OSMTextBox_Small();
            this.osmLabel7 = new OSMLabel();
            this.txtBuild = new OSMTextBox_Small();
            this.osmLabel6 = new OSMLabel();
            this.txtVersion = new OSMTextBox_Small();
            this.osmLabel5 = new OSMLabel();
            this.cboBranch = new OSMComboBox();
            this.btChooseFolder = new OSMButton_1();
            this.btStart = new OSMButton_1();
            this.btRCON = new OSMButton_1();
            this.btUpdate = new OSMButton_1();
            this.osmButton_12 = new OSMButton_1();
            this.osmButton_11 = new OSMButton_1();
            this.btReload = new OSMButton_1();
            this.osmLabel3 = new OSMLabel();
            this.txtLocation = new OSMTextBox_Small();
            this.osmLabel4 = new OSMLabel();
            this.txtServerType = new OSMTextBox_Small();
            this.osmLabel2 = new OSMLabel();
            this.txtProfileName = new OSMTextBox_Small();
            this.OSMLabel1 = new OSMLabel();
            this.txtProfileID = new OSMTextBox_Small();
            this.SuspendLayout();
            // 
            // timerGetProcess
            // 
            this.timerGetProcess.Interval = 500;
            this.timerGetProcess.Tick += new System.EventHandler(this.timerGetProcess_Tick);
            // 
            // txtDummy
            // 
            this.txtDummy.BackColor = System.Drawing.Color.Transparent;
            this.txtDummy.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtDummy.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtDummy.Location = new System.Drawing.Point(-675, -177);
            this.txtDummy.MaxLength = 32767;
            this.txtDummy.Multiline = false;
            this.txtDummy.Name = "txtDummy";
            this.txtDummy.ReadOnly = false;
            this.txtDummy.Size = new System.Drawing.Size(135, 28);
            this.txtDummy.TabIndex = 40;
            this.txtDummy.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtDummy.UseSystemPasswordChar = false;
            this.txtDummy.Value = "";
            // 
            // osmLabel7
            // 
            this.osmLabel7.AutoSize = true;
            this.osmLabel7.BackColor = System.Drawing.Color.Transparent;
            this.osmLabel7.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.osmLabel7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.osmLabel7.Location = new System.Drawing.Point(248, 179);
            this.osmLabel7.Name = "osmLabel7";
            this.osmLabel7.Size = new System.Drawing.Size(95, 20);
            this.osmLabel7.TabIndex = 39;
            this.osmLabel7.Text = "Build Version";
            // 
            // txtBuild
            // 
            this.txtBuild.BackColor = System.Drawing.Color.Transparent;
            this.txtBuild.Enabled = false;
            this.txtBuild.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtBuild.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtBuild.Location = new System.Drawing.Point(349, 171);
            this.txtBuild.MaxLength = 32767;
            this.txtBuild.Multiline = false;
            this.txtBuild.Name = "txtBuild";
            this.txtBuild.ReadOnly = false;
            this.txtBuild.Size = new System.Drawing.Size(107, 28);
            this.txtBuild.TabIndex = 38;
            this.txtBuild.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtBuild.UseSystemPasswordChar = false;
            this.txtBuild.Value = "";
            // 
            // osmLabel6
            // 
            this.osmLabel6.AutoSize = true;
            this.osmLabel6.BackColor = System.Drawing.Color.Transparent;
            this.osmLabel6.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.osmLabel6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.osmLabel6.Location = new System.Drawing.Point(3, 179);
            this.osmLabel6.Name = "osmLabel6";
            this.osmLabel6.Size = new System.Drawing.Size(117, 20);
            this.osmLabel6.TabIndex = 37;
            this.osmLabel6.Text = "Installed Version";
            // 
            // txtVersion
            // 
            this.txtVersion.BackColor = System.Drawing.Color.Transparent;
            this.txtVersion.Enabled = false;
            this.txtVersion.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtVersion.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtVersion.Location = new System.Drawing.Point(135, 171);
            this.txtVersion.MaxLength = 32767;
            this.txtVersion.Multiline = false;
            this.txtVersion.Name = "txtVersion";
            this.txtVersion.ReadOnly = false;
            this.txtVersion.Size = new System.Drawing.Size(107, 28);
            this.txtVersion.TabIndex = 36;
            this.txtVersion.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtVersion.UseSystemPasswordChar = false;
            this.txtVersion.Value = "";
            // 
            // osmLabel5
            // 
            this.osmLabel5.AutoSize = true;
            this.osmLabel5.BackColor = System.Drawing.Color.Transparent;
            this.osmLabel5.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.osmLabel5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.osmLabel5.Location = new System.Drawing.Point(3, 145);
            this.osmLabel5.Name = "osmLabel5";
            this.osmLabel5.Size = new System.Drawing.Size(54, 20);
            this.osmLabel5.TabIndex = 35;
            this.osmLabel5.Text = "Branch";
            // 
            // cboBranch
            // 
            this.cboBranch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboBranch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.cboBranch.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboBranch.DropDownHeight = 100;
            this.cboBranch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBranch.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cboBranch.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cboBranch.FormattingEnabled = true;
            this.cboBranch.HoverSelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            this.cboBranch.IntegralHeight = false;
            this.cboBranch.ItemHeight = 20;
            this.cboBranch.Location = new System.Drawing.Point(134, 139);
            this.cboBranch.Name = "cboBranch";
            this.cboBranch.Size = new System.Drawing.Size(464, 26);
            this.cboBranch.StartIndex = 0;
            this.cboBranch.TabIndex = 34;
            // 
            // btChooseFolder
            // 
            this.btChooseFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btChooseFolder.BackColor = System.Drawing.Color.Transparent;
            this.btChooseFolder.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btChooseFolder.Image = null;
            this.btChooseFolder.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btChooseFolder.Location = new System.Drawing.Point(604, 105);
            this.btChooseFolder.Name = "btChooseFolder";
            this.btChooseFolder.Size = new System.Drawing.Size(33, 28);
            this.btChooseFolder.TabIndex = 33;
            this.btChooseFolder.Text = "...";
            this.btChooseFolder.TextAlignment = System.Drawing.StringAlignment.Center;
            this.btChooseFolder.Click += new System.EventHandler(this.btChooseFolder_Click);
            // 
            // btStart
            // 
            this.btStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btStart.BackColor = System.Drawing.Color.Transparent;
            this.btStart.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btStart.Image = null;
            this.btStart.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btStart.Location = new System.Drawing.Point(604, 71);
            this.btStart.Name = "btStart";
            this.btStart.Size = new System.Drawing.Size(79, 28);
            this.btStart.TabIndex = 32;
            this.btStart.Text = "Start";
            this.btStart.TextAlignment = System.Drawing.StringAlignment.Center;
            this.btStart.Click += new System.EventHandler(this.btStart_Click);
            // 
            // btRCON
            // 
            this.btRCON.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btRCON.BackColor = System.Drawing.Color.Transparent;
            this.btRCON.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btRCON.Image = global::OphiussaFramework.Properties.Resources.consoleicon;
            this.btRCON.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btRCON.Location = new System.Drawing.Point(689, 71);
            this.btRCON.Name = "btRCON";
            this.btRCON.Size = new System.Drawing.Size(79, 28);
            this.btRCON.TabIndex = 31;
            this.btRCON.Text = "RCON";
            this.btRCON.TextAlignment = System.Drawing.StringAlignment.Center;
            this.btRCON.Click += new System.EventHandler(this.btRCON_Click);
            // 
            // btUpdate
            // 
            this.btUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btUpdate.BackColor = System.Drawing.Color.Transparent;
            this.btUpdate.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btUpdate.Image = global::OphiussaFramework.Properties.Resources.Refresh_icon;
            this.btUpdate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btUpdate.Location = new System.Drawing.Point(604, 37);
            this.btUpdate.Name = "btUpdate";
            this.btUpdate.Size = new System.Drawing.Size(164, 28);
            this.btUpdate.TabIndex = 30;
            this.btUpdate.Text = "Update/Verify";
            this.btUpdate.TextAlignment = System.Drawing.StringAlignment.Center;
            this.btUpdate.Click += new System.EventHandler(this.btUpdate_Click);
            // 
            // osmButton_12
            // 
            this.osmButton_12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.osmButton_12.BackColor = System.Drawing.Color.Transparent;
            this.osmButton_12.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.osmButton_12.Image = global::OphiussaFramework.Properties.Resources.SaveIcon;
            this.osmButton_12.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.osmButton_12.Location = new System.Drawing.Point(689, 3);
            this.osmButton_12.Name = "osmButton_12";
            this.osmButton_12.Size = new System.Drawing.Size(79, 28);
            this.osmButton_12.TabIndex = 29;
            this.osmButton_12.Text = "Save";
            this.osmButton_12.TextAlignment = System.Drawing.StringAlignment.Center;
            this.osmButton_12.Click += new System.EventHandler(this.btSave_Click);
            // 
            // osmButton_11
            // 
            this.osmButton_11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.osmButton_11.BackColor = System.Drawing.Color.Transparent;
            this.osmButton_11.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.osmButton_11.Image = global::OphiussaFramework.Properties.Resources.Copy_icon_icon;
            this.osmButton_11.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.osmButton_11.Location = new System.Drawing.Point(604, 3);
            this.osmButton_11.Name = "osmButton_11";
            this.osmButton_11.Size = new System.Drawing.Size(79, 28);
            this.osmButton_11.TabIndex = 28;
            this.osmButton_11.Text = "Sync";
            this.osmButton_11.TextAlignment = System.Drawing.StringAlignment.Center;
            this.osmButton_11.Click += new System.EventHandler(this.btSync_Click);
            // 
            // btReload
            // 
            this.btReload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btReload.BackColor = System.Drawing.Color.Transparent;
            this.btReload.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btReload.Image = null;
            this.btReload.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btReload.Location = new System.Drawing.Point(519, 3);
            this.btReload.Name = "btReload";
            this.btReload.Size = new System.Drawing.Size(79, 28);
            this.btReload.TabIndex = 27;
            this.btReload.Text = "Reload";
            this.btReload.TextAlignment = System.Drawing.StringAlignment.Center;
            this.btReload.Click += new System.EventHandler(this.btReload_Click);
            // 
            // osmLabel3
            // 
            this.osmLabel3.AutoSize = true;
            this.osmLabel3.BackColor = System.Drawing.Color.Transparent;
            this.osmLabel3.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.osmLabel3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.osmLabel3.Location = new System.Drawing.Point(3, 113);
            this.osmLabel3.Name = "osmLabel3";
            this.osmLabel3.Size = new System.Drawing.Size(128, 20);
            this.osmLabel3.TabIndex = 26;
            this.osmLabel3.Text = "Installation Folder";
            // 
            // txtLocation
            // 
            this.txtLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLocation.BackColor = System.Drawing.Color.Transparent;
            this.txtLocation.Enabled = false;
            this.txtLocation.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtLocation.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtLocation.Location = new System.Drawing.Point(135, 105);
            this.txtLocation.MaxLength = 32767;
            this.txtLocation.Multiline = false;
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.ReadOnly = false;
            this.txtLocation.Size = new System.Drawing.Size(463, 28);
            this.txtLocation.TabIndex = 25;
            this.txtLocation.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtLocation.UseSystemPasswordChar = false;
            this.txtLocation.Value = "";
            // 
            // osmLabel4
            // 
            this.osmLabel4.AutoSize = true;
            this.osmLabel4.BackColor = System.Drawing.Color.Transparent;
            this.osmLabel4.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.osmLabel4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.osmLabel4.Location = new System.Drawing.Point(3, 79);
            this.osmLabel4.Name = "osmLabel4";
            this.osmLabel4.Size = new System.Drawing.Size(85, 20);
            this.osmLabel4.TabIndex = 24;
            this.osmLabel4.Text = "Server Type";
            this.osmLabel4.Click += new System.EventHandler(this.osmLabel4_Click);
            // 
            // txtServerType
            // 
            this.txtServerType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtServerType.BackColor = System.Drawing.Color.Transparent;
            this.txtServerType.Enabled = false;
            this.txtServerType.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtServerType.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtServerType.Location = new System.Drawing.Point(135, 71);
            this.txtServerType.MaxLength = 32767;
            this.txtServerType.Multiline = false;
            this.txtServerType.Name = "txtServerType";
            this.txtServerType.ReadOnly = false;
            this.txtServerType.Size = new System.Drawing.Size(463, 28);
            this.txtServerType.TabIndex = 23;
            this.txtServerType.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtServerType.UseSystemPasswordChar = false;
            this.txtServerType.Value = "";
            // 
            // osmLabel2
            // 
            this.osmLabel2.AutoSize = true;
            this.osmLabel2.BackColor = System.Drawing.Color.Transparent;
            this.osmLabel2.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.osmLabel2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.osmLabel2.Location = new System.Drawing.Point(3, 45);
            this.osmLabel2.Name = "osmLabel2";
            this.osmLabel2.Size = new System.Drawing.Size(96, 20);
            this.osmLabel2.TabIndex = 22;
            this.osmLabel2.Text = "Profile Name";
            // 
            // txtProfileName
            // 
            this.txtProfileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtProfileName.BackColor = System.Drawing.Color.Transparent;
            this.txtProfileName.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtProfileName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtProfileName.Location = new System.Drawing.Point(135, 37);
            this.txtProfileName.MaxLength = 32767;
            this.txtProfileName.Multiline = false;
            this.txtProfileName.Name = "txtProfileName";
            this.txtProfileName.ReadOnly = false;
            this.txtProfileName.Size = new System.Drawing.Size(463, 28);
            this.txtProfileName.TabIndex = 21;
            this.txtProfileName.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtProfileName.UseSystemPasswordChar = false;
            this.txtProfileName.Value = "";
            this.txtProfileName.Validated += new System.EventHandler(this.txtProfileName_Validated);
            // 
            // OSMLabel1
            // 
            this.OSMLabel1.AutoSize = true;
            this.OSMLabel1.BackColor = System.Drawing.Color.Transparent;
            this.OSMLabel1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OSMLabel1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.OSMLabel1.Location = new System.Drawing.Point(3, 11);
            this.OSMLabel1.Name = "OSMLabel1";
            this.OSMLabel1.Size = new System.Drawing.Size(71, 20);
            this.OSMLabel1.TabIndex = 20;
            this.OSMLabel1.Text = "Profile ID";
            // 
            // txtProfileID
            // 
            this.txtProfileID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtProfileID.BackColor = System.Drawing.Color.Transparent;
            this.txtProfileID.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtProfileID.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtProfileID.Location = new System.Drawing.Point(135, 3);
            this.txtProfileID.MaxLength = 32767;
            this.txtProfileID.Multiline = false;
            this.txtProfileID.Name = "txtProfileID";
            this.txtProfileID.ReadOnly = true;
            this.txtProfileID.Size = new System.Drawing.Size(380, 28);
            this.txtProfileID.TabIndex = 19;
            this.txtProfileID.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtProfileID.UseSystemPasswordChar = false;
            this.txtProfileID.Value = "";
            // 
            // ProfileHeader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.txtDummy);
            this.Controls.Add(this.osmLabel7);
            this.Controls.Add(this.txtBuild);
            this.Controls.Add(this.osmLabel6);
            this.Controls.Add(this.txtVersion);
            this.Controls.Add(this.osmLabel5);
            this.Controls.Add(this.cboBranch);
            this.Controls.Add(this.btChooseFolder);
            this.Controls.Add(this.btStart);
            this.Controls.Add(this.btRCON);
            this.Controls.Add(this.btUpdate);
            this.Controls.Add(this.osmButton_12);
            this.Controls.Add(this.osmButton_11);
            this.Controls.Add(this.btReload);
            this.Controls.Add(this.osmLabel3);
            this.Controls.Add(this.txtLocation);
            this.Controls.Add(this.osmLabel4);
            this.Controls.Add(this.txtServerType);
            this.Controls.Add(this.osmLabel2);
            this.Controls.Add(this.txtProfileName);
            this.Controls.Add(this.OSMLabel1);
            this.Controls.Add(this.txtProfileID);
            this.Name = "ProfileHeader";
            this.Size = new System.Drawing.Size(772, 204);
            this.Load += new System.EventHandler(this.ProfileHeader_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer timerGetProcess;
        private System.Windows.Forms.FolderBrowserDialog fBD;
        private OSMTextBox_Small txtProfileID;
        private OSMLabel OSMLabel1;
        private OSMTextBox_Small txtProfileName;
        private OSMLabel osmLabel2;
        private OSMLabel osmLabel3;
        private OSMTextBox_Small txtLocation;
        private OSMLabel osmLabel4;
        private OSMTextBox_Small txtServerType;
        private OSMButton_1 btReload;
        private OSMButton_1 osmButton_11;
        private OSMButton_1 osmButton_12;
        private OSMButton_1 btUpdate;
        private OSMButton_1 btRCON;
        private OSMButton_1 btStart;
        private OSMButton_1 btChooseFolder;
        private OSMComboBox cboBranch;
        private OSMLabel osmLabel5;
        private OSMLabel osmLabel6;
        private OSMTextBox_Small txtVersion;
        private OSMLabel osmLabel7;
        private OSMTextBox_Small txtBuild;
        private OSMTextBox_Small txtDummy;
    }
}
