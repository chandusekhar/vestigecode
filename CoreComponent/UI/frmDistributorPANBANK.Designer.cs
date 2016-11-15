namespace CoreComponent.UI
{
    partial class frmDistributorPANBANK
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDistributorPANBANK));
            this.grpPAN = new System.Windows.Forms.GroupBox();
            this.btnPanBrowse = new System.Windows.Forms.Button();
            this.lblPANImgPath = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.grpBank = new System.Windows.Forms.GroupBox();
            this.btnBankBrowse = new System.Windows.Forms.Button();
            this.lblBankImgPath = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.grpImgPan = new System.Windows.Forms.GroupBox();
            this.picPAN = new System.Windows.Forms.PictureBox();
            this.grpImgBank = new System.Windows.Forms.GroupBox();
            this.picBank = new System.Windows.Forms.PictureBox();
            this.opnFileDlgPan = new System.Windows.Forms.OpenFileDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnImgLast = new System.Windows.Forms.Button();
            this.btnImgNext = new System.Windows.Forms.Button();
            this.btnImgPrevious = new System.Windows.Forms.Button();
            this.btnImgFirst = new System.Windows.Forms.Button();
            this.pnlGridSearch.SuspendLayout();
            this.pnlSearchHeader.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            this.grpPAN.SuspendLayout();
            this.grpBank.SuspendLayout();
            this.grpImgPan.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPAN)).BeginInit();
            this.grpImgBank.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBank)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlGridSearch
            // 
            this.pnlGridSearch.Controls.Add(this.panel1);
            this.pnlGridSearch.Controls.Add(this.grpImgBank);
            this.pnlGridSearch.Controls.Add(this.grpImgPan);
            this.pnlGridSearch.Size = new System.Drawing.Size(712, 292);
            // 
            // pnlSearchHeader
            // 
            this.pnlSearchHeader.Controls.Add(this.grpBank);
            this.pnlSearchHeader.Controls.Add(this.grpPAN);
            this.pnlSearchHeader.Size = new System.Drawing.Size(712, 176);
            this.pnlSearchHeader.Controls.SetChildIndex(this.grpPAN, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.pnlButtons, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.grpBank, 0);
            // 
            // lblSearchResult
            // 
            this.lblSearchResult.Size = new System.Drawing.Size(712, 28);
            // 
            // btnSave
            // 
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSave.Location = new System.Drawing.Point(562, 0);
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearch.Location = new System.Drawing.Point(487, 0);
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnReset
            // 
            this.btnReset.FlatAppearance.BorderSize = 0;
            this.btnReset.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnReset.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnReset.Location = new System.Drawing.Point(637, 0);
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // pnlButtons
            // 
            this.pnlButtons.Size = new System.Drawing.Size(712, 34);
            // 
            // grpPAN
            // 
            this.grpPAN.Controls.Add(this.btnPanBrowse);
            this.grpPAN.Controls.Add(this.lblPANImgPath);
            this.grpPAN.Controls.Add(this.label1);
            this.grpPAN.Location = new System.Drawing.Point(0, 0);
            this.grpPAN.Name = "grpPAN";
            this.grpPAN.Size = new System.Drawing.Size(350, 139);
            this.grpPAN.TabIndex = 10;
            this.grpPAN.TabStop = false;
            this.grpPAN.Text = "Distributor PAN";
            // 
            // btnPanBrowse
            // 
            this.btnPanBrowse.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPanBrowse.BackgroundImage")));
            this.btnPanBrowse.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPanBrowse.Location = new System.Drawing.Point(251, 100);
            this.btnPanBrowse.Name = "btnPanBrowse";
            this.btnPanBrowse.Size = new System.Drawing.Size(93, 34);
            this.btnPanBrowse.TabIndex = 1;
            this.btnPanBrowse.Text = "PAN Browse ";
            this.btnPanBrowse.UseVisualStyleBackColor = true;
            this.btnPanBrowse.Click += new System.EventHandler(this.btnPanBrowse_Click);
            // 
            // lblPANImgPath
            // 
            this.lblPANImgPath.AutoSize = true;
            this.lblPANImgPath.Location = new System.Drawing.Point(92, 31);
            this.lblPANImgPath.Name = "lblPANImgPath";
            this.lblPANImgPath.Size = new System.Drawing.Size(0, 13);
            this.lblPANImgPath.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "PAN Filepath";
            // 
            // grpBank
            // 
            this.grpBank.Controls.Add(this.btnBankBrowse);
            this.grpBank.Controls.Add(this.lblBankImgPath);
            this.grpBank.Controls.Add(this.label4);
            this.grpBank.Location = new System.Drawing.Point(353, 3);
            this.grpBank.Name = "grpBank";
            this.grpBank.Size = new System.Drawing.Size(356, 136);
            this.grpBank.TabIndex = 11;
            this.grpBank.TabStop = false;
            this.grpBank.Text = "DistributorBank";
            // 
            // btnBankBrowse
            // 
            this.btnBankBrowse.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBankBrowse.BackgroundImage")));
            this.btnBankBrowse.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBankBrowse.Location = new System.Drawing.Point(251, 99);
            this.btnBankBrowse.Name = "btnBankBrowse";
            this.btnBankBrowse.Size = new System.Drawing.Size(99, 33);
            this.btnBankBrowse.TabIndex = 1;
            this.btnBankBrowse.Text = "Bank Browse";
            this.btnBankBrowse.UseVisualStyleBackColor = true;
            this.btnBankBrowse.Click += new System.EventHandler(this.btnBankBrowse_Click);
            // 
            // lblBankImgPath
            // 
            this.lblBankImgPath.AutoSize = true;
            this.lblBankImgPath.Location = new System.Drawing.Point(98, 28);
            this.lblBankImgPath.Name = "lblBankImgPath";
            this.lblBankImgPath.Size = new System.Drawing.Size(0, 13);
            this.lblBankImgPath.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Bank Filepath";
            // 
            // grpImgPan
            // 
            this.grpImgPan.Controls.Add(this.picPAN);
            this.grpImgPan.Location = new System.Drawing.Point(3, 0);
            this.grpImgPan.Name = "grpImgPan";
            this.grpImgPan.Size = new System.Drawing.Size(348, 228);
            this.grpImgPan.TabIndex = 0;
            this.grpImgPan.TabStop = false;
            this.grpImgPan.Text = "PAN";
            // 
            // picPAN
            // 
            this.picPAN.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picPAN.Location = new System.Drawing.Point(3, 16);
            this.picPAN.Name = "picPAN";
            this.picPAN.Size = new System.Drawing.Size(342, 209);
            this.picPAN.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picPAN.TabIndex = 0;
            this.picPAN.TabStop = false;
            // 
            // grpImgBank
            // 
            this.grpImgBank.Controls.Add(this.picBank);
            this.grpImgBank.Location = new System.Drawing.Point(353, 3);
            this.grpImgBank.Name = "grpImgBank";
            this.grpImgBank.Size = new System.Drawing.Size(355, 224);
            this.grpImgBank.TabIndex = 1;
            this.grpImgBank.TabStop = false;
            this.grpImgBank.Text = "Bank";
            // 
            // picBank
            // 
            this.picBank.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picBank.Location = new System.Drawing.Point(3, 16);
            this.picBank.Name = "picBank";
            this.picBank.Size = new System.Drawing.Size(349, 205);
            this.picBank.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBank.TabIndex = 0;
            this.picBank.TabStop = false;
            // 
            // opnFileDlgPan
            // 
            this.opnFileDlgPan.FileName = "openFileDialog1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnImgLast);
            this.panel1.Controls.Add(this.btnImgNext);
            this.panel1.Controls.Add(this.btnImgPrevious);
            this.panel1.Controls.Add(this.btnImgFirst);
            this.panel1.Location = new System.Drawing.Point(6, 234);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(700, 54);
            this.panel1.TabIndex = 2;
            // 
            // btnImgLast
            // 
            this.btnImgLast.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnImgLast.BackgroundImage")));
            this.btnImgLast.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImgLast.Location = new System.Drawing.Point(652, 10);
            this.btnImgLast.Name = "btnImgLast";
            this.btnImgLast.Size = new System.Drawing.Size(41, 34);
            this.btnImgLast.TabIndex = 13;
            this.btnImgLast.Text = ">|";
            this.btnImgLast.UseVisualStyleBackColor = true;
            this.btnImgLast.Click += new System.EventHandler(this.btnImgLast_Click);
            // 
            // btnImgNext
            // 
            this.btnImgNext.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnImgNext.BackgroundImage")));
            this.btnImgNext.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImgNext.Location = new System.Drawing.Point(605, 10);
            this.btnImgNext.Name = "btnImgNext";
            this.btnImgNext.Size = new System.Drawing.Size(41, 34);
            this.btnImgNext.TabIndex = 12;
            this.btnImgNext.Text = ">>";
            this.btnImgNext.UseVisualStyleBackColor = true;
            this.btnImgNext.Click += new System.EventHandler(this.btnImgNext_Click);
            // 
            // btnImgPrevious
            // 
            this.btnImgPrevious.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnImgPrevious.BackgroundImage")));
            this.btnImgPrevious.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImgPrevious.Location = new System.Drawing.Point(557, 10);
            this.btnImgPrevious.Name = "btnImgPrevious";
            this.btnImgPrevious.Size = new System.Drawing.Size(42, 34);
            this.btnImgPrevious.TabIndex = 11;
            this.btnImgPrevious.Text = "<<";
            this.btnImgPrevious.UseVisualStyleBackColor = true;
            this.btnImgPrevious.Click += new System.EventHandler(this.btnImgPrevious_Click);
            // 
            // btnImgFirst
            // 
            this.btnImgFirst.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnImgFirst.BackgroundImage")));
            this.btnImgFirst.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImgFirst.Location = new System.Drawing.Point(508, 10);
            this.btnImgFirst.Name = "btnImgFirst";
            this.btnImgFirst.Size = new System.Drawing.Size(43, 34);
            this.btnImgFirst.TabIndex = 10;
            this.btnImgFirst.Text = "|<";
            this.btnImgFirst.UseVisualStyleBackColor = true;
            this.btnImgFirst.Click += new System.EventHandler(this.btnImgFirst_Click);
            // 
            // frmDistributorPANBANK
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(718, 540);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "frmDistributorPANBANK";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmDistributorPANBANK";
            this.WindowState = System.Windows.Forms.FormWindowState.Normal;
            this.Load += new System.EventHandler(this.frmDistributorPANBANK_Load);
            this.pnlGridSearch.ResumeLayout(false);
            this.pnlSearchHeader.ResumeLayout(false);
            this.pnlButtons.ResumeLayout(false);
            this.grpPAN.ResumeLayout(false);
            this.grpPAN.PerformLayout();
            this.grpBank.ResumeLayout(false);
            this.grpBank.PerformLayout();
            this.grpImgPan.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picPAN)).EndInit();
            this.grpImgBank.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBank)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpImgPan;
        private System.Windows.Forms.GroupBox grpBank;
        private System.Windows.Forms.GroupBox grpPAN;
        private System.Windows.Forms.GroupBox grpImgBank;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox picBank;
        private System.Windows.Forms.PictureBox picPAN;
        private System.Windows.Forms.Label lblBankImgPath;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblPANImgPath;
        private System.Windows.Forms.Button btnBankBrowse;
        private System.Windows.Forms.Button btnPanBrowse;
        private System.Windows.Forms.OpenFileDialog opnFileDlgPan;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnImgLast;
        private System.Windows.Forms.Button btnImgNext;
        private System.Windows.Forms.Button btnImgPrevious;
        private System.Windows.Forms.Button btnImgFirst;
    }
}