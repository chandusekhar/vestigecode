using System;
using System.Collections.Generic;
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
using CoreComponent.MasterData.BusinessObjects;

namespace CoreComponent.MasterData.UI
{
    public partial class frmVendor : CoreComponent.Core.UI.Transaction
    {
        private const string CON_GRIDCOL_EDIT = "Edit";
        private const string CON_GRIDCOL_REMOVE = "Remove";

        #region Variables
        List<Vendor> m_vendorList;
        Vendor m_objVendor;
        List<Contact> m_lstContact;
        List<Contact> m_lstRemovedContact = new List<Contact>();
        List<string> m_lstContactString;
        List<string> m_lstModifiedContactId = new List<string>();
        string m_contactModifiedDate = string.Empty;

        Contact m_Contact;

        int m_vendorId = Common.INT_DBNULL;
        int m_selectedRowNum = Common.INT_DBNULL;
        int m_selectedRowIndex = Common.INT_DBNULL;
        int m_selectContactId = Common.INT_DBNULL;
        bool m_initialPrimaryState = false;
        string m_modifiedDate = Common.DATETIME_NULL.ToString();

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

        #region C'frmVendor
        public frmVendor()
        {
            try
            {
                lblPageTitle.Text = "Vendor";
                m_isSaveAvailable = Authenticate.IsFunctionAccessible(strUserName, strLocationCode, Vendor.MODULE_CODE, Common.FUNCTIONCODE_SAVE);
                m_isSearchAvailable = Authenticate.IsFunctionAccessible(strUserName, strLocationCode, Vendor.MODULE_CODE, Common.FUNCTIONCODE_SEARCH);

                InitializeComponent();
                InitializeControls();
                txtVendorCode.Focus();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Method

        #region ValidateVendor

        void VendorCodeValidate(bool yesNo)
        {
            bool isTextBoxEmpty = CoreComponent.Core.BusinessObjects.Validators.CheckForEmptyString(txtVendorCode.Text.Length);
            if (isTextBoxEmpty == true && yesNo == false)
                errVendor.SetError(txtVendorCode, Common.GetMessage("INF0019", lblVendorCode.Text.Trim().Substring(0, lblVendorCode.Text.Trim().Length - 2)));
            else if (isTextBoxEmpty == false && yesNo == false)
                errVendor.SetError(txtVendorCode, Common.CodeValidate(txtVendorCode.Text, lblVendorCode.Text.Trim().Substring(0, lblVendorCode.Text.Trim().Length - 2)));
            else //if (isTextBoxEmpty == false)
                errVendor.SetError(txtVendorCode, string.Empty);
        }

        /// <summary>
        /// Vendor Validation Functions on Save Click
        /// </summary>
        private void ValidateVendorControls(Boolean yesNo)
        {
            ValidatedVendorText(txtVendorName, lblVendorName, yesNo);
            VendorCodeValidate(yesNo);
            //ValidatedVendorText(txtVendorCode, lblVendorCode, yesNo);
            ValidatedVendorText(txtAddressLine1, lblAddressLine1, yesNo);
            ValidatedVendorText(txtAddressLine2, lblAddressLine2, yesNo);
            ValidateVendorEmail(txtEmail, lblEmail, yesNo);

            ValidateVendorCombo(cmbWarehouse, lblWarehouse, yesNo);
            ValidatePinCode(errVendor, txtPinCode, lblPinCode, yesNo);
            CountryIndexChange(yesNo);
            StateIndexChange(yesNo);
            ValidateVendorCombo(cmbCity, lblCity, yesNo);
            //ValidateVendorCombo(cmbTaxJurisdiction, lblTaxJurisdiction, yesNo);
            ValidateVendorCombo(cmbStatus, lblStatus, yesNo);
            ValidateVendorCombo(cmbPaymentTerms, lblPaymentTerms, yesNo);
            ValidateVendorCombo(cmbTaxJurisdiction, lblTaxJurisdiction, yesNo);
        }

        /// <summary>
        /// fn. to Validate Email
        /// </summary>
        /// <param name="txt"></param>
        /// <param name="lbl"></param>
        private void ValidateVendorEmail(TextBox txt, Label lbl, Boolean yesNo)
        {
            bool isValidEMailID = Validators.IsValidEmailID(txt.Text, true);
            if (isValidEMailID == false && yesNo == false)
                errVendor.SetError(txt, Common.GetMessage("VAL0009", lbl.Text.Trim().Substring(0, lbl.Text.Trim().Length - 1)));
            else //if (isValidEMailID == true)
                errVendor.SetError(txt, string.Empty);
        }

        /// <summary>
        /// Validate Combo Box
        /// </summary>
        /// <param name="cmb"></param>
        /// <param name="lbl"></param>
        private void ValidateVendorCombo(ComboBox cmb, Label lbl, bool isCtrlEnabled)
        {
            if (cmb.SelectedIndex == 0 && isCtrlEnabled == false)
                errVendor.SetError(cmb, Common.GetMessage("INF0026", lbl.Text.Trim().Substring(0, lbl.Text.Trim().Length - 2)));
            else
                errVendor.SetError(cmb, string.Empty);
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
        private void ValidatedVendorText(TextBox txt, Label lbl, Boolean yesNo)
        {
            bool isTextBoxEmpty = CoreComponent.Core.BusinessObjects.Validators.CheckForEmptyString(txt.Text.Length);
            if (isTextBoxEmpty == true && yesNo == false)
                errVendor.SetError(txt, Common.GetMessage("INF0019", lbl.Text.Trim().Substring(0, lbl.Text.Trim().Length - 2)));
            else //if (isTextBoxEmpty == false)
                errVendor.SetError(txt, string.Empty);
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
            ValidateVendorCombo(cmbState, lblState, yesNo);
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
        /// Initialize Controls for Vendor
        /// </summary>
        private void InitializeControls()
        {
            m_lstContact = new List<Contact>();
            dgvVendorContact.AutoGenerateColumns = false;
            dgvVendorContact.DataSource = null;
            DataGridView dgvSearchNew = Common.GetDataGridViewColumns(dgvVendorContact, Environment.CurrentDirectory + "\\App_Data\\GridViewDefinition.xml");

            dgvVendorMaster.AutoGenerateColumns = false;
            dgvVendorMaster.DataSource = null;

            DataGridView dgvVendorSearch = Common.GetDataGridViewColumns(dgvVendorMaster, Environment.CurrentDirectory + "\\App_Data\\GridViewDefinition.xml");

            FillTitle();
            FillStatus();
            FillCountry();
            //FillTaxJurisdiction();
            FillPaymentTerms();
            FillWarehouse();
            ResetControl();
            AddSelectItemInCombo(cmbContactState);
            AddSelectItemInCombo(cmbContactCity);
            //FillTaxJurisdiction();

        }

        private void FillTaxJurisdiction()
        {
            DataTable dtTaxJurisdiction = Common.ParameterLookup(Common.ParameterType.TaxJurisdiction, new ParameterFilter(string.Empty, Convert.ToInt32(cmbCountry.SelectedValue), 0, 0));
            cmbTaxJurisdiction.DataSource = dtTaxJurisdiction;
            cmbTaxJurisdiction.ValueMember = "StateId";
            cmbTaxJurisdiction.DisplayMember = "StateName";
        }

        /// <summary>
        /// Remove Vendor Contact Record
        /// </summary>
        /// <param name="e"></param>
        private void RemoveVendorContact(DataGridViewCellEventArgs e)
        {
            if ((e.RowIndex >= 0) && (dgvVendorContact.Rows[e.RowIndex].Cells[e.ColumnIndex].GetType() == typeof(DataGridViewImageCell)) && (dgvVendorContact.Rows[e.RowIndex].Cells[e.ColumnIndex].OwningColumn.Name == CON_GRIDCOL_REMOVE))
            {
                if (Convert.ToInt32(dgvVendorContact.Rows[e.RowIndex].Cells[dgvVendorContact.Columns.Count - 1].Value) < 0)
                {
                    DialogResult saveResult = MessageBox.Show(Common.GetMessage("INF0028"), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (saveResult == DialogResult.Yes)
                    {
                        m_lstRemovedContact.Add(m_lstContact[e.RowIndex]);

                        dgvVendorContact.DataSource = null;
                        m_lstContact.RemoveAt(e.RowIndex);
                        dgvVendorContact.DataSource = m_lstContact;
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

        private void FillWarehouse()
        {
            DataTable dtTitle = Common.ParameterLookup(Common.ParameterType.Locations, new ParameterFilter(string.Empty, (int)Common.LocationConfigId.WH, 0, 0));
            cmbWarehouse.DataSource = dtTitle;
            cmbWarehouse.DisplayMember = "LocationName";
            cmbWarehouse.ValueMember = "LocationId";
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

            DataTable dtStatus = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter("STATUS", 0, 0, 0));
            cmbContactStatus.DataSource = dtStatus;
            cmbContactStatus.DisplayMember = Common.KEYVALUE1;
            cmbContactStatus.ValueMember = Common.KEYCODE1;
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

        /// <summary>
        /// Fill drop down for Tax Jurisdiction
        /// </summary>
        private void FillPaymentTerms()
        {
            DataTable dtPaymentTerms = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter("PAYMENTTERMS", 0, 0, 0));
            cmbPaymentTerms.DataSource = dtPaymentTerms;
            cmbPaymentTerms.DisplayMember = Common.KEYVALUE1;
            cmbPaymentTerms.ValueMember = Common.KEYCODE1.ToString();
        }


        /// <summary>
        /// Fill drop down for Distributor
        /// </summary>
        //private void FillDistributor()
        //{
        //    DataTable dtDistributor = Common.ParameterLookup(Common.ParameterType.Distributor, new ParameterFilter(string.Empty, 0, 0, 0));
        //    cmbDistributor.DataSource = dtDistributor;
        //    cmbDistributor.DisplayMember = "DistributorName";
        //    cmbDistributor.ValueMember = "DistributorId";
        //}

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
            ValidateVendorCombo(cmbCountry, lblCountry, yesNo);
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
        /// Generate Vendor Contact Error 
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

            #region Check Vendor Errors

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
            //else if (m_selectedRowIndex != Common.INT_DBNULL)
            //{
            //    if (m_lstContactString.IndexOf(txtContactEmail.Text.Trim()) >= 0)
            //    {
            //        MessageBox.Show(Common.GetMessage("VAL0007", lblContactEmail.Text.Trim().Substring(0, lblContactEmail.Text.Trim().Length - 2)), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        return;
            //    }
            //}

            if ((m_lstContact != null) && (m_lstContact.Count > 0))
            {
                primaryCount = (from p in m_lstContact where p.IsPrimary == true && p.Status == 1 select p.IsPrimary).Count();
                if (chkIsPrimaryContact.Checked)
                {
                    if (((m_initialPrimaryState == true) && (primaryCount > 1)) || ((m_initialPrimaryState == false) && (primaryCount > 0)))
                    {
                        MessageBox.Show(Common.GetMessage("VAL0008", "contact"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
            }
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

            cnt.Email1 = txtContactEmail.Text.Trim();
            cnt.Address1 = txtContactAddress1.Text.Trim();
            cnt.Address2 = txtContactAddress2.Text.Trim();
            cnt.Address3 = txtContactAddress3.Text.Trim();
            cnt.Mobile1 = txtContactMobile.Text.Trim();
            cnt.PhoneNumber1 = txtContactPhone.Text.Trim();
            cnt.Country = cmbContactCountry.Text.Trim();
            cnt.CountryId = Convert.ToInt32(cmbContactCountry.SelectedValue);
            cnt.StateId = Convert.ToInt32(cmbContactState.SelectedValue);
            cnt.State = cmbContactState.Text.Trim();
            cnt.CityId = Convert.ToInt32(cmbContactCity.SelectedValue);
            cnt.City = cmbContactCity.Text.Trim();
            cnt.Fax1 = txtContactFax.Text.Trim();
            cnt.Website = txtContactWebSite.Text.Trim();
            cnt.PinCode = txtContactPinCode.Text.Trim();
            cnt.ContactId = m_selectContactId.ToString();            
            cnt.Department = txtContactDepartment.Text.Trim();

            cnt.ModifiedBy = m_userId;
            cnt.ModifiedDate = m_contactModifiedDate;//.Trim().Length == 0 ? "1900-01-01" : m_contactModifiedDate;
            #endregion

            if (m_lstContact == null)
            {
                m_lstContact = new List<Contact>();
            }

            if (m_selectContactId != Common.INT_DBNULL)
                cnt.RowNum = Common.INT_DBNULL;//Convert.ToInt16(dgvVendorContact.Rows.Count);
            else
                cnt.RowNum = m_selectedRowNum;

            if ((m_selectedRowIndex != Common.INT_DBNULL) && (m_selectedRowIndex <= dgvVendorContact.Rows.Count))
            {
                m_lstContact.Insert(m_selectedRowIndex, cnt);
                m_lstContact.RemoveAt(m_selectedRowIndex + 1);
            }
            else
                m_lstContact.Add(cnt);


            dgvVendorContact.DataSource = new List<Contact>();
            if ((m_lstContact != null) && (m_lstContact.Count > 0))
            {
                //dgvVendorContact.DataSource = m_lstContact;

                var query = (from p in m_lstContact where p.Status == 1 select p.Email1);
                m_lstContactString = query.ToList();

                dgvVendorContact.DataSource = m_lstContact;
                // DisableButtons();
            }
            ResetContactControl();
        }

        private void DisableButtons()
        {
            foreach (DataGridViewRow dgvRow in dgvVendorContact.Rows)
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
        /// Generate and build Errors for Vendors on Save Click
        /// </summary>
        private StringBuilder GenerateVendorError()
        {
            StringBuilder sbError = new StringBuilder();
          
            if (errVendor.GetError(txtVendorCode).Trim().Length > 0)
            {
                sbError.Append(errVendor.GetError(txtVendorCode));
                sbError.AppendLine();
            }
            if (errVendor.GetError(txtVendorName).Trim().Length > 0)
            {
                sbError.Append(errVendor.GetError(txtVendorName));
                sbError.AppendLine();
            }
            if (errVendor.GetError(txtAddressLine1).Trim().Length > 0)
            {
                sbError.Append(errVendor.GetError(txtAddressLine1));
                sbError.AppendLine();
            }
            if (errVendor.GetError(txtAddressLine2).Trim().Length > 0)
            {
                sbError.Append(errVendor.GetError(txtAddressLine2));
                sbError.AppendLine();
            }
            if (errVendor.GetError(cmbWarehouse).Trim().Length > 0)
            {
                sbError.Append(errVendor.GetError(cmbWarehouse));
                sbError.AppendLine();
            }
            if (errVendor.GetError(cmbCountry).Trim().Length > 0)
            {
                sbError.Append(errVendor.GetError(cmbCountry));
                sbError.AppendLine();
            }
            if (errVendor.GetError(cmbState).Trim().Length > 0)
            {
                sbError.Append(errVendor.GetError(cmbState));
                sbError.AppendLine();
            }
            if (errVendor.GetError(cmbCity).Trim().Length > 0)
            {
                sbError.Append(errVendor.GetError(cmbCity));
                sbError.AppendLine();
            }
            if (errVendor.GetError(txtPinCode).Trim().Length > 0)
            {
                sbError.Append(errVendor.GetError(txtPinCode));
                sbError.AppendLine();
            }
            if (errVendor.GetError(txtEmail).Trim().Length > 0)
            {
                sbError.Append(errVendor.GetError(txtEmail));
                sbError.AppendLine();
            }
            if (errVendor.GetError(cmbStatus).Trim().Length > 0)
            {
                sbError.Append(errVendor.GetError(cmbStatus));
                sbError.AppendLine();
            }
            //if (errVendor.GetError(cmbTaxJurisdiction).Trim().Length > 0)
            //{
            //    sbError.Append(errVendor.GetError(cmbTaxJurisdiction));
            //    sbError.AppendLine();
            //}
            if (errVendor.GetError(cmbPaymentTerms).Trim().Length > 0)
            {
                sbError.Append(errVendor.GetError(cmbPaymentTerms));
                sbError.AppendLine();
            }
            if (errVendor.GetError(cmbTaxJurisdiction).Trim().Length > 0)
            {
                sbError.Append(errVendor.GetError(cmbTaxJurisdiction));
                sbError.AppendLine();
            }
            sbError = Common.ReturnErrorMessage(sbError);
            return sbError;
        }

        /// <summary>
        /// Save Vendor record
        /// </summary>
        private void SaveVendor()
        {
            #region ValidateVendorControls
            ValidateVendorControls(false);
            #endregion

            #region Check Vendor Errors

            StringBuilder sbError = new StringBuilder();
            sbError = GenerateVendorError();

            #endregion

            if (!sbError.ToString().Trim().Equals(string.Empty))
            {
                MessageBox.Show(sbError.ToString(), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

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
                Vendor m_locHierarchy = new Vendor();

                DialogResult saveResult = MessageBox.Show(Common.GetMessage("INF0027"), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (saveResult == DialogResult.Yes)
                {
                    if ((m_vendorId == Common.INT_DBNULL) && (Convert.ToInt32(cmbStatus.SelectedValue) == 2))
                    {
                        MessageBox.Show(Common.GetMessage("VAL0020"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    //m_locHierarchy.TaxJuridicationID = Convert.ToInt32(cmbTaxJurisdiction.SelectedValue);

                    m_locHierarchy.Address1 = txtAddressLine1.Text.Trim();
                    m_locHierarchy.Address2 = txtAddressLine2.Text.Trim();
                    m_locHierarchy.Address3 = txtAddressLine3.Text.Trim();
                    m_locHierarchy.CountryId = Convert.ToInt32(cmbCountry.SelectedValue);
                    m_locHierarchy.StateId = Convert.ToInt32(cmbState.SelectedValue);
                    m_locHierarchy.PinCode = txtPinCode.Text.Trim();
                    m_locHierarchy.PhoneNumber1 = txtPhone.Text.Trim();
                    m_locHierarchy.Email1 = txtEmail.Text.Trim();
                    m_locHierarchy.CityId = Convert.ToInt32(cmbCity.SelectedValue);

                    m_locHierarchy.Warehouse = cmbWarehouse.SelectedValue.ToString();
                    m_locHierarchy.VendorCode = txtVendorCode.Text.Trim();
                    m_locHierarchy.VendorName = txtVendorName.Text.Trim();
                    m_locHierarchy.PaymentTerms = Convert.ToInt32(cmbPaymentTerms.SelectedValue);
                    m_locHierarchy.TinNo = txtTinNo.Text.Trim();
                    m_locHierarchy.VatNo = txtVAT.Text.Trim();
                    m_locHierarchy.CstNo = txtCstNo.Text.Trim();
                    m_locHierarchy.Mobile1 = txtMobile.Text;
                    m_locHierarchy.Fax1 = txtFax.Text;
                    m_locHierarchy.Status = Convert.ToInt32(cmbState.SelectedValue);
                    m_locHierarchy.VendorID = m_vendorId;

                    m_locHierarchy.Status = Convert.ToInt32(cmbStatus.SelectedValue);
                    m_locHierarchy.ModifiedBy = m_userId;

                    m_locHierarchy.Contact = m_lstContact;

                    m_locHierarchy.ModifiedContactId = m_lstModifiedContactId;

                    m_locHierarchy.TaxJurisdictionId = cmbTaxJurisdiction.SelectedValue.ToString();

                    if (m_vendorId != Common.INT_DBNULL)
                        m_locHierarchy.ModifiedDate = m_modifiedDate.ToString();

                    m_locHierarchy.RemovedContacts = m_lstRemovedContact;

                    string errorMesage = string.Empty;
                    bool recordSaved = m_locHierarchy.Save(ref errorMesage);

                    if (errorMesage.Equals(string.Empty))
                    {
                        ResetContactControl();
                        ResetControl();
                        dgvVendorContact.DataSource = new List<Contact>();

                        m_vendorList = Search(string.Empty, string.Empty, string.Empty, Common.INT_DBNULL, string.Empty, string.Empty, Common.INT_DBNULL, Common.INT_DBNULL, Common.INT_DBNULL);
                        dgvVendorMaster.DataSource = m_vendorList;
                        dgvVendorMaster.ClearSelection();
                        MessageBox.Show(Common.GetMessage("8001"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        m_lstContact = null;
                        
                    }
                    else if (errorMesage.Equals("INF0020"))
                        MessageBox.Show(Common.GetMessage(errorMesage, lblVendorCode.Text.Trim().Substring(0, lblVendorCode.Text.Trim().Length - 2), lblVendorName.Text.Trim().Substring(0, lblVendorName.Text.Trim().Length - 2)), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            btnAddDetails.Enabled = m_isSaveAvailable;
            m_selectedRowNum = Common.INT_DBNULL;
            m_selectedRowIndex = Common.INT_DBNULL;
            m_selectContactId = Common.INT_DBNULL;
            m_initialPrimaryState = false;
            m_contactModifiedDate = string.Empty;
            AddItem_SelectOne(cmbContactCity);
            AddItem_SelectOne(cmbContactState);
            dgvVendorContact.ClearSelection();
            cmbContactTitle.Focus();
            cmbContactStatus.SelectedValue = 1;
        }

        /// <summary>
        /// bind State for Contact Vendor
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

        private void ReflectVendorContact(int rowIndex, int columnIndex)
        {
            SelectContactGridRow(rowIndex, columnIndex);
        }

        /// <summary>
        /// Show data in textboxes for Contact Vendor
        /// </summary>
        /// <param name="e"></param>
        private void SelectContactGridRow(int rowIndex, int columnIndex)
        {
            if (rowIndex >= 0 && columnIndex > 0)
            {
                txtContactEmail.Text = dgvVendorContact.Rows[rowIndex].Cells["Email"].Value.ToString().Trim();

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
                txtContactDepartment.Text = m_Contact.Department.Trim();
                // m_selectedRowNum = m_Contact.RowNum;
                m_selectedRowNum = m_Contact.RowNum;
                m_selectContactId = Convert.ToInt32(m_Contact.ContactId == string.Empty ? Common.INT_DBNULL.ToString() : m_Contact.ContactId);
                m_selectedRowIndex = rowIndex;
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
        private void SelectGridRow(int rowIndex)
        {
            if (rowIndex >= 0)
            {
                txtVendorCode.Text = dgvVendorMaster.Rows[rowIndex].Cells["Code"].Value.ToString().Trim();

                var orgSelect = (from p in m_vendorList where p.VendorCode.Trim() == txtVendorCode.Text.Trim() select p);

                if (orgSelect.ToList().Count == 0)
                    return;

                errVendor.Clear();

                m_objVendor = orgSelect.ToList()[0];
                txtVendorName.Text = m_objVendor.VendorName.Trim().ToString();
                txtAddressLine1.Text = m_objVendor.Address1.Trim().ToString();
                txtAddressLine2.Text = m_objVendor.Address2.Trim().ToString();
                txtAddressLine3.Text = m_objVendor.Address3.Trim().ToString();

                cmbWarehouse.SelectedValue = Convert.ToInt32(m_objVendor.Warehouse.Length == 0 ? Common.INT_DBNULL.ToString() : m_objVendor.Warehouse);
                cmbCountry.SelectedValue = Convert.ToInt32(m_objVendor.CountryId);
                cmbState.SelectedValue = Convert.ToInt32(m_objVendor.StateId);
                cmbCity.SelectedValue = Convert.ToInt32(m_objVendor.CityId);
                cmbStatus.SelectedValue = Convert.ToInt32(m_objVendor.Status);
                cmbPaymentTerms.SelectedValue = Convert.ToInt32(m_objVendor.PaymentTerms);

                txtPinCode.Text = m_objVendor.PinCode.Trim();
                txtEmail.Text = m_objVendor.Email1.Trim();
                txtFax.Text = m_objVendor.Fax1.Trim();
                txtPhone.Text = m_objVendor.PhoneNumber1;

                txtTinNo.Text = m_objVendor.TinNo.ToString();
                txtVAT.Text = m_objVendor.VatNo.ToString();
                txtCstNo.Text = m_objVendor.CstNo.ToString();
                txtMobile.Text = m_objVendor.Mobile1.ToString();
                //cmbTaxJurisdiction.SelectedValue = m_objVendor.TaxJuridicationID;

                cmbTaxJurisdiction.SelectedValue = m_objVendor.TaxJurisdictionId;

                m_modifiedDate = m_objVendor.ModifiedDate.ToString().Length>0 ? Convert.ToDateTime(m_objVendor.ModifiedDate).ToString(Common.DATE_TIME_FORMAT): Common.DATETIME_NULL.ToString();

                m_vendorId = Convert.ToInt32(m_objVendor.VendorID);

                Vendor loc = new Vendor();
                loc.VendorID = m_vendorId;
                string errMessage = string.Empty;
                m_lstContact = loc.ContactSearch(ref errMessage);
                dgvVendorContact.DataSource = new List<Contact>();
                if ((m_lstContact != null) && (m_lstContact.Count > 0))
                {
                    dgvVendorContact.DataSource = m_lstContact;

                    var query = (from p in m_lstContact where p.Status == 1 select p.Email1);
                    m_lstContactString = query.ToList();
                }

                //btnSaveVendor.Enabled = m_isSaveAvailable;
                //btnSearch.Enabled = false;
            }
        }

        /// <summary>
        /// To search records
        /// </summary>
        private List<Vendor> Search(string vendorCode, string vendorName, string warehouse, int status, string address1, string address2, int country, int state, int city)
        {
            List<Vendor> m_vendorListSearch = new List<Vendor>();
            Vendor loc = new Vendor();

            loc.VendorCode = vendorCode;
            loc.VendorName = vendorName.Trim();
            loc.Warehouse = warehouse;

            loc.CountryId = Convert.ToInt32(country);
            loc.StateId = Convert.ToInt32(state);
            loc.CityId = Convert.ToInt32(city);
            loc.Address1 = address1;
            loc.Address2 = address2;

            loc.Status = status;
            m_vendorListSearch = loc.GetVendors();

            return m_vendorListSearch;
        }

        /// <summary>
        /// reset controls for search 
        /// </summary>
        private void ResetControl()
        {
            VisitControls visitControls = new VisitControls();
            visitControls.ResetAllControlsInPanel(errVendor, pnlSearchHeader);
            cmbWarehouse.Enabled = true;
            m_lstContact = null;

            btnSaveVendor.Enabled = m_isSaveAvailable;
            btnSearch.Enabled = m_isSearchAvailable;
            dgvVendorContact.DataSource = new List<Contact>();
            dgvVendorMaster.DataSource = new List<Vendor>();

            m_vendorId = Common.INT_DBNULL;
            m_objVendor = null;
            txtVendorCode.Focus();

            cmbStatus.SelectedValue = 1;

            ResetContactControl();
        }

        /// <summary>
        /// Vallidates PINCODE
        /// </summary>
        /// <param name="err"></param>
        /// <param name="txt"></param>
        /// <param name="lbl"></param>
        /// <param name="yesNo"></param>
        private void ValidatePinCode(ErrorProvider err, TextBox txt, Label lbl, Boolean yesNo)
        {
            bool isValidPinCode = Validators.IsValidPinCode(txt.Text);

            if (isValidPinCode == false && yesNo == false)
                err.SetError(txt, Common.GetMessage("VAL0009", lbl.Text.Trim().Substring(0, lbl.Text.Trim().Length - 2)));
            else //if (isValidPinCode == true)
                err.SetError(txt, string.Empty);
        }

        #endregion

        #region Events

        #region ValidateVendor
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
        /// Call fn. ValidateVendorCombo to Validate Tax Jurisdiction
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void cmbTaxJurisdiction_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        ValidateVendorCombo(cmbTaxJurisdiction, lblTaxJurisdiction, true);
        //    }
        //    catch (Exception ex)
        //    {
        //        Common.LogException(ex);
        //        MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        /// <summary>
        /// Call fn. ValidateVendorCombo to Validate City
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ValidateVendorCombo(cmbCity, lblCity, true);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Call fn. ValidatedVendorText to Validate Name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtVendorName_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                ValidatedVendorText(txtVendorName, lblVendorName, true);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Call fn. ValidatedVendorText to Validate AddressLine 1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtAddressLine1_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                ValidatedVendorText(txtAddressLine1, lblAddressLine1, true);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Call fn. ValidatedVendorText to Validate AddressLine 2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtAddressLine2_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                ValidatedVendorText(txtAddressLine2, lblAddressLine2, true);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtVendorCode_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                ValidatedVendorText(txtVendorCode, lblVendorCode, true);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }        

        /// <summary>
        /// Call fn. ValidatedVendorText to validate Pin Code
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPinCode_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                ValidatePinCode(errVendor, txtPinCode, lblPinCode, true);
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
                ValidateVendorCombo(cmbStatus, lblStatus, true);
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
                errVendor.Clear();
                m_vendorList = Search(txtVendorCode.Text.Trim(), txtVendorName.Text.Trim(), cmbWarehouse.SelectedValue.ToString(), Convert.ToInt32(cmbStatus.SelectedValue.ToString()), txtAddressLine1.Text.Trim(), txtAddressLine2.Text.Trim(), Convert.ToInt32(cmbCountry.SelectedValue.ToString()), Convert.ToInt32(cmbState.SelectedValue.ToString()), Convert.ToInt32(cmbCity.SelectedValue.ToString()));

                dgvVendorMaster.CurrentCellChanged -= new EventHandler(dgvVendorMaster_CurrentCellChanged);
                if ((m_vendorList != null) && (m_vendorList.Count > 0))
                    dgvVendorMaster.DataSource = m_vendorList;
                else
                {
                    dgvVendorMaster.DataSource = new List<Vendor>();
                    MessageBox.Show(Common.GetMessage("8002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                dgvVendorMaster.ClearSelection();
                dgvVendorMaster.CurrentCellChanged += new EventHandler(dgvVendorMaster_CurrentCellChanged);
                dgvVendorMaster.ClearSelection();
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
                btnAddDetails.Enabled = m_isSaveAvailable;
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Call fn. SaveVendor to save Vendor Record
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveVendor_Click(object sender, EventArgs e)
        {
            try
            {
                SaveVendor();
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
        private void dgvVendor_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                SelectGridRow(e.RowIndex);
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
        private void dgvVendorContact_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                //SelectContactGridRow(e.RowIndex, e.ColumnIndex);
                ReflectVendorContact(e.RowIndex, e.ColumnIndex);
                btnAddDetails.Enabled = false;
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Remove Vendor Contact
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvVendorContact_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if ((e.RowIndex >= 0) && (dgvVendorContact.Rows[e.RowIndex].Cells[e.ColumnIndex].GetType() == typeof(DataGridViewImageCell)) && (dgvVendorContact.Rows[e.RowIndex].Cells[e.ColumnIndex].OwningColumn.Name == CON_GRIDCOL_EDIT))
                {
                    ReflectVendorContact(e.RowIndex, e.ColumnIndex);
                    btnAddDetails.Enabled = m_isSaveAvailable;
                }
                RemoveVendorContact(e);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                if (tabControlTransaction.SelectedIndex == 1 && (txtVendorCode.Text.Trim().Length == 0 ||cmbWarehouse.SelectedIndex<=0))
                {
                    DialogResult result = MessageBox.Show(Common.GetMessage("VAL0003", lblVendorCode.Text.Substring(0, lblVendorCode.Text.Trim().Length - 2), lblWarehouse.Text.Substring(0, lblWarehouse.Text.Trim().Length - 2)), Common.GetMessage("10001"));
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

        private void dgvVendorMaster_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvVendorMaster.SelectedCells.Count > 0)
                {
                    int rowIndex = dgvVendorMaster.SelectedCells[0].RowIndex;
                    int columnIndex = dgvVendorMaster.SelectedCells[0].ColumnIndex;
                    SelectGridRow(rowIndex);
                    btnSearch.Enabled = m_isSearchAvailable;
                    btnSaveVendor.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvVendorContact_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvVendorContact.SelectedCells.Count > 0)
                {
                    int rowIndex = dgvVendorContact.SelectedCells[0].RowIndex;
                    int columnIndex = dgvVendorContact.SelectedCells[0].ColumnIndex;
                    //SelectContactGridRow(rowIndex, columnIndex);
                    ReflectVendorContact(rowIndex, columnIndex);
                    btnAddDetails.Enabled = false;
                }
                else if (dgvVendorContact.SelectedCells.Count <= 0)
                {
                    dgvVendorContact.ClearSelection();
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbWarehouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ValidateVendorCombo(cmbWarehouse, lblWarehouse, true);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }     

        private void cmbPaymentTerms_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ValidateVendorCombo(cmbPaymentTerms, lblPaymentTerms, true);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        private void dgvVendorContact_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                DataGridView ctrl = (DataGridView)sender;
                if (ctrl.SelectedRows.Count > 0)
                {
                    if (ctrl.SelectedRows[0].Index > Common.INT_DBNULL)
                    {
                        ReflectVendorContact(ctrl.SelectedRows[0].Index, 1);
                        btnAddDetails.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvVendorMaster_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.RowIndex >= 0) && (dgvVendorMaster.Rows[e.RowIndex].Cells[e.ColumnIndex].GetType() == typeof(DataGridViewImageCell)))
            {
                SelectGridRow(e.RowIndex);
                btnSearch.Enabled = false;
                btnSaveVendor.Enabled = m_isSaveAvailable;
            }
        }
        
    }
}
