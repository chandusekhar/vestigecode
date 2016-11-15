using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaxComponent.BusinessObjects
{
    public class TaxDetail
    {

        private System.Int32 m_taxCodeId;

        public System.Int32 TaxCodeId
        {
            get { return m_taxCodeId; }
            set { m_taxCodeId = value; }
        }

        private System.String m_taxCode;

        public System.String TaxCode
        {
            get { return m_taxCode; }
            set { m_taxCode = value; }
        }

        private System.DateTime m_startDate;

        public System.DateTime StartDate
        {
            get { return m_startDate; }
            set { m_startDate = value; }
        }

        private System.Decimal m_taxPercent;

        public System.Decimal TaxPercent
        {
            get { return m_taxPercent; }
            set { m_taxPercent = value; }
        }

        private System.String m_description;

        public System.String Description
        {
            get { return m_description; }
            set { m_description = value; }
        }

        private System.Int32 m_status;

        public System.Int32 Status
        {
            get { return m_status; }
            set { m_status = value; }
        }

        public string StatusName
        {
            get;
            set;
        }

        private System.Int32 m_createdBy;

        public System.Int32 CreatedBy
        {
            get { return m_createdBy; }
            set { m_createdBy = value; }
        }


        public string CreatedDate
        {
            get;
            set;
        }

        private System.Int32 m_modifiedBy;

        public System.Int32 ModifiedBy
        {
            get { return m_modifiedBy; }
            set { m_modifiedBy = value; }
        }

        public int GroupOrder
        {
            get;
            set;
        }

        public string ModifiedDate
        {
            get;
            set;
        }
        public bool IsInclusive
        {
            get;
            set;
        }
    }
}
