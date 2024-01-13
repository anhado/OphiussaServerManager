using System.ComponentModel;

namespace OphiussaServerManager.Forms {
    partial class FrmEnshrouded {
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmEnshrouded));
            this.profileHeader1 = new OphiussaServerManager.Components.ProfileHeader();
            this.SuspendLayout();
            // 
            // profileHeader1
            // 
            this.profileHeader1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.profileHeader1.Dock        = System.Windows.Forms.DockStyle.Top;
            this.profileHeader1.Location    = new System.Drawing.Point(0, 0);
            this.profileHeader1.Name        = "profileHeader1";
            this.profileHeader1.Size        = new System.Drawing.Size(800, 148);
            this.profileHeader1.TabIndex    = 0;
            // 
            // FrmEnshrouded
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize          = new System.Drawing.Size(800, 611);
            this.Controls.Add(this.profileHeader1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmEnshrouded";
            this.Text = "Enshrouded";
            this.ResumeLayout(false);
        }

        private OphiussaServerManager.Components.ProfileHeader profileHeader1;

        #endregion
    }
}