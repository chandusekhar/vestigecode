using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CoreComponent.Core.BusinessObjects;
using Vinculum.Framework.Data;
using Vinculum.Framework.DataTypes;


namespace TaxComponent.BusinessObjects
{
    [Serializable]
    public class TaxApplication : Tax
    {
        public const string MODULE_CODE = "MDM06";
        private const string sp_GetTaxGroup = "usp_GetTaxGroupDetail";

        #region SP Declaration
        private const string SP_TAX_APPL_SEARCH = "usp_TaxApplicationSearch";
        private const string SP_TAX_APPL_SAVE = "usp_TaxApplicationSave";
        #endregion

        private System.Int32 m_taxCategoryId;
        private System.Int32 m_goodsDirection;
        private System.Int32 m_taxTypeId;
        private System.Int32 m_taxGroupId;
        private int m_formCTax;
        private Int32 m_stateId;
        private System.String m_taxAuthority;
        private List<Int32> m_stateList;
        private System.Int32 m_status;
        private string m_startDate;

        private string m_goodsDirectionValue, m_taxCategoryName, m_taxTypeyName, m_stateName, m_taxGroupCode, m_statusValue;

        public int FormCTax
        {
            get { return m_formCTax; }
            set { m_formCTax = value; }
        }

        public string StartDate
        {
            get { return m_startDate; }
            set { m_startDate = value; }
        }
        public string DisplayStartDate
        {
            get { return Convert.ToDateTime(StartDate).ToString(Common.DTP_DATE_FORMAT); }
            set { m_startDate = value; }
        }
        public System.Int32 TaxCategoryId
        {
            get { return m_taxCategoryId; }
            set { m_taxCategoryId = value; }
        }
        public System.Int32 GoodsDirection
        {
            get { return m_goodsDirection; }
            set { m_goodsDirection = value; }
        }
        public System.Int32 TaxTypeId
        {
            get { return m_taxTypeId; }
            set { m_taxTypeId = value; }
        }
        public System.Int32 TaxGroupId
        {
            get { return m_taxGroupId; }
            set { m_taxGroupId = value; }
        }

        public Int32 StateId
        {
            get { return m_stateId; }
            set { m_stateId = value; }
        }

        public List<Int32> States
        {
            get { return m_stateList; }
            set { m_stateList = value; }
        }

        public System.String TaxAuthority
        {
            get { return m_taxAuthority; }
            set { m_taxAuthority = value; }
        }

        public System.String TaxCategoryName
        {
            get { return m_taxCategoryName; }
            set { m_taxCategoryName = value; }
        }

        public System.String TaxTypeName
        {
            get { return m_taxTypeyName; }
            set { m_taxTypeyName = value; }
        }

        public System.String TaxGroupCode
        {
            get { return m_taxGroupCode; }
            set { m_taxGroupCode = value; }
        }

        public System.String StateName
        {
            get { return m_stateName; }
            set { m_stateName = value; }
        }

        public System.String GoodsDirectionValue
        {
            get { return m_goodsDirectionValue; }
            set { m_goodsDirectionValue = value; }
        }

        public System.String StatusValue
        {
            get { return m_statusValue; }
            set { m_statusValue = value; }
        }


        public System.Int32 Status
        {
            get { return m_status; }
            set { m_status = value; }
        }

        public bool Save(ref string errorMessage)
        {
            bool isSuccess = false;
            try
            {
                
                // call the save method which returns whether the save was successfull or not
                isSuccess = base.Save(CoreComponent.Core.BusinessObjects.Common.ToXml(this), SP_TAX_APPL_SAVE, ref errorMessage);
                return isSuccess;
            }
            catch (Exception ex)
            {
                CoreComponent.Core.BusinessObjects.Common.LogException(ex);
            }
            return isSuccess;
        }

