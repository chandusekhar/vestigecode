using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreComponent.Core.BusinessObjects;

namespace InventoryComponent.BusinessObjects
{
    [Serializable]
    public class StockCount:Inventory
    {
        public const string MODULE_CODE = "INV02";
        public const string SP_STOCK_SEARCH = "usp_StockCountSearch";
        public const string SP_STOCK_COUNT_ITEM_SEARCH = "usp_StockCountItemSearch";
        public const string SP_STOCK_COUNT_ITEM_BATCH_SEARCH = "usp_StockCountItemBatchSearch";
        public const string SP_STOCK_COUNT_SAVE = "usp_StockCountSave";
        
        public List<ItemStockCount> StockCountItem
        {
            get;
            set;
        }

        public string Remarks
        {
            get;
            set;
        }

        public int InititatedBy
        {
            get;
            set;
        }
        public List<ItemStockBatch> ItemBatch
        {
            get;
            set;
        }

        public string InitiatedDate
        {
            get;
            set;
        }
        public string DisplayInitiatedDate
        {
            get
            {
                if (InitiatedDate != null && InitiatedDate.ToString().Length > 0)
                    return Convert.ToDateTime(InitiatedDate).ToString(Common.DTP_DATE_FORMAT);
                else
                    return string.Empty;
            }
            set { InitiatedDate = value; }
        }
        public string ExecutedDate
        {
            get;
            set;
        }
        public string DisplayExecutedDate
        {
            get
            {
                if (ExecutedDate != null && ExecutedDate.ToString().Length > 0)
                    return Convert.ToDateTime(ExecutedDate).ToString(Common.DTP_DATE_FORMAT);
                else
                    return string.Empty;
            }
            set { ExecutedDate = value; }
        }
        public string ExecutedName
        {
            get;
            set;
        }

        public string InitiatedName
        {
            get;
            set;
        }

        /// <summary>
        /// Save Stock Count
        /// </summary>
        /// <param name="xmlString"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public bool Save(string xmlString, ref string errorMessage)
        {
            bool isSuccess = false;
            // call the save method which returns whether the save was successfull or not
            isSuccess = base.Save(xmlString, SP_STOCK_COUNT_SAVE, ref errorMessage);
            return isSuccess;
        }


        /// <summary>
        /// Search Item
        /// </summary>
        /// <param name="toNumber"></param>
        /// <param name="sourceAddressId"></param>
        /// <returns></returns>
        public List<ItemStockCount> SearchItem(string seqNo)
        {
            List<ItemStockCount> tiItemList = new List<ItemStockCount>();
            try
            {
                string errorMessage = string.Empty;
                System.Data.DataSet dTable = base.GetSelectedItems(seqNo,  SP_STOCK_COUNT_ITEM_SEARCH, ref errorMessage);

                if (dTable == null && dTable.Tables.Count > 0 && dTable.Tables[0].Rows.Count == 0)
                    return null;

                tiItemList = CreateItemObject(dTable.Tables[0]);

            }
            catch (Exception ex)
            {
                CoreComponent.Core.BusinessObjects.Common.LogException(ex);
            }
            return tiItemList;
        }

        public System.Data.DataTable SearchItemDataTable(string seqNo)
        {
            System.Data.DataSet dSet = new System.Data.DataSet();
            try
            {
                string errorMessage = string.Empty;
                dSet = base.GetSelectedItems(seqNo, SP_STOCK_COUNT_ITEM_SEARCH, ref errorMessage);

                if (dSet == null && dSet.Tables.Count > 0 && dSet.Tables[0].Rows.Count == 0)
                    return null;
            }
            catch (Exception ex)
            {
                CoreComponent.Core.BusinessObjects.Common.LogException(ex);
            }
            return dSet.Tables[0];
        }

