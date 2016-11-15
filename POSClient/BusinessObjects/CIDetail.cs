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
    public class CIDetail
    {
        public CIDetail()
        {


        }

        public string GiftVoucherNumber { get; set; }

        public string InvoiceNo { get; set; }

        public int RecordNo { get; set; }

        public string ItemCode { get; set; }

        public int ItemId { get; set; }

        public string ItemName { get; set; }

        public string ItemShortName { get; set; }

        public string ItemPrintName { get; set; }

        public string ItemDisplayName { get; set; }

        public string ItemReceiptName { get; set; }

        public decimal TotalWeight { get; set; }

        public decimal DisplayTotalWeight
        {
            get { return Math.Round(DBTotalWeight, Common.DisplayQtyRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property can not be explicitly set"); }
        }

        public decimal DBTotalWeight
        {
            get { return Math.Round(TotalWeight, Common.DBQtyRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property can not be explicitly set"); }
        }

        public decimal Quantity { get; set; }

        public decimal DisplayQuantity
        {
            get { return Math.Round(DBQuantity, Common.DisplayQtyRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property can not be explicitly set"); }
        }

        public decimal DBQuantity
        {
            get { return Math.Round(Quantity, Common.DBQtyRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property can not be explicitly set"); }
        }

        public decimal DP { get; set; }

        public decimal DisplayDP
        {
            get { return Math.Round(DBDP, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property can not be explicitly set"); }
        }

        public decimal DBDP
        {
            get { return Math.Round(DP, Common.DBAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property can not be explicitly set"); }
        }

        public decimal MRP { get; set; }

        public decimal DisplayMRP
        {
            get { return Math.Round(DBMRP, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property can not be explicitly set"); }
        }

        public decimal DBMRP
        {
            get { return Math.Round(MRP, Common.DBAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property can not be explicitly set"); }
        }

        public int UOM { get; set; }

        public decimal Discount { get; set; }

        public decimal DisplayDiscount
        {
            get { return Math.Round(DBDiscount, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property can not be explicitly set"); }
        }

        public decimal DBDiscount
        {
            get { return Math.Round(Discount, Common.DBAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property can not be explicitly set"); }
        }

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

        public decimal LineAmount { get; set; }

        public decimal DisplayLineAmount
        {
            get { return Math.Round(DBLineAmount, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property can not be explicitly set"); }
        }

        public decimal DBLineAmount
        {
            get { return Math.Round(LineAmount, Common.DBAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property can not be explicitly set"); }
        }

        public decimal LinePV { get; set; }

        public decimal DisplayLinePV
        {
            get { return Math.Round(DBLinePV, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property can not be explicitly set"); }
        }

        public decimal DBLinePV
        {
            get { return Math.Round(LinePV, Common.DBAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property can not be explicitly set"); }
        }

        public decimal LineBV { get; set; }

        public decimal DisplayLineBV
        {
            get { return Math.Round(DBLineBV, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property can not be explicitly set"); }
        }

        public decimal DBLineBV
        {
            get { return Math.Round(LineBV, Common.DBAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property can not be explicitly set"); }
        }


        public bool IsKit { get; set; }

        public bool IsComposite { get; set; }

        public decimal Weight { get; set; }

        public decimal DisplayWeight
        {
            get { return Math.Round(DBWeight, Common.DisplayQtyRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property can not be explicitly set"); }
        }

        public decimal DBWeight
        {
            get { return Math.Round(Weight, Common.DBQtyRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property can not be explicitly set"); }
        }

        public decimal Length { get; set; }

        public decimal DisplayLength
        {
            get { return Math.Round(DBLength, Common.DisplayQtyRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property can not be explicitly set"); }
        }

        public decimal DBLength
        {
            get { return Math.Round(Length, Common.DBQtyRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property can not be explicitly set"); }
        }

        public decimal Height { get; set; }

        public decimal DisplayHeight
        {
            get { return Math.Round(DBHeight, Common.DisplayQtyRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property can not be explicitly set"); }
        }

        public decimal DBHeight
        {
            get { return Math.Round(Height, Common.DBQtyRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property can not be explicitly set"); }
        }

        public decimal Width { get; set; }

        public decimal DisplayWidth
        {
            get { return Math.Round(DBWidth, Common.DisplayQtyRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property can not be explicitly set"); }
        }

        public decimal DBWidth
        {
            get { return Math.Round(Width, Common.DBQtyRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property can not be explicitly set"); }
        }

        public bool IsPromo { get; set; }

        public decimal PrimaryCost { get; set; }

        public decimal DisplayPrimaryCost
        {
            get { return Math.Round(DBPrimaryCost, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property can not be explicitly set"); }
        }

        public decimal DBPrimaryCost
        {
            get { return Math.Round(PrimaryCost, Common.DBAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property can not be explicitly set"); }
        }
        public string VoucherSrNo { get; set; }


        public int TaxCategoryId { get; set; }

        public List<CIDetailDiscount> CIDiscountList { get; set; }

        public List<CIBatchDetail> CIBatchList { get; set; }

    }
}
