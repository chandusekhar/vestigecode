using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PromotionsComponent.BusinessLayer;
using CoreComponent.BusinessObjects;
using CoreComponent.Core.BusinessObjects;
using System.Collections.Specialized;

namespace PromotionsComponent.UI
{
    public partial class frmGiftVoucher : CoreComponent.Core.UI.Transaction
    {

        #region Variables Declaration

        List<GiftVoucher> m_listSearch = null;
        private GiftVoucher m_CurrentVoucher = null;
        private string m_VoucherNo = string.Empty;
        private GiftVoucherDetail m_CurrentVoucherDetail = null;
        private List<GiftVoucherDetail> m_VoucherDetailList = null;
        private GiftVoucherItemDetail m_CurrentVoucherItemDetail = null;
        private List<GiftVoucherItemDetail> m_VoucherItemDetailList = null;
        CurrencyManager m_bindingMgr;
        AutoCompleteStringCollection _itemcollection = null;

        private System.Globalization.NumberFormatInfo m_nfi = null;
        private string m_strRoundingZeroesFormat = string.Empty;

        private enum Mode
        {
            View = 1,
            Add = 2,
            Reset = 3
        }
        private Mode m_modeWorkMode = Mode.Reset;

        private string CON_MODULENAME = string.Empty;
        private string GRIDVIEW_XML_PATH = Environment.CurrentDirectory + "\\App_Data\\GridViewDefinition.xml";
        private int m_LocationID = Common.CurrentLocationId;
        private string m_LocationCode = Common.LocationCode;
        private string m_UserName = null;
        private int m_userID = -1;
        private Common.LocationConfigId m_LocationType = (Common.LocationConfigId)Common.CurrentLocationTypeId;

        DataTable dtItems = null;

        #region Rights Variable
        private Boolean IsSaveAvailable = true;
        private Boolean IsUpdateAvailable = true;
        private Boolean IsSearchAvailable = true;
        private Boolean IsAddAvailable = true;
        #endregion


        #endregion

        #region Constant and Fields Name

        private const string CON_GRID_VOUCHERNO = "VoucherCode";

        #endregion

