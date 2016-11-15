using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CoreComponent.BusinessObjects;
using CoreComponent.Core.BusinessObjects;
using AuthenticationComponent.BusinessObjects;

namespace CoreComponent.UI
{
    public partial class frmCarRegistration : CoreComponent.Core.UI.Hierarchy
    {
        List<CarRegistration> m_CarRegistrationSearchList;

        List<CarRegistration> m_lstCarRegistration;
        CarRegistrationDetails m_itemDetail;
        CarBonusPartPayment m_CarBonusPartPayment;

        int m_selectedItemRowNum = Common.INT_DBNULL;

        int m_selectedItemRowIndex = Common.INT_DBNULL;
        List<CarRegistrationDetails> m_lstItemDetail;
        List<CarBonusPartPayment> m_lstCarBonusPartPayment;
        int m_userId = 0;
        DateTime m_modifiedDate = new DateTime(); //for concurrency control check
        private Boolean m_isSaveAvailable = true;
        private Boolean m_isSearchAvailable = true;
        StringBuilder sbError;
        CarRegistration carRegisObject;
        private static DateTime m_PaymentDate;

        private DataSet m_printDataSet = null;

        public frmCarRegistration()
        {
            lblPageTitle.Text = "Car Registration";

            // m_isSaveAvailable = Authenticate.IsFunctionAccessible(strUserName, strLocationCode, LocationHierarchy.MODULE_CODE, Common.FUNCTIONCODE_SAVE);
            // m_isSearchAvailable = Authenticate.IsFunctionAccessible(strUserName, strLocationCode, LocationHierarchy.MODULE_CODE, Common.FUNCTIONCODE_SEARCH);

            InitializeComponent();
            //this.pnlDetailsHeader.Size = new System.Drawing.Size(900, 175);
            this.Size = new Size(870, 703);

            Control[] ctrls = this.pnlDetailsHeader.Controls.Find("pnlTopButtons", true);

            if (ctrls != null)
            {
                foreach (Control ctrl in ctrls)
                {
                    if (ctrl is Panel)
                    {
                        (ctrl as Panel).Visible = true;
                    }

                }
            }
            GridInitialize();

            InitializeControls();
            btnAddDetails.Visible = false;
            btnClearDetails.Visible = false;
            dtpFirstPayoutBusinessMonth.MaxDate = System.DateTime.Now;
            //Bikram:
            lblPPAmt.Enabled = false;
            lblPPPaymentMode.Enabled = false;
            lblppRemarls.Enabled = false;
            txtPPAmount.Enabled = false;
            txtppPaymentMode.Enabled = false;
            txtPPRemarks.Enabled = false;
            lblPPdate.Enabled = false;
            dtpPPDate.Enabled = false;
            dgvPartPayment.Enabled = false;            
        }

       

        private void InitializeControls()
        {
            // m_lstContact = new List<Contact>();
            this.dgvCarRegistSearch.AutoGenerateColumns = false;
            dgvCarRegistSearch.DataSource = null;
            DataGridView dgvSearch = Common.GetDataGridViewColumns(dgvCarRegistSearch, Environment.CurrentDirectory + "\\App_Data\\GridViewDefinition.xml");

        }


        private void dgvCarRegistSearch_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if ((e.RowIndex >= 0) && (dgvCarRegistSearch.Rows[e.RowIndex].Cells[e.ColumnIndex].GetType() == typeof(DataGridViewImageCell)))
                {
                    //pnlSearchHeader.Enabled = true;
                    btnSave.Enabled = m_isSaveAvailable;
                    //btnSearch.Enabled = false;
                    EditCarRegistration(e.RowIndex, e.ColumnIndex);
                }
                //EditLocation(e.RowIndex, e.ColumnIndex);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EditCarRegistration(int rowIndex, int columnIndex)
        {
            if (rowIndex >= 0)
            {
                //Bikram
                m_lstItemDetail = null;
                m_lstItemDetail = carRegisObject.CarRegisDetails;

                m_lstCarBonusPartPayment = null;
                m_lstCarBonusPartPayment = carRegisObject.CarBonusPartPayment;

                this.tabPageCreate.Text = Common.TAB_UPDATE_MODE;
                this.tabControlHierarchy.SelectTab(this.tabPageCreate);

                if (m_lstCarRegistration == null)
                    m_lstCarRegistration = carRegisObject.CarRegisHeaders;

                if (m_lstItemDetail == null)
                    m_lstItemDetail = carRegisObject.CarRegisDetails;

                if (m_CarRegistrationSearchList == null)
                    m_CarRegistrationSearchList = carRegisObject.CarRegisSearchList;

                if (m_lstCarBonusPartPayment == null)
                    m_lstCarBonusPartPayment = carRegisObject.CarBonusPartPayment;

                //Bind Car Header Section
                CarRegistration cr = m_lstCarRegistration.Find(p => p.DistributorId == (int)dgvCarRegistSearch.Rows[rowIndex].Cells["DistributorId"].Value);

                txtDistributorId.Text = dgvCarRegistSearch.Rows[rowIndex].Cells["DistributorId"].Value.ToString().Trim();
                txtCarNumber.Text = cr.CarNumber;
                txtCarValue.Text = Convert.ToString(cr.CarValue);
                txtMaximumCarValue.Text = Convert.ToString(cr.MaximumCarValue);
                txtFirstPayoutValue.Text = Convert.ToString(cr.FirstPayoutValue);
                dtpCarPurchaseDate.Value = Convert.ToDateTime(cr.FirstCarPurDate);
                dtpFirstPayoutBusinessMonth.Value = cr.FirstPayoutDate;

                //Bind Car Details Section
                cr.CarRegisDetails = m_lstItemDetail.FindAll(p => p.DistId == (int)dgvCarRegistSearch.Rows[rowIndex].Cells["DistributorId"].Value);
                m_lstItemDetail = cr.CarRegisDetails;
                dgvCarRegDetails.DataSource = m_lstItemDetail;

                //Bind Car Part Payment Section
                cr.CarBonusPartPayment = m_lstCarBonusPartPayment.FindAll(p => p.DistributorID == (int)dgvCarRegistSearch.Rows[rowIndex].Cells["DistributorId"].Value);
                m_lstCarBonusPartPayment = cr.CarBonusPartPayment;
                dgvPartPayment.DataSource = m_lstCarBonusPartPayment;

                //if car details has been found then will not allow any more part payment
                foreach (Control ctrl in pnlDetailsHeader.Controls)
                {
                    ctrl.Enabled = false;
                }
                if (m_lstItemDetail.Count <= 0)
                {                    
                    foreach (Control ctrl in pnlCreateDetails.Controls)
                    {
                        ctrl.Enabled = false;
                    }
                    btnAddCarRegNoAndAmount.Enabled = true;
                    btnClear.Enabled = true;
                    chkIsPartPayment.Enabled = true;
                    chkIsPartPayment.Checked = true;

                    lblPPAmt.Enabled = true;
                    lblPPPaymentMode.Enabled = true;
                    lblppRemarls.Enabled = true;
                    txtPPAmount.Enabled = true;
                    txtppPaymentMode.Enabled = true;
                    txtPPRemarks.Enabled = true;
                    lblPPdate.Enabled = true;
                    dtpPPDate.Enabled = true;
                    dgvPartPayment.Enabled = true;
                }
                else
                {                    
                    foreach (Control ctrl in pnlCreateDetails.Controls)
                    {
                        ctrl.Enabled = true;
                    }
                    //Disable Max car value and frist car number
                    lblMaximumCarValue.Enabled = false;
                    txtMaximumCarValue.Enabled = false;
                    lblCarNumber.Enabled = false;
                    txtCarNumber.Enabled = false;


                    chkIsPartPayment.Enabled = false;
                    lblPPAmt.Enabled = false;
                    lblPPPaymentMode.Enabled = false;
                    lblppRemarls.Enabled = false;
                    txtPPAmount.Enabled = false;
                    txtppPaymentMode.Enabled = false;
                    txtPPRemarks.Enabled = false;
                    lblPPdate.Enabled = false;
                    dtpPPDate.Enabled = false;
                    dgvPartPayment.Enabled = false;
                }
                
                if(txtDistributorId.Text !="")
                {
                    fncGetMaximumPaymentDate(Convert.ToInt32(txtDistributorId.Text));
                
                }
                
                errorSaveCarRegistration.Clear();
            }
        }       

