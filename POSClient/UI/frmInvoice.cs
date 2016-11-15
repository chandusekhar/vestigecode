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
using System.Configuration;
using AuthenticationComponent.BusinessObjects;
namespace POSClient.UI
{
    public partial class frmInvoice : POSClient.UI.BaseChildForm
    {
        private string GRIDVIEW_XML_PATH = Environment.CurrentDirectory + "\\App_Data\\POSGridViewDefinition.xml";
        private string OrderNo = string.Empty;
        private string LogNo = string.Empty;        
        private CO m_Order = null;
        List<CIBatchDetail> ListBatch = null;
        CIBatchDetail m_CurrentBatch = null;
        CurrencyManager m_bindingMgr;
        int m_UserID ;
        private int m_LocationID;
        private int m_ItemID = 0;
        private int m_Recordno = 0;
        private string m_itemCode = string.Empty;
        
        private decimal m_TotalQty = 0;
        private AutoCompleteStringCollection _batchcollection;
        //private AutoCompleteStringCollection _itemcollection;
        private List<CODetail> CODetailList;

        //Return Properties
        public string ReturnMessage{get;set;}
        public string ErrorCode { get; set; }
        public string InvoiceNo { get; set; }

        public frmInvoice(string orderNo)
        {
            try
            {
                InitializeComponent();
                m_UserID = Authenticate.LoggedInUser.UserId;
                m_LocationID = Common.CurrentLocationId;
                ErrorCode = string.Empty;
                InvoiceNo = string.Empty;
                ReturnMessage = string.Empty;
                OrderNo = orderNo;
                if (string.IsNullOrEmpty(orderNo))
                {
                    ErrorCode = "40010";
                }
                else
                {
                    InitailizeControls();                   
                }
            }
            catch (Exception ex)
            {
                ErrorCode = "30007";
                ReturnMessage = ex.ToString();
            }
        }
       
        public frmInvoice(string orderNo,string logNo)
        {
            try
            {               
                InitializeComponent();
                m_UserID =  Authenticate.LoggedInUser.UserId;
                m_LocationID = Common.CurrentLocationId;
                ErrorCode = string.Empty;
                InvoiceNo = string.Empty;
                ReturnMessage = string.Empty;
                OrderNo = orderNo;
                LogNo = logNo;
                if (string.IsNullOrEmpty(orderNo) && string.IsNullOrEmpty(LogNo))
                {
                    ErrorCode = "40010";
                }
                else
                {
                    if (LogNo.Equals(string.Empty))
                        InitailizeControls();
                    else
                    {
                        CI invoice = new CI();
                        string error = string.Empty;
                        DataTable dtItems = new DataTable();
                        bool isProcessed=invoice.ProcessLog(logNo, m_LocationID, m_UserID,ref error, ref dtItems);
                        if (isProcessed && error.Trim().Equals(string.Empty))
                        {
                            ErrorCode = string.Empty;
                            ReturnMessage = "8011";
                        }
                        else
                        {
                            ErrorCode = error;   
                        }
                        //ProcessLog();
                    }
                }               
            }
            catch(Exception ex)
            {
                ErrorCode = "30007";
                ReturnMessage = ex.ToString();                                
            }
        }

        #region Methods

        #region Button Methods

