namespace VRisingPlugin.Forms
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
            this.automaticManagement1 = new OphiussaServerManager.Components.AutomaticManagement();
            this.profileHeader1 = new OphiussaFramework.Components.ProfileHeader();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(713, 133);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Open Command Builder";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // automaticManagement1
            // 
            this.automaticManagement1.Dock = System.Windows.Forms.DockStyle.Top;
            this.automaticManagement1.ForeColor = System.Drawing.Color.SteelBlue;
            this.automaticManagement1.Location = new System.Drawing.Point(0, 168);
            this.automaticManagement1.Name = "automaticManagement1";
            this.automaticManagement1.Size = new System.Drawing.Size(800, 347);
            this.automaticManagement1.TabIndex = 2;
            // 
            // profileHeader1
            // 
            this.profileHeader1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.profileHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.profileHeader1.Location = new System.Drawing.Point(0, 0);
            this.profileHeader1.Name = "profileHeader1";
            this.profileHeader1.RconEnabled = true;
            this.profileHeader1.Size = new System.Drawing.Size(800, 168);
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
            this.ClientSize = new System.Drawing.Size(800, 540);
            this.Controls.Add(this.automaticManagement1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.profileHeader1);
            this.ForeColor = System.Drawing.Color.SteelBlue;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmConfigurationForm";
            this.Text = "VRising";
            this.Load += new System.EventHandler(this.FrmConfigurationForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private OphiussaFramework.Components.ProfileHeader profileHeader1;
        private System.Windows.Forms.Button button1;
        private OphiussaServerManager.Components.AutomaticManagement automaticManagement1;
    }
}