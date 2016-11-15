using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Vinculum.Framework.Data;
using Vinculum.Framework.DataTypes;
using CoreComponent.BusinessObjects;
using CoreComponent.Core.BusinessObjects;
using System.Data.SqlClient;



namespace CoreComponent.BusinessObjects
{
    public class ExecuteInOut
    {

        #region Variable Declaration

        private string interfaceJobName = String.Empty;
        private string scheduledTaskName = String.Empty;
        private const string SP_INT_CONTROL_SEARCH = "Usp_Int_Interface_Control";
        string bussinessMonth, pcCode, chequeNo, status, createdDate, distributorId, header1, header2, header3 = String.Empty;

        #region Exportvariables

        public string Header1
        {
            get { return header1; }
            set { header1 = value; }
        }
        public string Header2
        {
            get { return header2; }
            set { header2 = value; }
        }
        public string Header3
        {
            get { return header3; }
            set { header3 = value; }
        }
        public string CreatedDate
        {
            get { return createdDate; }
            set { createdDate = value; }
        }

        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        public string ChequeNo
        {
            get { return chequeNo; }
            set { chequeNo = value; }
        }

        public string PcCode
        {
            get { return pcCode; }
            set { pcCode = value; }
        }

        public string DistributorId
        {
            get { return distributorId; }
            set { distributorId = value; }
        }

        public string BussinessMonth
        {
            get { return bussinessMonth; }
            set { bussinessMonth = value; }
        }
        #endregion
        #endregion

        #region SP Declaration

        private const string SP_EXECINOUT_JOB = "usp_ExecInOutJob";
        private const string SP_UPLOAD_DISTRIBUTORMASTER = "usp_UploadDistributorMaster";
        private const string SP_UPLOAD_CI = "usp_UploadCI";
        private const string SP_UPLOAD_PAYORDER = "usp_UploadPayOrder";
        private const string SP_UPLOAD_GIFTVOUCHER = "usp_UploadGiftVoucher";
        private const string SP_EXECUTESCHEDULEDTASK = "usp_ExecuteScheduledTask";

        #endregion

        #region Methods

