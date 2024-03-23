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
            this.chkUsedInstall = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.txtDirName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDir = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btCancel = new System.Windows.Forms.Button();
            this.btAdd = new System.Windows.Forms.Button();
            this.cboServerType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.fdDiag = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // chkUsedInstall
            // 
            this.chkUsedInstall.AutoSize = true;
            this.chkUsedInstall.Location = new System.Drawing.Point(96, 59);
            this.chkUsedInstall.Name = "chkUsedInstall";
            this.chkUsedInstall.Size = new System.Drawing.Size(169, 17);
            this.chkUsedInstall.TabIndex = 19;
            this.chkUsedInstall.Text = "Use Existing Installation Folder";
            this.chkUsedInstall.UseVisualStyleBackColor = true;
            this.chkUsedInstall.CheckedChanged += new System.EventHandler(this.chkUsedInstall_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(590, 31);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(33, 23);
            this.button1.TabIndex = 18;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtDirName
            // 
            this.txtDirName.Location = new System.Drawing.Point(96, 82);
            this.txtDirName.Name = "txtDirName";
            this.txtDirName.Size = new System.Drawing.Size(224, 20);
            this.txtDirName.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Directory Name";
            // 
            // txtDir
            // 
            this.txtDir.Location = new System.Drawing.Point(96, 33);
            this.txtDir.Name = "txtDir";
            this.txtDir.ReadOnly = true;
            this.txtDir.Size = new System.Drawing.Size(488, 20);
            this.txtDir.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Install Directory";
            // 
            // btCancel
            // 
            this.btCancel.Location = new System.Drawing.Point(12, 116);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 13;
            this.btCancel.Text = "Cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            // 
            // btAdd
            // 
            this.btAdd.Location = new System.Drawing.Point(548, 116);
            this.btAdd.Name = "btAdd";
            this.btAdd.Size = new System.Drawing.Size(75, 23);
            this.btAdd.TabIndex = 12;
            this.btAdd.Text = "Add";
            this.btAdd.UseVisualStyleBackColor = true;
            this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
            // 
            // cboServerType
            // 
            this.cboServerType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboServerType.FormattingEnabled = true;
            this.cboServerType.Location = new System.Drawing.Point(96, 6);
            this.cboServerType.Name = "cboServerType";
            this.cboServerType.Size = new System.Drawing.Size(224, 21);
            this.cboServerType.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Server Type";
            // 
            // FrmServerTypeSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(631, 147);
            this.Controls.Add(this.chkUsedInstall);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtDirName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtDir);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btAdd);
            this.Controls.Add(this.cboServerType);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmServerTypeSelection";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "New server";
            this.Load += new System.EventHandler(this.FrmServerTypeSelection_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkUsedInstall;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtDirName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDir;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Button btAdd;
        private System.Windows.Forms.ComboBox cboServerType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FolderBrowserDialog fdDiag;
    }
}