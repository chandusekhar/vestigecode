using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreComponent.BusinessObjects;
using CoreComponent.Core.BusinessObjects;
using Vinculum.Framework.DataTypes;
using Vinculum.Framework.Data;
using System.Data;


namespace PromotionsComponent.BusinessLayer
{
    public class PromotionTier
    {
        public PromotionTier()
        {
        }

        public PromotionTier(int promotionId, 
                                int tierId,
                                int conditionId,
                                int lineNumber,
                                int conditionOnId,
                                string conditionOn,
                                int conditionCodeId,
                                string conditionCode,
                                decimal buyQtyFrom, 
                                decimal buyQtyTo,
                                decimal qty,
                                int discountTypeId,
                                string discountTypeVal, 
                                decimal discountValue, 
                                int statusId, 
                                string status)
        {
            m_promotionId = promotionId;
            m_conditionId = conditionId;
            m_tierId = tierId;
            m_lineNumber = lineNumber;
            m_buyQtyFrom = buyQtyFrom;
            m_buyQtyTo = buyQtyTo;
            m_ConditionOnId = conditionOnId;
            m_ConditionOnVal = conditionOn;
            m_ConditionCodeId = conditionCodeId;
            m_ConditionCodeVal = conditionCode;
            m_Qty = qty;
            m_discountTypeId = discountTypeId;
            m_discountTypeVal = discountTypeVal;
            m_discountValue = discountValue;
            m_statusId = statusId;
            m_statusVal = status;
        }

        private System.Int32 m_promotionId;

        public System.Int32 PromotionId
        {
            get { return m_promotionId; }
            set { m_promotionId = value; }
        }

        private System.Int32 m_conditionId;

        public System.Int32 ConditionId
        {
            get { return m_conditionId ; }
            set { m_conditionId = value; }
        }

        private System.Int32 m_tierId;

        public System.Int32 TierId
        {
            get { return m_tierId; }
            set { m_tierId = value; }
        }

        private System.Int32 m_lineNumber;

        public System.Int32 LineNumber
        {
            get { return m_lineNumber; }
            set { m_lineNumber = value; }
        }

        private System.Decimal m_buyQtyFrom;

        public System.Decimal BuyQtyFrom
        {
            get { return m_buyQtyFrom; }
            set { m_buyQtyFrom = value; }
        }

        public System.Decimal DBBuyQtyFrom
        {
            get
            {
                return Math.Round(BuyQtyFrom, Common.DBQtyRounding, MidpointRounding.AwayFromZero);
            }
            set
            {
                throw new NotImplementedException("This property can not be explicitly set");
            }
        }

        public System.Decimal DisplayBuyQtyFrom
        {
            get
            {
                return Math.Round(DBBuyQtyFrom, Common.DisplayQtyRounding, MidpointRounding.AwayFromZero);
            }
            set
            {
                throw new NotImplementedException("This property can not be explicitly set");
            }
        }

        private System.Decimal m_buyQtyTo;

        public System.Decimal BuyQtyTo
        {
            get { return m_buyQtyTo; }
            set { m_buyQtyTo = value; }
        }

        public System.Decimal DBBuyQtyTo
        {
            get
            {
                return Math.Round(BuyQtyTo, Common.DBQtyRounding, MidpointRounding.AwayFromZero);
            }
            set
            {
                throw new NotImplementedException("This property can not be explicitly set");
            }
        }

        public System.Decimal DisplayBuyQtyTo
        {
            get
            {
                return Math.Round(DBBuyQtyTo, Common.DisplayQtyRounding, MidpointRounding.AwayFromZero);
            }
            set
            {
                throw new NotImplementedException("This property can not be explicitly set");
            }
        }

        private System.Decimal m_Qty;

        public System.Decimal Qty
        {
            get { return m_Qty; }
            set { m_Qty = value; }
        }

        public System.Decimal DBQty
        {
            get
            {
                return Math.Round(Qty, Common.DBQtyRounding, MidpointRounding.AwayFromZero);
            }
            set
            {
                throw new NotImplementedException("This property can not be explicitly set");
            }
        }

        public System.Decimal DisplayQty
        {
            get
            {
                return Math.Round(DBQty, Common.DisplayQtyRounding, MidpointRounding.AwayFromZero);
            }
            set
            {
                throw new NotImplementedException("This property can not be explicitly set");
            }
        }

        private System.Int32 m_discountTypeId;

        public System.Int32 DiscountTypeId
        {
            get { return m_discountTypeId; }
            set { m_discountTypeId = value; }
        }