        private void btnSearchReset_Click(object sender, EventArgs e)
        {
            ResetSearch();
        }

       

        private void btnClearDetails_Click(object sender, EventArgs e)
        {
            //this.tabPageCreate.Text = Common.TAB_CREATE_MODE;

            //clearDetailSections();
            //clearHeaderSection();
        }

        private void clearHeaderSection()
        {
            try
            {
                txtDistributorId.Text = string.Empty;
                txtCarValue.Text = string.Empty;
                txtMaximumCarValue.Text = string.Empty;
                txtFirstPayoutValue.Text = string.Empty;
                txtCarNumber.Text = string.Empty;
                dtpCarPurchaseDate.Value = DateTime.Today;
                dtpCarPurchaseDate.Checked = false;
                dtpFirstPayoutBusinessMonth.Value = DateTime.Today;
                dtpFirstPayoutBusinessMonth.Checked = false;
                m_lstCarRegistration = null;

                chkIsPartPayment.Checked = false;
                foreach (Control ctrl in pnlDetailsHeader.Controls)
                {
                    ctrl.Enabled = true;
                }
                //Disable Max car value and frist car number
                lblMaximumCarValue.Enabled = false;
                txtMaximumCarValue.Enabled = false;
                lblCarNumber.Enabled = false;
                txtCarNumber.Enabled = false;

                errorSaveCarRegistration.SetError(txtDistributorId, string.Empty);
                errorSaveCarRegistration.SetError(txtCarNumber, string.Empty);
                errorSaveCarRegistration.SetError(txtCarValue, string.Empty);
                errorSaveCarRegistration.SetError(txtMaximumCarValue, string.Empty);
                errorSaveCarRegistration.SetError(txtFirstPayoutValue, string.Empty);
                errorSaveCarRegistration.SetError(dtpCarPurchaseDate, string.Empty);
                // errorSaveCarRegistration.SetError(dtpFirstPayoutBusinessMonth, string.Empty);

                errorSaveCarRegistration.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);            
            }

        }