        /// <summary>
        /// To Search Item
        /// </summary>
        /// <param name="toNumber"></param>
        /// <param name="sourceAddressId"></param>
        /// <returns></returns>
        public List<ItemStockBatch> SearchItemBatch(string seqNo)
        {
            List<ItemStockBatch> tiItemBatchList = new List<ItemStockBatch>();
            try
            {
                string errorMessage = string.Empty;
                System.Data.DataSet dTable = base.GetSelectedItems(seqNo, SP_STOCK_COUNT_ITEM_BATCH_SEARCH, ref errorMessage);

                if (dTable == null && dTable.Tables.Count > 0 && dTable.Tables[0].Rows.Count == 0)
                    return null;

                tiItemBatchList = CreateItemBatchObject(dTable.Tables[0]);
            }
            catch (Exception ex)
            {
                CoreComponent.Core.BusinessObjects.Common.LogException(ex);
            }
            return tiItemBatchList;
        }
        /// <summary>
        /// Returns DataTable of ItemBatch Detail
        /// </summary>
        /// <param name="seqNo"></param>
        /// <returns></returns>
        public System.Data.DataTable SearchItemBatchDataTable(string seqNo)
        {
            System.Data.DataSet dSet = new System.Data.DataSet();
            try
            {
                string errorMessage = string.Empty;
                dSet = base.GetSelectedItems(seqNo, SP_STOCK_COUNT_ITEM_BATCH_SEARCH, ref errorMessage);

                if (dSet == null && dSet.Tables.Count > 0 && dSet.Tables[0].Rows.Count == 0)
                    return null;                
            }
            catch (Exception ex)
            {
                CoreComponent.Core.BusinessObjects.Common.LogException(ex);
            }
            return dSet.Tables[0];
        }

        List<ItemStockBatch> CreateItemBatchObject(System.Data.DataTable dTable)
        {
            List<ItemStockBatch> tiItemList = new List<ItemStockBatch>();
            foreach (System.Data.DataRow drow in dTable.Rows)
            {
                ItemStockBatch objTI = new ItemStockBatch();

                objTI.PhysicalQty = Math.Round(Convert.ToDecimal(drow["PhysicalQuantity"]), 2);
                objTI.ItemRowNo = Convert.ToInt32(drow["RowNo"]);
                objTI.BatchNo = drow["BatchNo"].ToString();
                objTI.ManufactureBatchNo = drow["ManufactureBatchNo"].ToString();
                
                tiItemList.Add(objTI);
            }
            return tiItemList;
        }
        List<ItemStockCount> CreateItemObject(System.Data.DataTable dTable)
        {
            List<ItemStockCount> tiItemList = new List<ItemStockCount>();
            foreach (System.Data.DataRow drow in dTable.Rows)
            {
                ItemStockCount objTI = new ItemStockCount();
                objTI.ItemCode = drow["ItemCode"].ToString();
                objTI.ItemName = drow["ItemName"].ToString();

                objTI.SystemQty = Math.Round(Convert.ToDecimal(drow["SystemQuantity"]), 2);
                objTI.BucketId = Convert.ToInt32(drow["BucketId"]);
                objTI.BucketName = drow["BucketName"].ToString();

                objTI.ItemId = Convert.ToInt32(drow["ItemId"]);
                objTI.RowNo = Convert.ToInt32(drow["RowNo"]);
                objTI.TotalPhysicalQuantity = Math.Round(Convert.ToDecimal(drow["TotalPhysicalQuantity"]),2);

                tiItemList.Add(objTI);
            }
            return tiItemList;
        }

        /// <summary>
        /// Return a list of Inventory Adjustment List
        /// </summary>
        /// <returns></returns>
        public List<StockCount> Search()
        {
            List<StockCount> stockList = new List<StockCount>();
            try
            {
                string errorMessage = string.Empty;
                System.Data.DataTable dTable = base.GetSelectedRecords(Common.ToXml(this), SP_STOCK_SEARCH, ref errorMessage);

                if (dTable == null | dTable.Rows.Count == 0)
                    return null;

                foreach (System.Data.DataRow drow in dTable.Rows)
                {
                    StockCount objInv = new StockCount();
                    objInv.ModifiedDate = drow["ModifiedDate"].ToString();
                    objInv.LocationId = Convert.ToInt32(drow["LocationId"]);
                    objInv.LocationName = drow["LocationName"].ToString();
                    objInv.LocationAddress = drow["LocationAddress"].ToString();

                    objInv.SeqNo = drow["SeqNo"].ToString();
                    objInv.InitiatedDate = drow["InitiatedDate"].ToString();
                    objInv.ExecutedDate = drow["ExecutedDate"].ToString();
                    objInv.InitiatedName = drow["InitiatedName"].ToString();

                    objInv.ExecutedName = drow["ExecutedName"].ToString();
                    objInv.StatusId = Convert.ToInt32(drow["StatusId"]);
                    objInv.StatusName = drow["StatusName"].ToString();
                    objInv.Remarks = drow["Remarks"].ToString();
                    stockList.Add(objInv);
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
            }
            return stockList;
        }
    }
}
