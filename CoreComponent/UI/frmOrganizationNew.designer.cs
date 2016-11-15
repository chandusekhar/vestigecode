namespace CoreComponent.Hierarchies.UI
{
    partial class frmOrganizationNew
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
            this.txtCode = new System.Windows.Forms.TextBox();
            this.lblSearchOrgCode = new System.Windows.Forms.Label();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.txtParentName = new System.Windows.Forms.TextBox();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.lblSearchStatus = new System.Windows.Forms.Label();
            this.lblSearchParentOrgName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblSearchOrgName = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblCreateOrgDescription = new System.Windows.Forms.Label();
            this.btnShowTree = new System.Windows.Forms.Button();
            this.lblOrganizationType = new System.Windows.Forms.Label();
            this.dgvOrgSearch = new System.Windows.Forms.DataGridView();
            this.errOrganization = new System.Windows.Forms.ErrorProvider(this.components);
            this.lblZoneState = new System.Windows.Forms.Label();
            this.txtZoneState = new System.Windows.Forms.TextBox();
            this.btnZoneState = new System.Windows.Forms.Button();
            this.pnlGridSearch.SuspendLayout();
            this.pnlSearchHeader.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrgSearch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errOrganization)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlGridSearch
            // 
            this.pnlGridSearch.Controls.Add(this.dgvOrgSearch);
            this.pnlGridSearch.Location = new System.Drawing.Point(0, 188);
            this.pnlGridSearch.Size = new System.Drawing.Size(1007, 461);
            this.pnlGridSearch.TabIndex = 10;
            // 
            // pnlSearchHeader
            // 
            this.pnlSearchHeader.Controls.Add(this.btnZoneState);
            this.pnlSearchHeader.Controls.Add(this.btnShowTree);
            this.pnlSearchHeader.Controls.Add(this.txtDescription);
            this.pnlSearchHeader.Controls.Add(this.lblCreateOrgDescription);
            this.pnlSearchHeader.Controls.Add(this.txtZoneState);
            this.pnlSearchHeader.Controls.Add(this.txtParentName);
            this.pnlSearchHeader.Controls.Add(this.cmbStatus);
            this.pnlSearchHeader.Controls.Add(this.lblSearchStatus);
            this.pnlSearchHeader.Controls.Add(this.lblZoneState);
            this.pnlSearchHeader.Controls.Add(this.lblSearchParentOrgName);
            this.pnlSearchHeader.Controls.Add(this.txtName);
            this.pnlSearchHeader.Controls.Add(this.lblSearchOrgName);
            this.pnlSearchHeader.Controls.Add(this.txtCode);
            this.pnlSearchHeader.Controls.Add(this.lblSearchOrgCode);
            this.pnlSearchHeader.Controls.Add(this.cmbType);
            this.pnlSearchHeader.Controls.Add(this.lblOrganizationType);
            this.pnlSearchHeader.Size = new System.Drawing.Size(1007, 160);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblOrganizationType, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.cmbType, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblSearchOrgCode, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.txtCode, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblSearchOrgName, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.txtName, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblSearchParentOrgName, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblZoneState, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblSearchStatus, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.cmbStatus, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.txtParentName, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.txtZoneState, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblCreateOrgDescription, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.txtDescription, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.btnShowTree, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.btnZoneState, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.pnlButtons, 0);
            // 
            // btnSave
            // 
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSave.TabIndex = 1;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearch.TabIndex = 0;
            this.btnSearch.Text = "S&earch";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnReset
            // 
            this.btnReset.FlatAppearance.BorderSize = 0;
            this.btnReset.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnReset.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnReset.TabIndex = 2;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // pnlButtons
            // 
            this.pnlButtons.Location = new System.Drawing.Point(0, 126);
            // 
            // txtCode
            // 
            this.txtCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtCode.Location = new System.Drawing.Point(525, 21);
            this.txtCode.MaxLength = 50;
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(110, 20);
            this.txtCode.TabIndex = 1;
            this.txtCode.Validated += new System.EventHandler(this.txtCode_Validated);
            // 
            // lblSearchOrgCode
            // 
            this.lblSearchOrgCode.AutoSize = true;
            this.lblSearchOrgCode.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.lblSearchOrgCode.Location = new System.Drawing.Point(401, 25);
            this.lblSearchOrgCode.Name = "lblSearchOrgCode";
            this.lblSearchOrgCode.Size = new System.Drawing.Size(125, 13);
            this.lblSearchOrgCode.TabIndex = 32;
            this.lblSearchOrgCode.Text = "Organization Code:*";
            // 
            // cmbType
            // 
            this.cmbType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Location = new System.Drawing.Point(143, 22);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(110, 21);
            this.cmbType.TabIndex = 0;
            this.cmbType.SelectedIndexChanged += new System.EventHandler(this.cmbType_SelectedIndexChanged);
            // 
            // txtParentName
            // 
            this.txtParentName.Location = new System.Drawing.Point(525, 57);
            this.txtParentName.Name = "txtParentName";
            this.txtParentName.Size = new System.Drawing.Size(110, 20);
            this.txtParentName.TabIndex = 9;
            this.txtParentName.TabStop = false;
            this.txtParentName.Validated += new System.EventHandler(this.txtParentName_Validated);
            // 
            // cmbStatus
            // 
            this.cmbStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Location = new System.Drawing.Point(835, 57);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(110, 21);
            this.cmbStatus.TabIndex = 5;
            this.cmbStatus.SelectedIndexChanged += new System.EventHandler(this.cmbStatus_SelectedIndexChanged);
            // 
            // lblSearchStatus
            // 
            this.lblSearchStatus.AutoSize = true;
            this.lblSearchStatus.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.lblSearchStatus.Location = new System.Drawing.Point(780, 61);
            this.lblSearchStatus.Name = "lblSearchStatus";
            this.lblSearchStatus.Size = new System.Drawing.Size(59, 13);
            this.lblSearchStatus.TabIndex = 37;
            this.lblSearchStatus.Text = "Status:* ";
            // 
            // lblSearchParentOrgName
            // 
            this.lblSearchParentOrgName.AutoSize = true;
            this.lblSearchParentOrgName.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.lblSearchParentOrgName.Location = new System.Drawing.Point(411, 62);
            this.lblSearchParentOrgName.Name = "lblSearchParentOrgName";
            this.lblSearchParentOrgName.Size = new System.Drawing.Size(115, 13);
            this.lblSearchParentOrgName.TabIndex = 36;
            this.lblSearchParentOrgName.Text = "Parent Org. Name:";
            // 
            // txtName
            // 
            this.txtName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtName.Location = new System.Drawing.Point(835, 21);
            this.txtName.MaxLength = 50;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(110, 20);
            this.txtName.TabIndex = 2;
            this.txtName.Validated += new System.EventHandler(this.txtName_Validated);
            // 
            // lblSearchOrgName
            // 
            this.lblSearchOrgName.AutoSize = true;
            this.lblSearchOrgName.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.lblSearchOrgName.Location = new System.Drawing.Point(709, 25);
            this.lblSearchOrgName.Name = "lblSearchOrgName";
            this.lblSearchOrgName.Size = new System.Drawing.Size(132, 13);
            this.lblSearchOrgName.TabIndex = 34;
            this.lblSearchOrgName.Text = "Organization Name:* ";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(143, 58);
            this.txtDescription.MaxLength = 500;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(213, 20);
            this.txtDescription.TabIndex = 3;
            // 
            // lblCreateOrgDescription
            // 
            this.lblCreateOrgDescription.AutoSize = true;
            this.lblCreateOrgDescription.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.lblCreateOrgDescription.Location = new System.Drawing.Point(65, 61);
            this.lblCreateOrgDescription.Name = "lblCreateOrgDescription";
            this.lblCreateOrgDescription.Size = new System.Drawing.Size(76, 13);
            this.lblCreateOrgDescription.TabIndex = 40;
            this.lblCreateOrgDescription.Text = "Description:";
            // 
            // btnShowTree
            // 
            this.btnShowTree.BackColor = System.Drawing.Color.Transparent;
            this.btnShowTree.BackgroundImage = global::CoreComponent.Properties.Resources.uparrow_new;
            this.btnShowTree.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnShowTree.FlatAppearance.BorderSize = 0;
            this.btnShowTree.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnShowTree.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnShowTree.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShowTree.Location = new System.Drawing.Point(641, 57);
            this.btnShowTree.Name = "btnShowTree";
            this.btnShowTree.Size = new System.Drawing.Size(28, 20);
            this.btnShowTree.TabIndex = 4;
            this.btnShowTree.UseVisualStyleBackColor = false;
            this.btnShowTree.Click += new System.EventHandler(this.btnShowTree_Click);
            // 
            // lblOrganizationType
            // 
            this.lblOrganizationType.AutoSize = true;
            this.lblOrganizationType.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.lblOrganizationType.Location = new System.Drawing.Point(17, 25);
            this.lblOrganizationType.Name = "lblOrganizationType";
            this.lblOrganizationType.Size = new System.Drawing.Size(127, 13);
            this.lblOrganizationType.TabIndex = 43;
            this.lblOrganizationType.Text = "Organization Type: *";
            // 
            // dgvOrgSearch
            // 
            this.dgvOrgSearch.AllowUserToAddRows = false;
            this.dgvOrgSearch.AllowUserToDeleteRows = false;
            this.dgvOrgSearch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrgSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOrgSearch.Location = new System.Drawing.Point(0, 0);
            this.dgvOrgSearch.MultiSelect = false;
            this.dgvOrgSearch.Name = "dgvOrgSearch";
            this.dgvOrgSearch.ReadOnly = true;
            this.dgvOrgSearch.RowHeadersVisible = false;
            this.dgvOrgSearch.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOrgSearch.Size = new System.Drawing.Size(1007, 461);
            this.dgvOrgSearch.TabIndex = 0;
            this.dgvOrgSearch.TabStop = false;
            this.dgvOrgSearch.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvOrgSearch_CellMouseClick);
            this.dgvOrgSearch.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvOrgSearch_RowHeaderMouseClick);
            this.dgvOrgSearch.SelectionChanged += new System.EventHandler(this.dgvOrgSearch_SelectionChanged);
            this.dgvOrgSearch.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvOrgSearch_CellContentClick);
            // 
            // errOrganization
            // 
            this.errOrganization.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errOrganization.ContainerControl = this;
            // 
            // lblZoneState
            // 
            this.lblZoneState.AutoSize = true;
            this.lblZoneState.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.lblZoneState.Location = new System.Drawing.Point(52, 94);
            this.lblZoneState.Name = "lblZoneState";
            this.lblZoneState.Size = new System.Drawing.Size(89, 13);
            this.lblZoneState.TabIndex = 36;
            this.lblZoneState.Text = "Attach State:*";
            // 
            // txtZoneState
            // 
            this.txtZoneState.Enabled = false;
            this.txtZoneState.Location = new System.Drawing.Point(143, 91);
            this.txtZoneState.Name = "txtZoneState";
            this.txtZoneState.Size = new System.Drawing.Size(110, 20);
            this.txtZoneState.TabIndex = 10;
            this.txtZoneState.TabStop = false;
            this.txtZoneState.Validated += new System.EventHandler(this.txtParentName_Validated);
            // 
            // btnZoneState
            // 
            this.btnZoneState.BackColor = System.Drawing.Color.Transparent;
            this.btnZoneState.BackgroundImage = global::CoreComponent.Properties.Resources.uparrow_new;
            this.btnZoneState.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnZoneState.FlatAppearance.BorderSize = 0;
            this.btnZoneState.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnZoneState.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnZoneState.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnZoneState.Location = new System.Drawing.Point(259, 91);
            this.btnZoneState.Name = "btnZoneState";
            this.btnZoneState.Size = new System.Drawing.Size(28, 20);
            this.btnZoneState.TabIndex = 6;
            this.btnZoneState.UseVisualStyleBackColor = false;
            this.btnZoneState.Click += new System.EventHandler(this.btnShowState_Click);
            // 
            // frmOrganizationNew
            // 
            this.AcceptButton = this.btnSearch;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1013, 707);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "frmOrganizationNew";
            this.Text = "frmOrganizationNew";
            this.WindowState = System.Windows.Forms.FormWindowState.Normal;
            this.pnlGridSearch.ResumeLayout(false);
            this.pnlSearchHeader.ResumeLayout(false);
            this.pnlSearchHeader.PerformLayout();
            this.pnlButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrgSearch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errOrganization)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Label lblSearchOrgCode;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.TextBox txtParentName;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Label lblSearchStatus;
        private System.Windows.Forms.Label lblSearchParentOrgName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblSearchOrgName;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblCreateOrgDescription;
        private System.Windows.Forms.Button btnShowTree;
        private System.Windows.Forms.Label lblOrganizationType;
        private System.Windows.Forms.DataGridView dgvOrgSearch;
        private System.Windows.Forms.ErrorProvider errOrganization;
        private System.Windows.Forms.TextBox txtZoneState;
        private System.Windows.Forms.Label lblZoneState;
        private System.Windows.Forms.Button btnZoneState;
    }
}