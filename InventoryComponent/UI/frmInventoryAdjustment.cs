using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using CoreComponent.Controls;
using CoreComponent.Core.BusinessObjects;
using CoreComponent.Hierarchies.BusinessObjects;
using CoreComponent.MasterData.BusinessObjects;
using System.Collections.Specialized;
using AuthenticationComponent.BusinessObjects;
using InventoryComponent.BusinessObjects;

namespace InventoryComponent.UI
{
    public partial class frmInventoryAdjustment : CoreComponent.Core.UI.Transaction
    {
        #region Variables
        Boolean m_bLoading = true;
        Boolean m_F4Press = false;
        int m_toBucketid = Common.INT_DBNULL;
        int m_fromBucketid = Common.INT_DBNULL;
        string m_fromBucketName = string.Empty;
        string m_toBucketName = string.Empty;
        string m_reasonDescription = string.Empty;
        int batchExist = Common.INT_DBNULL;

        public int availquan
        {
            get;
            set;
        }
        public static DataTable SelectReasoncode
        {
            get;
            set;
        }
        public static DataTable GenericReasoncode
        {
            get;
            set;
        }
        public int BatchExist
        {
            get { return batchExist; }
            set { batchExist = value; }
        }

        int m_UOMId = Common.INT_DBNULL;
        decimal m_weight = 0;
        string m_UOMName = string.Empty;
        int m_sourceLocationId = Common.INT_DBNULL;

        List<InventoryAdjust> m_lstInvHeader;
        List<ItemInventory> m_lstInvDetail;
        ItemInventory m_itemDetail;
        InventoryAdjust m_objInventoryAdj;
        string m_seqNo = Common.INT_DBNULL.ToString();
        int m_selectedItemRowNum = Common.INT_DBNULL;
        int m_selectedItemRowIndex = Common.INT_DBNULL;
        int m_itemId = Common.INT_DBNULL;
        DataTable m_dtLocation;
        string m_batchNo = string.Empty;
        string m_batchNoM = string.Empty;
        string m_mfgbatchNo = string.Empty;
        int m_isDuplicateRecordFound = Common.INT_DBNULL;
        private DataSet m_printDataSet = null;
        private int m_ToitemID = Common.INT_DBNULL;
        private int m_ToUomid = Common.INT_DBNULL;
        decimal m_ToWeight = 0;
        string m_ToUoMName;
        string m_Mfgdate = string.Empty;
        string m_Expdate = string.Empty;
        string m_TobatchNo = string.Empty;
        string m_TomfgbatchNo = string.Empty;

        string m_mfd = string.Empty;
        string m_epd = string.Empty;

        #region Premission Check
        private Boolean m_isSaveAvailable = false;
        private Boolean m_isSearchAvailable = false;
        private Boolean m_isInitiatedAvailable = false;
        private Boolean m_isApprovedAvailable = false;
        private Boolean m_isRejectAvailable = false;
        private Boolean m_isCancelAvailable = false;

        private int m_userId = Authenticate.LoggedInUser.UserId;
        private string strUserName = Authenticate.LoggedInUser.UserName;

        private string strLocationCode = Common.LocationCode;
        private int m_currentLocationId = Common.CurrentLocationId;
        private int m_locationType = Common.CurrentLocationTypeId;
        #endregion

        #endregion Variables

        #region C'tor
        public frmInventoryAdjustment()
        {
            try
            {
                InitializeComponent();

                InitializeControls();
                lblPageTitle.Text = "Stock Adjustment";
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Methods

        /// <summary>
        /// Initialize Controls
        /// </summary>
        void InitializeControls()
        {
            CheckRights();
            GridInitialize();
            ResetInventoryAndItems();
            // cmbLocation.SelectedIndexChanged -= new EventHandler(cmbLocation_SelectedIndexChanged);
            cmbFromBucket.SelectedIndexChanged -= new EventHandler(cmbFromBucket_SelectedIndexChanged);
            FillLocations();
            FillBucket(true, false);
            //cmbLocation.SelectedIndexChanged += new EventHandler(cmbLocation_SelectedIndexChanged);
            cmbFromBucket.SelectedIndexChanged += new EventHandler(cmbFromBucket_SelectedIndexChanged);
            FillSearchStatus();
            InitializeDateControl();
            FillUsers();
            FillReasonCode();

            EnableDisableButton((int)Common.InventoryStatus.New);
            ReadOnlyItem();


            if (m_locationType == (int)Common.LocationConfigId.HO)
            {
                lblSearchApprovedBy.Visible = true;
                cmbApprovedBy.Visible = true;
            }
        }

        /// <summary>
        /// To Check premission
        /// </summary>
        void CheckRights()
        {
            m_isSaveAvailable = Authenticate.IsFunctionAccessible(strUserName, strLocationCode, InventoryAdjust.MODULE_CODE, Common.FUNCTIONCODE_SAVE);
            m_isSearchAvailable = Authenticate.IsFunctionAccessible(strUserName, strLocationCode, InventoryAdjust.MODULE_CODE, Common.FUNCTIONCODE_SEARCH);
            m_isInitiatedAvailable = Authenticate.IsFunctionAccessible(strUserName, strLocationCode, InventoryAdjust.MODULE_CODE, Common.FUNCTIONCODE_INITIATE);
            m_isApprovedAvailable = Authenticate.IsFunctionAccessible(strUserName, strLocationCode, InventoryAdjust.MODULE_CODE, Common.FUNCTIONCODE_APPROVE);
            m_isRejectAvailable = Authenticate.IsFunctionAccessible(strUserName, strLocationCode, InventoryAdjust.MODULE_CODE, Common.FUNCTIONCODE_REJECT);
            m_isCancelAvailable = Authenticate.IsFunctionAccessible(strUserName, strLocationCode, InventoryAdjust.MODULE_CODE, Common.FUNCTIONCODE_CANCEL);
        }

        /// <summary>
        /// Fill Reason Code
        /// </summary>
        void FillReasonCode()
        {
            DataRow[] dtRows = null;
            DataTable dt = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter("ReasonCode", 0, 0, 0));
            SelectReasoncode = dt;
            //if (!chkExportIn.Checked)
            //{
            //    DataTable dts = new DataTable();
            //DataRow[] dtRows = null;
            //    dtRows = dt.Select("KeyCode1 <> 10");
            //    if (dtRows != null)
            //    {


            //        foreach (DataRow drRow in dtRows)
            //        {
            //            //dts.Rows.Add(drRow);
            //            dts.ImportRow(drRow);
            //            dts.AcceptChanges();
            //        }

            //    }
            //}
            //if (!chkExportIn.Enabled)
            //{
            //    dtRows = dt.Select("KeyCode1 <> 10");
            //    dt.Clear();
            //    if (dtRows != null)
            //    {
            //        foreach (DataRow drRow in dtRows)
            //        {
            //            dt.ImportRow(drRow);
            //        }
            //    }
            //}

            cmbReasonCode.DataSource = dt;
            cmbReasonCode.DisplayMember = Common.KEYVALUE1;
            cmbReasonCode.ValueMember = Common.KEYCODE1;
        }

        /// <summary>
        /// Grid Initialize
        /// </summary>
        void GridInitialize()
        {
            dgvInventory.AutoGenerateColumns = false;
            dgvInventory.DataSource = null;
            DataGridView dgvInventoryNew = Common.GetDataGridViewColumns(dgvInventory, Environment.CurrentDirectory + "\\App_Data\\Inventory.xml");

            dgvInventoryItem.AutoGenerateColumns = false;
            dgvInventoryItem.DataSource = null;

            DataGridView dgvdgvStockItemNew = Common.GetDataGridViewColumns(dgvInventoryItem, Environment.CurrentDirectory + "\\App_Data\\Inventory.xml");
            dgvdgvStockItemNew.AutoGenerateColumns = false;
            dgvdgvStockItemNew.DataSource = null;

        }

        /// <summary>
        /// Fill Bucket 
        /// </summary>
        /// <param name="yesNo"></param>
        void FillBucket(bool yesNo, bool editMode)
        {
            try
            {
                if (yesNo)
                {
                    DataView dv = new DataView();
                    DataView dvTo = new DataView();
                    DataTable dt = new DataTable();
                    DataTable m_dtSearchAllBucket;

                    if (editMode)
                        m_dtSearchAllBucket = Common.ParameterLookup(Common.ParameterType.AllSubBuckets, new ParameterFilter(string.Empty, 0, 0, 0));
                    else
                        m_dtSearchAllBucket = Common.ParameterLookup(Common.ParameterType.InventoryBucketBatchLocation, new ParameterFilter(string.Empty, 0, 0, 0));


                    if (m_dtSearchAllBucket != null && m_dtSearchAllBucket.Rows.Count > 0)
                    {
                        //m_dtSearchAllBucket = m_dtSearchAllBucket.AsEnumerable().Distinct().CopyToDataTable();
                        //dt = m_dtSearchAllBucket.Select("((LocationId = '" + Convert.ToInt32(cmbLocation.SelectedValue).ToString() + "') And ItemCode ='" + txtItemCode.Text.Trim() + "') OR ItemCode='-1'").AsEnumerable().Distinct().CopyToDataTable();
                        dv = new DataView(m_dtSearchAllBucket.DefaultView.ToTable(true, "BucketId", "BucketName"));
                        //dvTo = new DataView(dt.DefaultView.ToTable(true, "BucketId", "BucketName"));
                    }
                    //DataView dv = m_dtSearchAllBucket.DefaultView;
                    if (dv != null && dv.Count > 0)
                    {
                        m_bLoading = false;
                        cmbFromBucket.DataSource = dv;
                        cmbFromBucket.DisplayMember = "BucketName";
                        cmbFromBucket.ValueMember = "BucketId";


                        m_dtSearchAllBucket = Common.ParameterLookup(Common.ParameterType.AllSubBuckets, new ParameterFilter(string.Empty, 0, 0, 0));
                        cmbToBucket.DataSource = m_dtSearchAllBucket;
                        cmbToBucket.DisplayMember = "BucketName";
                        cmbToBucket.ValueMember = "BucketId";
                    }
                }
            }
            finally
            {
                m_bLoading = true;
            }
        }

        /// <summary>
        /// Fill Users
        /// </summary>
        void FillUsers()
        {
            DataTable dtUser = Common.ParameterLookup(Common.ParameterType.Users, new ParameterFilter("", 0, 0, 0));
            if (dtUser != null)
            {
                cmbApprovedBy.DataSource = dtUser;
                cmbApprovedBy.DisplayMember = "Name";
                cmbApprovedBy.ValueMember = "UserId";
            }
        }

        /// <summary>
        /// Fill Search and Update Location Drop Down List
        /// </summary>
        void FillLocations()
        {
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
        }

        /// <summary>
        /// Fill Item Price Information into UOM, name of Item
        /// </summary>
        /// <param name="itemCode"></param>
        void FillItemPriceInfo(string itemCode, Boolean yesNo, bool bFromItem)
        {
            bool invalid = false;

            ItemDetails itemDetails = new ItemDetails();
            itemDetails.LocationId = Convert.ToInt32(cmbLocation.SelectedValue).ToString();

            List<ItemDetails> lst = new List<ItemDetails>();
            if (!bFromItem)
                itemDetails.FromItemCode = txtItemCode.Text;
            itemDetails.ToItemCode = txtToitemcode.Text;
            lst = itemDetails.SearchLocationItem();

            var query = from p in lst where p.ItemCode.ToLower().Trim() == itemCode.ToLower().Trim() select p;
            if (query.ToList().Count > 0)
            {
                DataTable dt = Common.ParameterLookup(Common.ParameterType.Item, new ParameterFilter("Item", 0, 0, 0));
                DataView dv = dt.DefaultView;
                dv.RowFilter = "ItemCode = '" + itemCode + "'";
                if (dv.Count > 0)
                {
                    if (bFromItem)
                    {
                        m_itemId = Convert.ToInt32(dv[0]["ItemId"]);
                        m_UOMId = Convert.ToInt32(dv[0]["UOMId"]);
                        m_weight = Convert.ToDecimal(dv[0]["Weight"]);
                        m_UOMName = dv[0]["UOMName"].ToString();
                        txtWeight.Text = Math.Round(m_weight, 2).ToString();
                        //txtUOMName.Text = dv[0]["UOMName"].ToString();
                        txtItemDescription.Text = dv[0]["ItemName"].ToString();
                    }
                    else
                    {
                        m_ToitemID = Convert.ToInt32(dv[0]["ItemId"]);
                        m_ToUomid = Convert.ToInt32(dv[0]["UOMId"]);
                        m_ToWeight = Convert.ToDecimal(dv[0]["Weight"]);
                        m_ToUoMName = dv[0]["UOMName"].ToString();
                        txtToWeight.Text = Math.Round(m_weight, 2).ToString();
                        //txtToUOM.Text = dv[0]["UOMName"].ToString();
                        txtToItemDesc.Text = dv[0]["ItemName"].ToString();
                        //txtToBatchNo.Text = txtBatchNo.Text;
                        btnbatch.Enabled = true;
                    }
                    FillBucket(yesNo, false);
                }
                else
                    invalid = true;
            }
            else
                if (!chkbatchadj.Checked)
                    invalid = true;

            if (invalid == true)
            {
                if (bFromItem)
                {
                    errInventory.SetError(txtItemCode, Common.GetMessage("VAL0006", lblItemCode.Text.Trim().Substring(0, lblItemCode.Text.Trim().Length - 2)));
                    txtWeight.Text = string.Empty;
                    //txtUOMName.Text = string.Empty;
                    txtItemDescription.Text = string.Empty;
                    m_UOMName = string.Empty;
                    m_UOMId = Common.INT_DBNULL;
                    m_itemId = Common.INT_DBNULL;
                    m_weight = 0;
                }
                else
                {
                    errInventory.SetError(txtToitemcode, Common.GetMessage("VAL0006", lblToitemcode.Text.Trim().Substring(0, lblToitemcode.Text.Trim().Length - 2)));
                    txtToWeight.Text = string.Empty;
                    //txtToUOM.Text = string.Empty;
                    txtToItemDesc.Text = string.Empty;
                    m_ToUoMName = string.Empty;
                    m_ToUomid = Common.INT_DBNULL;
                    m_ToitemID = Common.INT_DBNULL;
                    m_ToWeight = 0;
                    btnbatch.Enabled = false;
                }
            }
        }

        ///// <summary>
        ///// Fill Status Drop Down List
        ///// </summary>
        private void FillSearchStatus()
        {
            DataTable dt = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter("InventoryStatus", 0, 0, 0));
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
            dtpDate.CustomFormat = Common.DTP_DATE_FORMAT;
            dtpDate.Checked = false;
            dtpSearchFrom.Checked = false;
            dtpSearchTo.Checked = false;

            dtpSearchFrom.Value = Convert.ToDateTime(System.DateTime.Now.ToShortDateString());
            dtpSearchTo.Value = Convert.ToDateTime(System.DateTime.Now.ToShortDateString());
            dtpDate.Value = Convert.ToDateTime(System.DateTime.Now.ToShortDateString());
        }

