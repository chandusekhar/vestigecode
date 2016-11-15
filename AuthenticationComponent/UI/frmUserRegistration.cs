using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CoreComponent.Core.BusinessObjects;
using AuthenticationComponent.BusinessObjects;

namespace AuthenticationComponent.UI
{
    public partial class frmUserRegistration : CoreComponent.Core.UI.Hierarchy
    {
        #region Global Variables

        User m_objUser;
        List<User> m_listOfUsers = new List<User>();
        List<LocationRole> m_listOfLocationRole = new List<LocationRole>();
        int m_userId = 0;
        DateTime m_modifiedDate = new DateTime(); //for concurrency control check

        private Boolean m_isPasswordModifyAvailable = false;
        //private string strLocationCode = Common.LocationCode;


        #region Constants

        private const string MODULE_CODE = "ADM02";
        private const int MODIFIY_PASSWORD_TRUE= 1;
        private const int MODIFIY_PASSWORD_FALSE = 0;

        #endregion Constants

        #endregion Global Variables
        public static List<LocationRole> Locationlist
        {
            get;
            set;
        }

        #region Constructor

        public frmUserRegistration()
        {
            InitializeComponent();

            lblPageTitle.Text = "User Management";

            //Search Tab
            InitializeControls_SearchUser();

            //Create/Update tab
            InitializeControls_CreateUser();
        }

        #endregion Constructor

        #region Local Functions

