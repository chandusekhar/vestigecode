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


namespace POSClient.UI
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
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion


        #region Events

        private void frmPickUpCentre_Load(object sender, EventArgs e)
        {
            CON_MODULENAME = "PUC01"; //this.Tag.ToString();

            InitializeRights();

            InitializeAndPopulateControls();

            this.lblPageTitle.Text = "Pick-Up-Centre Accounts";
            this.tabSearch.Text = "Search / Create";
            this.tabControlTransaction.TabPages.Remove(tabCreate);

            m_uisCurrentState = UIState.ResetAcc;
            ManageUIState();
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
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearchReset_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_objPUCAccount != null)
                {
                    m_uisCurrentState = UIState.AddAccDep;
                }
                else
                {
                    m_uisCurrentState = UIState.ResetAcc;
                }
                ResetPUCAccDep();
                ManageUIState();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddPUCAccDep_Click(object sender, EventArgs e)
        {
            try
            {
                AddPUCAccDep();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnResetUI_Click(object sender, EventArgs e)
        {
            try
            {
                //DialogResult resetresult = MessageBox.Show(Common.GetMessage("5006"), Common.GetMessage("10004"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //if (resetresult == DialogResult.Yes)
                {
                    ResetUI();
                    m_objPUCAccount = null;
                    m_lstPUCAccounts = null;
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                            ReflectPUCAccDep(Convert.ToInt32(dgvSender.SelectedRows[0].Cells["PCId"].Value.ToString()),
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
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSaveDBRecord_Click(object sender, EventArgs e)
        {
            try
            {
                SavePUCAcc();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                                    DialogResult confirmresult = MessageBox.Show(Common.GetMessage("5012"), Common.GetMessage("10004"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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
                        MessageBox.Show(errMsg, Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            DataTable dtTemp = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter("POSPUCDEPOSITMODE", 0, 0, 0));
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
                        DataTable dtPOSLocations = Common.ParameterLookup(Common.ParameterType.POSLocations, new ParameterFilter(Common.LocationCode, -1, -1, -1));
                        DataRow[] rows = dtPOSLocations.Select("LocationId = " + Common.CurrentLocationId.ToString());
                        Common.BOId = rows[0]["LocationType"].ToString() == Common.LocationConfigId.BO.ToString() ? Convert.ToInt32(rows[0]["LocationId"]) : Convert.ToInt32(rows[0]["ReplenishmentId"]);
                        //Common.BOId = Convert.ToInt32(rows[0]["ReplenishmentId"]) == -1 ? Convert.ToInt32(rows[0]["LocationId"]) : Convert.ToInt32(rows[0]["ReplenishmentId"]);
                        Common.PCId = Convert.ToInt32(rows[0]["LocationId"]);

                        DataTable dtLocations = Common.ParameterLookup(Common.ParameterType.WhgBoPucLocations, new ParameterFilter(string.Empty, Convert.ToInt32(locTypeId), Convert.ToInt32(locCodeId), 0));
                        cmbLocCode.DataSource = dtLocations;
                        cmbLocCode.DisplayMember = "LOCCODE";
                        cmbLocCode.ValueMember = "LOCVAL";

                        cmbLocCode.SelectedValue = Common.BOId;
                        cmbLocCode.Enabled = false;

                        FillLocationControls(PUCCommonTransaction.Locations.PUCCode, Common.BOId);

                        //dtLocations_PUC = Common.ParameterLookup(Common.ParameterType.WhgBoPucLocations, new ParameterFilter(string.Empty, Common.INT_DBNULL, (int)Common.LocationConfigId.PC, 0));
                        //cmbPUC.DataSource = dtLocations_PUC;
                       
                        //cmbPUC.DisplayMember = "PUCCODE";
                        //cmbPUC.ValueMember = "LOCVAL";

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
            try
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
                    if (Convert.ToInt32(cmbPUC.SelectedValue) != Common.INT_DBNULL)
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
                                MessageBox.Show(Common.GetMessage("8002"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            m_objPUCAccount = null;
                            MessageBox.Show(Common.GetMessage("8002"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show(errorMessage, Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show(m_sbErrorMessages.ToString(), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
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
                            cmbLocCode.Enabled = false;
                            cmbPUC.Enabled = true;
                        }
                        btnAddPUCAccDep.Enabled = true;
                        btnSearchReset.Enabled = true;
                    }
                    break;

                case UIState.EditAccDep:
                    {
                        btnAddPUCAccDep.Enabled = false;
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
                        cmbLocCode.Enabled = false;
                        cmbPUC.Enabled = true;
                        btnAddPUCAccDep.Enabled = true;
                        btnSearchReset.Enabled = true;
                        btnSearchDBRecords.Enabled = IsSearchAvailable; //true;
                        btnSaveDBRecord.Enabled = false; //IsSaveAvailable; //true;
                        btnResetUI.Enabled = true;
                    }
                    break;
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

        private void ReflectPUCAccDep(int pcId, string recordNo, int paymentModeId, string tranNo, decimal depositAmount, DateTime depositDate)
        {
            if (m_objPUCAccount != null)
            {
                ManagePUCAccControls(false);

                cmbLocCode.SelectedValue = m_objPUCAccount.LocationCodeId;
                cmbPUC.SelectedValue = m_objPUCAccount.PCId; //m_objPUCAccount.PUCLocId;
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
            cmbLocCode.Enabled = false;
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
                DialogResult saveResult = MessageBox.Show(Common.GetMessage("INF0027"), Common.GetMessage("10004"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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
                    PUCCommonTransaction objTransaction = new PUCCommonTransaction();
                    if (objTransaction.SavePUCInfo(m_objPUCAccount, ref errMsg))
                    {
                        MessageBox.Show(Common.GetMessage("8001"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        m_uisCurrentState = UIState.ViewAcc;
                        ResetPUCAccDep();

                        SearchPUCAcc();
                        ReflectPUCAcc(m_objPUCAccount.PCId);
                        ManageUIState();
                        btnSaveDBRecord.Enabled = false;
                    }
                    else
                    {
                        if (errMsg.StartsWith("30001"))
                        {
                            MessageBox.Show(Common.GetMessage("30001"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Common.LogException(new Exception(errMsg));
                        }
                        else
                        {
                            MessageBox.Show(Common.GetMessage(errMsg), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            }
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
                    objPUCdeposit.LocationCodeId = m_objPUCAccount.LocationCodeId;
                    objPUCdeposit.PCId = m_objPUCAccount.PCId;
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
                    objPUCdeposit.LocationCodeId = m_objPUCAccount.LocationCodeId;
                    objPUCdeposit.PCId = m_objPUCAccount.PCId;
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
                        }
                        else
                            isValid = false;
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
                MessageBox.Show(m_sbErrorMessages.ToString(), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show(m_sbErrorMessages.ToString(), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return isValid;
        }

        private void ResetUI()
        {
            m_uisCurrentState = UIState.ResetAcc;
            ResetPUCAccDep();
            ResetPUCAccDepGrid();
            ResetPUCAccGrid();
            ManageUIState();
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
        }

        private void ResetPUCAccControls()
        {
            if (m_uisCurrentState == UIState.ResetAcc)
            {
                FillLocationControls(PUCCommonTransaction.Locations.LocationCode, (int)Common.LocationConfigId.BO);
            }

            txtTranNo.Text = string.Empty;
            dtpDepositDate.Value = DateTime.Now.Date;
            dtpDepositDate.Checked = false;
            cmbPaymentMode.SelectedValue = -1;
            txtDepositAmount.Text = string.Empty;

            m_sbErrorMessages = null;
            errorprovPUCValid.Clear();
        }

        #endregion

    }
}
