using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PromotionsComponent.BusinessLayer
{
    internal class PromotionMaster
    {

        private System.Int32 m_promotionId;

        public System.Int32 PromotionId
        {
            get { return m_promotionId; }
            set { m_promotionId = value; }
        }

        private System.String m_promotionName;

        public System.String PromotionName
        {
            get { return m_promotionName; }
            set { m_promotionName = value; }
        }

        private System.Int32 m_promotionCategory;

        public System.Int32 PromotionCategory
        {
            get { return m_promotionCategory; }
            set { m_promotionCategory = value; }
        }

        private System.String m_promotionCode;

        public System.String PromotionCode
        {
            get { return m_promotionCode; }
            set { m_promotionCode = value; }
        }

        private System.DateTime m_startDate;

        public System.DateTime StartDate
        {
            get { return m_startDate; }
            set { m_startDate = value; }
        }

        private System.DateTime m_endDate;

        public System.DateTime EndDate
        {
            get { return m_endDate; }
            set { m_endDate = value; }
        }

        private System.DateTime m_durationStart;

        public System.DateTime DurationStart
        {
            get { return m_durationStart; }
            set { m_durationStart = value; }
        }

        private System.DateTime m_durationEnd;

        public System.DateTime DurationEnd
        {
            get { return m_durationEnd; }
            set { m_durationEnd = value; }
        }

        private System.Int32 m_discountType;

        public System.Int32 DiscountType
        {
            get { return m_discountType; }
            set { m_discountType = value; }
        }

        private System.Decimal m_discountValue;

        public System.Decimal DiscountValue
        {
            get { return m_discountValue; }
            set { m_discountValue = value; }
        }

        private System.Decimal m_maxOrderQty;

        public System.Decimal MaxOrderQty
        {
            get { return m_maxOrderQty; }
            set { m_maxOrderQty = value; }
        }

        private System.Int32 m_status;

        public System.Int32 Status
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

        private System.Int32 m_modifiedBy;

        public System.Int32 ModifiedBy
        {
            get { return m_modifiedBy; }
            set { m_modifiedBy = value; }
        }

        private System.DateTime m_createdDate;

        public System.DateTime CreatedDate
        {
            get { return m_createdDate; }
            set { m_createdDate = value; }
        }

        private System.DateTime m_modifiedDate;

        public System.DateTime ModifiedDate
        {
            get { return m_modifiedDate; }
            set { m_modifiedDate = value; }
        }

        private System.Int32 m_applicability;

        public System.Int32 Applicability
        {
            get { return m_applicability; }
            set { m_applicability = value; }
        }

        private System.Int32 m_buyConditionType;

        public System.Int32 BuyConditionType
        {
            get { return m_buyConditionType; }
            set { m_buyConditionType = value; }
        }

        private System.Int32 m_getConditionType;

        public System.Int32 GetConditionType
        {
            get { return m_getConditionType; }
            set { m_getConditionType = value; }
        }

        public bool Save(string xmDoc, string spName, ref string errorMessage)
        { 
            return false; 
        }

        public Promotion Search(string xmlDoc, string spName, ref string errorMessage)
        {
            return null;
        }
    }
}
