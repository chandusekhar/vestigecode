using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CoreComponent.Core.BusinessObjects;
using Vinculum.Framework.Data;
using Vinculum.Framework.DataTypes;

namespace InventoryComponent.BusinessObjects
{
    [Serializable]
    public class InventoryAdjust : Inventory
    {
        public const string MODULE_CODE = "INV03";

        #region SP Declaration
        private const string SP_INVENTORY_ADJUST_SEARCH = "usp_InventoryAdjustSearch";
        private const string SP_INVENTORY_ITEM_SEARCH = "usp_InventoryItemSearch";
        private const string SP_INVENTORY_SAVE = "usp_InventorySave";
        private const string SP_INVENTORYADJITEMQTY = "usp_InventoryAdjItemQty";
        #endregion

        public List<ItemInventory> InventoryItem
        {
            get;
            set;
        }
        public string InitiatedName
        {
            get;
            set;
        }
        public int LocationType
        {
            get;
            set;
        }


        public int InitiatedBy
        {
            get;
            set;
        }
        public string InitiatedDate
        {
            get;
            set;
        }

        /// <summary>
        /// Save Inventory Adjustment
        /// </summary>
        /// <param name="xmlString"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public bool Save(string xmlString, ref string errorMessage)
        {
            bool isSuccess = false;
            // call the save method which returns whether the save was successfull or not
            isSuccess = base.Save(xmlString, SP_INVENTORY_SAVE, ref errorMessage);
            return isSuccess;
        }

        /// <summary>
        /// To Search Item
        /// </summary>
        /// <param name="toNumber"></param>
        /// <param name="sourceAddressId"></param>
        /// <returns></returns>
        public List<ItemInventory> SearchItem(string adjustmentNo)
        {
            List<ItemInventory> itemList = new List<ItemInventory>();
            try
            {
                string errorMessage = string.Empty;
                System.Data.DataSet dTable = base.GetSelectedItems(adjustmentNo, SP_INVENTORY_ITEM_SEARCH, ref errorMessage);

                if (dTable == null && dTable.Tables.Count > 0 && dTable.Tables[0].Rows.Count == 0)
                    return null;

                itemList = CreateTOTIObject(dTable.Tables[0]);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
            }
            return itemList;
        }

        public DataTable SearchItemDataTable(string adjustmentNo)
        {
            System.Data.DataSet dTable = new DataSet();
            try
            {
                string errorMessage = string.Empty;
                dTable = base.GetSelectedItems(adjustmentNo, SP_INVENTORY_ITEM_SEARCH, ref errorMessage);

                if (dTable == null && dTable.Tables.Count > 0 && dTable.Tables[0].Rows.Count == 0)
                    return null;
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
            }
            return dTable.Tables[0];
        }

        List<ItemInventory> CreateTOTIObject(System.Data.DataTable dTable)
        {
            byte columnSearched = 0;
            foreach (DataColumn column in dTable.Columns)
            {
                Console.WriteLine(column.ColumnName);
                if (column.ColumnName.ToLower() == "IsInternalBatchAdj".ToLower())
                    columnSearched = 1;
            }


            List<ItemInventory> itemList = new List<ItemInventory>();
            foreach (System.Data.DataRow drow in dTable.Rows)
            {
                ItemInventory objItemInventory = new ItemInventory();
                objItemInventory.ItemCode = drow["ItemCode"].ToString();
                objItemInventory.ManufactureBatchNo = drow["ManufactureBatchNo"].ToString();
                objItemInventory.BatchNo = drow["BatchNo"].ToString();
                objItemInventory.ItemName = drow["ItemName"].ToString();
                objItemInventory.ReasonCode = drow["ReasonCode"].ToString().Trim();
                objItemInventory.ReasonDescription = drow["ReasonDescription"].ToString();
                objItemInventory.ReasonCodeDescription = drow["ReasonCodeDescription"].ToString();
                objItemInventory.UOMId = Convert.ToInt32(drow["UOMId"]);
                objItemInventory.UOMName = drow["UOMName"].ToString();

                objItemInventory.Quantity = Convert.ToDecimal(drow["Quantity"]);
                objItemInventory.ApprovedQty = Math.Round(Convert.ToDecimal(drow["ApprovedQty"].ToString().Length == 0 ? "0" : drow["ApprovedQty"]), 2);
                objItemInventory.Weight = Math.Round(Convert.ToDecimal(drow["Weight"]), 2);

                objItemInventory.FromBucketId = Convert.ToInt32(drow["FromBucketId"]);
                objItemInventory.ToBucketId = Convert.ToInt32(drow["ToBucketId"]);

                objItemInventory.FromBucketName = drow["FromBucketName"].ToString();
                objItemInventory.ToBucketName = drow["ToBucketName"].ToString();
                objItemInventory.ItemId = Convert.ToInt32(drow["ItemId"]);
                objItemInventory.RowNo = Convert.ToInt32(drow["RowNo"]);

                if (columnSearched == 1)
                {
                    objItemInventory.ToManufactureBatchNo = (drow["ToManufactureBatchNo"].ToString() != string.Empty ? drow["ToManufactureBatchNo"].ToString() : null);
                    objItemInventory.ToBatchNo = drow["ToBatchNo"].ToString();
                    objItemInventory.ToItemCode = drow["ToItemCode"].ToString();
                    objItemInventory.ToItemName = drow["ToItemName"].ToString();
                    objItemInventory.ToItemId = Convert.ToInt32(drow["ToItemId"]);

                    objItemInventory.Export = (Convert.ToInt32(objItemInventory.ReasonCode) == 10 ? 1 : 0);
                    if (drow["IsInternalBatchAdj"].ToString() == string.Empty)
                    {
                    }
                    else
                    objItemInventory.InterBatchAdj = Convert.ToBoolean(drow["IsInternalBatchAdj"]) == true ? 1 : 0;
                }
                else
                {
                    objItemInventory.ToManufactureBatchNo = string.Empty;
                    objItemInventory.ToBatchNo = string.Empty;
                    objItemInventory.ToItemCode = string.Empty;
                    objItemInventory.ToItemName = string.Empty;
                    objItemInventory.ToItemId = Common.INT_DBNULL;
                    
                    objItemInventory.Export = 0;
                    objItemInventory.InterBatchAdj = 0;
                }

                itemList.Add(objItemInventory);
            }
            return itemList;
        }


