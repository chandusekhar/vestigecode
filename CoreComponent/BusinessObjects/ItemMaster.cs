using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreComponent.Core.BusinessObjects;
using CoreComponent.BusinessObjects;
using CoreComponent.MasterData.BusinessObjects;
using Vinculum.Framework.Data;
using Vinculum.Framework;
using Vinculum.Framework.DataTypes;
using System.Data;
using System.Data.SqlClient;

namespace CoreComponent.BusinessObjects
{
    public class ItemMaster : IItemMaster
    {
        #region Variables
        Int32 m_itemId = Common.INT_DBNULL;
        String m_itemCode = String.Empty;
        String m_itemName = string.Empty;
        Int32 m_status = Common.INT_DBNULL;
        String m_statusName = string.Empty;
        Int32 m_createdBy = Common.INT_DBNULL;
        Int32 m_modifiedBy = Common.INT_DBNULL;
        DateTime m_createdDate = Common.DATETIME_NULL;
        DateTime m_modifiedDate = Common.DATETIME_NULL;
        Int32 m_ToitemId = Common.INT_DBNULL;
        String m_ToitemCode = String.Empty;
        String m_ToitemName = string.Empty;
        //double m_PrimaryCost = 0;
        #endregion

        #region Properties

        public Int32 ToItemId
        {
            get { return m_ToitemId; }
            set { m_ToitemId = value; }
        }

        public String ToItemCode
        {
            get { return m_ToitemCode; }
            set { m_ToitemCode = value; }
        }

        public String ToItemName
        {
            get { return m_ToitemName; }
            set { m_ToitemName = value; }
        }


        public Int32 ItemId
        {
            get { return m_itemId; }
            set { m_itemId = value; }
        }

        public String ItemCode
        {
            get { return m_itemCode; }
            set { m_itemCode = value; }
        }

        public String ItemName
        {
            get { return m_itemName; }
            set { m_itemName = value; }
        }

        public Int32 Status
        {
            get { return m_status; }
            set { m_status = value; }
        }

        public String StatusName
        {
            get { return m_statusName; }
            set { m_statusName = value; }
        }

        public Int32 CreatedBy
        {
            get { return m_createdBy; }
            set { m_createdBy = value; }
        }

        public Int32 ModifiedBy
        {
            get { return m_modifiedBy; }
            set { m_modifiedBy = value; }
        }

        public DateTime CreatedDate
        {
            get { return m_createdDate; }
        }

        public DateTime ModifiedDate
        {
            get { return m_modifiedDate; }
            set { m_modifiedDate = value; }
        }
        //public double PrimaryCost 
        //{ 
        //    get{ return m_PrimaryCost; }
        //    set{ value = m_PrimaryCost;} 
        //}
        #endregion

        public Boolean Save(String xmlDoc, String spName, ref String errorMessage)
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
        #region Shopping Cart
        public DataTable GetItemImage(int itemId, string spName)
        {
            DBParameterList dbParam;
            try
            {
                Vinculum.Framework.Data.DataTaskManager dtManager = new DataTaskManager();

                // initialize the parameter list object
                dbParam = new DBParameterList();

                // add the relevant 2 parameters
                dbParam.Add(new DBParameter("itemCode", itemId, DbType.Int32));
               
                // executing procedure to save the record 
                DataTable dt = dtManager.ExecuteDataTable(spName, dbParam);
               
                return dt;
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
        public DataSet GetSelectedRecordsInDataSet(string xmlDoc, string spName, ref string errorMessage)
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
                DataSet ds = dtManager.ExecuteDataSet(spName, dbParam);

                // update database message
                errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();

                // if an error returned from the database
                if (errorMessage != string.Empty)
                    return null;
                else
                {
                    return ds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetSelectedRecordsInDataTable(string xmlDoc, string spName, ref string errorMessage)
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
