using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CoreComponent.Core.BusinessObjects;
using Microsoft.Reporting.WinForms;
using System.Data;
using Vinculum.Framework.Data;
using Vinculum.Framework;
using Vinculum.Framework.DataTypes;

namespace CoreComponent.BusinessObjects.Reports
{
    [Serializable]
    public class DistributorPayment 
    {
        private const string SP_RPT_CHEQUECREDITCARD = "usp_RptChequeCreditCard";

        public static bool ValidatePaymentReport(ref string dbMessage, string dtFrom, string dtTo, int locationCode)
        {
            bool ret = true;
            StringBuilder sb = new StringBuilder();
            
            if ((dtFrom != string.Empty && Convert.ToDateTime(dtFrom) > DateTime.Now))
            {
                sb.Append(Common.GetMessage("VAL0060", "Date From", "Current Date"));
                sb.AppendLine();
                ret = false;
            }
            if ((dtTo != string.Empty && Convert.ToDateTime(dtTo) > DateTime.Now))
            {
                sb.Append(Common.GetMessage("VAL0060", "Date To", "Current Date"));
                sb.AppendLine();
                ret = false;
            }
            if (dtFrom != string.Empty && dtTo != string.Empty && Convert.ToDateTime(dtFrom) > Convert.ToDateTime(dtTo))
            {
                sb.Append(Common.GetMessage("VAL0060", "Date From", "Date To"));
                sb.AppendLine();
                ret = false;
            }
            if(ret == true)
                ret = true;
            else
                MessageBox.Show(Common.ReturnErrorMessage(sb).ToString(), Common.VALIDATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return ret;
        }

        public static bool ValidateCashBookReport(ref string dbMessage, string dtFrom, string dtTo, int locationCode)
        {
            bool ret = true;
            StringBuilder sb = new StringBuilder();

            if ((dtFrom != string.Empty && Convert.ToDateTime(dtFrom) > DateTime.Now))
            {
                sb.Append(Common.GetMessage("VAL0060", "Date From", "Current Date"));
                sb.AppendLine();
                ret = false;
            }
            if ((dtTo != string.Empty && Convert.ToDateTime(dtTo) > DateTime.Now))
            {
                sb.Append(Common.GetMessage("VAL0060", "Date To", "Current Date"));
                sb.AppendLine();
                ret = false;
            }
            if (dtFrom != string.Empty && dtTo != string.Empty && Convert.ToDateTime(dtFrom) > Convert.ToDateTime(dtTo))
            {
                sb.Append(Common.GetMessage("VAL0060", "Date From", "Date To"));
                sb.AppendLine();
                ret = false;
            }
            if (ret == true)
                ret = true;
            else
                MessageBox.Show(Common.ReturnErrorMessage(sb).ToString(), Common.VALIDATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return ret;
        }
        
        public static List<ReportDataSource> ChequePayment(ref string errorMessage, string dateFrom, string dateTo,int locationId)
        {
            List<ReportDataSource> lrds = new List<ReportDataSource>();
            try
            {
                DBParameterList dbParam;
                Vinculum.Framework.Data.DataTaskManager dtManager = new DataTaskManager();
                dbParam = new DBParameterList();
                dbParam.Add(new DBParameter("@paymentMode", 1, DbType.Int32));
                dbParam.Add(new DBParameter("@fromDate", dateFrom, DbType.String));
                dbParam.Add(new DBParameter("@toDate", dateTo, DbType.String));
                dbParam.Add(new DBParameter("@locationId", locationId, DbType.Int32));
                //dbParam.Add(new DBParameter("@pcId", -1, DbType.Int32));
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));
                DataSet ds = dtManager.ExecuteDataSet(SP_RPT_CHEQUECREDITCARD, dbParam);
                errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();

                // if an error returned from the database
                if (errorMessage != string.Empty)
                    return null;
                else
                {
                    if ((ds != null) && (ds.Tables.Count > 0) && (ds.Tables[0].Rows.Count > 0))
                    {
                        ds = ModifyDataSet(ds);
                        lrds.Add(new ReportDataSource("PaymentChequeCreditCard_PaymentChequeCreditCardDataTable", ds.Tables[0]));
                    }
                }
            }
            catch (Exception ex)
            {
                Vinculum.Framework.Logging.LogManager.WriteExceptionLog(ex);
            }
            return lrds;

        }

        public static List<ReportDataSource> CreditCardPayment(ref string errorMessage, string dateFrom, string dateTo, int locationId)
        {
            List<ReportDataSource> lrds = new List<ReportDataSource>();
            try
            {
                DBParameterList dbParam;
                Vinculum.Framework.Data.DataTaskManager dtManager = new DataTaskManager();
                dbParam = new DBParameterList();
                dbParam.Add(new DBParameter("@paymentMode", 2, DbType.Int32));
                dbParam.Add(new DBParameter("@fromDate", dateFrom, DbType.String));
                dbParam.Add(new DBParameter("@toDate", dateTo, DbType.String));
                dbParam.Add(new DBParameter("@locationId", locationId, DbType.Int32));
                //dbParam.Add(new DBParameter("@pcId", -1, DbType.Int32));
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));
                DataSet ds = dtManager.ExecuteDataSet(SP_RPT_CHEQUECREDITCARD, dbParam);
                errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();

