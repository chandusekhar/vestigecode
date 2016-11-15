using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreComponent.Core.BusinessObjects;
using Vinculum.Framework.Data;
using Vinculum.Framework.DataTypes;

using CoreComponent.BusinessObjects;
using System.Data;
namespace POSClient.BusinessObjects
{
    [Serializable]
    public class COLog
    {
        #region SP_Declaration
        private const string SP_LOG_SAVE = "usp_OrderLogSave";
        private const string SP_LOG_SEARCH = "usp_OrderLogSearch";

        #endregion
        public COLog()
        {           
            this.Address = new Address();
        }
   
        #region Property

        public string LogValue { get; set; }
        public string LogNo { get; set; }
        public Int32 LogType { get; set; }
        public string LogTypeName
        {
            get
            {
                return Enum.Parse(typeof(Common.COLogType), this.LogType.ToString()).ToString();
            }
            set
            {
                throw new NotImplementedException("This property is not intended to be implemented");
            }
        }

        public Int32 BOId { get; set; }
        public Int32 PCId { get; set; }
        public Int32 DistributorId { get; set; }
        public string DistributorName { get; set; }
        public string PCName { get; set; }        
        public Address Address { get; set; }
        public Int32 Status { get; set; }        
        public string StatusName
        {
            get
            {
                return Enum.Parse(typeof(Common.LogStatus), this.Status.ToString()).ToString();
            }
            set
            {
                throw new NotImplementedException("This property is not intended to be implemented");
            }
        }
        public int CreatedBy { get; set; }
        //public string LogDate { get; set; }

        //public string CreatedDate
        //{
        //    get { return Convert.ToDateTime(LogDate).ToString(Common.DTP_DATE_FORMAT) == "01-01-1900" ? string.Empty : Convert.ToDateTime(LogDate).ToString(Common.DTP_DATE_FORMAT); }
        //    set { throw new NotImplementedException("This Property can not be explicitly set"); }
        //}
        //public DateTime CreatedDate { get; set; }
        public string CreatedDate { get; set; }

        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public decimal LogOrderTotal { get; set; }
        public decimal LogInvoiceTotal { get; set; }
        public decimal LogCashTotal { get; set; }
        public decimal LogCreditCardTotal { get; set; }
        public decimal LogBankTotal { get; set; }
        public decimal LogBonusChequeTotal { get; set; }
        public decimal LogChequeTotal { get; set; }
        #endregion

        #region  Method

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

        public List<COLog> Search(int Status,int BOID,string LogNo,int LogType,int DistributorID,int PCID)
        {
            string errorMessage=string.Empty;
            List<COLog> LogList = new List<COLog>();
            System.Data.DataTable dTable = new DataTable();
            DBParameterList dbParam;
            try
            {
                Vinculum.Framework.Data.DataTaskManager dtManager = new DataTaskManager();
                // initialize the parameter list object
                dbParam = new DBParameterList();
                dbParam.Add(new DBParameter("@Status", Status, DbType.Int32));
                dbParam.Add(new DBParameter("@BOID", BOID, DbType.Int32));
                dbParam.Add(new DBParameter("@LogNo", LogNo, DbType.String));
                dbParam.Add(new DBParameter("@LogType", LogType, DbType.Int32));
                dbParam.Add(new DBParameter("@DistributorID", DistributorID, DbType.Int32));
                dbParam.Add(new DBParameter("@PCID", PCID, DbType.Int32));
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                // executing procedure to save the record 
                dTable = dtManager.ExecuteDataTable(SP_LOG_SEARCH, dbParam);

                // update database message
                errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();
                if (dTable != null && dTable.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow drow in dTable.Rows)
                    {
                        COLog log = CreateLOGObject(drow);
                        LogList.Add(log);
                    }
                }
                return LogList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private COLog CreateLOGObject(DataRow dr)
        {
            try
            {
                COLog log = new COLog();
                log.Address = new Address();
                log.LogValue = Convert.ToString(dr["LogValue"]);
                log.Address.Address1 = Convert.ToString(dr["Address1"]);
                log.Address.Address2 = Convert.ToString(dr["Address2"]);
                log.Address.Address3 = Convert.ToString(dr["Address3"]);
                log.Address.Address4 = Convert.ToString(dr["Address4"]);
                log.Address.CityId = Convert.ToInt32(dr["CityCode"]);
                log.Address.StateId = Convert.ToInt32(dr["StateCode"]);
                log.Address.CountryId = Convert.ToInt32(dr["CountryCode"]);
                log.Address.PhoneNumber1 = Convert.ToString(dr["Phone1"]);
                log.Address.Mobile1 = Convert.ToString(dr["Mobile1"]);
                log.Address.PinCode = Convert.ToString(dr["PinCode"]);
                log.Address.City = Convert.ToString(dr["CityName"]); ;
                log.Address.State = Convert.ToString(dr["StateName"]); ;
                log.Address.Country = Convert.ToString(dr["CountryName"]); ;

                log.CreatedBy = Convert.ToInt32(dr["CreatedBy"]);
                log.CreatedDate = Convert.ToDateTime(dr["CreatedDate"]).ToString(Common.DTP_DATE_FORMAT);
                //m_ModifiedDate.ToString(Common.DATE_TIME_FORMAT); dr["CreatedDate"].ToString(Common.DATE_TIME_FORMAT)
                log.LogType = Convert.ToInt32(dr["LogType"]);
                log.PCId = Convert.ToInt32(dr["PCId"]);
                log.BOId = Convert.ToInt32(dr["BOID"]);
                log.PCName = Convert.ToString(dr["PCName"]);
                log.DistributorId = Convert.ToInt32(dr["DistributorId"]);
                log.DistributorName = Convert.ToString(dr["DistributorName"]);
                log.LogNo = Convert.ToString(dr["LogNo"]);
                log.ModifiedBy = Convert.ToInt32(dr["ModifiedBy"]);
                log.ModifiedDate = Convert.ToDateTime(dr["ModifiedDate"]);
                log.Status = Convert.ToInt32(dr["Status"]);
                log.LogOrderTotal = Math.Round(Convert.ToDecimal(dr["LogOrderTotal"]),Common.DisplayAmountRounding,MidpointRounding.AwayFromZero);
                log.LogInvoiceTotal = Math.Round(Convert.ToDecimal(dr["LogInvoiceTotal"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                log.LogCashTotal = Math.Round(Convert.ToDecimal(dr["LogCashTotal"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                log.LogChequeTotal = Math.Round(Convert.ToDecimal(dr["LogChequeTotal"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                log.LogCreditCardTotal = Math.Round(Convert.ToDecimal(dr["LogCreditCardTotal"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                log.LogBonusChequeTotal = Math.Round(Convert.ToDecimal(dr["LogBonusChequeTotal"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                log.LogBankTotal = Math.Round(Convert.ToDecimal(dr["LogBankTotal"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);

                return log;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool Save(ref string dbMessage)
        {
            try
            {
                bool isSuccess = false;
                using (DataTaskManager dtManager = new DataTaskManager())
                {
                    DBParameterList dbParam;
                    dbParam = new DBParameterList();
                    dbParam.Add(new DBParameter(Common.PARAM_DATA, Common.ToXml(this), DbType.String));                    
                    dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, dbMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                    DataTable dt = dtManager.ExecuteDataTable(SP_LOG_SAVE, dbParam);                    
                    dbMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();

                    if (dt != null && string.IsNullOrEmpty(dbMessage))
                    {
                        isSuccess = true;
                        this.LogNo = dt.Rows[0]["LogNo"].ToString();                        
                    }
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
