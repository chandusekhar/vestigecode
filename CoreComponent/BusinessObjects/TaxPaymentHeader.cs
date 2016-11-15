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
    [Serializable]
    public class TaxPaymentHeader
    {
        #region Constants

        private const string USP_CHALLAN_SEARCH = "usp_TaxChallanSearch";
        private const string USP_CHALLAN_SAVE = "usp_TaxChallanSave";

        #endregion


        #region Properties

        public int ChallanId
        {
            get;
            set;
        }

        public string ChallanNo
        {
            get;
            set;
        }

        public string AcknowledgementNo
        {
            get;
            set;
        }

        public decimal DepositedAmount
        {
            get;
            set;
        }

        public decimal BalanceAmount
        {
            get;
            set;
        }

        public string FinancialYear
        {
            get;
            set;
        }

        public string BSRCode
        {
            get;
            set;
        }
        public string ChequeNo
        {
            get;
            set;
        }

        public DateTime Depositdate
        {
            get;
            set;
        }

        public int UserId
        {
            get;
            set;
        }

        public int Quarter
        {
            get;
            set;
        }

        public string DisplayDepositDate
        {
            get;
            set;
        }

        public string QuarterName
        {
            get;
            set;
        }
        
        public decimal DisplayDepositAmount
        {
            get
            {
                return Math.Round(DepositedAmount, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
            }
            set
            {
                throw new NotImplementedException("This property can not be explicitly set"); 
            }
        }
        
        public decimal DisplayBalanceAmount
        {
            get
            {
                return Math.Round(BalanceAmount, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
            }
            set
            {
                throw new NotImplementedException("This property can not be explicitly set"); 
            }
        }

        #endregion


        #region Methods

        public List<TaxPaymentHeader> SearchChallan(string financialYear, int quarter, string acknowledgeNo, string challanNo,  ref string errorMessage)
        {
            try
            {
                List<TaxPaymentHeader> lstTaxPaymentHeader = new List<TaxPaymentHeader>();

                using (DataTaskManager dt = new DataTaskManager())
                {
                    DBParameterList paramList = new DBParameterList();
                    paramList.Add(new DBParameter("FinancialYear", financialYear, DbType.String));
                    paramList.Add(new DBParameter("Quarter", quarter, DbType.Int32));
                    paramList.Add(new DBParameter("AcknowledgeNo", acknowledgeNo, DbType.String));
                    paramList.Add(new DBParameter("ChallanNo", challanNo, DbType.String));
                    paramList.Add(new DBParameter(Common.PARAM_OUTPUT, errorMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));
                    DataSet ds = dt.ExecuteDataSet(USP_CHALLAN_SEARCH, paramList);
                    errorMessage = paramList[Common.PARAM_OUTPUT].Value.ToString();
                    if (string.IsNullOrEmpty(errorMessage) && ds != null)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            TaxPaymentHeader obj = CreateHeaderObject(dr);
                            lstTaxPaymentHeader.Add(obj);
                        }
                        
                    }
                }
                return lstTaxPaymentHeader;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool SaveChallan(ref string errorMessage)
        {
            try
            {
                bool ret = false;
                List<TaxPaymentHeader> lstTaxPaymentHeader = new List<TaxPaymentHeader>();

                using (DataTaskManager dt = new DataTaskManager())
                {
                    DBParameterList paramList = new DBParameterList();
                    paramList.Add(new DBParameter(Common.PARAM_DATA, Common.ToXml(this), DbType.String));
                    paramList.Add(new DBParameter(Common.PARAM_OUTPUT, errorMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));
                    dt.ExecuteNonQuery(USP_CHALLAN_SAVE , paramList);
                    errorMessage = paramList[Common.PARAM_OUTPUT].Value.ToString();
                    if (string.IsNullOrEmpty(errorMessage))
                    {                        
                        ret = true;
                    }
                }
                return ret;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private TaxPaymentHeader CreateHeaderObject(DataRow dr)
        {
            TaxPaymentHeader objHeader = new TaxPaymentHeader();
            objHeader.ChallanId = Convert.ToInt32(dr["ChallanId"]);
            objHeader.ChallanNo = dr["ChallanNo"].ToString(); ;
            objHeader.ChequeNo = dr["ChequeNo"].ToString();
            objHeader.BSRCode = dr["BSRCode"].ToString();
            objHeader.AcknowledgementNo = dr["AcknowledgementNo"].ToString();
            objHeader.DepositedAmount = Convert.ToDecimal(dr["DepositedAmount"]);
            objHeader.BalanceAmount = Convert.ToDecimal(dr["BalanceAmount"]);
            objHeader.FinancialYear = dr["FinancialYear"].ToString();
            objHeader.Quarter = Convert.ToInt32(dr["Quarter"]);
            objHeader.QuarterName = Convert.ToString(dr["QuarterName"]);
            objHeader.Depositdate = Convert.ToDateTime(dr["DepositDate"]);
            objHeader.DisplayDepositDate = objHeader.Depositdate.ToString(Common.DTP_DATE_FORMAT);
            return objHeader;
        }

        #endregion
    }
}
