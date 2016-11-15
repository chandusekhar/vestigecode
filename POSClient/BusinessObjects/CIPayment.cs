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
    public class CIPayment
    {
        #region Property

        public string InvoiceNo { get; set; }
        public string RecordNo { get; set; }
        public Int32 TenderType { get; set; }
      
        public decimal PaymentAmount { get; set; }
        public decimal DisplayPaymentAmount
        {
            get { return Math.Round(DBPaymentAmount, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property can not be explicitly set"); }
        }
        public decimal DBPaymentAmount
        {
            get { return Math.Round(PaymentAmount, Common.DBAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property can not be explicitly set"); }
        }        
      
        public DateTime Date { get; set; }
        public string Remark { get; set; }
        public string BankName { get; set; }
        public DateTime ChqIssueDate { get; set; }
        public DateTime ChqExpiryDate { get; set; }

        public decimal ForexAmount { get; set; }
        public decimal DisplayForexAmount
        {
            get { return Math.Round(DBForexAmount, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property can not be explicitly set"); }
        }
        public decimal DBForexAmount
        {
            get { return Math.Round(ForexAmount, Common.DBAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property can not be explicitly set"); }
        }
       
        public string Reference { get; set; }
        public string CreditCardNumber { get; set; }
        public string CardHolderName { get; set; }
        public string CardExpiryDate { get; set; }

        public string CurrencyCode { get; set; }
        public decimal ExchangeRate { get; set; }
        public string CardType { get; set; }

        public string RecieptDisplay { get; set; }

        #endregion
    }
}
