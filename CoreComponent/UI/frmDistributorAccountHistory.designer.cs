namespace CoreComponent.UI
{
    partial class frmDistributorAccountHistory
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDistributorAccountHistory));
            this.pnlDitributorHistory = new System.Windows.Forms.Panel();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnLastRecord = new System.Windows.Forms.Button();
            this.btnForwrd = new System.Windows.Forms.Button();
            this.btnBackWrd = new System.Windows.Forms.Button();
            this.btnFirstRecord = new System.Windows.Forms.Button();
            this.pnlContact = new System.Windows.Forms.Panel();
            this.grpAddress = new System.Windows.Forms.GroupBox();
            this.btnBankDetail = new System.Windows.Forms.Button();
            this.btnPANDetails = new System.Windows.Forms.Button();
            this.txtPANNo = new System.Windows.Forms.TextBox();
            this.lblDistributorPan = new System.Windows.Forms.Label();
            this.txtDistributorLName = new System.Windows.Forms.TextBox();
            this.txtDistributorFName = new System.Windows.Forms.TextBox();
            this.lblDistributorName = new System.Windows.Forms.Label();
            this.txtDistributorId = new System.Windows.Forms.TextBox();
            this.lblDistributorId = new System.Windows.Forms.Label();
            this.txtSaveOn = new System.Windows.Forms.TextBox();
            this.lblSaveOn = new System.Windows.Forms.Label();
            this.txtSavedBy = new System.Windows.Forms.TextBox();
            this.lblSavedBY = new System.Windows.Forms.Label();
            this.txtDistributorBankBranch = new System.Windows.Forms.TextBox();
            this.txtBankAccountNo = new System.Windows.Forms.TextBox();
            this.txtBankName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblAddress = new System.Windows.Forms.Label();
            this.lblDH = new System.Windows.Forms.Label();
            this.btnDistributorH = new System.Windows.Forms.Button();
            this.pnlDitributorHistory.SuspendLayout();
            this.pnlContact.SuspendLayout();
            this.grpAddress.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlDitributorHistory
            // 
            this.pnlDitributorHistory.Controls.Add(this.btnExit);
            this.pnlDitributorHistory.Controls.Add(this.btnLastRecord);
            this.pnlDitributorHistory.Controls.Add(this.btnForwrd);
            this.pnlDitributorHistory.Controls.Add(this.btnBackWrd);
            this.pnlDitributorHistory.Controls.Add(this.btnFirstRecord);
            this.pnlDitributorHistory.Controls.Add(this.pnlContact);
            this.pnlDitributorHistory.Controls.Add(this.lblDH);
            this.pnlDitributorHistory.Controls.Add(this.btnDistributorH);
            this.pnlDitributorHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDitributorHistory.Location = new System.Drawing.Point(0, 0);
            this.pnlDitributorHistory.Name = "pnlDitributorHistory";
            this.pnlDitributorHistory.Size = new System.Drawing.Size(719, 306);
            this.pnlDitributorHistory.TabIndex = 0;
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.Location = new System.Drawing.Point(437, 24);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(28, 23);
            this.btnExit.TabIndex = 10;
            this.btnExit.Tag = "exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.DataNavigation);
            // 
            // btnLastRecord
            // 
            this.btnLastRecord.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLastRecord.Location = new System.Drawing.Point(404, 23);
            this.btnLastRecord.Name = "btnLastRecord";
            this.btnLastRecord.Size = new System.Drawing.Size(27, 23);
            this.btnLastRecord.TabIndex = 9;
            this.btnLastRecord.Tag = "last";
            this.btnLastRecord.Text = ">|";
            this.btnLastRecord.UseVisualStyleBackColor = true;
            this.btnLastRecord.Click += new System.EventHandler(this.DataNavigation);
            // 
            // btnForwrd
            // 
            this.btnForwrd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnForwrd.Location = new System.Drawing.Point(373, 23);
            this.btnForwrd.Name = "btnForwrd";
            this.btnForwrd.Size = new System.Drawing.Size(27, 23);
            this.btnForwrd.TabIndex = 8;
            this.btnForwrd.Tag = "next";
            this.btnForwrd.Text = ">";
            this.btnForwrd.UseVisualStyleBackColor = true;
            this.btnForwrd.Click += new System.EventHandler(this.DataNavigation);
            // 
            // btnBackWrd
            // 
            this.btnBackWrd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBackWrd.Location = new System.Drawing.Point(340, 23);
            this.btnBackWrd.Name = "btnBackWrd";
            this.btnBackWrd.Size = new System.Drawing.Size(27, 23);
            this.btnBackWrd.TabIndex = 7;
            this.btnBackWrd.Tag = "previous";
            this.btnBackWrd.Text = "<";
            this.btnBackWrd.UseVisualStyleBackColor = true;
            this.btnBackWrd.Click += new System.EventHandler(this.DataNavigation);
            // 
            // btnFirstRecord
            // 
            this.btnFirstRecord.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFirstRecord.Location = new System.Drawing.Point(307, 23);
            this.btnFirstRecord.Name = "btnFirstRecord";
            this.btnFirstRecord.Size = new System.Drawing.Size(27, 23);
            this.btnFirstRecord.TabIndex = 6;
            this.btnFirstRecord.Tag = "first";
            this.btnFirstRecord.Text = "|<";
            this.btnFirstRecord.UseVisualStyleBackColor = true;
            this.btnFirstRecord.Click += new System.EventHandler(this.DataNavigation);
            // 
            // pnlContact
            // 
            this.pnlContact.Controls.Add(this.grpAddress);
            this.pnlContact.Location = new System.Drawing.Point(0, 68);
            this.pnlContact.Name = "pnlContact";
            this.pnlContact.Size = new System.Drawing.Size(716, 236);
            this.pnlContact.TabIndex = 5;
            // 
            // grpAddress
            // 
            this.grpAddress.Controls.Add(this.btnBankDetail);
            this.grpAddress.Controls.Add(this.btnPANDetails);
            this.grpAddress.Controls.Add(this.txtPANNo);
            this.grpAddress.Controls.Add(this.lblDistributorPan);
            this.grpAddress.Controls.Add(this.txtDistributorLName);
            this.grpAddress.Controls.Add(this.txtDistributorFName);
            this.grpAddress.Controls.Add(this.lblDistributorName);
            this.grpAddress.Controls.Add(this.txtDistributorId);
            this.grpAddress.Controls.Add(this.lblDistributorId);
            this.grpAddress.Controls.Add(this.txtSaveOn);
            this.grpAddress.Controls.Add(this.lblSaveOn);
            this.grpAddress.Controls.Add(this.txtSavedBy);
            this.grpAddress.Controls.Add(this.lblSavedBY);
            this.grpAddress.Controls.Add(this.txtDistributorBankBranch);
            this.grpAddress.Controls.Add(this.txtBankAccountNo);
            this.grpAddress.Controls.Add(this.txtBankName);
            this.grpAddress.Controls.Add(this.label2);
            this.grpAddress.Controls.Add(this.label1);
            this.grpAddress.Controls.Add(this.lblAddress);
            this.grpAddress.Location = new System.Drawing.Point(11, 3);
            this.grpAddress.Name = "grpAddress";
            this.grpAddress.Size = new System.Drawing.Size(693, 227);
            this.grpAddress.TabIndex = 20;
            this.grpAddress.TabStop = false;
            this.grpAddress.Enter += new System.EventHandler(this.grpAddress_Enter);
            // 
            // btnBankDetail
            // 
            this.btnBankDetail.BackgroundImage = global::CoreComponent.Properties.Resources.button;
            this.btnBankDetail.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBankDetail.FlatAppearance.BorderSize = 0;
            this.btnBankDetail.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnBankDetail.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnBankDetail.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBankDetail.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnBankDetail.Location = new System.Drawing.Point(541, 123);
            this.btnBankDetail.Name = "btnBankDetail";
            this.btnBankDetail.Size = new System.Drawing.Size(103, 30);
            this.btnBankDetail.TabIndex = 113;
            this.btnBankDetail.Text = "Bank Details";
            this.btnBankDetail.UseVisualStyleBackColor = false;
            this.btnBankDetail.Click += new System.EventHandler(this.btnBankDetail_Click);
            // 
            // btnPANDetails
            // 
            this.btnPANDetails.BackgroundImage = global::CoreComponent.Properties.Resources.button;
            this.btnPANDetails.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPANDetails.FlatAppearance.BorderSize = 0;
            this.btnPANDetails.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnPANDetails.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnPANDetails.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPANDetails.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnPANDetails.Location = new System.Drawing.Point(540, 69);
            this.btnPANDetails.Name = "btnPANDetails";
            this.btnPANDetails.Size = new System.Drawing.Size(103, 30);
            this.btnPANDetails.TabIndex = 113;
            this.btnPANDetails.Text = "PAN Details";
            this.btnPANDetails.UseVisualStyleBackColor = false;
            this.btnPANDetails.Click += new System.EventHandler(this.btnPANDetails_Click);
            // 
            // txtPANNo
            // 
            this.txtPANNo.Location = new System.Drawing.Point(243, 73);
            this.txtPANNo.Name = "txtPANNo";
            this.txtPANNo.ReadOnly = true;
            this.txtPANNo.Size = new System.Drawing.Size(138, 20);
            this.txtPANNo.TabIndex = 33;
            // 
            // lblDistributorPan
            // 
            this.lblDistributorPan.Location = new System.Drawing.Point(188, 77);
            this.lblDistributorPan.Name = "lblDistributorPan";
            this.lblDistributorPan.Size = new System.Drawing.Size(49, 18);
            this.lblDistributorPan.TabIndex = 32;
            this.lblDistributorPan.Text = "PAN No:";
            // 
            // txtDistributorLName
            // 
            this.txtDistributorLName.Location = new System.Drawing.Point(387, 47);
            this.txtDistributorLName.Name = "txtDistributorLName";
            this.txtDistributorLName.ReadOnly = true;
            this.txtDistributorLName.Size = new System.Drawing.Size(147, 20);
            this.txtDistributorLName.TabIndex = 31;
            // 
            // txtDistributorFName
            // 
            this.txtDistributorFName.Location = new System.Drawing.Point(243, 47);
            this.txtDistributorFName.Name = "txtDistributorFName";
            this.txtDistributorFName.ReadOnly = true;
            this.txtDistributorFName.Size = new System.Drawing.Size(138, 20);
            this.txtDistributorFName.TabIndex = 30;
            // 
            // lblDistributorName
            // 
            this.lblDistributorName.Location = new System.Drawing.Point(149, 50);
            this.lblDistributorName.Name = "lblDistributorName";
            this.lblDistributorName.Size = new System.Drawing.Size(88, 15);
            this.lblDistributorName.TabIndex = 29;
            this.lblDistributorName.Text = "Distributor Name:";
            // 
            // txtDistributorId
            // 
            this.txtDistributorId.Location = new System.Drawing.Point(244, 19);
            this.txtDistributorId.Name = "txtDistributorId";
            this.txtDistributorId.ReadOnly = true;
            this.txtDistributorId.Size = new System.Drawing.Size(287, 20);
            this.txtDistributorId.TabIndex = 28;
            // 
            // lblDistributorId
            // 
            this.lblDistributorId.Location = new System.Drawing.Point(168, 22);
            this.lblDistributorId.Name = "lblDistributorId";
            this.lblDistributorId.Size = new System.Drawing.Size(73, 18);
            this.lblDistributorId.TabIndex = 27;
            this.lblDistributorId.Text = "DistributorId :";
            this.lblDistributorId.Click += new System.EventHandler(this.lblDistributorId_Click);
            // 
            // txtSaveOn
            // 
            this.txtSaveOn.Location = new System.Drawing.Point(406, 185);
            this.txtSaveOn.Name = "txtSaveOn";
            this.txtSaveOn.ReadOnly = true;
            this.txtSaveOn.Size = new System.Drawing.Size(129, 20);
            this.txtSaveOn.TabIndex = 26;
            // 
            // lblSaveOn
            // 
            this.lblSaveOn.Location = new System.Drawing.Point(356, 188);
            this.lblSaveOn.Name = "lblSaveOn";
            this.lblSaveOn.Size = new System.Drawing.Size(56, 17);
            this.lblSaveOn.TabIndex = 25;
            this.lblSaveOn.Text = "SaveOn";
            // 
            // txtSavedBy
            // 
            this.txtSavedBy.Location = new System.Drawing.Point(244, 184);
            this.txtSavedBy.Name = "txtSavedBy";
            this.txtSavedBy.ReadOnly = true;
            this.txtSavedBy.Size = new System.Drawing.Size(110, 20);
            this.txtSavedBy.TabIndex = 22;
            // 
            // lblSavedBY
            // 
            this.lblSavedBY.Location = new System.Drawing.Point(187, 187);
            this.lblSavedBY.Name = "lblSavedBY";
            this.lblSavedBY.Size = new System.Drawing.Size(56, 17);
            this.lblSavedBY.TabIndex = 21;
            this.lblSavedBY.Text = "SavedBy";
            // 
            // txtDistributorBankBranch
            // 
            this.txtDistributorBankBranch.Location = new System.Drawing.Point(244, 155);
            this.txtDistributorBankBranch.Name = "txtDistributorBankBranch";
            this.txtDistributorBankBranch.ReadOnly = true;
            this.txtDistributorBankBranch.Size = new System.Drawing.Size(291, 20);
            this.txtDistributorBankBranch.TabIndex = 8;
            // 
            // txtBankAccountNo
            // 
            this.txtBankAccountNo.Location = new System.Drawing.Point(244, 129);
            this.txtBankAccountNo.Name = "txtBankAccountNo";
            this.txtBankAccountNo.ReadOnly = true;
            this.txtBankAccountNo.Size = new System.Drawing.Size(291, 20);
            this.txtBankAccountNo.TabIndex = 7;
            // 
            // txtBankName
            // 
            this.txtBankName.Location = new System.Drawing.Point(244, 103);
            this.txtBankName.Name = "txtBankName";
            this.txtBankName.ReadOnly = true;
            this.txtBankName.Size = new System.Drawing.Size(291, 20);
            this.txtBankName.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(137, 158);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "RTGS/IFSC Code :";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(116, 131);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Bank Account Number :";
            // 
            // lblAddress
            // 
            this.lblAddress.Location = new System.Drawing.Point(169, 105);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(73, 23);
            this.lblAddress.TabIndex = 0;
            this.lblAddress.Text = "Bank Name :";
            // 
            // lblDH
            // 
            this.lblDH.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(233)))), ((int)(((byte)(251)))));
            this.lblDH.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDH.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblDH.Font = new System.Drawing.Font("Arial", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDH.Location = new System.Drawing.Point(32, 24);
            this.lblDH.Name = "lblDH";
            this.lblDH.Size = new System.Drawing.Size(259, 23);
            this.lblDH.TabIndex = 4;
            this.lblDH.Text = "        Distributor PAN/Bank  History";
            // 
            // btnDistributorH
            // 
            this.btnDistributorH.Enabled = false;
            this.btnDistributorH.Location = new System.Drawing.Point(11, 12);
            this.btnDistributorH.Name = "btnDistributorH";
            this.btnDistributorH.Size = new System.Drawing.Size(478, 51);
            this.btnDistributorH.TabIndex = 1;
            this.btnDistributorH.UseVisualStyleBackColor = true;
            // 
            // frmDistributorAccountHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(233)))), ((int)(((byte)(251)))));
            this.ClientSize = new System.Drawing.Size(719, 306);
            this.Controls.Add(this.pnlDitributorHistory);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDistributorAccountHistory";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Distributor PAN/Bank Details History";
            this.Load += new System.EventHandler(this.frmDistributorAccountHistory_Load);
            this.pnlDitributorHistory.ResumeLayout(false);
            this.pnlContact.ResumeLayout(false);
            this.grpAddress.ResumeLayout(false);
            this.grpAddress.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlDitributorHistory;
        private System.Windows.Forms.Button btnDistributorH;
        private System.Windows.Forms.Label lblDH;
        private System.Windows.Forms.Panel pnlContact;
        private System.Windows.Forms.GroupBox grpAddress;
        private System.Windows.Forms.TextBox txtDistributorBankBranch;
        private System.Windows.Forms.TextBox txtBankAccountNo;
        private System.Windows.Forms.TextBox txtBankName;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.TextBox txtSavedBy;
        private System.Windows.Forms.Label lblSavedBY;
        private System.Windows.Forms.Button btnFirstRecord;
        private System.Windows.Forms.Button btnLastRecord;
        private System.Windows.Forms.Button btnForwrd;
        private System.Windows.Forms.Button btnBackWrd;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.TextBox txtSaveOn;
        private System.Windows.Forms.Label lblSaveOn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDistributorFName;
        private System.Windows.Forms.Label lblDistributorName;
        private System.Windows.Forms.TextBox txtDistributorId;
        private System.Windows.Forms.Label lblDistributorId;
        private System.Windows.Forms.TextBox txtPANNo;
        private System.Windows.Forms.Label lblDistributorPan;
        private System.Windows.Forms.TextBox txtDistributorLName;
        private System.Windows.Forms.Button btnBankDetail;
        private System.Windows.Forms.Button btnPANDetails;
    }
}