using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CoreComponent.Core.BusinessObjects;
using Vinculum.Framework.Data;
using Vinculum.Framework.DataTypes;
namespace POSClient.BusinessObjects
{
    [Serializable]
    public class CO
    {
        #region SP_Declaration
        private const string SP_CO_UPDATE = "usp_UpdateOrderLog";
        private const string SP_CO_SEARCH = "usp_OrderSearch";
        private const string SP_CO_CREATE = "usp_POSOrderSave";
        private const string SP_CODETAIL_SEARCH = "usp_CODetailSearch";
        private const string SP_CO_Detail = "usp_GetOrderDetail";
        private const string SP_ORDER_PRINT = "usp_GetOrderPrint";
        private const string SP_PUC_CHECK_AMOUNT = "usp_PUCCheckAmount";
        private const string SP_CO_DELETEPAYEMNT = "usp_DeleteCOPayment";

        #endregion

        #region C'tors

        public CO()
        {
            this.Status = (int)Common.OrderStatus.Created;
            this.CODetailList = new List<CODetail>();
            this.COPaymentList = new List<COPayment>();
            this.DeliverFromAddress = new Address();
            this.DeliverToAddress = new Address();
        }

        #endregion

        #region Property

        public bool IsFirstOrderForDistributor { get; set; }

        public decimal MinimumOrderAmount { get; set; }

        public string SourceLocationCode { get; set; }

        public string CustomerOrderNo { get; set; }

        public string OrderDate { get; set; }

        public string DisplayOrderDate
        {
            get { return Convert.ToDateTime(OrderDate).ToString(Common.DTP_DATE_FORMAT); }
            set { OrderDate = value; }
        }
        public string InvoiceNo { get; set; }
        public string LogNo { get; set; }
        public string LogValue { get; set; }

        public string IsPrintAllowed { get; set; }
        public string ValidReportPrintDays { get; set; }
        public string InvoiceDate { get; set; }

        public Int32 DistributorId { get; set; }

        public string DistributorName { get; set; }

        public Address DeliverToAddress { get; set; }

        public string DeliverToAddressLine1
        {
            get { return this.DeliverToAddress.Address1; }
            set { throw new NotImplementedException("This Property can not be explicitly set"); }
        }

        public string DeliverToAddressLine2
        {
            get { return this.DeliverToAddress.Address2; }
            set { throw new NotImplementedException("This Property can not be explicitly set"); }
        }

        public string DeliverToAddressLine3
        {
            get { return this.DeliverToAddress.Address3; }
            set { throw new NotImplementedException("This Property can not be explicitly set"); }
        }

        public string DeliverToAddressLine4
        {
            get { return this.DeliverToAddress.Address4; }
            set { throw new NotImplementedException("This Property can not be explicitly set"); }
        }

        public int DeliverToCityId
        {
            get { return this.DeliverToAddress.CityId; }
            set { throw new NotImplementedException("This Property can not be explicitly set"); }
        }

        public string DeliverToPincode
        {
            get { return this.DeliverToAddress.PinCode; }
            set { throw new NotImplementedException("This Property can not be explicitly set"); }
        }

        public int DeliverToStateId
        {
            get { return this.DeliverToAddress.StateId; }
            set { throw new NotImplementedException("This Property can not be explicitly set"); }
        }

        public int DeliverToCountryId
        {
            get { return this.DeliverToAddress.CountryId; }
            set { throw new NotImplementedException("This Property can not be explicitly set"); }
        }

        public string DeliverToTelephone
        {
            get { return this.DeliverToAddress.PhoneNumber1; }
            set { throw new NotImplementedException("This Property can not be explicitly set"); }
        }

