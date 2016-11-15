using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CoreComponent.Core.BusinessObjects;
using Vinculum.Framework.Data;
using Vinculum.Framework.DataTypes;

namespace POSClient.BusinessObjects
{
    [Serializable]
    public class Currency
    {
        #region Private Variables

        private string m_currencyCode;
        private string m_currencyName;
        private double m_conversionRate;
        private bool m_isBaseCurrency;
        private static List<Currency> s_currencyList;

        #endregion

        #region C'tors

        static Currency()
        {
            ResetCurrencies();
        }


        #endregion

        #region Properties

        public static List<Currency> CurrencyList
        {
            get { return s_currencyList; }
            set { Currency.s_currencyList = value; }
        }

        public static Currency BaseCurrency
        {
            get
            {
                return s_currencyList.Find(delegate(Currency c) { return c.IsBaseCurrency == true; });
            }
        }

        public bool IsBaseCurrency
        {
            get { return m_isBaseCurrency; }
            set { m_isBaseCurrency = value; }
        }

        public string CurrencyCode
        {
            get { return m_currencyCode; }
            set { m_currencyCode = value; }
        }

        public string CurrencyName
        {
            get { return m_currencyName; }
            set { m_currencyName = value; }
        }

        public double ConversionRate
        {
            get { return m_conversionRate; }
            set { m_conversionRate = value; }
        }

        #endregion

        #region Public Methods
        #endregion

        #region Static Methods

        public static void ResetCurrencies()
        {
            string dbMessage = string.Empty;
            s_currencyList = new List<Currency>();
            Currency.GetCurrenciesForPOS(ref dbMessage, Common.LOCATION_ID);
        }

        /// <summary>
        /// Returns all currencies for a given service center including inactive
        /// </summary>
        /// <param name="dbMessage">DB Message</param>
        /// <param name="locationCode">ServiceCenter Code</param>
        /// <returns></returns>
        private static List<Currency> GetCurrenciesForPOS(ref string dbMessage, string locationCode)
        {
            try
            {
                DBParameterList dbParamList = new DBParameterList();
                dbParamList.Add(new DBParameter("@inputParam", locationCode, DbType.String));
                dbParamList.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));
                using (DataTaskManager dtManager = new DataTaskManager())
                {
                    using (DataTable dt = dtManager.ExecuteDataTable("usp_POSCurrencySearch", dbParamList))
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                Currency currency = new Currency();
                                currency.CurrencyCode = dt.Rows[i]["CurrencyCode"].ToString();
                                currency.ConversionRate = Convert.ToDouble(dt.Rows[i]["ConversionRate"]);
                                currency.CurrencyName = dt.Rows[i]["CurrencyName"].ToString();
                                currency.IsBaseCurrency = Convert.ToBoolean(dt.Rows[i]["IsBaseCurrency"]);
                                s_currencyList.Add(currency);
                            }
                        }
                    }
                    // update database message
                    dbMessage = dbParamList[Common.PARAM_OUTPUT].Value.ToString();
                }
                return s_currencyList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Returns Conversion Rate in Base Currency
        /// </summary>
        /// <param name="fromCode"></param>
        /// <returns></returns>
        public static double GetConversionRate(string fromCode)
        {
            return s_currencyList.Find(delegate(Currency c) { return c.CurrencyCode == fromCode; }).ConversionRate;
        }

        #endregion
    }
}
