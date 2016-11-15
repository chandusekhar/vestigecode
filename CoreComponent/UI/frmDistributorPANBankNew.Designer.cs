namespace CoreComponent.UI
{
    partial class frmDistributorPANBankNew
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDistributorPANBankNew));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnImgLast = new System.Windows.Forms.Button();
            this.btnImgNext = new System.Windows.Forms.Button();
            this.btnImgPrevious = new System.Windows.Forms.Button();
            this.btnImgFirst = new System.Windows.Forms.Button();
            this.grpImgPan = new System.Windows.Forms.GroupBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.picPAN = new System.Windows.Forms.PictureBox();
            this.opnFileDlgPan = new System.Windows.Forms.OpenFileDialog();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.grpImgPan.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPAN)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.btnBrowse);
            this.panel1.Controls.Add(this.btnReset);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.btnImgLast);
            this.panel1.Controls.Add(this.btnImgNext);
            this.panel1.Controls.Add(this.btnImgPrevious);
            this.panel1.Controls.Add(this.btnImgFirst);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 387);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(658, 47);
            this.panel1.TabIndex = 5;
            // 
            // btnClose
            // 
            this.btnClose.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClose.BackgroundImage")));
            this.btnClose.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(567, 8);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(77, 31);
            this.btnClose.TabIndex = 17;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnBrowse
            // 
            this.btnBrowse.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowse.BackgroundImage")));
            this.btnBrowse.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowse.Location = new System.Drawing.Point(315, 10);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(82, 29);
            this.btnBrowse.TabIndex = 16;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // btnReset
            // 
            this.btnReset.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnReset.BackgroundImage")));
            this.btnReset.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.Location = new System.Drawing.Point(485, 8);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(77, 31);
            this.btnReset.TabIndex = 15;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSave.BackgroundImage")));
            this.btnSave.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(401, 9);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(81, 30);
            this.btnSave.TabIndex = 15;
            this.btnSave.Text = "Ok";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnImgLast
            // 
            this.btnImgLast.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnImgLast.BackgroundImage")));
            this.btnImgLast.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImgLast.Location = new System.Drawing.Point(488, 10);
            this.btnImgLast.Name = "btnImgLast";
            this.btnImgLast.Size = new System.Drawing.Size(41, 34);
            this.btnImgLast.TabIndex = 13;
            this.btnImgLast.Text = ">|";
            this.btnImgLast.UseVisualStyleBackColor = true;
            this.btnImgLast.Visible = false;
            this.btnImgLast.Click += new System.EventHandler(this.btnImgLast_Click);
            // 
            // btnImgNext
            // 
            this.btnImgNext.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnImgNext.BackgroundImage")));
            this.btnImgNext.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImgNext.Location = new System.Drawing.Point(441, 10);
            this.btnImgNext.Name = "btnImgNext";
            this.btnImgNext.Size = new System.Drawing.Size(41, 34);
            this.btnImgNext.TabIndex = 12;
            this.btnImgNext.Text = ">>";
            this.btnImgNext.UseVisualStyleBackColor = true;
            this.btnImgNext.Visible = false;
            this.btnImgNext.Click += new System.EventHandler(this.btnImgNext_Click);
            // 
            // btnImgPrevious
            // 
            this.btnImgPrevious.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnImgPrevious.BackgroundImage")));
            this.btnImgPrevious.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImgPrevious.Location = new System.Drawing.Point(393, 10);
            this.btnImgPrevious.Name = "btnImgPrevious";
            this.btnImgPrevious.Size = new System.Drawing.Size(42, 34);
            this.btnImgPrevious.TabIndex = 11;
            this.btnImgPrevious.Text = "<<";
            this.btnImgPrevious.UseVisualStyleBackColor = true;
            this.btnImgPrevious.Visible = false;
            this.btnImgPrevious.Click += new System.EventHandler(this.btnImgPrevious_Click);
            // 
            // btnImgFirst
            // 
            this.btnImgFirst.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnImgFirst.BackgroundImage")));
            this.btnImgFirst.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImgFirst.Location = new System.Drawing.Point(344, 10);
            this.btnImgFirst.Name = "btnImgFirst";
            this.btnImgFirst.Size = new System.Drawing.Size(43, 34);
            this.btnImgFirst.TabIndex = 10;
            this.btnImgFirst.Text = "|<";
            this.btnImgFirst.UseVisualStyleBackColor = true;
            this.btnImgFirst.Visible = false;
            this.btnImgFirst.Click += new System.EventHandler(this.btnImgFirst_Click);
            // 
            // grpImgPan
            // 
            this.grpImgPan.Controls.Add(this.panel3);
            this.grpImgPan.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpImgPan.Location = new System.Drawing.Point(2, 46);
            this.grpImgPan.Name = "grpImgPan";
            this.grpImgPan.Size = new System.Drawing.Size(656, 343);
            this.grpImgPan.TabIndex = 3;
            this.grpImgPan.TabStop = false;
            this.grpImgPan.Text = "PAN Details";
            // 
            // panel3
            // 
            this.panel3.AutoScroll = true;
            this.panel3.AutoScrollMinSize = new System.Drawing.Size(300, 200);
            this.panel3.Controls.Add(this.picPAN);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 16);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(650, 324);
            this.panel3.TabIndex = 1;
            // 
            // picPAN
            // 
            this.picPAN.Image = ((System.Drawing.Image)(resources.GetObject("picPAN.Image")));
            this.picPAN.Location = new System.Drawing.Point(0, 0);
            this.picPAN.Name = "picPAN";
            this.picPAN.Size = new System.Drawing.Size(819, 460);
            this.picPAN.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picPAN.TabIndex = 1;
            this.picPAN.TabStop = false;
            // 
            // opnFileDlgPan
            // 
            this.opnFileDlgPan.FileName = "openFileDialog1";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(658, 45);
            this.panel2.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(117)))), ((int)(((byte)(186)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.ImageAlign = System.Drawing.ContentAlignment.TopRight;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(658, 45);
            this.label1.TabIndex = 0;
            this.label1.Text = "Distributor PAN/Bank Details";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmDistributorPANBankNew
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(658, 434);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.grpImgPan);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmDistributorPANBankNew";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Distributor PAN /Bank Details";
            this.Load += new System.EventHandler(this.frmDistributorPANBankNew_Load);
            this.panel1.ResumeLayout(false);
            this.grpImgPan.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPAN)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnImgLast;
        private System.Windows.Forms.Button btnImgNext;
        private System.Windows.Forms.Button btnImgPrevious;
        private System.Windows.Forms.Button btnImgFirst;
        private System.Windows.Forms.GroupBox grpImgPan;
        private System.Windows.Forms.OpenFileDialog opnFileDlgPan;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox picPAN;
    }
}