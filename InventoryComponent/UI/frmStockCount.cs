using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CoreComponent.Core.BusinessObjects;
using InventoryComponent.BusinessObjects;
using CoreComponent.Hierarchies.BusinessObjects;
using CoreComponent.MasterData.BusinessObjects;
using System.Collections.Specialized;
using AuthenticationComponent.BusinessObjects;
using System.Reflection;

namespace InventoryComponent.UI
{
    public partial class frmStockCount : CoreComponent.Core.UI.Transaction
    {
        #region Variables

        //Boolean m_invalidQuantity = false;
        Boolean m_F4Press = false;
        int m_bucketid = Common.INT_DBNULL;
        DataTable m_dtSearchAllBucket;
        decimal m_systemQty = 0;
        DataSet m_printDataSet = null;

        string m_mfgbatchNo = string.Empty;
        List<StockCount> m_lstStockCount;
        List<ItemStockCount> m_lstItemDetail;
        List<ItemStockBatch> m_lstItemBatchDetail;

        ItemStockBatch m_itemStockBatch;
        ItemStockCount m_itemDetail;

        StockCount m_objStockCount;
        string m_seqNo = Common.INT_DBNULL.ToString();

        int m_selectedItemBatchRowNum = Common.INT_DBNULL;
        int m_selectedItemRowNum = Common.INT_DBNULL;
        int m_selectedItemBatchRowIndex = Common.INT_DBNULL;
        int m_selectedItemRowIndex = Common.INT_DBNULL;
        int m_itemId = Common.INT_DBNULL;
        int m_itemRowNo = Common.INT_DBNULL;
        DataTable m_dtLocation;

        string m_batchNo = string.Empty;
        int m_isDuplicateRecordFound = Common.INT_DBNULL;

        private Boolean m_isSearchAvailable = false;
        private Boolean m_isCreateAvailable = false;
        private Boolean m_isCancelAvailable = false;
        private Boolean m_isInitiateAvailable = false;
        private Boolean m_isProcessAvailable = false;
        private Boolean m_isExecuteAvailable = false;
        private Boolean m_isCloseAvailable = false;
        private Boolean m_isPrintAvailable = false;


        private int m_userId = Authenticate.LoggedInUser.UserId;
        private string strUserName = Authenticate.LoggedInUser.UserName;

        private string strLocationCode = Common.LocationCode;
        private int m_currentLocationId = Common.CurrentLocationId;
        private int m_locationType = Common.CurrentLocationTypeId;
        #endregion Variables

        #region C'tor
        public frmStockCount()
        {
            try
            {
                m_isSearchAvailable = Authenticate.IsFunctionAccessible(strUserName, strLocationCode, StockCount.MODULE_CODE, Common.FUNCTIONCODE_SEARCH);
                m_isCreateAvailable = Authenticate.IsFunctionAccessible(strUserName, strLocationCode, StockCount.MODULE_CODE, Common.FUNCTIONCODE_CREATE);
                m_isInitiateAvailable = Authenticate.IsFunctionAccessible(strUserName, strLocationCode, StockCount.MODULE_CODE, Common.FUNCTIONCODE_INITIATE);
                m_isCancelAvailable = Authenticate.IsFunctionAccessible(strUserName, strLocationCode, StockCount.MODULE_CODE, Common.FUNCTIONCODE_CANCEL);
                m_isProcessAvailable = Authenticate.IsFunctionAccessible(strUserName, strLocationCode, StockCount.MODULE_CODE, Common.FUNCTIONCODE_PROCESS);
                m_isExecuteAvailable = Authenticate.IsFunctionAccessible(strUserName, strLocationCode, StockCount.MODULE_CODE, Common.FUNCTIONCODE_EXECUTE);
                m_isCloseAvailable = Authenticate.IsFunctionAccessible(strUserName, strLocationCode, StockCount.MODULE_CODE, Common.FUNCTIONCODE_CLOSE);
                m_isPrintAvailable = Authenticate.IsFunctionAccessible(strUserName, strLocationCode, StockCount.MODULE_CODE, Common.FUNCTIONCODE_PRINT);
                InitializeComponent();
                GridInitialize();
                FillLocations();
                FillBucket(true);
                FillSearchStatus();
                InitializeDateControl();
                FillUsers();
                EnableDisableButton((int)Common.StockStatus.New);

                if (m_locationType == (int)Common.LocationConfigId.BO || m_locationType == (int)Common.LocationConfigId.WH)
                {
                    dgvStockItem.Columns[4].Visible = false;
                    dgvStockItem.Columns[5].Visible = false;
                }

                ReadOnlyItemDetail();
                ReadOnlyBatchDetail();
                lblPageTitle.Text = "Stock Count";

            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Methods
        void ReadOnlyItemDetail()
        {

            if (m_locationType == (int)Common.LocationConfigId.HO)
            {
                cmbBucket.Enabled = true;
                if ((m_objStockCount == null) || (m_objStockCount != null && m_objStockCount.StatusId <= (int)Common.StockStatus.Created))
                {
                    txtItemCode.Enabled = true;
                    cmbBucket.Enabled = true;
                }
                else
                {
                    txtItemCode.Enabled = false;
                    cmbBucket.Enabled = false;
                }
            }
            else
            {
                txtItemCode.Enabled = false;
                cmbBucket.Enabled = false;
            }
        }

        void ReadOnlyBatchDetail()
        {
            bool batchDisable = false;
            if (m_locationType == (int)Common.LocationConfigId.BO || m_locationType == (int)Common.LocationConfigId.WH)
            {
                if (m_objStockCount != null && (m_objStockCount.StatusId == (int)Common.StockStatus.Initiated || m_objStockCount.StatusId == (int)Common.StockStatus.Processed))
                    batchDisable = false;
                else
                    batchDisable = true;


                cmbBucket.Enabled = false;
                dtpStockCountDate.Enabled = false;
                txtRemarks.Enabled = false;
            }
            else
                batchDisable = true;

            BatchDisable(batchDisable);
        }

        void BatchDisable(bool batchDisable)
        {
            txtBatchNo.Enabled = !batchDisable;
            txtPhysicalQty.Enabled = !batchDisable;
        }


        //#region Methods
        /// <summary>
        /// Grid Initialize
        /// </summary>
        void GridInitialize()
        {
            dgvSearchStockCount.AutoGenerateColumns = false;
            dgvSearchStockCount.DataSource = null;
            DataGridView dgvSearchStockCountNew = Common.GetDataGridViewColumns(dgvSearchStockCount, Environment.CurrentDirectory + "\\App_Data\\Inventory.xml");

            dgvStockItemBatch.AutoGenerateColumns = false;
            dgvStockItemBatch.DataSource = null;
            DataGridView dgvStockItemBatchNew = Common.GetDataGridViewColumns(dgvStockItemBatch, Environment.CurrentDirectory + "\\App_Data\\Inventory.xml");

            dgvStockItem.AutoGenerateColumns = false;
            dgvStockItem.DataSource = null;
            DataGridView dgvStockItemNew = Common.GetDataGridViewColumns(dgvStockItem, Environment.CurrentDirectory + "\\App_Data\\Inventory.xml");
        }

        /// <summary>
        /// Fill Bucket 
        /// </summary>
        /// <param name="yesNo"></param>
        void FillBucket(bool yesNo)
        {
            if (yesNo)
            {
                DataView dv = new DataView();
                DataTable dt = new DataTable();
                m_dtSearchAllBucket = Common.ParameterLookup(Common.ParameterType.BucketItemLocation, new ParameterFilter(string.Empty, 0, 0, 0));
                dt = Common.ParameterLookup(Common.ParameterType.AllSubBuckets, new ParameterFilter(string.Empty, 0, 0, 0));

                if (m_dtSearchAllBucket != null && m_dtSearchAllBucket.Rows.Count > 0)
                {
                    //m_dtSearchAllBucket = m_dtSearchAllBucket.AsEnumerable().Distinct().CopyToDataTable();
                    //dt = m_dtSearchAllBucket;//.Select("((LocationId = '" + cmbLocation.SelectedValue.ToString() + "') And (ItemCode ='" + txtItemCode.Text.Trim() + "' OR " + txtItemCode.Text.Trim().Length + "=0) OR ItemCode='-1')").AsEnumerable().Distinct().CopyToDataTable();

                    dv = new DataView(dt.DefaultView.ToTable(true, "BucketId", "BucketName"));
                }
                if (dv != null && dv.Count > 0)
                {
                    cmbBucket.SelectedIndexChanged -= new EventHandler(cmbBucket_SelectedIndexChanged);
                    cmbBucket.DataSource = dv;
                    cmbBucket.DisplayMember = "BucketName";
                    cmbBucket.ValueMember = "BucketId";
                    cmbBucket.SelectedIndexChanged += new EventHandler(cmbBucket_SelectedIndexChanged);
                }
            }
        }

        void FillUsers()
        {
            DataTable dtUser = Common.ParameterLookup(Common.ParameterType.Users, new ParameterFilter("", 0, 0, 0));
            if (dtUser != null)
            {
                cmbStockCountBy.DataSource = dtUser;
                cmbStockCountBy.DisplayMember = "Name";
                cmbStockCountBy.ValueMember = "UserId";
            }
        }
        /// <summary>
        /// Fill Search and Update Location Drop Down List
        /// </summary>
        void FillLocations()
        {

            cmbLocation.SelectedIndexChanged -= new System.EventHandler(cmbLocation_SelectedIndexChanged);

            m_dtLocation = Common.ParameterLookup(Common.ParameterType.Locations, new ParameterFilter("LOCATIONS", 0, 0, 0));
            DataRow[] dr = m_dtLocation.Select("LocationId = " + Common.INT_DBNULL);
            dr[0]["DisplayName"] = "Select";

            if (m_dtLocation != null)
            {

                cmbLocation.DataSource = m_dtLocation;
                cmbLocation.DisplayMember = "DisplayName";
                cmbLocation.ValueMember = "LocationId";

                if (m_locationType == (int)Common.LocationConfigId.BO || m_locationType == (int)Common.LocationConfigId.WH)
                {
                    cmbLocation.SelectedValue = m_currentLocationId.ToString();
                    cmbLocation.Enabled = false;
                }
            }

            DataTable dtSearchDest = Common.ParameterLookup(Common.ParameterType.Locations, new ParameterFilter("LOCATIONS", 0, 0, 0));
            if (dtSearchDest != null)
            {
                cmbSearchLocation.DataSource = dtSearchDest;
                cmbSearchLocation.DisplayMember = "DisplayName";
                cmbSearchLocation.ValueMember = "LocationId";

                if (m_locationType == (int)Common.LocationConfigId.BO || m_locationType == (int)Common.LocationConfigId.WH)
                {
                    cmbSearchLocation.SelectedValue = m_currentLocationId.ToString();
                    cmbSearchLocation.Enabled = false;
                }
            }
            cmbLocation.SelectedIndexChanged += new System.EventHandler(cmbLocation_SelectedIndexChanged);
        }

        ///// <summary>
        ///// Fill Status Drop Down List
        ///// </summary>
        private void FillSearchStatus()
        {
            DataTable dt;
            if (m_locationType == (int)Common.LocationConfigId.HO)
                dt = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter("STOCKSTATUS", 0, 0, 0));
            else
                dt = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter("STOCKSTATUSSEARCH", 0, 0, 0));
            cmbSearchStatus.DataSource = dt;
            cmbSearchStatus.DisplayMember = Common.KEYVALUE1;
            cmbSearchStatus.ValueMember = Common.KEYCODE1;

        }

