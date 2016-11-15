using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreComponent.BusinessObjects;
using CoreComponent.Core.BusinessObjects;

namespace CoreComponent.Hierarchies.BusinessObjects
{
    [Serializable]
    public class MerchandiseHierarchy: Hierarchy
    {
        public const string MODULE_CODE = "MDM03";
        #region SP Declaration
        private const string SP_MERCHANDISE_SAVE = "usp_MerchandiseSave";
        private const string SP_MERCHANDISE_SEARCH = "usp_MerchandiseSearch";
        private const string SP_ORG_CONFIG_SEARCH = "usp_MerchandiseConfigSearch";
        #endregion

        private System.Int32 m_merchConfigId;
        private System.String m_merchConfigCode;
        private System.String m_merchConfigName;
        private System.String m_description;
        private System.Int32 m_status = -9;
        private String m_statusName = string.Empty;
        private bool m_isTradable; 
        private System.Int32 m_createdBy;
        private System.Int32 m_modifiedBy;

        public System.Int32 MerchConfigId
        {
            get { return m_merchConfigId; }
            set { m_merchConfigId = value; }
        }
        public System.String ConfigCode
        {
            get { return m_merchConfigCode; }
            set { m_merchConfigCode = value; }
        }
        public System.String ConfigName
        {
            get { return m_merchConfigName; }
            set { m_merchConfigName = value; }
        }
        public System.String Description
        {
            get { return m_description; }
            set { m_description = value; }
        }
        public System.Int32 Status
        {
            get { return m_status; }
            set { m_status = value; }
        }
        public String StatusName
        {
            get { return m_statusName; }
            set { m_statusName = value; }
        }
        public bool IsTradable
        {
            get { return m_isTradable; }
            set { m_isTradable = value; }
        }
        public System.Int32 CreatedBy
        {
            get { return m_createdBy; }
            set { m_createdBy = value; }
        }
        public System.Int32 ModifiedBy
        {
            get { return m_modifiedBy; }
            set { m_modifiedBy = value; }
        }

        /// <summary>
        /// This function is used to save the location data
        /// </summary>
        /// <returns></returns>
        public bool Save(ref string errorMessage)
        {
            bool isSuccess = false;
            isSuccess = base.Save(Common.ToXml(this), SP_MERCHANDISE_SAVE, ref errorMessage);

            return isSuccess;
        }

        public List<MerchandiseHierarchy> Search()
        {
            string errorMessage = string.Empty;
            System.Data.DataTable dTable = base.GetSelectedRecords(Common.ToXml(this), SP_MERCHANDISE_SEARCH, ref errorMessage);

            if (dTable == null | dTable.Rows.Count <= 0)
                return null;

            List<MerchandiseHierarchy> merchandiseList = new List<MerchandiseHierarchy>();
            foreach (System.Data.DataRow drow in dTable.Rows)
            {
                MerchandiseHierarchy merch = new MerchandiseHierarchy();
                merch.HierarchyCode = drow["Code"].ToString();
                merch.HierarchyName = drow["Name"].ToString();
                merch.Description = drow["Description"].ToString();
                merch.ConfigCode = drow["TypeCode"].ToString();
                merch.ParentHierarchyName = drow["ParentName"].ToString();
                merch.HierarchyLevel = Convert.ToInt32(drow["Level"]);
                merch.Status = Convert.ToInt32(drow["Status"]);
                merch.StatusName = drow["StatusName"].ToString();
                merch.ParentHierarchyId = Convert.ToInt32(drow["ParentHierarchyId"]);
                merch.HierarchyId = Convert.ToInt32(drow["HierarchyId"]);
                merch.HierarchyType = Convert.ToInt32(drow["HierarchyType"]);
                merch.ModifiedDate = drow["ModifiedDate"].ToString();
                merch.IsTradable = Convert.ToBoolean(drow["IsTradable"].ToString());
                merchandiseList.Add(merch);
            }
            return merchandiseList;
        }

        public List<MerchandiseHierarchy> ConfigSearch()
        {
            List<MerchandiseHierarchy> orgList = new List<MerchandiseHierarchy>();
            try
            {
                string errorMessage = string.Empty;
                System.Data.DataTable dTable = base.GetConfigRecords(SP_ORG_CONFIG_SEARCH, ref errorMessage);

                if (dTable == null | dTable.Rows.Count <= 0)
                    return null;

                foreach (System.Data.DataRow drow in dTable.Rows)
                {
                    MerchandiseHierarchy org = new MerchandiseHierarchy();
                    org.HierarchyLevel = Convert.ToInt32(drow["Level"].ToString());
                    org.HierarchyName = drow["Name"].ToString();
                    orgList.Add(org);
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
            }
            return orgList;
        }
    }
}
