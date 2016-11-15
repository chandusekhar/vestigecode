using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Vinculum.Framework.Data;
using Vinculum.Framework.DataTypes;
using CoreComponent.Core.BusinessObjects;

namespace AuthenticationComponent.BusinessObjects
{
    public class LocationRole
    {

        public LocationRole()
        {

        }
        
        #region Properties

        private int m_roleId;
        public int RoleId
        {
            get { return m_roleId; }
            set { m_roleId = value; }
        }

        private string m_roleName;
        public string RoleName
        {
            get { return m_roleName; }
            set { m_roleName = value; }

        }

        private int m_locationId;
        public int LocationId
        {
            get { return m_locationId; }
            set { m_locationId = value; }
        }

        private string m_locationName;
        public string LocationName
        {
            get { return m_locationName; }
            set { m_locationName = value; }
        }

        private string m_locationType;
        public string LocationType
        {
            get { return m_locationType; }
            set { m_locationType = value; }
        }

        #endregion Properties

        #region Constants

        private const string GET_ALL_LOCATIONS = "usp_GetAllLocations";

        #endregion Constants

        #region Methods

        /// <summary>
        /// Method to get list of all locations (HO/BO/WH/PC).
        /// </summary>
        /// <returns>List of Location(s)</returns>
        public static List<LocationRole> GetAllLocations()
        {
            List<LocationRole> lLocations = new List<LocationRole>();
            try
            {
                using (DataTaskManager dtManager = new DataTaskManager())
                {
                    DBParameterList dbParam = new DBParameterList();
                    using (DataTable dtLocations = dtManager.ExecuteDataTable(GET_ALL_LOCATIONS, dbParam))
                    {
                        if (dtLocations != null && dtLocations.Rows.Count > 0)
                        {
                            for (int index = 0; index < dtLocations.Rows.Count; index++)
                            {
                                LocationRole tLocation = new LocationRole();
                                tLocation.LocationId = Convert.ToInt32(dtLocations.Rows[index]["LocationId"]);
                                tLocation.LocationName = Convert.ToString(dtLocations.Rows[index]["LocationName"]);
                                tLocation.LocationType = Convert.ToString(dtLocations.Rows[index]["LocationType"]);
                                lLocations.Add(tLocation);
                            }
                        }
                        else
                        {
                            throw new Exception(Common.GetMessage("2004"));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lLocations;
        }

        #endregion Methods
    }
}
