namespace InventoryComponent.UI
{
    partial class frmStockCount
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmStockCount));
            this.cmbSearchLocation = new System.Windows.Forms.ComboBox();
            this.cmbSearchStatus = new System.Windows.Forms.ComboBox();
            this.dtpSearchTo = new System.Windows.Forms.DateTimePicker();
            this.dtpSearchFrom = new System.Windows.Forms.DateTimePicker();
            this.lblSearchFromDate = new System.Windows.Forms.Label();
            this.lblSearchToDate = new System.Windows.Forms.Label();
            this.lblSearchLocation = new System.Windows.Forms.Label();
            this.lblSearchStatus = new System.Windows.Forms.Label();
            this.lblLocation = new System.Windows.Forms.Label();
            this.lblLocationName = new System.Windows.Forms.Label();
            this.lblPhysicalQty = new System.Windows.Forms.Label();
            this.txtPhysicalQty = new System.Windows.Forms.TextBox();
            this.txtItemCode = new System.Windows.Forms.TextBox();
            this.lblSystemQty = new System.Windows.Forms.Label();
            this.txtSystemQty = new System.Windows.Forms.TextBox();
            this.lblItemCode = new System.Windows.Forms.Label();
            this.lblItemDescription = new System.Windows.Forms.Label();
            this.txtItemDescription = new System.Windows.Forms.TextBox();
            this.lblBatchNo = new System.Windows.Forms.Label();
            this.dgvStockItem = new System.Windows.Forms.DataGridView();
            this.dgvSearchStockCount = new System.Windows.Forms.DataGridView();
            this.txtLocationCode = new System.Windows.Forms.TextBox();
            this.txtBatchNo = new System.Windows.Forms.TextBox();
            this.lblBucketName = new System.Windows.Forms.Label();
            this.btnInitiated = new System.Windows.Forms.Button();
            this.btnCreated = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnProcessed = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnExecute = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.dtpStockCountDate = new System.Windows.Forms.DateTimePicker();
            this.lblStockCountDate = new System.Windows.Forms.Label();
            this.lblVerifiedBy = new System.Windows.Forms.Label();
            this.cmbStockCountBy = new System.Windows.Forms.ComboBox();
            this.cmbLocation = new System.Windows.Forms.ComboBox();
            this.lblRemarks = new System.Windows.Forms.Label();
            this.txtRemarks = new System.Windows.Forms.TextBox();
            this.lblStockCountVerifiedBy = new System.Windows.Forms.Label();
            this.txtStockCountVerifiedBy = new System.Windows.Forms.TextBox();
            this.lblStockCountBy = new System.Windows.Forms.Label();
            this.txtStockCountBy = new System.Windows.Forms.TextBox();
            this.cmbBucket = new System.Windows.Forms.ComboBox();
            this.txtSeqNo = new System.Windows.Forms.TextBox();
            this.lblAdjustNo = new System.Windows.Forms.Label();
            this.grpItemBatchDetails = new System.Windows.Forms.GroupBox();
            this.btnAddBatch = new System.Windows.Forms.Button();
            this.txtAvailableQty = new System.Windows.Forms.TextBox();
            this.lblAvailableQty = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.dgvStockItemBatch = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.errSearch = new System.Windows.Forms.ErrorProvider(this.components);
            this.errStockCount = new System.Windows.Forms.ErrorProvider(this.components);
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.lblStockCountNumber = new System.Windows.Forms.Label();
            this.txtStockCountNo = new System.Windows.Forms.TextBox();
            this.pnlCreateHeader.SuspendLayout();
            this.grpAddDetails.SuspendLayout();
            this.pnlSearchHeader.SuspendLayout();
            this.pnlSearchGrid.SuspendLayout();
            this.pnlCreateDetail.SuspendLayout();
            this.pnlLowerButtons.SuspendLayout();
            this.pnlSearchButtons.SuspendLayout();
            this.tabSearch.SuspendLayout();
            this.tabCreate.SuspendLayout();
            this.tabControlTransaction.SuspendLayout();
            //              this.tabControlTransaction.SuspendLayout();
            //              this.tabControlTransaction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStockItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearchStockCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStockItemBatch)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errSearch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errStockCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCreateHeader
            // 
            this.pnlCreateHeader.Controls.Add(this.cmbLocation);
            this.pnlCreateHeader.Controls.Add(this.dtpStockCountDate);
            this.pnlCreateHeader.Controls.Add(this.txtStockCountBy);
            this.pnlCreateHeader.Controls.Add(this.txtStockCountNo);
            this.pnlCreateHeader.Controls.Add(this.txtRemarks);
            this.pnlCreateHeader.Controls.Add(this.txtStockCountVerifiedBy);
            this.pnlCreateHeader.Controls.Add(this.txtStatus);
            this.pnlCreateHeader.Controls.Add(this.lblStockCountNumber);
            this.pnlCreateHeader.Controls.Add(this.lblStockCountBy);
            this.pnlCreateHeader.Controls.Add(this.lblStockCountDate);
            this.pnlCreateHeader.Controls.Add(this.lblRemarks);
            this.pnlCreateHeader.Controls.Add(this.lblStockCountVerifiedBy);
            this.pnlCreateHeader.Controls.Add(this.txtLocationCode);
            this.pnlCreateHeader.Controls.Add(this.lblStatus);
            this.pnlCreateHeader.Controls.Add(this.lblLocationName);
            this.pnlCreateHeader.Controls.Add(this.lblLocation);
            this.pnlCreateHeader.Size = new System.Drawing.Size(1005, 120);
            this.pnlCreateHeader.TabIndex = 0;
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblLocation, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblLocationName, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblStatus, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.txtLocationCode, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblStockCountVerifiedBy, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblRemarks, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblStockCountDate, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblStockCountBy, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblStockCountNumber, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.txtStatus, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.txtStockCountVerifiedBy, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.txtRemarks, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.txtStockCountNo, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.txtStockCountBy, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.dtpStockCountDate, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.cmbLocation, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.pnlTopButtons, 0);

            this.tabControlTransaction.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabControlTransaction_Selecting);

            // 
            // btnCreateReset
            // 
            this.btnCreateReset.FlatAppearance.BorderSize = 0;
            this.btnCreateReset.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnCreateReset.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnCreateReset.Location = new System.Drawing.Point(395, 0);
            this.btnCreateReset.TabIndex = 14;
            this.btnCreateReset.Click += new System.EventHandler(this.btnCreateReset_Click);
            // 
            // btnSave
            // 
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnSave.Location = new System.Drawing.Point(320, 0);
            this.btnSave.TabIndex = 13;
            this.btnSave.Visible = false;
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
            this.grpAddDetails.Controls.Add(this.panel2);
            this.grpAddDetails.Location = new System.Drawing.Point(0, 144);
            this.grpAddDetails.Size = new System.Drawing.Size(1005, 463);
            this.grpAddDetails.TabIndex = 1;
            this.grpAddDetails.Controls.SetChildIndex(this.panel2, 0);
            this.grpAddDetails.Controls.SetChildIndex(this.pnlLowerButtons, 0);
            this.grpAddDetails.Controls.SetChildIndex(this.pnlCreateDetail, 0);
            // 
            // btnClearDetails
            // 
            this.btnClearDetails.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnClearDetails.Dock = System.Windows.Forms.DockStyle.None;
            this.btnClearDetails.FlatAppearance.BorderSize = 0;
            this.btnClearDetails.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnClearDetails.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnClearDetails.Location = new System.Drawing.Point(874, 43);
            this.btnClearDetails.Size = new System.Drawing.Size(75, 32);
            this.btnClearDetails.Text = "Clear";
            this.btnClearDetails.Click += new System.EventHandler(this.btnClearDetails_Click);
            // 
            // btnAddDetails
            // 
            this.btnAddDetails.Dock = System.Windows.Forms.DockStyle.None;
            this.btnAddDetails.FlatAppearance.BorderSize = 0;
            this.btnAddDetails.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnAddDetails.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnAddDetails.Location = new System.Drawing.Point(794, 43);
            this.btnAddDetails.Size = new System.Drawing.Size(75, 32);
            this.btnAddDetails.Text = "Add";
            this.btnAddDetails.Click += new System.EventHandler(this.btnAddDetails_Click);
            // 
            // pnlSearchHeader
            // 
            this.pnlSearchHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSearchHeader.Controls.Add(this.txtSeqNo);
            this.pnlSearchHeader.Controls.Add(this.lblAdjustNo);
            this.pnlSearchHeader.Controls.Add(this.cmbSearchLocation);
            this.pnlSearchHeader.Controls.Add(this.cmbStockCountBy);
            this.pnlSearchHeader.Controls.Add(this.cmbSearchStatus);
            this.pnlSearchHeader.Controls.Add(this.dtpSearchTo);
            this.pnlSearchHeader.Controls.Add(this.dtpSearchFrom);
            this.pnlSearchHeader.Controls.Add(this.lblSearchFromDate);
            this.pnlSearchHeader.Controls.Add(this.lblSearchToDate);
            this.pnlSearchHeader.Controls.Add(this.lblVerifiedBy);
            this.pnlSearchHeader.Controls.Add(this.lblSearchLocation);
            this.pnlSearchHeader.Controls.Add(this.lblSearchStatus);
            this.pnlSearchHeader.Size = new System.Drawing.Size(1005, 140);
            this.pnlSearchHeader.TabIndex = 0;
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblSearchStatus, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblSearchLocation, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblVerifiedBy, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblSearchToDate, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblSearchFromDate, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.dtpSearchFrom, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.dtpSearchTo, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.cmbSearchStatus, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.cmbStockCountBy, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.cmbSearchLocation, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblAdjustNo, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.txtSeqNo, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.pnlSearchButtons, 0);
            // 
            // btnSearch
            // 
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearch.Location = new System.Drawing.Point(853, 0);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "S&earch";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnSearchReset
            // 
            this.btnSearchReset.FlatAppearance.BorderSize = 0;
            this.btnSearchReset.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearchReset.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearchReset.Location = new System.Drawing.Point(928, 0);
            this.btnSearchReset.TabIndex = 2;
            this.btnSearchReset.Click += new System.EventHandler(this.btnSearchReset_Click);
            // 
            // pnlSearchGrid
            // 
            this.pnlSearchGrid.Controls.Add(this.dgvSearchStockCount);
            this.pnlSearchGrid.Location = new System.Drawing.Point(0, 164);
            this.pnlSearchGrid.Size = new System.Drawing.Size(1005, 443);
            // 
            // pnlCreateDetail
            // 
            this.pnlCreateDetail.Controls.Add(this.pictureBox3);
            this.pnlCreateDetail.Controls.Add(this.cmbBucket);
            this.pnlCreateDetail.Controls.Add(this.dgvStockItem);
            this.pnlCreateDetail.Controls.Add(this.btnAddDetails);
            this.pnlCreateDetail.Controls.Add(this.txtItemCode);
            this.pnlCreateDetail.Controls.Add(this.txtItemDescription);
            this.pnlCreateDetail.Controls.Add(this.lblItemDescription);
            this.pnlCreateDetail.Controls.Add(this.lblItemCode);
            this.pnlCreateDetail.Controls.Add(this.lblBucketName);
            this.pnlCreateDetail.Controls.Add(this.btnClearDetails);
            this.pnlCreateDetail.Controls.Add(this.txtSystemQty);
            this.pnlCreateDetail.Controls.Add(this.lblSystemQty);
            this.pnlCreateDetail.Size = new System.Drawing.Size(1005, 180);
            this.pnlCreateDetail.TabIndex = 1;
            this.pnlCreateDetail.Controls.SetChildIndex(this.lblSystemQty, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.txtSystemQty, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.btnClearDetails, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.lblBucketName, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.lblItemCode, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.lblItemDescription, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.txtItemDescription, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.txtItemCode, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.btnAddDetails, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.dgvStockItem, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.cmbBucket, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.pictureBox3, 0);
            // 
            // pnlLowerButtons
            // 
            this.pnlLowerButtons.Controls.Add(this.btnPrint);
            this.pnlLowerButtons.Controls.Add(this.btnCreated);
            this.pnlLowerButtons.Controls.Add(this.btnCancel);
            this.pnlLowerButtons.Controls.Add(this.btnInitiated);
            this.pnlLowerButtons.Controls.Add(this.btnProcessed);
            this.pnlLowerButtons.Controls.Add(this.btnExecute);
            this.pnlLowerButtons.Controls.Add(this.btnClose);
            this.pnlLowerButtons.Location = new System.Drawing.Point(0, 431);
            this.pnlLowerButtons.TabIndex = 12;
            this.pnlLowerButtons.Controls.SetChildIndex(this.btnClose, 0);
            this.pnlLowerButtons.Controls.SetChildIndex(this.btnExecute, 0);
            this.pnlLowerButtons.Controls.SetChildIndex(this.btnProcessed, 0);
            this.pnlLowerButtons.Controls.SetChildIndex(this.btnInitiated, 0);
            this.pnlLowerButtons.Controls.SetChildIndex(this.btnCancel, 0);
            this.pnlLowerButtons.Controls.SetChildIndex(this.btnCreated, 0);
            this.pnlLowerButtons.Controls.SetChildIndex(this.btnPrint, 0);
            this.pnlLowerButtons.Controls.SetChildIndex(this.btnCreateReset, 0);
            this.pnlLowerButtons.Controls.SetChildIndex(this.btnSave, 0);
            // 
            // pnlTopButtons
            // 
            this.pnlTopButtons.Location = new System.Drawing.Point(0, 118);
            this.pnlTopButtons.Size = new System.Drawing.Size(1003, 0);
            this.pnlTopButtons.Visible = false;
            // 
            // pnlSearchButtons
            // 
            this.pnlSearchButtons.Location = new System.Drawing.Point(0, 106);
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
            // cmbSearchLocation
            // 
            this.cmbSearchLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSearchLocation.FormattingEnabled = true;
            this.cmbSearchLocation.Location = new System.Drawing.Point(462, 15);
            this.cmbSearchLocation.Name = "cmbSearchLocation";
            this.cmbSearchLocation.Size = new System.Drawing.Size(121, 21);
            this.cmbSearchLocation.TabIndex = 1;
            // 
            // cmbSearchStatus
            // 
            this.cmbSearchStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSearchStatus.FormattingEnabled = true;
            this.cmbSearchStatus.Location = new System.Drawing.Point(796, 15);
            this.cmbSearchStatus.Name = "cmbSearchStatus";
            this.cmbSearchStatus.Size = new System.Drawing.Size(121, 21);
            this.cmbSearchStatus.TabIndex = 2;
            // 
            // dtpSearchTo
            // 
            this.dtpSearchTo.Checked = false;
            this.dtpSearchTo.CustomFormat = "dd-MM-yyyy";
            this.dtpSearchTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpSearchTo.Location = new System.Drawing.Point(462, 44);
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
            this.dtpSearchFrom.Location = new System.Drawing.Point(130, 49);
            this.dtpSearchFrom.Name = "dtpSearchFrom";
            this.dtpSearchFrom.ShowCheckBox = true;
            this.dtpSearchFrom.Size = new System.Drawing.Size(121, 21);
            this.dtpSearchFrom.TabIndex = 3;
            this.dtpSearchFrom.Value = new System.DateTime(2009, 7, 14, 0, 0, 0, 0);
            // 
            // lblSearchFromDate
            // 
            this.lblSearchFromDate.AutoSize = true;
            this.lblSearchFromDate.Location = new System.Drawing.Point(8, 53);
            this.lblSearchFromDate.Name = "lblSearchFromDate";
            this.lblSearchFromDate.Size = new System.Drawing.Size(126, 13);
            this.lblSearchFromDate.TabIndex = 67;
            this.lblSearchFromDate.Text = "From (Initiate) Date:";
            // 
            // lblSearchToDate
            // 
            this.lblSearchToDate.AutoSize = true;
            this.lblSearchToDate.Location = new System.Drawing.Point(346, 48);
            this.lblSearchToDate.Name = "lblSearchToDate";
            this.lblSearchToDate.Size = new System.Drawing.Size(115, 13);
            this.lblSearchToDate.TabIndex = 79;
            this.lblSearchToDate.Text = "To  (Initiate) Date:";
            // 
            // lblSearchLocation
            // 
            this.lblSearchLocation.AutoSize = true;
            this.lblSearchLocation.Location = new System.Drawing.Point(402, 18);
            this.lblSearchLocation.Name = "lblSearchLocation";
            this.lblSearchLocation.Size = new System.Drawing.Size(59, 13);
            this.lblSearchLocation.TabIndex = 71;
            this.lblSearchLocation.Text = "Location:";
            // 
            // lblSearchStatus
            // 
            this.lblSearchStatus.AutoSize = true;
            this.lblSearchStatus.Location = new System.Drawing.Point(748, 18);
            this.lblSearchStatus.Name = "lblSearchStatus";
            this.lblSearchStatus.Size = new System.Drawing.Size(48, 13);
            this.lblSearchStatus.TabIndex = 72;
            this.lblSearchStatus.Text = "Status:";
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Location = new System.Drawing.Point(74, 10);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(66, 13);
            this.lblLocation.TabIndex = 138;
            this.lblLocation.Text = "Location:*";
            // 
            // lblLocationName
            // 
            this.lblLocationName.AutoSize = true;
            this.lblLocationName.Location = new System.Drawing.Point(409, 12);
            this.lblLocationName.Name = "lblLocationName";
            this.lblLocationName.Size = new System.Drawing.Size(93, 13);
            this.lblLocationName.TabIndex = 119;
            this.lblLocationName.Text = "Location Code:";
            // 
            // lblPhysicalQty
            // 
            this.lblPhysicalQty.AutoSize = true;
            this.lblPhysicalQty.Location = new System.Drawing.Point(297, 30);
            this.lblPhysicalQty.Name = "lblPhysicalQty";
            this.lblPhysicalQty.Size = new System.Drawing.Size(89, 13);
            this.lblPhysicalQty.TabIndex = 91;
            this.lblPhysicalQty.Text = "Physical Qty:*";
            // 
            // txtPhysicalQty
            // 
            this.txtPhysicalQty.Location = new System.Drawing.Point(385, 27);
            this.txtPhysicalQty.MaxLength = 10;
            this.txtPhysicalQty.Name = "txtPhysicalQty";
            this.txtPhysicalQty.Size = new System.Drawing.Size(121, 21);
            this.txtPhysicalQty.TabIndex = 1;
            this.txtPhysicalQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPhysicalQty.Enter += new System.EventHandler(this.txtPhysicalQty_Enter);
            // 
            // txtItemCode
            // 
            this.txtItemCode.Location = new System.Drawing.Point(104, 15);
            this.txtItemCode.MaxLength = 20;
            this.txtItemCode.Name = "txtItemCode";
            this.txtItemCode.Size = new System.Drawing.Size(121, 21);
            this.txtItemCode.TabIndex = 0;
            this.txtItemCode.Validated += new System.EventHandler(this.txtItemCode_Validated);
            this.txtItemCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtItemCode_KeyDown);
            // 
            // lblSystemQty
            // 
            this.lblSystemQty.AutoSize = true;
            this.lblSystemQty.Location = new System.Drawing.Point(771, 20);
            this.lblSystemQty.Name = "lblSystemQty";
            this.lblSystemQty.Size = new System.Drawing.Size(86, 13);
            this.lblSystemQty.TabIndex = 88;
            this.lblSystemQty.Text = "System Qty:*";
            this.lblSystemQty.Visible = false;
            // 
            // txtSystemQty
            // 
            this.txtSystemQty.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.txtSystemQty.Location = new System.Drawing.Point(859, 16);
            this.txtSystemQty.Name = "txtSystemQty";
            this.txtSystemQty.ReadOnly = true;
            this.txtSystemQty.Size = new System.Drawing.Size(121, 21);
            this.txtSystemQty.TabIndex = 4;
            this.txtSystemQty.TabStop = false;
            this.txtSystemQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSystemQty.Visible = false;
            // 
            // lblItemCode
            // 
            this.lblItemCode.AutoSize = true;
            this.lblItemCode.Location = new System.Drawing.Point(27, 18);
            this.lblItemCode.Name = "lblItemCode";
            this.lblItemCode.Size = new System.Drawing.Size(80, 13);
            this.lblItemCode.TabIndex = 84;
            this.lblItemCode.Text = "Item Code:*";
            // 
            // lblItemDescription
            // 
            this.lblItemDescription.AutoSize = true;
            this.lblItemDescription.Location = new System.Drawing.Point(276, 19);
            this.lblItemDescription.Name = "lblItemDescription";
            this.lblItemDescription.Size = new System.Drawing.Size(107, 13);
            this.lblItemDescription.TabIndex = 85;
            this.lblItemDescription.Text = "Item Description:";
            // 
            // txtItemDescription
            // 
            this.txtItemDescription.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.txtItemDescription.Location = new System.Drawing.Point(381, 15);
            this.txtItemDescription.Name = "txtItemDescription";
            this.txtItemDescription.ReadOnly = true;
            this.txtItemDescription.Size = new System.Drawing.Size(121, 21);
            this.txtItemDescription.TabIndex = 0;
            this.txtItemDescription.TabStop = false;
            // 
            // lblBatchNo
            // 
            this.lblBatchNo.AutoSize = true;
            this.lblBatchNo.Location = new System.Drawing.Point(35, 30);
            this.lblBatchNo.Name = "lblBatchNo";
            this.lblBatchNo.Size = new System.Drawing.Size(70, 13);
            this.lblBatchNo.TabIndex = 82;
            this.lblBatchNo.Text = "Batch No:*";
            this.lblBatchNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dgvStockItem
            // 
            this.dgvStockItem.AllowDrop = true;
            this.dgvStockItem.AllowUserToAddRows = false;
            this.dgvStockItem.AllowUserToDeleteRows = false;
            this.dgvStockItem.AllowUserToResizeColumns = false;
            this.dgvStockItem.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dgvStockItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStockItem.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvStockItem.Location = new System.Drawing.Point(0, 80);
            this.dgvStockItem.MultiSelect = false;
            this.dgvStockItem.Name = "dgvStockItem";
            this.dgvStockItem.ReadOnly = true;
            this.dgvStockItem.RowHeadersVisible = false;
            this.dgvStockItem.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvStockItem.Size = new System.Drawing.Size(1005, 100);
            this.dgvStockItem.TabIndex = 12;
            this.dgvStockItem.TabStop = false;
            this.dgvStockItem.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvStockItem_CellClick);
            this.dgvStockItem.SelectionChanged += new System.EventHandler(this.dgvStockItem_SelectionChanged);
            // 
            // dgvSearchStockCount
            // 
            this.dgvSearchStockCount.AllowUserToAddRows = false;
            this.dgvSearchStockCount.AllowUserToDeleteRows = false;
            this.dgvSearchStockCount.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dgvSearchStockCount.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSearchStockCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSearchStockCount.Location = new System.Drawing.Point(0, 0);
            this.dgvSearchStockCount.MultiSelect = false;
            this.dgvSearchStockCount.Name = "dgvSearchStockCount";
            this.dgvSearchStockCount.ReadOnly = true;
            this.dgvSearchStockCount.RowHeadersVisible = false;
            this.dgvSearchStockCount.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSearchStockCount.Size = new System.Drawing.Size(1005, 443);
            this.dgvSearchStockCount.TabIndex = 11;
            this.dgvSearchStockCount.TabStop = false;
            this.dgvSearchStockCount.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgvSearchStockCount_MouseDoubleClick);
            this.dgvSearchStockCount.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSearchStockCount_CellContentClick);
            // 
            // txtLocationCode
            // 
            this.txtLocationCode.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.txtLocationCode.Location = new System.Drawing.Point(504, 9);
            this.txtLocationCode.Multiline = true;
            this.txtLocationCode.Name = "txtLocationCode";
            this.txtLocationCode.ReadOnly = true;
            this.txtLocationCode.Size = new System.Drawing.Size(121, 21);
            this.txtLocationCode.TabIndex = 0;
            this.txtLocationCode.TabStop = false;
            // 
            // txtBatchNo
            // 
            this.txtBatchNo.Location = new System.Drawing.Point(111, 27);
            this.txtBatchNo.MaxLength = 20;
            this.txtBatchNo.Name = "txtBatchNo";
            this.txtBatchNo.Size = new System.Drawing.Size(121, 21);
            this.txtBatchNo.TabIndex = 0;
            this.txtBatchNo.Validated += new System.EventHandler(this.txtBatchNo_Validated);
            this.txtBatchNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBatchNo_KeyDown);
            // 
            // lblBucketName
            // 
            this.lblBucketName.AutoSize = true;
            this.lblBucketName.Location = new System.Drawing.Point(526, 18);
            this.lblBucketName.Name = "lblBucketName";
            this.lblBucketName.Size = new System.Drawing.Size(95, 13);
            this.lblBucketName.TabIndex = 88;
            this.lblBucketName.Text = "Bucket Name:*";
            // 
            // btnInitiated
            // 
            this.btnInitiated.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnInitiated.BackgroundImage")));
            this.btnInitiated.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnInitiated.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnInitiated.FlatAppearance.BorderSize = 0;
            this.btnInitiated.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnInitiated.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnInitiated.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInitiated.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnInitiated.Location = new System.Drawing.Point(695, 0);
            this.btnInitiated.Name = "btnInitiated";
            this.btnInitiated.Size = new System.Drawing.Size(75, 32);
            this.btnInitiated.TabIndex = 18;
            this.btnInitiated.Text = "Ini&tiate";
            this.btnInitiated.UseVisualStyleBackColor = false;
            this.btnInitiated.Click += new System.EventHandler(this.btnInitiated_Click);
            // 
            // btnCreated
            // 
            this.btnCreated.BackColor = System.Drawing.Color.Transparent;
            this.btnCreated.BackgroundImage = global::InventoryComponent.Properties.Resources.button;
            this.btnCreated.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCreated.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCreated.FlatAppearance.BorderSize = 0;
            this.btnCreated.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnCreated.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnCreated.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreated.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnCreated.Location = new System.Drawing.Point(545, 0);
            this.btnCreated.Name = "btnCreated";
            this.btnCreated.Size = new System.Drawing.Size(75, 32);
            this.btnCreated.TabIndex = 16;
            this.btnCreated.Text = "Cre&ate";
            this.btnCreated.UseVisualStyleBackColor = false;
            this.btnCreated.Click += new System.EventHandler(this.btnCreated_Click);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(9, 579);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(959, 33);
            this.panel1.TabIndex = 9;
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
            this.btnPrint.Location = new System.Drawing.Point(470, 0);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 32);
            this.btnPrint.TabIndex = 15;
            this.btnPrint.Text = "&Print";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnProcessed
            // 
            this.btnProcessed.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnProcessed.BackgroundImage")));
            this.btnProcessed.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnProcessed.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnProcessed.FlatAppearance.BorderSize = 0;
            this.btnProcessed.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnProcessed.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnProcessed.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProcessed.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnProcessed.Location = new System.Drawing.Point(770, 0);
            this.btnProcessed.Name = "btnProcessed";
            this.btnProcessed.Size = new System.Drawing.Size(85, 32);
            this.btnProcessed.TabIndex = 19;
            this.btnProcessed.Text = "Pr&ocess";
            this.btnProcessed.UseVisualStyleBackColor = false;
            this.btnProcessed.Click += new System.EventHandler(this.btnProcessed_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClose.BackgroundImage")));
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnClose.Location = new System.Drawing.Point(930, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 32);
            this.btnClose.TabIndex = 21;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnExecute
            // 
            this.btnExecute.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExecute.BackgroundImage")));
            this.btnExecute.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExecute.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnExecute.FlatAppearance.BorderSize = 0;
            this.btnExecute.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnExecute.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnExecute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExecute.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnExecute.Location = new System.Drawing.Point(855, 0);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(75, 32);
            this.btnExecute.TabIndex = 20;
            this.btnExecute.Text = "Exec&ute";
            this.btnExecute.UseVisualStyleBackColor = false;
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
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
            this.btnCancel.Location = new System.Drawing.Point(620, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 32);
            this.btnCancel.TabIndex = 17;
            this.btnCancel.Text = "Canc&el";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(790, 10);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(48, 13);
            this.lblStatus.TabIndex = 123;
            this.lblStatus.Text = "Status:";
            // 
            // txtStatus
            // 
            this.txtStatus.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.txtStatus.Location = new System.Drawing.Point(838, 7);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.ReadOnly = true;
            this.txtStatus.Size = new System.Drawing.Size(115, 21);
            this.txtStatus.TabIndex = 0;
            this.txtStatus.TabStop = false;
            // 
            // dtpStockCountDate
            // 
            this.dtpStockCountDate.Checked = false;
            this.dtpStockCountDate.CustomFormat = "dd-MM-yyyy";
            this.dtpStockCountDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStockCountDate.Location = new System.Drawing.Point(136, 36);
            this.dtpStockCountDate.Name = "dtpStockCountDate";
            this.dtpStockCountDate.ShowCheckBox = true;
            this.dtpStockCountDate.Size = new System.Drawing.Size(152, 21);
            this.dtpStockCountDate.TabIndex = 1;
            this.dtpStockCountDate.Value = new System.DateTime(2009, 7, 14, 0, 0, 0, 0);
            this.dtpStockCountDate.Validated += new System.EventHandler(this.dtpStockCountDate_Validated);
            // 
            // lblStockCountDate
            // 
            this.lblStockCountDate.AutoSize = true;
            this.lblStockCountDate.Location = new System.Drawing.Point(17, 40);
            this.lblStockCountDate.Name = "lblStockCountDate";
            this.lblStockCountDate.Size = new System.Drawing.Size(120, 13);
            this.lblStockCountDate.TabIndex = 124;
            this.lblStockCountDate.Text = "Stock Count Date:*";
            // 
            // lblVerifiedBy
            // 
            this.lblVerifiedBy.AutoSize = true;
            this.lblVerifiedBy.Location = new System.Drawing.Point(635, 48);
            this.lblVerifiedBy.Name = "lblVerifiedBy";
            this.lblVerifiedBy.Size = new System.Drawing.Size(161, 13);
            this.lblVerifiedBy.TabIndex = 72;
            this.lblVerifiedBy.Text = "Stock Count (Initiated) by:";
            // 
            // cmbStockCountBy
            // 
            this.cmbStockCountBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStockCountBy.FormattingEnabled = true;
            this.cmbStockCountBy.Location = new System.Drawing.Point(796, 45);
            this.cmbStockCountBy.Name = "cmbStockCountBy";
            this.cmbStockCountBy.Size = new System.Drawing.Size(121, 21);
            this.cmbStockCountBy.TabIndex = 5;
            // 
            // cmbLocation
            // 
            this.cmbLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocation.FormattingEnabled = true;
            this.cmbLocation.Location = new System.Drawing.Point(136, 9);
            this.cmbLocation.Name = "cmbLocation";
            this.cmbLocation.Size = new System.Drawing.Size(152, 21);
            this.cmbLocation.TabIndex = 0;
            this.cmbLocation.SelectedIndexChanged += new System.EventHandler(this.cmbLocation_SelectedIndexChanged);
            // 
            // lblRemarks
            // 
            this.lblRemarks.AutoSize = true;
            this.lblRemarks.Location = new System.Drawing.Point(75, 64);
            this.lblRemarks.Name = "lblRemarks";
            this.lblRemarks.Size = new System.Drawing.Size(63, 13);
            this.lblRemarks.TabIndex = 82;
            this.lblRemarks.Text = "Remarks:";
            this.lblRemarks.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtRemarks
            // 
            this.txtRemarks.Location = new System.Drawing.Point(136, 61);
            this.txtRemarks.MaxLength = 100;
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Size = new System.Drawing.Size(152, 21);
            this.txtRemarks.TabIndex = 2;
            // 
            // lblStockCountVerifiedBy
            // 
            this.lblStockCountVerifiedBy.AutoSize = true;
            this.lblStockCountVerifiedBy.Location = new System.Drawing.Point(761, 37);
            this.lblStockCountVerifiedBy.Name = "lblStockCountVerifiedBy";
            this.lblStockCountVerifiedBy.Size = new System.Drawing.Size(77, 13);
            this.lblStockCountVerifiedBy.TabIndex = 123;
            this.lblStockCountVerifiedBy.Text = "Initiated by:";
            // 
            // txtStockCountVerifiedBy
            // 
            this.txtStockCountVerifiedBy.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.txtStockCountVerifiedBy.Location = new System.Drawing.Point(838, 34);
            this.txtStockCountVerifiedBy.Name = "txtStockCountVerifiedBy";
            this.txtStockCountVerifiedBy.ReadOnly = true;
            this.txtStockCountVerifiedBy.Size = new System.Drawing.Size(115, 21);
            this.txtStockCountVerifiedBy.TabIndex = 0;
            this.txtStockCountVerifiedBy.TabStop = false;
            // 
            // lblStockCountBy
            // 
            this.lblStockCountBy.AutoSize = true;
            this.lblStockCountBy.Location = new System.Drawing.Point(423, 40);
            this.lblStockCountBy.Name = "lblStockCountBy";
            this.lblStockCountBy.Size = new System.Drawing.Size(82, 13);
            this.lblStockCountBy.TabIndex = 123;
            this.lblStockCountBy.Text = "Executed by:";
            // 
            // txtStockCountBy
            // 
            this.txtStockCountBy.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.txtStockCountBy.Location = new System.Drawing.Point(504, 37);
            this.txtStockCountBy.Name = "txtStockCountBy";
            this.txtStockCountBy.ReadOnly = true;
            this.txtStockCountBy.Size = new System.Drawing.Size(121, 21);
            this.txtStockCountBy.TabIndex = 0;
            this.txtStockCountBy.TabStop = false;
            // 
            // cmbBucket
            // 
            this.cmbBucket.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBucket.FormattingEnabled = true;
            this.cmbBucket.Location = new System.Drawing.Point(621, 16);
            this.cmbBucket.Name = "cmbBucket";
            this.cmbBucket.Size = new System.Drawing.Size(121, 21);
            this.cmbBucket.TabIndex = 1;
            this.cmbBucket.SelectedIndexChanged += new System.EventHandler(this.cmbBucket_SelectedIndexChanged);
            // 
            // txtSeqNo
            // 
            this.txtSeqNo.Location = new System.Drawing.Point(130, 15);
            this.txtSeqNo.MaxLength = 20;
            this.txtSeqNo.Name = "txtSeqNo";
            this.txtSeqNo.Size = new System.Drawing.Size(121, 21);
            this.txtSeqNo.TabIndex = 0;
            // 
            // lblAdjustNo
            // 
            this.lblAdjustNo.AutoSize = true;
            this.lblAdjustNo.Location = new System.Drawing.Point(81, 18);
            this.lblAdjustNo.Name = "lblAdjustNo";
            this.lblAdjustNo.Size = new System.Drawing.Size(53, 13);
            this.lblAdjustNo.TabIndex = 91;
            this.lblAdjustNo.Text = "Seq No:";
            // 
            // grpItemBatchDetails
            // 
            this.grpItemBatchDetails.Location = new System.Drawing.Point(0, 0);
            this.grpItemBatchDetails.Name = "grpItemBatchDetails";
            this.grpItemBatchDetails.Size = new System.Drawing.Size(200, 100);
            this.grpItemBatchDetails.TabIndex = 0;
            this.grpItemBatchDetails.TabStop = false;
            // 
            // btnAddBatch
            // 
            this.btnAddBatch.BackColor = System.Drawing.Color.Transparent;
            this.btnAddBatch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAddBatch.BackgroundImage")));
            this.btnAddBatch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAddBatch.FlatAppearance.BorderSize = 0;
            this.btnAddBatch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnAddBatch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnAddBatch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddBatch.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnAddBatch.Location = new System.Drawing.Point(788, 40);
            this.btnAddBatch.Name = "btnAddBatch";
            this.btnAddBatch.Size = new System.Drawing.Size(75, 32);
            this.btnAddBatch.TabIndex = 2;
            this.btnAddBatch.Text = "Add";
            this.btnAddBatch.UseVisualStyleBackColor = false;
            this.btnAddBatch.Click += new System.EventHandler(this.btnAddBatch_Click);
            // 
            // txtAvailableQty
            // 
            this.txtAvailableQty.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.txtAvailableQty.Location = new System.Drawing.Point(636, 27);
            this.txtAvailableQty.MaxLength = 10;
            this.txtAvailableQty.Name = "txtAvailableQty";
            this.txtAvailableQty.ReadOnly = true;
            this.txtAvailableQty.Size = new System.Drawing.Size(121, 21);
            this.txtAvailableQty.TabIndex = 6;
            this.txtAvailableQty.TabStop = false;
            this.txtAvailableQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAvailableQty.Visible = false;
            this.txtAvailableQty.Enter += new System.EventHandler(this.txtPhysicalQty_Enter);
            // 
            // lblAvailableQty
            // 
            this.lblAvailableQty.AutoSize = true;
            this.lblAvailableQty.Location = new System.Drawing.Point(540, 31);
            this.lblAvailableQty.Name = "lblAvailableQty";
            this.lblAvailableQty.Size = new System.Drawing.Size(95, 13);
            this.lblAvailableQty.TabIndex = 91;
            this.lblAvailableQty.Text = "Available Qty:*";
            this.lblAvailableQty.Visible = false;
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
            this.btnClear.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnClear.Location = new System.Drawing.Point(869, 40);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 32);
            this.btnClear.TabIndex = 3;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // dgvStockItemBatch
            // 
            this.dgvStockItemBatch.AllowUserToAddRows = false;
            this.dgvStockItemBatch.AllowUserToDeleteRows = false;
            this.dgvStockItemBatch.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dgvStockItemBatch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStockItemBatch.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvStockItemBatch.Location = new System.Drawing.Point(0, 79);
            this.dgvStockItemBatch.MultiSelect = false;
            this.dgvStockItemBatch.Name = "dgvStockItemBatch";
            this.dgvStockItemBatch.ReadOnly = true;
            this.dgvStockItemBatch.RowHeadersVisible = false;
            this.dgvStockItemBatch.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvStockItemBatch.Size = new System.Drawing.Size(990, 141);
            this.dgvStockItemBatch.TabIndex = 92;
            this.dgvStockItemBatch.TabStop = false;
            this.dgvStockItemBatch.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvStockItemBatch_CellClick);
            this.dgvStockItemBatch.SelectionChanged += new System.EventHandler(this.dgvStockItemBatch_SelectionChanged);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(117)))), ((int)(((byte)(186)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(990, 23);
            this.label1.TabIndex = 11;
            this.label1.Text = "Batch Details";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel2
            // 
            this.panel2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Controls.Add(this.btnAddBatch);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.btnClear);
            this.panel2.Controls.Add(this.txtAvailableQty);
            this.panel2.Controls.Add(this.lblAvailableQty);
            this.panel2.Controls.Add(this.txtPhysicalQty);
            this.panel2.Controls.Add(this.lblPhysicalQty);
            this.panel2.Controls.Add(this.txtBatchNo);
            this.panel2.Controls.Add(this.lblBatchNo);
            this.panel2.Controls.Add(this.dgvStockItemBatch);
            this.panel2.Location = new System.Drawing.Point(6, 202);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(990, 220);
            this.panel2.TabIndex = 93;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::InventoryComponent.Properties.Resources.find;
            this.pictureBox1.Location = new System.Drawing.Point(247, 26);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(21, 21);
            this.pictureBox1.TabIndex = 178;
            this.pictureBox1.TabStop = false;
            // 
            // errSearch
            // 
            this.errSearch.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errSearch.ContainerControl = this;
            // 
            // errStockCount
            // 
            this.errStockCount.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errStockCount.ContainerControl = this;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::InventoryComponent.Properties.Resources.find;
            this.pictureBox3.Location = new System.Drawing.Point(239, 15);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(21, 21);
            this.pictureBox3.TabIndex = 177;
            this.pictureBox3.TabStop = false;
            // 
            // lblStockCountNumber
            // 
            this.lblStockCountNumber.AutoSize = true;
            this.lblStockCountNumber.Location = new System.Drawing.Point(400, 64);
            this.lblStockCountNumber.Name = "lblStockCountNumber";
            this.lblStockCountNumber.Size = new System.Drawing.Size(105, 13);
            this.lblStockCountNumber.TabIndex = 123;
            this.lblStockCountNumber.Text = "Stock Count No.:";
            // 
            // txtStockCountNo
            // 
            this.txtStockCountNo.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.txtStockCountNo.Location = new System.Drawing.Point(504, 61);
            this.txtStockCountNo.Name = "txtStockCountNo";
            this.txtStockCountNo.ReadOnly = true;
            this.txtStockCountNo.Size = new System.Drawing.Size(121, 21);
            this.txtStockCountNo.TabIndex = 0;
            this.txtStockCountNo.TabStop = false;
            // 
            // frmStockCount
            // 
            this.AcceptButton = this.btnSearch;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1013, 703);
            this.Name = "frmStockCount";
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
            this.pnlSearchButtons.ResumeLayout(false);
            this.tabSearch.ResumeLayout(false);
            this.tabCreate.ResumeLayout(false);
            this.tabControlTransaction.ResumeLayout(false);
            //this.tabControlTransaction.ResumeLayout(false);
            //this.tabControlTransaction.ResumeLayout(false);
            //  this.tabControlTransaction.ResumeLayout(false);
            //  this.tabControlTransaction.ResumeLayout(false);
            //  this.tabControlTransaction.ResumeLayout(false);

            
            ((System.ComponentModel.ISupportInitialize)(this.dgvStockItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearchStockCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStockItemBatch)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errSearch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errStockCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
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
        private System.Windows.Forms.Label lblLocationName;
        private System.Windows.Forms.Label lblPhysicalQty;
        private System.Windows.Forms.TextBox txtPhysicalQty;
        private System.Windows.Forms.TextBox txtItemCode;
        private System.Windows.Forms.Label lblSystemQty;
        private System.Windows.Forms.TextBox txtSystemQty;
        private System.Windows.Forms.Label lblItemCode;
        private System.Windows.Forms.Label lblItemDescription;
        private System.Windows.Forms.TextBox txtItemDescription;
        private System.Windows.Forms.Label lblBatchNo;
        private System.Windows.Forms.DataGridView dgvStockItem;
        private System.Windows.Forms.DataGridView dgvSearchStockCount;
        private System.Windows.Forms.TextBox txtLocationCode;
        private System.Windows.Forms.TextBox txtBatchNo;
        private System.Windows.Forms.Label lblBucketName;
        
        private System.Windows.Forms.Button btnInitiated;
        private System.Windows.Forms.Button btnCreated;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.DateTimePicker dtpStockCountDate;
        private System.Windows.Forms.Label lblStockCountDate;
        private System.Windows.Forms.ComboBox cmbStockCountBy;
        private System.Windows.Forms.Label lblVerifiedBy;
        private System.Windows.Forms.ComboBox cmbLocation;
        private System.Windows.Forms.TextBox txtRemarks;
        private System.Windows.Forms.Label lblRemarks;
        private System.Windows.Forms.Button btnProcessed;
        private System.Windows.Forms.TextBox txtStockCountBy;
        private System.Windows.Forms.TextBox txtStockCountVerifiedBy;
        private System.Windows.Forms.Label lblStockCountBy;
        private System.Windows.Forms.Label lblStockCountVerifiedBy;
        private System.Windows.Forms.ComboBox cmbBucket;
        private System.Windows.Forms.TextBox txtSeqNo;
        private System.Windows.Forms.Label lblAdjustNo;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.GroupBox grpItemBatchDetails;
        private System.Windows.Forms.DataGridView dgvStockItemBatch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAddBatch;
        private System.Windows.Forms.Button btnClear;
        
        private System.Windows.Forms.TextBox txtAvailableQty;
        private System.Windows.Forms.Label lblAvailableQty;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ErrorProvider errSearch;
        private System.Windows.Forms.ErrorProvider errStockCount;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.TextBox txtStockCountNo;
        private System.Windows.Forms.Label lblStockCountNumber;
    }
}