                // if an error returned from the database
                if (errorMessage != string.Empty)
                    return null;
                else
                {
                    if ((ds != null) && (ds.Tables.Count > 0) && (ds.Tables[0].Rows.Count > 0))
                    {
                        ds = ModifyDataSet(ds);
                        lrds.Add(new ReportDataSource("PaymentChequeCreditCard_PaymentChequeCreditCardDataTable", ds.Tables[0]));
                    }
                }
            }
            catch (Exception ex)
            {
                Vinculum.Framework.Logging.LogManager.WriteExceptionLog(ex);
            }
            return lrds;

        }

        public static List<ReportDataSource> CashBookPayment(ref string errorMessage, string dateFrom, string dateTo, int locationId)
        {
            List<ReportDataSource> lrds = new List<ReportDataSource>();
            try
            {
                DBParameterList dbParam;
                Vinculum.Framework.Data.DataTaskManager dtManager = new DataTaskManager();
                dbParam = new DBParameterList();
                dbParam.Add(new DBParameter("@paymentMode", 3, DbType.Int32));
                dbParam.Add(new DBParameter("@fromDate", dateFrom, DbType.String));
                dbParam.Add(new DBParameter("@toDate", dateTo, DbType.String));
                dbParam.Add(new DBParameter("@locationId", locationId, DbType.Int32));
                //dbParam.Add(new DBParameter("@pcId", pcId, DbType.Int32));
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));
                DataSet ds = dtManager.ExecuteDataSet(SP_RPT_CHEQUECREDITCARD, dbParam);
                errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();

                // if an error returned from the database
                if (errorMessage != string.Empty)
                    return null;
                else
                {
                    if ((ds != null) && (ds.Tables.Count > 0) && (ds.Tables[0].Rows.Count > 0))
                    {
                        ds = ModifyDataSet(ds);
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            ds.Tables[0].Rows[i]["PaymentAmount"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["PaymentAmount"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                        }
                        lrds.Add(new ReportDataSource("PaymentChequeCreditCard_PaymentChequeCreditCardDataTable", ds.Tables[0]));
                    
                    }
                }
            }
            catch (Exception ex)
            {
                Vinculum.Framework.Logging.LogManager.WriteExceptionLog(ex);
            }
            return lrds;

        }

        public static List<ReportDataSource> BankBookPayment(ref string errorMessage, string dateFrom, string dateTo, int locationId)
        {
            List<ReportDataSource> lrds = new List<ReportDataSource>();
            try
            {
                DBParameterList dbParam;
                Vinculum.Framework.Data.DataTaskManager dtManager = new DataTaskManager();
                dbParam = new DBParameterList();
                dbParam.Add(new DBParameter("@paymentMode", 4, DbType.Int32));
                dbParam.Add(new DBParameter("@fromDate", dateFrom, DbType.String));
                dbParam.Add(new DBParameter("@toDate", dateTo, DbType.String));
                dbParam.Add(new DBParameter("@locationId", locationId, DbType.Int32));
                //dbParam.Add(new DBParameter("@pcId", -1, DbType.Int32));
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));
                DataSet ds = dtManager.ExecuteDataSet(SP_RPT_CHEQUECREDITCARD, dbParam);
                errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();

                // if an error returned from the database
                if (errorMessage != string.Empty)
                    return null;
                else
                {
                    if ((ds != null) && (ds.Tables.Count > 0) && (ds.Tables[0].Rows.Count > 0))
                    {
                        ds = ModifyDataSet(ds);
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {                            
                            ds.Tables[0].Rows[i]["PaymentAmount"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["PaymentAmount"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                        }
                        lrds.Add(new ReportDataSource("PaymentChequeCreditCard_PaymentChequeCreditCardDataTable", ds.Tables[0]));
                    }
                }
            }
            catch (Exception ex)
            {
                Vinculum.Framework.Logging.LogManager.WriteExceptionLog(ex);
            }
            return lrds;

        }
        
        
        private static DataSet ModifyDataSet(DataSet ds)
        {
            string address = Common.ReportHeaderAddress();
            string headerText = address.Substring(0, address.IndexOf("*|$|*"));
            string addressText = address.Substring(address.IndexOf("*|$|*") + 5, address.Length - address.IndexOf("*|$|*") - 5);
            addressText = addressText.Replace("*|$|*", Environment.NewLine);
            ds.Tables[0].Columns.Add(new DataColumn("FromDateText", Type.GetType("System.String")));
            ds.Tables[0].Columns.Add(new DataColumn("ToDateText", Type.GetType("System.String")));
            ds.Tables[0].Columns.Add(new DataColumn("HeaderAddress", Type.GetType("System.String")));
            ds.Tables[0].Columns.Add(new DataColumn("AddressText", Type.GetType("System.String")));
            ds.Tables[0].Rows[0]["HeaderAddress"] = headerText;
            ds.Tables[0].Rows[0]["AddressText"] = addressText;
            ds.Tables[0].Columns.Add(new DataColumn("PaymentDateText", Type.GetType("System.String")));
            ds.Tables[0].Columns.Add(new DataColumn("InvoiceDateText", Type.GetType("System.String")));
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                ds.Tables[0].Rows[i]["OpeningBalance"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["OpeningBalance"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                ds.Tables[0].Rows[i]["Amount"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["Amount"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                ds.Tables[0].Rows[i]["InvoiceDateText"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["InvoiceDate"]).ToString(Common.DTP_DATE_FORMAT);
                ds.Tables[0].Rows[i]["PaymentDateText"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["PaymentDate"]).ToString(Common.DTP_DATE_FORMAT);
                ds.Tables[0].Rows[i]["FromDateText"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["FromDate"]).ToString(Common.DTP_DATE_FORMAT);
                ds.Tables[0].Rows[i]["ToDateText"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["ToDate"]).ToString(Common.DTP_DATE_FORMAT);
            }
            return ds;
        }

    }
}
