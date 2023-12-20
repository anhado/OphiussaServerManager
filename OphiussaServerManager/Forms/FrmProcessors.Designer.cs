namespace OphiussaServerManager.Forms
{
    partial class FrmProcessors
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmProcessors));
            this.label1 = new System.Windows.Forms.Label();
            this.chkAll = new System.Windows.Forms.CheckBox();
            this.grd = new System.Windows.Forms.DataGridView();
            this.selectedDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.processorNumberDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.processorAffinityList = new System.Windows.Forms.BindingSource(this.components);
            this.btAll = new System.Windows.Forms.Button();
            this.btNone = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.btOK = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.processorAffinityList)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(241, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Which processores are allowed to run the server?";
            // 
            // chkAll
            // 
            this.chkAll.AutoSize = true;
            this.chkAll.Location = new System.Drawing.Point(15, 25);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new System.Drawing.Size(91, 17);
            this.chkAll.TabIndex = 1;
            this.chkAll.Text = "All processors";
            this.chkAll.UseVisualStyleBackColor = true;
            this.chkAll.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // grd
            // 
            this.grd.AllowUserToAddRows = false;
            this.grd.AllowUserToDeleteRows = false;
            this.grd.AutoGenerateColumns = false;
            this.grd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grd.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.selectedDataGridViewCheckBoxColumn,
            this.processorNumberDataGridViewTextBoxColumn});
            this.grd.DataSource = this.processorAffinityList;
            this.grd.Location = new System.Drawing.Point(12, 48);
            this.grd.Name = "grd";
            this.grd.RowHeadersVisible = false;
            this.grd.Size = new System.Drawing.Size(253, 188);
            this.grd.TabIndex = 2;
            // 
            // selectedDataGridViewCheckBoxColumn
            // 
            this.selectedDataGridViewCheckBoxColumn.DataPropertyName = "Selected";
            this.selectedDataGridViewCheckBoxColumn.HeaderText = "Selected";
            this.selectedDataGridViewCheckBoxColumn.Name = "selectedDataGridViewCheckBoxColumn";
            // 
            // processorNumberDataGridViewTextBoxColumn
            // 
            this.processorNumberDataGridViewTextBoxColumn.DataPropertyName = "ProcessorNumber";
            this.processorNumberDataGridViewTextBoxColumn.HeaderText = "Processor";
            this.processorNumberDataGridViewTextBoxColumn.Name = "processorNumberDataGridViewTextBoxColumn";
            this.processorNumberDataGridViewTextBoxColumn.Width = 150;
            // 
            // processorAffinityList
            // 
            this.processorAffinityList.DataSource = typeof(OphiussaServerManager.Common.Models.ProcessorAffinity);
            // 
            // btAll
            // 
            this.btAll.Location = new System.Drawing.Point(15, 242);
            this.btAll.Name = "btAll";
            this.btAll.Size = new System.Drawing.Size(41, 23);
            this.btAll.TabIndex = 3;
            this.btAll.Text = "All";
            this.btAll.UseVisualStyleBackColor = true;
            this.btAll.Click += new System.EventHandler(this.btAll_Click);
            // 
            // btNone
            // 
            this.btNone.Location = new System.Drawing.Point(62, 242);
            this.btNone.Name = "btNone";
            this.btNone.Size = new System.Drawing.Size(41, 23);
            this.btNone.TabIndex = 4;
            this.btNone.Text = "None";
            this.btNone.UseVisualStyleBackColor = true;
            this.btNone.Click += new System.EventHandler(this.btNone_Click);
            // 
            // btCancel
            // 
            this.btCancel.Location = new System.Drawing.Point(190, 242);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 5;
            this.btCancel.Text = "Cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // btOK
            // 
            this.btOK.Location = new System.Drawing.Point(109, 242);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(75, 23);
            this.btOK.TabIndex = 6;
            this.btOK.Text = "OK";
            this.btOK.UseVisualStyleBackColor = true;
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // FrmProcessors
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(274, 277);
            this.Controls.Add(this.btOK);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btNone);
            this.Controls.Add(this.btAll);
            this.Controls.Add(this.grd);
            this.Controls.Add(this.chkAll);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmProcessors";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Processor Affinity";
            ((System.ComponentModel.ISupportInitialize)(this.grd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.processorAffinityList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkAll;
        private System.Windows.Forms.DataGridView grd;
        private System.Windows.Forms.Button btAll;
        private System.Windows.Forms.Button btNone;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.BindingSource processorAffinityList;
        private System.Windows.Forms.DataGridViewCheckBoxColumn selectedDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn processorNumberDataGridViewTextBoxColumn;
    }
}