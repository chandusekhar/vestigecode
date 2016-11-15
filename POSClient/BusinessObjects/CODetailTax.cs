using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CoreComponent.Core.BusinessObjects;

namespace POSClient.BusinessObjects
{
    [Serializable]
    public class CODetailTax
    {
        #region Property

        public string CustomerOrderNo { get; set; }
        public Int32 RecordNo { get; set; }
        public int TaxRecordNo { get; set; }
        public int ItemID { get; set; }
        public string ItemCode { get; set; }
        public string TaxCode { get; set; }
        public decimal TaxPercent { get; set; }

        public decimal RoundedTaxPercent
        {
            get { return Math.Round(DBRoundedTaxPercent, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }

        public decimal DBRoundedTaxPercent
        {
            get { return Math.Round(TaxPercent, Common.DBAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }

        public string TaxGroupCode { get; set; }
        public decimal TaxAmount { get; set; }

        public decimal RoundedTaxAmount
        {
            get { return Math.Round(DBRoundedTaxAmount, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }

        public decimal DBRoundedTaxAmount
        {
            get { return Math.Round(TaxAmount, Common.DBAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }

        #endregion
        public void GetCODetailTaxObject(DataRow dr)
        {
            try
            {
                this.CustomerOrderNo = Convert.ToString(dr["CustomerOrderNo"]);
                this.ItemCode = Convert.ToString(dr["ItemCode"]);
                this.ItemID = Convert.ToInt32(dr["ItemID"]);
                this.RecordNo = Convert.ToInt32(dr["RecordNo"]);
                this.TaxAmount = Convert.ToDecimal(dr["TaxAmount"]);
                this.TaxCode = Convert.ToString(dr["TaxCode"]);
                this.TaxGroupCode = Convert.ToString(dr["TaxGroupCode"]);
                this.TaxPercent = Convert.ToDecimal(dr["TaxPercent"]);
                this.TaxRecordNo = Convert.ToInt32(dr["TaxRecordNo"]);
                
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
    }
}
