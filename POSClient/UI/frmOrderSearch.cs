using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using POSClient.BusinessObjects;
using CoreComponent.Core.BusinessObjects;
namespace POSClient.UI
{
    public partial class frmOrderSearch : POSClient.UI.BaseChildForm
    {
        private Boolean IsAddToLogAvailable = false;
        private Boolean IsSearchAvailable = false;
        private List<COLog> LogList;
        private string CON_MODULENAME;
        private string m_LocationCode;
        private string m_UserName;
        int m_UserID;
        decimal totalAmount;
        private DGVColumnHeader m_objDGVColumnHeader;
        List<CO> m_orderSearchList;
        private CO m_returnObject;        
        public CO ReturnObject
        {
            get { return m_returnObject; }
        }
        public frmOrderSearch()
        {
            InitializeComponent();            
        }
        private void frmOrderSearch_Load(object sender, EventArgs e)
        {            
            if (AuthenticationComponent.BusinessObjects.Authenticate.LoggedInUser != null)
            {
                m_UserID = AuthenticationComponent.BusinessObjects.Authenticate.LoggedInUser.UserId;
                m_UserName = AuthenticationComponent.BusinessObjects.Authenticate.LoggedInUser.UserName;
            }
            InitializeRights();
            InitailizeControls();
            //m_objDGVColumnHeader = Common.CreateCheckBoxColumn(dgvOrderSearch, "Add To Log", "AddToLog");
            //m_objDGVColumnHeader.OnCheckBoxMouseClick += new EventHandler(m_objDGVColumnHeader_OnCheckBoxMouseClick);
        }

        
        #region Methods

        private void InitializeRights()
        {
            m_LocationCode = Common.LocationCode;            
            CON_MODULENAME = Common.MODULE_ORDERHISTORY;
            if (!string.IsNullOrEmpty(m_UserName) && !string.IsNullOrEmpty(CON_MODULENAME))
            {
                IsSearchAvailable = AuthenticationComponent.BusinessObjects.Authenticate.IsFunctionAccessible(m_UserName, m_LocationCode, CON_MODULENAME, Common.FUNCTIONCODE_SEARCH);
                IsAddToLogAvailable = AuthenticationComponent.BusinessObjects.Authenticate.IsFunctionAccessible(m_UserName, m_LocationCode, CON_MODULENAME, Common.FUNCTIONCODE_ADDTOLOG);
            }
        }
      
        private void InitailizeControls()
        {
            //Initailize Grid
               string GRIDVIEW_XML_PATH = Environment.CurrentDirectory + "\\App_Data\\POSGridViewDefinition.xml";
            //Search GridView
            dgvOrderSearch.AutoGenerateColumns = false;
            dgvOrderSearch.AllowUserToAddRows = false;
            dgvOrderSearch.AllowUserToDeleteRows = false;
            dgvOrderSearch.SelectionMode = DataGridViewSelectionMode.CellSelect;
            DataGridView dgvSearchNew = Common.GetDataGridViewColumns(dgvOrderSearch, GRIDVIEW_XML_PATH);
            
            ckBox.BackColor = Color.Transparent;
            ckBox.Name = "ckBox";            
          
            //initialize Combo Open Log
            if (Common.CurrentLocationTypeId == (int)Common.LocationConfigId.BO)
            {
                BindCombo();
            }
            btnSearch.Enabled = IsSearchAvailable;
            btnOk.Enabled = IsSearchAvailable;
            btnadd.Visible = IsAddToLogAvailable;
            cmbLogList.Visible = IsAddToLogAvailable;
            lblComboLog.Visible = IsAddToLogAvailable;
        }

        private void ckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (dgvOrderSearch.Rows.Count == 0)
            {
                ckBox.Checked = false;
                return;
            }
            if (ckBox.Checked == false)
            {
                for (int j = 0; j < this.dgvOrderSearch.RowCount; j++)
                {
                    this.dgvOrderSearch[1, j].Value = ckBox.Checked;
                }
            }
            else
            {
                for (int j = 0; j < this.dgvOrderSearch.RowCount; j++)
                {
                    if ((dgvOrderSearch.Rows[j].Cells["Print"].ReadOnly == false))
                        this.dgvOrderSearch[1, j].Value = ckBox.Checked;
                }
            }
            this.dgvOrderSearch.EndEdit();
        }


