using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Vinculum.Framework.Data;
using Vinculum.Framework.DataTypes;
using CoreComponent.BusinessObjects;
using CoreComponent.Core.BusinessObjects;

namespace CoreComponent.Hierarchies.BusinessObjects
{
    [Serializable]
    public class Hierarchy : Address, IHierarchy
    {
        private int m_hierarchyId, m_hierarchyType, m_hierarchyLevel;
        private int m_parentHierarchyId = Common.INT_DBNULL;
        private string m_hierarchyCode, m_hierarchyName, m_parentHierarchyCode, m_parentHierarchyName;
        private string m_modifiedDate = new DateTime(1900, 1, 1).ToString(Common.DATE_TIME_FORMAT);
        private string m_statusValue;
        #region Property

        public int ParentHierarchyId
        {
            get { return m_parentHierarchyId; }
            set { m_parentHierarchyId = value; }
        }

        public int HierarchyType
        {
            get { return m_hierarchyType; }
            set { m_hierarchyType = value; }
        }

        public int HierarchyId
        {
            get { return m_hierarchyId; }
            set { m_hierarchyId = value; }
        }

        public int HierarchyLevel
        {
            get { return m_hierarchyLevel; }
            set { m_hierarchyLevel = value; }
        }
        
        public string HierarchyCode
        {
            get { return m_hierarchyCode; }
            set { m_hierarchyCode = value; }
        }

        public string StatusValue
        {
            get { return m_statusValue; }
            set { m_statusValue = value; }
        }

        public string HierarchyName
        {
            get { return m_hierarchyName; }
            set { m_hierarchyName = value; }
        }

        public string ParentHierarchyName
        {
            get { return m_parentHierarchyName; }
            set { m_parentHierarchyName = value; }
        }

        public string ParentHierarchyCode
        {
            get { return m_parentHierarchyCode; }
            set { m_parentHierarchyCode = value; }
        }

        public string ModifiedDate
        {
            get { return Convert.ToDateTime(m_modifiedDate).ToString(Common.DATE_TIME_FORMAT); }
            set { m_modifiedDate = value; }
        }

        #endregion

        #region Methods

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
                        if ((errorMessage != string.Empty) && (!errorMessage.Equals("INF0030")))
                        {
                            isSuccess = false;
                            dtManager.RollbackTransaction();
                        }
                        else
                        {
                            this.HierarchyId = Convert.ToInt32(dt.Rows[0]["HierarchyId"]);
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

        public virtual List<IHierarchy> GetUplineChain(string xmlDoc, string spName, ref string errorMessage)
        {
            return null;
        }

        public virtual List<IHierarchy> GetDownlineChain(string xmlDoc, string spName, ref string errorMessage)
        {
            return null;
        }

        public virtual List<IHierarchy> GetCompleteChain(string xmlDoc, string spName, ref string errorMessage)
        {
            return null;
        }

        public virtual IHierarchy GetHierarchyRecord(string xmlDoc, string spName, ref string errorMessage)
        {
            return null;
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

        public virtual DataTable GetConfigRecords(string spName, ref string errorMessage)
        {
            DBParameterList dbParam;
            try
            {
                Vinculum.Framework.Data.DataTaskManager dtManager = new DataTaskManager();

                // initialize the parameter list object
                dbParam = new DBParameterList();

                // add the relevant 1 parameters
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


        public virtual DataTable GetContactRecords(string spName, int hierarchyId, ref string errorMessage)
        {
            DBParameterList dbParam;
            try
            {
                Vinculum.Framework.Data.DataTaskManager dtManager = new DataTaskManager();

                // initialize the parameter list object
                dbParam = new DBParameterList();

                // add the relevant 2 parameters
                dbParam.Add(new DBParameter(Common.HIERARCHY_ID, hierarchyId, DbType.Int32));
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
