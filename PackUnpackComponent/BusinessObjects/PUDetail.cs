using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PackUnpackComponent.BusinessObjects
{
[Serializable]
  public  class PUDetail
    {

        public String PUNo
            {
                get;
                set;
            }
        public int ItemId
            {
                get;
                set;
            }
        public int SeqNo
            {
                get;
                set;
            }
        public string ItemName
        {get; set;}
        public string CreatedBy
        { get; set; }
        public int ModifiedBy
        { get; set; }
        public string ModifiedDate
        { get; set; }
        public string ItemCode
        { get; set; }
        public int Quantity
        {
            get;
            set;
        }

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

    }
}
