using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CoreComponent.Core.BusinessObjects;
using CoreComponent.BusinessObjects;
using Vinculum.Framework.Data;
using Vinculum.Framework.DataTypes;

namespace CoreComponent.Hierarchies.BusinessObjects
{
    [Serializable]
    public class LocationHierarchy : Hierarchy
    {
        public const string MODULE_CODE = "MDM02";
        #region SP Declaration
        private const string SP_LOC_SAVE = "usp_LocationSave";
        private const string SP_LOC_SEARCH = "usp_LocationSearch";
        private const string SP_LOC_CONFIG_SEARCH = "usp_LocationConfigSearch";
        private const string SP_LOC_CONTACT_SEARCH = "usp_LocationContactSearch";
        #endregion

        private System.Int32 m_hierarchyConfigId;
        private System.Int32 m_locationType;
        private System.Int32 m_garde;
        private System.Int32 m_orgLevel;
        private List<Contact> m_contact;
        private List<Contact> m_remooveContact;
        private List<string> m_modifiedContactId;
        private System.String m_shortName;
        private System.String m_description;
        private System.String m_replenishmentLocationId;
        private System.String m_tinNo;
        private System.String m_vatNo;
        private System.String m_cstNo;
        //private System.String m_taxJurisdiction;
        private System.String m_distributorId;
        private System.String m_orgConfigId;
        private System.String m_orgHierarchyId;
        private System.String m_orgName;
        private System.Decimal m_startingAmount;
        private System.Int32 m_grade;
        private System.Int32 m_status;
        private System.Int32 m_createdBy;
        private System.String m_IECCode;
        private int? m_IsMiniBranch;
        
        private System.Int32 m_RegAddLocationId;

        


        #region Property
        public List<LocationTerminal> LocationTerminal
        {
            get ; 
            set ;
        }

        public List<string> ModifiedContactId
        {
            get { return m_modifiedContactId; }
            set { m_modifiedContactId = value; }
        }

        public List<Contact> RemovedContacts
        {
            get { return m_remooveContact; }
            set { m_remooveContact = value; }

        }
        public List<Contact> Contact
        {
            get { return m_contact; }
            set { m_contact = value; }
        }

        public System.Int32 OrgLevel
        {
            get { return m_orgLevel; }
            set { m_orgLevel = value; }
        }
        public System.Int32 Garde
        {
            get { return m_garde; }
            set { m_garde = value; }
        }

        public System.Int32 HierarchyConfigId
        {
            get { return m_hierarchyConfigId; }
            set { m_hierarchyConfigId = value; }
        }

        public System.Int32 LocationType
        {
            get { return m_locationType; }
            set { m_locationType = value; }
        }


        public System.String OrgName
        {
            get { return m_orgName; }
            set { m_orgName = value; }
        }

        public System.String Description
        {
            get { return m_description; }
            set { m_description = value; }
        }

        public System.String CstNo
        {
            get { return m_cstNo; }
            set { m_cstNo = value; }
        }
        public System.String IECCode
        {
            get { return m_IECCode; }
            set { m_IECCode = value; }
        }
        public Nullable<int> IsMiniBranch
        {
            get { return m_IsMiniBranch; }
            set { m_IsMiniBranch = value.HasValue ? value.Value : -1; }
        }
        public System.String VatNo
        {
            get { return m_vatNo; }
            set { m_vatNo = value; }
        }

        public System.String ShortName
        {
            get { return m_shortName; }
            set { m_shortName = value; }
        }

        public System.String ReplenishmentLocationId
        {
            get { return m_replenishmentLocationId; }
            set { m_replenishmentLocationId = value; }
        }

        public System.String TinNo
        {
            get { return m_tinNo; }
            set { m_tinNo = value; }
        }

        //public System.String TaxJurisdiction
        //{
        //    get { return m_taxJurisdiction; }
        //    set { m_taxJurisdiction = value; }
        //}

        public System.String DistributorId
        {
            get { return m_distributorId; }
            set { m_distributorId = value; }
        }

        public System.String OrgConfigId
        {
            get { return m_orgConfigId; }
            set { m_orgConfigId = value; }
        }

        public System.String OrgHierarchyId
        {
            get { return m_orgHierarchyId; }
            set { m_orgHierarchyId = value; }
        }

        public System.Decimal StartingAmount
        {
            get { return m_startingAmount; }
            set { m_startingAmount = value; }
        }

