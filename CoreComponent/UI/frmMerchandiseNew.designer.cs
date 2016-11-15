namespace CoreComponent.Hierarchies.UI
{
    partial class frmMerchandiseNew
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.txtParentName = new System.Windows.Forms.TextBox();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.txtDesc = new System.Windows.Forms.TextBox();
            this.lblDesc = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.lblName = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblCode = new System.Windows.Forms.Label();
            this.lblParentName = new System.Windows.Forms.Label();
            this.lblType = new System.Windows.Forms.Label();
            this.dgvMerchandise = new System.Windows.Forms.DataGridView();
            this.epMerchandise = new System.Windows.Forms.ErrorProvider(this.components);
            this.btnSearchParent = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.chkIsTradeable = new System.Windows.Forms.CheckBox();
            this.pnlGridSearch.SuspendLayout();
            this.pnlSearchHeader.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMerchandise)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.epMerchandise)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlGridSearch
            // 
            this.pnlGridSearch.Controls.Add(this.dgvMerchandise);
            this.pnlGridSearch.Location = new System.Drawing.Point(0, 178);
            this.pnlGridSearch.Size = new System.Drawing.Size(1007, 471);
            // 
            // pnlSearchHeader
            // 
            this.pnlSearchHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSearchHeader.Controls.Add(this.chkIsTradeable);
            this.pnlSearchHeader.Controls.Add(this.btnSearchParent);
            this.pnlSearchHeader.Controls.Add(this.txtParentName);
            this.pnlSearchHeader.Controls.Add(this.txtCode);
            this.pnlSearchHeader.Controls.Add(this.label1);
            this.pnlSearchHeader.Controls.Add(this.txtDesc);
            this.pnlSearchHeader.Controls.Add(this.lblDesc);
            this.pnlSearchHeader.Controls.Add(this.txtName);
            this.pnlSearchHeader.Controls.Add(this.cmbStatus);
            this.pnlSearchHeader.Controls.Add(this.cmbType);
            this.pnlSearchHeader.Controls.Add(this.lblName);
            this.pnlSearchHeader.Controls.Add(this.lblStatus);
            this.pnlSearchHeader.Controls.Add(this.lblCode);
            this.pnlSearchHeader.Controls.Add(this.lblParentName);
            this.pnlSearchHeader.Controls.Add(this.lblType);
            this.pnlSearchHeader.Size = new System.Drawing.Size(1007, 150);
            this.pnlSearchHeader.TabIndex = 0;
            this.pnlSearchHeader.Controls.SetChildIndex(this.pnlButtons, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblType, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblParentName, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblCode, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblStatus, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblName, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.cmbType, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.cmbStatus, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.txtName, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblDesc, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.txtDesc, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.label1, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.txtCode, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.txtParentName, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.btnSearchParent, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.chkIsTradeable, 0);
            // 
            // btnSave
            // 
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSave.Location = new System.Drawing.Point(855, 0);
            this.btnSave.Size = new System.Drawing.Size(75, 32);
            this.btnSave.TabIndex = 9;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearch.Location = new System.Drawing.Point(780, 0);
            this.btnSearch.Size = new System.Drawing.Size(75, 32);
            this.btnSearch.TabIndex = 8;
            this.btnSearch.Text = "S&earch";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnReset
            // 
            this.btnReset.FlatAppearance.BorderSize = 0;
            this.btnReset.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnReset.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnReset.Location = new System.Drawing.Point(930, 0);
            this.btnReset.Size = new System.Drawing.Size(75, 32);
            this.btnReset.TabIndex = 10;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // pnlButtons
            // 
            this.pnlButtons.Location = new System.Drawing.Point(0, 116);
            this.pnlButtons.Size = new System.Drawing.Size(1005, 32);
            // 
            // txtParentName
            // 
            this.txtParentName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtParentName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtParentName.Location = new System.Drawing.Point(539, 54);
            this.txtParentName.Name = "txtParentName";
            this.txtParentName.ReadOnly = true;
            this.txtParentName.Size = new System.Drawing.Size(110, 20);
            this.txtParentName.TabIndex = 5;
            this.txtParentName.TabStop = false;
            // 
            // txtCode
            // 
            this.txtCode.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtCode.Location = new System.Drawing.Point(539, 17);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(110, 20);
            this.txtCode.TabIndex = 2;
            this.txtCode.Validated += new System.EventHandler(this.txtCode_Validated);
            // 
            // txtDesc
            // 
            this.txtDesc.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtDesc.Location = new System.Drawing.Point(204, 54);
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.Size = new System.Drawing.Size(110, 20);
            this.txtDesc.TabIndex = 4;
            // 
            // lblDesc
            // 
            this.lblDesc.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblDesc.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDesc.Location = new System.Drawing.Point(36, 57);
            this.lblDesc.Margin = new System.Windows.Forms.Padding(0);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Size = new System.Drawing.Size(169, 13);
            this.lblDesc.TabIndex = 34;
            this.lblDesc.Text = "Merchandise Description:";
            this.lblDesc.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtName
            // 
            this.txtName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtName.Location = new System.Drawing.Point(858, 17);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(110, 20);
            this.txtName.TabIndex = 3;
            this.txtName.Validated += new System.EventHandler(this.txtName_Validated);
            // 
            // cmbStatus
            // 
            this.cmbStatus.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cmbStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Location = new System.Drawing.Point(858, 54);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(110, 21);
            this.cmbStatus.TabIndex = 7;
            this.cmbStatus.SelectedIndexChanged += new System.EventHandler(this.cmbStatus_SelectedIndexChanged);
            // 
            // cmbType
            // 
            this.cmbType.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cmbType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Location = new System.Drawing.Point(204, 17);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(110, 21);
            this.cmbType.TabIndex = 1;
            this.cmbType.SelectedIndexChanged += new System.EventHandler(this.cmbType_SelectedIndexChanged);
            // 
            // lblName
            // 
            this.lblName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblName.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(734, 20);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(127, 13);
            this.lblName.TabIndex = 33;
            this.lblName.Text = "Merchandise Name:*";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblStatus.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(731, 57);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(130, 13);
            this.lblStatus.TabIndex = 32;
            this.lblStatus.Text = "Merchandise Status:*";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCode
            // 
            this.lblCode.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblCode.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCode.Location = new System.Drawing.Point(381, 20);
            this.lblCode.Name = "lblCode";
            this.lblCode.Size = new System.Drawing.Size(161, 13);
            this.lblCode.TabIndex = 31;
            this.lblCode.Text = "Merchandise Code:*";
            this.lblCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblParentName
            // 
            this.lblParentName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblParentName.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblParentName.Location = new System.Drawing.Point(374, 57);
            this.lblParentName.Name = "lblParentName";
            this.lblParentName.Size = new System.Drawing.Size(168, 13);
            this.lblParentName.TabIndex = 30;
            this.lblParentName.Text = "Parent Merchandise Name:*";
            this.lblParentName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblType
            // 
            this.lblType.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblType.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblType.Location = new System.Drawing.Point(36, 20);
            this.lblType.Margin = new System.Windows.Forms.Padding(0);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(169, 13);
            this.lblType.TabIndex = 29;
            this.lblType.Text = "Merchandise Type:*";
            this.lblType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dgvMerchandise
            // 
            this.dgvMerchandise.AllowUserToAddRows = false;
            this.dgvMerchandise.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMerchandise.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvMerchandise.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvMerchandise.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvMerchandise.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMerchandise.Location = new System.Drawing.Point(0, 0);
            this.dgvMerchandise.MultiSelect = false;
            this.dgvMerchandise.Name = "dgvMerchandise";
            this.dgvMerchandise.ReadOnly = true;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvMerchandise.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvMerchandise.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMerchandise.Size = new System.Drawing.Size(1007, 471);
            this.dgvMerchandise.TabIndex = 11;
            this.dgvMerchandise.TabStop = false;
            this.dgvMerchandise.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvMerchandise_CellMouseClick);
            this.dgvMerchandise.SelectionChanged += new System.EventHandler(this.dgvMerchandise_SelectionChanged);
            // 
            // epMerchandise
            // 
            this.epMerchandise.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.epMerchandise.ContainerControl = this;
            // 
            // btnSearchParent
            // 
            this.btnSearchParent.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSearchParent.BackColor = System.Drawing.Color.Transparent;
            this.btnSearchParent.BackgroundImage = global::CoreComponent.Properties.Resources.uparrow_new;
            this.btnSearchParent.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearchParent.FlatAppearance.BorderSize = 0;
            this.btnSearchParent.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearchParent.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearchParent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchParent.Location = new System.Drawing.Point(651, 53);
            this.btnSearchParent.Name = "btnSearchParent";
            this.btnSearchParent.Size = new System.Drawing.Size(28, 21);
            this.btnSearchParent.TabIndex = 6;
            this.btnSearchParent.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSearchParent.UseVisualStyleBackColor = false;
            this.btnSearchParent.Click += new System.EventHandler(this.btnSearchParent_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(36, 87);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(169, 13);
            this.label1.TabIndex = 34;
            this.label1.Text = "Is Tradeable:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkIsTradeable
            // 
            this.chkIsTradeable.AutoSize = true;
            this.chkIsTradeable.Location = new System.Drawing.Point(204, 89);
            this.chkIsTradeable.Name = "chkIsTradeable";
            this.chkIsTradeable.Size = new System.Drawing.Size(15, 14);
            this.chkIsTradeable.TabIndex = 35;
            this.chkIsTradeable.UseVisualStyleBackColor = true;
            // 
            // frmMerchandiseNew
            // 
            this.AcceptButton = this.btnSearch;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1013, 707);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "frmMerchandiseNew";
            this.Text = "frmMerchandiseNew";
            this.WindowState = System.Windows.Forms.FormWindowState.Normal;
            this.Load += new System.EventHandler(this.frmMerchandiseNew_Load);
            this.pnlGridSearch.ResumeLayout(false);
            this.pnlSearchHeader.ResumeLayout(false);
            this.pnlSearchHeader.PerformLayout();
            this.pnlButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMerchandise)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.epMerchandise)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtParentName;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.TextBox txtDesc;
        private System.Windows.Forms.Label lblDesc;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblCode;
        private System.Windows.Forms.Label lblParentName;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.DataGridView dgvMerchandise;
        private System.Windows.Forms.ErrorProvider epMerchandise;
        private System.Windows.Forms.Button btnSearchParent;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkIsTradeable;
    }
}