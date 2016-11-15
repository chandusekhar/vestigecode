using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CoreComponent.Core.BusinessObjects;
using CoreComponent.MasterData.BusinessObjects;

namespace InventoryComponent.BusinessObjects
{
    [Serializable]
    public class ItemDetail : IItemHeader
    {
        Int32 m_itemId = Common.INT_DBNULL;
        Int32 m_UOMId,m_rowNo;
        String m_itemCode = String.Empty;
        String m_itemName = string.Empty;
        Int32 m_createdBy = Common.INT_DBNULL;
        Int32 m_modifiedBy = Common.INT_DBNULL;
        //string m_createdDate;
        string m_modifiedDate;
        string m_UOMName;
        //string m_indentNo;
        String m_ToitemCode = String.Empty;
        String m_ToitemName = string.Empty;
        int m_ToitemId = -1;

       
        public String ManufactureBatchNo
        {
            get;
            set;
        }

        public String ToManufactureBatchNo
        {
            get;
            set;
        }

        public String BatchNo
        {
            get;
            set;
        }

        public String ToBatchNo
        {
            get;
            set;
        }

        public decimal Weight
        {
            get;
            set;
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
     
        public Int32 UOMId
        {
            get { return m_UOMId; }
            set { m_UOMId = value; }
        }

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

        public Int32 ToItemId
        {
            get { return m_ToitemId; }
            set { m_ToitemId = value; }
        }

        public String ToItemCode
        {
            get { return m_ToitemCode; }
            set { m_ToitemCode = value; }
        }

        public String ItemCode
        {
            get { return m_itemCode; }
            set { m_itemCode = value; }
        }

        public String ToItemName
        {
            get { return m_ToitemName; }
            set { m_ToitemName = value; }
        }

        public String ItemName
        {
            get { return m_itemName; }
            set { m_itemName = value; }
        }

    }
}
