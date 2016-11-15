using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vinculum.Framework.Data;
using Vinculum.Framework.DataTypes;
using System.Data;
using CoreComponent.Core.BusinessObjects;

namespace CoreComponent.BusinessObjects
{
    [Serializable]
    public class CarRegistration : ICarRegistration
    {
        #region Constants

        public const string CAR_REGISTRATION_SAVE = "usp_CarBonusSave";
        public const string CAR_REGISTRATION_PARTPAYMENT_SAVE = "usp_CarBonusPartPaymentSave";
        public const string CAR_REGISTRATION_SEARCH = "usp_CarBonusAndRegistrationInfoSearch";
        public const string CAR_DISTRIBUTOR_BALANCE_AMOUNT = "usp_DistributorBalanceAmount";
        public const string CAR_DISTRIBUTOR_DETAIL = "usp_CarDistributorDetail";
        public const string CAR_DIST_SEARCH = "usp_DistributorExistSearch";
        public const string CAR_REGIS_NO_SEARCH = "usp_CarRegistrationNoSearch";
        public const string CAR_GET_BONUS_REPORT = "usp_CarBonusReport";
        #endregion

        #region Properties
        private int m_distributorId;

        public int DistributorId
        {
            get { return m_distributorId; }
            set { m_distributorId = value; }
        }
        private Decimal m_CarValue;

        public Decimal CarValue
        {
            get { return m_CarValue; }
            set { m_CarValue = value; }
        }
        private Decimal m_FirstPayoutValue;

        public Decimal FirstPayoutValue
        {
            get { return m_FirstPayoutValue; }
            set { m_FirstPayoutValue = value; }
        }
        private Decimal m_MaximumCarValue;

        public Decimal MaximumCarValue
        {
            get { return m_MaximumCarValue; }
            set { m_MaximumCarValue = value; }
        }
        private string m_CarNumber;

        public String CarNumber
        {
            get { return m_CarNumber; }
            set { m_CarNumber = value; }
        }
        private string m_CarPurchaseDate;

        public string CarPurchaseDate
        {
            get { return m_CarPurchaseDate; }
            set { m_CarPurchaseDate = value; }
        }
        private string m_FirstPayoutBusinessMonth;

        public string FirstPayoutBusinessMonth
        {
            get { return m_FirstPayoutBusinessMonth; }
            set { m_FirstPayoutBusinessMonth = value; }
        }

        private bool m_IsFirstPayout;
        public bool IsFirstPayout
        {
            get { return m_IsFirstPayout; }
            set { m_IsFirstPayout = value; }
        }

        private List<CarRegistrationDetails> m_CarRegisDetails;

        public List<CarRegistrationDetails> CarRegisDetails
        {
            get { return m_CarRegisDetails; }
            set { m_CarRegisDetails = value; }
        }

        private List<CarBonusPartPayment> m_CarBonusPartPayment;

        public List<CarBonusPartPayment> CarBonusPartPayment
        {
            get { return m_CarBonusPartPayment; }
            set { m_CarBonusPartPayment = value; }
        }

        private List<CarRegistration> m_CarRegisHeaders;

        public List<CarRegistration> CarRegisHeaders
        {
            get { return m_CarRegisHeaders; }
            set { m_CarRegisHeaders = value; }
        }

        private List<CarRegistration> m_CarRegisSearchList;

        public List<CarRegistration> CarRegisSearchList
        {
            get { return m_CarRegisSearchList; }
            set { m_CarRegisSearchList = value; }
        }

        private DateTime m_FirstCarPurDate;
        private DateTime m_FirstPayoutDate;

        public DateTime FirstCarPurDate
        {
            get { return m_FirstCarPurDate; }
            set { m_FirstCarPurDate = value; }
        }

        public DateTime FirstPayoutDate
        {
            get { return m_FirstPayoutDate; }
            set { m_FirstPayoutDate = value; }
        }
        //private System.Int32 m_createdBy;

        //public System.Int32 CreatedBy
        //{
        //    get { return m_createdBy; }
        //    set { m_createdBy = value; }
        //}

        //private System.String m_createdDate;

        //public System.String CreatedDate
        //{
        //    get { return m_createdDate; }
        //    set { m_createdDate = value; }
        //}

