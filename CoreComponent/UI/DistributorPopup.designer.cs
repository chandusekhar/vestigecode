namespace CoreComponent.UI
{
    partial class DistributorPopup
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
            this.dgvDistributorList = new System.Windows.Forms.DataGridView();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDistributorList)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvDistributorList
            // 
            this.dgvDistributorList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDistributorList.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgvDistributorList.Location = new System.Drawing.Point(0, 0);
            this.dgvDistributorList.MultiSelect = false;
            this.dgvDistributorList.Name = "dgvDistributorList";
            this.dgvDistributorList.ReadOnly = true;
            this.dgvDistributorList.RowHeadersVisible = false;
            this.dgvDistributorList.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDistributorList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDistributorList.Size = new System.Drawing.Size(300, 200);
            this.dgvDistributorList.TabIndex = 0;
            this.dgvDistributorList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDistributorList_CellDoubleClick);
            this.dgvDistributorList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvDistributorList_KeyDown);
            this.dgvDistributorList.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dgvDistributorList_KeyUp);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(8, 8);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "Ok";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(121, 8);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // DistributorPopup
            // 
            this.AcceptButton = this.btnOK;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(300, 200);
            this.Controls.Add(this.dgvDistributorList);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "DistributorPopup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            ((System.ComponentModel.ISupportInitialize)(this.dgvDistributorList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvDistributorList;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
    }
}
