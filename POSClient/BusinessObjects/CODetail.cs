using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreComponent.Core.BusinessObjects;
using Vinculum.Framework.Data;
using Vinculum.Framework.DataTypes;
using System.Data;
namespace POSClient.BusinessObjects
{
    [Serializable]
    public class CODetail
    {
        #region SP_Declaration

        private const string SP_CODETAIL_SEARCH = "usp_CODetailSearch";
        #endregion  
        
        public CODetail()
        {
            this.CODiscountList = new List<CODetailDiscount>();
            this.CODetailTaxList = new List<CODetailTax>();
        }
        
        public string CustomerOrderNo { get; set; }
        
        public int RecordNo { get; set;}

        public string ItemCode { get; set; }

        public int ItemId { get; set; }
        
        public string ItemName{ get; set; }
        
        public string ItemShortName { get; set; }
        
        public string ItemPrintName { get; set; }

        public string ItemDisplayName { get; set; }

        public string ItemReceiptName { get; set; }
        
        public int UOM { get; set; }
        
        public decimal PV { get; set; }

        public decimal RoundedPV
        {
            get { return Math.Round(DBRoundedPV, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }

        public decimal DBRoundedPV
        {
            get { return Math.Round(PV, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }

        public decimal BV { get; set; }

        public decimal RoundedBV
        {
            get { return Math.Round(DBRoundedBV, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }

        public decimal DBRoundedBV
        {
            get { return Math.Round(BV, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }


        public decimal RoundedDP
        {
            get { return Math.Round(DBRoundedDP, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }

        public decimal DBRoundedDP
        {
            get { return Math.Round(DP, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }

        public decimal DP { get; set; }
        
        public decimal MRP { get; set; }


        public decimal RoundedMRP
        {
            get { return Math.Round(DBRoundedMRP, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }

        public decimal DBRoundedMRP
        {
            get { return Math.Round(MRP, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }

        public decimal Qty { get; set; }


        public decimal RoundedQty
        {
            get { return Math.Round(DBRoundedQty, Common.DisplayQtyRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }

        public decimal DBRoundedQty
        {
            get { return Math.Round(Qty, Common.DisplayQtyRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }

        public decimal TotalWeight { get; set; }


        public decimal RoundedTotalWeight
        {
            get { return Math.Round(DBRoundedTotalWeight, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }

        public decimal DBRoundedTotalWeight
        {
            get { return Math.Round(TotalWeight, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }

        public decimal Amount
        {
            get;
            set;
            //get 
            //{
            //    return (DP - Discount) * Qty; 
            //}
            //set { throw new NotImplementedException("This property cannot be explicitly set"); }
        }

        public decimal RoundedAmount
        {
            get { return Math.Round(DBRoundedAmount, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }

        public decimal DBRoundedAmount
        {
            get { return Math.Round(Amount, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }

        

        public decimal UnitPrice { get; set; }

        public decimal RoundedUnitPrice
        {
            get { return Math.Round(DBRoundedUnitPrice, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }

        public decimal DBRoundedUnitPrice
        {
            get { return Math.Round(UnitPrice, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }

        private decimal m_discountAmount;

        public decimal Discount
        {
            get { return GetDiscount(); }
            set { m_discountAmount = value; }
        }

        public decimal RoundedDiscount
        {
            get { return Math.Round(DBRoundedDiscount, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }

        public decimal DBRoundedDiscount
        {
            get { return Math.Round(Discount, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }

        private decimal m_taxAmount;

        public decimal TaxAmount
        {
            get { return GetTaxAmount(); }
            set { m_taxAmount = value; }
        }

        public decimal RoundedTaxAmount
        {
            get { return Math.Round(DBRoundedTaxAmount, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }

        public decimal DBRoundedTaxAmount
        {
            get { return Math.Round(TaxAmount, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
        private decimal m_totalAmount = 0;
        public decimal TotalAmount
        {
            get { return this.Amount + this.TaxAmount; }
            set { m_totalAmount = value; }
        }

        public decimal RoundedTotalAmount
        {
            get { return Math.Round(DBRoundedTotalAmount, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }

        public decimal DBRoundedTotalAmount
        {
            get { return Math.Round(TotalAmount, Common.DBAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
        public string DisplayTotalAmount
        {
            get { return RoundedTotalAmount.ToString("0.00"); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
        public string ItemReceiptDisplay
        {
            get
            {
                string itemDisplay = string.Empty;
                itemDisplay = (Qty.ToString("0") + " x " + ItemReceiptName); // + " @" + RoundedUnitPrice.ToString("0.00") + " + Tax = " + RoundedTaxAmount.ToString("0.00"));// + " - " + RoundedDiscount.ToString("0.00") + " = " + ((RoundedAmount)/RoundedQty).ToString("0.00")); //+ (m_discount!=0)? "\n - Disc=" + m_discount.ToString("0.00"):"");
                return itemDisplay;
            }
            set { throw new NotImplementedException("This property cannot be explicitly set"); }
        }

        public bool IsKit { get; set; }
        
        public bool IsComposite { get; set; }
        
        public decimal Weight { get; set; }
        
        public decimal Length { get; set; }
        
        public decimal Height { get; set; }
        
        public decimal Width { get; set; }
        
        public bool IsPromo { get; set; }
        
        public decimal PrimaryCost { get; set; }

        public int TaxCategoryId { get; set; }

        public List<CODetailDiscount> CODiscountList { get; set; }
        
        public List<CODetailTax> CODetailTaxList { get; set; }

        public string GiftVoucherNumber { get; set; }

        public string VoucherSrNo { get; set; }

        private decimal GetDiscount()
        {
            decimal returnValue = 0;
            if (string.IsNullOrEmpty(this.CustomerOrderNo))
            {
                //Calculate Value if the order has not yet been saved to DB
                foreach (CODetailDiscount d in this.CODiscountList)
                {
                        returnValue += d.DiscountAmount;
                }
            }
            else
            {
                //If the order has been saved to DB then fetch the value from DB
                returnValue = m_discountAmount;
            }
            return returnValue;
        }

        private decimal GetTaxAmount()
        {
            decimal returnValue = 0;
            if (string.IsNullOrEmpty(this.CustomerOrderNo))
            {
                //Calculate Value if the order has not yet been saved to DB
                foreach (CODetailTax d in this.CODetailTaxList)
                {
                        returnValue += d.TaxAmount;
                }
            }
            else
            {
                //If the order has been saved to DB then fetch the value from DB
                returnValue = m_taxAmount;
            }
            return returnValue;
        }

        public virtual DataTable GetSelectedRecords(string xmlDoc, string spName, ref string errorMessage)
        {
            DBParameterList dbParam;
            try
            {
                Vinculum.Framework.Data.DataTaskManager dtManager = new DataTaskManager();

                // initialize the parameter list object
                dbParam = new DBParameterList();

                // add the relevant 2 parameters
                dbParam.Add(new DBParameter(Common.PARAM_DATA, xmlDoc, DbType.String));
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                // executing procedure to save the record 
                DataTable dt = dtManager.ExecuteDataTable(spName, dbParam);

                // update database message
                errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();

                // if an error returned from the database
                if (errorMessage != string.Empty)
                    return null;
                else
                {
                    return dt;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CODetail GetCODetail(string OrderNo,string ItemCode,int itemID,ref string errorMessage)
        {
            try
            {
                CODetail order = null;
                if (!OrderNo.Equals(string.Empty) && (!ItemCode.Equals(string.Empty) || itemID > 0))
                {
                    System.Data.DataTable dTable = new DataTable();
                    DBParameterList dbParam;
                    try
                    {
                        Vinculum.Framework.Data.DataTaskManager dtManager = new DataTaskManager();
                        // initialize the parameter list object
                        dbParam = new DBParameterList();
                        dbParam.Add(new DBParameter("@OrderNo", OrderNo, DbType.String));
                        dbParam.Add(new DBParameter("@ItemCode", ItemCode, DbType.String));
                        dbParam.Add(new DBParameter("@ItemID", itemID, DbType.Int32));
                        dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                        // executing procedure to save the record 
                        dTable = dtManager.ExecuteDataTable(SP_CODETAIL_SEARCH, dbParam);

                        // update database message
                        errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();

                        // if an error returned from the database
                        if (errorMessage != string.Empty)
                            return null;
                        else if (dTable != null && dTable.Rows.Count == 1)
                        {
                            order = new CODetail();
                            order.CreateCODetailObject(dTable.Rows[0]);
                        }                        
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                return order;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CreateCODetailObject(DataRow dr)
        {             
            this.Amount = Convert.ToDecimal(dr["Amount"]);
            this.BV = Convert.ToDecimal(dr["BV"]);
            this.CustomerOrderNo = Convert.ToString(dr["CustomerOrderNo"]);
            this.DP = Convert.ToDecimal(dr["DP"]);
            this.Height = Convert.ToDecimal(dr["Height"]);
            this.IsComposite = Convert.ToBoolean(dr["IsComposite"]);
            this.IsKit = Convert.ToBoolean(dr["IsKit"]);
            this.IsPromo = Convert.ToBoolean(dr["IsPromo"]);
            this.ItemCode = Convert.ToString(dr["ItemCode"]);
            this.ItemDisplayName = Convert.ToString(dr["DisplayName"]);
            this.ItemId = Convert.ToInt32(dr["ItemId"]);
            this.ItemName = Convert.ToString(dr["ItemName"]);
            this.ItemPrintName = Convert.ToString(dr["PrintName"]);
            this.ItemReceiptName = Convert.ToString(dr["ReceiptName"]);
            this.ItemShortName = Convert.ToString(dr["ShortName"]);
            this.Length = Convert.ToDecimal(dr["Length"]);
            this.MRP = Convert.ToDecimal(dr["MRP"]);
            this.PrimaryCost = Convert.ToDecimal(dr["PrimaryCost"]);
            this.PV = Convert.ToDecimal(dr["PV"]);
            this.Qty = Convert.ToDecimal(dr["Qty"]);
            this.RecordNo = Convert.ToInt32(dr["RecordNo"]);
            this.TotalWeight = Convert.ToDecimal(dr["TotalWeight"]);
            this.UnitPrice = Convert.ToDecimal(dr["UnitPrice"]);
            this.UOM = Convert.ToInt32(dr["UOM"]);
            this.Weight = Convert.ToDecimal(dr["Weight"]);
            this.Width = Convert.ToDecimal(dr["Width"]);
            this.TaxCategoryId = Convert.ToInt32(dr["TaxCategoryId"]);
            this.Discount = Convert.ToDecimal(dr["Discount"]);
            this.TaxAmount = Convert.ToDecimal(dr["TaxAmount"]);
            this.GiftVoucherNumber = Convert.ToString(dr["GiftVoucherCode"]);
            this.VoucherSrNo = Convert.ToString(dr["VoucherSrNo"]);
        }

        public void GetDiscountDetail(DataTable dt)
        {
            List<CODetailDiscount> ListDiscount = new List<CODetailDiscount>();
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow[] drCollection=dt.Select("CustomerOrderNo='" + this.CustomerOrderNo + "' AND RecordNo=" + RecordNo);
                ListDiscount = new List<CODetailDiscount>();

                for (int i = 0; i < drCollection.Length; i++)
                {
                    CODetailDiscount discount = new CODetailDiscount();
                    discount.GetDiscountObject(drCollection[i]);
                    ListDiscount.Add(discount);
                }
            }
            this.CODiscountList=ListDiscount;
        }

        public void GetCODetailTax(DataTable dt)
        {
            List<CODetailTax> ListDetailTax = new List<CODetailTax>();
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow[] drCollection = dt.Select("CustomerOrderNo='" + this.CustomerOrderNo + "' AND RecordNo=" + RecordNo);
                ListDetailTax = new List<CODetailTax>();
               
                for (int i = 0; i < drCollection.Length; i++)
                {                    
                    CODetailTax tax = new CODetailTax();
                    tax.GetCODetailTaxObject(drCollection[i]);                    
                }
            }
            this.CODetailTaxList = ListDetailTax;
        }
    }
}
