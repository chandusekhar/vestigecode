namespace POSClient.UI
{
	partial class Multiplier
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Multiplier));
            this.oskNumPad = new POSClient.UI.Controls.OnScreenKeyBoard();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.txtQuantity = new System.Windows.Forms.TextBox();
            this.lblNewDiscount = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // oskNumPad
            // 
            this.oskNumPad.ActionControlBackgroundImage = global::POSClient.Properties.Resources.GlassyLook;
            this.oskNumPad.ActionControlColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(227)))), ((int)(((byte)(195)))));
            this.oskNumPad.BackColor = System.Drawing.Color.Transparent;
            this.oskNumPad.CurrentFocus = null;
            this.oskNumPad.DataControlBackgroundImage = global::POSClient.Properties.Resources.GlassyLook;
            this.oskNumPad.DataControlColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.oskNumPad.Location = new System.Drawing.Point(201, 9);
            this.oskNumPad.Name = "oskNumPad";
            this.oskNumPad.Size = new System.Drawing.Size(295, 289);
            this.oskNumPad.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btnCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.BackgroundImage")));
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.Location = new System.Drawing.Point(104, 146);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 62);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.TabStop = false;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(219)))), ((int)(((byte)(192)))));
            this.btnOk.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOk.BackgroundImage")));
            this.btnOk.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnOk.Location = new System.Drawing.Point(16, 146);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(72, 62);
            this.btnOk.TabIndex = 10;
            this.btnOk.TabStop = false;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // txtQuantity
            // 
            this.txtQuantity.Font = new System.Drawing.Font("Verdana", 10.75F, System.Drawing.FontStyle.Bold);
            this.txtQuantity.Location = new System.Drawing.Point(104, 77);
            this.txtQuantity.MaxLength = 3;
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(75, 25);
            this.txtQuantity.TabIndex = 9;
            this.txtQuantity.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Quantity_KeyPress);
            // 
            // lblNewDiscount
            // 
            this.lblNewDiscount.AutoSize = true;
            this.lblNewDiscount.BackColor = System.Drawing.Color.Transparent;
            this.lblNewDiscount.Location = new System.Drawing.Point(20, 80);
            this.lblNewDiscount.Name = "lblNewDiscount";
            this.lblNewDiscount.Size = new System.Drawing.Size(71, 16);
            this.lblNewDiscount.TabIndex = 8;
            this.lblNewDiscount.Text = "Quantity";
            // 
            // Multiplier
            // 
            this.AcceptButton = this.btnOk;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(508, 310);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.txtQuantity);
            this.Controls.Add(this.lblNewDiscount);
            this.Controls.Add(this.oskNumPad);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Multiplier";
            this.Text = "Quantity";
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private POSClient.UI.Controls.OnScreenKeyBoard oskNumPad;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.TextBox txtQuantity;
		private System.Windows.Forms.Label lblNewDiscount;

	}
}