        ///// <summary>
        ///// Initialize Date Picker Value
        ///// </summary>
        void InitializeDateControl()
        {
            dtpSearchFrom.CustomFormat = Common.DTP_DATE_FORMAT;
            dtpSearchTo.CustomFormat = Common.DTP_DATE_FORMAT;
            dtpStockCountDate.CustomFormat = Common.DTP_DATE_FORMAT;
            dtpStockCountDate.Checked = false;
            dtpSearchFrom.Checked = false;
            dtpSearchTo.Checked = false;

            dtpSearchFrom.Value = Convert.ToDateTime(System.DateTime.Now.ToShortDateString());
            dtpSearchTo.Value = Convert.ToDateTime(System.DateTime.Now.ToShortDateString());
            dtpStockCountDate.Value = Convert.ToDateTime(System.DateTime.Now.ToShortDateString());
        }


        /// <summary>
        /// Validate Source Address
        /// </summary>
        /// <param name="yesNo"></param>
        void ValidateLocationAddress(bool yesNo)
        {
            if (cmbLocation.SelectedIndex == 0)
            {
                if (yesNo == false)
                    errStockCount.SetError(cmbLocation, Common.GetMessage("INF0026", lblLocation.Text.Trim().Substring(0, lblLocation.Text.Trim().Length - 1)));
                //txtLocationAddress.Text = string.Empty;
                txtLocationCode.Text = string.Empty;
            }
            else
            {
                errStockCount.SetError(cmbLocation, string.Empty);
                ShowAddress();
                FillBucket(yesNo);
            }

            if (yesNo)
            {
                ResetItemControl();
            }
        }
        /// <summary>
        /// Show Address, for Source and Destination Location Changed
        /// </summary>
        /// <param name="cmb"></param>
        /// <param name="txt"></param>
        void ShowAddress()
        {
            if (cmbLocation.SelectedIndex > 0 && m_dtLocation != null)
            {
                DataRow[] dr = m_dtLocation.Select("LocationId = " + Convert.ToInt32(cmbLocation.SelectedValue));
                //txtLocationAddress.Text = dr[0]["Address"].ToString();
                txtLocationCode.Text = dr[0]["LocationCode"].ToString();
            }
            else
            {
                txtLocationCode.Text = string.Empty;
                //txtLocationAddress.Text = string.Empty;
            }
        }


        /// <summary>
        /// Search TOIs
        /// </summary>
        void SearchTI()
        {
            DateTime DATETIME_MAX = Convert.ToDateTime(new DateTime(2099, 12, 31).ToString(Common.DATE_TIME_FORMAT));

            DateTime fromDate = dtpSearchFrom.Checked == true ? Convert.ToDateTime(dtpSearchFrom.Value) : Common.DATETIME_NULL;
            DateTime toDate = dtpSearchTo.Checked == true ? Convert.ToDateTime(dtpSearchTo.Value) : DATETIME_MAX;

            if ((m_lstStockCount != null) && (m_lstStockCount.Count > 0))
            {
                dgvSearchStockCount.DataSource = m_lstStockCount;
                dgvSearchStockCount.Select();
                ResetItemControl();
            }
            else
                dgvSearchStockCount.DataSource = new List<StockCount>();
        }

