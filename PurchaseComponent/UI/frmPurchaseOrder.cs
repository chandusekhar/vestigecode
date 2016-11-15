using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PurchaseComponent.BusinessObjects;
using CoreComponent.BusinessObjects;
using CoreComponent.Core.BusinessObjects;
using CoreComponent.MasterData.BusinessObjects;
using TaxComponent.BusinessObjects;
using System.Collections.Specialized;
namespace PurchaseComponent.UI
{
    public partial class frmPurchaseOrder : CoreComponent.Core.UI.Transaction
    {

        #region constants
        private const string CON_STATUS_PARAMETERTYPE = "POSTATUS";
        private const string CON_POTYPE_PARAMETERTYPE = "POTYPE";
        private const string CON_LOCATION_DISPLAYNAME = "DisplayName";
        private const string CON_LOCATION_ID = "LocationId";
        private const string CON_VENDOR_DISPLAYNAME = "VendorCode";
        private const string CON_VENDOR_ID = "VendorId";
        private const string SP_POTAXDETAIL_SEARCH = "usp_POTaxDetailSearch";

        //  public const string SP_GETITEM = "usp_GetItemDetail";
        private string CON_MODULENAME = "";

        #region Gridview Colum name

        private const string CON_GRID_PONo = "PONo";
        private const string CON_GRID_AmendmentNo = "AmendmentNo";
        private const string CON_GRID_Remove = "Remove";
        private const string CON_GRID_VIEW = "View";


        #endregion


        private string GRIDVIEW_XML_PATH = Environment.CurrentDirectory + "\\App_Data\\GridViewDefinition.xml";
        #endregion

        #region Variables Declarations

        List<PurchaseOrder> m_lstSearch;
        PurchaseOrder m_Purchase;
        PurchaseOrderDetail m_PurchaseDetail;
        CurrencyManager m_bindingMgr;
        Address vendorAddress;
        Address destinationAddress;

        private Boolean IsAddAvailable = false;
        private Boolean IsSaveAvailable = false;
        private Boolean IsUpdateAvailable = false;
        private Boolean IsCancelAvailable = false;
        private Boolean IsClosedAvailable = false;
        private Boolean IsSearchAvailable = false;
        private Boolean IsConfirmAvailable = false;
        private Boolean IsViewAvailable = false;
        private Boolean IsAmendAvailable = false;
        private Boolean IsRemoveAvailable = false;
        private Boolean IsPrintAvailable = false;

        private int UserID;//=AuthenticationComponent.BusinessObjects.Authenticate.LoggedInUser.UserId;
        private string UserName;// = AuthenticationComponent.BusinessObjects.Authenticate.LoggedInUser.UserName;

        private int LocationID = Common.CurrentLocationId;
        private string LocationCode = Common.LocationCode;
        private Common.LocationConfigId LocationType = (Common.LocationConfigId)Common.CurrentLocationTypeId;

        private Common.POType PoType = Common.POType.ADHOC;
        private Common.POStatus POStatus = Common.POStatus.New;
        private int Destination = -1;
        private int VendorID = -1;
        private string PoNumber = string.Empty;
        private int TaxJurisdictionId;
        private int AmendmentNo = 0;
        private int POFormType = 0;
        private List<PurchaseOrderDetail> m_existingPODetailList = null;
        private AutoCompleteStringCollection _itemcollection;
        private DataSet m_printDataSet;
        private bool ShowUpdateMsg = true;
        private int m_createdBy;
        #endregion
         
        #region Control Property

        #region Button Property
        private bool SaveEnable
        {
            set
            {
                btnSave.Enabled = value;
            }
            get
            {
                if ((POStatus == Common.POStatus.New || POStatus == Common.POStatus.Created) && POFormType == 0)
                    return true;
                else
                    return false;
            }
        }
        private bool CancelEnable
        {
            set
            {
                btnCancel.Enabled = value;
            }
            get
            {
                if ((POStatus == Common.POStatus.Created) || (POStatus == Common.POStatus.Confirmed) && POFormType == 0 && PoType == Common.POType.ADHOC)
                    return true;
                else
                    return false;
            }
        }
        private bool ConfirmEnable
        {
            set
            {
                btnConfirm.Enabled = value;
            }
            get
            {
                if ((POStatus == Common.POStatus.New || POStatus == Common.POStatus.Created) && POFormType == 0)
                    return true;
                else
                    return false;
            }
        }
        private bool CloseEnable
        {
            set
            {
                btnShortClosed.Enabled = value;
            }
            get
            {
                if (POStatus == Common.POStatus.Recieved && POFormType == 0)
                    return true;
                else
                    return false;
            }
        }
        private bool AddEnable
        {
            set
            {
                btnAddItem.Enabled = value;
            }
            get
            {
                if (((POStatus == Common.POStatus.New || POStatus == Common.POStatus.Created) && POFormType == 0) || (POFormType == 1 && POStatus == Common.POStatus.Confirmed))
                {

                    return true;
                }
                else
                    return false;
            }
        }
        private bool ClearEnable
        {
            set
            {
                btnClearItem.Enabled = value;
            }
            get
            {
                if (((POStatus == Common.POStatus.New || POStatus == Common.POStatus.Created) && POFormType == 0) || (POFormType == 1 && POStatus == Common.POStatus.Confirmed))
                {

                    return true;
                }
                else
                    return false;
            }
        }
        private bool RemoveEnable
        {
            get
            {
                if (((POStatus == Common.POStatus.New || POStatus == Common.POStatus.Created) && POFormType == 0) || (POFormType == 1 && POStatus == Common.POStatus.Confirmed))
                    return true;
                else
                    return false;
            }
        }
        private bool AmendEnable
        {
            set
            {
                btnAddItem.Enabled = value;
            }
            get
            {
                if ((POFormType == (int)Common.POFormType.Amendment && POStatus == Common.POStatus.Confirmed && !PoNumber.Equals(string.Empty)))
                    return true;
                else
                    return false;
            }
        }

        #endregion

        #region TextBox & Combo Property

        private bool EnableComboVendor
        {
            set
            {
                cmbVendorCode.Enabled = value;
            }
            get
            {
                if ((POStatus == Common.POStatus.New || POStatus == Common.POStatus.Created) && POFormType == 0)
                {
                    if (m_Purchase != null)
                    {
                        if (m_Purchase.PurchaseOrderDetail != null && m_Purchase.PurchaseOrderDetail.Count > 0)
                        {
                            return false;
                        }
                    }
                    return (true && IsSaveAvailable);
                }
                else
                    return false;

            }
        }

        private bool EnableComboDestination
        {
            set
            {
                cmbDestinationLocation.Enabled = value;
            }
            get
            {
                if ((POStatus == Common.POStatus.New || POStatus == Common.POStatus.Created) && POFormType == 0)
                {
                    if (m_Purchase != null)
                    {
                        if (m_Purchase.PurchaseOrderDetail != null && m_Purchase.PurchaseOrderDetail.Count > 0)
                        {
                            return false;
                        }
                    }
                    return (true && IsSaveAvailable);
                }
                else
                    return false;
            }
        }

        //Include Shipping
        private bool EnableTxtShipppingDetails
        {
            get
            {
                if ((POStatus == Common.POStatus.New || POStatus == Common.POStatus.Created) && POFormType == 0)
                {
                    return (true && IsSaveAvailable);
                }
                return false;
            }
        }

        private bool EnableTxtHeaderDetails
        {
            get
            {
                if (((POStatus == Common.POStatus.New || POStatus == Common.POStatus.Created) && POFormType == 0) || (POStatus == Common.POStatus.Confirmed && POFormType == 1))
                {
                    if(POFormType == 0)
                        return (true && IsSaveAvailable);
                    else
                        return (true && IsAmendAvailable);
                }
                return false;
            }
        }

        private bool EnableDTPDeliveryDate
        {
            get
            {
                if (((POStatus == Common.POStatus.New || POStatus == Common.POStatus.Created) && POFormType == 0) || (POFormType == 1 && POStatus == Common.POStatus.Confirmed))
                {
                    return (true && IsSaveAvailable);
                }
                return false;
            }
        }

        private bool EnableTxtItemDetail
        {
            get
            {
                if (((POStatus == Common.POStatus.New || POStatus == Common.POStatus.Created) && POFormType == 0) || (POFormType == 1 && POStatus == Common.POStatus.Confirmed))
                {                   
                        return (true && IsAddAvailable);                    
                }
                return false;
            }
        }

        private bool EnableGridRemoveColumn
        {
            get
            {
                if (IsRemoveAvailable && (((POStatus == Common.POStatus.New || POStatus == Common.POStatus.Created) && POFormType == 0)))// || (POFormType == 1 && POStatus == Common.POStatus.Confirmed)))
                {
                    return true;
                }
                return false;
            }
        }

        #endregion

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public frmPurchaseOrder()
        {
            InitializeComponent();
        }

