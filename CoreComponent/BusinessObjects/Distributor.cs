using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CoreComponent.Core.BusinessObjects;
using Vinculum.Framework.Data;
using Vinculum.Framework.DataTypes;
using System.Diagnostics;
namespace CoreComponent.BusinessObjects
{
    [Serializable]
    public class Distributor
    {
        #region SP DECLARATION

        //constant defined for storing 'search-all-info' sp-name
        private const string m_uspDistributorSearch = "usp_getDistributorMasterInfo";
        private const string DISTRIBUTORHISTORY = "usp_DistributorHistory";
        private const string BANKBRANCHDETAIL = "usp_GetBankBranch";
        private const string SP_DISTRIBUTOR_SEARCH = "usp_DistributorSearch";
        private const string SP_DISTRIBUTOR_REGISTER = "usp_RegisterDistributor";
        private const string SP_UPDATE_DISTRIBUTOR = "usp_DistributorUpdate";
        private const string SP_GETINVOICEDATE = "usp_GetInvoiceData";
        // Added by Kaushik for Distributor Navigation
        private const string SP_GETDISTRIBUTOR_POSITION = "usp_distributorNavigation";
        private const string SP_GETNEXTDISTRIBUTOR_ID = "usp_nextDistributor";

        #endregion

        #region Properties
        public byte[] PanImage
        {
            get;
            set;
        }
        public byte[] BankImage
        {
            get;
            set;
        }
        public string PANType
        {
            get;
            set;
        }
        public string BankType
        {
            get;
            set;
        }

        public string SDistributorId
        {
            get;
            set;
        }


        public Int32 DistributorId
        {
            get;
            set;
        }

        public Int32 UplineDistributorId
        {
            get;
            set;
        }

        public Int32 DistributorTitleId
        {
            get;
            set;
        }

        public String DistributorTitle
        {
            get;
            set;
        }

        public String DistributorFirstName
        {
            get;
            set;
        }

        public String DistributorLastName
        {
            get;
            set;
        }

        public string DistributorFullName
        {
            get
            {
                if (string.IsNullOrEmpty(DistributorLastName.Trim()))
                {
                    return DistributorFirstName.Trim();
                }
                else
                {
                    return DistributorFirstName.Trim() + " " + DistributorLastName.Trim();
                }
            }
        }


        public String DistributorDOB
        {
            get;
            set;
        }

        public String DistributorEnrolledOn
        {
            get;
            set;
        }

        public int CoDistributorTitleId
        {
            get;
            set;
        }

        public String CoDistributorTitle
        {
            get;
            set;
        }

        public String CoDistributorFirstName
        {
            get;
            set;
        }

        public String CoDistributorLastName
        {
            get;
            set;
        }

        public String CoDistributorDOB
        {
            get;
            set;
        }

        public String DistributorAddress
        {
            get;
            set;
        }

        public String DistributorAddress1
        {
            get;
            set;
        }

        public String DistributorAddress2
        {
            get;
            set;
        }

        public String DistributorAddress3
        {
            get;
            set;
        }

        public String DistributorAddress4
        {
            get;
            set;
        }

        public int DistributorCityCode
        {
            get;
            set;
        }

        public String DistributorCity
        {
            get;
            set;
        }

        public int DistributorStateCode
        {
            get;
            set;
        }

        public String DistributorState
        {
            get;
            set;
        }

        public int DistributorCountryCode
        {
            get;
            set;
        }

        public String DistributorCountry
        {
            get;
            set;
        }

        public int SubZoneCode
        {
            get;
            set;
        }

        public int AreaCode
        {
            get;
            set;
        }

        public String DistributorPinCode
        {
            get;
            set;
        }

        public String DistributorTeleNumber
        {
            get;
            set;
        }

        public String DistributorMobNumber
        {
            get;
            set;
        }

        public String DistributorFaxNumber
        {
            get;
            set;
        }

        public String DistributorEMailID
        {
            get;
            set;
        }

        public DateTime DistributorRegistrationDate
        {
            get;
            set;
        }

        public DateTime DistributorActivationDate
        {
            get;
            set;
        }

        public Decimal MinFirstPurchaseAmount
        {
            get;
            set;
        }

        public Double MinimumSaleAmount
        {
            get;
            set;
        }

        public String DistributorStatus
        {
            get;
            set;
        }

        public String DistributorStatusText
        {
            get;
            set;
        }

        public Int32 LocationId
        {
            get;
            set;
        }

        public String Location
        {
            get;
            set;
        }

        public Int32 CreatedById
        {
            get;
            set;
        }

        public String CreatedBy
        {
            get;
            set;
        }

        public string CreatedDate
        {
            get;
            set;
        }

        public Int32 ModifiedById
        {
            get;
            set;
        }

