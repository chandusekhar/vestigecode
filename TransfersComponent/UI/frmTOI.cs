using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections.Specialized;
using System.Reflection;
using CoreComponent.BusinessObjects;
using CoreComponent.Core.BusinessObjects;
using TransfersComponent.BusinessObjects;
using CoreComponent.MasterData.BusinessObjects;
using AuthenticationComponent.BusinessObjects;

namespace TransfersComponent.UI
{
    public partial class frmTOI : CoreComponent.Core.UI.Transaction
    {
        #region Variables
        Boolean m_validQuantity = true;
        Boolean m_validAvailableQuantity = true;
        int m_isDuplicateRecordFound = Common.INT_DBNULL;
        DataTable m_dtDest;
        List<TOI> m_listTOI = new List<TOI>();
        LocationList objLocationlst = new LocationList();
        string m_toiNo = string.Empty;
        int m_itemId = Common.INT_DBNULL;
        TOI m_objTOI;
        int m_RowNo = Common.INT_DBNULL;
        DataTable m_dtSearchAllBucket;
        TOIDetail m_toiDetail;
        List<TOIDetail> m_lstTOIDetail;
        List<int> m_lstRemovedItem = null;
        DataSet m_printDataSet;
        TOI m_objTOI1 = new TOI();
        Progress objPro = new Progress();

        //string m_indentNo = string.Empty;
        int m_selectedItemRowNum = Common.INT_DBNULL;
        int m_UOMId = Common.INT_DBNULL;
        int m_selectedItemRowIndex = Common.INT_DBNULL;
        string m_itemModifiedDate = string.Empty;
        DataTable dtAllSubBuckets;
        List<ItemDetails> lstSourceLocationItem;
        List<ItemDetails> lstDestinationLocationItem;

        private Boolean m_isSaveAvailable = false;
        private Boolean m_isSearchAvailable = false;
        private Boolean m_isConfirmAvailable = false;
        private Boolean m_isCancelAvailable = false;
        private Boolean m_isRejectAvailable = false;
        private Boolean m_isPrintAvailable = false;

        private int m_userId = Authenticate.LoggedInUser.UserId;
        private string strUserName = Authenticate.LoggedInUser.UserName;

        private string strLocationCode = Common.LocationCode;
        private int m_currentLocationId = Common.CurrentLocationId;//Common.LocationHierarchy.HierarchyId;
        private int m_locationType = Common.CurrentLocationTypeId;//Common.LocationHierarchy.HierarchyConfigId;

        private string m_strCtorValue;
        private int m_IsExportValue;

        #endregion Variables
        #region Constants
        private const string Price_Mode = "PriceMode";
        private const string Percentage_Type = "PercentageType";
        private const string App_Depreciation = "AppDepreciation";

