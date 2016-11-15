namespace CoreComponent.Core.UI
{
    partial class TransactionProcess
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tabControlTransaction = new System.Windows.Forms.TabControl();
            this.tabSearch = new System.Windows.Forms.TabPage();
            this.pnlProcess = new System.Windows.Forms.Panel();
            this.pnlSearchHeader = new System.Windows.Forms.Panel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnSearchReset = new System.Windows.Forms.Button();
            this.lblSearchResult = new System.Windows.Forms.Label();
            this.dgvSearch = new System.Windows.Forms.DataGridView();
            this.tabCreate = new System.Windows.Forms.TabPage();
            this.btnClearDetails = new System.Windows.Forms.Button();
            this.pnlCreateHeader = new System.Windows.Forms.Panel();
            this.btnCreateReset = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblAddDetails = new System.Windows.Forms.Label();
            this.dgvAddDetails = new System.Windows.Forms.DataGridView();
            this.grpAddDetails = new System.Windows.Forms.GroupBox();
            this.btnAddDetails = new System.Windows.Forms.Button();
            this.lblPageTitle = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabControlTransaction.SuspendLayout();
            this.tabSearch.SuspendLayout();
            this.pnlSearchHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearch)).BeginInit();
            this.tabCreate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAddDetails)).BeginInit();
            this.grpAddDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(117)))), ((int)(((byte)(186)))));
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.Controls.Add(this.tabControlTransaction, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblPageTitle, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnExit, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(890, 713);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // tabControlTransaction
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.tabControlTransaction, 3);
            this.tabControlTransaction.Controls.Add(this.tabSearch);
            this.tabControlTransaction.Controls.Add(this.tabCreate);
            this.tabControlTransaction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlTransaction.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.tabControlTransaction.Location = new System.Drawing.Point(0, 29);
            this.tabControlTransaction.Margin = new System.Windows.Forms.Padding(0);
            this.tabControlTransaction.Name = "tabControlTransaction";
            this.tabControlTransaction.SelectedIndex = 0;
            this.tabControlTransaction.Size = new System.Drawing.Size(890, 684);
            this.tabControlTransaction.TabIndex = 5;
            // 
            // tabSearch
            // 
            this.tabSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(233)))), ((int)(((byte)(251)))));
            this.tabSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tabSearch.Controls.Add(this.pnlProcess);
            this.tabSearch.Controls.Add(this.pnlSearchHeader);
            this.tabSearch.Controls.Add(this.lblSearchResult);
            this.tabSearch.Controls.Add(this.dgvSearch);
            this.tabSearch.Location = new System.Drawing.Point(4, 22);
            this.tabSearch.Name = "tabSearch";
            this.tabSearch.Size = new System.Drawing.Size(882, 658);
            this.tabSearch.TabIndex = 0;
            this.tabSearch.Text = "Search";
            // 
            // pnlProcess
            // 
            this.pnlProcess.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(233)))), ((int)(((byte)(251)))));
            this.pnlProcess.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlProcess.Location = new System.Drawing.Point(0, 525);
            this.pnlProcess.Name = "pnlProcess";
            this.pnlProcess.Size = new System.Drawing.Size(882, 133);
            this.pnlProcess.TabIndex = 5;
            // 
            // pnlSearchHeader
            // 
            this.pnlSearchHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(233)))), ((int)(((byte)(251)))));
            this.pnlSearchHeader.Controls.Add(this.btnSearch);
            this.pnlSearchHeader.Controls.Add(this.btnSearchReset);
            this.pnlSearchHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSearchHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlSearchHeader.Name = "pnlSearchHeader";
            this.pnlSearchHeader.Size = new System.Drawing.Size(882, 217);
            this.pnlSearchHeader.TabIndex = 4;
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnSearch.Image = global::CoreComponent.Core.Properties.Resources.btnsearch;
            this.btnSearch.Location = new System.Drawing.Point(719, 184);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 0;
            this.btnSearch.Text = "Sear&ch";
            this.btnSearch.UseVisualStyleBackColor = false;
            // 
            // btnSearchReset
            // 
            this.btnSearchReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearchReset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnSearchReset.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnSearchReset.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnSearchReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchReset.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnSearchReset.Image = global::CoreComponent.Core.Properties.Resources.btnreset;
            this.btnSearchReset.Location = new System.Drawing.Point(800, 184);
            this.btnSearchReset.Name = "btnSearchReset";
            this.btnSearchReset.Size = new System.Drawing.Size(75, 23);
            this.btnSearchReset.TabIndex = 1;
            this.btnSearchReset.Text = "&Reset";
            this.btnSearchReset.UseVisualStyleBackColor = false;
            // 
            // lblSearchResult
            // 
            this.lblSearchResult.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSearchResult.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(117)))), ((int)(((byte)(186)))));
            this.lblSearchResult.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblSearchResult.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblSearchResult.Location = new System.Drawing.Point(0, 217);
            this.lblSearchResult.Name = "lblSearchResult";
            this.lblSearchResult.Size = new System.Drawing.Size(882, 24);
            this.lblSearchResult.TabIndex = 3;
            this.lblSearchResult.Text = "Search Result";
            this.lblSearchResult.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dgvSearch
            // 
            this.dgvSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSearch.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvSearch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSearch.Location = new System.Drawing.Point(0, 241);
            this.dgvSearch.Name = "dgvSearch";
            this.dgvSearch.Size = new System.Drawing.Size(882, 278);
            this.dgvSearch.TabIndex = 2;
            // 
            // tabCreate
            // 
            this.tabCreate.BackColor = System.Drawing.Color.LightSteelBlue;
            this.tabCreate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tabCreate.Controls.Add(this.btnClearDetails);
            this.tabCreate.Controls.Add(this.pnlCreateHeader);
            this.tabCreate.Controls.Add(this.btnCreateReset);
            this.tabCreate.Controls.Add(this.btnSave);
            this.tabCreate.Controls.Add(this.lblAddDetails);
            this.tabCreate.Controls.Add(this.dgvAddDetails);
            this.tabCreate.Controls.Add(this.grpAddDetails);
            this.tabCreate.Location = new System.Drawing.Point(4, 22);
            this.tabCreate.Name = "tabCreate";
            this.tabCreate.Size = new System.Drawing.Size(882, 658);
            this.tabCreate.TabIndex = 1;
            this.tabCreate.Text = "Create";
            this.tabCreate.UseVisualStyleBackColor = true;
            // 
            // btnClearDetails
            // 
            this.btnClearDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearDetails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnClearDetails.BackgroundImage = global::CoreComponent.Core.Properties.Resources.btnclear;
            this.btnClearDetails.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearDetails.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnClearDetails.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnClearDetails.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearDetails.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnClearDetails.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClearDetails.Location = new System.Drawing.Point(631, 623);
            this.btnClearDetails.Name = "btnClearDetails";
            this.btnClearDetails.Size = new System.Drawing.Size(75, 23);
            this.btnClearDetails.TabIndex = 3;
            this.btnClearDetails.Text = "C&lear";
            this.btnClearDetails.UseVisualStyleBackColor = false;
            // 
            // pnlCreateHeader
            // 
            this.pnlCreateHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(233)))), ((int)(((byte)(251)))));
            this.pnlCreateHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCreateHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlCreateHeader.Name = "pnlCreateHeader";
            this.pnlCreateHeader.Size = new System.Drawing.Size(882, 243);
            this.pnlCreateHeader.TabIndex = 7;
            // 
            // btnCreateReset
            // 
            this.btnCreateReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreateReset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnCreateReset.BackgroundImage = global::CoreComponent.Core.Properties.Resources.btnreset;
            this.btnCreateReset.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnCreateReset.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnCreateReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateReset.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnCreateReset.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCreateReset.Location = new System.Drawing.Point(793, 623);
            this.btnCreateReset.Name = "btnCreateReset";
            this.btnCreateReset.Size = new System.Drawing.Size(75, 23);
            this.btnCreateReset.TabIndex = 5;
            this.btnCreateReset.Text = "&Reset";
            this.btnCreateReset.UseVisualStyleBackColor = false;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnSave.BackgroundImage = global::CoreComponent.Core.Properties.Resources.btnsave;
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(712, 623);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = false;
            // 
            // lblAddDetails
            // 
            this.lblAddDetails.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAddDetails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(117)))), ((int)(((byte)(186)))));
            this.lblAddDetails.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold);
            this.lblAddDetails.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblAddDetails.Location = new System.Drawing.Point(0, 246);
            this.lblAddDetails.Name = "lblAddDetails";
            this.lblAddDetails.Size = new System.Drawing.Size(876, 19);
            this.lblAddDetails.TabIndex = 5;
            // 
            // dgvAddDetails
            // 
            this.dgvAddDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvAddDetails.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvAddDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAddDetails.Location = new System.Drawing.Point(6, 440);
            this.dgvAddDetails.Name = "dgvAddDetails";
            this.dgvAddDetails.Size = new System.Drawing.Size(867, 173);
            this.dgvAddDetails.TabIndex = 4;
            // 
            // grpAddDetails
            // 
            this.grpAddDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpAddDetails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(233)))), ((int)(((byte)(251)))));
            this.grpAddDetails.Controls.Add(this.btnAddDetails);
            this.grpAddDetails.Location = new System.Drawing.Point(0, 268);
            this.grpAddDetails.Name = "grpAddDetails";
            this.grpAddDetails.Size = new System.Drawing.Size(876, 350);
            this.grpAddDetails.TabIndex = 3;
            this.grpAddDetails.TabStop = false;
            // 
            // btnAddDetails
            // 
            this.btnAddDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddDetails.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnAddDetails.Location = new System.Drawing.Point(715, 226);
            this.btnAddDetails.Name = "btnAddDetails";
            this.btnAddDetails.Size = new System.Drawing.Size(75, 23);
            this.btnAddDetails.TabIndex = 2;
            this.btnAddDetails.Text = "Add";
            this.btnAddDetails.UseVisualStyleBackColor = true;
            // 
            // lblPageTitle
            // 
            this.lblPageTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblPageTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPageTitle.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPageTitle.ForeColor = System.Drawing.Color.White;
            this.lblPageTitle.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblPageTitle.Location = new System.Drawing.Point(92, 0);
            this.lblPageTitle.Name = "lblPageTitle";
            this.lblPageTitle.Padding = new System.Windows.Forms.Padding(9, 0, 0, 0);
            this.lblPageTitle.Size = new System.Drawing.Size(706, 29);
            this.lblPageTitle.TabIndex = 7;
            this.lblPageTitle.Text = "Page Title";
            this.lblPageTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Red;
            this.btnExit.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnExit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnExit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnExit.Image = global::CoreComponent.Core.Properties.Resources.btnexit;
            this.btnExit.Location = new System.Drawing.Point(812, 3);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 8;
            this.btnExit.Text = "E&xit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // TransactionProcess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CadetBlue;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(890, 713);
            this.Controls.Add(this.tableLayoutPanel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "TransactionProcess";
            this.ShowIcon = false;
            this.Text = "TransactionProcess";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tabControlTransaction.ResumeLayout(false);
            this.tabSearch.ResumeLayout(false);
            this.pnlSearchHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearch)).EndInit();
            this.tabCreate.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAddDetails)).EndInit();
            this.grpAddDetails.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        protected System.Windows.Forms.TabControl tabControlTransaction;
        protected System.Windows.Forms.TabPage tabSearch;
        protected System.Windows.Forms.Panel pnlProcess;
        protected System.Windows.Forms.Panel pnlSearchHeader;
        protected System.Windows.Forms.Button btnSearch;
        protected System.Windows.Forms.Button btnSearchReset;
        protected System.Windows.Forms.Label lblSearchResult;
        protected System.Windows.Forms.DataGridView dgvSearch;
        protected System.Windows.Forms.TabPage tabCreate;
        protected System.Windows.Forms.Panel pnlCreateHeader;
        protected System.Windows.Forms.Button btnCreateReset;
        protected System.Windows.Forms.Button btnSave;
        protected System.Windows.Forms.Label lblAddDetails;
        protected System.Windows.Forms.DataGridView dgvAddDetails;
        protected System.Windows.Forms.GroupBox grpAddDetails;
        protected System.Windows.Forms.Button btnClearDetails;
        protected System.Windows.Forms.Button btnAddDetails;
        protected System.Windows.Forms.Label lblPageTitle;
        protected System.Windows.Forms.Button btnExit;




    }
}

