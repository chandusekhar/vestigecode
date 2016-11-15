namespace POSClient.UI
{
    partial class CreditCard: BaseChildForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblCardMessage2 = new System.Windows.Forms.Label();
            this.lblCardMessage = new System.Windows.Forms.Label();
            this.btnManual = new System.Windows.Forms.Button();
            this.btnCancelSwipe = new System.Windows.Forms.Button();
            this.pnlSwipe = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.pnlManual = new System.Windows.Forms.Panel();
            this.Astx8 = new System.Windows.Forms.Label();
            this.Astx2 = new System.Windows.Forms.Label();
            this.Astx4 = new System.Windows.Forms.Label();
            this.Astx1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtValidationResult = new System.Windows.Forms.TextBox();
            this.pbLogo = new System.Windows.Forms.PictureBox();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.oskbCreditCard = new POSClient.UI.Controls.OnScreenKeyBoard();
            this.val8 = new System.Windows.Forms.TextBox();
            this.val2 = new System.Windows.Forms.TextBox();
            this.val4 = new System.Windows.Forms.TextBox();
            this.val1 = new System.Windows.Forms.TextBox();
            this.lblCardHolderName = new System.Windows.Forms.Label();
            this.lblExpiryDate = new System.Windows.Forms.Label();
            this.lblCardNumber = new System.Windows.Forms.Label();
            this.lblCardType = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.pnlSwipe.SuspendLayout();
            this.pnlManual.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.lblCardMessage2);
            this.panel1.Controls.Add(this.lblCardMessage);
            this.panel1.Location = new System.Drawing.Point(12, 16);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(464, 317);
            this.panel1.TabIndex = 0;
            // 
            // lblCardMessage2
            // 
            this.lblCardMessage2.AutoSize = true;
            this.lblCardMessage2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCardMessage2.Location = new System.Drawing.Point(190, 280);
            this.lblCardMessage2.Name = "lblCardMessage2";
            this.lblCardMessage2.Size = new System.Drawing.Size(261, 14);
            this.lblCardMessage2.TabIndex = 1;
            this.lblCardMessage2.Text = "or Press Manual Button for Manual Entry";
            // 
            // lblCardMessage
            // 
            this.lblCardMessage.AutoSize = true;
            this.lblCardMessage.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCardMessage.Location = new System.Drawing.Point(45, 119);
            this.lblCardMessage.Name = "lblCardMessage";
            this.lblCardMessage.Size = new System.Drawing.Size(368, 25);
            this.lblCardMessage.TabIndex = 0;
            this.lblCardMessage.Text = "Please swipe Credit or Debit Card";
            // 
            // btnManual
            // 
            this.btnManual.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(227)))), ((int)(((byte)(195)))));
            this.btnManual.BackgroundImage = global::POSClient.Properties.Resources.GlassyLook;
            this.btnManual.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnManual.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnManual.Location = new System.Drawing.Point(490, 202);
            this.btnManual.Name = "btnManual";
            this.btnManual.Size = new System.Drawing.Size(90, 60);
            this.btnManual.TabIndex = 1;
            this.btnManual.Text = "Manual";
            this.btnManual.UseVisualStyleBackColor = false;
            this.btnManual.Click += new System.EventHandler(this.btnManual_Click);
            // 
            // btnCancelSwipe
            // 
            this.btnCancelSwipe.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btnCancelSwipe.BackgroundImage = global::POSClient.Properties.Resources.GlassyLook;
            this.btnCancelSwipe.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancelSwipe.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelSwipe.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancelSwipe.Location = new System.Drawing.Point(490, 273);
            this.btnCancelSwipe.Name = "btnCancelSwipe";
            this.btnCancelSwipe.Size = new System.Drawing.Size(90, 60);
            this.btnCancelSwipe.TabIndex = 2;
            this.btnCancelSwipe.Text = "Cancel";
            this.btnCancelSwipe.UseVisualStyleBackColor = false;
            this.btnCancelSwipe.Click += new System.EventHandler(this.cancel_Click);
            // 
            // pnlSwipe
            // 
            this.pnlSwipe.BackColor = System.Drawing.Color.Transparent;
            this.pnlSwipe.Controls.Add(this.panel1);
            this.pnlSwipe.Controls.Add(this.textBox1);
            this.pnlSwipe.Controls.Add(this.btnCancelSwipe);
            this.pnlSwipe.Controls.Add(this.btnManual);
            this.pnlSwipe.Location = new System.Drawing.Point(0, 0);
            this.pnlSwipe.Name = "pnlSwipe";
            this.pnlSwipe.Size = new System.Drawing.Size(593, 345);
            this.pnlSwipe.TabIndex = 3;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(352, 30);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 23);
            this.textBox1.TabIndex = 3;
            this.textBox1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyUp);
            // 
            // pnlManual
            // 
            this.pnlManual.BackColor = System.Drawing.Color.Transparent;
            this.pnlManual.Controls.Add(this.Astx8);
            this.pnlManual.Controls.Add(this.Astx2);
            this.pnlManual.Controls.Add(this.Astx4);
            this.pnlManual.Controls.Add(this.Astx1);
            this.pnlManual.Controls.Add(this.label2);
            this.pnlManual.Controls.Add(this.txtValidationResult);
            this.pnlManual.Controls.Add(this.pbLogo);
            this.pnlManual.Controls.Add(this.txtAmount);
            this.pnlManual.Controls.Add(this.label1);
            this.pnlManual.Controls.Add(this.oskbCreditCard);
            this.pnlManual.Controls.Add(this.val8);
            this.pnlManual.Controls.Add(this.val2);
            this.pnlManual.Controls.Add(this.val4);
            this.pnlManual.Controls.Add(this.val1);
            this.pnlManual.Controls.Add(this.lblCardHolderName);
            this.pnlManual.Controls.Add(this.lblExpiryDate);
            this.pnlManual.Controls.Add(this.lblCardNumber);
            this.pnlManual.Controls.Add(this.lblCardType);
            this.pnlManual.Controls.Add(this.btnCancel);
            this.pnlManual.Controls.Add(this.btnOk);
            this.pnlManual.Location = new System.Drawing.Point(0, 0);
            this.pnlManual.Name = "pnlManual";
            this.pnlManual.Size = new System.Drawing.Size(593, 345);
            this.pnlManual.TabIndex = 4;
            // 
            // Astx8
            // 
            this.Astx8.AutoSize = true;
            this.Astx8.Location = new System.Drawing.Point(518, 207);
            this.Astx8.Name = "Astx8";
            this.Astx8.Size = new System.Drawing.Size(17, 16);
            this.Astx8.TabIndex = 20;
            this.Astx8.Text = "*";
            this.Astx8.Visible = false;
            // 
            // Astx2
            // 
            this.Astx2.AutoSize = true;
            this.Astx2.Location = new System.Drawing.Point(485, 143);
            this.Astx2.Name = "Astx2";
            this.Astx2.Size = new System.Drawing.Size(17, 16);
            this.Astx2.TabIndex = 19;
            this.Astx2.Text = "*";
            this.Astx2.Visible = false;
            // 
            // Astx4
            // 
            this.Astx4.AutoSize = true;
            this.Astx4.Location = new System.Drawing.Point(430, 80);
            this.Astx4.Name = "Astx4";
            this.Astx4.Size = new System.Drawing.Size(17, 16);
            this.Astx4.TabIndex = 18;
            this.Astx4.Text = "*";
            this.Astx4.Visible = false;
            // 
            // Astx1
            // 
            this.Astx1.AutoSize = true;
            this.Astx1.Location = new System.Drawing.Point(402, 21);
            this.Astx1.Name = "Astx1";
            this.Astx1.Size = new System.Drawing.Size(17, 16);
            this.Astx1.TabIndex = 17;
            this.Astx1.Text = "*";
            this.Astx1.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(412, 207);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 16);
            this.label2.TabIndex = 16;
            this.label2.Text = "(MM/yy)";
            // 
            // txtValidationResult
            // 
            this.txtValidationResult.Location = new System.Drawing.Point(482, 199);
            this.txtValidationResult.Name = "txtValidationResult";
            this.txtValidationResult.Size = new System.Drawing.Size(100, 23);
            this.txtValidationResult.TabIndex = 15;
            this.txtValidationResult.Visible = false;
            // 
            // pbLogo
            // 
            this.pbLogo.Location = new System.Drawing.Point(482, 16);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new System.Drawing.Size(108, 79);
            this.pbLogo.TabIndex = 14;
            this.pbLogo.TabStop = false;
            // 
            // txtAmount
            // 
            this.txtAmount.Font = new System.Drawing.Font("Verdana", 12.75F);
            this.txtAmount.Location = new System.Drawing.Point(90, 17);
            this.txtAmount.MaxLength = 13;
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(204, 28);
            this.txtAmount.TabIndex = 13;
            this.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAmount_KeyPress);
            this.txtAmount.Enter += new System.EventHandler(this.textBox_Enter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 18);
            this.label1.TabIndex = 12;
            this.label1.Text = "Amount";
            // 
            // oskbCreditCard
            // 
            this.oskbCreditCard.ActionControlBackgroundImage = global::POSClient.Properties.Resources.GlassyLook;
            this.oskbCreditCard.ActionControlColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(227)))), ((int)(((byte)(195)))));
            this.oskbCreditCard.BackColor = System.Drawing.Color.Transparent;
            this.oskbCreditCard.CurrentFocus = null;
            this.oskbCreditCard.DataControlBackgroundImage = global::POSClient.Properties.Resources.GlassyLook;
            this.oskbCreditCard.DataControlColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.oskbCreditCard.Location = new System.Drawing.Point(0, 51);
            this.oskbCreditCard.Name = "oskbCreditCard";
            this.oskbCreditCard.Size = new System.Drawing.Size(295, 291);
            this.oskbCreditCard.TabIndex = 11;
            // 
            // val8
            // 
            this.val8.Font = new System.Drawing.Font("Verdana", 12.75F);
            this.val8.Location = new System.Drawing.Point(306, 234);
            this.val8.MaxLength = 5;
            this.val8.Name = "val8";
            this.val8.Size = new System.Drawing.Size(218, 28);
            this.val8.TabIndex = 10;
            this.val8.Enter += new System.EventHandler(this.textBox_Enter);
            // 
            // val2
            // 
            this.val2.Font = new System.Drawing.Font("Verdana", 12.75F);
            this.val2.Location = new System.Drawing.Point(306, 168);
            this.val2.MaxLength = 50;
            this.val2.Name = "val2";
            this.val2.Size = new System.Drawing.Size(218, 28);
            this.val2.TabIndex = 9;
            this.val2.Enter += new System.EventHandler(this.textBox_Enter);
            // 
            // val4
            // 
            this.val4.Font = new System.Drawing.Font("Verdana", 12.75F);
            this.val4.Location = new System.Drawing.Point(306, 103);
            this.val4.MaxLength = 16;
            this.val4.Name = "val4";
            this.val4.Size = new System.Drawing.Size(218, 28);
            this.val4.TabIndex = 8;
            this.val4.TextChanged += new System.EventHandler(this.val4_TextChanged);
            this.val4.Enter += new System.EventHandler(this.textBox_Enter);
            // 
            // val1
            // 
            this.val1.Font = new System.Drawing.Font("Verdana", 12.75F);
            this.val1.Location = new System.Drawing.Point(307, 44);
            this.val1.MaxLength = 50;
            this.val1.Name = "val1";
            this.val1.Size = new System.Drawing.Size(172, 28);
            this.val1.TabIndex = 7;
            this.val1.Enter += new System.EventHandler(this.textBox_Enter);
            // 
            // lblCardHolderName
            // 
            this.lblCardHolderName.AutoSize = true;
            this.lblCardHolderName.Location = new System.Drawing.Point(305, 141);
            this.lblCardHolderName.Name = "lblCardHolderName";
            this.lblCardHolderName.Size = new System.Drawing.Size(152, 16);
            this.lblCardHolderName.TabIndex = 6;
            this.lblCardHolderName.Text = "Card Holder\'s Name";
            // 
            // lblExpiryDate
            // 
            this.lblExpiryDate.AutoSize = true;
            this.lblExpiryDate.Location = new System.Drawing.Point(305, 207);
            this.lblExpiryDate.Name = "lblExpiryDate";
            this.lblExpiryDate.Size = new System.Drawing.Size(92, 16);
            this.lblExpiryDate.TabIndex = 5;
            this.lblExpiryDate.Text = "Expiry Date";
            // 
            // lblCardNumber
            // 
            this.lblCardNumber.AutoSize = true;
            this.lblCardNumber.Location = new System.Drawing.Point(306, 80);
            this.lblCardNumber.Name = "lblCardNumber";
            this.lblCardNumber.Size = new System.Drawing.Size(103, 16);
            this.lblCardNumber.TabIndex = 4;
            this.lblCardNumber.Text = "Card Number";
            // 
            // lblCardType
            // 
            this.lblCardType.AutoSize = true;
            this.lblCardType.Location = new System.Drawing.Point(306, 20);
            this.lblCardType.Name = "lblCardType";
            this.lblCardType.Size = new System.Drawing.Size(81, 16);
            this.lblCardType.TabIndex = 3;
            this.lblCardType.Text = "Card Type";
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btnCancel.BackgroundImage = global::POSClient.Properties.Resources.GlassyLook;
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.Location = new System.Drawing.Point(455, 270);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 62);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.TabStop = false;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(219)))), ((int)(((byte)(192)))));
            this.btnOk.BackgroundImage = global::POSClient.Properties.Resources.GlassyLook;
            this.btnOk.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnOk.Location = new System.Drawing.Point(377, 270);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(72, 62);
            this.btnOk.TabIndex = 1;
            this.btnOk.TabStop = false;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // CreditCard
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.btnCancelSwipe;
            this.ClientSize = new System.Drawing.Size(592, 341);
            this.Controls.Add(this.pnlManual);
            this.Controls.Add(this.pnlSwipe);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "CreditCard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Credit Card Information";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.CreditCardInformation_Paint);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlSwipe.ResumeLayout(false);
            this.pnlSwipe.PerformLayout();
            this.pnlManual.ResumeLayout(false);
            this.pnlManual.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblCardMessage2;
        private System.Windows.Forms.Label lblCardMessage;
        private System.Windows.Forms.Button btnManual;
        private System.Windows.Forms.Button btnCancelSwipe;
        private System.Windows.Forms.Panel pnlSwipe;
        private System.Windows.Forms.Panel pnlManual;
        private System.Windows.Forms.Label lblCardHolderName;
        private System.Windows.Forms.Label lblExpiryDate;
        private System.Windows.Forms.Label lblCardNumber;
        private System.Windows.Forms.Label lblCardType;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.TextBox val8;
        private System.Windows.Forms.TextBox val2;
        private System.Windows.Forms.TextBox val4;
        private System.Windows.Forms.TextBox val1;
        private POSClient.UI.Controls.OnScreenKeyBoard oskbCreditCard;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.PictureBox pbLogo;
        private System.Windows.Forms.TextBox txtValidationResult;
        private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label Astx8;
		private System.Windows.Forms.Label Astx2;
		private System.Windows.Forms.Label Astx4;
		private System.Windows.Forms.Label Astx1;
    }
}