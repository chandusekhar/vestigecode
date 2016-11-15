using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CoreComponent.BusinessObjects;
using CoreComponent.Core.BusinessObjects;
using TaxComponent.BusinessObjects;
using Vinculum.Framework.Data;
using Vinculum.Framework.DataTypes;

namespace PurchaseComponent.BusinessObjects
{
    [Serializable]
    public class PurchaseOrderDetail: IPurchaseOrderDetail
    {
        #region DATA Field Constants
        private const string CON_FIELD_PONUMBER = "PONumber";
        private const string CON_FIELD_AMENDMENTNO = "AmendmentNo";
        private const string CON_FIELD_ITEMID = "ItemId";
        private const string CON_FIELD_POUOMID = "PurchaseUOMID";
        private const string CON_FIELD_POUOM = "PurchaseUOMName";
       
        private const string CON_FIELD_ITEMDESC = "ItemDescription";
        private const string CON_FIELD_UNITPRICE = "UnitPrice";
        private const string CON_FIELD_UNITQTY  = "UnitQty";
        private const string CON_FIELD_TAXGROUPCODE = "TaxGroupCode";
        private const string CON_FIELD_TAXAMOUNT = "LineTaxAmount";
        private const string CON_FIELD_LINEAMOUNT = "LineAmount";
        private const string CON_FIELD_CREATEDBY = "CreatedBy";
        private const string CON_FIELD_CREATEDDATE = "CreatedDate";
        private const string CON_FIELD_MODIFIEDBY = "ModifiedBy";
        private const string CON_FIELD_MODIFIEDDATE = "ModifiedDate";
        private const string CON_FIELD_ITEMCODE = "ItemCode";
        private const string CON_FIELD_LEADTIME = "LeadTime";
        private const string CON_FIELD_MOQ = "MinOrderQty";
        private const string CON_FIELD_PUF = "PurchaseUnitFactor";
       
        #endregion

        private int m_fromStateId;
        private int m_toStateId;

        public PurchaseOrderDetail()
        {
        }

        public PurchaseOrderDetail(int fromStateId, int toStateId)
        {
            m_fromStateId = fromStateId;
            m_toStateId = toStateId;
        }

        public PurchaseOrderDetail(int locationId, bool isFormCApplicable, int fromStateId, int toStateId)
        {
            m_locationId = locationId;
            m_isFormCApplicable = isFormCApplicable;
            m_fromStateId = fromStateId;
            m_toStateId = toStateId;
        }

        #region Property

        private int m_locationId;
        private bool m_isFormCApplicable;

        private int m_leadTime = 0;

        public int LeadTime
        {
            get { return m_leadTime; }
            set { m_leadTime = value; }
        }
        private string m_poNumber=string.Empty;
        public string PONumber
        {
            get { return m_poNumber; }
            set { m_poNumber = value; }
        }
        private int m_amendmentNo = 0;
        public int AmendmentNo
        {
            get { return m_amendmentNo; }
            set { m_amendmentNo = value; }
        }
        private int m_itemId=Common.INT_DBNULL;
        public int ItemID
        {
            get { return m_itemId; }
            set { m_itemId = value; }
        }

        private string m_itemCode = string.Empty;
        public string ItemCode
        {
            get { return m_itemCode; }
            set { m_itemCode = value; }
        }
        private int m_purchaseUOMID = Common.INT_DBNULL;

        public int PurchaseUOMID
        {
            get { return m_purchaseUOMID; }
            set { m_purchaseUOMID = value; }
        }
        
        private string m_purchaseUOM = string.Empty;
        public string PurchaseUOM
        {
            get { return m_purchaseUOM; }
            set { m_purchaseUOM = value; }
        }

        private string m_itemDescription=string.Empty;
        public string ItemDescription
        {
            get { return m_itemDescription; }
            set { m_itemDescription = value; }
        }

