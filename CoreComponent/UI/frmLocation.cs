using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using CoreComponent.UI;
using CoreComponent.BusinessObjects;
using CoreComponent.Core.BusinessObjects;
using CoreComponent.Hierarchies.BusinessObjects;
using AuthenticationComponent.BusinessObjects;

namespace CoreComponent.Hierarchies.UI
{

    public partial class frmLocation : CoreComponent.Core.UI.Transaction
    {
        #region Variables
        List<LocationHierarchy> m_locList;
        LocationHierarchy m_LocationHierarchy;
        List<Contact> m_lstContact;
        List<Contact> m_lstRemovedContact = new List<Contact>();
        List<string> m_lstContactString;
        List<string> m_lstModifiedContactId = new List<string>();
        string m_contactModifiedDate = string.Empty;

        List<LocationTerminal> m_locationTerminals;
        public int m_selectedOrgHierarchyLevel = Common.INT_DBNULL;
        public int m_selectedOrgHierarchyId = Common.INT_DBNULL;
        Contact m_Contact;

        int m_selectedLocationHierarchyId = Common.INT_DBNULL;
        int m_selectedRowNum = Common.INT_DBNULL;
        int m_selectedRowIndex = Common.INT_DBNULL;
        int m_selectedOrgConfigId = Common.INT_DBNULL;
        int m_selectContactId = Common.INT_DBNULL;
        bool m_initialPrimaryState = false;
        string m_modifiedDate;

        //public event TerminalHandler TerminalAdded;

        //protected virtual void OnTerminalAdded(TerminalArgs e)
        //{
        //    if (TerminalAdded != null)
        //        TerminalAdded(this, e);
        //}

        void Method_Terminal_Add(object sender, TerminalArgs e)
        {
            m_locationTerminals = e.TerimalList;
        }
        #region Authorization Check
        private Boolean m_isSaveAvailable = false;
        private Boolean m_isSearchAvailable = false;

        private int m_userId = Authenticate.LoggedInUser.UserId;
        private string strUserName = Authenticate.LoggedInUser.UserName;

        private string strLocationCode = Common.LocationCode;
        private int m_currentLocationId = Common.CurrentLocationId;
        private int m_locationType = Common.CurrentLocationTypeId;
        #endregion

        #endregion Variables

