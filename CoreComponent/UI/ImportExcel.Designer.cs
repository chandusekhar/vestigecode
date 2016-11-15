namespace CoreComponent.UI
{
    partial class ImportExcel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportExcel));
            this.txtImportPath = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblImportPath = new System.Windows.Forms.Label();
            this.btnImportPath = new System.Windows.Forms.Button();
            this.btnUpload = new System.Windows.Forms.Button();
            this.cmbDataType = new System.Windows.Forms.ComboBox();
            this.lblDataFileType = new System.Windows.Forms.Label();
            this.lblBusiMonth = new System.Windows.Forms.Label();
            this.cmbMonth = new System.Windows.Forms.ComboBox();
            this.btnExportToExcel = new System.Windows.Forms.Button();
            this.txtExportPath = new System.Windows.Forms.TextBox();
            this.lblExportPath = new System.Windows.Forms.Label();
            this.btnShowDialogForExport = new System.Windows.Forms.Button();
            this.rdbImport = new System.Windows.Forms.RadioButton();
            this.rdbExport = new System.Windows.Forms.RadioButton();
            this.cmbYear = new System.Windows.Forms.ComboBox();
            this.cmbPaymentMode = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtImportPath
            // 
            this.txtImportPath.Location = new System.Drawing.Point(125, 76);
            this.txtImportPath.Name = "txtImportPath";
            this.txtImportPath.Size = new System.Drawing.Size(245, 20);
            this.txtImportPath.TabIndex = 123;
            this.txtImportPath.TextChanged += new System.EventHandler(this.txtImportPath_TextChanged);
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
            this.btnCancel.Location = new System.Drawing.Point(419, 157);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 32);
            this.btnCancel.TabIndex = 122;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblImportPath
            // 
            this.lblImportPath.BackColor = System.Drawing.Color.Transparent;
            this.lblImportPath.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblImportPath.Location = new System.Drawing.Point(12, 77);
            this.lblImportPath.Name = "lblImportPath";
            this.lblImportPath.Size = new System.Drawing.Size(98, 17);
            this.lblImportPath.TabIndex = 119;
            this.lblImportPath.Text = "Import Path:*";
            this.lblImportPath.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblImportPath.Click += new System.EventHandler(this.lblImportPath_Click);
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
            this.btnImportPath.Location = new System.Drawing.Point(376, 69);
            this.btnImportPath.Name = "btnImportPath";
            this.btnImportPath.Size = new System.Drawing.Size(37, 32);
            this.btnImportPath.TabIndex = 120;
            this.btnImportPath.Text = ".....";
            this.btnImportPath.UseVisualStyleBackColor = false;
            this.btnImportPath.Click += new System.EventHandler(this.btnImportPath_Click);
            // 
            // btnUpload
            // 
            this.btnUpload.BackColor = System.Drawing.Color.Transparent;
            this.btnUpload.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnUpload.BackgroundImage")));
            this.btnUpload.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnUpload.FlatAppearance.BorderSize = 0;
            this.btnUpload.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnUpload.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnUpload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpload.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpload.Location = new System.Drawing.Point(419, 69);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(75, 32);
            this.btnUpload.TabIndex = 121;
            this.btnUpload.Text = "&Upload";
            this.btnUpload.UseVisualStyleBackColor = false;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // cmbDataType
            // 
            this.cmbDataType.FormattingEnabled = true;
            this.cmbDataType.Location = new System.Drawing.Point(125, 39);
            this.cmbDataType.Name = "cmbDataType";
            this.cmbDataType.Size = new System.Drawing.Size(121, 21);
            this.cmbDataType.TabIndex = 124;
            this.cmbDataType.SelectedIndexChanged += new System.EventHandler(this.cmbDataType_SelectedIndexChanged);
            // 
            // lblDataFileType
            // 
            this.lblDataFileType.BackColor = System.Drawing.Color.Transparent;
            this.lblDataFileType.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDataFileType.Location = new System.Drawing.Point(12, 43);
            this.lblDataFileType.Name = "lblDataFileType";
            this.lblDataFileType.Size = new System.Drawing.Size(98, 17);
            this.lblDataFileType.TabIndex = 125;
            this.lblDataFileType.Text = "Data Type:*";
            this.lblDataFileType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblDataFileType.Click += new System.EventHandler(this.lblDataFileType_Click);
            // 
            // lblBusiMonth
            // 
            this.lblBusiMonth.BackColor = System.Drawing.Color.Transparent;
            this.lblBusiMonth.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBusiMonth.Location = new System.Drawing.Point(187, 12);
            this.lblBusiMonth.Name = "lblBusiMonth";
            this.lblBusiMonth.Size = new System.Drawing.Size(138, 12);
            this.lblBusiMonth.TabIndex = 126;
            this.lblBusiMonth.Text = "Business Month-Year:*";
            this.lblBusiMonth.Click += new System.EventHandler(this.lblBusiMonth_Click);
            // 
            // cmbMonth
            // 
            this.cmbMonth.FormattingEnabled = true;
            this.cmbMonth.Location = new System.Drawing.Point(331, 8);
            this.cmbMonth.Name = "cmbMonth";
            this.cmbMonth.Size = new System.Drawing.Size(104, 21);
            this.cmbMonth.TabIndex = 127;
            this.cmbMonth.SelectedIndexChanged += new System.EventHandler(this.cmbMonth_SelectedIndexChanged);
            // 
            // btnExportToExcel
            // 
            this.btnExportToExcel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExportToExcel.BackgroundImage")));
            this.btnExportToExcel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExportToExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportToExcel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnExportToExcel.Location = new System.Drawing.Point(419, 119);
            this.btnExportToExcel.Name = "btnExportToExcel";
            this.btnExportToExcel.Size = new System.Drawing.Size(80, 32);
            this.btnExportToExcel.TabIndex = 129;
            this.btnExportToExcel.Text = "Get Excel";
            this.btnExportToExcel.UseVisualStyleBackColor = true;
            this.btnExportToExcel.Click += new System.EventHandler(this.btnExportToExcel_Click);
            // 
            // txtExportPath
            // 
            this.txtExportPath.Location = new System.Drawing.Point(125, 123);
            this.txtExportPath.Name = "txtExportPath";
            this.txtExportPath.Size = new System.Drawing.Size(245, 20);
            this.txtExportPath.TabIndex = 132;
            this.txtExportPath.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // lblExportPath
            // 
            this.lblExportPath.BackColor = System.Drawing.Color.Transparent;
            this.lblExportPath.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExportPath.Location = new System.Drawing.Point(4, 124);
            this.lblExportPath.Name = "lblExportPath";
            this.lblExportPath.Size = new System.Drawing.Size(98, 17);
            this.lblExportPath.TabIndex = 130;
            this.lblExportPath.Text = "Export Path:*";
            this.lblExportPath.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnShowDialogForExport
            // 
            this.btnShowDialogForExport.BackColor = System.Drawing.Color.Transparent;
            this.btnShowDialogForExport.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnShowDialogForExport.BackgroundImage")));
            this.btnShowDialogForExport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnShowDialogForExport.FlatAppearance.BorderSize = 0;
            this.btnShowDialogForExport.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnShowDialogForExport.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnShowDialogForExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShowDialogForExport.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShowDialogForExport.Location = new System.Drawing.Point(376, 116);
            this.btnShowDialogForExport.Name = "btnShowDialogForExport";
            this.btnShowDialogForExport.Size = new System.Drawing.Size(37, 32);
            this.btnShowDialogForExport.TabIndex = 131;
            this.btnShowDialogForExport.Text = ".....";
            this.btnShowDialogForExport.UseVisualStyleBackColor = false;
            this.btnShowDialogForExport.Click += new System.EventHandler(this.btnShowDialogForExport_Click);
            // 
            // rdbImport
            // 
            this.rdbImport.AutoSize = true;
            this.rdbImport.Location = new System.Drawing.Point(25, 7);
            this.rdbImport.Name = "rdbImport";
            this.rdbImport.Size = new System.Drawing.Size(54, 17);
            this.rdbImport.TabIndex = 133;
            this.rdbImport.TabStop = true;
            this.rdbImport.Text = "Import";
            this.rdbImport.UseVisualStyleBackColor = true;
            this.rdbImport.CheckedChanged += new System.EventHandler(this.rdbImport_CheckedChanged);
            // 
            // rdbExport
            // 
            this.rdbExport.AutoSize = true;
            this.rdbExport.Location = new System.Drawing.Point(85, 7);
            this.rdbExport.Name = "rdbExport";
            this.rdbExport.Size = new System.Drawing.Size(55, 17);
            this.rdbExport.TabIndex = 134;
            this.rdbExport.TabStop = true;
            this.rdbExport.Text = "Export";
            this.rdbExport.UseVisualStyleBackColor = true;
            this.rdbExport.CheckedChanged += new System.EventHandler(this.rdbExport_CheckedChanged);
            // 
            // cmbYear
            // 
            this.cmbYear.FormattingEnabled = true;
            this.cmbYear.Location = new System.Drawing.Point(441, 8);
            this.cmbYear.Name = "cmbYear";
            this.cmbYear.Size = new System.Drawing.Size(58, 21);
            this.cmbYear.TabIndex = 135;
            this.cmbYear.SelectedIndexChanged += new System.EventHandler(this.cmbYear_SelectedIndexChanged_1);
            // 
            // cmbPaymentMode
            // 
            this.cmbPaymentMode.FormattingEnabled = true;
            this.cmbPaymentMode.Location = new System.Drawing.Point(125, 157);
            this.cmbPaymentMode.Name = "cmbPaymentMode";
            this.cmbPaymentMode.Size = new System.Drawing.Size(121, 21);
            this.cmbPaymentMode.TabIndex = 136;
            this.cmbPaymentMode.SelectedIndexChanged += new System.EventHandler(this.cmbPaymentMode_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 160);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 137;
            this.label1.Text = "Payment Mode:";
            // 
            // ImportExcel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(233)))), ((int)(((byte)(251)))));
            this.ClientSize = new System.Drawing.Size(511, 201);
            this.ControlBox = false;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbPaymentMode);
            this.Controls.Add(this.cmbYear);
            this.Controls.Add(this.rdbExport);
            this.Controls.Add(this.rdbImport);
            this.Controls.Add(this.txtExportPath);
            this.Controls.Add(this.lblExportPath);
            this.Controls.Add(this.btnShowDialogForExport);
            this.Controls.Add(this.btnExportToExcel);
            this.Controls.Add(this.cmbMonth);
            this.Controls.Add(this.lblBusiMonth);
            this.Controls.Add(this.lblDataFileType);
            this.Controls.Add(this.cmbDataType);
            this.Controls.Add(this.txtImportPath);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblImportPath);
            this.Controls.Add(this.btnImportPath);
            this.Controls.Add(this.btnUpload);
            this.Name = "ImportExcel";
            this.ShowIcon = false;
            this.Text = "ImportExcel";
            this.Load += new System.EventHandler(this.ImportExcel_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtImportPath;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblImportPath;
        private System.Windows.Forms.Button btnImportPath;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.ComboBox cmbDataType;
        private System.Windows.Forms.Label lblDataFileType;
        private System.Windows.Forms.Label lblBusiMonth;
        private System.Windows.Forms.ComboBox cmbMonth;
        private System.Windows.Forms.Button btnExportToExcel;
        private System.Windows.Forms.TextBox txtExportPath;
        private System.Windows.Forms.Label lblExportPath;
        private System.Windows.Forms.Button btnShowDialogForExport;
        private System.Windows.Forms.RadioButton rdbImport;
        private System.Windows.Forms.RadioButton rdbExport;
        private System.Windows.Forms.ComboBox cmbYear;
        private System.Windows.Forms.ComboBox cmbPaymentMode;
        private System.Windows.Forms.Label label1;
    }
}