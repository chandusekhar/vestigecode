using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CoreComponent.BusinessObjects;
using CoreComponent.Core.BusinessObjects;
using Vinculum.Framework.Data;
using Vinculum.Framework.DataTypes;

namespace PurchaseComponent.BusinessObjects
{
    [Serializable]
    public class PurchaseOrderTaxDetail
    {
        #region DATA Field Constants
        private const string CON_FIELD_PONUMBER = "PONumber";
        private const string CON_FIELD_AMENDMENTNO = "AmendmentNo";
        private const string CON_FIELD_ITEMID = "ItemId";
        private const string CON_FIELD_TAXGROUPCODE = "TaxGroupCode";
        private const string CON_FIELD_ROWNO = "RowNo";
        private const string CON_FIELD_GROUPORDER = "GroupOrder";        
        private const string CON_FIELD_TAXCODE = "TaxCode";
        private const string CON_FIELD_TAXPERCENT = "TaxPercent";        
        private const string CON_FIELD_TAXAMOUNT = "TaxAmount";
        private const string CON_FIELD_CREATEDBY = "CreatedBy";
        private const string CON_FIELD_CREATEDDATE = "CreatedDate";
        private const string CON_FIELD_MODIFIEDBY = "ModifiedBy";
        private const string CON_FIELD_MODIFIEDDATE = "ModifiedDate";
                 
        #endregion

        #region property
        private string m_poNumber = string.Empty;
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
        private int m_itemId = Common.INT_DBNULL;
        public int ItemID
        {
            get { return m_itemId; }
            set { m_itemId = value; }
        }
        private int m_rowno = 0;
        public int RowNo
        {
            get { return m_rowno; }
            set { m_rowno = value; }
        }
        private string m_taxCode = string.Empty;

        public string TaxCode
        {
            get { return m_taxCode; }
            set { m_taxCode = value; }
        }
        private decimal m_taxPerc = 0;

        public decimal TaxPercentage
        {
            get { return m_taxPerc; }
            set { m_taxPerc = value; }
        }

        private string m_taxgroup = string.Empty;

        public string TaxGroup
        {
            get { return m_taxgroup; }
            set { m_taxgroup = value; }
        }
        private decimal m_taxAmount = 0;

        public decimal TaxAmount
        {
            get { return m_taxAmount; }
            set { m_taxAmount = value; }
        }
        public decimal DBTaxAmount
        {
            get { return Math.Round(TaxAmount, Common.DBAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
        public decimal DisplayTaxAmount
        {
            get { return Math.Round(DBTaxAmount, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
        private int m_direction = 1;
        public int Direction
        {
            get { return m_direction; }
            set { m_direction = value; }
        }

      
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
        public int GroupOrder { get; set; }

        public bool IsInclusive
        {
            set;
            get;
        }
        #endregion

        #region SP declaration
        private const string sp_GetTaxGroup="usp_GetTaxGroupDetail";
        private const string SP_POTAXDETAIL_SEARCH = "usp_POTaxDetailSearch";
        #endregion

        #region Methods
        public List<PurchaseOrderTaxDetail> Search()
        {
            List<PurchaseOrderTaxDetail> purchaseOrderTaxList = new List<PurchaseOrderTaxDetail>();
            System.Data.DataTable dTable = new DataTable();
            try
            {
                string errorMessage = string.Empty;
                dTable = GetSelectedRecords(Common.ToXml(this), SP_POTAXDETAIL_SEARCH, ref errorMessage);

                if (dTable != null)
                    foreach (System.Data.DataRow drow in dTable.Rows)
                    {
                        PurchaseOrderTaxDetail _purchase = CreateTaxDetailObject(drow);
                        purchaseOrderTaxList.Add(_purchase);
                    }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return purchaseOrderTaxList;
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

        public static PurchaseOrderTaxDetail CreateTaxDetailObject(DataRow drow)
        {
            try
            {
                PurchaseOrderTaxDetail _purchase = new PurchaseOrderTaxDetail();

                if (drow.Table.Columns.Contains(CON_FIELD_PONUMBER))
                    _purchase.PONumber = Convert.ToString(drow[CON_FIELD_PONUMBER]);

                if (drow.Table.Columns.Contains(CON_FIELD_ITEMID))
                    _purchase.ItemID = Convert.ToInt32(drow[CON_FIELD_ITEMID]);

                if (drow.Table.Columns.Contains(CON_FIELD_ROWNO))
                    _purchase.RowNo = Convert.ToInt32(drow[CON_FIELD_ROWNO]);

                if (drow.Table.Columns.Contains(CON_FIELD_AMENDMENTNO))
                    _purchase.AmendmentNo = Convert.ToInt32(drow[CON_FIELD_AMENDMENTNO]);

                if (drow.Table.Columns.Contains(CON_FIELD_TAXCODE))
                    _purchase.TaxCode = Convert.ToString(drow[CON_FIELD_TAXCODE]);

                if (drow.Table.Columns.Contains(CON_FIELD_TAXPERCENT))
                    _purchase.TaxPercentage = Convert.ToDecimal(drow[CON_FIELD_TAXPERCENT]);

                if (drow.Table.Columns.Contains(CON_FIELD_TAXGROUPCODE))
                    _purchase.TaxGroup = Convert.ToString(drow[CON_FIELD_TAXGROUPCODE]);

                if (drow.Table.Columns.Contains(CON_FIELD_TAXAMOUNT))
                    _purchase.TaxAmount = Convert.ToDecimal(drow[CON_FIELD_TAXAMOUNT]);

                if (drow.Table.Columns.Contains(CON_FIELD_GROUPORDER))
                    _purchase.GroupOrder = Convert.ToInt32(drow[CON_FIELD_GROUPORDER]);

                if (drow.Table.Columns.Contains(CON_FIELD_CREATEDBY))
                    _purchase.CreatedBy = Convert.ToInt32(drow[CON_FIELD_CREATEDBY]);

                if (drow.Table.Columns.Contains(CON_FIELD_CREATEDDATE))
                    _purchase.CreatedDate = Convert.ToDateTime(drow[CON_FIELD_CREATEDDATE]);

                if (drow.Table.Columns.Contains(CON_FIELD_MODIFIEDBY))
                    _purchase.ModifiedBy = Convert.ToInt32(drow[CON_FIELD_MODIFIEDBY]);

                if (drow.Table.Columns.Contains(CON_FIELD_MODIFIEDDATE))
                    _purchase.ModifiedDate = Convert.ToDateTime(drow[CON_FIELD_MODIFIEDDATE]);
                return _purchase;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
