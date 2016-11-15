namespace POSClient.UI
{
    partial class MDIPOS
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MDIPOS));
            this.tsMain = new System.Windows.Forms.ToolStrip();
            this.tsbSign = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbOrderHistory = new System.Windows.Forms.ToolStripButton();
            this.tss5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbExit = new System.Windows.Forms.ToolStripButton();
            this.tslTraning = new System.Windows.Forms.ToolStripLabel();
            this.tsddbExtraFunc = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsmiReports = new System.Windows.Forms.ToolStripMenuItem();
            this.tss01 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiQuery = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiPUCDeposit = new System.Windows.Forms.ToolStripMenuItem();
            this.tss6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tsMain
            // 
            this.tsMain.AutoSize = false;
            this.tsMain.BackColor = System.Drawing.Color.Transparent;
            this.tsMain.BackgroundImage = global::POSClient.Properties.Resources.WinBack;
            this.tsMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tsMain.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsMain.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbSign,
            this.toolStripSeparator1,
            this.tsbOrderHistory,
            this.tss5,
            this.tsbExit,
            this.tslTraning,
            this.tsddbExtraFunc,
            this.tss6});
            this.tsMain.Location = new System.Drawing.Point(0, 0);
            this.tsMain.Name = "tsMain";
            this.tsMain.Padding = new System.Windows.Forms.Padding(3);
            this.tsMain.Size = new System.Drawing.Size(1018, 70);
            this.tsMain.TabIndex = 2;
            // 
            // tsbSign
            // 
            this.tsbSign.AutoSize = false;
            this.tsbSign.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tsbSign.BackgroundImage")));
            this.tsbSign.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tsbSign.Image = global::POSClient.Properties.Resources.NewOrder;
            this.tsbSign.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSign.Name = "tsbSign";
            this.tsbSign.Size = new System.Drawing.Size(80, 80);
            this.tsbSign.Tag = "SignIn";
            this.tsbSign.Text = "Sign In";
            this.tsbSign.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.tsbSign.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbSign.Click += new System.EventHandler(this.tsbSign_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 64);
            // 
            // tsbOrderHistory
            // 
            this.tsbOrderHistory.AutoSize = false;
            this.tsbOrderHistory.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.tsbOrderHistory.Image = global::POSClient.Properties.Resources.History;
            this.tsbOrderHistory.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOrderHistory.Name = "tsbOrderHistory";
            this.tsbOrderHistory.Size = new System.Drawing.Size(80, 80);
            this.tsbOrderHistory.Text = "History";
            this.tsbOrderHistory.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.tsbOrderHistory.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbOrderHistory.Click += new System.EventHandler(this.tsbOrderHistory_Click);
            // 
            // tss5
            // 
            this.tss5.Name = "tss5";
            this.tss5.Size = new System.Drawing.Size(6, 64);
            // 
            // tsbExit
            // 
            this.tsbExit.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbExit.AutoSize = false;
            this.tsbExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.tsbExit.Image = global::POSClient.Properties.Resources.Quit;
            this.tsbExit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExit.Name = "tsbExit";
            this.tsbExit.Size = new System.Drawing.Size(80, 80);
            this.tsbExit.Text = "Exit";
            this.tsbExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.tsbExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbExit.ToolTipText = "Exit";
            this.tsbExit.Click += new System.EventHandler(this.tsbExit_Click);
            // 
            // tslTraning
            // 
            this.tslTraning.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tslTraning.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tslTraning.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tslTraning.ForeColor = System.Drawing.Color.Red;
            this.tslTraning.Name = "tslTraning";
            this.tslTraning.Size = new System.Drawing.Size(88, 61);
            this.tslTraning.Text = "Training";
            this.tslTraning.Visible = false;
            // 
            // tsddbExtraFunc
            // 
            this.tsddbExtraFunc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.tsddbExtraFunc.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiReports,
            this.tss01,
            this.tsmiQuery,
            this.toolStripSeparator2,
            this.tsmiPUCDeposit});
            this.tsddbExtraFunc.Image = global::POSClient.Properties.Resources.ExtraFunc;
            this.tsddbExtraFunc.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddbExtraFunc.Name = "tsddbExtraFunc";
            this.tsddbExtraFunc.Size = new System.Drawing.Size(101, 61);
            this.tsddbExtraFunc.Text = "Extra Function";
            this.tsddbExtraFunc.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.tsddbExtraFunc.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tsmiReports
            // 
            this.tsmiReports.AutoSize = false;
            this.tsmiReports.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tsmiReports.Name = "tsmiReports";
            this.tsmiReports.Size = new System.Drawing.Size(189, 50);
            this.tsmiReports.Text = "Reports ...";
            this.tsmiReports.Click += new System.EventHandler(this.ExtraFunc_Click);
            // 
            // tss01
            // 
            this.tss01.Name = "tss01";
            this.tss01.Size = new System.Drawing.Size(192, 6);
            // 
            // tsmiQuery
            // 
            this.tsmiQuery.AutoSize = false;
            this.tsmiQuery.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tsmiQuery.Name = "tsmiQuery";
            this.tsmiQuery.Size = new System.Drawing.Size(189, 50);
            this.tsmiQuery.Text = "Distributor Query ...";
            this.tsmiQuery.Click += new System.EventHandler(this.tsmiQuery_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(192, 6);
            // 
            // tsmiPUCDeposit
            // 
            this.tsmiPUCDeposit.AutoSize = false;
            this.tsmiPUCDeposit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tsmiPUCDeposit.Name = "tsmiPUCDeposit";
            this.tsmiPUCDeposit.Size = new System.Drawing.Size(189, 50);
            this.tsmiPUCDeposit.Text = "PUC Deposit";
            this.tsmiPUCDeposit.Click += new System.EventHandler(this.tsmiPUCDeposit_Click);
            // 
            // tss6
            // 
            this.tss6.Name = "tss6";
            this.tss6.Size = new System.Drawing.Size(6, 64);
            this.tss6.Visible = false;
            // 
            // MDIPOS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1018, 706);
            this.Controls.Add(this.tsMain);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MDIPOS";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Vestige POS";
            this.Load += new System.EventHandler(this.MDIPOS_Load);
            this.tsMain.ResumeLayout(false);
            this.tsMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip tsMain;
        private System.Windows.Forms.ToolStripButton tsbOrderHistory;
        private System.Windows.Forms.ToolStripSeparator tss5;
        private System.Windows.Forms.ToolStripSeparator tss6;
        private System.Windows.Forms.ToolStripButton tsbExit;
        private System.Windows.Forms.ToolStripLabel tslTraning;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbSign;
        private System.Windows.Forms.ToolStripDropDownButton tsddbExtraFunc;
        private System.Windows.Forms.ToolStripMenuItem tsmiReports;
        private System.Windows.Forms.ToolStripSeparator tss01;
        private System.Windows.Forms.ToolStripMenuItem tsmiQuery;
        private System.Windows.Forms.ToolStripMenuItem tsmiPUCDeposit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}