namespace CoreComponent.UI
{
    partial class frmTaxPayment
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
            this.lblFinancialYear = new System.Windows.Forms.Label();
            this.cmbFinancialYear = new System.Windows.Forms.ComboBox();
            this.lblQuarter = new System.Windows.Forms.Label();
            this.cmbQuarters = new System.Windows.Forms.ComboBox();
            this.lblChallanNo = new System.Windows.Forms.Label();
            this.txtChallanNO = new System.Windows.Forms.TextBox();
            this.txtBSRCode = new System.Windows.Forms.TextBox();
            this.lblBSRCode = new System.Windows.Forms.Label();
            this.txtChequeNo = new System.Windows.Forms.TextBox();
            this.lblChequeNo = new System.Windows.Forms.Label();
            this.txtAcknowledgeNo = new System.Windows.Forms.TextBox();
            this.lblAcknowledgemenNo = new System.Windows.Forms.Label();
            this.dtpDepositDate = new System.Windows.Forms.DateTimePicker();
            this.lblDepositDate = new System.Windows.Forms.Label();
            this.dgvChallan = new System.Windows.Forms.DataGridView();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.lblAddQuarter = new System.Windows.Forms.Label();
            this.cmbAddQuarters = new System.Windows.Forms.ComboBox();
            this.lblAddFinancialYear = new System.Windows.Forms.Label();
            this.cmbAddFinancialYear = new System.Windows.Forms.ComboBox();
            this.btnChallanSave = new System.Windows.Forms.Button();
            this.txtDepositAmount = new System.Windows.Forms.TextBox();
            this.lblDepositAmount = new System.Windows.Forms.Label();
            this.errProvChallan = new System.Windows.Forms.ErrorProvider(this.components);
            this.pnlCreateHeader.SuspendLayout();
            this.grpAddDetails.SuspendLayout();
            this.pnlSearchHeader.SuspendLayout();
            this.pnlSearchGrid.SuspendLayout();
            this.pnlCreateDetail.SuspendLayout();
            this.pnlLowerButtons.SuspendLayout();
            this.pnlTopButtons.SuspendLayout();
            this.pnlSearchButtons.SuspendLayout();
            this.tabSearch.SuspendLayout();
            this.tabCreate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChallan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errProvChallan)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCreateHeader
            // 
            this.pnlCreateHeader.Controls.Add(this.lblAddQuarter);
            this.pnlCreateHeader.Controls.Add(this.cmbAddQuarters);
            this.pnlCreateHeader.Controls.Add(this.lblAddFinancialYear);
            this.pnlCreateHeader.Controls.Add(this.cmbAddFinancialYear);
            this.pnlCreateHeader.Size = new System.Drawing.Size(1005, 100);
            this.pnlCreateHeader.Controls.SetChildIndex(this.pnlTopButtons, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.cmbAddFinancialYear, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblAddFinancialYear, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.cmbAddQuarters, 0);
            this.pnlCreateHeader.Controls.SetChildIndex(this.lblAddQuarter, 0);
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
            // lblAddDetails
            // 
            this.lblAddDetails.Dock = System.Windows.Forms.DockStyle.None;
            this.lblAddDetails.Location = new System.Drawing.Point(0, -1);
            // 
            // grpAddDetails
            // 
            this.grpAddDetails.Location = new System.Drawing.Point(0, 124);
            this.grpAddDetails.Size = new System.Drawing.Size(1005, 487);
            // 
            // btnClearDetails
            // 
            this.btnClearDetails.FlatAppearance.BorderSize = 0;
            this.btnClearDetails.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnClearDetails.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnClearDetails.Text = "&Clear";
            // 
            // btnAddDetails
            // 
            this.btnAddDetails.FlatAppearance.BorderSize = 0;
            this.btnAddDetails.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnAddDetails.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnAddDetails.Text = "&Process";
            // 
            // pnlSearchHeader
            // 
            this.pnlSearchHeader.Controls.Add(this.txtDepositAmount);
            this.pnlSearchHeader.Controls.Add(this.lblDepositAmount);
            this.pnlSearchHeader.Controls.Add(this.lblDepositDate);
            this.pnlSearchHeader.Controls.Add(this.dtpDepositDate);
            this.pnlSearchHeader.Controls.Add(this.txtAcknowledgeNo);
            this.pnlSearchHeader.Controls.Add(this.lblAcknowledgemenNo);
            this.pnlSearchHeader.Controls.Add(this.txtChequeNo);
            this.pnlSearchHeader.Controls.Add(this.lblChequeNo);
            this.pnlSearchHeader.Controls.Add(this.txtBSRCode);
            this.pnlSearchHeader.Controls.Add(this.lblBSRCode);
            this.pnlSearchHeader.Controls.Add(this.txtChallanNO);
            this.pnlSearchHeader.Controls.Add(this.lblChallanNo);
            this.pnlSearchHeader.Controls.Add(this.lblQuarter);
            this.pnlSearchHeader.Controls.Add(this.cmbQuarters);
            this.pnlSearchHeader.Controls.Add(this.lblFinancialYear);
            this.pnlSearchHeader.Controls.Add(this.cmbFinancialYear);
            this.pnlSearchHeader.Controls.SetChildIndex(this.cmbFinancialYear, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblFinancialYear, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.pnlSearchButtons, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.cmbQuarters, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblQuarter, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblChallanNo, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.txtChallanNO, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblBSRCode, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.txtBSRCode, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblChequeNo, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.txtChequeNo, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblAcknowledgemenNo, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.txtAcknowledgeNo, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.dtpDepositDate, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblDepositDate, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblDepositAmount, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.txtDepositAmount, 0);
            // 
            // btnSearch
            // 
            this.btnSearch.Dock = System.Windows.Forms.DockStyle.None;
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearch.Location = new System.Drawing.Point(768, 0);
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnSearchReset
            // 
            this.btnSearchReset.Dock = System.Windows.Forms.DockStyle.None;
            this.btnSearchReset.FlatAppearance.BorderSize = 0;
            this.btnSearchReset.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearchReset.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearchReset.TabIndex = 0;
            this.btnSearchReset.Click += new System.EventHandler(this.btnSearchReset_Click);
            // 
            // pnlSearchGrid
            // 
            this.pnlSearchGrid.Controls.Add(this.dgvChallan);
            // 
            // pnlCreateDetail
            // 
            this.pnlCreateDetail.Controls.Add(this.dataGridView1);
            this.pnlCreateDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCreateDetail.Size = new System.Drawing.Size(1005, 473);
            // 
            // pnlLowerButtons
            // 
            this.pnlLowerButtons.Location = new System.Drawing.Point(0, 455);
            // 
            // pnlTopButtons
            // 
            this.pnlTopButtons.Location = new System.Drawing.Point(0, 66);
            // 
            // pnlSearchButtons
            // 
            this.pnlSearchButtons.Controls.Add(this.btnChallanSave);
            this.pnlSearchButtons.Controls.SetChildIndex(this.btnChallanSave, 0);
            this.pnlSearchButtons.Controls.SetChildIndex(this.btnSearch, 0);
            this.pnlSearchButtons.Controls.SetChildIndex(this.btnSearchReset, 0);
            // 
            // lblFinancialYear
            // 
            this.lblFinancialYear.AutoSize = true;
            this.lblFinancialYear.Location = new System.Drawing.Point(52, 33);
            this.lblFinancialYear.Name = "lblFinancialYear";
            this.lblFinancialYear.Size = new System.Drawing.Size(91, 13);
            this.lblFinancialYear.TabIndex = 7;
            this.lblFinancialYear.Text = "Financial Year:";
            // 
            // cmbFinancialYear
            // 
            this.cmbFinancialYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFinancialYear.FormattingEnabled = true;
            this.cmbFinancialYear.Location = new System.Drawing.Point(149, 30);
            this.cmbFinancialYear.Name = "cmbFinancialYear";
            this.cmbFinancialYear.Size = new System.Drawing.Size(150, 21);
            this.cmbFinancialYear.TabIndex = 8;
            // 
            // lblQuarter
            // 
            this.lblQuarter.AutoSize = true;
            this.lblQuarter.Location = new System.Drawing.Point(425, 33);
            this.lblQuarter.Name = "lblQuarter";
            this.lblQuarter.Size = new System.Drawing.Size(56, 13);
            this.lblQuarter.TabIndex = 9;
            this.lblQuarter.Text = "Quarter:";
            // 
            // cmbQuarters
            // 
            this.cmbQuarters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbQuarters.FormattingEnabled = true;
            this.cmbQuarters.Location = new System.Drawing.Point(487, 30);
            this.cmbQuarters.Name = "cmbQuarters";
            this.cmbQuarters.Size = new System.Drawing.Size(150, 21);
            this.cmbQuarters.TabIndex = 10;
            // 
            // lblChallanNo
            // 
            this.lblChallanNo.AutoSize = true;
            this.lblChallanNo.Location = new System.Drawing.Point(724, 33);
            this.lblChallanNo.Name = "lblChallanNo";
            this.lblChallanNo.Size = new System.Drawing.Size(78, 13);
            this.lblChallanNo.TabIndex = 11;
            this.lblChallanNo.Text = "Challan No.:";
            // 
            // txtChallanNO
            // 
            this.txtChallanNO.Location = new System.Drawing.Point(803, 30);
            this.txtChallanNO.Name = "txtChallanNO";
            this.txtChallanNO.Size = new System.Drawing.Size(150, 21);
            this.txtChallanNO.TabIndex = 12;
            // 
            // txtBSRCode
            // 
            this.txtBSRCode.Location = new System.Drawing.Point(149, 71);
            this.txtBSRCode.Name = "txtBSRCode";
            this.txtBSRCode.Size = new System.Drawing.Size(150, 21);
            this.txtBSRCode.TabIndex = 14;
            // 
            // lblBSRCode
            // 
            this.lblBSRCode.AutoSize = true;
            this.lblBSRCode.Location = new System.Drawing.Point(73, 75);
            this.lblBSRCode.Name = "lblBSRCode";
            this.lblBSRCode.Size = new System.Drawing.Size(70, 13);
            this.lblBSRCode.TabIndex = 13;
            this.lblBSRCode.Text = "BSR Code:";
            // 
            // txtChequeNo
            // 
            this.txtChequeNo.Location = new System.Drawing.Point(487, 71);
            this.txtChequeNo.Name = "txtChequeNo";
            this.txtChequeNo.Size = new System.Drawing.Size(150, 21);
            this.txtChequeNo.TabIndex = 16;
            // 
            // lblChequeNo
            // 
            this.lblChequeNo.AutoSize = true;
            this.lblChequeNo.Location = new System.Drawing.Point(406, 74);
            this.lblChequeNo.Name = "lblChequeNo";
            this.lblChequeNo.Size = new System.Drawing.Size(75, 13);
            this.lblChequeNo.TabIndex = 15;
            this.lblChequeNo.Text = "Cheque No:";
            // 
            // txtAcknowledgeNo
            // 
            this.txtAcknowledgeNo.Location = new System.Drawing.Point(149, 110);
            this.txtAcknowledgeNo.Name = "txtAcknowledgeNo";
            this.txtAcknowledgeNo.Size = new System.Drawing.Size(150, 21);
            this.txtAcknowledgeNo.TabIndex = 18;
            // 
            // lblAcknowledgemenNo
            // 
            this.lblAcknowledgemenNo.AutoSize = true;
            this.lblAcknowledgemenNo.Location = new System.Drawing.Point(37, 113);
            this.lblAcknowledgemenNo.Name = "lblAcknowledgemenNo";
            this.lblAcknowledgemenNo.Size = new System.Drawing.Size(106, 13);
            this.lblAcknowledgemenNo.TabIndex = 17;
            this.lblAcknowledgemenNo.Text = "Acknowledge No:";
            // 
            // dtpDepositDate
            // 
            this.dtpDepositDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDepositDate.Location = new System.Drawing.Point(803, 71);
            this.dtpDepositDate.Name = "dtpDepositDate";
            this.dtpDepositDate.Size = new System.Drawing.Size(120, 21);
            this.dtpDepositDate.TabIndex = 19;
            // 
            // lblDepositDate
            // 
            this.lblDepositDate.AutoSize = true;
            this.lblDepositDate.Location = new System.Drawing.Point(716, 74);
            this.lblDepositDate.Name = "lblDepositDate";
            this.lblDepositDate.Size = new System.Drawing.Size(86, 13);
            this.lblDepositDate.TabIndex = 20;
            this.lblDepositDate.Text = "Deposit Date:";
            // 
            // dgvChallan
            // 
            this.dgvChallan.AllowUserToAddRows = false;
            this.dgvChallan.AllowUserToDeleteRows = false;
            this.dgvChallan.AllowUserToResizeColumns = false;
            this.dgvChallan.AllowUserToResizeRows = false;
            this.dgvChallan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChallan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvChallan.Location = new System.Drawing.Point(0, 0);
            this.dgvChallan.Name = "dgvChallan";
            this.dgvChallan.RowHeadersVisible = false;
            this.dgvChallan.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvChallan.Size = new System.Drawing.Size(1005, 387);
            this.dgvChallan.TabIndex = 0;
            this.dgvChallan.SelectionChanged += new System.EventHandler(this.dgvChallan_SelectionChanged);
            this.dgvChallan.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvChallan_CellContentClick);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(1005, 473);
            this.dataGridView1.TabIndex = 0;
            // 
            // lblAddQuarter
            // 
            this.lblAddQuarter.AutoSize = true;
            this.lblAddQuarter.Location = new System.Drawing.Point(409, 28);
            this.lblAddQuarter.Name = "lblAddQuarter";
            this.lblAddQuarter.Size = new System.Drawing.Size(56, 13);
            this.lblAddQuarter.TabIndex = 13;
            this.lblAddQuarter.Text = "Quarter:";
            // 
            // cmbAddQuarters
            // 
            this.cmbAddQuarters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAddQuarters.FormattingEnabled = true;
            this.cmbAddQuarters.Location = new System.Drawing.Point(471, 25);
            this.cmbAddQuarters.Name = "cmbAddQuarters";
            this.cmbAddQuarters.Size = new System.Drawing.Size(150, 21);
            this.cmbAddQuarters.TabIndex = 14;
            // 
            // lblAddFinancialYear
            // 
            this.lblAddFinancialYear.AutoSize = true;
            this.lblAddFinancialYear.Location = new System.Drawing.Point(36, 28);
            this.lblAddFinancialYear.Name = "lblAddFinancialYear";
            this.lblAddFinancialYear.Size = new System.Drawing.Size(91, 13);
            this.lblAddFinancialYear.TabIndex = 11;
            this.lblAddFinancialYear.Text = "Financial Year:";
            // 
            // cmbAddFinancialYear
            // 
            this.cmbAddFinancialYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAddFinancialYear.FormattingEnabled = true;
            this.cmbAddFinancialYear.Location = new System.Drawing.Point(133, 25);
            this.cmbAddFinancialYear.Name = "cmbAddFinancialYear";
            this.cmbAddFinancialYear.Size = new System.Drawing.Size(150, 21);
            this.cmbAddFinancialYear.TabIndex = 12;
            // 
            // btnChallanSave
            // 
            this.btnChallanSave.BackgroundImage = global::CoreComponent.Properties.Resources.button;
            this.btnChallanSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnChallanSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChallanSave.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChallanSave.Location = new System.Drawing.Point(849, 0);
            this.btnChallanSave.Name = "btnChallanSave";
            this.btnChallanSave.Size = new System.Drawing.Size(75, 32);
            this.btnChallanSave.TabIndex = 2;
            this.btnChallanSave.Text = "&Save";
            this.btnChallanSave.UseVisualStyleBackColor = true;
            this.btnChallanSave.Click += new System.EventHandler(this.btnChallanSave_Click);
            // 
            // txtDepositAmount
            // 
            this.txtDepositAmount.Location = new System.Drawing.Point(487, 110);
            this.txtDepositAmount.Name = "txtDepositAmount";
            this.txtDepositAmount.Size = new System.Drawing.Size(150, 21);
            this.txtDepositAmount.TabIndex = 22;
            // 
            // lblDepositAmount
            // 
            this.lblDepositAmount.AutoSize = true;
            this.lblDepositAmount.Location = new System.Drawing.Point(385, 113);
            this.lblDepositAmount.Name = "lblDepositAmount";
            this.lblDepositAmount.Size = new System.Drawing.Size(96, 13);
            this.lblDepositAmount.TabIndex = 21;
            this.lblDepositAmount.Text = "Deposit Amout:";
            // 
            // errProvChallan
            // 
            this.errProvChallan.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errProvChallan.ContainerControl = this;
            // 
            // frmTaxPayment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1013, 707);
            this.Name = "frmTaxPayment";
            this.Text = "TaxPayment";
            this.WindowState = System.Windows.Forms.FormWindowState.Normal;
            this.pnlCreateHeader.ResumeLayout(false);
            this.pnlCreateHeader.PerformLayout();
            this.grpAddDetails.ResumeLayout(false);
            this.pnlSearchHeader.ResumeLayout(false);
            this.pnlSearchHeader.PerformLayout();
            this.pnlSearchGrid.ResumeLayout(false);
            this.pnlCreateDetail.ResumeLayout(false);
            this.pnlLowerButtons.ResumeLayout(false);
            this.pnlTopButtons.ResumeLayout(false);
            this.pnlSearchButtons.ResumeLayout(false);
            this.tabSearch.ResumeLayout(false);
            this.tabCreate.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvChallan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errProvChallan)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblFinancialYear;
        private System.Windows.Forms.ComboBox cmbFinancialYear;
        private System.Windows.Forms.Label lblChallanNo;
        private System.Windows.Forms.Label lblQuarter;
        private System.Windows.Forms.ComboBox cmbQuarters;
        private System.Windows.Forms.Label lblDepositDate;
        private System.Windows.Forms.DateTimePicker dtpDepositDate;
        private System.Windows.Forms.TextBox txtAcknowledgeNo;
        private System.Windows.Forms.Label lblAcknowledgemenNo;
        private System.Windows.Forms.TextBox txtChequeNo;
        private System.Windows.Forms.Label lblChequeNo;
        private System.Windows.Forms.TextBox txtBSRCode;
        private System.Windows.Forms.Label lblBSRCode;
        private System.Windows.Forms.TextBox txtChallanNO;
        private System.Windows.Forms.DataGridView dgvChallan;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label lblAddQuarter;
        private System.Windows.Forms.ComboBox cmbAddQuarters;
        private System.Windows.Forms.Label lblAddFinancialYear;
        private System.Windows.Forms.ComboBox cmbAddFinancialYear;
        private System.Windows.Forms.Button btnChallanSave;
        private System.Windows.Forms.TextBox txtDepositAmount;
        private System.Windows.Forms.Label lblDepositAmount;
        private System.Windows.Forms.ErrorProvider errProvChallan;
    }
}