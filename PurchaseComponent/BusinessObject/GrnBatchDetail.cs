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
    public class GrnBatchDetail
    {
        #region SP Declaration
        private const string SP_GRNBATCHDETAIL_SEARCH = "usp_GRNBatchDetailSearch";
        #endregion
      
        #region DATABASE FIELD
        private const string CON_FIELD_GRNNO = "GRNNo";
        private const string CON_FIELD_SERIALNO = "SerialNo";
        private const string CON_FIELD_ITEMID = "ItemId";
        private const string CON_FIELD_ITEMCODE = "ItemCode";
        private const string CON_FIELD_ITEMDESCRIPTION = "ItemDescription";
         private const string CON_FIELD_BATCHNUMBER = "BatchNumber";
        private const string CON_FIELD_MANUFACTURINGDATE = "ManufacturingDate";
        private const string CON_FIELD_MANUFACTURERBATCHNO = "ManufacturerBatchNo";
        private const string CON_FIELD_EXPIRYDATE = "ExpiryDate";
        private const string CON_FIELD_MRP = "MRP";
        private const string CON_FIELD_RECEIVEDQTY = "ReceivedQty";
       
      	
        #endregion  
        
        #region Property
        public string GRNNo { get; set; }
        public int SerialNo { get; set; }
        public int ItemId { get; set; }
        public string BatchNumber { get; set; }
        public int RowNo { get; set; }
        public string ManufacturerBatchNumber { get; set; }
        public string ManufacturingDate { get; set; }
        public string DisplayManufacturingDate
        {
            get
            {
                return Convert.ToDateTime(ManufacturingDate).ToString(Common.DTP_DATE_FORMAT);
            }
            set
            {
                ManufacturingDate = value;
            }
        }

        public string ExpiryDate { get; set; }
        public string DisplayExpiryDate 
        {
            get
            {
               return Convert.ToDateTime(ExpiryDate).ToString(Common.DTP_DATE_FORMAT);
            }
            set
            {
                ExpiryDate = value;
            }
        }
       
        public double MRP { get; set; }
        public double DisplayMRP 
        {
            get { return Math.Round(DBMRP, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
        public double DBMRP 
        {
            get { return Math.Round(MRP, Common.DBAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
        
        public double ReceivedQty { get; set; }
        public double DisplayReceivedQty
        {
            get { return Math.Round(DBReceivedQty, Common.DisplayQtyRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
        public double DBReceivedQty
        {
            get { return Math.Round(ReceivedQty, Common.DBQtyRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }

        

       
        public int ReasonId { get; set; }

        public List<GrnBatchTaxDetail> TaxList { get; set; }
        #endregion

        #region Methods

        public List<GrnBatchDetail> Search()
        {
            try
            {
                List<GrnBatchDetail> GRNBatchDetailList = new List<GrnBatchDetail>();
                System.Data.DataTable dTable = new DataTable();
                string errorMessage = string.Empty;
                dTable = GetSelectedRecords(Common.ToXml(this), SP_GRNBATCHDETAIL_SEARCH, ref errorMessage);

                if (dTable != null)
                {
                    foreach (System.Data.DataRow drow in dTable.Rows)
                    {
                        GrnBatchDetail _Grn = new GrnBatchDetail();
                        _Grn = CreateBatchObject(drow);
                        GRNBatchDetailList.Add(_Grn);
                    }
                }
                return GRNBatchDetailList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GRNBatchDetailSearchDataTable()
        {
            try
            {                
                System.Data.DataTable dTable = new DataTable();
                string errorMessage = string.Empty;
                dTable = GetSelectedRecords(Common.ToXml(this), SP_GRNBATCHDETAIL_SEARCH, ref errorMessage);

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

        private GrnBatchDetail CreateBatchObject(DataRow dr)
        {
            try
            {
                GrnBatchDetail batch = new GrnBatchDetail();
                batch.BatchNumber = Convert.ToString(dr[CON_FIELD_BATCHNUMBER]);
                batch.ExpiryDate = Convert.ToString(dr[CON_FIELD_EXPIRYDATE]);
                batch.GRNNo = Convert.ToString(dr[CON_FIELD_GRNNO]);
                batch.ItemId = Convert.ToInt32(dr[CON_FIELD_ITEMID]);
                batch.ManufacturerBatchNumber = Convert.ToString(dr[CON_FIELD_MANUFACTURERBATCHNO]);
                batch.ManufacturingDate = Convert.ToString(dr[CON_FIELD_MANUFACTURINGDATE]);
                batch.MRP = Convert.ToDouble(dr[CON_FIELD_MRP]);
                batch.ReceivedQty = Convert.ToDouble(dr[CON_FIELD_RECEIVEDQTY]);
                batch.SerialNo = Convert.ToInt32(dr[CON_FIELD_SERIALNO]);
                batch.RowNo = Convert.ToInt32(dr["RowNo"]);
                return batch;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SetTaxDetail(PurchaseOrderDetail detail, string sVendorCode, string sLocationCode, int fromstateid, int tostateid)
        {
            try
            {
                TaxList = new List<GrnBatchTaxDetail>();
                detail.UnitQty = (decimal)ReceivedQty;
                detail.GetAndCalculateTaxes(true, sVendorCode, sLocationCode, fromstateid, tostateid);
                foreach (PurchaseOrderTaxDetail tax in detail.PurchaseOrderTaxDetail)
                {
                    GrnBatchTaxDetail grnTax = new GrnBatchTaxDetail();
                    grnTax.BatchNumber = BatchNumber;
                    grnTax.GRNNo = GRNNo;
                    grnTax.GroupOrder = tax.GroupOrder;
                    grnTax.ItemId = tax.ItemID;
                    grnTax.SerialNo = this.SerialNo;
                    grnTax.RowNo = this.RowNo;
                    grnTax.TaxAmount = tax.TaxAmount;
                    grnTax.TaxCode = tax.TaxCode;
                    grnTax.TaxGroup = tax.TaxGroup;
                    grnTax.TaxPercentage = tax.TaxPercentage;
                    grnTax.ExpiryDate = ExpiryDate;
                    grnTax.ManufacturerBatchNumber = ManufacturerBatchNumber;
                    grnTax.ManufacturingDate = ManufacturingDate;
                    grnTax.MRP = MRP;
                    TaxList.Add(grnTax);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SetTaxDetail(PurchaseOrderDetail detail,string sVendorCode,string sLocationCode)
        {
            try
            {
                TaxList = new List<GrnBatchTaxDetail>();
                detail.UnitQty = (decimal)ReceivedQty;
                detail.GetAndCalculateTaxes(true,sVendorCode,sLocationCode);
                foreach (PurchaseOrderTaxDetail tax in detail.PurchaseOrderTaxDetail)
                {
                    GrnBatchTaxDetail grnTax = new GrnBatchTaxDetail();
                    grnTax.BatchNumber = BatchNumber;
                    grnTax.GRNNo = GRNNo;
                    grnTax.GroupOrder = tax.GroupOrder;
                    grnTax.ItemId = tax.ItemID;
                    grnTax.SerialNo = this.SerialNo;
                    grnTax.RowNo = this.RowNo;
                    grnTax.TaxAmount = tax.TaxAmount;
                    grnTax.TaxCode = tax.TaxCode;
                    grnTax.TaxGroup = tax.TaxGroup;
                    grnTax.TaxPercentage = tax.TaxPercentage;
                    grnTax.ExpiryDate = ExpiryDate;
                    grnTax.ManufacturerBatchNumber = ManufacturerBatchNumber;
                    grnTax.ManufacturingDate = ManufacturingDate;
                    grnTax.MRP = MRP;
                    TaxList.Add(grnTax);
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
