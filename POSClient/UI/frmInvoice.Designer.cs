namespace POSClient.UI
{
    partial class frmInvoice
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInvoice));
            this.pnlOrder = new System.Windows.Forms.Panel();
            this.lblOrderTypeValue = new System.Windows.Forms.Label();
            this.lblOrderType = new System.Windows.Forms.Label();
            this.lblDistributorNameValue = new System.Windows.Forms.Label();
            this.lblOrderDateValue = new System.Windows.Forms.Label();
            this.lblOrderNoValue = new System.Windows.Forms.Label();
            this.lblDistributor = new System.Windows.Forms.Label();
            this.lblOrderDate = new System.Windows.Forms.Label();
            this.lblOrderNo = new System.Windows.Forms.Label();
            this.pnlOrderDetail = new System.Windows.Forms.Panel();
            this.txtAvailableQty = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtMRP = new System.Windows.Forms.TextBox();
            this.txtInvoiceQty = new System.Windows.Forms.TextBox();
            this.txtExpiryDate = new System.Windows.Forms.TextBox();
            this.txtMfgbatchNo = new System.Windows.Forms.TextBox();
            this.txtMfgDate = new System.Windows.Forms.TextBox();
            this.txtBatchNo = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.lblMfgDate = new System.Windows.Forms.Label();
            this.lblMRP = new System.Windows.Forms.Label();
            this.lblmfgBatchNo = new System.Windows.Forms.Label();
            this.lblBatchNo = new System.Windows.Forms.Label();
            this.lblInvoiceQty = new System.Windows.Forms.Label();
            this.dgvInvoiceDetail = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnClearAll = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnInvoice = new System.Windows.Forms.Button();
            this.errorAdd = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorInvoice = new System.Windows.Forms.ErrorProvider(this.components);
            this.dgvCOItemDetails = new System.Windows.Forms.DataGridView();
            this.pnlOrder.SuspendLayout();
            this.pnlOrderDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInvoiceDetail)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorAdd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorInvoice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCOItemDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlOrder
            // 
            this.pnlOrder.BackColor = System.Drawing.Color.Transparent;
            this.pnlOrder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlOrder.Controls.Add(this.lblOrderTypeValue);
            this.pnlOrder.Controls.Add(this.lblOrderType);
            this.pnlOrder.Controls.Add(this.lblDistributorNameValue);
            this.pnlOrder.Controls.Add(this.lblOrderDateValue);
            this.pnlOrder.Controls.Add(this.lblOrderNoValue);
            this.pnlOrder.Controls.Add(this.lblDistributor);
            this.pnlOrder.Controls.Add(this.lblOrderDate);
            this.pnlOrder.Controls.Add(this.lblOrderNo);
            this.pnlOrder.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlOrder.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlOrder.Location = new System.Drawing.Point(0, 0);
            this.pnlOrder.Name = "pnlOrder";
            this.pnlOrder.Size = new System.Drawing.Size(864, 74);
            this.pnlOrder.TabIndex = 0;
            // 
            // lblOrderTypeValue
            // 
            this.lblOrderTypeValue.AutoSize = true;
            this.lblOrderTypeValue.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrderTypeValue.Location = new System.Drawing.Point(526, 20);
            this.lblOrderTypeValue.Name = "lblOrderTypeValue";
            this.lblOrderTypeValue.Size = new System.Drawing.Size(0, 13);
            this.lblOrderTypeValue.TabIndex = 15;
            // 
            // lblOrderType
            // 
            this.lblOrderType.AutoSize = true;
            this.lblOrderType.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrderType.Location = new System.Drawing.Point(432, 20);
            this.lblOrderType.Name = "lblOrderType";
            this.lblOrderType.Size = new System.Drawing.Size(88, 13);
            this.lblOrderType.TabIndex = 14;
            this.lblOrderType.Text = "Order Type :";
            this.lblOrderType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDistributorNameValue
            // 
            this.lblDistributorNameValue.AutoSize = true;
            this.lblDistributorNameValue.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDistributorNameValue.Location = new System.Drawing.Point(528, 50);
            this.lblDistributorNameValue.Name = "lblDistributorNameValue";
            this.lblDistributorNameValue.Size = new System.Drawing.Size(0, 13);
            this.lblDistributorNameValue.TabIndex = 5;
            // 
            // lblOrderDateValue
            // 
            this.lblOrderDateValue.AutoSize = true;
            this.lblOrderDateValue.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrderDateValue.Location = new System.Drawing.Point(156, 50);
            this.lblOrderDateValue.Name = "lblOrderDateValue";
            this.lblOrderDateValue.Size = new System.Drawing.Size(0, 13);
            this.lblOrderDateValue.TabIndex = 4;
            // 
            // lblOrderNoValue
            // 
            this.lblOrderNoValue.AutoSize = true;
            this.lblOrderNoValue.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrderNoValue.Location = new System.Drawing.Point(156, 20);
            this.lblOrderNoValue.Name = "lblOrderNoValue";
            this.lblOrderNoValue.Size = new System.Drawing.Size(0, 13);
            this.lblOrderNoValue.TabIndex = 3;
            // 
            // lblDistributor
            // 
            this.lblDistributor.AutoSize = true;
            this.lblDistributor.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDistributor.Location = new System.Drawing.Point(396, 50);
            this.lblDistributor.Name = "lblDistributor";
            this.lblDistributor.Size = new System.Drawing.Size(126, 13);
            this.lblDistributor.TabIndex = 2;
            this.lblDistributor.Text = "Distributor Name :";
            this.lblDistributor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblOrderDate
            // 
            this.lblOrderDate.AutoSize = true;
            this.lblOrderDate.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrderDate.Location = new System.Drawing.Point(47, 50);
            this.lblOrderDate.Name = "lblOrderDate";
            this.lblOrderDate.Size = new System.Drawing.Size(86, 13);
            this.lblOrderDate.TabIndex = 1;
            this.lblOrderDate.Text = "Order Date :";
            this.lblOrderDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblOrderNo
            // 
            this.lblOrderNo.AutoSize = true;
            this.lblOrderNo.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrderNo.Location = new System.Drawing.Point(60, 20);
            this.lblOrderNo.Name = "lblOrderNo";
            this.lblOrderNo.Size = new System.Drawing.Size(73, 13);
            this.lblOrderNo.TabIndex = 0;
            this.lblOrderNo.Text = "Order No :";
            this.lblOrderNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlOrderDetail
            // 
            this.pnlOrderDetail.BackColor = System.Drawing.Color.Transparent;
            this.pnlOrderDetail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlOrderDetail.Controls.Add(this.txtAvailableQty);
            this.pnlOrderDetail.Controls.Add(this.label1);
            this.pnlOrderDetail.Controls.Add(this.btnClear);
            this.pnlOrderDetail.Controls.Add(this.btnAdd);
            this.pnlOrderDetail.Controls.Add(this.txtMRP);
            this.pnlOrderDetail.Controls.Add(this.txtInvoiceQty);
            this.pnlOrderDetail.Controls.Add(this.txtExpiryDate);
            this.pnlOrderDetail.Controls.Add(this.txtMfgbatchNo);
            this.pnlOrderDetail.Controls.Add(this.txtMfgDate);
            this.pnlOrderDetail.Controls.Add(this.txtBatchNo);
            this.pnlOrderDetail.Controls.Add(this.label7);
            this.pnlOrderDetail.Controls.Add(this.lblMfgDate);
            this.pnlOrderDetail.Controls.Add(this.lblMRP);
            this.pnlOrderDetail.Controls.Add(this.lblmfgBatchNo);
            this.pnlOrderDetail.Controls.Add(this.lblBatchNo);
            this.pnlOrderDetail.Controls.Add(this.lblInvoiceQty);
            this.pnlOrderDetail.Location = new System.Drawing.Point(0, 205);
            this.pnlOrderDetail.Name = "pnlOrderDetail";
            this.pnlOrderDetail.Size = new System.Drawing.Size(864, 124);
            this.pnlOrderDetail.TabIndex = 1;
            // 
            // txtAvailableQty
            // 
            this.txtAvailableQty.Location = new System.Drawing.Point(366, 9);
            this.txtAvailableQty.Name = "txtAvailableQty";
            this.txtAvailableQty.ReadOnly = true;
            this.txtAvailableQty.Size = new System.Drawing.Size(127, 23);
            this.txtAvailableQty.TabIndex = 20;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(258, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 19;
            this.label1.Text = "Available Qty :";
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btnClear.BackgroundImage = global::POSClient.Properties.Resources.GlassyLook;
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClear.Location = new System.Drawing.Point(777, 52);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(72, 62);
            this.btnClear.TabIndex = 4;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(219)))), ((int)(((byte)(192)))));
            this.btnAdd.BackgroundImage = global::POSClient.Properties.Resources.GlassyLook;
            this.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAdd.Location = new System.Drawing.Point(689, 52);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(72, 62);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtMRP
            // 
            this.txtMRP.Location = new System.Drawing.Point(116, 41);
            this.txtMRP.Name = "txtMRP";
            this.txtMRP.ReadOnly = true;
            this.txtMRP.Size = new System.Drawing.Size(127, 23);
            this.txtMRP.TabIndex = 16;
            // 
            // txtInvoiceQty
            // 
            this.txtInvoiceQty.Location = new System.Drawing.Point(601, 9);
            this.txtInvoiceQty.Name = "txtInvoiceQty";
            this.txtInvoiceQty.Size = new System.Drawing.Size(127, 23);
            this.txtInvoiceQty.TabIndex = 2;
            // 
            // txtExpiryDate
            // 
            this.txtExpiryDate.Location = new System.Drawing.Point(366, 73);
            this.txtExpiryDate.Name = "txtExpiryDate";
            this.txtExpiryDate.ReadOnly = true;
            this.txtExpiryDate.Size = new System.Drawing.Size(127, 23);
            this.txtExpiryDate.TabIndex = 14;
            // 
            // txtMfgbatchNo
            // 
            this.txtMfgbatchNo.Location = new System.Drawing.Point(366, 41);
            this.txtMfgbatchNo.Name = "txtMfgbatchNo";
            this.txtMfgbatchNo.ReadOnly = true;
            this.txtMfgbatchNo.Size = new System.Drawing.Size(127, 23);
            this.txtMfgbatchNo.TabIndex = 13;
            // 
            // txtMfgDate
            // 
            this.txtMfgDate.Location = new System.Drawing.Point(116, 70);
            this.txtMfgDate.Name = "txtMfgDate";
            this.txtMfgDate.ReadOnly = true;
            this.txtMfgDate.Size = new System.Drawing.Size(127, 23);
            this.txtMfgDate.TabIndex = 10;
            // 
            // txtBatchNo
            // 
            this.txtBatchNo.Location = new System.Drawing.Point(116, 8);
            this.txtBatchNo.Name = "txtBatchNo";
            this.txtBatchNo.Size = new System.Drawing.Size(127, 23);
            this.txtBatchNo.TabIndex = 1;
            this.txtBatchNo.Validating += new System.ComponentModel.CancelEventHandler(this.txtBatchNo_Validating);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(272, 78);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(91, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Expiry Date :";
            // 
            // lblMfgDate
            // 
            this.lblMfgDate.AutoSize = true;
            this.lblMfgDate.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMfgDate.Location = new System.Drawing.Point(38, 73);
            this.lblMfgDate.Name = "lblMfgDate";
            this.lblMfgDate.Size = new System.Drawing.Size(72, 13);
            this.lblMfgDate.TabIndex = 6;
            this.lblMfgDate.Text = "Mfg Date :";
            // 
            // lblMRP
            // 
            this.lblMRP.AutoSize = true;
            this.lblMRP.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMRP.Location = new System.Drawing.Point(69, 44);
            this.lblMRP.Name = "lblMRP";
            this.lblMRP.Size = new System.Drawing.Size(41, 13);
            this.lblMRP.TabIndex = 5;
            this.lblMRP.Text = "MRP :";
            // 
            // lblmfgBatchNo
            // 
            this.lblmfgBatchNo.AutoSize = true;
            this.lblmfgBatchNo.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblmfgBatchNo.Location = new System.Drawing.Point(264, 46);
            this.lblmfgBatchNo.Name = "lblmfgBatchNo";
            this.lblmfgBatchNo.Size = new System.Drawing.Size(99, 13);
            this.lblmfgBatchNo.TabIndex = 4;
            this.lblmfgBatchNo.Text = "Mfg Batch No :";
            // 
            // lblBatchNo
            // 
            this.lblBatchNo.AutoSize = true;
            this.lblBatchNo.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBatchNo.Location = new System.Drawing.Point(35, 11);
            this.lblBatchNo.Name = "lblBatchNo";
            this.lblBatchNo.Size = new System.Drawing.Size(80, 13);
            this.lblBatchNo.TabIndex = 3;
            this.lblBatchNo.Text = "Batch No :*";
            // 
            // lblInvoiceQty
            // 
            this.lblInvoiceQty.AutoSize = true;
            this.lblInvoiceQty.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInvoiceQty.Location = new System.Drawing.Point(505, 14);
            this.lblInvoiceQty.Name = "lblInvoiceQty";
            this.lblInvoiceQty.Size = new System.Drawing.Size(98, 13);
            this.lblInvoiceQty.TabIndex = 2;
            this.lblInvoiceQty.Text = "Invoice Qty :*";
            // 
            // dgvInvoiceDetail
            // 
            this.dgvInvoiceDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInvoiceDetail.Location = new System.Drawing.Point(0, 3);
            this.dgvInvoiceDetail.Name = "dgvInvoiceDetail";
            this.dgvInvoiceDetail.RowHeadersVisible = false;
            this.dgvInvoiceDetail.Size = new System.Drawing.Size(861, 162);
            this.dgvInvoiceDetail.TabIndex = 0;
            this.dgvInvoiceDetail.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvInvoiceDetail_CellClick);
            this.dgvInvoiceDetail.SelectionChanged += new System.EventHandler(this.dgvInvoiceDetail_SelectionChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.dgvInvoiceDetail);
            this.panel1.Location = new System.Drawing.Point(0, 332);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(864, 254);
            this.panel1.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.btnClearAll);
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Controls.Add(this.btnInvoice);
            this.panel2.Location = new System.Drawing.Point(0, 171);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(861, 80);
            this.panel2.TabIndex = 3;
            // 
            // btnClearAll
            // 
            this.btnClearAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btnClearAll.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearAll.BackgroundImage")));
            this.btnClearAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearAll.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClearAll.Location = new System.Drawing.Point(690, 8);
            this.btnClearAll.Name = "btnClearAll";
            this.btnClearAll.Size = new System.Drawing.Size(72, 62);
            this.btnClearAll.TabIndex = 2;
            this.btnClearAll.Text = "Clear All";
            this.btnClearAll.UseVisualStyleBackColor = false;
            this.btnClearAll.Click += new System.EventHandler(this.btnClearAll_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btnCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.BackgroundImage")));
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.Location = new System.Drawing.Point(778, 8);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 62);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Exit";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnInvoice
            // 
            this.btnInvoice.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(219)))), ((int)(((byte)(192)))));
            this.btnInvoice.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnInvoice.BackgroundImage")));
            this.btnInvoice.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnInvoice.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnInvoice.Location = new System.Drawing.Point(595, 8);
            this.btnInvoice.Name = "btnInvoice";
            this.btnInvoice.Size = new System.Drawing.Size(72, 62);
            this.btnInvoice.TabIndex = 0;
            this.btnInvoice.Text = "Invoice";
            this.btnInvoice.UseVisualStyleBackColor = false;
            this.btnInvoice.Click += new System.EventHandler(this.btnInvoice_Click);
            // 
            // errorAdd
            // 
            this.errorAdd.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorAdd.ContainerControl = this;
            // 
            // errorInvoice
            // 
            this.errorInvoice.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorInvoice.ContainerControl = this;
            // 
            // dgvCOItemDetails
            // 
            this.dgvCOItemDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCOItemDetails.Location = new System.Drawing.Point(0, 80);
            this.dgvCOItemDetails.Name = "dgvCOItemDetails";
            this.dgvCOItemDetails.RowHeadersVisible = false;
            this.dgvCOItemDetails.Size = new System.Drawing.Size(864, 119);
            this.dgvCOItemDetails.TabIndex = 4;
            this.dgvCOItemDetails.SelectionChanged += new System.EventHandler(this.dgvCOItemDetails_SelectionChanged);
            // 
            // frmInvoice
            // 
            this.AcceptButton = this.btnInvoice;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(233)))), ((int)(((byte)(251)))));
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(864, 588);
            this.Controls.Add(this.dgvCOItemDetails);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlOrderDetail);
            this.Controls.Add(this.pnlOrder);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmInvoice";
            this.Text = "Invoice";
            this.TopMost = true;
            this.pnlOrder.ResumeLayout(false);
            this.pnlOrder.PerformLayout();
            this.pnlOrderDetail.ResumeLayout(false);
            this.pnlOrderDetail.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInvoiceDetail)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorAdd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorInvoice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCOItemDetails)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlOrder;
        private System.Windows.Forms.Label lblDistributor;
        private System.Windows.Forms.Label lblOrderDate;
        private System.Windows.Forms.Label lblOrderNo;
        private System.Windows.Forms.Label lblDistributorNameValue;
        private System.Windows.Forms.Label lblOrderDateValue;
        private System.Windows.Forms.Label lblOrderNoValue;
        private System.Windows.Forms.Label lblOrderTypeValue;
        private System.Windows.Forms.Label lblOrderType;
        private System.Windows.Forms.Panel pnlOrderDetail;
        private System.Windows.Forms.DataGridView dgvInvoiceDetail;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnInvoice;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblmfgBatchNo;
        private System.Windows.Forms.Label lblBatchNo;
        private System.Windows.Forms.Label lblInvoiceQty;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblMfgDate;
        private System.Windows.Forms.Label lblMRP;
        private System.Windows.Forms.TextBox txtMRP;
        private System.Windows.Forms.TextBox txtInvoiceQty;
        private System.Windows.Forms.TextBox txtExpiryDate;
        private System.Windows.Forms.TextBox txtMfgbatchNo;
        private System.Windows.Forms.TextBox txtMfgDate;
        private System.Windows.Forms.TextBox txtBatchNo;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox txtAvailableQty;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ErrorProvider errorAdd;
        private System.Windows.Forms.Button btnClearAll;
        private System.Windows.Forms.ErrorProvider errorInvoice;
        private System.Windows.Forms.DataGridView dgvCOItemDetails;
    }
}