using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vinculum.Framework.Data;
using Vinculum.Framework.DataTypes;
using CoreComponent.BusinessObjects;
using CoreComponent.Core.BusinessObjects;

namespace CoreComponent.BusinessObjects
{
    [Serializable]
    public class ItemGroup
    {
        #region Variable Declaration

        public string GroupName
        {
            get;
            set;
        }
        public int GroupItemId
        {
            get;
            set;
        }
        public int ItemId
        {
            get;
            set;
        }
        public string ItemName
        {
            get;
            set;
        }
        public decimal Quantity
        {
            get;
            set;
        }
        public decimal DisplayQuantity
        {
            get { return Math.Round(DbQuantity, Common.DisplayQtyRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
        public decimal DbQuantity
        {
            get { return Math.Round(Quantity, Common.DBQtyRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
        public int Status
        {
            get;
            set;
        }
        public string StatusName
        {
            get;
            set;
        }
        public int MHSubCategoryId
        {
            get;
            set;
        }
        public string MHSubCategoryName
        {
            get;
            set;
        }       
        public int ModifiedBy
        {
            get;
            set;
        }
        public List<ItemGroup> ItemList
        {
            get;
            set;
        }

        #endregion

        #region SP Declaration

        private const String SP_ITEMGROUP_SEARCH = "usp_ItemGroupSearch";
        private const String SP_ITEMGROUP_SAVE = "usp_ItemGroupSave";

        #endregion

        #region Methods

        public List<ItemGroup> SearchItemGroup(int mode, int groupItemId, string groupName, int status, int mhSubCategory, ref string errorMessage)
        {
            List<ItemGroup> lstItemGroup = new List<ItemGroup>();            
            DBParameterList dbParam;
            ItemGroup objItemGroup = null;            
            try
            {
                using (DataTaskManager dtManager = new DataTaskManager())
                {
                    dbParam = new DBParameterList();
                    dbParam.Add(new DBParameter("SearchMode", mode, DbType.Int32));
                    dbParam.Add(new DBParameter("GroupItemId", groupItemId, DbType.Int32));
                    dbParam.Add(new DBParameter("GroupName", groupName, DbType.String));
                    dbParam.Add(new DBParameter("Status", status, DbType.Int32));
                    dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, errorMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                    DataTable dt = dtManager.ExecuteDataTable(SP_ITEMGROUP_SEARCH, dbParam);
                    errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();
                    if (errorMessage.Trim().Length == 0)
                    {
                        if (mode == 1)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                objItemGroup = new ItemGroup();
                                objItemGroup.GroupItemId = Convert.ToInt32(dt.Rows[i]["GroupItemId"]);
                                objItemGroup.GroupName = dt.Rows[i]["GroupName"].ToString();   
                                objItemGroup.Status = Convert.ToInt32(dt.Rows[i]["Status"]);
                                objItemGroup.StatusName = dt.Rows[i]["StatusName"].ToString();
                                lstItemGroup.Add(objItemGroup);
                            }                            
                        }
                        if (mode == 2)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                objItemGroup = new ItemGroup();                                                                
                                objItemGroup.ItemId = Convert.ToInt32(dt.Rows[i]["ItemId"]);
                                objItemGroup.ItemName = dt.Rows[i]["ItemName"].ToString();
                                objItemGroup.MHSubCategoryId = Convert.ToInt32(dt.Rows[i]["MerchHierarchyDetailId"]);
                                objItemGroup.MHSubCategoryName = dt.Rows[i]["Name"].ToString();
                                lstItemGroup.Add(objItemGroup);
                            }
                        }
                    }  
                }return lstItemGroup;
                
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                throw ex;

            }
        }

        public Boolean SaveItemGroup(ref String errorMessage)
        {
            DBParameterList dbParam;
            bool isSuccess = false;
            using (DataTaskManager dtManager = new DataTaskManager())
            {
                dtManager.BeginTransaction();
                {
                    try
                    {
                        string xmlDoc = Common.ToXml(this);
                        dbParam = new DBParameterList();
                        dbParam.Add(new DBParameter(Common.PARAM_DATA, xmlDoc, DbType.String));
                        dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, errorMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                        DataTable dt = dtManager.ExecuteDataTable(SP_ITEMGROUP_SAVE, dbParam);

                        errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();
                        {
                            if (errorMessage.Length > 0)
                            {
                                isSuccess = false;
                                dtManager.RollbackTransaction();
                            }
                            else
                            {
                                dtManager.CommitTransaction();
                                isSuccess = true;
                            }
                        }
                        return isSuccess;
                    }
                    catch (Exception ex)
                    {
                        dtManager.RollbackTransaction();
                        throw ex;
                    }
                }
            }
        }

      
        
        #endregion

    }
    [Serializable]
    public class ItemWithSubCategory
    {
        public int ItemId
        {
            get;
            set;
        }
        public string ItemName
        {
            get;
            set;
        }
        public int MHSubCategoryId
        {
            get;
            set;
        }
        public string MHSubCategoryName
        {
            get;
            set;
        }


        public List<ItemWithSubCategory> SearchItems()
        {
            ItemWithSubCategory objItemWithSubcategory = null;
            List<ItemWithSubCategory> lst = new List<ItemWithSubCategory>();
            DataTable dtItembySubCategory = Common.ParameterLookup(Common.ParameterType.ItemBySubCategory, new ParameterFilter("", -1, 0, 0));
            if (dtItembySubCategory.Rows.Count > 0)
            {
                for (int i = 0; i < dtItembySubCategory.Rows.Count; i++)
                {
                    objItemWithSubcategory = new ItemWithSubCategory();
                    objItemWithSubcategory.ItemId = Convert.ToInt32(dtItembySubCategory.Rows[i]["ItemId"]);
                    objItemWithSubcategory.ItemName = dtItembySubCategory.Rows[i]["ItemName"].ToString();
                    objItemWithSubcategory.MHSubCategoryId = Convert.ToInt32(dtItembySubCategory.Rows[i]["MerchHierarchyDetailId"]);
                    objItemWithSubcategory.MHSubCategoryName = dtItembySubCategory.Rows[i]["Name"].ToString();
                    lst.Add(objItemWithSubcategory);
                }
            }
            return lst;
        }
    }
}
