using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreComponent.BusinessObjects;
using CoreComponent.Core.BusinessObjects;
using Vinculum.Framework.Data;
using Vinculum.Framework.DataTypes;
using System.Data;
namespace PurchaseComponent.BusinessObjects
{
    [Serializable]
    public class GrnDetail
    {
        #region SP Declaration
        private const string SP_GRNDETAIL_SEARCH = "usp_GRNDetailSearch";
        private const string SP_GETGRNITEMS = "usp_GetItemsForGRN";
        #endregion  

        #region DATABASE FIELD
        private const string CON_FIELD_GRNNO    = "GRNNo";
        private const string CON_FIELD_PONUMBER = "PONumber";
        private const string CON_FIELD_AMENDMENTNO = "AmendmentNo";
        private const string CON_FIELD_SERIALNO = "SerialNo";
        private const string CON_FIELD_ITEMID = "ItemId";
        private const string CON_FIELD_ITEMCODE = "ItemCode";
        private const string CON_FIELD_PURCHASEUOM= "PurchaseUOM";
        private const string CON_FIELD_ITEMDESCRIPTION= "ItemDescription";
        private const string CON_FIELD_ALREADYRECEIVEDQTY= "AlreadyReceivedQty";
        private const string CON_FIELD_BALANCEQTY= "BalanceQty";
        private const string CON_FIELD_POQTY = "POQty";
        private const string CON_FIELD_CHALLANQTY= "ChallanQty";
        private const string CON_FIELD_INVOICEQTY= "InvoiceQty";
        private const string CON_FIELD_RECEIVEDQTY= "ReceivedQty";
        private const string CON_FIELD_MAXQTY = "MaxQty";      
        private const string CON_FIELD_WEIGHT = "ItemWeight";
    
        
        #endregion  

        #region Property
        private string m_grnNo = string.Empty;
        public string GRNNo
        {
            get { return m_grnNo; }
            set { m_grnNo = value; }
        }

        private string m_poNumber = string.Empty;
        public string PONumber
        {
            get { return m_poNumber; }
            set { m_poNumber = value; }
        }

        private int m_amendmentNo = 0;
        public int AmendmentNo
        {
            get { return m_amendmentNo; }
            set { m_amendmentNo = value; }
        }

        private int m_serialNo=0;
        public int SerialNo
        {
            get { return m_serialNo; }
            set { m_serialNo = value; }
        }

        private int m_itemId = 0;
        public int ItemId
        {
            get { return m_itemId; }
            set { m_itemId = value; }
        }

        private string m_itemCode = string.Empty;
        public string ItemCode
        {
            get { return m_itemCode; }
            set { m_itemCode = value; }
        }

        private string m_purchaseUOM = string.Empty;
        public string PurchaseUOM
        {
            get { return m_purchaseUOM; }
            set { m_purchaseUOM = value; }
        }
        
        public double ReceivedQty
        {
            get { return GetReceivedQty(); }
            set { throw new NotImplementedException(); }
        }

