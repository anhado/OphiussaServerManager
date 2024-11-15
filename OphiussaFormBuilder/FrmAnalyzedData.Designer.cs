﻿namespace OphiussaFormBuilder {
    partial class FrmAnalyzedData {
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
            this.components = new System.ComponentModel.Container();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.objectNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.propertyNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BindingLocation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.objectTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.minimumDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.maximumDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valueDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.listOfObjectsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listOfObjectsBindingSource)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.groupDataGridViewTextBoxColumn,
            this.objectNameDataGridViewTextBoxColumn,
            this.propertyNameDataGridViewTextBoxColumn,
            this.BindingLocation,
            this.Description,
            this.objectTypeDataGridViewTextBoxColumn,
            this.minimumDataGridViewTextBoxColumn,
            this.maximumDataGridViewTextBoxColumn,
            this.valueDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.listOfObjectsBindingSource;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(800, 414);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
            this.dataGridView1.CurrentCellDirtyStateChanged += new System.EventHandler(this.dataGridView1_CurrentCellDirtyStateChanged);
            this.dataGridView1.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridView1_DataError);
            // 
            // groupDataGridViewTextBoxColumn
            // 
            this.groupDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.groupDataGridViewTextBoxColumn.DataPropertyName = "Group";
            this.groupDataGridViewTextBoxColumn.HeaderText = "Group";
            this.groupDataGridViewTextBoxColumn.Name = "groupDataGridViewTextBoxColumn";
            // 
            // objectNameDataGridViewTextBoxColumn
            // 
            this.objectNameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.objectNameDataGridViewTextBoxColumn.DataPropertyName = "ObjectName";
            this.objectNameDataGridViewTextBoxColumn.HeaderText = "Object Name";
            this.objectNameDataGridViewTextBoxColumn.Name = "objectNameDataGridViewTextBoxColumn";
            this.objectNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // propertyNameDataGridViewTextBoxColumn
            // 
            this.propertyNameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.propertyNameDataGridViewTextBoxColumn.DataPropertyName = "PropertyName";
            this.propertyNameDataGridViewTextBoxColumn.HeaderText = "Property Name";
            this.propertyNameDataGridViewTextBoxColumn.Name = "propertyNameDataGridViewTextBoxColumn";
            // 
            // BindingLocation
            // 
            this.BindingLocation.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.BindingLocation.DataPropertyName = "BindingLocation";
            this.BindingLocation.HeaderText = "Binding Location";
            this.BindingLocation.Name = "BindingLocation";
            // 
            // Description
            // 
            this.Description.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Description.DataPropertyName = "Description";
            this.Description.HeaderText = "Description";
            this.Description.Name = "Description";
            // 
            // objectTypeDataGridViewTextBoxColumn
            // 
            this.objectTypeDataGridViewTextBoxColumn.DataPropertyName = "ObjectTypeColumn";
            this.objectTypeDataGridViewTextBoxColumn.HeaderText = "Object Type";
            this.objectTypeDataGridViewTextBoxColumn.Name = "objectTypeDataGridViewTextBoxColumn";
            this.objectTypeDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.objectTypeDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.objectTypeDataGridViewTextBoxColumn.Width = 91;
            // 
            // minimumDataGridViewTextBoxColumn
            // 
            this.minimumDataGridViewTextBoxColumn.DataPropertyName = "Minimum";
            this.minimumDataGridViewTextBoxColumn.HeaderText = "Minimum";
            this.minimumDataGridViewTextBoxColumn.Name = "minimumDataGridViewTextBoxColumn";
            // 
            // maximumDataGridViewTextBoxColumn
            // 
            this.maximumDataGridViewTextBoxColumn.DataPropertyName = "Maximum";
            this.maximumDataGridViewTextBoxColumn.HeaderText = "Maximum";
            this.maximumDataGridViewTextBoxColumn.Name = "maximumDataGridViewTextBoxColumn";
            // 
            // valueDataGridViewTextBoxColumn
            // 
            this.valueDataGridViewTextBoxColumn.DataPropertyName = "Value";
            this.valueDataGridViewTextBoxColumn.HeaderText = "Value";
            this.valueDataGridViewTextBoxColumn.Name = "valueDataGridViewTextBoxColumn";
            // 
            // listOfObjectsBindingSource
            // 
            this.listOfObjectsBindingSource.DataSource = typeof(OphiussaFormBuilder.Models.ObjectDefinition);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 36);
            this.panel1.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 7);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(105, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Generate Code";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dataGridView1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 36);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(800, 414);
            this.panel2.TabIndex = 2;
            // 
            // FrmAnalyzedData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "FrmAnalyzedData";
            this.Text = "Analyzed Data";
            this.Load += new System.EventHandler(this.FrmAnalyzedData_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listOfObjectsBindingSource)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.BindingSource listOfObjectsBindingSource;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewTextBoxColumn groupDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn objectNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn propertyNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn BindingLocation;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewComboBoxColumn objectTypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn minimumDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn maximumDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn valueDataGridViewTextBoxColumn;
    }
}