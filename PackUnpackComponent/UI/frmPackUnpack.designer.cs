namespace PackUnpackComponent.UI
{
    partial class frmPackUnpack
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPackUnpack));
            this.tabControlHierarchy = new System.Windows.Forms.TabControl();
            this.tabPageSearch = new System.Windows.Forms.TabPage();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.splitCon_PU_Header_Detail = new System.Windows.Forms.SplitContainer();
            this.dgvPUHeader = new System.Windows.Forms.DataGridView();
            this.dgvPUDetail = new System.Windows.Forms.DataGridView();
            this.pnlSearchHeader = new System.Windows.Forms.Panel();
            this.cmbSearchPU = new System.Windows.Forms.ComboBox();
            this.txtSerachVouWithItemCode = new System.Windows.Forms.TextBox();
            this.lblSearchItemCode = new System.Windows.Forms.Label();
            this.lblPackUnpack = new System.Windows.Forms.Label();
            this.lblToDate = new System.Windows.Forms.Label();
            this.lblFromDate = new System.Windows.Forms.Label();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnSearchReset = new System.Windows.Forms.Button();
            this.lblSearchResult = new System.Windows.Forms.Label();
            this.tabPageCreate = new System.Windows.Forms.TabPage();
            this.pnlGridSearch = new System.Windows.Forms.Panel();
            this.dgvConstituentPack = new System.Windows.Forms.DataGridView();
            this.dgvConstituentUnpack = new System.Windows.Forms.DataGridView();
            this.btnSavePUVoucher = new System.Windows.Forms.Button();
            this.lblCon = new System.Windows.Forms.Label();
            this.pnlDetailsHeader = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblDisplayAvailableQty = new System.Windows.Forms.Label();
            this.lblAvailableQty = new System.Windows.Forms.Label();
            this.btnProcess = new System.Windows.Forms.Button();
            this.txtMfgNo = new System.Windows.Forms.TextBox();
            this.lblMfg = new System.Windows.Forms.Label();
            this.txtMrp = new System.Windows.Forms.TextBox();
            this.lblExpDate = new System.Windows.Forms.Label();
            this.dtpExpDate = new System.Windows.Forms.DateTimePicker();
            this.lblMfgDate = new System.Windows.Forms.Label();
            this.dtpMfgDate = new System.Windows.Forms.DateTimePicker();
            this.lblMrp = new System.Windows.Forms.Label();
            this.lblPUQty = new System.Windows.Forms.Label();
            this.lblRemarks = new System.Windows.Forms.Label();
            this.txtPUQuantity = new System.Windows.Forms.TextBox();
            this.txtRemarks = new System.Windows.Forms.TextBox();
            this.lblPUDate = new System.Windows.Forms.Label();
            this.dtpPUDate = new System.Windows.Forms.DateTimePicker();
            this.lblDisplayItemName = new System.Windows.Forms.Label();
            this.cmbPackUnpack = new System.Windows.Forms.ComboBox();
            this.lblSelPackUnPack = new System.Windows.Forms.Label();
            this.txtSearchItemCode = new System.Windows.Forms.TextBox();
            this.btnResetCreate = new System.Windows.Forms.Button();
            this.lblItemName = new System.Windows.Forms.Label();
            this.lblItemCode = new System.Windows.Forms.Label();
            this.cmbMfgBatch = new System.Windows.Forms.ComboBox();
            this.btnExit1 = new System.Windows.Forms.Button();
            this.errCreatePU = new System.Windows.Forms.ErrorProvider(this.components);
            this.pnlBase.SuspendLayout();
            this.tabControlHierarchy.SuspendLayout();
            this.tabPageSearch.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            this.splitCon_PU_Header_Detail.Panel1.SuspendLayout();
            this.splitCon_PU_Header_Detail.Panel2.SuspendLayout();
            this.splitCon_PU_Header_Detail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPUHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPUDetail)).BeginInit();
            this.pnlSearchHeader.SuspendLayout();
            this.tabPageCreate.SuspendLayout();
            this.pnlGridSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvConstituentPack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvConstituentUnpack)).BeginInit();
            this.pnlDetailsHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errCreatePU)).BeginInit();
            this.SuspendLayout();
            // 
            // btnExit
            // 
            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnExit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnExit.Location = new System.Drawing.Point(935, 3);
            // 
            // tabControlHierarchy
            // 
            this.tabControlHierarchy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlHierarchy.Controls.Add(this.tabPageSearch);
            this.tabControlHierarchy.Controls.Add(this.tabPageCreate);
            this.tabControlHierarchy.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.tabControlHierarchy.Location = new System.Drawing.Point(2, 51);
            this.tabControlHierarchy.Name = "tabControlHierarchy";
            this.tabControlHierarchy.SelectedIndex = 0;
            this.tabControlHierarchy.Size = new System.Drawing.Size(1008, 634);
            this.tabControlHierarchy.TabIndex = 0;
            this.tabControlHierarchy.SelectedIndexChanged += new System.EventHandler(this.tabControlHierarchy_SelectedIndexChanged);
            // 
            // tabPageSearch
            // 
            this.tabPageSearch.BackColor = System.Drawing.Color.Transparent;
            this.tabPageSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tabPageSearch.BackgroundImage")));
            this.tabPageSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tabPageSearch.Controls.Add(this.pnlSearch);
            this.tabPageSearch.Controls.Add(this.pnlSearchHeader);
            this.tabPageSearch.Controls.Add(this.lblSearchResult);
            this.tabPageSearch.Location = new System.Drawing.Point(4, 22);
            this.tabPageSearch.Name = "tabPageSearch";
            this.tabPageSearch.Size = new System.Drawing.Size(1000, 608);
            this.tabPageSearch.TabIndex = 0;
            this.tabPageSearch.Text = "Search";
            // 
            // pnlSearch
            // 
            this.pnlSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(117)))), ((int)(((byte)(186)))));
            this.pnlSearch.Controls.Add(this.splitCon_PU_Header_Detail);
            this.pnlSearch.Location = new System.Drawing.Point(3, 132);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(993, 463);
            this.pnlSearch.TabIndex = 5;
            // 
            // splitCon_PU_Header_Detail
            // 
            this.splitCon_PU_Header_Detail.BackColor = System.Drawing.Color.SteelBlue;
            this.splitCon_PU_Header_Detail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitCon_PU_Header_Detail.Location = new System.Drawing.Point(0, 0);
            this.splitCon_PU_Header_Detail.Name = "splitCon_PU_Header_Detail";
            // 
            // splitCon_PU_Header_Detail.Panel1
            // 
            this.splitCon_PU_Header_Detail.Panel1.Controls.Add(this.dgvPUHeader);
            // 
            // splitCon_PU_Header_Detail.Panel2
            // 
            this.splitCon_PU_Header_Detail.Panel2.Controls.Add(this.dgvPUDetail);
            this.splitCon_PU_Header_Detail.Size = new System.Drawing.Size(990, 460);
            this.splitCon_PU_Header_Detail.SplitterDistance = 497;
            this.splitCon_PU_Header_Detail.TabIndex = 0;
            // 
            // dgvPUHeader
            // 
            this.dgvPUHeader.AllowUserToAddRows = false;
            this.dgvPUHeader.AllowUserToDeleteRows = false;
            this.dgvPUHeader.AllowUserToResizeColumns = false;
            this.dgvPUHeader.AllowUserToResizeRows = false;
            this.dgvPUHeader.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPUHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPUHeader.Location = new System.Drawing.Point(0, 0);
            this.dgvPUHeader.MultiSelect = false;
            this.dgvPUHeader.Name = "dgvPUHeader";
            this.dgvPUHeader.ReadOnly = true;
            this.dgvPUHeader.RowHeadersVisible = false;
            this.dgvPUHeader.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPUHeader.Size = new System.Drawing.Size(495, 458);
            this.dgvPUHeader.TabIndex = 39;
            this.dgvPUHeader.TabStop = false;
            this.dgvPUHeader.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPUHeader_CellClick);
            this.dgvPUHeader.CurrentCellChanged += new System.EventHandler(this.dgvPUHeader_CurrentCellChanged);
            this.dgvPUHeader.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPUHeader_CellContentClick);
            // 
            // dgvPUDetail
            // 
            this.dgvPUDetail.AllowUserToAddRows = false;
            this.dgvPUDetail.AllowUserToDeleteRows = false;
            this.dgvPUDetail.AllowUserToResizeColumns = false;
            this.dgvPUDetail.AllowUserToResizeRows = false;
            this.dgvPUDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPUDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPUDetail.Location = new System.Drawing.Point(0, 0);
            this.dgvPUDetail.MultiSelect = false;
            this.dgvPUDetail.Name = "dgvPUDetail";
            this.dgvPUDetail.ReadOnly = true;
            this.dgvPUDetail.RowHeadersVisible = false;
            this.dgvPUDetail.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPUDetail.Size = new System.Drawing.Size(487, 458);
            this.dgvPUDetail.TabIndex = 40;
            this.dgvPUDetail.TabStop = false;
            // 
            // pnlSearchHeader
            // 
            this.pnlSearchHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlSearchHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(233)))), ((int)(((byte)(251)))));
            this.pnlSearchHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSearchHeader.Controls.Add(this.cmbSearchPU);
            this.pnlSearchHeader.Controls.Add(this.txtSerachVouWithItemCode);
            this.pnlSearchHeader.Controls.Add(this.lblSearchItemCode);
            this.pnlSearchHeader.Controls.Add(this.lblPackUnpack);
            this.pnlSearchHeader.Controls.Add(this.lblToDate);
            this.pnlSearchHeader.Controls.Add(this.lblFromDate);
            this.pnlSearchHeader.Controls.Add(this.dtpToDate);
            this.pnlSearchHeader.Controls.Add(this.dtpFromDate);
            this.pnlSearchHeader.Controls.Add(this.btnSearch);
            this.pnlSearchHeader.Controls.Add(this.btnSearchReset);
            this.pnlSearchHeader.Location = new System.Drawing.Point(1, 0);
            this.pnlSearchHeader.Name = "pnlSearchHeader";
            this.pnlSearchHeader.Size = new System.Drawing.Size(998, 108);
            this.pnlSearchHeader.TabIndex = 0;
            // 
            // cmbSearchPU
            // 
            this.cmbSearchPU.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSearchPU.FormattingEnabled = true;
            this.cmbSearchPU.Location = new System.Drawing.Point(132, 14);
            this.cmbSearchPU.Name = "cmbSearchPU";
            this.cmbSearchPU.Size = new System.Drawing.Size(125, 21);
            this.cmbSearchPU.TabIndex = 0;
            // 
            // txtSerachVouWithItemCode
            // 
            this.txtSerachVouWithItemCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtSerachVouWithItemCode.Location = new System.Drawing.Point(132, 52);
            this.txtSerachVouWithItemCode.MaxLength = 20;
            this.txtSerachVouWithItemCode.Name = "txtSerachVouWithItemCode";
            this.txtSerachVouWithItemCode.Size = new System.Drawing.Size(125, 21);
            this.txtSerachVouWithItemCode.TabIndex = 3;
            // 
            // lblSearchItemCode
            // 
            this.lblSearchItemCode.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblSearchItemCode.Location = new System.Drawing.Point(30, 55);
            this.lblSearchItemCode.Name = "lblSearchItemCode";
            this.lblSearchItemCode.Size = new System.Drawing.Size(96, 13);
            this.lblSearchItemCode.TabIndex = 115;
            this.lblSearchItemCode.Text = "ItemCode: ";
            this.lblSearchItemCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPackUnpack
            // 
            this.lblPackUnpack.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.lblPackUnpack.Location = new System.Drawing.Point(30, 18);
            this.lblPackUnpack.Name = "lblPackUnpack";
            this.lblPackUnpack.Size = new System.Drawing.Size(96, 13);
            this.lblPackUnpack.TabIndex = 113;
            this.lblPackUnpack.Text = "Pack/UnPack:*";
            this.lblPackUnpack.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblToDate
            // 
            this.lblToDate.Location = new System.Drawing.Point(640, 17);
            this.lblToDate.Name = "lblToDate";
            this.lblToDate.Size = new System.Drawing.Size(125, 13);
            this.lblToDate.TabIndex = 111;
            this.lblToDate.Text = "To date: ";
            this.lblToDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblFromDate
            // 
            this.lblFromDate.Location = new System.Drawing.Point(317, 18);
            this.lblFromDate.Name = "lblFromDate";
            this.lblFromDate.Size = new System.Drawing.Size(125, 13);
            this.lblFromDate.TabIndex = 110;
            this.lblFromDate.Text = "From date: ";
            this.lblFromDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpToDate
            // 
            this.dtpToDate.Checked = false;
            this.dtpToDate.CustomFormat = "dd-MM-yyyy";
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpToDate.Location = new System.Drawing.Point(771, 13);
            this.dtpToDate.MaxDate = new System.DateTime(2009, 8, 19, 0, 0, 0, 0);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.ShowCheckBox = true;
            this.dtpToDate.Size = new System.Drawing.Size(125, 21);
            this.dtpToDate.TabIndex = 2;
            this.dtpToDate.Value = new System.DateTime(2009, 8, 19, 0, 0, 0, 0);
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Checked = false;
            this.dtpFromDate.CustomFormat = "dd-MM-yyyy";
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFromDate.Location = new System.Drawing.Point(448, 14);
            this.dtpFromDate.MaxDate = new System.DateTime(2009, 8, 19, 0, 0, 0, 0);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.ShowCheckBox = true;
            this.dtpFromDate.Size = new System.Drawing.Size(125, 21);
            this.dtpFromDate.TabIndex = 1;
            this.dtpFromDate.Value = new System.DateTime(2009, 8, 19, 0, 0, 0, 0);
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.Transparent;
            this.btnSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.BackgroundImage")));
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnSearch.Location = new System.Drawing.Point(837, 71);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 32);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnSearchReset
            // 
            this.btnSearchReset.BackColor = System.Drawing.Color.Transparent;
            this.btnSearchReset.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearchReset.BackgroundImage")));
            this.btnSearchReset.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearchReset.FlatAppearance.BorderSize = 0;
            this.btnSearchReset.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearchReset.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearchReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchReset.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnSearchReset.Location = new System.Drawing.Point(918, 71);
            this.btnSearchReset.Name = "btnSearchReset";
            this.btnSearchReset.Size = new System.Drawing.Size(75, 32);
            this.btnSearchReset.TabIndex = 5;
            this.btnSearchReset.Text = "Reset";
            this.btnSearchReset.UseVisualStyleBackColor = false;
            this.btnSearchReset.Click += new System.EventHandler(this.btnSearchReset_Click);
            // 
            // lblSearchResult
            // 
            this.lblSearchResult.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSearchResult.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(117)))), ((int)(((byte)(186)))));
            this.lblSearchResult.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblSearchResult.ForeColor = System.Drawing.Color.Azure;
            this.lblSearchResult.Location = new System.Drawing.Point(1, 110);
            this.lblSearchResult.Name = "lblSearchResult";
            this.lblSearchResult.Size = new System.Drawing.Size(995, 20);
            this.lblSearchResult.TabIndex = 3;
            this.lblSearchResult.Text = "Search Result";
            this.lblSearchResult.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabPageCreate
            // 
            this.tabPageCreate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(233)))), ((int)(((byte)(251)))));
            this.tabPageCreate.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tabPageCreate.BackgroundImage")));
            this.tabPageCreate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tabPageCreate.Controls.Add(this.pnlGridSearch);
            this.tabPageCreate.Controls.Add(this.btnSavePUVoucher);
            this.tabPageCreate.Controls.Add(this.lblCon);
            this.tabPageCreate.Controls.Add(this.pnlDetailsHeader);
            this.tabPageCreate.Location = new System.Drawing.Point(4, 22);
            this.tabPageCreate.Name = "tabPageCreate";
            this.tabPageCreate.Size = new System.Drawing.Size(1000, 608);
            this.tabPageCreate.TabIndex = 1;
            this.tabPageCreate.Text = "Create";
            // 
            // pnlGridSearch
            // 
            this.pnlGridSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlGridSearch.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.pnlGridSearch.Controls.Add(this.dgvConstituentPack);
            this.pnlGridSearch.Controls.Add(this.dgvConstituentUnpack);
            this.pnlGridSearch.Location = new System.Drawing.Point(3, 181);
            this.pnlGridSearch.Name = "pnlGridSearch";
            this.pnlGridSearch.Size = new System.Drawing.Size(994, 383);
            this.pnlGridSearch.TabIndex = 45;
            // 
            // dgvConstituentPack
            // 
            this.dgvConstituentPack.AllowUserToAddRows = false;
            this.dgvConstituentPack.AllowUserToDeleteRows = false;
            this.dgvConstituentPack.AllowUserToResizeColumns = false;
            this.dgvConstituentPack.AllowUserToResizeRows = false;
            this.dgvConstituentPack.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvConstituentPack.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvConstituentPack.Location = new System.Drawing.Point(0, 0);
            this.dgvConstituentPack.MultiSelect = false;
            this.dgvConstituentPack.Name = "dgvConstituentPack";
            this.dgvConstituentPack.RowHeadersVisible = false;
            this.dgvConstituentPack.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvConstituentPack.Size = new System.Drawing.Size(994, 383);
            this.dgvConstituentPack.TabIndex = 40;
            // 
            // dgvConstituentUnpack
            // 
            this.dgvConstituentUnpack.AllowUserToAddRows = false;
            this.dgvConstituentUnpack.AllowUserToDeleteRows = false;
            this.dgvConstituentUnpack.AllowUserToResizeColumns = false;
            this.dgvConstituentUnpack.AllowUserToResizeRows = false;
            this.dgvConstituentUnpack.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvConstituentUnpack.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvConstituentUnpack.Location = new System.Drawing.Point(0, 0);
            this.dgvConstituentUnpack.MultiSelect = false;
            this.dgvConstituentUnpack.Name = "dgvConstituentUnpack";
            this.dgvConstituentUnpack.RowHeadersVisible = false;
            this.dgvConstituentUnpack.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvConstituentUnpack.Size = new System.Drawing.Size(994, 383);
            this.dgvConstituentUnpack.TabIndex = 41;
            this.dgvConstituentUnpack.TabStop = false;
            this.dgvConstituentUnpack.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvConstituentUnpack_CellMouseClick);
            // 
            // btnSavePUVoucher
            // 
            this.btnSavePUVoucher.BackColor = System.Drawing.Color.Transparent;
            this.btnSavePUVoucher.BackgroundImage = global::PackUnpackComponent.Properties.Resources.button;
            this.btnSavePUVoucher.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSavePUVoucher.FlatAppearance.BorderSize = 0;
            this.btnSavePUVoucher.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSavePUVoucher.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSavePUVoucher.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSavePUVoucher.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnSavePUVoucher.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnSavePUVoucher.Location = new System.Drawing.Point(920, 570);
            this.btnSavePUVoucher.Name = "btnSavePUVoucher";
            this.btnSavePUVoucher.Size = new System.Drawing.Size(75, 32);
            this.btnSavePUVoucher.TabIndex = 11;
            this.btnSavePUVoucher.Text = "&Save";
            this.btnSavePUVoucher.UseVisualStyleBackColor = false;
            this.btnSavePUVoucher.Click += new System.EventHandler(this.btnSavePUVoucher_Click);
            // 
            // lblCon
            // 
            this.lblCon.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(117)))), ((int)(((byte)(186)))));
            this.lblCon.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblCon.ForeColor = System.Drawing.Color.White;
            this.lblCon.Location = new System.Drawing.Point(-2, 155);
            this.lblCon.Name = "lblCon";
            this.lblCon.Size = new System.Drawing.Size(999, 23);
            this.lblCon.TabIndex = 44;
            this.lblCon.Text = "Constituent Item";
            this.lblCon.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlDetailsHeader
            // 
            this.pnlDetailsHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlDetailsHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(233)))), ((int)(((byte)(251)))));
            this.pnlDetailsHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlDetailsHeader.Controls.Add(this.pictureBox1);
            this.pnlDetailsHeader.Controls.Add(this.lblDisplayAvailableQty);
            this.pnlDetailsHeader.Controls.Add(this.lblAvailableQty);
            this.pnlDetailsHeader.Controls.Add(this.btnProcess);
            this.pnlDetailsHeader.Controls.Add(this.txtMfgNo);
            this.pnlDetailsHeader.Controls.Add(this.lblMfg);
            this.pnlDetailsHeader.Controls.Add(this.txtMrp);
            this.pnlDetailsHeader.Controls.Add(this.lblExpDate);
            this.pnlDetailsHeader.Controls.Add(this.dtpExpDate);
            this.pnlDetailsHeader.Controls.Add(this.lblMfgDate);
            this.pnlDetailsHeader.Controls.Add(this.dtpMfgDate);
            this.pnlDetailsHeader.Controls.Add(this.lblMrp);
            this.pnlDetailsHeader.Controls.Add(this.lblPUQty);
            this.pnlDetailsHeader.Controls.Add(this.lblRemarks);
            this.pnlDetailsHeader.Controls.Add(this.txtPUQuantity);
            this.pnlDetailsHeader.Controls.Add(this.txtRemarks);
            this.pnlDetailsHeader.Controls.Add(this.lblPUDate);
            this.pnlDetailsHeader.Controls.Add(this.dtpPUDate);
            this.pnlDetailsHeader.Controls.Add(this.lblDisplayItemName);
            this.pnlDetailsHeader.Controls.Add(this.cmbPackUnpack);
            this.pnlDetailsHeader.Controls.Add(this.lblSelPackUnPack);
            this.pnlDetailsHeader.Controls.Add(this.txtSearchItemCode);
            this.pnlDetailsHeader.Controls.Add(this.btnResetCreate);
            this.pnlDetailsHeader.Controls.Add(this.lblItemName);
            this.pnlDetailsHeader.Controls.Add(this.lblItemCode);
            this.pnlDetailsHeader.Controls.Add(this.cmbMfgBatch);
            this.pnlDetailsHeader.Location = new System.Drawing.Point(1, 3);
            this.pnlDetailsHeader.Name = "pnlDetailsHeader";
            this.pnlDetailsHeader.Size = new System.Drawing.Size(999, 149);
            this.pnlDetailsHeader.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::PackUnpackComponent.Properties.Resources.find;
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(459, 7);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(21, 21);
            this.pictureBox1.TabIndex = 126;
            this.pictureBox1.TabStop = false;
            // 
            // lblDisplayAvailableQty
            // 
            this.lblDisplayAvailableQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDisplayAvailableQty.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblDisplayAvailableQty.Location = new System.Drawing.Point(360, 80);
            this.lblDisplayAvailableQty.Name = "lblDisplayAvailableQty";
            this.lblDisplayAvailableQty.Size = new System.Drawing.Size(93, 21);
            this.lblDisplayAvailableQty.TabIndex = 125;
            this.lblDisplayAvailableQty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblAvailableQty
            // 
            this.lblAvailableQty.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblAvailableQty.Location = new System.Drawing.Point(251, 83);
            this.lblAvailableQty.Name = "lblAvailableQty";
            this.lblAvailableQty.Size = new System.Drawing.Size(103, 13);
            this.lblAvailableQty.TabIndex = 124;
            this.lblAvailableQty.Text = "Available Qty.: ";
            this.lblAvailableQty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnProcess
            // 
            this.btnProcess.BackColor = System.Drawing.Color.Transparent;
            this.btnProcess.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnProcess.BackgroundImage")));
            this.btnProcess.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnProcess.FlatAppearance.BorderSize = 0;
            this.btnProcess.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnProcess.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnProcess.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProcess.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnProcess.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnProcess.Location = new System.Drawing.Point(837, 112);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Size = new System.Drawing.Size(75, 32);
            this.btnProcess.TabIndex = 9;
            this.btnProcess.Text = "&Process";
            this.btnProcess.UseVisualStyleBackColor = false;
            this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
            // 
            // txtMfgNo
            // 
            this.txtMfgNo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtMfgNo.Location = new System.Drawing.Point(121, 45);
            this.txtMfgNo.MaxLength = 20;
            this.txtMfgNo.Name = "txtMfgNo";
            this.txtMfgNo.Size = new System.Drawing.Size(93, 21);
            this.txtMfgNo.TabIndex = 3;
            // 
            // lblMfg
            // 
            this.lblMfg.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblMfg.Location = new System.Drawing.Point(15, 48);
            this.lblMfg.Name = "lblMfg";
            this.lblMfg.Size = new System.Drawing.Size(100, 13);
            this.lblMfg.TabIndex = 122;
            this.lblMfg.Text = "MFG. Batch No.: ";
            this.lblMfg.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtMrp
            // 
            this.txtMrp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtMrp.Location = new System.Drawing.Point(360, 44);
            this.txtMrp.MaxLength = 10;
            this.txtMrp.Name = "txtMrp";
            this.txtMrp.Size = new System.Drawing.Size(93, 21);
            this.txtMrp.TabIndex = 4;
            // 
            // lblExpDate
            // 
            this.lblExpDate.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblExpDate.Location = new System.Drawing.Point(717, 47);
            this.lblExpDate.Name = "lblExpDate";
            this.lblExpDate.Size = new System.Drawing.Size(125, 13);
            this.lblExpDate.TabIndex = 120;
            this.lblExpDate.Text = "Exp. Date:";
            this.lblExpDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpExpDate
            // 
            this.dtpExpDate.Checked = false;
            this.dtpExpDate.CustomFormat = "dd-MM-yyyy";
            this.dtpExpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpExpDate.Location = new System.Drawing.Point(848, 44);
            this.dtpExpDate.MaxDate = new System.DateTime(2009, 8, 19, 0, 0, 0, 0);
            this.dtpExpDate.Name = "dtpExpDate";
            this.dtpExpDate.Size = new System.Drawing.Size(93, 21);
            this.dtpExpDate.TabIndex = 6;
            this.dtpExpDate.Value = new System.DateTime(2009, 8, 19, 0, 0, 0, 0);
            // 
            // lblMfgDate
            // 
            this.lblMfgDate.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblMfgDate.Location = new System.Drawing.Point(507, 48);
            this.lblMfgDate.Name = "lblMfgDate";
            this.lblMfgDate.Size = new System.Drawing.Size(85, 13);
            this.lblMfgDate.TabIndex = 118;
            this.lblMfgDate.Text = "MFG Date: ";
            this.lblMfgDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpMfgDate
            // 
            this.dtpMfgDate.Checked = false;
            this.dtpMfgDate.CustomFormat = "dd-MM-yyyy";
            this.dtpMfgDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpMfgDate.Location = new System.Drawing.Point(598, 44);
            this.dtpMfgDate.MaxDate = new System.DateTime(2009, 8, 19, 0, 0, 0, 0);
            this.dtpMfgDate.Name = "dtpMfgDate";
            this.dtpMfgDate.Size = new System.Drawing.Size(93, 21);
            this.dtpMfgDate.TabIndex = 5;
            this.dtpMfgDate.Value = new System.DateTime(2009, 8, 19, 0, 0, 0, 0);
            // 
            // lblMrp
            // 
            this.lblMrp.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblMrp.Location = new System.Drawing.Point(258, 48);
            this.lblMrp.Name = "lblMrp";
            this.lblMrp.Size = new System.Drawing.Size(96, 13);
            this.lblMrp.TabIndex = 115;
            this.lblMrp.Text = "MRP: ";
            this.lblMrp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPUQty
            // 
            this.lblPUQty.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblPUQty.Location = new System.Drawing.Point(476, 83);
            this.lblPUQty.Name = "lblPUQty";
            this.lblPUQty.Size = new System.Drawing.Size(116, 13);
            this.lblPUQty.TabIndex = 48;
            this.lblPUQty.Text = "Pack/Unpack Qty.: ";
            this.lblPUQty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblRemarks
            // 
            this.lblRemarks.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblRemarks.Location = new System.Drawing.Point(30, 83);
            this.lblRemarks.Name = "lblRemarks";
            this.lblRemarks.Size = new System.Drawing.Size(85, 13);
            this.lblRemarks.TabIndex = 114;
            this.lblRemarks.Text = "Remarks: ";
            this.lblRemarks.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPUQuantity
            // 
            this.txtPUQuantity.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtPUQuantity.Location = new System.Drawing.Point(598, 81);
            this.txtPUQuantity.MaxLength = 4;
            this.txtPUQuantity.Name = "txtPUQuantity";
            this.txtPUQuantity.Size = new System.Drawing.Size(93, 21);
            this.txtPUQuantity.TabIndex = 8;
            // 
            // txtRemarks
            // 
            this.txtRemarks.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtRemarks.Location = new System.Drawing.Point(121, 80);
            this.txtRemarks.MaxLength = 20;
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Size = new System.Drawing.Size(93, 21);
            this.txtRemarks.TabIndex = 7;
            // 
            // lblPUDate
            // 
            this.lblPUDate.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblPUDate.Location = new System.Drawing.Point(720, 11);
            this.lblPUDate.Name = "lblPUDate";
            this.lblPUDate.Size = new System.Drawing.Size(122, 13);
            this.lblPUDate.TabIndex = 112;
            this.lblPUDate.Text = "Pack/Unpack Date:";
            this.lblPUDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpPUDate
            // 
            this.dtpPUDate.Checked = false;
            this.dtpPUDate.CustomFormat = "dd-MM-yyyy";
            this.dtpPUDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpPUDate.Location = new System.Drawing.Point(848, 7);
            this.dtpPUDate.MaxDate = new System.DateTime(2009, 8, 19, 0, 0, 0, 0);
            this.dtpPUDate.Name = "dtpPUDate";
            this.dtpPUDate.Size = new System.Drawing.Size(93, 21);
            this.dtpPUDate.TabIndex = 2;
            this.dtpPUDate.Value = new System.DateTime(2009, 8, 19, 0, 0, 0, 0);
            // 
            // lblDisplayItemName
            // 
            this.lblDisplayItemName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDisplayItemName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblDisplayItemName.Location = new System.Drawing.Point(598, 7);
            this.lblDisplayItemName.Name = "lblDisplayItemName";
            this.lblDisplayItemName.Size = new System.Drawing.Size(93, 21);
            this.lblDisplayItemName.TabIndex = 45;
            this.lblDisplayItemName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbPackUnpack
            // 
            this.cmbPackUnpack.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPackUnpack.FormattingEnabled = true;
            this.cmbPackUnpack.Location = new System.Drawing.Point(121, 7);
            this.cmbPackUnpack.Name = "cmbPackUnpack";
            this.cmbPackUnpack.Size = new System.Drawing.Size(93, 21);
            this.cmbPackUnpack.TabIndex = 0;
            this.cmbPackUnpack.SelectedIndexChanged += new System.EventHandler(this.cmbPackUnpack_SelectedIndexChanged);
            // 
            // lblSelPackUnPack
            // 
            this.lblSelPackUnPack.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblSelPackUnPack.Location = new System.Drawing.Point(19, 11);
            this.lblSelPackUnPack.Name = "lblSelPackUnPack";
            this.lblSelPackUnPack.Size = new System.Drawing.Size(96, 13);
            this.lblSelPackUnPack.TabIndex = 50;
            this.lblSelPackUnPack.Text = "Pack/UnPack:";
            this.lblSelPackUnPack.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSearchItemCode
            // 
            this.txtSearchItemCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtSearchItemCode.Location = new System.Drawing.Point(360, 7);
            this.txtSearchItemCode.MaxLength = 20;
            this.txtSearchItemCode.Name = "txtSearchItemCode";
            this.txtSearchItemCode.Size = new System.Drawing.Size(93, 21);
            this.txtSearchItemCode.TabIndex = 1;
            this.txtSearchItemCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearchItemCode_KeyDown);
            this.txtSearchItemCode.Leave += new System.EventHandler(this.txtSearchItemCode_Leave);
            // 
            // btnResetCreate
            // 
            this.btnResetCreate.BackColor = System.Drawing.Color.Transparent;
            this.btnResetCreate.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnResetCreate.BackgroundImage")));
            this.btnResetCreate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnResetCreate.FlatAppearance.BorderSize = 0;
            this.btnResetCreate.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnResetCreate.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnResetCreate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnResetCreate.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnResetCreate.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnResetCreate.Location = new System.Drawing.Point(918, 112);
            this.btnResetCreate.Name = "btnResetCreate";
            this.btnResetCreate.Size = new System.Drawing.Size(75, 32);
            this.btnResetCreate.TabIndex = 10;
            this.btnResetCreate.Text = "&Reset";
            this.btnResetCreate.UseVisualStyleBackColor = false;
            this.btnResetCreate.Click += new System.EventHandler(this.btnResetCreate_Click);
            // 
            // lblItemName
            // 
            this.lblItemName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblItemName.Location = new System.Drawing.Point(507, 11);
            this.lblItemName.Name = "lblItemName";
            this.lblItemName.Size = new System.Drawing.Size(85, 13);
            this.lblItemName.TabIndex = 44;
            this.lblItemName.Text = "ItemName: ";
            this.lblItemName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblItemCode
            // 
            this.lblItemCode.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblItemCode.Location = new System.Drawing.Point(258, 11);
            this.lblItemCode.Name = "lblItemCode";
            this.lblItemCode.Size = new System.Drawing.Size(96, 13);
            this.lblItemCode.TabIndex = 43;
            this.lblItemCode.Text = "ItemCode: ";
            this.lblItemCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbMfgBatch
            // 
            this.cmbMfgBatch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMfgBatch.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbMfgBatch.FormattingEnabled = true;
            this.cmbMfgBatch.Location = new System.Drawing.Point(121, 45);
            this.cmbMfgBatch.Name = "cmbMfgBatch";
            this.cmbMfgBatch.Size = new System.Drawing.Size(93, 21);
            this.cmbMfgBatch.TabIndex = 3;
            this.cmbMfgBatch.SelectedIndexChanged += new System.EventHandler(this.cmbMfgBatch_SelectedIndexChanged);
            // 
            // btnExit1
            // 
            this.btnExit1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnExit1.Location = new System.Drawing.Point(905, 627);
            this.btnExit1.Name = "btnExit1";
            this.btnExit1.Size = new System.Drawing.Size(75, 23);
            this.btnExit1.TabIndex = 9;
            this.btnExit1.Text = "Exit";
            this.btnExit1.UseVisualStyleBackColor = true;
            this.btnExit1.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // errCreatePU
            // 
            this.errCreatePU.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errCreatePU.ContainerControl = this;
            // 
            // frmPackUnpack
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(117)))), ((int)(((byte)(186)))));
            this.ClientSize = new System.Drawing.Size(1013, 703);
            this.Controls.Add(this.tabControlHierarchy);
            this.Location = new System.Drawing.Point(0, 0);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPackUnpack";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Pack Unpack";
            this.WindowState = System.Windows.Forms.FormWindowState.Normal;
            this.Load += new System.EventHandler(this.frmPackUnpack_Load);
            this.Controls.SetChildIndex(this.pnlBase, 0);
            this.Controls.SetChildIndex(this.tabControlHierarchy, 0);
            this.pnlBase.ResumeLayout(false);
            this.pnlBase.PerformLayout();
            this.tabControlHierarchy.ResumeLayout(false);
            this.tabPageSearch.ResumeLayout(false);
            this.pnlSearch.ResumeLayout(false);
            this.splitCon_PU_Header_Detail.Panel1.ResumeLayout(false);
            this.splitCon_PU_Header_Detail.Panel2.ResumeLayout(false);
            this.splitCon_PU_Header_Detail.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPUHeader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPUDetail)).EndInit();
            this.pnlSearchHeader.ResumeLayout(false);
            this.pnlSearchHeader.PerformLayout();
            this.tabPageCreate.ResumeLayout(false);
            this.pnlGridSearch.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvConstituentPack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvConstituentUnpack)).EndInit();
            this.pnlDetailsHeader.ResumeLayout(false);
            this.pnlDetailsHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errCreatePU)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.TabControl tabControlHierarchy;
        protected System.Windows.Forms.TabPage tabPageSearch;
        protected System.Windows.Forms.Label lblSearchResult;
        protected System.Windows.Forms.TabPage tabPageCreate;
        protected System.Windows.Forms.Button btnExit1;

        protected System.Windows.Forms.Panel pnlSearch;
        protected System.Windows.Forms.Panel pnlSearchHeader;
        protected System.Windows.Forms.Button btnSearch;
        protected System.Windows.Forms.Button btnSearchReset;
        private System.Windows.Forms.SplitContainer splitCon_PU_Header_Detail;
        private System.Windows.Forms.Label lblPackUnpack;
        private System.Windows.Forms.Label lblToDate;
        private System.Windows.Forms.Label lblFromDate;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.DataGridView dgvPUHeader;
        private System.Windows.Forms.DataGridView dgvPUDetail;
        protected System.Windows.Forms.Button btnSavePUVoucher;
        private System.Windows.Forms.DataGridView dgvConstituentPack;
        private System.Windows.Forms.Label lblDisplayItemName;
        private System.Windows.Forms.Label lblItemName;
        protected System.Windows.Forms.Button btnResetCreate;
        protected System.Windows.Forms.Panel pnlDetailsHeader;
        private System.Windows.Forms.TextBox txtSearchItemCode;
        private System.Windows.Forms.Label lblItemCode;
        private System.Windows.Forms.ErrorProvider errCreatePU;
        private System.Windows.Forms.Label lblPUDate;
        private System.Windows.Forms.DateTimePicker dtpPUDate;
        private System.Windows.Forms.Label lblPUQty;
        private System.Windows.Forms.TextBox txtPUQuantity;
        private System.Windows.Forms.Label lblRemarks;
        private System.Windows.Forms.TextBox txtRemarks;
        private System.Windows.Forms.Label lblMrp;
        private System.Windows.Forms.Label lblExpDate;
        private System.Windows.Forms.DateTimePicker dtpExpDate;
        private System.Windows.Forms.Label lblMfgDate;
        private System.Windows.Forms.DateTimePicker dtpMfgDate;
        protected System.Windows.Forms.Panel pnlGridSearch;
        protected System.Windows.Forms.Label lblCon;
        private System.Windows.Forms.TextBox txtMrp;
        private System.Windows.Forms.ComboBox cmbPackUnpack;
        private System.Windows.Forms.Label lblSelPackUnPack;
        private System.Windows.Forms.DataGridView dgvConstituentUnpack;
        private System.Windows.Forms.TextBox txtMfgNo;
        private System.Windows.Forms.Label lblMfg;
        private System.Windows.Forms.TextBox txtSerachVouWithItemCode;
        private System.Windows.Forms.Label lblSearchItemCode;
        protected System.Windows.Forms.Button btnProcess;
        private System.Windows.Forms.ComboBox cmbMfgBatch;
        private System.Windows.Forms.ComboBox cmbSearchPU;
        private System.Windows.Forms.Label lblDisplayAvailableQty;
        private System.Windows.Forms.Label lblAvailableQty;
        private System.Windows.Forms.PictureBox pictureBox1;



    }
}

