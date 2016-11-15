using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CoreComponent.UI;
using CoreComponent.BusinessObjects;
using CoreComponent.Core.BusinessObjects;
using PurchaseComponent.BusinessObjects;
using AuthenticationComponent.BusinessObjects; 

namespace PurchaseComponent.UI
{
    public partial class frmIndentConsolidation : CoreComponent.UI.MultiTabTemplate
    {
        #region Variables

        private const string CON_DS_CONSOLIDATION_DATA = "DS_CONSOLIDATION_DATA";
        private const string CON_DT_ITEM_DATA = "DT_ITEM_DATA";
        private const string CON_DT_ALLOCATION_DATA = "DT_ALLOCATION_DATA";
        private const string CON_DT_ITEMVENDOR_DATA = "DT_ITEMVENDOR_DATA";
        private const string CON_DT_WAREHOUSE_DATA = "DT_WAREHOUSE_DATA";
        private const string CON_REL_ITEMLOCATIONALLOCATION = "REL_ITEMLOCATIONALLOCATION";

               
        private const string CON_COL_CONSOLIDATIONID = "ConsolidationId";
        private const string CON_COL_HEADERID = "HeaderId";
        private const string CON_COL_ITEMID = "ItemId";
        private const string CON_COL_HLINENO = "HLineNo";
        private const string CON_COL_INDENTNO = "IndentNo";
        private const string CON_COL_APPROVEDQTY = "ApprovedQty";
        private const string CON_COL_TRANSFERLOCATION = "ForLocationId";
        private const string CON_COL_TRANSFERFORLOCATION = "ForLocation";
        private const string CON_COL_PODETAILID = "PODetailId";
        private const string CON_COL_TODETAILID = "TOIDetailId";
        private const string CON_COL_DLINENO = "DLineNo";
        private const string CON_COL_TOIQTY = "TOIQty";
        private const string CON_COL_POQTY = "POQty";
        private const string CON_COL_DELIVERYLOCATIONID = "DeliveryLocationId";
        private const string CON_COL_TRANSERFROMLOC = "TransferFromLocationId";
        private const string CON_REL_ITEMLOCATIONALLOCATIONSTOCK = "TransferFromLocationStock";
        private const string CON_COL_ISCOMPOSITE = "IsComposite";
        private const string CON_COL_ROWINDICATOR = "RowIndicator";
        private const string CON_COL_WAREHOUSEQTYSUM = "WarehouseQtySum";

        private const string CON_COL_VENDORID = "VendorId";
        private const string CON_COL_WAREHOUSE_AVL_SUFFIX = " Avl Qty";
        private const string CON_COL_WAREHOUSE_ACT_SUFFIX = " Act Qty";
        private const string CON_VAR_DEFAULT_TOIQTY = "0.0000";

        private const string CON_COL_SEARCHVIEWCOLUMN = "View";
        private const string CON_COL_CONSOLIDATIONNO = "ConsolidationNo";
        private const string CON_COL_STATUS = "Status";
        private const string CON_COL_WAREHOUSEITEMCODE = "WarehouseItemCode";


        private int m_intCurrentItemId = Common.INT_DBNULL;

        private string m_strErrorMessage = string.Empty;
        private int m_intConsolidationId = Common.INT_DBNULL;
        private IndentDetail m_IndentDetail = new IndentDetail();
        private int m_currentSearchOperationState = (int)SearchOperationState.SearchCurrentIndent;

        private CurrentOperationState enumCurrentOperationState = 0;
        

        //This variable would be used between cellvalidating and cellvalidated event of the grid
        //to add last quantity subtracted from the available warehouse quantity 
        //private double m_dblTmpLastTOIQty;

        //private double m_dblTmpLastPOQty;

        //This variable would be used between cellvalidating and cellvalidated event of the grid
        //to add last quantity subtracted from the available warehouse quantity for the last warehouse
        //private string m_strTmpLastWarehouse;

        //DataSet containing Item details and there allocation detail for the current consolidation
        DataSet dsConsolidationItemAllocation;

        //DataTable containing Item details Select for the current consolidation
        DataTable dtItemDetail;
        DataTable dtItemDetail_DUMMY;

        //DataTable containing Allocation detail for the current consolidation
        DataTable dtItemLocationAllocationDetail;

        //DataTable containing Item Vendor Data 
        //DataTable dtItemVendor;

        DataView dvItemVendorWarehouse;

        //DataTable containing Item Vendor Data for all the items and vendor combinations
        //DataTable dtItemVendorWarehouse;

        //DataTable containing Available Warehouse(s) against requested item(s)
        DataTable dtAvailItemWarehouses;

        //DataTable to get warehouse information
        DataTable dtWarehouseData;

        //List of approved items on which consolidation needs to be started
        List<Item> lstItemRequested;

       

        //List of current items under the process of consolidation
        //List<Item> lstNewItemUnderConsolidation;
        IndentConsolidation m_Consolidation;

        //----------------------------
         //List of final items under the process of consolidation
        List<Item> lstItemUnderConsolidation;
        //DataTable dtConsolidationDetail;
        DataTable dtHeader;
        DataTable dtDetail;
        List<Item> lstApprovedItems;

        private int intUserId = Authenticate.LoggedInUser.UserId;
        private string strUserName = Authenticate.LoggedInUser.UserName;
        private string strLocationCode = Common.LocationCode ;

        private Boolean IsModuleAvailable = false;
        private Boolean IsSaveAvailable = false;
        private Boolean IsSearchAvailable = false;
        private Boolean IsConfirmAvailable = false;
        private Boolean IsCancelAvailable = false;



        //This variable would be used within the events outside Item-Allocation grid events so that
        //item-allocation grid could be fired:- Save/consolidate/Consolidate Selected etc
        //private Boolean IsValidTOI = true;

        //This variable would be used within the events outside Item-Allocation grid events so that
        //This would used from cancel and reset events. This would also avoid dirty row scenarios
        //private Boolean IsGridValidationNeeded = true;

        //This variable would be used to avoid setting datasource to the item grid multiple time when
        //binding the grid first time consolidation buttons
        private Boolean IsBoundFirstTime = false;

        private CurrencyManager m_bindingMgr;
        private bool IsgridEditable = true;
        DataTable dtVendorLocation = null;
        #endregion Variables

        #region Enum
        enum CurrentOperationState
        {
            New=1,
            Cancelled = 2,
            UnderConsolidation = 5,
            ConsolidationComplete = 6,
            NewUnderConsolidation = 10, //New record but in process
            NoItemAvailableForIndentation = 11,

        };

        public enum SearchOperationState
        {
            SearchAll = 1,
            SearchSingle = 2,
            SearchCurrentIndent = 3
        }

        public enum RowIndicator
        {
            Default = -1,
            Invalid = 0,
            Valid = 1
        }
        #endregion

        #region CTOR
        private void InitializeFormElements()
        {
            #region Initialise the grids
            dgvIndentConsolidationGrid.AutoGenerateColumns = false;
            dgvIndentConsolidationGrid =
                Common.GetDataGridViewColumns(dgvIndentConsolidationGrid,
                Environment.CurrentDirectory + "\\App_Data\\GridViewDefinition.xml");


            //Initialise Grid
            dgvApprovedItems.AutoGenerateColumns = false;

            //In case the binding-class exposes its extra members to the grid
            //for which definition does not exist in GridViewDefinition.xml,
            //the below line would clear the same and ensure that only defined columns exist in grid.
            dgvApprovedItems.Columns.Clear();

            dgvApprovedItems =
                Common.GetDataGridViewColumns(dgvApprovedItems,
                Environment.CurrentDirectory + "\\App_Data\\GridViewDefinition.xml");


            dgvItemWarehouseStatus.AutoGenerateColumns = true;
            
            dgvItemWarehouseStatus =
                Common.GetDataGridViewColumns(dgvItemWarehouseStatus,
                Environment.CurrentDirectory + "\\App_Data\\GridViewDefinition.xml");

            dgvItemLocationAllocation =
                Common.GetDataGridViewColumns(dgvItemLocationAllocation,
                Environment.CurrentDirectory + "\\App_Data\\GridViewDefinition.xml");

            dgvItemLocationAllocation.AutoGenerateColumns = false;
            #endregion

            DataTable dtStatus = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter("INDENTCONSTATUS", 0, 0, 0));
            if (dtStatus != null)
            {
                DataRow[] dr= dtStatus.Select(Common.KEYCODE1 + "='" +(int)Common.IndentConsolidationState.Cancelled+"'");
                if(dr!=null)
                    dtStatus.Rows.Remove(dr[0]);
                cmbStatus.DataSource = dtStatus;
                cmbStatus.DisplayMember = Common.KEYVALUE1;
                cmbStatus.ValueMember = Common.KEYCODE1;
            }

