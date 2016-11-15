namespace POSClient.UI
{
    partial class BonusCheque
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.lblChequeNo = new System.Windows.Forms.Label();
            this.lblBankName = new System.Windows.Forms.Label();
            this.txtChequeNo = new System.Windows.Forms.TextBox();
            this.txtBankName = new System.Windows.Forms.TextBox();
            this.txtTotalAmount = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.txtBalanceAmount = new System.Windows.Forms.TextBox();
            this.txtUsedAmount = new System.Windows.Forms.TextBox();
            this.lblBalanceAmount = new System.Windows.Forms.Label();
            this.lblUsedAmount = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.chkCanBeUsed = new System.Windows.Forms.CheckBox();
            this.txtUseAmount = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtOrderNo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtExpiryDate = new System.Windows.Forms.TextBox();
            this.lblExpiryDate = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btnCancel.BackgroundImage = global::POSClient.Properties.Resources.GlassyLook;
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.Location = new System.Drawing.Point(516, 189);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 62);
            this.btnCancel.TabIndex = 50;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(219)))), ((int)(((byte)(192)))));
            this.btnOk.BackgroundImage = global::POSClient.Properties.Resources.GlassyLook;
            this.btnOk.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnOk.Location = new System.Drawing.Point(415, 189);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(72, 62);
            this.btnOk.TabIndex = 40;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // lblChequeNo
            // 
            this.lblChequeNo.AutoSize = true;
            this.lblChequeNo.BackColor = System.Drawing.Color.Transparent;
            this.lblChequeNo.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChequeNo.Location = new System.Drawing.Point(25, 25);
            this.lblChequeNo.Name = "lblChequeNo";
            this.lblChequeNo.Size = new System.Drawing.Size(84, 13);
            this.lblChequeNo.TabIndex = 6;
            this.lblChequeNo.Text = "Cheque No :";
            this.lblChequeNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblBankName
            // 
            this.lblBankName.AutoSize = true;
            this.lblBankName.BackColor = System.Drawing.Color.Transparent;
            this.lblBankName.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBankName.Location = new System.Drawing.Point(296, 25);
            this.lblBankName.Name = "lblBankName";
            this.lblBankName.Size = new System.Drawing.Size(88, 13);
            this.lblBankName.TabIndex = 7;
            this.lblBankName.Text = "Bank Name :";
            this.lblBankName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtChequeNo
            // 
            this.txtChequeNo.Location = new System.Drawing.Point(115, 20);
            this.txtChequeNo.Name = "txtChequeNo";
            this.txtChequeNo.Size = new System.Drawing.Size(147, 23);
            this.txtChequeNo.TabIndex = 8;
            this.txtChequeNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtChequeNo_KeyDown);
            this.txtChequeNo.Validating += new System.ComponentModel.CancelEventHandler(this.txtChequeNo_Validating);
            // 
            // txtBankName
            // 
            this.txtBankName.Enabled = false;
            this.txtBankName.Location = new System.Drawing.Point(390, 20);
            this.txtBankName.Name = "txtBankName";
            this.txtBankName.Size = new System.Drawing.Size(147, 23);
            this.txtBankName.TabIndex = 9;
            // 
            // txtTotalAmount
            // 
            this.txtTotalAmount.Enabled = false;
            this.txtTotalAmount.Location = new System.Drawing.Point(390, 51);
            this.txtTotalAmount.Name = "txtTotalAmount";
            this.txtTotalAmount.Size = new System.Drawing.Size(147, 23);
            this.txtTotalAmount.TabIndex = 13;
            // 
            // txtName
            // 
            this.txtName.Enabled = false;
            this.txtName.Location = new System.Drawing.Point(115, 51);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(147, 23);
            this.txtName.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(323, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Amount:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.BackColor = System.Drawing.Color.Transparent;
            this.lblName.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(59, 56);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(48, 13);
            this.lblName.TabIndex = 10;
            this.lblName.Text = "Name:";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtBalanceAmount
            // 
            this.txtBalanceAmount.Enabled = false;
            this.txtBalanceAmount.Location = new System.Drawing.Point(390, 83);
            this.txtBalanceAmount.Name = "txtBalanceAmount";
            this.txtBalanceAmount.Size = new System.Drawing.Size(147, 23);
            this.txtBalanceAmount.TabIndex = 17;
            // 
            // txtUsedAmount
            // 
            this.txtUsedAmount.Enabled = false;
            this.txtUsedAmount.Location = new System.Drawing.Point(115, 83);
            this.txtUsedAmount.Name = "txtUsedAmount";
            this.txtUsedAmount.Size = new System.Drawing.Size(147, 23);
            this.txtUsedAmount.TabIndex = 16;
            // 
            // lblBalanceAmount
            // 
            this.lblBalanceAmount.AutoSize = true;
            this.lblBalanceAmount.BackColor = System.Drawing.Color.Transparent;
            this.lblBalanceAmount.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBalanceAmount.Location = new System.Drawing.Point(268, 88);
            this.lblBalanceAmount.Name = "lblBalanceAmount";
            this.lblBalanceAmount.Size = new System.Drawing.Size(116, 13);
            this.lblBalanceAmount.TabIndex = 15;
            this.lblBalanceAmount.Text = "Balance Amount:";
            this.lblBalanceAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblUsedAmount
            // 
            this.lblUsedAmount.AutoSize = true;
            this.lblUsedAmount.BackColor = System.Drawing.Color.Transparent;
            this.lblUsedAmount.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsedAmount.Location = new System.Drawing.Point(10, 88);
            this.lblUsedAmount.Name = "lblUsedAmount";
            this.lblUsedAmount.Size = new System.Drawing.Size(97, 13);
            this.lblUsedAmount.TabIndex = 14;
            this.lblUsedAmount.Text = "Used Amount:";
            this.lblUsedAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(18, 119);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Can Be Used:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkCanBeUsed
            // 
            this.chkCanBeUsed.AutoSize = true;
            this.chkCanBeUsed.Enabled = false;
            this.chkCanBeUsed.Location = new System.Drawing.Point(115, 119);
            this.chkCanBeUsed.Name = "chkCanBeUsed";
            this.chkCanBeUsed.Size = new System.Drawing.Size(15, 14);
            this.chkCanBeUsed.TabIndex = 19;
            this.chkCanBeUsed.UseVisualStyleBackColor = true;
            // 
            // txtUseAmount
            // 
            this.txtUseAmount.Location = new System.Drawing.Point(221, 207);
            this.txtUseAmount.Name = "txtUseAmount";
            this.txtUseAmount.Size = new System.Drawing.Size(147, 23);
            this.txtUseAmount.TabIndex = 21;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(112, 212);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "Use Amount:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtOrderNo
            // 
            this.txtOrderNo.Enabled = false;
            this.txtOrderNo.Location = new System.Drawing.Point(390, 114);
            this.txtOrderNo.Name = "txtOrderNo";
            this.txtOrderNo.Size = new System.Drawing.Size(147, 23);
            this.txtOrderNo.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(271, 119);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(113, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "Team/Order No:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtExpiryDate
            // 
            this.txtExpiryDate.Enabled = false;
            this.txtExpiryDate.Location = new System.Drawing.Point(115, 144);
            this.txtExpiryDate.Name = "txtExpiryDate";
            this.txtExpiryDate.Size = new System.Drawing.Size(147, 23);
            this.txtExpiryDate.TabIndex = 25;
            // 
            // lblExpiryDate
            // 
            this.lblExpiryDate.AutoSize = true;
            this.lblExpiryDate.BackColor = System.Drawing.Color.Transparent;
            this.lblExpiryDate.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExpiryDate.Location = new System.Drawing.Point(20, 149);
            this.lblExpiryDate.Name = "lblExpiryDate";
            this.lblExpiryDate.Size = new System.Drawing.Size(87, 13);
            this.lblExpiryDate.TabIndex = 24;
            this.lblExpiryDate.Text = "Expiry Date:";
            this.lblExpiryDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // BonusCheque
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(606, 265);
            this.Controls.Add(this.txtExpiryDate);
            this.Controls.Add(this.lblExpiryDate);
            this.Controls.Add(this.txtOrderNo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtUseAmount);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.chkCanBeUsed);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtBalanceAmount);
            this.Controls.Add(this.txtUsedAmount);
            this.Controls.Add(this.lblBalanceAmount);
            this.Controls.Add(this.lblUsedAmount);
            this.Controls.Add(this.txtTotalAmount);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.txtBankName);
            this.Controls.Add(this.txtChequeNo);
            this.Controls.Add(this.lblBankName);
            this.Controls.Add(this.lblChequeNo);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "BonusCheque";
            this.Text = "Bonus Cheque";
            this.Load += new System.EventHandler(this.BonusCheque_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label lblChequeNo;
        private System.Windows.Forms.Label lblBankName;
        private System.Windows.Forms.TextBox txtChequeNo;
        private System.Windows.Forms.TextBox txtBankName;
        private System.Windows.Forms.TextBox txtTotalAmount;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtBalanceAmount;
        private System.Windows.Forms.TextBox txtUsedAmount;
        private System.Windows.Forms.Label lblBalanceAmount;
        private System.Windows.Forms.Label lblUsedAmount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkCanBeUsed;
        private System.Windows.Forms.TextBox txtUseAmount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtOrderNo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtExpiryDate;
        private System.Windows.Forms.Label lblExpiryDate;
    }
}