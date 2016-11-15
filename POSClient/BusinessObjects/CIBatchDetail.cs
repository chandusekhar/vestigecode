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
    public class CIBatchDetail
    {
        #region SP_Declaration
        private const string SP_DEFAULT_BATCH = "usp_DefaultInvoiceBatch";
        private const string SP_BATCH_DETAIL = "usp_GetBatchDetail";
        #endregion

        #region Property

        public string InvoiceNo { get; set; }
        public Int32 RecordNo { get; set; }
        public Int32 ItemId { get; set; }
        public Int32 BucketId { get; set; }
        public string BatchNo { get; set; }
        public string ItemCode { get; set; }
        
        public decimal TotalQuantity {get; set; }
        public decimal DisplayTotalQuantity
        {
            get { return Math.Round(DBTotalQuantity, Common.DisplayQtyRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property can not be explicitly set"); }
        }
        public decimal DBTotalQuantity
        {
            get { return Math.Round(TotalQuantity, Common.DBQtyRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property can not be explicitly set"); }
        }
        
        public decimal Quantity { get; set; }
        public decimal DisplayQuantity 
        {
            get { return Math.Round(DBQuantity, Common.DisplayQtyRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property can not be explicitly set"); }
        }
        public decimal DBQuantity
        {
            get { return Math.Round(Quantity, Common.DBQtyRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property can not be explicitly set"); }
        }

        public string ManufacturerBatchNo { get; set; }
        public decimal MRP { get; set; }
        public decimal DisplayMRP
        {
            get { return Math.Round(DBMRP, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property can not be explicitly set"); }
        }
        public decimal DBMRP
        {
            get { return Math.Round(MRP, Common.DBAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property can not be explicitly set"); }
        }
     
        public string ManufacturingDate { get; set; }
        public string DisplayManufacturingDate 
        {
            get { return Convert.ToDateTime(ManufacturingDate).ToString(Common.DTP_DATE_FORMAT) == "01-01-1900" ? string.Empty : Convert.ToDateTime(ManufacturingDate).ToString(Common.DTP_DATE_FORMAT); }
            set { throw new NotImplementedException("This Property can not be explicitly set"); }
        }
  
        public string ExpiryDate { get; set; }
        public string DisplayExpiryDate 
        {
            get { return Convert.ToDateTime(ExpiryDate).ToString(Common.DTP_DATE_FORMAT) == "01-01-1900" ? string.Empty : Convert.ToDateTime(ExpiryDate).ToString(Common.DTP_DATE_FORMAT); }
            set { throw new NotImplementedException("This Property can not be explicitly set"); }
        }
     
        public decimal AvailableQty { get; set; }
        public decimal DisplayAvailableQty
        {
            get { return Math.Round(DBAvailableQty, Common.DisplayQtyRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property can not be explicitly set"); }
        }
        public decimal DBAvailableQty
        {
            get { return Math.Round(AvailableQty, Common.DBQtyRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property can not be explicitly set"); }
        }

        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }

        public List<CIBatchTax> CIBatchTaxList { get; set; }
        #endregion

        public static List<CIBatchDetail> GetDefaultBatch(string OrderNo,int LocationID, ref string errorMessage)
        {
            List<CIBatchDetail> BatchList = new List<CIBatchDetail>();
            System.Data.DataTable dTable = new DataTable();
            try
            {
                string dbMessage = string.Empty;
                DBParameterList dbParam = new DBParameterList();
                dbParam.Add(new DBParameter("@OrderNo", OrderNo, DbType.String));
                dbParam.Add(new DBParameter("@LocationId", LocationID, DbType.Int32));
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, dbMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                using (DataTaskManager dt = new DataTaskManager())
                {
                    dTable = dt.ExecuteDataTable(SP_DEFAULT_BATCH, dbParam);
                    if (dbMessage == string.Empty)
                    {
                        if (dTable != null && dTable.Rows.Count > 0)
                        {
                            foreach (System.Data.DataRow drow in dTable.Rows)
                            {
                                CIBatchDetail batch = new CIBatchDetail();
                                batch.CreateBatchObject(drow);
                                BatchList.Add(batch);
                            }
                        }
                    }
                }
                return BatchList;
            }

            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void CreateBatchObject(DataRow dr)
        {
            CIBatchDetail batch = new CIBatchDetail();

            if (dr.Table.Columns.Contains("AvailableQty"))
                this.AvailableQty = Convert.ToDecimal(dr["AvailableQty"]);

            if (dr.Table.Columns.Contains("BatchNo"))
                this.BatchNo = Convert.ToString(dr["BatchNo"]);

            if (dr.Table.Columns.Contains("BucketId"))
                this.BucketId = Convert.ToInt32(dr["BucketId"]);

            if (dr.Table.Columns.Contains("ExpDate"))
                this.ExpiryDate = Convert.ToDateTime(dr["ExpDate"]).ToShortDateString();

            if (dr.Table.Columns.Contains("InvoiceNo"))
                this.InvoiceNo = Convert.ToString(dr["InvoiceNo"]);

            if (dr.Table.Columns.Contains("ItemCode"))
                this.ItemCode = Convert.ToString(dr["ItemCode"]);

            if (dr.Table.Columns.Contains("ItemId"))
                this.ItemId = Convert.ToInt32(dr["ItemId"]);

            if (dr.Table.Columns.Contains("ManufacturingBatchNo"))
                this.ManufacturerBatchNo = Convert.ToString(dr["ManufacturingBatchNo"]);

            if (dr.Table.Columns.Contains("MfgDate"))
                this.ManufacturingDate = Convert.ToDateTime(dr["MfgDate"]).ToShortDateString();

            if (dr.Table.Columns.Contains("MRP"))
                this.MRP = Convert.ToDecimal(dr["MRP"]);

            if (dr.Table.Columns.Contains("Quantity"))
                this.Quantity = Convert.ToDecimal(dr["Quantity"]);

            if (dr.Table.Columns.Contains("RecordNo"))
                this.RecordNo = Convert.ToInt32(dr["RecordNo"]);

            if (dr.Table.Columns.Contains("TotalQty"))
                this.TotalQuantity = Convert.ToDecimal(dr["TotalQty"]);
            
        }

        public List<CIBatchDetail> GetBatchDetail(string BatchNo,int ItemID, int LocationID, ref string errorMessage)
        {
            List<CIBatchDetail> BatchList = new List<CIBatchDetail>();
            System.Data.DataTable dTable = new DataTable();
            try
            {
                string dbMessage = string.Empty;
                DBParameterList dbParam = new DBParameterList();
                dbParam.Add(new DBParameter("@BatchNo", BatchNo, DbType.String));
                dbParam.Add(new DBParameter("@ItemID", ItemID, DbType.String));
                dbParam.Add(new DBParameter("@LocationId", LocationID, DbType.Int32));
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, dbMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                using (DataTaskManager dt = new DataTaskManager())
                {
                    dTable = dt.ExecuteDataTable(SP_BATCH_DETAIL, dbParam);
                    if (dbMessage == string.Empty)
                    {
                        if (dTable != null && dTable.Rows.Count > 0)
                        {
                            foreach (System.Data.DataRow drow in dTable.Rows)
                            {
                                CIBatchDetail batch = new CIBatchDetail();
                                batch.CreateBatchObject(drow);
                                BatchList.Add(batch);
                            }
                        }
                    }
                }
                return BatchList;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
