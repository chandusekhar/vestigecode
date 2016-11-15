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
using System.Collections.Specialized;
namespace PurchaseComponent.UI
{
    public partial class frmGRN : CoreComponent.Core.UI.Transaction
    {
        #region Constants
        private const string CON_GRID_GRNNO = "GrnNo";
        private const string CON_COL_CHALLANQTY = "ChallanQty";
        private const string CON_COL_INVOICEQTY = "InvoiceQty";
        private const string CON_COL_POQTY = "POQty";
        private const string CON_LOCATION_DISPLAYNAME = "DisplayName";
        private const string CON_LOCATION_ID = "LocationId";

        #endregion

        #region Variables Declaration
        List<Grn> m_listSearch = null;       
        private Grn m_CurrentGRN = null;
        private string m_PONo = string.Empty;
        private GrnDetail m_CurrentGRNDetail = null;
        private string m_GRNNO=string.Empty;
       
        CurrencyManager m_bindingMgr;
        AutoCompleteStringCollection _itemcollection = null;
       
        int m_Status =(int) Common.GRNStatus.New;   
       
        private Boolean IsSaveAvailable = false;
        private Boolean IsUpdateAvailable = false;
        private Boolean IsCancelAvailable = false;
        private Boolean IsClosedAvailable = false;
        private Boolean IsSearchAvailable = false;
        private Boolean m_isPrintAvailable = false;

        private string CON_MODULENAME = string.Empty;
        private string GRIDVIEW_XML_PATH = Environment.CurrentDirectory + "\\App_Data\\Purchase.xml";
        private int m_LocationID = Common.CurrentLocationId;
        private string m_LocationCode = Common.LocationCode;
        private string m_UserName = string.Empty;
        int m_UserID = -1;
        private Common.LocationConfigId m_LocationType = (Common.LocationConfigId)Common.CurrentLocationTypeId;
        private DataSet m_printDataSet = null;
        

        #endregion

        #region Constants
        private const string CON_GRN_PARAM = "GRNSTATUS";
        private const string CON_VENDOR_DISPLAYNAME = "VendorCode";
        private const string CON_VENDOR_ID = "VendorId";
        
        
        #endregion

        #region Constructor
        public frmGRN()
        {
            try
            {
                InitializeComponent();
            }
            catch(Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex); 
            }
        }

        #endregion

