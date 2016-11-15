using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

//vinculum-framework namespace(s)
using Vinculum.Framework.Data;
using Vinculum.Framework.DataTypes;
using CoreComponent.Core.BusinessObjects;
using CoreComponent.BusinessObjects;


namespace CoreComponent.UI
{
    public partial class frmDistributorSearchModule : CoreComponent.UI.MultiTabTemplate //CoreComponent.Core.UI.Transaction
    {

        #region Constants

        private const string CON_GRIDCOL_UPLINE = "Upline";
        private const string CON_GRIDCOL_DOWNLINE = "Downline";
        private const string SP_ORDER_PRINT = "usp_GetOrderPrint";
        private int m_userId;
        #endregion


        #region Properties
        public bool IsPos
        { get; set; }

        #endregion
        
        #region Variables

        //constant defined for storing 'search stored-procedure' name
        private const string m_uspDistributorSearch = "usp_getDistributorMasterInfo";
        private string edittype = "";
        //object of business-class DistributorModule
        private CoreComponent.BusinessObjects.Distributor m_objDistributorMain = null;
        private bool m_IsSearchAvailable = false;
        private bool m_IsUpdateAvailable = false;
        private int m_SelectedBank = Common.INT_DBNULL;
        private string m_AccountNo = string.Empty;
        private string m_BankBranchCode = string.Empty;
        private bool m_UpdateDistributorNameAvailable = false;
        private bool m_UpdateDistributorPanBankDetail = false;
        private bool m_DisplayPassword = false;
        private bool m_EnableInvoicePrint = false;
        private int m_BankAccountNoLength;
        private DistributorEditHistory d;
        private int NavigatePos = 0;

        private string m_VaidateDistributorName = string.Empty;
        private DataTable dtBankBranch;
        private bool m_DistributorResignation = false;
        private bool m_ForSkincareItem = false;
        private DistributorEditHistory objDistrHistoryLog1 = new DistributorEditHistory();
        /*
        private DistributorEditHistory objDistrHistoryLog2 = new DistributorEditHistory();
        private DistributorEditHistory objDistrHistoryLog3 = new DistributorEditHistory();
        private DistributorEditHistory objDistrHistoryLog4 = new DistributorEditHistory();
        private DistributorEditHistory objDistrHistoryLog5 = new DistributorEditHistory();
        private DistributorEditHistory objDistrHistoryLog6 = new DistributorEditHistory();
         */
        //List<DistributorEditHistory> objLstDistHistoryLog = new List<DistributorEditHistory>();
        List<DistributorEditHistory> objLstDistHistoryLog = null;
        private string profileFlag = "";
        
        #endregion


        #region Constructor