        public String ModifiedBy
        {
            get;
            set;
        }

        public string ModifiedDate
        {
            get;
            set;
        }

        public string AccountNumber
        {
            get;
            set;
        }
        public string BankBranchCode
        {
            get;
            set;
        }
        public string DistributorPANNumber
        {
            get;
            set;
        }

        public string KitOrderNo
        {
            get;
            set;
        }

        public string KitInvoiceNo
        {
            get;
            set;
        }

        public bool FirstOrderTaken
        {
            get;
            set;
        }

        public string SerialNo
        {
            get;
            set;
        }

        public List<DistributorAccount> DistributorAccounts
        {
            get;
            set;
        }

        public string Zone
        {
            get;
            set;
        }
        
        public int ZoneCode
        {
            get;
            set;
        }

        public int BankCode
        {
            get;
            set;
        }

        public string Bank
        {
            get;
            set;
        }

        public String Star
        {
            get;
            set;
        }

        public String Entitled
        {
            get;
            set;
        }

        public String DirectorGroup
        {
            get;
            set;
        }

        public String DirectorName
        {
            get;
            set;
        }

        public double Curr_PrevCumPV
        {
            get;
            set;
        }

        public double AllInvoiceAmountSum
        {
            get;
            set;
        }

        public int CurrentLocationId
        {
            get 
            {
                return Common.CurrentLocationId; 
            }
            set 
            { 
                throw new NotImplementedException("This property can not be explicitly set"); 
            }
        }

        public double DisplayCurr_PrevCumPV
        {
            get
            {
                return Math.Round(Curr_PrevCumPV, Common.DBAmountRounding, MidpointRounding.AwayFromZero);
            }
            set
            {
                throw new NotImplementedException("This property can not be explicitly set");
            }
        }

        public double Curr_ExclPV
        {
            get;
            set;
        }

        public double DisplayCurr_ExclPV
        {
            get
            {
                return Math.Round(Curr_ExclPV, Common.DBAmountRounding, MidpointRounding.AwayFromZero);
            }
            set
            {
                throw new NotImplementedException("This property can not be explicitly set");
            }
        }
        public int DistributorDocumentFlag
        {
            get;
            set;
        }
        public double Curr_SelfPV
        {
            get;
            set;
        }

        public double DisplayCurr_SelfPV
        {
            get
            {
                return Math.Round(Curr_SelfPV, Common.DBAmountRounding, MidpointRounding.AwayFromZero);
            }
            set
            {
                throw new NotImplementedException("This property can not be explicitly set");
            }
        }

        public double Curr_GroupPV
        {
            get;
            set;
        }

        public double DisplayCurr_GroupPV
        {
            get
            {
                return Math.Round(Curr_GroupPV, Common.DBAmountRounding, MidpointRounding.AwayFromZero);
            }
            set
            {
                throw new NotImplementedException("This property can not be explicitly set");
            }
        }

        public double Curr_TotalPV
        {
            get;
            set;
        }

        public double DisplayCurr_TotalPV
        {
            get
            {
                return Math.Round(Curr_TotalPV, Common.DBAmountRounding, MidpointRounding.AwayFromZero);
            }
            set
            {
                throw new NotImplementedException("This property can not be explicitly set");
            }
        }

        public double Curr_BonusPercent
        {
            get;
            set;
        }

        public double DisplayCurr_BonusPercent
        {
            get
            {
                return Math.Round(Curr_BonusPercent, Common.DBAmountRounding, MidpointRounding.AwayFromZero);
            }
            set
            {
                throw new NotImplementedException("This property can not be explicitly set");
            }
        }

        public double Curr_TotalCumPV
        {
            get;
            set;
        }

        public double DisplayCurr_TotalCumPV
        {
            get
            {
                return Math.Round(Curr_TotalCumPV, Common.DBAmountRounding, MidpointRounding.AwayFromZero);
            }
            set
            {
                throw new NotImplementedException("This property can not be explicitly set");
            }
        }

        public double Curr_TotalBV
        {
            get;
            set;
        }

        public double DisplayCurr_TotalBV
        {
            get
            {
                return Math.Round(Curr_TotalBV, Common.DBAmountRounding, MidpointRounding.AwayFromZero);
            }
            set
            {
                throw new NotImplementedException("This property can not be explicitly set");
            }
        }

        public double Prev_ExclPV
        {
            get;
            set;
        }

        public double DisplayPrev_ExclPV
        {
            get
            {
                return Math.Round(Prev_ExclPV, Common.DBAmountRounding, MidpointRounding.AwayFromZero);
            }
            set
            {
                throw new NotImplementedException("This property can not be explicitly set");
            }
        }

        public double Prev_SelfPV
        {
            get;
            set;
        }

