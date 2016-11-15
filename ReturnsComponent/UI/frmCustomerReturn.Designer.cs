namespace ReturnsComponent.UI
{
    partial class frmCustomerReturn
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCustomerReturn));
            this.cmbSearchLocation = new System.Windows.Forms.ComboBox();
            this.cmbSearchStatus = new System.Windows.Forms.ComboBox();
            this.dtpSearchTo = new System.Windows.Forms.DateTimePicker();
            this.dtpSearchFrom = new System.Windows.Forms.DateTimePicker();
            this.lblSearchFromDate = new System.Windows.Forms.Label();
            this.lblSearchToDate = new System.Windows.Forms.Label();
            this.lblSearchLocation = new System.Windows.Forms.Label();
            this.lblSearchStatus = new System.Windows.Forms.Label();
            this.lblLocation = new System.Windows.Forms.Label();
            this.lblLocationAddress = new System.Windows.Forms.Label();
            this.lblQuantity = new System.Windows.Forms.Label();
            this.txtQuantity = new System.Windows.Forms.TextBox();
            this.txtItemCode = new System.Windows.Forms.TextBox();
            this.lblItemCode = new System.Windows.Forms.Label();
            this.lblItemDescription = new System.Windows.Forms.Label();
            this.txtItemDescription = new System.Windows.Forms.TextBox();
            this.lblBatchNo = new System.Windows.Forms.Label();
            this.dgvCustomerReturnItem = new System.Windows.Forms.DataGridView();
            this.dgvCustomerReturn = new System.Windows.Forms.DataGridView();
            this.txtLocationAddress = new System.Windows.Forms.TextBox();
            this.txtBatchNo = new System.Windows.Forms.TextBox();
            this.lblBucketName = new System.Windows.Forms.Label();
            this.errCustomerReturn = new System.Windows.Forms.ErrorProvider(this.components);
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnApproved = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.dtpApprovedDate = new System.Windows.Forms.DateTimePicker();
            this.lblApprovedDate = new System.Windows.Forms.Label();
            this.lblVerifiedBy = new System.Windows.Forms.Label();
            this.cmbCustomerReturnBy = new System.Windows.Forms.ComboBox();
            this.cmbLocation = new System.Windows.Forms.ComboBox();
            this.lblRemarks = new System.Windows.Forms.Label();
            this.txtRemarks = new System.Windows.Forms.TextBox();
            this.lblApprovedBy = new System.Windows.Forms.Label();
            this.txtApprovedBy = new System.Windows.Forms.TextBox();
            this.cmbBucket = new System.Windows.Forms.ComboBox();
            this.lblSearchCustomerType = new System.Windows.Forms.Label();
            this.errSearch = new System.Windows.Forms.ErrorProvider(this.components);
            this.lblDistributorPrice = new System.Windows.Forms.Label();
            this.txtDistributorPrice = new System.Windows.Forms.TextBox();
            this.cmbSearchCustomerType = new System.Windows.Forms.ComboBox();
            this.txtSearchDistributorPCId = new System.Windows.Forms.TextBox();
            this.lblSearchDistributorPCId = new System.Windows.Forms.Label();
            this.lblCustomerType = new System.Windows.Forms.Label();
            this.cmbCustomerType = new System.Windows.Forms.ComboBox();
            this.lblMRP = new System.Windows.Forms.Label();
            this.txtMRP = new System.Windows.Forms.TextBox();
            this.lblDistributorIdPCId = new System.Windows.Forms.Label();
            this.txtDistributorPCId = new System.Windows.Forms.TextBox();
            this.lblTotalAmount = new System.Windows.Forms.Label();
            this.txtTotalAmount = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtDeductionAmount = new System.Windows.Forms.TextBox();
            this.txtPayableAmount = new System.Windows.Forms.TextBox();
            this.lblDeductionAmount = new System.Windows.Forms.Label();
            this.lblPayableAmount = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnPrintCreditNode = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pnlInvoiceDetail = new System.Windows.Forms.Panel();
            this.txtInvoiceAmount = new System.Windows.Forms.TextBox();
            this.lblInvoiceAmount = new System.Windows.Forms.Label();
            this.txtTaxAmount = new System.Windows.Forms.TextBox();
            this.lblTaxAmount = new System.Windows.Forms.Label();
            this.txtInvoiceDate = new System.Windows.Forms.TextBox();
            this.lblInvoiceDate = new System.Windows.Forms.Label();
            this.txtDistributorId = new System.Windows.Forms.TextBox();
            this.lblDistributorName = new System.Windows.Forms.Label();
            this.pnlCreateHeader.SuspendLayout();
            this.grpAddDetails.SuspendLayout();
            this.pnlSearchHeader.SuspendLayout();
            this.pnlSearchGrid.SuspendLayout();
            this.pnlCreateDetail.SuspendLayout();
            this.pnlLowerButtons.SuspendLayout();
            this.pnlSearchButtons.SuspendLayout();
            this.tabSearch.SuspendLayout();
            this.tabCreate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomerReturnItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomerReturn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errCustomerReturn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errSearch)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.pnlInvoiceDetail.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCreateHeader
            // 
            this.pnlCreateHeader.Controls.Add(this.cmbLocation);
            this.pnlCreateHeader.Controls.Add(this.pnlInvoiceDetail);
            this.pnlCreateHeader.Controls.Add(this.pictureBox2);
            this.pnlCreateHeader.Controls.Add(this.cmbCustomerType);
            this.pnlCreateHeader.Controls.Add(this.dtpApprovedDate);
            this.pnlCreateHeader.Controls.Add(this.txtTotalAmount);
            this.pnlCreateHeader.Controls.Add(this.txtApprovedBy);
            this.pnlCreateHeader.Controls.Add(this.txtRemarks);
            this.pnlCreateHeader.Controls.Add(this.txtDistributorPCId);
            this.pnlCreateHeader.Controls.Add(this.txtStatus);
            this.pnlCreateHeader.Controls.Add(this.lblTotalAmount);
            this.pnlCreateHeader.Controls.Add(this.lblApprovedDate);
            this.pnlCreateHeader.Controls.Add(this.lblApprovedBy);
            this.pnlCreateHeader.Controls.Add(this.lblDistributorIdPCId);
            this.pnlCreateHeader.Controls.Add(this.txtLocationAddress);
            this.pnlCreateHeader.Controls.Add(this.lblRemarks);
            this.pnlCreateHeader.Controls.Add(this.lblStatus);
            this.pnlCreateHeader.Controls.Add(this.lblLocation);
            this.pnlCreateHeader.Controls.Add(this.lblCustomerType);
            this.pnlCreateHeader.Controls.Add(this.lblLocationAddress);
            this.pnlCreateHeader.Size = new System.Drawing.Size(1005, 154);
            this.pnlCreateHeader.TabIndex = 0;
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblLocationAddress, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblCustomerType, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblLocation, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblStatus, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblRemarks, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.txtLocationAddress, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblDistributorIdPCId, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblApprovedBy, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblApprovedDate, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblTotalAmount, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.txtStatus, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.txtDistributorPCId, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.txtRemarks, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.txtApprovedBy, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.txtTotalAmount, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.dtpApprovedDate, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.cmbCustomerType, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.pictureBox2, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.pnlInvoiceDetail, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.cmbLocation, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.pnlTopButtons, 0);
            // 
            // btnCreateReset
            // 
            this.btnCreateReset.FlatAppearance.BorderSize = 0;
            this.btnCreateReset.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnCreateReset.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnCreateReset.Location = new System.Drawing.Point(675, 0);
            this.btnCreateReset.TabIndex = 1;
            this.btnCreateReset.Click += new System.EventHandler(this.btnCreateReset_Click);
            // 
            // btnSave
            // 
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSave.Location = new System.Drawing.Point(930, 0);
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblAddDetails
            // 
            this.lblAddDetails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(117)))), ((int)(((byte)(186)))));
            this.lblAddDetails.ForeColor = System.Drawing.Color.White;
            this.lblAddDetails.TabIndex = 1;
            this.lblAddDetails.Text = "Item Details";
            // 
            // grpAddDetails
            // 
            this.grpAddDetails.Controls.Add(this.dgvCustomerReturnItem);
            this.grpAddDetails.Controls.Add(this.panel2);
            this.grpAddDetails.Location = new System.Drawing.Point(0, 178);
            this.grpAddDetails.Size = new System.Drawing.Size(1005, 429);
            this.grpAddDetails.TabIndex = 1;
            this.grpAddDetails.Controls.SetChildIndex(this.pnlCreateDetail, 0);
            this.grpAddDetails.Controls.SetChildIndex(this.pnlLowerButtons, 0);
            this.grpAddDetails.Controls.SetChildIndex(this.panel2, 0);
            this.grpAddDetails.Controls.SetChildIndex(this.dgvCustomerReturnItem, 0);
            // 
            // btnClearDetails
            // 
            this.btnClearDetails.Dock = System.Windows.Forms.DockStyle.None;
            this.btnClearDetails.FlatAppearance.BorderSize = 0;
            this.btnClearDetails.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnClearDetails.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnClearDetails.Location = new System.Drawing.Point(872, 72);
            this.btnClearDetails.TabIndex = 5;
            this.btnClearDetails.Click += new System.EventHandler(this.btnClearDetails_Click);
            // 
            // btnAddDetails
            // 
            this.btnAddDetails.Dock = System.Windows.Forms.DockStyle.None;
            this.btnAddDetails.FlatAppearance.BorderSize = 0;
            this.btnAddDetails.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnAddDetails.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnAddDetails.Location = new System.Drawing.Point(792, 72);
            this.btnAddDetails.TabIndex = 4;
            this.btnAddDetails.Click += new System.EventHandler(this.btnAddDetails_Click);
            // 
            // pnlSearchHeader
            // 
            this.pnlSearchHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSearchHeader.Controls.Add(this.txtSearchDistributorPCId);
            this.pnlSearchHeader.Controls.Add(this.lblSearchCustomerType);
            this.pnlSearchHeader.Controls.Add(this.cmbSearchCustomerType);
            this.pnlSearchHeader.Controls.Add(this.lblSearchDistributorPCId);
            this.pnlSearchHeader.Controls.Add(this.cmbSearchLocation);
            this.pnlSearchHeader.Controls.Add(this.cmbCustomerReturnBy);
            this.pnlSearchHeader.Controls.Add(this.cmbSearchStatus);
            this.pnlSearchHeader.Controls.Add(this.dtpSearchTo);
            this.pnlSearchHeader.Controls.Add(this.dtpSearchFrom);
            this.pnlSearchHeader.Controls.Add(this.lblSearchFromDate);
            this.pnlSearchHeader.Controls.Add(this.lblSearchToDate);
            this.pnlSearchHeader.Controls.Add(this.lblVerifiedBy);
            this.pnlSearchHeader.Controls.Add(this.lblSearchLocation);
            this.pnlSearchHeader.Controls.Add(this.lblSearchStatus);
            this.pnlSearchHeader.Size = new System.Drawing.Size(1005, 150);
            this.pnlSearchHeader.TabIndex = 0;
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblSearchStatus, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblSearchLocation, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblVerifiedBy, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblSearchToDate, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblSearchFromDate, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.dtpSearchFrom, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.dtpSearchTo, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.cmbSearchStatus, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.cmbCustomerReturnBy, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.cmbSearchLocation, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblSearchDistributorPCId, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.cmbSearchCustomerType, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblSearchCustomerType, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.txtSearchDistributorPCId, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.pnlSearchButtons, 0);
            // 
            // btnSearch
            // 
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearch.Location = new System.Drawing.Point(853, 0);
            this.btnSearch.TabIndex = 8;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnSearchReset
            // 
            this.btnSearchReset.FlatAppearance.BorderSize = 0;
            this.btnSearchReset.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearchReset.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearchReset.Location = new System.Drawing.Point(928, 0);
            this.btnSearchReset.TabIndex = 9;
            this.btnSearchReset.Click += new System.EventHandler(this.btnSearchReset_Click);
            // 
            // pnlSearchGrid
            // 
            this.pnlSearchGrid.Controls.Add(this.dgvCustomerReturn);
            this.pnlSearchGrid.Location = new System.Drawing.Point(0, 174);
            this.pnlSearchGrid.Size = new System.Drawing.Size(1005, 433);
            // 
            // pnlCreateDetail
            // 
            this.pnlCreateDetail.Controls.Add(this.pictureBox1);
            this.pnlCreateDetail.Controls.Add(this.pictureBox3);
            this.pnlCreateDetail.Controls.Add(this.btnAddDetails);
            this.pnlCreateDetail.Controls.Add(this.btnClearDetails);
            this.pnlCreateDetail.Controls.Add(this.txtItemCode);
            this.pnlCreateDetail.Controls.Add(this.txtItemDescription);
            this.pnlCreateDetail.Controls.Add(this.cmbBucket);
            this.pnlCreateDetail.Controls.Add(this.txtQuantity);
            this.pnlCreateDetail.Controls.Add(this.txtDistributorPrice);
            this.pnlCreateDetail.Controls.Add(this.lblItemDescription);
            this.pnlCreateDetail.Controls.Add(this.lblDistributorPrice);
            this.pnlCreateDetail.Controls.Add(this.lblQuantity);
            this.pnlCreateDetail.Controls.Add(this.lblItemCode);
            this.pnlCreateDetail.Controls.Add(this.txtBatchNo);
            this.pnlCreateDetail.Controls.Add(this.txtMRP);
            this.pnlCreateDetail.Controls.Add(this.lblBatchNo);
            this.pnlCreateDetail.Controls.Add(this.lblMRP);
            this.pnlCreateDetail.Controls.Add(this.lblBucketName);
            this.pnlCreateDetail.TabIndex = 1;
            this.pnlCreateDetail.Controls.SetChildIndex(this.lblBucketName, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.lblMRP, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.lblBatchNo, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.txtMRP, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.txtBatchNo, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.lblItemCode, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.lblQuantity, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.lblDistributorPrice, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.lblItemDescription, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.txtDistributorPrice, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.txtQuantity, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.cmbBucket, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.txtItemDescription, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.txtItemCode, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.btnClearDetails, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.btnAddDetails, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.pictureBox3, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.pictureBox1, 0);
            // 
            // pnlLowerButtons
            // 
            this.pnlLowerButtons.Controls.Add(this.btnPrintCreditNode);
            this.pnlLowerButtons.Controls.Add(this.btnPrint);
            this.pnlLowerButtons.Controls.Add(this.btnApproved);
            this.pnlLowerButtons.Controls.Add(this.btnCancel);
            this.pnlLowerButtons.Location = new System.Drawing.Point(0, 397);
            this.pnlLowerButtons.TabIndex = 3;
            this.pnlLowerButtons.Controls.SetChildIndex(this.btnSave, 0);
            this.pnlLowerButtons.Controls.SetChildIndex(this.btnCancel, 0);
            this.pnlLowerButtons.Controls.SetChildIndex(this.btnApproved, 0);
            this.pnlLowerButtons.Controls.SetChildIndex(this.btnCreateReset, 0);
            this.pnlLowerButtons.Controls.SetChildIndex(this.btnPrint, 0);
            this.pnlLowerButtons.Controls.SetChildIndex(this.btnPrintCreditNode, 0);
            // 
            // pnlTopButtons
            // 
            this.pnlTopButtons.Location = new System.Drawing.Point(0, 152);
            this.pnlTopButtons.Size = new System.Drawing.Size(1003, 0);
            this.pnlTopButtons.Visible = false;
            // 
            // pnlSearchButtons
            // 
            this.pnlSearchButtons.Location = new System.Drawing.Point(0, 116);
            this.pnlSearchButtons.Size = new System.Drawing.Size(1003, 32);
            this.pnlSearchButtons.TabIndex = 7;
            // 
            // tabSearch
            // 
            this.tabSearch.Size = new System.Drawing.Size(1005, 607);
            // 
            // tabCreate
            // 
            this.tabCreate.Size = new System.Drawing.Size(1005, 607);
            // 
            // cmbSearchLocation
            // 
            this.cmbSearchLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSearchLocation.FormattingEnabled = true;
            this.cmbSearchLocation.Location = new System.Drawing.Point(795, 15);
            this.cmbSearchLocation.Name = "cmbSearchLocation";
            this.cmbSearchLocation.Size = new System.Drawing.Size(121, 21);
            this.cmbSearchLocation.TabIndex = 2;
            // 
            // cmbSearchStatus
            // 
            this.cmbSearchStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSearchStatus.FormattingEnabled = true;
            this.cmbSearchStatus.Location = new System.Drawing.Point(795, 42);
            this.cmbSearchStatus.Name = "cmbSearchStatus";
            this.cmbSearchStatus.Size = new System.Drawing.Size(121, 21);
            this.cmbSearchStatus.TabIndex = 5;
            // 
            // dtpSearchTo
            // 
            this.dtpSearchTo.Checked = false;
            this.dtpSearchTo.CustomFormat = "dd-MM-yyyy";
            this.dtpSearchTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpSearchTo.Location = new System.Drawing.Point(462, 41);
            this.dtpSearchTo.Name = "dtpSearchTo";
            this.dtpSearchTo.ShowCheckBox = true;
            this.dtpSearchTo.Size = new System.Drawing.Size(121, 21);
            this.dtpSearchTo.TabIndex = 4;
            this.dtpSearchTo.Value = new System.DateTime(2009, 7, 14, 0, 0, 0, 0);
            // 
            // dtpSearchFrom
            // 
            this.dtpSearchFrom.Checked = false;
            this.dtpSearchFrom.CustomFormat = "dd-MM-yyyy";
            this.dtpSearchFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpSearchFrom.Location = new System.Drawing.Point(143, 46);
            this.dtpSearchFrom.Name = "dtpSearchFrom";
            this.dtpSearchFrom.ShowCheckBox = true;
            this.dtpSearchFrom.Size = new System.Drawing.Size(121, 21);
            this.dtpSearchFrom.TabIndex = 3;
            this.dtpSearchFrom.Value = new System.DateTime(2009, 7, 14, 0, 0, 0, 0);
            // 
            // lblSearchFromDate
            // 
            this.lblSearchFromDate.AutoSize = true;
            this.lblSearchFromDate.Location = new System.Drawing.Point(1, 50);
            this.lblSearchFromDate.Name = "lblSearchFromDate";
            this.lblSearchFromDate.Size = new System.Drawing.Size(141, 13);
            this.lblSearchFromDate.TabIndex = 67;
            this.lblSearchFromDate.Text = "From (Approved) Date:";
            // 
            // lblSearchToDate
            // 
            this.lblSearchToDate.AutoSize = true;
            this.lblSearchToDate.Location = new System.Drawing.Point(335, 45);
            this.lblSearchToDate.Name = "lblSearchToDate";
            this.lblSearchToDate.Size = new System.Drawing.Size(126, 13);
            this.lblSearchToDate.TabIndex = 79;
            this.lblSearchToDate.Text = "To (Approved) Date:";
            // 
            // lblSearchLocation
            // 
            this.lblSearchLocation.AutoSize = true;
            this.lblSearchLocation.Location = new System.Drawing.Point(735, 18);
            this.lblSearchLocation.Name = "lblSearchLocation";
            this.lblSearchLocation.Size = new System.Drawing.Size(59, 13);
            this.lblSearchLocation.TabIndex = 71;
            this.lblSearchLocation.Text = "Location:";
            // 
            // lblSearchStatus
            // 
            this.lblSearchStatus.AutoSize = true;
            this.lblSearchStatus.Location = new System.Drawing.Point(747, 45);
            this.lblSearchStatus.Name = "lblSearchStatus";
            this.lblSearchStatus.Size = new System.Drawing.Size(48, 13);
            this.lblSearchStatus.TabIndex = 72;
            this.lblSearchStatus.Text = "Status:";
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Location = new System.Drawing.Point(763, 6);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(66, 13);
            this.lblLocation.TabIndex = 138;
            this.lblLocation.Text = "Location:*";
            // 
            // lblLocationAddress
            // 
            this.lblLocationAddress.AutoSize = true;
            this.lblLocationAddress.Location = new System.Drawing.Point(715, 38);
            this.lblLocationAddress.Name = "lblLocationAddress";
            this.lblLocationAddress.Size = new System.Drawing.Size(109, 13);
            this.lblLocationAddress.TabIndex = 119;
            this.lblLocationAddress.Text = "Location Address:";
            // 
            // lblQuantity
            // 
            this.lblQuantity.AutoSize = true;
            this.lblQuantity.Location = new System.Drawing.Point(541, 48);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new System.Drawing.Size(67, 13);
            this.lblQuantity.TabIndex = 91;
            this.lblQuantity.Text = "Quantity:*";
            // 
            // txtQuantity
            // 
            this.txtQuantity.Location = new System.Drawing.Point(608, 45);
            this.txtQuantity.MaxLength = 10;
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(121, 21);
            this.txtQuantity.TabIndex = 3;
            this.txtQuantity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtQuantity.Validated += new System.EventHandler(this.txtQuantity_Validated);
            // 
            // txtItemCode
            // 
            this.txtItemCode.Location = new System.Drawing.Point(92, 15);
            this.txtItemCode.MaxLength = 20;
            this.txtItemCode.Name = "txtItemCode";
            this.txtItemCode.Size = new System.Drawing.Size(121, 21);
            this.txtItemCode.TabIndex = 0;
            this.txtItemCode.Validated += new System.EventHandler(this.txtItemCode_Validated);
            this.txtItemCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtItemCode_KeyDown);
            // 
            // lblItemCode
            // 
            this.lblItemCode.AutoSize = true;
            this.lblItemCode.Location = new System.Drawing.Point(15, 19);
            this.lblItemCode.Name = "lblItemCode";
            this.lblItemCode.Size = new System.Drawing.Size(80, 13);
            this.lblItemCode.TabIndex = 84;
            this.lblItemCode.Text = "Item Code:*";
            // 
            // lblItemDescription
            // 
            this.lblItemDescription.AutoSize = true;
            this.lblItemDescription.Location = new System.Drawing.Point(255, 19);
            this.lblItemDescription.Name = "lblItemDescription";
            this.lblItemDescription.Size = new System.Drawing.Size(107, 13);
            this.lblItemDescription.TabIndex = 85;
            this.lblItemDescription.Text = "Item Description:";
            // 
            // txtItemDescription
            // 
            this.txtItemDescription.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.txtItemDescription.Location = new System.Drawing.Point(360, 15);
            this.txtItemDescription.Name = "txtItemDescription";
            this.txtItemDescription.ReadOnly = true;
            this.txtItemDescription.Size = new System.Drawing.Size(121, 21);
            this.txtItemDescription.TabIndex = 0;
            this.txtItemDescription.TabStop = false;
            // 
            // lblBatchNo
            // 
            this.lblBatchNo.AutoSize = true;
            this.lblBatchNo.Location = new System.Drawing.Point(25, 48);
            this.lblBatchNo.Name = "lblBatchNo";
            this.lblBatchNo.Size = new System.Drawing.Size(70, 13);
            this.lblBatchNo.TabIndex = 82;
            this.lblBatchNo.Text = "Batch No:*";
            this.lblBatchNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dgvCustomerReturnItem
            // 
            this.dgvCustomerReturnItem.AllowDrop = true;
            this.dgvCustomerReturnItem.AllowUserToAddRows = false;
            this.dgvCustomerReturnItem.AllowUserToDeleteRows = false;
            this.dgvCustomerReturnItem.AllowUserToResizeColumns = false;
            this.dgvCustomerReturnItem.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dgvCustomerReturnItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCustomerReturnItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCustomerReturnItem.Location = new System.Drawing.Point(0, 154);
            this.dgvCustomerReturnItem.MultiSelect = false;
            this.dgvCustomerReturnItem.Name = "dgvCustomerReturnItem";
            this.dgvCustomerReturnItem.RowHeadersVisible = false;
            this.dgvCustomerReturnItem.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCustomerReturnItem.Size = new System.Drawing.Size(1005, 153);
            this.dgvCustomerReturnItem.TabIndex = 12;
            this.dgvCustomerReturnItem.TabStop = false;
            this.dgvCustomerReturnItem.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvStockItem_CellClick);
            this.dgvCustomerReturnItem.SelectionChanged += new System.EventHandler(this.dgvStockItem_SelectionChanged);
            this.dgvCustomerReturnItem.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCustomerReturnItem_CellContentClick);
            // 
            // dgvCustomerReturn
            // 
            this.dgvCustomerReturn.AllowUserToAddRows = false;
            this.dgvCustomerReturn.AllowUserToDeleteRows = false;
            this.dgvCustomerReturn.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dgvCustomerReturn.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCustomerReturn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCustomerReturn.Location = new System.Drawing.Point(0, 0);
            this.dgvCustomerReturn.MultiSelect = false;
            this.dgvCustomerReturn.Name = "dgvCustomerReturn";
            this.dgvCustomerReturn.ReadOnly = true;
            this.dgvCustomerReturn.RowHeadersVisible = false;
            this.dgvCustomerReturn.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCustomerReturn.Size = new System.Drawing.Size(1005, 433);
            this.dgvCustomerReturn.TabIndex = 11;
            this.dgvCustomerReturn.TabStop = false;
            this.dgvCustomerReturn.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgvCustomerReturn_MouseDoubleClick);
            this.dgvCustomerReturn.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCustomerReturn_CellContentClick);
            // 
            // txtLocationAddress
            // 
            this.txtLocationAddress.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.txtLocationAddress.Location = new System.Drawing.Point(825, 35);
            this.txtLocationAddress.Multiline = true;
            this.txtLocationAddress.Name = "txtLocationAddress";
            this.txtLocationAddress.ReadOnly = true;
            this.txtLocationAddress.Size = new System.Drawing.Size(121, 80);
            this.txtLocationAddress.TabIndex = 0;
            this.txtLocationAddress.TabStop = false;
            // 
            // txtBatchNo
            // 
            this.txtBatchNo.Location = new System.Drawing.Point(92, 45);
            this.txtBatchNo.MaxLength = 20;
            this.txtBatchNo.Name = "txtBatchNo";
            this.txtBatchNo.Size = new System.Drawing.Size(121, 21);
            this.txtBatchNo.TabIndex = 2;
            this.txtBatchNo.Validated += new System.EventHandler(this.txtBatchNo_Validated);
            this.txtBatchNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBatchNo_KeyDown);
            // 
            // lblBucketName
            // 
            this.lblBucketName.AutoSize = true;
            this.lblBucketName.Location = new System.Drawing.Point(735, 18);
            this.lblBucketName.Name = "lblBucketName";
            this.lblBucketName.Size = new System.Drawing.Size(95, 13);
            this.lblBucketName.TabIndex = 88;
            this.lblBucketName.Text = "Bucket Name:*";
            // 
            // errCustomerReturn
            // 
            this.errCustomerReturn.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errCustomerReturn.ContainerControl = this;
            // 
            // btnPrint
            // 
            this.btnPrint.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPrint.BackgroundImage")));
            this.btnPrint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPrint.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnPrint.FlatAppearance.BorderSize = 0;
            this.btnPrint.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnPrint.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnPrint.Location = new System.Drawing.Point(600, 0);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 32);
            this.btnPrint.TabIndex = 0;
            this.btnPrint.Text = "&Print";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnApproved
            // 
            this.btnApproved.BackColor = System.Drawing.Color.Transparent;
            this.btnApproved.BackgroundImage = global::ReturnsComponent.Properties.Resources.button;
            this.btnApproved.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnApproved.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnApproved.FlatAppearance.BorderSize = 0;
            this.btnApproved.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnApproved.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnApproved.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApproved.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnApproved.Location = new System.Drawing.Point(750, 0);
            this.btnApproved.Name = "btnApproved";
            this.btnApproved.Size = new System.Drawing.Size(102, 32);
            this.btnApproved.TabIndex = 2;
            this.btnApproved.Text = "A&pprove";
            this.btnApproved.UseVisualStyleBackColor = false;
            this.btnApproved.Click += new System.EventHandler(this.btnApproved_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.BackgroundImage")));
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnCancel.Location = new System.Drawing.Point(852, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(78, 32);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Canc&el";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(475, 68);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(48, 13);
            this.lblStatus.TabIndex = 123;
            this.lblStatus.Text = "Status:";
            // 
            // txtStatus
            // 
            this.txtStatus.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.txtStatus.Location = new System.Drawing.Point(523, 65);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.ReadOnly = true;
            this.txtStatus.Size = new System.Drawing.Size(119, 21);
            this.txtStatus.TabIndex = 0;
            this.txtStatus.TabStop = false;
            // 
            // dtpApprovedDate
            // 
            this.dtpApprovedDate.Checked = false;
            this.dtpApprovedDate.CustomFormat = "dd-MM-yyyy";
            this.dtpApprovedDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpApprovedDate.Location = new System.Drawing.Point(149, 32);
            this.dtpApprovedDate.Name = "dtpApprovedDate";
            this.dtpApprovedDate.ShowCheckBox = true;
            this.dtpApprovedDate.Size = new System.Drawing.Size(121, 21);
            this.dtpApprovedDate.TabIndex = 3;
            this.dtpApprovedDate.Value = new System.DateTime(2009, 7, 14, 0, 0, 0, 0);
            this.dtpApprovedDate.Validated += new System.EventHandler(this.dtpCustomerReturnDate_Validated);
            // 
            // lblApprovedDate
            // 
            this.lblApprovedDate.AutoSize = true;
            this.lblApprovedDate.Location = new System.Drawing.Point(46, 33);
            this.lblApprovedDate.Name = "lblApprovedDate";
            this.lblApprovedDate.Size = new System.Drawing.Size(105, 13);
            this.lblApprovedDate.TabIndex = 124;
            this.lblApprovedDate.Text = "Approved Date:*";
            // 
            // lblVerifiedBy
            // 
            this.lblVerifiedBy.AutoSize = true;
            this.lblVerifiedBy.Location = new System.Drawing.Point(59, 79);
            this.lblVerifiedBy.Name = "lblVerifiedBy";
            this.lblVerifiedBy.Size = new System.Drawing.Size(85, 13);
            this.lblVerifiedBy.TabIndex = 72;
            this.lblVerifiedBy.Text = "Approved by:";
            // 
            // cmbCustomerReturnBy
            // 
            this.cmbCustomerReturnBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCustomerReturnBy.FormattingEnabled = true;
            this.cmbCustomerReturnBy.Location = new System.Drawing.Point(143, 76);
            this.cmbCustomerReturnBy.Name = "cmbCustomerReturnBy";
            this.cmbCustomerReturnBy.Size = new System.Drawing.Size(121, 21);
            this.cmbCustomerReturnBy.TabIndex = 6;
            // 
            // cmbLocation
            // 
            this.cmbLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocation.FormattingEnabled = true;
            this.cmbLocation.Location = new System.Drawing.Point(825, 5);
            this.cmbLocation.Name = "cmbLocation";
            this.cmbLocation.Size = new System.Drawing.Size(121, 21);
            this.cmbLocation.TabIndex = 2;
            this.cmbLocation.SelectedIndexChanged += new System.EventHandler(this.cmbLocation_SelectedIndexChanged);
            // 
            // lblRemarks
            // 
            this.lblRemarks.AutoSize = true;
            this.lblRemarks.Location = new System.Drawing.Point(88, 65);
            this.lblRemarks.Name = "lblRemarks";
            this.lblRemarks.Size = new System.Drawing.Size(63, 13);
            this.lblRemarks.TabIndex = 82;
            this.lblRemarks.Text = "Remarks:";
            this.lblRemarks.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtRemarks
            // 
            this.txtRemarks.Location = new System.Drawing.Point(149, 62);
            this.txtRemarks.MaxLength = 100;
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Size = new System.Drawing.Size(121, 21);
            this.txtRemarks.TabIndex = 4;
            // 
            // lblApprovedBy
            // 
            this.lblApprovedBy.AutoSize = true;
            this.lblApprovedBy.Location = new System.Drawing.Point(437, 39);
            this.lblApprovedBy.Name = "lblApprovedBy";
            this.lblApprovedBy.Size = new System.Drawing.Size(85, 13);
            this.lblApprovedBy.TabIndex = 123;
            this.lblApprovedBy.Text = "Approved by:";
            // 
            // txtApprovedBy
            // 
            this.txtApprovedBy.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.txtApprovedBy.Location = new System.Drawing.Point(523, 36);
            this.txtApprovedBy.Name = "txtApprovedBy";
            this.txtApprovedBy.ReadOnly = true;
            this.txtApprovedBy.Size = new System.Drawing.Size(121, 21);
            this.txtApprovedBy.TabIndex = 0;
            this.txtApprovedBy.TabStop = false;
            // 
            // cmbBucket
            // 
            this.cmbBucket.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBucket.FormattingEnabled = true;
            this.cmbBucket.Location = new System.Drawing.Point(830, 15);
            this.cmbBucket.Name = "cmbBucket";
            this.cmbBucket.Size = new System.Drawing.Size(121, 21);
            this.cmbBucket.TabIndex = 1;
            // 
            // lblSearchCustomerType
            // 
            this.lblSearchCustomerType.AutoSize = true;
            this.lblSearchCustomerType.Location = new System.Drawing.Point(43, 18);
            this.lblSearchCustomerType.Name = "lblSearchCustomerType";
            this.lblSearchCustomerType.Size = new System.Drawing.Size(100, 13);
            this.lblSearchCustomerType.TabIndex = 91;
            this.lblSearchCustomerType.Text = "Customer Type:";
            // 
            // errSearch
            // 
            this.errSearch.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errSearch.ContainerControl = this;
            // 
            // lblDistributorPrice
            // 
            this.lblDistributorPrice.AutoSize = true;
            this.lblDistributorPrice.Location = new System.Drawing.Point(503, 19);
            this.lblDistributorPrice.Name = "lblDistributorPrice";
            this.lblDistributorPrice.Size = new System.Drawing.Size(104, 13);
            this.lblDistributorPrice.TabIndex = 85;
            this.lblDistributorPrice.Text = "Distributor Price:";
            // 
            // txtDistributorPrice
            // 
            this.txtDistributorPrice.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.txtDistributorPrice.Location = new System.Drawing.Point(608, 15);
            this.txtDistributorPrice.Name = "txtDistributorPrice";
            this.txtDistributorPrice.ReadOnly = true;
            this.txtDistributorPrice.Size = new System.Drawing.Size(121, 21);
            this.txtDistributorPrice.TabIndex = 0;
            this.txtDistributorPrice.TabStop = false;
            this.txtDistributorPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // cmbSearchCustomerType
            // 
            this.cmbSearchCustomerType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSearchCustomerType.FormattingEnabled = true;
            this.cmbSearchCustomerType.Location = new System.Drawing.Point(143, 15);
            this.cmbSearchCustomerType.Name = "cmbSearchCustomerType";
            this.cmbSearchCustomerType.Size = new System.Drawing.Size(121, 21);
            this.cmbSearchCustomerType.TabIndex = 0;
            this.cmbSearchCustomerType.SelectedIndexChanged += new System.EventHandler(this.cmbSearchCustomerType_SelectedIndexChanged);
            // 
            // txtSearchDistributorPCId
            // 
            this.txtSearchDistributorPCId.Location = new System.Drawing.Point(462, 15);
            this.txtSearchDistributorPCId.Name = "txtSearchDistributorPCId";
            this.txtSearchDistributorPCId.Size = new System.Drawing.Size(100, 21);
            this.txtSearchDistributorPCId.TabIndex = 1;
            // 
            // lblSearchDistributorPCId
            // 
            this.lblSearchDistributorPCId.AutoSize = true;
            this.lblSearchDistributorPCId.Location = new System.Drawing.Point(278, 18);
            this.lblSearchDistributorPCId.Name = "lblSearchDistributorPCId";
            this.lblSearchDistributorPCId.Size = new System.Drawing.Size(183, 13);
            this.lblSearchDistributorPCId.TabIndex = 91;
            this.lblSearchDistributorPCId.Text = "Distributor/PC Id/Invoice No. :";
            this.lblSearchDistributorPCId.UseMnemonic = false;
            // 
            // lblCustomerType
            // 
            this.lblCustomerType.AutoSize = true;
            this.lblCustomerType.Location = new System.Drawing.Point(44, 6);
            this.lblCustomerType.Name = "lblCustomerType";
            this.lblCustomerType.Size = new System.Drawing.Size(107, 13);
            this.lblCustomerType.TabIndex = 138;
            this.lblCustomerType.Text = "Customer Type:*";
            // 
            // cmbCustomerType
            // 
            this.cmbCustomerType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCustomerType.FormattingEnabled = true;
            this.cmbCustomerType.Location = new System.Drawing.Point(149, 5);
            this.cmbCustomerType.Name = "cmbCustomerType";
            this.cmbCustomerType.Size = new System.Drawing.Size(121, 21);
            this.cmbCustomerType.TabIndex = 0;
            this.cmbCustomerType.SelectedIndexChanged += new System.EventHandler(this.cmbCustomerType_SelectedIndexChanged);
            // 
            // lblMRP
            // 
            this.lblMRP.AutoSize = true;
            this.lblMRP.Location = new System.Drawing.Point(319, 48);
            this.lblMRP.Name = "lblMRP";
            this.lblMRP.Size = new System.Drawing.Size(36, 13);
            this.lblMRP.TabIndex = 82;
            this.lblMRP.Text = "MRP:";
            this.lblMRP.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtMRP
            // 
            this.txtMRP.Location = new System.Drawing.Point(360, 45);
            this.txtMRP.MaxLength = 20;
            this.txtMRP.Name = "txtMRP";
            this.txtMRP.ReadOnly = true;
            this.txtMRP.Size = new System.Drawing.Size(121, 21);
            this.txtMRP.TabIndex = 0;
            this.txtMRP.TabStop = false;
            this.txtMRP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtMRP.Validated += new System.EventHandler(this.txtBatchNo_Validated);
            this.txtMRP.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBatchNo_KeyDown);
            // 
            // lblDistributorIdPCId
            // 
            this.lblDistributorIdPCId.AutoSize = true;
            this.lblDistributorIdPCId.Location = new System.Drawing.Point(340, 6);
            this.lblDistributorIdPCId.Name = "lblDistributorIdPCId";
            this.lblDistributorIdPCId.Size = new System.Drawing.Size(183, 13);
            this.lblDistributorIdPCId.TabIndex = 82;
            this.lblDistributorIdPCId.Text = "Distributor/PC Id/Invoice No. :";
            this.lblDistributorIdPCId.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDistributorPCId
            // 
            this.txtDistributorPCId.Location = new System.Drawing.Point(523, 4);
            this.txtDistributorPCId.MaxLength = 20;
            this.txtDistributorPCId.Name = "txtDistributorPCId";
            this.txtDistributorPCId.Size = new System.Drawing.Size(121, 21);
            this.txtDistributorPCId.TabIndex = 1;
            this.txtDistributorPCId.Validated += new System.EventHandler(this.txtDistributorPCId_Validated);
            this.txtDistributorPCId.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDistributorPCId_KeyDown);
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.AutoSize = true;
            this.lblTotalAmount.Location = new System.Drawing.Point(63, 92);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(88, 13);
            this.lblTotalAmount.TabIndex = 82;
            this.lblTotalAmount.Text = "Total Amount:";
            this.lblTotalAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTotalAmount
            // 
            this.txtTotalAmount.Location = new System.Drawing.Point(149, 90);
            this.txtTotalAmount.MaxLength = 100;
            this.txtTotalAmount.Name = "txtTotalAmount";
            this.txtTotalAmount.ReadOnly = true;
            this.txtTotalAmount.Size = new System.Drawing.Size(121, 21);
            this.txtTotalAmount.TabIndex = 2;
            this.txtTotalAmount.TabStop = false;
            this.txtTotalAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.txtDeductionAmount);
            this.panel2.Controls.Add(this.txtPayableAmount);
            this.panel2.Controls.Add(this.lblDeductionAmount);
            this.panel2.Controls.Add(this.lblPayableAmount);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 307);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1005, 90);
            this.panel2.TabIndex = 2;
            // 
            // txtDeductionAmount
            // 
            this.txtDeductionAmount.Location = new System.Drawing.Point(126, 11);
            this.txtDeductionAmount.MaxLength = 20;
            this.txtDeductionAmount.Name = "txtDeductionAmount";
            this.txtDeductionAmount.Size = new System.Drawing.Size(121, 21);
            this.txtDeductionAmount.TabIndex = 0;
            this.txtDeductionAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtDeductionAmount.Validated += new System.EventHandler(this.txtDeductionAmount_Validated);
            // 
            // txtPayableAmount
            // 
            this.txtPayableAmount.Location = new System.Drawing.Point(126, 38);
            this.txtPayableAmount.MaxLength = 20;
            this.txtPayableAmount.Name = "txtPayableAmount";
            this.txtPayableAmount.ReadOnly = true;
            this.txtPayableAmount.Size = new System.Drawing.Size(121, 21);
            this.txtPayableAmount.TabIndex = 84;
            this.txtPayableAmount.TabStop = false;
            this.txtPayableAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblDeductionAmount
            // 
            this.lblDeductionAmount.AutoSize = true;
            this.lblDeductionAmount.Location = new System.Drawing.Point(6, 14);
            this.lblDeductionAmount.Name = "lblDeductionAmount";
            this.lblDeductionAmount.Size = new System.Drawing.Size(117, 13);
            this.lblDeductionAmount.TabIndex = 85;
            this.lblDeductionAmount.Text = "Deduction Amount:";
            this.lblDeductionAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPayableAmount
            // 
            this.lblPayableAmount.AutoSize = true;
            this.lblPayableAmount.Location = new System.Drawing.Point(18, 41);
            this.lblPayableAmount.Name = "lblPayableAmount";
            this.lblPayableAmount.Size = new System.Drawing.Size(105, 13);
            this.lblPayableAmount.TabIndex = 86;
            this.lblPayableAmount.Text = "Payable Amount:";
            this.lblPayableAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::ReturnsComponent.Properties.Resources.find;
            this.pictureBox3.Location = new System.Drawing.Point(226, 15);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(21, 21);
            this.pictureBox3.TabIndex = 160;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::ReturnsComponent.Properties.Resources.find;
            this.pictureBox1.Location = new System.Drawing.Point(226, 45);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(21, 21);
            this.pictureBox1.TabIndex = 160;
            this.pictureBox1.TabStop = false;
            // 
            // btnPrintCreditNode
            // 
            this.btnPrintCreditNode.AutoSize = true;
            this.btnPrintCreditNode.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPrintCreditNode.BackgroundImage")));
            this.btnPrintCreditNode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPrintCreditNode.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnPrintCreditNode.FlatAppearance.BorderSize = 0;
            this.btnPrintCreditNode.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnPrintCreditNode.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnPrintCreditNode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrintCreditNode.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnPrintCreditNode.Location = new System.Drawing.Point(475, 0);
            this.btnPrintCreditNode.Name = "btnPrintCreditNode";
            this.btnPrintCreditNode.Size = new System.Drawing.Size(125, 32);
            this.btnPrintCreditNode.TabIndex = 5;
            this.btnPrintCreditNode.Text = "Print &Credit Note";
            this.btnPrintCreditNode.UseVisualStyleBackColor = false;
            this.btnPrintCreditNode.Click += new System.EventHandler(this.btnPrintCreditNode_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::ReturnsComponent.Properties.Resources.find;
            this.pictureBox2.Location = new System.Drawing.Point(645, 3);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(21, 21);
            this.pictureBox2.TabIndex = 225;
            this.pictureBox2.TabStop = false;
            // 
            // pnlInvoiceDetail
            // 
            this.pnlInvoiceDetail.Controls.Add(this.txtInvoiceAmount);
            this.pnlInvoiceDetail.Controls.Add(this.lblInvoiceAmount);
            this.pnlInvoiceDetail.Controls.Add(this.txtTaxAmount);
            this.pnlInvoiceDetail.Controls.Add(this.lblTaxAmount);
            this.pnlInvoiceDetail.Controls.Add(this.txtInvoiceDate);
            this.pnlInvoiceDetail.Controls.Add(this.lblInvoiceDate);
            this.pnlInvoiceDetail.Controls.Add(this.txtDistributorId);
            this.pnlInvoiceDetail.Controls.Add(this.lblDistributorName);
            this.pnlInvoiceDetail.Location = new System.Drawing.Point(4, 118);
            this.pnlInvoiceDetail.Name = "pnlInvoiceDetail";
            this.pnlInvoiceDetail.Size = new System.Drawing.Size(950, 30);
            this.pnlInvoiceDetail.TabIndex = 226;
            this.pnlInvoiceDetail.Visible = false;
            // 
            // txtInvoiceAmount
            // 
            this.txtInvoiceAmount.Location = new System.Drawing.Point(821, 4);
            this.txtInvoiceAmount.MaxLength = 100;
            this.txtInvoiceAmount.Name = "txtInvoiceAmount";
            this.txtInvoiceAmount.ReadOnly = true;
            this.txtInvoiceAmount.Size = new System.Drawing.Size(120, 21);
            this.txtInvoiceAmount.TabIndex = 89;
            this.txtInvoiceAmount.TabStop = false;
            this.txtInvoiceAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblInvoiceAmount
            // 
            this.lblInvoiceAmount.AutoSize = true;
            this.lblInvoiceAmount.Location = new System.Drawing.Point(716, 9);
            this.lblInvoiceAmount.Name = "lblInvoiceAmount";
            this.lblInvoiceAmount.Size = new System.Drawing.Size(102, 13);
            this.lblInvoiceAmount.TabIndex = 90;
            this.lblInvoiceAmount.Text = "Invoice Amount:";
            this.lblInvoiceAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTaxAmount
            // 
            this.txtTaxAmount.Location = new System.Drawing.Point(585, 5);
            this.txtTaxAmount.MaxLength = 100;
            this.txtTaxAmount.Name = "txtTaxAmount";
            this.txtTaxAmount.ReadOnly = true;
            this.txtTaxAmount.Size = new System.Drawing.Size(120, 21);
            this.txtTaxAmount.TabIndex = 87;
            this.txtTaxAmount.TabStop = false;
            this.txtTaxAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblTaxAmount
            // 
            this.lblTaxAmount.AutoSize = true;
            this.lblTaxAmount.Location = new System.Drawing.Point(504, 9);
            this.lblTaxAmount.Name = "lblTaxAmount";
            this.lblTaxAmount.Size = new System.Drawing.Size(81, 13);
            this.lblTaxAmount.TabIndex = 88;
            this.lblTaxAmount.Text = "Tax Amount:";
            this.lblTaxAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtInvoiceDate
            // 
            this.txtInvoiceDate.Location = new System.Drawing.Point(370, 5);
            this.txtInvoiceDate.MaxLength = 100;
            this.txtInvoiceDate.Name = "txtInvoiceDate";
            this.txtInvoiceDate.ReadOnly = true;
            this.txtInvoiceDate.Size = new System.Drawing.Size(120, 21);
            this.txtInvoiceDate.TabIndex = 85;
            this.txtInvoiceDate.TabStop = false;
            this.txtInvoiceDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblInvoiceDate
            // 
            this.lblInvoiceDate.AutoSize = true;
            this.lblInvoiceDate.Location = new System.Drawing.Point(281, 8);
            this.lblInvoiceDate.Name = "lblInvoiceDate";
            this.lblInvoiceDate.Size = new System.Drawing.Size(85, 13);
            this.lblInvoiceDate.TabIndex = 86;
            this.lblInvoiceDate.Text = "Invoice Date:";
            this.lblInvoiceDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDistributorId
            // 
            this.txtDistributorId.Location = new System.Drawing.Point(145, 4);
            this.txtDistributorId.MaxLength = 100;
            this.txtDistributorId.Name = "txtDistributorId";
            this.txtDistributorId.ReadOnly = true;
            this.txtDistributorId.Size = new System.Drawing.Size(120, 21);
            this.txtDistributorId.TabIndex = 83;
            this.txtDistributorId.TabStop = false;
            this.txtDistributorId.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblDistributorName
            // 
            this.lblDistributorName.AutoSize = true;
            this.lblDistributorName.Location = new System.Drawing.Point(35, 8);
            this.lblDistributorName.Name = "lblDistributorName";
            this.lblDistributorName.Size = new System.Drawing.Size(88, 13);
            this.lblDistributorName.TabIndex = 84;
            this.lblDistributorName.Text = "Distributor Id:";
            this.lblDistributorName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // frmCustomerReturn
            // 
            this.AcceptButton = this.btnSearch;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1013, 703);
            this.Name = "frmCustomerReturn";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "frmTO";
            this.WindowState = System.Windows.Forms.FormWindowState.Normal;
            this.pnlCreateHeader.ResumeLayout(false);
            this.pnlCreateHeader.PerformLayout();
            this.grpAddDetails.ResumeLayout(false);
            this.pnlSearchHeader.ResumeLayout(false);
            this.pnlSearchHeader.PerformLayout();
            this.pnlSearchGrid.ResumeLayout(false);
            this.pnlCreateDetail.ResumeLayout(false);
            this.pnlCreateDetail.PerformLayout();
            this.pnlLowerButtons.ResumeLayout(false);
            this.pnlLowerButtons.PerformLayout();
            this.pnlSearchButtons.ResumeLayout(false);
            this.tabSearch.ResumeLayout(false);
            this.tabCreate.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomerReturnItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomerReturn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errCustomerReturn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errSearch)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.pnlInvoiceDetail.ResumeLayout(false);
            this.pnlInvoiceDetail.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbSearchLocation;
        private System.Windows.Forms.ComboBox cmbSearchStatus;
        private System.Windows.Forms.DateTimePicker dtpSearchTo;
        private System.Windows.Forms.DateTimePicker dtpSearchFrom;
        private System.Windows.Forms.Label lblSearchFromDate;
        private System.Windows.Forms.Label lblSearchToDate;
        private System.Windows.Forms.Label lblSearchLocation;
        private System.Windows.Forms.Label lblSearchStatus;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.Label lblLocationAddress;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.TextBox txtItemCode;
        private System.Windows.Forms.Label lblItemCode;
        private System.Windows.Forms.Label lblItemDescription;
        private System.Windows.Forms.TextBox txtItemDescription;
        private System.Windows.Forms.Label lblBatchNo;
        private System.Windows.Forms.DataGridView dgvCustomerReturnItem;
        private System.Windows.Forms.DataGridView dgvCustomerReturn;
        private System.Windows.Forms.TextBox txtLocationAddress;
        private System.Windows.Forms.TextBox txtBatchNo;
        private System.Windows.Forms.Label lblBucketName;
        private System.Windows.Forms.ErrorProvider errCustomerReturn;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.DateTimePicker dtpApprovedDate;
        private System.Windows.Forms.Label lblApprovedDate;
        private System.Windows.Forms.ComboBox cmbCustomerReturnBy;
        private System.Windows.Forms.Label lblVerifiedBy;
        private System.Windows.Forms.ComboBox cmbLocation;
        private System.Windows.Forms.TextBox txtRemarks;
        private System.Windows.Forms.Label lblRemarks;
        private System.Windows.Forms.TextBox txtApprovedBy;
        private System.Windows.Forms.Label lblApprovedBy;
        private System.Windows.Forms.ComboBox cmbBucket;
        private System.Windows.Forms.Label lblSearchCustomerType;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnApproved;
        private System.Windows.Forms.ErrorProvider errSearch;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.TextBox txtDistributorPrice;
        private System.Windows.Forms.Label lblDistributorPrice;
        private System.Windows.Forms.ComboBox cmbSearchCustomerType;
        private System.Windows.Forms.TextBox txtSearchDistributorPCId;
        private System.Windows.Forms.Label lblSearchDistributorPCId;
        private System.Windows.Forms.ComboBox cmbCustomerType;
        private System.Windows.Forms.Label lblCustomerType;
        private System.Windows.Forms.TextBox txtMRP;
        private System.Windows.Forms.Label lblMRP;
        private System.Windows.Forms.TextBox txtDistributorPCId;
        private System.Windows.Forms.Label lblDistributorIdPCId;
        private System.Windows.Forms.TextBox txtTotalAmount;
        private System.Windows.Forms.Label lblTotalAmount;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtDeductionAmount;
        private System.Windows.Forms.Label lblPayableAmount;
        private System.Windows.Forms.TextBox txtPayableAmount;
        private System.Windows.Forms.Label lblDeductionAmount;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnPrintCreditNode;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Panel pnlInvoiceDetail;
        private System.Windows.Forms.TextBox txtInvoiceAmount;
        private System.Windows.Forms.Label lblInvoiceAmount;
        private System.Windows.Forms.TextBox txtTaxAmount;
        private System.Windows.Forms.Label lblTaxAmount;
        private System.Windows.Forms.TextBox txtInvoiceDate;
        private System.Windows.Forms.Label lblInvoiceDate;
        private System.Windows.Forms.TextBox txtDistributorId;
        private System.Windows.Forms.Label lblDistributorName;
    }
}