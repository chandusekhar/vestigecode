using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Vinculum.Framework.Data;
using Vinculum.Framework.DataTypes;
using CoreComponent.Core.BusinessObjects;

namespace CoreComponent.BusinessObjects
{
   public  class DistributorEditHistoryLog
    {
       public const string SP_DISTRIBUTOR_REGISTER_LOG = "Usp_DistributorEditHistory_Save";

        public List<DistributorEditHistory> DistributorEditHistory
        {
            get;
            set;
        }

        public bool UpdateDistributorLOG(List<DistributorEditHistory> objeDesitributorEditHistory)
        {
            
            this.DistributorEditHistory = objeDesitributorEditHistory;
 
            string dbErrorMessage = "";
            using (DataTaskManager dt = new DataTaskManager())
            {

                DBParameterList paramList = new DBParameterList();
                paramList.Add(new DBParameter(Common.PARAM_DATA, Common.ToXml(this), DbType.String));
                paramList.Add(new DBParameter(Common.PARAM_OUTPUT, dbErrorMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                dt.ExecuteNonQuery(SP_DISTRIBUTOR_REGISTER_LOG, paramList);
                //dbValidationMessage = paramList["@validationMessage"].Value.ToString();
                dbErrorMessage = paramList[Common.PARAM_OUTPUT].Value.ToString();
                //distributorSerialNo = paramList["@DistributorSerialNo"].Value.ToString();
                /*if (string.IsNullOrEmpty(dbValidationMessage) && string.IsNullOrEmpty(dbErrorMessage) && distributorSerialNo.Length > 0)
                {
                    returnValue = true;
                }*/
                return true;
            }
        }


    }
}
