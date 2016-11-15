using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CoreComponent.Core.BusinessObjects;


namespace ReturnsComponent.BusinessObjects
{
    public class CustomerReturn : Return
    {
        public const string MODULE_CODE = "RET02";

        #region SP Declaration
        private const string SP_CUSTOMER_RETURN_SEARCH = "usp_CustomerReturnSearch";
        private const string SP_CUSTOMER_RETURN_ITEM_SEARCH = "usp_CustomerReturnItemSearch";
        private const string SP_CUSTOMER_RETURN_SAVE = "usp_CustomerReturnSave";
        #endregion

        public decimal TotalAmount
        {
            get;
            set;
        }
        public decimal DisplayTotalAmount
        {
            get { return Math.Round(DBTotalAmount, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
        public decimal DBTotalAmount
        {
            get { return Math.Round(TotalAmount, Common.DBAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }

        public decimal DeductionAmount
        { get; set; }
        public decimal DisplayDeductionAmount
        {
            get { return Math.Round(DBDeductionAmount, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
        public decimal DBDeductionAmount
        {
            get { return Math.Round(DeductionAmount, Common.DBAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }

        public decimal PayableAmount
        { get; set; }
        public decimal DisplayPayableAmount
        {
            get { return Math.Round(DBPayableAmount, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
        public decimal DBPayableAmount
        {
            get { return Math.Round(PayableAmount, Common.DBAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }

        public string DistributorPCId
        { get; set; }

        public string ReturnDate
        { get; set; }

        public int CustomerType
        { get; set; }

        public string CustomerTypeValue
        { get; set; }
        
        public string Remarks
        {get;set;}

        public string FromDate
        {get;set;}

        public string ToDate
        {get;set;}

        public List<CustomerReturnItem> ReturnItem
        { get; set; }
        public string DisplayApprovedDate
        { get; set; }

        public string ApprovedDate
        { get; set; }

        public string ApprovedName
        {get;set;}

        public int ApprovedBy
        {get;set;}

        public string StatusName
        {get;set;}

        public int StatusId
        {get;set;}

        public int LocationId
        {get;set;}

        public string LocationName
        { get; set; }

        public int SourceLocationId
        { get; set; }

        //Added by Kaushik 
        public string DistributorId
        { get; set; }

        public string InvoiceDate
        { get; set; }

        public decimal TaxAmount
        { get; set; }

        public decimal InvoiceAmount
        { get; set; }

        public string InvoiceNo
        { get; set; }

        
        /// <summary>
        /// Save CUSTOMER RETURN
        /// </summary>
        /// <param name="xmlString"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public bool Save(string xmlString, int nmode, ref string errorMessage)
        {
            bool isSuccess = false;
            // call the save method which returns whether the save was successfull or not
            isSuccess = base.Save(xmlString, nmode, SP_CUSTOMER_RETURN_SAVE, ref errorMessage);
            return isSuccess;
        }

        /// <summary>
        /// Return a list of Customer Return List
        /// </summary>
        /// <returns></returns>
        public List<CustomerReturn> Search()
        {
            List<CustomerReturn> invList = new List<CustomerReturn>();
            try
            {
                string errorMessage = string.Empty;
                System.Data.DataTable dTable = base.GetSelectedRecords(Common.ToXml(this), SP_CUSTOMER_RETURN_SEARCH, ref errorMessage);

                if (dTable == null | dTable.Rows.Count == 0)
                    return null;

                foreach (System.Data.DataRow drow in dTable.Rows)
                {
                    CustomerReturn objInv = new CustomerReturn();
                    objInv.ModifiedDate = drow["ModifiedDate"].ToString();
                    objInv.LocationId = Convert.ToInt32(drow["LocationId"]);
                    objInv.LocationName = drow["LocationName"].ToString();
                    objInv.CustomerType = Convert.ToInt32(drow["CustomerType"]);
                    objInv.TotalAmount = Math.Round(Convert.ToDecimal(drow["TotalAmount"]),2);
                    objInv.DeductionAmount = Math.Round(Convert.ToDecimal(drow["DeductionAmount"]),2);
                    objInv.PayableAmount = Math.Round(Convert.ToDecimal(drow["AmountPayable"]), 2);

                    objInv.CustomerTypeValue = drow["CustomerTypeValue"].ToString();
                    objInv.DistributorPCId = drow["DistributorPCId"].ToString();
                    objInv.ReturnNo = drow["ReturnNo"].ToString();
                    objInv.ApprovedBy = Convert.ToInt32(drow["ApprovedBy"]);
                    objInv.ApprovedDate = drow["ApprovedDate"].ToString();
                    objInv.DisplayApprovedDate = drow["ApprovedDate"].ToString().Length == 0 ? string.Empty : Convert.ToDateTime(drow["ApprovedDate"]).ToString(Common.DTP_DATE_FORMAT);
                    
                    objInv.ApprovedName = drow["ApprovedName"].ToString();

                    objInv.StatusId = Convert.ToInt32(drow["StatusId"]);
                    objInv.StatusName = drow["StatusName"].ToString();
                    invList.Add(objInv);
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
            }
            return invList;
        }

        /// <summary>
        /// To Search Item
        /// </summary>
        /// <param name="toNumber"></param>
        /// <param name="sourceAddressId"></param>
        /// <returns></returns>
        public List<CustomerReturnItem> SearchItem(string returnNo, int crmode)
        {
            List<CustomerReturnItem> itemList = new List<CustomerReturnItem>();
            try
            {
                System.Data.DataTable dTable = SearchItemDataTable(returnNo, crmode);

                if (dTable == null && dTable.Rows.Count == 0)
                    return null;

                itemList = CreateCusomerReturnItemObject(dTable, crmode);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
            }
            return itemList;
        }

        public List<CustomerReturnItem> CreateCusomerReturnItemObject(System.Data.DataTable dTable, int customerType)
        {
            List<CustomerReturnItem> itemList = new List<CustomerReturnItem>();
            foreach (System.Data.DataRow drow in dTable.Rows)
            {
                CustomerReturnItem objItemInventory = new CustomerReturnItem();
                objItemInventory.ItemCode = drow["ItemCode"].ToString();
                objItemInventory.ManufactureBatchNo = drow["ManufactureBatchNo"].ToString();
                objItemInventory.BatchNo = drow["BatchNo"].ToString();
                objItemInventory.ItemName = drow["ItemName"].ToString();
                objItemInventory.BucketId = Convert.ToInt32(drow["BucketId"]);
                objItemInventory.DistributorPrice = Math.Round(Convert.ToDecimal(drow["DistributorPrice"]), 2);

                objItemInventory.Quantity = Math.Round(Convert.ToDecimal(drow["Quantity"]), 2);
                objItemInventory.MRP = Math.Round(Convert.ToDecimal(drow["MRP"]), 2);
                objItemInventory.ItemId = Convert.ToInt32(drow["ItemId"]);
                objItemInventory.RowNo = Convert.ToInt32(drow["RowNo"]);

                
                if (customerType.Equals(3))
                //if (Convert.ToInt32(drow["CustomerType"]).Equals(3))
                {
                    objItemInventory.DistributorId = drow["DistributorId"].ToString();
                    objItemInventory.InvoiceDate = Convert.ToDateTime(drow["InvoiceDate"]).ToString(Common.DTP_DATE_FORMAT);
                    objItemInventory.InvoiceAmount = Math.Round(Convert.ToDecimal(drow["InvoiceAmount"]), 2);
                    objItemInventory.TaxAmount = Math.Round(Convert.ToDecimal(drow["TaxAmount"]), 2);
                    objItemInventory.TotalAmount = Math.Round(Convert.ToDecimal(drow["TotalAmount"]),2);
                    objItemInventory.InvoiceNo=drow["InvoiceNo"].ToString();
                }
                itemList.Add(objItemInventory);
            }
            return itemList;
        }

        public DataTable SearchItemDataTable(string returnNo, int crmode)
        {
            System.Data.DataSet dTable = new DataSet();
            try
            {
                string errorMessage = string.Empty;
                dTable = base.GetSelectedItems(returnNo, crmode, SP_CUSTOMER_RETURN_ITEM_SEARCH, ref errorMessage);

                if (dTable == null && dTable.Tables.Count > 0 && dTable.Tables[0].Rows.Count == 0)
                    return null;
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
            }
            return dTable.Tables[0];
        }
    }
}