        /// <summary>
        /// Remove Item Batch Record
        /// </summary>
        /// <param name="e"></param>
        Boolean RemoveItemBatch(int rowIndex, int colIndex)
        {
            if ((rowIndex >= 0) && (colIndex == 0))
            {

                if ((m_objStockCount == null) || (m_objStockCount != null && (Convert.ToInt32(m_objStockCount.StatusId) == (int)Common.StockStatus.Processed || Convert.ToInt32(m_objStockCount.StatusId) == (int)Common.StockStatus.Initiated)))
                {
                    DialogResult saveResult = MessageBox.Show(Common.GetMessage("INF0028"), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (saveResult == DialogResult.Yes)
                    {
                        if (m_lstItemBatchDetail.Count > 0)
                        {
                            dgvStockItemBatch.SelectionChanged -= new System.EventHandler(dgvStockItemBatch_SelectionChanged);

                            dgvStockItemBatch.DataSource = null;
                            m_lstItemBatchDetail.RemoveAt(rowIndex);
                            dgvStockItemBatch.DataSource = m_lstItemBatchDetail;
                            dgvStockItemBatch.Select();
                            ResetItemBatchControl();
                            //m_invalidQuantity = false;
                            dgvStockItemBatch.SelectionChanged += new System.EventHandler(dgvStockItemBatch_SelectionChanged);
                        }
                    }
                }
                else
                {
                    MessageBox.Show(Common.GetMessage("VAL0045"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Remove Item Record
        /// </summary>
        /// <param name="e"></param>
        void RemoveStockItem(DataGridViewCellEventArgs e)
        {
            if ((e.RowIndex >= 0) && (e.ColumnIndex == 0) && (dgvStockItem.Rows[e.RowIndex].Cells[e.ColumnIndex].GetType() == typeof(DataGridViewImageCell)))
            {
                if ((m_objStockCount == null) || (m_objStockCount != null && (Convert.ToInt32(m_objStockCount.StatusId) == (int)Common.StockStatus.Created || Convert.ToInt32(m_objStockCount.StatusId) == (int)Common.StockStatus.New)))
                {
                    DialogResult saveResult = MessageBox.Show(Common.GetMessage("INF0028"), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (saveResult == DialogResult.Yes)
                    {
                        if (m_lstItemDetail.Count > 0)
                        {
                            dgvStockItem.DataSource = null;
                            m_lstItemDetail.RemoveAt(e.RowIndex);
                            dgvStockItem.DataSource = m_lstItemDetail;
                            dgvStockItem.Select();
                            ResetItemControl();
                            ReadOnlyHeader();
                        }
                    }
                }
                else
                {
                    MessageBox.Show(Common.GetMessage("VAL0045"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if ((e.RowIndex >= 0) && (e.ColumnIndex == 1) && (dgvStockItem.Rows[e.RowIndex].Cells[e.ColumnIndex].GetType() == typeof(DataGridViewImageCell)))
            {
                m_itemDetail = GetItemInfo(e.RowIndex);
                //m_selectedItemRowIndex = e.RowIndex;
                //MessageBox.Show("test");
            }

        }

        /// <summary>
        /// Reset Item Controls
        /// </summary>
        void ResetItemControl()
        {
            string itemCode = txtItemCode.Text;
            m_selectedItemRowNum = Common.INT_DBNULL;
            //m_itemId = Common.INT_DBNULL;
            m_bucketid = Common.INT_DBNULL;
            m_itemDetail = null;
            m_itemRowNo = Common.INT_DBNULL;

            m_selectedItemRowIndex = Common.INT_DBNULL;
            CoreComponent.Core.BusinessObjects.VisitControls visitControls = new CoreComponent.Core.BusinessObjects.VisitControls();
            visitControls.ResetAllControlsInPanel(errStockCount, grpAddDetails);

            dgvStockItem.ClearSelection();
            txtItemCode.Text = itemCode;
            #region Bind Item Batch Grid
            if (m_objStockCount != null)
            {
                //m_lstItemBatchDetail = m_objStockCount.SearchItemBatch(m_seqNo);
                BindItemBatchGrid();
            }
            #endregion
            txtItemCode.Focus();
        }

        /// <summary>
        /// Reset Item Batch Controls
        /// </summary>
        void ResetItemBatchControl()
        {
            m_F4Press = false;
            m_selectedItemBatchRowNum = Common.INT_DBNULL;
            m_batchNo = string.Empty;
            m_mfgbatchNo = string.Empty;
            m_itemStockBatch = null;
            //m_invalidQuantity = false;
            m_selectedItemBatchRowIndex = Common.INT_DBNULL;
            CoreComponent.Core.BusinessObjects.VisitControls visitControls = new CoreComponent.Core.BusinessObjects.VisitControls();
            visitControls.ResetAllControlsInPanel(errStockCount, grpItemBatchDetails);
            m_isDuplicateRecordFound = Common.INT_DBNULL;
            dgvStockItemBatch.ClearSelection();
            txtBatchNo.Focus();
        }


        /// <summary>
        /// Generate string for Error
        /// </summary>
        /// <returns></returns>
        private StringBuilder GenerateError()
        {
            StringBuilder sbError = new StringBuilder();
            if (errStockCount.GetError(cmbLocation).Trim().Length > 0)
            {
                sbError.Append(errStockCount.GetError(cmbLocation));
                sbError.AppendLine();
            }
            if (errStockCount.GetError(dtpStockCountDate).Trim().Length > 0)
            {
                sbError.Append(errStockCount.GetError(dtpStockCountDate));
                sbError.AppendLine();
            }
            if (errStockCount.GetError(txtItemCode).Trim().Length > 0)
            {
                sbError.Append(errStockCount.GetError(txtItemCode));
                sbError.AppendLine();
            }
            if (errStockCount.GetError(cmbBucket).Trim().Length > 0)
            {
                sbError.Append(errStockCount.GetError(cmbBucket));
                sbError.AppendLine();
            }
            if (errStockCount.GetError(txtBatchNo).Trim().Length > 0)
            {
                sbError.Append(errStockCount.GetError(txtBatchNo));
                sbError.AppendLine();
            }

            if (errStockCount.GetError(txtSystemQty).Trim().Length > 0)
            {
                sbError.Append(errStockCount.GetError(txtSystemQty));
                sbError.AppendLine();
            }

            if (errStockCount.GetError(txtPhysicalQty).Trim().Length > 0)
            {
                sbError.Append(errStockCount.GetError(txtPhysicalQty));
                sbError.AppendLine();
            }
            return Common.ReturnErrorMessage(sbError);
            //return sbError;

        }
        Boolean ValidateSearchControls()
        {
            errSearch.SetError(dtpSearchFrom, string.Empty);
            ValidateSearchFromDate();

            StringBuilder sbError = new StringBuilder();
            if (errSearch.GetError(dtpSearchFrom).Trim().Length > 0)
            {
                sbError.Append(errSearch.GetError(dtpSearchFrom));
                sbError.AppendLine();
            }

            sbError = Common.ReturnErrorMessage(sbError);

            if (!sbError.ToString().Trim().Equals(string.Empty))
            {
                MessageBox.Show(sbError.ToString(), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        /// <summary>
        /// Validate Creation from and To Date
        /// </summary>
        void ValidateSearchFromDate()
        {
            if (dtpSearchFrom.Checked == true && dtpSearchTo.Checked == true)
            {
                DateTime fromDate = dtpSearchFrom.Checked == true ? Convert.ToDateTime(dtpSearchFrom.Value) : Common.DATETIME_NULL;
                DateTime toDate = dtpSearchTo.Checked == true ? Convert.ToDateTime(dtpSearchTo.Value) : Common.DATETIME_NULL;

                TimeSpan ts = fromDate - toDate;
                if (ts.Days > 0)
                    errSearch.SetError(dtpSearchFrom, Common.GetMessage("VAL0047", lblSearchToDate.Text.Trim().Substring(0, lblSearchToDate.Text.Trim().Length - 1), lblSearchFromDate.Text.Trim().Substring(0, lblSearchFromDate.Text.Trim().Length - 1)));
                else
                    errSearch.SetError(dtpSearchFrom, string.Empty);
            }
        }

        ///// <summary>
        ///// Add Item on add button click 
        ///// </summary>
        ///// <returns></returns>
        bool AddItem()
        {
            m_itemDetail = new ItemStockCount();
            m_itemDetail.ItemId = m_itemId;
            m_itemDetail.ItemCode = txtItemCode.Text.Trim();
            m_itemDetail.ItemName = txtItemDescription.Text.Trim();

            m_itemDetail.BucketId = Convert.ToInt32(cmbBucket.SelectedValue);
            m_itemDetail.BucketName = cmbBucket.Text.ToString();
            m_itemDetail.SystemQty = Convert.ToDecimal(txtSystemQty.Text.Trim().Length == 0 ? "0" : txtSystemQty.Text);

            m_itemDetail.RowNo = m_selectedItemRowNum;

            if (m_lstItemDetail == null)
                m_lstItemDetail = new List<ItemStockCount>();

            if ((m_selectedItemRowIndex != Common.INT_DBNULL) && (m_selectedItemRowIndex <= dgvStockItem.Rows.Count))
            {
                m_lstItemDetail.Insert(m_selectedItemRowIndex, m_itemDetail);
                m_lstItemDetail.RemoveAt(m_selectedItemRowIndex + 1);
            }
            else
                m_lstItemDetail.Add(m_itemDetail);

            ResetItemControl();
            return true;
        }

        /// <summary>
        /// Reset Item When Change of Tab Control
        /// </summary>
        /// <param name="e"></param>
        void tabControlSelect(TabControlCancelEventArgs e)
        {
            if ((tabControlTransaction.SelectedIndex == 0) && dgvStockItem.Rows.Count > 0)
            {

                DialogResult result = MessageBox.Show(Common.GetMessage("VAL0026"), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Cancel | DialogResult.No == result)
                {
                    tabControlTransaction.SelectedIndex = 0;
                    e.Cancel = true;
                }
                else if (result == DialogResult.Yes)
                {
                    ResetTOAndItems();
                    tabControlTransaction.TabPages[1].Text = Common.TAB_CREATE_MODE;
                    dgvSearchStockCount.ClearSelection();
                    ////ReadOnlyTOIField();
                }
            }
            else if (tabControlTransaction.SelectedIndex == 1)
            {
                if (tabControlTransaction.TabPages[1].Text == Common.TAB_CREATE_MODE)
                {
                    ResetTOAndItems();
                    EnableDisableButton((int)Common.TIStatus.New);
                }

            }
        }
        /// <summary>
        /// To Enable alll buttons
        /// </summary>
        void EnableAllButton(bool yesNo)
        {
            btnCreateReset.Enabled = yesNo;
            btnSave.Enabled = yesNo;
            btnClose.Enabled = yesNo;
            btnProcessed.Enabled = yesNo;
            btnCreated.Enabled = yesNo;

            btnInitiated.Enabled = yesNo;
            btnAddDetails.Enabled = yesNo;
            btnCancel.Enabled = yesNo;
            btnAddBatch.Enabled = yesNo;
            btnExecute.Enabled = yesNo;
        }
        /// <summary>
        /// To enable and disable button, when status is changed
        /// </summary>
        /// <param name="statusId"></param>
        void EnableDisableButton(int statusId)
        {
            EnableAllButton(true);
            if (statusId == (int)Common.StockStatus.New)
            {
                btnCancel.Enabled = false;
                btnClose.Enabled = false;
                btnProcessed.Enabled = false;
                btnExecute.Enabled = false;
                btnInitiated.Enabled = false;
                btnAddBatch.Enabled = false;
            }
            else if (statusId == (int)Common.StockStatus.Created)
            {
                btnClose.Enabled = false;
                btnProcessed.Enabled = false;
                btnExecute.Enabled = false;
                btnAddBatch.Enabled = false;
            }
            else if (statusId == (int)Common.StockStatus.Cancelled)
            {
                btnClose.Enabled = false;
                btnProcessed.Enabled = false;
                btnExecute.Enabled = false;
                btnAddBatch.Enabled = false;
                btnCreated.Enabled = false;
                btnCancel.Enabled = false;
                btnAddDetails.Enabled = false;
                btnInitiated.Enabled = false;
            }
            else if (statusId == (int)Common.StockStatus.Initiated || statusId == (int)Common.StockStatus.Processed)
            {
                btnCancel.Enabled = false;
                btnCreated.Enabled = false;
                btnInitiated.Enabled = false;
                btnClose.Enabled = false;
                btnAddDetails.Enabled = false;
                dtpStockCountDate.Enabled = false;
            }
            else if (statusId == (int)Common.StockStatus.Executed)
            {
                btnProcessed.Enabled = false;
                btnExecute.Enabled = false;
                btnCancel.Enabled = false;
                btnCreated.Enabled = false;
                btnInitiated.Enabled = false;
                btnAddDetails.Enabled = false;
                btnAddBatch.Enabled = false;
                dtpStockCountDate.Enabled = false;
            }
            else if (statusId == (int)Common.StockStatus.Closed)
            {
                EnableAllButton(false);
                btnCreateReset.Enabled = true;
                dtpStockCountDate.Enabled = false;
            }

            btnCreated.Enabled = btnCreated.Enabled & m_isCreateAvailable;
            btnCancel.Enabled = btnCancel.Enabled & m_isCancelAvailable;
            btnInitiated.Enabled = btnInitiated.Enabled & m_isInitiateAvailable;
            btnProcessed.Enabled = btnProcessed.Enabled & m_isProcessAvailable;
            btnExecute.Enabled = btnExecute.Enabled & m_isExecuteAvailable;
            btnClose.Enabled = btnClose.Enabled & m_isCloseAvailable;
            btnPrint.Enabled = m_isPrintAvailable;
            btnSearch.Enabled = m_isSearchAvailable;

            if (m_locationType == (int)Common.LocationConfigId.BO || m_currentLocationId == (int)Common.LocationConfigId.WH)
            {
                btnCreateReset.Enabled = false;
                btnAddDetails.Enabled = false;
            }
            if (m_locationType == (int)Common.LocationConfigId.HO)
            {
                btnAddBatch.Enabled = false;
            }
        }
        /// <summary>
        /// Reset TO and Item Controls
        /// </summary>
        void ResetTOAndItems()
        {
            txtStatus.Text = Common.StockStatus.New.ToString();
            txtRemarks.Text = string.Empty;
            cmbLocation.SelectedIndex = 0;
            dtpStockCountDate.Checked = false;
            txtStockCountNo.Text = string.Empty;
            m_seqNo = string.Empty;

            txtStockCountBy.Text = string.Empty;
            txtStockCountVerifiedBy.Text = string.Empty;

            m_objStockCount = null;
            dtpStockCountDate.Checked = false;
            dtpStockCountDate.Value = Convert.ToDateTime(System.DateTime.Now.ToShortDateString());

            ResetItemControl();
            ResetItemBatchControl();
            m_lstItemDetail = null;
            m_lstItemBatchDetail = null;
            dgvStockItem.DataSource = new List<ItemStockCount>();
            dgvStockItemBatch.DataSource = new List<ItemStockBatch>();

            ReadOnlyHeader();
            ReadOnlyItemDetail();
            ReadOnlyBatchDetail();

            EmptyErrorProvider();
        }

        void BindItemBatchGrid()
        {
            if (m_lstItemBatchDetail != null && m_lstItemBatchDetail.Count > 0)
            {
                var query = from p in m_lstItemBatchDetail where p.ItemRowNo == m_selectedItemRowNum || m_selectedItemRowNum == Common.INT_DBNULL select p;

                dgvStockItemBatch.SelectionChanged -= new System.EventHandler(dgvStockItemBatch_SelectionChanged);
                if (query.ToList().Count() > 0)
                {
                    dgvStockItemBatch.DataSource = new List<ItemStockBatch>();

                    dgvStockItemBatch.DataSource = (List<ItemStockBatch>)query.ToList();
                    ResetItemBatchControl();
                }
                else
                    dgvStockItemBatch.DataSource = new List<ItemStockBatch>();

                dgvStockItemBatch.SelectionChanged += new System.EventHandler(dgvStockItemBatch_SelectionChanged);
            }
        }

        void SelectSearchGrid(string seqNo)
        {
            m_lstItemDetail = m_objStockCount.SearchItem(seqNo);
            m_lstItemBatchDetail = m_objStockCount.SearchItemBatch(seqNo);

            m_seqNo = seqNo;
            tabControlTransaction.TabPages[1].Text = Common.TAB_UPDATE_MODE;
            tabControlTransaction.SelectedIndex = 1;

            #region Bind Item/Item Batch Grid
            BindItemGrid();
            #endregion

            FillBucket(true);

            cmbLocation.SelectedValue = m_objStockCount.LocationId.ToString();
            cmbLocation.Enabled = false;
            if (m_objStockCount.InitiatedDate.Length > 0)
            {
                dtpStockCountDate.Checked = true;
                dtpStockCountDate.Value = Convert.ToDateTime(m_objStockCount.InitiatedDate);
            }
            else
            { dtpStockCountDate.Checked = false; }

            EnableDisableButton(m_objStockCount.StatusId);

            txtStatus.Text = m_objStockCount.StatusName;
            txtStockCountVerifiedBy.Text = m_objStockCount.InitiatedName;
            txtStockCountBy.Text = m_objStockCount.ExecutedName;
            txtRemarks.Text = m_objStockCount.Remarks;
            txtStockCountNo.Text = m_objStockCount.SeqNo;
            ReadOnlyItemDetail();
            ReadOnlyBatchDetail();
            ReadOnlyHeader();

            EmptyErrorProvider();
        }

        /// <summary>
        /// Edit TOI
        /// </summary>
        /// <param name="e"></param> 
        private void EditStockCount(DataGridViewCellEventArgs e)
        {
            if ((e.RowIndex >= 0) && (dgvSearchStockCount.Rows[e.RowIndex].Cells[e.ColumnIndex].GetType() == typeof(DataGridViewImageCell)))
            {
                m_objStockCount = m_lstStockCount[e.RowIndex];

                SelectSearchGrid(m_objStockCount.SeqNo);
            }
        }

        /// <summary>
        /// Clear Batch Info
        /// </summary>
        void ClearBatchInfor()
        {
            txtBatchNo.Text = string.Empty;
            txtSystemQty.Text = string.Empty;
            txtPhysicalQty.Text = string.Empty;
            // txtRemarks.Text = string.Empty;
            m_batchNo = string.Empty;
        }

        /// <summary>
        /// Fill Item Price Information into UOM, name of Item
        /// </summary>
        /// <param name="itemCode"></param>
        void FillItemPriceInfo(string itemCode, Boolean yesNo)
        {
            bool invalid = false;

            //ItemDetails itemDetails = new ItemDetails();
            //itemDetails.LocationId = cmbLocation.SelectedValue.ToString();

            List<ItemDetails> lst = new List<ItemDetails>();
            //lst = itemDetails.SearchLocationItem();

           
            ItemDetails id = new ItemDetails();
            //List<ItemDetails> lst = new List<ItemDetails>();
            lst=id.Search();

            var query = from p in lst where p.ItemCode.ToLower().Trim() == itemCode.ToLower().Trim() select p;
            if (query.ToList().Count > 0)
            {
            //List<ItemDetails> Search()

                DataTable dt = Common.ParameterLookup(Common.ParameterType.InventoryBucketBatchLocation, new ParameterFilter(string.Empty, 0, 0, 0));
                DataView dv = dt.DefaultView;
                dv.RowFilter = "ItemCode = '" + itemCode + "'";
                if (dv.Count > 0)
                {
                    m_itemId = query.ToList()[0].ItemId;
                   
                    txtItemDescription.Text = query.ToList()[0].ItemName;
                    FillBucket(yesNo);
                }
                else
                    invalid = true;
            }
            else
                invalid = true;

            if (invalid == true)
            {
                if (Convert.ToInt32(cmbLocation.SelectedValue) == Common.INT_DBNULL)
                    errStockCount.SetError(txtItemCode, Common.GetMessage("VAL0053", lblItemCode.Text.Trim().Substring(0, lblItemCode.Text.Trim().Length - 2)));
                else
                    errStockCount.SetError(txtItemCode, Common.GetMessage("VAL0006", lblItemCode.Text.Trim().Substring(0, lblItemCode.Text.Trim().Length - 2)));
                //errStockCount.SetError(txtItemCode, Common.GetMessage("VAL0006", lblItemCode.Text.Trim().Substring(0, lblItemCode.Text.Trim().Length - 2)));
                //txtWeight.Text = string.Empty;
                //txtUOMName.Text = string.Empty;
                txtItemDescription.Text = string.Empty;
                //m_UOMName = string.Empty;
                //m_UOMId = Common.INT_DBNULL;
                m_itemId = Common.INT_DBNULL;
                //m_weight = 0;
            }
        }

        /// <summary>
        /// Clear Item Info
        /// </summary>
        void ClearItemInfo()
        {
            //txtWeight.Text = string.Empty;
            //txtUOMName.Text = string.Empty;
            txtItemDescription.Text = string.Empty;
            //m_UOMName = string.Empty;
            //m_UOMId = Common.INT_DBNULL;
            m_itemId = Common.INT_DBNULL;
            //m_weight = 0;

        }

        /// <summary>
        /// Validate Item Code
        /// </summary>
        /// <param name="yesNo"></param>
        void ValidateItemCode(Boolean yesNo)
        {
            bool isTextBoxEmpty = CoreComponent.Core.BusinessObjects.Validators.CheckForEmptyString(txtItemCode.Text.Trim().Length);
            if (isTextBoxEmpty == true)
            {
                ClearItemInfo();
                ClearBatchInfor();

                if (yesNo == false)
                    errStockCount.SetError(txtItemCode, Common.GetMessage("INF0019", lblItemCode.Text.Trim().Substring(0, lblItemCode.Text.Trim().Length - 2)));
                else
                    errStockCount.SetError(txtItemCode, string.Empty);
            }
            else if (isTextBoxEmpty == false)
            {
                errStockCount.SetError(txtItemCode, string.Empty);
                if (yesNo)
                {
                    errStockCount.SetError(txtItemCode, string.Empty);
                    FillBucket(yesNo);
                    ClearBatchInfor();
                }
                FillItemPriceInfo(txtItemCode.Text.Trim(), yesNo);
            }
        }
        /// <summary>
        /// Get Batch Information
        /// </summary>
        /// <param name="objItem"></param>
        void GetBatchInfo(ItemBatchDetails objItem)
        {
            m_mfgbatchNo = objItem.ManufactureBatchNo.ToString();
            txtBatchNo.Text = objItem.ManufactureBatchNo.ToString();
            m_batchNo = objItem.BatchNo.ToString();
            txtAvailableQty.Text = Math.Round(objItem.Quantity, 2).ToString();
        }

        /// <summary>
        /// Open new window form when F4 key is pressed
        /// </summary>
        /// <param name="e"></param>
        void BatchNoKeyDown(KeyEventArgs e)
        {
            if (m_selectedItemRowIndex <= Common.INT_DBNULL)
            {
                MessageBox.Show(Common.GetMessage("VAL0056"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            NameValueCollection nvc = new NameValueCollection();
            nvc.Add("ItemCode", txtItemCode.Text.Trim());
            if (cmbBucket.SelectedIndex == 0)
                nvc.Add("BucketId", "-2");
            else
                nvc.Add("BucketId", cmbBucket.SelectedValue.ToString());

            nvc.Add("LocationId", cmbLocation.SelectedValue.ToString());

            CoreComponent.Controls.frmSearch objfrmSearch = new CoreComponent.Controls.frmSearch(CoreComponent.Controls.SearchTypes.ItemBatch, nvc);
            objfrmSearch.ShowDialog();
            CoreComponent.MasterData.BusinessObjects.ItemBatchDetails objItem = (CoreComponent.MasterData.BusinessObjects.ItemBatchDetails)objfrmSearch.ReturnObject;
            if (objItem != null)
            {
                errStockCount.SetError(txtBatchNo, string.Empty);
                m_F4Press = true;
                GetBatchInfo(objItem);
            }
        }
        /// <summary>
        /// Copy Item into seperate List exclusive of current selected item
        /// </summary>
        /// <param name="excludeIndex"></param>
        /// <param name="lst"></param>
        /// <returns></returns>
        List<ItemStockBatch> CopyItemBatchDetail(int excludeIndex, List<ItemStockBatch> lst)
        {
            List<ItemStockBatch> returnList = new List<ItemStockBatch>();
            for (int i = 0; i < lst.Count; i++)
            {
                if (i != excludeIndex)
                {
                    ItemStockBatch tdetail = new ItemStockBatch();
                    tdetail = lst[i];
                    returnList.Add(tdetail);
                }
            }
            return returnList;
        }

        /// <summary>
        /// Copy Item into seperate List exclusive of current selected item
        /// </summary>
        /// <param name="excludeIndex"></param>
        /// <param name="lst"></param>
        /// <returns></returns>
        List<ItemStockCount> CopyItemDetail(int excludeIndex, List<ItemStockCount> lst)
        {
            List<ItemStockCount> returnList = new List<ItemStockCount>();
            for (int i = 0; i < lst.Count; i++)
            {
                if (i != excludeIndex)
                {
                    ItemStockCount tdetail = new ItemStockCount();
                    tdetail = lst[i];
                    returnList.Add(tdetail);
                }
            }

            return returnList;
        }
        /// <summary>
        /// To find out valid Quantity 
        /// </summary>
        /// <returns></returns>
        bool CheckValidQuantity()
        {
            if (m_locationType == (int)Common.LocationConfigId.HO)
            {
                // Check Duplicate For ItemCode and Batch No, from bucket and To Bucket
                //if (txtSystemQty.Text.Length > 0)
                //{
                    if (m_lstItemDetail != null && m_lstItemDetail.Count > 0)
                    {
                        List<ItemStockCount> tiDetail = CopyItemDetail(m_selectedItemRowIndex, m_lstItemDetail);
                        //checked based on ItemCode and Bucket Id
                        m_isDuplicateRecordFound = (from p in tiDetail where p.ItemCode.Trim().ToLower() == txtItemCode.Text.Trim().ToLower() && p.BucketId == Convert.ToInt32(cmbBucket.SelectedValue) select p).Count();

                        if (m_isDuplicateRecordFound > 0)
                        {
                            MessageBox.Show(Common.GetMessage("VAL0063", lblItemCode.Text.Trim().Substring(0, lblItemCode.Text.Trim().Length - 2) + ' ' + "Bucket"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    //}
                }

            }
            else if (m_locationType == (int)Common.LocationConfigId.BO || m_locationType == (int)Common.LocationConfigId.WH)
            {
                // Check Duplicate For ItemCode and Batch No, from bucket and To Bucket
                if (txtPhysicalQty.Text.Length > 0)
                {
                    if (m_lstItemBatchDetail != null && m_lstItemBatchDetail.Count > 0)
                    {
                        List<ItemStockBatch> tiDetail = CopyItemBatchDetail(m_selectedItemBatchRowIndex, m_lstItemBatchDetail);
                        //checked based on ItemCode and Bucket Id
                        m_isDuplicateRecordFound = (from p in tiDetail where p.BatchNo.Trim().ToLower() == m_batchNo.Trim().ToLower() && p.ItemRowNo == m_itemRowNo select p).Count();

                        if (m_isDuplicateRecordFound > 0)
                        {
                            MessageBox.Show(Common.GetMessage("VAL0063", lblBatchNo.Text.Substring(0, lblBatchNo.Text.Trim().Length - 2)), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }

                        //EvaluateTotal Physically Quantity Enetered 
                        /*
                         var lstItemBatchGroupBy = tiDetail.GroupBy(x => new { x.ItemRowNo})
                                       .Select(g => new
                                       {
                                           BatchNo = g.First<ItemStockBatch>().BatchNo.ToLower(),
                                           TotalPhysicalQty = g.Sum(t => t.PhysicalQty),
                                           ManBatchNo = g.First<ItemStockBatch>().ManufactureBatchNo.ToLower(),
                                           PhysicalQty = g.First<ItemStockBatch>().PhysicalQty
                                       });



                        var query = (from p in tiDetail where p.ItemRowNo == m_itemRowNo select p);
                        if (lstItemBatchGroupBy.ToList().Count > 0)
                        {
                            if (Convert.ToDecimal(lstItemBatchGroupBy.ToList()[0].TotalPhysicalQty) + Convert.ToDecimal(txtPhysicalQty.Text)>m_systemQty)
                            {
                                //m_invalidQuantity = true;
                            }
                        }
                         */
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Empty Error Provider for all controls
        /// </summary>
        void EmptyErrorProvider()
        {
            errStockCount.SetError(dtpStockCountDate, string.Empty);
            errStockCount.SetError(cmbLocation, string.Empty);
            errStockCount.SetError(cmbBucket, string.Empty);

            errStockCount.SetError(txtSystemQty, string.Empty);
            errStockCount.SetError(txtPhysicalQty, string.Empty);
            errStockCount.SetError(txtItemCode, string.Empty);
            errStockCount.SetError(txtBatchNo, string.Empty);
        }

        /// <summary>
        /// Validate Starting Amount
        /// </summary>
        void ValidateQuantity(TextBox txt, Label lbl)
        {
            bool isValidQuantity = CoreComponent.Core.BusinessObjects.Validators.IsValidQuantity(txt.Text);

            if (isValidQuantity == false)
                errStockCount.SetError(txt, Common.GetMessage("VAL0009", lbl.Text.Trim().Substring(0, lbl.Text.Trim().Length - 2)));
            else if (Convert.ToDecimal(txt.Text) < 0)
                errStockCount.SetError(txt, Common.GetMessage("VAL0009", lbl.Text.Trim().Substring(0, lbl.Text.Trim().Length - 2)));
            else
                errStockCount.SetError(txt, string.Empty);
        }

        /// <summary>
        /// Validate Combo Box
        /// </summary>
        /// <param name="cmb"></param>
        /// <param name="lbl"></param>
        private void ValidateCombo(ComboBox cmb, Label lbl)
        {
            if (cmb.SelectedIndex == 0)
                errStockCount.SetError(cmb, Common.GetMessage("INF0026", lbl.Text.Trim().Substring(0, lbl.Text.Trim().Length - 2)));
            else
                errStockCount.SetError(cmb, string.Empty);
        }

        void ValidateItemBatchMessages()
        {
            if (m_locationType == (int)Common.LocationConfigId.BO || m_locationType == (int)Common.LocationConfigId.WH)
            {
                ValidateBatchNo(false);

                if ((m_objStockCount != null) && (m_objStockCount.StatusId == (int)Common.StockStatus.Initiated || m_objStockCount.StatusId == (int)Common.StockStatus.Processed))
                    ValidateQuantity(txtPhysicalQty, lblPhysicalQty);
            }
        }

        /// <summary>
        /// Validate Controls On Add Button Click
        /// </summary>
        void ValidateItemMessages()
        {

            if (m_locationType == (int)Common.LocationConfigId.HO)
            {
                // ValidateQuantity(txtSystemQty, lblSystemQty);
                ValidateItemCode(false);
                ValidateCombo(cmbBucket, lblBucketName);
            }
        }
        /// <summary>
        /// Show Available Quantity, On Bucket changed
        /// </summary>
        void FillBucketInfo()
        {
            if (txtItemCode.Text.Trim().Length > 0 && cmbBucket.SelectedIndex > 0 && cmbBucket.SelectedIndex > 0)
            {
                DataRow[] dr = m_dtSearchAllBucket.Select("BucketId = " + Convert.ToInt32(cmbBucket.SelectedValue) + " And LocationId = " + Convert.ToInt32(cmbLocation.SelectedValue) + " And ItemCode = '" + txtItemCode.Text.Trim() + "'");
                //DataRow[] dr = m_dtSearchAllBucket.Select("BucketId = " + Convert.ToInt32(cmbBucket.SelectedValue));
                if (dr.Count() > 0)
                    txtSystemQty.Text = Math.Round(Convert.ToDecimal(dr[0]["Quantity"]), 2).ToString();
                else
                    txtSystemQty.Text = string.Empty;
            }
        }

        void BindItemGrid()
        {

            dgvStockItem.SelectionChanged -= new System.EventHandler(dgvStockItem_SelectionChanged);
            dgvStockItem.DataSource = new List<ItemStockCount>();
            if (m_lstItemDetail != null && m_lstItemDetail.Count > 0)
            {
                dgvStockItem.DataSource = m_lstItemDetail;
                cmbBucket.SelectedIndex = 0;
                //ResetItemControl();
            }
            dgvStockItem.SelectionChanged += new System.EventHandler(dgvStockItem_SelectionChanged);
        }

        /// <summary>
        /// Validate Batch No.
        /// </summary>
        void ValidateBatchNo(Boolean yesNo)
        {
            if (m_selectedItemRowIndex <= Common.INT_DBNULL && yesNo == false)
            {
                //MessageBox.Show(Common.GetMessage("VAL0056"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                errStockCount.SetError(txtBatchNo, Common.GetMessage("VAL0056", lblBatchNo.Text.Trim().Substring(0, lblBatchNo.Text.Trim().Length - 2)));
                return;
            }


            if (m_locationType == (int)Common.LocationConfigId.BO || m_locationType == (int)Common.LocationConfigId.WH)
            {
                bool isTextBoxEmpty = CoreComponent.Core.BusinessObjects.Validators.CheckForEmptyString(txtBatchNo.Text.Trim().Length);
                if (isTextBoxEmpty == true && yesNo == false)
                    errStockCount.SetError(txtBatchNo, Common.GetMessage("INF0019", lblBatchNo.Text.Trim().Substring(0, lblBatchNo.Text.Trim().Length - 2)));
                else if (isTextBoxEmpty == true)
                    errStockCount.SetError(txtBatchNo, string.Empty);
                else if (isTextBoxEmpty == false)
                {
                    if ((m_F4Press == false && yesNo == true) || (m_mfgbatchNo.Trim().ToLower() != txtBatchNo.Text.Trim().ToLower()))
                    {
                        errStockCount.SetError(txtBatchNo, string.Empty);

                        if (m_selectedItemRowIndex <= Common.INT_DBNULL)
                        {
                            //MessageBox.Show(Common.GetMessage("VAL0056"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            errStockCount.SetError(txtBatchNo, Common.GetMessage("VAL0056", lblBatchNo.Text.Trim().Substring(0, lblBatchNo.Text.Trim().Length - 2)));
                            return;
                        }
                        ItemBatchDetails objItem = new ItemBatchDetails();
                        objItem.ManufactureBatchNo = txtBatchNo.Text.Trim();
                        objItem.ItemCode = txtItemCode.Text.Trim();

                        if (m_locationType == (int)Common.LocationConfigId.BO || m_locationType == (int)Common.LocationConfigId.WH)
                            objItem.LocationId = m_currentLocationId.ToString();
                        else
                            objItem.LocationId = Common.INT_DBNULL.ToString();

                        objItem.BucketId = m_bucketid.ToString();
                        objItem.FromMfgDate = Common.DATETIME_NULL;
                        objItem.ToMfgDate = Common.DATETIME_MAX;
                        List<ItemBatchDetails> lstItemDetails = objItem.Search();
                        if (txtItemCode.Text.Length > 0 && lstItemDetails != null)
                        {
                            List<ItemBatchDetails> query;

                            if (m_batchNo.Length > 0 && m_selectedItemBatchRowIndex >= 0)
                                query = (from p in lstItemDetails where p.ItemId == m_itemId && p.BatchNo.ToLower().Trim() == m_batchNo.ToLower().Trim() select p).ToList();
                            else
                                query = (from p in lstItemDetails where p.ItemId == m_itemId select p).ToList();

                            lstItemDetails = (List<ItemBatchDetails>)query;

                            if (lstItemDetails.Count == 1)
                            {
                                objItem = lstItemDetails[0];
                                GetBatchInfo(objItem);
                            }
                            else if (lstItemDetails.Count > 1)
                                errStockCount.SetError(txtBatchNo, Common.GetMessage("VAL0038", lblBatchNo.Text.Trim().Substring(0, lblBatchNo.Text.Trim().Length - 2)));
                            else
                                errStockCount.SetError(txtBatchNo, Common.GetMessage("VAL0006", lblBatchNo.Text.Trim().Substring(0, lblBatchNo.Text.Trim().Length - 2)));
                        }
                        else
                        {
                            errStockCount.SetError(txtBatchNo, Common.GetMessage("VAL0006", lblBatchNo.Text.Trim().Substring(0, lblBatchNo.Text.Trim().Length - 2)));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Calll FillBucketInfo function to show available quantity
        /// </summary>
        /// <param name="yesNo"></param>
        void BucketChanged(bool yesNo)
        {
            if (cmbBucket.SelectedIndex > 0)
            {
                errStockCount.SetError(cmbBucket, string.Empty);
                FillBucketInfo();
            }
            else
            {
                txtSystemQty.Text = string.Empty;
                if (yesNo == false)
                    errStockCount.SetError(cmbBucket, Common.GetMessage("INF0026", lblBucketName.Text.Trim().Substring(0, lblBucketName.Text.Trim().Length - 2)));
            }
        }

        Boolean ValidateAddButton()//AddButtonClick(DataGridView dgvStockItem)
        {



            #region Check Errors
            StringBuilder sbError = new StringBuilder();
            sbError = GenerateError();
            #endregion

            if (!sbError.ToString().Trim().Equals(string.Empty))
            {
                MessageBox.Show(sbError.ToString(), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;

        }


        /// <summary>
        /// Search TOIs
        /// </summary>
        List<StockCount> SearchStockCount()
        {
            List<StockCount> lstStockCount = new List<StockCount>();

            DateTime fromDate = dtpSearchFrom.Checked == true ? Convert.ToDateTime(dtpSearchFrom.Value) : Common.DATETIME_NULL;
            DateTime toDate = dtpSearchTo.Checked == true ? Convert.ToDateTime(dtpSearchTo.Value) : Common.DATETIME_MAX;

            lstStockCount = Search(Convert.ToInt32(cmbSearchLocation.SelectedValue), txtSeqNo.Text.Trim(), Convert.ToInt32(cmbSearchStatus.SelectedValue), fromDate.ToString(Common.DATE_TIME_FORMAT), toDate.ToString(Common.DATE_TIME_FORMAT), Convert.ToInt32(cmbStockCountBy.SelectedValue));

            return lstStockCount;
        }

        /// <summary>
        /// Bind Grid
        /// </summary>
        void BindGrid()
        {
            m_lstStockCount = SearchStockCount();
            if ((m_lstStockCount != null) && (m_lstStockCount.Count > 0))
            {
                dgvSearchStockCount.DataSource = m_lstStockCount;
                dgvSearchStockCount.ClearSelection();
                //dgvInventory.Select();
                ResetItemControl();
            }
            else
            {
                dgvSearchStockCount.DataSource = new List<InventoryAdjust>();
                MessageBox.Show(Common.GetMessage("8002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Return List of listStock Count
        /// </summary>
        /// <param name="locationId"></param>
        /// <param name="adjustNo"></param>
        /// <param name="statusId"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        List<StockCount> Search(int locationId, string seqNo, int statusId, string fromDate, string toDate, int userId)
        {
            List<StockCount> listStockCount = new List<StockCount>();
            StockCount objStockCount = new StockCount();
            objStockCount.LocationId = locationId;
            objStockCount.SeqNo = seqNo;
            objStockCount.StatusId = statusId;
            objStockCount.FromDate = fromDate;
            objStockCount.ToDate = toDate;
            objStockCount.InititatedBy = userId;

            listStockCount = objStockCount.Search();

            return listStockCount;
        }

        bool AddBatchItem()
        {

            m_itemStockBatch = new ItemStockBatch();
            m_itemStockBatch.ItemRowNo = m_selectedItemRowNum;

            m_itemStockBatch.ManufactureBatchNo = txtBatchNo.Text.Trim();

            m_itemStockBatch.BatchNo = m_batchNo;

            m_itemStockBatch.PhysicalQty = Convert.ToDecimal(txtPhysicalQty.Text.Trim().Length == 0 ? "0" : txtPhysicalQty.Text);

            m_itemStockBatch.RowNo = m_selectedItemBatchRowIndex;

            if (m_lstItemDetail == null)
                m_lstItemBatchDetail = new List<ItemStockBatch>();

            if ((m_selectedItemBatchRowIndex != Common.INT_DBNULL) && (m_selectedItemBatchRowIndex <= dgvStockItemBatch.Rows.Count))
            {
                m_lstItemBatchDetail.Insert(m_selectedItemBatchRowIndex, m_itemStockBatch);
                m_lstItemBatchDetail.RemoveAt(m_selectedItemBatchRowIndex + 1);
            }
            else
                m_lstItemBatchDetail.Add(m_itemStockBatch);



            return true;
        }

        ItemStockCount GetItemInfo(int rowIndex)
        {
            m_isDuplicateRecordFound = Common.INT_DBNULL;


            string itemCode = dgvStockItem.Rows[rowIndex].Cells["ItemCode"].Value.ToString().Trim();

            m_bucketid = Convert.ToInt32(dgvStockItem.Rows[rowIndex].Cells["BucketId"].Value.ToString());

            //m_RowNo = Convert.ToInt32(dgvTOIItem.Rows[e.RowIndex].Cells["RowNo"].Value.ToString().Trim());

            ////Get ParentId
            if (m_lstItemDetail == null)
                return null;

            var itemSelect = (from p in m_lstItemDetail where p.ItemCode.Trim().ToLower() == itemCode.Trim().ToLower() && p.BucketId == m_bucketid select p);

            if (itemSelect.ToList().Count == 0)
                return null;

            m_selectedItemRowIndex = rowIndex;
            m_itemDetail = itemSelect.ToList()[0];

            m_selectedItemRowNum = m_itemDetail.RowNo;
            m_itemId = m_itemDetail.ItemId;
            m_itemRowNo = m_itemDetail.RowNo;
            m_systemQty = m_itemDetail.SystemQty;
            return m_itemDetail;

        }

        void ValidateStockCountDate()
        {
            if (dtpStockCountDate.Checked == false)
                errStockCount.SetError(dtpStockCountDate, Common.GetMessage("VAL0002", lblStockCountDate.Text.Trim().Substring(0, lblStockCountDate.Text.Trim().Length - 2)));
            else if (dtpStockCountDate.Checked == true)
            {
                DateTime expectedDate = dtpStockCountDate.Checked == true ? Convert.ToDateTime(dtpStockCountDate.Value) : Common.DATETIME_NULL;
                DateTime dt = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                TimeSpan ts = expectedDate - dt;
                if (ts.Days < 0)
                    errStockCount.SetError(dtpStockCountDate, Common.GetMessage("INF0010", lblStockCountDate.Text.Trim().Substring(0, lblStockCountDate.Text.Trim().Length - 2)));
                else
                    errStockCount.SetError(dtpStockCountDate, string.Empty);
            }
        }

        void Save(int statusId)
        {
            if (m_lstItemDetail == null || m_lstItemDetail.Count == 0)
            {
                MessageBox.Show(Common.GetMessage("VAL0024", lblItemCode.Text.Trim().Substring(0, lblItemCode.Text.Trim().Length - 2)), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            #region Check Errors, If Validation fails Show Error and Return From Prog.
            EmptyErrorProvider();
            ValidateCombo(cmbLocation, lblLocation);
            if (m_objStockCount != null && statusId == (int)Common.StockStatus.Initiated)
                ValidateStockCountDate();

            StringBuilder sbError = new StringBuilder();
            sbError = GenerateError();

            //If Validation fails Show Error and Return From Prog. 
            if (!sbError.ToString().Trim().Equals(string.Empty))
            {
                MessageBox.Show(sbError.ToString(), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            #endregion


            if ((statusId == (int)Common.StockStatus.Executed || statusId == (int)Common.StockStatus.Processed) && (m_lstItemBatchDetail == null || m_lstItemBatchDetail.Count == 0))
            {
                MessageBox.Show(Common.GetMessage("VAL0024", lblBatchNo.Text.Trim().Substring(0, lblBatchNo.Text.Trim().Length - 2)), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            MemberInfo[] memberInfos = typeof(Common.StockStatus).GetMembers(BindingFlags.Public | BindingFlags.Static);
            // Confirmation Before Saving, statusId
            DialogResult saveResult = MessageBox.Show(Common.GetMessage("5010", Common.GetConfirmationStatusText(memberInfos[statusId].Name)), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (saveResult == DialogResult.Yes)
            {
                if (m_lstStockCount != null && m_objStockCount != null && m_seqNo != string.Empty)
                {
                    var query = (from p in m_lstStockCount where p.SeqNo == m_seqNo select p);

                    if (query.ToList().Count > 0)
                        m_objStockCount = query.ToList()[0];
                    m_objStockCount.CreatedBy = Common.INT_DBNULL;
                    m_objStockCount.CreatedDate = string.Empty;
                }
                if (m_objStockCount == null)
                {
                    m_objStockCount = new StockCount();
                    m_objStockCount.SeqNo = string.Empty;
                    m_objStockCount.StatusId = (int)Common.TOIStatus.Created;

                    m_objStockCount.CreatedBy = m_userId;
                    m_objStockCount.CreatedDate = System.DateTime.Now.ToString(Common.DATE_TIME_FORMAT);

                }

                if (m_objStockCount != null)
                {
                    if (m_objStockCount.ModifiedDate != null && m_objStockCount.ModifiedDate.Length > 0)
                        m_objStockCount.ModifiedDate = Convert.ToDateTime(m_objStockCount.ModifiedDate).ToString(Common.DATE_TIME_FORMAT);//.ToString(Common.DATE_TIME_FORMAT);
                }

                DateTime stockCountDate = dtpStockCountDate.Checked == true ? Convert.ToDateTime(dtpStockCountDate.Value) : Common.DATETIME_NULL;

                m_objStockCount.LocationId = Convert.ToInt32(cmbLocation.SelectedValue);
                m_objStockCount.InitiatedDate = stockCountDate.ToString(Common.DATE_TIME_FORMAT);

                m_objStockCount.ModifiedBy = m_userId;
                m_objStockCount.StockCountItem = m_lstItemDetail;
                m_objStockCount.ItemBatch = m_lstItemBatchDetail;
                m_objStockCount.Remarks = txtRemarks.Text.Trim();
                m_objStockCount.StatusId = statusId;

                string errorMessage = string.Empty;

                bool result = m_objStockCount.Save(Common.ToXml(m_objStockCount), ref errorMessage);

                if (errorMessage.Equals(string.Empty))
                {
                    List<StockCount> lstStockCount = new List<StockCount>();
                    lstStockCount = Search(Convert.ToInt32(cmbLocation.SelectedValue), m_objStockCount.SeqNo.Trim(), Common.INT_DBNULL, Common.DATETIME_NULL.ToString(Common.DATE_TIME_FORMAT), Common.DATETIME_NULL.ToString(Common.DATE_TIME_FORMAT), Common.INT_DBNULL);

                    if (lstStockCount != null)
                    {
                        txtStatus.Text = lstStockCount[0].StatusName.Trim();
                        
                        if (lstStockCount[0].StatusId == (int)Common.StockStatus.Executed)
                            txtStockCountBy.Text = lstStockCount[0].ExecutedName.Trim();
                        if (lstStockCount[0].StatusId == (int)Common.StockStatus.Initiated)
                            txtStockCountVerifiedBy.Text = lstStockCount[0].InitiatedName.Trim();
                        m_seqNo = lstStockCount[0].SeqNo;
                        ReadOnlyHeader();
                        ReadOnlyItemDetail();
                        ReadOnlyBatchDetail();
                        ResetItemControl();
                        ResetItemBatchControl();

                        EnableDisableButton(statusId);

                        dgvStockItem.DataSource = new List<StockCount>();
                        if (m_lstItemDetail != null && m_lstItemDetail.Count > 0)
                        {
                            m_lstItemDetail = m_objStockCount.SearchItem(m_objStockCount.SeqNo);
                            BindItemGrid();
                        }

                        if (m_lstItemBatchDetail != null && m_lstItemBatchDetail.Count > 0)
                        {
                            m_lstItemBatchDetail = m_objStockCount.SearchItemBatch(m_objStockCount.SeqNo);
                            BindItemBatchGrid();
                        }

                    }
                    //MessageBox.Show(Common.GetMessage("8001"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MessageBox.Show(Common.GetMessage("8013", memberInfos[statusId].Name, m_objStockCount.SeqNo), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtStockCountNo.Text = m_objStockCount.SeqNo;
                }
                else
                    MessageBox.Show(Common.GetMessage(errorMessage), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void ReadOnlyHeader()
        {
            if (m_locationType == (int)Common.LocationConfigId.HO)
            {
                if ((m_objStockCount == null) || (m_objStockCount != null && m_objStockCount.StatusId <= (int)Common.StockStatus.Created))
                {

                    if ((m_lstItemDetail != null && m_lstItemDetail.Count > 0))
                        cmbLocation.Enabled = false;
                    else
                        cmbLocation.Enabled = true;
                }
                else
                {
                    cmbLocation.Enabled = false;
                }
            }

            if (m_objStockCount != null && m_objStockCount.StatusId == (int)Common.StockStatus.Executed)
                txtRemarks.Enabled = true;
            else
                txtRemarks.Enabled = false;

            if (m_objStockCount != null && m_objStockCount.StatusId == (int)Common.StockStatus.Created)
                dtpStockCountDate.Enabled = true;
            else
                dtpStockCountDate.Enabled = false;
        }
        /// <summary>
        /// This function is used to show data in controls
        /// </summary>
        /// <param name="e"></param>
        private void SelectItemGridRow(EventArgs e)
        {
            if (dgvStockItem.SelectedCells.Count > 0)
            {
                int rowIndex = dgvStockItem.SelectedCells[0].RowIndex;
                int columnIndex = dgvStockItem.SelectedCells[0].ColumnIndex;

                if (rowIndex >= 0 && columnIndex >= 0)
                {
                    int selectedRow = dgvStockItem.SelectedCells[0].RowIndex;
                    m_itemDetail = GetItemInfo(rowIndex);

                    txtBatchNo.Text = string.Empty;
                    txtPhysicalQty.Text = string.Empty;

                    txtItemCode.Text = m_itemDetail.ItemCode;
                    txtItemDescription.Text = m_itemDetail.ItemName;
                    txtSystemQty.Text = m_itemDetail.DisplaySystemQty.ToString();
                    cmbBucket.SelectedValue = Convert.ToInt32(m_itemDetail.BucketId);
                    m_itemRowNo = m_itemDetail.RowNo;

                    ReadOnlyItemDetail();

                    BindItemBatchGrid();

                }
            }
        }

        /// <summary>
        /// This function is used to show data in controls
        /// </summary>
        /// <param name="e"></param>
        private void SelectItemBatchGridRow(EventArgs e)
        {
            if (dgvStockItemBatch.SelectedCells.Count > 0)
            {
                int rowIndex = dgvStockItemBatch.CurrentCell.RowIndex;
                int columnIndex = dgvStockItemBatch.CurrentCell.ColumnIndex;

                if (RemoveItemBatch(rowIndex, columnIndex))
                {
                    return;
                }
                if (m_objStockCount != null && m_objStockCount.StatusId < (int)Common.StockStatus.Executed)
                {
                    if (m_selectedItemRowIndex <= Common.INT_DBNULL)
                    {
                        MessageBox.Show(Common.GetMessage("VAL0056"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                m_isDuplicateRecordFound = Common.INT_DBNULL;

                if (rowIndex >= 0 && columnIndex > 0)
                {
                    //m_invalidQuantity = false;
                    int selectedRow = dgvStockItemBatch.SelectedCells[0].RowIndex;

                    m_batchNo = dgvStockItemBatch.Rows[rowIndex].Cells["BatchNo"].Value.ToString().Trim();
                    m_itemRowNo = Convert.ToInt32(dgvStockItemBatch.Rows[rowIndex].Cells["ItemRowNo"].Value.ToString());

                    //m_RowNo = Convert.ToInt32(dgvTOIItem.Rows[e.RowIndex].Cells["RowNo"].Value.ToString().Trim());

                    ////Get ParentId
                    if (m_lstItemBatchDetail == null)
                        return;

                    var itemSelect = (from p in m_lstItemBatchDetail where p.ItemRowNo == m_itemRowNo && p.BatchNo == m_batchNo select p);

                    if (itemSelect.ToList().Count == 0)
                        return;

                    m_itemStockBatch = itemSelect.ToList()[0];
                    m_selectedItemBatchRowNum = m_itemStockBatch.ItemRowNo;
                    m_selectedItemBatchRowIndex = rowIndex;

                    txtBatchNo.Text = m_itemStockBatch.ManufactureBatchNo;
                    m_batchNo = m_itemStockBatch.BatchNo;

                    txtPhysicalQty.Text = m_itemStockBatch.DisplayPhysicalQty.ToString(); //Math.Round(m_itemStockBatch.PhysicalQty, 2).ToString();

                    ValidateBatchNo(true);
                    EmptyErrorProvider();
                }
            }
        }

        /// <summary>
        /// Creates DataSet for Printing Stock Count Screen report
        /// </summary>
        private void CreatePrintDataSet()
        {
            m_printDataSet = new DataSet();
            // Get Data For TOI Header Informaton in Stock Count Screen Report
            DataTable dtSCHeader = new DataTable("SCHeader");
            DataColumn SeqNumber = new DataColumn("SeqNumber", System.Type.GetType("System.String"));
            DataColumn Location = new DataColumn("Location", System.Type.GetType("System.String"));
            DataColumn LocationName = new DataColumn("LocationName", System.Type.GetType("System.String"));
            DataColumn Status = new DataColumn("Status", System.Type.GetType("System.String"));
            DataColumn SCDate = new DataColumn("SCDate", System.Type.GetType("System.String"));
            DataColumn ExecutedBy = new DataColumn("ExecutedBy", System.Type.GetType("System.String"));
            DataColumn InitiatedBy = new DataColumn("InitiatedBy", System.Type.GetType("System.String"));
            DataColumn Remarks = new DataColumn("Remarks", System.Type.GetType("System.String"));
            dtSCHeader.Columns.Add(SeqNumber);
            dtSCHeader.Columns.Add(Location);
            dtSCHeader.Columns.Add(LocationName);
            dtSCHeader.Columns.Add(Status);
            dtSCHeader.Columns.Add(SCDate);
            dtSCHeader.Columns.Add(ExecutedBy);
            dtSCHeader.Columns.Add(InitiatedBy);
            dtSCHeader.Columns.Add(Remarks);

            DataRow dRow = dtSCHeader.NewRow();
            dRow["SeqNumber"] = m_objStockCount.SeqNo;
            dRow["Location"] = cmbLocation.Text;
            dRow["LocationName"] = m_objStockCount.LocationName;
            dRow["Status"] = txtStatus.Text;
            dRow["SCDate"] = (Convert.ToDateTime(m_objStockCount.InitiatedDate)).ToString(Common.DTP_DATE_FORMAT);
            dRow["ExecutedBy"] = txtStockCountBy.Text;
            dRow["InitiatedBy"] = txtStockCountVerifiedBy.Text;
            dRow["Remarks"] = m_objStockCount.Remarks;

            dtSCHeader.Rows.Add(dRow);

            // Search ItemData
            DataTable dtSCItemDetail = new DataTable("SCItemDetail");
            dtSCItemDetail = m_objStockCount.SearchItemDataTable(m_objStockCount.SeqNo);
            // Search ItemBatchData
            DataTable dtSCItemBatchDetail = new DataTable("SCItemBatchDetail");
            dtSCItemBatchDetail = m_objStockCount.SearchItemBatchDataTable(m_objStockCount.SeqNo);
            // A new DataTable For SC Detail
            DataTable dtSCDetail = new DataTable("SCDetail");
            DataColumn ItemCode = new DataColumn("ItemCode", System.Type.GetType("System.String"));
            DataColumn ItemName = new DataColumn("ItemName", System.Type.GetType("System.String"));
            DataColumn Bucket = new DataColumn("Bucket", System.Type.GetType("System.String"));
            DataColumn SystemQty = new DataColumn("SystemQty", System.Type.GetType("System.String"));
            DataColumn ProcessedQty = new DataColumn("ProcessedQty", System.Type.GetType("System.String"));
            DataColumn BatchNo = new DataColumn("BatchNo", System.Type.GetType("System.String"));
            DataColumn PhysicalQty = new DataColumn("PhysicalQty", System.Type.GetType("System.String"));
            dtSCDetail.Columns.Add(ItemCode);
            dtSCDetail.Columns.Add(ItemName);
            dtSCDetail.Columns.Add(Bucket);
            dtSCDetail.Columns.Add(SystemQty);
            dtSCDetail.Columns.Add(ProcessedQty);
            dtSCDetail.Columns.Add(BatchNo);
            dtSCDetail.Columns.Add(PhysicalQty);

            for (int i = 0; i < dtSCItemDetail.Rows.Count; i++)
            {
                for (int j = 0; j < dtSCItemBatchDetail.Rows.Count; j++)
                {
                    if ((dtSCItemBatchDetail.Rows[j]["ItemId"].ToString() == dtSCItemDetail.Rows[i]["ItemId"].ToString()) && (dtSCItemBatchDetail.Rows[j]["BucketName"].ToString() == dtSCItemDetail.Rows[i]["BucketName"].ToString()))
                    {
                        DataRow dRowAdd = dtSCDetail.NewRow();
                        dRowAdd["ItemCode"] = dtSCItemDetail.Rows[i]["ItemCode"];
                        dRowAdd["ItemName"] = dtSCItemDetail.Rows[i]["ItemName"];
                        dRowAdd["Bucket"] = dtSCItemDetail.Rows[i]["BucketName"];
                        dRowAdd["SystemQty"] = m_locationType == 1 ? Math.Round(Convert.ToDecimal(dtSCItemDetail.Rows[i]["SystemQuantity"]), Common.DisplayQtyRounding, MidpointRounding.AwayFromZero).ToString() : "";
                        dRowAdd["ProcessedQty"] = m_locationType == 1 ? Math.Round(Convert.ToDecimal(dtSCItemDetail.Rows[i]["TotalPhysicalQuantity"]), Common.DisplayQtyRounding, MidpointRounding.AwayFromZero).ToString() : "";
                        dRowAdd["BatchNo"] = dtSCItemBatchDetail.Rows[j]["ManufactureBatchNo"];
                        dRowAdd["PhysicalQty"] = Math.Round(Convert.ToDecimal(dtSCItemBatchDetail.Rows[j]["PhysicalQuantity"]), Common.DisplayQtyRounding, MidpointRounding.AwayFromZero);
                        dtSCDetail.Rows.Add(dRowAdd);
                    }
                }
            }
            m_printDataSet.Tables.Add(dtSCHeader);
            m_printDataSet.Tables.Add(dtSCDetail);
        }
        /// <summary>
        /// Prints Stock Count Screen report
        /// </summary>
        private void PrintReport()
        {
            CreatePrintDataSet();
            CoreComponent.UI.ReportScreen reportScreenObj = new CoreComponent.UI.ReportScreen((int)Common.ReportType.StockCount, m_printDataSet);
            reportScreenObj.ShowDialog();
            //reportScreenObj.PrintReport();
            m_printDataSet = null;
        }

        #endregion

        #region Events
        /// <summary>
        /// Remove TOI Item from Grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvStockItem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                RemoveStockItem(e);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Set m_F4Press = false
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtAvailableQty_Enter(object sender, EventArgs e)
        {
            try
            {
                m_F4Press = false;
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Bind Grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateSearchControls())
                {
                    BindGrid();
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Call fn SelectItemBatchGridRow
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvStockItemBatch_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                SelectItemBatchGridRow(e);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Call EditStockCount
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvSearchStockCount_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                EditStockCount(e);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Call fn. ResetItemControl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearDetails_Click(object sender, EventArgs e)
        {
            try
            {
                txtItemCode.Text = string.Empty;
                ResetItemControl();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Call fn. ResetItemBatchControl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                ResetItemBatchControl();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtPhysicalQty_Enter(object sender, EventArgs e)
        {
            try
            {
                m_F4Press = false;
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Call fn. Save
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreated_Click(object sender, EventArgs e)
        {
            try
            {

                Save((int)Common.StockStatus.Created);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Call fn. Save
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Save((int)Common.StockStatus.Cancelled);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Call fn. Save
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInitiated_Click(object sender, EventArgs e)
        {
            try
            {
                Save((int)Common.StockStatus.Initiated);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Call fn. Save
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnProcessed_Click(object sender, EventArgs e)
        {
            try
            {
                Save((int)Common.StockStatus.Processed);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Call fn. Save
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExecute_Click(object sender, EventArgs e)
        {
            try
            {
                Save((int)Common.StockStatus.Executed);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvStockItemBatch_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                RemoveItemBatch(e.RowIndex, e.ColumnIndex);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Call fn. Save
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                Save((int)Common.StockStatus.Closed);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// To reset controls
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreateReset_Click(object sender, EventArgs e)
        {
            try
            {
                tabControlTransaction.TabPages[1].Text = Common.TAB_CREATE_MODE;
                ResetTOAndItems();
                EnableDisableButton((int)Common.StockStatus.New);
                cmbLocation.Focus();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dtpStockCountDate_Validated(object sender, EventArgs e)
        {
            try
            {
                if (dtpStockCountDate.Checked == true)
                    errStockCount.SetError(dtpStockCountDate, string.Empty);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvStockItem_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                SelectItemGridRow(e);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// To Reset Search Panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearchReset_Click(object sender, EventArgs e)
        {
            try
            {
                CoreComponent.Core.BusinessObjects.VisitControls visitControls = new CoreComponent.Core.BusinessObjects.VisitControls();
                visitControls.ResetAllControlsInPanel(pnlSearchHeader);
                InitializeDateControl();

                if (m_locationType == (int)Common.LocationConfigId.BO || m_locationType == (int)Common.LocationConfigId.WH)
                    cmbSearchLocation.SelectedValue = m_currentLocationId.ToString();

                dgvSearchStockCount.DataSource = new List<StockCount>();
                txtSeqNo.Focus();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtBatchNo_Validated(object sender, EventArgs e)
        {
            try
            {
                ValidateBatchNo(true);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Add Item Into Grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddDetails_Click(object sender, EventArgs e)
        {
            try
            {
                EmptyErrorProvider();
                ValidateItemMessages();

                if (ValidateAddButton())
                {
                    bool b = CheckValidQuantity();
                    if (cmbLocation.SelectedIndex == 0)
                    {
                        MessageBox.Show(Common.GetMessage("VAL0002", lblLocation.Text.Trim().Substring(0, lblLocation.Text.Trim().Length - 2)), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (m_isDuplicateRecordFound <= 0)
                    {
                        if (AddItem())
                        {
                            BindItemGrid();
                            cmbBucket.SelectedIndex = 0;
                            ReadOnlyHeader();
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtItemCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if ( e.KeyValue == Common.F4KEY && !e.Alt) 
                {
                    NameValueCollection nvc = new NameValueCollection();

                    CoreComponent.Controls.frmSearch objfrmSearch = new CoreComponent.Controls.frmSearch(CoreComponent.Controls.SearchTypes.ItemMaster, nvc);
                    objfrmSearch.ShowDialog();
                    ItemDetails _Item = (ItemDetails)objfrmSearch.ReturnObject;
                    if (_Item != null)
                    {
                        txtItemCode.Text = _Item.ItemCode.ToString();
                        //CalculateTransferPrice();
                        FillItemPriceInfo(txtItemCode.Text.Trim(), false);
                        ClearBatchInfor();
                    }
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Call fn. ValidateItemCode to Validate Item Code
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtItemCode_Validated(object sender, EventArgs e)
        {
            try
            {
                ValidateItemCode(true);

            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Call fn. BatchNoKeyDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBatchNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if ( e.KeyValue == Common.F4KEY && !e.Alt)
                {
                    BatchNoKeyDown(e);
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Call fn. tabControlSelect for Select index change event for Tab Controler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControlTransaction_Selecting(object sender, TabControlCancelEventArgs e)
        {
            try
            {
                tabControlSelect(e);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbBucket_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BucketChanged(true);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddBatch_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateItemBatchMessages();

                if (ValidateAddButton())
                {
                    //m_invalidQuantity = false;
                    bool b = CheckValidQuantity();

                    if (m_isDuplicateRecordFound <= 0)
                    {/*
                        if (m_systemQty > 0 && txtPhysicalQty.Text.Trim().Length > 0)
                        {
                            if (txtAvailableQty.Text.Trim().Length > 0 && Convert.ToDecimal(txtPhysicalQty.Text.Trim()) > Convert.ToDecimal(txtAvailableQty.Text.Trim()))
                            {
                                MessageBox.Show(Common.GetMessage("VAL0060", lblPhysicalQty.Text.Trim().Substring(0, lblPhysicalQty.Text.Trim().Length - 2), lblAvailableQty.Text.Trim().Substring(0, lblAvailableQty.Text.Trim().Length - 2)), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            if (Math.Round(Convert.ToDecimal(txtPhysicalQty.Text), 2) > Math.Round(m_systemQty, 2))
                            {
                                MessageBox.Show(Common.GetMessage("VAL0060", lblPhysicalQty.Text.Trim().Substring(0, lblPhysicalQty.Text.Trim().Length - 2), lblSystemQty.Text.Trim().Substring(0, lblSystemQty.Text.Trim().Length - 2)), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            //if (m_invalidQuantity)
                            //{
                            //    MessageBox.Show(Common.GetMessage("VAL0060", "Total " +lblPhysicalQty.Text.Trim().Substring(0, lblPhysicalQty.Text.Trim().Length - 2), lblSystemQty.Text.Trim().Substring(0, lblSystemQty.Text.Trim().Length - 2)), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //    return;
                            //}
                        }
                        */
                        if (AddBatchItem())
                        {
                            BindItemBatchGrid();
                            ResetItemControl();
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

        private void cmbLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ValidateLocationAddress(true);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_objStockCount != null && m_objStockCount.StatusId >= (int)Common.StockStatus.Executed)
                {
                    btnPrint.Enabled = false;
                    PrintReport();
                    btnPrint.Enabled = true;
                }
                else
                    MessageBox.Show(Common.GetMessage("INF0101", "Stock Count", Common.StockStatus.Executed.ToString()), Common.GetMessage("10001"),MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                btnPrint.Enabled = true;
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private void dgvSearchStockCount_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (dgvSearchStockCount.SelectedRows.Count > 0)
                {
                    string SeqNo = Convert.ToString(dgvSearchStockCount.SelectedRows[0].Cells["SeqNo"].Value);

                    var query = (from p in m_lstStockCount where p.SeqNo == SeqNo select p);
                    m_objStockCount = (StockCount)query.ToList()[0];

                    SelectSearchGrid(SeqNo);
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
