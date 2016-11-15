using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;

//vinculum framework/namespace(s)
using CoreComponent.BusinessObjects;
using CoreComponent.BusinessObjects.PickUpCentre;
using CoreComponent.Core.BusinessObjects;



namespace CoreComponent.UI
{
    public partial class frmPickUpCentre : CoreComponent.Core.UI.Transaction
    {

        #region Variables
        private DataTable dtLocations_PUC = null;
        private bool m_boolSuspendEventHandler = false;
        private int m_userId = -1;
        private string UserName;
        private string CON_MODULENAME = string.Empty;
        private StringBuilder m_sbErrorMessages = null;

        private PUCAccount m_objPUCAccount;
        private List<PUCAccount> m_lstPUCAccounts;

        private UIState m_uisCurrentState = UIState.ResetAcc;

        private Boolean IsSaveAvailable = false;
        private Boolean IsUpdateAvailable = false;
        private Boolean IsSearchAvailable = false;

        private string L = null;
        private string P = null;
        private string DD = null;
        private string PM = null;
        private bool IsDtpchecked;
        private string TransNoString = null;
        private const string DefaultValue = "Select";

        #endregion


        #region Constants

        private const string CON_GRIDCOL_EDIT = "Edit";
        private const string CON_GRIDCOL_REMOVE = "Remove";

        #endregion


        #region Enum

        private enum UIState
        {
            AddAccDep = 1,
            EditAccDep = 2,
            ViewAcc = 3,
            ResetAcc = 4
        }

        #endregion


        #region Constructor

        public frmPickUpCentre()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion


        #region Events

