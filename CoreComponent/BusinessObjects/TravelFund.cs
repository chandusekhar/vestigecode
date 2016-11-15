using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CoreComponent.Core.BusinessObjects;
using Vinculum.Framework.Data;
using Vinculum.Framework.DataTypes;
namespace CoreComponent.BusinessObjects
{
    public class TravelFund
    {
        #region Constants

        public const string DIST_TRAVEL_FUND_SAVE = "USP_TravelFundSave";
        public const string DISTRI_GET_DETAIL = "USP_DistriTravelDetails";
        public const string DISTRI_EXIST_IN_TRAVELFUND = "usp_DistriExistInTravelFund";
        public const string DIST_TRAVEL_FUND_SEARCH = "USP_TravelFundSearch";
        public const string DIST_TRAVEL_FUND_REPORT = "USP_TravelFundReport";
        public const string DIST_BULKIMPORT_TRAVEL_FUND = "USP_BulkImportTravelFund";
        
        #endregion
        #region Properties
        private int distributorID;
        public int DistributorID
        {
            get { return distributorID; }
            set { distributorID = value; }
        }

        private int beneficiaryID;
        public int BeneficiaryID
        {
            get { return beneficiaryID; }
            set { beneficiaryID = value; }
        }

        private Decimal paidAmount;
        public Decimal PaidAmount
        {
            get { return paidAmount; }
            set { paidAmount = value; }
        }

        private string businessMonth;
        public string BusinessMonth
        {
            get { return businessMonth; }
            set { businessMonth = value; }
        }

        private int createdBy;
        public int CreatedBy
        {
            get { return createdBy; }
            set { createdBy = value; }
        }

        private string createdDate;
        public string CreatedDate
        {
            get { return createdDate; }
            set { createdDate = value; }
        }


        private List<TravelFundDetails> lsttravelfunddetails;
        public List<TravelFundDetails> lstTravelFundDetails
        {
            get { return lsttravelfunddetails; }
            set { lsttravelfunddetails = value; }
        }

        private List<TravelFund> lsttravelfund;
        public List<TravelFund> lstTravelFund
        {
            get { return lsttravelfund; }
            set { lsttravelfund = value; }
        }

        private List<TravelFund> lsttravelfundsearch;
        public List<TravelFund> LstTravelFundSearch
        {
            get { return lsttravelfundsearch; }
            set { lsttravelfundsearch = value; }
        }

        private decimal cumulativeAmt;

        public decimal CumulativeAmt
        {
            get { return cumulativeAmt; }
            set { cumulativeAmt = value; }
        }

        private decimal paidAmt;

        public decimal PaidAmt
        {
            get { return paidAmt; }
            set { paidAmt = value; }
        }

        private decimal balanceAmt;

        public decimal BalanceAmt
        {
            get { return balanceAmt; }
            set { balanceAmt = value; }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        #endregion

        public bool TravelFundSave(string xmlDoc, string spName, ref string errorMessage)
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

        public static DataSet fncBuldImportTravelFund(string FileName, string spName, ref string errorMessage)
        {
            DataSet ds = new DataSet();
            try
            {
                using (DataTaskManager dtManager = new DataTaskManager())
                {
                    //Declare and initialize the parameter list object
                    DBParameterList dbParam = new DBParameterList();

                    //Add the relevant 2 parameters
                    dbParam.Add(new DBParameter(Common.PARAM_DATA, FileName, DbType.String));
                    dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String,
                    System.Data.ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));
                    // executing procedure to save the record 
                    ds = dtManager.ExecuteDataSet(spName, dbParam);
                    errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public static DataSet fncGetDetails(int intDistributorID, string spName)
        {
            DataSet dsDistributorDetails;
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
                    dsDistributorDetails = dtManager.ExecuteDataSet(spName, dbParam);
                }
            }
            catch { throw; }
            return dsDistributorDetails; 
        }


