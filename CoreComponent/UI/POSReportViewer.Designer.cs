namespace CoreComponent.UI
{
    partial class POSReportViewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(POSReportViewer));
            this.tss06 = new System.Windows.Forms.ToolStripSeparator();
            this.tss07 = new System.Windows.Forms.ToolStripSeparator();
            this.tsb06 = new System.Windows.Forms.ToolStripButton();
            this.tsb07 = new System.Windows.Forms.ToolStripButton();
            this.tss05 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnView = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.tlpParameter = new System.Windows.Forms.TableLayoutPanel();
            this.btnClose = new System.Windows.Forms.Button();
            this.tsb05 = new System.Windows.Forms.ToolStripButton();
            this.pParameter = new System.Windows.Forms.Panel();
            this.tsb01 = new System.Windows.Forms.ToolStripButton();
            this.tss04 = new System.Windows.Forms.ToolStripSeparator();
            this.tsReport = new System.Windows.Forms.ToolStrip();
            this.tss01 = new System.Windows.Forms.ToolStripSeparator();
            this.tsb02 = new System.Windows.Forms.ToolStripButton();
            this.tss02 = new System.Windows.Forms.ToolStripSeparator();
            this.tsb03 = new System.Windows.Forms.ToolStripButton();
            this.tss03 = new System.Windows.Forms.ToolStripSeparator();
            this.tsb04 = new System.Windows.Forms.ToolStripButton();
            this.tsb08 = new System.Windows.Forms.ToolStripButton();
            this.pReportSelector = new System.Windows.Forms.Panel();
            this.rvReport = new Microsoft.Reporting.WinForms.ReportViewer();
            this.tlpParameter.SuspendLayout();
            this.pParameter.SuspendLayout();
            this.tsReport.SuspendLayout();
            this.pReportSelector.SuspendLayout();
            this.SuspendLayout();
            // 
            // tss06
            // 
            this.tss06.Name = "tss06";
            this.tss06.Size = new System.Drawing.Size(96, 6);
            // 
            // tss07
            // 
            this.tss07.Name = "tss07";
            this.tss07.Size = new System.Drawing.Size(96, 6);
            // 
            // tsb06
            // 
            this.tsb06.AutoSize = false;
            this.tsb06.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tsb06.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb06.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb06.Name = "tsb06";
            this.tsb06.Size = new System.Drawing.Size(96, 60);
            this.tsb06.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            // 
            // tsb07
            // 
            this.tsb07.AutoSize = false;
            this.tsb07.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tsb07.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb07.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb07.Name = "tsb07";
            this.tsb07.Size = new System.Drawing.Size(96, 60);
            this.tsb07.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            // 
            // tss05
            // 
            this.tss05.Name = "tss05";
            this.tss05.Size = new System.Drawing.Size(96, 6);
            // 
            // btnDown
            // 
            this.btnDown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(227)))), ((int)(((byte)(195)))));
            this.btnDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDown.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnDown.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDown.Location = new System.Drawing.Point(0, 655);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(98, 47);
            this.btnDown.TabIndex = 3;
            this.btnDown.UseVisualStyleBackColor = false;
            // 
            // btnView
            // 
            this.btnView.AccessibleRole = System.Windows.Forms.AccessibleRole.TitleBar;
            this.btnView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(219)))), ((int)(((byte)(192)))));
            this.btnView.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnView.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnView.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnView.Location = new System.Drawing.Point(603, 6);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(99, 27);
            this.btnView.TabIndex = 5;
            this.btnView.Text = "View";
            this.btnView.UseVisualStyleBackColor = false;
            this.btnView.Click += new System.EventHandler(this.RVButton_Click);
            // 
            // btnUp
            // 
            this.btnUp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(227)))), ((int)(((byte)(195)))));
            this.btnUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnUp.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnUp.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnUp.Image = ((System.Drawing.Image)(resources.GetObject("btnUp.Image")));
            this.btnUp.Location = new System.Drawing.Point(0, 0);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(98, 69);
            this.btnUp.TabIndex = 2;
            this.btnUp.UseVisualStyleBackColor = false;
            // 
            // tlpParameter
            // 
            this.tlpParameter.AllowDrop = true;
            this.tlpParameter.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlpParameter.BackColor = System.Drawing.Color.Transparent;
            this.tlpParameter.BackgroundImage = global::CoreComponent.Properties.Resources.winback;
            this.tlpParameter.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.InsetDouble;
            this.tlpParameter.ColumnCount = 6;
            this.tlpParameter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.81497F));
            this.tlpParameter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.75052F));
            this.tlpParameter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.54262F));
            this.tlpParameter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.48857F));
            this.tlpParameter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.86487F));
            this.tlpParameter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.64241F));
            this.tlpParameter.Controls.Add(this.btnClose, 4, 0);
            this.tlpParameter.Controls.Add(this.btnView, 3, 0);
            this.tlpParameter.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.tlpParameter.Location = new System.Drawing.Point(0, 0);
            this.tlpParameter.Margin = new System.Windows.Forms.Padding(0);
            this.tlpParameter.Name = "tlpParameter";
            this.tlpParameter.RowCount = 2;
            this.tlpParameter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 94F));
            this.tlpParameter.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpParameter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpParameter.Size = new System.Drawing.Size(965, 73);
            this.tlpParameter.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(734, 6);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(111, 28);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.RVButton_Click);
            // 
            // tsb05
            // 
            this.tsb05.AutoSize = false;
            this.tsb05.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tsb05.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb05.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb05.Name = "tsb05";
            this.tsb05.Size = new System.Drawing.Size(96, 60);
            this.tsb05.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            // 
            // pParameter
            // 
            this.pParameter.BackColor = System.Drawing.Color.Transparent;
            this.pParameter.Controls.Add(this.tlpParameter);
            this.pParameter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pParameter.Location = new System.Drawing.Point(100, 0);
            this.pParameter.Margin = new System.Windows.Forms.Padding(0);
            this.pParameter.Name = "pParameter";
            this.pParameter.Size = new System.Drawing.Size(965, 74);
            this.pParameter.TabIndex = 4;
            // 
            // tsb01
            // 
            this.tsb01.AutoSize = false;
            this.tsb01.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tsb01.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb01.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb01.Name = "tsb01";
            this.tsb01.Size = new System.Drawing.Size(96, 60);
            this.tsb01.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            // 
            // tss04
            // 
            this.tss04.Name = "tss04";
            this.tss04.Size = new System.Drawing.Size(96, 6);
            // 
            // tsReport
            // 
            this.tsReport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tsReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tsReport.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsReport.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb01,
            this.tss01,
            this.tsb02,
            this.tss02,
            this.tsb03,
            this.tss03,
            this.tsb04,
            this.tss04,
            this.tsb05,
            this.tss05,
            this.tsb06,
            this.tss06,
            this.tsb07,
            this.tss07,
            this.tsb08});
            this.tsReport.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.tsReport.Location = new System.Drawing.Point(0, 69);
            this.tsReport.Name = "tsReport";
            this.tsReport.Size = new System.Drawing.Size(98, 586);
            this.tsReport.TabIndex = 4;
            // 
            // tss01
            // 
            this.tss01.Name = "tss01";
            this.tss01.Size = new System.Drawing.Size(96, 6);
            // 
            // tsb02
            // 
            this.tsb02.AutoSize = false;
            this.tsb02.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tsb02.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb02.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb02.Name = "tsb02";
            this.tsb02.Size = new System.Drawing.Size(96, 60);
            this.tsb02.Tag = "20";
            this.tsb02.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            // 
            // tss02
            // 
            this.tss02.Name = "tss02";
            this.tss02.Size = new System.Drawing.Size(96, 6);
            // 
            // tsb03
            // 
            this.tsb03.AutoSize = false;
            this.tsb03.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tsb03.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb03.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb03.Name = "tsb03";
            this.tsb03.Size = new System.Drawing.Size(96, 60);
            this.tsb03.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            // 
            // tss03
            // 
            this.tss03.Name = "tss03";
            this.tss03.Size = new System.Drawing.Size(96, 6);
            // 
            // tsb04
            // 
            this.tsb04.AutoSize = false;
            this.tsb04.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tsb04.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb04.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb04.Name = "tsb04";
            this.tsb04.Size = new System.Drawing.Size(96, 60);
            this.tsb04.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            // 
            // tsb08
            // 
            this.tsb08.AutoSize = false;
            this.tsb08.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tsb08.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb08.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb08.Name = "tsb08";
            this.tsb08.Size = new System.Drawing.Size(96, 60);
            this.tsb08.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            // 
            // pReportSelector
            // 
            this.pReportSelector.BackColor = System.Drawing.Color.Transparent;
            this.pReportSelector.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pReportSelector.Controls.Add(this.tsReport);
            this.pReportSelector.Controls.Add(this.btnDown);
            this.pReportSelector.Controls.Add(this.btnUp);
            this.pReportSelector.Dock = System.Windows.Forms.DockStyle.Left;
            this.pReportSelector.Location = new System.Drawing.Point(0, 0);
            this.pReportSelector.Name = "pReportSelector";
            this.pReportSelector.Size = new System.Drawing.Size(100, 704);
            this.pReportSelector.TabIndex = 3;
            // 
            // rvReport
            // 
            this.rvReport.Location = new System.Drawing.Point(102, 76);
            this.rvReport.Name = "rvReport";
            this.rvReport.ShowBackButton = false;
            this.rvReport.ShowContextMenu = false;
            this.rvReport.ShowDocumentMapButton = false;
            this.rvReport.ShowParameterPrompts = false;
            this.rvReport.ShowPromptAreaButton = false;
            this.rvReport.ShowRefreshButton = false;
            this.rvReport.Size = new System.Drawing.Size(963, 623);
            this.rvReport.TabIndex = 8;
            this.rvReport.TabStop = false;
            this.rvReport.RenderingComplete += new Microsoft.Reporting.WinForms.RenderingCompleteEventHandler(this.rvReport_RenderingComplete);
            this.rvReport.RenderingBegin += new System.ComponentModel.CancelEventHandler(this.rvReport_RenderingBegin);
            // 
            // POSReportViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1065, 704);
            this.Controls.Add(this.rvReport);
            this.Controls.Add(this.pParameter);
            this.Controls.Add(this.pReportSelector);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "POSReportViewer";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Report Viewer";
            this.tlpParameter.ResumeLayout(false);
            this.pParameter.ResumeLayout(false);
            this.tsReport.ResumeLayout(false);
            this.tsReport.PerformLayout();
            this.pReportSelector.ResumeLayout(false);
            this.pReportSelector.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripSeparator tss06;
        private System.Windows.Forms.ToolStripButton tsb06;
        private System.Windows.Forms.ToolStripSeparator tss07;
        private System.Windows.Forms.ToolStripButton tsb07;
        private System.Windows.Forms.ToolStripSeparator tss05;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.TableLayoutPanel tlpParameter;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ToolStripButton tsb05;
        private System.Windows.Forms.Panel pParameter;
        private System.Windows.Forms.ToolStripButton tsb01;
        private System.Windows.Forms.ToolStripSeparator tss04;
        private System.Windows.Forms.ToolStrip tsReport;
        private System.Windows.Forms.ToolStripSeparator tss01;
        private System.Windows.Forms.ToolStripButton tsb02;
        private System.Windows.Forms.ToolStripSeparator tss02;
        private System.Windows.Forms.ToolStripButton tsb03;
        private System.Windows.Forms.ToolStripSeparator tss03;
        private System.Windows.Forms.ToolStripButton tsb04;
        private System.Windows.Forms.Panel pReportSelector;
        private Microsoft.Reporting.WinForms.ReportViewer rvReport;
        private System.Windows.Forms.ToolStripButton tsb08;
    }
}