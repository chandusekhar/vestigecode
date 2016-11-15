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
using Ex = Microsoft.Office.Interop.Excel;

namespace CoreComponent.UI
{
    public partial class frnDistributorTravelFund : CoreComponent.Core.UI.Hierarchy
    {
        public const string PROMO_Beneficiary = "Beneficiary";
        private TravelFund m_TravelFundObject;
        private List<TravelFund> lstTravelFund;
        private List<TravelFundDetails> lstTravelFundDetails;
        private List<TravelFund> lstTravelFundSearch;
        private DateTime m_LastBusinessMonth;
        private DataSet m_printDataSet = null;
        private int m_userId = Authenticate.LoggedInUser.UserId;
        string m_dirPath;

        public frnDistributorTravelFund()
        {
            m_dirPath = Environment.CurrentDirectory;
            lblPageTitle.Text = "Travel Fund";
            InitializeComponent();
            this.Size = new Size(870, 703);
            GridInitialize();
            lblDistributorName.Text = "";
            lblBalanceAmount.Text = "0.00";
        }

        private void FillStatus()
        {
            DataTable dtTemp;

            dtTemp = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter(PROMO_Beneficiary, 0, 0, 0));
            cmbBeneficiary.DataSource = dtTemp;
            cmbBeneficiary.ValueMember = Common.KEYCODE1;
            cmbBeneficiary.DisplayMember = Common.KEYVALUE1;

        }

        private void frnDistributorTravelFund_Load(object sender, EventArgs e)
        {
            try
            {
                FillStatus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void txtDistributorID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar > 31 && (e.KeyChar < '0' || e.KeyChar > '9'))
            {
                e.Handled = true;
            }
        }

