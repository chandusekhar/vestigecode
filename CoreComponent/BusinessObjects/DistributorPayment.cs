using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreComponent.Core.BusinessObjects;
using System.Data;
using Vinculum.Framework.Data;
using Vinculum.Framework.DataTypes;

namespace CoreComponent.BusinessObjects
{
    [Serializable]
    public class DistributorPayment
    {
        #region SP Declaration
        private const string SP_DISTRIBUTOR_COLUMN_NAME_VALUE = "usp_GetDistributorColumnValue";
        private const string SP_DISTRIBUTOR_PAYMENT_SAVE = "usp_DistributorPaymentSave";
        private const string SP_DISTRIBUTOR_TRAVEL_FUND_SAVE = "usp_DistributorTravelFundSave";
        private const string SP_DISTRIBUTOR_TRAVEL_FUND_INFO = "usp_GetDistributorTravelFundInfo";
        
        #endregion

        #region Property
        private string m_month = string.Empty;
        private string m_year = string.Empty;

        private decimal m_accAmount = 0;
        private decimal m_carryFwdAmount = 0;

        private string m_distributorName = string.Empty;
        private string m_columnName = string.Empty;
        private string m_distributorId = string.Empty;
        private string m_bonusId = string.Empty;
        private string m_amount = string.Empty;
        private string m_chequeNo = string.Empty;
        private string m_paymentDate = string.Empty;
        private string m_bankName = string.Empty;
        private string m_chequeIssueDate = string.Empty;
        private string m_chequeExpiryDate = string.Empty;
        private string m_firstName = string.Empty;
        private string m_lastName = string.Empty;
        private string m_businessMonth = string.Empty;

