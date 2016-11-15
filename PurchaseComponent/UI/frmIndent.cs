using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CoreComponent.BusinessObjects;
using CoreComponent.Core.BusinessObjects;
using CoreComponent.MasterData.BusinessObjects;
using CoreComponent.MasterData;
using PurchaseComponent.BusinessObjects;
using System.Collections.Specialized;

namespace PurchaseComponent.UI
{
    public partial class frmIndent : CoreComponent.Core.UI.Transaction
    {
        #region Variable declaration


        #region Grid view Fields

        private const string CON_GRID_ISCONSOLIDATE = "IsConsolidate";
        private const string CON_GRID_INEDENTNO = "IndentNo";
        private const string CON_GRID_ITEMCODE = "ItemCode";
        private const string CON_GRID_ITEMID = "ItemId";
        private const string CON_GRID_ITEMNAME = "ItemName";
        private const string CON_GRID_SUGGESTEDQTY = "SuggestedQty";
        private const string CON_GRID_REQUESTEDQTY = "RequestedQty";
        private const string CON_GRID_APPROVEDHOQTY = "ApprovedHOQty";
        private const string CON_GRID_TOFROM = "TOFrom";
        private const string CON_GRID_TOFROMSTOK = "TOFromStock";
        private const string CON_GRID_APPROVEDTOQTY = "ApprovedTOQty";
        private const string CON_GRID_APPROVEDPOQTY = "ApprovedPOQty";
        private const string CON_GRID_VENDOR = "Vendor";
        private const string CON_GRID_DELLOCATION = "DelLocation";
        private const string CON_GRID_ISFORMC = "IsFormC";
        private const string CON_GRID_REMOVE = "Remove";
        private const string CON_GRID_VIEW = "View";

        #endregion

        #region Variables for Rights
        // Variables for Rights
        // private Boolean IsAddAvailable = false;
        private Boolean IsSaveAvailable = false;
        private Boolean IsUpdateAvailable = false;
        private Boolean IsCancelAvailable = false;
        private Boolean IsRejectAvailable = false;
        private Boolean IsApproveAvailable = false;
        private Boolean IsSearchAvailable = false;
        private Boolean IsConfirmAvailable = false;
        private Boolean IsPOTOAvailable = false;
        private Boolean IsViewAvailable = false;
        private Boolean IsRemoveAvailable = false;
        private Boolean IsPrintAvailable = false;
        //    private Boolean IsResetAvailable = false;

        #endregion
        //Combo Location Fields
        private const string CON_LOCATION_DISPLAYNAME = "DisplayName";
        private const string CON_LOCATION_ID = "LocationId";

        //Combo Status Parameter Type Fields
        private const string CON_SUGGESTED_INDENT_PARAM = "SINDENTSTATUS";
        private const string CON_MANUAL_INDENT_PARAM = "MINDENTSTATUS";

        private string GRIDVIEW_XML_PATH = Environment.CurrentDirectory + "\\App_Data\\GridViewDefinition.xml";
        List<Indent> m_indentListSearch = null;
        //Current Object 
        Indent m_CurrIndent;
        IndentDetail m_CurrIndentDetail;
        CurrencyManager m_bindingMgr;
        
        private DataSet m_printDataSet = null;

        private int UserID;
        private string UserName;
        private string CON_MODULENAME = string.Empty;
        public enum FormMode
        {
            Create = 1,
            Update = 2,
            View = 3
        }

        private string IndentNo = Common.DBNULL_VAL;
        private Common.IndentType IndentType;
        private FormMode IndentMode;
        private Common.LocationConfigId LocationType;
        private string LocationName = string.Empty;
        private List<ItemDetails> ItemList;
        private AutoCompleteStringCollection _itemcollection;
        private DataTable dtVendorLocation;
        #endregion

        #region Constructor & Properties

        private int m_indentScreenType;

        public frmIndent(params object[] arr)
        {
            // Initailize components
            InitializeComponent();
            // Get Inital values  
            IndentType = Common.IndentType.MANUAL;
            m_indentScreenType = Convert.ToInt32(arr[0]);
            LocationType = (Common.LocationConfigId)Common.CurrentLocationTypeId;
            if (AuthenticationComponent.BusinessObjects.Authenticate.LoggedInUser != null)
            {
                UserID = AuthenticationComponent.BusinessObjects.Authenticate.LoggedInUser.UserId;
                UserName = AuthenticationComponent.BusinessObjects.Authenticate.LoggedInUser.UserName;
            }
        }

        public frmIndent()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        #region Initailize Methods
        // Initailize Form Property
        private void InitailizeForm()
        {
            if (IndentType == Common.IndentType.MANUAL)
                lblPageTitle.Text = "Manual Indent";
            else
                lblPageTitle.Text = "Suggested Indent";
            lblLocationType.Text = ((Common.LocationConfigId)LocationType).ToString();
        }

