using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreComponent.Core.BusinessObjects;
using CoreComponent.BusinessObjects;
using Vinculum.Framework.Data;
using Vinculum.Framework.DataTypes;

namespace CoreComponent.Hierarchies.BusinessObjects
{
    [Serializable]
    public class OrganizationalHierarchy : Hierarchy
    {
        public const string MODULE_CODE = "MDM01";
        #region SP Declaration
        private const string SP_ORG_SAVE = "usp_OrganizationSave";
        private const string SP_ORG_SEARCH = "usp_OrganizationSearch";
        private const string SP_ORG_CONFIG_SEARCH = "usp_OrganizationConfigSearch";
        private const string SP_ORG_ZONE_STATE_SEARCH = "usp_OrgZoneStateSearch";
        
        #endregion

        private System.String m_description, m_orgConfigId, m_hierarchyTypeCode;
        private Int32 m_status, m_createdBy, m_modifiedBy;
        private System.DateTime m_createdDate;
        private List<State> m_lstState;

        public List<State> LstState
        {
            get { return m_lstState; }
            set { m_lstState = value; }
        }

        public System.String HierarchyTypeCode
        {
            get { return m_hierarchyTypeCode; }
            set { m_hierarchyTypeCode = value; }
        }

        public System.String OrgConfigId
        {
            get { return m_orgConfigId; }
            set { m_orgConfigId = value; }
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

        public System.Int32 CreatedBy
        {
            get { return m_createdBy; }
            set { m_createdBy = value; }
        }

        public System.DateTime CreatedDate
        {
            get { return m_createdDate; }
            set { m_createdDate = value; }
        }

        public System.Int32 ModifiedBy
        {
            get { return m_modifiedBy; }
            set { m_modifiedBy = value; }
        }

        public List<OrganizationalHierarchy> Search()
        {
            List<OrganizationalHierarchy> orgList = new List<OrganizationalHierarchy>();
            try
            {
                string errorMessage = string.Empty;
                System.Data.DataTable dTable = base.GetSelectedRecords(Common.ToXml(this), SP_ORG_SEARCH, ref errorMessage);

                if (dTable == null | dTable.Rows.Count <= 0)
                    return null;


                foreach (System.Data.DataRow drow in dTable.Rows)
                {
                    OrganizationalHierarchy org = new OrganizationalHierarchy();
                    org.HierarchyCode = drow["Code"].ToString();
                    org.HierarchyName = drow["Name"].ToString();
                    org.Description = drow["Description"].ToString();
                    org.HierarchyTypeCode = drow["TypeCode"].ToString();
                    org.ParentHierarchyName = drow["ParentName"].ToString();
                    org.HierarchyLevel = Convert.ToInt32(drow["Level"]);
                    org.Status = Convert.ToInt32(drow["Status"]);
                    org.ParentHierarchyId = Convert.ToInt32(drow["ParentHierarchyId"]);
                    org.HierarchyId = Convert.ToInt32(drow["HierarchyId"]);
                    org.HierarchyType = Convert.ToInt32(drow["HierarchyType"]);
                    org.ModifiedDate = drow["ModifiedDate"].ToString();
                    org.StatusValue = drow["StatusValue"].ToString();
                    orgList.Add(org);
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
            }
            return orgList;
        }

        public bool Save()
        {
            // variable for checking whether the save was successfulor not
            bool isSuccess = false;
            return isSuccess;
        }

        public List<State> ZoneStateSearch(int hierarchyId)
        {
            List<State> cntList = new List<State>();
            try
            {
                string errorMessage = string.Empty;
                System.Data.DataTable dTable = base.GetContactRecords(SP_ORG_ZONE_STATE_SEARCH, hierarchyId, ref errorMessage);

                if (dTable == null | dTable.Rows.Count <= 0)
                    return null;

                foreach (System.Data.DataRow drow in dTable.Rows)
                {
                    State cnt = new State();
                    cnt.StateName = drow["StateName"].ToString();
                    cnt.StateId = Convert.ToInt32(drow["StateId"]);

                    cnt.StatusName = drow["StatusName"].ToString();
                    cnt.Status = Convert.ToInt32(drow["Status"]);

                    cnt.CountryName = drow["CountryName"].ToString();
                    cnt.CountryId = Convert.ToInt32(drow["CountryId"]);

                    cntList.Add(cnt);
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
            }
            return cntList;
        }
        public bool Save(ref string errorMessage)
        {
            bool isSuccess = false;
            // call the save method which returns whether the save was successfull or not
            isSuccess = base.Save(Common.ToXml(this), SP_ORG_SAVE, ref errorMessage);

            return isSuccess;
        }

        public List<OrganizationalHierarchy> ConfigSearch()
        {
            List<OrganizationalHierarchy> orgList = new List<OrganizationalHierarchy>();
            try
            {
                string errorMessage = string.Empty;
                System.Data.DataTable dTable = base.GetConfigRecords(SP_ORG_CONFIG_SEARCH, ref errorMessage);

                if (dTable == null | dTable.Rows.Count <= 0)
                    return null;

                foreach (System.Data.DataRow drow in dTable.Rows)
                {
                    OrganizationalHierarchy org = new OrganizationalHierarchy();
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
