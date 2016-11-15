using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace POSClient.BusinessObjects
{
    [Serializable]
    public class DistributorLevel
    {

        public String DistributorId
        {
            get;
            set;
        }

        public Int32 LevelId
        {
            get;
            set;
        }

        public DateTime AchievedDate
        {
            get;
            set;
        }

        public Int32 Status
        {
            get;
            set;
        }


    }
}
