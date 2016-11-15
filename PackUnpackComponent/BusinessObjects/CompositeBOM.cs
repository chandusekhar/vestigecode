using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PackUnpackComponent.BusinessObjects
{
    [Serializable]
  public  class CompositeBOM
    {
        public int ItemId
        {
            get;
            set;
        }
        public string ItemName
        { get; set; }
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

        public int CompositeItemId
        {get; set;}
        public string ItemCode
        {get; set;}
        public string ShortName
        {get; set;}
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

        private int m_TotalQty = 0;
        public int TotalQty
        { get { return m_TotalQty; } set { m_TotalQty = value; } }

        public double DBTotalQty
        {
            get
            {
                return Math.Round((double)TotalQty, CoreComponent.Core.BusinessObjects.Common.DBQtyRounding, MidpointRounding.AwayFromZero);
            }
            set
            {
                throw new NotImplementedException("This property can not be explicitly set");
            }
        }

        public double DisplayTotalQty
        {
            get
            {
                return Math.Round((double)DBTotalQty, CoreComponent.Core.BusinessObjects.Common.DisplayQtyRounding, MidpointRounding.AwayFromZero);
            }
            set
            {
                throw new NotImplementedException("This property can not be explicitly set");
            }
        }

        public List<BatchDetails> ListSelectedBatchDetails
        { get; set; }
        public List<BatchDetails> ListAllBatchDetails
        { get; set; }
      
        
    }
}