        public frmGiftVoucher()
        {
            try
            {
                InitializeComponent();
                InitailizeForm();
                m_nfi = new System.Globalization.NumberFormatInfo();
                m_nfi.PercentDecimalDigits = Common.DisplayQtyRounding;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        private void InitailizeForm()
        {
            try
            {
                if (this.Tag != null)
                {
                    CON_MODULENAME = this.Tag.ToString();//"PUR01";
                }
                if (AuthenticationComponent.BusinessObjects.Authenticate.LoggedInUser != null)
                {
                    m_userID = AuthenticationComponent.BusinessObjects.Authenticate.LoggedInUser.UserId;
                    m_UserName = AuthenticationComponent.BusinessObjects.Authenticate.LoggedInUser.UserName;
                }
                // Initailize rights
                InitializeRights();
                // initailize controls
                InitializeControl();
                // reset controls        
                ResetForm();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Initailize Rights
        private void InitializeRights()
        {
            try
            {
                if (m_UserName != null && !CON_MODULENAME.Equals(string.Empty))
                {
                    IsAddAvailable = AuthenticationComponent.BusinessObjects.Authenticate.IsFunctionAccessible(m_UserName, Common.LocationCode, CON_MODULENAME, Common.FUNCTIONCODE_ADD);
                    IsSaveAvailable = AuthenticationComponent.BusinessObjects.Authenticate.IsFunctionAccessible(m_UserName, Common.LocationCode, CON_MODULENAME, Common.FUNCTIONCODE_SAVE);
                    IsUpdateAvailable = AuthenticationComponent.BusinessObjects.Authenticate.IsFunctionAccessible(m_UserName, Common.LocationCode, CON_MODULENAME, Common.FUNCTIONCODE_UPDATE);
                    IsSearchAvailable = AuthenticationComponent.BusinessObjects.Authenticate.IsFunctionAccessible(m_UserName, Common.LocationCode, CON_MODULENAME, Common.FUNCTIONCODE_SEARCH);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Initailize Tab Controls
        protected void InitializeControl()
        {
            try
            {
                lblPageTitle.Text = "Gift Voucher";
                InitailizeGrids();
                btnSearch.Enabled = IsSearchAvailable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void InitailizeGrids()
        {
            try
            {
                //Search GridView
                dgvSearchGiftVoucher.AutoGenerateColumns = false;
                dgvSearchGiftVoucher.AllowUserToAddRows = false;
                dgvSearchGiftVoucher.AllowUserToDeleteRows = false;
                DataGridView dgvSearchGiftVoucherNew = Common.GetDataGridViewColumns(dgvSearchGiftVoucher, GRIDVIEW_XML_PATH);

                //Item-Details GridView
                dgvGiftVoucherItemDetails.AutoGenerateColumns = false;
                dgvGiftVoucherItemDetails.AllowUserToAddRows = false;
                dgvGiftVoucherItemDetails.AllowUserToDeleteRows = false;
                DataGridView dgvCreateItemDetailNew = Common.GetDataGridViewColumns(dgvGiftVoucherItemDetails, GRIDVIEW_XML_PATH);

                // Create GridView
                dgvGiftVoucherDetails.AutoGenerateColumns = false;
                dgvGiftVoucherDetails.AllowUserToAddRows = false;
                dgvGiftVoucherDetails.AllowUserToDeleteRows = false;

                DataGridView dgvGiftVoucherDetailsNew = Common.GetDataGridViewColumns(dgvGiftVoucherDetails, GRIDVIEW_XML_PATH);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ResetForm()
        {
            try
            {
                errorCreate.Clear();
                m_CurrentVoucher = null;
                m_CurrentVoucherDetail = null;
                m_CurrentVoucherItemDetail = null;
                m_VoucherDetailList = null;
                m_VoucherItemDetailList = null;
                if (m_VoucherNo.Equals(string.Empty))
                {
                    m_CurrentVoucher = GetVoucherObject();
                    m_CurrentVoucherDetail = new GiftVoucherDetail();
                    m_CurrentVoucherItemDetail = new GiftVoucherItemDetail();
                }
                else
                {
                    m_CurrentVoucher = GetVoucherObject(m_VoucherNo);
                    //m_VoucherDetailList = GetVoucherDetailObject(m_VoucherNo);
                    m_VoucherDetailList = m_CurrentVoucher.VoucherDetailList;
                    m_VoucherItemDetailList = m_CurrentVoucher.VoucherItemDetailList;
                    m_CurrentVoucherDetail = m_CurrentVoucher.VoucherDetailList[0];
                    m_CurrentVoucherItemDetail = m_CurrentVoucher.VoucherItemDetailList[0];
                }
                if (m_CurrentVoucher != null)
                {
                    //update items for 

                    SetDetailValues();
                    SetHeaderValues();
                    ResetGrid();
                    RefreshItemList();
                    SetFormProperty();
                }
                else
                {
                    MessageBox.Show(Common.GetMessage("30008"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControlTransaction.TabPages[0].Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        #region Methods

        private void SetFormProperty()
        {
            if (m_CurrentVoucher != null)
            {
                if (m_CurrentVoucher.VoucherCode != null)
                {
                    if (m_CurrentVoucher.VoucherCode.Trim().Equals(string.Empty))
                    {
                        tabCreate.Text = "Create";
                    }
                    else
                    {
                        tabCreate.Text = "View";
                    }
                    btnSave.Enabled = IsSaveAvailable;
                    btnAddDetails.Enabled = IsAddAvailable;
                    if (m_CurrentVoucherDetail != null && m_CurrentVoucherDetail.IsActive)
                    {
                        btnSave.Enabled = false;
                    }
                }
                else
                {
                    btnSave.Enabled = IsSaveAvailable;
                    btnAddDetails.Enabled = IsAddAvailable;
                    if (m_CurrentVoucherDetail != null && m_CurrentVoucherDetail.IsActive)
                    {
                        btnSave.Enabled = false;
                    }
                }
            }
        }

        private void RefreshItemList()
        {
            try
            {
                _itemcollection = new AutoCompleteStringCollection();
                dtItems = Common.ParameterLookup(Common.ParameterType.ItemGiftVoucher, new ParameterFilter("", 0, 0, 0));
                if (dtItems != null)
                {
                    for (int j = 0; j < dtItems.Rows.Count; j++)
                    {
                        _itemcollection.Add(Convert.ToString(dtItems.Rows[j]["ItemCode"]));
                    }
                }
                txtItemCode.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txtItemCode.AutoCompleteSource = AutoCompleteSource.CustomSource;
                if (_itemcollection != null)
                    txtItemCode.AutoCompleteCustomSource = _itemcollection;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private GiftVoucher GetVoucherObject()
        {
            try
            {
                GiftVoucher _voucher = new GiftVoucher();
                _voucher.CreatedBy = m_userID;
                _voucher.ModifiedBy = m_userID;
                return _voucher;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private GiftVoucher GetVoucherObject(string voucherNo)
        {
            try
            {
                GiftVoucher _voucher = new GiftVoucher();
                _voucher.VoucherCode = voucherNo;
                List<GiftVoucher> _lstVoucher = _voucher.Search();
                if (_lstVoucher != null && _lstVoucher.Count > 0)
                    return _lstVoucher[0];
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<GiftVoucherDetail> GetVoucherDetailObject(string voucherno)
        {
            try
            {
                GiftVoucherDetail detail = new GiftVoucherDetail();
                detail.GiftVoucherCode = voucherno;
                return detail.Search();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetDetailValues()
        {
            try
            {
                if (m_CurrentVoucherDetail != null)
                {
                    txtStartText.Text = m_CurrentVoucherDetail.StartSeries;
                    txtStartRange.Text = m_CurrentVoucherDetail.EndSeries;
                    txtEndRange.Text = m_CurrentVoucherDetail.EndSeries;
                    if (m_CurrentVoucherDetail.ApplicableFrom == Common.DATETIME_NULL.ToString("dd/MM/yyyy"))
                    {
                        dtpApplicableFromDate.Value = Common.DATETIME_CURRENT;
                        dtpApplicableFromDate.Checked = false;
                    }
                    else
                    {
                        dtpApplicableFromDate.Value = Convert.ToDateTime(m_CurrentVoucherDetail.ApplicableFrom);
                        dtpApplicableFromDate.Checked = true;
                    }
                    if (m_CurrentVoucherDetail.ApplicableTo == Common.DATETIME_NULL.ToString("dd/MM/yyyy"))
                    {
                        dtpApplicableToDate.Value = Common.DATETIME_CURRENT;
                        dtpApplicableToDate.Checked = false;
                    }
                    else
                    {
                        dtpApplicableToDate.Value = Convert.ToDateTime(m_CurrentVoucherDetail.ApplicableTo);
                        dtpApplicableToDate.Checked = true;
                    }
                    chkActive.Checked = m_CurrentVoucherDetail.IsActive;
                }
                else
                {
                    txtStartText.Text = string.Empty;
                    txtStartRange.Text = string.Empty;
                    txtEndRange.Text = string.Empty;
                    dtpApplicableFromDate.Value = Common.DATETIME_CURRENT;
                    dtpApplicableToDate.Value = Common.DATETIME_CURRENT;
                    chkActive.Checked = false;
                }

                //if (m_CurrentVoucherItemDetail != null)
                //{
                //    txtItemCode.Text = m_CurrentVoucherItemDetail.ItemCode;
                //    txtItemDescription.Text = m_CurrentVoucherItemDetail.ItemDescription;

                //    System.Globalization.NumberFormatInfo nfi = new System.Globalization.NumberFormatInfo();
                //    nfi.PercentDecimalDigits = Common.DisplayQtyRounding;
                //    string strRoundingZeroesFormat = Common.GetRoundingZeroes(Common.DisplayQtyRounding); //"0.00";
                //    txtItemQty.Text = m_CurrentVoucherItemDetail.Quantity.ToString(strRoundingZeroesFormat, nfi);
                //}
                //else
                //{
                //    txtItemCode.Text = string.Empty;
                //    txtItemDescription.Text = string.Empty;
                //    txtItemQty.Text = string.Empty;
                //}

                SetDetailFieldsProperty();

                if (m_CurrentVoucherDetail != null)
                {
                    if (m_CurrentVoucherDetail.SeriesID > 0)
                    {
                        EnableDisableDetailValues(false);
                    }
                    else
                    {
                        EnableDisableDetailValues(true);
                    }
                }
                else
                {
                    EnableDisableDetailValues(true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void EnableDisableDetailValues(bool isEnabled)
        {
            txtStartText.Enabled = isEnabled;
            txtStartRange.Enabled = isEnabled;
            txtEndRange.Enabled = isEnabled;

            //if (m_CurrentVoucherDetail != null)
            //{
            //    if (m_CurrentVoucherDetail.IsActive)
            //    {
            //    }
            //}
        }

        private void SetItemDetailValues()
        {
            if (m_CurrentVoucherItemDetail != null)
            {
                txtItemCode.Text = m_CurrentVoucherItemDetail.ItemCode;
                txtItemDescription.Text = m_CurrentVoucherItemDetail.ItemDescription;

                //System.Globalization.NumberFormatInfo nfi = new System.Globalization.NumberFormatInfo();
                //nfi.PercentDecimalDigits = Common.DisplayQtyRounding;
                //string strRoundingZeroesFormat = Common.GetRoundingZeroes(Common.DisplayQtyRounding); //"0.00";
                m_strRoundingZeroesFormat = Common.GetRoundingZeroes(Common.DisplayQtyRounding);

                txtItemQty.Text = m_CurrentVoucherItemDetail.Quantity.ToString(m_strRoundingZeroesFormat, m_nfi);
            }
            else
            {
                txtItemCode.Text = string.Empty;
                txtItemDescription.Text = string.Empty;
                txtItemQty.Text = string.Empty;
            }
        }

        private void EnableDisableItemDetails(bool isEnabled)
        {
            txtItemCode.ReadOnly = isEnabled;
            if (m_modeWorkMode == Mode.Add)
            {
                txtItemQty.ReadOnly = !isEnabled;
            }
            else
            {
                txtItemQty.ReadOnly = isEnabled;
            }

            //if (m_CurrentVoucherItemDetail != null)
            //{
            //    if (m_CurrentVoucherItemDetail.VoucherCode != null)
            //    {
            //        btnAddItemDetail.Enabled = false;
            //        btnClearItemDetail.Enabled = false;
            //    }
            //    else
            //    {
            //        if (m_VoucherItemDetailList != null)
            //        {
            //            if (m_VoucherItemDetailList.Count > 0)
            //            {
            //                btnAddItemDetail.Enabled = false;
            //            }
            //            else
            //            {
            //                btnAddItemDetail.Enabled = true;
            //            }
            //        }
            //        else
            //        {
            //            btnAddItemDetail.Enabled = true;
            //        }
            //        btnClearItemDetail.Enabled = true;
            //    }
            //}
            //else
            //{
            //    if (m_VoucherItemDetailList != null)
            //    {
            //        if (m_VoucherItemDetailList.Count > 0)
            //        {
            //            btnAddItemDetail.Enabled = false;
            //        }
            //        else
            //        {
            //            btnAddItemDetail.Enabled = true;
            //        }
            //    }
            //    else
            //    {
            //        btnAddItemDetail.Enabled = true;
            //    }
            //    btnClearItemDetail.Enabled = true;
            //}

            switch (m_modeWorkMode)
            {
                case Mode.Add:
                    {
                        btnAddItemDetail.Enabled = true;
                        btnClearItemDetail.Enabled = true;
                    }
                    break;

                case Mode.View:
                    {
                        btnAddItemDetail.Enabled = false;
                        btnClearItemDetail.Enabled = false;
                    }
                    break;

                case Mode.Reset:
                    {
                        btnAddItemDetail.Enabled = true;
                        btnClearItemDetail.Enabled = true;
                    }
                    break;
            }
        }

        private void SetHeaderValues()
        {
            try
            {
                if (m_CurrentVoucher != null)
                {
                    //txtItemDescription.Text = m_CurrentVoucher.ItemDescription;
                    lblVoucherCodeValue.Text = m_CurrentVoucher.VoucherCode;
                    txtVoucherDescription.Text = m_CurrentVoucher.VoucherDescription;
                    txtVoucherName.Text = m_CurrentVoucher.VoucherName;
                    //txtItemCode.Text = m_CurrentVoucher.ItemCode;
                    m_strRoundingZeroesFormat = Common.GetRoundingZeroes(Common.DisplayAmountRounding);
                    txtMinBuyAmount.Text = m_CurrentVoucher.DisplayMinBuyAmount.ToString(m_strRoundingZeroesFormat, m_nfi);//m_CurrentVoucher.MinBuyAmount.ToString();

                    m_strRoundingZeroesFormat = Common.GetRoundingZeroes(Common.DisplayQtyRounding);
                    txtItemQty.Text = m_CurrentVoucherItemDetail.Quantity.ToString(m_strRoundingZeroesFormat, m_nfi);
                }
                else
                {
                    //txtItemCode.Text = string.Empty;
                    //txtItemDescription.Text = string.Empty;
                    txtMinBuyAmount.Text = string.Empty;
                    lblVoucherCodeValue.Text = string.Empty;
                    txtVoucherDescription.Text = string.Empty;
                    txtVoucherName.Text = string.Empty;
                }
                SetHeaderFieldsProperty();

                if (m_CurrentVoucher != null)
                {
                    if (m_CurrentVoucher.VoucherCode != null)
                    {
                        EnableDisableHeaderFields(false);
                    }
                    else
                    {
                        EnableDisableHeaderFields(true);
                    }
                }
                else
                {
                    EnableDisableHeaderFields(true);
                }

                SetItemDetailValues();
                if (m_CurrentVoucherItemDetail != null)
                {
                    if (m_CurrentVoucherItemDetail.VoucherCode != null)
                    {
                        EnableDisableItemDetails(true);
                    }
                    else
                    {
                        EnableDisableItemDetails(false);
                    }
                }
                else
                {
                    EnableDisableItemDetails(false);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void EnableDisableHeaderFields(bool isEnabled)
        {
            txtVoucherDescription.ReadOnly = !isEnabled;
            txtVoucherName.ReadOnly = !isEnabled;
            txtMinBuyAmount.ReadOnly = !isEnabled;
        }

        private void SetDetailFieldsProperty()
        {
            try
            {
                txtEndRange.ReadOnly = false;
                txtStartRange.ReadOnly = false;
                txtStartText.ReadOnly = false;
                dtpApplicableFromDate.Enabled = true;
                dtpApplicableToDate.Enabled = true;
                chkActive.Enabled = true;
                lblStartText.Text = "Series Start Text:*";
                lblStartRange.Text = "Start Series Range:*";
                lblEndRange.Visible = true;
                txtEndRange.Visible = true;

                txtStartText.MaxLength = 44;
                txtStartRange.MaxLength = 6;

                if (m_CurrentVoucherDetail != null && m_CurrentVoucherDetail.SeriesID > 0)
                {
                    if (m_CurrentVoucherDetail.IsActive)
                    {
                        dtpApplicableFromDate.Enabled = false;
                        dtpApplicableToDate.Enabled = false;
                        chkActive.Enabled = false;
                    }
                    txtEndRange.ReadOnly = true;
                    txtStartRange.ReadOnly = true;
                    txtStartText.ReadOnly = true;

                    lblStartText.Text = "Start Series:*";
                    lblStartRange.Text = "End Series:*";
                    lblEndRange.Visible = false;
                    txtEndRange.Visible = false;

                    txtStartText.MaxLength = 50;
                    txtStartRange.MaxLength = 50;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void SetHeaderFieldsProperty()
        {
            try
            {
                bool isEditable = true;
                if (m_VoucherDetailList != null)
                {
                    var query = (from m in m_VoucherDetailList where m.IsActive == true select m.GiftVoucherCode);
                    if (query.ToList<string>().Count > 0)
                    {
                        isEditable = false;
                    }
                }

                if (m_CurrentVoucher.VoucherCode != null)
                {
                    if (!m_CurrentVoucher.VoucherCode.Trim().Equals(string.Empty) && (!isEditable))
                    {
                        txtItemCode.ReadOnly = true;
                        txtMinBuyAmount.ReadOnly = true;
                        txtVoucherDescription.ReadOnly = true;
                        txtVoucherName.ReadOnly = true;
                    }
                    else
                    {
                        txtItemCode.ReadOnly = false;
                        txtMinBuyAmount.ReadOnly = false;
                        txtVoucherDescription.ReadOnly = false;
                        txtVoucherName.ReadOnly = false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ResetGrid()
        {
            try
            {
                dgvGiftVoucherDetails.DataSource = null;
                dgvGiftVoucherDetails.DataSource = new List<GiftVoucher>();
                if (m_VoucherDetailList != null)
                {
                    dgvGiftVoucherDetails.DataSource = m_VoucherDetailList;
                    m_bindingMgr = (CurrencyManager)this.BindingContext[m_VoucherDetailList];
                    m_bindingMgr.Refresh();
                }

                //dgvGiftVoucherItemDetails.DataSource = null;
                //dgvGiftVoucherItemDetails.DataSource = new List<GiftVoucherItemDetail>();
                //if (m_VoucherItemDetailList != null)
                //{
                //    dgvGiftVoucherItemDetails.DataSource = m_VoucherItemDetailList;
                //    m_bindingMgr = (CurrencyManager)this.BindingContext[m_VoucherItemDetailList];
                //    m_bindingMgr.Refresh();
                //}
                ResetItemDetailGrid();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ResetItemDetailGrid()
        {
            try
            {
                dgvGiftVoucherItemDetails.DataSource = null;
                dgvGiftVoucherItemDetails.DataSource = new List<GiftVoucherItemDetail>();
                if (m_VoucherItemDetailList != null)
                {
                    dgvGiftVoucherItemDetails.DataSource = m_VoucherItemDetailList;
                    m_bindingMgr = (CurrencyManager)this.BindingContext[m_VoucherItemDetailList];
                    m_bindingMgr.Refresh();
                    EnableDisableItemDetails(true);
                }
                //else
                //{
                //    EnableDisableItemDetails(true);
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Reset()
        {
            try
            {
                errorCreate.Clear();
                m_VoucherNo = string.Empty;
                ResetForm();
                txtVoucherName.Focus();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Add()
        {
            try
            {
                if (dtpApplicableFromDate.Checked)
                    m_CurrentVoucherDetail.ApplicableFrom = dtpApplicableFromDate.Value.ToShortDateString();
                else
                    m_CurrentVoucherDetail.ApplicableFrom = string.Empty;

                if (dtpApplicableToDate.Checked)
                    m_CurrentVoucherDetail.ApplicableTo = dtpApplicableToDate.Value.ToShortDateString();
                else
                    m_CurrentVoucherDetail.ApplicableTo = string.Empty;

                m_CurrentVoucherDetail.EndRange = Convert.ToInt32(txtEndRange.Text);
                m_CurrentVoucherDetail.GiftVoucherCode = m_CurrentVoucher.VoucherCode;
                m_CurrentVoucherDetail.IsActive = chkActive.Checked;
                m_CurrentVoucherDetail.StartRange = Convert.ToInt32(txtStartRange.Text);
                m_CurrentVoucherDetail.StartText = txtStartText.Text;
                m_CurrentVoucherDetail.StartSeries = txtStartText.Text + txtStartRange.Text.PadLeft(txtEndRange.Text.Length - txtStartRange.Text.Length + 1, '0');
                m_CurrentVoucherDetail.EndSeries = txtStartText.Text + txtEndRange.Text;
                m_CurrentVoucher.VoucherDetailList = null;
                m_CurrentVoucher.VoucherDetailList = new List<GiftVoucherDetail>();
                m_CurrentVoucher.VoucherDetailList.Add(m_CurrentVoucherDetail);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void UpdateObject()
        {
            try
            {
                if (dtpApplicableFromDate.Checked)
                    m_CurrentVoucherDetail.ApplicableFrom = dtpApplicableFromDate.Value.ToShortDateString();
                else
                    m_CurrentVoucherDetail.ApplicableFrom = string.Empty;

                if (dtpApplicableToDate.Checked)
                    m_CurrentVoucherDetail.ApplicableTo = dtpApplicableToDate.Value.ToShortDateString();
                else
                    m_CurrentVoucherDetail.ApplicableTo = string.Empty;

                m_CurrentVoucherDetail.IsActive = chkActive.Checked;
                m_CurrentVoucher.VoucherDetailList = null;
                m_CurrentVoucher.VoucherDetailList = new List<GiftVoucherDetail>();
                m_CurrentVoucher.VoucherDetailList.Add(m_CurrentVoucherDetail);

                m_CurrentVoucher.ModifiedBy = m_userID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Save()
        {
            try
            {
                if (m_CurrentVoucher == null)
                {
                    m_CurrentVoucher = new GiftVoucher();
                }
                if (m_CurrentVoucher.VoucherCode != null)
                {
                    if (m_CurrentVoucher.VoucherCode.Trim().Equals(string.Empty))
                    {
                        m_CurrentVoucher.CreatedBy = m_userID;
                    }
                }
                else
                {
                    m_CurrentVoucher.CreatedBy = m_userID;
                }
                //m_CurrentVoucher.ItemCode = txtItemCode.Text;
                //m_CurrentVoucher.ItemDescription = txtItemDescription.Text;
                m_CurrentVoucher.MinBuyAmount = string.IsNullOrEmpty(txtMinBuyAmount.Text.Trim()) ? Convert.ToDecimal(0.0000) : Convert.ToDecimal(txtMinBuyAmount.Text);
                m_CurrentVoucher.ModifiedBy = m_userID;
                m_CurrentVoucher.VoucherName = txtVoucherName.Text;
                m_CurrentVoucher.VoucherDescription = txtVoucherDescription.Text;

                //save

                if (m_CurrentVoucherDetail.SeriesID > 0)
                {
                    UpdateObject();
                }
                else
                {
                    Add();
                }

                if (m_CurrentVoucher.VoucherItemDetailList == null)
                {
                    m_CurrentVoucher.VoucherItemDetailList = new List<GiftVoucherItemDetail>();
                }
                m_CurrentVoucher.VoucherItemDetailList = m_VoucherItemDetailList;

                string ErrorMessage = string.Empty;
                bool isSaved = m_CurrentVoucher.Save(ref ErrorMessage);
                if (isSaved)
                {
                    MessageBox.Show(Common.GetMessage("8003", "Gift Voucher Code", m_CurrentVoucher.VoucherCode), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    m_VoucherNo = m_CurrentVoucher.VoucherCode;
                    m_modeWorkMode = Mode.View;
                    ResetForm();
                }
                else if (!ErrorMessage.Trim().Equals(string.Empty))
                {
                    if (ErrorMessage.Trim().IndexOf("30001:") >= 0)
                    {
                        MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Common.LogException(new Exception(ErrorMessage));
                    }
                    else
                    {
                        MessageBox.Show(Common.GetMessage(ErrorMessage.Trim()), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Search()
        {
            //search here

            try
            {
                m_listSearch = new List<GiftVoucher>();
                GiftVoucher m_search = new GiftVoucher();
                m_search.VoucherCode = txtSearchVoucherCode.Text.Trim();
                m_search.VoucherName = txtSearchVoucherName.Text.Trim();
                if (dtpSearchfrmDate.Checked)
                    m_search.FromDate = dtpSearchfrmDate.Value;
                else
                    m_search.FromDate = Common.DATETIME_NULL;
                if (dtpSearchToDate.Checked)
                    m_search.ToDate = dtpSearchToDate.Value;
                else
                    m_search.ToDate = Common.DATETIME_NULL;
                //m_search.ItemCode = txtSearcItemCode.Text.Trim();
                m_listSearch = m_search.Search();

                dgvSearchGiftVoucher.DataSource = null;
                dgvSearchGiftVoucher.DataSource = new List<GiftVoucher>();
                if (m_listSearch != null)
                {
                    if (m_listSearch.Count > 0)
                        dgvSearchGiftVoucher.DataSource = m_listSearch;
                    else
                        MessageBox.Show(Common.GetMessage("8002", "Search"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(Common.GetMessage("30009", "Search"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("30001"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        private void ResetSearchControls()
        {
            errorSearch.Clear();
            txtSearchVoucherCode.Text = string.Empty;
            txtSearchVoucherName.Text = string.Empty;
            dtpSearchfrmDate.Value = Common.DATETIME_CURRENT;
            dtpSearchToDate.Value = Common.DATETIME_CURRENT;
            dtpSearchfrmDate.Checked = false;
            dtpSearchToDate.Checked = false;
            txtSearcItemCode.Text = string.Empty;
            dgvSearchGiftVoucher.DataSource = null;
            dgvSearchGiftVoucher.DataSource = new List<GiftVoucher>();
            Reset();
            txtSearchVoucherCode.Focus();
        }

        #endregion

        #region Events

        #region TextBoxes

        private void txt_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!((e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9) || ((e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9) && (!e.Shift)) || e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete || e.KeyCode == Keys.Alt || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right || e.KeyCode == Keys.Shift || e.KeyCode == Keys.Home || e.KeyCode == Keys.End))
                {
                    e.SuppressKeyPress = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        private void txtItemCode_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                bool isvalid = IsItemValid(txtItemCode.Text.Trim());
                if (isvalid)
                {
                    if (dtItems != null && dtItems.Rows.Count > 0)
                    {
                        DataRow[] dr = dtItems.Select("ItemCode='" + txtItemCode.Text.Trim() + "'");
                        if (dr != null && dr.Length > 0)
                        {
                            txtItemDescription.Text = Convert.ToString(dr[0]["ItemName"]);
                            //m_CurrentVoucher.ItemDescription = Convert.ToString(dr[0]["ItemName"]);
                            //m_CurrentVoucher.ItemID = Convert.ToInt32(dr[0]["ItemID"]);
                            m_CurrentVoucherItemDetail.ItemDescription = Convert.ToString(dr[0]["ItemName"]);
                            m_CurrentVoucherItemDetail.ItemID = Convert.ToInt32(dr[0]["ItemID"]);
                        }
                    }
                }
                else
                {
                    txtItemDescription.Text = string.Empty;
                    if (txtItemCode.Text != string.Empty) 
                        MessageBox.Show(Common.GetMessage("VAL0125"),Common.GetMessage("10001"),MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        #endregion

        #region Buttons

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                m_CurrentVoucherDetail = new GiftVoucherDetail();
                m_CurrentVoucher.VoucherDetailList = null;
                dgvGiftVoucherDetails.ClearSelection();
                SetDetailValues();
                btnSave.Enabled = IsSaveAvailable;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                bool isValid = ValidateSave();
                if (!isValid)
                {
                    StringBuilder sbError = GenerateSaveError();
                    if (sbError.ToString().Trim().Length > 0)
                    {
                        MessageBox.Show(sbError.ToString().Trim(), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    string errItemDetail = string.Empty;
                    if (!ValidateItemDetailsCount(ref errItemDetail))
                    {
                        MessageBox.Show(errItemDetail, Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        DialogResult saveResult = MessageBox.Show(Common.GetMessage("5010", "Save"), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (saveResult == DialogResult.Yes)
                        {
                            Save();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        private bool ValidateItemDetailsCount(ref string errMsg)
        {
            bool IsValid = true;

            if (m_VoucherItemDetailList == null)
            {
                errMsg = Common.GetMessage("VAL0024", "Voucher Item-Detail");
                IsValid = false;
            }
            else if (m_VoucherItemDetailList.Count == 0)
            {
                errMsg = Common.GetMessage("VAL0024", "Voucher Item-Detail");
                IsValid = false;
            }

            return IsValid;
        }

        private void btnCreateReset_Click(object sender, EventArgs e)
        {
            try
            {
                m_modeWorkMode = Mode.Reset;
                Reset();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateSearch();
                StringBuilder sbError = new StringBuilder();
                sbError = GenerateSearchError();
                if (!sbError.ToString().Trim().Equals(string.Empty))
                {
                    MessageBox.Show(sbError.ToString(), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                Search();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        private void btnSearchReset_Click(object sender, EventArgs e)
        {
            try
            {
                ResetSearchControls();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }


        #endregion

        #region GRID VIEW

        private void dgvGiftVoucherDetails_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                m_CurrentVoucherDetail = new GiftVoucherDetail();
                if (dgvGiftVoucherDetails.SelectedRows.Count > 0)
                {
                    m_CurrentVoucherDetail = m_VoucherDetailList[dgvGiftVoucherDetails.SelectedRows[0].Index];
                    SetDetailValues();
                    SetFormProperty();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        private void dgvSearchGiftVoucher_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (dgvSearchGiftVoucher.SelectedRows.Count > 0)
                {
                    m_VoucherNo = Convert.ToString(dgvSearchGiftVoucher.SelectedRows[0].Cells[CON_GRID_VOUCHERNO].Value);
                    m_modeWorkMode = Mode.View;
                    ResetForm();
                    tabControlTransaction.SelectTab("tabCreate");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        private void dgvSearchGiftVoucher_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
                {
                    if (dgvSearchGiftVoucher.Columns[e.ColumnIndex].CellType == typeof(DataGridViewImageCell))
                    {
                        if (Convert.ToString(dgvSearchGiftVoucher.Rows[e.RowIndex].Cells[CON_GRID_VOUCHERNO].Value) != "")
                        {
                            m_VoucherNo = Convert.ToString(dgvSearchGiftVoucher.Rows[e.RowIndex].Cells[CON_GRID_VOUCHERNO].Value);
                            m_modeWorkMode = Mode.View;
                            ResetForm();
                            tabControlTransaction.SelectTab("tabCreate");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        #endregion

        #endregion

        #region Validations

        private bool ValidateDates(DateTime FromDate, DateTime ToDate)
        {
            int days = DateTime.Compare(FromDate, ToDate);
            if (days == 1)
            {
                return false;
            }
            return true;
        }

        private bool IsItemValid(string Item)
        {
            try
            {
                bool IsValid = false;
                if (_itemcollection != null)
                {
                    foreach (string auto in _itemcollection)
                    {
                        if (auto.ToUpper().Equals(Item.ToUpper()))
                        {
                            IsValid = true;
                            break;
                        }
                    }
                }
                return IsValid;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private StringBuilder GenerateSearchError()
        {
            bool focus = false;
            StringBuilder sbError = new StringBuilder();

            if (errorSearch.GetError(dtpSearchfrmDate).Trim().Length > 0)
            {
                sbError.Append(errorSearch.GetError(dtpSearchfrmDate));
                sbError.AppendLine();
                if (!focus)
                {
                    dtpSearchfrmDate.Focus();
                    focus = true;
                }
            }

            if (errorSearch.GetError(dtpSearchToDate).Trim().Length > 0)
            {
                sbError.Append(errorSearch.GetError(dtpSearchToDate));
                sbError.AppendLine();
                if (!focus)
                {
                    dtpSearchToDate.Focus();
                    focus = true;
                }
            }
            sbError = Common.ReturnErrorMessage(sbError);
            return sbError;
        }

        private StringBuilder GenerateSaveError()
        {
            bool focus = false;
            StringBuilder sbError = new StringBuilder();

            if (errorCreate.GetError(txtVoucherName).Trim().Length > 0)
            {
                sbError.Append(errorCreate.GetError(txtVoucherName));
                sbError.AppendLine();
                if (!focus)
                {
                    txtVoucherName.Focus();
                    focus = true;
                }
            }
            if (errorCreate.GetError(txtVoucherDescription).Trim().Length > 0)
            {
                sbError.Append(errorCreate.GetError(txtVoucherDescription));
                sbError.AppendLine();
                if (!focus)
                {
                    txtVoucherDescription.Focus();
                    focus = true;
                }
            }
            if (errorCreate.GetError(txtItemCode).Trim().Length > 0)
            {
                sbError.Append(errorCreate.GetError(txtItemCode));
                sbError.AppendLine();
                if (!focus)
                {
                    txtItemCode.Focus();
                    focus = true;
                }
            }
            if (errorCreate.GetError(txtItemDescription).Trim().Length > 0)
            {
                sbError.Append(errorCreate.GetError(txtItemDescription));
                sbError.AppendLine();
                if (!focus)
                {
                    txtItemDescription.Focus();
                    focus = true;
                }
            }
            if (errorCreate.GetError(txtItemQty).Trim().Trim().Length > 0)
            {
                sbError.Append(errorCreate.GetError(txtItemQty));
                sbError.AppendLine();
                if (!focus)
                {
                    txtItemQty.Focus();
                    focus = true;
                }
            }

            if (errorCreate.GetError(dgvGiftVoucherDetails).Trim().Length > 0)
            {
                sbError.Append(errorCreate.GetError(dgvGiftVoucherDetails));
                sbError.AppendLine();
                if (!focus)
                {
                    btnAddDetails.Focus();
                    focus = true;
                }
            }
            if (errorCreate.GetError(txtStartText).Trim().Length > 0)
            {
                sbError.Append(errorCreate.GetError(txtStartText));
                sbError.AppendLine();
                if (!focus)
                {
                    txtStartText.Focus();
                    focus = true;
                }
            }
            if (errorCreate.GetError(txtStartRange).Trim().Length > 0)
            {
                sbError.Append(errorCreate.GetError(txtStartRange));
                sbError.AppendLine();
                if (!focus)
                {
                    txtStartRange.Focus();
                    focus = true;
                }
            }
            if (errorCreate.GetError(txtEndRange).Trim().Length > 0)
            {
                sbError.Append(errorCreate.GetError(txtEndRange));
                sbError.AppendLine();
                if (!focus)
                {
                    txtEndRange.Focus();
                    focus = true;
                }
            }
            if (errorCreate.GetError(dtpApplicableFromDate).Trim().Length > 0)
            {
                sbError.Append(errorCreate.GetError(dtpApplicableFromDate));
                sbError.AppendLine();
                if (!focus)
                {
                    dtpApplicableFromDate.Focus();
                    focus = true;
                }
            }
            if (errorCreate.GetError(dtpApplicableToDate).Trim().Length > 0)
            {
                sbError.Append(errorCreate.GetError(dtpApplicableToDate));
                sbError.AppendLine();
                if (!focus)
                {
                    dtpApplicableToDate.Focus();
                    focus = true;
                }
            }

            string errMsg = string.Empty;
            if (!ValidateItemDetailsCount(ref errMsg))
            {
                sbError.AppendLine(errMsg);
            }

            sbError = Common.ReturnErrorMessage(sbError);
            return sbError;
        }

        /// <summary>
        /// 1.  Check Vocucher Name Blank
        /// 2.  Check Voucher Description Blank
        /// 3.  Check ItemCode Blank
        /// 4.  item Valid
        /// 4.  Check Item Description Blank
        /// 5.  Check For atleast one detail Row
        /// </summary>
        /// <returns></returns>
        private bool ValidateSave()
        {
            bool IsValid = true;
            errorCreate.Clear();

            if (Validators.CheckForEmptyString(txtVoucherName.Text.Trim().Length))
            {
                errorCreate.SetError(txtVoucherName, Common.GetMessage("VAL0001", lblVoucherName.Text.Trim().Substring(0, lblVoucherName.Text.Trim().Length - 2)));
                IsValid = false;
            }
            if (Validators.CheckForEmptyString(txtVoucherDescription.Text.Trim().Length))
            {
                errorCreate.SetError(txtVoucherDescription, Common.GetMessage("VAL0001", lblVoucherDescription.Text.Trim().Substring(0, lblVoucherDescription.Text.Trim().Length - 2)));
                IsValid = false;
            }

            //if (Validators.CheckForEmptyString(txtItemCode.Text.Trim().Length))
            //{
            //    errorCreate.SetError(txtItemCode, Common.GetMessage("VAL0001", lblItemCode.Text.Trim().Substring(0, lblItemCode.Text.Trim().Length - 2)));
            //    IsValid = false;
            //}
            //if (!IsItemValid(txtItemCode.Text.Trim()))
            //{
            //    errorCreate.SetError(txtItemCode, Common.GetMessage("VAL0009", lblItemCode.Text.Trim().Substring(0, lblItemCode.Text.Trim().Length - 2)));
            //    IsValid = false;
            //}
            //if (Validators.CheckForEmptyString(txtItemDescription.Text.Trim().Length))
            //{
            //    errorCreate.SetError(txtItemDescription, Common.GetMessage("VAL0001", lblItemDescription.Text.Trim().Substring(0, lblItemDescription.Text.Trim().Length - 2)));
            //    IsValid = false;
            //}
            //if (Validators.CheckForEmptyString(txtItemQty.Text.Trim().Length))
            //{
            //    errorCreate.SetError(txtItemQty, Common.GetMessage("VAL0001", lblItemQty.Text.Trim().Replace(":", "").Replace("*", "")));
            //    IsValid = false;
            //}
            //else if (!Validators.IsValidQuantity(txtItemQty.Text.Trim()))
            //{
            //    errorCreate.SetError(txtItemQty, Common.GetMessage("VAL0009", lblItemQty.Text.Trim().Replace(":", "").Replace("*", "")));
            //    IsValid = false;
            //}

            string errItemDetail = string.Empty;
            if (!ValidateItemDetailsCount(ref errItemDetail))
            {
                if (Validators.CheckForEmptyString(txtItemCode.Text.Trim().Length))
                {
                    errorCreate.SetError(txtItemCode, Common.GetMessage("VAL0001", lblItemCode.Text.Trim().Substring(0, lblItemCode.Text.Trim().Length - 2)));
                    IsValid = false;
                }
                if (!IsItemValid(txtItemCode.Text.Trim()))
                {
                    errorCreate.SetError(txtItemCode, Common.GetMessage("VAL0009", lblItemCode.Text.Trim().Substring(0, lblItemCode.Text.Trim().Length - 2)));
                    IsValid = false;
                }
                if (Validators.CheckForEmptyString(txtItemDescription.Text.Trim().Length))
                {
                    errorCreate.SetError(txtItemDescription, Common.GetMessage("VAL0001", lblItemDescription.Text.Trim().Substring(0, lblItemDescription.Text.Trim().Length - 2)));
                    IsValid = false;
                }
                if (Validators.CheckForEmptyString(txtItemQty.Text.Trim().Length))
                {
                    errorCreate.SetError(txtItemQty, Common.GetMessage("VAL0001", lblItemQty.Text.Trim().Replace(":", "").Replace("*", "")));
                    IsValid = false;
                }
                else if (!Validators.IsValidQuantity(txtItemQty.Text.Trim()))
                {
                    errorCreate.SetError(txtItemQty, Common.GetMessage("VAL0009", lblItemQty.Text.Trim().Replace(":", "").Replace("*", "")));
                    IsValid = false;
                }
            }

            if (Validators.CheckForEmptyString(txtStartText.Text.Trim().Length))
            {
                errorCreate.SetError(txtStartText, Common.GetMessage("VAL0001", lblStartText.Text.Trim().Substring(0, lblStartText.Text.Trim().Length - 2)));
                IsValid = false;
            }
            if (Validators.CheckForEmptyString(txtStartRange.Text.Trim().Length))
            {
                errorCreate.SetError(txtStartRange, Common.GetMessage("VAL0001", lblStartRange.Text.Trim().Substring(0, lblStartRange.Text.Trim().Length - 2)));
                IsValid = false;
            }
            if (Validators.CheckForEmptyString(txtEndRange.Text.Trim().Length))
            {
                errorCreate.SetError(txtEndRange, Common.GetMessage("VAL0001", lblEndRange.Text.Trim().Substring(0, lblEndRange.Text.Trim().Length - 2)));
                IsValid = false;
            }
            if (dtpApplicableFromDate.Checked && dtpApplicableToDate.Checked)
            {
                if (!ValidateDates(dtpApplicableFromDate.Value, dtpApplicableToDate.Value))
                {
                    errorCreate.SetError(dtpApplicableFromDate, Common.GetMessage("VAL0072", lblApplicableTo.Text.Trim().Substring(0, lblApplicableTo.Text.Trim().Length - 1), lblApplicableFrom.Text.Trim().Substring(0, lblApplicableFrom.Text.Trim().Length - 1)));
                    IsValid = false;
                }
            }
            if (chkActive.Checked && !dtpApplicableFromDate.Checked)
            {
                errorCreate.SetError(dtpApplicableFromDate, Common.GetMessage("VAL0001", lblApplicableFrom.Text.Trim().Substring(0, lblApplicableFrom.Text.Trim().Length - 1)));
                IsValid = false;
            }
            if (chkActive.Checked && !dtpApplicableToDate.Checked)
            {
                errorCreate.SetError(dtpApplicableToDate, Common.GetMessage("VAL0001", lblApplicableTo.Text.Trim().Substring(0, lblApplicableTo.Text.Trim().Length - 1)));
                IsValid = false;
            }
            return IsValid;
        }

        private void ValidateSearch()
        {
            errorSearch.SetError(dtpSearchfrmDate, string.Empty);
            if (dtpSearchfrmDate.Checked == true && dtpSearchToDate.Checked == true)
            {
                int days = DateTime.Compare(dtpSearchfrmDate.Value, dtpSearchToDate.Value);
                if (days == 1)
                {
                    errorSearch.SetError(dtpSearchfrmDate, Common.GetMessage("VAL0072", "To Date", "From Date"));
                }
            }
        }

        private bool ValidateItemDetails()
        {
            bool IsValid = true;
            string errMsg = "";
            StringBuilder sbErrors = new StringBuilder();
            errorCreate.Clear();

            if (Validators.CheckForEmptyString(txtItemCode.Text.Trim().Length))
            {
                errMsg = Common.GetMessage("VAL0001", lblItemCode.Text.Trim().Substring(0, lblItemCode.Text.Trim().Length - 2));
                errorCreate.SetError(txtItemCode, errMsg);
                sbErrors.AppendLine(errMsg);
                sbErrors.AppendLine(Environment.NewLine);
                IsValid = false;
            }
            else if (!IsItemValid(txtItemCode.Text.Trim()))
            {
                errMsg = Common.GetMessage("VAL0009", lblItemCode.Text.Trim().Substring(0, lblItemCode.Text.Trim().Length - 2));
                errorCreate.SetError(txtItemCode, errMsg);
                sbErrors.AppendLine(errMsg);
                sbErrors.AppendLine(Environment.NewLine);
                IsValid = false;
            }

            if (Validators.CheckForEmptyString(txtItemDescription.Text.Trim().Length))
            {
                errMsg = Common.GetMessage("VAL0001", lblItemDescription.Text.Trim().Substring(0, lblItemDescription.Text.Trim().Length - 2));
                errorCreate.SetError(txtItemDescription, errMsg);
                sbErrors.AppendLine(errMsg);
                sbErrors.AppendLine(Environment.NewLine);
                IsValid = false;
            }
            
            if (Validators.CheckForEmptyString(txtItemQty.Text.Trim().Length))
            {
                errMsg = Common.GetMessage("VAL0001", lblItemQty.Text.Trim().Replace(":", "").Replace("*", ""));
                errorCreate.SetError(txtItemQty, errMsg);
                sbErrors.AppendLine(errMsg);
                sbErrors.AppendLine(Environment.NewLine);
                IsValid = false;
            }
            else if (!Validators.IsValidQuantity(txtItemQty.Text.Trim()))
            {
                errMsg = Common.GetMessage("VAL0009", lblItemQty.Text.Trim().Replace(":", "").Replace("*", ""));
                errorCreate.SetError(txtItemQty, errMsg);
                sbErrors.AppendLine(errMsg);
                sbErrors.AppendLine(Environment.NewLine);
                IsValid = false;
            }
            else if (Convert.ToDecimal(txtItemQty.Text.Trim()) <= Convert.ToDecimal(0.0000))
            {
                errMsg = Common.GetMessage("VAL0104", lblItemQty.Text.Trim().Replace(":", "").Replace("*", ""), "0");
                errorCreate.SetError(txtItemQty, errMsg);
                sbErrors.AppendLine(errMsg);
                sbErrors.AppendLine(Environment.NewLine);
                IsValid = false;
            }

            if (IsValid)
            {
                if (m_VoucherItemDetailList != null)
                {
                    if (m_VoucherItemDetailList.Count > 0)
                    {
                        int countItems = (from giftVoucherItem in m_VoucherItemDetailList
                                     where giftVoucherItem.ItemCode == txtItemCode.Text.Trim()
                                     && giftVoucherItem.ItemDescription == txtItemDescription.Text.Trim()
                                     select giftVoucherItem).Count();

                        if (countItems > 0)
                        {
                            int countItemQty = (from giftVoucherItem in m_VoucherItemDetailList
                                                where giftVoucherItem.ItemCode == txtItemCode.Text.Trim()
                                                && giftVoucherItem.ItemDescription == txtItemDescription.Text.Trim()
                                                && giftVoucherItem.Quantity == Convert.ToDecimal(txtItemQty.Text.Trim())
                                                select giftVoucherItem).Count();
                            if (countItemQty > 0)
                            {
                                errMsg = Common.GetMessage("VAL0063", "Current Gift-Voucher's Item Details");
                                sbErrors.AppendLine(errMsg);
                                sbErrors.AppendLine(Environment.NewLine);
                                IsValid = false;
                            }
                        }
                    }
                }
            }

            sbErrors = Common.ReturnErrorMessage(sbErrors);
            if (!IsValid)
            {
                MessageBox.Show(sbErrors.ToString(), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return IsValid;
        }
        #endregion

        private void btnAddItemDetail_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateItemDetails())
                {
                    //if (m_CurrentVoucherItemDetail == null)
                    //{
                    //    m_CurrentVoucherItemDetail = new GiftVoucherItemDetail();
                    //}
                    m_CurrentVoucherItemDetail = new GiftVoucherItemDetail();

                    if (dtItems != null && dtItems.Rows.Count > 0)
                    {
                        DataRow[] dr = dtItems.Select("ItemCode='" + txtItemCode.Text.Trim() + "'");
                        if (dr != null && dr.Length > 0)
                        {
                            //m_CurrentVoucherItemDetail.ItemDescription = Convert.ToString(dr[0]["ItemName"]);
                            m_CurrentVoucherItemDetail.ItemID = Convert.ToInt32(dr[0]["ItemID"]);
                        }
                    }

                    m_CurrentVoucherItemDetail.ItemCode = txtItemCode.Text.Trim();
                    m_CurrentVoucherItemDetail.ItemDescription = txtItemDescription.Text.Trim();
                    m_CurrentVoucherItemDetail.Quantity = Convert.ToDecimal(txtItemQty.Text.Trim());

                    if (m_VoucherItemDetailList == null)
                    {
                        m_VoucherItemDetailList = new List<GiftVoucherItemDetail>();
                        //m_VoucherItemDetailList.Add(m_CurrentVoucherItemDetail);
                    }
                    //else
                    //{
                    //    GiftVoucherItemDetail itemDetail = null;
                    //    if (m_VoucherItemDetailList.Count > 0)
                    //    {
                    //        itemDetail = (from giftVoucherItem in m_VoucherItemDetailList
                    //                      where giftVoucherItem.ItemCode == txtItemCode.Text.Trim()
                    //                      && giftVoucherItem.ItemDescription == txtItemDescription.Text.Trim()
                    //                      //&& giftVoucherItem.Quantity == Convert.ToDecimal(txtItemQty.Text.Trim())
                    //                      select giftVoucherItem).FirstOrDefault();
                    //    }
                    //    if (itemDetail == null)
                    //    {
                    //        m_VoucherItemDetailList.Add(m_CurrentVoucherItemDetail);
                    //    }
                    //    else
                    //    {
                    //        itemDetail.Quantity = Convert.ToDecimal(txtItemQty.Text.Trim());
                    //    }
                    //}
                    GiftVoucherItemDetail itemDetail = null;
                    if (m_VoucherItemDetailList.Count > 0)
                    {
                        itemDetail = (from giftVoucherItem in m_VoucherItemDetailList
                                      where giftVoucherItem.ItemCode == txtItemCode.Text.Trim()
                                      && giftVoucherItem.ItemDescription == txtItemDescription.Text.Trim()
                                      select giftVoucherItem).FirstOrDefault();
                    }

                    if (itemDetail == null)
                    {
                        m_VoucherItemDetailList.Add(m_CurrentVoucherItemDetail);
                    }
                    else
                    {
                        itemDetail.Quantity = Convert.ToDecimal(txtItemQty.Text.Trim());
                    }

                    ResetItemDetailGrid();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        private void btnClearItemDetail_Click(object sender, EventArgs e)
        {
            txtItemCode.Text = string.Empty;
            txtItemDescription.Text = string.Empty;
            txtItemQty.Text = string.Empty;

            dgvGiftVoucherItemDetails.ClearSelection();

            errorCreate.Clear();

            m_modeWorkMode = Mode.Reset;
            EnableDisableItemDetails(false);
        }

        private void dgvGiftVoucherItemDetails_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                m_CurrentVoucherItemDetail = new GiftVoucherItemDetail();
                if (dgvGiftVoucherItemDetails.SelectedRows.Count > 0)
                {
                    m_CurrentVoucherItemDetail = m_VoucherItemDetailList[dgvGiftVoucherItemDetails.SelectedRows[0].Index];
                    SetItemDetailValues();
                    if (m_modeWorkMode != Mode.View)
                    {
                        m_modeWorkMode = Mode.Add;
                    }
                    EnableDisableItemDetails(true);
                }
                //SetDetailValues();
                //SetFormProperty();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        private void txtItemCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyValue == Common.F4KEY && !e.Alt)
                {
                    NameValueCollection _collection = new NameValueCollection();
                    _collection.Add("LocationId", Common.CurrentLocationId.ToString());
                    _collection.Add("IsAvailableForGift", "1");
                    CoreComponent.Controls.frmSearch objfrmSearch = new CoreComponent.Controls.frmSearch(CoreComponent.Controls.SearchTypes.Item, _collection);
                    CoreComponent.MasterData.BusinessObjects.ItemDetails _ItemDetails = (CoreComponent.MasterData.BusinessObjects.ItemDetails)objfrmSearch.ReturnObject;
                    objfrmSearch.ShowDialog();
                    _ItemDetails = (CoreComponent.MasterData.BusinessObjects.ItemDetails)objfrmSearch.ReturnObject;

                    if (_ItemDetails != null)
                    {
                        txtItemCode.Text = _ItemDetails.ItemCode;
                    }
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
