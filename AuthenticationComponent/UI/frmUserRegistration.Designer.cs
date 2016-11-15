namespace AuthenticationComponent.UI
{
    partial class frmUserRegistration
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUserRegistration));
            this.txtCreatePassword = new System.Windows.Forms.TextBox();
            this.txtCreateUserName = new System.Windows.Forms.TextBox();
            this.lblCreatePassword = new System.Windows.Forms.Label();
            this.lblCreateUserName = new System.Windows.Forms.Label();
            this.txtCreateFirstName = new System.Windows.Forms.TextBox();
            this.lblCreateFirstName = new System.Windows.Forms.Label();
            this.txtCreateLastName = new System.Windows.Forms.TextBox();
            this.lblCreateLastName = new System.Windows.Forms.Label();
            this.txtCreateAddress2 = new System.Windows.Forms.TextBox();
            this.txtCreateAddress1 = new System.Windows.Forms.TextBox();
            this.lblCreateAddress2 = new System.Windows.Forms.Label();
            this.lblCreateAddress1 = new System.Windows.Forms.Label();
            this.txtCreateAddress3 = new System.Windows.Forms.TextBox();
            this.lblCreateAddress3 = new System.Windows.Forms.Label();
            this.txtCreateFax = new System.Windows.Forms.TextBox();
            this.lblCreateFax = new System.Windows.Forms.Label();
            this.txtCreateMobile = new System.Windows.Forms.TextBox();
            this.txtCreatePhone = new System.Windows.Forms.TextBox();
            this.lblCreateMobile = new System.Windows.Forms.Label();
            this.lblCreatePhone = new System.Windows.Forms.Label();
            this.lblCreateCountry = new System.Windows.Forms.Label();
            this.cmbCreateCountry = new System.Windows.Forms.ComboBox();
            this.cmbCreateState = new System.Windows.Forms.ComboBox();
            this.lblCreateState = new System.Windows.Forms.Label();
            this.cmbCreateCity = new System.Windows.Forms.ComboBox();
            this.lblCreateCity = new System.Windows.Forms.Label();
            this.txtCreatePinCode = new System.Windows.Forms.TextBox();
            this.lblCreatePinCode = new System.Windows.Forms.Label();
            this.cmbCreateLocation = new System.Windows.Forms.ComboBox();
            this.lblCreateLocation = new System.Windows.Forms.Label();
            this.chkListBoxRoles = new System.Windows.Forms.CheckedListBox();
            this.grpBoxCreateRoles = new System.Windows.Forms.GroupBox();
            this.btnCreateSelNoneRoles = new System.Windows.Forms.Button();
            this.btnCreateSelAllRoles = new System.Windows.Forms.Button();
            this.dgvCreateLocationRoles = new System.Windows.Forms.DataGridView();
            this.btnCreateAddLocRole = new System.Windows.Forms.Button();
            this.txtSearchUserName = new System.Windows.Forms.TextBox();
            this.lblSearchUserName = new System.Windows.Forms.Label();
            this.txtSearchLastName = new System.Windows.Forms.TextBox();
            this.txtSearchFirstName = new System.Windows.Forms.TextBox();
            this.lblSearchLastName = new System.Windows.Forms.Label();
            this.lblSearchFirstName = new System.Windows.Forms.Label();
            this.txtSearchAddress1 = new System.Windows.Forms.TextBox();
            this.lblSearchAddress1 = new System.Windows.Forms.Label();
            this.txtSearchMobile = new System.Windows.Forms.TextBox();
            this.lblSearchMobile = new System.Windows.Forms.Label();
            this.txtSearchPinCode = new System.Windows.Forms.TextBox();
            this.lblSearchPinCode = new System.Windows.Forms.Label();
            this.cmbSearchCity = new System.Windows.Forms.ComboBox();
            this.lblSearchCity = new System.Windows.Forms.Label();
            this.cmbSearchState = new System.Windows.Forms.ComboBox();
            this.lblSearchState = new System.Windows.Forms.Label();
            this.cmbSearchCountry = new System.Windows.Forms.ComboBox();
            this.lblSearchCountry = new System.Windows.Forms.Label();
            this.cmbSearchUserStatus = new System.Windows.Forms.ComboBox();
            this.lblSearchUserStatus = new System.Windows.Forms.Label();
            this.dgvSearchUsers = new System.Windows.Forms.DataGridView();
            this.cmbCreateStatus = new System.Windows.Forms.ComboBox();
            this.lblCreateStatus = new System.Windows.Forms.Label();
            this.btnRemoveLocRole = new System.Windows.Forms.Button();
            this.dtpDob = new System.Windows.Forms.DateTimePicker();
            this.lblDOb = new System.Windows.Forms.Label();
            this.cmbCreateTitle = new System.Windows.Forms.ComboBox();
            this.lblCreateTitle = new System.Windows.Forms.Label();
            this.txtCreateEmail = new System.Windows.Forms.TextBox();
            this.lblCreateEmail = new System.Windows.Forms.Label();
            this.lblCreateLocRoleHeader = new System.Windows.Forms.Label();
            this.lblDesignation = new System.Windows.Forms.Label();
            this.txtCreateDesignation = new System.Windows.Forms.TextBox();
            this.errCreateUser = new System.Windows.Forms.ErrorProvider(this.components);
            this.cmbcenterspec = new System.Windows.Forms.ComboBox();
            this.tabPageSearch.SuspendLayout();
            this.pnlSearchHeader.SuspendLayout();
            this.tabPageCreate.SuspendLayout();
            this.pnlCreateDetails.SuspendLayout();
            this.pnlDetailsHeader.SuspendLayout();
            this.pnlSearchGrid.SuspendLayout();
            this.grpBoxCreateRoles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCreateLocationRoles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearchUsers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errCreateUser)).BeginInit();
            this.SuspendLayout();
            // 
            // tabPageSearch
            // 
            this.tabPageSearch.Size = new System.Drawing.Size(1005, 611);
            // 
            // pnlSearchHeader
            // 
            this.pnlSearchHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSearchHeader.Controls.Add(this.cmbSearchUserStatus);
            this.pnlSearchHeader.Controls.Add(this.lblSearchUserStatus);
            this.pnlSearchHeader.Controls.Add(this.cmbSearchCity);
            this.pnlSearchHeader.Controls.Add(this.lblSearchCity);
            this.pnlSearchHeader.Controls.Add(this.cmbSearchState);
            this.pnlSearchHeader.Controls.Add(this.lblSearchState);
            this.pnlSearchHeader.Controls.Add(this.cmbSearchCountry);
            this.pnlSearchHeader.Controls.Add(this.lblSearchCountry);
            this.pnlSearchHeader.Controls.Add(this.txtSearchPinCode);
            this.pnlSearchHeader.Controls.Add(this.lblSearchPinCode);
            this.pnlSearchHeader.Controls.Add(this.txtSearchMobile);
            this.pnlSearchHeader.Controls.Add(this.lblSearchMobile);
            this.pnlSearchHeader.Controls.Add(this.txtSearchAddress1);
            this.pnlSearchHeader.Controls.Add(this.lblSearchAddress1);
            this.pnlSearchHeader.Controls.Add(this.txtSearchLastName);
            this.pnlSearchHeader.Controls.Add(this.txtSearchFirstName);
            this.pnlSearchHeader.Controls.Add(this.lblSearchLastName);
            this.pnlSearchHeader.Controls.Add(this.lblSearchFirstName);
            this.pnlSearchHeader.Controls.Add(this.txtSearchUserName);
            this.pnlSearchHeader.Controls.Add(this.lblSearchUserName);
            this.pnlSearchHeader.Size = new System.Drawing.Size(1005, 160);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblSearchUserName, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.txtSearchUserName, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblSearchFirstName, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblSearchLastName, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.txtSearchFirstName, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.txtSearchLastName, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblSearchAddress1, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.txtSearchAddress1, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblSearchMobile, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.txtSearchMobile, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblSearchPinCode, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.txtSearchPinCode, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblSearchCountry, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.cmbSearchCountry, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblSearchState, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.cmbSearchState, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblSearchCity, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.cmbSearchCity, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.lblSearchUserStatus, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.cmbSearchUserStatus, 0);
            // 
            // btnSearch
            // 
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearch.Location = new System.Drawing.Point(853, 0);
            this.btnSearch.TabIndex = 21;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnSearchReset
            // 
            this.btnSearchReset.FlatAppearance.BorderSize = 0;
            this.btnSearchReset.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearchReset.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearchReset.Location = new System.Drawing.Point(928, 0);
            this.btnSearchReset.TabIndex = 22;
            this.btnSearchReset.Click += new System.EventHandler(this.btnSearchReset_Click);
            // 
            // lblSearchResult
            // 
            this.lblSearchResult.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblSearchResult.Size = new System.Drawing.Size(1005, 24);
            // 
            // tabPageCreate
            // 
            this.tabPageCreate.Size = new System.Drawing.Size(1005, 611);
            // 
            // pnlCreateDetails
            // 
            this.pnlCreateDetails.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCreateDetails.Controls.Add(this.cmbcenterspec);
            this.pnlCreateDetails.Controls.Add(this.dtpDob);
            this.pnlCreateDetails.Controls.Add(this.lblDOb);
            this.pnlCreateDetails.Controls.Add(this.btnRemoveLocRole);
            this.pnlCreateDetails.Controls.Add(this.btnCreateAddLocRole);
            this.pnlCreateDetails.Controls.Add(this.dgvCreateLocationRoles);
            this.pnlCreateDetails.Controls.Add(this.grpBoxCreateRoles);
            this.pnlCreateDetails.Controls.Add(this.cmbCreateLocation);
            this.pnlCreateDetails.Controls.Add(this.lblCreateLocation);
            this.pnlCreateDetails.Controls.Add(this.lblCreateLocRoleHeader);
            this.pnlCreateDetails.Controls.Add(this.txtCreatePinCode);
            this.pnlCreateDetails.Controls.Add(this.lblCreatePinCode);
            this.pnlCreateDetails.Controls.Add(this.cmbCreateCity);
            this.pnlCreateDetails.Controls.Add(this.lblCreateCity);
            this.pnlCreateDetails.Controls.Add(this.cmbCreateState);
            this.pnlCreateDetails.Controls.Add(this.lblCreateState);
            this.pnlCreateDetails.Controls.Add(this.cmbCreateCountry);
            this.pnlCreateDetails.Controls.Add(this.lblCreateCountry);
            this.pnlCreateDetails.Controls.Add(this.txtCreateFax);
            this.pnlCreateDetails.Controls.Add(this.lblCreateFax);
            this.pnlCreateDetails.Controls.Add(this.txtCreateMobile);
            this.pnlCreateDetails.Controls.Add(this.txtCreatePhone);
            this.pnlCreateDetails.Controls.Add(this.lblCreateMobile);
            this.pnlCreateDetails.Controls.Add(this.lblCreatePhone);
            this.pnlCreateDetails.Controls.Add(this.txtCreateAddress3);
            this.pnlCreateDetails.Controls.Add(this.lblCreateAddress3);
            this.pnlCreateDetails.Controls.Add(this.txtCreateAddress2);
            this.pnlCreateDetails.Controls.Add(this.txtCreateAddress1);
            this.pnlCreateDetails.Controls.Add(this.lblCreateAddress2);
            this.pnlCreateDetails.Controls.Add(this.lblCreateAddress1);
            this.pnlCreateDetails.Size = new System.Drawing.Size(999, 443);
            this.pnlCreateDetails.TabIndex = 1;
            // 
            // pnlDetailsHeader
            // 
            this.pnlDetailsHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlDetailsHeader.Controls.Add(this.txtCreateDesignation);
            this.pnlDetailsHeader.Controls.Add(this.txtCreateEmail);
            this.pnlDetailsHeader.Controls.Add(this.lblDesignation);
            this.pnlDetailsHeader.Controls.Add(this.lblCreateEmail);
            this.pnlDetailsHeader.Controls.Add(this.cmbCreateTitle);
            this.pnlDetailsHeader.Controls.Add(this.lblCreateTitle);
            this.pnlDetailsHeader.Controls.Add(this.cmbCreateStatus);
            this.pnlDetailsHeader.Controls.Add(this.lblCreateStatus);
            this.pnlDetailsHeader.Controls.Add(this.txtCreateLastName);
            this.pnlDetailsHeader.Controls.Add(this.txtCreatePassword);
            this.pnlDetailsHeader.Controls.Add(this.txtCreateFirstName);
            this.pnlDetailsHeader.Controls.Add(this.lblCreateLastName);
            this.pnlDetailsHeader.Controls.Add(this.lblCreateFirstName);
            this.pnlDetailsHeader.Controls.Add(this.txtCreateUserName);
            this.pnlDetailsHeader.Controls.Add(this.lblCreatePassword);
            this.pnlDetailsHeader.Controls.Add(this.lblCreateUserName);
            this.pnlDetailsHeader.Size = new System.Drawing.Size(1005, 115);
            this.pnlDetailsHeader.TabIndex = 0;
            this.pnlDetailsHeader.Controls.SetChildIndex(this.lblCreateUserName, 0);
            this.pnlDetailsHeader.Controls.SetChildIndex(this.lblCreatePassword, 0);
            this.pnlDetailsHeader.Controls.SetChildIndex(this.txtCreateUserName, 0);
            this.pnlDetailsHeader.Controls.SetChildIndex(this.lblCreateFirstName, 0);
            this.pnlDetailsHeader.Controls.SetChildIndex(this.lblCreateLastName, 0);
            this.pnlDetailsHeader.Controls.SetChildIndex(this.txtCreateFirstName, 0);
            this.pnlDetailsHeader.Controls.SetChildIndex(this.txtCreatePassword, 0);
            this.pnlDetailsHeader.Controls.SetChildIndex(this.txtCreateLastName, 0);
            this.pnlDetailsHeader.Controls.SetChildIndex(this.lblCreateStatus, 0);
            this.pnlDetailsHeader.Controls.SetChildIndex(this.cmbCreateStatus, 0);
            this.pnlDetailsHeader.Controls.SetChildIndex(this.lblCreateTitle, 0);
            this.pnlDetailsHeader.Controls.SetChildIndex(this.cmbCreateTitle, 0);
            this.pnlDetailsHeader.Controls.SetChildIndex(this.lblCreateEmail, 0);
            this.pnlDetailsHeader.Controls.SetChildIndex(this.lblDesignation, 0);
            this.pnlDetailsHeader.Controls.SetChildIndex(this.txtCreateEmail, 0);
            this.pnlDetailsHeader.Controls.SetChildIndex(this.txtCreateDesignation, 0);
            // 
            // btnClearDetails
            // 
            this.btnClearDetails.FlatAppearance.BorderSize = 0;
            this.btnClearDetails.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnClearDetails.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnClearDetails.Location = new System.Drawing.Point(933, 0);
            this.btnClearDetails.Size = new System.Drawing.Size(70, 32);
            this.btnClearDetails.TabIndex = 10;
            this.btnClearDetails.Visible = false;
            // 
            // btnAddDetails
            // 
            this.btnAddDetails.FlatAppearance.BorderSize = 0;
            this.btnAddDetails.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnAddDetails.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnAddDetails.Location = new System.Drawing.Point(863, 0);
            this.btnAddDetails.Size = new System.Drawing.Size(70, 32);
            this.btnAddDetails.TabIndex = 9;
            this.btnAddDetails.Visible = false;
            // 
            // btnCreateReset
            // 
            this.btnCreateReset.FlatAppearance.BorderSize = 0;
            this.btnCreateReset.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnCreateReset.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnCreateReset.Location = new System.Drawing.Point(924, 0);
            this.btnCreateReset.TabIndex = 52;
            this.btnCreateReset.Click += new System.EventHandler(this.btnCreateReset_Click);
            // 
            // btnSave
            // 
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSave.Location = new System.Drawing.Point(849, 0);
            this.btnSave.TabIndex = 51;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // pnlSearchGrid
            // 
            this.pnlSearchGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSearchGrid.Controls.Add(this.dgvSearchUsers);
            this.pnlSearchGrid.Location = new System.Drawing.Point(0, 184);
            this.pnlSearchGrid.Size = new System.Drawing.Size(1005, 427);
            // 
            // txtCreatePassword
            // 
            this.txtCreatePassword.Location = new System.Drawing.Point(767, 3);
            this.txtCreatePassword.MaxLength = 20;
            this.txtCreatePassword.Name = "txtCreatePassword";
            this.txtCreatePassword.PasswordChar = '*';
            this.txtCreatePassword.Size = new System.Drawing.Size(150, 21);
            this.txtCreatePassword.TabIndex = 3;
            // 
            // txtCreateUserName
            // 
            this.txtCreateUserName.Location = new System.Drawing.Point(450, 3);
            this.txtCreateUserName.MaxLength = 30;
            this.txtCreateUserName.Name = "txtCreateUserName";
            this.txtCreateUserName.Size = new System.Drawing.Size(110, 21);
            this.txtCreateUserName.TabIndex = 2;
            // 
            // lblCreatePassword
            // 
            this.lblCreatePassword.Location = new System.Drawing.Point(636, 3);
            this.lblCreatePassword.Name = "lblCreatePassword";
            this.lblCreatePassword.Size = new System.Drawing.Size(120, 13);
            this.lblCreatePassword.TabIndex = 27;
            this.lblCreatePassword.Text = "Password:*";
            this.lblCreatePassword.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCreateUserName
            // 
            this.lblCreateUserName.Location = new System.Drawing.Point(319, 3);
            this.lblCreateUserName.Name = "lblCreateUserName";
            this.lblCreateUserName.Size = new System.Drawing.Size(125, 13);
            this.lblCreateUserName.TabIndex = 26;
            this.lblCreateUserName.Text = "User Name:*";
            this.lblCreateUserName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCreateFirstName
            // 
            this.txtCreateFirstName.Location = new System.Drawing.Point(146, 28);
            this.txtCreateFirstName.MaxLength = 50;
            this.txtCreateFirstName.Name = "txtCreateFirstName";
            this.txtCreateFirstName.Size = new System.Drawing.Size(110, 21);
            this.txtCreateFirstName.TabIndex = 4;
            // 
            // lblCreateFirstName
            // 
            this.lblCreateFirstName.Location = new System.Drawing.Point(15, 28);
            this.lblCreateFirstName.Name = "lblCreateFirstName";
            this.lblCreateFirstName.Size = new System.Drawing.Size(125, 13);
            this.lblCreateFirstName.TabIndex = 29;
            this.lblCreateFirstName.Text = "First Name:*";
            this.lblCreateFirstName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCreateLastName
            // 
            this.txtCreateLastName.Location = new System.Drawing.Point(450, 28);
            this.txtCreateLastName.MaxLength = 50;
            this.txtCreateLastName.Name = "txtCreateLastName";
            this.txtCreateLastName.Size = new System.Drawing.Size(110, 21);
            this.txtCreateLastName.TabIndex = 5;
            // 
            // lblCreateLastName
            // 
            this.lblCreateLastName.Location = new System.Drawing.Point(319, 28);
            this.lblCreateLastName.Name = "lblCreateLastName";
            this.lblCreateLastName.Size = new System.Drawing.Size(125, 13);
            this.lblCreateLastName.TabIndex = 31;
            this.lblCreateLastName.Text = "Last Name:";
            this.lblCreateLastName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCreateAddress2
            // 
            this.txtCreateAddress2.Location = new System.Drawing.Point(450, 12);
            this.txtCreateAddress2.MaxLength = 50;
            this.txtCreateAddress2.Name = "txtCreateAddress2";
            this.txtCreateAddress2.Size = new System.Drawing.Size(121, 21);
            this.txtCreateAddress2.TabIndex = 1;
            // 
            // txtCreateAddress1
            // 
            this.txtCreateAddress1.Location = new System.Drawing.Point(146, 12);
            this.txtCreateAddress1.MaxLength = 50;
            this.txtCreateAddress1.Name = "txtCreateAddress1";
            this.txtCreateAddress1.Size = new System.Drawing.Size(110, 21);
            this.txtCreateAddress1.TabIndex = 0;
            // 
            // lblCreateAddress2
            // 
            this.lblCreateAddress2.Location = new System.Drawing.Point(319, 15);
            this.lblCreateAddress2.Name = "lblCreateAddress2";
            this.lblCreateAddress2.Size = new System.Drawing.Size(125, 13);
            this.lblCreateAddress2.TabIndex = 31;
            this.lblCreateAddress2.Text = "Address Line2:";
            this.lblCreateAddress2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCreateAddress1
            // 
            this.lblCreateAddress1.Location = new System.Drawing.Point(15, 15);
            this.lblCreateAddress1.Name = "lblCreateAddress1";
            this.lblCreateAddress1.Size = new System.Drawing.Size(125, 13);
            this.lblCreateAddress1.TabIndex = 30;
            this.lblCreateAddress1.Text = "Address Line1:*";
            this.lblCreateAddress1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCreateAddress3
            // 
            this.txtCreateAddress3.Location = new System.Drawing.Point(767, 12);
            this.txtCreateAddress3.MaxLength = 50;
            this.txtCreateAddress3.Name = "txtCreateAddress3";
            this.txtCreateAddress3.Size = new System.Drawing.Size(110, 21);
            this.txtCreateAddress3.TabIndex = 2;
            // 
            // lblCreateAddress3
            // 
            this.lblCreateAddress3.Location = new System.Drawing.Point(636, 15);
            this.lblCreateAddress3.Name = "lblCreateAddress3";
            this.lblCreateAddress3.Size = new System.Drawing.Size(125, 13);
            this.lblCreateAddress3.TabIndex = 33;
            this.lblCreateAddress3.Text = "Address Line3:";
            this.lblCreateAddress3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCreateFax
            // 
            this.txtCreateFax.Location = new System.Drawing.Point(767, 39);
            this.txtCreateFax.MaxLength = 20;
            this.txtCreateFax.Name = "txtCreateFax";
            this.txtCreateFax.Size = new System.Drawing.Size(110, 21);
            this.txtCreateFax.TabIndex = 5;
            // 
            // lblCreateFax
            // 
            this.lblCreateFax.Location = new System.Drawing.Point(636, 42);
            this.lblCreateFax.Name = "lblCreateFax";
            this.lblCreateFax.Size = new System.Drawing.Size(125, 13);
            this.lblCreateFax.TabIndex = 39;
            this.lblCreateFax.Text = "Fax:";
            this.lblCreateFax.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCreateMobile
            // 
            this.txtCreateMobile.Location = new System.Drawing.Point(450, 39);
            this.txtCreateMobile.MaxLength = 10;
            this.txtCreateMobile.Name = "txtCreateMobile";
            this.txtCreateMobile.Size = new System.Drawing.Size(121, 21);
            this.txtCreateMobile.TabIndex = 4;
            // 
            // txtCreatePhone
            // 
            this.txtCreatePhone.Location = new System.Drawing.Point(146, 39);
            this.txtCreatePhone.MaxLength = 20;
            this.txtCreatePhone.Name = "txtCreatePhone";
            this.txtCreatePhone.Size = new System.Drawing.Size(110, 21);
            this.txtCreatePhone.TabIndex = 3;
            // 
            // lblCreateMobile
            // 
            this.lblCreateMobile.Location = new System.Drawing.Point(319, 42);
            this.lblCreateMobile.Name = "lblCreateMobile";
            this.lblCreateMobile.Size = new System.Drawing.Size(125, 13);
            this.lblCreateMobile.TabIndex = 37;
            this.lblCreateMobile.Text = "Mobile:*";
            this.lblCreateMobile.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCreatePhone
            // 
            this.lblCreatePhone.Location = new System.Drawing.Point(15, 42);
            this.lblCreatePhone.Name = "lblCreatePhone";
            this.lblCreatePhone.Size = new System.Drawing.Size(125, 13);
            this.lblCreatePhone.TabIndex = 36;
            this.lblCreatePhone.Text = "Phone (Home):";
            this.lblCreatePhone.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCreateCountry
            // 
            this.lblCreateCountry.Location = new System.Drawing.Point(15, 69);
            this.lblCreateCountry.Name = "lblCreateCountry";
            this.lblCreateCountry.Size = new System.Drawing.Size(125, 13);
            this.lblCreateCountry.TabIndex = 41;
            this.lblCreateCountry.Text = "Country:*";
            this.lblCreateCountry.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbCreateCountry
            // 
            this.cmbCreateCountry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCreateCountry.FormattingEnabled = true;
            this.cmbCreateCountry.Location = new System.Drawing.Point(146, 66);
            this.cmbCreateCountry.Name = "cmbCreateCountry";
            this.cmbCreateCountry.Size = new System.Drawing.Size(110, 21);
            this.cmbCreateCountry.TabIndex = 6;
            this.cmbCreateCountry.SelectedIndexChanged += new System.EventHandler(this.cmbCreateCountry_SelectedIndexChanged);
            // 
            // cmbCreateState
            // 
            this.cmbCreateState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCreateState.FormattingEnabled = true;
            this.cmbCreateState.Location = new System.Drawing.Point(450, 66);
            this.cmbCreateState.Name = "cmbCreateState";
            this.cmbCreateState.Size = new System.Drawing.Size(121, 21);
            this.cmbCreateState.TabIndex = 7;
            this.cmbCreateState.SelectedIndexChanged += new System.EventHandler(this.cmbCreateState_SelectedIndexChanged);
            // 
            // lblCreateState
            // 
            this.lblCreateState.Location = new System.Drawing.Point(319, 69);
            this.lblCreateState.Name = "lblCreateState";
            this.lblCreateState.Size = new System.Drawing.Size(125, 13);
            this.lblCreateState.TabIndex = 43;
            this.lblCreateState.Text = "State:*";
            this.lblCreateState.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbCreateCity
            // 
            this.cmbCreateCity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCreateCity.FormattingEnabled = true;
            this.cmbCreateCity.Location = new System.Drawing.Point(767, 66);
            this.cmbCreateCity.Name = "cmbCreateCity";
            this.cmbCreateCity.Size = new System.Drawing.Size(110, 21);
            this.cmbCreateCity.TabIndex = 8;
            // 
            // lblCreateCity
            // 
            this.lblCreateCity.Location = new System.Drawing.Point(636, 69);
            this.lblCreateCity.Name = "lblCreateCity";
            this.lblCreateCity.Size = new System.Drawing.Size(125, 13);
            this.lblCreateCity.TabIndex = 45;
            this.lblCreateCity.Text = "City:*";
            this.lblCreateCity.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCreatePinCode
            // 
            this.txtCreatePinCode.Location = new System.Drawing.Point(146, 93);
            this.txtCreatePinCode.MaxLength = 6;
            this.txtCreatePinCode.Name = "txtCreatePinCode";
            this.txtCreatePinCode.Size = new System.Drawing.Size(110, 21);
            this.txtCreatePinCode.TabIndex = 9;
            // 
            // lblCreatePinCode
            // 
            this.lblCreatePinCode.Location = new System.Drawing.Point(15, 96);
            this.lblCreatePinCode.Name = "lblCreatePinCode";
            this.lblCreatePinCode.Size = new System.Drawing.Size(125, 13);
            this.lblCreatePinCode.TabIndex = 48;
            this.lblCreatePinCode.Text = "Pin Code:";
            this.lblCreatePinCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbCreateLocation
            // 
            this.cmbCreateLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCreateLocation.FormattingEnabled = true;
            this.cmbCreateLocation.Items.AddRange(new object[] {
            "Select"});
            this.cmbCreateLocation.Location = new System.Drawing.Point(175, 147);
            this.cmbCreateLocation.Name = "cmbCreateLocation";
            this.cmbCreateLocation.Size = new System.Drawing.Size(160, 21);
            this.cmbCreateLocation.TabIndex = 11;
            this.cmbCreateLocation.SelectedIndexChanged += new System.EventHandler(this.cmbCreateLocation_SelectedIndexChanged);
            // 
            // lblCreateLocation
            // 
            this.lblCreateLocation.Location = new System.Drawing.Point(15, 150);
            this.lblCreateLocation.Name = "lblCreateLocation";
            this.lblCreateLocation.Size = new System.Drawing.Size(70, 13);
            this.lblCreateLocation.TabIndex = 50;
            this.lblCreateLocation.Text = "Location:";
            this.lblCreateLocation.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkListBoxRoles
            // 
            this.chkListBoxRoles.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.chkListBoxRoles.CheckOnClick = true;
            this.chkListBoxRoles.FormattingEnabled = true;
            this.chkListBoxRoles.Location = new System.Drawing.Point(6, 20);
            this.chkListBoxRoles.Name = "chkListBoxRoles";
            this.chkListBoxRoles.Size = new System.Drawing.Size(229, 162);
            this.chkListBoxRoles.Sorted = true;
            this.chkListBoxRoles.TabIndex = 0;
            // 
            // grpBoxCreateRoles
            // 
            this.grpBoxCreateRoles.Controls.Add(this.btnCreateSelNoneRoles);
            this.grpBoxCreateRoles.Controls.Add(this.btnCreateSelAllRoles);
            this.grpBoxCreateRoles.Controls.Add(this.chkListBoxRoles);
            this.grpBoxCreateRoles.Location = new System.Drawing.Point(15, 174);
            this.grpBoxCreateRoles.Name = "grpBoxCreateRoles";
            this.grpBoxCreateRoles.Size = new System.Drawing.Size(241, 218);
            this.grpBoxCreateRoles.TabIndex = 12;
            this.grpBoxCreateRoles.TabStop = false;
            this.grpBoxCreateRoles.Text = "Role(s)";
            // 
            // btnCreateSelNoneRoles
            // 
            this.btnCreateSelNoneRoles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreateSelNoneRoles.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCreateSelNoneRoles.BackgroundImage")));
            this.btnCreateSelNoneRoles.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCreateSelNoneRoles.FlatAppearance.BorderSize = 0;
            this.btnCreateSelNoneRoles.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnCreateSelNoneRoles.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnCreateSelNoneRoles.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateSelNoneRoles.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnCreateSelNoneRoles.Location = new System.Drawing.Point(160, 185);
            this.btnCreateSelNoneRoles.Name = "btnCreateSelNoneRoles";
            this.btnCreateSelNoneRoles.Size = new System.Drawing.Size(75, 31);
            this.btnCreateSelNoneRoles.TabIndex = 2;
            this.btnCreateSelNoneRoles.Text = "N&one";
            this.btnCreateSelNoneRoles.UseVisualStyleBackColor = true;
            this.btnCreateSelNoneRoles.Click += new System.EventHandler(this.btnCreateSelNoneRoles_Click);
            // 
            // btnCreateSelAllRoles
            // 
            this.btnCreateSelAllRoles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCreateSelAllRoles.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCreateSelAllRoles.BackgroundImage")));
            this.btnCreateSelAllRoles.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCreateSelAllRoles.FlatAppearance.BorderSize = 0;
            this.btnCreateSelAllRoles.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnCreateSelAllRoles.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnCreateSelAllRoles.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateSelAllRoles.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnCreateSelAllRoles.Location = new System.Drawing.Point(6, 185);
            this.btnCreateSelAllRoles.Name = "btnCreateSelAllRoles";
            this.btnCreateSelAllRoles.Size = new System.Drawing.Size(75, 31);
            this.btnCreateSelAllRoles.TabIndex = 1;
            this.btnCreateSelAllRoles.Text = "A&ll";
            this.btnCreateSelAllRoles.UseVisualStyleBackColor = true;
            this.btnCreateSelAllRoles.Click += new System.EventHandler(this.btnCreateSelAllRoles_Click);
            // 
            // dgvCreateLocationRoles
            // 
            this.dgvCreateLocationRoles.AllowUserToAddRows = false;
            this.dgvCreateLocationRoles.AllowUserToDeleteRows = false;
            this.dgvCreateLocationRoles.AllowUserToResizeColumns = false;
            this.dgvCreateLocationRoles.AllowUserToResizeRows = false;
            this.dgvCreateLocationRoles.BackgroundColor = System.Drawing.Color.DarkGray;
            this.dgvCreateLocationRoles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCreateLocationRoles.GridColor = System.Drawing.Color.DarkGray;
            this.dgvCreateLocationRoles.Location = new System.Drawing.Point(447, 147);
            this.dgvCreateLocationRoles.Name = "dgvCreateLocationRoles";
            this.dgvCreateLocationRoles.RowHeadersVisible = false;
            this.dgvCreateLocationRoles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCreateLocationRoles.Size = new System.Drawing.Size(427, 262);
            this.dgvCreateLocationRoles.TabIndex = 15;
            this.dgvCreateLocationRoles.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCreateLocationRoles_CellContentClick);
            // 
            // btnCreateAddLocRole
            // 
            this.btnCreateAddLocRole.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCreateAddLocRole.BackgroundImage")));
            this.btnCreateAddLocRole.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCreateAddLocRole.FlatAppearance.BorderSize = 0;
            this.btnCreateAddLocRole.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnCreateAddLocRole.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnCreateAddLocRole.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateAddLocRole.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnCreateAddLocRole.Location = new System.Drawing.Point(309, 237);
            this.btnCreateAddLocRole.Name = "btnCreateAddLocRole";
            this.btnCreateAddLocRole.Size = new System.Drawing.Size(90, 32);
            this.btnCreateAddLocRole.TabIndex = 13;
            this.btnCreateAddLocRole.Text = "A&dd >>";
            this.btnCreateAddLocRole.UseVisualStyleBackColor = true;
            this.btnCreateAddLocRole.Click += new System.EventHandler(this.btnCreateAddLocRole_Click);
            // 
            // txtSearchUserName
            // 
            this.txtSearchUserName.Location = new System.Drawing.Point(142, 16);
            this.txtSearchUserName.MaxLength = 30;
            this.txtSearchUserName.Name = "txtSearchUserName";
            this.txtSearchUserName.Size = new System.Drawing.Size(110, 21);
            this.txtSearchUserName.TabIndex = 1;
            // 
            // lblSearchUserName
            // 
            this.lblSearchUserName.Location = new System.Drawing.Point(11, 19);
            this.lblSearchUserName.Name = "lblSearchUserName";
            this.lblSearchUserName.Size = new System.Drawing.Size(125, 13);
            this.lblSearchUserName.TabIndex = 28;
            this.lblSearchUserName.Text = "User Name:";
            this.lblSearchUserName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSearchLastName
            // 
            this.txtSearchLastName.Location = new System.Drawing.Point(683, 19);
            this.txtSearchLastName.MaxLength = 50;
            this.txtSearchLastName.Name = "txtSearchLastName";
            this.txtSearchLastName.Size = new System.Drawing.Size(110, 21);
            this.txtSearchLastName.TabIndex = 3;
            // 
            // txtSearchFirstName
            // 
            this.txtSearchFirstName.Location = new System.Drawing.Point(426, 16);
            this.txtSearchFirstName.MaxLength = 50;
            this.txtSearchFirstName.Name = "txtSearchFirstName";
            this.txtSearchFirstName.Size = new System.Drawing.Size(110, 21);
            this.txtSearchFirstName.TabIndex = 2;
            // 
            // lblSearchLastName
            // 
            this.lblSearchLastName.Location = new System.Drawing.Point(552, 19);
            this.lblSearchLastName.Name = "lblSearchLastName";
            this.lblSearchLastName.Size = new System.Drawing.Size(125, 13);
            this.lblSearchLastName.TabIndex = 35;
            this.lblSearchLastName.Text = "Last Name:";
            this.lblSearchLastName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSearchFirstName
            // 
            this.lblSearchFirstName.Location = new System.Drawing.Point(295, 19);
            this.lblSearchFirstName.Name = "lblSearchFirstName";
            this.lblSearchFirstName.Size = new System.Drawing.Size(125, 13);
            this.lblSearchFirstName.TabIndex = 34;
            this.lblSearchFirstName.Text = "First Name:";
            this.lblSearchFirstName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSearchAddress1
            // 
            this.txtSearchAddress1.Location = new System.Drawing.Point(142, 43);
            this.txtSearchAddress1.MaxLength = 50;
            this.txtSearchAddress1.Name = "txtSearchAddress1";
            this.txtSearchAddress1.Size = new System.Drawing.Size(110, 21);
            this.txtSearchAddress1.TabIndex = 4;
            // 
            // lblSearchAddress1
            // 
            this.lblSearchAddress1.Location = new System.Drawing.Point(11, 46);
            this.lblSearchAddress1.Name = "lblSearchAddress1";
            this.lblSearchAddress1.Size = new System.Drawing.Size(125, 13);
            this.lblSearchAddress1.TabIndex = 37;
            this.lblSearchAddress1.Text = "Address Line1:";
            this.lblSearchAddress1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSearchMobile
            // 
            this.txtSearchMobile.Location = new System.Drawing.Point(426, 43);
            this.txtSearchMobile.MaxLength = 10;
            this.txtSearchMobile.Name = "txtSearchMobile";
            this.txtSearchMobile.Size = new System.Drawing.Size(110, 21);
            this.txtSearchMobile.TabIndex = 5;
            // 
            // lblSearchMobile
            // 
            this.lblSearchMobile.Location = new System.Drawing.Point(295, 46);
            this.lblSearchMobile.Name = "lblSearchMobile";
            this.lblSearchMobile.Size = new System.Drawing.Size(125, 13);
            this.lblSearchMobile.TabIndex = 39;
            this.lblSearchMobile.Text = "Mobile:";
            this.lblSearchMobile.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSearchPinCode
            // 
            this.txtSearchPinCode.Location = new System.Drawing.Point(683, 46);
            this.txtSearchPinCode.MaxLength = 6;
            this.txtSearchPinCode.Name = "txtSearchPinCode";
            this.txtSearchPinCode.Size = new System.Drawing.Size(110, 21);
            this.txtSearchPinCode.TabIndex = 6;
            // 
            // lblSearchPinCode
            // 
            this.lblSearchPinCode.Location = new System.Drawing.Point(552, 46);
            this.lblSearchPinCode.Name = "lblSearchPinCode";
            this.lblSearchPinCode.Size = new System.Drawing.Size(125, 13);
            this.lblSearchPinCode.TabIndex = 50;
            this.lblSearchPinCode.Text = "Pin Code:";
            this.lblSearchPinCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbSearchCity
            // 
            this.cmbSearchCity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSearchCity.FormattingEnabled = true;
            this.cmbSearchCity.Location = new System.Drawing.Point(683, 73);
            this.cmbSearchCity.Name = "cmbSearchCity";
            this.cmbSearchCity.Size = new System.Drawing.Size(110, 21);
            this.cmbSearchCity.TabIndex = 9;
            // 
            // lblSearchCity
            // 
            this.lblSearchCity.Location = new System.Drawing.Point(552, 73);
            this.lblSearchCity.Name = "lblSearchCity";
            this.lblSearchCity.Size = new System.Drawing.Size(125, 13);
            this.lblSearchCity.TabIndex = 56;
            this.lblSearchCity.Text = "City:";
            this.lblSearchCity.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbSearchState
            // 
            this.cmbSearchState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSearchState.FormattingEnabled = true;
            this.cmbSearchState.Location = new System.Drawing.Point(426, 70);
            this.cmbSearchState.Name = "cmbSearchState";
            this.cmbSearchState.Size = new System.Drawing.Size(110, 21);
            this.cmbSearchState.TabIndex = 8;
            this.cmbSearchState.SelectedIndexChanged += new System.EventHandler(this.cmbSearchState_SelectedIndexChanged);
            // 
            // lblSearchState
            // 
            this.lblSearchState.Location = new System.Drawing.Point(295, 73);
            this.lblSearchState.Name = "lblSearchState";
            this.lblSearchState.Size = new System.Drawing.Size(125, 13);
            this.lblSearchState.TabIndex = 55;
            this.lblSearchState.Text = "State:";
            this.lblSearchState.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbSearchCountry
            // 
            this.cmbSearchCountry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSearchCountry.FormattingEnabled = true;
            this.cmbSearchCountry.Location = new System.Drawing.Point(142, 70);
            this.cmbSearchCountry.Name = "cmbSearchCountry";
            this.cmbSearchCountry.Size = new System.Drawing.Size(110, 21);
            this.cmbSearchCountry.TabIndex = 7;
            this.cmbSearchCountry.SelectedIndexChanged += new System.EventHandler(this.cmbSearchCountry_SelectedIndexChanged);
            // 
            // lblSearchCountry
            // 
            this.lblSearchCountry.Location = new System.Drawing.Point(11, 73);
            this.lblSearchCountry.Name = "lblSearchCountry";
            this.lblSearchCountry.Size = new System.Drawing.Size(125, 13);
            this.lblSearchCountry.TabIndex = 54;
            this.lblSearchCountry.Text = "Country:";
            this.lblSearchCountry.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbSearchUserStatus
            // 
            this.cmbSearchUserStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSearchUserStatus.FormattingEnabled = true;
            this.cmbSearchUserStatus.Location = new System.Drawing.Point(142, 99);
            this.cmbSearchUserStatus.Name = "cmbSearchUserStatus";
            this.cmbSearchUserStatus.Size = new System.Drawing.Size(110, 21);
            this.cmbSearchUserStatus.TabIndex = 11;
            // 
            // lblSearchUserStatus
            // 
            this.lblSearchUserStatus.Location = new System.Drawing.Point(11, 99);
            this.lblSearchUserStatus.Name = "lblSearchUserStatus";
            this.lblSearchUserStatus.Size = new System.Drawing.Size(125, 13);
            this.lblSearchUserStatus.TabIndex = 60;
            this.lblSearchUserStatus.Text = "Status:";
            this.lblSearchUserStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dgvSearchUsers
            // 
            this.dgvSearchUsers.AllowUserToAddRows = false;
            this.dgvSearchUsers.AllowUserToDeleteRows = false;
            this.dgvSearchUsers.AllowUserToResizeColumns = false;
            this.dgvSearchUsers.AllowUserToResizeRows = false;
            this.dgvSearchUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSearchUsers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSearchUsers.Location = new System.Drawing.Point(0, 0);
            this.dgvSearchUsers.MultiSelect = false;
            this.dgvSearchUsers.Name = "dgvSearchUsers";
            this.dgvSearchUsers.ReadOnly = true;
            this.dgvSearchUsers.RowHeadersVisible = false;
            this.dgvSearchUsers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSearchUsers.Size = new System.Drawing.Size(1003, 425);
            this.dgvSearchUsers.TabIndex = 38;
            this.dgvSearchUsers.TabStop = false;
            this.dgvSearchUsers.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSearchUsers_CellDoubleClick);
            this.dgvSearchUsers.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSearchUsers_CellContentClick);
            // 
            // cmbCreateStatus
            // 
            this.cmbCreateStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCreateStatus.FormattingEnabled = true;
            this.cmbCreateStatus.Location = new System.Drawing.Point(450, 52);
            this.cmbCreateStatus.Name = "cmbCreateStatus";
            this.cmbCreateStatus.Size = new System.Drawing.Size(110, 21);
            this.cmbCreateStatus.TabIndex = 8;
            // 
            // lblCreateStatus
            // 
            this.lblCreateStatus.Location = new System.Drawing.Point(319, 52);
            this.lblCreateStatus.Name = "lblCreateStatus";
            this.lblCreateStatus.Size = new System.Drawing.Size(125, 13);
            this.lblCreateStatus.TabIndex = 62;
            this.lblCreateStatus.Text = "Status:*";
            this.lblCreateStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnRemoveLocRole
            // 
            this.btnRemoveLocRole.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRemoveLocRole.BackgroundImage")));
            this.btnRemoveLocRole.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRemoveLocRole.FlatAppearance.BorderSize = 0;
            this.btnRemoveLocRole.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnRemoveLocRole.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnRemoveLocRole.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemoveLocRole.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnRemoveLocRole.Location = new System.Drawing.Point(309, 276);
            this.btnRemoveLocRole.Name = "btnRemoveLocRole";
            this.btnRemoveLocRole.Size = new System.Drawing.Size(90, 32);
            this.btnRemoveLocRole.TabIndex = 14;
            this.btnRemoveLocRole.Text = "<< Re&move";
            this.btnRemoveLocRole.UseVisualStyleBackColor = true;
            this.btnRemoveLocRole.Click += new System.EventHandler(this.btnRemoveLocRole_Click);
            // 
            // dtpDob
            // 
            this.dtpDob.Checked = false;
            this.dtpDob.CustomFormat = "dd-MM-yyyy";
            this.dtpDob.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDob.Location = new System.Drawing.Point(450, 93);
            this.dtpDob.MaxDate = new System.DateTime(2009, 9, 4, 0, 0, 0, 0);
            this.dtpDob.Name = "dtpDob";
            this.dtpDob.ShowCheckBox = true;
            this.dtpDob.Size = new System.Drawing.Size(121, 21);
            this.dtpDob.TabIndex = 10;
            this.dtpDob.Value = new System.DateTime(2009, 9, 4, 0, 0, 0, 0);
            // 
            // lblDOb
            // 
            this.lblDOb.AutoSize = true;
            this.lblDOb.Location = new System.Drawing.Point(359, 97);
            this.lblDOb.Name = "lblDOb";
            this.lblDOb.Size = new System.Drawing.Size(85, 13);
            this.lblDOb.TabIndex = 70;
            this.lblDOb.Text = "Date of Birth:";
            // 
            // cmbCreateTitle
            // 
            this.cmbCreateTitle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCreateTitle.FormattingEnabled = true;
            this.cmbCreateTitle.Location = new System.Drawing.Point(146, 3);
            this.cmbCreateTitle.Name = "cmbCreateTitle";
            this.cmbCreateTitle.Size = new System.Drawing.Size(110, 21);
            this.cmbCreateTitle.TabIndex = 1;
            // 
            // lblCreateTitle
            // 
            this.lblCreateTitle.Location = new System.Drawing.Point(15, 3);
            this.lblCreateTitle.Name = "lblCreateTitle";
            this.lblCreateTitle.Size = new System.Drawing.Size(125, 13);
            this.lblCreateTitle.TabIndex = 64;
            this.lblCreateTitle.Text = "Title:*";
            this.lblCreateTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCreateEmail
            // 
            this.txtCreateEmail.Location = new System.Drawing.Point(767, 28);
            this.txtCreateEmail.MaxLength = 50;
            this.txtCreateEmail.Name = "txtCreateEmail";
            this.txtCreateEmail.Size = new System.Drawing.Size(150, 21);
            this.txtCreateEmail.TabIndex = 6;
            // 
            // lblCreateEmail
            // 
            this.lblCreateEmail.Location = new System.Drawing.Point(636, 28);
            this.lblCreateEmail.Name = "lblCreateEmail";
            this.lblCreateEmail.Size = new System.Drawing.Size(120, 13);
            this.lblCreateEmail.TabIndex = 66;
            this.lblCreateEmail.Text = "Email: ";
            this.lblCreateEmail.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCreateLocRoleHeader
            // 
            this.lblCreateLocRoleHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(117)))), ((int)(((byte)(186)))));
            this.lblCreateLocRoleHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblCreateLocRoleHeader.ForeColor = System.Drawing.Color.White;
            this.lblCreateLocRoleHeader.Location = new System.Drawing.Point(-1, 120);
            this.lblCreateLocRoleHeader.Name = "lblCreateLocRoleHeader";
            this.lblCreateLocRoleHeader.Size = new System.Drawing.Size(997, 18);
            this.lblCreateLocRoleHeader.TabIndex = 49;
            this.lblCreateLocRoleHeader.Text = "Location wise Role(s)";
            this.lblCreateLocRoleHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDesignation
            // 
            this.lblDesignation.Location = new System.Drawing.Point(15, 52);
            this.lblDesignation.Name = "lblDesignation";
            this.lblDesignation.Size = new System.Drawing.Size(125, 13);
            this.lblDesignation.TabIndex = 72;
            this.lblDesignation.Text = "Designation: ";
            this.lblDesignation.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCreateDesignation
            // 
            this.txtCreateDesignation.Location = new System.Drawing.Point(146, 52);
            this.txtCreateDesignation.MaxLength = 50;
            this.txtCreateDesignation.Name = "txtCreateDesignation";
            this.txtCreateDesignation.Size = new System.Drawing.Size(110, 21);
            this.txtCreateDesignation.TabIndex = 7;
            // 
            // errCreateUser
            // 
            this.errCreateUser.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errCreateUser.ContainerControl = this;
            // 
            // cmbcenterspec
            // 
            this.cmbcenterspec.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbcenterspec.FormattingEnabled = true;
            this.cmbcenterspec.Items.AddRange(new object[] {
            "Select",
            "HO",
            "WH",
            "BO",
            "PUC"});
            this.cmbcenterspec.Location = new System.Drawing.Point(91, 147);
            this.cmbcenterspec.Name = "cmbcenterspec";
            this.cmbcenterspec.Size = new System.Drawing.Size(78, 21);
            this.cmbcenterspec.TabIndex = 71;
            this.cmbcenterspec.SelectedIndexChanged += new System.EventHandler(this.cmbcenterspec_SelectedIndexChanged);
            // 
            // frmUserRegistration
            // 
            this.AcceptButton = this.btnSearch;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1013, 707);
            this.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "frmUserRegistration";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "User Management";
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Normal;
            this.Load += new System.EventHandler(this.frmUserRegistration_Load);
            this.tabPageSearch.ResumeLayout(false);
            this.pnlSearchHeader.ResumeLayout(false);
            this.pnlSearchHeader.PerformLayout();
            this.tabPageCreate.ResumeLayout(false);
            this.pnlCreateDetails.ResumeLayout(false);
            this.pnlCreateDetails.PerformLayout();
            this.pnlDetailsHeader.ResumeLayout(false);
            this.pnlDetailsHeader.PerformLayout();
            this.pnlSearchGrid.ResumeLayout(false);
            this.grpBoxCreateRoles.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCreateLocationRoles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearchUsers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errCreateUser)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtCreatePassword;
        private System.Windows.Forms.TextBox txtCreateUserName;
        private System.Windows.Forms.Label lblCreatePassword;
        private System.Windows.Forms.Label lblCreateUserName;
        private System.Windows.Forms.TextBox txtCreateFirstName;
        private System.Windows.Forms.Label lblCreateFirstName;
        private System.Windows.Forms.TextBox txtCreateLastName;
        private System.Windows.Forms.Label lblCreateLastName;
        private System.Windows.Forms.TextBox txtCreateAddress2;
        private System.Windows.Forms.TextBox txtCreateAddress1;
        private System.Windows.Forms.Label lblCreateAddress2;
        private System.Windows.Forms.Label lblCreateAddress1;
        private System.Windows.Forms.TextBox txtCreateAddress3;
        private System.Windows.Forms.Label lblCreateAddress3;
        private System.Windows.Forms.TextBox txtCreateFax;
        private System.Windows.Forms.Label lblCreateFax;
        private System.Windows.Forms.TextBox txtCreateMobile;
        private System.Windows.Forms.TextBox txtCreatePhone;
        private System.Windows.Forms.Label lblCreateMobile;
        private System.Windows.Forms.Label lblCreatePhone;
        private System.Windows.Forms.Label lblCreateCountry;
        private System.Windows.Forms.ComboBox cmbCreateCountry;
        private System.Windows.Forms.ComboBox cmbCreateState;
        private System.Windows.Forms.Label lblCreateState;
        private System.Windows.Forms.ComboBox cmbCreateCity;
        private System.Windows.Forms.Label lblCreateCity;
        private System.Windows.Forms.TextBox txtCreatePinCode;
        private System.Windows.Forms.Label lblCreatePinCode;
        private System.Windows.Forms.ComboBox cmbCreateLocation;
        private System.Windows.Forms.Label lblCreateLocation;
        private System.Windows.Forms.CheckedListBox chkListBoxRoles;
        private System.Windows.Forms.GroupBox grpBoxCreateRoles;
        private System.Windows.Forms.Button btnCreateSelAllRoles;
        private System.Windows.Forms.Button btnCreateSelNoneRoles;
        private System.Windows.Forms.DataGridView dgvCreateLocationRoles;
        private System.Windows.Forms.Button btnCreateAddLocRole;
 
        private System.Windows.Forms.TextBox txtSearchMobile;
        private System.Windows.Forms.Label lblSearchMobile;
        private System.Windows.Forms.TextBox txtSearchAddress1;
        private System.Windows.Forms.Label lblSearchAddress1;
        private System.Windows.Forms.TextBox txtSearchLastName;
        private System.Windows.Forms.TextBox txtSearchFirstName;
        private System.Windows.Forms.Label lblSearchLastName;
        private System.Windows.Forms.Label lblSearchFirstName;
        private System.Windows.Forms.TextBox txtSearchUserName;
        private System.Windows.Forms.Label lblSearchUserName;
        private System.Windows.Forms.TextBox txtSearchPinCode;
        private System.Windows.Forms.Label lblSearchPinCode;
        private System.Windows.Forms.ComboBox cmbSearchCity;
        private System.Windows.Forms.Label lblSearchCity;
        private System.Windows.Forms.ComboBox cmbSearchState;
        private System.Windows.Forms.Label lblSearchState;
        private System.Windows.Forms.ComboBox cmbSearchCountry;
        private System.Windows.Forms.Label lblSearchCountry;
        private System.Windows.Forms.ComboBox cmbSearchUserStatus;
        private System.Windows.Forms.Label lblSearchUserStatus;
        private System.Windows.Forms.DataGridView dgvSearchUsers;
        private System.Windows.Forms.ComboBox cmbCreateStatus;
        private System.Windows.Forms.Label lblCreateStatus;
        private System.Windows.Forms.Button btnRemoveLocRole;
        private System.Windows.Forms.DateTimePicker dtpDob;
        private System.Windows.Forms.Label lblDOb;
        private System.Windows.Forms.ComboBox cmbCreateTitle;
        private System.Windows.Forms.Label lblCreateTitle;
        private System.Windows.Forms.TextBox txtCreateEmail;
        private System.Windows.Forms.Label lblCreateEmail;
        private System.Windows.Forms.TextBox txtCreateDesignation;
        private System.Windows.Forms.Label lblDesignation;
        private System.Windows.Forms.Label lblCreateLocRoleHeader;
        private System.Windows.Forms.ErrorProvider errCreateUser;
        private System.Windows.Forms.ComboBox cmbcenterspec;
    }
}