        #endregion
        #region C'tor
        public frmTOI()
        {
            try
            {

                m_strCtorValue = string.Empty;
                m_isSaveAvailable = Authenticate.IsFunctionAccessible(strUserName, strLocationCode, TOI.MODULE_CODE, Common.FUNCTIONCODE_SAVE);
                m_isSearchAvailable = Authenticate.IsFunctionAccessible(strUserName, strLocationCode, TOI.MODULE_CODE, Common.FUNCTIONCODE_SEARCH);
                m_isConfirmAvailable = Authenticate.IsFunctionAccessible(strUserName, strLocationCode, TOI.MODULE_CODE, Common.FUNCTIONCODE_CONFIRM);
                m_isCancelAvailable = Authenticate.IsFunctionAccessible(strUserName, strLocationCode, TOI.MODULE_CODE, Common.FUNCTIONCODE_CANCEL);
                m_isRejectAvailable = Authenticate.IsFunctionAccessible(strUserName, strLocationCode, TOI.MODULE_CODE, Common.FUNCTIONCODE_REJECT);
                m_isPrintAvailable = Authenticate.IsFunctionAccessible(strUserName, strLocationCode, TOI.MODULE_CODE, Common.FUNCTIONCODE_PRINT);

                InitializeComponent();

                InitializeControls();

                ResetTOIAndItems();

                ReadOnlyTOIField();

                InitializeDateControl();
                

                lblPageTitle.Text = "Transfer Order Instruction";
                FillBucket(true);
                //set export value
                m_IsExportValue = 0; 
               
                label1.Visible = false;
                chkexport.Visible = false;

                chkIsexported.Visible = false;
                lblexport.Visible = false;
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
        /// <summary>
        /// Bikram:Export Invoice 
        /// </summary>
        public frmTOI(params object[] arr)
        {
            try
            {
                m_strCtorValue = arr[0].ToString();
                m_isSaveAvailable = Authenticate.IsFunctionAccessible(strUserName, strLocationCode, TOI.MODULE_CODE, Common.FUNCTIONCODE_SAVE);
                m_isSearchAvailable = Authenticate.IsFunctionAccessible(strUserName, strLocationCode, TOI.MODULE_CODE, Common.FUNCTIONCODE_SEARCH);
                m_isConfirmAvailable = Authenticate.IsFunctionAccessible(strUserName, strLocationCode, TOI.MODULE_CODE, Common.FUNCTIONCODE_CONFIRM);
                m_isCancelAvailable = Authenticate.IsFunctionAccessible(strUserName, strLocationCode, TOI.MODULE_CODE, Common.FUNCTIONCODE_CANCEL);
                m_isRejectAvailable = Authenticate.IsFunctionAccessible(strUserName, strLocationCode, TOI.MODULE_CODE, Common.FUNCTIONCODE_REJECT);
                m_isPrintAvailable = Authenticate.IsFunctionAccessible(strUserName, strLocationCode, TOI.MODULE_CODE, Common.FUNCTIONCODE_PRINT);
                
                InitializeComponent();

                fncRenameFields();               
                
                InitializeControls();

                //Set Up Grid Value
                foreach (DataGridViewColumn column in dgvSearchTOI.Columns)
                {

                    column.HeaderText = column.HeaderText.Replace("TOI","EOI");
                }


                ResetTOIAndItems();

                ReadOnlyTOIField();

                InitializeDateControl();
                lblPageTitle.Text = "Export Order Instruction";
                FillBucket(true);

                m_IsExportValue = 1;

                label1.Visible = false;
                chkexport.Visible = false;
                chkIsexported.Visible = false;
                lblexport.Visible = false;
                // set proce mode for export invoice
                lblPrice.Visible = false;
                lblPercentage.Visible = false;
                lblAPPDPP.Visible = false;
                cmbPriceMode.Visible = false;
                cmbPercentage.Visible = false;
                cmbAppDpp.Visible = false;
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
        ///
        private void fncRenameFields()
        {
            lblSearchTOINumber.Text = "EOI Number:";
            lblSearchTOIFromDate.Text = "From EOI Date:";
            lblSearchTOICreationDate.Text = "From (EOI Creation) Date:";
            lblSearchTOIToDate.Text="To EOI Date:";
            lblSearchTOICreationDateTO.Text = "To (EOI Creation) Date:";
            lblTOINumber.Text = "EOI Number:";
            lblTOIAmount.Text = "Total EOI Amount:";
            lblTOIQuantity.Text = "Total EOI Quantity:*";
            lblSearchTOICreationDateTO.Text = "To (EOI Creation) Date:";
        }
        #endregion C'tor

        #region Method

        /// <summary>
        /// Initialize Date Picker Value
        /// </summary>
        void InitializeDateControl()
        {
            dtpSearchCreationFrom.CustomFormat = Common.DTP_DATE_FORMAT;
            dtpSearchCreationTO.CustomFormat = Common.DTP_DATE_FORMAT;
            dtpSearchTOIFrom.CustomFormat = Common.DTP_DATE_FORMAT;
            dtpSearchTOITO.CustomFormat = Common.DTP_DATE_FORMAT;
            dtpSearchCreationFrom.Checked = false;
            dtpSearchCreationTO.Checked = false;
            dtpSearchTOIFrom.Checked = false;
            dtpSearchTOITO.Checked = false;
            dtpSearchCreationFrom.Value = Convert.ToDateTime(System.DateTime.Now.ToShortDateString());
            dtpSearchCreationTO.Value = Convert.ToDateTime(System.DateTime.Now.ToShortDateString());
            dtpSearchTOIFrom.Value = Convert.ToDateTime(System.DateTime.Now.ToShortDateString());
            dtpSearchTOITO.Value = Convert.ToDateTime(System.DateTime.Now.ToShortDateString());
        }

        /// <summary>
        /// To Initialize Controls
        /// </summary>
        private void InitializeControls()
        {

            FillSearchStatus();
            FillLocations();
            FillPriceStatus();
            FillPricePercentage();
            FillAppDpp();

            dgvSearchTOI.AutoGenerateColumns = false;
            dgvSearchTOI.DataSource = null;
            DataGridView dgvSearchNew = Common.GetDataGridViewColumns(dgvSearchTOI, Environment.CurrentDirectory + "\\App_Data\\Transfer.xml");

            dgvTOIItem.AutoGenerateColumns = false;
            dgvTOIItem.DataSource = null;
            DataGridView dgvTOIItemNew = Common.GetDataGridViewColumns(dgvTOIItem, Environment.CurrentDirectory + "\\App_Data\\Transfer.xml");                            
            
            txtSearchTOINumber.Focus();
        }

        /// <summary>
        /// Calculate Tranfer Price when Item Code is Entered
        /// </summary>
        void CalculateTransferPrice()
        {
            if (cmbDestinationAddress.SelectedIndex > 0)
            {
                TOI objTOI = new TOI();
                string s = string.Empty;
                decimal price = 0;

                if (m_IsExportValue == 1)
                {
                    price = objTOI.CalTransferPrice(Convert.ToBoolean(m_IsExportValue), txtItemCode.Text, cmbDestinationAddress.SelectedValue.ToString());

                    if (m_toiDetail == null)
                        m_toiDetail = new TOIDetail();

                    m_toiDetail.ItemUnitPrice = price;
                    txtTransferPrice.Text = m_toiDetail.DisplayItemUnitPrice.ToString();
                }
                else
                {
                    if ((cmbPercentage.SelectedIndex == 0))
                    {
                        errItem.SetError(txtItemCode, Common.GetMessage("INF0026", lblPercentage.Text.Trim().Substring(0, lblPercentage.Text.Trim().Length - 2)));
                        txtTransferPrice.Text = string.Empty;
                        return;
                    }
                    else if (cmbPriceMode.SelectedIndex == 0)
                    {
                        errItem.SetError(txtItemCode, Common.GetMessage("INF0026", lblPrice.Text.Trim().Substring(0, lblPrice.Text.Trim().Length - 2)));
                        txtTransferPrice.Text = string.Empty;
                        return;
                    }
                    else if (cmbAppDpp.SelectedIndex == 0)
                    {
                        errItem.SetError(txtItemCode, Common.GetMessage("INF0026", lblAPPDPP.Text.Trim().Substring(0, lblAPPDPP.Text.Trim().Length - 2)));
                        txtTransferPrice.Text = string.Empty;
                        return;
                    }

                    price = objTOI.ToiCalculateTransferPrice(cmbPriceMode.SelectedValue.ToString(), cmbPercentage.SelectedValue.ToString(), cmbAppDpp.SelectedValue.ToString(), txtItemCode.Text, cmbDestinationAddress.SelectedValue.ToString(), ref s);

                    if (m_toiDetail == null)
                        m_toiDetail = new TOIDetail();

                    m_toiDetail.ItemUnitPrice = price;
                    txtTransferPrice.Text = m_toiDetail.DisplayItemUnitPrice.ToString();
                }

            }
            else
            {
                errItem.SetError(txtItemCode, Common.GetMessage("INF0026", lblDestinationLocation.Text.Trim().Substring(0, lblDestinationLocation.Text.Trim().Length - 2)));
                txtTransferPrice.Text = string.Empty;
            }
        }

        bool itemAvailAtDestLoc(string itemCode)
        {
            ItemDetails itemDetails = new ItemDetails();
            itemDetails.LocationId = cmbDestinationAddress.SelectedValue.ToString();

            if(lstDestinationLocationItem == null)
                lstDestinationLocationItem = itemDetails.SearchLocationItem();

            if (lstDestinationLocationItem == null || lstDestinationLocationItem.Count == 0)
                return false;

            return true;

        }
        /// <summary>
        /// Fill Item Price Information into UOM, name of Item
        /// </summary>
        /// <param name="itemCode"></param>
        void FillItemPriceInfo(string itemCode)
        {
            bool invalid = false;
            bool invalidDestLoc = false;
            ItemDetails itemDetails = new ItemDetails();
            itemDetails.LocationId = cmbSourceAddress.SelectedValue.ToString();

            if(lstSourceLocationItem == null)
                lstSourceLocationItem = itemDetails.SearchLocationItem();

            if (!itemAvailAtDestLoc(itemCode))
                invalidDestLoc = true;

            if (!invalidDestLoc)
            {
                var query = from p in lstSourceLocationItem where p.ItemCode.ToLower().Trim() == itemCode.ToLower().Trim() select p;
                if (query.ToList().Count > 0)
                {
                    DataTable dt = Common.ParameterLookup(Common.ParameterType.Item, new ParameterFilter("Item", 0, 0, 0));
                    DataView dv = dt.DefaultView;
                    dv.RowFilter = "ItemCode = '" + itemCode + "'";
                    if (dv.Count > 0)
                    {
                        m_UOMId = Convert.ToInt32(dv[0]["UOMId"]);
                        m_itemId = Convert.ToInt32(dv[0]["ItemId"]);
                        txtItemUOM.Text = dv[0]["UOMName"].ToString();
                        txtItemDescription.Text = dv[0]["ItemName"].ToString();

                        CalculateTransferPrice();
                    }
                    else
                        invalid = true;
                }
                else
                    invalid = true;
            }

            if (invalid)
            {
                if (Convert.ToInt32(cmbSourceAddress.SelectedValue) == Common.INT_DBNULL)
                    errItem.SetError(txtItemCode, Common.GetMessage("VAL0053", lblItemCode.Text.Trim().Substring(0, lblItemCode.Text.Trim().Length - 2)));
                else
                    errItem.SetError(txtItemCode, Common.GetMessage("VAL0006", lblItemCode.Text.Trim().Substring(0, lblItemCode.Text.Trim().Length - 2)));
            }

            if (invalidDestLoc)
                errItem.SetError(txtItemCode, Common.GetMessage("VAL0123"));


            if ((invalidDestLoc == true) || (invalid == true))
            {
                m_UOMId = Common.INT_DBNULL;
                txtItemUOM.Text = string.Empty;
                txtItemDescription.Text = string.Empty;
                txtTransferPrice.Text = string.Empty;
                txtItemTotalAmount.Text = string.Empty;
                txtAvailableQty.Text = string.Empty;
                txtUnitQty.Text = string.Empty;
            }
        }

        /// <summary>
        /// Reset Item Control
        /// </summary>
        private void ResetItemControl()
        {
            m_validQuantity = true;
            m_UOMId = Common.INT_DBNULL;
            m_itemId = Common.INT_DBNULL;
            m_validAvailableQuantity = true;

            txtUnitQty.ReadOnly = false;
            txtItemCode.ReadOnly = false;
            txtUnitQty.Enabled = true;
            cmbBucket.Enabled = true;
            txtItemCode.Enabled = true;

            CoreComponent.Core.BusinessObjects.VisitControls visitControls = new CoreComponent.Core.BusinessObjects.VisitControls();
            visitControls.ResetAllControlsInPanel(errTOI, grpAddDetails);

            dgvTOIItem.ClearSelection();
        }

        /// <summary>
        /// Search TOIs
        /// </summary>
        void Search()
        {
            DateTime DATETIME_MAX = Convert.ToDateTime(new DateTime(2099, 12, 31).ToString());

            DateTime toiFrom = dtpSearchTOIFrom.Checked == true ? Convert.ToDateTime(dtpSearchTOIFrom.Value) : Common.DATETIME_NULL;
            DateTime toiTo = dtpSearchTOITO.Checked == true ? Convert.ToDateTime(dtpSearchTOITO.Value) : DATETIME_MAX;

            DateTime creationTo = dtpSearchCreationTO.Checked == true ? Convert.ToDateTime(dtpSearchCreationTO.Value) : DATETIME_MAX;
            DateTime creationFrom = dtpSearchCreationFrom.Checked == true ? Convert.ToDateTime(dtpSearchCreationFrom.Value) : Common.DATETIME_NULL;
            //int isexported = Convert.ToInt32(chkexport.CheckState);// ? 1 : 0;
            int isexported = m_IsExportValue;
            m_listTOI = Search(txtSearchTOINumber.Text.Trim(), Convert.ToInt32(cmbSearchSourceLocation.SelectedValue), Convert.ToInt32(cmbSearchDestinationLocation.SelectedValue), toiFrom, toiTo, creationFrom, creationTo, Convert.ToInt32(cmbSearchStatus.SelectedValue), isexported);

            if ((m_listTOI != null) && (m_listTOI.Count > 0))
            {
                dgvSearchTOI.DataSource = m_listTOI;
                dgvSearchTOI.ClearSelection();
            }
            else
            {
                dgvSearchTOI.DataSource = new List<TOI>();
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
        List<TOI> Search(string toiNumber, int fromLocation, int toLocation, DateTime toiFromDate, DateTime toiToDate, DateTime creationFromDate, DateTime creationToDate, int status, int isexport)
        {
            List<TOI> listTOI = new List<TOI>();
            TOI objTOI = new TOI();
            objTOI.TNumber = toiNumber;
            objTOI.FromCreationDate = creationFromDate;
            objTOI.ToCreationDate = creationToDate;
            objTOI.StatusId = status;
            objTOI.FromTOIDate = toiFromDate;
            objTOI.ToTOIDate = toiToDate;
            objTOI.SourceLocationId = fromLocation;
            objTOI.DestinationLocationId = toLocation;
            objTOI.Indentised = Convert.ToInt32(chkSearchIndentised.CheckState);// chkSearchIndentised.Checked ? 1 : 0;
            //objTOI.Isexport = Convert.ToInt32(chkexport.CheckState);
            objTOI.Isexport = m_IsExportValue;
            
            listTOI = objTOI.Search();

            return listTOI;
        }

        /// <summary>
        /// Return List of Item Details
        /// </summary>
        /// <param name="TNumber"></param>
        /// <param name="sourceAddressId"></param>
        /// <returns></returns>
        List<TOIDetail> SearchTOIItem(string TNumber, int sourceAddressId)
        {
            m_lstTOIDetail = m_objTOI.SearchItem(TNumber, sourceAddressId);
            return m_lstTOIDetail;
        }

        void SelectSearchGrid(string tNumber, int sourceLocationId)
        {
            m_lstTOIDetail = SearchTOIItem(tNumber, sourceLocationId);

            dgvTOIItem.SelectionChanged -= new System.EventHandler(dgvTOIItem_SelectionChanged);
            dgvTOIItem.DataSource = new List<TOIDetail>();
            if (m_lstTOIDetail != null && m_lstTOIDetail.Count > 0)
            {
                dgvTOIItem.ClearSelection();
                dgvTOIItem.DataSource = m_lstTOIDetail;
                ResetItems();
            }

            tabControlTransaction.TabPages[1].Text = Common.TAB_UPDATE_MODE;
            tabControlTransaction.SelectedIndex = 1;
            m_toiNo = tNumber;
            txtStatus.Text = Enum.GetName(typeof(Common.TOIStatus), m_objTOI.StatusId);
            txtPONumber.Text = m_objTOI.PONumber;
            txtPOStatus.Text = m_objTOI.POStatusName;
            if (m_objTOI.Indentised == Common.INT_DBNULL || m_objTOI.Indentised == 0)
                chkIndentised.Checked = false;
            else
                chkIndentised.Checked = true;

            VisibleButton();
            dgvTOIItem.SelectionChanged += new System.EventHandler(dgvTOIItem_SelectionChanged);
            dgvTOIItem.ClearSelection();

            cmbPercentage.SelectedValue = m_objTOI.Percentage;
            cmbAppDpp.SelectedValue = m_objTOI.AppDep;
            cmbPriceMode.SelectedValue = m_objTOI.PriceMode;

            cmbSourceAddress.SelectedValue = Convert.ToInt32(m_objTOI.SourceLocationId);
            cmbDestinationAddress.SelectedValue = Convert.ToInt32(m_objTOI.DestinationLocationId);
            //chkIsexported.Checked = (m_objTOI.Isexport == 1 ? true : false);
            //chkIsexported.Enabled = (m_objTOI.Isexport == 1 ? false : true);


            GetQuantityAndAmount(m_objTOI);
            txtTOINumber.Text = m_objTOI.TNumber.ToString();
            ReadOnlyTOIField();
        }
        /// <summary>
        /// Edit TOI
        /// </summary>
        /// <param name="e"></param>
        private void EditTOI(DataGridViewCellEventArgs e)
        {
            if ((e.RowIndex >= 0) && (dgvSearchTOI.Rows[e.RowIndex].Cells[e.ColumnIndex].GetType() == typeof(DataGridViewImageCell)))
            {
                m_objTOI = m_listTOI[e.RowIndex];
                SelectSearchGrid(m_objTOI.TNumber, m_objTOI.SourceLocationId);
            }
        }

        void GetQuantityAndAmount(TOI objToi)
        {
            txtTotalQty.Text = objToi.DisplayTotalTOIQuantity.ToString();
            txtTotalTOIAmount.Text = objToi.DisplayTotalTOIAmount.ToString();
        }

        /// <summary>
        /// Show Available Quantity, On Bucket changed
        /// </summary>
        void FillBucketInfo()
        {
            if (txtItemCode.Text.Trim().Length > 0 && cmbBucket.SelectedIndex > 0 && cmbBucket.SelectedIndex > 0)
            {
                DataRow[] dr = m_dtSearchAllBucket.Select("BucketId = " + Convert.ToInt32(cmbBucket.SelectedValue) + " And LocationId = " + Convert.ToInt32(cmbSourceAddress.SelectedValue) + " And ItemCode = '" + txtItemCode.Text.Trim() + "'");
                if (dr.Count() > 0)
                {
                    m_toiDetail.AvailableQty = Convert.ToDecimal(dr[0]["Quantity"]);
                    txtAvailableQty.Text = m_toiDetail.DisplayAvailableQty.ToString();
                }
                else
                {
                    m_toiDetail.AvailableQty = 0;
                    txtAvailableQty.Text = string.Empty;
                }
                txtUnitQty.Text = string.Empty;
            }
        }

        /// <summary>
        /// Validate Combo Box 
        /// </summary>
        /// <param name="cmb"></param>
        /// <param name="lbl"></param>
        private void ValidateCombo(ComboBox cmb, Label lbl)
        {
            if (cmb.SelectedIndex == 0)
                errItem.SetError(cmb, Common.GetMessage("INF0026", lbl.Text.Trim().Substring(0, lbl.Text.Trim().Length - 2)));
            else
                errItem.SetError(cmb, string.Empty);
        }

        /// <summary>
        /// Calll FillBucketInfo function to show available quantity
        /// </summary>
        /// <param name="yesNo"></param>
        void BucketChanged(bool yesNo)
        {
            //if (txtItemDescription.Text.Trim().Length > 0)
            //{
            if (cmbBucket.SelectedIndex > 0)
            {
                errItem.SetError(cmbBucket, string.Empty);
                FillBucketInfo();
            }
            else
            {
                txtAvailableQty.Text = string.Empty;

                if (yesNo == false)
                    errItem.SetError(cmbBucket, Common.GetMessage("INF0026", lblBucket.Text.Trim().Substring(0, lblBucket.Text.Trim().Length - 2)));

            }
            //}
        }

        /// <summary>
        /// Validate Creation from and To Date
        /// </summary>
        void ValidateSearchFromDate()
        {
            if (dtpSearchCreationFrom.Checked == true && dtpSearchCreationTO.Checked == true)
            {
                DateTime fromDate = dtpSearchCreationFrom.Checked == true ? Convert.ToDateTime(dtpSearchCreationFrom.Value) : Common.DATETIME_NULL;
                DateTime toDate = dtpSearchCreationTO.Checked == true ? Convert.ToDateTime(dtpSearchCreationTO.Value) : Common.DATETIME_NULL;

                TimeSpan ts = fromDate - toDate;
                if (ts.Days > 0)
                    errSearch.SetError(dtpSearchCreationFrom, Common.GetMessage("VAL0047", lblSearchTOICreationDateTO.Text.Trim().Substring(0, lblSearchTOICreationDateTO.Text.Trim().Length - 1), lblSearchTOICreationDate.Text.Trim().Substring(0, lblSearchTOICreationDate.Text.Trim().Length - 1)));
                else
                    errSearch.SetError(dtpSearchCreationFrom, string.Empty);
            }
        }

        /// <summary>
        /// Validate TOI From and To Date
        /// </summary>
        void ValidateSearchTOIDate()
        {
            if (dtpSearchTOIFrom.Checked == true && dtpSearchTOITO.Checked == true)
            {
                DateTime fromDate = dtpSearchTOIFrom.Checked == true ? Convert.ToDateTime(dtpSearchTOIFrom.Value) : Common.DATETIME_NULL;
                DateTime toDate = dtpSearchTOITO.Checked == true ? Convert.ToDateTime(dtpSearchTOITO.Value) : Common.DATETIME_NULL;

                TimeSpan ts = fromDate - toDate;
                if (ts.Days > 0)
                    errSearch.SetError(dtpSearchTOIFrom, Common.GetMessage("VAL0047", lblSearchTOIToDate.Text.Trim().Substring(0, lblSearchTOIToDate.Text.Trim().Length - 1), lblSearchTOIFromDate.Text.Trim().Substring(0, lblSearchTOIFromDate.Text.Trim().Length - 1)));
                else
                    errSearch.SetError(dtpSearchTOIFrom, string.Empty);
            }
        }


        List<TOIDetail> CopyTOIDetail(int excludeIndex, List<TOIDetail> lst)
        {
            List<TOIDetail> returnList = new List<TOIDetail>();
            for (int i = 0; i < lst.Count; i++)
            {
                if (i != excludeIndex)
                {
                    TOIDetail tdetail = new TOIDetail();
                    tdetail = lst[i];
                    returnList.Add(tdetail);
                }
            }
            return returnList;
        }

        bool CheckDuplicateItem()
        {
            if (m_lstTOIDetail != null && m_lstTOIDetail.Count > 0)
            {
                List<TOIDetail> toiDetail = CopyTOIDetail(m_selectedItemRowIndex, m_lstTOIDetail);
                //checked based on ItemCode and Bucket Id
                m_isDuplicateRecordFound = (from p in toiDetail where p.ItemCode.Trim().ToLower() == txtItemCode.Text.Trim().ToLower() && p.BucketId == Convert.ToInt32(cmbBucket.SelectedValue) select p).Count();

                if (m_isDuplicateRecordFound > 0)
                {
                    MessageBox.Show(Common.GetMessage("VAL0063", lblItemCode.Text.Trim().Substring(0, lblItemCode.Text.Trim().Length - 2)), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtTotalTOIAmount.Text = string.Empty;
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// Return and calculate total valid quanity is enetered 
        /// </summary>
        /// <returns></returns>
        bool CheckValidTOIQuantityEntered()
        {
            decimal totalAmount = 0;
            decimal totalquantity = 0;
            txtTotalTOIAmount.Text = "0";
            txtTotalQty.Text = "0";

            if (m_lstTOIDetail != null && m_lstTOIDetail.Count > 0)
            {
                for (int i = 0; i < m_lstTOIDetail.Count; i++)
                {
                    if (i == m_selectedItemRowIndex)
                        continue;

                    decimal totQty = Convert.ToDecimal(m_lstTOIDetail[i].UnitQty) * 1;
                    decimal availQty = Convert.ToDecimal(m_lstTOIDetail[i].AvailableQty);

                    if (totQty > availQty)
                    {
                        m_validQuantity = false;
                    }
                    else
                    {

                        totalAmount = totQty * m_lstTOIDetail[i].ItemUnitPrice;
                        m_objTOI.TotalTOIAmount = Convert.ToDecimal(txtTotalTOIAmount.Text) + Math.Round(totalAmount, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                        totalAmount = m_objTOI.TotalTOIAmount;

                        m_objTOI.TotalTOIQuantity = totalquantity + totQty;
                        txtTotalTOIAmount.Text = m_objTOI.DisplayTotalTOIAmount.ToString();
                        totalquantity = totalquantity + totQty;

                        m_objTOI.TotalTOIQuantity = totalquantity;
                        txtTotalQty.Text = m_objTOI.DisplayTotalTOIQuantity.ToString();
                    }
                }

            }

            if (txtAvailableQty.Text.Length == 0)
            {
                m_validAvailableQuantity = false;
                txtItemTotalAmount.Text = string.Empty;
                return false;
            }
            else
            {
                m_validAvailableQuantity = true;
            }
            if (txtUnitQty.Text.Length > 0 && txtAvailableQty.Text.Length > 0)
            {

                decimal totQty = Convert.ToDecimal(txtUnitQty.Text) * 1;
                decimal availQty = m_toiDetail.AvailableQty;

                if (totQty > availQty)
                {
                    m_validQuantity = false;
                }
                else
                {



                    m_toiDetail.UnitQty = Convert.ToDecimal(txtUnitQty.Text);
                    m_toiDetail.ItemTotalAmount = m_toiDetail.ItemUnitPrice * m_toiDetail.UnitQty;

                    totalAmount = totalAmount + totQty * Convert.ToDecimal(m_toiDetail.ItemUnitPrice > 0 ? m_toiDetail.ItemUnitPrice : 0);
                    m_objTOI.TotalTOIAmount = totalAmount;
                    txtTotalTOIAmount.Text = m_objTOI.DisplayTotalTOIAmount.ToString();

                    m_toiDetail.ItemTotalAmount = m_toiDetail.ItemUnitPrice * m_toiDetail.UnitQty;
                    txtItemTotalAmount.Text = m_toiDetail.DisplayItemTotalAmount.ToString();//Math.Round(Convert.ToDecimal(txtTransferPrice.Text.Length > 0 ? txtTransferPrice.Text : "0") * Convert.ToDecimal(txtUnitQty.Text), 2).ToString();

                    totalquantity = totalquantity + totQty;
                    m_objTOI.TotalTOIQuantity = totalquantity;
                    txtTotalQty.Text = m_objTOI.DisplayTotalTOIQuantity.ToString();
                }
            }

            if (m_validQuantity == false)
            {
                txtItemTotalAmount.Text = string.Empty;
                return false;
            }
            return m_validQuantity;
        }

        /// <summary>
        /// Generate string for Error
        /// </summary>
        /// <returns></returns>
        private StringBuilder GenerateError()
        {
            StringBuilder sbError = new StringBuilder();
            if (errTOI.GetError(cmbSourceAddress).Trim().Length > 0)
            {
                sbError.Append(errTOI.GetError(cmbSourceAddress));
                sbError.AppendLine();
            }
            if (errTOI.GetError(cmbDestinationAddress).Trim().Length > 0)
            {
                sbError.Append(errTOI.GetError(cmbDestinationAddress));
                sbError.AppendLine();
            }
            //if (errTOI.GetError(txtTotalQty).Trim().Length > 0)
            //{
            //    sbError.Append(errTOI.GetError(txtTotalQty));
            //    sbError.AppendLine();
            //}

            if (errItem.GetError(txtItemCode).Trim().Length > 0)
            {
                sbError.Append(errItem.GetError(txtItemCode));
                sbError.AppendLine();
            }
            if (errItem.GetError(cmbBucket).Trim().Length > 0)
            {
                sbError.Append(errItem.GetError(cmbBucket));
                sbError.AppendLine();
            }
            if (errItem.GetError(txtAvailableQty).Trim().Length > 0)
            {
                sbError.Append(errItem.GetError(txtAvailableQty));
                sbError.AppendLine();
            }

            if (errItem.GetError(txtUnitQty).Trim().Length > 0)
            {
                sbError.Append(errItem.GetError(txtUnitQty));
                sbError.AppendLine();
            }

            return Common.ReturnErrorMessage(sbError);
        }

        /// <summary>
        /// Add Item on add button click 
        /// </summary>
        /// <returns></returns>
        bool AddItem()
        {
            //m_objTOI.i
            //m_toiDetail = new TOIDetail();
            //m_toiDetail.Isexported = 
            m_toiDetail.IndexSeqNo = Common.INT_DBNULL;
            m_toiDetail.ItemId = m_itemId;
            m_toiDetail.ItemCode = txtItemCode.Text;
            m_toiDetail.ItemName = txtItemDescription.Text;
            m_toiDetail.UOMId = m_UOMId;
            m_toiDetail.UOMName = txtItemUOM.Text; ;
            m_toiDetail.BucketId = Convert.ToInt32(cmbBucket.SelectedValue);
            m_toiDetail.BucketName = cmbBucket.Text;
            //m_toiDetail.IndentNo = String.Empty;
            //m_toiDetail.AvailableQty = Math.Round(Convert.ToDecimal(txtAvailableQty.Text), 2);
            //m_toiDetail.UnitQty = Math.Round(Convert.ToDecimal(txtUnitQty.Text), 2);
            //m_toiDetail.ItemUnitPrice = Math.Round(Convert.ToDecimal(txtTransferPrice.Text), 2);
            //m_toiDetail.ItemTotalAmount = Math.Round(Convert.ToDecimal(txtItemTotalAmount.Text), 2);

            m_toiDetail.RowNo = m_selectedItemRowNum;

            //decimal amt = CalculateTotalItemPrice();

            if (m_lstTOIDetail == null)
                m_lstTOIDetail = new List<TOIDetail>();

            if ((m_selectedItemRowIndex != Common.INT_DBNULL) && (m_selectedItemRowIndex <= dgvTOIItem.Rows.Count))
            {
                m_lstTOIDetail.Insert(m_selectedItemRowIndex, m_toiDetail);
                m_lstTOIDetail.RemoveAt(m_selectedItemRowIndex + 1);
            }
            else
                m_lstTOIDetail.Add(m_toiDetail);

            m_selectedItemRowNum = Common.INT_DBNULL;
            m_UOMId = Common.INT_DBNULL;
            m_toiDetail = null;
            m_selectedItemRowIndex = Common.INT_DBNULL;
            cmbBucket.Enabled = true;
            m_itemId = Common.INT_DBNULL;
            txtItemCode.Enabled = true;
            //m_indentNo = string.Empty;
            return true;
        }

        /// <summary>
        /// Validate Starting Amount
        /// </summary>
        void ValidateQuanity(TextBox txt, Label lbl, ErrorProvider ep, Boolean yesNo)
        {
            bool isValidQuantity = CoreComponent.Core.BusinessObjects.Validators.IsValidQuantity(txt.Text);

            if (isValidQuantity == false && yesNo == false)
                ep.SetError(txt, Common.GetMessage("VAL0009", lbl.Text.Trim().Substring(0, lbl.Text.Trim().Length - 2)));
            else if (isValidQuantity == true && !CheckValidTOIQuantityEntered())
            {
                if (m_validQuantity == false)
                    ep.SetError(txt, Common.GetMessage("VAL0090"));

                if (m_validAvailableQuantity == false)
                {
                    ep.SetError(txtAvailableQty, Common.GetMessage("VAL0006", lblAvailableQty.Text.Trim().Substring(0, lblAvailableQty.Text.Trim().Length - 1)));
                    //ep.SetError(cmbBucket, Common.GetMessage("VAL0006", cmbBucket.Text.Trim().Substring(0, cmbBucket.Text.Trim().Length - 2)));
                }
            }
            else if (Convert.ToDecimal(txt.Text) <= 0)
                ep.SetError(txt, Common.GetMessage("VAL0009", lbl.Text.Trim().Substring(0, lbl.Text.Trim().Length - 2)));
            else
            {
                ep.SetError(txt, string.Empty);
                ep.SetError(txtAvailableQty, string.Empty);
            }
        }

        /// <summary>
        /// Validate Source Address
        /// </summary>
        /// <param name="yesNo"></param>
        void ValidateSourceAddress(bool yesNo)
        {
            if (cmbSourceAddress.SelectedIndex == 0)
            {
                txtSourceAddress.Text = string.Empty;
                if (yesNo == false)
                    errTOI.SetError(cmbSourceAddress, Common.GetMessage("INF0026", lblSearchSourceLocation.Text.Trim().Substring(0, lblSearchSourceLocation.Text.Trim().Length - 1)));
            }
            else
            {
                errTOI.SetError(cmbSourceAddress, string.Empty);
                ShowAddress(cmbSourceAddress, txtSourceAddress);
                //FillBucket(yesNo);
            }
            if (yesNo)
            {
                ResetItemControl();
            }
        }

        /// <summary>
        /// Validate Source Address
        /// </summary>
        /// <param name="yesNo"></param>
        void ValidateDestinationAddress(bool yesNo)
        {
            if (cmbDestinationAddress.SelectedIndex == 0)
            {
                txtDestinationAddress.Text = string.Empty;

                if (yesNo == false)
                    errTOI.SetError(cmbDestinationAddress, Common.GetMessage("INF0026", lblDestinationLocation.Text.Trim().Substring(0, lblDestinationLocation.Text.Trim().Length - 2)));
            }
            else
            {
                errTOI.SetError(cmbDestinationAddress, string.Empty);
                ShowAddress(cmbDestinationAddress, txtDestinationAddress);
            }
            if (yesNo)
            {
                ResetItemControl();
                errItem.Clear();
            }

        }

        /// <summary>
        /// Fill Bucket 
        /// </summary>
        /// <param name="yesNo"></param>
        void FillBucket(bool yesNo)
        {
            if (yesNo)
            {
                if(m_dtSearchAllBucket == null)
                    m_dtSearchAllBucket = Common.ParameterLookup(Common.ParameterType.BucketItemLocation, new ParameterFilter(string.Empty, 0, 0, 0));

                
                if(dtAllSubBuckets == null)
                    dtAllSubBuckets = Common.ParameterLookup(Common.ParameterType.AllSubBuckets, new ParameterFilter(string.Empty, 0, 0, 0));

                cmbBucket.SelectedIndexChanged -= new System.EventHandler(cmbBucket_SelectedIndexChanged);
                cmbBucket.DataSource = dtAllSubBuckets;
                cmbBucket.DisplayMember = "BucketName";
                cmbBucket.ValueMember = "BucketId";
                cmbBucket.SelectedIndexChanged += new System.EventHandler(cmbBucket_SelectedIndexChanged);
            }
        }
        //AKASH
        private void FillPriceStatus()
        {
            try
            {
                string parameterType = Price_Mode;
                DataTable dtStatus = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter(parameterType, 0, 0, 0));

                if (dtStatus != null)
                {
                    cmbPriceMode.DataSource = dtStatus;
                    cmbPriceMode.DisplayMember = Common.KEYVALUE1;
                    cmbPriceMode.ValueMember = Common.KEYCODE1;




                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void FillPricePercentage()
        {
            try
            {
                string parameterType = Percentage_Type;
                DataTable dtStatus = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter(parameterType, 0, 0, 0));
                if (dtStatus != null)
                {
                    cmbPercentage.DataSource = dtStatus;
                    cmbPercentage.DisplayMember = Common.KEYVALUE1;
                    cmbPercentage.ValueMember = Common.KEYCODE1;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void FillAppDpp()
        {
            try
            {
                string parameterType = App_Depreciation;
                DataTable dtStatus = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter(parameterType, 0, 0, 0));
                if (dtStatus != null)
                {
                    cmbAppDpp.DataSource = dtStatus;
                    cmbAppDpp.DisplayMember = Common.KEYVALUE1;
                    cmbAppDpp.ValueMember = Common.KEYCODE1;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //AKASH
        /// <summary>
        /// Fill Search and Update Location Drop Down List
        /// Bikram: Export Invoice
        /// </summary>
        private void FillLocations()
        {
            DataTable dtSearchSource = Common.ParameterLookup(Common.ParameterType.Locations, new ParameterFilter("LOCATIONS", 0, 0, 0));
            if (m_strCtorValue.Equals("EOI", StringComparison.CurrentCultureIgnoreCase))
            {
                IEnumerable<DataRow> dt = from n in dtSearchSource.AsEnumerable()
                                          where n.Field<bool>("IsexportLocation") == true
                                          || n.Field<int>("LocationID") == -1
                                          select n;
                dtSearchSource = dt.CopyToDataTable<DataRow>();
            }
            else
            {
                //IEnumerable<DataRow> dt = from n in dtSearchSource.AsEnumerable()
                //                          where n.Field<string>("CountryName").ToString().ToUpper() == "INDIA"
                //                           || n.Field<int>("LocationID") == -1
                //                          select n;
                IEnumerable<DataRow> dt = from n in dtSearchSource.AsEnumerable()
                                          where n.Field<bool>("IsexportLocation") != true
                                          || n.Field<int>("LocationID") == -1
                                          select n;

                dtSearchSource = dt.CopyToDataTable<DataRow>();
            }
            if (dtSearchSource != null)
            {
                cmbSearchSourceLocation.DataSource = dtSearchSource;
                cmbSearchSourceLocation.DisplayMember = "DisplayName";
                cmbSearchSourceLocation.ValueMember = "LocationId";
            }

            DataTable m_dtSearchSource = Common.ParameterLookup(Common.ParameterType.Locations, new ParameterFilter("LOCATIONS", 0, 0, 0));
            if (m_strCtorValue.Equals("EOI", StringComparison.CurrentCultureIgnoreCase))
            {
                IEnumerable<DataRow> dt = from n in dtSearchSource.AsEnumerable()
                                          where n.Field<bool>("IsexportLocation") == true
                                          || n.Field<int>("LocationID") == -1
                                          select n;
                m_dtSearchSource = dt.CopyToDataTable<DataRow>();
            }
            else
            {
                //IEnumerable<DataRow> dt = from n in dtSearchSource.AsEnumerable()
                //                          where n.Field<string>("CountryName").ToString().ToUpper() == "INDIA"                                          
                //                          || n.Field<int>("LocationID") == -1
                //                          select n;
                IEnumerable<DataRow> dt = from n in dtSearchSource.AsEnumerable()
                                          where n.Field<bool>("IsexportLocation") != true
                                          || n.Field<int>("LocationID") == -1
                                          select n;

                m_dtSearchSource = dt.CopyToDataTable<DataRow>();
            }

            DataRow[] dr = m_dtSearchSource.Select("LocationId = " + Common.INT_DBNULL);
            dr[0]["DisplayName"] = "Select";
            if (m_dtSearchSource != null)
            {
                cmbSourceAddress.DataSource = m_dtSearchSource;
                cmbSourceAddress.DisplayMember = "DisplayName";
                cmbSourceAddress.ValueMember = "LocationId";
            }
            
            DataTable dtSearchDest = Common.ParameterLookup(Common.ParameterType.Locations, new ParameterFilter("LOCATIONS", 0, 0, 0));
            if (m_strCtorValue.Equals("EOI", StringComparison.CurrentCultureIgnoreCase))
            {
                IEnumerable<DataRow> dt = from n in dtSearchDest.AsEnumerable()
                                          where n.Field<bool>("IsexportLocation") ==true
                                          || n.Field<int>("LocationID") == -1
                                          select n;
                dtSearchDest = dt.CopyToDataTable<DataRow>();
            } 
            else
            {
                //IEnumerable<DataRow> dt = from n in dtSearchDest.AsEnumerable()
                //                          where n.Field<string>("CountryName").ToString().ToUpper() == "INDIA"               
                //                          || n.Field<int>("LocationID") == -1
                //                          select n;
                IEnumerable<DataRow> dt = from n in dtSearchSource.AsEnumerable()
                                          where n.Field<bool>("IsexportLocation") != true
                                          || n.Field<int>("LocationID") == -1
                                          select n;

                dtSearchDest = dt.CopyToDataTable<DataRow>();
            } 
            if (dtSearchDest != null)
            {
                cmbSearchDestinationLocation.DataSource = dtSearchDest;
                cmbSearchDestinationLocation.DisplayMember = "DisplayName";
                cmbSearchDestinationLocation.ValueMember = "LocationId";

            }

            m_dtDest = Common.ParameterLookup(Common.ParameterType.Locations, new ParameterFilter("LOCATIONS", 0, 0, 0));
            if (m_strCtorValue.Equals("EOI", StringComparison.CurrentCultureIgnoreCase))
            {
                IEnumerable<DataRow> dt = from n in dtSearchDest.AsEnumerable()
                                          where n.Field<bool>("IsexportLocation") == true
                                          || n.Field<int>("LocationID") == -1
                                          select n;
                m_dtDest = dt.CopyToDataTable<DataRow>();
            }
            else
            {
                //IEnumerable<DataRow> dt = from n in dtSearchDest.AsEnumerable()
                //                          where n.Field<string>("CountryName").ToString().ToUpper() == "INDIA"                                                         
                //                          || n.Field<int>("LocationID") == -1
                //                          select n;
                IEnumerable<DataRow> dt = from n in dtSearchSource.AsEnumerable()
                                          where n.Field<bool>("IsexportLocation") != true
                                          || n.Field<int>("LocationID") == -1
                                          select n;


                m_dtDest = dt.CopyToDataTable<DataRow>();
            }
            DataRow[] dr1 = m_dtDest.Select("LocationId = " + Common.INT_DBNULL);
            dr1[0]["DisplayName"] = "Select";
            if (m_dtDest != null)
            {
                cmbDestinationAddress.DataSource = m_dtDest;
                cmbDestinationAddress.DisplayMember = "DisplayName";
                cmbDestinationAddress.ValueMember = "LocationId";
            }
        }

        /// <summary>
        /// Fill Status Drop Down List
        /// </summary>
        private void FillSearchStatus()
        {
            DataTable dt = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter("TOIStatus", 0, 0, 0));
            cmbSearchStatus.DataSource = dt;
            cmbSearchStatus.DisplayMember = Common.KEYVALUE1;
            cmbSearchStatus.ValueMember = Common.KEYCODE1;
        }

        /// <summary>
        /// Show Address, for Source and Destination Location Changed
        /// </summary>
        /// <param name="cmb"></param>
        /// <param name="txt"></param>
        void ShowAddress(ComboBox cmb, TextBox txt)
        {
            if (cmb.SelectedIndex > 0 && m_dtDest != null)
            {
                DataRow[] dr = m_dtDest.Select("LocationId = " + Convert.ToInt32(cmb.SelectedValue));
                txt.Text = dr[0]["Address"].ToString();
            }
        }

        void ValidatedItemCode(Boolean yesNo)
        {
            bool isTextBoxEmpty = CoreComponent.Core.BusinessObjects.Validators.CheckForEmptyString(txtItemCode.Text.Length);


            if (isTextBoxEmpty == true)
            {
                txtItemDescription.Text = string.Empty;
                txtItemUOM.Text = string.Empty;
                txtItemTotalAmount.Text = string.Empty;
                txtTransferPrice.Text = string.Empty;
                txtAvailableQty.Text = string.Empty;
                txtUnitQty.Text = string.Empty;

                if (yesNo == false)
                    errItem.SetError(txtItemCode, Common.GetMessage("INF0019", lblItemCode.Text.Trim().Substring(0, lblItemCode.Text.Trim().Length - 2)));
                else
                    errItem.SetError(txtItemCode, string.Empty);
            }
            else if (isTextBoxEmpty == false)
            {
                errItem.SetError(txtItemCode, string.Empty);

                if (yesNo == true && cmbSourceAddress.SelectedIndex > 0 && txtItemCode.Text.Length > 0)
                {
                    cmbBucket.SelectedIndex = 0;
                    //cmbBucket.SelectedIndexChanged -= new System.EventHandler(cmbBucket_SelectedIndexChanged);
                    //FillBucket(true);
                    //cmbBucket.SelectedIndexChanged += new System.EventHandler(cmbBucket_SelectedIndexChanged);
                }
                   FillItemPriceInfo(txtItemCode.Text.Trim());
            }
        }



        /// <summary>
        /// Validate Text box fields
        /// </summary>
        /// <param name="txt"></param>
        /// <param name="lbl"></param>
        void ValidatedText(TextBox txt, Label lbl)
        {
            bool isTextBoxEmpty = CoreComponent.Core.BusinessObjects.Validators.CheckForEmptyString(txt.Text.Length);
            if (isTextBoxEmpty == true)
                errItem.SetError(txt, Common.GetMessage("INF0019", lbl.Text.Trim().Substring(0, lbl.Text.Trim().Length - 2)));
            else if (isTextBoxEmpty == false)
                errItem.SetError(txt, string.Empty);
        }

        /// <summary>
        /// Make read Only fields
        /// </summary>
        void ReadOnlyTOIField()
        {
            if ((m_lstTOIDetail != null && m_lstTOIDetail.Count > 0) && m_IsExportValue == 0)
            {
                cmbSourceAddress.Enabled = false;
                cmbDestinationAddress.Enabled = false;
                txtTotalQty.ReadOnly = true;
                //chkIsexported.Enabled = false;
                cmbPriceMode.Enabled = false;
                cmbPercentage.Enabled = false;
                cmbAppDpp.Enabled = false;                
            }
            else if ((m_lstTOIDetail != null && m_lstTOIDetail.Count > 0) && m_IsExportValue == 1)
            {
                cmbSourceAddress.Enabled = false;
                cmbDestinationAddress.Enabled = false;
                txtTotalQty.ReadOnly = true;
                //chkIsexported.Enabled = false;
                cmbPriceMode.Visible = false;
                cmbPercentage.Visible = false;
                cmbAppDpp.Visible = false;
                
            }            
            if (m_lstTOIDetail != null && m_lstTOIDetail.Count == 0 && m_IsExportValue==0)
            {
                cmbSourceAddress.Enabled = true;
                cmbDestinationAddress.Enabled = true;
                //chkIsexported.Enabled = true;
                txtTotalQty.ReadOnly = false;
                txtTotalTOIAmount.Text = string.Empty;
                cmbPriceMode.Enabled = true;
                cmbPercentage.Enabled = true;
                cmbAppDpp.Enabled = true;
            }
            else if (m_lstTOIDetail != null && m_lstTOIDetail.Count == 0 && m_IsExportValue == 1)
            {
                cmbSourceAddress.Enabled = true;
                cmbDestinationAddress.Enabled = true;
                //chkIsexported.Enabled = true;
                txtTotalQty.ReadOnly = false;
                txtTotalTOIAmount.Text = string.Empty;
                cmbPriceMode.Visible = false;
                cmbPercentage.Visible = false;
                cmbAppDpp.Visible = false;
            }

            if ((m_objTOI != null) && (m_objTOI.Indentised == 1 || m_objTOI.StatusId == (int)Common.TOIStatus.Confirmed))
            {
                txtItemCode.Enabled = false;
                cmbBucket.Enabled = false;
                txtUnitQty.Enabled = false;
                btnAddDetails.Enabled = false;
            }
            if ((m_objTOI == null) || (m_objTOI != null && m_objTOI.StatusId < (int)Common.TOIStatus.Confirmed))
            {
                // FillStatus();
                // cmbStatus.Enabled = false;
            }
        }

        /// <summary>
        /// Remove Location Contact Record
        /// </summary>
        /// <param name="e"></param>
        void RemoveTOIItem(DataGridViewCellEventArgs e)
        {
            if ((e.RowIndex >= 0) && (dgvTOIItem.Rows[e.RowIndex].Cells[e.ColumnIndex].GetType() == typeof(DataGridViewImageCell)))
            {

                if (m_objTOI != null && m_objTOI.Indentised == 1)
                {
                    MessageBox.Show(Common.GetMessage("VAL0065"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }


                if ((m_objTOI == null) || (m_objTOI != null && (Convert.ToInt32(m_objTOI.StatusId) == (int)Common.TOIStatus.New || Convert.ToInt32(m_objTOI.StatusId) == (int)Common.TOIStatus.Created)))
                {
                    DialogResult saveResult = MessageBox.Show(Common.GetMessage("INF0028"), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (saveResult == DialogResult.Yes)
                    {
                        if (m_lstRemovedItem == null)
                            m_lstRemovedItem = new List<int>();

                        if (m_lstTOIDetail.Count > 0)
                        {
                            m_lstRemovedItem.Add(m_lstTOIDetail[e.RowIndex].RowNo);

                            dgvTOIItem.DataSource = null;
                            m_lstTOIDetail.RemoveAt(e.RowIndex);
                            dgvTOIItem.DataSource = m_lstTOIDetail;
                            ResetItems();
                        }

                        ReadOnlyTOIField();
                        bool b = CheckValidTOIQuantityEntered();
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
        /// Add Item Details when Add button is clicked
        /// </summary>
        void AddDetails()
        {            
            CreateTOIObject();

            errItem.SetError(cmbSourceAddress, string.Empty);
            errItem.SetError(cmbDestinationAddress, string.Empty);
            errItem.SetError(txtItemCode, string.Empty);
            errItem.SetError(cmbBucket, string.Empty);
            errItem.SetError(txtUnitQty, string.Empty);

            m_validQuantity = true;
            m_isDuplicateRecordFound = Common.INT_DBNULL;
            //ValidateQuanity(txtTotalQty, lblTOIQuantity, errTOI);
            ValidatedItemCode(false);
            ValidateQuanity(txtUnitQty, lblUnitQty, errItem, false);
            ValidateSourceAddress(false);
            // ValidateDestinationAddress(false);
            if (m_validQuantity && m_validAvailableQuantity)
                BucketChanged(false);

            #region Check Location Errors

            StringBuilder sbError = new StringBuilder();
            sbError = GenerateError();
            #endregion

            if (CheckDuplicateItem() == false)
                return;

            if (!sbError.ToString().Trim().Equals(string.Empty))
            {
                MessageBox.Show(sbError.ToString(), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (cmbSourceAddress.SelectedValue.ToString() == cmbDestinationAddress.SelectedValue.ToString())
            {
                MessageBox.Show(Common.GetMessage("VAL0097"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (m_validQuantity && m_isDuplicateRecordFound <= 0)
            {
                if (AddItem())
                {
                    ReadOnlyTOIField();
                    dgvTOIItem.DataSource = null;
                    if (m_lstTOIDetail.Count > 0)
                    {
                        dgvTOIItem.DataSource = null;
                        dgvTOIItem.DataSource = m_lstTOIDetail;
                    }
                    //chkIsexported.Enabled = (m_objTOI1.Isexport == 1 ? false : true);

                    ResetItems();
                    EmptyErrorProvider();
                }
            }
        }

        void EmptyErrorProvider()
        {
            errItem.SetError(cmbSourceAddress, string.Empty);
            errItem.SetError(cmbDestinationAddress, string.Empty);
            errItem.SetError(txtItemCode, string.Empty);
            errItem.SetError(cmbBucket, string.Empty);
            errItem.SetError(txtUnitQty, string.Empty);
        }

        void CreateTOIObject()
        {
            if (m_objTOI != null && m_toiNo != string.Empty)
            {
                var query = (from p in m_listTOI where p.TNumber == m_toiNo select p);
                m_objTOI = query.ToList()[0];
                m_objTOI.IndexSeqNo = 1;
                m_objTOI.CreatedBy = Common.INT_DBNULL;
                m_objTOI.CreatedDate = string.Empty;

            }
            if (m_objTOI == null)
            {
                m_objTOI = new TOI();
                m_objTOI.TNumber = string.Empty;
                m_objTOI.StatusId = (int)Common.TOIStatus.Created;

                m_objTOI.CreatedBy = m_userId;
                m_objTOI.CreatedDate = System.DateTime.Now.ToString(Common.DATE_TIME_FORMAT);
                m_objTOI.IndexSeqNo = Common.INT_DBNULL;
            }
        }

        void Save(int statusId)
        {
            // Confirmation Before Saving
            //DialogResult saveResult = MessageBox.Show(Common.GetMessage("INF0027"), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            MemberInfo[] memberInfos = typeof(Common.TOIStatus).GetMembers(BindingFlags.Public | BindingFlags.Static);
            // Confirmation Before Saving, statusId
            DialogResult saveResult = MessageBox.Show(Common.GetMessage("5010", Common.GetConfirmationStatusText(memberInfos[statusId].Name)), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (saveResult == DialogResult.Yes)
            {               
                CreateTOIObject();
                if (m_objTOI != null)
                {
                    if (m_objTOI.ModifiedDate.Length > 0)
                        m_objTOI.ModifiedDate = Convert.ToDateTime(m_objTOI.ModifiedDate).ToString(Common.DATE_TIME_FORMAT);//.ToString(Common.DATE_TIME_FORMAT);
                }

                m_objTOI.PriceMode= cmbPriceMode.SelectedValue.ToString();
                m_objTOI.AppDep = cmbAppDpp.SelectedValue.ToString();
                m_objTOI.Percentage= cmbPercentage.SelectedValue.ToString();

                m_objTOI.SourceLocationId = Convert.ToInt32(cmbSourceAddress.SelectedValue);
                m_objTOI.SourceAddress = txtSourceAddress.Text;
                m_objTOI.DestinationAddress = txtDestinationAddress.Text;
                m_objTOI.DestinationLocationId = Convert.ToInt32(cmbDestinationAddress.SelectedValue);

                //m_objTOI.TotalTOIQuantity = Convert.ToDecimal(txtTotalQty.Text);
                //m_objTOI.TotalTOIAmount = Convert.ToDecimal(txtTotalTOIAmount.Text);
                //m_objTOI.TotalTOIAmount = Convert.ToDecimal(txtTotalTOIAmount.Text);
                m_objTOI.Indentised = 0;
                if (m_strCtorValue.Equals("EOI"))
                {
                    m_objTOI.Isexport = 1;
                }
                else
                {
                    m_objTOI.Isexport = 0;
                }

                if (m_strCtorValue.Equals("EOI"))
                {
                    m_objTOI.Isexported = true;
                }
                else
                {
                    m_objTOI.Isexported = false;                
                }

                // Get Location Code From User Object.
                m_objTOI.LocationId = m_currentLocationId;
                m_objTOI.ModifiedBy = m_userId;

                m_objTOI.StatusId = statusId;

                m_objTOI.TOIItems = m_lstTOIDetail;
                m_objTOI.RemoveItems = m_lstRemovedItem;

                //List<TOI> lstTOI = new List<TOI>();
                //lstTOI.Add(m_objTOI);

                string errorMessage1 = string.Empty;
                string errorMessage = string.Empty;
                //funOpenPopUP objPopUp = new funOpenPopUP(OpenPopUP);
                //IAsyncResult objRes = objPopUp.BeginInvoke(null, null);
                //System.Threading.Thread.Sleep(10000);
                //bool result = m_objTOI.Save(Common.ToXml(m_objTOI), ref errorMessage);
                ////Close the PopUP
                //objPro.Invoke(new Action(objPro.Close));
                //bool result = m_objTOI.Save(Common.ToXml(m_objTOI), ref errorMessage);
                funOpenPopUP objPopUp = new funOpenPopUP(m_objTOI.Save);
                IAsyncResult objRes = objPopUp.BeginInvoke(Common.ToXml(m_objTOI), ref errorMessage, null, null);
                objPro = new Progress();
                objPro.MdiParent = this.ParentForm;
                objPro.FormBorderStyle = FormBorderStyle.None;
                objPro.StartPosition = FormStartPosition.CenterScreen;
                objPro.Show();
                bool result = objPopUp.EndInvoke(ref errorMessage, objRes);
                objPro.Close();
                if (errorMessage.Equals(string.Empty))
                {
                    //ResetItems();
                    VisibleButton();
                    GetTOIStatus(m_objTOI.TNumber);
                    dgvTOIItem.DataSource = new List<TOIDetail>();
                    if (m_lstTOIDetail != null && m_lstTOIDetail.Count > 0)
                    {
                        m_lstTOIDetail = SearchTOIItem(m_objTOI.TNumber, m_objTOI.SourceLocationId);
                        dgvTOIItem.DataSource = m_lstTOIDetail;
                        ResetItems();
                        txtTOINumber.Text = m_objTOI.TNumber.ToString();
                    }

                    //MessageBox.Show(Common.GetMessage("8001"),Common.GetMessage("10001"),MessageBoxButtons.OK,MessageBoxIcon.Information);
                    MessageBox.Show(Common.GetMessage("8013", memberInfos[statusId].Name, m_objTOI.TNumber), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(Common.GetMessage(errorMessage), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        delegate bool funOpenPopUP(string strXml,ref string strOut);
        private void OpenPopUP()
        {
            try
            {
                objPro = new Progress();
                objPro.StartPosition = FormStartPosition.CenterParent;
                objPro.MdiParent = this.Owner;
                objPro.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// Return TOI Status 
        /// </summary>
        /// <param name="TOINumber"></param>
        /// <returns></returns>
        void GetTOIStatus(string TOINumber)
        {
            List<TOI> toiHeader = new List<TOI>();
            toiHeader = Search(TOINumber, Common.INT_DBNULL, Common.INT_DBNULL, Common.DATETIME_NULL, Common.DATETIME_MAX, Common.DATETIME_NULL, Common.DATETIME_NULL, Common.INT_DBNULL, Common.INT_DBNULL);
            //toHeader = SearchTO(TOINumber);
            if (toiHeader != null && toiHeader.Count > 0)
            {
                txtStatus.Text = toiHeader[0].StatusName;
                // return toiHeader[0].StatusId;
            }
            else
            {
                txtStatus.Text = Common.TOStatus.New.ToString();
                // return (int)Common.TOStatus.New;
            }
        }
        /// <summary>
        /// Reset Item Controls
        /// </summary>
        void ResetItems()
        {
            CoreComponent.Core.BusinessObjects.VisitControls visitControlsItems = new CoreComponent.Core.BusinessObjects.VisitControls();
            visitControlsItems.ResetAllControlsInPanel(errItem, grpAddDetails);

            m_selectedItemRowIndex = Common.INT_DBNULL;
            m_selectedItemRowNum = Common.INT_DBNULL;
            m_UOMId = Common.INT_DBNULL;
            m_toiDetail = null;
            AddItem_SelectOne(cmbBucket);
            cmbBucket.Enabled = true;
            txtItemCode.Enabled = true;
            txtUnitQty.Enabled = true;

            dgvTOIItem.ClearSelection();
            EmptyErrorProvider();
            ReadOnlyTOIField();
            txtItemCode.Focus();
            FillBucket(true);
        }

        /// <summary>
        /// Invisible Controls
        /// </summary>
        void InvisibleButton()
        {
            btnConfirm.Enabled = false;
            btnCancel.Enabled = false;
            btnReject.Enabled = false;
            btnSave.Enabled = false;
            btnAddDetails.Enabled = false;

        }
        /// <summary>
        /// Visible Controls 
        /// </summary>
        void VisibleButton()
        {
            InvisibleButton();

            if (m_objTOI == null)
            {
                btnSave.Enabled = true;
                btnAddDetails.Enabled = true;
            }
            else if (m_objTOI != null && (m_objTOI.StatusId == (int)Common.TOIStatus.Cancelled || m_objTOI.StatusId == (int)Common.TOIStatus.Rejected))
            {

            }
            else if (m_objTOI != null && m_objTOI.StatusId == (int)Common.TOIStatus.Confirmed)
            {
                //if (m_lstTOIDetail != null && m_lstTOIDetail[0].IndentNo.Trim().Length <= 0)
                if (m_objTOI != null && m_objTOI.Indentised == 0)
                    btnReject.Enabled = true;
            }
            else if (m_objTOI != null && m_objTOI.StatusId == (int)Common.TOIStatus.Created)
            {
                if (m_objTOI.Indentised == 0)
                {
                    btnCancel.Enabled = true;
                    btnSave.Enabled = true;
                    btnAddDetails.Enabled = true;
                }
                btnConfirm.Enabled = true;
            }

            btnSave.Enabled = btnSave.Enabled & m_isSaveAvailable;
            btnConfirm.Enabled = btnConfirm.Enabled & m_isConfirmAvailable;
            btnReject.Enabled = btnReject.Enabled & m_isRejectAvailable;
            btnCancel.Enabled = btnCancel.Enabled & m_isCancelAvailable;
            btnSearch.Enabled = m_isSearchAvailable;
            btnPrint.Enabled = m_isPrintAvailable;
        }

        /// <summary>
        /// Adding Blank Item into List
        /// </summary>
        /// <param name="cmb"></param>
        private void AddItem_SelectOne(ComboBox cmb)
        {
            DataTable dtSelectOne = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter(Common.INT_DBNULL.ToString(), 0, 0, 0));
            cmb.DataSource = dtSelectOne;
            cmb.ValueMember = Common.KEYCODE1.ToString();
            cmb.DisplayMember = Common.KEYVALUE1;
        }

        /// <summary>
        /// This function is used to show data in controls
        /// </summary>
        /// <param name="e"></param>
        private void SelectGridRow(EventArgs e)
        {
            if (dgvTOIItem.SelectedCells.Count > 0)
            {
                int rowIndex = dgvTOIItem.SelectedCells[0].RowIndex;
                int columnIndex = dgvTOIItem.SelectedCells[0].ColumnIndex;

                if (rowIndex >= 0 && columnIndex >= 0)
                {
                    m_isDuplicateRecordFound = Common.INT_DBNULL;
                    m_validQuantity = true;

                    string itemCode = dgvTOIItem.Rows[rowIndex].Cells["ItemCode"].Value.ToString().Trim();
                    int bucktId = Convert.ToInt32(dgvTOIItem.Rows[rowIndex].Cells["BucketId"].Value);

                    //m_RowNo = Convert.ToInt32(dgvTOIItem.Rows[e.RowIndex].Cells["RowNo"].Value.ToString().Trim());

                    ////Get ParentId
                    if (m_lstTOIDetail == null)
                        return;

                    var itemSelect = (from p in m_lstTOIDetail where p.ItemCode == itemCode && p.BucketId == bucktId select p);

                    if (itemSelect.ToList().Count == 0)
                        return;


                    m_toiDetail = itemSelect.ToList()[0];
                    //m_indentNo = m_toiDetail.IndentNo;
                    //txtIndentNo.Text = m_indentNo;
                    txtItemCode.Text = m_toiDetail.ItemCode;
                    txtItemDescription.Text = m_toiDetail.ItemName;
                    txtItemUOM.Text = m_toiDetail.UOMName;

                    txtTransferPrice.Text = m_toiDetail.DisplayItemUnitPrice.ToString();
                    txtItemTotalAmount.Text = m_toiDetail.DisplayItemTotalAmount.ToString();

                    FillBucket(true);
                    cmbBucket.SelectedValue = Convert.ToInt32(m_toiDetail.BucketId);
                    txtUnitQty.Text = m_toiDetail.DisplayUnitQty.ToString();
                    //cmbBucket.Enabled = false;
                    //txtItemCode.Enabled = false;

                    if ((m_objTOI == null) || (m_objTOI.StatusId == (int)Common.TOIStatus.Created || m_objTOI.StatusId == (int)Common.TOIStatus.New))
                        txtUnitQty.Enabled = true;
                    else
                        txtUnitQty.Enabled = false;

                    m_selectedItemRowNum = m_toiDetail.RowNo;
                    m_selectedItemRowIndex = rowIndex;
                    m_itemModifiedDate = m_toiDetail.ModifiedDate;
                    ReadOnlyTOIField();
                    EmptyErrorProvider();
                }
            }
        }
        /// <summary>
        /// Reset Controls for TOI and Items
        /// </summary>
        void ResetTOIAndItems()
        {
            CoreComponent.Core.BusinessObjects.VisitControls visitControls = new CoreComponent.Core.BusinessObjects.VisitControls();
            visitControls.ResetAllControlsInPanel(errTOI, pnlCreateHeader);
            cmbDestinationAddress.Enabled = true;
            cmbSourceAddress.Enabled = true;
            txtStatus.Text = Enum.GetName(typeof(Common.TOIStatus), Common.INT_DBNULL);
            m_lstTOIDetail = new List<TOIDetail>();
            m_objTOI = null;
            m_toiNo = string.Empty;
            ResetItems();
            dgvTOIItem.DataSource = new List<TODetail>();
            VisibleButton();
            cmbSourceAddress.Focus();
        }

        void tabControlSelect(TabControlCancelEventArgs e)
        {
            if ((tabControlTransaction.SelectedIndex == 0) && (cmbSourceAddress.SelectedIndex > 0 || cmbDestinationAddress.SelectedIndex > 0))
            {

                DialogResult result = MessageBox.Show(Common.GetMessage("VAL0026"), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Cancel | DialogResult.No == result)
                {
                    tabControlTransaction.SelectedIndex = 0;
                    e.Cancel = true;
                }
                else if (result == DialogResult.Yes)
                {
                    ResetTOIAndItems();
                    tabControlTransaction.TabPages[1].Text = Common.TAB_CREATE_MODE;
                    ReadOnlyTOIField();
                }
            }
            else if (tabControlTransaction.SelectedIndex == 1)
            {

                if (tabControlTransaction.TabPages[1].Text == Common.TAB_CREATE_MODE)
                {
                    ResetTOIAndItems();
                    VisibleButton();
                }

            }
        }

        private void CreatePrintDataSet()
        {
            m_printDataSet = new DataSet();
            // Get Data For TOI Header Informaton in TOI Screen Report
            DataTable dtTOIHeader = new DataTable("TOIHeader");
            DataColumn TOINumber = new DataColumn("TOINumber", System.Type.GetType("System.String"));
            DataColumn SourceLocation = new DataColumn("SourceLocation", System.Type.GetType("System.String"));
            DataColumn Status = new DataColumn("Status", System.Type.GetType("System.String"));
            DataColumn DestinationLocation = new DataColumn("DestinationLocation", System.Type.GetType("System.String"));
            DataColumn SourceAddress = new DataColumn("SourceAddress", System.Type.GetType("System.String"));
            DataColumn TotalTOIQuantity = new DataColumn("TotalTOIQuantity", System.Type.GetType("System.String"));
            DataColumn DestinationAddress = new DataColumn("DestinationAddress", System.Type.GetType("System.String"));
            DataColumn TotalTOIAmount = new DataColumn("TotalTOIAmount", System.Type.GetType("System.String"));
            DataColumn PONumber = new DataColumn("PONumber", System.Type.GetType("System.String"));
            DataColumn POStatus = new DataColumn("POStatus", System.Type.GetType("System.String"));
            DataColumn Indentised = new DataColumn("Indentised", System.Type.GetType("System.String"));
            DataColumn Isexported = new DataColumn("Isexported", System.Type.GetType("System.String"));
            DataColumn CreatedBy = new DataColumn("CreatedBy", System.Type.GetType("System.String"));
            dtTOIHeader.Columns.Add(TOINumber);
            dtTOIHeader.Columns.Add(SourceLocation);
            dtTOIHeader.Columns.Add(Status);
            dtTOIHeader.Columns.Add(DestinationLocation);
            dtTOIHeader.Columns.Add(SourceAddress);
            dtTOIHeader.Columns.Add(TotalTOIQuantity);
            dtTOIHeader.Columns.Add(DestinationAddress);
            dtTOIHeader.Columns.Add(TotalTOIAmount);
            dtTOIHeader.Columns.Add(PONumber);
            dtTOIHeader.Columns.Add(POStatus);
            dtTOIHeader.Columns.Add(Indentised);
            dtTOIHeader.Columns.Add(Isexported);
            dtTOIHeader.Columns.Add(CreatedBy);
            DataRow dRow = dtTOIHeader.NewRow();
            dRow["TOINumber"] = m_objTOI.TNumber;
            dRow["SourceLocation"] = cmbSourceAddress.Text;
            dRow["Status"] = Enum.GetName(typeof(Common.TOIStatus), m_objTOI.StatusId);
            dRow["DestinationLocation"] = cmbDestinationAddress.Text;
            dRow["SourceAddress"] = m_objTOI.SourceAddress;
            dRow["TotalTOIQuantity"] = Math.Round(m_objTOI.TotalTOIQuantity, Common.DisplayQtyRounding);
            dRow["DestinationAddress"] = m_objTOI.DestinationAddress;
            dRow["TotalTOIAmount"] = Math.Round(m_objTOI.TotalTOIAmount, Common.DisplayAmountRounding);
            dRow["PONumber"] = m_objTOI.PONumber;
            dRow["POStatus"] = m_objTOI.POStatusName;
            dRow["Indentised"] = (m_objTOI.Indentised == 1 ? "Yes" : "No");
            dRow["Isexported"] = (m_objTOI.Isexport == 1 ? "true" : "false");
            dRow["CreatedBy"] = m_objTOI.TOICreatedBY;
            dtTOIHeader.Rows.Add(dRow);
            // Search ItemData for dataTable
            DataTable dtTOIDetail = new DataTable("TOIDetail");
            dtTOIDetail = m_objTOI.SearchItemDataTable(m_objTOI.TNumber, m_objTOI.SourceLocationId);
            for (int i = 0; i < dtTOIDetail.Rows.Count; i++)
            {
                dtTOIDetail.Rows[i]["TransferPrice"] = Math.Round(Convert.ToDecimal(dtTOIDetail.Rows[i]["TransferPrice"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                dtTOIDetail.Rows[i]["AvailableQty"] = Math.Round(Convert.ToDecimal(dtTOIDetail.Rows[i]["AvailableQty"]), Common.DisplayQtyRounding, MidpointRounding.AwayFromZero);
                dtTOIDetail.Rows[i]["ItemQuantity"] = Math.Round(Convert.ToDecimal(dtTOIDetail.Rows[i]["ItemQuantity"]), Common.DisplayQtyRounding, MidpointRounding.AwayFromZero);
                dtTOIDetail.Rows[i]["TotalAmount"] = Math.Round(Convert.ToDecimal(dtTOIDetail.Rows[i]["TotalAmount"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
            }
            m_printDataSet.Tables.Add(dtTOIHeader);
            m_printDataSet.Tables.Add(dtTOIDetail.Copy());

        }

        private void PrintReport()
        {
            CreatePrintDataSet();
            CoreComponent.UI.ReportScreen reportScreenObj;
            if (m_strCtorValue == "EOI")
            {
                reportScreenObj = new CoreComponent.UI.ReportScreen((int)Common.ReportType.EOI, m_printDataSet);

            }
            else
            {
                reportScreenObj = new CoreComponent.UI.ReportScreen((int)Common.ReportType.TOI, m_printDataSet);
            }
            reportScreenObj.ShowDialog();
            //reportScreenObj.PrintReport();
            m_printDataSet = null;
        }

        #endregion

        #region Event

        Boolean ValidateSearchControls()
        {
            errSearch.SetError(dtpSearchCreationFrom, string.Empty);
            errSearch.SetError(dtpSearchTOIFrom, string.Empty);

            ValidateSearchFromDate();
            ValidateSearchTOIDate();

            StringBuilder sbError = new StringBuilder();
            if (errSearch.GetError(dtpSearchCreationFrom).Trim().Length > 0)
            {
                sbError.Append(errSearch.GetError(dtpSearchCreationFrom));
                sbError.AppendLine();
            }
            if (errSearch.GetError(dtpSearchTOIFrom).Trim().Length > 0)
            {
                sbError.Append(errSearch.GetError(dtpSearchTOIFrom));
                sbError.AppendLine();
            }

            if (!sbError.ToString().Trim().Equals(string.Empty))
            {
                MessageBox.Show(sbError.ToString(), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Call Search function 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateSearchControls())
                {

                    m_listTOI = null;
                    Search();
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Edit TOI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvSearchTOI_CellContentClick(object sender, DataGridViewCellEventArgs e)
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
        /// Call function AddDetails
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddDetails_Click(object sender, EventArgs e)
        {
            try
            {
                AddDetails();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Call function BucketChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Call function ValidateSourceAddress
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbSourceAddress_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ValidateSourceAddress(true);


                if ((cmbSourceAddress.SelectedIndex) > 0 && (cmbDestinationAddress.SelectedIndex > 0))
                {
                    bool result = objLocationlst.Search(Convert.ToInt32(cmbSourceAddress.SelectedValue), Convert.ToInt32(cmbDestinationAddress.SelectedValue));

                    if (result)
                    {
                        //chkIsexported.Checked = true;                        
                        m_objTOI1.Isexport = 1;
                        //chkIsexported.Enabled = false;
                    }
                    else
                    {
                        //chkIsexported.Checked = false;
                        m_objTOI1.Isexport = 0;
                        //chkIsexported.Enabled = true;
                    }
                }
                else
                    return;
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Call function ValidateDestinationAddress
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbDestinationAddress_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //m_objTOI = new TOI();
                ValidateDestinationAddress(true);
                if ((cmbSourceAddress.SelectedIndex) > 0 && (cmbDestinationAddress.SelectedIndex > 0))
                {
                    bool result = objLocationlst.Search(Convert.ToInt32(cmbSourceAddress.SelectedValue), Convert.ToInt32(cmbDestinationAddress.SelectedValue));

                    if (result)
                    {
                        //chkIsexported.Checked = true;
                        m_objTOI1.Isexport = 1;
                        //chkIsexported.Enabled = false;
                    }
                    else
                    {
                        //chkIsexported.Checked = false;
                        m_objTOI1.Isexport = 0;
                        //chkIsexported.Enabled = true;
                    }
                }
                else
                    return;

            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Call function ValidatedText
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtItemCode_Validated(object sender, EventArgs e)
        {
            try
            {
                m_toiDetail = new TOIDetail();
                ValidatedItemCode(true);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Call function ValidateQuanity
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtUnitQty_Validated(object sender, EventArgs e)
        {
            try
            {
                // ValidateQuanity(txtUnitQty, lblUnitQty, errItem, true);
                errItem.SetError(txtUnitQty, string.Empty);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Call function ValidateQuanity
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtTotalQty_Validated(object sender, EventArgs e)
        {
            try
            {
                //ValidateQuanity(txtTotalQty, lblTOIQuantity, errTOI);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Remove TOI Item Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvTOIItem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                RemoveTOIItem(e);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Save TOI and Items in Created Mode
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_lstTOIDetail == null || m_lstTOIDetail.Count == 0)
                {
                    MessageBox.Show(Common.GetMessage("VAL0024", "item"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                EmptyErrorProvider();
                ValidateSourceAddress(false);
                ValidateDestinationAddress(false);

                #region Check Location Errors
                StringBuilder sbError = new StringBuilder();
                sbError = GenerateError();
                #endregion

                Save((int)Common.TOIStatus.Created);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Reject TOI and Item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReject_Click(object sender, EventArgs e)
        {
            try
            {
                //if m_objTOI.Indentised
                Save((int)Common.TOIStatus.Rejected);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Cancel TOI 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Save((int)Common.TOIStatus.Cancelled);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Confirm TOI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_lstTOIDetail.Count == 0)
                {
                    MessageBox.Show(Common.GetMessage("VAL0024", "item"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (m_objTOI != null && m_objTOI.Indentised == 1 && m_objTOI.POStatus < (int)Common.POStatus.Recieved)
                {
                    MessageBox.Show(Common.GetMessage("VAL0067"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                Save((int)Common.TOIStatus.Confirmed);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Call function ResetTOIAndItems
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreateReset_Click(object sender, EventArgs e)
        {
            try
            {
                tabControlTransaction.TabPages[1].Text = Common.TAB_CREATE_MODE;
                ResetTOIAndItems();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Call function ResetItems
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearDetails_Click(object sender, EventArgs e)
        {
            try
            {
                ResetItems();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Call function tabControlSelect
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
        /// Call function SelectGridRow
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvTOIItem_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                // SelectGridRow(e);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Reset Search Controls
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
                chkSearchIndentised.CheckState = CheckState.Indeterminate;
                dgvSearchTOI.DataSource = new List<TOI>();
                txtSearchTOINumber.Focus();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Open Pop up when f4 is pressed
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
                    nvc.Add("LocationId", cmbSourceAddress.SelectedValue.ToString());

                    CoreComponent.Controls.frmSearch objfrmSearch = new CoreComponent.Controls.frmSearch(CoreComponent.Controls.SearchTypes.Item, nvc);
                    ItemDetails _Item = (ItemDetails)objfrmSearch.ReturnObject;
                    objfrmSearch.ShowDialog();
                    _Item = (ItemDetails)objfrmSearch.ReturnObject;
                    if (_Item != null)
                    {
                        txtItemCode.Text = _Item.ItemCode.ToString();

                        //FillBucket(true);
                        //CalculateTransferPrice();
                        FillItemPriceInfo(txtItemCode.Text.Trim());
                    }
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void dgvTOIItem_SelectionChanged(object sender, EventArgs e)
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

        private void txtUnitQty_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtUnitQty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                bool isValidQuantity = CoreComponent.Core.BusinessObjects.Validators.IsValidQuantity(txtUnitQty.Text);

                if (isValidQuantity == true && txtUnitQty.Text.Length > 0 && txtAvailableQty.Text.Length > 0)
                {
                    //m_toiDetail.UnitQty = Convert.ToDecimal(txtUnitQty.Text);
                    //m_toiDetail.ItemTotalAmount = m_toiDetail.ItemUnitPrice * m_toiDetail.UnitQty;
                    //txtItemTotalAmount.Text = m_toiDetail.DisplayItemTotalAmount.ToString();
                    if (m_toiDetail != null)
                        txtItemTotalAmount.Text = Math.Round((m_toiDetail.ItemUnitPrice * Convert.ToDecimal(txtUnitQty.Text)), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero).ToString();

                    //txtItemTotalAmount.Text = (Convert.ToDecimal(txtTransferPrice.Text) * Convert.ToDecimal(txtUnitQty.Text)).ToString();
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
                if (m_objTOI != null && m_objTOI.StatusId >= (int)Common.TOIStatus.Confirmed)
                {
                    btnPrint.Enabled = false;
                    PrintReport();
                    btnPrint.Enabled = true;
                }
                else
                    MessageBox.Show(Common.GetMessage("INF0101", "Instruction", Common.TOIStatus.Confirmed.ToString()), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                btnPrint.Enabled = true;
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        private void dgvSearchTOI_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (dgvSearchTOI.SelectedRows.Count > 0)
                {
                    string toiNumber = Convert.ToString(dgvSearchTOI.SelectedRows[0].Cells["TOINumber"].Value);

                    var query = (from p in m_listTOI where p.TNumber == toiNumber select p);
                    m_objTOI = (TOI)query.ToList()[0];
                    SelectSearchGrid(toiNumber, m_objTOI.SourceLocationId);
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void chkIsexported_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkIsexported.Checked == true)
                {
                    cmbPriceMode.SelectedIndex = 0;
                    cmbPercentage.SelectedIndex = 0;
                    cmbAppDpp.SelectedIndex = 0;
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
