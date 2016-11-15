using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CoreComponent.Core.BusinessObjects;

using Vinculum.Framework.Data;
using Vinculum.Framework.DataTypes;

namespace InventoryComponent.BusinessObjects
{
    public class Inventory
    {
        public int ApprovedBy
        {
            get;
            set;
        }
        public string ApprovedDate
        {
            get;
            set;
        }
        public string DisplayApprovedDate
        {
            get
            {
                if (ApprovedDate != null && ApprovedDate.ToString().Length > 0)
                    return Convert.ToDateTime(ApprovedDate).ToString(Common.DTP_DATE_FORMAT);
                else
                    return string.Empty;
            }
            set { ApprovedDate = value; }
        }
        public string ApprovedName
        {
            get;
            set;
        }
      
        public string FromDate
        {
            get;
            set;
        }

        public string ToDate
        {
            get;
            set;
        }

        public int StatusId
        {
            get;
            set;
        }
        public string SeqNo
        {
            get;
            set;
        }
        public string StatusName
        {
            get;
            set;
        }
        public string ModifiedDate
        {
            get;
            set;
        }
        public int ModifiedBy
        {
            get;
            set;
        }
        public string CreatedDate
        {
            get;
            set;
        }
        public int CreatedBy
        {
            get;
            set;
        }
        public int LocationId
        {
            get;
            set;
        }
        public int SourceLocationId
        {
            get;
            set;
        }
        public string LocationName
        {
            get;
            set;
        }
        public string LocationAddress
        {
            get;
            set;
        }
        public int Isexport
        {
            get;
            set;
        }
        public int InterbatchAdj
        {
            get;
            set;
        }
      
        public virtual DataSet GetSelectedItems(string seqNo, string spName, ref string errorMessage)
        {
            DBParameterList dbParam;
            try
            {
                Vinculum.Framework.Data.DataTaskManager dtManager = new DataTaskManager();

                // initialize the parameter list object
                dbParam = new DBParameterList();

                // add the relevant 2 parameters
                dbParam.Add(new DBParameter("@SeqNo", seqNo, DbType.String));
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                // executing procedure to save the record 
                DataSet dt = dtManager.ExecuteDataSet(spName, dbParam);

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
                        if (errorMessage != string.Empty)
                        {
                            isSuccess = false;
                            dtManager.RollbackTransaction();
                        }
                        else
                        {
                            this.SeqNo = dt.Rows[0]["SeqNo"].ToString();
                            this.ModifiedDate = (dt.Rows[0]["ModifiedDate"]).ToString();
                            dtManager.CommitTransaction();
                            isSuccess = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        Common.LogException(ex);
                        dtManager.RollbackTransaction();
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
    }
}
