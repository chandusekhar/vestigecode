using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CoreComponent.Core.BusinessObjects;
using CoreComponent.BusinessObjects;

namespace CoreComponent.BusinessObjects
{
    [Serializable]
    public class LocationTerminal : CoreComponent.Hierarchies.BusinessObjects.Hierarchy
    {
        #region SP Declaration
        private const string SP_LOC_TERMINAL_SEARCH = "usp_LocationTerminalSearch";
        #endregion

        public string TerminalCode
        {
            get;
            set;
        }

        public string StatusName
        {
            get;
            set;
        }

        public int Status
        {
            get;
            set;
        }

        public List<LocationTerminal> TerminalSearch(int hierarchyId)
        {
            List<LocationTerminal> cntList = new List<LocationTerminal>();
            try
            {
                string errorMessage = string.Empty;
                System.Data.DataTable dTable = base.GetContactRecords(SP_LOC_TERMINAL_SEARCH, hierarchyId, ref errorMessage);

                if (dTable == null | dTable.Rows.Count <= 0)
                    return null;

                foreach (System.Data.DataRow drow in dTable.Rows)
                {
                    LocationTerminal cnt = new LocationTerminal();
                    cnt.TerminalCode = drow["TerminalCode"].ToString();
                    cnt.Status = Convert.ToInt32(drow["Status"]);
                    cnt.StatusName = drow["StatusName"].ToString();

                    cntList.Add(cnt);
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
            }
            return cntList;
        }


    }
}
