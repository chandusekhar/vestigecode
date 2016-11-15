namespace ReturnsComponent.UI
{
    partial class frmVendorReturn
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmVendorReturn));
            this.pnlCreateHeader = new System.Windows.Forms.Panel();
            this.txtTotalItemCost = new System.Windows.Forms.TextBox();
            this.lblTotalItemCost = new System.Windows.Forms.Label();
            this.lblDebitNoteAmount = new System.Windows.Forms.Label();
            this.cmbLocationCode = new System.Windows.Forms.ComboBox();
            this.lblLocationCode = new System.Windows.Forms.Label();
            this.cmbVendorCode = new System.Windows.Forms.ComboBox();
            this.lblVendorCode = new System.Windows.Forms.Label();
            this.txtReturnNumber = new System.Windows.Forms.TextBox();
            this.lblReturnNumber = new System.Windows.Forms.Label();
            this.txtRemarks = new System.Windows.Forms.TextBox();
            this.txtDebitNoteAmount = new System.Windows.Forms.TextBox();
            this.txtDebitNoteNo = new System.Windows.Forms.TextBox();
            this.txtShippingDetails = new System.Windows.Forms.TextBox();
            this.lblRemarks = new System.Windows.Forms.Label();
            this.lblDebitNoteNo = new System.Windows.Forms.Label();
            this.txtCurrentStatus = new System.Windows.Forms.TextBox();
            this.lblCurrentStatus = new System.Windows.Forms.Label();
            this.dtpReturnDate = new System.Windows.Forms.DateTimePicker();
            this.lblReturnDate = new System.Windows.Forms.Label();
            this.txtTotalTaxAmount = new System.Windows.Forms.TextBox();
            this.txtTotalReturnQty = new System.Windows.Forms.TextBox();
            this.lblTotalTaxAmount = new System.Windows.Forms.Label();
            this.lblTotalReturnQty = new System.Windows.Forms.Label();
            this.lblShippingDetails = new System.Windows.Forms.Label();
            this.btnCreateReset = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblAddDetails = new System.Windows.Forms.Label();
            this.grpAddDetails = new System.Windows.Forms.GroupBox();
            this.txtItemTax = new System.Windows.Forms.TextBox();
            this.txtGRNInvoiceQty = new System.Windows.Forms.TextBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.cmbGRNInvoiceNo = new System.Windows.Forms.ComboBox();
            this.cmbGRNInvoiceType = new System.Windows.Forms.ComboBox();
            this.cmbPONumber = new System.Windows.Forms.ComboBox();
            this.pnlCreateDetail = new System.Windows.Forms.Panel();
            this.dgvReturnToVendorItems = new System.Windows.Forms.DataGridView();
            this.lblItemTax = new System.Windows.Forms.Label();
            this.txtReturnReason = new System.Windows.Forms.TextBox();
            this.lblGRNInvoiceQty = new System.Windows.Forms.Label();
            this.POItemQty = new System.Windows.Forms.Label();
            this.txtPOItemQty = new System.Windows.Forms.TextBox();
            this.txtPOItemAmount = new System.Windows.Forms.TextBox();
            this.txtReturnQty = new System.Windows.Forms.TextBox();
            this.lblPOItemAmount = new System.Windows.Forms.Label();
            this.txtItemCode = new System.Windows.Forms.TextBox();
            this.lblGRNInvoiceNo = new System.Windows.Forms.Label();
            this.lblInvoiceGRNType = new System.Windows.Forms.Label();
            this.lblPONumber = new System.Windows.Forms.Label();
            this.lblBucketName = new System.Windows.Forms.Label();
            this.lblAvailableQty = new System.Windows.Forms.Label();
            this.txtBucketName = new System.Windows.Forms.TextBox();
            this.txtAvailableQty = new System.Windows.Forms.TextBox();
            this.lblItemCode = new System.Windows.Forms.Label();
            this.txtPODate = new System.Windows.Forms.TextBox();
            this.txtItemDescription = new System.Windows.Forms.TextBox();
            this.lblItemDescription = new System.Windows.Forms.Label();
            this.lblReturnQty = new System.Windows.Forms.Label();
            this.btnClearDetails = new System.Windows.Forms.Button();
            this.btnAddDetails = new System.Windows.Forms.Button();
            this.lblPODate = new System.Windows.Forms.Label();
            this.lblReturnReason = new System.Windows.Forms.Label();
            this.tabSearch = new System.Windows.Forms.TabPage();
            this.pnlSearchHeader = new System.Windows.Forms.Panel();
            this.cmbSearchStatus = new System.Windows.Forms.ComboBox();
            this.lblSerachStatus = new System.Windows.Forms.Label();
            this.cmbSearchLocationCode = new System.Windows.Forms.ComboBox();
            this.lblSearchLocationCode = new System.Windows.Forms.Label();
            this.cmbSearchVendorCode = new System.Windows.Forms.ComboBox();
            this.lblSearchVendorCode = new System.Windows.Forms.Label();
            this.txtSearchDebitNo = new System.Windows.Forms.TextBox();
            this.lblSearchDebitNoteNo = new System.Windows.Forms.Label();
            this.dtpSearchTO = new System.Windows.Forms.DateTimePicker();
            this.dtpSearchFrom = new System.Windows.Forms.DateTimePicker();
            this.lblSearchToDate = new System.Windows.Forms.Label();
            this.lblSearchFromDate = new System.Windows.Forms.Label();
            this.txtSearchVenRetNumber = new System.Windows.Forms.TextBox();
            this.lblSearchVenRetNumber = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnSearchReset = new System.Windows.Forms.Button();
            this.lblSearchResult = new System.Windows.Forms.Label();
            this.dgvSearchReturnToVendor = new System.Windows.Forms.DataGridView();
            this.tabCreate = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnRTVCancel = new System.Windows.Forms.Button();
            this.btnPrintDebitNote = new System.Windows.Forms.Button();
            this.btnApprove = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnShip = new System.Windows.Forms.Button();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.tabControlTransaction = new System.Windows.Forms.TabControl();
            this.errSearch = new System.Windows.Forms.ErrorProvider(this.components);
            this.errItem = new System.Windows.Forms.ErrorProvider(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblAppUser = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.lblPageTitle = new System.Windows.Forms.Label();
            this.pnlCreateHeader.SuspendLayout();
            this.grpAddDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.pnlCreateDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReturnToVendorItems)).BeginInit();
            this.tabSearch.SuspendLayout();
            this.pnlSearchHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearchReturnToVendor)).BeginInit();
            this.tabCreate.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabControlTransaction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errSearch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errItem)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCreateHeader
            // 
            this.pnlCreateHeader.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlCreateHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(233)))), ((int)(((byte)(251)))));
            this.pnlCreateHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlCreateHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCreateHeader.Controls.Add(this.txtTotalItemCost);
            this.pnlCreateHeader.Controls.Add(this.lblTotalItemCost);
            this.pnlCreateHeader.Controls.Add(this.lblDebitNoteAmount);
            this.pnlCreateHeader.Controls.Add(this.cmbLocationCode);
            this.pnlCreateHeader.Controls.Add(this.lblLocationCode);
            this.pnlCreateHeader.Controls.Add(this.cmbVendorCode);
            this.pnlCreateHeader.Controls.Add(this.lblVendorCode);
            this.pnlCreateHeader.Controls.Add(this.txtReturnNumber);
            this.pnlCreateHeader.Controls.Add(this.lblReturnNumber);
            this.pnlCreateHeader.Controls.Add(this.txtRemarks);
            this.pnlCreateHeader.Controls.Add(this.txtDebitNoteAmount);
            this.pnlCreateHeader.Controls.Add(this.txtDebitNoteNo);
            this.pnlCreateHeader.Controls.Add(this.txtShippingDetails);
            this.pnlCreateHeader.Controls.Add(this.lblRemarks);
            this.pnlCreateHeader.Controls.Add(this.lblDebitNoteNo);
            this.pnlCreateHeader.Controls.Add(this.txtCurrentStatus);
            this.pnlCreateHeader.Controls.Add(this.lblCurrentStatus);
            this.pnlCreateHeader.Controls.Add(this.dtpReturnDate);
            this.pnlCreateHeader.Controls.Add(this.lblReturnDate);
            this.pnlCreateHeader.Controls.Add(this.txtTotalTaxAmount);
            this.pnlCreateHeader.Controls.Add(this.txtTotalReturnQty);
            this.pnlCreateHeader.Controls.Add(this.lblTotalTaxAmount);
            this.pnlCreateHeader.Controls.Add(this.lblTotalReturnQty);
            this.pnlCreateHeader.Controls.Add(this.lblShippingDetails);
            this.pnlCreateHeader.Location = new System.Drawing.Point(2, 4);
            this.pnlCreateHeader.Name = "pnlCreateHeader";
            this.pnlCreateHeader.Size = new System.Drawing.Size(999, 117);
            this.pnlCreateHeader.TabIndex = 0;
            // 
            // txtTotalItemCost
            // 
            this.txtTotalItemCost.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.txtTotalItemCost.Location = new System.Drawing.Point(347, 69);
            this.txtTotalItemCost.Multiline = true;
            this.txtTotalItemCost.Name = "txtTotalItemCost";
            this.txtTotalItemCost.ReadOnly = true;
            this.txtTotalItemCost.Size = new System.Drawing.Size(111, 21);
            this.txtTotalItemCost.TabIndex = 189;
            this.txtTotalItemCost.TabStop = false;
            this.txtTotalItemCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblTotalItemCost
            // 
            this.lblTotalItemCost.AutoSize = true;
            this.lblTotalItemCost.Location = new System.Drawing.Point(229, 72);
            this.lblTotalItemCost.Name = "lblTotalItemCost";
            this.lblTotalItemCost.Size = new System.Drawing.Size(119, 13);
            this.lblTotalItemCost.TabIndex = 190;
            this.lblTotalItemCost.Text = "Total Item Amount:";
            // 
            // lblDebitNoteAmount
            // 
            this.lblDebitNoteAmount.AutoSize = true;
            this.lblDebitNoteAmount.Location = new System.Drawing.Point(692, 72);
            this.lblDebitNoteAmount.Name = "lblDebitNoteAmount";
            this.lblDebitNoteAmount.Size = new System.Drawing.Size(120, 13);
            this.lblDebitNoteAmount.TabIndex = 188;
            this.lblDebitNoteAmount.Text = "Debit Note Amount:";
            // 
            // cmbLocationCode
            // 
            this.cmbLocationCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocationCode.FormattingEnabled = true;
            this.cmbLocationCode.Location = new System.Drawing.Point(116, 13);
            this.cmbLocationCode.Name = "cmbLocationCode";
            this.cmbLocationCode.Size = new System.Drawing.Size(111, 21);
            this.cmbLocationCode.TabIndex = 0;
            this.cmbLocationCode.SelectedIndexChanged += new System.EventHandler(this.cmbLocationCode_SelectedIndexChanged);
            // 
            // lblLocationCode
            // 
            this.lblLocationCode.AutoSize = true;
            this.lblLocationCode.Location = new System.Drawing.Point(12, 15);
            this.lblLocationCode.Name = "lblLocationCode";
            this.lblLocationCode.Size = new System.Drawing.Size(104, 13);
            this.lblLocationCode.TabIndex = 186;
            this.lblLocationCode.Text = "Location  Code:*";
            // 
            // cmbVendorCode
            // 
            this.cmbVendorCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbVendorCode.FormattingEnabled = true;
            this.cmbVendorCode.Location = new System.Drawing.Point(347, 14);
            this.cmbVendorCode.Name = "cmbVendorCode";
            this.cmbVendorCode.Size = new System.Drawing.Size(111, 21);
            this.cmbVendorCode.TabIndex = 1;
            // 
            // lblVendorCode
            // 
            this.lblVendorCode.AutoSize = true;
            this.lblVendorCode.Location = new System.Drawing.Point(255, 17);
            this.lblVendorCode.Name = "lblVendorCode";
            this.lblVendorCode.Size = new System.Drawing.Size(94, 13);
            this.lblVendorCode.TabIndex = 182;
            this.lblVendorCode.Text = "Vendor Code:*";
            // 
            // txtReturnNumber
            // 
            this.txtReturnNumber.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.txtReturnNumber.Location = new System.Drawing.Point(814, 13);
            this.txtReturnNumber.Name = "txtReturnNumber";
            this.txtReturnNumber.ReadOnly = true;
            this.txtReturnNumber.Size = new System.Drawing.Size(139, 21);
            this.txtReturnNumber.TabIndex = 3;
            this.txtReturnNumber.TabStop = false;
            // 
            // lblReturnNumber
            // 
            this.lblReturnNumber.AutoSize = true;
            this.lblReturnNumber.Location = new System.Drawing.Point(713, 15);
            this.lblReturnNumber.Name = "lblReturnNumber";
            this.lblReturnNumber.Size = new System.Drawing.Size(99, 13);
            this.lblReturnNumber.TabIndex = 181;
            this.lblReturnNumber.Text = "Return Number:";
            // 
            // txtRemarks
            // 
            this.txtRemarks.Location = new System.Drawing.Point(814, 40);
            this.txtRemarks.MaxLength = 100;
            this.txtRemarks.Multiline = true;
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Size = new System.Drawing.Size(139, 21);
            this.txtRemarks.TabIndex = 3;
            // 
            // txtDebitNoteAmount
            // 
            this.txtDebitNoteAmount.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.txtDebitNoteAmount.Location = new System.Drawing.Point(814, 69);
            this.txtDebitNoteAmount.Multiline = true;
            this.txtDebitNoteAmount.Name = "txtDebitNoteAmount";
            this.txtDebitNoteAmount.ReadOnly = true;
            this.txtDebitNoteAmount.Size = new System.Drawing.Size(140, 21);
            this.txtDebitNoteAmount.TabIndex = 179;
            this.txtDebitNoteAmount.TabStop = false;
            this.txtDebitNoteAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtDebitNoteNo
            // 
            this.txtDebitNoteNo.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.txtDebitNoteNo.Location = new System.Drawing.Point(116, 69);
            this.txtDebitNoteNo.Name = "txtDebitNoteNo";
            this.txtDebitNoteNo.ReadOnly = true;
            this.txtDebitNoteNo.Size = new System.Drawing.Size(111, 21);
            this.txtDebitNoteNo.TabIndex = 165;
            this.txtDebitNoteNo.TabStop = false;
            // 
            // txtShippingDetails
            // 
            this.txtShippingDetails.Location = new System.Drawing.Point(575, 39);
            this.txtShippingDetails.MaxLength = 100;
            this.txtShippingDetails.Name = "txtShippingDetails";
            this.txtShippingDetails.Size = new System.Drawing.Size(111, 21);
            this.txtShippingDetails.TabIndex = 5;
            // 
            // lblRemarks
            // 
            this.lblRemarks.AutoSize = true;
            this.lblRemarks.Location = new System.Drawing.Point(749, 42);
            this.lblRemarks.Name = "lblRemarks";
            this.lblRemarks.Size = new System.Drawing.Size(63, 13);
            this.lblRemarks.TabIndex = 175;
            this.lblRemarks.Text = "Remarks:";
            // 
            // lblDebitNoteNo
            // 
            this.lblDebitNoteNo.AutoSize = true;
            this.lblDebitNoteNo.Location = new System.Drawing.Point(19, 72);
            this.lblDebitNoteNo.Name = "lblDebitNoteNo";
            this.lblDebitNoteNo.Size = new System.Drawing.Size(91, 13);
            this.lblDebitNoteNo.TabIndex = 178;
            this.lblDebitNoteNo.Text = "Debit Note No:";
            // 
            // txtCurrentStatus
            // 
            this.txtCurrentStatus.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.txtCurrentStatus.Location = new System.Drawing.Point(116, 40);
            this.txtCurrentStatus.Name = "txtCurrentStatus";
            this.txtCurrentStatus.ReadOnly = true;
            this.txtCurrentStatus.Size = new System.Drawing.Size(111, 21);
            this.txtCurrentStatus.TabIndex = 168;
            this.txtCurrentStatus.TabStop = false;
            // 
            // lblCurrentStatus
            // 
            this.lblCurrentStatus.AutoSize = true;
            this.lblCurrentStatus.Location = new System.Drawing.Point(16, 42);
            this.lblCurrentStatus.Name = "lblCurrentStatus";
            this.lblCurrentStatus.Size = new System.Drawing.Size(100, 13);
            this.lblCurrentStatus.TabIndex = 177;
            this.lblCurrentStatus.Text = "Current Status: ";
            // 
            // dtpReturnDate
            // 
            this.dtpReturnDate.Checked = false;
            this.dtpReturnDate.CustomFormat = "dd-MM-yyyy";
            this.dtpReturnDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpReturnDate.Location = new System.Drawing.Point(575, 11);
            this.dtpReturnDate.Name = "dtpReturnDate";
            this.dtpReturnDate.ShowCheckBox = true;
            this.dtpReturnDate.Size = new System.Drawing.Size(111, 21);
            this.dtpReturnDate.TabIndex = 2;
            this.dtpReturnDate.Value = new System.DateTime(2009, 7, 14, 0, 0, 0, 0);
            // 
            // lblReturnDate
            // 
            this.lblReturnDate.AutoSize = true;
            this.lblReturnDate.Location = new System.Drawing.Point(485, 17);
            this.lblReturnDate.Name = "lblReturnDate";
            this.lblReturnDate.Size = new System.Drawing.Size(88, 13);
            this.lblReturnDate.TabIndex = 174;
            this.lblReturnDate.Text = "Return Date:*";
            // 
            // txtTotalTaxAmount
            // 
            this.txtTotalTaxAmount.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.txtTotalTaxAmount.Location = new System.Drawing.Point(575, 69);
            this.txtTotalTaxAmount.Multiline = true;
            this.txtTotalTaxAmount.Name = "txtTotalTaxAmount";
            this.txtTotalTaxAmount.ReadOnly = true;
            this.txtTotalTaxAmount.Size = new System.Drawing.Size(111, 21);
            this.txtTotalTaxAmount.TabIndex = 167;
            this.txtTotalTaxAmount.TabStop = false;
            this.txtTotalTaxAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtTotalReturnQty
            // 
            this.txtTotalReturnQty.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.txtTotalReturnQty.Location = new System.Drawing.Point(347, 41);
            this.txtTotalReturnQty.Multiline = true;
            this.txtTotalReturnQty.Name = "txtTotalReturnQty";
            this.txtTotalReturnQty.ReadOnly = true;
            this.txtTotalReturnQty.Size = new System.Drawing.Size(111, 21);
            this.txtTotalReturnQty.TabIndex = 166;
            this.txtTotalReturnQty.TabStop = false;
            this.txtTotalReturnQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblTotalTaxAmount
            // 
            this.lblTotalTaxAmount.AutoSize = true;
            this.lblTotalTaxAmount.Location = new System.Drawing.Point(463, 72);
            this.lblTotalTaxAmount.Name = "lblTotalTaxAmount";
            this.lblTotalTaxAmount.Size = new System.Drawing.Size(113, 13);
            this.lblTotalTaxAmount.TabIndex = 173;
            this.lblTotalTaxAmount.Text = "Total Tax Amount:";
            // 
            // lblTotalReturnQty
            // 
            this.lblTotalReturnQty.AutoSize = true;
            this.lblTotalReturnQty.Location = new System.Drawing.Point(242, 43);
            this.lblTotalReturnQty.Name = "lblTotalReturnQty";
            this.lblTotalReturnQty.Size = new System.Drawing.Size(106, 13);
            this.lblTotalReturnQty.TabIndex = 172;
            this.lblTotalReturnQty.Text = "Total Return Qty:";
            // 
            // lblShippingDetails
            // 
            this.lblShippingDetails.AutoSize = true;
            this.lblShippingDetails.Location = new System.Drawing.Point(469, 44);
            this.lblShippingDetails.Name = "lblShippingDetails";
            this.lblShippingDetails.Size = new System.Drawing.Size(104, 13);
            this.lblShippingDetails.TabIndex = 176;
            this.lblShippingDetails.Text = "Shipping Details:";
            // 
            // btnCreateReset
            // 
            this.btnCreateReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreateReset.BackColor = System.Drawing.Color.Transparent;
            this.btnCreateReset.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCreateReset.BackgroundImage")));
            this.btnCreateReset.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCreateReset.FlatAppearance.BorderSize = 0;
            this.btnCreateReset.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnCreateReset.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnCreateReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateReset.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnCreateReset.Location = new System.Drawing.Point(683, 2);
            this.btnCreateReset.Name = "btnCreateReset";
            this.btnCreateReset.Size = new System.Drawing.Size(75, 32);
            this.btnCreateReset.TabIndex = 5;
            this.btnCreateReset.Text = "&Reset";
            this.btnCreateReset.UseVisualStyleBackColor = false;
            this.btnCreateReset.Click += new System.EventHandler(this.btnCreateReset_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSave.BackgroundImage")));
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnSave.Location = new System.Drawing.Point(602, 2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 32);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "&Create";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblAddDetails
            // 
            this.lblAddDetails.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAddDetails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(117)))), ((int)(((byte)(186)))));
            this.lblAddDetails.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblAddDetails.ForeColor = System.Drawing.Color.White;
            this.lblAddDetails.Location = new System.Drawing.Point(2, 119);
            this.lblAddDetails.Name = "lblAddDetails";
            this.lblAddDetails.Size = new System.Drawing.Size(1002, 23);
            this.lblAddDetails.TabIndex = 5;
            this.lblAddDetails.Text = "Item Details";
            this.lblAddDetails.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // grpAddDetails
            // 
            this.grpAddDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpAddDetails.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.grpAddDetails.Controls.Add(this.txtItemTax);
            this.grpAddDetails.Controls.Add(this.txtGRNInvoiceQty);
            this.grpAddDetails.Controls.Add(this.pictureBox3);
            this.grpAddDetails.Controls.Add(this.cmbGRNInvoiceNo);
            this.grpAddDetails.Controls.Add(this.cmbGRNInvoiceType);
            this.grpAddDetails.Controls.Add(this.cmbPONumber);
            this.grpAddDetails.Controls.Add(this.pnlCreateDetail);
            this.grpAddDetails.Controls.Add(this.lblItemTax);
            this.grpAddDetails.Controls.Add(this.txtReturnReason);
            this.grpAddDetails.Controls.Add(this.lblGRNInvoiceQty);
            this.grpAddDetails.Controls.Add(this.POItemQty);
            this.grpAddDetails.Controls.Add(this.txtPOItemQty);
            this.grpAddDetails.Controls.Add(this.txtPOItemAmount);
            this.grpAddDetails.Controls.Add(this.txtReturnQty);
            this.grpAddDetails.Controls.Add(this.lblPOItemAmount);
            this.grpAddDetails.Controls.Add(this.txtItemCode);
            this.grpAddDetails.Controls.Add(this.lblGRNInvoiceNo);
            this.grpAddDetails.Controls.Add(this.lblInvoiceGRNType);
            this.grpAddDetails.Controls.Add(this.lblPONumber);
            this.grpAddDetails.Controls.Add(this.lblBucketName);
            this.grpAddDetails.Controls.Add(this.lblAvailableQty);
            this.grpAddDetails.Controls.Add(this.txtBucketName);
            this.grpAddDetails.Controls.Add(this.txtAvailableQty);
            this.grpAddDetails.Controls.Add(this.lblItemCode);
            this.grpAddDetails.Controls.Add(this.txtPODate);
            this.grpAddDetails.Controls.Add(this.txtItemDescription);
            this.grpAddDetails.Controls.Add(this.lblItemDescription);
            this.grpAddDetails.Controls.Add(this.lblReturnQty);
            this.grpAddDetails.Controls.Add(this.btnClearDetails);
            this.grpAddDetails.Controls.Add(this.btnAddDetails);
            this.grpAddDetails.Controls.Add(this.lblPODate);
            this.grpAddDetails.Controls.Add(this.lblReturnReason);
            this.grpAddDetails.Location = new System.Drawing.Point(10, 141);
            this.grpAddDetails.Name = "grpAddDetails";
            this.grpAddDetails.Size = new System.Drawing.Size(983, 451);
            this.grpAddDetails.TabIndex = 0;
            this.grpAddDetails.TabStop = false;
            // 
            // txtItemTax
            // 
            this.txtItemTax.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.txtItemTax.Location = new System.Drawing.Point(129, 107);
            this.txtItemTax.Name = "txtItemTax";
            this.txtItemTax.ReadOnly = true;
            this.txtItemTax.Size = new System.Drawing.Size(111, 21);
            this.txtItemTax.TabIndex = 218;
            this.txtItemTax.TabStop = false;
            this.txtItemTax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtGRNInvoiceQty
            // 
            this.txtGRNInvoiceQty.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.txtGRNInvoiceQty.Location = new System.Drawing.Point(627, 77);
            this.txtGRNInvoiceQty.Name = "txtGRNInvoiceQty";
            this.txtGRNInvoiceQty.ReadOnly = true;
            this.txtGRNInvoiceQty.Size = new System.Drawing.Size(111, 21);
            this.txtGRNInvoiceQty.TabIndex = 218;
            this.txtGRNInvoiceQty.TabStop = false;
            this.txtGRNInvoiceQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::ReturnsComponent.Properties.Resources.find;
            this.pictureBox3.Location = new System.Drawing.Point(241, 14);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(21, 21);
            this.pictureBox3.TabIndex = 223;
            this.pictureBox3.TabStop = false;
            // 
            // cmbGRNInvoiceNo
            // 
            this.cmbGRNInvoiceNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGRNInvoiceNo.FormattingEnabled = true;
            this.cmbGRNInvoiceNo.Location = new System.Drawing.Point(370, 77);
            this.cmbGRNInvoiceNo.Name = "cmbGRNInvoiceNo";
            this.cmbGRNInvoiceNo.Size = new System.Drawing.Size(168, 21);
            this.cmbGRNInvoiceNo.TabIndex = 3;
            this.cmbGRNInvoiceNo.SelectedIndexChanged += new System.EventHandler(this.cmbGRNInvoiceNo_SelectedIndexChanged);
            // 
            // cmbGRNInvoiceType
            // 
            this.cmbGRNInvoiceType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGRNInvoiceType.FormattingEnabled = true;
            this.cmbGRNInvoiceType.Location = new System.Drawing.Point(129, 76);
            this.cmbGRNInvoiceType.Name = "cmbGRNInvoiceType";
            this.cmbGRNInvoiceType.Size = new System.Drawing.Size(112, 21);
            this.cmbGRNInvoiceType.TabIndex = 2;
            this.cmbGRNInvoiceType.SelectedIndexChanged += new System.EventHandler(this.cmbGRNInvoiceType_SelectedIndexChanged);
            // 
            // cmbPONumber
            // 
            this.cmbPONumber.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPONumber.FormattingEnabled = true;
            this.cmbPONumber.Location = new System.Drawing.Point(129, 45);
            this.cmbPONumber.Name = "cmbPONumber";
            this.cmbPONumber.Size = new System.Drawing.Size(111, 21);
            this.cmbPONumber.TabIndex = 1;
            this.cmbPONumber.SelectedIndexChanged += new System.EventHandler(this.cmbPONumber_SelectedIndexChanged);
            // 
            // pnlCreateDetail
            // 
            this.pnlCreateDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlCreateDetail.BackColor = System.Drawing.Color.Transparent;
            this.pnlCreateDetail.Controls.Add(this.dgvReturnToVendorItems);
            this.pnlCreateDetail.Location = new System.Drawing.Point(3, 152);
            this.pnlCreateDetail.Name = "pnlCreateDetail";
            this.pnlCreateDetail.Size = new System.Drawing.Size(977, 244);
            this.pnlCreateDetail.TabIndex = 222;
            // 
            // dgvReturnToVendorItems
            // 
            this.dgvReturnToVendorItems.AllowUserToAddRows = false;
            this.dgvReturnToVendorItems.AllowUserToDeleteRows = false;
            this.dgvReturnToVendorItems.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dgvReturnToVendorItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReturnToVendorItems.Location = new System.Drawing.Point(4, 7);
            this.dgvReturnToVendorItems.MultiSelect = false;
            this.dgvReturnToVendorItems.Name = "dgvReturnToVendorItems";
            this.dgvReturnToVendorItems.ReadOnly = true;
            this.dgvReturnToVendorItems.RowHeadersVisible = false;
            this.dgvReturnToVendorItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvReturnToVendorItems.Size = new System.Drawing.Size(968, 230);
            this.dgvReturnToVendorItems.TabIndex = 201;
            this.dgvReturnToVendorItems.TabStop = false;
            this.dgvReturnToVendorItems.SelectionChanged += new System.EventHandler(this.dgvReturnToVendorItems_SelectionChanged);
            this.dgvReturnToVendorItems.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvReturnToVendorItems_CellContentClick);
            // 
            // lblItemTax
            // 
            this.lblItemTax.AutoSize = true;
            this.lblItemTax.BackColor = System.Drawing.Color.Transparent;
            this.lblItemTax.Location = new System.Drawing.Point(63, 110);
            this.lblItemTax.Name = "lblItemTax";
            this.lblItemTax.Size = new System.Drawing.Size(64, 13);
            this.lblItemTax.TabIndex = 219;
            this.lblItemTax.Text = "Item Tax:";
            // 
            // txtReturnReason
            // 
            this.txtReturnReason.Location = new System.Drawing.Point(370, 107);
            this.txtReturnReason.MaxLength = 100;
            this.txtReturnReason.Multiline = true;
            this.txtReturnReason.Name = "txtReturnReason";
            this.txtReturnReason.Size = new System.Drawing.Size(111, 21);
            this.txtReturnReason.TabIndex = 5;
            // 
            // lblGRNInvoiceQty
            // 
            this.lblGRNInvoiceQty.BackColor = System.Drawing.Color.Transparent;
            this.lblGRNInvoiceQty.Location = new System.Drawing.Point(545, 72);
            this.lblGRNInvoiceQty.Name = "lblGRNInvoiceQty";
            this.lblGRNInvoiceQty.Size = new System.Drawing.Size(80, 26);
            this.lblGRNInvoiceQty.TabIndex = 219;
            this.lblGRNInvoiceQty.Text = "GRN Batch / Inv. Qty.:";
            this.lblGRNInvoiceQty.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // POItemQty
            // 
            this.POItemQty.AutoSize = true;
            this.POItemQty.BackColor = System.Drawing.Color.Transparent;
            this.POItemQty.Location = new System.Drawing.Point(538, 49);
            this.POItemQty.Name = "POItemQty";
            this.POItemQty.Size = new System.Drawing.Size(87, 13);
            this.POItemQty.TabIndex = 219;
            this.POItemQty.Text = "PO Item Qty.:";
            // 
            // txtPOItemQty
            // 
            this.txtPOItemQty.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.txtPOItemQty.Location = new System.Drawing.Point(627, 46);
            this.txtPOItemQty.Name = "txtPOItemQty";
            this.txtPOItemQty.ReadOnly = true;
            this.txtPOItemQty.Size = new System.Drawing.Size(111, 21);
            this.txtPOItemQty.TabIndex = 218;
            this.txtPOItemQty.TabStop = false;
            this.txtPOItemQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtPOItemAmount
            // 
            this.txtPOItemAmount.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.txtPOItemAmount.Location = new System.Drawing.Point(844, 45);
            this.txtPOItemAmount.Name = "txtPOItemAmount";
            this.txtPOItemAmount.ReadOnly = true;
            this.txtPOItemAmount.Size = new System.Drawing.Size(111, 21);
            this.txtPOItemAmount.TabIndex = 209;
            this.txtPOItemAmount.TabStop = false;
            this.txtPOItemAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtReturnQty
            // 
            this.txtReturnQty.Location = new System.Drawing.Point(844, 76);
            this.txtReturnQty.MaxLength = 10;
            this.txtReturnQty.Name = "txtReturnQty";
            this.txtReturnQty.Size = new System.Drawing.Size(111, 21);
            this.txtReturnQty.TabIndex = 4;
            this.txtReturnQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtReturnQty.Validating += new System.ComponentModel.CancelEventHandler(this.txtReturnQty_Validating);
            // 
            // lblPOItemAmount
            // 
            this.lblPOItemAmount.AutoSize = true;
            this.lblPOItemAmount.BackColor = System.Drawing.Color.Transparent;
            this.lblPOItemAmount.Location = new System.Drawing.Point(742, 49);
            this.lblPOItemAmount.Name = "lblPOItemAmount";
            this.lblPOItemAmount.Size = new System.Drawing.Size(107, 13);
            this.lblPOItemAmount.TabIndex = 217;
            this.lblPOItemAmount.Text = "PO Item Amount:";
            // 
            // txtItemCode
            // 
            this.txtItemCode.Location = new System.Drawing.Point(129, 13);
            this.txtItemCode.MaxLength = 20;
            this.txtItemCode.Name = "txtItemCode";
            this.txtItemCode.Size = new System.Drawing.Size(111, 21);
            this.txtItemCode.TabIndex = 0;
            this.txtItemCode.TextChanged += new System.EventHandler(this.txtItemCode_TextChanged);
            this.txtItemCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtItemCode_KeyDown);
            this.txtItemCode.Validating += new System.ComponentModel.CancelEventHandler(this.txtItemCode_Validating);
            // 
            // lblGRNInvoiceNo
            // 
            this.lblGRNInvoiceNo.AutoSize = true;
            this.lblGRNInvoiceNo.BackColor = System.Drawing.Color.Transparent;
            this.lblGRNInvoiceNo.Location = new System.Drawing.Point(262, 80);
            this.lblGRNInvoiceNo.Name = "lblGRNInvoiceNo";
            this.lblGRNInvoiceNo.Size = new System.Drawing.Size(110, 13);
            this.lblGRNInvoiceNo.TabIndex = 215;
            this.lblGRNInvoiceNo.Text = "GRN/Invoice No:*";
            // 
            // lblInvoiceGRNType
            // 
            this.lblInvoiceGRNType.AutoSize = true;
            this.lblInvoiceGRNType.BackColor = System.Drawing.Color.Transparent;
            this.lblInvoiceGRNType.Location = new System.Drawing.Point(0, 79);
            this.lblInvoiceGRNType.Name = "lblInvoiceGRNType";
            this.lblInvoiceGRNType.Size = new System.Drawing.Size(133, 13);
            this.lblInvoiceGRNType.TabIndex = 215;
            this.lblInvoiceGRNType.Text = "Type (GRN/Invoice):*";
            // 
            // lblPONumber
            // 
            this.lblPONumber.AutoSize = true;
            this.lblPONumber.BackColor = System.Drawing.Color.Transparent;
            this.lblPONumber.Location = new System.Drawing.Point(43, 49);
            this.lblPONumber.Name = "lblPONumber";
            this.lblPONumber.Size = new System.Drawing.Size(84, 13);
            this.lblPONumber.TabIndex = 215;
            this.lblPONumber.Text = "PO Number:*";
            // 
            // lblBucketName
            // 
            this.lblBucketName.AutoSize = true;
            this.lblBucketName.BackColor = System.Drawing.Color.Transparent;
            this.lblBucketName.Location = new System.Drawing.Point(538, 17);
            this.lblBucketName.Name = "lblBucketName";
            this.lblBucketName.Size = new System.Drawing.Size(88, 13);
            this.lblBucketName.TabIndex = 213;
            this.lblBucketName.Text = "Bucket Name:";
            // 
            // lblAvailableQty
            // 
            this.lblAvailableQty.AutoSize = true;
            this.lblAvailableQty.BackColor = System.Drawing.Color.Transparent;
            this.lblAvailableQty.Location = new System.Drawing.Point(752, 17);
            this.lblAvailableQty.Name = "lblAvailableQty";
            this.lblAvailableQty.Size = new System.Drawing.Size(92, 13);
            this.lblAvailableQty.TabIndex = 212;
            this.lblAvailableQty.Text = "Available Qty.:";
            // 
            // txtBucketName
            // 
            this.txtBucketName.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.txtBucketName.Location = new System.Drawing.Point(627, 13);
            this.txtBucketName.Name = "txtBucketName";
            this.txtBucketName.ReadOnly = true;
            this.txtBucketName.Size = new System.Drawing.Size(111, 21);
            this.txtBucketName.TabIndex = 207;
            this.txtBucketName.TabStop = false;
            // 
            // txtAvailableQty
            // 
            this.txtAvailableQty.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.txtAvailableQty.Location = new System.Drawing.Point(844, 13);
            this.txtAvailableQty.Name = "txtAvailableQty";
            this.txtAvailableQty.ReadOnly = true;
            this.txtAvailableQty.Size = new System.Drawing.Size(111, 21);
            this.txtAvailableQty.TabIndex = 208;
            this.txtAvailableQty.TabStop = false;
            this.txtAvailableQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblItemCode
            // 
            this.lblItemCode.AutoSize = true;
            this.lblItemCode.BackColor = System.Drawing.Color.Transparent;
            this.lblItemCode.Location = new System.Drawing.Point(43, 17);
            this.lblItemCode.Name = "lblItemCode";
            this.lblItemCode.Size = new System.Drawing.Size(80, 13);
            this.lblItemCode.TabIndex = 210;
            this.lblItemCode.Text = "Item Code:*";
            // 
            // txtPODate
            // 
            this.txtPODate.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.txtPODate.Location = new System.Drawing.Point(370, 46);
            this.txtPODate.Name = "txtPODate";
            this.txtPODate.ReadOnly = true;
            this.txtPODate.Size = new System.Drawing.Size(111, 21);
            this.txtPODate.TabIndex = 206;
            this.txtPODate.TabStop = false;
            // 
            // txtItemDescription
            // 
            this.txtItemDescription.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.txtItemDescription.Location = new System.Drawing.Point(369, 14);
            this.txtItemDescription.Name = "txtItemDescription";
            this.txtItemDescription.ReadOnly = true;
            this.txtItemDescription.Size = new System.Drawing.Size(111, 21);
            this.txtItemDescription.TabIndex = 204;
            this.txtItemDescription.TabStop = false;
            // 
            // lblItemDescription
            // 
            this.lblItemDescription.AutoSize = true;
            this.lblItemDescription.BackColor = System.Drawing.Color.Transparent;
            this.lblItemDescription.Location = new System.Drawing.Point(297, 10);
            this.lblItemDescription.Name = "lblItemDescription";
            this.lblItemDescription.Size = new System.Drawing.Size(76, 26);
            this.lblItemDescription.TabIndex = 211;
            this.lblItemDescription.Text = "Item \r\nDescription:";
            // 
            // lblReturnQty
            // 
            this.lblReturnQty.AutoSize = true;
            this.lblReturnQty.BackColor = System.Drawing.Color.Transparent;
            this.lblReturnQty.Location = new System.Drawing.Point(764, 79);
            this.lblReturnQty.Name = "lblReturnQty";
            this.lblReturnQty.Size = new System.Drawing.Size(81, 13);
            this.lblReturnQty.TabIndex = 216;
            this.lblReturnQty.Text = "Return Qty:*";
            // 
            // btnClearDetails
            // 
            this.btnClearDetails.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnClearDetails.BackColor = System.Drawing.Color.Transparent;
            this.btnClearDetails.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearDetails.BackgroundImage")));
            this.btnClearDetails.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearDetails.FlatAppearance.BorderSize = 0;
            this.btnClearDetails.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnClearDetails.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnClearDetails.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearDetails.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnClearDetails.Location = new System.Drawing.Point(889, 118);
            this.btnClearDetails.Name = "btnClearDetails";
            this.btnClearDetails.Size = new System.Drawing.Size(75, 32);
            this.btnClearDetails.TabIndex = 7;
            this.btnClearDetails.Text = "C&lear";
            this.btnClearDetails.UseVisualStyleBackColor = false;
            this.btnClearDetails.Click += new System.EventHandler(this.btnClearDetails_Click);
            // 
            // btnAddDetails
            // 
            this.btnAddDetails.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnAddDetails.BackColor = System.Drawing.Color.Transparent;
            this.btnAddDetails.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAddDetails.BackgroundImage")));
            this.btnAddDetails.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAddDetails.FlatAppearance.BorderSize = 0;
            this.btnAddDetails.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnAddDetails.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnAddDetails.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddDetails.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnAddDetails.Location = new System.Drawing.Point(801, 118);
            this.btnAddDetails.Name = "btnAddDetails";
            this.btnAddDetails.Size = new System.Drawing.Size(75, 32);
            this.btnAddDetails.TabIndex = 6;
            this.btnAddDetails.Text = "&Add";
            this.btnAddDetails.UseVisualStyleBackColor = false;
            this.btnAddDetails.Click += new System.EventHandler(this.btnAddDetails_Click);
            // 
            // lblPODate
            // 
            this.lblPODate.AutoSize = true;
            this.lblPODate.BackColor = System.Drawing.Color.Transparent;
            this.lblPODate.Location = new System.Drawing.Point(312, 49);
            this.lblPODate.Name = "lblPODate";
            this.lblPODate.Size = new System.Drawing.Size(59, 13);
            this.lblPODate.TabIndex = 214;
            this.lblPODate.Text = "PO Date:";
            // 
            // lblReturnReason
            // 
            this.lblReturnReason.AutoSize = true;
            this.lblReturnReason.BackColor = System.Drawing.Color.Transparent;
            this.lblReturnReason.Location = new System.Drawing.Point(273, 109);
            this.lblReturnReason.Name = "lblReturnReason";
            this.lblReturnReason.Size = new System.Drawing.Size(100, 13);
            this.lblReturnReason.TabIndex = 221;
            this.lblReturnReason.Text = "Return Reason: ";
            // 
            // tabSearch
            // 
            this.tabSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(233)))), ((int)(((byte)(251)))));
            this.tabSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tabSearch.Controls.Add(this.pnlSearchHeader);
            this.tabSearch.Controls.Add(this.lblSearchResult);
            this.tabSearch.Controls.Add(this.dgvSearchReturnToVendor);
            this.tabSearch.Location = new System.Drawing.Point(4, 22);
            this.tabSearch.Name = "tabSearch";
            this.tabSearch.Size = new System.Drawing.Size(1004, 606);
            this.tabSearch.TabIndex = 0;
            this.tabSearch.Text = "Search";
            // 
            // pnlSearchHeader
            // 
            this.pnlSearchHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlSearchHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(233)))), ((int)(((byte)(251)))));
            this.pnlSearchHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlSearchHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSearchHeader.Controls.Add(this.cmbSearchStatus);
            this.pnlSearchHeader.Controls.Add(this.lblSerachStatus);
            this.pnlSearchHeader.Controls.Add(this.cmbSearchLocationCode);
            this.pnlSearchHeader.Controls.Add(this.lblSearchLocationCode);
            this.pnlSearchHeader.Controls.Add(this.cmbSearchVendorCode);
            this.pnlSearchHeader.Controls.Add(this.lblSearchVendorCode);
            this.pnlSearchHeader.Controls.Add(this.txtSearchDebitNo);
            this.pnlSearchHeader.Controls.Add(this.lblSearchDebitNoteNo);
            this.pnlSearchHeader.Controls.Add(this.dtpSearchTO);
            this.pnlSearchHeader.Controls.Add(this.dtpSearchFrom);
            this.pnlSearchHeader.Controls.Add(this.lblSearchToDate);
            this.pnlSearchHeader.Controls.Add(this.lblSearchFromDate);
            this.pnlSearchHeader.Controls.Add(this.txtSearchVenRetNumber);
            this.pnlSearchHeader.Controls.Add(this.lblSearchVenRetNumber);
            this.pnlSearchHeader.Controls.Add(this.btnSearch);
            this.pnlSearchHeader.Controls.Add(this.btnSearchReset);
            this.pnlSearchHeader.Location = new System.Drawing.Point(2, 2);
            this.pnlSearchHeader.Name = "pnlSearchHeader";
            this.pnlSearchHeader.Size = new System.Drawing.Size(999, 125);
            this.pnlSearchHeader.TabIndex = 0;
            // 
            // cmbSearchStatus
            // 
            this.cmbSearchStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSearchStatus.FormattingEnabled = true;
            this.cmbSearchStatus.Location = new System.Drawing.Point(457, 14);
            this.cmbSearchStatus.Name = "cmbSearchStatus";
            this.cmbSearchStatus.Size = new System.Drawing.Size(121, 21);
            this.cmbSearchStatus.TabIndex = 1;
            // 
            // lblSerachStatus
            // 
            this.lblSerachStatus.AutoSize = true;
            this.lblSerachStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblSerachStatus.Location = new System.Drawing.Point(403, 14);
            this.lblSerachStatus.Name = "lblSerachStatus";
            this.lblSerachStatus.Size = new System.Drawing.Size(48, 13);
            this.lblSerachStatus.TabIndex = 186;
            this.lblSerachStatus.Text = "Status:";
            // 
            // cmbSearchLocationCode
            // 
            this.cmbSearchLocationCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSearchLocationCode.FormattingEnabled = true;
            this.cmbSearchLocationCode.Location = new System.Drawing.Point(114, 14);
            this.cmbSearchLocationCode.Name = "cmbSearchLocationCode";
            this.cmbSearchLocationCode.Size = new System.Drawing.Size(120, 21);
            this.cmbSearchLocationCode.TabIndex = 0;
            // 
            // lblSearchLocationCode
            // 
            this.lblSearchLocationCode.AutoSize = true;
            this.lblSearchLocationCode.BackColor = System.Drawing.Color.Transparent;
            this.lblSearchLocationCode.Location = new System.Drawing.Point(13, 14);
            this.lblSearchLocationCode.Name = "lblSearchLocationCode";
            this.lblSearchLocationCode.Size = new System.Drawing.Size(93, 13);
            this.lblSearchLocationCode.TabIndex = 184;
            this.lblSearchLocationCode.Text = "Location Code:";
            // 
            // cmbSearchVendorCode
            // 
            this.cmbSearchVendorCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSearchVendorCode.FormattingEnabled = true;
            this.cmbSearchVendorCode.Location = new System.Drawing.Point(817, 14);
            this.cmbSearchVendorCode.Name = "cmbSearchVendorCode";
            this.cmbSearchVendorCode.Size = new System.Drawing.Size(111, 21);
            this.cmbSearchVendorCode.TabIndex = 2;
            // 
            // lblSearchVendorCode
            // 
            this.lblSearchVendorCode.AutoSize = true;
            this.lblSearchVendorCode.BackColor = System.Drawing.Color.Transparent;
            this.lblSearchVendorCode.Location = new System.Drawing.Point(714, 14);
            this.lblSearchVendorCode.Name = "lblSearchVendorCode";
            this.lblSearchVendorCode.Size = new System.Drawing.Size(87, 13);
            this.lblSearchVendorCode.TabIndex = 173;
            this.lblSearchVendorCode.Text = "Vendor Code:";
            // 
            // txtSearchDebitNo
            // 
            this.txtSearchDebitNo.Location = new System.Drawing.Point(457, 42);
            this.txtSearchDebitNo.MaxLength = 20;
            this.txtSearchDebitNo.Name = "txtSearchDebitNo";
            this.txtSearchDebitNo.Size = new System.Drawing.Size(121, 21);
            this.txtSearchDebitNo.TabIndex = 4;
            // 
            // lblSearchDebitNoteNo
            // 
            this.lblSearchDebitNoteNo.AutoSize = true;
            this.lblSearchDebitNoteNo.BackColor = System.Drawing.Color.Transparent;
            this.lblSearchDebitNoteNo.Location = new System.Drawing.Point(330, 42);
            this.lblSearchDebitNoteNo.Name = "lblSearchDebitNoteNo";
            this.lblSearchDebitNoteNo.Size = new System.Drawing.Size(121, 13);
            this.lblSearchDebitNoteNo.TabIndex = 172;
            this.lblSearchDebitNoteNo.Text = "Debit Note Number:";
            // 
            // dtpSearchTO
            // 
            this.dtpSearchTO.Checked = false;
            this.dtpSearchTO.CustomFormat = "dd-MM-yyyy";
            this.dtpSearchTO.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpSearchTO.Location = new System.Drawing.Point(457, 69);
            this.dtpSearchTO.Name = "dtpSearchTO";
            this.dtpSearchTO.ShowCheckBox = true;
            this.dtpSearchTO.Size = new System.Drawing.Size(121, 21);
            this.dtpSearchTO.TabIndex = 6;
            this.dtpSearchTO.Value = new System.DateTime(2009, 7, 16, 0, 0, 0, 0);
            // 
            // dtpSearchFrom
            // 
            this.dtpSearchFrom.Checked = false;
            this.dtpSearchFrom.CustomFormat = "dd-MM-yyyy";
            this.dtpSearchFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpSearchFrom.Location = new System.Drawing.Point(114, 68);
            this.dtpSearchFrom.Name = "dtpSearchFrom";
            this.dtpSearchFrom.ShowCheckBox = true;
            this.dtpSearchFrom.Size = new System.Drawing.Size(121, 21);
            this.dtpSearchFrom.TabIndex = 5;
            this.dtpSearchFrom.Value = new System.DateTime(2009, 7, 14, 0, 0, 0, 0);
            // 
            // lblSearchToDate
            // 
            this.lblSearchToDate.AutoSize = true;
            this.lblSearchToDate.BackColor = System.Drawing.Color.Transparent;
            this.lblSearchToDate.Location = new System.Drawing.Point(392, 73);
            this.lblSearchToDate.Name = "lblSearchToDate";
            this.lblSearchToDate.Size = new System.Drawing.Size(59, 13);
            this.lblSearchToDate.TabIndex = 170;
            this.lblSearchToDate.Text = "TO Date:";
            // 
            // lblSearchFromDate
            // 
            this.lblSearchFromDate.AutoSize = true;
            this.lblSearchFromDate.BackColor = System.Drawing.Color.Transparent;
            this.lblSearchFromDate.Location = new System.Drawing.Point(30, 72);
            this.lblSearchFromDate.Name = "lblSearchFromDate";
            this.lblSearchFromDate.Size = new System.Drawing.Size(76, 13);
            this.lblSearchFromDate.TabIndex = 169;
            this.lblSearchFromDate.Text = "From  Date:";
            // 
            // txtSearchVenRetNumber
            // 
            this.txtSearchVenRetNumber.Location = new System.Drawing.Point(114, 41);
            this.txtSearchVenRetNumber.MaxLength = 20;
            this.txtSearchVenRetNumber.Name = "txtSearchVenRetNumber";
            this.txtSearchVenRetNumber.Size = new System.Drawing.Size(121, 21);
            this.txtSearchVenRetNumber.TabIndex = 3;
            // 
            // lblSearchVenRetNumber
            // 
            this.lblSearchVenRetNumber.AutoSize = true;
            this.lblSearchVenRetNumber.BackColor = System.Drawing.Color.Transparent;
            this.lblSearchVenRetNumber.Location = new System.Drawing.Point(7, 45);
            this.lblSearchVenRetNumber.Name = "lblSearchVenRetNumber";
            this.lblSearchVenRetNumber.Size = new System.Drawing.Size(99, 13);
            this.lblSearchVenRetNumber.TabIndex = 171;
            this.lblSearchVenRetNumber.Text = "Return Number:";
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.BackColor = System.Drawing.Color.Transparent;
            this.btnSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.BackgroundImage")));
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnSearch.Location = new System.Drawing.Point(828, 90);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(78, 32);
            this.btnSearch.TabIndex = 7;
            this.btnSearch.Text = "S&earch";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnSearchReset
            // 
            this.btnSearchReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearchReset.BackColor = System.Drawing.Color.Transparent;
            this.btnSearchReset.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearchReset.BackgroundImage")));
            this.btnSearchReset.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearchReset.FlatAppearance.BorderSize = 0;
            this.btnSearchReset.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearchReset.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearchReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchReset.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnSearchReset.Location = new System.Drawing.Point(912, 90);
            this.btnSearchReset.Name = "btnSearchReset";
            this.btnSearchReset.Size = new System.Drawing.Size(75, 32);
            this.btnSearchReset.TabIndex = 8;
            this.btnSearchReset.Text = "&Reset";
            this.btnSearchReset.UseVisualStyleBackColor = false;
            this.btnSearchReset.Click += new System.EventHandler(this.btnSearchReset_Click);
            // 
            // lblSearchResult
            // 
            this.lblSearchResult.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSearchResult.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(117)))), ((int)(((byte)(186)))));
            this.lblSearchResult.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblSearchResult.ForeColor = System.Drawing.Color.White;
            this.lblSearchResult.Location = new System.Drawing.Point(2, 130);
            this.lblSearchResult.Name = "lblSearchResult";
            this.lblSearchResult.Size = new System.Drawing.Size(999, 24);
            this.lblSearchResult.TabIndex = 3;
            this.lblSearchResult.Text = "Search Result";
            this.lblSearchResult.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dgvSearchReturnToVendor
            // 
            this.dgvSearchReturnToVendor.AllowUserToAddRows = false;
            this.dgvSearchReturnToVendor.AllowUserToDeleteRows = false;
            this.dgvSearchReturnToVendor.AllowUserToResizeColumns = false;
            this.dgvSearchReturnToVendor.AllowUserToResizeRows = false;
            this.dgvSearchReturnToVendor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSearchReturnToVendor.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSearchReturnToVendor.Location = new System.Drawing.Point(2, 156);
            this.dgvSearchReturnToVendor.Name = "dgvSearchReturnToVendor";
            this.dgvSearchReturnToVendor.RowHeadersVisible = false;
            this.dgvSearchReturnToVendor.Size = new System.Drawing.Size(999, 447);
            this.dgvSearchReturnToVendor.TabIndex = 2;
            this.dgvSearchReturnToVendor.TabStop = false;
            this.dgvSearchReturnToVendor.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgvSearchReturnToVendor_MouseDoubleClick);
            this.dgvSearchReturnToVendor.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSearchReturnToVendor_CellContentClick);
            // 
            // tabCreate
            // 
            this.tabCreate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(233)))), ((int)(((byte)(251)))));
            this.tabCreate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tabCreate.Controls.Add(this.panel2);
            this.tabCreate.Controls.Add(this.pnlCreateHeader);
            this.tabCreate.Controls.Add(this.lblAddDetails);
            this.tabCreate.Controls.Add(this.grpAddDetails);
            this.tabCreate.Location = new System.Drawing.Point(4, 22);
            this.tabCreate.Name = "tabCreate";
            this.tabCreate.Size = new System.Drawing.Size(1004, 606);
            this.tabCreate.TabIndex = 1;
            this.tabCreate.Text = "Create";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.btnRTVCancel);
            this.panel2.Controls.Add(this.btnPrintDebitNote);
            this.panel2.Controls.Add(this.btnApprove);
            this.panel2.Controls.Add(this.btnPrint);
            this.panel2.Controls.Add(this.btnCreateReset);
            this.panel2.Controls.Add(this.btnSave);
            this.panel2.Controls.Add(this.btnShip);
            this.panel2.Controls.Add(this.btnConfirm);
            this.panel2.Location = new System.Drawing.Point(210, 542);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(767, 38);
            this.panel2.TabIndex = 3;
            // 
            // btnRTVCancel
            // 
            this.btnRTVCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRTVCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnRTVCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRTVCancel.BackgroundImage")));
            this.btnRTVCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRTVCancel.FlatAppearance.BorderSize = 0;
            this.btnRTVCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnRTVCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnRTVCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRTVCancel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnRTVCancel.Location = new System.Drawing.Point(524, 2);
            this.btnRTVCancel.Name = "btnRTVCancel";
            this.btnRTVCancel.Size = new System.Drawing.Size(75, 32);
            this.btnRTVCancel.TabIndex = 7;
            this.btnRTVCancel.Text = "Ca&ncel";
            this.btnRTVCancel.UseVisualStyleBackColor = false;
            this.btnRTVCancel.Click += new System.EventHandler(this.btnRTVCancel_Click);
            // 
            // btnPrintDebitNote
            // 
            this.btnPrintDebitNote.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrintDebitNote.BackColor = System.Drawing.Color.Transparent;
            this.btnPrintDebitNote.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPrintDebitNote.BackgroundImage")));
            this.btnPrintDebitNote.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPrintDebitNote.FlatAppearance.BorderSize = 0;
            this.btnPrintDebitNote.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnPrintDebitNote.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnPrintDebitNote.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrintDebitNote.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnPrintDebitNote.Location = new System.Drawing.Point(73, 2);
            this.btnPrintDebitNote.Name = "btnPrintDebitNote";
            this.btnPrintDebitNote.Size = new System.Drawing.Size(127, 32);
            this.btnPrintDebitNote.TabIndex = 6;
            this.btnPrintDebitNote.Text = "Print &Debit Note";
            this.btnPrintDebitNote.UseVisualStyleBackColor = false;
            this.btnPrintDebitNote.Click += new System.EventHandler(this.btnPrintDebitNote_Click);
            // 
            // btnApprove
            // 
            this.btnApprove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApprove.BackColor = System.Drawing.Color.Transparent;
            this.btnApprove.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnApprove.BackgroundImage")));
            this.btnApprove.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnApprove.FlatAppearance.BorderSize = 0;
            this.btnApprove.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnApprove.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnApprove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApprove.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnApprove.Location = new System.Drawing.Point(362, 2);
            this.btnApprove.Name = "btnApprove";
            this.btnApprove.Size = new System.Drawing.Size(75, 32);
            this.btnApprove.TabIndex = 1;
            this.btnApprove.Text = "Appr&ove";
            this.btnApprove.UseVisualStyleBackColor = false;
            this.btnApprove.Click += new System.EventHandler(this.btnApprove_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.BackColor = System.Drawing.Color.Transparent;
            this.btnPrint.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPrint.BackgroundImage")));
            this.btnPrint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPrint.FlatAppearance.BorderSize = 0;
            this.btnPrint.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnPrint.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnPrint.Location = new System.Drawing.Point(203, 2);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(73, 32);
            this.btnPrint.TabIndex = 0;
            this.btnPrint.Text = "&Print";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnShip
            // 
            this.btnShip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShip.BackColor = System.Drawing.Color.Transparent;
            this.btnShip.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnShip.BackgroundImage")));
            this.btnShip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnShip.FlatAppearance.BorderSize = 0;
            this.btnShip.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnShip.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnShip.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShip.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnShip.Location = new System.Drawing.Point(282, 2);
            this.btnShip.Name = "btnShip";
            this.btnShip.Size = new System.Drawing.Size(75, 32);
            this.btnShip.TabIndex = 2;
            this.btnShip.Text = "Sh&ip";
            this.btnShip.UseVisualStyleBackColor = false;
            this.btnShip.Click += new System.EventHandler(this.btnShip_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfirm.BackColor = System.Drawing.Color.Transparent;
            this.btnConfirm.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnConfirm.BackgroundImage")));
            this.btnConfirm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnConfirm.FlatAppearance.BorderSize = 0;
            this.btnConfirm.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnConfirm.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirm.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnConfirm.Location = new System.Drawing.Point(443, 2);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 32);
            this.btnConfirm.TabIndex = 3;
            this.btnConfirm.Text = "Con&firm";
            this.btnConfirm.UseVisualStyleBackColor = false;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // tabControlTransaction
            // 
            this.tabControlTransaction.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlTransaction.Controls.Add(this.tabSearch);
            this.tabControlTransaction.Controls.Add(this.tabCreate);
            this.tabControlTransaction.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.tabControlTransaction.Location = new System.Drawing.Point(1, 42);
            this.tabControlTransaction.Name = "tabControlTransaction";
            this.tabControlTransaction.SelectedIndex = 0;
            this.tabControlTransaction.Size = new System.Drawing.Size(1012, 632);
            this.tabControlTransaction.TabIndex = 1;
            this.tabControlTransaction.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabControlTransaction_Selecting);
            // 
            // errSearch
            // 
            this.errSearch.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errSearch.ContainerControl = this;
            // 
            // errItem
            // 
            this.errItem.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errItem.ContainerControl = this;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.lblAppUser);
            this.panel1.Controls.Add(this.btnExit);
            this.panel1.Controls.Add(this.lblPageTitle);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1013, 41);
            this.panel1.TabIndex = 16;
            // 
            // lblAppUser
            // 
            this.lblAppUser.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblAppUser.AutoSize = true;
            this.lblAppUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAppUser.ForeColor = System.Drawing.Color.White;
            this.lblAppUser.Location = new System.Drawing.Point(12, 14);
            this.lblAppUser.Margin = new System.Windows.Forms.Padding(10, 0, 3, 0);
            this.lblAppUser.Name = "lblAppUser";
            this.lblAppUser.Size = new System.Drawing.Size(0, 15);
            this.lblAppUser.TabIndex = 18;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.BackgroundImage = global::ReturnsComponent.Properties.Resources.exit;
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnExit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnExit.Location = new System.Drawing.Point(935, 4);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 32);
            this.btnExit.TabIndex = 18;
            this.btnExit.Text = "E&xit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lblPageTitle
            // 
            this.lblPageTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblPageTitle.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Bold);
            this.lblPageTitle.ForeColor = System.Drawing.Color.White;
            this.lblPageTitle.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblPageTitle.Location = new System.Drawing.Point(389, 7);
            this.lblPageTitle.Name = "lblPageTitle";
            this.lblPageTitle.Padding = new System.Windows.Forms.Padding(9, 0, 0, 0);
            this.lblPageTitle.Size = new System.Drawing.Size(260, 24);
            this.lblPageTitle.TabIndex = 17;
            this.lblPageTitle.Text = "Vendor Return";
            this.lblPageTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmVendorReturn
            // 
            this.AcceptButton = this.btnSearch;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(117)))), ((int)(((byte)(186)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1013, 703);
            this.ControlBox = false;
            this.Controls.Add(this.tabControlTransaction);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmVendorReturn";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Vendor Return";
            this.Load += new System.EventHandler(this.frmVendorReturn_Load);
            this.pnlCreateHeader.ResumeLayout(false);
            this.pnlCreateHeader.PerformLayout();
            this.grpAddDetails.ResumeLayout(false);
            this.grpAddDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.pnlCreateDetail.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReturnToVendorItems)).EndInit();
            this.tabSearch.ResumeLayout(false);
            this.pnlSearchHeader.ResumeLayout(false);
            this.pnlSearchHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearchReturnToVendor)).EndInit();
            this.tabCreate.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.tabControlTransaction.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errSearch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errItem)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.Panel pnlCreateHeader;
        protected System.Windows.Forms.Button btnCreateReset;
        protected System.Windows.Forms.Button btnSave;
        protected System.Windows.Forms.Label lblAddDetails;
        protected System.Windows.Forms.GroupBox grpAddDetails;
        protected System.Windows.Forms.Button btnClearDetails;
        protected System.Windows.Forms.Button btnAddDetails;
        protected System.Windows.Forms.TabPage tabSearch;
        protected System.Windows.Forms.Label lblSearchResult;
        protected System.Windows.Forms.DataGridView dgvSearchReturnToVendor;
        protected System.Windows.Forms.TabPage tabCreate;
        protected System.Windows.Forms.TabControl tabControlTransaction;
        private System.Windows.Forms.ComboBox cmbVendorCode;
        private System.Windows.Forms.Label lblVendorCode;
        private System.Windows.Forms.TextBox txtReturnNumber;
        private System.Windows.Forms.Label lblReturnNumber;
        private System.Windows.Forms.TextBox txtDebitNoteAmount;
        private System.Windows.Forms.TextBox txtDebitNoteNo;
        private System.Windows.Forms.Label lblDebitNoteNo;
        private System.Windows.Forms.TextBox txtCurrentStatus;
        private System.Windows.Forms.Label lblCurrentStatus;
        private System.Windows.Forms.DateTimePicker dtpReturnDate;
        private System.Windows.Forms.Label lblRemarks;
        private System.Windows.Forms.Label lblReturnDate;
        private System.Windows.Forms.TextBox txtRemarks;
        private System.Windows.Forms.TextBox txtTotalTaxAmount;
        private System.Windows.Forms.TextBox txtTotalReturnQty;
        private System.Windows.Forms.TextBox txtShippingDetails;
        private System.Windows.Forms.Label lblTotalTaxAmount;
        private System.Windows.Forms.Label lblTotalReturnQty;
        private System.Windows.Forms.Label lblShippingDetails;
        private System.Windows.Forms.DataGridView dgvReturnToVendorItems;
        private System.Windows.Forms.Label lblReturnReason;
        private System.Windows.Forms.TextBox txtReturnReason;
        private System.Windows.Forms.Label POItemQty;
        private System.Windows.Forms.TextBox txtPOItemQty;
        private System.Windows.Forms.TextBox txtPOItemAmount;
        private System.Windows.Forms.TextBox txtReturnQty;
        private System.Windows.Forms.Label lblPOItemAmount;
        private System.Windows.Forms.TextBox txtItemCode;
        private System.Windows.Forms.Label lblPONumber;
        private System.Windows.Forms.Label lblBucketName;
        private System.Windows.Forms.Label lblAvailableQty;
        private System.Windows.Forms.TextBox txtBucketName;
        private System.Windows.Forms.Label lblPODate;
        private System.Windows.Forms.TextBox txtAvailableQty;
        private System.Windows.Forms.Label lblItemCode;
        private System.Windows.Forms.TextBox txtPODate;
        private System.Windows.Forms.TextBox txtItemDescription;
        private System.Windows.Forms.Label lblItemDescription;
        private System.Windows.Forms.Label lblReturnQty;
        protected System.Windows.Forms.Panel pnlCreateDetail;
        private System.Windows.Forms.ComboBox cmbLocationCode;
        private System.Windows.Forms.Label lblLocationCode;
        private System.Windows.Forms.ErrorProvider errSearch;
        private System.Windows.Forms.ErrorProvider errItem;
        private System.Windows.Forms.Label lblDebitNoteAmount;
        protected System.Windows.Forms.Button btnPrint;
        protected System.Windows.Forms.Button btnConfirm;
        protected System.Windows.Forms.Button btnShip;
        protected System.Windows.Forms.Button btnApprove;
        protected System.Windows.Forms.Panel pnlSearchHeader;
        private System.Windows.Forms.ComboBox cmbSearchStatus;
        private System.Windows.Forms.Label lblSerachStatus;
        private System.Windows.Forms.ComboBox cmbSearchLocationCode;
        private System.Windows.Forms.Label lblSearchLocationCode;
        private System.Windows.Forms.ComboBox cmbSearchVendorCode;
        private System.Windows.Forms.Label lblSearchVendorCode;
        private System.Windows.Forms.TextBox txtSearchDebitNo;
        private System.Windows.Forms.Label lblSearchDebitNoteNo;
        private System.Windows.Forms.DateTimePicker dtpSearchTO;
        private System.Windows.Forms.DateTimePicker dtpSearchFrom;
        private System.Windows.Forms.Label lblSearchToDate;
        private System.Windows.Forms.Label lblSearchFromDate;
        private System.Windows.Forms.TextBox txtSearchVenRetNumber;
        private System.Windows.Forms.Label lblSearchVenRetNumber;
        protected System.Windows.Forms.Button btnSearch;
        protected System.Windows.Forms.Button btnSearchReset;
        private System.Windows.Forms.Panel panel1;
        protected System.Windows.Forms.Button btnExit;
        protected System.Windows.Forms.Label lblPageTitle;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox3;
        public System.Windows.Forms.Label lblAppUser;
        private System.Windows.Forms.ComboBox cmbPONumber;
        private System.Windows.Forms.ComboBox cmbGRNInvoiceType;
        private System.Windows.Forms.Label lblInvoiceGRNType;
        private System.Windows.Forms.Label lblGRNInvoiceNo;
        private System.Windows.Forms.ComboBox cmbGRNInvoiceNo;
        private System.Windows.Forms.Label lblGRNInvoiceQty;
        private System.Windows.Forms.TextBox txtGRNInvoiceQty;
        private System.Windows.Forms.TextBox txtItemTax;
        private System.Windows.Forms.Label lblItemTax;
        protected System.Windows.Forms.Button btnPrintDebitNote;
        protected System.Windows.Forms.Button btnRTVCancel;
        private System.Windows.Forms.TextBox txtTotalItemCost;
        private System.Windows.Forms.Label lblTotalItemCost;




    }
}

