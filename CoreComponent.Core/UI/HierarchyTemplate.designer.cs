namespace CoreComponent.Core.UI
{
    partial class HierarchyTemplate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HierarchyTemplate));
            this.pnlHierarchyTemplate = new System.Windows.Forms.Panel();
            this.pnlGridSearch = new System.Windows.Forms.Panel();
            this.pnlLabel = new System.Windows.Forms.Panel();
            this.lblSearchResult = new System.Windows.Forms.Label();
            this.pnlSearchHeader = new System.Windows.Forms.Panel();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.lblPageTitle = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblAppUser = new System.Windows.Forms.Label();
            this.pnlHierarchyTemplate.SuspendLayout();
            this.pnlLabel.SuspendLayout();
            this.pnlSearchHeader.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHierarchyTemplate
            // 
            this.pnlHierarchyTemplate.BackColor = System.Drawing.SystemColors.Control;
            this.pnlHierarchyTemplate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tableLayoutPanel1.SetColumnSpan(this.pnlHierarchyTemplate, 3);
            this.pnlHierarchyTemplate.Controls.Add(this.pnlGridSearch);
            this.pnlHierarchyTemplate.Controls.Add(this.pnlLabel);
            this.pnlHierarchyTemplate.Controls.Add(this.pnlSearchHeader);
            this.pnlHierarchyTemplate.Location = new System.Drawing.Point(3, 41);
            this.pnlHierarchyTemplate.Name = "pnlHierarchyTemplate";
            this.pnlHierarchyTemplate.Size = new System.Drawing.Size(1007, 649);
            this.pnlHierarchyTemplate.TabIndex = 0;
            // 
            // pnlGridSearch
            // 
            this.pnlGridSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(233)))), ((int)(((byte)(251)))));
            this.pnlGridSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlGridSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGridSearch.Location = new System.Drawing.Point(0, 204);
            this.pnlGridSearch.Name = "pnlGridSearch";
            this.pnlGridSearch.Size = new System.Drawing.Size(1007, 445);
            this.pnlGridSearch.TabIndex = 8;
            // 
            // pnlLabel
            // 
            this.pnlLabel.Controls.Add(this.lblSearchResult);
            this.pnlLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlLabel.Location = new System.Drawing.Point(0, 176);
            this.pnlLabel.Name = "pnlLabel";
            this.pnlLabel.Size = new System.Drawing.Size(1007, 28);
            this.pnlLabel.TabIndex = 10;
            // 
            // lblSearchResult
            // 
            this.lblSearchResult.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(117)))), ((int)(((byte)(186)))));
            this.lblSearchResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSearchResult.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblSearchResult.ForeColor = System.Drawing.Color.White;
            this.lblSearchResult.Location = new System.Drawing.Point(0, 0);
            this.lblSearchResult.Name = "lblSearchResult";
            this.lblSearchResult.Size = new System.Drawing.Size(1007, 28);
            this.lblSearchResult.TabIndex = 6;
            this.lblSearchResult.Text = "Search Result";
            this.lblSearchResult.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlSearchHeader
            // 
            this.pnlSearchHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(233)))), ((int)(((byte)(251)))));
            this.pnlSearchHeader.Controls.Add(this.pnlButtons);
            this.pnlSearchHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSearchHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlSearchHeader.Name = "pnlSearchHeader";
            this.pnlSearchHeader.Size = new System.Drawing.Size(1007, 176);
            this.pnlSearchHeader.TabIndex = 7;
            // 
            // pnlButtons
            // 
            this.pnlButtons.BackColor = System.Drawing.Color.Transparent;
            this.pnlButtons.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlButtons.Controls.Add(this.btnSearch);
            this.pnlButtons.Controls.Add(this.btnSave);
            this.pnlButtons.Controls.Add(this.btnReset);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButtons.Location = new System.Drawing.Point(0, 142);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(1007, 34);
            this.pnlButtons.TabIndex = 9;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.Transparent;
            this.btnSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.BackgroundImage")));
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearch.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnSearch.Location = new System.Drawing.Point(782, 0);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 34);
            this.btnSearch.TabIndex = 3;
            this.btnSearch.Text = "S&earch";
            this.btnSearch.UseVisualStyleBackColor = false;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSave.BackgroundImage")));
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnSave.Location = new System.Drawing.Point(857, 0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 34);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = false;
            // 
            // btnReset
            // 
            this.btnReset.BackColor = System.Drawing.Color.Transparent;
            this.btnReset.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnReset.BackgroundImage")));
            this.btnReset.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnReset.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnReset.FlatAppearance.BorderSize = 0;
            this.btnReset.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnReset.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReset.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnReset.Location = new System.Drawing.Point(932, 0);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 34);
            this.btnReset.TabIndex = 4;
            this.btnReset.Text = "&Reset";
            this.btnReset.UseVisualStyleBackColor = false;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.BackgroundImage = global::CoreComponent.Core.Properties.Resources.exit;
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExit.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnExit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnExit.Location = new System.Drawing.Point(935, 3);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 32);
            this.btnExit.TabIndex = 10;
            this.btnExit.Text = "E&xit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lblPageTitle
            // 
            this.lblPageTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblPageTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPageTitle.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPageTitle.ForeColor = System.Drawing.Color.White;
            this.lblPageTitle.Location = new System.Drawing.Point(205, 0);
            this.lblPageTitle.Name = "lblPageTitle";
            this.lblPageTitle.Size = new System.Drawing.Size(601, 38);
            this.lblPageTitle.TabIndex = 11;
            this.lblPageTitle.Text = "Page Title";
            this.lblPageTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(117)))), ((int)(((byte)(186)))));
            this.tableLayoutPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Controls.Add(this.btnExit, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.pnlHierarchyTemplate, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblPageTitle, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblAppUser, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1013, 703);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lblAppUser
            // 
            this.lblAppUser.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblAppUser.AutoSize = true;
            this.lblAppUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAppUser.ForeColor = System.Drawing.Color.White;
            this.lblAppUser.Location = new System.Drawing.Point(10, 11);
            this.lblAppUser.Margin = new System.Windows.Forms.Padding(10, 0, 3, 0);
            this.lblAppUser.Name = "lblAppUser";
            this.lblAppUser.Size = new System.Drawing.Size(0, 15);
            this.lblAppUser.TabIndex = 12;
            // 
            // HierarchyTemplate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CadetBlue;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1013, 703);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HierarchyTemplate";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "HierarchyTemplate";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.pnlHierarchyTemplate.ResumeLayout(false);
            this.pnlLabel.ResumeLayout(false);
            this.pnlSearchHeader.ResumeLayout(false);
            this.pnlButtons.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.Panel pnlGridSearch;
        protected System.Windows.Forms.Panel pnlSearchHeader;
        protected System.Windows.Forms.Label lblSearchResult;
        protected System.Windows.Forms.Panel pnlHierarchyTemplate;
        protected System.Windows.Forms.Button btnSave;
        protected System.Windows.Forms.Button btnSearch;
        protected System.Windows.Forms.Button btnReset;
        protected System.Windows.Forms.Panel pnlButtons;
        protected System.Windows.Forms.Button btnExit;
        protected System.Windows.Forms.Label lblPageTitle;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel pnlLabel;
        public System.Windows.Forms.Label lblAppUser;

    }
}