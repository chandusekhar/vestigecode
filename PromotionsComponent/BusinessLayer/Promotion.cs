using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data; 
using Vinculum.Framework.Data;
using Vinculum.Framework.DataTypes;
using CoreComponent.BusinessObjects;
using CoreComponent.Core.BusinessObjects;

namespace PromotionsComponent.BusinessLayer
{
    public class Promotion : IPromotion
    {
        #region CTOR
        public Promotion()
        {
        }

        public Promotion(int promotionId,
                         string promotionName,
                         int promotionCategoryId,
                         string promotionCategory,
                         string promotionCode,
                         DateTime startDate,
                         DateTime endDate,
                         DateTime durationStart,
                         DateTime durationEnd,
                         int discountTypeId,
                         string discountType,
                         Decimal discountValue,
                         Decimal maxOrderQty,
                         int statusId,
                         string status,
                         int createdBy,
                         int modifiedBy,
                         DateTime createdDate,
                         DateTime ModifiedDate,
                         int applicabilityId,
                         string applicability,
                         int buyConditionTypeId,
                         string buyConditionType,
                         int getConditionTypeId,
                         string getConditionType,
                        int repeatFactor,
                         List<PromotionCondition> conditions,
                         List<PromotionLocation> locations,
						 List<PromotionTier> tiers,
						 bool POS,
						 bool Web,
						 string WebImage)
        {
            m_PromotionId = promotionId;
            m_PromotionName = promotionName;
            m_PromotionCategoryId = promotionCategoryId;
            m_PromotionCategory = promotionCategory;
            m_PromotionCode = promotionCode;
            m_StartDate = startDate;
            m_EndDate = endDate;
            m_DurationStart = durationStart;
            m_DurationEnd = durationEnd;
            m_discountTypeId = discountTypeId;
            m_discountTypeVal = discountType;
            m_discountValue = discountValue;
            m_MaxOrderQty = maxOrderQty;
            m_StatusId = statusId;
            m_StatusVal = status;
            m_CreateBy = createdBy;
            m_ModifiedBy = modifiedBy;
            m_CreatedDate = createdDate;
            m_ModifiedDate = ModifiedDate;
            m_ApplicabilityId = applicabilityId;
            m_Applicability = applicability;
            m_BuyConditionTypeId = buyConditionTypeId;
            m_BuyConditionType = buyConditionType;
            m_GetConditionTypeId = getConditionTypeId;
            m_GetConditionType = getConditionType;
            m_RepeatFactor = repeatFactor; 
            m_Conditions = conditions;
			m_POS = POS;
			m_Web = Web;
			m_WebImage = WebImage;

            //Update Condition/Tiers Mapping
            for (int i = 0; i < m_Conditions.Count; i++)
            {
                m_Conditions[i].TempConditionId = i;
                for (int j = 0; j < m_Conditions[i].Tiers.Count; j++)
                {
                    m_Conditions[i].Tiers[j].TempConditionId = i;
                }
               }

            m_Locations = locations;
            m_Tiers = tiers;
        }
        


        //This constructor is for search functinality
        public Promotion(   string promotionName,
                            int promotionCategoryId,
                            string promotionCode,
                            DateTime startDate,
                            DateTime endDate,
                            int discountTypeId,
                            int statusId,
                            int applicabilityId)
        {
            m_PromotionName = promotionName;
            m_PromotionCategoryId = promotionCategoryId;
            m_PromotionCode = promotionCode;
            m_StartDate = startDate;
            m_EndDate = endDate;
            m_discountTypeId = discountTypeId;
            m_StatusId = statusId;
            m_ApplicabilityId = applicabilityId;
        }


        #endregion

        #region Properties
        
        private int m_PromotionId = Common.INT_DBNULL;
        public int PromotionId
        {
            get
            {
                return m_PromotionId;
            }
            set
            {
                m_PromotionId = value;
            }
        }

        private string m_PromotionName;
        public string PromotionName
        {
            get { return m_PromotionName; }
            set { m_PromotionName = value; }
        }

        private Int32 m_PromotionCategoryId;
        public Int32 PromotionCategoryId
        {
            get { return m_PromotionCategoryId; }
            set { m_PromotionCategoryId = value; }
        }

