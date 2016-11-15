namespace POSClient.UI
{
    partial class frmGiftVoucher
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGiftVoucher));
            this.lblDistributor = new System.Windows.Forms.Label();
            this.lblDistributorValue = new System.Windows.Forms.Label();
            this.dgvGiftVoucher = new System.Windows.Forms.DataGridView();
            this.dgvGiftVoucherItem = new System.Windows.Forms.DataGridView();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.gbGiftVoucher = new System.Windows.Forms.GroupBox();
            this.gbItems = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGiftVoucher)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGiftVoucherItem)).BeginInit();
            this.gbGiftVoucher.SuspendLayout();
            this.gbItems.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblDistributor
            // 
            this.lblDistributor.AutoSize = true;
            this.lblDistributor.BackColor = System.Drawing.Color.Transparent;
            this.lblDistributor.Location = new System.Drawing.Point(12, 9);
            this.lblDistributor.Name = "lblDistributor";
            this.lblDistributor.Size = new System.Drawing.Size(108, 16);
            this.lblDistributor.TabIndex = 0;
            this.lblDistributor.Text = "Distributor No";
            // 
            // lblDistributorValue
            // 
            this.lblDistributorValue.AutoSize = true;
            this.lblDistributorValue.BackColor = System.Drawing.Color.Transparent;
            this.lblDistributorValue.Location = new System.Drawing.Point(135, 9);
            this.lblDistributorValue.Name = "lblDistributorValue";
            this.lblDistributorValue.Size = new System.Drawing.Size(0, 16);
            this.lblDistributorValue.TabIndex = 1;
            // 
            // dgvGiftVoucher
            // 
            this.dgvGiftVoucher.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGiftVoucher.Location = new System.Drawing.Point(0, 22);
            this.dgvGiftVoucher.Name = "dgvGiftVoucher";
            this.dgvGiftVoucher.RowHeadersVisible = false;
            this.dgvGiftVoucher.Size = new System.Drawing.Size(578, 109);
            this.dgvGiftVoucher.TabIndex = 2;
            this.dgvGiftVoucher.SelectionChanged += new System.EventHandler(this.dgvGiftVoucher_SelectionChanged);
            // 
            // dgvGiftVoucherItem
            // 
            this.dgvGiftVoucherItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGiftVoucherItem.Location = new System.Drawing.Point(0, 18);
            this.dgvGiftVoucherItem.Name = "dgvGiftVoucherItem";
            this.dgvGiftVoucherItem.RowHeadersVisible = false;
            this.dgvGiftVoucherItem.Size = new System.Drawing.Size(578, 129);
            this.dgvGiftVoucherItem.TabIndex = 3;
            this.dgvGiftVoucherItem.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvGiftVoucherItem_CellMouseDoubleClick);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btnCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.BackgroundImage")));
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.Location = new System.Drawing.Point(505, 335);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 62);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(219)))), ((int)(((byte)(192)))));
            this.btnOk.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOk.BackgroundImage")));
            this.btnOk.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnOk.Location = new System.Drawing.Point(424, 335);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(72, 62);
            this.btnOk.TabIndex = 14;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // gbGiftVoucher
            // 
            this.gbGiftVoucher.BackColor = System.Drawing.Color.Transparent;
            this.gbGiftVoucher.Controls.Add(this.dgvGiftVoucher);
            this.gbGiftVoucher.Location = new System.Drawing.Point(3, 34);
            this.gbGiftVoucher.Name = "gbGiftVoucher";
            this.gbGiftVoucher.Size = new System.Drawing.Size(578, 137);
            this.gbGiftVoucher.TabIndex = 16;
            this.gbGiftVoucher.TabStop = false;
            this.gbGiftVoucher.Text = "Gift Vouchers";
            // 
            // gbItems
            // 
            this.gbItems.BackColor = System.Drawing.Color.Transparent;
            this.gbItems.Controls.Add(this.dgvGiftVoucherItem);
            this.gbItems.Location = new System.Drawing.Point(3, 177);
            this.gbItems.Name = "gbItems";
            this.gbItems.Size = new System.Drawing.Size(578, 153);
            this.gbItems.TabIndex = 17;
            this.gbItems.TabStop = false;
            this.gbItems.Text = "Items";
            // 
            // frmGiftVoucher
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(589, 408);
            this.Controls.Add(this.gbItems);
            this.Controls.Add(this.gbGiftVoucher);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.lblDistributorValue);
            this.Controls.Add(this.lblDistributor);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmGiftVoucher";
            this.Text = "frmGiftVoucher";
            ((System.ComponentModel.ISupportInitialize)(this.dgvGiftVoucher)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGiftVoucherItem)).EndInit();
            this.gbGiftVoucher.ResumeLayout(false);
            this.gbItems.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDistributor;
        private System.Windows.Forms.Label lblDistributorValue;
        private System.Windows.Forms.DataGridView dgvGiftVoucher;
        private System.Windows.Forms.DataGridView dgvGiftVoucherItem;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.GroupBox gbGiftVoucher;
        private System.Windows.Forms.GroupBox gbItems;
    }
}