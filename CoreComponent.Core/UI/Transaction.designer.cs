namespace CoreComponent.Core.UI
{
    partial class Transaction
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Transaction));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblPageTitle = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.tabControlTransaction = new System.Windows.Forms.TabControl();
            this.tabSearch = new System.Windows.Forms.TabPage();
            this.pnlSearchGrid = new System.Windows.Forms.Panel();
            this.pnlSearchLabel = new System.Windows.Forms.Panel();
            this.lblSearchResult = new System.Windows.Forms.Label();
            this.pnlSearchHeader = new System.Windows.Forms.Panel();
            this.pnlSearchButtons = new System.Windows.Forms.Panel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnSearchReset = new System.Windows.Forms.Button();
            this.tabCreate = new System.Windows.Forms.TabPage();
            this.grpAddDetails = new System.Windows.Forms.GroupBox();
            this.pnlLowerButtons = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCreateReset = new System.Windows.Forms.Button();
            this.pnlCreateDetail = new System.Windows.Forms.Panel();
            this.pnlLabel = new System.Windows.Forms.Panel();
            this.lblAddDetails = new System.Windows.Forms.Label();
            this.pnlCreateHeader = new System.Windows.Forms.Panel();
            this.pnlTopButtons = new System.Windows.Forms.Panel();
            this.btnAddDetails = new System.Windows.Forms.Button();
            this.btnClearDetails = new System.Windows.Forms.Button();
            this.lblAppUser = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabControlTransaction.SuspendLayout();
            this.tabSearch.SuspendLayout();
            this.pnlSearchLabel.SuspendLayout();
            this.pnlSearchHeader.SuspendLayout();
            this.pnlSearchButtons.SuspendLayout();
            this.tabCreate.SuspendLayout();
            this.grpAddDetails.SuspendLayout();
            this.pnlLowerButtons.SuspendLayout();
            this.pnlLabel.SuspendLayout();
            this.pnlCreateHeader.SuspendLayout();
            this.pnlTopButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(117)))), ((int)(((byte)(186)))));
            this.tableLayoutPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Controls.Add(this.lblPageTitle, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnExit, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.tabControlTransaction, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblAppUser, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 95.2381F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4.761905F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1013, 690);
            this.tableLayoutPanel1.TabIndex = 7;

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
            this.lblPageTitle.TabIndex = 6;
            this.lblPageTitle.Text = "Page Title";
            this.lblPageTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnExit
            // 
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
            this.btnExit.TabIndex = 99;
            this.btnExit.Text = "E&xit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // tabControlTransaction
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.tabControlTransaction, 3);
            this.tabControlTransaction.Controls.Add(this.tabSearch);
            this.tabControlTransaction.Controls.Add(this.tabCreate);
            this.tabControlTransaction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlTransaction.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.tabControlTransaction.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.tabControlTransaction.Location = new System.Drawing.Point(0, 38);
            this.tabControlTransaction.Margin = new System.Windows.Forms.Padding(0);
            this.tabControlTransaction.Name = "tabControlTransaction";
            this.tabControlTransaction.Padding = new System.Drawing.Point(0, 0);
            this.tabControlTransaction.SelectedIndex = 0;
            this.tabControlTransaction.Size = new System.Drawing.Size(1013, 620);
            this.tabControlTransaction.TabIndex = 4;
            this.tabControlTransaction.Visible = false;
            // 
            // tabSearch
            // 
            this.tabSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(233)))), ((int)(((byte)(251)))));
            this.tabSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.tabSearch.Controls.Add(this.pnlSearchGrid);
            this.tabSearch.Controls.Add(this.pnlSearchLabel);
            this.tabSearch.Controls.Add(this.pnlSearchHeader);
            this.tabSearch.Location = new System.Drawing.Point(4, 22);
            this.tabSearch.Margin = new System.Windows.Forms.Padding(0);
            this.tabSearch.Name = "tabSearch";
            this.tabSearch.Size = new System.Drawing.Size(1005, 611);
            this.tabSearch.TabIndex = 0;
            this.tabSearch.Text = "Search";
            // 
            // pnlSearchGrid
            // 
            this.pnlSearchGrid.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(233)))), ((int)(((byte)(251)))));
            this.pnlSearchGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSearchGrid.Location = new System.Drawing.Point(0, 224);
            this.pnlSearchGrid.Name = "pnlSearchGrid";
            this.pnlSearchGrid.Size = new System.Drawing.Size(1005, 387);
            this.pnlSearchGrid.TabIndex = 5;
            // 
            // pnlSearchLabel
            // 
            this.pnlSearchLabel.Controls.Add(this.lblSearchResult);
            this.pnlSearchLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSearchLabel.Location = new System.Drawing.Point(0, 200);
            this.pnlSearchLabel.Name = "pnlSearchLabel";
            this.pnlSearchLabel.Size = new System.Drawing.Size(1005, 24);
            this.pnlSearchLabel.TabIndex = 7;
            // 
            // lblSearchResult
            // 
            this.lblSearchResult.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(117)))), ((int)(((byte)(186)))));
            this.lblSearchResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSearchResult.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblSearchResult.ForeColor = System.Drawing.Color.White;
            this.lblSearchResult.Location = new System.Drawing.Point(0, 0);
            this.lblSearchResult.Name = "lblSearchResult";
            this.lblSearchResult.Size = new System.Drawing.Size(1005, 24);
            this.lblSearchResult.TabIndex = 3;
            this.lblSearchResult.Text = "Search Result";
            this.lblSearchResult.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlSearchHeader
            // 
            this.pnlSearchHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(233)))), ((int)(((byte)(251)))));
            this.pnlSearchHeader.Controls.Add(this.pnlSearchButtons);
            this.pnlSearchHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSearchHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlSearchHeader.Margin = new System.Windows.Forms.Padding(0);
            this.pnlSearchHeader.Name = "pnlSearchHeader";
            this.pnlSearchHeader.Size = new System.Drawing.Size(1005, 200);
            this.pnlSearchHeader.TabIndex = 4;
            // 
            // pnlSearchButtons
            // 
            this.pnlSearchButtons.BackColor = System.Drawing.Color.Transparent;
            this.pnlSearchButtons.Controls.Add(this.btnSearch);
            this.pnlSearchButtons.Controls.Add(this.btnSearchReset);
            this.pnlSearchButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlSearchButtons.Location = new System.Drawing.Point(0, 168);
            this.pnlSearchButtons.Name = "pnlSearchButtons";
            this.pnlSearchButtons.Size = new System.Drawing.Size(1005, 32);
            this.pnlSearchButtons.TabIndex = 6;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.Transparent;
            this.btnSearch.BackgroundImage = global::CoreComponent.Core.Properties.Resources.button;
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearch.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnSearch.Location = new System.Drawing.Point(855, 0);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 32);
            this.btnSearch.TabIndex = 0;
            this.btnSearch.Text = "S&earch";
            this.btnSearch.UseVisualStyleBackColor = false;
            // 
            // btnSearchReset
            // 
            this.btnSearchReset.BackColor = System.Drawing.Color.Transparent;
            this.btnSearchReset.BackgroundImage = global::CoreComponent.Core.Properties.Resources.button;
            this.btnSearchReset.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSearchReset.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSearchReset.FlatAppearance.BorderSize = 0;
            this.btnSearchReset.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearchReset.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearchReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchReset.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnSearchReset.Location = new System.Drawing.Point(930, 0);
            this.btnSearchReset.Name = "btnSearchReset";
            this.btnSearchReset.Size = new System.Drawing.Size(75, 32);
            this.btnSearchReset.TabIndex = 1;
            this.btnSearchReset.Text = "&Reset";
            this.btnSearchReset.UseVisualStyleBackColor = false;
            // 
            // tabCreate
            // 
            this.tabCreate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(233)))), ((int)(((byte)(251)))));
            this.tabCreate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tabCreate.Controls.Add(this.grpAddDetails);
            this.tabCreate.Controls.Add(this.pnlLabel);
            this.tabCreate.Controls.Add(this.pnlCreateHeader);
            this.tabCreate.Location = new System.Drawing.Point(4, 22);
            this.tabCreate.Name = "tabCreate";
            this.tabCreate.Size = new System.Drawing.Size(1005, 594);
            this.tabCreate.TabIndex = 1;
            this.tabCreate.Text = "Create";
            // 
            // grpAddDetails
            // 
            this.grpAddDetails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(233)))), ((int)(((byte)(251)))));
            this.grpAddDetails.Controls.Add(this.pnlLowerButtons);
            this.grpAddDetails.Controls.Add(this.pnlCreateDetail);
            this.grpAddDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpAddDetails.Location = new System.Drawing.Point(0, 279);
            this.grpAddDetails.Margin = new System.Windows.Forms.Padding(0);
            this.grpAddDetails.Name = "grpAddDetails";
            this.grpAddDetails.Padding = new System.Windows.Forms.Padding(0);
            this.grpAddDetails.Size = new System.Drawing.Size(1005, 315);
            this.grpAddDetails.TabIndex = 3;
            this.grpAddDetails.TabStop = false;
            // 
            // pnlLowerButtons
            // 
            this.pnlLowerButtons.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(233)))), ((int)(((byte)(251)))));
            this.pnlLowerButtons.Controls.Add(this.btnSave);
            this.pnlLowerButtons.Controls.Add(this.btnCreateReset);
            this.pnlLowerButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlLowerButtons.Location = new System.Drawing.Point(0, 283);
            this.pnlLowerButtons.Name = "pnlLowerButtons";
            this.pnlLowerButtons.Size = new System.Drawing.Size(1005, 32);
            this.pnlLowerButtons.TabIndex = 10;
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
            this.btnSave.Location = new System.Drawing.Point(855, 0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 32);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = false;
            // 
            // btnCreateReset
            // 
            this.btnCreateReset.BackColor = System.Drawing.Color.Transparent;
            this.btnCreateReset.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCreateReset.BackgroundImage")));
            this.btnCreateReset.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCreateReset.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCreateReset.FlatAppearance.BorderSize = 0;
            this.btnCreateReset.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnCreateReset.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnCreateReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateReset.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnCreateReset.Location = new System.Drawing.Point(930, 0);
            this.btnCreateReset.Name = "btnCreateReset";
            this.btnCreateReset.Size = new System.Drawing.Size(75, 32);
            this.btnCreateReset.TabIndex = 5;
            this.btnCreateReset.Text = "&Reset";
            this.btnCreateReset.UseVisualStyleBackColor = false;
            // 
            // pnlCreateDetail
            // 
            this.pnlCreateDetail.BackColor = System.Drawing.Color.Transparent;
            this.pnlCreateDetail.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCreateDetail.Location = new System.Drawing.Point(0, 14);
            this.pnlCreateDetail.Margin = new System.Windows.Forms.Padding(0);
            this.pnlCreateDetail.Name = "pnlCreateDetail";
            this.pnlCreateDetail.Size = new System.Drawing.Size(1005, 140);
            this.pnlCreateDetail.TabIndex = 8;
            // 
            // pnlLabel
            // 
            this.pnlLabel.Controls.Add(this.lblAddDetails);
            this.pnlLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlLabel.Location = new System.Drawing.Point(0, 255);
            this.pnlLabel.Name = "pnlLabel";
            this.pnlLabel.Size = new System.Drawing.Size(1005, 24);
            this.pnlLabel.TabIndex = 8;
            // 
            // lblAddDetails
            // 
            this.lblAddDetails.BackColor = System.Drawing.Color.SteelBlue;
            this.lblAddDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAddDetails.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblAddDetails.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblAddDetails.Location = new System.Drawing.Point(0, 0);
            this.lblAddDetails.Name = "lblAddDetails";
            this.lblAddDetails.Size = new System.Drawing.Size(1005, 24);
            this.lblAddDetails.TabIndex = 5;
            this.lblAddDetails.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlCreateHeader
            // 
            this.pnlCreateHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(233)))), ((int)(((byte)(251)))));
            this.pnlCreateHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlCreateHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCreateHeader.Controls.Add(this.pnlTopButtons);
            this.pnlCreateHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCreateHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlCreateHeader.Name = "pnlCreateHeader";
            this.pnlCreateHeader.Size = new System.Drawing.Size(1005, 255);
            this.pnlCreateHeader.TabIndex = 7;
            // 
            // pnlTopButtons
            // 
            this.pnlTopButtons.Controls.Add(this.btnAddDetails);
            this.pnlTopButtons.Controls.Add(this.btnClearDetails);
            this.pnlTopButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlTopButtons.Location = new System.Drawing.Point(0, 221);
            this.pnlTopButtons.Name = "pnlTopButtons";
            this.pnlTopButtons.Size = new System.Drawing.Size(1003, 32);
            this.pnlTopButtons.TabIndex = 9;
            // 
            // btnAddDetails
            // 
            this.btnAddDetails.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAddDetails.BackgroundImage")));
            this.btnAddDetails.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAddDetails.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnAddDetails.FlatAppearance.BorderSize = 0;
            this.btnAddDetails.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnAddDetails.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnAddDetails.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddDetails.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnAddDetails.Location = new System.Drawing.Point(843, 0);
            this.btnAddDetails.Name = "btnAddDetails";
            this.btnAddDetails.Size = new System.Drawing.Size(80, 32);
            this.btnAddDetails.TabIndex = 2;
            this.btnAddDetails.Text = "&Add";
            this.btnAddDetails.UseVisualStyleBackColor = false;
            // 
            // btnClearDetails
            // 
            this.btnClearDetails.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearDetails.BackgroundImage")));
            this.btnClearDetails.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearDetails.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClearDetails.FlatAppearance.BorderSize = 0;
            this.btnClearDetails.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnClearDetails.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnClearDetails.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearDetails.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnClearDetails.Location = new System.Drawing.Point(923, 0);
            this.btnClearDetails.Name = "btnClearDetails";
            this.btnClearDetails.Size = new System.Drawing.Size(80, 32);
            this.btnClearDetails.TabIndex = 3;
            this.btnClearDetails.Text = "C&lear";
            this.btnClearDetails.UseVisualStyleBackColor = false;
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
            this.lblAppUser.TabIndex = 7;
            // 
            // Transaction
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1013, 690);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Transaction";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Transaction";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Transaction_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tabControlTransaction.ResumeLayout(false);
            this.tabSearch.ResumeLayout(false);
            this.pnlSearchLabel.ResumeLayout(false);
            this.pnlSearchHeader.ResumeLayout(false);
            this.pnlSearchButtons.ResumeLayout(false);
            this.tabCreate.ResumeLayout(false);
            this.grpAddDetails.ResumeLayout(false);
            this.pnlLowerButtons.ResumeLayout(false);
            this.pnlLabel.ResumeLayout(false);
            this.pnlCreateHeader.ResumeLayout(false);
            this.pnlTopButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.Panel pnlCreateHeader;
        protected System.Windows.Forms.Button btnCreateReset;
        protected System.Windows.Forms.Button btnSave;
        protected System.Windows.Forms.Label lblPageTitle;
        protected System.Windows.Forms.Label lblAddDetails;
        protected System.Windows.Forms.GroupBox grpAddDetails;
        protected System.Windows.Forms.Button btnClearDetails;
        protected System.Windows.Forms.Button btnAddDetails;
        protected System.Windows.Forms.Panel pnlSearchHeader;
        protected System.Windows.Forms.Button btnSearch;
        protected System.Windows.Forms.Button btnSearchReset;
        protected System.Windows.Forms.Label lblSearchResult;
        protected System.Windows.Forms.Button btnExit;
        protected System.Windows.Forms.Panel pnlSearchGrid;
        protected System.Windows.Forms.Panel pnlCreateDetail;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel pnlLabel;
        protected System.Windows.Forms.Panel pnlLowerButtons;
        private System.Windows.Forms.Panel pnlSearchLabel;
        protected System.Windows.Forms.Panel pnlTopButtons;
        protected System.Windows.Forms.Panel pnlSearchButtons;
        protected System.Windows.Forms.TabPage tabSearch;
        protected System.Windows.Forms.TabPage tabCreate;
        protected System.Windows.Forms.TabControl tabControlTransaction;
        public System.Windows.Forms.Label lblAppUser;




    }
}