        //private System.Int32 m_modifiedBy;

        //public System.Int32 ModifiedBy
        //{
        //    get { return m_modifiedBy; }
        //    set { m_modifiedBy = value; }
        //}

        //private System.String m_modifiedDate;

        //public System.String ModifiedDate
        //{
        //    get { return m_modifiedDate; }
        //    set { m_modifiedDate = value; }
        //}

        private string m_CarPurchaseDateFrom;

        public string CarPurchaseDateFrom
        {
            get { return m_CarPurchaseDateFrom; }
            set { m_CarPurchaseDateFrom = value; }
        }

        private string m_CarPurchaseDateTo;

        public string CarPurchaseDateTo
        {
            get { return m_CarPurchaseDateTo; }
            set { m_CarPurchaseDateTo = value; }
        }


        private string m_FirstPayoutBusinessMonthFrom;

        public string FirstPayoutBusinessMonthFrom
        {
            get { return m_FirstPayoutBusinessMonthFrom; }
            set { m_FirstPayoutBusinessMonthFrom = value; }
        }

        private string m_FirstPayoutBusinessMonthTo;

        public string FirstPayoutBusinessMonthTo
        {
            get { return m_FirstPayoutBusinessMonthTo; }
            set { m_FirstPayoutBusinessMonthTo = value; }
        }

       


        private string m_DistributorName;

        public string DistributorName
        {
            get { return m_DistributorName; }
            set { m_DistributorName = value; }
        }


        private string m_CummulativeCF;

        public string CummulativeCF
        {
            get { return m_CummulativeCF; }
            set { m_CummulativeCF = value; }
        }


        private string m_PaidAmount;

        public string PaidAmount
        {
            get { return m_PaidAmount; }
            set { m_PaidAmount = value; }
        }

        private string m_BalanceAmount;

        public string BalanceAmount
        {
            get { return m_BalanceAmount; }
            set { m_BalanceAmount = value; }
        }

        #endregion

        #region ICarRegistration Members

