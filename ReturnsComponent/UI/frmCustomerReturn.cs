using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Windows.Forms;
using CoreComponent.Core.BusinessObjects;
using CoreComponent.Hierarchies.BusinessObjects;
using ReturnsComponent.BusinessObjects;
using CoreComponent.MasterData.BusinessObjects;
using System.Collections.Specialized;
using AuthenticationComponent.BusinessObjects;


namespace ReturnsComponent.UI
{
    public partial class frmCustomerReturn : CoreComponent.Core.UI.Transaction
    {
        #region Variables
        int negateMod = 0;
        Boolean m_F4Press = false;
        int m_bucketid = Common.INT_DBNULL;
        DataTable m_dtSearchAllBucket;
        decimal m_MRP = 0;
        DataSet m_printDataSet = null;
        private string strLoc = Common.INT_DBNULL.ToString();
        List<CustomerReturn> m_lstCustomerReturn;
        List<CustomerReturnItem> m_lstItemDetail;
        CustomerReturnItem m_itemDetail;
        CustomerReturn m_objCustomerReturn;

        int customerType = 0;
        //string strLocationCode = string.Empty;

        string m_seqNo = Common.INT_DBNULL.ToString();

        int m_selectedItemRowNum = Common.INT_DBNULL;

        int m_selectedItemRowIndex = Common.INT_DBNULL;
        int m_itemId = Common.INT_DBNULL;
        int m_itemRowNo = Common.INT_DBNULL;
        DataTable m_dtLocation;

        string m_batchNo = string.Empty;
        int m_isDuplicateRecordFound = Common.INT_DBNULL;

        private Boolean m_isSearchAvailable = false;
        private Boolean m_isCancelAvailable = false;
        private Boolean m_isSaveAvailable = false;
        private Boolean m_isApprovedAvailable = false;
        private Boolean m_isPrintAvailable = false;

        private int m_userId = Authenticate.LoggedInUser.UserId;
        private string strUserName = Authenticate.LoggedInUser.UserName;

        private string strLocationCode = Common.LocationCode;
        private int m_currentLocationId = Common.CurrentLocationId;
        private int m_locationType = Common.CurrentLocationTypeId;
        decimal totalAmount;
        #endregion Variables

        #region C'tor
        public frmCustomerReturn()
        {
            try
            {
                m_isSearchAvailable = Authenticate.IsFunctionAccessible(strUserName, strLocationCode, CustomerReturn.MODULE_CODE, Common.FUNCTIONCODE_SEARCH);
                m_isSaveAvailable = Authenticate.IsFunctionAccessible(strUserName, strLocationCode, CustomerReturn.MODULE_CODE, Common.FUNCTIONCODE_SAVE);
                m_isCancelAvailable = Authenticate.IsFunctionAccessible(strUserName, strLocationCode, CustomerReturn.MODULE_CODE, Common.FUNCTIONCODE_CANCEL);
                m_isApprovedAvailable = Authenticate.IsFunctionAccessible(strUserName, strLocationCode, CustomerReturn.MODULE_CODE, Common.FUNCTIONCODE_APPROVE);
                m_isPrintAvailable = Authenticate.IsFunctionAccessible(strUserName, strLocationCode, CustomerReturn.MODULE_CODE, Common.FUNCTIONCODE_PRINT);

                InitializeComponent();
                GridInitialize();
                FillLocations();
                FillBucket(true);
                FillSearchStatus();
                InitializeDateControl();
                FillUsers();
                EnableDisableButton((int)Common.CustomerReturnStatus.New);
                FillCustomerType();


                ReadOnlyItemDetail();
                //ReadOnlyBatchDetail();
                lblPageTitle.Text = "Customer Return";

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
            if ((m_locationType == (int)Common.LocationConfigId.HO) || (m_locationType == (int)Common.LocationConfigId.BO))
            {
                cmbBucket.Enabled = true;
                if ((m_objCustomerReturn == null) || (m_objCustomerReturn != null && m_objCustomerReturn.StatusId <= (int)Common.CustomerReturnStatus.Created))
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

        //#region Methods
        /// <summary>
        /// Grid Initialize
        /// </summary>
        void GridInitialize()
        {
            dgvCustomerReturn.AutoGenerateColumns = false;
            dgvCustomerReturn.DataSource = null;
            DataGridView dgvSearchCustomerReturnNew = Common.GetDataGridViewColumns(dgvCustomerReturn, Environment.CurrentDirectory + "\\App_Data\\Return.xml");

            dgvCustomerReturnItem.AutoGenerateColumns = false;
            dgvCustomerReturnItem.DataSource = null;
            DataGridView dgvdgvCustomerItemNew = Common.GetDataGridViewColumns(dgvCustomerReturnItem, Environment.CurrentDirectory + "\\App_Data\\Return.xml");
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
                m_dtSearchAllBucket = Common.ParameterLookup(Common.ParameterType.AllSubBuckets, new ParameterFilter(string.Empty, 0, 0, 0));
                if (m_dtSearchAllBucket != null && m_dtSearchAllBucket.Rows.Count > 0)
                {
                    //m_dtSearchAllBucket = m_dtSearchAllBucket.AsEnumerable().Distinct().CopyToDataTable();
                    //dt = m_dtSearchAllBucket.Select("BucketId=" + CoreComponent.Core.BusinessObjects.Common.ON_HAND_SUB_BUCKET_ID).AsEnumerable().Distinct().CopyToDataTable();
                    //dt.Rows.RemoveAt(0);
                    dv = new DataView(m_dtSearchAllBucket.DefaultView.ToTable(true, "BucketId", "BucketName", "Sellable"));
                    dv.RowFilter = "Sellable=1";
                }

                if (dv != null && dv.Count > 0)
                {
                    cmbBucket.DataSource = dv;
                    cmbBucket.DisplayMember = "BucketName";
                    cmbBucket.ValueMember = "BucketId";
                }
            }
        }

        void FillUsers()
        {
            DataTable dtUser = Common.ParameterLookup(Common.ParameterType.Users, new ParameterFilter("", 0, 0, 0));
            if (dtUser != null)
            {
                cmbCustomerReturnBy.DataSource = dtUser;
                cmbCustomerReturnBy.DisplayMember = "Name";
                cmbCustomerReturnBy.ValueMember = "UserId";
            }
        }

        void FillCustomerType()
        {
            DataTable dt = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter("CustomerReturnType", 0, 0, 0));
            DataTable dt1 = dt.Copy();

            cmbSearchCustomerType.DataSource = dt;
            cmbSearchCustomerType.DisplayMember = Common.KEYVALUE1;
            cmbSearchCustomerType.ValueMember = Common.KEYCODE1;

            cmbCustomerType.DataSource = dt1;
            cmbCustomerType.DisplayMember = Common.KEYVALUE1;
            cmbCustomerType.ValueMember = Common.KEYCODE1;
        }

        /// <summary>
        /// Fill Search and Update Location Drop Down List
        /// </summary>
        void FillLocations()
        {
            m_dtLocation = Common.ParameterLookup(Common.ParameterType.Locations, new ParameterFilter("LOCATIONS", (int)Common.LocationConfigId.BO, 0, 0));
            DataRow[] dr = m_dtLocation.Select("LocationId = " + Common.INT_DBNULL);
            dr[0]["DisplayName"] = "Select";


            if (m_dtLocation != null)
            {
                cmbLocation.DataSource = m_dtLocation;
                cmbLocation.DisplayMember = "DisplayName";
                cmbLocation.ValueMember = "LocationId";

                if (m_locationType == (int)Common.LocationConfigId.BO)
                {
                    cmbLocation.SelectedValue = m_currentLocationId.ToString();
                    cmbLocation.Enabled = false;
                }
            }

            DataTable dtSearchDest = Common.ParameterLookup(Common.ParameterType.Locations, new ParameterFilter("LOCATIONS", (int)Common.LocationConfigId.BO, 0, 0));
            if (dtSearchDest != null)
            {
                cmbSearchLocation.DataSource = dtSearchDest;
                cmbSearchLocation.DisplayMember = "DisplayName";
                cmbSearchLocation.ValueMember = "LocationId";

                if (m_locationType == (int)Common.LocationConfigId.BO)
                {
                    cmbSearchLocation.SelectedValue = m_currentLocationId.ToString();
                    cmbSearchLocation.Enabled = false;
                }
            }


        }

        ///// <summary>
        ///// Fill Status Drop Down List
        ///// </summary>
        private void FillSearchStatus()
        {
            DataTable dt = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter("CUSTOMERRETURNSTATUS", 0, 0, 0));
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
            dtpApprovedDate.CustomFormat = Common.DTP_DATE_FORMAT;
            dtpApprovedDate.Checked = false;
            dtpSearchFrom.Checked = false;
            dtpSearchTo.Checked = false;

            dtpSearchFrom.Value = Convert.ToDateTime(System.DateTime.Now.ToShortDateString());
            dtpSearchTo.Value = Convert.ToDateTime(System.DateTime.Now.ToShortDateString());
            dtpApprovedDate.Value = Convert.ToDateTime(System.DateTime.Now.ToShortDateString());
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
                    errCustomerReturn.SetError(cmbLocation, Common.GetMessage("INF0026", lblLocation.Text.Trim().Substring(0, lblLocation.Text.Trim().Length - 1)));
                //txtLocationAddress.Text = string.Empty;
                txtLocationAddress.Text = string.Empty;
            }
            else
            {

                errCustomerReturn.SetError(cmbLocation, string.Empty);
                ShowAddress();
                FillBucket(yesNo);
                ShowHideAdd(cmbCustomerType.SelectedIndex);
            }

            if (yesNo)
            {
                ResetItemControl();
            }
        }

