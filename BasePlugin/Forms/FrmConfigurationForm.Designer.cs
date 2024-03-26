namespace BasePlugin.Forms
{
    partial class FrmConfigurationForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConfigurationForm));
            this.button1 = new System.Windows.Forms.Button();
            this.txtProfileID = new OSMTextBox_Small();
            this.profileHeader1 = new OphiussaFramework.Components.ProfileHeader();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(438, 248);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Open Command Builder";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtProfileID
            // 
            this.txtProfileID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtProfileID.BackColor = System.Drawing.Color.Transparent;
            this.txtProfileID.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtProfileID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(142)))), ((int)(((byte)(142)))));
            this.txtProfileID.Location = new System.Drawing.Point(98, 324);
            this.txtProfileID.MaxLength = 32767;
            this.txtProfileID.Multiline = false;
            this.txtProfileID.Name = "txtProfileID";
            this.txtProfileID.ReadOnly = true;
            this.txtProfileID.Size = new System.Drawing.Size(749, 28);
            this.txtProfileID.TabIndex = 20;
            this.txtProfileID.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtProfileID.UseSystemPasswordChar = false;
            this.txtProfileID.Value = "";
            // 
            // profileHeader1
            // 
            this.profileHeader1.BackColor = System.Drawing.Color.White;
            this.profileHeader1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.profileHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.profileHeader1.Location = new System.Drawing.Point(0, 0);
            this.profileHeader1.Name = "profileHeader1";
            this.profileHeader1.RconEnabled = true;
            this.profileHeader1.Size = new System.Drawing.Size(869, 208);
            this.profileHeader1.TabIndex = 0;
            this.profileHeader1.ClickReload += new System.EventHandler(this.profileHeader1_ClickReload);
            this.profileHeader1.ClickSync += new System.EventHandler(this.profileHeader1_ClickSync);
            this.profileHeader1.ClickSave += new System.EventHandler(this.profileHeader1_ClickSave);
            this.profileHeader1.ClickUpgrade += new System.EventHandler(this.profileHeader1_ClickUpgrade);
            this.profileHeader1.ClickStartStop += new System.EventHandler(this.profileHeader1_ClickStartStop);
            this.profileHeader1.ClickRCON += new System.EventHandler(this.profileHeader1_ClickRCON);
            // 
            // FrmConfigurationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(869, 450);
            this.Controls.Add(this.txtProfileID);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.profileHeader1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmConfigurationForm";
            this.Text = "Base Plugin";
            this.Load += new System.EventHandler(this.FrmConfigurationForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private OphiussaFramework.Components.ProfileHeader profileHeader1;
        private System.Windows.Forms.Button button1;
        private OSMTextBox_Small txtProfileID;
    }
}