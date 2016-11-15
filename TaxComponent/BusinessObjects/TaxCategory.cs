using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaxComponent.BusinessObjects
{
    public class TaxCategory
    {

        private System.Int32 m_taxTypeId;

        public System.Int32 TaxTypeId
        {
            get { return m_taxTypeId; }
            set { m_taxTypeId = value; }
        }

        private System.String m_taxTypeCode;

        public System.String TaxTypeCode
        {
            get { return m_taxTypeCode; }
            set { m_taxTypeCode = value; }
        }

        private System.String m_name;

        public System.String Name
        {
            get { return m_name; }
            set { m_name = value; }
        }

        private System.Byte m_status;

        public System.Byte Status
        {
            get { return m_status; }
            set { m_status = value; }
        }

        private System.Int32 m_createdBy;

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

        private System.DateTime m_modifiedDate;

        public System.DateTime ModifiedDate
        {
            get { return m_modifiedDate; }
            set { m_modifiedDate = value; }
        }
    }
}