        public string DeliverToMobile
        {
            get { return this.DeliverToAddress.Mobile1; }
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

        private decimal m_totalUnits;
        public decimal TotalUnits
        {
            get { return GetTotalUnits();}
            set { m_totalUnits = value; }
        }

        private decimal m_totalWeight;
        public decimal TotalWeight 
        {
            get { return GetTotalWeight();}
            set { m_totalWeight = value;}
        }

        private decimal m_orderAmount = 0;
        public decimal OrderAmount
        {
            get { return GetOrderAmount(); }
            set { m_orderAmount = value; }
        }

        public decimal RoundedTotalWeight
        {
            get { return Math.Round(DBRoundedTotalWeight, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }

        public decimal DBRoundedTotalWeight
        {
            get { return Math.Round(TotalWeight, Common.DBAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }

        public decimal RoundedOrderAmount
        {
            get { return Math.Round(DBRoundedOrderAmount, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }

        public decimal DBRoundedOrderAmount
        {
            get { return Math.Round(OrderAmount, Common.DBAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }

        private decimal m_discountAmount = 0;
        public decimal DiscountAmount
        {
            get { return GetDiscountAmount(); }
            set { m_discountAmount = value; }
        }

        public decimal RoundedDiscountAmount
        {
            get { return Math.Round(DBRoundedDiscountAmount, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }

        public decimal DBRoundedDiscountAmount
        {
            get { return Math.Round(DiscountAmount, Common.DBAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }

        private decimal m_taxAmount = 0;
        public decimal TaxAmount
        {
            get { return GetTaxAmount(); }
            set { m_taxAmount = value; }
        }

        public decimal RoundedTaxAmount
        {
            get { return Math.Round(DBRoundedTaxAmount, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }

        public decimal DBRoundedTaxAmount
        {
            get { return Math.Round(TaxAmount, Common.DBAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }

        private decimal m_paymentAmount = 0;
        public decimal PaymentAmount
        {
            get { return GetPaymentAmount(); }
            set { m_paymentAmount = value; }
        }

        public decimal RoundedPaymentAmount
        {
            get { return Math.Round(DBRoundedPaymentAmount, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }

        public decimal DBRoundedPaymentAmount
        {
            get { return Math.Round(PaymentAmount, Common.DBAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }

        //private decimal m_totalQty;
        //public decimal TotalQty
        //{
        //    get 
        //    {
        //        return GetTotalQty();
        //    }
        //    set { m_totalQty = value; }
        //}
        public decimal RoundedTotalUnits
        {
            get { return Math.Round(DBRoundedTotalUnits, Common.DisplayQtyRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
        public decimal DBRoundedTotalUnits
        {
            get { return Math.Round(TotalUnits, Common.DBQtyRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
        private decimal m_changeAmount = 0;
        public decimal ChangeAmount
        {
            get { return GetChangeAmount(); }
            set { m_changeAmount = value; }
        }

        public decimal RoundedChangeAmount
        {
            get { return Math.Round(DBRoundedChangeAmount, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }

        public decimal DBRoundedChangeAmount
        {
            get { return Math.Round(ChangeAmount, Common.DBAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
        private decimal m_totalAmount = 0;
        public decimal TotalAmount
        {
            get { return this.OrderAmount + this.TaxAmount; }
            set { m_totalAmount = value; }
        }

        public decimal RoundedTotalAmount
        {
            get { return Math.Round(DBRoundedTotalAmount, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }

        public decimal DBRoundedTotalAmount
        {
            get { return Math.Round(TotalAmount, Common.DBAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }



        public int OrderType { get; set; }

        public string TaxJurisdictionId
        {
            get;
            set;
        }

        public string OrderTypeName
        {
            get
            {
                return Enum.Parse(typeof(Common.OrderType), this.OrderType.ToString()).ToString();
            }
            set
            {
                throw new NotImplementedException("This property is not intended to be implemented");
            }
        }

        public int PCId { get; set; }

        public string PCCode { get; set; }

        public int BOId { get; set; }

        public string DistributorAddress { get; set; }

        public Address DeliverFromAddress { get; set; }

        public string DeliverFromAddressLine1
        {
            get { return this.DeliverFromAddress.Address1; }
            set { throw new NotImplementedException("This Property can not be explicitly set"); }
        }

        public string DeliverFromAddressLine2
        {
            get { return this.DeliverFromAddress.Address2; }
            set { throw new NotImplementedException("This Property can not be explicitly set"); }
        }

        public string DeliverFromAddressLine3
        {
            get { return this.DeliverFromAddress.Address3; }
            set { throw new NotImplementedException("This Property can not be explicitly set"); }
        }

        public string DeliverFromAddressLine4
        {
            get { return this.DeliverFromAddress.Address4; }
            set { throw new NotImplementedException("This Property can not be explicitly set"); }
        }

        public int DeliverFromCityId
        {
            get { return this.DeliverFromAddress.CityId; }
            set { throw new NotImplementedException("This Property can not be explicitly set"); }
        }

        public string DeliverFromPincode
        {
            get { return this.DeliverFromAddress.PinCode; }
            set { throw new NotImplementedException("This Property can not be explicitly set"); }
        }

        public int DeliverFromStateId
        {
            get { return this.DeliverFromAddress.StateId; }
            set { throw new NotImplementedException("This Property can not be explicitly set"); }
        }

        public int DeliverFromCountryId
        {
            get { return this.DeliverFromAddress.CountryId; }
            set { throw new NotImplementedException("This Property can not be explicitly set"); }
        }

        public string DeliverFromTelephone
        {
            get { return this.DeliverFromAddress.PhoneNumber1; }
            set { throw new NotImplementedException("This Property can not be explicitly set"); }
        }

        public string DeliverFromMobile
        {
            get { return this.DeliverFromAddress.Mobile1; }
            set { throw new NotImplementedException("This Property can not be explicitly set"); }
        }

        public Boolean UsedForRegistration { get; set; }

        public Int32 OrderMode { get; set; }

        public string TerminalCode { get; set; }

        private decimal m_totalBV;

        public decimal TotalBV {
            get
            {
                  return GetTotalBV(); 
            }
            set { m_totalBV = value; } 
        }

        public decimal RoundedTotalBV 
        { 
            get { return Math.Round(DBRoundedTotalBV, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException(); }
        }
        
        public decimal DBRoundedTotalBV 
        {
            get { return Math.Round(TotalBV, Common.DBAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException();} 
        }

        private decimal m_totalPV;

        public decimal TotalPV {
            get 
            {
                return GetTotalPV();
            }
            set { m_totalPV = value; }
        }

        public decimal RoundedTotalPV {
            get { return Math.Round(DBRoundedTotalPV, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException(); } 
        }

        public decimal DBRoundedTotalPV 
        {
            get { return Math.Round(TotalPV, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException(); }
        }

        public List<CODetail> CODetailList { get; set; }

        public List<COPayment> COPaymentList { get; set; }

        public int CreatedBy { get; set; }

        public string CreatedByName { get; set; }

        public string CreatedDate { get; set; }

        public int ModifiedBy { get; set; }

        public string ModifiedDate { get; set; }

        public string FromDate { get; set; }

        public string ToDate { get; set; }

        #endregion

        #region Public Methods

        public static DataSet GetOrderForPrint(int type, string customerOrderNo, ref string errorMessage)
        {
            try
            {
                DataSet ds = new DataSet();
                using (DataTaskManager dtManager = new DataTaskManager())
                {
                    DBParameterList dbParam = new DBParameterList();
                    dbParam.Add(new DBParameter("@type", type, DbType.Int32));
                    dbParam.Add(new DBParameter("@orderId", customerOrderNo, DbType.String));
                    dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, errorMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                    ds = dtManager.ExecuteDataSet(SP_ORDER_PRINT, dbParam);
                }
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Search With XML
        public List<CO> Search(ref string errorMessage)
        {
            List<CO> OrderList = new List<CO>();
            System.Data.DataTable dTable = new DataTable();
            try
            {
                dTable = GetSelectedRecords(Common.ToXml(this), SP_CO_SEARCH, ref errorMessage);

                if (dTable != null)
                {
                    foreach (System.Data.DataRow drow in dTable.Rows)
                    {
                        CO order = new CO();
                        order.CreateCOObject(drow);
                        OrderList.Add(order);
                    }
                }
                return OrderList;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool Save(List<CO> COList, ref string errorMessage)
        {
            DataTaskManager dtManager = null;
            try
            {
                DBParameterList dbParam;
                bool isSuccess = false;
                using (dtManager = new DataTaskManager())
                {
                    dtManager.BeginTransaction();
                    {
                        string xmlDoc = Common.ToXml(COList);

                        dbParam = new DBParameterList();
                        dbParam.Add(new DBParameter(Common.PARAM_DATA, xmlDoc, DbType.String));

                        dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, errorMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                        DataTable dt = dtManager.ExecuteDataTable(SP_CO_UPDATE, dbParam);

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
                            }
                        }
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

        public bool Save(int statusParam, ref string validationMessage, ref string dbMessage)
        {

            try
            {
                int i = 0, j = 0;
                if (statusParam == (int)Common.OrderStatus.Created)
                {
                    foreach (CODetail d in this.CODetailList)
                    {
                        d.RecordNo = ++i;
                        foreach (CODetailTax dt in d.CODetailTaxList)
                        {
                            dt.RecordNo = d.RecordNo;
                            dt.TaxRecordNo = ++j;
                        }

                        foreach (CODetailDiscount dd in d.CODiscountList)
                        {
                            dd.RecordNo = d.RecordNo;
                        }
                    }
                }
                else if (statusParam == (int)Common.OrderStatus.Confirmed)
                {
                    i = 0;
                    foreach (COPayment p in this.COPaymentList)
                    {
                        p.RecordNo = ++i;
                    }
                    int index = (from p in this.CODetailList select p.RecordNo).Max();

                    List<CODetail> giftVoucherList = this.CODetailList.FindAll(delegate(CODetail d) { return string.IsNullOrEmpty(d.GiftVoucherNumber) == false; });
                    for (int k = 0; k < giftVoucherList.Count; k++)
                    {
                        giftVoucherList[k].RecordNo = ++index;
                        foreach (CODetailDiscount cd in giftVoucherList[k].CODiscountList)
                        {
                            cd.RecordNo = giftVoucherList[k].RecordNo;
                        }
                    }
                }
                //else if (statusParam == (int)Common.OrderStatus.Modify)
                //{
                //    int index = (from p in this.CODetailList select p.RecordNo).Max();

                //    List<CODetail> giftVoucherList = this.CODetailList.FindAll(delegate(CODetail d) { return string.IsNullOrEmpty(d.GiftVoucherNumber) == false; });
                //    for (int k = 0; k < giftVoucherList.Count; k++)
                //    {
                //        giftVoucherList[k].RecordNo = ++index;
                //        foreach (CODetailDiscount cd in giftVoucherList[k].CODiscountList)
                //        {
                //            cd.RecordNo = giftVoucherList[k].RecordNo;
                //        }
                //    }
                //}
                bool isSuccess = false;
                using (DataTaskManager dtManager = new DataTaskManager())
                {
                    DBParameterList dbParam;
                    dbParam = new DBParameterList();
                    dbParam.Add(new DBParameter(Common.PARAM_DATA, Common.ToXml(this), DbType.String));
                    dbParam.Add(new DBParameter("@validationMessage", validationMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));
                    dbParam.Add(new DBParameter("@statusParam", statusParam, DbType.Int32));
                    dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, dbMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                    DataSet ds = dtManager.ExecuteDataSet(SP_CO_CREATE, dbParam);
                    validationMessage = dbParam["@validationMessage"].Value.ToString();
                    dbMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();

                    if (ds != null && ds.Tables.Count > 0 && string.IsNullOrEmpty(validationMessage) && string.IsNullOrEmpty(dbMessage))
                    {
                        isSuccess = true;
                        if (string.IsNullOrEmpty(this.CustomerOrderNo))
                        {
                            this.CustomerOrderNo = ds.Tables[ds.Tables.Count - 1].Rows[0]["OrderId"].ToString();
                            this.CreatedDate = this.ModifiedDate = ds.Tables[ds.Tables.Count - 1].Rows[0]["CreatedDate"].ToString();
                        }
                        this.Status = Convert.ToInt32(ds.Tables[ds.Tables.Count - 1].Rows[0]["Status"]);
                    }
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteCourierPayment(string CustomerOrderNo, string TenderType, ref string dbMessage)
        {
            using (DataTaskManager dtManager = new DataTaskManager())
            {
                bool isSuccess = false;
                DBParameterList dbParam;
                dbParam = new DBParameterList();
                dbParam.Add(new DBParameter("@CustomerOrderNo", CustomerOrderNo, DbType.String));
                dbParam.Add(new DBParameter("@TenderType", TenderType, DbType.Int32));
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, dbMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                DataSet ds = dtManager.ExecuteDataSet(SP_CO_DELETEPAYEMNT, dbParam);
                dbMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();
                if (dbMessage.Equals(string.Empty))
                {
                    isSuccess = true;
                }
                return isSuccess;
            }
        }


        public bool Save(int statusParam,CO existingOrder,ref string validationMessage, ref string dbMessage)
        {

            try
            {               
                if (statusParam == (int)Common.OrderStatus.Modify)
                {
                    int index = (from p in existingOrder.CODetailList select p.RecordNo).Max();
                    
                    foreach (CODetail detail in this.CODetailList)
                    {
                        var query = (from p in existingOrder.CODetailList where p.ItemId==detail.ItemId select p.ItemId);
                        if (query == null || query.ToList().Count == 0)
                            detail.RecordNo = ++index;
                    }
                    int paymentindex = (from p in existingOrder.COPaymentList select p.RecordNo).Max();
                    List<COPayment> newPayments = new List<COPayment>();
                    var q1= (from p in this.COPaymentList where p.RecordNo == 0 select p);
                    if (q1 != null && q1.ToList().Count > 0)
                        newPayments = q1.ToList<COPayment>();
                    foreach (COPayment p in newPayments)
                    {
                        p.RecordNo = ++paymentindex;
                    }
                }          
                
                bool isSuccess = false;
                using (DataTaskManager dtManager = new DataTaskManager())
                {
                    DBParameterList dbParam;
                    dbParam = new DBParameterList();
                    dbParam.Add(new DBParameter(Common.PARAM_DATA, Common.ToXml(this), DbType.String));
                    dbParam.Add(new DBParameter("@validationMessage", validationMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));
                    dbParam.Add(new DBParameter("@statusParam", statusParam, DbType.Int32));
                    dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, dbMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                    DataSet ds = dtManager.ExecuteDataSet(SP_CO_CREATE, dbParam);
                    validationMessage = dbParam["@validationMessage"].Value.ToString();
                    dbMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();

                    if (ds != null && ds.Tables.Count > 0 && string.IsNullOrEmpty(validationMessage) && string.IsNullOrEmpty(dbMessage))
                    {
                        isSuccess = true;
                        if (string.IsNullOrEmpty(this.CustomerOrderNo))
                        {
                            this.CustomerOrderNo = ds.Tables[ds.Tables.Count - 1].Rows[0]["OrderId"].ToString();
                            this.CreatedDate = this.ModifiedDate = ds.Tables[ds.Tables.Count - 1].Rows[0]["CreatedDate"].ToString();
                        }
                        this.Status = Convert.ToInt32(ds.Tables[ds.Tables.Count - 1].Rows[0]["Status"]);
                    }
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Bikram: Get Loaction Courier amount
        /// </summary>
        /// <returns></returns>
        public static decimal GetWaiveoffCourierLimit()
        {
            decimal WaiveoffCourierAmount = 0;
            DataTable dtWaiveOffAmount = Common.ParameterLookup(Common.ParameterType.WaiveoffCourierLimit, new ParameterFilter("WaiveoffCourierLimit", 0, 0, 0));
            if (dtWaiveOffAmount!= null)
            {
                foreach (DataRow dr in dtWaiveOffAmount.Rows)
                {
                    if (dr[1].ToString().Trim().ToUpper() == Common.LocationCode.ToUpper())
                    {
                        WaiveoffCourierAmount = Convert.ToDecimal(dr[0].ToString());
                        break;
                    }
                }
            }
            return WaiveoffCourierAmount;
        }
        //GET All Details of CO
        public void GetCOAllDetails(string OrderNo, int Status)
        {
            if (!OrderNo.Equals(string.Empty))
            {
                string errorMessage = string.Empty;
                List<CO> OrderList = new List<CO>();
                System.Data.DataSet dSet = new DataSet();
                try
                {
                    DBParameterList dbParam = new DBParameterList();
                    dbParam.Add(new DBParameter("@CustomerOrderNo", OrderNo, DbType.String));
                    dbParam.Add(new DBParameter("@Status", Status, DbType.String));
                    dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                    dSet = GetSelectedRecordsDataSet(dbParam, SP_CO_Detail, ref errorMessage);

                    if (dSet != null && dSet.Tables.Count > 0)
                    {
                        foreach (System.Data.DataRow drow in dSet.Tables[0].Rows)
                        {
                            CreateCOObject(drow);
                            if (dSet.Tables[1] != null)
                                this.GetCOPaymentDetail(dSet.Tables[1]);
                            this.CODetailList = GetOrderDetail();
                        }
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        private void GetCOPaymentDetail(DataTable dt)
        {
            List<COPayment> ListPayment = null;
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow[] drCollection = dt.Select("CustomerOrderNo='" + this.CustomerOrderNo + "'");
                ListPayment = new List<COPayment>();

                for (int i = 0; i < drCollection.Length; i++)
                {
                    COPayment payment = new COPayment();
                    payment.GetCOPaymentObject(drCollection[i]);
                    ListPayment.Add(payment);
                }
                this.COPaymentList = ListPayment;
            }
        }

        
        public string CheckPUCAmount(int PCId, int BOId, decimal paymentAmount, decimal changeAmount, ref decimal availableAmt)
        {
            string dbMessage = string.Empty;
            List<CODetail> OrderDetailList = new List<CODetail>();
            System.Data.DataSet dSet = new DataSet();
            try
            {
                DataTaskManager dtManager = new DataTaskManager();
                DBParameterList dbParam;
                dbParam = new DBParameterList();

                dbParam.Add(new DBParameter("PCId", PCId, DbType.String));
                dbParam.Add(new DBParameter("BOId", BOId, DbType.String));
                dbParam.Add(new DBParameter("totalPayment", paymentAmount, DbType.String));
                dbParam.Add(new DBParameter("changeAmount", changeAmount, DbType.String));
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, dbMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                DataTable dtDeposit = dtManager.ExecuteDataTable(SP_PUC_CHECK_AMOUNT, dbParam);
                if (dtDeposit != null && dtDeposit.Rows.Count > 0)
                {
                    availableAmt = Convert.ToDecimal(dtDeposit.Rows[0]["AvailableAmt"]);
                }
                dbMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();

            }
            catch (Exception ex)
            {
                throw ex;
            }
             return dbMessage;
        }

        private List<CODetail> GetOrderDetail()
        {
            string errorMessage = string.Empty;
            List<CODetail> OrderDetailList = new List<CODetail>();
            System.Data.DataSet dSet = new DataSet();
            try
            {
                DBParameterList dbParam;
                dbParam = new DBParameterList();
                dbParam.Add(new DBParameter("@OrderNo", this.CustomerOrderNo, DbType.String));
                dbParam.Add(new DBParameter("@ItemCode", string.Empty, DbType.String));
                dbParam.Add(new DBParameter("@ItemID", -1, DbType.Int32));
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                // executing procedure to save the record 
                dSet = GetSelectedRecordsDataSet(dbParam, SP_CODETAIL_SEARCH, ref errorMessage);

                if (dSet != null && dSet.Tables.Count > 0)
                {
                    foreach (System.Data.DataRow drow in dSet.Tables[0].Rows)
                    {
                        CODetail order = new CODetail();
                        // Create Codetail Object
                        order.CreateCODetailObject(drow);
                        // Create Discount List
                        order.GetDiscountDetail(dSet.Tables[1]);
                        //Create Tax List
                        order.GetCODetailTax(dSet.Tables[2]);
                        OrderDetailList.Add(order);
                    }
                }
                return OrderDetailList;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion

        #region Private Methods

        private void CreateCOObject(DataRow dr)
        {
            this.BOId = Convert.ToInt32(dr["BOId"]);
            this.ChangeAmount = Convert.ToDecimal(dr["ChangeAmount"]);
            this.CreatedBy = Convert.ToInt32(dr["CreatedBy"]);
            this.CreatedByName = Convert.ToString(dr["CreatedByName"]);
            this.CreatedDate = dr["CreatedDate"].ToString();
            this.CustomerOrderNo = Convert.ToString(dr["CustomerOrderNo"]);
            this.DeliverFromAddress = Address.CreateAddressObject(dr);
            this.DeliverToAddress = new Address();
            this.DeliverToAddress.Address1 = Convert.ToString(dr["DeliverToAddressLine1"]);
            this.DeliverToAddress.Address2 = Convert.ToString(dr["DeliverToAddressLine2"]);
            this.DeliverToAddress.Address3 = Convert.ToString(dr["DeliverToAddressLine3"]);
            this.DeliverToAddress.Address4 = Convert.ToString(dr["DeliverToAddressLine4"]);
            this.DeliverToAddress.CityId = Convert.ToInt32(dr["DeliverToCityId"]);
            this.DeliverToAddress.City = Convert.ToString(dr["DeliverToCityName"]);
            this.DeliverToAddress.StateId = Convert.ToInt32(dr["DeliverToStateId"]);
            this.DeliverToAddress.State = Convert.ToString(dr["DeliverToStateName"]);
            this.DeliverToAddress.CountryId = Convert.ToInt32(dr["DeliverToCountryId"]);
            this.DeliverToAddress.Country = Convert.ToString(dr["DeliverToCountryName"]);
            this.DeliverToAddress.PhoneNumber1 = Convert.ToString(dr["DeliverToTelephone"]);
            this.DeliverToAddress.Mobile1 = Convert.ToString(dr["DeliverToMobile"]);
            this.DeliverToAddress.PinCode = Convert.ToString(dr["DeliverToPincode"]);
            if (dr.Table.Columns.Contains("DistributorName"))
                this.DeliverToAddress.DistributorName = Convert.ToString(dr["DistributorName"]);
            //this.DeliverToAddress.City = Convert.ToString(dr["DeliverToAddressLine4"]);
            //this.DeliverToAddress.Country = Convert.ToString(dr["DeliverToAddressLine4"]);
            //this.DeliverToAddress.State = Convert.ToString(dr["DeliverToAddressLine4"]);
            this.DiscountAmount = Convert.ToDecimal(dr["DiscountAmount"]);
            this.DistributorId = Convert.ToInt32(dr["DistributorId"]);
            this.DistributorName = Convert.ToString(dr["DistributorName"]);
            this.LogNo = Convert.ToString(dr["LogNo"]);
            this.ModifiedBy = Convert.ToInt32(dr["ModifiedBy"]);
            this.ModifiedDate = dr["ModifiedDate"].ToString();
            this.OrderAmount = Convert.ToDecimal(dr["OrderAmount"]);
            this.OrderDate = dr["Date"].ToString();
            this.OrderType = Convert.ToInt32(dr["OrderType"]);
            this.PaymentAmount = Convert.ToDecimal(dr["PaymentAmount"]);
            this.PCId = Convert.ToInt32(dr["PCId"]);
            this.PCCode = Convert.ToString(dr["PCCode"]);
            this.Status = Convert.ToInt32(dr["Status"]);
            this.TaxAmount = Convert.ToDecimal(dr["TaxAmount"]);
            this.TotalUnits = Convert.ToDecimal(dr["TotalUnits"]);
            this.TotalWeight = Convert.ToDecimal(dr["TotalWeight"]);
            this.UsedForRegistration = Convert.ToBoolean(dr["UsedForRegistration"]);
            this.DeliverFromAddress = new Address();
            this.DeliverFromAddress.Address1 = Convert.ToString(dr["DeliverFromAddress1"]);
            this.DeliverFromAddress.Address2 = Convert.ToString(dr["DelverFromAddress2"]);
            this.DeliverFromAddress.Address3 = Convert.ToString(dr["DeliverFromAddress3"]);
            this.DeliverFromAddress.Address4 = Convert.ToString(dr["DeliverFromAddress4"]);
            this.DeliverFromAddress.CityId = Convert.ToInt32(dr["DeliverFromCityId"]);
            this.DeliverFromAddress.City = Convert.ToString(dr["DeliverFromCityName"]);
            this.DeliverFromAddress.StateId = Convert.ToInt32(dr["DeliverFromStateId"]);
            this.DeliverFromAddress.State = Convert.ToString(dr["DeliverFromStateName"]);
            this.DeliverFromAddress.CountryId = Convert.ToInt32(dr["DeliverFromCountryId"]);
            this.DeliverFromAddress.Country = Convert.ToString(dr["DeliverFromCountryName"]);
            this.DeliverFromAddress.PhoneNumber1 = Convert.ToString(dr["DeliverFromTelephone"]);
            this.DeliverFromAddress.Mobile1 = Convert.ToString(dr["DeliverFromMobile"]);
            this.DeliverFromAddress.PinCode = Convert.ToString(dr["DeliverFromPincode"]);
            this.TerminalCode = Convert.ToString(dr["TerminalCode"]);
            this.OrderMode = Convert.ToInt32(dr["OrderMode"]);
            this.TotalBV = Convert.ToDecimal(dr["TotalBV"]);
            this.TotalPV = Convert.ToDecimal(dr["TotalPV"]);
            this.DistributorAddress = dr["DistributorAddress"].ToString();
            this.InvoiceNo = dr["InvoiceNo"].ToString();
            this.LogValue  = dr["LogValue"].ToString();
            this.IsPrintAllowed = dr["IsPrintAllowed"].ToString();
            this.InvoiceDate = dr["InvoiceDate"].ToString();
            this.ValidReportPrintDays = dr["ValidReportPrintDays"].ToString();
        }

        private decimal GetTotalPV()
        {
            decimal returnValue = 0;
            if (string.IsNullOrEmpty(this.CustomerOrderNo))
            {
                //Calculate Value if the order has not yet been saved to DB
                foreach (CODetail d in this.CODetailList)
                {
                    returnValue += d.PV * d.Qty;
                }
            }
            else
            {
                returnValue = m_totalPV;
            }
            return returnValue;
        }

        private decimal GetTotalBV()
        {
            decimal returnValue = 0;
            if (string.IsNullOrEmpty(this.CustomerOrderNo))
            {
                //Calculate Value if the order has not yet been saved to DB
                foreach (CODetail d in this.CODetailList)
                {
                    returnValue += d.BV * d.Qty;
                }
            }
            else
            {
                returnValue = m_totalBV;
            }
            return returnValue;
        }


        private decimal GetTotalWeight()
        {
            decimal returnValue = 0;
            if (string.IsNullOrEmpty(this.CustomerOrderNo))
            {
                //Calculate Value if the order has not yet been saved to DB
                foreach (CODetail d in this.CODetailList)
                {
                    returnValue += d.Qty;
                }
            }
            else
            {
                returnValue = m_totalWeight;
            }
            return returnValue;
        }

        private decimal GetTotalUnits()
        {
            decimal returnValue = 0;
            if (string.IsNullOrEmpty(this.CustomerOrderNo) || this.Status == (int)Common.OrderStatus.Created)
            {
                //Calculate Value if the order has not yet been saved to DB
                foreach (CODetail d in this.CODetailList)
                {
                    returnValue += d.Qty;
                }
            }
            else
            {
                returnValue = m_totalUnits;
            }
            return returnValue;
        }

        private decimal GetOrderAmount()
        {
            decimal returnValue = 0;
            if (string.IsNullOrEmpty(this.CustomerOrderNo) || this.Status == (int) Common.OrderStatus.Created || this.Status == (int) Common.OrderStatus.Modify)
            {
                //Calculate Value if the order has not yet been saved to DB
                foreach (CODetail d in this.CODetailList)
                {
                    returnValue += d.Amount;
                }
            }
            else
            {
                //If the order has been saved to DB then fetch the value from DB
                returnValue = m_orderAmount;
            }
            return returnValue;
        }

        private decimal GetTaxAmount()
        {
            decimal returnValue = 0;
            if (string.IsNullOrEmpty(this.CustomerOrderNo) || this.Status == (int)Common.OrderStatus.Modify)
            {
                //Calculate Value if the order has not yet been saved to DB
                foreach (CODetail d in this.CODetailList)
                {
                    foreach (CODetailTax dt in d.CODetailTaxList)
                    {
                        returnValue += dt.TaxAmount;
                    }
                }
            }
            else
            {
                //If the order has been saved to DB then fetch the value from DB
                returnValue = m_taxAmount;
            }
            return returnValue;
        }

        private decimal GetChangeAmount()
        {
            decimal returnValue = 0;
            if (this.Status == 1 || this.Status == 6)
            {
                decimal changeAmount = 0, bonusCheckPaymentAmount = 0;
                //Calculate Value if the order has not yet been saved to DB
                List<COPayment> cpList = this.COPaymentList.FindAll(delegate(COPayment c) { return c.TenderType == (int)Common.PaymentMode.BonusCheque; });
                if (cpList.Count == 0)
                {
                    changeAmount = this.PaymentAmount = this.TotalAmount;
                }
                else
                {
                    foreach (COPayment c in cpList)
                    {
                        bonusCheckPaymentAmount += c.PaymentAmount;
                    }
                }

                if (bonusCheckPaymentAmount >= this.TotalAmount)
                {
                    changeAmount = this.PaymentAmount - bonusCheckPaymentAmount;
                }
                else
                {
                    changeAmount = this.PaymentAmount - this.TotalAmount;
                }
                returnValue = changeAmount;
            }
            else
            {
                //If the order has been saved to DB then fetch the value from DB
                returnValue = m_changeAmount;
            }
            return returnValue;
        }

        private decimal GetDiscountAmount()
        {
            decimal returnValue = 0;
            if (string.IsNullOrEmpty(this.CustomerOrderNo) || this.Status == (int) Common.OrderStatus.Modify)
            {
                //Calculate Value if the order has not yet been saved to DB
                foreach (CODetail d in this.CODetailList)
                {
                    foreach (CODetailDiscount dd in d.CODiscountList)
                    {
                        returnValue += dd.DiscountAmount;
                    }
                }
            }
            else
            {
                //If the order has been saved to DB then fetch the value from DB
                returnValue = m_discountAmount;
            }
            return returnValue;
        }

        private decimal GetPaymentAmount()
        {
            decimal returnValue = 0;
            if (this.Status == (int)Common.OrderStatus.Created || this.Status == (int)Common.OrderStatus.Modify)
            {
                //Calculate Value if the order has not yet been saved to DB
                foreach (COPayment p in this.COPaymentList)
                {
                    //Bikram: AED changes
                    if (p.TenderType == 101)
                        continue;
                    returnValue += p.PaymentAmount;
                }
            }
            else
            {
                //If the order has been saved to DB then fetch the value from DB
                returnValue = m_paymentAmount;
            }
            return returnValue;
        }

        private DataTable GetSelectedRecords(string xmlDoc, string spName, ref string errorMessage)
        {
            DBParameterList dbParam;
            try
            {
                Vinculum.Framework.Data.DataTaskManager dtManager = new DataTaskManager();

                // initialize the parameter list object
                dbParam = new DBParameterList();

                // add the relevant 2 parameters
                dbParam.Add(new DBParameter(Common.PARAM_DATA, xmlDoc, DbType.String));
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                // executing procedure to save the record 
                DataTable dt = dtManager.ExecuteDataTable(spName, dbParam);

                // update database message
                errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();

                // if an error returned from the database
                if (errorMessage != string.Empty)
                    return null;
                else
                {
                    return dt;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private DataSet GetSelectedRecordsDataSet(string xmlDoc, string spName, ref string errorMessage)
        {
            DBParameterList dbParam;
            try
            {
                Vinculum.Framework.Data.DataTaskManager dtManager = new DataTaskManager();

                // initialize the parameter list object
                dbParam = new DBParameterList();

                // add the relevant 2 parameters
                dbParam.Add(new DBParameter(Common.PARAM_DATA, xmlDoc, DbType.String));
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                // executing procedure to save the record 
                DataSet ds = dtManager.ExecuteDataSet(spName, dbParam);

                // update database message
                errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();

                // if an error returned from the database
                if (errorMessage != string.Empty)
                    return null;
                else
                {
                    return ds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private DataSet GetSelectedRecordsDataSet(DBParameterList dbParam, string spName, ref string errorMessage)
        {

            try
            {
                Vinculum.Framework.Data.DataTaskManager dtManager = new DataTaskManager();

                // executing procedure to save the record 
                DataSet ds = dtManager.ExecuteDataSet(spName, dbParam);

                // update database message
                errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();

                // if an error returned from the database
                if (errorMessage != string.Empty)
                    return null;
                else
                {
                    return ds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }

}

