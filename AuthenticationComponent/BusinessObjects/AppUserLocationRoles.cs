using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AuthenticationComponent.BusinessObjects
{
    public class AppUserLocationRoles
    {
        public AppUserLocationRoles()
        {
            m_roles = new List<Role>();
        }

        private int m_locationId;

        public int LocationId
        {
            get { return m_locationId; }
            set { m_locationId = value; }
        }

        private string m_locationCode;

        public string LocationCode
        {
            get { return m_locationCode; }
            set { m_locationCode = value; }
        }

        private string m_locationName;

        public string LocationName
        {
            get { return m_locationName; }
            set { m_locationName = value; }
        }

        private List<Role> m_roles;

        public List<Role> Roles
        {
            get { return m_roles; }
            set { m_roles = value; }
        }

    }
}
