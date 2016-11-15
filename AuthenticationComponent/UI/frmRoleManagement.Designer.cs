namespace AuthenticationComponent.UI
{
    partial class frmRoleManagement
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRoleManagement));
            this.panel1 = new System.Windows.Forms.Panel();
            this.tvAvailableRights = new System.Windows.Forms.TreeView();
            this.lblToggle = new System.Windows.Forms.Label();
            this.tvAssignedRights = new System.Windows.Forms.TreeView();
            this.lblToggleAssigned = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.lblAvailableModules = new System.Windows.Forms.Label();
            this.lblAssignedModules = new System.Windows.Forms.Label();
            this.txtRoleDesc = new System.Windows.Forms.TextBox();
            this.txtRoleName = new System.Windows.Forms.TextBox();
            this.txtRoleCode = new System.Windows.Forms.TextBox();
            this.lblRoleDesc = new System.Windows.Forms.Label();
            this.lblRoleName = new System.Windows.Forms.Label();
            this.lblRoleCode = new System.Windows.Forms.Label();
            this.cmbRoleStatus = new System.Windows.Forms.ComboBox();
            this.lblRoleStatus = new System.Windows.Forms.Label();
            this.dgvSearchRoles = new System.Windows.Forms.DataGridView();
            this.errCreateRole = new System.Windows.Forms.ErrorProvider(this.components);
            this.pnlGridSearch.SuspendLayout();
            this.pnlSearchHeader.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearchRoles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errCreateRole)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlGridSearch
            // 
            this.pnlGridSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlGridSearch.Controls.Add(this.dgvSearchRoles);
            this.pnlGridSearch.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.pnlGridSearch.Location = new System.Drawing.Point(0, 428);
            this.pnlGridSearch.Size = new System.Drawing.Size(1007, 221);
            // 
            // pnlSearchHeader
            // 
            this.pnlSearchHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSearchHeader.Controls.Add(this.lblRoleCode);
            this.pnlSearchHeader.Controls.Add(this.lblRoleName);
            this.pnlSearchHeader.Controls.Add(this.lblRoleDesc);
            this.pnlSearchHeader.Controls.Add(this.txtRoleCode);
            this.pnlSearchHeader.Controls.Add(this.txtRoleName);
            this.pnlSearchHeader.Controls.Add(this.txtRoleDesc);
            this.pnlSearchHeader.Controls.Add(this.cmbRoleStatus);
            this.pnlSearchHeader.Controls.Add(this.panel1);
            this.pnlSearchHeader.Controls.Add(this.lblRoleStatus);
            this.pnlSearchHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlSearchHeader.Size = new System.Drawing.Size(1007, 400);
            this.pnlSearchHeader.TabIndex = 0;
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblRoleStatus, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.pnlButtons, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.panel1, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.cmbRoleStatus, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.txtRoleDesc, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.txtRoleName, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.txtRoleCode, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblRoleDesc, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblRoleName, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblRoleCode, 0);
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
            this.btnSave.Location = new System.Drawing.Point(855, 0);
            this.btnSave.TabIndex = 12;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearch.Location = new System.Drawing.Point(780, 0);
            this.btnSearch.TabIndex = 11;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnReset
            // 
            this.btnReset.FlatAppearance.BorderSize = 0;
            this.btnReset.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnReset.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnReset.Location = new System.Drawing.Point(930, 0);
            this.btnReset.TabIndex = 13;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // pnlButtons
            // 
            this.pnlButtons.Location = new System.Drawing.Point(0, 364);
            this.pnlButtons.Size = new System.Drawing.Size(1005, 34);
            this.pnlButtons.TabIndex = 10;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(233)))), ((int)(((byte)(251)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.tvAvailableRights);
            this.panel1.Controls.Add(this.lblToggle);
            this.panel1.Controls.Add(this.tvAssignedRights);
            this.panel1.Controls.Add(this.lblToggleAssigned);
            this.panel1.Controls.Add(this.btnAdd);
            this.panel1.Controls.Add(this.btnRemove);
            this.panel1.Controls.Add(this.lblAvailableModules);
            this.panel1.Controls.Add(this.lblAssignedModules);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 72);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1005, 292);
            this.panel1.TabIndex = 5;
            // 
            // tvAvailableRights
            // 
            this.tvAvailableRights.CheckBoxes = true;
            this.tvAvailableRights.FullRowSelect = true;
            this.tvAvailableRights.Location = new System.Drawing.Point(160, 26);
            this.tvAvailableRights.Name = "tvAvailableRights";
            this.tvAvailableRights.Size = new System.Drawing.Size(250, 255);
            this.tvAvailableRights.TabIndex = 6;
            this.tvAvailableRights.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tvAvailableRights_AfterCheck);
            this.tvAvailableRights.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.tvAvailableRights_AfterCollapse);
            this.tvAvailableRights.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.tvAvailableRights_AfterExpand);
            // 
            // lblToggle
            // 
            this.lblToggle.BackColor = System.Drawing.Color.DimGray;
            this.lblToggle.Font = new System.Drawing.Font("Verdana", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.lblToggle.ForeColor = System.Drawing.Color.Azure;
            this.lblToggle.Location = new System.Drawing.Point(325, 7);
            this.lblToggle.Name = "lblToggle";
            this.lblToggle.Size = new System.Drawing.Size(85, 18);
            this.lblToggle.TabIndex = 34;
            this.lblToggle.Text = "Expand All";
            this.lblToggle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblToggle.MouseLeave += new System.EventHandler(this.Toggle_MouseLeave);
            this.lblToggle.Click += new System.EventHandler(this.lblToggle_Click);
            this.lblToggle.MouseHover += new System.EventHandler(this.Toggle_MouseHover);
            // 
            // tvAssignedRights
            // 
            this.tvAssignedRights.CheckBoxes = true;
            this.tvAssignedRights.FullRowSelect = true;
            this.tvAssignedRights.Location = new System.Drawing.Point(569, 26);
            this.tvAssignedRights.Name = "tvAssignedRights";
            this.tvAssignedRights.Size = new System.Drawing.Size(250, 255);
            this.tvAssignedRights.TabIndex = 9;
            this.tvAssignedRights.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tvAssignedRights_AfterCheck);
            this.tvAssignedRights.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.tvAssignedRights_AfterCollapse);
            this.tvAssignedRights.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.tvAssignedRights_AfterExpand);
            // 
            // lblToggleAssigned
            // 
            this.lblToggleAssigned.BackColor = System.Drawing.Color.DimGray;
            this.lblToggleAssigned.Font = new System.Drawing.Font("Verdana", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.lblToggleAssigned.ForeColor = System.Drawing.Color.Azure;
            this.lblToggleAssigned.Location = new System.Drawing.Point(734, 7);
            this.lblToggleAssigned.Name = "lblToggleAssigned";
            this.lblToggleAssigned.Size = new System.Drawing.Size(85, 18);
            this.lblToggleAssigned.TabIndex = 37;
            this.lblToggleAssigned.Text = "Expand All";
            this.lblToggleAssigned.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblToggleAssigned.MouseLeave += new System.EventHandler(this.Toggle_MouseLeave);
            this.lblToggleAssigned.Click += new System.EventHandler(this.lblToggleAssigned_Click);
            this.lblToggleAssigned.MouseHover += new System.EventHandler(this.Toggle_MouseHover);
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.Transparent;
            this.btnAdd.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAdd.BackgroundImage")));
            this.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnAdd.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnAdd.Location = new System.Drawing.Point(450, 79);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(92, 32);
            this.btnAdd.TabIndex = 7;
            this.btnAdd.Text = "A&dd >>";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.BackColor = System.Drawing.Color.Transparent;
            this.btnRemove.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRemove.BackgroundImage")));
            this.btnRemove.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRemove.FlatAppearance.BorderSize = 0;
            this.btnRemove.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnRemove.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemove.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnRemove.Location = new System.Drawing.Point(450, 111);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(92, 32);
            this.btnRemove.TabIndex = 8;
            this.btnRemove.Text = "<< Re&move";
            this.btnRemove.UseVisualStyleBackColor = false;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // lblAvailableModules
            // 
            this.lblAvailableModules.BackColor = System.Drawing.Color.DimGray;
            this.lblAvailableModules.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblAvailableModules.ForeColor = System.Drawing.Color.Azure;
            this.lblAvailableModules.Location = new System.Drawing.Point(160, 7);
            this.lblAvailableModules.Name = "lblAvailableModules";
            this.lblAvailableModules.Size = new System.Drawing.Size(250, 18);
            this.lblAvailableModules.TabIndex = 33;
            this.lblAvailableModules.Text = "Available Module(s)";
            this.lblAvailableModules.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblAssignedModules
            // 
            this.lblAssignedModules.BackColor = System.Drawing.Color.DimGray;
            this.lblAssignedModules.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblAssignedModules.ForeColor = System.Drawing.Color.Azure;
            this.lblAssignedModules.Location = new System.Drawing.Point(569, 7);
            this.lblAssignedModules.Name = "lblAssignedModules";
            this.lblAssignedModules.Size = new System.Drawing.Size(250, 18);
            this.lblAssignedModules.TabIndex = 36;
            this.lblAssignedModules.Text = "Assigned Module(s)";
            this.lblAssignedModules.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtRoleDesc
            // 
            this.txtRoleDesc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtRoleDesc.Location = new System.Drawing.Point(697, 16);
            this.txtRoleDesc.MaxLength = 500;
            this.txtRoleDesc.Name = "txtRoleDesc";
            this.txtRoleDesc.Size = new System.Drawing.Size(250, 21);
            this.txtRoleDesc.TabIndex = 3;
            // 
            // txtRoleName
            // 
            this.txtRoleName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtRoleName.Location = new System.Drawing.Point(425, 16);
            this.txtRoleName.MaxLength = 50;
            this.txtRoleName.Name = "txtRoleName";
            this.txtRoleName.Size = new System.Drawing.Size(110, 21);
            this.txtRoleName.TabIndex = 2;
            // 
            // txtRoleCode
            // 
            this.txtRoleCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtRoleCode.Location = new System.Drawing.Point(160, 16);
            this.txtRoleCode.MaxLength = 20;
            this.txtRoleCode.Name = "txtRoleCode";
            this.txtRoleCode.Size = new System.Drawing.Size(110, 21);
            this.txtRoleCode.TabIndex = 1;
            // 
            // lblRoleDesc
            // 
            this.lblRoleDesc.Location = new System.Drawing.Point(566, 19);
            this.lblRoleDesc.Name = "lblRoleDesc";
            this.lblRoleDesc.Size = new System.Drawing.Size(125, 13);
            this.lblRoleDesc.TabIndex = 30;
            this.lblRoleDesc.Text = "Description:";
            this.lblRoleDesc.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblRoleName
            // 
            this.lblRoleName.Location = new System.Drawing.Point(294, 19);
            this.lblRoleName.Name = "lblRoleName";
            this.lblRoleName.Size = new System.Drawing.Size(125, 13);
            this.lblRoleName.TabIndex = 29;
            this.lblRoleName.Text = "Role Name:*";
            this.lblRoleName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblRoleCode
            // 
            this.lblRoleCode.Location = new System.Drawing.Point(29, 19);
            this.lblRoleCode.Name = "lblRoleCode";
            this.lblRoleCode.Size = new System.Drawing.Size(125, 13);
            this.lblRoleCode.TabIndex = 28;
            this.lblRoleCode.Text = "Role Code:*";
            this.lblRoleCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbRoleStatus
            // 
            this.cmbRoleStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.cmbRoleStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRoleStatus.FormattingEnabled = true;
            this.cmbRoleStatus.Location = new System.Drawing.Point(160, 43);
            this.cmbRoleStatus.Name = "cmbRoleStatus";
            this.cmbRoleStatus.Size = new System.Drawing.Size(110, 21);
            this.cmbRoleStatus.TabIndex = 4;
            // 
            // lblRoleStatus
            // 
            this.lblRoleStatus.Location = new System.Drawing.Point(29, 46);
            this.lblRoleStatus.Name = "lblRoleStatus";
            this.lblRoleStatus.Size = new System.Drawing.Size(125, 13);
            this.lblRoleStatus.TabIndex = 32;
            this.lblRoleStatus.Text = "Status:*";
            this.lblRoleStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dgvSearchRoles
            // 
            this.dgvSearchRoles.AllowUserToAddRows = false;
            this.dgvSearchRoles.AllowUserToDeleteRows = false;
            this.dgvSearchRoles.AllowUserToResizeColumns = false;
            this.dgvSearchRoles.AllowUserToResizeRows = false;
            this.dgvSearchRoles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSearchRoles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSearchRoles.Location = new System.Drawing.Point(0, 0);
            this.dgvSearchRoles.MultiSelect = false;
            this.dgvSearchRoles.Name = "dgvSearchRoles";
            this.dgvSearchRoles.ReadOnly = true;
            this.dgvSearchRoles.RowHeadersVisible = false;
            this.dgvSearchRoles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSearchRoles.Size = new System.Drawing.Size(1005, 219);
            this.dgvSearchRoles.TabIndex = 12;
            this.dgvSearchRoles.SelectionChanged += new System.EventHandler(this.dgvSearchRoles_SelectionChanged);
            this.dgvSearchRoles.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSearchRoles_CellContentClick);
            // 
            // errCreateRole
            // 
            this.errCreateRole.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errCreateRole.ContainerControl = this;
            // 
            // frmRoleManagement
            // 
            this.AcceptButton = this.btnSearch;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1013, 707);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "frmRoleManagement";
            this.Text = "Role Management";
            this.WindowState = System.Windows.Forms.FormWindowState.Normal;
            this.Load += new System.EventHandler(this.frmRoleManagement_Load);
            this.pnlGridSearch.ResumeLayout(false);
            this.pnlSearchHeader.ResumeLayout(false);
            this.pnlSearchHeader.PerformLayout();
            this.pnlButtons.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearchRoles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errCreateRole)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtRoleDesc;
        private System.Windows.Forms.TextBox txtRoleName;
        private System.Windows.Forms.TextBox txtRoleCode;
        private System.Windows.Forms.Label lblRoleDesc;
        private System.Windows.Forms.Label lblRoleName;
        private System.Windows.Forms.Label lblRoleCode;
        private System.Windows.Forms.ComboBox cmbRoleStatus;
        private System.Windows.Forms.Label lblRoleStatus;
        private System.Windows.Forms.Label lblToggle;
        internal System.Windows.Forms.TreeView tvAvailableRights;
        private System.Windows.Forms.Label lblAvailableModules;
        private System.Windows.Forms.Label lblToggleAssigned;
        internal System.Windows.Forms.TreeView tvAssignedRights;
        private System.Windows.Forms.Label lblAssignedModules;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.DataGridView dgvSearchRoles;
        private System.Windows.Forms.ErrorProvider errCreateRole;
    }
}