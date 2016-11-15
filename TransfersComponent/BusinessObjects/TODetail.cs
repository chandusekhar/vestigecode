using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreComponent.Core.BusinessObjects;

namespace TransfersComponent.BusinessObjects
{
    public class TODetail:ItemDetail
    {
        #region Variable Declaration
        decimal m_afterAdjustQty, m_requestQty, m_MRP, m_weight, m_GrossWeightItem;
        string m_batchNo, m_manufactureBatchNo, m_mfgDate, m_expDate;
        //Bikram: ExportInvoice
        string  m_ContainerNOFromTo;
        int m_EachCartonQty;


        public decimal GrossWeightItem
        {
            get { return m_GrossWeightItem; }
            set { m_GrossWeightItem = value; }
        }

        public string ContainerNOFromTo
        {
            get { return m_ContainerNOFromTo; }
            set { m_ContainerNOFromTo = value; }
        }

        public int EachCartonQty
        {
            get { return m_EachCartonQty; }
            set { m_EachCartonQty = value; }
        }

        //public string TaxCategoryName
        //{ get; set; }
        //public string  TaxPercent
        //{ get; set; }

        #endregion SP Declaration

        #region Property
        public string ManufactureBatchNo
        {
            get { return m_manufactureBatchNo; }
            set { m_manufactureBatchNo = value; }
        }
        public string BatchNo
        {
            get { return m_batchNo; }
            set { m_batchNo = value; }
        }
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
        public decimal Weight
        {
            get { return m_weight; }
            set { m_weight = value; }
        }
        public decimal AfterAdjustQty
        {
            get { return m_afterAdjustQty; }
            set { m_afterAdjustQty = value; }
        }
        public decimal DisplayAfterAdjustQty
        {
            get { return Math.Round(DBAfterAdjustQty, Common.DisplayQtyRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
        public decimal DBAfterAdjustQty
        {
            get { return Math.Round(AfterAdjustQty, Common.DBQtyRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
        public decimal MRP
        {
            get { return m_MRP; }
            set { m_MRP= value; }
        }
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
        public decimal RequestQty
        {
            get { return m_requestQty; }
            set { m_requestQty = value; }
        }
        public decimal DisplayRequestQty
        {
            get { return Math.Round(DBRequestQty, Common.DisplayQtyRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
        public decimal DBRequestQty
        {
            get { return Math.Round(RequestQty, Common.DBQtyRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
        #endregion 
    }
}