        #region Initialize Methods
        /// <summary>
        /// Initilize Rights On Form
        /// </summary>
        /// 
        private void frmGRN_Load(object sender, EventArgs e)
        {
            try
            {
                CON_MODULENAME = this.Tag.ToString();
                if (AuthenticationComponent.BusinessObjects.Authenticate.LoggedInUser != null)
                {
                    m_UserID = AuthenticationComponent.BusinessObjects.Authenticate.LoggedInUser.UserId;
                    m_UserName = AuthenticationComponent.BusinessObjects.Authenticate.LoggedInUser.UserName;
                }
                //Initialize Rights
                InitializeRights();
                //Initialize Controls
                InitializeControls();
                //Set Form
                ResetCreateForm();
                lblPageTitle.Text = "Goods Receipt Note";
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }
        private void InitializeRights()
        {
            if (m_UserName != null && !m_UserName.Equals(string.Empty) && CON_MODULENAME != null && !CON_MODULENAME.Equals(string.Empty))
            {
                IsCancelAvailable = AuthenticationComponent.BusinessObjects.Authenticate.IsFunctionAccessible(m_UserName, m_LocationCode, CON_MODULENAME, Common.FUNCTIONCODE_CANCEL);
                IsClosedAvailable = AuthenticationComponent.BusinessObjects.Authenticate.IsFunctionAccessible(m_UserName, m_LocationCode, CON_MODULENAME, Common.FUNCTIONCODE_CLOSE);
                IsSaveAvailable = AuthenticationComponent.BusinessObjects.Authenticate.IsFunctionAccessible(m_UserName, m_LocationCode, CON_MODULENAME, Common.FUNCTIONCODE_SAVE);
                IsUpdateAvailable = AuthenticationComponent.BusinessObjects.Authenticate.IsFunctionAccessible(m_UserName, m_LocationCode, CON_MODULENAME, Common.FUNCTIONCODE_UPDATE);
                IsSearchAvailable = AuthenticationComponent.BusinessObjects.Authenticate.IsFunctionAccessible(m_UserName, m_LocationCode, CON_MODULENAME, Common.FUNCTIONCODE_SEARCH);
                m_isPrintAvailable = AuthenticationComponent.BusinessObjects.Authenticate.IsFunctionAccessible(m_UserName, m_LocationCode, CON_MODULENAME, Common.FUNCTIONCODE_PRINT);
            }
        }

        /// <summary>
        /// Initialize Controls
        /// 1. Status Combobox in search Tab
        /// 2. Item List
        /// </summary>
        private void InitializeControls()
        {
            try
            {
                BindStatus();
                BindVendor();
                BindLocation();
                InitailizeGrids();
                dtpChallanDate.CustomFormat = Common.DTP_DATE_FORMAT;
                dtpInvoiceDate.CustomFormat = Common.DTP_DATE_FORMAT;
                dtpChallanDate.MaxDate = DateTime.Today;
                dtpInvoiceDate.MaxDate = DateTime.Today;
                btnSearch.Enabled = IsSearchAvailable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Bind Search Status Combobox
        /// </summary>
        private void BindStatus()
        {
            try
            {
                string parameterType = CON_GRN_PARAM;
                DataTable dtStatus = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter(parameterType, 0, 0, 0));
                if (dtStatus != null)
                {
                    cmbSearchStatus.DataSource = dtStatus;
                    cmbSearchStatus.DisplayMember = Common.KEYVALUE1;
                    cmbSearchStatus.ValueMember = Common.KEYCODE1;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Bind Search Vendor Combobox
        /// </summary>
        private void BindVendor()
        {
            try
            {
                DataTable dtVendors = Common.ParameterLookup(Common.ParameterType.ItemVendor, new ParameterFilter("", -1, 0, 0));
                if (dtVendors != null)
                {
                    cmbSearchVendorCode.DataSource = dtVendors;
                    cmbSearchVendorCode.DisplayMember = CON_VENDOR_DISPLAYNAME;
                    cmbSearchVendorCode.ValueMember = CON_VENDOR_ID;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void BindLocation()
        {
            DataTable dtLocations = Common.ParameterLookup(Common.ParameterType.Locations, new ParameterFilter("", -5, 0, 0));
            if (dtLocations != null)
            {
                cmbSearchDestinationLocation.DataSource = dtLocations;
                cmbSearchDestinationLocation.DisplayMember = CON_LOCATION_DISPLAYNAME;
                cmbSearchDestinationLocation.ValueMember = CON_LOCATION_ID;

                if (m_LocationType != Common.LocationConfigId.HO)
                {
                    cmbSearchDestinationLocation.SelectedValue = m_LocationID;
                    cmbSearchDestinationLocation.Enabled = false;
                }
            }
        }
        /// <summary>
        /// Initailize Grid Views
        /// 1. Search Gridview
        /// 2. Detail GridView
        /// </summary>
       
        private void InitailizeGrids()
        {
            try
            {
                //Search GridView
                dgvSearchGRN.AutoGenerateColumns = false;
                dgvSearchGRN.AllowUserToAddRows = false;
                dgvSearchGRN.AllowUserToDeleteRows = false;
                DataGridView dgvSearchGRnNew = Common.GetDataGridViewColumns(dgvSearchGRN, GRIDVIEW_XML_PATH);

                // Create GridView
                dgvGRNItems.AutoGenerateColumns = false;
                dgvGRNItems.AllowUserToAddRows = false;
                dgvGRNItems.AllowUserToDeleteRows = false;
              
                DataGridView dgvGRNItemsNew = Common.GetDataGridViewColumns(dgvGRNItems, GRIDVIEW_XML_PATH);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }    
        #endregion

        #region Set AND Get Method   
        public void ShowForm(string grnNo)
        {
            m_GRNNO =Convert.ToString(grnNo);
            ResetCreateForm();
            tabControlTransaction.SelectTab("tabCreate");
        }
        private void ResetCreateForm()
        {
            try
            {
                m_PONo = string.Empty;
                m_CurrentGRN = null;
                m_CurrentGRNDetail = null;
                m_Status = 0;               
                m_CurrentGRN = new Grn();
               
                if (m_GRNNO.Equals(string.Empty))
                    m_CurrentGRN = CreateGRNObject();
                else
                {
                    m_CurrentGRN = CreateGRNObject(m_GRNNO);
                    //m_ListCurrentGRN = m_CurrentGRN.GRNDetailList;
                }
                if (m_CurrentGRN != null)
                {
                    m_Status = m_CurrentGRN.Status;
                    ResetGRNValues(m_CurrentGRN);
                    SetProperty();
                }
                else
                {                    
                    MessageBox.Show(Common.GetMessage("30008"),Common.GetMessage("10001"),MessageBoxButtons.OK,MessageBoxIcon.Error);
                    tabControlTransaction.TabPages[0].Show();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void SetProperty()
        {
            string tabname = "Create";
            if(m_Status==(int)Common.GRNStatus.New && (m_LocationType==Common.LocationConfigId.WH
                || m_LocationType == Common.LocationConfigId.BO))
                tabname= "Create";
            else if(m_Status==(int)Common.GRNStatus.Created && (m_LocationType==Common.LocationConfigId.WH
                || m_LocationType == Common.LocationConfigId.BO))
                 tabname= "Update";
            else 
                tabname="View";            
            tabCreate.Text = tabname.ToString();
            if (m_Status == (int)Common.GRNStatus.New)
            {
                btnSave.Text = "&Save";
                btnSave.Enabled = EnableSave&&IsSaveAvailable;
            }
            else
            {
                btnSave.Text = "&Update";
                btnSave.Enabled = EnableUpdate && IsUpdateAvailable;
            }
            btnPrint.Enabled = m_isPrintAvailable;
            txtPONumber.Enabled = EnablePOTextBox;
            txtChallanNumber.ReadOnly = !EnableWHTextBox;
            txtInvoiceNumber.ReadOnly = !EnableHOTextBox;
            txtInvoiceAmount.ReadOnly = !EnableHOTextBox;
            txtInvoiceTax.ReadOnly = !EnableHOTextBox;
            txtNoOfBoxes.ReadOnly = !EnableWHTextBox;
            txtReceivedBy.ReadOnly = !EnableWHTextBox;
            txtVehicleNumber.ReadOnly = !EnableWHTextBox;
            dtpChallanDate.Enabled = EnableWHTextBox;
            dtpInvoiceDate.Enabled = EnableHOTextBox;
            btnReset.Enabled = EnableReset;
            btnClosed.Enabled = EnableClose && IsClosedAvailable;
            btnCancel.Enabled = EnableCancel && IsCancelAvailable;
            dgvGRNItems.Columns[CON_COL_CHALLANQTY].ReadOnly = !EnableWHTextBox;
            dgvGRNItems.Columns[CON_COL_INVOICEQTY].ReadOnly = !EnableHOTextBox;
            if(!dgvGRNItems.Columns[CON_COL_INVOICEQTY].ReadOnly)
               dgvGRNItems.Columns[CON_COL_INVOICEQTY].DefaultCellStyle.BackColor = Color.Silver;
            else
                dgvGRNItems.Columns[CON_COL_INVOICEQTY].DefaultCellStyle.BackColor = Color.White;
            if (!dgvGRNItems.Columns[CON_COL_CHALLANQTY].ReadOnly)
                dgvGRNItems.Columns[CON_COL_CHALLANQTY].DefaultCellStyle.BackColor = Color.Silver;
            else
                dgvGRNItems.Columns[CON_COL_CHALLANQTY].DefaultCellStyle.BackColor = Color.White;

        }
        private void ResetGrid(List<GrnDetail> GrnList)
        {      
            dgvGRNItems.DataSource = null;
            if (GrnList != null)
            {
                dgvGRNItems.DataSource = GrnList;
                m_bindingMgr = (CurrencyManager)this.BindingContext[GrnList];
                m_bindingMgr.Refresh();
            }
        }
        private void ResetGRNValues(Grn _grn)
        {
            try
            {
                txtPONumber.Text = _grn.PONumber;
                txtPOAmendmentNumber.Text = _grn.AmendmentNo.ToString();
                if(_grn.PODate.Trim().Equals(string.Empty))
                    lblPODateValue.Text=string.Empty;
                else
                    lblPODateValue.Text = _grn.DisplayPODate; 

                lblGrnNoValue.Text = _grn.GRNNo;

                if (_grn.GRNDate.Trim().Equals(string.Empty))
                    lblCreateGrnDate.Text = string.Empty;
                else
                    lblCreateGrnDate.Text = _grn.DisplayGRNDate;

                lblStatusValue.Text = _grn.StatusName;
                
                txtChallanNumber.Text = _grn.ChallanNo;

                dtpChallanDate.Value = Convert.ToDateTime(_grn.ChallanDate);
                txtVendorCode.Text = _grn.VendorCode;
                txtInvoiceNumber.Text = _grn.InvoiceNo;
                dtpInvoiceDate.Value = Convert.ToDateTime(_grn.InvoiceDate);
                txtVendorName.Text = _grn.VendorName;
                txtVehicleNumber.Text = _grn.VehicleNo;
                txtShippingDetails.Text = _grn.ShippingDetails;
                txtDestLocation.Text = _grn.DestinationLocation;
                txtReceivedBy.Text = _grn.ReceivedBy;
                txtGrossWeight.Text = _grn.GrossWeight;
                txtNoOfBoxes.Text = _grn.NoOfBoxes.ToString();
                txtInvoiceTax.Text = _grn.DisplayInvoiceTaxAmount.ToString();
                txtInvoiceAmount.Text = _grn.DisplayInvoiceAmount.ToString();
                ResetGrid(_grn.GRNDetailList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ResetPOValues(PurchaseOrder PO)
        {
            try
            {               
                if (PO != null)
                {
                    m_PONo = txtPONumber.Text;
                    m_CurrentGRN = CreateGRNObject(PO);
                    GrnDetail detail = new GrnDetail();
                    detail.PONumber = PO.PONumber;
                    detail.AmendmentNo = PO.AmendmentNo;
                    string errorMessage = string.Empty;
                    m_CurrentGRN.GRNDetailList = detail.GetGRNItem(ref errorMessage);
                    if (!errorMessage.Trim().Equals(string.Empty))
                    {
                        if (errorMessage == "INF0066")
                        {
                            MessageBox.Show(Common.GetMessage(errorMessage.Trim()),Common.GetMessage("10001"),MessageBoxButtons.OK,MessageBoxIcon.Information);
                            ResetCreateForm();
                        }
                        else
                        {
                            Common.LogException(new Exception(errorMessage));
                            MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else if (m_CurrentGRN.GRNDetailList != null && m_CurrentGRN.GRNDetailList.Count == 0)
                    {
                        MessageBox.Show(Common.GetMessage("INF0067"),Common.GetMessage("10001"),MessageBoxButtons.OK,MessageBoxIcon.Information);
                        ResetCreateForm();                       
                    }
                    else
                    {                        
                        ResetGRNValues(m_CurrentGRN);                      
                    }
                    SetProperty();
                }
                else
                {
                    MessageBox.Show(Common.GetMessage("VAL0009", " PO"),Common.GetMessage("10001"),MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    txtPONumber.Focus();
                    ResetCreateForm();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        #endregion

        #region Button Methods

        private void Search()
        {
            try
            {                
                Grn grn = new Grn();
                grn.GRNNo = txtSearchGRNNumber.Text.Trim();
                grn.PONumber = txtSearchPONumber.Text.Trim();
                grn.VendorCode = Convert.ToString(cmbSearchVendorCode.SelectedValue);
                grn.ReceivedBy = txtSearchReceivedBy.Text.Trim();
                grn.Status =Convert.ToInt32(cmbSearchStatus.SelectedValue);
                if(dtpSearchFrmGRNDate.Checked)
                    grn.FromGrnDate = dtpSearchFrmGRNDate.Value.ToString(Common.DATE_TIME_FORMAT);
                if(dtpSearchfrmPODate.Checked)
                    grn.FromPODate = dtpSearchfrmPODate.Value.ToString(Common.DATE_TIME_FORMAT);
                if(dtpSearchToGRNDate.Checked)
                    grn.ToGrnDate = dtpSearchToGRNDate.Value.ToString(Common.DATE_TIME_FORMAT);
                if(dtpSearchToPODate.Checked)
                    grn.ToPODate = dtpSearchToPODate.Value.ToString(Common.DATE_TIME_FORMAT);
                grn.DestLocationID = Convert.ToInt32(cmbSearchDestinationLocation.SelectedValue);
                m_listSearch=grn.Search();
                dgvSearchGRN.DataSource = null;
                dgvSearchGRN.DataSource = new List<Grn>();
                if (m_listSearch != null)
                {
                    dgvSearchGRN.DataSource = m_listSearch;
                    dgvSearchGRN.Focus();
                    if(m_listSearch.Count==0)
                        MessageBox.Show(Common.GetMessage("8002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(Common.GetMessage("30009", "Search"),Common.GetMessage("10001"),MessageBoxButtons.OK,MessageBoxIcon.Error);
                }            
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Reset Search Form Controls
        /// </summary>
        private void ResetSearch()
        {
            try
            {
                txtSearchGRNNumber.Text = string.Empty;
                txtSearchPONumber.Text = string.Empty;
                txtSearchReceivedBy.Text = string.Empty;
                cmbSearchStatus.SelectedIndex = 0;
                cmbSearchVendorCode.SelectedIndex = 0;                
                dtpSearchFrmGRNDate.Value = Common.DATETIME_CURRENT;
                dtpSearchfrmPODate.Value = Common.DATETIME_CURRENT;
                dtpSearchToGRNDate.Value = Common.DATETIME_CURRENT;
                dtpSearchToPODate.Value = Common.DATETIME_CURRENT;
                dtpSearchFrmGRNDate.Checked = false;
                dtpSearchfrmPODate.Checked = false;
                dtpSearchToGRNDate.Checked = false;
                dtpSearchToPODate.Checked = false;
                cmbSearchDestinationLocation.SelectedIndex = 0;
                if (m_LocationType != Common.LocationConfigId.HO)
                    cmbSearchDestinationLocation.SelectedValue = m_LocationID;
                //Reset Grid
                dgvSearchGRN.DataSource = null;
                //Reset Create Tab
                CreateReset();
                txtSearchGRNNumber.Focus();
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CreateSaveObject(int status)
        {
            //m_CurrentGRN.ChallanDate = dtpChallanDate.Value.ToString(System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat);
            m_CurrentGRN.ChallanDate = dtpChallanDate.Value.ToString();
            m_CurrentGRN.ChallanNo = txtChallanNumber.Text.Trim();
            m_CurrentGRN.GrossWeight = txtGrossWeight.Text.Trim();
            //m_CurrentGRN.InvoiceDate = dtpInvoiceDate.Value.ToString(System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat);
            m_CurrentGRN.InvoiceDate = dtpInvoiceDate.Value.ToString();
            m_CurrentGRN.InvoiceNo = txtInvoiceNumber.Text.Trim();
            m_CurrentGRN.GRNNo =string.IsNullOrEmpty(m_CurrentGRN.GRNNo)==true? string.Empty:m_CurrentGRN.GRNNo;
            m_CurrentGRN.NoOfBoxes = Convert.ToInt32(txtNoOfBoxes.Text.Trim());
            m_CurrentGRN.ReceivedBy = txtReceivedBy.Text.Trim();
            m_CurrentGRN.Status = status;
            m_CurrentGRN.VehicleNo = txtVehicleNumber.Text.Trim();
            m_CurrentGRN.CreatedBy = m_UserID;
            m_CurrentGRN.ModifiedBy = m_UserID;
            m_CurrentGRN.InvoiceAmount = txtInvoiceAmount.Text.Trim().Length > 0 ? Convert.ToDouble(txtInvoiceAmount.Text.Trim()) : 0;
            m_CurrentGRN.InvoiceTaxAmount = txtInvoiceTax.Text.Trim().Length > 0 ? Convert.ToDouble(txtInvoiceTax.Text.Trim()) : 0;

            m_CurrentGRN.DestinationLocation = txtDestLocation.Text.Trim();
            m_CurrentGRN.ShippingDetails = txtShippingDetails.Text.Trim();
            m_CurrentGRN.DestLocationID = Common.CurrentLocationId;
            m_CurrentGRN.VendorCode = txtVendorCode.Text;
            m_CurrentGRN.LocationCode = Common.LocationCode;
               
            int serialno = 1;
            if (m_CurrentGRN.GRNDetailList != null)
            {
                foreach (GrnDetail detail in m_CurrentGRN.GRNDetailList)
                {
                    detail.SerialNo = serialno++;
                    //    int batchserial = 1;
                    if (detail.GRNBatchDetailList != null)
                    {
                        PurchaseOrder order = new PurchaseOrder();
                        order.PONumber = detail.PONumber; 
                        order.AmendmentNo = detail.AmendmentNo;
                        List<PurchaseOrderDetail> AllOrderList = order.SearchDetails();
                        int i = 1;
                        foreach (GrnBatchDetail batch in detail.GRNBatchDetailList)
                        {
                            // batch.SerialNo = batchserial++;
                            batch.SerialNo = detail.SerialNo;
                            batch.RowNo = i++;
                            List<PurchaseOrderDetail> OrderList = AllOrderList.FindAll(delegate(PurchaseOrderDetail pd) { return pd.ItemID == batch.ItemId; });
                            if (OrderList != null && OrderList.Count > 0)
                            {
                                batch.SetTaxDetail(OrderList[0], m_CurrentGRN.VendorID.ToString(),m_CurrentGRN.ToStateId.ToString(),
                                    m_CurrentGRN.FromStateId,m_CurrentGRN.ToStateId);
                            }
                        }
                    }
                }
            }          
        }
        private bool Save(int status, ref string errorMessage)
        {
            try
            {
              
             
                CreateSaveObject(status);
                bool isSave=m_CurrentGRN.Save(ref errorMessage);
                return isSave;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private bool Update(int status,ref string ErrorMessage)
        {
            try
            {              
                
                CreateSaveObject(status);
                bool isSave = m_CurrentGRN.Save(ref ErrorMessage);
                return isSave;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Cancel()
        {
            try
            {
                string errorMessage = string.Empty;
                m_CurrentGRN.ModifiedBy = m_UserID;
                m_CurrentGRN.Status=(int)Common.GRNStatus.Cancelled;
                bool isSave = m_CurrentGRN.Save(ref errorMessage);
                if (isSave)
                {
                    MessageBox.Show(Common.GetMessage("INF0055", "GRN", "Cancelled"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    m_GRNNO = m_CurrentGRN.GRNNo;
                    ResetCreateForm();
                }
                else  
                if(!errorMessage.Trim().Equals(string.Empty))
                {
                    if (errorMessage.Trim().IndexOf("30001") >= 0)
                    {
                        MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Common.LogException(new Exception(errorMessage));
                    }
                    else
                    {
                        MessageBox.Show(Common.GetMessage(errorMessage.Trim()), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CreateReset()
        {
            try
            {
                errorCreate.Clear();
                m_GRNNO = string.Empty;
                ResetCreateForm();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CloseGRN()
        {
            try
            {
                bool isSave = false;
                string errorMessage = string.Empty;
                // set tax details
               
               
                if (m_Status == (int)Common.GRNStatus.New)
                {
                    isSave = Save((int)Common.GRNStatus.Closed, ref errorMessage);
                }
                else
                {
                    isSave = Update((int)Common.GRNStatus.Closed, ref errorMessage);
                }
               if (isSave)
               {
                   if (m_Status ==(int) Common.GRNStatus.New)
                   {
                       MessageBox.Show(Common.GetMessage("8006", "GRN", m_CurrentGRN.GRNNo), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                   }
                   else
                   {
                       MessageBox.Show(Common.GetMessage("INF0055", "GRN", "Closed"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                   }
                   m_GRNNO = m_CurrentGRN.GRNNo;
                   ResetCreateForm();
               }
               else
                   if (!errorMessage.Trim().Equals(string.Empty))
                   {
                       if (errorMessage.Trim().IndexOf("30001") >= 0)
                       {
                           MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                           Common.LogException(new Exception(errorMessage));
                       }
                       else
                       {
                           MessageBox.Show(Common.GetMessage(errorMessage.Trim()), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                       }
                   }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }       
        #endregion

        #region Data Methods        
        private PurchaseOrder GetPODetail(string PoNO)
        {
            PurchaseOrder po = new PurchaseOrder();
            po.PONumber = PoNO;
            po.Status = 1;
            List<PurchaseOrder> list = po.SearchPOForGRN();
            if (list != null && list.Count > 0)
            {
                return list[0];
            }           
            return null;            
        }
        /// <summary>
        /// GET GRN OBJECT FOR NEW GRN STATE
        /// </summary>
        /// <returns></returns>
        private Grn CreateGRNObject()
        {
            //IF GRN NO Blank
            try
            {
                Grn _grn = new Grn();
                _grn.Status =(int) Common.GRNStatus.New;
                _grn.StatusName = Common.GRNStatus.New.ToString();
                return _grn;
            }
            catch(Exception ex)
            {
                throw ex;
            }            
        }
        /// <summary>
        /// GET GRN OBJECT WHEN PARTICULAR GRN IS SELECTED
        /// </summary>
        /// <param name="GRNNo"></param>
        /// <returns></returns>
        private Grn CreateGRNObject(string GRNNo)
        {
            Grn grn = new Grn();
            grn.GRNNo = GRNNo;
            List<Grn> listGrn=grn.Search();
            //GRN NO Not Blank// Show details of existing GRN
            if (listGrn != null)
            {
                GrnDetail detail = new GrnDetail();
                detail.GRNNo = GRNNo;
                List<GrnDetail> _details=detail.Search();
                listGrn[0].GRNDetailList = _details;
                return listGrn[0];
            }
            else
                return null;
        }
        /// <summary>
        /// GET GRN OBJECT WHEN A NEW PO NO IS SELECTED
        /// </summary>
        /// <param name="PO"></param>
        /// <returns></returns>
        private Grn CreateGRNObject(PurchaseOrder PO)
        {
            //IF PO NOT  Blank
            try
            {
                Grn _grn = new Grn();
                if (PO != null)
                {
                    _grn.Status = (int)Common.GRNStatus.New;
                    _grn.StatusName = Common.GRNStatus.New.ToString();
                    _grn.AmendmentNo = PO.AmendmentNo;
                    _grn.DestinationLocation = PO.DeliveryAddress.GetAddress().ToString();
                    _grn.DestLocationID = PO.DestinationLocationID;
                    _grn.PODate = PO.PODate.ToString();
                    _grn.PONumber = PO.PONumber;
                    _grn.ShippingDetails = PO.ShippingDetails;
                    _grn.VendorCode = PO.VendorCode;
                    _grn.VendorName = PO.VendorName;
                    _grn.VendorID = PO.VendorID;
                    _grn.ToStateId = PO.DestinationLocationID;
                    _grn.FromStateId = PO.VendorAddress.StateId;
                }
                return _grn;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }     
     
        #endregion

        #region Events

        #region Button Events
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string ErrorMessage = string.Empty;
                if (ValidateSearch(ref ErrorMessage))
                    Search();
                else
                    MessageBox.Show(ErrorMessage, Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch(Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex); 
            }
        }

        private void btnSearchReset_Click(object sender, EventArgs e)
        {
            try
            {
                ResetSearch();
            }
              catch(Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex); 
            }
        }      

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string ErrorMessage = string.Empty;
                if (ValidateSave(ref ErrorMessage))
                {
                    DialogResult saveResult = MessageBox.Show(Common.GetMessage("5010", "Save"), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (saveResult == DialogResult.Yes)
                    {
                        bool isSave = false; bool isUpdate = false;
                        ErrorMessage = string.Empty;
                        if (btnSave.Text == "&Save")
                        {
                            isSave = Save((int)Common.GRNStatus.Created, ref ErrorMessage);
                        }
                        else
                        {
                            isUpdate = Update((int)Common.GRNStatus.Created, ref ErrorMessage);
                        }
                        if (isSave || isUpdate)
                        {
                            if (isSave)
                                MessageBox.Show(Common.GetMessage("8003", "GRN No", m_CurrentGRN.GRNNo), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            else if (isUpdate)
                                MessageBox.Show(Common.GetMessage("8001"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            m_GRNNO = m_CurrentGRN.GRNNo;
                            ResetCreateForm();
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
                else
                {
                    MessageBox.Show(ErrorMessage, Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult saveResult = MessageBox.Show(Common.GetMessage("5010", "Cancel"), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (saveResult == DialogResult.Yes)
                {
                    Cancel();
                    Search();
                }               
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }     

        /// <summary>
        /// 
        /// Make Status to Closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClosed_Click(object sender, EventArgs e)
        {
            try
            {
                string ErrorMessage = string.Empty;
                if (ValidateSave(ref ErrorMessage))
                {
                    DialogResult saveResult = MessageBox.Show(Common.GetMessage("5010", "Close"), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (saveResult == DialogResult.Yes)
                    {
                        CloseGRN();
                        Search();
                    }
                }
                else
                {
                    MessageBox.Show(ErrorMessage, Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        /// <summary>
        /// Reset To new status
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Reset_Click(object sender, EventArgs e)
        {
            try
            {                
                CreateReset();
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
                if (m_CurrentGRN != null && m_CurrentGRN.Status >= (int)Common.GRNStatus.Created)
                {
                    btnPrint.Enabled = false;
                    PrintReport();
                    btnPrint.Enabled = true;
                }
                else
                    MessageBox.Show(Common.GetMessage("INF0101", "GRN", Common.GRNStatus.Created.ToString()), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                btnPrint.Enabled = true;
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }      

        #endregion

        #region Gridview Events
        
        #region Search Gridview      
        /// <summary>
        /// ON double click of Mouse show GRN details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvSearchGRN_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if(dgvSearchGRN.SelectedRows.Count>0)
                {
                    m_GRNNO = Convert.ToString(dgvSearchGRN.SelectedRows[0].Cells[CON_GRID_GRNNO].Value);
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
        /// <summary>
        /// ON ENTER show GRN details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void dgvSearchGRN_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyValue == 13)
        //    {
        //        if (dgvSearchGRN.SelectedRows.Count > 0)
        //        {
        //            m_GRNNO = Convert.ToString(dgvSearchGRN.SelectedRows[0].Cells[CON_GRID_GRNNO].Value);
        //            ResetCreateForm();
        //            tabControlTransaction.SelectTab("tabCreate");
        //        }
        //    }
        //}
        /// <summary>
        /// ON VIEW BUTTON CLICK show GRN details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvSearchGRN_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
                {
                    if (dgvSearchGRN.Columns[e.ColumnIndex].CellType == typeof(DataGridViewImageCell))
                    {
                        if (Convert.ToString(dgvSearchGRN.Rows[e.RowIndex].Cells[CON_GRID_GRNNO].Value) != "")
                        {
                            m_GRNNO = Convert.ToString(dgvSearchGRN.Rows[e.RowIndex].Cells[CON_GRID_GRNNO].Value);
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
       
        #endregion

        #region CreateTab GridView
        private void dgvGRNItems_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
                {
                    if (dgvGRNItems.Columns[e.ColumnIndex].CellType == typeof(DataGridViewImageCell) && dgvGRNItems.Columns[e.ColumnIndex].Name == "AddBatch")
                    {
                        bool iseditable = EnableBatchEdit;
                        m_CurrentGRNDetail = m_CurrentGRN.GRNDetailList[e.RowIndex];
                        frmGRNBatch _batch = new frmGRNBatch(m_CurrentGRNDetail,dgvGRNItems,iseditable);
                        _batch.ShowDialog();
                        m_CurrentGRN.GrossWeight = m_CurrentGRN.GetGrossWeight().ToString();
                        txtGrossWeight.Text = m_CurrentGRN.GrossWeight;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        private void dgvGRNItems_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvGRNItems.SelectedRows.Count > 0)
                {
                    m_CurrentGRNDetail=m_CurrentGRN.GRNDetailList[dgvGRNItems.SelectedRows[0].Index];
                    
                }
                else
                {
                    m_CurrentGRNDetail = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        /// <summary>
        /// END EDITING WHEN LEAVE GRIDVIEW
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvGRNItems_Leave(object sender, EventArgs e)
        {
            try
            {
                dgvGRNItems.EndEdit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        private void dgvGRNItems_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
                {

                    if ((dgvGRNItems.Columns[e.ColumnIndex].Name == CON_COL_CHALLANQTY) || (dgvGRNItems.Columns[e.ColumnIndex].Name == CON_COL_INVOICEQTY))
                    {
                        string val = dgvGRNItems.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                        if (!Validators.IsNumeric(dgvGRNItems.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()))
                        {
                            errorCreate.SetError(dgvGRNItems, Common.GetMessage("INF0010", dgvGRNItems.Columns[e.ColumnIndex].Name));
                            MessageBox.Show(Common.GetMessage("INF0010", dgvGRNItems.Columns[e.ColumnIndex].Name), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            //  dgvGRNItems.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = Common.GetMessage("VAL0033", dgvGRNItems.Columns[e.ColumnIndex].Name);

                        }
                    }
                    //if(isvalid)
                    //{
                    //    if (dgvGRNItems.Columns[e.ColumnIndex].Name == CON_COL_CHALLANQTY)
                    //    {
                    //        if (Convert.ToDouble(dgvGRNItems.Rows[e.RowIndex].Cells[e.ColumnIndex].Value) > Convert.ToDouble(dgvGRNItems.Rows[e.RowIndex].Cells[CON_COL_POQTY].Value))
                    //        {
                    //            errorCreate.SetError(dgvGRNItems, Common.GetMessage("INF0034", "Challan Qty", "PO Qty"));
                    //            MessageBox.Show(Common.GetMessage("INF0034", "Challan Qty", "PO Qty"));                            
                    //        }
                    //    }
                    //}                
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }
        private void dgvGRNItems_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            try
            {
                if (e.Exception.GetType().Equals(typeof(System.FormatException)))
                {
                    MessageBox.Show(dgvGRNItems, Common.GetMessage("INF0010", "Qty"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Cancel = false;
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

        #region TextBox
        /// <summary>
        /// On Po NUmber VAlue change, Get New PO Detail
        /// Set Textbox values
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPONumber_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                //Same PO NO 
                if (m_PONo.Equals(txtPONumber.Text.Trim()))
                    return;
                //PO No changed
                PurchaseOrder PO = GetPODetail(txtPONumber.Text.Trim());
                ResetPOValues(PO);
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        private void txtPONumber_KeyDown(object sender, KeyEventArgs e)
        {
            if ( e.KeyValue == Common.F4KEY && !e.Alt)
            {
                try
                {
                    NameValueCollection _collection = new NameValueCollection();

                    _collection.Add("DestinationLocationID", Common.CurrentLocationId.ToString());                    
                    CoreComponent.Controls.frmSearch _frmSearch = new CoreComponent.Controls.frmSearch(CoreComponent.Controls.SearchTypes.POGRN, _collection);
                  //  _frmSearch.MdiParent = this.MdiParent;
                    _frmSearch.ShowDialog();
                   
                    if (_frmSearch.DialogResult == DialogResult.OK)
                    {
                        PurchaseOrder PO = (PurchaseOrder)_frmSearch.ReturnObject;
                      
                        if (PO != null)
                        {
                            if (m_PONo.Equals(PO.PONumber))                            
                                return;
                            txtPONumber.Text = PO.PONumber;
                        }                      
                        ResetPOValues(PO);
                    }             
                }
                catch (Exception ex)
                {
                    MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Common.LogException(ex);
                }
               
            }
        }      

        private void txt_KeyDown(object sender, KeyEventArgs e)
        {

        }       
        #endregion

        #endregion

        #region Validation

        /// <summary>
        /// Search Validation
        /// 1. From PO Date Should Not Be Greater Than To Date
        /// 2. From GRN Date Should Not Be Greater Than To Date
        /// 3. From Challan Date Should Not Be Greater Than To Date
        /// 4. From Invoice Date Should Not Be Greater Than To Date
        /// 5. Length of GRn No, PO No, Vendor Name, Received By, Challan No, Invoice No.
        /// </summary>
        /// <returns></returns>
        private bool ValidateSearch(ref string Error)
        {
            try
            {
               
                errorSearch.SetError(dtpSearchFrmGRNDate, string.Empty);
               
                errorSearch.SetError(dtpSearchfrmPODate, string.Empty);
              
                if (dtpSearchFrmGRNDate.Checked && dtpSearchToGRNDate.Checked)
                {
                    if (!ValidateDates(dtpSearchFrmGRNDate.Value, dtpSearchToGRNDate.Value))
                    {
                        errorSearch.SetError(dtpSearchFrmGRNDate, Common.GetMessage("INF0034", "From GRN Date", "To GRN Date"));                   
                    }
                }
              
                if (dtpSearchfrmPODate.Checked && dtpSearchToPODate.Checked)
                {
                    if (!ValidateDates(dtpSearchfrmPODate.Value, dtpSearchToPODate.Value))
                    {
                        errorSearch.SetError(dtpSearchfrmPODate, Common.GetMessage("INF0034", "From PO Date", "To PO Date"));                   
                    }
                }
                StringBuilder sbError = new StringBuilder();
                sbError=GenerateSearchError();
                if (!sbError.ToString().Trim().Equals(string.Empty))
                {
                    Error = sbError.ToString();                    
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            } 
        }
        /// <summary>
        /// Validations For Save Button
        /// 1. PONo should not be blank
        /// 2. Challno Should not be blank
        /// 3. Challan Date should not be greater than today
        /// 4. GRNLIst not blank
        /// 5. atleast one recieve qty should not be 0.
        /// 
        /// </summary>
        /// <returns></returns>
        private bool ValidateSave(ref string ErrorMessage)
        {
            try
            {
                errorCreate.Clear();
                bool IsValid = true;               
               if (Validators.CheckForEmptyString(txtPONumber.Text.Trim().Length))
               {
                    errorCreate.SetError(txtPONumber, Common.GetMessage("INF0001", lblPONumber.Text.Trim().Substring(0, lblPONumber.Text.Trim().Length - 2)));
                    txtPONumber.Focus();
                    IsValid = false;                    
               }
               if (Validators.CheckForEmptyString(txtChallanNumber.Text.Trim().Length))
               {
                   errorCreate.SetError(txtChallanNumber, Common.GetMessage("INF0001", lblChallanNumber.Text.Trim().Substring(0, lblChallanNumber.Text.Trim().Length - 2)));
                   IsValid = false;                   
               }
               if (!ValidateDates(Convert.ToDateTime(dtpChallanDate.Value.ToShortDateString()),Convert.ToDateTime(DateTime.Today.ToShortDateString())))
               {
                   errorCreate.SetError(dtpChallanDate, Common.GetMessage("INF0034", "Challan Date", "Today Date"));                   
                   IsValid = false;
               }
               if (Validators.IsGreaterThanZero(txtInvoiceAmount.Text.Trim()) && Validators.CheckForEmptyString(txtInvoiceNumber.Text.Trim().Length))
               {
                   errorCreate.SetError(txtInvoiceNumber, Common.GetMessage("INF0001", lblInvoiceNumber.Text.Trim().Substring(0, lblInvoiceNumber.Text.Trim().Length - 1)));
                   IsValid = false;
               }
               if (!Validators.IsGreaterThanZero(txtInvoiceAmount.Text.Trim()) && !Validators.CheckForEmptyString(txtInvoiceNumber.Text.Trim().Length))
               {
                   errorCreate.SetError(txtInvoiceAmount, Common.GetMessage("INF0001", lblInvoiceAmount.Text.Trim().Substring(0, lblInvoiceAmount.Text.Trim().Length - 1)));
                   IsValid = false;
               }
               if (!Validators.IsNumeric(txtNoOfBoxes.Text.Trim()))
               {
                   errorCreate.SetError(txtNoOfBoxes, Common.GetMessage("INF0010", lblNoOfBoxes.Text.Trim().Substring(0, lblNoOfBoxes.Text.Trim().Length - 1)));
                   IsValid = false;
               }
               if (!Validators.IsValidAmount(txtInvoiceTax.Text.Trim()))
               {
                   errorCreate.SetError(txtInvoiceTax, Common.GetMessage("INF0010", lblInvoiceTax.Text.Trim().Substring(0, lblInvoiceTax.Text.Trim().Length - 1)));
                   IsValid = false;
               }
               if (!Validators.IsValidAmount(txtInvoiceAmount.Text.Trim()))
               {
                   errorCreate.SetError(txtInvoiceAmount, Common.GetMessage("INF0010", lblInvoiceAmount.Text.Trim().Substring(0, lblInvoiceAmount.Text.Trim().Length - 1)));
                   IsValid = false;
               }
               bool IsValidReceivedQty = false;
               bool IsValidChallanQty = true;
               bool IsvalidInvoiceQty = true;
               if (m_CurrentGRN.GRNDetailList != null)
               {
                   foreach (GrnDetail grn in m_CurrentGRN.GRNDetailList)
                   {
                       if (grn.ReceivedQty > 0)
                           IsValidReceivedQty = true;
                       if (grn.ChallanQty < 0)
                           IsValidChallanQty = false;
                       if (grn.InvoiceQty < 0)
                           IsvalidInvoiceQty = false;
                   }
               }
               if (!IsValidReceivedQty)
               {                  
                   errorCreate.SetError(dgvGRNItems, Common.GetMessage("INF0072"));                   
                   IsValid = false;
               }
               if (!IsValidChallanQty)
               {
                   errorCreate.SetError(dgvGRNItems, Common.GetMessage("INF0010","ChallanQty"));
                   IsValid = false;
               }
               if (!IsvalidInvoiceQty)
               {
                   errorCreate.SetError(dgvGRNItems, Common.GetMessage("INF0010","InvoiceQty"));
                   IsValid = false;
               }
               if (!IsValid)
               {
                   StringBuilder sbError = new StringBuilder();
                   sbError = GenerateCreateError();
                   if (!sbError.ToString().Trim().Equals(string.Empty))
                   {
                       ErrorMessage = sbError.ToString();
                   }
               }
               return IsValid;               
            }
            catch (Exception ex)
            {
                throw ex;
            } 
        }
     
        

        private bool ValidateDates(DateTime FromDate,DateTime ToDate)
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

        /// <summary>
        /// Creates DataSet for Printing TO Screen report
        /// </summary>
        private void CreatePrintDataSet()
        {
            m_printDataSet = new DataSet();
            // Search GRN Data 
            DataTable dtGRNHeader = new DataTable("GRNHeader");
            Grn grnObj = new Grn();
            grnObj.GRNNo = m_GRNNO;
            dtGRNHeader = grnObj.SearchGRNDataTable();
            dtGRNHeader.Columns.Add(new DataColumn("GRNDateText", System.Type.GetType("System.String")));
            dtGRNHeader.Columns.Add(new DataColumn("PODateText", System.Type.GetType("System.String")));
            dtGRNHeader.Columns.Add(new DataColumn("ChallanDateText", System.Type.GetType("System.String")));
            dtGRNHeader.Columns.Add(new DataColumn("InvoiceDateText", System.Type.GetType("System.String")));
            for (int i = 0; i < dtGRNHeader.Rows.Count; i++)
            {
                dtGRNHeader.Rows[i]["GRNDateText"] = Convert.ToDateTime(dtGRNHeader.Rows[i]["GRNDate"]).ToString(Common.DTP_DATE_FORMAT);
                dtGRNHeader.Rows[i]["PODateText"] = Convert.ToDateTime(dtGRNHeader.Rows[i]["PODate"]).ToString(Common.DTP_DATE_FORMAT);
                dtGRNHeader.Rows[i]["ChallanDateText"] = Convert.ToDateTime(dtGRNHeader.Rows[i]["ChallanDate"]).ToString(Common.DTP_DATE_FORMAT);
                dtGRNHeader.Rows[i]["InvoiceDateText"] = Convert.ToDateTime(dtGRNHeader.Rows[i]["InvoiceDate"]).ToString(Common.DTP_DATE_FORMAT);
                dtGRNHeader.Rows[i]["InvoiceAmount"] = Math.Round(Convert.ToDecimal(dtGRNHeader.Rows[i]["InvoiceAmount"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                dtGRNHeader.Rows[i]["InvoiceTaxAmount"] = Math.Round(Convert.ToDecimal(dtGRNHeader.Rows[i]["InvoiceTaxAmount"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
            }
            m_printDataSet.Tables.Add(dtGRNHeader.Copy());
            m_printDataSet.Tables[0].TableName = "GRNHeader";

            // Search GRN Detail
            DataTable dtGRNDetail = new DataTable("GRNDetail");
            GrnDetail grnDetailObj = new GrnDetail();
            grnDetailObj.GRNNo = m_GRNNO;
            dtGRNDetail = grnDetailObj.GRNDetailSearchDataTable();
            for (int i = 0; i < dtGRNDetail.Rows.Count; i++)
            {
                dtGRNDetail.Rows[i]["POQty"] = Math.Round(Convert.ToDecimal(dtGRNDetail.Rows[i]["POQty"]), Common.DisplayQtyRounding, MidpointRounding.AwayFromZero);
                dtGRNDetail.Rows[i]["ChallanQty"] = Math.Round(Convert.ToDecimal(dtGRNDetail.Rows[i]["ChallanQty"]), Common.DisplayQtyRounding, MidpointRounding.AwayFromZero);
                dtGRNDetail.Rows[i]["InvoiceQty"] = Math.Round(Convert.ToDecimal(dtGRNDetail.Rows[i]["InvoiceQty"]), Common.DisplayQtyRounding, MidpointRounding.AwayFromZero);
                dtGRNDetail.Rows[i]["ReceivedQty"] = Math.Round(Convert.ToDecimal(dtGRNDetail.Rows[i]["ReceivedQty"]), Common.DisplayQtyRounding, MidpointRounding.AwayFromZero);
            }       

            DataTable dtGRNBatchDetail = new DataTable("GRNBatchDetail");
            GrnBatchDetail objGrnBatch = new GrnBatchDetail();
            objGrnBatch.GRNNo = m_GRNNO;
            objGrnBatch.ItemId = Common.INT_DBNULL;
            dtGRNBatchDetail = objGrnBatch.GRNBatchDetailSearchDataTable();
            for (int i = 0; i < dtGRNBatchDetail.Rows.Count; i++)
            {
                dtGRNBatchDetail.Rows[i]["ReceivedQty"] = Math.Round(Convert.ToDecimal(dtGRNBatchDetail.Rows[i]["ReceivedQty"]), Common.DisplayQtyRounding, MidpointRounding.AwayFromZero);
                dtGRNBatchDetail.Rows[i]["ManufacturingDate"] = (Convert.ToDateTime(dtGRNBatchDetail.Rows[i]["ManufacturingDate"])).ToString(Common.DTP_DATE_FORMAT);
                dtGRNBatchDetail.Rows[i]["ExpiryDate"] = (Convert.ToDateTime(dtGRNBatchDetail.Rows[i]["ExpiryDate"])).ToString(Common.DTP_DATE_FORMAT);
            }
            DataTable dtGrnFullDetail = new DataTable();
            dtGrnFullDetail.Columns.Add(new DataColumn("ItemCode", Type.GetType("System.String")));
            dtGrnFullDetail.Columns.Add(new DataColumn("ItemDescription", Type.GetType("System.String")));
            dtGrnFullDetail.Columns.Add(new DataColumn("POQty", Type.GetType("System.String")));
            dtGrnFullDetail.Columns.Add(new DataColumn("ChallanQty", Type.GetType("System.String")));
            dtGrnFullDetail.Columns.Add(new DataColumn("ReceivedQty", Type.GetType("System.String")));
            dtGrnFullDetail.Columns.Add(new DataColumn("InvoiceQty", Type.GetType("System.String")));
            dtGrnFullDetail.Columns.Add(new DataColumn("ManufacturerBatchNo", Type.GetType("System.String")));
            dtGrnFullDetail.Columns.Add(new DataColumn("BatchQty", Type.GetType("System.String")));
            dtGrnFullDetail.Columns.Add(new DataColumn("MfgDate", Type.GetType("System.String")));
            dtGrnFullDetail.Columns.Add(new DataColumn("ExpDate", Type.GetType("System.String")));
            bool firstEntryDone = false;
            for (int i = 0; i < dtGRNDetail.Rows.Count; i++)            
            {
                for (int j = 0; j < dtGRNBatchDetail.Rows.Count; j++)
                {
                    if (Convert.ToInt32(dtGRNDetail.Rows[i]["ItemId"]) == Convert.ToInt32(dtGRNBatchDetail.Rows[j]["ItemId"]))
                    {
                        DataRow dRow = dtGrnFullDetail.NewRow();
                        dRow["ItemCode"] = dtGRNDetail.Rows[i]["ItemCode"].ToString();
                        dRow["ItemDescription"] = dtGRNDetail.Rows[i]["ItemDescription"].ToString();
                        if(!firstEntryDone)
                        {
                            dRow["POQty"] = dtGRNDetail.Rows[i]["POQty"].ToString();
                            dRow["ChallanQty"] = dtGRNDetail.Rows[i]["ChallanQty"].ToString();
                            dRow["ReceivedQty"] = dtGRNDetail.Rows[i]["ReceivedQty"].ToString();
                            dRow["InvoiceQty"] = dtGRNDetail.Rows[i]["InvoiceQty"].ToString();
                        }
                        dRow["ManufacturerBatchNo"] = dtGRNBatchDetail.Rows[j]["ManufacturerBatchNo"].ToString();
                        dRow["BatchQty"] = dtGRNBatchDetail.Rows[j]["ReceivedQty"].ToString();
                        dRow["MfgDate"] = dtGRNBatchDetail.Rows[j]["ManufacturingDate"].ToString();
                        dRow["ExpDate"] = dtGRNBatchDetail.Rows[j]["ExpiryDate"].ToString();
                        dtGrnFullDetail.Rows.Add(dRow);
                        firstEntryDone = true;
                    }
                }
                firstEntryDone = false;
            }
            m_printDataSet.Tables.Add(dtGrnFullDetail.Copy());
            m_printDataSet.Tables[1].TableName = "GRNFullDetail";
        }
        /// <summary>
        /// Prints TO Screen report
        /// </summary>
        private void PrintReport()
        {
            CreatePrintDataSet();
            CoreComponent.UI.ReportScreen reportScreenObj = new CoreComponent.UI.ReportScreen((int)Common.ReportType.GRN, m_printDataSet);
            reportScreenObj.ShowDialog();
            //reportScreenObj.PrintReport();
            m_printDataSet = null;
        }

        private StringBuilder GenerateSearchError()
        {
            bool focus = false;
            StringBuilder sbError = new StringBuilder();
            
            if (errorSearch.GetError(dtpSearchFrmGRNDate).Trim().Length > 0)
            {
                sbError.Append(errorSearch.GetError(dtpSearchFrmGRNDate));
                sbError.AppendLine();
                if (!focus)
                {
                    dtpSearchFrmGRNDate.Focus();
                    focus = true;
                }
            }
            
            if (errorSearch.GetError(dtpSearchfrmPODate).Trim().Length > 0)
            {
                sbError.Append(errorSearch.GetError(dtpSearchfrmPODate));
                sbError.AppendLine();
                if (!focus)
                {
                    dtpSearchfrmPODate.Focus();
                    focus = true;
                }
            }
            sbError = Common.ReturnErrorMessage(sbError);
            return sbError;
        }

        private StringBuilder GenerateCreateError()
        {
            bool focus = false;
            StringBuilder sbError = new StringBuilder();

            if (errorCreate.GetError(txtPONumber).Trim().Length > 0)
            {
                sbError.Append(errorCreate.GetError(txtPONumber));
                sbError.AppendLine();
                if (!focus)
                {
                    txtPONumber.Focus();
                    focus = true;
                }
            }
            if (errorCreate.GetError(txtChallanNumber).Trim().Length > 0)
            {
                sbError.Append(errorCreate.GetError(txtChallanNumber));
                sbError.AppendLine();
                if (!focus)
                {
                    txtChallanNumber.Focus();
                    focus = true;
                }
            }
            if (errorCreate.GetError(dtpChallanDate).Trim().Length > 0)
            {
                sbError.Append(errorCreate.GetError(dtpChallanDate));
                sbError.AppendLine();
                if (!focus)
                {
                    dtpChallanDate.Focus();
                    focus = true;
                }
            }
            if (errorCreate.GetError(txtInvoiceNumber).Trim().Length > 0)
            {
                sbError.Append(errorCreate.GetError(txtInvoiceNumber));
                sbError.AppendLine();
                if (!focus)
                {
                    txtInvoiceNumber.Focus();
                    focus = true;
                }
            }
            if (errorCreate.GetError(txtInvoiceTax).Trim().Length > 0)
            {
                sbError.Append(errorCreate.GetError(txtInvoiceTax));
                sbError.AppendLine();
                if (!focus)
                {
                    txtInvoiceTax.Focus();
                    focus = true;
                }
            }
            if (errorCreate.GetError(txtInvoiceAmount).Trim().Length > 0)
            {
                sbError.Append(errorCreate.GetError(txtInvoiceAmount));
                sbError.AppendLine();
                if (!focus)
                {
                    txtInvoiceAmount.Focus();
                    focus = true;
                }
            }
            if (errorCreate.GetError(txtNoOfBoxes).Trim().Length > 0)
            {
                sbError.Append(errorCreate.GetError(txtNoOfBoxes));
                sbError.AppendLine();
                if (!focus)
                {
                    txtNoOfBoxes.Focus();
                    focus = true;
                }
            }
            if (errorCreate.GetError(dgvGRNItems).Trim().Length > 0)
            {
                sbError.Append(errorCreate.GetError(dgvGRNItems));
                sbError.AppendLine();
                if (!focus)
                {
                    dgvGRNItems.Focus();
                    focus = true;
                }
            }
            sbError = Common.ReturnErrorMessage(sbError);
            return sbError;
        }
     
        private bool ValidateQty(TextBox txt,Label lbl,ref bool setFocus)
        {
            if (!Validators.IsGreaterThanZero(txt.Text.Trim()))
            {
                errorCreate.SetError(txt, Common.GetMessage("VAL0033", lbl.Text.Trim().Substring(0, lbl.Text.Trim().Length - 2)));
                if (setFocus)
                {
                    txt.Focus();
                    setFocus = false;
                    return false;
                }
            }
            return true;
        }
        #endregion                 
      
        #region Control Property
        private bool EnableSave
        {
            get
            {
                if ((m_LocationType == Common.LocationConfigId.WH || m_LocationType == Common.LocationConfigId.BO) &&(m_Status == (int)Common.GRNStatus.New || m_Status == (int)Common.GRNStatus.Created) && (m_CurrentGRN != null && m_CurrentGRN.GRNDetailList != null && m_CurrentGRN.GRNDetailList.Count > 0))
                    return true;
                else
                    return false;
            }
        }
        private bool EnableUpdate
        {
            get
            {
                if ((EnableSave && (m_LocationType == Common.LocationConfigId.WH || m_LocationType == Common.LocationConfigId.BO)) 
                    || (m_LocationType == Common.LocationConfigId.HO && m_Status == (int)Common.GRNStatus.Closed))
                    return true;
                else
                    return false;
            }
        }
        private bool EnableClose
        {
            get
            {
                if (m_Status == (int)Common.GRNStatus.Closed || m_Status == (int)Common.GRNStatus.Cancelled)
                    return false;
                else if (((m_Status == (int)Common.GRNStatus.New) || (m_Status == (int)Common.GRNStatus.Created)) && (m_CurrentGRN != null && m_CurrentGRN.GRNDetailList != null && m_CurrentGRN.GRNDetailList.Count > 0))
                    return true;
                else
                    return false;
            }
        }
        private bool EnableCancel
        {
            get
            {
                if ((m_Status == (int)Common.GRNStatus.Created) && (m_CurrentGRN != null && m_CurrentGRN.GRNDetailList != null && m_CurrentGRN.GRNDetailList.Count > 0))
                    return true;
                else
                    return false;
            }
        }
        private bool EnableReset
        {
            get
            {
                if (m_LocationType == Common.LocationConfigId.WH || m_LocationType == Common.LocationConfigId.BO)
                    return true;
                else
                    return false;
            }
        }
        private bool EnableBatchEdit
        {
            get
            {
                if (EnableSave)
                    return true;
                else
                    return false;
            }
        }
        private bool EnablePOTextBox
        {
            get
            {
                if (EnableWHTextBox && ((m_CurrentGRN == null) || (m_CurrentGRN != null && m_CurrentGRN.GRNNo.Trim().Equals(string.Empty))))
                    return true;
                else
                    return false;
            }
        }
        private bool EnableWHTextBox
        {
            get
            {
                if ((m_LocationType == Common.LocationConfigId.WH || m_LocationType == Common.LocationConfigId.BO) && ((m_Status == (int)Common.GRNStatus.New) || (m_Status == (int)Common.GRNStatus.Created)))
                    return true;
                else
                    return false;
            }
        }
        private bool EnableHOTextBox
        {
            // ADD HO rights on closed state
            get
            {
                if ((m_Status == (int)Common.GRNStatus.Closed && m_LocationType==Common.LocationConfigId.HO) || ((m_Status == (int)Common.GRNStatus.New || m_Status == (int)Common.GRNStatus.Created)&&  (m_LocationType==Common.LocationConfigId.WH
                    || m_LocationType == Common.LocationConfigId.BO)))
                    return true;
                else
                    return false;
            }
        }
        #endregion      

        private void btnCreateReset_Click(object sender, EventArgs e)
        {
            CreateReset();
        }
    }
}
