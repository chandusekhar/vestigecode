namespace PromotionsComponent.UI
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
            this.components = new System.ComponentModel.Container();
            this.txtSearchVoucherName = new System.Windows.Forms.TextBox();
            this.lblSearchVoucherName = new System.Windows.Forms.Label();
            this.txtSearchVoucherCode = new System.Windows.Forms.TextBox();
            this.lblSearchVoucherCode = new System.Windows.Forms.Label();
            this.dgvSearchGiftVoucher = new System.Windows.Forms.DataGridView();
            this.dtpSearchfrmDate = new System.Windows.Forms.DateTimePicker();
            this.dtpSearchToDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.lblCreateVoucherCode = new System.Windows.Forms.Label();
            this.txtVoucherName = new System.Windows.Forms.TextBox();
            this.lblVoucherName = new System.Windows.Forms.Label();
            this.txtVoucherDescription = new System.Windows.Forms.TextBox();
            this.lblVoucherDescription = new System.Windows.Forms.Label();
            this.txtItemCode = new System.Windows.Forms.TextBox();
            this.lblItemCode = new System.Windows.Forms.Label();
            this.txtItemDescription = new System.Windows.Forms.TextBox();
            this.lblItemDescription = new System.Windows.Forms.Label();
            this.txtMinBuyAmount = new System.Windows.Forms.TextBox();
            this.lblBuyAmount = new System.Windows.Forms.Label();
            this.lblActive = new System.Windows.Forms.Label();
            this.txtEndRange = new System.Windows.Forms.TextBox();
            this.lblEndRange = new System.Windows.Forms.Label();
            this.txtStartRange = new System.Windows.Forms.TextBox();
            this.lblStartRange = new System.Windows.Forms.Label();
            this.lblApplicableFrom = new System.Windows.Forms.Label();
            this.lblApplicableTo = new System.Windows.Forms.Label();
            this.dtpApplicableFromDate = new System.Windows.Forms.DateTimePicker();
            this.dtpApplicableToDate = new System.Windows.Forms.DateTimePicker();
            this.dgvGiftVoucherDetails = new System.Windows.Forms.DataGridView();
            this.btnNew = new System.Windows.Forms.Button();
            this.txtStartText = new System.Windows.Forms.TextBox();
            this.lblStartText = new System.Windows.Forms.Label();
            this.chkActive = new System.Windows.Forms.CheckBox();
            this.lblVoucherCodeValue = new System.Windows.Forms.Label();
            this.errorCreate = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorSearch = new System.Windows.Forms.ErrorProvider(this.components);
            this.txtSearcItemCode = new System.Windows.Forms.TextBox();
            this.lblSearchItemCode = new System.Windows.Forms.Label();
            this.lblSearchfrmPODate = new System.Windows.Forms.Label();
            this.pnlCreateItemDetail = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnClearItemDetail = new System.Windows.Forms.Button();
            this.btnAddItemDetail = new System.Windows.Forms.Button();
            this.txtItemQty = new System.Windows.Forms.TextBox();
            this.lblItemQty = new System.Windows.Forms.Label();
            this.dgvGiftVoucherItemDetails = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlCreateHeader.SuspendLayout();
            this.grpAddDetails.SuspendLayout();
            this.pnlSearchHeader.SuspendLayout();
            this.pnlSearchGrid.SuspendLayout();
            this.pnlCreateDetail.SuspendLayout();
            this.pnlLowerButtons.SuspendLayout();
            this.pnlSearchButtons.SuspendLayout();
            this.tabSearch.SuspendLayout();
            this.tabCreate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearchGiftVoucher)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGiftVoucherDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorCreate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorSearch)).BeginInit();
            this.pnlCreateItemDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGiftVoucherItemDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCreateHeader
            // 
            this.pnlCreateHeader.Controls.Add(this.lblVoucherCodeValue);
            this.pnlCreateHeader.Controls.Add(this.txtMinBuyAmount);
            this.pnlCreateHeader.Controls.Add(this.txtVoucherDescription);
            this.pnlCreateHeader.Controls.Add(this.lblBuyAmount);
            this.pnlCreateHeader.Controls.Add(this.lblVoucherDescription);
            this.pnlCreateHeader.Controls.Add(this.txtVoucherName);
            this.pnlCreateHeader.Controls.Add(this.lblVoucherName);
            this.pnlCreateHeader.Controls.Add(this.lblCreateVoucherCode);
            this.pnlCreateHeader.Size = new System.Drawing.Size(1005, 78);
            this.pnlCreateHeader.TabIndex = 1;
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblCreateVoucherCode, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblVoucherName, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.txtVoucherName, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblVoucherDescription, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblBuyAmount, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.txtVoucherDescription, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.txtMinBuyAmount, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblVoucherCodeValue, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.pnlTopButtons, 0);
            // 
            // btnCreateReset
            // 
            this.btnCreateReset.FlatAppearance.BorderSize = 0;
            this.btnCreateReset.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnCreateReset.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnCreateReset.TabIndex = 6;
            this.btnCreateReset.Click += new System.EventHandler(this.btnCreateReset_Click);
            // 
            // btnSave
            // 
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSave.TabIndex = 5;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblAddDetails
            // 
            this.lblAddDetails.TabIndex = 2;
            this.lblAddDetails.Text = "Item-Details";
            // 
            // grpAddDetails
            // 
            this.grpAddDetails.Controls.Add(this.pnlCreateItemDetail);
            this.grpAddDetails.Controls.Add(this.label2);
            this.grpAddDetails.Location = new System.Drawing.Point(0, 102);
            this.grpAddDetails.Size = new System.Drawing.Size(1005, 505);
            this.grpAddDetails.Controls.SetChildIndex(this.label2, 0);
            this.grpAddDetails.Controls.SetChildIndex(this.pnlCreateItemDetail, 0);
            this.grpAddDetails.Controls.SetChildIndex(this.pnlCreateDetail, 0);
            this.grpAddDetails.Controls.SetChildIndex(this.pnlLowerButtons, 0);
            // 
            // btnClearDetails
            // 
            this.btnClearDetails.Dock = System.Windows.Forms.DockStyle.None;
            this.btnClearDetails.FlatAppearance.BorderSize = 0;
            this.btnClearDetails.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnClearDetails.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnClearDetails.Location = new System.Drawing.Point(912, 31);
            this.btnClearDetails.TabIndex = 7;
            this.btnClearDetails.Visible = false;
            // 
            // btnAddDetails
            // 
            this.btnAddDetails.Dock = System.Windows.Forms.DockStyle.None;
            this.btnAddDetails.FlatAppearance.BorderSize = 0;
            this.btnAddDetails.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnAddDetails.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnAddDetails.Location = new System.Drawing.Point(912, 3);
            this.btnAddDetails.TabIndex = 6;
            this.btnAddDetails.Text = "A&dd";
            this.btnAddDetails.Visible = false;
            // 
            // pnlSearchHeader
            // 
            this.pnlSearchHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSearchHeader.Controls.Add(this.txtSearcItemCode);
            this.pnlSearchHeader.Controls.Add(this.lblSearchItemCode);
            this.pnlSearchHeader.Controls.Add(this.txtSearchVoucherName);
            this.pnlSearchHeader.Controls.Add(this.dtpSearchfrmDate);
            this.pnlSearchHeader.Controls.Add(this.lblSearchVoucherName);
            this.pnlSearchHeader.Controls.Add(this.txtSearchVoucherCode);
            this.pnlSearchHeader.Controls.Add(this.dtpSearchToDate);
            this.pnlSearchHeader.Controls.Add(this.label1);
            this.pnlSearchHeader.Controls.Add(this.lblSearchVoucherCode);
            this.pnlSearchHeader.Controls.Add(this.lblSearchfrmPODate);
            this.pnlSearchHeader.Size = new System.Drawing.Size(1005, 130);
            this.pnlSearchHeader.Controls.SetChildIndex(this.pnlSearchButtons, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblSearchfrmPODate, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblSearchVoucherCode, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.label1, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.dtpSearchToDate, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.txtSearchVoucherCode, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblSearchVoucherName, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.dtpSearchfrmDate, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.txtSearchVoucherName, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblSearchItemCode, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.txtSearcItemCode, 0);
            // 
            // btnSearch
            // 
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearch.Location = new System.Drawing.Point(853, 0);
            this.btnSearch.TabIndex = 5;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnSearchReset
            // 
            this.btnSearchReset.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearchReset.FlatAppearance.BorderSize = 0;
            this.btnSearchReset.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearchReset.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearchReset.Location = new System.Drawing.Point(928, 0);
            this.btnSearchReset.TabIndex = 6;
            this.btnSearchReset.Click += new System.EventHandler(this.btnSearchReset_Click);
            // 
            // pnlSearchGrid
            // 
            this.pnlSearchGrid.Controls.Add(this.dgvSearchGiftVoucher);
            this.pnlSearchGrid.Location = new System.Drawing.Point(0, 154);
            this.pnlSearchGrid.Size = new System.Drawing.Size(1005, 453);
            // 
            // pnlCreateDetail
            // 
            this.pnlCreateDetail.Controls.Add(this.dgvGiftVoucherDetails);
            this.pnlCreateDetail.Controls.Add(this.chkActive);
            this.pnlCreateDetail.Controls.Add(this.txtStartText);
            this.pnlCreateDetail.Controls.Add(this.lblStartRange);
            this.pnlCreateDetail.Controls.Add(this.lblStartText);
            this.pnlCreateDetail.Controls.Add(this.txtStartRange);
            this.pnlCreateDetail.Controls.Add(this.btnClearDetails);
            this.pnlCreateDetail.Controls.Add(this.dtpApplicableToDate);
            this.pnlCreateDetail.Controls.Add(this.lblEndRange);
            this.pnlCreateDetail.Controls.Add(this.dtpApplicableFromDate);
            this.pnlCreateDetail.Controls.Add(this.btnAddDetails);
            this.pnlCreateDetail.Controls.Add(this.lblApplicableTo);
            this.pnlCreateDetail.Controls.Add(this.lblActive);
            this.pnlCreateDetail.Controls.Add(this.txtEndRange);
            this.pnlCreateDetail.Controls.Add(this.lblApplicableFrom);
            this.pnlCreateDetail.Dock = System.Windows.Forms.DockStyle.None;
            this.pnlCreateDetail.Location = new System.Drawing.Point(0, 255);
            this.pnlCreateDetail.Padding = new System.Windows.Forms.Padding(10);
            this.pnlCreateDetail.Size = new System.Drawing.Size(1005, 214);
            this.pnlCreateDetail.TabIndex = 3;
            this.pnlCreateDetail.Controls.SetChildIndex(this.lblApplicableFrom, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.txtEndRange, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.lblActive, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.lblApplicableTo, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.btnAddDetails, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.dtpApplicableFromDate, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.lblEndRange, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.dtpApplicableToDate, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.btnClearDetails, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.txtStartRange, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.lblStartText, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.lblStartRange, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.txtStartText, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.chkActive, 0);
            this.pnlCreateDetail.Controls.SetChildIndex(this.dgvGiftVoucherDetails, 0);
            // 
            // pnlLowerButtons
            // 
            this.pnlLowerButtons.Controls.Add(this.btnNew);
            this.pnlLowerButtons.Location = new System.Drawing.Point(0, 473);
            this.pnlLowerButtons.TabIndex = 4;
            this.pnlLowerButtons.Controls.SetChildIndex(this.btnCreateReset, 0);
            this.pnlLowerButtons.Controls.SetChildIndex(this.btnSave, 0);
            this.pnlLowerButtons.Controls.SetChildIndex(this.btnNew, 0);
            // 
            // pnlTopButtons
            // 
            this.pnlTopButtons.Location = new System.Drawing.Point(0, 76);
            this.pnlTopButtons.Size = new System.Drawing.Size(1003, 0);
            // 
            // pnlSearchButtons
            // 
            this.pnlSearchButtons.Location = new System.Drawing.Point(0, 96);
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
            // txtSearchVoucherName
            // 
            this.txtSearchVoucherName.Location = new System.Drawing.Point(495, 18);
            this.txtSearchVoucherName.MaxLength = 50;
            this.txtSearchVoucherName.Name = "txtSearchVoucherName";
            this.txtSearchVoucherName.Size = new System.Drawing.Size(130, 21);
            this.txtSearchVoucherName.TabIndex = 1;
            // 
            // lblSearchVoucherName
            // 
            this.lblSearchVoucherName.Location = new System.Drawing.Point(364, 21);
            this.lblSearchVoucherName.Name = "lblSearchVoucherName";
            this.lblSearchVoucherName.Size = new System.Drawing.Size(125, 13);
            this.lblSearchVoucherName.TabIndex = 40;
            this.lblSearchVoucherName.Text = "Voucher Name:";
            this.lblSearchVoucherName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSearchVoucherCode
            // 
            this.txtSearchVoucherCode.Location = new System.Drawing.Point(193, 18);
            this.txtSearchVoucherCode.MaxLength = 20;
            this.txtSearchVoucherCode.Name = "txtSearchVoucherCode";
            this.txtSearchVoucherCode.Size = new System.Drawing.Size(130, 21);
            this.txtSearchVoucherCode.TabIndex = 0;
            // 
            // lblSearchVoucherCode
            // 
            this.lblSearchVoucherCode.Location = new System.Drawing.Point(62, 21);
            this.lblSearchVoucherCode.Name = "lblSearchVoucherCode";
            this.lblSearchVoucherCode.Size = new System.Drawing.Size(125, 13);
            this.lblSearchVoucherCode.TabIndex = 39;
            this.lblSearchVoucherCode.Text = "VoucherCode:";
            this.lblSearchVoucherCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dgvSearchGiftVoucher
            // 
            this.dgvSearchGiftVoucher.AllowUserToAddRows = false;
            this.dgvSearchGiftVoucher.AllowUserToDeleteRows = false;
            this.dgvSearchGiftVoucher.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dgvSearchGiftVoucher.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSearchGiftVoucher.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSearchGiftVoucher.Location = new System.Drawing.Point(0, 0);
            this.dgvSearchGiftVoucher.MultiSelect = false;
            this.dgvSearchGiftVoucher.Name = "dgvSearchGiftVoucher";
            this.dgvSearchGiftVoucher.ReadOnly = true;
            this.dgvSearchGiftVoucher.RowHeadersVisible = false;
            this.dgvSearchGiftVoucher.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSearchGiftVoucher.Size = new System.Drawing.Size(1005, 453);
            this.dgvSearchGiftVoucher.TabIndex = 0;
            this.dgvSearchGiftVoucher.TabStop = false;
            this.dgvSearchGiftVoucher.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgvSearchGiftVoucher_MouseDoubleClick);
            this.dgvSearchGiftVoucher.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSearchGiftVoucher_CellClick);
            // 
            // dtpSearchfrmDate
            // 
            this.dtpSearchfrmDate.Checked = false;
            this.dtpSearchfrmDate.CustomFormat = "dd-MM-yyyy";
            this.dtpSearchfrmDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpSearchfrmDate.Location = new System.Drawing.Point(193, 56);
            this.dtpSearchfrmDate.Name = "dtpSearchfrmDate";
            this.dtpSearchfrmDate.ShowCheckBox = true;
            this.dtpSearchfrmDate.Size = new System.Drawing.Size(130, 21);
            this.dtpSearchfrmDate.TabIndex = 3;
            // 
            // dtpSearchToDate
            // 
            this.dtpSearchToDate.Checked = false;
            this.dtpSearchToDate.CustomFormat = "dd-MM-yyyy";
            this.dtpSearchToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpSearchToDate.Location = new System.Drawing.Point(495, 57);
            this.dtpSearchToDate.Name = "dtpSearchToDate";
            this.dtpSearchToDate.ShowCheckBox = true;
            this.dtpSearchToDate.Size = new System.Drawing.Size(130, 21);
            this.dtpSearchToDate.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.label1.Location = new System.Drawing.Point(334, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(155, 13);
            this.label1.TabIndex = 139;
            this.label1.Text = "To Date:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCreateVoucherCode
            // 
            this.lblCreateVoucherCode.Location = new System.Drawing.Point(22, 15);
            this.lblCreateVoucherCode.Name = "lblCreateVoucherCode";
            this.lblCreateVoucherCode.Size = new System.Drawing.Size(125, 13);
            this.lblCreateVoucherCode.TabIndex = 41;
            this.lblCreateVoucherCode.Text = "VoucherCode:";
            this.lblCreateVoucherCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtVoucherName
            // 
            this.txtVoucherName.Location = new System.Drawing.Point(448, 12);
            this.txtVoucherName.MaxLength = 50;
            this.txtVoucherName.Name = "txtVoucherName";
            this.txtVoucherName.Size = new System.Drawing.Size(130, 21);
            this.txtVoucherName.TabIndex = 0;
            // 
            // lblVoucherName
            // 
            this.lblVoucherName.Location = new System.Drawing.Point(317, 15);
            this.lblVoucherName.Name = "lblVoucherName";
            this.lblVoucherName.Size = new System.Drawing.Size(125, 13);
            this.lblVoucherName.TabIndex = 43;
            this.lblVoucherName.Text = "Voucher Name:*";
            this.lblVoucherName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtVoucherDescription
            // 
            this.txtVoucherDescription.Location = new System.Drawing.Point(781, 12);
            this.txtVoucherDescription.MaxLength = 100;
            this.txtVoucherDescription.Name = "txtVoucherDescription";
            this.txtVoucherDescription.Size = new System.Drawing.Size(130, 21);
            this.txtVoucherDescription.TabIndex = 1;
            // 
            // lblVoucherDescription
            // 
            this.lblVoucherDescription.Location = new System.Drawing.Point(625, 15);
            this.lblVoucherDescription.Name = "lblVoucherDescription";
            this.lblVoucherDescription.Size = new System.Drawing.Size(150, 13);
            this.lblVoucherDescription.TabIndex = 45;
            this.lblVoucherDescription.Text = "Voucher Description:*";
            this.lblVoucherDescription.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtItemCode
            // 
            this.txtItemCode.Location = new System.Drawing.Point(149, 11);
            this.txtItemCode.MaxLength = 20;
            this.txtItemCode.Name = "txtItemCode";
            this.txtItemCode.Size = new System.Drawing.Size(110, 21);
            this.txtItemCode.TabIndex = 0;
            this.txtItemCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtItemCode_KeyDown);
            this.txtItemCode.Validating += new System.ComponentModel.CancelEventHandler(this.txtItemCode_Validating);
            // 
            // lblItemCode
            // 
            this.lblItemCode.AutoSize = true;
            this.lblItemCode.Location = new System.Drawing.Point(69, 14);
            this.lblItemCode.Name = "lblItemCode";
            this.lblItemCode.Size = new System.Drawing.Size(76, 13);
            this.lblItemCode.TabIndex = 52;
            this.lblItemCode.Text = "ItemCode:*";
            this.lblItemCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtItemDescription
            // 
            this.txtItemDescription.Location = new System.Drawing.Point(488, 11);
            this.txtItemDescription.MaxLength = 100;
            this.txtItemDescription.Name = "txtItemDescription";
            this.txtItemDescription.ReadOnly = true;
            this.txtItemDescription.Size = new System.Drawing.Size(130, 21);
            this.txtItemDescription.TabIndex = 3;
            this.txtItemDescription.TabStop = false;
            // 
            // lblItemDescription
            // 
            this.lblItemDescription.AutoSize = true;
            this.lblItemDescription.Location = new System.Drawing.Point(368, 14);
            this.lblItemDescription.Name = "lblItemDescription";
            this.lblItemDescription.Size = new System.Drawing.Size(114, 13);
            this.lblItemDescription.TabIndex = 54;
            this.lblItemDescription.Text = "Item Description:*";
            this.lblItemDescription.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtMinBuyAmount
            // 
            this.txtMinBuyAmount.Location = new System.Drawing.Point(151, 40);
            this.txtMinBuyAmount.MaxLength = 25;
            this.txtMinBuyAmount.Name = "txtMinBuyAmount";
            this.txtMinBuyAmount.Size = new System.Drawing.Size(130, 21);
            this.txtMinBuyAmount.TabIndex = 4;
            this.txtMinBuyAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtMinBuyAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_KeyDown);
            // 
            // lblBuyAmount
            // 
            this.lblBuyAmount.Location = new System.Drawing.Point(20, 43);
            this.lblBuyAmount.Name = "lblBuyAmount";
            this.lblBuyAmount.Size = new System.Drawing.Size(125, 13);
            this.lblBuyAmount.TabIndex = 56;
            this.lblBuyAmount.Text = "Min Buy Amount:";
            this.lblBuyAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblActive
            // 
            this.lblActive.Location = new System.Drawing.Point(653, 41);
            this.lblActive.Name = "lblActive";
            this.lblActive.Size = new System.Drawing.Size(125, 13);
            this.lblActive.TabIndex = 56;
            this.lblActive.Text = "Active:";
            this.lblActive.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtEndRange
            // 
            this.txtEndRange.Location = new System.Drawing.Point(784, 10);
            this.txtEndRange.MaxLength = 6;
            this.txtEndRange.Name = "txtEndRange";
            this.txtEndRange.Size = new System.Drawing.Size(110, 21);
            this.txtEndRange.TabIndex = 2;
            this.txtEndRange.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_KeyDown);
            // 
            // lblEndRange
            // 
            this.lblEndRange.Location = new System.Drawing.Point(653, 13);
            this.lblEndRange.Name = "lblEndRange";
            this.lblEndRange.Size = new System.Drawing.Size(125, 13);
            this.lblEndRange.TabIndex = 54;
            this.lblEndRange.Text = "End Series Range:*";
            this.lblEndRange.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtStartRange
            // 
            this.txtStartRange.Location = new System.Drawing.Point(462, 10);
            this.txtStartRange.MaxLength = 6;
            this.txtStartRange.Name = "txtStartRange";
            this.txtStartRange.Size = new System.Drawing.Size(110, 21);
            this.txtStartRange.TabIndex = 1;
            this.txtStartRange.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_KeyDown);
            // 
            // lblStartRange
            // 
            this.lblStartRange.Location = new System.Drawing.Point(303, 13);
            this.lblStartRange.Name = "lblStartRange";
            this.lblStartRange.Size = new System.Drawing.Size(150, 13);
            this.lblStartRange.TabIndex = 52;
            this.lblStartRange.Text = "Start Series Range:*";
            this.lblStartRange.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblApplicableFrom
            // 
            this.lblApplicableFrom.Location = new System.Drawing.Point(36, 41);
            this.lblApplicableFrom.Name = "lblApplicableFrom";
            this.lblApplicableFrom.Size = new System.Drawing.Size(125, 13);
            this.lblApplicableFrom.TabIndex = 58;
            this.lblApplicableFrom.Text = "Applicable From:";
            this.lblApplicableFrom.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblApplicableTo
            // 
            this.lblApplicableTo.Location = new System.Drawing.Point(331, 41);
            this.lblApplicableTo.Name = "lblApplicableTo";
            this.lblApplicableTo.Size = new System.Drawing.Size(125, 13);
            this.lblApplicableTo.TabIndex = 59;
            this.lblApplicableTo.Text = "Applicable To:";
            this.lblApplicableTo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpApplicableFromDate
            // 
            this.dtpApplicableFromDate.CustomFormat = "dd-MM-yyyy";
            this.dtpApplicableFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpApplicableFromDate.Location = new System.Drawing.Point(167, 37);
            this.dtpApplicableFromDate.Name = "dtpApplicableFromDate";
            this.dtpApplicableFromDate.ShowCheckBox = true;
            this.dtpApplicableFromDate.Size = new System.Drawing.Size(110, 21);
            this.dtpApplicableFromDate.TabIndex = 3;
            // 
            // dtpApplicableToDate
            // 
            this.dtpApplicableToDate.CustomFormat = "dd-MM-yyyy";
            this.dtpApplicableToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpApplicableToDate.Location = new System.Drawing.Point(462, 37);
            this.dtpApplicableToDate.Name = "dtpApplicableToDate";
            this.dtpApplicableToDate.ShowCheckBox = true;
            this.dtpApplicableToDate.Size = new System.Drawing.Size(110, 21);
            this.dtpApplicableToDate.TabIndex = 4;
            // 
            // dgvGiftVoucherDetails
            // 
            this.dgvGiftVoucherDetails.AllowUserToAddRows = false;
            this.dgvGiftVoucherDetails.AllowUserToDeleteRows = false;
            this.dgvGiftVoucherDetails.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dgvGiftVoucherDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGiftVoucherDetails.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvGiftVoucherDetails.Location = new System.Drawing.Point(10, 69);
            this.dgvGiftVoucherDetails.MultiSelect = false;
            this.dgvGiftVoucherDetails.Name = "dgvGiftVoucherDetails";
            this.dgvGiftVoucherDetails.RowHeadersVisible = false;
            this.dgvGiftVoucherDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvGiftVoucherDetails.ShowCellErrors = false;
            this.dgvGiftVoucherDetails.ShowEditingIcon = false;
            this.dgvGiftVoucherDetails.ShowRowErrors = false;
            this.dgvGiftVoucherDetails.Size = new System.Drawing.Size(985, 135);
            this.dgvGiftVoucherDetails.TabIndex = 0;
            this.dgvGiftVoucherDetails.TabStop = false;
            this.dgvGiftVoucherDetails.SelectionChanged += new System.EventHandler(this.dgvGiftVoucherDetails_SelectionChanged);
            // 
            // btnNew
            // 
            this.btnNew.BackColor = System.Drawing.Color.Transparent;
            this.btnNew.BackgroundImage = global::PromotionsComponent.Properties.Resources.button;
            this.btnNew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnNew.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnNew.FlatAppearance.BorderSize = 0;
            this.btnNew.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnNew.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNew.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnNew.Location = new System.Drawing.Point(745, 0);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(110, 32);
            this.btnNew.TabIndex = 4;
            this.btnNew.Text = "&New Series";
            this.btnNew.UseVisualStyleBackColor = false;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // txtStartText
            // 
            this.txtStartText.Location = new System.Drawing.Point(167, 10);
            this.txtStartText.MaxLength = 44;
            this.txtStartText.Name = "txtStartText";
            this.txtStartText.Size = new System.Drawing.Size(110, 21);
            this.txtStartText.TabIndex = 0;
            // 
            // lblStartText
            // 
            this.lblStartText.Location = new System.Drawing.Point(36, 13);
            this.lblStartText.Name = "lblStartText";
            this.lblStartText.Size = new System.Drawing.Size(125, 13);
            this.lblStartText.TabIndex = 63;
            this.lblStartText.Text = "Series Start Text:*";
            this.lblStartText.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkActive
            // 
            this.chkActive.AutoSize = true;
            this.chkActive.Location = new System.Drawing.Point(784, 41);
            this.chkActive.Name = "chkActive";
            this.chkActive.Size = new System.Drawing.Size(15, 14);
            this.chkActive.TabIndex = 5;
            this.chkActive.UseVisualStyleBackColor = true;
            // 
            // lblVoucherCodeValue
            // 
            this.lblVoucherCodeValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblVoucherCodeValue.Location = new System.Drawing.Point(151, 12);
            this.lblVoucherCodeValue.Name = "lblVoucherCodeValue";
            this.lblVoucherCodeValue.Size = new System.Drawing.Size(130, 21);
            this.lblVoucherCodeValue.TabIndex = 58;
            this.lblVoucherCodeValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // errorCreate
            // 
            this.errorCreate.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorCreate.ContainerControl = this;
            // 
            // errorSearch
            // 
            this.errorSearch.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorSearch.ContainerControl = this;
            // 
            // txtSearcItemCode
            // 
            this.txtSearcItemCode.Location = new System.Drawing.Point(797, 18);
            this.txtSearcItemCode.MaxLength = 20;
            this.txtSearcItemCode.Name = "txtSearcItemCode";
            this.txtSearcItemCode.Size = new System.Drawing.Size(130, 21);
            this.txtSearcItemCode.TabIndex = 2;
            this.txtSearcItemCode.Visible = false;
            // 
            // lblSearchItemCode
            // 
            this.lblSearchItemCode.Location = new System.Drawing.Point(666, 21);
            this.lblSearchItemCode.Name = "lblSearchItemCode";
            this.lblSearchItemCode.Size = new System.Drawing.Size(125, 13);
            this.lblSearchItemCode.TabIndex = 141;
            this.lblSearchItemCode.Text = "Item Code:";
            this.lblSearchItemCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblSearchItemCode.Visible = false;
            // 
            // lblSearchfrmPODate
            // 
            this.lblSearchfrmPODate.Location = new System.Drawing.Point(32, 61);
            this.lblSearchfrmPODate.Name = "lblSearchfrmPODate";
            this.lblSearchfrmPODate.Size = new System.Drawing.Size(155, 13);
            this.lblSearchfrmPODate.TabIndex = 137;
            this.lblSearchfrmPODate.Text = "From Date:";
            this.lblSearchfrmPODate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlCreateItemDetail
            // 
            this.pnlCreateItemDetail.Controls.Add(this.pictureBox1);
            this.pnlCreateItemDetail.Controls.Add(this.btnClearItemDetail);
            this.pnlCreateItemDetail.Controls.Add(this.btnAddItemDetail);
            this.pnlCreateItemDetail.Controls.Add(this.txtItemQty);
            this.pnlCreateItemDetail.Controls.Add(this.lblItemQty);
            this.pnlCreateItemDetail.Controls.Add(this.dgvGiftVoucherItemDetails);
            this.pnlCreateItemDetail.Controls.Add(this.txtItemCode);
            this.pnlCreateItemDetail.Controls.Add(this.lblItemCode);
            this.pnlCreateItemDetail.Controls.Add(this.lblItemDescription);
            this.pnlCreateItemDetail.Controls.Add(this.txtItemDescription);
            this.pnlCreateItemDetail.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCreateItemDetail.Location = new System.Drawing.Point(0, 14);
            this.pnlCreateItemDetail.Name = "pnlCreateItemDetail";
            this.pnlCreateItemDetail.Padding = new System.Windows.Forms.Padding(10);
            this.pnlCreateItemDetail.Size = new System.Drawing.Size(1005, 206);
            this.pnlCreateItemDetail.TabIndex = 2;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::PromotionsComponent.Properties.Resources.find;
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(262, 11);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(21, 21);
            this.pictureBox1.TabIndex = 59;
            this.pictureBox1.TabStop = false;
            // 
            // btnClearItemDetail
            // 
            this.btnClearItemDetail.BackColor = System.Drawing.Color.Transparent;
            this.btnClearItemDetail.BackgroundImage = global::PromotionsComponent.Properties.Resources.button;
            this.btnClearItemDetail.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearItemDetail.FlatAppearance.BorderSize = 0;
            this.btnClearItemDetail.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnClearItemDetail.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnClearItemDetail.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearItemDetail.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnClearItemDetail.Location = new System.Drawing.Point(896, 36);
            this.btnClearItemDetail.Name = "btnClearItemDetail";
            this.btnClearItemDetail.Size = new System.Drawing.Size(80, 32);
            this.btnClearItemDetail.TabIndex = 3;
            this.btnClearItemDetail.Text = "Cl&ear";
            this.btnClearItemDetail.UseVisualStyleBackColor = false;
            this.btnClearItemDetail.Click += new System.EventHandler(this.btnClearItemDetail_Click);
            // 
            // btnAddItemDetail
            // 
            this.btnAddItemDetail.BackColor = System.Drawing.Color.Transparent;
            this.btnAddItemDetail.BackgroundImage = global::PromotionsComponent.Properties.Resources.button;
            this.btnAddItemDetail.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAddItemDetail.FlatAppearance.BorderSize = 0;
            this.btnAddItemDetail.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnAddItemDetail.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnAddItemDetail.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddItemDetail.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnAddItemDetail.Location = new System.Drawing.Point(810, 36);
            this.btnAddItemDetail.Name = "btnAddItemDetail";
            this.btnAddItemDetail.Size = new System.Drawing.Size(80, 32);
            this.btnAddItemDetail.TabIndex = 2;
            this.btnAddItemDetail.Text = "&Add";
            this.btnAddItemDetail.UseVisualStyleBackColor = false;
            this.btnAddItemDetail.Click += new System.EventHandler(this.btnAddItemDetail_Click);
            // 
            // txtItemQty
            // 
            this.txtItemQty.Location = new System.Drawing.Point(779, 11);
            this.txtItemQty.MaxLength = 25;
            this.txtItemQty.Name = "txtItemQty";
            this.txtItemQty.Size = new System.Drawing.Size(69, 21);
            this.txtItemQty.TabIndex = 1;
            this.txtItemQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblItemQty
            // 
            this.lblItemQty.AutoSize = true;
            this.lblItemQty.Location = new System.Drawing.Point(734, 14);
            this.lblItemQty.Name = "lblItemQty";
            this.lblItemQty.Size = new System.Drawing.Size(39, 13);
            this.lblItemQty.TabIndex = 58;
            this.lblItemQty.Text = "Qty:*";
            this.lblItemQty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dgvGiftVoucherItemDetails
            // 
            this.dgvGiftVoucherItemDetails.AllowUserToAddRows = false;
            this.dgvGiftVoucherItemDetails.AllowUserToDeleteRows = false;
            this.dgvGiftVoucherItemDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvGiftVoucherItemDetails.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dgvGiftVoucherItemDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGiftVoucherItemDetails.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvGiftVoucherItemDetails.Location = new System.Drawing.Point(10, 73);
            this.dgvGiftVoucherItemDetails.MultiSelect = false;
            this.dgvGiftVoucherItemDetails.Name = "dgvGiftVoucherItemDetails";
            this.dgvGiftVoucherItemDetails.RowHeadersVisible = false;
            this.dgvGiftVoucherItemDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvGiftVoucherItemDetails.ShowCellErrors = false;
            this.dgvGiftVoucherItemDetails.ShowEditingIcon = false;
            this.dgvGiftVoucherItemDetails.ShowRowErrors = false;
            this.dgvGiftVoucherItemDetails.Size = new System.Drawing.Size(985, 123);
            this.dgvGiftVoucherItemDetails.TabIndex = 0;
            this.dgvGiftVoucherItemDetails.TabStop = false;
            this.dgvGiftVoucherItemDetails.SelectionChanged += new System.EventHandler(this.dgvGiftVoucherItemDetails_SelectionChanged);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.SteelBlue;
            this.label2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(1, 224);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1002, 24);
            this.label2.TabIndex = 5;
            this.label2.Text = "Voucher-Series Details";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmGiftVoucher
            // 
            this.AcceptButton = this.btnSearch;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1013, 703);
            this.Name = "frmGiftVoucher";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "frmGiftVoucher";
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
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearchGiftVoucher)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGiftVoucherDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorCreate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorSearch)).EndInit();
            this.pnlCreateItemDetail.ResumeLayout(false);
            this.pnlCreateItemDetail.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGiftVoucherItemDetails)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtSearchVoucherName;
        private System.Windows.Forms.Label lblSearchVoucherName;
        private System.Windows.Forms.TextBox txtSearchVoucherCode;
        private System.Windows.Forms.Label lblSearchVoucherCode;
        private System.Windows.Forms.DataGridView dgvSearchGiftVoucher;
        private System.Windows.Forms.DateTimePicker dtpSearchToDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpSearchfrmDate;
        private System.Windows.Forms.TextBox txtVoucherName;
        private System.Windows.Forms.Label lblVoucherName;
        private System.Windows.Forms.Label lblCreateVoucherCode;
        private System.Windows.Forms.TextBox txtVoucherDescription;
        private System.Windows.Forms.Label lblVoucherDescription;
        private System.Windows.Forms.TextBox txtItemDescription;
        private System.Windows.Forms.Label lblItemDescription;
        private System.Windows.Forms.TextBox txtItemCode;
        private System.Windows.Forms.Label lblItemCode;
        private System.Windows.Forms.TextBox txtMinBuyAmount;
        private System.Windows.Forms.Label lblBuyAmount;
        private System.Windows.Forms.Label lblActive;
        private System.Windows.Forms.TextBox txtEndRange;
        private System.Windows.Forms.Label lblEndRange;
        private System.Windows.Forms.TextBox txtStartRange;
        private System.Windows.Forms.Label lblStartRange;
        private System.Windows.Forms.Label lblApplicableTo;
        private System.Windows.Forms.Label lblApplicableFrom;
        private System.Windows.Forms.DateTimePicker dtpApplicableToDate;
        private System.Windows.Forms.DateTimePicker dtpApplicableFromDate;
        private System.Windows.Forms.DataGridView dgvGiftVoucherDetails;
        private System.Windows.Forms.TextBox txtStartText;
        private System.Windows.Forms.Label lblStartText;
        private System.Windows.Forms.CheckBox chkActive;
        private System.Windows.Forms.Label lblVoucherCodeValue;
        private System.Windows.Forms.ErrorProvider errorCreate;
        private System.Windows.Forms.ErrorProvider errorSearch;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.TextBox txtSearcItemCode;
        private System.Windows.Forms.Label lblSearchItemCode;
        private System.Windows.Forms.Label lblSearchfrmPODate;
        private System.Windows.Forms.Panel pnlCreateItemDetail;
        private System.Windows.Forms.DataGridView dgvGiftVoucherItemDetails;
        private System.Windows.Forms.TextBox txtItemQty;
        private System.Windows.Forms.Label lblItemQty;
        private System.Windows.Forms.Button btnAddItemDetail;
        private System.Windows.Forms.Button btnClearItemDetail;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}