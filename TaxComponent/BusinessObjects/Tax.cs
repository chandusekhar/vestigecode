using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreComponent.Core.BusinessObjects;
using System.Data;

using Vinculum.Framework.Data;
using Vinculum.Framework.DataTypes;

namespace TaxComponent.BusinessObjects
{
    [Serializable]
    public class Tax
    {
        #region SP Declaration
        private const string SP_TAX_SEARCH = "usp_TaxSearch";
        private const string SP_TAX_APPLICABLE = "usp_FindApplicableTaxes";

        #endregion

        private System.Int32 m_createdBy, m_taxId;
        private string m_createdDate;
        private System.Int32 m_modifiedBy;
        private string m_modifiedDate = new DateTime(1900, 1, 1).ToString(CoreComponent.Core.BusinessObjects.Common.DATE_TIME_FORMAT);

        #region Property
        public System.Int32 TaxId
        {
            get { return m_taxId; }
            set { m_taxId = value; }
        }

        public System.Int32 CreatedBy
        {
            get { return m_createdBy; }
            set { m_createdBy = value; }
        }

        public string CreatedDate
        {
            get { return m_createdDate; }
            set { m_createdDate = value; }
        }

        public System.Int32 ModifiedBy
        {
            get { return m_modifiedBy; }
            set { m_modifiedBy = value; }
        }

        public string ModifiedDate
        {
            get { return m_modifiedDate; }
            set { m_modifiedDate = value; }
        }
        #endregion


        public enum TaxType
        {
            SOTAX,
            POTAX,
            TOTAX
        }

        #region TaxEnum
        public enum TaxEnum
        {
            TaxCategory = 1,
            TaxType = 2,
            TaxCode = 3,
            TaxGroup = 4,
            TaxJurisdiction = 5,
            TaxApplication = 6
        }
        public enum TaxDirection
        {
            In = 1,
            Out= 2,
            Within = 3,
        }

        #endregion

        #region Methods
        
        /// <summary>
        /// Method to fetch Parameter Data
        /// </summary>
        /// <param name="key">Parameter Identifier</param>
        /// <param name="filter">Parameter Filter Criteria</param>
        /// <returns></returns>
        /// 
        //public static 

        public static DataTable TaxLookup(TaxEnum key)
        {
            DBParameterList dbParam = null;

            try
            {
                dbParam = new DBParameterList();
                dbParam.Add(new DBParameter("@TaxParameter", (int)key, DbType.Int16));
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                using (DataTaskManager dtManager = new DataTaskManager())
                {
                    return dtManager.ExecuteDataTable(SP_TAX_SEARCH, dbParam);
                }
            }
            catch { throw; }
        }

        public virtual bool Save(string xmlDoc, string spName, ref string errorMessage)
        {
            bool isSuccess = false;
            try
            {
                DBParameterList dbParam;
                using (DataTaskManager dtManager = new DataTaskManager())
                {
                    try
                    {
                        // initialize the parameter list object
                        dbParam = new DBParameterList();

                        // add the relevant 2 parameters
                        dbParam.Add(new DBParameter(Common.PARAM_DATA, xmlDoc, DbType.String));
                        dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                        // begin the transaction
                        dtManager.BeginTransaction();
                        // executing procedure to save the record 
                        DataTable dt = dtManager.ExecuteDataTable(spName, dbParam);

                        // update database message
                        errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();

                        // if an error returned from the database
                        if ((errorMessage != string.Empty) && (errorMessage.IndexOf("INF0032")!=-1) &&  (errorMessage.IndexOf("INF0033")!=-1))
                        {
                            isSuccess = false;
                            dtManager.RollbackTransaction();
                        }
                        else
                        {
                            //this.TaxId = Convert.ToInt32(dt.Rows[0]["TaxId"]);
                            //this.ModifiedDate = (dt.Rows[0]["ModifiedDate"]).ToString();
                            dtManager.CommitTransaction();
                            isSuccess = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        Common.LogException(ex);
                    }
                }

            }
            catch (Exception ex)
            {
                Common.LogException(ex);
            }
            return isSuccess;
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

        public static DataTable FindApplicableTaxes(Int32 taxCategoryId, Int32 taxTypeId, Int32 goodsDirectionId, Int32 taxJurisdictionId, string  errorMessage)
        {
            DBParameterList dbParam;
            try
            {
                Vinculum.Framework.Data.DataTaskManager dtManager = new DataTaskManager();

                // initialize the parameter list object
                dbParam = new DBParameterList();

                // add the relevant 4 parameters
                dbParam.Add(new DBParameter("TaxCategoryId", taxCategoryId, DbType.Int32));
                dbParam.Add(new DBParameter("TaxTypeId", taxTypeId, DbType.Int32));
                dbParam.Add(new DBParameter("GoodsDirectionId", goodsDirectionId, DbType.Int32));
                dbParam.Add(new DBParameter("TaxJurisdictionId", taxJurisdictionId, DbType.Int32));

                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                // executing procedure to save the record 
                DataTable dt = dtManager.ExecuteDataTable(SP_TAX_APPLICABLE, dbParam);

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

      #endregion Methods
    }
}
