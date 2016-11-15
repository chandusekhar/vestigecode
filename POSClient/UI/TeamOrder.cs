using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using POSClient.BusinessObjects;
using CoreComponent.Core.BusinessObjects;
using AuthenticationComponent.BusinessObjects;
using CoreComponent.BusinessObjects;
using CoreComponent.Core.UI;
using CoreComponent.UI;
using System.Threading;
namespace POSClient.UI
{
    public partial class TeamOrder : POSClient.UI.BaseChildForm
    {
        BackgroundWorker bw;
        private string GRIDVIEW_XML_PATH;
        private COLog m_COLog;
        private Address m_Address;
        List<COLog> m_SearchList;
        int m_userID;
        private int m_location;


        private Boolean IsCloseAvailable = false;
        private Boolean IsInvoiceAvailable = false;
        private Boolean IsSaveAvailable = false;
        private Boolean IsSearchAvailable = false;

        private string CON_MODULENAME;
        private string m_LocationCode;
        private string m_UserName;
        //Bikram:AED Change
        private enum CourierCheck
        { 
            AllInvoice = 1,
            PendingInvoice = 2
        }
        private decimal TotalCourierAmt = 0;
        private List<CO> lstCourierOrder;
        public TeamOrder()
        {
            InitializeComponent();
            if (Authenticate.LoggedInUser != null)
            {
                m_userID = Authenticate.LoggedInUser.UserId;
                m_UserName = AuthenticationComponent.BusinessObjects.Authenticate.LoggedInUser.UserName;
            }
            m_location = Common.CurrentLocationId;
            InitializeRights();
            InitializeControls();
        }

        private void InitializeRights()
        {
            m_LocationCode = Common.LocationCode;
            CON_MODULENAME = Common.MODULE_TEAMORDER;
            if (!string.IsNullOrEmpty(m_UserName) && !string.IsNullOrEmpty(CON_MODULENAME))
            {
                IsSearchAvailable = AuthenticationComponent.BusinessObjects.Authenticate.IsFunctionAccessible(m_UserName, m_LocationCode, CON_MODULENAME, Common.FUNCTIONCODE_SEARCH);
                IsSaveAvailable = AuthenticationComponent.BusinessObjects.Authenticate.IsFunctionAccessible(m_UserName, m_LocationCode, CON_MODULENAME, Common.FUNCTIONCODE_SAVE);
                IsInvoiceAvailable = AuthenticationComponent.BusinessObjects.Authenticate.IsFunctionAccessible(m_UserName, m_LocationCode, CON_MODULENAME, Common.FUNCTIONCODE_INVOICE);
                IsCloseAvailable = AuthenticationComponent.BusinessObjects.Authenticate.IsFunctionAccessible(m_UserName, m_LocationCode, CON_MODULENAME, Common.FUNCTIONCODE_CLOSE);
            }
        }

        #region Methods

