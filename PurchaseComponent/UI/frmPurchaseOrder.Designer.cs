namespace PurchaseComponent.UI
{
    partial class frmPurchaseOrder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPurchaseOrder));
            this.lblPONumber = new System.Windows.Forms.Label();
            this.lblPOAmendmentNumber = new System.Windows.Forms.Label();
            this.lblVendorCode = new System.Windows.Forms.Label();
            this.cmbVendorCode = new System.Windows.Forms.ComboBox();
            this.lblVendorName = new System.Windows.Forms.Label();
            this.txtVendorName = new System.Windows.Forms.TextBox();
            this.lblVendorAddress = new System.Windows.Forms.Label();
            this.txtVendorAddress = new System.Windows.Forms.TextBox();
            this.lblPaymentTerms = new System.Windows.Forms.Label();
            this.txtPaymentTerms = new System.Windows.Forms.TextBox();
            this.lblDestinationAddress = new System.Windows.Forms.Label();
            this.lblDestinationLocation = new System.Windows.Forms.Label();
            this.cmbDestinationLocation = new System.Windows.Forms.ComboBox();
            this.txtDestinationAddress = new System.Windows.Forms.TextBox();
            this.lblPODate = new System.Windows.Forms.Label();
            this.dtpExpDeliveryDate = new System.Windows.Forms.DateTimePicker();
            this.lblExpDeliveryDate = new System.Windows.Forms.Label();
            this.dtpMaxDeliveryDate = new System.Windows.Forms.DateTimePicker();
            this.lblMaxDeliveryDate = new System.Windows.Forms.Label();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnShortClosed = new System.Windows.Forms.Button();
            this.txtShippingDetails = new System.Windows.Forms.TextBox();
            this.lblShippingDetails = new System.Windows.Forms.Label();
            this.txtRemarks = new System.Windows.Forms.TextBox();
            this.lblPODetails = new System.Windows.Forms.Label();
            this.txtPaymentDetails = new System.Windows.Forms.TextBox();
            this.lblPaymentDetails = new System.Windows.Forms.Label();
            this.lblFormCApplicable = new System.Windows.Forms.Label();
            this.chkFormCApplicable = new System.Windows.Forms.CheckBox();
            this.lblTotalTaxAmount = new System.Windows.Forms.Label();
            this.lblTotalPOAmount = new System.Windows.Forms.Label();
            this.cmbPOType = new System.Windows.Forms.ComboBox();
            this.lblPOType = new System.Windows.Forms.Label();
            this.txtItemCode = new System.Windows.Forms.TextBox();
            this.lblItemCode = new System.Windows.Forms.Label();
            this.txtItemName = new System.Windows.Forms.TextBox();
            this.lblItemName = new System.Windows.Forms.Label();
            this.txtItemUOM = new System.Windows.Forms.TextBox();
            this.lblItemUOM = new System.Windows.Forms.Label();
            this.lblTaxGroupCode = new System.Windows.Forms.Label();
            this.txtUnitPrice = new System.Windows.Forms.TextBox();
            this.lblUnitPrice = new System.Windows.Forms.Label();
            this.txtUnitQty = new System.Windows.Forms.TextBox();
            this.lblUnitQty = new System.Windows.Forms.Label();
            this.txtItemTotalTax = new System.Windows.Forms.TextBox();
            this.lblItemTotalTax = new System.Windows.Forms.Label();
            this.btnAddItem = new System.Windows.Forms.Button();
            this.btnClearItem = new System.Windows.Forms.Button();
            this.txtItemTotalAmount = new System.Windows.Forms.TextBox();
            this.lblItemTotalAmount = new System.Windows.Forms.Label();
            this.dgvPOItems = new System.Windows.Forms.DataGridView();
            this.lblSearchPONumber = new System.Windows.Forms.Label();
            this.txtSearchPONumber = new System.Windows.Forms.TextBox();
            this.cmbSearchVendorCode = new System.Windows.Forms.ComboBox();
            this.lblSearchVendorCode = new System.Windows.Forms.Label();
            this.dtpSearchFromPODate = new System.Windows.Forms.DateTimePicker();
            this.lblSearchFromPODate = new System.Windows.Forms.Label();
            this.dtpSearchToPODate = new System.Windows.Forms.DateTimePicker();
            this.lblSearchToPODate = new System.Windows.Forms.Label();
            this.cmbSearchStatus = new System.Windows.Forms.ComboBox();
            this.lblSearchStatus = new System.Windows.Forms.Label();
            this.cmbSearchDestinationLocation = new System.Windows.Forms.ComboBox();
            this.lblSearchDestinationLocation = new System.Windows.Forms.Label();
            this.lblSearchFormCApplicable = new System.Windows.Forms.Label();
            this.chkSearchFormCApplicable = new System.Windows.Forms.CheckBox();
            this.txtTotalTaxAmount = new System.Windows.Forms.TextBox();
            this.txtTotalPOAmount = new System.Windows.Forms.TextBox();
            this.txtPOAmendmentNumber = new System.Windows.Forms.TextBox();
            this.txtPONumber = new System.Windows.Forms.TextBox();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnAmend = new System.Windows.Forms.Button();
            this.lblCreationDate = new System.Windows.Forms.Label();
            this.lblCreatedDateValue = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblStatusValue = new System.Windows.Forms.Label();
            this.errorCreatePO = new System.Windows.Forms.ErrorProvider(this.components);
            this.txtTaxGroupCode = new System.Windows.Forms.TextBox();
            this.dgvPoSearch = new System.Windows.Forms.DataGridView();
            this.cmbSearchPOType = new System.Windows.Forms.ComboBox();
            this.lblSearchPoType = new System.Windows.Forms.Label();
            this.errorSearch = new System.Windows.Forms.ErrorProvider(this.components);
            this.lblPODateValue = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblVendorCodeValue = new System.Windows.Forms.Label();
            this.lblDestinationCodeValue = new System.Windows.Forms.Label();
            this.txtttlAmount = new System.Windows.Forms.TextBox();
            this.lblttlAmount = new System.Windows.Forms.Label();
            this.chkIsImported = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlCreateHeader.SuspendLayout();
            this.grpAddDetails.SuspendLayout();
            this.pnlSearchHeader.SuspendLayout();
            this.pnlSearchGrid.SuspendLayout();
            this.pnlCreateDetail.SuspendLayout();
            this.pnlLowerButtons.SuspendLayout();
            this.pnlTopButtons.SuspendLayout();
            this.pnlSearchButtons.SuspendLayout();
            this.tabSearch.SuspendLayout();
            this.tabCreate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPOItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorCreatePO)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPoSearch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorSearch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCreateHeader
            // 
            this.pnlCreateHeader.Controls.Add(this.label1);
            this.pnlCreateHeader.Controls.Add(this.chkIsImported);
            this.pnlCreateHeader.Controls.Add(this.lblPODateValue);
            this.pnlCreateHeader.Controls.Add(this.lblStatusValue);
            this.pnlCreateHeader.Controls.Add(this.lblStatus);
            this.pnlCreateHeader.Controls.Add(this.lblCreatedDateValue);
            this.pnlCreateHeader.Controls.Add(this.lblCreationDate);
            this.pnlCreateHeader.Controls.Add(this.txtPONumber);
            this.pnlCreateHeader.Controls.Add(this.txtPOAmendmentNumber);
            this.pnlCreateHeader.Controls.Add(this.txtTotalPOAmount);
            this.pnlCreateHeader.Controls.Add(this.txtTotalTaxAmount);
            this.pnlCreateHeader.Controls.Add(this.cmbPOType);
            this.pnlCreateHeader.Controls.Add(this.lblFormCApplicable);
            this.pnlCreateHeader.Controls.Add(this.chkFormCApplicable);
            this.pnlCreateHeader.Controls.Add(this.txtShippingDetails);
            this.pnlCreateHeader.Controls.Add(this.lblShippingDetails);
            this.pnlCreateHeader.Controls.Add(this.lblPODate);
            this.pnlCreateHeader.Controls.Add(this.txtPaymentTerms);
            this.pnlCreateHeader.Controls.Add(this.lblPaymentTerms);
            this.pnlCreateHeader.Controls.Add(this.lblPOType);
            this.pnlCreateHeader.Controls.Add(this.lblTotalTaxAmount);
            this.pnlCreateHeader.Controls.Add(this.txtPaymentDetails);
            this.pnlCreateHeader.Controls.Add(this.lblTotalPOAmount);
            this.pnlCreateHeader.Controls.Add(this.lblPaymentDetails);
            this.pnlCreateHeader.Controls.Add(this.txtRemarks);
            this.pnlCreateHeader.Controls.Add(this.lblPODetails);
            this.pnlCreateHeader.Controls.Add(this.dtpMaxDeliveryDate);
            this.pnlCreateHeader.Controls.Add(this.lblMaxDeliveryDate);
            this.pnlCreateHeader.Controls.Add(this.dtpExpDeliveryDate);
            this.pnlCreateHeader.Controls.Add(this.lblExpDeliveryDate);
            this.pnlCreateHeader.Controls.Add(this.txtDestinationAddress);
            this.pnlCreateHeader.Controls.Add(this.cmbDestinationLocation);
            this.pnlCreateHeader.Controls.Add(this.lblDestinationLocation);
            this.pnlCreateHeader.Controls.Add(this.lblDestinationAddress);
            this.pnlCreateHeader.Controls.Add(this.txtVendorAddress);
            this.pnlCreateHeader.Controls.Add(this.lblVendorAddress);
            this.pnlCreateHeader.Controls.Add(this.txtVendorName);
            this.pnlCreateHeader.Controls.Add(this.lblVendorName);
            this.pnlCreateHeader.Controls.Add(this.cmbVendorCode);
            this.pnlCreateHeader.Controls.Add(this.lblVendorCode);
            this.pnlCreateHeader.Controls.Add(this.lblPOAmendmentNumber);
            this.pnlCreateHeader.Controls.Add(this.lblPONumber);
            this.pnlCreateHeader.Controls.Add(this.lblDestinationCodeValue);
            this.pnlCreateHeader.Controls.Add(this.lblVendorCodeValue);
            this.pnlCreateHeader.Size = new System.Drawing.Size(1005, 265);
            this.pnlCreateHeader.TabIndex = 0;
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblVendorCodeValue, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblDestinationCodeValue, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblPONumber, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblPOAmendmentNumber, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblVendorCode, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.cmbVendorCode, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblVendorName, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.txtVendorName, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblVendorAddress, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.txtVendorAddress, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblDestinationAddress, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblDestinationLocation, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.cmbDestinationLocation, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.txtDestinationAddress, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblExpDeliveryDate, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.dtpExpDeliveryDate, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblMaxDeliveryDate, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.dtpMaxDeliveryDate, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblPODetails, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.txtRemarks, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblPaymentDetails, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblTotalPOAmount, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.txtPaymentDetails, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblTotalTaxAmount, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblPOType, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblPaymentTerms, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.txtPaymentTerms, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblPODate, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblShippingDetails, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.txtShippingDetails, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.chkFormCApplicable, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblFormCApplicable, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.cmbPOType, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.txtTotalTaxAmount, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.txtTotalPOAmount, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.txtPOAmendmentNumber, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.txtPONumber, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblCreationDate, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblCreatedDateValue, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblStatus, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblStatusValue, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblPODateValue, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.pnlTopButtons, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.chkIsImported, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.label1, 0);
            // 
            // btnCreateReset
            // 
            this.btnCreateReset.FlatAppearance.BorderSize = 0;
            this.btnCreateReset.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnCreateReset.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnCreateReset.Location = new System.Drawing.Point(855, 0);
            this.btnCreateReset.Click += new System.EventHandler(this.btnCreateReset_Click);
            // 
            // btnSave
            // 
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSave.Location = new System.Drawing.Point(439, 0);
            this.btnSave.TabIndex = 0;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblAddDetails
            // 
            this.lblAddDetails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(117)))), ((int)(((byte)(186)))));
            this.lblAddDetails.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAddDetails.ForeColor = System.Drawing.Color.White;
            this.lblAddDetails.Text = "Item Detail";
            // 
            // grpAddDetails
            // 
            this.grpAddDetails.Controls.Add(this.dgvPOItems);
            this.grpAddDetails.Location = new System.Drawing.Point(0, 289);
            this.grpAddDetails.Size = new System.Drawing.Size(1005, 318);
            this.grpAddDetails.TabIndex = 1;
            this.grpAddDetails.Controls.SetChildIndex(this.pnlCreateDetail, 0);
            this.grpAddDetails.Controls.SetChildIndex(this.pnlLowerButtons, 0);
            this.grpAddDetails.Controls.SetChildIndex(this.dgvPOItems, 0);
            // 
            // btnClearDetails
            // 
            this.btnClearDetails.FlatAppearance.BorderSize = 0;
            this.btnClearDetails.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnClearDetails.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnClearDetails.Location = new System.Drawing.Point(928, 0);
            this.btnClearDetails.Size = new System.Drawing.Size(75, 0);
            this.btnClearDetails.TabStop = false;
            this.btnClearDetails.Text = "Clear";
            this.btnClearDetails.Visible = false;
            // 
            // btnAddDetails
            // 
            this.btnAddDetails.FlatAppearance.BorderSize = 0;
            this.btnAddDetails.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnAddDetails.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnAddDetails.Location = new System.Drawing.Point(853, 0);
            this.btnAddDetails.Size = new System.Drawing.Size(75, 0);
            this.btnAddDetails.TabStop = false;
            this.btnAddDetails.Text = "Add";
            this.btnAddDetails.Visible = false;
            // 
            // pnlSearchHeader
            // 
            this.pnlSearchHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSearchHeader.Controls.Add(this.cmbSearchPOType);
            this.pnlSearchHeader.Controls.Add(this.lblSearchPoType);
            this.pnlSearchHeader.Controls.Add(this.cmbSearchDestinationLocation);
            this.pnlSearchHeader.Controls.Add(this.chkSearchFormCApplicable);
            this.pnlSearchHeader.Controls.Add(this.lblSearchFormCApplicable);
            this.pnlSearchHeader.Controls.Add(this.lblSearchStatus);
            this.pnlSearchHeader.Controls.Add(this.cmbSearchStatus);
            this.pnlSearchHeader.Controls.Add(this.lblSearchDestinationLocation);
            this.pnlSearchHeader.Controls.Add(this.dtpSearchToPODate);
            this.pnlSearchHeader.Controls.Add(this.lblSearchToPODate);
            this.pnlSearchHeader.Controls.Add(this.cmbSearchVendorCode);
            this.pnlSearchHeader.Controls.Add(this.lblSearchFromPODate);
            this.pnlSearchHeader.Controls.Add(this.dtpSearchFromPODate);
            this.pnlSearchHeader.Controls.Add(this.lblSearchVendorCode);
            this.pnlSearchHeader.Controls.Add(this.lblSearchPONumber);
            this.pnlSearchHeader.Controls.Add(this.txtSearchPONumber);
            this.pnlSearchHeader.Size = new System.Drawing.Size(1005, 146);
            this.pnlSearchHeader.TabIndex = 0;
            this.pnlSearchHeader.Controls.SetChildIndex(this.txtSearchPONumber, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblSearchPONumber, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblSearchVendorCode, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.dtpSearchFromPODate, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblSearchFromPODate, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.cmbSearchVendorCode, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblSearchToPODate, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.dtpSearchToPODate, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblSearchDestinationLocation, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.cmbSearchStatus, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblSearchStatus, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblSearchFormCApplicable, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.chkSearchFormCApplicable, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.cmbSearchDestinationLocation, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblSearchPoType, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.cmbSearchPOType, 0);
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
            this.pnlSearchGrid.Controls.Add(this.dgvPoSearch);
            this.pnlSearchGrid.Location = new System.Drawing.Point(0, 170);
            this.pnlSearchGrid.Size = new System.Drawing.Size(1005, 437);
            this.pnlSearchGrid.TabIndex = 1;
            // 
            // pnlCreateDetail
            // 
            this.pnlCreateDetail.Controls.Add(this.txtttlAmount);
            this.pnlCreateDetail.Controls.Add(this.lblttlAmount);
            this.pnlCreateDetail.Controls.Add(this.pictureBox1);
            this.pnlCreateDetail.Controls.Add(this.txtTaxGroupCode);
            this.pnlCreateDetail.Controls.Add(this.lblItemName);
            this.pnlCreateDetail.Controls.Add(this.txtItemTotalAmount);
            this.pnlCreateDetail.Controls.Add(this.lblUnitPrice);
            this.pnlCreateDetail.Controls.Add(this.lblItemTotalAmount);
            this.pnlCreateDetail.Controls.Add(this.lblTaxGroupCode);
            this.pnlCreateDetail.Controls.Add(this.txtUnitPrice);
            this.pnlCreateDetail.Controls.Add(this.txtItemUOM);
            this.pnlCreateDetail.Controls.Add(this.btnClearItem);
            this.pnlCreateDetail.Controls.Add(this.lblUnitQty);
            this.pnlCreateDetail.Controls.Add(this.lblItemCode);
            this.pnlCreateDetail.Controls.Add(this.lblItemUOM);
            this.pnlCreateDetail.Controls.Add(this.btnAddItem);
            this.pnlCreateDetail.Controls.Add(this.txtUnitQty);
            this.pnlCreateDetail.Controls.Add(this.txtItemCode);
            this.pnlCreateDetail.Controls.Add(this.txtItemName);
            this.pnlCreateDetail.Controls.Add(this.txtItemTotalTax);
            this.pnlCreateDetail.Controls.Add(this.lblItemTotalTax);
            this.pnlCreateDetail.TabIndex = 2;
            // 
            // pnlLowerButtons
            // 
            this.pnlLowerButtons.Controls.Add(this.btnConfirm);
            this.pnlLowerButtons.Controls.Add(this.btnCancel);
            this.pnlLowerButtons.Controls.Add(this.btnAmend);
            this.pnlLowerButtons.Controls.Add(this.btnShortClosed);
            this.pnlLowerButtons.Controls.Add(this.btnPrint);
            this.pnlLowerButtons.Location = new System.Drawing.Point(0, 286);
            this.pnlLowerButtons.Controls.SetChildIndex(this.btnPrint, 0);
            this.pnlLowerButtons.Controls.SetChildIndex(this.btnCreateReset, 0);
            this.pnlLowerButtons.Controls.SetChildIndex(this.btnShortClosed, 0);
            this.pnlLowerButtons.Controls.SetChildIndex(this.btnAmend, 0);
            this.pnlLowerButtons.Controls.SetChildIndex(this.btnCancel, 0);
            this.pnlLowerButtons.Controls.SetChildIndex(this.btnConfirm, 0);
            this.pnlLowerButtons.Controls.SetChildIndex(this.btnSave, 0);
            // 
            // pnlTopButtons
            // 
            this.pnlTopButtons.Location = new System.Drawing.Point(0, 263);
            this.pnlTopButtons.Size = new System.Drawing.Size(1003, 0);
            this.pnlTopButtons.Visible = false;
            // 
            // pnlSearchButtons
            // 
            this.pnlSearchButtons.Location = new System.Drawing.Point(0, 112);
            this.pnlSearchButtons.Size = new System.Drawing.Size(1003, 32);
            this.pnlSearchButtons.TabIndex = 8;
            // 
            // tabSearch
            // 
            this.tabSearch.Size = new System.Drawing.Size(1005, 607);
            // 
            // tabCreate
            // 
            this.tabCreate.Size = new System.Drawing.Size(1005, 607);
            // 
            // lblPONumber
            // 
            this.lblPONumber.Location = new System.Drawing.Point(63, 19);
            this.lblPONumber.Name = "lblPONumber";
            this.lblPONumber.Size = new System.Drawing.Size(125, 13);
            this.lblPONumber.TabIndex = 29;
            this.lblPONumber.Text = "PO Number:";
            this.lblPONumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPOAmendmentNumber
            // 
            this.lblPOAmendmentNumber.Location = new System.Drawing.Point(361, 19);
            this.lblPOAmendmentNumber.Name = "lblPOAmendmentNumber";
            this.lblPOAmendmentNumber.Size = new System.Drawing.Size(135, 13);
            this.lblPOAmendmentNumber.TabIndex = 31;
            this.lblPOAmendmentNumber.Text = "Amendment Number:";
            this.lblPOAmendmentNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblVendorCode
            // 
            this.lblVendorCode.Location = new System.Drawing.Point(53, 46);
            this.lblVendorCode.Name = "lblVendorCode";
            this.lblVendorCode.Size = new System.Drawing.Size(135, 13);
            this.lblVendorCode.TabIndex = 33;
            this.lblVendorCode.Text = "Vendor Code:*";
            this.lblVendorCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbVendorCode
            // 
            this.cmbVendorCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbVendorCode.FormattingEnabled = true;
            this.cmbVendorCode.Location = new System.Drawing.Point(194, 43);
            this.cmbVendorCode.Name = "cmbVendorCode";
            this.cmbVendorCode.Size = new System.Drawing.Size(140, 21);
            this.cmbVendorCode.TabIndex = 1;
            // 
            // lblVendorName
            // 
            this.lblVendorName.Location = new System.Drawing.Point(371, 46);
            this.lblVendorName.Name = "lblVendorName";
            this.lblVendorName.Size = new System.Drawing.Size(125, 13);
            this.lblVendorName.TabIndex = 35;
            this.lblVendorName.Text = "Vendor Name:";
            this.lblVendorName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtVendorName
            // 
            this.txtVendorName.Location = new System.Drawing.Point(502, 46);
            this.txtVendorName.MaxLength = 100;
            this.txtVendorName.Name = "txtVendorName";
            this.txtVendorName.ReadOnly = true;
            this.txtVendorName.Size = new System.Drawing.Size(140, 21);
            this.txtVendorName.TabIndex = 36;
            this.txtVendorName.TabStop = false;
            // 
            // lblVendorAddress
            // 
            this.lblVendorAddress.Location = new System.Drawing.Point(371, 72);
            this.lblVendorAddress.Name = "lblVendorAddress";
            this.lblVendorAddress.Size = new System.Drawing.Size(125, 13);
            this.lblVendorAddress.TabIndex = 37;
            this.lblVendorAddress.Text = "Vendor Address:";
            this.lblVendorAddress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtVendorAddress
            // 
            this.txtVendorAddress.Location = new System.Drawing.Point(502, 72);
            this.txtVendorAddress.MaxLength = 500;
            this.txtVendorAddress.Multiline = true;
            this.txtVendorAddress.Name = "txtVendorAddress";
            this.txtVendorAddress.ReadOnly = true;
            this.txtVendorAddress.Size = new System.Drawing.Size(140, 50);
            this.txtVendorAddress.TabIndex = 38;
            this.txtVendorAddress.TabStop = false;
            // 
            // lblPaymentTerms
            // 
            this.lblPaymentTerms.Location = new System.Drawing.Point(63, 72);
            this.lblPaymentTerms.Name = "lblPaymentTerms";
            this.lblPaymentTerms.Size = new System.Drawing.Size(125, 13);
            this.lblPaymentTerms.TabIndex = 39;
            this.lblPaymentTerms.Text = "Payment Terms:";
            this.lblPaymentTerms.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPaymentTerms
            // 
            this.txtPaymentTerms.Location = new System.Drawing.Point(194, 72);
            this.txtPaymentTerms.MaxLength = 100;
            this.txtPaymentTerms.Name = "txtPaymentTerms";
            this.txtPaymentTerms.ReadOnly = true;
            this.txtPaymentTerms.Size = new System.Drawing.Size(140, 21);
            this.txtPaymentTerms.TabIndex = 2;
            this.txtPaymentTerms.TabStop = false;
            // 
            // lblDestinationAddress
            // 
            this.lblDestinationAddress.Location = new System.Drawing.Point(685, 72);
            this.lblDestinationAddress.Name = "lblDestinationAddress";
            this.lblDestinationAddress.Size = new System.Drawing.Size(135, 13);
            this.lblDestinationAddress.TabIndex = 41;
            this.lblDestinationAddress.Text = "Destination Address:";
            this.lblDestinationAddress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDestinationLocation
            // 
            this.lblDestinationLocation.Location = new System.Drawing.Point(685, 46);
            this.lblDestinationLocation.Name = "lblDestinationLocation";
            this.lblDestinationLocation.Size = new System.Drawing.Size(135, 13);
            this.lblDestinationLocation.TabIndex = 42;
            this.lblDestinationLocation.Text = "Destination Location:*";
            this.lblDestinationLocation.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbDestinationLocation
            // 
            this.cmbDestinationLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDestinationLocation.FormattingEnabled = true;
            this.cmbDestinationLocation.Location = new System.Drawing.Point(826, 46);
            this.cmbDestinationLocation.Name = "cmbDestinationLocation";
            this.cmbDestinationLocation.Size = new System.Drawing.Size(140, 21);
            this.cmbDestinationLocation.TabIndex = 2;
            // 
            // txtDestinationAddress
            // 
            this.txtDestinationAddress.Location = new System.Drawing.Point(826, 72);
            this.txtDestinationAddress.MaxLength = 500;
            this.txtDestinationAddress.Multiline = true;
            this.txtDestinationAddress.Name = "txtDestinationAddress";
            this.txtDestinationAddress.ReadOnly = true;
            this.txtDestinationAddress.Size = new System.Drawing.Size(140, 50);
            this.txtDestinationAddress.TabIndex = 44;
            this.txtDestinationAddress.TabStop = false;
            // 
            // lblPODate
            // 
            this.lblPODate.Location = new System.Drawing.Point(53, 141);
            this.lblPODate.Name = "lblPODate";
            this.lblPODate.Size = new System.Drawing.Size(135, 13);
            this.lblPODate.TabIndex = 45;
            this.lblPODate.Text = "PO Date:*";
            this.lblPODate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpExpDeliveryDate
            // 
            this.dtpExpDeliveryDate.CustomFormat = "dd-MM-yyyy";
            this.dtpExpDeliveryDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpExpDeliveryDate.Location = new System.Drawing.Point(502, 137);
            this.dtpExpDeliveryDate.Name = "dtpExpDeliveryDate";
            this.dtpExpDeliveryDate.Size = new System.Drawing.Size(140, 21);
            this.dtpExpDeliveryDate.TabIndex = 3;
            // 
            // lblExpDeliveryDate
            // 
            this.lblExpDeliveryDate.Location = new System.Drawing.Point(346, 137);
            this.lblExpDeliveryDate.Name = "lblExpDeliveryDate";
            this.lblExpDeliveryDate.Size = new System.Drawing.Size(150, 13);
            this.lblExpDeliveryDate.TabIndex = 47;
            this.lblExpDeliveryDate.Text = "Expected Delivery Date:";
            this.lblExpDeliveryDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpMaxDeliveryDate
            // 
            this.dtpMaxDeliveryDate.CustomFormat = "dd-MM-yyyy";
            this.dtpMaxDeliveryDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpMaxDeliveryDate.Location = new System.Drawing.Point(826, 137);
            this.dtpMaxDeliveryDate.Name = "dtpMaxDeliveryDate";
            this.dtpMaxDeliveryDate.Size = new System.Drawing.Size(140, 21);
            this.dtpMaxDeliveryDate.TabIndex = 4;
            // 
            // lblMaxDeliveryDate
            // 
            this.lblMaxDeliveryDate.Location = new System.Drawing.Point(670, 137);
            this.lblMaxDeliveryDate.Name = "lblMaxDeliveryDate";
            this.lblMaxDeliveryDate.Size = new System.Drawing.Size(150, 13);
            this.lblMaxDeliveryDate.TabIndex = 49;
            this.lblMaxDeliveryDate.Text = "Max. Delivery Date:";
            this.lblMaxDeliveryDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnConfirm
            // 
            this.btnConfirm.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnConfirm.BackgroundImage")));
            this.btnConfirm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnConfirm.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnConfirm.FlatAppearance.BorderSize = 0;
            this.btnConfirm.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnConfirm.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirm.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnConfirm.Location = new System.Drawing.Point(514, 0);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(76, 32);
            this.btnConfirm.TabIndex = 1;
            this.btnConfirm.Text = "&Confirm";
            this.btnConfirm.UseVisualStyleBackColor = false;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
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
            this.btnCancel.Location = new System.Drawing.Point(590, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 32);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Canc&el";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnShortClosed
            // 
            this.btnShortClosed.BackColor = System.Drawing.Color.Transparent;
            this.btnShortClosed.BackgroundImage = global::PurchaseComponent.Properties.Resources.button;
            this.btnShortClosed.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnShortClosed.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnShortClosed.FlatAppearance.BorderSize = 0;
            this.btnShortClosed.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnShortClosed.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnShortClosed.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShortClosed.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnShortClosed.Location = new System.Drawing.Point(740, 0);
            this.btnShortClosed.Name = "btnShortClosed";
            this.btnShortClosed.Size = new System.Drawing.Size(115, 32);
            this.btnShortClosed.TabIndex = 4;
            this.btnShortClosed.Text = "Short Cl&osed";
            this.btnShortClosed.UseVisualStyleBackColor = false;
            this.btnShortClosed.Click += new System.EventHandler(this.btnShortClosed_Click);
            // 
            // txtShippingDetails
            // 
            this.txtShippingDetails.Location = new System.Drawing.Point(502, 204);
            this.txtShippingDetails.MaxLength = 100;
            this.txtShippingDetails.Multiline = true;
            this.txtShippingDetails.Name = "txtShippingDetails";
            this.txtShippingDetails.Size = new System.Drawing.Size(140, 50);
            this.txtShippingDetails.TabIndex = 7;
            // 
            // lblShippingDetails
            // 
            this.lblShippingDetails.Location = new System.Drawing.Point(371, 206);
            this.lblShippingDetails.Name = "lblShippingDetails";
            this.lblShippingDetails.Size = new System.Drawing.Size(125, 13);
            this.lblShippingDetails.TabIndex = 51;
            this.lblShippingDetails.Text = "Shipping Details:";
            this.lblShippingDetails.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtRemarks
            // 
            this.txtRemarks.Location = new System.Drawing.Point(826, 172);
            this.txtRemarks.MaxLength = 200;
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Size = new System.Drawing.Size(140, 21);
            this.txtRemarks.TabIndex = 6;
            // 
            // lblPODetails
            // 
            this.lblPODetails.Location = new System.Drawing.Point(695, 172);
            this.lblPODetails.Name = "lblPODetails";
            this.lblPODetails.Size = new System.Drawing.Size(125, 13);
            this.lblPODetails.TabIndex = 53;
            this.lblPODetails.Text = "Remarks:";
            this.lblPODetails.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPaymentDetails
            // 
            this.txtPaymentDetails.Location = new System.Drawing.Point(826, 203);
            this.txtPaymentDetails.MaxLength = 100;
            this.txtPaymentDetails.Multiline = true;
            this.txtPaymentDetails.Name = "txtPaymentDetails";
            this.txtPaymentDetails.Size = new System.Drawing.Size(140, 50);
            this.txtPaymentDetails.TabIndex = 8;
            // 
            // lblPaymentDetails
            // 
            this.lblPaymentDetails.Location = new System.Drawing.Point(695, 206);
            this.lblPaymentDetails.Name = "lblPaymentDetails";
            this.lblPaymentDetails.Size = new System.Drawing.Size(125, 13);
            this.lblPaymentDetails.TabIndex = 55;
            this.lblPaymentDetails.Text = "Payment Details:";
            this.lblPaymentDetails.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblFormCApplicable
            // 
            this.lblFormCApplicable.Location = new System.Drawing.Point(319, 172);
            this.lblFormCApplicable.Name = "lblFormCApplicable";
            this.lblFormCApplicable.Size = new System.Drawing.Size(125, 13);
            this.lblFormCApplicable.TabIndex = 57;
            this.lblFormCApplicable.Text = "Form C Applicable:";
            this.lblFormCApplicable.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkFormCApplicable
            // 
            this.chkFormCApplicable.AutoSize = true;
            this.chkFormCApplicable.Location = new System.Drawing.Point(479, 172);
            this.chkFormCApplicable.Name = "chkFormCApplicable";
            this.chkFormCApplicable.Size = new System.Drawing.Size(15, 14);
            this.chkFormCApplicable.TabIndex = 5;
            this.chkFormCApplicable.UseVisualStyleBackColor = true;
            this.chkFormCApplicable.CheckedChanged += new System.EventHandler(this.chkFormCApplicable_CheckedChanged);
            // 
            // lblTotalTaxAmount
            // 
            this.lblTotalTaxAmount.Location = new System.Drawing.Point(63, 203);
            this.lblTotalTaxAmount.Name = "lblTotalTaxAmount";
            this.lblTotalTaxAmount.Size = new System.Drawing.Size(125, 13);
            this.lblTotalTaxAmount.TabIndex = 59;
            this.lblTotalTaxAmount.Text = "Total Tax Amount:";
            this.lblTotalTaxAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotalPOAmount
            // 
            this.lblTotalPOAmount.Location = new System.Drawing.Point(13, 233);
            this.lblTotalPOAmount.Name = "lblTotalPOAmount";
            this.lblTotalPOAmount.Size = new System.Drawing.Size(175, 13);
            this.lblTotalPOAmount.TabIndex = 61;
            this.lblTotalPOAmount.Text = "Total PO Amount(incl. Tax):";
            this.lblTotalPOAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbPOType
            // 
            this.cmbPOType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPOType.FormattingEnabled = true;
            this.cmbPOType.Location = new System.Drawing.Point(826, 19);
            this.cmbPOType.Name = "cmbPOType";
            this.cmbPOType.Size = new System.Drawing.Size(140, 21);
            this.cmbPOType.TabIndex = 0;
            this.cmbPOType.TabStop = false;
            // 
            // lblPOType
            // 
            this.lblPOType.Location = new System.Drawing.Point(685, 19);
            this.lblPOType.Name = "lblPOType";
            this.lblPOType.Size = new System.Drawing.Size(135, 13);
            this.lblPOType.TabIndex = 63;
            this.lblPOType.Text = "PO Type:*";
            this.lblPOType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtItemCode
            // 
            this.txtItemCode.Location = new System.Drawing.Point(192, 13);
            this.txtItemCode.MaxLength = 20;
            this.txtItemCode.Name = "txtItemCode";
            this.txtItemCode.Size = new System.Drawing.Size(140, 21);
            this.txtItemCode.TabIndex = 0;
            this.txtItemCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtItemCode_KeyDown);
            this.txtItemCode.Validating += new System.ComponentModel.CancelEventHandler(this.txtItemCode_Validating);
            // 
            // lblItemCode
            // 
            this.lblItemCode.Location = new System.Drawing.Point(61, 16);
            this.lblItemCode.Name = "lblItemCode";
            this.lblItemCode.Size = new System.Drawing.Size(125, 13);
            this.lblItemCode.TabIndex = 41;
            this.lblItemCode.Text = "Item Code:*";
            this.lblItemCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtItemName
            // 
            this.txtItemName.Location = new System.Drawing.Point(526, 8);
            this.txtItemName.MaxLength = 100;
            this.txtItemName.Name = "txtItemName";
            this.txtItemName.ReadOnly = true;
            this.txtItemName.Size = new System.Drawing.Size(140, 21);
            this.txtItemName.TabIndex = 44;
            this.txtItemName.TabStop = false;
            // 
            // lblItemName
            // 
            this.lblItemName.Location = new System.Drawing.Point(395, 11);
            this.lblItemName.Name = "lblItemName";
            this.lblItemName.Size = new System.Drawing.Size(125, 13);
            this.lblItemName.TabIndex = 43;
            this.lblItemName.Text = "Item Name:";
            this.lblItemName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtItemUOM
            // 
            this.txtItemUOM.Location = new System.Drawing.Point(795, 8);
            this.txtItemUOM.MaxLength = 20;
            this.txtItemUOM.Name = "txtItemUOM";
            this.txtItemUOM.ReadOnly = true;
            this.txtItemUOM.Size = new System.Drawing.Size(140, 21);
            this.txtItemUOM.TabIndex = 46;
            this.txtItemUOM.TabStop = false;
            // 
            // lblItemUOM
            // 
            this.lblItemUOM.Location = new System.Drawing.Point(664, 11);
            this.lblItemUOM.Name = "lblItemUOM";
            this.lblItemUOM.Size = new System.Drawing.Size(125, 13);
            this.lblItemUOM.TabIndex = 45;
            this.lblItemUOM.Text = "Item UOM:";
            this.lblItemUOM.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTaxGroupCode
            // 
            this.lblTaxGroupCode.Location = new System.Drawing.Point(51, 43);
            this.lblTaxGroupCode.Name = "lblTaxGroupCode";
            this.lblTaxGroupCode.Size = new System.Drawing.Size(135, 13);
            this.lblTaxGroupCode.TabIndex = 65;
            this.lblTaxGroupCode.Text = "Tax Group Code:";
            this.lblTaxGroupCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtUnitPrice
            // 
            this.txtUnitPrice.Location = new System.Drawing.Point(526, 35);
            this.txtUnitPrice.MaxLength = 10;
            this.txtUnitPrice.Name = "txtUnitPrice";
            this.txtUnitPrice.ReadOnly = true;
            this.txtUnitPrice.Size = new System.Drawing.Size(140, 21);
            this.txtUnitPrice.TabIndex = 68;
            this.txtUnitPrice.TabStop = false;
            // 
            // lblUnitPrice
            // 
            this.lblUnitPrice.Location = new System.Drawing.Point(395, 38);
            this.lblUnitPrice.Name = "lblUnitPrice";
            this.lblUnitPrice.Size = new System.Drawing.Size(125, 13);
            this.lblUnitPrice.TabIndex = 67;
            this.lblUnitPrice.Text = "Unit Price:";
            this.lblUnitPrice.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtUnitQty
            // 
            this.txtUnitQty.Location = new System.Drawing.Point(795, 35);
            this.txtUnitQty.MaxLength = 12;
            this.txtUnitQty.Name = "txtUnitQty";
            this.txtUnitQty.Size = new System.Drawing.Size(140, 21);
            this.txtUnitQty.TabIndex = 2;
            this.txtUnitQty.TextChanged += new System.EventHandler(this.txtUnitQty_TextChanged);
            // 
            // lblUnitQty
            // 
            this.lblUnitQty.Location = new System.Drawing.Point(664, 38);
            this.lblUnitQty.Name = "lblUnitQty";
            this.lblUnitQty.Size = new System.Drawing.Size(125, 13);
            this.lblUnitQty.TabIndex = 69;
            this.lblUnitQty.Text = "Unit Qty:*";
            this.lblUnitQty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtItemTotalTax
            // 
            this.txtItemTotalTax.Location = new System.Drawing.Point(192, 67);
            this.txtItemTotalTax.MaxLength = 12;
            this.txtItemTotalTax.Name = "txtItemTotalTax";
            this.txtItemTotalTax.ReadOnly = true;
            this.txtItemTotalTax.Size = new System.Drawing.Size(140, 21);
            this.txtItemTotalTax.TabIndex = 72;
            this.txtItemTotalTax.TabStop = false;
            // 
            // lblItemTotalTax
            // 
            this.lblItemTotalTax.Location = new System.Drawing.Point(61, 70);
            this.lblItemTotalTax.Name = "lblItemTotalTax";
            this.lblItemTotalTax.Size = new System.Drawing.Size(125, 13);
            this.lblItemTotalTax.TabIndex = 71;
            this.lblItemTotalTax.Text = "Total Tax:";
            this.lblItemTotalTax.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnAddItem
            // 
            this.btnAddItem.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAddItem.BackgroundImage")));
            this.btnAddItem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAddItem.FlatAppearance.BorderSize = 0;
            this.btnAddItem.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnAddItem.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnAddItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddItem.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnAddItem.Location = new System.Drawing.Point(784, 72);
            this.btnAddItem.Name = "btnAddItem";
            this.btnAddItem.Size = new System.Drawing.Size(75, 32);
            this.btnAddItem.TabIndex = 3;
            this.btnAddItem.Text = "&Add";
            this.btnAddItem.UseVisualStyleBackColor = false;
            this.btnAddItem.Click += new System.EventHandler(this.btnAddItem_Click);
            // 
            // btnClearItem
            // 
            this.btnClearItem.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearItem.BackgroundImage")));
            this.btnClearItem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearItem.FlatAppearance.BorderSize = 0;
            this.btnClearItem.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnClearItem.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnClearItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearItem.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnClearItem.Location = new System.Drawing.Point(865, 72);
            this.btnClearItem.Name = "btnClearItem";
            this.btnClearItem.Size = new System.Drawing.Size(75, 32);
            this.btnClearItem.TabIndex = 4;
            this.btnClearItem.Text = "C&lear";
            this.btnClearItem.UseVisualStyleBackColor = false;
            this.btnClearItem.Click += new System.EventHandler(this.btnClearItem_Click);
            // 
            // txtItemTotalAmount
            // 
            this.txtItemTotalAmount.Location = new System.Drawing.Point(526, 62);
            this.txtItemTotalAmount.MaxLength = 12;
            this.txtItemTotalAmount.Name = "txtItemTotalAmount";
            this.txtItemTotalAmount.ReadOnly = true;
            this.txtItemTotalAmount.Size = new System.Drawing.Size(140, 21);
            this.txtItemTotalAmount.TabIndex = 75;
            this.txtItemTotalAmount.TabStop = false;
            // 
            // lblItemTotalAmount
            // 
            this.lblItemTotalAmount.Location = new System.Drawing.Point(395, 65);
            this.lblItemTotalAmount.Name = "lblItemTotalAmount";
            this.lblItemTotalAmount.Size = new System.Drawing.Size(125, 13);
            this.lblItemTotalAmount.TabIndex = 74;
            this.lblItemTotalAmount.Text = "Amount [Excl.Tax]:";
            this.lblItemTotalAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dgvPOItems
            // 
            this.dgvPOItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPOItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPOItems.Location = new System.Drawing.Point(0, 154);
            this.dgvPOItems.Name = "dgvPOItems";
            this.dgvPOItems.RowHeadersVisible = false;
            this.dgvPOItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPOItems.Size = new System.Drawing.Size(1005, 132);
            this.dgvPOItems.TabIndex = 2;
            this.dgvPOItems.TabStop = false;
            this.dgvPOItems.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPOItems_CellClick);
            this.dgvPOItems.SelectionChanged += new System.EventHandler(this.dgvPOItems_SelectionChanged);
            // 
            // lblSearchPONumber
            // 
            this.lblSearchPONumber.Location = new System.Drawing.Point(54, 27);
            this.lblSearchPONumber.Name = "lblSearchPONumber";
            this.lblSearchPONumber.Size = new System.Drawing.Size(125, 13);
            this.lblSearchPONumber.TabIndex = 30;
            this.lblSearchPONumber.Text = "PO Number:";
            this.lblSearchPONumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSearchPONumber
            // 
            this.txtSearchPONumber.Location = new System.Drawing.Point(185, 24);
            this.txtSearchPONumber.MaxLength = 20;
            this.txtSearchPONumber.Name = "txtSearchPONumber";
            this.txtSearchPONumber.Size = new System.Drawing.Size(140, 21);
            this.txtSearchPONumber.TabIndex = 0;
            // 
            // cmbSearchVendorCode
            // 
            this.cmbSearchVendorCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSearchVendorCode.FormattingEnabled = true;
            this.cmbSearchVendorCode.Location = new System.Drawing.Point(783, 24);
            this.cmbSearchVendorCode.Name = "cmbSearchVendorCode";
            this.cmbSearchVendorCode.Size = new System.Drawing.Size(140, 21);
            this.cmbSearchVendorCode.TabIndex = 2;
            // 
            // lblSearchVendorCode
            // 
            this.lblSearchVendorCode.Location = new System.Drawing.Point(642, 27);
            this.lblSearchVendorCode.Name = "lblSearchVendorCode";
            this.lblSearchVendorCode.Size = new System.Drawing.Size(135, 13);
            this.lblSearchVendorCode.TabIndex = 35;
            this.lblSearchVendorCode.Text = "Vendor Code:";
            this.lblSearchVendorCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpSearchFromPODate
            // 
            this.dtpSearchFromPODate.Checked = false;
            this.dtpSearchFromPODate.CustomFormat = "dd-MM-yyyy";
            this.dtpSearchFromPODate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpSearchFromPODate.Location = new System.Drawing.Point(185, 55);
            this.dtpSearchFromPODate.Name = "dtpSearchFromPODate";
            this.dtpSearchFromPODate.ShowCheckBox = true;
            this.dtpSearchFromPODate.Size = new System.Drawing.Size(140, 21);
            this.dtpSearchFromPODate.TabIndex = 3;
            // 
            // lblSearchFromPODate
            // 
            this.lblSearchFromPODate.Location = new System.Drawing.Point(44, 59);
            this.lblSearchFromPODate.Name = "lblSearchFromPODate";
            this.lblSearchFromPODate.Size = new System.Drawing.Size(135, 13);
            this.lblSearchFromPODate.TabIndex = 47;
            this.lblSearchFromPODate.Text = "From PO Date:";
            this.lblSearchFromPODate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpSearchToPODate
            // 
            this.dtpSearchToPODate.Checked = false;
            this.dtpSearchToPODate.CustomFormat = "dd-MM-yyyy";
            this.dtpSearchToPODate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpSearchToPODate.Location = new System.Drawing.Point(474, 55);
            this.dtpSearchToPODate.Name = "dtpSearchToPODate";
            this.dtpSearchToPODate.ShowCheckBox = true;
            this.dtpSearchToPODate.Size = new System.Drawing.Size(140, 21);
            this.dtpSearchToPODate.TabIndex = 4;
            // 
            // lblSearchToPODate
            // 
            this.lblSearchToPODate.Location = new System.Drawing.Point(333, 55);
            this.lblSearchToPODate.Name = "lblSearchToPODate";
            this.lblSearchToPODate.Size = new System.Drawing.Size(135, 13);
            this.lblSearchToPODate.TabIndex = 49;
            this.lblSearchToPODate.Text = "To PO Date:";
            this.lblSearchToPODate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbSearchStatus
            // 
            this.cmbSearchStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSearchStatus.FormattingEnabled = true;
            this.cmbSearchStatus.Location = new System.Drawing.Point(474, 24);
            this.cmbSearchStatus.Name = "cmbSearchStatus";
            this.cmbSearchStatus.Size = new System.Drawing.Size(140, 21);
            this.cmbSearchStatus.TabIndex = 1;
            // 
            // lblSearchStatus
            // 
            this.lblSearchStatus.AutoSize = true;
            this.lblSearchStatus.Location = new System.Drawing.Point(420, 24);
            this.lblSearchStatus.Name = "lblSearchStatus";
            this.lblSearchStatus.Size = new System.Drawing.Size(48, 13);
            this.lblSearchStatus.TabIndex = 59;
            this.lblSearchStatus.Text = "Status:";
            // 
            // cmbSearchDestinationLocation
            // 
            this.cmbSearchDestinationLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSearchDestinationLocation.FormattingEnabled = true;
            this.cmbSearchDestinationLocation.Location = new System.Drawing.Point(783, 56);
            this.cmbSearchDestinationLocation.Name = "cmbSearchDestinationLocation";
            this.cmbSearchDestinationLocation.Size = new System.Drawing.Size(140, 21);
            this.cmbSearchDestinationLocation.TabIndex = 5;
            // 
            // lblSearchDestinationLocation
            // 
            this.lblSearchDestinationLocation.Location = new System.Drawing.Point(642, 59);
            this.lblSearchDestinationLocation.Name = "lblSearchDestinationLocation";
            this.lblSearchDestinationLocation.Size = new System.Drawing.Size(135, 13);
            this.lblSearchDestinationLocation.TabIndex = 61;
            this.lblSearchDestinationLocation.Text = "Destination Location:";
            this.lblSearchDestinationLocation.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSearchFormCApplicable
            // 
            this.lblSearchFormCApplicable.Location = new System.Drawing.Point(54, 89);
            this.lblSearchFormCApplicable.Name = "lblSearchFormCApplicable";
            this.lblSearchFormCApplicable.Size = new System.Drawing.Size(125, 13);
            this.lblSearchFormCApplicable.TabIndex = 67;
            this.lblSearchFormCApplicable.Text = "Form C Applicable:";
            this.lblSearchFormCApplicable.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkSearchFormCApplicable
            // 
            this.chkSearchFormCApplicable.AutoSize = true;
            this.chkSearchFormCApplicable.Location = new System.Drawing.Point(185, 89);
            this.chkSearchFormCApplicable.Name = "chkSearchFormCApplicable";
            this.chkSearchFormCApplicable.Size = new System.Drawing.Size(15, 14);
            this.chkSearchFormCApplicable.TabIndex = 6;
            this.chkSearchFormCApplicable.UseVisualStyleBackColor = true;
            // 
            // txtTotalTaxAmount
            // 
            this.txtTotalTaxAmount.Location = new System.Drawing.Point(194, 203);
            this.txtTotalTaxAmount.MaxLength = 12;
            this.txtTotalTaxAmount.Name = "txtTotalTaxAmount";
            this.txtTotalTaxAmount.ReadOnly = true;
            this.txtTotalTaxAmount.Size = new System.Drawing.Size(140, 21);
            this.txtTotalTaxAmount.TabIndex = 76;
            this.txtTotalTaxAmount.TabStop = false;
            // 
            // txtTotalPOAmount
            // 
            this.txtTotalPOAmount.Location = new System.Drawing.Point(194, 233);
            this.txtTotalPOAmount.MaxLength = 12;
            this.txtTotalPOAmount.Name = "txtTotalPOAmount";
            this.txtTotalPOAmount.ReadOnly = true;
            this.txtTotalPOAmount.Size = new System.Drawing.Size(140, 21);
            this.txtTotalPOAmount.TabIndex = 77;
            this.txtTotalPOAmount.TabStop = false;
            // 
            // txtPOAmendmentNumber
            // 
            this.txtPOAmendmentNumber.Location = new System.Drawing.Point(502, 16);
            this.txtPOAmendmentNumber.MaxLength = 100;
            this.txtPOAmendmentNumber.Name = "txtPOAmendmentNumber";
            this.txtPOAmendmentNumber.ReadOnly = true;
            this.txtPOAmendmentNumber.Size = new System.Drawing.Size(140, 21);
            this.txtPOAmendmentNumber.TabIndex = 78;
            this.txtPOAmendmentNumber.TabStop = false;
            // 
            // txtPONumber
            // 
            this.txtPONumber.Location = new System.Drawing.Point(194, 16);
            this.txtPONumber.MaxLength = 100;
            this.txtPONumber.Name = "txtPONumber";
            this.txtPONumber.ReadOnly = true;
            this.txtPONumber.Size = new System.Drawing.Size(140, 21);
            this.txtPONumber.TabIndex = 79;
            this.txtPONumber.TabStop = false;
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
            this.btnPrint.Location = new System.Drawing.Point(930, 0);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 32);
            this.btnPrint.TabIndex = 6;
            this.btnPrint.Text = "&Print";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnAmend
            // 
            this.btnAmend.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAmend.BackgroundImage")));
            this.btnAmend.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAmend.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnAmend.FlatAppearance.BorderSize = 0;
            this.btnAmend.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnAmend.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnAmend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAmend.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnAmend.Location = new System.Drawing.Point(665, 0);
            this.btnAmend.Name = "btnAmend";
            this.btnAmend.Size = new System.Drawing.Size(75, 32);
            this.btnAmend.TabIndex = 3;
            this.btnAmend.Text = "Ame&nd";
            this.btnAmend.UseVisualStyleBackColor = false;
            this.btnAmend.Click += new System.EventHandler(this.btn_amend_Click);
            // 
            // lblCreationDate
            // 
            this.lblCreationDate.Location = new System.Drawing.Point(38, 109);
            this.lblCreationDate.Name = "lblCreationDate";
            this.lblCreationDate.Size = new System.Drawing.Size(150, 13);
            this.lblCreationDate.TabIndex = 81;
            this.lblCreationDate.Text = "Created Date:";
            this.lblCreationDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCreatedDateValue
            // 
            this.lblCreatedDateValue.Location = new System.Drawing.Point(189, 109);
            this.lblCreatedDateValue.Name = "lblCreatedDateValue";
            this.lblCreatedDateValue.Size = new System.Drawing.Size(140, 13);
            this.lblCreatedDateValue.TabIndex = 82;
            this.lblCreatedDateValue.Text = "Date";
            this.lblCreatedDateValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStatus
            // 
            this.lblStatus.Location = new System.Drawing.Point(53, 172);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(135, 13);
            this.lblStatus.TabIndex = 83;
            this.lblStatus.Text = "Status:";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblStatusValue
            // 
            this.lblStatusValue.Location = new System.Drawing.Point(191, 172);
            this.lblStatusValue.Name = "lblStatusValue";
            this.lblStatusValue.Size = new System.Drawing.Size(140, 13);
            this.lblStatusValue.TabIndex = 84;
            this.lblStatusValue.Text = "New";
            this.lblStatusValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // errorCreatePO
            // 
            this.errorCreatePO.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorCreatePO.ContainerControl = this;
            // 
            // txtTaxGroupCode
            // 
            this.txtTaxGroupCode.Location = new System.Drawing.Point(192, 40);
            this.txtTaxGroupCode.MaxLength = 12;
            this.txtTaxGroupCode.Name = "txtTaxGroupCode";
            this.txtTaxGroupCode.ReadOnly = true;
            this.txtTaxGroupCode.Size = new System.Drawing.Size(140, 21);
            this.txtTaxGroupCode.TabIndex = 76;
            this.txtTaxGroupCode.TabStop = false;
            // 
            // dgvPoSearch
            // 
            this.dgvPoSearch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPoSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPoSearch.Location = new System.Drawing.Point(0, 0);
            this.dgvPoSearch.Name = "dgvPoSearch";
            this.dgvPoSearch.RowHeadersVisible = false;
            this.dgvPoSearch.Size = new System.Drawing.Size(1005, 437);
            this.dgvPoSearch.TabIndex = 12;
            this.dgvPoSearch.TabStop = false;
            this.dgvPoSearch.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgvPoSearch_MouseDoubleClick);
            this.dgvPoSearch.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPoSearch_CellClick);
            // 
            // cmbSearchPOType
            // 
            this.cmbSearchPOType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSearchPOType.FormattingEnabled = true;
            this.cmbSearchPOType.Location = new System.Drawing.Point(473, 86);
            this.cmbSearchPOType.Name = "cmbSearchPOType";
            this.cmbSearchPOType.Size = new System.Drawing.Size(140, 21);
            this.cmbSearchPOType.TabIndex = 7;
            // 
            // lblSearchPoType
            // 
            this.lblSearchPoType.AutoSize = true;
            this.lblSearchPoType.Location = new System.Drawing.Point(404, 89);
            this.lblSearchPoType.Name = "lblSearchPoType";
            this.lblSearchPoType.Size = new System.Drawing.Size(60, 13);
            this.lblSearchPoType.TabIndex = 69;
            this.lblSearchPoType.Text = "PO Type:";
            // 
            // errorSearch
            // 
            this.errorSearch.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorSearch.ContainerControl = this;
            // 
            // lblPODateValue
            // 
            this.lblPODateValue.Location = new System.Drawing.Point(189, 141);
            this.lblPODateValue.Name = "lblPODateValue";
            this.lblPODateValue.Size = new System.Drawing.Size(140, 13);
            this.lblPODateValue.TabIndex = 83;
            this.lblPODateValue.Text = "Date";
            this.lblPODateValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::PurchaseComponent.Properties.Resources.find;
            this.pictureBox1.Location = new System.Drawing.Point(338, 13);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(21, 21);
            this.pictureBox1.TabIndex = 91;
            this.pictureBox1.TabStop = false;
            // 
            // lblVendorCodeValue
            // 
            this.lblVendorCodeValue.Location = new System.Drawing.Point(194, 46);
            this.lblVendorCodeValue.Name = "lblVendorCodeValue";
            this.lblVendorCodeValue.Size = new System.Drawing.Size(150, 13);
            this.lblVendorCodeValue.TabIndex = 85;
            this.lblVendorCodeValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDestinationCodeValue
            // 
            this.lblDestinationCodeValue.Location = new System.Drawing.Point(826, 49);
            this.lblDestinationCodeValue.Name = "lblDestinationCodeValue";
            this.lblDestinationCodeValue.Size = new System.Drawing.Size(135, 13);
            this.lblDestinationCodeValue.TabIndex = 86;
            this.lblDestinationCodeValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtttlAmount
            // 
            this.txtttlAmount.Location = new System.Drawing.Point(192, 92);
            this.txtttlAmount.MaxLength = 12;
            this.txtttlAmount.Name = "txtttlAmount";
            this.txtttlAmount.ReadOnly = true;
            this.txtttlAmount.Size = new System.Drawing.Size(140, 21);
            this.txtttlAmount.TabIndex = 93;
            this.txtttlAmount.TabStop = false;
            // 
            // lblttlAmount
            // 
            this.lblttlAmount.Location = new System.Drawing.Point(61, 95);
            this.lblttlAmount.Name = "lblttlAmount";
            this.lblttlAmount.Size = new System.Drawing.Size(125, 13);
            this.lblttlAmount.TabIndex = 92;
            this.lblttlAmount.Text = " Amount [Incl.Tax]:";
            this.lblttlAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkIsImported
            // 
            this.chkIsImported.AutoSize = true;
            this.chkIsImported.Location = new System.Drawing.Point(677, 172);
            this.chkIsImported.Name = "chkIsImported";
            this.chkIsImported.Size = new System.Drawing.Size(15, 14);
            this.chkIsImported.TabIndex = 87;
            this.chkIsImported.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(514, 170);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 13);
            this.label1.TabIndex = 88;
            this.label1.Text = "Is Imported";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // frmPurchaseOrder
            // 
            this.AcceptButton = this.btnSearch;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1013, 703);
            this.Name = "frmPurchaseOrder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Purchase Order";
            this.WindowState = System.Windows.Forms.FormWindowState.Normal;
            this.Load += new System.EventHandler(this.frmPurchaseOrder_Load);
            this.pnlCreateHeader.ResumeLayout(false);
            this.pnlCreateHeader.PerformLayout();
            this.grpAddDetails.ResumeLayout(false);
            this.pnlSearchHeader.ResumeLayout(false);
            this.pnlSearchHeader.PerformLayout();
            this.pnlSearchGrid.ResumeLayout(false);
            this.pnlCreateDetail.ResumeLayout(false);
            this.pnlCreateDetail.PerformLayout();
            this.pnlLowerButtons.ResumeLayout(false);
            this.pnlTopButtons.ResumeLayout(false);
            this.pnlSearchButtons.ResumeLayout(false);
            this.tabSearch.ResumeLayout(false);
            this.tabCreate.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPOItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorCreatePO)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPoSearch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorSearch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblPONumber;
        private System.Windows.Forms.Label lblPOAmendmentNumber;
        private System.Windows.Forms.Label lblVendorCode;
        private System.Windows.Forms.ComboBox cmbVendorCode;
        private System.Windows.Forms.Label lblVendorName;
        private System.Windows.Forms.TextBox txtVendorName;
        private System.Windows.Forms.Label lblVendorAddress;
        private System.Windows.Forms.TextBox txtVendorAddress;
        private System.Windows.Forms.TextBox txtPaymentTerms;
        private System.Windows.Forms.Label lblPaymentTerms;
        private System.Windows.Forms.ComboBox cmbDestinationLocation;
        private System.Windows.Forms.Label lblDestinationLocation;
        private System.Windows.Forms.Label lblDestinationAddress;
        private System.Windows.Forms.TextBox txtDestinationAddress;
        private System.Windows.Forms.Label lblPODate;
        private System.Windows.Forms.DateTimePicker dtpExpDeliveryDate;
        private System.Windows.Forms.Label lblExpDeliveryDate;
        private System.Windows.Forms.DateTimePicker dtpMaxDeliveryDate;
        private System.Windows.Forms.Label lblMaxDeliveryDate;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnShortClosed;
        private System.Windows.Forms.TextBox txtShippingDetails;
        private System.Windows.Forms.Label lblShippingDetails;
        private System.Windows.Forms.TextBox txtRemarks;
        private System.Windows.Forms.Label lblPODetails;
        private System.Windows.Forms.TextBox txtPaymentDetails;
        private System.Windows.Forms.Label lblPaymentDetails;
        private System.Windows.Forms.Label lblFormCApplicable;
        private System.Windows.Forms.CheckBox chkFormCApplicable;
        private System.Windows.Forms.Label lblTotalTaxAmount;
        private System.Windows.Forms.Label lblTotalPOAmount;
        private System.Windows.Forms.ComboBox cmbPOType;
        private System.Windows.Forms.Label lblPOType;
        private System.Windows.Forms.TextBox txtItemCode;
        private System.Windows.Forms.Label lblItemCode;
        private System.Windows.Forms.TextBox txtItemName;
        private System.Windows.Forms.Label lblItemName;
        private System.Windows.Forms.TextBox txtItemUOM;
        private System.Windows.Forms.Label lblItemUOM;
        private System.Windows.Forms.Label lblTaxGroupCode;
        private System.Windows.Forms.TextBox txtUnitQty;
        private System.Windows.Forms.Label lblUnitQty;
        private System.Windows.Forms.TextBox txtUnitPrice;
        private System.Windows.Forms.Label lblUnitPrice;
        private System.Windows.Forms.Button btnClearItem;
        private System.Windows.Forms.Button btnAddItem;
        private System.Windows.Forms.TextBox txtItemTotalTax;
        private System.Windows.Forms.Label lblItemTotalTax;
        private System.Windows.Forms.TextBox txtItemTotalAmount;
        private System.Windows.Forms.Label lblItemTotalAmount;
        private System.Windows.Forms.DataGridView dgvPOItems;
        private System.Windows.Forms.TextBox txtSearchPONumber;
        private System.Windows.Forms.Label lblSearchPONumber;
        private System.Windows.Forms.ComboBox cmbSearchVendorCode;
        private System.Windows.Forms.Label lblSearchVendorCode;
        private System.Windows.Forms.DateTimePicker dtpSearchFromPODate;
        private System.Windows.Forms.Label lblSearchFromPODate;
        private System.Windows.Forms.DateTimePicker dtpSearchToPODate;
        private System.Windows.Forms.Label lblSearchToPODate;
        private System.Windows.Forms.ComboBox cmbSearchStatus;
        private System.Windows.Forms.Label lblSearchStatus;
        private System.Windows.Forms.ComboBox cmbSearchDestinationLocation;
        private System.Windows.Forms.Label lblSearchDestinationLocation;
        private System.Windows.Forms.Label lblSearchFormCApplicable;
        private System.Windows.Forms.CheckBox chkSearchFormCApplicable;
        private System.Windows.Forms.TextBox txtTotalPOAmount;
        private System.Windows.Forms.TextBox txtTotalTaxAmount;
        private System.Windows.Forms.TextBox txtPONumber;
        private System.Windows.Forms.TextBox txtPOAmendmentNumber;
        private System.Windows.Forms.Label lblCreatedDateValue;
        private System.Windows.Forms.Label lblCreationDate;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblStatusValue;
        private System.Windows.Forms.ErrorProvider errorCreatePO;
        private System.Windows.Forms.DataGridView dgvPoSearch;
        private System.Windows.Forms.Button btnAmend;
        private System.Windows.Forms.TextBox txtTaxGroupCode;
        private System.Windows.Forms.ComboBox cmbSearchPOType;
        private System.Windows.Forms.Label lblSearchPoType;
        private System.Windows.Forms.ErrorProvider errorSearch;
        //private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Label lblPODateValue;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblVendorCodeValue;
        private System.Windows.Forms.Label lblDestinationCodeValue;
        private System.Windows.Forms.TextBox txtttlAmount;
        private System.Windows.Forms.Label lblttlAmount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkIsImported;

    }
}