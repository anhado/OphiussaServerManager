namespace OphiussaServerManagerV2 {
    partial class FrmServerMonitor {
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmServerMonitor));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btRestart = new System.Windows.Forms.Button();
            this.btStartStop = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.SteelBlue;
            this.panel1.Controls.Add(this.btRestart);
            this.panel1.Controls.Add(this.btStartStop);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 29);
            this.panel1.TabIndex = 1;
            // 
            // btRestart
            // 
            this.btRestart.Image = global::OphiussaServerManagerV2.Properties.Resources.refresh_icon_icon__4_;
            this.btRestart.Location = new System.Drawing.Point(39, 3);
            this.btRestart.Name = "btRestart";
            this.btRestart.Size = new System.Drawing.Size(30, 23);
            this.btRestart.TabIndex = 3;
            this.btRestart.UseVisualStyleBackColor = true;
            // 
            // btStartStop
            // 
            this.btStartStop.Image = global::OphiussaServerManagerV2.Properties.Resources.Close_icon_icon;
            this.btStartStop.Location = new System.Drawing.Point(3, 3);
            this.btStartStop.Name = "btStartStop";
            this.btStartStop.Size = new System.Drawing.Size(30, 23);
            this.btStartStop.TabIndex = 1;
            this.btStartStop.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 29);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(800, 421);
            this.dataGridView1.TabIndex = 2;
            // 
            // FrmServerMonitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmServerMonitor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Server Monitor";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btRestart;
        private System.Windows.Forms.Button btStartStop;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}