        private void InitializeControls()
        {
            try
            {
                FillLogType();
                FillStatus();
                FillPickUpCenter();
                FillCountry();
                FillState();
                FillCity();

                //Add On Change Handlers
                cmbLogType.SelectedIndexChanged += new EventHandler(cmbLogType_SelectedIndexChanged);
                cmbPickUpCenter.SelectedIndexChanged += new EventHandler(cmbPickUpCenter_SelectedIndexChanged);
                cmbContactCountry.SelectedIndexChanged += new EventHandler(cmbContactCountry_SelectedIndexChanged);
                cmbContactState.SelectedIndexChanged += new EventHandler(cmbContactState_SelectedIndexChanged);
                //Initialize Grid

                GRIDVIEW_XML_PATH = Environment.CurrentDirectory + "\\App_Data\\POSGridViewDefinition.xml";
                dgvOrderLog.AutoGenerateColumns = false;
                dgvOrderLog.AllowUserToAddRows = false;
                dgvOrderLog.AllowUserToDeleteRows = false;
                DataGridView dgvSearchNew = Common.GetDataGridViewColumns(dgvOrderLog, GRIDVIEW_XML_PATH);
                dgvOrderLog.SelectionChanged += new EventHandler(dgvOrderLog_SelectionChanged);
                btnInvoice.Enabled = IsInvoiceAvailable;
                btnSearch.Enabled = IsSearchAvailable;
                btnClose.Enabled = IsCloseAvailable;
                btnAdd.Enabled = IsSaveAvailable;
                btnClose.Enabled = false;
                btnInvoice.Enabled = false;
                txtDistributorId.Enabled = false;
                cmbPickUpCenter.Enabled = false;
                btnInvoice.Enabled = false;
                btnPrint.Enabled = false;


                dgvPendingOrders.AutoGenerateColumns = false;
                dgvPendingOrders.AllowUserToAddRows = false;
                dgvPendingOrders.AllowUserToDeleteRows = false;
                Common.GetDataGridViewColumns(dgvPendingOrders, GRIDVIEW_XML_PATH);

                dgvInvoiced.AutoGenerateColumns = false;
                dgvInvoiced.AllowUserToAddRows = false;
                dgvInvoiced.AllowUserToDeleteRows = false;
                Common.GetDataGridViewColumns(dgvInvoiced, GRIDVIEW_XML_PATH);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ResetForm()
        {
            SetFormData();
            if (m_COLog != null && m_COLog.Status != (int)Common.LogStatus.Closed && m_COLog.CreatedBy == AuthenticationComponent.BusinessObjects.Authenticate.LoggedInUser.UserId)
                btnClose.Enabled = IsCloseAvailable;
            else
                btnClose.Enabled = false;
        }

        private void SetFormData()
        {
            try
            {
                if (m_COLog != null)
                {
                    txtLogNo.Text = m_COLog.LogNo;
                    cmbLogType.SelectedValue = m_COLog.LogType;
                    cmbStatus.SelectedValue = m_COLog.Status;
                    cmbPickUpCenter.SelectedValue = m_COLog.PCId;
                    txtDistributorId.Text = m_COLog.DistributorId.ToString();
                    DataTable dt = Common.ParameterLookup(Common.ParameterType.LogPayments, new ParameterFilter(m_COLog.LogValue, -1, -1, -1));
                    if (dt != null && dt.Rows.Count > 0)
                        dgvLogPayments.DataSource = null;
                    //AED Changes: Calculate Courier changes
                    //dt = CalulateCourier(dt);

                    dgvLogPayments.DataSource = dt;
                    m_Address = m_COLog.Address;
                    SetAddress();
                }
                else
                    ClearLog();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private DataTable CalulateCourier(DataTable dt)
        {
            try
            {
                if (dt.Columns.Contains("COURIER"))
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        dr["CHANGEAMOUNT"] = (Convert.ToDecimal(dr["CHANGEAMOUNT"].ToString()) >= Convert.ToDecimal(dr["COURIER"].ToString()) ? Convert.ToDecimal(dr["CHANGEAMOUNT"].ToString()) : Convert.ToDecimal(dr["COURIER"].ToString())) - Convert.ToDecimal(dr["COURIER"].ToString());
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetAddress()
        {
            if (m_Address != null)
            {
                txtContactAddress1.Text = m_Address.Address1;
                txtContactAddress2.Text = m_Address.Address2;
                txtContactAddress3.Text = m_Address.Address3;
                txtContactAddress4.Text = m_Address.Address4;
                txtContactEmail1.Text = m_Address.Email1;
                txtContactEmail2.Text = m_Address.Email2;
                txtContactFax1.Text = m_Address.Fax1;
                txtContactFax2.Text = m_Address.Fax2;
                txtContactMobile1.Text = m_Address.Mobile1;
                txtContactMobile2.Text = m_Address.Mobile2;
                txtContactPhone1.Text = m_Address.PhoneNumber1;
                txtContactPhone2.Text = m_Address.PhoneNumber2;
                txtContactPinCode.Text = m_Address.PinCode;

                cmbContactCountry.SelectedValue = m_Address.CountryId;
                cmbContactState.SelectedValue = m_Address.StateId;
                cmbContactCity.SelectedValue = m_Address.CityId;
                txtContactWebsite.Text = m_Address.Website;
            }
        }

        #region Button Methods

        private void ClearLog()
        {
            try
            {
                errorAdd.Clear();
                txtLogNo.Text = string.Empty;
                txtDistributorId.Text = string.Empty;
                txtPUCDistID.Text = string.Empty;
                txtPUCDistName.Text = string.Empty;
                cmbLogType.SelectedIndex = 0;
                cmbStatus.SelectedIndex = 0;
                cmbPickUpCenter.SelectedIndex = 0;
                m_Address = new Address();
                SetAddress();
                btnAdd.Enabled = IsSaveAvailable;
                btnClose.Enabled = false;
                btnInvoice.Enabled = false;
                btnPrint.Enabled = false;
                dgvOrderLog.ClearSelection();
                txtPUCDistName.Text = string.Empty;
                txtPUCDistID.Text = string.Empty;
                dgvLogPayments.DataSource = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private bool Add(ref string errorMessage)
        {
            try
            {
                //validation();  
                int val = 0;
                if (m_COLog == null)
                    m_COLog = new COLog();
                m_COLog.LogNo = string.Empty;
                m_COLog.LogValue = string.Empty;
                m_COLog.Address.Address1 = txtContactAddress1.Text.Trim();
                m_COLog.Address.Address2 = txtContactAddress2.Text.Trim();
                m_COLog.Address.Address3 = txtContactAddress3.Text.Trim();
                m_COLog.Address.Address4 = txtContactAddress4.Text.Trim();
                m_COLog.Address.CityId = Convert.ToInt32(cmbContactCity.SelectedValue);
                m_COLog.Address.StateId = Convert.ToInt32(cmbContactState.SelectedValue);
                m_COLog.Address.CountryId = Convert.ToInt32(cmbContactCountry.SelectedValue);
                m_COLog.Address.Email1 = txtContactEmail1.Text.Trim();
                m_COLog.Address.Email2 = txtContactEmail2.Text.Trim();
                m_COLog.Address.Fax1 = txtContactFax1.Text.Trim();
                m_COLog.Address.Fax2 = txtContactFax2.Text.Trim();
                m_COLog.Address.Mobile1 = txtContactMobile1.Text.Trim();
                m_COLog.Address.Mobile2 = txtContactMobile2.Text.Trim();
                m_COLog.Address.PhoneNumber1 = txtContactPhone1.Text.Trim();
                m_COLog.Address.PhoneNumber2 = txtContactPhone2.Text.Trim();
                m_COLog.Address.PinCode = txtContactPinCode.Text.Trim();
                m_COLog.Address.Website = txtContactWebsite.Text.Trim();
                m_COLog.CreatedBy = m_userID;
                m_COLog.DistributorId = Int32.TryParse(txtDistributorId.Text, out val) ? val : 0;
                m_COLog.PCId = Convert.ToInt32(cmbPickUpCenter.SelectedValue) == -1 ? 0 : Convert.ToInt32(cmbPickUpCenter.SelectedValue);
                m_COLog.LogType = Convert.ToInt32(cmbLogType.SelectedValue);
                m_COLog.BOId = m_location;
                m_COLog.ModifiedBy = m_userID;
                m_COLog.Status = (int)Common.LogStatus.Open;
                return m_COLog.Save(ref errorMessage);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool Search()
        {
            try
            {
                int val = 0;
                dgvOrderLog.SelectionChanged -= dgvOrderLog_SelectionChanged;
                dgvOrderLog.DataSource = null;
                COLog log = new COLog();
                m_SearchList = log.Search(Convert.ToInt32(cmbStatus.SelectedValue), m_location, txtLogNo.Text.Trim(), Convert.ToInt32(cmbLogType.SelectedValue), Int32.TryParse(txtDistributorId.Text, out val) ? val : 0, Convert.ToInt32(cmbPickUpCenter.SelectedValue));
                if (m_SearchList != null)
                {
                    dgvOrderLog.DataSource = m_SearchList;
                    dgvOrderLog.ClearSelection();
                    btnInvoice.Enabled = false;
                    btnPrint.Enabled = false;
                }
                dgvOrderLog.SelectionChanged += new EventHandler(dgvOrderLog_SelectionChanged);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool CloseLog(ref string errorMessage)
        {
            try
            {
                if (m_COLog != null)
                {
                    m_COLog.ModifiedBy = m_userID;
                    m_COLog.Status = (int)Common.LogStatus.Closed;
                    return m_COLog.Save(ref errorMessage);
                }
                return false;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Cancel()
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


        #region Combo Methods

        private void FillLogType()
        {
            try
            {
                DataTable dtLogType = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter("COLOGTYPE", 0, 0, 0));
                if (dtLogType != null)
                {
                    cmbLogType.DataSource = dtLogType;
                    cmbLogType.DisplayMember = Common.KEYVALUE1;
                    cmbLogType.ValueMember = Common.KEYCODE1;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void FillStatus()
        {
            try
            {
                DataTable dtStatus = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter("COLOGSTATUS", 0, 0, 0));
                if (dtStatus != null)
                {
                    cmbStatus.DataSource = dtStatus;
                    cmbStatus.DisplayMember = Common.KEYVALUE1;
                    cmbStatus.ValueMember = Common.KEYCODE1;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void FillPickUpCenter()
        {
            try
            {
                DataTable dtPickUpCenter;
                dtPickUpCenter = Common.ParameterLookup(Common.ParameterType.POSLocations, new ParameterFilter(Common.LocationCode, (Int32)Common.LocationConfigId.PC, -1, -1));
                if ((dtPickUpCenter != null) && (dtPickUpCenter.Rows.Count > 0))
                {
                    dtPickUpCenter.Rows[0]["LocationId"] = "-1";
                    dtPickUpCenter.Rows[0]["LocationCode"] = "Select";
                    cmbPickUpCenter.DropDownStyle = ComboBoxStyle.DropDownList;
                    cmbPickUpCenter.DataSource = dtPickUpCenter;
                    cmbPickUpCenter.DisplayMember = "LocationCode";
                    cmbPickUpCenter.ValueMember = "LocationId";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /*
        private void FillDistributor()
        {
            try
            {
                DataTable dtDistributor = Common.ParameterLookup(Common.ParameterType.Distributor, new ParameterFilter(string.Empty, 0, 0, 0));
                if (dtDistributor != null)
                {
                    cmbDistributor.DataSource = dtDistributor;
                    cmbDistributor.DisplayMember = "DistributorName";
                    cmbDistributor.ValueMember = "DistributorId";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
         * 
         */

        private void FillCountry()
        {
            try
            {
                DataTable dtCountry = Common.ParameterLookup(Common.ParameterType.Country, new ParameterFilter(string.Empty, 0, 0, 0));
                if (dtCountry != null)
                {
                    cmbContactCountry.DataSource = dtCountry;
                    cmbContactCountry.DisplayMember = "CountryName";
                    cmbContactCountry.ValueMember = "CountryId";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void FillState()
        {
            try
            {
                cmbContactState.DataSource = null;
                DataTable dtState = new DataTable();
                if (cmbContactCountry.SelectedIndex > 0)
                {
                    dtState = Common.ParameterLookup(Common.ParameterType.State, new ParameterFilter(string.Empty, Convert.ToInt32(cmbContactCountry.SelectedValue), 0, 0));
                    cmbContactState.DataSource = dtState;
                    cmbContactState.DisplayMember = "StateName";
                    cmbContactState.ValueMember = "StateId";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void FillCity()
        {
            try
            {
                cmbContactCity.DataSource = null;
                DataTable dtCity = new DataTable();
                if (cmbContactState.SelectedIndex > 0)
                {
                    dtCity = Common.ParameterLookup(Common.ParameterType.City, new ParameterFilter(string.Empty, Convert.ToInt32(cmbContactState.SelectedValue), 0, 0));
                    cmbContactCity.DataSource = dtCity;
                    cmbContactCity.DisplayMember = "CityName";
                    cmbContactCity.ValueMember = "CityId";
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ChangeLogType()
        {
            try
            {
                if (Convert.ToInt32(cmbLogType.SelectedValue) == (Int32)Common.COLogType.Log)
                {
                    cmbPickUpCenter.Enabled = true;
                    txtDistributorId.Enabled = false;
                }
                else if (Convert.ToInt32(cmbLogType.SelectedValue) == (Int32)Common.COLogType.TeamOrder)
                {
                    cmbPickUpCenter.Enabled = false;
                    txtDistributorId.Enabled = true;
                }
                else
                {
                    cmbPickUpCenter.Enabled = false;
                    txtDistributorId.Enabled = false;
                }
                cmbPickUpCenter.SelectedIndex = 0;
                txtDistributorId.Text = string.Empty;
                txtPUCDistID.Text = string.Empty;
                txtPUCDistName.Text = string.Empty;

                ChangeDistributor();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ChangePickUpCenter()
        {
            try
            {
                m_Address = new Address();
                if (cmbPickUpCenter.SelectedIndex > 0)
                {
                    m_Address = GetPickUpCenterAddress(Convert.ToInt32(cmbPickUpCenter.SelectedValue));
                    DataTable dt = Common.ParameterLookup(Common.ParameterType.PUCDetails, new ParameterFilter("", Convert.ToInt32(cmbPickUpCenter.SelectedValue), -1, -1));
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        txtPUCDistID.Text = dt.Rows[0]["DistributorId"].ToString();
                        txtPUCDistName.Text = dt.Rows[0]["DistributorName"].ToString();
                    }
                }
                else
                {
                    txtPUCDistName.Text = string.Empty;
                    txtPUCDistID.Text = string.Empty;
                }
                SetAddress();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ChangeDistributor()
        {
            try
            {
                m_Address = new Address();
                int val = 0;
                if (Int32.TryParse(txtDistributorId.Text, out val))
                    m_Address = GetDistributorAddress(Convert.ToInt32(txtDistributorId.Text));
                SetAddress();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private Address GetDistributorAddress(int DistributorID)
        {
            Address _address = new Address();
            DataTable dtDistributor = Common.ParameterLookup(Common.ParameterType.Distributor, new ParameterFilter(string.Empty, DistributorID, 0, 0));
            if (dtDistributor != null && dtDistributor.Rows.Count > 0)
            {
                _address = Address.CreateAddressObject(dtDistributor.Rows[0]);
            }
            return _address;
        }

        private Address GetPickUpCenterAddress(int LocationID)
        {
            Address _address = new Address();
            DataTable dt = Common.ParameterLookup(Common.ParameterType.Locations, new ParameterFilter(string.Empty, -1, LocationID, -1));
            if (dt != null && dt.Rows.Count > 0)
            {
                _address = Address.CreateAddressObject(dt.Rows[0]);
            }
            return _address;
        }

        #endregion

        #endregion

        #region Events

        #region Button

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Search();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateAdd();
                StringBuilder sbError = new StringBuilder();
                sbError = GenerateAddError();
                sbError = Common.ReturnErrorMessage(sbError);
                if (sbError.ToString().Trim().Equals(string.Empty))
                {
                    DialogResult saveResult = MessageBox.Show(Common.GetMessage("INF0027"), Common.GetMessage("10004"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (saveResult == DialogResult.Yes)
                    {
                        string errorMessage = string.Empty;
                        if (Add(ref errorMessage))
                        {
                            MessageBox.Show(Common.GetMessage("8003", "CO Log", m_COLog.LogNo));
                            m_COLog = null;
                            ResetForm();
                            Search();
                        }
                        else if (!errorMessage.Trim().Equals(string.Empty))
                        {
                            if (errorMessage.Trim().IndexOf("30001:") >= 0)
                            {
                                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Common.LogException(new Exception(errorMessage));
                            }
                            else
                            {
                                MessageBox.Show(Common.GetMessage(errorMessage.Trim()));
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show(sbError.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult saveResult = MessageBox.Show(Common.GetMessage("5010", "Close"), Common.GetMessage("10004"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (saveResult == DialogResult.Yes)
                {
                    string errorMessage = string.Empty;
                    if (CloseLog(ref errorMessage))
                    {
                        MessageBox.Show(Common.GetMessage("INF0055", m_COLog.LogTypeName, "Closed"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        m_COLog = null;
                        ClearLog();
                        Search();
                    }
                    else if (!errorMessage.Trim().Equals(string.Empty))
                    {
                        if (errorMessage.Trim().IndexOf("30001:") >= 0)
                        {
                            MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Common.LogException(new Exception(errorMessage));
                        }
                        else
                        {
                            MessageBox.Show(Common.GetMessage(errorMessage.Trim()));
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Cancel();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                ClearLog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        #endregion

        #region Combos

        private void cmbContactCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                FillState();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        private void cmbContactState_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                FillCity();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        private void cmbLogType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ChangeLogType();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        private void cmbPickUpCenter_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ChangePickUpCenter();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        private void dgvOrderLog_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvOrderLog.SelectedRows.Count > 0)
            {
                m_COLog = m_SearchList[dgvOrderLog.SelectedRows[0].Index];
                btnAdd.Enabled = false;
                ResetForm();
                btnInvoice.Enabled = IsInvoiceAvailable;
                btnPrint.Enabled = true;
            }
        }

        #endregion


        #endregion

        #region Validation

        private void ValidateAdd()
        {
            errorAdd.Clear();
            ValidateRequiredComboField(cmbLogType, lblType);

            if (Convert.ToInt32(cmbLogType.SelectedValue) == (int)Common.COLogType.Log)
                ValidateRequiredComboField(cmbPickUpCenter, lblPC);
            else if (Convert.ToInt32(cmbLogType.SelectedValue) == (int)Common.COLogType.TeamOrder)
                ValidateDistributor();
            ValidateRequiredTextField(txtContactAddress1, lblContactAddress1);
            ValidateRequiredTextField(txtContactAddress2, lblContactAddress2);
            ValidateRequiredComboField(cmbContactCountry, lblContactCountry);
            ValidateRequiredComboField(cmbContactState, lblContactState);
            if (Common.CountryID != "4")
            {
                ValidateRequiredComboField(cmbContactCity, lblContactCity);
            }
            ValidateEmail(txtContactEmail1, lblContactEmail1);
            ValidateEmail(txtContactEmail2, lblContactEmail2);
            ValidatePinCode(txtContactPinCode, lblContactPinCode);
        }

        private bool ValidateClose()
        {
            return true;
        }

        private bool ValidateDistributor()
        {
            int val = 0;
            DataTable dt = Common.ParameterLookup(Common.ParameterType.Distributor, new ParameterFilter("", Int32.TryParse(txtDistributorId.Text, out val) ? val : 0, 0, 0));
            if (dt.Rows.Count == 1)
            {
                errorAdd.SetError(txtDistributorId, string.Empty);
                return true;
            }
            else
            {
                errorAdd.SetError(txtDistributorId, Common.GetMessage("VAL0109"));
                return false;
            }
        }

        private StringBuilder GenerateAddError()
        {
            StringBuilder sbError = new StringBuilder();
            bool isFocus = false;

            if (errorAdd.GetError(cmbLogType).Trim().Length > 0)
            {
                sbError.Append(errorAdd.GetError(cmbLogType));
                sbError.AppendLine();
                if (!isFocus)
                {
                    cmbLogType.Focus();
                    isFocus = true;
                }
            }
            if (errorAdd.GetError(cmbStatus).Trim().Length > 0)
            {
                sbError.Append(errorAdd.GetError(cmbStatus));
                sbError.AppendLine();
                if (!isFocus)
                {
                    cmbStatus.Focus();
                    isFocus = true;
                }
            }
            if (errorAdd.GetError(txtDistributorId).Trim().Length > 0)
            {
                sbError.Append(errorAdd.GetError(txtDistributorId));
                sbError.AppendLine();
                if (!isFocus)
                {
                    txtDistributorId.Focus();
                    isFocus = true;
                }
            }
            if (errorAdd.GetError(cmbPickUpCenter).Trim().Length > 0)
            {
                sbError.Append(errorAdd.GetError(cmbPickUpCenter));
                sbError.AppendLine();
                if (!isFocus)
                {
                    cmbPickUpCenter.Focus();
                    isFocus = true;
                }
            }
            if (errorAdd.GetError(txtContactAddress1).Trim().Length > 0)
            {
                sbError.Append(errorAdd.GetError(txtContactAddress1));
                sbError.AppendLine();
                if (!isFocus)
                {
                    txtContactAddress1.Focus();
                    isFocus = true;
                }
            }
            if (errorAdd.GetError(txtContactAddress2).Trim().Length > 0)
            {
                sbError.Append(errorAdd.GetError(txtContactAddress2));
                sbError.AppendLine();
                if (!isFocus)
                {
                    txtContactAddress2.Focus();
                    isFocus = true;
                }
            }
            if (errorAdd.GetError(cmbContactCountry).Trim().Length > 0)
            {
                sbError.Append(errorAdd.GetError(cmbContactCountry));
                sbError.AppendLine();
                if (!isFocus)
                {
                    cmbContactCountry.Focus();
                    isFocus = true;
                }
            }
            if (errorAdd.GetError(cmbContactState).Trim().Length > 0)
            {
                sbError.Append(errorAdd.GetError(cmbContactState));
                sbError.AppendLine();
                if (!isFocus)
                {
                    cmbContactState.Focus();
                    isFocus = true;
                }
            }
            if (errorAdd.GetError(cmbContactCity).Trim().Length > 0)
            {
                sbError.Append(errorAdd.GetError(cmbContactCity));
                sbError.AppendLine();
                if (!isFocus)
                {
                    cmbContactCity.Focus();
                    isFocus = true;
                }
            }
            if (errorAdd.GetError(txtContactEmail1).Trim().Length > 0)
            {
                sbError.Append(errorAdd.GetError(txtContactEmail1));
                sbError.AppendLine();
                if (!isFocus)
                {
                    txtContactEmail1.Focus();
                    isFocus = true;
                }
            }
            if (errorAdd.GetError(txtContactEmail2).Trim().Length > 0)
            {
                sbError.Append(errorAdd.GetError(txtContactEmail2));
                sbError.AppendLine();
                if (!isFocus)
                {
                    txtContactEmail2.Focus();
                    isFocus = true;
                }
            }
            if (errorAdd.GetError(txtContactPinCode).Trim().Length > 0)
            {
                sbError.Append(errorAdd.GetError(txtContactPinCode));
                sbError.AppendLine();
                if (!isFocus)
                {
                    txtContactPinCode.Focus();
                    isFocus = true;
                }
            }
            return sbError;
        }

        private bool ValidateRequiredTextField(TextBox txt, Label lbl)
        {
            try
            {
                bool isvalid = true;
                errorAdd.SetError(txt, string.Empty);
                bool isTextBoxEmpty = CoreComponent.Core.BusinessObjects.Validators.CheckForEmptyString(txt.Text.Trim().Length);
                if (isTextBoxEmpty == true)
                {
                    isvalid = false;
                    errorAdd.SetError(txt, Common.GetMessage("INF0019", lbl.Text.Trim().Substring(0, lbl.Text.Trim().Length - 2)));
                }
                return isvalid;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool ValidateRequiredComboField(ComboBox cmb, Label lbl)
        {
            try
            {
                bool isvalid = true;
                errorAdd.SetError(cmb, string.Empty);
                if (cmb.SelectedIndex <= 0)
                {
                    isvalid = false;
                    errorAdd.SetError(cmb, Common.GetMessage("VAL0002", lbl.Text.Trim().Substring(0, lbl.Text.Trim().Length - 2)));
                }
                return isvalid;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool ValidateEmail(TextBox txt, Label lbl)
        {
            try
            {
                errorAdd.SetError(txt, string.Empty);
                bool isEmailValid = Validators.IsValidEmailID(txt.Text.Trim(), true);
                if (!isEmailValid)
                {
                    errorAdd.SetError(txt, Common.GetMessage("VAL0009", lbl.Text.Trim().Substring(0, lbl.Text.Trim().Length - 1)));
                }
                return isEmailValid;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ValidatePinCode(TextBox txt, Label lbl)
        {
            if (txt.Text.Trim().Length > 0)
            {
                bool isValidPinCode = Validators.IsValidPinCode(txt.Text.Trim());
                if (!isValidPinCode)
                {
                    errorAdd.SetError(txt, Common.GetMessage("VAL0009", lbl.Text.Trim().Substring(0, lbl.Text.Trim().Length - 1)));
                }
            }
        }

        #endregion

        private void btnInvoice_Click(object sender, EventArgs e)
        {
            try
            {
                //Bikram:AED
                //Get Waiveoff limit
                decimal WaiveoffCourierLimit = CO.GetWaiveoffCourierLimit();
                //if Teamorder amount > waiveoff limit the do not 
               
                    if (m_COLog.LogOrderTotal < WaiveoffCourierLimit)
                    {
                        if (ValidateCourierCheck(CourierCheck.AllInvoice))
                        {
                            if (GetCourierAmount(lstCourierOrder) <= 0)
                            {
                                MessageBox.Show(Common.GetMessage("VAL0620"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }
                    }
                

                //Disable Invoice button 
                btnInvoice.Enabled = false;
                if (dgvOrderLog.SelectedRows.Count > 0)
                {
                    //string logNo = Convert.ToString(dgvOrderLog.SelectedRows[0].Cells["LogNo"].Value);
                    if (m_COLog != null)
                    {
                        string logNo = m_COLog.LogValue;
                        CI invoice = new CI();
                        string error = string.Empty;
                        if (!logNo.Equals(string.Empty) && m_location > 0 && m_userID > 0)
                        {
                            //Check for PUC Deposit
                            int logType = 0;
                            // find the Type of log. i.e. Log Order or Team Order 
                            DataTable dtLogType = Common.ParameterLookup(Common.ParameterType.LogType, new ParameterFilter(logNo, Common.INT_DBNULL, Common.INT_DBNULL, Common.INT_DBNULL));
                            if (dtLogType != null && dtLogType.Rows.Count == 1)
                                logType = Convert.ToInt32(dtLogType.Rows[0][0]);
                            if (logType == (int)Common.COLogType.Log)
                            {
                                string errorMessage = string.Empty;
                                CO order = new CO();
                                order.LogNo = logNo;
                                order.Status = (int)Common.OrderStatus.Confirmed;
                                errorMessage = string.Empty;
                                // Search All confirmed orders in this Log order
                                List<CO> lstCo = order.Search(ref errorMessage);
                                if (errorMessage == string.Empty && lstCo != null && lstCo.Count > 0)
                                {
                                    decimal totalPayment = 0;
                                    decimal totalChange = 0;
                                    for (int count = 0; count < lstCo.Count; count++)
                                    {
                                        totalPayment += lstCo[count].DBRoundedPaymentAmount;
                                        totalChange += lstCo[count].DBRoundedChangeAmount;
                                    }
                                    order = new CO();
                                    // check for available puc deposit for this PC.
                                    decimal pucAvailableAmt = 0;
                                    decimal requiredAmt = totalPayment - totalChange;
                                    requiredAmt = Math.Round(requiredAmt, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                                    string pucMsg = order.CheckPUCAmount(lstCo[0].PCId, lstCo[0].BOId, totalPayment, totalChange, ref pucAvailableAmt);
                                    if (pucMsg != string.Empty)
                                    {
                                        if (pucMsg.Contains("INF0145"))
                                        {
                                            MessageBox.Show(Common.GetMessage(pucMsg, pucAvailableAmt.ToString(), requiredAmt.ToString()), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        }
                                        else
                                        {
                                            MessageBox.Show(Common.GetMessage(pucMsg), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        }
                                        btnInvoice.Enabled = true;
                                        return;
                                    }
                                }
                            }

                            DataTable dtItems = new DataTable();
                            bool isProcessed = invoice.ProcessLog(logNo, m_location, m_userID, ref error, ref dtItems);
                            if (isProcessed && error.Trim().Equals(string.Empty))
                            {
                                MessageBox.Show(Common.GetMessage("8011"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                if (error.Contains(","))
                                {
                                    MessageBox.Show(Common.GetMessage(error.Split(",".ToCharArray()[0])), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                else if (error.Contains("40008"))
                                {
                                    if (dtItems != null && dtItems.Rows.Count > 0)
                                    {
                                        ShortItemsPopup objPopup = new ShortItemsPopup(dtItems);
                                        objPopup.ShowDialog();
                                    }
                                }
                                else
                                {
                                    MessageBox.Show(Common.GetMessage(error), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                        }
                    }
                }
               // Enabled invoice button once invoice done or process completes
                btnInvoice.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        private void txtDistributorId_Validated(object sender, EventArgs e)
        {
            try
            {
                SearchDistributor();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        private void txtDistributorId_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    //CoreComponent.Controls.frmSearch objfrmSearch = new CoreComponent.Controls.frmSearch(CoreComponent.Controls.SearchTypes.DistributorSearch);
                    //CoreComponent.BusinessObjects.Distributor _distributor = (CoreComponent.BusinessObjects.Distributor)objfrmSearch.ReturnObject;
                    //objfrmSearch.ShowDialog();
                    //_distributor = (CoreComponent.BusinessObjects.Distributor)objfrmSearch.ReturnObject;
                    //frmDistributorSearch objfrmdist = new frmDistributorSearch();
                    //objfrmdist.ShowDialog();
                    //CoreComponent.BusinessObjects.Distributor _distributor = new CoreComponent.BusinessObjects.Distributor();
                    //_distributor = objfrmdist.distributorReturnObject;
                    //if (_distributor != null)
                    //{
                    //    txtDistributorId.Text = _distributor.DistributorId.ToString();
                    //}

                    SearchDistributor();
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtLogNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtLogNo.Text.Trim().Length > 0)
                {
                    Search();
                }
            }
        }

        private void SearchDistributor()
        {
            if (txtDistributorId.Text.Trim() != string.Empty)
            {
                int outDisNumber = 0;
                if (int.TryParse(txtDistributorId.Text.Trim(), out outDisNumber))
                {
                    Distributor objDistributor = new Distributor();
                    objDistributor.SDistributorId = txtDistributorId.Text.Trim();
                    objDistributor.DistributorId = Convert.ToInt32(txtDistributorId.Text.Trim());
                    string errorMessage = string.Empty;
                    List<Distributor> dist = objDistributor.SearchDistributor(ref errorMessage);
                    if (dist == null)
                    {
                        txtDistributorId.Text = string.Empty;
                        txtPUCDistID.Text = string.Empty;
                        txtPUCDistName.Text = string.Empty;
                        txtContactAddress1.Text = string.Empty;
                        txtContactAddress2.Text = string.Empty;
                        txtContactAddress3.Text = string.Empty;
                        txtContactAddress4.Text = string.Empty;
                        txtContactEmail1.Text = string.Empty;
                        txtContactEmail2.Text = string.Empty;
                        txtContactFax1.Text = string.Empty;
                        txtContactFax2.Text = string.Empty;
                        txtContactMobile1.Text = string.Empty;
                        txtContactMobile2.Text = string.Empty;
                        txtContactPhone1.Text = string.Empty;
                        txtContactPhone2.Text = string.Empty;
                        txtContactPinCode.Text = string.Empty;
                        txtContactWebsite.Text = string.Empty;

                        MessageBox.Show(Common.GetMessage("40018"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (dist.Count == 0)
                    {
                        txtDistributorId.Text = string.Empty;
                        txtPUCDistID.Text = string.Empty;
                        txtPUCDistName.Text = string.Empty;
                        txtContactAddress1.Text = string.Empty;
                        txtContactAddress2.Text = string.Empty;
                        txtContactAddress3.Text = string.Empty;
                        txtContactAddress4.Text = string.Empty;
                        txtContactEmail1.Text = string.Empty;
                        txtContactEmail2.Text = string.Empty;
                        txtContactFax1.Text = string.Empty;
                        txtContactFax2.Text = string.Empty;
                        txtContactMobile1.Text = string.Empty;
                        txtContactMobile2.Text = string.Empty;
                        txtContactPhone1.Text = string.Empty;
                        txtContactPhone2.Text = string.Empty;
                        txtContactPinCode.Text = string.Empty;
                        txtContactWebsite.Text = string.Empty;
                        MessageBox.Show(Common.GetMessage("40018"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (dist.Count > 1)
                    {
                        using (DistributorPopup dp = new DistributorPopup(dist))
                        {
                            Point pointTree = new Point();
                            pointTree = panel2.PointToScreen(new Point(167, 46));
                            pointTree.Y = pointTree.Y + 25;
                            pointTree.X = pointTree.X + 5;
                            dp.Location = pointTree;
                            if (dp.ShowDialog() == DialogResult.OK)
                            {
                                txtDistributorId.Text = dp.SelectedDistributor.DistributorId.ToString().Trim();
                                txtPUCDistID.Text = dp.SelectedDistributor.DistributorId.ToString().Trim();
                                txtPUCDistName.Text = dp.SelectedDistributor.DistributorFullName.ToString().Trim();
                                //txtContactAddress1.Text = dp.SelectedDistributor.DistributorAddress1.ToString().Trim();
                                //txtContactAddress2.Text = dp.SelectedDistributor.DistributorAddress2.ToString().Trim();
                                //txtContactAddress3.Text = dp.SelectedDistributor.DistributorAddress3.ToString().Trim();
                                //txtContactAddress4.Text = dp.SelectedDistributor.DistributorAddress4.ToString().Trim();
                                //txtContactEmail1.Text = dp.SelectedDistributor.DistributorEMailID.ToString().Trim();
                                //txtContactEmail2.Text = string.Empty;
                                //txtContactFax1.Text = dp.SelectedDistributor.DistributorFaxNumber.ToString().Trim();
                                //txtContactFax2.Text = string.Empty;
                                //txtContactMobile1.Text = dp.SelectedDistributor.DistributorMobNumber.ToString().Trim();
                                //txtContactMobile2.Text = string.Empty;
                                //txtContactPhone1.Text = dp.SelectedDistributor.DistributorTeleNumber.ToString().Trim();
                                //txtContactPhone2.Text = string.Empty;
                                //txtContactPinCode.Text = dp.SelectedDistributor.DistributorPinCode.ToString().Trim();
                                //txtContactWebsite.Text = string.Empty;
                                //cmbContactCountry.SelectedValue = dp.SelectedDistributor.DistributorCountryCode;
                                //cmbContactState.SelectedValue = dp.SelectedDistributor.DistributorStateCode;
                                //cmbContactCity.SelectedValue = dp.SelectedDistributor.DistributorCityCode;
                                ChangeDistributor();
                            }
                            else
                            {
                                txtDistributorId.Text = string.Empty;
                                txtPUCDistID.Text = string.Empty;
                                txtPUCDistName.Text = string.Empty;
                                txtContactAddress1.Text = string.Empty;
                                txtContactAddress2.Text = string.Empty;
                                txtContactAddress3.Text = string.Empty;
                                txtContactAddress4.Text = string.Empty;
                                txtContactEmail1.Text = string.Empty;
                                txtContactEmail2.Text = string.Empty;
                                txtContactFax1.Text = string.Empty;
                                txtContactFax2.Text = string.Empty;
                                txtContactMobile1.Text = string.Empty;
                                txtContactMobile2.Text = string.Empty;
                                txtContactPhone1.Text = string.Empty;
                                txtContactPhone2.Text = string.Empty;
                                txtContactPinCode.Text = string.Empty;
                                txtContactWebsite.Text = string.Empty;
                            }
                        }
                    }
                    else
                    {
                        txtDistributorId.Text = dist[0].DistributorId.ToString().Trim();
                        txtPUCDistID.Text = dist[0].DistributorId.ToString().Trim();
                        txtPUCDistName.Text = dist[0].DistributorFullName.ToString().Trim();
                        //txtContactAddress1.Text = dist[0].DistributorAddress1.ToString().Trim();
                        //txtContactAddress2.Text = dist[0].DistributorAddress2.ToString().Trim();
                        //txtContactAddress3.Text = dist[0].DistributorAddress3.ToString().Trim();
                        //txtContactAddress4.Text = dist[0].DistributorAddress4.ToString().Trim();
                        //txtContactEmail1.Text = dist[0].DistributorEMailID.ToString().Trim();
                        //txtContactEmail2.Text = string.Empty;
                        //txtContactFax1.Text = dist[0].DistributorFaxNumber.ToString().Trim();
                        //txtContactFax2.Text = string.Empty;
                        //txtContactMobile1.Text = dist[0].DistributorMobNumber.ToString().Trim();
                        //txtContactMobile2.Text = string.Empty;
                        //txtContactPhone1.Text = dist[0].DistributorTeleNumber.ToString().Trim();
                        //txtContactPhone2.Text = string.Empty;
                        //txtContactPinCode.Text = dist[0].DistributorPinCode.ToString().Trim();
                        //txtContactWebsite.Text = string.Empty;
                        //cmbContactCountry.SelectedValue = dist[0].DistributorCountryCode;
                        //cmbContactState.SelectedValue = dist[0].DistributorStateCode;
                        //cmbContactCity.SelectedValue = dist[0].DistributorCityCode;
                        ChangeDistributor();
                    }
                }
                else
                {
                    txtDistributorId.Text = string.Empty;
                    txtPUCDistID.Text = string.Empty;
                    txtPUCDistName.Text = string.Empty;
                    txtContactAddress1.Text = string.Empty;
                    txtContactAddress2.Text = string.Empty;
                    txtContactAddress3.Text = string.Empty;
                    txtContactAddress4.Text = string.Empty;
                    txtContactEmail1.Text = string.Empty;
                    txtContactEmail2.Text = string.Empty;
                    txtContactFax1.Text = string.Empty;
                    txtContactFax2.Text = string.Empty;
                    txtContactMobile1.Text = string.Empty;
                    txtContactMobile2.Text = string.Empty;
                    txtContactPhone1.Text = string.Empty;
                    txtContactPhone2.Text = string.Empty;
                    txtContactPinCode.Text = string.Empty;
                    txtContactWebsite.Text = string.Empty;
                }
            }
            else
            {
                txtDistributorId.Text = string.Empty;
                txtPUCDistID.Text = string.Empty;
                txtPUCDistName.Text = string.Empty;
                txtContactAddress1.Text = string.Empty;
                txtContactAddress2.Text = string.Empty;
                txtContactAddress3.Text = string.Empty;
                txtContactAddress4.Text = string.Empty;
                txtContactEmail1.Text = string.Empty;
                txtContactEmail2.Text = string.Empty;
                txtContactFax1.Text = string.Empty;
                txtContactFax2.Text = string.Empty;
                txtContactMobile1.Text = string.Empty;
                txtContactMobile2.Text = string.Empty;
                txtContactPhone1.Text = string.Empty;
                txtContactPhone2.Text = string.Empty;
                txtContactPinCode.Text = string.Empty;
                txtContactWebsite.Text = string.Empty;
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_COLog.Status.Equals(1))
                {
                    MessageBox.Show(Common.GetMessage("40039"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    PrintInvoice();
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PrintInvoice()
        {
            string logNo = m_COLog.LogValue;
            string errorMessage = string.Empty;
            CO order = new CO();
            order.LogNo = logNo;
            order.Status = (int)Common.OrderStatus.Invoiced;
            errorMessage = string.Empty;
            // Search All Invoiced orders in this Log order
            List<CO> lstCo = order.Search(ref errorMessage);
            if (errorMessage == string.Empty && lstCo != null && lstCo.Count > 0)
            {
                foreach (CO _order in lstCo)
                {
                    DataSet dsReport = CreatePrintDataSet((int)Common.PrintType.PrintInvoice, _order.CustomerOrderNo);
                    CoreComponent.UI.ReportScreen reportScreenObj = new CoreComponent.UI.ReportScreen((int)Common.ReportType.CustomerInvoice, dsReport);
                    reportScreenObj.PrintReport();
                    dsReport = null;
                }
                MessageBox.Show(Common.GetMessage("INF0223", lstCo.Count.ToString()), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(Common.GetMessage("INF0224"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private DataSet CreatePrintDataSet(int type, string OrderNo)
        {

            string errorMessage = string.Empty;
            DataSet ds = CO.GetOrderForPrint(type, OrderNo, ref errorMessage);
            if (errorMessage.Trim().Length == 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ds.Tables[0].Columns.Add(new DataColumn("Header", Type.GetType("System.String")));
                    ds.Tables[0].Columns.Add(new DataColumn("DateText", Type.GetType("System.String")));
                    ds.Tables[0].Columns.Add(new DataColumn("TINNo", Type.GetType("System.String")));
                    ds.Tables[0].Columns.Add(new DataColumn("OrderAmountWords", Type.GetType("System.String")));
                    ds.Tables[1].Columns.Add(new DataColumn("PriceInclTax", Type.GetType("System.String")));
                    ds.Tables[0].Columns.Add(new DataColumn("PANNo", Type.GetType("System.String")));
                    ds.Tables[1].Columns.Add(new DataColumn("IsLocation", Type.GetType("System.String")));
                    ds.Tables[0].Rows[0]["Header"] = (type == 1 ? "Customer Order" : "Retail Invoice");
                    ds.Tables[0].Rows[0]["TINNo"] = Common.TINNO;
                    ds.Tables[0].Rows[0]["PANNo"] = Common.PANNO;

                    ds.Tables[0].Columns.Add(new DataColumn("MiniBranch", Type.GetType("System.String")));
                    ds.Tables[0].Rows[0]["MiniBranch"] = (Common.IsMiniBranchLocation == 1 ? "N" : "Y");
                    //if (type == 2)
                    //{
                    //    if (ds.Tables[0].Rows[0]["Password"].ToString() != string.Empty)
                    //    {
                    //        User objUser = new User();
                    //        ds.Tables[0].Rows[0]["Password"] = objUser.DecryptPassword(ds.Tables[0].Rows[0]["Password"].ToString());
                    //    }
                    //    else
                    //    {
                    //        ds.Tables[0].Rows[0]["Password"] = string.Empty;

                    //    }
                    //}

                }
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    ds.Tables[0].Rows[i]["OrderAmount"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["OrderAmount"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                    ds.Tables[0].Rows[i]["TaxAmount"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["TaxAmount"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                    ds.Tables[0].Rows[i]["OrderAmountWords"] = Common.AmountToWords.AmtInWord(Convert.ToDecimal(ds.Tables[0].Rows[i]["OrderAmount"]) + Convert.ToDecimal(ds.Tables[0].Rows[i]["TaxAmount"]));
                    ds.Tables[0].Rows[i]["PaymentAmount"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["PaymentAmount"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                    ds.Tables[0].Rows[i]["ChangeAmount"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["ChangeAmount"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                    ds.Tables[0].Rows[i]["TotalBV"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["TotalBV"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                    ds.Tables[0].Rows[i]["TotalPV"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["TotalPV"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                    ds.Tables[0].Rows[i]["TotalWeight"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["TotalWeight"]), 3);
                    ds.Tables[0].Rows[i]["DateText"] = (Convert.ToDateTime(ds.Tables[0].Rows[i]["Date"])).ToString(Common.DTP_DATE_FORMAT);
                }
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    ds.Tables[1].Rows[i]["DP"] = Math.Round(Convert.ToDecimal(ds.Tables[1].Rows[i]["DP"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                    ds.Tables[1].Rows[i]["UnitPrice"] = Math.Round(Convert.ToDecimal(ds.Tables[1].Rows[i]["UnitPrice"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                    ds.Tables[1].Rows[i]["Qty"] = Math.Round(Convert.ToDecimal(ds.Tables[1].Rows[i]["Qty"]), Common.DisplayQtyRounding, MidpointRounding.AwayFromZero);
                    ds.Tables[1].Rows[i]["Amount"] = Math.Round(Convert.ToDecimal(ds.Tables[1].Rows[i]["Amount"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                    ds.Tables[1].Rows[i]["PriceInclTax"] = Math.Round(Convert.ToDecimal(ds.Tables[1].Rows[i]["UnitPrice"]) + (Convert.ToDecimal(ds.Tables[1].Rows[i]["TaxAmount"]) / Convert.ToDecimal(ds.Tables[1].Rows[i]["Qty"])), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                    ds.Tables[1].Rows[i]["TaxAmount"] = Math.Round(Convert.ToDecimal(ds.Tables[1].Rows[i]["TaxAmount"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                    if (ds.Tables[1].Rows[i]["TaxPercent"].ToString().Length > 0)
                        ds.Tables[1].Rows[i]["TaxPercent"] = Math.Round(Convert.ToDecimal(ds.Tables[1].Rows[i]["TaxPercent"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                    ds.Tables[1].Rows[i]["Discount"] = Math.Round(Convert.ToDecimal(ds.Tables[1].Rows[i]["Discount"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                }
                for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                {
                    ds.Tables[2].Rows[i]["PaymentAmount"] = Math.Round(Convert.ToDecimal(ds.Tables[2].Rows[i]["PaymentAmount"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                }

                for (int i = 0; i < ds.Tables[3].Rows.Count; i++)
                {
                    ds.Tables[3].Rows[i]["TaxPercent"] = Math.Round(Convert.ToDecimal(ds.Tables[3].Rows[i]["TaxPercent"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                    ds.Tables[3].Rows[i]["TaxAmount"] = Math.Round(Convert.ToDecimal(ds.Tables[3].Rows[i]["TaxAmount"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                }
            }
            return ds;
        }

        private void grdvInvoiced_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvOrderLog_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (string.Compare(dgvOrderLog["Status", e.RowIndex].Value.ToString(), "Open", true) == 0)
            {
                m_COLog = m_SearchList[e.RowIndex];
                pnlIvoice.BringToFront();
                pnlIvoice.Visible = true;
                GetOrderList(true);
                GetOrderList(false);
            }
        }

        private void GetOrderList(bool bIsInvoiced)
        {
            string errorMessage = "";
            List<CI> CIList = new List<CI>();
            CO order = new CO();
            order.LogNo = m_COLog.LogValue;
            if (!bIsInvoiced)
                order.Status = (int)Common.OrderStatus.Confirmed;
            else
                order.Status = (int)Common.OrderStatus.Invoiced;
            errorMessage = string.Empty;
            List<CO> lstCo = order.Search(ref errorMessage);
            if (!bIsInvoiced)
                dgvPendingOrders.DataSource = lstCo;
            else
                dgvInvoiced.DataSource = lstCo;
        }

        private void GetOrderList(bool bIsInvoiced, List<CO> lst)
        {
            string errorMessage = "";
            List<CI> CIList = new List<CI>();
            CO order = new CO();
            order.LogNo = m_COLog.LogValue;
            if (!bIsInvoiced)
                order.Status = (int)Common.OrderStatus.Confirmed;
            else
                order.Status = (int)Common.OrderStatus.Invoiced;
            errorMessage = string.Empty;
            List<CO> lstCo = order.Search(ref errorMessage);
            if (!bIsInvoiced)
                dgvPendingOrders.DataSource = lstCo;
            else
                dgvInvoiced.DataSource = lstCo;

            foreach (CO co1 in lstCo)
            {
                foreach (CO co2 in lst)
                {
                    if (co1.CustomerOrderNo == co2.CustomerOrderNo)
                    {
                        //if (dgvPendingOrders["CustOrderNo", lstCo.IndexOf(co1)].Value ==co1.CustomerOrderNo )
                        dgvPendingOrders["Invoice", lstCo.IndexOf(co1)].Value = true;
                    }
                }
            }

        }

        private void btnExist_Click(object sender, EventArgs e)
        {
            pnlIvoice.SendToBack();
            pnlIvoice.Visible = false;
        }

        private void btnInvoiceOrders_Click(object sender, EventArgs e)
        {           
            pgbStatus.Maximum = TotalSelectedRecords();
            //Bikram:AED
            //Get Waiveoff limit
            decimal WaiveoffCourierLimit = CO.GetWaiveoffCourierLimit();
            //if Teamorder amount > waiveoff limit the do not 

            if (m_COLog.LogOrderTotal < WaiveoffCourierLimit)
            {
                if (ValidateCourierCheck(CourierCheck.PendingInvoice))
                {
                    if (GetCourierAmount(lstCoProcess) <= 0)
                    {
                        MessageBox.Show(Common.GetMessage("VAL0620"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
            }

            btnInvoiceOrders.Enabled = false;
            pgbStatus.Visible = true;
            bw = new BackgroundWorker();
            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;
            bw.DoWork += bw_DoWork;
            bw.ProgressChanged += bw_ProgressChanged;
            bw.RunWorkerCompleted += bw_RunWorkerCompleted;
            bw.RunWorkerAsync("Hello to worker");
            lblOrderStatus.Text = "Process Start...";
            //Console.WriteLine("Press Enter in next 5 seconds to cancel");
            //Console.ReadLine();
            //if (bw.IsBusy) bw.CancelAsync();
            //Console.ReadLine();     
             

           
        }
        /// <summary>
        /// Bikram: Team Order Courier check 
        /// </summary>
        /// <param name="courierCheck"></param>
        /// <returns></returns>
        private bool ValidateCourierCheck(CourierCheck courierCheck)
        {
            CO currentOrder = new CO();
            if (CourierCheck.PendingInvoice.Equals(courierCheck))
            {
                for (int i = 0; i < lstCoProcess.Count; i++)
                {
                    currentOrder = lstCoProcess[i];
                    //Order mode 2 is Courier
                    if (currentOrder.OrderMode == 2)
                    {
                        return true;
                        break;
                    }
                }
            }
            else // all INVOICE
            {
                string errorMessage = string.Empty;
                currentOrder.LogNo = m_COLog.LogValue;
                currentOrder.Status = (int)Common.OrderStatus.Confirmed;
                errorMessage = string.Empty;
                // Search All confirmed orders in this Log order
                lstCourierOrder = currentOrder.Search(ref errorMessage);
                for (int i = 0; i < lstCourierOrder.Count; i++)
                {
                    currentOrder = lstCourierOrder[i];
                    //Order mode 2 is Courier
                    if (currentOrder.OrderMode == 2)
                    {
                        return true;
                        break;
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// Get total LOG Courier amount
        /// </summary>
        private decimal GetCourierAmount(List<CO> lstCoProc)
        {            
            CO currentOrder; 
            CO m_currentOrder;
            TotalCourierAmt = 0;
            for (int i = 0; i < lstCoProc.Count; i++)
            {
                decimal CourierAmt = 0;
                currentOrder = new CO();
                currentOrder = lstCoProc[i];
                m_currentOrder = new CO();
                m_currentOrder.GetCOAllDetails(currentOrder.CustomerOrderNo, currentOrder.Status);

                IEnumerable<COPayment> objPay = from n in m_currentOrder.COPaymentList
                                                where n.TenderType == 101
                                                select n;
                foreach (COPayment obj in objPay)
                {
                    CourierAmt = Math.Round(obj.PaymentAmount, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                }
                TotalCourierAmt = TotalCourierAmt + CourierAmt;
            }
            return TotalCourierAmt;
        }

         void bw_RunWorkerCompleted(object sender,RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
                lblOrderStatus.Text = "You cancelled!";
            else if (e.Error != null)
                lblOrderStatus.Text = "Worker exception: " + e.Error.ToString();
            else
            {
                if (e.Result != null)
                {
                    lblOrderStatus.Text = e.Result.ToString();
                    pgbStatus.Visible = false;
                    //GetOrderList(false,lstCoPass);
                    GetOrderList(true);
                    GetOrderList(false, lstCoPass);
                    //GetOrderList(true, lstCoPass);
                    dtItems.Clear();
                    dtItems3.Clear();
                    int iCounter = 0;
                    for (iCounter = 0; iCounter < dgvPendingOrders.RowCount; iCounter++)
                    {
                        if (dgvPendingOrders["Invoice", iCounter].Value != null &&
                            ((Boolean)dgvPendingOrders["Invoice", iCounter].Value))
                        {
                            DataRow[] dr1 = dtItems2.Select("CustomerOrderNo = '" + dgvPendingOrders["CustOrderNo", iCounter].Value + "'");
                            if (dr1 != null && dr1.Count() > 0)
                            {
                                dtItems3 = dr1.CopyToDataTable();
                                dtItems.Merge(dtItems3, true);
                            }
                        }
                    }

                    if (dtItems != null && dtItems.Rows.Count > 0)
                    {
                        ShortItemsPopup objPopup = new ShortItemsPopup(dtItems, true);
                        objPopup.ShowDialog();
                    }

                }
               
            }
                //Console.WriteLine("Complete - " + e.Result); // from DoWork
            //Enable Invoice button after process completes irrespective of conditions
             btnInvoiceOrders.Enabled = true;
        }
        void bw_ProgressChanged(object sender,ProgressChangedEventArgs e)
        {
            try
            {
                int percentage = e.ProgressPercentage;
                pgbStatus.Value = percentage;
                lblOrderStatus.Width = 1400;
                //lblOrderStatus.Left = 100;
                //lblOrderStatus.Text = string.Empty;
                //lblOrderStatus.Text = "Processing Order No:-" + lstCoProcess[percentage - 1].CustomerOrderNo.ToString();
                if (percentage == lstCoProcess.Count)
                {

                    //lblOrderStatus.Text = "Processing Order No:-" + lstCoProcess[percentage-1].CustomerOrderNo.ToString();    
                    GetOrderList(true);
                    GetOrderList(false);
                    //GetOrderList(true, lstCoPass);
                    GetOrderList(false, lstCoPass);
                    pgbStatus.Visible = false;
                    lblOrderStatus.Text = Common.GetMessage("8011");
                }
                else
                {
                    lblOrderStatus.Text = "Processing Order No:-" + lstCoProcess[percentage - 1].CustomerOrderNo.ToString();
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
            }
            finally
            {
            }
            //Console.WriteLine("Reached " + e.ProgressPercentage + "%");
        }
        List<CO> lstCoProcess = new List<CO>();
        int TotalSelectedRecords()
        {
            int iCheckedCount = 0;
            try
            {
                lstCoProcess = new List<CO>();
                int iCounter;
                

                List<CO> objlstData;
                CI invoice = new CI();
                string error = string.Empty;

                objlstData = dgvPendingOrders.DataSource as List<CO>;
                for (iCounter = 0; iCounter < dgvPendingOrders.RowCount; iCounter++)
                {
                    if (dgvPendingOrders["Invoice", iCounter].Value != null && ((Boolean)dgvPendingOrders["Invoice", iCounter].Value))
                    {
                        iCheckedCount++;
                        lstCoProcess.Add(objlstData[iCounter]);
                    }
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
            }
            finally
            {
            }
            return iCheckedCount;
        }

        List<CO> lstCoPass = new List<CO>();
        DataTable dtItems2 = new DataTable();

        DataTable dtItems = new DataTable();
        DataTable dtItems3 = new DataTable();

        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            lstCoPass = new List<CO>();
            int iCounter;
            int iCheckedCount = 0;
            List<CO> lstCo = new List<CO>();
            List<CO> objlstData;
            CO order = null;
            CI invoice = new CI();
            string error = string.Empty;
            decimal totalPayment = 0;
            decimal totalChange = 0;
            objlstData = dgvPendingOrders.DataSource as List<CO>;
            for (iCounter = 0; iCounter < dgvPendingOrders.RowCount; iCounter++)
            {
                if (dgvPendingOrders["Invoice", iCounter].Value != null &&
                    ((Boolean)dgvPendingOrders["Invoice", iCounter].Value))
                {
                    iCheckedCount++;
                    lstCo.Add(objlstData[iCounter]);
                    totalPayment += objlstData[iCounter].DBRoundedPaymentAmount;
                    totalChange += objlstData[iCounter].DBRoundedChangeAmount;
                }
            }

            
            if (iCheckedCount == 0)
            {
                MessageBox.Show(Common.GetMessage("VAL0609"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                //Added by Kaushik
                DataTable dtOrder = new DataTable();
                DataColumn CONo = new DataColumn();
                CONo.DataType = System.Type.GetType("System.String");
                CONo.Caption = "Customer OrderNo";
                CONo.ColumnName = "CustomerOrderNo";
                dtOrder.Columns.Add(CONo);

                if (lstCo.Count == 0)
                    return;
                order = new CO();
                // check for available puc deposit for this PC.
                decimal pucAvailableAmt = 0;
                decimal requiredAmt = totalPayment - totalChange;
                requiredAmt = Math.Round(requiredAmt, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                string pucMsg = order.CheckPUCAmount(lstCo[0].PCId, lstCo[0].BOId, totalPayment, totalChange, ref pucAvailableAmt);
                if (pucMsg != string.Empty)
                {
                    if (pucMsg.Contains("INF0145"))
                    {
                        MessageBox.Show(Common.GetMessage(pucMsg, pucAvailableAmt.ToString(), requiredAmt.ToString()), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show(Common.GetMessage(pucMsg), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    return;
                }
                

                int stLoop = 0;
                /////Kaushik starts
                foreach (CO o in lstCo)
                {
                    if (bw.CancellationPending)
                    {
                        e.Cancel = true;
                        return;
                    }
                    stLoop++;
                    bw.ReportProgress(stLoop);

                    bool isProcessed = invoice.ProcessLogInvoice((m_COLog.LogValue).ToString(), m_location, m_userID, o, ref error, ref dtItems);
                    lstCoPass = lstCo;
                    //bool isProcessed = true;
                    //error = "40008";
                    ////bool isProcessed = invoice.ProcessLogInvoice((m_COLog.LogValue).ToString(), m_location, m_userID, o, ref error);
                    if (isProcessed && error.Trim().Equals(string.Empty))
                    {
                        
                        //MessageBox.Show(Common.GetMessage("8011"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //GetOrderList(true, lstCo);
                        //GetOrderList(false, lstCo);
                    }
                    else if (!isProcessed && error.Trim().Equals(string.Empty))
                    {
                        ////GetOrderList(true);
                        //GetOrderList(false);
                    }
                    else
                    {
                        if (error.Contains(","))
                        {
                            MessageBox.Show(Common.GetMessage(error.Split(",".ToCharArray()[0])), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else if (error.Contains("40008"))
                        {
                            if (dtItems != null && dtItems.Rows.Count > 0)
                            {
                                dtItems2 = dtItems.Copy();
                            }
                            else
                            {
                                MessageBox.Show(Common.GetMessage(error), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        //dtItems2 = dtItems.Copy();
                    }
                }
                dtItems.Clear();
                dtItems3.Clear();
                e.Result = Common.GetMessage("8011");
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
            }
            finally
            {
            }
        }
    }
}
            
               
           
 

        
        
 
       

