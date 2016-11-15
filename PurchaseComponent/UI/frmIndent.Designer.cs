namespace PurchaseComponent.UI
{
    partial class frmIndent
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
        protected void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmIndent));
            this.lblStatus = new System.Windows.Forms.Label();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.lblLocationSearch = new System.Windows.Forms.Label();
            this.lblToDate = new System.Windows.Forms.Label();
            this.lblFromDate = new System.Windows.Forms.Label();
            this.txtIndentNo = new System.Windows.Forms.TextBox();
            this.cmbLocation = new System.Windows.Forms.ComboBox();
            this.lblIndentNo = new System.Windows.Forms.Label();
            this.lblCreateStaus = new System.Windows.Forms.Label();
            this.lblCreateLocationCode = new System.Windows.Forms.Label();
            this.lblLocation = new System.Windows.Forms.Label();
            this.lblIndentDate = new System.Windows.Forms.Label();
            this.txtAvgSales = new System.Windows.Forms.TextBox();
            this.txtStockInHand = new System.Windows.Forms.TextBox();
            this.txtApprovedPOQty = new System.Windows.Forms.TextBox();
            this.txtRequestedQty = new System.Windows.Forms.TextBox();
            this.txtApprovedToQty = new System.Windows.Forms.TextBox();
            this.txtItemName = new System.Windows.Forms.TextBox();
            this.txtStockInTransit = new System.Windows.Forms.TextBox();
            this.txtApprovedHOQty = new System.Windows.Forms.TextBox();
            this.txtItemCode = new System.Windows.Forms.TextBox();
            this.lblAvgSale = new System.Windows.Forms.Label();
            this.lblStockInHand = new System.Windows.Forms.Label();
            this.lblApprovedPOQty = new System.Windows.Forms.Label();
            this.lblRequestedQty = new System.Windows.Forms.Label();
            this.lblTONo = new System.Windows.Forms.Label();
            this.lblApprovedToQty = new System.Windows.Forms.Label();
            this.lblItemName = new System.Windows.Forms.Label();
            this.lblStockInTransit = new System.Windows.Forms.Label();
            this.lblPONo = new System.Windows.Forms.Label();
            this.lblApprovedHOQty = new System.Windows.Forms.Label();
            this.lblItemCode = new System.Windows.Forms.Label();
            this.lblCreateIndentNo = new System.Windows.Forms.Label();
            this.lblIndentNoValue = new System.Windows.Forms.Label();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.errCreateIndent = new System.Windows.Forms.ErrorProvider(this.components);
            this.lblLocationCode = new System.Windows.Forms.Label();
            this.lblStatusValue = new System.Windows.Forms.Label();
            this.lblLocationName = new System.Windows.Forms.Label();
            this.btnReject = new System.Windows.Forms.Button();
            this.btnApproved = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnAddPO_TO = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.dgvIndentDetails = new System.Windows.Forms.DataGridView();
            this.lblLocationType = new System.Windows.Forms.Label();
            this.errorSearch = new System.Windows.Forms.ErrorProvider(this.components);
            this.lblCreateDate = new System.Windows.Forms.Label();
            this.lblCreateRemark = new System.Windows.Forms.Label();
            this.txtCreateRemark = new System.Windows.Forms.TextBox();
            this.lblCreateIndentDateValue = new System.Windows.Forms.Label();
            this.lblCreateIndentDate = new System.Windows.Forms.Label();
            this.lblApprovedDateValue = new System.Windows.Forms.Label();
            this.lblApprovedDate = new System.Windows.Forms.Label();
            this.lstPONumber = new System.Windows.Forms.ListBox();
            this.lstTONumber = new System.Windows.Forms.ListBox();
            this.dgvIndent = new System.Windows.Forms.DataGridView();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtAvgStockTransfer = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTotalStk = new System.Windows.Forms.TextBox();
            this.txtSaleStkTransf = new System.Windows.Forms.TextBox();
            this.lblTotalSaleStkTrnas = new System.Windows.Forms.Label();
            this.lblTotalStock = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbIndentFromLocation = new System.Windows.Forms.ComboBox();
            this.pnlCreateHeader.SuspendLayout();
            this.grpAddDetails.SuspendLayout();
            this.pnlSearchHeader.SuspendLayout();
            this.pnlSearchGrid.SuspendLayout();
            this.pnlCreateDetail.SuspendLayout();
            this.pnlLowerButtons.SuspendLayout();
            this.pnlSearchButtons.SuspendLayout();
            this.tabSearch.SuspendLayout();
            this.tabCreate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errCreateIndent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIndentDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorSearch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIndent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCreateHeader
            // 
            this.pnlCreateHeader.Controls.Add(this.lblIndentDate);
            this.pnlCreateHeader.Controls.Add(this.lblApprovedDateValue);
            this.pnlCreateHeader.Controls.Add(this.lblApprovedDate);
            this.pnlCreateHeader.Controls.Add(this.lblCreateIndentDateValue);
            this.pnlCreateHeader.Controls.Add(this.lblCreateIndentDate);
            this.pnlCreateHeader.Controls.Add(this.lblCreateDate);
            this.pnlCreateHeader.Controls.Add(this.lblLocationName);
            this.pnlCreateHeader.Controls.Add(this.lblStatusValue);
            this.pnlCreateHeader.Controls.Add(this.lblLocationCode);
            this.pnlCreateHeader.Controls.Add(this.lblIndentNoValue);
            this.pnlCreateHeader.Controls.Add(this.lblCreateIndentNo);
            this.pnlCreateHeader.Controls.Add(this.txtCreateRemark);
            this.pnlCreateHeader.Controls.Add(this.lblCreateRemark);
            this.pnlCreateHeader.Controls.Add(this.lblCreateStaus);
            this.pnlCreateHeader.Controls.Add(this.lblCreateLocationCode);
            this.pnlCreateHeader.Controls.Add(this.lblLocation);
            this.pnlCreateHeader.Size = new System.Drawing.Size(1005, 120);
            this.pnlCreateHeader.TabIndex = 0;
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblLocation, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblCreateLocationCode, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblCreateStaus, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblCreateRemark, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.txtCreateRemark, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblCreateIndentNo, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblIndentNoValue, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblLocationCode, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblStatusValue, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblLocationName, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblCreateDate, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblCreateIndentDate, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblCreateIndentDateValue, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblApprovedDate, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblApprovedDateValue, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblIndentDate, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.pnlTopButtons, 0);
            // 
            // btnCreateReset
            // 
            this.btnCreateReset.FlatAppearance.BorderSize = 0;
            this.btnCreateReset.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnCreateReset.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnCreateReset.Location = new System.Drawing.Point(480, 0);
            this.btnCreateReset.Click += new System.EventHandler(this.btnCreateReset_Click);
            // 
            // btnSave
            // 
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSave.Location = new System.Drawing.Point(405, 0);
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblAddDetails
            // 
            this.lblAddDetails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(117)))), ((int)(((byte)(186)))));
            this.lblAddDetails.ForeColor = System.Drawing.Color.White;
            this.lblAddDetails.Text = "Product Details";
            // 
            // grpAddDetails
            // 
            this.grpAddDetails.Controls.Add(this.label1);
            this.grpAddDetails.Controls.Add(this.dgvIndentDetails);
            this.grpAddDetails.Location = new System.Drawing.Point(0, 144);
            this.grpAddDetails.Size = new System.Drawing.Size(1005, 463);
            this.grpAddDetails.Controls.SetChildIndex(this.pnlLowerButtons, 0);
            this.grpAddDetails.Controls.SetChildIndex(this.pnlCreateDetail, 0);
            this.grpAddDetails.Controls.SetChildIndex(this.dgvIndentDetails, 0);
            this.grpAddDetails.Controls.SetChildIndex(this.label1, 0);
            // 
            // btnClearDetails
            // 
            this.btnClearDetails.Dock = System.Windows.Forms.DockStyle.None;
            this.btnClearDetails.FlatAppearance.BorderSize = 0;
            this.btnClearDetails.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnClearDetails.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnClearDetails.Location = new System.Drawing.Point(899, 137);
            this.btnClearDetails.Click += new System.EventHandler(this.btnClearDetails_Click);
            // 
            // btnAddDetails
            // 
            this.btnAddDetails.Dock = System.Windows.Forms.DockStyle.None;
            this.btnAddDetails.FlatAppearance.BorderSize = 0;
            this.btnAddDetails.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnAddDetails.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnAddDetails.Location = new System.Drawing.Point(819, 137);
            this.btnAddDetails.Click += new System.EventHandler(this.btnAddDetails_Click);
            // 
            // pnlSearchHeader
            // 
            this.pnlSearchHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSearchHeader.Controls.Add(this.dtpToDate);
            this.pnlSearchHeader.Controls.Add(this.dtpFromDate);
            this.pnlSearchHeader.Controls.Add(this.lblIndentNo);
            this.pnlSearchHeader.Controls.Add(this.lblStatus);
            this.pnlSearchHeader.Controls.Add(this.lblFromDate);
            this.pnlSearchHeader.Controls.Add(this.lblToDate);
            this.pnlSearchHeader.Controls.Add(this.cmbStatus);
            this.pnlSearchHeader.Controls.Add(this.txtIndentNo);
            this.pnlSearchHeader.Controls.Add(this.cmbLocation);
            this.pnlSearchHeader.Controls.Add(this.lblLocationSearch);
            this.pnlSearchHeader.Size = new System.Drawing.Size(1005, 120);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblLocationSearch, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.cmbLocation, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.txtIndentNo, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.cmbStatus, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblToDate, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblFromDate, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblStatus, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblIndentNo, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.dtpFromDate, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.dtpToDate, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.pnlSearchButtons, 0);
            // 
            // btnSearch
            // 
            this.btnSearch.BackgroundImage = global::PurchaseComponent.Properties.Resources.button;
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearch.Location = new System.Drawing.Point(853, 0);
            this.btnSearch.TabIndex = 5;
            this.btnSearch.Text = "&Search";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnSearchReset
            // 
            this.btnSearchReset.FlatAppearance.BorderSize = 0;
            this.btnSearchReset.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearchReset.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearchReset.Location = new System.Drawing.Point(928, 0);
            this.btnSearchReset.TabIndex = 6;
            this.btnSearchReset.Click += new System.EventHandler(this.btnSearchReset_Click);
            // 
            // pnlSearchGrid
            // 
            this.pnlSearchGrid.Controls.Add(this.dgvIndent);
            this.pnlSearchGrid.Location = new System.Drawing.Point(0, 144);
            this.pnlSearchGrid.Size = new System.Drawing.Size(1005, 463);
            // 
            // pnlCreateDetail
            // 
            this.pnlCreateDetail.Controls.Add(this.cmbIndentFromLocation);
            this.pnlCreateDetail.Controls.Add(this.label2);
            this.pnlCreateDetail.Controls.Add(this.txtTotalStk);
            this.pnlCreateDetail.Controls.Add(this.txtSaleStkTransf);
            this.pnlCreateDetail.Controls.Add(this.lblTotalSaleStkTrnas);
            this.pnlCreateDetail.Controls.Add(this.lblTotalStock);
            this.pnlCreateDetail.Controls.Add(this.txtAvgStockTransfer);
            this.pnlCreateDetail.Controls.Add(this.label3);
            this.pnlCreateDetail.Controls.Add(this.pictureBox1);
            this.pnlCreateDetail.Controls.Add(this.lstTONumber);
            this.pnlCreateDetail.Controls.Add(this.lstPONumber);
            this.pnlCreateDetail.Controls.Add(this.btnAddDetails);
            this.pnlCreateDetail.Controls.Add(this.btnClearDetails);
            this.pnlCreateDetail.Controls.Add(this.txtStockInTransit);
            this.pnlCreateDetail.Controls.Add(this.txtAvgSales);
            this.pnlCreateDetail.Controls.Add(this.txtStockInHand);
            this.pnlCreateDetail.Controls.Add(this.txtApprovedPOQty);
            this.pnlCreateDetail.Controls.Add(this.txtRequestedQty);
            this.pnlCreateDetail.Controls.Add(this.txtApprovedToQty);
            this.pnlCreateDetail.Controls.Add(this.txtApprovedHOQty);
            this.pnlCreateDetail.Controls.Add(this.txtItemCode);
            this.pnlCreateDetail.Controls.Add(this.txtItemName);
            this.pnlCreateDetail.Controls.Add(this.lblAvgSale);
            this.pnlCreateDetail.Controls.Add(this.lblTONo);
            this.pnlCreateDetail.Controls.Add(this.lblRequestedQty);
            this.pnlCreateDetail.Controls.Add(this.lblStockInHand);
            this.pnlCreateDetail.Controls.Add(this.lblApprovedToQty);
            this.pnlCreateDetail.Controls.Add(this.lblItemCode);
            this.pnlCreateDetail.Controls.Add(this.lblApprovedPOQty);
            this.pnlCreateDetail.Controls.Add(this.lblStockInTransit);
            this.pnlCreateDetail.Controls.Add(this.lblPONo);
            this.pnlCreateDetail.Controls.Add(this.lblItemName);
            this.pnlCreateDetail.Controls.Add(this.lblApprovedHOQty);
            this.pnlCreateDetail.Size = new System.Drawing.Size(1005, 172);
            this.pnlCreateDetail.Controls.SetChildIndex(this.lblApprovedHOQty, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.lblItemName, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.lblPONo, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.lblStockInTransit, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.lblApprovedPOQty, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.lblItemCode, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.lblApprovedToQty, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.lblStockInHand, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.lblRequestedQty, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.lblTONo, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.lblAvgSale, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.txtItemName, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.txtItemCode, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.txtApprovedHOQty, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.txtApprovedToQty, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.txtRequestedQty, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.txtApprovedPOQty, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.txtStockInHand, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.txtAvgSales, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.txtStockInTransit, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.btnClearDetails, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.btnAddDetails, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.lstPONumber, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.lstTONumber, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.pictureBox1, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.label3, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.txtAvgStockTransfer, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.lblTotalStock, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.lblTotalSaleStkTrnas, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.txtSaleStkTransf, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.txtTotalStk, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.label2, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.cmbIndentFromLocation, 0);
            // 
            // pnlLowerButtons
            // 
            this.pnlLowerButtons.Controls.Add(this.btnConfirm);
            this.pnlLowerButtons.Controls.Add(this.btnCancel);
            this.pnlLowerButtons.Controls.Add(this.btnAddPO_TO);
            this.pnlLowerButtons.Controls.Add(this.btnPrint);
            this.pnlLowerButtons.Controls.Add(this.btnApproved);
            this.pnlLowerButtons.Controls.Add(this.btnReject);
            this.pnlLowerButtons.Location = new System.Drawing.Point(0, 431);
            this.pnlLowerButtons.Controls.SetChildIndex(this.btnReject, 0);
            this.pnlLowerButtons.Controls.SetChildIndex(this.btnApproved, 0);
            this.pnlLowerButtons.Controls.SetChildIndex(this.btnPrint, 0);
            this.pnlLowerButtons.Controls.SetChildIndex(this.btnAddPO_TO, 0);
            this.pnlLowerButtons.Controls.SetChildIndex(this.btnCancel, 0);
            this.pnlLowerButtons.Controls.SetChildIndex(this.btnConfirm, 0);
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
            this.pnlSearchButtons.Location = new System.Drawing.Point(0, 86);
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
            // lblStatus
            // 
            this.lblStatus.Location = new System.Drawing.Point(296, 65);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(100, 13);
            this.lblStatus.TabIndex = 55;
            this.lblStatus.Text = "Status:";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbStatus
            // 
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Location = new System.Drawing.Point(402, 62);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(199, 21);
            this.cmbStatus.TabIndex = 4;
            // 
            // lblLocationSearch
            // 
            this.lblLocationSearch.Location = new System.Drawing.Point(99, 62);
            this.lblLocationSearch.Name = "lblLocationSearch";
            this.lblLocationSearch.Size = new System.Drawing.Size(75, 13);
            this.lblLocationSearch.TabIndex = 51;
            this.lblLocationSearch.Text = "Location:";
            this.lblLocationSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblToDate
            // 
            this.lblToDate.Location = new System.Drawing.Point(608, 31);
            this.lblToDate.Name = "lblToDate";
            this.lblToDate.Size = new System.Drawing.Size(100, 13);
            this.lblToDate.TabIndex = 49;
            this.lblToDate.Text = "To Date:";
            this.lblToDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblFromDate
            // 
            this.lblFromDate.Location = new System.Drawing.Point(296, 27);
            this.lblFromDate.Name = "lblFromDate";
            this.lblFromDate.Size = new System.Drawing.Size(100, 13);
            this.lblFromDate.TabIndex = 47;
            this.lblFromDate.Text = "From Date:";
            this.lblFromDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtIndentNo
            // 
            this.txtIndentNo.Location = new System.Drawing.Point(180, 24);
            this.txtIndentNo.MaxLength = 20;
            this.txtIndentNo.Name = "txtIndentNo";
            this.txtIndentNo.Size = new System.Drawing.Size(110, 21);
            this.txtIndentNo.TabIndex = 0;
            // 
            // cmbLocation
            // 
            this.cmbLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocation.FormattingEnabled = true;
            this.cmbLocation.Location = new System.Drawing.Point(180, 62);
            this.cmbLocation.Name = "cmbLocation";
            this.cmbLocation.Size = new System.Drawing.Size(110, 21);
            this.cmbLocation.TabIndex = 3;
            // 
            // lblIndentNo
            // 
            this.lblIndentNo.Location = new System.Drawing.Point(99, 27);
            this.lblIndentNo.Name = "lblIndentNo";
            this.lblIndentNo.Size = new System.Drawing.Size(75, 13);
            this.lblIndentNo.TabIndex = 45;
            this.lblIndentNo.Text = "Indent No:";
            this.lblIndentNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCreateStaus
            // 
            this.lblCreateStaus.Location = new System.Drawing.Point(359, 51);
            this.lblCreateStaus.Name = "lblCreateStaus";
            this.lblCreateStaus.Size = new System.Drawing.Size(100, 13);
            this.lblCreateStaus.TabIndex = 59;
            this.lblCreateStaus.Text = "Status:";
            this.lblCreateStaus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCreateLocationCode
            // 
            this.lblCreateLocationCode.Location = new System.Drawing.Point(359, 23);
            this.lblCreateLocationCode.Name = "lblCreateLocationCode";
            this.lblCreateLocationCode.Size = new System.Drawing.Size(100, 13);
            this.lblCreateLocationCode.TabIndex = 57;
            this.lblCreateLocationCode.Text = "Location Code:";
            this.lblCreateLocationCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblLocation
            // 
            this.lblLocation.Location = new System.Drawing.Point(631, 23);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(100, 13);
            this.lblLocation.TabIndex = 55;
            this.lblLocation.Text = "Location:";
            this.lblLocation.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblIndentDate
            // 
            this.lblIndentDate.Location = new System.Drawing.Point(36, 20);
            this.lblIndentDate.Name = "lblIndentDate";
            this.lblIndentDate.Size = new System.Drawing.Size(100, 13);
            this.lblIndentDate.TabIndex = 53;
            this.lblIndentDate.Text = "Create Date:";
            this.lblIndentDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtAvgSales
            // 
            this.txtAvgSales.Location = new System.Drawing.Point(181, 71);
            this.txtAvgSales.Name = "txtAvgSales";
            this.txtAvgSales.ReadOnly = true;
            this.txtAvgSales.Size = new System.Drawing.Size(110, 21);
            this.txtAvgSales.TabIndex = 89;
            this.txtAvgSales.TabStop = false;
            // 
            // txtStockInHand
            // 
            this.txtStockInHand.Location = new System.Drawing.Point(181, 43);
            this.txtStockInHand.Name = "txtStockInHand";
            this.txtStockInHand.ReadOnly = true;
            this.txtStockInHand.Size = new System.Drawing.Size(110, 21);
            this.txtStockInHand.TabIndex = 88;
            this.txtStockInHand.TabStop = false;
            // 
            // txtApprovedPOQty
            // 
            this.txtApprovedPOQty.Location = new System.Drawing.Point(558, 156);
            this.txtApprovedPOQty.Name = "txtApprovedPOQty";
            this.txtApprovedPOQty.ReadOnly = true;
            this.txtApprovedPOQty.Size = new System.Drawing.Size(110, 21);
            this.txtApprovedPOQty.TabIndex = 87;
            this.txtApprovedPOQty.TabStop = false;
            this.txtApprovedPOQty.Visible = false;
            // 
            // txtRequestedQty
            // 
            this.txtRequestedQty.Location = new System.Drawing.Point(181, 101);
            this.txtRequestedQty.MaxLength = 8;
            this.txtRequestedQty.Name = "txtRequestedQty";
            this.txtRequestedQty.Size = new System.Drawing.Size(110, 21);
            this.txtRequestedQty.TabIndex = 1;
            this.txtRequestedQty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRequestedQty_KeyDown);
            // 
            // txtApprovedToQty
            // 
            this.txtApprovedToQty.Location = new System.Drawing.Point(463, 98);
            this.txtApprovedToQty.Name = "txtApprovedToQty";
            this.txtApprovedToQty.ReadOnly = true;
            this.txtApprovedToQty.Size = new System.Drawing.Size(110, 21);
            this.txtApprovedToQty.TabIndex = 84;
            this.txtApprovedToQty.TabStop = false;
            // 
            // txtItemName
            // 
            this.txtItemName.Location = new System.Drawing.Point(460, 14);
            this.txtItemName.Name = "txtItemName";
            this.txtItemName.ReadOnly = true;
            this.txtItemName.Size = new System.Drawing.Size(431, 21);
            this.txtItemName.TabIndex = 0;
            this.txtItemName.TabStop = false;
            // 
            // txtStockInTransit
            // 
            this.txtStockInTransit.Location = new System.Drawing.Point(462, 43);
            this.txtStockInTransit.Name = "txtStockInTransit";
            this.txtStockInTransit.ReadOnly = true;
            this.txtStockInTransit.Size = new System.Drawing.Size(110, 21);
            this.txtStockInTransit.TabIndex = 82;
            this.txtStockInTransit.TabStop = false;
            // 
            // txtApprovedHOQty
            // 
            this.txtApprovedHOQty.Location = new System.Drawing.Point(129, 156);
            this.txtApprovedHOQty.Name = "txtApprovedHOQty";
            this.txtApprovedHOQty.ReadOnly = true;
            this.txtApprovedHOQty.Size = new System.Drawing.Size(110, 21);
            this.txtApprovedHOQty.TabIndex = 80;
            this.txtApprovedHOQty.TabStop = false;
            this.txtApprovedHOQty.Visible = false;
            // 
            // txtItemCode
            // 
            this.txtItemCode.Location = new System.Drawing.Point(181, 14);
            this.txtItemCode.Name = "txtItemCode";
            this.txtItemCode.Size = new System.Drawing.Size(110, 21);
            this.txtItemCode.TabIndex = 0;
            this.txtItemCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtItemCode_KeyDown);
            this.txtItemCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtItemCode_KeyPress);
            this.txtItemCode.Validating += new System.ComponentModel.CancelEventHandler(this.txtItemCode_Validating);
            // 
            // lblAvgSale
            // 
            this.lblAvgSale.Location = new System.Drawing.Point(21, 74);
            this.lblAvgSale.Name = "lblAvgSale";
            this.lblAvgSale.Size = new System.Drawing.Size(150, 13);
            this.lblAvgSale.TabIndex = 78;
            this.lblAvgSale.Text = "Last 3 Months Avg Sale:";
            this.lblAvgSale.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblStockInHand
            // 
            this.lblStockInHand.Location = new System.Drawing.Point(70, 44);
            this.lblStockInHand.Name = "lblStockInHand";
            this.lblStockInHand.Size = new System.Drawing.Size(100, 13);
            this.lblStockInHand.TabIndex = 77;
            this.lblStockInHand.Text = "Stock In Hand:";
            this.lblStockInHand.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblApprovedPOQty
            // 
            this.lblApprovedPOQty.Location = new System.Drawing.Point(402, 156);
            this.lblApprovedPOQty.Name = "lblApprovedPOQty";
            this.lblApprovedPOQty.Size = new System.Drawing.Size(150, 13);
            this.lblApprovedPOQty.TabIndex = 76;
            this.lblApprovedPOQty.Text = "Approved PO Qty:";
            this.lblApprovedPOQty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblApprovedPOQty.Visible = false;
            // 
            // lblRequestedQty
            // 
            this.lblRequestedQty.Location = new System.Drawing.Point(25, 104);
            this.lblRequestedQty.Name = "lblRequestedQty";
            this.lblRequestedQty.Size = new System.Drawing.Size(150, 13);
            this.lblRequestedQty.TabIndex = 75;
            this.lblRequestedQty.Text = "Requested Qty: *";
            this.lblRequestedQty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTONo
            // 
            this.lblTONo.Location = new System.Drawing.Point(650, 104);
            this.lblTONo.Name = "lblTONo";
            this.lblTONo.Size = new System.Drawing.Size(120, 13);
            this.lblTONo.TabIndex = 74;
            this.lblTONo.Text = "TOI Number:";
            this.lblTONo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblApprovedToQty
            // 
            this.lblApprovedToQty.Location = new System.Drawing.Point(340, 101);
            this.lblApprovedToQty.Name = "lblApprovedToQty";
            this.lblApprovedToQty.Size = new System.Drawing.Size(120, 13);
            this.lblApprovedToQty.TabIndex = 73;
            this.lblApprovedToQty.Text = "Approved TOI Qty:";
            this.lblApprovedToQty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblItemName
            // 
            this.lblItemName.Location = new System.Drawing.Point(340, 17);
            this.lblItemName.Name = "lblItemName";
            this.lblItemName.Size = new System.Drawing.Size(120, 13);
            this.lblItemName.TabIndex = 72;
            this.lblItemName.Text = "Item Name:";
            this.lblItemName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblStockInTransit
            // 
            this.lblStockInTransit.Location = new System.Drawing.Point(340, 45);
            this.lblStockInTransit.Name = "lblStockInTransit";
            this.lblStockInTransit.Size = new System.Drawing.Size(120, 13);
            this.lblStockInTransit.TabIndex = 71;
            this.lblStockInTransit.Text = "Stock In Transit:";
            this.lblStockInTransit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPONo
            // 
            this.lblPONo.Location = new System.Drawing.Point(50, 156);
            this.lblPONo.Name = "lblPONo";
            this.lblPONo.Size = new System.Drawing.Size(120, 13);
            this.lblPONo.TabIndex = 70;
            this.lblPONo.Text = "PO Number:";
            this.lblPONo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblPONo.Visible = false;
            // 
            // lblApprovedHOQty
            // 
            this.lblApprovedHOQty.Location = new System.Drawing.Point(-2, 156);
            this.lblApprovedHOQty.Name = "lblApprovedHOQty";
            this.lblApprovedHOQty.Size = new System.Drawing.Size(120, 13);
            this.lblApprovedHOQty.TabIndex = 69;
            this.lblApprovedHOQty.Text = "Approved HO Qty:";
            this.lblApprovedHOQty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblApprovedHOQty.Visible = false;
            // 
            // lblItemCode
            // 
            this.lblItemCode.Location = new System.Drawing.Point(50, 17);
            this.lblItemCode.Name = "lblItemCode";
            this.lblItemCode.Size = new System.Drawing.Size(120, 13);
            this.lblItemCode.TabIndex = 68;
            this.lblItemCode.Text = "Item Code: *";
            this.lblItemCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCreateIndentNo
            // 
            this.lblCreateIndentNo.Location = new System.Drawing.Point(36, 51);
            this.lblCreateIndentNo.Name = "lblCreateIndentNo";
            this.lblCreateIndentNo.Size = new System.Drawing.Size(100, 13);
            this.lblCreateIndentNo.TabIndex = 63;
            this.lblCreateIndentNo.Text = "Indent No:";
            this.lblCreateIndentNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblIndentNoValue
            // 
            this.lblIndentNoValue.Location = new System.Drawing.Point(142, 51);
            this.lblIndentNoValue.Name = "lblIndentNoValue";
            this.lblIndentNoValue.Size = new System.Drawing.Size(150, 13);
            this.lblIndentNoValue.TabIndex = 64;
            this.lblIndentNoValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Checked = false;
            this.dtpFromDate.CustomFormat = "dd-MM-yyyy";
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFromDate.Location = new System.Drawing.Point(402, 27);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.ShowCheckBox = true;
            this.dtpFromDate.Size = new System.Drawing.Size(200, 21);
            this.dtpFromDate.TabIndex = 1;
            // 
            // dtpToDate
            // 
            this.dtpToDate.Checked = false;
            this.dtpToDate.CustomFormat = "dd-MM-yyyy";
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpToDate.Location = new System.Drawing.Point(714, 27);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.ShowCheckBox = true;
            this.dtpToDate.Size = new System.Drawing.Size(200, 21);
            this.dtpToDate.TabIndex = 2;
            // 
            // errCreateIndent
            // 
            this.errCreateIndent.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errCreateIndent.ContainerControl = this;
            // 
            // lblLocationCode
            // 
            this.lblLocationCode.Location = new System.Drawing.Point(465, 23);
            this.lblLocationCode.Name = "lblLocationCode";
            this.lblLocationCode.Size = new System.Drawing.Size(100, 13);
            this.lblLocationCode.TabIndex = 66;
            this.lblLocationCode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStatusValue
            // 
            this.lblStatusValue.Location = new System.Drawing.Point(465, 51);
            this.lblStatusValue.Name = "lblStatusValue";
            this.lblStatusValue.Size = new System.Drawing.Size(150, 13);
            this.lblStatusValue.TabIndex = 67;
            this.lblStatusValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblLocationName
            // 
            this.lblLocationName.Location = new System.Drawing.Point(737, 25);
            this.lblLocationName.Name = "lblLocationName";
            this.lblLocationName.Size = new System.Drawing.Size(100, 13);
            this.lblLocationName.TabIndex = 68;
            this.lblLocationName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnReject
            // 
            this.btnReject.BackColor = System.Drawing.Color.Transparent;
            this.btnReject.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnReject.BackgroundImage")));
            this.btnReject.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnReject.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnReject.FlatAppearance.BorderSize = 0;
            this.btnReject.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnReject.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnReject.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReject.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnReject.Location = new System.Drawing.Point(930, 0);
            this.btnReject.Name = "btnReject";
            this.btnReject.Size = new System.Drawing.Size(75, 32);
            this.btnReject.TabIndex = 11;
            this.btnReject.Text = "Re&ject";
            this.btnReject.UseVisualStyleBackColor = false;
            this.btnReject.Click += new System.EventHandler(this.btnReject_Click);
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
            this.btnApproved.Location = new System.Drawing.Point(855, 0);
            this.btnApproved.Name = "btnApproved";
            this.btnApproved.Size = new System.Drawing.Size(75, 32);
            this.btnApproved.TabIndex = 10;
            this.btnApproved.Text = "Appr&ove";
            this.btnApproved.UseVisualStyleBackColor = false;
            this.btnApproved.Click += new System.EventHandler(this.btnApproved_Click);
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
            this.btnPrint.Location = new System.Drawing.Point(780, 0);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 32);
            this.btnPrint.TabIndex = 9;
            this.btnPrint.Text = "&Print";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnAddPO_TO
            // 
            this.btnAddPO_TO.BackColor = System.Drawing.Color.Transparent;
            this.btnAddPO_TO.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAddPO_TO.BackgroundImage")));
            this.btnAddPO_TO.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAddPO_TO.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnAddPO_TO.FlatAppearance.BorderSize = 0;
            this.btnAddPO_TO.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnAddPO_TO.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnAddPO_TO.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddPO_TO.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnAddPO_TO.Location = new System.Drawing.Point(705, 0);
            this.btnAddPO_TO.Name = "btnAddPO_TO";
            this.btnAddPO_TO.Size = new System.Drawing.Size(75, 32);
            this.btnAddPO_TO.TabIndex = 8;
            this.btnAddPO_TO.Text = "&TOI";
            this.btnAddPO_TO.UseVisualStyleBackColor = false;
            this.btnAddPO_TO.Click += new System.EventHandler(this.btnAddPO_TO_Click);
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
            this.btnCancel.Location = new System.Drawing.Point(630, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 32);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Canc&el";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.BackColor = System.Drawing.Color.Transparent;
            this.btnConfirm.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnConfirm.BackgroundImage")));
            this.btnConfirm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnConfirm.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnConfirm.FlatAppearance.BorderSize = 0;
            this.btnConfirm.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnConfirm.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirm.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnConfirm.Location = new System.Drawing.Point(555, 0);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 32);
            this.btnConfirm.TabIndex = 6;
            this.btnConfirm.Text = "Con&firm";
            this.btnConfirm.UseVisualStyleBackColor = false;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // dgvIndentDetails
            // 
            this.dgvIndentDetails.AllowUserToAddRows = false;
            this.dgvIndentDetails.AllowUserToDeleteRows = false;
            this.dgvIndentDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvIndentDetails.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvIndentDetails.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dgvIndentDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvIndentDetails.Location = new System.Drawing.Point(6, 189);
            this.dgvIndentDetails.MultiSelect = false;
            this.dgvIndentDetails.Name = "dgvIndentDetails";
            this.dgvIndentDetails.RowHeadersVisible = false;
            this.dgvIndentDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvIndentDetails.Size = new System.Drawing.Size(992, 226);
            this.dgvIndentDetails.TabIndex = 0;
            this.dgvIndentDetails.TabStop = false;
            this.dgvIndentDetails.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvIndentDetails_RowEnter);
            this.dgvIndentDetails.Leave += new System.EventHandler(this.dgvIndentDetails_Leave);
            this.dgvIndentDetails.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvIndentDetails_CellEndEdit);
            this.dgvIndentDetails.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvIndentDetails_CellClick);
            this.dgvIndentDetails.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvIndentDetails_EditingControlShowing);
            this.dgvIndentDetails.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvIndentDetails_DataError);
            this.dgvIndentDetails.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvIndentDetails_CellEnter);
            this.dgvIndentDetails.SelectionChanged += new System.EventHandler(this.dgvIndentDetails_SelectionChanged);
            // 
            // lblLocationType
            // 
            this.lblLocationType.AutoSize = true;
            this.lblLocationType.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblLocationType.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.lblLocationType.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblLocationType.Location = new System.Drawing.Point(3, 0);
            this.lblLocationType.Name = "lblLocationType";
            this.lblLocationType.Size = new System.Drawing.Size(48, 29);
            this.lblLocationType.TabIndex = 7;
            this.lblLocationType.Text = "label1";
            this.lblLocationType.Visible = false;
            // 
            // errorSearch
            // 
            this.errorSearch.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorSearch.ContainerControl = this;
            // 
            // lblCreateDate
            // 
            this.lblCreateDate.Location = new System.Drawing.Point(142, 20);
            this.lblCreateDate.Name = "lblCreateDate";
            this.lblCreateDate.Size = new System.Drawing.Size(100, 13);
            this.lblCreateDate.TabIndex = 101;
            this.lblCreateDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCreateRemark
            // 
            this.lblCreateRemark.Location = new System.Drawing.Point(631, 51);
            this.lblCreateRemark.Name = "lblCreateRemark";
            this.lblCreateRemark.Size = new System.Drawing.Size(100, 13);
            this.lblCreateRemark.TabIndex = 60;
            this.lblCreateRemark.Text = "Remarks:";
            this.lblCreateRemark.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCreateRemark
            // 
            this.txtCreateRemark.Location = new System.Drawing.Point(737, 52);
            this.txtCreateRemark.MaxLength = 100;
            this.txtCreateRemark.Multiline = true;
            this.txtCreateRemark.Name = "txtCreateRemark";
            this.txtCreateRemark.Size = new System.Drawing.Size(150, 50);
            this.txtCreateRemark.TabIndex = 0;
            // 
            // lblCreateIndentDateValue
            // 
            this.lblCreateIndentDateValue.Location = new System.Drawing.Point(142, 79);
            this.lblCreateIndentDateValue.Name = "lblCreateIndentDateValue";
            this.lblCreateIndentDateValue.Size = new System.Drawing.Size(100, 13);
            this.lblCreateIndentDateValue.TabIndex = 103;
            this.lblCreateIndentDateValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCreateIndentDate
            // 
            this.lblCreateIndentDate.Location = new System.Drawing.Point(36, 79);
            this.lblCreateIndentDate.Name = "lblCreateIndentDate";
            this.lblCreateIndentDate.Size = new System.Drawing.Size(100, 13);
            this.lblCreateIndentDate.TabIndex = 102;
            this.lblCreateIndentDate.Text = "Indent Date:";
            this.lblCreateIndentDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblApprovedDateValue
            // 
            this.lblApprovedDateValue.Location = new System.Drawing.Point(465, 79);
            this.lblApprovedDateValue.Name = "lblApprovedDateValue";
            this.lblApprovedDateValue.Size = new System.Drawing.Size(100, 13);
            this.lblApprovedDateValue.TabIndex = 105;
            this.lblApprovedDateValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblApprovedDate
            // 
            this.lblApprovedDate.Location = new System.Drawing.Point(359, 79);
            this.lblApprovedDate.Name = "lblApprovedDate";
            this.lblApprovedDate.Size = new System.Drawing.Size(100, 13);
            this.lblApprovedDate.TabIndex = 104;
            this.lblApprovedDate.Text = "Approved Date:";
            this.lblApprovedDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lstPONumber
            // 
            this.lstPONumber.FormattingEnabled = true;
            this.lstPONumber.Location = new System.Drawing.Point(181, 156);
            this.lstPONumber.Name = "lstPONumber";
            this.lstPONumber.Size = new System.Drawing.Size(140, 43);
            this.lstPONumber.TabIndex = 2;
            this.lstPONumber.TabStop = false;
            this.lstPONumber.Visible = false;
            // 
            // lstTONumber
            // 
            this.lstTONumber.FormattingEnabled = true;
            this.lstTONumber.Location = new System.Drawing.Point(789, 104);
            this.lstTONumber.Name = "lstTONumber";
            this.lstTONumber.Size = new System.Drawing.Size(140, 30);
            this.lstTONumber.TabIndex = 3;
            this.lstTONumber.TabStop = false;
            // 
            // dgvIndent
            // 
            this.dgvIndent.AllowUserToAddRows = false;
            this.dgvIndent.AllowUserToDeleteRows = false;
            this.dgvIndent.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvIndent.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvIndent.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dgvIndent.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvIndent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvIndent.Location = new System.Drawing.Point(0, 0);
            this.dgvIndent.MultiSelect = false;
            this.dgvIndent.Name = "dgvIndent";
            this.dgvIndent.ReadOnly = true;
            this.dgvIndent.RowHeadersVisible = false;
            this.dgvIndent.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvIndent.Size = new System.Drawing.Size(1005, 463);
            this.dgvIndent.TabIndex = 0;
            this.dgvIndent.TabStop = false;
            this.dgvIndent.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgvIndent_MouseDoubleClick);
            this.dgvIndent.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvIndent_CellClick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::PurchaseComponent.Properties.Resources.find;
            this.pictureBox1.Location = new System.Drawing.Point(308, 14);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(21, 21);
            this.pictureBox1.TabIndex = 90;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(650, 415);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(280, 13);
            this.label1.TabIndex = 91;
            this.label1.Text = "Note : Transfer price will be 120% of item cost";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtAvgStockTransfer
            // 
            this.txtAvgStockTransfer.Location = new System.Drawing.Point(462, 71);
            this.txtAvgStockTransfer.Name = "txtAvgStockTransfer";
            this.txtAvgStockTransfer.ReadOnly = true;
            this.txtAvgStockTransfer.Size = new System.Drawing.Size(111, 21);
            this.txtAvgStockTransfer.TabIndex = 92;
            this.txtAvgStockTransfer.TabStop = false;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(344, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 15);
            this.label3.TabIndex = 91;
            this.label3.Text = "Stock Transfer:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTotalStk
            // 
            this.txtTotalStk.Location = new System.Drawing.Point(789, 45);
            this.txtTotalStk.Name = "txtTotalStk";
            this.txtTotalStk.ReadOnly = true;
            this.txtTotalStk.Size = new System.Drawing.Size(102, 21);
            this.txtTotalStk.TabIndex = 95;
            this.txtTotalStk.TabStop = false;
            // 
            // txtSaleStkTransf
            // 
            this.txtSaleStkTransf.Location = new System.Drawing.Point(789, 72);
            this.txtSaleStkTransf.Name = "txtSaleStkTransf";
            this.txtSaleStkTransf.ReadOnly = true;
            this.txtSaleStkTransf.Size = new System.Drawing.Size(101, 21);
            this.txtSaleStkTransf.TabIndex = 96;
            this.txtSaleStkTransf.TabStop = false;
            // 
            // lblTotalSaleStkTrnas
            // 
            this.lblTotalSaleStkTrnas.AutoSize = true;
            this.lblTotalSaleStkTrnas.Location = new System.Drawing.Point(649, 75);
            this.lblTotalSaleStkTrnas.Name = "lblTotalSaleStkTrnas";
            this.lblTotalSaleStkTrnas.Size = new System.Drawing.Size(134, 13);
            this.lblTotalSaleStkTrnas.TabIndex = 93;
            this.lblTotalSaleStkTrnas.Text = "Total Sale + Transfer:";
            this.lblTotalSaleStkTrnas.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotalStock
            // 
            this.lblTotalStock.Location = new System.Drawing.Point(663, 48);
            this.lblTotalStock.Name = "lblTotalStock";
            this.lblTotalStock.Size = new System.Drawing.Size(120, 13);
            this.lblTotalStock.TabIndex = 94;
            this.lblTotalStock.Text = "Total Stock:";
            this.lblTotalStock.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(55, 132);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 13);
            this.label2.TabIndex = 97;
            this.label2.Text = "From Location: *";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbIndentFromLocation
            // 
            this.cmbIndentFromLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbIndentFromLocation.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbIndentFromLocation.FormattingEnabled = true;
            this.cmbIndentFromLocation.Location = new System.Drawing.Point(181, 128);
            this.cmbIndentFromLocation.Name = "cmbIndentFromLocation";
            this.cmbIndentFromLocation.Size = new System.Drawing.Size(175, 22);
            this.cmbIndentFromLocation.TabIndex = 98;
            this.cmbIndentFromLocation.SelectedIndexChanged += new System.EventHandler(this.cmbIndentFromLocation_SelectedIndexChanged);
            // 
            // frmIndent
            // 
            this.AcceptButton = this.btnSearch;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1013, 703);
            this.Name = "frmIndent";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "frmManualIndent";
            this.WindowState = System.Windows.Forms.FormWindowState.Normal;
            this.Load += new System.EventHandler(this.frmIndent_Load);
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
            ((System.ComponentModel.ISupportInitialize)(this.errCreateIndent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIndentDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorSearch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIndent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Label lblLocationSearch;
        private System.Windows.Forms.Label lblToDate;
        private System.Windows.Forms.Label lblFromDate;
        private System.Windows.Forms.TextBox txtIndentNo;
        private System.Windows.Forms.ComboBox cmbLocation;
        private System.Windows.Forms.Label lblIndentNo;
        private System.Windows.Forms.Label lblCreateStaus;
        private System.Windows.Forms.Label lblCreateLocationCode;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.Label lblIndentDate;
        private System.Windows.Forms.TextBox txtAvgSales;
        private System.Windows.Forms.TextBox txtStockInHand;
        private System.Windows.Forms.TextBox txtApprovedPOQty;
        private System.Windows.Forms.TextBox txtRequestedQty;
        private System.Windows.Forms.TextBox txtApprovedToQty;
        private System.Windows.Forms.TextBox txtItemName;
        private System.Windows.Forms.TextBox txtStockInTransit;
        private System.Windows.Forms.TextBox txtApprovedHOQty;
        private System.Windows.Forms.TextBox txtItemCode;
        private System.Windows.Forms.Label lblAvgSale;
        private System.Windows.Forms.Label lblStockInHand;
        private System.Windows.Forms.Label lblApprovedPOQty;
        private System.Windows.Forms.Label lblRequestedQty;
        private System.Windows.Forms.Label lblTONo;
        private System.Windows.Forms.Label lblApprovedToQty;
        private System.Windows.Forms.Label lblItemName;
        private System.Windows.Forms.Label lblStockInTransit;
        private System.Windows.Forms.Label lblPONo;
        private System.Windows.Forms.Label lblApprovedHOQty;
        private System.Windows.Forms.Label lblItemCode;
        private System.Windows.Forms.Label lblCreateIndentNo;
        private System.Windows.Forms.Label lblIndentNoValue;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.ErrorProvider errCreateIndent;
        private System.Windows.Forms.Label lblLocationName;
        private System.Windows.Forms.Label lblStatusValue;
        private System.Windows.Forms.Label lblLocationCode;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnReject;
        private System.Windows.Forms.Button btnApproved;
        private System.Windows.Forms.DataGridView dgvIndentDetails;
        private System.Windows.Forms.Label lblLocationType;
        private System.Windows.Forms.Button btnAddPO_TO;
        private System.Windows.Forms.ErrorProvider errorSearch;
        private System.Windows.Forms.Label lblCreateDate;
        private System.Windows.Forms.TextBox txtCreateRemark;
        private System.Windows.Forms.Label lblCreateRemark;
        private System.Windows.Forms.Label lblApprovedDateValue;
        private System.Windows.Forms.Label lblApprovedDate;
        private System.Windows.Forms.Label lblCreateIndentDateValue;
        private System.Windows.Forms.Label lblCreateIndentDate;
        private System.Windows.Forms.ListBox lstTONumber;
        private System.Windows.Forms.ListBox lstPONumber;
        private System.Windows.Forms.DataGridView dgvIndent;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTotalStk;
        private System.Windows.Forms.TextBox txtSaleStkTransf;
        private System.Windows.Forms.Label lblTotalSaleStkTrnas;
        private System.Windows.Forms.Label lblTotalStock;
        private System.Windows.Forms.TextBox txtAvgStockTransfer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbIndentFromLocation;
    }
}