        public List<TaxApplication> Search()
        {
            List<TaxApplication> taxList = new List<TaxApplication>();
            try
            {
                string errorMessage = string.Empty;
                System.Data.DataTable dTable = base.GetSelectedRecords(CoreComponent.Core.BusinessObjects.Common.ToXml(this), SP_TAX_APPL_SEARCH, ref errorMessage);

                if (dTable == null | dTable.Rows.Count == 0)
                    return null;

                foreach (System.Data.DataRow drow in dTable.Rows)
                {
                    TaxApplication tax = new TaxApplication();
                    tax.TaxId = Convert.ToInt32(drow["TaxId"]);
                    tax.TaxCategoryId = Convert.ToInt32(drow["TaxCategoryId"]);
                    tax.TaxTypeId = Convert.ToInt32(drow["TaxTypeId"]);
                    tax.TaxGroupId = Convert.ToInt32(drow["TaxGroupId"]);
                    tax.StateId = Convert.ToInt32(drow["StateId"]);
                    tax.GoodsDirection = Convert.ToInt32(drow["GoodsDirection"]);
                    tax.Status = Convert.ToInt32(drow["Status"]);
                    tax.FormCTax = Convert.ToInt32(drow["IsFormCTax"]);

                    tax.TaxAuthority = drow["TaxAuthority"].ToString();
                    tax.TaxCategoryName = drow["TaxCategoryName"].ToString();
                    tax.TaxTypeName = drow["TaxTypeName"].ToString();
                    tax.TaxGroupCode = drow["TaxGroupCode"].ToString();
                    tax.StateName = drow["StateName"].ToString();
                    tax.GoodsDirectionValue = drow["GoodsDirectionValue"].ToString();
                    tax.StatusValue = drow["StatusValue"].ToString();
                    tax.StartDate = drow["StartDate"].ToString();
                    tax.ModifiedDate = drow["ModifiedDate"].ToString();

                    taxList.Add(tax);
                }
            }
            catch (Exception ex)
            {
                CoreComponent.Core.BusinessObjects.Common.LogException(ex);
            }
            return taxList;
        }

        public static TaxGroup GetApplicableTaxes(int itemId, string itemCode, int fromStateId,
          int toStateId, string taxTypeCode, string Date, bool IsFormCApplicable, ref string errorMessage,
           ref string validationMessage, string sVendorCode, string sLocationCode)
        {
            DBParameterList dbParam = null;
            try
            {
                TaxGroup returnTaxGroup = null;
                dbParam = new DBParameterList();
                dbParam.Add(new DBParameter("@itemId", itemId, DbType.Int16));
                dbParam.Add(new DBParameter("@itemCode", itemCode, DbType.String));
                dbParam.Add(new DBParameter("@fromStateID", fromStateId, DbType.Int16));
                dbParam.Add(new DBParameter("@tostateID", toStateId, DbType.Int16));
                dbParam.Add(new DBParameter("@TaxTypeCode", taxTypeCode, DbType.String));
                dbParam.Add(new DBParameter("@Date", Date, DbType.String));
                dbParam.Add(new DBParameter("@IsFormC", IsFormCApplicable, DbType.Boolean));
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));
                dbParam.Add(new DBParameter("@validationMessage", string.Empty, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));
                dbParam.Add(new DBParameter("@vendorCode", sVendorCode, DbType.Int32));
                dbParam.Add(new DBParameter("@locationCode", sLocationCode, DbType.Int32));
             
                DataTaskManager dtManager = new DataTaskManager();
                // executing procedure to save the record 
                DataTable dt = dtManager.ExecuteDataTable(sp_GetTaxGroup, dbParam);
                errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();
                //If no error then process data
                validationMessage = dbParam["@validationMessage"].Value.ToString();
                if (!string.IsNullOrEmpty(validationMessage))
                {
                    return null;
                }
                if (string.IsNullOrEmpty(errorMessage) && dt != null)
                {
                    int rowCount = dt.Rows.Count;
                    for (int i = 0; i < rowCount; i++)
                    {
                        if (i == 0)
                        {
                            returnTaxGroup = new TaxGroup();
                            returnTaxGroup.TaxGroupId = Convert.ToInt32(dt.Rows[i]["TaxGroupId"]);
                            returnTaxGroup.TaxGroupCode = dt.Rows[i]["TaxGroupCode"].ToString();
                            returnTaxGroup.TaxCodeList = new List<TaxDetail>();
                        }
                        TaxDetail taxCode = new TaxDetail();
                        taxCode.TaxCode = dt.Rows[i]["TaxCode"].ToString();
                        taxCode.TaxCodeId = Convert.ToInt32(dt.Rows[i]["TaxCodeId"]);
                        taxCode.TaxPercent = Convert.ToDecimal(dt.Rows[i]["TaxPercent"]);
                        taxCode.GroupOrder = Convert.ToInt32(dt.Rows[i]["GroupOrder"]);
                        taxCode.IsInclusive = Convert.ToBoolean(dt.Rows[i]["IsInclusive"]);
                        returnTaxGroup.TaxCodeList.Add(taxCode);
                    }
                }
                return returnTaxGroup; ;
            }
            catch { throw; }
        }

    }
}
