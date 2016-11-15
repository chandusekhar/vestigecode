using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreComponent.Core.BusinessObjects;
using CoreComponent.MasterData.BusinessObjects;
using System.Data;
using Vinculum.Framework.Data;
using Vinculum.Framework.DataTypes;

namespace CoreComponent.BusinessObjects
{
    [Serializable]
    public class LocationList : ItemMaster, IItemLocation
    {
        private const string SP_TOI_CountryID_SEARCH = "usp_CountryIDSearch";
        private const string SP_ITEMLOCATION_SEARCH = "usp_ItemLocationSearch";
        private Int32 m_locationId = Common.INT_DBNULL;
        private String m_locationCode = String.Empty;
        private String m_locationName = String.Empty;
        private String m_locationDisplayName = String.Empty;
        private Decimal m_reorderLevel = Common.INT_DBNULL;

        public const String LOC_VALUE_MEM = "LocationId";
        public const String LOC_TEXT_MEM = "DisplayName";

        public const String GRID_ITEMID = "ItemId";
        public const String GRID_LOCID = "LocationId";
        public const String GRID_LOCNAME = "LocationName";
        public const String GRID_MODDATE = "ModifiedDate";

        public Int32 LocationId
        {
            get { return m_locationId; }
            set { m_locationId = value; }
        }
        public String LocationCode
        {
            get { return m_locationCode; }
            set { m_locationCode = value; }
        }
        public String LocationName
        {
            get { return m_locationName; }
            set { m_locationName = value; }
        }
        public String LocationDisplayName
        {
            get { return m_locationDisplayName; }
            set { m_locationDisplayName = value; }
        }

        public Decimal ReorderLevel
        {
            get { return m_reorderLevel; }
            set { m_reorderLevel = value; }
        }

        public System.Decimal DBReorderLevel
        {
            get
            {
                return Math.Round(ReorderLevel, Common.DBQtyRounding, MidpointRounding.AwayFromZero);
            }
            set
            {
                throw new NotImplementedException("This property can not be explicitly set");
            }
        }

        public System.Decimal DisplayReorderLevel
        {
            get
            {
                return Math.Round(DBReorderLevel, Common.DisplayQtyRounding, MidpointRounding.AwayFromZero);
            }
            set
            {
                throw new NotImplementedException("This property can not be explicitly set");
            }
        }

        public List<LocationList> search()
        {   
          
            List<LocationList> ItemList = new List<LocationList>();
            try
            {
                string errorMessage = string.Empty;
                DataTable dTable = GetSelectedRecords(Common.ToXml(this), SP_ITEMLOCATION_SEARCH, ref errorMessage);
                if (dTable != null)
                {
                    foreach (System.Data.DataRow drow in dTable.Rows)
                    {
                        LocationList list = new LocationList();
                        list.ItemCode = Convert.ToString(drow["ItemCode"]);
                        list.ItemId = Convert.ToInt32(drow["ItemId"]);
                        list.ItemName = Convert.ToString(drow["ItemName"]);
                        list.LocationId = Convert.ToInt32(drow["LocationId"]);
                        
                        ItemList.Add(list);
                    }
                }
                return ItemList;
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


        public bool Search(int Source,int Dest)
        {
            //List<int> toiList = new List<int>();
            List<int> listLocation = new List<int>();
            try
            {
                listLocation.Add(Source);
                listLocation.Add(Dest);
                string errorMessage = string.Empty;
                System.Data.DataTable dTable = GetCountryID(CoreComponent.Core.BusinessObjects.Common.ToXml(listLocation), SP_TOI_CountryID_SEARCH, ref errorMessage);

                if (dTable == null | dTable.Rows.Count == 0)
                    //return null;
                    return false;

                //foreach (System.Data.DataRow drow in dTable.Rows)
                //{
                

                int a = Convert.ToInt32(dTable.Rows[0][0]);
                int b = Convert.ToInt32(dTable.Rows[1][0]);
                if (a != b)
                {
                    return true;
                }
                else
                {
                    return false;
                }


                //toiList.Add(objLocation);
                //}
            }
            catch (Exception ex)
            {
                CoreComponent.Core.BusinessObjects.Common.LogException(ex);
            }
            return false;
        }
        public virtual DataTable GetCountryID(string xmlDoc, string spName, ref string errorMessage)
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


    
        
    }
}
