using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CoreComponent.Core.BusinessObjects;
using CoreComponent.BusinessObjects;
using TransfersComponent.BusinessObjects;
using CoreComponent.Hierarchies.BusinessObjects;
using CoreComponent.MasterData.BusinessObjects;
using System.Collections.Specialized;
using AuthenticationComponent.BusinessObjects;

using System.Reflection;

namespace TransfersComponent.UI
{
    public partial class frmTI : CoreComponent.Core.UI.Transaction
    {
        #region Variables
        // int m_currentLocationId = 13;
        // int m_userId = 1;
        // int m_locationType = 2;

        Boolean m_F4Press = false;
        int m_bucketid = Common.INT_DBNULL;
        int m_UOMId = Common.INT_DBNULL;
        decimal m_weight = 0;
        //string m_IndentNo = string.Empty;
        string m_UOMName = string.Empty;
        decimal m_MRP = 0;
        int m_sourceLocationId = Common.INT_DBNULL;

        List<int> m_lstRemovedItem;
        List<TIHeader> m_lstTIHeader;
        List<TIDetail> m_lstTIDetail;
        List<TIDetail> m_lstTIFullDetail;
        TIDetail m_tiDetail;
        TIHeader m_objTI;
        decimal m_oldQty = Common.INT_DBNULL;
        string m_mfgDate = Common.DATETIME_NULL.ToString();
        string m_expDate = Common.DATETIME_NULL.ToString();
        string m_tiNo = Common.INT_DBNULL.ToString();
        int m_selectedItemRowNum = Common.INT_DBNULL;
        int m_selectedItemRowIndex = Common.INT_DBNULL;
        int m_itemId = Common.INT_DBNULL;
        Boolean m_validQuantity = true;        
        string m_batchNo = string.Empty;
        string m_mfgbatchNo = string.Empty;
        bool m_ValidateItem = true;
        int m_isDuplicateRecordFound = Common.INT_DBNULL;

        private Boolean m_isSaveAvailable = false;
        private Boolean m_isSearchAvailable = false;
        private Boolean m_isConfirmAvailable = false;
        private Boolean m_isPrintAvailable = false;

        private int m_userId = Authenticate.LoggedInUser.UserId;
        private string strUserName = Authenticate.LoggedInUser.UserName;

        private string strLocationCode = Common.LocationCode;
        private int m_currentLocationId = Common.CurrentLocationId;
        private int m_locationType = Common.CurrentLocationTypeId;

        private DataSet m_printDataSet;
        LocationList objLocationlst = new LocationList();

        #endregion Variables

        public string Totaltoquan
        {
            get;
            set;
        }

        public string updatetoquan
        {
            get;
            set;
        }

