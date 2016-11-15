using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReturnsComponent.BusinessObjects
{
    [Serializable]
    public class RetVendorHeader:Return
    {
        public RetVendorHeader()
        { }
        public int LocationId
        {
            get;
            set;
        }
        public int VendorId
        {
            get;
            set;
        }
        //public string ReturnNo
        //{ get; set; }
        public string ShippingDetails
        { get; set; }
        public string Remarks
        { get; set; }
        public int StatusId
        { get; set; }
        public string DebitNoteNumber
        { get; set; }
        public double TotalAmount
        { get; set; }

        public double DBTotalAmount
        {
            get
            {
                return Math.Round(TotalAmount, CoreComponent.Core.BusinessObjects.Common.DBAmountRounding, MidpointRounding.AwayFromZero);
            }
            set
            {
                throw new NotImplementedException("This property can not be explicitly set");
            }
        }

        public double DisplayTotalAmount
        {
            get
            {
                return Math.Round(DBTotalAmount, CoreComponent.Core.BusinessObjects.Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
            }
            set
            {
                throw new NotImplementedException("This property can not be explicitly set");
            }
        }

        public double DebitNoteAmount
        { get; set; }

        public double DBDebitNoteAmount
        {
            get
            {
                return Math.Round(DebitNoteAmount, CoreComponent.Core.BusinessObjects.Common.DBAmountRounding, MidpointRounding.AwayFromZero);
            }
            set
            {
                throw new NotImplementedException("This property can not be explicitly set");
            }
        }

        public double DisplayDebitNoteAmount
        {
            get
            {
                return Math.Round(DBDebitNoteAmount, CoreComponent.Core.BusinessObjects.Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
            }
            set
            {
                throw new NotImplementedException("This property can not be explicitly set");
            }
        }

        public int Quantity
        { get; set; }

        public double DBQuantity
        {
            get
            {
                return Math.Round((double)Quantity, CoreComponent.Core.BusinessObjects.Common.DBQtyRounding, MidpointRounding.AwayFromZero);
            }
            set
            {
                throw new NotImplementedException("This property can not be explicitly set");
            }
        }

        public double DisplayQuantity
        {
            get
            {
                return Math.Round((double)DBQuantity, CoreComponent.Core.BusinessObjects.Common.DisplayQtyRounding, MidpointRounding.AwayFromZero);
            }
            set
            {
                throw new NotImplementedException("This property can not be explicitly set");
            }
        }

        public string RetVendorDate
        { get; set; }

        public String DisplayRetVendorDate
        {
            get
            {
                if ((RetVendorDate != null) && (RetVendorDate.Length > 0))
                {
                    return Convert.ToDateTime(RetVendorDate).ToString(CoreComponent.Core.BusinessObjects.Common.DTP_DATE_FORMAT);
                }
                else
                {
                    return String.Empty;
                }
            }
            set
            {
                throw new NotImplementedException("This property can not be explicitly set");
            }
        }

        //public string DisplayRetVendorDate
        //{ get; set; }

        public string VendorName
        { get; set; }
        public string StatusName
        { get; set; }
        //public int ModifiedBy
        //{ get; set; }
        public string ShipmentDate
        { get; set; }

        public String DisplayShipmentDate
        {
            get
            {
                if ((ShipmentDate != null) && (ShipmentDate.Length > 0))
                {
                    return Convert.ToDateTime(ShipmentDate).ToString(CoreComponent.Core.BusinessObjects.Common.DTP_DATE_FORMAT);
                }
                else
                {
                    return String.Empty;
                }
            }
            set
            {
                throw new NotImplementedException("This property can not be explicitly set");
            }
        }

        public string DebitNoteText
        { get; set; }

        public int CurrentLocationType
        { get; set; }

        public int CurrentLocationId
        { get; set; }

        public List<RetVendorDetails> ListRetVendorDetails
        { get; set; }
    }
}