        public frmPurchaseOrder(params object[] arr)
        {
            try
            {
                InitializeComponent();
                if (AuthenticationComponent.BusinessObjects.Authenticate.LoggedInUser != null)
                {
                    UserID = AuthenticationComponent.BusinessObjects.Authenticate.LoggedInUser.UserId;
                    UserName = AuthenticationComponent.BusinessObjects.Authenticate.LoggedInUser.UserName;
                }
                //assigns POform type // Amendment or PO
                POFormType = Convert.ToInt32(arr[0]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        private void frmPurchaseOrder_Load(object sender, EventArgs e)
        {
            if (POFormType == (int)Common.POFormType.PO)
            {
                CON_MODULENAME = this.Tag.ToString();//"PUR03";
            }
            else
            {
                CON_MODULENAME = this.Tag.ToString();// "PUR05";
            }
            //Get Rights Initialized
            InitailizeRights();
            //Initailize controls
            InitailizeControls();
            // Form Settings
            FormSettings();
        }

        public void ShowForm(string PONo, int AmendNo)
        {
            PoNumber = PONo;
            AmendmentNo = AmendNo;
            ResetCreateForm();
            tabControlTransaction.SelectTab("tabCreate");
        }

        #endregion

        #region Methods

        #region Initialize Methods
        /// <summary>
        ///  Initailize the rights from assigned Role
        /// </summary>
        private void InitailizeRights()
        {
            try
            {
                if (UserName != null && CON_MODULENAME != null)
                {
                    IsAddAvailable = AuthenticationComponent.BusinessObjects.Authenticate.IsFunctionAccessible(UserName, LocationCode, CON_MODULENAME, Common.FUNCTIONCODE_ADD);
                    IsCancelAvailable = AuthenticationComponent.BusinessObjects.Authenticate.IsFunctionAccessible(UserName, LocationCode, CON_MODULENAME, Common.FUNCTIONCODE_CANCEL);
                    IsClosedAvailable = AuthenticationComponent.BusinessObjects.Authenticate.IsFunctionAccessible(UserName, LocationCode, CON_MODULENAME, Common.FUNCTIONCODE_CLOSE);
                    IsConfirmAvailable = AuthenticationComponent.BusinessObjects.Authenticate.IsFunctionAccessible(UserName, LocationCode, CON_MODULENAME, Common.FUNCTIONCODE_CONFIRM);
                    IsSaveAvailable = AuthenticationComponent.BusinessObjects.Authenticate.IsFunctionAccessible(UserName, LocationCode, CON_MODULENAME, Common.FUNCTIONCODE_SAVE);
                    IsUpdateAvailable = AuthenticationComponent.BusinessObjects.Authenticate.IsFunctionAccessible(UserName, LocationCode, CON_MODULENAME, Common.FUNCTIONCODE_UPDATE);
                    IsSearchAvailable = AuthenticationComponent.BusinessObjects.Authenticate.IsFunctionAccessible(UserName, LocationCode, CON_MODULENAME, Common.FUNCTIONCODE_SEARCH);
                    IsViewAvailable = AuthenticationComponent.BusinessObjects.Authenticate.IsFunctionAccessible(UserName, LocationCode, CON_MODULENAME, Common.FUNCTIONCODE_VIEW);
                    IsAmendAvailable = AuthenticationComponent.BusinessObjects.Authenticate.IsFunctionAccessible(UserName, LocationCode, CON_MODULENAME, Common.FUNCTIONCODE_AMEND);
                    IsRemoveAvailable = AuthenticationComponent.BusinessObjects.Authenticate.IsFunctionAccessible(UserName, LocationCode, CON_MODULENAME, Common.FUNCTIONCODE_REMOVE);
                    IsPrintAvailable = AuthenticationComponent.BusinessObjects.Authenticate.IsFunctionAccessible(UserName, LocationCode, CON_MODULENAME, Common.FUNCTIONCODE_PRINT);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }
        // Set Properties of Form
        private void FormSettings()
        {
            if (POFormType == 0)
            {
                tabCreate.Text = "Create";
                lblPageTitle.Text = "Purchase Order";
                btnSave.Visible = true;
                btnCancel.Visible = true;
                btnConfirm.Visible = true;
                btnShortClosed.Visible = true;
                btnAmend.Visible = false;
            }
            else
            {
                POStatus = Common.POStatus.Confirmed;
                btnSave.Visible = false;
                btnCancel.Visible = false;
                btnConfirm.Visible = false;
                btnShortClosed.Visible = false;

                btnAmend.Visible = true;
                tabCreate.Text = "Amend";
                lblPageTitle.Text = "Purchase Order Amendment";
                cmbSearchStatus.SelectedValue = (int)Common.POStatus.Confirmed;
                cmbSearchStatus.Enabled = false;                
                //tabControlTransaction.TabPages.Remove(tabCreate);
            }
        }

        private void InitailizeControls()
        {
            // Bind Combo boxes of Search Tab
            BindSearchLists();
            //Bind Create Lists
            BindCreateLists();
            //Initailize Grid Views            
            InitailizeGrids();
            //Set Default values
            ResetCreateForm();
        }

        private void BindCreateLists()
        {
            try
            {
                //Bind Location
               // DataTable dtLocations = Common.ParameterLookup(Common.ParameterType.VendorLocation, new ParameterFilter("", VendorID, 0, 0));
                DataTable dtVendors = Common.ParameterLookup(Common.ParameterType.ItemVendor, new ParameterFilter("", -1, 0, 0));
                //if (dtLocations != null)
                //{
                //    cmbDestinationLocation.DataSource = dtLocations;
                //    cmbDestinationLocation.DisplayMember = CON_LOCATION_DISPLAYNAME;
                //    cmbDestinationLocation.ValueMember = CON_LOCATION_ID;
                //}
                cmbDestinationLocation.SelectedIndexChanged += new EventHandler(cmbDestinationLocation_SelectedIndexChanged);
                //
                if (dtVendors != null)
                {
                    cmbVendorCode.DataSource = dtVendors;
                    cmbVendorCode.DisplayMember = CON_VENDOR_DISPLAYNAME;
                    cmbVendorCode.ValueMember = CON_VENDOR_ID;
                }
                cmbVendorCode.SelectedIndexChanged += new EventHandler(cmbVendorCode_SelectedIndexChanged);
                DataTable dtPoType = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter(CON_POTYPE_PARAMETERTYPE, 0, 0, 0));
                if (dtPoType != null)
                {
                    cmbPOType.DataSource = dtPoType;
                    cmbPOType.DisplayMember = Common.KEYVALUE1;
                    cmbPOType.ValueMember = Common.KEYCODE1;
                    cmbPOType.Enabled = false;
                    cmbPOType.SelectedIndex = 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        private void BindSearchLists()
        {
            try
            {

                // Fill status in status Combo box            
                DataTable dtStatus = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter(CON_STATUS_PARAMETERTYPE, 0, 0, 0));
                if (dtStatus != null)
                {
                    cmbSearchStatus.DataSource = dtStatus;
                    cmbSearchStatus.DisplayMember = Common.KEYVALUE1;
                    cmbSearchStatus.ValueMember = Common.KEYCODE1;
                    //  cmbSearchStatus.SelectedValue = (int)Common.POStatus.Confirmed;
                }
                // Fill Locations in Locations Combo Box           

                DataTable dtLocations = Common.ParameterLookup(Common.ParameterType.Locations, new ParameterFilter("", -5, 0, 0));
                if (dtLocations != null)
                {
                    cmbSearchDestinationLocation.DataSource = dtLocations;
                    cmbSearchDestinationLocation.DisplayMember = CON_LOCATION_DISPLAYNAME;
                    cmbSearchDestinationLocation.ValueMember = CON_LOCATION_ID;
                }
                //FIll Vendor
                DataTable dtVendors = Common.ParameterLookup(Common.ParameterType.ItemVendor, new ParameterFilter("", -1, 0, 0));
                if (dtVendors != null)
                {
                    cmbSearchVendorCode.DataSource = dtVendors;
                    cmbSearchVendorCode.DisplayMember = CON_VENDOR_DISPLAYNAME;
                    cmbSearchVendorCode.ValueMember = CON_VENDOR_ID;
                }
                DataTable dtPoType = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter(CON_POTYPE_PARAMETERTYPE, 0, 0, 0));
                if (dtPoType != null)
                {
                    cmbSearchPOType.DataSource = dtPoType;
                    cmbSearchPOType.DisplayMember = Common.KEYVALUE1;
                    cmbSearchPOType.ValueMember = Common.KEYCODE1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        private void InitailizeGrids()
        {
            PurchaseOrder m_search = new PurchaseOrder();
            try
            {
                dgvPoSearch.AutoGenerateColumns = false;
                dgvPoSearch.AllowUserToAddRows = false;
                dgvPoSearch.AllowUserToDeleteRows = false;
                DataGridView dgvSearchPONew = Common.GetDataGridViewColumns(dgvPoSearch, GRIDVIEW_XML_PATH);

                dgvPOItems.AutoGenerateColumns = false;
                dgvPOItems.AllowUserToAddRows = false;
                dgvPOItems.AllowUserToDeleteRows = false;
                DataGridView dgvPOItemsNew = Common.GetDataGridViewColumns(dgvPOItems, GRIDVIEW_XML_PATH);
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        private void DestinationFill()
        {
            DataTable dtLocations = Common.ParameterLookup(Common.ParameterType.VendorLocation, new ParameterFilter("", VendorID, -5, 0));            
            if (dtLocations != null)
            {
                cmbDestinationLocation.DataSource = dtLocations;
                cmbDestinationLocation.DisplayMember = CON_LOCATION_DISPLAYNAME;
                cmbDestinationLocation.ValueMember = CON_LOCATION_ID;
            }
           // cmbDestinationLocation.SelectedIndexChanged += new EventHandler(cmbDestinationLocation_SelectedIndexChanged);
        }
        #endregion

        #region Set Get Methods
        private void ResetCreateForm()
        {
            //Reset Errors
            errorCreatePO.Clear();
            //Reset Objects
            m_Purchase = null;
            m_PurchaseDetail = null;
            bool updated = false;
            // Get Current PO Object 
            m_Purchase = GetObjectPurchase(PoNumber, AmendmentNo, ref updated,"","");
           
            if (m_Purchase != null)
            {
                
                vendorAddress = m_Purchase.VendorAddress;
                m_existingPODetailList = CopyPurchaseOrderDetail(m_Purchase.PurchaseOrderDetail);
                PoType = (Common.POType)m_Purchase.POType;
                POStatus = (Common.POStatus)m_Purchase.Status;
                Destination = m_Purchase.DestinationLocationID;
                VendorID = m_Purchase.VendorID;
                
            }
            cmbVendorCode.SelectedIndexChanged -= cmbVendorCode_SelectedIndexChanged;
            DestinationFill();
            // Set Header Text Boxes Values
            SetHeaderDefaultValues();
            cmbVendorCode.SelectedIndexChanged+=new EventHandler(cmbVendorCode_SelectedIndexChanged);
            
            // REset Grid View to bind with Current Pruchase order detail
            ResetGrid();
            // Set Item Details Panel Text Boxes //should be blank
            SetDetailValues(null);
            // Set button, gridview, textboxes enable diable proeprties.
            SetProperty();

            RefreshItemList();
            // Give message if updated values of price are coming from database.
            if (updated && ShowUpdateMsg)
            {
                MessageBox.Show(Common.GetMessage("INF0058"),Common.GetMessage("10001"),MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            cmbVendorCode.Focus();
        }
        #region Set
        /// <summary>
        /// Set Header TextBoxes Values
        /// </summary>
        private void SetHeaderDefaultValues()
        {
            txtDestinationAddress.Text = m_Purchase.DeliveryAddress.FullAddress.Trim();            
            txtPaymentDetails.Text = m_Purchase.PaymentDetails;
            txtPaymentTerms.Text = m_Purchase.PaymentTerms;
            txtPOAmendmentNumber.Text = m_Purchase.AmendmentNo.ToString();
            txtRemarks.Text = m_Purchase.Remarks;
            txtPONumber.Text = m_Purchase.PONumber;
            txtShippingDetails.Text = m_Purchase.ShippingDetails;
            txtVendorAddress.Text = m_Purchase.VendorAddress.GetAddress().Trim();
            txtVendorName.Text = m_Purchase.VendorName;
            cmbPOType.SelectedValue = m_Purchase.POType;
            cmbVendorCode.SelectedValue = m_Purchase.VendorID;
            cmbDestinationLocation.SelectedValue = m_Purchase.DestinationLocationID;
            dtpExpDeliveryDate.Value = m_Purchase.ExpectedDeliveryDate;
            dtpMaxDeliveryDate.Value = m_Purchase.MaxDeliveryDate;
            //dtpPODate.Value = m_Purchase.PODate;
            lblPODateValue.Text = m_Purchase.DisplayPODate;
            chkFormCApplicable.Checked = m_Purchase.IsFormCApplicable;
            lblStatusValue.Text = m_Purchase.StatusName;
            lblCreatedDateValue.Text = m_Purchase.DisplayCreateDate;
            txtTotalPOAmount.Text = m_Purchase.DisplayTotalPOAmount.ToString();
            txtTotalTaxAmount.Text = m_Purchase.DisplayTotalTaxAmount.ToString();
            lblDestinationCodeValue.Text = m_Purchase.DestinationLocationCode;
            lblVendorCodeValue.Text = m_Purchase.VendorCode;
            if (m_Purchase.Status == (int)Common.POStatus.New || m_Purchase.Status == (int)Common.POStatus.Created)
            {
                bool exists = false;
                DataTable dt = (DataTable)cmbVendorCode.DataSource;
                foreach (DataRow dr in dt.Rows)
                {
                    if (Convert.ToInt32(dr[cmbVendorCode.ValueMember.ToString()])==m_Purchase.VendorID)
                    {
                        exists = true;
                    }
                }
                if (!exists)
                {
                    MessageBox.Show(Common.GetMessage("VAL0121",m_Purchase.VendorName));
                }
                if (exists)
                {
                    dt = (DataTable)cmbDestinationLocation.DataSource;
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (Convert.ToInt32(dr[cmbDestinationLocation.ValueMember.ToString()]) == m_Purchase.DestinationLocationID)
                        {
                            exists = true;
                        }
                    }
                    if (!exists)
                    {                        
                        MessageBox.Show(Common.GetMessage("VAL0122", m_Purchase.DestinationLocationCode, m_Purchase.VendorName));
                    }
                }
            }
                     
         }

        /// <summary>
        ///  set Detail Textboxes values
        /// </summary>
        /// <param name="detail"></param>
        private void SetDetailValues(PurchaseOrderDetail detail)
        {
            if (detail != null)
            {
                txtItemCode.Text = detail.ItemCode;
                txtItemName.Text = detail.ItemDescription;
                txtItemTotalAmount.Text = detail.LineAmounts.ToString();
                txtttlAmount.Text = detail.DisplayLineAmount.ToString();
                txtItemTotalTax.Text = detail.DisplayLineTaxAmount.ToString();
                txtItemUOM.Text = detail.PurchaseUOM;
                txtUnitPrice.Text = detail.DisplayUnitPrice.ToString();
                txtUnitQty.Text = detail.DisplayUnitQty.ToString();
                txtTaxGroupCode.Text = detail.TaxGroupCode;
            }
            else
            {
                txtItemCode.Text = string.Empty;
                txtItemName.Text = string.Empty;
                txtItemTotalAmount.Text = string.Empty;
                txtItemTotalTax.Text = string.Empty;
                txtItemUOM.Text = string.Empty;
                txtUnitPrice.Text = string.Empty;
                txtUnitQty.Text = string.Empty;
                txtTaxGroupCode.Text = string.Empty;
                txtttlAmount.Text = string.Empty;
            }
        }
        /// <summary>
        /// Refresh Gridview
        /// </summary>
        private void ResetGrid()
        {
            dgvPOItems.DataSource = null;
            if (m_Purchase.PurchaseOrderDetail != null)
            {
                dgvPOItems.DataSource = m_Purchase.PurchaseOrderDetail;
                m_bindingMgr = (CurrencyManager)this.BindingContext[m_Purchase.PurchaseOrderDetail];
                m_bindingMgr.Refresh();
            }
            dgvPOItems.ClearSelection();
        }

        private void SetTabText()
        {
            string text = "";
            //check Amend or PO
            if (POFormType == (int)Common.POFormType.Amendment)
                text = "Amend";
            else
            {
                if (POStatus == Common.POStatus.New)
                    text = "Create";
                else if (POStatus == Common.POStatus.Created || (POStatus == Common.POStatus.Confirmed && PoType == Common.POType.ADHOC) || POStatus == Common.POStatus.Recieved)
                    text = "Update";
                else
                    text = "View";
            }
            tabCreate.Text = text;

        }
        /// <summary>
        /// Set Properties of Textboxes, Gridview, Buttons, combobox
        /// </summary>
        private void SetProperty()
        {
            SetTabText();
            // Set Remove button of Gridview Visibility

            dgvPOItems.Columns[CON_GRID_Remove].Visible = EnableGridRemoveColumn;


            txtShippingDetails.Enabled = EnableTxtShipppingDetails;
            txtPaymentDetails.Enabled = EnableTxtHeaderDetails;
            txtRemarks.Enabled = EnableTxtHeaderDetails;

            dtpExpDeliveryDate.Enabled = EnableDTPDeliveryDate;
            dtpMaxDeliveryDate.Enabled = EnableDTPDeliveryDate;
            //dtpExpDeliveryDate.CanSelect = EnableDTPDeliveryDate;
            //dtpMaxDeliveryDate.CanSelect = EnableDTPDeliveryDate;

            if(POFormType == 0)
                txtItemCode.Enabled = true && EnableTxtItemDetail;
            else
                txtItemCode.Enabled = false;

            txtUnitQty.ReadOnly = !(EnableTxtItemDetail);

            cmbDestinationLocation.Enabled = EnableComboDestination;
            cmbVendorCode.Enabled = EnableComboVendor;
            chkFormCApplicable.Enabled = EnableComboVendor;

            btnCreateReset.Enabled = true;

            btnAddItem.Enabled = AddEnable & IsAddAvailable;
            btnClearItem.Enabled = ClearEnable;
            btnCancel.Enabled = CancelEnable & IsCancelAvailable;
            btnConfirm.Enabled = ConfirmEnable & IsConfirmAvailable;
            btnShortClosed.Enabled = CloseEnable & IsClosedAvailable;
            btnAmend.Enabled = AmendEnable & IsAmendAvailable;

            btnSearch.Enabled = IsSearchAvailable;
            btnPrint.Enabled = IsPrintAvailable;            
            if (POStatus == Common.POStatus.New)
            {
                btnSave.Text = "&Save";
                btnSave.Enabled = SaveEnable & IsSaveAvailable;
            }
            else
            {
                btnSave.Text = "&Update";
                btnSave.Enabled = SaveEnable & IsUpdateAvailable;
            }
            bool show = false;
            if (POStatus == Common.POStatus.New || POStatus == Common.POStatus.Created)
               show = true;
            else 
                show=false;

            cmbVendorCode.Visible = show;
            cmbDestinationLocation.Visible = show;
            lblVendorCodeValue.Visible = !show;
            lblDestinationCodeValue.Visible = !show;
            
            
        }

        /// <summary>
        /// Refresh Expected Delievery date and Max Date
        /// </summary>
        private void RefreshDates()
        {
            try
            {
                if (POStatus == Common.POStatus.Created || POStatus == Common.POStatus.New)
                {
                    if (m_Purchase.PurchaseOrderDetail != null && m_Purchase.PurchaseOrderDetail.Count > 0)
                    {
                        var query = (from p in m_Purchase.PurchaseOrderDetail select p.LeadTime).Min();
                        dtpExpDeliveryDate.Value = m_Purchase.CreatedDate.AddDays((int)query);
                        dtpMaxDeliveryDate.Value = m_Purchase.CreatedDate.AddDays((int)query);
                    }
                    else
                    {
                        dtpExpDeliveryDate.Value = Common.DATETIME_CURRENT;
                        dtpMaxDeliveryDate.Value = Common.DATETIME_CURRENT;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        // Set purchase objects total values
        private void SetTotalAmount(PurchaseOrder _PO)
        {
            _PO.SetTotalAmount();
            _PO.SetTotalTax();
        }

        private void RefreshItemList()
        {
            try
            {
                _itemcollection = new AutoCompleteStringCollection();
                if (VendorID > 0 && Destination > 0)
                {
                    ItemVendorLocationDetails m_Item = new ItemVendorLocationDetails();
                    m_Item.VendorId = VendorID;
                    m_Item.LocationId = Destination;
                    m_Item.Status = 1;
                    string errorMessage = string.Empty;
                    List<ItemVendorLocationDetails> _itemdetail = m_Item.GetItemsForVendorLocation();
                    if (_itemdetail != null)
                    {
                        for (int j = 0; j < _itemdetail.Count; j++)
                        {
                            _itemcollection.Add(_itemdetail[j].ItemCode);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            txtItemCode.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtItemCode.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtItemCode.AutoCompleteCustomSource = _itemcollection;
        }
        #endregion

        //Get   
        #region GET

        /// <summary>
        /// GEt Particular Item Details
        /// </summary>
        /// <param name="itemcode"></param>
        /// <param name="locaionID"></param>
        /// <param name="vendorID"></param>
        /// <returns></returns>
        private ItemVendorLocationDetails GetItemDetail(string itemcode, int locaionID, int vendorID, int isCompositeItem)
        {
            try
            {
                CoreComponent.MasterData.BusinessObjects.ItemVendorLocationDetails m_Item = new CoreComponent.MasterData.BusinessObjects.ItemVendorLocationDetails();
                m_Item.ItemCode = itemcode;
                m_Item.VendorId = vendorID;
                m_Item.LocationId = locaionID;
                m_Item.IsCompositeItem = isCompositeItem;
                m_Item.Status = 1;
                string errorMessage = string.Empty;
                List<ItemVendorLocationDetails> _itemdetail = m_Item.GetItemsForVendorLocation();
                List<ItemVendorLocationDetails> t;
                if (_itemdetail != null && _itemdetail.Count>0)
                {
                    var query = from a in _itemdetail where a.ItemCode.ToLower().Trim() == itemcode.ToLower().Trim() select a;
                    if (query.ToList().Count == 1)
                    {
                        t = (List<ItemVendorLocationDetails>)query.ToList();
                        return t[0];
                    }
                    else
                        return null;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get Object of Purchase
        /// </summary>
        /// <returns></returns>
        ///   
        private PurchaseOrder GetObjectPurchase(string PO, int amendno, ref bool updated,string sVendorCode,
                                                 string sLocationCode)
        {
            try
            {
                PurchaseOrder _purchase = new PurchaseOrder();
                _purchase.TaxJurisdictionId = TaxJurisdictionId;
                if (PO.Equals(string.Empty))
                {
                    // new po object
                    //New PO
                    _purchase.POType = (int)PoType;
                    _purchase.Status = (int)Common.POStatus.New;
                    _purchase.StatusName = Common.POStatus.New.ToString();
                    _purchase.PurchaseOrderDetail = new List<PurchaseOrderDetail>();
                }
                else
                {
                    List<PurchaseOrder> _purchaseList;
                    _purchase.PONumber = PO; 
                    _purchase.AmendmentNo = amendno;
                    _purchaseList = _purchase.Search();
                    if (_purchaseList != null && _purchaseList.Count > 0)
                    {
                        _purchase = _purchaseList[0];
                        _purchase.TaxJurisdictionId = TaxJurisdictionId;
                        //GetPO detail                    
                        //PurchaseOrderDetail _purchaseDetail = new PurchaseOrderDetail(_purchase.DestinationLocationID, _purchase.IsFormCApplicable);
                        //_purchaseDetail.PONumber = _purchase.PONumber;
                        //_purchaseDetail.AmendmentNo = _purchase.AmendmentNo;
                        List<PurchaseOrderDetail> _purchaseOrderList = new List<PurchaseOrderDetail>();
                        _purchaseOrderList = _purchase.SearchDetails();
                        List<PurchaseOrderDetail> ValidOrderDetail = new List<PurchaseOrderDetail>();
                        // in search add search for taxdetail
                        if (_purchaseOrderList != null && _purchaseOrderList.Count > 0 && (_purchase.Status == (int)Common.POStatus.Created))                           //||(_purchase.Status == (int)Common.POStatus.Confirmed && POFormType==(int)Common.POFormType.Amendment)))
                        {
                            // IF PO is in CreatedState
                            updated = true;
                            for (int i = 0; i < _purchaseOrderList.Count; i++)
                            {
                                PurchaseOrderDetail d = _purchaseOrderList[i];
                                // GET Latest Details of Item from Master
                                if (Validate_PurchaseDetail(ref d, _purchase.DestinationLocationID, _purchase.VendorID))
                                {
                                    d.GetAndCalculateTaxes(true,sVendorCode,sLocationCode);
                                }
                                ValidOrderDetail.Add(d);
                            }
                            _purchase.PurchaseOrderDetail = ValidOrderDetail;
                            SetTotalAmount(_purchase);
                        }
                        else if (_purchaseOrderList != null)
                            _purchase.PurchaseOrderDetail = _purchaseOrderList;

                    }
                }
                return _purchase;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
                return null;
            }
        }

        #endregion

        #endregion

        #region  Button Click Methods

        /// <summary>
        /// Search Button Method
        /// </summary>
        private void Search()
        {
            try
            {
                m_lstSearch = new List<PurchaseOrder>();
                PurchaseOrder m_search = new PurchaseOrder();
                m_search.PONumber = txtSearchPONumber.Text.Trim();

                m_search.VendorID = Convert.ToInt32(cmbSearchVendorCode.SelectedValue);
                m_search.DestinationLocationID = Convert.ToInt32(cmbSearchDestinationLocation.SelectedValue);
                if (dtpSearchFromPODate.Checked)
                    m_search.FromDate = dtpSearchFromPODate.Value;
                else
                    m_search.FromDate = Common.DATETIME_NULL;
                if (dtpSearchToPODate.Checked)
                    m_search.ToDate = dtpSearchToPODate.Value;
                else
                    m_search.ToDate = Common.DATETIME_NULL;

                m_search.Status = Convert.ToInt32(cmbSearchStatus.SelectedValue);
                m_search.POType = Convert.ToInt32(cmbSearchPOType.SelectedValue);
                m_search.IsFormCApplicable = chkSearchFormCApplicable.Checked;
                m_lstSearch = m_search.Search();

                dgvPoSearch.DataSource = null;
                dgvPoSearch.DataSource = new List<PurchaseOrderDetail>();
                if (m_lstSearch != null)
                {
                    dgvPoSearch.DataSource = m_lstSearch;
                    if (m_lstSearch.Count == 0)
                        MessageBox.Show(Common.GetMessage("8002"),Common.GetMessage("10001"),MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(Common.GetMessage("30009", "Search"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("30001"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        /// <summary>
        /// Reset Search Tab Controls
        /// </summary>
        private void ResetSearchControls()
        {
            txtSearchPONumber.Text = string.Empty;
            cmbSearchDestinationLocation.SelectedIndex = 0;
            if(POFormType == 0)
                cmbSearchStatus.SelectedIndex = 0;
            else
                cmbSearchStatus.SelectedValue = 1;
            cmbSearchVendorCode.SelectedIndex = 0;
            cmbSearchPOType.SelectedIndex = 0;
            dtpSearchFromPODate.Value = Common.DATETIME_CURRENT;
            dtpSearchToPODate.Value = Common.DATETIME_CURRENT;
            dtpSearchFromPODate.Checked = false;
            dtpSearchToPODate.Checked = false;
            chkSearchFormCApplicable.Checked = false;
            dgvPoSearch.DataSource = null;
            dgvPoSearch.DataSource = new List<PurchaseOrderDetail>();
            Reset();
            txtSearchPONumber.Focus();

        }

        /// <summary>
        /// Clear Item Detail Text Boxes 
        /// </summary>
        private void ClearItem()
        {
            errorCreatePO.Clear();
            m_PurchaseDetail = null;
            m_createdBy = 0;
            if (POFormType == 1)
                txtItemCode.Enabled = false;
            else
                txtItemCode.Enabled = true;
            dgvPOItems.ClearSelection();
            SetDetailValues(m_PurchaseDetail);
            txtItemCode.Focus();
        }

        /// <summary>
        /// Reset Create Tab
        /// </summary>
        private void Reset()
        {
            PoNumber = string.Empty;
            PoType = Common.POType.ADHOC;
            POStatus = Common.POStatus.New;
            ResetCreateForm();
            if (POFormType == (int)Common.POFormType.Amendment)
            {
                //tabSearch.Show();
                tabControlTransaction.SelectedTab = tabControlTransaction.TabPages["tabSearch"];
                tabSearch.Select();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rowIndex"></param>
        private void Remove(int rowIndex)
        {
            try
            {
                DialogResult saveResult = MessageBox.Show(Common.GetMessage("5010", "Remove"), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (saveResult == DialogResult.Yes)
                {
                    m_Purchase.PurchaseOrderDetail.RemoveAt(rowIndex);
                    ResetGrid();
                    ClearItem();
                    RefreshDates();
                    SetTotalAmount(m_Purchase);
                    txtTotalPOAmount.Text = m_Purchase.DisplayTotalPOAmount.ToString();
                    txtTotalTaxAmount.Text = m_Purchase.DisplayTotalTaxAmount.ToString();
                    SetProperty();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        private void Save()
        {
            try
            {
                #region Validation
                ValidateSave();
                StringBuilder sbError = new StringBuilder();
                sbError = GenerateSaveError();
                if (!sbError.ToString().Trim().Equals(string.Empty))
                {
                    MessageBox.Show(sbError.ToString(), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                #endregion

                DialogResult saveResult = MessageBox.Show(Common.GetMessage("INF0027"), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (saveResult == DialogResult.Yes)
                {
                    string ErrorMessage = string.Empty;
                    CreateSaveObject();
                    bool IsSave = m_Purchase.Save(ref ErrorMessage, false);
                    if (IsSave)
                    {                    
                        MessageBox.Show(Common.GetMessage("8003", "Purchase Order",m_Purchase.PONumber),Common.GetMessage("10001"),MessageBoxButtons.OK,MessageBoxIcon.Information);
                        PoNumber = m_Purchase.PONumber;
                        ShowUpdateMsg = false;
                        ResetCreateForm();
                        if (m_lstSearch != null)
                            Search();
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
                            MessageBox.Show(Common.GetMessage(ErrorMessage.Trim()),Common.GetMessage("10001"),MessageBoxButtons.OK,MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Confirm()
        {
            try
            {
                #region Validation
                ValidateSave();
                #endregion

                #region Check Errors

                StringBuilder sbError = new StringBuilder();
                sbError = GenerateSaveError();

                #endregion

                # region Display Error
                if (!sbError.ToString().Trim().Equals(string.Empty))
                {
                    MessageBox.Show(sbError.ToString(), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                #endregion

                DialogResult saveResult = MessageBox.Show(Common.GetMessage("5010", "Confirm"), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (saveResult == DialogResult.Yes)
                {
                    string ErrorMessage = string.Empty;
                    CreateSaveObject();
                    m_Purchase.Status = (int)Common.POStatus.Confirmed;
                    bool isConfirm = m_Purchase.Save(ref ErrorMessage, false);
                    if (isConfirm)
                    {
                        if (POStatus == Common.POStatus.New)
                        {
                            MessageBox.Show(Common.GetMessage("8006", "Purchase Order", m_Purchase.PONumber), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show(Common.GetMessage("INF0055", "PO", "Confirmed"),Common.GetMessage("10001"),MessageBoxButtons.OK,MessageBoxIcon.Information);
                        }
                        PoNumber = m_Purchase.PONumber;
                        ResetCreateForm();
                        if (m_lstSearch != null)
                            Search();
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
                            MessageBox.Show(Common.GetMessage(ErrorMessage.Trim()),Common.GetMessage("10001"),MessageBoxButtons.OK,MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ShortClosed()
        {
            try
            {
                 DialogResult saveResult = MessageBox.Show(Common.GetMessage("5010", "Close"), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                 if (saveResult == DialogResult.Yes)
                 {
                     m_Purchase.ModifiedBy = UserID;
                     m_Purchase.Status = (int)Common.POStatus.ShortClosed;
                     string ErrorMessage = string.Empty;
                     bool isClosed = m_Purchase.Save(ref ErrorMessage, false);
                     if (isClosed)
                     {
                         MessageBox.Show(Common.GetMessage("INF0055", "Closed"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                         PoNumber = m_Purchase.PONumber;
                         ResetCreateForm();
                         if (m_lstSearch != null)
                             Search();
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
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SaveAmend()
        {
            try
            {
                #region Validation
                ValidateSave();
                StringBuilder sbError = new StringBuilder();
                sbError = GenerateSaveError();
                if (!sbError.ToString().Trim().Equals(string.Empty))
                {
                    MessageBox.Show(sbError.ToString(), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                #endregion
                DialogResult saveResult = MessageBox.Show(Common.GetMessage("INF0027"), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (saveResult == DialogResult.Yes)
                {
                    string ErrorMessage = string.Empty;
                    CreateAmendObject();
                    bool isAmend = m_Purchase.Save(ref ErrorMessage, true);
                    if (isAmend)
                    {
                        MessageBox.Show(Common.GetMessage("INF0055", "PO", "Amended"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        PoNumber = m_Purchase.PONumber;
                        ResetCreateForm();
                        if (m_lstSearch != null)
                            Search();
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
            
        }

        private void Cancel()
        {
            try
            {
                DialogResult saveResult = MessageBox.Show(Common.GetMessage("5010", "Cancel"), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (saveResult == DialogResult.Yes)
                {

                    m_Purchase.Status = (int)Common.POStatus.Cancelled;
                    m_Purchase.ModifiedBy = UserID;
                    string ErrorMessage = string.Empty;
                    bool isCancel = m_Purchase.Save(ref ErrorMessage, false);
                    if (ErrorMessage.Equals(string.Empty))
                    {
                        MessageBox.Show(Common.GetMessage("INF0055", "PO", "Cancelled"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        PoNumber = m_Purchase.PONumber;
                        ResetCreateForm();
                        if (m_lstSearch != null)
                            Search();
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
                            MessageBox.Show(Common.GetMessage(ErrorMessage.Trim()),Common.GetMessage("10001"),MessageBoxButtons.OK,MessageBoxIcon.Error);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void AddItem()
        {
            try
            {
                #region Validation
                ValidateAdd();
                StringBuilder sbError = new StringBuilder();
                sbError = GenerateAddError();
                if (!sbError.ToString().Trim().Equals(string.Empty))
                {
                    MessageBox.Show(sbError.ToString(), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                #endregion

                if (m_PurchaseDetail != null)
                {
                    //check POU and MOQ
                    if (m_PurchaseDetail.MOQ > Convert.ToDecimal(txtUnitQty.Text.Trim()) || Convert.ToDecimal(txtUnitQty.Text.Trim()) % m_PurchaseDetail.PUF != 0)
                    {
                        MessageBox.Show(Common.GetMessage("INF0054", m_PurchaseDetail.DisplayMOQ.ToString(), m_PurchaseDetail.DisplayPUF.ToString()), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtUnitQty.Focus();
                        return;
                    }

                    //check for unit-qty of each item and compare it with its 1st amendment qty.
                    //if unit-qty is less than 1st amendment qty, show error.
                    foreach (PurchaseOrderDetail m in m_existingPODetailList)
                    {
                        if (m.ItemCode == m_PurchaseDetail.ItemCode)
                        {
                            DataTable dtTemp = Common.ParameterLookup(Common.ParameterType.FirstAmendmentQuantity, new ParameterFilter(m.PONumber, m.ItemID, 0, 0));
                            if (POFormType == (int)Common.POFormType.Amendment && (Convert.ToDecimal(dtTemp.Rows[0]["UnitQty"].ToString()) > Convert.ToDecimal(txtUnitQty.Text.Trim())))
                            {
                                decimal unitQty = Math.Round(Convert.ToDecimal(dtTemp.Rows[0]["UnitQty"].ToString()), Common.DisplayQtyRounding, MidpointRounding.AwayFromZero);
                                MessageBox.Show(Common.GetMessage("INF0059", m.ItemCode + "'s Quanity", "1st Amendment Quantity of " + unitQty), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                txtUnitQty.Focus();
                                return;
                            }
                            
                            //if (POFormType == (int)Common.POFormType.Amendment && m.UnitQty > Convert.ToDecimal(txtUnitQty.Text.Trim()))
                            //{
                            //    MessageBox.Show(Common.GetMessage("INF0059", "Quanity", "Confirmed Quantity"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            //    txtUnitQty.Focus();
                            //    return;
                            //}
                        }
                    }
        
                    bool exist = false;
                    foreach (PurchaseOrderDetail m in m_Purchase.PurchaseOrderDetail)
                    {
                        if (m.ItemCode == m_PurchaseDetail.ItemCode)
                        {
                            m_PurchaseDetail = m;
                            exist = true;
                            break;
                        }
                    }
                    m_PurchaseDetail.PONumber = PoNumber;
                    m_PurchaseDetail.AmendmentNo = AmendmentNo;
                    m_PurchaseDetail.TaxGroupCode = Convert.ToString(txtTaxGroupCode.Text.Trim());
                    m_PurchaseDetail.TaxGroupCodeName = Convert.ToString(txtTaxGroupCode.Text.Trim());
                    m_PurchaseDetail.UnitPrice = Convert.ToDecimal(txtUnitPrice.Text.Trim());
                    m_PurchaseDetail.UnitQty = Convert.ToDecimal(txtUnitQty.Text.Trim());
                    m_PurchaseDetail.ModifiedBy = UserID;
                    m_PurchaseDetail.GetAndCalculateTaxes(true,GetVendorCode(),GetLocationCode()); //pauru
                    if (!exist)
                    {
                        m_PurchaseDetail.CreatedBy = UserID;
                        m_PurchaseDetail.CreatedDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                        m_Purchase.PurchaseOrderDetail.Add(m_PurchaseDetail);
                    }
                    ResetGrid();
                    ClearItem();
                    RefreshDates();
                    SetTotalAmount(m_Purchase);
                    txtTotalPOAmount.Text = m_Purchase.DisplayTotalPOAmount.ToString();
                    txtTotalTaxAmount.Text = m_Purchase.DisplayTotalTaxAmount.ToString();
                    SetProperty();

                    txtItemCode.Focus();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        #endregion

        #endregion

        #region Events

        #region Button Events
        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Cancel();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("30001"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
            
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {      
               Confirm();                
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
               Save();               
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            try
            {
                AddItem();
            //{
            //    #region Validation
            //    ValidateAdd();
            //    #endregion

            //    #region Check Errors

            //    StringBuilder sbError = new StringBuilder();
            //    sbError = GenerateAddError();

            //    #endregion

            //    # region Display Error
            //    if (!sbError.ToString().Trim().Equals(string.Empty))
            //    {
            //        MessageBox.Show(sbError.ToString(), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        return;
            //    }
            //    #endregion

            //    if (m_PurchaseDetail != null)
            //    {
            //        try
            //        {
            //            //check POU and MOQ
            //            if (m_PurchaseDetail.MOQ > Convert.ToDecimal(txtUnitQty.Text.Trim()) || Convert.ToDecimal(txtUnitQty.Text.Trim()) % m_PurchaseDetail.PUF != 0)
            //            {
            //                MessageBox.Show(Common.GetMessage("INF0054", m_PurchaseDetail.DisplayMOQ.ToString(), m_PurchaseDetail.DisplayPUF.ToString()), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //                txtUnitQty.Focus();
            //                return;
            //            }

            //            foreach (PurchaseOrderDetail m in m_existingPODetailList)
            //            {
            //                if (m.ItemCode == m_PurchaseDetail.ItemCode)
            //                {
            //                    if (POFormType == (int)Common.POFormType.Amendment && m.UnitQty > Convert.ToDecimal(txtUnitQty.Text.Trim()))
            //                    {
            //                        MessageBox.Show(Common.GetMessage("INF0059", "Quanity", "Confirmed Quantity"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            //                        txtUnitQty.Focus();
            //                        return;
            //                    }
            //                }
            //            }
            //            bool exist = false;
            //            foreach (PurchaseOrderDetail m in m_Purchase.PurchaseOrderDetail)
            //            {
            //                if (m.ItemCode == m_PurchaseDetail.ItemCode)
            //                {
            //                    m_PurchaseDetail = m;
            //                    exist = true;
            //                    break;
            //                }
            //            }
            //            m_PurchaseDetail.PONumber = PoNumber;
            //            m_PurchaseDetail.AmendmentNo = AmendmentNo;
            //            m_PurchaseDetail.TaxGroupCode = Convert.ToString(txtTaxGroupCode.Text.Trim());
            //            m_PurchaseDetail.TaxGroupCodeName = Convert.ToString(txtTaxGroupCode.Text.Trim());
            //            m_PurchaseDetail.UnitPrice = Convert.ToDecimal(txtUnitPrice.Text.Trim());
            //            m_PurchaseDetail.UnitQty = Convert.ToDecimal(txtUnitQty.Text.Trim());
            //            m_PurchaseDetail.ModifiedBy = UserID;
            //            if (!exist)
            //            {
            //                m_PurchaseDetail.CreatedBy = UserID;
            //                m_PurchaseDetail.CreatedDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            //                m_Purchase.PurchaseOrderDetail.Add(m_PurchaseDetail);
            //            }
            //            ResetGrid();
            //            ClearItem();
            //            RefreshDates();
            //            SetTotalAmount(m_Purchase);
            //            txtTotalPOAmount.Text = m_Purchase.DisplayTotalPOAmount.ToString();
            //            txtTotalTaxAmount.Text = m_Purchase.DisplayTotalTaxAmount.ToString();
            //            SetProperty();
            //        }
            //        catch (Exception ex)
            //        {
            //            MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            //            Common.LogException(ex);
            //        }
            //        txtItemCode.Focus();
            //    }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnClearItem_Click(object sender, EventArgs e)
        {
            try
            {
                ClearItem();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        private void btnCreateReset_Click(object sender, EventArgs e)
        {
            try
            {
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

        private void btnShortClosed_Click(object sender, EventArgs e)
        {
            try
            {
                ShortClosed();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
            
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_Purchase != null && m_Purchase.Status >= (int)Common.POStatus.Created && m_Purchase.Status != (int)Common.POStatus.Cancelled)
                {
                    btnPrint.Enabled = false;
                    PrintReport();
                    btnPrint.Enabled = true;
                }
                else
                    MessageBox.Show(Common.GetMessage("INF0101", "PO", Common.POStatus.Confirmed.ToString()), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                btnPrint.Enabled = true;
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_amend_Click(object sender, EventArgs e)
        {
            try
            {
                SaveAmend();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
            
        }

        #endregion

        #region ComboBox Events
        /// <summary>
        /// Fill Vendor Address and Name On Change of vendorcode
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbVendorCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                TaxJurisdictionId = 0;
                if (cmbVendorCode.SelectedIndex > 0)
                {
                    VendorID = (int)cmbVendorCode.SelectedValue;
                    try
                    {
                        List<VendorMaster> lstvendor = VendorMaster.GetVendor((int)cmbVendorCode.SelectedValue);
                        vendorAddress = lstvendor[0].Address;
                        txtVendorName.Text = lstvendor[0].VendorName;
                        txtVendorAddress.Text = lstvendor[0].AddressText;
                        txtPaymentTerms.Text = lstvendor[0].PaymentTerms;
                        m_Purchase.VendorAddress = vendorAddress;
                        m_Purchase.TaxJurisdictionId = lstvendor[0].TaxJuridicationID;
                        TaxJurisdictionId = m_Purchase.TaxJurisdictionId;
                        RefreshItemList();
                        DestinationFill();
                        ClearItem();
                        cmbVendorCode.Focus();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Common.LogException(ex);
                    }
                }
                else
                {
                    txtVendorAddress.Text = string.Empty;
                    txtVendorName.Text = string.Empty;
                    txtPaymentTerms.Text = string.Empty;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        /// <summary>
        /// Fill Destination location address on change of destination
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbDestinationLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbDestinationLocation.SelectedIndex > 0)
                {
                    Destination = Convert.ToInt32(cmbDestinationLocation.SelectedValue);
                    DataTable dtLocation = Common.ParameterLookup(Common.ParameterType.Locations, new ParameterFilter("", -5, (int)cmbDestinationLocation.SelectedValue, 0));
                    if (dtLocation != null && dtLocation.Rows.Count > 0)
                    {
                        destinationAddress = Address.CreateAddressObject(dtLocation.Rows[0]);
                        txtDestinationAddress.Text = destinationAddress.GetAddress();
                        m_Purchase.DeliveryAddress = destinationAddress;
                    }
                    RefreshItemList();
                    ClearItem();
                    cmbDestinationLocation.Focus();
                }
                else
                {
                    txtDestinationAddress.Text = string.Empty;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        #endregion

        #region TextBox Events
        private void txtItemCode_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                errorCreatePO.Clear();
                //Check blank fields
                if (txtItemCode.Text.Trim().Length > 0)
                {
                    bool isvalid = CheckItemFields();
                    if (isvalid)
                    {

                        bool istemValid = Validate_ItemCode();
                        
                        if (istemValid)
                        {
                            errorCreatePO.Clear();
                            SetDetailValues(m_PurchaseDetail);
                        }
                    }
                    else
                    {
                        #region Check Errors
                        StringBuilder sbError = new StringBuilder();
                        sbError = GenerateAddError();
                        #endregion

                        # region Display Error
                        if (!sbError.ToString().Trim().Equals(string.Empty))
                        {
                            MessageBox.Show(sbError.ToString(), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtItemCode.Text = string.Empty;
                            return;
                        }
                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        private void txtUnitQty_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtUnitQty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                decimal qty = 0;
                if (txtUnitQty.Text.Trim() == "")
                {
                    qty = 0;
                }
                else
                {    
                    if(Validators.IsValidQuantity(txtUnitQty.Text.Trim()))
                        qty = Convert.ToDecimal((sender as TextBox).Text.Trim());
                }
                if (m_PurchaseDetail != null)
                {
                    m_PurchaseDetail.UnitQty = qty;
                    m_PurchaseDetail.GetAndCalculateTaxes(true, GetVendorCode(), GetLocationCode()); //pauru
                    txtItemTotalTax.Text = m_PurchaseDetail.DisplayLineTaxAmount.ToString();
                    
                   // decimal d= m_PurchaseDetail.LineAmount + m_PurchaseDetail.LineTaxAmount;
                   // d = Math.Round(d, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);

                    txtItemTotalAmount.Text = m_PurchaseDetail.LineAmounts.ToString();
                    txtttlAmount.Text = m_PurchaseDetail.DisplayLineAmount.ToString(); 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        private void txtItemCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == Common.F4KEY && !e.Alt)
            {
                try
                {
                    CheckItemFields();
                    errorCreatePO.SetError(txtItemCode, string.Empty);
                    StringBuilder sbError = new StringBuilder();
                    sbError = GenerateCreateError();
                    if (!sbError.ToString().Trim().Equals(string.Empty))
                    {
                        MessageBox.Show(sbError.ToString(), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    NameValueCollection _collection = new NameValueCollection();
                    _collection.Add("LocationId", cmbDestinationLocation.SelectedValue.ToString());
                    _collection.Add("VendorId", cmbVendorCode.SelectedValue.ToString());
                    _collection.Add("IsCompositeItem", "0");

                    CoreComponent.Controls.frmSearch _frmSearch = new CoreComponent.Controls.frmSearch(CoreComponent.Controls.SearchTypes.ItemLocationVendor, _collection);
                    CoreComponent.MasterData.BusinessObjects.ItemVendorLocationDetails _Item = new ItemVendorLocationDetails();
                    //_frmSearch.MdiParent = this.MdiParent;
                    _frmSearch.ShowDialog();

                    if (_frmSearch.DialogResult == DialogResult.OK)
                    {
                        _Item = (CoreComponent.MasterData.BusinessObjects.ItemVendorLocationDetails)_frmSearch.ReturnObject;
                        if (_Item != null)
                        {
                            txtItemCode.Text = _Item.ItemCode.ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Common.LogException(ex);
                }
            }
        }
        #endregion

        #region GridView Events
        private void dgvPoSearch_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
                {
                    if (dgvPoSearch.Columns[e.ColumnIndex].CellType == typeof(DataGridViewImageCell))
                    {
                        if (Convert.ToString(dgvPoSearch.Rows[e.RowIndex].Cells[CON_GRID_PONo].Value) != "")
                        {
                            PoNumber = Convert.ToString(dgvPoSearch.Rows[e.RowIndex].Cells[CON_GRID_PONo].Value);
                            AmendmentNo = Convert.ToInt32(dgvPoSearch.Rows[e.RowIndex].Cells[CON_GRID_AmendmentNo].Value);
                            m_createdBy = m_lstSearch[e.RowIndex].CreatedBy;
                            ResetCreateForm();
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

        private void dgvPoSearch_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (dgvPoSearch.SelectedRows.Count > 0)
                {
                    PoNumber = Convert.ToString(dgvPoSearch.SelectedRows[0].Cells[CON_GRID_PONo].Value);
                    AmendmentNo = Convert.ToInt32(dgvPoSearch.SelectedRows[0].Cells[CON_GRID_AmendmentNo].Value);
                    m_createdBy = m_lstSearch[dgvPoSearch.SelectedRows[0].Index].CreatedBy;
                    ResetCreateForm();
                    tabControlTransaction.SelectTab("tabCreate");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        private void dgvPOItems_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
                {
                    if (dgvPOItems.Columns[e.ColumnIndex].CellType == typeof(DataGridViewImageCell))
                    {
                        Remove(e.RowIndex);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        private void dgvPOItems_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                PurchaseOrderDetail detail = new PurchaseOrderDetail();
                if (dgvPOItems.SelectedRows.Count > 0)
                {
                    detail = m_Purchase.PurchaseOrderDetail[dgvPOItems.SelectedRows[0].Index];
                    m_PurchaseDetail = CopyPurchaseOrderDetailObject(detail);
                    txtItemCode.Enabled = false;
                }
                else
                {
                    m_PurchaseDetail = null;
                    detail = null;
                }
                SetDetailValues(detail);
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }
        #endregion

        #endregion

        #region Validation Methods

        private bool Validate_PurchaseDetail(ref PurchaseOrderDetail detail, int locationID, int vendorID)
        {
            try
            {
                ItemVendorLocationDetails _item = GetItemDetail(detail.ItemCode, locationID, vendorID, 0);
                DataTable dtUOM = new DataTable();
                if (_item != null)
                {
                    dtUOM = Common.ParameterLookup(Common.ParameterType.Item, new ParameterFilter("", _item.ItemId, 0, 0));
                    if (dtUOM != null && dtUOM.Rows.Count > 0)
                    {
                        detail.PurchaseUOM = Convert.ToString(dtUOM.Rows[0]["UOMName"]);
                        detail.PurchaseUOMID = Convert.ToInt32(dtUOM.Rows[0]["UOMId"]);
                        return false;
                    }
                    detail.ItemID = _item.ItemId;
                    detail.AmendmentNo = AmendmentNo;
                    detail.ItemCode = _item.ItemCode;
                    detail.ItemDescription = _item.ItemName;
                    detail.UnitPrice = (decimal)_item.CostForLocation;
                    detail.LeadTime = _item.LeadTime;
                    detail.PUF = _item.PurchaseUnitFactor;
                    detail.MOQ = _item.MinOrderQuantity;
                    return true;
                }
                else
                {
                    return false;
                    //Invalid Item
                    //  errorCreatePO.SetError(txtItemCode, Common.GetMessage("INF0010", lblItemCode.Text.Trim().Substring(0, lblItemCode.Text.Trim().Length - 2)));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool Validate_ItemCode()
        {
            try
            {
                bool isvalid = false;
                //Get Item Details
                ItemVendorLocationDetails _item = GetItemDetail(txtItemCode.Text.Trim(), (int)cmbDestinationLocation.SelectedValue, (int)cmbVendorCode.SelectedValue, 0);
                DataTable dtUOM = new DataTable();
                m_PurchaseDetail = new PurchaseOrderDetail((int)cmbDestinationLocation.SelectedValue, chkFormCApplicable.Checked, this.m_Purchase.TaxJurisdictionId, this.destinationAddress.StateId);
                if (_item != null && _item.IsCompositeItem != 1)
                {
                    isvalid = true;
                    //DataTable dtVendors = Common.ParameterLookup(Common.ParameterType.Vendor, new ParameterFilter("", vendorID, 0, 0));
                    dtUOM = Common.ParameterLookup(Common.ParameterType.Item, new ParameterFilter("", _item.ItemId, 0, 0));
                    if (dtUOM != null && dtUOM.Rows.Count > 0)
                    {
                        m_PurchaseDetail.PurchaseUOM = Convert.ToString(dtUOM.Rows[0]["UOMName"]);
                        m_PurchaseDetail.PurchaseUOMID = Convert.ToInt32(dtUOM.Rows[0]["UOMId"]);
                        m_PurchaseDetail.ItemID = _item.ItemId;
                        m_PurchaseDetail.AmendmentNo = AmendmentNo;
                        m_PurchaseDetail.ItemCode = _item.ItemCode;
                        m_PurchaseDetail.ItemDescription = _item.ItemName;
                        m_PurchaseDetail.UnitPrice = (decimal)_item.CostForLocation;
                        m_PurchaseDetail.LeadTime = _item.LeadTime;
                        m_PurchaseDetail.PUF = _item.PurchaseUnitFactor;
                        m_PurchaseDetail.MOQ = _item.MinOrderQuantity;
                        m_PurchaseDetail.PONumber = PoNumber;


                        m_PurchaseDetail.GetAndCalculateTaxes(false, GetVendorCode(), _item.LocationId.ToString()); 
                        foreach (PurchaseOrderTaxDetail tax in m_PurchaseDetail.PurchaseOrderTaxDetail)
                        {
                            tax.PONumber = PoNumber;
                            tax.CreatedBy = UserID;
                        }
                    }
                    else
                    {
                        isvalid = false;
                        errorCreatePO.SetError(txtItemCode, Common.GetMessage("INF0099"));
                    }
                }
                else
                {
                    isvalid = false;
                    //Invalid Item
                    errorCreatePO.SetError(txtItemCode, Common.GetMessage("INF0010", lblItemCode.Text.Trim().Substring(0, lblItemCode.Text.Trim().Length - 2)));
                }
                return isvalid;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private string GetVendorCode()
        {
            //DataRowView drView = cmbVendorCode.SelectedItem as DataRowView;
            string sVendorCode = "";
            if (cmbVendorCode.SelectedIndex != -1)
                sVendorCode = cmbVendorCode.SelectedValue.ToString();
            return sVendorCode;
        }

        private string GetLocationCode()
        {
            //DataRowView drView = cmbDestinationLocation.SelectedItem as DataRowView;
            string sLocationCode = "";
            if (cmbDestinationLocation.SelectedIndex !=-1)
                sLocationCode = cmbDestinationLocation.SelectedValue.ToString();
                // sLocationCode = drView.Row.ItemArray[2].ToString();
            return sLocationCode;
        }

        private bool CheckItemFields()
        {
            bool isvalid = true;
            // Check Item Code Blank
            errorCreatePO.Clear();
            //Check Vendor Code
            if (CoreComponent.Core.BusinessObjects.Validators.CheckForSelectedValue(cmbVendorCode.SelectedIndex))
            {
                isvalid = false;
                errorCreatePO.SetError(cmbVendorCode, Common.GetMessage("VAL0002", "Vendor"));
            }
            // Check Destination Location
            if (CoreComponent.Core.BusinessObjects.Validators.CheckForSelectedValue(cmbDestinationLocation.SelectedIndex))
            {
                isvalid = false;
                errorCreatePO.SetError(cmbDestinationLocation, Common.GetMessage("VAL0002", "Destination Location"));
            }
            return isvalid;
        }

        private StringBuilder GenerateCreateError()
        {
            bool focus = false;
            StringBuilder sbError = new StringBuilder();
            if (errorCreatePO.GetError(cmbVendorCode).Trim().Length > 0)
            {
                sbError.Append(errorCreatePO.GetError(cmbVendorCode));
                sbError.AppendLine();
                if (!focus)
                {
                    cmbVendorCode.Focus();
                    focus = true;
                }
            }
            if (errorCreatePO.GetError(cmbDestinationLocation).Trim().Length > 0)
            {
                sbError.Append(errorCreatePO.GetError(cmbDestinationLocation));
                sbError.AppendLine();
                if (!focus)
                {
                    cmbDestinationLocation.Focus();
                    focus = true;
                }
            }
            if (errorCreatePO.GetError(txtItemCode).Trim().Length > 0)
            {
                sbError.Append(errorCreatePO.GetError(txtItemCode));
                sbError.AppendLine();
                if (!focus)
                {
                    txtItemCode.Focus();
                    focus = true;
                }
            }

            sbError = Common.ReturnErrorMessage(sbError);
            return sbError;
        }

        private StringBuilder GenerateAddError()
        {
            bool focus = false;
            StringBuilder sbError = new StringBuilder();
            if (errorCreatePO.GetError(cmbVendorCode).Trim().Length > 0)
            {
                sbError.Append(errorCreatePO.GetError(cmbVendorCode));
                sbError.AppendLine();
                if (!focus)
                {
                    cmbVendorCode.Focus();
                    focus = true;
                }
            }
            if (errorCreatePO.GetError(cmbDestinationLocation).Trim().Length > 0)
            {
                sbError.Append(errorCreatePO.GetError(cmbDestinationLocation));
                sbError.AppendLine();
                if (!focus)
                {
                    cmbDestinationLocation.Focus();
                    focus = true;
                }
            }
            if (errorCreatePO.GetError(txtItemCode).Trim().Length > 0)
            {
                sbError.Append(errorCreatePO.GetError(txtItemCode));
                sbError.AppendLine();
                if (!focus)
                {
                    txtItemCode.Focus();
                    focus = true;
                }
            }
            if (errorCreatePO.GetError(txtUnitQty).Trim().Length > 0)
            {
                sbError.Append(errorCreatePO.GetError(txtUnitQty));
                sbError.AppendLine();
                if (!focus)
                {
                    txtUnitQty.Focus();
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
            if (errorCreatePO.GetError(cmbVendorCode).Trim().Length > 0)
            {
                sbError.Append(errorCreatePO.GetError(cmbVendorCode));
                sbError.AppendLine();
                if (!focus)
                {
                    cmbVendorCode.Focus();
                    focus = true;
                }
            }
            if (errorCreatePO.GetError(cmbDestinationLocation).Trim().Length > 0)
            {
                sbError.Append(errorCreatePO.GetError(cmbDestinationLocation));
                sbError.AppendLine();
                if (!focus)
                {
                    cmbDestinationLocation.Focus();
                    focus = true;
                }
            }
            if (errorCreatePO.GetError(dgvPOItems).Trim().Length > 0)
            {
                sbError.Append(errorCreatePO.GetError(dgvPOItems));
                sbError.AppendLine();
                if (!focus)
                {
                    txtItemCode.Focus();
                    focus = true;
                }
            }
            if (errorCreatePO.GetError(dtpExpDeliveryDate).Trim().Length > 0)
            {
                sbError.Append(errorCreatePO.GetError(dtpExpDeliveryDate));
                sbError.AppendLine();
                if (!focus)
                {
                    txtItemCode.Focus();
                    focus = true;
                }
            }
            if (errorCreatePO.GetError(dtpMaxDeliveryDate).Trim().Length > 0)
            {
                sbError.Append(errorCreatePO.GetError(dtpMaxDeliveryDate));
                sbError.AppendLine();
                if (!focus)
                {
                    txtItemCode.Focus();
                    focus = true;
                }
            }
            sbError = Common.ReturnErrorMessage(sbError);
            return sbError;
        }

        private StringBuilder GenerateSearchError()
        {
            StringBuilder sbError = new StringBuilder();
            if (errorSearch.GetError(dtpSearchFromPODate).Trim().Length > 0)
            {
                sbError.Append(errorSearch.GetError(dtpSearchFromPODate));
                sbError.AppendLine();
                dtpSearchFromPODate.Focus();
            }
            sbError = Common.ReturnErrorMessage(sbError);
            return sbError;
        }

        private void ValidateSave()
        {
            // Check Item Code Blank
            errorCreatePO.SetError(txtItemCode, string.Empty);
            errorCreatePO.SetError(txtUnitQty, string.Empty);
            errorCreatePO.SetError(dtpMaxDeliveryDate, string.Empty);
            errorCreatePO.SetError(dtpExpDeliveryDate, string.Empty);
            errorCreatePO.Clear();
            //Check Vendor Code
            if (CoreComponent.Core.BusinessObjects.Validators.CheckForSelectedValue(cmbVendorCode.SelectedIndex))
            {
                errorCreatePO.SetError(cmbVendorCode, Common.GetMessage("VAL0002", "Vendor"));
            }
            //Check Destination ID
            if (CoreComponent.Core.BusinessObjects.Validators.CheckForSelectedValue(cmbDestinationLocation.SelectedIndex))
            {
                errorCreatePO.SetError(cmbDestinationLocation, Common.GetMessage("VAL0002", "Destination Location"));
            }
            if (m_Purchase.PurchaseOrderDetail.Count == 0)
            {
                errorCreatePO.SetError(dgvPOItems, Common.GetMessage("VAL0024", " Item "));
            }
            int days = DateTime.Compare(DateTime.Today, dtpMaxDeliveryDate.Value);
            if (days == 1)
            {
                errorCreatePO.SetError(dtpMaxDeliveryDate, Common.GetMessage("INF0098", "Max Delivery Date", "Today Date"));            
            }
            days = DateTime.Compare(DateTime.Today, dtpExpDeliveryDate.Value);
            if (days == 1)
            {
                errorCreatePO.SetError(dtpExpDeliveryDate, Common.GetMessage("INF0098", "Expected Delivery Date", "Today Date"));
            }

        }

        private void ValidateAdd()
        {
            // Check Item Code Blank
            errorCreatePO.Clear();
            bool isvalid = CheckItemFields();

            // Check for blank Item Code
            bool isTextBoxEmpty = CoreComponent.Core.BusinessObjects.Validators.CheckForEmptyString(txtItemCode.Text.Length);
            if (isTextBoxEmpty == true)
            {
                errorCreatePO.SetError(txtItemCode, Common.GetMessage("INF0010", lblItemCode.Text.Trim().Substring(0, lblItemCode.Text.Trim().Length - 2)));
            }
            else
            {
                if (isvalid)
                {
                    Validate_ItemCode();
                }
            }

            isTextBoxEmpty = CoreComponent.Core.BusinessObjects.Validators.CheckForEmptyString(txtUnitQty.Text.Trim().Length);
            if (isTextBoxEmpty == true)
            {
                errorCreatePO.SetError(txtUnitQty, Common.GetMessage("INF0001", lblUnitQty.Text.Trim().Substring(0, lblUnitQty.Text.Trim().Length - 2)));
            }
            else if (Validators.IsValidQuantity(txtUnitQty.Text.Trim()))
            {
                bool isGreaterThanZero = CoreComponent.Core.BusinessObjects.Validators.IsGreaterThanZero(txtUnitQty.Text.Trim());
                if (!isGreaterThanZero)
                {
                    errorCreatePO.SetError(txtUnitQty, Common.GetMessage("VAL0033", lblUnitQty.Text.Trim().Substring(0, lblUnitQty.Text.Trim().Length - 2)));
                }
            }
            else
            {
                errorCreatePO.SetError(txtUnitQty, Common.GetMessage("INF0010", lblUnitQty.Text.Trim().Substring(0, lblUnitQty.Text.Trim().Length - 2)));
            }
            

        }

        private void ValidateSearch()
        {
            errorSearch.SetError(dtpSearchFromPODate, string.Empty);
            if (dtpSearchFromPODate.Checked == true && dtpSearchToPODate.Checked == true)
            {
                int days = DateTime.Compare(dtpSearchFromPODate.Value, dtpSearchToPODate.Value);
                if (days == 1)
                {
                    errorSearch.SetError(dtpSearchFromPODate, Common.GetMessage("INF0034", "From Date", "To Date"));
                }
            }
        }

        #endregion

        #region Helping Methods  Object creation and  Calculation

        private void CreateSaveObject()
        {
            try
            {
                m_Purchase.CreatedBy = UserID;
                m_Purchase.AmendmentNo = Convert.ToInt32(txtPOAmendmentNumber.Text.Trim());
                m_Purchase.DeliveryAddress = destinationAddress;
                m_Purchase.VendorAddress = vendorAddress;
                m_Purchase.VendorName = txtVendorName.Text.Trim();
                m_Purchase.VendorCode = cmbVendorCode.Text;
                m_Purchase.DestinationLocationID = Convert.ToInt32(cmbDestinationLocation.SelectedValue);
                m_Purchase.ExpectedDeliveryDate = Convert.ToDateTime(dtpExpDeliveryDate.Value.ToString(Common.DATE_TIME_FORMAT));
                m_Purchase.MaxDeliveryDate = Convert.ToDateTime(dtpMaxDeliveryDate.Value.ToString(Common.DATE_TIME_FORMAT));
                m_Purchase.PaymentDetails = txtPaymentDetails.Text.Trim();
                m_Purchase.PaymentTerms = txtPaymentTerms.Text.Trim();
               // m_Purchase.PODate = Common.DATETIME_NULL;
                m_Purchase.Remarks = txtRemarks.Text.Trim();
                m_Purchase.ShippingDetails = txtShippingDetails.Text.Trim();
                m_Purchase.TotalPOAmount = Convert.ToDecimal(txtTotalPOAmount.Text.Trim());
                m_Purchase.TotalTaxAmount = Convert.ToDecimal(txtTotalTaxAmount.Text.Trim());
                m_Purchase.VendorID = Convert.ToInt32(cmbVendorCode.SelectedValue);
                m_Purchase.Status = (int)Common.POStatus.Created;
                m_Purchase.IsFormCApplicable = (bool)chkFormCApplicable.Checked;
                m_Purchase.ModifiedBy = UserID; int i = 1;
                m_createdBy = UserID;
                foreach (PurchaseOrderDetail detail in m_Purchase.PurchaseOrderDetail)
                {
                    detail.PONumber = PoNumber;
                    detail.AmendmentNo = AmendmentNo;
                    foreach (PurchaseOrderTaxDetail taxdetail in detail.PurchaseOrderTaxDetail)
                    {
                        taxdetail.AmendmentNo = m_Purchase.AmendmentNo;
                        taxdetail.PONumber = PoNumber;
                        taxdetail.ModifiedBy = UserID;
                        taxdetail.CreatedBy = UserID;
                        taxdetail.RowNo = i++;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }

        }

        private void CreateAmendObject()
        {
            try
            {
                m_Purchase.PODate = Common.DATETIME_CURRENT;
                m_Purchase.AmendmentNo = m_Purchase.AmendmentNo + 1;
                m_Purchase.CreatedBy = UserID;
                m_Purchase.CreatedDate = Common.DATETIME_CURRENT;
                m_Purchase.ExpectedDeliveryDate = Convert.ToDateTime(dtpExpDeliveryDate.Value.ToString(Common.DATE_TIME_FORMAT));
                m_Purchase.MaxDeliveryDate = Convert.ToDateTime(dtpMaxDeliveryDate.Value.ToString(Common.DATE_TIME_FORMAT));
                m_Purchase.Remarks = txtRemarks.Text.Trim();
                m_Purchase.PaymentDetails = txtPaymentDetails.Text.Trim();
                m_Purchase.TotalPOAmount = Convert.ToDecimal(txtTotalPOAmount.Text.Trim());
                m_Purchase.TotalTaxAmount = Convert.ToDecimal(txtTotalTaxAmount.Text.Trim());
                m_Purchase.ModifiedBy = UserID;
                m_Purchase.ModifiedDate = Common.DATETIME_CURRENT;
                int i = 1;
                foreach (PurchaseOrderDetail detail in m_Purchase.PurchaseOrderDetail)
                {
                    detail.AmendmentNo = m_Purchase.AmendmentNo;
                    foreach (PurchaseOrderTaxDetail taxdetail in detail.PurchaseOrderTaxDetail)
                    {
                        taxdetail.AmendmentNo = m_Purchase.AmendmentNo;
                        taxdetail.CreatedBy = UserID;
                        taxdetail.ModifiedBy = UserID;
                        taxdetail.RowNo = i++;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        private List<PurchaseOrderDetail> CopyPurchaseOrderDetail(List<PurchaseOrderDetail> CopyFrom)
        {
            List<PurchaseOrderDetail> PODetailList = new List<PurchaseOrderDetail>();
            if (CopyFrom != null)
            {
                foreach (PurchaseOrderDetail p in CopyFrom)
                {
                    PurchaseOrderDetail newpo = new PurchaseOrderDetail();
                    newpo = p;
                    PODetailList.Add(newpo);
                }
            }
            return PODetailList;
        }
        private PurchaseOrderDetail CopyPurchaseOrderDetailObject(PurchaseOrderDetail CopyFrom)
        {
            if (CopyFrom != null)
            {
                PurchaseOrderDetail detail = new PurchaseOrderDetail();
                detail.AmendmentNo = CopyFrom.AmendmentNo;
                detail.CreatedBy = CopyFrom.CreatedBy;
                detail.CreatedDate = CopyFrom.CreatedDate;
                detail.ItemCode = CopyFrom.ItemCode;
                detail.ItemDescription = CopyFrom.ItemDescription;
                detail.ItemID = CopyFrom.ItemID;
                detail.LeadTime = CopyFrom.LeadTime;
                detail.ModifiedBy = CopyFrom.ModifiedBy;
                detail.ModifiedDate = CopyFrom.ModifiedDate;
                detail.MOQ = CopyFrom.MOQ;
                detail.PONumber = CopyFrom.PONumber;
                detail.PUF = CopyFrom.PUF;
                detail.PurchaseOrderTaxDetail = CopyFrom.PurchaseOrderTaxDetail;
                detail.PurchaseUOM = CopyFrom.PurchaseUOM;
                detail.PurchaseUOMID = CopyFrom.PurchaseUOMID;
                detail.Status = CopyFrom.Status;
                detail.StatusName = CopyFrom.StatusName;
                detail.TaxGroupCode = CopyFrom.TaxGroupCode;
                detail.TaxGroupCodeName = CopyFrom.TaxGroupCodeName;
                detail.UnitPrice = CopyFrom.UnitPrice;
                detail.UnitQty = CopyFrom.UnitQty;
                return detail;
            }
            else
                return null;

        }
        #endregion

        #region Printing Screen Report
        /// <summary>
        /// Creates DataSet for Printing TO Screen report
        /// </summary>
        private void CreatePrintDataSet()
        {
            m_printDataSet = new DataSet();
            // Get Data For PO Header Informaton in PO Screen Report
            DataTable dtPOHeader = new DataTable("POHeader");
            DataColumn PoNumber = new DataColumn("PoNumber", System.Type.GetType("System.String"));
            DataColumn AmendmentNumber = new DataColumn("AmendmentNumber", System.Type.GetType("System.String"));
            DataColumn PoType = new DataColumn("PoType", System.Type.GetType("System.String"));
            DataColumn VendorCode = new DataColumn("VendorCode", System.Type.GetType("System.String"));
            DataColumn VendorName = new DataColumn("VendorName", System.Type.GetType("System.String"));
            DataColumn DestinationLocation = new DataColumn("DestinationLocation", System.Type.GetType("System.String"));
            DataColumn CreatedDate = new DataColumn("CreatedDate", System.Type.GetType("System.String"));
            DataColumn VendorAddress1 = new DataColumn("VendorAddress1", System.Type.GetType("System.String"));
            DataColumn VendorAddress2 = new DataColumn("VendorAddress2", System.Type.GetType("System.String"));
            DataColumn VendorAddress3 = new DataColumn("VendorAddress3", System.Type.GetType("System.String"));
            DataColumn VendorAddress4 = new DataColumn("VendorAddress4", System.Type.GetType("System.String"));
            DataColumn VendorCity = new DataColumn("VendorCity", System.Type.GetType("System.String"));
            DataColumn VendorState = new DataColumn("VendorState", System.Type.GetType("System.String"));
            DataColumn VendorPinCode = new DataColumn("VendorPinCode", System.Type.GetType("System.String"));
            DataColumn VendorPhone = new DataColumn("VendorPhone", System.Type.GetType("System.String"));
            DataColumn DestinationAddress1 = new DataColumn("DestinationAddress1", System.Type.GetType("System.String"));
            DataColumn DestinationAddress2 = new DataColumn("DestinationAddress2", System.Type.GetType("System.String"));
            DataColumn DestinationAddress3 = new DataColumn("DestinationAddress3", System.Type.GetType("System.String"));
            DataColumn DestinationAddress4 = new DataColumn("DestinationAddress4", System.Type.GetType("System.String"));
            DataColumn DestinationCity = new DataColumn("DestinationCity", System.Type.GetType("System.String"));
            DataColumn DestinationState = new DataColumn("DestinationState", System.Type.GetType("System.String"));
            DataColumn DestinationPinCode = new DataColumn("DestinationPinCode", System.Type.GetType("System.String"));
            DataColumn DestinationPhone = new DataColumn("DestinationPhone", System.Type.GetType("System.String"));
            DataColumn PoDate = new DataColumn("PoDate", System.Type.GetType("System.String"));
            DataColumn ExpectedDeliveryDate = new DataColumn("ExpectedDeliveryDate", System.Type.GetType("System.String"));
            DataColumn MaxDeliveryDate = new DataColumn("MaxDeliveryDate", System.Type.GetType("System.String"));
            DataColumn Status = new DataColumn("Status", System.Type.GetType("System.String"));
            DataColumn FormCApplicable = new DataColumn("FormCApplicable", System.Type.GetType("System.String"));
            DataColumn Remarks = new DataColumn("Remarks", System.Type.GetType("System.String"));
            DataColumn TotalTaxAmount = new DataColumn("TotalTaxAmount", System.Type.GetType("System.String"));
            DataColumn ShippingDetails = new DataColumn("ShippingDetails", System.Type.GetType("System.String"));
            DataColumn PaymentDetails = new DataColumn("PaymentDetails", System.Type.GetType("System.String"));
            DataColumn TotalPoAmount = new DataColumn("TotalPoAmount", System.Type.GetType("System.String"));
            DataColumn CreatedBy = new DataColumn("CreatedBy", System.Type.GetType("System.String"));
            DataColumn PrintedBy = new DataColumn("PrintedBy", System.Type.GetType("System.String"));
            DataColumn VendorTINNo = new DataColumn("VendorTINNo", System.Type.GetType("System.String"));
            DataColumn VendorCSTNo = new DataColumn("VendorCSTNo", System.Type.GetType("System.String"));
            DataColumn LocationTINNo = new DataColumn("LocationTINNo", System.Type.GetType("System.String"));

            
            dtPOHeader.Columns.Add(PoNumber);
            dtPOHeader.Columns.Add(AmendmentNumber);
            dtPOHeader.Columns.Add(PoType);
            dtPOHeader.Columns.Add(VendorCode);
            dtPOHeader.Columns.Add(VendorName);
            dtPOHeader.Columns.Add(DestinationLocation);
            dtPOHeader.Columns.Add(CreatedDate);
            dtPOHeader.Columns.Add(VendorAddress1);
            dtPOHeader.Columns.Add(VendorAddress2);
            dtPOHeader.Columns.Add(VendorAddress3);
            dtPOHeader.Columns.Add(VendorAddress4);
            dtPOHeader.Columns.Add(VendorCity);
            dtPOHeader.Columns.Add(VendorState);
            dtPOHeader.Columns.Add(VendorPinCode);
            dtPOHeader.Columns.Add(VendorPhone);
            dtPOHeader.Columns.Add(DestinationAddress1);
            dtPOHeader.Columns.Add(DestinationAddress2);
            dtPOHeader.Columns.Add(DestinationAddress3);
            dtPOHeader.Columns.Add(DestinationAddress4);
            dtPOHeader.Columns.Add(DestinationCity);
            dtPOHeader.Columns.Add(DestinationState);
            dtPOHeader.Columns.Add(DestinationPinCode);
            dtPOHeader.Columns.Add(DestinationPhone);
            dtPOHeader.Columns.Add(PoDate);
            dtPOHeader.Columns.Add(ExpectedDeliveryDate);
            dtPOHeader.Columns.Add(MaxDeliveryDate);
            dtPOHeader.Columns.Add(Status);
            dtPOHeader.Columns.Add(FormCApplicable);
            dtPOHeader.Columns.Add(Remarks);
            dtPOHeader.Columns.Add(TotalTaxAmount);
            dtPOHeader.Columns.Add(ShippingDetails);
            dtPOHeader.Columns.Add(PaymentDetails);
            dtPOHeader.Columns.Add(TotalPoAmount);
            dtPOHeader.Columns.Add(CreatedBy);
            dtPOHeader.Columns.Add(PrintedBy);
            dtPOHeader.Columns.Add(VendorTINNo);
            dtPOHeader.Columns.Add(VendorCSTNo);
            dtPOHeader.Columns.Add(LocationTINNo);
            DataRow dRow = dtPOHeader.NewRow();
            dRow["PoNumber"] = m_Purchase.PONumber;
            dRow["AmendmentNumber"] = m_Purchase.AmendmentNo;
            dRow["PoType"] = m_Purchase.POTypeName;
            dRow["VendorCode"] = m_Purchase.VendorCode;
            dRow["VendorName"] = m_Purchase.VendorName;
            dRow["DestinationLocation"] = m_Purchase.DestinationLocationCode;
            dRow["CreatedDate"] = Convert.ToDateTime(m_Purchase.CreatedDate).ToString(Common.DTP_DATE_FORMAT);
            dRow["VendorAddress1"] = m_Purchase.VendorAddress.Address1;
            dRow["VendorAddress2"] = m_Purchase.VendorAddress.Address2;
            dRow["VendorAddress3"] = m_Purchase.VendorAddress.Address3;
            dRow["VendorAddress4"] = m_Purchase.VendorAddress.Address4;
            dRow["VendorCity"] = m_Purchase.VendorAddress.City;
            dRow["VendorState"] = m_Purchase.VendorAddress.State;
            dRow["VendorPinCode"] = m_Purchase.VendorAddress.PinCode;
            dRow["VendorPhone"] = m_Purchase.VendorAddress.PhoneNumber1;
            dRow["DestinationAddress1"] = m_Purchase.DeliveryAddress.Address1;
            dRow["DestinationAddress2"] = m_Purchase.DeliveryAddress.Address2;
            dRow["DestinationAddress3"] = m_Purchase.DeliveryAddress.Address3;
            dRow["DestinationAddress4"] = m_Purchase.DeliveryAddress.Address4;
            dRow["DestinationCity"] = m_Purchase.DeliveryAddress.City;
            dRow["DestinationState"] = m_Purchase.DeliveryAddress.State;
            dRow["DestinationPinCode"] = m_Purchase.DeliveryAddress.PinCode;
            dRow["DestinationPhone"] = m_Purchase.DeliveryAddress.PhoneNumber1;
            dRow["PoDate"] = Convert.ToDateTime(m_Purchase.PODate).ToString(Common.DTP_DATE_FORMAT);
            dRow["ExpectedDeliveryDate"] = Convert.ToDateTime(m_Purchase.ExpectedDeliveryDate).ToString(Common.DTP_DATE_FORMAT);
            dRow["MaxDeliveryDate"] = Convert.ToDateTime(m_Purchase.MaxDeliveryDate).ToString(Common.DTP_DATE_FORMAT);
            dRow["Status"] = m_Purchase.StatusName;
            dRow["FormCApplicable"] = m_Purchase.IsFormCApplicable;
            dRow["Remarks"] = m_Purchase.Remarks;
            dRow["TotalTaxAmount"] = m_Purchase.TotalTaxAmount;
            dRow["ShippingDetails"] = m_Purchase.ShippingDetails;
            dRow["PaymentDetails"] = m_Purchase.PaymentDetails;
            dRow["TotalPoAmount"] = m_Purchase.TotalPOAmount - m_Purchase.TotalTaxAmount;
            DataTable dtUserName = Common.ParameterLookup(Common.ParameterType.GetUserNamebyId, new ParameterFilter("", m_Purchase.CreatedBy, Common.INT_DBNULL, Common.INT_DBNULL));
            if (dtUserName != null && dtUserName.Rows.Count == 1)
                dRow["CreatedBy"] = dtUserName.Rows[0][0].ToString();
            else
                dRow["CreatedBy"] = string.Empty;
            //dRow["PrintedBy"] = AuthenticationComponent.BusinessObjects.Authenticate.LoggedInUser.UserName;
            DataTable dtLoginName = Common.ParameterLookup(Common.ParameterType.GetUserNamebyId, new ParameterFilter("", AuthenticationComponent.BusinessObjects.Authenticate.LoggedInUser.UserId, Common.INT_DBNULL, Common.INT_DBNULL));
            if (dtLoginName != null && dtLoginName.Rows.Count == 1)
                dRow["PrintedBy"] = dtLoginName.Rows[0][0].ToString();
            else
                dRow["PrintedBy"] = string.Empty;

            DataTable dtVendorDetails = Common.ParameterLookup(Common.ParameterType.Vendor, new ParameterFilter("", m_Purchase.VendorID, -1, -1));
            if (dtVendorDetails != null && dtVendorDetails.Rows.Count > 0)
            {
                dRow["VendorTINNo"] = dtVendorDetails.Rows[0]["TinNo"].ToString();
                dRow["VendorCSTNo"] = dtVendorDetails.Rows[0]["CstNo"].ToString();                
            }
            DataTable dtLocationDetails = Common.ParameterLookup(Common.ParameterType.Locations, new ParameterFilter("", -1, m_Purchase.DestinationLocationID, -1));
            if (dtVendorDetails != null && dtVendorDetails.Rows.Count > 0)
            {
                dRow["LocationTINNo"] = dtLocationDetails.Rows[0]["TinNo"].ToString();
            }

            dtPOHeader.Rows.Add(dRow);
            // Search ItemData for dataTable
            DataTable dtPODetail = new DataTable("PODetail");
            dtPODetail = m_Purchase.SearchDetailsDataTable();
            decimal TotalAmount = 0;
            for (int i = 0; i < dtPODetail.Rows.Count; i++)
            {
                dtPODetail.Rows[i]["LineAmount"] = Math.Round(Convert.ToDecimal(dtPODetail.Rows[i]["LineAmount"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                dtPODetail.Rows[i]["UnitPrice"] = Math.Round(Convert.ToDecimal(dtPODetail.Rows[i]["UnitPrice"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                dtPODetail.Rows[i]["UnitQty"] = Math.Round(Convert.ToDecimal(dtPODetail.Rows[i]["UnitQty"]), Common.DisplayQtyRounding, MidpointRounding.AwayFromZero);
                dtPODetail.Rows[i]["LineTaxAmount"] = Math.Round(Convert.ToDecimal(dtPODetail.Rows[i]["LineTaxAmount"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                TotalAmount += Convert.ToDecimal(dtPODetail.Rows[i]["UnitPrice"]) * Convert.ToDecimal(dtPODetail.Rows[i]["UnitQty"]);
            }
            string errorMessage = string.Empty;
            DataTable dtPOTaxDetail = new DataTable("POTaxDetail");
            PurchaseOrderTaxDetail objPOTaxDetail = new PurchaseOrderTaxDetail();
            objPOTaxDetail.PONumber = m_Purchase.PONumber;
            objPOTaxDetail.AmendmentNo = m_Purchase.AmendmentNo;
            objPOTaxDetail.ItemID = 0;
            dtPOTaxDetail = objPOTaxDetail.GetSelectedRecords(Common.ToXml(objPOTaxDetail), SP_POTAXDETAIL_SEARCH, ref errorMessage);
            DataTable dtPOTaxTotal = new DataTable("POTaxTotal");
            if (dtPOTaxDetail.Rows.Count > 0)
            {
                DataTable dt = new DataTable();
                dt = dtPOTaxDetail.AsEnumerable().Distinct().CopyToDataTable();
                DataView dv = new DataView(dt.DefaultView.ToTable(true, "TaxPercent", "Description", "TaxCode"));
                DataColumn TaxPercent = new DataColumn("TaxPercent", Type.GetType("System.String"));
                DataColumn TaxDescription = new DataColumn("Description", Type.GetType("System.String"));
                DataColumn TaxAmount = new DataColumn("Amount", Type.GetType("System.String"));
                DataColumn SubTotal = new DataColumn("SubTotal", Type.GetType("System.String"));
                
                dtPOTaxTotal.Columns.Add(TaxPercent);
                dtPOTaxTotal.Columns.Add(TaxDescription);
                dtPOTaxTotal.Columns.Add(TaxAmount);
                dtPOTaxTotal.Columns.Add(SubTotal);               
 
                for (int i = 0; i < dv.Table.Rows.Count; i++)
                {
                    decimal taxAmt = 0;
                    DataRow dr = dtPOTaxTotal.NewRow();
                    for (int j = 0; j < dtPOTaxDetail.Rows.Count; j++)
                    {
                        if (dv.Table.Rows[i]["TaxCode"].ToString() == dtPOTaxDetail.Rows[j]["TaxCode"].ToString())
                            taxAmt += Convert.ToDecimal(dtPOTaxDetail.Rows[j]["TaxAmount"]);
                    }
                    dr["SubTotal"] = Math.Round(TotalAmount, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                    dr["TaxPercent"] = (Math.Round(Convert.ToDecimal(dv.Table.Rows[i]["TaxPercent"]),Common.DisplayAmountRounding,MidpointRounding.AwayFromZero)).ToString() + "% Tax";
                    dr["Description"] = dv.Table.Rows[i]["Description"];
                    dr["Amount"] = Math.Round(taxAmt, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                    dtPOTaxTotal.Rows.Add(dr);
                }
            }
            else
            {
                DataColumn TaxPercent = new DataColumn("TaxPercent", Type.GetType("System.String"));
                DataColumn TaxDescription = new DataColumn("Description", Type.GetType("System.String"));
                DataColumn TaxAmount = new DataColumn("Amount", Type.GetType("System.String"));
                DataColumn SubTotal = new DataColumn("SubTotal", Type.GetType("System.String"));
                dtPOTaxTotal.Columns.Add(TaxPercent);
                dtPOTaxTotal.Columns.Add(TaxDescription);
                dtPOTaxTotal.Columns.Add(TaxAmount);
                dtPOTaxTotal.Columns.Add(SubTotal);  
                DataRow drAdd = dtPOTaxTotal.NewRow();
                drAdd["SubTotal"] = Math.Round(TotalAmount, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                drAdd["TaxPercent"] = "Tax";
                drAdd["Description"] = "Tax";
                drAdd["Amount"] = 0.00;
                dtPOTaxTotal.Rows.Add(drAdd);
            }
            m_printDataSet.Tables.Add(dtPOHeader);
            m_printDataSet.Tables.Add(dtPODetail.Copy());
            m_printDataSet.Tables.Add(dtPOTaxTotal);
            m_printDataSet.Tables[1].TableName = "PODetail";
        }

        /// <summary>
        /// Prints TO Screen report
        /// </summary>
        private void PrintReport()
        {
            CreatePrintDataSet();
            CoreComponent.UI.ReportScreen reportScreenObj = new CoreComponent.UI.ReportScreen((int)Common.ReportType.PO, m_printDataSet);
            reportScreenObj.ShowDialog();
            //reportScreenObj.PrintReport();
            m_printDataSet = null;
        }
        #endregion

        private void chkFormCApplicable_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                ClearItem();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
       
    }
}