        public double DBReceivedQty
        {
            get { return Math.Round(ReceivedQty, Common.DBQtyRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }

        public double DisplayReceivedQty
        {
            get { return Math.Round(DBReceivedQty, Common.DisplayQtyRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }

            

        private string m_itemDescription = string.Empty;
        public string ItemDescription
        {
            get { return m_itemDescription; }
            set { m_itemDescription = value; }
        }

        private double m_poQty = 0;
        public double POQty
        {
            get { return m_poQty; }
            set { m_poQty = value; }
        }
        public double DisplayPOQty
        {
            get { return Math.Round(DBPOQty, Common.DisplayQtyRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
        public double DBPOQty
        {
            get { return Math.Round(POQty, Common.DBQtyRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }

        private double m_alreadyReceivedQty = 0;
        public double AlreadyReceivedQty
        {
            get { return m_alreadyReceivedQty; }
            set { m_alreadyReceivedQty = value; }
        }
        public double DisplayAlreadyReceivedQty
        {
            get { return Math.Round(DBAlreadyReceivedQty, Common.DisplayQtyRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
        public double DBAlreadyReceivedQty
        {
            get { return Math.Round(AlreadyReceivedQty, Common.DBQtyRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }

        public double BalanceQty
        {
            get {
                if (POQty - (AlreadyReceivedQty + ReceivedQty) < 0)
                    return 0;
                else
                    return POQty-(AlreadyReceivedQty+ReceivedQty); 
                }
            set { throw new NotImplementedException(); }
        }

        public double DisplayBalanceQty
        {
            get { return Math.Round(DBBalanceQty, Common.DisplayQtyRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
        public double DBBalanceQty
        {
            get { return Math.Round(BalanceQty, Common.DBQtyRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }

        private double m_maxQty = 0;
        public double MaxQty
        {
            get { return m_maxQty; }
            set { m_maxQty = value; }
        }

        public double DisplayMaxQty
        {
            get { return Math.Round(DBMaxQty, Common.DisplayQtyRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
        public double DBMaxQty
        {
            get { return Math.Round(MaxQty, Common.DBQtyRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }

        private double m_challanQty = 0;
        public double ChallanQty
        {
            get { return m_challanQty; }
            set { m_challanQty = value; }
        }
        public double DisplayChallanQty
        {
            get { return Math.Round(DBChallanQty, Common.DisplayQtyRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
        public double DBChallanQty
        {
            get { return Math.Round(ChallanQty, Common.DBQtyRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }

        private double m_invoiceQty = 0;
        public double InvoiceQty
        {
            get { return Math.Round(m_invoiceQty,2); }
            set { m_invoiceQty = value; }
        }
        public double DisplayInvoiceQty
        {
            get { return Math.Round(DBInvoiceQty, Common.DisplayQtyRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
        public double DBInvoiceQty
        {
            get { return Math.Round(InvoiceQty, Common.DBQtyRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }

        //private string m_batchNo = string.Empty;
        //public string BatchNo
        //{
        //    get { return m_batchNo; }
        //    set { m_batchNo = value; }
        //}

        //private string m_manufacturingDate = string.Empty;
        //public string ManufacturingDate
        //{
        //    get { return m_manufacturingDate; }
        //    set { m_manufacturingDate = value; }
        //}

        //private string m_expiryDate = string.Empty;
        //public string ExpiryDate
        //{
        //    get { return m_expiryDate; }
        //    set { m_expiryDate = value; }
        //}

        private int m_weight = 0;

        public int Weight
        {
            get { return m_weight; }
            set { m_weight = value; }
        }
     
        private List<GrnBatchDetail> m_grnBatchDetailList = null;

        public List<GrnBatchDetail> GRNBatchDetailList
        {
            get { return m_grnBatchDetailList; }
            set { m_grnBatchDetailList = value; }
        }

        #endregion

        #region Methods
        public List<GrnDetail> Search()
        {           
            try
            {
                List<GrnDetail> GRNDetailList = new List<GrnDetail>();
                System.Data.DataTable dTable = new DataTable();
                string errorMessage = string.Empty;
                dTable = GetSelectedRecords(Common.ToXml(this), SP_GRNDETAIL_SEARCH, ref errorMessage);

                if (dTable != null)
                {
                    foreach (System.Data.DataRow drow in dTable.Rows)
                    {
                        GrnDetail _Grn = CreateGRNObject(drow);
                        _Grn.GRNBatchDetailList = _Grn.GetBatchDetail();
                        GRNDetailList.Add(_Grn);
                    }
                }
                return GRNDetailList;
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        public DataTable GRNDetailSearchDataTable()
        {
            try
            {                
                DataTable dTable = new DataTable();
                string errorMessage = string.Empty;
                dTable = GetSelectedRecords(Common.ToXml(this), SP_GRNDETAIL_SEARCH, ref errorMessage);
                if (errorMessage != String.Empty)
                {
                    dTable = null;
                }
                return dTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GrnDetail> GetGRNItem(ref string errorMessage)
        {           
            try
            {
                List<GrnDetail> GRNDetailList = new List<GrnDetail>();
                System.Data.DataTable dTable = new DataTable();               
                
                dTable = GetSelectedRecords(Common.ToXml(this), SP_GETGRNITEMS, ref errorMessage);                
                if (dTable != null)
                {
                    foreach (System.Data.DataRow drow in dTable.Rows)
                    {
                        GrnDetail _Grn = CreateGRNObject(drow);                   
                        GRNDetailList.Add(_Grn);
                    }
                }
                return GRNDetailList;
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }        
        
        private List<GrnBatchDetail> GetBatchDetail()
        {
            try
            {
                GrnBatchDetail batch = new GrnBatchDetail();
                batch.GRNNo = GRNNo;
                batch.ItemId = ItemId;
                List<GrnBatchDetail> ListBatch = batch.Search();
                return ListBatch;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public virtual DataTable GetSelectedRecords(string xmlDoc, string spName, ref string errorMessage)
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
        
        private GrnDetail CreateGRNObject(DataRow dr)
        {
            try
            {
                GrnDetail detail = new GrnDetail();
                detail.AlreadyReceivedQty = Convert.ToDouble(dr[CON_FIELD_ALREADYRECEIVEDQTY]);
                detail.AmendmentNo = Convert.ToInt32(dr[CON_FIELD_AMENDMENTNO]);
                detail.ChallanQty = Convert.ToDouble(dr[CON_FIELD_CHALLANQTY]);
                detail.GRNNo = Convert.ToString(dr[CON_FIELD_GRNNO]);
                detail.InvoiceQty = Convert.ToDouble(dr[CON_FIELD_INVOICEQTY]);
                detail.ItemCode = Convert.ToString(dr[CON_FIELD_ITEMCODE]);
                detail.ItemDescription = Convert.ToString(dr[CON_FIELD_ITEMDESCRIPTION]);
                detail.ItemId = Convert.ToInt32(dr[CON_FIELD_ITEMID]);
                detail.PONumber = Convert.ToString(dr[CON_FIELD_PONUMBER]);
                detail.POQty = Convert.ToDouble(dr[CON_FIELD_POQTY]);               
                detail.MaxQty =Convert.ToDouble(dr[CON_FIELD_MAXQTY]);
                detail.PurchaseUOM = Convert.ToString(dr[CON_FIELD_PURCHASEUOM]);
                detail.SerialNo = Convert.ToInt32(dr[CON_FIELD_SERIALNO]);
                detail.Weight = Convert.ToInt32(dr[CON_FIELD_WEIGHT]);
                return detail;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        
        private double GetReceivedQty()
        {
            try
            {
                double qty = 0;
                if (GRNBatchDetailList != null)
                {
                    foreach (GrnBatchDetail _batch in GRNBatchDetailList)
                    {
                        qty = qty + _batch.ReceivedQty;
                    }
                }
                return qty;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
      
        #endregion
    }
}
