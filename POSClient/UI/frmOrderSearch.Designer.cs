namespace POSClient.UI
{
    partial class frmOrderSearch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOrderSearch));
            this.lblOrderNo = new System.Windows.Forms.Label();
            this.txtOrderNo = new System.Windows.Forms.TextBox();
            this.lblFromDate = new System.Windows.Forms.Label();
            this.lblToDate = new System.Windows.Forms.Label();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.dtpTodate = new System.Windows.Forms.DateTimePicker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkLogAll = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbStockPoint = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ckBox = new System.Windows.Forms.CheckBox();
            this.lblSelecttoPrint = new System.Windows.Forms.Label();
            this.txtInvoiceNo = new System.Windows.Forms.TextBox();
            this.lblInvoiceNo = new System.Windows.Forms.Label();
            this.txtDistributorNo = new System.Windows.Forms.TextBox();
            this.lblDistributorNo = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.txtLogNo = new System.Windows.Forms.TextBox();
            this.lblLogNo = new System.Windows.Forms.Label();
            this.pnlButton = new System.Windows.Forms.Panel();
            this.txtGetTotalAmount = new System.Windows.Forms.TextBox();
            this.btnGetTotal = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.lblComboLog = new System.Windows.Forms.Label();
            this.cmbLogList = new System.Windows.Forms.ComboBox();
            this.btnadd = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.dgvOrderSearch = new System.Windows.Forms.DataGridView();
            this.errorAddToLog = new System.Windows.Forms.ErrorProvider(this.components);
            this.panel1.SuspendLayout();
            this.pnlButton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrderSearch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorAddToLog)).BeginInit();
            this.SuspendLayout();
            // 
            // lblOrderNo
            // 
            this.lblOrderNo.AutoSize = true;
            this.lblOrderNo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrderNo.Location = new System.Drawing.Point(32, 17);
            this.lblOrderNo.Name = "lblOrderNo";
            this.lblOrderNo.Size = new System.Drawing.Size(73, 14);
            this.lblOrderNo.TabIndex = 0;
            this.lblOrderNo.Text = "Order No:";
            // 
            // txtOrderNo
            // 
            this.txtOrderNo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOrderNo.Location = new System.Drawing.Point(103, 14);
            this.txtOrderNo.MaxLength = 20;
            this.txtOrderNo.Name = "txtOrderNo";
            this.txtOrderNo.Size = new System.Drawing.Size(138, 22);
            this.txtOrderNo.TabIndex = 1;
            // 
            // lblFromDate
            // 
            this.lblFromDate.AutoSize = true;
            this.lblFromDate.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFromDate.Location = new System.Drawing.Point(24, 51);
            this.lblFromDate.Name = "lblFromDate";
            this.lblFromDate.Size = new System.Drawing.Size(81, 14);
            this.lblFromDate.TabIndex = 2;
            this.lblFromDate.Text = "From Date:";
            // 
            // lblToDate
            // 
            this.lblToDate.AutoSize = true;
            this.lblToDate.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblToDate.Location = new System.Drawing.Point(267, 51);
            this.lblToDate.Name = "lblToDate";
            this.lblToDate.Size = new System.Drawing.Size(63, 14);
            this.lblToDate.TabIndex = 3;
            this.lblToDate.Text = "To Date:";
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Checked = false;
            this.dtpFromDate.CustomFormat = "dd-MM-yyyy";
            this.dtpFromDate.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFromDate.Location = new System.Drawing.Point(103, 47);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.ShowCheckBox = true;
            this.dtpFromDate.Size = new System.Drawing.Size(115, 22);
            this.dtpFromDate.TabIndex = 4;
            // 
            // dtpTodate
            // 
            this.dtpTodate.Checked = false;
            this.dtpTodate.CustomFormat = "dd-MM-yyyy";
            this.dtpTodate.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpTodate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTodate.Location = new System.Drawing.Point(331, 47);
            this.dtpTodate.Name = "dtpTodate";
            this.dtpTodate.ShowCheckBox = true;
            this.dtpTodate.Size = new System.Drawing.Size(121, 22);
            this.dtpTodate.TabIndex = 5;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.chkLogAll);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cmbStockPoint);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.ckBox);
            this.panel1.Controls.Add(this.lblSelecttoPrint);
            this.panel1.Controls.Add(this.txtInvoiceNo);
            this.panel1.Controls.Add(this.lblInvoiceNo);
            this.panel1.Controls.Add(this.txtDistributorNo);
            this.panel1.Controls.Add(this.lblDistributorNo);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.cmbStatus);
            this.panel1.Controls.Add(this.lblStatus);
            this.panel1.Controls.Add(this.txtLogNo);
            this.panel1.Controls.Add(this.lblLogNo);
            this.panel1.Controls.Add(this.txtOrderNo);
            this.panel1.Controls.Add(this.dtpTodate);
            this.panel1.Controls.Add(this.lblOrderNo);
            this.panel1.Controls.Add(this.dtpFromDate);
            this.panel1.Controls.Add(this.lblFromDate);
            this.panel1.Controls.Add(this.lblToDate);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(725, 135);
            this.panel1.TabIndex = 6;
            // 
            // chkLogAll
            // 
            this.chkLogAll.AutoSize = true;
            this.chkLogAll.Location = new System.Drawing.Point(64, 114);
            this.chkLogAll.Name = "chkLogAll";
            this.chkLogAll.Size = new System.Drawing.Size(15, 14);
            this.chkLogAll.TabIndex = 26;
            this.chkLogAll.UseVisualStyleBackColor = true;
            this.chkLogAll.CheckedChanged += new System.EventHandler(this.chkLogAll_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 113);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 14);
            this.label2.TabIndex = 27;
            this.label2.Text = "Log All:";
            // 
            // cmbStockPoint
            // 
            this.cmbStockPoint.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStockPoint.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbStockPoint.FormattingEnabled = true;
            this.cmbStockPoint.Location = new System.Drawing.Point(331, 110);
            this.cmbStockPoint.Name = "cmbStockPoint";
            this.cmbStockPoint.Size = new System.Drawing.Size(258, 22);
            this.cmbStockPoint.TabIndex = 24;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(243, 113);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 14);
            this.label1.TabIndex = 25;
            this.label1.Text = "Stock Point:";
            // 
            // ckBox
            // 
            this.ckBox.AutoSize = true;
            this.ckBox.Location = new System.Drawing.Point(172, 114);
            this.ckBox.Name = "ckBox";
            this.ckBox.Size = new System.Drawing.Size(15, 14);
            this.ckBox.TabIndex = 22;
            this.ckBox.UseVisualStyleBackColor = true;
            this.ckBox.CheckedChanged += new System.EventHandler(this.ckBox_CheckedChanged);
            // 
            // lblSelecttoPrint
            // 
            this.lblSelecttoPrint.AutoSize = true;
            this.lblSelecttoPrint.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelecttoPrint.Location = new System.Drawing.Point(100, 113);
            this.lblSelecttoPrint.Name = "lblSelecttoPrint";
            this.lblSelecttoPrint.Size = new System.Drawing.Size(65, 14);
            this.lblSelecttoPrint.TabIndex = 23;
            this.lblSelecttoPrint.Text = "Print All:";
            // 
            // txtInvoiceNo
            // 
            this.txtInvoiceNo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInvoiceNo.Location = new System.Drawing.Point(331, 14);
            this.txtInvoiceNo.MaxLength = 20;
            this.txtInvoiceNo.Name = "txtInvoiceNo";
            this.txtInvoiceNo.Size = new System.Drawing.Size(139, 22);
            this.txtInvoiceNo.TabIndex = 2;
            // 
            // lblInvoiceNo
            // 
            this.lblInvoiceNo.AutoSize = true;
            this.lblInvoiceNo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInvoiceNo.Location = new System.Drawing.Point(247, 17);
            this.lblInvoiceNo.Name = "lblInvoiceNo";
            this.lblInvoiceNo.Size = new System.Drawing.Size(83, 14);
            this.lblInvoiceNo.TabIndex = 21;
            this.lblInvoiceNo.Text = "Invoice No:";
            // 
            // txtDistributorNo
            // 
            this.txtDistributorNo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDistributorNo.Location = new System.Drawing.Point(103, 79);
            this.txtDistributorNo.MaxLength = 20;
            this.txtDistributorNo.Name = "txtDistributorNo";
            this.txtDistributorNo.Size = new System.Drawing.Size(115, 22);
            this.txtDistributorNo.TabIndex = 6;
            // 
            // lblDistributorNo
            // 
            this.lblDistributorNo.AutoSize = true;
            this.lblDistributorNo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDistributorNo.Location = new System.Drawing.Point(2, 82);
            this.lblDistributorNo.Name = "lblDistributorNo";
            this.lblDistributorNo.Size = new System.Drawing.Size(105, 14);
            this.lblDistributorNo.TabIndex = 19;
            this.lblDistributorNo.Text = "Distributor No:";
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btnSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.BackgroundImage")));
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSearch.Location = new System.Drawing.Point(641, 59);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(72, 62);
            this.btnSearch.TabIndex = 8;
            this.btnSearch.TabStop = false;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // cmbStatus
            // 
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Location = new System.Drawing.Point(331, 79);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(121, 22);
            this.cmbStatus.TabIndex = 7;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(276, 82);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(54, 14);
            this.lblStatus.TabIndex = 16;
            this.lblStatus.Text = "Status:";
            // 
            // txtLogNo
            // 
            this.txtLogNo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLogNo.Location = new System.Drawing.Point(537, 14);
            this.txtLogNo.MaxLength = 20;
            this.txtLogNo.Name = "txtLogNo";
            this.txtLogNo.Size = new System.Drawing.Size(165, 22);
            this.txtLogNo.TabIndex = 3;
            // 
            // lblLogNo
            // 
            this.lblLogNo.AutoSize = true;
            this.lblLogNo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLogNo.Location = new System.Drawing.Point(484, 19);
            this.lblLogNo.Name = "lblLogNo";
            this.lblLogNo.Size = new System.Drawing.Size(58, 14);
            this.lblLogNo.TabIndex = 14;
            this.lblLogNo.Text = "Log No:";
            // 
            // pnlButton
            // 
            this.pnlButton.BackColor = System.Drawing.Color.Transparent;
            this.pnlButton.Controls.Add(this.txtGetTotalAmount);
            this.pnlButton.Controls.Add(this.btnGetTotal);
            this.pnlButton.Controls.Add(this.btnPrint);
            this.pnlButton.Controls.Add(this.lblComboLog);
            this.pnlButton.Controls.Add(this.cmbLogList);
            this.pnlButton.Controls.Add(this.btnadd);
            this.pnlButton.Controls.Add(this.btnCancel);
            this.pnlButton.Controls.Add(this.btnOk);
            this.pnlButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButton.Location = new System.Drawing.Point(0, 397);
            this.pnlButton.Name = "pnlButton";
            this.pnlButton.Size = new System.Drawing.Size(725, 67);
            this.pnlButton.TabIndex = 7;
            // 
            // txtGetTotalAmount
            // 
            this.txtGetTotalAmount.Enabled = false;
            this.txtGetTotalAmount.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGetTotalAmount.Location = new System.Drawing.Point(159, 40);
            this.txtGetTotalAmount.MaxLength = 20;
            this.txtGetTotalAmount.Name = "txtGetTotalAmount";
            this.txtGetTotalAmount.Size = new System.Drawing.Size(224, 22);
            this.txtGetTotalAmount.TabIndex = 28;
            // 
            // btnGetTotal
            // 
            this.btnGetTotal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(219)))), ((int)(((byte)(192)))));
            this.btnGetTotal.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnGetTotal.BackgroundImage")));
            this.btnGetTotal.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnGetTotal.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnGetTotal.Location = new System.Drawing.Point(3, 36);
            this.btnGetTotal.Name = "btnGetTotal";
            this.btnGetTotal.Size = new System.Drawing.Size(150, 28);
            this.btnGetTotal.TabIndex = 18;
            this.btnGetTotal.Text = "Get Total Amount";
            this.btnGetTotal.UseVisualStyleBackColor = false;
            this.btnGetTotal.Click += new System.EventHandler(this.btnGetTotal_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(219)))), ((int)(((byte)(192)))));
            this.btnPrint.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPrint.BackgroundImage")));
            this.btnPrint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPrint.Location = new System.Drawing.Point(485, 2);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(72, 62);
            this.btnPrint.TabIndex = 17;
            this.btnPrint.TabStop = false;
            this.btnPrint.Text = "&Print Invoice";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // lblComboLog
            // 
            this.lblComboLog.AutoSize = true;
            this.lblComboLog.Location = new System.Drawing.Point(2, 9);
            this.lblComboLog.Name = "lblComboLog";
            this.lblComboLog.Size = new System.Drawing.Size(62, 16);
            this.lblComboLog.TabIndex = 16;
            this.lblComboLog.Text = "Log No:";
            // 
            // cmbLogList
            // 
            this.cmbLogList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLogList.FormattingEnabled = true;
            this.cmbLogList.Location = new System.Drawing.Point(64, 6);
            this.cmbLogList.Name = "cmbLogList";
            this.cmbLogList.Size = new System.Drawing.Size(319, 24);
            this.cmbLogList.TabIndex = 9;
            // 
            // btnadd
            // 
            this.btnadd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(219)))), ((int)(((byte)(192)))));
            this.btnadd.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnadd.BackgroundImage")));
            this.btnadd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnadd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnadd.Location = new System.Drawing.Point(391, 2);
            this.btnadd.Name = "btnadd";
            this.btnadd.Size = new System.Drawing.Size(88, 62);
            this.btnadd.TabIndex = 10;
            this.btnadd.Text = "Add To Log";
            this.btnadd.UseVisualStyleBackColor = false;
            this.btnadd.Click += new System.EventHandler(this.btnadd_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btnCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.BackgroundImage")));
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.Location = new System.Drawing.Point(641, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 62);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.TabStop = false;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(219)))), ((int)(((byte)(192)))));
            this.btnOk.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOk.BackgroundImage")));
            this.btnOk.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnOk.Location = new System.Drawing.Point(563, 2);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(72, 62);
            this.btnOk.TabIndex = 11;
            this.btnOk.TabStop = false;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // dgvOrderSearch
            // 
            this.dgvOrderSearch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrderSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOrderSearch.Location = new System.Drawing.Point(0, 135);
            this.dgvOrderSearch.MultiSelect = false;
            this.dgvOrderSearch.Name = "dgvOrderSearch";
            this.dgvOrderSearch.RowHeadersVisible = false;
            this.dgvOrderSearch.Size = new System.Drawing.Size(725, 262);
            this.dgvOrderSearch.TabIndex = 8;
            this.dgvOrderSearch.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvOrderSearch_RowEnter);
            this.dgvOrderSearch.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvOrderSearch_CellMouseDoubleClick);
            // 
            // errorAddToLog
            // 
            this.errorAddToLog.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorAddToLog.ContainerControl = this;
            // 
            // frmOrderSearch
            // 
            this.AcceptButton = this.btnSearch;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(725, 464);
            this.Controls.Add(this.dgvOrderSearch);
            this.Controls.Add(this.pnlButton);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmOrderSearch";
            this.Text = "Order Search";
            this.Load += new System.EventHandler(this.frmOrderSearch_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlButton.ResumeLayout(false);
            this.pnlButton.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrderSearch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorAddToLog)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblOrderNo;
        private System.Windows.Forms.TextBox txtOrderNo;
        private System.Windows.Forms.Label lblFromDate;
        private System.Windows.Forms.Label lblToDate;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.DateTimePicker dtpTodate;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnlButton;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.DataGridView dgvOrderSearch;
        private System.Windows.Forms.Button btnadd;
        private System.Windows.Forms.ComboBox cmbLogList;
        private System.Windows.Forms.ErrorProvider errorAddToLog;
        private System.Windows.Forms.Label lblComboLog;
        private System.Windows.Forms.TextBox txtLogNo;
        private System.Windows.Forms.Label lblLogNo;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtDistributorNo;
        private System.Windows.Forms.Label lblDistributorNo;
        private System.Windows.Forms.TextBox txtInvoiceNo;
        private System.Windows.Forms.Label lblInvoiceNo;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.CheckBox ckBox;
        private System.Windows.Forms.Label lblSelecttoPrint;
        private System.Windows.Forms.ComboBox cmbStockPoint;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkLogAll;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnGetTotal;
        private System.Windows.Forms.TextBox txtGetTotalAmount;
    }
}