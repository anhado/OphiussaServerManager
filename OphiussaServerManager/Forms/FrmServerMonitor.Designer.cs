namespace OphiussaServerManager.Forms
{
    partial class FrmServerMonitor
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmServerMonitor));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.bStop = new System.Windows.Forms.DataGridViewButtonColumn();
            this.bUpdateMods = new System.Windows.Forms.DataGridViewButtonColumn();
            this.bRCON = new System.Windows.Forms.DataGridViewButtonColumn();
            this.bSave = new System.Windows.Forms.DataGridViewButtonColumn();
            this.bOpen = new System.Windows.Forms.DataGridViewButtonColumn();
            this.selectDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.profileDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.serverNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mapDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.portsDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.modsDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.versionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.playersDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.monitorGridBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.monitorGridBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 27);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 350);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(800, 100);
            this.panel2.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.dataGridView1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 27);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(800, 323);
            this.panel3.TabIndex = 2;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.selectDataGridViewCheckBoxColumn,
            this.profileDataGridViewTextBoxColumn,
            this.serverNameDataGridViewTextBoxColumn,
            this.mapDataGridViewTextBoxColumn,
            this.portsDataGridViewTextBoxColumn,
            this.modsDataGridViewTextBoxColumn,
            this.versionDataGridViewTextBoxColumn,
            this.playersDataGridViewTextBoxColumn,
            this.statusDataGridViewTextBoxColumn,
            this.bStop,
            this.bUpdateMods,
            this.bRCON,
            this.bSave,
            this.bOpen});
            this.dataGridView1.DataSource = this.monitorGridBindingSource;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(800, 323);
            this.dataGridView1.TabIndex = 0;
            // 
            // bStop
            // 
            this.bStop.HeaderText = "Stop";
            this.bStop.Name = "bStop";
            this.bStop.Width = 35;
            // 
            // bUpdateMods
            // 
            this.bUpdateMods.HeaderText = "Update Mods";
            this.bUpdateMods.Name = "bUpdateMods";
            this.bUpdateMods.Width = 77;
            // 
            // bRCON
            // 
            this.bRCON.HeaderText = "RCON";
            this.bRCON.Name = "bRCON";
            this.bRCON.Width = 44;
            // 
            // bSave
            // 
            this.bSave.HeaderText = "Save World";
            this.bSave.Name = "bSave";
            this.bSave.Width = 69;
            // 
            // bOpen
            // 
            this.bOpen.HeaderText = "Open Install Folder";
            this.bOpen.Name = "bOpen";
            this.bOpen.Width = 91;
            // 
            // selectDataGridViewCheckBoxColumn
            // 
            this.selectDataGridViewCheckBoxColumn.DataPropertyName = "Select";
            this.selectDataGridViewCheckBoxColumn.HeaderText = "Select";
            this.selectDataGridViewCheckBoxColumn.Name = "selectDataGridViewCheckBoxColumn";
            this.selectDataGridViewCheckBoxColumn.Width = 43;
            // 
            // profileDataGridViewTextBoxColumn
            // 
            this.profileDataGridViewTextBoxColumn.DataPropertyName = "Profile";
            this.profileDataGridViewTextBoxColumn.HeaderText = "Profile";
            this.profileDataGridViewTextBoxColumn.Name = "profileDataGridViewTextBoxColumn";
            this.profileDataGridViewTextBoxColumn.ReadOnly = true;
            this.profileDataGridViewTextBoxColumn.Width = 61;
            // 
            // serverNameDataGridViewTextBoxColumn
            // 
            this.serverNameDataGridViewTextBoxColumn.DataPropertyName = "ServerName";
            this.serverNameDataGridViewTextBoxColumn.HeaderText = "ServerName";
            this.serverNameDataGridViewTextBoxColumn.Name = "serverNameDataGridViewTextBoxColumn";
            this.serverNameDataGridViewTextBoxColumn.ReadOnly = true;
            this.serverNameDataGridViewTextBoxColumn.Width = 91;
            // 
            // mapDataGridViewTextBoxColumn
            // 
            this.mapDataGridViewTextBoxColumn.DataPropertyName = "Map";
            this.mapDataGridViewTextBoxColumn.HeaderText = "Map";
            this.mapDataGridViewTextBoxColumn.Name = "mapDataGridViewTextBoxColumn";
            this.mapDataGridViewTextBoxColumn.ReadOnly = true;
            this.mapDataGridViewTextBoxColumn.Width = 53;
            // 
            // portsDataGridViewTextBoxColumn
            // 
            this.portsDataGridViewTextBoxColumn.DataPropertyName = "Ports";
            this.portsDataGridViewTextBoxColumn.HeaderText = "Ports";
            this.portsDataGridViewTextBoxColumn.Name = "portsDataGridViewTextBoxColumn";
            this.portsDataGridViewTextBoxColumn.ReadOnly = true;
            this.portsDataGridViewTextBoxColumn.Width = 56;
            // 
            // modsDataGridViewTextBoxColumn
            // 
            this.modsDataGridViewTextBoxColumn.DataPropertyName = "Mods";
            this.modsDataGridViewTextBoxColumn.HeaderText = "Mods";
            this.modsDataGridViewTextBoxColumn.Name = "modsDataGridViewTextBoxColumn";
            this.modsDataGridViewTextBoxColumn.ReadOnly = true;
            this.modsDataGridViewTextBoxColumn.Width = 58;
            // 
            // versionDataGridViewTextBoxColumn
            // 
            this.versionDataGridViewTextBoxColumn.DataPropertyName = "Version";
            this.versionDataGridViewTextBoxColumn.HeaderText = "Version";
            this.versionDataGridViewTextBoxColumn.Name = "versionDataGridViewTextBoxColumn";
            this.versionDataGridViewTextBoxColumn.ReadOnly = true;
            this.versionDataGridViewTextBoxColumn.Width = 67;
            // 
            // playersDataGridViewTextBoxColumn
            // 
            this.playersDataGridViewTextBoxColumn.DataPropertyName = "Players";
            this.playersDataGridViewTextBoxColumn.HeaderText = "Players";
            this.playersDataGridViewTextBoxColumn.Name = "playersDataGridViewTextBoxColumn";
            this.playersDataGridViewTextBoxColumn.ReadOnly = true;
            this.playersDataGridViewTextBoxColumn.Width = 66;
            // 
            // statusDataGridViewTextBoxColumn
            // 
            this.statusDataGridViewTextBoxColumn.DataPropertyName = "Status";
            this.statusDataGridViewTextBoxColumn.HeaderText = "Status";
            this.statusDataGridViewTextBoxColumn.Name = "statusDataGridViewTextBoxColumn";
            this.statusDataGridViewTextBoxColumn.ReadOnly = true;
            this.statusDataGridViewTextBoxColumn.Width = 62;
            // 
            // monitorGridBindingSource
            // 
            this.monitorGridBindingSource.DataSource = typeof(OphiussaServerManager.Common.Models.MonitorGrid);
            // 
            // FrmServerMonitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.Color.SteelBlue;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmServerMonitor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ophiusa Server Manager - Server Monitor";
            this.Load += new System.EventHandler(this.ServerMonitor_Load);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.monitorGridBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingSource monitorGridBindingSource;
        private System.Windows.Forms.DataGridViewCheckBoxColumn selectDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn profileDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn serverNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn mapDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn portsDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn modsDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn versionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn playersDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn statusDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewButtonColumn bStop;
        private System.Windows.Forms.DataGridViewButtonColumn bUpdateMods;
        private System.Windows.Forms.DataGridViewButtonColumn bRCON;
        private System.Windows.Forms.DataGridViewButtonColumn bSave;
        private System.Windows.Forms.DataGridViewButtonColumn bOpen;
    }
}