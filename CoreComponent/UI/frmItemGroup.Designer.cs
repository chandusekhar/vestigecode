namespace CoreComponent.UI
{
    partial class frmItemGroup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmItemGroup));
            this.lblGroupName = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.txtGroupName = new System.Windows.Forms.TextBox();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.dgvItemGroup = new System.Windows.Forms.DataGridView();
            this.epItemGroup = new System.Windows.Forms.ErrorProvider(this.components);
            this.tvSelectedItems = new System.Windows.Forms.TreeView();
            this.tvAllItems = new System.Windows.Forms.TreeView();
            this.lblSelectableItems = new System.Windows.Forms.Label();
            this.btnAddItems = new System.Windows.Forms.Button();
            this.btnRemoveItems = new System.Windows.Forms.Button();
            this.pnlGridSearch.SuspendLayout();
            this.pnlSearchHeader.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.epItemGroup)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlGridSearch
            // 
            this.pnlGridSearch.Controls.Add(this.dgvItemGroup);
            this.pnlGridSearch.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlGridSearch.Location = new System.Drawing.Point(0, 279);
            this.pnlGridSearch.Size = new System.Drawing.Size(1007, 370);
            // 
            // pnlSearchHeader
            // 
            this.pnlSearchHeader.Controls.Add(this.btnRemoveItems);
            this.pnlSearchHeader.Controls.Add(this.btnAddItems);
            this.pnlSearchHeader.Controls.Add(this.lblSelectableItems);
            this.pnlSearchHeader.Controls.Add(this.tvSelectedItems);
            this.pnlSearchHeader.Controls.Add(this.cmbStatus);
            this.pnlSearchHeader.Controls.Add(this.tvAllItems);
            this.pnlSearchHeader.Controls.Add(this.txtGroupName);
            this.pnlSearchHeader.Controls.Add(this.lblStatus);
            this.pnlSearchHeader.Controls.Add(this.lblGroupName);
            this.pnlSearchHeader.Size = new System.Drawing.Size(1007, 250);
            this.pnlSearchHeader.TabIndex = 0;
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblGroupName, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblStatus, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.txtGroupName, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.tvAllItems, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.cmbStatus, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.pnlButtons, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.tvSelectedItems, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblSelectableItems, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.btnAddItems, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.btnRemoveItems, 0);
            // 
            // lblSearchResult
            // 
            this.lblSearchResult.TabIndex = 0;
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
            this.pnlButtons.Location = new System.Drawing.Point(0, 216);
            // 
            // lblGroupName
            // 
            this.lblGroupName.AutoSize = true;
            this.lblGroupName.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGroupName.Location = new System.Drawing.Point(56, 16);
            this.lblGroupName.Name = "lblGroupName";
            this.lblGroupName.Size = new System.Drawing.Size(91, 13);
            this.lblGroupName.TabIndex = 0;
            this.lblGroupName.Text = "Group Name:*";
            this.lblGroupName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(441, 16);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(55, 13);
            this.lblStatus.TabIndex = 2;
            this.lblStatus.Text = "Status:*";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtGroupName
            // 
            this.txtGroupName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtGroupName.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGroupName.Location = new System.Drawing.Point(150, 13);
            this.txtGroupName.MaxLength = 20;
            this.txtGroupName.Name = "txtGroupName";
            this.txtGroupName.Size = new System.Drawing.Size(196, 21);
            this.txtGroupName.TabIndex = 0;
            this.txtGroupName.Validated += new System.EventHandler(this.txtGroupName_Validated);
            // 
            // cmbStatus
            // 
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Location = new System.Drawing.Point(499, 13);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(196, 21);
            this.cmbStatus.TabIndex = 1;
            this.cmbStatus.SelectedIndexChanged += new System.EventHandler(this.cmbStatus_SelectedIndexChanged);
            // 
            // dgvItemGroup
            // 
            this.dgvItemGroup.AllowUserToAddRows = false;
            this.dgvItemGroup.AllowUserToDeleteRows = false;
            this.dgvItemGroup.AllowUserToOrderColumns = true;
            this.dgvItemGroup.AllowUserToResizeColumns = false;
            this.dgvItemGroup.AllowUserToResizeRows = false;
            this.dgvItemGroup.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItemGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvItemGroup.Location = new System.Drawing.Point(0, 0);
            this.dgvItemGroup.MultiSelect = false;
            this.dgvItemGroup.Name = "dgvItemGroup";
            this.dgvItemGroup.ReadOnly = true;
            this.dgvItemGroup.RowHeadersVisible = false;
            this.dgvItemGroup.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItemGroup.Size = new System.Drawing.Size(1007, 370);
            this.dgvItemGroup.TabIndex = 0;
            this.dgvItemGroup.TabStop = false;
            this.dgvItemGroup.SelectionChanged += new System.EventHandler(this.dgvItemGroup_SelectionChanged);
            this.dgvItemGroup.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvItemGroup_CellContentClick);
            // 
            // epItemGroup
            // 
            this.epItemGroup.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.epItemGroup.ContainerControl = this;
            // 
            // tvSelectedItems
            // 
            this.tvSelectedItems.CheckBoxes = true;
            this.epItemGroup.SetIconAlignment(this.tvSelectedItems, System.Windows.Forms.ErrorIconAlignment.TopRight);
            this.tvSelectedItems.Location = new System.Drawing.Point(499, 51);
            this.tvSelectedItems.Name = "tvSelectedItems";
            this.tvSelectedItems.Size = new System.Drawing.Size(196, 150);
            this.tvSelectedItems.TabIndex = 6;
            this.tvSelectedItems.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tvSelectedItems_AfterCheck);
            // 
            // tvAllItems
            // 
            this.tvAllItems.CheckBoxes = true;
            this.tvAllItems.Location = new System.Drawing.Point(150, 51);
            this.tvAllItems.Name = "tvAllItems";
            this.tvAllItems.Size = new System.Drawing.Size(196, 150);
            this.tvAllItems.TabIndex = 2;
            this.tvAllItems.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tvAllItems_AfterCheck);
            // 
            // lblSelectableItems
            // 
            this.lblSelectableItems.AutoSize = true;
            this.lblSelectableItems.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectableItems.Location = new System.Drawing.Point(94, 51);
            this.lblSelectableItems.Name = "lblSelectableItems";
            this.lblSelectableItems.Size = new System.Drawing.Size(45, 13);
            this.lblSelectableItems.TabIndex = 12;
            this.lblSelectableItems.Text = "Items:";
            // 
            // btnAddItems
            // 
            this.btnAddItems.BackColor = System.Drawing.Color.Transparent;
            this.btnAddItems.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAddItems.BackgroundImage")));
            this.btnAddItems.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAddItems.FlatAppearance.BorderSize = 0;
            this.btnAddItems.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnAddItems.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnAddItems.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddItems.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddItems.Location = new System.Drawing.Point(382, 89);
            this.btnAddItems.Name = "btnAddItems";
            this.btnAddItems.Size = new System.Drawing.Size(75, 28);
            this.btnAddItems.TabIndex = 3;
            this.btnAddItems.Text = "A&dd";
            this.btnAddItems.UseVisualStyleBackColor = false;
            this.btnAddItems.Click += new System.EventHandler(this.btnAddItems_Click);
            // 
            // btnRemoveItems
            // 
            this.btnRemoveItems.BackColor = System.Drawing.Color.Transparent;
            this.btnRemoveItems.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRemoveItems.BackgroundImage")));
            this.btnRemoveItems.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRemoveItems.FlatAppearance.BorderSize = 0;
            this.btnRemoveItems.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnRemoveItems.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnRemoveItems.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemoveItems.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemoveItems.Location = new System.Drawing.Point(382, 118);
            this.btnRemoveItems.Name = "btnRemoveItems";
            this.btnRemoveItems.Size = new System.Drawing.Size(75, 28);
            this.btnRemoveItems.TabIndex = 4;
            this.btnRemoveItems.Text = "Re&move";
            this.btnRemoveItems.UseVisualStyleBackColor = false;
            this.btnRemoveItems.Click += new System.EventHandler(this.btnRemoveItems_Click);
            // 
            // frmItemGroup
            // 
            this.AcceptButton = this.btnSearch;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Tile;
            this.ClientSize = new System.Drawing.Size(1013, 703);
            this.DoubleBuffered = false;
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "frmItemGroup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "ItemGroup";
            this.WindowState = System.Windows.Forms.FormWindowState.Normal;
            this.pnlGridSearch.ResumeLayout(false);
            this.pnlSearchHeader.ResumeLayout(false);
            this.pnlSearchHeader.PerformLayout();
            this.pnlButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.epItemGroup)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.TextBox txtGroupName;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblGroupName;
        private System.Windows.Forms.DataGridView dgvItemGroup;
        private System.Windows.Forms.ErrorProvider epItemGroup;
        private System.Windows.Forms.Label lblSelectableItems;
        private System.Windows.Forms.TreeView tvSelectedItems;
        private System.Windows.Forms.TreeView tvAllItems;
        private System.Windows.Forms.Button btnRemoveItems;
        private System.Windows.Forms.Button btnAddItems;
    }
}