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
    public partial class frmTO : CoreComponent.Core.UI.Transaction
    {
        #region Variables
        //int m_currentLocationId = 11;
        //int m_userId = 5;
        //int m_locationType = 2;
        private StringBuilder m_sbContactNo;
        int m_bucketid = Common.INT_DBNULL;
        int m_UOMId = Common.INT_DBNULL;
        //string m_oldManufactureBatchNo = string.Empty;
        Boolean m_F4Press = false;
        //string m_IndentNo = string.Empty;
        decimal m_weight = 0;
        string m_UOMName = string.Empty;
        decimal m_MRP = 0;
        int m_destinationLocationId = Common.INT_DBNULL;
        List<int> m_lstRemovedItem;
        List<TOHeader> m_lstTOHeader;
        List<TODetail> m_lstTODetail;
        List<TODetail> m_lstTOFullDetail;
        TODetail m_toDetail;
        TOHeader m_objTO;
        string m_mfgDate = Common.DATETIME_NULL.ToString();
        string m_expDate = Common.DATETIME_NULL.ToString();
        string m_toNo = Common.INT_DBNULL.ToString();
        int m_selectedItemRowNum = Common.INT_DBNULL;
        int m_selectedItemRowIndex = Common.INT_DBNULL;
        int m_itemId = Common.INT_DBNULL;
        Boolean m_validQuantity = true;
        string m_batchNo = string.Empty;
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
        LocationList objLocation = new LocationList();
        TOI m_objTOI1 = new TOI();
        List<TOI> lstTOI = new List<TOI>();
        TOI objTOI = new TOI();
        #endregion Variables

        #region C'tor
        public frmTO()
        {
            try
            {
                m_isSaveAvailable = Authenticate.IsFunctionAccessible(strUserName, strLocationCode, TOHeader.MODULE_CODE, Common.FUNCTIONCODE_SAVE);
                m_isSearchAvailable = Authenticate.IsFunctionAccessible(strUserName, strLocationCode, TOHeader.MODULE_CODE, Common.FUNCTIONCODE_SEARCH);
                m_isConfirmAvailable = Authenticate.IsFunctionAccessible(strUserName, strLocationCode, TOHeader.MODULE_CODE, Common.FUNCTIONCODE_CONFIRM);
                m_isPrintAvailable = Authenticate.IsFunctionAccessible(strUserName, strLocationCode, TOHeader.MODULE_CODE, Common.FUNCTIONCODE_PRINT);
                InitializeComponent();
                GridInitialize();
                FillLocations();
                FillSearchStatus();
                InitializeDateControl();

                EnableDisableButton((int)Common.TOStatus.New);
                lblPageTitle.Text = "Transfer Out";
                m_sbContactNo = new StringBuilder();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion


        #region Properties
        public string Toquantity
        {
            get;
            set;
        }
        public string Grossweight
        {
            get;
            set;
        }
        public string TotalToamont
        {
            get;
            set;
        }

        #endregion


        #region Methods
        /// <summary>
        /// Show Pop up for TOI Number
        /// </summary>
        /// <param name="txt"></param>
        void ShowTOINumber(TextBox txt)
        {
            NameValueCollection nvc = new NameValueCollection();
            if (m_locationType == (int)Common.LocationConfigId.BO || m_locationType == (int)Common.LocationConfigId.WH)
                nvc.Add("SourceLocationId", m_currentLocationId.ToString());
            else
                nvc.Add("SourceLocationId", Common.INT_DBNULL.ToString());

            //nvc.Add("FromCreationDate", Common.DATETIME_NULL.ToString());
            //nvc.Add("ToCreationDate", Common.DATETIME_MAX.ToString());
            nvc.Add("StatusId", ((int)Common.TOIStatus.Confirmed).ToString());
            //nvc.Add("FromTOIDate", Common.DATETIME_NULL.ToString());
            //nvc.Add("ToTOIDate", Common.DATETIME_MAX.ToString());
            nvc.Add("DestinationLocationId", Common.INT_DBNULL.ToString());
            //nvc.Add("Indentised", Common.INT_DBNULL.ToString());

            CoreComponent.Controls.frmSearch objfrmSearch = new CoreComponent.Controls.frmSearch(CoreComponent.Controls.SearchTypes.TOI, nvc);
            TOI objTOI = (TOI)objfrmSearch.ReturnObject;
            objfrmSearch.ShowDialog();
            objTOI = (TOI)objfrmSearch.ReturnObject;

            if (objTOI != null)
            {
                txt.Text = objTOI.TNumber.ToString();
            }
        }

        /// <summary>
        /// Validate Creation from and To Date
        /// </summary>
        void ValidateSearchFromDate()
        {
            if (dtpSearchTOFrom.Checked == true && dtpSearchTOTO.Checked == true)
            {
                DateTime fromDate = dtpSearchTOFrom.Checked == true ? Convert.ToDateTime(dtpSearchTOFrom.Value) : Common.DATETIME_NULL;
                DateTime toDate = dtpSearchTOTO.Checked == true ? Convert.ToDateTime(dtpSearchTOTO.Value) : Common.DATETIME_NULL;

                TimeSpan ts = fromDate - toDate;
                if (ts.Days > 0)
                    errSearch.SetError(dtpSearchTOFrom, Common.GetMessage("VAL0047", lblSearchTOToDate.Text.Trim().Substring(0, lblSearchTOToDate.Text.Trim().Length - 1), lblSearchTOIFromDate.Text.Trim().Substring(0, lblSearchTOIFromDate.Text.Trim().Length - 1)));
                else
                    errSearch.SetError(dtpSearchTOFrom, string.Empty);
            }
        }

        /// <summary>
        /// Validate TOI From and To Date
        /// </summary>
        void ValidateSearchShipDate()
        {
            if (dtpSearchShipFrom.Checked == true && dtpSearchShipTO.Checked == true)
            {
                DateTime fromDate = dtpSearchShipFrom.Checked == true ? Convert.ToDateTime(dtpSearchShipFrom.Value) : Common.DATETIME_NULL;
                DateTime toDate = dtpSearchShipTO.Checked == true ? Convert.ToDateTime(dtpSearchShipTO.Value) : Common.DATETIME_NULL;

                TimeSpan ts = fromDate - toDate;
                if (ts.Days > 0)
                    errSearch.SetError(dtpSearchShipFrom, Common.GetMessage("VAL0047", lblSearchTOICreationDateTO.Text.Trim().Substring(0, lblSearchTOICreationDateTO.Text.Trim().Length - 1), lblSearchTOICreationDate.Text.Trim().Substring(0, lblSearchTOICreationDate.Text.Trim().Length - 1)));
                else
                    errSearch.SetError(dtpSearchShipFrom, string.Empty);
            }
        }
        /// <summary>
        /// Grid Initialize
        /// </summary>
        void GridInitialize()
        {
            dgvSearchTO.AutoGenerateColumns = false;
            dgvSearchTO.DataSource = null;
            DataGridView dgvSearchTONew = Common.GetDataGridViewColumns(dgvSearchTO, Environment.CurrentDirectory + "\\App_Data\\Transfer.xml");

            dgvTOItem.AutoGenerateColumns = false;
            dgvTOItem.DataSource = null;
            DataGridView dgvTOItemNew = Common.GetDataGridViewColumns(dgvTOItem, Environment.CurrentDirectory + "\\App_Data\\Transfer.xml");
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

                if (m_locationType == (int)Common.LocationConfigId.BO || m_locationType == (int)Common.LocationConfigId.WH)
                {
                    cmbSearchSourceLocation.SelectedValue = m_currentLocationId.ToString();
                    cmbSearchSourceLocation.Enabled = false;
                }
            }

            DataTable dtSearchDest = Common.ParameterLookup(Common.ParameterType.Locations, new ParameterFilter("LOCATIONS", 0, 0, 0));
            if (dtSearchDest != null)
            {
                cmbSearchDestinationLocation.DataSource = dtSearchDest;
                cmbSearchDestinationLocation.DisplayMember = "DisplayName";
                cmbSearchDestinationLocation.ValueMember = "LocationId";
            }
        }
        /// <summary>
        /// Fill Status Drop Down List
        /// </summary>
        private void FillSearchStatus()
        {
            DataTable dt = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter("TOStatus", 0, 0, 0));
            cmbSearchStatus.DataSource = dt;
            cmbSearchStatus.DisplayMember = Common.KEYVALUE1;
            cmbSearchStatus.ValueMember = Common.KEYCODE1;
        }
        /// <summary>
        /// Initialize Date Picker Value
        /// </summary>
        void InitializeDateControl()
        {
            dtpSearchShipFrom.CustomFormat = Common.DTP_DATE_FORMAT;
            dtpSearchShipTO.CustomFormat = Common.DTP_DATE_FORMAT;
            dtpSearchTOFrom.CustomFormat = Common.DTP_DATE_FORMAT;
            dtpSearchTOTO.CustomFormat = Common.DTP_DATE_FORMAT;
            dtpSearchShipFrom.Checked = false;
            dtpSearchShipTO.Checked = false;
            dtpSearchTOFrom.Checked = false;
            dtpSearchTOTO.Checked = false;

            dtpExpectedDate.CustomFormat = Common.DTP_DATE_FORMAT;
            dtpExpectedDate.Checked = false;

            dtpSearchShipFrom.Value = Convert.ToDateTime(System.DateTime.Now.ToShortDateString());
            dtpSearchShipTO.Value = Convert.ToDateTime(System.DateTime.Now.ToShortDateString());
            dtpSearchTOFrom.Value = Convert.ToDateTime(System.DateTime.Now.ToShortDateString());
            dtpSearchTOTO.Value = Convert.ToDateTime(System.DateTime.Now.ToShortDateString());
        }
        /// <summary>
        /// Search TO
        /// </summary>
        List<TOHeader> SearchTO(string toiNumber)
        {
            DateTime DATETIME_MAX = Convert.ToDateTime(new DateTime(2099, 12, 31).ToString(Common.DATE_TIME_FORMAT));

            DateTime toFrom = dtpSearchTOFrom.Checked == true ? Convert.ToDateTime(dtpSearchTOFrom.Value) : Common.DATETIME_NULL;
            DateTime toTo = dtpSearchTOTO.Checked == true ? Convert.ToDateTime(dtpSearchTOTO.Value) : DATETIME_MAX;

            DateTime shipTo = dtpSearchShipTO.Checked == true ? Convert.ToDateTime(dtpSearchShipTO.Value) : DATETIME_MAX;
            DateTime shipFrom = dtpSearchShipFrom.Checked == true ? Convert.ToDateTime(dtpSearchShipFrom.Value) : Common.DATETIME_NULL;

            List<TOHeader> toHeader = new List<TOHeader>();
            toHeader = Search(toiNumber, string.Empty, Common.INT_DBNULL, Common.INT_DBNULL, toFrom, toTo, shipFrom, shipTo, Common.INT_DBNULL, string.Empty, Common.INT_DBNULL, Common.INT_DBNULL);
            return toHeader;
        }
        /// <summary>
        /// Search TOIs
        /// </summary>
        void SearchTO()
        {
            //DateTime DATETIME_MAX = Convert.ToDateTime(new DateTime(2099, 12, 31).ToString(Common.DATE_TIME_FORMAT));

            DateTime toFrom = dtpSearchTOFrom.Checked == true ? Convert.ToDateTime(dtpSearchTOFrom.Value) : Common.DATETIME_NULL;
            DateTime toTo = dtpSearchTOTO.Checked == true ? Convert.ToDateTime(dtpSearchTOTO.Value) : Common.DATETIME_NULL;

            DateTime shipTo = dtpSearchShipTO.Checked == true ? Convert.ToDateTime(dtpSearchShipTO.Value) : Common.DATETIME_NULL;
            DateTime shipFrom = dtpSearchShipFrom.Checked == true ? Convert.ToDateTime(dtpSearchShipFrom.Value) : Common.DATETIME_NULL;

            int indentised = Convert.ToInt32(chkSearchIndentised.CheckState);// ? 1 : 0;
            int isexported = 0;//Convert.ToInt32(ckkexport.CheckState);// ? 1 : 0;
            m_lstTOHeader = Search(txtSearchTOINumber.Text.Trim(), txtSearchTONumber.Text.Trim(), Convert.ToInt32(cmbSearchSourceLocation.SelectedValue), Convert.ToInt32(cmbSearchDestinationLocation.SelectedValue), toFrom, toTo, shipFrom, shipTo, Convert.ToInt32(cmbSearchStatus.SelectedValue), txtSearchRefNumber.Text.Trim(), indentised, isexported);

            if ((m_lstTOHeader != null) && (m_lstTOHeader.Count > 0))
            {
                dgvSearchTO.DataSource = m_lstTOHeader;
                dgvSearchTO.ClearSelection();
            }
            else
            {
                dgvSearchTO.DataSource = new List<TOHeader>();
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
        List<TOHeader> Search(string toiNumber, string toNumber, int fromLocation, int toLocation, DateTime toiFromDate, DateTime toiToDate, DateTime shipFromDate, DateTime shipToDate, int status, string refNumber, int indentised, int isexported)
        {
            List<TOHeader> listTO = new List<TOHeader>();
            TOHeader objTO = new TOHeader();
            objTO.TOINumber = toiNumber;
            objTO.TNumber = toNumber;
            objTO.FromShipDate = shipFromDate;
            objTO.ToShipDate = shipToDate;
            objTO.StatusId = status;
            objTO.FromTODate = toiFromDate;
            objTO.ToTODate = toiToDate;
            objTO.SourceLocationId = fromLocation;
            objTO.DestinationLocationId = toLocation;
            objTO.Indentised = indentised; // chkSearchIndentised.Checked ? 1 : 0;
            objTO.Isexport = isexported;
            objTO.RefNumber = refNumber;
            listTO = objTO.Search();

            return listTO;
        }
        /// <summary>
        /// Validate Ref. Number
        /// </summary>
        void ValidateRefNumber()
        {
            ValidatedText(txtRefNumber, lblRefNumber);
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
        /// Show Remarks
        /// </summary>
        /// <param name="stateId"></param>
        void STNRemarks(int stateId)
        {
            DataTable dtStnRemarks = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter("STNREMARKS", stateId, 0, 0));
            if (dtStnRemarks.Rows.Count > 1)
                txtBranchSpecificRemarks.Text = dtStnRemarks.Rows[1]["KeyValue1"].ToString();
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

            DataTable dtData = Common.ParameterLookup(Common.ParameterType.CenterInfo, new ParameterFilter("CENTERINFO", sourceLocationID, 0, 0));

            //List<LocationHierarchy> lstHier = new List<LocationHierarchy>();
            //lstHier = locHie.Search();
            if (dtData != null && dtData.Rows.Count > 0)
            {
                txtSourceVAT.Text = dtData.Rows[0]["VatNo"] as string;
                txtSourceCST.Text = dtData.Rows[0]["CstNo"] as string;

                txtSourceTin.Text = dtData.Rows[0]["TinNo"] as string;
                if (m_sbContactNo != null)
                {
                    m_sbContactNo.Remove(0, m_sbContactNo.Length);

                    if (dtData.Rows[0]["phone1"] != null && dtData.Rows[0]["phone1"].ToString().Trim().Length > 0)
                        m_sbContactNo.AppendLine(dtData.Rows[0]["phone1"].ToString());
                    if (dtData.Rows[0]["phone2"] != null && dtData.Rows[0]["phone2"].ToString().Trim().Length > 0)
                        m_sbContactNo.AppendLine(dtData.Rows[0]["phone2"].ToString());
                    if (dtData.Rows[0]["mobile1"] != null && dtData.Rows[0]["mobile1"].ToString().Trim().Length > 0)
                        m_sbContactNo.AppendLine(dtData.Rows[0]["mobile1"].ToString());
                    if (dtData.Rows[0]["mobile2"] != null && dtData.Rows[0]["mobile2"].ToString().Trim().Length > 0)
                        m_sbContactNo.AppendLine(dtData.Rows[0]["mobile2"].ToString());
                }
            }
            dtData.Clear();
            locHie.HierarchyId = destinationLocationID;
            dtData = Common.ParameterLookup(Common.ParameterType.CenterInfo, new ParameterFilter("CENTERINFO", destinationLocationID, 0, 0));
            //lstHier = locHie.Search();
            if (dtData != null && dtData.Rows.Count > 0)
            {
                txtDestVAT.Text = dtData.Rows[0]["VatNo"] as string;
                txtDestCST.Text = dtData.Rows[0]["CstNo"] as string;
                txtDestTin.Text = dtData.Rows[0]["TinNo"] as string;
            }
        }
        /// <summary>
        /// Remove Location Contact Record
        /// </summary>
        /// <param name="e"></param>
        void RemoveTOItem(DataGridViewCellEventArgs e)
        {
            if ((e.RowIndex >= 0) && (dgvTOItem.Rows[e.RowIndex].Cells[e.ColumnIndex].GetType() == typeof(DataGridViewImageCell)))
            {

                if ((m_objTO == null) || (m_objTO != null && Convert.ToInt32(m_objTO.StatusId) < (int)Common.TOStatus.Shipped))
                {
                    DialogResult saveResult = MessageBox.Show(Common.GetMessage("INF0028"), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (saveResult == DialogResult.Yes)
                    {
                        if (m_lstRemovedItem == null)
                            m_lstRemovedItem = new List<int>();

                        if (m_lstTODetail.Count > 0)
                        {
                            m_lstRemovedItem.Add(m_lstTODetail[e.RowIndex].RowNo);

                            dgvTOItem.DataSource = null;
                            m_lstTODetail.RemoveAt(e.RowIndex);
                            dgvTOItem.DataSource = m_lstTODetail;
                            ResetItemControl();
                        }

                        ReadOnlyTOField();
                        bool b = CheckValidQuantity(m_objTO.StatusId);
                        m_validQuantity = true;
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
        private void SelectGridRow(EventArgs e)
        {
            if (dgvTOItem.SelectedCells.Count > 0)
            {
                int rowIndex = dgvTOItem.SelectedCells[0].RowIndex;
                int columnIndex = dgvTOItem.SelectedCells[0].ColumnIndex;

                if (rowIndex >= 0 && columnIndex >= 0)
                {
                    errItem.Clear();
                    int selectedRow = dgvTOItem.SelectedCells[0].RowIndex;

                    m_isDuplicateRecordFound = Common.INT_DBNULL;
                    m_validQuantity = true;

                    string itemCode = dgvTOItem.Rows[rowIndex].Cells["ItemCode"].Value.ToString().Trim();
                    m_batchNo = dgvTOItem.Rows[rowIndex].Cells["BatchNo"].Value.ToString().Trim();
                    m_bucketid = Convert.ToInt32(dgvTOItem.Rows[rowIndex].Cells["BucketId"].Value.ToString());

                    //m_RowNo = Convert.ToInt32(dgvTOIItem.Rows[e.RowIndex].Cells["RowNo"].Value.ToString().Trim());

                    ////Get ParentId
                    if (m_lstTODetail == null)
                        return;

                    var itemSelect = (from p in m_lstTODetail where p.ItemCode.ToLower() == itemCode.ToLower() && p.BatchNo.ToLower() == m_batchNo.ToLower() && p.BucketId == m_bucketid select p);

                    if (itemSelect.ToList().Count == 0)
                        return;

                    m_selectedItemRowIndex = rowIndex;

                    m_toDetail = itemSelect.ToList()[0];

                    m_batchNo = m_toDetail.BatchNo;
                    m_itemId = m_toDetail.ItemId;
                    m_UOMId = m_toDetail.UOMId;
                    //m_IndentNo = m_toDetail.IndentNo;
                    m_weight = m_toDetail.Weight;
                    m_UOMName = m_toDetail.UOMName;
                    m_MRP = m_toDetail.MRP;
                    m_mfgDate = m_toDetail.MfgDate;
                    m_expDate = m_toDetail.ExpDate;
                    //m_bucketid = m_toDetail.BucketId;

                    txtWeight.Text = Math.Round(m_weight, 2).ToString();
                    txtItemCode.Text = m_toDetail.ItemCode;
                    txtItemDescription.Text = m_toDetail.ItemName;
                    txtBucketName.Text = m_toDetail.BucketName;
                    txtBatchNo.Text = m_toDetail.ManufactureBatchNo;
                    //txtIndentNo.Text = m_IndentNo;
                    txtAvailableQty.Text = m_toDetail.DisplayAvailableQty.ToString();
                    txtAdjustableQty.Text = m_toDetail.DisplayAfterAdjustQty.ToString();
                    txtTransferPrice.Text = m_toDetail.DisplayItemUnitPrice.ToString();
                    txtItemTotalAmount.Text = m_toDetail.DisplayItemTotalAmount.ToString();
                    txtRequestedQty.Text = m_toDetail.DisplayRequestQty.ToString();
                    txtUOMName.Text = m_UOMName;

                    //txtGrossWeight.Text = Math.Round(m_objTO.GrossWeight, 2).ToString();
                    //txtTotalQty.Text = Math.Round(Convert.ToDecimal(m_objTO.TotalTOQuantity), 2).ToString();
                    //txtTotalTOAmount.Text = Math.Round(Convert.ToDecimal(m_objTO.TotalTOAmount), 2).ToString();

                    txtTOINumber.Enabled = false;
                }
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
            m_batchNo = string.Empty;
            m_toDetail = null;
            m_bucketid = Common.INT_DBNULL;
            string m_mfgDate = Common.DATETIME_NULL.ToString();
            string m_expDate = Common.DATETIME_NULL.ToString();

            m_selectedItemRowIndex = Common.INT_DBNULL;
            CoreComponent.Core.BusinessObjects.VisitControls visitControls = new CoreComponent.Core.BusinessObjects.VisitControls();
            visitControls.ResetAllControlsInPanel(errItem, grpAddDetails);

            dgvTOItem.ClearSelection();
            txtItemCode.Focus();
        }

        /// <summary>
        /// Validate Controls On Add Button Click
        /// </summary>
        void ValidateMessages()
        {
            ValidateTOINumber(false);

            ValidateItemCode(false);
            if (errItem.GetError(txtBatchNo) == string.Empty)
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
            if (errItem.GetError(txtTOINumber).Trim().Length > 0)
            {
                sbError.Append(errItem.GetError(txtTOINumber));
                sbError.AppendLine();
            }
            if (errItem.GetError(txtPackSize).Trim().Length > 0)
            {
                sbError.Append(errItem.GetError(txtPackSize));
                sbError.AppendLine();
            }
            if (errItem.GetError(dtpExpectedDate).Trim().Length > 0)
            {
                sbError.Append(errItem.GetError(dtpExpectedDate));
                sbError.AppendLine();
            }

            if (errItem.GetError(txtRefNumber).Trim().Length > 0)
            {
                sbError.Append(errItem.GetError(txtRefNumber));
                sbError.AppendLine();
            }
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
        void ValidateOnSave()
        {
            ValidateTOINumber(false);
            ValidatePackSize();

            ValidateExpectedDate();

            ValidateRefNumber();
            ValidateShippingDetails();
            ValidateShippingWayBillNo();
        }
        /// <summary>
        /// Add Item on add button click 
        /// </summary>
        /// <returns></returns>
        bool AddItem()
        {

            //m_toDetail = new TODetail();
            m_toDetail.IndexSeqNo = Common.INT_DBNULL;
            m_toDetail.ItemId = m_itemId;
            m_toDetail.ItemCode = txtItemCode.Text;
            m_toDetail.ItemName = txtItemDescription.Text;
            m_toDetail.ManufactureBatchNo = txtBatchNo.Text;

            m_toDetail.BatchNo = m_batchNo;
            m_toDetail.BucketId = m_bucketid;
            m_toDetail.MRP = m_MRP;
            m_toDetail.UOMId = m_UOMId;

            m_toDetail.Weight = m_weight;
            m_toDetail.UOMName = m_UOMName;
            m_toDetail.MfgDate = m_mfgDate;
            m_toDetail.ExpDate = m_expDate;


            m_toDetail.BucketName = txtBucketName.Text;
            //m_toDetail.IndentNo = m_IndentNo;
            //m_toDetail.AvailableQty = Math.Round(Convert.ToDecimal(txtAvailableQty.Text), 2);
            //m_toDetail.AfterAdjustQty = Math.Round(Convert.ToDecimal(txtAdjustableQty.Text), 2);
            //m_toDetail.RequestQty = Math.Round(Convert.ToDecimal(txtRequestedQty.Text), 2);
            //m_toDetail.ItemUnitPrice = Math.Round(Convert.ToDecimal(txtTransferPrice.Text), 2);
            //m_toDetail.ItemTotalAmount = Math.Round(Convert.ToDecimal(txtItemTotalAmount.Text), 2);

            m_toDetail.RowNo = m_selectedItemRowNum;

            //decimal amt = CalculateTotalItemPrice();

            if (m_lstTODetail == null)
                m_lstTODetail = new List<TODetail>();

            if ((m_selectedItemRowIndex != Common.INT_DBNULL) && (m_selectedItemRowIndex <= dgvTOItem.Rows.Count))
            {
                m_lstTODetail.Insert(m_selectedItemRowIndex, m_toDetail);
                m_lstTODetail.RemoveAt(m_selectedItemRowIndex + 1);
            }
            else
                m_lstTODetail.Add(m_toDetail);

            ResetItemControl();
            return true;
        }

        List<TODetail> CopyTODetail(int excludeIndex, List<TODetail> lst)
        {
            List<TODetail> returnList = new List<TODetail>();
            for (int i = 0; i < lst.Count; i++)
            {
                if (i != excludeIndex)
                {
                    TODetail tdetail = new TODetail();
                    tdetail = lst[i];
                    returnList.Add(tdetail);
                }
            }

            return returnList;
        }

        bool CheckValidQuantity(int statusId)
        {
            decimal totalAmount = 0;
            decimal totalquantity = 0;
            decimal grossWeight = 0;
            txtTotalQty.Text = "0";
            txtTotalTOAmount.Text = "0";

            if (m_lstTODetail != null && m_lstTODetail.Count > 0)
            {
                for (int i = 0; i < m_lstTODetail.Count; i++)
                {
                    if (i == m_selectedItemRowIndex)
                        continue;


                    decimal totQty = Convert.ToDecimal(m_lstTODetail[i].AfterAdjustQty);
                    decimal availQty = Convert.ToDecimal(m_lstTODetail[i].AvailableQty);

                    if ((totQty > availQty) && (statusId < (int)Common.TOStatus.Created))
                    {
                        m_validQuantity = false;
                    }
                    else
                    {
                        totalAmount = totQty * m_lstTODetail[i].ItemUnitPrice;
                        m_objTO.TotalTOAmount = Convert.ToDecimal(txtTotalTOAmount.Text) + totalAmount;
                        txtTotalTOAmount.Text = m_objTO.DisplayTotalTOAmount.ToString();

                        totalquantity = totalquantity + totQty;
                        m_objTO.TotalTOQuantity = totalquantity;
                        txtTotalQty.Text = m_objTO.DisplayTotalTOQuantity.ToString();

                        grossWeight = grossWeight + totQty * m_lstTODetail[i].Weight;
                    }
                }

            }

            if (txtAdjustableQty.Text.Length > 0 && txtAvailableQty.Text.Length > 0)
            {
                m_toDetail.AvailableQty = Convert.ToDecimal(txtAvailableQty.Text);
                m_toDetail.AfterAdjustQty = Convert.ToDecimal(txtAdjustableQty.Text);

                decimal totQty = m_toDetail.AfterAdjustQty * 1;
                decimal availQty = m_toDetail.AvailableQty;

                if (totQty > availQty)
                {
                    m_validQuantity = false;
                }
                else
                {
                    if (m_lstTODetail != null && m_lstTODetail.Count > 0)
                    {
                        //checked based on ItemCode and Bucket Id
                        List<TODetail> toDetail = CopyTODetail(m_selectedItemRowIndex, m_lstTODetail);

                        m_isDuplicateRecordFound = (from p in toDetail where p.ItemCode.Trim().ToLower() == txtItemCode.Text.Trim().ToLower() && p.BatchNo.ToLower() == m_batchNo.ToLower() && p.BucketId == m_bucketid select p).Count();

                        //if (m_selectedItemRowIndex >= 0 && m_isDuplicateRecordFound >= 1)
                        //{
                        //    m_isDuplicateRecordFound = Common.INT_DBNULL;
                        //}

                        if (m_isDuplicateRecordFound > 0)
                        {
                            MessageBox.Show(Common.GetMessage("VAL0064", "Item code", "batch"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return false;
                        }
                    }

                    totalAmount = totalAmount + totQty * m_toDetail.ItemUnitPrice;
                    m_objTO.TotalTOAmount = totalAmount;
                    txtTotalTOAmount.Text = m_objTO.DisplayTotalTOAmount.ToString();

                    m_toDetail.AfterAdjustQty = Convert.ToInt32(txtAdjustableQty.Text);
                    m_toDetail.ItemTotalAmount = (m_toDetail.ItemUnitPrice * m_toDetail.AfterAdjustQty);
                    txtItemTotalAmount.Text = m_toDetail.DisplayItemTotalAmount.ToString();

                    totalquantity = totalquantity + totQty;
                    m_objTO.TotalTOQuantity = totalquantity;
                    txtTotalQty.Text = m_objTO.DisplayTotalTOQuantity.ToString();

                    grossWeight = grossWeight + m_toDetail.AfterAdjustQty * m_weight;
                }
            }

            if (m_validQuantity == false)
            {
                txtItemTotalAmount.Text = string.Empty;
                return false;
            }

            txtGrossWeight.Text = Math.Round(grossWeight, 2).ToString();

            return m_validQuantity;
        }
        /// <summary>
        /// Reset Item When Change of Tab Control
        /// </summary>
        /// <param name="e"></param>
        void tabControlSelect(TabControlCancelEventArgs e)
        {
            if ((tabControlTransaction.SelectedIndex == 0) && txtTOINumber.Text.Trim().Length > 0)
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
                    MessageBox.Show(Common.GetMessage("VAL0119", "TO"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tabControlTransaction.SelectedIndex = 0;
                }
                if (tabControlTransaction.TabPages[1].Text == Common.TAB_CREATE_MODE)
                {
                    ResetTOAndItems();
                    EnableDisableButton((int)Common.TOStatus.New);
                    //VisibleButton();
                }

            }
        }
        /// <summary>
        /// Reset TO and Item Controls
        /// </summary>
        void ResetTOAndItems()
        {
            txtTOINumber.Enabled = true;
            txtPackSize.Text = string.Empty;
            txtTOINumber.Text = string.Empty;
            txtRefNumber.Text = string.Empty;
            m_lstTOFullDetail = null;
            m_toNo = string.Empty;
            m_toDetail = null;
            m_objTO = null;
            dtpExpectedDate.Value = Convert.ToDateTime(System.DateTime.Now.ToShortDateString());
            dtpExpectedDate.Checked = false;
            EmptyErrorProvider();
            txtStatus.Text = Common.TOStatus.New.ToString();
            txtDestCST.Text = string.Empty;
            txtDestTin.Text = string.Empty;
            txtDestVAT.Text = string.Empty;
            txtSourceCST.Text = string.Empty;
            txtSourceVAT.Text = string.Empty;
            txtSourceTin.Text = string.Empty;

            txtBranchSpecificRemarks.Text = string.Empty;
            txtDestinationAddress.Text = string.Empty;
            txtDestinationLocation.Text = string.Empty;
            txtSourceLocation.Text = string.Empty;
            txtSourceAddress.Text = string.Empty;
            txtTONumber.Text = string.Empty;
            txtShippingDetails.Text = string.Empty;
            txtShippingBillNo.Text = string.Empty;
            txtRefNumber.Text = string.Empty;
            txtRemarks.Text = string.Empty;
            txtTotalQty.Text = string.Empty;
            txtTotalTOAmount.Text = string.Empty;
            txtGrossWeight.Text = string.Empty;

            ResetItemControl();
            dgvTOItem.DataSource = new List<TODetail>();
        }
        /// <summary>
        /// Save TO and Items
        /// </summary>
        /// <param name="statusId"></param>
        void Save(int statusId)
        {

            //EmptyErrorProvider();

            errItem.Clear();
            ValidateOnSave();

            #region Check Errors
            StringBuilder sbError = new StringBuilder();
            sbError = GenerateError();
            #endregion

            if (!sbError.ToString().Trim().Equals(string.Empty))
            {
                MessageBox.Show(sbError.ToString(), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (m_lstTODetail == null || m_lstTODetail.Count == 0)
            {
                MessageBox.Show(Common.GetMessage("VAL0024", "item"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            string itemlist = string.Empty;

            //var lstEditedItemGroupBy = from t in m_lstTODetail
            //                           group t by t.ItemCode into g
            //                           select new
            //                           {
            //                               ItemID = g.First<TODetail>().ItemCode,
            //                               RequestQty = g.First<TODetail>().RequestQty,
            //                               ItemCount = g.Count(),
            //                               TotalAvailableQty = g.Sum(p => p.AvailableQty),
            //                               TotalAdjustableQty = g.Sum(p => p.AfterAdjustQty)
            //                           };



            var lstEditedItemGroupBy = m_lstTODetail.GroupBy(x => new { x.ItemId, x.BucketId })
                                       .Select(g => new
                                       {
                                           ItemID = g.First<TODetail>().ItemCode.ToLower(),
                                           RequestQty = g.First<TODetail>().RequestQty,
                                           BucketId = g.First<TODetail>().BucketId,
                                           TotalAvailableQty = g.Sum(p => p.AvailableQty),
                                           TotalAdjustableQty = g.Sum(p => p.AfterAdjustQty)
                                       });


            // No of Item in Grid Should match with added Item
            List<TODetail> toItemDetail = new List<TODetail>();
            TOHeader objTO = new TOHeader();
            string errMessage = string.Empty;

            if (m_objTO.StatusId < (int)Common.TOStatus.Created)
                toItemDetail = objTO.AdjustItemAndBatch(txtTOINumber.Text.Trim(), m_currentLocationId, ref errMessage);
            else
            {
                toItemDetail = m_objTO.SearchItem(m_objTO.TNumber, m_currentLocationId);
            }

            //var lstItemGroupByBeforeSearch = from t in toItemDetail
            //                                 group t by t.ItemCode into g
            //                                 select new
            //                                 {
            //                                     ItemID = g.First<TODetail>().ItemCode,
            //                                     RequestQty = g.First<TODetail>().RequestQty,
            //                                     ItemCount = g.Count(),
            //                                     TotalAvailableQty = g.Sum(p => p.AvailableQty),
            //                                     TotalAdjustableQty = g.Sum(p => p.AfterAdjustQty)
            //                                 };

            if (toItemDetail != null)
            {
                var lstItemGroupByBeforeSearch = toItemDetail.GroupBy(x => new { x.ItemId, x.BucketId })
                                                   .Select(g => new
                                                   {
                                                       ItemID = g.First<TODetail>().ItemCode,
                                                       RequestQty = g.First<TODetail>().RequestQty,
                                                       BucketId = g.First<TODetail>().BucketId,
                                                       TotalAvailableQty = g.Sum(p => p.AvailableQty),
                                                       TotalAdjustableQty = g.Sum(p => p.AfterAdjustQty)
                                                   });


                for (int i = 0; i < lstItemGroupByBeforeSearch.ToList().Count; i++)
                {
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

            // Confirmation Before Saving
            //DialogResult saveResult = MessageBox.Show(Common.GetMessage("INF0027"), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            MemberInfo[] memberInfos = typeof(Common.TIStatus).GetMembers(BindingFlags.Public | BindingFlags.Static);
            // Confirmation Before Saving, statusId
            DialogResult saveResult = MessageBox.Show(Common.GetMessage("5010", Common.GetConfirmationStatusText(memberInfos[statusId].Name)), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (saveResult == DialogResult.Yes)
            {
                CreateTOObject();

                if (m_objTO != null)
                {
                    if (m_objTO.ModifiedDate.Length > 0)
                        m_objTO.ModifiedDate = Convert.ToDateTime(m_objTO.ModifiedDate).ToString(Common.DATE_TIME_FORMAT);//.ToString(Common.DATE_TIME_FORMAT);
                }

                m_objTO.TOINumber = txtTOINumber.Text.Trim();
                m_objTO.ShipDate = System.DateTime.Now.ToString(Common.DATE_TIME_FORMAT).ToString();
                m_objTO.ShippingBillNo = txtShippingBillNo.Text.Trim();
                m_objTO.ShippingDetails = txtShippingDetails.Text.Trim();
                m_objTO.GrossWeight = Convert.ToDecimal(txtGrossWeight.Text.Trim().Length == 0 ? grossWeight().ToString() : txtGrossWeight.Text);

                m_objTO.SourceLocationId = m_currentLocationId;
                m_objTO.SourceAddress = txtSourceAddress.Text;
                m_objTO.DestinationAddress = txtDestinationAddress.Text;
                m_objTO.DestinationLocationId = m_destinationLocationId;
                //m_objTO.Isexport = 0;
                m_objTO.Isexport = (chkIsexported.Checked == true ? 1 : 0);
                m_objTO.Isexported = (chkIsexported.Checked == true ? true : false);

                //m_objTO.TotalTOQuantity = Convert.ToDecimal(txtTotalQty.Text);
                //m_objTO.TotalTOAmount = Convert.ToDecimal(txtTotalTOAmount.Text);
                m_objTO.PackSize = Convert.ToInt32(txtPackSize.Text);
                m_objTO.RefNumber = txtRefNumber.Text;
                m_objTO.Remarks = txtRemarks.Text;
                DateTime expectedDate = dtpExpectedDate.Checked == true ? Convert.ToDateTime(dtpExpectedDate.Value) : Common.DATETIME_NULL;

                m_objTO.ExpectedDeliveryDate = Convert.ToDateTime(expectedDate).ToString(Common.DATE_TIME_FORMAT);
                m_objTO.Indentised = 0;

                // Get Location Code From User Object.
                m_objTO.LocationId = m_currentLocationId;
                m_objTO.ModifiedBy = m_userId;

                m_objTO.StatusId = statusId;

                m_objTO.TOItems = m_lstTODetail;
                //m_objTO.RemoveItems = m_lstRemovedItem;

                string errorMessage = string.Empty;

                bool result = m_objTO.Save(Common.ToXml(m_objTO), ref errorMessage);

                if (errorMessage.Equals(string.Empty))
                {
                    ResetItemControl();

                    EnableDisableButton(GetTOIStatus(txtTOINumber.Text));

                    dgvTOItem.DataSource = new List<TODetail>();
                    if (m_lstTODetail != null && m_lstTODetail.Count > 0)
                    {
                        m_lstTODetail = SearchTOItem(m_objTO.TNumber, m_objTO.SourceLocationId);
                        dgvTOItem.DataSource = m_lstTODetail;
                        ResetItemControl();
                        txtTONumber.Text = m_objTO.TNumber.ToString();
                    }
                    //MessageBox.Show(Common.GetMessage("8001"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MessageBox.Show(Common.GetMessage("8013", memberInfos[statusId].Name, m_objTO.TNumber), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show(Common.GetMessage(errorMessage), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }


        void SelectSearchGrid(string tNumber, int sourceLocationId)
        {
            chkIsexported.Enabled = false;
            m_lstTODetail = SearchTOItem(m_objTO.TNumber, m_objTO.SourceLocationId);
            m_lstTOFullDetail = CopyTODetail(-1, m_lstTODetail);

            dgvTOItem.SelectionChanged -= new System.EventHandler(dgvTOItem_SelectionChanged);
            dgvTOItem.DataSource = new List<TODetail>();
            if (m_lstTODetail != null && m_lstTODetail.Count > 0)
            {
                dgvTOItem.DataSource = m_lstTODetail;
                ResetItemControl();
            }
            tabControlTransaction.TabPages[1].Text = Common.TAB_UPDATE_MODE;
            tabControlTransaction.SelectedIndex = 1;
            m_toNo = tNumber;

            txtTOINumber.Text = m_objTO.TOINumber;
            EnableDisableButton(m_objTO.StatusId);
            dgvTOItem.SelectionChanged += new System.EventHandler(dgvTOItem_SelectionChanged);
            dgvTOItem.ClearSelection();

            txtPackSize.Text = m_objTO.PackSize.ToString();
            txtShippingBillNo.Text = m_objTO.ShippingBillNo;
            txtRefNumber.Text = m_objTO.RefNumber;
            txtGrossWeight.Text = Math.Round(m_objTO.GrossWeight, 2).ToString();
            txtShippingDetails.Text = m_objTO.ShippingDetails;
            txtRemarks.Text = m_objTO.Remarks;
            txtStatus.Text = m_objTO.StatusName;
            if (m_objTO.ExpectedDeliveryDate.Length > 0)
            {
                dtpExpectedDate.Checked = true;
                dtpExpectedDate.Value = Convert.ToDateTime(m_objTO.ExpectedDeliveryDate);
            }
            else
            { dtpExpectedDate.Checked = false; }

            DataTable dtSource = Common.ParameterLookup(Common.ParameterType.Locations, new ParameterFilter("LOCATIONS", 0, 0, 0));
            DataTable dt = dtSource.Clone();
            DataRow[] drSource = dtSource.Select("LocationId = " + m_objTO.SourceLocationId);
            DataRow[] drDest = dtSource.Select("LocationId = " + m_objTO.DestinationLocationId);
            //chkIsexported.Enabled = false;
            chkIsexported.Checked = (m_objTO.Isexport == 1 ? true : false);
            //chkIsexported.Enabled = (m_objTO.Isexport == 0 ? false : true);

            m_destinationLocationId = m_objTO.DestinationLocationId;

            txtSourceLocation.Text = drSource[0]["DisplayName"].ToString();
            txtDestinationLocation.Text = drDest[0]["DisplayName"].ToString();

            txtSourceAddress.Text = m_objTO.SourceAddress;
            txtDestinationAddress.Text = m_objTO.DestinationAddress;

            STNRemarks(m_objTO.StateId);
            ShowControlTaxInfor(m_objTO.SourceLocationId, m_objTO.DestinationLocationId);

            //VisibleButton();
            txtTONumber.Text = m_objTO.TNumber.ToString();

            txtTotalQty.Text = Math.Round(Convert.ToDecimal(m_objTO.TotalTOQuantity), 2).ToString();
            txtTotalTOAmount.Text = Math.Round(Convert.ToDecimal(m_objTO.TotalTOAmount), 2).ToString();
            Toquantity = txtTotalQty.Text;
            Grossweight = txtGrossWeight.Text;
            TotalToamont = txtTotalTOAmount.Text;

            txtTOINumber.Enabled = false;
        }


        /// <summary>
        /// Edit TOI
        /// </summary>
        /// <param name="e"></param>
        private void EditTOI(DataGridViewCellEventArgs e)
        {
            if ((e.RowIndex >= 0) && (dgvSearchTO.Rows[e.RowIndex].Cells[e.ColumnIndex].GetType() == typeof(DataGridViewImageCell)))
            {
                m_objTO = m_lstTOHeader[e.RowIndex];

                SelectSearchGrid(m_objTO.TNumber, m_objTO.SourceLocationId);
            }
        }
        /// <summary>
        /// Make read Only fields
        /// </summary>
        void ReadOnlyTOField()
        {
            if ((m_lstTODetail != null && m_lstTODetail.Count > 0))
            {
                txtTOINumber.Enabled = false;
            }
            else if (m_lstTODetail.Count == 0)
            {
                txtTOINumber.Enabled = true;
            }
        }
        /// <summary>
        /// Return List of Item Details
        /// </summary>
        /// <param name="TNumber"></param>
        /// <param name="sourceAddressId"></param>
        /// <returns></returns>
        List<TODetail> SearchTOItem(string TNumber, int sourceAddressId)
        {
            m_lstTODetail = m_objTO.SearchItem(TNumber, sourceAddressId);
            return m_lstTODetail;
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
            //TOI objTOI = new TOI();
            //decimal price = objTOI.CalTransferPrice(txtItemCode.Text, m_destinationLocationId.ToString());

            //if (m_toDetail == null)
            //    m_toDetail = new TODetail();
            //m_toDetail.ItemUnitPrice = m_lstTODetail[0].ItemUnitPrice;

            //m_toDetail.ItemUnitPrice = price;
            //txtTransferPrice.Text = m_toDetail.DisplayItemUnitPrice.ToString();
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
            itemDetails.LocationId = m_currentLocationId.ToString();

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
                txtWeight.Text = string.Empty;
                txtUOMName.Text = string.Empty;
                txtItemDescription.Text = string.Empty;
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
                errItem.SetError(txtBatchNo, string.Empty);

                //if (m_batchNo == string.Empty || m_oldManufactureBatchNo.ToLower().Trim()!=txtBatchNo.Text.Trim().ToLower())
                if (m_F4Press == false && yesNo == true)
                {
                    ItemBatchDetails objItem = new ItemBatchDetails();
                    objItem.ManufactureBatchNo = txtBatchNo.Text.Trim();
                    objItem.ItemCode = txtItemCode.Text.Trim();
                    objItem.LocationId = m_currentLocationId.ToString();
                    objItem.BucketId = m_bucketid.ToString();
                    objItem.FromMfgDate = Common.DATETIME_NULL;
                    objItem.ToMfgDate = Common.DATETIME_MAX;
                    List<ItemBatchDetails> lstItemDetails = objItem.Search();
                    if (txtItemCode.Text.Length > 0 && lstItemDetails != null)
                    {
                        var query = from p in lstItemDetails where p.Quantity > 0 && p.ItemId == m_itemId select p;
                        lstItemDetails = query.ToList();

                        if (lstItemDetails.Count == 1)
                        {
                            objItem = lstItemDetails[0];
                            GetBatchInfo(objItem);
                            //  m_oldManufactureBatchNo = txtBatchNo.Text;
                        }
                        else if (lstItemDetails.Count > 1)
                        {
                            errItem.SetError(txtBatchNo, Common.GetMessage("VAL0038", lblBatchNo.Text.Trim().Substring(0, lblBatchNo.Text.Trim().Length - 2)));
                        }
                        else
                        {
                            errItem.SetError(txtBatchNo, Common.GetMessage("VAL0001", lblBatchNo.Text.Trim().Substring(0, lblBatchNo.Text.Trim().Length - 2)));
                        }
                    }
                    else
                    {
                        errItem.SetError(txtBatchNo, Common.GetMessage("VAL0006", lblBatchNo.Text.Trim().Substring(0, lblBatchNo.Text.Trim().Length - 2)));
                        //MessageBox.Show(Common.GetMessage("VAL0006", "item code"));
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
            {
                txtItemDescription.Text = string.Empty;
                txtUOMName.Text = string.Empty;
                txtBatchNo.Text = string.Empty;
                txtAdjustableQty.Text = string.Empty;
                txtWeight.Text = string.Empty;
                txtItemTotalAmount.Text = string.Empty;
                txtTransferPrice.Text = string.Empty;
                txtAvailableQty.Text = string.Empty;

                if (yesNo == false)
                    errItem.SetError(txtItemCode, Common.GetMessage("INF0019", lblItemCode.Text.Trim().Substring(0, lblItemCode.Text.Trim().Length - 2)));
            }
            else if (isTextBoxEmpty == false)
            {
                errItem.SetError(txtItemCode, string.Empty);
                if (yesNo)
                {
                    errItem.SetError(txtItemCode, string.Empty);
                    ClearBatchInfor();
                    //CalculateTransferPrice();
                    if (m_lstTOFullDetail != null)
                    {
                        var query = (from p in m_lstTOFullDetail where p.ItemCode.ToLower().Trim() == txtItemCode.Text.ToLower().Trim() select p);
                        if (query.ToList().Count > 0)
                        {
                            m_toDetail.ItemUnitPrice = query.ToList()[0].ItemUnitPrice;
                            txtTransferPrice.Text = query.ToList()[0].DisplayItemUnitPrice.ToString();
                        }
                        else
                            txtTransferPrice.Text = string.Empty;
                    }
                }
                FillItemPriceInfo(txtItemCode.Text.Trim());
                GetRequestedQuantity(txtItemCode.Text.Trim());
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
            txtBatchNo.Text = objItem.ManufactureBatchNo.ToString();
            m_batchNo = objItem.BatchNo.ToString();
            m_mfgDate = objItem.MfgDate.ToString();
            m_expDate = objItem.ExpDate.ToString();
            m_bucketid = Convert.ToInt32(objItem.BucketId);
            txtBucketName.Text = objItem.BucketName;
            //txtBucketName.Text = objItem.BucketName.ToString();
            txtAvailableQty.Text = objItem.DisplayQuantity.ToString();
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
            nvc.Add("LocationId", m_currentLocationId.ToString());
            nvc.Add("BucketId", m_bucketid.ToString());

            CoreComponent.Controls.frmSearch objfrmSearch = new CoreComponent.Controls.frmSearch(CoreComponent.Controls.SearchTypes.ItemBatch, nvc);
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
            List<TODetail> lstTODetail = new List<TODetail>();
            TODetail objToDetail = new TODetail();
            TOHeader objTO = new TOHeader();
            string errMessage = string.Empty;

            if (txtTOINumber.Text.Trim().Length == 0 || m_objTO == null)
            {
                MessageBox.Show(Common.GetMessage("VAL0006", "TOI Number"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (m_objTO.StatusId < (int)Common.TOStatus.Created)
                lstTODetail = objTO.AdjustItemAndBatch(txtTOINumber.Text.ToString(), m_currentLocationId, ref errMessage);
            else
            {
                if (m_objTO != null)
                    lstTODetail = m_objTO.SearchItem(m_objTO.TNumber, m_currentLocationId);
                else
                    lstTODetail = m_objTO.SearchItem(txtTOINumber.Text.Trim(), m_currentLocationId);
            }

            if (lstTODetail != null)
            {
                if (txtTOINumber.Text.Trim().Length > 0)
                {
                    objToDetail = null;
                    //var query = from p in lstTODetail where p.ItemCode.ToLower().Trim() == itemCode.ToLower().Trim() && p.BatchNo.ToLower().Trim() == batchNo.ToLower().Trim() && p.BucketId == bucketId select p; //&& p.BucketId = 
                    if (m_bucketid != Common.INT_DBNULL)
                    {
                        var query = from p in lstTODetail where p.ItemCode.ToLower().Trim() == itemCode.ToLower().Trim() && p.BucketId == m_bucketid select p; //&& p.BucketId = 
                        if (query.ToList().Count > 0)
                            objToDetail = query.ToList()[0];
                        else
                            objToDetail = null;
                    }
                    else
                    {
                        var query1 = from p in lstTODetail where p.ItemCode.ToLower().Trim() == itemCode.ToLower().Trim() select p; //&& p.BucketId = 
                        if (query1.ToList().Count > 0)
                            objToDetail = query1.ToList()[0];
                        else
                            objToDetail = null;
                    }

                    if (objToDetail == null)
                    {
                        txtRequestedQty.Text = string.Empty;
                        m_bucketid = Common.INT_DBNULL;
                        errItem.SetError(txtItemCode, Common.GetMessage("VAL0040", lblItemCode.Text.Trim().Substring(0, lblItemCode.Text.Trim().Length - 2)));
                    }
                    else
                    {
                        m_toDetail.RequestQty = objToDetail.RequestQty;
                        decimal qty = Convert.ToDecimal(objToDetail.RequestQty);
                        txtRequestedQty.Text = m_toDetail.DisplayRequestQty.ToString();
                        //txtIndentNo.Text = objToDetail.IndentNo.ToString();
                        //m_IndentNo = objToDetail.IndentNo.ToString();
                        m_bucketid = objToDetail.BucketId;
                        txtBucketName.Text = objToDetail.BucketName;
                    }
                }
                else
                {
                    txtRequestedQty.Text = string.Empty;
                    m_bucketid = Common.INT_DBNULL;
                    //MessageBox.Show(Common.GetMessage("INF0026", "TOI Number"));
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
                errItem.SetError(txtAdjustableQty, string.Empty);

                if (txtAvailableQty.Text.Trim().Length > 0 && txtTransferPrice.Text.Trim().Length > 0 && Convert.ToDecimal(txtAvailableQty.Text) >= Convert.ToDecimal(txtAdjustableQty.Text))
                {
                    m_toDetail.AfterAdjustQty = Convert.ToDecimal(txtAdjustableQty.Text);
                    m_toDetail.ItemTotalAmount = m_toDetail.AfterAdjustQty * m_toDetail.ItemUnitPrice;
                    txtItemTotalAmount.Text = m_toDetail.DisplayItemTotalAmount.ToString();
                }
                else
                {
                    txtItemTotalAmount.Text = string.Empty;
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
            btnTransferOut.Enabled = enable;
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

                if (statusId == (int)Common.TOStatus.New)
                {
                    //btnTransferOut.Enabled = false;
                }
                else if (statusId == (int)Common.TOStatus.Shipped || statusId == (int)Common.TOStatus.Closed)
                {
                    btnTransferOut.Enabled = false;
                    btnSave.Enabled = false;
                    btnItemReset.Enabled = false;
                    btnAddDetails.Enabled = false;
                }
                else if (statusId == (int)Common.TOStatus.Created)
                {
                    //btnItemReset.Enabled = false;
                }

                btnTransferOut.Enabled = btnTransferOut.Enabled & m_isConfirmAvailable;
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
        int GetTOIStatus(string TOINumber)
        {
            List<TOHeader> toHeader = new List<TOHeader>();
            toHeader = Search(TOINumber, string.Empty, Common.INT_DBNULL, Common.INT_DBNULL, Common.DATETIME_NULL, Common.DATETIME_NULL, Common.DATETIME_NULL, Common.DATETIME_NULL, Common.INT_DBNULL, string.Empty, Common.INT_DBNULL, Common.INT_DBNULL);
            //toHeader = SearchTO(TOINumber);
            if (toHeader != null && toHeader.Count > 0)
            {
                txtStatus.Text = toHeader[0].StatusName;
                return toHeader[0].StatusId;
            }
            else
            {
                txtStatus.Text = Common.TOStatus.New.ToString();
                return (int)Common.TOStatus.New;
            }
        }
        /// <summary>
        /// Validate TOI Number
        /// </summary>
        /// <param name="yesNo"></param>
        void ValidateTOINumber(Boolean yesNo)
        {
            bool isTextBoxEmpty = CoreComponent.Core.BusinessObjects.Validators.CheckForEmptyString(txtTOINumber.Text.Trim().Length);
            if (isTextBoxEmpty == true)
                errItem.SetError(txtTOINumber, Common.GetMessage("INF0019", lblTOINumber.Text.Trim().Substring(0, lblTOINumber.Text.Trim().Length - 2)));
            else
            {
                #region If TOI Is Entered
                DataTable dtSearchSource = Common.ParameterLookup(Common.ParameterType.Locations, new ParameterFilter("LOCATIONS", 0, 0, 0));
                DateTime DATETIME_MAX = Convert.ToDateTime(new DateTime(2099, 12, 31).ToString(Common.DATE_TIME_FORMAT));
                //List<TOI> lstTOI = new List<TOI>();
                //TOI objTOI = new TOI();
                objTOI.TNumber = txtTOINumber.Text.Trim();
                objTOI.DestinationLocationId = Common.INT_DBNULL;
                objTOI.SourceLocationId = m_currentLocationId;
                objTOI.FromTOIDate = Common.DATETIME_NULL;
                objTOI.ToTOIDate = DATETIME_MAX;
                objTOI.FromCreationDate = Common.DATETIME_NULL;
                objTOI.ToCreationDate = DATETIME_MAX;
                objTOI.StatusId = Common.INT_DBNULL;
                objTOI.Indentised = Common.INT_DBNULL;
                //string toiNumber = txtTOINumber.Text;
                //ResetTOAndItems();
                //txtTOINumber.Text = toiNumber;
                lstTOI = objTOI.Search();
                if (lstTOI != null && lstTOI.Count > 0)
                {
                    objTOI = lstTOI[0];
                    txtTOINumber.Text = objTOI.TNumber;

                    if (objTOI.StatusId == (int)Common.TOIStatus.Closed || objTOI.StatusId == (int)Common.TOIStatus.Rejected)
                    {
                        MessageBox.Show(Common.GetMessage("VAL0039", lblTOINumber.Text.Trim().Substring(0, lblTOINumber.Text.Trim().Length - 2), objTOI.StatusName), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    //TODO::DONE REMOVE COMMENT
                    if (objTOI.StatusId != (int)Common.TOIStatus.Confirmed)
                    {
                        MessageBox.Show(Common.GetMessage("VAL0029"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    EnableDisableButton(GetTOIStatus(txtTOINumber.Text));
                    DataTable dtSource = Common.ParameterLookup(Common.ParameterType.Locations, new ParameterFilter("LOCATIONS", 0, 0, 0));
                    DataTable dt = dtSource.Clone();
                    DataRow[] drSource = dtSource.Select("LocationId = " + lstTOI[0].SourceLocationId);
                    DataRow[] drDest = dtSource.Select("LocationId = " + lstTOI[0].DestinationLocationId);

                    chkIsexported.Checked = (objTOI.Isexport == 1 ? true : false);

                    m_destinationLocationId = lstTOI[0].DestinationLocationId;
                    txtSourceLocation.Text = drSource[0]["DisplayName"].ToString();

                    txtDestinationLocation.Text = drDest[0]["DisplayName"].ToString();

                    txtSourceAddress.Text = lstTOI[0].SourceAddress;
                    txtDestinationAddress.Text = lstTOI[0].DestinationAddress;

                    ShowControlTaxInfor(lstTOI[0].SourceLocationId, lstTOI[0].DestinationLocationId);

                    STNRemarks(lstTOI[0].StateId);

                    if (yesNo)
                    {
                        string errMessage = string.Empty;
                        TOHeader objTO = new TOHeader();
                        m_lstTODetail = objTO.AdjustItemAndBatch(objTOI.TNumber, objTOI.SourceLocationId, ref errMessage);


                        if (errMessage != string.Empty)
                        {
                            MessageBox.Show(Common.GetMessage("VAL0027"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                        if (m_lstTODetail == null || m_lstTODetail.Count == 0)
                        {
                            MessageBox.Show(Common.GetMessage("VAL0028"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }

                        m_lstTOFullDetail = new List<TODetail>();
                        for (int i = 0; i < m_lstTODetail.Count; i++)
                            m_lstTOFullDetail.Add((TODetail)m_lstTODetail[i]);

                        m_bucketid = Common.INT_DBNULL;

                        //m_bucketid = m_lstTODetail[0].BucketId;

                        if (m_lstTODetail.Count > 0)
                            txtTOINumber.Enabled = false;

                        dgvTOItem.DataSource = m_lstTODetail;
                        ResetItemControl();

                        CreateTOObject();

                        if (m_toDetail == null)
                            m_toDetail = new TODetail();

                        bool b = CheckValidQuantity(Common.INT_DBNULL);
                        //m_objTO = objTO;
                        m_objTO.StatusId = (int)Common.TOStatus.New;
                        m_objTO.TNumber = Common.INT_DBNULL.ToString();
                    }

                    errItem.SetError(txtTOINumber, string.Empty);
                }
                else
                {
                    string toiNumber = txtTOINumber.Text;
                    ResetTOAndItems();
                    txtTOINumber.Text = toiNumber;
                    errItem.SetError(txtTOINumber, Common.GetMessage("INF0010", lblTOINumber.Text.Trim().Substring(0, lblTOINumber.Text.Trim().Length - 2)));
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
            var lstEditedItemGroupBy = (from t in m_lstTODetail select t.Weight * t.AfterAdjustQty).Sum();
            //                           select new
            //                           {
            //                               ItemID = g.First<TODetail>().ItemCode,
            //                               GrossWeight = g.First<TODetail>().Weight,
            //                               TotalGrossWeight1 = g.Sum(t => t.Weight * t.AfterAdjustQty)
            //                           };


            //var query = (from p in lstEditedItemGroupBy.ToList() select p.TotalGrossWeight1).Sum();

            //var lstEditedItemGroupBy = (from p in
            //                                (from t in m_lstTODetail
            //                                 group t by t.ItemCode into g
            //                                 select new
            //                                 {
            //                                     ItemID = g.First<TODetail>().ItemCode,
            //                                     //GrossWeight = g.First<TODetail>().Weight,
            //                                     TotalGrossWeight1 = g.Sum(t => t.Weight * t.AfterAdjustQty)
            //                                 })
            //                            select p.TotalGrossWeight1).Sum();


            return Convert.ToDecimal(lstEditedItemGroupBy);
        }
        /// <summary>
        /// Validate Expected date
        /// </summary>
        void ValidateExpectedDate()
        {
            if (dtpExpectedDate.Checked == false)
                errItem.SetError(dtpExpectedDate, Common.GetMessage("VAL0002", lblExpectedDate.Text.Trim().Substring(0, lblExpectedDate.Text.Trim().Length - 2)));
            else if (dtpExpectedDate.Checked == true)
            {
                DateTime expectedDate = dtpExpectedDate.Checked == true ? Convert.ToDateTime(dtpExpectedDate.Value) : Common.DATETIME_NULL;
                DateTime dt = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                TimeSpan ts = expectedDate - dt;
                if (ts.Days < 0)
                    errItem.SetError(dtpExpectedDate, Common.GetMessage("INF0010", lblExpectedDate.Text.Trim().Substring(0, lblExpectedDate.Text.Trim().Length - 2)));
                else
                    errItem.SetError(dtpExpectedDate, string.Empty);
            }
        }
        void EmptyErrorProvider()
        {
            errItem.SetError(txtTOINumber, string.Empty);
            errItem.SetError(txtPackSize, string.Empty);
            errItem.SetError(txtRemarks, string.Empty);
            errItem.SetError(dtpExpectedDate, string.Empty);
            errItem.SetError(txtRefNumber, string.Empty);
            errItem.SetError(txtShippingBillNo, string.Empty);
            errItem.SetError(txtShippingDetails, string.Empty);

            errItem.SetError(txtAdjustableQty, string.Empty);
            errItem.SetError(txtItemCode, string.Empty);
            //errItem.SetError(txtBatchNo, string.Empty);
        }
        /// <summary>
        /// Creates DataSet for Printing TO Screen report
        /// </summary>
        private void CreatePrintDataSet()
        {
            m_printDataSet = new DataSet();
            // Get Data For TOI Header Informaton in TOI Screen Report
            DataTable dtTOHeader = new DataTable("TOHeader");
            DataColumn TONumber = new DataColumn("TONumber", System.Type.GetType("System.String"));
            DataColumn TOINumber = new DataColumn("TOINumber", System.Type.GetType("System.String"));
            DataColumn SourceLocation = new DataColumn("SourceLocation", System.Type.GetType("System.String"));
            DataColumn SourcePhone = new DataColumn("SourcePhone", System.Type.GetType("System.String"));
            DataColumn SourceEmail = new DataColumn("SourceEmail", System.Type.GetType("System.String"));
            DataColumn DestinationLocation = new DataColumn("DestinationLocation", System.Type.GetType("System.String"));
            DataColumn SourceAddress = new DataColumn("SourceAddress", System.Type.GetType("System.String"));
            DataColumn DestinationAddress = new DataColumn("DestinationAddress", System.Type.GetType("System.String"));
            DataColumn SourceTINNo = new DataColumn("SourceTINNo", System.Type.GetType("System.String"));
            DataColumn SourceCSTNo = new DataColumn("SourceCSTNo", System.Type.GetType("System.String"));
            DataColumn SourceVATNo = new DataColumn("SourceVATNo", System.Type.GetType("System.String"));
            DataColumn DestinationTINNo = new DataColumn("DestinationTINNo", System.Type.GetType("System.String"));
            DataColumn DestinationCSTNo = new DataColumn("DestinationCStNo", System.Type.GetType("System.String"));
            DataColumn DestinationVATNo = new DataColumn("DestinationVATNo", System.Type.GetType("System.String"));
            DataColumn ExpectedDeliveryDate = new DataColumn("ExpectedDeliveryDate", System.Type.GetType("System.String"));
            DataColumn ReferenceNo = new DataColumn("ReferenceNo", System.Type.GetType("System.String"));
            DataColumn ShippingDetails = new DataColumn("ShippingDetails", System.Type.GetType("System.String"));
            DataColumn ShippingWayBillNo = new DataColumn("ShippingWayBillNo", System.Type.GetType("System.String"));
            DataColumn ModifiedByName = new DataColumn("ModifiedByName", System.Type.GetType("System.String"));
            DataColumn ModifiedDate = new DataColumn("ModifiedDate", System.Type.GetType("System.String"));
            DataColumn SourceCity = new DataColumn("SourceCity", System.Type.GetType("System.String"));
            DataColumn DestinationCity = new DataColumn("DestinationCity", System.Type.GetType("System.String"));
            DataColumn IsExported = new DataColumn("IsExported", System.Type.GetType("System.String"));
            //DataColumn ExportNo = new DataColumn("ExportNo", System.Type.GetType("System.String"));
            //DataColumn ImportNo = new DataColumn("ImportNo", System.Type.GetType("System.String"));
            DataColumn IECCode = new DataColumn("IECCode", System.Type.GetType("System.String"));


            dtTOHeader.Columns.Add(TONumber);
            dtTOHeader.Columns.Add(TOINumber);
            dtTOHeader.Columns.Add(SourceLocation);
            dtTOHeader.Columns.Add(SourcePhone);
            dtTOHeader.Columns.Add(SourceEmail);
            dtTOHeader.Columns.Add(DestinationLocation);
            dtTOHeader.Columns.Add(SourceAddress);
            dtTOHeader.Columns.Add(DestinationAddress);
            dtTOHeader.Columns.Add(SourceTINNo);
            dtTOHeader.Columns.Add(SourceCSTNo);
            dtTOHeader.Columns.Add(SourceVATNo);
            dtTOHeader.Columns.Add(DestinationTINNo);
            dtTOHeader.Columns.Add(DestinationCSTNo);
            dtTOHeader.Columns.Add(DestinationVATNo);
            dtTOHeader.Columns.Add(ExpectedDeliveryDate);
            dtTOHeader.Columns.Add(ReferenceNo);
            dtTOHeader.Columns.Add(ShippingDetails);
            dtTOHeader.Columns.Add(ShippingWayBillNo);
            dtTOHeader.Columns.Add(ModifiedByName);
            dtTOHeader.Columns.Add(ModifiedDate);
            dtTOHeader.Columns.Add(SourceCity);
            dtTOHeader.Columns.Add(DestinationCity);
            dtTOHeader.Columns.Add(IsExported);
            //dtTOHeader.Columns.Add(ExportNo);
            //dtTOHeader.Columns.Add(ImportNo);
            dtTOHeader.Columns.Add(IECCode);
            DataTable dtSource = Common.ParameterLookup(Common.ParameterType.Locations, new ParameterFilter("LOCATIONS", 0, 0, 0));
            DataTable dt = dtSource.Clone();
            DataRow[] drSource = dtSource.Select("LocationId = " + m_objTO.SourceLocationId);
            DataRow[] drDest = dtSource.Select("LocationId = " + m_objTO.DestinationLocationId);
            DataRow dRow = dtTOHeader.NewRow();
            dRow["TONumber"] = m_objTO.TNumber;
            dRow["TOINumber"] = m_objTO.TOINumber;
            dRow["SourceLocation"] = txtSourceLocation.Text;
            dRow["SourcePhone"] = m_sbContactNo.ToString();
            dRow["SourceEmail"] = m_objTO.email1;
            dRow["DestinationLocation"] = txtDestinationLocation.Text;
            dRow["SourceAddress"] = m_objTO.SourceAddress;
            dRow["DestinationAddress"] = m_objTO.DestinationAddress;
            dRow["SourceTINNo"] = txtSourceTin.Text;
            dRow["SourceCSTNo"] = txtSourceCST.Text;
            dRow["SourceVATNo"] = txtSourceVAT.Text;
            dRow["DestinationTINNo"] = txtDestTin.Text;
            dRow["DestinationCStNo"] = txtDestCST.Text;
            dRow["DestinationVATNo"] = txtDestVAT.Text;
            dRow["ExpectedDeliveryDate"] = Convert.ToDateTime(m_objTO.ExpectedDeliveryDate).ToString(Common.DTP_DATE_FORMAT);
            dRow["ReferenceNo"] = m_objTO.RefNumber;
            dRow["ShippingDetails"] = m_objTO.ShippingDetails;
            dRow["ShippingWayBillNo"] = m_objTO.ShippingBillNo;
            dRow["ModifiedByName"] = m_objTO.ModifiedByName;
            dRow["ModifiedDate"] = (Convert.ToDateTime(m_objTO.ModifiedDate)).ToString(Common.DTP_DATE_FORMAT);
            dRow["SourceCity"] = drSource[0]["CityName"].ToString();
            dRow["DestinationCity"] = drDest[0]["CityName"].ToString();
            dRow["IsExported"] = chkIsexported.Checked.ToString();
            //dRow["ExportNo"] = m_objTO.ExportNo;
            //dRow["ImportNo"] = m_objTO.ImportNo;
            dRow["IECCode"] = m_objTO.IECCode;


            dtTOHeader.Rows.Add(dRow);
            // Search ItemData for dataTable
            DataTable dtTODetail = new DataTable("TODetail");
            dtTODetail = m_objTO.SearchItemDataTable(m_objTO.TNumber, m_objTO.SourceLocationId);
            for (int i = 0; i < dtTODetail.Rows.Count; i++)
            {
                dtTODetail.Rows[i]["TransferPrice"] = Math.Round(Convert.ToDecimal(dtTODetail.Rows[i]["TransferPrice"]),

                Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                dtTODetail.Rows[i]["TotalAmount"] = Math.Round(Convert.ToDecimal(dtTODetail.Rows[i]["TotalAmount"]),

                Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                dtTODetail.Rows[i]["AfterAdjustQty"] = Math.Round(Convert.ToDecimal(dtTODetail.Rows[i]["AfterAdjustQty"]),

                Common.DisplayQtyRounding, MidpointRounding.AwayFromZero);
            }
            m_printDataSet.Tables.Add(dtTOHeader);
            m_printDataSet.Tables.Add(dtTODetail.Copy());

        }
        /// <summary>
        /// Prints TO Screen report
        /// </summary>
        private void PrintReport()
        {
            CreatePrintDataSet();
            CoreComponent.UI.ReportScreen reportScreenObj = new CoreComponent.UI.ReportScreen((int)Common.ReportType.TO, m_printDataSet);
            reportScreenObj.ShowDialog();
            //reportScreenObj.PrintReport();
            m_printDataSet = null;
        }

        void CreateTOObject()
        {
            if (m_objTO != null && txtTOINumber.Text.Trim().Length > 0 && m_lstTOHeader != null)
            {
                var query = (from p in m_lstTOHeader where p.TOINumber.ToLower().Trim() == txtTOINumber.Text.ToLower().Trim() select p);
                List<TOHeader> lstToHead = new List<TOHeader>();
                lstToHead = query.ToList();

                if (lstToHead != null && lstToHead.Count > 0)
                    m_objTO = lstToHead[0];
            }
            if (m_objTO == null)
            {
                m_objTO = new TOHeader();

                m_objTO.TNumber = Common.INT_DBNULL.ToString();
                m_objTO.CreatedBy = m_userId;
                m_objTO.CreatedDate = System.DateTime.Now.ToString(Common.DATE_TIME_FORMAT);
                m_objTO.IndexSeqNo = Common.INT_DBNULL;
            }
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
                CreateTOObject();

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

                if (m_objTO == null)
                {
                    return;
                }

                if (txtAdjustableQty.Text.Trim().Length > 0 && txtAvailableQty.Text.Trim().Length > 0 && Convert.ToDecimal(txtAdjustableQty.Text.Trim()) > Convert.ToDecimal(txtAvailableQty.Text.Trim()))
                {
                    MessageBox.Show(Common.GetMessage("VAL0022"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                m_validQuantity = true;

                bool b = CheckValidQuantity(m_objTO.StatusId);

                if (m_validQuantity && m_isDuplicateRecordFound <= 0)
                {
                    if (AddItem())
                    {
                        dgvTOItem.DataSource = null;
                        if (m_lstTODetail.Count > 0)
                        {
                            dgvTOItem.DataSource = m_lstTODetail;
                        }
                        ResetItemControl();
                    }
                }
                else if (m_validQuantity == false && b == true)
                {
                    MessageBox.Show(Common.GetMessage("VAL0022"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //txtTotalQty.Text = Toquantity;
            //txtGrossWeight.Text = Grossweight;
            //txtTotalTOAmount.Text = TotalToamont;
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
                Save((int)Common.TOStatus.Created);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Call fn. EditTOI to Edit TO
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvSearchTO_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                EditTOI(e);
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
                if (e.KeyValue == Common.F4KEY && !e.Alt)
                {
                    NameValueCollection nvc = new NameValueCollection();
                    nvc.Add("LocationId", m_currentLocationId.ToString());

                    CoreComponent.Controls.frmSearch objfrmSearch = new CoreComponent.Controls.frmSearch(CoreComponent.Controls.SearchTypes.Item, nvc);
                    ItemDetails _Item = (ItemDetails)objfrmSearch.ReturnObject;
                    objfrmSearch.ShowDialog();
                    _Item = (ItemDetails)objfrmSearch.ReturnObject;

                    if (_Item != null)
                    {
                        txtItemCode.Text = _Item.ItemCode.ToString();
                        //CalculateTransferPrice();     
                        if (m_lstTOFullDetail != null)
                        {
                            var query = (from p in m_lstTOFullDetail where p.ItemCode == txtItemCode.Text.Trim() select p);
                            if (query.ToList().Count > 0)
                            {
                                if (m_toDetail == null)
                                    m_toDetail = new TODetail();

                                m_toDetail.ItemUnitPrice = query.ToList()[0].ItemUnitPrice;
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
                m_toDetail = new TODetail();
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
        private void btnTransferOut_Click(object sender, EventArgs e)
        {
            try
            {
                Save((int)Common.TOStatus.Shipped);
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
                ValidateTOINumber(false);
                StringBuilder sbError = new StringBuilder();
                if (errItem.GetError(txtTOINumber).Trim().Length > 0)
                {
                    sbError.Append(errItem.GetError(txtTOINumber));
                    sbError.AppendLine();
                }

                if (!sbError.ToString().Trim().Equals(string.Empty))
                {
                    MessageBox.Show(sbError.ToString(), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                TOHeader objTO = new TOHeader();
                string errMessage = string.Empty;
                m_lstTODetail = objTO.AdjustItemAndBatch(txtTOINumber.Text.Trim(), m_currentLocationId, ref errMessage);
                m_bucketid = Common.INT_DBNULL;

                if (errMessage == string.Empty && m_lstTODetail != null && m_lstTODetail.Count > 0)
                {
                    dgvTOItem.DataSource = m_lstTODetail;
                    txtTOINumber.Enabled = false;
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
                txtTOINumber.Focus();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Call fn. ValidateTOINumber
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtTOINumber_Validated(object sender, EventArgs e)
        {
            try
            {
                ValidateTOINumber(true);
                Toquantity = txtTotalQty.Text;
                Grossweight = txtGrossWeight.Text;
                TotalToamont = txtTotalTOAmount.Text;
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
                // ValidatePackSize();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Call fn. ValidateExpectedDate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtpExpectedDate_Validated(object sender, EventArgs e)
        {
            try
            {
                //ValidateExpectedDate();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Call fn. ValidateRefNumber
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtRefNumber_Validated(object sender, EventArgs e)
        {
            try
            {
                //ValidateRefNumber();
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
        Boolean ValidateSearchControls()
        {
            errSearch.SetError(dtpSearchShipFrom, string.Empty);
            errSearch.SetError(dtpSearchTOFrom, string.Empty);

            ValidateSearchFromDate();
            ValidateSearchShipDate();

            StringBuilder sbError = new StringBuilder();
            if (errSearch.GetError(dtpSearchTOFrom).Trim().Length > 0)
            {
                sbError.Append(errSearch.GetError(dtpSearchTOFrom));
                sbError.AppendLine();
            }
            if (errSearch.GetError(dtpSearchShipFrom).Trim().Length > 0)
            {
                sbError.Append(errSearch.GetError(dtpSearchShipFrom));
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
        /// Calll fn. SearchTO
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateSearchControls())
                {
                    SearchTO();
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
                //SelectGridRow(e);
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
                    cmbSearchSourceLocation.SelectedValue = m_currentLocationId.ToString();

                chkSearchIndentised.CheckState = CheckState.Indeterminate;
                dgvSearchTO.DataSource = new List<TOHeader>();
                txtSearchTOINumber.Focus();
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
        private void dgvTOItem_SelectionChanged(object sender, EventArgs e)
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
        private void txtTOINumber_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyValue == Common.F4KEY && !e.Alt)
                {
                    ShowTOINumber(txtTOINumber);

                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSearchTOINumber_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyValue == Common.F4KEY && !e.Alt)
                {
                    ShowTOINumber(txtSearchTOINumber);
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
                if (m_objTO != null && m_objTO.StatusId >= (int)Common.TOStatus.Shipped)
                {
                    btnPrint.Enabled = false;
                    PrintReport();
                    btnPrint.Enabled = true;
                }
                else
                    MessageBox.Show(Common.GetMessage("INF0101", "TO", Common.TOStatus.Shipped.ToString()), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                btnPrint.Enabled = true;
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        private void dgvSearchTO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (dgvSearchTO.SelectedRows.Count > 0)
                {
                    string toNumber = Convert.ToString(dgvSearchTO.SelectedRows[0].Cells["TONumber"].Value);

                    var query = (from p in m_lstTOHeader where p.TNumber == toNumber select p);
                    m_objTO = (TOHeader)query.ToList()[0];
                    SelectSearchGrid(toNumber, m_objTO.SourceLocationId);
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtTOINumber_TextChanged(object sender, EventArgs e)
        {

        }


        //bool Search(int fromLocation, int toLocation)
        //{
        //    List<Location> listLocation = new List<Location>();
        //    Location objLocation = new Location();
        //    objLocation.SourceLocationID = fromLocation;
        //    objLocation.DestinationLocationID = toLocation;

        //    //listLocation = objLocation.Search();
        //    bool result = objLocation.Search();
        //    //return listLocation;
        //    return result;
        //}


    }
}
