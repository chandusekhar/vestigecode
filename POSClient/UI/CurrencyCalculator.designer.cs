namespace POSClient.UI
{
	partial class CurrencyCalculator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CurrencyCalculator));
            this.oskbCurrCalc = new POSClient.UI.Controls.OnScreenKeyBoard();
            this.txtCurrFrom = new System.Windows.Forms.TextBox();
            this.txtCurrFromVal = new System.Windows.Forms.TextBox();
            this.txtExRateFromBase = new System.Windows.Forms.TextBox();
            this.lblAt01 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblCurrFromCap = new System.Windows.Forms.Label();
            this.lblExRateToBase01 = new System.Windows.Forms.Label();
            this.lblExRateToBase02 = new System.Windows.Forms.Label();
            this.lblCurrToCap = new System.Windows.Forms.Label();
            this.lblAt02 = new System.Windows.Forms.Label();
            this.txtExRateToBase = new System.Windows.Forms.TextBox();
            this.txtCurrToVal = new System.Windows.Forms.TextBox();
            this.txtCurrTo = new System.Windows.Forms.TextBox();
            this.isCurrFrom = new POSClient.UI.Controls.ItemSelector();
            this.isCurrTo = new POSClient.UI.Controls.ItemSelector();
            this.btnOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // oskbCurrCalc
            // 
            this.oskbCurrCalc.ActionControlBackgroundImage = global::POSClient.Properties.Resources.GlassyLook;
            this.oskbCurrCalc.ActionControlColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(227)))), ((int)(((byte)(195)))));
            this.oskbCurrCalc.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.oskbCurrCalc.CurrentFocus = null;
            this.oskbCurrCalc.DataControlBackgroundImage = global::POSClient.Properties.Resources.GlassyLook;
            this.oskbCurrCalc.DataControlColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.oskbCurrCalc.Location = new System.Drawing.Point(574, 5);
            this.oskbCurrCalc.Name = "oskbCurrCalc";
            this.oskbCurrCalc.Size = new System.Drawing.Size(295, 289);
            this.oskbCurrCalc.TabIndex = 0;
            this.oskbCurrCalc.TabStop = false;
            // 
            // txtCurrFrom
            // 
            this.txtCurrFrom.Location = new System.Drawing.Point(513, 317);
            this.txtCurrFrom.Name = "txtCurrFrom";
            this.txtCurrFrom.ReadOnly = true;
            this.txtCurrFrom.Size = new System.Drawing.Size(50, 23);
            this.txtCurrFrom.TabIndex = 1;
            this.txtCurrFrom.TabStop = false;
            // 
            // txtCurrFromVal
            // 
            this.txtCurrFromVal.BackColor = System.Drawing.SystemColors.Window;
            this.txtCurrFromVal.Location = new System.Drawing.Point(569, 317);
            this.txtCurrFromVal.MaxLength = 13;
            this.txtCurrFromVal.Name = "txtCurrFromVal";
            this.txtCurrFromVal.ReadOnly = true;
            this.txtCurrFromVal.Size = new System.Drawing.Size(111, 23);
            this.txtCurrFromVal.TabIndex = 0;
            this.txtCurrFromVal.TabStop = false;
            this.txtCurrFromVal.Validated += new System.EventHandler(this.TextBox_Validated);
            this.txtCurrFromVal.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox_KeyDown);
            // 
            // txtExRateFromBase
            // 
            this.txtExRateFromBase.Location = new System.Drawing.Point(718, 317);
            this.txtExRateFromBase.Name = "txtExRateFromBase";
            this.txtExRateFromBase.ReadOnly = true;
            this.txtExRateFromBase.Size = new System.Drawing.Size(151, 23);
            this.txtExRateFromBase.TabIndex = 3;
            this.txtExRateFromBase.TabStop = false;
            // 
            // lblAt01
            // 
            this.lblAt01.AutoSize = true;
            this.lblAt01.BackColor = System.Drawing.Color.Transparent;
            this.lblAt01.Location = new System.Drawing.Point(686, 320);
            this.lblAt01.Name = "lblAt01";
            this.lblAt01.Size = new System.Drawing.Size(21, 16);
            this.lblAt01.TabIndex = 4;
            this.lblAt01.Text = "@";
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btnCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.BackgroundImage")));
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.Location = new System.Drawing.Point(794, 452);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 62);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.TabStop = false;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.DialogButton_Click);
            // 
            // lblCurrFromCap
            // 
            this.lblCurrFromCap.AutoSize = true;
            this.lblCurrFromCap.BackColor = System.Drawing.Color.Transparent;
            this.lblCurrFromCap.Font = new System.Drawing.Font("Verdana", 9F);
            this.lblCurrFromCap.Location = new System.Drawing.Point(566, 348);
            this.lblCurrFromCap.Name = "lblCurrFromCap";
            this.lblCurrFromCap.Size = new System.Drawing.Size(92, 14);
            this.lblCurrFromCap.TabIndex = 10;
            this.lblCurrFromCap.Text = "Convert From";
            // 
            // lblExRateToBase01
            // 
            this.lblExRateToBase01.AutoSize = true;
            this.lblExRateToBase01.BackColor = System.Drawing.Color.Transparent;
            this.lblExRateToBase01.Font = new System.Drawing.Font("Verdana", 9F);
            this.lblExRateToBase01.Location = new System.Drawing.Point(714, 348);
            this.lblExRateToBase01.Name = "lblExRateToBase01";
            this.lblExRateToBase01.Size = new System.Drawing.Size(155, 14);
            this.lblExRateToBase01.TabIndex = 12;
            this.lblExRateToBase01.Text = "Exchange Rate To Base";
            // 
            // lblExRateToBase02
            // 
            this.lblExRateToBase02.AutoSize = true;
            this.lblExRateToBase02.BackColor = System.Drawing.Color.Transparent;
            this.lblExRateToBase02.Font = new System.Drawing.Font("Verdana", 9F);
            this.lblExRateToBase02.Location = new System.Drawing.Point(714, 414);
            this.lblExRateToBase02.Name = "lblExRateToBase02";
            this.lblExRateToBase02.Size = new System.Drawing.Size(155, 14);
            this.lblExRateToBase02.TabIndex = 18;
            this.lblExRateToBase02.Text = "Exchange Rate To Base";
            // 
            // lblCurrToCap
            // 
            this.lblCurrToCap.AutoSize = true;
            this.lblCurrToCap.BackColor = System.Drawing.Color.Transparent;
            this.lblCurrToCap.Font = new System.Drawing.Font("Verdana", 9F);
            this.lblCurrToCap.Location = new System.Drawing.Point(566, 414);
            this.lblCurrToCap.Name = "lblCurrToCap";
            this.lblCurrToCap.Size = new System.Drawing.Size(76, 14);
            this.lblCurrToCap.TabIndex = 17;
            this.lblCurrToCap.Text = "Convert To";
            // 
            // lblAt02
            // 
            this.lblAt02.AutoSize = true;
            this.lblAt02.BackColor = System.Drawing.Color.Transparent;
            this.lblAt02.Location = new System.Drawing.Point(686, 386);
            this.lblAt02.Name = "lblAt02";
            this.lblAt02.Size = new System.Drawing.Size(21, 16);
            this.lblAt02.TabIndex = 16;
            this.lblAt02.Text = "@";
            // 
            // txtExRateToBase
            // 
            this.txtExRateToBase.Location = new System.Drawing.Point(718, 383);
            this.txtExRateToBase.Name = "txtExRateToBase";
            this.txtExRateToBase.ReadOnly = true;
            this.txtExRateToBase.Size = new System.Drawing.Size(151, 23);
            this.txtExRateToBase.TabIndex = 15;
            this.txtExRateToBase.TabStop = false;
            // 
            // txtCurrToVal
            // 
            this.txtCurrToVal.Location = new System.Drawing.Point(569, 383);
            this.txtCurrToVal.Name = "txtCurrToVal";
            this.txtCurrToVal.ReadOnly = true;
            this.txtCurrToVal.Size = new System.Drawing.Size(111, 23);
            this.txtCurrToVal.TabIndex = 14;
            this.txtCurrToVal.TabStop = false;
            // 
            // txtCurrTo
            // 
            this.txtCurrTo.Location = new System.Drawing.Point(513, 383);
            this.txtCurrTo.Name = "txtCurrTo";
            this.txtCurrTo.ReadOnly = true;
            this.txtCurrTo.Size = new System.Drawing.Size(50, 23);
            this.txtCurrTo.TabIndex = 13;
            this.txtCurrTo.TabStop = false;
            // 
            // isCurrFrom
            // 
            this.isCurrFrom.ActionControlBackgroundImage = ((System.Drawing.Image)(resources.GetObject("isCurrFrom.ActionControlBackgroundImage")));
            this.isCurrFrom.ActionControlColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(227)))), ((int)(((byte)(195)))));
            this.isCurrFrom.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.isCurrFrom.BackColor = System.Drawing.Color.Silver;
            this.isCurrFrom.ColumnWidth = 75F;
            this.isCurrFrom.ItemControlBackgroundImage = ((System.Drawing.Image)(resources.GetObject("isCurrFrom.ItemControlBackgroundImage")));
            this.isCurrFrom.ItemControlColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.isCurrFrom.ItemIDField = "CurrencyCode";
            this.isCurrFrom.ItemTextField = "CurrencyCode";
            this.isCurrFrom.Location = new System.Drawing.Point(9, 5);
            this.isCurrFrom.Margin = new System.Windows.Forms.Padding(0);
            this.isCurrFrom.Name = "isCurrFrom";
            this.isCurrFrom.RowHeight = 75F;
            this.isCurrFrom.Size = new System.Drawing.Size(464, 233);
            this.isCurrFrom.TabIndex = 21;
            this.isCurrFrom.TabStop = false;
            this.isCurrFrom.ItemSelected += new POSClient.UI.Controls.ItemSelectedEventHandler(this.Currency_ItemSelected);
            // 
            // isCurrTo
            // 
            this.isCurrTo.ActionControlBackgroundImage = ((System.Drawing.Image)(resources.GetObject("isCurrTo.ActionControlBackgroundImage")));
            this.isCurrTo.ActionControlColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(227)))), ((int)(((byte)(195)))));
            this.isCurrTo.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.isCurrTo.BackColor = System.Drawing.Color.Silver;
            this.isCurrTo.ColumnWidth = 75F;
            this.isCurrTo.ItemControlBackgroundImage = ((System.Drawing.Image)(resources.GetObject("isCurrTo.ItemControlBackgroundImage")));
            this.isCurrTo.ItemControlColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.isCurrTo.ItemIDField = "CurrencyCode";
            this.isCurrTo.ItemTextField = "CurrencyCode";
            this.isCurrTo.Location = new System.Drawing.Point(9, 283);
            this.isCurrTo.Margin = new System.Windows.Forms.Padding(0);
            this.isCurrTo.Name = "isCurrTo";
            this.isCurrTo.RowHeight = 75F;
            this.isCurrTo.Size = new System.Drawing.Size(464, 233);
            this.isCurrTo.TabIndex = 22;
            this.isCurrTo.TabStop = false;
            this.isCurrTo.ItemSelected += new POSClient.UI.Controls.ItemSelectedEventHandler(this.Currency_ItemSelected);
            // 
            // btnOk
            // 
            this.btnOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(219)))), ((int)(((byte)(192)))));
            this.btnOk.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOk.BackgroundImage")));
            this.btnOk.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnOk.Location = new System.Drawing.Point(713, 453);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(72, 62);
            this.btnOk.TabIndex = 23;
            this.btnOk.TabStop = false;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Visible = false;
            this.btnOk.Click += new System.EventHandler(this.DialogButton_Click);
            // 
            // CurrencyCalculator
            // 
            this.AcceptButton = this.btnOk;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(879, 524);
            this.ControlBox = true;
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.isCurrTo);
            this.Controls.Add(this.isCurrFrom);
            this.Controls.Add(this.lblExRateToBase02);
            this.Controls.Add(this.lblCurrToCap);
            this.Controls.Add(this.lblAt02);
            this.Controls.Add(this.txtExRateToBase);
            this.Controls.Add(this.txtCurrToVal);
            this.Controls.Add(this.txtCurrTo);
            this.Controls.Add(this.lblExRateToBase01);
            this.Controls.Add(this.lblCurrFromCap);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblAt01);
            this.Controls.Add(this.txtExRateFromBase);
            this.Controls.Add(this.txtCurrFromVal);
            this.Controls.Add(this.txtCurrFrom);
            this.Controls.Add(this.oskbCurrCalc);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "CurrencyCalculator";
            this.Text = "Currency Calculator";
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private POSClient.UI.Controls.OnScreenKeyBoard oskbCurrCalc;
		private System.Windows.Forms.TextBox txtCurrFrom;
		private System.Windows.Forms.TextBox txtCurrFromVal;
		private System.Windows.Forms.TextBox txtExRateFromBase;
		private System.Windows.Forms.Label lblAt01;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Label lblCurrFromCap;
		private System.Windows.Forms.Label lblExRateToBase01;
		private System.Windows.Forms.Label lblExRateToBase02;
		private System.Windows.Forms.Label lblCurrToCap;
		private System.Windows.Forms.Label lblAt02;
		private System.Windows.Forms.TextBox txtExRateToBase;
		private System.Windows.Forms.TextBox txtCurrToVal;
		private System.Windows.Forms.TextBox txtCurrTo;
		private POSClient.UI.Controls.ItemSelector isCurrFrom;
		private POSClient.UI.Controls.ItemSelector isCurrTo;
        private System.Windows.Forms.Button btnOk;
	}
}