        #region C'tor
        public frmLocation()
        {
            try
            {
                lblPageTitle.Text = "Location Hierarchy";

                m_isSaveAvailable = Authenticate.IsFunctionAccessible(strUserName, strLocationCode, LocationHierarchy.MODULE_CODE, Common.FUNCTIONCODE_SAVE);
                m_isSearchAvailable = Authenticate.IsFunctionAccessible(strUserName, strLocationCode, LocationHierarchy.MODULE_CODE, Common.FUNCTIONCODE_SEARCH);

                InitializeComponent();

                InitializeControls();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Method

        #region ValidateLocation

        private void ValidatePinCode(ErrorProvider err, TextBox txt, Label lbl, Boolean yesNo)
        {
            bool isValidPinCode = Validators.IsValidPinCode(txt.Text);

            if (isValidPinCode == false && yesNo == false)
                err.SetError(txt, Common.GetMessage("VAL0009", lbl.Text.Trim().Substring(0, lbl.Text.Trim().Length - 2)));
            else //if (isValidPinCode == true)
                err.SetError(txt, string.Empty);
        }

        private void StateIndexChange(bool yesNo)
        {
            if (yesNo == true)
            {
                if (cmbState.SelectedIndex > 0)
                {
                    DataTable dtCity = Common.ParameterLookup(Common.ParameterType.City, new ParameterFilter(string.Empty, Convert.ToInt32(cmbState.SelectedValue), 0, 0));
                    cmbCity.SelectedIndexChanged -= new EventHandler(cmbCity_SelectedIndexChanged);

                    cmbCity.DataSource = dtCity;
                    cmbCity.DisplayMember = "CityName";
                    cmbCity.ValueMember = "CityId";

                    cmbCity.SelectedIndexChanged += new EventHandler(cmbCity_SelectedIndexChanged);
                }
                else
                {
                    AddItem_SelectOne(cmbCity);
                }
            }
            ValidateLocationCombo(cmbState, lblState, yesNo);
        }
        #endregion

        #region ValidateContact

        private void StateContactIndexChange(bool yesNo)
        {
            if (yesNo)
            {
                if (cmbContactState.SelectedIndex > 0)
                {
                    DataTable dtCity = Common.ParameterLookup(Common.ParameterType.City, new ParameterFilter(string.Empty, Convert.ToInt32(cmbContactState.SelectedValue), 0, 0));
                    cmbContactCity.DataSource = dtCity;
                    cmbContactCity.DisplayMember = "CityName";
                    cmbContactCity.ValueMember = "CityId";
                }
                else
                {
                    AddItem_SelectOne(cmbContactCity);
                }
            }
            ValidateStatus(cmbContactState, lblContactState, yesNo);
        }

        #endregion

        /// <summary>
        /// Initialize Controls for Location
        /// </summary>
        private void InitializeControls()
        {
            m_lstContact = new List<Contact>();
            dgvLocationContact.AutoGenerateColumns = false;
            dgvLocationContact.DataSource = null;
            DataGridView dgvSearchNew = Common.GetDataGridViewColumns(dgvLocationContact, Environment.CurrentDirectory + "\\App_Data\\GridViewDefinition.xml");

            dgvLocation.AutoGenerateColumns = false;
            dgvLocation.DataSource = null;

            DataGridView dgvLocationSearch = Common.GetDataGridViewColumns(dgvLocation, Environment.CurrentDirectory + "\\App_Data\\GridViewDefinition.xml");


            FillTitle();
            FillStatus();
            //FillDistributor();
            FillCountry();
            FillLiveLocations();
            //FillTaxJurisdiction();
            PopulateLocationCombo();
            LocationHierarchy loc = new LocationHierarchy();
            m_locList = loc.ConfigSearch();

            LocationHierarchy orgItemSelect = new LocationHierarchy();
            orgItemSelect.HierarchyConfigId = Common.INT_DBNULL;
            orgItemSelect.HierarchyName = Common.SELECT_ONE;
            m_locList.Add(orgItemSelect);

            AddSelectItemInCombo(cmbState);
            AddSelectItemInCombo(cmbParentLocation);
            AddSelectItemInCombo(cmbContactState);
            AddSelectItemInCombo(cmbCity);
            AddSelectItemInCombo(cmbContactCity);

            string hierarchyConfig = "HierarchyConfigId Asc";
            m_locList.Sort((new GenericComparer<BusinessObjects.LocationHierarchy>(hierarchyConfig.Split(' ')[0], hierarchyConfig.Split(' ')[1] == "Asc" ? SortDirection.Ascending : SortDirection.Descending)).Compare);
            cmbType.SelectedIndexChanged -= new EventHandler(cmbType_SelectedIndexChanged);
            cmbType.DataSource = m_locList;
            cmbType.DisplayMember = Common.HIERARCHY_NAME;
            cmbType.ValueMember = Common.HIERARCHY_CONFIG;
            cmbType.SelectedIndexChanged += new EventHandler(cmbType_SelectedIndexChanged);



            ResetControl();
        }

        private void PopulateLocationCombo()
        {


            cmbRegAddressLocation.DataSource = null;
            DataTable dtLocations = Common.ParameterLookup(Common.ParameterType.Locations, new ParameterFilter("", -5, 0, 0));
            cmbRegAddressLocation.DataSource = dtLocations;
            cmbRegAddressLocation.DisplayMember = "LocationName";
            cmbRegAddressLocation.ValueMember = "LocationId";
        }

        private void FillLiveLocations()
        {
            DataTable dt = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter("LIVELOCATION", 0, 0, 0));
            cmbLiveLocation.DataSource = dt;
            cmbLiveLocation.DisplayMember = Common.KEYVALUE1;
            cmbLiveLocation.ValueMember = Common.KEYCODE1;
        }

        /// <summary>
        /// Remove Location Contact Record
        /// </summary>
        /// <param name="e"></param>
        private void RemoveLocationContact(DataGridViewCellEventArgs e)
        {
            if ((e.RowIndex >= 0) && (dgvLocationContact.Rows[e.RowIndex].Cells[e.ColumnIndex].GetType() == typeof(DataGridViewImageCell)))
            {
                if (Convert.ToInt32(dgvLocationContact.Rows[e.RowIndex].Cells[dgvLocationContact.Columns.Count - 1].Value) < 0)
                {
                    DialogResult saveResult = MessageBox.Show(Common.GetMessage("INF0028"), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (saveResult == DialogResult.Yes)
                    {
                        m_lstRemovedContact.Add(m_lstContact[e.RowIndex]);

                        dgvLocationContact.DataSource = null;
                        m_lstContact.RemoveAt(e.RowIndex);
                        dgvLocationContact.DataSource = m_lstContact;
                        m_selectedRowNum = Common.INT_DBNULL;
                        m_selectedRowIndex = Common.INT_DBNULL;
                        m_selectContactId = Common.INT_DBNULL;
                        m_contactModifiedDate = string.Empty;
                    }
                }
                else
                {
                    DialogResult saveResult = MessageBox.Show(Common.GetMessage("VAL0010", "contacts"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
        }

        /// <summary>
        /// Fill drop down for title
        /// </summary>
        private void FillTitle()
        {
            DataTable dtTitle = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter("TITLE", 0, 0, 0));
            cmbContactTitle.DataSource = dtTitle;
            cmbContactTitle.DisplayMember = Common.KEYVALUE1;
            cmbContactTitle.ValueMember = Common.KEYCODE1;
        }

        /// <summary>
        /// Fill drop down for Status
        /// </summary>
        private void FillStatus()
        {
            DataTable dt = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter("STATUS", 0, 0, 0));
            cmbStatus.DataSource = dt;
            cmbStatus.DisplayMember = Common.KEYVALUE1;
            cmbStatus.ValueMember = Common.KEYCODE1;
            cmbStatus.SelectedValue = 1;

            DataTable dtStatus = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter("STATUS", 0, 0, 0));
            cmbContactStatus.DataSource = dtStatus;
            cmbContactStatus.DisplayMember = Common.KEYVALUE1;
            cmbContactStatus.ValueMember = Common.KEYCODE1;
            cmbContactStatus.SelectedValue = 1;
        }

        /// <summary>
        /// Add Select Item into Combo Box
        /// </summary>
        /// <param name="cmb"></param>
        private void AddSelectItemInCombo(ComboBox cmb)
        {
            DataTable dtSelectOne = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter(Common.INT_DBNULL.ToString(), 0, 0, 0));
            cmb.DataSource = dtSelectOne;
            cmb.ValueMember = Common.KEYCODE1.ToString();
            cmb.DisplayMember = Common.KEYVALUE1;
        }

        /// <summary>
        /// Fill drop down for Tax Jurisdiction
        /// </summary>
        //private void FillTaxJurisdiction()
        //{
        //    DataTable dtJurisdiction = Common.ParameterLookup(Common.ParameterType.TaxJurisdiction, new ParameterFilter(string.Empty, 0, 0, 0));
        //    cmbTaxJurisdiction.DataSource = dtJurisdiction;
        //    cmbTaxJurisdiction.DisplayMember = "JurisdictionName";
        //    cmbTaxJurisdiction.ValueMember = "JurisdictionId";
        //}



        /*
        /// <summary>
        /// Fill drop down for Distributor
        /// </summary>
        private void FillDistributor()
        {
            DataTable dtDistributor = Common.ParameterLookup(Common.ParameterType.Distributor, new ParameterFilter(string.Empty, 0, 0, 0));
            cmbDistributor.DataSource = dtDistributor;
            cmbDistributor.DisplayMember = "DistributorName";
            cmbDistributor.ValueMember = "DistributorId";
        }
        */

        /// <summary>
        /// Fill drop down for Country
        /// </summary>
        private void FillCountry()
        {
            DataTable dtCountry = Common.ParameterLookup(Common.ParameterType.Country, new ParameterFilter(string.Empty, 0, 0, 0));
            cmbCountry.DataSource = dtCountry;
            cmbCountry.DisplayMember = "CountryName";
            cmbCountry.ValueMember = "CountryId";

            DataTable dtCountry1 = Common.ParameterLookup(Common.ParameterType.Country, new ParameterFilter(string.Empty, 0, 0, 0));
            cmbContactCountry.DataSource = dtCountry1;
            cmbContactCountry.DisplayMember = "CountryName";
            cmbContactCountry.ValueMember = "CountryId";
        }

        /// <summary>
        /// Bind State
        /// </summary>
        private void CountryIndexChange(bool yesNo)
        {
            if (yesNo)
            {
                if (cmbCountry.SelectedIndex > 0)
                {
                    cmbState.SelectedIndexChanged -= new EventHandler(cmbState_SelectedIndexChanged);

                    DataTable dtState = Common.ParameterLookup(Common.ParameterType.State, new ParameterFilter(string.Empty, Convert.ToInt32(cmbCountry.SelectedValue), 0, 0));

                    cmbState.DataSource = dtState;
                    cmbState.DisplayMember = "StateName";
                    cmbState.ValueMember = "StateId";

                    FillTaxJurisdiction();

                    cmbState.SelectedIndexChanged += new EventHandler(cmbState_SelectedIndexChanged);
                }
                else if (cmbCountry.SelectedIndex == 0)
                {
                    cmbState.SelectedIndexChanged -= new EventHandler(cmbState_SelectedIndexChanged);
                    cmbCity.SelectedIndexChanged -= new EventHandler(cmbCity_SelectedIndexChanged);
                    //StateIndexChange(yesNo);
                    //cmbState.SelectedIndex = 0;
                    //cmbCity.SelectedIndex = 0;

                    //cmbCity.Items.Clear();
                    //cmbState.Items.Clear();

                    AddItem_SelectOne(cmbCity);
                    AddItem_SelectOne(cmbState);
                    AddItem_SelectOne(cmbTaxJurisdiction);

                    cmbState.SelectedIndexChanged += new EventHandler(cmbState_SelectedIndexChanged);
                    cmbCity.SelectedIndexChanged += new EventHandler(cmbCity_SelectedIndexChanged);
                }
            }
            ValidateLocationCombo(cmbCountry, lblCountry, yesNo);
        }

        /// <summary>
        /// Fill Dropdown of TaxJurisdiction
        /// </summary>
        private void FillTaxJurisdiction()
        {
            DataTable dtTaxJurisdiction = Common.ParameterLookup(Common.ParameterType.TaxJurisdiction, new ParameterFilter(string.Empty, Convert.ToInt32(cmbCountry.SelectedValue), 0, 0));
            cmbTaxJurisdiction.DataSource = dtTaxJurisdiction;
            cmbTaxJurisdiction.ValueMember = "StateId";
            cmbTaxJurisdiction.DisplayMember = "StateName";
        }


        /// <summary>
        /// fn. to Validate Parent Location
        /// </summary>
        /// <param name="yesNo"></param>
        private void ValidateParentLocation(bool yesNo)
        {
            if ((cmbType.SelectedIndex > 0) && (Convert.ToInt32(cmbType.SelectedValue) == Convert.ToInt32(Common.LocationConfigId.BO) || Convert.ToInt32(cmbType.SelectedValue) == Convert.ToInt32(Common.LocationConfigId.PC)) && (cmbParentLocation.SelectedIndex == 0))
                ValidateLocationCombo(cmbParentLocation, lblParentLocation, yesNo);
            else
                errLocation.SetError(cmbParentLocation, string.Empty);
        }

        /// <summary>
        /// Adding Blank Item into List
        /// </summary>
        /// <param name="cmb"></param>
        private void AddItem_SelectOne(ComboBox cmb)
        {
            DataTable dtSelectOne = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter(Common.INT_DBNULL.ToString(), 0, 0, 0));
            cmb.DataSource = dtSelectOne;
            cmb.ValueMember = Common.KEYCODE1.ToString();
            cmb.DisplayMember = Common.KEYVALUE1;
        }

        /// <summary>
        /// Generate Location Contact Error 
        /// </summary>
        /// <returns></returns>
        private StringBuilder GenerateContactError()
        {
            StringBuilder sbError = new StringBuilder();
            if (errContact.GetError(cmbContactTitle).Trim().Length > 0)
            {
                sbError.Append(errContact.GetError(cmbContactTitle));
                sbError.AppendLine();
            }
            if (errContact.GetError(txtContactFirstName).Trim().Length > 0)
            {
                sbError.Append(errContact.GetError(txtContactFirstName));
                sbError.AppendLine();
            }
            if (errContact.GetError(txtContactLastName).Trim().Length > 0)
            {
                sbError.Append(errContact.GetError(txtContactLastName));
                sbError.AppendLine();
            }
            if (errContact.GetError(txtContactAddress1).Trim().Length > 0)
            {
                sbError.Append(errContact.GetError(txtContactAddress1));
                sbError.AppendLine();
            }
            if (errContact.GetError(txtContactAddress2).Trim().Length > 0)
            {
                sbError.Append(errContact.GetError(txtContactAddress2));
                sbError.AppendLine();
            }
            if (errContact.GetError(cmbContactCountry).Trim().Length > 0)
            {
                sbError.Append(errContact.GetError(cmbContactCountry));
                sbError.AppendLine();
            }
            if (errContact.GetError(cmbContactState).Trim().Length > 0)
            {
                sbError.Append(errContact.GetError(cmbContactState));
                sbError.AppendLine();
            }
            if (errContact.GetError(cmbContactCity).Trim().Length > 0)
            {
                sbError.Append(errContact.GetError(cmbContactCity));
                sbError.AppendLine();
            }
            if (errContact.GetError(txtContactPinCode).Trim().Length > 0)
            {
                sbError.Append(errContact.GetError(txtContactPinCode));
                sbError.AppendLine();
            }
            if (errContact.GetError(txtContactEmail).Trim().Length > 0)
            {
                sbError.Append(errContact.GetError(txtContactEmail));
                sbError.AppendLine();
            }
            if (errContact.GetError(cmbContactState).Length > 0)
            {
                sbError.Append(errContact.GetError(cmbContactState));
                sbError.AppendLine();
            }
            if (errContact.GetError(cmbContactStatus).Length > 0)
            {
                sbError.Append(errContact.GetError(cmbContactStatus));
                sbError.AppendLine();
            }
            sbError = Common.ReturnErrorMessage(sbError);
            return sbError;
        }

        /// <summary>
        /// Adding Contact into List
        /// </summary>
        private void AddContact()
        {
            #region ValidateContact

            ValidateStatus(cmbContactTitle, lblContactTitle, false);
            ValidatedText(txtContactFirstName, lblContactFirstName, false);
            ValidatedText(txtContactLastName, lblContactLastName, false);
            ValidatedText(txtContactAddress1, lblContactAddress1, false);
            ValidatedText(txtContactAddress2, lblContactAddress2, false);

            ContactCountry(false);
            StateContactIndexChange(false);
            ValidateStatus(cmbContactCity, lblContactCity, false);

            ValidatePinCode(errContact, txtContactPinCode, lblContactPinCode, false);
            //ValidatedText(txtContactPinCode, lblContactPinCode);

            ValidateStatus(cmbContactStatus, lblContactStatus, false);
            ValidateEmail(txtContactEmail, lblContactEmail, false);
            #endregion

            #region Check Location Errors

            StringBuilder sbError = new StringBuilder();
            sbError = GenerateContactError();

            #endregion

            if (!sbError.ToString().Trim().Equals(string.Empty))
            {
                MessageBox.Show(sbError.ToString(), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int isDuplicateRecordFound = Common.INT_DBNULL;
            int primaryCount = Common.INT_DBNULL;
            if ((m_lstContact != null) && (m_lstContact.Count > 0) && (m_selectedRowIndex == Common.INT_DBNULL))
            {
                //checked based on Email Id, Every Person have unique Email id
                isDuplicateRecordFound = (from p in m_lstContact where p.Email1.Trim() == txtContactEmail.Text.Trim() select p.Email1).Count();


                if (isDuplicateRecordFound > 0)
                {
                    MessageBox.Show(Common.GetMessage("VAL0007", lblContactEmail.Text.Trim().Substring(0, lblContactEmail.Text.Trim().Length - 2)), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else if (m_selectedRowIndex != Common.INT_DBNULL)
            {
                if (m_lstContactString.IndexOf(txtContactEmail.Text.Trim()) >= 0)
                {
                    MessageBox.Show(Common.GetMessage("VAL0007", lblContactEmail.Text.Trim().Substring(0, lblContactEmail.Text.Trim().Length - 2)), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            if ((m_lstContact != null) && (m_lstContact.Count > 0))
            {
                primaryCount = (from p in m_lstContact where p.IsPrimary == true && p.Status == 1 select p.IsPrimary).Count();
                if (chkIsPrimaryContact.Checked)
                {
                    if (((m_initialPrimaryState == true) && (primaryCount > 1)) || ((m_initialPrimaryState == false) && (primaryCount > 0)))
                    {
                        MessageBox.Show(Common.GetMessage("VAL0008", "contact"));
                        return;
                    }
                }
            }
            DialogResult saveResult = MessageBox.Show(Common.GetMessage("5010", "Add"), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (saveResult == DialogResult.Yes)
            {
                #region Assign info to Contact Object
                Contact cnt = new Contact();
                cnt.Title = Convert.ToInt32(cmbContactTitle.SelectedValue);
                cnt.FirstName = txtContactFirstName.Text.Trim();
                cnt.LastName = txtContactLastName.Text.Trim();
                cnt.MiddleName = txtContactMiddleName.Text.Trim();
                cnt.Designation = txtContactDesignation.Text.Trim();
                cnt.Status = Convert.ToInt32(cmbContactStatus.SelectedValue);
                cnt.StatusValue = cmbContactStatus.Text.Trim();
                cnt.IsPrimary = chkIsPrimaryContact.Checked;

                cnt.Email1 = txtContactEmail.Text;
                cnt.Address1 = txtContactAddress1.Text;
                cnt.Address2 = txtContactAddress2.Text;
                cnt.Address3 = txtContactAddress3.Text;
                cnt.Mobile1 = txtContactMobile.Text;
                cnt.PhoneNumber1 = txtContactPhone.Text;
                cnt.Country = cmbContactCountry.Text;
                cnt.CountryId = Convert.ToInt32(cmbContactCountry.SelectedValue);
                cnt.StateId = Convert.ToInt32(cmbContactState.SelectedValue);
                cnt.State = cmbContactState.Text;
                cnt.CityId = Convert.ToInt32(cmbContactCity.SelectedValue);
                cnt.City = cmbContactCity.Text;
                cnt.Fax1 = txtContactFax.Text;
                cnt.Website = txtContactWebSite.Text;
                cnt.PinCode = txtContactPinCode.Text;
                cnt.ContactId = m_selectContactId.ToString();
                cnt.ModifiedBy = 1;
                cnt.ModifiedDate = m_contactModifiedDate;//.Trim().Length == 0 ? "1900-01-01" : m_contactModifiedDate;
                #endregion

                if (m_lstContact == null)
                {
                    m_lstContact = new List<Contact>();
                }

                if (m_selectContactId != Common.INT_DBNULL)
                    cnt.RowNum = Common.INT_DBNULL;//Convert.ToInt16(dgvLocationContact.Rows.Count);
                else
                    cnt.RowNum = m_selectedRowNum;

                if ((m_selectedRowIndex != Common.INT_DBNULL) && (m_selectedRowIndex <= dgvLocationContact.Rows.Count))
                {
                    m_lstContact.Insert(m_selectedRowIndex, cnt);
                    m_lstContact.RemoveAt(m_selectedRowIndex + 1);
                }
                else
                    m_lstContact.Add(cnt);


                dgvLocationContact.DataSource = new List<Contact>();
                if ((m_lstContact != null) && (m_lstContact.Count > 0))
                {
                    dgvLocationContact.DataSource = m_lstContact;

                    var query = (from p in m_lstContact where p.Status == 1 select p.Email1);
                    m_lstContactString = query.ToList();

                    // DisableButtons();
                }
                ResetContactControl();
            }
        }

        private void DisableButtons()
        {
            foreach (DataGridViewRow dgvRow in dgvLocationContact.Rows)
            {
                int id = Convert.ToInt32(dgvRow.Cells[dgvRow.Cells.Count - 1].Value);
                if (id > 0)
                {
                    dgvRow.Frozen = true;
                    //if (dgvRow.Cells[0].GetType() == typeof(DataGridViewButtonCell))
                    //{
                    //    DataGridViewButtonCell dgvbc = (DataGridViewButtonCell)dgvRow.Cells[0];
                    //}
                }
            }
        }
        /// <summary>
        /// Validate Combo Box
        /// </summary>
        /// <param name="cmb"></param>
        /// <param name="lbl"></param>
        //private void ValidateLocationCombo(ComboBox cmb, Label lbl, Boolean yesNo)
        //{
        //    if (cmb.SelectedIndex == 0 && yesNo==false)
        //        errLocation.SetError(cmb, Common.GetMessage("INF0026", lbl.Text.Trim().Substring(0, lbl.Text.Trim().Length - 2)));
        //    else
        //        errLocation.SetError(cmb, string.Empty);
        //}
        private void ValidateLocationCombo(ComboBox cmb, Label lbl, bool isCtrlEnabled)
        {
            if (cmb.SelectedIndex == 0 && isCtrlEnabled == false)
                errLocation.SetError(cmb, Common.GetMessage("INF0026", lbl.Text.Trim().Substring(0, lbl.Text.Trim().Length - 2)));
            else
                errLocation.SetError(cmb, string.Empty);
        }

        private void ValidateTaxJurisdictionCombo(ComboBox cmb, Label lbl, bool isCtrlEnabled)
        {
            if (cmb.SelectedIndex == 0 && isCtrlEnabled == false)
                errLocation.SetError(cmb, Common.GetMessage("INF0026", lbl.Text.Trim().Substring(0, lbl.Text.Trim().Length - 2)));
            else
                errLocation.SetError(cmb, string.Empty);
        }

        /// <summary>
        /// Validate Combo Box
        /// </summary>
        /// <param name="cmb"></param>
        /// <param name="lbl"></param>
        private void ValidateStatus(ComboBox cmb, Label lbl, Boolean yesNo)
        {
            if (cmb.SelectedIndex == 0 && yesNo == false)
                errContact.SetError(cmb, Common.GetMessage("INF0026", lbl.Text.Trim().Substring(0, lbl.Text.Trim().Length - 2)));
            else
                errContact.SetError(cmb, string.Empty);
        }

        /// <summary>
        /// Validate Text
        /// </summary>
        /// <param name="txt"></param>
        /// <param name="lbl"></param>
        private void ValidatedLocationText(TextBox txt, Label lbl, Boolean yesNo)
        {
            bool isTextBoxEmpty = CoreComponent.Core.BusinessObjects.Validators.CheckForEmptyString(txt.Text.Length);
            if (isTextBoxEmpty == true && yesNo == false)
                errLocation.SetError(txt, Common.GetMessage("INF0019", lbl.Text.Trim().Substring(0, lbl.Text.Trim().Length - 2)));
            else //if (isTextBoxEmpty == false)
                errLocation.SetError(txt, string.Empty);
        }

        /// <summary>
        /// Validate Text
        /// </summary>
        /// <param name="txt"></param>
        /// <param name="lbl"></param>
        private void ValidatedText(TextBox txt, Label lbl, Boolean yesNo)
        {
            bool isTextBoxEmpty = CoreComponent.Core.BusinessObjects.Validators.CheckForEmptyString(txt.Text.Length);
            if (isTextBoxEmpty == true && yesNo == false)
                errContact.SetError(txt, Common.GetMessage("INF0019", lbl.Text.Trim().Substring(0, lbl.Text.Trim().Length - 2)));
            else //if (isTextBoxEmpty == false)
                errContact.SetError(txt, string.Empty);
        }

        /// <summary>
        /// Generate and build Errors for Locations on Save Click
        /// </summary>
        private StringBuilder GenerateLocationError()
        {
            StringBuilder sbError = new StringBuilder();
            if (errLocation.GetError(cmbType).Trim().Length > 0)
            {
                sbError.Append(errLocation.GetError(cmbType));
                sbError.AppendLine();
            }
            if (errLocation.GetError(txtLocationCode).Trim().Length > 0)
            {
                sbError.Append(errLocation.GetError(txtLocationCode));
                sbError.AppendLine();
            }
            if (errLocation.GetError(txtLocationName).Trim().Length > 0)
            {
                sbError.Append(errLocation.GetError(txtLocationName));
                sbError.AppendLine();
            }
            if (errLocation.GetError(txtAddressLine1).Trim().Length > 0)
            {
                sbError.Append(errLocation.GetError(txtAddressLine1));
                sbError.AppendLine();
            }
            if (errLocation.GetError(txtAddressLine2).Trim().Length > 0)
            {
                sbError.Append(errLocation.GetError(txtAddressLine2));
                sbError.AppendLine();
            }
            if (errLocation.GetError(cmbCountry).Trim().Length > 0)
            {
                sbError.Append(errLocation.GetError(cmbCountry));
                sbError.AppendLine();
            }
            if (errLocation.GetError(cmbState).Trim().Length > 0)
            {
                sbError.Append(errLocation.GetError(cmbState));
                sbError.AppendLine();
            }
            if (errLocation.GetError(cmbCity).Trim().Length > 0)
            {
                sbError.Append(errLocation.GetError(cmbCity));
                sbError.AppendLine();
            }
            if (errLocation.GetError(txtPinCode).Trim().Length > 0)
            {
                sbError.Append(errLocation.GetError(txtPinCode));
                sbError.AppendLine();
            }
            if (errLocation.GetError(txtEmail).Trim().Length > 0)
            {
                sbError.Append(errLocation.GetError(txtEmail));
                sbError.AppendLine();
            }
            if (errLocation.GetError(cmbParentLocation).Length > 0)
            {
                sbError.Append(errLocation.GetError(cmbParentLocation));
                sbError.AppendLine();
            }
            //if (errLocation.GetError(cmbTaxJurisdiction).Trim().Length > 0)
            //{
            //    sbError.Append(errLocation.GetError(cmbTaxJurisdiction));
            //    sbError.AppendLine();
            //}
            if (errLocation.GetError(txtStartingAmount).Trim().Length > 0)
            {
                sbError.Append(errLocation.GetError(txtStartingAmount));
                sbError.AppendLine();
            }
            if (errLocation.GetError(cmbStatus).Trim().Length > 0)
            {
                sbError.Append(errLocation.GetError(cmbStatus));
                sbError.AppendLine();
            }
            if (errLocation.GetError(cmbGrade).Trim().Length > 0)
            {
                sbError.Append(errLocation.GetError(cmbGrade));
                sbError.AppendLine();
            }
            if (cmbType.SelectedValue.ToString() == Convert.ToInt32(Common.LocationConfigId.BO).ToString() &&
               chkIsMiniBranch.CheckState == CheckState.Checked)
            {
                if (string.IsNullOrEmpty(txtDistributorId.Text.Trim()))
                {
                    txtDistributorId.Enabled = true;
                    pictureBox1.Enabled = true;
                    sbError.Append(errLocation.GetError(txtDistributorId));
                    sbError.AppendLine();
                }

            }
            else if (cmbType.SelectedValue.ToString() == Convert.ToInt32(Common.LocationConfigId.PC).ToString())
            {
                if (string.IsNullOrEmpty(txtDistributorId.Text.Trim()))
                {
                    txtDistributorId.Enabled = true;
                    pictureBox1.Enabled = true;
                    sbError.Append(errLocation.GetError(txtDistributorId));
                    sbError.AppendLine();
                }
            }
            if (errLocation.GetError(cmbLiveLocation).Trim().Length > 0)
            {
                sbError.Append(errLocation.GetError(cmbLiveLocation));
                sbError.AppendLine();
            }
            if (errLocation.GetError(cmbTaxJurisdiction).Trim().Length > 0)
            {
                sbError.Append(errLocation.GetError(cmbTaxJurisdiction));
                sbError.AppendLine();
            }
            sbError = Common.ReturnErrorMessage(sbError);
            return sbError;
        }
        void LocationCodeValidate(bool yesNo)
        {
            bool isTextBoxEmpty = CoreComponent.Core.BusinessObjects.Validators.CheckForEmptyString(txtLocationCode.Text.Length);
            if (isTextBoxEmpty == true && yesNo == false)
                errLocation.SetError(txtLocationCode, Common.GetMessage("INF0019", lblLocationCode.Text.Trim().Substring(0, lblLocationCode.Text.Trim().Length - 2)));
            else if (isTextBoxEmpty == false && yesNo == false)
                errLocation.SetError(txtLocationCode, Common.CodeValidate(txtLocationCode.Text, lblLocationCode.Text.Trim().Substring(0, lblLocationCode.Text.Trim().Length - 2)));
            else //if (isTextBoxEmpty == false)
                errLocation.SetError(txtLocationCode, string.Empty);

        }
        /// <summary>
        /// Location Validation Functions on Save Click
        /// </summary>
        private void ValidateLocationControls(Boolean yesNo)
        {
            cmbTypeValidation(yesNo);

            ValidatedLocationText(txtLocationName, lblLocationName, yesNo);
            LocationCodeValidate(yesNo);
            //ValidatedLocationText(txtLocationCode, lblLocationCode, yesNo);
            ValidatedLocationText(txtAddressLine1, lblAddressLine1, yesNo);
            ValidatedLocationText(txtAddressLine2, lblAddressLine2, yesNo);
            ValidateLocationEmail(txtEmail, lblEmail, yesNo);
            ValidateStartingAmount(yesNo);
            ValidatePinCode(errLocation, txtPinCode, lblPinCode, yesNo);
            CountryIndexChange(yesNo);
            StateIndexChange(yesNo);
            ValidateLocationCombo(cmbCity, lblCity, yesNo);
            //ValidateTaxJurisdiction(yesNo);
            ValidateLocationCombo(cmbStatus, lblStatus, yesNo);
            ValidateParentLocation(yesNo);
            ValidateGrade(yesNo);
            ValidateDistributor(yesNo);
            ValidateLiveLocation(yesNo);
            if (cmbTaxJurisdiction.Enabled == true)
                ValidateTaxJurisdictionCombo(cmbTaxJurisdiction, lblTaxJurisdiction, yesNo);
            else
                errLocation.SetError(cmbTaxJurisdiction, string.Empty);
        }

        private void ValidateLiveLocation(bool yesNo)
        {
            if (!yesNo)
            {
                if (Convert.ToInt32(cmbLiveLocation.SelectedValue) < 0)
                    errLocation.SetError(cmbLiveLocation, Common.GetMessage("VAL0002", lblLiveLocation.Text.Substring(0, lblLiveLocation.Text.Length - 2)));
                else
                    errLocation.SetError(cmbLiveLocation, string.Empty);
            }
        }

        /// <summary>
        /// Validate Distributor
        /// </summary>
        private void ValidateDistributor(Boolean yesNo)
        {
            if (Convert.ToInt32(cmbType.SelectedValue) == Convert.ToInt32(Common.LocationConfigId.PC) && txtDistributorId.Enabled == true)
            {
                if (txtDistributorId.Text == string.Empty)
                    errLocation.SetError(txtDistributorId, Common.GetMessage("VAL0001", lblDistributor.Text.Substring(0, lblDistributor.Text.Length - 2)));
                else if (!(Validators.IsInt32(txtDistributorId.Text) && CheckDistributor()))
                    errLocation.SetError(txtDistributorId, Common.GetMessage("VAL0109"));
                else
                    errLocation.SetError(txtDistributorId, string.Empty);
            }

            if (chkIsMiniBranch.Checked)
            {
                if (txtDistributorId.Text == string.Empty)
                    errLocation.SetError(txtDistributorId, Common.GetMessage("VAL0001", lblDistributor.Text.Substring(0, lblDistributor.Text.Length - 2)));
            }
        }

        private bool CheckDistributor()
        {
            DataTable dt = Common.ParameterLookup(Common.ParameterType.Distributor, new ParameterFilter("", 0, Convert.ToInt32(txtDistributorId.Text), 0));
            if (dt.Rows.Count == 1)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Save location record
        /// </summary>
        private void SaveLocation()
        {
            #region ValidateLocationControls
            ValidateLocationControls(false);
            #endregion

            #region Check Location Errors

            StringBuilder sbError = new StringBuilder();
            sbError = GenerateLocationError();

            #endregion

            //if (txtLocationCode.Text.IndexOf(" ") >= 0)
            //{
            //    MessageBox.Show(Common.GetMessage("VAL0082"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}
            if (!sbError.ToString().Trim().Equals(string.Empty))
            {
                MessageBox.Show(sbError.ToString(), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //txtParentName_Validated(this, new EventArgs());

            if ((m_lstContact != null) && (m_lstContact.Count > 0))
            {
                int isPrimaryCount = (from p in m_lstContact where p.IsPrimary == true && p.Status == 1 select p.IsPrimary).Count();
                if (isPrimaryCount == 0)
                {
                    MessageBox.Show(Common.GetMessage("VAL0004"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            if ((m_lstContact == null) || (m_lstContact.Count == 0))
            {
                MessageBox.Show(Common.GetMessage("VAL0004"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                if (((Convert.ToInt32(cmbType.SelectedValue) == Convert.ToInt32(Common.LocationConfigId.WH)) || (Convert.ToInt32(cmbType.SelectedValue) == Convert.ToInt32(Common.LocationConfigId.BO))) && (m_selectedOrgHierarchyId == Common.INT_DBNULL))
                {
                    MessageBox.Show(Common.GetMessage("VAL0005"));
                    return;
                }
                LocationHierarchy m_locHierarchy = new LocationHierarchy();

                if ((Convert.ToInt32(cmbType.SelectedValue) == Convert.ToInt32(Common.LocationConfigId.BO)) && (m_selectedOrgHierarchyLevel != Convert.ToInt32(Common.LocationOrgLevel.BOArea) - 1))
                {
                    MessageBox.Show(Common.GetMessage("VAL0006", "area"));
                    return;
                }
                else if ((Convert.ToInt32(cmbType.SelectedValue) == Convert.ToInt32(Common.LocationConfigId.WH)) && (m_selectedOrgHierarchyLevel != Convert.ToInt32(Common.LocationOrgLevel.WHZone) - 1))
                {
                    MessageBox.Show(Common.GetMessage("VAL0006", "zone"));
                    return;
                }

                DialogResult saveResult = MessageBox.Show(Common.GetMessage("INF0027"), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (saveResult == DialogResult.Yes)
                {
                    if ((m_selectedLocationHierarchyId == Common.INT_DBNULL) && (Convert.ToInt32(cmbStatus.SelectedValue) == 2))
                    {
                        MessageBox.Show(Common.GetMessage("VAL0020"), Common.GetMessage("10001"), MessageBoxButtons.OK);
                        return;
                    }

                    if ((Convert.ToInt32(cmbType.SelectedValue) == Convert.ToInt32(Common.LocationConfigId.PC)))
                    {
                        m_locHierarchy.Garde = Convert.ToInt32(cmbGrade.SelectedValue);
                    }

                    if ((Convert.ToInt32(cmbType.SelectedValue) == Convert.ToInt32(Common.LocationConfigId.WH)))
                    {
                        m_locHierarchy.ReplenishmentLocationId = Common.INT_DBNULL.ToString();
                        m_locHierarchy.StartingAmount = Common.INT_DBNULL;
                        //m_locHierarchy.TaxJurisdiction = cmbTaxJurisdiction.SelectedValue.ToString();
                        m_locHierarchy.DistributorId = Common.INT_DBNULL.ToString();
                    }
                    else
                    {
                        //m_locHierarchy.TaxJurisdiction = Common.INT_DBNULL.ToString();
                        m_locHierarchy.StartingAmount = Convert.ToDecimal(txtStartingAmount.Text.Trim().Length == 0 ? Common.INT_DBNULL.ToString() : txtStartingAmount.Text);
                        m_locHierarchy.ReplenishmentLocationId = cmbParentLocation.SelectedValue.ToString();
                        m_locHierarchy.DistributorId = (txtDistributorId.Text == "" ? "-1" : txtDistributorId.Text);
                    }
                    //if m_orgList
                    m_locHierarchy.Address1 = txtAddressLine1.Text;
                    m_locHierarchy.Address2 = txtAddressLine2.Text;
                    m_locHierarchy.Address3 = txtAddressLine3.Text;
                    m_locHierarchy.CountryId = Convert.ToInt32(cmbCountry.SelectedValue);
                    m_locHierarchy.StateId = Convert.ToInt32(cmbState.SelectedValue);
                    m_locHierarchy.PinCode = txtPinCode.Text;
                    m_locHierarchy.PhoneNumber1 = txtPhone.Text;
                    m_locHierarchy.Email1 = txtEmail.Text;
                    m_locHierarchy.CityId = Convert.ToInt32(cmbCity.SelectedValue);

                    m_locHierarchy.LocationType = Convert.ToInt32(cmbType.SelectedValue);
                    m_locHierarchy.HierarchyCode = txtLocationCode.Text;
                    m_locHierarchy.HierarchyName = txtLocationName.Text;
                    m_locHierarchy.ShortName = txtShortName.Text;
                    m_locHierarchy.Grade = Convert.ToInt32(cmbGrade.SelectedValue);
                    m_locHierarchy.TinNo = txtTinNo.Text;
                    m_locHierarchy.VatNo = txtVAT.Text.Trim();
                    m_locHierarchy.CstNo = txtCstNo.Text.Trim();
                    m_locHierarchy.Grade = Convert.ToInt32(cmbGrade.SelectedValue);
                    m_locHierarchy.Mobile1 = txtMobile.Text;
                    m_locHierarchy.Fax1 = txtFax.Text;
                    m_locHierarchy.Status = Convert.ToInt32(cmbState.SelectedValue);
                    m_locHierarchy.HierarchyId = m_selectedLocationHierarchyId;

                    m_locHierarchy.Status = Convert.ToInt32(cmbStatus.SelectedValue);
                    m_locHierarchy.ModifiedBy = m_userId;
                    m_locHierarchy.Description = txtDescription.Text;

                    m_locHierarchy.OrgHierarchyId = m_selectedOrgHierarchyId.ToString();
                    m_locHierarchy.OrgConfigId = m_selectedOrgConfigId.ToString();
                    m_locHierarchy.Contact = m_lstContact;

                    m_locHierarchy.ModifiedContactId = m_lstModifiedContactId;

                    if (m_selectedLocationHierarchyId != Common.INT_DBNULL)
                        m_locHierarchy.ModifiedDate = m_modifiedDate.ToString();

                    m_locHierarchy.RemovedContacts = m_lstRemovedContact;
                    m_locHierarchy.LocationTerminal = m_locationTerminals;
                    m_locHierarchy.LiveLocation = Convert.ToInt32(cmbLiveLocation.SelectedValue);
                    m_locHierarchy.TaxJurisdiction = cmbTaxJurisdiction.SelectedValue.ToString();
                    //AKASH
                    m_locHierarchy.IECCode = txtIECCode.Text;


                    int? miniBranchIntValue = null;
                    if (chkIsMiniBranch.CheckState != CheckState.Indeterminate && chkIsMiniBranch.Checked)
                    {
                        miniBranchIntValue = 1;
                        m_locHierarchy.IsMiniBranch = 1;
                    }
                    else if (chkIsMiniBranch.CheckState != CheckState.Indeterminate && chkIsMiniBranch.Checked == false)
                    {
                        miniBranchIntValue = 0;
                        m_locHierarchy.IsMiniBranch = 0;
                    }
                    else if (chkIsMiniBranch.CheckState == CheckState.Indeterminate)
                    {
                        miniBranchIntValue = -1;
                        m_locHierarchy.IsMiniBranch = -1;
                    }

                    m_locHierarchy.RegAddLocationId = Convert.ToInt32(cmbRegAddressLocation.SelectedValue);

                    string errorMesage = string.Empty;
                    bool recordSaved = m_locHierarchy.Save(ref errorMesage);

                    if (errorMesage.Equals(string.Empty))
                    {
                        m_locList = Search(string.Empty, string.Empty, string.Empty, Common.INT_DBNULL, Common.INT_DBNULL, Common.INT_DBNULL, string.Empty, string.Empty, Common.INT_DBNULL, Common.INT_DBNULL, Common.INT_DBNULL, Common.INT_DBNULL, string.Empty, miniBranchIntValue, Common.INT_DBNULL);
                        dgvLocation.DataSource = m_locList;
                        dgvLocation.ClearSelection();
                        ResetContactControl();
                        ResetControl();
                        dgvLocationContact.DataSource = new List<Contact>();

                        m_locList = Search(string.Empty, string.Empty, string.Empty, Common.INT_DBNULL, Common.INT_DBNULL, Common.INT_DBNULL, string.Empty, string.Empty, Common.INT_DBNULL, Common.INT_DBNULL, Common.INT_DBNULL, Common.INT_DBNULL, string.Empty, miniBranchIntValue, Common.INT_DBNULL);
                        dgvLocation.DataSource = m_locList;
                        dgvLocation.ClearSelection();
                        //List<LocationHierarchy> lst_hierarchy = new Llist<LocationHierarchy>();

                        MessageBox.Show(Common.GetMessage("8001"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        m_lstContact = null;

                    }
                    else if (errorMesage.Equals("INF0020"))
                        MessageBox.Show(Common.GetMessage(errorMesage, lblLocationCode.Text.Trim().Substring(0, lblLocationCode.Text.Trim().Length - 2), lblLocationName.Text.Trim().Substring(0, lblLocationName.Text.Trim().Length - 2)), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else if (errorMesage.IndexOf("INF0030") >= 0)
                        MessageBox.Show(Common.GetMessage("INF0030", errorMesage.Substring("INF00030".Length)), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                        MessageBox.Show(Common.GetMessage(errorMesage), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// reset controls
        /// </summary>
        private void ResetContactControl()
        {
            VisitControls visitControls = new VisitControls();
            visitControls.ResetAllControlsInPanel(errContact, pnlCreateHeader);
            m_selectedRowNum = Common.INT_DBNULL;
            m_selectedRowIndex = Common.INT_DBNULL;
            m_selectContactId = Common.INT_DBNULL;
            m_initialPrimaryState = false;
            m_contactModifiedDate = string.Empty;
            cmbContactStatus.SelectedValue = 1;
            AddItem_SelectOne(cmbContactState);
            AddItem_SelectOne(cmbContactCity);
            cmbContactTitle.Focus();
            
        }

        /// <summary>
        /// bind State for Contact Location
        /// </summary>
        private void ContactCountry(bool yesNo)
        {
            try
            {
                if (yesNo)
                {
                    if (cmbContactCountry.SelectedIndex > 0)
                    {
                        DataTable dtState = Common.ParameterLookup(Common.ParameterType.State, new ParameterFilter(string.Empty, Convert.ToInt32(cmbContactCountry.SelectedValue), 0, 0));
                        cmbContactState.DataSource = dtState;
                        cmbContactState.DisplayMember = "StateName";
                        cmbContactState.ValueMember = "StateId";
                    }
                    else if (cmbContactState.SelectedIndex == 0)
                    {
                        cmbContactState.SelectedIndexChanged -= new EventHandler(cmbContactState_SelectedIndexChanged);
                        cmbContactCity.SelectedIndexChanged -= new EventHandler(cmbContactCity_SelectedIndexChanged);


                        AddItem_SelectOne(cmbContactCity);
                        AddItem_SelectOne(cmbContactState);


                        cmbContactState.SelectedIndexChanged += new EventHandler(cmbContactState_SelectedIndexChanged);
                        cmbContactCity.SelectedIndexChanged += new EventHandler(cmbContactCity_SelectedIndexChanged);
                    }
                }
                ValidateStatus(cmbContactCountry, lblContactCountry, yesNo);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Show orgnazation Tree
        /// </summary>
        private void ShowOrganizationTree()
        {
            frmTree objTree;
            int level = Common.INT_DBNULL;
            if (Convert.ToInt32(cmbType.SelectedValue) == Convert.ToInt32(Common.LocationConfigId.BO))
                level = Convert.ToInt32(Common.LocationOrgLevel.BOArea);
            else if (Convert.ToInt32(cmbType.SelectedValue) == Convert.ToInt32(Common.LocationConfigId.WH))
                level = Convert.ToInt32(Common.LocationOrgLevel.WHZone);

            objTree = new frmTree("OrganizationalLocation", "", level, this);
            Point pointTree = new Point();
            pointTree = this.PointToScreen(txtOrgLevel.Location);
            pointTree.Y = pointTree.Y + Common.TREE_COMP_Y;
            pointTree.X = pointTree.X + Common.TREE_COMP_X;
            objTree.Location = pointTree;
            objTree.ShowDialog();
        }

        /// <summary>
        /// Show data in textboxes for Contact Location
        /// </summary>
        /// <param name="e"></param>
        private void SelectContactGridRow(DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 & e.ColumnIndex > 0)
            {
                txtContactEmail.Text = dgvLocationContact.Rows[e.RowIndex].Cells["Email"].Value.ToString().Trim();

                var contactSelect = (from p in m_lstContact where p.Email1.Trim() == txtContactEmail.Text.Trim() select p);

                m_Contact = contactSelect.ToList()[0];

                errContact.Clear();
                cmbContactTitle.SelectedValue = Convert.ToInt32(m_Contact.Title);
                txtContactFirstName.Text = m_Contact.FirstName.Trim();
                txtContactLastName.Text = m_Contact.LastName.Trim();
                cmbContactTitle.SelectedValue = Convert.ToInt32(m_Contact.Title);
                txtContactMiddleName.Text = m_Contact.MiddleName.Trim();
                txtContactAddress1.Text = m_Contact.Address1.Trim();
                txtContactAddress2.Text = m_Contact.Address2.Trim();
                txtContactAddress3.Text = m_Contact.Address3.Trim();
                cmbContactCountry.SelectedValue = m_Contact.CountryId;
                cmbContactState.SelectedValue = m_Contact.StateId;
                cmbContactCity.SelectedValue = Convert.ToInt32(m_Contact.CityId);
                txtContactPinCode.Text = m_Contact.PinCode.Trim();
                txtContactPhone.Text = m_Contact.PhoneNumber1.Trim();
                txtContactMobile.Text = m_Contact.Mobile1.Trim();
                txtContactFax.Text = m_Contact.Fax1.Trim();
                txtContactEmail.Text = m_Contact.Email1.Trim();
                txtContactDesignation.Text = m_Contact.Designation.Trim();
                cmbContactStatus.SelectedValue = m_Contact.Status;
                txtContactWebSite.Text = m_Contact.Website.Trim();
                chkIsPrimaryContact.Checked = m_Contact.IsPrimary;
                // m_selectedRowNum = m_Contact.RowNum;
                m_selectedRowNum = m_Contact.RowNum;
                m_selectContactId = Convert.ToInt32(m_Contact.ContactId == string.Empty ? Common.INT_DBNULL.ToString() : m_Contact.ContactId);
                m_selectedRowIndex = e.RowIndex;
                m_contactModifiedDate = m_Contact.ModifiedDate;

                if (m_Contact.Status == 1)
                    m_initialPrimaryState = m_Contact.IsPrimary;
                else
                    m_initialPrimaryState = false;

                m_lstModifiedContactId.Add(m_Contact.ContactId);

                m_lstContactString.Remove(m_Contact.Email1);
            }
        }

        /// <summary>
        /// This function is used to show data in controls
        /// </summary>
        /// <param name="e"></param>
        private void SelectGridRow(DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {

            }
        }

        /// <summary>
        /// To search records
        /// </summary>
        private List<LocationHierarchy> Search(string locationCode, string locationName, string parentLocatioName, int parentHierarchyId, int hierarchyType, int status, string address1, string address2, int country, int state, int city, int LiveLocation, string distributorId, int? IsMiniBranchValue, int RegAddressLocationId)
        {
            List<LocationHierarchy> m_locListSearch = new List<LocationHierarchy>();
            LocationHierarchy loc = new LocationHierarchy();

            int parentId = Common.INT_DBNULL;
            loc.HierarchyCode = DBNull.Value.ToString();
            loc.HierarchyName = DBNull.Value.ToString();
            loc.ParentHierarchyName = DBNull.Value.ToString();

            //parentId = m_selectedParentId == Common.INT_DBNULL ? loc.ParentHierarchyId : m_selectedParentId;

            loc.HierarchyCode = locationCode;
            loc.HierarchyName = locationName;
            loc.ParentHierarchyName = parentLocatioName;
            loc.ParentHierarchyId = parentHierarchyId;
            loc.HierarchyType = hierarchyType;
            loc.CountryId = Convert.ToInt32(country);
            loc.StateId = Convert.ToInt32(state);
            loc.CityId = Convert.ToInt32(city);
            loc.Address1 = address1;
            loc.Address2 = address2;
            loc.LiveLocation = LiveLocation;
            loc.Status = status;
            loc.DistributorId = distributorId;
            loc.IsMiniBranch = IsMiniBranchValue;
            loc.RegAddLocationId = RegAddressLocationId;

            m_locListSearch = loc.Search();

            return m_locListSearch;
        }

        /// <summary>
        /// reset controls for search 
        /// </summary>
        private void ResetControl()
        {
            VisitControls visitControls = new VisitControls();
            visitControls.ResetAllControlsInPanel(errLocation, pnlSearchHeader);
            cmbType.Enabled = true;
            m_lstContact = null;

            btnSaveLocation.Enabled = m_isSaveAvailable;
            btnSearch.Enabled = m_isSearchAvailable;
            dgvLocationContact.DataSource = new List<Contact>();
            dgvLocation.DataSource = new List<LocationHierarchy>();

            chkIsMiniBranch.CheckState = CheckState.Indeterminate;
            txtDistributorId.Enabled = false;

            m_selectedLocationHierarchyId = Common.INT_DBNULL;
            m_selectedOrgHierarchyId = Common.INT_DBNULL;
            m_selectedOrgConfigId = Common.INT_DBNULL;
            m_locationTerminals = null;
            m_LocationHierarchy = null;
            cmbStatus.SelectedValue = 1;
            cmbType.Focus();
            ResetContactControl();
        }

        /// <summary>
        /// fn. to Validate Location Type
        /// </summary>
        /// <param name="yesNo"></param>
        private void cmbTypeValidation(bool yesNo)
        {
            if (yesNo)
            {
                cmbState.SelectedIndexChanged -= new EventHandler(cmbState_SelectedIndexChanged);
                cmbCity.SelectedIndexChanged -= new EventHandler(cmbCity_SelectedIndexChanged);


                //cmbTaxJurisdiction.SelectedIndex = 0;
                cmbCountry.SelectedIndex = 0;
                txtDistributorId.Text = string.Empty;
                AddItem_SelectOne(cmbState);
                AddItem_SelectOne(cmbGrade);
                txtStartingAmount.Text = string.Empty;
                txtOrgLevel.Text = string.Empty;
                txtCstNo.Text = string.Empty;
                txtTinNo.Text = string.Empty;
                txtVAT.Text = string.Empty;

                cmbState.SelectedIndexChanged += new EventHandler(cmbState_SelectedIndexChanged);
                cmbCity.SelectedIndexChanged += new EventHandler(cmbCity_SelectedIndexChanged);

            }
            txtCstNo.ReadOnly = false;
            txtTinNo.ReadOnly = false;
            txtVAT.ReadOnly = false;

            txtTinNo.ReadOnly = true;
            txtCstNo.ReadOnly = true;
            txtVAT.ReadOnly = true;
            btnShowOrganization.Enabled = false;

            txtStartingAmount.Enabled = false;
            cmbParentLocation.Enabled = false;
            //cmbTaxJurisdiction.Enabled = false;
            cmbGrade.Enabled = false;
            //cmbDistributor.Enabled = false;
            txtDistributorId.Enabled = false;
            cmbTaxJurisdiction.Enabled = false;

            int? miniBranchIntValue = null;
            if (chkIsMiniBranch.CheckState != CheckState.Indeterminate && chkIsMiniBranch.Checked)
            {
                miniBranchIntValue = 1;

            }
            else if (chkIsMiniBranch.CheckState != CheckState.Indeterminate && chkIsMiniBranch.Checked == false)
            {
                miniBranchIntValue = 0;

            }
            else if (chkIsMiniBranch.CheckState == CheckState.Indeterminate)
            {
                miniBranchIntValue = -1;

            }

            if (cmbType.SelectedIndex > 0)
            {
                if (Convert.ToInt32(cmbType.SelectedValue) == Convert.ToInt32(Common.LocationConfigId.BO))
                {

                    //cmbTaxJurisdiction.Enabled = true;
                    btnShowOrganization.Enabled = true;
                    txtCstNo.ReadOnly = false;
                    txtVAT.ReadOnly = false;
                    txtTinNo.ReadOnly = false;

                    cmbParentLocation.Enabled = true;

                    cmbTaxJurisdiction.Enabled = true;

                    if (yesNo)
                    {
                        //Fill WH- Replineshment Location Id
                        List<LocationHierarchy> lst_hierarchy = new List<LocationHierarchy>();
                        lst_hierarchy = Search(string.Empty, string.Empty, string.Empty, Common.INT_DBNULL, Convert.ToInt32(Common.LocationConfigId.WH), Common.INT_DBNULL, string.Empty, string.Empty, Common.INT_DBNULL, Common.INT_DBNULL, Common.INT_DBNULL, Common.INT_DBNULL, string.Empty, miniBranchIntValue, Common.INT_DBNULL);

                        //var qry = from p in lst_hierarchy where p.HierarchyConfigId == (int)Common.LocationConfigId.WH select p;

                        //lst_hierarchy = (List<LocationHierarchy>)qry.ToList();
                        if (lst_hierarchy != null)
                        {
                            LocationHierarchy loc_hierarchy = new LocationHierarchy();
                            loc_hierarchy.HierarchyId = Common.INT_DBNULL;
                            loc_hierarchy.HierarchyName = Common.SELECT_ONE;
                            lst_hierarchy.Add(loc_hierarchy);

                            string hierarchyId = "HierarchyId Asc";
                            lst_hierarchy.Sort((new GenericComparer<BusinessObjects.LocationHierarchy>(hierarchyId.Split(' ')[0], hierarchyId.Split(' ')[1] == "Asc" ? SortDirection.Ascending : SortDirection.Descending)).Compare);

                            cmbParentLocation.DataSource = lst_hierarchy;
                            cmbParentLocation.ValueMember = Common.HIERARCHY_ID;
                            cmbParentLocation.DisplayMember = Common.HIERARCHY_NAME;
                        }
                    }
                    // txtOrgLevel.Text = "L4";
                }
                else if (Convert.ToInt32(cmbType.SelectedValue) == Convert.ToInt32(Common.LocationConfigId.WH))
                {
                    //cmbTaxJurisdiction.Enabled = true;
                    txtCstNo.ReadOnly = false;
                    txtVAT.ReadOnly = false;
                    txtTinNo.ReadOnly = false;

                    btnShowOrganization.Enabled = true;
                    cmbTaxJurisdiction.Enabled = true;
                    //txtOrgLevel.Text = "L2";
                }
                else if (Convert.ToInt32(cmbType.SelectedValue) == Convert.ToInt32(Common.LocationConfigId.PC))
                {
                    txtStartingAmount.Enabled = true;
                    //cmbDistributor.Enabled = true;
                    txtDistributorId.Enabled = true;
                    txtCstNo.ReadOnly = false;
                    txtVAT.ReadOnly = false;
                    cmbParentLocation.Enabled = true;
                    cmbGrade.Enabled = true;
                    cmbTaxJurisdiction.Enabled = false;
                    cmbTaxJurisdiction.SelectedValue = Common.INT_DBNULL;

                    //Fill Parent Location
                    List<LocationHierarchy> lst_hierarchy = new List<LocationHierarchy>();
                    lst_hierarchy = Search(string.Empty, string.Empty, string.Empty, Common.INT_DBNULL, Convert.ToInt32(Common.LocationConfigId.BO), Common.INT_DBNULL, string.Empty, string.Empty, Common.INT_DBNULL, Common.INT_DBNULL, Common.INT_DBNULL, Common.INT_DBNULL, string.Empty, miniBranchIntValue, Common.INT_DBNULL);
                    btnSaveLocation.Enabled = m_isSaveAvailable;
                    if (yesNo)
                    {

                        txtCstNo.ReadOnly = true;
                        txtTinNo.ReadOnly = true;
                        txtVAT.ReadOnly = true;

                        LocationHierarchy loc_hierarchy = new LocationHierarchy();
                        loc_hierarchy.HierarchyId = Common.INT_DBNULL;
                        loc_hierarchy.HierarchyName = Common.SELECT_ONE;
                        lst_hierarchy.Add(loc_hierarchy);

                        string hierarchyId = "HierarchyId Asc";
                        lst_hierarchy.Sort((new GenericComparer<BusinessObjects.LocationHierarchy>(hierarchyId.Split(' ')[0], hierarchyId.Split(' ')[1] == "Asc" ? SortDirection.Ascending : SortDirection.Descending)).Compare);

                        if (lst_hierarchy != null)
                        {
                            cmbParentLocation.DataSource = lst_hierarchy;
                            cmbParentLocation.ValueMember = Common.HIERARCHY_ID;
                            cmbParentLocation.DisplayMember = Common.HIERARCHY_NAME;
                        }
                        //Grade 
                        DataTable dtGrade = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter("GRADE", 0, 0, 0));
                        cmbGrade.DataSource = dtGrade;
                        cmbGrade.DisplayMember = Common.KEYVALUE1;
                        cmbGrade.ValueMember = Common.KEYCODE1;
                    }

                }
                errLocation.SetError(cmbType, string.Empty);
            }
            else if (cmbType.SelectedIndex == 0 && yesNo == false)
                errLocation.SetError(cmbType, Common.GetMessage("INF0026", lblLocationType.Text.Trim().Substring(0, lblLocationType.Text.Trim().Length - 2)));
        }

        /// <summary>
        /// fn. to Validate Email
        /// </summary>
        /// <param name="txt"></param>
        /// <param name="lbl"></param>
        private void ValidateLocationEmail(TextBox txt, Label lbl, Boolean yesNo)
        {
            bool isValidEMailID = Validators.IsValidEmailID(txt.Text, true);
            if (isValidEMailID == false && yesNo == false)
                errLocation.SetError(txt, Common.GetMessage("VAL0009", lbl.Text.Trim().Substring(0, lbl.Text.Trim().Length - 1)));
            else //if (isValidEMailID == true)
                errLocation.SetError(txt, string.Empty);
        }

        /// <summary>
        /// fn. to Validate Email
        /// </summary>
        /// <param name="txt"></param>
        /// <param name="lbl"></param>
        private void ValidateEmail(TextBox txt, Label lbl, Boolean yesNo)
        {
            bool isValidEMailID = Validators.IsValidEmailID(txt.Text, false);
            if (isValidEMailID == false && yesNo == false)
                errContact.SetError(txt, Common.GetMessage("VAL0009", lbl.Text.Trim().Substring(0, lbl.Text.Trim().Length - 2)));
            else //if (isValidEMailID == true)
                errContact.SetError(txt, string.Empty);
        }

        /// <summary>
        /// Validate Tax Jurisdiction
        /// </summary>
        //private void ValidateTaxJurisdiction(Boolean yesNo)
        //{
        //    if ((Convert.ToInt32(cmbType.SelectedValue) == Convert.ToInt32(Common.LocationConfigId.WH)) || (Convert.ToInt32(cmbType.SelectedValue) == Convert.ToInt32(Common.LocationConfigId.BO)))
        //        ValidateLocationCombo(cmbTaxJurisdiction, lblTaxJurisdiction, false);
        //    else
        //        errLocation.SetError(cmbTaxJurisdiction, string.Empty);
        //    //ValidateLocationCombo(cmbDistributor, lblDistributor, true);
        //}

        /// <summary>
        /// Validate Starting Amount
        /// </summary>
        private void ValidateStartingAmount(Boolean yesNo)
        {
            if (Convert.ToInt32(cmbType.SelectedValue) == Convert.ToInt32(Common.LocationConfigId.PC))
            {
                bool isValidAmount = CoreComponent.Core.BusinessObjects.Validators.IsValidAmount(txtStartingAmount.Text);

                if (isValidAmount == false && yesNo == false)
                    errLocation.SetError(txtStartingAmount, Common.GetMessage("VAL0009", lblStartingAmount.Text.Trim().Substring(0, lblStartingAmount.Text.Trim().Length - 2)));
                else
                    errLocation.SetError(txtStartingAmount, string.Empty);
                /*
                ValidatedLocationText(txtStartingAmount, lblStartingAmount);

                if (txtStartingAmount.Text.Length > 0)
                {
                    bool isDouble = CoreComponent.BusinessObjects.Validators.IsDecimal(txtStartingAmount.Text);
                    if (isDouble == false)
                        errLocation.SetError(txtStartingAmount, Common.GetMessage("VAL0009", lblStartingAmount.Text.Trim().Substring(0, lblStartingAmount.Text.Trim().Length - 2)));
                    else
                        errLocation.SetError(txtStartingAmount, string.Empty);
                }*/
            }
            else
                errLocation.SetError(txtStartingAmount, string.Empty);
        }

        /// <summary>
        /// Validate Grade
        /// </summary>
        private void ValidateGrade(Boolean yesNo)
        {
            if ((cmbType.SelectedIndex > 0) && (Convert.ToInt32(cmbType.SelectedValue) == Convert.ToInt32(Common.LocationConfigId.PC)) && (cmbGrade.SelectedIndex <= 0))
                ValidateLocationCombo(cmbGrade, lblGrade, yesNo);
            else
                errLocation.SetError(cmbGrade, string.Empty);
        }
        #endregion

        #region Events

        #region ValidateLocation
        /// <summary>
        /// Call function CountryIndexChange to validate Country
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CountryIndexChange(true);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Call function StateIndexChange to validate State
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbState_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                StateIndexChange(true);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Call fn. cmbTypeValidation to Validate Location Type
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt16(cmbType.SelectedValue) == 1)
                    txtLocationCode.ReadOnly = true;
                else
                    txtLocationCode.ReadOnly = false;
                cmbTypeValidation(true);

                if (cmbType.SelectedValue.ToString() == Convert.ToInt32(Common.LocationConfigId.BO).ToString())
                {
                    EnableMiniBranchControl(true);
                }
                else
                {
                    EnableMiniBranchControl(false);
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Call fn. ValidateLocationCombo to Validate Tax Jurisdiction
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        //private void cmbTaxJurisdiction_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        ValidateTaxJurisdiction(true);
        //    }
        //    catch (Exception ex)
        //    {
        //        Common.LogException(ex);
        //        MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        /// <summary>
        /// Call fn. ValidatedLocationText to Validate Amount
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtStartingAmount_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                ValidateStartingAmount(true);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Call fn. ValidateLocationCombo to Validate City
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ValidateLocationCombo(cmbCity, lblCity, true);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Call fn. ValidatedLocationText to Validate Name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtLocationName_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                ValidatedLocationText(txtLocationName, lblLocationName, true);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Call fn. ValidatedLocationText to Validate AddressLine 1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtAddressLine1_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                ValidatedLocationText(txtAddressLine1, lblAddressLine1, true);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Call fn. ValidatedLocationText to Validate AddressLine 2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtAddressLine2_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                ValidatedLocationText(txtAddressLine2, lblAddressLine2, true);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Call fn. ValidateLocationCombo to Validate Grade
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbGrade_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ValidateGrade(true);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Call fn. ValidateLocationCombo to Validate Parent Location
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbParentLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ValidateParentLocation(true);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void txtLocationCode_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                ValidatedLocationText(txtLocationCode, lblLocationCode, true);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Call fn. ValidatedLocationText to validate Pin Code
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPinCode_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                ValidatePinCode(errLocation, txtPinCode, lblPinCode, true);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ValidateLocationCombo(cmbStatus, lblStatus, true);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region ValidateContact
        /// <summary>
        /// Call fn. ValidateContactTitle() to Validate Contact Title
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbContactTitle_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ValidateStatus(cmbContactTitle, lblContactTitle, true);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Call fn. ValidateText() to Validate Contact Title
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtContactFirstName_Validated(object sender, EventArgs e)
        {
            try
            {
                ValidatedText(txtContactFirstName, lblContactFirstName, true);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Call fn. to Validate Last Name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtContactLastName_Validated(object sender, EventArgs e)
        {
            try
            {
                ValidatedText(txtContactLastName, lblContactLastName, true);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Call fn. to Validate Address1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtContactAddress1_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                ValidatedText(txtContactAddress1, lblContactAddress1, true);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Call fn. to Validate Address2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtContactAddress2_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                ValidatedText(txtContactAddress2, lblContactAddress2, true);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Call fn. to Validate Pin Code
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtContactPinCode_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                ValidatedText(txtContactPinCode, lblContactPinCode, true);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Validate Status drop down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbContactStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ValidateStatus(cmbContactStatus, lblContactStatus, true);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Fill State drop down and Validate Country drop down when country index change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbContactCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ContactCountry(true);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Call fn. to Validate Contact City
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbContactCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ValidateStatus(cmbContactCity, lblContactCity, true);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Call fn. to Validate Email
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtContactEmail_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                ValidateEmail(txtContactEmail, lblContactEmail, true);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Call fn.StateContactIndexChange to Validate State
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbContactState_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                StateContactIndexChange(true);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        /// <summary>
        /// Call fn. Search to Bind Grid 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                int? miniBranchIntValue = null;
                if (chkIsMiniBranch.CheckState != CheckState.Indeterminate && chkIsMiniBranch.Checked)
                {
                    miniBranchIntValue = 1;

                }
                else if (chkIsMiniBranch.CheckState != CheckState.Indeterminate && chkIsMiniBranch.Checked == false)
                {
                    miniBranchIntValue = 0;

                }
                else if (chkIsMiniBranch.CheckState != CheckState.Indeterminate)
                {
                    miniBranchIntValue = -1;

                }

                errLocation.Clear();
                m_locList = Search(txtLocationCode.Text.Trim(), txtLocationName.Text.Trim(), cmbParentLocation.Text.Trim(), Convert.ToInt32(cmbParentLocation.SelectedValue), Convert.ToInt32(cmbType.SelectedValue.ToString()), Convert.ToInt32(cmbStatus.SelectedValue.ToString()), txtAddressLine1.Text.Trim(), txtAddressLine2.Text.Trim(), Convert.ToInt32(cmbCountry.SelectedValue.ToString()), Convert.ToInt32(cmbState.SelectedValue.ToString()), Convert.ToInt32(cmbCity.SelectedValue.ToString()), Convert.ToInt32(cmbLiveLocation.SelectedValue), txtDistributorId.Text.Trim(), miniBranchIntValue, Convert.ToInt32(cmbRegAddressLocation.SelectedValue));
                if ((m_locList != null) && (m_locList.Count > 0))
                    dgvLocation.DataSource = m_locList;
                else
                {
                    dgvLocation.DataSource = new List<LocationHierarchy>();
                    MessageBox.Show(Common.GetMessage("8002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Call fn. AddContact to Add Contact 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddDetails_Click(object sender, EventArgs e)
        {
            try
            {
                AddContact();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        ///  Call fn. ResetContactControl to Reset Contact Controls
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearDetails_Click(object sender, EventArgs e)
        {
            try
            {
                ResetContactControl();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Call fn. SaveLocation to save Location Record
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveLocation_Click(object sender, EventArgs e)
        {
            try
            {
                SaveLocation();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Show org. tree hierarchy
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnShowOrganization_Click(object sender, EventArgs e)
        {
            try
            {
                ShowOrganizationTree();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Call function SelectGridRow, when user select a row
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvLocation_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                EditLocation(e.RowIndex, e.ColumnIndex);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Call fn. ResetControl to Reset Search Controls
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearchReset_Click(object sender, EventArgs e)
        {
            try
            {
                ResetControl();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Call fn. SelectContactGridRow
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvLocationContact_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                SelectContactGridRow(e);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Remove Location Contact
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvLocationContact_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        /// <summary>
        /// Select Contact tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControlTransaction_Selecting(object sender, TabControlCancelEventArgs e)
        {
            try
            {
                if (tabControlTransaction.SelectedIndex == 1 & (cmbType.SelectedIndex == 0 || txtLocationCode.Text.Trim().Length == 0))// & tabControlTransaction.TabPages[1].Text.Equals("Contact", StringComparison.InvariantCultureIgnoreCase))
                {
                    DialogResult result = MessageBox.Show(Common.GetMessage("VAL0003", lblLocationCode.Text.Substring(0, lblLocationCode.Text.Trim().Length - 2), lblLocationType.Text.Substring(0, lblLocationType.Text.Trim().Length - 2)), Common.GetMessage("10001"));
                    e.Cancel = true;
                }
                else
                {
                    ResetContactControl();
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /*
        /// <summary>
        /// Validate Distributor Id 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbDistributor_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ValidateDistributor(true);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
         * 
         */



        private void btnAddTerminal_Click(object sender, EventArgs e)
        {
            try
            {
                if ((Convert.ToInt32(cmbType.SelectedValue) == (int)Common.LocationConfigId.HO) || (Convert.ToInt32(cmbType.SelectedValue) == (int)Common.LocationConfigId.WH))
                {
                    MessageBox.Show(Common.GetMessage("VAL0066", cmbType.Text.ToString()), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                int locationId = Common.INT_DBNULL;
                if (m_LocationHierarchy != null)
                    locationId = m_LocationHierarchy.HierarchyId;

                frmLocationTerminal objFrmLocationTerminal = new frmLocationTerminal(locationId, m_locationTerminals);

                (objFrmLocationTerminal as frmLocationTerminal).TerminalAdded += new TerminalHandler(Method_Terminal_Add);
                DialogResult dResult = objFrmLocationTerminal.ShowDialog();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void txtLocationCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyValue == 32)
                {
                    e.SuppressKeyPress = true;
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        private void dgvLocation_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if ((e.RowIndex >= 0) && (dgvLocation.Rows[e.RowIndex].Cells[e.ColumnIndex].GetType() == typeof(DataGridViewImageCell)))
                {
                    //pnlSearchHeader.Enabled = true;
                    btnSaveLocation.Enabled = m_isSaveAvailable;
                    btnSearch.Enabled = false;
                    EditLocation(e.RowIndex, e.ColumnIndex);
                }
                //EditLocation(e.RowIndex, e.ColumnIndex);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Edit Location
        /// </summary>
        /// <param name="e"></param>
        private void EditLocation(int rowIndex, int columnIndex)
        {
            if (rowIndex >= 0)
            {
                pnlSearchButtons.Enabled = true;
                txtLocationCode.Text = dgvLocation.Rows[rowIndex].Cells["Code"].Value.ToString().Trim();

                ////Get ParentId
                var orgSelect = (from p in m_locList where p.HierarchyCode.Trim() == txtLocationCode.Text.Trim() select p);

                if (orgSelect.ToList().Count == 0)
                    return;

                errLocation.Clear();

                m_LocationHierarchy = orgSelect.ToList()[0];
                cmbGrade.SelectedValue = m_LocationHierarchy.Grade.ToString();
                cmbType.Enabled = false;
                cmbType.SelectedValue = Convert.ToInt32(m_LocationHierarchy.HierarchyConfigId);
                txtLocationName.Text = m_LocationHierarchy.HierarchyName.Trim().ToString();
                txtShortName.Text = m_LocationHierarchy.ShortName.Trim().ToString();

                txtAddressLine1.Text = m_LocationHierarchy.Address1.Trim().ToString();
                txtAddressLine2.Text = m_LocationHierarchy.Address2.Trim().ToString();
                txtAddressLine3.Text = m_LocationHierarchy.Address3.Trim().ToString();

                cmbCountry.SelectedValue = Convert.ToInt32(m_LocationHierarchy.CountryId);
                cmbState.SelectedValue = Convert.ToInt32(m_LocationHierarchy.StateId);
                cmbCity.SelectedValue = Convert.ToInt32(m_LocationHierarchy.CityId);
                txtDistributorId.Text = (m_LocationHierarchy.DistributorId == "-1" ? "" : m_LocationHierarchy.DistributorId);
                txtPinCode.Text = m_LocationHierarchy.PinCode.Trim();
                txtEmail.Text = m_LocationHierarchy.Email1.Trim();
                txtFax.Text = m_LocationHierarchy.Fax1.Trim();
                txtMobile.Text = m_LocationHierarchy.Mobile1.Trim();
                m_selectedOrgHierarchyLevel = m_LocationHierarchy.OrgLevel;

                cmbParentLocation.SelectedValue = Convert.ToInt32(m_LocationHierarchy.ReplenishmentLocationId);
                txtPhone.Text = m_LocationHierarchy.PhoneNumber1;
                txtStartingAmount.Text = Math.Round(m_LocationHierarchy.StartingAmount == Common.INT_DBNULL ? 0 : m_LocationHierarchy.StartingAmount, 2).ToString();
                txtStartingAmount.Text = txtStartingAmount.Text == Common.INT_DBNULL.ToString() ? string.Empty : txtStartingAmount.Text;
                txtDescription.Text = m_LocationHierarchy.Description.ToString();
                //cmbGrade.SelectedValue = m_LocationHierarchy.Garde.ToString();

                txtTinNo.Text = m_LocationHierarchy.TinNo.ToString();
                txtVAT.Text = m_LocationHierarchy.VatNo.ToString();
                txtCstNo.Text = m_LocationHierarchy.CstNo.ToString();
                cmbLiveLocation.SelectedValue = Convert.ToInt32(m_LocationHierarchy.LiveLocation);

                if (!string.IsNullOrEmpty(m_LocationHierarchy.IECCode))
                    txtIECCode.Text = m_LocationHierarchy.IECCode.Trim();

                if (m_LocationHierarchy.IsMiniBranch.HasValue && m_LocationHierarchy.IsMiniBranch == 1)
                {
                    chkIsMiniBranch.Checked = true;
                    chkIsMiniBranch.CheckState = CheckState.Checked;
                }
                else if (m_LocationHierarchy.IsMiniBranch.HasValue && m_LocationHierarchy.IsMiniBranch == 0)
                {
                    chkIsMiniBranch.Checked = true;
                    chkIsMiniBranch.CheckState = CheckState.Unchecked;
                }
                else if (m_LocationHierarchy.IsMiniBranch.HasValue && m_LocationHierarchy.IsMiniBranch == -1)
                {
                    chkIsMiniBranch.CheckState = CheckState.Indeterminate;
                }

                if ((cmbType.SelectedValue.ToString() == "2") || (cmbType.SelectedValue.ToString() == "3"))
                    cmbTaxJurisdiction.SelectedValue = m_LocationHierarchy.TaxJurisdiction;
                else
                    cmbTaxJurisdiction.SelectedValue = Common.INT_DBNULL;

                //cmbTaxJurisdiction.SelectedValue = Convert.ToInt32(m_LocationHierarchy.TaxJurisdiction.Trim() == string.Empty ? Common.INT_DBNULL.ToString() : m_LocationHierarchy.TaxJurisdiction);
                cmbStatus.SelectedValue = Convert.ToInt32(m_LocationHierarchy.Status);
                m_modifiedDate = m_LocationHierarchy.ModifiedDate.ToString();

                txtOrgLevel.Text = m_LocationHierarchy.OrgName;

                m_selectedLocationHierarchyId = Convert.ToInt32(m_LocationHierarchy.HierarchyId);
                m_selectedOrgHierarchyId = Convert.ToInt32(m_LocationHierarchy.OrgHierarchyId);
                m_selectedOrgConfigId = Convert.ToInt32(m_LocationHierarchy.OrgConfigId);

                cmbRegAddressLocation.SelectedValue = m_LocationHierarchy.RegAddLocationId.ToString();

                LocationHierarchy loc = new LocationHierarchy();

                m_lstContact = loc.ContactSearch(m_selectedLocationHierarchyId);

                //  btnSaveLocation.Enabled = m_isSaveAvailable;               
                dgvLocationContact.DataSource = new List<Contact>();
                if ((m_lstContact != null) && (m_lstContact.Count > 0))
                {
                    ResetContactControl();
                    dgvLocationContact.DataSource = m_lstContact;
                    var query = (from p in m_lstContact where p.Status == 1 select p.Email1);
                    m_lstContactString = query.ToList();
                }
                m_locationTerminals = null;

                // Added by Anubhav on 15 July 2010
                LocationTerminal lt = new LocationTerminal();
                List<LocationTerminal> lstTerminal = new List<LocationTerminal>();
                lstTerminal = lt.TerminalSearch(m_LocationHierarchy.HierarchyId);
                if (lstTerminal != null && lstTerminal.Count > 0)
                    m_locationTerminals = lstTerminal;
            }
        }

        private void dgvLocation_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvLocation.SelectedRows.Count > 0)
                {
                    EditLocation(dgvLocation.SelectedRows[0].Index, -1);
                    btnSaveLocation.Enabled = false;
                    btnSearch.Enabled = m_isSearchAvailable;
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void txtDistributorId_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyValue == Common.F4KEY && !e.Alt)
                {
                    System.Collections.Specialized.NameValueCollection nvc = new NameValueCollection();
                    nvc.Add("DistributorStatus", "2");
                    CoreComponent.Controls.frmSearch objfrmSearch = new CoreComponent.Controls.frmSearch(CoreComponent.Controls.SearchTypes.DistributorSearch, nvc);
                    CoreComponent.BusinessObjects.Distributor _distributor = (Distributor)objfrmSearch.ReturnObject;
                    objfrmSearch.ShowDialog();
                    _distributor = (Distributor)objfrmSearch.ReturnObject;

                    if (_distributor != null)
                    {
                        txtDistributorId.Text = _distributor.DistributorId.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtDistributorId_Validated(object sender, EventArgs e)
        {
            try
            {
                if (txtDistributorId.Text.Length > 0)
                    ValidateDistributor(true);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //private void chkIsMiniBranch_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (chkIsMiniBranch.Checked)
        //    {
        //        txtDistributorId.Enabled = true;
        //        pictureBox1.Enabled = true;
        //    }
        //    else if (chkIsMiniBranch.Checked == false)
        //    {
        //        txtDistributorId.Enabled = false;
        //        pictureBox1.Enabled = false;
        //    }

        //}

        private void EnableMiniBranchControl(bool miniBranchEnabled)
        {
            chkIsMiniBranch.Enabled = miniBranchEnabled;

            if (chkIsMiniBranch.Enabled == false)
                chkIsMiniBranch.Checked = false;
        }

        private void chkIsMiniBranch_CheckStateChanged(object sender, EventArgs e)
        {
            if (cmbType.SelectedValue.ToString() == Convert.ToInt32(Common.LocationConfigId.BO).ToString() &&
                chkIsMiniBranch.CheckState == CheckState.Checked)
            {
                txtDistributorId.Enabled = true;
                pictureBox1.Enabled = true;
            }
            else if (cmbType.SelectedValue.ToString() == Convert.ToInt32(Common.LocationConfigId.BO).ToString() &&
                (chkIsMiniBranch.CheckState == CheckState.Unchecked || chkIsMiniBranch.CheckState == CheckState.Indeterminate))
            {
                txtDistributorId.Enabled = false;
                pictureBox1.Enabled = false;
            }
            else if (cmbType.SelectedValue.ToString() == Convert.ToInt32(Common.LocationConfigId.PC).ToString())
            {
                txtDistributorId.Enabled = true;
                pictureBox1.Enabled = true;
                chkIsMiniBranch.CheckState = CheckState.Indeterminate;
            }
        }

        private void pnlSearchHeader_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblShortName_Click(object sender, EventArgs e)
        {

        }

        private void lblIsRegAddress_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }



        private void cmbRegAddressLocation_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
