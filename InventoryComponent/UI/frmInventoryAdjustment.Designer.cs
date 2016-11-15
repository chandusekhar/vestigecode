namespace InventoryComponent.UI
{
    partial class frmInventoryAdjustment
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInventoryAdjustment));
            this.cmbSearchLocation = new System.Windows.Forms.ComboBox();
            this.cmbSearchStatus = new System.Windows.Forms.ComboBox();
            this.dtpSearchTo = new System.Windows.Forms.DateTimePicker();
            this.dtpSearchFrom = new System.Windows.Forms.DateTimePicker();
            this.lblSearchFromDate = new System.Windows.Forms.Label();
            this.lblSearchToDate = new System.Windows.Forms.Label();
            this.lblSearchLocation = new System.Windows.Forms.Label();
            this.lblSearchStatus = new System.Windows.Forms.Label();
            this.cmbApprovedBy = new System.Windows.Forms.ComboBox();
            this.lblSearchApprovedBy = new System.Windows.Forms.Label();
            this.cmbLocation = new System.Windows.Forms.ComboBox();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.txtInitiatedBy = new System.Windows.Forms.TextBox();
            this.txtApprovedBy = new System.Windows.Forms.TextBox();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.lblInitiatedBy = new System.Windows.Forms.Label();
            this.lblInitiatedDate = new System.Windows.Forms.Label();
            this.lblApprovedBy = new System.Windows.Forms.Label();
            this.txtLocationCode = new System.Windows.Forms.TextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.txtLocationAddress = new System.Windows.Forms.TextBox();
            this.lblLocationName = new System.Windows.Forms.Label();
            this.lblLocationAddress = new System.Windows.Forms.Label();
            this.lblLocation = new System.Windows.Forms.Label();
            this.cmbFromBucket = new System.Windows.Forms.ComboBox();
            this.txtItemCode = new System.Windows.Forms.TextBox();
            this.txtWeight = new System.Windows.Forms.TextBox();
            this.txtBatchNo = new System.Windows.Forms.TextBox();
            this.lblReasonCode = new System.Windows.Forms.Label();
            this.txtItemDescription = new System.Windows.Forms.TextBox();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.lblWeight = new System.Windows.Forms.Label();
            this.lblItemDescription = new System.Windows.Forms.Label();
            this.lblBatchNo = new System.Windows.Forms.Label();
            this.lblQty = new System.Windows.Forms.Label();
            this.lblItemCode = new System.Windows.Forms.Label();
            this.lblFromBucketName = new System.Windows.Forms.Label();
            this.lblToBukcetName = new System.Windows.Forms.Label();
            this.cmbToBucket = new System.Windows.Forms.ComboBox();
            this.cmbReasonCode = new System.Windows.Forms.ComboBox();
            this.dgvInventory = new System.Windows.Forms.DataGridView();
            this.dgvInventoryItem = new System.Windows.Forms.DataGridView();
            this.errInventory = new System.Windows.Forms.ErrorProvider(this.components);
            this.lblAdjustNo = new System.Windows.Forms.Label();
            this.txtSeqNo = new System.Windows.Forms.TextBox();
            this.btnInitiated = new System.Windows.Forms.Button();
            this.btnApproved = new System.Windows.Forms.Button();
            this.lblAvailableQty = new System.Windows.Forms.Label();
            this.txtAvailableQty = new System.Windows.Forms.TextBox();
            this.btnReject = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblApprovedQty = new System.Windows.Forms.Label();
            this.txtApprovedQty = new System.Windows.Forms.TextBox();
            this.lblReasonDescription = new System.Windows.Forms.Label();
            this.txtReasonDescription = new System.Windows.Forms.TextBox();
            this.errSearch = new System.Windows.Forms.ErrorProvider(this.components);
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblAdjustmentNo = new System.Windows.Forms.Label();
            this.txtAdjustmentNo = new System.Windows.Forms.TextBox();
            this.picToBatchNo = new System.Windows.Forms.PictureBox();
            this.picToitemcode = new System.Windows.Forms.PictureBox();
            this.txtToitemcode = new System.Windows.Forms.TextBox();
            this.txtToWeight = new System.Windows.Forms.TextBox();
            this.txtToBatchNo = new System.Windows.Forms.TextBox();
            this.txtToItemDesc = new System.Windows.Forms.TextBox();
            this.lblToWeight = new System.Windows.Forms.Label();
            this.lblToItemDesc = new System.Windows.Forms.Label();
            this.lblToBatchNo = new System.Windows.Forms.Label();
            this.lblToitemcode = new System.Windows.Forms.Label();
            this.grpBoxTo = new System.Windows.Forms.GroupBox();
            this.grpBoxFrom = new System.Windows.Forms.GroupBox();
            this.btnbatch = new System.Windows.Forms.Button();
            this.chkExportIn = new System.Windows.Forms.CheckBox();
            this.chkisexport = new System.Windows.Forms.CheckBox();
            this.lblisexport = new System.Windows.Forms.Label();
            this.lblexport = new System.Windows.Forms.Label();
            this.chkbatchadj = new System.Windows.Forms.CheckBox();
            this.lblbatchadj = new System.Windows.Forms.Label();
            this.chkinternalbatadj = new System.Windows.Forms.CheckBox();
            this.lblinterbatadj = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textInitiatedQty = new System.Windows.Forms.TextBox();
            this.pnlCreateHeader.SuspendLayout();
            this.grpAddDetails.SuspendLayout();
            this.pnlSearchHeader.SuspendLayout();
            this.pnlSearchGrid.SuspendLayout();
            this.pnlCreateDetail.SuspendLayout();
            this.pnlLowerButtons.SuspendLayout();
            this.pnlSearchButtons.SuspendLayout();
            this.tabSearch.SuspendLayout();
            this.tabCreate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInventory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInventoryItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errInventory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errSearch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picToBatchNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picToitemcode)).BeginInit();
            this.grpBoxTo.SuspendLayout();
            this.grpBoxFrom.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCreateHeader
            // 
            this.pnlCreateHeader.Controls.Add(this.cmbLocation);
            this.pnlCreateHeader.Controls.Add(this.chkbatchadj);
            this.pnlCreateHeader.Controls.Add(this.lblbatchadj);
            this.pnlCreateHeader.Controls.Add(this.dtpDate);
            this.pnlCreateHeader.Controls.Add(this.txtInitiatedBy);
            this.pnlCreateHeader.Controls.Add(this.lblexport);
            this.pnlCreateHeader.Controls.Add(this.txtApprovedBy);
            this.pnlCreateHeader.Controls.Add(this.chkExportIn);
            this.pnlCreateHeader.Controls.Add(this.txtAdjustmentNo);
            this.pnlCreateHeader.Controls.Add(this.lblAdjustmentNo);
            this.pnlCreateHeader.Controls.Add(this.lblInitiatedBy);
            this.pnlCreateHeader.Controls.Add(this.lblInitiatedDate);
            this.pnlCreateHeader.Controls.Add(this.txtStatus);
            this.pnlCreateHeader.Controls.Add(this.lblApprovedBy);
            this.pnlCreateHeader.Controls.Add(this.txtLocationCode);
            this.pnlCreateHeader.Controls.Add(this.txtLocationAddress);
            this.pnlCreateHeader.Controls.Add(this.lblLocationAddress);
            this.pnlCreateHeader.Controls.Add(this.lblStatus);
            this.pnlCreateHeader.Controls.Add(this.lblLocationName);
            this.pnlCreateHeader.Controls.Add(this.lblLocation);
            this.pnlCreateHeader.Size = new System.Drawing.Size(1005, 155);
            this.pnlCreateHeader.TabIndex = 1;
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblLocation, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblLocationName, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblStatus, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblLocationAddress, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.txtLocationAddress, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.txtLocationCode, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblApprovedBy, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.txtStatus, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblInitiatedDate, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblInitiatedBy, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblAdjustmentNo, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.txtAdjustmentNo, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.chkExportIn, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.txtApprovedBy, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblexport, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.txtInitiatedBy, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.dtpDate, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblbatchadj, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.chkbatchadj, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.cmbLocation, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.pnlTopButtons, 0);
            // 
            // btnCreateReset
            // 
            this.btnCreateReset.FlatAppearance.BorderSize = 0;
            this.btnCreateReset.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnCreateReset.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnCreateReset.Location = new System.Drawing.Point(681, 0);
            this.btnCreateReset.Size = new System.Drawing.Size(77, 32);
            this.btnCreateReset.TabIndex = 6;
            this.btnCreateReset.Click += new System.EventHandler(this.btnCreateReset_Click);
            // 
            // btnSave
            // 
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSave.Location = new System.Drawing.Point(758, 0);
            this.btnSave.Size = new System.Drawing.Size(77, 32);
            this.btnSave.TabIndex = 1;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblAddDetails
            // 
            this.lblAddDetails.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAddDetails.ForeColor = System.Drawing.Color.White;
            this.lblAddDetails.TabIndex = 1;
            this.lblAddDetails.Text = "Item Detail";
            // 
            // grpAddDetails
            // 
            this.grpAddDetails.Controls.Add(this.dgvInventoryItem);
            this.grpAddDetails.Location = new System.Drawing.Point(0, 179);
            this.grpAddDetails.Size = new System.Drawing.Size(1005, 428);
            this.grpAddDetails.TabIndex = 1;
            this.grpAddDetails.Controls.SetChildIndex(this.pnlLowerButtons, 0);
            this.grpAddDetails.Controls.SetChildIndex(this.pnlCreateDetail, 0);
            this.grpAddDetails.Controls.SetChildIndex(this.dgvInventoryItem, 0);
            // 
            // btnClearDetails
            // 
            this.btnClearDetails.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnClearDetails.Dock = System.Windows.Forms.DockStyle.None;
            this.btnClearDetails.FlatAppearance.BorderSize = 0;
            this.btnClearDetails.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnClearDetails.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnClearDetails.Location = new System.Drawing.Point(867, 120);
            this.btnClearDetails.Size = new System.Drawing.Size(80, 40);
            this.btnClearDetails.TabIndex = 21;
            this.btnClearDetails.Click += new System.EventHandler(this.btnClearDetails_Click);
            // 
            // btnAddDetails
            // 
            this.btnAddDetails.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnAddDetails.Dock = System.Windows.Forms.DockStyle.None;
            this.btnAddDetails.FlatAppearance.BorderSize = 0;
            this.btnAddDetails.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnAddDetails.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnAddDetails.Location = new System.Drawing.Point(787, 121);
            this.btnAddDetails.Size = new System.Drawing.Size(80, 39);
            this.btnAddDetails.TabIndex = 20;
            this.btnAddDetails.Click += new System.EventHandler(this.btnAddDetails_Click);
            // 
            // pnlSearchHeader
            // 
            this.pnlSearchHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSearchHeader.Controls.Add(this.txtSeqNo);
            this.pnlSearchHeader.Controls.Add(this.cmbApprovedBy);
            this.pnlSearchHeader.Controls.Add(this.cmbSearchLocation);
            this.pnlSearchHeader.Controls.Add(this.lblSearchApprovedBy);
            this.pnlSearchHeader.Controls.Add(this.cmbSearchStatus);
            this.pnlSearchHeader.Controls.Add(this.lblAdjustNo);
            this.pnlSearchHeader.Controls.Add(this.dtpSearchTo);
            this.pnlSearchHeader.Controls.Add(this.dtpSearchFrom);
            this.pnlSearchHeader.Controls.Add(this.lblSearchFromDate);
            this.pnlSearchHeader.Controls.Add(this.lblSearchToDate);
            this.pnlSearchHeader.Controls.Add(this.lblSearchStatus);
            this.pnlSearchHeader.Controls.Add(this.lblSearchLocation);
            this.pnlSearchHeader.Size = new System.Drawing.Size(1005, 120);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblSearchLocation, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblSearchStatus, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblSearchToDate, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblSearchFromDate, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.dtpSearchFrom, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.dtpSearchTo, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblAdjustNo, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.cmbSearchStatus, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblSearchApprovedBy, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.cmbSearchLocation, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.cmbApprovedBy, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.txtSeqNo, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.pnlSearchButtons, 0);
            // 
            // btnSearch
            // 
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearch.Location = new System.Drawing.Point(853, 0);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnSearchReset
            // 
            this.btnSearchReset.FlatAppearance.BorderSize = 0;
            this.btnSearchReset.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearchReset.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearchReset.Location = new System.Drawing.Point(928, 0);
            this.btnSearchReset.TabIndex = 4;
            this.btnSearchReset.Click += new System.EventHandler(this.btnSearchReset_Click);
            // 
            // lblSearchResult
            // 
            this.lblSearchResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            // 
            // pnlSearchGrid
            // 
            this.pnlSearchGrid.Controls.Add(this.dgvInventory);
            this.pnlSearchGrid.Location = new System.Drawing.Point(0, 144);
            this.pnlSearchGrid.Size = new System.Drawing.Size(1005, 463);
            // 
            // pnlCreateDetail
            // 
            this.pnlCreateDetail.Controls.Add(this.textInitiatedQty);
            this.pnlCreateDetail.Controls.Add(this.label1);
            this.pnlCreateDetail.Controls.Add(this.grpBoxTo);
            this.pnlCreateDetail.Controls.Add(this.cmbToBucket);
            this.pnlCreateDetail.Controls.Add(this.grpBoxFrom);
            this.pnlCreateDetail.Controls.Add(this.cmbReasonCode);
            this.pnlCreateDetail.Controls.Add(this.cmbFromBucket);
            this.pnlCreateDetail.Controls.Add(this.txtAvailableQty);
            this.pnlCreateDetail.Controls.Add(this.lblReasonCode);
            this.pnlCreateDetail.Controls.Add(this.txtReasonDescription);
            this.pnlCreateDetail.Controls.Add(this.txtApprovedQty);
            this.pnlCreateDetail.Controls.Add(this.txtQty);
            this.pnlCreateDetail.Controls.Add(this.lblAvailableQty);
            this.pnlCreateDetail.Controls.Add(this.lblReasonDescription);
            this.pnlCreateDetail.Controls.Add(this.lblApprovedQty);
            this.pnlCreateDetail.Controls.Add(this.lblQty);
            this.pnlCreateDetail.Controls.Add(this.lblToBukcetName);
            this.pnlCreateDetail.Controls.Add(this.lblFromBucketName);
            this.pnlCreateDetail.Controls.Add(this.btnAddDetails);
            this.pnlCreateDetail.Controls.Add(this.btnClearDetails);
            this.pnlCreateDetail.Size = new System.Drawing.Size(1005, 165);
            this.pnlCreateDetail.TabIndex = 0;
            this.pnlCreateDetail.Controls.SetChildIndex(this.btnClearDetails, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.btnAddDetails, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.lblFromBucketName, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.lblToBukcetName, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.lblQty, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.lblApprovedQty, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.lblReasonDescription, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.lblAvailableQty, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.txtQty, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.txtApprovedQty, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.txtReasonDescription, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.lblReasonCode, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.txtAvailableQty, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.cmbFromBucket, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.cmbReasonCode, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.grpBoxFrom, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.cmbToBucket, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.grpBoxTo, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.label1, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.textInitiatedQty, 0);
            // 
            // pnlLowerButtons
            // 
            this.pnlLowerButtons.Controls.Add(this.btnPrint);
            this.pnlLowerButtons.Controls.Add(this.btnCancel);
            this.pnlLowerButtons.Controls.Add(this.btnApproved);
            this.pnlLowerButtons.Controls.Add(this.btnInitiated);
            this.pnlLowerButtons.Controls.Add(this.btnReject);
            this.pnlLowerButtons.Location = new System.Drawing.Point(0, 396);
            this.pnlLowerButtons.Controls.SetChildIndex(this.btnReject, 0);
            this.pnlLowerButtons.Controls.SetChildIndex(this.btnInitiated, 0);
            this.pnlLowerButtons.Controls.SetChildIndex(this.btnApproved, 0);
            this.pnlLowerButtons.Controls.SetChildIndex(this.btnSave, 0);
            this.pnlLowerButtons.Controls.SetChildIndex(this.btnCreateReset, 0);
            this.pnlLowerButtons.Controls.SetChildIndex(this.btnCancel, 0);
            this.pnlLowerButtons.Controls.SetChildIndex(this.btnPrint, 0);
            // 
            // pnlTopButtons
            // 
            this.pnlTopButtons.Location = new System.Drawing.Point(0, 153);
            this.pnlTopButtons.Size = new System.Drawing.Size(1003, 0);
            // 
            // pnlSearchButtons
            // 
            this.pnlSearchButtons.Controls.Add(this.chkisexport);
            this.pnlSearchButtons.Controls.Add(this.lblisexport);
            this.pnlSearchButtons.Controls.Add(this.chkinternalbatadj);
            this.pnlSearchButtons.Controls.Add(this.lblinterbatadj);
            this.pnlSearchButtons.Location = new System.Drawing.Point(0, 86);
            this.pnlSearchButtons.Size = new System.Drawing.Size(1003, 32);
            this.pnlSearchButtons.Controls.SetChildIndex(this.lblinterbatadj, 0);
            this.pnlSearchButtons.Controls.SetChildIndex(this.chkinternalbatadj, 0);
            this.pnlSearchButtons.Controls.SetChildIndex(this.lblisexport, 0);
            this.pnlSearchButtons.Controls.SetChildIndex(this.chkisexport, 0);
            this.pnlSearchButtons.Controls.SetChildIndex(this.btnSearchReset, 0);
            this.pnlSearchButtons.Controls.SetChildIndex(this.btnSearch, 0);
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
            this.cmbSearchLocation.Location = new System.Drawing.Point(483, 12);
            this.cmbSearchLocation.Name = "cmbSearchLocation";
            this.cmbSearchLocation.Size = new System.Drawing.Size(121, 21);
            this.cmbSearchLocation.TabIndex = 1;
            // 
            // cmbSearchStatus
            // 
            this.cmbSearchStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSearchStatus.FormattingEnabled = true;
            this.cmbSearchStatus.Location = new System.Drawing.Point(849, 12);
            this.cmbSearchStatus.Name = "cmbSearchStatus";
            this.cmbSearchStatus.Size = new System.Drawing.Size(121, 21);
            this.cmbSearchStatus.TabIndex = 2;
            // 
            // dtpSearchTo
            // 
            this.dtpSearchTo.Checked = false;
            this.dtpSearchTo.CustomFormat = "dd-MM-yyyy";
            this.dtpSearchTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpSearchTo.Location = new System.Drawing.Point(483, 52);
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
            this.dtpSearchFrom.Location = new System.Drawing.Point(125, 52);
            this.dtpSearchFrom.Name = "dtpSearchFrom";
            this.dtpSearchFrom.ShowCheckBox = true;
            this.dtpSearchFrom.Size = new System.Drawing.Size(121, 21);
            this.dtpSearchFrom.TabIndex = 3;
            this.dtpSearchFrom.Value = new System.DateTime(2009, 7, 14, 0, 0, 0, 0);
            // 
            // lblSearchFromDate
            // 
            this.lblSearchFromDate.AutoSize = true;
            this.lblSearchFromDate.Location = new System.Drawing.Point(3, 56);
            this.lblSearchFromDate.Name = "lblSearchFromDate";
            this.lblSearchFromDate.Size = new System.Drawing.Size(126, 13);
            this.lblSearchFromDate.TabIndex = 84;
            this.lblSearchFromDate.Text = "From (Initiate) Date:";
            // 
            // lblSearchToDate
            // 
            this.lblSearchToDate.AutoSize = true;
            this.lblSearchToDate.Location = new System.Drawing.Point(373, 56);
            this.lblSearchToDate.Name = "lblSearchToDate";
            this.lblSearchToDate.Size = new System.Drawing.Size(111, 13);
            this.lblSearchToDate.TabIndex = 87;
            this.lblSearchToDate.Text = "To (Initiate) Date:";
            // 
            // lblSearchLocation
            // 
            this.lblSearchLocation.AutoSize = true;
            this.lblSearchLocation.Location = new System.Drawing.Point(425, 15);
            this.lblSearchLocation.Name = "lblSearchLocation";
            this.lblSearchLocation.Size = new System.Drawing.Size(59, 13);
            this.lblSearchLocation.TabIndex = 85;
            this.lblSearchLocation.Text = "Location:";
            // 
            // lblSearchStatus
            // 
            this.lblSearchStatus.AutoSize = true;
            this.lblSearchStatus.Location = new System.Drawing.Point(803, 15);
            this.lblSearchStatus.Name = "lblSearchStatus";
            this.lblSearchStatus.Size = new System.Drawing.Size(48, 13);
            this.lblSearchStatus.TabIndex = 86;
            this.lblSearchStatus.Text = "Status:";
            // 
            // cmbApprovedBy
            // 
            this.cmbApprovedBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbApprovedBy.FormattingEnabled = true;
            this.cmbApprovedBy.Location = new System.Drawing.Point(849, 52);
            this.cmbApprovedBy.Name = "cmbApprovedBy";
            this.cmbApprovedBy.Size = new System.Drawing.Size(121, 21);
            this.cmbApprovedBy.TabIndex = 5;
            this.cmbApprovedBy.Visible = false;
            // 
            // lblSearchApprovedBy
            // 
            this.lblSearchApprovedBy.AutoSize = true;
            this.lblSearchApprovedBy.Location = new System.Drawing.Point(711, 55);
            this.lblSearchApprovedBy.Name = "lblSearchApprovedBy";
            this.lblSearchApprovedBy.Size = new System.Drawing.Size(140, 13);
            this.lblSearchApprovedBy.TabIndex = 89;
            this.lblSearchApprovedBy.Text = "Approved/Rejected by:";
            this.lblSearchApprovedBy.Visible = false;
            // 
            // cmbLocation
            // 
            this.cmbLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocation.FormattingEnabled = true;
            this.cmbLocation.Location = new System.Drawing.Point(124, 19);
            this.cmbLocation.Name = "cmbLocation";
            this.cmbLocation.Size = new System.Drawing.Size(156, 21);
            this.cmbLocation.TabIndex = 3;
            this.cmbLocation.SelectedIndexChanged += new System.EventHandler(this.cmbLocation_SelectedIndexChanged);
            // 
            // dtpDate
            // 
            this.dtpDate.Checked = false;
            this.dtpDate.CustomFormat = "dd-MM-yyyy";
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Location = new System.Drawing.Point(476, 57);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.ShowCheckBox = true;
            this.dtpDate.Size = new System.Drawing.Size(121, 21);
            this.dtpDate.TabIndex = 1;
            this.dtpDate.Value = new System.DateTime(2009, 7, 14, 0, 0, 0, 0);
            this.dtpDate.Validated += new System.EventHandler(this.dtpDate_Validated);
            // 
            // txtInitiatedBy
            // 
            this.txtInitiatedBy.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.txtInitiatedBy.Location = new System.Drawing.Point(124, 99);
            this.txtInitiatedBy.Name = "txtInitiatedBy";
            this.txtInitiatedBy.ReadOnly = true;
            this.txtInitiatedBy.Size = new System.Drawing.Size(148, 21);
            this.txtInitiatedBy.TabIndex = 0;
            this.txtInitiatedBy.TabStop = false;
            // 
            // txtApprovedBy
            // 
            this.txtApprovedBy.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.txtApprovedBy.Location = new System.Drawing.Point(476, 99);
            this.txtApprovedBy.Name = "txtApprovedBy";
            this.txtApprovedBy.ReadOnly = true;
            this.txtApprovedBy.Size = new System.Drawing.Size(121, 21);
            this.txtApprovedBy.TabIndex = 0;
            this.txtApprovedBy.TabStop = false;
            // 
            // txtStatus
            // 
            this.txtStatus.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.txtStatus.Location = new System.Drawing.Point(808, 102);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.ReadOnly = true;
            this.txtStatus.Size = new System.Drawing.Size(121, 21);
            this.txtStatus.TabIndex = 0;
            this.txtStatus.TabStop = false;
            // 
            // lblInitiatedBy
            // 
            this.lblInitiatedBy.AutoSize = true;
            this.lblInitiatedBy.Location = new System.Drawing.Point(41, 102);
            this.lblInitiatedBy.Name = "lblInitiatedBy";
            this.lblInitiatedBy.Size = new System.Drawing.Size(77, 13);
            this.lblInitiatedBy.TabIndex = 169;
            this.lblInitiatedBy.Text = "Initiated by:";
            // 
            // lblInitiatedDate
            // 
            this.lblInitiatedDate.AutoSize = true;
            this.lblInitiatedDate.Location = new System.Drawing.Point(380, 61);
            this.lblInitiatedDate.Name = "lblInitiatedDate";
            this.lblInitiatedDate.Size = new System.Drawing.Size(97, 13);
            this.lblInitiatedDate.TabIndex = 171;
            this.lblInitiatedDate.Text = "Initiated Date:*";
            // 
            // lblApprovedBy
            // 
            this.lblApprovedBy.AutoSize = true;
            this.lblApprovedBy.Location = new System.Drawing.Point(330, 102);
            this.lblApprovedBy.Name = "lblApprovedBy";
            this.lblApprovedBy.Size = new System.Drawing.Size(140, 13);
            this.lblApprovedBy.TabIndex = 170;
            this.lblApprovedBy.Text = "Approved/Rejected by:";
            // 
            // txtLocationCode
            // 
            this.txtLocationCode.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.txtLocationCode.Location = new System.Drawing.Point(476, 19);
            this.txtLocationCode.Multiline = true;
            this.txtLocationCode.Name = "txtLocationCode";
            this.txtLocationCode.ReadOnly = true;
            this.txtLocationCode.Size = new System.Drawing.Size(121, 21);
            this.txtLocationCode.TabIndex = 7;
            this.txtLocationCode.TabStop = false;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(760, 105);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(48, 13);
            this.lblStatus.TabIndex = 168;
            this.lblStatus.Text = "Status:";
            // 
            // txtLocationAddress
            // 
            this.txtLocationAddress.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.txtLocationAddress.Location = new System.Drawing.Point(809, 17);
            this.txtLocationAddress.Multiline = true;
            this.txtLocationAddress.Name = "txtLocationAddress";
            this.txtLocationAddress.ReadOnly = true;
            this.txtLocationAddress.Size = new System.Drawing.Size(121, 80);
            this.txtLocationAddress.TabIndex = 0;
            this.txtLocationAddress.TabStop = false;
            // 
            // lblLocationName
            // 
            this.lblLocationName.AutoSize = true;
            this.lblLocationName.Location = new System.Drawing.Point(377, 22);
            this.lblLocationName.Name = "lblLocationName";
            this.lblLocationName.Size = new System.Drawing.Size(93, 13);
            this.lblLocationName.TabIndex = 5;
            this.lblLocationName.Text = "Location Code:";
            // 
            // lblLocationAddress
            // 
            this.lblLocationAddress.AutoSize = true;
            this.lblLocationAddress.Location = new System.Drawing.Point(701, 20);
            this.lblLocationAddress.Name = "lblLocationAddress";
            this.lblLocationAddress.Size = new System.Drawing.Size(109, 13);
            this.lblLocationAddress.TabIndex = 167;
            this.lblLocationAddress.Text = "Location Address:";
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Location = new System.Drawing.Point(60, 20);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(66, 13);
            this.lblLocation.TabIndex = 1;
            this.lblLocation.Text = "Location:*";
            // 
            // cmbFromBucket
            // 
            this.cmbFromBucket.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFromBucket.FormattingEnabled = true;
            this.cmbFromBucket.Location = new System.Drawing.Point(95, 89);
            this.cmbFromBucket.Name = "cmbFromBucket";
            this.cmbFromBucket.Size = new System.Drawing.Size(121, 21);
            this.cmbFromBucket.TabIndex = 6;
            this.cmbFromBucket.SelectedIndexChanged += new System.EventHandler(this.cmbFromBucket_SelectedIndexChanged);
            // 
            // txtItemCode
            // 
            this.txtItemCode.Location = new System.Drawing.Point(92, 9);
            this.txtItemCode.MaxLength = 20;
            this.txtItemCode.Name = "txtItemCode";
            this.txtItemCode.Size = new System.Drawing.Size(121, 21);
            this.txtItemCode.TabIndex = 1;
            this.txtItemCode.Validated += new System.EventHandler(this.txtItemCode_Validated);
            this.txtItemCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtItemCode_KeyDown);
            // 
            // txtWeight
            // 
            this.txtWeight.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.txtWeight.Location = new System.Drawing.Point(831, 9);
            this.txtWeight.Name = "txtWeight";
            this.txtWeight.ReadOnly = true;
            this.txtWeight.Size = new System.Drawing.Size(95, 21);
            this.txtWeight.TabIndex = 7;
            this.txtWeight.TabStop = false;
            this.txtWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtBatchNo
            // 
            this.txtBatchNo.Location = new System.Drawing.Point(535, 9);
            this.txtBatchNo.MaxLength = 20;
            this.txtBatchNo.Name = "txtBatchNo";
            this.txtBatchNo.Size = new System.Drawing.Size(121, 21);
            this.txtBatchNo.TabIndex = 5;
            this.txtBatchNo.Validated += new System.EventHandler(this.txtBatchNo_Validated);
            this.txtBatchNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBatchNo_KeyDown);
            this.txtBatchNo.Leave += new System.EventHandler(this.txtBatchNo_Leave);
            // 
            // lblReasonCode
            // 
            this.lblReasonCode.Location = new System.Drawing.Point(443, 90);
            this.lblReasonCode.Name = "lblReasonCode";
            this.lblReasonCode.Size = new System.Drawing.Size(95, 13);
            this.lblReasonCode.TabIndex = 9;
            this.lblReasonCode.Text = "Reason Code:*";
            this.lblReasonCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtItemDescription
            // 
            this.txtItemDescription.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.txtItemDescription.Location = new System.Drawing.Point(318, 9);
            this.txtItemDescription.Name = "txtItemDescription";
            this.txtItemDescription.ReadOnly = true;
            this.txtItemDescription.Size = new System.Drawing.Size(121, 21);
            this.txtItemDescription.TabIndex = 3;
            this.txtItemDescription.TabStop = false;
            // 
            // txtQty
            // 
            this.txtQty.Location = new System.Drawing.Point(95, 114);
            this.txtQty.MaxLength = 10;
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(121, 21);
            this.txtQty.TabIndex = 14;
            this.txtQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtQty.Leave += new System.EventHandler(this.txtQty_Leave);
            this.txtQty.Enter += new System.EventHandler(this.txtQty_Enter);
            // 
            // lblWeight
            // 
            this.lblWeight.AutoSize = true;
            this.lblWeight.Location = new System.Drawing.Point(774, 9);
            this.lblWeight.Name = "lblWeight";
            this.lblWeight.Size = new System.Drawing.Size(51, 13);
            this.lblWeight.TabIndex = 6;
            this.lblWeight.Text = "Weight:";
            // 
            // lblItemDescription
            // 
            this.lblItemDescription.AutoSize = true;
            this.lblItemDescription.Location = new System.Drawing.Point(242, 9);
            this.lblItemDescription.Name = "lblItemDescription";
            this.lblItemDescription.Size = new System.Drawing.Size(75, 13);
            this.lblItemDescription.TabIndex = 2;
            this.lblItemDescription.Text = "Item Desc.:";
            // 
            // lblBatchNo
            // 
            this.lblBatchNo.Location = new System.Drawing.Point(441, 9);
            this.lblBatchNo.Name = "lblBatchNo";
            this.lblBatchNo.Size = new System.Drawing.Size(93, 13);
            this.lblBatchNo.TabIndex = 4;
            this.lblBatchNo.Text = "Batch No:*";
            this.lblBatchNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblQty
            // 
            this.lblQty.AutoSize = true;
            this.lblQty.Location = new System.Drawing.Point(-1, 118);
            this.lblQty.Name = "lblQty";
            this.lblQty.Size = new System.Drawing.Size(95, 13);
            this.lblQty.TabIndex = 13;
            this.lblQty.Text = "Allocated Qty:*";
            // 
            // lblItemCode
            // 
            this.lblItemCode.AutoSize = true;
            this.lblItemCode.Location = new System.Drawing.Point(7, 9);
            this.lblItemCode.Name = "lblItemCode";
            this.lblItemCode.Size = new System.Drawing.Size(80, 13);
            this.lblItemCode.TabIndex = 0;
            this.lblItemCode.Text = "Item Code:*";
            // 
            // lblFromBucketName
            // 
            this.lblFromBucketName.AutoSize = true;
            this.lblFromBucketName.Location = new System.Drawing.Point(3, 92);
            this.lblFromBucketName.Name = "lblFromBucketName";
            this.lblFromBucketName.Size = new System.Drawing.Size(91, 13);
            this.lblFromBucketName.TabIndex = 5;
            this.lblFromBucketName.Text = "From Bucket:*";
            // 
            // lblToBukcetName
            // 
            this.lblToBukcetName.AutoSize = true;
            this.lblToBukcetName.Location = new System.Drawing.Point(242, 90);
            this.lblToBukcetName.Name = "lblToBukcetName";
            this.lblToBukcetName.Size = new System.Drawing.Size(76, 13);
            this.lblToBukcetName.TabIndex = 7;
            this.lblToBukcetName.Text = "To Bucket:*";
            // 
            // cmbToBucket
            // 
            this.cmbToBucket.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbToBucket.FormattingEnabled = true;
            this.cmbToBucket.Location = new System.Drawing.Point(321, 88);
            this.cmbToBucket.Name = "cmbToBucket";
            this.cmbToBucket.Size = new System.Drawing.Size(121, 21);
            this.cmbToBucket.TabIndex = 8;
            this.cmbToBucket.SelectedIndexChanged += new System.EventHandler(this.cmbToBucket_SelectedIndexChanged);
            // 
            // cmbReasonCode
            // 
            this.cmbReasonCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbReasonCode.DropDownWidth = 156;
            this.cmbReasonCode.FormattingEnabled = true;
            this.cmbReasonCode.Location = new System.Drawing.Point(537, 87);
            this.cmbReasonCode.Name = "cmbReasonCode";
            this.cmbReasonCode.Size = new System.Drawing.Size(140, 21);
            this.cmbReasonCode.TabIndex = 10;
            this.cmbReasonCode.SelectedIndexChanged += new System.EventHandler(this.cmbReasonCode_SelectedIndexChanged);
            // 
            // dgvInventory
            // 
            this.dgvInventory.AllowUserToAddRows = false;
            this.dgvInventory.AllowUserToDeleteRows = false;
            this.dgvInventory.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dgvInventory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInventory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvInventory.Location = new System.Drawing.Point(0, 0);
            this.dgvInventory.MultiSelect = false;
            this.dgvInventory.Name = "dgvInventory";
            this.dgvInventory.ReadOnly = true;
            this.dgvInventory.RowHeadersVisible = false;
            this.dgvInventory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvInventory.Size = new System.Drawing.Size(1005, 463);
            this.dgvInventory.TabIndex = 13;
            this.dgvInventory.TabStop = false;
            this.dgvInventory.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgvInventory_MouseDoubleClick);
            this.dgvInventory.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvInventory_CellContentClick);
            // 
            // dgvInventoryItem
            // 
            this.dgvInventoryItem.AllowUserToAddRows = false;
            this.dgvInventoryItem.AllowUserToDeleteRows = false;
            this.dgvInventoryItem.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dgvInventoryItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInventoryItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvInventoryItem.Location = new System.Drawing.Point(0, 179);
            this.dgvInventoryItem.MultiSelect = false;
            this.dgvInventoryItem.Name = "dgvInventoryItem";
            this.dgvInventoryItem.ReadOnly = true;
            this.dgvInventoryItem.RowHeadersVisible = false;
            this.dgvInventoryItem.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvInventoryItem.Size = new System.Drawing.Size(1005, 217);
            this.dgvInventoryItem.TabIndex = 1;
            this.dgvInventoryItem.TabStop = false;
            this.dgvInventoryItem.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvInventoryItem_CellClick);
            this.dgvInventoryItem.SelectionChanged += new System.EventHandler(this.dgvInventoryItem_SelectionChanged);
            // 
            // errInventory
            // 
            this.errInventory.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errInventory.ContainerControl = this;
            // 
            // lblAdjustNo
            // 
            this.lblAdjustNo.AutoSize = true;
            this.lblAdjustNo.Location = new System.Drawing.Point(76, 15);
            this.lblAdjustNo.Name = "lblAdjustNo";
            this.lblAdjustNo.Size = new System.Drawing.Size(53, 13);
            this.lblAdjustNo.TabIndex = 89;
            this.lblAdjustNo.Text = "Seq No:";
            // 
            // txtSeqNo
            // 
            this.txtSeqNo.Location = new System.Drawing.Point(125, 12);
            this.txtSeqNo.MaxLength = 20;
            this.txtSeqNo.Name = "txtSeqNo";
            this.txtSeqNo.Size = new System.Drawing.Size(121, 21);
            this.txtSeqNo.TabIndex = 0;
            // 
            // btnInitiated
            // 
            this.btnInitiated.BackColor = System.Drawing.Color.Transparent;
            this.btnInitiated.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnInitiated.BackgroundImage")));
            this.btnInitiated.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnInitiated.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnInitiated.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnInitiated.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnInitiated.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInitiated.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnInitiated.Location = new System.Drawing.Point(920, 0);
            this.btnInitiated.Name = "btnInitiated";
            this.btnInitiated.Size = new System.Drawing.Size(85, 32);
            this.btnInitiated.TabIndex = 3;
            this.btnInitiated.Text = "Ini&tiate";
            this.btnInitiated.UseVisualStyleBackColor = false;
            this.btnInitiated.Click += new System.EventHandler(this.btnInitiated_Click);
            // 
            // btnApproved
            // 
            this.btnApproved.BackColor = System.Drawing.Color.Transparent;
            this.btnApproved.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnApproved.BackgroundImage")));
            this.btnApproved.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnApproved.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnApproved.FlatAppearance.BorderSize = 0;
            this.btnApproved.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnApproved.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnApproved.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApproved.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnApproved.Location = new System.Drawing.Point(835, 0);
            this.btnApproved.Name = "btnApproved";
            this.btnApproved.Size = new System.Drawing.Size(85, 32);
            this.btnApproved.TabIndex = 4;
            this.btnApproved.Text = "Appr&ove";
            this.btnApproved.UseVisualStyleBackColor = false;
            this.btnApproved.Click += new System.EventHandler(this.btnApproved_Click);
            // 
            // lblAvailableQty
            // 
            this.lblAvailableQty.AutoSize = true;
            this.lblAvailableQty.Location = new System.Drawing.Point(737, 92);
            this.lblAvailableQty.Name = "lblAvailableQty";
            this.lblAvailableQty.Size = new System.Drawing.Size(95, 13);
            this.lblAvailableQty.TabIndex = 11;
            this.lblAvailableQty.Text = "Available Qty:*";
            this.lblAvailableQty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtAvailableQty
            // 
            this.txtAvailableQty.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.txtAvailableQty.Location = new System.Drawing.Point(834, 89);
            this.txtAvailableQty.MaxLength = 20;
            this.txtAvailableQty.Name = "txtAvailableQty";
            this.txtAvailableQty.ReadOnly = true;
            this.txtAvailableQty.Size = new System.Drawing.Size(150, 21);
            this.txtAvailableQty.TabIndex = 12;
            this.txtAvailableQty.TabStop = false;
            this.txtAvailableQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAvailableQty.Validated += new System.EventHandler(this.txtBatchNo_Validated);
            this.txtAvailableQty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBatchNo_KeyDown);
            // 
            // btnReject
            // 
            this.btnReject.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnReject.BackColor = System.Drawing.Color.Transparent;
            this.btnReject.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnReject.BackgroundImage")));
            this.btnReject.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnReject.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnReject.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnReject.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReject.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnReject.Location = new System.Drawing.Point(437, 1);
            this.btnReject.Name = "btnReject";
            this.btnReject.Size = new System.Drawing.Size(85, 31);
            this.btnReject.TabIndex = 5;
            this.btnReject.Text = "Re&ject";
            this.btnReject.UseVisualStyleBackColor = false;
            this.btnReject.Click += new System.EventHandler(this.btnReject_Click);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(27, 571);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(959, 33);
            this.panel1.TabIndex = 7;
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnPrint.BackColor = System.Drawing.Color.Transparent;
            this.btnPrint.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPrint.BackgroundImage")));
            this.btnPrint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPrint.FlatAppearance.BorderSize = 0;
            this.btnPrint.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnPrint.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnPrint.Location = new System.Drawing.Point(529, 0);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 32);
            this.btnPrint.TabIndex = 0;
            this.btnPrint.Text = "&Print";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
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
            this.btnCancel.Location = new System.Drawing.Point(604, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(77, 32);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Canc&el";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblApprovedQty
            // 
            this.lblApprovedQty.AutoSize = true;
            this.lblApprovedQty.Location = new System.Drawing.Point(221, 118);
            this.lblApprovedQty.Name = "lblApprovedQty";
            this.lblApprovedQty.Size = new System.Drawing.Size(98, 13);
            this.lblApprovedQty.TabIndex = 15;
            this.lblApprovedQty.Text = "Approved Qty:*";
            // 
            // txtApprovedQty
            // 
            this.txtApprovedQty.Location = new System.Drawing.Point(320, 115);
            this.txtApprovedQty.MaxLength = 10;
            this.txtApprovedQty.Name = "txtApprovedQty";
            this.txtApprovedQty.Size = new System.Drawing.Size(121, 21);
            this.txtApprovedQty.TabIndex = 16;
            this.txtApprovedQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtApprovedQty.TextChanged += new System.EventHandler(this.txtApprovedQty_TextChanged);
            this.txtApprovedQty.Enter += new System.EventHandler(this.txtQty_Enter);
            // 
            // lblReasonDescription
            // 
            this.lblReasonDescription.AutoSize = true;
            this.lblReasonDescription.Location = new System.Drawing.Point(4, 143);
            this.lblReasonDescription.Name = "lblReasonDescription";
            this.lblReasonDescription.Size = new System.Drawing.Size(90, 13);
            this.lblReasonDescription.TabIndex = 17;
            this.lblReasonDescription.Text = "Reason Desc.:";
            // 
            // txtReasonDescription
            // 
            this.txtReasonDescription.Location = new System.Drawing.Point(94, 140);
            this.txtReasonDescription.MaxLength = 100;
            this.txtReasonDescription.Name = "txtReasonDescription";
            this.txtReasonDescription.Size = new System.Drawing.Size(348, 21);
            this.txtReasonDescription.TabIndex = 18;
            this.txtReasonDescription.TextChanged += new System.EventHandler(this.txtReasonDescription_TextChanged);
            this.txtReasonDescription.Enter += new System.EventHandler(this.txtQty_Enter);
            // 
            // errSearch
            // 
            this.errSearch.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errSearch.ContainerControl = this;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::InventoryComponent.Properties.Resources.find;
            this.pictureBox3.Location = new System.Drawing.Point(217, 9);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(21, 21);
            this.pictureBox3.TabIndex = 176;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::InventoryComponent.Properties.Resources.find;
            this.pictureBox1.Location = new System.Drawing.Point(656, 9);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(21, 21);
            this.pictureBox1.TabIndex = 176;
            this.pictureBox1.TabStop = false;
            // 
            // lblAdjustmentNo
            // 
            this.lblAdjustmentNo.AutoSize = true;
            this.lblAdjustmentNo.Location = new System.Drawing.Point(22, 60);
            this.lblAdjustmentNo.Name = "lblAdjustmentNo";
            this.lblAdjustmentNo.Size = new System.Drawing.Size(96, 13);
            this.lblAdjustmentNo.TabIndex = 169;
            this.lblAdjustmentNo.Text = "Adjustment No:";
            // 
            // txtAdjustmentNo
            // 
            this.txtAdjustmentNo.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.txtAdjustmentNo.Location = new System.Drawing.Point(124, 57);
            this.txtAdjustmentNo.Name = "txtAdjustmentNo";
            this.txtAdjustmentNo.ReadOnly = true;
            this.txtAdjustmentNo.Size = new System.Drawing.Size(149, 21);
            this.txtAdjustmentNo.TabIndex = 0;
            this.txtAdjustmentNo.TabStop = false;
            // 
            // picToBatchNo
            // 
            this.picToBatchNo.Image = global::InventoryComponent.Properties.Resources.find;
            this.picToBatchNo.Location = new System.Drawing.Point(656, 10);
            this.picToBatchNo.Name = "picToBatchNo";
            this.picToBatchNo.Size = new System.Drawing.Size(21, 21);
            this.picToBatchNo.TabIndex = 187;
            this.picToBatchNo.TabStop = false;
            // 
            // picToitemcode
            // 
            this.picToitemcode.Image = global::InventoryComponent.Properties.Resources.find;
            this.picToitemcode.Location = new System.Drawing.Point(217, 10);
            this.picToitemcode.Name = "picToitemcode";
            this.picToitemcode.Size = new System.Drawing.Size(21, 21);
            this.picToitemcode.TabIndex = 188;
            this.picToitemcode.TabStop = false;
            // 
            // txtToitemcode
            // 
            this.txtToitemcode.Location = new System.Drawing.Point(92, 10);
            this.txtToitemcode.MaxLength = 20;
            this.txtToitemcode.Name = "txtToitemcode";
            this.txtToitemcode.Size = new System.Drawing.Size(121, 21);
            this.txtToitemcode.TabIndex = 1;
            this.txtToitemcode.Tag = "ToItem";
            this.txtToitemcode.TextChanged += new System.EventHandler(this.txtToitemcode_TextChanged);
            this.txtToitemcode.Validated += new System.EventHandler(this.txtItemCode_Validated);
            this.txtToitemcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtItemCode_KeyDown);
            this.txtToitemcode.Leave += new System.EventHandler(this.txtToitemcode_Leave);
            // 
            // txtToWeight
            // 
            this.txtToWeight.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.txtToWeight.Location = new System.Drawing.Point(831, 10);
            this.txtToWeight.Name = "txtToWeight";
            this.txtToWeight.ReadOnly = true;
            this.txtToWeight.Size = new System.Drawing.Size(95, 21);
            this.txtToWeight.TabIndex = 7;
            this.txtToWeight.TabStop = false;
            this.txtToWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtToBatchNo
            // 
            this.txtToBatchNo.Enabled = false;
            this.txtToBatchNo.Location = new System.Drawing.Point(536, 10);
            this.txtToBatchNo.MaxLength = 20;
            this.txtToBatchNo.Name = "txtToBatchNo";
            this.txtToBatchNo.Size = new System.Drawing.Size(121, 21);
            this.txtToBatchNo.TabIndex = 5;
            this.txtToBatchNo.Tag = "ToBatch";
            this.txtToBatchNo.Validated += new System.EventHandler(this.txtBatchNo_Validated);
            this.txtToBatchNo.Click += new System.EventHandler(this.txtToBatchNo_Click);
            this.txtToBatchNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBatchNo_KeyDown);
            this.txtToBatchNo.Leave += new System.EventHandler(this.txtToBatchNo_Leave);
            // 
            // txtToItemDesc
            // 
            this.txtToItemDesc.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.txtToItemDesc.Location = new System.Drawing.Point(318, 10);
            this.txtToItemDesc.Name = "txtToItemDesc";
            this.txtToItemDesc.ReadOnly = true;
            this.txtToItemDesc.Size = new System.Drawing.Size(121, 21);
            this.txtToItemDesc.TabIndex = 3;
            this.txtToItemDesc.TabStop = false;
            // 
            // lblToWeight
            // 
            this.lblToWeight.AutoSize = true;
            this.lblToWeight.Location = new System.Drawing.Point(774, 10);
            this.lblToWeight.Name = "lblToWeight";
            this.lblToWeight.Size = new System.Drawing.Size(51, 13);
            this.lblToWeight.TabIndex = 6;
            this.lblToWeight.Text = "Weight:";
            // 
            // lblToItemDesc
            // 
            this.lblToItemDesc.AutoSize = true;
            this.lblToItemDesc.Location = new System.Drawing.Point(242, 10);
            this.lblToItemDesc.Name = "lblToItemDesc";
            this.lblToItemDesc.Size = new System.Drawing.Size(75, 13);
            this.lblToItemDesc.TabIndex = 2;
            this.lblToItemDesc.Text = "Item Desc.:";
            // 
            // lblToBatchNo
            // 
            this.lblToBatchNo.Location = new System.Drawing.Point(441, 10);
            this.lblToBatchNo.Name = "lblToBatchNo";
            this.lblToBatchNo.Size = new System.Drawing.Size(93, 13);
            this.lblToBatchNo.TabIndex = 4;
            this.lblToBatchNo.Text = "Batch No:*";
            this.lblToBatchNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblToitemcode
            // 
            this.lblToitemcode.AutoSize = true;
            this.lblToitemcode.Location = new System.Drawing.Point(8, 10);
            this.lblToitemcode.Name = "lblToitemcode";
            this.lblToitemcode.Size = new System.Drawing.Size(80, 13);
            this.lblToitemcode.TabIndex = 0;
            this.lblToitemcode.Text = "Item Code:*";
            // 
            // grpBoxTo
            // 
            this.grpBoxTo.Controls.Add(this.pictureBox3);
            this.grpBoxTo.Controls.Add(this.lblItemCode);
            this.grpBoxTo.Controls.Add(this.lblBatchNo);
            this.grpBoxTo.Controls.Add(this.lblItemDescription);
            this.grpBoxTo.Controls.Add(this.lblWeight);
            this.grpBoxTo.Controls.Add(this.txtItemDescription);
            this.grpBoxTo.Controls.Add(this.txtBatchNo);
            this.grpBoxTo.Controls.Add(this.txtWeight);
            this.grpBoxTo.Controls.Add(this.txtItemCode);
            this.grpBoxTo.Controls.Add(this.pictureBox1);
            this.grpBoxTo.Location = new System.Drawing.Point(4, 24);
            this.grpBoxTo.Name = "grpBoxTo";
            this.grpBoxTo.Size = new System.Drawing.Size(1000, 30);
            this.grpBoxTo.TabIndex = 3;
            this.grpBoxTo.TabStop = false;
            this.grpBoxTo.Text = "From";
            // 
            // grpBoxFrom
            // 
            this.grpBoxFrom.Controls.Add(this.btnbatch);
            this.grpBoxFrom.Controls.Add(this.lblToitemcode);
            this.grpBoxFrom.Controls.Add(this.picToBatchNo);
            this.grpBoxFrom.Controls.Add(this.picToitemcode);
            this.grpBoxFrom.Controls.Add(this.lblToBatchNo);
            this.grpBoxFrom.Controls.Add(this.lblToItemDesc);
            this.grpBoxFrom.Controls.Add(this.txtToitemcode);
            this.grpBoxFrom.Controls.Add(this.lblToWeight);
            this.grpBoxFrom.Controls.Add(this.txtToWeight);
            this.grpBoxFrom.Controls.Add(this.txtToItemDesc);
            this.grpBoxFrom.Controls.Add(this.txtToBatchNo);
            this.grpBoxFrom.Enabled = false;
            this.grpBoxFrom.Location = new System.Drawing.Point(3, 52);
            this.grpBoxFrom.Name = "grpBoxFrom";
            this.grpBoxFrom.Size = new System.Drawing.Size(1000, 32);
            this.grpBoxFrom.TabIndex = 4;
            this.grpBoxFrom.TabStop = false;
            this.grpBoxFrom.Text = "To";
            // 
            // btnbatch
            // 
            this.btnbatch.Location = new System.Drawing.Point(678, 8);
            this.btnbatch.Name = "btnbatch";
            this.btnbatch.Size = new System.Drawing.Size(33, 22);
            this.btnbatch.TabIndex = 189;
            this.btnbatch.Text = "...";
            this.btnbatch.UseVisualStyleBackColor = true;
            this.btnbatch.Click += new System.EventHandler(this.btnbatch_Click);
            // 
            // chkExportIn
            // 
            this.chkExportIn.AutoSize = true;
            this.chkExportIn.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkExportIn.Location = new System.Drawing.Point(124, 126);
            this.chkExportIn.Name = "chkExportIn";
            this.chkExportIn.Size = new System.Drawing.Size(15, 14);
            this.chkExportIn.TabIndex = 2;
            this.chkExportIn.UseVisualStyleBackColor = true;
            this.chkExportIn.Click += new System.EventHandler(this.chkExportIn_Click);
            // 
            // chkisexport
            // 
            this.chkisexport.AutoSize = true;
            this.chkisexport.Checked = true;
            this.chkisexport.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.chkisexport.Location = new System.Drawing.Point(125, 0);
            this.chkisexport.Name = "chkisexport";
            this.chkisexport.Size = new System.Drawing.Size(15, 14);
            this.chkisexport.TabIndex = 5;
            this.chkisexport.ThreeState = true;
            this.chkisexport.UseVisualStyleBackColor = true;
            // 
            // lblisexport
            // 
            this.lblisexport.AutoSize = true;
            this.lblisexport.Location = new System.Drawing.Point(51, 1);
            this.lblisexport.Name = "lblisexport";
            this.lblisexport.Size = new System.Drawing.Size(78, 13);
            this.lblisexport.TabIndex = 6;
            this.lblisexport.Text = "Is Exported:";
            // 
            // lblexport
            // 
            this.lblexport.AutoSize = true;
            this.lblexport.Location = new System.Drawing.Point(38, 126);
            this.lblexport.Name = "lblexport";
            this.lblexport.Size = new System.Drawing.Size(80, 13);
            this.lblexport.TabIndex = 172;
            this.lblexport.Text = "Export Item:";
            // 
            // chkbatchadj
            // 
            this.chkbatchadj.AutoSize = true;
            this.chkbatchadj.Location = new System.Drawing.Point(476, 130);
            this.chkbatchadj.Name = "chkbatchadj";
            this.chkbatchadj.Size = new System.Drawing.Size(15, 14);
            this.chkbatchadj.TabIndex = 173;
            this.chkbatchadj.UseVisualStyleBackColor = true;
            this.chkbatchadj.Click += new System.EventHandler(this.chkbatchadj_Click);
            // 
            // lblbatchadj
            // 
            this.lblbatchadj.AutoSize = true;
            this.lblbatchadj.Location = new System.Drawing.Point(357, 126);
            this.lblbatchadj.Name = "lblbatchadj";
            this.lblbatchadj.Size = new System.Drawing.Size(113, 13);
            this.lblbatchadj.TabIndex = 174;
            this.lblbatchadj.Text = "Batch Adjustment:";
            // 
            // chkinternalbatadj
            // 
            this.chkinternalbatadj.AutoSize = true;
            this.chkinternalbatadj.Checked = true;
            this.chkinternalbatadj.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.chkinternalbatadj.Location = new System.Drawing.Point(483, 3);
            this.chkinternalbatadj.Name = "chkinternalbatadj";
            this.chkinternalbatadj.Size = new System.Drawing.Size(15, 14);
            this.chkinternalbatadj.TabIndex = 7;
            this.chkinternalbatadj.ThreeState = true;
            this.chkinternalbatadj.UseVisualStyleBackColor = true;
            // 
            // lblinterbatadj
            // 
            this.lblinterbatadj.AutoSize = true;
            this.lblinterbatadj.Location = new System.Drawing.Point(358, 0);
            this.lblinterbatadj.Name = "lblinterbatadj";
            this.lblinterbatadj.Size = new System.Drawing.Size(126, 13);
            this.lblinterbatadj.TabIndex = 8;
            this.lblinterbatadj.Text = "Internal Adjustment:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(452, 116);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "Initiated Qty: ";
            // 
            // textInitiatedQty
            // 
            this.textInitiatedQty.Enabled = false;
            this.textInitiatedQty.Location = new System.Drawing.Point(538, 112);
            this.textInitiatedQty.Name = "textInitiatedQty";
            this.textInitiatedQty.Size = new System.Drawing.Size(100, 21);
            this.textInitiatedQty.TabIndex = 23;
            // 
            // frmInventoryAdjustment
            // 
            this.AcceptButton = this.btnSearch;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1013, 703);
            this.Name = "frmInventoryAdjustment";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Stock Adjustment";
            this.WindowState = System.Windows.Forms.FormWindowState.Normal;
            this.Load += new System.EventHandler(this.frmInventoryAdjustment_Load);
            this.pnlCreateHeader.ResumeLayout(false);
            this.pnlCreateHeader.PerformLayout();
            this.grpAddDetails.ResumeLayout(false);
            this.pnlSearchHeader.ResumeLayout(false);
            this.pnlSearchHeader.PerformLayout();
            this.pnlSearchGrid.ResumeLayout(false);
            this.pnlCreateDetail.ResumeLayout(false);
            this.pnlCreateDetail.PerformLayout();
            this.pnlLowerButtons.ResumeLayout(false);
            this.pnlSearchButtons.ResumeLayout(false);
            this.pnlSearchButtons.PerformLayout();
            this.tabSearch.ResumeLayout(false);
            this.tabCreate.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInventory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInventoryItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errInventory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errSearch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picToBatchNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picToitemcode)).EndInit();
            this.grpBoxTo.ResumeLayout(false);
            this.grpBoxTo.PerformLayout();
            this.grpBoxFrom.ResumeLayout(false);
            this.grpBoxFrom.PerformLayout();
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
        private System.Windows.Forms.ComboBox cmbApprovedBy;
        private System.Windows.Forms.Label lblSearchApprovedBy;
        private System.Windows.Forms.ComboBox cmbLocation;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.TextBox txtInitiatedBy;
        private System.Windows.Forms.TextBox txtApprovedBy;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.Label lblInitiatedBy;
        private System.Windows.Forms.Label lblInitiatedDate;
        private System.Windows.Forms.Label lblApprovedBy;
        private System.Windows.Forms.TextBox txtLocationCode;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.TextBox txtLocationAddress;
        private System.Windows.Forms.Label lblLocationName;
        private System.Windows.Forms.Label lblLocationAddress;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.ComboBox cmbFromBucket;
        private System.Windows.Forms.TextBox txtItemCode;
        private System.Windows.Forms.TextBox txtWeight;
        private System.Windows.Forms.TextBox txtBatchNo;
        private System.Windows.Forms.Label lblReasonCode;
        private System.Windows.Forms.TextBox txtItemDescription;
        private System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.Label lblWeight;
        private System.Windows.Forms.Label lblItemDescription;
        private System.Windows.Forms.Label lblBatchNo;
        private System.Windows.Forms.Label lblQty;
        private System.Windows.Forms.Label lblItemCode;
        private System.Windows.Forms.Label lblFromBucketName;
        private System.Windows.Forms.ComboBox cmbToBucket;
        private System.Windows.Forms.Label lblToBukcetName;
        private System.Windows.Forms.ComboBox cmbReasonCode;
        private System.Windows.Forms.DataGridView dgvInventory;
        private System.Windows.Forms.DataGridView dgvInventoryItem;
        private System.Windows.Forms.ErrorProvider errInventory;
        private System.Windows.Forms.Label lblAdjustNo;
        private System.Windows.Forms.TextBox txtSeqNo;
        private System.Windows.Forms.Button btnApproved;
        private System.Windows.Forms.Button btnInitiated;
        private System.Windows.Forms.TextBox txtAvailableQty;
        private System.Windows.Forms.Label lblAvailableQty;
        private System.Windows.Forms.Button btnReject;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtApprovedQty;
        private System.Windows.Forms.Label lblApprovedQty;
        private System.Windows.Forms.TextBox txtReasonDescription;
        private System.Windows.Forms.Label lblReasonDescription;
        private System.Windows.Forms.ErrorProvider errSearch;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txtAdjustmentNo;
        private System.Windows.Forms.Label lblAdjustmentNo;
        private System.Windows.Forms.PictureBox picToBatchNo;
        private System.Windows.Forms.PictureBox picToitemcode;
        private System.Windows.Forms.TextBox txtToitemcode;
        private System.Windows.Forms.TextBox txtToWeight;
        public  System.Windows.Forms.TextBox txtToBatchNo;
        private System.Windows.Forms.TextBox txtToItemDesc;
        private System.Windows.Forms.Label lblToWeight;
        private System.Windows.Forms.Label lblToItemDesc;
        private System.Windows.Forms.Label lblToBatchNo;
        private System.Windows.Forms.Label lblToitemcode;
        private System.Windows.Forms.GroupBox grpBoxTo;
        private System.Windows.Forms.GroupBox grpBoxFrom;
        private System.Windows.Forms.CheckBox chkExportIn;
        private System.Windows.Forms.CheckBox chkisexport;
        private System.Windows.Forms.Label lblisexport;
        private System.Windows.Forms.Label lblexport;
        private System.Windows.Forms.CheckBox chkbatchadj;
        private System.Windows.Forms.Label lblbatchadj;
        private System.Windows.Forms.Button btnbatch;
        private System.Windows.Forms.CheckBox chkinternalbatadj;
        private System.Windows.Forms.Label lblinterbatadj;
        private System.Windows.Forms.TextBox textInitiatedQty;
        private System.Windows.Forms.Label label1;
    }
}