        private void ClearDetails()
        {
            try
            {
                txtAvailableQty.Text = string.Empty;
                txtBatchNo.Text = string.Empty;
                txtExpiryDate.Text = string.Empty;
                txtInvoiceQty.Text = string.Empty;
               // txtItemCode.Text = string.Empty;
                txtMfgbatchNo.Text = string.Empty;
                txtMfgDate.Text = string.Empty;
                txtMRP.Text = string.Empty;
              //  txtTotalQty.Text = string.Empty;
              //  txtItemCode.ReadOnly = false;
                dgvInvoiceDetail.ClearSelection();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void AddBatch()
        {
            try
            {
                bool toadd = true;
                var q1 = from q in ListBatch where q.ItemId == m_CurrentBatch.ItemId && q.RecordNo==m_CurrentBatch.RecordNo && q.BatchNo == txtBatchNo.Text.Trim() select q;
                if (q1 != null && q1.ToList().Count > 0)
                {
                    m_CurrentBatch = (CIBatchDetail)q1.ToList()[0];
                    toadd = false;
                }                
                m_CurrentBatch.CreatedBy = m_UserID;
                m_CurrentBatch.Quantity = Convert.ToDecimal(txtInvoiceQty.Text.Trim());
                if (toadd)
                {
                    ListBatch.Add(m_CurrentBatch);
                }                
                SetGridView();
                ClearDetails();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void Remove(int Index)
        {
            try
            {
                DialogResult saveResult = MessageBox.Show(Common.GetMessage("5010", "Remove"), Common.GetMessage("10004"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (saveResult == DialogResult.Yes)
                {
                    ListBatch.RemoveAt(Index);
                    SetGridView();
                    ClearDetails();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool SaveInvoice(ref string ErrorMessage)
        {
            try
            {
                CI invoice = CreateInvoiceObject();
                bool IsSave = invoice.Save(ref ErrorMessage);
                if(IsSave)
                    InvoiceNo = invoice.InvoiceNo;
                return IsSave;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        // For Processing Complete Log No
        //private void ProcessLog()
        //{
        //    try
        //    {
        //        bool isValid = true;
        //        string errorMessage=string.Empty;
        //        isValid = CI.CheckBatchForLog(LogNo, m_LocationID,ref errorMessage);
        //        if (isValid)
        //        {
        //            List<CI> CIList = new List<CI>();
        //            CO order = new CO();
        //            order.LogNo = LogNo;
        //            order.Status = (int)Common.OrderStatus.Confirmed;
        //            errorMessage = string.Empty;
        //            List<CO> lstCo = order.Search(ref errorMessage);
        //            if (errorMessage.Equals(string.Empty) && lstCo != null && lstCo.Count>0)
        //            {

        //                foreach (CO o in lstCo)
        //                {
        //                    m_Order = new CO();
        //                    m_Order.GetCOAllDetails(o.CustomerOrderNo, -1);
        //                    CODetailList = m_Order.CODetailList;
        //                    errorMessage = string.Empty;
        //                    ListBatch = CIBatchDetail.GetDefaultBatch(m_Order.CustomerOrderNo, m_LocationID, ref errorMessage);
        //                    CI invoice = CreateInvoiceObject();
        //                    if (invoice != null)
        //                    {
        //                        string ValidationCode = string.Empty;
        //                        isValid = ValidateInvoice(ref ValidationCode);
        //                        if (isValid)
        //                        {
        //                            CIList.Add(invoice);
        //                        }
        //                        else
        //                        {
        //                            if (ValidationCode == "40007")
        //                            {
        //                                // Not sufficent Batch Qty present
        //                                ErrorCode = "40008";
        //                            }
        //                            else
        //                            {
        //                                ErrorCode = ValidationCode;
        //                            }
        //                            break;
        //                        }
        //                    }
        //                }
        //                if (isValid)
        //                {
        //                    CI invoice = new CI();
        //                    bool isSave = invoice.Save(CIList, ref errorMessage);
        //                    if (isSave)
        //                    {
        //                        ErrorCode = string.Empty;
        //                        // Log saved Succesfully
        //                        ReturnMessage = "8011";
        //                    }
        //                    else
        //                    {
        //                        ErrorCode = errorMessage;
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                //Order Not Found
        //                ErrorCode = "40011";
        //            }
                  
        //        }
        //        else if (!errorMessage.Trim().Equals(string.Empty))
        //        {
        //            if (errorMessage.Trim().IndexOf("30001:") >= 0)
        //            {
        //                throw new Exception(errorMessage.Trim());                        
        //            }
        //            else
        //            {
        //                ErrorCode = errorMessage.Trim();
        //            }
        //        }                
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        
        //}
        
        private void InitailizeControls()
        {
            try
            {
                //Initailize Grid          
                dgvInvoiceDetail.AutoGenerateColumns = false;
                dgvInvoiceDetail.AllowUserToAddRows = false;
                dgvInvoiceDetail.AllowUserToDeleteRows = false;
                dgvInvoiceDetail.SelectionMode = DataGridViewSelectionMode.CellSelect;
                DataGridView dgvSearchNew = Common.GetDataGridViewColumns(dgvInvoiceDetail, GRIDVIEW_XML_PATH);

                dgvCOItemDetails.AutoGenerateColumns = false;
                dgvCOItemDetails.AllowUserToAddRows = false;
                dgvCOItemDetails.AllowUserToDeleteRows = false;              
                DataGridView dgvCOItemDetailsNew = Common.GetDataGridViewColumns(dgvCOItemDetails, GRIDVIEW_XML_PATH);

                ResetForm();
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
                m_Order = new CO();
                m_Order.GetCOAllDetails(OrderNo, (int)Common.OrderStatus.Confirmed);
                if (m_Order != null && !string.IsNullOrEmpty(m_Order.CustomerOrderNo))
                {
                    CODetailList = m_Order.CODetailList;
                    dgvCOItemDetails.DataSource = CODetailList;
                    SetHeaderValue();
                    //RefreshItemList();
                    // Get Grid View data
                    string errorMessage = string.Empty;
                    ListBatch = CIBatchDetail.GetDefaultBatch(OrderNo, m_LocationID, ref errorMessage);                    
                    SetGridView();
                    dgvCOItemDetails.ClearSelection();
                    dgvInvoiceDetail.ClearSelection();
                }
                else
                {
                    // Co Detail not Found
                    ErrorCode = "40009";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void RefreshBatchList()
        {
            try
            {
                _batchcollection = new AutoCompleteStringCollection();
                if (m_LocationID > 0 && m_ItemID > 0)
                {
                    string errorMessage = string.Empty;
                    CIBatchDetail batch = new CIBatchDetail();
                    List<CIBatchDetail> Lists = batch.GetBatchDetail(string.Empty, m_ItemID, m_LocationID, ref errorMessage);
                    if (Lists != null)
                    {
                        for (int j = 0; j < Lists.Count; j++)
                        {
                            _batchcollection.Add(Lists[j].BatchNo);
                        }
                    }
                }
                txtBatchNo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txtBatchNo.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtBatchNo.AutoCompleteCustomSource = _batchcollection;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
            
        private void SetHeaderValue()
        {
            try
            {
                lblDistributorNameValue.Text = m_Order.DistributorName;
                lblOrderDateValue.Text = m_Order.DisplayOrderDate;                
                lblOrderNoValue.Text = m_Order.CustomerOrderNo;
                lblOrderTypeValue.Text = m_Order.OrderTypeName;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void SetBatchDetails()
        {
            try
            {
                if (m_CurrentBatch != null)
                {
                    //m_ItemID = m_CurrentBatch.ItemId;
                    //m_TotalQty = m_CurrentBatch.TotalQuantity;

                    // txtItemCode.Text = m_CurrentBatch.ItemCode;
                  //  txtTotalQty.Text = m_CurrentBatch.DisplayTotalQuantity.ToString();
                    txtBatchNo.Text = m_CurrentBatch.BatchNo;
                    txtInvoiceQty.Text = m_CurrentBatch.DisplayQuantity.ToString();
                    txtAvailableQty.Text = m_CurrentBatch.DisplayAvailableQty.ToString();
                    txtMRP.Text = m_CurrentBatch.DisplayMRP.ToString();
                    txtMfgbatchNo.Text = m_CurrentBatch.ManufacturerBatchNo;
                    txtExpiryDate.Text = m_CurrentBatch.DisplayExpiryDate;
                    txtMfgDate.Text = m_CurrentBatch.DisplayManufacturingDate;

                    
                }
                else
                {
                  //  txtItemCode.Text = string.Empty;
                  //  txtTotalQty.Text = string.Empty;
                    txtBatchNo.Text = string.Empty;
                    txtInvoiceQty.Text = string.Empty;
                    txtAvailableQty.Text = string.Empty;
                    txtMRP.Text = string.Empty;
                    txtMfgbatchNo.Text = string.Empty;
                    txtExpiryDate.Text = string.Empty;
                    txtMfgDate.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetGridView()
        {
            try
            {
                dgvInvoiceDetail.DataSource = null;
                if (ListBatch != null)
                {
                    dgvInvoiceDetail.DataSource = ListBatch;
                    m_bindingMgr = (CurrencyManager)this.BindingContext[ListBatch];
                    m_bindingMgr.Refresh();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private CI CreateInvoiceObject()
        {
            try
            {
                CI invoice = new CI();
                invoice.InvoiceNo = string.Empty;                
                invoice.CreatedBy = m_UserID;                
                invoice.CustomerOrderNo = m_Order.CustomerOrderNo;
                invoice.InvoiceAmount = m_Order.OrderAmount;
                invoice.DistributorId = m_Order.DistributorId;
                invoice.PCId = m_Order.PCId;
                //invoice.InvoiceDate = DateTime.Today.ToString(Common.DATE_TIME_FORMAT);
                invoice.BOId = m_Order.BOId;
                //to check
                invoice.InvoicePrinted = false;
                invoice.IsProcessed = 0;
                invoice.LogNo = m_Order.LogNo;
                invoice.ModifiedBy = m_UserID;
                invoice.ModifiedDate = DateTime.Today.ToString();
                invoice.StaffId = m_UserID;
                invoice.Status = 1;
                invoice.TINNo = Common.TINNO; // ConfigurationManager.AppSettings["TINNO"];
                invoice.CIDetailList = new List<CIDetail>();
                
                foreach (CODetail detail in CODetailList)
                {
                    CIDetail ciDetail = new CIDetail();
                    ciDetail.ItemCode = detail.ItemCode;
                    ciDetail.ItemId = detail.ItemId;
                    var query = from q in ListBatch where q.ItemId == detail.ItemId && q.RecordNo==detail.RecordNo select q;
                    if (query != null)
                        ciDetail.CIBatchList = query.ToList();                   
                    foreach (CIBatchDetail batch in ciDetail.CIBatchList)
                    {
                        batch.RecordNo = detail.RecordNo;
                        batch.CreatedBy = m_UserID;
                        batch.ModifiedBy = m_UserID;
                    }
                    invoice.CIDetailList.Add(ciDetail);
                }
                return invoice;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        private CIBatchDetail CreateBatchObject(CODetail ODetail)
        {
            
            try
            {
                CIBatchDetail batch = new CIBatchDetail();
                batch.ItemCode = ODetail.ItemCode;
                batch.ItemId = ODetail.ItemId;
                batch.TotalQuantity = ODetail.Qty;
                return batch;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
       
        #region Events

        #region GridView
     
        private void dgvInvoiceDetail_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvInvoiceDetail.SelectedRows.Count > 0)
                {
                    //errorAdd.Clear();
                   // m_CurrentBatch = ListBatch[dgvInvoiceDetail.SelectedRows[0].Index];
                    //SetBatchDetails();
                   // txtItemCode.ReadOnly = true;
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        private void dgvInvoiceDetail_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
                {
                    if (dgvInvoiceDetail.Columns[e.ColumnIndex].CellType == typeof(DataGridViewImageCell))
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
      
        #endregion
      
        #region Button

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

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                ClearDetails();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                
                //m_CurrentBatch = new CIBatchDetail();
                //m_CurrentBatch.ItemId = m_ItemID;                
                bool isvalid = ValidateAdd();
                if (isvalid)
                {
                    DialogResult saveResult = MessageBox.Show(Common.GetMessage("5010", "Add"), Common.GetMessage("10004"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (saveResult == DialogResult.Yes)
                    {
                        AddBatch();              
                    }
                }
                else
                {
                    StringBuilder sb=GenerateAddError();
                    if (!sb.ToString().Trim().Equals(string.Empty))
                    {
                        MessageBox.Show(sb.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        private void btnInvoice_Click(object sender, EventArgs e)
        {
            try
            {
                string errorCode = string.Empty;
                bool isvalid = ValidateInvoice(ref errorCode);
                if (isvalid)
                {
                    DialogResult saveResult = MessageBox.Show(Common.GetMessage("5010", "Save"), Common.GetMessage("10004"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (saveResult == DialogResult.Yes)
                    {
                        string ErrorMessage = string.Empty;
                        bool isSave = SaveInvoice(ref ErrorMessage);
                        if (isSave && ErrorMessage.Trim().Equals(string.Empty))
                        {
                            ErrorCode = string.Empty;
                            ReturnMessage = "8011";
                            DialogResult = DialogResult.OK;
                            this.Close();
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
                                MessageBox.Show(Common.GetMessage(ErrorMessage));
                            }
                        }
                    }
                }
                else
                {
                    if (!errorCode.Equals(string.Empty))
                    {
                        errorInvoice.SetError(dgvInvoiceDetail, Common.GetMessage(errorCode));
                        MessageBox.Show(Common.GetMessage(errorCode),Common.GetMessage("10004"),MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (ListBatch != null)
                {
                    ListBatch.Clear();
                    SetGridView();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }
     
        #endregion

        #region TEXTBOX

        private void txtBatchNo_Validating(object sender, CancelEventArgs e)
        {
            try
            {
               // m_ItemID = m_CurrentBatch.ItemId;                
                m_CurrentBatch = new CIBatchDetail();
               
                string errorMessage = string.Empty;
                if(txtBatchNo.Text.Trim().Equals(string.Empty))
                    return;
                List<CIBatchDetail> List = m_CurrentBatch.GetBatchDetail(txtBatchNo.Text.Trim(), m_ItemID, m_LocationID, ref errorMessage);
                if (List != null && List.Count > 0 && errorMessage.Trim().Equals(string.Empty))
                {
                    m_CurrentBatch = List[0];
                    m_CurrentBatch.ItemId = m_ItemID;
                    m_CurrentBatch.ItemCode = m_itemCode;
                    m_CurrentBatch.RecordNo = m_Recordno;
                    m_CurrentBatch.TotalQuantity = m_TotalQty;
                    m_CurrentBatch.AvailableQty= GetAvailableQty(m_CurrentBatch);
                }
                else
                    m_CurrentBatch.BatchNo = txtBatchNo.Text.Trim();
              
               // m_CurrentBatch.ItemCode = txtItemCode.Text.Trim();
                m_CurrentBatch.Quantity = txtInvoiceQty.Text.Trim()==string.Empty?0:Convert.ToDecimal(txtInvoiceQty.Text.Trim());
                SetBatchDetails();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }
        private decimal GetAvailableQty(CIBatchDetail batch)
        {
            if (batch.AvailableQty > 0)
            {
                decimal usedQty = 0;
                var query = from q in ListBatch where q.ItemId == m_CurrentBatch.ItemId && q.RecordNo!=m_CurrentBatch.RecordNo select q.Quantity;
                if (query != null && query.ToList().Count > 0)
                {
                     usedQty= query.Sum();
                }
                batch.AvailableQty = batch.AvailableQty; //- usedQty;
            }
            return batch.AvailableQty;
        }
        //private void txtItemCode_Validating(object sender, CancelEventArgs e)
        //{
        //    try
        //    {
        //       // if (m_CurrentBatch == null || !txtItemCode.Text.Trim().Equals(m_CurrentBatch.ItemCode))
        //        {
        //            m_CurrentBatch = new CIBatchDetail();
        //            string errorMessage = string.Empty;
        //            CODetail CODetail = new CODetail();
        //          //  CODetail = CODetail.GetCODetail(m_Order.CustomerOrderNo, txtItemCode.Text.Trim(), -1, ref errorMessage);
        //            if (CODetail != null && errorMessage.Trim().Equals(string.Empty))
        //            {
        //                m_CurrentBatch = CreateBatchObject(CODetail);
        //            }
        //            SetBatchDetails();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        Common.LogException(ex);
        //    }
        //}
        #endregion

        #endregion

        #region Validation
        private StringBuilder GenerateAddError()
        {
            bool focus = false;
            StringBuilder sbError = new StringBuilder();
           // if (errorAdd.GetError(txtItemCode).Trim().Length > 0)
            {
          //      sbError.Append(errorAdd.GetError(txtItemCode));
                sbError.AppendLine();
                if (!focus)
                {
          //          txtItemCode.Focus();
                    focus = true;
                }
            }
            if (errorAdd.GetError(txtBatchNo).Trim().Length > 0)
            {
                sbError.Append(errorAdd.GetError(txtBatchNo));
                sbError.AppendLine();
                if (!focus)
                {
                    txtBatchNo.Focus();
                    focus = true;
                }
            }
            if (errorAdd.GetError(txtAvailableQty).Trim().Length > 0)
            {
                sbError.Append(errorAdd.GetError(txtAvailableQty));
                sbError.AppendLine();
                if (!focus)
                {
                    txtAvailableQty.Focus();
                    focus = true;
                }
            }
            if (errorAdd.GetError(txtInvoiceQty).Trim().Length > 0)
            {
                sbError.Append(errorAdd.GetError(txtInvoiceQty));
                sbError.AppendLine();
                if (!focus)
                {
                    txtInvoiceQty.Focus();
                    focus = true;
                }
            }
            return sbError;
         
        }
        private bool ValidateAdd()
        {
            try
            {
                errorAdd.Clear();
                bool isvalid = true;
                //Check Item Selected                
                if(dgvCOItemDetails.SelectedRows.Count==0)
                {
                    isvalid=false;
                    errorAdd.SetError(dgvCOItemDetails, Common.GetMessage("40025"));
                    return isvalid;
                }                
                // BatchNo and InvoiceQty textBox Blank check
                isvalid = ValidateRequiredTextField(txtBatchNo, lblBatchNo) && ValidateValidQty(txtInvoiceQty, lblInvoiceQty);
                if(!isvalid)
                    return isvalid;
                //invoice qty should not be greater than available qty
                isvalid = ValidateBatchNo();
                if (!isvalid)
                {
                    errorAdd.SetError(txtBatchNo, Common.GetMessage("40026"));
                    return isvalid;
                }
                if (Convert.ToDateTime(m_CurrentBatch.ExpiryDate) <= DateTime.Today)
                {
                    isvalid = false;
                    errorAdd.SetError(txtBatchNo, Common.GetMessage("40027"));
                    return isvalid;
                }               
                if (Convert.ToDecimal(txtAvailableQty.Text.Trim()) <= 0)
                {
                    isvalid = false;
                    errorAdd.SetError(txtAvailableQty, Common.GetMessage("40024"));
                }

                    if (Convert.ToDecimal(txtInvoiceQty.Text.Trim()) > Convert.ToDecimal(txtAvailableQty.Text.Trim()))
                    {
                        isvalid = false;
                        errorAdd.SetError(txtInvoiceQty, Common.GetMessage("INF0034", "Invoice Qty", "Available Qty"));
                    }
                    else
                    {
                        decimal totalQty = 0; decimal batchQty = 0;
                        var q1 = from q in ListBatch where q.ItemId == m_CurrentBatch.ItemId && q.RecordNo==m_CurrentBatch.RecordNo select q.Quantity;
                        if (q1.ToList().Count > 0)
                        {
                            totalQty = (decimal)q1.Sum();
                        }
                        var query = from g in ListBatch where g.BatchNo == m_CurrentBatch.BatchNo && g.ItemId==m_CurrentBatch.ItemId && g.RecordNo==m_CurrentBatch.RecordNo select g;
                        if (query.ToList().Count > 0)
                        {
                            batchQty = query.ToList()[0].Quantity;
                        }
                        if (totalQty - batchQty + Convert.ToDecimal(txtInvoiceQty.Text.Trim()) > m_TotalQty)
                        {
                            errorAdd.SetError(txtInvoiceQty, Common.GetMessage("INF0034", "Invoice Qty", "Required Qty"));
                            isvalid = false;
                        }
                    }
                
                           
                return isvalid;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private bool ValidateRequiredTextField(TextBox txt, Label lbl)
        {
            try
            {
                bool isvalid = true;
                errorAdd.SetError(txt, string.Empty);
                bool isTextBoxEmpty = CoreComponent.Core.BusinessObjects.Validators.CheckForEmptyString(txt.Text.Trim().Length);
                if (isTextBoxEmpty == true)
                {
                    errorAdd.SetError(txt, Common.GetMessage("INF0019", lbl.Text.Trim().Substring(0, lbl.Text.Trim().Length - 2)));
                    isvalid = false;
                }
                return isvalid;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private bool ValidateInvoice(ref string error)
        {
            try
            {
                bool isvalid = true; decimal totalQty = 0;
                if (CODetailList != null && CODetailList.Count > 0)
                {
                    foreach (CODetail d in CODetailList)
                    {
                        var q1 = from q in ListBatch where q.ItemId == d.ItemId && q.RecordNo==d.RecordNo select q.Quantity;
                        if (q1 == null || q1.ToList().Count == 0)
                        {
                            // All Items Are not in list
                            error = "40006";
                            isvalid = false;
                            break;
                        }
                        else
                        {
                            totalQty = q1.Sum();
                            if (totalQty != d.Qty)
                            {
                                // Qty is not equal to required qty
                                //MessageBox.Show(Common.GetMessage("40037", d.ItemName, Math.Floor(totalQty).ToString(), Math.Floor(d.Qty).ToString(), d.CustomerOrderNo), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                error = "40007";
                                isvalid = false;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    //Co detail not found
                    error = "40009";
                    isvalid = false; 
                }
                return isvalid;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private bool ValidateValidQty(TextBox txt, Label lbl)
        {
            try
            {
                bool isvalid = true;
                errorAdd.SetError(txt, string.Empty);
                bool isTextBoxEmpty = CoreComponent.Core.BusinessObjects.Validators.CheckForEmptyString(txt.Text.Trim().Length);
                if (isTextBoxEmpty == true)
                {
                    isvalid = false;
                    errorAdd.SetError(txt, Common.GetMessage("INF0019", lbl.Text.Trim().Substring(0, lbl.Text.Trim().Length - 2)));
                }
                else if (Validators.IsValidQuantity(txt.Text.Trim()))
                {
                    bool isGreaterThanZero = CoreComponent.Core.BusinessObjects.Validators.IsGreaterThanZero(txt.Text.Trim());
                    if (!isGreaterThanZero)
                    {
                        errorAdd.SetError(txt, Common.GetMessage("VAL0033", lbl.Text.Trim().Substring(0, lbl.Text.Trim().Length - 2)));
                        isvalid = false;  
                    }
                }
                else
                {
                    errorAdd.SetError(txt, Common.GetMessage("INF0010", lbl.Text.Trim().Substring(0, lbl.Text.Trim().Length - 2)));
                    isvalid = false;  
                }               
                return isvalid;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private bool ValidateBatchNo()
        {
            string errorMessage = string.Empty;
            List<CIBatchDetail> List = m_CurrentBatch.GetBatchDetail(txtBatchNo.Text.Trim(), m_ItemID, m_LocationID, ref errorMessage);
            if (List != null && List.Count > 0 && errorMessage.Trim().Equals(string.Empty))
            {
                //m_CurrentBatch = List[0];
                return true;
            }
            else
                return false;
        }
        #endregion

        private void dgvCOItemDetails_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvCOItemDetails.SelectedRows.Count > 0)
                {
                    m_ItemID = Convert.ToInt32(dgvCOItemDetails.SelectedRows[0].Cells["ItemID"].Value);
                    m_Recordno = Convert.ToInt32(dgvCOItemDetails.SelectedRows[0].Cells["RecordNo"].Value);
                    m_TotalQty = Convert.ToInt32(dgvCOItemDetails.SelectedRows[0].Cells["qty"].Value);
                    m_itemCode = Convert.ToString(dgvCOItemDetails.SelectedRows[0].Cells["ItemCode"].Value);
                    RefreshBatchList();
                    ClearDetails();
                 
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
            }
        }

         
    }
}
