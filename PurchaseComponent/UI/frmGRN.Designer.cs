namespace PurchaseComponent.UI
{
    partial class frmGRN
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGRN));
            this.txtPONumber = new System.Windows.Forms.TextBox();
            this.lblPONumber = new System.Windows.Forms.Label();
            this.lblPOAmendmentNumner = new System.Windows.Forms.Label();
            this.txtPOAmendmentNumber = new System.Windows.Forms.TextBox();
            this.lblPODate = new System.Windows.Forms.Label();
            this.lblGrnNo = new System.Windows.Forms.Label();
            this.txtVendorCode = new System.Windows.Forms.TextBox();
            this.lblVendorCode = new System.Windows.Forms.Label();
            this.txtVendorName = new System.Windows.Forms.TextBox();
            this.lblVendorName = new System.Windows.Forms.Label();
            this.txtChallanNumber = new System.Windows.Forms.TextBox();
            this.lblChallanNumber = new System.Windows.Forms.Label();
            this.lblChallanDate = new System.Windows.Forms.Label();
            this.dtpChallanDate = new System.Windows.Forms.DateTimePicker();
            this.txtShippingDetails = new System.Windows.Forms.TextBox();
            this.lblShippingdetails = new System.Windows.Forms.Label();
            this.txtVehicleNumber = new System.Windows.Forms.TextBox();
            this.lblVehicleNumber = new System.Windows.Forms.Label();
            this.txtReceivedBy = new System.Windows.Forms.TextBox();
            this.lblReceivedBy = new System.Windows.Forms.Label();
            this.txtDestLocation = new System.Windows.Forms.TextBox();
            this.lblDestLocation = new System.Windows.Forms.Label();
            this.lblGRNDate = new System.Windows.Forms.Label();
            this.dtpInvoiceDate = new System.Windows.Forms.DateTimePicker();
            this.lblInvoiceDate = new System.Windows.Forms.Label();
            this.txtInvoiceNumber = new System.Windows.Forms.TextBox();
            this.lblInvoiceNumber = new System.Windows.Forms.Label();
            this.txtGrossWeight = new System.Windows.Forms.TextBox();
            this.lblGrossWeight = new System.Windows.Forms.Label();
            this.txtNoOfBoxes = new System.Windows.Forms.TextBox();
            this.lblNoOfBoxes = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtSearchGRNNumber = new System.Windows.Forms.TextBox();
            this.lblSearchGRNNumber = new System.Windows.Forms.Label();
            this.txtSearchPONumber = new System.Windows.Forms.TextBox();
            this.lblSearchPONumber = new System.Windows.Forms.Label();
            this.cmbSearchVendorCode = new System.Windows.Forms.ComboBox();
            this.lblSearchVendorCode = new System.Windows.Forms.Label();
            this.dtpSearchfrmPODate = new System.Windows.Forms.DateTimePicker();
            this.lblSearchfrmPODate = new System.Windows.Forms.Label();
            this.dtpSearchToPODate = new System.Windows.Forms.DateTimePicker();
            this.lblSearchToPODate = new System.Windows.Forms.Label();
            this.dtpSearchToGRNDate = new System.Windows.Forms.DateTimePicker();
            this.lblSearchToGRNDate = new System.Windows.Forms.Label();
            this.dtpSearchFrmGRNDate = new System.Windows.Forms.DateTimePicker();
            this.lblSearchFrmGRNDate = new System.Windows.Forms.Label();
            this.txtSearchReceivedBy = new System.Windows.Forms.TextBox();
            this.lblSearchReceivedBy = new System.Windows.Forms.Label();
            this.cmbSearchStatus = new System.Windows.Forms.ComboBox();
            this.lblSearchStatus = new System.Windows.Forms.Label();
            this.dgvSearchGRN = new System.Windows.Forms.DataGridView();
            this.btnClosed = new System.Windows.Forms.Button();
            this.errorSearch = new System.Windows.Forms.ErrorProvider(this.components);
            this.lblCreateGrnDate = new System.Windows.Forms.Label();
            this.lblGrnNoValue = new System.Windows.Forms.Label();
            this.lblStatusValue = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblPODateValue = new System.Windows.Forms.Label();
            this.pnlButton = new System.Windows.Forms.Panel();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.dgvGRNItems = new System.Windows.Forms.DataGridView();
            this.errorCreate = new System.Windows.Forms.ErrorProvider(this.components);
            this.txtInvoiceTax = new System.Windows.Forms.TextBox();
            this.lblInvoiceTax = new System.Windows.Forms.Label();
            this.txtInvoiceAmount = new System.Windows.Forms.TextBox();
            this.lblInvoiceAmount = new System.Windows.Forms.Label();
            this.cmbSearchDestinationLocation = new System.Windows.Forms.ComboBox();
            this.lblSearchDestinationLocation = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pnlCreateHeader.SuspendLayout();
            this.grpAddDetails.SuspendLayout();
            this.pnlSearchHeader.SuspendLayout();
            this.pnlSearchGrid.SuspendLayout();
            this.pnlLowerButtons.SuspendLayout();
            this.pnlTopButtons.SuspendLayout();
            this.pnlSearchButtons.SuspendLayout();
            this.tabSearch.SuspendLayout();
            this.tabCreate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearchGRN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorSearch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGRNItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorCreate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCreateHeader
            // 
            this.pnlCreateHeader.Controls.Add(this.pictureBox1);
            this.pnlCreateHeader.Controls.Add(this.txtInvoiceAmount);
            this.pnlCreateHeader.Controls.Add(this.lblInvoiceAmount);
            this.pnlCreateHeader.Controls.Add(this.txtInvoiceTax);
            this.pnlCreateHeader.Controls.Add(this.lblInvoiceTax);
            this.pnlCreateHeader.Controls.Add(this.lblPODateValue);
            this.pnlCreateHeader.Controls.Add(this.lblStatusValue);
            this.pnlCreateHeader.Controls.Add(this.lblStatus);
            this.pnlCreateHeader.Controls.Add(this.lblGrnNoValue);
            this.pnlCreateHeader.Controls.Add(this.lblCreateGrnDate);
            this.pnlCreateHeader.Controls.Add(this.txtNoOfBoxes);
            this.pnlCreateHeader.Controls.Add(this.lblNoOfBoxes);
            this.pnlCreateHeader.Controls.Add(this.txtGrossWeight);
            this.pnlCreateHeader.Controls.Add(this.lblGrossWeight);
            this.pnlCreateHeader.Controls.Add(this.dtpInvoiceDate);
            this.pnlCreateHeader.Controls.Add(this.lblInvoiceDate);
            this.pnlCreateHeader.Controls.Add(this.txtInvoiceNumber);
            this.pnlCreateHeader.Controls.Add(this.lblInvoiceNumber);
            this.pnlCreateHeader.Controls.Add(this.lblGRNDate);
            this.pnlCreateHeader.Controls.Add(this.txtDestLocation);
            this.pnlCreateHeader.Controls.Add(this.lblDestLocation);
            this.pnlCreateHeader.Controls.Add(this.txtReceivedBy);
            this.pnlCreateHeader.Controls.Add(this.lblReceivedBy);
            this.pnlCreateHeader.Controls.Add(this.txtVehicleNumber);
            this.pnlCreateHeader.Controls.Add(this.lblVehicleNumber);
            this.pnlCreateHeader.Controls.Add(this.txtShippingDetails);
            this.pnlCreateHeader.Controls.Add(this.lblShippingdetails);
            this.pnlCreateHeader.Controls.Add(this.dtpChallanDate);
            this.pnlCreateHeader.Controls.Add(this.lblChallanDate);
            this.pnlCreateHeader.Controls.Add(this.txtChallanNumber);
            this.pnlCreateHeader.Controls.Add(this.lblChallanNumber);
            this.pnlCreateHeader.Controls.Add(this.txtVendorName);
            this.pnlCreateHeader.Controls.Add(this.lblVendorName);
            this.pnlCreateHeader.Controls.Add(this.txtVendorCode);
            this.pnlCreateHeader.Controls.Add(this.lblVendorCode);
            this.pnlCreateHeader.Controls.Add(this.lblGrnNo);
            this.pnlCreateHeader.Controls.Add(this.lblPODate);
            this.pnlCreateHeader.Controls.Add(this.txtPOAmendmentNumber);
            this.pnlCreateHeader.Controls.Add(this.lblPOAmendmentNumner);
            this.pnlCreateHeader.Controls.Add(this.txtPONumber);
            this.pnlCreateHeader.Controls.Add(this.lblPONumber);
            this.pnlCreateHeader.Size = new System.Drawing.Size(1005, 230);
            this.pnlCreateHeader.TabIndex = 0;
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblPONumber, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.txtPONumber, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblPOAmendmentNumner, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.txtPOAmendmentNumber, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblPODate, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblGrnNo, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblVendorCode, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.txtVendorCode, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblVendorName, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.txtVendorName, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblChallanNumber, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.txtChallanNumber, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblChallanDate, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.dtpChallanDate, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblShippingdetails, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.txtShippingDetails, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblVehicleNumber, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.txtVehicleNumber, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblReceivedBy, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.txtReceivedBy, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblDestLocation, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.txtDestLocation, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblGRNDate, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblInvoiceNumber, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.txtInvoiceNumber, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblInvoiceDate, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.dtpInvoiceDate, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblGrossWeight, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.txtGrossWeight, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblNoOfBoxes, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.txtNoOfBoxes, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblCreateGrnDate, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblGrnNoValue, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblStatus, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblStatusValue, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblPODateValue, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblInvoiceTax, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.txtInvoiceTax, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblInvoiceAmount, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.txtInvoiceAmount, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.pnlTopButtons, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.pictureBox1, 0);
            // 
            // btnCreateReset
            // 
            this.btnCreateReset.FlatAppearance.BorderSize = 0;
            this.btnCreateReset.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnCreateReset.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnCreateReset.Location = new System.Drawing.Point(705, 0);
            this.btnCreateReset.TabIndex = 1;
            this.btnCreateReset.Click += new System.EventHandler(this.Reset_Click);
            // 
            // btnSave
            // 
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSave.Location = new System.Drawing.Point(630, 0);
            this.btnSave.TabIndex = 0;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblAddDetails
            // 
            this.lblAddDetails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(117)))), ((int)(((byte)(186)))));
            this.lblAddDetails.ForeColor = System.Drawing.Color.White;
            this.lblAddDetails.Text = "Item Detail";
            // 
            // grpAddDetails
            // 
            this.grpAddDetails.Controls.Add(this.dgvGRNItems);
            this.grpAddDetails.Location = new System.Drawing.Point(0, 254);
            this.grpAddDetails.Size = new System.Drawing.Size(1005, 353);
            this.grpAddDetails.Controls.SetChildIndex(this.pnlLowerButtons, 0);
            this.grpAddDetails.Controls.SetChildIndex(this.dgvGRNItems, 0);
            this.grpAddDetails.Controls.SetChildIndex(this.pnlCreateDetail, 0);
            // 
            // btnClearDetails
            // 
            this.btnClearDetails.FlatAppearance.BorderSize = 0;
            this.btnClearDetails.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnClearDetails.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnClearDetails.Size = new System.Drawing.Size(80, 0);
            this.btnClearDetails.TabIndex = 11;
            this.btnClearDetails.TabStop = false;
            this.btnClearDetails.Text = "&Clear";
            this.btnClearDetails.Visible = false;
            // 
            // btnAddDetails
            // 
            this.btnAddDetails.FlatAppearance.BorderSize = 0;
            this.btnAddDetails.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnAddDetails.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnAddDetails.Location = new System.Drawing.Point(864, 0);
            this.btnAddDetails.Size = new System.Drawing.Size(59, 0);
            this.btnAddDetails.TabIndex = 10;
            this.btnAddDetails.TabStop = false;
            this.btnAddDetails.Visible = false;
            // 
            // pnlSearchHeader
            // 
            this.pnlSearchHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSearchHeader.Controls.Add(this.cmbSearchDestinationLocation);
            this.pnlSearchHeader.Controls.Add(this.lblSearchDestinationLocation);
            this.pnlSearchHeader.Controls.Add(this.cmbSearchStatus);
            this.pnlSearchHeader.Controls.Add(this.lblSearchStatus);
            this.pnlSearchHeader.Controls.Add(this.dtpSearchToGRNDate);
            this.pnlSearchHeader.Controls.Add(this.lblSearchToGRNDate);
            this.pnlSearchHeader.Controls.Add(this.dtpSearchFrmGRNDate);
            this.pnlSearchHeader.Controls.Add(this.lblSearchFrmGRNDate);
            this.pnlSearchHeader.Controls.Add(this.txtSearchReceivedBy);
            this.pnlSearchHeader.Controls.Add(this.lblSearchReceivedBy);
            this.pnlSearchHeader.Controls.Add(this.dtpSearchToPODate);
            this.pnlSearchHeader.Controls.Add(this.lblSearchToPODate);
            this.pnlSearchHeader.Controls.Add(this.dtpSearchfrmPODate);
            this.pnlSearchHeader.Controls.Add(this.lblSearchfrmPODate);
            this.pnlSearchHeader.Controls.Add(this.cmbSearchVendorCode);
            this.pnlSearchHeader.Controls.Add(this.lblSearchVendorCode);
            this.pnlSearchHeader.Controls.Add(this.txtSearchPONumber);
            this.pnlSearchHeader.Controls.Add(this.lblSearchPONumber);
            this.pnlSearchHeader.Controls.Add(this.txtSearchGRNNumber);
            this.pnlSearchHeader.Controls.Add(this.lblSearchGRNNumber);
            this.pnlSearchHeader.Size = new System.Drawing.Size(1005, 170);
            this.pnlSearchHeader.TabIndex = 10;
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblSearchGRNNumber, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.txtSearchGRNNumber, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblSearchPONumber, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.txtSearchPONumber, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblSearchVendorCode, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.cmbSearchVendorCode, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblSearchfrmPODate, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.dtpSearchfrmPODate, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblSearchToPODate, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.dtpSearchToPODate, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblSearchReceivedBy, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.txtSearchReceivedBy, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblSearchFrmGRNDate, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.dtpSearchFrmGRNDate, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblSearchToGRNDate, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.dtpSearchToGRNDate, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblSearchStatus, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.cmbSearchStatus, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblSearchDestinationLocation, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.cmbSearchDestinationLocation, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.pnlSearchButtons, 0);
            // 
            // btnSearch
            // 
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearch.Location = new System.Drawing.Point(853, 0);
            this.btnSearch.TabIndex = 11;
            this.btnSearch.Text = "&Search";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnSearchReset
            // 
            this.btnSearchReset.FlatAppearance.BorderSize = 0;
            this.btnSearchReset.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearchReset.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearchReset.Location = new System.Drawing.Point(928, 0);
            this.btnSearchReset.TabIndex = 12;
            this.btnSearchReset.Click += new System.EventHandler(this.btnSearchReset_Click);
            // 
            // pnlSearchGrid
            // 
            this.pnlSearchGrid.Controls.Add(this.dgvSearchGRN);
            this.pnlSearchGrid.Location = new System.Drawing.Point(0, 194);
            this.pnlSearchGrid.Size = new System.Drawing.Size(1005, 413);
            this.pnlSearchGrid.TabIndex = 1;
            // 
            // pnlCreateDetail
            // 
            this.pnlCreateDetail.Location = new System.Drawing.Point(0, 314);
            this.pnlCreateDetail.Size = new System.Drawing.Size(1005, 0);
            this.pnlCreateDetail.Visible = false;
            // 
            // pnlLowerButtons
            // 
            this.pnlLowerButtons.Controls.Add(this.btnClosed);
            this.pnlLowerButtons.Controls.Add(this.btnCancel);
            this.pnlLowerButtons.Controls.Add(this.btnPrint);
            this.pnlLowerButtons.Location = new System.Drawing.Point(0, 321);
            this.pnlLowerButtons.Controls.SetChildIndex(this.btnPrint, 0);
            this.pnlLowerButtons.Controls.SetChildIndex(this.btnCancel, 0);
            this.pnlLowerButtons.Controls.SetChildIndex(this.btnClosed, 0);
            this.pnlLowerButtons.Controls.SetChildIndex(this.btnCreateReset, 0);
            this.pnlLowerButtons.Controls.SetChildIndex(this.btnSave, 0);
            // 
            // pnlTopButtons
            // 
            this.pnlTopButtons.Location = new System.Drawing.Point(0, 228);
            this.pnlTopButtons.Size = new System.Drawing.Size(1003, 0);
            this.pnlTopButtons.Visible = false;
            // 
            // pnlSearchButtons
            // 
            this.pnlSearchButtons.Location = new System.Drawing.Point(0, 136);
            this.pnlSearchButtons.Size = new System.Drawing.Size(1003, 32);
            // 
            // tabSearch
            // 
            this.tabSearch.Size = new System.Drawing.Size(1005, 607);
            // 
            // tabCreate
            // 
            this.tabCreate.Size = new System.Drawing.Size(1005, 607);
            // 
            // txtPONumber
            // 
            this.txtPONumber.Location = new System.Drawing.Point(181, 19);
            this.txtPONumber.MaxLength = 20;
            this.txtPONumber.Name = "txtPONumber";
            this.txtPONumber.Size = new System.Drawing.Size(110, 21);
            this.txtPONumber.TabIndex = 0;
            this.txtPONumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPONumber_KeyDown);
            this.txtPONumber.Validating += new System.ComponentModel.CancelEventHandler(this.txtPONumber_Validating);
            // 
            // lblPONumber
            // 
            this.lblPONumber.Location = new System.Drawing.Point(50, 19);
            this.lblPONumber.Name = "lblPONumber";
            this.lblPONumber.Size = new System.Drawing.Size(125, 13);
            this.lblPONumber.TabIndex = 32;
            this.lblPONumber.Text = "PO Number:*";
            this.lblPONumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPOAmendmentNumner
            // 
            this.lblPOAmendmentNumner.Location = new System.Drawing.Point(353, 19);
            this.lblPOAmendmentNumner.Name = "lblPOAmendmentNumner";
            this.lblPOAmendmentNumner.Size = new System.Drawing.Size(135, 13);
            this.lblPOAmendmentNumner.TabIndex = 33;
            this.lblPOAmendmentNumner.Text = "Amendment Number:";
            this.lblPOAmendmentNumner.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPOAmendmentNumber
            // 
            this.txtPOAmendmentNumber.Location = new System.Drawing.Point(494, 19);
            this.txtPOAmendmentNumber.MaxLength = 100;
            this.txtPOAmendmentNumber.Name = "txtPOAmendmentNumber";
            this.txtPOAmendmentNumber.ReadOnly = true;
            this.txtPOAmendmentNumber.Size = new System.Drawing.Size(110, 21);
            this.txtPOAmendmentNumber.TabIndex = 79;
            this.txtPOAmendmentNumber.TabStop = false;
            // 
            // lblPODate
            // 
            this.lblPODate.Location = new System.Drawing.Point(645, 19);
            this.lblPODate.Name = "lblPODate";
            this.lblPODate.Size = new System.Drawing.Size(125, 13);
            this.lblPODate.TabIndex = 80;
            this.lblPODate.Text = "PO Date:";
            this.lblPODate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblGrnNo
            // 
            this.lblGrnNo.Location = new System.Drawing.Point(35, 46);
            this.lblGrnNo.Name = "lblGrnNo";
            this.lblGrnNo.Size = new System.Drawing.Size(140, 13);
            this.lblGrnNo.TabIndex = 82;
            this.lblGrnNo.Text = "GRN No:";
            this.lblGrnNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtVendorCode
            // 
            this.txtVendorCode.Location = new System.Drawing.Point(776, 73);
            this.txtVendorCode.MaxLength = 20;
            this.txtVendorCode.Name = "txtVendorCode";
            this.txtVendorCode.ReadOnly = true;
            this.txtVendorCode.Size = new System.Drawing.Size(110, 21);
            this.txtVendorCode.TabIndex = 85;
            this.txtVendorCode.TabStop = false;
            // 
            // lblVendorCode
            // 
            this.lblVendorCode.Location = new System.Drawing.Point(630, 74);
            this.lblVendorCode.Name = "lblVendorCode";
            this.lblVendorCode.Size = new System.Drawing.Size(140, 13);
            this.lblVendorCode.TabIndex = 84;
            this.lblVendorCode.Text = "Vendor Code:";
            this.lblVendorCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtVendorName
            // 
            this.txtVendorName.Location = new System.Drawing.Point(776, 102);
            this.txtVendorName.MaxLength = 100;
            this.txtVendorName.Name = "txtVendorName";
            this.txtVendorName.ReadOnly = true;
            this.txtVendorName.Size = new System.Drawing.Size(110, 21);
            this.txtVendorName.TabIndex = 87;
            this.txtVendorName.TabStop = false;
            // 
            // lblVendorName
            // 
            this.lblVendorName.Location = new System.Drawing.Point(630, 102);
            this.lblVendorName.Name = "lblVendorName";
            this.lblVendorName.Size = new System.Drawing.Size(140, 13);
            this.lblVendorName.TabIndex = 86;
            this.lblVendorName.Text = "Vendor Name:";
            this.lblVendorName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtChallanNumber
            // 
            this.txtChallanNumber.Location = new System.Drawing.Point(181, 73);
            this.txtChallanNumber.MaxLength = 20;
            this.txtChallanNumber.Name = "txtChallanNumber";
            this.txtChallanNumber.Size = new System.Drawing.Size(110, 21);
            this.txtChallanNumber.TabIndex = 1;
            // 
            // lblChallanNumber
            // 
            this.lblChallanNumber.Location = new System.Drawing.Point(20, 74);
            this.lblChallanNumber.Name = "lblChallanNumber";
            this.lblChallanNumber.Size = new System.Drawing.Size(155, 13);
            this.lblChallanNumber.TabIndex = 89;
            this.lblChallanNumber.Text = "Challan Number:*";
            this.lblChallanNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblChallanDate
            // 
            this.lblChallanDate.Location = new System.Drawing.Point(333, 74);
            this.lblChallanDate.Name = "lblChallanDate";
            this.lblChallanDate.Size = new System.Drawing.Size(155, 13);
            this.lblChallanDate.TabIndex = 91;
            this.lblChallanDate.Text = "Challan Date:";
            this.lblChallanDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpChallanDate
            // 
            this.dtpChallanDate.CustomFormat = "dd-MM-yyyy";
            this.dtpChallanDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpChallanDate.Location = new System.Drawing.Point(494, 73);
            this.dtpChallanDate.Name = "dtpChallanDate";
            this.dtpChallanDate.Size = new System.Drawing.Size(110, 21);
            this.dtpChallanDate.TabIndex = 2;
            // 
            // txtShippingDetails
            // 
            this.txtShippingDetails.Location = new System.Drawing.Point(494, 132);
            this.txtShippingDetails.MaxLength = 100;
            this.txtShippingDetails.Name = "txtShippingDetails";
            this.txtShippingDetails.ReadOnly = true;
            this.txtShippingDetails.Size = new System.Drawing.Size(110, 21);
            this.txtShippingDetails.TabIndex = 7;
            this.txtShippingDetails.TabStop = false;
            // 
            // lblShippingdetails
            // 
            this.lblShippingdetails.Location = new System.Drawing.Point(363, 133);
            this.lblShippingdetails.Name = "lblShippingdetails";
            this.lblShippingdetails.Size = new System.Drawing.Size(125, 13);
            this.lblShippingdetails.TabIndex = 94;
            this.lblShippingdetails.Text = "Shipping Details:";
            this.lblShippingdetails.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtVehicleNumber
            // 
            this.txtVehicleNumber.Location = new System.Drawing.Point(181, 193);
            this.txtVehicleNumber.MaxLength = 20;
            this.txtVehicleNumber.Name = "txtVehicleNumber";
            this.txtVehicleNumber.Size = new System.Drawing.Size(110, 21);
            this.txtVehicleNumber.TabIndex = 7;
            // 
            // lblVehicleNumber
            // 
            this.lblVehicleNumber.Location = new System.Drawing.Point(50, 196);
            this.lblVehicleNumber.Name = "lblVehicleNumber";
            this.lblVehicleNumber.Size = new System.Drawing.Size(125, 13);
            this.lblVehicleNumber.TabIndex = 96;
            this.lblVehicleNumber.Text = "Vehicle Number:";
            this.lblVehicleNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtReceivedBy
            // 
            this.txtReceivedBy.Location = new System.Drawing.Point(494, 193);
            this.txtReceivedBy.MaxLength = 20;
            this.txtReceivedBy.Name = "txtReceivedBy";
            this.txtReceivedBy.Size = new System.Drawing.Size(110, 21);
            this.txtReceivedBy.TabIndex = 8;
            this.txtReceivedBy.Tag = "UserId of current user";
            // 
            // lblReceivedBy
            // 
            this.lblReceivedBy.Location = new System.Drawing.Point(363, 196);
            this.lblReceivedBy.Name = "lblReceivedBy";
            this.lblReceivedBy.Size = new System.Drawing.Size(125, 13);
            this.lblReceivedBy.TabIndex = 98;
            this.lblReceivedBy.Text = "Received By:";
            this.lblReceivedBy.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDestLocation
            // 
            this.txtDestLocation.Location = new System.Drawing.Point(776, 132);
            this.txtDestLocation.MaxLength = 500;
            this.txtDestLocation.Multiline = true;
            this.txtDestLocation.Name = "txtDestLocation";
            this.txtDestLocation.ReadOnly = true;
            this.txtDestLocation.Size = new System.Drawing.Size(110, 50);
            this.txtDestLocation.TabIndex = 100;
            this.txtDestLocation.TabStop = false;
            // 
            // lblDestLocation
            // 
            this.lblDestLocation.Location = new System.Drawing.Point(625, 133);
            this.lblDestLocation.Name = "lblDestLocation";
            this.lblDestLocation.Size = new System.Drawing.Size(145, 13);
            this.lblDestLocation.TabIndex = 99;
            this.lblDestLocation.Text = "Destination Location:";
            this.lblDestLocation.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblGRNDate
            // 
            this.lblGRNDate.Location = new System.Drawing.Point(398, 46);
            this.lblGRNDate.Name = "lblGRNDate";
            this.lblGRNDate.Size = new System.Drawing.Size(90, 13);
            this.lblGRNDate.TabIndex = 103;
            this.lblGRNDate.Text = "GRN Date:";
            this.lblGRNDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpInvoiceDate
            // 
            this.dtpInvoiceDate.CustomFormat = "dd-MM-yyyy";
            this.dtpInvoiceDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpInvoiceDate.Location = new System.Drawing.Point(494, 102);
            this.dtpInvoiceDate.Name = "dtpInvoiceDate";
            this.dtpInvoiceDate.Size = new System.Drawing.Size(110, 21);
            this.dtpInvoiceDate.TabIndex = 4;
            // 
            // lblInvoiceDate
            // 
            this.lblInvoiceDate.Location = new System.Drawing.Point(333, 103);
            this.lblInvoiceDate.Name = "lblInvoiceDate";
            this.lblInvoiceDate.Size = new System.Drawing.Size(155, 13);
            this.lblInvoiceDate.TabIndex = 107;
            this.lblInvoiceDate.Text = "Invoice Date:";
            this.lblInvoiceDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtInvoiceNumber
            // 
            this.txtInvoiceNumber.Location = new System.Drawing.Point(181, 102);
            this.txtInvoiceNumber.MaxLength = 20;
            this.txtInvoiceNumber.Name = "txtInvoiceNumber";
            this.txtInvoiceNumber.Size = new System.Drawing.Size(110, 21);
            this.txtInvoiceNumber.TabIndex = 3;
            // 
            // lblInvoiceNumber
            // 
            this.lblInvoiceNumber.Location = new System.Drawing.Point(20, 103);
            this.lblInvoiceNumber.Name = "lblInvoiceNumber";
            this.lblInvoiceNumber.Size = new System.Drawing.Size(155, 13);
            this.lblInvoiceNumber.TabIndex = 106;
            this.lblInvoiceNumber.Text = "Invoice Number:";
            this.lblInvoiceNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtGrossWeight
            // 
            this.txtGrossWeight.Location = new System.Drawing.Point(494, 160);
            this.txtGrossWeight.MaxLength = 20;
            this.txtGrossWeight.Name = "txtGrossWeight";
            this.txtGrossWeight.ReadOnly = true;
            this.txtGrossWeight.Size = new System.Drawing.Size(110, 21);
            this.txtGrossWeight.TabIndex = 9;
            this.txtGrossWeight.TabStop = false;
            // 
            // lblGrossWeight
            // 
            this.lblGrossWeight.Location = new System.Drawing.Point(363, 161);
            this.lblGrossWeight.Name = "lblGrossWeight";
            this.lblGrossWeight.Size = new System.Drawing.Size(125, 13);
            this.lblGrossWeight.TabIndex = 110;
            this.lblGrossWeight.Text = "Gross Weight:";
            this.lblGrossWeight.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtNoOfBoxes
            // 
            this.txtNoOfBoxes.Location = new System.Drawing.Point(776, 193);
            this.txtNoOfBoxes.MaxLength = 5;
            this.txtNoOfBoxes.Name = "txtNoOfBoxes";
            this.txtNoOfBoxes.Size = new System.Drawing.Size(110, 21);
            this.txtNoOfBoxes.TabIndex = 9;
            // 
            // lblNoOfBoxes
            // 
            this.lblNoOfBoxes.Location = new System.Drawing.Point(645, 196);
            this.lblNoOfBoxes.Name = "lblNoOfBoxes";
            this.lblNoOfBoxes.Size = new System.Drawing.Size(125, 13);
            this.lblNoOfBoxes.TabIndex = 112;
            this.lblNoOfBoxes.Text = "No of Boxes:";
            this.lblNoOfBoxes.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.BackgroundImage")));
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnCancel.Location = new System.Drawing.Point(855, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 32);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Ca&ncel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtSearchGRNNumber
            // 
            this.txtSearchGRNNumber.Location = new System.Drawing.Point(206, 16);
            this.txtSearchGRNNumber.MaxLength = 25;
            this.txtSearchGRNNumber.Name = "txtSearchGRNNumber";
            this.txtSearchGRNNumber.Size = new System.Drawing.Size(110, 21);
            this.txtSearchGRNNumber.TabIndex = 0;
            // 
            // lblSearchGRNNumber
            // 
            this.lblSearchGRNNumber.Location = new System.Drawing.Point(75, 19);
            this.lblSearchGRNNumber.Name = "lblSearchGRNNumber";
            this.lblSearchGRNNumber.Size = new System.Drawing.Size(125, 13);
            this.lblSearchGRNNumber.TabIndex = 34;
            this.lblSearchGRNNumber.Text = "GRN Number:";
            this.lblSearchGRNNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSearchPONumber
            // 
            this.txtSearchPONumber.Location = new System.Drawing.Point(508, 16);
            this.txtSearchPONumber.MaxLength = 20;
            this.txtSearchPONumber.Name = "txtSearchPONumber";
            this.txtSearchPONumber.Size = new System.Drawing.Size(110, 21);
            this.txtSearchPONumber.TabIndex = 1;
            // 
            // lblSearchPONumber
            // 
            this.lblSearchPONumber.Location = new System.Drawing.Point(377, 19);
            this.lblSearchPONumber.Name = "lblSearchPONumber";
            this.lblSearchPONumber.Size = new System.Drawing.Size(125, 13);
            this.lblSearchPONumber.TabIndex = 36;
            this.lblSearchPONumber.Text = "PO Number:";
            this.lblSearchPONumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbSearchVendorCode
            // 
            this.cmbSearchVendorCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSearchVendorCode.FormattingEnabled = true;
            this.cmbSearchVendorCode.Location = new System.Drawing.Point(794, 16);
            this.cmbSearchVendorCode.Name = "cmbSearchVendorCode";
            this.cmbSearchVendorCode.Size = new System.Drawing.Size(110, 21);
            this.cmbSearchVendorCode.TabIndex = 2;
            // 
            // lblSearchVendorCode
            // 
            this.lblSearchVendorCode.Location = new System.Drawing.Point(653, 19);
            this.lblSearchVendorCode.Name = "lblSearchVendorCode";
            this.lblSearchVendorCode.Size = new System.Drawing.Size(135, 13);
            this.lblSearchVendorCode.TabIndex = 38;
            this.lblSearchVendorCode.Text = "Vendor Code:";
            this.lblSearchVendorCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpSearchfrmPODate
            // 
            this.dtpSearchfrmPODate.Checked = false;
            this.dtpSearchfrmPODate.CustomFormat = "dd-MM-yyyy";
            this.dtpSearchfrmPODate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpSearchfrmPODate.Location = new System.Drawing.Point(508, 46);
            this.dtpSearchfrmPODate.Name = "dtpSearchfrmPODate";
            this.dtpSearchfrmPODate.ShowCheckBox = true;
            this.dtpSearchfrmPODate.Size = new System.Drawing.Size(110, 21);
            this.dtpSearchfrmPODate.TabIndex = 4;
            // 
            // lblSearchfrmPODate
            // 
            this.lblSearchfrmPODate.Location = new System.Drawing.Point(347, 47);
            this.lblSearchfrmPODate.Name = "lblSearchfrmPODate";
            this.lblSearchfrmPODate.Size = new System.Drawing.Size(155, 13);
            this.lblSearchfrmPODate.TabIndex = 109;
            this.lblSearchfrmPODate.Text = "From (PO) Date:";
            this.lblSearchfrmPODate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpSearchToPODate
            // 
            this.dtpSearchToPODate.Checked = false;
            this.dtpSearchToPODate.CustomFormat = "dd-MM-yyyy";
            this.dtpSearchToPODate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpSearchToPODate.Location = new System.Drawing.Point(794, 47);
            this.dtpSearchToPODate.Name = "dtpSearchToPODate";
            this.dtpSearchToPODate.ShowCheckBox = true;
            this.dtpSearchToPODate.Size = new System.Drawing.Size(110, 21);
            this.dtpSearchToPODate.TabIndex = 5;
            // 
            // lblSearchToPODate
            // 
            this.lblSearchToPODate.Location = new System.Drawing.Point(633, 48);
            this.lblSearchToPODate.Name = "lblSearchToPODate";
            this.lblSearchToPODate.Size = new System.Drawing.Size(155, 13);
            this.lblSearchToPODate.TabIndex = 111;
            this.lblSearchToPODate.Text = "To (PO) Date:";
            this.lblSearchToPODate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpSearchToGRNDate
            // 
            this.dtpSearchToGRNDate.Checked = false;
            this.dtpSearchToGRNDate.CustomFormat = "dd-MM-yyyy";
            this.dtpSearchToGRNDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpSearchToGRNDate.Location = new System.Drawing.Point(794, 74);
            this.dtpSearchToGRNDate.Name = "dtpSearchToGRNDate";
            this.dtpSearchToGRNDate.ShowCheckBox = true;
            this.dtpSearchToGRNDate.Size = new System.Drawing.Size(110, 21);
            this.dtpSearchToGRNDate.TabIndex = 8;
            // 
            // lblSearchToGRNDate
            // 
            this.lblSearchToGRNDate.Location = new System.Drawing.Point(633, 75);
            this.lblSearchToGRNDate.Name = "lblSearchToGRNDate";
            this.lblSearchToGRNDate.Size = new System.Drawing.Size(155, 13);
            this.lblSearchToGRNDate.TabIndex = 117;
            this.lblSearchToGRNDate.Text = "To (GRN) Date:";
            this.lblSearchToGRNDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpSearchFrmGRNDate
            // 
            this.dtpSearchFrmGRNDate.Checked = false;
            this.dtpSearchFrmGRNDate.CustomFormat = "dd-MM-yyyy";
            this.dtpSearchFrmGRNDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpSearchFrmGRNDate.Location = new System.Drawing.Point(508, 73);
            this.dtpSearchFrmGRNDate.Name = "dtpSearchFrmGRNDate";
            this.dtpSearchFrmGRNDate.ShowCheckBox = true;
            this.dtpSearchFrmGRNDate.Size = new System.Drawing.Size(110, 21);
            this.dtpSearchFrmGRNDate.TabIndex = 7;
            // 
            // lblSearchFrmGRNDate
            // 
            this.lblSearchFrmGRNDate.Location = new System.Drawing.Point(347, 74);
            this.lblSearchFrmGRNDate.Name = "lblSearchFrmGRNDate";
            this.lblSearchFrmGRNDate.Size = new System.Drawing.Size(155, 13);
            this.lblSearchFrmGRNDate.TabIndex = 115;
            this.lblSearchFrmGRNDate.Text = "From (GRN) Date:";
            this.lblSearchFrmGRNDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSearchReceivedBy
            // 
            this.txtSearchReceivedBy.Location = new System.Drawing.Point(206, 70);
            this.txtSearchReceivedBy.MaxLength = 100;
            this.txtSearchReceivedBy.Name = "txtSearchReceivedBy";
            this.txtSearchReceivedBy.Size = new System.Drawing.Size(110, 21);
            this.txtSearchReceivedBy.TabIndex = 6;
            // 
            // lblSearchReceivedBy
            // 
            this.lblSearchReceivedBy.Location = new System.Drawing.Point(75, 73);
            this.lblSearchReceivedBy.Name = "lblSearchReceivedBy";
            this.lblSearchReceivedBy.Size = new System.Drawing.Size(125, 13);
            this.lblSearchReceivedBy.TabIndex = 113;
            this.lblSearchReceivedBy.Text = "Received By:";
            this.lblSearchReceivedBy.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbSearchStatus
            // 
            this.cmbSearchStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSearchStatus.FormattingEnabled = true;
            this.cmbSearchStatus.Location = new System.Drawing.Point(206, 45);
            this.cmbSearchStatus.Name = "cmbSearchStatus";
            this.cmbSearchStatus.Size = new System.Drawing.Size(110, 21);
            this.cmbSearchStatus.TabIndex = 3;
            // 
            // lblSearchStatus
            // 
            this.lblSearchStatus.AutoSize = true;
            this.lblSearchStatus.Location = new System.Drawing.Point(152, 45);
            this.lblSearchStatus.Name = "lblSearchStatus";
            this.lblSearchStatus.Size = new System.Drawing.Size(48, 13);
            this.lblSearchStatus.TabIndex = 131;
            this.lblSearchStatus.Text = "Status:";
            // 
            // dgvSearchGRN
            // 
            this.dgvSearchGRN.AllowUserToAddRows = false;
            this.dgvSearchGRN.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(233)))), ((int)(((byte)(251)))));
            this.dgvSearchGRN.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvSearchGRN.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dgvSearchGRN.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.dgvSearchGRN.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvSearchGRN.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSearchGRN.EnableHeadersVisualStyles = false;
            this.dgvSearchGRN.GridColor = System.Drawing.SystemColors.Control;
            this.dgvSearchGRN.Location = new System.Drawing.Point(0, 0);
            this.dgvSearchGRN.MultiSelect = false;
            this.dgvSearchGRN.Name = "dgvSearchGRN";
            this.dgvSearchGRN.ReadOnly = true;
            this.dgvSearchGRN.RowHeadersVisible = false;
            this.dgvSearchGRN.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSearchGRN.Size = new System.Drawing.Size(1005, 413);
            this.dgvSearchGRN.TabIndex = 0;
            this.dgvSearchGRN.TabStop = false;
            this.dgvSearchGRN.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgvSearchGRN_MouseDoubleClick);
            this.dgvSearchGRN.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSearchGRN_CellClick);
            // 
            // btnClosed
            // 
            this.btnClosed.BackColor = System.Drawing.Color.Transparent;
            this.btnClosed.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClosed.BackgroundImage")));
            this.btnClosed.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClosed.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClosed.FlatAppearance.BorderSize = 0;
            this.btnClosed.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnClosed.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnClosed.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClosed.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnClosed.Location = new System.Drawing.Point(780, 0);
            this.btnClosed.Name = "btnClosed";
            this.btnClosed.Size = new System.Drawing.Size(75, 32);
            this.btnClosed.TabIndex = 2;
            this.btnClosed.Text = "Cl&ose";
            this.btnClosed.UseVisualStyleBackColor = false;
            this.btnClosed.Click += new System.EventHandler(this.btnClosed_Click);
            // 
            // errorSearch
            // 
            this.errorSearch.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorSearch.ContainerControl = this;
            // 
            // lblCreateGrnDate
            // 
            this.lblCreateGrnDate.Location = new System.Drawing.Point(494, 49);
            this.lblCreateGrnDate.Name = "lblCreateGrnDate";
            this.lblCreateGrnDate.Size = new System.Drawing.Size(110, 13);
            this.lblCreateGrnDate.TabIndex = 113;
            this.lblCreateGrnDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblGrnNoValue
            // 
            this.lblGrnNoValue.Location = new System.Drawing.Point(181, 49);
            this.lblGrnNoValue.Name = "lblGrnNoValue";
            this.lblGrnNoValue.Size = new System.Drawing.Size(200, 13);
            this.lblGrnNoValue.TabIndex = 114;
            this.lblGrnNoValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStatusValue
            // 
            this.lblStatusValue.Location = new System.Drawing.Point(776, 49);
            this.lblStatusValue.Name = "lblStatusValue";
            this.lblStatusValue.Size = new System.Drawing.Size(110, 13);
            this.lblStatusValue.TabIndex = 116;
            this.lblStatusValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStatus
            // 
            this.lblStatus.Location = new System.Drawing.Point(615, 49);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(155, 13);
            this.lblStatus.TabIndex = 115;
            this.lblStatus.Text = "Status:";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPODateValue
            // 
            this.lblPODateValue.Location = new System.Drawing.Point(773, 22);
            this.lblPODateValue.Name = "lblPODateValue";
            this.lblPODateValue.Size = new System.Drawing.Size(110, 13);
            this.lblPODateValue.TabIndex = 117;
            this.lblPODateValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlButton
            // 
            this.pnlButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButton.Location = new System.Drawing.Point(0, 298);
            this.pnlButton.Name = "pnlButton";
            this.pnlButton.Size = new System.Drawing.Size(993, 25);
            this.pnlButton.TabIndex = 2;
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.Color.Transparent;
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
            this.btnPrint.TabIndex = 4;
            this.btnPrint.Text = "&Print";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnReset
            // 
            this.btnReset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReset.Location = new System.Drawing.Point(0, 0);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 1;
            this.btnReset.Text = "&Reset";
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.Reset_Click);
            // 
            // dgvGRNItems
            // 
            this.dgvGRNItems.AllowUserToAddRows = false;
            this.dgvGRNItems.AllowUserToDeleteRows = false;
            this.dgvGRNItems.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dgvGRNItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGRNItems.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgvGRNItems.Location = new System.Drawing.Point(0, 14);
            this.dgvGRNItems.Margin = new System.Windows.Forms.Padding(0);
            this.dgvGRNItems.MultiSelect = false;
            this.dgvGRNItems.Name = "dgvGRNItems";
            this.dgvGRNItems.RowHeadersVisible = false;
            this.dgvGRNItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvGRNItems.ShowCellErrors = false;
            this.dgvGRNItems.ShowEditingIcon = false;
            this.dgvGRNItems.ShowRowErrors = false;
            this.dgvGRNItems.Size = new System.Drawing.Size(1005, 300);
            this.dgvGRNItems.TabIndex = 0;
            this.dgvGRNItems.TabStop = false;
            this.dgvGRNItems.Leave += new System.EventHandler(this.dgvGRNItems_Leave);
            this.dgvGRNItems.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvGRNItems_CellEndEdit);
            this.dgvGRNItems.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvGRNItems_CellClick);
            this.dgvGRNItems.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvGRNItems_DataError);
            this.dgvGRNItems.SelectionChanged += new System.EventHandler(this.dgvGRNItems_SelectionChanged);
            // 
            // errorCreate
            // 
            this.errorCreate.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorCreate.ContainerControl = this;
            // 
            // txtInvoiceTax
            // 
            this.txtInvoiceTax.Location = new System.Drawing.Point(181, 133);
            this.txtInvoiceTax.MaxLength = 20;
            this.txtInvoiceTax.Name = "txtInvoiceTax";
            this.txtInvoiceTax.Size = new System.Drawing.Size(110, 21);
            this.txtInvoiceTax.TabIndex = 5;
            // 
            // lblInvoiceTax
            // 
            this.lblInvoiceTax.Location = new System.Drawing.Point(20, 134);
            this.lblInvoiceTax.Name = "lblInvoiceTax";
            this.lblInvoiceTax.Size = new System.Drawing.Size(155, 13);
            this.lblInvoiceTax.TabIndex = 119;
            this.lblInvoiceTax.Text = "Invoice Tax:";
            this.lblInvoiceTax.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtInvoiceAmount
            // 
            this.txtInvoiceAmount.Location = new System.Drawing.Point(181, 162);
            this.txtInvoiceAmount.MaxLength = 20;
            this.txtInvoiceAmount.Name = "txtInvoiceAmount";
            this.txtInvoiceAmount.Size = new System.Drawing.Size(110, 21);
            this.txtInvoiceAmount.TabIndex = 6;
            // 
            // lblInvoiceAmount
            // 
            this.lblInvoiceAmount.Location = new System.Drawing.Point(-1, 162);
            this.lblInvoiceAmount.Name = "lblInvoiceAmount";
            this.lblInvoiceAmount.Size = new System.Drawing.Size(175, 13);
            this.lblInvoiceAmount.TabIndex = 121;
            this.lblInvoiceAmount.Text = "Invoice Amount (incl. Tax):";
            this.lblInvoiceAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbSearchDestinationLocation
            // 
            this.cmbSearchDestinationLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSearchDestinationLocation.FormattingEnabled = true;
            this.cmbSearchDestinationLocation.Location = new System.Drawing.Point(206, 99);
            this.cmbSearchDestinationLocation.Name = "cmbSearchDestinationLocation";
            this.cmbSearchDestinationLocation.Size = new System.Drawing.Size(150, 21);
            this.cmbSearchDestinationLocation.TabIndex = 9;
            // 
            // lblSearchDestinationLocation
            // 
            this.lblSearchDestinationLocation.Location = new System.Drawing.Point(65, 102);
            this.lblSearchDestinationLocation.Name = "lblSearchDestinationLocation";
            this.lblSearchDestinationLocation.Size = new System.Drawing.Size(135, 13);
            this.lblSearchDestinationLocation.TabIndex = 133;
            this.lblSearchDestinationLocation.Text = "Destination Location:";
            this.lblSearchDestinationLocation.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::PurchaseComponent.Properties.Resources.find;
            this.pictureBox1.Location = new System.Drawing.Point(308, 19);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(21, 21);
            this.pictureBox1.TabIndex = 122;
            this.pictureBox1.TabStop = false;
            // 
            // frmGRN
            // 
            this.AcceptButton = this.btnSearch;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1013, 703);
            this.Name = "frmGRN";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "GRN";
            this.WindowState = System.Windows.Forms.FormWindowState.Normal;
            this.Load += new System.EventHandler(this.frmGRN_Load);
            this.pnlCreateHeader.ResumeLayout(false);
            this.pnlCreateHeader.PerformLayout();
            this.grpAddDetails.ResumeLayout(false);
            this.pnlSearchHeader.ResumeLayout(false);
            this.pnlSearchHeader.PerformLayout();
            this.pnlSearchGrid.ResumeLayout(false);
            this.pnlLowerButtons.ResumeLayout(false);
            this.pnlTopButtons.ResumeLayout(false);
            this.pnlSearchButtons.ResumeLayout(false);
            this.tabSearch.ResumeLayout(false);
            this.tabCreate.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearchGRN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorSearch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGRNItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorCreate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtPONumber;
        private System.Windows.Forms.Label lblPONumber;
        private System.Windows.Forms.Label lblPOAmendmentNumner;
        private System.Windows.Forms.TextBox txtPOAmendmentNumber;
        private System.Windows.Forms.Label lblPODate;
        private System.Windows.Forms.Label lblGrnNo;
        private System.Windows.Forms.TextBox txtVendorCode;
        private System.Windows.Forms.Label lblVendorCode;
        private System.Windows.Forms.TextBox txtVendorName;
        private System.Windows.Forms.Label lblVendorName;
        private System.Windows.Forms.TextBox txtChallanNumber;
        private System.Windows.Forms.Label lblChallanNumber;
        private System.Windows.Forms.Label lblChallanDate;
        private System.Windows.Forms.DateTimePicker dtpChallanDate;
        private System.Windows.Forms.TextBox txtShippingDetails;
        private System.Windows.Forms.Label lblShippingdetails;
        private System.Windows.Forms.TextBox txtVehicleNumber;
        private System.Windows.Forms.Label lblVehicleNumber;
        private System.Windows.Forms.TextBox txtReceivedBy;
        private System.Windows.Forms.Label lblReceivedBy;
        private System.Windows.Forms.TextBox txtDestLocation;
        private System.Windows.Forms.Label lblDestLocation;
        private System.Windows.Forms.Label lblGRNDate;
        private System.Windows.Forms.DateTimePicker dtpInvoiceDate;
        private System.Windows.Forms.Label lblInvoiceDate;
        private System.Windows.Forms.TextBox txtInvoiceNumber;
        private System.Windows.Forms.Label lblInvoiceNumber;
        private System.Windows.Forms.TextBox txtGrossWeight;
        private System.Windows.Forms.Label lblGrossWeight;
        private System.Windows.Forms.TextBox txtNoOfBoxes;
        private System.Windows.Forms.Label lblNoOfBoxes;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtSearchPONumber;
        private System.Windows.Forms.Label lblSearchPONumber;
        private System.Windows.Forms.TextBox txtSearchGRNNumber;
        private System.Windows.Forms.Label lblSearchGRNNumber;
        private System.Windows.Forms.ComboBox cmbSearchVendorCode;
        private System.Windows.Forms.Label lblSearchVendorCode;
        private System.Windows.Forms.DateTimePicker dtpSearchToPODate;
        private System.Windows.Forms.Label lblSearchToPODate;
        private System.Windows.Forms.DateTimePicker dtpSearchfrmPODate;
        private System.Windows.Forms.Label lblSearchfrmPODate;
        private System.Windows.Forms.DateTimePicker dtpSearchToGRNDate;
        private System.Windows.Forms.Label lblSearchToGRNDate;
        private System.Windows.Forms.DateTimePicker dtpSearchFrmGRNDate;
        private System.Windows.Forms.Label lblSearchFrmGRNDate;
        private System.Windows.Forms.TextBox txtSearchReceivedBy;
        private System.Windows.Forms.Label lblSearchReceivedBy;
        private System.Windows.Forms.ComboBox cmbSearchStatus;
        private System.Windows.Forms.Label lblSearchStatus;
        private System.Windows.Forms.DataGridView dgvSearchGRN;
        private System.Windows.Forms.Button btnClosed;
        private System.Windows.Forms.ErrorProvider errorSearch;
        private System.Windows.Forms.Label lblGrnNoValue;
        private System.Windows.Forms.Label lblCreateGrnDate;
        private System.Windows.Forms.Label lblStatusValue;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblPODateValue;
        private System.Windows.Forms.Panel pnlButton;
        private System.Windows.Forms.DataGridView dgvGRNItems;
        private System.Windows.Forms.ErrorProvider errorCreate;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.TextBox txtInvoiceAmount;
        private System.Windows.Forms.Label lblInvoiceAmount;
        private System.Windows.Forms.TextBox txtInvoiceTax;
        private System.Windows.Forms.Label lblInvoiceTax;
        private System.Windows.Forms.ComboBox cmbSearchDestinationLocation;
        private System.Windows.Forms.Label lblSearchDestinationLocation;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}