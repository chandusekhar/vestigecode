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
    public class CIBatchTax
    {
        #region Property

        public string InvoiceNo { get; set; }
        public Int32 RecordNo { get; set; }
        public string TaxRecordNo { get; set; }
        public string ItemCode { get; set; }
        public string TaxCode { get; set; }
        public decimal TaxPercent { get; set; }
        public string TaxGroupCode { get; set; }
        public decimal TaxAmount { get; set; }

        public decimal DisplayTaxAmount
        {
            get { return Math.Round(DBTaxAmount, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property can not be explicitly set"); }
        }

        public decimal DBTaxAmount
        {
            get { return Math.Round(TaxAmount, Common.DBAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property can not be explicitly set"); }
        }

        #endregion
    }
}
