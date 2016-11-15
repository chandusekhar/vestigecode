using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CoreComponent.Core.BusinessObjects;

namespace POSClient.BusinessObjects
{
    [Serializable]
    public class CODetailDiscount
    {
        #region Property

        public string CustomerOrderNo { get; set; }
        public Int32 RecordNo { get; set; }
        public int PromotionId { get; set; }
        public string Description { get; set; }
        public decimal DiscountPercent { get; set; }

        public decimal RoundedDiscountPercent
        {
            get { return Math.Round(DBRoundedDiscountPercent, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }

        public decimal DBRoundedDiscountPercent
        {
            get { return Math.Round(DiscountPercent, Common.DBAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }

        public decimal DiscountAmount { get; set; }

        public decimal RoundedDiscountAmount
        {
            get { return Math.Round(DBRoundedDiscountAmount, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }

        public decimal DBRoundedDiscountAmount
        {
            get { return Math.Round(DiscountAmount, Common.DBAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
            
        #endregion

        public void GetDiscountObject(DataRow dr)
        {
            try
            {
                this.CustomerOrderNo = Convert.ToString(dr["CustomerOrderNo"]);
                this.Description = Convert.ToString(dr["Description"]);
                this.DiscountAmount = Convert.ToDecimal(dr["DiscountAmount"]);
                this.DiscountPercent = Convert.ToDecimal(dr["DiscountPercent"]);
                this.PromotionId = Convert.ToInt32(dr["PromotionId"]);
                this.RecordNo = Convert.ToInt32(dr["RecordNo"]);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       
    }
}