        public frmDistributorSearchModule()
        {
            try
            {
                this.label1.Text = "Distributor-Info Search";

                InitializeComponent();

                //Reorder Layout for MiniBranch specfic controls
                if (CheckIfLocationIsMiniBranch())
                    LoadControlsForMiniBranch();

                dgvDownlines.AutoGenerateColumns = false;
                dgvDownlines = Common.GetDataGridViewColumns(dgvDownlines, Environment.CurrentDirectory + "\\App_Data\\GridViewDefinition.xml");

                //// -- COMMENTED BY ANUBHAV BECAUSE COLUMNS ARE BEING GNERATED BY DYNAMIC PIVOT QUERY SO THEY MAY INCREASE OR DECREASE
                //dgvBonus.AutoGenerateColumns = false;
                //dgvBonus = Common.GetDataGridViewColumns(dgvBonus, Environment.CurrentDirectory + "\\App_Data\\GridViewDefinition.xml");
                FormatGrid(dgvBonus);

                dgvInvoice.AutoGenerateColumns = false;
                dgvInvoice = Common.GetDataGridViewColumns(dgvInvoice, Environment.CurrentDirectory + "\\App_Data\\GridViewDefinition.xml");

                dgvAllInvoices.AutoGenerateColumns = false;
                dgvAllInvoices = Common.GetDataGridViewColumns(dgvAllInvoices, Environment.CurrentDirectory + "\\App_Data\\GridViewDefinition.xml");

                dgvLastMonth.AutoGenerateColumns = false;
                dgvLastMonth = Common.GetDataGridViewColumns(dgvLastMonth, Environment.CurrentDirectory + "\\App_Data\\GridViewDefinition.xml");

                dgvDailyInvoice.AutoGenerateColumns = false;
                Common.GetDataGridViewColumns(dgvDailyInvoice, Environment.CurrentDirectory + "\\App_Data\\GridViewDefinition.xml");

                dgvLevelHistory.AutoGenerateColumns = false;
                dgvLevelHistory = Common.GetDataGridViewColumns(dgvLevelHistory, Environment.CurrentDirectory + "\\App_Data\\GridViewDefinition.xml");

                BindComboBox();
                GetBankBranchDetail();
                dtpDistributorDOB.CustomFormat = Common.DTP_DATE_FORMAT;
                dtpCoDistributorDOB.CustomFormat = Common.DTP_DATE_FORMAT;

                m_IsSearchAvailable = AuthenticationComponent.BusinessObjects.Authenticate.IsFunctionAccessible(AuthenticationComponent.BusinessObjects.Authenticate.LoggedInUser.UserName, Common.LocationCode, "DS01", "F001");
                m_IsUpdateAvailable = AuthenticationComponent.BusinessObjects.Authenticate.IsFunctionAccessible(AuthenticationComponent.BusinessObjects.Authenticate.LoggedInUser.UserName, Common.LocationCode, "DS01", "F004");
                m_UpdateDistributorNameAvailable = AuthenticationComponent.BusinessObjects.Authenticate.IsFunctionAccessible(AuthenticationComponent.BusinessObjects.Authenticate.LoggedInUser.UserName, Common.LocationCode, "DS01", "F042");
                //Added for Displaying Distribuot Password in Distributor Query window
                m_DisplayPassword = AuthenticationComponent.BusinessObjects.Authenticate.IsFunctionAccessible(AuthenticationComponent.BusinessObjects.Authenticate.LoggedInUser.UserName, Common.LocationCode, "DS01", "F043");
                m_EnableInvoicePrint = AuthenticationComponent.BusinessObjects.Authenticate.IsFunctionAccessible(AuthenticationComponent.BusinessObjects.Authenticate.LoggedInUser.UserName, Common.LocationCode, "DS01", "F044");
                btnSearch.Enabled = m_IsSearchAvailable;
                btnSearchUpline.Enabled = m_IsSearchAvailable;
                btnHistory.Enabled = m_IsSearchAvailable;
                btnAccountHistory.Enabled = m_IsSearchAvailable;
                btnSave.Enabled = false;
                lblPassword.Visible = m_DisplayPassword;
                txtPassword.Visible = m_DisplayPassword;
                txtPassword.Enabled = m_DisplayPassword;
                //grpReject.Enabled = false;
                chkPan.Enabled = false;
                chkBank.Enabled = false;
                m_DistributorResignation = AuthenticationComponent.BusinessObjects.Authenticate.IsFunctionAccessible(AuthenticationComponent.BusinessObjects.Authenticate.LoggedInUser.UserName, Common.LocationCode, "DS01", "F045");
                m_ForSkincareItem = AuthenticationComponent.BusinessObjects.Authenticate.IsFunctionAccessible(AuthenticationComponent.BusinessObjects.Authenticate.LoggedInUser.UserName, Common.LocationCode, "DS01", "F046");
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
       
        /// <summary>
        /// Checks for mini Branch
        /// </summary>
        /// <returns></returns>
        private bool CheckIfLocationIsMiniBranch()
        {
            DataTable dtLocation = Common.ParameterLookup(Common.ParameterType.IsMiniBranch, new ParameterFilter(Common.LocationCode, 0, 0, 0));
            try
            {
                if (dtLocation != null && dtLocation.Rows.Count > 0 && dtLocation.Rows[0].ItemArray[0] != null && dtLocation.Rows[0].ItemArray[0].ToString() == "1")
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }



        private void RemoveUnnecessaryTabs()
        {
            grpComputedData.Visible = true;
            if (IsPos)
            {
                this.Size = new Size(1028, 733);
                this.tabMultiTabTemplate.TabPages.RemoveByKey("tabPage3");
                //this.tabMultiTabTemplate.TabPages.RemoveByKey("tabPage4");
                //this.tabMultiTabTemplate.TabPages.RemoveByKey("tabPage6");
                this.tabMultiTabTemplate.TabPages.RemoveByKey("tabPage7");
                this.tabMultiTabTemplate.TabPages.RemoveByKey("tabPage8");
                this.tabMultiTabTemplate.TabPages.RemoveByKey("tabPage9");
                this.tabMultiTabTemplate.TabPages.RemoveByKey("tabPage10");
            }
            else
            {
                //this.Size = new Size(1028, 772);

                this.tabMultiTabTemplate.TabPages.RemoveByKey("tabPage9");
                this.tabMultiTabTemplate.TabPages.RemoveByKey("tabPage10");
                //if (Common.LocationCode == "HO")
                //  grpComputedData.Visible = true;
            }
        }

        #endregion


        #region Events



        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if ((txtSearchDistribID.Text == "") && (txtSearchDistribFName.Text == "") && (txtSearchDistribLName.Text == ""))
                {
                    MessageBox.Show(Common.GetMessage("INF0237"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    NavigatePos = 0;
                    DisableFields();

                    SetAllInfo(txtSearchDistribID.Text.Trim(), txtSearchDistribFName.Text.Trim(), txtSearchDistribLName.Text.Trim());

                    //m_UpdateDistributorPanBankDetail = AuthenticationComponent.BusinessObjects.Authenticate.IsConditionAccessible(AuthenticationComponent.BusinessObjects.Authenticate.LoggedInUser.UserName, Common.LocationCode, "DS01", "F042", m_objDistributorMain.CreatedDate);      
                    
                    if ((m_objDistributorMain != null) && (m_objDistributorMain.DistributorId > 0))
                    {
                        btnEdit.Enabled = m_IsUpdateAvailable;
                        btnSave.Enabled = m_IsUpdateAvailable;
                    }
                    else
                    {
                        btnEdit.Enabled = false;
                        btnSave.Enabled = false;
                    }
                    m_VaidateDistributorName = txtDistribFName.Text + txtDistribLName.Text;
                }
            }
            //SetDistributorNavigation(Convert.ToInt32(txtSearchDistribID.Text.Trim()));

            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearchUpline_Click(object sender, EventArgs e)
        {
            try
            {
                NavigatePos = 0;
                DisableFields();
                if (!string.IsNullOrEmpty(txtSearchDistribID.Text.Trim()) ||
                    !string.IsNullOrEmpty(txtSearchDistribFName.Text.Trim()) ||
                    !string.IsNullOrEmpty(txtSearchDistribLName.Text.Trim()))
                {
                    SetAllInfo(txtSearchDistribID.Text.Trim(), txtSearchDistribFName.Text.Trim(), txtSearchDistribLName.Text.Trim(), "UP", "XACT");
                }
                else
                {
                    if (MessageBox.Show(Common.GetMessage("VAL0098"), "No Distributor selected", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        SetAllInfo(txtSearchDistribID.Text.Trim(), txtSearchDistribFName.Text.Trim(), txtSearchDistribLName.Text.Trim(), string.Empty, "XACT");
                        SetAllInfo(txtSearchDistribID.Text.Trim(), txtSearchDistribFName.Text.Trim(), txtSearchDistribLName.Text.Trim(), "UP", "XACT");
                    }
                }
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
                ResetAllInfo();
                btnFirst.Enabled = false;
                btnPrevious.Enabled = false;
                btnNext.Enabled = false;
                btnLast.Enabled = false;
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvDownlines_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                NavigatePos = 0;
                DataGridView dgvSender = (DataGridView)sender;
                if ((e.RowIndex > -1) && (e.ColumnIndex > -1))
                {
                    DataTable dtTemp = (DataTable)((DataGridView)sender).DataSource;
                    switch (dgvSender.Columns[e.ColumnIndex].Name)
                    {
                        case CON_GRIDCOL_UPLINE:
                            {
                                SetAllInfo(dtTemp.Rows[e.RowIndex]["DISTRIBUTORID"].ToString().Trim(), dtTemp.Rows[e.RowIndex]["DISTRIBUTORFIRSTNAME"].ToString().Trim(), dtTemp.Rows[e.RowIndex]["DISTRIBUTORLASTNAME"].ToString().Trim(), "UP", "XACT");
                                SetAllInfo(txtSearchDistribID.Text.Trim(), txtSearchDistribFName.Text.Trim(), txtSearchDistribLName.Text.Trim(), "UP", "XACT");
                            }
                            break;

                        case CON_GRIDCOL_DOWNLINE:
                            {
                                SetAllInfo(dtTemp.Rows[e.RowIndex]["DISTRIBUTORID"].ToString().Trim(), dtTemp.Rows[e.RowIndex]["DISTRIBUTORFIRSTNAME"].ToString().Trim(), dtTemp.Rows[e.RowIndex]["DISTRIBUTORLASTNAME"].ToString().Trim(), "DOWN", "XACT");
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvDownlines_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                NavigatePos = 0;
                if (e.KeyCode == Keys.Back)
                {
                    DataTable dtTemp = (DataTable)((DataGridView)sender).DataSource;
                    SetAllInfo(dtTemp.Rows[dgvDownlines.SelectedRows[0].Index]["DISTRIBUTORID"].ToString().Trim(), dtTemp.Rows[dgvDownlines.SelectedRows[0].Index]["DISTRIBUTORFIRSTNAME"].ToString().Trim(), dtTemp.Rows[dgvDownlines.SelectedRows[0].Index]["DISTRIBUTORLASTNAME"].ToString().Trim(), "UP", "XACT");
                    SetAllInfo(txtSearchDistribID.Text.Trim(), txtSearchDistribFName.Text.Trim(), txtSearchDistribLName.Text.Trim(), "UP", "XACT");
                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    DataTable dtTemp = (DataTable)((DataGridView)sender).DataSource;
                    SetAllInfo(dtTemp.Rows[dgvDownlines.SelectedRows[0].Index]["DISTRIBUTORID"].ToString().Trim(), dtTemp.Rows[dgvDownlines.SelectedRows[0].Index]["DISTRIBUTORFIRSTNAME"].ToString().Trim(), dtTemp.Rows[dgvDownlines.SelectedRows[0].Index]["DISTRIBUTORLASTNAME"].ToString().Trim(), "DOWN", "XACT");
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tabPage2_Enter(object sender, EventArgs e)
        {
            dgvDownlines.Select();
        }

        private void tabPage3_Enter(object sender, EventArgs e)
        {
            dgvBonus.Select();
        }

        private void tabPage4_Enter(object sender, EventArgs e)
        {
            //dgvInvoice.Select();
            //DataTable dtInvoice = null;
            try
            {
                FillInvoiceData(dgvDailyInvoice, "Day");
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
            }
        }

        private void FillInvoiceData(DataGridView objGrid, string sConditionParam)
        {
            DataTable dtInvoice = null;
            try
            {
                if (string.IsNullOrEmpty(txtSearchDistribID.Text.Trim()))
                    return;
                Distributor objDistributor = null;
                objDistributor = new Distributor();
                dtInvoice = objDistributor.GetInvoiceData(txtSearchDistribID.Text.Trim(), sConditionParam);
                objGrid.DataSource = dtInvoice;

            }
            finally
            {

            }
        }

        private void tabPage5_Enter(object sender, EventArgs e)
        {
            FillInvoiceData(dgvInvoice, "Month");
        }

        private void tabPage6_Enter(object sender, EventArgs e)
        {
            FillInvoiceData(dgvAllInvoices, "All");
        }

        void tabPage7_Enter(object sender, System.EventArgs e)
        {
            dgvLastMonth.Select();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (btnEdit.Text == "E&dit")
                EditDistributorDetails();
            else
                UpdateDistributorDetails();
        }

        private void lblSearchResult_Click(object sender, EventArgs e)
        {

        }

        private void cmbCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbCountry.SelectedIndex > 0)
                {
                    DataTable dtStates = Common.ParameterLookup(Common.ParameterType.State, new ParameterFilter("", Convert.ToInt32(cmbCountry.SelectedValue), -1, -1));
                    cmbState.DataSource = dtStates;
                    cmbState.DisplayMember = "StateName";
                    cmbState.ValueMember = "StateId";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        private void cmbState_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbState.SelectedIndex > 0)
                {
                    //DataTable dtZones = Common.ParameterLookup(Common.ParameterType.Zone, new ParameterFilter("", Convert.ToInt32(cmbState.SelectedValue), 0, 0));
                    //cmbZone.DataSource = dtZones;
                    //cmbZone.DisplayMember = "OrgHierarchyCode";
                    //cmbZone.ValueMember = "OrgHierarchyId";

                    //if (cmbZone.Items.Count > 1)
                    //{
                    //    cmbZone.SelectedIndex = 1;
                    //}
                    //cmbZone.Enabled = false;
                    DataTable dtCities = Common.ParameterLookup(Common.ParameterType.City, new ParameterFilter("", Convert.ToInt32(cmbState.SelectedValue), -1, -1));
                    cmbCity.DataSource = dtCities;
                    cmbCity.DisplayMember = "CityName";
                    cmbCity.ValueMember = "CityId";

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        private void dgvDownlines_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                NavigatePos = 0;
                DataGridView dgvSender = (DataGridView)sender;
                if ((e.RowIndex > -1) && (e.ColumnIndex > -1))
                {
                    DataTable dtTemp = (DataTable)((DataGridView)sender).DataSource;

                    SetAllInfo(dtTemp.Rows[e.RowIndex]["DISTRIBUTORID"].ToString().Trim(), dtTemp.Rows[e.RowIndex]["DISTRIBUTORFIRSTNAME"].ToString().Trim(), dtTemp.Rows[e.RowIndex]["DISTRIBUTORLASTNAME"].ToString().Trim(), "DOWN", "XACT");
                    //switch (dgvSender.Columns[e.ColumnIndex].Name)
                    //{
                    //    case CON_GRIDCOL_UPLINE:
                    //        {
                    //            SetAllInfo(dtTemp.Rows[e.RowIndex]["DISTRIBUTORID"].ToString().Trim(), dtTemp.Rows[e.RowIndex]["DISTRIBUTORFIRSTNAME"].ToString().Trim(), dtTemp.Rows[e.RowIndex]["DISTRIBUTORLASTNAME"].ToString().Trim(), "UP", "XACT");
                    //            SetAllInfo(txtSearchDistribID.Text.Trim(), txtSearchDistribFName.Text.Trim(), txtSearchDistribLName.Text.Trim(), "UP", "XACT");
                    //        }
                    //        break;

                    //    case CON_GRIDCOL_DOWNLINE:
                    //        {
                    //            SetAllInfo(dtTemp.Rows[e.RowIndex]["DISTRIBUTORID"].ToString().Trim(), dtTemp.Rows[e.RowIndex]["DISTRIBUTORFIRSTNAME"].ToString().Trim(), dtTemp.Rows[e.RowIndex]["DISTRIBUTORLASTNAME"].ToString().Trim(), "DOWN", "XACT");
                    //        }
                    //        break;
                    //}
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmDistributorSearchModule_Load(object sender, EventArgs e)
        {
            RemoveUnnecessaryTabs();
            txtSearchDistribID.Select();
            if (Common.LocationCode.ToUpper() != "HO")
            {
                lblRefId.Visible = false;
                lblRefIdTitle.Visible = false;
            }
            else
            {
                lblRefId.Visible = true;
                lblRefIdTitle.Visible = true; 
            }
        }

        private void btnHistory_Click(object sender, EventArgs e)
        {
            frmDistributorHistory objDSHistory;
            try
            {
                if (string.IsNullOrEmpty(txtSearchDistribID.Text))
                {
                    //MessageBox.Show("Please enter the distributor id");
                    MessageBox.Show(string.Format(Common.GetMessage("VAL0001"), "distributor id"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                objDSHistory = new frmDistributorHistory();
                objDSHistory.DistributorId = txtSearchDistribID.Text.Trim();
                objDSHistory.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
            finally
            {
                objDSHistory = null;
            }
        }

        #endregion


        #region Methods

        /// <summary>
        /// Set/Show all information on all screens
        /// </summary>
        /// <param name="searchParams">variable no. of parameters containing: Distributor Id, First Name, Last Name, Search-Type; in the same order</param>
        /// 


        private void SetAllInfo(params string[] searchParams)
        {
            string distributorID = string.Empty;
            string distributorFName = string.Empty;
            string distributorLName = string.Empty;
            string searchCondition = string.Empty;
            string searchType = string.Empty;

            if (searchParams.Length > 0)
            {
                distributorID = searchParams[0];
            }
            if (searchParams.Length > 1)
            {
                distributorFName = searchParams[1];
            }
            if (searchParams.Length > 2)
            {
                distributorLName = searchParams[2];
            }
            if (searchParams.Length > 3)
            {
                searchCondition = searchParams[3];
            }
            if (searchParams.Length > 4)
            {
                searchType = searchParams[4];
            }

            string errMsg = string.Empty;


            Distributor objTemp = new Distributor();
            DataSet ds = objTemp.SearchAllInfoForDistributor(distributorID, distributorFName, distributorLName, searchCondition, searchType, ref errMsg);
            if (ds != null)
            {
                if (ds.Tables.Count > 1)
                {
                    //SetDistributorObject(ds);
                    //SetDistributorInfo();
                    //SetDownlinesInfo(ds);
                    //SetBonusInfo(ds);
                    //SetInvoicesInfo(ds);
                    //SetAllInvoicesInfo(ds);
                    //SetLastMonthInfo(ds);
                    //SetSuccessInfo(ds);
                    SetResetInfo(ds);
                }
                else if (ds.Tables.Count == 1)
                {
                    //CHECK WHETHER DISTRIBUTOR WAS SEARCHED OR ITS UPLINE WAS SEARCHED OR ITS DOWNLINE WAS SEARCHED
                    if (searchCondition == "UP")
                    {
                        //CHECK WHETHER DATABASE IS IN STATE-CONFLICT
                        if (ds.Tables[0].Rows[0][0].ToString() == "0")
                        {
                            //RESET ALL DATA AND 
                            //ALERT USER THAT NO UPLINE-ID EXISTS FOR THE CURRENT DISTRIBUTOR
                            //[NO STATE-CONFLICT IN DATABASE]
                            MessageBox.Show(Common.GetMessage("VAL0074"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else if (ds.Tables[0].Rows[0][0].ToString() == "-1")
                        {
                            //TODO: UPLINEID EXISTS, BUT NO RECORD FOUND
                            //[STATE-CONFLICT PRESENT IN DATABASE]
                            MessageBox.Show(Common.GetMessage("VAL0099"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else if (searchCondition == "DOWN")
                    {
                        //CHECK FOR UNAVAILABILITY OF ANY DOWNLINE(S), 
                        //AND IN CASE TRUE THEN ALERT THE USER OF THE SAME
                        if (ds.Tables[0].Rows[0][0].ToString() == "0")
                        {
                            MessageBox.Show(Common.GetMessage("VAL0075"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //Kaushik Debnath
                            ds = objTemp.SearchAllInfoForDistributor(distributorID, " ", " ", " ", " ", ref errMsg);
                            SetResetInfo(ds);
                        }
                    }
                    else
                    {
                        if (NavigatePos == 0)
                            //SHOW DISTRIBUTOR-POPUP SCREEN, SINCE DISTRIBUTOR WAS SEARCHED AND IT RETURNED MULTIPLE ROWS
                            ShowDistributorPopup(ds);
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(searchType))
                    {
                        //RESET ALL DATA AND 
                        //ALERT USER THAT NO RECORD HAS BEEN FOUND FOR THE SEARCH-PARAMETER OF DISTRIBUTOR
                        ResetDistributorObject();
                        ResetDistributorInfo();
                        ResetGridInfo();
                        ResetSuccessInfo();
                        MessageBox.Show(Common.GetMessage("8002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void SetResetInfo(DataSet dsDistributorInfo)
        {
            objLstDistHistoryLog = new List<DistributorEditHistory>();
            SetDistributorObject(dsDistributorInfo);
            if (dsDistributorInfo.Tables[0].Rows.Count > 0)
            {
                SetDistributorInfo();
            }
            else
            {
                ResetDistributorInfo();
            }

            if (dsDistributorInfo.Tables[1].Rows.Count > 0)
            {
                SetDownlinesInfo(dsDistributorInfo);
            }
            else
            {
                dgvDownlines.DataSource = null;
            }

            if (dsDistributorInfo.Tables[2].Rows.Count >= 0)
            {
                SetBonusInfo(dsDistributorInfo);
            }
            else
            {
                dgvBonus.DataSource = null;
            }

            //if (dsDistributorInfo.Tables[3].Rows.Count > 0)
            //{
            //    SetInvoicesInfo(dsDistributorInfo);
            //}
            //else
            //{
            //    dgvInvoice.DataSource = null;
            //}

            //if (dsDistributorInfo.Tables[4].Rows.Count > 0)
            //{
            //    SetAllInvoicesInfo(dsDistributorInfo);
            //}
            //else
            //{
            //    dgvAllInvoices.DataSource = null;
            //}

            if (dsDistributorInfo.Tables[3].Rows.Count > 0)
            {
                SetLastMonthInfo(dsDistributorInfo);
            }
            else
            {
                dgvLastMonth.DataSource = null;
            }

            if (dsDistributorInfo.Tables[4].Rows.Count > 0)
            {
                SetSuccessInfo(dsDistributorInfo);
            }
            else
            {
                ResetSuccessInfo();
            }
            if (dsDistributorInfo.Tables[6].Rows.Count > 0)
            {
                setDistributorPanBankUpdateDate(dsDistributorInfo);
            }
        }

        /// <summary>
        /// Show Distributor-Popup screen
        /// </summary>
        /// <param name="distributorList">Main dataset containing the list of distributors</param>
        private void ShowDistributorPopup(DataSet distributorList)
        {
            NavigatePos = 0;
            frmDistributorModule_Popup distributorPopup = new frmDistributorModule_Popup(distributorList);
            distributorPopup.ShowDialog();

            if (!string.IsNullOrEmpty(distributorPopup.DistributerId))
            {
                SetSearchFields(new string[] { distributorPopup.DistributerId, distributorPopup.FirstName.Trim(), distributorPopup.LastName.Trim() });
                SetAllInfo(distributorPopup.DistributerId, distributorPopup.FirstName.Trim(), distributorPopup.LastName.Trim(), string.Empty, "XACT");
            }
        }

        /// <summary>
        /// Initialize the object of 'DistributorModule' business-class with the master-information of distributor
        /// </summary>
        /// <param name="distributorInfo">Main dataset containing the master-information of the distributor</param>
        private void SetDistributorObject(DataSet distributorInfo)
        {
            if (distributorInfo.Tables[0].Rows.Count == 1)
            {
                if (m_objDistributorMain == null)
                {
                    m_objDistributorMain = new CoreComponent.BusinessObjects.Distributor();
                }

                m_objDistributorMain.DistributorId = Convert.ToInt32(distributorInfo.Tables[0].Rows[0]["DISTRIBUTORID"].ToString());
                m_objDistributorMain.DistributorTitleId = Convert.ToInt32(distributorInfo.Tables[0].Rows[0]["DISTRIBUTORTITLEID"]);
                m_objDistributorMain.DistributorTitle = distributorInfo.Tables[0].Rows[0]["DISTRIBUTORTITLE"].ToString();
                m_objDistributorMain.DistributorFirstName = distributorInfo.Tables[0].Rows[0]["DISTRIBUTORFIRSTNAME"].ToString();
                m_objDistributorMain.DistributorLastName = distributorInfo.Tables[0].Rows[0]["DISTRIBUTORLASTNAME"].ToString();
                m_objDistributorMain.DistributorDOB = Convert.ToDateTime(Validators.IsDateTime(distributorInfo.Tables[0].Rows[0]["DISTRIBUTORDOB"].ToString()) ? distributorInfo.Tables[0].Rows[0]["DISTRIBUTORDOB"].ToString() : Common.DATETIME_NULL.ToString()).ToString();

                m_objDistributorMain.CoDistributorTitleId = Convert.ToInt32(distributorInfo.Tables[0].Rows[0]["CODISTRIBUTORTITLEID"]);
                m_objDistributorMain.CoDistributorTitle = distributorInfo.Tables[0].Rows[0]["CO_DISTRIBUTORTITLE"].ToString();
                m_objDistributorMain.CoDistributorFirstName = distributorInfo.Tables[0].Rows[0]["CO_DISTRIBUTORFIRSTNAME"].ToString();
                m_objDistributorMain.CoDistributorLastName = distributorInfo.Tables[0].Rows[0]["CO_DISTRIBUTORLASTNAME"].ToString();
                m_objDistributorMain.CoDistributorDOB = Convert.ToDateTime(Validators.IsDateTime(distributorInfo.Tables[0].Rows[0]["CO_DISTRIBUTORDOB"].ToString()) ? distributorInfo.Tables[0].Rows[0]["CO_DISTRIBUTORDOB"].ToString() : Common.DATETIME_NULL.ToString()).ToString();

                m_objDistributorMain.DistributorEnrolledOn = Convert.ToDateTime(Validators.IsDateTime(distributorInfo.Tables[0].Rows[0]["DISTRIBUTORREGISTRATIONDATE"].ToString()) ? distributorInfo.Tables[0].Rows[0]["DISTRIBUTORREGISTRATIONDATE"].ToString() : Common.DATETIME_NULL.ToString()).ToString(Common.DTP_DATE_FORMAT);
                m_objDistributorMain.UplineDistributorId = Convert.ToInt32(distributorInfo.Tables[0].Rows[0]["UPLINEDISTRIBUTORID"].ToString());


                m_objDistributorMain.BankCode = Convert.ToInt32(distributorInfo.Tables[0].Rows[0]["BANKID"]);
                m_objDistributorMain.AccountNumber = distributorInfo.Tables[0].Rows[0]["BANK"].ToString();
                m_objDistributorMain.BankBranchCode = distributorInfo.Tables[0].Rows[0]["BankBranchCode"].ToString();

                m_objDistributorMain.Zone = distributorInfo.Tables[0].Rows[0]["ZONE"].ToString();

                m_objDistributorMain.DistributorAddress1 = distributorInfo.Tables[0].Rows[0]["ADDRESS1"].ToString();
                m_objDistributorMain.DistributorAddress2 = distributorInfo.Tables[0].Rows[0]["ADDRESS2"].ToString();
                m_objDistributorMain.DistributorAddress3 = distributorInfo.Tables[0].Rows[0]["ADDRESS3"].ToString();
                m_objDistributorMain.DistributorCountryCode = Convert.ToInt32(distributorInfo.Tables[0].Rows[0]["COUNTRYCODE"]);
                m_objDistributorMain.DistributorStateCode = Convert.ToInt32(distributorInfo.Tables[0].Rows[0]["STATECODE"]);
                m_objDistributorMain.DistributorCityCode = Convert.ToInt32(distributorInfo.Tables[0].Rows[0]["CITYCODE"]);

                m_objDistributorMain.DistributorCity = distributorInfo.Tables[0].Rows[0]["CITY"].ToString();
                m_objDistributorMain.DistributorState = distributorInfo.Tables[0].Rows[0]["STATE"].ToString();
                m_objDistributorMain.DistributorPinCode = distributorInfo.Tables[0].Rows[0]["PINCODE"].ToString();
                m_objDistributorMain.DistributorTeleNumber = distributorInfo.Tables[0].Rows[0]["TELEPHONENUMBER"].ToString();
                m_objDistributorMain.DistributorFaxNumber = distributorInfo.Tables[0].Rows[0]["FAXNUMBER"].ToString();
                m_objDistributorMain.DistributorMobNumber = distributorInfo.Tables[0].Rows[0]["MOBILENUMBER"].ToString();
                m_objDistributorMain.DistributorEMailID = distributorInfo.Tables[0].Rows[0]["EMAILID"].ToString();
                m_objDistributorMain.DistributorPANNumber = distributorInfo.Tables[0].Rows[0]["PANNUMBER"].ToString();

                m_objDistributorMain.SerialNo = distributorInfo.Tables[0].Rows[0]["SERIALNO"].ToString();

                m_objDistributorMain.CreatedBy = distributorInfo.Tables[0].Rows[0]["SAVEDBY"].ToString();
                m_objDistributorMain.CreatedDate = Convert.ToDateTime(Validators.IsDateTime(distributorInfo.Tables[0].Rows[0]["SAVEDDATE"].ToString()) ? distributorInfo.Tables[0].Rows[0]["SAVEDDATE"].ToString() : Common.DTP_DATE_FORMAT.ToString()).ToString(Common.DTP_DATE_FORMAT);

                m_objDistributorMain.Star = distributorInfo.Tables[0].Rows[0]["STAR"].ToString();
                m_objDistributorMain.DirectorGroup = distributorInfo.Tables[0].Rows[0]["DIRECTORGROUP"].ToString();
                m_objDistributorMain.DirectorName = distributorInfo.Tables[0].Rows[0]["DIRECTORNAME"].ToString();

                m_objDistributorMain.Curr_PrevCumPV = Math.Round(Convert.ToDouble(Validators.IsDouble(distributorInfo.Tables[0].Rows[0]["PREVCUMPV"].ToString()) ? distributorInfo.Tables[0].Rows[0]["PREVCUMPV"].ToString() : "0"), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);

                m_objDistributorMain.Curr_ExclPV = Math.Round(Convert.ToDouble(Validators.IsDecimal(distributorInfo.Tables[0].Rows[0]["EXCLPV"].ToString()) ? distributorInfo.Tables[0].Rows[0]["EXCLPV"].ToString() : "0"), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                m_objDistributorMain.Curr_SelfPV = Math.Round(Convert.ToDouble(Validators.IsDecimal(distributorInfo.Tables[0].Rows[0]["SELFPV"].ToString()) ? distributorInfo.Tables[0].Rows[0]["SELFPV"].ToString() : "0"), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                m_objDistributorMain.Curr_GroupPV = Math.Round(Convert.ToDouble(Validators.IsDecimal(distributorInfo.Tables[0].Rows[0]["GROUPPV"].ToString()) ? distributorInfo.Tables[0].Rows[0]["GROUPPV"].ToString() : "0"), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                m_objDistributorMain.Curr_TotalPV = Math.Round(Convert.ToDouble(Validators.IsDecimal(distributorInfo.Tables[0].Rows[0]["TOTALPV"].ToString()) ? distributorInfo.Tables[0].Rows[0]["TOTALPV"].ToString() : "0"), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                m_objDistributorMain.Curr_BonusPercent = Math.Round(Convert.ToDouble(Validators.IsDecimal(distributorInfo.Tables[0].Rows[0]["BONUSPERCENT"].ToString()) ? distributorInfo.Tables[0].Rows[0]["BONUSPERCENT"].ToString() : "0"), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                m_objDistributorMain.Curr_TotalCumPV = Math.Round(Convert.ToDouble(Validators.IsDecimal(distributorInfo.Tables[0].Rows[0]["TOTALCUMPV"].ToString()) ? distributorInfo.Tables[0].Rows[0]["TOTALCUMPV"].ToString() : "0"), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                m_objDistributorMain.Curr_TotalBV = Math.Round(Convert.ToDouble(Validators.IsDecimal(distributorInfo.Tables[0].Rows[0]["TOTALBV"].ToString()) ? distributorInfo.Tables[0].Rows[0]["TOTALBV"].ToString() : "0"), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                m_objDistributorMain.Prev_ExclPV = Math.Round(Convert.ToDouble(Validators.IsDecimal(distributorInfo.Tables[0].Rows[0]["PREVEXCLPV"].ToString()) ? distributorInfo.Tables[0].Rows[0]["PREVEXCLPV"].ToString() : "0"), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                m_objDistributorMain.Prev_SelfPV = Math.Round(Convert.ToDouble(Validators.IsDecimal(distributorInfo.Tables[0].Rows[0]["PREVSELFPV"].ToString()) ? distributorInfo.Tables[0].Rows[0]["PREVSELFPV"].ToString() : "0"), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                m_objDistributorMain.Prev_GroupPV = Math.Round(Convert.ToDouble(Validators.IsDecimal(distributorInfo.Tables[0].Rows[0]["PREVGROUPPV"].ToString()) ? distributorInfo.Tables[0].Rows[0]["PREVGROUPPV"].ToString() : "0"), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                m_objDistributorMain.Prev_TotalPV = Math.Round(Convert.ToDouble(Validators.IsDecimal(distributorInfo.Tables[0].Rows[0]["PREVTOTALPV"].ToString()) ? distributorInfo.Tables[0].Rows[0]["PREVTOTALPV"].ToString() : "0"), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                m_objDistributorMain.Prev_BonusPercent = Math.Round(Convert.ToDouble(Validators.IsDecimal(distributorInfo.Tables[0].Rows[0]["PREVBONUSPERCENT"].ToString()) ? distributorInfo.Tables[0].Rows[0]["PREVBONUSPERCENT"].ToString() : "0"), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                m_objDistributorMain.Prev_TotalBV = Math.Round(Convert.ToDouble(Validators.IsDecimal(distributorInfo.Tables[0].Rows[0]["PREVTOTALBV"].ToString()) ? distributorInfo.Tables[0].Rows[0]["PREVTOTALBV"].ToString() : "0"), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                m_objDistributorMain.Curr_SelfBV = Math.Round(Convert.ToDouble(Validators.IsDecimal(distributorInfo.Tables[0].Rows[0]["SelfBV"].ToString()) ? distributorInfo.Tables[0].Rows[0]["SelfBV"].ToString() : "0"), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                m_objDistributorMain.Prev_SelfBV = Math.Round(Convert.ToDouble(Validators.IsDecimal(distributorInfo.Tables[0].Rows[0]["PREVSelfBV"].ToString()) ? distributorInfo.Tables[0].Rows[0]["PREVSelfBV"].ToString() : "0"), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                m_objDistributorMain.CurrentPrevCummulativePV = Math.Round(Convert.ToDouble(Validators.IsDecimal(distributorInfo.Tables[0].Rows[0]["PrevCumCurrentPV"].ToString()) ? distributorInfo.Tables[0].Rows[0]["PrevCumCurrentPV"].ToString() : "0"), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                m_objDistributorMain.CurrentTotalCummulativePV = Math.Round(Convert.ToDouble(Validators.IsDecimal(distributorInfo.Tables[0].Rows[0]["TotalCumCurrentPV"].ToString()) ? distributorInfo.Tables[0].Rows[0]["TotalCumCurrentPV"].ToString() : "0"), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                m_objDistributorMain.Password = distributorInfo.Tables[0].Rows[0]["PASSWORD"].ToString();
                m_objDistributorMain.DistributorStatus = distributorInfo.Tables[0].Rows[0]["DistributorStatus"].ToString();
                m_objDistributorMain.RefId = distributorInfo.Tables[0].Rows[0]["RefId"].ToString();
                m_objDistributorMain.Remarks = distributorInfo.Tables[0].Rows[0]["Remarks"].ToString() == "" ? "Select" : distributorInfo.Tables[0].Rows[0]["Remarks"].ToString();
                m_objDistributorMain.RegistrationDate = Convert.ToDateTime ( distributorInfo.Tables[0].Rows[0]["DISTRIBUTORREGISTRATIONDATE"].ToString() );
                m_objDistributorMain.SaveON = Convert.ToDateTime(Validators.IsDateTime(distributorInfo.Tables[0].Rows[0]["SaveON"].ToString()) ? distributorInfo.Tables[0].Rows[0]["SaveOn"].ToString() : Common.DATETIME_NULL.ToString()).ToString(Common.DTP_DATE_FORMAT);
                m_objDistributorMain.RegistrationLocation = distributorInfo.Tables[0].Rows[0]["LocationCode"].ToString();
               // m_objDistributorMain.forSkinCareItem =Convert.ToBoolean(  distributorInfo.Tables[0].Rows[0]["ForSkinCareItem"].ToString()); 
                if (m_objDistributorMain.DistributorPANNumber == "REJECT" && m_objDistributorMain.AccountNumber == "REJECT")
                {
                    m_objDistributorMain.DistributorDocumentFlag = 4;
                }
                else if (m_objDistributorMain.AccountNumber == "REJECT")
                {
                    m_objDistributorMain.DistributorDocumentFlag = 3;
                }
                else if (m_objDistributorMain.DistributorPANNumber == "REJECT" )
                {
                    m_objDistributorMain.DistributorDocumentFlag = 2; 
                }
            }
        }

        /// <summary>
        /// Bind All ComboBox on screen
        /// </summary>
        private void BindComboBox()
        {
            DataTable dtTitles = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter("TITLE", 0, 0, 0));
            DataTable dtCoTitles = dtTitles.Copy();

            cmbDistribTitle.DataSource = dtTitles;
            cmbDistribTitle.DisplayMember = "keyvalue1";
            cmbDistribTitle.ValueMember = "keycode1";

            cmbCoDistribTitle.DataSource = dtCoTitles;
            cmbCoDistribTitle.DisplayMember = "keyvalue1";
            cmbCoDistribTitle.ValueMember = "keycode1";


            DataTable dtCountry = Common.ParameterLookup(Common.ParameterType.Country, new ParameterFilter("", 0, 0, 0));
            cmbCountry.DataSource = dtCountry;
            cmbCountry.DisplayMember = "CountryName";
            cmbCountry.ValueMember = "CountryId";


            DataTable dtBanks = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter("BANKID", 0, 0, 0));
            cmbBank.DataSource = dtBanks;
            cmbBank.DisplayMember = "keyvalue1";
            cmbBank.ValueMember = "keycode1";

            DataTable dtRemarks = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter("DISTREMARKS", 0, 0, 0));
            cmbRemarks.DataSource = dtRemarks;
            cmbRemarks.DisplayMember = "keyvalue1";
            cmbRemarks.ValueMember = "keycode1";

            

        }

        /// <summary>
        /// Set/Show data in Distributor screen
        /// </summary>
        private void SetDistributorInfo()
        {
            if (m_objDistributorMain != null)
            {
                SetSearchFields(new string[] { m_objDistributorMain.DistributorId.ToString(), m_objDistributorMain.DistributorFirstName, m_objDistributorMain.DistributorLastName });
                SetSuccessHeaderInfo(new string[] { m_objDistributorMain.DistributorTitle, m_objDistributorMain.DistributorFirstName, m_objDistributorMain.DistributorLastName });

                SetDistributorNavigation(m_objDistributorMain.DistributorId);

                txtDistributorIdTab1.Text = m_objDistributorMain.DistributorId.ToString();
                txtDistributorNameTab1.Text = m_objDistributorMain.DistributorTitle.Trim() + " " + m_objDistributorMain.DistributorFirstName.Trim() + " " + m_objDistributorMain.DistributorLastName.Trim();
                //
                cmbDistribTitle.SelectedValue = m_objDistributorMain.DistributorTitleId;

                //txtDistribTitle.Text = m_objDistributorMain.DistributorTitle;
                //
                txtDistribFName.Text = m_objDistributorMain.DistributorFirstName;
                txtDistribLName.Text = m_objDistributorMain.DistributorLastName;
                //txtDistribDOB.Text = m_objDistributorMain.DistributorDOB;
                dtpDistributorDOB.Value = Convert.ToDateTime(m_objDistributorMain.DistributorDOB);
                cmbCoDistribTitle.SelectedValue = m_objDistributorMain.CoDistributorTitleId;
                //txtCoDistribTitle.Text = m_objDistributorMain.CoDistributorTitle;
                txtCoDistribFName.Text = m_objDistributorMain.CoDistributorFirstName;
                txtCoDistribLName.Text = m_objDistributorMain.CoDistributorLastName;
                //txtCoDistribDOB.Text = m_objDistributorMain.CoDistributorDOB;
                dtpCoDistributorDOB.Value = Convert.ToDateTime(m_objDistributorMain.CoDistributorDOB);
                txtPassword.Text = m_objDistributorMain.Password;

                txtDistribRegDate.Text = m_objDistributorMain.DistributorEnrolledOn;
                txtDistribUplineNo.Text = m_objDistributorMain.UplineDistributorId.ToString();

                cmbBank.SelectedValue = m_objDistributorMain.BankCode;
                m_SelectedBank = m_objDistributorMain.BankCode;
                txtAccountNo.Text = m_objDistributorMain.AccountNumber == "-1" ? "" : m_objDistributorMain.AccountNumber;
                m_AccountNo = m_objDistributorMain.AccountNumber;
                txtBankBranchCode.Text = m_objDistributorMain.BankBranchCode=="-1" ? "" :m_objDistributorMain.BankBranchCode;
                SetBankBranchCode();
                m_BankBranchCode = m_objDistributorMain.BankBranchCode;
                txtZone.Text = m_objDistributorMain.Zone;
                lblRefId.Text = m_objDistributorMain.RefId;
                cmbRemarks.Text = m_objDistributorMain.Remarks; 
                if (!Common.CheckIfDistributorAddHidden(m_objDistributorMain.DistributorId))
                {
                    txtAddress01.Text = m_objDistributorMain.DistributorAddress1;
                    txtAddress02.Text = m_objDistributorMain.DistributorAddress2;
                    txtAddress03.Text = m_objDistributorMain.DistributorAddress3;
                }

                cmbCountry.SelectedValue = m_objDistributorMain.DistributorCountryCode;
                cmbState.SelectedValue = m_objDistributorMain.DistributorStateCode;
                cmbCity.SelectedValue = m_objDistributorMain.DistributorCityCode;

                txtSerialNo.Text = m_objDistributorMain.SerialNo;

                //txtCity.Text = m_objDistributorMain.DistributorCity;
                //txtState.Text = m_objDistributorMain.DistributorState;
                txtPinCode.Text = m_objDistributorMain.DistributorPinCode.ToString();
                txtPhoneNo.Text = m_objDistributorMain.DistributorTeleNumber;
                txtFaxNo.Text = m_objDistributorMain.DistributorFaxNumber;
                txtMobileNo.Text = m_objDistributorMain.DistributorMobNumber;
                txtEmailID.Text = m_objDistributorMain.DistributorEMailID;
                txtPANNo.Text = m_objDistributorMain.DistributorPANNumber;


                txtSavedBy.Text = m_objDistributorMain.CreatedBy;
                txtSavedOn.Text = m_objDistributorMain.CreatedDate;

                txtStar.Text = m_objDistributorMain.Star;
                txtDirGroup.Text = m_objDistributorMain.DirectorGroup;
                txtDirName.Text = m_objDistributorMain.DirectorName;

                txtCurrPrevCumPV.Text = m_objDistributorMain.DisplayCurr_PrevCumPV.ToString();

                txtCurrExclPV.Text = m_objDistributorMain.DisplayCurr_ExclPV.ToString();
                txtCurrSelfPV.Text = m_objDistributorMain.DisplayCurr_SelfBV.ToString();
                txtCurrGroupPV.Text = m_objDistributorMain.DisplayCurr_GroupPV.ToString();
                txtCurrTotalPV.Text = m_objDistributorMain.DisplayCurr_TotalPV.ToString();
                txtCurrBonusPercent.Text = m_objDistributorMain.DisplayCurr_BonusPercent.ToString();
                txtCurrTotalCumPV.Text = m_objDistributorMain.DisplayCurr_TotalCumPV.ToString();
                txtCurrTotalBV.Text = m_objDistributorMain.DisplayCurr_TotalBV.ToString();
                txtPrevExclPV.Text = m_objDistributorMain.DisplayPrev_ExclPV.ToString();
                txtPrevSelfPV.Text = m_objDistributorMain.DisplayPrev_SelfBV.ToString();
                txtPrevGroupPV.Text = m_objDistributorMain.DisplayPrev_GroupPV.ToString();
                txtPrevTotalPV.Text = m_objDistributorMain.DisplayPrev_TotalPV.ToString();
                txtPrevBonusPercent.Text = m_objDistributorMain.DisplayPrev_BonusPercent.ToString();
                txtPrevTotalBV.Text = m_objDistributorMain.DisplayPrev_TotalBV.ToString();

                txtCurrentPrvCumPv.Text = m_objDistributorMain.CurrentPrevCummulativePV.ToString();
                txtCurrentTotalCumPv.Text = m_objDistributorMain.CurrentTotalCummulativePV.ToString();
                setDocRejectFlag(m_objDistributorMain.DistributorDocumentFlag);
                txtModifiedDate.Text = m_objDistributorMain.SaveON;
                chkSF9.Checked = m_objDistributorMain.forSkinCareItem; 
                if (m_objDistributorMain.DistributorStatus == ((int)Common.DistributorStatusenum.Terminated).ToString())
                //if (m_objDistributorMain.DistributorStatus == "3")
                {
                    lblTerminated.Visible = true;

                }
                else
                {
                    lblTerminated.Visible = false;
                }

                if (Common.LocationCode.ToUpper() == "HO" && m_objDistributorMain.DistributorStatus != ((int)Common.DistributorStatusenum.Terminated).ToString())
                {
                    if(txtPANNo.Text.Trim().Length >0 && chkPan.Checked != true )
                        btnPANDetails.Enabled = true;
                    else
                        btnPANDetails.Enabled = false;
                    if (txtAccountNo.Text.Trim().Length > 0 && chkBank.Checked != true)
                        btnBankDetails.Enabled = true;
                    else
                        btnBankDetails.Enabled = false;
                }
                else
                {
                    btnPANDetails.Enabled = false;
                    btnBankDetails.Enabled = false;
                }

            }
            else
            {
                ResetDistributorInfo();
            }
        }

        /// <summary>
        /// Set data in Search-Fields; namely Distributor-Id, First-Name,  Last-Name
        /// </summary>
        /// <param name="fillParams">string-array containing: param1 -> Distributor Id, param2 -> First Name, param3 -> Last Name; in the same order</param>
        private void SetSearchFields(string[] fillParams)
        {
            txtSearchDistribID.Text = fillParams[0];
            txtSearchDistribFName.Text = fillParams[1];
            txtSearchDistribLName.Text = fillParams[2];
        }

        /// <summary>
        /// Set/Show data in Downlines screen
        /// </summary>
        /// <param name="distributorInfo">Main dataset containing performance-information of the downline(s) of distributor</param>
        private void SetDownlinesInfo(DataSet distributorInfo)
        {
            if (distributorInfo.Tables.Count > 1)
            {
                dgvDownlines.DataSource = null;
                if (distributorInfo.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow drRow in distributorInfo.Tables[1].Rows)
                    {
                        drRow["EXCLPV"] = Math.Round(Convert.ToDouble(drRow["EXCLPV"].ToString()), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                        drRow["SELFPV"] = Math.Round(Convert.ToDouble(drRow["SELFPV"].ToString()), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                        drRow["GROUPPV"] = Math.Round(Convert.ToDouble(drRow["GROUPPV"].ToString()), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                        drRow["TOTALPV"] = Math.Round(Convert.ToDouble(drRow["TOTALPV"].ToString()), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                        drRow["PERFORMANCEBONUS"] = Math.Round(Convert.ToDouble(drRow["PERFORMANCEBONUS"].ToString()), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                        drRow["BONUSPERCENT"] = Math.Round(Convert.ToDouble(drRow["BONUSPERCENT"].ToString()), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                        drRow["TOTALCUMPV"] = Math.Round(Convert.ToDouble(drRow["TOTALCUMPV"].ToString()), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                        drRow["TOTALCUMBV"] = Math.Round(Convert.ToDouble(drRow["TOTALCUMBV"].ToString()), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                    }
                    dgvDownlines.DataSource = distributorInfo.Tables[1];
                }
            }
        }

        /// <summary>
        /// Set/Show data in Bonus screen
        /// </summary>
        /// <param name="bonusInfo">Main dataset containing Bonus-related information of the distributor</param>
        private void SetBonusInfo(DataSet bonusInfo)
        {
            if (bonusInfo.Tables.Count > 2)
            {
                dgvBonus.DataSource = null;
                {
                    //foreach (DataRow drRow in bonusInfo.Tables[2].Rows)
                    //{
                    //    drRow["ExclPV"] = Math.Round(Convert.ToDecimal(drRow["ExclPV"].ToString()), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                    //    drRow["SelfBV"] = Math.Round(Convert.ToDecimal(drRow["SelfBV"].ToString()), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                    //    drRow["Pcent"] = Math.Round(Convert.ToDecimal(drRow["Pcent"].ToString()), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                    //    drRow["Payable"] = Math.Round(Convert.ToDecimal(drRow["Payable"].ToString()), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                    //    drRow["TDSAmt"] = Math.Round(Convert.ToDecimal(drRow["TDSAmt"].ToString()), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                    //    drRow["NetPayable"] = Math.Round(Convert.ToDecimal(drRow["NetPayable"].ToString()), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                    //    drRow["BalCF"] = Math.Round(Convert.ToDecimal(drRow["BalCF"].ToString()), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                    //}
                    dgvBonus.DataSource = null;
                    dgvBonus.AutoGenerateColumns = true;
                    dgvBonus.DataSource = bonusInfo.Tables[2];
                    FormatGrid(dgvBonus);
                }
            }
        }

        private void FormatGrid(DataGridView dgv)
        {
            dgv.EnableHeadersVisualStyles = false;
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            //GET FROM Config
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(233)))), ((int)(((byte)(251)))));
            dgv.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;

            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Verdana", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;

            dgv.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(233)))), ((int)(((byte)(251)))));
            dgv.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;


            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dgv.DefaultCellStyle = dataGridViewCellStyle3;

            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        /// <summary>
        /// Set/Show data in Invoices screen
        /// </summary>
        /// <param name="invoicesInfo">Main dataset containing data for current-month invoice(s) of the distributor</param>
        //private void SetInvoicesInfo(DataSet invoicesInfo)
        //{
        //    if (invoicesInfo.Tables.Count > 3)
        //    {
        //        dgvInvoice.DataSource = null;
        //        if (invoicesInfo.Tables[3].Rows.Count > 0)
        //        {
        //            invoicesInfo.Tables[3].Columns.Add(new DataColumn("INVOICEDATECUSTOM", typeof(string)));
        //            invoicesInfo.Tables[3].Columns.Add(new DataColumn("INVOICECONSIDERDATECUSTOM", typeof(string)));
        //            foreach (DataRow drRow in invoicesInfo.Tables[3].Rows)
        //            {
        //                drRow["QUANTITY"] = Math.Round(Convert.ToDouble(drRow["QUANTITY"].ToString()), Common.DisplayQtyRounding, MidpointRounding.AwayFromZero);
        //                drRow["TOTALPV"] = Math.Round(Convert.ToDouble(drRow["TOTALPV"].ToString()), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
        //                drRow["TOTALBV"] = Math.Round(Convert.ToDouble(drRow["TOTALBV"].ToString()), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
        //                drRow["INVOICEDATECUSTOM"] = Convert.ToDateTime(drRow["INVOICEDATE"].ToString()).ToString(Commofn.DTP_DATE_FORMAT);
        //                drRow["INVOICECONSIDERDATECUSTOM"] = Convert.ToDateTime(drRow["INVOICECONSIDERDATE"].ToString()).ToString(Common.DTP_DATE_FORMAT);
        //                drRow["INVOICEAMOUNT"] = Math.Round(Convert.ToDouble(drRow["INVOICEAMOUNT"].ToString()), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
        //                drRow["PAIDAMOUNT"] = Math.Round(Convert.ToDouble(drRow["PAIDAMOUNT"].ToString()), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
        //                drRow["CHANGEAMOUNT"] = Math.Round(Convert.ToDouble(drRow["CHANGEAMOUNT"].ToString()), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
        //            }
        //            dgvInvoice.DataSource = invoicesInfo.Tables[3];                    
        //        }
        //        else
        //        {
        //            dgvInvoice.DataSource = null;
        //        }
        //    }
        //}

        /// <summary>
        /// Set/Show data in All-Invoices screen
        /// </summary>
        /// <param name="allInvoicesInfo">Main dataset containing all-invoices for the distributor</param>
        //private void SetAllInvoicesInfo(DataSet allInvoicesInfo)
        //{
        //    if (allInvoicesInfo.Tables.Count > 4)
        //    {
        //        dgvAllInvoices.DataSource = null;
        //        if (allInvoicesInfo.Tables[4].Rows.Count > 0)
        //        {
        //            allInvoicesInfo.Tables[4].Columns.Add(new DataColumn("INVOICEDATECUSTOM", typeof(string)));
        //            allInvoicesInfo.Tables[4].Columns.Add(new DataColumn("INVOICECONSIDERDATECUSTOM", typeof(string)));
        //            foreach (DataRow drRow in allInvoicesInfo.Tables[4].Rows)
        //            {
        //                drRow["QUANTITY"] = Math.Round(Convert.ToDouble(drRow["QUANTITY"].ToString()), Common.DisplayQtyRounding, MidpointRounding.AwayFromZero);
        //                drRow["TOTALBP"] = Math.Round(Convert.ToDouble(drRow["TOTALBP"].ToString()), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
        //                drRow["TOTALBV"] = Math.Round(Convert.ToDouble(drRow["TOTALBV"].ToString()), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
        //                drRow["INVOICEDATECUSTOM"] = Convert.ToDateTime(drRow["INVOICEDATE"].ToString()).ToString(Common.DTP_DATE_FORMAT);
        //                drRow["INVOICECONSIDERDATECUSTOM"] = Convert.ToDateTime(drRow["INVOICECONSIDERDATE"].ToString()).ToString(Common.DTP_DATE_FORMAT);
        //                drRow["INVOICEAMOUNT"] = Math.Round(Convert.ToDouble(drRow["INVOICEAMOUNT"].ToString()), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
        //                drRow["PAIDAMOUNT"] = Math.Round(Convert.ToDouble(drRow["PAIDAMOUNT"].ToString()), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
        //                drRow["CHANGEAMOUNT"] = Math.Round(Convert.ToDouble(drRow["CHANGEAMOUNT"].ToString()), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
        //            }
        //            dgvAllInvoices.DataSource = allInvoicesInfo.Tables[4];                    
        //        }
        //        else
        //        {
        //            dgvAllInvoices.DataSource = null;
        //        }
        //    }
        //}

        /// <summary>
        /// Set/Show data in Last-Month screen
        /// </summary>
        /// <param name="lastMonthInfo">Main dataset containing the last-month perfomance of the downline(s) of the distributor</param>
        private void SetLastMonthInfo(DataSet lastMonthInfo)
        {
            if (lastMonthInfo.Tables.Count > 3)
            {
                dgvLastMonth.DataSource = null;
                if (lastMonthInfo.Tables[3].Rows.Count > 0)
                {
                    lastMonthInfo.Tables[3].Columns.Add(new DataColumn("BUSINESSDATECUSTOM", typeof(string)));
                    foreach (DataRow drRow in lastMonthInfo.Tables[3].Rows)
                    {
                        drRow["EXCLPV"] = Math.Round(Convert.ToDouble(drRow["EXCLPV"].ToString()), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                        drRow["SELFPV"] = Math.Round(Convert.ToDouble(drRow["SELFPV"].ToString()), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                        drRow["TOTALPV"] = Math.Round(Convert.ToDouble(drRow["TOTALPV"].ToString()), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                        drRow["BUSINESSDATECUSTOM"] = Convert.ToDateTime(drRow["BUSINESSDATE"].ToString()).ToString(Common.DTP_DATE_FORMAT);
                        drRow["BONUSPERCENT"] = Math.Round(Convert.ToDouble(drRow["BONUSPERCENT"].ToString()), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                        drRow["PERFORMANCEBONUS"] = Math.Round(Convert.ToDouble(drRow["PERFORMANCEBONUS"].ToString()), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                    }
                    dgvLastMonth.DataSource = lastMonthInfo.Tables[3];
                }
                else
                {
                    dgvLastMonth.DataSource = null;
                }
            }
        }

        /// <summary>
        /// Set/Show the data in Success screen's header portion; related to Distributor Title, First Name, Last Name
        /// </summary>
        /// <param name="distributorInitials"></param>
        private void SetSuccessHeaderInfo(string[] distributorInitials)
        {
            txtDistributorTitle.Text = distributorInitials[0];
            txtDistributorFName.Text = distributorInitials[1];
            txtDistributorLName.Text = distributorInitials[2];
        }

        /// <summary>
        /// Set/Show data in Success screen
        /// </summary>
        /// <param name="successInfo">Main dataset containing the success-table for the distributor</param>
        private void SetSuccessInfo(DataSet successInfo)
        {
            if (successInfo.Tables.Count > 5)
            {
                if (successInfo.Tables[4].Rows.Count > 0)
                {
                    successInfo.Tables[4].Columns.Add(new DataColumn("LEVELDATECUSTOM", typeof(string)));
                    successInfo.Tables[4].Columns.Add(new DataColumn("ACHIEVEDDATECUSTOM", typeof(string)));
                    DataTable dtMainTemp = successInfo.Tables[4].Copy();

                    foreach (DataRow drRow in successInfo.Tables[4].Rows)
                    {
                        if (!string.IsNullOrEmpty(drRow["ACHIEVEDDATE"].ToString()))
                        {
                            drRow["ACHIEVEDDATECUSTOM"] = Convert.ToDateTime(drRow["ACHIEVEDDATE"].ToString()).ToString(Common.DTP_DATE_FORMAT);
                            drRow["LEVELDATECUSTOM"] = Convert.ToDateTime(drRow["ACHIEVEDDATE"].ToString()).ToString(Common.DTP_DATE_FORMAT);
                        }
                    }
                    if (successInfo.Tables[5].Rows.Count > 0)
                    {
                        successInfo.Tables[5].Columns.Add(new DataColumn("LEVELDATECUSTOM", typeof(string)));
                        successInfo.Tables[5].Columns.Add(new DataColumn("ACHIEVEDDATECUSTOM", typeof(string)));
                        foreach (DataRow drRow in successInfo.Tables[5].Rows)
                        {
                            if (!string.IsNullOrEmpty(drRow["ACHIEVEDDATE"].ToString()))
                            {
                                drRow["ACHIEVEDDATECUSTOM"] = Convert.ToDateTime(drRow["ACHIEVEDDATE"].ToString()).ToString(Common.DTP_DATE_FORMAT);
                                drRow["LEVELDATECUSTOM"] = Convert.ToDateTime(drRow["ACHIEVEDDATE"].ToString()).ToString(Common.DTP_DATE_FORMAT);
                            }
                        }
                        DataTable dtLevelHistory = successInfo.Tables[5].Copy();
                        dgvLevelHistory.DataSource = dtLevelHistory;

                        for (int count = 0; count <= dgvLevelHistory.Columns.Count - 1; count++)
                        {
                            dgvLevelHistory.Columns[count].SortMode = DataGridViewColumnSortMode.Programmatic;

                        }
                    }

                    DataTable dtTemp = successInfo.Tables[4].Copy();
                    dtTemp.DefaultView.Sort = "ACHIEVEDDATE DESC";
                    txtAcutalTitle.Text = dtTemp.DefaultView[0]["LEVELNAME"].ToString();

                    foreach (DataRow drRow in dtMainTemp.Rows)
                    {
                        if (!string.IsNullOrEmpty(drRow["ACHIEVEDDATE"].ToString()))
                        {
                            int differenceInMonths = Convert.ToInt32(drRow["MONTHDIFF"].ToString());

                            switch (Convert.ToInt32(drRow["MONTHDIFF"].ToString()))
                            {
                                case 12:
                                    {
                                        txtLevel_Month12.Text = drRow["LEVELNAME"].ToString();
                                        txtPcentMonth12.Text = drRow["BONUSPERCENT"].ToString();
                                        txtLevelNo_Month12.Text = drRow["LEVELID"].ToString();
                                    }
                                    break;

                                case 11:
                                    {
                                        txtLevel_Month11.Text = drRow["LEVELNAME"].ToString();
                                        txtPcentMonth11.Text = drRow["BONUSPERCENT"].ToString();
                                        txtLevelNo_Month11.Text = drRow["LEVELID"].ToString();
                                    }
                                    break;

                                case 10:
                                    {
                                        txtLevel_Month10.Text = drRow["LEVELNAME"].ToString();
                                        txtPcentMonth10.Text = drRow["BONUSPERCENT"].ToString();
                                        txtLevelNo_Month10.Text = drRow["LEVELID"].ToString();
                                    }
                                    break;

                                case 9:
                                    {
                                        txtLevel_Month9.Text = drRow["LEVELNAME"].ToString();
                                        txtPcentMonth9.Text = drRow["BONUSPERCENT"].ToString();
                                        txtLevelNo_Month9.Text = drRow["LEVELID"].ToString();
                                    }
                                    break;

                                case 8:
                                    {
                                        txtLevel_Month8.Text = drRow["LEVELNAME"].ToString();
                                        txtPcentMonth8.Text = drRow["BONUSPERCENT"].ToString();
                                        txtLevelNo_Month8.Text = drRow["LEVELID"].ToString();
                                    }
                                    break;

                                case 7:
                                    {
                                        txtLevel_Month7.Text = drRow["LEVELNAME"].ToString();
                                        txtPcentMonth7.Text = drRow["BONUSPERCENT"].ToString();
                                        txtLevelNo_Month7.Text = drRow["LEVELID"].ToString();
                                    }
                                    break;

                                case 6:
                                    {
                                        txtLevel_Month6.Text = drRow["LEVELNAME"].ToString();
                                        txtPcentMonth6.Text = drRow["BONUSPERCENT"].ToString();
                                        txtLevelNo_Month6.Text = drRow["LEVELID"].ToString();
                                    }
                                    break;

                                case 5:
                                    {
                                        txtLevel_Month5.Text = drRow["LEVELNAME"].ToString();
                                        txtPcentMonth5.Text = drRow["BONUSPERCENT"].ToString();
                                        txtLevelNo_Month5.Text = drRow["LEVELID"].ToString();
                                    }
                                    break;

                                case 4:
                                    {
                                        txtLevel_Month4.Text = drRow["LEVELNAME"].ToString();
                                        txtPcentMonth4.Text = drRow["BONUSPERCENT"].ToString();
                                        txtLevelNo_Month4.Text = drRow["LEVELID"].ToString();
                                    }
                                    break;

                                case 3:
                                    {
                                        txtLevel_Month3.Text = drRow["LEVELNAME"].ToString();
                                        txtPcentMonth3.Text = drRow["BONUSPERCENT"].ToString();
                                        txtLevelNo_Month3.Text = drRow["LEVELID"].ToString();
                                    }
                                    break;

                                case 2:
                                    {
                                        txtLevel_Month2.Text = drRow["LEVELNAME"].ToString();
                                        txtPcentMonth2.Text = drRow["BONUSPERCENT"].ToString();
                                        txtLevelNo_Month2.Text = drRow["LEVELID"].ToString();
                                    }
                                    break;

                                case 1:
                                    {
                                        txtLevel_Month1.Text = drRow["LEVELNAME"].ToString();
                                        txtPcentMonth1.Text = drRow["BONUSPERCENT"].ToString();
                                        txtLevelNo_Month1.Text = drRow["LEVELID"].ToString();
                                    }
                                    break;
                            }
                        }
                    }
                }
                else
                {
                    dgvLevelHistory.DataSource = null;
                }
            }
        }

        /// <summary>
        /// Reset/Clears all data/information from all screens
        /// </summary>
        private void ResetAllInfo()
        {
            //DialogResult resetresult = MessageBox.Show(Common.GetMessage("5006"), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //if (resetresult == DialogResult.Yes)
            {
                m_objDistributorMain = null;
                ResetSearchFields();
                ResetDistributorInfo();
                ResetGridInfo();
                ResetSuccessInfo();
                txtSearchDistribID.Focus();
                lblTerminated.Visible = false;
                objLstDistHistoryLog = null;
            }
        }

        /// <summary>
        /// Make the reference of the DistributorModule class's object to null
        /// </summary>
        private void ResetDistributorObject()
        {
            m_objDistributorMain = null;
        }

        /// <summary>
        /// Reset/Clears the data from the Search-fields
        /// </summary>
        private void ResetSearchFields()
        {
            txtSearchDistribID.Text = string.Empty;
            txtSearchDistribFName.Text = string.Empty;
            txtSearchDistribLName.Text = string.Empty;
        }

        /// <summary>
        /// Reset/Clears the data from the Distributor screen
        /// </summary>
        private void ResetDistributorInfo()
        {
            foreach (Control ctrl in pnlGridSearch.Controls)
            {
                if (ctrl is GroupBox)
                {
                    ResetGroupBox((GroupBox)ctrl);
                }
                //if (ctrl is GroupBox)
                //{
                //    foreach (Control text in ctrl.Controls)
                //    {
                //        if (text is TextBox)
                //        {
                //            text.Text = string.Empty;
                //        }

                //    }
                //}
                //else
                //{
                //    if (ctrl is TextBox)
                //    {
                //        ctrl.Text = string.Empty;
                //    }
                //}                
            }
            txtDistributorIdTab1.Text = string.Empty;
            txtDistributorNameTab1.Text = string.Empty;
            chkBank.Checked = false;
            chkPan.Checked = false;
            chkSF9.Checked = false;
            lblRefId.Text = string.Empty; 
            DisableFields();
        }

        private void DisableFields()
        {
            btnEdit.Enabled = false;
            btnEdit.Text = "E&dit";
            btnSave.Enabled = false;

            txtDistribFName.ReadOnly = true;
            txtDistribLName.ReadOnly = true;
            cmbDistribTitle.Enabled = false;
            cmbCoDistribTitle.Enabled = false;
            cmbDistribTitle.SelectedValue = Common.INT_DBNULL;
            cmbCoDistribTitle.SelectedValue = Common.INT_DBNULL;
            txtDistribUplineNo.ReadOnly = true;
            cmbCountry.Enabled = false;
            cmbState.Enabled = false;
            cmbCity.Enabled = false;
            cmbCountry.SelectedValue = Common.INT_DBNULL;
            cmbState.SelectedValue = Common.INT_DBNULL;
            cmbCity.SelectedValue = Common.INT_DBNULL;
            //txtDistribTitle.ReadOnly = true;
            //txtCoDistribTitle.ReadOnly = true;
            txtCoDistribFName.ReadOnly = true;
            txtCoDistribLName.ReadOnly = true;
            txtAddress01.ReadOnly = true;
            txtAddress02.ReadOnly = true;
            txtAddress03.ReadOnly = true;
            txtPhoneNo.ReadOnly = true;
            txtMobileNo.ReadOnly = true;
            txtFaxNo.ReadOnly = true;
            txtEmailID.ReadOnly = true;
            txtPinCode.ReadOnly = true;
            txtPANNo.ReadOnly = true;
            dtpDistributorDOB.Enabled = false;
            dtpCoDistributorDOB.Enabled = false;
            dtpDistributorDOB.Value = DateTime.Today;
            dtpCoDistributorDOB.Value = DateTime.Today;
            cmbBank.Enabled = false;
            cmbBank.SelectedValue = Common.INT_DBNULL;
            txtAccountNo.ReadOnly = true;
            txtBankBranchCode.ReadOnly = true;
            txtRemarks.ReadOnly = true;
            cmbRemarks.Enabled = false;
            cmbRemarks.SelectedValue = Common.INT_DBNULL;
            errDistribErrProv.Clear();

            txtSearchDistribID.Enabled = true;
            txtSearchDistribFName.Enabled = true;
            txtSearchDistribLName.Enabled = true;
            btnSearch.Enabled = m_IsSearchAvailable;
            btnSearchUpline.Enabled = m_IsSearchAvailable;
            btnHistory.Enabled = m_IsSearchAvailable;
            btnResignation.Enabled = m_DistributorResignation;
            chkPan.Enabled = false;
            chkBank.Enabled = false;
            chkSF9.Enabled = m_ForSkincareItem ;
            if (txtPANNo.Text.Trim().Length > 0 && chkPan.Checked != true)
            {
                btnPANDetails.Enabled = true;
            }
            else
            {
                btnPANDetails.Enabled = false; 
            }
            if (txtAccountNo.Text.Trim().Length > 0 && chkBank.Checked != true)
            {
                btnBankDetails.Enabled = true;
            }
            else
            {
                btnBankDetails.Enabled = false; 
            }
        }

        private void ResetGroupBox(GroupBox gp)
        {
            foreach (Control ctrl in gp.Controls)
            {
                if (ctrl is TextBox)
                {
                    ctrl.Text = string.Empty;
                }
                else if (ctrl is GroupBox)
                {
                    ResetGroupBox((GroupBox)ctrl);
                }
            }
        }

        /// <summary>
        /// Reset/Clears the data from the Grids of screen Downlines, Bonus, Invoices, All Invoices, Last Month; in the same order
        /// </summary>
        private void ResetGridInfo()
        {
            dgvDownlines.DataSource = null;
            dgvBonus.DataSource = null;
            dgvInvoice.DataSource = null;
            dgvAllInvoices.DataSource = null;
            dgvLastMonth.DataSource = null;
        }

        /// <summary>
        /// Reset/Clears the data from Sucess screen
        /// </summary>
        private void ResetSuccessInfo()
        {
            txtDistributorTitle.Text = string.Empty;
            txtDistributorFName.Text = string.Empty;
            txtDistributorLName.Text = string.Empty;
            dgvLevelHistory.DataSource = null;

            txtAcutalTitle.Text = string.Empty;
            txtValidTill.Text = string.Empty;

            foreach (Control ctrl in grpDistRankInFinYr.Controls)
            {
                if (ctrl is TextBox)
                {
                    ctrl.Text = string.Empty;
                }
            }
        }

        private void UpdateDistributorDetails()
        {
            //setDocumentReject(m_objDistributorMain.DistributorDocumentFlag);
            
            ValidateFields();
            
            StringBuilder sbError = GetErrors();                        
            if (sbError.ToString().Trim().Length == 0)
            {
                Distributor objDistributor = new Distributor();
                
                objDistributor.DistributorId = m_objDistributorMain.DistributorId;
                objDistributor.UplineDistributorId = Convert.ToInt32(txtDistribUplineNo.Text.Trim());
                objDistributor.DistributorTitleId = Convert.ToInt32(cmbDistribTitle.SelectedValue);
                objDistributor.DistributorFirstName = txtDistribFName.Text.Trim();
                objDistributor.DistributorLastName = txtDistribLName.Text.Trim();
                objDistributor.DistributorDOB = dtpDistributorDOB.Value.ToString();
                objDistributor.CoDistributorTitleId = Convert.ToInt32(cmbCoDistribTitle.SelectedValue);
                objDistributor.CoDistributorFirstName = txtCoDistribFName.Text.Trim();
                objDistributor.CoDistributorLastName = txtCoDistribLName.Text.Trim();
                objDistributor.CoDistributorDOB = dtpCoDistributorDOB.Value.ToString();
                objDistributor.DistributorAddress1 = txtAddress01.Text.Trim();
                objDistributor.DistributorAddress2 = txtAddress02.Text.Trim();
                objDistributor.DistributorAddress3 = txtAddress03.Text.Trim();
                objDistributor.DistributorPinCode = txtPinCode.Text.Trim();
                objDistributor.DistributorCountryCode = Convert.ToInt32(cmbCountry.SelectedValue);
                objDistributor.DistributorStateCode = Convert.ToInt32(cmbState.SelectedValue);
                objDistributor.DistributorCityCode = Convert.ToInt32(cmbCity.SelectedValue);
                objDistributor.DistributorTeleNumber = txtPhoneNo.Text.Trim();
                objDistributor.DistributorMobNumber = txtMobileNo.Text.Trim();
                objDistributor.DistributorFaxNumber = txtFaxNo.Text.Trim();
                objDistributor.DistributorEMailID = txtEmailID.Text.Trim();
                objDistributor.BankCode = Convert.ToInt32(cmbBank.SelectedValue);
                objDistributor.AccountNumber = txtAccountNo.Text.Trim().Length > 0 ? txtAccountNo.Text.Trim() : Common.INT_DBNULL.ToString();
                objDistributor.BankBranchCode = txtBankBranchCode.Text.Trim().Length > 0 ? txtBankBranchCode.Text.Trim() : Common.INT_DBNULL.ToString();
                objDistributor.DistributorPANNumber = txtPANNo.Text.Trim();
                objDistributor.ModifiedById = AuthenticationComponent.BusinessObjects.Authenticate.LoggedInUser.UserId;
                objDistributor.PanImage = m_objDistributorMain.PanImage;
                objDistributor.PANType = m_objDistributorMain.PANType;
                objDistributor.BankType = m_objDistributorMain.BankType;
                objDistributor.BankImage = m_objDistributorMain.BankImage;
                objDistributor.forSkinCareItem = chkSF9.Checked; 
                //objDistributor.DistributorDocumentFlag = m_objDistributorMain.DistributorDocumentFlag;
                
 
                string errorMessage = string.Empty;
                bool result = objDistributor.UpdateDistributorInfo(ref errorMessage);
                if (Common.LocationCode.ToUpper() == "HO")
                {
                    if (objLstDistHistoryLog != null)
                    {
                        DistributorEditHistoryLog objDist = new DistributorEditHistoryLog();

                        for (int i = 0; i < objLstDistHistoryLog.Count; i++)
                        {
                            objLstDistHistoryLog[i].Remarks = cmbRemarks.Text;
                        }

                        objDist.UpdateDistributorLOG(objLstDistHistoryLog);
                    }
                }
                DistributorPANBank objDistributorPanBank = new DistributorPANBank();
                bool issuccess;
                if (m_objDistributorMain.PANType == "P" || m_objDistributorMain.BankType == "B")
                {
                    
                    issuccess = objDistributorPanBank.Save(objDistributor.DistributorId.ToString(), "P", objDistributor.DistributorPANNumber, "", "", objDistributor.PanImage, objDistributor.ModifiedById, "Usp_DistPANBankSave", ref errorMessage);
                    
                    issuccess = objDistributorPanBank.Save(objDistributor.DistributorId.ToString(), "B", objDistributor.AccountNumber, objDistributor.BankCode.ToString(), objDistributor.BankBranchCode, objDistributor.BankImage, objDistributor.ModifiedById, "Usp_DistPANBankSave", ref errorMessage);
                }
                
                if (result)
                {
                    string id = (m_objDistributorMain.DistributorId).ToString();
                    MessageBox.Show(Common.GetMessage("INF0220"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    string sms = setSmsMessage();
                    objDistributor.SendSms(m_objDistributorMain,sms);
                    ResetAllInfo();
                    txtSearchDistribID.Text = id;
                    btnSearch_Click(null, null);
                    //SetAllInfo(txtSearchDistribID.Text.Trim(), txtSearchDistribFName.Text.Trim(), txtSearchDistribLName.Text.Trim())
                }
                else
                {
                    MessageBox.Show(errorMessage, Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show(sbError.ToString(), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private string setSmsMessage()
        { 
            string sms ="";
            if (txtPANNo.Text.Trim().Length > 0 && txtAccountNo.Text.Trim().Length >0  && txtBankBranchCode.Text.Trim().Length > 0   &&  chkBank.Checked != true && chkPan.Checked != true)
            {
                sms = Common.GetMessage("INF0252",m_objDistributorMain.DistributorId.ToString()); 
            }
            else if (chkBank.Checked == true && chkPan.Checked != true)
            {
                sms = Common.GetMessage("INF0256", m_objDistributorMain.DistributorId.ToString());
            }
            else if (chkBank.Checked != true && chkPan.Checked == true)
            {
                sms = Common.GetMessage("INF0257", m_objDistributorMain.DistributorId.ToString()); 
            }
            else if (chkBank.Checked == true && chkPan.Checked == true)
            {
                sms = Common.GetMessage("INF0260", m_objDistributorMain.DistributorId.ToString()); 
            }
            else if (txtPANNo.Text.Trim().Length >0 && txtAccountNo.Text.Trim().Length == 0 && txtBankBranchCode.Text.Trim().Length ==0 )
            {
                sms = Common.GetMessage("INF0254", m_objDistributorMain.DistributorId.ToString());
            }
            else if (txtPANNo.Text.Trim().Length ==0 && txtAccountNo.Text.Trim().Length >0 && txtBankBranchCode.Text.Trim().Length > 0)
            {
                sms = Common.GetMessage("INF0253", m_objDistributorMain.DistributorId.ToString());
            }
            else
                sms = "";
            
            
            return sms;
        }

        private bool validateRefID(List<DistributorEditHistory> objDistEditHistory)
        {
            for (int i = 0; i < objDistEditHistory.Count - 1; i++)
            {
                
            }
            return false; 
        }
        /// <summary>
        /// Validate all required fields and set their error messages
        /// </summary>
        private void ValidateFields()
        {
            // Validate cmbDistribTitle
            if (cmbDistribTitle.SelectedIndex <= 0)
            {
                errDistribErrProv.SetError(cmbDistribTitle, Common.GetMessage("VAL0002", lblTitle.Text.Trim()));
            }
            else
            {
                errDistribErrProv.SetError(cmbDistribTitle, string.Empty);
            }

            // Validate txtDistribFName
            if (txtDistribFName.Text.Trim().Length <= 0)
            {
                errDistribErrProv.SetError(txtDistribFName, Common.GetMessage("VAL0001", lblDistributorFName.Text.Trim()));
            }
            else
            {
                errDistribErrProv.SetError(txtDistribFName, string.Empty);
            }
            // Validate txtDistribLName
            //if (txtDistribLName.Text.Trim().Length <= 0)
            //{
            //    errDistribErrProv.SetError(txtDistribLName, Common.GetMessage("VAL0001", lblDistributorLName.Text.Substring(0, lblDistributorLName.Text.Length - 1)));
            //}
            //else
            //{
            //    errDistribErrProv.SetError(txtDistribLName, string.Empty);
            //}

            // Validate cmbCoDistribTitle
            if ((txtCoDistribFName.Text.Trim().Length > 0) && (cmbCoDistribTitle.SelectedIndex <= 0))
            {
                errDistribErrProv.SetError(cmbCoDistribTitle, Common.GetMessage("VAL0002", lblTitle.Text.Trim()));
            }
            else
            {
                errDistribErrProv.SetError(cmbCoDistribTitle, string.Empty);
            }

            //Validate txtAddress01
            /*
            if (txtAddress01.Text.Trim().Length <= 0)
            {
                errDistribErrProv.SetError(txtAddress01, Common.GetMessage("VAL0001", lblAddress.Text.Substring(0, lblAddress.Text.Length - 1)));
            }
            else
            {
                errDistribErrProv.SetError(txtAddress01, string.Empty);
            }
            */
            // Validate txtPhoneNo
            //if (txtPhoneNo.Text.Trim().Length <= 0)
            //{
            //    errDistribErrProv.SetError(txtPhoneNo, Common.GetMessage("VAL0001", lblPhoneNo.Text.Substring(0, lblPhoneNo.Text.Length - 1)));
            //}
            //else
            //{
            //    errDistribErrProv.SetError(txtPhoneNo, string.Empty);
            //}


            //txtPinCode
            //if (txtPinCode.Text.Trim().Length <= 0)
            //{
            //    errDistribErrProv.SetError(txtPinCode, Common.GetMessage("VAL0001", lblPinCode.Text.Substring(0, lblPinCode.Text.Length - 1)));
            //}
            //else
            //{
            //    errDistribErrProv.SetError(txtPinCode, string.Empty);
            //}


            //Validate cmbCountry
            if (cmbCountry.SelectedIndex <= 0)
            {
                errDistribErrProv.SetError(cmbCountry, Common.GetMessage("VAL0002", lblCountry.Text.Substring(0, lblCountry.Text.Length - 1)));
            }
            else
            {
                errDistribErrProv.SetError(cmbCountry, string.Empty);
            }

            //Validate cmbState
            if (cmbState.SelectedIndex <= 0)
            {
                errDistribErrProv.SetError(cmbState, Common.GetMessage("VAL0002", lblState.Text.Substring(0, lblState.Text.Length - 1)));
            }
            else
            {
               errDistribErrProv.SetError(cmbState, string.Empty);
            }

            //Validate cmbCity
            if (cmbCity.SelectedIndex <= 0)
            {
                errDistribErrProv.SetError(cmbCity, Common.GetMessage("VAL0002", lblCity.Text.Substring(0, lblCity.Text.Length - 1)));
            }
            else
            {
                 errDistribErrProv.SetError(cmbCity, string.Empty);
            }

            //Validate cmbBank
            //if ((m_SelectedBank > -1 && cmbBank.SelectedIndex <= 0) || (cmbBank.SelectedIndex == 0 && (txtAccountNo.Text.Trim().Length > 0 || txtBankBranchCode.Text.Trim().Length > 0))&& cmbCountry.SelectedValue != "4")
            //if ((cmbBank.SelectedIndex <= 0) || (cmbBank.SelectedIndex == 0 && (txtAccountNo.Text.Trim().Length > 0 || txtBankBranchCode.Text.Trim().Length > 0)) && cmbCountry.SelectedValue != "4")
            //{
            //    errDistribErrProv.SetError(cmbBank, Common.GetMessage("VAL0002", lblBankType.Text.Substring(0, lblBankType.Text.Length - 1)));
            //}
            //else
            //{
            //    errDistribErrProv.SetError(cmbBank, string.Empty);
            //}

            ////txtAccountNo         
            if (m_SelectedBank > 0 && cmbBank.SelectedIndex == 0 && txtAccountNo.Text.Trim().Length > 0 && cmbCountry.SelectedValue.ToString()  != "4")
            {
                errDistribErrProv.SetError(txtAccountNo, Common.GetMessage("VAL0001", lblAccount.Text.Substring(0, lblAccount.Text.Length - 1)));
            }
            else
            {
                errDistribErrProv.SetError(txtAccountNo, string.Empty);
            }

            
            //if (((txtAccountNo.Text.Trim().Length == 0)) || (cmbBank.SelectedIndex > 0 && txtAccountNo.Text.Trim().Length == 0) && cmbCountry.SelectedValue != "4")
            //{
            //    errDistribErrProv.SetError(txtAccountNo, Common.GetMessage("VAL0001", lblAccount.Text.Substring(0, lblAccount.Text.Length - 1)));
            //}
            //else
            //{
            //    errDistribErrProv.SetError(txtAccountNo, string.Empty);
            //}

            if (txtAccountNo.Text.Trim().Length > 0)
            {
                System.Text.RegularExpressions.Regex objRegex = new System.Text.RegularExpressions.Regex("^[a-zA-Z0-9]*$");                
                if (objRegex.IsMatch(txtAccountNo.Text))
                {
                    errDistribErrProv.SetError(txtAccountNo, string.Empty);
                }
                else
                {
                    errDistribErrProv.SetError(txtAccountNo, Common.GetMessage("VAL0009", lblAccount.Text.Substring(0, lblAccount.Text.Length - 1)));

                }
            }
            if (txtBankBranchCode.Text.Trim().Length > 0 && cmbBank.SelectedIndex > 0 && txtAccountNo.Text.Trim().Length == 0 && cmbCountry.SelectedValue.ToString() != "4")
            {
                errDistribErrProv.SetError(txtAccountNo, Common.GetMessage("VAL0001", lblAccount.Text.Substring(0, lblAccount.Text.Length - 1)));
            }
            else 
            {
                errDistribErrProv.SetError(txtAccountNo, string.Empty);
            }
            
            // txtBankBranchCode            
            //if (((m_AccountNo.Trim().Length > 0) && (txtBankBranchCode.Text.Trim().Length == 0)) || (m_SelectedBank == -1 && cmbBank.SelectedIndex > 0 && txtBankBranchCode.Text.Trim().Length == 0) && cmbCountry.SelectedValue != "4")
            //{
            //    errDistribErrProv.SetError(txtBankBranchCode, Common.GetMessage("VAL0001", lblBankBranchCode.Text.Substring(0, lblBankBranchCode.Text.Length - 1)));
            //}
            //else
            //{
            //     errDistribErrProv.SetError(txtBankBranchCode, string.Empty);
            //}

            if (txtBankBranchCode.Text.Trim().Length > 0 && txtBankBranchCode.Text.Trim().Length != 11 && cmbCountry.SelectedValue.ToString() != "4")
            {
                errDistribErrProv.SetError(txtBankBranchCode, Common.GetMessage("VAL0624", lblBankBranchCode.Text.Trim()));
            }
            else
            {
                errDistribErrProv.SetError(txtBankBranchCode, string.Empty);
            }

            // txtAccountNo 
            if ((txtAccountNo.Text.Trim().Length > 0) && (m_SelectedBank == -1 && cmbBank.SelectedIndex == 0) && (txtBankBranchCode.Text.Trim().Length == 0) && cmbCountry.SelectedValue.ToString() != "4")
            {
                errDistribErrProv.SetError(txtBankBranchCode, Common.GetMessage("VAL0624", lblBankBranchCode.Text.Substring(0, lblBankBranchCode.Text.Length - 1)));
            }
            else
            {
                errDistribErrProv.SetError(txtBankBranchCode, string.Empty);
            }

            if (txtBankBranchCode.Text.Trim().Length >0 && cmbCountry.SelectedValue != "4")
            {
                System.Text.RegularExpressions.Regex objRegex = new System.Text.RegularExpressions.Regex("^[a-zA-Z0-9]*$");
                if (objRegex.IsMatch(txtBankBranchCode.Text))
                {
                    errDistribErrProv.SetError(txtBankBranchCode, string.Empty);
                }
                else
                {
                    errDistribErrProv.SetError(txtBankBranchCode, Common.GetMessage("VAL0009", lblBankBranchCode.Text.Substring(0, lblBankBranchCode.Text.Length - 1)));                   
                }  
            }
            //txtPANNo
            if (txtPANNo.Text.Trim().Length > 0 && chkPan.Checked != true)
            {
                string lastName = txtDistributorLName.Text.Substring(0, 1);
                System.Text.RegularExpressions.Regex objRegex = new System.Text.RegularExpressions.Regex("[A-Z]{3}[P,H,F,A,T,C]{1}[" + lastName + "]{1}\\d{4}[A-Z]{1}");

                //System.Text.RegularExpressions.Regex objRegex = new System.Text.RegularExpressions.Regex("[A-Z]{5}\\d{4}[A-Z]{1}");
                //System.Text.RegularExpressions.Regex objRegex = new System.Text.RegularExpressions.Regex("/^[A-Z]{3}[G|A|F|C|T|H|P]{1}[A-Z]{1}\\d{4}[A-Z]{1}$/;");
                if (!objRegex.IsMatch(txtPANNo.Text) )
                {
                    errDistribErrProv.SetError(txtPANNo, Common.GetMessage("VAL0009", lblPANNo.Text.Substring(0, lblPANNo.Text.Length - 1)));
                }
                else
                {
                    errDistribErrProv.SetError(txtPANNo, string.Empty);
                    
                }
                //errDistribErrProv.SetError(txtPANNo, Common.GetMessage("VAL0001", lblPANNo.Text.Substring(0, lblPANNo.Text.Length - 1)));
            }
            else
            {
                errDistribErrProv.SetError(txtPANNo, string.Empty);
                
            }
            //dtpdistributorDOB
            if (dtpDistributorDOB.Value > DateTime.Today.AddYears(-18))
            {
                errDistribErrProv.SetError(dtpDistributorDOB, Common.GetMessage("40005", lblDOB.Text.Trim()));
            }
            else
            {
                errDistribErrProv.SetError(dtpDistributorDOB, string.Empty);
                
            }

            //dtpCoDistributorDOB
            if ((txtCoDistribFName.Text.Trim().Length > 0) && (dtpCoDistributorDOB.Value > DateTime.Today.AddYears(-18)))
            {
                errDistribErrProv.SetError(dtpCoDistributorDOB, Common.GetMessage("40005", lblDOB.Text.Trim()));
            }
            else
            {
                errDistribErrProv.SetError(dtpCoDistributorDOB, string.Empty);
            }
            validateForPANBANK();
        }
        private void validateForPANBANK()
        {

            //if (cmbBank.SelectedIndex == 0 && txtAccountNo.Text.Trim().Length > 0 && txtBankBranchCode.Text.Trim().Length == 11 && cmbCountry.SelectedValue != "4")
            //{
            //    errDistribErrProv.SetError(txtBankBranchCode, Common.GetMessage("VAL0009", lblBankBranchCode.Text.Substring(0, lblBankBranchCode.Text.Length - 1)));
            //}
           if (txtBankBranchCode.Text.Trim().Length > 0 && txtBankBranchCode.Text.Trim().Length != 11 && cmbCountry.SelectedValue.ToString() != "4" && chkBank.Checked !=true )
            {
                errDistribErrProv.SetError(txtBankBranchCode, Common.GetMessage("VAL0624", lblBankBranchCode.Text.Trim()));
            }
            else if ((txtAccountNo.Text.Trim().Length > 0) && ( cmbBank.SelectedIndex == 0) && (txtBankBranchCode.Text.Trim().Length == 0) && cmbCountry.SelectedValue.ToString()  != "4")
            {
                errDistribErrProv.SetError(txtBankBranchCode, Common.GetMessage("VAL0009", lblBankBranchCode.Text.Substring(0, lblBankBranchCode.Text.Length - 1)));
            }
            else  if (((txtAccountNo.Text.Trim().Length > 0) && (txtBankBranchCode.Text.Trim().Length == 0)) && (m_SelectedBank == -1 && cmbBank.SelectedIndex == 0 && txtBankBranchCode.Text.Trim().Length == 0) && cmbCountry.SelectedValue.ToString()  != "4")
            {
                errDistribErrProv.SetError(txtBankBranchCode, Common.GetMessage("VAL0001", lblBankBranchCode.Text.Substring(0, lblBankBranchCode.Text.Length - 1)));
            }
            else
            {
                errDistribErrProv.SetError(txtBankBranchCode, string.Empty);
            }
            
            /*if (cmbBank.SelectedIndex == 0 && txtAccountNo.Text.Trim().Length > 0 && cmbCountry.SelectedValue != "4")
            {
                errDistribErrProv.SetError(cmbBank , Common.GetMessage("VAL0001", lblBankType.Text.Substring(0, lblBankType.Text.Length - 1)));
            }
            else
            {
                errDistribErrProv.SetError(cmbBank , string.Empty);
            }
            */
            if (cmbBank.SelectedIndex > 0 && txtAccountNo.Text.Trim().Length == 0 && txtBankBranchCode.Text.Trim().Length == 11 && cmbCountry.SelectedValue.ToString()  != "4")
            {
                errDistribErrProv.SetError(txtAccountNo, Common.GetMessage("VAL0001", lblAccount.Text.Substring(0, lblAccount.Text.Length - 1)));
            }
            else if (cmbBank.SelectedIndex == 0 && txtAccountNo.Text.Trim().Length == 0 && txtBankBranchCode.Text.Trim().Length == 11 && cmbCountry.SelectedValue.ToString()  != "4")
            {
                errDistribErrProv.SetError(txtAccountNo, Common.GetMessage("VAL0001", lblAccount.Text.Substring(0, lblAccount.Text.Length - 1)));
            }
            else
            {
                errDistribErrProv.SetError(txtAccountNo, string.Empty);
            }
        }
        /// <summary>
        /// Get Error messages for all req fields
        /// </summary>
        /// <returns></returns>
        private StringBuilder GetErrors()
        {
            StringBuilder sb = new StringBuilder();

            if (errDistribErrProv.GetError(cmbDistribTitle).Length > 0)
            {
                sb.Append(errDistribErrProv.GetError(cmbDistribTitle));
                sb.AppendLine();
            }

            if (errDistribErrProv.GetError(txtDistribFName).Length > 0)
            {
                sb.Append(errDistribErrProv.GetError(txtDistribFName));
                sb.AppendLine();
            }


            if (errDistribErrProv.GetError(txtDistribUplineNo).Length > 0)
            {
                sb.Append(errDistribErrProv.GetError(txtDistribUplineNo));
                sb.AppendLine();
            }

            //if (errDistribErrProv.GetError(txtDistribLName).Length > 0)
            //{
            //    sb.Append(errDistribErrProv.GetError(txtDistribLName));
            //    sb.AppendLine();
            //}

            if (errDistribErrProv.GetError(cmbCoDistribTitle).Length > 0)
            {
                sb.Append(errDistribErrProv.GetError(cmbCoDistribTitle));
                sb.AppendLine();
            }

            if (errDistribErrProv.GetError(txtAddress01).Length > 0)
            {
                sb.Append(errDistribErrProv.GetError(txtAddress01));
                sb.AppendLine();
            }

            //if (errDistribErrProv.GetError(txtPhoneNo).Length > 0)
            //{
            //    sb.Append(errDistribErrProv.GetError(txtPhoneNo));
            //    sb.AppendLine();
            //}

            //if (errDistribErrProv.GetError(txtPinCode).Length > 0)
            //{
            //    sb.Append(errDistribErrProv.GetError(txtPinCode));
            //    sb.AppendLine();
            //}

            if (errDistribErrProv.GetError(cmbCountry).Length > 0)
            {
                sb.Append(errDistribErrProv.GetError(cmbCountry));
                sb.AppendLine();
            }

            if (errDistribErrProv.GetError(cmbState).Length > 0)
            {
                sb.Append(errDistribErrProv.GetError(cmbState));
                sb.AppendLine();
            }

            if (errDistribErrProv.GetError(cmbCity).Length > 0)
            {
                sb.Append(errDistribErrProv.GetError(cmbCity));
                sb.AppendLine();
            }

            if (errDistribErrProv.GetError(cmbBank).Length > 0)
            {
                sb.Append(errDistribErrProv.GetError(cmbBank));
                sb.AppendLine();
            }

            if (errDistribErrProv.GetError(txtAccountNo).Length > 0)
            {
                sb.Append(errDistribErrProv.GetError(txtAccountNo));
                sb.AppendLine();
            }
            if (errDistribErrProv.GetError(txtBankBranchCode).Length > 0)
            {
                sb.Append(errDistribErrProv.GetError(txtBankBranchCode));
                sb.AppendLine();
            }
            if (errDistribErrProv.GetError(txtPANNo).Length > 0)
            {
                sb.Append(errDistribErrProv.GetError(txtPANNo));
                sb.AppendLine();
            }

            if (errDistribErrProv.GetError(dtpDistributorDOB).Length > 0)
            {
                sb.Append(errDistribErrProv.GetError(dtpDistributorDOB));
                sb.AppendLine();
            }

            if (errDistribErrProv.GetError(dtpCoDistributorDOB).Length > 0)
            {
                sb.Append(errDistribErrProv.GetError(dtpCoDistributorDOB));
                sb.AppendLine();
            }

            return Common.ReturnErrorMessage(sb);
            //return sb;
        }

        private void EditDistributorDetails()
        {
            txtSearchDistribID.Enabled = false;
            txtSearchDistribFName.Enabled = false;
            txtSearchDistribLName.Enabled = false;
            btnSearch.Enabled = false;
            btnSearchUpline.Enabled = false;
            //txtDistribFName.ReadOnly = false;
            //txtDistribLName.ReadOnly = false;
            //cmbDistribTitle.Enabled = true;
            //cmbCoDistribTitle.Enabled = true;
            cmbCountry.Enabled = true;
            cmbState.Enabled = true;
            cmbCity.Enabled = true;
            //txtDistribTitle.ReadOnly = false;
            //txtCoDistribTitle.ReadOnly = false;
            //txtCoDistribFName.ReadOnly = false;
            //txtCoDistribLName.ReadOnly = false;
            txtAddress01.ReadOnly = false;
            txtAddress02.ReadOnly = false;
            txtAddress03.ReadOnly = false;
            txtPhoneNo.ReadOnly = false;
            txtMobileNo.ReadOnly = false;
            txtFaxNo.ReadOnly = false;
            txtEmailID.ReadOnly = false;
            txtPinCode.ReadOnly = false;
            dtpDistributorDOB.Enabled = true;
            dtpCoDistributorDOB.Enabled = true;
            chkSF9.Enabled = true;
 
            if (accessDistributorBank())
            {
                cmbBank.Enabled = false;
                txtAccountNo.ReadOnly = false;
                txtBankBranchCode.ReadOnly = false;
                txtDistribFName.ReadOnly = true;
                txtDistribLName.ReadOnly = true;
                txtCoDistribFName.ReadOnly = true;
                txtCoDistribLName.ReadOnly = true;
                cmbDistribTitle.Enabled = false;
                cmbCoDistribTitle.Enabled = false;
                txtDistribUplineNo.ReadOnly = true;
                chkBank.Enabled = true;
            }
            else 
            {
                cmbBank.Enabled = false;
                txtAccountNo.ReadOnly = true;
                txtBankBranchCode.ReadOnly = true;
                txtDistribFName.ReadOnly = true;
                txtDistribLName.ReadOnly = true;
                txtCoDistribFName.ReadOnly = true;
                txtCoDistribLName.ReadOnly = true;
                cmbDistribTitle.Enabled = false;
                cmbCoDistribTitle.Enabled = false;
                txtDistribUplineNo.ReadOnly = true;
                chkBank.Enabled = false;
            }

            if (accessDistributorPan())
            {
                txtPANNo.ReadOnly = false;
                txtDistribFName.ReadOnly = true;
                txtDistribLName.ReadOnly = true;
                txtCoDistribFName.ReadOnly = true;
                txtCoDistribLName.ReadOnly = true;
                cmbDistribTitle.Enabled = false;
                cmbCoDistribTitle.Enabled = false;
                txtDistribUplineNo.ReadOnly = true;
                chkPan.Enabled = true;
            }
            else 
            {
                txtPANNo.ReadOnly = true;
                txtDistribFName.ReadOnly = true;
                txtDistribLName.ReadOnly = true;
                txtCoDistribFName.ReadOnly = true;
                txtCoDistribLName.ReadOnly = true;
                cmbDistribTitle.Enabled = false;
                cmbCoDistribTitle.Enabled = false;
                txtDistribUplineNo.ReadOnly = true;
                chkPan.Enabled = false;
            }

            
            if(Common.LocationCode.ToUpper() == "HO")
            {
                txtPANNo.ReadOnly = false;
                cmbBank.Enabled = false ;
                txtAccountNo.ReadOnly = false;
                txtBankBranchCode.ReadOnly = false;
                txtDistribFName.ReadOnly = false;
                txtDistribLName.ReadOnly = false;
                txtCoDistribFName.ReadOnly = false;
                txtCoDistribLName.ReadOnly = false;
                cmbDistribTitle.Enabled = true;
                cmbCoDistribTitle.Enabled = true;
                txtDistribUplineNo.ReadOnly = false; ;
                chkPan.Enabled = true;
                chkBank.Enabled = true;
            }

            txtRemarks.ReadOnly = false;
            cmbRemarks.Enabled = true;
            // These fields can be edited 
            /*
            txtDistribFName.ReadOnly = !m_UpdateDistributorNameAvailable;
            txtDistribLName.ReadOnly = !m_UpdateDistributorNameAvailable;
            txtCoDistribFName.ReadOnly = !m_UpdateDistributorNameAvailable;
            txtCoDistribLName.ReadOnly = !m_UpdateDistributorNameAvailable;
            cmbDistribTitle.Enabled = m_UpdateDistributorNameAvailable;
            cmbCoDistribTitle.Enabled = m_UpdateDistributorNameAvailable;
            txtDistribUplineNo.ReadOnly = !m_UpdateDistributorNameAvailable;
            */
            btnResignation.Enabled = m_DistributorResignation;
            chkSF9.Enabled = m_ForSkincareItem;
            //grpReject.Enabled = true;
            btnEdit.Text = "&Save";
        }

        #endregion

        private void txtDistribUplineNo_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                errDistribErrProv.SetError(txtDistribUplineNo, string.Empty);
                if (txtDistribUplineNo.ReadOnly == false)
                {
                    if (txtDistribUplineNo.Text.Trim() != string.Empty)
                    {
                        int outDisNumber = 0;
                        if (int.TryParse(txtDistribUplineNo.Text.Trim(), out outDisNumber))
                        {
                            Distributor upline = new Distributor();
                            upline.SDistributorId = txtDistribUplineNo.Text.Trim();
                            upline.DistributorId = Convert.ToInt32(txtDistribUplineNo.Text.Trim());
                            string errorMessage = string.Empty;
                            List<Distributor> dist = upline.SearchDistributor(ref errorMessage);
                            if (dist == null)
                            {
                                errDistribErrProv.SetError(txtDistribUplineNo, Common.GetMessage("40018"));
                                MessageBox.Show(Common.GetMessage("40018"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else if (dist.Count == 0)
                            {
                                errDistribErrProv.SetError(txtDistribUplineNo, Common.GetMessage("40018"));
                                MessageBox.Show(Common.GetMessage("40018"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);

                            }
                            else if (dist.Count == 1)
                            {
                                txtDistribUplineNo.Text = dist[0].DistributorId.ToString().Trim();
                                if (txtDistribUplineNo.Text.Trim() != m_objDistributorMain.DistributorId.ToString())
                                    errDistribErrProv.SetError(txtDistribUplineNo, string.Empty);
                                else
                                    errDistribErrProv.SetError(txtDistribUplineNo, Common.GetMessage("VAL0605"));
                            }
                            else if (dist.Count > 1)
                            {
                                using (DistributorPopup dp = new DistributorPopup(dist))
                                {
                                    Point pointTree = new Point();
                                    pointTree = grpDistMasterInfo.PointToScreen(new Point(650, 38));
                                    pointTree.Y = pointTree.Y + 25;
                                    pointTree.X = pointTree.X + 5;
                                    dp.Location = pointTree;
                                    if (dp.ShowDialog() == DialogResult.OK)
                                    {
                                        txtDistribUplineNo.Text = dp.SelectedDistributor.DistributorId.ToString().Trim();
                                        if (txtDistribUplineNo.Text.Trim() != m_objDistributorMain.DistributorId.ToString())
                                            errDistribErrProv.SetError(txtDistribUplineNo, string.Empty);
                                        else
                                            errDistribErrProv.SetError(txtDistribUplineNo, Common.GetMessage("VAL0605"));
                                    }
                                    else
                                    {
                                        txtDistribUplineNo.Text = string.Empty;
                                        txtDistribUplineNo.Focus();
                                    }
                                }
                            }
                        }
                        else
                        {
                            txtDistribUplineNo.Focus();
                            errDistribErrProv.SetError(txtDistribUplineNo, Common.GetMessage("40018"));
                            MessageBox.Show(Common.GetMessage("40018"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        errDistribErrProv.SetError(txtDistribUplineNo, Common.GetMessage("40018"));
                    }
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
            }
        }

        private void txtCurrTotalCumPV_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvDailyInvoice_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView DgvView = null;
            DataGridViewRow dtRow = null;
            string sCustomerOrder = "";
            DataTable dtTable = null;
            DataRow[] dtRows = null;
            DataSet dsReport = null;
            string sTinNumber = null;
            int iBoid = 0;
            try
            {
                if (e != null && e.Clicks == 2)
                {
                    DgvView = (sender) as DataGridView;
                    dtRow = DgvView.Rows[e.RowIndex];
                    dtTable = DgvView.DataSource as DataTable;
                    iBoid = Common.CurrentLocationId;
                    if (dtTable != null)
                    {
                        dtRows = dtTable.Select("InvoiceNo = '" + dtRow.Cells["InvoiceNo"].Value.ToString() + "'");
                        if (dtRows != null && dtRows.Length > 0)
                        {
                            sCustomerOrder = dtRows[0]["CustomerOrderNo"].ToString();
                            sTinNumber = dtRows[0]["TinNo"].ToString();
                            Common.CurrentLocationId = Convert.ToInt32(dtRows[0]["Boid"].ToString());
                            dsReport = CreatePrintDataSet((int)Common.PrintType.PrintInvoice, sCustomerOrder, sTinNumber);
                            CoreComponent.UI.ReportScreen reportScreenObj = new CoreComponent.UI.ReportScreen((int)Common.ReportType.CustomerInvoice, dsReport);
                            reportScreenObj.rptViewer.ShowPrintButton = false;
                            reportScreenObj.rptViewer.ShowExportButton = false;
                            reportScreenObj.btnPrint.Visible = m_EnableInvoicePrint;
                            reportScreenObj.btnPrint.Enabled = m_EnableInvoicePrint;
                            //reportScreenObj.btnPrint.Enabled = false;
                            reportScreenObj.ShowDialog();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);

            }
            finally
            {
                dsReport = null;
                if (iBoid != 0)
                    Common.CurrentLocationId = iBoid;
            }
        }
        private DataSet CreatePrintDataSet(int type, string CustomerOrderNo, string sTinNumber)
        {

            string errorMessage = string.Empty;
            DataSet ds = GetOrderForPrint(type, CustomerOrderNo, ref errorMessage);
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
                    ds.Tables[0].Rows[0]["Header"] = (type == 1 ? "Customer Order" : "Retail Invoice");
                    ds.Tables[1].Columns.Add(new DataColumn("IsLocation", Type.GetType("System.String")));
                    //ds.Tables[1].Columns.Add(new DataColumn("IsPromoRecord", Type.GetType("System.String")));
                    ds.Tables[0].Rows[0]["TINNo"] = sTinNumber;
                    ds.Tables[0].Rows[0]["PANNo"] = Common.PANNO;


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

        public DataSet GetOrderForPrint(int type, string customerOrderNo, ref string errorMessage)
        {
            try
            {
                DataSet ds = new DataSet();
                using (DataTaskManager dtManager = new DataTaskManager())
                {
                    DBParameterList dbParam = new DBParameterList();
                    dbParam.Add(new DBParameter("@type", type, DbType.Int32));
                    dbParam.Add(new DBParameter("@orderId", customerOrderNo, DbType.String));
                    dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, errorMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                    ds = dtManager.ExecuteDataSet(SP_ORDER_PRINT, dbParam);
                }
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       

        /// <summary>
        /// Set the Navigation among Distribuotrs
        /// </summary>
        private void SetDistributorNavigation(int curDistributorId)
        {
            try
            {
                //string errMsg = string.Empty;

                //Distributor objTemp = new Distributor();
                //DataSet ds = objTemp.SearchDistributorPosition(curDistributorId, ref errMsg);

                //distributorPos = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
                //totalDistributor = Convert.ToInt32(ds.Tables[0].Rows[0][1].ToString());

                //if ((distributorPos > 1) && (distributorPos < totalDistributor))
                //{
                //    btnFirstRecord.Enabled = true;
                //    btnPreviousRecord.Enabled = true;
                //    btnNextRecord.Enabled = true;
                //    btnLastRecord.Enabled = true;
                //}
                //if (distributorPos == 1)
                //{
                //    btnFirstRecord.Enabled = false;
                //    btnPreviousRecord.Enabled = false;
                //    btnNextRecord.Enabled = true;
                //    btnLastRecord.Enabled = true;
                //}
                //if (distributorPos == totalDistributor)
                //{
                //    btnFirstRecord.Enabled = true;
                //    btnPreviousRecord.Enabled = true;
                //    btnNextRecord.Enabled = false;
                //    btnLastRecord.Enabled = false;
                //}
                btnFirstRecord.Enabled = true;
                btnPreviousRecord.Enabled = true;
                btnNextRecord.Enabled = true;
                btnLastRecord.Enabled = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Move to Next Distribuotr according to navigation option selected
        /// </summary>
        private void FindNextDistributorId(int curDistributorId, int navigate)
        {
            try
            {
                string errMsg = string.Empty;
                Distributor objTemp = new Distributor();
                DataSet ds = objTemp.FindNextDistributorId(curDistributorId, navigate, ref errMsg);

                //MessageBox.Show(ds.Tables[0].Rows[0][0].ToString());

                DisableFields();
                SetAllInfo(ds.Tables[0].Rows[0][0].ToString());
                if ((m_objDistributorMain != null) && (m_objDistributorMain.DistributorId > 0))
                {
                    btnEdit.Enabled = m_IsUpdateAvailable;
                }
                else
                {
                    btnEdit.Enabled = false;
                }

                SetDistributorNavigation(Convert.ToInt32(txtSearchDistribID.Text.Trim()));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnFirstRecord_Click(object sender, EventArgs e)
        {
            btnFirstRecord.Enabled = false;
            btnPreviousRecord.Enabled = true;
            btnNextRecord.Enabled = true;
            btnLastRecord.Enabled = true;
            NavigatePos = 1;
            FindNextDistributorId(m_objDistributorMain.DistributorId, NavigatePos);
        }

        private void btnLastRecord_Click(object sender, EventArgs e)
        {
            try
            {
                btnFirstRecord.Enabled = true;
                btnPreviousRecord.Enabled = true;
                btnNextRecord.Enabled = true;
                btnLastRecord.Enabled = false;
                NavigatePos = 4;
                FindNextDistributorId(m_objDistributorMain.DistributorId, NavigatePos);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnNextRecord_Click(object sender, EventArgs e)
        {
            try
            {
                NavigatePos = 3;
                FindNextDistributorId(m_objDistributorMain.DistributorId, NavigatePos);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnPreviousRecord_Click(object sender, EventArgs e)
        {
            try
            {
                NavigatePos = 2;
                FindNextDistributorId(m_objDistributorMain.DistributorId, NavigatePos);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadControlsForMiniBranch()
        {
            lblAddress.Visible = false;
            txtAddress01.Visible = false;
            txtAddress02.Visible = false;
            txtAddress03.Visible = false;
            lblPhoneNo.Visible = false;
            txtPhoneNo.Visible = false;
            lblFaxNo.Visible = false;
            txtFaxNo.Visible = false;
            lblMobileNo.Visible = false;
            txtMobileNo.Visible = false;
            lblEmailID.Visible = false;
            txtEmailID.Visible = false;
            lblAccount.Visible = false;
            txtAccountNo.Visible = false;
            txtBankBranchCode.Visible = false;
            lblBranchName.Visible = false;
            lblPANNo.Visible = false;
            txtPANNo.Visible = false;

            // Location Shifts of controls

            Point LocationLblCityOld = lblCity.Location;
            Point LocationCmbCityOld = cmbCity.Location;

            Point LocationLblStateOld = lblState.Location;
            Point LocationCmbStateOld = cmbState.Location;

            Point LocationLblCountryOld = lblCountry.Location;
            Point LocationCmbCountryOld = cmbCountry.Location;

            lblCountry.Location = new Point(lblAddress.Location.X, lblAddress.Location.Y);
            cmbCountry.Location = new Point(txtAddress01.Location.X, txtAddress01.Location.Y);

            lblState.Location = new Point(lblCountry.Location.X + 10, lblCountry.Location.Y + 25);
            cmbState.Location = new Point(txtAddress02.Location.X , txtAddress02.Location.Y);

            lblCity.Location = new Point(lblState.Location.X + 10, lblState.Location.Y + 25);
            cmbCity.Location = new Point(txtAddress03.Location.X, txtAddress03.Location.Y);

            lblZone.Location = new Point(LocationLblCountryOld.X - 133 + 25 + 3, LocationLblCountryOld.Y);
            txtZone.Location = new Point(LocationCmbCountryOld.X - 150 + 25, LocationCmbCountryOld.Y);

            lblPinCode.Location = new Point(LocationLblStateOld.X - 133 + 25, LocationLblStateOld.Y);
            txtPinCode.Location = new Point(LocationCmbStateOld.X - 150 + 25, LocationCmbStateOld.Y);

            lblBankType.Location = new Point(LocationLblCityOld.X - 150 + 25, LocationLblCityOld.Y);
            cmbBank.Location = new Point(LocationCmbCityOld.X - 150 + 25, LocationCmbCityOld.Y);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_objDistributorMain == null)
                    return;

                if (string.IsNullOrEmpty(txtSearchDistribID.Text))
                {
                    MessageBox.Show(Common.GetMessage("VAL0001", "Distributor-ID"), Common.GetMessage("10001"),
                                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                DataSet dsDistributorDetail = m_objDistributorMain.CreateDatasetForPrint(txtSearchDistribID.Text);
                CoreComponent.UI.ReportScreen reportScreenObj = new ReportScreen((int)Common.ReportType.DistributorDetail, dsDistributorDetail);
                reportScreenObj.ShowDialog();

                dsDistributorDetail = null;
            }
            catch (Exception ex)
            {
                Common.LogException(new Exception(ex.Message.ToString()));
                MessageBox.Show(ex.Message, Common.GetMessage("10001"),
                                         MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected void btnRegignation_Click(object sender, EventArgs e)
        {
            Resign();
        }

        private void Resign()
        {
            try
            {
                #region Validation Code

                StringBuilder sbError = new StringBuilder();
                //sbError = ValidateCarRegistrationData();
                if ((txtSearchDistribID.Text == ""))
                {
                    MessageBox.Show(Common.GetMessage("INF0237"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (m_objDistributorMain.DistributorStatus == ((int)Common.DistributorStatusenum.Terminated).ToString())
                {
                    MessageBox.Show(Common.GetMessage("INF0251"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                #endregion Validation Code

                if (sbError.ToString().Trim().Equals(string.Empty))
                {
                    string msg = string.Empty;
                    if (m_userId > 0)
                        msg = "Edit";
                    else
                        msg = "Save";

                    DialogResult saveResult = MessageBox.Show(Common.GetMessage("5016", msg), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (saveResult == DialogResult.Yes)
                    {

                        DistributorResignation m_DistributorResignation = new DistributorResignation();
                        m_DistributorResignation.DistributorId = Convert.ToInt32(txtSearchDistribID.Text);

                        if (m_userId == 0)
                        {
                            // TO UNCOMMENT once column is created and included in sp
                            //m_objCarRegistration.CreatedBy = Authenticate.LoggedInUser.UserId;
                            //m_objCarRegistration.CreatedDate = Common.DATETIME_CURRENT.ToString(Common.DATE_TIME_FORMAT);
                            //m_objCarRegistration.ModifiedDate = Convert.ToDateTime(new DateTime(1900, 1, 1)).ToString(Common.DATE_TIME_FORMAT);
                        }
                        else //m_userId > 0
                        {
                            //m_objCarRegistration.ModifiedDate = Convert.ToDateTime(m_modifiedDate).ToString(Common.DATE_TIME_FORMAT); ;
                        }

                        string errMsg = string.Empty;
                        bool retVal = m_DistributorResignation.Resign(Convert.ToInt32(m_DistributorResignation.DistributorId), DistributorResignation.DISTRIBUTOR_RESIGNATION,ref errMsg);
                        if (retVal)
                        {
                            MessageBox.Show(Common.GetMessage("INF0250"), Common.GetMessage("10001"),
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                            m_objDistributorMain.SendSms(m_objDistributorMain, Common.GetMessage("INF0261",m_objDistributorMain.DistributorMobNumber));
                            string id = (m_DistributorResignation.DistributorId).ToString();
                            
                            ResetAllInfo();
                            txtSearchDistribID.Text = id;
                            btnSearch_Click(null, null);
                           
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
                Common.LogException(new Exception(ex.Message.ToString()));
                MessageBox.Show(ex.Message, Common.GetMessage("10001"),
                                         MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnAccountHistory_Click(object sender, EventArgs e)
        {
            frmDistributorAccountHistory objDSHistory;
            try
            {
                if (string.IsNullOrEmpty(txtSearchDistribID.Text))
                {
                    //MessageBox.Show("Please enter the distributor id");
                    MessageBox.Show(string.Format(Common.GetMessage("VAL0001"), "distributor id"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                objDSHistory = new frmDistributorAccountHistory();
                objDSHistory.DistributorId = txtSearchDistribID.Text.Trim();
                objDSHistory.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
            finally
            {
                objDSHistory = null;
            }
        }

        private void txtDistribFName_Validated(object sender, EventArgs e)
        {
            //if (!m_VaidateDistributorName.Equals(txtDistribFName.Text + txtDistribLName.Text, StringComparison.CurrentCultureIgnoreCase))
            //{
            //    MessageBox.Show(Common.GetMessage("INF0249"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);                   
            //}
            
            if (m_objDistributorMain == null)
                return;

            DistributorEditHistory objDistrHistoryLog = new DistributorEditHistory();
            if (m_objDistributorMain.DistributorFirstName != txtDistribFName.Text)
            {
                objLstDistHistoryLog.RemoveAll(d => d.FieldName == lblFName.Text );
                objDistrHistoryLog.DistributorId = m_objDistributorMain.DistributorId;
                objDistrHistoryLog.FieldName = lblFName.Text;
                objDistrHistoryLog.OldValue = m_objDistributorMain.DistributorFirstName ;
                objDistrHistoryLog.NewValue = txtDistribFName.Text;
                objLstDistHistoryLog.Add(objDistrHistoryLog);

                setPANBankDetailLog(lblLName.Text, m_objDistributorMain.DistributorLastName, txtDistribLName.Text);
                
                profileFlag = "P";
            }
            DistributorEditHistory objDistrHistoryLog1 = new DistributorEditHistory();
            if (m_objDistributorMain.DistributorLastName != txtDistribLName.Text)
            {
                objLstDistHistoryLog.RemoveAll(d => d.FieldName == lblLName.Text); 
                objDistrHistoryLog1.DistributorId = m_objDistributorMain.DistributorId;
                objDistrHistoryLog1.FieldName = lblLName.Text;
                objDistrHistoryLog1.OldValue =  m_objDistributorMain.DistributorLastName;
                objDistrHistoryLog1.NewValue =  txtDistribLName.Text;
                objLstDistHistoryLog.Add(objDistrHistoryLog1);

                setPANBankDetailLog(lblFName.Text, m_objDistributorMain.DistributorFirstName, txtDistribFName.Text);
                
                profileFlag = "P";
            }
            setPanBankDetails();
        }
        private void GetBankBranchDetail()
        {
            Distributor objDistributor = new Distributor();
            try
            {
                dtBankBranch = objDistributor.GetBankBranch();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
            finally
            {
                objDistributor = null;
            }
             
        }

        private void SetBankBranchCode()
        {
            try
            {
                string strBankBranch = txtBankBranchCode.Text;
                if (txtBankBranchCode.Text != string.Empty)
                {
                    IEnumerable<DataRow> dr = from n in dtBankBranch.AsEnumerable()
                                              where n.Field<string>("BankBranckCode").ToUpper() == strBankBranch.ToUpper()
                                              select n;
                    if (dr.Count() > 0)
                    {
                        foreach (DataRow Obj in dr)
                        {
                            lblBranchName.Text = Obj["BranchName"].ToString();
                            cmbBank.SelectedValue   = Obj["BankID"].ToString();
                            m_BankAccountNoLength = Convert.ToInt32(Obj["FixedLength"].ToString());
                          
                            
                        }
                    }
                    else
                    {
                        lblBranchName.Text = "";
                        cmbBank.SelectedIndex = 0;
                        m_BankAccountNoLength = 0;
                        if (txtBankBranchCode.Text.Trim().Length == 11 && lblBranchName.Text == string.Empty && (cmbCountry.SelectedValue.ToString() != "4"   && cmbCountry.SelectedValue.ToString() !="-1") )
                        {
                            MessageBox.Show(Common.GetMessage("INF0010", "RTGS/IFSC Code"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }
                else
                {
                    lblBranchName.Text = "";
                    cmbBank.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        private void txtBankBranchCode_TextChanged(object sender, EventArgs e)
        {
        }

        private void grpDistMasterInfo_Enter(object sender, EventArgs e)
        {

        }

        private void btnPANBank_Click(object sender, EventArgs e)
        {
            
        }

        private void btnPANDetails_Click(object sender, EventArgs e)
        {
            if (m_objDistributorMain!= null)
            {
                
                if (btnEdit.Text == "E&dit")
                {
                    edittype = "E";
                }
                else
                {
                    edittype = "S";
                }
               
                frmDistributorPANBankNew objfrmDistPanBank = new frmDistributorPANBankNew(m_objDistributorMain.DistributorId, "P", null, m_objDistributorMain, "C", edittype);
                objfrmDistPanBank.Text = "Distributor PAN Details";
                objfrmDistPanBank.ShowDialog();
            }
        }

        private void btnBankDetails_Click(object sender, EventArgs e)
        {
            if (m_objDistributorMain != null)
            {
                string edittype = "";
                if (btnEdit.Text == "E&dit")
                {
                    edittype = "E";
                }
                else
                {
                    edittype = "S";
                }

                frmDistributorPANBankNew objfrmDistPanBank = new frmDistributorPANBankNew(m_objDistributorMain.DistributorId, "B",null, m_objDistributorMain,"C",edittype);
                objfrmDistPanBank.Text = "Distributor Bank Details";
                objfrmDistPanBank.ShowDialog();
            }
        }

        private void txtDistribFName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDistribLName_TextChanged(object sender, EventArgs e)
        {

        }

        private void dtpDistributorDOB_Validated(object sender, EventArgs e)
        {
            if (m_objDistributorMain != null)
                return;

            DistributorEditHistory objDistrHistoryLog = new DistributorEditHistory();
            if (!Convert.ToDateTime( m_objDistributorMain.DistributorDOB ).Equals( dtpDistributorDOB.Value ) )
            {
                objLstDistHistoryLog.RemoveAll(d => d.FieldName == lblDOB.Text );
                objDistrHistoryLog.DistributorId = m_objDistributorMain.DistributorId;
                objDistrHistoryLog.FieldName = lblDOB.Text;
                objDistrHistoryLog.OldValue = m_objDistributorMain.DistributorDOB;
                objDistrHistoryLog.NewValue = dtpDistributorDOB.Text;
                objLstDistHistoryLog.Add(objDistrHistoryLog);

                profileFlag = "P";
            }
        }

        private void txtDistribUplineNo_Validated(object sender, EventArgs e)
        {
            if (m_objDistributorMain == null)
                return;

            DistributorEditHistory objDistrHistoryLog = new DistributorEditHistory();
            if (m_objDistributorMain.UplineDistributorId != Convert.ToInt32(txtDistribUplineNo.Text))
            {
                objLstDistHistoryLog.RemoveAll(d => d.FieldName == lblUplineNo.Text );
                objDistrHistoryLog.DistributorId = m_objDistributorMain.DistributorId;
                objDistrHistoryLog.FieldName = lblUplineNo.Text;
                objDistrHistoryLog.OldValue = m_objDistributorMain.UplineDistributorId.ToString();
                objDistrHistoryLog.NewValue = txtDistribUplineNo.Text;
                objLstDistHistoryLog.Add(objDistrHistoryLog);

                profileFlag = "P";
            }
        }

        private void txtCoDistribFName_Validated(object sender, EventArgs e)
        {
            if (m_objDistributorMain == null)
                return;

            DistributorEditHistory objDistrHistoryLog = new DistributorEditHistory();
            if (m_objDistributorMain.CoDistributorFirstName != txtCoDistribFName.Text)
            {
                objLstDistHistoryLog.RemoveAll(d => d.FieldName == lblDistributorFName.Text );
                objDistrHistoryLog.DistributorId = m_objDistributorMain.DistributorId;
                objDistrHistoryLog.FieldName = lblDistributorFName.Text ;
                objDistrHistoryLog.OldValue = m_objDistributorMain.CoDistributorFirstName;
                objDistrHistoryLog.NewValue = txtCoDistribFName.Text;
                objLstDistHistoryLog.Add(objDistrHistoryLog);

                profileFlag = "P";
            }
        }

        private void txtCoDistribLName_Validated(object sender, EventArgs e)
        {
            if (m_objDistributorMain == null)
                return;

            DistributorEditHistory objDistrHistoryLog = new DistributorEditHistory();
            if (m_objDistributorMain.CoDistributorLastName != txtCoDistribLName.Text)
            {
                objLstDistHistoryLog.RemoveAll(d => d.FieldName == lblDistributorLName.Text );
                objDistrHistoryLog.DistributorId = m_objDistributorMain.DistributorId;
                objDistrHistoryLog.FieldName = lblDistributorLName.Text;
                objDistrHistoryLog.OldValue = m_objDistributorMain.CoDistributorLastName;
                objDistrHistoryLog.NewValue = txtCoDistribLName.Text;
                objLstDistHistoryLog.Add(objDistrHistoryLog);

                profileFlag = "P";
            }
        }

        private void dtpCoDistributorDOB_Validated(object sender, EventArgs e)
        {
            if (m_objDistributorMain == null)
                return;

            DistributorEditHistory objDistrHistoryLog = new DistributorEditHistory();
            if (! Convert.ToDateTime(m_objDistributorMain.CoDistributorDOB).Equals(dtpDistributorDOB.Value ))
            {
                objLstDistHistoryLog.RemoveAll(d => d.FieldName == "CoDistributorDOB");
                objDistrHistoryLog.DistributorId = m_objDistributorMain.DistributorId;
                objDistrHistoryLog.FieldName = "CoDistributorDOB";
                objDistrHistoryLog.OldValue = m_objDistributorMain.CoDistributorDOB;
                objDistrHistoryLog.NewValue = dtpDistributorDOB.Text ;
                objLstDistHistoryLog.Add(objDistrHistoryLog);

                profileFlag = "P";
            }
        }

        private void txtAddress01_Validated(object sender, EventArgs e)
        {
            if (m_objDistributorMain == null)
                return;

            DistributorEditHistory objDistrHistoryLog = new DistributorEditHistory();
            if (m_objDistributorMain.DistributorAddress1!= txtAddress01.Text)
            {
                objLstDistHistoryLog.RemoveAll(d => d.FieldName == "Address1");
                objDistrHistoryLog.DistributorId = m_objDistributorMain.DistributorId;
                objDistrHistoryLog.FieldName = "Address1";
                objDistrHistoryLog.OldValue = m_objDistributorMain.DistributorAddress1;
                objDistrHistoryLog.NewValue = txtAddress01.Text;
                objLstDistHistoryLog.Add(objDistrHistoryLog);

                profileFlag = "P";
            }
        }

        private void txtAddress02_Validated(object sender, EventArgs e)
        {
            if (m_objDistributorMain == null)
                return;

            DistributorEditHistory objDistrHistoryLog = new DistributorEditHistory();
            if (m_objDistributorMain.DistributorAddress2 != txtAddress02.Text)
            {
                objLstDistHistoryLog.RemoveAll(d => d.FieldName == "Address2");
                objDistrHistoryLog.DistributorId = m_objDistributorMain.DistributorId;
                objDistrHistoryLog.FieldName = "Address2";
                objDistrHistoryLog.OldValue = m_objDistributorMain.DistributorAddress2;
                objDistrHistoryLog.NewValue = txtAddress02.Text;
                objLstDistHistoryLog.Add(objDistrHistoryLog);

                profileFlag = "P";
            }
        }

        private void txtAddress03_Validated(object sender, EventArgs e)
        {
            if (m_objDistributorMain == null)
                return;

            DistributorEditHistory objDistrHistoryLog = new DistributorEditHistory();
            if (m_objDistributorMain.DistributorAddress3 != txtAddress03.Text)
            {
                objLstDistHistoryLog.RemoveAll(d => d.FieldName == "Address3");
                objDistrHistoryLog.DistributorId = m_objDistributorMain.DistributorId;
                objDistrHistoryLog.FieldName = "Address3";
                objDistrHistoryLog.OldValue = m_objDistributorMain.DistributorAddress3;
                objDistrHistoryLog.NewValue = txtAddress03.Text;
                objLstDistHistoryLog.Add(objDistrHistoryLog);
                profileFlag = "P";
            }
        }

        private void txtPhoneNo_Validated(object sender, EventArgs e)
        {
            if (m_objDistributorMain == null)
                return;

            DistributorEditHistory objDistrHistoryLog = new DistributorEditHistory();
            if (m_objDistributorMain.DistributorTeleNumber != txtPhoneNo.Text)
            {
                objLstDistHistoryLog.RemoveAll(d => d.FieldName == lblPhoneNo.Text);
                objDistrHistoryLog.DistributorId = m_objDistributorMain.DistributorId;
                objDistrHistoryLog.FieldName = lblPhoneNo.Text ;
                objDistrHistoryLog.OldValue = m_objDistributorMain.DistributorTeleNumber;
                objDistrHistoryLog.NewValue = txtPhoneNo.Text ;
                objLstDistHistoryLog.Add(objDistrHistoryLog);
                
                profileFlag = "P";
            }
        }

        private void txtEmailID_Validated(object sender, EventArgs e)
        {
            if (m_objDistributorMain == null)
                return;

            DistributorEditHistory objDistrHistoryLog = new DistributorEditHistory();
            if (m_objDistributorMain.DistributorEMailID != txtEmailID.Text)
            {
                objLstDistHistoryLog.RemoveAll(d => d.FieldName == lblEmailID.Text);
                objDistrHistoryLog.DistributorId = m_objDistributorMain.DistributorId;
                objDistrHistoryLog.FieldName = lblEmailID.Text;
                objDistrHistoryLog.OldValue = m_objDistributorMain.DistributorEMailID;
                objDistrHistoryLog.NewValue = txtEmailID.Text;
                objLstDistHistoryLog.Add(objDistrHistoryLog);
                profileFlag = "P";
            }
        }

        private void cmbBank_Validated(object sender, EventArgs e)
        {
            if (m_objDistributorMain == null)
                return;

            DistributorEditHistory objDistrHistoryLog = new DistributorEditHistory();
            if (m_objDistributorMain.Bank != cmbBank.Text)
            {
                objLstDistHistoryLog.RemoveAll(d => d.FieldName == lblBankType.Text);
                objDistrHistoryLog.DistributorId = m_objDistributorMain.DistributorId;
                objDistrHistoryLog.FieldName = lblBankType.Text;
                objDistrHistoryLog.OldValue = m_objDistributorMain.BankCode.ToString() ;
                objDistrHistoryLog.NewValue = cmbBank.Text;
                objLstDistHistoryLog.Add(objDistrHistoryLog);
                m_objDistributorMain.BankType = "B";

                setPANBankDetailLog(lblFName.Text, m_objDistributorMain.DistributorFirstName, txtDistribFName.Text);
                setPANBankDetailLog(lblLName.Text, m_objDistributorMain.DistributorLastName, txtDistribLName.Text);
                setPANBankDetailLog(lblPANNo.Text, m_objDistributorMain.DistributorPANNumber , txtPANNo.Text);
                setPANBankDetailLog(lblAccount.Text, m_objDistributorMain.AccountNumber , txtAccountNo.Text);
                setPANBankDetailLog(lblBankBranchCode.Text, m_objDistributorMain.BankBranchCode, txtBankBranchCode.Text);
            }
        }

        private void cmbCountry_Validated(object sender, EventArgs e)
        {
            if (m_objDistributorMain == null)
                return;

            DistributorEditHistory objDistrHistoryLog = new DistributorEditHistory();
            if (m_objDistributorMain.DistributorCountry != cmbCountry.Text)
            {
                objLstDistHistoryLog.RemoveAll(d => d.FieldName == lblCountry.Text);
                objDistrHistoryLog.DistributorId = m_objDistributorMain.DistributorId;
                objDistrHistoryLog.FieldName = lblCountry.Text;
                objDistrHistoryLog.OldValue = m_objDistributorMain.DistributorCountry;
                objDistrHistoryLog.NewValue = cmbCountry.Text;
                objLstDistHistoryLog.Add(objDistrHistoryLog);
                profileFlag = "P";
            }
        }

        private void cmbState_Validated(object sender, EventArgs e)
        {
            if (m_objDistributorMain == null)
                return;

            DistributorEditHistory objDistrHistoryLog = new DistributorEditHistory();
            if (m_objDistributorMain.DistributorState != cmbState.Text)
            {
                objLstDistHistoryLog.RemoveAll(d => d.FieldName == lblState.Text   );
                objDistrHistoryLog.DistributorId = m_objDistributorMain.DistributorId;
                objDistrHistoryLog.FieldName = lblState.Text;
                objDistrHistoryLog.OldValue = m_objDistributorMain.DistributorState;
                objDistrHistoryLog.NewValue = cmbState.Text;
                objLstDistHistoryLog.Add(objDistrHistoryLog);

                profileFlag = "P";
            }
        }

        private void cmbCity_Validated(object sender, EventArgs e)
        {
            if (m_objDistributorMain == null)
                return;

            DistributorEditHistory objDistrHistoryLog = new DistributorEditHistory();
            if (m_objDistributorMain.DistributorCity != cmbCity.Text)
            {
                objLstDistHistoryLog.RemoveAll(d => d.FieldName == lblCity.Text  );
                objDistrHistoryLog.DistributorId = m_objDistributorMain.DistributorId;
                objDistrHistoryLog.FieldName = lblCity.Text;
                objDistrHistoryLog.OldValue = m_objDistributorMain.DistributorCity;
                objDistrHistoryLog.NewValue = cmbCity.Text;
                objLstDistHistoryLog.Add(objDistrHistoryLog);
                
                profileFlag = "P";
            }
        }

        private void txtMobileNo_Validated(object sender, EventArgs e)
        {
            if (m_objDistributorMain == null)
                return;


            DistributorEditHistory objDistrHistoryLog = new DistributorEditHistory();
            if (m_objDistributorMain.DistributorMobNumber != txtMobileNo.Text)
            {
                objLstDistHistoryLog.RemoveAll(d => d.FieldName == lblMobileNo.Text );
                objDistrHistoryLog.DistributorId = m_objDistributorMain.DistributorId;
                objDistrHistoryLog.FieldName = lblMobileNo.Text;
                objDistrHistoryLog.OldValue = m_objDistributorMain.DistributorMobNumber;
                objDistrHistoryLog.NewValue = txtMobileNo.Text;
                objLstDistHistoryLog.Add(objDistrHistoryLog);
                
                profileFlag = "P";
            }
        }

        private void txtPANNo_Validated(object sender, EventArgs e)
        {
            if (m_objDistributorMain == null)
                return;

            if (Common.LocationCode.ToUpper()=="HO" && txtPANNo.Text.Trim().Length > 0 && chkPan.Checked != true)
            {
                btnPANDetails.Enabled = true;
            }
            else
            {
                btnPANDetails.Enabled = false;
            }

            DistributorEditHistory objDistrHistoryLog = new DistributorEditHistory();
            if (m_objDistributorMain.DistributorPANNumber != txtPANNo.Text)
            {
                objLstDistHistoryLog.RemoveAll(d => d.FieldName == lblPANNo.Text );
                objDistrHistoryLog.DistributorId = m_objDistributorMain.DistributorId;
                objDistrHistoryLog.FieldName = lblPANNo.Text;
                objDistrHistoryLog.OldValue = m_objDistributorMain.DistributorPANNumber;
                objDistrHistoryLog.NewValue = txtPANNo.Text;
                objLstDistHistoryLog.Add(objDistrHistoryLog);
                
                setPANBankDetailLog(lblFName.Text, m_objDistributorMain.DistributorFirstName , txtDistribFName.Text);
                setPANBankDetailLog(lblLName.Text, m_objDistributorMain.DistributorLastName, txtDistribLName.Text);
                setPANBankDetailLog(lblBankBranchCode.Text, m_objDistributorMain.BankBranchCode, txtBankBranchCode.Text);
                setPANBankDetailLog(lblAccount.Text, m_objDistributorMain.AccountNumber , txtAccountNo.Text);
                setPANBankDetailLog(lblBankType.Text, m_objDistributorMain.BankCode.ToString()  , cmbBank.Text);
                
                m_objDistributorMain.PANType = "P";
            
            }
            
        }

        private void txtAccountNo_Validated(object sender, EventArgs e)
        {
            if (m_objDistributorMain == null)
                return;

            if (m_BankAccountNoLength != 0 && m_BankAccountNoLength < txtAccountNo.Text.Length)
            {
                if (DialogResult.No == MessageBox.Show(Common.GetMessage("VAL0625", cmbBank.Text, m_BankAccountNoLength.ToString()), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                {
                    txtAccountNo.Focus();
                    return;
                }
            }
           
            if (Common.LocationCode.ToUpper() == "HO" && txtAccountNo.Text.Trim().Length > 0 && chkBank.Checked != true)
            {
                btnBankDetails.Enabled = true;
            }
            else
            {
                btnBankDetails.Enabled = false;
            }
            DistributorEditHistory objDistrHistoryLog = new DistributorEditHistory();
            if (m_objDistributorMain.AccountNumber != txtAccountNo.Text)
            {
                objLstDistHistoryLog.RemoveAll(d => d.FieldName == lblAccount.Text  );
                objDistrHistoryLog.DistributorId = m_objDistributorMain.DistributorId;
                objDistrHistoryLog.FieldName = lblAccount.Text;
                objDistrHistoryLog.OldValue = m_objDistributorMain.AccountNumber;
                objDistrHistoryLog.NewValue = txtAccountNo.Text;
                objLstDistHistoryLog.Add(objDistrHistoryLog);

                setPANBankDetailLog(lblFName.Text, m_objDistributorMain.DistributorFirstName, txtDistribFName.Text);
                setPANBankDetailLog(lblLName.Text, m_objDistributorMain.DistributorLastName, txtDistribLName.Text);
                setPANBankDetailLog(lblBankBranchCode.Text, m_objDistributorMain.BankBranchCode, txtBankBranchCode.Text);
                setPANBankDetailLog(lblPANNo.Text, m_objDistributorMain.DistributorPANNumber , txtPANNo.Text);
                setPANBankDetailLog(lblBankType.Text, m_objDistributorMain.BankCode.ToString() , cmbBank.Text);
                
                m_objDistributorMain.BankType = "B";
            }
        }

        private void txtPinCode_Validated(object sender, EventArgs e)
        {
            if (m_objDistributorMain == null)
                return;

            DistributorEditHistory objDistrHistoryLog = new DistributorEditHistory();
            if (m_objDistributorMain.DistributorPinCode != txtPinCode.Text)
            {
                objLstDistHistoryLog.RemoveAll(d => d.FieldName == lblPinCode.Text );
                objDistrHistoryLog.DistributorId = m_objDistributorMain.DistributorId;
                objDistrHistoryLog.FieldName = lblPinCode.Text;
                objDistrHistoryLog.OldValue = m_objDistributorMain.DistributorPinCode;
                objDistrHistoryLog.NewValue = txtPinCode.Text  ;
                objLstDistHistoryLog.Add(objDistrHistoryLog);

                profileFlag = "P";
            }
        }

        private void txtFaxNo_Validated(object sender, EventArgs e)
        {
            if (m_objDistributorMain == null)
                return;

            DistributorEditHistory objDistrHistoryLog = new DistributorEditHistory();
            if (m_objDistributorMain.DistributorFaxNumber != txtFaxNo.Text)
            {
                objLstDistHistoryLog.RemoveAll(d => d.FieldName == lblFaxNo.Text  );
                objDistrHistoryLog.DistributorId = m_objDistributorMain.DistributorId;
                objDistrHistoryLog.FieldName = lblFaxNo.Text;
                objDistrHistoryLog.OldValue = m_objDistributorMain.DistributorFaxNumber;
                objDistrHistoryLog.NewValue = txtFaxNo.Text;
                objLstDistHistoryLog.Add(objDistrHistoryLog);

                profileFlag = "P";
            }
        }

        private void txtBankBranchCode_Validated(object sender, EventArgs e)
        {
            if (m_objDistributorMain == null)
                return;

            SetBankBranchCode();

            DistributorEditHistory objDistrHistoryLog = new DistributorEditHistory();
            if (m_objDistributorMain.BankBranchCode != txtBankBranchCode.Text )
            {
                objLstDistHistoryLog.RemoveAll(d => d.FieldName == lblBankBranchCode.Text);
                objDistrHistoryLog.DistributorId = m_objDistributorMain.DistributorId;
                objDistrHistoryLog.FieldName = lblBankBranchCode.Text;
                objDistrHistoryLog.OldValue = m_objDistributorMain.BankBranchCode;
                objDistrHistoryLog.NewValue = txtBankBranchCode.Text;
                objLstDistHistoryLog.Add(objDistrHistoryLog);
                m_objDistributorMain.BankType = "B";

                setPANBankDetailLog(lblFName.Text, m_objDistributorMain.DistributorFirstName, txtDistribFName.Text);
                setPANBankDetailLog(lblLName.Text, m_objDistributorMain.DistributorLastName, txtDistribLName.Text);
                setPANBankDetailLog(lblPANNo.Text, m_objDistributorMain.DistributorPANNumber, txtPANNo.Text);
                setPANBankDetailLog(lblAccount.Text, m_objDistributorMain.AccountNumber, txtAccountNo.Text);
                setPANBankDetailLog(lblBankType.Text, m_objDistributorMain.BankCode.ToString() , cmbBank.Text);
            }

            if (txtBankBranchCode.Text.Trim().Length < 11)
            {
                cmbBank.SelectedIndex = 0; 
            }
        }

        private void cmbRemarks_Validated(object sender, EventArgs e)
        {
            if (m_objDistributorMain == null)
                return;

            DistributorEditHistory objDistrHistoryLog = new DistributorEditHistory();
            if (m_objDistributorMain.Remarks != cmbRemarks.Text)
            {
                objLstDistHistoryLog.RemoveAll(d => d.FieldName == lblRemarks.Text);
                objDistrHistoryLog.DistributorId = m_objDistributorMain.DistributorId;
                objDistrHistoryLog.FieldName = lblRemarks.Text;
                objDistrHistoryLog.OldValue = m_objDistributorMain.Remarks;
                objDistrHistoryLog.NewValue = cmbRemarks.Text.ToString();
                objLstDistHistoryLog.Add(objDistrHistoryLog);

            }
        }

        private void txtBankBranchCode_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void txtBankBranchCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetterOrDigit(e.KeyChar) || e.KeyChar == '\b'))
            {
                e.Handled = true;
            }
        }

        private void txtPANNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetterOrDigit(e.KeyChar) || e.KeyChar == '\b'))
            {
                e.Handled = true;
            }
        }

        private void setPanBankDetails()
        {
            if ((txtDistribFName.Text != m_objDistributorMain.DistributorFirstName) || (txtDistribLName.Text != m_objDistributorMain.DistributorLastName))
            {
                txtPANNo.Text = "";
               // if (txtPANNo.Text != m_objDistributorMain.DistributorPANNumber)
                //{
                    setPANBankDetailLog(lblPANNo.Text, m_objDistributorMain.DistributorPANNumber, txtPANNo.Text);
                    m_objDistributorMain.PANType = "P";                    
               // }
                txtAccountNo.Text = "";
                //if (txtAccountNo.Text != m_objDistributorMain.AccountNumber)
                //{
                    setPANBankDetailLog(lblAccount.Text, m_objDistributorMain.AccountNumber, txtAccountNo.Text);
                    m_objDistributorMain.BankType ="B";
                //}
                
                cmbBank.SelectedIndex = 0;
                //if (m_objDistributorMain.Bank != cmbBank.Text)
                //{
                    setPANBankDetailLog(lblBankType.Text, m_objDistributorMain.BankCode.ToString(), cmbBank.Text);
                    m_objDistributorMain.BankType = "B";
                //}
                
                txtBankBranchCode.Text = "";
                //if (txtBankBranchCode.Text != m_objDistributorMain.BankBranchCode)
                //{
                    setPANBankDetailLog(lblBankBranchCode.Text, m_objDistributorMain.BankBranchCode, txtBankBranchCode.Text);
                    m_objDistributorMain.BankType = "B";
                //}

                if (txtPANNo.Text == string.Empty  && txtAccountNo.Text ==string.Empty)
                {
                    btnPANDetails.Enabled = false;
                    btnBankDetails.Enabled = false;
                }

            }
        }

        private void setPANBankDetailLog(string label,string oldValue, string newValue)
        {
                DistributorEditHistory objDistrHistoryLog = new DistributorEditHistory();
                objLstDistHistoryLog.RemoveAll(d => d.FieldName == label);
                objDistrHistoryLog.DistributorId = m_objDistributorMain.DistributorId;
                objDistrHistoryLog.FieldName = label;
                objDistrHistoryLog.OldValue = oldValue ;
                objDistrHistoryLog.NewValue = newValue;
                objLstDistHistoryLog.Add(objDistrHistoryLog);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {

        }

        private bool ValidationForReport(string distributorID)
        {
            bool result = false;

            if (string.IsNullOrEmpty(distributorID))
            {
                result = false;
            }
            else
            {
                result= true;
            }

            return result;
        }

        public void setDocRejectFlag(int rejectFlag)
        {
            if (rejectFlag == Convert.ToInt16(Common.DistRejectType.Pan))
                chkPan.Checked = true;
            else if (rejectFlag == Convert.ToInt16(Common.DistRejectType.Bank))
                chkBank.Checked = true;
            else if (rejectFlag == Convert.ToInt16(Common.DistRejectType.Both))
            {
                chkPan.Checked = true;
                chkBank.Checked = true;
            }
         }

        public void setDocumentReject(int  rejectFlag)
        {
            if (rejectFlag == 2)
            {
                //m_objDistributorMain.DistributorPANNumber = "REJECT";
                txtPANNo.Text = "REJECT";  
            }
            else if (rejectFlag == 3)
            {
                txtAccountNo.Text = "REJECT";
                txtBankBranchCode.Text = "REJECT";
                //m_objDistributorMain.AccountNumber = "REJECT";
            }
            else if(rejectFlag==4)
            {
                txtAccountNo.Text = "REJECT";
                txtBankBranchCode.Text = "REJECT";
                txtPANNo.Text = "REJECT";
                //m_objDistributorMain.AccountNumber = "REJECT";
                //m_objDistributorMain.DistributorPANNumber = "REJECT";  
            }
        }

        private void chkPan_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPan.Checked && chkBank.Checked)
            {
                m_objDistributorMain.DistributorDocumentFlag = Convert.ToInt16(Common.DistRejectType.Both);
                txtPANNo.Text = "REJECT";
                txtAccountNo.Text = "REJECT";
                txtBankBranchCode.Text = "REJECT";
                txtBankBranchCode.Enabled = false;
                txtAccountNo.Enabled = false;
                txtPANNo.Enabled = false;
                btnPANDetails.Enabled = false;
                btnBankDetails.Enabled = false;
                
                setPANBankDetailLog(lblFName.Text, m_objDistributorMain.DistributorFirstName, txtDistribFName.Text);
                setPANBankDetailLog(lblLName.Text, m_objDistributorMain.DistributorLastName, txtDistribLName.Text);
                setPANBankDetailLog(lblPANNo.Text, m_objDistributorMain.DistributorPANNumber, txtPANNo.Text);
                setPANBankDetailLog(lblBankBranchCode.Text, m_objDistributorMain.BankBranchCode, txtBankBranchCode.Text);
                setPANBankDetailLog(lblBankType.Text, m_objDistributorMain.BankCode.ToString(), cmbBank.Text);
                setPANBankDetailLog(lblAccount.Text, m_objDistributorMain.AccountNumber, txtAccountNo.Text);
                m_objDistributorMain.PANType = "P";
                m_objDistributorMain.BankType = "B";

            }
            else if(chkPan.Checked) 
            {
                txtPANNo.Text = "REJECT";
                txtPANNo.Enabled = false;
                btnPANDetails.Enabled = false;
                m_objDistributorMain.DistributorDocumentFlag = Convert.ToInt16(Common.DistRejectType.Pan);
                
                setPANBankDetailLog(lblFName.Text, m_objDistributorMain.DistributorFirstName, txtDistribFName.Text);
                setPANBankDetailLog(lblLName.Text, m_objDistributorMain.DistributorLastName, txtDistribLName.Text);
                setPANBankDetailLog(lblPANNo.Text, m_objDistributorMain.DistributorPANNumber, txtPANNo.Text);
                setPANBankDetailLog(lblBankBranchCode.Text, m_objDistributorMain.BankBranchCode, txtBankBranchCode.Text);
                setPANBankDetailLog(lblBankType.Text, m_objDistributorMain.BankCode.ToString(), cmbBank.Text);
                setPANBankDetailLog(lblAccount.Text, m_objDistributorMain.AccountNumber, txtAccountNo.Text);
                //setPanBankDetails();
                m_objDistributorMain.PANType = "P";
            }
            else{
                if (m_objDistributorMain == null)
                    return;

                txtPANNo.Text = "";
                txtPANNo.Enabled = true;
                //btnPANDetails.Enabled = true;
                //setPanBankDetails();
                setPANBankDetailLog(lblFName.Text, m_objDistributorMain.DistributorFirstName, txtDistribFName.Text);
                setPANBankDetailLog(lblLName.Text, m_objDistributorMain.DistributorLastName, txtDistribLName.Text);
                setPANBankDetailLog(lblPANNo.Text, m_objDistributorMain.DistributorPANNumber, txtPANNo.Text);
                setPANBankDetailLog(lblBankBranchCode.Text, m_objDistributorMain.BankBranchCode, txtBankBranchCode.Text);
                setPANBankDetailLog(lblBankType.Text, m_objDistributorMain.BankCode.ToString(), cmbBank.Text);
                setPANBankDetailLog(lblAccount.Text, m_objDistributorMain.AccountNumber, txtAccountNo.Text);
                m_objDistributorMain.PANType = "P";
            }
        }

        private void chkBank_CheckedChanged(object sender, EventArgs e)
        {
             if (chkPan.Checked && chkBank.Checked)
            {
                txtPANNo.Text = "REJECT";
                txtAccountNo.Text = "REJECT";
                txtBankBranchCode.Text = "REJECT";
                txtBankBranchCode.Enabled = false;
                txtAccountNo.Enabled = false;
                txtPANNo.Enabled = false;
                btnPANDetails.Enabled = false;
                btnBankDetails.Enabled = false;

                setPANBankDetailLog(lblFName.Text, m_objDistributorMain.DistributorFirstName, txtDistribFName.Text);
                setPANBankDetailLog(lblLName.Text, m_objDistributorMain.DistributorLastName, txtDistribLName.Text);
                setPANBankDetailLog(lblPANNo.Text, m_objDistributorMain.DistributorPANNumber , txtPANNo.Text);
                setPANBankDetailLog(lblBankBranchCode.Text, m_objDistributorMain.BankBranchCode, txtBankBranchCode.Text);
                setPANBankDetailLog(lblBankType.Text, m_objDistributorMain.Bank, cmbBank.Text);
                setPANBankDetailLog(lblAccount.Text, m_objDistributorMain.AccountNumber, txtAccountNo.Text);
                m_objDistributorMain.DistributorDocumentFlag = Convert.ToInt16(Common.DistRejectType.Both);
                m_objDistributorMain.PANType = "P";
                m_objDistributorMain.BankType = "B";
            }
            else if (chkBank.Checked)
            {
                txtAccountNo.Text = "REJECT";
                txtBankBranchCode.Text = "REJECT";
                m_objDistributorMain.DistributorDocumentFlag = Convert.ToInt16(Common.DistRejectType.Bank);
                txtBankBranchCode.Enabled = false;
                txtAccountNo.Enabled = false;
                btnBankDetails.Enabled = false;
                setPANBankDetailLog(lblFName.Text, m_objDistributorMain.DistributorFirstName, txtDistribFName.Text);
                setPANBankDetailLog(lblLName.Text, m_objDistributorMain.DistributorLastName, txtDistribLName.Text);
                setPANBankDetailLog(lblBankBranchCode.Text, m_objDistributorMain.BankBranchCode, txtBankBranchCode.Text);
                setPANBankDetailLog(lblBankType.Text, m_objDistributorMain.BankCode.ToString() , cmbBank.Text);
                setPANBankDetailLog(lblAccount.Text, m_objDistributorMain.AccountNumber, txtAccountNo.Text);
                setPANBankDetailLog(lblPANNo.Text, m_objDistributorMain.DistributorPANNumber, txtPANNo.Text);
                //setPanBankDetails();
                m_objDistributorMain.BankType = "B";
            }
            else 
            {
                if (m_objDistributorMain == null)
                    return;

                txtAccountNo.Text = "";
                txtBankBranchCode.Text = "";
                txtBankBranchCode.Enabled = true;
                txtAccountNo.Enabled = true;
                //setPanBankDetails();
                //btnBankDetails.Enabled = true;
                setPANBankDetailLog(lblFName.Text, m_objDistributorMain.DistributorFirstName, txtDistribFName.Text);
                setPANBankDetailLog(lblLName.Text, m_objDistributorMain.DistributorLastName, txtDistribLName.Text);
                setPANBankDetailLog(lblBankBranchCode.Text, m_objDistributorMain.BankBranchCode, txtBankBranchCode.Text);
                setPANBankDetailLog(lblBankType.Text, m_objDistributorMain.BankCode.ToString() , cmbBank.Text);
                setPANBankDetailLog(lblAccount.Text, m_objDistributorMain.AccountNumber, txtAccountNo.Text);
                setPANBankDetailLog(lblPANNo.Text, m_objDistributorMain.DistributorPANNumber, txtPANNo.Text);
                m_objDistributorMain.BankType = "B";
            }
        }

        private void setDistributorPanBankUpdateDate(DataSet  ds)
        {
            try
            {
                if (ds.Tables[6].Rows.Count > 0)
                {
                    m_objDistributorMain.BankUpdateDate = Convert.ToDateTime(Validators.IsDateTime(ds.Tables[6].Rows[0]["BankUpdate"].ToString()) ? ds.Tables[6].Rows[0]["BankUpdate"].ToString() : Common.DATETIME_NULL.ToString());
                    m_objDistributorMain.PanUpdateDate = Convert.ToDateTime(Validators.IsDateTime(ds.Tables[6].Rows[0]["PANUpdate"].ToString()) ? ds.Tables[6].Rows[0]["PANUpdate"].ToString() : Common.DATETIME_NULL.ToString()); 
                }
                else
                {
                    m_objDistributorMain.BankUpdateDate = Common.DATETIME_NULL;
                    m_objDistributorMain.PanUpdateDate = Common.DATETIME_NULL;
                }
            }
            catch(Exception ex)
            {
            
            }
        }

        private bool accessDistributorBank()
        {
            bool accessPanBank = false;
            
            if(Common.LocationCode.ToUpper()  !="HO" && (Common.LocationCode.ToUpper()==m_objDistributorMain.RegistrationLocation.ToUpper()      && ((txtAccountNo.Text =="" || txtAccountNo.Text == string.Empty || txtAccountNo.Text.Trim() =="REJECT") || m_objDistributorMain.BankUpdateDate.ToShortDateString().Equals(Common.DATETIME_CURRENT.ToShortDateString()))))
            {
                accessPanBank = true;
            }
            else 
            {
                accessPanBank =false;                
            }
            return accessPanBank;
        }

        private bool accessDistributorPan()
        {
            bool accessPanBank = false;

            if (Common.LocationCode.ToUpper() != "HO" &&( Common.LocationCode.ToUpper() == m_objDistributorMain.RegistrationLocation.ToUpper() && ((txtPANNo.Text == "" || txtPANNo.Text == string.Empty || txtPANNo.Text.Trim() == "REJECT") || m_objDistributorMain.PanUpdateDate.ToShortDateString().Equals(Common.DATETIME_CURRENT.ToShortDateString()))))
            {
                accessPanBank = true;
            }
            else
            {
                accessPanBank = false;
            }
            return accessPanBank;
        }

        private void pnlBodyTop_Paint(object sender, PaintEventArgs e)
        {

        }

        private void chkSF9_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void chkSF9_CheckStateChanged(object sender, EventArgs e)
        {
             
        }

        private void dgvAllInvoices_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
     }
}
