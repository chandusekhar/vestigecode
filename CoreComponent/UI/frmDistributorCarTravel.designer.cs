namespace CoreComponent.UI
{
    partial class frmDistributorCarTravel : CoreComponent.Core.UI.Transaction
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDistributorCarTravel));
            this.label1 = new System.Windows.Forms.Label();
            this.txtDistributorId = new System.Windows.Forms.TextBox();
            this.txtDistributorName = new System.Windows.Forms.TextBox();
            this.lblDistributorName = new System.Windows.Forms.Label();
            this.lblDistributorId = new System.Windows.Forms.Label();
            this.dgvDistributorTravelFund = new System.Windows.Forms.DataGridView();
            this.txtSDistributorId = new System.Windows.Forms.TextBox();
            this.lblSearchDistributorId = new System.Windows.Forms.Label();
            this.txtSDistributorName = new System.Windows.Forms.TextBox();
            this.lblSDistributorName = new System.Windows.Forms.Label();
            this.errTravel = new System.Windows.Forms.ErrorProvider(this.components);
            this.errCar = new System.Windows.Forms.ErrorProvider(this.components);
            this.dgvDistributorCarFund = new System.Windows.Forms.DataGridView();
            this.btnSaveDistributor = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.lblMonth = new System.Windows.Forms.Label();
            this.cmbYear = new System.Windows.Forms.ComboBox();
            this.lblYear = new System.Windows.Forms.Label();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.lblAmountTotal = new System.Windows.Forms.Label();
            this.txtAmountTotal = new System.Windows.Forms.TextBox();
            this.lblAmountAccumulated = new System.Windows.Forms.Label();
            this.txtAmountAccumulated = new System.Windows.Forms.TextBox();
            this.btnSSearch = new System.Windows.Forms.Button();
            this.pnlCreateHeader.SuspendLayout();
            this.grpAddDetails.SuspendLayout();
            this.pnlSearchHeader.SuspendLayout();
            this.pnlSearchGrid.SuspendLayout();
            this.pnlLowerButtons.SuspendLayout();
            this.pnlTopButtons.SuspendLayout();
            this.pnlSearchButtons.SuspendLayout();
            this.tabSearch.SuspendLayout();
            this.tabCreate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDistributorTravelFund)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errTravel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errCar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDistributorCarFund)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCreateHeader
            // 
            this.pnlCreateHeader.Controls.Add(this.lblMonth);
            this.pnlCreateHeader.Controls.Add(this.cmbYear);
            this.pnlCreateHeader.Controls.Add(this.lblYear);
            this.pnlCreateHeader.Controls.Add(this.cmbStatus);
            this.pnlCreateHeader.Controls.Add(this.txtSDistributorName);
            this.pnlCreateHeader.Controls.Add(this.txtAmountAccumulated);
            this.pnlCreateHeader.Controls.Add(this.txtAmountTotal);
            this.pnlCreateHeader.Controls.Add(this.lblAmountAccumulated);
            this.pnlCreateHeader.Controls.Add(this.txtSDistributorId);
            this.pnlCreateHeader.Controls.Add(this.lblAmountTotal);
            this.pnlCreateHeader.Controls.Add(this.lblSDistributorName);
            this.pnlCreateHeader.Controls.Add(this.lblSearchDistributorId);
            this.pnlCreateHeader.Size = new System.Drawing.Size(1005, 130);
            this.pnlCreateHeader.TabIndex = 0;
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblSearchDistributorId, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblSDistributorName, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblAmountTotal, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.txtSDistributorId, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblAmountAccumulated, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.txtAmountTotal, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.txtAmountAccumulated, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.txtSDistributorName, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.pnlTopButtons, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.cmbStatus, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblYear, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.cmbYear, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblMonth, 0);
            // 
            // btnCreateReset
            // 
            this.btnCreateReset.FlatAppearance.BorderSize = 0;
            this.btnCreateReset.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnCreateReset.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnCreateReset.Size = new System.Drawing.Size(75, 0);
            this.btnCreateReset.TabStop = false;
            this.btnCreateReset.Visible = false;
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSave.Size = new System.Drawing.Size(75, 0);
            this.btnSave.TabIndex = 1;
            this.btnSave.TabStop = false;
            this.btnSave.Visible = false;
            // 
            // lblAddDetails
            // 
            this.lblAddDetails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(117)))), ((int)(((byte)(186)))));
            this.lblAddDetails.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAddDetails.ForeColor = System.Drawing.Color.White;
            this.lblAddDetails.TabIndex = 0;
            this.lblAddDetails.Text = "Travel";
            // 
            // grpAddDetails
            // 
            this.grpAddDetails.Controls.Add(this.dgvDistributorTravelFund);
            this.grpAddDetails.Location = new System.Drawing.Point(0, 154);
            this.grpAddDetails.Size = new System.Drawing.Size(1005, 457);
            this.grpAddDetails.TabIndex = 0;
            this.grpAddDetails.Controls.SetChildIndex(this.pnlCreateDetail, 0);
            this.grpAddDetails.Controls.SetChildIndex(this.pnlLowerButtons, 0);
            this.grpAddDetails.Controls.SetChildIndex(this.dgvDistributorTravelFund, 0);
            // 
            // btnClearDetails
            // 
            this.btnClearDetails.BackColor = System.Drawing.Color.Transparent;
            this.btnClearDetails.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearDetails.BackgroundImage")));
            this.btnClearDetails.FlatAppearance.BorderSize = 0;
            this.btnClearDetails.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnClearDetails.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnClearDetails.TabIndex = 8;
            this.btnClearDetails.Text = "Reset";
            this.btnClearDetails.Click += new System.EventHandler(this.btnClearDetails_Click);
            // 
            // btnAddDetails
            // 
            this.btnAddDetails.BackColor = System.Drawing.Color.Transparent;
            this.btnAddDetails.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAddDetails.BackgroundImage")));
            this.btnAddDetails.FlatAppearance.BorderSize = 0;
            this.btnAddDetails.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnAddDetails.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnAddDetails.TabIndex = 7;
            this.btnAddDetails.Text = "&Save";
            this.btnAddDetails.Click += new System.EventHandler(this.btnAddDetails_Click);
            // 
            // pnlSearchHeader
            // 
            this.pnlSearchHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSearchHeader.Controls.Add(this.txtDistributorName);
            this.pnlSearchHeader.Controls.Add(this.lblDistributorName);
            this.pnlSearchHeader.Controls.Add(this.txtDistributorId);
            this.pnlSearchHeader.Controls.Add(this.lblDistributorId);
            this.pnlSearchHeader.Size = new System.Drawing.Size(1005, 70);
            this.pnlSearchHeader.Tag = "Car Fund";
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblDistributorId, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.txtDistributorId, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblDistributorName, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.txtDistributorName, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.pnlSearchButtons, 0);
            // 
            // btnSearch
            // 
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearch.Location = new System.Drawing.Point(853, 0);
            this.btnSearch.TabIndex = 29;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnSearchReset
            // 
            this.btnSearchReset.FlatAppearance.BorderSize = 0;
            this.btnSearchReset.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearchReset.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearchReset.Location = new System.Drawing.Point(928, 0);
            this.btnSearchReset.TabIndex = 30;
            this.btnSearchReset.Click += new System.EventHandler(this.btnSearchReset_Click);
            // 
            // lblSearchResult
            // 
            this.lblSearchResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSearchResult.Text = "Distributor Search Result";
            // 
            // pnlSearchGrid
            // 
            this.pnlSearchGrid.Controls.Add(this.btnReset);
            this.pnlSearchGrid.Controls.Add(this.btnSaveDistributor);
            this.pnlSearchGrid.Controls.Add(this.dgvDistributorCarFund);
            this.pnlSearchGrid.Location = new System.Drawing.Point(0, 94);
            this.pnlSearchGrid.Size = new System.Drawing.Size(1005, 517);
            // 
            // pnlCreateDetail
            // 
            this.pnlCreateDetail.Size = new System.Drawing.Size(1005, 0);
            this.pnlCreateDetail.TabIndex = 0;
            // 
            // pnlLowerButtons
            // 
            this.pnlLowerButtons.Location = new System.Drawing.Point(0, 457);
            this.pnlLowerButtons.Size = new System.Drawing.Size(1005, 0);
            this.pnlLowerButtons.Visible = false;
            // 
            // pnlTopButtons
            // 
            this.pnlTopButtons.Controls.Add(this.btnSSearch);
            this.pnlTopButtons.Location = new System.Drawing.Point(0, 96);
            this.pnlTopButtons.TabIndex = 19;
            this.pnlTopButtons.Controls.SetChildIndex(this.btnClearDetails, 0);
            this.pnlTopButtons.Controls.SetChildIndex(this.btnAddDetails, 0);
            this.pnlTopButtons.Controls.SetChildIndex(this.btnSSearch, 0);
            // 
            // pnlSearchButtons
            // 
            this.pnlSearchButtons.Location = new System.Drawing.Point(0, 36);
            this.pnlSearchButtons.Size = new System.Drawing.Size(1003, 32);
            this.pnlSearchButtons.TabIndex = 28;
            // 
            // tabSearch
            // 
            this.tabSearch.Text = "Car";
            // 
            // tabCreate
            // 
            this.tabCreate.Text = "Travel";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(43, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "label1";
            // 
            // txtDistributorId
            // 
            this.txtDistributorId.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtDistributorId.Location = new System.Drawing.Point(140, 12);
            this.txtDistributorId.MaxLength = 50;
            this.txtDistributorId.Name = "txtDistributorId";
            this.txtDistributorId.Size = new System.Drawing.Size(111, 21);
            this.txtDistributorId.TabIndex = 1;
            // 
            // txtDistributorName
            // 
            this.txtDistributorName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtDistributorName.Location = new System.Drawing.Point(518, 12);
            this.txtDistributorName.MaxLength = 100;
            this.txtDistributorName.Name = "txtDistributorName";
            this.txtDistributorName.Size = new System.Drawing.Size(110, 21);
            this.txtDistributorName.TabIndex = 2;
            // 
            // lblDistributorName
            // 
            this.lblDistributorName.Location = new System.Drawing.Point(382, 15);
            this.lblDistributorName.Name = "lblDistributorName";
            this.lblDistributorName.Size = new System.Drawing.Size(138, 13);
            this.lblDistributorName.TabIndex = 80;
            this.lblDistributorName.Text = "Distributor Name:*";
            this.lblDistributorName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDistributorId
            // 
            this.lblDistributorId.Location = new System.Drawing.Point(15, 15);
            this.lblDistributorId.Name = "lblDistributorId";
            this.lblDistributorId.Size = new System.Drawing.Size(125, 13);
            this.lblDistributorId.TabIndex = 78;
            this.lblDistributorId.Text = "Distributor Id:*";
            this.lblDistributorId.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dgvDistributorTravelFund
            // 
            this.dgvDistributorTravelFund.AllowUserToAddRows = false;
            this.dgvDistributorTravelFund.AllowUserToDeleteRows = false;
            this.dgvDistributorTravelFund.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDistributorTravelFund.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDistributorTravelFund.Location = new System.Drawing.Point(0, 14);
            this.dgvDistributorTravelFund.MultiSelect = false;
            this.dgvDistributorTravelFund.Name = "dgvDistributorTravelFund";
            this.dgvDistributorTravelFund.RowHeadersVisible = false;
            this.dgvDistributorTravelFund.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDistributorTravelFund.Size = new System.Drawing.Size(1005, 443);
            this.dgvDistributorTravelFund.TabIndex = 0;
            this.dgvDistributorTravelFund.TabStop = false;
            this.dgvDistributorTravelFund.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDistributorTravelFund_CellClick);
            // 
            // txtSDistributorId
            // 
            this.txtSDistributorId.Location = new System.Drawing.Point(826, 15);
            this.txtSDistributorId.MaxLength = 30;
            this.txtSDistributorId.Name = "txtSDistributorId";
            this.txtSDistributorId.Size = new System.Drawing.Size(150, 21);
            this.txtSDistributorId.TabIndex = 3;
            // 
            // lblSearchDistributorId
            // 
            this.lblSearchDistributorId.Location = new System.Drawing.Point(701, 17);
            this.lblSearchDistributorId.Name = "lblSearchDistributorId";
            this.lblSearchDistributorId.Size = new System.Drawing.Size(125, 13);
            this.lblSearchDistributorId.TabIndex = 78;
            this.lblSearchDistributorId.Text = "Distributor Id:*";
            this.lblSearchDistributorId.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSDistributorName
            // 
            this.txtSDistributorName.Location = new System.Drawing.Point(134, 56);
            this.txtSDistributorName.MaxLength = 100;
            this.txtSDistributorName.Name = "txtSDistributorName";
            this.txtSDistributorName.Size = new System.Drawing.Size(150, 21);
            this.txtSDistributorName.TabIndex = 4;
            // 
            // lblSDistributorName
            // 
            this.lblSDistributorName.Location = new System.Drawing.Point(9, 59);
            this.lblSDistributorName.Name = "lblSDistributorName";
            this.lblSDistributorName.Size = new System.Drawing.Size(125, 13);
            this.lblSDistributorName.TabIndex = 124;
            this.lblSDistributorName.Text = "Distributor Name:*";
            this.lblSDistributorName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // errTravel
            // 
            this.errTravel.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errTravel.ContainerControl = this;
            // 
            // errCar
            // 
            this.errCar.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errCar.ContainerControl = this;
            // 
            // dgvDistributorCarFund
            // 
            this.dgvDistributorCarFund.AllowUserToAddRows = false;
            this.dgvDistributorCarFund.AllowUserToDeleteRows = false;
            this.dgvDistributorCarFund.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dgvDistributorCarFund.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDistributorCarFund.Location = new System.Drawing.Point(0, 2);
            this.dgvDistributorCarFund.Name = "dgvDistributorCarFund";
            this.dgvDistributorCarFund.RowHeadersVisible = false;
            this.dgvDistributorCarFund.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDistributorCarFund.Size = new System.Drawing.Size(1005, 467);
            this.dgvDistributorCarFund.TabIndex = 3;
            this.dgvDistributorCarFund.TabStop = false;
            this.dgvDistributorCarFund.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvDistributorCarFund_CellPainting);
            this.dgvDistributorCarFund.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDistributorCarFund_CellContentClick);
            // 
            // btnSaveDistributor
            // 
            this.btnSaveDistributor.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSaveDistributor.BackColor = System.Drawing.Color.Transparent;
            this.btnSaveDistributor.BackgroundImage = global::CoreComponent.Properties.Resources.button;
            this.btnSaveDistributor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSaveDistributor.FlatAppearance.BorderSize = 0;
            this.btnSaveDistributor.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSaveDistributor.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSaveDistributor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveDistributor.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnSaveDistributor.Location = new System.Drawing.Point(841, 475);
            this.btnSaveDistributor.Name = "btnSaveDistributor";
            this.btnSaveDistributor.Size = new System.Drawing.Size(75, 32);
            this.btnSaveDistributor.TabIndex = 123;
            this.btnSaveDistributor.Text = "&Save";
            this.btnSaveDistributor.UseVisualStyleBackColor = false;
            this.btnSaveDistributor.Click += new System.EventHandler(this.btnSaveDistributor_Click);
            // 
            // btnReset
            // 
            this.btnReset.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnReset.BackColor = System.Drawing.Color.Transparent;
            this.btnReset.BackgroundImage = global::CoreComponent.Properties.Resources.button;
            this.btnReset.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnReset.FlatAppearance.BorderSize = 0;
            this.btnReset.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnReset.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReset.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnReset.Location = new System.Drawing.Point(922, 475);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 32);
            this.btnReset.TabIndex = 124;
            this.btnReset.Text = "Reset All";
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // lblMonth
            // 
            this.lblMonth.BackColor = System.Drawing.Color.Transparent;
            this.lblMonth.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMonth.Location = new System.Drawing.Point(48, 20);
            this.lblMonth.Name = "lblMonth";
            this.lblMonth.Size = new System.Drawing.Size(86, 13);
            this.lblMonth.TabIndex = 127;
            this.lblMonth.Text = "Month:*";
            this.lblMonth.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbYear
            // 
            this.cmbYear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.cmbYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbYear.Enabled = false;
            this.cmbYear.FormattingEnabled = true;
            this.cmbYear.Location = new System.Drawing.Point(461, 17);
            this.cmbYear.Name = "cmbYear";
            this.cmbYear.Size = new System.Drawing.Size(150, 21);
            this.cmbYear.TabIndex = 2;
            // 
            // lblYear
            // 
            this.lblYear.BackColor = System.Drawing.Color.Transparent;
            this.lblYear.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblYear.Location = new System.Drawing.Point(409, 17);
            this.lblYear.Name = "lblYear";
            this.lblYear.Size = new System.Drawing.Size(51, 17);
            this.lblYear.TabIndex = 126;
            this.lblYear.Text = "Year:*";
            this.lblYear.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbStatus
            // 
            this.cmbStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.Enabled = false;
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Location = new System.Drawing.Point(134, 16);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(150, 21);
            this.cmbStatus.TabIndex = 1;
            // 
            // lblAmountTotal
            // 
            this.lblAmountTotal.Location = new System.Drawing.Point(337, 59);
            this.lblAmountTotal.Name = "lblAmountTotal";
            this.lblAmountTotal.Size = new System.Drawing.Size(125, 13);
            this.lblAmountTotal.TabIndex = 78;
            this.lblAmountTotal.Text = "Total Amount:*";
            this.lblAmountTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtAmountTotal
            // 
            this.txtAmountTotal.Location = new System.Drawing.Point(461, 56);
            this.txtAmountTotal.MaxLength = 20;
            this.txtAmountTotal.Name = "txtAmountTotal";
            this.txtAmountTotal.ReadOnly = true;
            this.txtAmountTotal.Size = new System.Drawing.Size(150, 21);
            this.txtAmountTotal.TabIndex = 3;
            this.txtAmountTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblAmountAccumulated
            // 
            this.lblAmountAccumulated.Location = new System.Drawing.Point(679, 57);
            this.lblAmountAccumulated.Name = "lblAmountAccumulated";
            this.lblAmountAccumulated.Size = new System.Drawing.Size(150, 13);
            this.lblAmountAccumulated.TabIndex = 78;
            this.lblAmountAccumulated.Text = "Accumulated Amount:*";
            this.lblAmountAccumulated.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtAmountAccumulated
            // 
            this.txtAmountAccumulated.Location = new System.Drawing.Point(826, 55);
            this.txtAmountAccumulated.MaxLength = 20;
            this.txtAmountAccumulated.Name = "txtAmountAccumulated";
            this.txtAmountAccumulated.Size = new System.Drawing.Size(150, 21);
            this.txtAmountAccumulated.TabIndex = 5;
            this.txtAmountAccumulated.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAmountAccumulated.Validated += new System.EventHandler(this.txtAmountAccumulated_Validated);
            // 
            // btnSSearch
            // 
            this.btnSSearch.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSSearch.BackColor = System.Drawing.Color.Transparent;
            this.btnSSearch.BackgroundImage = global::CoreComponent.Properties.Resources.button;
            this.btnSSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSSearch.FlatAppearance.BorderSize = 0;
            this.btnSSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSSearch.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnSSearch.Location = new System.Drawing.Point(762, 1);
            this.btnSSearch.Name = "btnSSearch";
            this.btnSSearch.Size = new System.Drawing.Size(75, 32);
            this.btnSSearch.TabIndex = 6;
            this.btnSSearch.Text = "&Search";
            this.btnSSearch.UseVisualStyleBackColor = false;
            this.btnSSearch.Click += new System.EventHandler(this.btnSSearch_Click);
            // 
            // frmDistributorCarTravel
            // 
            this.AcceptButton = this.btnSearch;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImage = global::CoreComponent.Properties.Resources.button;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1013, 707);
            this.Name = "frmDistributorCarTravel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Location Hierarchy";
            this.WindowState = System.Windows.Forms.FormWindowState.Normal;
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
            ((System.ComponentModel.ISupportInitialize)(this.dgvDistributorTravelFund)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errTravel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errCar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDistributorCarFund)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        

        //#endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDistributorId;
        private System.Windows.Forms.TextBox txtDistributorName;
        private System.Windows.Forms.Label lblDistributorName;
        private System.Windows.Forms.Label lblDistributorId;
        private System.Windows.Forms.DataGridView dgvDistributorTravelFund;
        private System.Windows.Forms.Label lblSearchDistributorId;
        private System.Windows.Forms.TextBox txtSDistributorId;
        private System.Windows.Forms.TextBox txtSDistributorName;
        private System.Windows.Forms.Label lblSDistributorName;
        private System.Windows.Forms.ErrorProvider errTravel;
        private System.Windows.Forms.ErrorProvider errCar;
        private System.Windows.Forms.DataGridView dgvDistributorCarFund;
        private System.Windows.Forms.Button btnSaveDistributor;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Label lblMonth;
        private System.Windows.Forms.ComboBox cmbYear;
        private System.Windows.Forms.Label lblYear;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.TextBox txtAmountTotal;
        private System.Windows.Forms.Label lblAmountTotal;
        private System.Windows.Forms.TextBox txtAmountAccumulated;
        private System.Windows.Forms.Label lblAmountAccumulated;
        private System.Windows.Forms.Button btnSSearch;
    }
}