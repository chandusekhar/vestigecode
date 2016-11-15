using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PackUnpackComponent.BusinessObjects
{
  
    [Serializable]
    public class BatchDetails
    {
        public BatchDetails()
        {

        }
        public string BatchNo
        { 
            get;set;
        }
        public string MfgBatchNo
        {
            get;
            set;
        }
        public int ItemId
        { get; set; }
        public string ItemCode
        { get; set; }
        public string ItemName
        { get; set; }      
        public int CompositeItemId
        { get; set;}
        public int AvailableQty
        { get; set; }

        public double DBAvailableQty
        {
            get
            {
                return Math.Round((double)AvailableQty, CoreComponent.Core.BusinessObjects.Common.DBQtyRounding, MidpointRounding.AwayFromZero);
            }
            set
            {
                throw new NotImplementedException("This property can not be explicitly set");
            }
        }

        public double DisplayAvailableQty
        {
            get
            {
                return Math.Round((double)DBAvailableQty, CoreComponent.Core.BusinessObjects.Common.DisplayQtyRounding, MidpointRounding.AwayFromZero);
            }
            set
            {
                throw new NotImplementedException("This property can not be explicitly set");
            }
        }

        public int RequestedQty
        {
            get;
            set;
        }

        public double DBRequestedQty
        {
            get
            {
                return Math.Round((double)RequestedQty, CoreComponent.Core.BusinessObjects.Common.DBQtyRounding, MidpointRounding.AwayFromZero);
            }
            set
            {
                throw new NotImplementedException("This property can not be explicitly set");
            }
        }

        public double DisplayRequestedQty
        {
            get
            {
                return Math.Round((double)DBRequestedQty, CoreComponent.Core.BusinessObjects.Common.DisplayQtyRounding, MidpointRounding.AwayFromZero);
            }
            set
            {
                throw new NotImplementedException("This property can not be explicitly set");
            }
        }

        public string MfgDate
        { get; set; }

        public String DisplayMfgDate
        {
            get
            {
                if ((MfgDate != null) && (MfgDate.Length > 0))
                {
                    return Convert.ToDateTime(MfgDate).ToString(CoreComponent.Core.BusinessObjects.Common.DTP_DATE_FORMAT);
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

        public string ExpDate
        { get; set; }

        public String DisplayExpDate
        {
            get
            {
                if ((ExpDate != null) && (ExpDate.Length > 0))
                {
                    return Convert.ToDateTime(ExpDate).ToString(CoreComponent.Core.BusinessObjects.Common.DTP_DATE_FORMAT);
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

        public string MRP
        { get; set; }

        public double DBMRP
        {
            get
            {
                return Math.Round(Convert.ToDouble(MRP), CoreComponent.Core.BusinessObjects.Common.DBAmountRounding, MidpointRounding.AwayFromZero);
            }
            set
            {
                throw new NotImplementedException("This property can not be explicitly set");
            }
        }

        public double DisplayMRP
        {
            get
            {
                return Math.Round((double)DBMRP, CoreComponent.Core.BusinessObjects.Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
            }
            set
            {
                throw new NotImplementedException("This property can not be explicitly set");
            }
        }
    }

}
