namespace CoreComponent.UI
{
    partial class frmDistributorPaymentSummary
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDistributorPaymentSummary));
            this.lblMonth = new System.Windows.Forms.Label();
            this.epUOM = new System.Windows.Forms.ErrorProvider(this.components);
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.lblYear = new System.Windows.Forms.Label();
            this.cmbYear = new System.Windows.Forms.ComboBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtExportPath = new System.Windows.Forms.TextBox();
            this.lblExportPath = new System.Windows.Forms.Label();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnImportPath = new System.Windows.Forms.Button();
            this.lblImportPath = new System.Windows.Forms.Label();
            this.txtImportPath = new System.Windows.Forms.TextBox();
            this.grpDistributorSummary = new System.Windows.Forms.GroupBox();
            this.rdoImport = new System.Windows.Forms.RadioButton();
            this.rdoExport = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.epUOM)).BeginInit();
            this.grpDistributorSummary.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblMonth
            // 
            this.lblMonth.BackColor = System.Drawing.Color.Transparent;
            this.lblMonth.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMonth.Location = new System.Drawing.Point(27, 22);
            this.lblMonth.Name = "lblMonth";
            this.lblMonth.Size = new System.Drawing.Size(86, 13);
            this.lblMonth.TabIndex = 0;
            this.lblMonth.Text = "Month:*";
            this.lblMonth.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // epUOM
            // 
            this.epUOM.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.epUOM.ContainerControl = this;
            // 
            // cmbStatus
            // 
            this.cmbStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Location = new System.Drawing.Point(128, 74);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(110, 21);
            this.cmbStatus.TabIndex = 117;
            this.cmbStatus.SelectedIndexChanged += new System.EventHandler(this.cmbStatus_SelectedIndexChanged);
            // 
            // lblYear
            // 
            this.lblYear.BackColor = System.Drawing.Color.Transparent;
            this.lblYear.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblYear.Location = new System.Drawing.Point(261, 22);
            this.lblYear.Name = "lblYear";
            this.lblYear.Size = new System.Drawing.Size(51, 17);
            this.lblYear.TabIndex = 0;
            this.lblYear.Text = "Year:*";
            this.lblYear.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbYear
            // 
            this.cmbYear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.cmbYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbYear.FormattingEnabled = true;
            this.cmbYear.Location = new System.Drawing.Point(327, 73);
            this.cmbYear.Name = "cmbYear";
            this.cmbYear.Size = new System.Drawing.Size(110, 21);
            this.cmbYear.TabIndex = 117;
            this.cmbYear.SelectedIndexChanged += new System.EventHandler(this.cmbYear_SelectedIndexChanged);
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
            this.btnOK.Location = new System.Drawing.Point(60, 194);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 32);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "O&k";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.BackgroundImage")));
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(141, 194);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 32);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtExportPath
            // 
            this.txtExportPath.Location = new System.Drawing.Point(128, 110);
            this.txtExportPath.Name = "txtExportPath";
            this.txtExportPath.Size = new System.Drawing.Size(106, 20);
            this.txtExportPath.TabIndex = 118;
            this.txtExportPath.TextChanged += new System.EventHandler(this.txtExportPath_TextChanged);
            this.txtExportPath.Validated += new System.EventHandler(this.txtExportPath_Validated);
            // 
            // lblExportPath
            // 
            this.lblExportPath.BackColor = System.Drawing.Color.Transparent;
            this.lblExportPath.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExportPath.Location = new System.Drawing.Point(27, 59);
            this.lblExportPath.Name = "lblExportPath";
            this.lblExportPath.Size = new System.Drawing.Size(86, 13);
            this.lblExportPath.TabIndex = 0;
            this.lblExportPath.Text = "Export Path:*";
            this.lblExportPath.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.btnExport.Location = new System.Drawing.Point(240, 103);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(37, 32);
            this.btnExport.TabIndex = 7;
            this.btnExport.Text = ".....";
            this.btnExport.UseVisualStyleBackColor = false;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnImportPath
            // 
            this.btnImportPath.BackColor = System.Drawing.Color.Transparent;
            this.btnImportPath.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnImportPath.BackgroundImage")));
            this.btnImportPath.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnImportPath.FlatAppearance.BorderSize = 0;
            this.btnImportPath.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnImportPath.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnImportPath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImportPath.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImportPath.Location = new System.Drawing.Point(240, 143);
            this.btnImportPath.Name = "btnImportPath";
            this.btnImportPath.Size = new System.Drawing.Size(37, 32);
            this.btnImportPath.TabIndex = 7;
            this.btnImportPath.Text = ".....";
            this.btnImportPath.UseVisualStyleBackColor = false;
            this.btnImportPath.Click += new System.EventHandler(this.btnImportPath_Click);
            // 
            // lblImportPath
            // 
            this.lblImportPath.BackColor = System.Drawing.Color.Transparent;
            this.lblImportPath.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblImportPath.Location = new System.Drawing.Point(15, 99);
            this.lblImportPath.Name = "lblImportPath";
            this.lblImportPath.Size = new System.Drawing.Size(98, 17);
            this.lblImportPath.TabIndex = 0;
            this.lblImportPath.Text = "Import Path:*";
            this.lblImportPath.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtImportPath
            // 
            this.txtImportPath.Location = new System.Drawing.Point(128, 150);
            this.txtImportPath.Name = "txtImportPath";
            this.txtImportPath.Size = new System.Drawing.Size(106, 20);
            this.txtImportPath.TabIndex = 118;
            this.txtImportPath.TextChanged += new System.EventHandler(this.txtImportPath_TextChanged);
            this.txtImportPath.Validated += new System.EventHandler(this.txtImportPath_Validated);
            // 
            // grpDistributorSummary
            // 
            this.grpDistributorSummary.Controls.Add(this.rdoImport);
            this.grpDistributorSummary.Controls.Add(this.rdoExport);
            this.grpDistributorSummary.Location = new System.Drawing.Point(8, 2);
            this.grpDistributorSummary.Name = "grpDistributorSummary";
            this.grpDistributorSummary.Size = new System.Drawing.Size(439, 47);
            this.grpDistributorSummary.TabIndex = 119;
            this.grpDistributorSummary.TabStop = false;
            this.grpDistributorSummary.Text = "Functions";
            // 
            // rdoImport
            // 
            this.rdoImport.AutoSize = true;
            this.rdoImport.Location = new System.Drawing.Point(172, 19);
            this.rdoImport.Name = "rdoImport";
            this.rdoImport.Size = new System.Drawing.Size(54, 17);
            this.rdoImport.TabIndex = 0;
            this.rdoImport.TabStop = true;
            this.rdoImport.Text = "Import";
            this.rdoImport.UseVisualStyleBackColor = true;
            this.rdoImport.CheckedChanged += new System.EventHandler(this.rdoImport_CheckedChanged);
            // 
            // rdoExport
            // 
            this.rdoExport.AutoSize = true;
            this.rdoExport.Location = new System.Drawing.Point(72, 19);
            this.rdoExport.Name = "rdoExport";
            this.rdoExport.Size = new System.Drawing.Size(55, 17);
            this.rdoExport.TabIndex = 0;
            this.rdoExport.TabStop = true;
            this.rdoExport.Text = "Export";
            this.rdoExport.UseVisualStyleBackColor = true;
            this.rdoExport.CheckedChanged += new System.EventHandler(this.rdoExport_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblYear);
            this.groupBox1.Controls.Add(this.lblImportPath);
            this.groupBox1.Controls.Add(this.lblExportPath);
            this.groupBox1.Controls.Add(this.lblMonth);
            this.groupBox1.Location = new System.Drawing.Point(9, 54);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(438, 185);
            this.groupBox1.TabIndex = 120;
            this.groupBox1.TabStop = false;
            // 
            // frmDistributorPaymentSummary
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(233)))), ((int)(((byte)(251)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(454, 252);
            this.ControlBox = false;
            this.Controls.Add(this.txtImportPath);
            this.Controls.Add(this.txtExportPath);
            this.Controls.Add(this.cmbYear);
            this.Controls.Add(this.cmbStatus);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnImportPath);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.grpDistributorSummary);
            this.Controls.Add(this.groupBox1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDistributorPaymentSummary";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Distributor Payment Summary";
            ((System.ComponentModel.ISupportInitialize)(this.epUOM)).EndInit();
            this.grpDistributorSummary.ResumeLayout(false);
            this.grpDistributorSummary.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label lblMonth;
        private System.Windows.Forms.ErrorProvider epUOM;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.ComboBox cmbYear;
        private System.Windows.Forms.Label lblYear;
        private System.Windows.Forms.TextBox txtExportPath;
        private System.Windows.Forms.Label lblExportPath;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.TextBox txtImportPath;
        private System.Windows.Forms.Label lblImportPath;
        private System.Windows.Forms.Button btnImportPath;
        private System.Windows.Forms.GroupBox grpDistributorSummary;
        private System.Windows.Forms.RadioButton rdoImport;
        private System.Windows.Forms.RadioButton rdoExport;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}