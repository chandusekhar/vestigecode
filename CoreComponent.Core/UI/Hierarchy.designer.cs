namespace CoreComponent.Core.UI
{
    partial class Hierarchy
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
            this.tlpCtrlContainer = new System.Windows.Forms.TableLayoutPanel();
            this.lblAppUser = new System.Windows.Forms.Label();
            this.tabControlHierarchy = new System.Windows.Forms.TabControl();
            this.tabPageSearch = new System.Windows.Forms.TabPage();
            this.pnlSearchGrid = new System.Windows.Forms.Panel();
            this.pnlSearchLabel = new System.Windows.Forms.Panel();
            this.lblSearchResult = new System.Windows.Forms.Label();
            this.pnlSearchHeader = new System.Windows.Forms.Panel();
            this.pnlSearchButtons = new System.Windows.Forms.Panel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnSearchReset = new System.Windows.Forms.Button();
            this.tabPageCreate = new System.Windows.Forms.TabPage();
            this.grpAddDetails = new System.Windows.Forms.GroupBox();
            this.pnlBottomButtons = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCreateReset = new System.Windows.Forms.Button();
            this.pnlCreateDetails = new System.Windows.Forms.Panel();
            this.pnlLabel = new System.Windows.Forms.Panel();
            this.pnlDetailsHeader = new System.Windows.Forms.Panel();
            this.pnlTopButtons = new System.Windows.Forms.Panel();
            this.btnAddDetails = new System.Windows.Forms.Button();
            this.btnClearDetails = new System.Windows.Forms.Button();
            this.lblPageTitle = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.tlpCtrlContainer.SuspendLayout();
            this.tabControlHierarchy.SuspendLayout();
            this.tabPageSearch.SuspendLayout();
            this.pnlSearchLabel.SuspendLayout();
            this.pnlSearchHeader.SuspendLayout();
            this.pnlSearchButtons.SuspendLayout();
            this.tabPageCreate.SuspendLayout();
            this.grpAddDetails.SuspendLayout();
            this.pnlBottomButtons.SuspendLayout();
            this.pnlDetailsHeader.SuspendLayout();
            this.pnlTopButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpCtrlContainer
            // 
            this.tlpCtrlContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(117)))), ((int)(((byte)(186)))));
            this.tlpCtrlContainer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tlpCtrlContainer.ColumnCount = 3;
            this.tlpCtrlContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpCtrlContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tlpCtrlContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpCtrlContainer.Controls.Add(this.lblAppUser, 0, 0);
            this.tlpCtrlContainer.Controls.Add(this.tabControlHierarchy, 0, 1);
            this.tlpCtrlContainer.Controls.Add(this.lblPageTitle, 1, 0);
            this.tlpCtrlContainer.Controls.Add(this.btnExit, 2, 0);
            this.tlpCtrlContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpCtrlContainer.Location = new System.Drawing.Point(0, 0);
            this.tlpCtrlContainer.Name = "tlpCtrlContainer";
            this.tlpCtrlContainer.RowCount = 3;
            this.tlpCtrlContainer.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpCtrlContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 95.24F));
            this.tlpCtrlContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4.76F));
            this.tlpCtrlContainer.Size = new System.Drawing.Size(1030, 703);
            this.tlpCtrlContainer.TabIndex = 11;
            // 
            // lblAppUser
            // 
            this.lblAppUser.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblAppUser.AutoSize = true;
            this.lblAppUser.BackColor = System.Drawing.Color.Transparent;
            this.lblAppUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAppUser.ForeColor = System.Drawing.Color.White;
            this.lblAppUser.Location = new System.Drawing.Point(10, 11);
            this.lblAppUser.Margin = new System.Windows.Forms.Padding(10, 0, 3, 0);
            this.lblAppUser.Name = "lblAppUser";
            this.lblAppUser.Size = new System.Drawing.Size(0, 15);
            this.lblAppUser.TabIndex = 21;
            // 
            // tabControlHierarchy
            // 
            this.tlpCtrlContainer.SetColumnSpan(this.tabControlHierarchy, 3);
            this.tabControlHierarchy.Controls.Add(this.tabPageSearch);
            this.tabControlHierarchy.Controls.Add(this.tabPageCreate);
            this.tabControlHierarchy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlHierarchy.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.tabControlHierarchy.Location = new System.Drawing.Point(0, 38);
            this.tabControlHierarchy.Margin = new System.Windows.Forms.Padding(0);
            this.tabControlHierarchy.Name = "tabControlHierarchy";
            this.tabControlHierarchy.SelectedIndex = 0;
            this.tabControlHierarchy.Size = new System.Drawing.Size(1030, 633);
            this.tabControlHierarchy.TabIndex = 13;
            // 
            // tabPageSearch
            // 
            this.tabPageSearch.BackColor = System.Drawing.Color.CadetBlue;
            this.tabPageSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tabPageSearch.Controls.Add(this.pnlSearchGrid);
            this.tabPageSearch.Controls.Add(this.pnlSearchLabel);
            this.tabPageSearch.Controls.Add(this.pnlSearchHeader);
            this.tabPageSearch.Location = new System.Drawing.Point(4, 22);
            this.tabPageSearch.Name = "tabPageSearch";
            this.tabPageSearch.Size = new System.Drawing.Size(1022, 607);
            this.tabPageSearch.TabIndex = 0;
            this.tabPageSearch.Text = "Search";
            this.tabPageSearch.UseVisualStyleBackColor = true;
            // 
            // pnlSearchGrid
            // 
            this.pnlSearchGrid.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(233)))), ((int)(((byte)(251)))));
            this.pnlSearchGrid.Cursor = System.Windows.Forms.Cursors.Default;
            this.pnlSearchGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSearchGrid.Location = new System.Drawing.Point(0, 244);
            this.pnlSearchGrid.Name = "pnlSearchGrid";
            this.pnlSearchGrid.Size = new System.Drawing.Size(1022, 363);
            this.pnlSearchGrid.TabIndex = 5;
            // 
            // pnlSearchLabel
            // 
            this.pnlSearchLabel.Controls.Add(this.lblSearchResult);
            this.pnlSearchLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSearchLabel.Location = new System.Drawing.Point(0, 220);
            this.pnlSearchLabel.Name = "pnlSearchLabel";
            this.pnlSearchLabel.Size = new System.Drawing.Size(1022, 24);
            this.pnlSearchLabel.TabIndex = 3;
            // 
            // lblSearchResult
            // 
            this.lblSearchResult.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(117)))), ((int)(((byte)(186)))));
            this.lblSearchResult.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSearchResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSearchResult.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearchResult.ForeColor = System.Drawing.Color.White;
            this.lblSearchResult.Location = new System.Drawing.Point(0, 0);
            this.lblSearchResult.Name = "lblSearchResult";
            this.lblSearchResult.Size = new System.Drawing.Size(1022, 24);
            this.lblSearchResult.TabIndex = 3;
            this.lblSearchResult.Text = "Search Results";
            this.lblSearchResult.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlSearchHeader
            // 
            this.pnlSearchHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(233)))), ((int)(((byte)(251)))));
            this.pnlSearchHeader.Controls.Add(this.pnlSearchButtons);
            this.pnlSearchHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSearchHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlSearchHeader.Name = "pnlSearchHeader";
            this.pnlSearchHeader.Size = new System.Drawing.Size(1022, 220);
            this.pnlSearchHeader.TabIndex = 4;
            // 
            // pnlSearchButtons
            // 
            this.pnlSearchButtons.Controls.Add(this.btnSearch);
            this.pnlSearchButtons.Controls.Add(this.btnSearchReset);
            this.pnlSearchButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlSearchButtons.Location = new System.Drawing.Point(0, 188);
            this.pnlSearchButtons.Name = "pnlSearchButtons";
            this.pnlSearchButtons.Size = new System.Drawing.Size(1022, 32);
            this.pnlSearchButtons.TabIndex = 20;
            // 
            // btnSearch
            // 
            this.btnSearch.BackgroundImage = global::CoreComponent.Core.Properties.Resources.button;
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearch.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnSearch.Location = new System.Drawing.Point(872, 0);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 32);
            this.btnSearch.TabIndex = 0;
            this.btnSearch.Text = "S&earch";
            this.btnSearch.UseVisualStyleBackColor = false;
            // 
            // btnSearchReset
            // 
            this.btnSearchReset.BackgroundImage = global::CoreComponent.Core.Properties.Resources.button;
            this.btnSearchReset.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSearchReset.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSearchReset.FlatAppearance.BorderSize = 0;
            this.btnSearchReset.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearchReset.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearchReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchReset.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnSearchReset.Location = new System.Drawing.Point(947, 0);
            this.btnSearchReset.Name = "btnSearchReset";
            this.btnSearchReset.Size = new System.Drawing.Size(75, 32);
            this.btnSearchReset.TabIndex = 1;
            this.btnSearchReset.Text = "&Reset";
            this.btnSearchReset.UseVisualStyleBackColor = false;
            // 
            // tabPageCreate
            // 
            this.tabPageCreate.BackColor = System.Drawing.Color.CadetBlue;
            this.tabPageCreate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tabPageCreate.Controls.Add(this.grpAddDetails);
            this.tabPageCreate.Controls.Add(this.pnlLabel);
            this.tabPageCreate.Controls.Add(this.pnlDetailsHeader);
            this.tabPageCreate.Location = new System.Drawing.Point(4, 22);
            this.tabPageCreate.Name = "tabPageCreate";
            this.tabPageCreate.Size = new System.Drawing.Size(1022, 607);
            this.tabPageCreate.TabIndex = 1;
            this.tabPageCreate.Text = "Create";
            this.tabPageCreate.UseVisualStyleBackColor = true;
            // 
            // grpAddDetails
            // 
            this.grpAddDetails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(233)))), ((int)(((byte)(251)))));
            this.grpAddDetails.Controls.Add(this.pnlBottomButtons);
            this.grpAddDetails.Controls.Add(this.pnlCreateDetails);
            this.grpAddDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpAddDetails.Location = new System.Drawing.Point(0, 244);
            this.grpAddDetails.Name = "grpAddDetails";
            this.grpAddDetails.Size = new System.Drawing.Size(1022, 363);
            this.grpAddDetails.TabIndex = 9;
            this.grpAddDetails.TabStop = false;
            // 
            // pnlBottomButtons
            // 
            this.pnlBottomButtons.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(233)))), ((int)(((byte)(251)))));
            this.pnlBottomButtons.Controls.Add(this.btnSave);
            this.pnlBottomButtons.Controls.Add(this.btnCreateReset);
            this.pnlBottomButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottomButtons.Location = new System.Drawing.Point(3, 328);
            this.pnlBottomButtons.Name = "pnlBottomButtons";
            this.pnlBottomButtons.Size = new System.Drawing.Size(1016, 32);
            this.pnlBottomButtons.TabIndex = 50;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.BackgroundImage = global::CoreComponent.Core.Properties.Resources.button;
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnSave.Location = new System.Drawing.Point(866, 0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 32);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = false;
            // 
            // btnCreateReset
            // 
            this.btnCreateReset.BackColor = System.Drawing.Color.Transparent;
            this.btnCreateReset.BackgroundImage = global::CoreComponent.Core.Properties.Resources.button;
            this.btnCreateReset.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCreateReset.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCreateReset.FlatAppearance.BorderSize = 0;
            this.btnCreateReset.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnCreateReset.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnCreateReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateReset.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnCreateReset.Location = new System.Drawing.Point(941, 0);
            this.btnCreateReset.Name = "btnCreateReset";
            this.btnCreateReset.Size = new System.Drawing.Size(75, 32);
            this.btnCreateReset.TabIndex = 5;
            this.btnCreateReset.Text = "&Reset";
            this.btnCreateReset.UseVisualStyleBackColor = false;
            // 
            // pnlCreateDetails
            // 
            this.pnlCreateDetails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(233)))), ((int)(((byte)(251)))));
            this.pnlCreateDetails.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCreateDetails.Location = new System.Drawing.Point(3, 17);
            this.pnlCreateDetails.Name = "pnlCreateDetails";
            this.pnlCreateDetails.Size = new System.Drawing.Size(1016, 120);
            this.pnlCreateDetails.TabIndex = 6;
            // 
            // pnlLabel
            // 
            this.pnlLabel.BackColor = System.Drawing.Color.SteelBlue;
            this.pnlLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlLabel.Location = new System.Drawing.Point(0, 217);
            this.pnlLabel.Name = "pnlLabel";
            this.pnlLabel.Size = new System.Drawing.Size(1022, 27);
            this.pnlLabel.TabIndex = 5;
            // 
            // pnlDetailsHeader
            // 
            this.pnlDetailsHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(233)))), ((int)(((byte)(251)))));
            this.pnlDetailsHeader.Controls.Add(this.pnlTopButtons);
            this.pnlDetailsHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDetailsHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlDetailsHeader.Name = "pnlDetailsHeader";
            this.pnlDetailsHeader.Size = new System.Drawing.Size(1022, 217);
            this.pnlDetailsHeader.TabIndex = 7;
            // 
            // pnlTopButtons
            // 
            this.pnlTopButtons.Controls.Add(this.btnAddDetails);
            this.pnlTopButtons.Controls.Add(this.btnClearDetails);
            this.pnlTopButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlTopButtons.Location = new System.Drawing.Point(0, 185);
            this.pnlTopButtons.Name = "pnlTopButtons";
            this.pnlTopButtons.Size = new System.Drawing.Size(1022, 32);
            this.pnlTopButtons.TabIndex = 4;
            // 
            // btnAddDetails
            // 
            this.btnAddDetails.BackgroundImage = global::CoreComponent.Core.Properties.Resources.button;
            this.btnAddDetails.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAddDetails.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnAddDetails.FlatAppearance.BorderSize = 0;
            this.btnAddDetails.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnAddDetails.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnAddDetails.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddDetails.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnAddDetails.Location = new System.Drawing.Point(872, 0);
            this.btnAddDetails.Name = "btnAddDetails";
            this.btnAddDetails.Size = new System.Drawing.Size(75, 32);
            this.btnAddDetails.TabIndex = 2;
            this.btnAddDetails.Text = "&Add";
            this.btnAddDetails.UseVisualStyleBackColor = false;
            // 
            // btnClearDetails
            // 
            this.btnClearDetails.BackgroundImage = global::CoreComponent.Core.Properties.Resources.button;
            this.btnClearDetails.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearDetails.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClearDetails.FlatAppearance.BorderSize = 0;
            this.btnClearDetails.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnClearDetails.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnClearDetails.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearDetails.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnClearDetails.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClearDetails.Location = new System.Drawing.Point(947, 0);
            this.btnClearDetails.Name = "btnClearDetails";
            this.btnClearDetails.Size = new System.Drawing.Size(75, 32);
            this.btnClearDetails.TabIndex = 3;
            this.btnClearDetails.Text = "C&lear";
            this.btnClearDetails.UseVisualStyleBackColor = false;
            // 
            // lblPageTitle
            // 
            this.lblPageTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblPageTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPageTitle.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPageTitle.ForeColor = System.Drawing.Color.White;
            this.lblPageTitle.Location = new System.Drawing.Point(209, 0);
            this.lblPageTitle.Name = "lblPageTitle";
            this.lblPageTitle.Padding = new System.Windows.Forms.Padding(9, 0, 0, 0);
            this.lblPageTitle.Size = new System.Drawing.Size(612, 38);
            this.lblPageTitle.TabIndex = 11;
            this.lblPageTitle.Text = "Page Title";
            this.lblPageTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.btnExit.Location = new System.Drawing.Point(952, 3);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 32);
            this.btnExit.TabIndex = 20;
            this.btnExit.Text = "E&xit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // Hierarchy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1030, 703);
            this.Controls.Add(this.tlpCtrlContainer);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Hierarchy";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Hierarchy";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.tlpCtrlContainer.ResumeLayout(false);
            this.tlpCtrlContainer.PerformLayout();
            this.tabControlHierarchy.ResumeLayout(false);
            this.tabPageSearch.ResumeLayout(false);
            this.pnlSearchLabel.ResumeLayout(false);
            this.pnlSearchHeader.ResumeLayout(false);
            this.pnlSearchButtons.ResumeLayout(false);
            this.tabPageCreate.ResumeLayout(false);
            this.grpAddDetails.ResumeLayout(false);
            this.pnlBottomButtons.ResumeLayout(false);
            this.pnlDetailsHeader.ResumeLayout(false);
            this.pnlTopButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpCtrlContainer;
        protected System.Windows.Forms.Label lblPageTitle;
        protected System.Windows.Forms.Button btnExit;
        protected System.Windows.Forms.TabControl tabControlHierarchy;
        protected System.Windows.Forms.TabPage tabPageSearch;
        protected System.Windows.Forms.Panel pnlSearchHeader;
        protected System.Windows.Forms.Button btnSearch;
        protected System.Windows.Forms.Button btnSearchReset;
        protected System.Windows.Forms.Label lblSearchResult;
        protected System.Windows.Forms.TabPage tabPageCreate;
        protected System.Windows.Forms.Panel pnlCreateDetails;
        protected System.Windows.Forms.Panel pnlDetailsHeader;
        protected System.Windows.Forms.Button btnClearDetails;
        protected System.Windows.Forms.Button btnAddDetails;
        protected System.Windows.Forms.Button btnCreateReset;
        protected System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Panel pnlSearchButtons;
        private System.Windows.Forms.Panel pnlSearchLabel;
        private System.Windows.Forms.Panel pnlTopButtons;
        private System.Windows.Forms.Panel pnlLabel;
        protected System.Windows.Forms.Panel pnlBottomButtons;
        private System.Windows.Forms.GroupBox grpAddDetails;
        protected System.Windows.Forms.Panel pnlSearchGrid;
        public System.Windows.Forms.Label lblAppUser;



    }
}

