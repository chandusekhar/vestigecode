using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Vinculum.Framework.Data;
using Vinculum.Framework.DataTypes;

namespace CoreComponent.BusinessObjects
{
    public class DistributorResignation
    {
        public const string DISTRIBUTOR_RESIGNATION = "usp_DistributorResign";
        private int m_DistributorId;

        public int DistributorId
        {
            get { return m_DistributorId; }
            set {m_DistributorId = value;}
        }
        public bool Resign(int DistributorId, string spName, ref string errMsg)
        {
            bool isSuccess = false;
            try
            {
                using (DataTaskManager dtManager = new DataTaskManager())
                {

                    //Declare and initialize the parameter list object
                    DBParameterList dbParam = new DBParameterList();

                    //Add the relevant 2 parameters
                    dbParam.Add(new DBParameter("@DistributorId", DistributorId, DbType.Int32));
                    
                       // System.Data.ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                    try
                    {
                        //Begin the transaction and executing procedure to save the record(s) 
                        //dtManager.BeginTransaction();

                        // executing procedure to save the record 
                        dtManager.ExecuteNonQuery(spName, dbParam);
                        isSuccess = true;
                        //Update database message
                        //errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();

                        //If an error returned from the database
                        //if (errorMessage.Length > 0)
                        //{
                        //    dtManager.RollbackTransaction();
                        //    isSuccess = false;
                        //}
                        //else
                        //{
                        //    dtManager.CommitTransaction();
                        //    isSuccess = true;
                        //}
                    }
                    catch (Exception ex)
                    {
                        //dtManager.RollbackTransaction();
                        isSuccess = false;
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isSuccess;
        }
    }
}
