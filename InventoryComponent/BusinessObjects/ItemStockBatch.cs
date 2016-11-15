using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreComponent.Core.BusinessObjects;

namespace InventoryComponent.BusinessObjects
{
    [Serializable]
    public class ItemStockBatch
    {
        public int ItemRowNo
        {
            get;
            set;
        }

        public int RowNo
        {
            get;
            set;
        }
  
        public string BatchNo
        {
            get;
            set;
        }
        public decimal PhysicalQty
        {
            get;
            set;
        }
        public decimal DisplayPhysicalQty
        {
            get { return Math.Round(DBPhysicalQty, Common.DisplayQtyRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
        public decimal DBPhysicalQty
        {
            get { return Math.Round(PhysicalQty, Common.DBQtyRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
        public string ManufactureBatchNo
        {
            get;
            set;
        }
    }
}
