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
    public class POSItem
    {
        private string iBarcode;
        private const string SP_ITEMS_SEARCH = "usp_POS_GetItems";
        private const string SP_BarcodeItems_Search = "Usp_GetBarcodeItemSearch";
        public int Id { get; set; }
        public string Code { get; set; }
        public int ParentId { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string PrintName { get; set; }
        public string ReceiptName { get; set; }
        public string DisplayName { get; set; }
        public int TaxCategoryId { get; set; }
        public string ItemName { get; set; }
        public bool IsItem { get; set; }
        public bool IsKit { get; set; }
        public bool IsComposite { get; set; }
        public List<POSItem> POSItems { get; set; }
        public int Level { get; set; }
        public decimal CurrentInventoryPosition { get; set; }
        public string BODisplayName
        {
            get
            {
                if (IsItem)
                    return Code + "\n" + "[ " + CurrentInventoryPosition.ToString("0") + " ]";
                else
                    return Code;
            }
        }

        public POSItem()
        {
        }

        public POSItem(int id, string code, int parentId, string shortName, string printName, string receiptName, string displayName,
                        int taxCategoryId, string itemName, bool isItem, bool isKit, bool isComposite, int level, decimal currentInventoryPosition)
        {
            Id = id;
            Code = code;
            ParentId = parentId;
            ShortName = shortName;
            PrintName = printName;
            ReceiptName = receiptName;
            DisplayName = displayName;
            TaxCategoryId = taxCategoryId;
            ItemName = itemName;
            IsItem = isItem;
            IsKit = isKit;
            IsComposite = isComposite;
            Level = level;
            CurrentInventoryPosition = currentInventoryPosition;
            POSItems = new List<POSItem>();
        }

        public static List<POSItem> Search(string locationCode, bool isKitOrder, int isFirstOrder, ref string dbMessage)
        {
            try
            {
                DBParameterList dbParamList = new DBParameterList();
                List<POSItem> itemList = new List<POSItem>();
                dbParamList.Add(new DBParameter("@inputParam", locationCode, DbType.String));
                dbParamList.Add(new DBParameter("@isKitOrder", isKitOrder, DbType.Boolean));
                dbParamList.Add(new DBParameter("@isFirstOrder", isFirstOrder, DbType.Int32));
                dbParamList.Add(new DBParameter("@forSkinCareItem", Common.ForSkinCareItem, DbType.Boolean));
                dbParamList.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));
                using (DataTaskManager dtManager = new DataTaskManager())
                {
                    DataSet ds = dtManager.ExecuteDataSet(SP_ITEMS_SEARCH, dbParamList);
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 2)
                        {

                            int min = Convert.ToInt32(ds.Tables[0].Rows[0]["Min"]);
                            int max = Convert.ToInt32(ds.Tables[0].Rows[0]["Max"]);

                            DataRow[] rows = ds.Tables[1].Select("Level = " + min);

                            for (int i = 0; i < rows.Length; i++)
                            {
                                POSItem item = CreateItem(rows[i]);
                                AddHierarchyChildren(item, ds, ds.Tables[1], max);
                                if (item.POSItems.Count > 0)
                                {
                                    itemList.Add(item);
                                }
                            }
                        }
                    }
                }
                return itemList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static POSItem Search(string itemBarcode, string locationCode, bool isKitOrder, int isFirstOrder, ref string dbMessage)
        {
            try
            {
                POSItem item=null;
                DBParameterList dbParamList = new DBParameterList();
                List<POSItem> itemList = new List<POSItem>();
                dbParamList.Add(new DBParameter("@itemBarcode", itemBarcode, DbType.String)); 
                dbParamList.Add(new DBParameter("@inputParam", locationCode, DbType.String));
                dbParamList.Add(new DBParameter("@isKitOrder", isKitOrder, DbType.Boolean));
                dbParamList.Add(new DBParameter("@isFirstOrder", isFirstOrder, DbType.Int32));
                dbParamList.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));
                using (DataTaskManager dtManager = new DataTaskManager())
                {
                    DataSet ds = dtManager.ExecuteDataSet(SP_BarcodeItems_Search, dbParamList);
                    if (ds != null)
                    {
                        DataRow[] rows = ds.Tables[0].Select();
                        item = CreateItem(rows[0]);
                    }
                }
                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static void AddHierarchyChildren(POSItem item, DataSet ds, DataTable dt, int max)
        {
            DataRow[] rows = dt.Select("ParentId=" + item.Id);
            if (rows.Length == 0)
            {
                if (item.Level == max)
                {
                    //ADD ITEMS HERE
                    AddItemChildrem(item, ds.Tables[2]);
                }
            }
            else
            {
                for (int i = 0; i < rows.Length; i++)
                {
                    //ADD HIERARCHIES - CHILDREN
                    POSItem item1 = CreateItem(rows[i]);
                    AddHierarchyChildren(item1, ds, ds.Tables[1], max);
                    if (item1.POSItems.Count > 0)
                    {
                        item.POSItems.Add(item1);
                    }
                }
            }
        }

        private static void AddItemChildrem(POSItem item, DataTable dataTable)
        {
            DataRow[] rows = dataTable.Select("ParentId = " + item.Id);
            for (int i = 0; i < rows.Length; i++)
            {
                POSItem item1 = CreateItem(rows[i]);
                item.POSItems.Add(item1);
            }
        }

        private static POSItem CreateItem(DataRow row)
        {
            return new POSItem(Convert.ToInt32(row["Id"]), row["Code"].ToString(), Convert.ToInt32(row["ParentId"]),
                                                         row["ShortName"].ToString(), row["PrintName"].ToString(), row["ReceiptName"].ToString(),
                                                        row["DisplayName"].ToString(), Convert.ToInt32(row["TaxCategoryId"]), row["Name"].ToString(),
                                                        Convert.ToBoolean(row["IsItem"]), Convert.ToBoolean(row["IsKit"]), Convert.ToBoolean(row["IsComposite"]), Convert.ToInt32(row["Level"]), Convert.ToDecimal(row["CurrentInventoryPosition"]));
        }

    }
}
