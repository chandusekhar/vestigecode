using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PromotionsComponent.BusinessLayer
{
    [Serializable]
    public class Price
    {
        public Price(int promotionId, int itemId, string itemCode, string itemName, string shortName, string printName, string receiptName, string displayName, decimal mrp, decimal distributorPrice, decimal discountPercent, decimal discountValue, decimal discountedPrice, decimal businessVolume, decimal pointValue, decimal quantity)
        {
            this.PromotionId = promotionId;
            this.ItemId = itemId;
            this.ItemCode = itemCode;
            this.ItemName = itemName;
            this.ShortName = shortName;
            this.PrintName = printName;
            this.ReceiptName = receiptName;
            this.DisplayName = displayName;
            this.MRP = mrp;
            this.DistributorPrice = distributorPrice;
            this.DiscountPercent = discountPercent;
            this.DiscountValue = discountValue;
            this.DiscountedPrice = discountedPrice;
            this.BusinessVolume = businessVolume;
            this.PointValue = pointValue;
            this.Quantity = quantity;
            this.GiftVoucherNumber = string.Empty;
            this.VoucherSrNo = string.Empty;
        }


        public int PromotionId
        { get; set; }

        public int ItemId
        { get; set; }

        public string ItemCode
        { get; set; }

        public string ItemName
        { get; set; }

        public string ShortName
        { get; set; }

        public string PrintName
        { get; set; }

        public string ReceiptName
        { get; set; }

        public string DisplayName
        { get; set; }

        public decimal MRP
        { get; set; }

        public decimal DistributorPrice
        { get; set; }

        public decimal DiscountPercent
        { get; set; }

        public decimal DiscountValue
        { get; set; }

        public decimal DiscountedPrice
        { get; set; }

        public decimal BusinessVolume
        { get; set; }

        public decimal PointValue
        { get; set; }

        public decimal Quantity
        { get; set; }

        public string GiftVoucherNumber
        {
            get;
            set;
        }
        public string VoucherSrNo
        {
            get;
            set;
        }
        
    }
}
