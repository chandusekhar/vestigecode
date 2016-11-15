using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreComponent.Core.BusinessObjects;

namespace ReturnsComponent.BusinessObjects
{
    public class CustomerReturnItem
    {
        public int RowNo
        { get; set; }
        public string ReturnNo
        { get; set; }
        public int ItemId
        { get; set; }
        public string ItemCode
        { get; set; }
        public string ItemName
        { get; set; }

        public string BatchNo
        { get; set; }

        public decimal Quantity
        { get; set; }

        public decimal DisplayQuantity
        {
            get { return Math.Round(DBQuantity, Common.DisplayQtyRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
        public decimal DBQuantity
        {
            get { return Math.Round(Quantity, Common.DBQtyRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
        public string ManufacturingDate { get; set; }
        public string DisplayManufacturingDate
        {
            get
            {
                return Convert.ToDateTime(ManufacturingDate).ToString(Common.DTP_DATE_FORMAT);
            }
            set
            {
                ManufacturingDate = value;
            }
        }

        public string ExpiryDate { get; set; }
        public string DisplayExpiryDate
        {
            get
            {
                return Convert.ToDateTime(ExpiryDate).ToString(Common.DTP_DATE_FORMAT);
            }
            set
            {
                ExpiryDate = value;
            }
        }
        public decimal DistributorPrice
        { get; set; }

        public decimal DisplayDistributorPrice
        {
            get { return Math.Round(DBDistributorPrice, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
        public decimal DBDistributorPrice
        {
            get { return Math.Round(DistributorPrice, Common.DBAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }

        public decimal MRP
        { get; set; }

        public decimal DisplayMRP
        {
            get { return Math.Round(DBMRP, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
        public decimal DBMRP
        {
            get { return Math.Round(MRP, Common.DBAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }

        public decimal TotalAmount
        { get; set; }

        public string ManufactureBatchNo
        { get; set; }

        public int BucketId
        { get; set; }
        public string BucketName
        { get; set; }

        //Added by Kaushik 
        public string DistributorId
        { get; set; }

        public string InvoiceDate
        { get; set; }

        public decimal TaxAmount
        { get; set; }

        public decimal InvoiceAmount
        { get; set; }

        public string InvoiceNo
        { get; set; }
    }
}
