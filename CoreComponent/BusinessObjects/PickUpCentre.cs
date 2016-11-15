using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

//vinculum framework namespace(s)
using Vinculum.Framework.DataTypes;
using Vinculum.Framework.Data;
using CoreComponent.Core.BusinessObjects;


namespace CoreComponent.BusinessObjects
{
    namespace PickUpCentre
    {
        [Serializable]
        public class PUCAccount
        {
            #region Properties

            public int LocationTypeId
            {
                get;
                set;
            }

            public int LocationCodeId
            {
                get;
                set;
            }

            public string LocationCode
            {
                get;
                set;
            }

            public int PCId
            {
                get;
                set;
            }

            public String PCLocation
            {
                get;
                set;
            }

            public Int32 CurrentLocationId
            {
                get;
                set;
            }

            public decimal AvailAmt
            {
                get
                {
                    return GetAvailAmt(this);
                }
                set
                {
                    throw new NotImplementedException("This property can not be explicitly set");
                }
            }

            public decimal DBAvailAmt
            {
                get
                {
                    return Math.Round(AvailAmt, Common.DBAmountRounding, MidpointRounding.AwayFromZero);
                }
                set
                {
                    throw new NotImplementedException("This property can not be explicitly set");
                }
            }

            public String DisplayAvailAmt
            {
                get
                {
                    return Math.Round(DBAvailAmt, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero).ToString(Common.GetRoundingZeroes(Common.DisplayAmountRounding));
                }
                set
                {
                    throw new NotImplementedException("This property can not be explicitly set");
                }
            }

            public Decimal UsedAmount
            {
                get;
                set;
            }

            public List<PUCDeposit> Deposits
            {
                get;
                set;
            }

            public int CreatedBy
            {
                get;
                set;
            }

            public DateTime CreatedDate
            {
                get;
                set;
            }

            public int ModifiedBy
            {
                get;
                set;
            }

            public DateTime ModifiedDate
            {
                get;
                set;
            }

            #endregion

            #region Method
            /// <summary>
            /// Get the available-amount of a PUC-Account
            /// </summary>
            /// <param name="pucAccount">Object of PUCAccount BL-Class which holds the PUC-Account's info</param>
            /// <returns>decimal value of the total available-amount</returns>
            private decimal GetAvailAmt(PUCAccount pucAccount)
            {
                decimal totalAmount = 0.0000m;
                foreach (PUCDeposit pdep in pucAccount.Deposits)
                {
                    totalAmount += pdep.Amount;
                }
                return totalAmount;
            }

            #endregion
        }

        [Serializable]
        public class PUCDeposit
        {
            #region Properties

            public int LocationCodeId
            {
                get;
                set;
            }

            public int PCId
            {
                get;
                set;
            }

            public string RecordNo
            {
                get;
                set;
            }

            public int PaymentModeId
            {
                get;
                set;
            }

            public string PaymentModeType
            {
                get;
                set;
            }

            public string TransactionNo
            {
                get;
                set;
            }

            public decimal Amount
            {
                get;
                set;
            }

            public decimal DBAmount
            {
                get
                {
                    return Math.Round(Amount, Common.DBAmountRounding, MidpointRounding.AwayFromZero);
                }
                set
                {
                    throw new NotImplementedException("This property can not be explicitly set");
                }
            }

            public String DisplayAmount
            {
                get
                {
                    return Math.Round(DBAmount, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero).ToString(Common.GetRoundingZeroes(Common.DisplayAmountRounding));
                }
                set
                {
                    throw new NotImplementedException("This property can not be explicitly set");
                }
            }

            public DateTime Date
            {
                get;
                set;
            }

            public String DisplayDate
            {
                get
                {
                    if ((Date != null) && (Date.ToString().Length > 0))
                    {
                        return Date.ToString(Common.DTP_DATE_FORMAT);
                    }
                    else
                    {
                        return String.Empty;
                    }
                }
                set
                {
                    throw new NotImplementedException("This property can not be explicitly set");
                }
            }

