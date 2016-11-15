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
    public class FinanceReports
    {
        #region SP Declaration
        private const string SP_RPT_PURCHASEAGTFORMC = "usp_rptPurchaseAgtFormC";
        private const string SP_RPT_STNFINANCE = "usp_rptStnFinance";
        private const string SP_RPT_SALESTAX = "usp_rptSalesTax";
        private const string SP_RPT_VATCST = "usp_rptVATCST";
        private const string SP_RPT_TAXREGISTER = "usp_rptTaxRegister";
        private const string SP_RPT_CREDIT_TAXREGISTER = "usp_rptCreditTaxRegister";
        private const string SP_RPT_Dcc_BO_Sales = "usp_DccBoRptSales";
        private const string SP_RPT_SALESRETURNREGISTER = "usp_rptSalesReturnRegister";
        private const string SP_RPT_PaymentStatusSummary = "usp_rptPaymentStatusSummary";
        #endregion

        #region Validations
        public static bool ValidatePurchaseAgtFormC(ref string dbMessage, string dtFrom, string dtTo)
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
            if (ret != true)
                MessageBox.Show(Common.ReturnErrorMessage(sb).ToString(), Common.VALIDATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return ret;
        }

        public static bool ValidateSTNFinance(ref string errorMessage, string dtFrom, string dtTo, int from, int to)
        {
            bool ret = true;
            StringBuilder sb = new StringBuilder();
            if ((dtFrom != string.Empty && Convert.ToDateTime(dtFrom) > DateTime.Now))
            {
                sb.Append(Common.GetMessage("VAL0060", "Date From", "Current Date"));
                sb.AppendLine();
                ret = false;
            }
            else if ((dtTo != string.Empty && Convert.ToDateTime(dtTo) > DateTime.Now))
            {
                sb.Append(Common.GetMessage("VAL0060", "Date To", "Current Date"));
                sb.AppendLine();
                ret = false;
            }
            else if (dtFrom != string.Empty && dtTo != string.Empty && Convert.ToDateTime(dtFrom) > Convert.ToDateTime(dtTo))
            {
                sb.Append(Common.GetMessage("VAL0060", "Date From", "Date To"));
                sb.AppendLine();
                ret = false;
            }
            else
                ret = true;
            if (ret == false)
                MessageBox.Show(Common.ReturnErrorMessage(sb).ToString(), Common.VALIDATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return ret;
        }

        public static bool ValidateVATCST(ref string errorMessage, string dtFrom, string dtTo, int from)
        {
            bool ret = true;
            StringBuilder sb = new StringBuilder();
            if ((dtFrom != string.Empty && Convert.ToDateTime(dtFrom) > DateTime.Now))
            {
                sb.Append(Common.GetMessage("VAL0060", "Date From", "Current Date"));
                sb.AppendLine();
                ret = false;
            }
            else if ((dtTo != string.Empty && Convert.ToDateTime(dtTo) > DateTime.Now))
            {
                sb.Append(Common.GetMessage("VAL0060", "Date To", "Current Date"));
                sb.AppendLine();
                ret = false;
            }
            else if (dtFrom != string.Empty && dtTo != string.Empty && Convert.ToDateTime(dtFrom) > Convert.ToDateTime(dtTo))
            {
                sb.Append(Common.GetMessage("VAL0060", "Date From", "Date To"));
                sb.AppendLine();
                ret = false;
            }
            else
                ret = true;
            if (ret == false)
                MessageBox.Show(Common.ReturnErrorMessage(sb).ToString(), Common.VALIDATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return ret;
        }

        public static bool ValidateSalesTax(ref string errorMessage, int BOlocationId, int month, int year)
        {
            bool ret = true;
            StringBuilder sb = new StringBuilder();
            if (month > DateTime.Today.Month)
            {
                sb.Append(Common.GetMessage("VAL0060", "Month", "Current Month"));
                sb.AppendLine();
                ret = false;
            }
            else if (year > DateTime.Today.Year)
            {
                sb.Append(Common.GetMessage("VAL0060", "Year", "Current Year"));
                sb.AppendLine();
                ret = false;
            }           
            else
                ret = true;
            if (ret == false)
                MessageBox.Show(Common.ReturnErrorMessage(sb).ToString(), Common.VALIDATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return ret;
        }

        public static bool ValidateMonthYear(ref string errorMessage, int month, int year, int BOlocationID)
        {
            bool ret = true;
            StringBuilder sb = new StringBuilder();
            if ((month > DateTime.Today.Month))
            {
                sb.Append(Common.GetMessage("VAL0060", "Month", "Current Month"));
                sb.AppendLine();
                ret = false;
            }

            else if ((year > DateTime.Today.Year))
            {
                sb.Append(Common.GetMessage("VAL0060", "Year", "Current Year"));
                sb.AppendLine();
                ret = false;
            }

            else if (BOlocationID == -1)
            {
                MessageBox.Show(Common.GetMessage("VAL0002", "Location"), Common.VALIDATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else ret = true;
            if (ret == false)
                MessageBox.Show(Common.ReturnErrorMessage(sb).ToString(), Common.VALIDATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return ret;
        }
        
        #endregion

        #region Methods
        
        public static bool ValidateTaxRegister(ref string errorMessage, string dtFrom, string dtTo, int location)
        {
            bool ret = true;
            StringBuilder sb = new StringBuilder();
            if ((dtFrom != string.Empty && Convert.ToDateTime(dtFrom) > DateTime.Now))
            {
                sb.Append(Common.GetMessage("VAL0060", "Date From", "Current Date"));
                sb.AppendLine();
                ret = false;
            }
            else if ((dtTo != string.Empty && Convert.ToDateTime(dtTo) > DateTime.Now))
            {
                sb.Append(Common.GetMessage("VAL0060", "Date To", "Current Date"));
                sb.AppendLine();
                ret = false;
            }
            else if (dtFrom != string.Empty && dtTo != string.Empty && Convert.ToDateTime(dtFrom) > Convert.ToDateTime(dtTo))
            {
                sb.Append(Common.GetMessage("VAL0060", "Date From", "Date To"));
                sb.AppendLine();
                ret = false;
            }
            else
                ret = true;

            if (ret == false)
            {
                MessageBox.Show(Common.ReturnErrorMessage(sb).ToString(), Common.VALIDATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (location == -1)
            {
                MessageBox.Show(Common.GetMessage("VAL0002","Location"), Common.VALIDATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return ret;
        }


        public static bool ValidateCreditTaxRegister(ref string errorMessage, string dtFrom, string dtTo, int location, int reportType)
        {
            bool ret = true;
            StringBuilder sb = new StringBuilder();
            if ((dtFrom != string.Empty && Convert.ToDateTime(dtFrom) > DateTime.Now))
            {
                sb.Append(Common.GetMessage("VAL0060", "Date From", "Current Date"));
                sb.AppendLine();
                ret = false;
            }
            else if ((dtTo != string.Empty && Convert.ToDateTime(dtTo) > DateTime.Now))
            {
                sb.Append(Common.GetMessage("VAL0060", "Date To", "Current Date"));
                sb.AppendLine();
                ret = false;
            }
            else if (dtFrom != string.Empty && dtTo != string.Empty && Convert.ToDateTime(dtFrom) > Convert.ToDateTime(dtTo))
            {
                sb.Append(Common.GetMessage("VAL0060", "Date From", "Date To"));
                sb.AppendLine();
                ret = false;
            }
            else if (reportType == -1)
            {
                sb.Append(Common.GetMessage("VAL0002", "Report Type"));
                sb.AppendLine();
                ret = false;
            }
            else
                ret = true;

            if (ret == false)
            {
                MessageBox.Show(Common.ReturnErrorMessage(sb).ToString(), Common.VALIDATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (location == -1)
            {
                MessageBox.Show(Common.GetMessage("VAL0002", "Location"), Common.VALIDATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return ret;
        }




        public static bool ValidateSalesReturnRegister(ref string errorMessage, string dtFrom, string dtTo, int location)
        {
            bool ret = true;
            StringBuilder sb = new StringBuilder();
            if ((dtFrom != string.Empty && Convert.ToDateTime(dtFrom) > DateTime.Now))
            {
                sb.Append(Common.GetMessage("VAL0060", "Date From", "Current Date"));
                sb.AppendLine();
                ret = false;
            }
            else if ((dtTo != string.Empty && Convert.ToDateTime(dtTo) > DateTime.Now))
            {
                sb.Append(Common.GetMessage("VAL0060", "Date To", "Current Date"));
                sb.AppendLine();
                ret = false;
            }
            else if (dtFrom != string.Empty && dtTo != string.Empty && Convert.ToDateTime(dtFrom) > Convert.ToDateTime(dtTo))
            {
                sb.Append(Common.GetMessage("VAL0060", "Date From", "Date To"));
                sb.AppendLine();
                ret = false;
            }
            else
                ret = true;

            if (ret == false)
            {
                MessageBox.Show(Common.ReturnErrorMessage(sb).ToString(), Common.VALIDATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (location == -1)
            {
                MessageBox.Show(Common.GetMessage("VAL0002", "Location"), Common.VALIDATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return ret;
        }



        public static bool ValidatePaymentStatusSummary(ref string dbMessage, int Month, int Year, string DistributorID, int chequenumber)
        {
            bool ret = true;
            StringBuilder sb = new StringBuilder();
            //if (Month > DateTime.Today.Month)
            //{
            //    sb.Append(Common.GetMessage("VAL0060", "Month", "Current Month"));
            //    sb.AppendLine();
            //    ret = false;
            //}
            //else 
            if (Year > DateTime.Today.Year)
            {
                sb.Append(Common.GetMessage("VAL0060", "Year", "Current Year"));
                sb.AppendLine();
                ret = false;
            }
            else
                ret = true;
            if (ret == false)
                MessageBox.Show(Common.ReturnErrorMessage(sb).ToString(), Common.VALIDATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return ret;
        }

        
        
        public static List<ReportDataSource> TaxRegister(ref string errorMessage, string dtFrom, string dtTo, int location)
        {
            List<ReportDataSource> lrds = new List<ReportDataSource>();
            try
            {
                DBParameterList dbParam;

                Vinculum.Framework.Data.DataTaskManager dtManager = new DataTaskManager();

                // initialize the parameter list object
                dbParam = new DBParameterList();

                // add the relevant 2 parameters
                //dbParam.Add(new DBParameter("@InvoiceType", invoiceType, DbType.String));

                dbParam.Add(new DBParameter("@dtFrom", dtFrom, DbType.String));
                dbParam.Add(new DBParameter("@dtTo", dtTo, DbType.String));
                dbParam.Add(new DBParameter("@location", location, DbType.Int32));
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                // executing procedure to save the record 
                DataSet ds = dtManager.ExecuteDataSet(SP_RPT_TAXREGISTER, dbParam);

                // update database message
                errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();

                // if an error returned from the database
                if (errorMessage != string.Empty)
                    return null;
                else
                {
                    if (ds.Tables[0].Rows.Count > 0)
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
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            //ds.Tables[0].Rows[i]["TaxGroupCode"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["TaxGroupCode"]), Common.DisplayAmountRounding);
                            ds.Tables[0].Rows[i]["TaxAmount"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["TaxAmount"]), Common.DisplayAmountRounding);
                            ds.Tables[0].Rows[i]["Sale"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["Sale"]), Common.DisplayAmountRounding);

                            ds.Tables[0].Rows[i]["InvoiceDate"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["InvoiceDate"]).ToString(Common.DTP_DATE_FORMAT);

                            ds.Tables[0].Rows[i]["FromDateText"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["dtFrom"]).ToString(Common.DTP_DATE_FORMAT);
                            ds.Tables[0].Rows[i]["ToDateText"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["dtTo"]).ToString(Common.DTP_DATE_FORMAT);
                        }
                        lrds.Add(new ReportDataSource("TaxRegister_TaxRegisterDataTable", ds.Tables[0]));
                    }
                }
            }
            catch (Exception ex)
            {
                Vinculum.Framework.Logging.LogManager.WriteExceptionLog(ex);
            }
            return lrds;
        }


        public static List<ReportDataSource> CreditTaxRegister(ref string errorMessage, string dtFrom, string dtTo, int location, int reportType)
        {
            List<ReportDataSource> lrds = new List<ReportDataSource>();
            try
            {
                DBParameterList dbParam;

                Vinculum.Framework.Data.DataTaskManager dtManager = new DataTaskManager();

                // initialize the parameter list object
                dbParam = new DBParameterList();

                // add the relevant 2 parameters
                //dbParam.Add(new DBParameter("@InvoiceType", invoiceType, DbType.String));

                dbParam.Add(new DBParameter("@dtFrom", dtFrom, DbType.String));
                dbParam.Add(new DBParameter("@dtTo", dtTo, DbType.String));
                dbParam.Add(new DBParameter("@location", location, DbType.Int32));
                dbParam.Add(new DBParameter("@TaxTye", reportType, DbType.Int32));
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                // executing procedure to save the record 
                DataSet ds = dtManager.ExecuteDataSet(SP_RPT_CREDIT_TAXREGISTER, dbParam);

                // update database message
                errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();

                // if an error returned from the database
                if (errorMessage != string.Empty)
                    return null;
                else
                {
                    if (ds.Tables[0].Rows.Count > 0)
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
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            //ds.Tables[0].Rows[i]["TaxGroupCode"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["TaxGroupCode"]), Common.DisplayAmountRounding);
                            ds.Tables[0].Rows[i]["TaxAmount"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["TaxAmount"]), Common.DisplayAmountRounding);
                            ds.Tables[0].Rows[i]["Sale"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["Sale"]), Common.DisplayAmountRounding);
                            ds.Tables[0].Rows[i]["NewSale"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["NewSale"]), Common.DisplayAmountRounding);
                            ds.Tables[0].Rows[i]["NewTax"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["NewTax"]), Common.DisplayAmountRounding);
                            ds.Tables[0].Rows[i]["BonusPercent"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["BonusPercent"]), Common.DisplayQtyRounding);


                            ds.Tables[0].Rows[i]["InvoiceDate"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["InvoiceDate"]).ToString(Common.DTP_DATE_FORMAT);

                            ds.Tables[0].Rows[i]["FromDateText"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["dtFrom"]).ToString(Common.DTP_DATE_FORMAT);
                            ds.Tables[0].Rows[i]["ToDateText"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["dtTo"]).ToString(Common.DTP_DATE_FORMAT);
                        }
                        lrds.Add(new ReportDataSource("CreditTaxRegister_CreditTaxRegisterDataTable", ds.Tables[0]));
                    }
                }
            }
            catch (Exception ex)
            {
                Vinculum.Framework.Logging.LogManager.WriteExceptionLog(ex);
            }
            return lrds;
        }



        public static List<ReportDataSource> PurchaseAgtFormC(ref string errorMessage, string dtFrom, string dtTo)
        {
            List<ReportDataSource> lrds = new List<ReportDataSource>();
            try
            {
                DBParameterList dbParam;

                Vinculum.Framework.Data.DataTaskManager dtManager = new DataTaskManager();

                // initialize the parameter list object
                dbParam = new DBParameterList();

                // add the relevant 2 parameters
                dbParam.Add(new DBParameter("@dtFrom", dtFrom, DbType.String));
                dbParam.Add(new DBParameter("@dtTo", dtTo, DbType.String));
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                // executing procedure to save the record 
                DataSet ds = dtManager.ExecuteDataSet(SP_RPT_PURCHASEAGTFORMC, dbParam);

                // update database message
                errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();

                // if an error returned from the database
                if (errorMessage != string.Empty)
                    return null;
                else
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        string address = Common.ReportHeaderAddress();
                        string headerText = address.Substring(0, address.IndexOf("*|$|*"));
                        string addressText = address.Substring(address.IndexOf("*|$|*") + 5, address.Length - address.IndexOf("*|$|*") - 5);
                        addressText = addressText.Replace("*|$|*", Environment.NewLine);
                        ds.Tables[0].Columns.Add(new DataColumn("GRNDateText", Type.GetType("System.String")));
                        ds.Tables[0].Columns.Add(new DataColumn("FromDateText", Type.GetType("System.String")));
                        ds.Tables[0].Columns.Add(new DataColumn("ToDateText", Type.GetType("System.String")));
                        ds.Tables[0].Columns.Add(new DataColumn("HeaderAddress", Type.GetType("System.String")));
                        ds.Tables[0].Columns.Add(new DataColumn("AddressText", Type.GetType("System.String")));
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            ds.Tables[0].Rows[i]["GRNDateText"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["GRNDate"]).ToString(Common.DTP_DATE_FORMAT);
                            ds.Tables[0].Rows[i]["FromDateText"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["FromDate"]).ToString(Common.DTP_DATE_FORMAT);
                            ds.Tables[0].Rows[i]["ToDateText"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["ToDate"]).ToString(Common.DTP_DATE_FORMAT);
                            ds.Tables[0].Rows[i]["InvoiceAmount"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["InvoiceAmount"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["HeaderAddress"] = headerText;
                            ds.Tables[0].Rows[i]["AddressText"] = addressText;
                        }
                        lrds.Add(new ReportDataSource("PurchaseAgtFormC_PurchaseAgtFormCDataTable", ds.Tables[0]));
                    }
                }
            }
            catch (Exception ex)
            {
                Vinculum.Framework.Logging.LogManager.WriteExceptionLog(ex);
            }
            return lrds;

        }

        public static List<ReportDataSource> STNFinance(ref string errorMessage, string dtFrom, string dtTo, int from, int to)
        {
            List<ReportDataSource> lrds = new List<ReportDataSource>();
            try
            {
                DBParameterList dbParam;

                Vinculum.Framework.Data.DataTaskManager dtManager = new DataTaskManager();

                // initialize the parameter list object
                dbParam = new DBParameterList();

                // add the relevant parameters
                
                dbParam.Add(new DBParameter("@dtFrom", dtFrom, DbType.String));
                dbParam.Add(new DBParameter("@dtTo", dtTo, DbType.String));
                dbParam.Add(new DBParameter("@from", from, DbType.Int32));
                dbParam.Add(new DBParameter("@to", to, DbType.Int32));              
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                // executing procedure to save the record 
                DataSet ds = dtManager.ExecuteDataSet(SP_RPT_STNFINANCE, dbParam);

                // update database message
                errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();

                // if an error returned from the database
                if (errorMessage != string.Empty)
                    return null;
                else
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        string address = Common.ReportHeaderAddress();
                        string headerText = address.Substring(0, address.IndexOf("*|$|*"));
                        string addressText = address.Substring(address.IndexOf("*|$|*") + 5, address.Length - address.IndexOf("*|$|*") - 5);
                        addressText = addressText.Replace("*|$|*", Environment.NewLine);
                        ds.Tables[0].Columns.Add(new DataColumn("ExpectedDeliveryDateText", Type.GetType("System.String")));
                        ds.Tables[0].Columns.Add(new DataColumn("FromDateText", Type.GetType("System.String")));
                        ds.Tables[0].Columns.Add(new DataColumn("ToDateText", Type.GetType("System.String")));
                        ds.Tables[0].Columns.Add(new DataColumn("HeaderAddress", Type.GetType("System.String")));
                        ds.Tables[0].Columns.Add(new DataColumn("AddressText", Type.GetType("System.String")));
                        ds.Tables[0].Rows[0]["HeaderAddress"] = headerText;
                        ds.Tables[0].Rows[0]["AddressText"] = addressText;
                        ds.Tables[0].Columns.Add(new DataColumn("CreationDateText", Type.GetType("System.String")));
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            ds.Tables[0].Rows[i]["CreationDateText"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["CreationDate"]).ToString(Common.DTP_DATE_FORMAT);
                            ds.Tables[0].Rows[i]["TotalTOQuantity"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["TotalToQuantity"]), Common.DisplayQtyRounding,MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["TotalTOAmount"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["TotalToAmount"]), Common.DisplayAmountRounding,MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["ExpectedDeliveryDateText"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["ExpectedDeliveryDate"]).ToString(Common.DTP_DATE_FORMAT);
                            ds.Tables[0].Rows[i]["FromDateText"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["FromDate"]).ToString(Common.DTP_DATE_FORMAT);
                            ds.Tables[0].Rows[i]["ToDateText"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["ToDate"]).ToString(Common.DTP_DATE_FORMAT);
                        }
                        lrds.Add(new ReportDataSource("STNFinance_STNFinanceDataTable", ds.Tables[0]));
                    }
                }
            }
            catch (Exception ex)
            {
                Vinculum.Framework.Logging.LogManager.WriteExceptionLog(ex);
            }
            return lrds;
        }

        public static List<ReportDataSource> SalesTax(ref string errorMessage, int BOlocationId, int month, int year)
        {
            List<ReportDataSource> lrds = new List<ReportDataSource>();
            try
            {
                DBParameterList dbParam;

                Vinculum.Framework.Data.DataTaskManager dtManager = new DataTaskManager();

                // initialize the parameter list object
                dbParam = new DBParameterList();

                // add the relevant 2 parameters
                dbParam.Add(new DBParameter("BOLocationId", BOlocationId, DbType.Int32));
                dbParam.Add(new DBParameter("month", month, DbType.Int32));
                dbParam.Add(new DBParameter("year",year, DbType.Int32));
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                // executing procedure to save the record 
                DataSet ds = dtManager.ExecuteDataSet(SP_RPT_SALESTAX, dbParam);

                // update database message
                errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();

                // if an error returned from the database
                if (errorMessage != string.Empty)
                    return null;
                else
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        
                        string address = Common.ReportHeaderAddress();
                        string headerText = address.Substring(0, address.IndexOf("*|$|*"));
                        string addressText = address.Substring(address.IndexOf("*|$|*") + 5, address.Length - address.IndexOf("*|$|*") - 5);
                        addressText = addressText.Replace("*|$|*", Environment.NewLine);                        
                        ds.Tables[0].Columns.Add(new DataColumn("FromDateText", Type.GetType("System.String")));
                        ds.Tables[0].Columns.Add(new DataColumn("ToDateText", Type.GetType("System.String")));
                        ds.Tables[0].Columns.Add(new DataColumn("HeaderAddress", Type.GetType("System.String")));
                        ds.Tables[0].Columns.Add(new DataColumn("AddressText", Type.GetType("System.String")));

                        // Code for Tax Calculation
                        //ds.Tables[0].Columns.Add(new DataColumn("TaxAmount", System.Type.GetType("System.Decimal")));
                                               
                        //decimal previousLevelAmount = 0, currentLevelAmount = 0;
                        //int previousLevel = 1, currentLevel = 1;
                        //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        //{
                        //    if ((i > 0) && ((Convert.ToInt32(ds.Tables[0].Rows[i]["ItemId"]) != Convert.ToInt32(ds.Tables[0].Rows[i - 1]["ItemId"])) || ((ds.Tables[0].Rows[i]["TaxGroupCode"].ToString()) != (ds.Tables[0].Rows[i - 1]["TaxGroupCode"].ToString()))))
                        //    {
                        //        previousLevelAmount = 0;
                        //        currentLevelAmount = 0;
                        //        previousLevel = 1;
                        //        currentLevel = 1;
                        //    }

                        //   if (Convert.ToInt32(ds.Tables[0].Rows[i]["GroupOrder"]) == previousLevel)
                        //    {
                        //        if ((Convert.ToInt32(ds.Tables[0].Rows[i]["ThisMonthQty"]) > 0) || (Convert.ToInt32(ds.Tables[0].Rows[i]["TillMonthQty"]) > 0) || (Convert.ToInt32(ds.Tables[0].Rows[i]["ThisMonthFreeQty"]) > 0) || (Convert.ToInt32(ds.Tables[0].Rows[i]["TillMonthFreeQty"]) > 0))
                        //        {
                        //            currentLevel = previousLevel;
                        //            ds.Tables[0].Rows[i]["TaxAmount"] = ((previousLevelAmount == 0) ? Convert.ToDecimal(ds.Tables[0].Rows[i]["UnitPrice"]) : previousLevelAmount) * (Convert.ToDecimal(ds.Tables[0].Rows[i]["TaxPercent"])) / 100;
                        //            currentLevelAmount += Convert.ToDecimal(ds.Tables[0].Rows[i]["TaxAmount"]);
                        //        }
                        //        else
                        //        {
                        //            ds.Tables[0].Rows[i]["UnitPrice"] = ds.Tables[0].Rows[i]["DistributorPrice"];
                        //            ds.Tables[0].Rows[i]["TaxAmount"] = 0;
                        //            ds.Tables[0].Rows[i]["TaxPercent"] = 0;
                        //        }
                        //    }
                        //    else
                        //    {
                        //       if ((Convert.ToInt32(ds.Tables[0].Rows[i]["ThisMonthQty"]) > 0) || (Convert.ToInt32(ds.Tables[0].Rows[i]["TillMonthQty"]) > 0) || (Convert.ToInt32(ds.Tables[0].Rows[i]["ThisMonthFreeQty"]) > 0) || (Convert.ToInt32(ds.Tables[0].Rows[i]["TillMonthFreeQty"]) > 0))
                        //        {
                        //            currentLevel = ++previousLevel;
                        //            previousLevelAmount = currentLevelAmount;
                        //            ds.Tables[0].Rows[i]["TaxAmount"] = ((previousLevelAmount == 0) ? Convert.ToDecimal(ds.Tables[0].Rows[i]["UnitPrice"]) : previousLevelAmount) * (Convert.ToDecimal(ds.Tables[0].Rows[i]["TaxPercent"])) / 100;
                        //            currentLevelAmount = Convert.ToDecimal(ds.Tables[0].Rows[i]["TaxAmount"]);
                        //        }
                        //        else
                        //        {
                        //           ds.Tables[0].Rows[i]["UnitPrice"] = ds.Tables[0].Rows[i]["DistributorPrice"];
                        //           ds.Tables[0].Rows[i]["TaxAmount"] = 0;
                        //           ds.Tables[0].Rows[i]["TaxPercent"] = 0;
                        //        }
                        //    }
                        // }
                      

                        // Tax calculation finished

                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            if ((Convert.ToInt32(ds.Tables[0].Rows[i]["ThisMonthQty"]) == 0) && (Convert.ToInt32(ds.Tables[0].Rows[i]["TillMonthQty"]) == 0) && (Convert.ToInt32(ds.Tables[0].Rows[i]["ThisMonthFreeQty"]) == 0) && (Convert.ToInt32(ds.Tables[0].Rows[i]["TillMonthFreeQty"]) == 0))
                            {
                                ds.Tables[0].Rows[i]["UnitPrice"] = ds.Tables[0].Rows[i]["DistributorPrice"];
                                ds.Tables[0].Rows[i]["UnitTax"] = 0.00;
                                ds.Tables[0].Rows[i]["TaxPercent"] = 0.00;
                            }
                        }

                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            //ds.Tables[0].Rows[i]["FromDateText"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["FromDate"]).ToString(Common.DTP_DATE_FORMAT);
                            //ds.Tables[0].Rows[i]["ToDateText"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["ToDate"]).ToString(Common.DTP_DATE_FORMAT);
                            ds.Tables[0].Rows[i]["DistributorPrice"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["DistributorPrice"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["UnitPrice"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["UnitPrice"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["Qty"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["Qty"]), Common.DisplayQtyRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["TillMonthQty"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["TillMonthQty"]), Common.DisplayQtyRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["ThisMonthQty"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["ThisMonthQty"]), Common.DisplayQtyRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["TillMonthFreeQty"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["TillMonthFreeQty"]), Common.DisplayQtyRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["ThisMonthFreeQty"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["ThisMonthFreeQty"]), Common.DisplayQtyRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["UnitTax"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["UnitTax"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["PrimaryCost"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["PrimaryCost"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["TaxPercent"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["TaxPercent"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["HeaderAddress"] = headerText;
                            ds.Tables[0].Rows[i]["AddressText"] = addressText;
                            
                        }
                        lrds.Add(new ReportDataSource("SalesTax_SalesTaxDataTable", ds.Tables[0]));
                    }
                }
            }
            catch (Exception ex)
            {
                Vinculum.Framework.Logging.LogManager.WriteExceptionLog(ex);
            }
            return lrds;

        }

        public static List<ReportDataSource> VATCST(ref string errorMessage, string dtFrom, string dtTo, int from)
        {
            List<ReportDataSource> lrds = new List<ReportDataSource>();
            try
            {
                DBParameterList dbParam;

                Vinculum.Framework.Data.DataTaskManager dtManager = new DataTaskManager();

                // initialize the parameter list object
                dbParam = new DBParameterList();

                // add the relevant parameters
                dbParam.Add(new DBParameter("@dtFrom", dtFrom, DbType.String));
                dbParam.Add(new DBParameter("@dtTo", dtTo, DbType.String));
                dbParam.Add(new DBParameter("@locationId", from, DbType.Int32));
                               
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                // executing procedure to save the record 
                DataSet ds = dtManager.ExecuteDataSet(SP_RPT_VATCST, dbParam);

                // update database message
                errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();

                // if an error returned from the database
                if (errorMessage != string.Empty)
                    return null;
                else
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        string address = Common.ReportHeaderAddress();
                        string headerText = address.Substring(0, address.IndexOf("*|$|*"));
                        string addressText = address.Substring(address.IndexOf("*|$|*") + 5, address.Length - address.IndexOf("*|$|*") - 5);
                        addressText = addressText.Replace("*|$|*", Environment.NewLine);
                        ds.Tables[0].Columns.Add(new DataColumn("ExpectedDeliveryDateText", Type.GetType("System.String")));
                        ds.Tables[0].Columns.Add(new DataColumn("FromDateText", Type.GetType("System.String")));
                        ds.Tables[0].Columns.Add(new DataColumn("ToDateText", Type.GetType("System.String")));
                        ds.Tables[0].Columns.Add(new DataColumn("HeaderAddress", Type.GetType("System.String")));
                        ds.Tables[0].Columns.Add(new DataColumn("AddressText", Type.GetType("System.String")));
                        ds.Tables[0].Rows[0]["HeaderAddress"] = headerText;
                        ds.Tables[0].Rows[0]["AddressText"] = addressText;
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            ds.Tables[0].Rows[i]["DistributorPrice"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["DistributorPrice"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["BillingPrice"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["BillingPrice"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["VATQty"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["VATQty"]), Common.DisplayQtyRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["VATAmount"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["VATAmount"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["CSTQty"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["CSTQty"]), Common.DisplayQtyRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["CSTAmount"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["CStAmount"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["TotalQty"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["TotalQty"]), Common.DisplayQtyRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["TotalAmount"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["TotalAmount"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["FromDateText"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["FromDate"]).ToString(Common.DTP_DATE_FORMAT);
                            ds.Tables[0].Rows[i]["ToDateText"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["ToDate"]).ToString(Common.DTP_DATE_FORMAT);
                        }
                        lrds.Add(new ReportDataSource("VATCST_VATCSTDataTable", ds.Tables[0]));
                    }
                }
            }
            catch (Exception ex)
            {
                Vinculum.Framework.Logging.LogManager.WriteExceptionLog(ex);
            }
            return lrds;
        }


        public static List<ReportDataSource> DccBoRegister(ref string errorMessage, int month, int year, int BOlocationID)
        {
            List<ReportDataSource> lrds = new List<ReportDataSource>();


            try
            {
                DBParameterList dbParam;

                Vinculum.Framework.Data.DataTaskManager dtManager = new DataTaskManager();

                // initialize the parameter list object
                dbParam = new DBParameterList();
                dbParam.Add(new DBParameter("@BusinessMonth", month, DbType.String));
                dbParam.Add(new DBParameter("@year", year, DbType.Int32));
                dbParam.Add(new DBParameter("@LocationId", BOlocationID, DbType.Int32));
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                DataSet ds = dtManager.ExecuteDataSet(SP_RPT_Dcc_BO_Sales, dbParam);

                // update database message
                errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();

                // if an error returned from the database
                if (errorMessage != string.Empty)
                    return null;
                else
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        string address = Common.ReportHeaderAddress();
                        string headerText = address.Substring(0, address.IndexOf("*|$|*"));
                        string addressText = address.Substring(address.IndexOf("*|$|*") + 5, address.Length - address.IndexOf("*|$|*") - 5);
                        addressText = addressText.Replace("*|$|*", Environment.NewLine);
                        ds.Tables[0].Columns.Add(new DataColumn("FromDateText", Type.GetType("System.String")));
                        ds.Tables[0].Columns.Add(new DataColumn("ToDateText", Type.GetType("System.String")));
                        ds.Tables[0].Columns.Add(new DataColumn("HeaderAddress", Type.GetType("System.String")));
                        ds.Tables[0].Columns.Add(new DataColumn("AddressText", Type.GetType("System.String")));
                        ds.Tables[0].Columns.Add(new DataColumn("TotalinWords", Type.GetType("System.String")));
                        //ds.Tables[0].Columns.Add(new DataColumn("Dated", Type.GetType("System.String")));
                        ds.Tables[0].Rows[0]["HeaderAddress"] = headerText;
                        ds.Tables[0].Rows[0]["AddressText"] = addressText;
                        //ds.Tables[0].Rows[0]["Date"] = Dated;
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            //ds.Tables[0].Rows[i]["TaxGroupCode"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["TaxGroupCode"]), Common.DisplayAmountRounding);
                            //ds.Tables[0].Rows[i]["TaxAmount"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["TaxAmount"]), Common.DisplayAmountRounding);
                            //ds.Tables[0].Rows[i]["Sale"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["Sale"]), Common.DisplayAmountRounding);
                            //ds.Tables[0].Rows[i]["NewSale"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["NewSale"]), Common.DisplayAmountRounding);
                            //ds.Tables[0].Rows[i]["NewTax"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["NewTax"]), Common.DisplayAmountRounding);
                            //ds.Tables[0].Rows[i]["BonusPercent"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["BonusPercent"]), Common.DisplayQtyRounding);


                            //ds.Tables[0].Rows[i]["InvoiceDate"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["InvoiceDate"]).ToString(Common.DTP_DATE_FORMAT);

                            ds.Tables[0].Rows[i]["FromDateText"] = "ABC";//Convert.ToDateTime(ds.Tables[0].Rows[i]["dtForm"]).ToString(Common.DTP_DATE_FORMAT);
                            ds.Tables[0].Rows[i]["ToDateText"] = "ABC";// Convert.ToDateTime(ds.Tables[0].Rows[i]["dtTo"]).ToString(Common.DTP_DATE_FORMAT);
                            ds.Tables[0].Rows[i]["TotalinWords"] = Common.AmountToWords.AmtInWord(Convert.ToDecimal(ds.Tables[0].Rows[i]["TotalPayableAmt"]));
                            ds.Tables[0].Rows[i]["Payable"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["Payable"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["NetSale"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["NetSale"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["TotalPayableAmt"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["TotalPayableAmt"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                            // ds.Tables[0].Rows[i]["Dated"] = Convert.ToDateTime(Dated).ToString(Common.DATE_TIME_FORMAT);



                        }
                        lrds.Add(new ReportDataSource("DccBo_DataTable1", ds.Tables[0]));

                    }

                }
                return lrds;
            }
            catch (Exception ex)
            {

                Vinculum.Framework.Logging.LogManager.WriteExceptionLog(ex);
                return null;
            }

        }


        public static List<ReportDataSource> SalesReturnRegister(ref string errorMessage, string dtFrom, string dtTo, int location)
        {
            List<ReportDataSource> lrds = new List<ReportDataSource>();
            try
            {
                DBParameterList dbParam;

                Vinculum.Framework.Data.DataTaskManager dtManager = new DataTaskManager();

                // initialize the parameter list object
                dbParam = new DBParameterList();

                // add the relevant 2 parameters
                //dbParam.Add(new DBParameter("@InvoiceType", invoiceType, DbType.String));

                dbParam.Add(new DBParameter("@dtFrom", dtFrom, DbType.String));
                dbParam.Add(new DBParameter("@dtTo", dtTo, DbType.String));
                dbParam.Add(new DBParameter("@location", location, DbType.Int32));
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                // executing procedure to save the record 
                DataSet ds = dtManager.ExecuteDataSet(SP_RPT_SALESRETURNREGISTER, dbParam);

                // update database message
                errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();

                // if an error returned from the database
                if (errorMessage != string.Empty)
                    return null;
                else
                {
                    if (ds.Tables[0].Rows.Count > 0)
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
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            //ds.Tables[0].Rows[i]["TaxGroupCode"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["TaxGroupCode"]), Common.DisplayAmountRounding);
                            ds.Tables[0].Rows[i]["TaxAmount"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["TaxAmount"]), Common.DisplayAmountRounding);
                            ds.Tables[0].Rows[i]["Sale"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["Sale"]), Common.DisplayAmountRounding);

                            ds.Tables[0].Rows[i]["InvoiceDate"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["InvoiceDate"]).ToString(Common.DTP_DATE_FORMAT);

                            ds.Tables[0].Rows[i]["FromDateText"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["dtFrom"]).ToString(Common.DTP_DATE_FORMAT);
                            ds.Tables[0].Rows[i]["ToDateText"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["dtTo"]).ToString(Common.DTP_DATE_FORMAT);
                        }
                        lrds.Add(new ReportDataSource("SalesReturnRegister_SalesReturnRegister", ds.Tables[0]));
                    }
                }
            }
            catch (Exception ex)
            {
                Vinculum.Framework.Logging.LogManager.WriteExceptionLog(ex);
            }
            return lrds;
        }



   



        public static List<ReportDataSource> PaymentStatusSummary(ref string errorMessage, int Month, int Year, string DistributorID, int chequenumber)
        {
            List<ReportDataSource> lrds = new List<ReportDataSource>();
            try
            {
                DBParameterList dbParam;

                Vinculum.Framework.Data.DataTaskManager dtManager = new DataTaskManager();

                // initialize the parameter list object
                dbParam = new DBParameterList();

                // add the relevant 2 parameters
                //dbParam.Add(new DBParameter("@InvoiceType", invoiceType, DbType.String));

                dbParam.Add(new DBParameter("@Month", Month, DbType.Int32));
                dbParam.Add(new DBParameter("@Year", Year, DbType.Int32));
                dbParam.Add(new DBParameter("@DistributorID", DistributorID, DbType.String));
                dbParam.Add(new DBParameter("@chequenumber", chequenumber, DbType.Int32));
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                // executing procedure to save the record 
                DataSet ds = dtManager.ExecuteDataSet(SP_RPT_PaymentStatusSummary, dbParam);

                // update database message
                errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();

                // if an error returned from the database
                if (errorMessage != string.Empty)
                    return null;
                else
                {
                    if (ds.Tables[0].Rows.Count > 0)
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
                        ds.Tables[0].Columns.Add(new DataColumn("TransDateText", Type.GetType("System.String")));
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                                                     
                            //ds.Tables[0].Rows[i]["TransDateText"] = "2011-08-30"; //Convert.ToDateTime(ds.Tables[0].Rows[i]["TransDate"]).ToString(Common.DTP_DATE_FORMAT);
                            //ds.Tables[0].Rows[i]["TransQty"]  ="20"; // Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["TransQty"]), Common.DisplayQtyRounding);
                            //ds.Tables[0].Rows[i]["FromDateText"] = "2011-08-30"; //Convert.ToDateTime(ds.Tables[0].Rows[i]["dtFrom"]).ToString(Common.DTP_DATE_FORMAT);
                            //ds.Tables[0].Rows[i]["ToDateText"] = "2011-08-30"; //Convert.ToDateTime(ds.Tables[0].Rows[i]["dtTo"]).ToString(Common.DTP_DATE_FORMAT);
                        }
                        lrds.Add(new ReportDataSource("PaymentStatusSummary_PaymentStatusSummary", ds.Tables[0]));
                    }
                }
            }
            catch (Exception ex)
            {
                Vinculum.Framework.Logging.LogManager.WriteExceptionLog(ex);
            }
            return lrds;
        }

        
      
            #endregion
        }
}