        public double DisplayPrev_SelfPV
        {
            get
            {
                return Math.Round(Prev_SelfPV, Common.DBAmountRounding, MidpointRounding.AwayFromZero);
            }
            set
            {
                throw new NotImplementedException("This property can not be explicitly set");
            }
        }

        public double Prev_GroupPV
        {
            get;
            set;
        }

        public double DisplayPrev_GroupPV
        {
            get
            {
                return Math.Round(Prev_GroupPV, Common.DBAmountRounding, MidpointRounding.AwayFromZero);
            }
            set
            {
                throw new NotImplementedException("This property can not be explicitly set");
            }
        }

        public double Prev_TotalPV
        {
            get;
            set;
        }

        public double DisplayPrev_TotalPV
        {
            get
            {
                return Math.Round(Prev_TotalPV, Common.DBAmountRounding, MidpointRounding.AwayFromZero);
            }
            set
            {
                throw new NotImplementedException("This property can not be explicitly set");
            }
        }

        public double Prev_BonusPercent
        {
            get;
            set;
        }

        public double DisplayPrev_BonusPercent
        {
            get
            {
                return Math.Round(Prev_BonusPercent, Common.DBAmountRounding, MidpointRounding.AwayFromZero);
            }
            set
            {
                throw new NotImplementedException("This property can not be explicitly set");
            }
        }

        public double Prev_TotalBV
        {
            get;
            set;
        }


        public double CurrentPrevCummulativePV
        {
            get;
            set;
        }

        public double CurrentTotalCummulativePV
        {
            get;
            set;
        }

        public double DisplayPrev_TotalBV
        {
            get
            {
                return Math.Round(Prev_TotalBV, Common.DBAmountRounding, MidpointRounding.AwayFromZero);
            }
            set
            {
                throw new NotImplementedException("This property can not be explicitly set");
            }
        }

        public string Remarks
        {
            get;
            set;
        }
        public double Curr_SelfBV
        {
            get;
            set;
        }

        public double DisplayCurr_SelfBV
        {
            get
            {
                return Math.Round(Curr_SelfBV, Common.DBAmountRounding, MidpointRounding.AwayFromZero);
            }
            set
            {
                throw new NotImplementedException("This property can not be explicitly set");
            }
        }
        
        public double Prev_SelfBV
        {
            get;
            set;
        }

        public double DisplayPrev_SelfBV
        {
            get
            {
                return Math.Round(Prev_SelfBV, Common.DBAmountRounding, MidpointRounding.AwayFromZero);
            }
            set
            {
                throw new NotImplementedException("This property can not be explicitly set");
            }
        }

        string m_userName = string.Empty;
        string m_Password = string.Empty;

        public string UserName
        {
            get{ return m_userName;}
            set {m_userName = value;}
        }

        public string Password
        {
            get { return m_Password ; }
            set { m_Password = value; }

        }

        public string RefId
        {
            get;
            set;
        }

        public DateTime RegistrationDate
        {
            get;
            set;
        }

        public DateTime PanUpdateDate
        {
            get;
            set;
        }
        public DateTime BankUpdateDate
        {
            get;
            set;
        }
        
        public string SaveON
        {
            get;
            set;
        }
        public string RegistrationLocation
        {
            get;
            set;
        }

        public Boolean forSkinCareItem
        {
            get;
            set;
        }

        #endregion

        #region Methods

        public List<Distributor> Search()
        {
            List<Distributor> lst = null;
            try
            {
                string errorMessage = string.Empty;
                lst = SearchDistributor(ref errorMessage);
            }
            catch (Exception ex)
            {                
                Common.LogException(ex);
                throw ex;
            }
            return lst;
        }


