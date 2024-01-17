using System.ComponentModel;

namespace OphiussaServerManager.Components {
    partial class ArkEngrams {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            this.groupBox35              = new System.Windows.Forms.GroupBox();
            this.checkBox1               = new System.Windows.Forms.CheckBox();
            this.label1                  = new System.Windows.Forms.Label();
            this.dataGridView1           = new System.Windows.Forms.DataGridView();
            this.btDCFilter              = new System.Windows.Forms.Button();
            this.txtDCFilter             = new System.Windows.Forms.TextBox();
            this.cboFilter               = new System.Windows.Forms.ComboBox();
            this.label238                = new System.Windows.Forms.Label();
            this.CheckBox2               = new System.Windows.Forms.CheckBox();
            this.chkAutoUnlockAllEngrams = new System.Windows.Forms.CheckBox();
            this.groupBox35.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox35
            // 
            this.groupBox35.Anchor    = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox35.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.groupBox35.Controls.Add(this.checkBox1);
            this.groupBox35.Controls.Add(this.label1);
            this.groupBox35.Controls.Add(this.dataGridView1);
            this.groupBox35.Controls.Add(this.btDCFilter);
            this.groupBox35.Controls.Add(this.txtDCFilter);
            this.groupBox35.Controls.Add(this.cboFilter);
            this.groupBox35.Controls.Add(this.label238);
            this.groupBox35.Controls.Add(this.CheckBox2);
            this.groupBox35.Enabled   = false;
            this.groupBox35.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.groupBox35.Location  = new System.Drawing.Point(3, 26);
            this.groupBox35.Name      = "groupBox35";
            this.groupBox35.Size      = new System.Drawing.Size(702, 515);
            this.groupBox35.TabIndex  = 157;
            this.groupBox35.TabStop   = false;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize                = true;
            this.checkBox1.BackColor               = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.checkBox1.ForeColor               = System.Drawing.Color.FromArgb(((int)(((byte)(70)))),  ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.checkBox1.Location                = new System.Drawing.Point(9, 23);
            this.checkBox1.Name                    = "checkBox1";
            this.checkBox1.Size                    = new System.Drawing.Size(164, 17);
            this.checkBox1.TabIndex                = 157;
            this.checkBox1.Tag                     = "OnlyAllowSpecifiedEngrams";
            this.checkBox1.Text                    = "Only Allow Selected Engrams";
            this.checkBox1.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.Anchor    = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font      = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location  = new System.Drawing.Point(23, 99);
            this.label1.Name      = "label1";
            this.label1.Size      = new System.Drawing.Size(655, 380);
            this.label1.TabIndex  = 26;
            this.label1.Text      = "Coming Soon";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows          = false;
            this.dataGridView1.AllowUserToDeleteRows       = false;
            this.dataGridView1.Anchor                      = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location                    = new System.Drawing.Point(10, 69);
            this.dataGridView1.Name                        = "dataGridView1";
            this.dataGridView1.RowHeadersVisible           = false;
            this.dataGridView1.SelectionMode               = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size                        = new System.Drawing.Size(686, 436);
            this.dataGridView1.TabIndex                    = 20;
            // 
            // btDCFilter
            // 
            this.btDCFilter.Image                   = global::OphiussaServerManager.Properties.Resources.FilterIcon;
            this.btDCFilter.Location                = new System.Drawing.Point(311, 38);
            this.btDCFilter.Name                    = "btDCFilter";
            this.btDCFilter.Size                    = new System.Drawing.Size(26, 25);
            this.btDCFilter.TabIndex                = 19;
            this.btDCFilter.UseVisualStyleBackColor = true;
            // 
            // txtDCFilter
            // 
            this.txtDCFilter.Location = new System.Drawing.Point(174, 40);
            this.txtDCFilter.Name     = "txtDCFilter";
            this.txtDCFilter.Size     = new System.Drawing.Size(135, 20);
            this.txtDCFilter.TabIndex = 18;
            // 
            // cboFilter
            // 
            this.cboFilter.DropDownStyle     = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFilter.FormattingEnabled = true;
            this.cboFilter.Location          = new System.Drawing.Point(47, 40);
            this.cboFilter.Name              = "cboFilter";
            this.cboFilter.Size              = new System.Drawing.Size(121, 21);
            this.cboFilter.TabIndex          = 17;
            // 
            // label238
            // 
            this.label238.Location = new System.Drawing.Point(9, 43);
            this.label238.Name     = "label238";
            this.label238.Size     = new System.Drawing.Size(100, 23);
            this.label238.TabIndex = 16;
            this.label238.Text     = "Filter";
            // 
            // CheckBox2
            // 
            this.CheckBox2.AutoSize                =  true;
            this.CheckBox2.Location                =  new System.Drawing.Point(9, 0);
            this.CheckBox2.Name                    =  "CheckBox2";
            this.CheckBox2.Size                    =  new System.Drawing.Size(151, 17);
            this.CheckBox2.TabIndex                =  11;
            this.CheckBox2.Text                    =  "Enable Engrams Overrides";
            this.CheckBox2.UseVisualStyleBackColor =  true;
            this.CheckBox2.CheckedChanged          += new System.EventHandler(this.CheckBox2_CheckedChanged);
            // 
            // chkAutoUnlockAllEngrams
            // 
            this.chkAutoUnlockAllEngrams.AutoSize                = true;
            this.chkAutoUnlockAllEngrams.BackColor               = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.chkAutoUnlockAllEngrams.ForeColor               = System.Drawing.Color.FromArgb(((int)(((byte)(70)))),  ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.chkAutoUnlockAllEngrams.Location                = new System.Drawing.Point(3, 3);
            this.chkAutoUnlockAllEngrams.Name                    = "chkAutoUnlockAllEngrams";
            this.chkAutoUnlockAllEngrams.Size                    = new System.Drawing.Size(143, 17);
            this.chkAutoUnlockAllEngrams.TabIndex                = 156;
            this.chkAutoUnlockAllEngrams.Tag                     = "AutoUnlockAllEngrams";
            this.chkAutoUnlockAllEngrams.Text                    = "Auto Unlock All Engrams";
            this.chkAutoUnlockAllEngrams.UseVisualStyleBackColor = false;
            // 
            // ArkEngrams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox35);
            this.Controls.Add(this.chkAutoUnlockAllEngrams);
            this.Name =  "ArkEngrams";
            this.Size =  new System.Drawing.Size(708, 546);
            this.Load += new System.EventHandler(this.ArkEngrams_Load);
            this.groupBox35.ResumeLayout(false);
            this.groupBox35.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.CheckBox checkBox1;

        private System.Windows.Forms.GroupBox     groupBox35;
        private System.Windows.Forms.Label        label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button       btDCFilter;
        private System.Windows.Forms.TextBox      txtDCFilter;
        private System.Windows.Forms.ComboBox     cboFilter;
        private System.Windows.Forms.Label        label238;
        private System.Windows.Forms.CheckBox     CheckBox2;
        private System.Windows.Forms.CheckBox     chkAutoUnlockAllEngrams;

        #endregion
    }
}