        private String m_PromotionCategory;
        public String PromotionCategory
        {
            get { return m_PromotionCategory; }
            set { m_PromotionCategory = value; }
        }

        private string m_PromotionCode;
        public string PromotionCode
        {
            get { return m_PromotionCode; }
            set { m_PromotionCode = value; }
        }

        private DateTime m_StartDate = Common.DATETIME_NULL;
        public DateTime StartDate
        {
            get { return m_StartDate; }
            set { m_StartDate = value; }
        }

        public String DisplayStartDate
        {
            get
            {
                if ((m_StartDate != null) && (m_StartDate.ToString().Length > 0))
                {
                    return StartDate.ToString(Common.DTP_DATE_FORMAT);
                }
                else
                {
                    return String.Empty;  
                }
            }
            set
            {
                //Setter would do nothing. This is just to 
                throw new InvalidOperationException("Can't Set. Setter is only there so that this property can be XML Serialised");
            }
        }

        private DateTime m_EndDate = Common.DATETIME_NULL;
        public DateTime EndDate
        {
            get { return m_EndDate; }
            set { m_EndDate = value; }
        }

        public String DisplayEndDate
        {
            get
            {
                if ((m_EndDate != null) && (m_EndDate.ToString().Length > 0))
                {
                    return EndDate.ToString(Common.DTP_DATE_FORMAT);
                }
                else
                {
                    return String.Empty;
                }
            }
            set
            {
                //Setter would do nothing. This is just to 
                throw new InvalidOperationException("Can't Set. Setter is only there so that this property can be XML Serialised");
            }
        }


        private DateTime m_DurationStart = Common.DATETIME_NULL;
        public DateTime DurationStart
        {
            get { return m_DurationStart; }
            set { m_DurationStart = value; }
        }

        public String DisplayDurationStart
        {
            get
            {
                if ((m_DurationStart != null) && (m_DurationStart.ToString().Length > 0)) 
                {
                    return m_DurationStart.ToString(Common.TIME_FORMAT);
                }
                else
                {
                    return string.Empty; 
                }
            }
            set
            {
                //Setter would do nothing. This is just to 
                throw new InvalidOperationException("Can't Set. Setter is only there so that this property can be XML Serialised"); 
            }
        }

        private DateTime m_DurationEnd = Common.DATETIME_NULL;
        public DateTime DurationEnd
        {
            get { return m_DurationEnd; }
            set { m_DurationEnd = value; }
        }