        /// <summary>
        /// Search Distributor Master And Distributor Account Details
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public List<Distributor> SearchDistributor(ref string errorMessage)
        {
            List<Distributor> listOfDistributor = new List<Distributor>();
            DBParameterList dbParam;
            try
            {
                {                    
                    string xmlDoc = Common.ToXml(this);
                    DataTaskManager dtManager = new DataTaskManager();
                    
                    // initialize the parameter list object
                    dbParam = new DBParameterList();
                    
                    // add the relevant parameters
                    dbParam.Add(new DBParameter(Common.PARAM_DATA, xmlDoc, DbType.String));
                    dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, errorMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                    // executing procedure to fetch the record
                    DataSet ds = dtManager.ExecuteDataSet(SP_DISTRIBUTOR_SEARCH, dbParam);

                    // update errorMessage
                    errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();

                    // if error returned from the database
                    if (errorMessage != string.Empty)
                        return null;
                    else
                    {
                        if (ds.Tables.Count == 2)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                {
                                    Distributor distributorObj = new Distributor();
                                    Address distributorAddressObj = new Address();
                                    List<DistributorAccount> listOfDistributorAccount = new List<DistributorAccount>();
                                    distributorObj.DistributorId = Convert.ToInt32(ds.Tables[0].Rows[i]["DistributorId"]);
                                    distributorObj.UplineDistributorId = Convert.ToInt32(ds.Tables[0].Rows[i]["UplineDistributorId"]);
                                    distributorObj.DistributorTitleId = Convert.ToInt32(ds.Tables[0].Rows[i]["DistributorTitle"]);
                                    distributorObj.DistributorTitle = ds.Tables[0].Rows[i]["DistributorTitleText"].ToString();
                                    distributorObj.DistributorFirstName = ds.Tables[0].Rows[i]["DistributorFirstName"].ToString();
                                    distributorObj.DistributorLastName = ds.Tables[0].Rows[i]["DistributorLastName"].ToString();
                                    distributorObj.DistributorDOB = ds.Tables[0].Rows[i]["DistributorDOB"].ToString(); ;
                                    distributorObj.CoDistributorTitleId = Convert.ToInt32(ds.Tables[0].Rows[i]["Co_DistributorTitle"]);
                                    distributorObj.CoDistributorTitle = ds.Tables[0].Rows[i]["CoDistributorTitleText"].ToString();
                                    distributorObj.CoDistributorFirstName = ds.Tables[0].Rows[i]["Co_DistributorFirstName"].ToString();
                                    distributorObj.CoDistributorLastName = ds.Tables[0].Rows[i]["Co_DistributorLastName"].ToString();
                                    distributorObj.CoDistributorDOB = ds.Tables[0].Rows[i]["Co_DistributorDOB"].ToString();
                                    
                                    distributorAddressObj.Address1 = ds.Tables[0].Rows[i]["DistributorAddress1"].ToString();
                                    distributorAddressObj.Address2 = ds.Tables[0].Rows[i]["DistributorAddress2"].ToString();
                                    distributorAddressObj.Address3 = ds.Tables[0].Rows[i]["DistributorAddress3"].ToString();
                                    distributorAddressObj.Address4 = ds.Tables[0].Rows[i]["DistributorAddress4"].ToString();
                                    distributorAddressObj.CityId=distributorObj.DistributorCityCode = Convert.ToInt32(ds.Tables[0].Rows[i]["DistributorCityCode"]);
                                    distributorAddressObj.City=distributorObj.DistributorCity = ds.Tables[0].Rows[i]["CityName"].ToString();
                                    distributorAddressObj.StateId=distributorObj.DistributorStateCode = Convert.ToInt32(ds.Tables[0].Rows[i]["DistributorStateCode"].ToString());
                                    distributorAddressObj.State=distributorObj.DistributorState = ds.Tables[0].Rows[i]["StateName"].ToString();
                                    distributorAddressObj.PinCode=distributorObj.DistributorPinCode = ds.Tables[0].Rows[i]["DistributorPinCode"].ToString();
                                    distributorAddressObj.PhoneNumber1=distributorObj.DistributorTeleNumber = ds.Tables[0].Rows[i]["DistributorTeleNumber"].ToString();
                                    distributorAddressObj.Mobile1=distributorObj.DistributorMobNumber = ds.Tables[0].Rows[i]["DistributorMobNumber"].ToString();
                                    distributorAddressObj.Fax1=distributorObj.DistributorFaxNumber = ds.Tables[0].Rows[i]["DistributorFaxNumber"].ToString();
                                    distributorAddressObj.Email1=distributorObj.DistributorEMailID = ds.Tables[0].Rows[i]["DistributorEMailID"].ToString();
                                    if ((Common.IsMiniBranchLocation != 1) && (!Common.CheckIfDistributorAddHidden(Convert.ToInt32(ds.Tables[0].Rows[i]["DistributorId"]))))
                                    {
                                        distributorObj.DistributorAddress = distributorAddressObj.GetAddress();

                                        distributorObj.DistributorAddress1 = ds.Tables[0].Rows[i]["DistributorAddress1"].ToString();
                                        distributorObj.DistributorAddress2 = ds.Tables[0].Rows[i]["DistributorAddress2"].ToString();
                                        distributorObj.DistributorAddress3 = ds.Tables[0].Rows[i]["DistributorAddress3"].ToString();
                                        distributorObj.DistributorAddress4 = ds.Tables[0].Rows[i]["DistributorAddress4"].ToString();
                                    }
                                    
                                    distributorObj.DistributorRegistrationDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["DistributorRegistrationDate"]);
                                    distributorObj.DistributorActivationDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["DistributorActivationDate"]);
                                    distributorObj.MinFirstPurchaseAmount = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["MinFirstPurchaseAmount"]),2);
                                    distributorObj.LocationId = Convert.ToInt32(ds.Tables[0].Rows[i]["LocationId"]);
                                    distributorObj.DistributorPANNumber = ds.Tables[0].Rows[i]["DistributorPANNumber"].ToString();
                                    distributorObj.KitOrderNo = ds.Tables[0].Rows[i]["KitOrderNo"].ToString();
                                    distributorObj.KitInvoiceNo = ds.Tables[0].Rows[i]["KitInvoiceNo"].ToString();
                                    distributorObj.FirstOrderTaken = Convert.ToBoolean(ds.Tables[0].Rows[i]["FirstOrderTaken"]);
                                    distributorObj.DistributorStatus = ds.Tables[0].Rows[i]["DistributorStatus"].ToString();
                                    distributorObj.DistributorStatusText = ds.Tables[0].Rows[i]["StatusText"].ToString();
                                    distributorObj.CreatedById = Convert.ToInt32(ds.Tables[0].Rows[i]["CreatedBy"]);
                                    distributorObj.CreatedBy = ds.Tables[0].Rows[i]["CreatedByName"].ToString();
                                    distributorObj.CreatedDate = ds.Tables[0].Rows[i]["CreatedDate"].ToString();
                                    distributorObj.ModifiedById = Convert.ToInt32(ds.Tables[0].Rows[i]["ModifiedBy"]);
                                    distributorObj.ModifiedBy = ds.Tables[0].Rows[i]["ModifiedByName"].ToString();
                                    distributorObj.ModifiedDate = ds.Tables[0].Rows[i]["ModifiedDate"].ToString();
                                    distributorObj.DirectorGroup =Convert.ToString(ds.Tables[0].Rows[i]["DirectorGroup"]);
                                    distributorObj.DirectorName = Convert.ToString(ds.Tables[0].Rows[i]["DirectorName"]);
                                    distributorObj.AllInvoiceAmountSum = Convert.ToDouble(ds.Tables[0].Rows[i]["AllInvoiceAmountSum"]);
                                    distributorObj.MinimumSaleAmount= Convert.ToDouble(ds.Tables[0].Rows[i]["MinimumSaleAmount"]);
                                    distributorObj.forSkinCareItem = Convert.ToBoolean(ds.Tables[0].Rows[i]["ForSkinCareItem"]);
                                    if (ds.Tables[1].Rows.Count > 0)
                                    {
                                        for (int j = 0; j < ds.Tables[1].Rows.Count; j++)
                                        {
                                            if (Convert.ToInt32(ds.Tables[1].Rows[j]["DistributorId"]) == distributorObj.DistributorId)
                                            {
                                                DistributorAccount distributorAccountObj = new DistributorAccount();
                                                distributorAccountObj.DistributorId = ds.Tables[1].Rows[j]["DistributorId"].ToString();
                                                distributorAccountObj.DistributorBankName = ds.Tables[1].Rows[j]["DistributorBankName"].ToString();
                                                distributorAccountObj.DistributorBankBranch = ds.Tables[1].Rows[j]["DistributorBankBranch"].ToString();
                                                distributorAccountObj.DistributorBankAccNumber = ds.Tables[1].Rows[j]["DistributorBankAccNumber"].ToString();
                                                distributorAccountObj.BankID = Convert.ToInt32(ds.Tables[1].Rows[j]["BankID"]);
                                                distributorAccountObj.IsPrimary = Convert.ToInt32(ds.Tables[1].Rows[j]["IsPrimary"]);
                                                distributorAccountObj.CreatedBy = Convert.ToInt32(ds.Tables[1].Rows[j]["CreatedBy"]);
                                                distributorAccountObj.CreatedDate = Convert.ToDateTime(ds.Tables[1].Rows[j]["CreatedDate"]);
                                                distributorAccountObj.ModifiedBy = Convert.ToInt32(ds.Tables[1].Rows[j]["ModifiedBy"]);
                                                distributorAccountObj.ModifiedDate = Convert.ToDateTime(ds.Tables[1].Rows[j]["ModifiedDate"]);
                                                listOfDistributorAccount.Add(distributorAccountObj);
                                            }
                                        }
                                    }
                                    distributorObj.DistributorAccounts = listOfDistributorAccount;
                                    listOfDistributor.Add(distributorObj);
                                }
                            }
                        }
                    }
                }
                return listOfDistributor;               
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                throw ex;
            }

        }

        public bool Register(ref string dbValidationMessage, ref string dbErrorMessage, ref string distributorSerialNo)
        {
            try
            {
                bool returnValue = false;
                using (DataTaskManager dt = new DataTaskManager())
                {
                    DBParameterList paramList = new DBParameterList();
                    paramList.Add(new DBParameter(Common.PARAM_DATA, Common.ToXml(this), DbType.String));
                    paramList.Add(new DBParameter(Common.PARAM_OUTPUT, dbErrorMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));
                    paramList.Add(new DBParameter("@validationMessage", dbValidationMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));
                    paramList.Add(new DBParameter("@DistributorSerialNo", distributorSerialNo, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                    dt.ExecuteNonQuery(SP_DISTRIBUTOR_REGISTER, paramList);
                    dbValidationMessage = paramList["@validationMessage"].Value.ToString();
                    dbErrorMessage = paramList[Common.PARAM_OUTPUT].Value.ToString();
                    distributorSerialNo = paramList["@DistributorSerialNo"].Value.ToString();
                    if (string.IsNullOrEmpty(dbValidationMessage) && string.IsNullOrEmpty(dbErrorMessage) && distributorSerialNo.Length > 0)
                    {
                        returnValue = true;
                    }
                }
                return returnValue;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public DataSet SearchAllInfoForDistributor(string distributorID, string distributorFName, string distributorLName, string searchCondition, string searchType, ref string errMsg)
        //{
        //    DataSet dsAllInfo = null;
        //    DBParameterList dbParam;
        //    if (Common.AppType == ((int)Common.ApplicationType.BOS).ToString())
        //    {
        //        using (DataTaskManager dtManager = new DataTaskManager(Common.HODB))
        //        {
        //            dbParam = new DBParameterList();
        //            dbParam.Add(new DBParameter(Common.PARAM_DATA, distributorID, DbType.String));
        //            dbParam.Add(new DBParameter(Common.PARAM_DATA2, distributorFName, DbType.String));
        //            dbParam.Add(new DBParameter(Common.PARAM_DATA3, distributorLName, DbType.String));
        //            dbParam.Add(new DBParameter(Common.PARAM_DATA4, searchCondition, DbType.String));
        //            dbParam.Add(new DBParameter(Common.PARAM_DATA5, searchType, DbType.String));
        //            dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, errMsg, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

        //            dsAllInfo = dtManager.ExecuteDataSet(m_uspDistributorSearch, dbParam);
        //            errMsg = dbParam[Common.PARAM_OUTPUT].Value.ToString();
        //        }
        //    }
        //    else
        //    {
        //        using (DataTaskManager dtManager = new DataTaskManager())
        //        {
        //            dbParam = new DBParameterList();
        //            dbParam.Add(new DBParameter(Common.PARAM_DATA, distributorID, DbType.String));
        //            dbParam.Add(new DBParameter(Common.PARAM_DATA2, distributorFName, DbType.String));
        //            dbParam.Add(new DBParameter(Common.PARAM_DATA3, distributorLName, DbType.String));
        //            dbParam.Add(new DBParameter(Common.PARAM_DATA4, searchCondition, DbType.String));
        //            dbParam.Add(new DBParameter(Common.PARAM_DATA5, searchType, DbType.String));
        //            dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, errMsg, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

        //            dsAllInfo = dtManager.ExecuteDataSet(m_uspDistributorSearch, dbParam);
        //            errMsg = dbParam[Common.PARAM_OUTPUT].Value.ToString();
        //        }
        //    }

        //    return dsAllInfo;
        //}

        public DataSet SearchAllInfoForDistributor(string distributorID, string distributorFName, string distributorLName, string searchCondition, string searchType, ref string errMsg)
        {
            DataSet dsAllInfo = null;
            DBParameterList dbParam;
            if (Common.AppType == ((int)Common.ApplicationType.BOS).ToString())
            {
                using (DataTaskManager dtManager = new DataTaskManager(Common.HODB))
                {
                    dbParam = new DBParameterList();
                    dbParam.Add(new DBParameter(Common.PARAM_DATA, distributorID, DbType.String));
                    dbParam.Add(new DBParameter(Common.PARAM_DATA2, distributorFName, DbType.String));
                    dbParam.Add(new DBParameter(Common.PARAM_DATA3, distributorLName, DbType.String));
                    dbParam.Add(new DBParameter(Common.PARAM_DATA4, searchCondition, DbType.String));
                    dbParam.Add(new DBParameter(Common.PARAM_DATA5, searchType, DbType.String));
                    dbParam.Add(new DBParameter(Common.PARAM_DATA6, Common.AppType, DbType.String));
                    dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, errMsg, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                    dsAllInfo = dtManager.ExecuteDataSet(m_uspDistributorSearch, dbParam);
                    errMsg = dbParam[Common.PARAM_OUTPUT].Value.ToString();
                }
            }
            else
            {
                using (DataTaskManager dtManager = new DataTaskManager())
                {
                    dbParam = new DBParameterList();
                    dbParam.Add(new DBParameter(Common.PARAM_DATA, distributorID, DbType.String));
                    dbParam.Add(new DBParameter(Common.PARAM_DATA2, distributorFName, DbType.String));
                    dbParam.Add(new DBParameter(Common.PARAM_DATA3, distributorLName, DbType.String));
                    dbParam.Add(new DBParameter(Common.PARAM_DATA4, searchCondition, DbType.String));
                    dbParam.Add(new DBParameter(Common.PARAM_DATA5, searchType, DbType.String));
                    dbParam.Add(new DBParameter(Common.PARAM_DATA6, Common.AppType, DbType.String));
                    dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, errMsg, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                    dsAllInfo = dtManager.ExecuteDataSet(m_uspDistributorSearch, dbParam);
                    errMsg = dbParam[Common.PARAM_OUTPUT].Value.ToString();
                }
            }

            return dsAllInfo;
        }

        public bool UpdateDistributorInfo(ref string errorMessage)
        {
            try
            {
                bool returnValue = false;
                if (Common.AppType == ((int)Common.ApplicationType.BOS).ToString())
                {
                    using (DataTaskManager dt = new DataTaskManager(Common.HODB))
                    {
                        DBParameterList paramList = new DBParameterList();
                        paramList.Add(new DBParameter(Common.PARAM_DATA, Common.ToXml(this), DbType.String));
                        paramList.Add(new DBParameter(Common.PARAM_OUTPUT, errorMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));
                        dt.BeginTransaction();
                        dt.ExecuteNonQuery(SP_UPDATE_DISTRIBUTOR, paramList);
                        errorMessage = paramList[Common.PARAM_OUTPUT].Value.ToString();
                        if (string.IsNullOrEmpty(errorMessage) && string.IsNullOrEmpty(errorMessage))
                        {
                            dt.CommitTransaction();
                            returnValue = true;
                        }
                        else
                        {
                            dt.RollbackTransaction();
                        }
                    }
                }
                else
                {
                    using (DataTaskManager dt = new DataTaskManager())
                    {
                        DBParameterList paramList = new DBParameterList();
                        paramList.Add(new DBParameter(Common.PARAM_DATA, Common.ToXml(this), DbType.String));
                        paramList.Add(new DBParameter(Common.PARAM_OUTPUT, errorMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));
                        dt.BeginTransaction();
                        dt.ExecuteNonQuery(SP_UPDATE_DISTRIBUTOR, paramList);
                        errorMessage = paramList[Common.PARAM_OUTPUT].Value.ToString();
                        if (string.IsNullOrEmpty(errorMessage) && string.IsNullOrEmpty(errorMessage))
                        {
                            dt.CommitTransaction();
                            returnValue = true;
                        }
                        else
                        {
                            dt.RollbackTransaction();
                        }
                    }
                }
                return returnValue;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Added By Kaushik for Searching Distributor Navigation
        public DataSet SearchDistributorPosition(int distributorID, ref string errMsgDP)
        {
            DataSet dsDistributorPosition = null;
            DBParameterList dbParamDP;

            using (DataTaskManager dtManager = new DataTaskManager())
            {
                dbParamDP = new DBParameterList();
                dbParamDP.Add(new DBParameter("@DistributorId", distributorID, DbType.Int32));
                dbParamDP.Add(new DBParameter(Common.PARAM_OUTPUT, errMsgDP, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                dsDistributorPosition = dtManager.ExecuteDataSet(SP_GETDISTRIBUTOR_POSITION, dbParamDP);
                errMsgDP = dbParamDP[Common.PARAM_OUTPUT].Value.ToString();
            }

            return dsDistributorPosition;
        }

        //Added By Kaushik for Finding Next Distributor Id in Distributor Navigation
        public DataSet FindNextDistributorId(int distributorID, int navigation, ref string errMsgDP)
        {
            DataSet dsNextDistributorId = null;
            DBParameterList dbParamDP;

            using (DataTaskManager dtManager = new DataTaskManager())
            {
                dbParamDP = new DBParameterList();
                dbParamDP.Add(new DBParameter("@DistributorId", distributorID, DbType.Int32));
                dbParamDP.Add(new DBParameter("@Navigation", navigation, DbType.Int32));
                dbParamDP.Add(new DBParameter(Common.PARAM_OUTPUT, errMsgDP, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                dsNextDistributorId = dtManager.ExecuteDataSet(SP_GETNEXTDISTRIBUTOR_ID, dbParamDP);
                errMsgDP = dbParamDP[Common.PARAM_OUTPUT].Value.ToString();
            }
            
            return dsNextDistributorId;
        }

        #endregion

        #region GetDeistributorHistory
        /// <summary>
        /// This method is used to get the data of Distributor History.
        /// </summary>
        /// <param name="sDistributorId"></param>
        /// <returns></returns>
        public DataTable GetDistributorHistory(string sDistributorId)
        {
            DataTaskManager objDataManager;
            DBParameterList dbParam;
            string sErrorMessage = "";
            DataTable dtData = null;
            try
            {
                if (Common.AppType == ((int)Common.ApplicationType.BOS).ToString())
                {
                    objDataManager = new DataTaskManager(Common.HODB);
                    dbParam = new DBParameterList();
                    dbParam.Add(new DBParameter("distributorid", sDistributorId, DbType.String));
                    dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, sErrorMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));
                    dtData = objDataManager.ExecuteDataTable(DISTRIBUTORHISTORY, dbParam);
                }
                else
                {
                    objDataManager = new DataTaskManager();
                    dbParam = new DBParameterList();
                    dbParam.Add(new DBParameter("distributorid", sDistributorId, DbType.String));
                    dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, sErrorMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));
                    dtData = objDataManager.ExecuteDataTable(DISTRIBUTORHISTORY, dbParam);
                }
                return dtData;
            }            
            finally
            {
                objDataManager = null;
                dbParam = null;
                dtData = null;
            }

        }
        #endregion

        #region GetDeistributorHistory
        /// <summary>
        /// This method is used to get the data of Distributor History.
        /// </summary>
        /// <param name="sDistributorId"></param>
        /// <returns></returns>
        public DataTable GetInvoiceData(string sDistributorId,string sConditionParam)
        {
            DataTaskManager objDataManager;
            DBParameterList dbParam;
            string sErrorMessage = "";
            DataTable dtData = null;
            try
            {
                if (string.Compare(sConditionParam, "Day", true) != 0)
                {
                    objDataManager = new DataTaskManager(Common.HODB);
                    dbParam = new DBParameterList();
                    dbParam.Add(new DBParameter("DistributorID", sDistributorId, DbType.String));
                    dbParam.Add(new DBParameter("ConditionParam", sConditionParam, DbType.String));
                    dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, sErrorMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));
                    dtData = objDataManager.ExecuteDataTable(SP_GETINVOICEDATE, dbParam);
                }
                else
                {
                    objDataManager = new DataTaskManager();
                    dbParam = new DBParameterList();
                    dbParam.Add(new DBParameter("DistributorID", sDistributorId, DbType.String));
                    dbParam.Add(new DBParameter("ConditionParam", sConditionParam, DbType.String));
                    dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, sErrorMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));
                    dtData = objDataManager.ExecuteDataTable(SP_GETINVOICEDATE, dbParam);
                }
                return dtData;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                objDataManager = null;
                dbParam = null;
                dtData = null;
            }

        }
        #endregion

        #region GetBankBranch
        /// <summary>
        /// This method is used to get the Bank Branch details
        /// </summary>
        /// <param name="sDistributorId"></param>
        /// <returns></returns>
        public DataTable GetBankBranch()
        {
            DataTaskManager objDataManager;
            DBParameterList dbParam;
            string sErrorMessage = "";
            DataTable dtData = null;
            try
            {
                if (Common.AppType == ((int)Common.ApplicationType.BOS).ToString())
                {
                    objDataManager = new DataTaskManager(Common.HODB);
                    dbParam = new DBParameterList();
                    dtData = objDataManager.ExecuteDataTable(BANKBRANCHDETAIL, dbParam);
                }
                else
                {
                    objDataManager = new DataTaskManager();
                    dbParam = new DBParameterList();
                    dtData = objDataManager.ExecuteDataTable(BANKBRANCHDETAIL, dbParam);
                }
                return dtData;
            }
            finally
            {
                objDataManager = null;
                dbParam = null;
                dtData = null;
            }

        }


        #endregion

        public void SendSms(Distributor objDistributor,string sms)
        {
            try
            {
                if (sms == "")
                    return;
                
                string url = Common.GetMessage("INF0262", objDistributor.DistributorMobNumber.ToString(), sms);
                ProcessStartInfo startInfo = new ProcessStartInfo("iexplore.exe");
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                startInfo.Arguments = url;

                Process.Start(startInfo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet  CreateDatasetForPrint(string distributorId)
        {
            DataTaskManager dtTaskMangr;
            DBParameterList dbParam;
            string errMessage="";
            DataSet  dtData;
            try
            {
                dtTaskMangr = new DataTaskManager();
                dbParam = new DBParameterList();
                dbParam.Add(new DBParameter("@DistributorID", distributorId, DbType.String));
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, errMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));
                dtData = dtTaskMangr.ExecuteDataSet("Usp_RptDistributorDetail", dbParam);
                return dtData;
            }
            finally 
            {
                dtTaskMangr = null;
                dbParam = null;
                dtData = null;
            }
        }
        
    }
}
