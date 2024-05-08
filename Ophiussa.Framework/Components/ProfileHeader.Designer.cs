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
            this.txtBuild = new System.Windows.Forms.TextBox();
            this.label35 = new System.Windows.Forms.Label();
            this.txtVersion = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtServerType = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btReload = new System.Windows.Forms.Button();
            this.btRCON = new System.Windows.Forms.Button();
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
            this.timerGetProcess = new System.Windows.Forms.Timer(this.components);
            this.fBD = new System.Windows.Forms.FolderBrowserDialog();
            this.cboBranch = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtBeta = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtBetaPassword = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtBuild
            // 
            this.txtBuild.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBuild.Location = new System.Drawing.Point(527, 86);
            this.txtBuild.Name = "txtBuild";
            this.txtBuild.ReadOnly = true;
            this.txtBuild.Size = new System.Drawing.Size(84, 20);
            this.txtBuild.TabIndex = 18;
            // 
            // label35
            // 
            this.label35.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(453, 92);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(30, 13);
            this.label35.TabIndex = 17;
            this.label35.Text = "Build";
            // 
            // txtVersion
            // 
            this.txtVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtVersion.Location = new System.Drawing.Point(527, 112);
            this.txtVersion.Name = "txtVersion";
            this.txtVersion.ReadOnly = true;
            this.txtVersion.Size = new System.Drawing.Size(84, 20);
            this.txtVersion.TabIndex = 16;
            this.txtVersion.Tag = "Version";
            // 
            // label16
            // 
            this.label16.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(453, 115);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(42, 13);
            this.label16.TabIndex = 15;
            this.label16.Text = "Version";
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
            // btReload
            // 
            this.btReload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btReload.Location = new System.Drawing.Point(374, 8);
            this.btReload.Name = "btReload";
            this.btReload.Size = new System.Drawing.Size(75, 23);
            this.btReload.TabIndex = 12;
            this.btReload.Text = "Reload";
            this.btReload.UseVisualStyleBackColor = true;
            this.btReload.Click += new System.EventHandler(this.btReload_Click);
            // 
            // btRCON
            // 
            this.btRCON.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btRCON.Image = global::OphiussaFramework.Properties.Resources.consoleicon;
            this.btRCON.Location = new System.Drawing.Point(536, 60);
            this.btRCON.Name = "btRCON";
            this.btRCON.Size = new System.Drawing.Size(75, 23);
            this.btRCON.TabIndex = 11;
            this.btRCON.Text = "RCON";
            this.btRCON.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btRCON.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btRCON.UseVisualStyleBackColor = true;
            this.btRCON.Click += new System.EventHandler(this.btRCON_Click);
            // 
            // btStart
            // 
            this.btStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btStart.Location = new System.Drawing.Point(455, 60);
            this.btStart.Name = "btStart";
            this.btStart.Size = new System.Drawing.Size(75, 23);
            this.btStart.TabIndex = 10;
            this.btStart.Text = "Start";
            this.btStart.UseVisualStyleBackColor = true;
            this.btStart.Click += new System.EventHandler(this.btStart_Click);
            // 
            // btUpdate
            // 
            this.btUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btUpdate.Image = global::OphiussaFramework.Properties.Resources.Refresh_icon;
            this.btUpdate.Location = new System.Drawing.Point(455, 34);
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
            this.btChooseFolder.Location = new System.Drawing.Point(419, 84);
            this.btChooseFolder.Name = "btChooseFolder";
            this.btChooseFolder.Size = new System.Drawing.Size(30, 23);
            this.btChooseFolder.TabIndex = 8;
            this.btChooseFolder.Text = "...";
            this.btChooseFolder.UseVisualStyleBackColor = true;
            this.btChooseFolder.Click += new System.EventHandler(this.btChooseFolder_Click);
            // 
            // txtLocation
            // 
            this.txtLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLocation.Location = new System.Drawing.Point(108, 86);
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.ReadOnly = true;
            this.txtLocation.Size = new System.Drawing.Size(303, 20);
            this.txtLocation.TabIndex = 7;
            this.txtLocation.Tag = "InstallLocation";
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
            this.btSave.Image = global::OphiussaFramework.Properties.Resources.SaveIcon;
            this.btSave.Location = new System.Drawing.Point(536, 8);
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
            this.btSync.Enabled = false;
            this.btSync.Image = global::OphiussaFramework.Properties.Resources.Copy_icon_icon;
            this.btSync.Location = new System.Drawing.Point(455, 8);
            this.btSync.Name = "btSync";
            this.btSync.Size = new System.Drawing.Size(75, 23);
            this.btSync.TabIndex = 4;
            this.btSync.Text = "Sync";
            this.btSync.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btSync.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btSync.UseVisualStyleBackColor = true;
            this.btSync.Click += new System.EventHandler(this.btSync_Click);
            // 
            // txtProfileName
            // 
            this.txtProfileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtProfileName.Location = new System.Drawing.Point(108, 36);
            this.txtProfileName.Name = "txtProfileName";
            this.txtProfileName.Size = new System.Drawing.Size(333, 20);
            this.txtProfileName.TabIndex = 3;
            this.txtProfileName.Tag = "Name";
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
            this.txtProfileID.Tag = "Key";
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
            this.timerGetProcess.Interval = 500;
            this.timerGetProcess.Tick += new System.EventHandler(this.timerGetProcess_Tick);
            // 
            // cboBranch
            // 
            this.cboBranch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboBranch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBranch.FormattingEnabled = true;
            this.cboBranch.Location = new System.Drawing.Point(108, 112);
            this.cboBranch.Name = "cboBranch";
            this.cboBranch.Size = new System.Drawing.Size(303, 21);
            this.cboBranch.TabIndex = 19;
            this.cboBranch.Click += new System.EventHandler(this.cboBranch_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 120);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 13);
            this.label5.TabIndex = 20;
            this.label5.Text = "Branch";
            // 
            // txtBeta
            // 
            this.txtBeta.Location = new System.Drawing.Point(108, 139);
            this.txtBeta.Name = "txtBeta";
            this.txtBeta.Size = new System.Drawing.Size(92, 20);
            this.txtBeta.TabIndex = 22;
            this.txtBeta.Tag = "Name";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 142);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 13);
            this.label6.TabIndex = 21;
            this.label6.Text = "Steam Branch";
            // 
            // txtBetaPassword
            // 
            this.txtBetaPassword.Location = new System.Drawing.Point(271, 139);
            this.txtBetaPassword.Name = "txtBetaPassword";
            this.txtBetaPassword.Size = new System.Drawing.Size(140, 20);
            this.txtBetaPassword.TabIndex = 24;
            this.txtBetaPassword.Tag = "Name";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(212, 142);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 13);
            this.label7.TabIndex = 23;
            this.label7.Text = "Password";
            // 
            // ProfileHeader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.txtBetaPassword);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtBeta);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cboBranch);
            this.Controls.Add(this.txtBuild);
            this.Controls.Add(this.label35);
            this.Controls.Add(this.txtVersion);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.txtServerType);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btReload);
            this.Controls.Add(this.btRCON);
            this.Controls.Add(this.btStart);
            this.Controls.Add(this.btUpdate);
            this.Controls.Add(this.btChooseFolder);
            this.Controls.Add(this.txtLocation);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btSave);
            this.Controls.Add(this.btSync);
            this.Controls.Add(this.txtProfileName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtProfileID);
            this.Controls.Add(this.label1);
            this.Name = "ProfileHeader";
            this.Size = new System.Drawing.Size(625, 166);
            this.Load += new System.EventHandler(this.ProfileHeader_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtBuild;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.TextBox txtVersion;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtServerType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btReload;
        private System.Windows.Forms.Button btRCON;
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
        private System.Windows.Forms.Timer timerGetProcess;
        private System.Windows.Forms.FolderBrowserDialog fBD;
        private System.Windows.Forms.ComboBox cboBranch;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtBeta;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtBetaPassword;
        private System.Windows.Forms.Label label7;
    }
}
