using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreComponent.Core.BusinessObjects;
using System.Data;
using Vinculum.Framework.Data;
using Vinculum.Framework.DataTypes;

namespace CoreComponent.BusinessObjects
{
    [Serializable]
    public class DistributorCarTravel
    {
        #region SP Declaration
        private const string SP_DISTRIBUTOR_CAR_FUND_INFO = "usp_GetDistributorCarFundInfo";
        private const string SP_DISTRIBUTOR_CAR_FUND_SAVE = "usp_DistributorCarFundSave";
        #endregion

        #region Property
        private string m_distributorId = string.Empty;
        private string m_distributorName= string.Empty;
        private string m_bonusPercent = string.Empty;
        private Boolean m_payCarFund = false;
        
        public string DistributorId
        {
            get { return m_distributorId; }
            set { m_distributorId = value; }
        }
        public string DistributorName
        {
            get { return m_distributorName; }
            set { m_distributorName = value; }
        }
        public string BonusPercent
        {
            get { return m_bonusPercent; }
            set { m_bonusPercent = value; }
        }
        public Boolean PayCarFund
        {
            get { return m_payCarFund; }
            set { m_payCarFund = value; }
        }

        #endregion

        #region Methods

        public static bool Save(String xmlDoc, ref String errorMessage)
        {
            DBParameterList dbParam;
            bool isSuccess = false;
            using (DataTaskManager dtManager = new DataTaskManager())
            {
                dtManager.BeginTransaction();
                {
                    try
                    {
                        dbParam = new DBParameterList();
                        dbParam.Add(new DBParameter(Common.PARAM_DATA, xmlDoc, DbType.String));
                        dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, errorMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                        DataTable dt = dtManager.ExecuteDataTable(SP_DISTRIBUTOR_CAR_FUND_SAVE, dbParam);

                        errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();
                        {
                            if (errorMessage.Length > 0)
                            {
                                isSuccess = false;
                                dtManager.RollbackTransaction();
                            }
                            else
                            {
                                dtManager.CommitTransaction();
                                isSuccess = true;
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        dtManager.RollbackTransaction();
                        throw ex;
                    }
                    return isSuccess;
                }
            }
        }

        public static List<DistributorCarTravel> GetDistributorCarFundInfo(String xmlDoc)
        {
            System.Data.DataTable dTable = new DataTable();
            List<DistributorCarTravel> lst = new List<DistributorCarTravel>();
            try
            {
                string errorMessage = string.Empty;
                DistributorPayment dp = new DistributorPayment();
                dTable = GetSelectedRecords(xmlDoc, SP_DISTRIBUTOR_CAR_FUND_INFO, ref errorMessage);
              
                for (int i = 0; i < dTable.Rows.Count; i++)
                {
                    DistributorCarTravel dct = new DistributorCarTravel();
                    dct.DistributorId= dTable.Rows[i]["DistributorId"].ToString();
                    dct.BonusPercent= dTable.Rows[i]["BonusPercent"].ToString();
                    dct.DistributorName = dTable.Rows[i]["DistributorName"].ToString();
                    dct.PayCarFund = Convert.ToBoolean(dTable.Rows[i]["PayCarFund"]);
                    lst.Add(dct);
                }

                return lst;
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
            }
            return lst;
        }

        public static DataTable GetSelectedRecords(string xmlDoc, string spName, ref string errorMessage)
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
        #endregion
    }
}