        public bool CarRegistrationSave(string xmlDoc, string spName, ref string errorMessage)
        {
            bool isSuccess = false;
            try
            {
                using (DataTaskManager dtManager = new DataTaskManager())
                {

                    //Declare and initialize the parameter list object
                    DBParameterList dbParam = new DBParameterList();

                    //Add the relevant 2 parameters
                    dbParam.Add(new DBParameter(Common.PARAM_DATA, xmlDoc, DbType.String));
                    dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String,
                        System.Data.ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                    try
                    {
                        //Begin the transaction and executing procedure to save the record(s) 
                        dtManager.BeginTransaction();

                        // executing procedure to save the record 
                        dtManager.ExecuteNonQuery(spName, dbParam);

                        //Update database message
                        errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();

                        //If an error returned from the database
                        if (errorMessage.Length > 0)
                        {
                            dtManager.RollbackTransaction();
                            isSuccess = false;
                        }
                        else
                        {
                            dtManager.CommitTransaction();
                            isSuccess = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        dtManager.RollbackTransaction();
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
        /// <summary>
        /// Return balance amount for a distributor
        /// </summary>
        /// <param name="intDistributorID"></param>
        /// <returns></returns>
        public static DataSet fnBalanceAmount(int intDistributorID, string spName)
        {
            DataSet dsBalanceAmount;
            try
            {
                using (DataTaskManager dtManager = new DataTaskManager())
                {
                    //Declare and initialize the parameter list object.
                    DBParameterList dbParam = new DBParameterList();

                    //Add the relevant 2 parameters
                    dbParam.Add(new DBParameter(Common.PARAM_DATA, intDistributorID, DbType.Int32));
                    dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String,
                    ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));
                    dsBalanceAmount = dtManager.ExecuteDataSet(spName, dbParam);                  
                }
            }
            catch { throw; }
            return dsBalanceAmount; 
        }

        public CarRegistration CarRegistrationSearch(string xmlDoc, string spName, ref string errorMessage)
        {
            CarRegistration carReg = new CarRegistration();
            carReg.CarRegisSearchList = new List<CarRegistration>();
            carReg.CarRegisHeaders = new List<CarRegistration>();
            carReg.CarRegisDetails = new List<CarRegistrationDetails>();
            carReg.CarBonusPartPayment = new List<CarBonusPartPayment>();
            
            try
            {
                using (DataTaskManager dtManager = new DataTaskManager())
                {
                    //Declare and initialize the parameter list object.
                    DBParameterList dbParam = new DBParameterList();

                    //Add the relevant 2 parameters
                    dbParam.Add(new DBParameter(Common.PARAM_DATA, xmlDoc, DbType.String));
                    dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String,
                    ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                    using (DataSet dsDistributorsBonusData = dtManager.ExecuteDataSet(spName, dbParam))
                    {
                        // update database message
                        errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();
                        if (errorMessage.Length == 0 && dsDistributorsBonusData.Tables[0].Rows.Count > 0) //No dbError
                        {
                            for (int i = 0; i < dsDistributorsBonusData.Tables[0].Rows.Count; i++)
                            {
                                CarRegistration carRegistration = new CarRegistration();
                                carRegistration.DistributorId = Convert.ToInt32(dsDistributorsBonusData.Tables[0].Rows[i]["DistributorId"]);
                                carRegistration.DistributorName = Convert.ToString(dsDistributorsBonusData.Tables[0].Rows[i]["DistributorName"]);
                                carRegistration.CarValue = Convert.ToDecimal(dsDistributorsBonusData.Tables[0].Rows[i]["CarValue"]);
                                carRegistration.MaximumCarValue = Convert.ToDecimal(dsDistributorsBonusData.Tables[0].Rows[i]["MaximumCarValue"]);
                               // carRegistration.
                                carRegistration.FirstCarPurDate = dsDistributorsBonusData.Tables[0].Rows[i]["FirstCarPurchaseDate"] != DBNull.Value ? Convert.ToDateTime(dsDistributorsBonusData.Tables[0].Rows[i]["FirstCarPurchaseDate"]) : Common.DATETIME_NULL;
                                //carRegistration.FirstCarPurDate = Convert.ToDateTime(dsDistributorsBonusData.Tables[0].Rows[i]["FirstCarPurchaseDate"]);
                                carRegistration.FirstPayoutDate = dsDistributorsBonusData.Tables[0].Rows[i]["FirstPayoutBusinessMonth"] != DBNull.Value ? Convert.ToDateTime(dsDistributorsBonusData.Tables[0].Rows[i]["FirstPayoutBusinessMonth"]) : Common.DATETIME_NULL;
                                carRegistration.FirstPayoutValue = dsDistributorsBonusData.Tables[0].Rows[i]["FisrtPayoutValue"] != DBNull.Value ? Convert.ToDecimal(dsDistributorsBonusData.Tables[0].Rows[i]["FisrtPayoutValue"]) : 0 ;
                                carRegistration.CarNumber = dsDistributorsBonusData.Tables[0].Rows[i]["FirstCarNo"] !=DBNull.Value ? Convert.ToString(dsDistributorsBonusData.Tables[0].Rows[i]["FirstCarNo"]) : "" ;    

                                carReg.CarRegisHeaders.Add(carRegistration);                 

                            }
                        }

                        if (errorMessage.Length == 0 && dsDistributorsBonusData.Tables[1].Rows.Count > 0) //No dbError
                        {
                            for (int i = 0; i < dsDistributorsBonusData.Tables[1].Rows.Count; i++)
                            {
                                CarRegistration carRegistration = new CarRegistration();
                                carRegistration.DistributorId = Convert.ToInt32(dsDistributorsBonusData.Tables[1].Rows[i]["DistributorId"]);
                                carRegistration.DistributorName = Convert.ToString(dsDistributorsBonusData.Tables[1].Rows[i]["DistributorName"]);
                                carRegistration.MaximumCarValue = dsDistributorsBonusData.Tables[1].Rows[i]["MaximumCarValue"] != DBNull.Value ? Convert.ToDecimal(dsDistributorsBonusData.Tables[1].Rows[i]["MaximumCarValue"]) : 0; ;
                                carRegistration.CummulativeCF = dsDistributorsBonusData.Tables[1].Rows[i]["CummulativeCF"]!=DBNull.Value ? Convert.ToString(dsDistributorsBonusData.Tables[1].Rows[i]["CummulativeCF"]):"";
                                carRegistration.PaidAmount = dsDistributorsBonusData.Tables[1].Rows[i]["PaidAmount"]!=DBNull.Value ? Convert.ToString(dsDistributorsBonusData.Tables[1].Rows[i]["PaidAmount"]) : "";
                                carRegistration.BalanceAmount = dsDistributorsBonusData.Tables[1].Rows[i]["BalanceAmount"]!=DBNull.Value ? Convert.ToString(dsDistributorsBonusData.Tables[1].Rows[i]["BalanceAmount"]) : "";
                             
                                carReg.CarRegisSearchList.Add(carRegistration);
                            } 
                        }

                        if (errorMessage.Length == 0 && dsDistributorsBonusData.Tables[2].Rows.Count > 0) //No dbError
                        {
                            for (int i = 0; i < dsDistributorsBonusData.Tables[2].Rows.Count; i++)
                            {
                                CarRegistrationDetails carRegistration = new CarRegistrationDetails();
                                carRegistration.DistId = Convert.ToInt32(dsDistributorsBonusData.Tables[2].Rows[i]["DistributorId"]);
                                carRegistration.CarNo = Convert.ToString(dsDistributorsBonusData.Tables[2].Rows[i]["CarRegistrationNo"]);
                                carRegistration.MaxCarValue = Convert.ToDecimal(dsDistributorsBonusData.Tables[2].Rows[i]["MaxAllowedAmount"]);
                                carRegistration.IsFirstPaid = Convert.ToBoolean(dsDistributorsBonusData.Tables[2].Rows[i]["IsFirstPayout"]);
                                carRegistration.CarPurchDate = Convert.ToString(dsDistributorsBonusData.Tables[2].Rows[i]["CarPurchaseDate"]);
                            

                                carReg.CarRegisDetails.Add(carRegistration);
                            }
                        }
                        // Part payment details
                        if (errorMessage.Length == 0 && dsDistributorsBonusData.Tables[3].Rows.Count > 0) //No dbError
                        {
                            for (int i = 0; i < dsDistributorsBonusData.Tables[3].Rows.Count; i++)
                            {
                                CarBonusPartPayment objCarBonusPartPayment = new CarBonusPartPayment();
                                objCarBonusPartPayment.DistributorID = Convert.ToInt32(dsDistributorsBonusData.Tables[3].Rows[i]["DistributorID"]);
                                objCarBonusPartPayment.PartPaymentAmount = Convert.ToInt32(dsDistributorsBonusData.Tables[3].Rows[i]["PartPaymentAmount"]);
                                objCarBonusPartPayment.PaymentMode = dsDistributorsBonusData.Tables[3].Rows[i]["PaymentMode"] !=DBNull.Value ? Convert.ToString(dsDistributorsBonusData.Tables[3].Rows[i]["PaymentMode"]): "";
                                objCarBonusPartPayment.Remarks = dsDistributorsBonusData.Tables[3].Rows[i]["Remarks"] != DBNull.Value ? Convert.ToString(dsDistributorsBonusData.Tables[3].Rows[i]["Remarks"]) : "";
                                objCarBonusPartPayment.PartPaymentDate = dsDistributorsBonusData.Tables[3].Rows[i]["PartPaymentDate"] != DBNull.Value ? Convert.ToString(dsDistributorsBonusData.Tables[3].Rows[i]["PartPaymentDate"]) : Convert.ToString(Common.DATETIME_NULL.ToString());

                                carReg.CarBonusPartPayment.Add(objCarBonusPartPayment);
                            }
                        }
                    }
                }
            }
            catch { throw; }
            return carReg;
        }

        #endregion


        public bool ValidateDistIdInSystem(int distributorId, string spName, ref string errorMessage)
        {
            bool isSuccess = false;
            try
            {
                using (DataTaskManager dtManager = new DataTaskManager())
                {

                    //Declare and initialize the parameter list object
                    DBParameterList dbParam = new DBParameterList();

                    //Add the relevant 2 parameters
                    dbParam.Add(new DBParameter(Common.PARAM_DATA, distributorId, DbType.Int32));
                    dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String,
                     System.Data.ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                    try
                    {
                        //Begin the transaction and executing procedure to search the record(s) 
                        dtManager.BeginTransaction();

                        // executing procedure to save the record 
                        dtManager.ExecuteNonQuery(spName, dbParam);

                        //Update database message
                        errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();

                        //If an error returned from the database
                        if (errorMessage.Length > 0)
                        {
                            dtManager.RollbackTransaction();
                            isSuccess = false;
                        }
                        else
                        {
                            dtManager.CommitTransaction();
                            isSuccess = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        dtManager.RollbackTransaction();
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

        public bool ValidateCarNoInSystem(string carRegNo, string spName, ref string errorMessage)
        {
            bool isSuccess = false;
            try
            {
                using (DataTaskManager dtManager = new DataTaskManager())
                {

                    //Declare and initialize the parameter list object
                    DBParameterList dbParam = new DBParameterList();

                    //Add the relevant 2 parameters
                       dbParam.Add(new DBParameter(Common.PARAM_DATA, carRegNo, DbType.String));
                       dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String,
                        System.Data.ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                    try
                    {
                        //Begin the transaction and executing procedure to search the record(s) 
                        dtManager.BeginTransaction();

                        // executing procedure to search the record 
                        dtManager.ExecuteNonQuery(spName, dbParam);

                        //Update database message
                        errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();

                        //If an error returned from the database
                        if (errorMessage.Length > 0)
                        {
                            dtManager.RollbackTransaction();
                            isSuccess = false;
                        }
                        else
                        {
                            dtManager.CommitTransaction();
                            isSuccess = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        dtManager.RollbackTransaction();
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

        public static DataSet GetCarBonusReport(int distributorid, ref string errorMessage )
        {
            try
            {
                string DistId = distributorid.ToString();

                DataSet ds = new DataSet();
                using (DataTaskManager dtManager = new DataTaskManager())
                {
                    DBParameterList dbParam = new DBParameterList();               
                    dbParam.Add(new DBParameter("@DistributorId", DistId, DbType.String));
                    dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, errorMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                    ds = dtManager.ExecuteDataSet(CarRegistration.CAR_GET_BONUS_REPORT, dbParam);
                }
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        
        }
    }

    public class CarRegistrationDetails
    {

        private int m_DistId;

        public int DistId
        {
            get { return m_DistId; }
            set { m_DistId = value; }
        }


        private string m_CarNumber;

        public String CarNo
        {
            get { return m_CarNumber; }
            set { m_CarNumber = value; }
        }
        private string m_CarPurchaseDate;

        public string CarPurchDate
        {
            get { return m_CarPurchaseDate; }
            set { m_CarPurchaseDate = value; }
        }

        private Decimal m_MaximumCarValue;

        public Decimal MaxCarValue
        {
            get { return m_MaximumCarValue; }
            set { m_MaximumCarValue = value; }
        }
        private int m_CreatedBy;

        public int CreatedBy
        {
            get { return m_CreatedBy; }
            set { m_CreatedBy = value; }
        }
        private string m_CreatedDate;

        public string CreatedDate
        {
            get { return m_CreatedDate; }
            set { m_CreatedDate = value; }
        }

        private bool m_IsFirstPayout;
        public bool IsFirstPaid
        {
            get { return m_IsFirstPayout; }
            set { m_IsFirstPayout = value; }
        }
    }

    public class CarBonusPartPayment
    {
        private int _DistributorID;

        public int DistributorID
        {
            get { return _DistributorID; }
            set { _DistributorID = value; }
        }
        private Decimal _PartPaymentAmount;

        public Decimal PartPaymentAmount
        {
            get { return _PartPaymentAmount; }
            set { _PartPaymentAmount = value; }
        }
        private string _PaymentMode;

        public string PaymentMode
        {
            get { return _PaymentMode; }
            set { _PaymentMode = value; }
        }
        private string _Remarks;

        public string Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        private string _PartPaymentDate;

        public string PartPaymentDate
        {
            get { return _PartPaymentDate; }
            set { _PartPaymentDate = value; }
        }
        private int _CreatedBy;

        public int CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        private string _CreatedDate;

        public string CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
    }
}
