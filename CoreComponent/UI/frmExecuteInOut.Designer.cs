namespace CoreComponent.UI
{
    partial class frmExecuteInOut
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmExecuteInOut));
            this.lblAppUser = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.grpDBC = new System.Windows.Forms.GroupBox();
            this.rdbdcc = new System.Windows.Forms.RadioButton();
            this.rdbTotalpv = new System.Windows.Forms.RadioButton();
            this.btnDBC = new System.Windows.Forms.Button();
            this.grpBoxUpload = new System.Windows.Forms.GroupBox();
            this.btndcc = new System.Windows.Forms.Button();
            this.txtdcc = new System.Windows.Forms.TextBox();
            this.lbldcc = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.dtpInvoiceConsiderDate = new System.Windows.Forms.DateTimePicker();
            this.cmbFileType = new System.Windows.Forms.ComboBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.lblDistributorInvoice = new System.Windows.Forms.Label();
            this.lblExportPath = new System.Windows.Forms.Label();
            this.txtExportPath = new System.Windows.Forms.TextBox();
            this.btnExport = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnExecute = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.grpDBC.SuspendLayout();
            this.grpBoxUpload.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblAppUser
            // 
            this.lblAppUser.AutoSize = true;
            this.lblAppUser.ForeColor = System.Drawing.Color.White;
            this.lblAppUser.Location = new System.Drawing.Point(12, 19);
            this.lblAppUser.Name = "lblAppUser";
            this.lblAppUser.Size = new System.Drawing.Size(29, 13);
            this.lblAppUser.TabIndex = 1;
            this.lblAppUser.Text = "User";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(233)))), ((int)(((byte)(251)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.grpDBC);
            this.panel1.Controls.Add(this.grpBoxUpload);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Location = new System.Drawing.Point(12, 47);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(728, 607);
            this.panel1.TabIndex = 3;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // grpDBC
            // 
            this.grpDBC.Controls.Add(this.rdbdcc);
            this.grpDBC.Controls.Add(this.rdbTotalpv);
            this.grpDBC.Controls.Add(this.btnDBC);
            this.grpDBC.Location = new System.Drawing.Point(171, 353);
            this.grpDBC.Name = "grpDBC";
            this.grpDBC.Size = new System.Drawing.Size(461, 100);
            this.grpDBC.TabIndex = 126;
            this.grpDBC.TabStop = false;
            this.grpDBC.Text = "Distributor Bussiness Current";
            // 
            // rdbdcc
            // 
            this.rdbdcc.AutoSize = true;
            this.rdbdcc.Location = new System.Drawing.Point(78, 62);
            this.rdbdcc.Name = "rdbdcc";
            this.rdbdcc.Size = new System.Drawing.Size(85, 17);
            this.rdbdcc.TabIndex = 131;
            this.rdbdcc.TabStop = true;
            this.rdbdcc.Text = "DCC Record";
            this.rdbdcc.UseVisualStyleBackColor = true;
            // 
            // rdbTotalpv
            // 
            this.rdbTotalpv.AutoSize = true;
            this.rdbTotalpv.Location = new System.Drawing.Point(78, 30);
            this.rdbTotalpv.Name = "rdbTotalpv";
            this.rdbTotalpv.Size = new System.Drawing.Size(84, 17);
            this.rdbTotalpv.TabIndex = 130;
            this.rdbTotalpv.TabStop = true;
            this.rdbTotalpv.Text = "Total PV > 0";
            this.rdbTotalpv.UseVisualStyleBackColor = true;
            // 
            // btnDBC
            // 
            this.btnDBC.BackColor = System.Drawing.Color.Transparent;
            this.btnDBC.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDBC.BackgroundImage")));
            this.btnDBC.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDBC.FlatAppearance.BorderSize = 0;
            this.btnDBC.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnDBC.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnDBC.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDBC.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDBC.Location = new System.Drawing.Point(349, 62);
            this.btnDBC.Name = "btnDBC";
            this.btnDBC.Size = new System.Drawing.Size(106, 32);
            this.btnDBC.TabIndex = 129;
            this.btnDBC.Text = "Save";
            this.btnDBC.UseVisualStyleBackColor = false;
            this.btnDBC.Click += new System.EventHandler(this.btnDBC_Click);
            // 
            // grpBoxUpload
            // 
            this.grpBoxUpload.Controls.Add(this.btndcc);
            this.grpBoxUpload.Controls.Add(this.txtdcc);
            this.grpBoxUpload.Controls.Add(this.lbldcc);
            this.grpBoxUpload.Controls.Add(this.lblDate);
            this.grpBoxUpload.Controls.Add(this.dtpInvoiceConsiderDate);
            this.grpBoxUpload.Controls.Add(this.cmbFileType);
            this.grpBoxUpload.Controls.Add(this.btnOK);
            this.grpBoxUpload.Controls.Add(this.lblDistributorInvoice);
            this.grpBoxUpload.Controls.Add(this.lblExportPath);
            this.grpBoxUpload.Controls.Add(this.txtExportPath);
            this.grpBoxUpload.Controls.Add(this.btnExport);
            this.grpBoxUpload.Location = new System.Drawing.Point(171, 144);
            this.grpBoxUpload.Name = "grpBoxUpload";
            this.grpBoxUpload.Size = new System.Drawing.Size(461, 203);
            this.grpBoxUpload.TabIndex = 125;
            this.grpBoxUpload.TabStop = false;
            this.grpBoxUpload.Text = "Upload Incremental Data";
            this.grpBoxUpload.Enter += new System.EventHandler(this.grpBoxUpload_Enter);
            // 
            // btndcc
            // 
            this.btndcc.BackColor = System.Drawing.Color.Transparent;
            this.btndcc.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btndcc.BackgroundImage")));
            this.btndcc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btndcc.FlatAppearance.BorderSize = 0;
            this.btndcc.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btndcc.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btndcc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btndcc.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btndcc.Location = new System.Drawing.Point(247, 102);
            this.btndcc.Name = "btndcc";
            this.btndcc.Size = new System.Drawing.Size(37, 32);
            this.btndcc.TabIndex = 134;
            this.btndcc.Text = ".....";
            this.btndcc.UseVisualStyleBackColor = false;
            this.btndcc.Click += new System.EventHandler(this.btndcc_Click);
            // 
            // txtdcc
            // 
            this.txtdcc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtdcc.Location = new System.Drawing.Point(135, 107);
            this.txtdcc.Name = "txtdcc";
            this.txtdcc.Size = new System.Drawing.Size(106, 20);
            this.txtdcc.TabIndex = 133;
            this.txtdcc.TextChanged += new System.EventHandler(this.txtdcc_TextChanged);
            // 
            // lbldcc
            // 
            this.lbldcc.BackColor = System.Drawing.Color.Transparent;
            this.lbldcc.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbldcc.Location = new System.Drawing.Point(31, 110);
            this.lbldcc.Name = "lbldcc";
            this.lbldcc.Size = new System.Drawing.Size(98, 17);
            this.lbldcc.TabIndex = 132;
            this.lbldcc.Text = "DCC:*";
            this.lbldcc.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDate
            // 
            this.lblDate.BackColor = System.Drawing.Color.Transparent;
            this.lblDate.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.Location = new System.Drawing.Point(6, 77);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(123, 28);
            this.lblDate.TabIndex = 131;
            this.lblDate.Text = "Invoice Consider Date:";
            this.lblDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblDate.Visible = false;
            // 
            // dtpInvoiceConsiderDate
            // 
            this.dtpInvoiceConsiderDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpInvoiceConsiderDate.Location = new System.Drawing.Point(135, 81);
            this.dtpInvoiceConsiderDate.Name = "dtpInvoiceConsiderDate";
            this.dtpInvoiceConsiderDate.Size = new System.Drawing.Size(106, 20);
            this.dtpInvoiceConsiderDate.TabIndex = 130;
            this.dtpInvoiceConsiderDate.Visible = false;
            // 
            // cmbFileType
            // 
            this.cmbFileType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.cmbFileType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFileType.FormattingEnabled = true;
            this.cmbFileType.Location = new System.Drawing.Point(135, 24);
            this.cmbFileType.Name = "cmbFileType";
            this.cmbFileType.Size = new System.Drawing.Size(106, 21);
            this.cmbFileType.TabIndex = 129;
            this.cmbFileType.SelectedValueChanged += new System.EventHandler(this.cmbFileType_SelectedValueChanged);
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.Transparent;
            this.btnOK.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOK.BackgroundImage")));
            this.btnOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOK.FlatAppearance.BorderSize = 0;
            this.btnOK.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnOK.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Location = new System.Drawing.Point(349, 165);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(106, 32);
            this.btnOK.TabIndex = 128;
            this.btnOK.Text = "Process";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lblDistributorInvoice
            // 
            this.lblDistributorInvoice.BackColor = System.Drawing.Color.Transparent;
            this.lblDistributorInvoice.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDistributorInvoice.Location = new System.Drawing.Point(9, 25);
            this.lblDistributorInvoice.Name = "lblDistributorInvoice";
            this.lblDistributorInvoice.Size = new System.Drawing.Size(120, 20);
            this.lblDistributorInvoice.TabIndex = 125;
            this.lblDistributorInvoice.Text = "File Type:*";
            this.lblDistributorInvoice.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblExportPath
            // 
            this.lblExportPath.BackColor = System.Drawing.Color.Transparent;
            this.lblExportPath.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExportPath.Location = new System.Drawing.Point(22, 51);
            this.lblExportPath.Name = "lblExportPath";
            this.lblExportPath.Size = new System.Drawing.Size(107, 20);
            this.lblExportPath.TabIndex = 124;
            this.lblExportPath.Text = "Zip file Path:*";
            this.lblExportPath.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtExportPath
            // 
            this.txtExportPath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtExportPath.Location = new System.Drawing.Point(135, 51);
            this.txtExportPath.Name = "txtExportPath";
            this.txtExportPath.ReadOnly = true;
            this.txtExportPath.Size = new System.Drawing.Size(106, 20);
            this.txtExportPath.TabIndex = 127;
            // 
            // btnExport
            // 
            this.btnExport.BackColor = System.Drawing.Color.Transparent;
            this.btnExport.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExport.BackgroundImage")));
            this.btnExport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExport.FlatAppearance.BorderSize = 0;
            this.btnExport.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnExport.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExport.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExport.Location = new System.Drawing.Point(247, 45);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(37, 32);
            this.btnExport.TabIndex = 126;
            this.btnExport.Text = ".....";
            this.btnExport.UseVisualStyleBackColor = false;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnExecute);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox1.Location = new System.Drawing.Point(171, 38);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(461, 100);
            this.groupBox1.TabIndex = 124;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Execute Interface Job / Scheduler";
            // 
            // btnExecute
            // 
            this.btnExecute.BackColor = System.Drawing.Color.Transparent;
            this.btnExecute.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExecute.FlatAppearance.BorderSize = 0;
            this.btnExecute.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(233)))), ((int)(((byte)(251)))));
            this.btnExecute.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(233)))), ((int)(((byte)(251)))));
            this.btnExecute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExecute.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExecute.Image = global::CoreComponent.Properties.Resources.button;
            this.btnExecute.Location = new System.Drawing.Point(173, 33);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(90, 32);
            this.btnExecute.TabIndex = 2;
            this.btnExecute.Text = "&Execute";
            this.btnExecute.UseVisualStyleBackColor = false;
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(117)))), ((int)(((byte)(186)))));
            this.btnExit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(117)))), ((int)(((byte)(186)))));
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Image = global::CoreComponent.Properties.Resources.exit;
            this.btnExit.Location = new System.Drawing.Point(649, 12);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(91, 32);
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "E&xit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // frmExecuteInOut
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(117)))), ((int)(((byte)(186)))));
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(755, 666);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.lblAppUser);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmExecuteInOut";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmExecuteInOut_Load);
            this.panel1.ResumeLayout(false);
            this.grpDBC.ResumeLayout(false);
            this.grpDBC.PerformLayout();
            this.grpBoxUpload.ResumeLayout(false);
            this.grpBoxUpload.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblAppUser;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.GroupBox grpBoxUpload;
        private System.Windows.Forms.ComboBox cmbFileType;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label lblDistributorInvoice;
        private System.Windows.Forms.Label lblExportPath;
        private System.Windows.Forms.TextBox txtExportPath;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.DateTimePicker dtpInvoiceConsiderDate;
        private System.Windows.Forms.Label lbldcc;
        private System.Windows.Forms.TextBox txtdcc;
        private System.Windows.Forms.Button btndcc;
        private System.Windows.Forms.GroupBox grpDBC;
        private System.Windows.Forms.RadioButton rdbdcc;
        private System.Windows.Forms.RadioButton rdbTotalpv;
        private System.Windows.Forms.Button btnDBC;
    }
}