        private void btnAddDetails_Click(object sender, EventArgs e)
        {

        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {

                this.errorSearchCarRegistration.Clear();
                CarRegistration carRegistration = new CarRegistration();
                carRegistration.CarNumber = !string.IsNullOrEmpty(txtCarRegisSearch.Text.Trim()) ? txtCarRegisSearch.Text.Trim() : "-1";

                //carRegistration.DistributorId = !string.IsNullOrEmpty(txtDistIdSearch.Text) ? Convert.ToInt32(txtDistIdSearch.Text) : 0;
                carRegistration.DistributorId = !string.IsNullOrEmpty(txtDistIdSearch.Text) ? Convert.ToInt32(txtDistIdSearch.Text) : -1;


                DateTime dtPayoutDateFrom = dtpPayoutDateFrom.Checked == true ? Convert.ToDateTime(dtpPayoutDateFrom.Value) : Common.DATETIME_NULL;
                carRegistration.FirstPayoutBusinessMonthFrom = Convert.ToDateTime(dtPayoutDateFrom).ToString(Common.DATE_TIME_FORMAT);


                DateTime dtPayoutDateTo = this.dtpPayoutTo.Checked == true ? Convert.ToDateTime(dtpPayoutTo.Value) : Common.DATETIME_NULL;
                carRegistration.FirstPayoutBusinessMonthTo = Convert.ToDateTime(dtPayoutDateTo).ToString(Common.DATE_TIME_FORMAT);

                DateTime dtCarPurchaseDateFrom = dtpCarPurDateFrom.Checked == true ? Convert.ToDateTime(dtpCarPurDateFrom.Value) : Common.DATETIME_NULL;
                carRegistration.CarPurchaseDateFrom = Convert.ToDateTime(dtCarPurchaseDateFrom).ToString(Common.DATE_TIME_FORMAT);

                DateTime dtCarPurchaseDateTo = dtpCarPurDateTo.Checked == true ? Convert.ToDateTime(dtpCarPurDateTo.Value) : Common.DATETIME_NULL;
                carRegistration.CarPurchaseDateTo = Convert.ToDateTime(dtCarPurchaseDateTo).ToString(Common.DATE_TIME_FORMAT);


                //carRegistration.CarNumber = txtCarNumber.Text;


                Search(carRegistration);
                if ((m_CarRegistrationSearchList != null) && (m_CarRegistrationSearchList.Count > 0))
                    dgvCarRegistSearch.DataSource = m_CarRegistrationSearchList;

                else
                {
                    dgvCarRegistSearch.DataSource = new List<CarRegistration>();
                    MessageBox.Show(Common.GetMessage("8002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Search(CarRegistration carRegistration)
        {

            string errMsg = string.Empty;
            CarRegistration carRegis = carRegistration.CarRegistrationSearch(Common.ToXml(carRegistration), CarRegistration.CAR_REGISTRATION_SEARCH, ref errMsg);
            m_lstCarRegistration = carRegis.CarRegisHeaders;
            m_lstItemDetail = carRegis.CarRegisDetails;
            m_CarRegistrationSearchList = carRegis.CarRegisSearchList;
            m_lstCarBonusPartPayment = carRegis.CarBonusPartPayment;

            carRegisObject = carRegis;


        }
        private void ResetSearch()
        {
            m_lstCarRegistration = null;
            m_lstItemDetail = null;
            m_CarRegistrationSearchList = null;
            m_CarRegistrationSearchList = null;
            txtCarRegisSearch.Text = string.Empty;
            txtDistIdSearch.Text = string.Empty;
            dtpCarPurDateFrom.Value = DateTime.Now;
            dtpCarPurDateFrom.Checked = false;
            dtpCarPurDateTo.Value = DateTime.Now;
            dtpCarPurDateTo.Checked = false;
            dgvCarRegistSearch.DataSource = m_CarRegistrationSearchList;
            carRegisObject = null;
        }

        private bool SaveCarRegistration()
        {
            try
            {
                #region Validation Code

                StringBuilder sbError = new StringBuilder();
                sbError = ValidateCarRegistrationData();

                if (!sbError.ToString().Trim().Equals(string.Empty))
                {
                    MessageBox.Show(sbError.ToString(), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (m_lstItemDetail == null)
                {
                    MessageBox.Show("Please enter car details.", Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                else if (m_lstItemDetail.Count == 0)
                {
                    MessageBox.Show("Please enter car details.", Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);                    
                    return false;
                }

                #endregion Validation Code

                if (sbError.ToString().Trim().Equals(string.Empty))
                {
                    string msg = string.Empty;
                    if (m_userId > 0)
                        msg = "Edit";
                    else
                        msg = "Save";

                    DialogResult saveResult = MessageBox.Show(Common.GetMessage("5010", msg), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (saveResult == DialogResult.Yes)
                    {

                        CarRegistration m_objCarRegistration = new CarRegistration();
                        m_objCarRegistration.DistributorId = Convert.ToInt32(txtDistributorId.Text.Trim());

                        if (!string.IsNullOrEmpty(txtCarNumber.Text))                        
                        {
                            m_objCarRegistration.CarNumber = txtCarNumber.Text.Trim();
                        }

                        if (Convert.ToDecimal(txtCarValue.Text) > 0m)
                        {
                            m_objCarRegistration.CarValue = Convert.ToDecimal(txtCarValue.Text);
                        }

                        if (Convert.ToDecimal(txtMaximumCarValue.Text) > 0m)
                        {
                            m_objCarRegistration.MaximumCarValue = Convert.ToDecimal(txtMaximumCarValue.Text);
                        }

                        if (Convert.ToDecimal(txtFirstPayoutValue.Text) > 0m)                        
                        {
                            m_objCarRegistration.FirstPayoutValue = Convert.ToDecimal(this.txtFirstPayoutValue.Text);
                        }

                        DateTime dtCarPurchaseDate = dtpCarPurchaseDate.Checked == true ? Convert.ToDateTime(dtpCarPurchaseDate.Value) : Common.DATETIME_NULL;
                        m_objCarRegistration.CarPurchaseDate = Convert.ToDateTime(dtCarPurchaseDate).ToString(Common.DATE_TIME_FORMAT);


                        DateTime dtFirstPayoutBusinessMonth = dtpFirstPayoutBusinessMonth.Checked == true ? Convert.ToDateTime(dtpFirstPayoutBusinessMonth.Value) : Common.DATETIME_NULL;
                        m_objCarRegistration.FirstPayoutBusinessMonth = Convert.ToDateTime(dtFirstPayoutBusinessMonth).ToString(Common.DATE_TIME_FORMAT);

                        if (!chkIsPartPayment.Checked)
                        {
                            m_objCarRegistration.IsFirstPayout = true;
                        }
                        
                        if (m_userId == 0)
                        {
                            foreach (CarRegistrationDetails itemDetail in m_lstItemDetail)
                            {
                                itemDetail.CreatedBy = Authenticate.LoggedInUser.UserId;
                                itemDetail.CreatedDate = DateTime.Now.ToString(Common.DATE_TIME_FORMAT);

                                if (itemDetail.DistId != m_objCarRegistration.DistributorId)
                                {
                                    itemDetail.DistId = m_objCarRegistration.DistributorId;
                                }
                            }
                        }
                        else //m_userId > 0
                        {
                            //m_objCarRegistration.ModifiedDate = Convert.ToDateTime(m_modifiedDate).ToString(Common.DATE_TIME_FORMAT); ;
                        }
                        m_objCarRegistration.CarRegisDetails = new List<CarRegistrationDetails>();
                        m_objCarRegistration.CarRegisDetails = m_lstItemDetail;


                        string errMsg = string.Empty;
                        bool retVal = m_objCarRegistration.CarRegistrationSave(Common.ToXml(m_objCarRegistration), CarRegistration.CAR_REGISTRATION_SAVE, ref errMsg);
                        if (retVal)
                        {
                            MessageBox.Show(Common.GetMessage("8001"), Common.GetMessage("10001"),
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return true;
                        }
                        else if (errMsg.Equals("VAL0007"))//Duplicate data
                        {
                            MessageBox.Show(Common.GetMessage(errMsg,
                                lblDistributorId.Text.Substring(0, lblDistributorId.Text.Trim().Length - 2)),
                                Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                        else if (errMsg.Equals("INF0022"))//Concurrency
                        {
                            MessageBox.Show(Common.GetMessage("INF0022"), Common.GetMessage("10001"),
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                        else
                        {
                            Common.LogException(new Exception(errMsg));
                            MessageBox.Show(Common.GetMessage("2003"), Common.GetMessage("10001"),
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }

                }
                return false;                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
       
        private StringBuilder ValidateCarDetatils()
        {
           
            errorSaveCarRegistration.SetError(txtCarNoInDetailSec, string.Empty);
            errorSaveCarRegistration.SetError(txtDistributorId, string.Empty);
            errorSaveCarRegistration.SetError(txtMaxCarBonusInDetailSec, string.Empty);

            errorSaveCarRegistration.Clear();

            if (!Validators.IsInt32(txtDistributorId.Text))
            {
                errorSaveCarRegistration.SetError(txtDistributorId, "Please enter valid Distributor Id.");
            }
            if (!Validators.IsAlphaNumHyphen(txtCarNoInDetailSec.Text))
            {
                errorSaveCarRegistration.SetError(txtCarNoInDetailSec, "Please enter Car Number.");
            }
            if (!Validators.IsDecimal(txtMaxCarBonusInDetailSec.Text))
            {
                errorSaveCarRegistration.SetError(txtMaxCarBonusInDetailSec, "Please enter Car Bonus.");
            }
            if (Validators.IsDecimal(txtMaxCarBonusInDetailSec.Text))
            {
                if (Convert.ToInt32(txtMaxCarBonusInDetailSec.Text) <= 0)
                {
                    errorSaveCarRegistration.SetError(txtMaxCarBonusInDetailSec, "Please enter Valid Car Bonus.");
                }
            }
            if ((dpCarPurDateInDetailSec.Checked != true))
            {
                errorSaveCarRegistration.SetError(dpCarPurDateInDetailSec, "Please enter Purchase Date.");
            }

            StringBuilder sbError = new StringBuilder();
            CheckBlankMessage(Validators.GetErrorMessage(errorSaveCarRegistration, txtDistributorId), sbError);
            sbError.AppendLine();
            CheckBlankMessage(Validators.GetErrorMessage(errorSaveCarRegistration, txtCarNoInDetailSec), sbError);
            sbError.AppendLine();
            CheckBlankMessage(Validators.GetErrorMessage(errorSaveCarRegistration, txtMaxCarBonusInDetailSec), sbError);
            sbError.AppendLine();
            CheckBlankMessage(Validators.GetErrorMessage(errorSaveCarRegistration, dpCarPurDateInDetailSec), sbError);
            sbError.AppendLine();
            CheckBlankMessage(Validators.GetErrorMessage(errorSaveCarRegistration, txtFirstPayoutValue), sbError);
            sbError.AppendLine();

            return Common.ReturnErrorMessage(sbError);
        }

        private StringBuilder ValidateDistId()
        {
            errorSaveCarRegistration.SetError(txtDistributorId, string.Empty);
            
            errorSaveCarRegistration.Clear();

            if (!Validators.IsInt32(txtDistributorId.Text))
            {
                errorSaveCarRegistration.SetError(txtDistributorId, "Please enter Valid Distributor ID.");
            }
            
            StringBuilder sbError = new StringBuilder();
            CheckBlankMessage(Validators.GetErrorMessage(errorSaveCarRegistration, txtDistributorId), sbError);
            sbError.AppendLine();
            
            return Common.ReturnErrorMessage(sbError);
        }


        private StringBuilder ValidateCarRegistrationData()
        {
            errorSaveCarRegistration.SetError(txtDistributorId, string.Empty);
            errorSaveCarRegistration.SetError(txtCarValue, string.Empty);
            errorSaveCarRegistration.SetError(txtFirstPayoutValue, string.Empty);
            errorSaveCarRegistration.SetError(dtpCarPurchaseDate, string.Empty);
            errorSaveCarRegistration.SetError(dtpFirstPayoutBusinessMonth, string.Empty);

            errorSaveCarRegistration.Clear();

            if (!Validators.IsInt32(txtDistributorId.Text))
            {
                errorSaveCarRegistration.SetError(txtDistributorId, "Please enter Valid Distributor ID.");
            }
            if (!Validators.IsDecimal(txtCarValue.Text))
            {
                errorSaveCarRegistration.SetError(txtCarValue, "Please enter Car Value.");
            }
           
            if (!Validators.IsDecimal(txtFirstPayoutValue.Text))
            {
                errorSaveCarRegistration.SetError(txtFirstPayoutValue, "Please enter First Payout");
            }
           
            if (dtpCarPurchaseDate.Checked == false)
            {
                errorSaveCarRegistration.SetError(dtpCarPurchaseDate, "Please enter Car Purchase Date.");
            }
            if (dtpFirstPayoutBusinessMonth.Checked == false)
            {
                errorSaveCarRegistration.SetError(dtpFirstPayoutBusinessMonth, "Please enter First Payout Date.");
            }

            StringBuilder sbError = new StringBuilder();
            CheckBlankMessage(Validators.GetErrorMessage(errorSaveCarRegistration, txtDistributorId), sbError);
            sbError.AppendLine();
            CheckBlankMessage(Validators.GetErrorMessage(errorSaveCarRegistration, txtCarValue), sbError);
            sbError.AppendLine();
            CheckBlankMessage(Validators.GetErrorMessage(errorSaveCarRegistration, txtFirstPayoutValue), sbError);
            sbError.AppendLine();
            CheckBlankMessage(Validators.GetErrorMessage(errorSaveCarRegistration, dtpCarPurchaseDate), sbError);
            sbError.AppendLine();
            CheckBlankMessage(Validators.GetErrorMessage(errorSaveCarRegistration, dtpFirstPayoutBusinessMonth), sbError);
            sbError.AppendLine();           

            return Common.ReturnErrorMessage(sbError);

        }

        private void CheckBlankMessage(string strMessage, StringBuilder sb)
        {
            if (strMessage != "")
            {
                sb.Append(strMessage);
                sb.AppendLine();
            }
        }
        private void btnCreateReset_Click(object sender, EventArgs e)
        {
            this.tabPageCreate.Text = Common.TAB_CREATE_MODE;

            clearDetailSections();
            clearHeaderSection();

            lblAvailableBalance.Text = "0.00";
            lblName.Text = "";

            m_lstItemDetail = null;
            dgvCarRegDetails.DataSource = null;

            m_lstCarBonusPartPayment = null;
            dgvPartPayment.DataSource = null;


        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //if (txtDistributorId.Text == "")
                //    return;

                //int DistributorID = Convert.ToInt32(txtDistributorId.Text);
                if (chkIsPartPayment.Checked)
                {
                    if (!SaveCarBonusPartPayment())
                    {
                        return;
                    }
                }
                else
                {
                    if (!SaveCarRegistration())
                    {
                        return;
                    }
                }                
                btnSearch_Click(null, null);
                fncGetMaximumPaymentDate(Convert.ToInt32(txtDistributorId.Text));
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        /// <summary>
        /// Bikram:Save car bonus part payment
        /// </summary>
        private bool SaveCarBonusPartPayment()
        {
            try
            {
                string msg = string.Empty;
                if (m_userId > 0)
                msg = "Edit";
                else
                msg = "Save";

                if (m_lstCarBonusPartPayment == null)
                {
                    MessageBox.Show("Please add part payment details.", Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                if (m_lstCarBonusPartPayment.Count == 0)
                {
                    MessageBox.Show("Please add part payment details.", Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                DialogResult saveResult = MessageBox.Show(Common.GetMessage("5010", msg), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (saveResult == DialogResult.Yes)
                {
                    CarRegistration m_objCarRegistration = new CarRegistration();
                    m_objCarRegistration.DistributorId = Convert.ToInt32(txtDistributorId.Text.Trim());
                    if (m_userId == 0)
                    {
                        foreach (CarBonusPartPayment partpayment in m_lstCarBonusPartPayment)
                        {
                            partpayment.CreatedBy = Authenticate.LoggedInUser.UserId;
                            partpayment.CreatedDate = DateTime.Now.ToString(Common.DATE_TIME_FORMAT);

                            if (partpayment.DistributorID != m_objCarRegistration.DistributorId)
                            {
                                partpayment.DistributorID = m_objCarRegistration.DistributorId;
                            }
                        }
                    }

                    m_objCarRegistration.CarBonusPartPayment = new List<CarBonusPartPayment>();
                    m_objCarRegistration.CarBonusPartPayment = m_lstCarBonusPartPayment;

                    string errMsg = string.Empty;
                    bool retVal = m_objCarRegistration.CarRegistrationSave(Common.ToXml(m_objCarRegistration), CarRegistration.CAR_REGISTRATION_PARTPAYMENT_SAVE, ref errMsg);
                    if (retVal)
                    {
                        MessageBox.Show(Common.GetMessage("8001"), Common.GetMessage("10001"),
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return true;

                    }
                    else if (errMsg.Equals("VAL0007"))//Duplicate data
                    {
                        MessageBox.Show(Common.GetMessage(errMsg,
                        lblDistributorId.Text.Substring(0, lblDistributorId.Text.Trim().Length - 2)),
                        Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;

                    }
                    else if (errMsg.Equals("INF0022"))//Concurrency
                    {
                        MessageBox.Show(Common.GetMessage("INF0022"), Common.GetMessage("10001"),
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;

                    }
                    else
                    {
                        Common.LogException(new Exception(errMsg));
                        MessageBox.Show(Common.GetMessage("2003"), Common.GetMessage("10001"),
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;

                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;               
            }
        }

        private void dgvCarRegDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        void GridInitialize()
        {
            dgvCarRegDetails.AutoGenerateColumns = false;
            dgvCarRegDetails.DataSource = null;
            DataGridView dgvCarRegDetailsNew = Common.GetDataGridViewColumns(dgvCarRegDetails, Environment.CurrentDirectory + "\\App_Data\\GridViewDefinition.xml");

            dgvPartPayment.AutoGenerateColumns = false;
            dgvPartPayment.DataSource = null;
            DataGridView dgvPartPaymentNew = Common.GetDataGridViewColumns(dgvPartPayment, Environment.CurrentDirectory + "\\App_Data\\GridViewDefinition.xml");




        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox4_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void btnAddCarRegNoAndAmount_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkIsPartPayment.Checked)
                {
                    //Validate
                    StringBuilder sbError = new StringBuilder();
                    sbError = ValidateCarPartPayment();

                    if (!sbError.ToString().Trim().Equals(string.Empty))
                    {
                        MessageBox.Show(sbError.ToString(), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (Convert.ToDateTime(m_PaymentDate.ToString(Common.DATE_FORMAT)) > Convert.ToDateTime(dtpPPDate.Value.ToString(Common.DATE_FORMAT)))
                    {
                        MessageBox.Show("Payout date can not be less than " + String.Format("{0:dd-MM-yyyy}", m_PaymentDate), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    //Save Part Payment details
                    BindPartPaymentGrid();
                    //txtPPRemarks.Text = "";
                }

                else
                {
                    StringBuilder sbError = new StringBuilder();
                    sbError = ValidateAddCarRegNoAndAmountData();

                    if (!sbError.ToString().Trim().Equals(string.Empty))
                    {
                        MessageBox.Show(sbError.ToString(), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Only for first payment
                    if (m_lstItemDetail == null)
                    {
                        if (Convert.ToDateTime(m_PaymentDate.ToShortDateString()) > Convert.ToDateTime(dtpFirstPayoutBusinessMonth.Value.ToShortDateString()))
                        {
                            MessageBox.Show("Payout date can not be less than " +  String.Format("{0:dd-MM-yyyy}", m_PaymentDate), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        if (Convert.ToInt32(txtFirstPayoutValue.Text) > Convert.ToInt32(txtCarValue.Text))
                        {
                            MessageBox.Show("Payout Amount should be less than Car value.", Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                    else if (m_lstItemDetail.Count == 0)
                    {
                        if (Convert.ToDateTime(m_PaymentDate.ToShortDateString()) > Convert.ToDateTime(dtpFirstPayoutBusinessMonth.Value.ToShortDateString()))
                        {
                            MessageBox.Show("Payout date can not be less then " + String.Format("{0:dd-MM-yyyy}", m_PaymentDate), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        if (Convert.ToDecimal(txtFirstPayoutValue.Text) > Convert.ToDecimal(txtCarValue.Text))
                        {
                            MessageBox.Show("Payout Amount should be less then Car value.", Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }

                    // Save car bonus details 
                    if (AddItem())
                    {
                        BindItemGrid();

                        if (m_lstItemDetail != null && m_lstItemDetail.Count > 0)
                            txtCarNumber.Text = m_lstItemDetail[0].CarNo;

                        clearDetailSections();
                    }
                    dpCarPurDateInDetailSec.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
           
        }

        private StringBuilder ValidateAddCarRegNoAndAmountData()
        {
            errorSaveCarRegistration.SetError(txtDistributorId, string.Empty);
            errorSaveCarRegistration.SetError(txtCarNumber, string.Empty);
            errorSaveCarRegistration.SetError(txtCarValue, string.Empty);
            errorSaveCarRegistration.SetError(txtMaximumCarValue, string.Empty);
            errorSaveCarRegistration.SetError(txtFirstPayoutValue, string.Empty);
            errorSaveCarRegistration.SetError(dtpCarPurchaseDate, string.Empty);
            errorSaveCarRegistration.SetError(txtCarNoInDetailSec, string.Empty);
            errorSaveCarRegistration.SetError(txtDistributorId, string.Empty);
            errorSaveCarRegistration.SetError(txtMaxCarBonusInDetailSec, string.Empty);
            errorSaveCarRegistration.SetError(dpCarPurDateInDetailSec, string.Empty);

            errorSaveCarRegistration.Clear();

            if (!Validators.IsInt32(txtDistributorId.Text))
            {
                errorSaveCarRegistration.SetError(txtDistributorId, "Please enter Valid Distributor ID.");
            }
            if (!Validators.IsDecimal(txtCarValue.Text))
            {
                errorSaveCarRegistration.SetError(txtCarValue, "Please enter Car Value.");
            }
            if (Validators.IsDecimal(txtCarValue.Text))
            {
                if (Convert.ToDecimal(txtCarValue.Text) <= 0)
                {
                    errorSaveCarRegistration.SetError(txtCarValue, "Please enter Valid Car Value.");
                }
            }
            if (!Validators.IsDecimal(txtFirstPayoutValue.Text))
            {
                errorSaveCarRegistration.SetError(txtFirstPayoutValue, "Please enter First Payout");
            }
            if (Validators.IsDecimal(txtFirstPayoutValue.Text))
            {
                if (Convert.ToDecimal(txtFirstPayoutValue.Text) <= 0)
                {
                    errorSaveCarRegistration.SetError(txtFirstPayoutValue, "Please enter Valid First Payout.");
                }
            }
            if (dtpCarPurchaseDate.Checked == false)
            {
                errorSaveCarRegistration.SetError(dtpCarPurchaseDate, "Please enter Car Purchase Date.");
            }
            if (dtpFirstPayoutBusinessMonth.Checked == false)
            {
                errorSaveCarRegistration.SetError(dtpFirstPayoutBusinessMonth, "Please enter First Payout Date.");
            }

            if (!Validators.IsAlphaNumHyphen(txtCarNoInDetailSec.Text))
            {
                errorSaveCarRegistration.SetError(txtCarNoInDetailSec, "Please enter Car Number.");
            }
            if (!Validators.IsDecimal(txtMaxCarBonusInDetailSec.Text))
            {
                errorSaveCarRegistration.SetError(txtMaxCarBonusInDetailSec, "Please enter Car Bonus.");
            }
            if (Validators.IsDecimal(txtMaxCarBonusInDetailSec.Text))
            {
                if (Convert.ToInt32(txtMaxCarBonusInDetailSec.Text) <= 0)
                {
                    errorSaveCarRegistration.SetError(txtMaxCarBonusInDetailSec, "Please enter Valid Car Bonus.");
                }
            }
            if ((dpCarPurDateInDetailSec.Checked == false) && (dpCarPurDateInDetailSec.Enabled==true))
            {
                errorSaveCarRegistration.SetError(dpCarPurDateInDetailSec, "Please enter Car Purchase Date.");
            }
            StringBuilder sbError = new StringBuilder();
            CheckBlankMessage(Validators.GetErrorMessage(errorSaveCarRegistration, txtDistributorId), sbError);
            sbError.AppendLine();
            CheckBlankMessage(Validators.GetErrorMessage(errorSaveCarRegistration, txtCarValue), sbError);
            sbError.AppendLine();
            CheckBlankMessage(Validators.GetErrorMessage(errorSaveCarRegistration, txtFirstPayoutValue), sbError);
            sbError.AppendLine();
            CheckBlankMessage(Validators.GetErrorMessage(errorSaveCarRegistration, dtpCarPurchaseDate), sbError);
            sbError.AppendLine();
            CheckBlankMessage(Validators.GetErrorMessage(errorSaveCarRegistration, dtpFirstPayoutBusinessMonth), sbError);
            sbError.AppendLine();
            CheckBlankMessage(Validators.GetErrorMessage(errorSaveCarRegistration, txtCarNoInDetailSec), sbError);
            sbError.AppendLine();
            CheckBlankMessage(Validators.GetErrorMessage(errorSaveCarRegistration, txtMaxCarBonusInDetailSec), sbError);
            sbError.AppendLine();
            CheckBlankMessage(Validators.GetErrorMessage(errorSaveCarRegistration, dpCarPurDateInDetailSec), sbError);
            sbError.AppendLine();

            return Common.ReturnErrorMessage(sbError);

        }
        /// <summary>
        /// Validate Part Payment details
        /// </summary>
        /// <returns></returns>
        private StringBuilder ValidateCarPartPayment()
        {
            errorSaveCarRegistration.SetError(txtDistributorId, string.Empty);
            errorSaveCarRegistration.SetError(txtPPAmount, string.Empty);            
            errorSaveCarRegistration.SetError(txtPPRemarks, string.Empty);
            errorSaveCarRegistration.SetError(txtppPaymentMode, string.Empty);
            errorSaveCarRegistration.Clear();

            if (!Validators.IsInt32(txtDistributorId.Text))
            {
                errorSaveCarRegistration.SetError(txtDistributorId, "Please enter Valid Distributor ID.");
            }
            if (!Validators.IsDecimal(txtPPAmount.Text))
            {
                errorSaveCarRegistration.SetError(txtPPAmount, "Please enter Part Payment Amount.");
            }

            if (Validators.IsDecimal(txtPPAmount.Text))
            {
                if (Convert.ToDecimal(txtPPAmount.Text) <= 0)
                {
                    errorSaveCarRegistration.SetError(txtPPAmount, "Please enter valid Part Payment Amount.");
                }
            }

            if (txtPPRemarks.Text == String.Empty)
            {
                errorSaveCarRegistration.SetError(txtPPRemarks, "Please enter Remarks.");            
            }
            if (txtppPaymentMode.Text == String.Empty)
            {
                errorSaveCarRegistration.SetError(txtppPaymentMode, "Please enter Payment Mode.");
            }
            if ((dtpPPDate.Checked != true))
            {
                errorSaveCarRegistration.SetError(dtpPPDate, "Please enter Part Payment Date.");
            }

            StringBuilder sbError = new StringBuilder();
            CheckBlankMessage(Validators.GetErrorMessage(errorSaveCarRegistration, txtDistributorId), sbError);
            sbError.AppendLine();
            CheckBlankMessage(Validators.GetErrorMessage(errorSaveCarRegistration, txtPPAmount), sbError);
            sbError.AppendLine();
            CheckBlankMessage(Validators.GetErrorMessage(errorSaveCarRegistration, txtPPRemarks), sbError);
            sbError.AppendLine();
            CheckBlankMessage(Validators.GetErrorMessage(errorSaveCarRegistration, txtppPaymentMode), sbError);
            sbError.AppendLine();
            CheckBlankMessage(Validators.GetErrorMessage(errorSaveCarRegistration, dtpPPDate), sbError);
            sbError.AppendLine();

            return Common.ReturnErrorMessage(sbError);
        }
        /// <summary>
        /// Bikram: 29-05-2012 - Part Payment
        /// </summary>
        private void BindPartPaymentGrid()
        {
            //Check the availability of Balance amount
            if (!ValidateAmountPaid())
                return;

            m_CarBonusPartPayment = new CarBonusPartPayment();
            m_CarBonusPartPayment.DistributorID = Convert.ToInt32(txtDistributorId.Text);
            m_CarBonusPartPayment.PartPaymentAmount = Convert.ToDecimal(txtPPAmount.Text);
            m_CarBonusPartPayment.PartPaymentDate = dtpPPDate.Value.ToString(Common.DTP_DATE_FORMAT);
            m_CarBonusPartPayment.Remarks = txtPPRemarks.Text;
            m_CarBonusPartPayment.PaymentMode = txtppPaymentMode.Text;

            if (m_lstCarBonusPartPayment == null)
            {
                m_lstCarBonusPartPayment = new List<CarBonusPartPayment>();
            }

            if ((m_selectedItemRowIndex != Common.INT_DBNULL) && (m_selectedItemRowIndex <= dgvPartPayment.Rows.Count))
            {
                m_lstCarBonusPartPayment.Insert(m_selectedItemRowIndex, m_CarBonusPartPayment);
                m_lstCarBonusPartPayment.RemoveAt(m_selectedItemRowIndex + 1);
            }
            else if (!ContainsPartPaymentItem(m_lstCarBonusPartPayment, m_CarBonusPartPayment))
                m_lstCarBonusPartPayment.Add(m_CarBonusPartPayment);

            dgvPartPayment.DataSource = null;
            dgvPartPayment.DataSource = m_lstCarBonusPartPayment;
            clearDetailSections();
        }
        /// <summary>
        /// Validate Amount need to pay
        /// </summary>
        /// <returns></returns>
        private bool ValidateAmountPaid()
        {
            if (txtDistributorId.Text == "")
                return false;

            int DistributorID = Convert.ToInt32(txtDistributorId.Text);
            DataSet dsBalanceAmount = CarRegistration.fnBalanceAmount(DistributorID, CarRegistration.CAR_DISTRIBUTOR_BALANCE_AMOUNT);
            Decimal BalanceAmount = dsBalanceAmount.Tables[0].Rows[0]["BalanceAmount"] != DBNull.Value ? Convert.ToDecimal(dsBalanceAmount.Tables[0].Rows[0]["BalanceAmount"].ToString()) : 0;                       
            
            if (chkIsPartPayment.Checked)
            {
                if (Convert.ToDecimal(txtPPAmount.Text) > BalanceAmount)
                {
                    MessageBox.Show("Payment amount can not be more then balance amount " + BalanceAmount.ToString(), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPPAmount.Text = "";
                    return false;
                }
            }
            else
            {
                if (Convert.ToDecimal(txtFirstPayoutValue.Text) > BalanceAmount)
                {
                    MessageBox.Show("Payment amount can not be more then balance amount " + BalanceAmount.ToString(), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtFirstPayoutValue.Text = "";
                    return false;
                }
            }
            return true;
            
        }

        bool AddItem()
        {
            #region Validation Code

            StringBuilder sbError = new StringBuilder();
            sbError = ValidateCarDetatils();

            if (!sbError.ToString().Trim().Equals(string.Empty))
            {
                MessageBox.Show(sbError.ToString(), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }           

            #endregion Validation Code

            m_itemDetail = new CarRegistrationDetails();
            m_itemDetail.DistId = Convert.ToInt32(txtDistributorId.Text);
            m_itemDetail.CarNo = txtCarNoInDetailSec.Text;

            if (txtMaxCarBonusInDetailSec.Text != string.Empty)
                m_itemDetail.MaxCarValue = Convert.ToDecimal(txtMaxCarBonusInDetailSec.Text);

            DateTime dtCarPurchaseDate = dpCarPurDateInDetailSec.Checked == true ? Convert.ToDateTime(dpCarPurDateInDetailSec.Value) : Common.DATETIME_NULL;
            m_itemDetail.CarPurchDate = Convert.ToDateTime(dtCarPurchaseDate).ToString(Common.DTP_DATE_FORMAT);
            //m_itemDetail.CarPurchDate = dtCarPurchaseDate.ToString();

            if (m_lstItemDetail == null)
                m_lstItemDetail = new List<CarRegistrationDetails>();

            if ((m_selectedItemRowIndex != Common.INT_DBNULL) && (m_selectedItemRowIndex <= dgvCarRegDetails.Rows.Count))
            {
                m_lstItemDetail.Insert(m_selectedItemRowIndex, m_itemDetail);
                m_lstItemDetail.RemoveAt(m_selectedItemRowIndex + 1);
            }
            else if (!ContainsItem(m_lstItemDetail, m_itemDetail))
                m_lstItemDetail.Add(m_itemDetail);

            //ResetItemControl();
            return true;
        }

        private bool ContainsItem(List<CarRegistrationDetails> m_lstItemDetail, CarRegistrationDetails m_itemDetail)
        {
            if (m_lstItemDetail.Find(p => p.DistId == m_itemDetail.DistId && p.CarNo == m_itemDetail.CarNo) != null)
                return true;

            else
                return false;
        }
        private bool ContainsPartPaymentItem(List<CarBonusPartPayment> m_lstCarBonusPartPayment, CarBonusPartPayment m_CarBonusPartPayment)
        {
            if (m_lstCarBonusPartPayment.Find(p => p.DistributorID == m_CarBonusPartPayment.DistributorID && 
                p.PartPaymentAmount == m_CarBonusPartPayment.PartPaymentAmount &&
                p.PartPaymentDate == m_CarBonusPartPayment.PartPaymentDate &&
                p.Remarks == m_CarBonusPartPayment.Remarks)
                != null)
                return true;

            else
                return false;
        }

        void BindItemGrid()
        {
            dgvCarRegDetails.DataSource = null;
            dgvCarRegDetails.DataSource = m_lstItemDetail;

            txtMaximumCarValue.Text = string.Empty;
            decimal sum = 0M;
            if (m_lstItemDetail != null)
            {

                foreach (var item in m_lstItemDetail)
                {

                    sum += item.MaxCarValue;
                }
            }

            txtMaximumCarValue.Text = sum.ToString();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clearDetailSections();
        }


        private void clearDetailSections()
        {           

            foreach (Control ctrl in pnlCreateDetails.Controls)
            {
                if (ctrl is TextBox)
                {
                    ((TextBox)ctrl).Text = string.Empty;
                }
                if (ctrl is DateTimePicker)
                {
                    ((DateTimePicker)ctrl).Value = DateTime.Now;
                    ((DateTimePicker)ctrl).Checked = false;
                }
            }

            errorSaveCarRegistration.SetError(txtDistributorId, string.Empty);
            errorSaveCarRegistration.SetError(txtMaxCarBonusInDetailSec, string.Empty);
            errorSaveCarRegistration.SetError(txtCarNoInDetailSec, string.Empty);
            errorSaveCarRegistration.Clear();
        }       
      

        private void txtCarNoInDetailSec_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //string errMsg = string.Empty;
                //CarRegistration cr = new CarRegistration();
                //cr.ValidateCarNoInSystem(txtCarNoInDetailSec.Text, CarRegistration.CAR_REGIS_NO_SEARCH, ref errMsg);

                //if (string.IsNullOrEmpty(errMsg))
                //{
                //    MessageBox.Show(Common.GetMessage("40040"), Common.GetMessage("10003"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}
                //else if (string.IsNullOrEmpty(errMsg))
                //{
                //    MessageBox.Show(Common.GetMessage("40040"), Common.GetMessage("10003"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}
            }
        }

        private void chkIsPartPayment_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIsPartPayment.Checked)
            {
                foreach (Control ctrl in pnlDetailsHeader.Controls)
                {
                    ctrl.Enabled = false;
                }
                foreach (Control ctrl in pnlCreateDetails.Controls)
                {
                    ctrl.Enabled = false;
                }
                btnAddCarRegNoAndAmount.Enabled = true;
                btnClear.Enabled = true;
                chkIsPartPayment.Enabled = true;

                lblPPAmt.Enabled = true;
                lblPPPaymentMode.Enabled = true;
                lblppRemarls.Enabled = true;
                txtPPAmount.Enabled = true;
                txtppPaymentMode.Enabled = true;
                txtPPRemarks.Enabled = true;
                lblPPdate.Enabled = true;
                dtpPPDate.Enabled = true;
                dgvPartPayment.Enabled = true;

            }
            else
            {
                foreach (Control ctrl in pnlDetailsHeader.Controls)
                {
                    ctrl.Enabled = true;
                }
                foreach (Control ctrl in pnlCreateDetails.Controls)
                {
                    ctrl.Enabled = true;
                }
                //Disable Max car value and frist car number
                lblMaximumCarValue.Enabled = false;
                txtMaximumCarValue.Enabled = false;
                lblCarNumber.Enabled = false;
                txtCarNumber.Enabled = false;



                lblPPAmt.Enabled = false;
                lblPPPaymentMode.Enabled = false;
                lblppRemarls.Enabled = false;
                txtPPAmount.Enabled = false;
                txtppPaymentMode.Enabled = false;
                txtPPRemarks.Enabled = false;
                lblPPdate.Enabled = false;
                dtpPPDate.Enabled = false;
                dgvPartPayment.Enabled = false;
            }

        }
        /// <summary>
        /// Print Car Bonus Deatils
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkIsPartPayment.Checked)
                {
                    if (m_lstCarBonusPartPayment == null)
                    {
                        MessageBox.Show("No details found.", Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return ;
                    }
                    else if (m_lstCarBonusPartPayment.Count == 0)
                    {
                         MessageBox.Show("No details found.", Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                         return ;
                    }
                }
                else
                {                    
                    if (m_lstItemDetail == null)
                    {
                        MessageBox.Show("No details found.", Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return ;
                    }
                    else if (m_lstItemDetail.Count == 0)
                    {
                        MessageBox.Show("No details found.", Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return ;
                    }
                }  

                if(!string.IsNullOrEmpty(txtDistributorId.Text))
                {
                    btnPrint.Enabled = false;
                    btnPrint1.Enabled = false;
                    PrintReport();
                    btnPrint1.Enabled = true;
                    btnPrint.Enabled = true;
                }
             }
            catch (Exception ex)
            {
                btnPrint.Enabled = true;
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PrintReport()
        {
            CreatePrintDataSet();
            CoreComponent.UI.ReportScreen reportScreenObj = new CoreComponent.UI.ReportScreen((int)Common.ReportType.CarBonusReport, m_printDataSet);            
            reportScreenObj.ShowDialog();
            //reportScreenObj.PrintReport();
            m_printDataSet = null;
        }

        private void CreatePrintDataSet()
        {
            string strError = "";
            try
            {
                m_printDataSet = CarRegistration.GetCarBonusReport(Convert.ToInt32(txtDistributorId.Text), ref strError);            
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
        }

        private void txtDistributorId_Leave(object sender, EventArgs e)
        {
            try
            {
                if(!string.IsNullOrEmpty(txtDistributorId.Text))
                {
                    if (!Validators.IsInt32(txtDistributorId.Text))
                    {
                        MessageBox.Show("Only Integer value is allowed in Distributor Id.", Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    if (fncGetMaximumPaymentDate(Convert.ToInt32(txtDistributorId.Text)))
                    {
                        fnGetDistributorDetail();
                        m_lstCarBonusPartPayment = null;
                        m_lstItemDetail = null; ;
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.txtDistributorId.Text = "";
                return;
            }
        }

        private void fnGetDistributorDetail()
        {
            try
            {
                int DistributorID = Convert.ToInt32(txtDistributorId.Text);
                DataSet dsDistributor = CarRegistration.fnBalanceAmount(DistributorID, CarRegistration.CAR_DISTRIBUTOR_DETAIL);
                if (dsDistributor != null && dsDistributor.Tables[0].Rows.Count > 0)
                {
                    MessageBox.Show("Distributor details already exist. Please use search option to get details.", Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblName.Text = "";
                    lblAvailableBalance.Text = "";
                    txtDistributorId.Text = "";
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }
        private bool fncGetMaximumPaymentDate(int DistributorID)
        {  
            DataSet dsBalanceAmount = CarRegistration.fnBalanceAmount(DistributorID, CarRegistration.CAR_DISTRIBUTOR_BALANCE_AMOUNT);
            if (dsBalanceAmount != null && dsBalanceAmount.Tables[0].Rows.Count > 0)
            {
                m_PaymentDate = dsBalanceAmount.Tables[0].Rows[0]["BusinessMonth"] != DBNull.Value ? Convert.ToDateTime(dsBalanceAmount.Tables[0].Rows[0]["BusinessMonth"].ToString()) : DateTime.Now;
                lblName.Text = dsBalanceAmount.Tables[0].Rows[0]["Name"].ToString();
                lblAvailableBalance.Text = dsBalanceAmount.Tables[0].Rows[0]["BalanceAmount"] != DBNull.Value ? Convert.ToInt32(dsBalanceAmount.Tables[0].Rows[0]["BalanceAmount"]).ToString() : "0.00";
                return true;
            }
            else
            {
                MessageBox.Show("Please enter valid Distributer ID.", Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDistributorId.Text = "";
                lblName.Text = "";
                lblAvailableBalance.Text = "";
                this.txtDistributorId.Focus();
                return false;
            }
            return false;
        }
        private void txtDistIdSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar > 31 && (e.KeyChar < '0' || e.KeyChar > '9'))
            {
                e.Handled = true;
            }
        }

        private void txtDistributorId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar > 31 && (e.KeyChar < '0' || e.KeyChar > '9'))
            {
                e.Handled = true;
            }
        }

        private void dtpCarPurchaseDate_Leave(object sender, EventArgs e)
        {
            dpCarPurDateInDetailSec.Value = dtpCarPurchaseDate.Value;
            dpCarPurDateInDetailSec.Checked = true;
            dpCarPurDateInDetailSec.Enabled = false;
        }

        private void txtFirstPayoutValue_Leave(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(txtFirstPayoutValue.Text))
            {
                if (!ValidateAmountPaid())
                    return;
            }
        }

        private void txtMaxCarBonusInDetailSec_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar > 31 && (e.KeyChar < '0' || e.KeyChar > '9'))
            {
                e.Handled = true;
            }

        }

        private void txtFirstPayoutValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar > 31 && (e.KeyChar < '0' || e.KeyChar > '9'))
            {
                e.Handled = true;
            }
        }

        private void txtCarValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar > 31 && (e.KeyChar < '0' || e.KeyChar > '9'))
            {
                e.Handled = true;
            }
        }

        private void txtMaximumCarValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar > 31 && (e.KeyChar < '0' || e.KeyChar > '9'))
            {
                e.Handled = true;
            }
        }

        private void txtPPAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar > 31 && (e.KeyChar < '0' || e.KeyChar > '9'))
            {
                e.Handled = true;
            }
        }

        private void dtpFirstPayoutBusinessMonth_ValueChanged(object sender, EventArgs e)
        {

        }

        private void txtDistIdSearch_Leave(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(txtDistributorId.Text))
            {
                if (!Validators.IsInt32(txtDistributorId.Text))
                {
                    MessageBox.Show("Only Integer value is allowed in Distributor Id.", Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
        }
    }
}