        public static TravelFund fncTravelFundSearch(string xmlDoc, string spName, ref string errorMessage)
        {
            TravelFund objTravelFund = new TravelFund();
            objTravelFund.lstTravelFund = new List<TravelFund>();
            objTravelFund.lstTravelFundDetails = new List<TravelFundDetails>();
            objTravelFund.LstTravelFundSearch = new List<TravelFund>();

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

                    using (DataSet dsTFDeatils = dtManager.ExecuteDataSet(spName, dbParam))
                    {
                        // update database message
                        errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();
                        if (errorMessage.Length == 0 && dsTFDeatils.Tables[0].Rows.Count > 0) //No dbError
                        {
                            for (int i = 0; i < dsTFDeatils.Tables[0].Rows.Count; i++)
                            {
                                TravelFund objTF = new TravelFund();
                                objTF.DistributorID = Convert.ToInt32(dsTFDeatils.Tables[0].Rows[i]["DistributorID"].ToString());
                                objTF.Name = dsTFDeatils.Tables[0].Rows[i]["Name"].ToString();
                                objTF.CumulativeAmt = Convert.ToDecimal(dsTFDeatils.Tables[0].Rows[i]["CumulativeAmount"].ToString());
                                objTF.PaidAmount = Convert.ToDecimal(dsTFDeatils.Tables[0].Rows[i]["PaidAmount"].ToString());
                                objTF.BalanceAmt = Convert.ToDecimal(dsTFDeatils.Tables[0].Rows[i]["BalanceAmount"].ToString());
                                objTF.BusinessMonth = dsTFDeatils.Tables[0].Rows[i]["BusinessMonth"].ToString();

                                objTravelFund.LstTravelFundSearch.Add(objTF);
                            }

                            for (int i = 0; i < dsTFDeatils.Tables[1].Rows.Count; i++)
                            {
                                TravelFundDetails objTFD = new TravelFundDetails();
                                objTFD.DistributorID = Convert.ToInt32(dsTFDeatils.Tables[1].Rows[i]["DistributorID"].ToString());
                                objTFD.Beneficiary = dsTFDeatils.Tables[1].Rows[i]["BeneficiaryType"].ToString();
                                objTFD.BeneficiaryID = Convert.ToInt32(dsTFDeatils.Tables[1].Rows[i]["BeneficiaryID"].ToString());
                                objTFD.PaidAmount = Convert.ToDecimal(dsTFDeatils.Tables[1].Rows[i]["Amount"].ToString());
                                objTFD.BusinessMonth = dsTFDeatils.Tables[1].Rows[i]["BusinessMonth"] != DBNull.Value ? dsTFDeatils.Tables[1].Rows[i]["BusinessMonth"].ToString()  : Common.DATETIME_NULL.ToString(Common.DATE_TIME_FORMAT);
                                objTFD.Remarks = dsTFDeatils.Tables[1].Rows[i]["Remarks"].ToString();
                                objTFD.CreatedBy = Convert.ToInt32(dsTFDeatils.Tables[1].Rows[i]["CreatedBy"].ToString());
                                objTFD.CreatedDate = dsTFDeatils.Tables[1].Rows[i]["CreatedDate"] != DBNull.Value ? Convert.ToDateTime(dsTFDeatils.Tables[1].Rows[i]["CreatedDate"]).ToString(Common.DATE_TIME_FORMAT)  : Common.DATETIME_NULL.ToString(Common.DATE_TIME_FORMAT);
                                
                                objTravelFund.lstTravelFundDetails.Add(objTFD);
                            }
                        }
                    }
                }
            }
            catch { throw; }
            return objTravelFund;
        }

        public static DataSet GetTravelFundReport(int distributorid, ref string errorMessage)
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

                    ds = dtManager.ExecuteDataSet(TravelFund.DIST_TRAVEL_FUND_REPORT, dbParam);
                }
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
    public class TravelFundDetails
    {
        #region Properties
        private int distributorID;
        public int DistributorID
        {
            get { return distributorID; }
            set { distributorID = value; }
        }

        private int beneficiaryID;
        public int BeneficiaryID
        {
            get { return beneficiaryID; }
            set { beneficiaryID = value; }
        }

        private string beneficiary;
        public string Beneficiary
        {
            get { return beneficiary; }
            set { beneficiary = value; }
        }

        private Decimal paidAmount;
        public Decimal PaidAmount
        {
            get { return paidAmount; }
            set { paidAmount = value; }
        }

        private string businessMonth;
        public string BusinessMonth
        {
            get { return businessMonth; }
            set { businessMonth = value; }
        }

        private int createdBy;
        public int CreatedBy
        {
            get { return createdBy; }
            set { createdBy = value; }
        }

        private string createdDate;
        public string CreatedDate
        {
            get { return createdDate; }
            set { createdDate = value; }
        }

        private string remarks;
        public string Remarks
        {
            get { return remarks; }
            set { remarks = value; }
        }
        #endregion
    }

    public class TraveFundBulkImport
    {
        private int distributorid;

        public int DistributorID
        {
            get { return distributorid; }
            set { distributorid = value; }
        }

        private decimal amount;

        public decimal Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        private string remarks;

        public string Remarks
        {
            get { return remarks; }
            set { remarks = value; }
        }

        private bool payOradjust;

        public bool PayOrAdjust
        {
            get { return payOradjust; }
            set { payOradjust = value; }
        }
    }



}
