using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreComponent.Core.BusinessObjects;
using CoreComponent.BusinessObjects;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using CoreComponent.Controls;
using CoreComponent.Hierarchies.BusinessObjects;
using CoreComponent.MasterData.BusinessObjects;
using System.Collections.Specialized;



namespace InventoryComponent.BusinessObjects
{
    [Serializable]
    public class ItemInventory:ItemDetail
    {
        string m_fromBucketName, m_toBucketName, m_reasonCode, m_reasonDescription, m_reasonCodeDescription, m_expDate, m_mfgDate,mfg,exp;
        int m_fromBucketId, m_toBucketId, m_export,m_InterBatchAdj;
        decimal m_quantity, m_approvedQty;

        private static List<ItemBatchDetails> m_BatchDetailList = null;
        public static List<ItemBatchDetails> BatchDetailList
        {
            get { return m_BatchDetailList; }
            set { m_BatchDetailList = value; }
        }
        public decimal BalanceQty { get; set; }
        public string MfgDate
        {
            get { return m_mfgDate; }
            set { m_mfgDate = value; }
        }

        public string DisplayMfgDate
        {
            get
            {
                return Convert.ToDateTime(MfgDate).ToString(Common.DTP_DATE_FORMAT);
            }
            set
            {
                MfgDate = value;
            }
        }

        public string ExpDate
        {
            get { return m_expDate; }
            set { m_expDate = value; }
        }

        public string DisplayExpDate
        {
            get
            {
                return Convert.ToDateTime(ExpDate).ToString(Common.DTP_DATE_FORMAT);
            }
            set
            {
                ExpDate = value;
            }
        }

        public int Export
        {
            get { return m_export; }
            set { m_export = value; }
        }
        public string ReasonCodeDescription
        {
            get { return m_reasonCodeDescription; }
            set { m_reasonCodeDescription = value; }
        }
        public decimal ApprovedQty
        {
            get { return m_approvedQty; }
            set { m_approvedQty = value; }
        }
        public decimal DisplayApprovedQty
        {
            get { return Math.Round(DBApprovedQty, Common.DisplayQtyRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
        public decimal DBApprovedQty
        {
            get { return Math.Round(ApprovedQty, Common.DBQtyRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
        public string ReasonDescription
        {
            get { return m_reasonDescription; }
            set { m_reasonDescription = value; }
        }

        public string ReasonCode
        {
            get { return m_reasonCode; }
            set { m_reasonCode = value; }
        }
        public int InterBatchAdj
        {
            get { return m_InterBatchAdj; }
            set { m_InterBatchAdj = value; }
        }
        
        public int FromBucketId
        {
            get { return m_fromBucketId; }
            set { m_fromBucketId = value; }
        }

        public string FromBucketName
        {
            get { return m_fromBucketName; }
            set { m_fromBucketName = value; }
        }

        public int ToBucketId
        {
            get { return m_toBucketId; }
            set { m_toBucketId = value; }
        }

        public string ToBucketName
        {
            get { return m_toBucketName; }
            set { m_toBucketName = value; }
        }

        public decimal Quantity
        {
            get { return m_quantity; }
            set { m_quantity = value; }
        }
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

        public string Mfg
        {
            get { return mfg; }
            set { mfg = value; }
        }
        public string Exp
        {
            get { return exp; }
            set { exp = value; }
        }
    }
}
