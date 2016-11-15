using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoreComponent.BusinessObjects
{
    [Serializable]
    public class DistributorAccount
    {

        #region Properties

        public String DistributorId
        {
            get;
            set;
        }

        public String DistributorPANNumber
        {
            get;
            set;
        }

        public String DistributorBankName
        {
            get;
            set;
        }

        public String DistributorBankBranch
        {
            get;
            set;
        }

        public String DistributorBankAccNumber
        {
            get;
            set;
        }

        public int BankID
        {
            get;
            set;
        }

        public int IsPrimary
        {
            get;
            set;
        }

        public Int32 CreatedBy
        {
            get;
            set;
        }

        public DateTime CreatedDate
        {
            get;
            set;
        }

        public Int32 ModifiedBy
        {
            get;
            set;
        }

        public DateTime ModifiedDate
        {
            get;
            set;
        }

        #endregion

    }
}