        private void frmIndent_Load(object sender, EventArgs e)
        {
            try
            {
                if (m_indentScreenType == (int)Common.IndentType.MANUAL)
                {
                    if (this.Tag != null)
                    {
                        CON_MODULENAME = this.Tag.ToString();//"PUR01";
                    }
                    IndentType = Common.IndentType.MANUAL;
                }
                else
                {
                    if (this.Tag != null)
                    {
                        CON_MODULENAME = this.Tag.ToString();//"PUR02";
                    }
                    IndentType = Common.IndentType.SUGGESTED;
                }
                DataTable dtLocation = Common.ParameterLookup(Common.ParameterType.Locations, new ParameterFilter("", 0, Common.CurrentLocationId, 0));
                if (dtLocation != null && dtLocation.Rows.Count > 0)
                {
                    LocationName = Convert.ToString(dtLocation.Rows[0]["LocationName"]);
                }
                // Initailize rights
                InitializeRights();
                // set form property
                InitailizeForm();
                // initailize controls
                InitializeControl();
                // reset controls        
                ResetForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }
        //Initailize Rights
        private void InitializeRights()
        {
            try
            {
                if (UserName != null && !CON_MODULENAME.Equals(string.Empty))
                {
                    //  IsAddAvailable = AuthenticationComponent.BusinessObjects.Authenticate.IsFunctionAccessible(UserName, Common.LocationCode, CON_MODULENAME, Common.FUNCTIONCODE_ADD);
                    IsCancelAvailable = AuthenticationComponent.BusinessObjects.Authenticate.IsFunctionAccessible(UserName, Common.LocationCode, CON_MODULENAME, Common.FUNCTIONCODE_CANCEL);
                    IsConfirmAvailable = AuthenticationComponent.BusinessObjects.Authenticate.IsFunctionAccessible(UserName, Common.LocationCode, CON_MODULENAME, Common.FUNCTIONCODE_CONFIRM);
                    IsSaveAvailable = AuthenticationComponent.BusinessObjects.Authenticate.IsFunctionAccessible(UserName, Common.LocationCode, CON_MODULENAME, Common.FUNCTIONCODE_SAVE);
                    IsUpdateAvailable = AuthenticationComponent.BusinessObjects.Authenticate.IsFunctionAccessible(UserName, Common.LocationCode, CON_MODULENAME, Common.FUNCTIONCODE_UPDATE);
                    IsSearchAvailable = AuthenticationComponent.BusinessObjects.Authenticate.IsFunctionAccessible(UserName, Common.LocationCode, CON_MODULENAME, Common.FUNCTIONCODE_SEARCH);
                    IsViewAvailable = AuthenticationComponent.BusinessObjects.Authenticate.IsFunctionAccessible(UserName, Common.LocationCode, CON_MODULENAME, Common.FUNCTIONCODE_VIEW);
                    IsApproveAvailable = AuthenticationComponent.BusinessObjects.Authenticate.IsFunctionAccessible(UserName, Common.LocationCode, CON_MODULENAME, Common.FUNCTIONCODE_APPROVE);
                    IsPOTOAvailable = AuthenticationComponent.BusinessObjects.Authenticate.IsFunctionAccessible(UserName, Common.LocationCode, CON_MODULENAME, Common.FUNCTIONCODE_CONSOLIDATE);
                    IsRejectAvailable = AuthenticationComponent.BusinessObjects.Authenticate.IsFunctionAccessible(UserName, Common.LocationCode, CON_MODULENAME, Common.FUNCTIONCODE_REJECT);
                    IsRemoveAvailable = AuthenticationComponent.BusinessObjects.Authenticate.IsFunctionAccessible(UserName, Common.LocationCode, CON_MODULENAME, Common.FUNCTIONCODE_REMOVE);
                    IsPrintAvailable = AuthenticationComponent.BusinessObjects.Authenticate.IsFunctionAccessible(UserName, Common.LocationCode, CON_MODULENAME, Common.FUNCTIONCODE_PRINT);
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
                //Initailize Search Grid Property           
                dgvIndent.AutoGenerateColumns = false;
                dgvIndent.AllowUserToAddRows = false;
                dgvIndent.AllowUserToDeleteRows = false;
                dgvIndent.EditMode = DataGridViewEditMode.EditProgrammatically;
                dgvIndent.DataSource = null;
                DataGridView dgvSearchNew = Common.GetDataGridViewColumns(dgvIndent, GRIDVIEW_XML_PATH);

                //Initailize Create Grid View
                dgvIndentDetails.AutoGenerateColumns = false;
                dgvIndentDetails.AllowUserToAddRows = false;
                dgvIndentDetails.AllowUserToDeleteRows = false;
                DataGridView dgvCreateNew = Common.GetDataGridViewColumns(dgvIndentDetails, GRIDVIEW_XML_PATH);

                // Fill Status Combo box           
                try
                {
                    string parameterType = Common.DBNULL_VAL;
                    if (IndentType == Common.IndentType.SUGGESTED)
                        parameterType = CON_SUGGESTED_INDENT_PARAM;
                    else if (IndentType == Common.IndentType.MANUAL)
                        parameterType = CON_MANUAL_INDENT_PARAM;
                    DataTable dtStatus = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter(parameterType, 0, 0, 0));
                    if (dtStatus != null)
                    {
                        cmbStatus.DataSource = dtStatus;
                        cmbStatus.DisplayMember = Common.KEYVALUE1;
                        cmbStatus.ValueMember = Common.KEYCODE1;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                // Fill Location Combo box
                try
                {
                    DataTable dtLocations = Common.ParameterLookup(Common.ParameterType.Locations, new ParameterFilter("LOCATIONS", 0, 0, 0));
                    if (dtLocations != null)
                    {
                        cmbLocation.DataSource = dtLocations;
                        cmbLocation.DisplayMember = CON_LOCATION_DISPLAYNAME;
                        cmbLocation.ValueMember = CON_LOCATION_ID;
                        if (LocationType != Common.LocationConfigId.HO)
                        {
                            cmbLocation.SelectedValue = Common.CurrentLocationId;
                            cmbLocation.Enabled = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                // Initailize ItemList

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Settings Method
        public void ResetForm()
        {
            try
            {
                errCreateIndent.Clear();
                m_CurrIndent = null;
                IndentMode = FormMode.View;
                m_CurrIndentDetail = null;
                m_CurrIndent = GetIndent();
                if (m_CurrIndent != null)
                {
                    RefreshItemList();
                    SetFormMode();
                    SetHeaderValues();
                    SetDetailValues();
                    SetProperty();
                    ResetGrid();
                }
                else
                {
                    DisableAllButton();
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

        #region Set
        private void SetFormMode()
        {
            try
            {
                //Check IndentNo exists
                if (m_CurrIndent.Status == (Int32)Common.IndentStatus.New && LocationType != Common.LocationConfigId.HO && m_CurrIndent.IndentType == (int)Common.IndentType.MANUAL)
                {
                    IndentMode = FormMode.Create;
                }
                else if (m_CurrIndent.Status == (Int32)Common.IndentStatus.Approved || m_CurrIndent.Status == (Int32)Common.IndentStatus.Confirmed || m_CurrIndent.Status == (Int32)Common.IndentStatus.Created)
                {
                    IndentMode = FormMode.Update;
                }
                else
                {
                    IndentMode = FormMode.View;
                }
                // dgvIndentDetails.Focus();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetHeaderValues()
        {
            try
            {
                if (m_CurrIndent != null)
                {
                    lblIndentNoValue.Text = m_CurrIndent.IndentNo;
                    lblLocationCode.Text = m_CurrIndent.LocationCode;
                    lblLocationName.Text = m_CurrIndent.LocationName;
                    lblStatusValue.Text = m_CurrIndent.StatusName;
                    lblLocationType.Text = LocationType.ToString();
                    lblCreateDate.Text = m_CurrIndent.DisplayCreatedDate;
                    lblCreateIndentDateValue.Text = m_CurrIndent.DisplayIndentDate == "01-01-1900" ? string.Empty : m_CurrIndent.DisplayIndentDate;
                    lblApprovedDateValue.Text = m_CurrIndent.DisplayApprovedDate == "01-01-1900" ? string.Empty : m_CurrIndent.DisplayApprovedDate;
                    txtCreateRemark.Text = m_CurrIndent.Remark;

                }
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
                if (m_CurrIndentDetail != null)
                {
                    //txtApprovedHOQty.Text = Convert.ToString(m_CurrIndentDetail.DisplayApprovedHOQty);
                    txtApprovedPOQty.Text = Convert.ToString(m_CurrIndentDetail.DisplayApprovedPOQty);
                    //txtApprovedToQty.Text = Convert.ToString(m_CurrIndentDetail.DisplayApprovedTOQty);
                    txtApprovedToQty.Text = Convert.ToString(m_CurrIndentDetail.DisplayApprovedHOQty);
                    txtAvgSales.Text = Convert.ToString(m_CurrIndentDetail.DisplayAvgSale);
                    txtItemCode.Text = m_CurrIndentDetail.ItemCode;
                    txtItemName.Text = m_CurrIndentDetail.ItemName;
                    if (m_CurrIndentDetail.PONumber != null)
                        lstPONumber.DataSource = m_CurrIndentDetail.PONumber;
                    else
                        lstPONumber.DataSource = new List<string>();
                    txtRequestedQty.Text = Convert.ToString(m_CurrIndentDetail.DisplayRequestedQty);
                    txtStockInHand.Text = Convert.ToString(m_CurrIndentDetail.DisplayStockInHand);
                    txtStockInTransit.Text = Convert.ToString(m_CurrIndentDetail.DisplayStockInTransit);
                    txtAvgStockTransfer.Text = Convert.ToString(m_CurrIndentDetail.AvgStockTransfer);
                    txtTotalStk.Text = Convert.ToString(m_CurrIndentDetail.TotalStock);
                    txtSaleStkTransf.Text = Convert.ToString(m_CurrIndentDetail.TotalSaleStockTransfer);
                    if (m_CurrIndentDetail.TOFromLocationID == 0)
                    {
                        setComboToFromLocation();
                    }
                    else
                    {
                        cmbIndentFromLocation.SelectedValue = Convert.ToInt32(m_CurrIndentDetail.TOFromLocationID);
                    }
                    if (m_CurrIndentDetail.TONumber != null)
                        lstTONumber.DataSource = m_CurrIndentDetail.TONumber;
                    else
                        lstTONumber.DataSource = new List<string>();
                }
                else
                {
                    txtApprovedHOQty.Text = string.Empty;
                    txtApprovedPOQty.Text = string.Empty;
                    txtApprovedToQty.Text = string.Empty;
                    txtAvgSales.Text = string.Empty;
                    txtItemCode.Text = string.Empty;
                    txtItemName.Text = string.Empty;
                    lstTONumber.DataSource = null;
                    lstTONumber.DataSource = new List<string>();
                    txtRequestedQty.Text = string.Empty;
                    txtStockInHand.Text = string.Empty;
                    txtStockInTransit.Text = string.Empty;
                    lstPONumber.DataSource = null;
                    lstPONumber.DataSource = new List<string>();
                    txtAvgStockTransfer.Text = string.Empty;
                    txtTotalStk.Text = string.Empty;
                    txtSaleStkTransf.Text = string.Empty;
                    if (cmbIndentFromLocation.Items.Count < 0)
                    {
                        cmbIndentFromLocation.DataSource = null;
                    }
                    else
                    {
                        cmbIndentFromLocation.SelectedValue = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetProperty()
        {
            try
            {
                SetButtonProperty();
                SetTextBoxProperty();
                setComboToFromLocation();
                SetGridViewProperty();
                SetFormProperty();
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetFormProperty()
        {
            try
            {
                if (IndentMode == FormMode.Create)
                {
                    //Create Mode
                    txtItemCode.Focus();
                    tabControlTransaction.TabPages[1].Text = "Create";
                }
                else if (IndentMode == FormMode.View)
                {

                    tabControlTransaction.TabPages[1].Text = "View";
                }
                else if (IndentMode == FormMode.Update)
                {
                    tabControlTransaction.TabPages[1].Text = "Update";

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void setComboToFromLocation()
        {
            try
            {
                cmbIndentFromLocation.DataSource = null;
                DataTable DTWareHouse = Common.ParameterLookup(Common.ParameterType.Locations, new ParameterFilter("Locations", -5, 0, 0));
                DataRow[] dr = DTWareHouse.Select("LocationId=" + m_CurrIndent.LocationID);
                if (dr != null && dr.Length > 0)
                {
                    DTWareHouse.Rows.Remove(dr[0]);
                }
                cmbIndentFromLocation.DataSource = DTWareHouse;
                cmbIndentFromLocation.DisplayMember = "DisplayName";
                cmbIndentFromLocation.ValueMember = "LocationId";
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
      
        private void SetGridViewProperty()
        {
            try
            {
                /*
                //DataTable DTWareHouse = Common.ParameterLookup(Common.ParameterType.Locations, new ParameterFilter("Locations", -5, 0, 0));
                //DataRow[] dr = DTWareHouse.Select("LocationId=" + m_CurrIndent.LocationID);
                if (dr != null && dr.Length > 0)
                {
                    DTWareHouse.Rows.Remove(dr[0]);
                }
                DataGridViewComboBoxColumn _col = (DataGridViewComboBoxColumn)dgvIndentDetails.Columns[CON_GRID_TOFROM];

                _col.DataSource = DTWareHouse;
                _col.DisplayMember = "DisplayName";
                _col.ValueMember = "LocationId";
                _col.DataPropertyName = "TOFromLocationID";
                 */
                dgvIndentDetails.Columns[CON_GRID_ISCONSOLIDATE].Visible = GetGridColumnVisiblity(CON_GRID_ISCONSOLIDATE);
                dgvIndentDetails.Columns[CON_GRID_ISCONSOLIDATE].ReadOnly = GetGridColumnReadOnly(CON_GRID_ISCONSOLIDATE);
                dgvIndentDetails.Columns[CON_GRID_SUGGESTEDQTY].Visible = GetGridColumnVisiblity(CON_GRID_SUGGESTEDQTY);
                dgvIndentDetails.Columns[CON_GRID_REMOVE].Visible = GetGridColumnVisiblity(CON_GRID_REMOVE) && IsRemoveAvailable;
                dgvIndentDetails.Columns[CON_GRID_APPROVEDHOQTY].Visible = GetGridColumnVisiblity(CON_GRID_APPROVEDHOQTY);
                dgvIndentDetails.Columns[CON_GRID_APPROVEDPOQTY].Visible = GetGridColumnVisiblity(CON_GRID_APPROVEDPOQTY);
                dgvIndentDetails.Columns[CON_GRID_APPROVEDTOQTY].Visible = GetGridColumnVisiblity(CON_GRID_APPROVEDTOQTY);
                dgvIndentDetails.Columns[CON_GRID_TOFROM].Visible = GetGridColumnVisiblity(CON_GRID_TOFROM);
                dgvIndentDetails.Columns[CON_GRID_TOFROMSTOK].Visible = GetGridColumnVisiblity(CON_GRID_TOFROMSTOK);
                dgvIndentDetails.Columns[CON_GRID_TOFROMSTOK].ReadOnly = GetGridColumnReadOnly(CON_GRID_TOFROMSTOK);
                dgvIndentDetails.Columns[CON_GRID_ISFORMC].Visible = GetGridColumnVisiblity(CON_GRID_ISFORMC);
                dgvIndentDetails.Columns[CON_GRID_APPROVEDHOQTY].ReadOnly = GetGridColumnReadOnly(CON_GRID_APPROVEDHOQTY);
                dgvIndentDetails.Columns[CON_GRID_APPROVEDPOQTY].ReadOnly = GetGridColumnReadOnly(CON_GRID_APPROVEDPOQTY);
                dgvIndentDetails.Columns[CON_GRID_APPROVEDTOQTY].ReadOnly = GetGridColumnReadOnly(CON_GRID_APPROVEDTOQTY);
                dgvIndentDetails.Columns[CON_GRID_ITEMCODE].ReadOnly = GetGridColumnReadOnly(CON_GRID_ITEMCODE);
                dgvIndentDetails.Columns[CON_GRID_ITEMID].ReadOnly = GetGridColumnReadOnly(CON_GRID_ITEMID);
                dgvIndentDetails.Columns[CON_GRID_ITEMNAME].ReadOnly = GetGridColumnReadOnly(CON_GRID_ITEMNAME);
                dgvIndentDetails.Columns[CON_GRID_REQUESTEDQTY].ReadOnly = GetGridColumnReadOnly(CON_GRID_REQUESTEDQTY);
                dgvIndentDetails.Columns[CON_GRID_REQUESTEDQTY].ReadOnly = true;
 
                dgvIndentDetails.Columns[CON_GRID_SUGGESTEDQTY].ReadOnly = true;
                dgvIndentDetails.Columns[CON_GRID_TOFROM].ReadOnly = GetGridColumnReadOnly(CON_GRID_TOFROM);
                dgvIndentDetails.Columns[CON_GRID_TOFROMSTOK].ReadOnly = true;
                dgvIndentDetails.Columns[CON_GRID_ISFORMC].ReadOnly = GetGridColumnReadOnly(CON_GRID_ISFORMC);
                dgvIndentDetails.Columns[CON_GRID_VENDOR].Visible = GetGridColumnVisiblity(CON_GRID_VENDOR);
                dgvIndentDetails.Columns[CON_GRID_VENDOR].ReadOnly = GetGridColumnReadOnly(CON_GRID_VENDOR); 
                dgvIndentDetails.Columns[CON_GRID_DELLOCATION].Visible = GetGridColumnVisiblity(CON_GRID_DELLOCATION);
                dgvIndentDetails.Columns[CON_GRID_DELLOCATION].ReadOnly = GetGridColumnReadOnly(CON_GRID_DELLOCATION); 
                             

                dgvIndent.Columns[CON_GRID_VIEW].Visible = IsViewAvailable;

                foreach (DataGridViewColumn col in dgvIndentDetails.Columns)
                {
                    if (!col.ReadOnly)
                        col.DefaultCellStyle.BackColor = Color.Silver;
                    else
                        col.DefaultCellStyle.BackColor = Color.White;
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetButtonProperty()
        {
            try
            {
                btnAddDetails.Text = "&Add";
                btnAddDetails.Enabled = AddEnable;
                btnAddPO_TO.Enabled = POTOEnable;
                btnApproved.Enabled = ApproveEnable;
                btnCancel.Enabled = CancelEnable;
                btnClearDetails.Enabled = ClearEnable;
                btnConfirm.Enabled = ConfirmEnable;
                btnReject.Enabled = RejectEnable;
                btnPrint.Enabled = PrintEnable;
                btnCreateReset.Enabled = ResetEnable;
                btnSearch.Enabled = IsSearchAvailable;
                if (m_CurrIndent.Status == (int)Common.IndentStatus.Created)
                {
                    btnSave.Text = "&Update";
                    btnSave.Enabled = UpdateEnable;
                }
                else
                {
                    btnSave.Text = "&Save";
                    btnSave.Enabled = SaveEnable;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void DisableAllButton()
        {
            btnAddDetails.Enabled = false;
            btnAddPO_TO.Enabled = false;
            btnApproved.Enabled = false;
            btnCancel.Enabled = false;
            btnClearDetails.Enabled = false;
            btnConfirm.Enabled = false;
            btnReject.Enabled = false;
            btnSave.Enabled = false;
        }

        private void SetTextBoxProperty()
        {
            try
            {
                txtItemCode.Enabled = false;
                txtRequestedQty.ReadOnly = true;
                txtCreateRemark.Enabled = false;
                if ((IsSaveAvailable || IsUpdateAvailable) && IndentType == Common.IndentType.MANUAL && ((m_CurrIndent.Status == (Int32)Common.IndentStatus.New) || (m_CurrIndent.Status == (Int32)Common.IndentStatus.Created)))
                {
                    txtItemCode.Enabled = true;
                    txtRequestedQty.ReadOnly = false;
                    txtCreateRemark.Enabled = true;
                    cmbIndentFromLocation.Enabled = false;
                }
                if ((LocationType == Common.LocationConfigId.HO) && (m_CurrIndent.Status == (Int32)Common.IndentStatus.Confirmed))
                {
                    txtCreateRemark.Enabled = true;
                    cmbIndentFromLocation.Enabled = true;
                }
                if ((LocationType == Common.LocationConfigId.HO) && (m_CurrIndent.Status == (Int32)Common.IndentStatus.Approved))
                {
                    txtCreateRemark.Enabled = true;
                    cmbIndentFromLocation.Enabled = false;
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
                //dgvIndentDetails.DataSource = null;
                //dgvIndentDetails.DataSource = new List<IndentDetail>();

                (dgvIndentDetails.Columns[CON_GRID_APPROVEDHOQTY] as DataGridViewTextBoxColumn).MaxInputLength = 8;
                (dgvIndentDetails.Columns[CON_GRID_APPROVEDPOQTY] as DataGridViewTextBoxColumn).MaxInputLength = 8;
                (dgvIndentDetails.Columns[CON_GRID_APPROVEDTOQTY] as DataGridViewTextBoxColumn).MaxInputLength = 8;
                
                if (m_CurrIndent.IndentDetail != null)
                {
                    m_bindingMgr = (CurrencyManager)this.BindingContext[m_CurrIndent.IndentDetail];
                    m_bindingMgr.Refresh();
                }
                dgvIndentDetails.DataSource = new List<IndentDetail>();
                if (m_CurrIndent.IndentDetail != null && m_CurrIndent.IndentDetail.Count > 0)
                {
                    dgvIndentDetails.DataSource = m_CurrIndent.IndentDetail;
                    m_bindingMgr = (CurrencyManager)this.BindingContext[m_CurrIndent.IndentDetail];
                    m_bindingMgr.Refresh();
                }
               // dgvIndentDetails.Refresh();
                
                foreach (DataGridViewRow dr in dgvIndentDetails.Rows)
                {
                    
                    if (Convert.ToInt32(dr.Cells["Status"].Value) > 4)
                        dr.ReadOnly = true;
                    if (m_CurrIndent.IndentType == (int)Common.IndentType.SUGGESTED && m_CurrIndent.Status == (int)Common.IndentStatus.Created && m_CurrIndent.LocationID == (int)Common.CurrentLocationId)
                    {
                        if (dgvIndentDetails.Columns.Contains(CON_GRID_SUGGESTEDQTY))
                        {
                            dr.Cells[CON_GRID_REQUESTEDQTY].Value = Convert.ToDecimal(dr.Cells[CON_GRID_SUGGESTEDQTY].Value);
                        }
                    }
                    if (m_CurrIndent.Status == (int)Common.IndentStatus.Confirmed && IsApproveAvailable)
                    {
                        if (dgvIndentDetails.Columns.Contains(CON_GRID_APPROVEDHOQTY))
                        {
                            dr.Cells[CON_GRID_APPROVEDHOQTY].Value = Convert.ToDecimal(dr.Cells[CON_GRID_REQUESTEDQTY].Value);
                            txtApprovedHOQty.Text =Convert.ToString(dr.Cells[CON_GRID_REQUESTEDQTY].Value);
                        }
                    }

                    if (Convert.ToInt32(dr.Cells["Status"].Value) > 4)
                    {
                        //dr.DefaultCellStyle.BackColor = Color.Gray;
                        dr.Cells[CON_GRID_ISCONSOLIDATE].Value = true;
                        //dr.ReadOnly = true;
                    }
                    
                    //if (Convert.ToInt32(dr.Cells["Status"].Value) > 4)
                    //{
                        //dr.DefaultCellStyle.BackColor = Color.Gray;

                        //dr.Cells[CON_GRID_ISCONSOLIDATE].Value = true;
                        //dr.Cells[CON_GRID_ITEMCODE].Style.BackColor = Color.Gray;
                        //dr.Cells[CON_GRID_ITEMNAME].Style.BackColor = Color.Gray;
                        //dr.Cells[CON_GRID_REQUESTEDQTY].Style.BackColor = Color.Gray;
                        //dr.Cells[CON_GRID_SUGGESTEDQTY].Style.BackColor = Color.Gray;
                        //dr.Cells[CON_GRID_TOFROM].Style.BackColor = Color.Gray;
                        //dr.Cells[CON_GRID_TOFROMSTOK].Style.BackColor = Color.Gray;
                        //dr.Cells[CON_GRID_VENDOR].Style.BackColor = Color.Gray;
                        //dr.Cells[CON_GRID_APPROVEDPOQTY].Style.BackColor = Color.Gray;
                        //dr.Cells[CON_GRID_APPROVEDHOQTY].Style.BackColor = Color.Gray;
                        //dr.Cells[CON_GRID_APPROVEDTOQTY].Style.BackColor = Color.Gray;
                        //dr.Cells[CON_GRID_ISFORMC].Style.BackColor = Color.Gray;

                    //}
                }

                //dgvIndentDetails.Refresh();
                if (dgvIndentDetails.Columns.Contains(CON_GRID_ITEMNAME))
                    dgvIndentDetails.AutoResizeColumn(dgvIndentDetails.Columns[CON_GRID_ITEMNAME].Index, DataGridViewAutoSizeColumnMode.DisplayedCells);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void ResetSearchGrid()
        {
            dgvIndent.DataSource = null;
            if (m_indentListSearch != null && m_indentListSearch.Count > 0)
                dgvIndent.DataSource = m_indentListSearch;
        }
        /// <summary>
        /// Creates DataSet for Printing TO Screen report
        /// </summary>
        private void CreatePrintDataSet()
        {
            m_printDataSet = new DataSet();
            // Indent Header Data 
            DataTable dtIndentHeader = new DataTable("IndentHeader");
            DataColumn IndentType = new DataColumn("IndentType", Type.GetType("System.String"));
            DataColumn Create = new DataColumn("Create", Type.GetType("System.String"));
            DataColumn LocationCode = new DataColumn("LocationCode", Type.GetType("System.String"));
            DataColumn Location = new DataColumn("Location", Type.GetType("System.String"));
            DataColumn CityName = new DataColumn("CityName", Type.GetType("System.String"));
            DataColumn IndentNo = new DataColumn("IndentNo", Type.GetType("System.String"));
            DataColumn Status = new DataColumn("Status", Type.GetType("System.String"));
            DataColumn Remark = new DataColumn("Remark", Type.GetType("System.String"));
            DataColumn IndentDate = new DataColumn("IndentDate", Type.GetType("System.String"));
            DataColumn ApprovedDate = new DataColumn("ApprovedDate", Type.GetType("System.String"));
            dtIndentHeader.Columns.Add(IndentType);
            dtIndentHeader.Columns.Add(Create);
            dtIndentHeader.Columns.Add(LocationCode);
            dtIndentHeader.Columns.Add(Location);
            dtIndentHeader.Columns.Add(CityName);
            dtIndentHeader.Columns.Add(IndentNo);
            dtIndentHeader.Columns.Add(Status);
            dtIndentHeader.Columns.Add(Remark);
            dtIndentHeader.Columns.Add(IndentDate);
            dtIndentHeader.Columns.Add(ApprovedDate);
            DataRow dRow = dtIndentHeader.NewRow();
            dRow["IndentType"] = m_CurrIndent.IndentType;
            dRow["Create"] = m_CurrIndent.CreatedDate.ToString(Common.DTP_DATE_FORMAT);
            dRow["LocationCode"] = m_CurrIndent.LocationCode;
            dRow["Location"] = m_CurrIndent.LocationName;
            dRow["CityName"] = m_CurrIndent.CityName.Trim().ToUpper();
            dRow["IndentNo"] = m_CurrIndent.IndentNo;
            dRow["Status"] = m_CurrIndent.StatusName;
            dRow["Remark"] = m_CurrIndent.Remark;
            dRow["IndentDate"] = m_CurrIndent.IndentDate.ToString(Common.DTP_DATE_FORMAT);
            dRow["ApprovedDate"] = m_CurrIndent.ApprovedDate.ToString(Common.DTP_DATE_FORMAT);
            dtIndentHeader.Rows.Add(dRow);
            m_printDataSet.Tables.Add(dtIndentHeader);
            // dataTable for Indent Screen Report    
            DataTable dtIndentDetail = new DataTable("IndentDetail");
            IndentDetail indentDetailObj = new IndentDetail();
            indentDetailObj.IndentNo = m_CurrIndent.IndentNo;
            dtIndentDetail = indentDetailObj.IndentDetailSearchDataTable();
            for (int i = 0; i < dtIndentDetail.Rows.Count; i++)
            {
                dtIndentDetail.Rows[i]["SuggestedQty"] = Math.Round(Convert.ToDecimal(dtIndentDetail.Rows[i]["SuggestedQty"]), Common.DisplayQtyRounding, MidpointRounding.AwayFromZero);
                dtIndentDetail.Rows[i]["StockInHand"] = Math.Round(Convert.ToDecimal(dtIndentDetail.Rows[i]["StockInHand"]), Common.DisplayQtyRounding, MidpointRounding.AwayFromZero);
                dtIndentDetail.Rows[i]["AvgSaleFactor"] = Math.Round(Convert.ToDecimal(dtIndentDetail.Rows[i]["AvgSaleFactor"]), Common.DisplayQtyRounding, MidpointRounding.AwayFromZero);
                dtIndentDetail.Rows[i]["RequestedQty"] = Math.Round(Convert.ToDecimal(dtIndentDetail.Rows[i]["RequestedQty"]), Common.DisplayQtyRounding, MidpointRounding.AwayFromZero);
                dtIndentDetail.Rows[i]["ApprovedHOQty"] = Math.Round(Convert.ToDecimal(dtIndentDetail.Rows[i]["ApprovedHOQty"]), Common.DisplayQtyRounding, MidpointRounding.AwayFromZero);
                dtIndentDetail.Rows[i]["ApprovedTOQty"] = Math.Round(Convert.ToDecimal(dtIndentDetail.Rows[i]["ApprovedTOQty"]), Common.DisplayQtyRounding, MidpointRounding.AwayFromZero);
                dtIndentDetail.Rows[i]["ApprovedPOQty"] = Math.Round(Convert.ToDecimal(dtIndentDetail.Rows[i]["ApprovedPOQty"]), Common.DisplayQtyRounding, MidpointRounding.AwayFromZero);
                dtIndentDetail.Rows[i]["StockInTransit"] = Math.Round(Convert.ToDecimal(dtIndentDetail.Rows[i]["StockInTransit"]), Common.DisplayQtyRounding, MidpointRounding.AwayFromZero);
                dtIndentDetail.Rows[i]["AvgStockTransfer"] = Math.Round(Convert.ToDecimal(dtIndentDetail.Rows[i]["AvgStockTransfer"]), Common.DisplayQtyRounding, MidpointRounding.AwayFromZero);
                dtIndentDetail.Rows[i]["TotalStock"] = Math.Round(Convert.ToDecimal(dtIndentDetail.Rows[i]["AvgStockTransfer"]), Common.DisplayQtyRounding, MidpointRounding.AwayFromZero);
                dtIndentDetail.Rows[i]["TotalSaleTransfer"] = Math.Round(Convert.ToDecimal(dtIndentDetail.Rows[i]["TotalSaleTransfer"]), Common.DisplayQtyRounding, MidpointRounding.AwayFromZero);
            }
            m_printDataSet.Tables.Add(dtIndentDetail.Copy());
            m_printDataSet.Tables[1].TableName = "IndentDetail";
        }

        /// <summary>
        /// Prints TO Screen report
        /// </summary>
        private void PrintReport()
        {
            CreatePrintDataSet();
            CoreComponent.UI.ReportScreen reportScreenObj = new CoreComponent.UI.ReportScreen((int)Common.ReportType.ManualIndent, m_printDataSet);
           
            reportScreenObj.ShowDialog();
            //reportScreenObj.PrintReport();

            //m_printDataSet = null;
           
        }



        #endregion

        #region Get

        #region Button Property
        private bool ResetEnable
        {
            get
            {
                if (LocationType == Common.LocationConfigId.HO)
                    return false;
                else
                    return true;
            }
        }
        private bool SaveEnable
        {
            get
            {
                if ((IsSaveAvailable) & (m_CurrIndent.Status == (int)Common.IndentStatus.New || m_CurrIndent.Status == (int)Common.IndentStatus.Created))
                    return true;
                else
                    return false;
            }
        }
        private bool UpdateEnable
        {
            get
            {
                if ((IsUpdateAvailable) & (m_CurrIndent.Status == (int)Common.IndentStatus.New || m_CurrIndent.Status == (int)Common.IndentStatus.Created))
                    return true;
                else
                    return false;
            }
        }
        private bool RejectEnable
        {
            get
            {
                if ((IsRejectAvailable) && m_CurrIndent.Status == (int)Common.IndentStatus.Confirmed)
                    return true;
                else
                    return false;
            }

        }
        private bool CancelEnable
        {
            get
            {
                if (IsCancelAvailable && IndentType == Common.IndentType.MANUAL && m_CurrIndent.Status == (int)Common.IndentStatus.Created)
                    return true;
                else
                    return false;
            }
        }
        private bool ConfirmEnable
        {
            get
            {
                if (IsConfirmAvailable && (m_CurrIndent.Status == (int)Common.IndentStatus.Created || m_CurrIndent.Status == (int)Common.IndentStatus.New))
                    return true;
                else
                    return false;
            }
        }
        private bool AddEnable
        {
            get
            {
                if ((IsSaveAvailable || IsUpdateAvailable) && (IndentType == Common.IndentType.MANUAL && (m_CurrIndent.Status == (int)Common.IndentStatus.New || m_CurrIndent.Status == (int)Common.IndentStatus.Created)))
                {
                    return true;
                }
                else
                    return false;
            }
        }
        private bool ClearEnable
        {
            get
            {
                if (IndentType == Common.IndentType.MANUAL && (m_CurrIndent.Status == (int)Common.IndentStatus.New || m_CurrIndent.Status == (int)Common.IndentStatus.Created))
                {

                    return true;
                }
                else
                    return false;
            }
        }
        private bool ApproveEnable
        {
            get
            {
                if (IsApproveAvailable && m_CurrIndent.Status == (int)Common.IndentStatus.Confirmed)
                    return true;
                else
                    return false;
            }
        }
        private bool POTOEnable
        {
            get
            {
                if (IsPOTOAvailable && m_CurrIndent.Status == (int)Common.IndentStatus.Approved)
                    return true;
                else
                    return false;
            }
        }
        private bool PrintEnable
        {
            get
            {
                if (IsPrintAvailable && m_CurrIndent.Status >= (int)Common.IndentStatus.Confirmed)
                    return true;
                else
                    return false;
            }
        }
        #endregion
        private bool GetGridColumnReadOnly(string col)
        {
            switch (col)
            {
                case CON_GRID_REQUESTEDQTY:
                    {
                        //if (LocationType != Common.LocationConfigId.HO && (m_CurrIndent.Status == (int)Common.IndentStatus.Created || m_CurrIndent.Status == (int)Common.IndentStatus.New))
                        if ((IsSaveAvailable || IsUpdateAvailable || IsConfirmAvailable) && (m_CurrIndent.Status == (int)Common.IndentStatus.Created || m_CurrIndent.Status == (int)Common.IndentStatus.New))
                            return false;
                        else
                            return true;
                    }
                case CON_GRID_APPROVEDHOQTY:
                    {
                        if (IsApproveAvailable && m_CurrIndent.Status == (int)Common.IndentStatus.Confirmed)
                            return false;
                        else
                            return true;
                    }
                case CON_GRID_APPROVEDPOQTY:
                    {
                        if (IsPOTOAvailable && m_CurrIndent.Status == (int)Common.IndentStatus.Approved)
                            return false;
                        else
                            return true;
                    }
                case CON_GRID_APPROVEDTOQTY:
                    {
                        if (IsPOTOAvailable && m_CurrIndent.Status == (int)Common.IndentStatus.Approved)
                            return false;
                        else
                            return true;
                    }
                case CON_GRID_ISCONSOLIDATE:
                    {
                        //if (IsPOTOAvailable && m_CurrIndent.Status == (int)Common.IndentStatus.Approved)
                        //    return false;
                        //else
                            return true;
                    }    
                case CON_GRID_TOFROM:
                    {
                        if (IsPOTOAvailable && m_CurrIndent.Status == (int)Common.IndentStatus.Confirmed)
                            return false;
                        else
                            return true;
                    }
                case CON_GRID_TOFROMSTOK:
                    {
                        if (IsPOTOAvailable && m_CurrIndent.Status == (int)Common.IndentStatus.Confirmed)
                            return false;
                        else
                            return true;
                    }
                case CON_GRID_ISFORMC:
                    {
                        if (IsPOTOAvailable && m_CurrIndent.Status == (int)Common.IndentStatus.Approved)
                            return false;
                        else
                            return true;
                    }
                case CON_GRID_VENDOR:
                    {
                        if (IsPOTOAvailable && m_CurrIndent.Status == (int)Common.IndentStatus.Approved)
                            return false;
                        else
                            return true;
                    }
                case CON_GRID_DELLOCATION:
                    {
                        if (IsPOTOAvailable && m_CurrIndent.Status == (int)Common.IndentStatus.Approved)
                            return false;
                        else
                            return true;
                    }

                default:
                    return true;
            }
        }
        private bool GetGridColumnVisiblity(string col)
        {
            switch (col)
            {
                case CON_GRID_SUGGESTEDQTY:
                    {
                        if (m_CurrIndent.IndentType == (Int32)Common.IndentType.MANUAL)
                            return false;
                        else
                            return true;

                    }
                case CON_GRID_REMOVE:
                    {
                        if ((m_CurrIndent.Status == (int)Common.IndentStatus.Created || m_CurrIndent.Status == (int)Common.IndentStatus.New) && IndentType == Common.IndentType.MANUAL)
                            return true;
                        else
                            return false;
                    }
                case CON_GRID_APPROVEDHOQTY:
                    {
                        if (m_CurrIndent.Status == (int)Common.IndentStatus.Created || m_CurrIndent.Status == (int)Common.IndentStatus.New)
                            return false;
                        else
                            return true;
                    }
                case CON_GRID_APPROVEDPOQTY:
                    {
                        if (m_CurrIndent.Status == (int)Common.IndentStatus.Created || m_CurrIndent.Status == (int)Common.IndentStatus.New || m_CurrIndent.Status == (int)Common.IndentStatus.Confirmed || m_CurrIndent.Status ==(int)Common.IndentStatus.Approved || m_CurrIndent.Status ==(int)Common.IndentStatus.ConsolidationComplete)
                            return false;
                        else
                            return true;
                    }
                case CON_GRID_DELLOCATION:
                    {
                        if (m_CurrIndent.Status == (int)Common.IndentStatus.Created || m_CurrIndent.Status == (int)Common.IndentStatus.New || m_CurrIndent.Status == (int)Common.IndentStatus.Confirmed || m_CurrIndent.Status == (int)Common.IndentStatus.Approved || m_CurrIndent.Status == (int)Common.IndentStatus.ConsolidationComplete)
                            return false;
                        else
                            return true;
                    }
                case CON_GRID_VENDOR:
                    {
                        if (m_CurrIndent.Status == (int)Common.IndentStatus.Created || m_CurrIndent.Status == (int)Common.IndentStatus.New || m_CurrIndent.Status == (int)Common.IndentStatus.Confirmed || m_CurrIndent.Status == (int)Common.IndentStatus.Approved || m_CurrIndent.Status == (int)Common.IndentStatus.ConsolidationComplete)
                            return false;
                        else
                            return true;
                    }
                case CON_GRID_APPROVEDTOQTY:
                    {
                        if (m_CurrIndent.Status == (int)Common.IndentStatus.Created || m_CurrIndent.Status == (int)Common.IndentStatus.New || m_CurrIndent.Status == (int)Common.IndentStatus.Confirmed || m_CurrIndent.Status == (int)Common.IndentStatus.Approved || m_CurrIndent.Status == (int)Common.IndentStatus.ConsolidationComplete)
                            return false;
                        else
                            return true;
                    }
                case CON_GRID_ISCONSOLIDATE :
                    {
                        if (m_CurrIndent.Status == (int)Common.IndentStatus.Created || m_CurrIndent.Status == (int)Common.IndentStatus.New || m_CurrIndent.Status == (int)Common.IndentStatus.Confirmed)
                            return false;
                        else
                            return true;
                    }
                case CON_GRID_TOFROM:
                    {
                        if (m_CurrIndent.Status == (int)Common.IndentStatus.Created || m_CurrIndent.Status == (int)Common.IndentStatus.New || m_CurrIndent.Status == (int)Common.IndentStatus.Confirmed || m_CurrIndent.Status == (int)Common.IndentStatus.Approved || m_CurrIndent.Status == (int)Common.IndentStatus.ConsolidationComplete)
                            return false;
                        else
                            return true;
                    }
                case CON_GRID_TOFROMSTOK:
                    {
                        if (m_CurrIndent.Status == (int)Common.IndentStatus.Created || m_CurrIndent.Status == (int)Common.IndentStatus.New )
                            return false;
                        else
                            return true;
                    }    
                case CON_GRID_ISFORMC:
                    {
                        if (m_CurrIndent.Status == (int)Common.IndentStatus.Created || m_CurrIndent.Status == (int)Common.IndentStatus.New || m_CurrIndent.Status == (int)Common.IndentStatus.Confirmed)
                            return false;
                        else
                            return true;
                    }
                default:
                    {
                        return false;
                    }
            }
        }

        private void RefreshItemList()
        {
            try
            {
                ItemDetails list = new ItemDetails();
                list.LocationId = Common.CurrentLocationId.ToString();
                ItemList = list.SearchLocationItem();
                _itemcollection = new AutoCompleteStringCollection();
                for (int j = 0; j < ItemList.Count; j++)
                {
                    _itemcollection.Add(ItemList[j].ItemCode);
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

        #endregion

        #region Data Methods

        private void GetItemDetail()
        {
            try
            {
                m_CurrIndentDetail = new IndentDetail();
                m_CurrIndentDetail.GetItemStockDetail(txtItemCode.Text.Trim(), -1, Common.CurrentLocationId);
                m_CurrIndentDetail.IndentNo = IndentNo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private Indent GetIndent()
        {
            try
            {
                Indent _indent = new Indent();
                if (IndentNo != string.Empty)
                {
                    _indent.IndentNo = IndentNo;
                    _indent.IndentType = (int)IndentType;
                    _indent.GetCompleteIndent();
                }
                if (_indent != null && _indent.Status == (Int32)Common.IndentStatus.New)
                {
                    //Set values from user object .
                    _indent.LocationID = Common.CurrentLocationId;
                    _indent.LocationCode = Common.LocationCode;
                    _indent.LocationName = LocationName;
                    _indent.IndentType = (int)IndentType;
                    _indent.CreatedBy = UserID;
                }
                return _indent;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
                return null;
            }
        }

        private int GetPrimaryLocation(string ItemCode, int vendorID)
        {
            try
            {
                int locationId = 0;
                ItemVendorLocationDetails vendordetail = new ItemVendorLocationDetails();
                vendordetail.ItemCode = ItemCode;
                vendordetail.IsVendorPrimaryForLocation = 1;
                vendordetail.VendorId = vendorID;
                List<ItemVendorLocationDetails> LoctaionList = vendordetail.Search();
                if (LoctaionList != null && LoctaionList.Count > 0)
                    locationId = Convert.ToInt32(LoctaionList[0].LocationId);
                return locationId;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("30001"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
                return 0;
            }
        }

        private int GetPrimaryVendor(string itemCode, ref int vendorStatus)
        {
            try
            {
                int vendorID = 0;
                CoreComponent.MasterData.BusinessObjects.ItemVendorDetails item = new CoreComponent.MasterData.BusinessObjects.ItemVendorDetails();
                item.ItemCode = itemCode;
                item.IsVendorPrimary = 1;
                item.Status = 1;
                List<CoreComponent.MasterData.BusinessObjects.ItemVendorDetails> VendorList = item.Search();
                if (VendorList != null && VendorList.Count > 0)
                {
                    vendorID = Convert.ToInt32(VendorList[0].VendorId);
                    vendorStatus = Convert.ToInt32(VendorList[0].VendorStatus);
                }
                return vendorID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private IndentConsolidation PopulateIndentConsolidationObject(ref string error)
        {
            try
            {
                IIndentConsolidationHeader objIndentConsolidationHeader;
                List<IndentConsolidationHeader> lstIndentConsolidationHeader = new List<IndentConsolidationHeader>();

                //Populate IndentConsolidation object
                IIndentConsolidationDetail objIndentConsolidationDetail;
                List<IndentConsolidationDetail> lstIndentConsolidationDetail;


                int intHeaderLineNo = 1;
                int intDetailLineNo = 1;
                int _vendorid = 0;
                int _locationid = 0;
                foreach (IndentDetail _detail in m_CurrIndent.IndentDetail)
                {
                    
                    //Check for status of IndentDetail
                    if (_detail.Status <= 0)
                    {
                        lstIndentConsolidationDetail = new List<IndentConsolidationDetail>();
                        int _vendorStatus = Common.INT_DBNULL;
                        //_vendorid = GetPrimaryVendor(_detail.ItemCode, ref _vendorStatus);
                        _vendorid = _detail.Vendor;
                         _locationid = _detail.DelLocation;
                        //if (_vendorid > 0)
                        //{
                        //    if (_vendorStatus == 0 || _vendorStatus == Common.INT_DBNULL || _vendorStatus == 2)
                        //    {
                        //        MessageBox.Show(Common.GetMessage("VAL0126", _detail.ItemCode), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //        return null;
                        //    }
                    
                        //    _locationid = GetPrimaryLocation(_detail.ItemCode, _vendorid);
                         //   if (_locationid > 0)
                         //   {
                                //Populate PO Values
                                objIndentConsolidationDetail = new IndentConsolidationDetail(Common.INT_DBNULL,
                                                                                           Common.INT_DBNULL,
                                                                                           intDetailLineNo++,
                                                                                           1,
                                                                                           Convert.ToInt32(Common.IndentConsolidationRecordType.PO),
                                                                                           _detail.ApprovedPOQty,
                                                                                           _vendorid,
                                                                                           _locationid,
                                                                                           Common.INT_DBNULL, _detail.IsFormC);
                                lstIndentConsolidationDetail.Add((IndentConsolidationDetail)objIndentConsolidationDetail);

                                //Populate TO Values
                                objIndentConsolidationDetail =
                                       new IndentConsolidationDetail(Common.INT_DBNULL,
                                                                       Common.INT_DBNULL,
                                                                       intDetailLineNo++,
                                                                       1,
                                                                       (int)Common.IndentConsolidationRecordType.TOI,
                                                                       _detail.ApprovedTOQty,
                                                                       Common.INT_DBNULL,
                                                                       Common.INT_DBNULL,
                                                                       _detail.TOFromLocationID, false
                                                                       );

                                lstIndentConsolidationDetail.Add((IndentConsolidationDetail)objIndentConsolidationDetail);

                                //Populate Header Info first
                                objIndentConsolidationHeader = new IndentConsolidationHeader(Common.INT_DBNULL,
                                                                                         _detail.ItemID,
                                                                                          intHeaderLineNo,
                                                                                         _detail.IndentNo,
                                                                                         _detail.ApprovedHOQty,
                                                                                         m_CurrIndent.LocationID,
                                                                                         Common.INT_DBNULL, lstIndentConsolidationDetail);
                                intHeaderLineNo++;
                                lstIndentConsolidationHeader.Add((IndentConsolidationHeader)objIndentConsolidationHeader);
                                intDetailLineNo = 1;
                           // }
                            //else
                            //{
                            //    error = Common.GetMessage("30010", "Primary Location", "Item :" + _detail.ItemName);
                            //}
                        //}
                        //else
                        //{
                        //    error = Common.GetMessage("30010", "Primary VendorCode", "Item :" + _detail.ItemName);
                        //}
                    }
                }
                IndentConsolidation objIndentConsolidation = new IndentConsolidation(Common.INT_DBNULL,
                                                                                       (int)Common.IndentConsolidationState.ConsolidationComplete,
                                                                                        Convert.ToInt32(Common.IndentConsolidationSource.Manual),
                                                                                        UserID,
                                                                                        DateTime.Now.ToString(Common.DATE_TIME_FORMAT),
                                                                                        UserID,
                                                                                        DateTime.Now.ToString(Common.DATE_TIME_FORMAT),
                                                                                        lstIndentConsolidationHeader,
                                                                                        IndentConsolidationBL.OperationState.New);

                return (IndentConsolidation)objIndentConsolidation;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Button Methods

        //Save Indent
        private void Save(int status)
        {
            try
            {
                #region Check validation
                ValidateSave();
                #endregion

                #region Check Errors

                StringBuilder sbError = new StringBuilder();
                sbError = GenerateDataGridViewError();

                #endregion

                # region Display Error
                if (!sbError.ToString().Trim().Equals(string.Empty))
                {
                    MessageBox.Show(sbError.ToString(), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                #endregion
                DialogResult saveResult = MessageBox.Show(Common.GetMessage("INF0027"), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (saveResult == DialogResult.Yes)
                {
                    string errorMessage = "";
                    if (status == (Int32)Common.IndentStatus.Created)
                    {
                        m_CurrIndent.Status = (Int32)Common.IndentStatus.Created;
                        m_CurrIndent.Remark = txtCreateRemark.Text.Trim();
                    }
                    else if (status == (Int32)Common.IndentStatus.Confirmed)
                    {
                        m_CurrIndent.IndentDate = Common.DATETIME_CURRENT;
                        m_CurrIndent.Status = (int)Common.IndentStatus.Confirmed;
                        m_CurrIndent.Remark = txtCreateRemark.Text.Trim();
                    }
                    bool isSave = m_CurrIndent.Save(ref errorMessage);

                    if (isSave && errorMessage.Equals(string.Empty))
                    {
                        IndentNo = m_CurrIndent.IndentNo;
                        MessageBox.Show(Common.GetMessage("8003", "Indent No", m_CurrIndent.IndentNo), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ResetForm();
                        Search();
                        ClearItem();
                    }
                    else
                    {
                        //log error
                        MessageBox.Show(Common.GetMessage(errorMessage), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(ex.ToString(), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Update Indent
        private void Update(int status)
        {
            try
            {
                #region Check validation
                ValidateSave();
                #endregion

                #region Check Errors

                StringBuilder sbError = new StringBuilder();
                sbError = GenerateDataGridViewError();

                #endregion

                # region Display Error
                if (!sbError.ToString().Trim().Equals(string.Empty))
                {
                    MessageBox.Show(sbError.ToString(), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                #endregion
                DialogResult saveResult = MessageBox.Show(Common.GetMessage("INF0027"), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (saveResult == DialogResult.Yes)
                {
                    switch (status)
                    {
                        case (Int32)Common.IndentStatus.Created:
                            {
                                m_CurrIndent.Status = (Int32)Common.IndentStatus.Created;
                                m_CurrIndent.Remark = txtCreateRemark.Text.Trim();
                                m_CurrIndent.ModifiedBy = UserID;
                                break;
                            }
                        case (Int32)Common.IndentStatus.Confirmed:
                            {
                                //m_CurrIndent.IndentDate = Common.DATETIME_CURRENT;
                                m_CurrIndent.Status = (int)Common.IndentStatus.Confirmed;
                                m_CurrIndent.Remark = txtCreateRemark.Text.Trim();
                                m_CurrIndent.ModifiedBy = UserID;
                                break;
                            }

                        default:
                            break;
                    }
                    string errorMessage = "";
                    bool isUpdated = m_CurrIndent.Save(ref errorMessage);
                    if (isUpdated && errorMessage.Equals(string.Empty))
                    {
                        IndentNo = m_CurrIndent.IndentNo;
                        MessageBox.Show(Common.GetMessage("8001"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ResetForm();
                        Search();
                        ClearItem();
                    }
                    else
                    {
                        // log error
                        MessageBox.Show(Common.GetMessage(errorMessage), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(ex.ToString(), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Remove Indent detail from list
        private void Remove(int rowIndex)
        {
            try
            {
                DialogResult saveResult = MessageBox.Show(Common.GetMessage("5010", "Remove"), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (saveResult == DialogResult.Yes)
                {
                    m_CurrIndent.IndentDetail.RemoveAt(rowIndex);
                    ClearItem();
                    ResetGrid();
                    ClearItem();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        private void ClearItem()
        {
            // Reset Indent Detail Object
            m_CurrIndentDetail = null;
            txtItemCode.Enabled = IsSaveAvailable;
            btnAddDetails.Text = "&Add";
            //Reset Text Boxes
            SetDetailValues();
            dgvIndentDetails.ClearSelection();
            errCreateIndent.Clear();
        }

        private void Search()
        {
            m_indentListSearch = new List<Indent>();
            ResetSearchGrid();
            try
            {
                Indent _indent = new Indent();
                if (dtpFromDate.Checked)
                    _indent.FromIndentDate = dtpFromDate.Value;
                else
                    _indent.FromIndentDate = Common.DATETIME_NULL;

                if (dtpToDate.Checked)
                    _indent.ToIndentDate = dtpToDate.Value;
                else
                    _indent.ToIndentDate = Common.DATETIME_NULL;
                _indent.IndentNo = txtIndentNo.Text.Trim();
                _indent.IndentType = (int)IndentType;
                _indent.LocationID = (int)cmbLocation.SelectedValue;
                _indent.Status = (int)cmbStatus.SelectedValue;
                List<Indent> lstIndent = _indent.Search();
                foreach (Indent item in lstIndent)
                {
                    if (item.IndentDate == Common.DATETIME_NULL)
                    {
                        item.StrIndentDate = "";
                    }
                    else
                    {
                        item.StrIndentDate = item.IndentDate.ToShortDateString();
                    }
                }
                m_indentListSearch = _indent.Search();
                ResetSearchGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("30009", "Search"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }


        #endregion

        #endregion

        #region Events

        #region Button Clicks

        //Search Button Click
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

                if (m_indentListSearch.Count == 0)
                {
                    MessageBox.Show(Common.GetMessage("8002", "Search"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("30001"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        // Search Reset Button Click
        private void btnSearchReset_Click(object sender, EventArgs e)
        {
            try
            {
                txtIndentNo.Text = string.Empty;
                dtpFromDate.Value = DateTime.Now;
                dtpToDate.Value = DateTime.Now;
                dtpFromDate.Checked = false;
                dtpToDate.Checked = false;
                cmbLocation.SelectedIndex = 0;
                if (LocationType != Common.LocationConfigId.HO)
                {
                    cmbLocation.SelectedValue = Common.CurrentLocationId;
                    cmbLocation.Enabled = false;
                }
                cmbStatus.SelectedIndex = 0;
                dgvIndent.DataSource = null;
                dgvIndent.DataSource = new List<Indent>();
                IndentNo = string.Empty;
                ResetForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("30001"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        // Add Details
        private void btnAddDetails_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateLogIN())
                    return;
                #region Check validation
                bool IsValid = ValidateAdd();
                #endregion

                #region Check Errors
                if (!IsValid)
                {
                    StringBuilder sbError = new StringBuilder();
                    sbError = GenerateCreateError();

                    if (!sbError.ToString().Trim().Equals(string.Empty))
                    {
                        MessageBox.Show(sbError.ToString(), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                #endregion

                # region Create IndentDetail Object and Bind to Gridview

                if (m_CurrIndent.IndentDetail == null)
                    m_CurrIndent.IndentDetail = new List<IndentDetail>();
                bool exist = false;
                foreach (IndentDetail I in m_CurrIndent.IndentDetail)
                {
                    if (I.ItemCode == m_CurrIndentDetail.ItemCode)
                    {
                        m_CurrIndentDetail = I;
                        exist = true;
                        break;
                    }
                }
                m_CurrIndentDetail.RequestedQty = Convert.ToDouble(txtRequestedQty.Text.Trim());
                if (!exist)
                {
                    m_CurrIndent.IndentDetail.Add(m_CurrIndentDetail);
                }
                ResetGrid();
                ClearItem();
                #endregion
               // txtItemCode.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("30001"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        //Clear Details
        private void btnClearDetails_Click(object sender, EventArgs e)
        {
            try
            {
                ClearItem();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("30001"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        //Save Update Button Click 
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateLogIN())
                    return;
                if (m_CurrIndent.Status == (Int32)Common.IndentStatus.New)
                {
                    Save((Int32)Common.IndentStatus.Created);
                }
                else
                {
                    Update((Int32)Common.IndentStatus.Created);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("30001"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        //Confirm Button
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateLogIN())
                    return;
                #region Check validation
                ValidateSave();
                #endregion

                #region Check Errors

                StringBuilder sbError = new StringBuilder();
                sbError = GenerateDataGridViewError();

                #endregion

                # region Display Error
                if (!sbError.ToString().Trim().Equals(string.Empty))
                {
                    MessageBox.Show(sbError.ToString(), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                #endregion
                DialogResult saveResult;
                if (!ValidateConfirm())
                {
                    saveResult = MessageBox.Show(Common.GetMessage("5013"), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                }
                else
                {
                    saveResult = MessageBox.Show(Common.GetMessage("5010", "Confirm"), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                }
                if (saveResult == DialogResult.Yes)
                {
                    int status = m_CurrIndent.Status;
                    string errorMessage = "";
                    if (m_CurrIndent.Status == (int)Common.IndentStatus.New)
                    {
                        m_CurrIndent.CreatedBy = UserID;
                    }
                    // m_CurrIndent.IndentDate = Convert.ToDateTime(DateTime.Today.ToString(Common.DATE_TIME_FORMAT));
                    m_CurrIndent.Status = (int)Common.IndentStatus.Confirmed;
                    m_CurrIndent.Remark = txtCreateRemark.Text.Trim();
                    m_CurrIndent.ModifiedBy = UserID;
                    bool isConfirm = m_CurrIndent.Save(ref errorMessage);
                    if (isConfirm && errorMessage.Equals(string.Empty))
                    {
                        string msg = string.Empty;
                        if (status == (int)Common.IndentStatus.New)
                        {
                            MessageBox.Show(Common.GetMessage("8006", "Indent No.", m_CurrIndent.IndentNo), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show(Common.GetMessage("INF0055", "Indent", "Confirmed"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        IndentNo = m_CurrIndent.IndentNo;
                        ResetForm();
                        Search();
                        ClearItem();
                    }
                    else
                    {
                        MessageBox.Show(Common.GetMessage(errorMessage), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("30001"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);

            }
        }

        //Cancel Button
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (!ValidateLogIN())
                return;
            if (m_CurrIndent.Status == (Int32)Common.IndentStatus.Created)
            {
                try
                {
                    string errorMessage = "";
                    DialogResult saveResult = MessageBox.Show(Common.GetMessage("5010", "Cancel"), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (saveResult == DialogResult.Yes)
                    {
                        m_CurrIndent.ModifiedBy = UserID;
                        m_CurrIndent.Remark = txtCreateRemark.Text.Trim();
                        m_CurrIndent.Status = (Int32)Common.IndentStatus.Cancelled;
                        bool isCancel = m_CurrIndent.Save(ref errorMessage);
                        if (isCancel && errorMessage.Equals(string.Empty))
                        {
                            MessageBox.Show(Common.GetMessage("INF0055", "Indent", "Cancelled"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ResetForm();
                            Search();
                        }
                        else
                        {
                            // log exception
                            MessageBox.Show(Common.GetMessage(errorMessage), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(Common.GetMessage("30001"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Common.LogException(ex);
                }
            }
        }

        //Approved Button
        private void btnApproved_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateLogIN())
                    return;
                string errorMessage = "";
                Boolean iszero = ValidatedApprove();
                if (!iszero)
                {
                    MessageBox.Show(Common.GetMessage("INF0244"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    if (m_CurrIndentDetail == null)
                    {
                        MessageBox.Show(Common.GetMessage("VAL0002","at least one item from grid "), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    if (m_CurrIndentDetail.TOFromLocationID < 0)
                    {
                        MessageBox.Show(Common.GetMessage("VAL0002","From Location"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    Boolean  isMorethanAppQty = ValidateLessFromLocStockQty();
                    if (!isMorethanAppQty)
                    {
                        MessageBox.Show(Common.GetMessage("VAL0060", "Approved TOI Qty.", "From Loc.Stock"), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        return;
                    }

                    DialogResult saveResult = MessageBox.Show(Common.GetMessage("5010", "Approve"), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (saveResult == DialogResult.Yes)
                    {
                        isMorethanAppQty = ValidateMoreApprovedQty();
                        if (!isMorethanAppQty)
                        {
                          DialogResult result = MessageBox.Show(Common.GetMessage( "INF0245","Approve"),Common.GetMessage("10001"),MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                          if (result == DialogResult.No)
                              return;
                        }

               
                        m_CurrIndent.ModifiedBy = UserID;
                        m_CurrIndent.Remark = txtCreateRemark.Text.Trim();
                        m_CurrIndent.Status = (Int32)Common.IndentStatus.Approved;
                        m_CurrIndent.ApprovedBy = UserID;
                        //m_CurrIndent.ApprovedDate = Convert.ToDateTime(DateTime.Today.ToString(Common.DATE_TIME_FORMAT));
                        bool isApproved = m_CurrIndent.Save(ref errorMessage);
                        if (isApproved && errorMessage.Equals(string.Empty))
                        {
                            MessageBox.Show(Common.GetMessage("INF0055", "Indent", "Approved"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ResetForm();
                            Search();
                            ClearItem();
                        }
                        else
                        {
                            // log exception
                            MessageBox.Show(Common.GetMessage(errorMessage), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("30001"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);

            }
        }

        //Reject Button
        private void btnReject_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateLogIN())
                    return;
                string errorMessage = "";
                DialogResult saveResult = MessageBox.Show(Common.GetMessage("5010", "Rejected"), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (saveResult == DialogResult.Yes)
                {
                    m_CurrIndent.ModifiedBy = UserID;
                    m_CurrIndent.Remark = txtCreateRemark.Text.Trim();
                    m_CurrIndent.Status = (Int32)Common.IndentStatus.Rejected;
                    foreach (IndentDetail detail in m_CurrIndent.IndentDetail)
                    {
                        detail.ApprovedHOQty = 0;
                    }
                    bool isReject = m_CurrIndent.Save(ref errorMessage);
                    if (isReject && errorMessage.Equals(string.Empty))
                    {
                        MessageBox.Show(Common.GetMessage("INF0055", "Indent", "Reject"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ResetForm();
                        Search();
                    }
                    else
                    {
                        // log exception
                        MessageBox.Show(Common.GetMessage(errorMessage), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("30001"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        //Add PO And TO Button
        private void btnAddPO_TO_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateLogIN())
                    return;
                #region Check validation
                ValidatedAddPOTO();
                #endregion

                #region Check Errors

                StringBuilder sbError = new StringBuilder();
                sbError = GenerateDataGridViewError();

                #endregion

                # region Display Error
                if (!sbError.ToString().Trim().Equals(string.Empty))
                {
                    MessageBox.Show(sbError.ToString(), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                #endregion
                else
                {
                    DialogResult saveResult = MessageBox.Show(Common.GetMessage("5010", "Consolidate"), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (saveResult == DialogResult.Yes)
                    {
                        string errorMessage = string.Empty;
                        int ConsolidationID = 0;
                        IndentConsolidation Consolidation = PopulateIndentConsolidationObject(ref errorMessage);
                        if (Consolidation==null)
                            return;
                        if (errorMessage.Trim().Equals(string.Empty) && Consolidation != null)
                        {
                            bool IsConsolidate = Consolidation.Save(Consolidation, (int)Common.IndentStatus.ConsolidationComplete, ref ConsolidationID, ref errorMessage);
                            //Consolidation.Save(Consolidation, (int)Common.IndentStatus.ConsolidationComplete, ref ConsolidationID, ref poNumbers, ref toiNumbers, ref errorMessage);
                            if (IsConsolidate)
                            {
                                MessageBox.Show(Common.GetMessage("INF0055", "Indent", "Consolidate"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Search();
                                ResetForm();
                                ClearItem(); 
                            }
                            else if (errorMessage.StartsWith("MULTIERROR"))
                            {
                                string errMsg = string.Empty;
                                errMsg = Common.GetMultiErrorMessage(errorMessage);

                                MessageBox.Show(errMsg, Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else if (errorMessage.Contains("|"))
                            {
                                string errorCode = errorMessage.Split('|')[0];
                                string itemIds = errorMessage.Split('|')[1];
                                string forLocation = errorMessage.Split('|')[2];
                                string vendorId = errorMessage.Split('|')[3];

                                MessageBox.Show(Common.GetMessage("INF0115", itemIds, forLocation, itemIds), Common.GetMessage("10001").Replace("\\n", Environment.NewLine), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else if (errorMessage.Contains(","))
                            {
                                MessageBox.Show(Common.GetMessage(errorMessage.Split(",".ToCharArray()[0])), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                MessageBox.Show(Common.GetMessage(errorMessage), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show(errorMessage.Trim().ToString(), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(ex.ToString(), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Create Tab Reset Button
        private void btnCreateReset_Click(object sender, EventArgs e)
        {
            try
            {
                IndentNo = string.Empty;
                ResetForm();
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
                if (m_CurrIndent.IndentNo != string.Empty && m_CurrIndent.Status >= (int)Common.IndentStatus.Confirmed)
                {
                    btnPrint.Enabled = false;
                    PrintReport();
                    btnPrint.Enabled = true;
                }
                else
                    MessageBox.Show(Common.GetMessage("INF0101", "Indent", Common.IndentStatus.Confirmed.ToString()), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                btnPrint.Enabled = true;
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Text Box Events

        // When New ItemCode is Selected in TextBox TextCode
        //1. Check Item Is Valid
        //2. If Valid, Set Item Related all Data
        //3. Set Values in Detail Form
        private void txtItemCode_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (txtItemCode.Text.Trim().Equals(string.Empty) || (m_CurrIndentDetail != null && txtItemCode.Text.Trim() == m_CurrIndentDetail.ItemCode))
                    return;
                bool isvalid = CheckValidItem();
                if (isvalid)
                {
                    GetItemDetail();
                    SetDetailValues();
                    txtRequestedQty.Focus();
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("30001"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// TextBox Requested Qty Validation
        /// Should have only Integer values
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtRequestedQty_KeyDown(object sender, KeyEventArgs e)
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
        /// <summary>
        /// TextItemCode F4 Functionality 
        /// For Item Search
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtItemCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {

                if (e.KeyValue == Common.F4KEY && !e.Alt)
                {
                    NameValueCollection _collection = new NameValueCollection();
                    _collection.Add("LocationId", Common.CurrentLocationId.ToString());
                    CoreComponent.Controls.frmSearch _frmSearch = new CoreComponent.Controls.frmSearch(CoreComponent.Controls.SearchTypes.Item, _collection);
                    //CoreComponent.MasterData.BusinessObjects.ItemDetails _Item = (CoreComponent.MasterData.BusinessObjects.ItemDetails)_frmSearch.ReturnObject;
                    _frmSearch.ShowDialog();
                    //_frmSearch.MdiParent = this.MdiParent;
                    CoreComponent.MasterData.BusinessObjects.ItemDetails _Item = (CoreComponent.MasterData.BusinessObjects.ItemDetails)_frmSearch.ReturnObject;
                    if (_Item != null)
                    {
                        txtItemCode.Text = _Item.ItemCode.ToString();
                        txtRequestedQty.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(ex.ToString(), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region GridView Events
        /// <summary>
        /// Show Details of Indent
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvIndent_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
                {

                    if (dgvIndent.Columns[e.ColumnIndex].CellType == typeof(DataGridViewImageCell))
                    {
                        if (Convert.ToString(dgvIndent.Rows[e.RowIndex].Cells[CON_GRID_INEDENTNO].Value) != "")
                        {
                            IndentNo = Convert.ToString(dgvIndent.Rows[e.RowIndex].Cells[CON_GRID_INEDENTNO].Value);
                            ResetForm();
                            ClearItem();
                            tabControlTransaction.SelectTab("tabCreate");
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("30001"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }
        /// <summary>
        /// Show Details of Indent 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvIndent_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (IsViewAvailable && dgvIndent.SelectedRows.Count > 0)
                {
                    IndentNo = Convert.ToString(dgvIndent.SelectedRows[0].Cells[CON_GRID_INEDENTNO].Value);
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

        private void dgvIndentDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                CellEndEdit(e.RowIndex, e.ColumnIndex);
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("30001"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        private void CellEndEdit(int rowIndex, int columnIndex)
        {
            if (columnIndex >= 0 && rowIndex >= 0)
            {
                // Validations
                if ((dgvIndentDetails.Columns[columnIndex].Name == CON_GRID_APPROVEDTOQTY) || (dgvIndentDetails.Columns[columnIndex].Name == CON_GRID_APPROVEDPOQTY) || (dgvIndentDetails.Columns[columnIndex].Name == CON_GRID_APPROVEDHOQTY))
                {
                    if (!Validators.IsNumeric(dgvIndentDetails.Rows[rowIndex].Cells[columnIndex].Value.ToString()))
                    {
                        //errorCreate.SetError(dgvGRNItems, Common.GetMessage("VAL0033", dgvGRNItems.Columns[columnIndex].Name));
                        MessageBox.Show(Common.GetMessage("INF0010", dgvIndentDetails.Columns[columnIndex].Name), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        dgvIndentDetails.Rows[rowIndex].Cells[columnIndex].Value = 0;

                        dgvIndentDetails.Rows[rowIndex].Cells[columnIndex + 1].Selected = false;
                        dgvIndentDetails.Rows[rowIndex].Cells[columnIndex].Selected = true;
                    }
                }
                if (dgvIndentDetails.Columns[columnIndex].Name == CON_GRID_APPROVEDTOQTY)
                {
                    if (Convert.ToDouble(dgvIndentDetails.Rows[rowIndex].Cells[columnIndex].Value) > Convert.ToDouble(dgvIndentDetails.Rows[rowIndex].Cells[CON_GRID_APPROVEDHOQTY].Value))
                    {

                        MessageBox.Show(Common.GetMessage("INF0034", "TO Qty", "Approved HO Qty"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        dgvIndentDetails.Rows[rowIndex].Cells[CON_GRID_APPROVEDTOQTY].Value = 0;
                        dgvIndentDetails.Rows[rowIndex].Cells[CON_GRID_APPROVEDPOQTY].Value = 0;
                       // dgvIndentDetails.Rows[rowIndex].Cells[columnIndex].Selected = true;
                    }
                    else
                    {
                        //dgvIndentDetails.Rows[rowIndex].Cells[CON_GRID_APPROVEDPOQTY].Value = Convert.ToDouble(dgvIndentDetails.Rows[rowIndex].Cells[CON_GRID_APPROVEDHOQTY].Value) - Convert.ToDouble(dgvIndentDetails.Rows[rowIndex].Cells[columnIndex].Value);

                    }
                }
                if (dgvIndentDetails.Columns[columnIndex].Name == CON_GRID_APPROVEDPOQTY)
                {
                    if (Convert.ToDouble(dgvIndentDetails.Rows[rowIndex].Cells[columnIndex].Value) > Convert.ToDouble(dgvIndentDetails.Rows[rowIndex].Cells[CON_GRID_APPROVEDHOQTY].Value))
                    {
                        MessageBox.Show(Common.GetMessage("INF0034", "PO Qty", "Approved HO Qty!"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        dgvIndentDetails.Rows[rowIndex].Cells[CON_GRID_APPROVEDTOQTY].Value = 0;
                        dgvIndentDetails.Rows[rowIndex].Cells[CON_GRID_APPROVEDPOQTY].Value = 0;
                        dgvIndentDetails.CancelEdit();
                        // dgvIndentDetails.Rows[rowIndex].Cells[columnIndex].Selected = true;
                    }
                    else
                    {
                       // dgvIndentDetails.Rows[rowIndex].Cells[CON_GRID_APPROVEDTOQTY].Value = Convert.ToDouble(dgvIndentDetails.Rows[rowIndex].Cells[CON_GRID_APPROVEDHOQTY].Value) - Convert.ToDouble(dgvIndentDetails.Rows[rowIndex].Cells[CON_GRID_APPROVEDPOQTY].Value);
                    }
                }
                SetDetailValues();
            }

        }
        /// <summary>
        /// Remove From Grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvIndentDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                
                if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
                {
                    //if (dgvIndentDetails.SelectedRows.Count == 0 || dgvIndentDetails.SelectedRows[0].Index != e.RowIndex)
                    //{
                    //    dgvIndentDetails.Rows[e.RowIndex].Selected = true;
                    //}
                    if (dgvIndentDetails.Columns[e.ColumnIndex].CellType == typeof(DataGridViewImageCell))
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
        private void dgvIndentDetails_Leave(object sender, EventArgs e)
        {
            try
            {
                if (dgvIndentDetails.CurrentRow != null && dgvIndentDetails.CurrentCell != null)
                    CellEndEdit(dgvIndentDetails.CurrentRow.Index, dgvIndentDetails.CurrentCell.ColumnIndex);
                //dgvIndentDetails.EndEdit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }
        private void dgvIndentDetails_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            try
            {

                if (e.Exception.GetType().Equals(typeof(System.FormatException)))
                {
                    MessageBox.Show(dgvIndentDetails, Common.GetMessage("INF0010", "Qty"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Cancel = true;
                }
                else
                {
                    e.Cancel = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }
        private void dgvIndentDetails_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                
                if (dgvIndentDetails.SelectedRows.Count > 0)
                {
                    errCreateIndent.Clear();
                    m_CurrIndentDetail = m_CurrIndent.IndentDetail[dgvIndentDetails.SelectedRows[0].Index];
                    if (Convert.ToBoolean(dgvIndentDetails.SelectedRows[0].Cells[CON_GRID_ISCONSOLIDATE].Value) == true)
                    {
                        dgvIndentDetails.SelectedRows[0].ReadOnly = true;
                    }
                    else
                    {
                        dgvIndentDetails.SelectedRows[0].ReadOnly = false ;
                    }

                    DataGridViewComboBoxColumn dgvVendor = (DataGridViewComboBoxColumn)dgvIndentDetails.Columns[CON_GRID_VENDOR]; 
                    DataGridViewComboBoxColumn dgvComboDeliveryLocInfo = (DataGridViewComboBoxColumn)dgvIndentDetails.Columns[CON_GRID_DELLOCATION];
                    
                    DataGridViewComboBoxCell dgvcell = (DataGridViewComboBoxCell)dgvIndentDetails.SelectedRows[0].Cells[dgvIndentDetails.Columns[CON_GRID_VENDOR].Index];
                    DataGridViewComboBoxCell dgvDeliveryLocInfoCell = (DataGridViewComboBoxCell)dgvIndentDetails.SelectedRows[0].Cells[dgvIndentDetails.Columns[CON_GRID_DELLOCATION].Index]; 
                    
                    if (dgvVendor.Visible)
                    {
                        List<Item> lst = new List<Item>();
                        Item item = new Item();
                        item.ItemId = m_CurrIndentDetail.ItemID;
                        
                        lst.Add(item);
                        
                        string str = string.Empty;
                        Indent bl = new Indent();
                        dtVendorLocation = bl.GetItemVendor(lst, ref str);
                        DataView dvVendor = new DataView(dtVendorLocation.DefaultView.ToTable(true, "VendorId", "VendorCode"));
                        dgvcell.DataSource = dvVendor;
                        dgvcell.DisplayMember = "VENDORCODE";
                        dgvcell.ValueMember = "VENDORID";
                                           
                        //dgvVendor.DataSource = dvVendor;
                        //dgvVendor.DisplayMember = "VENDORCODE";//"VENDORNAME";
                        //dgvVendor.ValueMember = "VENDORID";
                        dgvVendor.DataPropertyName = CON_GRID_VENDOR;
                        //dgvIndent.ReadOnly = false;
                    }
                    else
                    {
                        dgvVendor.DataSource = null;
                    }

                    if (dgvComboDeliveryLocInfo.Visible && dtVendorLocation != null)
                    {
                        DataView dvItemVendorWarehouse = new DataView(dtVendorLocation.DefaultView.ToTable(true, "LocationId", "LocationName", "DisplayName"));
                        //  dvItemVendorWarehouse.RowFilter = "VendorID= " + intItemId.ToString() + " OR ITEMID=" + Common.INT_DBNULL.ToString();
                        dgvDeliveryLocInfoCell.DataSource = dvItemVendorWarehouse;
                        dgvDeliveryLocInfoCell.DisplayMember = "DisplayName";
                        dgvDeliveryLocInfoCell.ValueMember = "LocationId";
                        //dgvComboDeliveryLocInfo.DataSource = dvItemVendorWarehouse;
                        //dgvComboDeliveryLocInfo.DisplayMember = "DisplayName";
                        //dgvComboDeliveryLocInfo.ValueMember = "LocationId";
                        dgvComboDeliveryLocInfo.DataPropertyName = CON_GRID_DELLOCATION;
                    }
                    else
                        dgvComboDeliveryLocInfo.DataSource = null;

                    
                    SetDetailValues();
                    txtItemCode.Enabled = false;
                    btnAddDetails.Text = "&Edit";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }
        private void dgvIndentDetails_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                dgvIndentDetails.Rows[e.RowIndex].Selected = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }
        private void dgvIndentDetails_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //m_CurrIndentDetail.getFromLocationStock(Convert.ToInt16( dgvIndentDetails.Rows[e.RowIndex].Cells[CON_GRID_TOFROM].Value),Convert.ToInt16(dgvIndentDetails.Rows[e.RowIndex].Cells[CON_GRID_ITEMID].Value));
                //dgvIndentDetails.Rows[e.RowIndex].Cells[CON_GRID_TOFROMSTOK].Value= m_CurrIndentDetail.WhLocationStock;
                if (dgvIndentDetails.CurrentCell.GetType().Name == "DataGridViewComboBoxCell")
                    SendKeys.Send("{F4}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }
        #endregion

        #endregion

        #region Validation

        private Boolean CheckValidItem()
        {
            try
            {
                errCreateIndent.Clear();
                bool Isvalid = false;
                RefreshItemList();
                var query = (from I in ItemList where I.ItemCode.ToUpper().Trim() == txtItemCode.Text.ToUpper().Trim() select I.ItemCode);
                if (query.ToList<string>().Count > 0)
                    Isvalid = true;
                else
                {
                    Isvalid = false;
                    errCreateIndent.SetError(txtItemCode, Common.GetMessage("INF0010", lblItemCode.Text.Trim().Substring(0, lblItemCode.Text.Trim().Length - 2)));
                }
                return Isvalid;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //else
            //{
            //    errCreateIndent.SetError(txt, Common.GetMessage("INF0019", lbl.Text.Trim().Substring(0, lbl.Text.Trim().Length - 2)));
            //    return Isvalid;
            //}
        }

        private bool ValidateLogIN()
        {
            bool isLoggedIn = false;
            if (UserID > 0)
                isLoggedIn = true;
            else
                MessageBox.Show(Common.GetMessage("INF0079"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Warning);

            return isLoggedIn;
        }

        //Validate Add New Item
        private bool ValidateAdd()
        {
            bool Isvalid = true;
            errCreateIndent.Clear();
            // ItemCode Blank
            bool isTextBoxEmpty = Validators.CheckForEmptyString(txtItemCode.Text.Trim().Length);
            if (isTextBoxEmpty)
            {
                errCreateIndent.SetError(txtItemCode, Common.GetMessage("INF0019", lblItemCode.Text.Trim().Substring(0, lblItemCode.Text.Trim().Length - 2)));
                Isvalid = false;
            }
            if (Isvalid)
                Isvalid = CheckValidItem();
            // Requested Qty Valid
            isTextBoxEmpty = Validators.CheckForEmptyString(txtRequestedQty.Text.Trim().Length);
            if (isTextBoxEmpty)
            {
                errCreateIndent.SetError(txtRequestedQty, Common.GetMessage("INF0019", lblRequestedQty.Text.Trim().Substring(0, lblRequestedQty.Text.Trim().Length - 2)));
                Isvalid = false;
            }
            if (Isvalid)
            {
                if (!Validators.IsGreaterThanZero(txtRequestedQty.Text.Trim()))
                {
                    Isvalid = false;
                    errCreateIndent.SetError(txtRequestedQty, Common.GetMessage("VAL0033", lblRequestedQty.Text.Trim().Substring(0, lblRequestedQty.Text.Trim().Length - 2)));
                }
            }

            if(Isvalid)
            {
                if (!Validators.IsValidQuantity(txtRequestedQty.Text.Trim()))
                {
                    Isvalid = false;
                    errCreateIndent.SetError(txtRequestedQty, Common.GetMessage("INF0010", lblRequestedQty.Text.Trim().Substring(0, lblRequestedQty.Text.Trim().Length - 2)));
                }
            }
            
            return Isvalid;
        }
        //Save Update validation
        private void ValidateSave()
        {
            errCreateIndent.SetError(dgvIndentDetails, string.Empty);
            // CHECK FOR BLANK LIST
            
            if (m_CurrIndent.IndentDetail == null || m_CurrIndent.IndentDetail.Count == 0)
            {
                errCreateIndent.SetError(dgvIndentDetails, Common.GetMessage("INF0243", "Item"));
                txtItemCode.Focus();
            }
        }
        
        //Validate Confirmation
        private bool ValidateConfirm()
        {
            foreach (IndentDetail _detail in m_CurrIndent.IndentDetail)
            {
                if (_detail.RequestedQty > 0)
                {
                    return true;
                }
            }
            return false;
        }

        private void ValidatedAddPOTO()
        {
            errCreateIndent.SetError(dgvIndentDetails, string.Empty);
            // CHECK FOR BLANK LIST
            if (m_CurrIndent.IndentDetail == null || m_CurrIndent.IndentDetail.Count == 0)
            {
                errCreateIndent.SetError(dgvIndentDetails, Common.GetMessage("INF0001", "Item Details"));
            }
            else
            {   //Check For Items Which are not used in consolidation
                var query = from q in m_CurrIndent.IndentDetail where q.Status <= 0 select q;
                if (query == null || query.ToList().Count == 0)
                {
                    errCreateIndent.SetError(dgvIndentDetails, Common.GetMessage("INF0203"));
                    return;
                }
                // CHECK FOR PO AND TO QTY SUM GREATER THAN HO QTY
                // CHECK From, PO and To Qty Blank in case of po and To Button
                else
                {
                    foreach (IndentDetail _detail in m_CurrIndent.IndentDetail)
                    {
                        if (_detail.Status <= 0)
                        {
                            /*if ((_detail.ApprovedPOQty + _detail.ApprovedTOQty) > _detail.ApprovedHOQty)
                            {
                                errCreateIndent.SetError(dgvIndentDetails, Common.GetMessage("INF0056"));
                                break;
                            }
                            if ((_detail.TOFromLocationID == 0 || _detail.TOFromLocationID == -1) && _detail.ApprovedTOQty > 0)
                            {
                                errCreateIndent.SetError(dgvIndentDetails, Common.GetMessage("INF0001", " TO From Location"));
                                break;
                            }
                            if ((_detail.TOFromLocationID != 0 || _detail.TOFromLocationID != -1) && (_detail.ApprovedTOQty <= 0 && _detail.ApprovedPOQty <= 0))
                            {
                                errCreateIndent.SetError(dgvIndentDetails, Common.GetMessage("INF0056", " TO From Location"));
                                break;
                            }
                            if ((_detail.Vendor == 0 || _detail.Vendor == -1) && _detail.ApprovedPOQty > 0)
                            {
                                errCreateIndent.SetError(dgvIndentDetails, Common.GetMessage("INF0001", " Vendor Location"));
                                break;
                            }
                            if ((_detail.DelLocation == 0 || _detail.DelLocation == -1) && _detail.ApprovedPOQty > 0)
                            {
                                errCreateIndent.SetError(dgvIndentDetails, Common.GetMessage("INF0001", " Delivery Location"));
                                break;
                            }*/
                        }
                    }
                }
            }
        }

        //Validate Approve Button
        // Atleast one Approved Qty should be greater than 0
        private bool ValidatedApprove()
        {
            foreach (IndentDetail _detail in m_CurrIndent.IndentDetail)
            {
                if (_detail.ApprovedHOQty != 0)
                {
                    return true;
                }
            }
            return false;

        }

        private bool ValidateMoreApprovedQty()
        {
            foreach (IndentDetail _detail in m_CurrIndent.IndentDetail)
            {
                if (_detail.RequestedQty < _detail.ApprovedHOQty)
                {
                    return false ;
                }
            }
            return true ;
        }

        private bool ValidateLessFromLocStockQty()
        {
            foreach (IndentDetail _detail in m_CurrIndent.IndentDetail)
            {
                if (_detail.ToFromLocationStock < _detail.ApprovedHOQty)
                {
                    return false;
                }
            }
            return true;
        }
        private void ValidateSearch()
        {
            errorSearch.SetError(dtpFromDate, string.Empty);
            if (dtpFromDate.Checked == true && dtpToDate.Checked == true)
            {
                int days = DateTime.Compare(dtpFromDate.Value, dtpToDate.Value);
                if (days == 1)
                {
                    errorSearch.SetError(dtpFromDate, Common.GetMessage("INF0034", "From Date", "To Date"));
                }
            }
        }
        // Generate Error String
        private StringBuilder GenerateCreateError()
        {
            StringBuilder sbError = new StringBuilder();
            bool isFocus = false;
            if (errCreateIndent.GetError(txtItemCode).Trim().Length > 0)
            {
                sbError.Append(errCreateIndent.GetError(txtItemCode));
                sbError.AppendLine();
                if (!isFocus)
                {
                    isFocus = true;
                    txtItemCode.Focus();
                }
            }
            if (errCreateIndent.GetError(txtRequestedQty).Trim().Length > 0)
            {
                sbError.Append(errCreateIndent.GetError(txtRequestedQty));
                sbError.AppendLine();
                if (!isFocus)
                {
                    isFocus = true;
                    txtRequestedQty.Focus();
                }
            }
            sbError = Common.ReturnErrorMessage(sbError);
            return sbError;
        }
        // Generate Error String
        private StringBuilder GenerateDataGridViewError()
        {
            StringBuilder sbError = new StringBuilder();
            if (errCreateIndent.GetError(dgvIndentDetails).Trim().Length > 0)
            {
                sbError.Append(errCreateIndent.GetError(dgvIndentDetails));
                sbError.AppendLine();
                dgvIndentDetails.Focus();
            }
            sbError = Common.ReturnErrorMessage(sbError);
            return sbError;
        }

        private StringBuilder GenerateSearchError()
        {
            StringBuilder sbError = new StringBuilder();
            if (errorSearch.GetError(dtpFromDate).Trim().Length > 0)
            {
                sbError.Append(errorSearch.GetError(dtpFromDate));
                sbError.AppendLine();
                dtpFromDate.Focus();
            }
            sbError = Common.ReturnErrorMessage(sbError);
            return sbError;
        }


        #endregion
                

        private void txtItemCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetterOrDigit(e.KeyChar) || e.KeyChar == '\b'))
            {
                e.Handled = true;
            }
        }

        
        private void dgvIndentDetails_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is  TextBox)
            {
                TextBox txtEdit = (TextBox)e.Control;
                txtEdit.KeyPress += txtEdit_KeyPress;
            }
            
        }

        private void txtEdit_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (dgvIndentDetails.CurrentCell.ColumnIndex != dgvIndentDetails.Columns[CON_GRID_APPROVEDHOQTY].Index &&
                dgvIndentDetails.CurrentCell.ColumnIndex != dgvIndentDetails.Columns[CON_GRID_APPROVEDTOQTY].Index &&
                dgvIndentDetails.CurrentCell.ColumnIndex != dgvIndentDetails.Columns[CON_GRID_APPROVEDPOQTY].Index)
                return;
            
            var backKeyChr = char.ConvertFromUtf32((int)Keys.Back);
            int result;
            if (int.TryParse(e.KeyChar.ToString(), out result) || e.KeyChar == (char)Keys.Back)
            {
                e.Handled = false;//if numeric display
                return;
            }
            else
            {
                e.Handled = true; //if non numeric don't display
            }
        }

        private void cmbIndentFromLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
           if(cmbIndentFromLocation.SelectedIndex > -1)
           {
               for(int rCount=0; rCount <= dgvIndentDetails.Rows.Count -1;rCount ++)
               {
                   if (m_CurrIndentDetail != null)
                   {
                       dgvIndentDetails.Rows[rCount].Cells[CON_GRID_TOFROM].Value = Convert.ToString(cmbIndentFromLocation.SelectedValue);     
                       m_CurrIndentDetail.getFromLocationStock(
                                                                Convert.ToInt32(  cmbIndentFromLocation.SelectedValue),
                                                                Convert.ToInt32( dgvIndentDetails.Rows[rCount].Cells[CON_GRID_ITEMID].Value) 
                                                              );
                       dgvIndentDetails.Rows[rCount].Cells[CON_GRID_TOFROMSTOK].Value = m_CurrIndentDetail.WhLocationStock;
                   }
                }
           }
        }
    }
}
