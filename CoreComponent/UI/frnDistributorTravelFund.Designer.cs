namespace CoreComponent.UI
{
    partial class frnDistributorTravelFund
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frnDistributorTravelFund));
            this.lblDistributorID = new System.Windows.Forms.Label();
            this.txtDistributorID = new System.Windows.Forms.TextBox();
            this.dgvTravelFundDetails = new System.Windows.Forms.DataGridView();
            this.lblDistributorName = new System.Windows.Forms.Label();
            this.lblBalance = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblBalanceAmount = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.errTravelFundValidate = new System.Windows.Forms.ErrorProvider(this.components);
            this.lblDistributorSearch = new System.Windows.Forms.Label();
            this.txtDistributorSearch = new System.Windows.Forms.TextBox();
            this.lblDateSearch = new System.Windows.Forms.Label();
            this.dtpDateSearch = new System.Windows.Forms.DateTimePicker();
            this.dgvTravelFundSearch = new System.Windows.Forms.DataGridView();
            this.btnPrint = new System.Windows.Forms.Button();
            this.ofdExcel = new System.Windows.Forms.OpenFileDialog();
            this.lblDate = new System.Windows.Forms.Label();
            this.dtpBusinessMonth = new System.Windows.Forms.DateTimePicker();
            this.lblBeneficiary = new System.Windows.Forms.Label();
            this.cmbBeneficiary = new System.Windows.Forms.ComboBox();
            this.lblRemarks = new System.Windows.Forms.Label();
            this.txtRemarks = new System.Windows.Forms.TextBox();
            this.lblAmount = new System.Windows.Forms.Label();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnClearDet = new System.Windows.Forms.Button();
            this.btnAddDet = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btnBulkImport = new System.Windows.Forms.Button();
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtImportExcel = new System.Windows.Forms.TextBox();
            this.gbBulkImport = new System.Windows.Forms.GroupBox();
            this.rbtnAdjustment = new System.Windows.Forms.RadioButton();
            this.rbtnPayment = new System.Windows.Forms.RadioButton();
            this.btnSearchResetNew = new System.Windows.Forms.Button();
            this.btnSearchNew = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblRule = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tabPageSearch.SuspendLayout();
            this.pnlSearchHeader.SuspendLayout();
            this.tabPageCreate.SuspendLayout();
            this.pnlCreateDetails.SuspendLayout();
            this.pnlDetailsHeader.SuspendLayout();
            this.pnlBottomButtons.SuspendLayout();
            this.pnlSearchGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTravelFundDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errTravelFundValidate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTravelFundSearch)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.gbBulkImport.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlSearchHeader
            // 
            this.pnlSearchHeader.Controls.Add(this.groupBox2);
            this.pnlSearchHeader.Controls.Add(this.gbBulkImport);
            this.pnlSearchHeader.Controls.SetChildIndex(this.gbBulkImport, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.groupBox2, 0);
            // 
            // btnSearch
            // 
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearch.TabIndex = 3;
            this.btnSearch.Visible = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnSearchReset
            // 
            this.btnSearchReset.FlatAppearance.BorderSize = 0;
            this.btnSearchReset.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearchReset.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearchReset.TabIndex = 4;
            this.btnSearchReset.Visible = false;
            this.btnSearchReset.Click += new System.EventHandler(this.btnSearchReset_Click);
            // 
            // pnlCreateDetails
            // 
            this.pnlCreateDetails.Controls.Add(this.label4);
            this.pnlCreateDetails.Controls.Add(this.label2);
            this.pnlCreateDetails.Controls.Add(this.lblRule);
            this.pnlCreateDetails.Controls.Add(this.dgvTravelFundDetails);
            this.pnlCreateDetails.Size = new System.Drawing.Size(1016, 310);
            this.pnlCreateDetails.TabIndex = 0;
            // 
            // pnlDetailsHeader
            // 
            this.pnlDetailsHeader.Controls.Add(this.groupBox1);
            this.pnlDetailsHeader.Controls.Add(this.lblName);
            this.pnlDetailsHeader.Controls.Add(this.label7);
            this.pnlDetailsHeader.Controls.Add(this.lblBalanceAmount);
            this.pnlDetailsHeader.Controls.Add(this.lblBalance);
            this.pnlDetailsHeader.Controls.Add(this.lblDistributorName);
            this.pnlDetailsHeader.Controls.Add(this.lblDistributorID);
            this.pnlDetailsHeader.Controls.Add(this.txtDistributorID);
            this.pnlDetailsHeader.TabIndex = 0;
            this.pnlDetailsHeader.Controls.SetChildIndex(this.txtDistributorID, 0);
            this.pnlDetailsHeader.Controls.SetChildIndex(this.lblDistributorID, 0);
            this.pnlDetailsHeader.Controls.SetChildIndex(this.lblDistributorName, 0);
            this.pnlDetailsHeader.Controls.SetChildIndex(this.lblBalance, 0);
            this.pnlDetailsHeader.Controls.SetChildIndex(this.lblBalanceAmount, 0);
            this.pnlDetailsHeader.Controls.SetChildIndex(this.label7, 0);
            this.pnlDetailsHeader.Controls.SetChildIndex(this.lblName, 0);
            this.pnlDetailsHeader.Controls.SetChildIndex(this.groupBox1, 0);
            // 
            // btnClearDetails
            // 
            this.btnClearDetails.FlatAppearance.BorderSize = 0;
            this.btnClearDetails.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnClearDetails.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnClearDetails.Visible = false;
            // 
            // btnAddDetails
            // 
            this.btnAddDetails.FlatAppearance.BorderSize = 0;
            this.btnAddDetails.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnAddDetails.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnAddDetails.Visible = false;
            // 
            // btnCreateReset
            // 
            this.btnCreateReset.Dock = System.Windows.Forms.DockStyle.None;
            this.btnCreateReset.FlatAppearance.BorderSize = 0;
            this.btnCreateReset.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnCreateReset.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnCreateReset.Location = new System.Drawing.Point(663, -1);
            this.btnCreateReset.TabIndex = 8;
            this.btnCreateReset.Click += new System.EventHandler(this.btnCreateReset_Click);
            // 
            // btnSave
            // 
            this.btnSave.Dock = System.Windows.Forms.DockStyle.None;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSave.Location = new System.Drawing.Point(582, -1);
            this.btnSave.TabIndex = 7;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // pnlBottomButtons
            // 
            this.pnlBottomButtons.Controls.Add(this.btnPrint);
            this.pnlBottomButtons.TabIndex = 0;
            this.pnlBottomButtons.Controls.SetChildIndex(this.btnCreateReset, 0);
            this.pnlBottomButtons.Controls.SetChildIndex(this.btnSave, 0);
            this.pnlBottomButtons.Controls.SetChildIndex(this.btnPrint, 0);
            // 
            // pnlSearchGrid
            // 
            this.pnlSearchGrid.Controls.Add(this.dgvTravelFundSearch);
            // 
            // lblDistributorID
            // 
            this.lblDistributorID.AutoSize = true;
            this.lblDistributorID.Location = new System.Drawing.Point(46, 10);
            this.lblDistributorID.Name = "lblDistributorID";
            this.lblDistributorID.Size = new System.Drawing.Size(94, 13);
            this.lblDistributorID.TabIndex = 0;
            this.lblDistributorID.Text = "Distributor ID :";
            // 
            // txtDistributorID
            // 
            this.txtDistributorID.Location = new System.Drawing.Point(143, 8);
            this.txtDistributorID.MaxLength = 8;
            this.txtDistributorID.Name = "txtDistributorID";
            this.txtDistributorID.Size = new System.Drawing.Size(100, 21);
            this.txtDistributorID.TabIndex = 1;
            this.txtDistributorID.Leave += new System.EventHandler(this.txtDistributorID_Leave);
            this.txtDistributorID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDistributorID_KeyPress);
            // 
            // dgvTravelFundDetails
            // 
            this.dgvTravelFundDetails.AllowUserToAddRows = false;
            this.dgvTravelFundDetails.AllowUserToDeleteRows = false;
            this.dgvTravelFundDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTravelFundDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvTravelFundDetails.Location = new System.Drawing.Point(13, 10);
            this.dgvTravelFundDetails.MultiSelect = false;
            this.dgvTravelFundDetails.Name = "dgvTravelFundDetails";
            this.dgvTravelFundDetails.ReadOnly = true;
            this.dgvTravelFundDetails.RowHeadersVisible = false;
            this.dgvTravelFundDetails.Size = new System.Drawing.Size(975, 210);
            this.dgvTravelFundDetails.TabIndex = 0;
            this.dgvTravelFundDetails.TabStop = false;
            // 
            // lblDistributorName
            // 
            this.lblDistributorName.AutoSize = true;
            this.lblDistributorName.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblDistributorName.Location = new System.Drawing.Point(321, 12);
            this.lblDistributorName.Name = "lblDistributorName";
            this.lblDistributorName.Size = new System.Drawing.Size(118, 13);
            this.lblDistributorName.TabIndex = 0;
            this.lblDistributorName.Text = "Distributor Name";
            // 
            // lblBalance
            // 
            this.lblBalance.AutoSize = true;
            this.lblBalance.Location = new System.Drawing.Point(566, 10);
            this.lblBalance.Name = "lblBalance";
            this.lblBalance.Size = new System.Drawing.Size(113, 13);
            this.lblBalance.TabIndex = 0;
            this.lblBalance.Text = "Available Balance:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(515, -35);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "label3";
            // 
            // lblBalanceAmount
            // 
            this.lblBalanceAmount.AutoSize = true;
            this.lblBalanceAmount.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblBalanceAmount.Location = new System.Drawing.Point(678, 10);
            this.lblBalanceAmount.Name = "lblBalanceAmount";
            this.lblBalanceAmount.Size = new System.Drawing.Size(112, 13);
            this.lblBalanceAmount.TabIndex = 0;
            this.lblBalanceAmount.Text = "Balance Amount";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(260, 11);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(49, 13);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Name :";
            // 
            // errTravelFundValidate
            // 
            this.errTravelFundValidate.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errTravelFundValidate.ContainerControl = this;
            // 
            // lblDistributorSearch
            // 
            this.lblDistributorSearch.AutoSize = true;
            this.lblDistributorSearch.Location = new System.Drawing.Point(2, 31);
            this.lblDistributorSearch.Name = "lblDistributorSearch";
            this.lblDistributorSearch.Size = new System.Drawing.Size(94, 13);
            this.lblDistributorSearch.TabIndex = 0;
            this.lblDistributorSearch.Text = "Distributor ID :";
            // 
            // txtDistributorSearch
            // 
            this.txtDistributorSearch.Location = new System.Drawing.Point(125, 27);
            this.txtDistributorSearch.MaxLength = 8;
            this.txtDistributorSearch.Name = "txtDistributorSearch";
            this.txtDistributorSearch.Size = new System.Drawing.Size(100, 21);
            this.txtDistributorSearch.TabIndex = 1;
            this.txtDistributorSearch.Leave += new System.EventHandler(this.txtDistributorSearch_Leave);
            this.txtDistributorSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDistributorSearch_KeyPress);
            // 
            // lblDateSearch
            // 
            this.lblDateSearch.AutoSize = true;
            this.lblDateSearch.Location = new System.Drawing.Point(2, 71);
            this.lblDateSearch.Name = "lblDateSearch";
            this.lblDateSearch.Size = new System.Drawing.Size(104, 13);
            this.lblDateSearch.TabIndex = 0;
            this.lblDateSearch.Text = "Business Month :";
            // 
            // dtpDateSearch
            // 
            this.dtpDateSearch.Checked = false;
            this.dtpDateSearch.CustomFormat = "MMM-yyyy";
            this.dtpDateSearch.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDateSearch.Location = new System.Drawing.Point(125, 67);
            this.dtpDateSearch.Name = "dtpDateSearch";
            this.dtpDateSearch.ShowCheckBox = true;
            this.dtpDateSearch.Size = new System.Drawing.Size(100, 21);
            this.dtpDateSearch.TabIndex = 2;
            // 
            // dgvTravelFundSearch
            // 
            this.dgvTravelFundSearch.AllowUserToAddRows = false;
            this.dgvTravelFundSearch.AllowUserToDeleteRows = false;
            this.dgvTravelFundSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTravelFundSearch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvTravelFundSearch.Location = new System.Drawing.Point(9, 6);
            this.dgvTravelFundSearch.MultiSelect = false;
            this.dgvTravelFundSearch.Name = "dgvTravelFundSearch";
            this.dgvTravelFundSearch.ReadOnly = true;
            this.dgvTravelFundSearch.RowHeadersVisible = false;
            this.dgvTravelFundSearch.Size = new System.Drawing.Size(1000, 225);
            this.dgvTravelFundSearch.TabIndex = 0;
            this.dgvTravelFundSearch.TabStop = false;
            this.dgvTravelFundSearch.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTravelFundSearch_CellContentClick);
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.Color.Transparent;
            this.btnPrint.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPrint.BackgroundImage")));
            this.btnPrint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPrint.FlatAppearance.BorderSize = 0;
            this.btnPrint.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnPrint.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnPrint.Location = new System.Drawing.Point(739, 0);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 32);
            this.btnPrint.TabIndex = 9;
            this.btnPrint.Text = "&Print";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(463, 29);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(43, 13);
            this.lblDate.TabIndex = 9;
            this.lblDate.Text = "Date :";
            // 
            // dtpBusinessMonth
            // 
            this.dtpBusinessMonth.CustomFormat = "dd-MM-yyyy";
            this.dtpBusinessMonth.Enabled = false;
            this.dtpBusinessMonth.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpBusinessMonth.Location = new System.Drawing.Point(512, 25);
            this.dtpBusinessMonth.Name = "dtpBusinessMonth";
            this.dtpBusinessMonth.Size = new System.Drawing.Size(100, 21);
            this.dtpBusinessMonth.TabIndex = 7;
            // 
            // lblBeneficiary
            // 
            this.lblBeneficiary.AutoSize = true;
            this.lblBeneficiary.Location = new System.Drawing.Point(227, 28);
            this.lblBeneficiary.Name = "lblBeneficiary";
            this.lblBeneficiary.Size = new System.Drawing.Size(80, 13);
            this.lblBeneficiary.TabIndex = 10;
            this.lblBeneficiary.Text = "Beneficiary :";
            // 
            // cmbBeneficiary
            // 
            this.cmbBeneficiary.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBeneficiary.FormattingEnabled = true;
            this.cmbBeneficiary.Location = new System.Drawing.Point(318, 26);
            this.cmbBeneficiary.Name = "cmbBeneficiary";
            this.cmbBeneficiary.Size = new System.Drawing.Size(121, 21);
            this.cmbBeneficiary.TabIndex = 3;
            // 
            // lblRemarks
            // 
            this.lblRemarks.AutoSize = true;
            this.lblRemarks.Location = new System.Drawing.Point(19, 54);
            this.lblRemarks.Name = "lblRemarks";
            this.lblRemarks.Size = new System.Drawing.Size(67, 13);
            this.lblRemarks.TabIndex = 8;
            this.lblRemarks.Text = "Remarks :";
            // 
            // txtRemarks
            // 
            this.txtRemarks.Location = new System.Drawing.Point(96, 52);
            this.txtRemarks.Multiline = true;
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Size = new System.Drawing.Size(335, 65);
            this.txtRemarks.TabIndex = 4;
            // 
            // lblAmount
            // 
            this.lblAmount.AutoSize = true;
            this.lblAmount.Location = new System.Drawing.Point(33, 28);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(60, 13);
            this.lblAmount.TabIndex = 11;
            this.lblAmount.Text = "Amount :";
            // 
            // txtAmount
            // 
            this.txtAmount.Location = new System.Drawing.Point(98, 26);
            this.txtAmount.MaxLength = 9;
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(100, 21);
            this.txtAmount.TabIndex = 2;
            this.txtAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAmount_KeyPress);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnClearDet);
            this.groupBox1.Controls.Add(this.btnAddDet);
            this.groupBox1.Controls.Add(this.lblDate);
            this.groupBox1.Controls.Add(this.dtpBusinessMonth);
            this.groupBox1.Controls.Add(this.lblBeneficiary);
            this.groupBox1.Controls.Add(this.cmbBeneficiary);
            this.groupBox1.Controls.Add(this.lblRemarks);
            this.groupBox1.Controls.Add(this.txtRemarks);
            this.groupBox1.Controls.Add(this.lblAmount);
            this.groupBox1.Controls.Add(this.txtAmount);
            this.groupBox1.Location = new System.Drawing.Point(42, 50);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(747, 121);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Travel Fund Details ";
            // 
            // btnClearDet
            // 
            this.btnClearDet.BackColor = System.Drawing.Color.Transparent;
            this.btnClearDet.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearDet.BackgroundImage")));
            this.btnClearDet.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearDet.FlatAppearance.BorderSize = 0;
            this.btnClearDet.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnClearDet.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnClearDet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearDet.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnClearDet.Location = new System.Drawing.Point(562, 82);
            this.btnClearDet.Name = "btnClearDet";
            this.btnClearDet.Size = new System.Drawing.Size(75, 32);
            this.btnClearDet.TabIndex = 6;
            this.btnClearDet.Text = "&Clear";
            this.btnClearDet.UseVisualStyleBackColor = false;
            this.btnClearDet.Click += new System.EventHandler(this.btnClearDet_Click);
            // 
            // btnAddDet
            // 
            this.btnAddDet.BackColor = System.Drawing.Color.Transparent;
            this.btnAddDet.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAddDet.BackgroundImage")));
            this.btnAddDet.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAddDet.FlatAppearance.BorderSize = 0;
            this.btnAddDet.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnAddDet.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnAddDet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddDet.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnAddDet.Location = new System.Drawing.Point(482, 82);
            this.btnAddDet.Name = "btnAddDet";
            this.btnAddDet.Size = new System.Drawing.Size(75, 32);
            this.btnAddDet.TabIndex = 5;
            this.btnAddDet.Text = "&Add";
            this.btnAddDet.UseVisualStyleBackColor = false;
            this.btnAddDet.Click += new System.EventHandler(this.btnAddDet_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(74, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(175, 13);
            this.label3.TabIndex = 24;
            this.label3.Text = "Only \".xls\" file can be import.";
            // 
            // btnBulkImport
            // 
            this.btnBulkImport.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBulkImport.BackgroundImage")));
            this.btnBulkImport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBulkImport.FlatAppearance.BorderSize = 0;
            this.btnBulkImport.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnBulkImport.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnBulkImport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBulkImport.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnBulkImport.Location = new System.Drawing.Point(260, 71);
            this.btnBulkImport.Name = "btnBulkImport";
            this.btnBulkImport.Size = new System.Drawing.Size(120, 32);
            this.btnBulkImport.TabIndex = 25;
            this.btnBulkImport.TabStop = false;
            this.btnBulkImport.Text = "Bulk Import";
            this.btnBulkImport.UseVisualStyleBackColor = true;
            this.btnBulkImport.Click += new System.EventHandler(this.btnBulkImport_Click);
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOpenFile.BackgroundImage")));
            this.btnOpenFile.Location = new System.Drawing.Point(353, 37);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(23, 23);
            this.btnOpenFile.TabIndex = 23;
            this.btnOpenFile.TabStop = false;
            this.btnOpenFile.Text = "...";
            this.btnOpenFile.UseVisualStyleBackColor = true;
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "File Path :";
            // 
            // txtImportExcel
            // 
            this.txtImportExcel.Location = new System.Drawing.Point(77, 38);
            this.txtImportExcel.Name = "txtImportExcel";
            this.txtImportExcel.Size = new System.Drawing.Size(275, 21);
            this.txtImportExcel.TabIndex = 22;
            this.txtImportExcel.TabStop = false;
            this.txtImportExcel.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtImportExcel_KeyPress);
            this.txtImportExcel.Validating += new System.ComponentModel.CancelEventHandler(this.txtImportExcel_Validating);
            // 
            // gbBulkImport
            // 
            this.gbBulkImport.Controls.Add(this.rbtnAdjustment);
            this.gbBulkImport.Controls.Add(this.rbtnPayment);
            this.gbBulkImport.Controls.Add(this.label3);
            this.gbBulkImport.Controls.Add(this.btnBulkImport);
            this.gbBulkImport.Controls.Add(this.btnOpenFile);
            this.gbBulkImport.Controls.Add(this.label1);
            this.gbBulkImport.Controls.Add(this.txtImportExcel);
            this.gbBulkImport.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gbBulkImport.Location = new System.Drawing.Point(420, 20);
            this.gbBulkImport.Name = "gbBulkImport";
            this.gbBulkImport.Size = new System.Drawing.Size(403, 112);
            this.gbBulkImport.TabIndex = 26;
            this.gbBulkImport.TabStop = false;
            this.gbBulkImport.Text = "Bulk Import";
            // 
            // rbtnAdjustment
            // 
            this.rbtnAdjustment.AutoSize = true;
            this.rbtnAdjustment.Location = new System.Drawing.Point(254, 15);
            this.rbtnAdjustment.Name = "rbtnAdjustment";
            this.rbtnAdjustment.Size = new System.Drawing.Size(90, 17);
            this.rbtnAdjustment.TabIndex = 26;
            this.rbtnAdjustment.Text = "Adjustment";
            this.rbtnAdjustment.UseVisualStyleBackColor = true;
            // 
            // rbtnPayment
            // 
            this.rbtnPayment.AutoSize = true;
            this.rbtnPayment.Checked = true;
            this.rbtnPayment.Location = new System.Drawing.Point(77, 15);
            this.rbtnPayment.Name = "rbtnPayment";
            this.rbtnPayment.Size = new System.Drawing.Size(75, 17);
            this.rbtnPayment.TabIndex = 26;
            this.rbtnPayment.TabStop = true;
            this.rbtnPayment.Text = "Payment";
            this.rbtnPayment.UseVisualStyleBackColor = true;
            // 
            // btnSearchResetNew
            // 
            this.btnSearchResetNew.BackColor = System.Drawing.Color.Transparent;
            this.btnSearchResetNew.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearchResetNew.BackgroundImage")));
            this.btnSearchResetNew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearchResetNew.FlatAppearance.BorderSize = 0;
            this.btnSearchResetNew.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearchResetNew.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearchResetNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchResetNew.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnSearchResetNew.Location = new System.Drawing.Point(322, 61);
            this.btnSearchResetNew.Name = "btnSearchResetNew";
            this.btnSearchResetNew.Size = new System.Drawing.Size(75, 32);
            this.btnSearchResetNew.TabIndex = 28;
            this.btnSearchResetNew.Text = "Reset";
            this.btnSearchResetNew.UseVisualStyleBackColor = false;
            this.btnSearchResetNew.Click += new System.EventHandler(this.btnSearchReset_Click);
            // 
            // btnSearchNew
            // 
            this.btnSearchNew.BackColor = System.Drawing.Color.Transparent;
            this.btnSearchNew.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearchNew.BackgroundImage")));
            this.btnSearchNew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearchNew.FlatAppearance.BorderSize = 0;
            this.btnSearchNew.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearchNew.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearchNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchNew.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnSearchNew.Location = new System.Drawing.Point(241, 61);
            this.btnSearchNew.Name = "btnSearchNew";
            this.btnSearchNew.Size = new System.Drawing.Size(75, 32);
            this.btnSearchNew.TabIndex = 27;
            this.btnSearchNew.Text = "Search";
            this.btnSearchNew.UseVisualStyleBackColor = false;
            this.btnSearchNew.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnSearchResetNew);
            this.groupBox2.Controls.Add(this.btnSearchNew);
            this.groupBox2.Controls.Add(this.lblDateSearch);
            this.groupBox2.Controls.Add(this.dtpDateSearch);
            this.groupBox2.Controls.Add(this.lblDistributorSearch);
            this.groupBox2.Controls.Add(this.txtDistributorSearch);
            this.groupBox2.Location = new System.Drawing.Point(8, 18);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(403, 112);
            this.groupBox2.TabIndex = 29;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Search";
            // 
            // lblRule
            // 
            this.lblRule.AutoSize = true;
            this.lblRule.Font = new System.Drawing.Font("Verdana", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.lblRule.Location = new System.Drawing.Point(10, 232);
            this.lblRule.Name = "lblRule";
            this.lblRule.Size = new System.Drawing.Size(50, 13);
            this.lblRule.TabIndex = 10;
            this.lblRule.Text = "Rules :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.label2.Location = new System.Drawing.Point(10, 249);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(392, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "* Only one transaction (Payment / Adjustment) is allowed in a day.";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.label4.Location = new System.Drawing.Point(10, 263);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(332, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "* Payment date should be greater than adjustment date.";
            // 
            // frnDistributorTravelFund
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1030, 703);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "frnDistributorTravelFund";
            this.Text = "";
            this.Load += new System.EventHandler(this.frnDistributorTravelFund_Load);
            this.tabPageSearch.ResumeLayout(false);
            this.pnlSearchHeader.ResumeLayout(false);
            this.tabPageCreate.ResumeLayout(false);
            this.pnlCreateDetails.ResumeLayout(false);
            this.pnlCreateDetails.PerformLayout();
            this.pnlDetailsHeader.ResumeLayout(false);
            this.pnlDetailsHeader.PerformLayout();
            this.pnlBottomButtons.ResumeLayout(false);
            this.pnlSearchGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTravelFundDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errTravelFundValidate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTravelFundSearch)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbBulkImport.ResumeLayout(false);
            this.gbBulkImport.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblDistributorID;
        private System.Windows.Forms.TextBox txtDistributorID;
        private System.Windows.Forms.DataGridView dgvTravelFundDetails;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblBalance;
        private System.Windows.Forms.Label lblDistributorName;
        private System.Windows.Forms.Label lblBalanceAmount;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.ErrorProvider errTravelFundValidate;
        private System.Windows.Forms.Label lblDistributorSearch;
        private System.Windows.Forms.TextBox txtDistributorSearch;
        private System.Windows.Forms.Label lblDateSearch;
        private System.Windows.Forms.DateTimePicker dtpDateSearch;
        private System.Windows.Forms.DataGridView dgvTravelFundSearch;
        protected System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.OpenFileDialog ofdExcel;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.DateTimePicker dtpBusinessMonth;
        private System.Windows.Forms.Label lblBeneficiary;
        private System.Windows.Forms.ComboBox cmbBeneficiary;
        private System.Windows.Forms.Label lblRemarks;
        private System.Windows.Forms.TextBox txtRemarks;
        private System.Windows.Forms.Label lblAmount;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        protected System.Windows.Forms.Button btnBulkImport;
        private System.Windows.Forms.Button btnOpenFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtImportExcel;
        private System.Windows.Forms.GroupBox gbBulkImport;
        protected System.Windows.Forms.Button btnAddDet;
        protected System.Windows.Forms.Button btnClearDet;
        protected System.Windows.Forms.Button btnSearchResetNew;
        protected System.Windows.Forms.Button btnSearchNew;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbtnAdjustment;
        private System.Windows.Forms.RadioButton rbtnPayment;
        private System.Windows.Forms.Label lblRule;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
    }
}