using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreComponent.BusinessObjects;
using CoreComponent.Core.BusinessObjects;

namespace PromotionsComponent.BusinessLayer
{
    public class PromotionCondition
    {
        public PromotionCondition()
        {
        }

        public PromotionCondition(int promotionId,
                                    int conditionId,
                                    int conditionTypeId,
                                    string conditionType,
                                    int conditionOnId,
                                    string conditionOn,
                                    int conditionCodeId,
                                    string conditionCode,
                                    decimal minBuyQty,
                                    decimal maxBuyQty,
                                    decimal qty,
                                    int discountTypeId,
                                    string discountType,
                                    decimal discountValue,
                                    int statusId,
                                    string status
                                    )
        {
            m_promotionId = promotionId;
            m_conditionId = conditionId;
            m_conditionTypeId = conditionTypeId;
            m_conditionTypeVal = conditionType;
            m_ConditionOnId = conditionOnId;
            m_ConditionOnVal = conditionOn;
            m_ConditionCodeId = conditionCodeId;
            m_ConditionCodeVal = conditionCode;
            m_minBuyQty = minBuyQty;
            m_maxBuyQty = maxBuyQty;
            m_Qty = qty;
            m_discountTypeId = discountTypeId;
            m_discountTypeVal = discountType;
            m_discountValue = discountValue;
            m_StatusId = statusId;
            m_StatusVal = status;
        }

        private System.Int32 m_promotionId = Common.INT_DBNULL;

        public System.Int32 PromotionId
        {
            get { return m_promotionId; }
            set { m_promotionId = value; }
        }

        private System.Int32 m_conditionId = Common.INT_DBNULL;

        public System.Int32 ConditionId
        {
            get { return m_conditionId; }
            set { m_conditionId = value; }
        }

        private System.Int32 m_conditionTypeId;

        public System.Int32 ConditionTypeId
        {
            get { return m_conditionTypeId; }
            set { m_conditionTypeId = value; }
        }

        private String m_conditionTypeVal;

        public String ConditionTypeVal
        {
            get
            {
                if (m_conditionTypeId == Common.INT_DBNULL)
                {
                    return string.Empty;
                }
                else
                {
                    return m_conditionTypeVal;
                }
            }
            set { m_conditionTypeVal = value; }
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

        private System.Decimal m_minBuyQty;

        public System.Decimal MinBuyQty
        {
            get { return m_minBuyQty; }
            set { m_minBuyQty = value; }
        }

        public System.Decimal DBMinBuyQty
        {
            get
            {
                return Math.Round(MinBuyQty, Common.DBQtyRounding, MidpointRounding.AwayFromZero);
            }
            set
            {
                throw new NotImplementedException("This property can not be explicitly set");
            }
        }

        public System.Decimal DisplayMinBuyQty
        {
            get
            {
                return Math.Round(DBMinBuyQty, Common.DisplayQtyRounding, MidpointRounding.AwayFromZero);
            }
            set
            {
                throw new NotImplementedException("This property can not be explicitly set");
            }
        }

        private System.Decimal m_maxBuyQty;

        public System.Decimal MaxBuyQty
        {
            get { return m_maxBuyQty; }
            set { m_maxBuyQty = value; }
        }

        public System.Decimal DBMaxBuyQty
        {
            get
            {
                return Math.Round(MaxBuyQty, Common.DBQtyRounding, MidpointRounding.AwayFromZero);
            }
            set
            {
                throw new NotImplementedException("This property can not be explicitly set");
            }
        }

        public System.Decimal DisplayMaxBuyQty
        {
            get
            {
                return Math.Round(DBMaxBuyQty, Common.DisplayQtyRounding, MidpointRounding.AwayFromZero);
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

        private String m_discountTypeVal;

        public String DiscountTypeVal
        {
            get
            {
                if (m_discountTypeId == Common.INT_DBNULL)
                {
                    return string.Empty;
                }
                else
                {
                    return m_discountTypeVal;
                }
            }
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

        private Int32 m_StatusId;
        public System.Int32 StatusId
        {
            get { return m_StatusId; }
            set { m_StatusId = value; }
        }

        private string m_StatusVal;
        public String StatusVal
        {
            get
            {
                if (m_StatusId == Common.INT_DBNULL)
                {
                    return string.Empty;
                }
                else
                {
                    return m_StatusVal;
                }

            }
            set { m_StatusVal = value; }
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

        private List<PromotionTier> m_Tiers = new List<PromotionTier>();
        public List<PromotionTier> Tiers
        {
            get
            {
                return m_Tiers;
            }
            set
            {
                m_Tiers = value;
            }
        }


        public static void UpdateCondition(PromotionCondition condition,
                                            int promotionId,
                                            int conditionId,
                                            int conditionTypeId,
                                            string conditionType,
                                            int conditionOnId,
                                            string conditionOn,
                                            int conditionCodeId,
                                            string conditionCode,
                                            decimal minBuyQty,
                                            decimal maxBuyQty,
                                            decimal qty,
                                            int discountTypeId,
                                            string discountType,
                                            decimal discountValue,
                                            int statusId,
                                            string status)
        {
            condition.PromotionId = promotionId;
            condition.ConditionId = conditionId;
            condition.ConditionTypeId = conditionTypeId;
            condition.ConditionTypeVal = conditionType;
            condition.ConditionOnId = conditionOnId;
            condition.ConditionOnVal = conditionOn;
            condition.ConditionCodeId = conditionCodeId;
            condition.ConditionCodeVal = conditionCode;
            condition.MinBuyQty = minBuyQty;
            condition.MaxBuyQty = maxBuyQty;
            condition.Qty = qty;
            condition.DiscountTypeId = discountTypeId;
            condition.DiscountTypeVal = discountType;
            condition.DiscountValue = discountValue;
            condition.StatusId = statusId;
            condition.StatusVal = status;
        }

        public enum PromotionConditionOn
        {
            PRODUCT = 1,
            PRODUCTGROUP = 2,
            MERCHANDISINGHIERARCHY = 3
        }

        public double FetchMinimumDistributorPrice(int itemType, int id, ref string errorMessage)
        {
            double minDistributorPrice = Common.INT_DBNULL;
            Vinculum.Framework.DataTypes.DBParameterList dbParam;
            using (Vinculum.Framework.Data.DataTaskManager dtManager = new Vinculum.Framework.Data.DataTaskManager())
            {
                dbParam = new Vinculum.Framework.DataTypes.DBParameterList();
                dbParam.Add(new Vinculum.Framework.DataTypes.DBParameter(Common.PARAM_DATA, itemType, System.Data.DbType.String));
                dbParam.Add(new Vinculum.Framework.DataTypes.DBParameter(Common.PARAM_DATA2, id, System.Data.DbType.String));
                dbParam.Add(new Vinculum.Framework.DataTypes.DBParameter(Common.PARAM_OUTPUT, errorMessage, System.Data.DbType.String, System.Data.ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                System.Data.DataTable dt = dtManager.ExecuteDataTable("usp_getMinDistributorPrice", dbParam);
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