        public static DataTable FTPServerPath()
        {
            try
            {
                string err = string.Empty;
                DataTable dTable = GetSelectedRecordsWithoutParameter(SP_INT_CONTROL_SEARCH, ref err);

                return dTable;
                /*
                string incrementalPath = string.Empty;
                if (dTable.Rows.Count > 0)
                {
                    incrementalPath = dTable.Rows[0]["FTPServer"].ToString();
                    userName = dTable.Rows[0]["UserName"].ToString();
                    password = dTable.Rows[0]["UserName"].ToString();
                }
                return incrementalPath;
                 */
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable GetSelectedRecordsWithoutParameter(string spName, ref string errorMessage)
        {
            DBParameterList dbParam;
            try
            {
                Vinculum.Framework.Data.DataTaskManager dtManager = new DataTaskManager();

                // initialize the parameter list object
                dbParam = new DBParameterList();

                // add the relevant 2 parameters

                // executing procedure to save the record 
                DataTable dt = dtManager.ExecuteDataTable(spName, dbParam);

                // update database message

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

        public bool ExecuteScheduler(ref string errorMessage)
        {
          try
            {
                DBParameterList dbParam;
                bool isSuccess = false;

                using (DataTaskManager dtManager = new DataTaskManager())
                {

                    dbParam = new DBParameterList();
                    dbParam.Add(new DBParameter("outParam", errorMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));
                    dtManager.ExecuteNonQuery(SP_EXECUTESCHEDULEDTASK, dbParam);
                    errorMessage = dbParam["outParam"].Value.ToString();
                    {
                        if (errorMessage.Trim().Length > 0)
                        {
                            isSuccess = false;
                        }
                        else
                        {
                            isSuccess = true;
                        }
                    }
                   
                }
                return isSuccess;

              //DataTable dtscheduledTask = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter("SCHEDULEDTASKNAME", 0, 0, 0));
              //if (dtscheduledTask != null && dtscheduledTask.Rows.Count == 2)
              //   scheduledTaskName = dtscheduledTask.Rows[1]["keyValue1"].ToString();
              //bool isSuccess = false;
              //TaskScheduler.ScheduledTasks objScheduledTasks = new TaskScheduler.ScheduledTasks();
              //TaskScheduler.Task objTask;
              //objTask = objScheduledTasks.OpenTask(scheduledTaskName);
              //objTask.Run();
              //objTask.Dispose();
              //isSuccess = true;
              //return isSuccess;                   
            }
          catch (Exception ex)
            {
                Common.LogException(ex);
                return false;
            }   

        }

        public bool ExecuteJob(ref string errorMessage)
        {
            try
            {
                DBParameterList dbParam;
                bool isSuccess = false;               
                
                using (DataTaskManager dtManager = new DataTaskManager())
                {

                    dbParam = new DBParameterList();
                    dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, errorMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));
                    dtManager.ExecuteNonQuery(SP_EXECINOUT_JOB, dbParam);
                    errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();
                    {
                        if (errorMessage.Trim().Length > 0)
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
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                return false;
            }

        }

        public bool UploadDistributorMaster(string path, ref string errorMessage)
        {
            try
            {
                
                bool isSuccess = false;               
                {
                    SqlTransaction dtTran = null;
                    SqlConnection conn = new SqlConnection();
                    conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BOSDB"].ToString();
                    DataSet dsResults = new DataSet();
                    SqlParameter paramPath = new SqlParameter("vPath", path);
                    SqlParameter paramOutParam = new SqlParameter("OutParam", SqlDbType.VarChar, 500);
                    paramOutParam.Direction = ParameterDirection.Output;

                    SqlCommand cmd = new SqlCommand(SP_UPLOAD_DISTRIBUTORMASTER, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(paramPath);
                    cmd.Parameters.Add(paramOutParam);
                    cmd.CommandTimeout = 9000;
                    conn.Open();

                    dtTran = conn.BeginTransaction();
                    cmd.Transaction = dtTran;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dsResults);
                    errorMessage = paramOutParam.Value.ToString();                   
                    {
                        if (errorMessage.Trim().ToUpper() != "SUCCESS")
                        {
                            isSuccess = false;
                            dtTran.Rollback();
                        }
                        else if (errorMessage.Trim().ToUpper() == "SUCCESS")
                        {
                            isSuccess = true;
                            dtTran.Commit();
                        }
                    }
                    conn.Close();
                    conn.Dispose();

                    return isSuccess;
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                return false;
            }
        }
        
        public bool UploadBonusCheque(string path, string fromDate, ref string errorMessage)
        {
            try
            {

                bool isSuccess = false;
                {
                    SqlTransaction dtTran = null;
                    SqlConnection conn = new SqlConnection();
                    conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BOSDB"].ToString();
                    DataSet dsResults = new DataSet();
                    SqlParameter paramPath = new SqlParameter("vPath", path);
                    SqlParameter paramFromDate = new SqlParameter("fromDate", fromDate);
                    SqlParameter paramOutParam = new SqlParameter("OutParam", SqlDbType.VarChar, 500);
                    paramOutParam.Direction = ParameterDirection.Output;

                    SqlCommand cmd = new SqlCommand(SP_UPLOAD_PAYORDER, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(paramPath);
                    cmd.Parameters.Add(paramFromDate);
                    cmd.Parameters.Add(paramOutParam);
                    cmd.CommandTimeout = 9000;
                    conn.Open();
                    dtTran = conn.BeginTransaction();
                    cmd.Transaction = dtTran;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dsResults);
                    errorMessage = paramOutParam.Value.ToString();
                    {
                        if (errorMessage.Trim().ToUpper() != "SUCCESS")
                        {
                            isSuccess = false;
                            dtTran.Rollback();
                        }
                        else if (errorMessage.Trim().ToUpper() == "SUCCESS")
                        {
                            isSuccess = true;
                            dtTran.Commit();
                        }
                    }
                    conn.Close();
                    conn.Dispose();

                    return isSuccess;
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                return false;
            }
        }

        public bool UploadGiftVoucher(string path, string fromDate, ref string errorMessage)
        {
            try
            {

                bool isSuccess = false;
                {
                    SqlTransaction dtTran = null;
                    SqlConnection conn = new SqlConnection();
                    conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BOSDB"].ToString();
                    DataSet dsResults = new DataSet();
                    SqlParameter paramPath = new SqlParameter("vPath", path);
                    SqlParameter paramFromDate = new SqlParameter("fromDate", fromDate);
                    SqlParameter paramOutParam = new SqlParameter("OutParam", SqlDbType.VarChar, 500);
                    paramOutParam.Direction = ParameterDirection.Output;

                    SqlCommand cmd = new SqlCommand(SP_UPLOAD_GIFTVOUCHER, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(paramPath);
                    cmd.Parameters.Add(paramFromDate);
                    cmd.Parameters.Add(paramOutParam);
                    cmd.CommandTimeout = 9000;
                    conn.Open();
                    dtTran = conn.BeginTransaction();
                    cmd.Transaction = dtTran;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dsResults);
                    errorMessage = paramOutParam.Value.ToString();
                    {
                        if (errorMessage.Trim().ToUpper() != "SUCCESS")
                        {
                            isSuccess = false;
                            dtTran.Rollback();
                        }
                        else if (errorMessage.Trim().ToUpper() == "SUCCESS")
                        {
                            isSuccess = true;
                            dtTran.Commit();
                        }
                    }
                    conn.Close();
                    conn.Dispose();

                    return isSuccess;
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                return false;
            }
        }

        public bool UploadCI(string path, int cash, int cheque, int bank, int ccard, string invConsDate,string cashFileNames, ref string errorMessage)
        {
            try
            {
                //DBParameterList dbParam;
                bool isSuccess = false;
                
                {
                    SqlTransaction dtTran = null;
                    SqlConnection conn = new SqlConnection();
                    conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BOSDB"].ToString();
                    DataSet dsResults = new DataSet();
                    SqlParameter paramPath = new SqlParameter("vPath", path);
                    SqlParameter paramIsProcessed = new SqlParameter("iIsProcessed",DbType.Int32);
                    paramIsProcessed.Value = 0;
                    SqlParameter paramCash = new SqlParameter("iCashFileCount", cash);
                    SqlParameter paramCheque = new SqlParameter("iChequeFileCount", cheque);
                    SqlParameter paramBank = new SqlParameter("iBankFileCount", bank);
                    SqlParameter paramCcard = new SqlParameter("iCCardFileCount", ccard);
                    SqlParameter paramInvConsDate = new SqlParameter("vInvoiceConsiderDate", invConsDate);
                    SqlParameter paramCashFileNames = new SqlParameter("vCashFileNames", cashFileNames);
                    SqlParameter paramOutParam = new SqlParameter("OutParam",SqlDbType.VarChar,500);
                    paramOutParam.Direction = ParameterDirection.Output;

                    SqlCommand cmd = new SqlCommand(SP_UPLOAD_CI, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(paramPath);
                    cmd.Parameters.Add(paramIsProcessed);
                    cmd.Parameters.Add(paramCash);
                    cmd.Parameters.Add(paramCheque);
                    cmd.Parameters.Add(paramBank);
                    cmd.Parameters.Add(paramCcard);
                    cmd.Parameters.Add(paramInvConsDate);
                    cmd.Parameters.Add(paramCashFileNames);
                    cmd.Parameters.Add(paramOutParam);
                    cmd.CommandTimeout = 9000;
                    conn.Open();

                    dtTran = conn.BeginTransaction();
                    cmd.Transaction = dtTran;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dsResults);
                    

                    errorMessage = paramOutParam.Value.ToString();
                                          
                    if (errorMessage.Trim().ToUpper() != "SUCCESS")
                    {                      
                        isSuccess = false;
                        dtTran.Rollback();
                    }
                    else if (errorMessage.Trim().ToUpper() == "SUCCESS")
                    {
                        isSuccess = true;
                        dtTran.Commit();
                    }
                    conn.Close();
                    conn.Dispose();
                    
                    return isSuccess;
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);                
                return false;
            }
        }
        
        #endregion


    }
}
