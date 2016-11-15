using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreComponent.Core.BusinessObjects;

namespace InventoryComponent.BusinessObjects
{
    [Serializable]
    public class ItemStockCount : ItemDetail
    {
        string m_bucketName;
        int m_bucketId;
        decimal m_systemQty;


        public decimal TotalPhysicalQuantity
        {
            get;
            set;
        }
        public decimal DisplayTotalPhysicalQuantity
        {
            get { return Math.Round(DBTotalPhysicalQuantity, Common.DisplayQtyRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
        public decimal DBTotalPhysicalQuantity
        {
            get { return Math.Round(TotalPhysicalQuantity, Common.DBQtyRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
        public int BucketId
        {
            get { return m_bucketId; }
            set { m_bucketId = value; }
        }

        public string BucketName
        {
            get { return m_bucketName; }
            set { m_bucketName = value; }
        }
        public decimal SystemQty
        {
            get { return m_systemQty; }
            set { m_systemQty = value; }
        }
        public decimal DisplaySystemQty
        {
            get { return Math.Round(DBSystemQty, Common.DisplayQtyRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
        public decimal DBSystemQty
        {
            get { return Math.Round(SystemQty, Common.DBQtyRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
    }
}
