namespace CoreComponent.UI
{
    partial class ReportViewer
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
            this.cmbReport = new System.Windows.Forms.ComboBox();
            this.tlpParameter = new System.Windows.Forms.TableLayoutPanel();
            this.pParameter = new System.Windows.Forms.Panel();
            this.lblReport = new System.Windows.Forms.Label();
            this.lblPageTitle = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblAppUser = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnExit = new System.Windows.Forms.Button();
            this.rvReport = new Microsoft.Reporting.WinForms.ReportViewer();
            this.btnView = new System.Windows.Forms.Button();
            this.pParameter.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbReport
            // 
            this.cmbReport.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbReport.FormattingEnabled = true;
            this.cmbReport.Location = new System.Drawing.Point(107, 6);
            this.cmbReport.Name = "cmbReport";
            this.cmbReport.Size = new System.Drawing.Size(200, 21);
            this.cmbReport.TabIndex = 0;
            this.cmbReport.SelectedIndexChanged += new System.EventHandler(this.cmbReport_SelectedIndexChanged);
            // 
            // tlpParameter
            // 
            this.tlpParameter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(233)))), ((int)(((byte)(251)))));
            this.tlpParameter.ColumnCount = 6;
            this.tlpParameter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 52.32067F));
            this.tlpParameter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 47.67933F));
            this.tlpParameter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 157F));
            this.tlpParameter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 162F));
            this.tlpParameter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 157F));
            this.tlpParameter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 161F));
            this.tlpParameter.Location = new System.Drawing.Point(2, 1);
            this.tlpParameter.Name = "tlpParameter";
            this.tlpParameter.RowCount = 3;
            this.tlpParameter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 47.82609F));
            this.tlpParameter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 52.17391F));
            this.tlpParameter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tlpParameter.Size = new System.Drawing.Size(1000, 84);
            this.tlpParameter.TabIndex = 0;
            // 
            // pParameter
            // 
            this.pParameter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(233)))), ((int)(((byte)(251)))));
            this.pParameter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tableLayoutPanel1.SetColumnSpan(this.pParameter, 3);
            this.pParameter.Controls.Add(this.tlpParameter);
            this.pParameter.Location = new System.Drawing.Point(3, 78);
            this.pParameter.Name = "pParameter";
            this.pParameter.Size = new System.Drawing.Size(1007, 89);
            this.pParameter.TabIndex = 2;
            // 
            // lblReport
            // 
            this.lblReport.AutoSize = true;
            this.lblReport.BackColor = System.Drawing.Color.Transparent;
            this.lblReport.Location = new System.Drawing.Point(14, 9);
            this.lblReport.Name = "lblReport";
            this.lblReport.Size = new System.Drawing.Size(87, 13);
            this.lblReport.TabIndex = 5;
            this.lblReport.Text = "Report Name:";
            // 
            // lblPageTitle
            // 
            this.lblPageTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblPageTitle.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Bold);
            this.lblPageTitle.ForeColor = System.Drawing.Color.White;
            this.lblPageTitle.Location = new System.Drawing.Point(205, 0);
            this.lblPageTitle.Name = "lblPageTitle";
            this.lblPageTitle.Size = new System.Drawing.Size(597, 32);
            this.lblPageTitle.TabIndex = 0;
            this.lblPageTitle.Text = "Reports";
            this.lblPageTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblPageTitle.Click += new System.EventHandler(this.lblPageTitle_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(233)))), ((int)(((byte)(251)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tableLayoutPanel1.SetColumnSpan(this.panel1, 3);
            this.panel1.Controls.Add(this.cmbReport);
            this.panel1.Controls.Add(this.lblReport);
            this.panel1.Location = new System.Drawing.Point(3, 41);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1007, 31);
            this.panel1.TabIndex = 1;
            // 
            // lblAppUser
            // 
            this.lblAppUser.BackColor = System.Drawing.Color.Transparent;
            this.lblAppUser.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblAppUser.ForeColor = System.Drawing.Color.White;
            this.lblAppUser.Location = new System.Drawing.Point(3, 0);
            this.lblAppUser.Name = "lblAppUser";
            this.lblAppUser.Size = new System.Drawing.Size(196, 38);
            this.lblAppUser.TabIndex = 6;
            this.lblAppUser.Text = "label2";
            this.lblAppUser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Controls.Add(this.btnExit, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.pParameter, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblAppUser, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblPageTitle, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.rvReport, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.btnView, 2, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 95F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1013, 700);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.BackgroundImage = global::CoreComponent.Properties.Resources.exit;
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnExit.Location = new System.Drawing.Point(935, 3);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 32);
            this.btnExit.TabIndex = 5;
            this.btnExit.Text = "E&xit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // rvReport
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.rvReport, 3);
            this.rvReport.Location = new System.Drawing.Point(3, 213);
            this.rvReport.Name = "rvReport";
            this.rvReport.ShowBackButton = false;
            this.rvReport.ShowContextMenu = false;
            this.rvReport.ShowDocumentMapButton = false;
            this.rvReport.ShowParameterPrompts = false;
            this.rvReport.ShowPromptAreaButton = false;
            this.rvReport.ShowRefreshButton = false;
            this.rvReport.Size = new System.Drawing.Size(1007, 484);
            this.rvReport.TabIndex = 7;
            this.rvReport.TabStop = false;
            this.rvReport.RenderingComplete += new Microsoft.Reporting.WinForms.RenderingCompleteEventHandler(this.rvReport_RenderingComplete);
            this.rvReport.RenderingBegin += new System.ComponentModel.CancelEventHandler(this.rvReport_RenderingBegin);
            // 
            // btnView
            // 
            this.btnView.BackColor = System.Drawing.Color.Transparent;
            this.btnView.BackgroundImage = global::CoreComponent.Properties.Resources.button;
            this.btnView.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnView.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnView.Enabled = false;
            this.btnView.FlatAppearance.BorderSize = 0;
            this.btnView.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnView.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnView.Location = new System.Drawing.Point(935, 173);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(75, 34);
            this.btnView.TabIndex = 4;
            this.btnView.Text = "&View";
            this.btnView.UseVisualStyleBackColor = false;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // ReportViewer
            // 
            this.AcceptButton = this.btnView;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(117)))), ((int)(((byte)(186)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1013, 703);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ReportViewer";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "ReportViewer";
            this.Load += new System.EventHandler(this.ReportViewer_Load);
            this.pParameter.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbReport;
        private System.Windows.Forms.TableLayoutPanel tlpParameter;
        private System.Windows.Forms.Panel pParameter;
        private System.Windows.Forms.Label lblReport;
        private System.Windows.Forms.Label lblPageTitle;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblAppUser;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnExit;
        private Microsoft.Reporting.WinForms.ReportViewer rvReport;
        private System.Windows.Forms.Button btnView;
    }
}