        #region C'tor
        public frmTI()
        {
            try
            {
                m_isSaveAvailable = Authenticate.IsFunctionAccessible(strUserName, strLocationCode, TIHeader.MODULE_CODE, Common.FUNCTIONCODE_SAVE);
                m_isSearchAvailable = Authenticate.IsFunctionAccessible(strUserName, strLocationCode, TIHeader.MODULE_CODE, Common.FUNCTIONCODE_SEARCH);
                m_isConfirmAvailable = Authenticate.IsFunctionAccessible(strUserName, strLocationCode, TIHeader.MODULE_CODE, Common.FUNCTIONCODE_CONFIRM);
                m_isPrintAvailable = Authenticate.IsFunctionAccessible(strUserName, strLocationCode, TIHeader.MODULE_CODE, Common.FUNCTIONCODE_PRINT);

                InitializeComponent();
                GridInitialize();
                FillLocations();
                FillSearchStatus();
                InitializeDateControl();
                EnableDisableButton((int)Common.TIStatus.New);
                lblPageTitle.Text = "Transfer In";
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Methods
        Boolean ValidateSearchControls()
        {
            errSearch.SetError(dtpSearchTOFrom, string.Empty);
            errSearch.SetError(dtpSearchRecFrom, string.Empty);

            ValidateSearchFromReceivedDate();
            ValidateSearchShipDate();

            StringBuilder sbError = new StringBuilder();
            if (errSearch.GetError(dtpSearchTOFrom).Trim().Length > 0)
            {
                sbError.Append(errSearch.GetError(dtpSearchTOFrom));
                sbError.AppendLine();
            }
            if (errSearch.GetError(dtpSearchRecFrom).Trim().Length > 0)
            {
                sbError.Append(errSearch.GetError(dtpSearchRecFrom));
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
        void ValidateSearchShipDate()
        {
            if (dtpSearchTOFrom.Checked == true && dtpSearchTOTO.Checked == true)
            {
                DateTime fromDate = dtpSearchTOFrom.Checked == true ? Convert.ToDateTime(dtpSearchTOFrom.Value) : Common.DATETIME_NULL;
                DateTime toDate = dtpSearchTOTO.Checked == true ? Convert.ToDateTime(dtpSearchTOTO.Value) : Common.DATETIME_NULL;

                TimeSpan ts = fromDate - toDate;
                if (ts.Days > 0)
                    errSearch.SetError(dtpSearchTOFrom, Common.GetMessage("VAL0047", lblSearchTOToDate.Text.Trim().Substring(0, lblSearchTOToDate.Text.Trim().Length - 1), lblSearchTOFromDate.Text.Trim().Substring(0, lblSearchTOFromDate.Text.Trim().Length - 1)));
                else
                    errSearch.SetError(dtpSearchTOFrom, string.Empty);
            }
        }

        /// <summary>
        /// Validate TOI From and To Date
        /// </summary>
        void ValidateSearchFromReceivedDate()
        {
            if (dtpSearchRecFrom.Checked == true && dtpSearchRecTO.Checked == true)
            {
                DateTime fromDate = dtpSearchRecFrom.Checked == true ? Convert.ToDateTime(dtpSearchRecFrom.Value) : Common.DATETIME_NULL;
                DateTime toDate = dtpSearchRecTO.Checked == true ? Convert.ToDateTime(dtpSearchRecTO.Value) : Common.DATETIME_NULL;

                TimeSpan ts = fromDate - toDate;
                if (ts.Days > 0)
                    errSearch.SetError(dtpSearchRecFrom, Common.GetMessage("VAL0047", lblSearchTOICreationDateTO.Text.Trim().Substring(0, lblSearchTOICreationDateTO.Text.Trim().Length - 1), lblSearchTOICreationDate.Text.Trim().Substring(0, lblSearchTOICreationDate.Text.Trim().Length - 1)));
                else
                    errSearch.SetError(dtpSearchRecFrom, string.Empty);
            }
        }
        /// <summary>
        /// Grid Initialize
        /// </summary>
        void GridInitialize()
        {
            dgvSearchTI.AutoGenerateColumns = false;
            dgvSearchTI.DataSource = null;
            DataGridView dgvSearchTONew = Common.GetDataGridViewColumns(dgvSearchTI, Environment.CurrentDirectory + "\\App_Data\\Transfer.xml");

            dgvTIItem.AutoGenerateColumns = false;
            dgvTIItem.DataSource = null;
            DataGridView dgvTOItemNew = Common.GetDataGridViewColumns(dgvTIItem, Environment.CurrentDirectory + "\\App_Data\\Transfer.xml");
        }
        /// <summary>
        /// Fill Search and Update Location Drop Down List
        /// </summary>
        private void FillLocations()
        {
            DataTable dtSearchSource = Common.ParameterLookup(Common.ParameterType.Locations, new ParameterFilter("LOCATIONS", 0, 0, 0));
            if (dtSearchSource != null)
            {
                cmbSearchSourceLocation.DataSource = dtSearchSource;
                cmbSearchSourceLocation.DisplayMember = "DisplayName";
                cmbSearchSourceLocation.ValueMember = "LocationId";
            }

            DataTable dtSearchDest = Common.ParameterLookup(Common.ParameterType.Locations, new ParameterFilter("LOCATIONS", 0, 0, 0));
            if (dtSearchDest != null)
            {
                cmbSearchDestinationLocation.DataSource = dtSearchDest;
                cmbSearchDestinationLocation.DisplayMember = "DisplayName";
                cmbSearchDestinationLocation.ValueMember = "LocationId";

                if (m_locationType == (int)Common.LocationConfigId.BO || m_locationType == (int)Common.LocationConfigId.WH)
                {
                    cmbSearchDestinationLocation.SelectedValue = m_currentLocationId.ToString();
                    cmbSearchDestinationLocation.Enabled = false;
                }
            }
        }
        /// <summary>
        /// Fill Status Drop Down List
        /// </summary>
        private void FillSearchStatus()
        {
            DataTable dt = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter("TIStatus", 0, 0, 0));
            cmbSearchStatus.DataSource = dt;
            cmbSearchStatus.DisplayMember = Common.KEYVALUE1;
            cmbSearchStatus.ValueMember = Common.KEYCODE1;
        }
        /// <summary>
        /// Initialize Date Picker Value
        /// </summary>
        void InitializeDateControl()
        {
            dtpSearchRecFrom.CustomFormat = Common.DTP_DATE_FORMAT;
            dtpSearchRecTO.CustomFormat = Common.DTP_DATE_FORMAT;
            dtpSearchTOFrom.CustomFormat = Common.DTP_DATE_FORMAT;
            dtpSearchTOTO.CustomFormat = Common.DTP_DATE_FORMAT;
            dtpSearchRecFrom.Checked = false;
            dtpSearchRecTO.Checked = false;
            dtpSearchTOFrom.Checked = false;
            dtpSearchTOTO.Checked = false;

            dtpReceivedDate.CustomFormat = Common.DTP_DATE_FORMAT;
            dtpReceivedDate.Checked = false;

            dtpSearchRecFrom.Value = Convert.ToDateTime(System.DateTime.Now.ToShortDateString());
            dtpSearchRecTO.Value = Convert.ToDateTime(System.DateTime.Now.ToShortDateString());
            dtpSearchTOFrom.Value = Convert.ToDateTime(System.DateTime.Now.ToShortDateString());
            dtpSearchTOTO.Value = Convert.ToDateTime(System.DateTime.Now.ToShortDateString());
        }
        /// <summary>
        /// Search TO
        /// </summary>
        List<TIHeader> SearchTI(string toNumber)
        {
            DateTime DATETIME_MAX = Convert.ToDateTime(new DateTime(2099, 12, 31).ToString(Common.DATE_TIME_FORMAT));

            DateTime toFrom = dtpSearchTOFrom.Checked == true ? Convert.ToDateTime(dtpSearchTOFrom.Value) : Common.DATETIME_NULL;
            DateTime toTo = dtpSearchTOTO.Checked == true ? Convert.ToDateTime(dtpSearchTOTO.Value) : DATETIME_MAX;

            DateTime shipTo = dtpSearchRecTO.Checked == true ? Convert.ToDateTime(dtpSearchRecTO.Value) : DATETIME_MAX;
            DateTime shipFrom = dtpSearchRecFrom.Checked == true ? Convert.ToDateTime(dtpSearchRecFrom.Value) : Common.DATETIME_NULL;

            List<TIHeader> toHeader = new List<TIHeader>();
            toHeader = Search(toNumber, string.Empty, Common.INT_DBNULL, Common.INT_DBNULL, toFrom.ToString(), toTo.ToString(), shipFrom.ToString(), shipTo.ToString(), Common.INT_DBNULL, Common.INT_DBNULL, Common.INT_DBNULL);
            return toHeader;
        }
        /// <summary>
        /// Search TOIs
        /// </summary>
        void SearchTI()
        {
            DateTime DATETIME_MAX = Convert.ToDateTime(new DateTime(2099, 12, 31).ToString(Common.DATE_TIME_FORMAT));

            DateTime toFrom = dtpSearchTOFrom.Checked == true ? Convert.ToDateTime(dtpSearchTOFrom.Value) : Common.DATETIME_NULL;
            DateTime toTo = dtpSearchTOTO.Checked == true ? Convert.ToDateTime(dtpSearchTOTO.Value) : DATETIME_MAX;

            DateTime shipTo = dtpSearchRecTO.Checked == true ? Convert.ToDateTime(dtpSearchRecTO.Value) : DATETIME_MAX;
            DateTime shipFrom = dtpSearchRecFrom.Checked == true ? Convert.ToDateTime(dtpSearchRecFrom.Value) : Common.DATETIME_NULL;

            int identised = Convert.ToInt32(chkSearchIndentised.CheckState);
            int isexport = Convert.ToInt32(chkexport.CheckState);

            m_lstTIHeader = Search(txtSearchTONumber.Text.Trim(), txtSearchTINumber.Text.Trim(), Convert.ToInt32(cmbSearchSourceLocation.SelectedValue), Convert.ToInt32(cmbSearchDestinationLocation.SelectedValue), toFrom.ToString(Common.DATE_TIME_FORMAT), toTo.ToString(Common.DATE_TIME_FORMAT), shipFrom.ToString(Common.DATE_TIME_FORMAT), shipTo.ToString(Common.DATE_TIME_FORMAT), Convert.ToInt32(cmbSearchStatus.SelectedValue), identised, isexport);

            if ((m_lstTIHeader != null) && (m_lstTIHeader.Count > 0))
            {
                dgvSearchTI.DataSource = m_lstTIHeader;
                dgvTIItem.Select();
                dgvSearchTI.ClearSelection();
                ResetItemControl();
            }
            else
            {
                dgvSearchTI.DataSource = new List<TIHeader>();
                MessageBox.Show(Common.GetMessage("8002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// Return List of TOI
        /// </summary>
        /// <param name="toiNumber"></param>
        /// <param name="fromLocation"></param>
        /// <param name="toLocation"></param>
        /// <param name="toiFromDate"></param>
        /// <param name="toiToDate"></param>
        /// <param name="creationFromDate"></param>
        /// <param name="creationToDate"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        List<TIHeader> Search(string toNumber, string tiNumber, int fromLocation, int toLocation, string toFromDate, string toToDate, string recvFromDate, string recvToDate, int status, int identised ,int isexport)
        {
            List<TIHeader> listTI = new List<TIHeader>();
            TIHeader objTI = new TIHeader();
            objTI.TONumber = toNumber;
            objTI.TNumber = tiNumber;
            objTI.FromReceiveDate = recvFromDate;
            objTI.ToReceiveDate = recvToDate;
            objTI.StatusId = status;
            objTI.FromTODate = toFromDate;
            objTI.ToTODate = toToDate;
            objTI.SourceLocationId = fromLocation;
            objTI.DestinationLocationId = toLocation;
            objTI.Indentised = identised;
            objTI.Isexport = isexport;
            listTI = objTI.Search();
            return listTI;
        }
        /// <summary>
        /// Validate Shipping Details
        /// </summary>
        void ValidateShippingDetails()
        {
            ValidatedText(txtShippingDetails, lblShippingDetails);
        }
        /// <summary>
        /// Validate Shipping Bill No.
        /// </summary>
        void ValidateShippingWayBillNo()
        {
            ValidatedText(txtShippingBillNo, lblShippingWayNo);
        }

        /// <summary>
        /// Show Location TIN/CST/VAT Information
        /// </summary>
        /// <param name="sourceLocationID"></param>
        /// <param name="destinationLocationID"></param>
        void ShowControlTaxInfor(int sourceLocationID, int destinationLocationID)
        {
            LocationHierarchy locHie = new LocationHierarchy();

            locHie.HierarchyId = sourceLocationID;
            locHie.HierarchyLevel = Common.INT_DBNULL;
            locHie.HierarchyType = Common.INT_DBNULL;
            locHie.LocationType = Common.INT_DBNULL;
            List<LocationHierarchy> lstHier = new List<LocationHierarchy>();
            lstHier = locHie.Search();
            if (lstHier != null && lstHier.Count > 0)
            {
                txtSourceVAT.Text = lstHier[0].VatNo;
                txtSourceCST.Text = lstHier[0].CstNo;
                txtSourceTin.Text = lstHier[0].TinNo;
            }

            locHie.HierarchyId = destinationLocationID;
            lstHier = locHie.Search();
            if (lstHier != null && lstHier.Count > 0)
            {
                txtDestVAT.Text = lstHier[0].VatNo;
                txtDestCST.Text = lstHier[0].CstNo;
                txtDestTin.Text = lstHier[0].TinNo;
            }
        }
        /// <summary>
        /// Remove Location Contact Record
        /// </summary>
        /// <param name="e"></param>
        void RemoveTOItem(DataGridViewCellEventArgs e)
        {
            Totaltoquan = txtTotalQty.Text;
            updatetoquan = dgvTIItem.Rows[e.RowIndex].Cells[6].Value.ToString();
            if ((e.RowIndex >= 0) && (dgvTIItem.Rows[e.RowIndex].Cells[e.ColumnIndex].GetType() == typeof(DataGridViewImageCell)))
            {
                if ((m_objTI == null) || (m_objTI != null && Convert.ToInt32(m_objTI.StatusId) < (int)Common.TIStatus.Confirmed))
                {
                    DialogResult saveResult = MessageBox.Show(Common.GetMessage("INF0028"), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (saveResult == DialogResult.Yes)
                    {
                        if (m_lstRemovedItem == null)
                            m_lstRemovedItem = new List<int>();

                        if (m_lstTIDetail.Count > 0)
                        {
                            m_lstRemovedItem.Add(m_lstTIDetail[e.RowIndex].RowNo);

                            dgvTIItem.DataSource = null;
                            m_lstTIDetail.RemoveAt(e.RowIndex);
                            dgvTIItem.DataSource = m_lstTIDetail;
                            dgvTIItem.Select();
                            ResetItemControl();
                        }

                        ReadOnlyTOField();
                        //bool b = CheckValidQuantity(m_objTI.StatusId);
                        //m_validQuantity = true;
                        txtTotalQty.Text = (Convert.ToInt32(Totaltoquan) - Convert.ToInt32(updatetoquan)).ToString();

                    }
                }
                else
                {
                    MessageBox.Show(Common.GetMessage("VAL0045"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            }
        }
        /// <summary>
        /// This function is used to show data in controls
        /// </summary>
        /// <param name="e"></param>
        private void SelectGridRow(DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex > 0)
            {
                m_isDuplicateRecordFound = Common.INT_DBNULL;
                m_validQuantity = true;

                string itemCode = dgvTIItem.Rows[e.RowIndex].Cells["ItemCode"].Value.ToString().Trim();
                m_batchNo = dgvTIItem.Rows[e.RowIndex].Cells["BatchNo"].Value.ToString().Trim();
                m_bucketid = Convert.ToInt32(dgvTIItem.Rows[e.RowIndex].Cells["BucketId"].Value.ToString());

                //m_RowNo = Convert.ToInt32(dgvTOIItem.Rows[e.RowIndex].Cells["RowNo"].Value.ToString().Trim());

                if (m_lstTIDetail == null)
                    return;

                var itemSelect = (from p in m_lstTIDetail where p.ItemCode.ToLower() == itemCode.ToLower() && p.BatchNo.ToLower() == m_batchNo.ToLower() && p.BucketId == m_bucketid select p);

                if (itemSelect.ToList().Count == 0)
                    return;

                m_selectedItemRowIndex = e.RowIndex;

                m_tiDetail = itemSelect.ToList()[0];
                //m_IndentNo = m_tiDetail.IndentNo;
                m_batchNo = m_tiDetail.BatchNo;
                m_itemId = m_tiDetail.ItemId;
                m_UOMId = m_tiDetail.UOMId;
                m_weight = m_tiDetail.Weight;
                m_UOMName = m_tiDetail.UOMName;
                m_MRP = m_tiDetail.MRP;
                m_mfgDate = m_tiDetail.MfgDate;
                m_expDate = m_tiDetail.ExpDate;
                m_oldQty = m_tiDetail.AfterAdjustQty;
                m_bucketid = m_tiDetail.BucketId;

                btnAddDetails.Focus();

                txtWeight.Text = Math.Round(m_weight, 2).ToString();
                txtItemCode.Text = m_tiDetail.ItemCode;
                txtItemDescription.Text = m_tiDetail.ItemName;
                txtBucketName.Text = m_tiDetail.BucketName;
                txtBatchNo.Text = m_tiDetail.ManufactureBatchNo;
                //txtIndentNo.Text = m_IndentNo;
                txtAvailableQty.Text = m_tiDetail.DisplayAvailableQty.ToString();
                txtAdjustableQty.Text = m_tiDetail.DisplayAfterAdjustQty.ToString();
                txtTransferPrice.Text = m_tiDetail.DisplayItemUnitPrice.ToString();
                txtItemTotalAmount.Text = m_tiDetail.DisplayItemTotalAmount.ToString();
                txtRequestedQty.Text = m_tiDetail.DisplayRequestQty.ToString();
                txtUOMName.Text = m_UOMName;

                errItem.Clear();

                txtTotalQty.Text = m_objTI.DisplayTotalTOQuantity.ToString();
                txtTotalTOAmount.Text = m_objTI.DisplayTotalTOAmount.ToString();
                txtGrossWeight.Text = Math.Round(m_objTI.GrossWeight, 2).ToString();
            }
        }
        /// <summary>
        /// Reset Item Controls
        /// </summary>
        void ResetItemControl()
        {
            m_F4Press = false;
            txtItemCode.ReadOnly = false;
            txtBatchNo.ReadOnly = false;
            m_selectedItemRowNum = Common.INT_DBNULL;
            m_itemId = Common.INT_DBNULL;
            m_bucketid = Common.INT_DBNULL;
            m_batchNo = string.Empty;
            m_tiDetail = null;
            m_mfgbatchNo = string.Empty;
            string m_mfgDate = Common.DATETIME_NULL.ToString();
            string m_expDate = Common.DATETIME_NULL.ToString();

            m_selectedItemRowIndex = Common.INT_DBNULL;
            CoreComponent.Core.BusinessObjects.VisitControls visitControls = new CoreComponent.Core.BusinessObjects.VisitControls();
            visitControls.ResetAllControlsInPanel(errItem, grpAddDetails);

            dgvTIItem.ClearSelection();
            txtItemCode.Focus();
        }
        /// <summary>
        /// Validate Controls On Add Button Click
        /// </summary>
        void ValidateMessages()
        {
            ValidateTONumber(false);
            ValidateItemCode(false);
            ValidateBatchNo(false);
            ValidateQuantity(false);
        }
        /// <summary>
        /// Generate string for Error
        /// </summary>
        /// <returns></returns>
        private StringBuilder GenerateError()
        {
            StringBuilder sbError = new StringBuilder();
            if (errItem.GetError(txtTONumber).Trim().Length > 0)
            {
                sbError.Append(errItem.GetError(txtTONumber));
                sbError.AppendLine();
            }
            if (errItem.GetError(txtPackSize).Trim().Length > 0)
            {
                sbError.Append(errItem.GetError(txtPackSize));
                sbError.AppendLine();
            }
            if (errItem.GetError(dtpReceivedDate).Trim().Length > 0)
            {
                sbError.Append(errItem.GetError(dtpReceivedDate));
                sbError.AppendLine();
            }
            //if (errItem.GetError(dtpReceivedTime).Trim().Length > 0)
            //{
            //    sbError.Append(errItem.GetError(dtpReceivedTime));
            //    sbError.AppendLine();
            //}
            if (errItem.GetError(txtShippingDetails).Trim().Length > 0)
            {
                sbError.Append(errItem.GetError(txtShippingDetails));
                sbError.AppendLine();
            }

            if (errItem.GetError(txtShippingBillNo).Trim().Length > 0)
            {
                sbError.Append(errItem.GetError(txtShippingBillNo));
                sbError.AppendLine();
            }

            if (errItem.GetError(txtItemCode).Trim().Length > 0)
            {
                sbError.Append(errItem.GetError(txtItemCode));
                sbError.AppendLine();
            }

            if (errItem.GetError(txtBatchNo).Trim().Length > 0)
            {
                sbError.Append(errItem.GetError(txtBatchNo));
                sbError.AppendLine();
            }
            if (errItem.GetError(txtAdjustableQty).Trim().Length > 0)
            {
                sbError.Append(errItem.GetError(txtAdjustableQty));
                sbError.AppendLine();
            }
            return Common.ReturnErrorMessage(sbError);
        }
        /// <summary>
        /// Validate Controls On Save Button Click
        /// </summary>
        void ValidateOnSave(int statusId)
        {
            ValidateTONumber(false);
            ValidatePackSize();

            if (statusId == (int)Common.TIStatus.Confirmed)
            {
                ValidateReceivedDate();
                //ValidateReceivedTime();
            }
            ValidateShippingDetails();
            ValidateShippingWayBillNo();
        }
        /// <summary>
        /// Add Item on add button click 
        /// </summary>
        /// <returns></returns>
        bool AddItem()
        {
            //m_tiDetail = new TIDetail();
            m_tiDetail.IndexSeqNo = Common.INT_DBNULL;
            m_tiDetail.ItemId = m_itemId;
            m_tiDetail.ItemCode = txtItemCode.Text;
            m_tiDetail.ItemName = txtItemDescription.Text;
            m_tiDetail.ManufactureBatchNo = txtBatchNo.Text;

            m_tiDetail.BatchNo = m_batchNo;
            m_tiDetail.BucketId = m_bucketid;
            m_tiDetail.MRP = m_MRP;
            m_tiDetail.UOMId = m_UOMId;
            m_tiDetail.Weight = m_weight;
            m_tiDetail.UOMName = m_UOMName;
            m_tiDetail.MfgDate = m_mfgDate;
            m_tiDetail.ExpDate = m_expDate;
            
            m_tiDetail.BucketName = txtBucketName.Text;
            //m_tiDetail.IndentNo = m_IndentNo;
            //m_tiDetail.AvailableQty = Math.Round(Convert.ToDecimal(txtAvailableQty.Text), 2);
            //m_tiDetail.AfterAdjustQty = Math.Round(Convert.ToDecimal(txtAdjustableQty.Text), 2);
            //m_tiDetail.RequestQty = Math.Round(Convert.ToDecimal(txtRequestedQty.Text), 2);
            //m_tiDetail.ItemUnitPrice = Math.Round(Convert.ToDecimal(txtTransferPrice.Text), 2);
            //m_tiDetail.ItemTotalAmount = Math.Round(Convert.ToDecimal(txtItemTotalAmount.Text), 2);

            m_tiDetail.RowNo = m_selectedItemRowNum;

            //decimal amt = CalculateTotalItemPrice();

            if (m_lstTIDetail == null)
                m_lstTIDetail = new List<TIDetail>();

            if ((m_selectedItemRowIndex != Common.INT_DBNULL) && (m_selectedItemRowIndex <= dgvTIItem.Rows.Count))
            {
                m_lstTIDetail.Insert(m_selectedItemRowIndex, m_tiDetail);
                m_lstTIDetail.RemoveAt(m_selectedItemRowIndex + 1);
            }
            else
                m_lstTIDetail.Add(m_tiDetail);

            ResetItemControl();
            return true;
        }
        List<TIDetail> CopyTIDetail(int excludeIndex, List<TIDetail> lst)
        {
            List<TIDetail> returnList = new List<TIDetail>();
            for (int i = 0; i < lst.Count; i++)
            {
                if (i != excludeIndex)
                {
                    TIDetail tdetail = new TIDetail();
                    tdetail = lst[i];
                    returnList.Add(tdetail);
                }
            }

            return returnList;
        }

        bool CheckValidQuantity(int statusId)
        {
            m_isDuplicateRecordFound = 0;
            // Check Dusplicate Code For ItemCode and Batch No.
            if (txtAdjustableQty.Text.Length > 0)
            {
                m_tiDetail.AvailableQty = Convert.ToDecimal(txtAvailableQty.Text);
                m_tiDetail.AfterAdjustQty = Convert.ToDecimal(txtAdjustableQty.Text);

                decimal totQty = m_tiDetail.AfterAdjustQty * 1;
                decimal availQty = m_tiDetail.AvailableQty;

                if (m_lstTIDetail != null && m_lstTIDetail.Count > 0)
                {
                    List<TIDetail> tiDetail = CopyTIDetail(m_selectedItemRowIndex, m_lstTIDetail);
                    //checked based on ItemCode and Bucket Id
                    m_isDuplicateRecordFound = (from p in tiDetail where p.ItemCode.Trim().ToLower() == txtItemCode.Text.Trim().ToLower() && p.BatchNo.ToLower() == m_batchNo.ToLower() && p.BucketId == m_bucketid select p).Count();

                    //if (m_selectedItemRowIndex >= 0 && m_isDuplicateRecordFound == 1)
                    //{
                    //    m_isDuplicateRecordFound = Common.INT_DBNULL;
                    //}

                    if (m_isDuplicateRecordFound > 0)
                    {
                        MessageBox.Show(Common.GetMessage("VAL0032", "item code", "batch no"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
                else if (Convert.ToDecimal(txtAdjustableQty.Text) > Convert.ToDecimal(txtRequestedQty.Text))
                {
                    m_validQuantity = false;
                    return false;
                }

            }

            // Check TotalQty + Qty In TextBox <=Total Item Qty

            //var lstEditedItemGroupBy = from t in m_lstTIDetail
            //                           group t by t.ItemCode into g
            //                           select new
            //                           {
            //                               ItemID = g.First<TIDetail>().ItemCode,
            //                               RequestQty = g.First<TIDetail>().RequestQty,
            //                               ItemCount = g.Count(),
            //                               TotalAvailableQty = g.Sum(p => p.AvailableQty),
            //                               TotalAdjustableQty = g.Sum(p => p.AfterAdjustQty)
            //                           };

            var lstEditedItemGroupBy = m_lstTIDetail.GroupBy(x => new { x.ItemId, x.BucketId })
                                      .Select(g => new
                                      {
                                          ItemID = g.First<TIDetail>().ItemCode.ToLower(),
                                          RequestQty = g.First<TIDetail>().RequestQty,
                                          BucketId = g.First<TIDetail>().BucketId,
                                          TotalAvailableQty = g.Sum(p => p.AvailableQty),
                                          TotalAdjustableQty = g.Sum(p => p.AfterAdjustQty)
                                      });


            // No of Item in Grid Should match with added Item
            List<TIDetail> tiItemDetail = new List<TIDetail>();
            TIHeader objTI = new TIHeader();
            string errMessage = string.Empty;

            if (m_objTI.StatusId < (int)Common.TIStatus.Created)
                tiItemDetail = m_objTI.SearchItem(txtTONumber.Text.ToString(), m_objTI.TNumber, m_sourceLocationId);
            else
                tiItemDetail = m_objTI.SearchItem(string.Empty, m_objTI.TNumber, m_sourceLocationId);

            //var lstItemGroupByBeforeSearch = from t in tiItemDetail
            //                                 group t by t.ItemCode into g
            //                                 select new
            //                                 {
            //                                     ItemID = g.First<TIDetail>().ItemCode,
            //                                     RequestQty = g.First<TIDetail>().RequestQty,
            //                                     ItemCount = g.Count(),
            //                                     TotalAvailableQty = g.Sum(p => p.AvailableQty),
            //                                     TotalAdjustableQty = g.Sum(p => p.AfterAdjustQty)
            //                                 };

            var lstItemGroupByBeforeSearch = tiItemDetail.GroupBy(x => new { x.ItemId, x.BucketId })
                                              .Select(g => new
                                              {
                                                  ItemID = g.First<TIDetail>().ItemCode,
                                                  RequestQty = g.First<TIDetail>().RequestQty,
                                                  BucketId = g.First<TIDetail>().BucketId,
                                                  TotalAvailableQty = g.Sum(p => p.AvailableQty),
                                                  TotalAdjustableQty = g.Sum(p => p.AfterAdjustQty)
                                              });

            string itemlist = string.Empty;
            m_tiDetail.AfterAdjustQty = Convert.ToDecimal(txtAdjustableQty.Text.Trim().Length == 0 ? "0" : txtAdjustableQty.Text);
            decimal itemQty = m_tiDetail.AfterAdjustQty;

            for (int i = 0; i < lstEditedItemGroupBy.ToList().Count; i++)
            {
                if ((m_selectedItemRowIndex >= 0) && (lstEditedItemGroupBy.ToList()[i].ItemID.ToLower().Trim() == txtItemCode.Text.ToLower().Trim() && lstEditedItemGroupBy.ToList()[i].RequestQty < lstEditedItemGroupBy.ToList()[i].TotalAdjustableQty))
                    itemlist = itemlist + lstEditedItemGroupBy.ToList()[i].ItemID + ",";
                else if ((m_selectedItemRowIndex == Common.INT_DBNULL) && (lstEditedItemGroupBy.ToList()[i].ItemID.ToLower().Trim() == txtItemCode.Text.ToLower().Trim() && lstEditedItemGroupBy.ToList()[i].RequestQty < lstEditedItemGroupBy.ToList()[i].TotalAdjustableQty + itemQty))
                    itemlist = itemlist + lstEditedItemGroupBy.ToList()[i].ItemID + ",";
            }


            if (itemlist != string.Empty)
            {
                m_validQuantity = false;
                return false;
            }

            // txtItemTotalAmount.Text = Math.Round(Convert.ToDecimal(txtTransferPrice.Text.Length==0?"0":txtTransferPrice.Text) * Convert.ToDecimal(txtAdjustableQty.Text.Length==0?"0":txtAdjustableQty.Text), 2).ToString();

            return m_validQuantity;
        }
        /// <summary>
        /// Reset Item When Change of Tab Control
        /// </summary>
        /// <param name="e"></param>
        void tabControlSelect(TabControlCancelEventArgs e)
        {
            if ((tabControlTransaction.SelectedIndex == 0) && txtTONumber.Text.Trim().Length > 0)
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
                    ////ReadOnlyTOIField();
                }
            }
            else if (tabControlTransaction.SelectedIndex == 1)
            {
                if (m_locationType == 1 && tabControlTransaction.TabPages[1].Text == Common.TAB_CREATE_MODE)
                {
                    MessageBox.Show(Common.GetMessage("VAL0119", "TI"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tabControlTransaction.SelectedIndex = 0;
                }
                if (tabControlTransaction.TabPages[1].Text == Common.TAB_CREATE_MODE)
                {
                    ResetTOAndItems();
                    EnableDisableButton((int)Common.TIStatus.New);
                }

            }
        }
        /// <summary>
        /// Reset TO and Item Controls
        /// </summary>
        void ResetTOAndItems()
        {
            txtTONumber.Enabled = true;
            txtPackSize.Text = string.Empty;
            txtTONumber.Text = string.Empty;
            txtTINumber.Text = string.Empty;
            m_lstTIFullDetail = null;
            m_tiDetail = null;
            m_tiNo = null;
            m_objTI = null;
            dtpReceivedDate.Value = Convert.ToDateTime(System.DateTime.Now.ToShortDateString());
            dtpReceivedDate.Checked = false;

            dtpReceivedTime.Value = Convert.ToDateTime(System.DateTime.Now.ToString());
            dtpReceivedTime.Checked = false;
            EmptyErrorProvider();
            txtStatus.Text = Common.TIStatus.New.ToString();
            txtDestCST.Text = string.Empty;
            txtDestTin.Text = string.Empty;
            txtDestVAT.Text = string.Empty;
            txtSourceCST.Text = string.Empty;
            txtSourceVAT.Text = string.Empty;
            txtSourceTin.Text = string.Empty;

            txtDestinationAddress.Text = string.Empty;
            txtDestinationLocation.Text = string.Empty;
            txtSourceLocation.Text = string.Empty;
            txtSourceAddress.Text = string.Empty;


            txtShippingDetails.Text = string.Empty;
            txtShippingBillNo.Text = string.Empty;
            txtRemarks.Text = string.Empty;
            txtTotalQty.Text = string.Empty;
            txtTotalTOAmount.Text = string.Empty;
            txtGrossWeight.Text = string.Empty;

            ResetItemControl();
            errItem.SetError(txtItemCode, String.Empty);
            dgvTIItem.DataSource = new List<TIDetail>();
            
        }

        void CreateTIObject()
        {
            if (m_objTI != null && txtTONumber.Text.Trim().Length > 0 && m_lstTIHeader != null)
            {
                var query = (from p in m_lstTIHeader where p.TOINumber.ToLower().Trim() == txtTONumber.Text.ToLower().Trim() select p);
                List<TIHeader> lstToHead = new List<TIHeader>();
                lstToHead = query.ToList();

                if (lstToHead != null && lstToHead.Count > 0)
                    m_objTI = lstToHead[0];
            }
            if (m_objTI == null)
            {
                m_objTI = new TIHeader();

                m_objTI.TNumber = Common.INT_DBNULL.ToString();
                m_objTI.CreatedBy = m_userId;
                m_objTI.IndexSeqNo = Common.INT_DBNULL;
            }

        }
        /// <summary>
        /// Save TO and Items
        /// </summary>
        /// <param name="statusId"></param>
        void Save(int statusId)
        {
            errItem.SetError(txtBatchNo, string.Empty);
            EmptyErrorProvider();

            ValidateOnSave(statusId);

            #region Check Errors
            StringBuilder sbError = new StringBuilder();
            sbError = GenerateError();
            #endregion

            if (!sbError.ToString().Trim().Equals(string.Empty))
            {
                MessageBox.Show(sbError.ToString(), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (m_lstTIDetail == null || m_lstTIDetail.Count == 0)
            {
                MessageBox.Show(Common.GetMessage("VAL0024", "item"),Common.GetMessage("10001"),MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }

            #region "Compare Quantity"
            string itemlist = string.Empty;
            // Check TotalQty + Qty In TextBox <=Total Item Qty
            //var lstEditedItemGroupBy = from t in m_lstTIDetail
            //                           group t by t.ItemCode into g
            //                           select new
            //                           {
            //                               ItemID = g.First<TIDetail>().ItemCode,
            //                               RequestQty = g.First<TIDetail>().RequestQty,
            //                               ItemCount = g.Count(),
            //                               TotalAvailableQty = g.Sum(p => p.AvailableQty),
            //                               TotalAdjustableQty = g.Sum(p => p.AfterAdjustQty)
            //                           };
            var lstEditedItemGroupBy = m_lstTIDetail.GroupBy(x => new { x.ItemId, x.BucketId })
                                    .Select(g => new
                                    {
                                        ItemID = g.First<TIDetail>().ItemCode.ToLower(),
                                        RequestQty = g.First<TIDetail>().RequestQty,
                                        BucketId = g.First<TIDetail>().BucketId,
                                        TotalAvailableQty = g.Sum(p => p.AvailableQty),
                                        TotalAdjustableQty = g.Sum(p => p.AfterAdjustQty)
                                    });


            // No of Item in Grid Should match with added Item
            List<TIDetail> tiItemDetail = new List<TIDetail>();
            TIHeader objTI = new TIHeader();
            string errMessage = string.Empty;


            if (m_objTI.StatusId < (int)Common.TIStatus.Created)
                tiItemDetail = m_objTI.SearchItem(txtTONumber.Text.ToString(), m_objTI.TNumber, m_sourceLocationId);
            else
                tiItemDetail = m_objTI.SearchItem(string.Empty, m_objTI.TNumber, m_sourceLocationId);


            for (int i = 0; i < lstEditedItemGroupBy.ToList().Count; i++)
            {
                if (lstEditedItemGroupBy.ToList()[i].RequestQty != lstEditedItemGroupBy.ToList()[i].TotalAdjustableQty)
                    itemlist = itemlist + lstEditedItemGroupBy.ToList()[i].ItemID + ",";
            }

            if (itemlist != string.Empty)
            {
                MessageBox.Show(Common.GetMessage("VAL0025", itemlist.Substring(0, itemlist.Length - 1)), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            #endregion

            #region If any Item code is deleted or added, which was not in TO Number
            if (tiItemDetail != null)
            {
                var lstItemGroupByBeforeSearch = tiItemDetail.GroupBy(x => new { x.ItemId, x.BucketId })
                                                .Select(g => new
                                                {
                                                    ItemID = g.First<TIDetail>().ItemCode,
                                                    RequestQty = g.First<TIDetail>().RequestQty,
                                                    BucketId = g.First<TIDetail>().BucketId,
                                                    TotalAvailableQty = g.Sum(p => p.AvailableQty),
                                                    TotalAdjustableQty = g.Sum(p => p.AfterAdjustQty)
                                                });
                //var lstItemGroupByBeforeSearch = from t in tiItemDetail
                //                                 group t by t.ItemCode into g
                //                                 select new
                //                                 {
                //                                     ItemID = g.First<TIDetail>().ItemCode,
                //                                     RequestQty = g.First<TIDetail>().RequestQty,
                //                                     ItemCount = g.Count(),
                //                                     TotalAvailableQty = g.Sum(p => p.AvailableQty),
                //                                     TotalAdjustableQty = g.Sum(p => p.AfterAdjustQty)
                //                                 };

                for (int i = 0; i < lstItemGroupByBeforeSearch.ToList().Count; i++)
                {
                    //int query = (from p in lstEditedItemGroupBy.ToList() where p.ItemID.ToLower().Trim() == lstItemGroupByBeforeSearch.ToList()[i].ItemID.ToLower().Trim() select p).Count();
                    int query = (from p in lstEditedItemGroupBy.ToList() where p.ItemID.ToLower().Trim() == lstItemGroupByBeforeSearch.ToList()[i].ItemID.ToLower().Trim() && p.BucketId == lstItemGroupByBeforeSearch.ToList()[i].BucketId select p).Count();

                    if (query == 0)
                        itemlist = itemlist + lstItemGroupByBeforeSearch.ToList()[i].ItemID + ",";
                }

                if (itemlist != string.Empty)
                {
                    MessageBox.Show(Common.GetMessage("VAL0025", itemlist.Substring(0, itemlist.Length - 1)), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
            #endregion

            // Confirmation Before Saving
            //DialogResult saveResult = MessageBox.Show(Common.GetMessage("INF0027"), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            MemberInfo[] memberInfos = typeof(Common.TIStatus).GetMembers(BindingFlags.Public | BindingFlags.Static);
            // Confirmation Before Saving, statusId
            DialogResult saveResult = MessageBox.Show(Common.GetMessage("5010", Common.GetConfirmationStatusText(memberInfos[statusId].Name)), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (saveResult == DialogResult.Yes)
            {

                CreateTIObject();
                if (m_objTI != null)
                {
                    if (m_objTI.ModifiedDate.Length > 0)
                        m_objTI.ModifiedDate = Convert.ToDateTime(m_objTI.ModifiedDate).ToString(Common.DATE_TIME_FORMAT);//.ToString(Common.DATE_TIME_FORMAT);
                }

                m_objTI.TONumber = txtTONumber.Text.Trim();
                // m_objTI = System.DateTime.Now.ToString(Common.DATE_TIME_FORMAT).ToString();
                m_objTI.ShippingBillNo = txtShippingBillNo.Text.Trim();
                m_objTI.ShippingDetails = txtShippingDetails.Text.Trim();
                m_objTI.GrossWeight = Convert.ToDecimal(txtGrossWeight.Text.Trim().Length == 0 ? grossWeight().ToString() : txtGrossWeight.Text);

                m_objTI.SourceLocationId = m_sourceLocationId; //m_currentLocationId;
                m_objTI.SourceAddress = txtSourceAddress.Text;
                m_objTI.DestinationAddress = txtDestinationAddress.Text;
                m_objTI.DestinationLocationId = m_currentLocationId;

                //m_objTI.TotalTOQuantity = Convert.ToDecimal(txtTotalQty.Text);
                //m_objTI.TotalTOAmount = Convert.ToDecimal(txtTotalTOAmount.Text);
                m_objTI.PackSize = Convert.ToInt32(txtPackSize.Text);
                m_objTI.Remarks = txtRemarks.Text;

                DateTime receivedDate = dtpReceivedDate.Checked == true ? Convert.ToDateTime(dtpReceivedDate.Value) : Common.DATETIME_NULL;
                DateTime receivedTime = dtpReceivedTime.Checked == true ? Convert.ToDateTime(dtpReceivedTime.Value) : Common.DATETIME_NULL;

                m_objTI.ReceivedDate = Convert.ToDateTime(receivedDate).ToString(Common.DATE_TIME_FORMAT);
                m_objTI.ReceivedTime = Convert.ToDateTime(receivedTime).ToString(Common.DATE_TIME_FORMAT);

                // Get Location Code From User Object.
                m_objTI.LocationId = m_currentLocationId;
                m_objTI.ModifiedBy = m_userId;

                m_objTI.StatusId = statusId;

                m_objTI.TIItems = m_lstTIDetail;

                string errorMessage = string.Empty;

                bool result = m_objTI.Save(Common.ToXml(m_objTI), ref errorMessage);

                if (errorMessage.Equals(string.Empty))
                {
                    ResetItemControl();

                    EnableDisableButton(GetTOIStatus(txtTONumber.Text));

                    dgvTIItem.DataSource = new List<TIDetail>();
                    if (m_lstTIDetail != null && m_lstTIDetail.Count > 0)
                    {
                        m_lstTIDetail = SearchTIItem(m_objTI.TNumber, m_objTI.SourceLocationId);
                        txtTINumber.Text = m_objTI.TNumber.ToString();
                        dgvTIItem.DataSource = m_lstTIDetail;
                        ResetItemControl();
                    }

                    //MessageBox.Show(Common.GetMessage("8001"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MessageBox.Show(Common.GetMessage("8013", memberInfos[statusId].Name, m_objTI.TNumber), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show(Common.GetMessage(errorMessage),Common.GetMessage("10001"),MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        void SelectSearchGrid(string tNumber, int sourceLocationId)
        {
            chkIsexported.Enabled = false;
            m_lstTIDetail = SearchTIItem(tNumber, sourceLocationId);

            //dgvTIItem.SelectionChanged -= new System.EventHandler(dgvTIItem_SelectionChanged);
            dgvTIItem.DataSource = new List<TIDetail>();
            if (m_lstTIDetail != null && m_lstTIDetail.Count > 0)
            {
                dgvTIItem.DataSource = m_lstTIDetail;
                ResetItemControl();
            }

            tabControlTransaction.TabPages[1].Text = Common.TAB_UPDATE_MODE;
            tabControlTransaction.SelectedIndex = 1;
            m_tiNo = tNumber;

            txtTONumber.Text = m_objTI.TONumber;
            EnableDisableButton(m_objTI.StatusId);

            //dgvTIItem.SelectionChanged += new System.EventHandler(dgvTIItem_SelectionChanged);
            dgvTIItem.ClearSelection();

            txtPackSize.Text = m_objTI.PackSize.ToString();
            txtShippingBillNo.Text = m_objTI.ShippingBillNo;
            txtGrossWeight.Text = Math.Round(m_objTI.GrossWeight, 2).ToString();
            txtShippingDetails.Text = m_objTI.ShippingDetails;
            txtRemarks.Text = m_objTI.Remarks;
            txtStatus.Text = m_objTI.StatusName;
            if (m_objTI.ReceivedDate.Length > 0)
            {
                dtpReceivedDate.Checked = true;
                dtpReceivedDate.Value = Convert.ToDateTime(m_objTI.ReceivedDate);
            }
            else
            { dtpReceivedDate.Checked = false; }


            if (m_objTI.ReceivedTime.Length > 0)
            {
                dtpReceivedTime.Checked = true;
                dtpReceivedTime.Value = Convert.ToDateTime(m_objTI.ReceivedTime.Substring(0, 8));
            }
            else
            { dtpReceivedTime.Checked = false; }

            DataTable dtSource = Common.ParameterLookup(Common.ParameterType.Locations, new ParameterFilter("LOCATIONS", 0, 0, 0));
            DataTable dt = dtSource.Clone();
            DataRow[] drSource = dtSource.Select("LocationId = " + m_objTI.SourceLocationId);
            DataRow[] drDest = dtSource.Select("LocationId = " + m_objTI.DestinationLocationId);
            chkIsexported.Checked = (m_objTI.Isexport ==1? true : false );
            //chkIsexported.Enabled = (m_objTI.Isexport == 0? false : true);
            
            m_sourceLocationId = m_objTI.SourceLocationId;

            txtSourceLocation.Text = drSource[0]["DisplayName"].ToString();
            txtDestinationLocation.Text = drDest[0]["DisplayName"].ToString();

            txtSourceAddress.Text = m_objTI.SourceAddress;
            txtDestinationAddress.Text = m_objTI.DestinationAddress;

            ShowControlTaxInfor(m_objTI.SourceLocationId, m_objTI.DestinationLocationId);

            txtTotalQty.Text = m_objTI.DisplayTotalTOQuantity.ToString();
            txtTotalTOAmount.Text = m_objTI.DisplayTotalTOAmount.ToString();
            //txtTotalQty.Text = Math.Round(Convert.ToDecimal(m_objTI.TotalTOQuantity), 2).ToString();
            //txtTotalTOAmount.Text = Math.Round(Convert.ToDecimal(m_objTI.TotalTOAmount), 2).ToString();
            txtTINumber.Text = m_objTI.TNumber.ToString();
            txtTONumber.Enabled = false;
            EmptyErrorProvider();
        }
        /// <summary>
        /// Edit TOI
        /// </summary>
        /// <param name="e"></param> 
        private void EditTO(DataGridViewCellEventArgs e)
        {
            if ((e.RowIndex >= 0) && (dgvSearchTI.Rows[e.RowIndex].Cells[e.ColumnIndex].GetType() == typeof(DataGridViewImageCell)))
            {
                m_objTI = m_lstTIHeader[e.RowIndex];

                SelectSearchGrid(m_objTI.TNumber, m_objTI.SourceLocationId);
            }
        }
        /// <summary>
        /// Make read Only fields
        /// </summary>
        void ReadOnlyTOField()
        {
            if ((m_lstTIDetail != null && m_lstTIDetail.Count > 0))
            {
                txtTONumber.Enabled = false;
            }
            else if (m_lstTIDetail.Count == 0)
            {
                txtTONumber.Enabled = true;
            }
        }
        /// <summary>
        /// Return List of Item Details
        /// </summary>
        /// <param name="TNumber"></param>
        /// <param name="sourceAddressId"></param>
        /// <returns></returns>
        List<TIDetail> SearchTIItem(string TNumber, int sourceAddressId)
        {
            m_lstTIDetail = m_objTI.SearchItem(string.Empty, TNumber, sourceAddressId);
            return m_lstTIDetail;
        }
        /// <summary>
        /// Clear Batch Info
        /// </summary>
        void ClearBatchInfor()
        {
            txtBatchNo.Text = string.Empty;
            //txtRequestedQty.Text = string.Empty;
            //m_bucketid = Common.INT_DBNULL;
            //m_UOMId = Common.INT_DBNULL;
            //m_weight = 0;
            //m_UOMName = string.Empty;
            m_batchNo = string.Empty;
            txtItemTotalAmount.Text = string.Empty;
        }
        /*
        /// <summary>
        /// Calculate Tranfer Price when Item Code is Entered
        /// </summary>
        void CalculateTransferPrice()
        {
            TOI objTOI = new TOI();
            decimal price = objTOI.CalTransferPrice(txtItemCode.Text, m_currentLocationId.ToString());

            if (m_tiDetail == null)
         * m_tiDetail = new TIDetail();

            m_tiDetail.ItemUnitPrice = price;

            txtTransferPrice.Text =m_tiDetail.DisplayItemUnitPrice.ToString();
        }
         * 
         * */
        /// <summary>
        /// Fill Item Price Information into UOM, name of Item
        /// </summary>
        /// <param name="itemCode"></param>
        void FillItemPriceInfo(string itemCode)
        {
            ItemDetails itemDetails = new ItemDetails();
            itemDetails.LocationId = m_sourceLocationId.ToString();

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
                    m_UOMId = Convert.ToInt32(dv[0]["UOMId"]);
                    m_weight = Convert.ToDecimal(dv[0]["Weight"]);
                    m_UOMName = dv[0]["UOMName"].ToString();
                    txtWeight.Text = Math.Round(m_weight, 2).ToString();
                    txtUOMName.Text = dv[0]["UOMName"].ToString();
                    txtItemDescription.Text = dv[0]["ItemName"].ToString();
                }
            }
            else
            {
                errItem.SetError(txtItemCode, Common.GetMessage("VAL0006", lblItemCode.Text.Trim().Substring(0, lblItemCode.Text.Trim().Length - 2)));
                m_UOMName = string.Empty;
                m_UOMId = Common.INT_DBNULL;
                m_itemId = Common.INT_DBNULL;
                m_weight = 0;
            }
        }
        /// <summary>
        /// Validate Text box fields
        /// </summary>
        /// <param name="txt"></param>
        /// <param name="lbl"></param>
        void ValidatedText(TextBox txt, Label lbl)
        {
            bool isTextBoxEmpty = CoreComponent.Core.BusinessObjects.Validators.CheckForEmptyString(txt.Text.Trim().Length);
            if (isTextBoxEmpty == true)
                errItem.SetError(txt, Common.GetMessage("INF0019", lbl.Text.Trim().Substring(0, lbl.Text.Trim().Length - 2)));
            else if (isTextBoxEmpty == false)
                errItem.SetError(txt, string.Empty);
        }
        /// <summary>
        /// Validate Batch No.
        /// </summary>
        void ValidateBatchNo(Boolean yesNo)
        {
            bool isTextBoxEmpty = CoreComponent.Core.BusinessObjects.Validators.CheckForEmptyString(txtBatchNo.Text.Trim().Length);
            if (isTextBoxEmpty == true)
                errItem.SetError(txtBatchNo, Common.GetMessage("INF0019", lblBatchNo.Text.Trim().Substring(0, lblBatchNo.Text.Trim().Length - 2)));
            else if (isTextBoxEmpty == false)
            {
                if (yesNo)
                    errItem.SetError(txtBatchNo, string.Empty);

                if ((m_F4Press == false && yesNo == true) || (m_mfgbatchNo.Trim().ToLower()!=txtBatchNo.Text.Trim().ToLower()))
                {
                    ItemBatchDetails objItem = new ItemBatchDetails();
                    objItem.ManufactureBatchNo = txtBatchNo.Text.Trim();
                    objItem.ItemCode = txtItemCode.Text.Trim();
                    objItem.LocationId = Common.INT_DBNULL.ToString();
                    objItem.BucketId = m_bucketid.ToString();
                    objItem.FromMfgDate = Common.DATETIME_NULL;
                    objItem.ToMfgDate = Common.DATETIME_MAX;

                    List<ItemBatchDetails> lstItemDetails = objItem.Search();
                    if (txtItemCode.Text.Length > 0 && lstItemDetails != null)
                    {
                        var query = from p in lstItemDetails where p.ItemId == m_itemId select p;
                        lstItemDetails = query.ToList();

                        if (lstItemDetails.Count == 1)
                        {
                            objItem = lstItemDetails[0];
                            GetBatchInfo(objItem);
                        }
                        else if (lstItemDetails.Count > 1)
                        {
                            errItem.SetError(txtBatchNo, Common.GetMessage("VAL0038", lblBatchNo.Text.Trim().Substring(0, lblBatchNo.Text.Trim().Length - 2)));
                        }
                        else
                        {
                            errItem.SetError(txtBatchNo, Common.GetMessage("VAL0006", lblBatchNo.Text.Trim().Substring(0, lblBatchNo.Text.Trim().Length - 2)));
                        }
                    }
                    else
                    {
                        errItem.SetError(txtBatchNo, Common.GetMessage("VAL0006", lblBatchNo.Text.Trim().Substring(0, lblBatchNo.Text.Trim().Length - 2)));
                        //MessageBox.Show(Common.GetMessage("VAL0006", "batch code"));
                    }
                }
            }
        }
        /// <summary>
        /// Validate Item Code
        /// </summary>
        /// <param name="yesNo"></param>
        void ValidateItemCode(Boolean yesNo)
        {
            bool isTextBoxEmpty = CoreComponent.Core.BusinessObjects.Validators.CheckForEmptyString(txtItemCode.Text.Trim().Length);
            if (isTextBoxEmpty == true)
                errItem.SetError(txtItemCode, Common.GetMessage("INF0019", lblItemCode.Text.Trim().Substring(0, lblItemCode.Text.Trim().Length - 2)));
            else if (isTextBoxEmpty == false)
            {
                errItem.SetError(txtItemCode, string.Empty);
                if (yesNo)
                {
                    errItem.SetError(txtItemCode, string.Empty);
                    ClearBatchInfor();
                    //CalculateTransferPrice();
                    if (m_lstTIFullDetail != null)
                    {
                        var query = (from p in m_lstTIFullDetail where p.ItemCode == txtItemCode.Text.Trim() select p);
                        if (query.ToList().Count > 0)
                        {
                            if (m_tiDetail == null)
                                m_tiDetail = new TIDetail();
                            m_tiDetail.ItemUnitPrice = query.ToList()[0].ItemUnitPrice;
                            txtTransferPrice.Text = query.ToList()[0].DisplayItemUnitPrice.ToString();
                        }
                        else
                            txtTransferPrice.Text = string.Empty;
                    }
                }
                FillItemPriceInfo(txtItemCode.Text.Trim());
                GetRequestedQuantity(txtItemCode.Text.Trim());

                txtItemTotalAmount.Text = Math.Round(Convert.ToDecimal(txtTransferPrice.Text.Length == 0 ? "0" : txtTransferPrice.Text) * Convert.ToDecimal(txtAdjustableQty.Text.Length == 0 ? "0" : txtAdjustableQty.Text), 2).ToString();
            }
        }
        /// <summary>
        /// Get Batch Information
        /// </summary>
        /// <param name="objItem"></param>
        void GetBatchInfo(ItemBatchDetails objItem)
        {
            m_MRP = objItem.MRP;
            m_mfgbatchNo = objItem.ManufactureBatchNo.ToString();
            txtBatchNo.Text = objItem.ManufactureBatchNo.ToString();
            m_batchNo = objItem.BatchNo.ToString();
            m_mfgDate = objItem.MfgDate;
            m_expDate = objItem.ExpDate;
            if (m_tiDetail != null)
            {
                m_tiDetail.AvailableQty = objItem.Quantity;
                txtAvailableQty.Text = m_tiDetail.DisplayAvailableQty.ToString();
            }
        }
        /// <summary>
        /// Validate Pack Size
        /// </summary>
        void ValidatePackSize()
        {
            bool isValidAmount = CoreComponent.Core.BusinessObjects.Validators.IsValidQuantity(txtPackSize.Text);

            if (isValidAmount == false)
                errItem.SetError(txtPackSize, Common.GetMessage("VAL0009", lblPackSize.Text.Trim().Substring(0, lblPackSize.Text.Trim().Length - 2)));
            else
                errItem.SetError(txtPackSize, string.Empty);
        }
        /// <summary>
        /// Open new window form when F4 key is pressed
        /// </summary>
        /// <param name="e"></param>
        void BatchNoKeyDown(KeyEventArgs e)
        {
            NameValueCollection nvc = new NameValueCollection();
            nvc.Add("ItemCode", txtItemCode.Text.Trim());
            nvc.Add("BucketId", m_bucketid.ToString());

            CoreComponent.Controls.frmSearch objfrmSearch = new CoreComponent.Controls.frmSearch(CoreComponent.Controls.SearchTypes.ItemBatchMaster, nvc);
            objfrmSearch.ShowDialog();
            CoreComponent.MasterData.BusinessObjects.ItemBatchDetails objItem = (CoreComponent.MasterData.BusinessObjects.ItemBatchDetails)objfrmSearch.ReturnObject;
            if (objItem != null)
            {
                m_F4Press = true;
                GetBatchInfo(objItem);
            }
        }
        /// <summary>
        /// Find Item Requested Quantity 
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="batchNo"></param>
        /// <param name="bucketId"></param>
        void GetRequestedQuantity(string itemCode)
        {
            List<TIDetail> lstTIDetail = new List<TIDetail>();
            TIDetail objTIDetail = new TIDetail();
            TIHeader objTI = new TIHeader();
            string errMessage = string.Empty;

            if (txtTONumber.Text.Trim().Length == 0 || m_objTI == null)
            {
                MessageBox.Show(Common.GetMessage("VAL0006", "TO Number"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            // if (m_objTI.StatusId < (int)Common.TIStatus.Created)
            lstTIDetail = m_objTI.SearchItem(txtTONumber.Text.ToString(), m_objTI.TNumber, m_sourceLocationId);
            //else
            //    lstTIDetail = m_objTI.SearchItem(string.Empty, m_objTI.TNumber, m_currentLocationId);

            if (lstTIDetail != null)
            {
                if (txtTONumber.Text.Trim().Length > 0)
                {

                    if (m_bucketid != Common.INT_DBNULL)
                    {
                        var query = from p in lstTIDetail where p.ItemCode.ToLower().Trim() == itemCode.ToLower().Trim() && p.BucketId == m_bucketid select p; //&& p.BucketId = 
                        if (query.ToList().Count > 0)
                            objTIDetail = query.ToList()[0];
                        else
                            objTIDetail = null;
                    }
                    else
                    {
                        var query1 = from p in lstTIDetail where p.ItemCode.ToLower().Trim() == itemCode.ToLower().Trim() select p; //&& p.BucketId = 
                        if (query1.ToList().Count > 0)
                            objTIDetail = query1.ToList()[0];
                        else
                            objTIDetail = null;
                    }

                    if (objTIDetail == null)
                    {
                        txtRequestedQty.Text = string.Empty;
                        m_bucketid = Common.INT_DBNULL;
                        errItem.SetError(txtItemCode, Common.GetMessage("VAL0040", lblItemCode.Text.Trim().Substring(0, lblItemCode.Text.Trim().Length - 2)));
                    }
                    else
                    {
                        decimal qty = Convert.ToDecimal(objTIDetail.RequestQty);
                        if (m_tiDetail == null)
                            m_tiDetail = new TIDetail();

                        m_tiDetail.RequestQty = qty;

                        

                        txtRequestedQty.Text = m_tiDetail.DisplayRequestQty.ToString();
                        //txtIndentNo.Text = objTIDetail.IndentNo.ToString();
                        //m_IndentNo = objTIDetail.IndentNo.ToString();
                        m_bucketid = objTIDetail.BucketId;
                        txtBucketName.Text = objTIDetail.BucketName;
                    }
                }

                else
                {
                    txtRequestedQty.Text = string.Empty;
                    m_bucketid = Common.INT_DBNULL;
                    //errItem.SetError(txtItemCode, Common.GetMessage("INF0019", lblItemCode.Text.Trim().Substring(0, lblItemCode.Text.Trim().Length - 2)));
                    //MessageBox.Show(Common.GetMessage("INF0026", "TO Number"));
                }
            }
        }


        /// <summary>
        /// Validate Adjustable Quantity
        /// </summary>
        void ValidateQuantity(Boolean yesNo)
        {
            bool isTextBoxEmpty = CoreComponent.Core.BusinessObjects.Validators.CheckForEmptyString(txtAdjustableQty.Text.Trim().Length);
            if (isTextBoxEmpty == true)
            {
                if (yesNo == false)
                    errItem.SetError(txtAdjustableQty, Common.GetMessage("INF0019", lblUnitQty.Text.Trim().Substring(0, lblUnitQty.Text.Trim().Length - 2)));
                else
                    errItem.SetError(txtAdjustableQty, string.Empty);
            }
            else if (isTextBoxEmpty == false)
            {
                bool isValidQuantity = CoreComponent.Core.BusinessObjects.Validators.IsValidQuantity(txtAdjustableQty.Text);

                if (isValidQuantity == false)
                    errItem.SetError(txtAdjustableQty, Common.GetMessage("VAL0009", lblUnitQty.Text.Trim().Substring(0, lblUnitQty.Text.Trim().Length - 2)));
                else
                {
                    errItem.SetError(txtAdjustableQty, string.Empty);

                    if (txtTransferPrice.Text.Trim().Length > 0)
                    {
                        m_tiDetail.AfterAdjustQty = Convert.ToDecimal(txtAdjustableQty.Text);
                        m_tiDetail.ItemTotalAmount = (m_tiDetail.AfterAdjustQty * m_tiDetail.ItemUnitPrice);
                        txtItemTotalAmount.Text = m_tiDetail.DisplayItemTotalAmount.ToString();//Math.Round(Convert.ToDecimal(txtAdjustableQty.Text) * Convert.ToDecimal(txtTransferPrice.Text), 2).ToString();
                    }
                    else
                    {
                        txtItemTotalAmount.Text = string.Empty;
                    }
                }
                //txtItemTotalAmount.Text = txt
            }
        }
        /// <summary>
        /// To Enable alll buttons
        /// </summary>
        void EnableAllButton(bool enable)
        {
            btnCreateReset.Enabled = enable;
            btnSave.Enabled = enable;
            btnItemReset.Enabled = enable;
            btnConfirmed.Enabled = enable;
            btnAddDetails.Enabled = enable;
        }
        /// <summary>
        /// To enable and disable button, when status is changed
        /// </summary>
        /// <param name="statusId"></param>
        void EnableDisableButton(int statusId)
        {
            if (m_currentLocationId == (int)Common.LocationConfigId.HO)
                EnableAllButton(false);
            else
            {
                EnableAllButton(true);
                if (statusId == (int)Common.TIStatus.New)
                {
                   // btnConfirmed.Enabled = false;
                }
                else if (statusId == (int)Common.TIStatus.Confirmed)
                {
                    btnConfirmed.Enabled = false;
                    btnSave.Enabled = false;
                    btnItemReset.Enabled = false;
                    btnAddDetails.Enabled = false;
                }

                btnConfirmed.Enabled = btnConfirmed.Enabled & m_isConfirmAvailable;
                btnSearch.Enabled = m_isSearchAvailable;
                btnSave.Enabled = btnSave.Enabled & m_isSaveAvailable;
                btnPrint.Enabled = m_isPrintAvailable;
            }
        }
        /// <summary>
        /// Return TOI Status 
        /// </summary>
        /// <param name="TOINumber"></param>
        /// <returns></returns>
        int GetTOIStatus(string TONumber)
        {
            List<TIHeader> tiHeader = new List<TIHeader>();
            //tiHeader = SearchTI(TONumber);
            tiHeader = Search(TONumber, string.Empty, Common.INT_DBNULL, m_currentLocationId, Common.DATETIME_NULL.ToString(Common.DATE_TIME_FORMAT), Common.DATETIME_NULL.ToString(Common.DATE_TIME_FORMAT), Common.DATETIME_NULL.ToString(Common.DATE_TIME_FORMAT), Common.DATETIME_NULL.ToString(Common.DATE_TIME_FORMAT), Common.INT_DBNULL, Common.INT_DBNULL, Common.INT_DBNULL);

            if (tiHeader != null && tiHeader.Count > 0)
            {
                txtStatus.Text = tiHeader[0].StatusName;
                return tiHeader[0].StatusId;
            }
            else
            {
                txtStatus.Text = Common.TIStatus.New.ToString();
                return (int)Common.TIStatus.New;
            }
        }
        //TIHeader ConvertTIHeaderObj(TOHeader toHeader)
        //{
        //    TIHeader tiHeader = new TIHeader();
        //    tiHeader.StatusId = toHeader.StatusId;
        //    tiHeader.StatusName = toHeader.StatusName;

        //    return tiHeader;


        //}
        /// <summary>
        /// Validate TOI Number
        /// </summary>
        /// <param name="yesNo"></param>
        void ValidateTONumber(Boolean yesNo)
        {
            bool isTextBoxEmpty = CoreComponent.Core.BusinessObjects.Validators.CheckForEmptyString(txtTONumber.Text.Trim().Length);
            if (isTextBoxEmpty == true)
                errItem.SetError(txtTONumber, Common.GetMessage("INF0019", lblTONumber.Text.Trim().Substring(0, lblTONumber.Text.Trim().Length - 2)));
            else
            {
                #region If TO Is Entered
                DataTable dtSearchSource = Common.ParameterLookup(Common.ParameterType.Locations, new ParameterFilter("LOCATIONS", 0, 0, 0));
                DateTime DATETIME_MAX = Convert.ToDateTime(new DateTime(2099, 12, 31).ToString(Common.DATE_TIME_FORMAT));
                List<TOHeader> lstTO = new List<TOHeader>();
                TOHeader objTO = new TOHeader();
                objTO.TNumber = txtTONumber.Text.Trim();
                objTO.DestinationLocationId = m_currentLocationId;
                objTO.SourceLocationId = Common.INT_DBNULL;
                objTO.FromTODate = Common.DATETIME_NULL;
                objTO.ToTODate = DATETIME_MAX;
                objTO.FromShipDate = Common.DATETIME_NULL;
                objTO.ToShipDate = DATETIME_MAX;
                objTO.StatusId = Common.INT_DBNULL;
                objTO.Indentised = Common.INT_DBNULL;
                //string toiNumber = txtTOINumber.Text;
                //ResetTOAndItems();
                //txtTOINumber.Text = toiNumber;
                lstTO = objTO.Search();
                if (lstTO != null && lstTO.Count > 0)
                {
                    objTO = lstTO[0];
                    txtTONumber.Text = objTO.TNumber.ToString().Trim();

                    if (objTO.StatusId == (int)Common.TOStatus.Closed)
                    {
                        errItem.SetError(txtTONumber, Common.GetMessage("VAL0042", objTO.StatusName, txtTONumber.Text.Trim().ToUpper()));
                        return;
                    }

                    if (objTO.StatusId != (int)Common.TOStatus.Shipped)
                    {
                        errItem.SetError(txtTONumber, Common.GetMessage("VAL0039", "TI", objTO.StatusName));
                        return;
                    }

                    txtPackSize.Text = objTO.PackSize.ToString();
                    txtShippingBillNo.Text = objTO.ShippingBillNo.ToString();
                    txtShippingDetails.Text = objTO.ShippingDetails.ToString();
                    txtGrossWeight.Text = objTO.GrossWeight.ToString();
                    //objTO.Isexport = (chkIsexported.Checked == true || objTO.Isexported==false ? 1 : 0);
                   

                    if (yesNo)
                    {
                        int tiStatus = Common.INT_DBNULL;
                        List<TIHeader> tiHeader = new List<TIHeader>();

                        tiHeader = Search(objTO.TNumber, string.Empty, objTO.SourceLocationId, m_currentLocationId, Common.DATETIME_NULL.ToString(Common.DATE_TIME_FORMAT), Common.DATETIME_NULL.ToString(Common.DATE_TIME_FORMAT), Common.DATETIME_NULL.ToString(Common.DATE_TIME_FORMAT), Common.DATETIME_NULL.ToString(Common.DATE_TIME_FORMAT), Common.INT_DBNULL, Common.INT_DBNULL, Common.INT_DBNULL);

                        //tiHeader = SearchTI(objTO.TNumber);
                        if (tiHeader != null && tiHeader.Count > 0)
                        {
                            tiStatus = tiHeader[0].StatusId;
                            if (tiStatus == (int)Common.TIStatus.Confirmed || tiStatus == (int)Common.TIStatus.Created)
                            {
                                errItem.SetError(txtTONumber, Common.GetMessage("VAL0042", tiHeader[0].StatusName, txtTONumber.Text.Trim().ToUpper()));
                                // MessageBox.Show(Common.GetMessage("VAL0042", tiHeader[0].StatusName), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                        }
                    }

                    DataTable dtSource = Common.ParameterLookup(Common.ParameterType.Locations, new ParameterFilter("LOCATIONS", 0, 0, 0));
                    DataTable dt = dtSource.Clone();
                    DataRow[] drSource = dtSource.Select("LocationId = " + lstTO[0].SourceLocationId);
                    DataRow[] drDest = dtSource.Select("LocationId = " + lstTO[0].DestinationLocationId);

                    txtSourceLocation.Text = drSource[0]["DisplayName"].ToString();
                    txtDestinationLocation.Text = drDest[0]["DisplayName"].ToString();
                    m_sourceLocationId = lstTO[0].SourceLocationId;

                    txtSourceAddress.Text = lstTO[0].SourceAddress;
                    txtDestinationAddress.Text = lstTO[0].DestinationAddress;

                    txtTotalQty.Text = lstTO[0].DisplayTotalTOQuantity.ToString();
                    txtGrossWeight.Text = lstTO[0].GrossWeight.ToString();
                    txtTotalTOAmount.Text = lstTO[0].DisplayTotalTOAmount.ToString();
                    chkIsexported.Checked = lstTO[0].Isexported;


                    ShowControlTaxInfor(lstTO[0].SourceLocationId, lstTO[0].DestinationLocationId);

                    if (yesNo)
                    {
                        EnableDisableButton((int)Common.TIStatus.New);

                        string errMessage = string.Empty;
                        TIHeader objTI = new TIHeader();

                        m_lstTIDetail = objTI.SearchItem(objTO.TNumber, string.Empty, objTO.SourceLocationId);
                        
                        if (m_lstTIDetail == null || m_lstTIDetail.Count == 0)
                        {
                            MessageBox.Show(Common.GetMessage("VAL0028"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        //make a local copy of list for fetching item unit price
                        m_lstTIFullDetail = new List<TIDetail>();
                        for (int i = 0; i < m_lstTIDetail.Count; i++)
                            m_lstTIFullDetail.Add((TIDetail)m_lstTIDetail[i]);

                        m_bucketid = Common.INT_DBNULL;

                        if (m_lstTIDetail.Count > 0)
                            txtTONumber.Enabled = false;

                        dgvTIItem.DataSource = m_lstTIDetail;
                        ResetItemControl();

                        //bool b = CheckValidQuantity(Common.INT_DBNULL);
                        m_objTI = new TIHeader(); //ConvertTIHeaderObj(objTO);
                        m_objTI.StatusId = (int)Common.TIStatus.New;
                        m_objTI.TotalTIQuantity = Convert.ToDecimal(txtTotalQty.Text);
                        m_objTI.TotalTIAmount = Convert.ToDecimal(txtTotalTOAmount.Text);
                        m_objTI.TotalTOAmount = Convert.ToDecimal(txtTotalTOAmount.Text);
                        m_objTI.TotalTOQuantity = Convert.ToDecimal(txtTotalQty.Text);
                    }
                    errItem.SetError(txtTONumber, string.Empty);

                }
                else
                {
                    string toNumber = txtTONumber.Text;
                    ResetTOAndItems();
                    txtTONumber.Text = toNumber;
                    errItem.SetError(txtTONumber, Common.GetMessage("INF0010", lblTONumber.Text.Trim().Substring(0, lblTONumber.Text.Trim().Length - 2)));
                }
                #endregion
            }

        }
        /// <summary>
        /// Return gross weight
        /// </summary>
        /// <returns></returns>
        decimal grossWeight()
        {

            var lstEditedItemGroupBy = from t in m_lstTIDetail
                                       group t by t.ItemCode into g
                                       select new
                                       {
                                           ItemID = g.First<TIDetail>().ItemCode,
                                           GrossWeight = g.First<TIDetail>().Weight,
                                       };


            var query = (from p in lstEditedItemGroupBy.ToList() select p.GrossWeight).Sum();

            return Convert.ToDecimal(query);
        }

        /// <summary>
        /// Validate Received Time
        /// </summary>
        void ValidateReceivedTime()
        {
            //if (dtpReceivedTime.Checked == false)
            //    errItem.SetError(dtpReceivedTime, Common.GetMessage("VAL0002", lblReceivedTime.Text.Trim().Substring(0, lblReceivedTime.Text.Trim().Length - 2)));
            //else if (dtpReceivedTime.Checked == true)
            //    errItem.SetError(dtpReceivedTime, string.Empty);
        }

        /// <summary>
        /// Validate Received date
        /// </summary>
        void ValidateReceivedDate()
        {
            if (dtpReceivedDate.Checked == false)
                errItem.SetError(dtpReceivedDate, Common.GetMessage("VAL0002", lblReceivedDate.Text.Trim().Substring(0, lblReceivedDate.Text.Trim().Length - 2)));
            else if (dtpReceivedDate.Checked == true)
            {
                DateTime expectedDate = dtpReceivedDate.Checked == true ? Convert.ToDateTime(dtpReceivedDate.Value) : Common.DATETIME_NULL;
                DateTime dt = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                TimeSpan ts = expectedDate - dt;
                if (ts.Days < 0)
                    errItem.SetError(dtpReceivedDate, Common.GetMessage("INF0010", lblReceivedDate.Text.Trim().Substring(0, lblReceivedDate.Text.Trim().Length - 2)));
                else
                    errItem.SetError(dtpReceivedDate, string.Empty);
            }
        }
        void EmptyErrorProvider()
        {
            errItem.SetError(txtTONumber, string.Empty);
            errItem.SetError(txtPackSize, string.Empty);
            errItem.SetError(txtRemarks, string.Empty);
            errItem.SetError(dtpReceivedDate, string.Empty);
            errItem.SetError(dtpReceivedTime, string.Empty);
            errItem.SetError(txtShippingBillNo, string.Empty);
            errItem.SetError(txtShippingDetails, string.Empty);

            errItem.SetError(txtAdjustableQty, string.Empty);
            errItem.SetError(txtItemCode, string.Empty);
            //errItem.SetError(txtBatchNo, string.Empty);
        }

        void ShowTONumber(TextBox txt)
        {
            NameValueCollection nvc = new NameValueCollection();
            if (m_locationType == (int)Common.LocationConfigId.BO || m_locationType == (int)Common.LocationConfigId.WH)
                nvc.Add("DestinationLocationId", m_currentLocationId.ToString());
            else
                nvc.Add("DestinationLocationId", Common.INT_DBNULL.ToString());

            //nvc.Add("DestinationLocationId", m_currentLocationId.ToString());
            nvc.Add("StatusId", ((int)Common.TOStatus.Shipped).ToString());
            nvc.Add("SourceLocationId", Common.INT_DBNULL.ToString());


            CoreComponent.Controls.frmSearch objfrmSearch = new CoreComponent.Controls.frmSearch(CoreComponent.Controls.SearchTypes.TO, nvc);
            TOHeader objTO = (TOHeader)objfrmSearch.ReturnObject;
            objfrmSearch.ShowDialog();
            objTO = (TOHeader)objfrmSearch.ReturnObject;

            if (objTO != null)
            {
                txt.Text = objTO.TNumber.ToString();
            }
        }
        /// <summary>
        /// Creates dataset of data to  be printed in TI Report 
        /// </summary>
        private void CreatePrintDataSet()
        {
            m_printDataSet = new DataSet();
            // Get Data For TOI Header Informaton in TOI Screen Report
            DataTable dtTIHeader = new DataTable("TIHeader");
            DataColumn TINumber = new DataColumn("TINumber", System.Type.GetType("System.String"));
            DataColumn TONumber = new DataColumn("TONumber", System.Type.GetType("System.String"));
            DataColumn SourceLocation = new DataColumn("SourceLocation", System.Type.GetType("System.String"));
            DataColumn DestinationLocation = new DataColumn("DestinationLocation", System.Type.GetType("System.String"));
            DataColumn PackSize = new DataColumn("PackSize", System.Type.GetType("System.String"));
            DataColumn SourceAddress = new DataColumn("SourceAddress", System.Type.GetType("System.String"));
            DataColumn DestinationAddress = new DataColumn("DestinationAddress", System.Type.GetType("System.String"));
            DataColumn Status = new DataColumn("Status", System.Type.GetType("System.String"));
            DataColumn SourceTINNo = new DataColumn("SourceTINNo", System.Type.GetType("System.String"));
            DataColumn SourceCSTNo = new DataColumn("SourceCSTNo", System.Type.GetType("System.String"));
            DataColumn SourceVATNo = new DataColumn("SourceVATNo", System.Type.GetType("System.String"));
            DataColumn DestinationTINNo = new DataColumn("DestinationTINNo", System.Type.GetType("System.String"));
            DataColumn DestinationCSTNo = new DataColumn("DestinationCStNo", System.Type.GetType("System.String"));
            DataColumn DestinationVATNo = new DataColumn("DestinationVATNo", System.Type.GetType("System.String"));
            DataColumn Indentised = new DataColumn("Indentised", System.Type.GetType("System.String"));
            DataColumn ReceivedDate = new DataColumn("ReceivedDate", System.Type.GetType("System.String"));
            DataColumn ReceivedTime = new DataColumn("ReceivedTime", System.Type.GetType("System.String"));
            DataColumn ShippingDetails = new DataColumn("ShippingDetails", System.Type.GetType("System.String"));
            DataColumn ShippingWayBillNo = new DataColumn("ShippingWayBillNo", System.Type.GetType("System.String"));
            DataColumn Remarks = new DataColumn("Remarks", System.Type.GetType("System.String"));
            DataColumn GrossWeight = new DataColumn("GrossWeight", System.Type.GetType("System.String"));
            DataColumn TotalTIQuantity = new DataColumn("TotalTIQuantity", System.Type.GetType("System.String"));
            DataColumn TotalTIAmount = new DataColumn("TotalTIAmount", System.Type.GetType("System.String"));
            DataColumn Isexported = new DataColumn("Isexported", System.Type.GetType("System.String"));
            dtTIHeader.Columns.Add(TINumber);
            dtTIHeader.Columns.Add(TONumber);
            dtTIHeader.Columns.Add(SourceLocation);
            dtTIHeader.Columns.Add(DestinationLocation);
            dtTIHeader.Columns.Add(PackSize);
            dtTIHeader.Columns.Add(SourceAddress);
            dtTIHeader.Columns.Add(DestinationAddress);
            dtTIHeader.Columns.Add(Status);
            dtTIHeader.Columns.Add(SourceTINNo);
            dtTIHeader.Columns.Add(SourceCSTNo);
            dtTIHeader.Columns.Add(SourceVATNo);
            dtTIHeader.Columns.Add(DestinationTINNo);
            dtTIHeader.Columns.Add(DestinationCSTNo);
            dtTIHeader.Columns.Add(DestinationVATNo);
            dtTIHeader.Columns.Add(Indentised);
            dtTIHeader.Columns.Add(ReceivedDate);
            dtTIHeader.Columns.Add(ReceivedTime);
            dtTIHeader.Columns.Add(ShippingDetails);
            dtTIHeader.Columns.Add(ShippingWayBillNo);
            dtTIHeader.Columns.Add(Remarks);
            dtTIHeader.Columns.Add(TotalTIQuantity);
            dtTIHeader.Columns.Add(GrossWeight);
            dtTIHeader.Columns.Add(TotalTIAmount);
            dtTIHeader.Columns.Add(Isexported);
            DataRow dRow = dtTIHeader.NewRow();
            dRow["TINumber"] = m_objTI.TNumber;
            dRow["TONumber"] = m_objTI.TONumber;
            dRow["SourceLocation"] = txtSourceLocation.Text;
            dRow["DestinationLocation"] = txtDestinationLocation.Text;
            dRow["PackSize"] = m_objTI.PackSize;
            dRow["SourceAddress"] = m_objTI.SourceAddress;
            dRow["DestinationAddress"] = m_objTI.DestinationAddress;
            dRow["Status"] = txtStatus.Text;
            dRow["SourceTINNo"] = txtSourceTin.Text;
            dRow["SourceCSTNo"] = txtSourceCST.Text;
            dRow["SourceVATNo"] = txtSourceVAT.Text;
            dRow["DestinationTINNo"] = txtDestTin.Text;
            dRow["DestinationTINNo"] = txtDestCST.Text;
            dRow["DestinationVATNo"] = txtDestVAT.Text;
            dRow["Indentised"] = (m_objTI.Indentised == 1 ? "Yes" : "No");
            dRow["ReceivedDate"] = Convert.ToDateTime(m_objTI.ReceivedDate).ToString(Common.DTP_DATE_FORMAT);
            string dtime = Convert.ToDateTime(m_objTI.ReceivedTime).ToString("HH:mm:ss");
            dRow["ReceivedTime"] = dtime;
            dRow["ShippingDetails"] = m_objTI.ShippingDetails;
            dRow["ShippingWayBillNo"] = m_objTI.ShippingBillNo;
            dRow["Remarks"] = m_objTI.Remarks;
            dRow["TotalTIQuantity"] = Math.Round(m_objTI.TotalTIQuantity, 2);
            dRow["GrossWeight"] = Math.Round(m_objTI.GrossWeight, 3);
            dRow["TotalTIAmount"] = Math.Round(m_objTI.TotalTIAmount, 2);
            dRow["Isexported"] = (m_objTI.Isexport == 1 ? "true" : "false");
            dtTIHeader.Rows.Add(dRow);
            // Search ItemData for dataTable
            DataTable dtTIDetail = new DataTable("TIDetail");
            dtTIDetail = m_objTI.SearchItemDataTable(String.Empty, m_objTI.TNumber, m_objTI.SourceLocationId);
            for (int i = 0; i < dtTIDetail.Rows.Count; i++)
            {
                dtTIDetail.Rows[i]["TransferPrice"] = Math.Round(Convert.ToDecimal(dtTIDetail.Rows[i]["TransferPrice"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                dtTIDetail.Rows[i]["TotalAmount"] = Math.Round(Convert.ToDecimal(dtTIDetail.Rows[i]["TotalAmount"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                dtTIDetail.Rows[i]["AfterAdjustQty"] = Math.Round(Convert.ToDecimal(dtTIDetail.Rows[i]["AfterAdjustQty"]), Common.DisplayQtyRounding, MidpointRounding.AwayFromZero);
            }

            m_printDataSet.Tables.Add(dtTIHeader);
            m_printDataSet.Tables.Add(dtTIDetail.Copy());

        }
        /// <summary>
        /// Prints TO Screen report
        /// </summary>
        private void PrintReport()
        {
            CreatePrintDataSet();
            CoreComponent.UI.ReportScreen reportScreenObj = new CoreComponent.UI.ReportScreen((int)Common.ReportType.TI, m_printDataSet);
            reportScreenObj.ShowDialog();
            //reportScreenObj.PrintReport();
            m_printDataSet = null;
        }


        #endregion

        #region Event



        /// <summary>
        /// Add Item Into Grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddDetails_Click(object sender, EventArgs e)
        {
            try
            {
                Totaltoquan = txtTotalQty.Text;
                updatetoquan = txtAdjustableQty.Text;
                CreateTIObject();
                EmptyErrorProvider();
                ValidateMessages();

                #region Check Errors
                StringBuilder sbError = new StringBuilder();
                sbError = GenerateError();
                #endregion

                if (!sbError.ToString().Trim().Equals(string.Empty))
                {
                    MessageBox.Show(sbError.ToString(), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                m_validQuantity = true;

                bool b = CheckValidQuantity(m_objTI.StatusId);

                if (m_validQuantity && m_isDuplicateRecordFound <= 0)
                {
                    if (AddItem())
                    {
                        dgvTIItem.DataSource = null;
                        if (m_lstTIDetail.Count > 0)
                        {
                            dgvTIItem.DataSource = m_lstTIDetail;
                            txtTotalQty.Text =(Convert.ToInt32( Totaltoquan) + Convert.ToInt32(updatetoquan)).ToString();
                        }
                        ResetItemControl();
                    }
                }
                else if (m_validQuantity == false)
                {
                    MessageBox.Show(Common.GetMessage("VAL0085"),Common.GetMessage("10001"),MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Clear Item Controls 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearDetails_Click(object sender, EventArgs e)
        {
            try
            {
                ResetItemControl();
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
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Save((int)Common.TIStatus.Created);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Call fn. EditTO to Edit TI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvSearchTI_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                EditTO(e);
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
                    nvc.Add("LocationId", m_sourceLocationId.ToString());

                    CoreComponent.Controls.frmSearch objfrmSearch = new CoreComponent.Controls.frmSearch(CoreComponent.Controls.SearchTypes.Item, nvc);
                    ItemDetails _Item = (ItemDetails)objfrmSearch.ReturnObject;
                    objfrmSearch.ShowDialog();
                    _Item = (ItemDetails)objfrmSearch.ReturnObject;

                    if (_Item != null)
                    {
                        txtItemCode.Text = _Item.ItemCode.ToString();
                        //CalculateTransferPrice();
                        if (m_lstTIFullDetail != null)
                        {
                            var query = (from p in m_lstTIFullDetail where p.ItemCode == txtItemCode.Text.Trim() select p);
                            if (query.ToList().Count > 0)
                            {
                                if (m_tiDetail == null)
                                    m_tiDetail = new TIDetail();

                                m_tiDetail.ItemUnitPrice = query.ToList()[0].ItemUnitPrice;
                                txtTransferPrice.Text = query.ToList()[0].DisplayItemUnitPrice.ToString();
                            }
                            else
                                txtTransferPrice.Text = string.Empty;


                            FillItemPriceInfo(txtItemCode.Text.Trim());
                            ClearBatchInfor();
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
        /// Call fn. ValidateItemCode to Validate Item Code
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtItemCode_Validated(object sender, EventArgs e)
        {
            try
            {
                if(m_ValidateItem)
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
        /// Call fn. ValidateBatchNo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// Call fn. ValidateQuantity
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtAdjustableQty_Validated(object sender, EventArgs e)
        {
            try
            {
                ValidateQuantity(true);
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

        /// <summary>
        /// Save TO with Status Shipped
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirmed_Click(object sender, EventArgs e)
        {
            try
            {
                Save((int)Common.TIStatus.Confirmed);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        ///  To Reset Grid Before Saving into Grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateTONumber(false);
                StringBuilder sbError = new StringBuilder();
                if (errItem.GetError(txtTONumber).Trim().Length > 0)
                {
                    sbError.Append(errItem.GetError(txtTONumber));
                    sbError.AppendLine();
                }

                if (!sbError.ToString().Trim().Equals(string.Empty))
                {
                    MessageBox.Show(sbError.ToString(), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                TIHeader objTI = new TIHeader();
                string errMessage = string.Empty;
                m_lstTIDetail = objTI.SearchItem(txtTONumber.Text.Trim(), string.Empty, m_sourceLocationId);
                //m_bucketid = m_lstTIDetail[0].BucketId;
                m_bucketid = Common.INT_DBNULL;

                if (errMessage == string.Empty && m_lstTIDetail != null && m_lstTIDetail.Count > 0)
                {
                    dgvTIItem.DataSource = m_lstTIDetail;
                    dgvTIItem.Select();
                    ResetItemControl();
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Call fn. ResetTOAndItems
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreateReset_Click(object sender, EventArgs e)
        {
            try
            {
                tabControlTransaction.TabPages[1].Text = Common.TAB_CREATE_MODE;
                ResetTOAndItems();
                EnableDisableButton((int)Common.TOStatus.New);
                m_ValidateItem = false;
                txtTONumber.Focus();
                m_ValidateItem = true;
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Call fn. ValidateTONumber
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtTOINumber_Validated(object sender, EventArgs e)
        {
            try
            {
                ValidateTONumber(true);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Call fn. ValidatePackSize
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPackSize_Validated(object sender, EventArgs e)
        {
            try
            {
                ValidatePackSize();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Call fn. ValidateReceivedDate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtpReceivedDate_Validated(object sender, EventArgs e)
        {
            try
            {
                //ValidateReceivedDate();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Call fn. ValidateShippingWayBillNo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtShippingBillNo_Validated(object sender, EventArgs e)
        {
            try
            {
                //ValidateShippingWayBillNo();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Calll fn. SearchTI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateSearchControls())
                {
                    SearchTI();
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Call Fn. ValidateShippingDetails
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtShippingDetails_Validated(object sender, EventArgs e)
        {
            try
            {
                //ValidateShippingDetails();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Remove TOI Item from Grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvTOItem_CellClick(object sender, DataGridViewCellEventArgs e)
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
        /// Call fn. SelectGridRow
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvTOItem_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                SelectGridRow(e);
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
                CoreComponent.Core.BusinessObjects.VisitControls visitControls = new CoreComponent.Core.BusinessObjects.VisitControls();
                visitControls.ResetAllControlsInPanel(pnlSearchHeader);
                InitializeDateControl();

                if (m_locationType == (int)Common.LocationConfigId.BO || m_locationType == (int)Common.LocationConfigId.WH)
                    cmbSearchDestinationLocation.SelectedValue = m_currentLocationId.ToString();
                chkSearchIndentised.CheckState = CheckState.Indeterminate;
                dgvSearchTI.DataSource = new List<TIHeader>();
                txtSearchTONumber.Focus();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void txtAdjustableQty_Enter(object sender, EventArgs e)
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

        private void txtTONumber_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if ( e.KeyValue == Common.F4KEY && !e.Alt)
                {
                    ShowTONumber(txtTONumber);
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSearchTONumber_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if ( e.KeyValue == Common.F4KEY && !e.Alt)
                {
                    ShowTONumber(txtSearchTONumber);
                }
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
                if (m_objTI != null && m_objTI.StatusId >= (int)Common.TIStatus.Confirmed)
                {
                    btnPrint.Enabled = false;
                    PrintReport();
                    btnPrint.Enabled = true;
                }
                else
                    MessageBox.Show(Common.GetMessage("INF0101", "TI", Common.TIStatus.Confirmed.ToString()), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                btnPrint.Enabled = true;
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        #endregion

        private void txtAdjustableQty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ValidateQuantity(true);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvSearchTI_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (dgvSearchTI.SelectedRows.Count > 0)
                {
                    string tiNumber = Convert.ToString(dgvSearchTI.SelectedRows[0].Cells["TINumber"].Value);

                    var  query = (from p in m_lstTIHeader where p.TNumber==tiNumber select p);
                    m_objTI = (TIHeader)query.ToList()[0];
                    SelectSearchGrid(tiNumber, m_objTI.SourceLocationId);
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pnlCreateHeader_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cmbSearchSourceLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }
        //bool Search(int fromLocation, int toLocation)
        //{
        //    List<int> listLocation = new List<int>();
        //    //LocationList objLocationlst = new LocationList();
        //    //objLocationlst.SourceLocationID = fromLocation;
        //    //objLocationlst.DestinationLocationID = toLocation;

        //    //listLocation = objLocation.Search();
        //    bool result = objLocationlst.Search();
        //    //return listLocation;
        //    return result;
        //}



    }
}
