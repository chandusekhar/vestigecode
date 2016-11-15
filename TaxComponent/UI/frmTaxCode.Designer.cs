namespace TaxComponent.UI
{
    partial class frmTaxCode
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTaxCode));
            this.lblCode = new System.Windows.Forms.Label();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.lblPercentage = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.txtTaxCode = new System.Windows.Forms.TextBox();
            this.dtpStartDateTaxCode = new System.Windows.Forms.DateTimePicker();
            this.txtPercentageTaxCode = new System.Windows.Forms.TextBox();
            this.txtDescriptionTaxCode = new System.Windows.Forms.TextBox();
            this.cmbStatusTaxCode = new System.Windows.Forms.ComboBox();
            this.btnSaveTaxCode = new System.Windows.Forms.Button();
            this.errProviderTaxCode = new System.Windows.Forms.ErrorProvider(this.components);
            this.cmbstatusTaxGroup = new System.Windows.Forms.ComboBox();
            this.cmbTaxGroupAppliedOn = new System.Windows.Forms.ComboBox();
            this.dtpStartDateTaxGroup = new System.Windows.Forms.DateTimePicker();
            this.txtTaxGroupCode = new System.Windows.Forms.TextBox();
            this.lblStatusTaxGroup = new System.Windows.Forms.Label();
            this.lblStartDateTaxGroup = new System.Windows.Forms.Label();
            this.lblAppliedOn = new System.Windows.Forms.Label();
            this.lblTaxGroupCode = new System.Windows.Forms.Label();
            this.dgvTaxGroupSearch = new System.Windows.Forms.DataGridView();
            this.dgvTaxCodeSearch = new System.Windows.Forms.DataGridView();
            this.errorProviderTaxGroup = new System.Windows.Forms.ErrorProvider(this.components);
            this.dgvTaxGroupDetail = new System.Windows.Forms.DataGridView();
            this.btnResetTGDetail = new System.Windows.Forms.Button();
            this.btnAddTGDetail = new System.Windows.Forms.Button();
            this.cmbTaxCodes = new System.Windows.Forms.ComboBox();
            this.txtGroupOrder = new System.Windows.Forms.TextBox();
            this.lblGroupOrder = new System.Windows.Forms.Label();
            this.lblTaxCode = new System.Windows.Forms.Label();
            this.lblSearchDetails = new System.Windows.Forms.Label();
            this.pnlbuttons = new System.Windows.Forms.Panel();
            this.btnSearchTaxGroup = new System.Windows.Forms.Button();
            this.btnResetTaxGroup = new System.Windows.Forms.Button();
            this.btnSaveTaxGroup = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlCreateHeader.SuspendLayout();
            this.grpAddDetails.SuspendLayout();
            this.pnlSearchHeader.SuspendLayout();
            this.pnlSearchGrid.SuspendLayout();
            this.pnlCreateDetail.SuspendLayout();
            this.pnlTopButtons.SuspendLayout();
            this.pnlSearchButtons.SuspendLayout();
            this.tabSearch.SuspendLayout();
            this.tabCreate.SuspendLayout();
            this.tabControlTransaction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errProviderTaxCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTaxGroupSearch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTaxCodeSearch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderTaxGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTaxGroupDetail)).BeginInit();
            this.pnlbuttons.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCreateHeader
            // 
            this.pnlCreateHeader.Controls.Add(this.txtTaxGroupCode);
            this.pnlCreateHeader.Controls.Add(this.cmbTaxGroupAppliedOn);
            this.pnlCreateHeader.Controls.Add(this.lblTaxGroupCode);
            this.pnlCreateHeader.Controls.Add(this.cmbstatusTaxGroup);
            this.pnlCreateHeader.Controls.Add(this.lblAppliedOn);
            this.pnlCreateHeader.Controls.Add(this.dtpStartDateTaxGroup);
            this.pnlCreateHeader.Controls.Add(this.lblStatusTaxGroup);
            this.pnlCreateHeader.Controls.Add(this.lblStartDateTaxGroup);
            this.pnlCreateHeader.Size = new System.Drawing.Size(1005, 60);
            this.pnlCreateHeader.TabIndex = 0;
            this.pnlCreateHeader.Controls.SetChildIndex(this.pnlTopButtons, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblStartDateTaxGroup, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblStatusTaxGroup, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.dtpStartDateTaxGroup, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblAppliedOn, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.cmbstatusTaxGroup, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblTaxGroupCode, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.cmbTaxGroupAppliedOn, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.txtTaxGroupCode, 0);
            // 
            // btnCreateReset
            // 
            this.btnCreateReset.Enabled = false;
            this.btnCreateReset.FlatAppearance.BorderSize = 0;
            this.btnCreateReset.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnCreateReset.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnCreateReset.Location = new System.Drawing.Point(928, 0);
            this.btnCreateReset.TabIndex = 8;
            this.btnCreateReset.Visible = false;

            this.tabControlTransaction.SelectedIndexChanged += new System.EventHandler(this.tabControlTransaction_SelectedIndexChanged);
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSave.Location = new System.Drawing.Point(853, 0);
            this.btnSave.TabIndex = 7;
            this.btnSave.Visible = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblAddDetails
            // 
            this.lblAddDetails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(117)))), ((int)(((byte)(186)))));
            this.lblAddDetails.Dock = System.Windows.Forms.DockStyle.None;
            this.lblAddDetails.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAddDetails.ForeColor = System.Drawing.Color.White;
            this.lblAddDetails.Location = new System.Drawing.Point(1, 0);
            this.lblAddDetails.Text = "TaxCodes";
            // 
            // grpAddDetails
            // 
            this.grpAddDetails.Controls.Add(this.panel1);
            this.grpAddDetails.Controls.Add(this.lblSearchDetails);
            this.grpAddDetails.Controls.Add(this.pnlbuttons);
            this.grpAddDetails.Location = new System.Drawing.Point(0, 84);
            this.grpAddDetails.Size = new System.Drawing.Size(1005, 527);
            this.grpAddDetails.TabIndex = 1;
            this.grpAddDetails.Controls.SetChildIndex(this.pnlCreateDetail, 0);
            this.grpAddDetails.Controls.SetChildIndex(this.pnlLowerButtons, 0);
            this.grpAddDetails.Controls.SetChildIndex(this.pnlbuttons, 0);
            this.grpAddDetails.Controls.SetChildIndex(this.lblSearchDetails, 0);
            this.grpAddDetails.Controls.SetChildIndex(this.panel1, 0);
            // 
            // btnClearDetails
            // 
            this.btnClearDetails.Dock = System.Windows.Forms.DockStyle.None;
            this.btnClearDetails.Enabled = false;
            this.btnClearDetails.FlatAppearance.BorderSize = 0;
            this.btnClearDetails.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnClearDetails.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnClearDetails.Location = new System.Drawing.Point(687, 1);
            this.btnClearDetails.TabIndex = 11;
            this.btnClearDetails.TabStop = false;
            this.btnClearDetails.Visible = false;
            // 
            // btnAddDetails
            // 
            this.btnAddDetails.Enabled = false;
            this.btnAddDetails.FlatAppearance.BorderSize = 0;
            this.btnAddDetails.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnAddDetails.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnAddDetails.Location = new System.Drawing.Point(773, 0);
            this.btnAddDetails.TabStop = false;
            this.btnAddDetails.Visible = false;
            // 
            // pnlSearchHeader
            // 
            this.pnlSearchHeader.Controls.Add(this.lblCode);
            this.pnlSearchHeader.Controls.Add(this.txtPercentageTaxCode);
            this.pnlSearchHeader.Controls.Add(this.lblStartDate);
            this.pnlSearchHeader.Controls.Add(this.cmbStatusTaxCode);
            this.pnlSearchHeader.Controls.Add(this.lblDescription);
            this.pnlSearchHeader.Controls.Add(this.lblPercentage);
            this.pnlSearchHeader.Controls.Add(this.lblStatus);
            this.pnlSearchHeader.Controls.Add(this.txtDescriptionTaxCode);
            this.pnlSearchHeader.Controls.Add(this.txtTaxCode);
            this.pnlSearchHeader.Controls.Add(this.dtpStartDateTaxCode);
            this.pnlSearchHeader.Size = new System.Drawing.Size(1005, 120);
            this.pnlSearchHeader.Controls.SetChildIndex(this.dtpStartDateTaxCode, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.txtTaxCode, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.txtDescriptionTaxCode, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblStatus, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblPercentage, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblDescription, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.cmbStatusTaxCode, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblStartDate, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.txtPercentageTaxCode, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblCode, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.pnlSearchButtons, 0);
            // 
            // btnSearch
            // 
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearch.Location = new System.Drawing.Point(780, 0);
            this.btnSearch.TabIndex = 6;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnSearchReset
            // 
            this.btnSearchReset.FlatAppearance.BorderSize = 0;
            this.btnSearchReset.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearchReset.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearchReset.TabIndex = 8;
            this.btnSearchReset.Click += new System.EventHandler(this.btnSearchReset_Click);
            // 
            // pnlSearchGrid
            // 
            this.pnlSearchGrid.Controls.Add(this.dgvTaxCodeSearch);
            this.pnlSearchGrid.Location = new System.Drawing.Point(0, 144);
            this.pnlSearchGrid.Size = new System.Drawing.Size(1005, 467);
            // 
            // pnlCreateDetail
            // 
            this.pnlCreateDetail.Controls.Add(this.dgvTaxGroupSearch);
            this.pnlCreateDetail.Dock = System.Windows.Forms.DockStyle.None;
            this.pnlCreateDetail.Location = new System.Drawing.Point(0, 274);
            this.pnlCreateDetail.Size = new System.Drawing.Size(1005, 250);
            // 
            // pnlLowerButtons
            // 
            this.pnlLowerButtons.Location = new System.Drawing.Point(0, 527);
            this.pnlLowerButtons.Size = new System.Drawing.Size(1005, 0);
            this.pnlLowerButtons.Visible = false;
            // 
            // pnlTopButtons
            // 
            this.pnlTopButtons.Controls.Add(this.btnSave);
            this.pnlTopButtons.Controls.Add(this.btnCreateReset);
            this.pnlTopButtons.Location = new System.Drawing.Point(0, 26);
            // 
            // pnlSearchButtons
            // 
            this.pnlSearchButtons.Controls.Add(this.btnSaveTaxCode);
            this.pnlSearchButtons.Location = new System.Drawing.Point(0, 88);
            this.pnlSearchButtons.Controls.SetChildIndex(this.btnSearchReset, 0);
            this.pnlSearchButtons.Controls.SetChildIndex(this.btnSaveTaxCode, 0);
            this.pnlSearchButtons.Controls.SetChildIndex(this.btnSearch, 0);
            // 
            // tabSearch
            // 
            this.tabSearch.Text = "Tax Code";
            // 
            // tabCreate
            // 
            this.tabCreate.Text = "Tax Group";
            // 
            // lblCode
            // 
            this.lblCode.AutoSize = true;
            this.lblCode.Location = new System.Drawing.Point(44, 27);
            this.lblCode.Name = "lblCode";
            this.lblCode.Size = new System.Drawing.Size(74, 13);
            this.lblCode.TabIndex = 2;
            this.lblCode.Text = "Tax Code:*";
            this.lblCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblStartDate
            // 
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.Location = new System.Drawing.Point(389, 27);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(78, 13);
            this.lblStartDate.TabIndex = 3;
            this.lblStartDate.Text = "Start Date:*";
            this.lblStartDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPercentage
            // 
            this.lblPercentage.AutoSize = true;
            this.lblPercentage.Location = new System.Drawing.Point(658, 27);
            this.lblPercentage.Name = "lblPercentage";
            this.lblPercentage.Size = new System.Drawing.Size(83, 13);
            this.lblPercentage.TabIndex = 4;
            this.lblPercentage.Text = "Percentage:*";
            this.lblPercentage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(36, 54);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(76, 13);
            this.lblDescription.TabIndex = 5;
            this.lblDescription.Text = "Description:";
            this.lblDescription.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(412, 55);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(55, 13);
            this.lblStatus.TabIndex = 7;
            this.lblStatus.Text = "Status:*";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTaxCode
            // 
            this.txtTaxCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtTaxCode.Location = new System.Drawing.Point(122, 24);
            this.txtTaxCode.MaxLength = 20;
            this.txtTaxCode.Name = "txtTaxCode";
            this.txtTaxCode.Size = new System.Drawing.Size(152, 21);
            this.txtTaxCode.TabIndex = 0;
            this.txtTaxCode.Validated += new System.EventHandler(this.txtTaxCode_Validated);
            // 
            // dtpStartDateTaxCode
            // 
            this.dtpStartDateTaxCode.Checked = false;
            this.dtpStartDateTaxCode.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartDateTaxCode.Location = new System.Drawing.Point(473, 23);
            this.dtpStartDateTaxCode.Name = "dtpStartDateTaxCode";
            this.dtpStartDateTaxCode.ShowCheckBox = true;
            this.dtpStartDateTaxCode.Size = new System.Drawing.Size(117, 21);
            this.dtpStartDateTaxCode.TabIndex = 1;
            this.dtpStartDateTaxCode.Validated += new System.EventHandler(this.dtpStartDateTaxCode_Validated);
            // 
            // txtPercentageTaxCode
            // 
            this.txtPercentageTaxCode.Location = new System.Drawing.Point(742, 24);
            this.txtPercentageTaxCode.MaxLength = 5;
            this.txtPercentageTaxCode.Name = "txtPercentageTaxCode";
            this.txtPercentageTaxCode.Size = new System.Drawing.Size(121, 21);
            this.txtPercentageTaxCode.TabIndex = 2;
            this.txtPercentageTaxCode.Validated += new System.EventHandler(this.txtPercentageTaxCode_Validated);
            // 
            // txtDescriptionTaxCode
            // 
            this.txtDescriptionTaxCode.Location = new System.Drawing.Point(122, 52);
            this.txtDescriptionTaxCode.MaxLength = 100;
            this.txtDescriptionTaxCode.Name = "txtDescriptionTaxCode";
            this.txtDescriptionTaxCode.Size = new System.Drawing.Size(151, 21);
            this.txtDescriptionTaxCode.TabIndex = 3;
            // 
            // cmbStatusTaxCode
            // 
            this.cmbStatusTaxCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.cmbStatusTaxCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatusTaxCode.FormattingEnabled = true;
            this.cmbStatusTaxCode.Location = new System.Drawing.Point(473, 52);
            this.cmbStatusTaxCode.Name = "cmbStatusTaxCode";
            this.cmbStatusTaxCode.Size = new System.Drawing.Size(117, 21);
            this.cmbStatusTaxCode.TabIndex = 5;
            this.cmbStatusTaxCode.SelectedIndexChanged += new System.EventHandler(this.cmbStatusTaxCode_SelectedIndexChanged);
            // 
            // btnSaveTaxCode
            // 
            this.btnSaveTaxCode.BackColor = System.Drawing.Color.Transparent;
            this.btnSaveTaxCode.BackgroundImage = global::TaxComponent.Properties.Resources.button;
            this.btnSaveTaxCode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSaveTaxCode.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSaveTaxCode.FlatAppearance.BorderSize = 0;
            this.btnSaveTaxCode.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSaveTaxCode.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSaveTaxCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveTaxCode.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnSaveTaxCode.Location = new System.Drawing.Point(855, 0);
            this.btnSaveTaxCode.Name = "btnSaveTaxCode";
            this.btnSaveTaxCode.Size = new System.Drawing.Size(75, 32);
            this.btnSaveTaxCode.TabIndex = 7;
            this.btnSaveTaxCode.Text = "&Save";
            this.btnSaveTaxCode.UseVisualStyleBackColor = false;
            this.btnSaveTaxCode.Click += new System.EventHandler(this.btnSaveTaxCode_Click);
            // 
            // errProviderTaxCode
            // 
            this.errProviderTaxCode.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errProviderTaxCode.ContainerControl = this;
            // 
            // cmbstatusTaxGroup
            // 
            this.cmbstatusTaxGroup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.cmbstatusTaxGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbstatusTaxGroup.FormattingEnabled = true;
            this.cmbstatusTaxGroup.Location = new System.Drawing.Point(869, 20);
            this.cmbstatusTaxGroup.Name = "cmbstatusTaxGroup";
            this.cmbstatusTaxGroup.Size = new System.Drawing.Size(105, 21);
            this.cmbstatusTaxGroup.TabIndex = 3;
            this.cmbstatusTaxGroup.SelectedIndexChanged += new System.EventHandler(this.cmbstatusTaxGroup_SelectedIndexChanged);
            // 
            // cmbTaxGroupAppliedOn
            // 
            this.cmbTaxGroupAppliedOn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.cmbTaxGroupAppliedOn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTaxGroupAppliedOn.FormattingEnabled = true;
            this.cmbTaxGroupAppliedOn.Location = new System.Drawing.Point(394, 20);
            this.cmbTaxGroupAppliedOn.Name = "cmbTaxGroupAppliedOn";
            this.cmbTaxGroupAppliedOn.Size = new System.Drawing.Size(152, 21);
            this.cmbTaxGroupAppliedOn.TabIndex = 1;
            this.cmbTaxGroupAppliedOn.SelectedIndexChanged += new System.EventHandler(this.cmbTaxGroupAppliedOn_SelectedIndexChanged);
            // 
            // dtpStartDateTaxGroup
            // 
            this.dtpStartDateTaxGroup.Checked = false;
            this.dtpStartDateTaxGroup.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartDateTaxGroup.Location = new System.Drawing.Point(654, 19);
            this.dtpStartDateTaxGroup.Name = "dtpStartDateTaxGroup";
            this.dtpStartDateTaxGroup.ShowCheckBox = true;
            this.dtpStartDateTaxGroup.Size = new System.Drawing.Size(112, 21);
            this.dtpStartDateTaxGroup.TabIndex = 2;
            this.dtpStartDateTaxGroup.Validated += new System.EventHandler(this.dtpStartDateTaxGroup_Validated);
            // 
            // txtTaxGroupCode
            // 
            this.txtTaxGroupCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtTaxGroupCode.Location = new System.Drawing.Point(132, 20);
            this.txtTaxGroupCode.MaxLength = 20;
            this.txtTaxGroupCode.Name = "txtTaxGroupCode";
            this.txtTaxGroupCode.Size = new System.Drawing.Size(152, 21);
            this.txtTaxGroupCode.TabIndex = 0;
            this.txtTaxGroupCode.Validated += new System.EventHandler(this.txtTaxGroupCode_Validated);
            // 
            // lblStatusTaxGroup
            // 
            this.lblStatusTaxGroup.AutoSize = true;
            this.lblStatusTaxGroup.Location = new System.Drawing.Point(803, 23);
            this.lblStatusTaxGroup.Name = "lblStatusTaxGroup";
            this.lblStatusTaxGroup.Size = new System.Drawing.Size(55, 13);
            this.lblStatusTaxGroup.TabIndex = 23;
            this.lblStatusTaxGroup.Text = "Status:*";
            // 
            // lblStartDateTaxGroup
            // 
            this.lblStartDateTaxGroup.AutoSize = true;
            this.lblStartDateTaxGroup.Location = new System.Drawing.Point(570, 23);
            this.lblStartDateTaxGroup.Name = "lblStartDateTaxGroup";
            this.lblStartDateTaxGroup.Size = new System.Drawing.Size(78, 13);
            this.lblStartDateTaxGroup.TabIndex = 22;
            this.lblStartDateTaxGroup.Text = "Start Date:*";
            // 
            // lblAppliedOn
            // 
            this.lblAppliedOn.AutoSize = true;
            this.lblAppliedOn.Location = new System.Drawing.Point(311, 24);
            this.lblAppliedOn.Name = "lblAppliedOn";
            this.lblAppliedOn.Size = new System.Drawing.Size(81, 13);
            this.lblAppliedOn.TabIndex = 21;
            this.lblAppliedOn.Text = "Applied On:*";
            // 
            // lblTaxGroupCode
            // 
            this.lblTaxGroupCode.AutoSize = true;
            this.lblTaxGroupCode.Location = new System.Drawing.Point(16, 23);
            this.lblTaxGroupCode.Name = "lblTaxGroupCode";
            this.lblTaxGroupCode.Size = new System.Drawing.Size(113, 13);
            this.lblTaxGroupCode.TabIndex = 18;
            this.lblTaxGroupCode.Text = "Tax Group Code:*";
            // 
            // dgvTaxGroupSearch
            // 
            this.dgvTaxGroupSearch.AllowUserToAddRows = false;
            this.dgvTaxGroupSearch.AllowUserToDeleteRows = false;
            this.dgvTaxGroupSearch.AllowUserToResizeColumns = false;
            this.dgvTaxGroupSearch.AllowUserToResizeRows = false;
            this.dgvTaxGroupSearch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTaxGroupSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTaxGroupSearch.Location = new System.Drawing.Point(0, 0);
            this.dgvTaxGroupSearch.MultiSelect = false;
            this.dgvTaxGroupSearch.Name = "dgvTaxGroupSearch";
            this.dgvTaxGroupSearch.RowHeadersVisible = false;
            this.dgvTaxGroupSearch.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTaxGroupSearch.Size = new System.Drawing.Size(1005, 250);
            this.dgvTaxGroupSearch.TabIndex = 0;
            this.dgvTaxGroupSearch.TabStop = false;
            this.dgvTaxGroupSearch.SelectionChanged += new System.EventHandler(this.dgvTaxGroupSearch_SelectionChanged);
            this.dgvTaxGroupSearch.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTaxGroupSearch_CellContentClick);
            // 
            // dgvTaxCodeSearch
            // 
            this.dgvTaxCodeSearch.AllowUserToAddRows = false;
            this.dgvTaxCodeSearch.AllowUserToDeleteRows = false;
            this.dgvTaxCodeSearch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTaxCodeSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTaxCodeSearch.Location = new System.Drawing.Point(0, 0);
            this.dgvTaxCodeSearch.MultiSelect = false;
            this.dgvTaxCodeSearch.Name = "dgvTaxCodeSearch";
            this.dgvTaxCodeSearch.RowHeadersVisible = false;
            this.dgvTaxCodeSearch.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTaxCodeSearch.Size = new System.Drawing.Size(1005, 467);
            this.dgvTaxCodeSearch.TabIndex = 0;
            this.dgvTaxCodeSearch.TabStop = false;
            this.dgvTaxCodeSearch.SelectionChanged += new System.EventHandler(this.dgvTaxCodeSearch_SelectionChanged);
            this.dgvTaxCodeSearch.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTaxCodeSearch_CellContentClick);
            // 
            // errorProviderTaxGroup
            // 
            this.errorProviderTaxGroup.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProviderTaxGroup.ContainerControl = this;
            // 
            // dgvTaxGroupDetail
            // 
            this.dgvTaxGroupDetail.AllowUserToAddRows = false;
            this.dgvTaxGroupDetail.AllowUserToDeleteRows = false;
            this.dgvTaxGroupDetail.AllowUserToOrderColumns = true;
            this.dgvTaxGroupDetail.AllowUserToResizeColumns = false;
            this.dgvTaxGroupDetail.AllowUserToResizeRows = false;
            this.dgvTaxGroupDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTaxGroupDetail.Location = new System.Drawing.Point(-1, 45);
            this.dgvTaxGroupDetail.Name = "dgvTaxGroupDetail";
            this.dgvTaxGroupDetail.RowHeadersVisible = false;
            this.dgvTaxGroupDetail.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTaxGroupDetail.Size = new System.Drawing.Size(1005, 153);
            this.dgvTaxGroupDetail.TabIndex = 28;
            this.dgvTaxGroupDetail.TabStop = false;
            this.dgvTaxGroupDetail.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTaxGroupDetail_CellContentClick);
            // 
            // btnResetTGDetail
            // 
            this.btnResetTGDetail.BackColor = System.Drawing.Color.Transparent;
            this.btnResetTGDetail.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnResetTGDetail.BackgroundImage")));
            this.btnResetTGDetail.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnResetTGDetail.FlatAppearance.BorderSize = 0;
            this.btnResetTGDetail.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnResetTGDetail.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnResetTGDetail.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnResetTGDetail.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnResetTGDetail.Location = new System.Drawing.Point(913, 7);
            this.btnResetTGDetail.Name = "btnResetTGDetail";
            this.btnResetTGDetail.Size = new System.Drawing.Size(75, 32);
            this.btnResetTGDetail.TabIndex = 3;
            this.btnResetTGDetail.Text = "Rese&t";
            this.btnResetTGDetail.UseVisualStyleBackColor = false;
            this.btnResetTGDetail.Click += new System.EventHandler(this.btnResetTGDetail_Click);
            // 
            // btnAddTGDetail
            // 
            this.btnAddTGDetail.BackColor = System.Drawing.Color.Transparent;
            this.btnAddTGDetail.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAddTGDetail.BackgroundImage")));
            this.btnAddTGDetail.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAddTGDetail.FlatAppearance.BorderSize = 0;
            this.btnAddTGDetail.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnAddTGDetail.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnAddTGDetail.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddTGDetail.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnAddTGDetail.Location = new System.Drawing.Point(832, 7);
            this.btnAddTGDetail.Name = "btnAddTGDetail";
            this.btnAddTGDetail.Size = new System.Drawing.Size(75, 32);
            this.btnAddTGDetail.TabIndex = 2;
            this.btnAddTGDetail.Text = "&Add";
            this.btnAddTGDetail.UseVisualStyleBackColor = false;
            this.btnAddTGDetail.Click += new System.EventHandler(this.btnAddTGDetail_Click);
            // 
            // cmbTaxCodes
            // 
            this.cmbTaxCodes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTaxCodes.FormattingEnabled = true;
            this.cmbTaxCodes.Location = new System.Drawing.Point(133, 14);
            this.cmbTaxCodes.Name = "cmbTaxCodes";
            this.cmbTaxCodes.Size = new System.Drawing.Size(152, 21);
            this.cmbTaxCodes.TabIndex = 0;
            this.cmbTaxCodes.SelectedIndexChanged += new System.EventHandler(this.cmbTaxCodes_SelectedIndexChanged);
            // 
            // txtGroupOrder
            // 
            this.txtGroupOrder.Location = new System.Drawing.Point(395, 14);
            this.txtGroupOrder.MaxLength = 2;
            this.txtGroupOrder.Name = "txtGroupOrder";
            this.txtGroupOrder.Size = new System.Drawing.Size(133, 21);
            this.txtGroupOrder.TabIndex = 1;
            this.txtGroupOrder.Validated += new System.EventHandler(this.txtGroupOrder_Validated);
            // 
            // lblGroupOrder
            // 
            this.lblGroupOrder.AutoSize = true;
            this.lblGroupOrder.Location = new System.Drawing.Point(302, 17);
            this.lblGroupOrder.Name = "lblGroupOrder";
            this.lblGroupOrder.Size = new System.Drawing.Size(91, 13);
            this.lblGroupOrder.TabIndex = 24;
            this.lblGroupOrder.Text = "Group Order:*";
            // 
            // lblTaxCode
            // 
            this.lblTaxCode.AutoSize = true;
            this.lblTaxCode.Location = new System.Drawing.Point(56, 17);
            this.lblTaxCode.Name = "lblTaxCode";
            this.lblTaxCode.Size = new System.Drawing.Size(74, 13);
            this.lblTaxCode.TabIndex = 23;
            this.lblTaxCode.Text = "Tax Code:*";
            // 
            // lblSearchDetails
            // 
            this.lblSearchDetails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(117)))), ((int)(((byte)(186)))));
            this.lblSearchDetails.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblSearchDetails.ForeColor = System.Drawing.Color.White;
            this.lblSearchDetails.Location = new System.Drawing.Point(0, 246);
            this.lblSearchDetails.Name = "lblSearchDetails";
            this.lblSearchDetails.Size = new System.Drawing.Size(1005, 24);
            this.lblSearchDetails.TabIndex = 1;
            this.lblSearchDetails.Text = "Search Result";
            this.lblSearchDetails.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlbuttons
            // 
            this.pnlbuttons.Controls.Add(this.btnSearchTaxGroup);
            this.pnlbuttons.Controls.Add(this.btnResetTaxGroup);
            this.pnlbuttons.Controls.Add(this.btnSaveTaxGroup);
            this.pnlbuttons.Location = new System.Drawing.Point(3, 204);
            this.pnlbuttons.Name = "pnlbuttons";
            this.pnlbuttons.Size = new System.Drawing.Size(990, 32);
            this.pnlbuttons.TabIndex = 4;
            // 
            // btnSearchTaxGroup
            // 
            this.btnSearchTaxGroup.BackColor = System.Drawing.Color.Transparent;
            this.btnSearchTaxGroup.BackgroundImage = global::TaxComponent.Properties.Resources.button;
            this.btnSearchTaxGroup.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearchTaxGroup.FlatAppearance.BorderSize = 0;
            this.btnSearchTaxGroup.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearchTaxGroup.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearchTaxGroup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchTaxGroup.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnSearchTaxGroup.Location = new System.Drawing.Point(749, 0);
            this.btnSearchTaxGroup.Name = "btnSearchTaxGroup";
            this.btnSearchTaxGroup.Size = new System.Drawing.Size(75, 32);
            this.btnSearchTaxGroup.TabIndex = 5;
            this.btnSearchTaxGroup.Text = "S&earch";
            this.btnSearchTaxGroup.UseVisualStyleBackColor = false;
            this.btnSearchTaxGroup.Click += new System.EventHandler(this.btnSearchTaxGroup_Click);
            // 
            // btnResetTaxGroup
            // 
            this.btnResetTaxGroup.BackColor = System.Drawing.Color.Transparent;
            this.btnResetTaxGroup.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnResetTaxGroup.BackgroundImage")));
            this.btnResetTaxGroup.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnResetTaxGroup.FlatAppearance.BorderSize = 0;
            this.btnResetTaxGroup.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnResetTaxGroup.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnResetTaxGroup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnResetTaxGroup.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnResetTaxGroup.Location = new System.Drawing.Point(911, 0);
            this.btnResetTaxGroup.Name = "btnResetTaxGroup";
            this.btnResetTaxGroup.Size = new System.Drawing.Size(75, 32);
            this.btnResetTaxGroup.TabIndex = 7;
            this.btnResetTaxGroup.Text = "&Reset";
            this.btnResetTaxGroup.UseVisualStyleBackColor = false;
            this.btnResetTaxGroup.Click += new System.EventHandler(this.btnResetTaxGroup_Click);
            // 
            // btnSaveTaxGroup
            // 
            this.btnSaveTaxGroup.BackColor = System.Drawing.Color.Transparent;
            this.btnSaveTaxGroup.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSaveTaxGroup.BackgroundImage")));
            this.btnSaveTaxGroup.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSaveTaxGroup.FlatAppearance.BorderSize = 0;
            this.btnSaveTaxGroup.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSaveTaxGroup.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSaveTaxGroup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveTaxGroup.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnSaveTaxGroup.Location = new System.Drawing.Point(830, 0);
            this.btnSaveTaxGroup.Name = "btnSaveTaxGroup";
            this.btnSaveTaxGroup.Size = new System.Drawing.Size(75, 32);
            this.btnSaveTaxGroup.TabIndex = 6;
            this.btnSaveTaxGroup.Text = "&Save";
            this.btnSaveTaxGroup.UseVisualStyleBackColor = false;
            this.btnSaveTaxGroup.Click += new System.EventHandler(this.btnSaveTaxGroup_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnResetTGDetail);
            this.panel1.Controls.Add(this.dgvTaxGroupDetail);
            this.panel1.Controls.Add(this.lblGroupOrder);
            this.panel1.Controls.Add(this.btnAddTGDetail);
            this.panel1.Controls.Add(this.lblTaxCode);
            this.panel1.Controls.Add(this.txtGroupOrder);
            this.panel1.Controls.Add(this.cmbTaxCodes);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1004, 199);
            this.panel1.TabIndex = 3;
            // 
            // frmTaxCode
            // 
            this.AcceptButton = this.btnSearch;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1013, 707);
            this.Name = "frmTaxCode";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "TaxGroupCode";
            this.WindowState = System.Windows.Forms.FormWindowState.Normal;
            this.Load += new System.EventHandler(this.frmTaxCode_Load);
            this.pnlCreateHeader.ResumeLayout(false);
            this.pnlCreateHeader.PerformLayout();
            this.grpAddDetails.ResumeLayout(false);
            this.pnlSearchHeader.ResumeLayout(false);
            this.pnlSearchHeader.PerformLayout();
            this.pnlSearchGrid.ResumeLayout(false);
            this.pnlCreateDetail.ResumeLayout(false);
            this.pnlTopButtons.ResumeLayout(false);
            this.pnlSearchButtons.ResumeLayout(false);
            this.tabSearch.ResumeLayout(false);
            this.tabCreate.ResumeLayout(false);
            this.tabControlTransaction.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errProviderTaxCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTaxGroupSearch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTaxCodeSearch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderTaxGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTaxGroupDetail)).EndInit();
            this.pnlbuttons.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpStartDateTaxCode;
        private System.Windows.Forms.TextBox txtTaxCode;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblPercentage;
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.Label lblCode;
        private System.Windows.Forms.ComboBox cmbStatusTaxCode;
        private System.Windows.Forms.TextBox txtDescriptionTaxCode;
        private System.Windows.Forms.TextBox txtPercentageTaxCode;
        private System.Windows.Forms.Button btnSaveTaxCode;
        private System.Windows.Forms.ErrorProvider errProviderTaxCode;
        private System.Windows.Forms.ComboBox cmbstatusTaxGroup;
        private System.Windows.Forms.ComboBox cmbTaxGroupAppliedOn;
        private System.Windows.Forms.DateTimePicker dtpStartDateTaxGroup;
        private System.Windows.Forms.TextBox txtTaxGroupCode;
        private System.Windows.Forms.Label lblStatusTaxGroup;
        private System.Windows.Forms.Label lblStartDateTaxGroup;
        private System.Windows.Forms.Label lblAppliedOn;
        private System.Windows.Forms.Label lblTaxGroupCode;
        private System.Windows.Forms.DataGridView dgvTaxGroupSearch;
        private System.Windows.Forms.DataGridView dgvTaxCodeSearch;
        private System.Windows.Forms.ErrorProvider errorProviderTaxGroup;
        private System.Windows.Forms.TextBox txtGroupOrder;
        private System.Windows.Forms.Label lblGroupOrder;
        private System.Windows.Forms.Label lblTaxCode;
        private System.Windows.Forms.ComboBox cmbTaxCodes;
        private System.Windows.Forms.Button btnResetTGDetail;
        private System.Windows.Forms.Button btnAddTGDetail;
        private System.Windows.Forms.Label lblSearchDetails;
        private System.Windows.Forms.DataGridView dgvTaxGroupDetail;
        private System.Windows.Forms.Panel pnlbuttons;
        private System.Windows.Forms.Button btnSearchTaxGroup;
        private System.Windows.Forms.Button btnResetTaxGroup;
        private System.Windows.Forms.Button btnSaveTaxGroup;
        private System.Windows.Forms.Panel panel1;
    }
}
