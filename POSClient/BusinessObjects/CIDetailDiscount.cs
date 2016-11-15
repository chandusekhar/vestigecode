using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreComponent.Core.BusinessObjects;
using Vinculum.Framework.Data;
using Vinculum.Framework.DataTypes;
namespace POSClient.BusinessObjects
{
    [Serializable]
    public class CIDetailDiscount
    {
        #region Property

        public string InvoiceNo { get; set; }
        public Int32 RecordNo { get; set; }
        public string PromotionId { get; set; }
        public string Description { get; set; }
        public decimal DiscountPercent { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal DisplayDiscountAmount
        {
            get { return Math.Round(DBDiscountAmount, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property can not be explicitly set"); }
        }
        public decimal DBDiscountAmount
        {
            get { return Math.Round(DiscountAmount, Common.DBAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property can not be explicitly set"); }
        }  
        #endregion
    }

}