        /// <summary>
        /// Return a list of Inventory Adjustment List
        /// </summary>
        /// <returns></returns>
        public List<InventoryAdjust> Search()
        {
            List<InventoryAdjust> invList = new List<InventoryAdjust>();
            try
            {
                string errorMessage = string.Empty;
                System.Data.DataTable dTable = base.GetSelectedRecords(Common.ToXml(this), SP_INVENTORY_ADJUST_SEARCH, ref errorMessage);

                
                if (dTable == null | dTable.Rows.Count == 0)
                    return null;


                byte columnSearched = 0;
                foreach (DataColumn column in dTable.Columns)
                {
                    Console.WriteLine(column.ColumnName);
                    if ((column.ColumnName.ToLower() == "Exportstatus".ToLower()) || (column.ColumnName.ToLower() == "Interadjstatus".ToLower()))
                        columnSearched = 1;
                }

                foreach (System.Data.DataRow drow in dTable.Rows)
                {
                    InventoryAdjust objInv = new InventoryAdjust();
                    objInv.ModifiedDate = drow["ModifiedDate"].ToString();
                    objInv.LocationId = Convert.ToInt32(drow["LocationId"]);
                    objInv.LocationName = drow["LocationName"].ToString();
                    objInv.LocationAddress = drow["LocationAddress"].ToString();

                    objInv.SeqNo = drow["AdjustmentNo"].ToString();
                    objInv.InitiatedDate = drow["InitiatedDate"].ToString();
                    objInv.ApprovedDate = drow["ApprovedDate"].ToString();
                    objInv.InitiatedName = drow["InitiatedName"].ToString();

                    objInv.ApprovedName = drow["ApprovedName"].ToString();
                    objInv.StatusId = Convert.ToInt32(drow["StatusId"]);
                    objInv.StatusName = drow["StatusName"].ToString();

                    if (columnSearched == 1)
                    {
                        objInv.Isexport = Convert.ToInt32(drow["Exportstatus"].ToString());
                        objInv.InterbatchAdj = Convert.ToInt32(drow["Interadjstatus"]);
                    }
                    else
                    {
                        objInv.Isexport = 0;
                        objInv.InterbatchAdj = 0;
                    }
                    invList.Add(objInv);
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
            }
            return invList;
        }


        public decimal GetInventoryAdjItemQty(int locationId, string itemCode, int bucketId, string batchNo, int status, ref string errorMsg)
        {
            DBParameterList dbParam;
            try
            {
                Vinculum.Framework.Data.DataTaskManager dtManager = new DataTaskManager();

                // initialize the parameter list object
                dbParam = new DBParameterList();

                // add the relevant 2 parameters
                dbParam.Add(new DBParameter("locationId", locationId, DbType.Int32));
                dbParam.Add(new DBParameter("itemCode", itemCode, DbType.String));
                dbParam.Add(new DBParameter("bucketId", bucketId, DbType.Int32));
                dbParam.Add(new DBParameter("batchNo", batchNo, DbType.String));
                dbParam.Add(new DBParameter("status", status, DbType.Int32));
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                // executing procedure to save the record 
                DataTable dt = dtManager.ExecuteDataTable(SP_INVENTORYADJITEMQTY, dbParam);

                // update database message
                errorMsg = dbParam[Common.PARAM_OUTPUT].Value.ToString();

                // if an error returned from the database
                if (errorMsg != string.Empty)
                    return 0;
                else
                {
                    if (dt != null && dt.Rows.Count > 0)
                        return Convert.ToDecimal(dt.Rows[0]["Quantity"]);
                    else
                        return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
