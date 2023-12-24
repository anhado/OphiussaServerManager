namespace OphiussaServerManager.Forms
{
    partial class FrmModManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmModManager));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.orderDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.modIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewLinkColumn();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.modTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lastDownloadedDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lastUpdatedAuthorDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timeStampDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.folderSizeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clUp = new System.Windows.Forms.DataGridViewImageColumn();
            this.clDown = new System.Windows.Forms.DataGridViewImageColumn();
            this.clDelete = new System.Windows.Forms.DataGridViewImageColumn();
            this.modListDetailsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewImageColumn2 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewImageColumn3 = new System.Windows.Forms.DataGridViewImageColumn();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btSavetooltip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.modListDetailsBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.orderDataGridViewTextBoxColumn,
            this.modIDDataGridViewTextBoxColumn,
            this.nameDataGridViewTextBoxColumn,
            this.modTypeDataGridViewTextBoxColumn,
            this.lastDownloadedDataGridViewTextBoxColumn,
            this.lastUpdatedAuthorDataGridViewTextBoxColumn,
            this.timeStampDataGridViewTextBoxColumn,
            this.folderSizeDataGridViewTextBoxColumn,
            this.clUp,
            this.clDown,
            this.clDelete});
            this.dataGridView1.DataSource = this.modListDetailsBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(12, 41);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(776, 397);
            this.dataGridView1.TabIndex = 5;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // orderDataGridViewTextBoxColumn
            // 
            this.orderDataGridViewTextBoxColumn.DataPropertyName = "Order";
            this.orderDataGridViewTextBoxColumn.HeaderText = "#";
            this.orderDataGridViewTextBoxColumn.Name = "orderDataGridViewTextBoxColumn";
            this.orderDataGridViewTextBoxColumn.ReadOnly = true;
            this.orderDataGridViewTextBoxColumn.Width = 25;
            // 
            // modIDDataGridViewTextBoxColumn
            // 
            this.modIDDataGridViewTextBoxColumn.DataPropertyName = "ModID";
            this.modIDDataGridViewTextBoxColumn.HeaderText = "Mod ID";
            this.modIDDataGridViewTextBoxColumn.Name = "modIDDataGridViewTextBoxColumn";
            this.modIDDataGridViewTextBoxColumn.ReadOnly = true;
            this.modIDDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.modIDDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.modIDDataGridViewTextBoxColumn.Width = 75;
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            this.nameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // modTypeDataGridViewTextBoxColumn
            // 
            this.modTypeDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.modTypeDataGridViewTextBoxColumn.DataPropertyName = "ModType";
            this.modTypeDataGridViewTextBoxColumn.HeaderText = "Mod Type";
            this.modTypeDataGridViewTextBoxColumn.Name = "modTypeDataGridViewTextBoxColumn";
            this.modTypeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // lastDownloadedDataGridViewTextBoxColumn
            // 
            this.lastDownloadedDataGridViewTextBoxColumn.DataPropertyName = "LastDownloaded";
            this.lastDownloadedDataGridViewTextBoxColumn.HeaderText = "Last Downloaded";
            this.lastDownloadedDataGridViewTextBoxColumn.Name = "lastDownloadedDataGridViewTextBoxColumn";
            this.lastDownloadedDataGridViewTextBoxColumn.ReadOnly = true;
            this.lastDownloadedDataGridViewTextBoxColumn.Width = 110;
            // 
            // lastUpdatedAuthorDataGridViewTextBoxColumn
            // 
            this.lastUpdatedAuthorDataGridViewTextBoxColumn.DataPropertyName = "LastUpdatedAuthor";
            this.lastUpdatedAuthorDataGridViewTextBoxColumn.HeaderText = "Last Updated(Author)";
            this.lastUpdatedAuthorDataGridViewTextBoxColumn.Name = "lastUpdatedAuthorDataGridViewTextBoxColumn";
            this.lastUpdatedAuthorDataGridViewTextBoxColumn.ReadOnly = true;
            this.lastUpdatedAuthorDataGridViewTextBoxColumn.Width = 110;
            // 
            // timeStampDataGridViewTextBoxColumn
            // 
            this.timeStampDataGridViewTextBoxColumn.DataPropertyName = "TimeStamp";
            this.timeStampDataGridViewTextBoxColumn.HeaderText = "TimeStamp";
            this.timeStampDataGridViewTextBoxColumn.Name = "timeStampDataGridViewTextBoxColumn";
            this.timeStampDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // folderSizeDataGridViewTextBoxColumn
            // 
            this.folderSizeDataGridViewTextBoxColumn.DataPropertyName = "FolderSize";
            this.folderSizeDataGridViewTextBoxColumn.HeaderText = "Folder Size";
            this.folderSizeDataGridViewTextBoxColumn.Name = "folderSizeDataGridViewTextBoxColumn";
            this.folderSizeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // clUp
            // 
            this.clUp.HeaderText = "";
            this.clUp.Image = global::OphiussaServerManager.Properties.Resources.CollapseIcon;
            this.clUp.Name = "clUp";
            this.clUp.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.clUp.Width = 32;
            // 
            // clDown
            // 
            this.clDown.HeaderText = "";
            this.clDown.Image = global::OphiussaServerManager.Properties.Resources.ExpandIcon;
            this.clDown.Name = "clDown";
            this.clDown.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.clDown.Width = 32;
            // 
            // clDelete
            // 
            this.clDelete.HeaderText = "";
            this.clDelete.Image = global::OphiussaServerManager.Properties.Resources.CloseIcon;
            this.clDelete.Name = "clDelete";
            this.clDelete.Width = 32;
            // 
            // modListDetailsBindingSource
            // 
            this.modListDetailsBindingSource.DataSource = typeof(OphiussaServerManager.Common.Models.ModListDetails);
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.HeaderText = "";
            this.dataGridViewImageColumn1.Image = global::OphiussaServerManager.Properties.Resources.CollapseIcon;
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewImageColumn1.Width = 32;
            // 
            // dataGridViewImageColumn2
            // 
            this.dataGridViewImageColumn2.HeaderText = "";
            this.dataGridViewImageColumn2.Image = global::OphiussaServerManager.Properties.Resources.ExpandIcon;
            this.dataGridViewImageColumn2.Name = "dataGridViewImageColumn2";
            this.dataGridViewImageColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewImageColumn2.Width = 32;
            // 
            // dataGridViewImageColumn3
            // 
            this.dataGridViewImageColumn3.HeaderText = "";
            this.dataGridViewImageColumn3.Image = global::OphiussaServerManager.Properties.Resources.CloseIcon;
            this.dataGridViewImageColumn3.Name = "dataGridViewImageColumn3";
            this.dataGridViewImageColumn3.Width = 32;
            // 
            // button5
            // 
            this.button5.Image = global::OphiussaServerManager.Properties.Resources.Redo_Icon16x16;
            this.button5.Location = new System.Drawing.Point(197, 3);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(32, 32);
            this.button5.TabIndex = 4;
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Image = global::OphiussaServerManager.Properties.Resources.RefeshIcon;
            this.button4.Location = new System.Drawing.Point(159, 3);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(32, 32);
            this.button4.TabIndex = 3;
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Image = global::OphiussaServerManager.Properties.Resources.DeleteIcon;
            this.button3.Location = new System.Drawing.Point(103, 3);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(32, 32);
            this.button3.TabIndex = 2;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Image = global::OphiussaServerManager.Properties.Resources.AddIcon32x32;
            this.button2.Location = new System.Drawing.Point(65, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(32, 32);
            this.button2.TabIndex = 1;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Image = global::OphiussaServerManager.Properties.Resources.SaveIcon;
            this.button1.Location = new System.Drawing.Point(12, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(32, 32);
            this.button1.TabIndex = 0;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // FrmModManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.ForeColor = System.Drawing.Color.SteelBlue;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmModManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Mod Details";
            this.Load += new System.EventHandler(this.FrmModManager_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.modListDetailsBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn orderDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewLinkColumn modIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn modTypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lastDownloadedDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lastUpdatedAuthorDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn timeStampDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn folderSizeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewImageColumn clUp;
        private System.Windows.Forms.DataGridViewImageColumn clDown;
        private System.Windows.Forms.DataGridViewImageColumn clDelete;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn2;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn3;
        internal System.Windows.Forms.BindingSource modListDetailsBindingSource;
        private System.Windows.Forms.ToolTip btSavetooltip;
    }
}