            public int CreatedBy
            {
                get;
                set;
            }

            public DateTime CreatedDate
            {
                get;
                set;
            }

            public int ModifiedBy
            {
                get;
                set;
            }

            public DateTime ModifiedDate
            {
                get;
                set;
            }

            #endregion
        }

        public class PUCCommonTransaction
        {
            #region Constants
            private const string m_uspDataSearch = "usp_getPUCInfo";
            private const string m_uspDataSave = "usp_SavePUCInfo";
            #endregion

            #region Constructor
            public PUCCommonTransaction()
            {

            }
            #endregion

            #region Enums
            public enum Locations
            {
                LocationCode = 2,
                PUCCode = 3
            }

            public enum LocationTypes
            {
                HO = 1,
                WH = 2,
                BO = 3,
                PC = 4
            }
            #endregion

            #region Methods
            /// <summary>
            /// Get the records related to PUC account/deposits
            /// </summary>
            /// <param name="locCode">LocationId of the concerned BO</param>
            /// <param name="pucId">LocationId of the concerned PUC</param>
            /// <param name="paymentMode">Mode-of-payment in which order was paid</param>
            /// <param name="errorMessage">Error-Message variable; to store the error-message during the Fetch-operation</param>
            /// <returns>DataTable containing LOCCODEID,PUCLOCID,PUCLOCATION,(PCID),RECORDNO,AVAILABLEAMOUNT,DEPOSITAMOUNT,DEPOSITDATE,DEPOSITMODE,DEPOSITTYPE,TRANSACTIONNO,MODIFIEDDATE</returns>
            public DataTable FetchPUCInfo(int locCodeId, int pucId, int paymentMode,string transNo,string date,int mode,int depmnt, ref string errorMessage)
            {
                DataTable dtPUCInfo = null;
                DBParameterList dbParam;
                using (DataTaskManager dtManager = new DataTaskManager())
                {
                    dbParam = new DBParameterList();

                    dbParam.Add(new DBParameter(Common.PARAM_DATA, locCodeId, DbType.Int32));
                    dbParam.Add(new DBParameter(Common.PARAM_DATA2, pucId, DbType.Int32));
                    dbParam.Add(new DBParameter(Common.PARAM_DATA3, paymentMode, DbType.Int32));
                    dbParam.Add(new DBParameter(Common.PARAM_DATA4, transNo, DbType.String));
                    dbParam.Add(new DBParameter(Common.PARAM_DATA5, date, DbType.String));
                    dbParam.Add(new DBParameter(Common.PARAM_DATA6, mode, DbType.Int32));
                    dbParam.Add(new DBParameter(Common.PARAM_DATA7, depmnt, DbType.Int32));
                    dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, errorMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                    dtPUCInfo = dtManager.ExecuteDataTable(m_uspDataSearch, dbParam);
                    errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();
                }

                return dtPUCInfo;
            }

            /// <summary>
            /// Save a new/existing PUC account-record; new with 1st deposit(s), existing with added new deposit(s)
            /// </summary>
            /// <param name="objPUCAccount">Object of the PUCAccount BL-Class which contains info of the PUC-Account and its deposit(s)</param>
            /// <param name="errorMessage">Error-Message variable; to store the error-message during the Save-transaction</param>
            /// <returns>bool value stating whether the Save-transaction was successful or not</returns>
            public bool SavePUCInfo(PUCAccount objPUCAccount, ref string errorMessage)
            {
                DBParameterList dbParam;
                using (DataTaskManager dtManager = new DataTaskManager())
                {
                    dbParam = new DBParameterList();
                    dbParam.Add(new DBParameter(Common.PARAM_DATA, Common.ToXml(objPUCAccount), DbType.String));
                    dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, errorMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                    dtManager.ExecuteNonQuery(m_uspDataSave, dbParam);
                    errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();
                }

                if (string.IsNullOrEmpty(errorMessage))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            #endregion
        }
    }
}