        private void txtDistributorID_Leave(object sender, EventArgs e)
        {
            if (this.ActiveControl.Name == "btnExit")
                return;
            try
            {
                if (!string.IsNullOrEmpty(txtDistributorID.Text))
                {
                    if (!Validators.IsInt32(txtDistributorID.Text))
                    {
                        MessageBox.Show("Only Integer value is allowed in Distributor Id.", Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (fncDistriExistInTravelFund())
                        return;

                    fncShowBalance();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.txtDistributorID.Text = "";
                return;
            }
        }

        private void fncShowBalance()
        {
            DataSet dsDistributorDetails;

            dsDistributorDetails = TravelFund.fncGetDetails(Convert.ToInt32(txtDistributorID.Text), TravelFund.DISTRI_GET_DETAIL);
            if (dsDistributorDetails.Tables[0].Rows.Count > 0)
            {
                lblBalanceAmount.Text = dsDistributorDetails.Tables[0].Rows[0]["BalanceAmount"].ToString();
                lblDistributorName.Text = dsDistributorDetails.Tables[0].Rows[0]["Name"].ToString();
                m_LastBusinessMonth = Convert.ToDateTime(dsDistributorDetails.Tables[0].Rows[0]["BusinessMonth"]);
            }
            else
            {
                MessageBox.Show("Invalid Distributor for Travel Fund.", Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDistributorID.Text = "";
                lblBalanceAmount.Text = "";
                lblDistributorName.Text = "";
                txtDistributorID.Focus();
                return;
            }
        }
        /// <summary>
        /// if distruibutor already exist in TravleFUND
        /// then records only can be update using serach/update option
        /// </summary>
        /// <returns></returns>
        private bool fncDistriExistInTravelFund()
        {   
            bool IsExist;
            IsExist = false;
            try
            {
                DataSet dsDistriExistInTravelFund;
                dsDistriExistInTravelFund = TravelFund.fncGetDetails(Convert.ToInt32(txtDistributorID.Text), TravelFund.DISTRI_EXIST_IN_TRAVELFUND);
                if (dsDistriExistInTravelFund.Tables[0].Rows.Count > 0)
                {
                    MessageBox.Show("Distributor details already exist. Please use search option to get details.", Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblDistributorName.Text = "";
                    lblBalanceAmount.Text = "0.00";
                    txtDistributorID.Text = "";
                    //dgvTravelFundDetails.DataSource = null;
                    txtDistributorID.Focus();
                    IsExist = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return IsExist;        
        }

        private void btnAddDet_Click(object sender, EventArgs e)
        {
            System.IO.Directory.SetCurrentDirectory(m_dirPath);

            try
            {                
                //Validate
                StringBuilder sbError = new StringBuilder();
                sbError = fncValidateDetails();

                if (!sbError.ToString().Trim().Equals(string.Empty))
                {
                    MessageBox.Show(sbError.ToString(), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (fncValidatePaymentAmount())
                {
                    if (!BindTravelFundDetail())
                    {
                        btnAddDet.Enabled = false;
                        fncResetDetailsSec();
                    }
                }
            }
            catch (Exception ex)
            {
                btnAddDet.Enabled = true;
                MessageBox.Show(ex.Message.ToString(), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void fncResetDetailsSec()
        {
            txtAmount.Text = "";
            txtRemarks.Text = "";
            cmbBeneficiary.SelectedIndex = 0;
        }
        /// <summary>
        /// Form level validation
        /// </summary>
        /// <returns></returns>
        private StringBuilder fncValidateDetails()
        {
            this.errTravelFundValidate.SetError(this.txtDistributorID, string.Empty);
            this.errTravelFundValidate.SetError(this.txtAmount, string.Empty);
            this.errTravelFundValidate.SetError(this.cmbBeneficiary, string.Empty);
            this.errTravelFundValidate.SetError(this.txtRemarks, string.Empty);
            this.errTravelFundValidate.SetError(this.dtpBusinessMonth, string.Empty);
            this.errTravelFundValidate.Clear();
            if (!Validators.IsInt32(this.txtDistributorID.Text))
            {
                this.errTravelFundValidate.SetError(this.txtDistributorID, "Please enter Distributor Id.");
            }
            if (!Validators.IsInt32(this.txtAmount.Text))
            {
                this.errTravelFundValidate.SetError(this.txtAmount, "Please enter Amount.");
            }
            if (Validators.IsInt32(this.txtAmount.Text) && (Convert.ToInt32(this.txtAmount.Text) <= 0))
            {
                this.errTravelFundValidate.SetError(this.txtAmount, "Please enter Amount.");
            }
            if (Convert.ToInt32(this.cmbBeneficiary.SelectedValue) < 0)
            {
                this.errTravelFundValidate.SetError(this.cmbBeneficiary, "Please enter Beneficiary.");
            }
            if (this.txtRemarks.Text.Trim() == string.Empty)
            {
                this.errTravelFundValidate.SetError(this.txtRemarks, "Please enter Remarks");
            }
            StringBuilder sb = new StringBuilder();
            this.CheckBlankMessage(Validators.GetErrorMessage(this.errTravelFundValidate, this.txtDistributorID), sb);
            sb.AppendLine();
            this.CheckBlankMessage(Validators.GetErrorMessage(this.errTravelFundValidate, this.txtAmount), sb);
            sb.AppendLine();
            this.CheckBlankMessage(Validators.GetErrorMessage(this.errTravelFundValidate, this.cmbBeneficiary), sb);
            sb.AppendLine();
            this.CheckBlankMessage(Validators.GetErrorMessage(this.errTravelFundValidate, this.txtRemarks), sb);
            sb.AppendLine();
            return Common.ReturnErrorMessage(sb);

        }
        private void CheckBlankMessage(string strMessage, StringBuilder sb)
        {
            if (strMessage != "")
            {
                sb.Append(strMessage);
                sb.AppendLine();
            }
        }
        /// <summary>
        /// Payment amount should not be  more then available balance;
        /// </summary>
        private bool fncValidatePaymentAmount()
        {
            if (Convert.ToDecimal(txtAmount.Text) > Convert.ToDecimal(lblBalanceAmount.Text))
            {
                MessageBox.Show("Payment amount can not be more than balance amount " + lblBalanceAmount.Text, Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtAmount.Text = "";
                return false;
            }
            if (dtpBusinessMonth.Value < m_LastBusinessMonth)
            {
                MessageBox.Show("Payment date can not be less than or equal to " + String.Format("{0:dd-MM-yyyy}", m_LastBusinessMonth), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;
        }

        private bool BindTravelFundDetail()
        {
            bool IsAdded = false;
            try
            {

                if ((lstTravelFundDetails == null) || (this.tabPageCreate.Text == Common.TAB_CREATE_MODE))
                    lstTravelFundDetails = new List<TravelFundDetails>();

                

                TravelFundDetails objTravelFundDetails = new TravelFundDetails();
                objTravelFundDetails.DistributorID = Convert.ToInt32(txtDistributorID.Text);
                objTravelFundDetails.BeneficiaryID = Convert.ToInt32(cmbBeneficiary.SelectedValue);
                objTravelFundDetails.Beneficiary = cmbBeneficiary.Text.ToString();
                objTravelFundDetails.PaidAmount = Convert.ToDecimal(txtAmount.Text);
                //objTravelFundDetails.BusinessMonth = DateTime.Now.ToString(Common.DATE_TIME_FORMAT);
                objTravelFundDetails.BusinessMonth = DateTime.Now.ToString("dd MMM yyyy");
                objTravelFundDetails.Remarks = txtRemarks.Text;
                objTravelFundDetails.CreatedBy = m_userId;
                objTravelFundDetails.CreatedDate = DateTime.Now.ToString(Common.DATE_TIME_FORMAT) ;

                if (!IsContainInDetailList(lstTravelFundDetails, objTravelFundDetails))
                {
                    IsAdded = false;                    
                    lstTravelFundDetails.Add(objTravelFundDetails);
                }
                else
                {
                    IsAdded = true;
                    MessageBox.Show("Record already exist for current date.", Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return IsAdded;
                }
                dgvTravelFundDetails.DataSource = null;
                dgvTravelFundDetails.DataSource = lstTravelFundDetails;
                return IsAdded;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return IsAdded;
            }
        }
        /// <summary>
        /// if record alreadyt exist then return TRUE elae FLASE
        /// </summary>
        /// <returns></returns>       
        private bool IsContainInDetailList(List<TravelFundDetails> m_lstTravelFundDetails, TravelFundDetails m_TravelFundDetails)
        {
            if (m_lstTravelFundDetails.Find(p => p.DistributorID == m_TravelFundDetails.DistributorID &&
                Convert.ToDateTime(p.BusinessMonth).ToString(Common.DATE_FORMAT)  == Convert.ToDateTime(m_TravelFundDetails.BusinessMonth).ToString(Common.DATE_FORMAT)) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        void GridInitialize()
        {
            try
            {
                dgvTravelFundDetails.AutoGenerateColumns = false;
                dgvTravelFundDetails.DataSource = null;
                DataGridView dgvTravelFundDetail = Common.GetDataGridViewColumns(dgvTravelFundDetails, Environment.CurrentDirectory + "\\App_Data\\GridViewDefinition.xml");

                dgvTravelFundSearch.AutoGenerateColumns = false;
                dgvTravelFundSearch.DataSource = null;
                DataGridView dgvTravelFundSear = Common.GetDataGridViewColumns(dgvTravelFundSearch, Environment.CurrentDirectory + "\\App_Data\\GridViewDefinition.xml");


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            System.IO.Directory.SetCurrentDirectory(m_dirPath);

            try
            {
                StringBuilder sbError = new StringBuilder();
                sbError = fncValidate();

                if (!sbError.ToString().Trim().Equals(string.Empty))
                {
                    MessageBox.Show(sbError.ToString(), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (dgvTravelFundDetails.RowCount == 0)
                {
                    MessageBox.Show("No payment details found", Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                fncSaveTravelFund();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private StringBuilder fncValidate()
        {
            this.errTravelFundValidate.SetError(this.txtDistributorID, string.Empty);
            this.errTravelFundValidate.SetError(this.dgvTravelFundDetails, string.Empty);
            this.errTravelFundValidate.Clear();
            if (!Validators.IsInt32(this.txtDistributorID.Text))
            {
                this.errTravelFundValidate.SetError(this.txtDistributorID, "Please select Distributor Id.");
            }
            StringBuilder sb = new StringBuilder();
            this.CheckBlankMessage(Validators.GetErrorMessage(this.errTravelFundValidate, this.txtDistributorID), sb);
            sb.AppendLine();
            return Common.ReturnErrorMessage(sb);

        }

        private void fncSaveTravelFund()
        {
            try
            {
                string msg = string.Empty;                
                msg = "Save";       

                DialogResult saveResult = MessageBox.Show(Common.GetMessage("5010", msg), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (saveResult == DialogResult.Yes)
                {
                    if (!Validators.IsInt32(this.txtAmount.Text) && !string.IsNullOrEmpty(txtAmount.Text))
                    {
                        MessageBox.Show("Please enter valid amount.", Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    TravelFund objTravelFund = new TravelFund();
                    objTravelFund.DistributorID = Convert.ToInt32(txtDistributorID.Text.Trim());
                    objTravelFund.BeneficiaryID = Convert.ToInt32(cmbBeneficiary.SelectedValue);
                    objTravelFund.PaidAmount = !string.IsNullOrEmpty(txtAmount.Text) ? Convert.ToDecimal(txtAmount.Text.ToString()) : 0;
                    objTravelFund.BusinessMonth = Convert.ToDateTime(DateTime.Now).ToString(Common.DATE_TIME_FORMAT);
                    objTravelFund.CreatedBy = m_userId;
                    objTravelFund.CreatedDate = DateTime.Now.ToString(Common.DATE_TIME_FORMAT);

                    objTravelFund.lstTravelFundDetails = new List<TravelFundDetails>();
                    objTravelFund.lstTravelFundDetails = lstTravelFundDetails;

                    string errMsg = string.Empty;
                    bool retVal = objTravelFund.TravelFundSave(Common.ToXml(objTravelFund), TravelFund.DIST_TRAVEL_FUND_SAVE, ref errMsg);
                    if (retVal)
                    {
                        MessageBox.Show(Common.GetMessage("8001"), Common.GetMessage("10001"),
                        MessageBoxButtons.OK, MessageBoxIcon.Information);                        
                        //fncResetForm();
                        //update search
                        btnSearch_Click(null, null);
                       
                    }
                    else if (errMsg.Equals("VAL0007"))//Duplicate data
                    {
                        MessageBox.Show(Common.GetMessage(errMsg,
                        lblDistributorID.Text.Substring(0, lblDistributorID.Text.Trim().Length - 2)),
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }        

        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar > 31 && (e.KeyChar < '0' || e.KeyChar > '9'))
            {
                e.Handled = true;
            }
        }

        private void btnClearDet_Click(object sender, EventArgs e)
        {
            System.IO.Directory.SetCurrentDirectory(m_dirPath);
            txtAmount.Text = "";
            txtRemarks.Text = "";
            cmbBeneficiary.SelectedIndex = 0;
            errTravelFundValidate.Clear();
        }

        private void txtDistributorSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar > 31 && (e.KeyChar < '0' || e.KeyChar > '9'))
            {
                e.Handled = true;
            }
        }

        private void btnCreateReset_Click(object sender, EventArgs e)
        {
            System.IO.Directory.SetCurrentDirectory(m_dirPath);
            fncResetForm();
            errTravelFundValidate.Clear();
        }

        private void fncResetForm()
        {
            this.tabPageCreate.Text = Common.TAB_CREATE_MODE;
            txtDistributorID.Text = "";
            txtDistributorID.Enabled = true;
            lblBalanceAmount.Text = "0.00";
            lblDistributorName.Text = "";

            //clear all fieald            
            fncResetDetailsSec();

            btnAddDet.Enabled = true;

            lstTravelFundDetails = null;
            dgvTravelFundDetails.DataSource = null;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            System.IO.Directory.SetCurrentDirectory(m_dirPath);
            try
            {
                TravelFund objTravelFund = new TravelFund();
                if(!string.IsNullOrEmpty(txtDistributorSearch.Text.Trim()))
                {
                    if (!Validators.IsInt32(this.txtDistributorSearch.Text))
                    {
                        MessageBox.Show("Only Integer value is allowed in Distributor Id.", Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                objTravelFund.DistributorID = !string.IsNullOrEmpty(txtDistributorSearch.Text.Trim()) ? Convert.ToInt32(txtDistributorSearch.Text) : -1;
                
                DateTime dtpBusinessMonth = dtpDateSearch.Checked == true ? Convert.ToDateTime(dtpDateSearch.Value) : Common.DATETIME_NULL;
                objTravelFund.BusinessMonth = dtpBusinessMonth.ToString(Common.DATE_TIME_FORMAT) ;


                Search(objTravelFund);

                if ((lstTravelFundSearch != null) && (lstTravelFundSearch.Count > 0))
                    dgvTravelFundSearch.DataSource = lstTravelFundSearch;

                else
                {
                    dgvTravelFundSearch.DataSource = new List<TravelFund>();
                    MessageBox.Show(Common.GetMessage("8002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDistributorSearch.Text = "";
                    dtpDateSearch.Value = DateTime.Now;
                    dtpDateSearch.Checked = false;
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Search(TravelFund objTravelFund)
        {
            string errMsg = string.Empty;
            TravelFund objTFund = TravelFund.fncTravelFundSearch(Common.ToXml(objTravelFund), TravelFund.DIST_TRAVEL_FUND_SEARCH, ref errMsg);
            lstTravelFundSearch = objTFund.LstTravelFundSearch;
            lstTravelFundDetails = objTFund.lstTravelFundDetails;
            m_TravelFundObject = objTFund;
        }

        private void btnSearchReset_Click(object sender, EventArgs e)
        {
            System.IO.Directory.SetCurrentDirectory(m_dirPath);
            txtImportExcel.Text = "";
            txtDistributorSearch.Text = "";
            dtpDateSearch.Value = DateTime.Now;
            dtpDateSearch.Checked = false;
            
            m_TravelFundObject = null;
            dgvTravelFundSearch.DataSource = null;
            
        }

        private void dgvTravelFundSearch_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if ((e.RowIndex >= 0) && (dgvTravelFundSearch.Rows[e.RowIndex].Cells[e.ColumnIndex].GetType() == typeof(DataGridViewImageCell)))
                {
                    txtAmount.Text = "";
                    txtRemarks.Text = "";
                    cmbBeneficiary.SelectedIndex = 0;
                    EditTravelFund(e.RowIndex, e.ColumnIndex);
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EditTravelFund(int rowIndex, int columnIndex)
        {
            if (rowIndex < 0)
                return;

            lstTravelFundSearch = null;
            lstTravelFundSearch = m_TravelFundObject.LstTravelFundSearch;

            lstTravelFundDetails = null;
            lstTravelFundDetails = m_TravelFundObject.lstTravelFundDetails;

            this.tabPageCreate.Text = Common.TAB_UPDATE_MODE;
            this.tabControlHierarchy.SelectTab(this.tabPageCreate);

            //header section
            TravelFund objTravelFund = lstTravelFundSearch.Find(p => p.DistributorID == (int)dgvTravelFundSearch.Rows[rowIndex].Cells["DistributorID"].Value);
            txtDistributorID.Text = objTravelFund.DistributorID.ToString();
            lblDistributorName.Text = objTravelFund.Name.ToString();
            lblBalanceAmount.Text = objTravelFund.BalanceAmt.ToString();
            //last business month
            m_LastBusinessMonth = Convert.ToDateTime(objTravelFund.BusinessMonth);

            //bind detail section
            objTravelFund.lstTravelFundDetails = lstTravelFundDetails.FindAll(p => p.DistributorID == (int)dgvTravelFundSearch.Rows[rowIndex].Cells["DistributorID"].Value);
            lstTravelFundDetails = objTravelFund.lstTravelFundDetails;
            dgvTravelFundDetails.DataSource = lstTravelFundDetails;

            btnAddDet.Enabled = true;
            errTravelFundValidate.Clear();
            txtDistributorID.Enabled = false;

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            System.IO.Directory.SetCurrentDirectory(m_dirPath);
            try
            {
                StringBuilder builder = new StringBuilder();
                builder = this.fncValidate();
                if (!builder.ToString().Trim().Equals(string.Empty))
                {
                    MessageBox.Show(builder.ToString(), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                else if (this.dgvTravelFundDetails.RowCount == 0)
                {
                    MessageBox.Show("No payment details found", Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                else if (!string.IsNullOrEmpty(this.txtDistributorID.Text))
                {
                    this.btnPrint.Enabled = false;
                    this.PrintReport();
                    this.btnPrint.Enabled = true;
                }
            }
            catch (Exception exception)
            {
                this.btnPrint.Enabled = true;
                Common.LogException(exception);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }

        }

        private void PrintReport()
        {
            CreatePrintDataSet();
            CoreComponent.UI.ReportScreen reportScreenObj = new CoreComponent.UI.ReportScreen((int)Common.ReportType.TravelFundReport, m_printDataSet);
            reportScreenObj.ShowDialog();
            //reportScreenObj.PrintReport();
            m_printDataSet = null;
        }

        private void CreatePrintDataSet()
        {
            string strError = "";
            try
            {
                m_printDataSet = TravelFund.GetTravelFundReport(Convert.ToInt32(txtDistributorID.Text), ref strError);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            System.IO.Directory.SetCurrentDirectory(m_dirPath);
            ofdExcel.Filter = "Excel files (*.xls)|*.xls";
            ofdExcel.InitialDirectory = Environment.CurrentDirectory;
            ofdExcel.Title = "Select a Excel file";
            ofdExcel.FileName = "";
            DialogResult result = ofdExcel.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                string strExt = ofdExcel.FileName.Substring(ofdExcel.FileName.LastIndexOf("."));
                if (strExt.ToLower() != ".xls")
                {
                    MessageBox.Show("Invalid file.", "Bulk Import", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                string file = ofdExcel.FileName;
                txtImportExcel.Text = file;
            }
        }
        public static bool fncIsOpened(string strFileName)
        {
            try
            {
                System.IO.Stream s = System.IO.File.Open(strFileName, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.None);

                s.Close();

                return false;
            }
            catch (Exception)
            {
                return true;
            }

        }
        private void btnBulkImport_Click(object sender, EventArgs e)
        {
            System.IO.Directory.SetCurrentDirectory(m_dirPath);
            if (string.IsNullOrEmpty(txtImportExcel.Text))
            {
                MessageBox.Show("Please enter file name.", Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if(fncIsOpened(txtImportExcel.Text))
            {
                MessageBox.Show("Please close the file before import. \n File Name : " + txtImportExcel.Text, Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            btnBulkImport.Enabled = false;
            Cursor.Current = Cursors.WaitCursor;
            DataSet dsInvalidDistributor;
            try
            {
                List<TraveFundBulkImport> lst = new List<TraveFundBulkImport>();
                Ex.Application exc = new Ex.Application();
                Ex.Workbooks workbooks = exc.Workbooks;
                Ex.Workbook theWorkbook = workbooks.Open(txtImportExcel.Text, 0, true, 5, "", "", true, Ex.XlPlatform.xlWindows, "\t", false, false, 0, true, false, false);

                Ex.Sheets sheets = theWorkbook.Worksheets;
                Ex.Worksheet worksheet = (Ex.Worksheet)sheets.get_Item(1);

                //For Import We need to Unprotect the Sheet First
                //worksheet.Unprotect("");
                //Ex.Range unProtectedRange = worksheet.get_Range("A2", "B9999");
                //unProtectedRange.Locked = false;
                if (worksheet.Rows.CurrentRegion.Count <= 2)
                {
                    exc.Workbooks.Close();
                    exc.Quit();
                    MessageBox.Show("Invalid file.", Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);                    
                    btnBulkImport.Enabled = true;
                    Cursor.Current = Cursors.Default;
                    return;
                }
                
                for (int i = 2; i <= worksheet.Rows.CurrentRegion.Count / 2; i++)
                {
                    Ex.Range range = worksheet.get_Range("A" + i.ToString(), "C" + i.ToString());
                    System.Array myvalues = (System.Array)range.Cells.Value2;

                    TraveFundBulkImport objTFBI = new TraveFundBulkImport();
                    if (((object[,])(myvalues))[1, 1] != null)
                        objTFBI.DistributorID = Convert.ToInt32(((object[,])(myvalues))[1, 1].ToString());
                    //check for duplicate
                    if (lst.Find(p => p.DistributorID == objTFBI.DistributorID) != null)
                    {
                        MessageBox.Show("There are more than one records exist for distributor id " + objTFBI.DistributorID.ToString() + ".\n Please correct the data and re-try to import.", Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        exc.Workbooks.Close();
                        exc.Quit();
                        btnBulkImport.Enabled = true;
                        return;  
                    }
            
                    if (((object[,])(myvalues))[1, 2] != null)
                        objTFBI.Amount = Convert.ToDecimal(((object[,])(myvalues))[1, 2].ToString());

                    if (rbtnPayment.Checked)
                        objTFBI.PayOrAdjust = true;//payment
                    else
                        objTFBI.PayOrAdjust = false;//adjustment

                    if ((objTFBI.Amount) < 0 &&(rbtnPayment.Checked))
                    {
                        MessageBox.Show("Payment amount is invalid for distributor id " + objTFBI.DistributorID.ToString() + ".\n Please correct the amount and re-try again." , Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        exc.Workbooks.Close();
                        exc.Quit();
                        btnBulkImport.Enabled = true;
                        return;                    
                    }
                    if ((objTFBI.Amount) > 0 && (rbtnAdjustment.Checked))
                    {
                        MessageBox.Show("Adjustment amount is invalid for distributor id " + objTFBI.DistributorID.ToString() + ".\n Please correct the amount and re-try for payment.", Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        exc.Workbooks.Close();
                        exc.Quit();
                        btnBulkImport.Enabled = true;
                        return;
                    }
                    if (((object[,])(myvalues))[1, 3] != null)
                        objTFBI.Remarks = ((object[,])(myvalues))[1, 3].ToString();


                    if (((object[,])(myvalues))[1, 1] != null)
                        lst.Add(objTFBI);
                }
                               
                exc.Workbooks.Close();
                exc.Quit();
                string errMsg = string.Empty;
                dsInvalidDistributor = TravelFund.fncBuldImportTravelFund(Common.ToXml(lst), TravelFund.DIST_BULKIMPORT_TRAVEL_FUND, ref errMsg);
                if (errMsg.Length==0)
                {
                    MessageBox.Show("Data imported successfully", Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtImportExcel.Text = "";
                    btnBulkImport.Enabled = true;
                    Cursor.Current = Cursors.Default;
                    //Refresh search
                    btnSearch_Click(null, null);
                    return;
                }
                else
                {
                    fncWriteDistributorToExcelSheet(dsInvalidDistributor);
                    MessageBox.Show("Data has been imported successfully However there are invalid records exist.", Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtImportExcel.Text = "";
                    btnBulkImport.Enabled = true;
                    Cursor.Current = Cursors.Default;
                    //Refresh search
                    btnSearch_Click(null, null);
                    return;
                }
            }
            catch (Exception ex)
            {
                btnBulkImport.Enabled = true;
                Cursor.Current = Cursors.Default;
                System.IO.Directory.SetCurrentDirectory(m_dirPath);
                MessageBox.Show(ex.Message.ToString(), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

        }
        /// <summary>
        /// Write into Excel sheet
        /// </summary>
        /// <param name="dsInvalidDistributor"></param>
        private void fncWriteDistributorToExcelSheet(DataSet dsInvalidDistributor)
        {
            Ex.Application exc = new Ex.Application();
            Ex.Workbooks workbooks = exc.Workbooks;
            Ex.Workbook theWorkbook = workbooks.Open(txtImportExcel.Text, 0, false, 5, "", "", true, Ex.XlPlatform.xlWindows, "\t", false, false, 0, true, false, false);
            Ex.Sheets sheets = theWorkbook.Worksheets;
            //------------------Distributor not exist
            Ex._Worksheet worksheet = ((Ex._Worksheet)(sheets[2]));

            Ex.Range range1;
            worksheet.Name = "Distributor not exist";
            Ex.Range xlCells;
            range1 = worksheet.Cells;
            xlCells = worksheet.Cells;

            worksheet.Columns.ColumnWidth = 15;
            worksheet.get_Range("A1", "C1").Font.Bold = true;


            worksheet.Activate();

            worksheet.Unprotect("");
            Ex.Range unProtectedRange = worksheet.get_Range("A2", "K9999");
            unProtectedRange.Locked = false;

            //worksheet.get_Range("A1", "K1").Font.Color=Color.White;
            worksheet.get_Range("A1", "C1").Font.ColorIndex = 0;
            worksheet.get_Range("A1", "C1").Interior.ColorIndex = 20;
            worksheet.get_Range("A1", "C1").Borders.ColorIndex = 0;

            ((Ex.Range)worksheet.Cells[2, "A"]).EntireColumn.Locked = true; //DistributorId
            ((Ex.Range)worksheet.Cells[2, "B"]).EntireColumn.Locked = true; //Amount           
            ((Ex.Range)worksheet.Cells[2, "C"]).EntireColumn.Locked = true; //Remarks 

            xlCells.HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlLeft;
            xlCells[1, 1] = "DistributorID";
            xlCells[1, 2] = "Amount";
            xlCells[1, 3] = "Remarks";

            for (int i = 0; i < dsInvalidDistributor.Tables[0].Rows.Count; i++)
            {
                xlCells[i + 2, 1] = dsInvalidDistributor.Tables[0].Rows[i][0].ToString();
                xlCells[i + 2, 2] = dsInvalidDistributor.Tables[0].Rows[i][1].ToString();
                xlCells[i + 2, 3] = dsInvalidDistributor.Tables[0].Rows[i][2].ToString();           
            }

            //Ex.Range protectedRange = worksheet.get_Range("A2", "B9999");
            //protectedRange.Locked = true;
            //worksheet.Protect("", Type.Missing, Type.Missing, Type.Missing, Type.Missing, false, false, false, false, false, false, false, false, false, false, Type.Missing);
            //---------Invalid distributor            
            worksheet = ((Ex._Worksheet)(sheets[3]));

            
            worksheet.Name = "Invalid Distributor";
            
            range1 = worksheet.Cells;
            xlCells = worksheet.Cells;

            worksheet.Columns.ColumnWidth = 15;
            worksheet.get_Range("A1", "C1").Font.Bold = true;


            worksheet.Activate();

            worksheet.Unprotect("");
            unProtectedRange = worksheet.get_Range("A2", "K9999");
            unProtectedRange.Locked = false;

            //worksheet.get_Range("A1", "K1").Font.Color=Color.White;
            worksheet.get_Range("A1", "C1").Font.ColorIndex = 0;
            worksheet.get_Range("A1", "C1").Interior.ColorIndex = 20;
            worksheet.get_Range("A1", "C1").Borders.ColorIndex = 0;

            ((Ex.Range)worksheet.Cells[2, "A"]).EntireColumn.Locked = true; //DistributorId
            ((Ex.Range)worksheet.Cells[2, "B"]).EntireColumn.Locked = true; //Amount           
            ((Ex.Range)worksheet.Cells[2, "C"]).EntireColumn.Locked = true; //Remarks           

            xlCells.HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlLeft;
            xlCells[1, 1] = "DistributorID";
            xlCells[1, 2] = "Amount";
            xlCells[1, 3] = "Remarks";

            for (int i = 0; i < dsInvalidDistributor.Tables[1].Rows.Count; i++)
            {
                xlCells[i + 2, 1] = dsInvalidDistributor.Tables[1].Rows[i][0].ToString();
                xlCells[i + 2, 2] = dsInvalidDistributor.Tables[1].Rows[i][1].ToString();
                xlCells[i + 2, 3] = dsInvalidDistributor.Tables[1].Rows[i][2].ToString();

            }

            //protectedRange = worksheet.get_Range("A2", "B9999");
            //protectedRange.Locked = true;
            //worksheet.Protect("", Type.Missing, Type.Missing, Type.Missing, Type.Missing, false, false, false, false, false, false, false, false, false, false, Type.Missing);
            //-------save
            string strTemp = System.IO.Path.GetTempPath() + "tDistriTF.xls";
            if (System.IO.File.Exists(strTemp))
            {
                System.IO.File.Delete(strTemp);
            }
            theWorkbook.SaveAs(strTemp, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            theWorkbook.Close(false, Type.Missing, Type.Missing);
            exc.Quit();

            System.IO.File.Delete(txtImportExcel.Text);
            System.IO.File.Move(strTemp, txtImportExcel.Text);
            //-----------------------

            System.Runtime.InteropServices.Marshal.ReleaseComObject(exc);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(theWorkbook);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(sheets);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(range1);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workbooks);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlCells);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
       }

        private void txtDistributorSearch_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtDistributorID.Text))
                {
                    if (!Validators.IsInt32(txtDistributorID.Text))
                    {
                        MessageBox.Show("Only Integer value is allowed in Distributor Id", Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.txtDistributorID.Text = "";
                return;
            }
        }

        private void txtImportExcel_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }       

        private void txtImportExcel_Validating(object sender, CancelEventArgs e)
        {
            System.IO.Directory.SetCurrentDirectory(m_dirPath);
        }            
     
    }
}
