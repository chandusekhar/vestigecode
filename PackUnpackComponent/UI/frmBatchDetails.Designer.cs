namespace PackUnpackComponent.UI
{
    partial class frmBatchDetails
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBatchDetails));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.cmbMfgBatch = new System.Windows.Forms.ComboBox();
            this.lblMfgBatch = new System.Windows.Forms.Label();
            this.lblItemCode = new System.Windows.Forms.Label();
            this.dgvBatchDetails = new System.Windows.Forms.DataGridView();
            this.errBatchDetails = new System.Windows.Forms.ErrorProvider(this.components);
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.lblDisplayItemCode = new System.Windows.Forms.Label();
            this.lblTotalUnPackQty = new System.Windows.Forms.Label();
            this.lblDisplayTotalUnpackQty = new System.Windows.Forms.Label();
            this.lblBatchUnPackQty = new System.Windows.Forms.Label();
            this.txtBatchUnPackQty = new System.Windows.Forms.TextBox();
            this.lblExpDate = new System.Windows.Forms.Label();
            this.lblMfgDate = new System.Windows.Forms.Label();
            this.lblMrp = new System.Windows.Forms.Label();
            this.lblDisplayMfgDate = new System.Windows.Forms.Label();
            this.lblDisplayMRP = new System.Windows.Forms.Label();
            this.lblDisplayExpDate = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBatchDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errBatchDetails)).BeginInit();
            this.SuspendLayout();
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
            this.btnAdd.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Location = new System.Drawing.Point(386, 89);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 33);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "&Add";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.Transparent;
            this.btnOK.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOK.BackgroundImage")));
            this.btnOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOK.FlatAppearance.BorderSize = 0;
            this.btnOK.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnOK.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Location = new System.Drawing.Point(386, 274);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 33);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "&Ok";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // cmbMfgBatch
            // 
            this.cmbMfgBatch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMfgBatch.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbMfgBatch.FormattingEnabled = true;
            this.cmbMfgBatch.Location = new System.Drawing.Point(143, 33);
            this.cmbMfgBatch.Name = "cmbMfgBatch";
            this.cmbMfgBatch.Size = new System.Drawing.Size(125, 21);
            this.cmbMfgBatch.TabIndex = 0;
            this.cmbMfgBatch.SelectedIndexChanged += new System.EventHandler(this.cmbMfgBatch_SelectedIndexChanged);
            // 
            // lblMfgBatch
            // 
            this.lblMfgBatch.AutoSize = true;
            this.lblMfgBatch.BackColor = System.Drawing.Color.Transparent;
            this.lblMfgBatch.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMfgBatch.Location = new System.Drawing.Point(49, 33);
            this.lblMfgBatch.Name = "lblMfgBatch";
            this.lblMfgBatch.Size = new System.Drawing.Size(87, 13);
            this.lblMfgBatch.TabIndex = 0;
            this.lblMfgBatch.Text = "Mfg. BatchNo:";
            // 
            // lblItemCode
            // 
            this.lblItemCode.BackColor = System.Drawing.Color.Transparent;
            this.lblItemCode.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblItemCode.Location = new System.Drawing.Point(49, 5);
            this.lblItemCode.Name = "lblItemCode";
            this.lblItemCode.Size = new System.Drawing.Size(87, 13);
            this.lblItemCode.TabIndex = 0;
            this.lblItemCode.Text = "ItemCode:";
            this.lblItemCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dgvBatchDetails
            // 
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvBatchDetails.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvBatchDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvBatchDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvBatchDetails.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvBatchDetails.Location = new System.Drawing.Point(12, 132);
            this.dgvBatchDetails.Name = "dgvBatchDetails";
            this.dgvBatchDetails.RowHeadersVisible = false;
            this.dgvBatchDetails.Size = new System.Drawing.Size(539, 136);
            this.dgvBatchDetails.TabIndex = 6;
            this.dgvBatchDetails.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvBatchDetail_CellMouseClick);
            this.dgvBatchDetails.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBatchDetails_CellContentClick);
            // 
            // errBatchDetails
            // 
            this.errBatchDetails.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errBatchDetails.ContainerControl = this;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.BackgroundImage")));
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(476, 274);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 33);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnReset
            // 
            this.btnReset.BackColor = System.Drawing.Color.Transparent;
            this.btnReset.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnReset.BackgroundImage")));
            this.btnReset.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnReset.FlatAppearance.BorderSize = 0;
            this.btnReset.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnReset.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReset.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.Location = new System.Drawing.Point(476, 89);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 33);
            this.btnReset.TabIndex = 3;
            this.btnReset.Text = "&Reset";
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // lblDisplayItemCode
            // 
            this.lblDisplayItemCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDisplayItemCode.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDisplayItemCode.Location = new System.Drawing.Point(143, 5);
            this.lblDisplayItemCode.Name = "lblDisplayItemCode";
            this.lblDisplayItemCode.Size = new System.Drawing.Size(125, 20);
            this.lblDisplayItemCode.TabIndex = 9;
            this.lblDisplayItemCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotalUnPackQty
            // 
            this.lblTotalUnPackQty.BackColor = System.Drawing.Color.Transparent;
            this.lblTotalUnPackQty.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalUnPackQty.Location = new System.Drawing.Point(276, 5);
            this.lblTotalUnPackQty.Name = "lblTotalUnPackQty";
            this.lblTotalUnPackQty.Size = new System.Drawing.Size(144, 14);
            this.lblTotalUnPackQty.TabIndex = 10;
            this.lblTotalUnPackQty.Text = "Total Unpack Qty.: ";
            this.lblTotalUnPackQty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDisplayTotalUnpackQty
            // 
            this.lblDisplayTotalUnpackQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDisplayTotalUnpackQty.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDisplayTotalUnpackQty.Location = new System.Drawing.Point(426, 5);
            this.lblDisplayTotalUnpackQty.Name = "lblDisplayTotalUnpackQty";
            this.lblDisplayTotalUnpackQty.Size = new System.Drawing.Size(125, 20);
            this.lblDisplayTotalUnpackQty.TabIndex = 11;
            this.lblDisplayTotalUnpackQty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblBatchUnPackQty
            // 
            this.lblBatchUnPackQty.BackColor = System.Drawing.Color.Transparent;
            this.lblBatchUnPackQty.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBatchUnPackQty.Location = new System.Drawing.Point(6, 90);
            this.lblBatchUnPackQty.Name = "lblBatchUnPackQty";
            this.lblBatchUnPackQty.Size = new System.Drawing.Size(130, 13);
            this.lblBatchUnPackQty.TabIndex = 12;
            this.lblBatchUnPackQty.Text = "Batch Unpack Qty.: ";
            this.lblBatchUnPackQty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtBatchUnPackQty
            // 
            this.txtBatchUnPackQty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtBatchUnPackQty.Location = new System.Drawing.Point(142, 90);
            this.txtBatchUnPackQty.MaxLength = 5;
            this.txtBatchUnPackQty.Name = "txtBatchUnPackQty";
            this.txtBatchUnPackQty.Size = new System.Drawing.Size(126, 20);
            this.txtBatchUnPackQty.TabIndex = 1;
            // 
            // lblExpDate
            // 
            this.lblExpDate.BackColor = System.Drawing.Color.Transparent;
            this.lblExpDate.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblExpDate.Location = new System.Drawing.Point(295, 62);
            this.lblExpDate.Name = "lblExpDate";
            this.lblExpDate.Size = new System.Drawing.Size(125, 12);
            this.lblExpDate.TabIndex = 126;
            this.lblExpDate.Text = "Exp. Date:";
            this.lblExpDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblMfgDate
            // 
            this.lblMfgDate.BackColor = System.Drawing.Color.Transparent;
            this.lblMfgDate.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblMfgDate.Location = new System.Drawing.Point(75, 62);
            this.lblMfgDate.Name = "lblMfgDate";
            this.lblMfgDate.Size = new System.Drawing.Size(61, 13);
            this.lblMfgDate.TabIndex = 125;
            this.lblMfgDate.Text = "MFG Date: ";
            this.lblMfgDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblMrp
            // 
            this.lblMrp.BackColor = System.Drawing.Color.Transparent;
            this.lblMrp.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblMrp.Location = new System.Drawing.Point(324, 33);
            this.lblMrp.Name = "lblMrp";
            this.lblMrp.Size = new System.Drawing.Size(96, 13);
            this.lblMrp.TabIndex = 124;
            this.lblMrp.Text = "MRP: ";
            this.lblMrp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDisplayMfgDate
            // 
            this.lblDisplayMfgDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDisplayMfgDate.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDisplayMfgDate.Location = new System.Drawing.Point(143, 62);
            this.lblDisplayMfgDate.Name = "lblDisplayMfgDate";
            this.lblDisplayMfgDate.Size = new System.Drawing.Size(125, 20);
            this.lblDisplayMfgDate.TabIndex = 127;
            this.lblDisplayMfgDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDisplayMRP
            // 
            this.lblDisplayMRP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDisplayMRP.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDisplayMRP.Location = new System.Drawing.Point(426, 34);
            this.lblDisplayMRP.Name = "lblDisplayMRP";
            this.lblDisplayMRP.Size = new System.Drawing.Size(125, 20);
            this.lblDisplayMRP.TabIndex = 128;
            this.lblDisplayMRP.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDisplayExpDate
            // 
            this.lblDisplayExpDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDisplayExpDate.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDisplayExpDate.Location = new System.Drawing.Point(426, 62);
            this.lblDisplayExpDate.Name = "lblDisplayExpDate";
            this.lblDisplayExpDate.Size = new System.Drawing.Size(125, 20);
            this.lblDisplayExpDate.TabIndex = 129;
            this.lblDisplayExpDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // frmBatchDetails
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(566, 314);
            this.ControlBox = false;
            this.Controls.Add(this.lblDisplayExpDate);
            this.Controls.Add(this.lblDisplayMRP);
            this.Controls.Add(this.lblDisplayMfgDate);
            this.Controls.Add(this.lblExpDate);
            this.Controls.Add(this.lblMfgDate);
            this.Controls.Add(this.lblMrp);
            this.Controls.Add(this.txtBatchUnPackQty);
            this.Controls.Add(this.lblBatchUnPackQty);
            this.Controls.Add(this.lblDisplayTotalUnpackQty);
            this.Controls.Add(this.lblTotalUnPackQty);
            this.Controls.Add(this.lblDisplayItemCode);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.dgvBatchDetails);
            this.Controls.Add(this.lblItemCode);
            this.Controls.Add(this.lblMfgBatch);
            this.Controls.Add(this.cmbMfgBatch);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnAdd);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBatchDetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Batch Details";
            this.Load += new System.EventHandler(this.frmBatchDetails_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBatchDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errBatchDetails)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ComboBox cmbMfgBatch;
        private System.Windows.Forms.Label lblMfgBatch;
        private System.Windows.Forms.Label lblItemCode;
        private System.Windows.Forms.DataGridView dgvBatchDetails;
        private System.Windows.Forms.ErrorProvider errBatchDetails;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Label lblDisplayItemCode;
        private System.Windows.Forms.Label lblTotalUnPackQty;
        private System.Windows.Forms.Label lblDisplayTotalUnpackQty;
        private System.Windows.Forms.Label lblBatchUnPackQty;
        private System.Windows.Forms.TextBox txtBatchUnPackQty;
        private System.Windows.Forms.Label lblExpDate;
        private System.Windows.Forms.Label lblMfgDate;
        private System.Windows.Forms.Label lblMrp;
        private System.Windows.Forms.Label lblDisplayMfgDate;
        private System.Windows.Forms.Label lblDisplayExpDate;
        private System.Windows.Forms.Label lblDisplayMRP;
    }
}