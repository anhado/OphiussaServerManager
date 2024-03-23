namespace OphiussaFramework.Components {
    partial class exTrackBar {
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
            this.lblUnits = new System.Windows.Forms.Label();
            this.txtUC = new System.Windows.Forms.TextBox();
            this.tbUC = new System.Windows.Forms.TrackBar();
            this.lblDesc = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.tbUC)).BeginInit();
            this.SuspendLayout();
            // 
            // lblUnits
            // 
            this.lblUnits.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUnits.AutoSize = true;
            this.lblUnits.Location = new System.Drawing.Point(691, 7);
            this.lblUnits.Name = "lblUnits";
            this.lblUnits.Size = new System.Drawing.Size(31, 13);
            this.lblUnits.TabIndex = 22;
            this.lblUnits.Text = "Units";
            // 
            // txtUC
            // 
            this.txtUC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUC.Location = new System.Drawing.Point(636, 3);
            this.txtUC.Name = "txtUC";
            this.txtUC.Size = new System.Drawing.Size(49, 20);
            this.txtUC.TabIndex = 21;
            this.txtUC.TextChanged += new System.EventHandler(this.txtUC_TextChanged);
            this.txtUC.Enter += new System.EventHandler(this.txtUC_Enter);
            this.txtUC.Leave += new System.EventHandler(this.txtUC_Leave);
            // 
            // tbUC
            // 
            this.tbUC.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbUC.Location = new System.Drawing.Point(181, 0);
            this.tbUC.Maximum = 100;
            this.tbUC.Minimum = 1;
            this.tbUC.Name = "tbUC";
            this.tbUC.Size = new System.Drawing.Size(449, 45);
            this.tbUC.TabIndex = 20;
            this.tbUC.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbUC.Value = 1;
            this.tbUC.Scroll += new System.EventHandler(this.tbUC_Scroll);
            // 
            // lblDesc
            // 
            this.lblDesc.Location = new System.Drawing.Point(0, 0);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Size = new System.Drawing.Size(175, 26);
            this.lblDesc.TabIndex = 23;
            this.lblDesc.Text = "Desc";
            // 
            // exTrackBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblDesc);
            this.Controls.Add(this.lblUnits);
            this.Controls.Add(this.txtUC);
            this.Controls.Add(this.tbUC);
            this.Name = "exTrackBar";
            this.Size = new System.Drawing.Size(738, 26);
            this.Resize += new System.EventHandler(this.UcTrackBar_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.tbUC)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblUnits;
        private System.Windows.Forms.TextBox txtUC;
        private System.Windows.Forms.TrackBar tbUC;
        private System.Windows.Forms.Label lblDesc;
    }
}
