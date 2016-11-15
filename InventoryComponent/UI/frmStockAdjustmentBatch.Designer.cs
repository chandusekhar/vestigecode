namespace InventoryComponent.UI
{
    partial class frmStockAdjustmentBatch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmStockAdjustmentBatch));
            this.btnExit = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtManuBatchNo = new System.Windows.Forms.TextBox();
            this.lblManuBatchNo = new System.Windows.Forms.Label();
            this.dtpExpiryDate = new System.Windows.Forms.DateTimePicker();
            this.dtpMfgDate = new System.Windows.Forms.DateTimePicker();
            this.lblExpiryDate = new System.Windows.Forms.Label();
            this.lblMfgDate = new System.Windows.Forms.Label();
            this.dgvBatchDetails = new System.Windows.Forms.DataGridView();
            this.errorAdd = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBatchDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorAdd)).BeginInit();
            this.SuspendLayout();
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExit.BackgroundImage")));
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnExit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(582, 78);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 32);
            this.btnExit.TabIndex = 11;
            this.btnExit.Text = "E&xit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.Transparent;
            this.btnClear.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClear.BackgroundImage")));
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClear.FlatAppearance.BorderSize = 0;
            this.btnClear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnClear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.Location = new System.Drawing.Point(499, 78);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 32);
            this.btnClear.TabIndex = 10;
            this.btnClear.Text = "&Clear";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.Transparent;
            this.btnAdd.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAdd.BackgroundImage")));
            this.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnAdd.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Location = new System.Drawing.Point(408, 78);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 32);
            this.btnAdd.TabIndex = 9;
            this.btnAdd.Text = "&Add";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtManuBatchNo
            // 
            this.txtManuBatchNo.Location = new System.Drawing.Point(141, 32);
            this.txtManuBatchNo.MaxLength = 20;
            this.txtManuBatchNo.Name = "txtManuBatchNo";
            this.txtManuBatchNo.Size = new System.Drawing.Size(110, 20);
            this.txtManuBatchNo.TabIndex = 158;
            // 
            // lblManuBatchNo
            // 
            this.lblManuBatchNo.BackColor = System.Drawing.Color.Transparent;
            this.lblManuBatchNo.Location = new System.Drawing.Point(10, 35);
            this.lblManuBatchNo.Name = "lblManuBatchNo";
            this.lblManuBatchNo.Size = new System.Drawing.Size(125, 13);
            this.lblManuBatchNo.TabIndex = 163;
            this.lblManuBatchNo.Text = "Manufacturer Batch No:*";
            this.lblManuBatchNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpExpiryDate
            // 
            this.dtpExpiryDate.CustomFormat = "dd-MM-yy";
            this.dtpExpiryDate.DropDownAlign = System.Windows.Forms.LeftRightAlignment.Right;
            this.dtpExpiryDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpExpiryDate.Location = new System.Drawing.Point(547, 33);
            this.dtpExpiryDate.Name = "dtpExpiryDate";
            this.dtpExpiryDate.Size = new System.Drawing.Size(110, 20);
            this.dtpExpiryDate.TabIndex = 4;
            // 
            // dtpMfgDate
            // 
            this.dtpMfgDate.CustomFormat = "dd-MM-yy";
            this.dtpMfgDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpMfgDate.Location = new System.Drawing.Point(338, 32);
            this.dtpMfgDate.Name = "dtpMfgDate";
            this.dtpMfgDate.Size = new System.Drawing.Size(110, 20);
            this.dtpMfgDate.TabIndex = 3;
            // 
            // lblExpiryDate
            // 
            this.lblExpiryDate.BackColor = System.Drawing.Color.Transparent;
            this.lblExpiryDate.Location = new System.Drawing.Point(463, 36);
            this.lblExpiryDate.Name = "lblExpiryDate";
            this.lblExpiryDate.Size = new System.Drawing.Size(75, 13);
            this.lblExpiryDate.TabIndex = 162;
            this.lblExpiryDate.Text = "Expiry Date:*";
            this.lblExpiryDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblMfgDate
            // 
            this.lblMfgDate.BackColor = System.Drawing.Color.Transparent;
            this.lblMfgDate.Location = new System.Drawing.Point(257, 36);
            this.lblMfgDate.Name = "lblMfgDate";
            this.lblMfgDate.Size = new System.Drawing.Size(75, 13);
            this.lblMfgDate.TabIndex = 161;
            this.lblMfgDate.Text = "Mfg. Date:*";
            this.lblMfgDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dgvBatchDetails
            // 
            this.dgvBatchDetails.AllowUserToAddRows = false;
            this.dgvBatchDetails.AllowUserToDeleteRows = false;
            this.dgvBatchDetails.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dgvBatchDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBatchDetails.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvBatchDetails.Location = new System.Drawing.Point(0, 130);
            this.dgvBatchDetails.MultiSelect = false;
            this.dgvBatchDetails.Name = "dgvBatchDetails";
            this.dgvBatchDetails.RowHeadersVisible = false;
            this.dgvBatchDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBatchDetails.Size = new System.Drawing.Size(663, 198);
            this.dgvBatchDetails.TabIndex = 164;
            this.dgvBatchDetails.TabStop = false;
            this.dgvBatchDetails.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBatchDetails_CellClick);
            this.dgvBatchDetails.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvBatchDetails_CellMouseDoubleClick);
            this.dgvBatchDetails.SelectionChanged += new System.EventHandler(this.dgvBatchDetails_SelectionChanged);
            // 
            // errorAdd
            // 
            this.errorAdd.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorAdd.ContainerControl = this;
            // 
            // frmStockAdjustmentBatch
            // 
            this.AcceptButton = this.btnAdd;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(233)))), ((int)(((byte)(251)))));
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(663, 328);
            this.Controls.Add(this.dgvBatchDetails);
            this.Controls.Add(this.txtManuBatchNo);
            this.Controls.Add(this.lblManuBatchNo);
            this.Controls.Add(this.dtpExpiryDate);
            this.Controls.Add(this.dtpMfgDate);
            this.Controls.Add(this.lblExpiryDate);
            this.Controls.Add(this.lblMfgDate);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnAdd);
            this.DoubleBuffered = true;
            this.Name = "frmStockAdjustmentBatch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "StockAdjustmentBatch";
            ((System.ComponentModel.ISupportInitialize)(this.dgvBatchDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorAdd)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox txtManuBatchNo;
        private System.Windows.Forms.Label lblManuBatchNo;
        private System.Windows.Forms.DateTimePicker dtpExpiryDate;
        private System.Windows.Forms.DateTimePicker dtpMfgDate;
        private System.Windows.Forms.Label lblExpiryDate;
        private System.Windows.Forms.Label lblMfgDate;
       
        private System.Windows.Forms.DataGridView dgvBatchDetails;
        private System.Windows.Forms.ErrorProvider errorAdd;
    }
}