        public String DisplayDurationEnd
        {
            get
            {
                if (m_DurationEnd  != null)
                {
                    return m_DurationEnd.ToString(Common.TIME_FORMAT);
                }
                else
                {
                    return String.Empty;
                }
            }
            set
            {
                //Setter would do nothing. This is just to 
                throw new InvalidOperationException("Can't Set. Setter is only there so that this property can be XML Serialised");
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

		private System.Boolean m_POS;
		private System.Boolean m_Web;
		private System.String m_WebImage;

		public bool POS
		{
			get { return m_POS; }
			set { m_POS = value; }
		}
		public bool Web
		{
			get { return m_Web; }
			set { m_Web = value; }
		}
		public string WebImage
		{
			get { return m_WebImage; }
			set { m_WebImage = value; }
		}
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

        private System.Decimal m_MaxOrderQty;
        public System.Decimal MaxOrderQty
        {
            get { return m_MaxOrderQty; }
            set { m_MaxOrderQty = value; }
        }

        public System.Decimal DBMaxOrderQty
        {
            get
            {
                return Math.Round(MaxOrderQty, Common.DBQtyRounding, MidpointRounding.AwayFromZero);
            }
            set
            {
                throw new NotImplementedException("This property can not be explicitly set");
            }
        }

        public System.Decimal DisplayMaxOrderQty
        {
            get
            {
                return Math.Round(DBMaxOrderQty, Common.DisplayQtyRounding, MidpointRounding.AwayFromZero);
            }
            set
            {
                throw new NotImplementedException("This property can not be explicitly set");
            }
        }

        private int m_CreateBy;
        public int CreatedBy
        {
            get
            {
                return m_CreateBy;
            }
            set
            {
                m_CreateBy = value;
            }
        }

        private DateTime m_CreatedDate = Common.DATETIME_NULL;
        public DateTime CreatedDate
        {
            get
            {
                return m_CreatedDate;
            }
            set
            {
                m_CreatedDate = value;
            }
        }

        public String DisplayCreatedDate
        {
            get
            {
                if ((m_CreatedDate  != null) && (m_CreatedDate.ToString().Length > 0))
                {
                    return m_CreatedDate.ToString(Common.DTP_DATE_FORMAT);
                }
                else
                {
                    return String.Empty;
                }
            }
            set
            {
                //Setter would do nothing. This is just to 
                throw new NotImplementedException("This property can not be explicitly set");
            }
        }

        private int m_ModifiedBy;
        public int ModifiedBy
        {
            get
            {
                return m_ModifiedBy;
            }
            set
            {
                m_ModifiedBy = value;
            }
        }

        private DateTime m_ModifiedDate = Common.DATETIME_NULL;
        public DateTime ModifiedDate
        {
            get
            {
                return m_ModifiedDate;
            }
            set
            {
                m_ModifiedDate = value;
            }
        }

        public String DisplayModifiedDate
        {
            get
            {
                if ((m_ModifiedDate  != null) && (m_ModifiedDate.ToString().Length > 0))
                {
                    return m_ModifiedDate.ToString(Common.DATE_TIME_FORMAT);
                }
                else
                {
                    return String.Empty;
                }
            }
            set
            {
                //Setter would do nothing. This is just to 
                throw new NotImplementedException("This property can not be explicitly set");
            }
        }

        private Int32 m_ApplicabilityId;
        public Int32 ApplicabilityId
        {
            get { return m_ApplicabilityId; }
            set { m_ApplicabilityId = value; }
        }

        private String m_Applicability;
        public String Applicability
        {
            get { return m_Applicability; }
            set { m_Applicability = value; }
        }

        private Int32 m_BuyConditionTypeId;
        public Int32 BuyConditionTypeId
        {
            get { return m_BuyConditionTypeId; }
            set { m_BuyConditionTypeId = value; }
        }

        private String m_BuyConditionType;
        public String BuyConditionType
        {
            get { return m_BuyConditionType; }
            set { m_BuyConditionType = value; }
        }

        private Int32 m_GetConditionTypeId;
        public Int32 GetConditionTypeId
        {
            get { return m_GetConditionTypeId; }
            set { m_GetConditionTypeId = value; }
        }

        private String m_GetConditionType;
        public String GetConditionType
        {
            get { return m_GetConditionType; }
            set { m_GetConditionType = value; }
        }

        private Int32 m_RepeatFactor;
        public Int32 RepeatFactor
        {
            get { return m_RepeatFactor; }
            set { m_RepeatFactor = value; }
        }

        private List<PromotionCondition> m_Conditions = new List<PromotionCondition>();

        public List<PromotionCondition> Conditions
        {
            get
            {
                return m_Conditions;
            }
            set
            {
                m_Conditions = value;
            }
        }

        private List<PromotionLocation> m_Locations = new List<PromotionLocation>();

        public List<PromotionLocation> Locations
        {
            get
            {
                return m_Locations;
            }
            set
            {
                m_Locations = value;
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

        #endregion

        #region Enum
        public enum PromotionCategoryType
        {
            BillBuster=1,
            Line=2,
            Quantity=3,
            Volume=4,
        }

        public enum ConditionType
        {
            Buy = 1,
            Get = 2,
        }

        public enum ConditionOperation
        {
            And = 1,
            Or = 2,
        }

        public enum Status
        {
            Active = 1,
            Inactive = 2,
            Deleted = 3,
        }

        public enum DiscountType
        {
            PercentOff = 1,
            ValueOff = 2,
            FixedPrice = 3,
            FreeItem = 4
        }
        #endregion

        #region IPromotion Members
        #region Shopping Cart
        public string GetPromotionImage(int promotionID, string spName)
        {
            string fileName = null;
            DBParameterList dbParam;
            try
            {
                Vinculum.Framework.Data.DataTaskManager dtManager = new DataTaskManager();
                // initialize the parameter list object
                dbParam = new DBParameterList();

                // add the relevant 2 parameters
                dbParam.Add(new DBParameter("promotionID", promotionID, DbType.Int32));

                // executing procedure to save the record 
                DataTable dt = dtManager.ExecuteDataTable(spName, dbParam);

                if (dt == null || dt.Rows.Count <= 0)
                    return null;

                fileName = Convert.ToString(dt.Rows[0]["WebImage"]);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return fileName;
        }

        #endregion       

        public bool Save(string xmlDoc, string spName,ref Int32 promotionId, ref string errorMessage)
        {
            DBParameterList dbParam;
            bool isSuccess = false;
            using (DataTaskManager dtManager = new DataTaskManager())
            {
                dtManager.BeginTransaction();
                {
                    dbParam = new DBParameterList();
                    dbParam.Add(new DBParameter(Common.PARAM_DATA, xmlDoc , DbType.String));
                    dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, errorMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                    DataTable dt = dtManager.ExecuteDataTable(spName , dbParam);
                    errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();
                    if (errorMessage.Length > 0)
                    {
                        isSuccess = false;
                        dtManager.RollbackTransaction();
                    }
                    else
                    {
                        isSuccess = true;
                        promotionId = Convert.ToInt32(dt.Rows[0]["PromotionId"]);
                        dtManager.CommitTransaction();
                    }
                }
            }
            return isSuccess;
        }

        public Promotion Search(string xmlDoc, string spName, ref string errorMessage)
        {
            Promotion  promotion =new Promotion() ;
            DBParameterList dbParam;
            using (DataTaskManager dtManager = new DataTaskManager())
            {
                dbParam = new DBParameterList();
                dbParam.Add(new DBParameter(Common.PARAM_DATA, xmlDoc , DbType.String));
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, errorMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                DataSet ds = dtManager.ExecuteDataSet(spName , dbParam);
                errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();

                if (ds != null)
                {
                    //Add promotion Master
                    List<PromotionCondition> conditions = new List<PromotionCondition>();  

                        //Add list of condition to the promotion object
                        for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                        {
                            PromotionCondition condition = new PromotionCondition(
                                                                                    Convert.ToInt32(ds.Tables[1].Rows[i]["PromotionId"]),
                                                                                    Convert.ToInt32(ds.Tables[1].Rows[i]["ConditionId"]),
                                                                                    Convert.ToInt32(ds.Tables[1].Rows[i]["ConditionTypeId"]),
                                                                                    Convert.ToString(ds.Tables[1].Rows[i]["ConditionType"]),
                                                                                    Convert.ToInt32(ds.Tables[1].Rows[i]["ConditionOnId"]),
                                                                                    Convert.ToString(ds.Tables[1].Rows[i]["ConditionOn"]),
                                                                                    Convert.ToInt32(ds.Tables[1].Rows[i]["ConditionCodeId"]),
                                                                                    Validators.CheckForDBNull(ds.Tables[1].Rows[i]["ConditionCode"],string.Empty), 
                                                                                    Convert.ToInt32(ds.Tables[1].Rows[i]["MinBuyQty"]),
                                                                                    Convert.ToInt32(ds.Tables[1].Rows[i]["MaxBuyQty"]),
                                                                                    Validators.CheckForDBNull(ds.Tables[1].Rows[i]["Qty"],(decimal)0),
                                                                                    Convert.ToInt32(ds.Tables[1].Rows[i]["DiscountTypeId"]),
                                                                                    Validators.CheckForDBNull(Convert.ToString(ds.Tables[1].Rows[i]["DiscountType"]),String.Empty)  ,
                                                                                    Convert.ToDecimal(ds.Tables[1].Rows[i]["DiscountValue"]),
                                                                                    Convert.ToInt32(ds.Tables[1].Rows[i]["StatusId"]),
                                                                                    Convert.ToString(ds.Tables[1].Rows[i]["Status"])
                                                                                    );

                            
                            DataRow[] drTiers = ds.Tables[2].Select("ConditionId = " + Convert.ToString(ds.Tables[1].Rows[i]["ConditionId"]));  
                            //Add list of tiers to the condition
                            List<PromotionTier> tiers = new List<PromotionTier>();  
                            PromotionTier tier;
                            for (int j = 0; j < drTiers.Length ; j++)
                            {
                                tier = new PromotionTier(
                                                            Convert.ToInt32(drTiers[j]["PromotionId"]),
                                                            Convert.ToInt32(drTiers[j]["TierId"]),
                                                            Convert.ToInt32(drTiers[j]["ConditionId"]),
                                                            Common.INT_DBNULL,
                                                            Validators.CheckForDBNull(drTiers[j]["ConditionOnId"], Common.INT_DBNULL),
                                                            Validators.CheckForDBNull  ( drTiers[j]["ConditionOn"],String.Empty), 
                                                            Validators.CheckForDBNull ( drTiers[j]["ConditionCodeId"],Common.INT_DBNULL),
                                                            Validators.CheckForDBNull(drTiers[j]["ConditionCode"], string.Empty), 
                                                            Convert.ToInt32(drTiers[j]["BuyQtyFrom"]),
                                                            Convert.ToInt32(drTiers[j]["BuyQtyTo"]),
                                                            Convert.ToInt32(drTiers[j]["Qty"]),
                                                            Convert.ToInt32(drTiers[j]["DiscountTypeId"]),
                                                            Convert.ToString(drTiers[j]["DiscountTypeVal"]),
                                                            //Convert.ToInt32(drTiers[j]["DiscountValue"]),
                                                            Convert.ToDecimal(drTiers[j]["DiscountValue"]),
                                                            Convert.ToInt32(drTiers[j]["StatusId"]),
                                                            Convert.ToString(drTiers[j]["Status"]));
                                condition.Tiers.Add(tier);    

                            }
                            conditions.Add(condition);    
                        }

                        List<PromotionLocation> locations = new List<PromotionLocation>();  
                        for (int i = 0; i < ds.Tables[3].Rows.Count; i++)
                        {
                            PromotionLocation location = new PromotionLocation(
                                                                                Convert.ToInt32(ds.Tables[3].Rows[i]["LocationId"]),
                                                                                Convert.ToInt32(ds.Tables[3].Rows[i]["PromotionId"]),
                                                                                Convert.ToString(ds.Tables[3].Rows[i]["LocationVal"]), 
                                                                                Convert.ToString(ds.Tables[3].Rows[i]["LocationCode"]), 
                                                                                Convert.ToString(ds.Tables[3].Rows[i]["LocationType"]), 
                                                                                Convert.ToInt32(ds.Tables[3].Rows[i]["LineNumber"]),
                                                                                Convert.ToInt32(ds.Tables[3].Rows[i]["StatusId"]),
                                                                                Convert.ToString(ds.Tables[3].Rows[i]["Status"]));
                            locations.Add(location);
                        }
                        promotion = new Promotion(
                                                        Convert.ToInt32(ds.Tables[0].Rows[0]["PromotionId"]),
                                                        Convert.ToString(ds.Tables[0].Rows[0]["PromotionName"]),
                                                        Convert.ToInt32(ds.Tables[0].Rows[0]["PromotionCategoryId"]),
                                                        Convert.ToString(ds.Tables[0].Rows[0]["PromotionCategory"]),
                                                        Convert.ToString(ds.Tables[0].Rows[0]["PromotionCode"]),
                                                        Convert.ToDateTime(ds.Tables[0].Rows[0]["StartDate"]),
                                                        Convert.ToDateTime(ds.Tables[0].Rows[0]["EndDate"]),//check for null
                                                        Convert.ToDateTime(ds.Tables[0].Rows[0]["DurationStart"]),//check for null
                                                        Convert.ToDateTime(ds.Tables[0].Rows[0]["DurationEnd"]),//check for null
                                                        Convert.ToInt32(ds.Tables[0].Rows[0]["DiscountTypeId"]),//check for null
                                                        Validators.CheckForDBNull(ds.Tables[0].Rows[0]["DiscountType"],String.Empty),//check for null
                                                        Convert.ToDecimal(ds.Tables[0].Rows[0]["DiscountValue"]),//check for null
                                                        Convert.ToDecimal(ds.Tables[0].Rows[0]["MaxOrderQty"]),
                                                        Convert.ToInt32(ds.Tables[0].Rows[0]["StatusId"]),
                                                        Convert.ToString(ds.Tables[0].Rows[0]["Status"]),
                                                        Convert.ToInt32(ds.Tables[0].Rows[0]["CreatedBy"]),
                                                        Convert.ToInt32(ds.Tables[0].Rows[0]["ModifiedBy"]),
                                                        Convert.ToDateTime(ds.Tables[0].Rows[0]["CreatedDate"]),
                                                        Convert.ToDateTime(ds.Tables[0].Rows[0]["ModifiedDate"]),//check for null
                                                        Validators.CheckForDBNull(ds.Tables[0].Rows[0]["ApplicabilityId"],Common.INT_DBNULL),//check for null
                                                        Validators.CheckForDBNull(ds.Tables[0].Rows[0]["Applicability"],string.Empty),//check for null
                                                        Validators.CheckForDBNull(ds.Tables[0].Rows[0]["BuyConditionTypeId"],Common.INT_DBNULL),//check for null
                                                        Validators.CheckForDBNull(ds.Tables[0].Rows[0]["BuyConditionType"],string.Empty),
                                                        Validators.CheckForDBNull(ds.Tables[0].Rows[0]["GetConditionTypeId"],Common.INT_DBNULL ),
                                                        Validators.CheckForDBNull(ds.Tables[0].Rows[0]["GetConditionType"],string.Empty ),
                                                        Convert.ToInt32 (ds.Tables[0].Rows[0]["RepeatFactor"]),
													    conditions, locations, null,
                                                     #region Shopping Cart
                                                      Convert.ToBoolean(ds.Tables[0].Rows[0]["POS"]),
													  Convert.ToBoolean(ds.Tables[0].Rows[0]["Web"]),
													  Convert.ToString(ds.Tables[0].Rows[0]["WebImage"])
                                                    #endregion

);
                }
            }
            return promotion ;
        }

        public List<Promotion> SearchPromotions(string xmlDoc, string spName, ref string errorMessage)
        {
            List<Promotion> lstPromotion = new List<Promotion>();
            Promotion promotion = null;
            DBParameterList dbParam;
            using (DataTaskManager dtManager = new DataTaskManager())
            {
                dbParam = new DBParameterList();
                dbParam.Add(new DBParameter(Common.PARAM_DATA, xmlDoc, DbType.String));
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, errorMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                DataTable dt= dtManager.ExecuteDataTable(spName , dbParam);
                errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();

                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        promotion = new Promotion(
                                                        Convert.ToInt32(dt.Rows[i]["PromotionId"]),
                                                        Convert.ToString(dt.Rows[i]["PromotionName"]),
                                                        Convert.ToInt32(dt.Rows[i]["PromotionCategoryId"]),
                                                        Convert.ToString(dt.Rows[i]["PromotionCategory"]),
                                                        Convert.ToString(dt.Rows[i]["PromotionCode"]),
                                                        Convert.ToDateTime(dt.Rows[i]["StartDate"]),
                                                        Convert.ToDateTime(dt.Rows[i]["EndDate"]),//check for null
                                                        Convert.ToDateTime(dt.Rows[i]["DurationStart"]),//check for null
                                                        Convert.ToDateTime(dt.Rows[i]["DurationEnd"]),//check for null
                                                        Convert.ToInt32(dt.Rows[i]["DiscountTypeId"]),//check for null
                                                        //-1,
                                                        Validators.CheckForDBNull(dt.Rows[i]["DiscountType"], String.Empty),//check for null
                                                        //"Select",
                                                        Convert.ToDecimal(dt.Rows[i]["DiscountValue"]),//check for null
                                                        //0,
                                                        Convert.ToDecimal(dt.Rows[i]["MaxOrderQty"]),
                                                        Convert.ToInt32(dt.Rows[i]["StatusId"]),
                                                        Convert.ToString(dt.Rows[i]["Status"]),
                                                        Convert.ToInt32(dt.Rows[i]["CreatedBy"]),
                                                        Convert.ToInt32(dt.Rows[i]["ModifiedBy"]),
                                                        Convert.ToDateTime(dt.Rows[i]["CreatedDate"]),
                                                        Convert.ToDateTime(dt.Rows[i]["ModifiedDate"]),//check for null
                                                        Validators.CheckForDBNull(dt.Rows[i]["ApplicabilityId"], Common.INT_DBNULL),//check for null
                                                        Validators.CheckForDBNull(dt.Rows[i]["Applicability"], string.Empty),//check for null
                                                        Validators.CheckForDBNull(dt.Rows[i]["BuyConditionTypeId"], Common.INT_DBNULL),//check for null
                                                        Validators.CheckForDBNull(dt.Rows[i]["BuyConditionType"], string.Empty),
                                                        Validators.CheckForDBNull(dt.Rows[i]["GetConditionTypeId"], Common.INT_DBNULL),
                                                        Validators.CheckForDBNull(dt.Rows[i]["GetConditionType"], string.Empty),
                                                        Validators.CheckForDBNull(dt.Rows[i]["RepeatFactor"],Common.INT_DBNULL),
														new List<PromotionCondition>(), new List<PromotionLocation>(), new List<PromotionTier>(),
                                                        #region Shopping cart
                                                        Convert.ToBoolean(dt.Rows[i]["POS"]),         //Roshan
														Convert.ToBoolean(dt.Rows[i]["Web"]),		  //Roshan
														Convert.ToString(dt.Rows[i]["WebImage"]));    //Roshan
                                                        #endregion
                        lstPromotion.Add(promotion);   

                    }
                }
            }
            return lstPromotion;
        }

        public DataTable GetLocationCodes(int promoCategory, int conditionOn, DateTime startDate, DateTime endDate, ref string errorMessage)
        {
            DataTable dtLocationCodes = null;
            DBParameterList dbParam;
            using (DataTaskManager dtManager = new DataTaskManager())
            {
                dbParam = new DBParameterList();
                dbParam.Add(new DBParameter(Common.PARAM_DATA, promoCategory, DbType.Int32));
                dbParam.Add(new DBParameter(Common.PARAM_DATA2, conditionOn, DbType.Int32));
                dbParam.Add(new DBParameter(Common.PARAM_DATA3, startDate, DbType.DateTime));
                dbParam.Add(new DBParameter(Common.PARAM_DATA4, endDate, DbType.DateTime));
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, errorMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                DataTable dt = dtManager.ExecuteDataTable("usp_PromotionCondtionCodes", dbParam);
                errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();

                if (string.IsNullOrEmpty(errorMessage))
                {
                    if (dt.Rows.Count > 0)
                    {
                        dtLocationCodes = dt.Copy();
                    }
                }
            }

            return dtLocationCodes;
        }

        public decimal FetchMinimumDistributorPrice(int itemType, int id, ref string errorMessage)
        {
            decimal minDistributorPrice = Common.INT_DBNULL;
            DBParameterList dbParam;
            using (DataTaskManager dtManager = new DataTaskManager())
            {
                dbParam = new DBParameterList();
                dbParam.Add(new DBParameter(Common.PARAM_DATA, itemType, DbType.String));
                dbParam.Add(new DBParameter(Common.PARAM_DATA2, id, DbType.String));
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, errorMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                DataTable dt = dtManager.ExecuteDataTable("usp_getMinDistributorPrice", dbParam);
                errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();

                if (!string.IsNullOrEmpty(errorMessage))
                {
                    minDistributorPrice = Convert.ToDecimal(dt.Rows[0][0].ToString());
                }
            }

            return minDistributorPrice;
        }

        public bool ValidatePromotionAgainstExisiting(int promoCategory, int conditionOn, int conditionType, DateTime startDate, DateTime endDate, ref string errorMessage)
        {
            bool isValid = true;
            DBParameterList dbParam;
            using (DataTaskManager dtManager = new DataTaskManager())
            {
                dbParam = new DBParameterList();
                dbParam.Add(new DBParameter(Common.PARAM_DATA, promoCategory, DbType.Int32));
                dbParam.Add(new DBParameter(Common.PARAM_DATA2, conditionOn, DbType.Int32));
                dbParam.Add(new DBParameter(Common.PARAM_DATA3, conditionType, DbType.Int32));
                dbParam.Add(new DBParameter(Common.PARAM_DATA4, startDate, DbType.DateTime));
                dbParam.Add(new DBParameter(Common.PARAM_DATA5, endDate, DbType.DateTime));
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, errorMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                DataTable dt = dtManager.ExecuteDataTable("usp_PromotionValidator", dbParam);
                errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();

                if (string.IsNullOrEmpty(errorMessage))
                {
                    if (dt.Rows.Count > 0)
                    {
                        isValid = false;
                    }
                }
                else
                {
                    isValid = false;
                }
            }

            return isValid;
        }
        
        #endregion
    }
}
