namespace CoreComponent.UI
{
    partial class frmAppLocModLink
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAppLocModLink));
            this.lblapptype = new System.Windows.Forms.Label();
            this.lblloctype = new System.Windows.Forms.Label();
            this.lblmoduletype = new System.Windows.Forms.Label();
            this.cmbapptype = new System.Windows.Forms.ComboBox();
            this.cmbloctype = new System.Windows.Forms.ComboBox();
            this.cmbmodule = new System.Windows.Forms.ComboBox();
            this.cmbstatusadd = new System.Windows.Forms.ComboBox();
            this.cmbmoduleadd = new System.Windows.Forms.ComboBox();
            this.cmbapptypeadd = new System.Windows.Forms.ComboBox();
            this.lblstatusadd = new System.Windows.Forms.Label();
            this.lblfunctionsadd = new System.Windows.Forms.Label();
            this.lblmoduleadd = new System.Windows.Forms.Label();
            this.lblloctypeadd = new System.Windows.Forms.Label();
            this.lblapptypeadd = new System.Windows.Forms.Label();
            this.errprovadd = new System.Windows.Forms.ErrorProvider(this.components);
            this.cmbParentadd = new System.Windows.Forms.ComboBox();
            this.lblparentadd = new System.Windows.Forms.Label();
            this.lblmenunameadd = new System.Windows.Forms.Label();
            this.menunameadd = new System.Windows.Forms.TextBox();
            this.seqnoadd = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.dgvApplocmodfunc = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmbloctypeadd = new System.Windows.Forms.ComboBox();
            this.listBoxfunctionsadd = new System.Windows.Forms.CheckedListBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.pnlCreateHeader.SuspendLayout();
            this.grpAddDetails.SuspendLayout();
            this.pnlSearchHeader.SuspendLayout();
            this.pnlSearchGrid.SuspendLayout();
            this.pnlCreateDetail.SuspendLayout();
            this.tabSearch.SuspendLayout();
            this.tabCreate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errprovadd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvApplocmodfunc)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCreateHeader
            // 
            this.pnlCreateHeader.Controls.Add(this.groupBox2);
            this.pnlCreateHeader.Size = new System.Drawing.Size(1003, 298);
            this.pnlCreateHeader.Controls.SetChildIndex(this.groupBox2, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.pnlTopButtons, 0);
            // 
            // btnCreateReset
            // 
            this.btnCreateReset.Dock = System.Windows.Forms.DockStyle.None;
            this.btnCreateReset.FlatAppearance.BorderSize = 0;
            this.btnCreateReset.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnCreateReset.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnCreateReset.Location = new System.Drawing.Point(844, 199);
            this.btnCreateReset.Size = new System.Drawing.Size(65, 32);
            this.btnCreateReset.Visible = false;
            // 
            // btnSave
            // 
            this.btnSave.Dock = System.Windows.Forms.DockStyle.None;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnSave.Location = new System.Drawing.Point(919, 199);
            this.btnSave.Size = new System.Drawing.Size(65, 32);
            this.btnSave.Text = "Sa&ve";
            this.btnSave.Visible = false;
            // 
            // lblAddDetails
            // 
            this.lblAddDetails.Size = new System.Drawing.Size(1003, 24);
            this.lblAddDetails.Visible = false;
            // 
            // grpAddDetails
            // 
            this.grpAddDetails.Location = new System.Drawing.Point(0, 322);
            this.grpAddDetails.Size = new System.Drawing.Size(1003, 289);
            this.grpAddDetails.Visible = false;
            // 
            // btnClearDetails
            // 
            this.btnClearDetails.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearDetails.BackgroundImage")));
            this.btnClearDetails.Dock = System.Windows.Forms.DockStyle.None;
            this.btnClearDetails.FlatAppearance.BorderSize = 0;
            this.btnClearDetails.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnClearDetails.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnClearDetails.Location = new System.Drawing.Point(868, 225);
            this.btnClearDetails.Size = new System.Drawing.Size(75, 32);
            this.btnClearDetails.TabIndex = 10;
            this.btnClearDetails.Click += new System.EventHandler(this.btnClearDetails_Click);
            // 
            // btnAddDetails
            // 
            this.btnAddDetails.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAddDetails.BackgroundImage")));
            this.btnAddDetails.Dock = System.Windows.Forms.DockStyle.None;
            this.btnAddDetails.FlatAppearance.BorderSize = 0;
            this.btnAddDetails.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnAddDetails.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnAddDetails.Location = new System.Drawing.Point(787, 225);
            this.btnAddDetails.Size = new System.Drawing.Size(75, 32);
            this.btnAddDetails.TabIndex = 9;
            this.btnAddDetails.Text = "&Save";
            this.btnAddDetails.Click += new System.EventHandler(this.btnAddDetails_Click);
            // 
            // pnlSearchHeader
            // 
            this.pnlSearchHeader.Controls.Add(this.groupBox1);
            this.pnlSearchHeader.Size = new System.Drawing.Size(1003, 127);
            this.pnlSearchHeader.Controls.SetChildIndex(this.groupBox1, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.pnlSearchButtons, 0);
            // 
            // btnSearch
            // 
            this.btnSearch.Dock = System.Windows.Forms.DockStyle.None;
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearch.Location = new System.Drawing.Point(649, 63);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnSearchReset
            // 
            this.btnSearchReset.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearchReset.Dock = System.Windows.Forms.DockStyle.None;
            this.btnSearchReset.FlatAppearance.BorderSize = 0;
            this.btnSearchReset.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearchReset.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearchReset.Location = new System.Drawing.Point(729, 63);
            this.btnSearchReset.TabIndex = 5;
            this.btnSearchReset.Click += new System.EventHandler(this.btnSearchReset_Click);
            // 
            // lblSearchResult
            // 
            this.lblSearchResult.Dock = System.Windows.Forms.DockStyle.None;
            this.lblSearchResult.Location = new System.Drawing.Point(0, 3);
            this.lblSearchResult.Size = new System.Drawing.Size(990, 24);
            // 
            // pnlSearchGrid
            // 
            this.pnlSearchGrid.Controls.Add(this.dgvApplocmodfunc);
            this.pnlSearchGrid.Controls.Add(this.groupBox4);
            this.pnlSearchGrid.Dock = System.Windows.Forms.DockStyle.None;
            this.pnlSearchGrid.Location = new System.Drawing.Point(1, 155);
            this.pnlSearchGrid.Size = new System.Drawing.Size(989, 445);
            // 
            // pnlCreateDetail
            // 
            this.pnlCreateDetail.Controls.Add(this.btnCreateReset);
            this.pnlCreateDetail.Controls.Add(this.btnSave);
            this.pnlCreateDetail.Size = new System.Drawing.Size(1003, 266);
            // 
            // pnlLowerButtons
            // 
            this.pnlLowerButtons.Location = new System.Drawing.Point(0, 257);
            this.pnlLowerButtons.Size = new System.Drawing.Size(1003, 32);
            // 
            // pnlTopButtons
            // 
            this.pnlTopButtons.Location = new System.Drawing.Point(0, 264);
            this.pnlTopButtons.Size = new System.Drawing.Size(1001, 32);
            // 
            // pnlSearchButtons
            // 
            this.pnlSearchButtons.Location = new System.Drawing.Point(0, 99);
            this.pnlSearchButtons.Size = new System.Drawing.Size(1003, 28);
            // 
            // tabSearch
            // 
            this.tabSearch.Size = new System.Drawing.Size(1003, 611);
            // 
            // tabCreate
            // 
            this.tabCreate.Size = new System.Drawing.Size(1003, 611);
            // 
            // lblapptype
            // 
            this.lblapptype.Location = new System.Drawing.Point(161, 23);
            this.lblapptype.Name = "lblapptype";
            this.lblapptype.Size = new System.Drawing.Size(101, 13);
            this.lblapptype.TabIndex = 3;
            this.lblapptype.Text = "Application Type";
            this.lblapptype.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblloctype
            // 
            this.lblloctype.Location = new System.Drawing.Point(514, 23);
            this.lblloctype.Name = "lblloctype";
            this.lblloctype.Size = new System.Drawing.Size(86, 13);
            this.lblloctype.TabIndex = 7;
            this.lblloctype.Text = "Location Type";
            this.lblloctype.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblmoduletype
            // 
            this.lblmoduletype.Location = new System.Drawing.Point(215, 57);
            this.lblmoduletype.Name = "lblmoduletype";
            this.lblmoduletype.Size = new System.Drawing.Size(47, 13);
            this.lblmoduletype.TabIndex = 4;
            this.lblmoduletype.Text = "Module";
            this.lblmoduletype.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbapptype
            // 
            this.cmbapptype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbapptype.FormattingEnabled = true;
            this.cmbapptype.Location = new System.Drawing.Point(268, 20);
            this.cmbapptype.Name = "cmbapptype";
            this.cmbapptype.Size = new System.Drawing.Size(203, 21);
            this.cmbapptype.TabIndex = 1;
            // 
            // cmbloctype
            // 
            this.cmbloctype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbloctype.FormattingEnabled = true;
            this.cmbloctype.Location = new System.Drawing.Point(606, 20);
            this.cmbloctype.Name = "cmbloctype";
            this.cmbloctype.Size = new System.Drawing.Size(203, 21);
            this.cmbloctype.TabIndex = 2;
            // 
            // cmbmodule
            // 
            this.cmbmodule.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbmodule.DropDownWidth = 204;
            this.cmbmodule.FormattingEnabled = true;
            this.cmbmodule.Location = new System.Drawing.Point(268, 54);
            this.cmbmodule.Name = "cmbmodule";
            this.cmbmodule.Size = new System.Drawing.Size(204, 21);
            this.cmbmodule.TabIndex = 3;
            // 
            // cmbstatusadd
            // 
            this.cmbstatusadd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbstatusadd.FormattingEnabled = true;
            this.cmbstatusadd.Location = new System.Drawing.Point(225, 172);
            this.cmbstatusadd.Name = "cmbstatusadd";
            this.cmbstatusadd.Size = new System.Drawing.Size(121, 21);
            this.cmbstatusadd.TabIndex = 8;
            // 
            // cmbmoduleadd
            // 
            this.cmbmoduleadd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbmoduleadd.FormattingEnabled = true;
            this.cmbmoduleadd.Location = new System.Drawing.Point(541, 58);
            this.cmbmoduleadd.Name = "cmbmoduleadd";
            this.cmbmoduleadd.Size = new System.Drawing.Size(204, 21);
            this.cmbmoduleadd.TabIndex = 4;
            this.cmbmoduleadd.SelectedIndexChanged += new System.EventHandler(this.cmbmoduleadd_SelectedIndexChanged);
            // 
            // cmbapptypeadd
            // 
            this.cmbapptypeadd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbapptypeadd.FormattingEnabled = true;
            this.cmbapptypeadd.Location = new System.Drawing.Point(225, 20);
            this.cmbapptypeadd.Name = "cmbapptypeadd";
            this.cmbapptypeadd.Size = new System.Drawing.Size(203, 21);
            this.cmbapptypeadd.TabIndex = 1;
            this.cmbapptypeadd.SelectedIndexChanged += new System.EventHandler(this.cmbapptypeadd_SelectedIndexChanged);
            // 
            // lblstatusadd
            // 
            this.lblstatusadd.Location = new System.Drawing.Point(174, 175);
            this.lblstatusadd.Name = "lblstatusadd";
            this.lblstatusadd.Size = new System.Drawing.Size(43, 13);
            this.lblstatusadd.TabIndex = 16;
            this.lblstatusadd.Text = "Status";
            this.lblstatusadd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblfunctionsadd
            // 
            this.lblfunctionsadd.Location = new System.Drawing.Point(470, 97);
            this.lblfunctionsadd.Name = "lblfunctionsadd";
            this.lblfunctionsadd.Size = new System.Drawing.Size(60, 13);
            this.lblfunctionsadd.TabIndex = 15;
            this.lblfunctionsadd.Text = "Functions";
            this.lblfunctionsadd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblmoduleadd
            // 
            this.lblmoduleadd.Location = new System.Drawing.Point(483, 61);
            this.lblmoduleadd.Name = "lblmoduleadd";
            this.lblmoduleadd.Size = new System.Drawing.Size(47, 13);
            this.lblmoduleadd.TabIndex = 14;
            this.lblmoduleadd.Text = "Module";
            this.lblmoduleadd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblloctypeadd
            // 
            this.lblloctypeadd.Location = new System.Drawing.Point(445, 23);
            this.lblloctypeadd.Name = "lblloctypeadd";
            this.lblloctypeadd.Size = new System.Drawing.Size(86, 13);
            this.lblloctypeadd.TabIndex = 13;
            this.lblloctypeadd.Text = "Location Type";
            this.lblloctypeadd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblapptypeadd
            // 
            this.lblapptypeadd.Location = new System.Drawing.Point(119, 23);
            this.lblapptypeadd.Name = "lblapptypeadd";
            this.lblapptypeadd.Size = new System.Drawing.Size(101, 13);
            this.lblapptypeadd.TabIndex = 12;
            this.lblapptypeadd.Text = "Application Type";
            this.lblapptypeadd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // errprovadd
            // 
            this.errprovadd.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errprovadd.ContainerControl = this;
            // 
            // cmbParentadd
            // 
            this.cmbParentadd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbParentadd.FormattingEnabled = true;
            this.cmbParentadd.Location = new System.Drawing.Point(225, 58);
            this.cmbParentadd.Name = "cmbParentadd";
            this.cmbParentadd.Size = new System.Drawing.Size(204, 21);
            this.cmbParentadd.TabIndex = 3;
            this.cmbParentadd.SelectedIndexChanged += new System.EventHandler(this.cmbParentadd_SelectedIndexChanged);
            // 
            // lblparentadd
            // 
            this.lblparentadd.Location = new System.Drawing.Point(176, 61);
            this.lblparentadd.Name = "lblparentadd";
            this.lblparentadd.Size = new System.Drawing.Size(44, 13);
            this.lblparentadd.TabIndex = 22;
            this.lblparentadd.Text = "Parent";
            this.lblparentadd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblmenunameadd
            // 
            this.lblmenunameadd.Location = new System.Drawing.Point(146, 97);
            this.lblmenunameadd.Name = "lblmenunameadd";
            this.lblmenunameadd.Size = new System.Drawing.Size(74, 13);
            this.lblmenunameadd.TabIndex = 24;
            this.lblmenunameadd.Text = "Menu Name";
            this.lblmenunameadd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // menunameadd
            // 
            this.menunameadd.Location = new System.Drawing.Point(225, 94);
            this.menunameadd.Name = "menunameadd";
            this.menunameadd.Size = new System.Drawing.Size(204, 21);
            this.menunameadd.TabIndex = 5;
            // 
            // seqnoadd
            // 
            this.seqnoadd.Location = new System.Drawing.Point(225, 132);
            this.seqnoadd.Name = "seqnoadd";
            this.seqnoadd.Size = new System.Drawing.Size(123, 21);
            this.seqnoadd.TabIndex = 7;
            this.seqnoadd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(138, 135);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(82, 13);
            this.label13.TabIndex = 26;
            this.label13.Text = "Sequence No";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dgvApplocmodfunc
            // 
            this.dgvApplocmodfunc.AllowUserToAddRows = false;
            this.dgvApplocmodfunc.AllowUserToDeleteRows = false;
            this.dgvApplocmodfunc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvApplocmodfunc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvApplocmodfunc.Location = new System.Drawing.Point(0, 0);
            this.dgvApplocmodfunc.MultiSelect = false;
            this.dgvApplocmodfunc.Name = "dgvApplocmodfunc";
            this.dgvApplocmodfunc.RowHeadersVisible = false;
            this.dgvApplocmodfunc.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvApplocmodfunc.Size = new System.Drawing.Size(989, 445);
            this.dgvApplocmodfunc.TabIndex = 0;
            this.dgvApplocmodfunc.TabStop = false;
            this.dgvApplocmodfunc.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvApplocmodfunc_CellDoubleClick);
            this.dgvApplocmodfunc.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvApplocmodfunc_CellContentClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblapptype);
            this.groupBox1.Controls.Add(this.btnSearchReset);
            this.groupBox1.Controls.Add(this.lblloctype);
            this.groupBox1.Controls.Add(this.cmbloctype);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Controls.Add(this.cmbmodule);
            this.groupBox1.Controls.Add(this.lblmoduletype);
            this.groupBox1.Controls.Add(this.cmbapptype);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1003, 126);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Controls.SetChildIndex(this.cmbapptype, 0);
            this.groupBox1.Controls.SetChildIndex(this.lblmoduletype, 0);
            this.groupBox1.Controls.SetChildIndex(this.cmbmodule, 0);
            this.groupBox1.Controls.SetChildIndex(this.btnSearch, 0);
            this.groupBox1.Controls.SetChildIndex(this.cmbloctype, 0);
            this.groupBox1.Controls.SetChildIndex(this.lblloctype, 0);
            this.groupBox1.Controls.SetChildIndex(this.btnSearchReset, 0);
            this.groupBox1.Controls.SetChildIndex(this.lblapptype, 0);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmbloctypeadd);
            this.groupBox2.Controls.Add(this.listBoxfunctionsadd);
            this.groupBox2.Controls.Add(this.menunameadd);
            this.groupBox2.Controls.Add(this.cmbstatusadd);
            this.groupBox2.Controls.Add(this.seqnoadd);
            this.groupBox2.Controls.Add(this.lblstatusadd);
            this.groupBox2.Controls.Add(this.cmbmoduleadd);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.cmbParentadd);
            this.groupBox2.Controls.Add(this.btnClearDetails);
            this.groupBox2.Controls.Add(this.cmbapptypeadd);
            this.groupBox2.Controls.Add(this.btnAddDetails);
            this.groupBox2.Controls.Add(this.lblfunctionsadd);
            this.groupBox2.Controls.Add(this.lblloctypeadd);
            this.groupBox2.Controls.Add(this.lblapptypeadd);
            this.groupBox2.Controls.Add(this.lblparentadd);
            this.groupBox2.Controls.Add(this.lblmenunameadd);
            this.groupBox2.Controls.Add(this.lblmoduleadd);
            this.groupBox2.Location = new System.Drawing.Point(5, -3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(950, 263);
            this.groupBox2.TabIndex = 28;
            this.groupBox2.TabStop = false;
            this.groupBox2.Controls.SetChildIndex(this.lblmoduleadd, 0);
            this.groupBox2.Controls.SetChildIndex(this.lblmenunameadd, 0);
            this.groupBox2.Controls.SetChildIndex(this.lblparentadd, 0);
            this.groupBox2.Controls.SetChildIndex(this.lblapptypeadd, 0);
            this.groupBox2.Controls.SetChildIndex(this.lblloctypeadd, 0);
            this.groupBox2.Controls.SetChildIndex(this.lblfunctionsadd, 0);
            this.groupBox2.Controls.SetChildIndex(this.btnAddDetails, 0);
            this.groupBox2.Controls.SetChildIndex(this.cmbapptypeadd, 0);
            this.groupBox2.Controls.SetChildIndex(this.btnClearDetails, 0);
            this.groupBox2.Controls.SetChildIndex(this.cmbParentadd, 0);
            this.groupBox2.Controls.SetChildIndex(this.label13, 0);
            this.groupBox2.Controls.SetChildIndex(this.cmbmoduleadd, 0);
            this.groupBox2.Controls.SetChildIndex(this.lblstatusadd, 0);
            this.groupBox2.Controls.SetChildIndex(this.seqnoadd, 0);
            this.groupBox2.Controls.SetChildIndex(this.cmbstatusadd, 0);
            this.groupBox2.Controls.SetChildIndex(this.menunameadd, 0);
            this.groupBox2.Controls.SetChildIndex(this.listBoxfunctionsadd, 0);
            this.groupBox2.Controls.SetChildIndex(this.cmbloctypeadd, 0);
            // 
            // cmbloctypeadd
            // 
            this.cmbloctypeadd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbloctypeadd.FormattingEnabled = true;
            this.cmbloctypeadd.Location = new System.Drawing.Point(541, 20);
            this.cmbloctypeadd.Name = "cmbloctypeadd";
            this.cmbloctypeadd.Size = new System.Drawing.Size(203, 21);
            this.cmbloctypeadd.TabIndex = 2;
            // 
            // listBoxfunctionsadd
            // 
            this.listBoxfunctionsadd.CheckOnClick = true;
            this.listBoxfunctionsadd.FormattingEnabled = true;
            this.listBoxfunctionsadd.Location = new System.Drawing.Point(541, 97);
            this.listBoxfunctionsadd.Name = "listBoxfunctionsadd";
            this.listBoxfunctionsadd.Size = new System.Drawing.Size(203, 132);
            this.listBoxfunctionsadd.TabIndex = 6;
            // 
            // groupBox4
            // 
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(0, 0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(989, 445);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            // 
            // frmAppLocModLink
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1011, 707);
            this.Name = "frmAppLocModLink";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Create Menu";
            this.WindowState = System.Windows.Forms.FormWindowState.Normal;
            this.Load += new System.EventHandler(this.frmAppLocModLink_Load);
            this.pnlCreateHeader.ResumeLayout(false);
            this.grpAddDetails.ResumeLayout(false);
            this.pnlSearchHeader.ResumeLayout(false);
            this.pnlSearchGrid.ResumeLayout(false);
            this.pnlCreateDetail.ResumeLayout(false);
            this.tabSearch.ResumeLayout(false);
            this.tabCreate.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errprovadd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvApplocmodfunc)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbmodule;
        private System.Windows.Forms.ComboBox cmbloctype;
        private System.Windows.Forms.ComboBox cmbapptype;
        private System.Windows.Forms.Label lblmoduletype;
        private System.Windows.Forms.Label lblloctype;
        private System.Windows.Forms.Label lblapptype;
        private System.Windows.Forms.ComboBox cmbstatusadd;
        private System.Windows.Forms.ComboBox cmbmoduleadd;
        private System.Windows.Forms.ComboBox cmbapptypeadd;
        private System.Windows.Forms.Label lblstatusadd;
        private System.Windows.Forms.Label lblfunctionsadd;
        private System.Windows.Forms.Label lblmoduleadd;
        private System.Windows.Forms.Label lblloctypeadd;
        private System.Windows.Forms.Label lblapptypeadd;
        private System.Windows.Forms.ErrorProvider errprovadd;
        private System.Windows.Forms.ComboBox cmbParentadd;
        private System.Windows.Forms.Label lblparentadd;
        private System.Windows.Forms.TextBox menunameadd;
        private System.Windows.Forms.Label lblmenunameadd;
        private System.Windows.Forms.TextBox seqnoadd;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.DataGridView dgvApplocmodfunc;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox cmbloctypeadd;
        private System.Windows.Forms.CheckedListBox listBoxfunctionsadd;

    }
}