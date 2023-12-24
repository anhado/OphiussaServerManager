namespace OphiussaServerManager.Forms
{
    partial class FrmPortForward
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPortForward));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.profileDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.serverNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clRefresh = new System.Windows.Forms.DataGridViewImageColumn();
            this.clFWServer = new System.Windows.Forms.DataGridViewImageColumn();
            this.clFWPeerPort = new System.Windows.Forms.DataGridViewImageColumn();
            this.clFWQueryPort = new System.Windows.Forms.DataGridViewImageColumn();
            this.clFWRconPort = new System.Windows.Forms.DataGridViewImageColumn();
            this.clAddFW = new System.Windows.Forms.DataGridViewImageColumn();
            this.clDeleteFW = new System.Windows.Forms.DataGridViewImageColumn();
            this.clServerPort = new System.Windows.Forms.DataGridViewImageColumn();
            this.clPeerPort = new System.Windows.Forms.DataGridViewImageColumn();
            this.clQueryPort = new System.Windows.Forms.DataGridViewImageColumn();
            this.clRconPort = new System.Windows.Forms.DataGridViewImageColumn();
            this.clAddRouter = new System.Windows.Forms.DataGridViewImageColumn();
            this.clDeleteRouter = new System.Windows.Forms.DataGridViewImageColumn();
            this.portForwardGridBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewImageColumn2 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewImageColumn3 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewImageColumn4 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewImageColumn5 = new System.Windows.Forms.DataGridViewImageColumn();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.portForwardGridBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(792, 42);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Brown;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(777, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "ATTENTION: THIS IS HIGHLY EXPERMENTAL, WAS ONLY TESTED IN HOME ENVIRONMENT";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dataGridView1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 42);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(792, 408);
            this.panel2.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.profileDataGridViewTextBoxColumn,
            this.serverNameDataGridViewTextBoxColumn,
            this.clRefresh,
            this.clFWServer,
            this.clFWPeerPort,
            this.clFWQueryPort,
            this.clFWRconPort,
            this.clAddFW,
            this.clDeleteFW,
            this.clServerPort,
            this.clPeerPort,
            this.clQueryPort,
            this.clRconPort,
            this.clAddRouter,
            this.clDeleteRouter});
            this.dataGridView1.DataSource = this.portForwardGridBindingSource;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(792, 408);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataGridView1_RowsAdded);
            // 
            // profileDataGridViewTextBoxColumn
            // 
            this.profileDataGridViewTextBoxColumn.DataPropertyName = "Profile";
            this.profileDataGridViewTextBoxColumn.HeaderText = "Profile";
            this.profileDataGridViewTextBoxColumn.Name = "profileDataGridViewTextBoxColumn";
            this.profileDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // serverNameDataGridViewTextBoxColumn
            // 
            this.serverNameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.serverNameDataGridViewTextBoxColumn.DataPropertyName = "ServerName";
            this.serverNameDataGridViewTextBoxColumn.HeaderText = "ServerName";
            this.serverNameDataGridViewTextBoxColumn.MinimumWidth = 100;
            this.serverNameDataGridViewTextBoxColumn.Name = "serverNameDataGridViewTextBoxColumn";
            this.serverNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // clRefresh
            // 
            this.clRefresh.HeaderText = "Refresh";
            this.clRefresh.Image = global::OphiussaServerManager.Properties.Resources.RefeshIcon;
            this.clRefresh.Name = "clRefresh";
            this.clRefresh.Width = 64;
            // 
            // clFWServer
            // 
            this.clFWServer.DataPropertyName = "FirewallServerPort";
            this.clFWServer.HeaderText = "Server Port (Firewall)";
            this.clFWServer.Name = "clFWServer";
            this.clFWServer.ReadOnly = true;
            // 
            // clFWPeerPort
            // 
            this.clFWPeerPort.DataPropertyName = "FirewallPeerPort";
            this.clFWPeerPort.HeaderText = "Peer Port (Firewall)";
            this.clFWPeerPort.Name = "clFWPeerPort";
            this.clFWPeerPort.ReadOnly = true;
            // 
            // clFWQueryPort
            // 
            this.clFWQueryPort.DataPropertyName = "FirewallQueryPort";
            this.clFWQueryPort.HeaderText = "Query Port (Firewall )";
            this.clFWQueryPort.Name = "clFWQueryPort";
            this.clFWQueryPort.ReadOnly = true;
            // 
            // clFWRconPort
            // 
            this.clFWRconPort.DataPropertyName = "FirewallRconPort";
            this.clFWRconPort.HeaderText = "Rcon Port (Firewall)";
            this.clFWRconPort.Name = "clFWRconPort";
            this.clFWRconPort.ReadOnly = true;
            // 
            // clAddFW
            // 
            this.clAddFW.HeaderText = "Add/Update Firewall Rule";
            this.clAddFW.Image = global::OphiussaServerManager.Properties.Resources.AddIcon16x16;
            this.clAddFW.Name = "clAddFW";
            this.clAddFW.ReadOnly = true;
            // 
            // clDeleteFW
            // 
            this.clDeleteFW.HeaderText = "Delete Firewall Rules";
            this.clDeleteFW.Image = global::OphiussaServerManager.Properties.Resources.CloseIcon;
            this.clDeleteFW.Name = "clDeleteFW";
            this.clDeleteFW.Width = 64;
            // 
            // clServerPort
            // 
            this.clServerPort.DataPropertyName = "RouterServerPort";
            this.clServerPort.HeaderText = "Server Port (Router)";
            this.clServerPort.Name = "clServerPort";
            this.clServerPort.ReadOnly = true;
            // 
            // clPeerPort
            // 
            this.clPeerPort.DataPropertyName = "RouterPeerPort";
            this.clPeerPort.HeaderText = "Peer Port (Router) ";
            this.clPeerPort.Name = "clPeerPort";
            this.clPeerPort.ReadOnly = true;
            // 
            // clQueryPort
            // 
            this.clQueryPort.DataPropertyName = "RouterQueryPort";
            this.clQueryPort.HeaderText = "Query Port (Router)";
            this.clQueryPort.Name = "clQueryPort";
            this.clQueryPort.ReadOnly = true;
            // 
            // clRconPort
            // 
            this.clRconPort.DataPropertyName = "RouterRconPort";
            this.clRconPort.HeaderText = "Rcon Port (Router)";
            this.clRconPort.Name = "clRconPort";
            this.clRconPort.ReadOnly = true;
            // 
            // clAddRouter
            // 
            this.clAddRouter.HeaderText = "Add/Update Router Rule";
            this.clAddRouter.Image = global::OphiussaServerManager.Properties.Resources.AddIcon16x16;
            this.clAddRouter.Name = "clAddRouter";
            this.clAddRouter.ReadOnly = true;
            // 
            // clDeleteRouter
            // 
            this.clDeleteRouter.HeaderText = "Delete Router Rule";
            this.clDeleteRouter.Image = global::OphiussaServerManager.Properties.Resources.CloseIcon;
            this.clDeleteRouter.Name = "clDeleteRouter";
            this.clDeleteRouter.ReadOnly = true;
            // 
            // portForwardGridBindingSource
            // 
            this.portForwardGridBindingSource.DataSource = typeof(OphiussaServerManager.Common.Models.PortForwardGrid);
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.HeaderText = "Refresh";
            this.dataGridViewImageColumn1.Image = global::OphiussaServerManager.Properties.Resources.RefeshIcon;
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.Width = 64;
            // 
            // dataGridViewImageColumn2
            // 
            this.dataGridViewImageColumn2.HeaderText = "Add/Update Firewall Rule";
            this.dataGridViewImageColumn2.Image = global::OphiussaServerManager.Properties.Resources.AddIcon16x16;
            this.dataGridViewImageColumn2.Name = "dataGridViewImageColumn2";
            this.dataGridViewImageColumn2.ReadOnly = true;
            // 
            // dataGridViewImageColumn3
            // 
            this.dataGridViewImageColumn3.HeaderText = "Delete Firewall Rules";
            this.dataGridViewImageColumn3.Image = global::OphiussaServerManager.Properties.Resources.CloseIcon;
            this.dataGridViewImageColumn3.Name = "dataGridViewImageColumn3";
            this.dataGridViewImageColumn3.Width = 64;
            // 
            // dataGridViewImageColumn4
            // 
            this.dataGridViewImageColumn4.HeaderText = "Add/Update Router Rule";
            this.dataGridViewImageColumn4.Image = global::OphiussaServerManager.Properties.Resources.AddIcon16x16;
            this.dataGridViewImageColumn4.Name = "dataGridViewImageColumn4";
            this.dataGridViewImageColumn4.ReadOnly = true;
            // 
            // dataGridViewImageColumn5
            // 
            this.dataGridViewImageColumn5.HeaderText = "Delete Router Rule";
            this.dataGridViewImageColumn5.Image = global::OphiussaServerManager.Properties.Resources.CloseIcon;
            this.dataGridViewImageColumn5.Name = "dataGridViewImageColumn5";
            this.dataGridViewImageColumn5.ReadOnly = true;
            // 
            // FrmPortForward
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 450);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmPortForward";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Port Foward & Firewall Settings";
            this.Load += new System.EventHandler(this.FrmPortForward_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.portForwardGridBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingSource portForwardGridBindingSource;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn profileDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn serverNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewImageColumn clRefresh;
        private System.Windows.Forms.DataGridViewImageColumn clFWServer;
        private System.Windows.Forms.DataGridViewImageColumn clFWPeerPort;
        private System.Windows.Forms.DataGridViewImageColumn clFWQueryPort;
        private System.Windows.Forms.DataGridViewImageColumn clFWRconPort;
        private System.Windows.Forms.DataGridViewImageColumn clAddFW;
        private System.Windows.Forms.DataGridViewImageColumn clDeleteFW;
        private System.Windows.Forms.DataGridViewImageColumn clServerPort;
        private System.Windows.Forms.DataGridViewImageColumn clPeerPort;
        private System.Windows.Forms.DataGridViewImageColumn clQueryPort;
        private System.Windows.Forms.DataGridViewImageColumn clRconPort;
        private System.Windows.Forms.DataGridViewImageColumn clAddRouter;
        private System.Windows.Forms.DataGridViewImageColumn clDeleteRouter;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn2;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn3;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn4;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn5;
    }
}