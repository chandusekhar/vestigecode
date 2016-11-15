namespace POSClient.UI
{
    partial class frmPickUpCentre
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPickUpCentre));
            this.btnAddPUCAccDep = new System.Windows.Forms.Button();
            this.dgvPUCAccounts = new System.Windows.Forms.DataGridView();
            this.txtPCId = new System.Windows.Forms.TextBox();
            this.lblPCId = new System.Windows.Forms.Label();
            this.lblTranNo = new System.Windows.Forms.Label();
            this.txtTranNo = new System.Windows.Forms.TextBox();
            this.lblDepositAmount = new System.Windows.Forms.Label();
            this.txtDepositAmount = new System.Windows.Forms.TextBox();
            this.cmbPaymentMode = new System.Windows.Forms.ComboBox();
            this.lblPaymentMode = new System.Windows.Forms.Label();
            this.lblDepositDate = new System.Windows.Forms.Label();
            this.dtpDepositDate = new System.Windows.Forms.DateTimePicker();
            this.errorprovPUCValid = new System.Windows.Forms.ErrorProvider(this.components);
            this.cmbLocType = new System.Windows.Forms.ComboBox();
            this.lblLocType = new System.Windows.Forms.Label();
            this.lblLocCode = new System.Windows.Forms.Label();
            this.lblPUC = new System.Windows.Forms.Label();
            this.cmbPUC = new System.Windows.Forms.ComboBox();
            this.cmbLocCode = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSearchDBRecords = new System.Windows.Forms.Button();
            this.btnSaveDBRecord = new System.Windows.Forms.Button();
            this.btnResetUI = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvPUCAccDep = new System.Windows.Forms.DataGridView();
            this.pnlPUCAccDep = new System.Windows.Forms.Panel();
            this.pnlCreateHeader.SuspendLayout();
            this.grpAddDetails.SuspendLayout();
            this.pnlSearchHeader.SuspendLayout();
            this.pnlSearchGrid.SuspendLayout();
            this.pnlLowerButtons.SuspendLayout();
            this.pnlTopButtons.SuspendLayout();
            this.pnlSearchButtons.SuspendLayout();
            this.tabSearch.SuspendLayout();
            this.tabCreate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPUCAccounts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorprovPUCValid)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPUCAccDep)).BeginInit();
            this.pnlPUCAccDep.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCreateReset
            // 
            this.btnCreateReset.FlatAppearance.BorderSize = 0;
            this.btnCreateReset.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnCreateReset.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            // 
            // btnSave
            // 
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            // 
            // grpAddDetails
            // 
            this.grpAddDetails.Size = new System.Drawing.Size(1005, 328);
            // 
            // btnClearDetails
            // 
            this.btnClearDetails.FlatAppearance.BorderSize = 0;
            this.btnClearDetails.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnClearDetails.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            // 
            // btnAddDetails
            // 
            this.btnAddDetails.FlatAppearance.BorderSize = 0;
            this.btnAddDetails.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnAddDetails.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            // 
            // pnlSearchHeader
            // 
            this.pnlSearchHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSearchHeader.Controls.Add(this.cmbLocType);
            this.pnlSearchHeader.Controls.Add(this.lblLocType);
            this.pnlSearchHeader.Controls.Add(this.lblLocCode);
            this.pnlSearchHeader.Controls.Add(this.lblPUC);
            this.pnlSearchHeader.Controls.Add(this.cmbPUC);
            this.pnlSearchHeader.Controls.Add(this.cmbLocCode);
            this.pnlSearchHeader.Controls.Add(this.dtpDepositDate);
            this.pnlSearchHeader.Controls.Add(this.lblDepositDate);
            this.pnlSearchHeader.Controls.Add(this.lblPaymentMode);
            this.pnlSearchHeader.Controls.Add(this.cmbPaymentMode);
            this.pnlSearchHeader.Controls.Add(this.lblDepositAmount);
            this.pnlSearchHeader.Controls.Add(this.txtDepositAmount);
            this.pnlSearchHeader.Controls.Add(this.lblTranNo);
            this.pnlSearchHeader.Controls.Add(this.txtTranNo);
            this.pnlSearchHeader.Controls.Add(this.lblPCId);
            this.pnlSearchHeader.Controls.Add(this.txtPCId);
            this.pnlSearchHeader.Size = new System.Drawing.Size(1005, 134);
            this.pnlSearchHeader.TabIndex = 99;
            this.pnlSearchHeader.Controls.SetChildIndex(this.pnlSearchButtons, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.txtPCId, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblPCId, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.txtTranNo, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblTranNo, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.txtDepositAmount, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblDepositAmount, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.cmbPaymentMode, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblPaymentMode, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblDepositDate, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.dtpDepositDate, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.cmbLocCode, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.cmbPUC, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblPUC, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblLocCode, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblLocType, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.cmbLocType, 0);
            // 
            // btnSearch
            // 
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearch.Location = new System.Drawing.Point(778, 0);
            this.btnSearch.Visible = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnSearchReset
            // 
            this.btnSearchReset.FlatAppearance.BorderSize = 0;
            this.btnSearchReset.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearchReset.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearchReset.Location = new System.Drawing.Point(928, 0);
            this.btnSearchReset.TabIndex = 2;
            this.btnSearchReset.Text = "C&lear";
            this.btnSearchReset.Click += new System.EventHandler(this.btnSearchReset_Click);
            // 
            // lblSearchResult
            // 
            this.lblSearchResult.Text = "PUC-Account Deposits";
            // 
            // pnlSearchGrid
            // 
            this.pnlSearchGrid.Controls.Add(this.pnlPUCAccDep);
            this.pnlSearchGrid.Controls.Add(this.panel2);
            this.pnlSearchGrid.Controls.Add(this.panel1);
            this.pnlSearchGrid.Controls.Add(this.dgvPUCAccounts);
            this.pnlSearchGrid.Location = new System.Drawing.Point(0, 158);
            this.pnlSearchGrid.Padding = new System.Windows.Forms.Padding(5);
            this.pnlSearchGrid.Size = new System.Drawing.Size(1005, 449);
            // 
            // pnlLowerButtons
            // 
            this.pnlLowerButtons.Location = new System.Drawing.Point(0, 296);
            // 
            // pnlSearchButtons
            // 
            this.pnlSearchButtons.Controls.Add(this.btnAddPUCAccDep);
            this.pnlSearchButtons.Location = new System.Drawing.Point(0, 100);
            this.pnlSearchButtons.Size = new System.Drawing.Size(1003, 32);
            this.pnlSearchButtons.TabIndex = 7;
            this.pnlSearchButtons.Controls.SetChildIndex(this.btnSearchReset, 0);
            this.pnlSearchButtons.Controls.SetChildIndex(this.btnAddPUCAccDep, 0);
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
            // btnAddPUCAccDep
            // 
            this.btnAddPUCAccDep.BackgroundImage = global::POSClient.Properties.Resources.button;
            this.btnAddPUCAccDep.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAddPUCAccDep.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnAddPUCAccDep.FlatAppearance.BorderSize = 0;
            this.btnAddPUCAccDep.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnAddPUCAccDep.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnAddPUCAccDep.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddPUCAccDep.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnAddPUCAccDep.Location = new System.Drawing.Point(853, 0);
            this.btnAddPUCAccDep.Name = "btnAddPUCAccDep";
            this.btnAddPUCAccDep.Size = new System.Drawing.Size(75, 32);
            this.btnAddPUCAccDep.TabIndex = 1;
            this.btnAddPUCAccDep.Text = "&Add";
            this.btnAddPUCAccDep.UseVisualStyleBackColor = true;
            this.btnAddPUCAccDep.Click += new System.EventHandler(this.btnAddPUCAccDep_Click);
            // 
            // dgvPUCAccounts
            // 
            this.dgvPUCAccounts.AllowUserToAddRows = false;
            this.dgvPUCAccounts.AllowUserToDeleteRows = false;
            this.dgvPUCAccounts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPUCAccounts.Location = new System.Drawing.Point(5, 211);
            this.dgvPUCAccounts.MultiSelect = false;
            this.dgvPUCAccounts.Name = "dgvPUCAccounts";
            this.dgvPUCAccounts.RowHeadersVisible = false;
            this.dgvPUCAccounts.Size = new System.Drawing.Size(995, 203);
            this.dgvPUCAccounts.TabIndex = 0;
            this.dgvPUCAccounts.TabStop = false;
            this.dgvPUCAccounts.SelectionChanged += new System.EventHandler(this.dgvPUCAccounts_SelectionChanged);
            // 
            // txtPCId
            // 
            this.txtPCId.Location = new System.Drawing.Point(901, 73);
            this.txtPCId.Name = "txtPCId";
            this.txtPCId.Size = new System.Drawing.Size(100, 21);
            this.txtPCId.TabIndex = 1;
            this.txtPCId.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPCId.Visible = false;
            // 
            // lblPCId
            // 
            this.lblPCId.AutoSize = true;
            this.lblPCId.Location = new System.Drawing.Point(898, 54);
            this.lblPCId.Name = "lblPCId";
            this.lblPCId.Size = new System.Drawing.Size(94, 13);
            this.lblPCId.TabIndex = 4;
            this.lblPCId.Text = "PUC Location:*";
            this.lblPCId.Visible = false;
            // 
            // lblTranNo
            // 
            this.lblTranNo.AutoSize = true;
            this.lblTranNo.Location = new System.Drawing.Point(585, 22);
            this.lblTranNo.Name = "lblTranNo";
            this.lblTranNo.Size = new System.Drawing.Size(108, 13);
            this.lblTranNo.TabIndex = 6;
            this.lblTranNo.Text = "Transaction No.:*";
            // 
            // txtTranNo
            // 
            this.txtTranNo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtTranNo.Location = new System.Drawing.Point(699, 19);
            this.txtTranNo.Name = "txtTranNo";
            this.txtTranNo.Size = new System.Drawing.Size(150, 21);
            this.txtTranNo.TabIndex = 3;
            // 
            // lblDepositAmount
            // 
            this.lblDepositAmount.AutoSize = true;
            this.lblDepositAmount.Location = new System.Drawing.Point(630, 58);
            this.lblDepositAmount.Name = "lblDepositAmount";
            this.lblDepositAmount.Size = new System.Drawing.Size(63, 13);
            this.lblDepositAmount.TabIndex = 8;
            this.lblDepositAmount.Text = "Amount:*";
            // 
            // txtDepositAmount
            // 
            this.txtDepositAmount.Location = new System.Drawing.Point(699, 55);
            this.txtDepositAmount.Name = "txtDepositAmount";
            this.txtDepositAmount.Size = new System.Drawing.Size(150, 21);
            this.txtDepositAmount.TabIndex = 6;
            this.txtDepositAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // cmbPaymentMode
            // 
            this.cmbPaymentMode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.cmbPaymentMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPaymentMode.FormattingEnabled = true;
            this.cmbPaymentMode.Location = new System.Drawing.Point(400, 55);
            this.cmbPaymentMode.Name = "cmbPaymentMode";
            this.cmbPaymentMode.Size = new System.Drawing.Size(178, 21);
            this.cmbPaymentMode.TabIndex = 5;
            // 
            // lblPaymentMode
            // 
            this.lblPaymentMode.AutoSize = true;
            this.lblPaymentMode.Location = new System.Drawing.Point(289, 58);
            this.lblPaymentMode.Name = "lblPaymentMode";
            this.lblPaymentMode.Size = new System.Drawing.Size(103, 13);
            this.lblPaymentMode.TabIndex = 10;
            this.lblPaymentMode.Text = "Payment Mode:*";
            // 
            // lblDepositDate
            // 
            this.lblDepositDate.AutoSize = true;
            this.lblDepositDate.Location = new System.Drawing.Point(64, 59);
            this.lblDepositDate.Name = "lblDepositDate";
            this.lblDepositDate.Size = new System.Drawing.Size(46, 13);
            this.lblDepositDate.TabIndex = 11;
            this.lblDepositDate.Text = "Date:*";
            // 
            // dtpDepositDate
            // 
            this.dtpDepositDate.Checked = false;
            this.dtpDepositDate.CustomFormat = "dd/MM/yyyy";
            this.dtpDepositDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDepositDate.Location = new System.Drawing.Point(116, 54);
            this.dtpDepositDate.Name = "dtpDepositDate";
            this.dtpDepositDate.ShowCheckBox = true;
            this.dtpDepositDate.Size = new System.Drawing.Size(114, 21);
            this.dtpDepositDate.TabIndex = 4;
            this.dtpDepositDate.Value = new System.DateTime(2009, 12, 4, 0, 0, 0, 0);
            // 
            // errorprovPUCValid
            // 
            this.errorprovPUCValid.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorprovPUCValid.ContainerControl = this;
            // 
            // cmbLocType
            // 
            this.cmbLocType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocType.FormattingEnabled = true;
            this.cmbLocType.Location = new System.Drawing.Point(902, 19);
            this.cmbLocType.Name = "cmbLocType";
            this.cmbLocType.Size = new System.Drawing.Size(98, 21);
            this.cmbLocType.TabIndex = 0;
            this.cmbLocType.Visible = false;
            this.cmbLocType.SelectedIndexChanged += new System.EventHandler(this.cmbLocType_SelectedIndexChanged);
            // 
            // lblLocType
            // 
            this.lblLocType.AutoSize = true;
            this.lblLocType.Location = new System.Drawing.Point(898, 3);
            this.lblLocType.Name = "lblLocType";
            this.lblLocType.Size = new System.Drawing.Size(91, 13);
            this.lblLocType.TabIndex = 14;
            this.lblLocType.Text = "Location Type:";
            this.lblLocType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblLocType.Visible = false;
            // 
            // lblLocCode
            // 
            this.lblLocCode.AutoSize = true;
            this.lblLocCode.Location = new System.Drawing.Point(9, 23);
            this.lblLocCode.Name = "lblLocCode";
            this.lblLocCode.Size = new System.Drawing.Size(100, 13);
            this.lblLocCode.TabIndex = 16;
            this.lblLocCode.Text = "Location Code:*";
            this.lblLocCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPUC
            // 
            this.lblPUC.AutoSize = true;
            this.lblPUC.Location = new System.Drawing.Point(289, 22);
            this.lblPUC.Name = "lblPUC";
            this.lblPUC.Size = new System.Drawing.Size(105, 13);
            this.lblPUC.TabIndex = 17;
            this.lblPUC.Text = "Pick-Up Centre:*";
            this.lblPUC.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbPUC
            // 
            this.cmbPUC.AllowDrop = true;
            this.cmbPUC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.cmbPUC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPUC.DropDownWidth = 121;
            this.cmbPUC.FormattingEnabled = true;
            this.cmbPUC.Location = new System.Drawing.Point(400, 19);
            this.cmbPUC.Name = "cmbPUC";
            this.cmbPUC.Size = new System.Drawing.Size(177, 21);
            this.cmbPUC.TabIndex = 2;
            // 
            // cmbLocCode
            // 
            this.cmbLocCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.cmbLocCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocCode.Enabled = false;
            this.cmbLocCode.FormattingEnabled = true;
            this.cmbLocCode.Location = new System.Drawing.Point(116, 19);
            this.cmbLocCode.Name = "cmbLocCode";
            this.cmbLocCode.Size = new System.Drawing.Size(162, 21);
            this.cmbLocCode.TabIndex = 1;
            this.cmbLocCode.SelectedIndexChanged += new System.EventHandler(this.cmbLocCode_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSearchDBRecords);
            this.panel1.Controls.Add(this.btnSaveDBRecord);
            this.panel1.Controls.Add(this.btnResetUI);
            this.panel1.Location = new System.Drawing.Point(1, 416);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1003, 32);
            this.panel1.TabIndex = 1;
            // 
            // btnSearchDBRecords
            // 
            this.btnSearchDBRecords.BackColor = System.Drawing.Color.Transparent;
            this.btnSearchDBRecords.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearchDBRecords.BackgroundImage")));
            this.btnSearchDBRecords.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearchDBRecords.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSearchDBRecords.FlatAppearance.BorderSize = 0;
            this.btnSearchDBRecords.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearchDBRecords.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearchDBRecords.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchDBRecords.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnSearchDBRecords.Location = new System.Drawing.Point(778, 0);
            this.btnSearchDBRecords.Name = "btnSearchDBRecords";
            this.btnSearchDBRecords.Size = new System.Drawing.Size(75, 32);
            this.btnSearchDBRecords.TabIndex = 2;
            this.btnSearchDBRecords.Text = "S&earch";
            this.btnSearchDBRecords.UseVisualStyleBackColor = false;
            this.btnSearchDBRecords.Click += new System.EventHandler(this.btnSearchDBRecords_Click);
            // 
            // btnSaveDBRecord
            // 
            this.btnSaveDBRecord.BackColor = System.Drawing.Color.Transparent;
            this.btnSaveDBRecord.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSaveDBRecord.BackgroundImage")));
            this.btnSaveDBRecord.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSaveDBRecord.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSaveDBRecord.FlatAppearance.BorderSize = 0;
            this.btnSaveDBRecord.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSaveDBRecord.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSaveDBRecord.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveDBRecord.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnSaveDBRecord.Location = new System.Drawing.Point(853, 0);
            this.btnSaveDBRecord.Name = "btnSaveDBRecord";
            this.btnSaveDBRecord.Size = new System.Drawing.Size(75, 32);
            this.btnSaveDBRecord.TabIndex = 1;
            this.btnSaveDBRecord.Text = "&Save";
            this.btnSaveDBRecord.UseVisualStyleBackColor = false;
            this.btnSaveDBRecord.Click += new System.EventHandler(this.btnSaveDBRecord_Click);
            // 
            // btnResetUI
            // 
            this.btnResetUI.BackColor = System.Drawing.Color.Transparent;
            this.btnResetUI.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnResetUI.BackgroundImage")));
            this.btnResetUI.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnResetUI.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnResetUI.FlatAppearance.BorderSize = 0;
            this.btnResetUI.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnResetUI.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnResetUI.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnResetUI.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnResetUI.Location = new System.Drawing.Point(928, 0);
            this.btnResetUI.Name = "btnResetUI";
            this.btnResetUI.Size = new System.Drawing.Size(75, 32);
            this.btnResetUI.TabIndex = 0;
            this.btnResetUI.Text = "&Reset";
            this.btnResetUI.UseVisualStyleBackColor = false;
            this.btnResetUI.Click += new System.EventHandler(this.btnResetUI_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(1, 184);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1003, 24);
            this.panel2.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(117)))), ((int)(((byte)(186)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1003, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "PUC Accounts";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dgvPUCAccDep
            // 
            this.dgvPUCAccDep.AllowUserToAddRows = false;
            this.dgvPUCAccDep.AllowUserToDeleteRows = false;
            this.dgvPUCAccDep.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPUCAccDep.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPUCAccDep.Location = new System.Drawing.Point(5, 5);
            this.dgvPUCAccDep.MultiSelect = false;
            this.dgvPUCAccDep.Name = "dgvPUCAccDep";
            this.dgvPUCAccDep.RowHeadersVisible = false;
            this.dgvPUCAccDep.Size = new System.Drawing.Size(993, 167);
            this.dgvPUCAccDep.TabIndex = 3;
            this.dgvPUCAccDep.TabStop = false;
            this.dgvPUCAccDep.SelectionChanged += new System.EventHandler(this.dgvPUCAccDep_SelectionChanged);
            this.dgvPUCAccDep.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPUCAccDep_CellContentClick);
            // 
            // pnlPUCAccDep
            // 
            this.pnlPUCAccDep.Controls.Add(this.dgvPUCAccDep);
            this.pnlPUCAccDep.Location = new System.Drawing.Point(1, 4);
            this.pnlPUCAccDep.Name = "pnlPUCAccDep";
            this.pnlPUCAccDep.Padding = new System.Windows.Forms.Padding(5);
            this.pnlPUCAccDep.Size = new System.Drawing.Size(1003, 177);
            this.pnlPUCAccDep.TabIndex = 4;
            // 
            // frmPickUpCentre
            // 
            this.ClientSize = new System.Drawing.Size(1013, 703);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmPickUpCentre";
            this.ShowInTaskbar = false;
            this.Text = "PUC Deposit";
            this.WindowState = System.Windows.Forms.FormWindowState.Normal;
            this.Load += new System.EventHandler(this.frmPickUpCentre_Load);
            this.pnlCreateHeader.ResumeLayout(false);
            this.grpAddDetails.ResumeLayout(false);
            this.pnlSearchHeader.ResumeLayout(false);
            this.pnlSearchHeader.PerformLayout();
            this.pnlSearchGrid.ResumeLayout(false);
            this.pnlLowerButtons.ResumeLayout(false);
            this.pnlTopButtons.ResumeLayout(false);
            this.pnlSearchButtons.ResumeLayout(false);
            this.tabSearch.ResumeLayout(false);
            this.tabCreate.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPUCAccounts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorprovPUCValid)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPUCAccDep)).EndInit();
            this.pnlPUCAccDep.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAddPUCAccDep;
        private System.Windows.Forms.DataGridView dgvPUCAccounts;
        private System.Windows.Forms.Label lblPCId;
        private System.Windows.Forms.TextBox txtPCId;
        private System.Windows.Forms.Label lblDepositAmount;
        private System.Windows.Forms.TextBox txtDepositAmount;
        private System.Windows.Forms.Label lblTranNo;
        private System.Windows.Forms.TextBox txtTranNo;
        private System.Windows.Forms.Label lblPaymentMode;
        private System.Windows.Forms.ComboBox cmbPaymentMode;
        private System.Windows.Forms.DateTimePicker dtpDepositDate;
        private System.Windows.Forms.Label lblDepositDate;
        private System.Windows.Forms.ErrorProvider errorprovPUCValid;
        private System.Windows.Forms.ComboBox cmbLocType;
        private System.Windows.Forms.Label lblLocType;
        private System.Windows.Forms.Label lblLocCode;
        private System.Windows.Forms.Label lblPUC;
        private System.Windows.Forms.ComboBox cmbPUC;
        private System.Windows.Forms.ComboBox cmbLocCode;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvPUCAccDep;
        private System.Windows.Forms.Button btnSearchDBRecords;
        private System.Windows.Forms.Button btnSaveDBRecord;
        private System.Windows.Forms.Button btnResetUI;
        private System.Windows.Forms.Panel pnlPUCAccDep;
    }
}
