using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CoreComponent.Core.BusinessObjects;
using CoreComponent.MasterData.BusinessObjects;

using Vinculum.Framework.Data;
using Vinculum.Framework.DataTypes;

namespace TransfersComponent.BusinessObjects
{
    public class TransferOrder : ITransferOrder
    {
        #region SP Declaration
        private const string SP_TOI_CALCULATE_PRICE = "usp_TOICalculateTransferPrice";
        private const string SP_CALCULATE_PRICE = "usp_CalculateTransferPrice";
        #endregion
        #region variables
        int m_sourceLocationId, m_destinationLocationId, m_statusId;
        string m_sourceAddress, m_destinationAddress, m_statusName, m_tNumber;
        Int32 m_locationId;
        Int32 m_createdBy = Common.INT_DBNULL;
        Int32 m_modifiedBy = Common.INT_DBNULL;
        string m_createdDate = string.Empty;
        string m_modifiedDate = string.Empty;
        Int32 m_indexSeqNo = Common.INT_DBNULL;
        int m_indentised = Common.INT_DBNULL;
        int m_isexport = Common.INT_DBNULL;
        Boolean m_isexported;

        #endregion

        public TransferOrder()
        {
            Indentised = Common.INT_DBNULL;
            Isexport = Common.INT_DBNULL;
        }



        #region Property

        public int Indentised
        {
            get { return m_indentised; }
            set { m_indentised = value; }
        }
        public int Isexport
        {
            get { return m_isexport; }
            set { m_isexport = value; }
        }

        public Boolean Isexported
        {
            get { return m_isexported; }
            set { m_isexported = value; }
        }

        public System.Int32 IndexSeqNo
        {
            get { return m_indexSeqNo; }
            set { m_indexSeqNo = value; }
        }

        public Int32 LocationId
        {
            get { return m_locationId; }
            set { m_locationId = value; }
        }

        public System.Int32 StatusId
        {
            get { return m_statusId; }
            set { m_statusId = value; }
        }

        public System.Int32 SourceLocationId
        {
            get { return m_sourceLocationId; }
            set { m_sourceLocationId = value; }
        }

        public System.Int32 DestinationLocationId
        {
            get { return m_destinationLocationId; }
            set { m_destinationLocationId = value; }
        }

        public string StatusName
        {
            get { return m_statusName; }
            set { m_statusName = value; }
        }

        public string DestinationAddress
        {
            get { return m_destinationAddress; }
            set { m_destinationAddress = value; }
        }

        public string SourceAddress
        {
            get { return m_sourceAddress; }
            set { m_sourceAddress = value; }
        }

        public string TNumber
        {
            get { return m_tNumber; }
            set { m_tNumber = value; }
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

        public string CreatedDate
        {
            get { return m_createdDate; }
            set { m_createdDate = value; }
        }

        public string ModifiedDate
        {
            get { return m_modifiedDate; }
            set { m_modifiedDate = value; }
        }
        #endregion
        //AKASH
        public virtual decimal ToiCalculateTransferPrice(string PriceMode, string Percentage, string AppDpp, string itemCode, string locationId, ref string errorMessage)
        {
            DBParameterList dbParam;
            try
            {
                Vinculum.Framework.Data.DataTaskManager dtManager = new DataTaskManager();

                // initialize the parameter list object
                dbParam = new DBParameterList();

                // add the relevant 2 parameters
                dbParam.Add(new DBParameter("@PriceMode", PriceMode, DbType.String));
                dbParam.Add(new DBParameter("@Percentage", Percentage, DbType.String));
                dbParam.Add(new DBParameter("@AppDpp", AppDpp, DbType.String));
                dbParam.Add(new DBParameter("@ItemCode", itemCode, DbType.String));
                dbParam.Add(new DBParameter("@DestinationId", locationId, DbType.String));
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                // executing procedure to save the record 
                DataTable dt = dtManager.ExecuteDataTable(SP_TOI_CALCULATE_PRICE, dbParam);

                // update database message
                errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();

                // if an error returned from the database
                if (errorMessage != string.Empty)
                    return 0;
                else
                {
                    decimal price;
                    if (dt != null && dt.Rows.Count > 0)
                        price = Convert.ToDecimal(dt.Rows[0][0]);
                    else
                        price = 0;

                    return price;
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //AKASH


        public virtual decimal CalculateTransferPrice(bool modevalue, string itemCode, string locationId, ref string errorMessage)
        {
            DBParameterList dbParam;
            try
            {
                Vinculum.Framework.Data.DataTaskManager dtManager = new DataTaskManager();

                // initialize the parameter list object
                dbParam = new DBParameterList();

                // add the relevant 2 parameters
                dbParam.Add(new DBParameter("@Mode", modevalue, DbType.Boolean));
                dbParam.Add(new DBParameter("@ItemCode", itemCode, DbType.String));
                dbParam.Add(new DBParameter("@DestincationId", locationId, DbType.String));
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                // executing procedure to save the record 
                DataTable dt = dtManager.ExecuteDataTable(SP_CALCULATE_PRICE, dbParam);

                // update database message
                errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();

                // if an error returned from the database
                if (errorMessage != string.Empty)
                    return 0;
                else
                {
                    decimal price;
                    if (dt != null && dt.Rows.Count > 0)
                        price = Convert.ToDecimal(dt.Rows[0][0]);
                    else
                        price = 0;

                    return price;
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
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


        public virtual DataTable GetSelectedItems(string tNumber, int sourceAddressId, string spName, ref string errorMessage)
        {
            DBParameterList dbParam;
            try
            {
                Vinculum.Framework.Data.DataTaskManager dtManager = new DataTaskManager();

                // initialize the parameter list object
                dbParam = new DBParameterList();

                // add the relevant 2 parameters
                dbParam.Add(new DBParameter("@TNumber", tNumber, DbType.String));
                dbParam.Add(new DBParameter("@SourceAddressId", sourceAddressId, DbType.Int32));
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

        public virtual DataTable GetSelectedItems(string toNumber, string tNumber, int sourceAddressId, string spName, ref string errorMessage)
        {
            DBParameterList dbParam;
            try
            {
                Vinculum.Framework.Data.DataTaskManager dtManager = new DataTaskManager();

                // initialize the parameter list object
                dbParam = new DBParameterList();

                // add the relevant 2 parameters
                dbParam.Add(new DBParameter("@TONumber", toNumber, DbType.String));
                dbParam.Add(new DBParameter("@TNumber", tNumber, DbType.String));
                dbParam.Add(new DBParameter("@SourceAddressId", sourceAddressId, DbType.Int32));
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
                            this.TNumber = dt.Rows[0]["TNumber"].ToString();
                            this.ModifiedDate = (dt.Rows[0]["ModifiedDate"]).ToString();
                            this.IndexSeqNo = Convert.ToInt32(dt.Rows[0]["IndexSeqNo"]);
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

    }
}
