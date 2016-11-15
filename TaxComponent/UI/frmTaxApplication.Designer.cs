namespace TaxComponent.UI
{
    partial class frmTaxApplication
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
            this.lblTaxCategory = new System.Windows.Forms.Label();
            this.cmbTaxCategory = new System.Windows.Forms.ComboBox();
            this.cmbTaxType = new System.Windows.Forms.ComboBox();
            this.lblTaxType = new System.Windows.Forms.Label();
            this.cmbTaxGroup = new System.Windows.Forms.ComboBox();
            this.lblTaxGroup = new System.Windows.Forms.Label();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cmbGoodsDirection = new System.Windows.Forms.ComboBox();
            this.lblGoodsDirection = new System.Windows.Forms.Label();
            this.lblState = new System.Windows.Forms.Label();
            this.errTax = new System.Windows.Forms.ErrorProvider(this.components);
            this.dgvTax = new System.Windows.Forms.DataGridView();
            this.lblTaxAuthority = new System.Windows.Forms.Label();
            this.txtAuthority = new System.Windows.Forms.TextBox();
            this.chkStateList = new System.Windows.Forms.CheckedListBox();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.chkFormCTax = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlGridSearch.SuspendLayout();
            this.pnlSearchHeader.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errTax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTax)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlGridSearch
            // 
            this.pnlGridSearch.Controls.Add(this.dgvTax);
            this.pnlGridSearch.Location = new System.Drawing.Point(0, 208);
            this.pnlGridSearch.Size = new System.Drawing.Size(1007, 441);
            // 
            // pnlSearchHeader
            // 
            this.pnlSearchHeader.Controls.Add(this.chkFormCTax);
            this.pnlSearchHeader.Controls.Add(this.dtpStartDate);
            this.pnlSearchHeader.Controls.Add(this.label1);
            this.pnlSearchHeader.Controls.Add(this.lblStartDate);
            this.pnlSearchHeader.Controls.Add(this.chkStateList);
            this.pnlSearchHeader.Controls.Add(this.txtAuthority);
            this.pnlSearchHeader.Controls.Add(this.lblTaxAuthority);
            this.pnlSearchHeader.Controls.Add(this.cmbStatus);
            this.pnlSearchHeader.Controls.Add(this.lblStatus);
            this.pnlSearchHeader.Controls.Add(this.cmbGoodsDirection);
            this.pnlSearchHeader.Controls.Add(this.lblGoodsDirection);
            this.pnlSearchHeader.Controls.Add(this.cmbTaxGroup);
            this.pnlSearchHeader.Controls.Add(this.lblState);
            this.pnlSearchHeader.Controls.Add(this.lblTaxGroup);
            this.pnlSearchHeader.Controls.Add(this.cmbTaxType);
            this.pnlSearchHeader.Controls.Add(this.lblTaxType);
            this.pnlSearchHeader.Controls.Add(this.cmbTaxCategory);
            this.pnlSearchHeader.Controls.Add(this.lblTaxCategory);
            this.pnlSearchHeader.Size = new System.Drawing.Size(1007, 180);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblTaxCategory, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.cmbTaxCategory, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblTaxType, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.cmbTaxType, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblTaxGroup, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblState, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.cmbTaxGroup, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblGoodsDirection, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.cmbGoodsDirection, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblStatus, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.cmbStatus, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblTaxAuthority, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.txtAuthority, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.chkStateList, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblStartDate, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.label1, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.dtpStartDate, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.chkFormCTax, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.pnlButtons, 0);
            // 
            // lblSearchResult
            // 
            this.lblSearchResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            // 
            // btnSave
            // 
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSave.TabIndex = 11;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearch.TabIndex = 10;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnReset
            // 
            this.btnReset.FlatAppearance.BorderSize = 0;
            this.btnReset.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnReset.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnReset.TabIndex = 12;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // pnlButtons
            // 
            this.pnlButtons.Location = new System.Drawing.Point(0, 146);
            // 
            // lblTaxCategory
            // 
            this.lblTaxCategory.AutoSize = true;
            this.lblTaxCategory.Location = new System.Drawing.Point(50, 15);
            this.lblTaxCategory.Name = "lblTaxCategory";
            this.lblTaxCategory.Size = new System.Drawing.Size(97, 13);
            this.lblTaxCategory.TabIndex = 0;
            this.lblTaxCategory.Text = "Tax Category:*";
            // 
            // cmbTaxCategory
            // 
            this.cmbTaxCategory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.cmbTaxCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTaxCategory.FormattingEnabled = true;
            this.cmbTaxCategory.Location = new System.Drawing.Point(145, 12);
            this.cmbTaxCategory.Name = "cmbTaxCategory";
            this.cmbTaxCategory.Size = new System.Drawing.Size(150, 21);
            this.cmbTaxCategory.TabIndex = 1;
            this.cmbTaxCategory.SelectedIndexChanged += new System.EventHandler(this.cmbTaxCategory_SelectedIndexChanged);
            // 
            // cmbTaxType
            // 
            this.cmbTaxType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.cmbTaxType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTaxType.FormattingEnabled = true;
            this.cmbTaxType.Location = new System.Drawing.Point(493, 12);
            this.cmbTaxType.Name = "cmbTaxType";
            this.cmbTaxType.Size = new System.Drawing.Size(150, 21);
            this.cmbTaxType.TabIndex = 2;
            this.cmbTaxType.SelectedIndexChanged += new System.EventHandler(this.cmbTaxType_SelectedIndexChanged);
            // 
            // lblTaxType
            // 
            this.lblTaxType.AutoSize = true;
            this.lblTaxType.Location = new System.Drawing.Point(422, 15);
            this.lblTaxType.Name = "lblTaxType";
            this.lblTaxType.Size = new System.Drawing.Size(72, 13);
            this.lblTaxType.TabIndex = 2;
            this.lblTaxType.Text = "Tax Type:*";
            // 
            // cmbTaxGroup
            // 
            this.cmbTaxGroup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.cmbTaxGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTaxGroup.FormattingEnabled = true;
            this.cmbTaxGroup.Location = new System.Drawing.Point(808, 12);
            this.cmbTaxGroup.Name = "cmbTaxGroup";
            this.cmbTaxGroup.Size = new System.Drawing.Size(150, 21);
            this.cmbTaxGroup.TabIndex = 3;
            this.cmbTaxGroup.SelectedIndexChanged += new System.EventHandler(this.cmbTaxGroup_SelectedIndexChanged);
            // 
            // lblTaxGroup
            // 
            this.lblTaxGroup.AutoSize = true;
            this.lblTaxGroup.Location = new System.Drawing.Point(731, 16);
            this.lblTaxGroup.Name = "lblTaxGroup";
            this.lblTaxGroup.Size = new System.Drawing.Size(79, 13);
            this.lblTaxGroup.TabIndex = 4;
            this.lblTaxGroup.Text = "Tax Group:*";
            // 
            // cmbStatus
            // 
            this.cmbStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Location = new System.Drawing.Point(493, 88);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(150, 21);
            this.cmbStatus.TabIndex = 8;
            this.cmbStatus.SelectedIndexChanged += new System.EventHandler(this.cmbStatus_SelectedIndexChanged);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(439, 92);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(55, 13);
            this.lblStatus.TabIndex = 10;
            this.lblStatus.Text = "Status:*";
            // 
            // cmbGoodsDirection
            // 
            this.cmbGoodsDirection.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.cmbGoodsDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGoodsDirection.FormattingEnabled = true;
            this.cmbGoodsDirection.Location = new System.Drawing.Point(145, 50);
            this.cmbGoodsDirection.Name = "cmbGoodsDirection";
            this.cmbGoodsDirection.Size = new System.Drawing.Size(150, 21);
            this.cmbGoodsDirection.TabIndex = 4;
            this.cmbGoodsDirection.SelectedIndexChanged += new System.EventHandler(this.cmbGoodsDirection_SelectedIndexChanged);
            // 
            // lblGoodsDirection
            // 
            this.lblGoodsDirection.AutoSize = true;
            this.lblGoodsDirection.Location = new System.Drawing.Point(37, 53);
            this.lblGoodsDirection.Name = "lblGoodsDirection";
            this.lblGoodsDirection.Size = new System.Drawing.Size(110, 13);
            this.lblGoodsDirection.TabIndex = 8;
            this.lblGoodsDirection.Text = "Goods Direction:*";
            // 
            // lblState
            // 
            this.lblState.AutoSize = true;
            this.lblState.Location = new System.Drawing.Point(737, 53);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(73, 13);
            this.lblState.TabIndex = 6;
            this.lblState.Text = "Tax Zone:*";
            // 
            // errTax
            // 
            this.errTax.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errTax.ContainerControl = this;
            // 
            // dgvTax
            // 
            this.dgvTax.AllowUserToAddRows = false;
            this.dgvTax.AllowUserToDeleteRows = false;
            this.dgvTax.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTax.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTax.Location = new System.Drawing.Point(0, 0);
            this.dgvTax.MultiSelect = false;
            this.dgvTax.Name = "dgvTax";
            this.dgvTax.ReadOnly = true;
            this.dgvTax.RowHeadersVisible = false;
            this.dgvTax.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTax.Size = new System.Drawing.Size(1007, 441);
            this.dgvTax.TabIndex = 0;
            this.dgvTax.TabStop = false;
            this.dgvTax.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvTax_CellMouseClick);
            this.dgvTax.SelectionChanged += new System.EventHandler(this.dgvTax_SelectionChanged);
            // 
            // lblTaxAuthority
            // 
            this.lblTaxAuthority.AutoSize = true;
            this.lblTaxAuthority.Location = new System.Drawing.Point(400, 53);
            this.lblTaxAuthority.Name = "lblTaxAuthority";
            this.lblTaxAuthority.Size = new System.Drawing.Size(89, 13);
            this.lblTaxAuthority.TabIndex = 11;
            this.lblTaxAuthority.Text = "Tax Authority:";
            // 
            // txtAuthority
            // 
            this.txtAuthority.Location = new System.Drawing.Point(493, 50);
            this.txtAuthority.MaxLength = 100;
            this.txtAuthority.Name = "txtAuthority";
            this.txtAuthority.Size = new System.Drawing.Size(150, 21);
            this.txtAuthority.TabIndex = 5;
            // 
            // chkStateList
            // 
            this.chkStateList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.chkStateList.CheckOnClick = true;
            this.chkStateList.FormattingEnabled = true;
            this.chkStateList.Location = new System.Drawing.Point(807, 50);
            this.chkStateList.Name = "chkStateList";
            this.chkStateList.Size = new System.Drawing.Size(150, 84);
            this.chkStateList.TabIndex = 6;
            this.chkStateList.SelectedIndexChanged += new System.EventHandler(this.chkLstTaxJurisdiction_SelectedIndexChanged);
            this.chkStateList.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chkLstTaxJurisdiction_ItemCheck);
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Checked = false;
            this.dtpStartDate.CustomFormat = "dd-MM-yyyy";
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartDate.Location = new System.Drawing.Point(144, 87);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.ShowCheckBox = true;
            this.dtpStartDate.Size = new System.Drawing.Size(150, 21);
            this.dtpStartDate.TabIndex = 7;
            this.dtpStartDate.Value = new System.DateTime(2009, 7, 16, 0, 0, 0, 0);
            this.dtpStartDate.Validated += new System.EventHandler(this.dtpStartDate_Validated);
            // 
            // lblStartDate
            // 
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.Location = new System.Drawing.Point(68, 91);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(78, 13);
            this.lblStartDate.TabIndex = 70;
            this.lblStartDate.Text = "Start Date:*";
            // 
            // chkFormCTax
            // 
            this.chkFormCTax.AutoSize = true;
            this.chkFormCTax.Location = new System.Drawing.Point(144, 122);
            this.chkFormCTax.Name = "chkFormCTax";
            this.chkFormCTax.Size = new System.Drawing.Size(15, 14);
            this.chkFormCTax.TabIndex = 71;
            this.chkFormCTax.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 123);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 13);
            this.label1.TabIndex = 70;
            this.label1.Text = "Is Form C Tax:*";
            // 
            // frmTaxApplication
            // 
            this.AcceptButton = this.btnSearch;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1013, 703);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "frmTaxApplication";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Normal;
            this.pnlGridSearch.ResumeLayout(false);
            this.pnlSearchHeader.ResumeLayout(false);
            this.pnlSearchHeader.PerformLayout();
            this.pnlButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errTax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTax)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbTaxCategory;
        private System.Windows.Forms.Label lblTaxCategory;
        private System.Windows.Forms.ComboBox cmbTaxGroup;
        private System.Windows.Forms.Label lblTaxGroup;
        private System.Windows.Forms.ComboBox cmbTaxType;
        private System.Windows.Forms.Label lblTaxType;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cmbGoodsDirection;
        private System.Windows.Forms.Label lblGoodsDirection;
        private System.Windows.Forms.Label lblState;
        private System.Windows.Forms.ErrorProvider errTax;
        private System.Windows.Forms.DataGridView dgvTax;
        private System.Windows.Forms.Label lblTaxAuthority;
        private System.Windows.Forms.TextBox txtAuthority;
        private System.Windows.Forms.CheckedListBox chkStateList;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.CheckBox chkFormCTax;
        private System.Windows.Forms.Label label1;
    }
}

