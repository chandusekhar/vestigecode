using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CoreComponent.Core.BusinessObjects;

namespace POSClient.BusinessObjects
{
    [Serializable]
    public class COPayment
    {
        #region Property

        public string CustomerOrderNo { get; set; }
        public Int32 RecordNo { get; set; }
        public Int32 TenderType { get; set; }
        
        public decimal PaymentAmount { get; set; }

        public decimal RoundedPaymentAmount
        {
            get { return Math.Round(DBRoundedPaymentAmount, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }

        public decimal DBRoundedPaymentAmount
        {
            get { return Math.Round(PaymentAmount, Common.DBAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }

        public string Date { get; set; }
        public string BankName { get; set; }
        public string ChequeNo { get; set; }
        public string ChqIssueDate { get; set; }
        public string ChqExpiryDate { get; set; }

        public string Remark { get; set; }
        public decimal ForexAmount { get; set; }

        public decimal RoundedForexAmount
        {
            get { return Math.Round(DBRoundedForexAmount, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }

        public decimal DBRoundedForexAmount
        {
            get { return Math.Round(ForexAmount, Common.DBAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
        
        public string Reference { get; set; }
        public string CreditCardNumber { get; set; }
        public string CardHolderName { get; set; }
        public string CardExpiryDate { get; set; }
        //public string Reference { get; set; }
        public string CurrencyCode { get; set; }
        public decimal ExchangeRate { get; set; }

        public decimal RoundedExchangeRate
        {
            get { return Math.Round(DBRoundedExchangeRate, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }

        public decimal DBRoundedExchangeRate
        {
            get { return Math.Round(ExchangeRate, Common.DBAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
        
        public int CardType { get; set; }
        
        public string ReceiptDisplay { get; set; }
        public string ItemReceiptDisplay
        {
            get
            {
                string paymentDisplay = string.Empty;
                paymentDisplay = ReceiptDisplay;
                ////////////////////////

                //if (RoundedForexAmount != 0)
                //{
                //    paymentDisplay = "Forex - " + CurrencyCode + " " + RoundedForexAmount + "\n@" + RoundedExchangeRate;
                //}
                //else if (!string.IsNullOrEmpty(CreditCardNumber))
                //{
                //    paymentDisplay = "Card - " + Enum.Parse(typeof(Common.CreditCardType), CardType.ToString()).ToString();
                //}
                //else if (CurrencyCode == Currency.BaseCurrency.CurrencyCode)
                //{
                //    paymentDisplay = CurrencyCode;
                //}
                //else
                //{
                //    paymentDisplay = TenderType == 0 ? ReceiptDisplay : Enum.Parse(typeof(Common.PaymentMode), TenderType.ToString()).ToString();
                //}
                
                
                

                return paymentDisplay;
            }
            set { throw new InvalidOperationException("This Property cannot be explicitly set"); }
        }

        public int PaymentModeId
        {
            get;
            set;
        }

        #endregion

        public void GetCOPaymentObject(DataRow dr)
        {
            this.BankName = Convert.ToString(dr["BankName"]);
            this.CardExpiryDate = Convert.ToString(dr["CardExpiryDate"]);
            this.CardHolderName = Convert.ToString(dr["CardHolderName"]);
            this.CardType = Convert.ToInt32(dr["CardType"]);
            this.ChqExpiryDate = Convert.ToString(dr["ChqExpiryDate"]);
            this.ChqIssueDate = Convert.ToString(dr["ChqIssueDate"]);
            this.CreditCardNumber = Convert.ToString(dr["CreditCardNumber"]);
            this.CurrencyCode = Convert.ToString(dr["CurrencyCode"]);
            this.CustomerOrderNo = Convert.ToString(dr["CustomerOrderNo"]);
            this.Date = Convert.ToString(dr["Date"]);
            this.ExchangeRate = Convert.ToDecimal(dr["ExchangeRate"]);
            this.ForexAmount = Convert.ToDecimal(dr["ForexAmount"]);
            this.PaymentAmount = Convert.ToDecimal(dr["PaymentAmount"]);
            this.ReceiptDisplay = Convert.ToString(dr["ReceiptDisplay"]);
            this.RecordNo = Convert.ToInt32(dr["RecordNo"]);
            this.Reference = Convert.ToString(dr["Reference"]);
            this.Remark = Convert.ToString(dr["Remark"]);
            this.TenderType = Convert.ToInt32(dr["TenderType"]);
        }
    }
}