        public decimal DisplayAmount
        {
            get { return Math.Round(DBAmount, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
        public decimal DBAmount
        {
            get { return Math.Round(Convert.ToDecimal(Amount), Common.DBAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
        public decimal DisplayAccAmount
        {
            get { return Math.Round(DBAccAmount, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
        public decimal DBAccAmount
        {
            get { return Math.Round(AccAmount, Common.DBAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
        public decimal DisplayCarryFwdAmount
        {
            get { return Math.Round(DBCarryFwdAmount, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
        public decimal DBCarryFwdAmount
        {
            get { return Math.Round(CarryFwdAmount, Common.DBAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
        public decimal AccAmount
        {
            get { return m_accAmount; }
            set { m_accAmount = value; }
        }
        public decimal CarryFwdAmount
        {
            get { return m_carryFwdAmount; }
            set { m_carryFwdAmount = value; }
        }

        public string DistributorName
        {
            get { return m_distributorName; }
            set { m_distributorName = value; }
        }

        public string Month
        {
            get { return m_month; }
            set { m_month = value; }
        }
        public string Year
        {
            get { return m_year; }
            set { m_year = value; }
        }

        public string DistributorId
        {
            get { return m_distributorId; }
            set { m_distributorId = value; }
        }
        public string BonusId
        {
            get { return m_bonusId; }
            set { m_bonusId = value; }
        }
        public string Amount
        {
            get { return m_amount; }
            set { m_amount = value; }
        }
        public string ChequeNo
        {
            get { return m_chequeNo; }
            set { m_chequeNo = value; }
        }
        public string PaymentDate
        {
            get { return m_paymentDate; }
            set { m_paymentDate = value; }
        }
        public string BankName
        {
            get { return m_bankName; }
            set { m_bankName = value; }
        }
        public string ChequeIssueDate
        {
            get { return m_chequeIssueDate; }
            set { m_chequeIssueDate = value; }
        }
        public string ChequeExpiryDate
        {
            get { return m_chequeExpiryDate; }
            set { m_chequeExpiryDate = value; }
        }
        public string FirstName
        {
            get { return m_firstName; }
            set { m_firstName = value; }
        }
        public string LastName
        {
            get { return m_lastName; }
            set { m_lastName = value; }
        }
        public string BusinessMonth
        {
            get { return m_businessMonth; }
            set { m_businessMonth = value; }
        }

        public string DisplayBusinessMonth
        {
            get 
            { 
                if(!String.IsNullOrEmpty(BusinessMonth))
                    return (Convert.ToDateTime(BusinessMonth)).ToString("MM-yyyy"); 
                else
                    return DateTime.Now.ToString("MM-yyyy"); 
            }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }


        public string Column_Name
        {
            get { return m_columnName; }
            set { m_columnName = value; }
        }

        #endregion

        #region Methods
        public static bool Save(String xmlDoc, ref String errorMessage)
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

                        DataTable dt = dtManager.ExecuteDataTable(SP_DISTRIBUTOR_PAYMENT_SAVE, dbParam);

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
                       
                    }
                    catch (Exception ex)
                    {
                        dtManager.RollbackTransaction();
                        throw ex;
                    }
                    return isSuccess;
                }
            }
        }

        public static bool TravelFundSave(String xmlDoc, ref string errorMessage)
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

                        DataTable dt = dtManager.ExecuteDataTable(SP_DISTRIBUTOR_TRAVEL_FUND_SAVE, dbParam);

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

                    }
                    catch (Exception ex)
                    {
                        dtManager.RollbackTransaction();
                        throw ex;
                    }
                    return isSuccess;
                }
            }
        }

        public static List<DistributorPayment> GetDistributorTravelFund(String xmlDoc)
        {
          
            List<DistributorPayment> lst = new List<DistributorPayment>();
            try
            {
                DataTable dTable = GetDistributorTravelFundInfo(xmlDoc);
                for (int i = 0; i < dTable.Rows.Count; i++)
                {
                    DistributorPayment dp = new DistributorPayment();
                    dp.Amount = dTable.Rows[i]["Amount"].ToString();
                    dp.DistributorId = dTable.Rows[i]["DistributorId"].ToString();
                    dp.DistributorName = dTable.Rows[i]["DistributorName"].ToString();
                    dp.Amount =dTable.Rows[i]["Amount"].ToString();
                    dp.AccAmount = Convert.ToDecimal(dTable.Rows[i]["AccAmount"].ToString());// -Convert.ToDecimal(dTable.Rows[i]["CarryFwdAmount"].ToString());
                    lst.Add(dp);
                }
                return lst;
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
            }
            return lst;
        }

        public static DataTable GetDistributorTravelFundInfo(String xmlDoc)
        {
            System.Data.DataTable dTable = new DataTable();
            try
            {
                string errorMessage = string.Empty;
                DistributorPayment dp = new DistributorPayment();
                dTable = GetSelectedRecords(xmlDoc, SP_DISTRIBUTOR_TRAVEL_FUND_INFO, ref errorMessage);

                return dTable;
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
            }
            return dTable;
        }


        public static DataTable GetDistributorPaymentColumnValue(String xmlDoc)
        {
            System.Data.DataTable dTable = new DataTable();
            try
            {
                string errorMessage = string.Empty;
                DistributorPayment dp = new DistributorPayment();
                dTable = GetSelectedRecords(xmlDoc, SP_DISTRIBUTOR_COLUMN_NAME_VALUE, ref errorMessage);

                return dTable;
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
            }
            return dTable;
        }


        public static List<DistributorPayment> GetDistributorPaymentColumn()
        {
            List<DistributorPayment> locList = new List<DistributorPayment>();
            try
            {
                string errorMessage = string.Empty;
                
                    DistributorPayment loc = new DistributorPayment();
                    loc.Column_Name = "Distributor Id";
                    locList.Add(loc);

                    loc = new DistributorPayment();
                    loc.Column_Name = "Amount";
                    locList.Add(loc);

                    loc = new DistributorPayment();
                    loc.Column_Name = "Bonus Id";
                    locList.Add(loc);

                    loc = new DistributorPayment();
                    loc.Column_Name = "Business Month";
                    locList.Add(loc);

                    loc = new DistributorPayment();
                    loc.Column_Name = "Cheque No.";
                    locList.Add(loc);

                    loc = new DistributorPayment();
                    loc.Column_Name = "Payment Date";
                    locList.Add(loc);

                    loc = new DistributorPayment();
                    loc.Column_Name = "Bank Name";
                    locList.Add(loc);

                    loc = new DistributorPayment();
                    loc.Column_Name = "Chq. Issue Date";
                    locList.Add(loc);
                
                    loc = new DistributorPayment();
                    loc.Column_Name = "Chq. Expiry Date";
                    locList.Add(loc);

                    loc = new DistributorPayment();
                    loc.Column_Name = "First Name";
                    locList.Add(loc);

                    loc = new DistributorPayment();
                    loc.Column_Name = "Last Name";
                    locList.Add(loc);

            

                return locList;
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
            }
            return locList;
        }

        public static DataTable GetSelectedRecords(string xmlDoc, string spName, ref string errorMessage)
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
        #endregion
    }

    public class DistributorBusinessMonth
    {
        private string m_month = string.Empty;
        private string m_year = string.Empty;

        public string Month
        {
            get { return m_month; }
            set { m_month = value; }
        }
        public string Year
        {
            get { return m_year; }
            set { m_year = value; }
        }

    }

}