        /// <summary>
        /// Shows or Hides the Add Button in the ItemGrid - added by Kaushik 
        /// </summary>
        void ShowHideAdd(int customerType)
        {
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
                txtLocationAddress.Text = dr[0]["Address"].ToString();
            }
            else
                txtLocationAddress.Text = string.Empty;
        }


        /// <summary>
        /// Search TOIs
        /// </summary>
        //void SearchCustomerReturn()
        //{
        //    DateTime DATETIME_MAX = Convert.ToDateTime(new DateTime(2099, 12, 31).ToString(Common.DATE_TIME_FORMAT));

        //    DateTime fromDate = dtpSearchFrom.Checked == true ? Convert.ToDateTime(dtpSearchFrom.Value) : Common.DATETIME_NULL;
        //    DateTime toDate = dtpSearchTo.Checked == true ? Convert.ToDateTime(dtpSearchTo.Value) : DATETIME_MAX;

        //    if ((m_lstCustomerReturn != null) && (m_lstCustomerReturn.Count > 0))
        //    {
        //        dgvCustomerReturn.DataSource = m_lstCustomerReturn;
        //        dgvCustomerReturn.Select();
        //        ResetItemControl();
        //    }
        //    else
        //        dgvCustomerReturn.DataSource = new List<CustomerReturn>();
        //}



        /// <summary>
        /// Remove Item Record
        /// </summary>
        /// <param name="e"></param>
        void RemoveTOItem(DataGridViewCellEventArgs e)
        {
            if ((e.RowIndex >= 0) && (e.ColumnIndex == 0) && (dgvCustomerReturnItem.Rows[e.RowIndex].Cells[e.ColumnIndex].GetType() == typeof(DataGridViewImageCell)))
            {

                if (cmbCustomerType.SelectedIndex == 3)
                    MessageBox.Show(Common.GetMessage("VAL0616"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;

                if ((m_objCustomerReturn == null) || (m_objCustomerReturn != null && (Convert.ToInt32(m_objCustomerReturn.StatusId) == (int)Common.CustomerReturnStatus.Created || Convert.ToInt32(m_objCustomerReturn.StatusId) == (int)Common.CustomerReturnStatus.New)))
                {
                    DialogResult saveResult = MessageBox.Show(Common.GetMessage("INF0028"), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (saveResult == DialogResult.Yes)
                    {
                        if (m_lstItemDetail.Count > 0)
                        {
                            dgvCustomerReturnItem.DataSource = null;
                            m_lstItemDetail.RemoveAt(e.RowIndex);
                            dgvCustomerReturnItem.DataSource = m_lstItemDetail;

                            dgvCustomerReturnItem.Select();
                            ResetItemControl();
                            ReadOnlyHeader();
                            bool b = CheckValidQuantity();

                        }
                    }
                }
                else
                {
                    MessageBox.Show(Common.GetMessage("VAL0045"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if ((e.RowIndex >= 0) && (e.ColumnIndex == 1) && (dgvCustomerReturnItem.Rows[e.RowIndex].Cells[e.ColumnIndex].GetType() == typeof(DataGridViewImageCell)))
            {
                m_itemDetail = GetItemInfo(e.RowIndex);
                //m_selectedItemRowIndex = e.RowIndex;
                MessageBox.Show("test");
            }

        }

        /// <summary>
        /// Reset Item Controls
        /// </summary>
        void ResetItemControl()
        {
            m_F4Press = false;
            m_selectedItemRowNum = Common.INT_DBNULL;
            m_itemId = Common.INT_DBNULL;
            m_bucketid = Common.INT_DBNULL;
            m_itemDetail = null;

            m_F4Press = false;

            m_itemRowNo = Common.INT_DBNULL;
            m_batchNo = string.Empty;


            m_selectedItemRowIndex = Common.INT_DBNULL;
            CoreComponent.Core.BusinessObjects.VisitControls visitControls = new CoreComponent.Core.BusinessObjects.VisitControls();
            visitControls.ResetAllControlsInPanel(errCustomerReturn, grpAddDetails);

            dgvCustomerReturnItem.ClearSelection();
            txtItemCode.Focus();


        }

        /// <summary>
        /// Generate string for Error
        /// </summary>
        /// <returns></returns>
        private StringBuilder GenerateError()
        {
            StringBuilder sbError = new StringBuilder();
            if (errCustomerReturn.GetError(cmbCustomerType).Trim().Length > 0)
            {
                sbError.Append(errCustomerReturn.GetError(cmbCustomerType));
                sbError.AppendLine();
            }
            
            if (errCustomerReturn.GetError(txtDistributorPCId).Trim().Length > 0)
            {
                sbError.Append(errCustomerReturn.GetError(txtDistributorPCId));
                sbError.AppendLine();
            }

            if (errCustomerReturn.GetError(cmbLocation).Trim().Length > 0)
            {
                sbError.Append(errCustomerReturn.GetError(cmbLocation));
                sbError.AppendLine();
            }
            if (errCustomerReturn.GetError(dtpApprovedDate).Trim().Length > 0)
            {
                sbError.Append(errCustomerReturn.GetError(dtpApprovedDate));
                sbError.AppendLine();
            }
            if (errCustomerReturn.GetError(txtItemCode).Trim().Length > 0)
            {
                sbError.Append(errCustomerReturn.GetError(txtItemCode));
                sbError.AppendLine();
            }
            if (errCustomerReturn.GetError(cmbBucket).Trim().Length > 0)
            {
                sbError.Append(errCustomerReturn.GetError(cmbBucket));
                sbError.AppendLine();
            }
            if (errCustomerReturn.GetError(txtBatchNo).Trim().Length > 0)
            {
                sbError.Append(errCustomerReturn.GetError(txtBatchNo));
                sbError.AppendLine();
            }

            if (errCustomerReturn.GetError(txtQuantity).Trim().Length > 0)
            {
                sbError.Append(errCustomerReturn.GetError(txtQuantity));
                sbError.AppendLine();
            }

            if (errCustomerReturn.GetError(txtDeductionAmount).Trim().Length > 0)
            {
                sbError.Append(errCustomerReturn.GetError(txtDeductionAmount));
                sbError.AppendLine();
            }
            sbError = Common.ReturnErrorMessage(sbError);
            return sbError;
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

            if (!sbError.ToString().Trim().Equals(string.Empty))
            {
                sbError = Common.ReturnErrorMessage(sbError);
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
            //m_itemDetail = new CustomerReturnItem();
            m_itemDetail.ItemId = m_itemId;
            m_itemDetail.ItemCode = txtItemCode.Text.Trim();
            m_itemDetail.ItemName = txtItemDescription.Text.Trim();
            m_itemDetail.Quantity = Convert.ToDecimal(txtQuantity.Text.ToString());
            //m_itemDetail.DistributorPrice = Convert.ToDecimal(txtDistributorPrice.Text.ToString());
            m_itemDetail.BucketId = Convert.ToInt32(cmbBucket.SelectedValue);
            m_itemDetail.BucketName = cmbBucket.Text.ToString();
            m_itemDetail.BatchNo = m_batchNo;
            m_itemDetail.ManufactureBatchNo = txtBatchNo.Text.Trim();
            //m_itemDetail.MRP = m_MRP;

            m_itemDetail.RowNo = m_selectedItemRowNum;

            if (m_lstItemDetail == null)
                m_lstItemDetail = new List<CustomerReturnItem>();

            if ((m_selectedItemRowIndex != Common.INT_DBNULL) && (m_selectedItemRowIndex <= dgvCustomerReturnItem.Rows.Count))
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
            if ((tabControlTransaction.SelectedIndex == 0) && dgvCustomerReturnItem.Rows.Count > 0)
            {

                DialogResult result = MessageBox.Show(Common.GetMessage("VAL0026"), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Cancel | DialogResult.No == result)
                {
                    tabControlTransaction.SelectedIndex = 0;
                    e.Cancel = true;
                }
                else if (result == DialogResult.Yes)
                {
                    ResetHeaderAndItems();
                    tabControlTransaction.TabPages[1].Text = Common.TAB_CREATE_MODE;
                    dgvCustomerReturn.ClearSelection();
                }
            }
            else if (tabControlTransaction.SelectedIndex == 1)
            {
                if (tabControlTransaction.TabPages[1].Text == Common.TAB_CREATE_MODE)
                {
                    ResetHeaderAndItems();
                    EnableDisableButton((int)Common.TIStatus.New);
                    txtDistributorPCId.Enabled = true;
                    cmbCustomerType.Enabled = true;
                }

            }
        }
        /// <summary>
        /// To Enable alll buttons
        /// </summary>
        void EnableAllButton(bool yesNo)
        {
            //btnCreateReset.Enabled = yesNo;
            btnSave.Enabled = yesNo;


            btnAddDetails.Enabled = yesNo;
            btnCancel.Enabled = yesNo;

            btnApproved.Enabled = yesNo;
            txtDeductionAmount.Enabled = true;
        }
        /// <summary>
        /// To enable and disable button, when status is changed
        /// </summary>
        /// <param name="statusId"></param>
        void EnableDisableButton(int statusId)
        {
            EnableAllButton(true);
            if (statusId == (int)Common.CustomerReturnStatus.New)
            {
                btnCancel.Enabled = false;
                btnApproved.Enabled = false;
            }
            else if (statusId == (int)Common.CustomerReturnStatus.Created)
            {
                btnSave.Enabled = false;
            }
            else if (statusId == (int)Common.CustomerReturnStatus.Cancelled)
            {
                btnApproved.Enabled = false;
                btnCancel.Enabled = false;
                btnAddDetails.Enabled = false;
                btnSave.Enabled = false;
            }
            else if (statusId == (int)Common.CustomerReturnStatus.Approved)
            {
                EnableAllButton(false);
                txtDeductionAmount.Enabled = false;

                dtpApprovedDate.Enabled = false;
            }

            btnSave.Enabled = btnSave.Enabled & m_isSaveAvailable;
            btnCancel.Enabled = btnCancel.Enabled & m_isCancelAvailable;
            btnSearch.Enabled = m_isSearchAvailable;
            btnApproved.Enabled = btnApproved.Enabled & m_isApprovedAvailable;
            btnPrint.Enabled = m_isPrintAvailable;
            //btnCancel.Enabled = true;
            //btnSearch.Enabled = true;
        }
        /// <summary>
        /// Reset TO and Item Controls
        /// </summary>
        void ResetHeaderAndItems()
        {
            CoreComponent.Core.BusinessObjects.VisitControls visitControls = new CoreComponent.Core.BusinessObjects.VisitControls();
            visitControls.ResetAllControlsInPanel(errCustomerReturn, pnlCreateHeader);

            m_objCustomerReturn = null;
            dtpApprovedDate.Checked = false;
            dtpApprovedDate.Value = Convert.ToDateTime(System.DateTime.Now.ToShortDateString());

            ResetItemControl();

            m_lstItemDetail = null;
            dgvCustomerReturnItem.DataSource = new List<CustomerReturnItem>();
            m_seqNo = string.Empty;
            ReadOnlyHeader();
            ReadOnlyItemDetail();
            //ReadOnlyBatchDetail();
            //txtDeductionAmount.Text = "0";
            txtPayableAmount.Text = string.Empty;
            errCustomerReturn.Clear();
        }

        void SelectSearchGrid(string returnNo)
        {
            try
            {
                m_lstItemDetail = m_objCustomerReturn.SearchItem(returnNo,m_objCustomerReturn.CustomerType);
                cmbCustomerType.SelectedValue = Convert.ToInt32(m_objCustomerReturn.CustomerType);
                m_seqNo = returnNo;
                tabControlTransaction.TabPages[1].Text = Common.TAB_UPDATE_MODE;
                tabControlTransaction.SelectedIndex = 1;

                #region Bind Item/Item Batch Grid
                BindItemGrid();
                #endregion

                FillBucket(true);

                cmbLocation.SelectedValue = m_objCustomerReturn.LocationId.ToString();
                cmbLocation.Enabled = false;


                EnableDisableButton(m_objCustomerReturn.StatusId);

                //cmbCustomerType.SelectedValue = Convert.ToInt32(m_objCustomerReturn.CustomerType);

                if (m_objCustomerReturn.ApprovedDate.Length > 0)
                {
                    dtpApprovedDate.Checked = true;
                    dtpApprovedDate.Value = Convert.ToDateTime(m_objCustomerReturn.ApprovedDate);
                }
                else
                { dtpApprovedDate.Checked = false; }
                txtDistributorPCId.Text = m_objCustomerReturn.DistributorPCId;
                txtStatus.Text = m_objCustomerReturn.StatusName;
                txtApprovedBy.Text = m_objCustomerReturn.ApprovedName;

                txtTotalAmount.Text = m_objCustomerReturn.DisplayTotalAmount.ToString();
                txtDeductionAmount.Text = m_objCustomerReturn.DisplayDeductionAmount.ToString();
                txtPayableAmount.Text = m_objCustomerReturn.DisplayPayableAmount.ToString();

                txtRemarks.Text = m_objCustomerReturn.Remarks;

                ReadOnlyItemDetail();
                //ReadOnlyBatchDetail();
                ReadOnlyHeader();
                txtDistributorPCId.Enabled = false;
                cmbCustomerType.Enabled = false;
                errCustomerReturn.Clear();

                if ((m_objCustomerReturn.StatusName != "Approved") && m_locationType != (int)Common.LocationConfigId.HO)
                {
                    dtpApprovedDate.Enabled = false;
                }
                else
                {
                    dtpApprovedDate.Enabled = true;
                }

            }
            catch (Exception exp)
            {
                Common.LogException(exp);
            }
        }

        /// <summary>
        /// Edit Customer Return 
        /// </summary>
        /// <param name="e"></param> 
        private void EditCustomerReturn(DataGridViewCellEventArgs e)
        {
            if ((e.RowIndex >= 0) && (dgvCustomerReturn.Rows[e.RowIndex].Cells[e.ColumnIndex].GetType() == typeof(DataGridViewImageCell)))
            {
                m_objCustomerReturn = m_lstCustomerReturn[e.RowIndex];
                SelectSearchGrid(m_objCustomerReturn.ReturnNo);
            }
        }

        /// <summary>
        /// Clear Batch Info
        /// </summary>
        void ClearBatchInfor()
        {
            txtBatchNo.Text = string.Empty;

            txtQuantity.Text = string.Empty;

            m_batchNo = string.Empty;
        }

        /// <summary>
        /// Fill Item Price Information into UOM, name of Item
        /// </summary>
        /// <param name="itemCode"></param>
        void FillItemPriceInfo(string itemCode, Boolean yesNo)
        {
            bool invalid = false;

            ItemDetails itemDetails = new ItemDetails();
            itemDetails.LocationId = cmbLocation.SelectedValue.ToString();

            List<ItemDetails> lst = new List<ItemDetails>();
            lst = itemDetails.SearchLocationItem();

            var query = from p in lst where p.ItemCode.ToLower().Trim() == itemCode.ToLower().Trim() select p;
            if (query.ToList().Count > 0)
            {
                DataTable dt = Common.ParameterLookup(Common.ParameterType.Item, new ParameterFilter("Item", 0, 0, 0));
                DataView dv = dt.DefaultView;
                dv.RowFilter = "ItemCode = '" + itemCode + "'";
                if (dv.Count > 0)
                {
                    m_itemId = Convert.ToInt32(dv[0]["ItemId"]);
                    txtItemDescription.Text = dv[0]["ItemName"].ToString();
                    if (m_itemDetail == null)
                        m_itemDetail = new CustomerReturnItem();

                    m_itemDetail.DistributorPrice = Convert.ToDecimal(dv[0]["DistributorPrice"]);
                    txtDistributorPrice.Text = m_itemDetail.DisplayDistributorPrice.ToString();
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
                    errCustomerReturn.SetError(txtItemCode, Common.GetMessage("VAL0053", lblItemCode.Text.Trim().Substring(0, lblItemCode.Text.Trim().Length - 2)));
                else
                    errCustomerReturn.SetError(txtItemCode, Common.GetMessage("VAL0006", lblItemCode.Text.Trim().Substring(0, lblItemCode.Text.Trim().Length - 2)));
                txtItemDescription.Text = string.Empty;
                m_itemId = Common.INT_DBNULL;
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
                    errCustomerReturn.SetError(txtItemCode, Common.GetMessage("INF0019", lblItemCode.Text.Trim().Substring(0, lblItemCode.Text.Trim().Length - 2)));
                else
                    errCustomerReturn.SetError(txtItemCode, string.Empty);
            }
            else if (isTextBoxEmpty == false)
            {
                errCustomerReturn.SetError(txtItemCode, string.Empty);
                if (yesNo)
                {
                    errCustomerReturn.SetError(txtItemCode, string.Empty);
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
            m_MRP = objItem.MRP;
            //GetRequestedQuantity(objItem.ItemCode.ToString(),objItem.BucketId);
            //txtItemCode.Text = _Item.ItemCode.ToString();
            m_itemDetail.MRP = objItem.MRP;
            txtMRP.Text = m_itemDetail.DisplayMRP.ToString();

            txtBatchNo.Text = objItem.ManufactureBatchNo.ToString();
            m_batchNo = objItem.BatchNo.ToString();
            //txtAvailableQty.Text = Math.Round(objItem.Quantity, 2).ToString();
        }

        /// <summary>
        /// Open new window form when F4 key is pressed
        /// </summary>
        /// <param name="e"></param>
        void BatchNoKeyDown(KeyEventArgs e)
        {
            NameValueCollection nvc = new NameValueCollection();
            nvc.Add("ItemCode", txtItemCode.Text.Trim());
            nvc.Add("BucketId", cmbBucket.SelectedValue.ToString());
            nvc.Add("LocationId", cmbLocation.SelectedValue.ToString());

            CoreComponent.Controls.frmSearch objfrmSearch = new CoreComponent.Controls.frmSearch(CoreComponent.Controls.SearchTypes.ItemBatch, nvc);
            objfrmSearch.ShowDialog();
            CoreComponent.MasterData.BusinessObjects.ItemBatchDetails objItem = (CoreComponent.MasterData.BusinessObjects.ItemBatchDetails)objfrmSearch.ReturnObject;
            if (objItem != null)
            {
                errCustomerReturn.SetError(txtBatchNo, string.Empty);
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
        List<CustomerReturnItem> CopyItemDetail(int excludeIndex, List<CustomerReturnItem> lst)
        {
            List<CustomerReturnItem> returnList = new List<CustomerReturnItem>();
            for (int i = 0; i < lst.Count; i++)
            {
                if (i != excludeIndex)
                {
                    CustomerReturnItem tdetail = new CustomerReturnItem();
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
            // Check Duplicate For ItemCode and Batch No, from bucket and To Bucket

            if (m_lstItemDetail != null && m_lstItemDetail.Count > 0)
            {

                List<CustomerReturnItem> tiDetail = CopyItemDetail(m_selectedItemRowIndex, m_lstItemDetail);
                //checked based on ItemCode and Bucket Id checking kaushik
                m_isDuplicateRecordFound = (from p in tiDetail where p.BatchNo.Trim().ToLower() == p.BatchNo.Trim().ToLower() && p.ItemCode.Trim().ToLower() == txtItemCode.Text.Trim().ToLower() && p.BucketId == Convert.ToInt32(cmbBucket.SelectedValue) select p).Count();

                if (m_isDuplicateRecordFound > 0)
                {
                    MessageBox.Show(Common.GetMessage("VAL0063", lblItemCode.Text.Trim().Substring(0, lblItemCode.Text.Trim().Length - 2)), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                // m_itemDetail.Quantity = Convert.ToDecimal(txtQuantity.Text);
                CreateCustomerReturnObjet();

                if (cmbCustomerType.SelectedValue.Equals(3))
                {

                    m_objCustomerReturn.TotalAmount = (Convert.ToDecimal(txtInvoiceAmount.Text) + Convert.ToDecimal(txtTaxAmount.Text));
                    txtPayableAmount.Text = txtTotalAmount.Text = m_objCustomerReturn.DisplayTotalAmount.ToString();

                    // decimal 
                    //totalAmount = Convert.ToDecimal(txtInvoiceAmount.Text) + Convert.ToDecimal(txtTaxAmount.Text);
                    //txtPayableAmount.Text = (Convert.ToDecimal(txtInvoiceAmount.Text) + Convert.ToDecimal(txtTaxAmount.Text)).ToString();
                }

                else
                {
                    var query = (from p in m_lstItemDetail select p.DBQuantity *p.DisplayDistributorPrice).Sum();
                    decimal qtySum = Convert.ToDecimal(query);

                    m_objCustomerReturn.TotalAmount = (m_objCustomerReturn.TotalAmount + Convert.ToDecimal(txtDistributorPrice.Text.Trim().Length == 0 ? "0" : txtDistributorPrice.Text.Trim()) * Convert.ToDecimal(txtQuantity.Text.Trim().Length == 0 ? "0" : txtQuantity.Text.Trim()));
                    txtPayableAmount.Text = txtTotalAmount.Text = m_objCustomerReturn.DisplayTotalAmount.ToString();


                    //decimal totalAmount;
                    //m_objCustomerReturn.TotalAmount = (totalAmount + m_itemDetail.DistributorPrice * m_itemDetail.Quantity);
                    //decimal totalAmount = (from p in tiDetail select p.Quantity * p.DistributorPrice).Sum();
                    //decimal totalAmount = Convert.ToDecimal(txtDistributorPrice.Text) * Convert.ToDecimal(txtQuantity.Text);
                }
                //m_objCustomerReturn.TotalAmount = (totalAmount + m_itemDetail.DistributorPrice * m_itemDetail.Quantity);                

                //m_objCustomerReturn.TotalAmount = totalAmount;

                txtTotalAmount.Text = m_objCustomerReturn.DisplayTotalAmount.ToString();

            }
            else if ((m_lstItemDetail == null) || (m_lstItemDetail != null && m_lstItemDetail.Count == 0))
            {
                m_objCustomerReturn.TotalAmount = (Convert.ToDecimal(txtDistributorPrice.Text.Trim().Length == 0 ? "0" : txtDistributorPrice.Text.Trim()) * Convert.ToDecimal(txtQuantity.Text.Trim().Length == 0 ? "0" : txtQuantity.Text.Trim()));
                txtPayableAmount.Text = txtTotalAmount.Text = m_objCustomerReturn.DisplayTotalAmount.ToString();
            }

            //if (txtDeductionAmount.Text.Trim().Length > 0)
            ValidateDeductionAmount(true);

            return true;
        }

        /// <summary>
        /// Validate Starting Amount
        /// </summary>
        void ValidateAmount(TextBox txt, Label lbl)
        {
            bool isValidAmount = CoreComponent.Core.BusinessObjects.Validators.IsValidAmount(txt.Text == "" ? "0" : txt.Text);
            if (isValidAmount == false)
                errCustomerReturn.SetError(txt, Common.GetMessage("VAL0009", lbl.Text.Trim().Substring(0, lbl.Text.Trim().Length - 2)));
            else if (Convert.ToDecimal(txt.Text == "" ? "0" : txt.Text) < 0)
                errCustomerReturn.SetError(txt, Common.GetMessage("VAL0009", lbl.Text.Trim().Substring(0, lbl.Text.Trim().Length - 2)));
            else
                errCustomerReturn.SetError(txt, string.Empty);
        }

        /// <summary>
        /// Validate Starting Amount
        /// </summary>
        void ValidateQuantity(TextBox txt, Label lbl)
        {
            bool isValidQuantity = CoreComponent.Core.BusinessObjects.Validators.IsValidQuantity(txt.Text);

            if (isValidQuantity == false)
                errCustomerReturn.SetError(txt, Common.GetMessage("VAL0009", lbl.Text.Trim().Substring(0, lbl.Text.Trim().Length - 2)));
            else if (Convert.ToDecimal(txt.Text) <= 0)
                errCustomerReturn.SetError(txt, Common.GetMessage("VAL0009", lbl.Text.Trim().Substring(0, lbl.Text.Trim().Length - 2)));
            else
                errCustomerReturn.SetError(txt, string.Empty);
        }

        /// <summary>
        /// Validate Combo Box
        /// </summary>
        /// <param name="cmb"></param>
        /// <param name="lbl"></param>
        private void ValidateCombo(ComboBox cmb, Label lbl)
        {
            if (cmb.SelectedIndex == 0)
                errCustomerReturn.SetError(cmb, Common.GetMessage("INF0026", lbl.Text.Trim().Substring(0, lbl.Text.Trim().Length - 2)));
            else
                errCustomerReturn.SetError(cmb, string.Empty);
        }

        /// <summary>
        /// Validate Controls On Add Button Click
        /// </summary>
        void ValidateItemMessages()
        {
            ValidateUnitQuantity(true);
            ValidateItemCode(false);
            ValidateBatchNo(false);
        }

        void BindItemGrid()
        {

            dgvCustomerReturnItem.SelectionChanged -= new System.EventHandler(dgvStockItem_SelectionChanged);
            dgvCustomerReturnItem.DataSource = new List<CustomerReturnItem>();
            if (m_lstItemDetail != null && m_lstItemDetail.Count > 0)
            {
                dgvCustomerReturnItem.DataSource = m_lstItemDetail;
                //txtTotalAmount.Text = m_lstItemDetail[m_lstItemDetail.Count-1].TotalAmount.ToString();
                if (cmbCustomerType.SelectedValue.Equals(3) )
                {
                    txtTotalAmount.Text = m_lstItemDetail[m_lstItemDetail.Count - 1].TotalAmount.ToString();
                    txtDistributorId.Text = m_lstItemDetail[m_lstItemDetail.Count - 1].DistributorId.ToString();
                    txtInvoiceDate.Text = m_lstItemDetail[m_lstItemDetail.Count - 1].InvoiceDate.ToString();
                    txtInvoiceAmount.Text = m_lstItemDetail[m_lstItemDetail.Count - 1].InvoiceAmount.ToString();
                    txtTaxAmount.Text = m_lstItemDetail[m_lstItemDetail.Count - 1].TaxAmount.ToString();
                    string locCode = SetLocationName(m_lstItemDetail[m_lstItemDetail.Count - 1].InvoiceNo.ToString());
                    //DataRow[] dr1 = new DataRow[5];
                    DataRow[] dr1 = m_dtLocation.Select("LocationCode = '" + locCode + "'");
                    //dr1 = m_dtLocation.Select("LocationCode = '"+locCode +"'");
                    cmbLocation.SelectedValue = dr1[0].ItemArray[0].ToString();
                    btnAddDetails.Enabled = false;
                    pnlInvoiceDetail.Visible = true;
                }
                else
                {
                    btnAddDetails.Enabled = true;
                    pnlInvoiceDetail.Visible = false;
                }
                ResetItemControl();
            }
            dgvCustomerReturnItem.SelectionChanged += new System.EventHandler(dgvStockItem_SelectionChanged);
        }

        /// <summary>
        /// Validate Batch No.
        /// </summary>
        void ValidateBatchNo(Boolean yesNo)
        {
            //if (m_selectedItemRowIndex <= Common.INT_DBNULL && yesNo == false)
            //{
            //    //MessageBox.Show(Common.GetMessage("VAL0056"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    errCustomerReturn.SetError(txtBatchNo, Common.GetMessage("VAL0056", lblBatchNo.Text.Trim().Substring(0, lblBatchNo.Text.Trim().Length - 2)));
            //    return;
            //}


            bool isTextBoxEmpty = CoreComponent.Core.BusinessObjects.Validators.CheckForEmptyString(txtBatchNo.Text.Trim().Length);
            if (isTextBoxEmpty == true && yesNo == false)
                errCustomerReturn.SetError(txtBatchNo, Common.GetMessage("INF0019", lblBatchNo.Text.Trim().Substring(0, lblBatchNo.Text.Trim().Length - 2)));
            else if (isTextBoxEmpty == true)
                errCustomerReturn.SetError(txtBatchNo, string.Empty);
            else if (isTextBoxEmpty == false)
            {
                if (m_F4Press == false && yesNo == true)
                {
                    errCustomerReturn.SetError(txtBatchNo, string.Empty);

                    ItemBatchDetails objItem = new ItemBatchDetails();
                    objItem.ManufactureBatchNo = txtBatchNo.Text.Trim();
                    objItem.ItemCode = txtItemCode.Text.Trim();

                    objItem.LocationId = cmbLocation.SelectedValue.ToString();

                    objItem.BucketId = m_bucketid.ToString();
                    objItem.FromMfgDate = Common.DATETIME_NULL;
                    objItem.ToMfgDate = Common.DATETIME_MAX;

                    List<ItemBatchDetails> lstItemDetails = objItem.Search();
                    if (txtItemCode.Text.Length > 0 && lstItemDetails != null)
                    {
                        List<ItemBatchDetails> query;

                        query = (from p in lstItemDetails where p.ItemId == m_itemId select p).ToList();

                        lstItemDetails = (List<ItemBatchDetails>)query;

                        if (lstItemDetails.Count == 1)
                        {
                            objItem = lstItemDetails[0];
                            GetBatchInfo(objItem);
                        }
                        else if (lstItemDetails.Count > 1)
                            errCustomerReturn.SetError(txtBatchNo, Common.GetMessage("VAL0038", lblBatchNo.Text.Trim().Substring(0, lblBatchNo.Text.Trim().Length - 2)));
                        else
                            errCustomerReturn.SetError(txtBatchNo, Common.GetMessage("VAL0006", lblBatchNo.Text.Trim().Substring(0, lblBatchNo.Text.Trim().Length - 2)));
                    }
                    else
                    {
                        errCustomerReturn.SetError(txtBatchNo, Common.GetMessage("VAL0006", lblBatchNo.Text.Trim().Substring(0, lblBatchNo.Text.Trim().Length - 2)));
                    }
                }
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
        /// Search Customer Returns
        /// </summary>
        List<CustomerReturn> SearchCustomerReturn()
        {
            List<CustomerReturn> lstCustomerReturn = new List<CustomerReturn>();

            DateTime fromDate = dtpSearchFrom.Checked == true ? Convert.ToDateTime(dtpSearchFrom.Value) : Common.DATETIME_NULL;
            DateTime toDate = dtpSearchTo.Checked == true ? Convert.ToDateTime(dtpSearchTo.Value) : Common.DATETIME_MAX;

            lstCustomerReturn = Search(Convert.ToInt32(cmbSearchLocation.SelectedValue), Convert.ToInt32(cmbSearchCustomerType.SelectedValue), txtSearchDistributorPCId.Text.Trim(), Convert.ToInt32(cmbSearchStatus.SelectedValue), fromDate.ToString(Common.DATE_TIME_FORMAT), toDate.ToString(Common.DATE_TIME_FORMAT), Convert.ToInt32(cmbCustomerReturnBy.SelectedValue));

            return lstCustomerReturn;
        }

        /// <summary>
        /// Bind Grid
        /// </summary>
        void BindGrid()
        {
            m_lstCustomerReturn = SearchCustomerReturn();
            if ((m_lstCustomerReturn != null) && (m_lstCustomerReturn.Count > 0))
            {
                dgvCustomerReturn.DataSource = m_lstCustomerReturn;
                dgvCustomerReturn.ClearSelection();
                //dgvInventory.Select();
                ResetItemControl();
            }
            else
            {
                dgvCustomerReturn.DataSource = new List<CustomerReturn>();
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
        List<CustomerReturn> Search(int locationId, int customerType, string returnNo, int statusId, string fromDate, string toDate, int userId)
        {
            List<CustomerReturn> listCustomerReturn = new List<CustomerReturn>();
            CustomerReturn objCustomerReturn = new CustomerReturn();
            objCustomerReturn.LocationId = locationId;
            objCustomerReturn.CustomerType = customerType;
            objCustomerReturn.ReturnNo = returnNo;
            objCustomerReturn.StatusId = statusId;
            objCustomerReturn.FromDate = fromDate;
            objCustomerReturn.ToDate = toDate;
            objCustomerReturn.ApprovedBy = userId;

            listCustomerReturn = objCustomerReturn.Search();

            return listCustomerReturn;
        }

        CustomerReturnItem GetItemInfo(int rowIndex)
        {
            m_isDuplicateRecordFound = Common.INT_DBNULL;

            string itemCode = dgvCustomerReturnItem.Rows[rowIndex].Cells["ItemCode"].Value.ToString().Trim();

            m_batchNo = dgvCustomerReturnItem.Rows[rowIndex].Cells["BatchNo"].Value.ToString();


            ////Get ParentId
            if (m_lstItemDetail == null)
                return null;

            var itemSelect = (from p in m_lstItemDetail where p.ItemCode.ToLower() == itemCode.ToLower() && p.BatchNo == m_batchNo select p);

            if (itemSelect.ToList().Count == 0)
                return null;

            m_selectedItemRowIndex = rowIndex;
            m_itemDetail = itemSelect.ToList()[0];

            m_selectedItemRowNum = m_itemDetail.RowNo;
            m_itemId = m_itemDetail.ItemId;
            m_itemRowNo = m_itemDetail.RowNo;
            m_MRP = m_itemDetail.MRP;

            return m_itemDetail;

        }

        void ValidateStockCountDate()
        {
            if (dtpApprovedDate.Checked == false)
                errCustomerReturn.SetError(dtpApprovedDate, Common.GetMessage("VAL0002", lblApprovedDate.Text.Trim().Substring(0, lblApprovedDate.Text.Trim().Length - 2)));
            else if (dtpApprovedDate.Checked == true)
            {
                DateTime expectedDate = dtpApprovedDate.Checked == true ? Convert.ToDateTime(dtpApprovedDate.Value) : Common.DATETIME_NULL;
                DateTime dt = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                TimeSpan ts = expectedDate - dt;
                if (ts.Days < 0)
                    errCustomerReturn.SetError(dtpApprovedDate, Common.GetMessage("INF0010", lblApprovedDate.Text.Trim().Substring(0, lblApprovedDate.Text.Trim().Length - 2)));
                else
                    errCustomerReturn.SetError(dtpApprovedDate, string.Empty);
            }
        }
        void CreateCustomerReturnObjet()
        {
            if (m_lstCustomerReturn != null && m_objCustomerReturn != null && m_seqNo != string.Empty)
            {
                var query = (from p in m_lstCustomerReturn where p.ReturnNo == m_seqNo select p);

                if (query.ToList().Count > 0)
                    m_objCustomerReturn = query.ToList()[0];
                m_objCustomerReturn.CreatedBy = Common.INT_DBNULL;
                m_objCustomerReturn.CreatedDate = string.Empty;
            }
            if (m_objCustomerReturn == null)
            {
                m_objCustomerReturn = new CustomerReturn();
                m_objCustomerReturn.ReturnNo = string.Empty;
                m_objCustomerReturn.StatusId = (int)Common.CustomerReturnStatus.Created;

                m_objCustomerReturn.CreatedBy = m_userId;
                m_objCustomerReturn.CreatedDate = System.DateTime.Now.ToString(Common.DATE_TIME_FORMAT);

            }
        }
        void Save(int statusId)
        {
            try
            {
                if ((statusId != 1) || (statusId != 3))
                {
                    negateMod = 0;
                    if (m_lstItemDetail == null || m_lstItemDetail.Count == 0)
                    {
                        MessageBox.Show(Common.GetMessage("VAL0024", lblItemCode.Text.Trim().Substring(0, lblItemCode.Text.Trim().Length - 2)), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    #region Check Errors, If Validation fails Show Error and Return From Prog.

                    errCustomerReturn.Clear();
                    if (txtDeductionAmount.Text != string.Empty)
                        ValidateDeductionAmount(true);

                    ValidateCombo(cmbCustomerType, lblCustomerType);
                    ValidateCombo(cmbLocation, lblLocation);
                    if (m_objCustomerReturn != null && statusId == (int)Common.CustomerReturnStatus.Approved)
                        ValidateStockCountDate();
                    if (Convert.ToInt32(cmbCustomerType.SelectedValue) != 3)
                        ValidateDistributorPCId(true);
                    StringBuilder sbError = new StringBuilder();
                    sbError = GenerateError();

                    //If Validation fails Show Error and Return From Prog. 
                    if (!sbError.ToString().Trim().Equals(string.Empty))
                    {
                        MessageBox.Show(sbError.ToString(), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    #endregion


                    // Confirmation Before Saving
                    DialogResult saveResult = MessageBox.Show(Common.GetMessage("5010", Common.GetConfirmationStatusText((((Common.CustomerReturnStatus)statusId).ToString()))), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (saveResult == DialogResult.Yes)
                    {
                        if (statusId.Equals(3))
                        {
                            negateMod = 0;
                            if (cmbCustomerType.SelectedValue.Equals(3))
                            {
                                DialogResult negateresult = MessageBox.Show(Common.GetMessage("40038"), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (negateresult == DialogResult.Yes)
                                    negateMod = 1;
                                else
                                    negateMod = 0;
                            }
                        }
                        CreateCustomerReturnObjet();

                        if (m_objCustomerReturn != null)
                        {
                            if (m_objCustomerReturn.ModifiedDate != null && m_objCustomerReturn.ModifiedDate.Length > 0)
                                m_objCustomerReturn.ModifiedDate = Convert.ToDateTime(m_objCustomerReturn.ModifiedDate).ToString(Common.DATE_TIME_FORMAT);//.ToString(Common.DATE_TIME_FORMAT);
                        }
                        DateTime stockCountDate = dtpApprovedDate.Checked == true ? Convert.ToDateTime(dtpApprovedDate.Value) : Common.DATETIME_NULL;

                        m_objCustomerReturn.LocationId = Common.CurrentLocationId;
                        m_objCustomerReturn.SourceLocationId = Convert.ToInt32(cmbLocation.SelectedValue);
                        m_objCustomerReturn.TotalAmount = Convert.ToDecimal(txtTotalAmount.Text);
                        if (txtDeductionAmount.Text != string.Empty)
                        {
                            m_objCustomerReturn.DeductionAmount = m_objCustomerReturn.DeductionAmount;
                            m_objCustomerReturn.PayableAmount = m_objCustomerReturn.PayableAmount;
                        }
                        else
                        {
                            m_objCustomerReturn.DeductionAmount = 0;
                            m_objCustomerReturn.PayableAmount = m_objCustomerReturn.TotalAmount;
                        }

                        m_objCustomerReturn.ApprovedDate = stockCountDate.ToString(Common.DATE_TIME_FORMAT);
                        m_objCustomerReturn.CustomerType = Convert.ToInt32(cmbCustomerType.SelectedValue);
                        m_objCustomerReturn.ModifiedBy = m_userId;
                        m_objCustomerReturn.ApprovedBy = m_userId;
                        m_objCustomerReturn.ReturnItem = m_lstItemDetail;
                        m_objCustomerReturn.Remarks = txtRemarks.Text.Trim();
                        m_objCustomerReturn.StatusId = statusId;
                        m_objCustomerReturn.DistributorPCId = txtDistributorPCId.Text;

                        // Added by Kaushik
                        if (cmbCustomerType.SelectedValue.Equals(3))
                        {
                            if (txtTaxAmount.Text == "" || txtTaxAmount.Text == null)
                            {
                                m_objCustomerReturn.TaxAmount = 0;
                            }
                            else
                            {
                                m_objCustomerReturn.TaxAmount = Convert.ToDecimal(txtTaxAmount.Text);
                            }
                            if (txtInvoiceAmount.Text == "" || txtInvoiceAmount.Text == null)
                            {
                                m_objCustomerReturn.TaxAmount = 0;
                            }
                            else
                            {
                                m_objCustomerReturn.InvoiceAmount = Convert.ToDecimal(txtInvoiceAmount.Text);
                            }

                        }
                        else
                        {
                            m_objCustomerReturn.TaxAmount = 0;
                            m_objCustomerReturn.InvoiceAmount = 0;
                        }

                        string errorMessage = string.Empty;

                        bool result = m_objCustomerReturn.Save(Common.ToXml(m_objCustomerReturn), negateMod, ref errorMessage);


                        if (errorMessage.Equals(string.Empty))
                        {
                            List<CustomerReturn> lstStockCount = new List<CustomerReturn>();
                            lstStockCount = Search(Convert.ToInt32(cmbLocation.SelectedValue), m_objCustomerReturn.CustomerType, m_objCustomerReturn.ReturnNo, Common.INT_DBNULL, Common.DATETIME_NULL.ToString(Common.DATE_TIME_FORMAT), Common.DATETIME_NULL.ToString(Common.DATE_TIME_FORMAT), Common.INT_DBNULL);

                            txtStatus.Text = lstStockCount[0].StatusName.Trim();


                            if (lstStockCount[0].StatusId == (int)Common.CustomerReturnStatus.Approved)
                                txtApprovedBy.Text = lstStockCount[0].ApprovedName.Trim();
                            m_seqNo = lstStockCount[0].ReturnNo;
                            ReadOnlyHeader();
                            ReadOnlyItemDetail();
                            EnableDisableButton(statusId);

                            //if (Convert.ToInt32(cmbCustomerType.SelectedValue) != 3)
                            //{
                            dgvCustomerReturnItem.DataSource = new List<CustomerReturn>();
                            if (m_lstItemDetail != null && m_lstItemDetail.Count > 0)
                            {
                                m_lstItemDetail = m_objCustomerReturn.SearchItem(m_objCustomerReturn.ReturnNo, customerType);
                                BindItemGrid();
                            }
                            txtPayableAmount.Text = lstStockCount[0].PayableAmount.ToString();
                            txtDeductionAmount.Text = lstStockCount[0].DeductionAmount.ToString();
                            MessageBox.Show(Common.GetMessage("8013", ((Common.CustomerReturnStatus)m_objCustomerReturn.StatusId).ToString(), m_seqNo), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //}

                        }
                        else
                            MessageBox.Show(Common.GetMessage(errorMessage), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);

                        if (m_locationType == (int)Common.LocationConfigId.HO)
                        {
                            dtpApprovedDate.Enabled = true;
                        }
                        else
                        {
                            dtpApprovedDate.Enabled = false;
                        }
                    }
                }
                else
                    MessageBox.Show(Common.GetMessage("8015"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
            }
        }

        void ReadOnlyHeader()
        {
            if (m_locationType == (int)Common.LocationConfigId.HO)
            {
                if ((m_objCustomerReturn == null) || (m_objCustomerReturn != null && m_objCustomerReturn.StatusId <= (int)Common.CustomerReturnStatus.Created))
                    cmbLocation.Enabled = false;
                else
                    cmbLocation.Enabled = false;
            }
            else if (m_locationType == (int)Common.LocationConfigId.BO)
            {
                cmbLocation.SelectedValue = m_currentLocationId.ToString();
                cmbLocation.Enabled = false;
            }


            if (m_objCustomerReturn != null && m_objCustomerReturn.StatusId == (int)Common.CustomerReturnStatus.Created)
                dtpApprovedDate.Enabled = true;
            else
                dtpApprovedDate.Enabled = false;

            if ((m_lstItemDetail != null && m_lstItemDetail.Count > 0))
            {
                cmbCustomerType.Enabled = false;
                txtDistributorPCId.Enabled = false;
            }
            else
            {
                cmbCustomerType.Enabled = true;
                txtDistributorPCId.Enabled = true;
            }
        }
        /// <summary>
        /// This function is used to show data in controls
        /// </summary>
        /// <param name="e"></param>
        private void SelectItemGridRow(EventArgs e)
        {
            if (dgvCustomerReturnItem.SelectedCells.Count > 0)
            {
                int rowIndex = dgvCustomerReturnItem.SelectedCells[0].RowIndex;
                int columnIndex = dgvCustomerReturnItem.SelectedCells[0].ColumnIndex;

                if (rowIndex >= 0 && columnIndex >= 0)
                {
                    int selectedRow = dgvCustomerReturnItem.SelectedCells[0].RowIndex;
                    m_itemDetail = GetItemInfo(rowIndex);
                    m_batchNo = m_itemDetail.BatchNo;
                    txtBatchNo.Text = m_itemDetail.ManufactureBatchNo;
                    txtMRP.Text = m_itemDetail.DisplayMRP.ToString();

                    txtItemCode.Text = m_itemDetail.ItemCode;
                    txtItemDescription.Text = m_itemDetail.ItemName;
                    cmbBucket.SelectedValue = Convert.ToInt32(m_itemDetail.BucketId);
                    txtQuantity.Text = m_itemDetail.DisplayQuantity.ToString();

                    txtDistributorPrice.Text = m_itemDetail.DisplayDistributorPrice.ToString();
                    ReadOnlyItemDetail();
                }
            }
        }

        /// <summary>
        /// Creates DataSet for Printing Stock Count Screen report
        /// </summary>
        private void CreatePrintDataSet()
        {
            try
            {
                m_printDataSet = new DataSet();
                // Get Data For CR Header Informaton in Stock Count Screen Report
                DataTable dtCRHeader = new DataTable("CRHeader");
                DataColumn ReturnNo = new DataColumn("ReturnNo", System.Type.GetType("System.String"));
                DataColumn PartyName = new DataColumn("PartyName", System.Type.GetType("System.String"));
                DataColumn PartyAddress = new DataColumn("PartyAddress", System.Type.GetType("System.String"));
                DataColumn CustomerType = new DataColumn("CustomerType", System.Type.GetType("System.String"));
                DataColumn DistributorPCId = new DataColumn("DistributorPCId", System.Type.GetType("System.String"));
                DataColumn Location = new DataColumn("Location", System.Type.GetType("System.String"));
                DataColumn ApprovedDate = new DataColumn("ApprovedDate", System.Type.GetType("System.String"));
                DataColumn ApprovedBy = new DataColumn("ApprovedBy", System.Type.GetType("System.String"));
                DataColumn LocationAddress = new DataColumn("LocationAddress", System.Type.GetType("System.String"));
                DataColumn Remarks = new DataColumn("Remarks", System.Type.GetType("System.String"));
                DataColumn Status = new DataColumn("Status", System.Type.GetType("System.String"));
                DataColumn TotalAmount = new DataColumn("TotalAmount", System.Type.GetType("System.String"));
                DataColumn DeductionAmount = new DataColumn("DeductionAmount", System.Type.GetType("System.String"));
                DataColumn PayableAmount = new DataColumn("PayableAmount", System.Type.GetType("System.String"));
                DataColumn AmountInWords = new DataColumn("AmountInWords", System.Type.GetType("System.String"));
                dtCRHeader.Columns.Add(ReturnNo);
                dtCRHeader.Columns.Add(PartyName);
                dtCRHeader.Columns.Add(PartyAddress);
                dtCRHeader.Columns.Add(CustomerType);
                dtCRHeader.Columns.Add(DistributorPCId);
                dtCRHeader.Columns.Add(Location);
                dtCRHeader.Columns.Add(LocationAddress);
                dtCRHeader.Columns.Add(Status);
                dtCRHeader.Columns.Add(ApprovedDate);
                dtCRHeader.Columns.Add(ApprovedBy);
                dtCRHeader.Columns.Add(Remarks);
                dtCRHeader.Columns.Add(TotalAmount);
                dtCRHeader.Columns.Add(DeductionAmount);
                dtCRHeader.Columns.Add(PayableAmount);
                dtCRHeader.Columns.Add(AmountInWords);
                DataRow dRow = dtCRHeader.NewRow();
                dRow["ReturnNo"] = m_objCustomerReturn.ReturnNo;
                DataTable dtPartyName;

                if (Convert.ToInt32(m_objCustomerReturn.CustomerType) == 1)
                {
                    dtPartyName = Common.ParameterLookup(Common.ParameterType.Distributor, new ParameterFilter("", 0, 0, Convert.ToInt32(m_objCustomerReturn.DistributorPCId)));
                    dRow["PartyName"] = dtPartyName.Rows[0]["DistributorName"].ToString();
                    dRow["PartyAddress"] = dtPartyName.Rows[0]["Address1"].ToString() + " " + dtPartyName.Rows[0]["Address2"].ToString() + Environment.NewLine + dtPartyName.Rows[0]["Address3"].ToString() + " " + dtPartyName.Rows[0]["Address4"].ToString() + Environment.NewLine + dtPartyName.Rows[0]["CityName"].ToString() + " " + dtPartyName.Rows[0]["StateName"].ToString() + " " + dtPartyName.Rows[0]["CountryName"].ToString();
                }
                //kaushik code starts
                else if (Convert.ToInt32(m_objCustomerReturn.CustomerType) == 3)
                {
                    dtPartyName = Common.ParameterLookup(Common.ParameterType.InvoiceReturn, new ParameterFilter(txtDistributorPCId.Text, 0, 0, 0));
                    dRow["PartyName"] = dtPartyName.Rows[0]["DistributorName"].ToString();
                    dRow["PartyAddress"] = dtPartyName.Rows[0]["Address1"].ToString() + " " + dtPartyName.Rows[0]["Address2"].ToString() + Environment.NewLine + dtPartyName.Rows[0]["Address3"].ToString() + " " + dtPartyName.Rows[0]["Address4"].ToString() + Environment.NewLine + dtPartyName.Rows[0]["CityName"].ToString() + " " + dtPartyName.Rows[0]["StateName"].ToString() + " " + dtPartyName.Rows[0]["CountryName"].ToString();
                }//kaushik code ends
                else
                {
                    dtPartyName = Common.ParameterLookup(Common.ParameterType.LocationsWithAllStatus, new ParameterFilter("", 4, 0, 0));
                    DataRow[] foundRows = dtPartyName.Select("LocationCode = '" + m_objCustomerReturn.DistributorPCId.ToString().Trim() + "'");
                    //DataRow[] foundRows = dtPartyName.Select("LocationId = '" + m_objCustomerReturn.LocationId.ToString().Trim() + "'");

                    dRow["PartyName"] = foundRows[0][3].ToString();
                    dRow["PartyAddress"] = foundRows[0][4].ToString();

                }
                dRow["CustomerType"] = m_objCustomerReturn.CustomerTypeValue;
                dRow["DistributorPCId"] = m_objCustomerReturn.DistributorPCId;
                dRow["Location"] = m_objCustomerReturn.LocationName;
                dRow["LocationAddress"] = txtLocationAddress.Text;
                dRow["Status"] = m_objCustomerReturn.StatusName;
                dRow["ApprovedDate"] = (Convert.ToDateTime(m_objCustomerReturn.ApprovedDate)).ToString(Common.DTP_DATE_FORMAT);
                dRow["ApprovedBy"] = m_objCustomerReturn.ApprovedName;
                dRow["Remarks"] = m_objCustomerReturn.Remarks;
                dRow["TotalAmount"] = m_objCustomerReturn.TotalAmount;
                dRow["DeductionAmount"] = m_objCustomerReturn.DeductionAmount;
                dRow["PayableAmount"] = m_objCustomerReturn.PayableAmount;
                dRow["AmountInWords"] = Common.AmountToWords.AmtInWord(Convert.ToDecimal(dRow["PayableAmount"]));
                dtCRHeader.Rows.Add(dRow);

                // Search ItemData
                DataTable dtCRDetail = new DataTable("CRDetail");
                dtCRDetail = m_objCustomerReturn.SearchItemDataTable(m_objCustomerReturn.ReturnNo, customerType);
                for (int i = 0; i < dtCRDetail.Rows.Count; i++)
                {
                    dtCRDetail.Rows[i]["DistributorPrice"] = Math.Round(Convert.ToDecimal(dtCRDetail.Rows[i]["DistributorPrice"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                    dtCRDetail.Rows[i]["MRP"] = Math.Round(Convert.ToDecimal(dtCRDetail.Rows[i]["MRP"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                    dtCRDetail.Rows[i]["Quantity"] = Math.Round(Convert.ToDecimal(dtCRDetail.Rows[i]["Quantity"]), Common.DisplayQtyRounding, MidpointRounding.AwayFromZero);
                }
                m_printDataSet.Tables.Add(dtCRHeader);
                m_printDataSet.Tables.Add(dtCRDetail.Copy());
            }
            catch (Exception ex)
            {
            }
        }
        /// <summary>
        /// Prints Stock Count Screen report
        /// </summary>
        private void PrintReport()
        {
            CreatePrintDataSet();
            CoreComponent.UI.ReportScreen reportScreenObj = new CoreComponent.UI.ReportScreen((int)Common.ReportType.CR, m_printDataSet);
            reportScreenObj.ShowDialog();
            //reportScreenObj.PrintReport();
            m_printDataSet = null;
        }

        private void PrintCreditNoteReport()
        {
            CreatePrintDataSet();
            CoreComponent.UI.ReportScreen reportScreenObj = new CoreComponent.UI.ReportScreen((int)Common.ReportType.CRCreditNote, m_printDataSet);
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
                RemoveTOItem(e);
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
        /// Call EditCustomerReturn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvCustomerReturn_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                EditCustomerReturn(e);
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
                //ResetItemControl();
                txtItemDescription.Text = string.Empty;
                txtItemCode.Text = string.Empty;
                txtDistributorPrice.Text = string.Empty;
                txtBatchNo.Text = string.Empty;
                txtMRP.Text = string.Empty;
                txtQuantity.Text = string.Empty;
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
                Save((int)Common.CustomerReturnStatus.Cancelled);
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
        private void btnApproved_Click(object sender, EventArgs e)
        {
            try
            {
                Save((int)Common.CustomerReturnStatus.Approved);
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
                m_seqNo = string.Empty;
                tabControlTransaction.TabPages[1].Text = Common.TAB_CREATE_MODE;
                ResetHeaderAndItems();
                EnableDisableButton((int)Common.CustomerReturnStatus.New);
                cmbCustomerType.Focus();
                pnlInvoiceDetail.Visible = false;
                dtpApprovedDate.Enabled = true;
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dtpCustomerReturnDate_Validated(object sender, EventArgs e)
        {
            try
            {
                if (dtpApprovedDate.Checked == true)
                    errCustomerReturn.SetError(dtpApprovedDate, string.Empty);
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

                dgvCustomerReturn.DataSource = new List<CustomerReturn>();
                cmbSearchCustomerType.Focus();
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
                errCustomerReturn.SetError(txtItemCode, string.Empty);
                errCustomerReturn.SetError(txtQuantity, string.Empty);
                ValidateCombo(cmbCustomerType, lblCustomerType);
                if (Convert.ToInt32(cmbCustomerType.SelectedValue) != 3)
                {
                    ValidateDistributorPCId(true);
                }
                ValidateLocationAddress(false);
                //errCustomerReturn.Clear();
                ValidateItemMessages();

                if (ValidateAddButton())
                {
                    CreateCustomerReturnObjet();
                    bool b = CheckValidQuantity();
                   
                    if (m_isDuplicateRecordFound <= 0)
                    {
                        if (AddItem())
                        {
                            BindItemGrid();
                            ReadOnlyHeader();
                        }
                    }

                    var query = (from p in m_lstItemDetail select p.Quantity * p.DistributorPrice).Sum();
                    decimal qtySum = Convert.ToDecimal(query);

                    m_objCustomerReturn.TotalAmount = qtySum;
                    txtTotalAmount.Text = m_objCustomerReturn.DisplayTotalAmount.ToString();
                    //string deduction = txtDeductionAmount.Text;
                    decimal Payable = Convert.ToDecimal(txtTotalAmount.Text.ToString().Length==0?0:Convert.ToDecimal(txtTotalAmount.Text)) - Convert.ToDecimal(txtDeductionAmount.Text.ToString().Length == 0 ? 0 : Convert.ToDecimal(txtDeductionAmount.Text));
                    m_objCustomerReturn.DeductionAmount = Convert.ToDecimal(txtDeductionAmount.Text.Length == 0 ? 0 : Convert.ToDecimal(txtDeductionAmount.Text));
                    m_objCustomerReturn.PayableAmount = Payable;

                    txtDeductionAmount.Text = m_objCustomerReturn.DisplayDeductionAmount.ToString();
                    txtTotalAmount.Text = m_objCustomerReturn.DisplayTotalAmount.ToString();
                    txtPayableAmount.Text = m_objCustomerReturn.DisplayPayableAmount.ToString();
                    //txtDeductionAmount.Text = deduction;
                    //txtPayableAmount.Text = Payable;
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
                if (Convert.ToInt32(cmbLocation.SelectedValue) != Common.INT_DBNULL)
                {
                    if (e.KeyValue == Common.F4KEY && !e.Alt)
                    {
                        NameValueCollection nvc = new NameValueCollection();
                        nvc.Add("LocationId", Convert.ToInt32(cmbLocation.SelectedValue).ToString());

                        CoreComponent.Controls.frmSearch objfrmSearch = new CoreComponent.Controls.frmSearch(CoreComponent.Controls.SearchTypes.Item, nvc);
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
                else
                {
                    MessageBox.Show(Common.GetMessage("VAL0002", lblLocation.Text.Trim().Substring(0, lblLocation.Text.Trim().Length - 2)), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtItemCode.Text = string.Empty;
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
                m_itemDetail = new CustomerReturnItem();
                m_bucketid = Convert.ToInt32(cmbBucket.SelectedValue);
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
                if (e.KeyValue == Common.F4KEY && !e.Alt)
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

        private void cmbLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ValidateLocationAddress(true);
                customerType = cmbCustomerType.SelectedIndex;
                invoiceControls();
                //ResetItemControl();
                //ResetHeaderAndItems();
                txtDistributorPCId.Text = string.Empty;
                txtRemarks.Text = string.Empty;
                dtpApprovedDate.Checked = false;
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
                if (m_objCustomerReturn != null && m_objCustomerReturn.StatusId >= (int)Common.CustomerReturnStatus.Approved)
                {
                    btnPrint.Enabled = false;
                    PrintReport();
                    btnPrint.Enabled = true;
                }
                else
                    MessageBox.Show(Common.GetMessage("INF0101", "Return", Common.CustomerReturnStatus.Approved.ToString()), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            catch (Exception ex)
            {
                btnPrint.Enabled = true;
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        void ValidateDistributorPCId(bool yesNO)
        {
            if (yesNO)
            {
                bool isTextBoxEmpty = CoreComponent.Core.BusinessObjects.Validators.CheckForEmptyString(txtDistributorPCId.Text.Trim().Length);
                if (isTextBoxEmpty == true)
                    if (Convert.ToInt32(cmbCustomerType.SelectedValue) == (int)Common.CustomerType.InvoiceReturn)
                        errCustomerReturn.SetError(txtDistributorPCId, Common.GetMessage("INF0019", lblDistributorIdPCId.Text.Trim().Substring(0, lblDistributorIdPCId.Text.Trim().Length - 2)));
                    else
                        errCustomerReturn.SetError(txtDistributorPCId, Common.GetMessage("INF0019", lblDistributorIdPCId.Text.Trim().Substring(0, lblDistributorIdPCId.Text.Trim().Length - 2)));

                else
                {
                    errCustomerReturn.SetError(txtDistributorPCId, string.Empty);
                    //bool isValidId = CoreComponent.Core.BusinessObjects.Validators.IsInt64(txtDistributorPCId.Text);
                    if (Convert.ToInt32(cmbCustomerType.SelectedValue) == (int)Common.CustomerType.Distributor)
                    {
                        bool isValidId = CoreComponent.Core.BusinessObjects.Validators.IsInt64(txtDistributorPCId.Text);
                        pnlInvoiceDetail.Visible = false;
                        if (isValidId)
                        {
                            DataTable dt = Common.ParameterLookup(Common.ParameterType.Distributor, new ParameterFilter(string.Empty, 0, 0, 0));
                            DataTable dtParent = dt.Copy();

                            DataRow[] dr = dt.Select("DistributorId = " + Convert.ToInt32(txtDistributorPCId.Text) + " And DistributorStatus <> " + (int)Common.DistributorStatus.Cancel);
                            if (dr.Count() == 0)
                                errCustomerReturn.SetError(txtDistributorPCId, Common.GetMessage("VAL0009", lblDistributorIdPCId.Text.Trim().Substring(0, lblDistributorIdPCId.Text.Trim().Length - 2)));
                            else if (dr.Count() > 0)
                            {
                                // Check if child Exists
                                DataRow[] drParent = dtParent.Select("DistributorId = " + Convert.ToInt32(dr[0]["UplineDistributorId"]) + " And DistributorStatus <> " + (int)Common.DistributorStatus.Cancel);
                                if (drParent.Count() == 0)
                                    errCustomerReturn.SetError(txtDistributorPCId, Common.GetMessage("VAL0083", lblDistributorIdPCId.Text.Trim().Substring(0, lblDistributorIdPCId.Text.Trim().Length - 2)));
                                else
                                    errCustomerReturn.SetError(txtDistributorPCId, string.Empty);
                            }
                        }
                        else
                        {
                            errCustomerReturn.SetError(txtDistributorPCId, Common.GetMessage("VAL0009", lblDistributorIdPCId.Text.Trim().Substring(0, lblDistributorIdPCId.Text.Trim().Length - 2)));
                        }
                    }
                    else if (Convert.ToInt32(cmbCustomerType.SelectedValue) == (int)Common.CustomerType.PC)
                    {
                        pnlInvoiceDetail.Visible = false;
                        DataTable dt = Common.ParameterLookup(Common.ParameterType.LocationAddress, new ParameterFilter(string.Empty, -1, 0, 0));

                        DataRow[] dr = dt.Select("LocationType = '" + ((int)CoreComponent.Core.BusinessObjects.Common.LocationConfigId.PC).ToString() + "' And LocationCode = '" + txtDistributorPCId.Text.Trim() + "' And Status =1");

                        if (dr.Count() == 0)
                            errCustomerReturn.SetError(txtDistributorPCId, Common.GetMessage("VAL0009", lblDistributorIdPCId.Text.Trim().Substring(0, lblDistributorIdPCId.Text.Trim().Length - 2)));
                        else
                            errCustomerReturn.SetError(txtDistributorPCId, string.Empty);
                    }
                    else if (Convert.ToInt32(cmbCustomerType.SelectedValue) == ((int)Common.CustomerType.InvoiceReturn) - 1)
                    {
                        try
                        {
                            DataTable dt = Common.ParameterLookup(Common.ParameterType.InvoiceReturn, new ParameterFilter(txtDistributorPCId.Text, 0, 0, 0));


                            List<CustomerReturnItem> itemList = new List<CustomerReturnItem>();

                            if (dt != null && dt.Rows.Count != 0)
                            {
                                CustomerReturn cr = new CustomerReturn();
                                itemList = cr.CreateCusomerReturnItemObject(dt, customerType);
                                dgvCustomerReturnItem.DataSource = new List<CustomerReturn>();

                                m_lstItemDetail = itemList;
                                BindItemGrid();
                                ReadOnlyHeader();
                                CheckValidQuantity();
                                //pnlInvoiceDetail.Visible = true;
                            }

                            if (dt.Rows.Count == 0)
                                errCustomerReturn.SetError(txtDistributorPCId, Common.GetMessage("VAL0009", lblDistributorIdPCId.Text.Trim().Substring(0, lblDistributorIdPCId.Text.Trim().Length - 2)));
                            else
                                errCustomerReturn.SetError(txtDistributorPCId, string.Empty);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }

                    }
                }
            }
        }

        private string SetLocationName(string strInvoiceNo)
        {
            string[] strTemp = strInvoiceNo.Split('/');

            return strTemp[1];
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Save((int)Common.CustomerReturnStatus.Created);
                //dgvCustomerReturnItem.Enabled = false;

            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void ValidateDeductionAmount(bool yesNo)
        {
            if (yesNo)
            {
                ValidateAmount(txtDeductionAmount, lblDeductionAmount);

                if (errCustomerReturn.GetError(txtDeductionAmount) == string.Empty)
                {
                    if (Convert.ToDecimal(txtTotalAmount.Text.Trim() == "" ? "0" : txtTotalAmount.Text) < Convert.ToDecimal(txtDeductionAmount.Text.Trim() == "" ? "0" : txtDeductionAmount.Text))
                    {
                        errCustomerReturn.SetError(txtDeductionAmount, Common.GetMessage("VAL0060", lblDeductionAmount.Text.Trim().Substring(0, lblDeductionAmount.Text.Trim().Length - 1), lblTotalAmount.Text.Trim().Substring(0, lblTotalAmount.Text.Trim().Length - 1)));
                        return;
                    }
                    else
                    {
                        if (m_objCustomerReturn != null)
                        {
                            m_objCustomerReturn.DeductionAmount = Convert.ToDecimal(txtDeductionAmount.Text == "" ? "0" : txtDeductionAmount.Text);
                            m_objCustomerReturn.PayableAmount = (Convert.ToDecimal(txtTotalAmount.Text) - Convert.ToDecimal(txtDeductionAmount.Text == "" ? "0" : txtDeductionAmount.Text));
                            txtPayableAmount.Text = m_objCustomerReturn.DisplayPayableAmount.ToString();
                        }
                        errCustomerReturn.SetError(txtDeductionAmount, string.Empty);
                    }
                }
            }
        }

        private void txtDeductionAmount_Validated(object sender, EventArgs e)
        {
            try
            {
                ValidateDeductionAmount(true);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void ValidateUnitQuantity(bool yesNo)
        {
            if (yesNo)
            {
                ValidateQuantity(txtQuantity, lblQuantity);
            }
        }

        private void txtQuantity_Validated(object sender, EventArgs e)
        {
            try
            {
                ValidateUnitQuantity(true);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtDistributorPCId_Validated(object sender, EventArgs e)
        {
            try
            {
                ValidateDistributorPCId(true);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPrintCreditNode_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_objCustomerReturn != null && m_objCustomerReturn.StatusId >= (int)Common.CustomerReturnStatus.Approved)
                {
                    btnPrintCreditNode.Enabled = false;
                    PrintCreditNoteReport();
                    btnPrintCreditNode.Enabled = true;
                }
                else
                    MessageBox.Show(Common.GetMessage("INF0101", "Return", Common.CustomerReturnStatus.Approved.ToString()), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            catch (Exception ex)
            {
                btnPrintCreditNode.Enabled = true;
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvCustomerReturn_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (dgvCustomerReturn.SelectedRows.Count > 0)
                {
                    string returnNo = Convert.ToString(dgvCustomerReturn.SelectedRows[0].Cells["ReturnNo"].Value);

                    var query = (from p in m_lstCustomerReturn where p.ReturnNo == returnNo select p);
                    m_objCustomerReturn = (CustomerReturn)query.ToList()[0];
                    SelectSearchGrid(returnNo);
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtDistributorPCId_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyValue == Common.F4KEY && !e.Alt)
                {
                    if (cmbCustomerType.SelectedValue.ToString() == "1")
                    {
                        System.Collections.Specialized.NameValueCollection nvc = new NameValueCollection();
                        nvc.Add("DistributorStatus", "2");
                        CoreComponent.Controls.frmSearch objfrmSearch = new CoreComponent.Controls.frmSearch(CoreComponent.Controls.SearchTypes.DistributorSearch, nvc);
                        CoreComponent.BusinessObjects.Distributor _distributor = (CoreComponent.BusinessObjects.Distributor)objfrmSearch.ReturnObject;
                        objfrmSearch.ShowDialog();
                        _distributor = (CoreComponent.BusinessObjects.Distributor)objfrmSearch.ReturnObject;

                        if (_distributor != null)
                        {
                            txtDistributorPCId.Text = _distributor.DistributorId.ToString();
                        }
                    }

                    else if (cmbCustomerType.SelectedValue.ToString() == "2")
                    {
                        System.Collections.Specialized.NameValueCollection nvc = new NameValueCollection();
                        nvc.Add("LocationType", "4");
                        CoreComponent.Controls.frmSearch objfrmSearch = new CoreComponent.Controls.frmSearch(CoreComponent.Controls.SearchTypes.LocationSearch);
                        CoreComponent.Hierarchies.BusinessObjects.LocationHierarchy objLoc = (CoreComponent.Hierarchies.BusinessObjects.LocationHierarchy)objfrmSearch.ReturnObject;
                        objfrmSearch.ShowDialog();
                        objLoc = (CoreComponent.Hierarchies.BusinessObjects.LocationHierarchy)objfrmSearch.ReturnObject;

                        if (objLoc != null)
                        {
                            txtDistributorPCId.Text = objLoc.HierarchyCode.ToString();
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

        private void GetInvoiceInfo(string InvoiceNo)
        {
            try
            {
                string errMsg = string.Empty;

                //Distributor objTemp = new Distributor();
                //DataSet ds = objTemp.FindNextDistributorId(curDistributorId, navigate, ref errMsg);

                ////MessageBox.Show(ds.Tables[0].Rows[0][0].ToString());

                //DisableFields();
                //SetAllInfo(ds.Tables[0].Rows[0][0].ToString());
                //if ((m_objDistributorMain != null) && (m_objDistributorMain.DistributorId > 0))
                //{
                //    btnEdit.Enabled = m_IsUpdateAvailable;
                //}
                //else
                //{
                //    btnEdit.Enabled = false;
                //}

                //SetDistributorNavigation(Convert.ToInt32(txtSearchDistribID.Text.Trim()));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        private void cmbSearchCustomerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            invoiceControls();
            customerType = cmbSearchCustomerType.SelectedIndex;
        }

        private void invoiceControls()
        {
            if (cmbCustomerType.SelectedIndex == 3)
            {
                btnAddDetails.Enabled = false;
                pnlInvoiceDetail.Visible = true;
                if (m_locationType == (int)Common.LocationConfigId.HO)
                    btnSave.Enabled = false;
                else
                    btnSave.Enabled = true;
            }
            else
            {
                btnAddDetails.Enabled = true;
                pnlInvoiceDetail.Visible = false;
            }
        }

        private void dgvCustomerReturnItem_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cmbCustomerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValidateCombo(cmbCustomerType, lblCustomerType);
            customerType = cmbCustomerType.SelectedIndex;
        }

    }
}
