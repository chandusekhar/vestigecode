namespace POSClient.UI
{
    partial class DeliveryAdress
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeliveryAdress));
            this.cmbAddressType = new System.Windows.Forms.ComboBox();
            this.lblAddress = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.pnlAddress = new System.Windows.Forms.Panel();
            this.txtContactWebsite = new System.Windows.Forms.TextBox();
            this.lblContactWebsite = new System.Windows.Forms.Label();
            this.txtContactFax2 = new System.Windows.Forms.TextBox();
            this.lblContactFax2 = new System.Windows.Forms.Label();
            this.lblContactEmail2 = new System.Windows.Forms.Label();
            this.txtContactEmail2 = new System.Windows.Forms.TextBox();
            this.lblContactMobile2 = new System.Windows.Forms.Label();
            this.txtContactMobile2 = new System.Windows.Forms.TextBox();
            this.lblContactPhone1 = new System.Windows.Forms.Label();
            this.txtContactPhone1 = new System.Windows.Forms.TextBox();
            this.txtContactAddress4 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtContactAddress1 = new System.Windows.Forms.TextBox();
            this.cmbContactCity = new System.Windows.Forms.ComboBox();
            this.txtContactPinCode = new System.Windows.Forms.TextBox();
            this.lblContactPinCode = new System.Windows.Forms.Label();
            this.lblContactMobile1 = new System.Windows.Forms.Label();
            this.lblContactCity = new System.Windows.Forms.Label();
            this.lblContactEmail1 = new System.Windows.Forms.Label();
            this.txtContactFax1 = new System.Windows.Forms.TextBox();
            this.cmbContactCountry = new System.Windows.Forms.ComboBox();
            this.txtContactMobile1 = new System.Windows.Forms.TextBox();
            this.cmbContactState = new System.Windows.Forms.ComboBox();
            this.lblContactPhone2 = new System.Windows.Forms.Label();
            this.lblContactState = new System.Windows.Forms.Label();
            this.txtContactEmail1 = new System.Windows.Forms.TextBox();
            this.txtContactAddress3 = new System.Windows.Forms.TextBox();
            this.lblContactFax1 = new System.Windows.Forms.Label();
            this.lblContactCountry = new System.Windows.Forms.Label();
            this.txtContactPhone2 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.lblContactAddress1 = new System.Windows.Forms.Label();
            this.txtContactAddress2 = new System.Windows.Forms.TextBox();
            this.lblContactAddress2 = new System.Windows.Forms.Label();
            this.cmbPickUpCenter = new System.Windows.Forms.ComboBox();
            this.lblPC = new System.Windows.Forms.Label();
            this.errorAdd = new System.Windows.Forms.ErrorProvider(this.components);
            this.lblDistributor = new System.Windows.Forms.Label();
            this.txtDistributorId = new System.Windows.Forms.TextBox();
            this.pnlAddress.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorAdd)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbAddressType
            // 
            this.cmbAddressType.FormattingEnabled = true;
            this.cmbAddressType.Location = new System.Drawing.Point(93, 12);
            this.cmbAddressType.Name = "cmbAddressType";
            this.cmbAddressType.Size = new System.Drawing.Size(111, 24);
            this.cmbAddressType.TabIndex = 0;
            this.cmbAddressType.SelectedIndexChanged += new System.EventHandler(this.cmbAddressType_SelectedIndexChanged);
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.BackColor = System.Drawing.Color.Transparent;
            this.lblAddress.Location = new System.Drawing.Point(6, 15);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(87, 16);
            this.lblAddress.TabIndex = 1;
            this.lblAddress.Text = "Courier To:";
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btnCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.BackgroundImage")));
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.Location = new System.Drawing.Point(718, 296);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 62);
            this.btnCancel.TabIndex = 5;
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
            this.btnOk.Location = new System.Drawing.Point(630, 296);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(72, 62);
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // pnlAddress
            // 
            this.pnlAddress.BackColor = System.Drawing.Color.Transparent;
            this.pnlAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlAddress.Controls.Add(this.txtContactWebsite);
            this.pnlAddress.Controls.Add(this.lblContactWebsite);
            this.pnlAddress.Controls.Add(this.txtContactFax2);
            this.pnlAddress.Controls.Add(this.lblContactFax2);
            this.pnlAddress.Controls.Add(this.lblContactEmail2);
            this.pnlAddress.Controls.Add(this.txtContactEmail2);
            this.pnlAddress.Controls.Add(this.lblContactMobile2);
            this.pnlAddress.Controls.Add(this.txtContactMobile2);
            this.pnlAddress.Controls.Add(this.lblContactPhone1);
            this.pnlAddress.Controls.Add(this.txtContactPhone1);
            this.pnlAddress.Controls.Add(this.txtContactAddress4);
            this.pnlAddress.Controls.Add(this.label1);
            this.pnlAddress.Controls.Add(this.txtContactAddress1);
            this.pnlAddress.Controls.Add(this.cmbContactCity);
            this.pnlAddress.Controls.Add(this.txtContactPinCode);
            this.pnlAddress.Controls.Add(this.lblContactPinCode);
            this.pnlAddress.Controls.Add(this.lblContactMobile1);
            this.pnlAddress.Controls.Add(this.lblContactCity);
            this.pnlAddress.Controls.Add(this.lblContactEmail1);
            this.pnlAddress.Controls.Add(this.txtContactFax1);
            this.pnlAddress.Controls.Add(this.cmbContactCountry);
            this.pnlAddress.Controls.Add(this.txtContactMobile1);
            this.pnlAddress.Controls.Add(this.cmbContactState);
            this.pnlAddress.Controls.Add(this.lblContactPhone2);
            this.pnlAddress.Controls.Add(this.lblContactState);
            this.pnlAddress.Controls.Add(this.txtContactEmail1);
            this.pnlAddress.Controls.Add(this.txtContactAddress3);
            this.pnlAddress.Controls.Add(this.lblContactFax1);
            this.pnlAddress.Controls.Add(this.lblContactCountry);
            this.pnlAddress.Controls.Add(this.txtContactPhone2);
            this.pnlAddress.Controls.Add(this.label7);
            this.pnlAddress.Controls.Add(this.lblContactAddress1);
            this.pnlAddress.Controls.Add(this.txtContactAddress2);
            this.pnlAddress.Controls.Add(this.lblContactAddress2);
            this.pnlAddress.Enabled = false;
            this.pnlAddress.Location = new System.Drawing.Point(3, 42);
            this.pnlAddress.Name = "pnlAddress";
            this.pnlAddress.Size = new System.Drawing.Size(796, 246);
            this.pnlAddress.TabIndex = 3;
            // 
            // txtContactWebsite
            // 
            this.txtContactWebsite.Location = new System.Drawing.Point(626, 208);
            this.txtContactWebsite.MaxLength = 20;
            this.txtContactWebsite.Name = "txtContactWebsite";
            this.txtContactWebsite.Size = new System.Drawing.Size(133, 23);
            this.txtContactWebsite.TabIndex = 16;
            // 
            // lblContactWebsite
            // 
            this.lblContactWebsite.AutoSize = true;
            this.lblContactWebsite.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContactWebsite.Location = new System.Drawing.Point(553, 213);
            this.lblContactWebsite.Name = "lblContactWebsite";
            this.lblContactWebsite.Size = new System.Drawing.Size(63, 13);
            this.lblContactWebsite.TabIndex = 182;
            this.lblContactWebsite.Text = "Website:";
            this.lblContactWebsite.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtContactFax2
            // 
            this.txtContactFax2.Location = new System.Drawing.Point(377, 210);
            this.txtContactFax2.MaxLength = 20;
            this.txtContactFax2.Name = "txtContactFax2";
            this.txtContactFax2.Size = new System.Drawing.Size(132, 23);
            this.txtContactFax2.TabIndex = 15;
            // 
            // lblContactFax2
            // 
            this.lblContactFax2.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContactFax2.Location = new System.Drawing.Point(306, 215);
            this.lblContactFax2.Name = "lblContactFax2";
            this.lblContactFax2.Size = new System.Drawing.Size(69, 13);
            this.lblContactFax2.TabIndex = 180;
            this.lblContactFax2.Text = "Fax 2:";
            this.lblContactFax2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblContactEmail2
            // 
            this.lblContactEmail2.AutoSize = true;
            this.lblContactEmail2.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContactEmail2.Location = new System.Drawing.Point(555, 184);
            this.lblContactEmail2.Name = "lblContactEmail2";
            this.lblContactEmail2.Size = new System.Drawing.Size(59, 13);
            this.lblContactEmail2.TabIndex = 178;
            this.lblContactEmail2.Text = "Email 2:";
            this.lblContactEmail2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtContactEmail2
            // 
            this.txtContactEmail2.Location = new System.Drawing.Point(626, 179);
            this.txtContactEmail2.MaxLength = 50;
            this.txtContactEmail2.Name = "txtContactEmail2";
            this.txtContactEmail2.Size = new System.Drawing.Size(133, 23);
            this.txtContactEmail2.TabIndex = 13;
            // 
            // lblContactMobile2
            // 
            this.lblContactMobile2.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContactMobile2.Location = new System.Drawing.Point(3, 181);
            this.lblContactMobile2.Name = "lblContactMobile2";
            this.lblContactMobile2.Size = new System.Drawing.Size(120, 13);
            this.lblContactMobile2.TabIndex = 176;
            this.lblContactMobile2.Text = "Mobile 2:";
            this.lblContactMobile2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtContactMobile2
            // 
            this.txtContactMobile2.Location = new System.Drawing.Point(129, 182);
            this.txtContactMobile2.MaxLength = 20;
            this.txtContactMobile2.Name = "txtContactMobile2";
            this.txtContactMobile2.Size = new System.Drawing.Size(128, 23);
            this.txtContactMobile2.TabIndex = 11;
            // 
            // lblContactPhone1
            // 
            this.lblContactPhone1.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContactPhone1.Location = new System.Drawing.Point(3, 152);
            this.lblContactPhone1.Name = "lblContactPhone1";
            this.lblContactPhone1.Size = new System.Drawing.Size(120, 13);
            this.lblContactPhone1.TabIndex = 174;
            this.lblContactPhone1.Text = "Phone1:";
            this.lblContactPhone1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtContactPhone1
            // 
            this.txtContactPhone1.Location = new System.Drawing.Point(129, 153);
            this.txtContactPhone1.MaxLength = 20;
            this.txtContactPhone1.Name = "txtContactPhone1";
            this.txtContactPhone1.Size = new System.Drawing.Size(128, 23);
            this.txtContactPhone1.TabIndex = 8;
            // 
            // txtContactAddress4
            // 
            this.txtContactAddress4.Location = new System.Drawing.Point(129, 84);
            this.txtContactAddress4.MaxLength = 100;
            this.txtContactAddress4.Multiline = true;
            this.txtContactAddress4.Name = "txtContactAddress4";
            this.txtContactAddress4.Size = new System.Drawing.Size(128, 63);
            this.txtContactAddress4.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 13);
            this.label1.TabIndex = 172;
            this.label1.Text = "Address Line4:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtContactAddress1
            // 
            this.txtContactAddress1.Location = new System.Drawing.Point(128, 13);
            this.txtContactAddress1.MaxLength = 100;
            this.txtContactAddress1.Multiline = true;
            this.txtContactAddress1.Name = "txtContactAddress1";
            this.txtContactAddress1.Size = new System.Drawing.Size(128, 63);
            this.txtContactAddress1.TabIndex = 0;
            // 
            // cmbContactCity
            // 
            this.cmbContactCity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbContactCity.FormattingEnabled = true;
            this.cmbContactCity.Location = new System.Drawing.Point(377, 114);
            this.cmbContactCity.Name = "cmbContactCity";
            this.cmbContactCity.Size = new System.Drawing.Size(132, 24);
            this.cmbContactCity.TabIndex = 6;
            // 
            // txtContactPinCode
            // 
            this.txtContactPinCode.Location = new System.Drawing.Point(626, 117);
            this.txtContactPinCode.MaxLength = 6;
            this.txtContactPinCode.Name = "txtContactPinCode";
            this.txtContactPinCode.Size = new System.Drawing.Size(133, 23);
            this.txtContactPinCode.TabIndex = 7;
            // 
            // lblContactPinCode
            // 
            this.lblContactPinCode.AutoSize = true;
            this.lblContactPinCode.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContactPinCode.Location = new System.Drawing.Point(549, 119);
            this.lblContactPinCode.Name = "lblContactPinCode";
            this.lblContactPinCode.Size = new System.Drawing.Size(67, 13);
            this.lblContactPinCode.TabIndex = 169;
            this.lblContactPinCode.Text = "Pin Code:";
            this.lblContactPinCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblContactMobile1
            // 
            this.lblContactMobile1.AutoSize = true;
            this.lblContactMobile1.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContactMobile1.Location = new System.Drawing.Point(551, 155);
            this.lblContactMobile1.Name = "lblContactMobile1";
            this.lblContactMobile1.Size = new System.Drawing.Size(65, 13);
            this.lblContactMobile1.TabIndex = 156;
            this.lblContactMobile1.Text = "Mobile 1:";
            this.lblContactMobile1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblContactCity
            // 
            this.lblContactCity.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContactCity.Location = new System.Drawing.Point(275, 119);
            this.lblContactCity.Name = "lblContactCity";
            this.lblContactCity.Size = new System.Drawing.Size(100, 13);
            this.lblContactCity.TabIndex = 168;
            this.lblContactCity.Text = "City:*";
            this.lblContactCity.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblContactEmail1
            // 
            this.lblContactEmail1.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContactEmail1.Location = new System.Drawing.Point(273, 186);
            this.lblContactEmail1.Name = "lblContactEmail1";
            this.lblContactEmail1.Size = new System.Drawing.Size(102, 13);
            this.lblContactEmail1.TabIndex = 161;
            this.lblContactEmail1.Text = "Email 1:";
            this.lblContactEmail1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtContactFax1
            // 
            this.txtContactFax1.Location = new System.Drawing.Point(129, 211);
            this.txtContactFax1.MaxLength = 20;
            this.txtContactFax1.Name = "txtContactFax1";
            this.txtContactFax1.Size = new System.Drawing.Size(128, 23);
            this.txtContactFax1.TabIndex = 14;
            // 
            // cmbContactCountry
            // 
            this.cmbContactCountry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbContactCountry.FormattingEnabled = true;
            this.cmbContactCountry.Location = new System.Drawing.Point(377, 84);
            this.cmbContactCountry.Name = "cmbContactCountry";
            this.cmbContactCountry.Size = new System.Drawing.Size(132, 24);
            this.cmbContactCountry.TabIndex = 4;
            this.cmbContactCountry.SelectedIndexChanged += new System.EventHandler(this.cmbContactCountry_SelectedIndexChanged);
            // 
            // txtContactMobile1
            // 
            this.txtContactMobile1.Location = new System.Drawing.Point(626, 150);
            this.txtContactMobile1.MaxLength = 20;
            this.txtContactMobile1.Name = "txtContactMobile1";
            this.txtContactMobile1.Size = new System.Drawing.Size(133, 23);
            this.txtContactMobile1.TabIndex = 10;
            // 
            // cmbContactState
            // 
            this.cmbContactState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbContactState.FormattingEnabled = true;
            this.cmbContactState.Location = new System.Drawing.Point(626, 82);
            this.cmbContactState.Name = "cmbContactState";
            this.cmbContactState.Size = new System.Drawing.Size(133, 24);
            this.cmbContactState.TabIndex = 5;
            this.cmbContactState.SelectedIndexChanged += new System.EventHandler(this.cmbContactState_SelectedIndexChanged);
            // 
            // lblContactPhone2
            // 
            this.lblContactPhone2.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContactPhone2.Location = new System.Drawing.Point(275, 154);
            this.lblContactPhone2.Name = "lblContactPhone2";
            this.lblContactPhone2.Size = new System.Drawing.Size(100, 13);
            this.lblContactPhone2.TabIndex = 160;
            this.lblContactPhone2.Text = "Phone 2:";
            this.lblContactPhone2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblContactState
            // 
            this.lblContactState.AutoSize = true;
            this.lblContactState.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContactState.Location = new System.Drawing.Point(570, 88);
            this.lblContactState.Name = "lblContactState";
            this.lblContactState.Size = new System.Drawing.Size(53, 13);
            this.lblContactState.TabIndex = 167;
            this.lblContactState.Text = "State:*";
            this.lblContactState.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtContactEmail1
            // 
            this.txtContactEmail1.Location = new System.Drawing.Point(377, 181);
            this.txtContactEmail1.MaxLength = 50;
            this.txtContactEmail1.Name = "txtContactEmail1";
            this.txtContactEmail1.Size = new System.Drawing.Size(132, 23);
            this.txtContactEmail1.TabIndex = 12;
            // 
            // txtContactAddress3
            // 
            this.txtContactAddress3.Location = new System.Drawing.Point(626, 12);
            this.txtContactAddress3.MaxLength = 100;
            this.txtContactAddress3.Multiline = true;
            this.txtContactAddress3.Name = "txtContactAddress3";
            this.txtContactAddress3.Size = new System.Drawing.Size(133, 63);
            this.txtContactAddress3.TabIndex = 2;
            // 
            // lblContactFax1
            // 
            this.lblContactFax1.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContactFax1.Location = new System.Drawing.Point(3, 215);
            this.lblContactFax1.Name = "lblContactFax1";
            this.lblContactFax1.Size = new System.Drawing.Size(120, 13);
            this.lblContactFax1.TabIndex = 159;
            this.lblContactFax1.Text = "Fax 1:";
            this.lblContactFax1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblContactCountry
            // 
            this.lblContactCountry.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContactCountry.Location = new System.Drawing.Point(275, 88);
            this.lblContactCountry.Name = "lblContactCountry";
            this.lblContactCountry.Size = new System.Drawing.Size(100, 13);
            this.lblContactCountry.TabIndex = 166;
            this.lblContactCountry.Text = "Country:*";
            this.lblContactCountry.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtContactPhone2
            // 
            this.txtContactPhone2.Location = new System.Drawing.Point(377, 152);
            this.txtContactPhone2.MaxLength = 20;
            this.txtContactPhone2.Name = "txtContactPhone2";
            this.txtContactPhone2.Size = new System.Drawing.Size(132, 23);
            this.txtContactPhone2.TabIndex = 9;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(513, 18);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(103, 13);
            this.label7.TabIndex = 165;
            this.label7.Text = "Address Line3:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblContactAddress1
            // 
            this.lblContactAddress1.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContactAddress1.Location = new System.Drawing.Point(3, 17);
            this.lblContactAddress1.Name = "lblContactAddress1";
            this.lblContactAddress1.Size = new System.Drawing.Size(120, 13);
            this.lblContactAddress1.TabIndex = 163;
            this.lblContactAddress1.Text = "Address Line1:*";
            this.lblContactAddress1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtContactAddress2
            // 
            this.txtContactAddress2.Location = new System.Drawing.Point(377, 14);
            this.txtContactAddress2.MaxLength = 100;
            this.txtContactAddress2.Multiline = true;
            this.txtContactAddress2.Name = "txtContactAddress2";
            this.txtContactAddress2.Size = new System.Drawing.Size(132, 63);
            this.txtContactAddress2.TabIndex = 1;
            // 
            // lblContactAddress2
            // 
            this.lblContactAddress2.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContactAddress2.Location = new System.Drawing.Point(250, 17);
            this.lblContactAddress2.Name = "lblContactAddress2";
            this.lblContactAddress2.Size = new System.Drawing.Size(125, 13);
            this.lblContactAddress2.TabIndex = 164;
            this.lblContactAddress2.Text = "Address Line2:*";
            this.lblContactAddress2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbPickUpCenter
            // 
            this.cmbPickUpCenter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPickUpCenter.Enabled = false;
            this.cmbPickUpCenter.FormattingEnabled = true;
            this.cmbPickUpCenter.Location = new System.Drawing.Point(559, 12);
            this.cmbPickUpCenter.Name = "cmbPickUpCenter";
            this.cmbPickUpCenter.Size = new System.Drawing.Size(218, 24);
            this.cmbPickUpCenter.TabIndex = 2;
            this.cmbPickUpCenter.SelectedIndexChanged += new System.EventHandler(this.cmbPickUpCenter_SelectedIndexChanged);
            // 
            // lblPC
            // 
            this.lblPC.AutoSize = true;
            this.lblPC.BackColor = System.Drawing.Color.Transparent;
            this.lblPC.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblPC.Location = new System.Drawing.Point(446, 15);
            this.lblPC.Name = "lblPC";
            this.lblPC.Size = new System.Drawing.Size(114, 16);
            this.lblPC.TabIndex = 17;
            this.lblPC.Text = "PickUp Center:";
            // 
            // errorAdd
            // 
            this.errorAdd.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorAdd.ContainerControl = this;
            // 
            // lblDistributor
            // 
            this.lblDistributor.AutoSize = true;
            this.lblDistributor.BackColor = System.Drawing.Color.Transparent;
            this.lblDistributor.Location = new System.Drawing.Point(215, 15);
            this.lblDistributor.Name = "lblDistributor";
            this.lblDistributor.Size = new System.Drawing.Size(109, 16);
            this.lblDistributor.TabIndex = 18;
            this.lblDistributor.Text = "Distributor Id:";
            // 
            // txtDistributorId
            // 
            this.txtDistributorId.Enabled = false;
            this.txtDistributorId.Location = new System.Drawing.Point(323, 12);
            this.txtDistributorId.MaxLength = 20;
            this.txtDistributorId.Name = "txtDistributorId";
            this.txtDistributorId.Size = new System.Drawing.Size(110, 23);
            this.txtDistributorId.TabIndex = 1;
            this.txtDistributorId.Validated += new System.EventHandler(this.txtDistributorId_Validated);
            this.txtDistributorId.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDistributorId_KeyDown);
            this.txtDistributorId.Leave += new System.EventHandler(this.txtDistributorId_Leave);
            // 
            // DeliveryAdress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(802, 364);
            this.Controls.Add(this.txtDistributorId);
            this.Controls.Add(this.lblDistributor);
            this.Controls.Add(this.cmbPickUpCenter);
            this.Controls.Add(this.lblPC);
            this.Controls.Add(this.pnlAddress);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.lblAddress);
            this.Controls.Add(this.cmbAddressType);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "DeliveryAdress";
            this.Text = "DeliveryAdress";
            this.pnlAddress.ResumeLayout(false);
            this.pnlAddress.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorAdd)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbAddressType;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Panel pnlAddress;
        private System.Windows.Forms.TextBox txtContactWebsite;
        private System.Windows.Forms.Label lblContactWebsite;
        private System.Windows.Forms.TextBox txtContactFax2;
        private System.Windows.Forms.Label lblContactFax2;
        private System.Windows.Forms.Label lblContactEmail2;
        private System.Windows.Forms.TextBox txtContactEmail2;
        private System.Windows.Forms.Label lblContactMobile2;
        private System.Windows.Forms.TextBox txtContactMobile2;
        private System.Windows.Forms.Label lblContactPhone1;
        private System.Windows.Forms.TextBox txtContactPhone1;
        private System.Windows.Forms.TextBox txtContactAddress4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtContactAddress1;
        private System.Windows.Forms.ComboBox cmbContactCity;
        private System.Windows.Forms.TextBox txtContactPinCode;
        private System.Windows.Forms.Label lblContactPinCode;
        private System.Windows.Forms.Label lblContactMobile1;
        private System.Windows.Forms.Label lblContactCity;
        private System.Windows.Forms.Label lblContactEmail1;
        private System.Windows.Forms.TextBox txtContactFax1;
        private System.Windows.Forms.ComboBox cmbContactCountry;
        private System.Windows.Forms.TextBox txtContactMobile1;
        private System.Windows.Forms.ComboBox cmbContactState;
        private System.Windows.Forms.Label lblContactPhone2;
        private System.Windows.Forms.Label lblContactState;
        private System.Windows.Forms.TextBox txtContactEmail1;
        private System.Windows.Forms.TextBox txtContactAddress3;
        private System.Windows.Forms.Label lblContactFax1;
        private System.Windows.Forms.Label lblContactCountry;
        private System.Windows.Forms.TextBox txtContactPhone2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblContactAddress1;
        private System.Windows.Forms.TextBox txtContactAddress2;
        private System.Windows.Forms.Label lblContactAddress2;
        private System.Windows.Forms.ComboBox cmbPickUpCenter;
        private System.Windows.Forms.Label lblPC;
        private System.Windows.Forms.ErrorProvider errorAdd;
        private System.Windows.Forms.TextBox txtDistributorId;
        private System.Windows.Forms.Label lblDistributor;
    }
}