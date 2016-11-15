using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreComponent.BusinessObjects;
using CoreComponent.Core.BusinessObjects;
using Vinculum.Framework.Data;
using Vinculum.Framework.DataTypes;

namespace PurchaseComponent.BusinessObjects
{
    public class GrnBatchTaxDetail
    {
        #region Property

        public string GRNNo { get; set; }
        public int RowNo { get; set; }
        public int ItemId { get; set; }
        public int SerialNo { get; set; }
        public string BatchNumber { get; set; }
        public string TaxCode { get; set; }
        public decimal TaxPercentage { get; set; }
        public string TaxGroup { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal DisplayTaxAmount 
        {
            get { return Math.Round(DBTaxAmount, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
        public decimal DBTaxAmount 
        {
            get { return Math.Round(TaxAmount, Common.DBAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
        public int GroupOrder { get; set; }
        public string ManufacturerBatchNumber { get; set; }
        public string ManufacturingDate { get; set; }
        public string ExpiryDate { get; set; }
        public double MRP { get; set; }
        public double DisplayMRP
        {
            get { return Math.Round(DBMRP, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
        public double DBMRP
        {
            get { return Math.Round(MRP, Common.DBAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }


        /// <summary>
        /// this is used to check exclusive tax or inclusive taxs.
        /// </summary>
        public bool IsExclusiveTax
        {
            get;
            set;
        }
        #endregion


    }
}