        private decimal m_unitPrice=0;
        public decimal UnitPrice
        {
            get { return m_unitPrice; }
            set { m_unitPrice = value; }
        }
        public decimal DisplayUnitPrice
        {
            get { return Math.Round(DBUnitPrice, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
        public decimal DBUnitPrice
        {
            get { return Math.Round(UnitPrice, Common.DBAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }

        private decimal m_unitQty=0;
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

        private string m_taxGroupCode=string.Empty;
        public string TaxGroupCode
        {
            get { return m_taxGroupCode; }
            set { m_taxGroupCode = value; }
        }

        private string m_taxGroupCodeName = string.Empty;
        public string TaxGroupCodeName
        {
            get { return m_taxGroupCodeName; }
            set { m_taxGroupCodeName = value; }
        }
      
        public decimal LineTaxAmount
        {
            get { return GetTotalDetailTax(); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
        public decimal DisplayLineTaxAmount
        {
            get { return Math.Round(DBLineTaxAmount, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
        public decimal DBLineTaxAmount
        {
            get { return Math.Round(LineTaxAmount, Common.DBAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }

        public decimal LineAmounts
        {
            get { return Math.Round((this.UnitQty * this.UnitPrice)-((IsInclusive) ? this.LineTaxAmount : 0), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }

        public decimal LineAmount
        {
            get { return (this.UnitQty * this.UnitPrice)+ ((IsInclusive)? 0: this.LineTaxAmount); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }

        public decimal DisplayLineAmount
        {
            get { return Math.Round(DBLineAmount, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
        public decimal DBLineAmount
        {
            get { return Math.Round(LineAmount, Common.DBAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }

        private int m_status=0;
        public int Status
        {
            get { return m_status; }
            set { m_status = value; }
        }

        private string m_statusName=string.Empty;
        public string StatusName
        {
            get { return m_statusName; }
            set { m_statusName = value; }
        }

        public decimal PUF { get; set; }

        public decimal DisplayPUF
        {
            get { return Math.Round(DBPUF, Common.DisplayQtyRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
        public decimal DBPUF
        {
            get { return Math.Round(PUF, Common.DBQtyRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }

        public decimal MOQ { get; set; }
        public decimal DisplayMOQ
        {
            get { return Math.Round(DBMOQ, Common.DisplayQtyRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
        public decimal DBMOQ
        {
            get { return Math.Round(MOQ, Common.DBQtyRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
       

        private List<PurchaseOrderTaxDetail> m_purchaseOrderTaxDetail = new List<PurchaseOrderTaxDetail>();

        public List<PurchaseOrderTaxDetail> PurchaseOrderTaxDetail
        {
            get { return m_purchaseOrderTaxDetail; }
            set { m_purchaseOrderTaxDetail = value; }
        }
        public bool IsInclusive
        {
            get;
            set;
        }

        #endregion

        #region SP Declaration
        #endregion  
              
        public List<PurchaseOrderTaxDetail> GetTaxDetail()
        {
            try
            {
                PurchaseOrderTaxDetail _tax = new PurchaseOrderTaxDetail();
                _tax.PONumber = PONumber;
                _tax.AmendmentNo = AmendmentNo;
                _tax.ItemID = ItemID;
                return _tax.Search();
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        public void CreatePODetailObject(System.Data.DataRow drow)
        {
            try
            {
                if (drow.Table.Columns.Contains(CON_FIELD_AMENDMENTNO))
                    this.AmendmentNo = Convert.ToInt32(drow[CON_FIELD_AMENDMENTNO]);

                if (drow.Table.Columns.Contains(CON_FIELD_CREATEDBY))
                    this.CreatedBy = Convert.ToInt32(drow[CON_FIELD_CREATEDBY]);

                if (drow.Table.Columns.Contains(CON_FIELD_CREATEDDATE))
                    this.CreatedDate = Convert.ToDateTime(drow[CON_FIELD_CREATEDDATE]);

                if (drow.Table.Columns.Contains(CON_FIELD_ITEMDESC))
                    this.ItemDescription = Convert.ToString(drow[CON_FIELD_ITEMDESC]);

                if (drow.Table.Columns.Contains(CON_FIELD_ITEMID))
                    this.ItemID = Convert.ToInt32(drow[CON_FIELD_ITEMID]);

                if (drow.Table.Columns.Contains(CON_FIELD_MODIFIEDBY))
                    this.ModifiedBy = Convert.ToInt32(drow[CON_FIELD_MODIFIEDBY]);

                if (drow.Table.Columns.Contains(CON_FIELD_MODIFIEDDATE))
                    this.ModifiedDate = Convert.ToDateTime(drow[CON_FIELD_MODIFIEDDATE]);

                if (drow.Table.Columns.Contains(CON_FIELD_PONUMBER))
                    this.PONumber = Convert.ToString(drow[CON_FIELD_PONUMBER]);

                if (drow.Table.Columns.Contains(CON_FIELD_POUOMID))
                    this.PurchaseUOMID = Convert.ToInt32(drow[CON_FIELD_POUOMID]);

                if (drow.Table.Columns.Contains(CON_FIELD_POUOM))
                    this.PurchaseUOM = Convert.ToString(drow[CON_FIELD_POUOM]);

                if (drow.Table.Columns.Contains(CON_FIELD_TAXGROUPCODE))
                    this.TaxGroupCode = Convert.ToString(drow[CON_FIELD_TAXGROUPCODE]);

                if (drow.Table.Columns.Contains(CON_FIELD_UNITPRICE))
                    this.UnitPrice = Convert.ToDecimal(drow[CON_FIELD_UNITPRICE]);

                if (drow.Table.Columns.Contains(CON_FIELD_UNITQTY))
                    this.UnitQty = Convert.ToDecimal(drow[CON_FIELD_UNITQTY]);

                if (drow.Table.Columns.Contains(CON_FIELD_ITEMCODE))
                    this.ItemCode = Convert.ToString(drow[CON_FIELD_ITEMCODE]);

                if (drow.Table.Columns.Contains(CON_FIELD_PUF))
                    this.PUF = Convert.ToDecimal(drow[CON_FIELD_PUF]);

                if (drow.Table.Columns.Contains(CON_FIELD_MOQ))
                    this.MOQ = Convert.ToDecimal(drow[CON_FIELD_MOQ]);
                if (drow.Table.Columns.Contains("IsInclusiveofTax"))
                    this.IsInclusive = Convert.ToBoolean(drow["IsInclusiveofTax"]);

                
              
            }
            catch (Exception ex)
            {
                throw ex;
            }
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

        //public void SetTaxAmount()
        //{
        //    if (PurchaseOrderTaxDetail != null)
        //    {
        //        decimal taxableamount = LineAmount;
        //        decimal tax = 0; string taxgroup = "";
        //        foreach (PurchaseOrderTaxDetail ptax in PurchaseOrderTaxDetail)
        //        {
        //            ptax.TaxAmount = (ptax.TaxPercentage * taxableamount) / 100;
        //            tax = tax + ptax.TaxAmount;
        //            taxableamount = tax;
        //        }
        //        LineTaxAmount = tax;
        //    }
        //}

        //public void GetAndCalculateTaxes()
        //{
        //    try
        //    {
        //        GetAndCalculateTaxes(true);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public void GetAndCalculateTaxes(bool callCalculate, string sVendorCode, string sLocationCode, int fromstateid, int tostateid)
        {
            try
            {
                if (string.IsNullOrEmpty(this.TaxGroupCode))
                {
                    GetTaxes(sVendorCode, sLocationCode,fromstateid,tostateid);
                }
                if (callCalculate)
                    CalculateTaxes();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void GetAndCalculateTaxes(bool callCalculate,string sVendorCode,string sLocationCode)
        {
            try
            {
                if (string.IsNullOrEmpty(this.TaxGroupCode))
                {
                    GetTaxes(sVendorCode,sLocationCode);
                }
                if (callCalculate)
                    CalculateTaxes();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CalculateTaxes()
        {
            bool bIsInclusive = false;
            decimal dcValue = 0;
            DataTable dtTemp = new DataTable();
            decimal ItemMRP = 0;
            decimal AssMRPPercent = 0;
            try
            {
                if (this.PurchaseOrderTaxDetail != null )
                {
                    decimal previousLevelAmount = 0, currentLevelAmount = 0;
                    int previousLevel = 0, currentLevel = 0;

                    this.PurchaseOrderTaxDetail.Sort(new CoreComponent.Core.BusinessObjects.GenericComparer<PurchaseOrderTaxDetail>("GroupOrder", SortDirection.Ascending));
                    dtTemp = fncItemTaxExtension(this.ItemID);
                    if (dtTemp.Rows.Count > 0)
                    {
                        ItemMRP = Convert.ToDecimal(dtTemp.Rows[0]["MRP"].ToString());
                        AssMRPPercent = Convert.ToDecimal(dtTemp.Rows[0]["AssMRPPercent"].ToString());
                    for (int i = 0; i < this.PurchaseOrderTaxDetail.Count; i++)
                    {
                        bIsInclusive = PurchaseOrderTaxDetail[i].IsInclusive;
                            if (this.PurchaseOrderTaxDetail[i].GroupOrder == previousLevel)
                            {
                                currentLevel = previousLevel;
                                dcValue = this.PurchaseOrderTaxDetail[i].TaxAmount = (
                                    (previousLevelAmount == 0) ? this.UnitQty * ((ItemMRP*AssMRPPercent)/100) : previousLevelAmount)
                                    * ((bIsInclusive) ? this.PurchaseOrderTaxDetail[i].TaxPercentage / (100 + this.PurchaseOrderTaxDetail[i].TaxPercentage) : this.PurchaseOrderTaxDetail[i].TaxPercentage / 100);
                        
                                currentLevelAmount += dcValue;
                        
                            }
                            else
                            {
                                currentLevel = ++previousLevel;
                                previousLevelAmount = currentLevelAmount;
                                currentLevelAmount = this.PurchaseOrderTaxDetail[i].TaxAmount = (
                                    (previousLevelAmount == 0) ? this.UnitQty * ((ItemMRP * AssMRPPercent) / 100) : previousLevelAmount)
                                    * ((bIsInclusive) ? this.PurchaseOrderTaxDetail[i].TaxPercentage / (100 + this.PurchaseOrderTaxDetail[i].TaxPercentage) : this.PurchaseOrderTaxDetail[i].TaxPercentage / 100);
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < this.PurchaseOrderTaxDetail.Count; i++)
                        {
                            bIsInclusive = PurchaseOrderTaxDetail[i].IsInclusive;
                            if (this.PurchaseOrderTaxDetail[i].GroupOrder == previousLevel)
                            {
                                currentLevel = previousLevel;
                                dcValue = this.PurchaseOrderTaxDetail[i].TaxAmount = (
                                    (previousLevelAmount == 0) ? this.UnitQty * this.UnitPrice : previousLevelAmount)
                                    * ((bIsInclusive) ? this.PurchaseOrderTaxDetail[i].TaxPercentage / (100 + this.PurchaseOrderTaxDetail[i].TaxPercentage): this.PurchaseOrderTaxDetail[i].TaxPercentage / 100);
                                //if (!bIsInclusive)
                                    currentLevelAmount += dcValue;
                                //else
                                    //currentLevelAmount -= dcValue;
                            }
                            else
                            {
                                currentLevel = ++previousLevel;
                                previousLevelAmount = currentLevelAmount;
                                currentLevelAmount = this.PurchaseOrderTaxDetail[i].TaxAmount = (
                                    (previousLevelAmount == 0) ? this.UnitQty * this.UnitPrice : previousLevelAmount)
                                    * ((bIsInclusive) ? this.PurchaseOrderTaxDetail[i].TaxPercentage / (100+ this.PurchaseOrderTaxDetail[i].TaxPercentage): this.PurchaseOrderTaxDetail[i].TaxPercentage / 100);
                            }
                        }
                    }
                    }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private DataTable fncItemTaxExtension(int ItemID)
        {
            DataTable dtItemTaxExtension = new DataTable();
            DBParameterList dbParam;
            try
            {
                Vinculum.Framework.Data.DataTaskManager dtManager = new DataTaskManager();
                dbParam = new DBParameterList();
                dbParam.Add(new DBParameter("@ItemID", ItemID, DbType.Int32));
                dtItemTaxExtension = dtManager.ExecuteDataTable("usp_GetItemTaxExtension", dbParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtItemTaxExtension;
        }

        private void GetTaxes(string sVendorCode, string sLocationCode, int fromstateid, int tostateid)
        {
            try
            {
                string errorMessage = string.Empty;
                string validationMessage = string.Empty;
                TaxGroup tg = TaxApplication.GetApplicableTaxes(ItemID, "", fromstateid, tostateid,
                    Tax.TaxType.SOTAX.ToString(), "", m_isFormCApplicable, ref errorMessage,
                    ref validationMessage, sVendorCode, sLocationCode);
                if (string.IsNullOrEmpty(errorMessage))
                {
                    //If no error message then process data
                    if (tg != null && !(string.IsNullOrEmpty(tg.TaxGroupCode) && tg.TaxCodeList != null && tg.TaxCodeList.Count > 0))
                    {
                        this.TaxGroupCode = tg.TaxGroupCode;
                        this.PurchaseOrderTaxDetail = new List<PurchaseOrderTaxDetail>();
                        int rowno = 1;
                        for (int i = 0; i < tg.TaxCodeList.Count; i++)
                        {
                            PurchaseOrderTaxDetail potd = new PurchaseOrderTaxDetail();
                            potd.TaxCode = tg.TaxCodeList[i].TaxCode;
                            potd.TaxPercentage = tg.TaxCodeList[i].TaxPercent;
                            potd.GroupOrder = tg.TaxCodeList[i].GroupOrder;
                            potd.ItemID = ItemID;
                            potd.Direction = (int)Tax.TaxDirection.In;
                            potd.TaxGroup = tg.TaxGroupCode;
                            potd.PONumber = PONumber;
                            potd.RowNo = rowno++;
                            potd.IsInclusive = tg.TaxCodeList[i].IsInclusive;
                            this.PurchaseOrderTaxDetail.Add(potd);
                            this.IsInclusive = potd.IsInclusive;
                        }
                    }
                }
                else
                {
                    //Log error
                    //Show message
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void GetTaxes(string sVendorCode,string sLocationCode)
        {
            try
            {
                string errorMessage = string.Empty;
                string validationMessage = string.Empty;
                //TaxGroup tg = TaxApplication.GetApplicableTaxes(ItemID, "", m_fromStateId, m_toStateId,
                //    Tax.TaxType.SOTAX.ToString(), "", m_isFormCApplicable, ref errorMessage,
                //    ref validationMessage, sVendorCode, sLocationCode);
                TaxGroup tg = TaxApplication.GetApplicableTaxes(ItemID, "", m_fromStateId, m_toStateId, 
                    Tax.TaxType.POTAX.ToString(), "", m_isFormCApplicable, ref errorMessage, 
                    ref validationMessage,sVendorCode,sLocationCode);
                if (string.IsNullOrEmpty(errorMessage))
                {
                    //If no error message then process data
                    if (tg != null && !(string.IsNullOrEmpty(tg.TaxGroupCode) && tg.TaxCodeList != null && tg.TaxCodeList.Count > 0))
                    {
                        this.TaxGroupCode = tg.TaxGroupCode;
                        this.PurchaseOrderTaxDetail = new List<PurchaseOrderTaxDetail>();
                        int rowno = 1;
                        for (int i = 0; i < tg.TaxCodeList.Count; i++)
                        {
                            PurchaseOrderTaxDetail potd = new PurchaseOrderTaxDetail();
                            potd.TaxCode = tg.TaxCodeList[i].TaxCode;
                            potd.TaxPercentage = tg.TaxCodeList[i].TaxPercent;
                            potd.GroupOrder = tg.TaxCodeList[i].GroupOrder;
                            potd.ItemID = ItemID;
                            potd.Direction = (int)Tax.TaxDirection.In;
                            potd.TaxGroup = tg.TaxGroupCode;
                            potd.PONumber = PONumber;
                            potd.RowNo = rowno++;
                            potd.IsInclusive = tg.TaxCodeList[i].IsInclusive;
                            this.PurchaseOrderTaxDetail.Add(potd);
                            this.IsInclusive = potd.IsInclusive;
                        }
                    }
                }
                else
                {
                    //Log error
                    //Show message
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private decimal GetTotalDetailTax()
        {
            try
            {
                decimal returnTaxAmount = 0;
                if (this.PurchaseOrderTaxDetail != null)
                {
                    for (int i = 0; i < this.PurchaseOrderTaxDetail.Count; i++)
                    {
                        returnTaxAmount += this.PurchaseOrderTaxDetail[i].TaxAmount;
                    }
                }
                return returnTaxAmount;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #region IPurchaseOrderDetail Members

        private int m_createdBy = 0;
        public int CreatedBy
        {
            get
            {
                return m_createdBy;
            }
            set
            {
                m_createdBy = value;
            }
        }
        private DateTime m_createdDate = Common.DATETIME_CURRENT;
        public DateTime CreatedDate
        {
            get
            {
                return m_createdDate;
            }
            set
            {
                m_createdDate = value;
            }
        }
        private int m_modifiedBy = 0;
        public int ModifiedBy
        {
            get
            {
                return m_modifiedBy;
            }
            set
            {
                m_modifiedBy = value;
            }
        }
        private DateTime m_modifiedDate = Common.DATETIME_CURRENT;
        public DateTime ModifiedDate
        {
            get
            {
                return m_modifiedDate;
            }
            set
            {
                m_modifiedDate = value;
            }
        }


        #endregion
    }
}