        /// <summary>
        /// Method to Initialize the Controls in Search mode.
        /// </summary>
        private void InitializeControls_SearchUser()
        {
            try
            {
                this.txtSearchUserName.Focus();
                this.dtpDob.MaxDate = DateTime.Today;
                //Bind Country Combo                
                Common.FillComboBox(cmbSearchCountry, Common.ParameterType.Country, 0);

                //Locations
                //this.BindLocationComboBox(cmbSearchLocation);

                //Status Combo
                Common.BindParamComboBox(cmbSearchUserStatus, Common.SEARCH_STATUS, 0, 0, 0); //SEARCH_STATUS

                cmbSearchUserStatus.SelectedValue = 1;
                //Define GridView for Search Users (dgvSearchUsers)
                this.DefineGridView_SearchUsers();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method to define grid view on Search form. i.e.
        /// add columns to grid view and set their properties.
        /// </summary>
        private void DefineGridView_SearchUsers()
        {
            try
            {
                dgvSearchUsers.AutoGenerateColumns = false;
                dgvSearchUsers.DataSource = null;
                DataGridView dgvSearchUsersTemp =
                    Common.GetDataGridViewColumns(dgvSearchUsers,
                    Environment.CurrentDirectory + User.GRIDVIEW_DEFINITION_XML_PATH);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method to bind search User grid.
        /// </summary>
        private void BindGridView_SearchUsers()
        {
            try
            {
                dgvSearchUsers.DataSource = new List<User>();
                if (m_listOfUsers.Count > 0)
                {
                    dgvSearchUsers.DataSource = m_listOfUsers;
                    dgvSearchUsers.Rows[0].Selected = false;
                }
                else
                {
                    MessageBox.Show(Common.GetMessage("8002"), Common.GetMessage("10001"),
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method to fill form data in update mode.
        /// </summary>
        private void LoadFormData_UpdateMode()
        {
            try
            {
                DisableValidation();
                txtCreateUserName.Enabled = false;
                txtCreateUserName.Text = m_objUser.UserName;
                if (m_isPasswordModifyAvailable == true)
                {
                    txtCreatePassword.Enabled = true;
                    
                }
                else
                {
                    txtCreatePassword.Enabled = false;
                    //txtCreatePassword.Text = "**********"; //As masked text
                }
                txtCreatePassword.Text =m_objUser.DecryptPassword(m_objUser.Password);               

                txtCreateFirstName.Text = m_objUser.FirstName;
                txtCreateLastName.Text = m_objUser.LastName;
                cmbCreateStatus.SelectedValue = m_objUser.Status;

                txtCreateAddress1.Text = m_objUser.Address1;
                txtCreateAddress2.Text = m_objUser.Address2;
                txtCreateAddress3.Text = m_objUser.Address3;

                txtCreatePhone.Text = m_objUser.PhoneNumber1;
                txtCreateMobile.Text = m_objUser.Mobile1;
                txtCreateFax.Text = m_objUser.Fax1;
                txtCreateEmail.Text = m_objUser.Email1;

                cmbCreateCountry.SelectedValue = m_objUser.CountryId;
                cmbCreateState.SelectedValue = m_objUser.StateId;
                cmbCreateCity.SelectedValue = m_objUser.CityId;
                cmbCreateTitle.SelectedValue = m_objUser.Title;
                dtpDob.Value =Convert.ToDateTime(m_objUser.Dob);
                txtCreateDesignation.Text = m_objUser.Designation;
                txtCreatePinCode.Text = m_objUser.PinCode;
                m_modifiedDate = Convert.ToDateTime(m_objUser.ModifiedDate);

                //Keep reset to Location-Role area.
                cmbCreateLocation.SelectedIndex = 0;
                chkListBoxRoles.DataSource = null;
                BindRolesList(chkListBoxRoles);

                //Bind Grid for Location-Roles 
                m_listOfLocationRole = m_objUser.LocationRoles;
                BindLocationRoleGrid();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        /// <summary>
        /// Method to Initialize the Controls in Create mode.
        /// </summary>
        private void InitializeControls_CreateUser()
        {
            try
            {
                this.txtCreateUserName.Focus();

                //Status Combo
                DateTime dtAssignDate = Convert.ToDateTime( DateTime.Now.ToString(Common.DATE_TIME_FORMAT));
                 dtpDob.MaxDate = dtAssignDate;
                 dtpDob.Value = dtAssignDate;
                Common.BindParamComboBox(cmbCreateStatus, Common.STATUS, 0, 0, 0);
                Common.BindParamComboBox(cmbCreateTitle, "TITLE", 0, 0, 0);
                cmbCreateStatus.SelectedValue = 1;

                //Bind Country Combo                
                Common.FillComboBox(cmbCreateCountry, Common.ParameterType.Country, 0);

                //Locations
                this.BindLocationComboBox(cmbCreateLocation);

                //Roles
                this.BindRolesList(chkListBoxRoles);

                //Define GridView
                this.DefineGridView();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method to reset the form in create mode.
        /// </summary>
        private void ResetForm_CreateMode(bool bolChangeTab)
        {
            try
            {
                txtCreateUserName.Text = string.Empty;
                txtCreateUserName.Enabled = true;

                txtCreatePassword.Text = string.Empty;
                txtCreatePassword.Enabled = true;

                txtCreateFirstName.Text = string.Empty;
                txtCreateLastName.Text = string.Empty;

                //cmbCreateStatus.SelectedIndex = 0;
                if(cmbCreateStatus.Items.Count>0)
                    cmbCreateStatus.SelectedValue = 1;

                txtCreateAddress1.Text = string.Empty;
                txtCreateAddress2.Text = string.Empty;
                txtCreateAddress3.Text = string.Empty;

                txtCreatePhone.Text = string.Empty;
                txtCreateMobile.Text = string.Empty;
                txtCreateFax.Text = string.Empty;
                txtCreateEmail.Text = string.Empty;
                txtCreateDesignation.Text = string.Empty;
                cmbCreateTitle.SelectedIndex = 0;

                cmbCreateCountry.SelectedIndex = 0;
                cmbCreateState.SelectedIndex = 0;
                cmbCreateCity.SelectedIndex = 0;
                dtpDob.Value = DateTime.Today;
                dtpDob.Checked = false;
                txtCreatePinCode.Text = string.Empty;

                cmbCreateLocation.SelectedIndex = 0;
                chkListBoxRoles.DataSource = null;
                BindRolesList(chkListBoxRoles);

                dgvCreateLocationRoles.DataSource = null;

                m_objUser = new User();
                m_listOfLocationRole = new List<LocationRole>();
                if(bolChangeTab)
                tabControlHierarchy.SelectedIndex = 1;
               
                tabControlHierarchy.TabPages[1].Text = Common.TAB_CREATE_MODE;
                m_userId = 0; //Ready for Create mode.
                m_modifiedDate = Convert.ToDateTime(new DateTime(1900, 1, 1).ToString(Common.DATE_TIME_FORMAT));

                //InitializeControls_CreateUser();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method to bind the location combo.
        /// </summary>
        /// <param name="cmbName">Pass ComboBox name.</param>
        public void BindLocationComboBox(ComboBox cmbName)
        {
            try
            {
                //cmbName.DataSource = LocationRole.GetAllLocations();
                Locationlist = LocationRole.GetAllLocations();
                cmbName.ValueMember = Common.LOCATION_ID;
                cmbName.DisplayMember = Common.LOCATION_NAME;
                cmbName.SelectedIndex = 0;
                cmbcenterspec.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method to bind Role_CheckedListBox.
        /// </summary>
        /// <param name="chkListBox">Pass CheckedListBox control.</param>
        public void BindRolesList(CheckedListBox chkListBox)
        {
            try
            {
                chkListBox.Items.Clear();
                chkListBox.DataSource = Role.GetAllRoles(); ;
                chkListBox.ValueMember = Common.ROLE_ID;
                chkListBox.DisplayMember = Common.ROLE_NAME;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Method to define grid view on Craete form. i.e.
        /// add columns to grid view and set their properties.
        /// </summary>
        private void DefineGridView()
        {
            try
            {
                dgvCreateLocationRoles.AutoGenerateColumns = false;
                dgvCreateLocationRoles.DataSource = null;
                DataGridView dgvCreateLocationRolesTemp =
                    Common.GetDataGridViewColumns(dgvCreateLocationRoles,
                    Environment.CurrentDirectory + User.GRIDVIEW_DEFINITION_XML_PATH);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method to bind location role grid.
        /// </summary>
        private void BindLocationRoleGrid()
        {
            try
            {
                dgvCreateLocationRoles.DataSource = new List<LocationRole>();
                if (m_listOfLocationRole.Count > 0)
                {
                    //LINQ
                    var temp = (from p in m_listOfLocationRole where p.LocationId != 0 && p.RoleId != 0 select p);
                    List<LocationRole> tempListOfLocRole = new List<LocationRole>();
                    tempListOfLocRole = temp.ToList();

                    if (tempListOfLocRole.Count > 0)
                    {
                        dgvCreateLocationRoles.DataSource = tempListOfLocRole;
                        dgvCreateLocationRoles.Rows[0].Selected = false;
                        //Make global Location-Role list same as one that is assigned to Grid. (tempListOfLocRole)
                        m_listOfLocationRole = tempListOfLocRole;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Event Related Functions

        /// <summary>
        /// Method to validate Location wise Role assignment.
        /// </summary>
        /// <returns></returns>
        private bool ValidateLocationRoleArea()
        {
            //if (cmbCreateLocation.SelectedIndex == 0)
            //{
            //    MessageBox.Show(Common.GetMessage("VAL0011"), Common.GetMessage("10001"),
            //        MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    cmbCreateLocation.Focus();
            //    return false;
            //}
            //else if (cmbCreateLocation.SelectedIndex > 0)
            //{
            //    if (chkListBoxRoles.CheckedItems.Count == 0)
            //    {
            //        MessageBox.Show(Common.GetMessage("VAL0012"), Common.GetMessage("10001"),
            //            MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        chkListBoxRoles.Focus();
            //        return false;
            //    }
            //    else
            //    {
            //        return true;
            //    }
            //}
            //else
            //{
                return true;
            //}

        }
        /// <summary>
        /// Method to add selected location and checked roles in the grid view
        /// of UserRegistration form's create/update mode.
        /// </summary>
        private void AddLocationRoleInGridView()
        {

            LocationRole objLocRole;
            StringBuilder sbMessage;
            Boolean bolLocationAlreadyAdded = false;
            try
            {
                sbMessage = new StringBuilder();
                //Read checked roles from chkListBoxRoles
                foreach (Role tRole in chkListBoxRoles.CheckedItems)
                {
                    //Find duplicate record in list, by delegate
                    LocationRole lrm = m_listOfLocationRole.Find(delegate(LocationRole lr)
                    {
                        return lr.LocationId == Convert.ToInt32(cmbCreateLocation.SelectedValue) &&
                               lr.RoleId == tRole.RoleId;
                    });
                    if (lrm == null) //If same LocationId and RoleId do not exist.
                    {
                        objLocRole = new LocationRole();
                        objLocRole.LocationId = Convert.ToInt32(cmbCreateLocation.SelectedValue);
                        objLocRole.LocationName = cmbCreateLocation.Text;
                        objLocRole.RoleId = tRole.RoleId;
                        objLocRole.RoleName = tRole.RoleName;

                        //Add one by one objLocRole object to m_objListLocRole list
                        m_listOfLocationRole.Add(objLocRole);
                    }
                    else
                    {
                        if (!bolLocationAlreadyAdded)
                        {
                            sbMessage.Append("Location Name: " +  lrm.LocationName + Environment.NewLine +
                                               Environment.NewLine + "Role Name:"+ Environment.NewLine +
                                              "  -".PadLeft(15) + lrm.RoleName + Environment.NewLine);
                            bolLocationAlreadyAdded = true;
                        }
                        else
                            sbMessage.Append("  -".PadLeft(15) + lrm.RoleName + Environment.NewLine);
                    }
                }if (sbMessage.Length>0)
                    MessageBox.Show(sbMessage.ToString() + Common.GetMessage("INF0057"), Common.GetMessage("10001"),
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //Display in grid
                BindLocationRoleGrid();
                //btnCreateAddLocRole.Enabled = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method to valiadte user data.
        /// </summary>
        /// <returns></returns>
        private StringBuilder ValidateUserData()
        {
            //CheckEmail

            if (!Validators.IsValidEmailID(txtCreateEmail.Text, true))
            {
                Validators.SetErrorMessage(errCreateUser, txtCreateEmail, "INF0010", lblCreateEmail.Text);
            }
            else
            {
                Validators.SetErrorMessage(errCreateUser, txtCreateEmail);
            
            }
           

            //txtCreateUserName
            if (Validators.CheckForEmptyString(txtCreateUserName.Text.Trim().Length))
            {
                Validators.SetErrorMessage(errCreateUser, txtCreateUserName, "INF0019", lblCreateUserName.Text);
            }
            ////Valid input for UserName
            //else if (!Validators.IsAlphaNumeric(txtCreateUserName.Text.Trim()))
            //{
            //    //errCreateUser.SetError(txtCreateUserName, Common.GetMessage("VAL0018"));
            //    Validators.SetErrorMessage(errCreateUser, txtCreateUserName, "VAL0018", lblCreateUserName.Text);
            //}
            else
            {
                Validators.SetErrorMessage(errCreateUser, txtCreateUserName);
            }

            //txtCreatePassword
            if (Validators.CheckForEmptyString(txtCreatePassword.Text.Trim().Length))
            {
                Validators.SetErrorMessage(errCreateUser, txtCreatePassword, "INF0019", lblCreatePassword.Text);
            }
            else if (!Validators.RangeValidator(txtCreatePassword.Text.Trim().Length,
                6, 20))
            {
                errCreateUser.SetError(txtCreatePassword, Common.GetMessage("VAL0014", "6", "20"));
            }
            else
            {
                Validators.SetErrorMessage(errCreateUser, txtCreatePassword);
            }

            //txtCreateFirstName
            if (Validators.CheckForEmptyString(txtCreateFirstName.Text.Trim().Length))
            {
                Validators.SetErrorMessage(errCreateUser, txtCreateFirstName, "INF0019", lblCreateFirstName.Text);
            }
            ////Valid input for First Name
            //else if (!Validators.IsAlphaSpace(txtCreateFirstName.Text.Trim()))
            //{
            //    //errCreateUser.SetError(txtCreateFirstName, Common.GetMessage("VAL0017"));
            //    Validators.SetErrorMessage(errCreateUser, txtCreateFirstName, "VAL0017", lblCreateFirstName.Text);
            //}
            else
            {
                Validators.SetErrorMessage(errCreateUser, txtCreateFirstName);
            }

            ////Valid input for Last Name
            //if (!Validators.IsAlphaSpace(txtCreateLastName.Text.Trim()))
            //{
            //    Validators.SetErrorMessage(errCreateUser, txtCreateLastName, "VAL0017", lblCreateLastName.Text);
            //}
            //else
            //{
            //    Validators.SetErrorMessage(errCreateUser, txtCreateLastName);
            //}

            //cmbCreateStatus
            if (Validators.CheckForSelectedValue(cmbCreateStatus.SelectedIndex))
            {
                Validators.SetErrorMessage(errCreateUser, cmbCreateStatus, "INF0026", lblCreateStatus.Text);
            }
            else
            {
                Validators.SetErrorMessage(errCreateUser, cmbCreateStatus);
            }

            //txtCreateAddress1
            if (Validators.CheckForEmptyString(txtCreateAddress1.Text.Trim().Length))
            {
                Validators.SetErrorMessage(errCreateUser, txtCreateAddress1, "INF0019", lblCreateAddress1.Text);
            }
            else
            {
                Validators.SetErrorMessage(errCreateUser, txtCreateAddress1);
            }
            //txtCreateDesignation
            if (!Validators.CheckForEmptyString(txtCreateDesignation.Text.Trim().Length))
            {
                if (!Validators.IsAlphaSpace(txtCreateDesignation.Text))
                {
                    Validators.SetErrorMessage(errCreateUser, txtCreateDesignation, "INF0010", lblDesignation.Text);
                }
                else
                {
                    Validators.SetErrorMessage(errCreateUser, txtCreateDesignation);
                }
            }
          

            //txtCreateMobile
            if (Validators.CheckForEmptyString(txtCreateMobile.Text.Trim().Length))
            {
                Validators.SetErrorMessage(errCreateUser, txtCreateMobile, "INF0019", lblCreateMobile.Text);
            }
            //Valid input for Mobile
            else if (!Validators.IsNumeric(txtCreateMobile.Text.Trim()))
            {
                Validators.SetErrorMessage(errCreateUser, txtCreateMobile, "VAL0021", lblCreateMobile.Text);
            }
            else
            {
                Validators.SetErrorMessage(errCreateUser, txtCreateMobile);
            }

            //cmbCreateCountry
            if (Validators.CheckForSelectedValue(cmbCreateCountry.SelectedIndex))
            {
                Validators.SetErrorMessage(errCreateUser, cmbCreateCountry, "INF0026", lblCreateCountry.Text);
            }
            else
            {
                Validators.SetErrorMessage(errCreateUser, cmbCreateCountry);
            }

            //cmbCreateState
            if (Validators.CheckForSelectedValue(cmbCreateState.SelectedIndex))
            {
                Validators.SetErrorMessage(errCreateUser, cmbCreateState, "INF0026", lblCreateState.Text);
            }
            else
            {
                Validators.SetErrorMessage(errCreateUser, cmbCreateState);
            }

            //cmbCreateCity
            if (Validators.CheckForSelectedValue(cmbCreateCity.SelectedIndex))
            {
                Validators.SetErrorMessage(errCreateUser, cmbCreateCity, "INF0026", lblCreateCity.Text);
            }
            else
            {
                Validators.SetErrorMessage(errCreateUser, cmbCreateCity);
            }

            //Valid input for Pin Code
            if (!Validators.IsNumeric(txtCreatePinCode.Text.Trim()))
            {
                Validators.SetErrorMessage(errCreateUser, txtCreatePinCode, "VAL0021", lblCreatePinCode.Text + " ");
            }
            else
            {
                Validators.SetErrorMessage(errCreateUser, txtCreatePinCode);
            }

            if (Validators.CheckForSelectedValue(cmbCreateTitle.SelectedIndex))
            {
                Validators.SetErrorMessage(errCreateUser, cmbCreateTitle, "INF0026", lblCreateTitle.Text);
            }
            else
            {
                Validators.SetErrorMessage(errCreateUser, cmbCreateState);
            }


            //Append all error msgs
            StringBuilder sbError = new StringBuilder();
            CheckBlankMessage(Validators.GetErrorMessage(errCreateUser, txtCreateUserName),sbError);
           // sbError.AppendLine();
            CheckBlankMessage(Validators.GetErrorMessage(errCreateUser, txtCreatePassword), sbError);
            //sbError.AppendLine();
            CheckBlankMessage(Validators.GetErrorMessage(errCreateUser, txtCreateFirstName), sbError);
           // sbError.AppendLine();
            //sbError.Append(Validators.GetErrorMessage(errCreateUser, txtCreateLastName));
            //sbError.AppendLine();
            CheckBlankMessage(Validators.GetErrorMessage(errCreateUser, cmbCreateStatus), sbError);
           // sbError.AppendLine();
            CheckBlankMessage(Validators.GetErrorMessage(errCreateUser, txtCreateAddress1), sbError);
           // sbError.AppendLine();
            CheckBlankMessage(Validators.GetErrorMessage(errCreateUser, txtCreateMobile), sbError);
            //sbError.AppendLine();
            CheckBlankMessage(Validators.GetErrorMessage(errCreateUser, cmbCreateCountry), sbError);
            //sbError.AppendLine();
            CheckBlankMessage(Validators.GetErrorMessage(errCreateUser, cmbCreateState), sbError);
            //sbError.AppendLine();
            CheckBlankMessage(Validators.GetErrorMessage(errCreateUser, cmbCreateCity), sbError);
           // sbError.AppendLine();
            CheckBlankMessage(Validators.GetErrorMessage(errCreateUser, txtCreatePinCode), sbError);

            CheckBlankMessage(Validators.GetErrorMessage(errCreateUser, txtCreateDesignation), sbError);
            //sbError.AppendLine();
            CheckBlankMessage(Validators.GetErrorMessage(errCreateUser, txtCreateEmail), sbError);
            sbError.AppendLine();

            return Common.ReturnErrorMessage(sbError);
        }
        /// <summary>
        /// Method to save user's data in create/update mode.
        /// </summary>
        /// 
        private void CheckBlankMessage(string strMessage, StringBuilder sb)
        {
            if (strMessage != "")
            {
                sb.Append(strMessage);
                sb.AppendLine();
            }
        }
        private void SaveUserData()
        {
            try
            {
                #region Validation Code

                StringBuilder sbError = new StringBuilder();
                sbError = ValidateUserData();

                if (!sbError.ToString().Trim().Equals(string.Empty))
                {
                    MessageBox.Show(sbError.ToString(), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (m_listOfLocationRole == null || m_listOfLocationRole.Count == 0)
                {
                    MessageBox.Show(Common.GetMessage("INF0200"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                #endregion Validation Code

                if (sbError.ToString().Trim().Equals(string.Empty))
                {
                    string msg=string.Empty;
                    if (m_userId > 0)
                        msg = "Edit";
                    else
                        msg = "Save";

                    DialogResult saveResult = MessageBox.Show(Common.GetMessage("5010", msg), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (saveResult == DialogResult.Yes)
                    {

                        m_objUser = new User();
                        m_objUser.UserId = m_userId; // Global variable
                        m_objUser.UserName = txtCreateUserName.Text.Trim();
                        
                        m_objUser.PasswordModifiedTrueFalse = frmUserRegistration.MODIFIY_PASSWORD_FALSE;
                       
                        m_objUser.Password = m_objUser.EncryptPassword(txtCreatePassword.Text.Trim());
                        
                        m_objUser.Title = Convert.ToInt32(cmbCreateTitle.SelectedValue);
                        m_objUser.FirstName = txtCreateFirstName.Text.Trim();
                        m_objUser.MiddleName = string.Empty;
                        m_objUser.LastName = txtCreateLastName.Text.Trim();
                        m_objUser.Status = Convert.ToInt32(cmbCreateStatus.SelectedValue);

                        DateTime dtDob = dtpDob.Checked == true ? Convert.ToDateTime(dtpDob.Value) : Common.DATETIME_NULL;
                        m_objUser.Dob = Convert.ToDateTime(dtDob).ToString(Common.DATE_TIME_FORMAT);

                        m_objUser.Designation = txtCreateDesignation.Text.Trim();
                        if (m_userId == 0)
                        {
                            m_objUser.CreatedBy = Authenticate.LoggedInUser.UserId;
                            m_objUser.CreatedDate = Common.DATETIME_CURRENT.ToString(Common.DATE_TIME_FORMAT);
                            m_objUser.ModifiedDate = Convert.ToDateTime(new DateTime(1900, 1, 1)).ToString(Common.DATE_TIME_FORMAT);
                        }
                        else //m_userId > 0
                        {
                            m_objUser.ModifiedDate = Convert.ToDateTime(m_modifiedDate).ToString(Common.DATE_TIME_FORMAT); ;
                        }

                        m_objUser.Address1 = txtCreateAddress1.Text.Trim();
                        m_objUser.Address2 = txtCreateAddress2.Text.Trim();
                        m_objUser.Address3 = txtCreateAddress3.Text.Trim();
                        //m_objUser.Address4 = string.Empty;

                        m_objUser.PhoneNumber1 = txtCreatePhone.Text.Trim();
                        m_objUser.PhoneNumber2 = string.Empty;
                        m_objUser.Mobile1 = txtCreateMobile.Text.Trim();
                        m_objUser.Mobile2 = string.Empty;
                        m_objUser.Fax1 = txtCreateFax.Text.Trim();
                        m_objUser.Fax2 = string.Empty;
                        m_objUser.Email1 = txtCreateEmail.Text.Trim();
                        m_objUser.Email2 = string.Empty;
                        m_objUser.Website = string.Empty;

                        m_objUser.CountryId = Convert.ToInt32(cmbCreateCountry.SelectedValue);
                        m_objUser.StateId = Convert.ToInt32(cmbCreateState.SelectedValue);
                        m_objUser.CityId = Convert.ToInt32(cmbCreateCity.SelectedValue);
                        m_objUser.PinCode = txtCreatePinCode.Text.Trim();

                        m_objUser.LocationRoles = m_listOfLocationRole;

                        string errMsg = string.Empty;
                        bool retVal = m_objUser.UserSave(Common.ToXml(m_objUser), User.USER_SAVE, ref errMsg);
                        if (retVal)
                        {
                            MessageBox.Show(Common.GetMessage("8001"), Common.GetMessage("10001"),
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                            if (m_userId == 0) //Create mode
                            {
                                //In case of 'Create', after save, reset the form.
                                ResetForm_CreateMode(true);
                                //After successful Update, Refresh the default Search result.
                                SearchUserData();
                            }
                            else //Update mode.
                            {
                                //After successful Update, Refresh the default Search result.
                                ResetForm_CreateMode(true);
                                SearchUserData();
                            }
                        }
                        else if (errMsg.Equals("VAL0007"))//Duplicate data
                        {
                            MessageBox.Show(Common.GetMessage(errMsg,
                                lblCreateUserName.Text.Substring(0, lblCreateUserName.Text.Trim().Length - 2)),
                                Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else if (errMsg.Equals("INF0022"))//Concurrency
                        {
                            MessageBox.Show(Common.GetMessage("INF0022"), Common.GetMessage("10001"),
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            Common.LogException(new Exception(errMsg));
                            MessageBox.Show(Common.GetMessage("2003"), Common.GetMessage("10001"),
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                   
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion Event Related Functions

        #endregion Local Functions

        /// <summary>
        /// Code to execute at loading of this form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmUserRegistration_Load(object sender, EventArgs e)
        {
            try
            {
               
                //Set Modify Password Right to Login User
                m_isPasswordModifyAvailable = Authenticate.IsFunctionAccessible(Authenticate.LoggedInUser.UserName, Common.LocationCode, frmUserRegistration.MODULE_CODE, Common.FUNCTIONCODE_PWDMODIFY);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"),
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Code to bind 'STATE' combo.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbCreateCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //Bind State Combo            
                if (cmbCreateCountry.SelectedIndex > 0)
                {
                    Common.FillComboBox(cmbCreateState, Common.ParameterType.State,
                        Convert.ToInt32(cmbCreateCountry.SelectedValue));
                }
                else
                {
                    Common.FillComboBox(cmbCreateState, Common.ParameterType.State, -1);
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"),
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Code to bind 'CITY' combo.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbCreateState_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //Bind City Combo            
                if (cmbCreateState.SelectedIndex > 0)
                {
                    Common.FillComboBox(cmbCreateCity, Common.ParameterType.City,
                        Convert.ToInt32(cmbCreateState.SelectedValue));
                }
                else
                {
                    Common.FillComboBox(cmbCreateCity, Common.ParameterType.City, -1);
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"),
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Code to select all roles in one click at role list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreateSelAllRoles_Click(object sender, EventArgs e)
        {
            try
            {
                for (int index = 0; index < chkListBoxRoles.Items.Count; index++)
                {
                    chkListBoxRoles.SetItemChecked(index, true);
                }
                //btnCreateSelAllRoles.Enabled = false;
                //btnCreateSelNoneRoles.Enabled = true;
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"),
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Code to de-select all roles in one click at role list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreateSelNoneRoles_Click(object sender, EventArgs e)
        {
            try
            {
                for (int index = 0; index < chkListBoxRoles.Items.Count; index++)
                {
                    chkListBoxRoles.SetItemChecked(index, false);
                }
                //btnCreateSelNoneRoles.Enabled = false;
                //btnCreateSelAllRoles.Enabled = true;
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"),
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Code to add selected location and checked roles in the grid view.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreateAddLocRole_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateLocationRoleArea())
                {
                    this.AddLocationRoleInGridView();
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"),
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Code to save user's data in create/update mode.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                this.SaveUserData();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"),
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Method to hide error providers icon on click of 
        /// reset button.
        /// </summary>
        private void DisableValidation()
        {
            errCreateUser.SetError(cmbCreateTitle, string.Empty);
            errCreateUser.SetError(txtCreateUserName, string.Empty);
            errCreateUser.SetError(txtCreatePassword, string.Empty);
            errCreateUser.SetError(txtCreateFirstName, string.Empty);
            errCreateUser.SetError(cmbCreateStatus, string.Empty);
            errCreateUser.SetError(txtCreateAddress1, string.Empty);
            errCreateUser.SetError(txtCreateMobile, string.Empty);
            errCreateUser.SetError(cmbCreateCountry, string.Empty);
            errCreateUser.SetError(cmbCreateState, string.Empty);
            errCreateUser.SetError(cmbCreateCity, string.Empty);
            errCreateUser.SetError(txtCreateDesignation, string.Empty);
            errCreateUser.SetError(txtCreateEmail, string.Empty);
        }

        /// <summary>
        /// Code to reset the form in create mode.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreateReset_Click(object sender, EventArgs e)
        {
            try
            {
                DisableValidation();
                this.ResetForm_CreateMode(true);
                cmbCreateTitle.Focus();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"),
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// code to execute on value change of location combo.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbCreateLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                for (int index = 0; index < chkListBoxRoles.Items.Count; index++)
                {
                    chkListBoxRoles.SetItemChecked(index, false);
                }
                //btnCreateAddLocRole.Enabled = true;
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"),
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Method to search Users.
        /// </summary>
        private void SearchUserData()
        {
            try
            {
                LocationRole tLocRole = new LocationRole();
                List<LocationRole> lstLocRole = new List<LocationRole>();

                m_objUser = new User();
                m_objUser.UserName = txtSearchUserName.Text.Trim();
                m_objUser.FirstName = txtSearchFirstName.Text.Trim();
                m_objUser.LastName = txtSearchLastName.Text.Trim();
                m_objUser.Address1 = txtSearchAddress1.Text.Trim();
                m_objUser.Mobile1 = txtSearchMobile.Text.Trim();
                m_objUser.CityId = Convert.ToInt32(cmbSearchCity.SelectedValue);
                m_objUser.PinCode = txtSearchPinCode.Text.Trim();
                m_objUser.StateId = Convert.ToInt32(cmbSearchState.SelectedValue);
                m_objUser.CountryId = Convert.ToInt32(cmbSearchCountry.SelectedValue);

                tLocRole.LocationId = Common.INT_DBNULL; // Convert.ToInt32(cmbSearchLocation.SelectedValue);
                lstLocRole.Add(tLocRole);
                m_objUser.LocationRoles = lstLocRole;

                m_objUser.Status = Convert.ToInt32(cmbSearchUserStatus.SelectedValue);

                string errMsg = string.Empty;
                m_listOfUsers = m_objUser.UserSearch(Common.ToXml(m_objUser), User.USER_SEARCH, ref errMsg);

                //Bind Grid
                BindGridView_SearchUsers();
            }
            catch { throw; }
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                SearchUserData();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"),
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbSearchCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //Bind State Combo            
                if (cmbSearchCountry.SelectedIndex > 0)
                {
                    Common.FillComboBox(cmbSearchState, Common.ParameterType.State,
                        Convert.ToInt32(cmbSearchCountry.SelectedValue));
                }
                else
                {
                    Common.FillComboBox(cmbSearchState, Common.ParameterType.State, -1);
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"),
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbSearchState_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //Validation                

                //Bind City Combo            
                if (cmbSearchState.SelectedIndex > 0)
                {
                    Common.FillComboBox(cmbSearchCity, Common.ParameterType.City,
                        Convert.ToInt32(cmbSearchState.SelectedValue));
                }
                else
                {
                    Common.FillComboBox(cmbSearchCity, Common.ParameterType.City, -1);
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"),
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Method to invoke form in update mode of current record.
        /// </summary>
        /// <param name="e"></param>
        private void EditCellContentClick(DataGridViewCellEventArgs e)
        {
           
            
            try
            {

                if (dgvSearchUsers.Columns[e.ColumnIndex].CellType == typeof(DataGridViewImageCell))
                {
                    if (e.RowIndex >= 0) //To leave header row.
                    {
                        //m_isPasswordModifyAvailable = Authenticate.IsFunctionAccessible(Authenticate.LoggedInUser.UserName, Common.LocationCode, frmUserRegistration.MODULE_CODE, Common.FUNCTIONCODE_PWDMODIFY);
                        LoadSearchedUser(e.RowIndex);
                    }
                }
            }
            catch 
            {
                throw; 
            }
            
        }

        private void LoadSearchedUser(int rowIndex)
        {
            tabControlHierarchy.SelectedIndex = 1;
            tabControlHierarchy.TabPages[1].Text = Common.TAB_UPDATE_MODE;
            int userId = Convert.ToInt32(dgvSearchUsers.Rows[rowIndex].Cells[Common.USER_ID].Value);
            m_userId = userId; //Assign this UserId to Global Variable.
            m_objUser = m_listOfUsers.Find(delegate(User user)
            {
                return user.UserId == userId;
            });

            //Load Form data in update mode                        
            LoadFormData_UpdateMode();
        }

        private void dgvSearchUsers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                EditCellContentClick(e);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"),
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Method to reset the form in search mode.
        /// </summary>
        private void ResetForm_SearchMode()
        {
            try
            {
                txtSearchUserName.Text = string.Empty;
                txtSearchFirstName.Text = string.Empty;
                txtSearchLastName.Text = string.Empty;
                txtSearchAddress1.Text = string.Empty;
                txtSearchMobile.Text = string.Empty;
                txtSearchPinCode.Text = string.Empty;

                cmbSearchCountry.SelectedIndex = 0;
                cmbSearchState.SelectedIndex = 0;
                cmbSearchCity.SelectedIndex = 0;
                //cmbSearchLocation.SelectedIndex = 0;

                if (cmbSearchUserStatus.Items.Count > 0)
                    cmbSearchUserStatus.SelectedValue = 1;
              //  cmbSearchUserStatus.SelectedIndex = 0;

                //Reset Grid also.
                dgvSearchUsers.DataSource = null;
                txtSearchUserName.Focus();
            }
            catch { throw; }
        }

        private void btnSearchReset_Click(object sender, EventArgs e)
        {
            try
            {
                ResetForm_SearchMode();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"),
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvCreateLocationRoles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //LocationRole objLocRole;
            //try
            //{
            //    if (dgvCreateLocationRoles.Columns[e.ColumnIndex].CellType == typeof(DataGridViewButtonCell))
            //    {
            //        if (e.RowIndex >= 0) //To leave header row.
            //        {
            //            if (dgvCreateLocationRoles.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()
            //                .Equals(Common.GRID_REMOVE, StringComparison.InvariantCultureIgnoreCase))
            //            {
            //                //If user click on 'Remove' button of Location-Role Grid.
            //                //Remove the particular 'Location-Role' combination record from this Grid.
            //                //Removing any record from this grid, means actually removing that record 
            //                //from its DataSource and then bind again.

            //                //LocationId, Index in Grid = 1.
            //                //RoleId, Index in Grid = 3.

            //                DialogResult result = MessageBox.Show(Common.GetMessage("5004"), 
            //                    Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //                if (result == DialogResult.No)
            //                {
            //                    return;
            //                }
            //                else
            //                {
            //                    int currentLocationId = Convert.ToInt32(dgvCreateLocationRoles.Rows[e.RowIndex].Cells[Common.LOCATION_ID].Value);
            //                    int currentRoleId = Convert.ToInt32(dgvCreateLocationRoles.Rows[e.RowIndex].Cells[Common.ROLE_ID].Value);

            //                    LocationRole tempLocRole = m_listOfLocationRole.Find(delegate(LocationRole lr)
            //                    {
            //                        return lr.LocationId == currentLocationId &&
            //                               lr.RoleId == currentRoleId;
            //                    });
            //                    if (!Equals(tempLocRole, null))
            //                    {
            //                        //Selected LocationId and RoleId found in the 'm_listOfLocationRole'
            //                        //Remove that 'Location-Role' combination from 'm_listOfLocationRole'
            //                        m_listOfLocationRole.Remove(tempLocRole);

            //                        //Refresh the 'Location-Role' grid.
            //                        BindLocationRoleGrid();
            //                    }
            //                }                            
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Common.LogException(ex);
            //    MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"),
            //                MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        /// <summary>
        /// Method to remove checked 'Location-Role' from
        /// dgvCreateLocationRoles grid.
        /// </summary>
        private void RemoveLocationRoleFromGrid()
        {
            List<LocationRole> tempListOfLocRole = new List<LocationRole>();
            try
            {
                //If user click on '<< Remove' button.
                //Remove the particular 'Location-Role' combination record from this Grid.
                //Removing any record from this grid, means actually removing that record 
                //from its DataSource(m_listOfLocationRole) and then bind again.

                if (dgvCreateLocationRoles.Rows.Count > 0)
                {
                    for (int rowIndex = 0; rowIndex < dgvCreateLocationRoles.Rows.Count; rowIndex++)
                    {
                        if (Convert.ToBoolean(dgvCreateLocationRoles[Common.GRID_REMOVE, rowIndex].Value) == true) //CheckBox column.
                        {
                            int currentLocationId = Convert.ToInt32(dgvCreateLocationRoles[Common.LOCATION_ID, rowIndex].Value);
                            int currentRoleId = Convert.ToInt32(dgvCreateLocationRoles[Common.ROLE_ID, rowIndex].Value);

                            LocationRole tLocRole = new LocationRole();
                            tLocRole.LocationId = currentLocationId;
                            tLocRole.RoleId = currentRoleId;

                            //Add checked 'Location-Role' in 'tempListOfLocRole'
                            //'tempListOfLocRole' contains items to be removed from 'm_listOfLocationRole'
                            tempListOfLocRole.Add(tLocRole);
                        }
                    }
                    //Now all checked items from grid has been picked in a temp list.

                    if (tempListOfLocRole.Count > 0) //Count of checked items
                    {
                        DialogResult result = MessageBox.Show(Common.GetMessage("5004"),
                        Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.No)
                        {
                            return;
                        }
                        else //Yes
                        {
                            //Now compare 'm_listOfLocationRole' and 'tempListOfLocRole' and
                            //remove all those items from 'm_listOfLocationRole' which are 
                            //existing in 'tempListOfLocRole'
                            foreach (LocationRole tempLocRole in tempListOfLocRole)
                            {
                                LocationRole tempLR = m_listOfLocationRole.Find(delegate(LocationRole lr)
                                {
                                    return lr.LocationId == tempLocRole.LocationId &&
                                        lr.RoleId == tempLocRole.RoleId;
                                });                                

                                if (!Equals(tempLR, null)) //checked 'Location-Role' found in 'm_listOfLocationRole'
                                {
                                    //checked LocationId and RoleId found in the 'm_listOfLocationRole'
                                    //Remove that 'Location-Role' combination from 'm_listOfLocationRole'
                                    m_listOfLocationRole.Remove(tempLR);
                                }
                            }
                        }
                        //Refresh the 'Location-Role' grid.
                        BindLocationRoleGrid(); 
                    }
                    else
                    {
                        MessageBox.Show(Common.GetMessage("VAL0015"),
                        Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch { throw; }            
        }

        /// <summary>
        /// To remove checked 'Location-Role' from dgvCreateLocationRoles grid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRemoveLocRole_Click(object sender, EventArgs e)
        {
            try
            {
                RemoveLocationRoleFromGrid();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"),
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tabControlHierarchy_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (tabControlHierarchy.TabPages[1].Text.Equals(Common.TAB_UPDATE_MODE))
            {
                DialogResult confirmresult = MessageBox.Show(Common.GetMessage("5011"), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirmresult == DialogResult.Yes)
                {
                    ResetForm_CreateMode(false);
                    DisableValidation();
                    e.Cancel = false;
                }

                else
                {

                    e.Cancel = true;
                }
            }
        }

        private void dgvSearchUsers_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvSearchUsers_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > 0)
            {
                LoadSearchedUser(e.RowIndex);
            }
        }

   
        private void cmbcenterspec_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
            List<LocationRole> lstItem;
            ComboBox cmb = (ComboBox)sender;
            if (cmb.SelectedItem == "HO")
            {
                if (Locationlist != null && Locationlist.Count > 0)
                {
                    var query = from a in Locationlist where a.LocationType.ToUpper() == "HO" select a;
                    lstItem = (List<LocationRole>)query.ToList();
                    if (lstItem.Count > 0)
                    {
                        cmbCreateLocation.DataSource = lstItem;
                        cmbCreateLocation.ValueMember = Common.LOCATION_ID;
                        cmbCreateLocation.DisplayMember = Common.LOCATION_NAME;
                    }
                }
            }
                else if (cmb.SelectedItem == "BO")
                {
                    if (Locationlist != null && Locationlist.Count > 0)
                    {
                        var query = from a in Locationlist where a.LocationType.ToUpper() == "BO" select a;
                        lstItem = (List<LocationRole>)query.ToList();
                        if (lstItem.Count > 0)
                        {
                            cmbCreateLocation.DataSource = lstItem;
                            cmbCreateLocation.ValueMember = Common.LOCATION_ID;
                            cmbCreateLocation.DisplayMember = Common.LOCATION_NAME;
                        }
                    }
                }
                else if (cmb.SelectedItem == "PUC")
                {
                    if (Locationlist != null && Locationlist.Count > 0)
                    {
                        var query = from a in Locationlist where a.LocationType.ToUpper() == "PC" select a;
                        lstItem = (List<LocationRole>)query.ToList();
                        if (lstItem.Count > 0)
                        {
                            cmbCreateLocation.DataSource = lstItem;
                            cmbCreateLocation.ValueMember = Common.LOCATION_ID;
                            cmbCreateLocation.DisplayMember = Common.LOCATION_NAME;
                        }
                    }
                }
                else if (cmb.SelectedItem == "WH")
                {
                    if (Locationlist != null && Locationlist.Count > 0)
                    {
                        var query = from a in Locationlist where a.LocationType.ToUpper() == "WH" select a;
                        lstItem = (List<LocationRole>)query.ToList();
                        if (lstItem.Count > 0)
                        {
                            cmbCreateLocation.DataSource = lstItem;
                            cmbCreateLocation.ValueMember = Common.LOCATION_ID;
                            cmbCreateLocation.DisplayMember = Common.LOCATION_NAME;
                        }
                    }
                }
            
            
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }      
    }
}