        public System.Int32 Grade
        {
            get { return m_grade; }
            set { m_grade = value; }
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

        private System.DateTime m_createdDate;

        public System.DateTime CreatedDate
        {
            get { return m_createdDate; }
            set { m_createdDate = value; }
        }

        private System.Int32 m_modifiedBy;

        public System.Int32 ModifiedBy
        {
            get { return m_modifiedBy; }
            set { m_modifiedBy = value; }
        }

        public int LiveLocation
        {
            get;
            set;
        }
        public string TaxJurisdiction
        {
            get;
            set;
        }
       
        public int RegAddLocationId
        {
            get { return m_RegAddLocationId; }
            set { m_RegAddLocationId = value; }
        }


        #endregion

        public bool Save(ref string errorMessage)
        {
            bool isSuccess = false;
            // call the save method which returns whether the save was successfull or not
            isSuccess = base.Save(Common.ToXml(this), SP_LOC_SAVE, ref errorMessage);
            return isSuccess;
        }

        public List<LocationHierarchy> Search()
        {
            List<LocationHierarchy> locList = new List<LocationHierarchy>();
            try
            {
                string errorMessage = string.Empty;
                System.Data.DataTable dTable = base.GetSelectedRecords(Common.ToXml(this), SP_LOC_SEARCH, ref errorMessage);

                if (dTable == null | (dTable != null && dTable.Rows.Count <= 0))
                    return null;

                foreach (System.Data.DataRow drow in dTable.Rows)
                {
                    LocationHierarchy loc = new LocationHierarchy();
                    loc.HierarchyCode = drow["Code"].ToString();
                    loc.HierarchyName = drow["Name"].ToString();
                    loc.Description = drow["Description"].ToString();
                    loc.HierarchyConfigId = Convert.ToInt32(drow["HierarchyConfigId"]);
                    loc.ParentHierarchyName = drow["ParentName"].ToString();
                    loc.HierarchyLevel = Convert.ToInt32(drow["Level"]);
                    loc.Status = Convert.ToInt32(drow["Status"]);
                    loc.ParentHierarchyId = Convert.ToInt32(drow["ParentHierarchyId"]);
                    loc.HierarchyId = Convert.ToInt32(drow["HierarchyId"]);
                    //org.HierarchyType = Convert.ToInt32(drow["HierarchyType"]);
                    loc.ModifiedDate = drow["ModifiedDate"].ToString();

                    loc.Address1 = drow["Address1"].ToString();
                    loc.Address2 = drow["Address2"].ToString();
                    loc.Address3 = drow["Address3"].ToString();

                    loc.City= drow["City"].ToString();
                    loc.State = drow["State"].ToString();
                    loc.Country = drow["Country"].ToString();
                    loc.CityId = Convert.ToInt32(drow["CityId"]);
                    loc.StateId = Convert.ToInt32(drow["StateId"]);
                    loc.CountryId = Convert.ToInt32(drow["CountryId"]);
                    
                    loc.PinCode = drow["PinCode"].ToString();
                    loc.Email1 = drow["EmailId1"].ToString();
                    loc.PhoneNumber1 = drow["Phone1"].ToString();
                    loc.Mobile1 = drow["Mobile1"].ToString();
                    loc.Fax1 = drow["Fax1"].ToString();
                    loc.Website = drow["Website"].ToString();
                    loc.ReplenishmentLocationId = drow["ReplenishmentLocationId"].ToString();
                    loc.TinNo = drow["TinNo"].ToString();
                    //loc.TaxJurisdiction = drow["TaxJurisdiction"].ToString();
                    loc.DistributorId = drow["DistributorId"].ToString();
                    loc.StartingAmount = Convert.ToDecimal(drow["StartingAmount"]);
                    loc.Grade = Convert.ToInt32(drow["Grade"]);
                    loc.VatNo = drow["VatNo"].ToString();
                    loc.CstNo = drow["CstNo"].ToString();
                    loc.ShortName = drow["ShortName"].ToString();
                    loc.OrgName = drow["OrgName"].ToString();
                    loc.OrgConfigId = drow["OrgConfigId"].ToString();
                    loc.OrgHierarchyId = drow["OrgHierarchyId"].ToString();
                    loc.OrgLevel = Convert.ToInt32(drow["OrgLevel"]);
                    loc.StatusValue = drow["StatusValue"].ToString();
                    loc.LiveLocation = Convert.ToInt32(drow["IsLiveLocation"]);
                    loc.TaxJurisdiction = drow["TaxJurisdiction"].ToString();
                    loc.IECCode = drow["IECCode"].ToString();
                    loc.IsMiniBranch = Convert.ToInt32(drow["IsMiniBranch"]);                    
                    loc.RegAddLocationId = Convert.ToInt32(drow["RegAddLocationId"]);
                    locList.Add(loc);
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
            }
            return locList;
        }

        public List<Contact> ContactSearch(int hierarchyId)
        {
            List<Contact> cntList = new List<Contact>();
            try
            {
                string errorMessage = string.Empty;
                System.Data.DataTable dTable = base.GetContactRecords(SP_LOC_CONTACT_SEARCH, hierarchyId, ref errorMessage);

                if (dTable == null | dTable.Rows.Count <= 0)
                    return null;

                foreach (System.Data.DataRow drow in dTable.Rows)
                {
                    Contact cnt = new Contact();
                    cnt.ContactId = drow["ContactId"].ToString();
                    cnt.RowNum = Convert.ToInt32(drow["RowNum"].ToString());

                    cnt.Title = Convert.ToInt32(drow["Title"].ToString());
                    cnt.FirstName = drow["FirstName"].ToString();
                    cnt.MiddleName = drow["MiddleName"].ToString();
                    cnt.LastName = drow["LastName"].ToString();
                    cnt.Address1 = drow["Address1"].ToString();
                    cnt.Address2 = drow["Address2"].ToString();
                    cnt.Address3 = drow["Address3"].ToString();
                    cnt.PinCode = drow["PinCode"].ToString();
                    cnt.Mobile1 = drow["Mobile1"].ToString();
                    cnt.PhoneNumber1 = drow["Phone1"].ToString();
                    cnt.Fax1 = drow["Fax1"].ToString();
                    cnt.Email1 = drow["EmailId1"].ToString();
                    cnt.Designation = drow["Designation"].ToString();
                    cnt.CountryId =Convert.ToInt32(drow["CountryId"]);
                    cnt.StateId = Convert.ToInt32(drow["StateId"]);
                    cnt.CityId = Convert.ToInt32(drow["CityId"]);

                    cnt.Country= drow["Country"].ToString();
                    cnt.State = drow["State"].ToString();
                    cnt.City = drow["City"].ToString();

                    cnt.Website = drow["Website"].ToString();
                    cnt.IsPrimary = Convert.ToBoolean(drow["IsPrimary"]);
                    cnt.Status = Convert.ToInt32(drow["Status"]);
                    cnt.ModifiedDate = drow["ModifiedDate"].ToString();
                    cnt.StatusValue = drow["StatusValue"].ToString();

                    cntList.Add(cnt);
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
            }
            return cntList;
        }

        public List<LocationHierarchy> ConfigSearch()
        {
            List<LocationHierarchy> locList = new List<LocationHierarchy>();
            try
            {
                string errorMessage = string.Empty;
                System.Data.DataTable dTable = base.GetConfigRecords(SP_LOC_CONFIG_SEARCH, ref errorMessage);

                if (dTable == null | dTable.Rows.Count <= 0)
                    return null;

                foreach (System.Data.DataRow drow in dTable.Rows)
                {
                    LocationHierarchy loc = new LocationHierarchy();
                    loc.HierarchyConfigId = Convert.ToInt32(drow["LocationConfigId"].ToString());
                    loc.HierarchyName = drow["Name"].ToString();
                    locList.Add(loc);
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
            }
            return locList;
        }
                
        /// <summary>
        /// code for location popup
        /// </summary>
        /// <returns>List of location hierarchy type</returns>
        public List<LocationHierarchy> SearchLocationType()
        {
            List<LocationHierarchy> locList = new List<LocationHierarchy>();
            LocationHierarchy loc = null;
            DBParameterList dbParam;            
            try
            {
                using (DataTaskManager dtManager = new DataTaskManager())
                {
                    string errorMessage = string.Empty;
                    dbParam = new DBParameterList();
                    dbParam.Add(new DBParameter("@LocationCode", this.HierarchyCode, DbType.String));
                    dbParam.Add(new DBParameter("@LocationName", this.HierarchyName, DbType.String));
                    dbParam.Add(new DBParameter("@City", this.City, DbType.String));
                    dbParam.Add(new DBParameter("@LocationType", this.LocationType, DbType.String));
                    dbParam.Add(new DBParameter("@Status", this.Status, DbType.String));
                    dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, errorMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));
                    System.Data.DataSet ds = dtManager.ExecuteDataSet("usp_LocationSearchPopup",dbParam);
                               
                    if (ds == null | ds.Tables[0].Rows.Count <= 0)
                        return null;

                    foreach (System.Data.DataRow drow in ds.Tables[0].Rows)
                    {
                        loc = new LocationHierarchy();
                        loc.HierarchyCode = drow["LocationCode"].ToString();
                        loc.HierarchyName = drow["LocationName"].ToString();
                        loc.City = drow["CityName"].ToString();
                        loc.StatusValue = drow["StatusValue"].ToString();
                        locList.Add(loc);
                    }
                }            
                return locList;
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                throw ex;
            }            
        }
         
    }
}