        public void BindCombo()
        {
            try
            {
                cmbLogList.DataSource = null;
                COLog log = new COLog();           
                LogList = log.Search(1,Common.CurrentLocationId,string.Empty,Common.INT_DBNULL,Common.INT_DBNULL,Common.INT_DBNULL);
                if (LogList != null)
                {
                    COLog logSearch=new COLog();
                    logSearch.LogNo="Select Log";
                    LogList.Insert(0, logSearch);

                    cmbLogList.DataSource = LogList;
                    cmbLogList.DisplayMember = "LogNo";
                    cmbLogList.ValueMember = "LogValue";
                }

                DataTable dtStatus = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter("ORDERSTATUS", 0,0, 0));
                if (dtStatus != null)
                {
                    cmbStatus.DataSource = dtStatus;
                    cmbStatus.ValueMember = Common.KEYCODE1;
                    cmbStatus.DisplayMember = Common.KEYVALUE1;
                }

                DataTable dtStockPoint = Common.ParameterLookup(Common.ParameterType.BOwithPUC, new ParameterFilter("", Common.CurrentLocationId, 0, 0));
                if (dtStockPoint != null)
                {
                    cmbStockPoint.DataSource = dtStockPoint;
                    cmbStockPoint.ValueMember = "LocationId";
                    cmbStockPoint.DisplayMember = "LocationCode";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool AddToLog(List<CO> ListToAdd)
        {
            try
            {
                CO order = new CO();
                string errorMessage = string.Empty;
                return order.Save(ListToAdd, ref errorMessage);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void BindGrid()
        {
            try
            {
                totalAmount = 0;
                if (m_orderSearchList != null)
                {
                    dgvOrderSearch.DataSource = m_orderSearchList;
                }

                btnadd.Enabled = false;                
                ckBox.Checked = false;                  
                bool enableAddToLog = false;
                foreach (DataGridViewRow row in dgvOrderSearch.Rows)
                {
                    if (Convert.ToInt32(row.Cells["Status"].Value)==(int)Common.OrderStatus.Confirmed)
                    {
                        row.Cells["AddToLog"].ReadOnly = false;
                        enableAddToLog = true;
                    }
                    else
                    {
                        row.Cells["AddToLog"].ReadOnly = true;
                        if (Convert.ToString(row.Cells["LogNo"].Value).Equals(string.Empty))
                            row.Cells["AddToLog"].Value = false;
                        else if (Convert.ToInt32(row.Cells["Status"].Value) == (int)Common.OrderStatus.Invoiced )
                            row.Cells["AddToLog"].Value = true;
                    }
                    if (Convert.ToInt32(row.Cells["Status"].Value) == (int)Common.OrderStatus.Invoiced)
                    {
                        row.Cells["Print"].ReadOnly = false;
                    }
                    else
                    {
                        row.Cells["Print"].ReadOnly = true;
                        row.Cells["Print"].Value = false;
                    }
                    totalAmount = totalAmount + Convert.ToDecimal(row.Cells["OrderAmount"].Value);

                    // To stop invoive printing of 0 price kit print 
                    // Disallow invoice printing of older invoices
                   
                    if (!Convert.ToBoolean(row.Cells["IsPrintAllowed"].Value) )
                    {
                        row.Cells["Print"].ReadOnly = true;                        
                    }
                    else if ((Convert.ToInt32(row.Cells["ValidReportPrintDays"].Value) > 0 ) && Convert.ToInt32(row.Cells["ValidReportPrintDays"].Value) <= (DateTime.Today - Convert.ToDateTime(row.Cells["InvoiceDate"].Value)).Days)
                    {

                        row.Cells["Print"].ReadOnly = true;             
                    }
                    else
                    {
                        row.Cells["Print"].ReadOnly = false;
                    }

                }

                btnadd.Enabled = enableAddToLog ? IsAddToLogAvailable : enableAddToLog;
            }
             catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Search()
        {
            try
            {
                dgvOrderSearch.DataSource = null;
                CO order = new CO();
                order.CustomerOrderNo = txtOrderNo.Text.Trim();
                order.LogNo = txtLogNo.Text.Trim();
                order.InvoiceNo = txtInvoiceNo.Text.ToString();
                if (dtpFromDate.Checked)
                {
                    order.FromDate = dtpFromDate.Value.ToString();
                }
                else
                    order.FromDate = Common.DATETIME_NULL.ToShortDateString();
                if (dtpTodate.Checked)
                    order.ToDate = dtpTodate.Value.ToString();
                else
                    order.ToDate = Common.DATETIME_NULL.ToShortDateString();
                order.Status = Convert.ToInt32(cmbStatus.SelectedValue);
                order.DistributorId =string.IsNullOrEmpty(txtDistributorNo.Text)==true? -1:Convert.ToInt32(txtDistributorNo.Text.Trim());
                order.PCId = Convert.ToInt32(cmbStockPoint.SelectedValue);
                string errorMessage = string.Empty;
                m_orderSearchList = order.Search(ref errorMessage);
                BindGrid();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Events
        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult = DialogResult.Cancel;
                this.Close();
            }
             catch (Exception ex)
             {
                 MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                 Common.LogException(ex);
             }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvOrderSearch.SelectedCells.Count > 0)
                {
                    string orderno = Convert.ToString(dgvOrderSearch.Rows[dgvOrderSearch.SelectedCells[0].RowIndex].Cells["OrderNo"].Value);
                    CO order = new CO();
                    order.GetCOAllDetails(orderno,-1);
                    m_returnObject = order;
                    //add order detail form call
                    DialogResult = DialogResult.OK;
                    this.Close();
                    //BindCombo();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex); 
            }
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            try
            {
                //validation 
                if (ValidateAdd())
                {
                    DialogResult saveResult = MessageBox.Show(Common.GetMessage("5010","Add"), Common.GetMessage("10004"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (saveResult == DialogResult.Yes)
                    {
                        List<CO> ListToAdd = new List<CO>();
                        dgvOrderSearch.EndEdit();
                        foreach (DataGridViewRow row in dgvOrderSearch.Rows)
                        {
                            if (Convert.ToInt32(row.Cells["Status"].Value) == (int)Common.OrderStatus.Confirmed && Convert.ToBoolean(row.Cells["AddToLog"].Value) == true)
                            {
                                //(from p in existingOrder.CODetailList where p.ItemId == detail.ItemId select p.ItemId);
                                var queryPCId = (from p in LogList where p.LogValue == Convert.ToString(cmbLogList.SelectedValue) select p.PCId).Max();

                                if (m_orderSearchList[row.Index].PCId!=queryPCId)
                                {
                                    MessageBox.Show(Common.GetMessage("VAL0601"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }

                                m_orderSearchList[row.Index].LogNo = Convert.ToString(cmbLogList.SelectedValue);
                                //if (m_orderSearchList[row.Index].PCId == 
                                ListToAdd.Add(m_orderSearchList[row.Index]);
                            }
                        }
                        if (ListToAdd != null && ListToAdd.Count > 0)
                        {
                            var query = (from q in ListToAdd select q.PCId).Distinct();
                            if (query != null && query.ToList().Count == 1)
                            {
                                if (AddToLog(ListToAdd))
                                {
                                    if (ListToAdd.Count > 0)
                                    {
                                        MessageBox.Show(Common.GetMessage("VAL0602"));
                                        Search();
                                    }
                                    else
                                    {
                                        MessageBox.Show(Common.GetMessage("INF0097"));
                                    }
                                }
                                //else
                                //    MessageBox.Show(Common.GetMessage("INF0097"));
                            }
                            else
                            {
                                MessageBox.Show(Common.GetMessage("40022"));
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show(errorAddToLog.GetError(cmbLogList));
                }
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
                txtGetTotalAmount.Text = "";
                if (ValidateSearch())
                {
                    Search();
                }
                else
                {
                    MessageBox.Show(errorAddToLog.GetError(dtpFromDate));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        private void dgvOrderSearch_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > 0 && e.ColumnIndex >= 1)
            {
                dgvOrderSearch.Rows[e.RowIndex].Selected = true;
            }
        }

       
        #endregion
                
        #region Validations

        private bool ValidateAdd()
        {
            try
            {
                errorAddToLog.Clear();
                if (cmbLogList.SelectedIndex==0 || cmbLogList.SelectedValue.Equals("Select Log"))
                {
                    errorAddToLog.SetError(cmbLogList, Common.GetMessage("VAL0002", lblComboLog.Text.Trim().Substring(0, lblComboLog.Text.Trim().Length - 1)));
                    cmbLogList.Focus();
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }  

        }

        private bool ValidateSearch()
        {
            try
            {
                errorAddToLog.Clear();
                bool isvalid = true;
                if (dtpFromDate.Checked && dtpTodate.Checked)
                {
                    if (!ValidateDates(dtpFromDate.Value, dtpTodate.Value))
                    {
                        errorAddToLog.SetError(dtpFromDate, Common.GetMessage("INF0034", "From Date", "To Date"));
                        isvalid= false;
                    }                   
                }
                return isvalid;
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        private bool ValidateDates(DateTime FromDate, DateTime ToDate)
        {
            int days = DateTime.Compare(FromDate, ToDate);
            if (days == 1)
            {
                return false;
            }
            return true;
        }

        #endregion       

        private void dgvOrderSearch_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.ColumnIndex > 0 && e.RowIndex >= 0)
                {
                    string orderno = Convert.ToString(dgvOrderSearch.Rows[e.RowIndex].Cells["OrderNo"].Value);
                    CO order = new CO();
                    order.GetCOAllDetails(orderno, -1);
                    m_returnObject = order;
                    //add order detail form call
                    DialogResult = DialogResult.OK;
                    this.Close();
                    //BindCombo();
                }
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
                PrintSelectedInvoices();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);

            }
        }

        private void PrintSelectedInvoices()
        {
            try
            {
                List<String> listToPrint = new List<String>();
                dgvOrderSearch.EndEdit();
                foreach (DataGridViewRow row in dgvOrderSearch.Rows)
                {
                    if ((Convert.ToInt32(row.Cells["Status"].Value) == (int)Common.OrderStatus.Invoiced) && Convert.ToBoolean(row.Cells["Print"].Value) == true)
                    {                        
                        listToPrint.Add(m_orderSearchList[row.Index].CustomerOrderNo);
                    }
                }
                if (listToPrint.Count > 0)
                {
                    foreach (String OrderNo in listToPrint)
                    {
                        DataSet dsReport = CreatePrintDataSet((int)Common.PrintType.PrintInvoice, OrderNo);
                        CoreComponent.UI.ReportScreen reportScreenObj = new CoreComponent.UI.ReportScreen((int)Common.ReportType.CustomerInvoice, dsReport);
                        reportScreenObj.PrintReport();
                        dsReport = null;
                    }
                    foreach (DataGridViewRow row in dgvOrderSearch.Rows)
                    {
                        row.Cells["Print"].Value = false;
                    }
                    MessageBox.Show(Common.GetMessage("INF0223",listToPrint.Count.ToString()), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(Common.GetMessage("INF0222"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                                
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
        }

        private DataSet CreatePrintDataSet(int type,string OrderNo)
        {

            string errorMessage = string.Empty;
            DataSet ds = CO.GetOrderForPrint(type, OrderNo, ref errorMessage);
            if (errorMessage.Trim().Length == 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ds.Tables[0].Columns.Add(new DataColumn("Header", Type.GetType("System.String")));
                    ds.Tables[0].Columns.Add(new DataColumn("DateText", Type.GetType("System.String")));
                    ds.Tables[0].Columns.Add(new DataColumn("TINNo", Type.GetType("System.String")));
                    ds.Tables[0].Columns.Add(new DataColumn("OrderAmountWords", Type.GetType("System.String")));
                    ds.Tables[1].Columns.Add(new DataColumn("PriceInclTax", Type.GetType("System.String")));
                    ds.Tables[0].Columns.Add(new DataColumn("PANNo", Type.GetType("System.String")));
                    ds.Tables[1].Columns.Add(new DataColumn("IsLocation", Type.GetType("System.String")));
                    ds.Tables[0].Rows[0]["Header"] = (type == 1 ? "Customer Order" : "Retail Invoice");
                    ds.Tables[0].Rows[0]["TINNo"] = Common.TINNO;
                    ds.Tables[0].Rows[0]["PANNo"] = Common.PANNO;
                    ds.Tables[0].Columns.Add(new DataColumn("CourierAmount", Type.GetType("System.String")));

                    ds.Tables[0].Columns.Add(new DataColumn("MiniBranch", Type.GetType("System.String")));
                    ds.Tables[0].Rows[0]["MiniBranch"] = (Common.IsMiniBranchLocation == 1 ? "N" : "Y");
                    //if (type == 2)
                    //{
                    //    if (ds.Tables[0].Rows[0]["Password"].ToString() != string.Empty)
                    //    {
                    //        User objUser = new User();
                    //        ds.Tables[0].Rows[0]["Password"] = objUser.DecryptPassword(ds.Tables[0].Rows[0]["Password"].ToString());
                    //    }
                    //    else
                    //    {
                    //        ds.Tables[0].Rows[0]["Password"] = string.Empty;

                    //    }
                    //}

                }
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    ds.Tables[0].Rows[i]["OrderAmount"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["OrderAmount"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                    ds.Tables[0].Rows[i]["TaxAmount"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["TaxAmount"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                    ds.Tables[0].Rows[i]["OrderAmountWords"] = Common.AmountToWords.AmtInWord(Convert.ToDecimal(ds.Tables[0].Rows[i]["OrderAmount"]) + Convert.ToDecimal(ds.Tables[0].Rows[i]["TaxAmount"]));
                    ds.Tables[0].Rows[i]["PaymentAmount"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["PaymentAmount"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                    ds.Tables[0].Rows[i]["ChangeAmount"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["ChangeAmount"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                    ds.Tables[0].Rows[i]["TotalBV"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["TotalBV"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                    ds.Tables[0].Rows[i]["TotalPV"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["TotalPV"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                    ds.Tables[0].Rows[i]["TotalWeight"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["TotalWeight"]), 3);
                    ds.Tables[0].Rows[i]["DateText"] = (Convert.ToDateTime(ds.Tables[0].Rows[i]["Date"])).ToString(Common.DTP_DATE_FORMAT);
                }
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    ds.Tables[1].Rows[i]["DP"] = Math.Round(Convert.ToDecimal(ds.Tables[1].Rows[i]["DP"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                    ds.Tables[1].Rows[i]["UnitPrice"] = Math.Round(Convert.ToDecimal(ds.Tables[1].Rows[i]["UnitPrice"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                    ds.Tables[1].Rows[i]["Qty"] = Math.Round(Convert.ToDecimal(ds.Tables[1].Rows[i]["Qty"]), Common.DisplayQtyRounding, MidpointRounding.AwayFromZero);
                    ds.Tables[1].Rows[i]["Amount"] = Math.Round(Convert.ToDecimal(ds.Tables[1].Rows[i]["Amount"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                    ds.Tables[1].Rows[i]["PriceInclTax"] = Math.Round(Convert.ToDecimal(ds.Tables[1].Rows[i]["UnitPrice"]) + (Convert.ToDecimal(ds.Tables[1].Rows[i]["TaxAmount"]) / Convert.ToDecimal(ds.Tables[1].Rows[i]["Qty"])), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                    ds.Tables[1].Rows[i]["TaxAmount"] = Math.Round(Convert.ToDecimal(ds.Tables[1].Rows[i]["TaxAmount"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                    if(ds.Tables[1].Rows[i]["TaxPercent"].ToString().Length > 0)
                       ds.Tables[1].Rows[i]["TaxPercent"] = Math.Round(Convert.ToDecimal(ds.Tables[1].Rows[i]["TaxPercent"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                    ds.Tables[1].Rows[i]["Discount"] = Math.Round(Convert.ToDecimal(ds.Tables[1].Rows[i]["Discount"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                }
                for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                {
                    ds.Tables[2].Rows[i]["PaymentAmount"] = Math.Round(Convert.ToDecimal(ds.Tables[2].Rows[i]["PaymentAmount"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                    //Bikram:AED Changes
                    if (Convert.ToInt32(ds.Tables[2].Rows[i]["TenderType"].ToString()) == 101)
                    {
                        ds.Tables[0].Rows[0]["CourierAmount"] = Math.Round(Convert.ToDecimal(ds.Tables[2].Rows[i]["PaymentAmount"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                        ds.Tables[2].Rows[i]["PaymentAmount"] = Math.Round(Convert.ToDecimal(0), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                    }
                }

                for (int i = 0; i < ds.Tables[3].Rows.Count; i++)
                {
                    ds.Tables[3].Rows[i]["TaxPercent"] = Math.Round(Convert.ToDecimal(ds.Tables[3].Rows[i]["TaxPercent"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                    ds.Tables[3].Rows[i]["TaxAmount"] = Math.Round(Convert.ToDecimal(ds.Tables[3].Rows[i]["TaxAmount"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                }
            }
            return ds;
        }

        private void chkLogAll_CheckedChanged(object sender, EventArgs e)
        {
            if (dgvOrderSearch.Rows.Count == 0)
            {
                chkLogAll.Checked = false;
                return;
            }
            if (chkLogAll.Checked == false)
            {
                for (int j = 0; j < this.dgvOrderSearch.RowCount; j++)
                {
                    if ((dgvOrderSearch.Rows[j].Cells["AddToLog"].ReadOnly == false))
                        this.dgvOrderSearch[0, j].Value = chkLogAll.Checked;
                }
            }
            else
            {
                for (int j = 0; j < this.dgvOrderSearch.RowCount; j++)
                {
                    if ((dgvOrderSearch.Rows[j].Cells["AddToLog"].ReadOnly == false))
                        this.dgvOrderSearch[0, j].Value = chkLogAll.Checked;
                }
            }
            this.dgvOrderSearch.EndEdit();
        }          

        void m_objDGVColumnHeader_OnCheckBoxMouseClick(object sender, EventArgs e)
        {
            try
            {
                if (dgvOrderSearch.Rows.Count == 0)
                {
                    m_objDGVColumnHeader.CheckAll = false;
                    return;
                }
                if (m_objDGVColumnHeader.CheckAll == false)
                {
                    for (int j = 0; j < this.dgvOrderSearch.RowCount; j++)
                    {
                        if ((dgvOrderSearch.Rows[j].Cells["AddToLog"].ReadOnly == false))
                            this.dgvOrderSearch[0, j].Value = m_objDGVColumnHeader.CheckAll;
                    }
                }
                else
                {
                    for (int j = 0; j < this.dgvOrderSearch.RowCount; j++)
                    {
                        if ((dgvOrderSearch.Rows[j].Cells["AddToLog"].ReadOnly == false))
                            this.dgvOrderSearch[0, j].Value = m_objDGVColumnHeader.CheckAll;
                    }
                }
                this.dgvOrderSearch.EndEdit();
            }
            catch (Exception ex)
            {
            }
        }

        //Added by Kaushik For calulating TotalAmount
        private void btnGetTotal_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (m_orderSearchList != null)
                {
                    txtGetTotalAmount.Text = totalAmount.ToString();
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
            }
        }

                 
    }
}
