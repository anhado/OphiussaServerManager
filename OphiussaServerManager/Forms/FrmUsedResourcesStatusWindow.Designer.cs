namespace OphiussaServerManager.Forms
{
    partial class FrmUsedResourcesStatusWindow
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
            this.components = new System.ComponentModel.Container();
            this.lblTypeDesc = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblUsagePerc = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblUsage = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblTypeDesc
            // 
            this.lblTypeDesc.AutoSize = true;
            this.lblTypeDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTypeDesc.Location = new System.Drawing.Point(12, 9);
            this.lblTypeDesc.Name = "lblTypeDesc";
            this.lblTypeDesc.Size = new System.Drawing.Size(51, 22);
            this.lblTypeDesc.TabIndex = 0;
            this.lblTypeDesc.Text = "Type";
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(15, 31);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(60, 13);
            this.lblDescription.TabIndex = 1;
            this.lblDescription.Text = "Description";
            // 
            // lblUsagePerc
            // 
            this.lblUsagePerc.AutoSize = true;
            this.lblUsagePerc.Location = new System.Drawing.Point(15, 44);
            this.lblUsagePerc.Name = "lblUsagePerc";
            this.lblUsagePerc.Size = new System.Drawing.Size(49, 13);
            this.lblUsagePerc.TabIndex = 2;
            this.lblUsagePerc.Text = "Usage %";
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(18, 60);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(228, 23);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 3;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lblUsage
            // 
            this.lblUsage.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblUsage.Location = new System.Drawing.Point(16, 86);
            this.lblUsage.Name = "lblUsage";
            this.lblUsage.Size = new System.Drawing.Size(230, 13);
            this.lblUsage.TabIndex = 4;
            this.lblUsage.Text = "Usage";
            this.lblUsage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrmUsedResourcesStatusWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(256, 101);
            this.ControlBox = false;
            this.Controls.Add(this.lblUsage);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.lblUsagePerc);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.lblTypeDesc);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmUsedResourcesStatusWindow";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.FrmUsedResourcesStatusWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTypeDesc;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblUsagePerc;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblUsage;
    }
}