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
            this.profileHeader1 = new OphiussaFramework.Components.ProfileHeader();
            this.SuspendLayout();
            // 
            // profileHeader1
            // 
            this.profileHeader1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.profileHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.profileHeader1.Location = new System.Drawing.Point(0, 0);
            this.profileHeader1.Name = "profileHeader1";
            this.profileHeader1.Size = new System.Drawing.Size(800, 148);
            this.profileHeader1.TabIndex = 0;
            // 
            // FrmConfigurationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.profileHeader1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmConfigurationForm";
            this.Text = "Base Plugin";
            this.ResumeLayout(false);

        }

        #endregion

        private OphiussaFramework.Components.ProfileHeader profileHeader1;
    }
}