        private void frmPickUpCentre_Load(object sender, EventArgs e)
        {
            CON_MODULENAME = this.Tag.ToString();

            InitializeRights();

            InitializeAndPopulateControls();

            this.lblPageTitle.Text = "Pick-Up-Centre Accounts";
            this.tabSearch.Text = "Search / Create";
            this.tabControlTransaction.TabPages.Remove(tabCreate);

            m_uisCurrentState = UIState.ResetAcc;
            ManageUIState();
            txtTranNo.Text = string.Empty;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                SearchPUCAcc();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearchReset_Click(object sender, EventArgs e)
        {
            try
            {

                m_uisCurrentState = UIState.ResetAcc; //saurabh Garg

                ResetPUCAccDep(); //saurabh Garg
                ManageUIState(); //saurabh Garg
                //if (m_objPUCAccount != null)
                //{
                //    m_uisCurrentState = UIState.AddAccDep;
                //}
                //else
                //{
                //    m_uisCurrentState = UIState.ResetAcc;
                //}
                //ResetPUCAccDep();
                //ManageUIState();
                ////txtTranNo.Text = string.Empty;
                ///*L = txtTranNo.Text;
                //int a = L.IndexOf(" ");
                //L = L.Substring(a + 0);


                //txtTranNo.Text = (L).ToString();
                //*/


            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddPUCAccDep_Click(object sender, EventArgs e)
        {
            try
            {
                AddPUCAccDep();

                m_uisCurrentState = UIState.ResetAcc; //saurabh Garg

                //  ResetPUCAccDep(); //saurabh Garg
                txtDepositAmount.Text = string.Empty;
                cmbPUC.SelectedIndex = 0;
                ManageUIState(); //saurabh Garg


            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("VAL0615"), Common.GetMessage("VAL0615"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvPUCAccounts_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                DataGridView dgvSender = (DataGridView)sender;
                if (!m_boolSuspendEventHandler)
                {
                    if (dgvSender.SelectedRows.Count > 0)
                    {
                        if (dgvSender.SelectedRows[0].Index > Common.INT_DBNULL)
                        {
                            m_uisCurrentState = UIState.ViewAcc;
                            ReflectPUCAcc(Convert.ToInt32(dgvSender.SelectedRows[0].Cells["PCId"].Value.ToString()));
                            ManageUIState();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbLocType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (!m_boolSuspendEventHandler)
                {
                    ComboBox ctrl = (ComboBox)sender;
                    FillLocationControls(PUCCommonTransaction.Locations.LocationCode, (int)Common.LocationConfigId.BO);
                }
                //if (dgvPUCAccDep.RowCount == 0)
                //{

                //    L = cmbLocCode.Text.Trim();
                //    int a = L.IndexOf("-");
                //    L = L.Substring(a + 1);
                //    if (P == DefaultValue && L == DefaultValue && IsDtpchecked == false && PM == DefaultValue)
                //        txtTranNo.Text = null;
                //    else if (IsDtpchecked == false && PM == DefaultValue)
                //        txtTranNo.Text = L + "/";
                //    else if (IsDtpchecked == true)
                //        txtTranNo.Text = L + "/" + P + "/";
                //    else if (PM != DefaultValue)
                //        txtTranNo.Text = (L + "/" + P + "/" + PM + "/").ToString();
                //}
                if (cmbLocCode.Text.Trim() != "" && cmbLocCode.Text.Trim() != DefaultValue && cmbLocCode.Text.Trim() != "System.Data.DataRowView")
                    L = cmbLocCode.Text.Trim();
                else
                    L = null;
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbLocCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (!m_boolSuspendEventHandler)
                {
                    ComboBox ctrl = (ComboBox)sender;
                    FillLocationControls(PUCCommonTransaction.Locations.PUCCode, Convert.ToInt32(ctrl.SelectedValue));
                }
                //if (dgvPUCAccDep.RowCount == 0)
                //{

                ////////////////////////////////////Commented by Saurabh Garg ///////////////////////////////////////////

                //L = cmbLocCode.Text.Trim();
                //int a = L.IndexOf("-");
                //L = L.Substring(a + 1);
                //if (P == DefaultValue && L == DefaultValue && IsDtpchecked == false && PM == DefaultValue)
                //    txtTranNo.Text = null;
                //else if (IsDtpchecked == false && PM == DefaultValue)
                //    txtTranNo.Text = L + "/";
                //else if (IsDtpchecked == true)
                //    txtTranNo.Text = L + "/" + P + "/" + DD + "/";
                //else if (PM != DefaultValue && PM != null)
                //    txtTranNo.Text = (L + "/" + P + "/" + DD + "/" + PM + "/").ToString();

                if (cmbLocCode.Text.Trim() != string.Empty && cmbLocCode.Text.Trim() != null && cmbLocCode.Text.Trim() != "System.Data.DataRowView")
                    L = cmbLocCode.Text.Trim();
                else
                    L = null;
                SetTransactionNo();

                ///////////////////////////////////////////////////////////////////////////////////////////////////////////
                dtpDepositDate.Checked = false;
                if (cmbPaymentMode.Items.Count > 0)
                    cmbPaymentMode.SelectedIndex = 0;
                //}
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearchDBRecords_Click(object sender, EventArgs e)
        {
            try
            {
                SearchPUCAcc();


            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnResetUI_Click(object sender, EventArgs e)
        {
            try
            {
                //DialogResult resetresult = MessageBox.Show(Common.GetMessage("5006"), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //if (resetresult == DialogResult.Yes)
                {
                    ResetUI();
                    m_objPUCAccount = null;
                    m_lstPUCAccounts = null;
                    txtTranNo.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvPUCAccDep_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                DataGridView dgvSender = (DataGridView)sender;
                if (!m_boolSuspendEventHandler)
                {
                    if (dgvSender.SelectedRows.Count > 0)
                    {
                        if (dgvSender.SelectedRows[0].Index > Common.INT_DBNULL)
                        {
                            m_uisCurrentState = UIState.EditAccDep;
                            string slctdTransactionNumber = dgvSender.SelectedRows[0].Cells["TransactionNumber"].Value.ToString();
                            string slctdLocationCode = slctdTransactionNumber.Substring(0, slctdTransactionNumber.IndexOf('/'));
                            ReflectPUCAccDep(slctdLocationCode, Convert.ToInt32(dgvSender.SelectedRows[0].Cells["PCId"].Value.ToString()),
                                             dgvSender.SelectedRows[0].Cells["RecordNo"].Value.ToString(),
                                             Convert.ToInt32(dgvSender.SelectedRows[0].Cells["Type"].Value.ToString()),
                                             dgvSender.SelectedRows[0].Cells["TransactionNumber"].Value.ToString(),
                                             Convert.ToDecimal(dgvSender.SelectedRows[0].Cells["Amount"].Value.ToString()),
                                             Convert.ToDateTime(dgvSender.SelectedRows[0].Cells["Date"].Value.ToString())
                                             );
                            ManageUIState();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSaveDBRecord_Click(object sender, EventArgs e)
        {
            try
            {
                m_uisCurrentState = UIState.ResetAcc; //saurabh Garg

                ResetPUCAccDep(); //saurabh Garg
                //ManageUIState(); //saurabh Garg

                SavePUCAcc();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvPUCAccDep_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridView dgvSender = (DataGridView)sender;
                if ((e.RowIndex > Common.INT_DBNULL) && (e.ColumnIndex > Common.INT_DBNULL))
                {
                    string errMsg = string.Empty;
                    switch (dgvSender.Columns[e.ColumnIndex].Name)
                    {
                        case CON_GRIDCOL_EDIT:
                            {
                                ///TODO: EDIT MODE;
                                ///UNCOMMENT THE FOLLOWING COMMENTED-CODE

                                ////Check for RecordNo of the Deposit-Record
                                ////If the Deposit-Record has not yet been saved in DB, 
                                ////then RecordNo would be either empty or '-1'
                                //string recordNo = m_objPUCAccount.Deposits[e.RowIndex].RecordNo.Trim();
                                //if (string.IsNullOrEmpty(recordNo) || recordNo == "-1")
                                //{
                                //    m_uisCurrentState = UIState.AddAccDep;
                                //    ManageUIState();
                                //}
                                //else
                                //{
                                //    errMsg = Common.GetMessage("VAL0108", "edit", "saved Deposit-Entry");
                                //}
                            }
                            break;

                        case CON_GRIDCOL_REMOVE:
                            {
                                //Check for RecordNo of the Deposit-Record
                                //If the Deposit-Record has not yet been saved in DB, 
                                //then RecordNo would be either empty or '-1'
                                string recordNo = m_objPUCAccount.Deposits[e.RowIndex].RecordNo.Trim();
                                if (string.IsNullOrEmpty(recordNo) || recordNo == "-1")
                                {
                                    DialogResult confirmresult = MessageBox.Show(Common.GetMessage("5012"), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    if (confirmresult == DialogResult.Yes)
                                    {
                                        m_objPUCAccount.Deposits.Remove(m_objPUCAccount.Deposits[e.RowIndex]);
                                        m_boolSuspendEventHandler = true;
                                        dgvSender.DataSource = null;
                                        m_boolSuspendEventHandler = false;
                                        if (m_objPUCAccount.Deposits.Count > 0)
                                        {
                                            m_uisCurrentState = UIState.ViewAcc;
                                            dgvSender.DataSource = m_objPUCAccount.Deposits;
                                            ManageUIState();
                                            btnSaveDBRecord.Enabled = IsSaveAvailable;
                                        }
                                        else
                                        {
                                            m_objPUCAccount = null;
                                            m_uisCurrentState = UIState.ResetAcc;
                                            ResetPUCAccDep();
                                            ManageUIState();
                                            btnSaveDBRecord.Enabled = false;
                                        }
                                    }
                                }
                                else
                                {
                                    errMsg = Common.GetMessage("VAL0108", "remove", "saved Deposit-Entry");
                                }
                            }
                            break;
                    }

                    if (!string.IsNullOrEmpty(errMsg))
                    {
                        MessageBox.Show(errMsg, Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion


        #region Methods

        private void InitializeRights()
        {
            if (AuthenticationComponent.BusinessObjects.Authenticate.LoggedInUser != null)
            {
                m_userId = AuthenticationComponent.BusinessObjects.Authenticate.LoggedInUser.UserId;
                UserName = AuthenticationComponent.BusinessObjects.Authenticate.LoggedInUser.UserName;
            }

            if (UserName != null && !CON_MODULENAME.Equals(string.Empty))
            {
                IsSaveAvailable = AuthenticationComponent.BusinessObjects.Authenticate.IsFunctionAccessible(UserName, Common.LocationCode, CON_MODULENAME, Common.FUNCTIONCODE_SAVE);
                IsUpdateAvailable = AuthenticationComponent.BusinessObjects.Authenticate.IsFunctionAccessible(UserName, Common.LocationCode, CON_MODULENAME, Common.FUNCTIONCODE_UPDATE);
                IsSearchAvailable = AuthenticationComponent.BusinessObjects.Authenticate.IsFunctionAccessible(UserName, Common.LocationCode, CON_MODULENAME, Common.FUNCTIONCODE_SEARCH);
            }
        }

        private void InitializeAndPopulateControls()
        {
            string errMsg = string.Empty;
            PUCCommonTransaction objTemp = new PUCCommonTransaction();
            FillLocationControls(PUCCommonTransaction.Locations.LocationCode, (int)Common.LocationConfigId.BO);

            DataTable dtTemp = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter("PAYMENTMODE", 0, 0, 0));
            //Remove Forex from Mode-of-Payments
            dtTemp.DefaultView.RowFilter = "KeyCode1 <> 3";
            dtTemp.DefaultView.Sort = "KeyCode1";
            cmbPaymentMode.DataSource = null;
            cmbPaymentMode.DataSource = dtTemp.DefaultView.ToTable();
            cmbPaymentMode.DisplayMember = "KeyValue1";
            cmbPaymentMode.ValueMember = "KeyCode1";

            dtpDepositDate.Format = DateTimePickerFormat.Custom;
            dtpDepositDate.CustomFormat = Common.DTP_DATE_FORMAT;
            dtpDepositDate.MaxDate = DateTime.Now.Date;
            dtpDepositDate.Value = DateTime.Now.Date;
            dtpDepositDate.Checked = false;
            txtTranNo.Text = null;

            dgvPUCAccDep.AutoGenerateColumns = false;
            dgvPUCAccDep.Columns.Clear();
            dgvPUCAccDep = Common.GetDataGridViewColumns(dgvPUCAccDep, Environment.CurrentDirectory + "\\App_Data\\GridViewDefinition.xml");

            dgvPUCAccounts.AutoGenerateColumns = false;
            dgvPUCAccounts.Columns.Clear();
            dgvPUCAccounts = Common.GetDataGridViewColumns(dgvPUCAccounts, Environment.CurrentDirectory + "\\App_Data\\GridViewDefinition.xml");
        }

        private void FillLocationControls(PUCCommonTransaction.Locations locType, int locValue)
        {
            string errMsg = string.Empty;
            PUCCommonTransaction objTemp = new PUCCommonTransaction();

            m_boolSuspendEventHandler = true;

            switch (locType)
            {
                case PUCCommonTransaction.Locations.LocationCode:
                    {
                        int locTypeId = Common.INT_DBNULL;
                        int locCodeId = Common.INT_DBNULL;

                        locTypeId = locValue;

                        cmbLocCode.DataSource = null;
                        cmbPUC.DataSource = null;

                        DataTable dtLocations = Common.ParameterLookup(Common.ParameterType.WhgBoPucLocations, new ParameterFilter(string.Empty, Convert.ToInt32(locTypeId), Convert.ToInt32(locCodeId), 0));
                        cmbLocCode.DataSource = dtLocations;
                        cmbLocCode.DisplayMember = "LOCCODE";
                        cmbLocCode.ValueMember = "LOCVAL";

                        dtLocations_PUC = Common.ParameterLookup(Common.ParameterType.WhgBoPucLocations, new ParameterFilter(string.Empty, Common.INT_DBNULL, (int)Common.LocationConfigId.PC, 0));
                        cmbPUC.DataSource = dtLocations_PUC;

                        cmbPUC.DisplayMember = "PUCCODE";
                        cmbPUC.ValueMember = "LOCVAL";

                        //AutoCompleteStringCollection _batchcollection = new AutoCompleteStringCollection();

                        //if (dtLocations_PUC != null)
                        //    {
                        //        for (int j = 0; j < dtLocations_PUC.Rows.Count; j++)
                        //        {
                        //            _batchcollection.Add(dtLocations_PUC.Rows[j][0].ToString());
                        //        }
                        //    }
                        //cmbPUC.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        //cmbPUC.AutoCompleteSource = AutoCompleteSource.CustomSource;
                        //cmbPUC.AutoCompleteCustomSource = _batchcollection;

                    }
                    break;

                case PUCCommonTransaction.Locations.PUCCode:
                    {
                        int locTypeId = Common.INT_DBNULL;
                        int locCodeId = Common.INT_DBNULL;

                        locTypeId = Convert.ToInt32(cmbLocType.SelectedValue);
                        locCodeId = locValue;

                        cmbPUC.DataSource = null;

                        dtLocations_PUC = Common.ParameterLookup(Common.ParameterType.WhgBoPucLocations, new ParameterFilter(string.Empty, (int)Common.LocationConfigId.PC, Convert.ToInt32(locCodeId), 0));
                        cmbPUC.DataSource = dtLocations_PUC;
                        cmbPUC.DisplayMember = "PUCCODE";
                        cmbPUC.ValueMember = "LOCVAL";
                    }
                    break;
            }

            m_boolSuspendEventHandler = false;
        }

        private void SearchPUCAcc()
        {
            if (ValidateSearchCriteria())
            {
                string errorMessage = "";

                int locCode = Common.INT_DBNULL;
                int pcId = Common.INT_DBNULL;
                string transNo = txtTranNo.Text.Trim();
                string date;
                if (dtpDepositDate.Checked == true)
                    date = dtpDepositDate.Value.ToString();
                else
                    date = Common.DATETIME_NULL.ToString();
                locCode = (int)cmbLocCode.SelectedValue;
                if ((cmbPUC.SelectedValue != null) && (Convert.ToInt32(cmbPUC.SelectedValue) != Common.INT_DBNULL))
                {
                    pcId = (int)cmbPUC.SelectedValue;
                }

                PUCCommonTransaction objPUCInfo = new PUCCommonTransaction();
                DataTable dtPUCInfo = objPUCInfo.FetchPUCInfo(locCode, pcId, Convert.ToInt32(cmbPaymentMode.SelectedValue), transNo, date, 0, 0, ref errorMessage);

                if (string.IsNullOrEmpty(errorMessage))
                {
                    if (dtPUCInfo != null)
                    {
                        if (dtPUCInfo.Rows.Count > 0)
                        {
                            if (m_objPUCAccount == null)
                            {
                                m_objPUCAccount = new PUCAccount();
                            }

                            m_lstPUCAccounts = new List<PUCAccount>();

                            DataTable dtTemp = dtPUCInfo.Copy();

                            dtPUCInfo.DefaultView.Sort = "PCId,DepositMode";
                            dtPUCInfo = dtPUCInfo.DefaultView.ToTable();
                            DataTable dtPUCIds = dtPUCInfo.DefaultView.ToTable(true, "LOCCODE", "LOCCODEID", "PCId", "PUCLocation", "AvailableAmount", "MODIFIEDDATE", "UsedAmount");

                            DataColumn dcColToAdd = new DataColumn("GridRowNo", typeof(int));
                            dtPUCIds.Columns.Add(dcColToAdd);
                            dtPUCIds.AcceptChanges();

                            int tempRowNo = 0;
                            foreach (DataRow dtRow in dtPUCIds.Rows)
                            {
                                dtRow["GridRowNo"] = tempRowNo++;
                            }

                            foreach (DataRow drPCId in dtPUCIds.Rows)
                            {
                                m_objPUCAccount = new PUCAccount();
                                m_objPUCAccount.LocationCodeId = Convert.ToInt32(drPCId["LOCCODEID"]);
                                m_objPUCAccount.LocationCode = drPCId["LOCCODE"].ToString();
                                //m_objPUCAccount.PUCLocId = Convert.ToInt32(drPCId["PUCLOCID"]);
                                m_objPUCAccount.PCId = Convert.ToInt32(drPCId["PCId"]);
                                m_objPUCAccount.PCLocation = drPCId["PUCLOCATION"].ToString();
                                m_objPUCAccount.ModifiedDate = Convert.ToDateTime(drPCId["MODIFIEDDATE"].ToString());
                                m_objPUCAccount.UsedAmount = Math.Round(Convert.ToDecimal(drPCId["UsedAmount"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);

                                dtPUCInfo.DefaultView.RowFilter = string.Empty;
                                dtPUCInfo.DefaultView.RowFilter = "PCId = " + m_objPUCAccount.PCId;
                                DataTable dtPUCInfo_Temp = dtPUCInfo.DefaultView.ToTable();
                                m_objPUCAccount.Deposits = new List<PUCDeposit>();
                                foreach (DataRow drPUCInfo in dtPUCInfo_Temp.Rows)
                                {
                                    PUCDeposit pucaccdeposit = new PUCDeposit();
                                    pucaccdeposit.Amount = Convert.ToDecimal(drPUCInfo["DEPOSITAMOUNT"]);
                                    pucaccdeposit.Date = Convert.ToDateTime(drPUCInfo["DEPOSITDATE"]);
                                    pucaccdeposit.RecordNo = drPUCInfo["RECORDNO"].ToString();
                                    pucaccdeposit.TransactionNo = drPUCInfo["TRANSACTIONNO"].ToString();
                                    pucaccdeposit.PaymentModeId = Convert.ToInt32(drPUCInfo["DEPOSITMODE"]);
                                    pucaccdeposit.PaymentModeType = drPUCInfo["DEPOSITTYPE"].ToString();
                                    pucaccdeposit.PCId = Convert.ToInt32(drPUCInfo["PCId"].ToString());

                                    m_objPUCAccount.Deposits.Add(pucaccdeposit);
                                }

                                m_lstPUCAccounts.Add(m_objPUCAccount);
                            }

                            dtPUCInfo.DefaultView.RowFilter = string.Empty;
                            dtPUCInfo.DefaultView.Sort = string.Empty;
                            dtPUCInfo = dtTemp.Copy();

                            dtTemp.Dispose();
                            dtTemp = null;

                            System.Globalization.NumberFormatInfo nfi = new System.Globalization.NumberFormatInfo();
                            nfi.PercentDecimalDigits = Common.DisplayAmountRounding;
                            string strRoundingZeroesFormat = Common.GetRoundingZeroes(Common.DisplayAmountRounding); //"0.00";
                            foreach (DataRow drRow in dtPUCInfo.Rows)
                            {
                                drRow["AVAILABLEAMOUNT"] = Convert.ToDecimal(drRow["AVAILABLEAMOUNT"]).ToString(strRoundingZeroesFormat, nfi);
                                drRow["DEPOSITAMOUNT"] = Convert.ToDecimal(drRow["DEPOSITAMOUNT"]).ToString(strRoundingZeroesFormat, nfi);
                            }
                            dtPUCInfo.AcceptChanges();

                            m_boolSuspendEventHandler = true;
                            dgvPUCAccounts.DataSource = m_lstPUCAccounts;//dtPUCInfo;
                            m_boolSuspendEventHandler = false;
                            if (dgvPUCAccounts.Rows.Count > 0)
                            {
                                m_uisCurrentState = UIState.ViewAcc;
                                ReflectPUCAcc(Convert.ToInt32(dgvPUCAccounts.Rows[0].Cells["PCId"].Value.ToString()));
                                ManageUIState();
                            }
                        }
                        else
                        {
                            m_objPUCAccount = null;
                            MessageBox.Show(Common.GetMessage("8002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        m_objPUCAccount = null;
                        MessageBox.Show(Common.GetMessage("8002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show(errorMessage, Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show(m_sbErrorMessages.ToString(), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FetchPUCAcc()
        {

            string errorMessage = "";

            int locCode = Common.INT_DBNULL;
            int pcId = Common.INT_DBNULL;
            string transNo = txtTranNo.Text.Trim();
            string date;

            date = dtpDepositDate.Value.ToString();


            PUCCommonTransaction objPUCInfo = new PUCCommonTransaction();
            //DataTable dtPUCInfo = objPUCInfo.FetchPUCInfo(locCode, pcId, Convert.ToInt32(cmbPaymentMode.SelectedValue), transNo, date, 2, 0, ref errorMessage);

            //m_lstPUCAccounts

            //DataTable dtPUCInfo = objPUCInfo.FetchPUCInfo(locCode, pcId, Convert.ToInt32(cmbPaymentMode.SelectedValue), transNo, date, 0, 0, ref errorMessage);
            DataTable dtPUCInfo = new DataTable();
            DataTable dt;
            foreach (PUCDeposit item in m_lstPUCAccounts[0].Deposits)
            {
                dt = new DataTable();
                locCode = item.LocationCodeId;
                pcId = item.PCId;
                dt = objPUCInfo.FetchPUCInfo(locCode, pcId, Convert.ToInt32(cmbPaymentMode.SelectedValue), transNo, date, 0, 0, ref errorMessage);
                foreach (DataRow dr in dt.Rows)
                {
                    if (dtPUCInfo.Rows.Count == 0)
                    {
                        dtPUCInfo = dt.Copy();
                    }
                    else
                    {
                        DataRow r = dtPUCInfo.NewRow();
                        r["LOCCODEID"] = dr["LOCCODEID"];//LOCCODEID
                        r["LOCCODE"] = dr["LOCCODE"];
                        r["PUCLOCATION"] = dr["PUCLOCATION"];
                        r["PCID"] = dr["PCID"];
                        r["RECORDNO"] = dr["RECORDNO"];
                        r["AVAILABLEAMOUNT"] = dr["AVAILABLEAMOUNT"];
                        r["DEPOSITAMOUNT"] = dr["DEPOSITAMOUNT"];
                        r["DEPOSITDATE"] = dr["DEPOSITDATE"];
                        r["DEPOSITMODE"] = dr["DEPOSITMODE"];
                        r["DEPOSITTYPE"] = dr["DEPOSITTYPE"];
                        r["TRANSACTIONNO"] = dr["TRANSACTIONNO"];
                        r["MODIFIEDDATE"] = dr["MODIFIEDDATE"];
                        r["UsedAmount"] = dr["UsedAmount"];
                        dtPUCInfo.Rows.Add(r);
                    }

                }
            }

            if (string.IsNullOrEmpty(errorMessage))
            {
                if (dtPUCInfo != null)
                {
                    if (dtPUCInfo.Rows.Count > 0)
                    {
                        if (m_objPUCAccount == null)
                        {
                            m_objPUCAccount = new PUCAccount();
                        }

                        m_lstPUCAccounts = new List<PUCAccount>();

                        DataTable dtTemp = dtPUCInfo.Copy();

                        dtPUCInfo.DefaultView.Sort = "PCId,DepositMode";
                        dtPUCInfo = dtPUCInfo.DefaultView.ToTable();
                        DataTable dtPUCIds = dtPUCInfo.DefaultView.ToTable(true, "LOCCODE", "LOCCODEID", "PCId", "PUCLocation", "AvailableAmount", "MODIFIEDDATE", "UsedAmount");

                        DataColumn dcColToAdd = new DataColumn("GridRowNo", typeof(int));
                        dtPUCIds.Columns.Add(dcColToAdd);
                        dtPUCIds.AcceptChanges();

                        int tempRowNo = 0;
                        foreach (DataRow dtRow in dtPUCIds.Rows)
                        {
                            dtRow["GridRowNo"] = tempRowNo++;
                        }

                        foreach (DataRow drPCId in dtPUCIds.Rows)
                        {
                            m_objPUCAccount = new PUCAccount();
                            m_objPUCAccount.LocationCodeId = Convert.ToInt32(drPCId["LOCCODEID"]);
                            m_objPUCAccount.LocationCode = drPCId["LOCCODE"].ToString();
                            //m_objPUCAccount.PUCLocId = Convert.ToInt32(drPCId["PUCLOCID"]);
                            m_objPUCAccount.PCId = Convert.ToInt32(drPCId["PCId"]);
                            m_objPUCAccount.PCLocation = drPCId["PUCLOCATION"].ToString();
                            m_objPUCAccount.ModifiedDate = Convert.ToDateTime(drPCId["MODIFIEDDATE"].ToString());
                            m_objPUCAccount.UsedAmount = Math.Round(Convert.ToDecimal(drPCId["UsedAmount"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);

                            dtPUCInfo.DefaultView.RowFilter = string.Empty;
                            dtPUCInfo.DefaultView.RowFilter = "PCId = " + m_objPUCAccount.PCId;
                            DataTable dtPUCInfo_Temp = dtPUCInfo.DefaultView.ToTable();
                            m_objPUCAccount.Deposits = new List<PUCDeposit>();
                            foreach (DataRow drPUCInfo in dtPUCInfo_Temp.Rows)
                            {
                                PUCDeposit pucaccdeposit = new PUCDeposit();
                                pucaccdeposit.Amount = Convert.ToDecimal(drPUCInfo["DEPOSITAMOUNT"]);
                                pucaccdeposit.Date = Convert.ToDateTime(drPUCInfo["DEPOSITDATE"]);
                                pucaccdeposit.RecordNo = drPUCInfo["RECORDNO"].ToString();
                                pucaccdeposit.TransactionNo = drPUCInfo["TRANSACTIONNO"].ToString();
                                pucaccdeposit.PaymentModeId = Convert.ToInt32(drPUCInfo["DEPOSITMODE"]);
                                pucaccdeposit.PaymentModeType = drPUCInfo["DEPOSITTYPE"].ToString();

                                m_objPUCAccount.Deposits.Add(pucaccdeposit);
                            }

                            m_lstPUCAccounts.Add(m_objPUCAccount);
                        }

                        dtPUCInfo.DefaultView.RowFilter = string.Empty;
                        dtPUCInfo.DefaultView.Sort = string.Empty;
                        dtPUCInfo = dtTemp.Copy();

                        dtTemp.Dispose();
                        dtTemp = null;

                        System.Globalization.NumberFormatInfo nfi = new System.Globalization.NumberFormatInfo();
                        nfi.PercentDecimalDigits = Common.DisplayAmountRounding;
                        string strRoundingZeroesFormat = Common.GetRoundingZeroes(Common.DisplayAmountRounding); //"0.00";
                        foreach (DataRow drRow in dtPUCInfo.Rows)
                        {
                            drRow["AVAILABLEAMOUNT"] = Convert.ToDecimal(drRow["AVAILABLEAMOUNT"]).ToString(strRoundingZeroesFormat, nfi);
                            drRow["DEPOSITAMOUNT"] = Convert.ToDecimal(drRow["DEPOSITAMOUNT"]).ToString(strRoundingZeroesFormat, nfi);
                        }
                        dtPUCInfo.AcceptChanges();

                        m_boolSuspendEventHandler = true;
                        dgvPUCAccounts.DataSource = m_lstPUCAccounts;//dtPUCInfo;
                        m_boolSuspendEventHandler = false;
                        if (dgvPUCAccounts.Rows.Count > 0)
                        {
                            m_uisCurrentState = UIState.ViewAcc;
                            ReflectPUCAcc(Convert.ToInt32(dgvPUCAccounts.Rows[0].Cells["PCId"].Value.ToString()));
                            ManageUIState();
                        }
                    }
                    else
                    {
                        m_objPUCAccount = null;
                        MessageBox.Show(Common.GetMessage("8002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    m_objPUCAccount = null;
                    MessageBox.Show(Common.GetMessage("8002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show(errorMessage, Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private bool ValidateSearchCriteria()
        {
            bool isValid = true;
            string err = string.Empty;
            errorprovPUCValid.Clear();
            m_sbErrorMessages = new StringBuilder();

            //Check if atleast Location-Code has been selected
            if (Convert.ToInt32(cmbLocCode.SelectedValue) == Common.INT_DBNULL)
            {
                isValid = false;
                err = Common.GetMessage("VAL0002", lblLocCode.Text.Replace(":", "").Replace("*", ""));
                errorprovPUCValid.SetError(cmbLocCode, err);
                m_sbErrorMessages.AppendLine(err);
            }
            else
            {
                errorprovPUCValid.SetError(cmbLocCode, string.Empty);
            }

            return isValid;
        }

        private void ManageUIState()
        {
            switch (m_uisCurrentState)
            {
                case UIState.AddAccDep:
                    {
                        if (m_objPUCAccount != null)
                        {
                            cmbLocCode.Enabled = false;
                            cmbPUC.Enabled = false;
                        }
                        else
                        {
                            cmbLocCode.Enabled = true;
                            cmbPUC.Enabled = true;
                        }
                        btnAddPUCAccDep.Enabled = true;
                        btnSearchReset.Enabled = true;
                    }
                    break;

                case UIState.EditAccDep:
                    {
                        //btnAddPUCAccDep.Enabled = false;
                        btnSearchReset.Enabled = true;
                    }
                    break;

                case UIState.ViewAcc:
                    {
                        btnAddPUCAccDep.Enabled = false;
                        btnSearchReset.Enabled = true;
                        btnSearchDBRecords.Enabled = false;
                        btnSaveDBRecord.Enabled = false;
                        btnResetUI.Enabled = true;
                    }
                    break;

                case UIState.ResetAcc:
                    {
                        cmbLocCode.Enabled = true;
                        cmbPUC.Enabled = true;
                        btnAddPUCAccDep.Enabled = true;
                        btnSearchReset.Enabled = true;
                        btnSearchDBRecords.Enabled = IsSearchAvailable; //true;
                        if (dgvPUCAccDep.RowCount == 0)
                            btnSaveDBRecord.Enabled = false; //IsSaveAvailable; //true;
                        else
                            btnSaveDBRecord.Enabled = true;
                        btnResetUI.Enabled = true;
                    }
                    break;
            }

            if ((dgvPUCAccDep.RowCount > 0) && (btnSaveDBRecord.Enabled))
            {
                cmbLocCode.Enabled = false;
                dtpDepositDate.Enabled = false;
                cmbPaymentMode.Enabled = false;
            }

        }

        private void ReflectPUCAcc(int pcId)
        {
            m_objPUCAccount = InitializePUCObject(pcId);
            m_boolSuspendEventHandler = true;
            dgvPUCAccDep.DataSource = null;
            m_boolSuspendEventHandler = false;
            dgvPUCAccDep.DataSource = m_objPUCAccount.Deposits;
        }

        private void ReflectPUCAccDep(string slctdLocationCode, int pcId, string recordNo, int paymentModeId, string tranNo, decimal depositAmount, DateTime depositDate)
        {
            if (m_objPUCAccount != null)
            {
                // ManagePUCAccControls(false); changed by saurabh Garg
                ManagePUCAccControls(true); //changed by saurabh Garg
                DataTable dtLocations = Common.ParameterLookup(Common.ParameterType.WhgBoPucLocations, new ParameterFilter(string.Empty, (int)Common.LocationConfigId.BO, Convert.ToInt32("-1"), 0));
                //dtLocations.Rows
                string locationID = "0";
                foreach (DataRow rw in dtLocations.Rows)
                {
                    if (rw["LocationCode"].ToString() == slctdLocationCode)
                    {
                        locationID = rw["LOCVAL"].ToString();
                        break;
                    }
                }
                cmbLocCode.SelectedValue = locationID;//m_objPUCAccount.LocationCodeId;

                cmbPUC.DataSource = null;

                dtLocations_PUC = Common.ParameterLookup(Common.ParameterType.WhgBoPucLocations, new ParameterFilter(string.Empty, (int)Common.LocationConfigId.PC, Convert.ToInt32(locationID), 0));
                cmbPUC.DataSource = dtLocations_PUC;
                cmbPUC.DisplayMember = "PUCCODE";
                cmbPUC.ValueMember = "LOCVAL";

                cmbPUC.SelectedValue = pcId;//m_objPUCAccount.PCId; //m_objPUCAccount.PUCLocId;
                foreach (PUCDeposit pdep in m_objPUCAccount.Deposits)
                {
                    //If its a new record, then check all details
                    //else just check record-number
                    if (recordNo != Common.INT_DBNULL.ToString())
                    {
                        if (pdep.RecordNo == recordNo)
                        {
                            txtTranNo.Text = pdep.TransactionNo;
                            dtpDepositDate.Value = pdep.Date;
                            txtDepositAmount.Text = pdep.DisplayAmount;
                            cmbPaymentMode.SelectedValue = Convert.ToInt32(pdep.PaymentModeId);
                            break;
                        }
                    }
                    else
                    {
                        if ((pdep.RecordNo == recordNo) &&
                            (pdep.PaymentModeId == paymentModeId) &&
                            (pdep.TransactionNo == tranNo) &&
                            (pdep.Amount == depositAmount) &&
                            (pdep.Date == depositDate))
                        {

                            txtTranNo.Text = pdep.TransactionNo;
                            dtpDepositDate.Value = pdep.Date;
                            txtDepositAmount.Text = pdep.DisplayAmount;
                            cmbPaymentMode.SelectedValue = Convert.ToInt32(pdep.PaymentModeId);
                            break;
                        }
                    }
                }
            }
        }

        private PUCAccount InitializePUCObject(DataGridView dgvSender)
        {
            PUCAccount objPUCinfo = null;

            if (dgvSender.SelectedCells.Count > 0)
            {
                int pcId = Convert.ToInt32(dgvSender.SelectedRows[0].Cells["PCId"].Value);
                string recordNo = dgvSender.SelectedRows[0].Cells["RecordNo"].Value.ToString();

                foreach (PUCAccount pacc in m_lstPUCAccounts)
                {
                    if (pacc.PCId == pcId)
                    {
                        objPUCinfo = new PUCAccount();
                        objPUCinfo.LocationCodeId = pacc.LocationCodeId;
                        objPUCinfo.LocationCode = pacc.LocationCode;
                        //objPUCinfo.PUCLocId = pacc.PUCLocId;
                        objPUCinfo.PCLocation = pacc.PCLocation;
                        objPUCinfo.PCId = pacc.PCId;

                        foreach (PUCDeposit pdep in pacc.Deposits)
                        {
                            if (pdep.RecordNo == recordNo)
                            {
                                objPUCinfo.Deposits = new List<PUCDeposit>();
                                objPUCinfo.Deposits.Add(pdep);
                                break;
                            }
                        }
                        break;
                    }
                }

                if (objPUCinfo != null)
                {
                    if (objPUCinfo.Deposits == null)
                    {
                        objPUCinfo = null;
                    }
                }
            }

            return objPUCinfo;
        }

        private PUCAccount InitializePUCObject(int pcId)
        {
            PUCAccount objPUCinfo = null;
            foreach (PUCAccount pacc in m_lstPUCAccounts)
            {
                if (pacc.PCId == pcId)
                {
                    objPUCinfo = new PUCAccount();
                    objPUCinfo.LocationCodeId = pacc.LocationCodeId;
                    //objPUCinfo.PUCLocId = pacc.PUCLocId == pacc.LocationCodeId ? Common.INT_DBNULL : pacc.PUCLocId;
                    objPUCinfo.PCLocation = pacc.PCLocation;
                    objPUCinfo.PCId = pacc.PCId;
                    objPUCinfo.ModifiedDate = pacc.ModifiedDate;

                    objPUCinfo.Deposits = new List<PUCDeposit>();
                    foreach (PUCDeposit pdep in pacc.Deposits)
                    {
                        objPUCinfo.Deposits.Add(pdep);
                    }
                    break;
                }
            }
            var query = from a in objPUCinfo.Deposits orderby a.Date select a;
            objPUCinfo.Deposits = (List<PUCDeposit>)query.ToList();
            return objPUCinfo;
        }

        private void ManagePUCAccControls(bool isEnabled)
        {
            cmbLocCode.Enabled = isEnabled;
            cmbPUC.Enabled = isEnabled;

            txtTranNo.Enabled = isEnabled;
            dtpDepositDate.Enabled = isEnabled;
            txtDepositAmount.Enabled = isEnabled;
            cmbPaymentMode.Enabled = isEnabled;
        }

        private void SavePUCAcc()
        {
            if (ValidatePUCAcc())
            {
                DialogResult saveResult = MessageBox.Show(Common.GetMessage("INF0027"), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (saveResult == DialogResult.Yes)
                {
                    string errMsg = string.Empty;
                    if (m_lstPUCAccounts == null)
                    {
                        m_lstPUCAccounts = new List<PUCAccount>();
                        m_lstPUCAccounts.Add(m_objPUCAccount);
                    }
                    else
                    {
                        foreach (PUCAccount pacc in m_lstPUCAccounts)
                        {
                            if (pacc.PCId == m_objPUCAccount.PCId)
                            {
                                pacc.Deposits = m_objPUCAccount.Deposits;
                                break;
                            }
                        }
                    }
                    List<PUCAccount> fnlLst = new List<PUCAccount>();
                    PUCAccount obj = null;
                    foreach (PUCDeposit pd in m_objPUCAccount.Deposits)
                    {
                        obj = new PUCAccount();
                        obj.LocationCodeId = pd.LocationCodeId;
                        obj.PCId = pd.PCId;
                        obj.CurrentLocationId = 1;
                        //obj.AvailAmt = pd.Amount;
                        //obj.DBAvailAmt = pd.DBAmount;
                        //obj.DisplayAvailAmt = pd.DisplayAmount;
                        obj.UsedAmount = 0;
                        obj.CreatedBy = pd.CreatedBy;
                        obj.CreatedDate = m_objPUCAccount.CreatedDate;
                        obj.ModifiedBy = pd.ModifiedBy;
                        obj.ModifiedDate = m_objPUCAccount.ModifiedDate;
                        obj.Deposits = new List<PUCDeposit>();
                        obj.Deposits.Add(pd);
                        fnlLst.Add(obj);

                    }
                    PUCCommonTransaction objTransaction = new PUCCommonTransaction();
                    List<PUCAccount> savedPUCAccounts = new List<PUCAccount>();
                    foreach (PUCAccount pc in fnlLst)
                    {

                        if (objTransaction.SavePUCInfo(pc, ref errMsg))//m_objPUCAccount
                        {

                            MessageBox.Show(Common.GetMessage("8001") + " For TransactionNo." + pc.Deposits[0].TransactionNo, Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //m_uisCurrentState = UIState.ViewAcc;
                            //ResetPUCAccDep();
                            ////FetchPUCAcc(); This is made by sgarg and commented by sgarg
                            //SearchPUCAcc();
                            //ReflectPUCAcc(pc.PCId);
                            //ManageUIState();
                            savedPUCAccounts.Add(pc);
                            btnSaveDBRecord.Enabled = false;

                        }
                        else
                        {


                            if (errMsg.StartsWith("VAL0108"))
                            {
                                MessageBox.Show(Common.GetMessage("VAL0108", "saved", " already one exists for this Transaction No"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Common.LogException(new Exception(errMsg));
                            }
                            else if (errMsg.StartsWith("INF0144"))//INF0144
                            {
                                string transNo = pc.Deposits[0].TransactionNo;
                                MessageBox.Show(Common.GetMessage("VAL0108", "saved", "exists for this Transaction No " + transNo), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Common.LogException(new Exception(errMsg));
                            }
                            else if (errMsg.StartsWith("30001"))
                            {
                                MessageBox.Show(Common.GetMessage("30001"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Common.LogException(new Exception(errMsg));
                            }
                            else
                            {
                                MessageBox.Show(Common.GetMessage(errMsg), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                            if (m_lstPUCAccounts != null)
                            {
                                if (m_lstPUCAccounts.Count == 1)
                                {
                                    if (m_lstPUCAccounts[0].PCId == Common.INT_DBNULL)
                                    {
                                        m_lstPUCAccounts = null;
                                    }
                                }
                            }
                        }

                    }

                    SearchSavedPUCAcc(savedPUCAccounts);

                }
            }
        }

        private void SearchSavedPUCAcc(List<PUCAccount> savedPUCAccount)
        {

            string errorMessage = "";
            m_lstPUCAccounts = new List<PUCAccount>();
            foreach (PUCAccount spa in savedPUCAccount)
            {
                int locCode = Common.INT_DBNULL;
                int pcId = Common.INT_DBNULL;
                string transNo = spa.Deposits[0].TransactionNo;
                string date = spa.Deposits[0].Date.ToString();
                int paymentMode = spa.Deposits[0].PaymentModeId;
                locCode = spa.Deposits[0].LocationCodeId;
                pcId = spa.Deposits[0].PCId;
                PUCCommonTransaction objPUCInfo = new PUCCommonTransaction();
                DataTable dtPUCInfo = objPUCInfo.FetchPUCInfo(locCode, pcId, paymentMode, transNo, date, 0, 0, ref errorMessage);
                if (string.IsNullOrEmpty(errorMessage))
                {
                    if (dtPUCInfo != null)
                    {
                        if (dtPUCInfo.Rows.Count > 0)
                        {
                            DataTable dtTemp = dtPUCInfo.Copy();

                            dtPUCInfo.DefaultView.Sort = "PCId,DepositMode";
                            dtPUCInfo = dtPUCInfo.DefaultView.ToTable();
                            DataTable dtPUCIds = dtPUCInfo.DefaultView.ToTable(true, "LOCCODE", "LOCCODEID", "PCId", "PUCLocation", "AvailableAmount", "MODIFIEDDATE", "UsedAmount");

                            DataColumn dcColToAdd = new DataColumn("GridRowNo", typeof(int));
                            dtPUCIds.Columns.Add(dcColToAdd);
                            dtPUCIds.AcceptChanges();

                            int tempRowNo = 0;
                            foreach (DataRow dtRow in dtPUCIds.Rows)
                            {
                                dtRow["GridRowNo"] = tempRowNo++;
                            }


                            foreach (DataRow drPCId in dtPUCIds.Rows)
                            {
                                m_objPUCAccount = new PUCAccount();
                                m_objPUCAccount.LocationCodeId = Convert.ToInt32(drPCId["LOCCODEID"]);
                                m_objPUCAccount.LocationCode = drPCId["LOCCODE"].ToString();
                                //m_objPUCAccount.PUCLocId = Convert.ToInt32(drPCId["PUCLOCID"]);
                                m_objPUCAccount.PCId = Convert.ToInt32(drPCId["PCId"]);
                                m_objPUCAccount.PCLocation = drPCId["PUCLOCATION"].ToString();
                                m_objPUCAccount.ModifiedDate = Convert.ToDateTime(drPCId["MODIFIEDDATE"].ToString());
                                m_objPUCAccount.UsedAmount = Math.Round(Convert.ToDecimal(drPCId["UsedAmount"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);

                                dtPUCInfo.DefaultView.RowFilter = string.Empty;
                                dtPUCInfo.DefaultView.RowFilter = "PCId = " + m_objPUCAccount.PCId;
                                DataTable dtPUCInfo_Temp = dtPUCInfo.DefaultView.ToTable();
                                m_objPUCAccount.Deposits = new List<PUCDeposit>();
                                foreach (DataRow drPUCInfo in dtPUCInfo_Temp.Rows)
                                {
                                    PUCDeposit pucaccdeposit = new PUCDeposit();
                                    pucaccdeposit.Amount = Convert.ToDecimal(drPUCInfo["DEPOSITAMOUNT"]);
                                    pucaccdeposit.Date = Convert.ToDateTime(drPUCInfo["DEPOSITDATE"]);
                                    pucaccdeposit.RecordNo = drPUCInfo["RECORDNO"].ToString();
                                    pucaccdeposit.TransactionNo = drPUCInfo["TRANSACTIONNO"].ToString();
                                    pucaccdeposit.PaymentModeId = Convert.ToInt32(drPUCInfo["DEPOSITMODE"]);
                                    pucaccdeposit.PaymentModeType = drPUCInfo["DEPOSITTYPE"].ToString();
                                    pucaccdeposit.PCId = Convert.ToInt32(drPUCInfo["PCId"].ToString());

                                    m_objPUCAccount.Deposits.Add(pucaccdeposit);
                                }

                                m_lstPUCAccounts.Add(m_objPUCAccount);
                            }

                        }
                        else
                        {
                            m_objPUCAccount = null;
                            MessageBox.Show(Common.GetMessage("8002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        m_objPUCAccount = null;
                        MessageBox.Show(Common.GetMessage("8002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                else
                {
                    MessageBox.Show(errorMessage, Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            dgvPUCAccounts.DataSource = m_lstPUCAccounts;

            List<PUCDeposit> Pdep = new List<PUCDeposit>();
            foreach (PUCAccount pucAcc in m_lstPUCAccounts)
            {
                Pdep.Add(pucAcc.Deposits[0]);
            }
            dgvPUCAccDep.DataSource = Pdep;
        }

        private void AddPUCAccDep()
        {
            if (ValidatePUCAccDep())
            {
                if (m_objPUCAccount == null)
                {
                    m_objPUCAccount = new PUCAccount();

                    m_objPUCAccount.LocationCodeId = Convert.ToInt32(cmbLocCode.SelectedValue);
                    //m_objPUCAccount.PUCLocId = (int)cmbPUC.SelectedValue;
                    m_objPUCAccount.PCId = (int)cmbPUC.SelectedValue;
                    m_objPUCAccount.CurrentLocationId = Common.CurrentLocationId;
                    m_objPUCAccount.CreatedBy = m_userId;
                    m_objPUCAccount.CreatedDate = Convert.ToDateTime(Common.DATETIME_NULL);
                    m_objPUCAccount.ModifiedBy = m_userId;
                    m_objPUCAccount.ModifiedDate = Convert.ToDateTime(Common.DATETIME_NULL);

                    PUCDeposit objPUCdeposit = new PUCDeposit();
                    objPUCdeposit.LocationCodeId = Convert.ToInt32(cmbLocCode.SelectedValue);
                    objPUCdeposit.PCId = (int)cmbPUC.SelectedValue;
                    objPUCdeposit.RecordNo = "-1";
                    objPUCdeposit.TransactionNo = txtTranNo.Text.Trim();
                    objPUCdeposit.Date = Convert.ToDateTime(dtpDepositDate.Value.ToString(Common.DATE_TIME_FORMAT));
                    objPUCdeposit.Amount = Convert.ToDecimal(txtDepositAmount.Text.Trim());
                    objPUCdeposit.PaymentModeId = Convert.ToInt32(cmbPaymentMode.SelectedValue);
                    objPUCdeposit.PaymentModeType = ((DataRowView)cmbPaymentMode.SelectedItem)["keyvalue1"].ToString();
                    objPUCdeposit.CreatedBy = m_userId;
                    objPUCdeposit.ModifiedBy = m_userId;

                    m_objPUCAccount.Deposits = new List<PUCDeposit>();
                    m_objPUCAccount.Deposits.Add(objPUCdeposit);
                }
                else
                {
                    m_objPUCAccount.ModifiedBy = m_userId;
                    m_objPUCAccount.ModifiedDate = Convert.ToDateTime(m_objPUCAccount.ModifiedDate.ToString(Common.DATE_TIME_FORMAT));

                    PUCDeposit objPUCdeposit = new PUCDeposit();
                    objPUCdeposit.LocationCodeId = Convert.ToInt32(cmbLocCode.SelectedValue);
                    objPUCdeposit.PCId = (int)cmbPUC.SelectedValue;
                    objPUCdeposit.RecordNo = "-1";
                    objPUCdeposit.TransactionNo = txtTranNo.Text.Trim();
                    objPUCdeposit.Date = Convert.ToDateTime(dtpDepositDate.Value.ToString(Common.DATE_TIME_FORMAT));
                    objPUCdeposit.Amount = Convert.ToDecimal(txtDepositAmount.Text.Trim());
                    objPUCdeposit.PaymentModeId = Convert.ToInt32(cmbPaymentMode.SelectedValue);
                    objPUCdeposit.PaymentModeType = ((DataRowView)cmbPaymentMode.SelectedItem)["keyvalue1"].ToString();
                    objPUCdeposit.CreatedBy = m_userId;
                    objPUCdeposit.ModifiedBy = m_userId;

                    m_objPUCAccount.Deposits.Add(objPUCdeposit);
                }

                m_boolSuspendEventHandler = true;
                dgvPUCAccDep.DataSource = null;
                m_boolSuspendEventHandler = false;
                dgvPUCAccDep.DataSource = m_objPUCAccount.Deposits;

                btnSaveDBRecord.Enabled = IsSaveAvailable;
            }
        }

        private bool ValidatePUCAccDep()
        {
            bool isValid = true;
            string errMsg = string.Empty;

            errorprovPUCValid.Clear();
            m_sbErrorMessages = new StringBuilder();

            if (Convert.ToInt32(cmbLocCode.SelectedValue) == Common.INT_DBNULL)
            {
                isValid = false;
                errMsg = Common.GetMessage("VAL0002", lblLocCode.Text.Replace(":", "").Replace("*", ""));
                m_sbErrorMessages.AppendLine(errMsg);
                errorprovPUCValid.SetError(cmbLocCode, errMsg);
            }
            else
            {
                errorprovPUCValid.SetError(cmbLocCode, string.Empty);
            }
            if (string.IsNullOrEmpty(txtTranNo.Text.Trim()))
            {
                isValid = false;
                errMsg = Common.GetMessage("VAL0614", lblDepositAmount.Text.Replace(":", "").Replace("*", ""));
                m_sbErrorMessages.AppendLine(errMsg);
                errorprovPUCValid.SetError(txtTranNo, errMsg);
            }
            else
            {
                errorprovPUCValid.SetError(txtTranNo, string.Empty);
            }
            if (Convert.ToInt32(cmbPUC.SelectedValue) == Common.INT_DBNULL)
            {
                isValid = false;
                errMsg = Common.GetMessage("VAL0002", lblPUC.Text.Replace(":", "").Replace("*", ""));
                m_sbErrorMessages.AppendLine(errMsg);
                errorprovPUCValid.SetError(cmbPUC, errMsg);
            }
            else
            {

                //DataRow[] dr =  dtLocations_PUC.Select("PUCCode = " + cmbPUC.Text);


            }

            if (!dtpDepositDate.Checked)
            {
                isValid = false;
                errMsg = Common.GetMessage("VAL0002", lblDepositDate.Text.Replace(":", "").Replace("*", ""));
                m_sbErrorMessages.AppendLine(errMsg);
                errorprovPUCValid.SetError(dtpDepositDate, errMsg);
            }
            else
            {
                errorprovPUCValid.SetError(dtpDepositDate, string.Empty);
            }

            if (Convert.ToInt32(cmbPaymentMode.SelectedValue) == Common.INT_DBNULL)
            {
                isValid = false;
                errMsg = Common.GetMessage("VAL0002", lblPaymentMode.Text.Replace(":", "").Replace("*", ""));
                m_sbErrorMessages.AppendLine(errMsg);
                errorprovPUCValid.SetError(cmbPaymentMode, errMsg);
            }
            else
            {
                errorprovPUCValid.SetError(cmbPaymentMode, string.Empty);
            }

            if (string.IsNullOrEmpty(txtDepositAmount.Text.Trim()))
            {
                isValid = false;
                errMsg = Common.GetMessage("VAL0001", lblDepositAmount.Text.Replace(":", "").Replace("*", ""));
                m_sbErrorMessages.AppendLine(errMsg);
                errorprovPUCValid.SetError(txtDepositAmount, errMsg);
            }
            else if (!Validators.IsValidAmount(txtDepositAmount.Text.Trim()))
            {
                try
                {
                    if (Convert.ToInt32(txtDepositAmount.Text.Trim()) < 0)
                    {
                        string errorMessage = "";

                        int locCode = Common.INT_DBNULL;
                        int pcId = Common.INT_DBNULL;
                        int price;
                        string transNo = txtTranNo.Text.Trim();
                        string date;

                        date = dtpDepositDate.Value.ToString();
                        locCode = (int)cmbLocCode.SelectedValue;
                        pcId = (int)cmbPUC.SelectedValue;
                        price = (Convert.ToInt32(txtDepositAmount.Text));


                        PUCCommonTransaction objPUCInfo = new PUCCommonTransaction();
                        DataTable dtPUCInfo = objPUCInfo.FetchPUCInfo(locCode, pcId, Convert.ToInt32(cmbPaymentMode.SelectedValue), transNo, date, 1, price, ref errorMessage);
                        if (dtPUCInfo.Rows.Count > 0)
                        {
                            isValid = true;
                            string Ttext = txtTranNo.Text.Trim().ToString();
                            txtTranNo.Text = Ttext + "(Negate)";
                        }
                        else
                        {
                            isValid = false;
                            errMsg = Common.GetMessage("VAL0081", lblDepositAmount.Text.Replace(":", "").Replace("*", ""));
                            m_sbErrorMessages.AppendLine(errMsg);
                            errorprovPUCValid.SetError(txtDepositAmount, errMsg);
                        }
                    }
                    else
                    {
                        isValid = false;
                        errMsg = Common.GetMessage("VAL0081", lblDepositAmount.Text.Replace(":", "").Replace("*", ""));
                        m_sbErrorMessages.AppendLine(errMsg);
                        errorprovPUCValid.SetError(txtDepositAmount, errMsg);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else if (Convert.ToDecimal(txtDepositAmount.Text.Trim()) == 0)
            {
                isValid = false;
                errMsg = Common.GetMessage("VAL0081", lblDepositAmount.Text.Replace(":", "").Replace("*", ""));
                m_sbErrorMessages.AppendLine(errMsg);
                errorprovPUCValid.SetError(txtDepositAmount, errMsg);
            }
            else
            {
                errorprovPUCValid.SetError(txtDepositAmount, string.Empty);
            }

            if (m_sbErrorMessages.Length > 0)
            {
                MessageBox.Show(m_sbErrorMessages.ToString(), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return isValid;
        }

        private bool ValidatePUCAcc()
        {
            bool isValid = true;

            errorprovPUCValid.Clear();
            m_sbErrorMessages = new StringBuilder();

            if (m_objPUCAccount == null)
            {
                isValid = false;
                m_sbErrorMessages.AppendLine(Common.GetMessage("VAL0001", "valid PUC-Account with its Deposit(s)"));
            }

            if (m_sbErrorMessages.Length > 0)
            {
                MessageBox.Show(m_sbErrorMessages.ToString(), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return isValid;
        }

        private void ResetUI()
        {
            m_uisCurrentState = UIState.ResetAcc;
            ResetPUCAccDepGrid();
            ResetPUCAccGrid();
            ManageUIState();
            ResetPUCAccDep();

        }

        private void ResetPUCAccDepGrid()
        {
            m_boolSuspendEventHandler = true;
            dgvPUCAccDep.DataSource = null;
            m_boolSuspendEventHandler = false;
        }

        private void ResetPUCAccGrid()
        {
            m_boolSuspendEventHandler = true;
            dgvPUCAccounts.DataSource = null;
            m_boolSuspendEventHandler = false;
        }

        private void ResetPUCAccDep()
        {
            ManagePUCAccControls(true);
            ResetPUCAccControls();

            SetTransactionNo();
        }

        private void ResetPUCAccControls()
        {
            if ((dgvPUCAccDep.Rows.Count == 0) && (!btnSaveDBRecord.Enabled))
            {
                if (m_uisCurrentState == UIState.ResetAcc)
                {
                    FillLocationControls(PUCCommonTransaction.Locations.LocationCode, (int)Common.LocationConfigId.BO);
                }

                //txtTranNo.Text = string.Empty;
                dtpDepositDate.Value = DateTime.Now.Date;
                dtpDepositDate.Checked = false;
                cmbPaymentMode.SelectedIndex = 0;
            }
            txtDepositAmount.Text = string.Empty;
            m_sbErrorMessages = null;
            errorprovPUCValid.Clear();
        }

        #endregion

        private void SetTransactionNo()
        {
            if (L != null)
            {
                int a = L.IndexOf("-");
                if (a != -1)
                    L = L.Substring(a + 1);
            }
            if (PM != null)
            {
                int a = PM.IndexOf("-");
                if (a != -1)
                    PM = PM.Substring(a + 1);
            }
            if (P != null)
            {
                int a = P.IndexOf("-");
                if (a != -1)
                    P = P.Substring(a + 1);
            }

            if (L != null && L != "" && L != DefaultValue && PM != null && PM != "" && PM != DefaultValue && P != null && P != "" && P != DefaultValue)
                txtTranNo.Text = (L + "/" + PM + "/" + P + "/").ToString();
            else if (L != null && L != "" && L != DefaultValue && PM != null && PM != "" && PM != DefaultValue)
                txtTranNo.Text = (L + "/" + PM + "/").ToString();
            else if (L != null && L != "" && L != DefaultValue)
                txtTranNo.Text = (L + "/").ToString();
            else if ((L == null || L == "" || L == DefaultValue) && (PM == null || PM == "" || PM == DefaultValue) && (P == null || P == "" || P == DefaultValue))
                txtTranNo.Text = "";
        }

        private void cmbPUC_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (dgvPUCAccDep.RowCount == 0 || cmbPUC.Text.Trim() != string.Empty)
            //{
            //    if (cmbPUC.Text.Trim() != string.Empty)
            //        P = cmbPUC.Text.Trim();
            //    int a = P.IndexOf("-");
            //    P = P.Substring(a + 1);
            //    if ((P == DefaultValue && L == DefaultValue && IsDtpchecked == false && PM == DefaultValue) || (P == null && L == null && PM == null))
            //        txtTranNo.Text = null;
            //    else if (IsDtpchecked == false && PM == DefaultValue)
            //        txtTranNo.Text = L + "/" + P + "/";
            //    else if (IsDtpchecked == true)
            //        txtTranNo.Text = L + "/" + P + "/";
            //    else if (PM != DefaultValue && PM != null)
            //        txtTranNo.Text = (L + "/" + P + "/" + PM + "/").ToString();

            //}

            if (cmbPUC.Text.Trim() != string.Empty && cmbPUC.Text.Trim() != DefaultValue)
                P = cmbPUC.Text.Trim();
            else
                P = null;
            SetTransactionNo();
        }

        private void dtpDepositDate_ValueChanged(object sender, EventArgs e)
        {

            //if (dgvPUCAccDep.RowCount == 0 || cmbPUC.Text.Trim() != string.Empty)
            //{
            //    //IsDtpchecked =dtpDepositDate.Checked;
            //    DD = dtpDepositDate.Value.ToString(Common.DATE_FORMAT);
            //    if (P == DefaultValue && L == DefaultValue && IsDtpchecked == false && PM == DefaultValue)
            //        txtTranNo.Text = null;
            //    else if (IsDtpchecked == false && PM == DefaultValue)
            //        txtTranNo.Text = (L + "/" + P );
            //    else if (IsDtpchecked == false && PM != DefaultValue)
            //        txtTranNo.Text = (L + "/" + P + "/" + cmbPaymentMode.Text.ToString() + "/").ToString();
            //    else if (IsDtpchecked == true && PM == DefaultValue)
            //        txtTranNo.Text = L + "/" + P;
            //}
        }

        private void cmbPaymentMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            ////////////////////////////////////// Commented by Saurabh Garg //////////////////////////////

            //if (dgvPUCAccDep.RowCount == 0 || cmbPUC.Text.Trim() != string.Empty)
            //{
            //    if (cmbPUC.Text.Trim() != string.Empty)
            //        PM = cmbPaymentMode.Text;

            //    if (PM != DefaultValue)
            //        txtTranNo.Text = (L + "/" + cmbPaymentMode.Text.ToString() + "/" + P + "/").ToString();
            //    else if (P == DefaultValue && L == DefaultValue && IsDtpchecked == false && PM == DefaultValue && P == null && L == null && PM == null)
            //        txtTranNo.Text = null;
            //    else if (P != DefaultValue || L != DefaultValue || IsDtpchecked == false || PM == DefaultValue)
            //        txtTranNo.Text = (L + "/");
            //    else if (P == string.Empty && L == string.Empty && IsDtpchecked == false && PM == DefaultValue)
            //        txtTranNo.Text = null;

            //}

            if (cmbPaymentMode.Text.Trim() != string.Empty && cmbPaymentMode.Text.Trim() != DefaultValue)
                PM = cmbPaymentMode.Text;
            else
                PM = null;

            SetTransactionNo();

            ///////////////////////////////////////////////////////////////////////////////////////////////////
        }

        private void txtDepositAmount_Validating(object sender, CancelEventArgs e)
        {
            if (!Validators.IsNumeric(txtDepositAmount.Text))
            {
                MessageBox.Show(Common.GetMessage("VAL0021", "Amount"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDepositAmount.Text = "";
                txtDepositAmount.Focus();
            }
        }

    }
}
