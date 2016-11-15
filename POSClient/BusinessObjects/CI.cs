using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreComponent.Core.BusinessObjects;
using Vinculum.Framework.Data;
using Vinculum.Framework.DataTypes;
using System.Data;
using System.Windows.Forms;
namespace POSClient.BusinessObjects
{
    [Serializable]
    public class CI
    {
        #region SP_Declaration
        private const string SP_CI_SAVE = "usp_InvoiceSave";
        private const string SP_CI_LOGCHECK = "usp_ValidateLogInvoice";        
        #endregion
        
        #region Property

        public string InvoiceNo { get; set; }

        public string InvoiceDate { get; set; }
        
        public string CustomerOrderNo { get; set; }

        public string LogNo { get; set; }

        public string OrderDate { get; set; }

        public int PCId { get; set; }

        public int BOId { get; set; }

        public string TINNo { get; set; }

        public Int32 DistributorId { get; set; }

        public decimal TaxAmount { get; set; }

        public decimal DisplayTaxAmount
        {
            get { return Math.Round(DBTaxAmount, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property can not be explicitly set"); }
        }

        public decimal DBTaxAmount
        {
            get { return Math.Round(TaxAmount, Common.DBAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property can not be explicitly set"); }
        }
       

        public decimal InvoiceAmount { get; set; }

        public decimal DisplayInvoiceAmount
        {
            get { return Math.Round(DBInvoiceAmount, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property can not be explicitly set"); }
        }

        public decimal DBInvoiceAmount
        {
            get { return Math.Round(InvoiceAmount, Common.DBAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property can not be explicitly set"); }
        }

        public decimal DiscountAmount { get; set; }

        public decimal DisplayDiscountAmount
        {
            get { return Math.Round(DBDiscountAmount, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property can not be explicitly set"); }
        }

        public decimal DBDiscountAmount
        {
            get { return Math.Round(DiscountAmount, Common.DBAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property can not be explicitly set"); }
        }

        public decimal PaymentAmount { get; set; }

        public decimal DisplayPaymentAmount
        {
            get { return Math.Round(DBPaymentAmount, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property can not be explicitly set"); }
        }

        public decimal DBPaymentAmount
        {
            get { return Math.Round(PaymentAmount, Common.DBAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property can not be explicitly set"); }
        }


        public decimal ChangeAmount { get; set; }

        public decimal DisplayChangeAmount
        {
            get { return Math.Round(DBChangeAmount, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property can not be explicitly set"); }
        }

        public decimal DBChangeAmount
        {
            get { return Math.Round(ChangeAmount, Common.DBAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property can not be explicitly set"); }
        }

        public string DistributorName { get; set; }

        public decimal TotalBV { get; set; }

        public decimal DisplayTotalBV
        {
            get { return Math.Round(DBTotalBV, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property can not be explicitly set"); }
        }

        public decimal DBTotalBV
        {
            get { return Math.Round(TotalBV, Common.DBAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property can not be explicitly set"); }
        }

        public decimal TotalPV { get; set; }

        public decimal DisplayTotalPV
        {
            get { return Math.Round(DBTotalPV, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property can not be explicitly set"); }
        }

        public decimal DBTotalPV
        {
            get { return Math.Round(TotalPV, Common.DBAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property can not be explicitly set"); }
        }

        public Int32 Status { get; set; }

        public string StatusName
        {
            get
            {
                return Enum.Parse(typeof(Common.OrderStatus), this.Status.ToString()).ToString();
            }
            set
            {
                throw new NotImplementedException("This property is not intended to be implemented");
            }
        }

        public string DeliveryMode { get; set; }

        public Int32 IsProcessed { get; set; }

        public int CreatedBy { get; set; }

        public string CreatedDate { get; set; }

        public int ModifiedBy { get; set; }

        public string ModifiedDate { get; set; }

        public Int32 StaffId { get; set; }

        public string DeliverTo { get; set; }

        public int DeliverFromId { get; set; }

        public Address DeliverToAddress { get; set; }

        public Address DeliverFromAddress { get; set; }

        public decimal TotalUnits { get; set; }

        public decimal DisplayTotalUnits
        {
            get { return Math.Round(DBTotalUnits, Common.DisplayQtyRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property can not be explicitly set"); }
        }

        public decimal DBTotalUnits
        {
            get { return Math.Round(TotalUnits, Common.DBQtyRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property can not be explicitly set"); }
        }

        public decimal TotalWeight { get; set; }

        public decimal DisplayTotalWeight
        {
            get { return Math.Round(DBTotalWeight, Common.DisplayQtyRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property can not be explicitly set"); }
        }

        public decimal DBTotalWeight
        {
            get { return Math.Round(TotalWeight, Common.DBQtyRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property can not be explicitly set"); }
        }

        public Boolean InvoicePrinted { get; set; }

        public Int32 OrderMode { get; set; }

        public string TerminalCode { get; set; }

        public List<CIDetail> CIDetailList { get; set; }

        public List<CIPayment> CIPaymentList { get; set; }

        public string FromDate { get; set; }

        public string ToDate { get; set; }

        #endregion

        public bool Save(ref string errorMessage)
        {
            DataTaskManager dtManager = null;
            try
            {
                DBParameterList dbParam;
                bool isSuccess = false;
                using (dtManager = new DataTaskManager())
                {
                    try
                    {
                        dtManager.BeginTransaction();
                        {
                            string xmlDoc = Common.ToXml(this);

                            dbParam = new DBParameterList();
                            dbParam.Add(new DBParameter(Common.PARAM_DATA, xmlDoc, DbType.String));

                            dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, errorMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                            DataTable dt = dtManager.ExecuteDataTable(SP_CI_SAVE, dbParam);

                            errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();
                            {
                                if (errorMessage.Length > 0)
                                {
                                    isSuccess = false;
                                    dtManager.RollbackTransaction();                                         
                                }
                                else
                                {
                                    isSuccess = true;
                                    dtManager.CommitTransaction();
                                    this.InvoiceNo = Convert.ToString(dt.Rows[0]["InvoiceNo"]); 
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        if (dtManager != null)
                        {
                            dtManager.RollbackTransaction();
                        }
                        throw ex;
                    }
                }
                return isSuccess;
            }
            catch (Exception ex)
            {                
                throw ex;
            }

        }

        public bool Save(List<CI> CIList, ref string errorMessage)
        {
            DataTaskManager dtManager = null;
            try
            {
                DBParameterList dbParam;
                bool isSuccess = false;
                using (dtManager = new DataTaskManager())
                {
                    dtManager.BeginTransaction();
                    foreach (CI invoice in CIList)
                    {
                        try
                        {
                            string xmlDoc = Common.ToXml(invoice);

                            dbParam = new DBParameterList();
                            dbParam.Add(new DBParameter(Common.PARAM_DATA, xmlDoc, DbType.String));

                            dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, errorMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                            DataTable dt = dtManager.ExecuteDataTable(SP_CI_SAVE, dbParam);

                            errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();
                            {
                                if (errorMessage.Length > 0)
                                {
                                    isSuccess = false;
                                    dtManager.RollbackTransaction();
                                    break;
                                }
                                else
                                {
                                    isSuccess = true;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            if (dtManager != null)
                            {
                                dtManager.RollbackTransaction();
                            }
                            throw ex;
                        }
                    }
                    if (isSuccess)
                    {
                        dtManager.CommitTransaction();
                    }
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                if (dtManager != null)
                {
                    dtManager.RollbackTransaction();
                }
                throw ex;
            }

        }

        public static bool CheckBatchForLog(string LogNo,int locationId,ref string Message, ref DataTable dtItems)
        {
            
            System.Data.DataTable dTable = new DataTable();
            DBParameterList dbParam;
            try
            {
                Vinculum.Framework.Data.DataTaskManager dtManager = new DataTaskManager();
                // initialize the parameter list object
                dbParam = new DBParameterList();

                dbParam.Add(new DBParameter("@logNo", LogNo, DbType.String));
                dbParam.Add(new DBParameter("@locationId", locationId, DbType.Int32));                
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));
               
                dtItems = dtManager.ExecuteDataTable(SP_CI_LOGCHECK, dbParam);
                
                Message = dbParam[Common.PARAM_OUTPUT].Value.ToString();

                // if an error returned from the database
                if (string.IsNullOrEmpty(Message))
                    return true;
                else
                    return false;                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        private bool ValidateInvoice(List<CODetail> CODetailList,List<CIBatchDetail>ListBatch, ref string error)
        {

            try
            {
                string errMsg;
                bool isvalid = true; decimal totalQty = 0;
                if (CODetailList != null && CODetailList.Count > 0)
                {
                    foreach (CODetail d in CODetailList)
                    {
                        var q1 = from q in ListBatch where q.ItemId == d.ItemId && q.RecordNo == d.RecordNo select q.Quantity;
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
                                error = Common.GetMessage("40037", d.ItemName, Math.Floor(totalQty).ToString(), Math.Floor(d.Qty).ToString(), d.CustomerOrderNo);
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

        private CI CreateInvoiceObject(CO m_Order, List<CIBatchDetail> ListBatch,int m_UserID)
        {
            try
            {
                CI invoice = new CI();
                invoice.InvoiceNo = string.Empty;
                invoice.CreatedBy = m_UserID;
                invoice.CustomerOrderNo = m_Order.CustomerOrderNo;
                invoice.InvoiceAmount = m_Order.OrderAmount;
                invoice.DistributorId = m_Order.DistributorId;
                invoice.BOId = m_Order.BOId;
                invoice.PCId = m_Order.PCId;
                invoice.InvoicePrinted = false;
                invoice.IsProcessed = 0;
                invoice.LogNo = m_Order.LogNo;
                invoice.ModifiedBy = m_UserID;
                invoice.ModifiedDate = DateTime.Today.ToString();
                invoice.StaffId = m_UserID;
                invoice.Status = 1;
                invoice.TINNo = Common.TINNO; // ConfigurationManager.AppSettings["TINNO"];
                invoice.CIDetailList = new List<CIDetail>();
                foreach (CODetail detail in m_Order.CODetailList)
                {
                    CIDetail ciDetail = new CIDetail();
                    ciDetail.ItemCode = detail.ItemCode;
                    ciDetail.ItemId = detail.ItemId;
                    var query = from q in ListBatch where q.ItemId == detail.ItemId && q.RecordNo == detail.RecordNo select q;
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

        public bool ProcessLogInvoice(string logNo, int LocationID, int userID, CO lstCo, ref String ErrorCode, ref DataTable dtItems)
        {
            try
            {
                bool isSuccess = false;
                bool isValid = true;
                string errorMessage = string.Empty;
                //Check Batch Qty Not Available for all Items of orders
                isValid = CI.CheckBatchForLog(logNo, LocationID, ref errorMessage, ref dtItems);
                //if (isValid)
                //{
                List<CI> CIList = new List<CI>();
                CO order = new CO();
                order.LogNo = logNo;
                order.Status = (int)Common.OrderStatus.Confirmed;
                errorMessage = string.Empty;
                //List<CO> lstCo = order.Search(ref errorMessage);
                
                if (errorMessage.Equals(string.Empty) && lstCo != null)
                {
                    DataTaskManager dtManager = null;
                    try
                    {
                        using (dtManager = new DataTaskManager())
                        {
                            dtManager.BeginTransaction();
                            try
                            {
                                //foreach (CO o in lstCo)
                                //{
                                    CO m_Order = new CO();
                                    m_Order.GetCOAllDetails(lstCo.CustomerOrderNo, -1);
                                    errorMessage = string.Empty;
                                    List<CIBatchDetail> ListBatch = CIBatchDetail.GetDefaultBatch(m_Order.CustomerOrderNo, LocationID, ref errorMessage);
                                    CI invoice = CreateInvoiceObject(m_Order, ListBatch, userID);
                                    if (invoice != null)
                                    {
                                        string ValidationCode = string.Empty;
                                        isValid = invoice.ValidateInvoice(m_Order.CODetailList, ListBatch, ref ValidationCode);
                                        if (isValid)
                                        {
                                            bool isSaved = invoice.Save(ref ValidationCode);
                                            //isSuccess = true;
                                            if (!isSaved)
                                            {
                                                isSuccess = false;
                                            }
                                            else
                                            {
                                                dtManager.CommitTransaction();
                                                ErrorCode = string.Empty;
                                                isSuccess = true;
                                            }

                                        }
                                        else
                                        {
                                            if (ValidationCode == "40007")
                                            {
                                                // Not sufficent Batch Qty present
                                                ErrorCode = "40008";
                                            }
                                            else
                                            {
                                                //ErrorCode = ValidationCode;
                                            }
                                            ErrorCode = "40008";
                                            isSuccess = false;
                                            dtManager.RollbackTransaction();
                                            //break;
                                        }
                                    }
                                    else
                                    {
                                        ErrorCode = "40029," + m_Order.CustomerOrderNo;
                                        isSuccess = false;
                                        dtManager.RollbackTransaction();
                                        //break;
                                    }
                                //}

                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Common.LogException(ex);
                    }

                }
                else
                {
                    //No Order found in confirmed status
                    ErrorCode = "40011";
                }
                //} 
                // Error in batch qty check
                //else if (!errorMessage.Trim().Equals(string.Empty))
                //{
                //    if (errorMessage.Trim().IndexOf("30001:") >= 0)
                //    {
                //        throw new Exception(errorMessage.Trim());
                //    }
                //    else
                //    {
                //        ErrorCode = errorMessage.Trim();
                //    }
                //}
                return isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ProcessLog(string logNo, int LocationID, int userID, ref String ErrorCode, ref DataTable dtItems)
        {
            try
            {
                bool isSuccess = false;
                bool isValid = true;
                string errorMessage = string.Empty;
                //Check Batch Qty Not Available for all Items of orders
                isValid = CI.CheckBatchForLog(logNo, LocationID, ref errorMessage, ref dtItems);
                if (isValid)
                {
                    List<CI> CIList = new List<CI>();
                    CO order = new CO();
                    order.LogNo = logNo;
                    order.Status = (int)Common.OrderStatus.Confirmed;
                    errorMessage = string.Empty;
                    List<CO> lstCo = order.Search(ref errorMessage);
                    if (errorMessage.Equals(string.Empty) && lstCo != null && lstCo.Count > 0)
                    {
                        DataTaskManager dtManager = null;
                        try
                        {
                            using (dtManager = new DataTaskManager())
                            {
                                dtManager.BeginTransaction();
                                try
                                {
                                    foreach (CO o in lstCo)
                                    {
                                        CO m_Order = new CO();
                                        m_Order.GetCOAllDetails(o.CustomerOrderNo, -1);
                                        errorMessage = string.Empty;
                                        List<CIBatchDetail> ListBatch = CIBatchDetail.GetDefaultBatch(m_Order.CustomerOrderNo, LocationID, ref errorMessage);
                                        CI invoice = CreateInvoiceObject(m_Order, ListBatch, userID);
                                        if (invoice != null)
                                        {
                                            string ValidationCode = string.Empty;
                                            isValid = invoice.ValidateInvoice(m_Order.CODetailList, ListBatch, ref ValidationCode);
                                            if (isValid)
                                            {
                                                bool isSaved = invoice.Save(ref ValidationCode);
                                                if (!isSaved)
                                                {
                                                    isSuccess = false;
                                                    dtManager.RollbackTransaction();
                                                }
                                            }
                                            else
                                            {
                                                if (ValidationCode == "40007")
                                                {
                                                    // Not sufficent Batch Qty present
                                                    ErrorCode = "40008";
                                                }
                                                else
                                                {
                                                    ErrorCode = ValidationCode;
                                                }
                                                isSuccess = false;
                                                dtManager.RollbackTransaction();
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            ErrorCode = "40029," + m_Order.CustomerOrderNo;
                                            isSuccess = false;
                                            dtManager.RollbackTransaction();
                                            break;
                                        }
                                    }
                                    dtManager.CommitTransaction();
                                    ErrorCode = string.Empty;
                                    isSuccess = true;
                                }
                                catch (Exception ex)
                                {
                                    throw ex;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Common.LogException(ex);
                        }

                    }
                    else
                    {
                        //No Order found in confirmed status
                        ErrorCode = "40011";
                    }
                }
                // Error in batch qty check
                else if (!errorMessage.Trim().Equals(string.Empty))
                {
                    if (errorMessage.Trim().IndexOf("30001:") >= 0)
                    {
                        throw new Exception(errorMessage.Trim());
                    }
                    else
                    {
                        ErrorCode = errorMessage.Trim();
                    }
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ProcessLog(string logNo, int LocationID, int userID,List<CO> lstCo, ref String ErrorCode)
        {
            try
            {
                bool isSuccess = false;
                bool isValid = true;
                string errorMessage = string.Empty;
                //Check Batch Qty Not Available for all Items of orders
                //isValid = CI.CheckBatchForLog(logNo, LocationID, ref errorMessage);
                //if (isValid)
                {
                    List<CI> CIList = new List<CI>();
                    errorMessage = string.Empty;
                    if (lstCo != null && lstCo.Count > 0)
                    {
                        DataTaskManager dtManager = null;
                        try
                        {
                            using (dtManager = new DataTaskManager())
                            {
                                //dtManager.BeginTransaction();
                                try
                                {
                                    foreach (CO o in lstCo)
                                    {
                                        dtManager.BeginTransaction();
                                        CO COrder = new CO();
                                        COrder.GetCOAllDetails(o.CustomerOrderNo, -1);
                                        errorMessage = string.Empty;
                                        List<CIBatchDetail> ListBatch = CIBatchDetail.GetDefaultBatch(COrder.CustomerOrderNo, LocationID, ref errorMessage);
                                        CI invoice = CreateInvoiceObject(COrder, ListBatch, userID);
                                        if (invoice != null)
                                        {
                                            string ValidationCode = string.Empty;
                                            isValid = invoice.ValidateInvoice(COrder.CODetailList, ListBatch, ref ValidationCode);
                                            errorMessage = errorMessage + ValidationCode;
                                            if (isValid)
                                            {
                                                bool isSaved = invoice.Save(ref ValidationCode);
                                                if (!isSaved)
                                                {
                                                    isSuccess = false;
                                                    ErrorCode = ValidationCode;
                                                    dtManager.RollbackTransaction();
                                                    //return isSuccess;
                                                }
                                                else
                                                {
                                                    dtManager.CommitTransaction();
                                                    ErrorCode = string.Empty;
                                                    isSuccess = true;
                                                }
                                            }
                                            else
                                            {
                                                if (ValidationCode == "40007")
                                                {
                                                    // Not sufficent Batch Qty present
                                                    ErrorCode = "40008";
                                                }
                                                else
                                                {
                                                    ErrorCode = ValidationCode;
                                                }
                                                isSuccess = false;
                                                dtManager.RollbackTransaction();
                                                //break;
                                            }
                                        }
                                        else
                                        {
                                            ErrorCode = "40029," + COrder.CustomerOrderNo;
                                            isSuccess = false;
                                            dtManager.RollbackTransaction();
                                            break;
                                        }
                                    }
                                    
                                    //dtManager.CommitTransaction();
                                    //ErrorCode = string.Empty;
                                    //isSuccess = true;
                                    //return isSuccess;
                                }
                                
                                catch (Exception ex)
                                {
                                    throw ex;
                                }
                            }
                            
                            //return isSuccess;
                        }
                        catch (Exception ex)
                        {
                            Common.LogException(ex);
                        }

                    }
                    else
                    {
                        //No Order found in confirmed status
                        ErrorCode = "40011";
                    }
                }
                // Error in batch qty check
                ////else if (!errorMessage.Trim().Equals(string.Empty))
                //{
                //    if (errorMessage.Trim().IndexOf("30001:") >= 0)
                //    {
                //        throw new Exception(errorMessage.Trim());
                //    }
                //    else
                //    {
                //        ErrorCode = errorMessage.Trim();
                //    }
                //}
                if (!errorMessage.Equals(""))
                {
                    MessageBox.Show(errorMessage, Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}
