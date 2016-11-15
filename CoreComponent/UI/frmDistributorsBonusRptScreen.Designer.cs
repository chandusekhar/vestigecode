namespace CoreComponent.UI
{
    partial class frmDistributorsBonusRptScreen
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
            this.txtDistributorId = new System.Windows.Forms.TextBox();
            this.lblDistributorId = new System.Windows.Forms.Label();
            this.lblCountry = new System.Windows.Forms.Label();
            this.cmbCountry = new System.Windows.Forms.ComboBox();
            this.dtpBusinessMonth = new System.Windows.Forms.DateTimePicker();
            this.txtBonusPercent = new System.Windows.Forms.TextBox();
            this.cmbState = new System.Windows.Forms.ComboBox();
            this.lblBusinessmonth = new System.Windows.Forms.Label();
            this.lblLevel = new System.Windows.Forms.Label();
            this.lblState = new System.Windows.Forms.Label();
            this.lblDistributorLevel = new System.Windows.Forms.Label();
            this.cmbDistributorLevel = new System.Windows.Forms.ComboBox();
            this.chkPB = new System.Windows.Forms.CheckBox();
            this.chkDB = new System.Windows.Forms.CheckBox();
            this.chkCF = new System.Windows.Forms.CheckBox();
            this.chkHF = new System.Windows.Forms.CheckBox();
            this.chkTF = new System.Windows.Forms.CheckBox();
            this.chkLOB = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnPrint = new System.Windows.Forms.Button();
            this.chkPrintAll = new System.Windows.Forms.CheckBox();
            this.dgvDistriBonusSearch = new System.Windows.Forms.DataGridView();
            this.errorProviderDistBonusRept = new System.Windows.Forms.ErrorProvider(this.components);
            this.lblCity = new System.Windows.Forms.Label();
            this.cmbCity = new System.Windows.Forms.ComboBox();
            this.btnExportReport = new System.Windows.Forms.Button();
            this.pnlGridSearch.SuspendLayout();
            this.pnlSearchHeader.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDistriBonusSearch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderDistBonusRept)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlGridSearch
            // 
            this.pnlGridSearch.Controls.Add(this.btnExportReport);
            this.pnlGridSearch.Controls.Add(this.dgvDistriBonusSearch);
            this.pnlGridSearch.Controls.Add(this.chkPrintAll);
            this.pnlGridSearch.Controls.Add(this.btnPrint);
            // 
            // pnlSearchHeader
            // 
            this.pnlSearchHeader.Controls.Add(this.groupBox1);
            this.pnlSearchHeader.Controls.Add(this.lblDistributorLevel);
            this.pnlSearchHeader.Controls.Add(this.cmbDistributorLevel);
            this.pnlSearchHeader.Controls.Add(this.lblCountry);
            this.pnlSearchHeader.Controls.Add(this.cmbCountry);
            this.pnlSearchHeader.Controls.Add(this.dtpBusinessMonth);
            this.pnlSearchHeader.Controls.Add(this.txtBonusPercent);
            this.pnlSearchHeader.Controls.Add(this.cmbCity);
            this.pnlSearchHeader.Controls.Add(this.cmbState);
            this.pnlSearchHeader.Controls.Add(this.lblBusinessmonth);
            this.pnlSearchHeader.Controls.Add(this.lblCity);
            this.pnlSearchHeader.Controls.Add(this.lblLevel);
            this.pnlSearchHeader.Controls.Add(this.lblState);
            this.pnlSearchHeader.Controls.Add(this.txtDistributorId);
            this.pnlSearchHeader.Controls.Add(this.lblDistributorId);
            this.pnlSearchHeader.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlSearchHeader_Paint);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblDistributorId, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.txtDistributorId, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblState, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblLevel, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblCity, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblBusinessmonth, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.cmbState, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.cmbCity, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.txtBonusPercent, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.dtpBusinessMonth, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.cmbCountry, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblCountry, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.pnlButtons, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.cmbDistributorLevel, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblDistributorLevel, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.groupBox1, 0);
            // 
            // btnSave
            // 
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSave.TabIndex = 18;
            // 
            // btnSearch
            // 
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearch.TabIndex = 7;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnReset
            // 
            this.btnReset.FlatAppearance.BorderSize = 0;
            this.btnReset.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnReset.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnReset.TabIndex = 8;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // txtDistributorId
            // 
            this.txtDistributorId.Location = new System.Drawing.Point(380, 57);
            this.txtDistributorId.MaxLength = 8;
            this.txtDistributorId.Name = "txtDistributorId";
            this.txtDistributorId.Size = new System.Drawing.Size(121, 20);
            this.txtDistributorId.TabIndex = 5;
            this.txtDistributorId.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDistributorId_KeyPress);
            // 
            // lblDistributorId
            // 
            this.lblDistributorId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDistributorId.AutoSize = true;
            this.lblDistributorId.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.lblDistributorId.Location = new System.Drawing.Point(286, 60);
            this.lblDistributorId.Name = "lblDistributorId";
            this.lblDistributorId.Size = new System.Drawing.Size(88, 13);
            this.lblDistributorId.TabIndex = 10;
            this.lblDistributorId.Text = "&DistributorId :";
            // 
            // lblCountry
            // 
            this.lblCountry.AutoSize = true;
            this.lblCountry.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.lblCountry.Location = new System.Drawing.Point(312, 26);
            this.lblCountry.Name = "lblCountry";
            this.lblCountry.Size = new System.Drawing.Size(62, 13);
            this.lblCountry.TabIndex = 21;
            this.lblCountry.Text = "&Country :";
            // 
            // cmbCountry
            // 
            this.cmbCountry.FormattingEnabled = true;
            this.cmbCountry.Location = new System.Drawing.Point(380, 20);
            this.cmbCountry.Name = "cmbCountry";
            this.cmbCountry.Size = new System.Drawing.Size(121, 21);
            this.cmbCountry.TabIndex = 2;
            this.cmbCountry.SelectedIndexChanged += new System.EventHandler(this.cmbCountry_SelectedIndexChanged);
            // 
            // dtpBusinessMonth
            // 
            this.dtpBusinessMonth.CustomFormat = "MM-yyyy";
            this.dtpBusinessMonth.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpBusinessMonth.Location = new System.Drawing.Point(115, 24);
            this.dtpBusinessMonth.Name = "dtpBusinessMonth";
            this.dtpBusinessMonth.Size = new System.Drawing.Size(121, 20);
            this.dtpBusinessMonth.TabIndex = 1;
            this.dtpBusinessMonth.Value = new System.DateTime(2012, 2, 1, 0, 0, 0, 0);
            // 
            // txtBonusPercent
            // 
            this.txtBonusPercent.Location = new System.Drawing.Point(655, 57);
            this.txtBonusPercent.MaxLength = 2;
            this.txtBonusPercent.Name = "txtBonusPercent";
            this.txtBonusPercent.Size = new System.Drawing.Size(121, 20);
            this.txtBonusPercent.TabIndex = 6;
            // 
            // cmbState
            // 
            this.cmbState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbState.FormattingEnabled = true;
            this.cmbState.Location = new System.Drawing.Point(655, 23);
            this.cmbState.Name = "cmbState";
            this.cmbState.Size = new System.Drawing.Size(121, 21);
            this.cmbState.TabIndex = 3;
            this.cmbState.SelectedIndexChanged += new System.EventHandler(this.cmbState_SelectedIndexChanged);
            // 
            // lblBusinessmonth
            // 
            this.lblBusinessmonth.AutoSize = true;
            this.lblBusinessmonth.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.lblBusinessmonth.Location = new System.Drawing.Point(9, 28);
            this.lblBusinessmonth.Name = "lblBusinessmonth";
            this.lblBusinessmonth.Size = new System.Drawing.Size(104, 13);
            this.lblBusinessmonth.TabIndex = 16;
            this.lblBusinessmonth.Text = "&BusinessMonth : ";
            // 
            // lblLevel
            // 
            this.lblLevel.AutoSize = true;
            this.lblLevel.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.lblLevel.Location = new System.Drawing.Point(557, 63);
            this.lblLevel.Name = "lblLevel";
            this.lblLevel.Size = new System.Drawing.Size(93, 13);
            this.lblLevel.TabIndex = 15;
            this.lblLevel.Text = "Percent &Level :";
            // 
            // lblState
            // 
            this.lblState.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblState.AutoSize = true;
            this.lblState.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.lblState.Location = new System.Drawing.Point(601, 26);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(46, 13);
            this.lblState.TabIndex = 14;
            this.lblState.Text = "&State :";
            // 
            // lblDistributorLevel
            // 
            this.lblDistributorLevel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDistributorLevel.AutoSize = true;
            this.lblDistributorLevel.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.lblDistributorLevel.Location = new System.Drawing.Point(0, 102);
            this.lblDistributorLevel.Name = "lblDistributorLevel";
            this.lblDistributorLevel.Size = new System.Drawing.Size(110, 13);
            this.lblDistributorLevel.TabIndex = 23;
            this.lblDistributorLevel.Text = "&Distributor Level :";
            // 
            // cmbDistributorLevel
            // 
            this.cmbDistributorLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDistributorLevel.FormattingEnabled = true;
            this.cmbDistributorLevel.Location = new System.Drawing.Point(115, 96);
            this.cmbDistributorLevel.Name = "cmbDistributorLevel";
            this.cmbDistributorLevel.Size = new System.Drawing.Size(121, 21);
            this.cmbDistributorLevel.TabIndex = 7;
            this.cmbDistributorLevel.SelectedIndexChanged += new System.EventHandler(this.cmbBonusPercent_SelectedIndexChanged);
            // 
            // chkPB
            // 
            this.chkPB.AutoSize = true;
            this.chkPB.Checked = true;
            this.chkPB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPB.Location = new System.Drawing.Point(16, 13);
            this.chkPB.Name = "chkPB";
            this.chkPB.Size = new System.Drawing.Size(40, 17);
            this.chkPB.TabIndex = 8;
            this.chkPB.Text = "PB";
            this.chkPB.UseVisualStyleBackColor = true;
            this.chkPB.CheckedChanged += new System.EventHandler(this.chkPB_CheckedChanged);
            // 
            // chkDB
            // 
            this.chkDB.AutoSize = true;
            this.chkDB.Checked = true;
            this.chkDB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDB.Location = new System.Drawing.Point(78, 13);
            this.chkDB.Name = "chkDB";
            this.chkDB.Size = new System.Drawing.Size(41, 17);
            this.chkDB.TabIndex = 9;
            this.chkDB.Text = "DB";
            this.chkDB.UseVisualStyleBackColor = true;
            this.chkDB.CheckedChanged += new System.EventHandler(this.chkDB_CheckedChanged);
            // 
            // chkCF
            // 
            this.chkCF.AutoSize = true;
            this.chkCF.Checked = true;
            this.chkCF.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCF.Location = new System.Drawing.Point(137, 13);
            this.chkCF.Name = "chkCF";
            this.chkCF.Size = new System.Drawing.Size(39, 17);
            this.chkCF.TabIndex = 10;
            this.chkCF.Text = "CF";
            this.chkCF.UseVisualStyleBackColor = true;
            this.chkCF.CheckedChanged += new System.EventHandler(this.chkCF_CheckedChanged);
            // 
            // chkHF
            // 
            this.chkHF.AutoSize = true;
            this.chkHF.Checked = true;
            this.chkHF.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkHF.Location = new System.Drawing.Point(193, 13);
            this.chkHF.Name = "chkHF";
            this.chkHF.Size = new System.Drawing.Size(40, 17);
            this.chkHF.TabIndex = 11;
            this.chkHF.Text = "HF";
            this.chkHF.UseVisualStyleBackColor = true;
            this.chkHF.CheckedChanged += new System.EventHandler(this.chkHF_CheckedChanged);
            // 
            // chkTF
            // 
            this.chkTF.AutoSize = true;
            this.chkTF.Checked = true;
            this.chkTF.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTF.Location = new System.Drawing.Point(250, 13);
            this.chkTF.Name = "chkTF";
            this.chkTF.Size = new System.Drawing.Size(39, 17);
            this.chkTF.TabIndex = 12;
            this.chkTF.Text = "TF";
            this.chkTF.UseVisualStyleBackColor = true;
            this.chkTF.CheckedChanged += new System.EventHandler(this.chkTF_CheckedChanged);
            // 
            // chkLOB
            // 
            this.chkLOB.AutoSize = true;
            this.chkLOB.Checked = true;
            this.chkLOB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkLOB.Location = new System.Drawing.Point(299, 13);
            this.chkLOB.Name = "chkLOB";
            this.chkLOB.Size = new System.Drawing.Size(47, 17);
            this.chkLOB.TabIndex = 13;
            this.chkLOB.Text = "LOB";
            this.chkLOB.UseVisualStyleBackColor = true;
            this.chkLOB.CheckedChanged += new System.EventHandler(this.chkLOB_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkLOB);
            this.groupBox1.Controls.Add(this.chkPB);
            this.groupBox1.Controls.Add(this.chkDB);
            this.groupBox1.Controls.Add(this.chkCF);
            this.groupBox1.Controls.Add(this.chkHF);
            this.groupBox1.Controls.Add(this.chkTF);
            this.groupBox1.Location = new System.Drawing.Point(294, 88);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(365, 40);
            this.groupBox1.TabIndex = 30;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Bonus Types";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnPrint.Location = new System.Drawing.Point(600, 406);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 23);
            this.btnPrint.TabIndex = 10;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // chkPrintAll
            // 
            this.chkPrintAll.AutoSize = true;
            this.chkPrintAll.Location = new System.Drawing.Point(25, 414);
            this.chkPrintAll.Name = "chkPrintAll";
            this.chkPrintAll.Size = new System.Drawing.Size(70, 17);
            this.chkPrintAll.TabIndex = 9;
            this.chkPrintAll.Text = "Select All";
            this.chkPrintAll.UseVisualStyleBackColor = true;
            this.chkPrintAll.CheckedChanged += new System.EventHandler(this.chkPrintAll_CheckedChanged);
            // 
            // dgvDistriBonusSearch
            // 
            this.dgvDistriBonusSearch.AllowUserToAddRows = false;
            this.dgvDistriBonusSearch.AllowUserToDeleteRows = false;
            this.dgvDistriBonusSearch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDistriBonusSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgvDistriBonusSearch.Location = new System.Drawing.Point(0, 0);
            this.dgvDistriBonusSearch.MultiSelect = false;
            this.dgvDistriBonusSearch.Name = "dgvDistriBonusSearch";
            this.dgvDistriBonusSearch.RowHeadersVisible = false;
            this.dgvDistriBonusSearch.Size = new System.Drawing.Size(1007, 400);
            this.dgvDistriBonusSearch.TabIndex = 34;
            this.dgvDistriBonusSearch.TabStop = false;
            this.dgvDistriBonusSearch.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDistriBonusSearch_CellContentClick_1);
            // 
            // errorProviderDistBonusRept
            // 
            this.errorProviderDistBonusRept.ContainerControl = this;
            // 
            // lblCity
            // 
            this.lblCity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCity.AutoSize = true;
            this.lblCity.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.lblCity.Location = new System.Drawing.Point(61, 63);
            this.lblCity.Name = "lblCity";
            this.lblCity.Size = new System.Drawing.Size(39, 13);
            this.lblCity.TabIndex = 14;
            this.lblCity.Text = "&City :";
            // 
            // cmbCity
            // 
            this.cmbCity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCity.FormattingEnabled = true;
            this.cmbCity.Location = new System.Drawing.Point(115, 60);
            this.cmbCity.Name = "cmbCity";
            this.cmbCity.Size = new System.Drawing.Size(121, 21);
            this.cmbCity.TabIndex = 4;
            // 
            // btnExportReport
            // 
            this.btnExportReport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnExportReport.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnExportReport.Location = new System.Drawing.Point(691, 406);
            this.btnExportReport.Name = "btnExportReport";
            this.btnExportReport.Size = new System.Drawing.Size(125, 23);
            this.btnExportReport.TabIndex = 35;
            this.btnExportReport.Text = "Export into PDF";
            this.btnExportReport.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnExportReport.UseVisualStyleBackColor = false;
            this.btnExportReport.Click += new System.EventHandler(this.btnExportReport_Click);
            // 
            // frmDistributorsBonusRptScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1013, 703);
            this.Location = new System.Drawing.Point(0, 0);
            this.MaximumSize = new System.Drawing.Size(1013, 703);
            this.Name = "frmDistributorsBonusRptScreen";
            this.pnlGridSearch.ResumeLayout(false);
            this.pnlGridSearch.PerformLayout();
            this.pnlSearchHeader.ResumeLayout(false);
            this.pnlSearchHeader.PerformLayout();
            this.pnlButtons.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDistriBonusSearch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderDistBonusRept)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtDistributorId;
        private System.Windows.Forms.Label lblDistributorId;
        private System.Windows.Forms.Label lblCountry;
        private System.Windows.Forms.ComboBox cmbCountry;
        private System.Windows.Forms.DateTimePicker dtpBusinessMonth;
        private System.Windows.Forms.TextBox txtBonusPercent;
        private System.Windows.Forms.ComboBox cmbState;
        private System.Windows.Forms.Label lblBusinessmonth;
        private System.Windows.Forms.Label lblLevel;
        private System.Windows.Forms.Label lblState;
        private System.Windows.Forms.Label lblDistributorLevel;
        private System.Windows.Forms.ComboBox cmbDistributorLevel;
        private System.Windows.Forms.CheckBox chkLOB;
        private System.Windows.Forms.CheckBox chkTF;
        private System.Windows.Forms.CheckBox chkHF;
        private System.Windows.Forms.CheckBox chkCF;
        private System.Windows.Forms.CheckBox chkDB;
        private System.Windows.Forms.CheckBox chkPB;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.CheckBox chkPrintAll;
        private System.Windows.Forms.DataGridView dgvDistriBonusSearch;
        private System.Windows.Forms.ErrorProvider errorProviderDistBonusRept;
        private System.Windows.Forms.ComboBox cmbCity;
        private System.Windows.Forms.Label lblCity;
        private System.Windows.Forms.Button btnExportReport;
    }
}
