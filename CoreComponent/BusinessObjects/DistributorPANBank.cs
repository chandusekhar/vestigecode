using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vinculum.Framework.Data;
using Vinculum.Framework;
using Vinculum.Framework.DataTypes;
using System.Data;
using System.Data.SqlClient;
using CoreComponent.Core.BusinessObjects;

namespace CoreComponent.BusinessObjects
{
    public class DistributorPANBank
    {
        private byte[] panBankImage;
        private string distributorId;
        private int refId;

        public string DistributorId
        {
            get { return distributorId; }
            set { distributorId = value; }
        }
        public int RefId
        {
            get { return refId; }
            set { refId = value; }
        }
        
        public byte[] PanBankImage
        {
            get { return panBankImage; }
            set { panBankImage = value; }
        }
        public int ModifiedBy
        {
            get;
            set;
        }
        
        public void SetDistributor(string distributorid)
        {
            distributorId = distributorid;
            
        }
        
        public DataTable Search(string Distributorid,String Type,string endDate,string strPanBankNo,Int64 batchno, string spName, ref String errorMessage)
        {
            DBParameterList dbParam;
            DataTable dt;
            using (DataTaskManager dtManager = new DataTaskManager())
            {
                
                dbParam = new DBParameterList();
                dbParam.Add(new DBParameter("@distributorid",Distributorid,DbType.String));
                dbParam.Add(new DBParameter("@Type", Type, DbType.String));
                dbParam.Add(new DBParameter("@EndDate", endDate, DbType.String));
                dbParam.Add(new DBParameter("@PANBankNo", strPanBankNo, DbType.String));
                dbParam.Add(new DBParameter("@BatchNo", batchno, DbType.Int64));
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT,errorMessage,DbType.String,ParameterDirection.Output,Common.PARAM_OUTPUT_LENGTH ));
                dt=dtManager.ExecuteDataTable(spName,dbParam);                

                errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();
                 {
                    if (errorMessage.Length > 0)
                    {
                        //isSuccess = false;
                    
                    }
                    else
                    {
                       //isSuccess = true;
                    }
                 }
            }
            return dt;
        }
        public Boolean Save(string Distributorid,string type,string panBankno,string BankName,string Ifsccode,byte[] imagePan, Int32  modifiedBy,String spName, ref String errorMessage)
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
                        dbParam.Add(new DBParameter("@inputParam1", Distributorid, DbType.String));
                        dbParam.Add(new DBParameter("@inputParam2", imagePan, DbType.Binary));
                        dbParam.Add(new DBParameter("@inputParam3", type, DbType.String));
                        dbParam.Add(new DBParameter("@inputParam4", panBankno, DbType.String));
                        dbParam.Add(new DBParameter("@inputParam5", BankName, DbType.String));
                        dbParam.Add(new DBParameter("@inputParam6", Ifsccode, DbType.String));
                        dbParam.Add(new DBParameter("@inputParam7", modifiedBy, DbType.Int32));
                        dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, errorMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                        DataTable dt = dtManager.ExecuteDataTable(spName, dbParam);

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
                        return isSuccess;
                    }
                    catch (Exception ex)
                    {
                        dtManager.RollbackTransaction();
                        throw ex;
                    }
                }
            }
        }
    }
}
