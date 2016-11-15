using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vinculum.Framework.Data;
using Vinculum.Framework.DataTypes;
using CoreComponent.BusinessObjects;
using CoreComponent.Core.BusinessObjects;

namespace CoreComponent.BusinessObjects
{
    public class BonusMasterBatch
    {
        #region SP Declaration

        private const string SP_BONUSPOINTMASTER = "usp_BonusPointsMaster";

        #endregion

        #region METHODS

        public Boolean ProcessBatch(int iFrequency, int @bReProcessDaily,int iMode, ref String validationMessage, ref String errorMessage)
        {
            DBParameterList dbParam;
            bool isSuccess = false;
            using (DataTaskManager dtManager = new DataTaskManager())
            {               
                try
                {
                    dbParam = new DBParameterList();
                    dbParam.Add(new DBParameter("iFrequency", iFrequency, DbType.String));
                    dbParam.Add(new DBParameter("bReProcessDaily", bReProcessDaily, DbType.String));
                    dbParam.Add(new DBParameter("iMode", iMode, DbType.String));
                    dbParam.Add(new DBParameter(Common.PARAM_VALIDATIONMESSAGE, errorMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));
                    dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, errorMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));
                    int count = dtManager.ExecuteNonQuery(SP_BONUSPOINTMASTER, dbParam);
                    validationMessage = dbParam[Common.PARAM_VALIDATIONMESSAGE].Value.ToString();
                    errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();
                    {
                        if (errorMessage.Length > 0 || validationMessage.Length > 0)
                        {
                            isSuccess = false;                               
                        }
                        else
                        {                                
                            isSuccess = true;                               
                        }
                    }
                    return isSuccess;
                }
                catch (Exception ex)
                {                    
                    throw ex;
                }                
            }
        }
        
        #endregion
    }
}
