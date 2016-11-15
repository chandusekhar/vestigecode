namespace CoreComponent.MasterData.UI
{
    partial class frmItemCodeMapping
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmItemCodeMapping));
            this.lblFromitemcode = new System.Windows.Forms.Label();
            this.txtitemcode = new System.Windows.Forms.TextBox();
            this.lblfrmitemname = new System.Windows.Forms.Label();
            this.txtfitemName = new System.Windows.Forms.TextBox();
            this.lbltoitemCode = new System.Windows.Forms.Label();
            this.txtToItemCode = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.llbCreatedDate = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dgvItemCodeMapping = new System.Windows.Forms.DataGridView();
            this.lblToItemName = new System.Windows.Forms.Label();
            this.txtToItemName = new System.Windows.Forms.TextBox();
            this.errorSave = new System.Windows.Forms.ErrorProvider(this.components);
            this.cmbBoxStatus = new System.Windows.Forms.ComboBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnPrint = new System.Windows.Forms.Button();
            this.pnlHierarchyTemplate.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            this.pnlGridSearch.SuspendLayout();
            this.pnlSearchHeader.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemCodeMapping)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorSave)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHierarchyTemplate
            // 
            this.pnlHierarchyTemplate.Size = new System.Drawing.Size(856, 617);
            // 
            // pnlButtons
            // 
            this.pnlButtons.Controls.Add(this.btnPrint);
            this.pnlButtons.Size = new System.Drawing.Size(854, 27);
            this.pnlButtons.Controls.SetChildIndex(this.btnPrint, 0);
            this.pnlButtons.Controls.SetChildIndex(this.btnReset, 0);
            this.pnlButtons.Controls.SetChildIndex(this.btnSave, 0);
            this.pnlButtons.Controls.SetChildIndex(this.btnSearch, 0);
            // 
            // btnSave
            // 
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSave.Location = new System.Drawing.Point(629, 0);
            this.btnSave.TabIndex = 1;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearch.Location = new System.Drawing.Point(554, 0);
            this.btnSearch.TabIndex = 0;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnReset
            // 
            this.btnReset.FlatAppearance.BorderSize = 0;
            this.btnReset.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnReset.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnReset.Location = new System.Drawing.Point(704, 0);
            this.btnReset.TabIndex = 2;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // pnlGridSearch
            // 
            this.pnlGridSearch.Controls.Add(this.dgvItemCodeMapping);
            this.pnlGridSearch.Size = new System.Drawing.Size(856, 441);
            // 
            // pnlSearchHeader
            // 
            this.pnlSearchHeader.Controls.Add(this.lblStatus);
            this.pnlSearchHeader.Controls.Add(this.cmbBoxStatus);
            this.pnlSearchHeader.Controls.Add(this.lblToItemName);
            this.pnlSearchHeader.Controls.Add(this.txtToItemName);
            this.pnlSearchHeader.Controls.Add(this.dateTimePicker1);
            this.pnlSearchHeader.Controls.Add(this.llbCreatedDate);
            this.pnlSearchHeader.Controls.Add(this.pictureBox2);
            this.pnlSearchHeader.Controls.Add(this.pictureBox1);
            this.pnlSearchHeader.Controls.Add(this.lbltoitemCode);
            this.pnlSearchHeader.Controls.Add(this.txtToItemCode);
            this.pnlSearchHeader.Controls.Add(this.lblfrmitemname);
            this.pnlSearchHeader.Controls.Add(this.txtfitemName);
            this.pnlSearchHeader.Controls.Add(this.lblFromitemcode);
            this.pnlSearchHeader.Controls.Add(this.txtitemcode);
            this.pnlSearchHeader.Size = new System.Drawing.Size(856, 152);
            this.pnlSearchHeader.Controls.SetChildIndex(this.txtitemcode, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblFromitemcode, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.pnlButtons, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.txtfitemName, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblfrmitemname, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.txtToItemCode, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lbltoitemCode, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.pictureBox1, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.pictureBox2, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.llbCreatedDate, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.dateTimePicker1, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.txtToItemName, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblToItemName, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.cmbBoxStatus, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblStatus, 0);
            // 
            // lblSearchResult
            // 
            this.lblSearchResult.Size = new System.Drawing.Size(856, 24);
            // 
            // tabPage1
            // 
            this.tabPage1.Size = new System.Drawing.Size(862, 623);
            // 
            // btnExit
            // 
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.Location = new System.Drawing.Point(1488, 668);
            // 
            // lblPageTitle
            // 
            this.lblPageTitle.Location = new System.Drawing.Point(1165, 9);
            // 
            // button1
            // 
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.button1.Location = new System.Drawing.Point(118, 0);
            this.button1.Visible = false;
            // 
            // btnFirstRecord
            // 
            this.btnFirstRecord.FlatAppearance.BorderSize = 0;
            this.btnFirstRecord.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnFirstRecord.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnFirstRecord.Location = new System.Drawing.Point(-26, 0);
            this.btnFirstRecord.Visible = false;
            // 
            // btnPreviousRecord
            // 
            this.btnPreviousRecord.FlatAppearance.BorderSize = 0;
            this.btnPreviousRecord.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnPreviousRecord.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnPreviousRecord.Location = new System.Drawing.Point(10, 0);
            this.btnPreviousRecord.Visible = false;
            // 
            // btnNextRecord
            // 
            this.btnNextRecord.FlatAppearance.BorderSize = 0;
            this.btnNextRecord.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnNextRecord.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnNextRecord.Location = new System.Drawing.Point(46, 0);
            this.btnNextRecord.Visible = false;
            // 
            // btnLastRecord
            // 
            this.btnLastRecord.FlatAppearance.BorderSize = 0;
            this.btnLastRecord.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnLastRecord.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnLastRecord.Location = new System.Drawing.Point(82, 0);
            this.btnLastRecord.Visible = false;
            // 
            // lblFromitemcode
            // 
            this.lblFromitemcode.AutoEllipsis = true;
            this.lblFromitemcode.Location = new System.Drawing.Point(9, 10);
            this.lblFromitemcode.Name = "lblFromitemcode";
            this.lblFromitemcode.Size = new System.Drawing.Size(119, 21);
            this.lblFromitemcode.TabIndex = 0;
            this.lblFromitemcode.Text = "From Item Code*";
            this.lblFromitemcode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtitemcode
            // 
            this.txtitemcode.Location = new System.Drawing.Point(133, 10);
            this.txtitemcode.Name = "txtitemcode";
            this.txtitemcode.Size = new System.Drawing.Size(150, 20);
            this.txtitemcode.TabIndex = 1;
            this.txtitemcode.Tag = "FromItem";
            this.txtitemcode.Validated += new System.EventHandler(this.txtitemcode_Validated);
            this.txtitemcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtitemcode_KeyDown);
            // 
            // lblfrmitemname
            // 
            this.lblfrmitemname.Location = new System.Drawing.Point(322, 10);
            this.lblfrmitemname.Name = "lblfrmitemname";
            this.lblfrmitemname.Size = new System.Drawing.Size(100, 20);
            this.lblfrmitemname.TabIndex = 2;
            this.lblfrmitemname.Text = "From Item Name";
            this.lblfrmitemname.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtfitemName
            // 
            this.txtfitemName.Location = new System.Drawing.Point(426, 10);
            this.txtfitemName.Name = "txtfitemName";
            this.txtfitemName.Size = new System.Drawing.Size(159, 20);
            this.txtfitemName.TabIndex = 3;
            // 
            // lbltoitemCode
            // 
            this.lbltoitemCode.Location = new System.Drawing.Point(587, 10);
            this.lbltoitemCode.Name = "lbltoitemCode";
            this.lbltoitemCode.Size = new System.Drawing.Size(105, 21);
            this.lbltoitemCode.TabIndex = 4;
            this.lbltoitemCode.Text = "To Item Code*";
            this.lbltoitemCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtToItemCode
            // 
            this.txtToItemCode.Location = new System.Drawing.Point(699, 10);
            this.txtToItemCode.Name = "txtToItemCode";
            this.txtToItemCode.Size = new System.Drawing.Size(100, 20);
            this.txtToItemCode.TabIndex = 5;
            this.txtToItemCode.Tag = "ToItem";
            this.txtToItemCode.Validated += new System.EventHandler(this.txtitemcode_Validated);
            this.txtToItemCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtitemcode_KeyDown);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::CoreComponent.Properties.Resources.find;
            this.pictureBox1.Location = new System.Drawing.Point(285, 10);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(21, 21);
            this.pictureBox1.TabIndex = 94;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::CoreComponent.Properties.Resources.find;
            this.pictureBox2.Location = new System.Drawing.Point(800, 10);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(21, 21);
            this.pictureBox2.TabIndex = 95;
            this.pictureBox2.TabStop = false;
            // 
            // llbCreatedDate
            // 
            this.llbCreatedDate.Location = new System.Drawing.Point(609, 51);
            this.llbCreatedDate.Name = "llbCreatedDate";
            this.llbCreatedDate.Size = new System.Drawing.Size(100, 20);
            this.llbCreatedDate.TabIndex = 8;
            this.llbCreatedDate.Text = "Created Date";
            this.llbCreatedDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.llbCreatedDate.Visible = false;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(713, 51);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(106, 20);
            this.dateTimePicker1.TabIndex = 11;
            this.dateTimePicker1.Visible = false;
            // 
            // dgvItemCodeMapping
            // 
            this.dgvItemCodeMapping.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvItemCodeMapping.Location = new System.Drawing.Point(0, 0);
            this.dgvItemCodeMapping.Name = "dgvItemCodeMapping";
            this.dgvItemCodeMapping.Size = new System.Drawing.Size(856, 441);
            this.dgvItemCodeMapping.TabIndex = 34;
            this.dgvItemCodeMapping.TabStop = false;
            this.dgvItemCodeMapping.SelectionChanged += new System.EventHandler(this.dgvItemCodeMapping_SelectionChanged);
            this.dgvItemCodeMapping.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvItemCodeMapping_CellContentClick);
            // 
            // lblToItemName
            // 
            this.lblToItemName.Location = new System.Drawing.Point(25, 52);
            this.lblToItemName.Name = "lblToItemName";
            this.lblToItemName.Size = new System.Drawing.Size(100, 20);
            this.lblToItemName.TabIndex = 6;
            this.lblToItemName.Text = "To Item Name";
            this.lblToItemName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtToItemName
            // 
            this.txtToItemName.Location = new System.Drawing.Point(133, 52);
            this.txtToItemName.Name = "txtToItemName";
            this.txtToItemName.Size = new System.Drawing.Size(150, 20);
            this.txtToItemName.TabIndex = 7;
            // 
            // errorSave
            // 
            this.errorSave.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorSave.ContainerControl = this;
            // 
            // cmbBoxStatus
            // 
            this.cmbBoxStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBoxStatus.FormattingEnabled = true;
            this.cmbBoxStatus.Location = new System.Drawing.Point(426, 51);
            this.cmbBoxStatus.Name = "cmbBoxStatus";
            this.cmbBoxStatus.Size = new System.Drawing.Size(100, 21);
            this.cmbBoxStatus.TabIndex = 9;
            this.cmbBoxStatus.SelectedIndexChanged += new System.EventHandler(this.cmbBoxStatus_SelectedIndexChanged);
            this.cmbBoxStatus.Validated += new System.EventHandler(this.cmbBoxStatus_Validated);
            // 
            // lblStatus
            // 
            this.lblStatus.Location = new System.Drawing.Point(318, 51);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(100, 20);
            this.lblStatus.TabIndex = 97;
            this.lblStatus.Text = "Status";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.Color.Transparent;
            this.btnPrint.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPrint.BackgroundImage")));
            this.btnPrint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPrint.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.Location = new System.Drawing.Point(779, 0);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 27);
            this.btnPrint.TabIndex = 3;
            this.btnPrint.Text = "&Print";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.button2_Click);
            // 
            // frmItemCodeMapping
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(870, 707);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmItemCodeMapping";
            this.Text = "frmItemCodeMapping";
            this.Load += new System.EventHandler(this.frmItemCodeMapping_Load);
            this.pnlHierarchyTemplate.ResumeLayout(false);
            this.pnlButtons.ResumeLayout(false);
            this.pnlGridSearch.ResumeLayout(false);
            this.pnlSearchHeader.ResumeLayout(false);
            this.pnlSearchHeader.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemCodeMapping)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorSave)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblFromitemcode;
        private System.Windows.Forms.TextBox txtitemcode;
        private System.Windows.Forms.Label lbltoitemCode;
        private System.Windows.Forms.TextBox txtToItemCode;
        private System.Windows.Forms.Label lblfrmitemname;
        private System.Windows.Forms.TextBox txtfitemName;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label llbCreatedDate;
        private System.Windows.Forms.DataGridView dgvItemCodeMapping;
        private System.Windows.Forms.Label lblToItemName;
        private System.Windows.Forms.TextBox txtToItemName;
        private System.Windows.Forms.ErrorProvider errorSave;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cmbBoxStatus;
        protected System.Windows.Forms.Button btnPrint;
    }
}