        private System.String m_discountTypeVal;

        public System.String DiscountTypeVal
        {
            get { return m_discountTypeVal; }
            set { m_discountTypeVal = value; }
        }

        private System.Decimal m_discountValue;

        public System.Decimal DiscountValue
        {
            get { return m_discountValue; }
            set { m_discountValue = value; }
        }

        public System.Decimal DBDiscountValue
        {
            get
            {
                return Math.Round(DiscountValue, Common.DBAmountRounding, MidpointRounding.AwayFromZero);
            }
            set
            {
                throw new NotImplementedException("This property can not be explicitly set");
            }
        }

        public System.Decimal DisplayDiscountValue
        {
            get
            {
                return Math.Round(DBDiscountValue, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
            }
            set
            {
                throw new NotImplementedException("This property can not be explicitly set");
            }
        }

        private System.Int32 m_ConditionOnId;

        public System.Int32 ConditionOnId
        {
            get
            {
                return m_ConditionOnId;
            }
            set { m_ConditionOnId = value; }
        }

        private String m_ConditionOnVal;

        public String ConditionOnVal
        {
            get
            {
                if (m_ConditionOnId == Common.INT_DBNULL)
                {
                    return string.Empty;
                }
                else
                {
                    return m_ConditionOnVal;
                }
            }
            set { m_ConditionOnVal = value; }
        }

        private int m_ConditionCodeId;

        public int ConditionCodeId
        {
            get { return m_ConditionCodeId; }
            set { m_ConditionCodeId = value; }
        }

        private System.String m_ConditionCodeVal;

        public System.String ConditionCodeVal
        {
            get
            {
                if (m_ConditionCodeId == Common.INT_DBNULL)
                {
                    return string.Empty;
                }
                else
                {
                    return m_ConditionCodeVal;
                }

                //return m_ConditionCodeVal;
            }
            set { m_ConditionCodeVal = value; }
        }

        private System.Int32 m_statusId;

        public System.Int32 StatusId
        {
            get { return m_statusId; }
            set { m_statusId = value; }
        }

        private System.String m_statusVal;

        public System.String StatusVal
        {
            get { return m_statusVal; }
            set { m_statusVal = value; }
        }

        //This property is used to get the mapping between Tiers and condition.
        //would be required in Stored Proc for bulk insert
        private int m_TempConditionId;
        public int TempConditionId
        {
            get
            {
                return m_TempConditionId;
            }
            set
            {
                m_TempConditionId = value;
            }
        }


        public void UpdateTier(PromotionTier tier, 
                                int promotionId, 
                                int tierId, 
                                int conditionId,
                                int lineNumber,
                                int conditionOnId,
                                string conditionOn,
                                int conditionCodeId,
                                string conditionCode,
                                decimal buyQtyFrom, 
                                decimal buyQtyTo, 
                                decimal  qty,
                                int discountTypeId,
                                string discountTypeVal, decimal discountValue, int statusId, string status)
        {
            tier.PromotionId   = promotionId;
            tier.TierId   = tierId;
            tier.ConditionId = conditionId;  
            tier.LineNumber  = lineNumber;
            tier.BuyQtyFrom   = buyQtyFrom;
            tier.BuyQtyTo = buyQtyTo;
            tier.ConditionOnId = conditionOnId;
            tier.ConditionOnVal = conditionOn;
            tier.ConditionCodeId = conditionCodeId;
            tier.ConditionCodeVal = conditionCode;
            tier.Qty = qty;
            tier.DiscountTypeId  = discountTypeId;
            tier.DiscountTypeVal= discountTypeVal;
            tier.DiscountValue = discountValue;
            tier.StatusId  = statusId;
            tier.StatusVal = status;

        }

        public double FetchMinimumDistributorPrice(int itemType, int id, ref string errorMessage)
        {
            double minDistributorPrice = Common.INT_DBNULL;
            DBParameterList dbParam;
            using (DataTaskManager dtManager = new DataTaskManager())
            {
                dbParam = new DBParameterList();
                dbParam.Add(new DBParameter(Common.PARAM_DATA, itemType, DbType.String));
                dbParam.Add(new DBParameter(Common.PARAM_DATA2, id, DbType.String));
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, errorMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                DataTable dt = dtManager.ExecuteDataTable("usp_getMinDistributorPrice", dbParam);
                errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();

                if (string.IsNullOrEmpty(errorMessage))
                {
                    minDistributorPrice = Convert.ToDouble(dt.Rows[0][0].ToString());
                }
            }

            return minDistributorPrice;
        }
    }
}