        /// <summary>
        /// Validate Source Address
        /// </summary>
        /// <param name="yesNo"></param>
        void ValidateLocationAddress(bool yesNo)
        {
            if (cmbLocation.SelectedIndex == 0)
            {
                txtLocationAddress.Text = string.Empty;
                txtLocationCode.Text = string.Empty;

                if (yesNo == false)
                    errInventory.SetError(cmbLocation, Common.GetMessage("INF0026", lblLocation.Text.Trim().Substring(0, lblLocation.Text.Trim().Length - 1)));
            }
            else
            {
                errInventory.SetError(cmbLocation, string.Empty);
                ShowAddress();
                FillBucket(yesNo, false);
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
                txtLocationAddress.Text = dr[0]["Address"].ToString();
                txtLocationCode.Text = dr[0]["LocationCode"].ToString();
            }
            else
            {
                txtLocationCode.Text = string.Empty;
                txtLocationAddress.Text = string.Empty;
            }
        }

        /// <summary>
        /// Clear Batch Info
        /// </summary>
        void ClearBatchAndQuantityInfo(bool bFlag)
        {
            if (m_bLoading && bFlag)
            {

                //txtBatchNo.Text = string.Empty;
                txtAvailableQty.Text = string.Empty;
                txtQty.Text = string.Empty;
                txtApprovedQty.Text = string.Empty;
                txtReasonDescription.Text = string.Empty;
                m_batchNo = string.Empty;
                m_batchNoM = string.Empty;
                //m_TobatchNo = string.Empty;
                //txtToBatchNo.Text = string.Empty;

                //if (!chkExportIn.Checked)
                // cmbReasonCode.SelectedIndex = 0;

                if (cmbFromBucket.SelectedIndex > 0)
                    errInventory.SetError(cmbFromBucket, string.Empty);
            }
        }

        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtItemCode_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox objtxtBox = null;
            bool bFlag = true;
            try
            {
                if (e.KeyValue == Common.F4KEY && !e.Alt)
                {
                    objtxtBox = (sender) as TextBox;
                    NameValueCollection nvc = new NameValueCollection();
                    nvc.Add("LocationId", Convert.ToInt32(cmbLocation.SelectedValue).ToString());

                    CoreComponent.Controls.frmSearch objfrmSearch = new CoreComponent.Controls.frmSearch(CoreComponent.Controls.SearchTypes.Item, nvc);
                    if (objtxtBox.Tag != null)
                    {
                        objfrmSearch.ItemCodeMapping = true;
                        objfrmSearch.FromItemCode = txtItemCode.Text;
                    }
                    ItemDetails _Item = (ItemDetails)objfrmSearch.ReturnObject;
                    objfrmSearch.ShowDialog();
                    _Item = (ItemDetails)objfrmSearch.ReturnObject;
                    if (_Item != null)
                    {

                        if (objtxtBox != null && objtxtBox.Tag == null)
                            txtItemCode.Text = _Item.ItemCode.ToString();
                        else
                        {
                            txtToitemcode.Text = _Item.ItemCode.ToString();
                            bFlag = false;
                        }
                        FillItemPriceInfo(txtItemCode.Text.Trim(), true, bFlag);
                        ClearBatchAndQuantityInfo(bFlag);

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
        /// Clear Item Info
        /// </summary>
        void ClearItemInfo(bool bFromItem)
        {
            if (bFromItem)
            {
                txtWeight.Text = string.Empty;
                //txtUOMName.Text = string.Empty;
                txtItemDescription.Text = string.Empty;
                m_UOMName = string.Empty;
                m_UOMId = Common.INT_DBNULL;
                m_itemId = Common.INT_DBNULL;
                m_weight = 0;
            }
            else
            {
                txtToWeight.Text = string.Empty;
                //txtToUOM.Text = string.Empty;
                txtToItemDesc.Text = string.Empty;
                m_ToUoMName = string.Empty;
                m_ToUomid = Common.INT_DBNULL;
                m_ToitemID = Common.INT_DBNULL;
                m_ToWeight = 0;
            }

        }

        /// <summary>
        /// Validate Item Code
        /// </summary>
        /// <param name="yesNo"></param>
        void ValidateItemCode(Boolean yesNo, TextBox objtxt)
        {
            bool bFlag = true;
            if (objtxt.Tag != null)
                bFlag = false;
            bool isTextBoxEmpty = CoreComponent.Core.BusinessObjects.Validators.CheckForEmptyString(objtxt.Text.Trim().Length);
            if (isTextBoxEmpty == true)
            {
                if (yesNo == false)
                {
                    if (bFlag)
                        errInventory.SetError(objtxt, Common.GetMessage("INF0019", lblItemCode.Text.Trim().Substring(0, lblItemCode.Text.Trim().Length - 2)));
                    else
                        errInventory.SetError(objtxt, Common.GetMessage("INF0019", lblToitemcode.Text.Trim().Substring(0, lblToitemcode.Text.Trim().Length - 2)));
                }
                else
                    errInventory.SetError(objtxt, string.Empty);

                ClearItemInfo(bFlag);
                ClearBatchAndQuantityInfo(bFlag);
            }
            else if (isTextBoxEmpty == false)
            {
                if (chkbatchadj.Checked == true && txtItemCode.Text == txtToitemcode.Text)
                    errInventory.SetError(objtxt, string.Empty);
                if (yesNo)
                {
                    errInventory.SetError(objtxt, string.Empty);
                    FillBucket(true, false);

                    ClearBatchAndQuantityInfo(bFlag);
                }

                FillItemPriceInfo(objtxt.Text.Trim(), yesNo, bFlag);
                //if (txtItemCode.Text != null && txtToitemcode.Text != null)
                //{
                //    if (txtItemCode.Text.ToUpper().Trim() != ("NP" + txtToitemcode.Text.Trim()).ToUpper())
                //    {
                //if (objtxt.Tag != null)
                //bFlag = false;


                //    if (bFlag)
                //        errInventory.SetError(objtxt, Common.GetMessage("INF0019", lblItemCode.Text.Trim().Substring(0, lblItemCode.Text.Trim().Length - 2)));
                //    else
                //        errInventory.SetError(objtxt, Common.GetMessage("INF0019", lblToitemcode.Text.Trim().Substring(0, lblToitemcode.Text.Trim().Length - 2)));
                //}
                //else
                //    errInventory.SetError(objtxt, string.Empty);


            }
        }


        /// <summary>
        /// Call fn. ValidateItemCode to Validate Item Code
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtItemCode_Validated(object sender, EventArgs e)
        {
            TextBox objTxtBox = null;
            objTxtBox = (sender) as TextBox;
            try
            {

                m_itemDetail = new ItemInventory();
                ValidateItemCode(true, objTxtBox);
                if (chkExportIn.Checked == true)
                {
                    if (txtItemCode.Text == txtToitemcode.Text)
                    {
                        if (objTxtBox.Name == "txtItemCode" && txtToitemcode.Text != string.Empty)
                            errInventory.SetError(objTxtBox, Common.GetMessage("VAL0006", lblItemCode.Text.Trim().Substring(0, lblItemCode.Text.Trim().Length - 2)));
                        else if (objTxtBox.Name == "txtToitemcode" && txtItemCode.Text != string.Empty)
                            errInventory.SetError(objTxtBox, Common.GetMessage("VAL0006", lblToitemcode.Text.Trim().Substring(0, lblToitemcode.Text.Trim().Length - 2)));
                    }
                    else
                        return;

                }

                if (chkbatchadj.Checked == true)
                {
                    if (txtItemCode.Text != txtToitemcode.Text)
                    {
                        if (objTxtBox.Name == "txtItemCode" && txtToitemcode.Text != string.Empty)
                            errInventory.SetError(objTxtBox, Common.GetMessage("VAL0006", lblItemCode.Text.Trim().Substring(0, lblItemCode.Text.Trim().Length - 2)));
                        else if (objTxtBox.Name == "txtToitemcode" && txtItemCode.Text != string.Empty)
                            errInventory.SetError(objTxtBox, Common.GetMessage("VAL0006", lblToitemcode.Text.Trim().Substring(0, lblToitemcode.Text.Trim().Length - 2)));
                    }
                    else
                        errInventory.SetError(objTxtBox, string.Empty);
                    return;

                }

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
                ItemBatchDetails ibd = new ItemBatchDetails();
                TextBox objtext = (TextBox)sender;
                if (objtext.Name == "txtBatchNo")
                {
                    ItemBatchDetails.Spcall = 0;
                    availquan = (txtAvailableQty.Text == string.Empty ? 0 : Convert.ToInt32(txtAvailableQty.Text));
                }
                else if (objtext.Name == "txtToBatchNo")
                {
                    if (txtToitemcode.TextLength > 0)
                    {

                        ibd.ItemCode = txtToitemcode.Text;
                        ibd.LocationId = cmbLocation.SelectedValue.ToString();
                        ibd.BucketId = cmbToBucket.SelectedValue.ToString();
                        ibd.FromMfgDate = Common.DATETIME_NULL;
                        ibd.ToMfgDate = Common.DATETIME_NULL;
                        ItemBatchDetails.Spcall = 0;
                        List<ItemBatchDetails> lt = ibd.Search();
                        List<ItemBatchDetails> lt1 = null;
                        if (lt == null)
                        {
                            ItemBatchDetails.Spcall = 1;
                            lt1 = ibd.Search();

                        }
                        if ((lt != null) && (lt.Count > 0))
                        {

                            ItemBatchDetails.Spcall = 0;
                        }
                        else if ((lt1 != null) && (lt1.Count > 0))
                        {

                            ItemBatchDetails.Spcall = 1;
                        }
                    }
                }
                if (e.KeyValue == Common.F4KEY && !e.Alt)
                {
                    BatchNoKeyDown(sender);
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Return Availble Qty for batch, Location, and Item Wise
        /// </summary>
        /// <param name="batchNo"></param>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        decimal GetAvailableQty(string batchNo, string itemCode, string bucketId)
        {
            ItemBatchDetails ibd = new ItemBatchDetails();
            ibd.BatchNo = batchNo;
            ibd.ItemCode = itemCode;
            ibd.FromMfgDate = Common.DATETIME_NULL;
            ibd.ToMfgDate = Common.DATETIME_MAX;

            ibd.LocationId = Convert.ToInt32(cmbLocation.SelectedValue).ToString();
            List<ItemBatchDetails> lst = new List<ItemBatchDetails>();
            lst = ibd.Search();
            if (lst != null)
            {
                var query = (from p in lst where p.BucketId == bucketId && p.ItemCode.ToLower() == itemCode.ToLower() && p.BatchNo.ToUpper() == batchNo.ToUpper() select p);

                if (query.Count() > 0)
                    lst = (List<ItemBatchDetails>)query.ToList();

                //textInitiatedQty.Text = Math.Round(GetInitiatedQty(itemCode, bucketId, batchNo), Common.DisplayQtyRounding, MidpointRounding.AwayFromZero).ToString();

                //textNetQty.Text = Math.Round(GetAvailableQty(batchNo,itemCode,bucketId) - GetInitiatedQty(itemCode, bucketId, batchNo), Common.DisplayQtyRounding, MidpointRounding.AwayFromZero).ToString();
                
                 return Math.Round(lst[0].Quantity , Common.DisplayQtyRounding, MidpointRounding.AwayFromZero);
                //- GetInitiatedQty(itemCode, bucketId, batchNo)
            }
            else
                return 0;
        }

        /// <summary>
        /// Open new window form when F4 key is pressed
        /// </summary>
        /// <param name="e"></param>
        
        void BatchNoKeyDown(object objSender)
        {
            TextBox objtxtBox = (objSender) as TextBox;
            NameValueCollection nvc = new NameValueCollection();
            if (objtxtBox.Name == "txtBatchNo")
            {
                nvc.Add("ItemCode", txtItemCode.Text.Trim());
            }
            else
                nvc.Add("ItemCode", txtToitemcode.Text.Trim());
            if (cmbFromBucket.SelectedIndex == 0)
                nvc.Add("BucketId", "-1");
            else
                nvc.Add("BucketId", cmbFromBucket.SelectedValue.ToString());

            //if (m_locationType == (int)Common.LocationConfigId.BO || m_locationType == (int)Common.LocationConfigId.WH)
            nvc.Add("LocationId", Convert.ToInt32(cmbLocation.SelectedValue).ToString());
            //else
            //    nvc.Add("LocationId", Common.INT_DBNULL.ToString());

            CoreComponent.Controls.frmSearch objfrmSearch = new CoreComponent.Controls.frmSearch(CoreComponent.Controls.SearchTypes.ItemBatch, nvc);
            objfrmSearch.ShowDialog();
            CoreComponent.MasterData.BusinessObjects.ItemBatchDetails objItem = (CoreComponent.MasterData.BusinessObjects.ItemBatchDetails)objfrmSearch.ReturnObject;

            if (objItem != null)
            {
                errInventory.SetError(objtxtBox, string.Empty);
                m_F4Press = true;
                GetBatchInfo(objItem, objtxtBox.Tag == null ? true : false);
                if (objtxtBox.Name == "txtBatchNo")
                {
                    if (txtToitemcode.Text != string.Empty && txtToItemDesc.Text != string.Empty && txtToBatchNo.Enabled == false)
                    {
                        txtToBatchNo.Text = txtBatchNo.Text;
                    }
                    else if (txtToBatchNo.Text.ToString() == string.Empty)
                    {
                        txtToBatchNo.Text = null;
                        m_batchNo = objItem.BatchNo.ToString();
                    }
                }

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="yesNo"></param>
        void ValidateToBatchNo(Boolean yesNo)
        {
            bool isTextBoxEmpty = CoreComponent.Core.BusinessObjects.Validators.CheckForEmptyString(txtToBatchNo.Text.Trim().Length);
            if (isTextBoxEmpty == true && yesNo == false)
                errInventory.SetError(txtToBatchNo, Common.GetMessage("INF0019", lblToBatchNo.Text.Trim().Substring(0, lblToBatchNo.Text.Trim().Length - 2)));
            else if (isTextBoxEmpty == false)
            {
                if ((m_F4Press == false && yesNo == true) || (m_TomfgbatchNo.Trim().ToLower() != txtToBatchNo.Text.Trim().ToLower()))
                {
                    errInventory.SetError(txtToBatchNo, string.Empty);

                    ItemBatchDetails objItem = new ItemBatchDetails();
                    objItem.ManufactureBatchNo = txtToBatchNo.Text.Trim();
                    objItem.ItemCode = txtToitemcode.Text.Trim();

                    //if (m_locationType == (int)Common.LocationConfigId.BO || m_locationType == (int)Common.LocationConfigId.WH)
                    objItem.LocationId = Convert.ToInt32(cmbLocation.SelectedValue).ToString();
                    //else
                    //objItem.LocationId = Common.INT_DBNULL.ToString();

                    objItem.BucketId = cmbFromBucket.SelectedValue.ToString();
                    objItem.FromMfgDate = Common.DATETIME_NULL;
                    objItem.ToMfgDate = Common.DATETIME_MAX;

                    List<ItemBatchDetails> lstItemDetails = objItem.Search();
                    if (txtToitemcode.Text.Length > 0 && lstItemDetails != null)
                    {
                        var query = from p in lstItemDetails where p.ItemId == m_ToitemID select p;
                        lstItemDetails = query.ToList();

                        if (lstItemDetails.Count == 1)
                        {
                            objItem = lstItemDetails[0];
                            GetBatchInfo(objItem, false);
                        }
                        else if (lstItemDetails.Count > 1)
                        {
                            errInventory.SetError(txtToBatchNo, Common.GetMessage("VAL0038", lblToBatchNo.Text.Trim().Substring(0, lblToBatchNo.Text.Trim().Length - 2)));
                        }
                        else
                        {
                            errInventory.SetError(txtToBatchNo, Common.GetMessage("VAL0006", lblToBatchNo.Text.Trim().Substring(0, lblToBatchNo.Text.Trim().Length - 2)));
                        }
                    }
                    else
                    {
                        errInventory.SetError(txtToBatchNo, Common.GetMessage("VAL0006", lblToBatchNo.Text.Trim().Substring(0, lblToBatchNo.Text.Trim().Length - 2)));
                        //MessageBox.Show(Common.GetMessage("VAL0006", "batch code"));
                    }

                    if (grpBoxFrom.Enabled == true && txtToItemDesc.Text != string.Empty && m_TomfgbatchNo == string.Empty)
                        errInventory.SetError(txtToBatchNo, Common.GetMessage("VAL0006", lblToBatchNo.Text.Trim().Substring(0, lblToBatchNo.Text.Trim().Length - 2)));

                }
                if ((m_TomfgbatchNo.Trim().ToLower() == txtToBatchNo.Text.Trim().ToLower()))
                {
                    errInventory.SetError(txtToBatchNo, string.Empty);
                }

            }
        }

        void GetBatchQty()
        {
            try
            {

            }
            finally
            {
            }
        }








        void GetItem_BatchQty(Boolean yesNo)
        {
            bool isTextBoxEmpty = CoreComponent.Core.BusinessObjects.Validators.CheckForEmptyString(txtBatchNo.Text.Trim().Length);
            if (isTextBoxEmpty == true && yesNo == false)
                errInventory.SetError(txtBatchNo, Common.GetMessage("INF0019", lblBatchNo.Text.Trim().Substring(0, lblBatchNo.Text.Trim().Length - 2)));
            else if (isTextBoxEmpty == false)
            {
                // if ((m_F4Press == false && yesNo == true) || (m_mfgbatchNo.Trim().ToLower() != txtBatchNo.Text.Trim().ToLower()))
                {
                    errInventory.SetError(txtBatchNo, string.Empty);

                    ItemBatchDetails objItem = new ItemBatchDetails();
                    objItem.ManufactureBatchNo = txtBatchNo.Text.Trim();
                    objItem.ItemCode = txtItemCode.Text.Trim();

                    //if (m_locationType == (int)Common.LocationConfigId.BO || m_locationType == (int)Common.LocationConfigId.WH)
                    objItem.LocationId = Convert.ToInt32(cmbLocation.SelectedValue).ToString();
                    //else
                    //objItem.LocationId = Common.INT_DBNULL.ToString();

                    
                    objItem.BucketId = cmbFromBucket.SelectedValue.ToString();
                    objItem.FromMfgDate = Common.DATETIME_NULL;
                    objItem.ToMfgDate = Common.DATETIME_MAX;
                    ItemBatchDetails.Spcall = 0;
                    List<ItemBatchDetails> lstItemDetails = objItem.Search();
                    if (txtItemCode.Text.Length > 0 && lstItemDetails != null)
                    {
                        var query = from p in lstItemDetails where p.ItemId == m_itemId && p.BatchNo == m_batchNo select p;
                        lstItemDetails = query.ToList();

                        if (lstItemDetails.Count == 1 || lstItemDetails.Count > 1)
                        {
                            objItem = lstItemDetails[0];
                            GetBatchInfo(objItem, true);
                            //GetBatchInfo(objItem, false);
                        }
                        else if (lstItemDetails.Count > 1)
                        {
                            errInventory.SetError(txtBatchNo, Common.GetMessage("VAL0038", lblBatchNo.Text.Trim().Substring(0, lblBatchNo.Text.Trim().Length - 2)));
                        }
                        else
                        {
                            errInventory.SetError(txtBatchNo, Common.GetMessage("VAL0006", lblBatchNo.Text.Trim().Substring(0, lblBatchNo.Text.Trim().Length - 2)));
                        }
                    }
                    else
                    {
                        errInventory.SetError(txtBatchNo, Common.GetMessage("VAL0006", lblBatchNo.Text.Trim().Substring(0, lblBatchNo.Text.Trim().Length - 2)));
                        //MessageBox.Show(Common.GetMessage("VAL0006", "batch code"));
                    }
                }
            }
        }







        /// <summary>
        /// Validate Batch No.
        /// </summary>
        void ValidateBatchNo(Boolean yesNo)
        {
            bool isTextBoxEmpty = CoreComponent.Core.BusinessObjects.Validators.CheckForEmptyString(txtBatchNo.Text.Trim().Length);
            if (isTextBoxEmpty == true && yesNo == false)
                errInventory.SetError(txtBatchNo, Common.GetMessage("INF0019", lblBatchNo.Text.Trim().Substring(0, lblBatchNo.Text.Trim().Length - 2)));
            else if (isTextBoxEmpty == false)
            {
                if ((m_F4Press == false && yesNo == true) || (m_mfgbatchNo.Trim().ToLower() != txtBatchNo.Text.Trim().ToLower()))
                {
                    errInventory.SetError(txtBatchNo, string.Empty);

                    ItemBatchDetails objItem = new ItemBatchDetails();
                    objItem.ManufactureBatchNo = txtBatchNo.Text.Trim();
                    objItem.ItemCode = txtItemCode.Text.Trim();

                    //if (m_locationType == (int)Common.LocationConfigId.BO || m_locationType == (int)Common.LocationConfigId.WH)
                    objItem.LocationId = Convert.ToInt32(cmbLocation.SelectedValue).ToString();
                    //else
                    //objItem.LocationId = Common.INT_DBNULL.ToString();

                    objItem.BucketId = cmbFromBucket.SelectedValue.ToString();
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
                            GetBatchInfo(objItem, true);
                        }
                        else if (lstItemDetails.Count > 1)
                        {
                            errInventory.SetError(txtBatchNo, Common.GetMessage("VAL0038", lblBatchNo.Text.Trim().Substring(0, lblBatchNo.Text.Trim().Length - 2)));
                        }
                        else
                        {
                            errInventory.SetError(txtBatchNo, Common.GetMessage("VAL0006", lblBatchNo.Text.Trim().Substring(0, lblBatchNo.Text.Trim().Length - 2)));
                        }
                    }
                    else
                    {
                        errInventory.SetError(txtBatchNo, Common.GetMessage("VAL0006", lblBatchNo.Text.Trim().Substring(0, lblBatchNo.Text.Trim().Length - 2)));
                        //MessageBox.Show(Common.GetMessage("VAL0006", "batch code"));
                    }
                }
            }
        }

        ///// <summary>
        ///// Call fn. ValidateBatchNo
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        private void txtBatchNo_Validated(object sender, EventArgs e)
        {
            TextBox objTxtBox = null;
            try
            {

                objTxtBox = (sender) as TextBox;

                if (objTxtBox.Tag == null)
                    ValidateBatchNo(true);
                else
                    ValidateToBatchNo(true);

                if (chkbatchadj.Checked == true)
                {
                    if (m_batchNo != string.Empty && m_TobatchNo != string.Empty)
                        if (m_batchNo == m_TobatchNo)
                        {
                            if (objTxtBox.Name == "txtBatchNo" && txtToBatchNo.Text != string.Empty)
                                errInventory.SetError(objTxtBox, Common.GetMessage("VAL0006", lblBatchNo.Text.Trim().Substring(0, lblBatchNo.Text.Trim().Length - 2)));
                            else if (objTxtBox.Name == "txtToBatchNo" && txtBatchNo.Text != string.Empty)
                                errInventory.SetError(objTxtBox, Common.GetMessage("VAL0006", lblToBatchNo.Text.Trim().Substring(0, lblToBatchNo.Text.Trim().Length - 2)));

                        }
                        else
                            return;

                }



            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        decimal GetInitiatedQty(string ItemCode, string fromBucketId, string batchNo)
        {
            InventoryAdjust objInvAdj = new InventoryAdjust();
            string errorMessage = string.Empty;
            decimal i = objInvAdj.GetInventoryAdjItemQty(Convert.ToInt32(cmbLocation.SelectedValue), ItemCode, Convert.ToInt32(fromBucketId), batchNo, (int)Common.InventoryStatus.Initiated, ref errorMessage);

            if (errorMessage.Trim() == string.Empty)
                return i;
            else
                return 0;
        }

        /// <summary>
        /// Get Batch Information
        /// </summary>
        /// <param name="objItem"></param>
        void GetBatchInfo(ItemBatchDetails objItem, bool bFromItem)
        {
            decimal qty = 0;
            qty = Math.Round(objItem.Quantity); // GetInitiatedQty(objItem.ItemCode, objItem.BucketId, objItem.BatchNo), Common.DisplayQtyRounding);
            if (bFromItem != false)
                txtAvailableQty.Text = qty.ToString();

            if (bFromItem)
            {
                m_mfgbatchNo = objItem.ManufactureBatchNo.ToString();
                txtBatchNo.Text = objItem.ManufactureBatchNo.ToString();
                m_batchNo = objItem.BatchNo.ToString();
                //m_batchNo = objItem.ManufactureBatchNo.ToString();
                m_batchNoM = objItem.ManufactureBatchNo.ToString();
            }
            else
            {
                m_TomfgbatchNo = objItem.ManufactureBatchNo.ToString();
                txtToBatchNo.Text = objItem.ManufactureBatchNo.ToString();
                m_TobatchNo = objItem.BatchNo.ToString();
                //m_TobatchNo = objItem.ManufactureBatchNo.ToString();
                if (objItem.Manufacure != null && objItem.Expiry != null)
                {
                    m_Mfgdate = objItem.Manufacure.ToString();
                    m_Expdate = objItem.Expiry.ToString();
                }
            }

        }

        /// <summary>
        /// Call fn. SearchInventoryAdjustment
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

        /// <summary>
        /// Reset Item Controls
        /// </summary>
        void ResetItemControl()
        {
            m_F4Press = false;
            txtItemCode.ReadOnly = false;
            txtBatchNo.ReadOnly = false;
            m_selectedItemRowNum = Common.INT_DBNULL;
            m_reasonDescription = string.Empty;
            m_itemId = Common.INT_DBNULL;
            m_toBucketid = Common.INT_DBNULL;
            m_fromBucketid = Common.INT_DBNULL;
            m_batchNo = string.Empty;
            m_batchNoM = string.Empty;
            m_itemDetail = null;
            m_mfgbatchNo = string.Empty;
            //bool bFlag = true;
            m_selectedItemRowIndex = Common.INT_DBNULL;
            VisitControls visitControls = new VisitControls();
            visitControls.ResetAllControlsInPanel(errInventory, grpAddDetails);

            dgvInventoryItem.ClearSelection();
            

            if ((m_lstInvDetail != null && m_lstInvDetail.Count > 0) || (m_locationType == (int)Common.LocationConfigId.BO) || (m_locationType == (int)Common.LocationConfigId.WH))
            {
                //cmbLocation.Enabled = false;
                //chkExportIn.Enabled = false;
                chkExportIn.Enabled = false;// (m_locationType == (int)Common.LocationConfigId.HO || m_locationType == (int)Common.LocationConfigId.WH && (m_lstInvDetail == null || m_lstInvDetail.Count == 0) ? true : false);
                chkbatchadj.Enabled = false;
                chkExportIn.Enabled = false;
               
                if (dgvInventoryItem.Rows.Count == 0 && ((m_locationType == (int)Common.LocationConfigId.BO) || (m_locationType == (int)Common.LocationConfigId.WH)))
                {
                    chkExportIn.Enabled = true;
                }
            
                cmbReasonCode.Enabled = (chkExportIn.Checked == true || chkbatchadj.Checked == true ? false : true);

                //cmbReasonCode.SelectedIndex = 0;
                if (chkExportIn.Checked == true)
                    cmbReasonCode.SelectedIndex = 10;
                if (chkbatchadj.Checked == true)
                    cmbReasonCode.SelectedIndex = 11;
            }
            else
            {
                cmbLocation.Enabled = true;
                chkExportIn.Enabled = true;
                //chkExportIn.Enabled = false;
                chkExportIn.Checked = false;
                chkbatchadj.Enabled = false;
                chkbatchadj.Checked = false;
                cmbReasonCode.Enabled = true;
                //cmbReasonCode.SelectedIndex = -1;
                //chkExportIn.Checked = (chkExportIn.Checked== true?true:false);

                grpBoxFrom.Enabled = false;
                //txtToitemcode.Enabled = (chkExportIn.Checked == true ? true : false);
            }
            //if (chkExportIn.Checked)
            //    chkExportIn_Click(null, null);
        }

        /// <summary>
        /// Search TOIs
        /// </summary>
        List<InventoryAdjust> SearchInventoryAdjustment()
        {
            List<InventoryAdjust> lstInventory = new List<InventoryAdjust>();
            DateTime DATETIME_MAX = Convert.ToDateTime(new DateTime(2099, 12, 31).ToString(Common.DATE_TIME_FORMAT));

            DateTime fromDate = dtpSearchFrom.Checked == true ? Convert.ToDateTime(dtpSearchFrom.Value) : Common.DATETIME_NULL;
            DateTime toDate = dtpSearchTo.Checked == true ? Convert.ToDateTime(dtpSearchTo.Value) : DATETIME_MAX;
            int isexported = Convert.ToInt32(chkisexport.CheckState);// ? 1 : 0;
            int interbatadj = Convert.ToInt32(chkinternalbatadj.CheckState);// ? 1 : 0;

            lstInventory = Search(Convert.ToInt32(cmbSearchLocation.SelectedValue), txtSeqNo.Text.Trim(), Convert.ToInt32(cmbSearchStatus.SelectedValue), fromDate.ToString(Common.DATE_TIME_FORMAT), toDate.ToString(Common.DATE_TIME_FORMAT), Convert.ToInt32(cmbApprovedBy.SelectedValue), isexported, interbatadj);

            return lstInventory;
        }

        /// <summary>
        /// Bind Grid
        /// </summary>
        void BindGrid()
        {
            m_lstInvHeader = SearchInventoryAdjustment();
            if ((m_lstInvHeader != null) && (m_lstInvHeader.Count > 0))
            {
                dgvInventory.DataSource = m_lstInvHeader;
                dgvInventory.ClearSelection();
                //dgvInventory.Select();
                ResetItemControl();
            }
            else
            {
                dgvInventory.DataSource = new List<InventoryAdjust>();
                MessageBox.Show(Common.GetMessage("8002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Return List of Inventory Adjustment
        /// </summary>
        /// <param name="locationId"></param>
        /// <param name="adjustNo"></param>
        /// <param name="statusId"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        List<InventoryAdjust> Search(int locationId, string adjustNo, int statusId, string fromDate, string toDate, int userId, int exportstatus, int interbatadjstatus)
        {
            List<InventoryAdjust> listInventory = new List<InventoryAdjust>();
            InventoryAdjust objInventory = new InventoryAdjust();
            objInventory.LocationId = locationId;
            objInventory.SeqNo = adjustNo;
            objInventory.StatusId = statusId;
            objInventory.FromDate = fromDate;
            objInventory.ToDate = toDate;
            objInventory.ApprovedBy = userId;
            objInventory.Isexport = exportstatus;
            objInventory.InterbatchAdj = interbatadjstatus;


            listInventory = objInventory.Search();

            return listInventory;
        }

        /// <summary>
        /// Empty Error Provider for all controls
        /// </summary>
        void EmptyErrorProvider()
        {
            errInventory.SetError(dtpDate, string.Empty);
            errInventory.SetError(cmbLocation, string.Empty);
            errInventory.SetError(cmbToBucket, string.Empty);
            errInventory.SetError(cmbFromBucket, string.Empty);
            errInventory.SetError(cmbReasonCode, string.Empty);

            errInventory.SetError(txtApprovedBy, string.Empty);
            errInventory.SetError(txtQty, string.Empty);
            errInventory.SetError(txtItemCode, string.Empty);
            //errInventory.SetError(txtBatchNo, string.Empty);
        }

        /// <summary>
        /// Validate Controls On Add Button Click
        /// </summary>
        void ValidateMessages()
        {
            if (m_objInventoryAdj != null && (grpBoxFrom.Enabled || m_objInventoryAdj.StatusId == (int)Common.InventoryStatus.Initiated))
                ValidateQuantity(txtApprovedQty, lblApprovedQty, errInventory);
            else if (m_objInventoryAdj == null || grpBoxFrom.Enabled || m_objInventoryAdj.StatusId < (int)Common.InventoryStatus.Initiated)
            {
                if (!grpBoxFrom.Enabled)
                {
                    ValidateItemCode(false, txtItemCode);
                    ValidateBatchNo(false);
                }
                else
                {
                    ValidateItemCode(false, txtItemCode);
                    ValidateBatchNo(false);
                    ValidateItemCode(false, txtToitemcode);
                    //ValidateToBatchNo(false);
                }
                ValidateQuantity(txtQty, lblQty, errInventory);
                ValidateCombo(cmbReasonCode, lblReasonCode);
                ValidateCombo(cmbToBucket, lblToBukcetName);
                ValidateCombo(cmbFromBucket, lblFromBucketName);

            }
        }

        /// <summary>
        /// Generate string for Error
        /// </summary>
        /// <returns></returns>
        private StringBuilder GenerateError()
        {
            StringBuilder sbError = new StringBuilder();
            if (errInventory.GetError(cmbLocation).Trim().Length > 0)
            {
                sbError.Append(errInventory.GetError(cmbLocation));
                sbError.AppendLine();
            }
            if (errInventory.GetError(dtpDate).Trim().Length > 0)
            {
                sbError.Append(errInventory.GetError(dtpDate));
                sbError.AppendLine();
            }
            if (errInventory.GetError(txtItemCode).Trim().Length > 0)
            {
                sbError.Append(errInventory.GetError(txtItemCode));
                sbError.AppendLine();
            }
            if (grpBoxFrom.Enabled && errInventory.GetError(txtToitemcode).Trim().Length > 0)
            {
                sbError.Append(errInventory.GetError(txtToitemcode));
                sbError.AppendLine();
            }
            if (errInventory.GetError(cmbFromBucket).Trim().Length > 0)
            {
                sbError.Append(errInventory.GetError(cmbFromBucket));
                sbError.AppendLine();
            }
            if (errInventory.GetError(cmbToBucket).Trim().Length > 0)
            {
                sbError.Append(errInventory.GetError(cmbToBucket));
                sbError.AppendLine();
            }
            if (errInventory.GetError(txtBatchNo).Trim().Length > 0)
            {
                sbError.Append(errInventory.GetError(txtBatchNo));
                sbError.AppendLine();
            }
            if (errInventory.GetError(txtQty).Trim().Length > 0)
            {
                sbError.Append(errInventory.GetError(txtQty));
                sbError.AppendLine();
            }
            if (errInventory.GetError(cmbReasonCode).Trim().Length > 0)
            {
                sbError.Append(errInventory.GetError(cmbReasonCode));
                sbError.AppendLine();
            }
            if (errInventory.GetError(txtApprovedQty).Trim().Length > 0)
            {
                sbError.Append(errInventory.GetError(txtApprovedQty));
                sbError.AppendLine();
            }

            if (errInventory.GetError(txtToBatchNo).Trim().Length > 0)
            {
                sbError.Append(errInventory.GetError(txtToBatchNo));
                sbError.AppendLine();
            }
            return Common.ReturnErrorMessage(sbError);
            //return sbError;
        }

        /// <summary>
        /// Add/Update Item into Grid
        /// </summary>
        /// <returns></returns>
        bool AddItem()
        {

            //m_itemDetail = new ItemInventory();
            m_itemDetail.ToItemId = m_ToitemID;
            m_itemDetail.ToItemCode = txtToitemcode.Text.Trim();
            m_itemDetail.ToItemName = txtToItemDesc.Text.Trim();
            m_itemDetail.ToBatchNo = (m_TobatchNo != string.Empty ? m_TobatchNo : null);
            m_itemDetail.ToManufactureBatchNo = (m_TomfgbatchNo != string.Empty ? m_TomfgbatchNo : m_mfgbatchNo);
            if (m_epd != string.Empty && m_mfd != string.Empty)
            {
                m_itemDetail.Mfg = m_mfd;
                m_itemDetail.Exp = m_epd;
            }

            m_itemDetail.ItemId = m_itemId;
            m_itemDetail.ItemCode = txtItemCode.Text.Trim();
            m_itemDetail.ItemName = txtItemDescription.Text.Trim();
            m_itemDetail.ManufactureBatchNo = txtBatchNo.Text.Trim();

            m_itemDetail.BatchNo = m_batchNo;
            m_itemDetail.UOMId = m_UOMId;
            m_itemDetail.Weight = m_weight;
            m_itemDetail.UOMName = m_UOMName;
            m_itemDetail.ReasonCodeDescription = cmbReasonCode.Text.Trim().ToString();
            m_itemDetail.ReasonDescription = txtReasonDescription.Text.Trim();

            m_itemDetail.FromBucketId = Convert.ToInt32(cmbFromBucket.SelectedValue);
            m_itemDetail.ToBucketId = Convert.ToInt32(cmbToBucket.SelectedValue);
            //m_itemDetail.ApprovedQty = Convert.ToDecimal(txtApprovedQty.Text.Trim().Length == 0 ? "0" : txtApprovedQty.Text);

            m_itemDetail.FromBucketName = cmbFromBucket.Text.ToString().Trim();
            m_itemDetail.ToBucketName = cmbToBucket.Text.Trim();
            //m_itemDetail.Quantity = Math.Round(Convert.ToDecimal(txtQty.Text), 2);
            m_itemDetail.ReasonCode = cmbReasonCode.SelectedValue.ToString();
            m_itemDetail.BalanceQty = Math.Round(Convert.ToDecimal(txtAvailableQty.Text), 2) - Math.Round(Convert.ToDecimal(txtQty.Text), 2);
            m_itemDetail.RowNo = m_selectedItemRowNum;

            if (m_lstInvDetail == null)
                m_lstInvDetail = new List<ItemInventory>();

            if ((m_selectedItemRowIndex != Common.INT_DBNULL) && (m_selectedItemRowIndex <= dgvInventoryItem.Rows.Count))
            {
                m_lstInvDetail.Insert(m_selectedItemRowIndex, m_itemDetail);
                m_lstInvDetail.RemoveAt(m_selectedItemRowIndex + 1);
            }
            else
                m_lstInvDetail.Add(m_itemDetail);

            ResetItemControl();
            ItemBatchDetails.Spcall = 0;
            return true;
        }

        /// <summary>
        /// Copy Item into seperate List exclusive of current selected item
        /// </summary>
        /// <param name="excludeIndex"></param>
        /// <param name="lst"></param>
        /// <returns></returns>
        List<ItemInventory> CopyItemDetail(int excludeIndex, List<ItemInventory> lst)
        {
            List<ItemInventory> returnList = new List<ItemInventory>();
            for (int i = 0; i < lst.Count; i++)
            {
                if (i != excludeIndex)
                {
                    ItemInventory tdetail = new ItemInventory();
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
            //if (m_locationType == (int)Common.LocationConfigId.BO || m_locationType == (int)Common.LocationConfigId.WH)
            //{
            // Check Duplicate For ItemCode and Batch No, from bucket and To Bucket
            if (txtQty.Text.Length > 0)
            {
                if (m_lstInvDetail != null && m_lstInvDetail.Count > 0)
                {
                    List<ItemInventory> tiDetail = CopyItemDetail(m_selectedItemRowIndex, m_lstInvDetail);
                    //checked based on ItemCode and Bucket Id
                    m_isDuplicateRecordFound = (from p in tiDetail where p.ItemCode.Trim().ToLower() == txtItemCode.Text.Trim().ToLower() && p.BatchNo.Trim().ToLower() == m_batchNo.Trim().ToLower() && p.FromBucketId == Convert.ToInt32(cmbFromBucket.SelectedValue) && p.ToBucketId == Convert.ToInt32(cmbToBucket.SelectedValue) select p).Count();

                    if (m_isDuplicateRecordFound > 0)
                    {
                        MessageBox.Show(Common.GetMessage("VAL0032", "item code", "batch no"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
                if (txtAvailableQty.Text.Trim().Length == 0)
                {
                    txtAvailableQty.Text = "0";
                }

                if (txtAvailableQty.Text.Trim().Length > 0 && txtQty.Text.Trim().Length > 0)
                {
                    m_itemDetail.Quantity = Convert.ToDecimal(txtQty.Text);
                    m_itemDetail.ApprovedQty = Convert.ToDecimal(txtApprovedQty.Text);

                    if (m_itemDetail.DisplayQuantity > Math.Round(Convert.ToDecimal(txtAvailableQty.Text)))
                    {
                        return false;
                    }


                }
            }
            //}
            return true;
        }

        /// <summary>
        /// Return List of Item Details
        /// </summary>
        /// <param name="TNumber"></param>
        /// <param name="sourceAddressId"></param>
        /// <returns></returns>
        List<ItemInventory> SearchTIItem(string adjustmentNo)
        {
            if (m_objInventoryAdj == null)
                m_objInventoryAdj = new InventoryAdjust();

            m_lstInvDetail = m_objInventoryAdj.SearchItem(adjustmentNo);
            return m_lstInvDetail;
        }

        void SelectSearchGrid(string seqNo)
        {
            if (m_lstInvDetail == null)
                m_lstInvDetail = new List<ItemInventory>();
            m_lstInvDetail = SearchTIItem(seqNo);

            m_seqNo = seqNo;
            
            dgvInventoryItem.SelectionChanged -= new System.EventHandler(dgvInventoryItem_SelectionChanged);
            dgvInventoryItem.DataSource = new List<ItemInventory>();
            if (m_lstInvDetail != null && m_lstInvDetail.Count > 0)
            {
                dgvInventoryItem.ClearSelection();
                dgvInventoryItem.DataSource = m_lstInvDetail;

                ResetItemControl();

            }

            tabControlTransaction.TabPages[1].Text = Common.TAB_UPDATE_MODE;
            tabControlTransaction.SelectedIndex = 1;

            EnableDisableButton(m_objInventoryAdj.StatusId);

            dgvInventoryItem.SelectionChanged += new System.EventHandler(dgvInventoryItem_SelectionChanged);
            dgvInventoryItem.ClearSelection();
            if (m_lstInvDetail != null && m_lstInvDetail.Count > 0 && m_lstInvDetail[0].ReasonCode == "10")
            {
                chkExportIn.Checked = true;
                chkExportIn_Click(null, null);
            }
            chkExportIn.Checked = (m_lstInvDetail[0].Export == 1 ? true : false);
            chkbatchadj.Checked = false;// (m_lstInvDetail[0].InterBatchAdj == 1 ? true : false);
            //ItemInventory objinv = new ItemInventory();
            //chkExportIn.Checked = (objinv.Export == 1 ? true : false); 
            //chkExportIn.Enabled = (objinv.Isexport ==1? false: true);
            //chkExportIn.Enabled = (objinv.StatusId == 2 ? false : true);
            cmbLocation.SelectedValue = Convert.ToInt32(m_objInventoryAdj.LocationId);
            cmbLocation.Enabled = false;
            DataTable dtSource = Common.ParameterLookup(Common.ParameterType.Locations, new ParameterFilter("LOCATIONS", 0, 0, 0));
            DataRow[] drSource = dtSource.Select("LocationId = " + m_objInventoryAdj.LocationId);
            m_sourceLocationId = m_objInventoryAdj.LocationId;
            txtLocationCode.Text = drSource[0]["DisplayName"].ToString();
            txtLocationAddress.Text = m_objInventoryAdj.LocationAddress;

            BindChangedControls(m_objInventoryAdj);

            txtAdjustmentNo.Text = m_objInventoryAdj.SeqNo.ToString();
            if (m_lstInvDetail[0].ToItemCode == string.Empty && m_lstInvDetail[0].ToItemName == string.Empty)
            {
                dgvInventoryItem.Columns["ToItemCode"].Visible = false;
                dgvInventoryItem.Columns["ToItemName"].Visible = false;
                dgvInventoryItem.Columns["ToBatchNo"].Visible = false;
            }
            ReadOnlyItem();
            dtpDate.Enabled = (m_objInventoryAdj.StatusId != (int)Common.InventoryStatus.Initiated) ? true : false;

        }
        /// <summary>
        /// Edit TOI
        /// </summary>
        /// <param name="e"></param> 
        private void EditInventoryItem(DataGridViewCellEventArgs e)
        {
            if ((e.RowIndex >= 0) && (dgvInventory.Rows[e.RowIndex].Cells[e.ColumnIndex].GetType() == typeof(DataGridViewImageCell)))
            {
                m_objInventoryAdj = m_lstInvHeader[e.RowIndex];

                SelectSearchGrid(m_objInventoryAdj.SeqNo);
            }
        }

        /// <summary>
        /// Bind Controls for Status 
        /// </summary>
        /// <param name="m_objInventoryAdj"></param>
        void BindChangedControls(InventoryAdjust m_objInventoryAdj)
        {
            cmbLocation.Enabled = false;
            txtApprovedBy.Text = m_objInventoryAdj.ApprovedName.ToString();
            txtInitiatedBy.Text = m_objInventoryAdj.InitiatedName.ToString();

            txtStatus.Text = m_objInventoryAdj.StatusName;
            if (m_objInventoryAdj.InitiatedDate.Length > 0)
            {
                dtpDate.Checked = true;
                dtpDate.Value = Convert.ToDateTime(m_objInventoryAdj.InitiatedDate);
            }
            else
            { dtpDate.Checked = false; }

        }

        /// <summary>
        /// This function is used to show data in controls
        /// </summary>
        /// <param name="e"></param>
        private void SelectGridRow(EventArgs e)
        {
            if (dgvInventoryItem.SelectedCells.Count > 0)
            {
                int rowIndex = dgvInventoryItem.SelectedCells[0].RowIndex;
                int columnIndex = dgvInventoryItem.SelectedCells[0].ColumnIndex;

                if (rowIndex >= 0 && columnIndex >= 0)
                {
                    m_isDuplicateRecordFound = Common.INT_DBNULL;

                    string itemCode = dgvInventoryItem.Rows[rowIndex].Cells["ItemCode"].Value.ToString().Trim();
                    m_batchNo = dgvInventoryItem.Rows[rowIndex].Cells["BatchId"].Value.ToString().Trim();
                    m_fromBucketid = Convert.ToInt32(dgvInventoryItem.Rows[rowIndex].Cells["FromBucketId"].Value.ToString());
                    m_toBucketid = Convert.ToInt32(dgvInventoryItem.Rows[rowIndex].Cells["ToBucketId"].Value.ToString());

                    if (m_lstInvDetail == null)
                        return;

                    var itemSelect = (from p in m_lstInvDetail where p.ItemCode.ToLower() == itemCode.ToLower() && p.BatchNo.ToLower() == m_batchNo.ToLower() && p.FromBucketId == m_fromBucketid && p.ToBucketId == m_toBucketid select p);

                    if (itemSelect.ToList().Count == 0)
                        return;

                    m_selectedItemRowIndex = rowIndex;
                    m_selectedItemRowNum = Convert.ToInt32(dgvInventoryItem.Rows[rowIndex].Cells["RowNo"].Value.ToString());
                    m_itemDetail = itemSelect.ToList()[0];
                    m_batchNo = m_itemDetail.BatchNo;
                    m_TobatchNo = m_itemDetail.ToBatchNo;
                    m_ToitemID = m_itemDetail.ToItemId;
                    m_Mfgdate = m_itemDetail.MfgDate;
                    m_Expdate = m_itemDetail.ExpDate;


                    m_batchNoM = m_itemDetail.BatchNo;
                    m_itemId = m_itemDetail.ItemId;
                    m_UOMId = m_itemDetail.UOMId;
                    m_weight = m_itemDetail.Weight;
                    m_UOMName = m_itemDetail.UOMName;
                    m_fromBucketName = m_itemDetail.FromBucketName;
                    m_toBucketName = m_itemDetail.FromBucketName;
                    m_reasonDescription = m_itemDetail.ReasonDescription;
                    m_mfgbatchNo = m_itemDetail.ManufactureBatchNo;

                    txtWeight.Text = Math.Round(m_weight, 2).ToString();
                    txtItemCode.Text = m_itemDetail.ItemCode;
                    txtItemDescription.Text = m_itemDetail.ItemName;




                    cmbFromBucket.SelectedIndexChanged -= new EventHandler(cmbFromBucket_SelectedIndexChanged);
                    FillBucket(true, true);
                    cmbFromBucket.SelectedValue = Convert.ToInt32(m_itemDetail.FromBucketId);
                    cmbFromBucket.SelectedIndexChanged += new EventHandler(cmbFromBucket_SelectedIndexChanged);

                    cmbToBucket.SelectedValue = Convert.ToInt32(m_itemDetail.ToBucketId);
                    cmbReasonCode.SelectedValue = Convert.ToInt32(m_itemDetail.ReasonCode);
                    txtApprovedQty.Text = m_itemDetail.DisplayApprovedQty.ToString();
                    txtReasonDescription.Text = m_itemDetail.ReasonDescription;
                    if (grpBoxFrom.Enabled)
                    {
                        txtToWeight.Text = Math.Round(m_weight, 2).ToString();
                        txtToitemcode.Text = m_itemDetail.ToItemCode;
                        txtToItemDesc.Text = m_itemDetail.ToItemName;
                        txtToBatchNo.Text = m_itemDetail.ToManufactureBatchNo;
                    }
                    txtBatchNo.Text = m_itemDetail.ManufactureBatchNo;
                    txtQty.Text = m_itemDetail.DisplayQuantity.ToString();
                    //txtUOMName.Text = m_UOMName;

                    ReadOnlyItem();
                    if (m_objInventoryAdj != null)
                        dtpDate.Enabled = true;// (m_objInventoryAdj.StatusId != (int)Common.InventoryStatus.Initiated) ? true : false;
                    ItemBatchDetails.Spcall = 0;
                    //if (m_locationType == (int)Common.LocationConfigId.BO || m_locationType == (int)Common.LocationConfigId.WH)
                    txtAvailableQty.Text = GetAvailableQty(m_batchNo, itemCode, m_fromBucketid.ToString()).ToString();
                    textInitiatedQty.Text = Math.Round(GetInitiatedQty(itemCode, m_fromBucketid.ToString(), m_batchNo), Common.DisplayQtyRounding, MidpointRounding.AwayFromZero).ToString();
                    //textNetQty.Text = (Math.Round(Convert.ToDecimal(txtAvailableQty.Text), Common.DisplayAmountRounding) - Math.Round(Convert.ToDecimal(textInitiatedQty.Text), Common.DisplayAmountRounding)).ToString();
                    //textNetQty.Text = Math.Round(txtAvailableQty.Text. - textInitiatedQty.Text, Common.DisplayQtyRounding, MidpointRounding.AwayFromZero).ToString();
                    cmbReasonCode.Enabled = (chkExportIn.Checked == true || chkbatchadj.Checked == true ? false : true);
                    chkExportIn.Enabled = (dgvInventoryItem.Rows.Count > 0 ? false : true);
                    chkbatchadj.Enabled = false;
                 

                }
            }
        }

        /// <summary>
        /// Disable Item Controls
        /// </summary>
        void ReadOnlyItem()
        {
            //if (m_objInventoryAdj == null || m_objInventoryAdj.StatusId == (int)Common.InventoryStatus.Approved || m_objInventoryAdj.StatusId == (int)Common.InventoryStatus.Rejected)
            //{
            //    txtReasonDescription.Enabled = false;
            //    txtApprovedQty.Enabled = false;
            //    btnApproved.Enabled = false;
            //    btnReject.Enabled = false;
            //}

            if (m_locationType == (int)Common.LocationConfigId.BO || m_locationType == (int)Common.LocationConfigId.WH)
            {
                //chkExportIn.Enabled = false;
                //chkExportIn.Enabled = (m_locationType == (int)Common.LocationConfigId.WH ? true : false);

                grpBoxFrom.Enabled = (chkExportIn.Checked == true || chkbatchadj.Checked == true ? true : false);
                txtReasonDescription.Enabled = false;
                txtApprovedQty.Enabled = false;
            }
            else
            {
                if (m_objInventoryAdj == null || m_objInventoryAdj.StatusId == (int)Common.InventoryStatus.Approved || m_objInventoryAdj.StatusId == (int)Common.InventoryStatus.Rejected)
                {
                    txtReasonDescription.Enabled = false;
                    txtApprovedQty.Enabled = false;
                    btnApproved.Enabled = false;
                    btnReject.Enabled = false;
                }
                else if (m_objInventoryAdj != null && m_objInventoryAdj.StatusId == (int)Common.InventoryStatus.Initiated)
                {
                    txtReasonDescription.Enabled = true;
                    txtApprovedQty.Enabled = false;
                    btnAddDetails.Enabled = true;
                }
            }

            if (m_objInventoryAdj != null && m_objInventoryAdj.StatusId != (int)Common.InventoryStatus.Initiated)
            {
                txtItemCode.Enabled = false;
                txtBatchNo.Enabled = false;
                cmbFromBucket.Enabled = false;
                cmbToBucket.Enabled = false;
                txtQty.Enabled = false;
                cmbReasonCode.Enabled = false;
                dtpDate.Enabled = true;
                txtToitemcode.Enabled = false;

                txtToBatchNo.Enabled = false;
                //cmbLocation.Enabled = false;
                btnbatch.Enabled = false;

            }
            else if (m_objInventoryAdj != null && m_objInventoryAdj.StatusId == (int)Common.InventoryStatus.Initiated)
            {
                txtItemCode.Enabled = false;
                txtBatchNo.Enabled = false;
                cmbFromBucket.Enabled = false;
                cmbToBucket.Enabled = false;
                txtQty.Enabled = false;
                cmbReasonCode.Enabled = false;
                dtpDate.Enabled = true;
                txtToitemcode.Enabled = false;
                txtToBatchNo.Enabled = false;
                //cmbLocation.Enabled = false;
                btnbatch.Enabled = false;


            }


            else
            {
                txtItemCode.Enabled = true;
                txtBatchNo.Enabled = true;
                cmbFromBucket.Enabled = true;
                cmbToBucket.Enabled = true;
                txtQty.Enabled = true;
                //cmbReasonCode.Enabled = true;
                dtpDate.Enabled = true;
                txtToitemcode.Enabled = true;


            }




            //}
            //else if (m_locationType == (int)Common.LocationConfigId.HO)
            //{
            //    txtItemCode.Enabled = false;
            //    txtBatchNo.Enabled = false;
            //    cmbFromBucket.Enabled = false;
            //    cmbToBucket.Enabled = false;
            //    txtQty.Enabled = false;
            //    cmbReasonCode.Enabled = false;
            //    dtpDate.Enabled = false;
            //    txtApprovedQty.ReadOnly = false;
            //    txtReasonDescription.ReadOnly = false;
            //    txtReasonDescription.Enabled = true;
            //    txtApprovedQty.Enabled = true;

            //    if (m_objInventoryAdj == null || m_objInventoryAdj.StatusId == (int)Common.InventoryStatus.Approved || m_objInventoryAdj.StatusId == (int)Common.InventoryStatus.Rejected)
            //    {
            //        txtReasonDescription.Enabled = false;
            //        txtApprovedQty.Enabled = false;
            //        btnApproved.Enabled = false;
            //        btnReject.Enabled = false;
            //    }
            //}
        }
        /// <summary>
        /// Reset Item When Change of Tab Control
        /// </summary>
        /// <param name="e"></param>
        void tabControlSelect(TabControlCancelEventArgs e)
        {
            if ((tabControlTransaction.SelectedIndex == 0) && dgvInventoryItem.Rows.Count > 0)
            {
                DialogResult result = MessageBox.Show(Common.GetMessage("VAL0026"), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Cancel | DialogResult.No == result)
                {
                    tabControlTransaction.SelectedIndex = 0;
                    e.Cancel = true;
                }
                else if (result == DialogResult.Yes)
                {
                    ResetInventoryAndItems();
                    tabControlTransaction.TabPages[1].Text = Common.TAB_CREATE_MODE;
                    ////ReadOnlyTOIField();
                }
            }
            else if (tabControlTransaction.SelectedIndex == 1)
            {
                if (tabControlTransaction.TabPages[1].Text == Common.TAB_CREATE_MODE)
                {
                    ResetInventoryAndItems();
                    EnableDisableButton((int)Common.TIStatus.New);
                    ReadOnlyItem();

                }

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
                errInventory.SetError(cmb, Common.GetMessage("INF0026", lbl.Text.Trim().Substring(0, lbl.Text.Trim().Length - 2)));
            else
                errInventory.SetError(cmb, string.Empty);
        }

        /// <summary>
        /// To Enable alll buttons
        /// </summary>
        void EnableAllButton()
        {
            btnCreateReset.Enabled = true;
            btnSave.Enabled = true;
            btnInitiated.Enabled = true;
            btnApproved.Enabled = true;
            btnAddDetails.Enabled = true;
            btnReject.Enabled = true;
            btnCancel.Enabled = true;
            //chkExportIn.Enabled = false;
        }

        /// <summary>
        /// To enable and disable button, when status is changed
        /// </summary>
        /// <param name="statusId"></param>
        void EnableDisableButton(int statusId)
        {
            EnableAllButton();
            if (statusId == (int)Common.InventoryStatus.New)
            {
                btnInitiated.Enabled = false;
                btnApproved.Enabled = false;
                btnReject.Enabled = false;
                btnCancel.Enabled = false;
                //if (m_locationType == (int)Common.LocationConfigId.HO)
                //    btnAddDetails.Enabled = false;
            }
            else if (statusId == (int)Common.InventoryStatus.Created)
            {
                //if (m_locationType == (int)Common.LocationConfigId.BO || m_locationType == (int)Common.LocationConfigId.WH)
                //{
                btnApproved.Enabled = false;
                btnReject.Enabled = false;
                btnCancel.Enabled = false;
                //}
                //else if (m_locationType == (int)Common.LocationConfigId.HO)
                //{
                //    btnReject.Enabled = false;
                //}

            }
            else if (statusId == (int)Common.InventoryStatus.Initiated)
            {
                btnInitiated.Enabled = false;
                btnSave.Enabled = false;
                btnCancel.Enabled = false;
                //if (m_locationType == (int)Common.LocationConfigId.BO || m_locationType == (int)Common.LocationConfigId.WH)
                //{
                btnAddDetails.Enabled = false;
                //txtApprovedBy.Enabled = false;
                //txtReasonDescription.Enabled = false;
                //}
            }
            else if ((statusId == (int)Common.InventoryStatus.Approved) || (statusId == (int)Common.InventoryStatus.Rejected))
            {
                btnInitiated.Enabled = false;
                btnAddDetails.Enabled = false;
                btnSave.Enabled = false;
                btnApproved.Enabled = false;
                btnReject.Enabled = false;
                btnCancel.Enabled = false;
            }

            //if (m_locationType == (int)Common.LocationConfigId.HO)
            //    btnCreateReset.Enabled = false;

            btnReject.Enabled = btnReject.Enabled & m_isRejectAvailable;
            btnApproved.Enabled = btnApproved.Enabled & m_isApprovedAvailable;
            btnInitiated.Enabled = btnInitiated.Enabled & m_isInitiatedAvailable;
            btnSearch.Enabled = m_isSearchAvailable;
            btnSave.Enabled = btnSave.Enabled & m_isSaveAvailable;
            btnCancel.Enabled = btnCancel.Enabled & m_isCancelAvailable;
        }

        /// <summary>
        /// Validate Starting Amount
        /// </summary>
        void ValidateQuantity(TextBox txt, Label lbl, ErrorProvider ep)
        {
            bool isValidQuantity = CoreComponent.Core.BusinessObjects.Validators.IsValidQuantity(txt.Text);

            if (isValidQuantity == false)
                ep.SetError(txt, Common.GetMessage("VAL0009", lbl.Text.Trim().Substring(0, lbl.Text.Trim().Length - 2)));
            else if (Convert.ToDecimal(txt.Text) <= 0)
                ep.SetError(txt, Common.GetMessage("VAL0009", lbl.Text.Trim().Substring(0, lbl.Text.Trim().Length - 2)));
            else
                ep.SetError(txt, string.Empty);
        }

        /// <summary>
        /// Remove Location Contact Record
        /// </summary>
        /// <param name="e"></param>
        void RemoveItem(DataGridViewCellEventArgs e)
        {
            if ((e.RowIndex >= 0) && (dgvInventoryItem.Rows[e.RowIndex].Cells[e.ColumnIndex].GetType() == typeof(DataGridViewImageCell)))
            {
                if ((m_objInventoryAdj == null) || (m_objInventoryAdj != null && Convert.ToInt32(m_objInventoryAdj.StatusId) == (int)Common.InventoryStatus.New))
                {
                    DialogResult saveResult = MessageBox.Show(Common.GetMessage("INF0028"), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (saveResult == DialogResult.Yes)
                    {
                        if (m_lstInvDetail.Count > 0)
                        {
                            dgvInventoryItem.DataSource = null;
                            m_lstInvDetail.RemoveAt(e.RowIndex);
                            dgvInventoryItem.DataSource = m_lstInvDetail;
                            dgvInventoryItem.Select();
                            ResetItemControl();
                        }
                        //cmbLocation.Enabled = true;
                    }
                }
                else
                {
                    MessageBox.Show(Common.GetMessage("VAL0049"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
        }

        /// <summary>
        /// Return Inventory Status
        /// </summary>
        /// <param name="seqNo"></param>
        /// <returns></returns>
        int GetAdjustStatus(string seqNo)
        {
            List<InventoryAdjust> lstInventory = new List<InventoryAdjust>();
            DateTime fromDate = Common.DATETIME_NULL;
            DateTime toDate = Common.DATETIME_MAX;

            List<InventoryAdjust> lst = new List<InventoryAdjust>();
            lst = Search(Convert.ToInt32(cmbLocation.SelectedValue), seqNo, Common.INT_DBNULL, fromDate.ToString(), toDate.ToString(), Common.INT_DBNULL, Common.INT_DBNULL, Common.INT_DBNULL);

            if (lst != null && lst.Count > 0)
            {
                m_objInventoryAdj = lst[0];
                m_seqNo = m_objInventoryAdj.SeqNo;
                return lst[0].StatusId;
            }
            else
                return Common.INT_DBNULL;
        }

        /// <summary>
        /// Reset Inventory and Item Controls
        /// </summary>
        void ResetInventoryAndItems()
        {
            dtpDate.Enabled = true;
            dtpDate.Value = Convert.ToDateTime(System.DateTime.Now.ToShortDateString());
            dtpDate.Checked = false;
            EmptyErrorProvider();
            txtStatus.Text = Common.TOStatus.New.ToString();
            txtAdjustmentNo.Text = string.Empty;

            m_lstInvDetail = null;
            m_objInventoryAdj = null;

            txtApprovedBy.Text = string.Empty;
            txtInitiatedBy.Text = string.Empty;
            ResetItemControl();
            dgvInventoryItem.DataSource = new List<ItemInventory>();
            ReadOnlyItem();

            if (m_locationType == (int)Common.LocationConfigId.HO)
            {
                cmbLocation.Enabled = true;

                if (cmbLocation.Items.Count > 0)
                {
                    cmbLocation.SelectedIndexChanged -= new EventHandler(cmbLocation_SelectedIndexChanged);
                    cmbLocation.SelectedIndex = 0;
                    cmbLocation.SelectedIndexChanged += new EventHandler(cmbLocation_SelectedIndexChanged);
                }

                txtLocationAddress.Text = string.Empty;
                txtLocationCode.Text = string.Empty;
                chkbatchadj.Enabled = true;
            }
        }

        /// <summary>
        /// Validate Initiate Date
        /// </summary>
        void ValidateInitiateDate()
        {
            if (dtpDate.Checked == false)
                errInventory.SetError(dtpDate, Common.GetMessage("VAL0002", lblInitiatedDate.Text.Trim().Substring(0, lblInitiatedDate.Text.Trim().Length - 2)));
            else if (dtpDate.Checked == true)
            {
                DateTime expectedDate = dtpDate.Checked == true ? Convert.ToDateTime(dtpDate.Value) : Common.DATETIME_NULL;
                DateTime dt = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                TimeSpan ts = expectedDate - dt;
                if (ts.Days > 0)
                    errInventory.SetError(dtpDate, Common.GetMessage("INF0010", lblInitiatedDate.Text.Trim().Substring(0, lblInitiatedDate.Text.Trim().Length - 2)));
                else
                    errInventory.SetError(dtpDate, string.Empty);
            }
        }

        private bool ValidateAvailableItemQuantity()
        {
            InventoryAdjust objInv = new InventoryAdjust();
            bool ret = true;
            string error = string.Empty;
            decimal underProcess = 0;
            decimal available = 0;
            foreach (ItemInventory inv in m_lstInvDetail)
            {
                available = GetAvailableQty(inv.BatchNo, inv.ItemCode, inv.FromBucketId.ToString());
                underProcess = objInv.GetInventoryAdjItemQty(Convert.ToInt32(cmbLocation.SelectedValue), inv.ItemCode, inv.FromBucketId, inv.BatchNo, (int)Common.InventoryStatus.Initiated, ref error);
                //if ((available - underProcess - inv.ApprovedQty) < 0)
                //AK
                if ((available - inv.ApprovedQty) < 0)
                {
                    MessageBox.Show(Common.GetMessage("VAL0128"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ret = false;
                    break;
                }
            }
            return ret;
        }


        /// <summary>
        /// Save Inventory and its Items
        /// </summary>
        /// <param name="statusId"></param>
        void Save(int statusId)
        {
            if (m_lstInvDetail == null || m_lstInvDetail.Count == 0)
            {
                MessageBox.Show(Common.GetMessage("VAL0024", lblItemCode.Text.Trim().Substring(0, lblItemCode.Text.Trim().Length - 2)), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (statusId == (int)Common.InventoryStatus.Initiated)
            {
                if (!ValidateAvailableItemQuantity())
                    return;
            }

            if (statusId == (int)Common.InventoryStatus.Approved)
            {
                dtpDate.Checked = true;
                dtpDate.Value = Convert.ToDateTime(System.DateTime.Now.ToShortDateString());
            }

            #region Check Errors, If Validation fails Show Error and Return From Prog.
            EmptyErrorProvider();
            ValidateCombo(cmbLocation, lblLocation);
            if (m_objInventoryAdj != null && (statusId == (int)Common.InventoryStatus.Initiated || statusId == (int)Common.InventoryStatus.Approved))
                ValidateInitiateDate();


            StringBuilder sbError = new StringBuilder();
            sbError = GenerateError();

            //If Validation fails Show Error and Return From Prog. 
            if (!sbError.ToString().Trim().Equals(string.Empty))
            {
                MessageBox.Show(sbError.ToString(), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            #endregion

            MemberInfo[] memberInfos = typeof(Common.InventoryStatus).GetMembers(BindingFlags.Public | BindingFlags.Static);

            // Confirmation Before Saving
            DialogResult saveResult = MessageBox.Show(Common.GetMessage("5010", Common.GetConfirmationStatusText(memberInfos[statusId].Name)), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (saveResult == DialogResult.Yes)
            {
                if (m_objInventoryAdj != null && m_seqNo != string.Empty)
                {
                    if (m_lstInvHeader != null)
                    {
                        var query = (from p in m_lstInvHeader where p.SeqNo == m_seqNo select p);
                        if (query.ToList().Count > 0)
                            m_objInventoryAdj = query.ToList()[0];
                    }
                    m_objInventoryAdj.CreatedBy = Common.INT_DBNULL;
                    m_objInventoryAdj.CreatedDate = string.Empty;
                }
                if (m_objInventoryAdj == null)
                {
                    m_objInventoryAdj = new InventoryAdjust();
                    m_objInventoryAdj.SeqNo = string.Empty;
                    m_objInventoryAdj.StatusId = (int)Common.InventoryStatus.Created;

                    m_objInventoryAdj.CreatedBy = m_userId;
                    m_objInventoryAdj.CreatedDate = System.DateTime.Now.ToString(Common.DATE_TIME_FORMAT);

                    m_objInventoryAdj.ModifiedBy = m_userId;
                    m_objInventoryAdj.ModifiedDate = Convert.ToDateTime(m_objInventoryAdj.ModifiedDate).ToString(Common.DATE_TIME_FORMAT);//.ToString(Common.DATE_TIME_FORMAT);
                }

                if (m_objInventoryAdj != null)
                {
                    if (m_objInventoryAdj.ModifiedDate.Length > 0)
                        m_objInventoryAdj.ModifiedDate = Convert.ToDateTime(m_objInventoryAdj.ModifiedDate).ToString(Common.DATE_TIME_FORMAT);//.ToString(Common.DATE_TIME_FORMAT);
                }

                DateTime receivedDate = dtpDate.Checked == true ? Convert.ToDateTime(dtpDate.Value) : Common.DATETIME_NULL;
                m_objInventoryAdj.InitiatedDate = Convert.ToDateTime(receivedDate).ToString(Common.DATE_TIME_FORMAT);
                m_objInventoryAdj.LocationId = m_currentLocationId;
                m_objInventoryAdj.SourceLocationId = Convert.ToInt32(cmbLocation.SelectedValue);
                m_objInventoryAdj.ModifiedBy = m_userId;
                m_objInventoryAdj.StatusId = statusId;
                m_objInventoryAdj.InventoryItem = m_lstInvDetail;
                m_objInventoryAdj.LocationType = m_locationType;
                m_objInventoryAdj.InterbatchAdj = (chkbatchadj.Checked == true ? 1 : 0);

                string errorMessage = string.Empty;

                bool result = m_objInventoryAdj.Save(Common.ToXml(m_objInventoryAdj), ref errorMessage);

                if (errorMessage.Equals(string.Empty))
                {
                    ResetItemControl();
                    m_seqNo = m_objInventoryAdj.SeqNo;
                    txtAdjustmentNo.Text = m_seqNo;

                    EnableDisableButton(GetAdjustStatus(m_objInventoryAdj.SeqNo));
                    BindChangedControls(m_objInventoryAdj);

                    ReadOnlyItem();
                    dgvInventoryItem.DataSource = new List<ItemInventory>();
                    if (m_lstInvDetail != null && m_lstInvDetail.Count > 0)
                    {
                        dgvInventoryItem.SelectionChanged -= new System.EventHandler(dgvInventoryItem_SelectionChanged);

                        m_lstInvDetail = SearchTIItem(m_objInventoryAdj.SeqNo);
                        //dgvInventoryItem.DataSource = m_lstInvDetail;
                        dgvInventoryItem.SelectionChanged += new System.EventHandler(dgvInventoryItem_SelectionChanged);
                        dgvInventoryItem.ClearSelection();

                        dgvInventoryItem.DataSource = m_lstInvDetail;

                        //cmbLocation.Enabled = true;
                        chkExportIn.Checked = ((m_lstInvDetail != null && m_lstInvDetail[0].Export == 1) ? true : false);
                        chkbatchadj.Checked = false;// ((m_lstInvDetail != null && m_lstInvDetail[0].InterBatchAdj == 1) ? true : false);

                        //cmbLocation.Enabled = false;
                        cmbReasonCode.Enabled = false;
                        //ReadOnlyItem();
                        dtpDate.Enabled = true;// (m_objInventoryAdj.StatusId != (int)Common.InventoryStatus.Initiated) ? true : false;

                    }
                    //MessageBox.Show(Common.GetMessage("8001"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MessageBox.Show(Common.GetMessage("8013", memberInfos[statusId].Name, m_objInventoryAdj.SeqNo), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (errorMessage.Equals("8004"))
                {
                    MessageBox.Show(Common.GetMessage("8004"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    EnableDisableButton(GetAdjustStatus(m_objInventoryAdj.SeqNo));
                }
                else
                    MessageBox.Show(Common.GetMessage(errorMessage), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Creates dataset of data to  be printed in TI Report 
        /// </summary>
        private void CreatePrintDataSet()
        {
            m_printDataSet = new DataSet();
            // Get Data For TOI Header Informaton in Stock Adjustment Screen Report
            DataTable dtInventoryAdjHeader = new DataTable("InventoryAdjHeader");
            DataColumn Location = new DataColumn("Location", System.Type.GetType("System.String"));
            DataColumn LocationName = new DataColumn("LocationName", System.Type.GetType("System.String"));
            DataColumn LocationAddress = new DataColumn("LocationAddress", System.Type.GetType("System.String"));
            DataColumn Status = new DataColumn("Status", System.Type.GetType("System.String"));
            DataColumn InitiatedBy = new DataColumn("InitiatedBy", System.Type.GetType("System.String"));
            DataColumn InitiatedDate = new DataColumn("InitiatedDate", System.Type.GetType("System.String"));
            DataColumn ApprovedRejectedBy = new DataColumn("ApprovedRejectedBy", System.Type.GetType("System.String"));
            dtInventoryAdjHeader.Columns.Add(Location);
            dtInventoryAdjHeader.Columns.Add(LocationName);
            dtInventoryAdjHeader.Columns.Add(LocationAddress);
            dtInventoryAdjHeader.Columns.Add(Status);
            dtInventoryAdjHeader.Columns.Add(InitiatedDate);
            dtInventoryAdjHeader.Columns.Add(InitiatedBy);
            dtInventoryAdjHeader.Columns.Add(ApprovedRejectedBy);
            DataRow dRow = dtInventoryAdjHeader.NewRow();
            dRow["Location"] = cmbLocation.Text;
            dRow["LocationName"] = cmbLocation.Text;
            dRow["LocationAddress"] = m_objInventoryAdj.LocationAddress;
            dRow["Status"] = m_objInventoryAdj.StatusName;
            dRow["InitiatedBy"] = m_objInventoryAdj.InitiatedName;
            dRow["InitiatedDate"] = Convert.ToDateTime(m_objInventoryAdj.InitiatedDate).ToString(Common.DTP_DATE_FORMAT);
            dRow["ApprovedRejectedBy"] = m_objInventoryAdj.ApprovedName;
            dtInventoryAdjHeader.Rows.Add(dRow);
            // Search ItemData for dataTable
            DataTable dtInventoryAdjDetail = new DataTable("InventoryAdjDetail");
            dtInventoryAdjDetail = m_objInventoryAdj.SearchItemDataTable(m_objInventoryAdj.SeqNo);
            for (int i = 0; i < dtInventoryAdjDetail.Rows.Count; i++)
            {
                dtInventoryAdjDetail.Rows[i]["Quantity"] = Math.Round(Convert.ToDecimal(dtInventoryAdjDetail.Rows[i]["Quantity"]), Common.DisplayQtyRounding, MidpointRounding.AwayFromZero);
                dtInventoryAdjDetail.Rows[i]["ApprovedQty"] = Math.Round(Convert.ToDecimal(dtInventoryAdjDetail.Rows[i]["ApprovedQty"]), Common.DisplayQtyRounding, MidpointRounding.AwayFromZero);
            }
            m_printDataSet.Tables.Add(dtInventoryAdjHeader);
            m_printDataSet.Tables.Add(dtInventoryAdjDetail.Copy());
        }
        /// <summary>
        /// Prints TO Screen report
        /// </summary>
        private void PrintReport()
        {
            CreatePrintDataSet();
            CoreComponent.UI.ReportScreen reportScreenObj = new CoreComponent.UI.ReportScreen((int)Common.ReportType.StockAdjustment, m_printDataSet);
            reportScreenObj.ShowDialog();
            //reportScreenObj.PrintReport();
            m_printDataSet = null;
        }

        private void txtApprovedQty_TextChanged(object sender, EventArgs e)
        {
            try
            {

                //bool isValidQuantity = CoreComponent.Core.BusinessObjects.Validators.IsValidQuantity(txtApprovedQty.Text);
                //{
                //    if (isValidQuantity)
                //    {
                //        decimal quantity = Convert.ToDecimal(txtApprovedQty.Text);
                //    }
                //}
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Events
        /// <summary>
        /// To Initiate Inventory
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInitiated_Click(object sender, EventArgs e)
        {
            try
            {
                Save((int)Common.InventoryStatus.Initiated);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Save Inventory with Approved Status
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnApproved_Click(object sender, EventArgs e)
        {
            try
            {
                Save((int)Common.InventoryStatus.Approved);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Reset Inventory and its Item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreateReset_Click(object sender, EventArgs e)
        {
            try
            {
                tabControlTransaction.TabPages[1].Text = Common.TAB_CREATE_MODE;
                ResetInventoryAndItems();
                EnableDisableButton((int)Common.InventoryStatus.New);
                cmbLocation.Focus();
                m_TobatchNo = string.Empty;
                m_Mfgdate = string.Empty;
                m_Expdate = string.Empty;
                ItemInventory.BatchDetailList = null;
                chkExportIn.Checked = false;
                grpBoxFrom.Enabled = false;
                chkExportIn.Enabled = (m_locationType == (int)Common.LocationConfigId.HO || m_locationType == (int)Common.LocationConfigId.WH ? true : false);
                //chkExportIn.Enabled = true;

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
        /// Assign Defalult value for approved qty is assign Qty
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtQty_Leave(object sender, EventArgs e)
        {
            try
            {
                //m_itemDetail.ApprovedQty = Convert.ToDecimal(txtQty.Text);
                txtApprovedQty.Text = txtQty.Text;
                errInventory.SetError(txtQty, string.Empty);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Call save fn. with Reject Status
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReject_Click(object sender, EventArgs e)
        {
            try
            {
                Save((int)Common.InventoryStatus.Rejected);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Call fn. ClearBatchAndQuantityInfo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbFromBucket_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboBox frmbox = (ComboBox)sender;
                if (frmbox.Name == "cmbFromBucket")
                {
                   // ClearBatchAndQuantityInfo(true);
                    GetItem_BatchQty(true);
                    //if (availquan != 0)
                    //    txtAvailableQty.Text = (availquan.ToString() != "0" ? availquan.ToString() : null);
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

        bool CheckApprovedQuantity()
        {
            decimal quantity = Convert.ToDecimal(txtApprovedQty.Text);

            if (quantity <= m_itemDetail.Quantity)
            {
                m_itemDetail.ApprovedQty = quantity;
                return true;
            }
            return false;

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

                if (grpBoxFrom.Enabled)
                {
                    if (!string.IsNullOrEmpty(txtItemCode.Text.Trim()) && !string.IsNullOrEmpty(txtToitemcode.Text.Trim()) &&
                        string.Compare(txtItemCode.Text.Trim(), txtToitemcode.Text.Trim(), true) == 0)
                    {
                        if (chkbatchadj.Checked != true)
                        {
                            MessageBox.Show(Common.GetMessage("40035"), Common.GetMessage("40035"), MessageBoxButtons.OK, MessageBoxIcon.Error);

                            // MessageBox.Show("Same Item Code");
                            return;
                        }
                    }
                    if (cmbFromBucket.SelectedIndex > 0 && cmbToBucket.SelectedIndex > 0 &&
                        string.Compare(cmbFromBucket.SelectedValue.ToString(), cmbToBucket.SelectedValue.ToString()) != 0)
                    {
                        MessageBox.Show(Common.GetMessage("40036"), Common.GetMessage("40036"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //MessageBox.Show("Can't be export with in diffrent bucket");
                        return;
                    }
                    //txtToBatchNo.Text = txtBatchNo.Text;
                   
                }
               
                EmptyErrorProvider();

                #region Check Errors, If Validation fails Show Error and Return From Prog.

                ValidateMessages();

                StringBuilder sbError = new StringBuilder();
                sbError = GenerateError();

                //If Validation fails Show Error and Return From Prog. 
                if (!sbError.ToString().Trim().Equals(string.Empty))
                {
                    MessageBox.Show(sbError.ToString(), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                #endregion

                // User can not add any quantity after Initiated State
                //if (m_locationType == (int)Common.LocationConfigId.HO && m_selectedItemRowIndex == Common.INT_DBNULL)
                //{
                //    MessageBox.Show(Common.GetMessage("VAL0048", this.Text), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return;
                //}

                if (Convert.ToInt32(cmbFromBucket.SelectedValue) == Convert.ToInt32(cmbToBucket.SelectedValue) && !grpBoxFrom.Enabled)
                {
                    MessageBox.Show(Common.GetMessage("VAL0050"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (chkbatchadj.Checked == true)
                {
                    if (Convert.ToInt32(cmbFromBucket.SelectedValue) != Convert.ToInt32(cmbToBucket.SelectedValue) && grpBoxFrom.Enabled)
                    {
                        MessageBox.Show(Common.GetMessage("VAL0050"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                bool b = CheckValidQuantity();

                if (b == false && m_isDuplicateRecordFound <= 0)
                //if (b == false && m_isDuplicateRecordFound <= 0 && m_objInventoryAdj.StatusId < (int)Common.InventoryStatus.Initiated)
                {
                    MessageBox.Show(Common.GetMessage("VAL0052"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                else if (m_objInventoryAdj != null && m_objInventoryAdj.StatusId == (int)Common.InventoryStatus.Initiated)
                {
                    if (txtApprovedQty.Text.Trim().Length > 0 && txtQty.Text.Trim().Length > 0 && Convert.ToDecimal(txtApprovedQty.Text.Trim()) > Convert.ToDecimal(txtQty.Text.Trim()))
                    {
                        MessageBox.Show(Common.GetMessage("VAL0051"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                if ((b && m_isDuplicateRecordFound <= 0))
                {
                    b = CheckApprovedQuantity();
                    if (b == false)
                    {
                        MessageBox.Show(Common.GetMessage("VAL0089"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (CheckAlreadyAllocatedQuantity() == false)
                        return;

                    if (AddItem())
                    {
                        dgvInventoryItem.DataSource = null;
                        if (m_lstInvDetail.Count > 0)
                        {
                            dgvInventoryItem.DataSource = m_lstInvDetail;
                        }
                        ResetItemControl();
                        if (chkExportIn.Checked)
                        {
                            cmbReasonCode.SelectedValue = 10;
                            cmbReasonCode.Enabled = false;
                            txtToitemcode.Enabled = true;
                            //
                            txtToBatchNo.Enabled = true;
                            //
                            grpBoxTo.Text = "From";
                        }
                        else if (chkbatchadj.Checked == true)
                        {

                            txtToitemcode.Enabled = true;
                            //
                            txtToBatchNo.Enabled = true;
                            //
                        }
                        else
                            txtToitemcode.Enabled = false;
                        

                    }
                }
                
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        bool CheckAlreadyAllocatedQuantity()
        {
            //m_lstInvDetail
            if (m_lstInvDetail != null)
            {

                List<ItemInventory> lstItemInventory = CopyItemDetail(m_selectedItemRowIndex, m_lstInvDetail);

                var lstItemGroupBy = lstItemInventory.GroupBy(x => new { x.ItemId, x.FromBucketId, x.BatchNo })
                                        .Select(g => new
                                        {
                                            ItemID = g.First<ItemInventory>().ItemId,
                                            FromBucketId = g.First<ItemInventory>().FromBucketId,
                                            BatchNo = g.First<ItemInventory>().BatchNo,
                                            TotalApprovedQty = g.Sum(p => p.ApprovedQty)
                                        }
                                        );
                //List<ItemGroup> lst;
                if (lstItemGroupBy.ToList().Count > 0)
                {
                    //lst = (List<ItemGroup>)lstItemGroupBy;


                    //List<ItemInventory> lst = new List<ItemInventory>();
                    //lst = List<ItemInventory>(lstItemGroupBy.ToList());


                    var lstItemGroupByNew = (from p in lstItemGroupBy.ToList()
                                             where p.FromBucketId == Convert.ToInt32(cmbFromBucket.SelectedValue) &&
                                                Convert.ToInt32(p.ItemID) == Convert.ToInt32(m_itemId) && p.BatchNo.ToString() == m_batchNo.ToString()
                                             select p.TotalApprovedQty).Sum();

                    //var lstItemGroupByNew = from p in lst
                    //                        where p.FromBucketId == Convert.ToInt32(cmbFromBucket.SelectedValue) &&
                    //                           Convert.ToInt32(p.ItemID) == Convert.ToInt32(m_itemId) && p.BatchNo.ToString() == m_batchNo.ToString()
                    //                        select p.TotalApprovedQty;

                    decimal availableQty = GetAvailableQty(m_batchNo, txtItemCode.Text.Trim().ToString(), cmbFromBucket.SelectedValue.ToString());

                    if (Convert.ToDecimal(lstItemGroupByNew) > 0)
                        if (Convert.ToDecimal(lstItemGroupByNew) + Convert.ToDecimal(txtApprovedQty.Text) > availableQty)
                        {
                            if (m_locationType == (int)Common.LocationConfigId.HO)
                                MessageBox.Show(Common.GetMessage("VAL0107"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            else
                                MessageBox.Show(Common.GetMessage("VAL0052"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);

                            return false;
                        }
                }
            }
            return true;
        }


        /// <summary>
        /// Call fn. SelectGridRow 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvInventoryItem_SelectionChanged(object sender, EventArgs e)
        {
            //DataGridView dv = (DataGridView)sender;
            try
            {
                SelectGridRow(e);
                ItemBatchDetails.Spcall = 0;
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// m_F4Press = false, used for BatchInfo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtQty_Enter(object sender, EventArgs e)
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
        /// Clear Item slection for grid and clear items controls values
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearDetails_Click(object sender, EventArgs e)
        {
            try
            {
                ItemBatchDetails.Spcall = 0;
                ResetItemControl();
                ReadOnlyItem();
                txtItemCode.Focus();
                if (dgvInventoryItem.Rows.Count == 0)
                {
                    chkExportIn.Checked = false;
                    cmbReasonCode.DataSource = GenericReasoncode;
                    cmbReasonCode.Enabled = true;
                    grpBoxFrom.Enabled = false;
                    cmbReasonCode.SelectedIndex = 0;
                    dgvInventoryItem.Columns["ToItemCode"].Visible = false;
                    dgvInventoryItem.Columns["ToItemName"].Visible = false;
                    dgvInventoryItem.Columns["ToBatchNo"].Visible = false;
                    txtToitemcode.Text = string.Empty;
                    txtToBatchNo.Text = string.Empty;
                    txtToItemDesc.Text = string.Empty;

                    chkbatchadj.Enabled = false;// (m_locationType == (int)Common.LocationConfigId.HO ? true : false);
                    btnbatch.Enabled = false;
                }
                //chkExportIn.Enabled = false;


            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Remove Item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvInventoryItem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                RemoveItem(e);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Call fn. EditInventoryItem
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvInventory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                EditInventoryItem(e);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Save Inventory and its item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Save((int)Common.InventoryStatus.Created);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Call fun ValidateLocationAddress To Show Address Details 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Save((int)Common.InventoryStatus.Cancelled);
                ResetInventoryAndItems();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Print Button Click 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_objInventoryAdj != null && m_objInventoryAdj.StatusId >= (int)Common.InventoryStatus.Approved)
                {
                    btnPrint.Enabled = false;
                    PrintReport();
                    btnPrint.Enabled = true;
                }
                else
                    MessageBox.Show(Common.GetMessage("INF0101", "Adjustment", Common.InventoryStatus.Approved.ToString()), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                btnPrint.Enabled = true;
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
                dgvInventory.DataSource = new List<InventoryAdjust>();
                txtSeqNo.Focus();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbToBucket_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbToBucket.SelectedIndex > 0)
                    errInventory.SetError(cmbToBucket, string.Empty);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbReasonCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {


                grpBoxTo.Text = "";
                //dgvInventoryItem.Columns["ToItemCode"].Visible = false;
                //dgvInventoryItem.Columns["ToItemName"].Visible = false;
                if (cmbReasonCode.SelectedIndex > 0)
                {
                    errInventory.SetError(cmbReasonCode, string.Empty);
                    if (cmbReasonCode.SelectedValue.ToString() == "10" && chkExportIn.Enabled)
                    {



                        //chkExportIn.Checked = true;

                    }
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dtpDate_Validated(object sender, EventArgs e)
        {
            try
            {
                if (dtpDate.Checked == true)
                    errInventory.SetError(dtpDate, string.Empty);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        private void dgvInventory_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (dgvInventory.SelectedRows.Count > 0)
                {
                    string SeqNo = Convert.ToString(dgvInventory.SelectedRows[0].Cells["SeqNo"].Value);

                    var query = (from p in m_lstInvHeader where p.SeqNo == SeqNo select p);
                    m_objInventoryAdj = (InventoryAdjust)query.ToList()[0];

                    SelectSearchGrid(SeqNo);
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkExportIn_Click(object sender, EventArgs e)
        {
            //if (dgvInventoryItem.RowCount > 0)
            //    chkExportIn.Checked = true;
            if (chkExportIn.Checked)
            {
                cmbReasonCode.DataSource = SelectReasoncode;
                cmbReasonCode.SelectedValue = 10;
                cmbReasonCode.Enabled = false;
                txtToitemcode.Enabled = true;
                txtToBatchNo.Enabled = true;
                grpBoxFrom.Enabled = true;
                dgvInventoryItem.Columns["ToItemCode"].Visible = true;
                dgvInventoryItem.Columns["ToItemName"].Visible = true;
                dgvInventoryItem.Columns["ToBatchNo"].Visible = true;
                chkbatchadj.Enabled = false;
                btnbatch.Enabled = (grpBoxFrom.Enabled == true ? false : false);

                grpBoxTo.Text = "From";
            }
            else
            {
                cmbReasonCode.DataSource = GenericReasoncode;
                cmbReasonCode.Enabled = true;
                grpBoxFrom.Enabled = false;
                cmbReasonCode.SelectedIndex = 0;
                dgvInventoryItem.Columns["ToItemCode"].Visible = false;
                dgvInventoryItem.Columns["ToItemName"].Visible = false;
                dgvInventoryItem.Columns["ToBatchNo"].Visible = false;
                txtToitemcode.Text = string.Empty;
                txtToBatchNo.Text = string.Empty;
                txtToItemDesc.Text = string.Empty;

                chkbatchadj.Enabled = false;// (m_locationType == (int)Common.LocationConfigId.HO ? true : false);
                btnbatch.Enabled = false;
            }
        }

        private void txtToitemcode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtToBatchNo.Text = string.Empty;
                if (txtToitemcode.TextLength > 0)
                {
                    ItemBatchDetails ibd = new ItemBatchDetails();
                    ibd.ItemCode = txtToitemcode.Text;
                    ibd.LocationId = cmbLocation.SelectedValue.ToString();
                    ibd.BucketId = cmbToBucket.SelectedValue.ToString();
                    ibd.FromMfgDate = Common.DATETIME_NULL;
                    ibd.ToMfgDate = Common.DATETIME_NULL;
                    List<ItemBatchDetails> lt = ibd.Search();

                    List<ItemBatchDetails> lt1 = null;
                    if (lt == null)
                    {
                        ItemBatchDetails.Spcall = 1;
                        lt1 = ibd.Search();

                    }
                    if ((lt != null) && (lt.Count > 0))
                    {
                        btnbatch.Enabled = true;
                        txtToBatchNo.Enabled = true;
                        txtToBatchNo.Text = null;
                        BatchExist = 1;
                        ItemBatchDetails.Spcall = 0;
                    }
                    else if ((lt1 != null) && (lt1.Count > 0))
                    {
                        btnbatch.Enabled = true;
                        txtToBatchNo.Enabled = true;
                        txtToBatchNo.Text = null;
                        BatchExist = 1;
                        ItemBatchDetails.Spcall = 1;
                    }
                    else
                    {
                        btnbatch.Enabled = false;
                        txtToBatchNo.Enabled = false;
                        BatchExist = 0;
                        txtToBatchNo.Text = txtBatchNo.Text;
                        //btnbatch.Enabled = true;
                    }
                }
                else
                {
                    btnbatch.Enabled = false;
                    txtToBatchNo.Enabled = false;
                    txtToBatchNo.Text = null;
                }



            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtToitemcode_Leave(object sender, EventArgs e)
        {

            //if( txtToBatchNo.Text== string.Empty )
            //    txtToBatchNo.Text = ((BatchExist == 1 ) ? null : txtBatchNo.Text);

            frmStockAdjustmentBatch.Itemcode = txtToitemcode.Text;

        }

        private void txtBatchNo_Leave(object sender, EventArgs e)
        {
            availquan = (txtAvailableQty.Text == string.Empty ? 0 : Convert.ToInt32(txtAvailableQty.Text));
        }



        private void txtToBatchNo_Click(object sender, EventArgs e)
        {
            ItemBatchDetails ibd = new ItemBatchDetails();
            ibd.ItemCode = txtToitemcode.Text;
            ibd.LocationId = cmbLocation.SelectedValue.ToString();
            ibd.BucketId = cmbToBucket.SelectedValue.ToString();
            ibd.FromMfgDate = Common.DATETIME_NULL;
            ibd.ToMfgDate = Common.DATETIME_NULL;
            List<ItemBatchDetails> lt = ibd.Search();

            List<ItemBatchDetails> lt1 = null;
            if (lt == null)
            {
                ItemBatchDetails.Spcall = 1;
                lt1 = ibd.Search();

            }
            if ((lt != null) && (lt.Count > 0))
            {

                ItemBatchDetails.Spcall = 0;
            }
            else if ((lt1 != null) && (lt1.Count > 0))
            {

                ItemBatchDetails.Spcall = 1;
            }
        }

        private void txtToBatchNo_Leave(object sender, EventArgs e)
        {
            if (availquan != 0)
                txtAvailableQty.Text = (availquan.ToString() != "0" ? availquan.ToString() : null);
        }

        private void chkbatchadj_Click(object sender, EventArgs e)
        {
            if (chkbatchadj.Checked)
            {
                cmbReasonCode.DataSource = SelectReasoncode;
                cmbReasonCode.SelectedValue = 11;
                cmbReasonCode.Enabled = false;
                txtToitemcode.Enabled = true;
                //
                txtToBatchNo.Enabled = true;
                //

                ItemDetails.IsInternaladj = 1;

                grpBoxFrom.Enabled = true;
                dgvInventoryItem.Columns["ToItemCode"].Visible = true;
                dgvInventoryItem.Columns["ToItemName"].Visible = true;
                dgvInventoryItem.Columns["ToBatchNo"].Visible = true;
                chkExportIn.Enabled = false;
                errInventory.SetError(txtItemCode, string.Empty);
                errInventory.SetError(txtToitemcode, string.Empty);
                errInventory.SetError(txtToBatchNo, string.Empty);
                errInventory.SetError(txtBatchNo, string.Empty);

                grpBoxTo.Text = "From";
            }
            else
            {
                cmbReasonCode.DataSource = GenericReasoncode;
                cmbReasonCode.Enabled = true;
                grpBoxFrom.Enabled = false;
                cmbReasonCode.SelectedIndex = 0;

                ItemDetails.IsInternaladj = 0;

                dgvInventoryItem.Columns["ToItemCode"].Visible = false;
                dgvInventoryItem.Columns["ToItemName"].Visible = false;
                dgvInventoryItem.Columns["ToBatchNo"].Visible = false;
                txtToitemcode.Text = string.Empty;
                txtToBatchNo.Text = string.Empty;
                txtToItemDesc.Text = string.Empty;
                chkExportIn.Enabled = true;
                errInventory.SetError(txtItemCode, string.Empty);
                errInventory.SetError(txtToitemcode, string.Empty);
                errInventory.SetError(txtToBatchNo, string.Empty);
                errInventory.SetError(txtBatchNo, string.Empty);
            }
        }

        private void btnbatch_Click(object sender, EventArgs e)
        {
            ItemInventory.BatchDetailList = null;
            frmStockAdjustmentBatch _batch = new frmStockAdjustmentBatch();
            _batch.ShowDialog();
            int a = frmStockAdjustmentBatch.CurrentSerialno;
            a = 0;
            if (ItemInventory.BatchDetailList != null)
            {
                if ((ItemInventory.BatchDetailList[a].Manufacure != string.Empty && ItemInventory.BatchDetailList[a].Expiry != string.Empty && ItemInventory.BatchDetailList[a].ManufactureBatchNo != string.Empty))
                {
                    //int a = frmStockAdjustmentBatch.CurrentSerialno;
                    txtToBatchNo.Text = ItemInventory.BatchDetailList[a].ManufactureBatchNo;
                    //m_TobatchNo = ItemInventory.BatchDetailList[a].ManufactureBatchNo.ToString();
                    m_TomfgbatchNo = ItemInventory.BatchDetailList[a].ManufactureBatchNo.ToString();
                    m_TobatchNo = null;
                    m_mfd = ItemInventory.BatchDetailList[a].Manufacure.ToString();
                    m_epd = ItemInventory.BatchDetailList[a].Expiry.ToString();
                    if (txtToBatchNo.Text == ItemInventory.BatchDetailList[a].ManufactureBatchNo)
                        errInventory.SetError(txtToBatchNo, string.Empty);
                }
            }


        }

        private void frmInventoryAdjustment_Load(object sender, EventArgs e)
        {
            chkExportIn.Enabled = (m_locationType == (int)Common.LocationConfigId.HO || m_locationType == (int)Common.LocationConfigId.WH ? true : false);

            chkbatchadj.Enabled = (m_locationType == (int)Common.LocationConfigId.HO? true : false);
            DataRow[] dtRows = null;
            dtRows = SelectReasoncode.Select("KeyCode1 <> 10");
            DataTable dts = dtRows.CopyToDataTable();
            dtRows = dts.Select("KeyCode1 <> 11");
            DataTable dtsf = dtRows.CopyToDataTable();
            GenericReasoncode = dtsf;
            cmbReasonCode.DataSource = dtsf;
        }

        private void txtReasonDescription_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
