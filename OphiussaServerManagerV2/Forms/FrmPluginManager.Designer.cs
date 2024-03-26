namespace OphiussaServerManagerV2
{
    partial class FrmPluginManager
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
            ControlRenderer controlRenderer1 = new ControlRenderer();
            MSColorTable msColorTable1 = new MSColorTable();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPluginManager));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.fDiag = new System.Windows.Forms.OpenFileDialog();
            this.osmThemeContainer1 = new OSMThemeContainer();
            this.osmControlBox1 = new OSMControlBox();
            this.osmMenuStrip1 = new OSMMenuStrip();
            this.addNewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.osmThemeContainer1.SuspendLayout();
            this.osmMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 52);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(794, 370);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            // 
            // osmThemeContainer1
            // 
            this.osmThemeContainer1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.osmThemeContainer1.Controls.Add(this.osmControlBox1);
            this.osmThemeContainer1.Controls.Add(this.dataGridView1);
            this.osmThemeContainer1.Controls.Add(this.osmMenuStrip1);
            this.osmThemeContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.osmThemeContainer1.DrawBottomBar = true;
            this.osmThemeContainer1.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.osmThemeContainer1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.osmThemeContainer1.Location = new System.Drawing.Point(0, 0);
            this.osmThemeContainer1.Name = "osmThemeContainer1";
            this.osmThemeContainer1.Padding = new System.Windows.Forms.Padding(3, 28, 3, 28);
            this.osmThemeContainer1.Size = new System.Drawing.Size(800, 450);
            this.osmThemeContainer1.TabIndex = 2;
            this.osmThemeContainer1.Text = "Plugin Manager";
            this.osmThemeContainer1.TextBottom = null;
            // 
            // osmControlBox1
            // 
            this.osmControlBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.osmControlBox1.BackColor = System.Drawing.Color.Transparent;
            this.osmControlBox1.Location = new System.Drawing.Point(723, 0);
            this.osmControlBox1.MinimizeBox = false;
            this.osmControlBox1.Name = "osmControlBox1";
            this.osmControlBox1.Size = new System.Drawing.Size(77, 19);
            this.osmControlBox1.TabIndex = 4;
            this.osmControlBox1.Text = "osmControlBox1";
            // 
            // osmMenuStrip1
            // 
            this.osmMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addNewToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.saveToolStripMenuItem});
            this.osmMenuStrip1.Location = new System.Drawing.Point(3, 28);
            this.osmMenuStrip1.Name = "osmMenuStrip1";
            controlRenderer1.ColorTable = msColorTable1;
            controlRenderer1.RoundedEdges = true;
            this.osmMenuStrip1.Renderer = controlRenderer1;
            this.osmMenuStrip1.Size = new System.Drawing.Size(794, 24);
            this.osmMenuStrip1.TabIndex = 3;
            this.osmMenuStrip1.Text = "osmMenuStrip1";
            // 
            // addNewToolStripMenuItem
            // 
            this.addNewToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.addNewToolStripMenuItem.Image = global::OphiussaServerManagerV2.Properties.Resources.add_icon_icon__1_;
            this.addNewToolStripMenuItem.Name = "addNewToolStripMenuItem";
            this.addNewToolStripMenuItem.Size = new System.Drawing.Size(84, 20);
            this.addNewToolStripMenuItem.Text = "Add New";
            this.addNewToolStripMenuItem.Click += new System.EventHandler(this.btAdd_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.deleteToolStripMenuItem.Image = global::OphiussaServerManagerV2.Properties.Resources.Close_icon_icon;
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(68, 20);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.btDelete_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.saveToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.saveToolStripMenuItem.Image = global::OphiussaServerManagerV2.Properties.Resources.save__floppy__disk_icon_icon__2_;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Visible = false;
            // 
            // FrmPluginManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.osmThemeContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.osmMenuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmPluginManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Plugin Manager";
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmPluginManager_FormClosing);
            this.Load += new System.EventHandler(this.FrmPluginManager_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.osmThemeContainer1.ResumeLayout(false);
            this.osmThemeContainer1.PerformLayout();
            this.osmMenuStrip1.ResumeLayout(false);
            this.osmMenuStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.OpenFileDialog fDiag;
        private OSMThemeContainer osmThemeContainer1;
        private OSMMenuStrip osmMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem addNewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private OSMControlBox osmControlBox1;
    }
}