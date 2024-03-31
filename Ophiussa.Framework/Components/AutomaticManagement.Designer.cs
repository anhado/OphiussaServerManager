namespace OphiussaServerManager.Components {
    partial class AutomaticManagement {
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
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btDelete = new System.Windows.Forms.Button();
            this.btAdd = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.chkRestartIfShutdown = new System.Windows.Forms.CheckBox();
            this.chkAutoUpdate = new System.Windows.Forms.CheckBox();
            this.chkIncludeAutoBackup = new System.Windows.Forms.CheckBox();
            this.rbOnLogin = new System.Windows.Forms.RadioButton();
            this.rbOnBoot = new System.Windows.Forms.RadioButton();
            this.chkAutoStart = new System.Windows.Forms.CheckBox();
            this.groupBox11.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox11
            // 
            this.groupBox11.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.groupBox11.Controls.Add(this.groupBox1);
            this.groupBox11.Controls.Add(this.chkRestartIfShutdown);
            this.groupBox11.Controls.Add(this.chkAutoUpdate);
            this.groupBox11.Controls.Add(this.chkIncludeAutoBackup);
            this.groupBox11.Controls.Add(this.rbOnLogin);
            this.groupBox11.Controls.Add(this.rbOnBoot);
            this.groupBox11.Controls.Add(this.chkAutoStart);
            this.groupBox11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.groupBox11.Location = new System.Drawing.Point(3, 3);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(731, 339);
            this.groupBox11.TabIndex = 2;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "Server Manager Settings";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btDelete);
            this.groupBox1.Controls.Add(this.btAdd);
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.ForeColor = System.Drawing.Color.SteelBlue;
            this.groupBox1.Location = new System.Drawing.Point(7, 91);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(718, 242);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Auto Restart Settings";
            // 
            // btDelete
            // 
            this.btDelete.Image = global::OphiussaFramework.Properties.Resources.Delete;
            this.btDelete.Location = new System.Drawing.Point(42, 19);
            this.btDelete.Name = "btDelete";
            this.btDelete.Size = new System.Drawing.Size(30, 23);
            this.btDelete.TabIndex = 7;
            this.btDelete.UseVisualStyleBackColor = true;
            this.btDelete.Click += new System.EventHandler(this.btDelete_Click);
            // 
            // btAdd
            // 
            this.btAdd.Image = global::OphiussaFramework.Properties.Resources.AddNew;
            this.btAdd.Location = new System.Drawing.Point(6, 19);
            this.btAdd.Name = "btAdd";
            this.btAdd.Size = new System.Drawing.Size(30, 23);
            this.btAdd.TabIndex = 6;
            this.btAdd.UseVisualStyleBackColor = true;
            this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(6, 47);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(706, 189);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
            this.dataGridView1.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridView1_DataError);
            // 
            // chkRestartIfShutdown
            // 
            this.chkRestartIfShutdown.AutoSize = true;
            this.chkRestartIfShutdown.Location = new System.Drawing.Point(249, 67);
            this.chkRestartIfShutdown.Name = "chkRestartIfShutdown";
            this.chkRestartIfShutdown.Size = new System.Drawing.Size(149, 17);
            this.chkRestartIfShutdown.TabIndex = 7;
            this.chkRestartIfShutdown.Tag = "RestartIfShutdown";
            this.chkRestartIfShutdown.Text = "Restart server if shutdown";
            this.chkRestartIfShutdown.UseVisualStyleBackColor = true;
            // 
            // chkAutoUpdate
            // 
            this.chkAutoUpdate.AutoSize = true;
            this.chkAutoUpdate.Location = new System.Drawing.Point(7, 68);
            this.chkAutoUpdate.Name = "chkAutoUpdate";
            this.chkAutoUpdate.Size = new System.Drawing.Size(213, 17);
            this.chkAutoUpdate.TabIndex = 6;
            this.chkAutoUpdate.Tag = "IncludeInAutoUpdate";
            this.chkAutoUpdate.Text = "Include server in the Auto-Update cycle";
            this.chkAutoUpdate.UseVisualStyleBackColor = true;
            // 
            // chkIncludeAutoBackup
            // 
            this.chkIncludeAutoBackup.AutoSize = true;
            this.chkIncludeAutoBackup.Location = new System.Drawing.Point(7, 43);
            this.chkIncludeAutoBackup.Name = "chkIncludeAutoBackup";
            this.chkIncludeAutoBackup.Size = new System.Drawing.Size(215, 17);
            this.chkIncludeAutoBackup.TabIndex = 5;
            this.chkIncludeAutoBackup.Tag = "IncludeInAutoBackup";
            this.chkIncludeAutoBackup.Text = "Include server in the Auto-Backup cycle";
            this.chkIncludeAutoBackup.UseVisualStyleBackColor = true;
            // 
            // rbOnLogin
            // 
            this.rbOnLogin.AutoSize = true;
            this.rbOnLogin.Enabled = false;
            this.rbOnLogin.Location = new System.Drawing.Point(220, 20);
            this.rbOnLogin.Name = "rbOnLogin";
            this.rbOnLogin.Size = new System.Drawing.Size(66, 17);
            this.rbOnLogin.TabIndex = 2;
            this.rbOnLogin.Text = "on Login";
            this.rbOnLogin.UseVisualStyleBackColor = true;
            // 
            // rbOnBoot
            // 
            this.rbOnBoot.AutoSize = true;
            this.rbOnBoot.Checked = true;
            this.rbOnBoot.Enabled = false;
            this.rbOnBoot.Location = new System.Drawing.Point(143, 20);
            this.rbOnBoot.Name = "rbOnBoot";
            this.rbOnBoot.Size = new System.Drawing.Size(62, 17);
            this.rbOnBoot.TabIndex = 1;
            this.rbOnBoot.TabStop = true;
            this.rbOnBoot.Text = "on Boot";
            this.rbOnBoot.UseVisualStyleBackColor = true;
            // 
            // chkAutoStart
            // 
            this.chkAutoStart.AutoSize = true;
            this.chkAutoStart.Location = new System.Drawing.Point(7, 20);
            this.chkAutoStart.Name = "chkAutoStart";
            this.chkAutoStart.Size = new System.Drawing.Size(105, 17);
            this.chkAutoStart.TabIndex = 0;
            this.chkAutoStart.Tag = "AutoStartServer";
            this.chkAutoStart.Text = "Auto-Start server";
            this.chkAutoStart.UseVisualStyleBackColor = true;
            this.chkAutoStart.CheckedChanged += new System.EventHandler(this.chkAutoStart_CheckedChanged);
            // 
            // AutomaticManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox11);
            this.ForeColor = System.Drawing.Color.SteelBlue;
            this.Name = "AutomaticManagement";
            this.Size = new System.Drawing.Size(737, 347);
            this.Load += new System.EventHandler(this.AutomaticManagement_Load);
            this.groupBox11.ResumeLayout(false);
            this.groupBox11.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox11;
        private System.Windows.Forms.CheckBox chkRestartIfShutdown;
        private System.Windows.Forms.CheckBox chkAutoUpdate;
        private System.Windows.Forms.CheckBox chkIncludeAutoBackup;
        private System.Windows.Forms.RadioButton rbOnLogin;
        private System.Windows.Forms.RadioButton rbOnBoot;
        private System.Windows.Forms.CheckBox chkAutoStart;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btDelete;
        private System.Windows.Forms.Button btAdd;
    }
}