            DataTable dtSource = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter("INDENTSOURCE", 0, 0, 0));
            if (dtSource != null)
            {
                cmbSource.DataSource = dtSource;
                cmbSource.DisplayMember = Common.KEYVALUE1;
                cmbSource.ValueMember = Common.KEYCODE1;
            }

            dsConsolidationItemAllocation = new DataSet(CON_DS_CONSOLIDATION_DATA);
            lstItemRequested = new List<Item>();
            lstItemUnderConsolidation = new List<Item>();

        }
        public frmIndentConsolidation()
        {
            try
            {
                this.label1.Text = "Indent Consolidation";
                IsModuleAvailable = Authenticate.IsModuleAccessible(strUserName, strLocationCode, IndentConsolidationBL.MODULE_CODE);
                if (!IsModuleAvailable)
                {
                    MessageBox.Show(Common.GetMessage("INF0053"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                IsSaveAvailable = Authenticate.IsFunctionAccessible(strUserName, strLocationCode, IndentConsolidationBL.MODULE_CODE, IndentConsolidationBL.FUNCTION_CODE_SAVE);
                IsCancelAvailable = Authenticate.IsFunctionAccessible(strUserName, strLocationCode, IndentConsolidationBL.MODULE_CODE, IndentConsolidationBL.FUNCTION_CODE_CANCEL);
                IsConfirmAvailable = Authenticate.IsFunctionAccessible(strUserName, strLocationCode, IndentConsolidationBL.MODULE_CODE, IndentConsolidationBL.FUNCTION_CODE_CONFIRM);
                IsSearchAvailable = Authenticate.IsFunctionAccessible(strUserName, strLocationCode, IndentConsolidationBL.MODULE_CODE, IndentConsolidationBL.FUNCTION_CODE_SEARCH);

                InitializeComponent();
                (new ModifyTabControl()).RemoveTabPageArray(tabMultiTabTemplate, 2);
                InitializeFormElements();

                ResetForm(-1);
                // PopulateForm(Common.INT_DBNULL);
                // EnableDisableControls(enumCurrentOperationState);           
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Methods  
     
        private void ResetForm(int ConsolidateID)
        {            
            dgvApprovedItems.DataSource = null;
            dgvItemLocationAllocation.DataSource = null;
            dgvItemWarehouseStatus.DataSource = null;
            lstItemUnderConsolidation = null;
            IndentConsolidationBL bl = new IndentConsolidationBL();
           
            if (ConsolidateID == -1)
            {                            
                IndentConsolidation consolidation = bl.GetConsolidationDetail(-1, true);
                if (consolidation != null && consolidation.ConsolidationId > 0)
                {
                    ConsolidateID = consolidation.ConsolidationId;
                }
            }
            if (ConsolidateID > 0)
            {
                DataSet ds = bl.GetCompleteConsolidation(ConsolidateID);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    m_Consolidation = IndentConsolidationBL.GetIndentConsolidationObject(ds.Tables[0].Rows[0]);
                    if (ds.Tables[1] != null)
                        dtHeader = ds.Tables[1];
                    if (ds.Tables[2] != null)
                        dtDetail = ds.Tables[2];
                }
            }
            else
            {
                m_Consolidation = new IndentConsolidation();
                m_Consolidation.ConsolidationId = Common.INT_DBNULL;
                m_Consolidation.Status = (int)CurrentOperationState.New;
                m_Consolidation.StatusCap = CurrentOperationState.New.ToString();
                m_Consolidation.Source = (int)Common.IndentConsolidationSource.Auto;
                m_Consolidation.SourceCap = Common.IndentConsolidationSource.Auto.ToString();
                dtHeader = null;
                dtDetail = null;
            }
            if (m_Consolidation.Status == (int)CurrentOperationState.UnderConsolidation)
            {
                bool exists = false;
                lstItemUnderConsolidation = GetUnderConsolidationItems(ref exists);
                dtWarehouseData = GetWareHouseData(lstItemUnderConsolidation);
                dtHeader = dtWarehouseData;
            }            
            lstApprovedItems = GetApprovedItems();        
            LoadForm();
            
        }
       
        private void SetButtonProperty()
        {
            btnConsolidateAll.Enabled = false;
            btnConsolidateSelected.Enabled = false;
            btnIndentSave.Enabled = false;
            btnIndentCancel.Enabled = false;
            btnIndentConsolidate.Enabled = false;
            btnIndentReset.Enabled = true;
            switch (m_Consolidation.Status)
            {
                case (int)CurrentOperationState.New:
                    //pnlApprovedItems.Enabled = true;
                    //pnlIndentConsolidationDetail.Enabled = false;
                    //pnlConsolidateProcess.Enabled = false;
                    //pnlControlButtons.Enabled = false;                    
                    btnConsolidateAll.Enabled = true;
                    btnConsolidateSelected.Enabled = true;
                    //btnIndentSave.Enabled = IsSaveAvailable;
                    //btnIndentCancel.Enabled = IsCancelAvailable;
                    //btnIndentConsolidate.Enabled = IsConfirmAvailable;
                    btnIndentReset.Enabled = true;                    
                    break;
                case (int)CurrentOperationState.UnderConsolidation:
                    //pnlApprovedItems.Enabled = false;
                    //pnlIndentConsolidationDetail.Enabled = true;
                    //pnlConsolidateProcess.Enabled = true;
                    //pnlControlButtons.Enabled = true;
                    btnIndentSave.Enabled = IsSaveAvailable;
                    btnIndentCancel.Enabled = IsCancelAvailable;
                    btnIndentConsolidate.Enabled = IsConfirmAvailable;
                    btnIndentReset.Enabled = true;
                    break;
                case (int)CurrentOperationState.NoItemAvailableForIndentation:
                    //pnlApprovedItems.Enabled = false;
                    //pnlConsolidateProcess.Enabled = false;
                    //pnlIndentConsolidationDetail.Enabled = false;
                   // pnlControlButtons.Enabled = false;
                    btnIndentReset.Enabled = true;
                    break;
                case (int)CurrentOperationState.Cancelled:
                  
                    btnIndentReset.Enabled = true;
                    break;
                case (int)CurrentOperationState.NewUnderConsolidation:
                 
                    btnIndentReset.Enabled = true;
                    btnConsolidateSelected.Enabled = true;
                    btnConsolidateAll.Enabled = true;
                    btnIndentSave.Enabled = IsSaveAvailable;
                    btnIndentCancel.Enabled = IsCancelAvailable;
                    btnIndentConsolidate.Enabled = IsConfirmAvailable;
                    break;
                case (int)CurrentOperationState.ConsolidationComplete:
                    btnIndentReset.Enabled = true;
                    break;
                default:
                    break;

            }       
       
        }

        private void ConsolidateSelect()
        {
            bool IsSelected = false;
            if (m_Consolidation != null && m_Consolidation.Status==(int)CurrentOperationState.New)
                m_Consolidation.Status = (int)CurrentOperationState.NewUnderConsolidation;
            List<Item> itemList = new List<Item>();
            for(int i=0;i<dgvApprovedItems.Rows.Count;i++)
            {
                DataGridViewCheckBoxCell dgvCell = (DataGridViewCheckBoxCell)dgvApprovedItems.Rows[i].Cells["ItemCheckBox"];
                if (Convert.ToBoolean(dgvCell.Value))
                {
                    IsSelected = true;
                    if (lstItemUnderConsolidation == null)
                        lstItemUnderConsolidation = new List<Item>();
                    itemList.Add(lstApprovedItems[i]);
                    lstItemUnderConsolidation.Add(lstApprovedItems[i]);                    
                    SetItemDetailTable(lstApprovedItems[i].ItemId);                                    

                }                
            }
            if (IsSelected)
            {
                foreach (Item item in itemList)
                    lstApprovedItems.Remove(item);
                dtHeader = GetWareHouseData(lstItemUnderConsolidation);

                IsBoundFirstTime = false;
                LoadForm();
            }
            else
            {
                MessageBox.Show(Common.GetMessage("VAL0046"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
       
        private void LoadForm()
        {            
            if (m_Consolidation != null)
            {
                SetMasterDetail();             

                if (m_Consolidation.Status == (int)CurrentOperationState.ConsolidationComplete)
                {
                    ShowComboFields(false);
                    IsgridEditable = false;
                }
                else
                {
                    BindApprovedItemGrid();
                    ShowComboFields(true);
                    IsgridEditable = true;
                }
                SetHeaderGrid(dtHeader);
                SetButtonProperty();
            }
        }
       
        private void ShowComboFields(bool Show)
        {
            dgvItemLocationAllocation.Columns["TransferFromLocationValue"].Visible = !Show;
            dgvItemLocationAllocation.Columns["VendorValue"].Visible = !Show;
            dgvItemLocationAllocation.Columns["DeliveryLocationValue"].Visible = !Show;
            dgvItemLocationAllocation.Columns["PONumber"].Visible = !Show;
            dgvItemLocationAllocation.Columns["TOINumber"].Visible = !Show;

            dgvItemLocationAllocation.Columns["TransferFromLocationId"].Visible = Show;
            dgvItemLocationAllocation.Columns["TransferFromLocationStock"].Visible =Show;
            //dgvItemLocationAllocation.Columns["TransferFromLocationStock"].ReadOnly = true;
            dgvItemLocationAllocation.Columns["VendorId"].Visible = Show;
            dgvItemLocationAllocation.Columns["DeliveryLocationId"].Visible = Show;
            dgvItemLocationAllocation.Columns[CON_COL_POQTY].ReadOnly = false;
            dgvItemLocationAllocation.Columns[CON_COL_TOIQTY].ReadOnly = false;
            dgvItemLocationAllocation.Columns["IsFormC"].ReadOnly = false;
            (dgvItemLocationAllocation.Columns[CON_COL_POQTY] as DataGridViewTextBoxColumn).MaxInputLength = 8;
            (dgvItemLocationAllocation.Columns[CON_COL_TOIQTY] as DataGridViewTextBoxColumn).MaxInputLength = 8;
            if (!Show)
            {
                dgvItemLocationAllocation.Columns["TransferFromLocationId"].DataPropertyName = null;
                //dgvItemLocationAllocation.Columns["TransferFromLocationStock"].DataPropertyName = null;
                dgvItemLocationAllocation.Columns["VendorId"].DataPropertyName = null;
                dgvItemLocationAllocation.Columns["DeliveryLocationId"].DataPropertyName = null;

                ((DataGridViewComboBoxColumn)dgvItemLocationAllocation.Columns["TransferFromLocationId"]).DataSource = null;
                ((DataGridViewComboBoxColumn)dgvItemLocationAllocation.Columns["VendorId"]).DataSource = null;
                ((DataGridViewComboBoxColumn)dgvItemLocationAllocation.Columns["DeliveryLocationId"]).DataSource = null;
                dgvItemLocationAllocation.Columns[CON_COL_POQTY].ReadOnly=true;
                dgvItemLocationAllocation.Columns[CON_COL_TOIQTY].ReadOnly = true;
                dgvItemLocationAllocation.Columns["IsFormC"].ReadOnly = true;
                
            }
        }
        
        private void SetMasterDetail()
        {
            if (m_Consolidation == null || m_Consolidation.Status == (int)CurrentOperationState.New)
            {
                txtConsolidationNoVal.Text = string.Empty;
                txtCreatedDateVal.Text = string.Empty;
                txtLastModifiedVal.Text = string.Empty;
                txtSourceTxt.Text = string.Empty;
                txtStatusVal.Text = string.Empty;
            }
            else
            {
                txtConsolidationNoVal.Text = m_Consolidation.ConsolidationId == -1 ? string.Empty : m_Consolidation.ConsolidationId.ToString();
                txtCreatedDateVal.Text = m_Consolidation.CreatedDate == null ? string.Empty : m_Consolidation.DisplayCreatedDate;
                txtLastModifiedVal.Text = m_Consolidation.ModifiedDate == null ? string.Empty : m_Consolidation.DisplayModifiedDate;
                txtSourceTxt.Text = m_Consolidation.SourceCap;
                txtStatusVal.Text = m_Consolidation.StatusCap;
            }
        }

        private void SetHeaderGrid(DataTable dt)
        {
            dgvItemWarehouseStatus.DataSource=null;
            if (dt != null && dt.Rows.Count > 0)
            {
                dgvItemWarehouseStatus.DataSource = dt;
            }
            if (dgvItemWarehouseStatus.Columns[CON_COL_ROWINDICATOR] != null)
                dgvItemWarehouseStatus.Columns[CON_COL_ROWINDICATOR].Visible = false;
            if (dgvItemWarehouseStatus.Columns[CON_COL_ITEMID] != null)
                dgvItemWarehouseStatus.Columns[CON_COL_ITEMID].Visible = false;
            if (dgvItemWarehouseStatus.Columns.Contains("WarehouseItemName"))
                dgvItemWarehouseStatus.AutoResizeColumn(dgvItemWarehouseStatus.Columns["WarehouseItemName"].Index, DataGridViewAutoSizeColumnMode.DisplayedCells);
            if (dgvItemWarehouseStatus.Columns.Contains("ItemName"))
                dgvItemWarehouseStatus.AutoResizeColumn(dgvItemWarehouseStatus.Columns["ItemName"].Index, DataGridViewAutoSizeColumnMode.DisplayedCells);
        }
        
        private List<Item> GetApprovedItems()
        {
            IndentConsolidationBL item = new IndentConsolidationBL();
            List<Item> listApprovedItems = item.GetAppovedItemsForConsolidation();
            return listApprovedItems;
        }
       
        private DataTable GetWareHouseData(List<Item> Items)
        {
            if (dtAvailItemWarehouses == null)
            {
                dtAvailItemWarehouses = new DataTable();
            }

            IndentConsolidationBL objICBL = new IndentConsolidationBL();
            DataTable dt = objICBL.GetItemWarehouse(Items, ref m_strErrorMessage);

            //Store the available warehouses for the item(s) in question
            if (dt != null)
            {
                dtAvailItemWarehouses = dt.Copy();
            }

            dt = CreateCrossTabDataForItemWarehouseQty(dt);
            dtItemDetail_DUMMY = null;
            dtItemDetail_DUMMY = dt.Copy();

            //Reusing the structure and data as returned for the database
            if (dtItemDetail == null)
            {
                //If Item Detail table is not initialized, initialise it and get data
                dtItemDetail = dt.Copy();
                dtItemDetail.TableName = CON_DT_ITEM_DATA;
                dsConsolidationItemAllocation.Tables.Add(dtItemDetail);
                dsConsolidationItemAllocation.Tables[CON_DT_ITEM_DATA].Constraints.Add("PrimaryKey", dsConsolidationItemAllocation.Tables[CON_DT_ITEM_DATA].Columns[CON_COL_ITEMID], true);
            }
            else
            {
                //If Item Detail table is already initialized, just get the newly added data
                dtItemDetail.Merge(dt);
            }
            return dt;
        }
      
        private DataTable GetItemLocationDetails(int ItemID)
        {
            IndentConsolidationBL item = new IndentConsolidationBL();
            DataTable dt = item.GetIndentDetailForItem(ItemID);
            return dt;
        }
        
        private List<Item> GetUnderConsolidationItems(ref bool Exists)
        {
            List<Item> lstApprovedIndentItems = new List<Item>();
            List<Item> lstExistingItems = IndentConsolidationBL.GetUnderConsolidationItems(-1, ref Exists, ref lstApprovedIndentItems);
            return lstExistingItems;
        }
        
        private void BindApprovedItemGrid()
        {            
            dgvApprovedItems.DataSource = null;
            if (lstApprovedItems != null && lstApprovedItems.Count > 0)
            {
                dgvApprovedItems.DataSource = lstApprovedItems;
                m_bindingMgr = (CurrencyManager)this.BindingContext[lstApprovedItems];
                m_bindingMgr.Refresh();
            }
        }
        
        private void ItemSelectionChange()
        {
            if (dgvItemWarehouseStatus.SelectedRows.Count > 0)
            {
                dgvItemLocationAllocation.DataSource = null;
                    //filter Items
                if (dtDetail != null && dtDetail.Rows.Count > 0)
                {
                    DataGridViewTextBoxCell txtItemCode = (DataGridViewTextBoxCell)dgvItemWarehouseStatus.SelectedRows[0].Cells[CON_COL_ITEMID];
                    m_intCurrentItemId = Convert.ToInt32(txtItemCode.Value);
                    DataView dv = dtDetail.DefaultView;
                    dv.RowFilter = "ItemId=" + m_intCurrentItemId;
                    
                  
                    DataGridViewComboBoxColumn dgvVendor = (DataGridViewComboBoxColumn)dgvItemLocationAllocation.Columns[CON_COL_VENDORID];
                    DataGridViewComboBoxColumn dgvComboTransferFromLoc = (DataGridViewComboBoxColumn)dgvItemLocationAllocation.Columns[CON_COL_TRANSERFROMLOC];
                    DataGridViewComboBoxColumn dgvComboDeliveryLocInfo = (DataGridViewComboBoxColumn)dgvItemLocationAllocation.Columns[CON_COL_DELIVERYLOCATIONID];
                    if(dgvVendor.Visible)
                    {                    
                        List<Item> lst = new List<Item>();
                        Item item = new Item();
                        item.ItemId = m_intCurrentItemId;
                        lst.Add(item);

                        string str = string.Empty;
                        IndentConsolidationBL bl = new IndentConsolidationBL();
                        dtVendorLocation = bl.GetItemVendor(lst, ref str);
                        DataView dvVendor = new DataView(dtVendorLocation.DefaultView.ToTable(true, "VendorId", "VendorCode"));
                                                
                        dgvVendor.DataSource = dvVendor;
                        dgvVendor.DisplayMember = "VENDORCODE";//"VENDORNAME";
                        dgvVendor.ValueMember = "VENDORID";
                        dgvVendor.DataPropertyName = CON_COL_VENDORID;
                    }
                    else
                    {
                        dgvVendor.DataSource=null;
                    }
                    if(dgvComboTransferFromLoc.Visible)
                    {
                        DataTable DTWareHouse = Common.ParameterLookup(Common.ParameterType.Locations, new ParameterFilter("Locations", -5, 0, 0));                                                
                        dgvComboTransferFromLoc.DataSource = DTWareHouse;
                        dgvComboTransferFromLoc.DisplayMember = "Name";
                        dgvComboTransferFromLoc.ValueMember = "LocationId";
                        dgvComboTransferFromLoc.DataPropertyName = "TransferFromLocationId";
                    }
                    else
                        dgvComboTransferFromLoc.DataSource=null;
                    
                    if(dgvComboDeliveryLocInfo.Visible && dtVendorLocation!=null)
                    {
                        dvItemVendorWarehouse = new DataView(dtVendorLocation.DefaultView.ToTable(true, "LocationId", "LocationName","DisplayName"));
                        //  dvItemVendorWarehouse.RowFilter = "VendorID= " + intItemId.ToString() + " OR ITEMID=" + Common.INT_DBNULL.ToString();

                        dgvComboDeliveryLocInfo.DataSource = dvItemVendorWarehouse;
                        dgvComboDeliveryLocInfo.DisplayMember = "DisplayName";
                        dgvComboDeliveryLocInfo.ValueMember = "LocationId";
                        dgvComboDeliveryLocInfo.DataPropertyName = CON_COL_DELIVERYLOCATIONID;
                    }
                    else
                        dgvComboDeliveryLocInfo.DataSource=null;

                    dgvItemLocationAllocation.DataSource = dv;
                        // For each Row
                    if (dgvComboTransferFromLoc.Visible)
                    {
                        for (int i = 0; i < dgvItemLocationAllocation.Rows.Count; i++)
                        {                            
                            DataGridViewComboBoxCell colTransfer = (DataGridViewComboBoxCell)dgvItemLocationAllocation.Rows[i].Cells[CON_COL_TRANSERFROMLOC];
                            object obj=colTransfer.Value;
                            DataTable dtAll = (DataTable)colTransfer.DataSource;
                            DataTable dt1 = dtAll.Copy();
                            DataRow[] dr = dt1.Select("LocationId=" + dgvItemLocationAllocation.Rows[i].Cells["ForLocationId"].Value.ToString());
                            if (dr != null && dr.Length > 0)
                            {
                                dt1.Rows.Remove(dr[0]);
                            }
                            colTransfer.DataSource = dt1;
                            colTransfer.Value = obj;                           
                        }
                    }
                }           
            }
        }

        private DataTable CreateCrossTabDataForItemWarehouseQty(DataTable dt)
        {
            //Get the distict rows for location Id 
            DataView dvDistictItems = new DataView(dt.DefaultView.ToTable("Location", true, "Name"));

            //Create Item Warehouse Table Structure
            DataTable dtItemWareHouse = new DataTable(CON_DT_ITEM_DATA);
            dtItemWareHouse.Columns.Add("ItemId", typeof(Int32));
            dtItemWareHouse.Columns.Add("ItemCode", typeof(String));
            dtItemWareHouse.Columns.Add("ItemName", typeof(String));
            dtItemWareHouse.Columns.Add("RowIndicator", typeof(Int32));


            //Create Cross Tab Columns
            for (int i = 0; i < dvDistictItems.Count; i++)
            {
                dtItemWareHouse.Columns.Add(dvDistictItems[i].Row["Name"].ToString() + CON_COL_WAREHOUSE_AVL_SUFFIX, typeof(Double));
                dtItemWareHouse.Columns.Add(dvDistictItems[i].Row["Name"].ToString() + CON_COL_WAREHOUSE_ACT_SUFFIX, typeof(Double));
            }

            //Start Populating the Data

            Boolean IsRecordExists = false;

            if (dt.Rows.Count > 0) IsRecordExists = true;

            while (IsRecordExists)
            {
                //Get all the rows for the current item and populate the respective cross 
                //tab column and delete it from the dt data-table afterwards

                DataRow[] dr = dt.Select("ItemId =" + dt.Rows[0]["ItemId"].ToString());

                DataRow drItemWarehouse = dtItemWareHouse.NewRow();

                if (dr.Length > 0)
                {
                    drItemWarehouse["ItemId"] = dr[0]["ItemId"];
                    drItemWarehouse["ItemCode"] = dr[0]["ItemCode"];
                    drItemWarehouse["ItemName"] = dr[0]["ItemName"];
                    drItemWarehouse["RowIndicator"] = Common.INT_DBNULL;
                }

                for (int j = 0; j < dr.Length; j++)
                {
                    for (int k = 0; k < dtItemWareHouse.Columns.Count; k++)
                    {
                        //Identify the current row contains data from which tabbed column 
                        //and populate the tabbed column.
                        if (dtItemWareHouse.Columns[k].ColumnName == dr[j]["Name"].ToString() + CON_COL_WAREHOUSE_AVL_SUFFIX)
                        {
                            drItemWarehouse[k] = dr[j]["AvailableQuantity"];
                            drItemWarehouse[k + 1] = dr[j]["Quantity"];
                            dt.Rows.Remove(dr[j]);
                            break;
                        }
                    }
                }
                dtItemWareHouse.Rows.Add(drItemWarehouse);
                dt.AcceptChanges();
                if (dt.Rows.Count > 0)
                {
                    IsRecordExists = true;
                }
                else
                {
                    IsRecordExists = false;
                }
            }
            return dtItemWareHouse;
        }

        private void SetItemDetailTable(int Itemid)
        {
            DataTable itemTable=GetItemLocationDetails(Itemid);
            if (dtDetail == null)
                dtDetail = itemTable;
            else
            {
                dtDetail.Merge(itemTable);
            }
        }
     
        private bool GetItemLocation()
        {
            bool retVal = true;

            IndentConsolidationBL objICBL = new IndentConsolidationBL();

            //Get Indent Consolidation Search Request
            IndentConsolidationRequest objIndentConsolidationReq = new IndentConsolidationRequest();
            objIndentConsolidationReq.ConsolidationId = m_intConsolidationId;
            //objIndentConsolidationReq.RequestedItems = lstNewItemUnderConsolidation;
            objIndentConsolidationReq.Operation = m_currentSearchOperationState;


            DataSet ds = objICBL.GetIndentConsolidationData(objIndentConsolidationReq, ref m_strErrorMessage);

            DataTable dt = null;
            if (ds.Tables.Count >= 1 && ds.Tables[0].Rows.Count > 0)
            {
                dt = ds.Tables[0];
            }
            else
            {
                MessageBox.Show(Common.GetMessage("VAL0034"), Common.GetMessage("10001"));
                //return;
                retVal = false;
            }

            if (retVal)
            {
                if (ds.Tables.Count == 2 && ds.Tables[1].Rows.Count != 0)
                {
                    //Populate the indent Master Records
                    txtConsolidationNoVal.Text = ds.Tables[1].Rows[0]["ConsolidationId"].ToString();
                    txtSourceTxt.Text = ds.Tables[1].Rows[0]["Status"].ToString();
                    txtStatusVal.Text = ds.Tables[1].Rows[0]["Source"].ToString();
                    txtCreatedDateVal.Text = Convert.ToDateTime(ds.Tables[1].Rows[0]["CreatedDate"]).ToString(Common.DTP_DATE_FORMAT);
                    txtLastModifiedVal.Text = Convert.ToDateTime(ds.Tables[1].Rows[0]["ModifiedDate"]).ToString(Common.DTP_DATE_FORMAT);
                }

                //Reusing the structure and data as returned for the database
                if (dtItemLocationAllocationDetail == null)
                {
                    dtItemLocationAllocationDetail = dt.Copy();
                    dtItemLocationAllocationDetail.TableName = CON_DT_ALLOCATION_DATA;
                    dsConsolidationItemAllocation.Tables.Add(dtItemLocationAllocationDetail);
                    if (dsConsolidationItemAllocation.Tables[CON_DT_ITEM_DATA].Rows.Count ==
                        dsConsolidationItemAllocation.Tables[CON_DT_ALLOCATION_DATA].Rows.Count)
                    {
                        DataRelation dRel = new DataRelation(CON_REL_ITEMLOCATIONALLOCATION,
                                                         dsConsolidationItemAllocation.Tables[CON_DT_ITEM_DATA].Columns[CON_COL_ITEMID],
                                                         dsConsolidationItemAllocation.Tables[CON_DT_ALLOCATION_DATA].Columns[CON_COL_ITEMID]);
                        dsConsolidationItemAllocation.Relations.Add(dRel);
                    }
                }
                else if (dtItemLocationAllocationDetail != null & enumCurrentOperationState == CurrentOperationState.NewUnderConsolidation)
                {
                    //If Item Detail table is already initialized, just get the newly added data
                    dtItemLocationAllocationDetail.Merge(dt);
                }
                else
                {
                    dtItemLocationAllocationDetail = dt.Copy();
                    dtItemLocationAllocationDetail.TableName = CON_DT_ALLOCATION_DATA;
                    //Rebind the table
                    if (dsConsolidationItemAllocation.Relations.Contains(CON_REL_ITEMLOCATIONALLOCATION))
                    {
                        dsConsolidationItemAllocation.Relations.Remove(CON_REL_ITEMLOCATIONALLOCATION);
                    }
                    if (dsConsolidationItemAllocation.Tables[CON_DT_ALLOCATION_DATA].Constraints.Contains(CON_REL_ITEMLOCATIONALLOCATION))
                    {
                        dsConsolidationItemAllocation.Tables[CON_DT_ALLOCATION_DATA].Constraints.Remove(CON_REL_ITEMLOCATIONALLOCATION);
                    }

                    dsConsolidationItemAllocation.Tables.Remove(CON_DT_ALLOCATION_DATA);


                    dsConsolidationItemAllocation.Tables.Add(dtItemLocationAllocationDetail);

                    if (dsConsolidationItemAllocation.Tables[CON_DT_ITEM_DATA].Rows.Count ==
                        dsConsolidationItemAllocation.Tables[CON_DT_ALLOCATION_DATA].Rows.Count)
                    {
                        DataRelation dRel = new DataRelation(CON_REL_ITEMLOCATIONALLOCATION,
                                                         dsConsolidationItemAllocation.Tables[CON_DT_ITEM_DATA].Columns[CON_COL_ITEMID],
                                                         dsConsolidationItemAllocation.Tables[CON_DT_ALLOCATION_DATA].Columns[CON_COL_ITEMID]);
                        dsConsolidationItemAllocation.Relations.Add(dRel);
                    }
                }
                //SetCurrentConsolidationId();
            }

            return retVal;
        }
        
        private void CellEdit(int col, int row)
        {
            if (IsgridEditable)
            {
                if (col >= 0 && row >= 0)
                {

                    string m_name = dgvItemLocationAllocation.Columns[col].Name;
                    object m_value = dgvItemLocationAllocation.Rows[row].Cells[col].Value;
                    if (m_name == CON_COL_TOIQTY)
                    {
                        bool isvalid=Validators.IsInt32(m_value.ToString());
                        if (!isvalid)
                        {
                            //dgvItemLocationAllocation.Rows[row].Cells[col].Value = 0;
                           // MessageBox.Show(Common.GetMessage("INF0010", m_name));                            
                            return;
                        }
                        
                        Int32 approveQty = (Int32)dgvItemLocationAllocation.Rows[row].Cells[CON_COL_APPROVEDQTY].Value;
                        if (Convert.ToInt32(m_value) > approveQty)
                        {
                            MessageBox.Show("TOI Qty can not be greater than Approve Qty");
                            //dgvItemLocationAllocation.Rows[row].Cells[CON_COL_TOIQTY].Value = m_value;
                            dgvItemLocationAllocation.Rows[row].Cells[CON_COL_TOIQTY].Value = 0;
                            return;
                        }
                        else
                        {
                           // dgvItemLocationAllocation.Rows[row].Cells[CON_COL_POQTY].Value = approveQty - Convert.ToDecimal(m_value);
                        }
                    }
                    if (m_name == CON_COL_POQTY)
                    {
                        bool isvalid = Validators.IsInt32(m_value.ToString());
                        if (!isvalid)
                        {
                          //  MessageBox.Show(Common.GetMessage("INF0010",m_name));
                           // dgvItemLocationAllocation.Rows[row].Cells[col].Value = 0;
                            return;
                        }
                        Int32 approveQty = (Int32)dgvItemLocationAllocation.Rows[row].Cells[CON_COL_APPROVEDQTY].Value;
                        if (Convert.ToInt32(m_value) > approveQty)
                        {
                            MessageBox.Show("PO Qty can not be greater than Approve Qty");
                            //dgvItemLocationAllocation.Rows[row].Cells[CON_COL_POQTY].Value = m_value;
                            dgvItemLocationAllocation.Rows[row].Cells[CON_COL_POQTY].Value =0;
                            return;
                        }
                        else
                        {
                           // dgvItemLocationAllocation.Rows[row].Cells[CON_COL_TOIQTY].Value = approveQty - Convert.ToDecimal(m_value);
                        }
                    }
                    if (m_name == CON_COL_VENDORID)
                    {
                        DataGridViewComboBoxCell cmb = (DataGridViewComboBoxCell)dgvItemLocationAllocation.Rows[row].Cells[CON_COL_DELIVERYLOCATIONID];
                        //Rebind Delivery Location
                        DestinationFill(ref cmb, (int)m_value);
                    }
                }
            }
        }
        
        private void DestinationFill(ref DataGridViewComboBoxCell cmb, int vendorID)
        {
            DataTable dt = dtVendorLocation.Copy();
            DataView dv = new DataView(dt.DefaultView.ToTable(true, "ItemId", "VendorId", "LocationId", "LocationName", "DisplayName"));
            dv.RowFilter = "VendorID=" + vendorID + " OR VendorID=" + Common.INT_DBNULL.ToString();
            if (dv != null)
            {
                cmb.DataSource = dv;
                cmb.DisplayMember = "DisplayName";
                cmb.ValueMember = "LocationId";
                //cmb.DataPropertyName = CON_COL_DELIVERYLOCATIONID;
            }
            cmb.Value = -1;

            //dvItemVendorWarehouse = new DataView(dtVendorLocation.DefaultView.ToTable(true, "ItemId", "VendorId", "LocationId", "LocationName", "DisplayName"));
            ////  dvItemVendorWarehouse.RowFilter = "VendorID= " + intItemId.ToString() + " OR ITEMID=" + Common.INT_DBNULL.ToString();

            //dgvComboDeliveryLocInfo.DataSource = dvItemVendorWarehouse;
            //dgvComboDeliveryLocInfo.DisplayMember = "DisplayName";
            //dgvComboDeliveryLocInfo.ValueMember = "LocationId";
            //dgvComboDeliveryLocInfo.DataPropertyName = CON_COL_DELIVERYLOCATIONID;
            // cmbDestinationLocation.SelectedIndexChanged += new EventHandler(cmbDestinationLocation_SelectedIndexChanged);
        }
        
        private Boolean ProcessIndent(Common.IndentConsolidationState enumIndentConsolidationState)
        {
            bool isvalid = true;
            //Populate the Indent Object for Saving

            string error = string.Empty;
            if (enumIndentConsolidationState == Common.IndentConsolidationState.ConsolidationComplete)
            {
                isvalid = ValidateQty(ref error);
                if (!isvalid)
                {
                    MessageBox.Show(error);
                    return false;
                }
            }
            IndentConsolidation indentConsolidation = PopulateIndentConsolidationObject(enumIndentConsolidationState);
            if (indentConsolidation == null)
                return false;

            if (enumIndentConsolidationState == Common.IndentConsolidationState.ConsolidationComplete)
                isvalid = Validate(indentConsolidation, ref error);
            if (!isvalid)
            {
                MessageBox.Show(error);
                return false;
            }
            string strConfirm=string.Empty;
            if (enumIndentConsolidationState == Common.IndentConsolidationState.ConsolidationComplete)
                strConfirm = "Consolidate";
            else if (enumIndentConsolidationState == Common.IndentConsolidationState.InConsolidation)
                strConfirm = "Save";
            else if (enumIndentConsolidationState == Common.IndentConsolidationState.Cancelled)
                strConfirm = "Cancel";
            DialogResult Saveresult = MessageBox.Show(Common.GetMessage("5010", strConfirm), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (Saveresult == DialogResult.Yes)
            {
                string errorMessage = string.Empty;
                Boolean IsSuccess = false;

                IsSuccess = indentConsolidation.Save(indentConsolidation, (int)enumIndentConsolidationState, ref m_intConsolidationId, ref errorMessage);
                if (!IsSuccess)
                {
                    if (errorMessage.StartsWith("30001"))
                    {
                        MessageBox.Show(Common.GetMessage("30001"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Common.LogException(new Exception(errorMessage));
                    }
                    //else if (errorMessage.StartsWith("MULTIERROR"))
                    //{
                    //    string errMsg = string.Empty;
                    //    errMsg = Common.GetMultiErrorMessage(errorMessage);

                    //    MessageBox.Show(errMsg, Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //}
                    //else if (errorMessage.Contains("|"))
                    //{
                    //    string errorCode = errorMessage.Split('|')[0];
                    //    string itemIds = errorMessage.Split('|')[1];
                    //    string forLocation = errorMessage.Split('|')[2];
                    //    string vendorId = errorMessage.Split('|')[3];

                    //    MessageBox.Show(Common.GetMessage("INF0115", itemIds, forLocation, itemIds), Common.GetMessage("10001").Replace("\\n", Environment.NewLine), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //}
                    //else if (errorMessage.Contains(","))
                    //{
                    //    MessageBox.Show(Common.GetMessage(errorMessage.Split(",".ToCharArray()[0])), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //}
                    else
                    {
                        MessageBox.Show(Common.GetMessage(errorMessage), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                return IsSuccess;
            }
            else
                return false;


        }
        
        private void BindIndentConsolidationSearch(List<IndentConsolidation> lstConsolidation)
        {
            try
            {
                dgvIndentConsolidationGrid.DataSource = lstConsolidation;
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool Validate(IndentConsolidation indentConsolidation, ref string Messgae)
        {
            bool isvalid = true;
            foreach (IndentConsolidationHeader header in indentConsolidation.IndentConsolidationHeader)
            {
                foreach (IndentConsolidationDetail detail in header.IndentConsolidationDetail)
                {
                    
                    if (detail.RecordType == (int)Common.IndentConsolidationRecordType.TOI && detail.Quantity > 0 && detail.TransferFromLocationId <= 0)
                    {
                        Messgae = Common.GetMessage("INF0001", "Transfer From Location");
                        isvalid = false;
                        break;
                    }
                    if (detail.RecordType == (int)Common.IndentConsolidationRecordType.PO && detail.Quantity > 0 && (detail.VendorId <= 0 || detail.DeliveryLocationId <= 0))
                    {
                        Messgae = Common.GetMessage("INF0001", "Vendor and DeliveryLocation");
                        isvalid = false;
                        break;
                    }
                }
            }
            return isvalid;
        }
      
        private bool ValidateQty(ref string error)
        {
            bool isvalid = true;
            for (int i = 0; i < dtDetail.Rows.Count; i++)
            {
                if (Convert.ToInt32(dtDetail.Rows[i][CON_COL_APPROVEDQTY]) >0 && Convert.ToString(dtDetail.Rows[i][CON_COL_POQTY]) == "0" && Convert.ToString(dtDetail.Rows[i][CON_COL_TOIQTY])== "0")
                {
                    if (!Validators.IsGreaterThanZero(Convert.ToString(dtDetail.Rows[i][CON_COL_TOIQTY])))
                     {
                        isvalid = false;
                        error = Common.GetMessage("VAL0033", "TOI Qty");
                        break;
                     }

                    if (!Validators.IsGreaterThanZero(Convert.ToString(dtDetail.Rows[i][CON_COL_POQTY])))
                     {
                        isvalid = false;
                        error = Common.GetMessage("VAL0033", "PO Qty");
                        break;
                     }
                }
                if (Convert.ToInt32( dtDetail.Rows[i][CON_COL_APPROVEDQTY]) > 0)
                {
                    if (!Validators.IsValidQuantity(Convert.ToString(dtDetail.Rows[i][CON_COL_POQTY])))
                    {
                        isvalid = false;
                        error = Common.GetMessage("INF0010", "PO Qty");
                        break;
                    }
                    if (!Validators.IsValidQuantity(Convert.ToString(dtDetail.Rows[i][CON_COL_TOIQTY])))
                    {
                        isvalid = false;
                        error = Common.GetMessage("INF0010", "TOI Qty");
                        break;
                    }
                }
                 Int32 approveQty = 0;
                 Int32 poQty = 0;
                 Int32 TOIQty = 0;
                 approveQty = Convert.ToInt32(dtDetail.Rows[i][CON_COL_APPROVEDQTY]);
                 poQty = Convert.ToInt32(dtDetail.Rows[i][CON_COL_POQTY]);
                 TOIQty = Convert.ToInt32(dtDetail.Rows[i][CON_COL_TOIQTY]);
                 if (poQty + TOIQty > approveQty)
                 {
                     //  dtDetail.Rows[i][CON_COL_ROWINDICATOR] = RowIndicator.Invalid;
                     error = Common.GetMessage("INF0056");
                     isvalid = false;
                     break;
                 }
                
              }
            return isvalid;
        }
      
        private IndentConsolidation PopulateIndentConsolidationObject(Common.IndentConsolidationState enumIndentConsolidationState)
        {
            IIndentConsolidationHeader objIndentConsolidationHeader;
            List<IndentConsolidationHeader> lstIndentConsolidationHeader = new List<IndentConsolidationHeader>();

            //Populate IndentConsolidation object
            IIndentConsolidationDetail objIndentConsolidationDetail;
            List<IndentConsolidationDetail> lstIndentConsolidationDetail;

            int itemId;
            DataRow[] dr;
            int intHeaderLineNo = 1;
            int intDetailLineNo = 1;
            IndentConsolidation objIndentConsolidation=new IndentConsolidation();
            for (int i = 0; i < dtHeader.Rows.Count; i++)
            {
                itemId = Convert.ToInt32(dtHeader.Rows[i]["ItemId"]);
                dr = dtDetail.Select("ItemId=" + itemId.ToString());

                for (int j = 0; j < dr.Length; j++)
                {
                    if (dr[j][CON_COL_TOIQTY].ToString().Trim().Length == 0)
                    {
                        MessageBox.Show(Common.GetMessage("INF0010", "TOI Qty"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return null;
                    }
                    if (dr[j][CON_COL_POQTY].ToString().Trim().Length == 0)
                    {
                        MessageBox.Show(Common.GetMessage("INF0010", "PO Qty"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return null;
                    }
                }

                for (int j = 0; j < dr.Length; j++)
                {
                    lstIndentConsolidationDetail = new List<IndentConsolidationDetail>();

                    //Populate PO Values

                    objIndentConsolidationDetail = new IndentConsolidationDetail(Convert.ToInt32(dr[j][CON_COL_HEADERID]),
                                                                                  Convert.ToInt32(dr[j][CON_COL_PODETAILID]),
                        //Convert.ToInt32(dr[j][CON_COL_PODETAILID]),
                                                                                 intDetailLineNo++,
                                                                                 1,
                                                                                 Convert.ToInt32(Common.IndentConsolidationRecordType.PO),
                                                                                 Convert.ToDouble(dr[j][CON_COL_POQTY]),
                                                                                 Validators.CheckForDBNull(dr[j][CON_COL_VENDORID], Common.INT_DBNULL),
                                                                                 Validators.CheckForDBNull(dr[j][CON_COL_DELIVERYLOCATIONID], Common.INT_DBNULL),
                                                                                 Common.INT_DBNULL,
                                                                                 dr[j]["IsFormC"] == null || dr[j]["IsFormC"] == System.DBNull.Value ? false : Convert.ToBoolean(dr[j]["IsFormC"])
                                                                                 );
                
                    lstIndentConsolidationDetail.Add((IndentConsolidationDetail)objIndentConsolidationDetail);

                    //Populate TO Values
                    objIndentConsolidationDetail =
                            new IndentConsolidationDetail(Convert.ToInt32(dr[j][CON_COL_HEADERID]),
                                                            Convert.ToInt32(dr[j][CON_COL_TODETAILID]),
                                                            intDetailLineNo++,
                                                            1,
                                                            (int)Common.IndentConsolidationRecordType.TOI,
                                                            Convert.ToDouble(dr[j][CON_COL_TOIQTY]),
                                                            Common.INT_DBNULL,
                                                            Common.INT_DBNULL,
                                                            Convert.ToInt32(dr[j][CON_COL_TRANSERFROMLOC]),false);
                    lstIndentConsolidationDetail.Add((IndentConsolidationDetail)objIndentConsolidationDetail);

                    //Populate Header Info first
                    objIndentConsolidationHeader = new IndentConsolidationHeader(Convert.ToInt32(dr[j][CON_COL_HEADERID]),
                                                                                 itemId,
                                                                                 intHeaderLineNo,
                                                                                 (string)dr[j][CON_COL_INDENTNO],
                                                                                 Convert.ToDouble(dr[j][CON_COL_APPROVEDQTY]),
                                                                                 Convert.ToInt32(dr[j][CON_COL_TRANSFERLOCATION]),
                                                                                 m_Consolidation.ConsolidationId,
                                                                                 lstIndentConsolidationDetail);
                    intHeaderLineNo++;
                    lstIndentConsolidationHeader.Add((IndentConsolidationHeader)objIndentConsolidationHeader);
                    intDetailLineNo = 1;
                }
                intHeaderLineNo = 1;
                //headerId++;
            }

            objIndentConsolidation = new IndentConsolidation(m_Consolidation.ConsolidationId,
                                                                                (int)enumIndentConsolidationState,
                                                                                Convert.ToInt32(Common.IndentConsolidationSource.Auto),
                                                                                intUserId,
                                                                                DateTime.Now.ToString(Common.DATE_TIME_FORMAT),
                                                                                intUserId,
                                                                                DateTime.Now.ToString(Common.DATE_TIME_FORMAT),
                                                                                lstIndentConsolidationHeader,
                                                                                IndentConsolidationBL.OperationState.New);

            return (IndentConsolidation)objIndentConsolidation;
        }

        #endregion

        #region Events

        #region GridView

        private void dgvItemWarehouseStatus_SelectionChanged(object sender, EventArgs e)
        {
            //Bind ItemAllocation Grid
            if (!IsBoundFirstTime)
            {
                try
                {
                    ItemSelectionChange();
                }
                catch (Exception ex)
                {
                    Common.LogException(ex);
                    MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvIndentConsolidationGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
                {

                    if (dgvIndentConsolidationGrid.Columns[e.ColumnIndex].CellType == typeof(DataGridViewImageCell))
                    {
                        if (Convert.ToString(dgvIndentConsolidationGrid.Rows[e.RowIndex].Cells[CON_COL_CONSOLIDATIONNO].Value) != "")
                        {
                            int consolidationId = Convert.ToInt32(dgvIndentConsolidationGrid.Rows[e.RowIndex].Cells[CON_COL_CONSOLIDATIONNO].Value);
                            ResetForm(consolidationId);
                            tabMultiTabTemplate.SelectedIndex = 1;
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

        private void dgvItemLocationAllocation_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {           
            try
            {
                if (e.Exception != null)
                {
                    MessageBox.Show(dgvItemLocationAllocation, Common.GetMessage("INF0010", "Qty"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    if (dgvItemLocationAllocation.Columns[e.ColumnIndex].Name == CON_COL_POQTY || dgvItemLocationAllocation.Columns[e.ColumnIndex].Name == CON_COL_TOIQTY)
                    {
                        dgvItemLocationAllocation.CurrentCell.Value = 0;
                        
                    }
                    e.Cancel = false;
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

        private void dgvIndentConsolidationGrid_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (dgvIndentConsolidationGrid.SelectedRows.Count > 0)
                {
                    if (Convert.ToString(dgvIndentConsolidationGrid.Rows[dgvIndentConsolidationGrid.SelectedRows[0].Index].Cells[CON_COL_CONSOLIDATIONNO].Value) != "")
                    {
                        int consolidationId = Convert.ToInt32(dgvIndentConsolidationGrid.Rows[dgvIndentConsolidationGrid.SelectedRows[0].Index].Cells[CON_COL_CONSOLIDATIONNO].Value);
                        ResetForm(consolidationId);
                        tabMultiTabTemplate.SelectedIndex = 1;
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("30001"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }
  
        private void dgvItemLocationAllocation_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                if (dgvItemLocationAllocation.Rows[e.RowIndex].Cells[CON_COL_TRANSERFROMLOC].Value != null)
                {

                    m_IndentDetail.getFromLocationStock(Convert.ToInt16(dgvItemLocationAllocation.Rows[e.RowIndex].Cells[CON_COL_TRANSERFROMLOC].Value.ToString()), m_intCurrentItemId);
                    dgvItemLocationAllocation.Rows[e.RowIndex].Cells[CON_REL_ITEMLOCATIONALLOCATIONSTOCK].Value = Convert.ToDouble(m_IndentDetail.WhLocationStock);
                    //IndentConsolidation indentstock = new IndentConsolidation();
                    //indentstock.Stock= Convert.ToDouble(m_IndentDetail.WhLocationStock);
                }
                if (dgvItemLocationAllocation.CurrentCell.GetType().Name == "DataGridViewComboBoxCell") 
                    SendKeys.Send("{F4}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("30001"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        private void dgvItemLocationAllocation_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {                
                CellEdit(e.ColumnIndex, e.RowIndex);
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("30001"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }

        }
        
        private void dgvItemLocationAllocation_Leave(object sender, EventArgs e)
        {
            try
            {

                if (dgvItemLocationAllocation.CurrentRow != null && dgvItemLocationAllocation.CurrentCell != null)
                {
                    CellEdit(5, dgvItemLocationAllocation.CurrentRow.Index);
                    CellEdit(6, dgvItemLocationAllocation.CurrentRow.Index);
                }
             
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
     
        #endregion
        
        #region Button
        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult resetresult = MessageBox.Show(Common.GetMessage("5006"), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resetresult == DialogResult.Yes)
                {
                    //Reset Search Page and over all screen
                    m_intConsolidationId = Common.INT_DBNULL;
                    dgvIndentConsolidationGrid.DataSource = null;
                    txtSearchConsolidationNo.Text = string.Empty;
                    txtSearchIndentNo.Text = string.Empty;
                    dtpFromDate.Text = string.Empty;
                    dtpToDate.Text = string.Empty;
                    cmbSource.SelectedIndex = 0;
                    cmbStatus.SelectedIndex = 0;
                    ResetForm(m_intConsolidationId);
                }

            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            m_intConsolidationId = Common.INT_DBNULL;

            //Boolean IsValidationSuccess = true;
            StringBuilder errorMessages = new StringBuilder();
            string errorMessage = string.Empty;

            try
            {
                //Validate Search Data
                if (!String.IsNullOrEmpty(txtSearchConsolidationNo.Text))
                {
                    if (Validators.IsInt32(txtSearchConsolidationNo.Text))
                    {
                        m_intConsolidationId = Convert.ToInt32(txtSearchConsolidationNo.Text);
                    }
                    else
                    {
                        MessageBox.Show(Common.GetMessage("VAL0021", "Consolidation No"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtSearchConsolidationNo.Focus();
                        return;
                    }
                }



                //IndentConsolidation indentConsolidation = new IndentConsolidation(intConsolidationId, Convert.ToInt32(cmbStatus.SelectedValue), Convert.ToInt32(cmbSource.SelectedValue), Convert.ToDateTime(dtpFromDate.Text), Convert.ToDateTime(dtpToDate.Text));
                IndentConsolidation indentConsolidation = new IndentConsolidation(
                                                            txtSearchIndentNo.Text.Trim(),
                                                            m_intConsolidationId,
                                                            Convert.ToInt32(cmbStatus.SelectedValue),
                                                            Convert.ToInt32(cmbSource.SelectedValue),
                                                            dtpFromDate.Checked ? Convert.ToDateTime(dtpFromDate.Text) : Common.DATETIME_NULL,
                                                            dtpToDate.Checked ? Convert.ToDateTime(dtpToDate.Text) : Common.DATETIME_NULL
                                                            );
                //String errorMessage = string.Empty;

                //List<IndentConsolidation> lstConsolidation = indentConsolidation.Search(indentConsolidation, ref errorMessage);
                List<IndentConsolidation> lstConsolidation = indentConsolidation.Search(indentConsolidation,txtPONumber.Text.Trim(),txtTOINumber.Text.Trim(), ref errorMessage);



                if (errorMessage.Length > 0)
                {
                    Common.LogException(new Exception(errorMessage));
                    dgvIndentConsolidationGrid.DataSource = null;
                    MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (lstConsolidation.Count == 0)
                {
                    dgvIndentConsolidationGrid.DataSource = null;
                    MessageBox.Show(Common.GetMessage("8002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    dgvIndentConsolidationGrid.DataSource = null;
                    var query = from q in lstConsolidation where q.Status != (int)Common.IndentConsolidationState.Cancelled select q;
                    if (query != null && query.ToList().Count > 0)
                    {
                        lstConsolidation = query.ToList();
                        BindIndentConsolidationSearch(lstConsolidation);
                    }
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnConsolidateAll_Click(object sender, EventArgs e)
        {

            try
            {
                for (int i = 0; i < dgvApprovedItems.Rows.Count; i++)
                {
                    DataGridViewCheckBoxCell dgvCell = (DataGridViewCheckBoxCell)dgvApprovedItems.Rows[i].Cells["ItemCheckBox"];
                    dgvCell.Value = true;
                }
                ConsolidateSelect();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                IsBoundFirstTime = false;
            }
        }

        private void btnIndentSave_Click(object sender, EventArgs e)
        {
            try
            {
                dgvItemLocationAllocation.EndEdit();
              
                    if (ProcessIndent(Common.IndentConsolidationState.InConsolidation))
                    {
                        MessageBox.Show(Common.GetMessage("8001"));
                        ResetForm(-1);
                    }
           

            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnIndentConsolidate_Click(object sender, EventArgs e)
        {
            try
            {
                dgvItemLocationAllocation.EndEdit();
        
                    if (ProcessIndent(Common.IndentConsolidationState.ConsolidationComplete))
                    {
                        MessageBox.Show(Common.GetMessage("8012","Consolidated"));
                        ResetForm(m_intConsolidationId);
                    }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnIndentCancel_Click(object sender, EventArgs e)
        {
            try
            {
                //DialogResult cancelresult = MessageBox.Show(Common.GetMessage("5005"), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //if (cancelresult == DialogResult.Yes)
                //{
                    if (m_Consolidation.Status == (int)CurrentOperationState.UnderConsolidation)
                    {
                        if (ProcessIndent(Common.IndentConsolidationState.Cancelled))
                        {
                            MessageBox.Show(Common.GetMessage("8012", "Cancelled"));
                            ResetForm(-1);
                        }
                    }
                    else
                        ResetForm(-1);

               // }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnIndentReset_Click(object sender, EventArgs e)
        {
            try
            {
                ResetForm(-1);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void btnConsolidateSelected_Click(object sender, EventArgs e)
        {
           
            try
            {
                ConsolidateSelect();

            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        private void dgvItemLocationAllocation_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        #endregion

        private void dgvItemWarehouseStatus_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }
    
    }
}
