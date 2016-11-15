namespace PurchaseComponent.UI
{
    partial class frmGRNBatch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGRNBatch));
            this.dtpExpiryDate = new System.Windows.Forms.DateTimePicker();
            this.dtpMfgDate = new System.Windows.Forms.DateTimePicker();
            this.lblExpiryDate = new System.Windows.Forms.Label();
            this.lblMfgDate = new System.Windows.Forms.Label();
            this.txtReceivedQty = new System.Windows.Forms.TextBox();
            this.lblReceivedQty = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.dgvBatchDetails = new System.Windows.Forms.DataGridView();
            this.txtManuBatchNo = new System.Windows.Forms.TextBox();
            this.lblManuBatchNo = new System.Windows.Forms.Label();
            this.txtItemCode = new System.Windows.Forms.TextBox();
            this.lblItemCode = new System.Windows.Forms.Label();
            this.txtMRP = new System.Windows.Forms.TextBox();
            this.lblMRP = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.errorAdd = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBatchDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorAdd)).BeginInit();
            this.SuspendLayout();
            // 
            // dtpExpiryDate
            // 
            this.dtpExpiryDate.CustomFormat = "dd-MM-yy";
            this.dtpExpiryDate.DropDownAlign = System.Windows.Forms.LeftRightAlignment.Right;
            this.dtpExpiryDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpExpiryDate.Location = new System.Drawing.Point(591, 35);
            this.dtpExpiryDate.Name = "dtpExpiryDate";
            this.dtpExpiryDate.Size = new System.Drawing.Size(110, 20);
            this.dtpExpiryDate.TabIndex = 4;
            // 
            // dtpMfgDate
            // 
            this.dtpMfgDate.CustomFormat = "dd-MM-yy";
            this.dtpMfgDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpMfgDate.Location = new System.Drawing.Point(371, 35);
            this.dtpMfgDate.Name = "dtpMfgDate";
            this.dtpMfgDate.Size = new System.Drawing.Size(110, 20);
            this.dtpMfgDate.TabIndex = 3;
            this.dtpMfgDate.ValueChanged += new System.EventHandler(this.dtpMfgDate_ValueChanged);
            // 
            // lblExpiryDate
            // 
            this.lblExpiryDate.BackColor = System.Drawing.Color.Transparent;
            this.lblExpiryDate.Location = new System.Drawing.Point(506, 41);
            this.lblExpiryDate.Name = "lblExpiryDate";
            this.lblExpiryDate.Size = new System.Drawing.Size(75, 13);
            this.lblExpiryDate.TabIndex = 144;
            this.lblExpiryDate.Text = "Expiry Date:*";
            this.lblExpiryDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblMfgDate
            // 
            this.lblMfgDate.BackColor = System.Drawing.Color.Transparent;
            this.lblMfgDate.Location = new System.Drawing.Point(277, 41);
            this.lblMfgDate.Name = "lblMfgDate";
            this.lblMfgDate.Size = new System.Drawing.Size(75, 13);
            this.lblMfgDate.TabIndex = 143;
            this.lblMfgDate.Text = "Mfg. Date:*";
            this.lblMfgDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtReceivedQty
            // 
            this.txtReceivedQty.Location = new System.Drawing.Point(371, 5);
            this.txtReceivedQty.MaxLength = 12;
            this.txtReceivedQty.Name = "txtReceivedQty";
            this.txtReceivedQty.Size = new System.Drawing.Size(110, 20);
            this.txtReceivedQty.TabIndex = 0;
            // 
            // lblReceivedQty
            // 
            this.lblReceivedQty.BackColor = System.Drawing.Color.Transparent;
            this.lblReceivedQty.Location = new System.Drawing.Point(265, 12);
            this.lblReceivedQty.Name = "lblReceivedQty";
            this.lblReceivedQty.Size = new System.Drawing.Size(100, 13);
            this.lblReceivedQty.TabIndex = 148;
            this.lblReceivedQty.Text = "Received Qty:*";
            this.lblReceivedQty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.btnAdd.Location = new System.Drawing.Point(470, 71);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 32);
            this.btnAdd.TabIndex = 5;
            this.btnAdd.Text = "&Add";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
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
            this.btnClear.Location = new System.Drawing.Point(551, 71);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 32);
            this.btnClear.TabIndex = 6;
            this.btnClear.Text = "&Clear";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // dgvBatchDetails
            // 
            this.dgvBatchDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBatchDetails.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvBatchDetails.Location = new System.Drawing.Point(0, 116);
            this.dgvBatchDetails.Name = "dgvBatchDetails";
            this.dgvBatchDetails.RowHeadersVisible = false;
            this.dgvBatchDetails.Size = new System.Drawing.Size(714, 198);
            this.dgvBatchDetails.TabIndex = 8;
            this.dgvBatchDetails.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBatchDetails_CellClick);
            this.dgvBatchDetails.SelectionChanged += new System.EventHandler(this.dgvBatchDetails_SelectionChanged);
            // 
            // txtManuBatchNo
            // 
            this.txtManuBatchNo.Location = new System.Drawing.Point(140, 35);
            this.txtManuBatchNo.MaxLength = 20;
            this.txtManuBatchNo.Name = "txtManuBatchNo";
            this.txtManuBatchNo.Size = new System.Drawing.Size(110, 20);
            this.txtManuBatchNo.TabIndex = 2;
            // 
            // lblManuBatchNo
            // 
            this.lblManuBatchNo.BackColor = System.Drawing.Color.Transparent;
            this.lblManuBatchNo.Location = new System.Drawing.Point(9, 38);
            this.lblManuBatchNo.Name = "lblManuBatchNo";
            this.lblManuBatchNo.Size = new System.Drawing.Size(125, 13);
            this.lblManuBatchNo.TabIndex = 157;
            this.lblManuBatchNo.Text = "Manufacturer Batch No:*";
            this.lblManuBatchNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtItemCode
            // 
            this.txtItemCode.Location = new System.Drawing.Point(140, 5);
            this.txtItemCode.MaxLength = 20;
            this.txtItemCode.Name = "txtItemCode";
            this.txtItemCode.ReadOnly = true;
            this.txtItemCode.Size = new System.Drawing.Size(110, 20);
            this.txtItemCode.TabIndex = 0;
            this.txtItemCode.TabStop = false;
            // 
            // lblItemCode
            // 
            this.lblItemCode.BackColor = System.Drawing.Color.Transparent;
            this.lblItemCode.Location = new System.Drawing.Point(59, 12);
            this.lblItemCode.Name = "lblItemCode";
            this.lblItemCode.Size = new System.Drawing.Size(75, 13);
            this.lblItemCode.TabIndex = 159;
            this.lblItemCode.Text = "Item Code:";
            this.lblItemCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtMRP
            // 
            this.txtMRP.Location = new System.Drawing.Point(591, 9);
            this.txtMRP.MaxLength = 20;
            this.txtMRP.Name = "txtMRP";
            this.txtMRP.Size = new System.Drawing.Size(110, 20);
            this.txtMRP.TabIndex = 1;
            // 
            // lblMRP
            // 
            this.lblMRP.BackColor = System.Drawing.Color.Transparent;
            this.lblMRP.Location = new System.Drawing.Point(506, 14);
            this.lblMRP.Name = "lblMRP";
            this.lblMRP.Size = new System.Drawing.Size(75, 13);
            this.lblMRP.TabIndex = 161;
            this.lblMRP.Text = "MRP:*";
            this.lblMRP.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.btnExit.Location = new System.Drawing.Point(632, 71);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 32);
            this.btnExit.TabIndex = 7;
            this.btnExit.Text = "E&xit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // errorAdd
            // 
            this.errorAdd.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorAdd.ContainerControl = this;
            // 
            // frmGRNBatch
            // 
            this.AcceptButton = this.btnAdd;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(233)))), ((int)(((byte)(251)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(714, 314);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.txtMRP);
            this.Controls.Add(this.lblMRP);
            this.Controls.Add(this.txtItemCode);
            this.Controls.Add(this.lblItemCode);
            this.Controls.Add(this.txtManuBatchNo);
            this.Controls.Add(this.lblManuBatchNo);
            this.Controls.Add(this.dgvBatchDetails);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.txtReceivedQty);
            this.Controls.Add(this.lblReceivedQty);
            this.Controls.Add(this.dtpExpiryDate);
            this.Controls.Add(this.dtpMfgDate);
            this.Controls.Add(this.lblExpiryDate);
            this.Controls.Add(this.lblMfgDate);
            this.DoubleBuffered = true;
            this.Name = "frmGRNBatch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "GRN Batch Details";
            ((System.ComponentModel.ISupportInitialize)(this.dgvBatchDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorAdd)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpExpiryDate;
        private System.Windows.Forms.DateTimePicker dtpMfgDate;
        private System.Windows.Forms.Label lblExpiryDate;
        private System.Windows.Forms.Label lblMfgDate;
        private System.Windows.Forms.TextBox txtReceivedQty;
        private System.Windows.Forms.Label lblReceivedQty;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.DataGridView dgvBatchDetails;
        private System.Windows.Forms.TextBox txtManuBatchNo;
        private System.Windows.Forms.Label lblManuBatchNo;
        private System.Windows.Forms.TextBox txtItemCode;
        private System.Windows.Forms.Label lblItemCode;
        private System.Windows.Forms.TextBox txtMRP;
        private System.Windows.Forms.Label lblMRP;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.ErrorProvider errorAdd;
    }
}