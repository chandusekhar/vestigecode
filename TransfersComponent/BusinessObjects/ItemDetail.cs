using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CoreComponent.Core.BusinessObjects;
using CoreComponent.MasterData.BusinessObjects;

namespace TransfersComponent.BusinessObjects
{
    public class ItemDetail : IItemHeader
    {
        Int32 m_itemId = Common.INT_DBNULL;
        Int32 m_UOMId,m_rowNo;
        String m_itemCode = String.Empty;
        String m_itemName = string.Empty;
        Int32 m_createdBy = Common.INT_DBNULL;
        string m_bucketName;
        int m_bucketId;
        Int32 m_modifiedBy = Common.INT_DBNULL;
        string m_modifiedDate, m_UOMName;//, m_indentNo;
        decimal m_itemUnitPrice,m_availableQty,  m_itemTotalAmount, m_unitQty;
        Int32 m_indexSeqNo = Common.INT_DBNULL;

        public decimal AvailableQty
        {
            get { return m_availableQty; }
            set { m_availableQty = value; }
        }

        public decimal DisplayAvailableQty
        {
            get { return Math.Round(DBAvailableQty, Common.DisplayQtyRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
        public decimal DBAvailableQty
        {
            get { return Math.Round(AvailableQty, Common.DBQtyRounding, MidpointRounding.AwayFromZero); }
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

        public System.Int32 IndexSeqNo
        {
            get { return m_indexSeqNo; }
            set { m_indexSeqNo = value; }
        }

        public String ModifiedDate
        {
            get { return m_modifiedDate; }
            set { m_modifiedDate = value; }
        }

        public Int32 ModifiedBy
        {
            get { return m_modifiedBy; }
            set { m_modifiedBy = value; }
        }

        public Int32 RowNo
        {
            get { return m_rowNo; }
            set { m_rowNo = value; }
        }

        public decimal ItemUnitPrice
        {
            get { return m_itemUnitPrice; }
            set { m_itemUnitPrice = value; }
        }
        public decimal DisplayItemUnitPrice
        {
            get { return Math.Round(DBItemUnitPrice, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
        public decimal DBItemUnitPrice
        {
            get { return Math.Round(ItemUnitPrice, Common.DBAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }

        public decimal ItemTotalAmount
        {
            get { return m_itemTotalAmount; }
            set { m_itemTotalAmount = value; }
        }
        public decimal DisplayItemTotalAmount
        {
            get { return Math.Round(DBItemTotalAmount, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
        public decimal DBItemTotalAmount
        {
            get { return Math.Round(ItemTotalAmount, Common.DBAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
        public decimal UnitQty
        {
            get { return m_unitQty; }
            set { m_unitQty = value; }
        }
        public decimal DisplayUnitQty
        {
            get { return Math.Round(DBUnitQty, Common.DisplayQtyRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
        public decimal DBUnitQty
        {
            get { return Math.Round(UnitQty, Common.DBQtyRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }

        public Int32 UOMId
        {
            get { return m_UOMId; }
            set { m_UOMId = value; }
        }

        //public String IndentNo
        //{
        //    get { return m_indentNo; }
        //    set { m_indentNo = value; }
        //}

        public String UOMName
        {
            get { return m_UOMName; }
            set { m_UOMName = value; }
        }
        public Int32 ItemId
        {
            get { return m_itemId; }
            set { m_itemId = value; }
        }

        public String ItemCode
        {
            get { return m_itemCode; }
            set { m_itemCode = value; }
        }

        public String ItemName
        {
            get { return m_itemName; }
            set { m_itemName = value; }
        }

    }
}
