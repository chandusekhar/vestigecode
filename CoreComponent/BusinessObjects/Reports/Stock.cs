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
    public class Stock
    {
        #region SP Declaration

        private const string SP_RPT_STOCK_INVOICE = "usp_rptStockSales";
        private const string SP_RPT_STOCKIST_LOG = "usp_RptStockistLog";
        private const string SP_RPT_SALES_COLLECTION = "usp_rptSalesCollection";
        private const string SP_RPT_PUC_ACCOUNTS = "usp_rptPUCAccountsSummary";
        private const string SP_RPT_PUC_DEPOSIT = "usp_rptPUCAccountDeposit";
        private const string SP_RPT_RegionwiseSales = "usp_rptRegionwiseSales";
        private const string SP_RPT_RegionwiseSalesMiniBranch = "usp_rptRegionwiseSalesMiniBranch";
        private const string SP_RPT_MonthItemwiseSales = "usp_rptMonthItemwiseSales";
        private const string SP_RPT_ItemBatchAdjustment = "usp_Item_Batch_Adjustment";
        private const string SP_RPT_ORDER_COLLECTION = "usp_rptOrderCollection";
        private const string SP_RPT_BonusStatementDirectors = "usp_BonusStatementDirectors";
        private const string SP_RPT_StatewiseSales = "usp_rptStatewiseSales";
        private const string SP_RPT_Consolidated_SALES_COLLECTION = "usp_rptConsolidatedSalesCollection";

        #endregion

        #region Validations

        public static bool ValidateStockSalesReport(ref string dbMessage, int BOlocationId, int PClocationId, string logNo, string dtFrom, string dtTo, int ReportType)
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
            else if (ReportType == -1)
            {
                sb.Append(Common.GetMessage("VAL0002", "Report Type"));
                sb.AppendLine();
                ret = false;
            }
            else
                ret = true;
            if (ret == false)
                MessageBox.Show(Common.ReturnErrorMessage(sb).ToString(), Common.VALIDATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return ret;
        }

        public static bool ValidateStockistLog(ref string dbMessage, int BOlocationId, int PClocationId, string logNo, string dtFrom, string dtTo)
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

        public static bool ValidateSalesCollectionReport(ref string dbMessage, int StateId, int DLCP, int BOlocationId, int PClocationId, int UserId, string dtFrom, string dtTo)
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

        public static bool ValidatePUCAccountsSummary(ref string dbMessage, int BOlocationId, int PClocationId, string dtFrom, string dtTo)
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

        public static bool ValidatePOSStockSalesReport(ref string dbMessage, int PClocationId, string dtFrom, string dtTo, string logNo, int ReportType)
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
            else if (ReportType == -1)
            {
                sb.Append(Common.GetMessage("VAL0002", "Report Type"));
                sb.AppendLine();
                ret = false;
            }
            else
                ret = true;
            if (ret == false)
                MessageBox.Show(Common.ReturnErrorMessage(sb).ToString(), Common.VALIDATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return ret;
        }

        public static bool ValidatePOSSalesCollectionReport(ref string dbMessage, int PClocationId, int UserId, string dtFrom, string dtTo)
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

        public static bool ValidatePOSStockistLog(ref string dbMessage, int PClocationId, string logNo, string dtFrom, string dtTo)
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

        public static bool ValidatePUCAccountDeposit(ref string dbMessage, int BOlocationId, int PClocationId, string dtFrom, string dtTo)
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

        public static bool ValidateMonthToDate(ref string dbMessage, string @Current, int LocationId, string itemcode)
        {
            bool ret = true;
            if (@Current == string.Empty)
                ret = false;

            return ret;
        }


        public static bool ValidateOrderCollectionReport(ref string dbMessage, int BOlocationId, int PClocationId, int UserId, string dtFrom, string dtTo)
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


        public static bool ValidateBonusStatementDirectors(ref string dbMessage, int month, int year, string DistributorID)
        {
            bool ret = true;
            StringBuilder sb = new StringBuilder();
            //if (month > DateTime.Today.Month)
            //{
            //    sb.Append(Common.GetMessage("VAL0060", "Month", "Current Month"));
            //    sb.AppendLine();
            //    ret = false;
            //}
            //else 
            if (year > DateTime.Today.Year)
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


        #endregion

        #region Methods

        public static List<ReportDataSource> StockInvoiceSearch(ref string errorMessage, int BOlocationId, int PClocationId, string logNo, string dtFrom, string dtTo, int ReportType)
        {
            List<ReportDataSource> lrds = new List<ReportDataSource>();
            try
            {
                DBParameterList dbParam;

                Vinculum.Framework.Data.DataTaskManager dtManager = new DataTaskManager();

                // initialize the parameter list object
                dbParam = new DBParameterList();

                // add the relevant 2 parameters
                dbParam.Add(new DBParameter("BOlocationId", BOlocationId, DbType.Int32));
                dbParam.Add(new DBParameter("PClocationId", PClocationId, DbType.Int32));
                dbParam.Add(new DBParameter("logNo", logNo, DbType.String));
                dbParam.Add(new DBParameter("FromDate", dtFrom, DbType.String));
                dbParam.Add(new DBParameter("ToDate", dtTo, DbType.String));
                dbParam.Add(new DBParameter("WithKit", ReportType, DbType.Int32));

                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                // executing procedure to save the record 
                DataSet ds = dtManager.ExecuteDataSet(SP_RPT_STOCK_INVOICE, dbParam);

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
                        ds.Tables[0].Columns.Add(new DataColumn("InvoiceDateText", Type.GetType("System.String")));
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            ds.Tables[0].Rows[i]["InvoiceDateText"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["InvoiceDate"]).ToString(Common.DTP_DATE_FORMAT);
                            ds.Tables[0].Rows[i]["InvoiceAmount"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["InvoiceAmount"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["TaxAmount"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["TaxAmount"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["NetAmount"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["NetAmount"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["FromDateText"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["FromDate"]).ToString(Common.DTP_DATE_FORMAT);
                            ds.Tables[0].Rows[i]["ToDateText"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["ToDate"]).ToString(Common.DTP_DATE_FORMAT);
                        }
                        lrds.Add(new ReportDataSource("StockSales_dtStockInvoice", ds.Tables[0]));
                    }
                }
            }
            catch (Exception ex)
            {
                Vinculum.Framework.Logging.LogManager.WriteExceptionLog(ex);
            }
            return lrds;
        }

        public static List<ReportDataSource> StockistLogSearch(ref string errorMessage, int BOlocationId, int PClocationId, string logNo, string dateFrom, string dateTo)
        {
            List<ReportDataSource> lrds = new List<ReportDataSource>();
            try
            {
                DBParameterList dbParam;

                Vinculum.Framework.Data.DataTaskManager dtManager = new DataTaskManager();

                // initialize the parameter list object
                dbParam = new DBParameterList();

                // add the relevant 2 parameters
                dbParam.Add(new DBParameter("dtFrom", dateFrom, DbType.String));
                dbParam.Add(new DBParameter("dtTo", dateTo, DbType.String));
                dbParam.Add(new DBParameter("BOlocationId", BOlocationId, DbType.Int32));
                dbParam.Add(new DBParameter("PClocationId", PClocationId, DbType.Int32));
                dbParam.Add(new DBParameter("logNo", logNo, DbType.String));

                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                // executing procedure to save the record 
                DataSet ds = dtManager.ExecuteDataSet(SP_RPT_STOCKIST_LOG, dbParam);

                // update database message
                errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();

                // if an error returned from the database
                if (errorMessage != string.Empty)
                    return null;
                else
                {
                    string address = Common.ReportHeaderAddress();
                    string headerText = address.Substring(0, address.IndexOf("*|$|*"));
                    string addressText = address.Substring(address.IndexOf("*|$|*") + 5, address.Length - address.IndexOf("*|$|*") - 5);
                    addressText = addressText.Replace("*|$|*", Environment.NewLine);
                    ds.Tables[0].Columns.Add(new DataColumn("HeaderAddress", Type.GetType("System.String")));
                    ds.Tables[0].Columns.Add(new DataColumn("AddressText", Type.GetType("System.String")));
                    ds.Tables[0].Rows[0]["HeaderAddress"] = headerText;
                    ds.Tables[0].Rows[0]["AddressText"] = addressText;
                    ds.Tables[0].Columns.Add(new DataColumn("InvoiceDateText", Type.GetType("System.String")));
                    ds.Tables[0].Columns.Add(new DataColumn("FromDateText", Type.GetType("System.String")));
                    ds.Tables[0].Columns.Add(new DataColumn("ToDateText", Type.GetType("System.String")));
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        ds.Tables[0].Rows[i]["Quantity"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["Quantity"]), Common.DisplayQtyRounding, MidpointRounding.AwayFromZero);
                        ds.Tables[0].Rows[i]["DistributorPrice"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["DistributorPrice"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                        ds.Tables[0].Rows[i]["Amount"] = Math.Round((Convert.ToDecimal(ds.Tables[0].Rows[i]["Quantity"]) * Convert.ToDecimal(ds.Tables[0].Rows[i]["DistributorPrice"])), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                        ds.Tables[0].Rows[i]["Weight"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["Weight"]), Common.DisplayQtyRounding, MidpointRounding.AwayFromZero);
                        ds.Tables[0].Rows[i]["FromDateText"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["FromDate"]).ToString(Common.DTP_DATE_FORMAT);
                        ds.Tables[0].Rows[i]["ToDateText"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["ToDate"]).ToString(Common.DTP_DATE_FORMAT);
                    }
                    lrds.Add(new ReportDataSource("StockistLogRptDataSet_StockistLogRptDataTable", ds.Tables[0]));
                }
            }
            catch (Exception ex)
            {
                Vinculum.Framework.Logging.LogManager.WriteExceptionLog(ex);
            }
            return lrds;

        }

        public static List<ReportDataSource> SalesCollectionSearch(ref string errorMessage, int StateId, int DLCP, int BOlocationId, int PClocationId, int UserId, string dtFrom, string dtTo)
        {
            List<ReportDataSource> lrds = new List<ReportDataSource>();
            int iCount = 0;
            try
            {
                DBParameterList dbParam;

                Vinculum.Framework.Data.DataTaskManager dtManager = new DataTaskManager();

                // initialize the parameter list object
                dbParam = new DBParameterList();

                // add the relevant parameters
                dbParam.Add(new DBParameter("StateId", StateId, DbType.Int32));
                dbParam.Add(new DBParameter("DLCP", DLCP, DbType.Int32));
                dbParam.Add(new DBParameter("BolocationId", BOlocationId, DbType.Int32));
                dbParam.Add(new DBParameter("PClocationId", PClocationId, DbType.Int32));
                dbParam.Add(new DBParameter("UserId", UserId, DbType.Int32));
                dbParam.Add(new DBParameter("FromDate", dtFrom, DbType.String));
                dbParam.Add(new DBParameter("ToDate", dtTo, DbType.String));

                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                // executing procedure to save the record 
                DataSet ds = dtManager.ExecuteDataSet(SP_RPT_SALES_COLLECTION, dbParam);

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
                        ds.Tables[0].Columns.Add(new DataColumn("visible", Type.GetType("System.String")));
                        ds.Tables[0].Rows[0]["HeaderAddress"] = headerText;
                        ds.Tables[0].Rows[0]["AddressText"] = addressText;
                        ds.Tables[0].Rows[0]["visible"] = ds.Tables[1].Rows[0]["visible"].ToString();
                        //iCount = Convert.ToInt32(ds.Tables[1].Rows[0]["visible"]);
                        ds.Tables[0].Columns.Add(new DataColumn("InvoiceDateText", Type.GetType("System.String")));
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            ds.Tables[0].Rows[i]["InvoiceDateText"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["InvoiceDate"]).ToString(Common.DTP_DATE_FORMAT);
                            ds.Tables[0].Rows[i]["FromDateText"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["FromDate"]).ToString(Common.DTP_DATE_FORMAT);
                            ds.Tables[0].Rows[i]["ToDateText"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["ToDate"]).ToString(Common.DTP_DATE_FORMAT);
                            ds.Tables[0].Rows[i]["Cash"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["Cash"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["CreditCard"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["CreditCard"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["Forex"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["Forex"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["Cheque"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["Cheque"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["BonusCheque"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["BonusCheque"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["Bank"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["Bank"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["COD"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["COD"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["AvailableBefore"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["AvailableBefore"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["DepositInBetween"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["DepositInBetween"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["ChangeAmt"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["ChangeAmt"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                        }
                        lrds.Add(new ReportDataSource("SalesCollection_SalesCollectionDataTable", ds.Tables[0]));
                    }
                }
            }
            catch (Exception ex)
            {
                Vinculum.Framework.Logging.LogManager.WriteExceptionLog(ex);
            }
            return lrds;
        }

        public static List<ReportDataSource> PUCAccountsSummary(ref string errorMessage, int BOlocationId, int PClocationId, string dtFrom, string dtTo)
        {
            List<ReportDataSource> lrds = new List<ReportDataSource>();
            try
            {
                DBParameterList dbParam;

                Vinculum.Framework.Data.DataTaskManager dtManager = new DataTaskManager();

                // initialize the parameter list object
                dbParam = new DBParameterList();

                // add the relevant parameters
                dbParam.Add(new DBParameter("BOlocation", BOlocationId, DbType.Int32));
                dbParam.Add(new DBParameter("PClocation", PClocationId, DbType.Int32));
                dbParam.Add(new DBParameter("dtFrom", dtFrom, DbType.String));
                dbParam.Add(new DBParameter("dtTo", dtTo, DbType.String));

                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                // executing procedure to save the record 
                DataSet ds = dtManager.ExecuteDataSet(SP_RPT_PUC_ACCOUNTS, dbParam);

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
                        ds.Tables[0].Columns.Add(new DataColumn("PayDateText", Type.GetType("System.String")));
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            ds.Tables[0].Rows[i]["PayDateText"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["PayDate"]).ToString(Common.DTP_DATE_FORMAT);
                            ds.Tables[0].Rows[i]["FromDateText"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["FromDate"]).ToString(Common.DTP_DATE_FORMAT);
                            ds.Tables[0].Rows[i]["ToDateText"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["ToDate"]).ToString(Common.DTP_DATE_FORMAT);
                            ds.Tables[0].Rows[i]["Cash"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["Cash"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["CreditCard"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["CreditCard"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["Bank"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["Bank"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["Cheque"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["Cheque"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["BonusCheque"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["BonusCheque"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["OnAccount"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["OnAccount"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["AvailableBefore"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["AvailableBefore"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["DepositInBetween"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["DepositInBetween"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["UsedAmount"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["UsedAmount"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                        }
                        lrds.Add(new ReportDataSource("PUCAccountsDetails_PUCAccountsDetails_DataTable", ds.Tables[0]));
                    }
                }
            }
            catch (Exception ex)
            {

                Vinculum.Framework.Logging.LogManager.WriteExceptionLog(ex);
            }
            return lrds;
        }

        public static List<ReportDataSource> POSStockInvoiceSearch(ref string errorMessage, int PClocationId, string dtFrom, string dtTo, string logNo, int ReportType)
        {
            List<ReportDataSource> lrds = new List<ReportDataSource>();
            try
            {
                DBParameterList dbParam;

                Vinculum.Framework.Data.DataTaskManager dtManager = new DataTaskManager();

                // initialize the parameter list object
                dbParam = new DBParameterList();

                // add the relevant 2 parameters
                dbParam.Add(new DBParameter("BOlocationId", Common.INT_DBNULL, DbType.Int32));
                dbParam.Add(new DBParameter("PClocationId", PClocationId, DbType.Int32));
                dbParam.Add(new DBParameter("logNo", logNo, DbType.String));
                dbParam.Add(new DBParameter("FromDate", dtFrom, DbType.String));
                dbParam.Add(new DBParameter("ToDate", dtTo, DbType.String));
                dbParam.Add(new DBParameter("WithKit", ReportType, DbType.Int32));
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                // executing procedure to save the record 
                DataSet ds = dtManager.ExecuteDataSet(SP_RPT_STOCK_INVOICE, dbParam);

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
                        ds.Tables[0].Columns.Add(new DataColumn("InvoiceDateText", Type.GetType("System.String")));
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            ds.Tables[0].Rows[i]["InvoiceDateText"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["InvoiceDate"]).ToString(Common.DTP_DATE_FORMAT);
                            ds.Tables[0].Rows[i]["InvoiceAmount"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["InvoiceAmount"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["TaxAmount"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["TaxAmount"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["NetAmount"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["NetAmount"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["FromDateText"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["FromDate"]).ToString(Common.DTP_DATE_FORMAT);
                            ds.Tables[0].Rows[i]["ToDateText"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["ToDate"]).ToString(Common.DTP_DATE_FORMAT);
                        }
                        lrds.Add(new ReportDataSource("StockSales_dtStockInvoice", ds.Tables[0]));
                    }
                }
            }
            catch (Exception ex)
            {
                Vinculum.Framework.Logging.LogManager.WriteExceptionLog(ex);
            }
            return lrds;
        }

        public static List<ReportDataSource> POSSalesCollectionSearch(ref string errorMessage, int PClocationId, int UserId, string dtFrom, string dtTo)
        {
            List<ReportDataSource> lrds = new List<ReportDataSource>();
            try
            {
                DBParameterList dbParam;

                Vinculum.Framework.Data.DataTaskManager dtManager = new DataTaskManager();

                // initialize the parameter list object
                dbParam = new DBParameterList();

                // add the relevant parameters
                dbParam.Add(new DBParameter("BolocationId", Common.INT_DBNULL, DbType.Int32));
                dbParam.Add(new DBParameter("PClocationId", PClocationId, DbType.Int32));
                dbParam.Add(new DBParameter("UserId", UserId, DbType.Int32));
                dbParam.Add(new DBParameter("FromDate", dtFrom, DbType.String));
                dbParam.Add(new DBParameter("ToDate", dtTo, DbType.String));

                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                // executing procedure to save the record 
                DataSet ds = dtManager.ExecuteDataSet(SP_RPT_SALES_COLLECTION, dbParam);

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
                        ds.Tables[0].Columns.Add(new DataColumn("visible", Type.GetType("System.String")));
                        ds.Tables[0].Rows[0]["HeaderAddress"] = headerText;
                        ds.Tables[0].Rows[0]["AddressText"] = addressText;
                        ds.Tables[0].Columns.Add(new DataColumn("InvoiceDateText", Type.GetType("System.String")));
                        ds.Tables[0].Rows[0]["visible"] = ds.Tables[1].Rows[0]["visible"].ToString();
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            ds.Tables[0].Rows[i]["InvoiceDateText"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["InvoiceDate"]).ToString(Common.DTP_DATE_FORMAT);
                            ds.Tables[0].Rows[i]["FromDateText"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["FromDate"]).ToString(Common.DTP_DATE_FORMAT);
                            ds.Tables[0].Rows[i]["ToDateText"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["ToDate"]).ToString(Common.DTP_DATE_FORMAT);
                            ds.Tables[0].Rows[i]["Cash"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["Cash"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["CreditCard"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["CreditCard"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["Forex"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["Forex"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["Cheque"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["Cheque"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["BonusCheque"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["BonusCheque"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["AvailableBefore"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["AvailableBefore"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["Bank"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["Bank"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);

                            ds.Tables[0].Rows[i]["DepositInBetween"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["DepositInBetween"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["ChangeAmt"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["ChangeAmt"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                        }
                        lrds.Add(new ReportDataSource("SalesCollection_SalesCollectionDataTable", ds.Tables[0]));
                    }
                }
            }
            catch (Exception ex)
            {
                Vinculum.Framework.Logging.LogManager.WriteExceptionLog(ex);
            }
            return lrds;
        }

        public static List<ReportDataSource> POSStockistLogSearch(ref string errorMessage, int PClocationId, string logNo, string dateFrom, string dateTo)
        {
            List<ReportDataSource> lrds = new List<ReportDataSource>();
            try
            {
                DBParameterList dbParam;

                Vinculum.Framework.Data.DataTaskManager dtManager = new DataTaskManager();

                // initialize the parameter list object
                dbParam = new DBParameterList();

                // add the relevant 2 parameters
                dbParam.Add(new DBParameter("dtFrom", dateFrom, DbType.String));
                dbParam.Add(new DBParameter("dtTo", dateTo, DbType.String));
                dbParam.Add(new DBParameter("BOlocationId", Common.INT_DBNULL, DbType.Int32));
                dbParam.Add(new DBParameter("PClocationId", PClocationId, DbType.Int32));
                dbParam.Add(new DBParameter("logNo", logNo, DbType.String));

                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                // executing procedure to save the record 
                DataSet ds = dtManager.ExecuteDataSet(SP_RPT_STOCKIST_LOG, dbParam);

                // update database message
                errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();

                // if an error returned from the database
                if (errorMessage != string.Empty)
                    return null;
                else
                {
                    string address = Common.ReportHeaderAddress();
                    string headerText = address.Substring(0, address.IndexOf("*|$|*"));
                    string addressText = address.Substring(address.IndexOf("*|$|*") + 5, address.Length - address.IndexOf("*|$|*") - 5);
                    addressText = addressText.Replace("*|$|*", Environment.NewLine);
                    ds.Tables[0].Columns.Add(new DataColumn("HeaderAddress", Type.GetType("System.String")));
                    ds.Tables[0].Columns.Add(new DataColumn("AddressText", Type.GetType("System.String")));
                    ds.Tables[0].Rows[0]["HeaderAddress"] = headerText;
                    ds.Tables[0].Rows[0]["AddressText"] = addressText;
                    ds.Tables[0].Columns.Add(new DataColumn("InvoiceDateText", Type.GetType("System.String")));
                    ds.Tables[0].Columns.Add(new DataColumn("FromDateText", Type.GetType("System.String")));
                    ds.Tables[0].Columns.Add(new DataColumn("ToDateText", Type.GetType("System.String")));
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        ds.Tables[0].Rows[i]["Quantity"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["Quantity"]), Common.DisplayQtyRounding, MidpointRounding.AwayFromZero);
                        ds.Tables[0].Rows[i]["DistributorPrice"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["DistributorPrice"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                        ds.Tables[0].Rows[i]["Amount"] = Math.Round((Convert.ToDecimal(ds.Tables[0].Rows[i]["Quantity"]) * Convert.ToDecimal(ds.Tables[0].Rows[i]["DistributorPrice"])), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                        ds.Tables[0].Rows[i]["FromDateText"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["FromDate"]).ToString(Common.DTP_DATE_FORMAT);
                        ds.Tables[0].Rows[i]["ToDateText"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["ToDate"]).ToString(Common.DTP_DATE_FORMAT);
                    }
                    lrds.Add(new ReportDataSource("StockistLogRptDataSet_StockistLogRptDataTable", ds.Tables[0]));
                }
            }
            catch (Exception ex)
            {
                Vinculum.Framework.Logging.LogManager.WriteExceptionLog(ex);
            }
            return lrds;

        }

        public static List<ReportDataSource> PUCAccountDeposit(ref string errorMessage, int BOlocationId, int PClocationId, string dtFrom, string dtTo)
        {
            List<ReportDataSource> lrds = new List<ReportDataSource>();
            try
            {
                DBParameterList dbParam;

                Vinculum.Framework.Data.DataTaskManager dtManager = new DataTaskManager();

                // initialize the parameter list object
                dbParam = new DBParameterList();

                // add the relevant parameters
                dbParam.Add(new DBParameter("BOlocation", BOlocationId, DbType.Int32));
                dbParam.Add(new DBParameter("PClocation", PClocationId, DbType.Int32));
                dbParam.Add(new DBParameter("dtFrom", dtFrom, DbType.String));
                dbParam.Add(new DBParameter("dtTo", dtTo, DbType.String));

                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                // executing procedure to save the record 
                DataSet ds = dtManager.ExecuteDataSet(SP_RPT_PUC_DEPOSIT, dbParam);

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
                        ds.Tables[0].Columns.Add(new DataColumn("PayDateText", Type.GetType("System.String")));
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            ds.Tables[0].Rows[i]["PayDateText"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["PayDate"]).ToString(Common.DTP_DATE_FORMAT);
                            ds.Tables[0].Rows[i]["FromDateText"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["FromDate"]).ToString(Common.DTP_DATE_FORMAT);
                            ds.Tables[0].Rows[i]["ToDateText"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["ToDate"]).ToString(Common.DTP_DATE_FORMAT);
                            ds.Tables[0].Rows[i]["Amount"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["Amount"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                        }
                        lrds.Add(new ReportDataSource("PUCAccountDeposit_PUCAccountDeposit_DataTable", ds.Tables[0]));
                    }
                }
            }
            catch (Exception ex)
            {

                Vinculum.Framework.Logging.LogManager.WriteExceptionLog(ex);
            }
            return lrds;
        }

        public static bool ValidateToDate(ref string dbMessage, string dtTo)
        {
            bool ret = true;
            if (dtTo == string.Empty)
                ret = false;
            //StringBuilder sb = new StringBuilder();
            //if ((dtFrom != string.Empty && Convert.ToDateTime(dtFrom) > DateTime.Now))
            //{
            //    sb.Append(Common.GetMessage("VAL0060", "Date From", "Current Date"));
            //    sb.AppendLine();
            //    ret = false;
            //}
            //else if ((dtTo != string.Empty && Convert.ToDateTime(dtTo) > DateTime.Now))
            //{
            //    sb.Append(Common.GetMessage("VAL0060", "Date To", "Current Date"));
            //    sb.AppendLine();
            //    ret = false;
            //}
            //else if (dtFrom != string.Empty && dtTo != string.Empty && Convert.ToDateTime(dtFrom) > Convert.ToDateTime(dtTo))
            //{
            //    sb.Append(Common.GetMessage("VAL0060", "Date From", "Date To"));
            //    sb.AppendLine();
            //    ret = false;
            //}
            //else
            //    ret = true;
            //if (ret == false)
            //    MessageBox.Show(Common.ReturnErrorMessage(sb).ToString(), Common.VALIDATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return ret;
        }



        public static List<ReportDataSource> GetStateWiseSalesData(ref string errorMessage, string dtTo)
        {
            List<ReportDataSource> lrds = new List<ReportDataSource>();
            try
            {
                DBParameterList dbParam;

                Vinculum.Framework.Data.DataTaskManager dtManager = new DataTaskManager();

                // initialize the parameter list object
                dbParam = new DBParameterList();

                // add the relevant parameters
                dbParam.Add(new DBParameter("Current", "Y", DbType.String));
                dbParam.Add(new DBParameter("Date", dtTo, DbType.String));

                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                // executing procedure to save the record 
                DataSet ds = dtManager.ExecuteDataSet(SP_RPT_StatewiseSales, dbParam);

                // update database message
                errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();

                // if an error returned from the database
                if (errorMessage != string.Empty)
                    return null;
                else
                {
                    if (ds.Tables.Count > 0)
                    {
                        
                        int iTemp;
                        DataRow dtRow;
                        foreach (DataTable dttable in ds.Tables)
                        {
                            if (dttable.Rows.Count != 7)
                            {
                                iTemp = 7 - dttable.Rows.Count;
                                for (int i = 0; i < iTemp; i++)
                                {
                                    dtRow = dttable.NewRow();
                                    dttable.Rows.Add(dtRow);
                                }
                            }
                            foreach (DataRow drRow in dttable.Rows)
                            {
                                if (!string.IsNullOrEmpty(drRow["Sales"].ToString()))
                                    drRow["Sales"] = Math.Round(Convert.ToDecimal(drRow["Sales"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                                
                            }
                        }
                        foreach (DataRow drow in ds.Tables[4].Rows)
                        {
                            if (!string.IsNullOrEmpty(drow["TotalSales"].ToString()))
                                drow["TotalSales"] = Math.Round(Convert.ToDecimal(drow["TotalSales"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                        }
                        lrds.Add(new ReportDataSource("StateZoneWiseSales_NorthSales_State", ds.Tables[0]));
                       // lrds.Add(new ReportDataSource("StateZoneWiseSales_UPSales_State", ds.Tables[1]));
                        lrds.Add(new ReportDataSource("StateZoneWiseSales_SouthSales_State", ds.Tables[1]));
                        lrds.Add(new ReportDataSource("StateZoneWiseSales_EastSales_State", ds.Tables[2]));
                        //lrds.Add(new ReportDataSource("StateZoneWiseSales_NorthEastSales_State", ds.Tables[4]));
                        lrds.Add(new ReportDataSource("StateZoneWiseSales_WestSales_State", ds.Tables[3]));
                        lrds.Add(new ReportDataSource("StateZoneWiseSales_NepalSales_State", ds.Tables[4]));
                        //lrds.Add(new ReportDataSource("StateZoneWiseSales_CenteralSales_State", ds.Tables[7]));

                    }
                    GetLastMonthDataForState(lrds, dtTo);
                }
            }
            catch (Exception ex)
            {

                Vinculum.Framework.Logging.LogManager.WriteExceptionLog(ex);
            }
            return lrds;
        }


        public static List<ReportDataSource> GetRegionSalesData(ref string errorMessage, string dtTo)
        {
            List<ReportDataSource> lrds = new List<ReportDataSource>();
            try
            {
                DBParameterList dbParam;

                Vinculum.Framework.Data.DataTaskManager dtManager = new DataTaskManager();

                // initialize the parameter list object
                dbParam = new DBParameterList();

                // add the relevant parameters
                dbParam.Add(new DBParameter("Current", "Y", DbType.String));
                dbParam.Add(new DBParameter("Date", dtTo, DbType.String));

                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                // executing procedure to save the record 
                DataSet ds = dtManager.ExecuteDataSet(SP_RPT_RegionwiseSales, dbParam);

                // update database message
                errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();

                // if an error returned from the database
                if (errorMessage != string.Empty)
                    return null;
                else
                {
                    if (ds.Tables.Count > 0)
                    {
                        //string address = Common.ReportHeaderAddress();
                        //string headerText = address.Substring(0, address.IndexOf("*|$|*"));
                        //string addressText = address.Substring(address.IndexOf("*|$|*") + 5, address.Length - address.IndexOf("*|$|*") - 5);
                        //addressText = addressText.Replace("*|$|*", Environment.NewLine);
                        //ds.Tables[0].Columns.Add(new DataColumn("FromDateText", Type.GetType("System.String")));
                        //ds.Tables[0].Columns.Add(new DataColumn("ToDateText", Type.GetType("System.String")));
                        //ds.Tables[0].Columns.Add(new DataColumn("HeaderAddress", Type.GetType("System.String")));
                        //ds.Tables[0].Columns.Add(new DataColumn("AddressText", Type.GetType("System.String")));
                        //ds.Tables[0].Rows[0]["HeaderAddress"] = headerText;
                        //ds.Tables[0].Rows[0]["AddressText"] = addressText;
                        //ds.Tables[0].Columns.Add(new DataColumn("PayDateText", Type.GetType("System.String")));
                        //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        //{
                        //    ds.Tables[0].Rows[i]["PayDateText"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["PayDate"]).ToString(Common.DTP_DATE_FORMAT);
                        //    ds.Tables[0].Rows[i]["FromDateText"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["FromDate"]).ToString(Common.DTP_DATE_FORMAT);
                        //    ds.Tables[0].Rows[i]["ToDateText"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["ToDate"]).ToString(Common.DTP_DATE_FORMAT);
                        //    ds.Tables[0].Rows[i]["Amount"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["Amount"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                        //}
                        //int iCount =5;
                        int iTemp;
                        DataRow dtRow;
                        foreach (DataTable dttable in ds.Tables)
                        {
                            if (dttable.Rows.Count != 5)
                            {
                                iTemp = 5 - dttable.Rows.Count;
                                for (int i = 0; i < iTemp; i++)
                                {
                                    dtRow = dttable.NewRow();
                                    dttable.Rows.Add(dtRow);
                                }
                            }
                            foreach (DataRow drRow in dttable.Rows)
                            {
                                if (!string.IsNullOrEmpty(drRow["Sales"].ToString()))
                                    drRow["Sales"] = Math.Round(Convert.ToDecimal(drRow["Sales"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                                //drRow["fromdate"] = Convert.ToDateTime(drRow["fromdate"]).ToString(Common.DTP_DATE_FORMAT);
                                //drRow["todate"] = dtTo;//Convert.ToDateTime(dtTo).ToString(Common.DTP_DATE_FORMAT);
                            }
                        }
                        foreach (DataRow drow in ds.Tables[6].Rows)
                        {
                            if (!string.IsNullOrEmpty(drow["TotalSales"].ToString()))
                                drow["TotalSales"] = Math.Round(Convert.ToDecimal(drow["TotalSales"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                        }
                        lrds.Add(new ReportDataSource("RegionWiseSales_NorthSales", ds.Tables[0]));
                        lrds.Add(new ReportDataSource("RegionWiseSales_UPSales", ds.Tables[1]));
                        lrds.Add(new ReportDataSource("RegionWiseSales_SouthSales", ds.Tables[2]));
                        lrds.Add(new ReportDataSource("RegionWiseSales_EastSales", ds.Tables[3]));
                        lrds.Add(new ReportDataSource("RegionWiseSales_NorthEastSales", ds.Tables[4]));
                        lrds.Add(new ReportDataSource("RegionWiseSales_WestSales", ds.Tables[5]));
                        lrds.Add(new ReportDataSource("RegionWiseSales_NepalSales", ds.Tables[6]));
                        lrds.Add(new ReportDataSource("RegionWiseSales_CenteralSales", ds.Tables[7]));

                    }
                    GetLastMonthData(lrds, dtTo);
                }
            }
            catch (Exception ex)
            {

                Vinculum.Framework.Logging.LogManager.WriteExceptionLog(ex);
            }
            return lrds;
        }



        public static List<ReportDataSource> GetRegionSalesDataMiniBranch(ref string errorMessage, string dtTo)
        {
            List<ReportDataSource> lrds = new List<ReportDataSource>();
            try
            {
                DBParameterList dbParam;

                Vinculum.Framework.Data.DataTaskManager dtManager = new DataTaskManager();

                // initialize the parameter list object
                dbParam = new DBParameterList();

                // add the relevant parameters
                dbParam.Add(new DBParameter("Current", "Y", DbType.String));
                dbParam.Add(new DBParameter("Date", dtTo, DbType.String));

                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                // executing procedure to save the record 
                DataSet ds = dtManager.ExecuteDataSet(SP_RPT_RegionwiseSalesMiniBranch, dbParam);

                // update database message
                errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();

                // if an error returned from the database
                if (errorMessage != string.Empty)
                    return null;
                else
                {
                    if (ds.Tables.Count > 0)
                    {
                        //string address = Common.ReportHeaderAddress();
                        //string headerText = address.Substring(0, address.IndexOf("*|$|*"));
                        //string addressText = address.Substring(address.IndexOf("*|$|*") + 5, address.Length - address.IndexOf("*|$|*") - 5);
                        //addressText = addressText.Replace("*|$|*", Environment.NewLine);
                        //ds.Tables[0].Columns.Add(new DataColumn("FromDateText", Type.GetType("System.String")));
                        //ds.Tables[0].Columns.Add(new DataColumn("ToDateText", Type.GetType("System.String")));
                        //ds.Tables[0].Columns.Add(new DataColumn("HeaderAddress", Type.GetType("System.String")));
                        //ds.Tables[0].Columns.Add(new DataColumn("AddressText", Type.GetType("System.String")));
                        //ds.Tables[0].Rows[0]["HeaderAddress"] = headerText;
                        //ds.Tables[0].Rows[0]["AddressText"] = addressText;
                        //ds.Tables[0].Columns.Add(new DataColumn("PayDateText", Type.GetType("System.String")));
                        //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        //{
                        //    ds.Tables[0].Rows[i]["PayDateText"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["PayDate"]).ToString(Common.DTP_DATE_FORMAT);
                        //    ds.Tables[0].Rows[i]["FromDateText"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["FromDate"]).ToString(Common.DTP_DATE_FORMAT);
                        //    ds.Tables[0].Rows[i]["ToDateText"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["ToDate"]).ToString(Common.DTP_DATE_FORMAT);
                        //    ds.Tables[0].Rows[i]["Amount"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["Amount"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                        //}
                        //int iCount =5;
                        int iTemp;
                        DataRow dtRow;
                        foreach (DataTable dttable in ds.Tables)
                        {
                            if (dttable.Rows.Count != 5)
                            {
                                iTemp = 5 - dttable.Rows.Count;
                                for (int i = 0; i < iTemp; i++)
                                {
                                    dtRow = dttable.NewRow();
                                    dttable.Rows.Add(dtRow);
                                }
                            }
                            foreach (DataRow drRow in dttable.Rows)
                            {
                                if (!string.IsNullOrEmpty(drRow["Sales"].ToString()))
                                    drRow["Sales"] = Math.Round(Convert.ToDecimal(drRow["Sales"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                                //drRow["fromdate"] = Convert.ToDateTime(drRow["fromdate"]).ToString(Common.DTP_DATE_FORMAT);
                                //drRow["todate"] = dtTo;//Convert.ToDateTime(dtTo).ToString(Common.DTP_DATE_FORMAT);
                            }
                        }
                        foreach (DataRow drow in ds.Tables[6].Rows)
                        {
                            if (!string.IsNullOrEmpty(drow["TotalSales"].ToString()))
                                drow["TotalSales"] = Math.Round(Convert.ToDecimal(drow["TotalSales"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                        }
                        lrds.Add(new ReportDataSource("RegionWiseSales_NorthSales", ds.Tables[0]));
                        lrds.Add(new ReportDataSource("RegionWiseSales_UPSales", ds.Tables[1]));
                        lrds.Add(new ReportDataSource("RegionWiseSales_SouthSales", ds.Tables[2]));
                        lrds.Add(new ReportDataSource("RegionWiseSales_EastSales", ds.Tables[3]));
                        lrds.Add(new ReportDataSource("RegionWiseSales_NorthEastSales", ds.Tables[4]));
                        lrds.Add(new ReportDataSource("RegionWiseSales_WestSales", ds.Tables[5]));
                        lrds.Add(new ReportDataSource("RegionWiseSales_NepalSales", ds.Tables[6]));
                        lrds.Add(new ReportDataSource("RegionWiseSales_CenteralSales", ds.Tables[7]));

                    }
                    GetLastMonthDataMiniBranch(lrds, dtTo);
                }
            }
            catch (Exception ex)
            {

                Vinculum.Framework.Logging.LogManager.WriteExceptionLog(ex);
            }
            return lrds;
        }



        public static List<ReportDataSource> GetMonthItemSalesData(ref string errorMessage, string @Current, int locationId, string itemcode)
        {
            List<ReportDataSource> lrds = new List<ReportDataSource>();
            try
            {
                DBParameterList dbParam;

                Vinculum.Framework.Data.DataTaskManager dtManager = new DataTaskManager();

                // initialize the parameter list object
                dbParam = new DBParameterList();

                // add the relevant parameters
                dbParam.Add(new DBParameter("@Current", Current, DbType.String));
                //dbParam.Add(new DBParameter("Date", dtTo, DbType.String));
                dbParam.Add(new DBParameter("locationId", locationId, DbType.Int32));
                dbParam.Add(new DBParameter("itemcode", itemcode, DbType.String));

                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                // executing procedure to save the record 
                DataSet ds = dtManager.ExecuteDataSet(SP_RPT_MonthItemwiseSales, dbParam);

                // update database message
                errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();

                // if an error returned from the database
                if (errorMessage != string.Empty)
                    return null;
                else
                {
                    if (ds.Tables.Count > 0)
                    {

                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            ds.Tables[0].Rows[i]["CurrentMonthQty"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["CurrentMonthQty"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                        }

                        for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                        {
                            ds.Tables[1].Rows[i]["PrvQuantity"] = Math.Round(Convert.ToDecimal(ds.Tables[1].Rows[i]["PrvQuantity"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                        }
                        lrds.Add(new ReportDataSource("MonthwiseItemSales_MonthItemWiseSales", ds.Tables[0]));
                        lrds.Add(new ReportDataSource("MonthwiseItemSales_MonthItemWiseSales1", ds.Tables[1]));

                        //lrds.Add(new ReportDataSource("MonthItemWiseSales_UPSales", ds.Tables[1]));
                        //lrds.Add(new ReportDataSource("MonthItemWiseSales_SouthSales", ds.Tables[2]));
                        //lrds.Add(new ReportDataSource("MonthItemWiseSales_EastSales", ds.Tables[3]));
                        //lrds.Add(new ReportDataSource("MonthItemWiseSales_NorthEastSales", ds.Tables[4]));
                        //lrds.Add(new ReportDataSource("MonthItemWiseSales_WestSales", ds.Tables[5]));
                        //lrds.Add(new ReportDataSource("MonthItemWiseSales_NepalSales", ds.Tables[6]));
                        //lrds.Add(new ReportDataSource("MonthItemWiseSales_CenteralSales", ds.Tables[7]));

                    }
                    //GetLastMonthData(lrds, dtTo);
                }
            }
            catch (Exception ex)
            {

                Vinculum.Framework.Logging.LogManager.WriteExceptionLog(ex);
            }
            return lrds;
        }

        public static void GetLastMonthData(List<ReportDataSource> lrds, String dtTo)
        {

            try
            {
                DBParameterList dbParam;

                Vinculum.Framework.Data.DataTaskManager dtManager = new DataTaskManager();

                // initialize the parameter list object
                dbParam = new DBParameterList();

                // add the relevant parameters
                dbParam.Add(new DBParameter("Current", "N", DbType.String));
                dbParam.Add(new DBParameter("Date", dtTo, DbType.String));

                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                // executing procedure to save the record 
                DataSet ds = dtManager.ExecuteDataSet(SP_RPT_RegionwiseSales, dbParam);

                // update database message
                //errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();

                // if an error returned from the database
                //if (errorMessage != string.Empty)
                // return null;
                // else
                {
                    if (ds.Tables.Count > 0)
                    {
                        int iTemp;
                        DataRow dtRow;
                        foreach (DataTable dttable in ds.Tables)
                        {
                            if (dttable.Rows.Count != 5)
                            {
                                iTemp = 5 - dttable.Rows.Count;
                                for (int i = 0; i < iTemp; i++)
                                {
                                    dtRow = dttable.NewRow();
                                    dttable.Rows.Add(dtRow);
                                }
                            }
                            foreach (DataRow drRow in dttable.Rows)
                            {
                                if (!string.IsNullOrEmpty(drRow["Sales"].ToString()))
                                    drRow["Sales"] = Math.Round(Convert.ToDecimal(drRow["Sales"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);


                            }
                        }
                        foreach (DataRow drow in ds.Tables[6].Rows)
                        {
                            if (!string.IsNullOrEmpty(drow["TotalSales"].ToString()))
                                drow["TotalSales"] = Math.Round(Convert.ToDecimal(drow["TotalSales"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                        }
                        lrds.Add(new ReportDataSource("RegionWiseSales_L_NorthSales", ds.Tables[0]));
                        lrds.Add(new ReportDataSource("RegionWiseSales_L_UPSales", ds.Tables[1]));
                        lrds.Add(new ReportDataSource("RegionWiseSales_L_SouthSales", ds.Tables[2]));
                        lrds.Add(new ReportDataSource("RegionWiseSales_L_EastSales", ds.Tables[3]));
                        lrds.Add(new ReportDataSource("RegionWiseSales_L_NorthEastSales", ds.Tables[4]));
                        lrds.Add(new ReportDataSource("RegionWiseSales_L_WestSales", ds.Tables[5]));
                        lrds.Add(new ReportDataSource("RegionWiseSales_L_NepalSales", ds.Tables[6]));
                        lrds.Add(new ReportDataSource("RegionWiseSales_L_CenteralSales", ds.Tables[7]));

                    }
                }
            }
            catch (Exception ex)
            {

                Vinculum.Framework.Logging.LogManager.WriteExceptionLog(ex);
            }

        }


        public static void GetLastMonthDataMiniBranch(List<ReportDataSource> lrds, String dtTo)
        {

            try
            {
                DBParameterList dbParam;

                Vinculum.Framework.Data.DataTaskManager dtManager = new DataTaskManager();

                // initialize the parameter list object
                dbParam = new DBParameterList();

                // add the relevant parameters
                dbParam.Add(new DBParameter("Current", "N", DbType.String));
                dbParam.Add(new DBParameter("Date", dtTo, DbType.String));

                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                // executing procedure to save the record 
                DataSet ds = dtManager.ExecuteDataSet(SP_RPT_RegionwiseSalesMiniBranch, dbParam);

                // update database message
                //errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();

                // if an error returned from the database
                //if (errorMessage != string.Empty)
                // return null;
                // else
                {
                    if (ds.Tables.Count > 0)
                    {
                        int iTemp;
                        DataRow dtRow;
                        foreach (DataTable dttable in ds.Tables)
                        {
                            if (dttable.Rows.Count != 5)
                            {
                                iTemp = 5 - dttable.Rows.Count;
                                for (int i = 0; i < iTemp; i++)
                                {
                                    dtRow = dttable.NewRow();
                                    dttable.Rows.Add(dtRow);
                                }
                            }
                            foreach (DataRow drRow in dttable.Rows)
                            {
                                if (!string.IsNullOrEmpty(drRow["Sales"].ToString()))
                                    drRow["Sales"] = Math.Round(Convert.ToDecimal(drRow["Sales"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);


                            }
                        }
                        foreach (DataRow drow in ds.Tables[6].Rows)
                        {
                            if (!string.IsNullOrEmpty(drow["TotalSales"].ToString()))
                                drow["TotalSales"] = Math.Round(Convert.ToDecimal(drow["TotalSales"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                        }
                        lrds.Add(new ReportDataSource("RegionWiseSales_L_NorthSales", ds.Tables[0]));
                        lrds.Add(new ReportDataSource("RegionWiseSales_L_UPSales", ds.Tables[1]));
                        lrds.Add(new ReportDataSource("RegionWiseSales_L_SouthSales", ds.Tables[1]));
                        lrds.Add(new ReportDataSource("RegionWiseSales_L_EastSales", ds.Tables[2]));
                        lrds.Add(new ReportDataSource("RegionWiseSales_L_NorthEastSales", ds.Tables[4]));
                        lrds.Add(new ReportDataSource("RegionWiseSales_L_WestSales", ds.Tables[3]));
                        lrds.Add(new ReportDataSource("RegionWiseSales_L_NepalSales", ds.Tables[4]));
                        lrds.Add(new ReportDataSource("RegionWiseSales_L_CenteralSales", ds.Tables[7]));

                    }
                }
            }
            catch (Exception ex)
            {

                Vinculum.Framework.Logging.LogManager.WriteExceptionLog(ex);
            }

        }




        public static void GetLastMonthDataForState(List<ReportDataSource> lrds, String dtTo)
        {

            try
            {
                DBParameterList dbParam;

                Vinculum.Framework.Data.DataTaskManager dtManager = new DataTaskManager();

                // initialize the parameter list object
                dbParam = new DBParameterList();

                // add the relevant parameters
                dbParam.Add(new DBParameter("Current", "N", DbType.String));
                dbParam.Add(new DBParameter("Date", dtTo, DbType.String));

                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                // executing procedure to save the record 
                DataSet ds = dtManager.ExecuteDataSet(SP_RPT_StatewiseSales, dbParam);

                // update database message
                //errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();

                // if an error returned from the database
                //if (errorMessage != string.Empty)
                // return null;
                // else
                {
                    if (ds.Tables.Count > 0)
                    {
                        int iTemp;
                        DataRow dtRow;
                        foreach (DataTable dttable in ds.Tables)
                        {
                            if (dttable.Rows.Count != 7)
                            {
                                iTemp = 7 - dttable.Rows.Count;
                                for (int i = 0; i < iTemp; i++)
                                {
                                    dtRow = dttable.NewRow();
                                    dttable.Rows.Add(dtRow);
                                }
                            }
                            foreach (DataRow drRow in dttable.Rows)
                            {
                                if (!string.IsNullOrEmpty(drRow["Sales"].ToString()))
                                    drRow["Sales"] = Math.Round(Convert.ToDecimal(drRow["Sales"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);


                            }
                        }
                        foreach (DataRow drow in ds.Tables[4].Rows)
                        {
                            if (!string.IsNullOrEmpty(drow["TotalSales"].ToString()))
                                drow["TotalSales"] = Math.Round(Convert.ToDecimal(drow["TotalSales"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                        }
                        lrds.Add(new ReportDataSource("StateZoneWiseSales_L_NorthSales_State", ds.Tables[0]));
                        //lrds.Add(new ReportDataSource("StateZoneWiseSales_L_UPSales_State", ds.Tables[1]));
                        lrds.Add(new ReportDataSource("StateZoneWiseSales_L_SouthSales_State", ds.Tables[1]));
                        lrds.Add(new ReportDataSource("StateZoneWiseSales_L_EastSales_State", ds.Tables[2]));
                        //lrds.Add(new ReportDataSource("StateZoneWiseSales_L_NorthEastSales_State", ds.Tables[4]));
                        lrds.Add(new ReportDataSource("StateZoneWiseSales_L_WestSales_State", ds.Tables[3]));
                        lrds.Add(new ReportDataSource("StateZoneWiseSales_L_NepalSales_State", ds.Tables[4]));
                       // lrds.Add(new ReportDataSource("StateZoneWiseSales_L_CenteralSales_State", ds.Tables[7]));

                    }
                }
            }
            catch (Exception ex)
            {

                Vinculum.Framework.Logging.LogManager.WriteExceptionLog(ex);
            }

        }


        public static bool ValidateItemBatchAdjustment(ref string dbMessage, string dtFrom, string dtTo, int BOLocation, string ItemCode)
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









        public static List<ReportDataSource> ItemBatchAdjustment(ref string errorMessage, string dtFrom, string dtTo, int BOLocation, string ItemCode)
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
                dbParam.Add(new DBParameter("@BOLocation", BOLocation, DbType.String));
                dbParam.Add(new DBParameter("@ItemCode", ItemCode, DbType.String));


                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                // executing procedure to save the record 
                DataSet ds = dtManager.ExecuteDataSet(SP_RPT_ItemBatchAdjustment, dbParam);

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
                        ds.Tables[0].Columns.Add(new DataColumn("dtFrom", Type.GetType("System.String")));
                        ds.Tables[0].Columns.Add(new DataColumn("dtTo", Type.GetType("System.String")));
                        ds.Tables[0].Columns.Add(new DataColumn("HeaderAddress", Type.GetType("System.String")));
                        ds.Tables[0].Columns.Add(new DataColumn("AddressText", Type.GetType("System.String")));
                        ds.Tables[0].Rows[0]["HeaderAddress"] = headerText;
                        ds.Tables[0].Rows[0]["AddressText"] = addressText;
                        //ds.Tables[0].Columns.Add(new DataColumn("InvoiceDateText", Type.GetType("System.String")));
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            //ds.Tables[0].Rows[i]["InvoiceDateText"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["InvoiceDate"]).ToString(Common.DTP_DATE_FORMAT);
                            //ds.Tables[0].Rows[i]["InvoiceAmount"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["InvoiceAmount"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["TransQty"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["TransQty"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                            // ds.Tables[0].Rows[i]["NetAmount"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["NetAmount"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                            //ds.Tables[0].Rows[i]["@dtFrom"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["@dtFrom"]).ToString(Common.DTP_DATE_FORMAT);
                            //ds.Tables[0].Rows[i]["FromDateText"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["@dtFrom"]).ToString(Common.DTP_DATE_FORMAT);
                            //ds.Tables[0].Rows[i]["@dtTo"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["@dtTo"]).ToString(Common.DTP_DATE_FORMAT);
                        }
                        lrds.Add(new ReportDataSource("ItemBatchAdjustment_ItemBatch", ds.Tables[0]));
                    }
                }
            }
            catch (Exception ex)
            {
                Vinculum.Framework.Logging.LogManager.WriteExceptionLog(ex);
            }
            return lrds;
        }

        public static List<ReportDataSource> OrderCollectionSearch(ref string errorMessage, int BOlocationId, int PClocationId, int UserId, string dtFrom, string dtTo)
        {
            List<ReportDataSource> lrds = new List<ReportDataSource>();
            int iCount = 0;
            try
            {
                DBParameterList dbParam;

                Vinculum.Framework.Data.DataTaskManager dtManager = new DataTaskManager();

                // initialize the parameter list object
                dbParam = new DBParameterList();

                // add the relevant parameters
                dbParam.Add(new DBParameter("BolocationId", BOlocationId, DbType.Int32));
                dbParam.Add(new DBParameter("PClocationId", PClocationId, DbType.Int32));
                dbParam.Add(new DBParameter("UserId", UserId, DbType.Int32));
                dbParam.Add(new DBParameter("FromDate", dtFrom, DbType.String));
                dbParam.Add(new DBParameter("ToDate", dtTo, DbType.String));

                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                // executing procedure to save the record 
                DataSet ds = dtManager.ExecuteDataSet(SP_RPT_ORDER_COLLECTION, dbParam);

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
                        ds.Tables[0].Columns.Add(new DataColumn("visible", Type.GetType("System.String")));
                        ds.Tables[0].Rows[0]["HeaderAddress"] = headerText;
                        ds.Tables[0].Rows[0]["AddressText"] = addressText;
                        ds.Tables[0].Rows[0]["visible"] = ds.Tables[1].Rows[0]["visible"].ToString();
                        //iCount = Convert.ToInt32(ds.Tables[1].Rows[0]["visible"]);
                        //ds.Tables[0].Columns.Add(new DataColumn("InvoiceDateText", Type.GetType("System.String")));
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            //ds.Tables[0].Rows[i]["DateText"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["InvoiceDate"]).ToString(Common.DTP_DATE_FORMAT);
                            ds.Tables[0].Rows[i]["FromDateText"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["FromDate"]).ToString(Common.DTP_DATE_FORMAT);
                            ds.Tables[0].Rows[i]["ToDateText"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["ToDate"]).ToString(Common.DTP_DATE_FORMAT);
                            ds.Tables[0].Rows[i]["Cash"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["Cash"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["CreditCard"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["CreditCard"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["Forex"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["Forex"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["Cheque"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["Cheque"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["BonusCheque"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["BonusCheque"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["Bank"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["Bank"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["COD"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["COD"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["AvailableBefore"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["AvailableBefore"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["DepositInBetween"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["DepositInBetween"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["ChangeAmt"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["ChangeAmt"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["Date"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["Date"]).ToString(Common.DTP_DATE_FORMAT);
                        }
                        lrds.Add(new ReportDataSource("OrderCollection_OrderCollection", ds.Tables[0]));
                    }
                }
            }
            catch (Exception ex)
            {
                Vinculum.Framework.Logging.LogManager.WriteExceptionLog(ex);
            }
            return lrds;
        }

        public static List<ReportDataSource> BonusStatementDirectors(ref string errorMessage, int month, int year, string DistributorID)
        {
            //month = 12;
            //year = 2010;
            //DistributorID = "11000160";
            List<ReportDataSource> lrds = new List<ReportDataSource>();
            try
            {
            
                DBParameterList dbParam;

                Vinculum.Framework.Data.DataTaskManager dtManager = new DataTaskManager();

                // initialize the parameter list object
                dbParam = new DBParameterList();

                // add the relevant parameters
                dbParam.Add(new DBParameter("month", month, DbType.Int32));
                dbParam.Add(new DBParameter("year", year, DbType.Int32));
                dbParam.Add(new DBParameter("DistributorID", DistributorID, DbType.String));
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                // executing procedure to save the record 
                DataSet ds = dtManager.ExecuteDataSet(SP_RPT_BonusStatementDirectors, dbParam);

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
                        ds.Tables[0].Columns.Add(new DataColumn("PayDateText", Type.GetType("System.String")));
                        ds.Tables[3].Columns.Add(new DataColumn("BonusChequeinWords", Type.GetType("System.String")));
                        ds.Tables[4].Columns.Add(new DataColumn("AmountinWords", Type.GetType("System.String")));
                        //TABLE[1] ---Performance Bonus
                        for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                        {
                            ds.Tables[1].Rows[i]["SelfPv"] = Math.Round(Convert.ToDecimal(ds.Tables[1].Rows[i]["SelfPV"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[1].Rows[i]["SelfBv"] = Math.Round(Convert.ToDecimal(ds.Tables[1].Rows[i]["SelfBV"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[1].Rows[i]["TotalPV"] = Math.Round(Convert.ToDecimal(ds.Tables[1].Rows[i]["TotalPV"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[1].Rows[i]["TotalBV"] = Math.Round(Convert.ToDecimal(ds.Tables[1].Rows[i]["TotalBV"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[1].Rows[i]["CUMPV"] = Math.Round(Convert.ToDecimal(ds.Tables[1].Rows[i]["CUMPV"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[1].Rows[i]["CUMBV"] = Math.Round(Convert.ToDecimal(ds.Tables[1].Rows[i]["CUMBV"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[1].Rows[i]["GroupPV"] = Math.Round(Convert.ToDecimal(ds.Tables[1].Rows[i]["GroupPV"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[1].Rows[i]["GroupBV"] = Math.Round(Convert.ToDecimal(ds.Tables[1].Rows[i]["GroupBV"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                        }
                        //TABLE[2] --BonusStatementDirectors_DownlineInfo
                        for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                        {

                            ds.Tables[2].Rows[i]["PV"] = Math.Round(Convert.ToDecimal(ds.Tables[2].Rows[i]["PV"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[2].Rows[i]["BV"] = Math.Round(Convert.ToDecimal(ds.Tables[2].Rows[i]["BV"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[2].Rows[i]["BonusPercent"] = Math.Round(Convert.ToDecimal(ds.Tables[2].Rows[i]["BonusPercent"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                        }

                        //Table[3] -- Check qualification for Bonuses (BonusStatementDirectors_DistributorGroupMonthly)
                        //Table[4] --(BonusStatementDirectors_DistributorGroupMonthly1)
                        
                        //--Table[6] --(BonusStatementDirectors_DistributorPayOrderInfo)
                        //--Table[7] --BonusStatementDirectors_DistributorGiftVoucherInfo
                        //--Table[8] --BonusStatementDirectors_DistributorPBAmountInfo (Not used in report)
                        //for (int i = 0; i < ds.Tables[8].Rows.Count; i++)
                        //{

                        //    ds.Tables[8].Rows[i]["Amount"] = Math.Round(Convert.ToDecimal(ds.Tables[8].Rows[i]["Amount"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);

                        //}
                        //--Table[9] --BonusStatementDirectors_DistributorCarInfo
                        for (int i = 0; i < ds.Tables[9].Rows.Count; i++)
                        {

                            ds.Tables[9].Rows[i]["CumulativeCarFund"] = Math.Round(Convert.ToDecimal(ds.Tables[9].Rows[i]["CumulativeCarFund"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[9].Rows[i]["PaidAmount"] = Math.Round(Convert.ToDecimal(ds.Tables[9].Rows[i]["PaidAmount"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[9].Rows[i]["TotalPayable"] = Math.Round(Convert.ToDecimal(ds.Tables[9].Rows[i]["TotalPayable"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);

                        }
                        //--Table[10] --BonusStatementDirectors_DistributorCarInfo
                        for (int i = 0; i < ds.Tables[10].Rows.Count; i++)
                        {

                            ds.Tables[10].Rows[i]["CurrentCarBonus"] = Math.Round(Convert.ToDecimal(ds.Tables[10].Rows[i]["CurrentCarBonus"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);

                        }
                        //--Table[11] --BonusStatementDirectors_DistributorViaChequeInfo
                        for (int i = 0; i < ds.Tables[11].Rows.Count; i++)
                        {
                            ds.Tables[11].Rows[i]["ViaCheque"] = ds.Tables[11].Rows[i][0];
                        }
                        //--Table[12] --BonusStatementDirectors_DistPerformanceBonus
                        for (int i = 0; i < ds.Tables[12].Rows.Count; i++)
                        {
                            ds.Tables[12].Rows[0]["PerformanceBonus"] = ds.Tables[12].Rows[0][0];
                        }
                        //--Table[13] --BonusStatementDirectors_BonusChkVoucher
                        for (int i = 0; i < ds.Tables[13].Rows.Count; i++)
                        {
                            ds.Tables[13].Rows[0]["BonusChkVoucher"] = ds.Tables[13].Rows[0][0];

                        }
                        //--Table[14] --BonusStatementDirectors_ProductVoucher
                        for (int i = 0; i < ds.Tables[14].Rows.Count; i++)
                        {
                            ds.Tables[14].Rows[0]["ProductVoucher"] = ds.Tables[14].Rows[0][0];
                        }
                        //--Table[15] --BonusStatementDirectors_QualPvNonPV
                        for (int i = 0; i < ds.Tables[15].Rows.Count; i++)
                        {
                            ds.Tables[15].Rows[i]["TaxDeducted"] = Math.Round(Convert.ToDecimal(ds.Tables[15].Rows[i]["TaxDeducted"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[15].Rows[i]["TotalPV"] = Math.Round(Convert.ToDecimal(ds.Tables[15].Rows[i]["TotalPV"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[15].Rows[i]["ExclPV"] = Math.Round(Convert.ToDecimal(ds.Tables[15].Rows[i]["ExclPV"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[15].Rows[i]["NonQualifiedPV"] = Math.Round(Convert.ToDecimal(ds.Tables[15].Rows[i]["NonQualifiedPV"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[15].Rows[i]["QualifiedDirect"] = Math.Round(Convert.ToDecimal(ds.Tables[15].Rows[i]["QualifiedDirect"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                        }

                        //--Table[16] --BonusStatementDirectors_GroupArchiveSummary
                        for (int i = 0; i < ds.Tables[16].Rows.Count; i++)
                        {
                            ds.Tables[16].Rows[0]["GroupSummary"] = ds.Tables[16].Rows[0][0];
                        }
                        //--Table[17] --BonusStatementDirectors_DistributorTFInfo 
                        for (int i = 0; i < ds.Tables[17].Rows.Count; i++)
                        {

                            ds.Tables[17].Rows[i]["CumulativeTravelFund"] = Math.Round(Convert.ToDecimal(ds.Tables[17].Rows[i]["CumulativeTravelFund"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[17].Rows[i]["PaidTravelFund"] = Math.Round(Convert.ToDecimal(ds.Tables[17].Rows[i]["PaidTravelFund"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[17].Rows[i]["PayableTravelFund"] = Math.Round(Convert.ToDecimal(ds.Tables[17].Rows[i]["PayableTravelFund"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[17].Rows[i]["CurrentTravelFund"] = Math.Round(Convert.ToDecimal(ds.Tables[17].Rows[i]["CurrentTravelFund"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);

                        }
                        //--Table[18] --BonusStatementDirectors_Declaration
                        //for (int i = 0; i < ds.Tables[18].Rows.Count; i++)
                        //{
                        //    ds.Tables[18].Rows[0]["Declaration"] = ds.Tables[18].Rows[0][0];
                        //}
                        for (int i = 0; i < ds.Tables[19].Rows.Count; i++)
                        {
                            ds.Tables[19].Rows[0]["TotalBvAmount"] = ds.Tables[19].Rows[0][0];
                        }

                        // Max allowed 20 Voucher

                        for (int i = 20; i < 40; i++)
                        {
                            try
                            {
                                if (ds.Tables[i] != null)
                                {
                                    continue;
                                }
                            }
                            catch (IndexOutOfRangeException ex)
                            {
                                DataTable dt = new DataTable();
                                dt.Clear();
                                dt.Columns.Add(new DataColumn("VoucherSrNo", typeof(string)));
                                dt.Columns.Add(new DataColumn("IssuedTo", typeof(string)));
                                dt.Columns.Add(new DataColumn("Name", typeof(string)));
                                dt.Columns.Add(new DataColumn("Expirydate", typeof(string)));
                                dt.Columns.Add(new DataColumn("Issuedate", typeof(string)));
                                dt.Columns.Add(new DataColumn("Quantity", typeof(string)));
                                dt.Columns.Add(new DataColumn("ItemId", typeof(string)));
                                dt.Columns.Add(new DataColumn("ItemName", typeof(string)));

                                ds.Tables.Add(dt);
                            }
 
                        
                        }
                        lrds.Add(new ReportDataSource("BonusStatementDirectors_DistributorInfo", ds.Tables[0]));
                        lrds.Add(new ReportDataSource("BonusStatementDirectors_PerformanceBonus", ds.Tables[1]));
                        lrds.Add(new ReportDataSource("BonusStatementDirectors_DownlineInfo", ds.Tables[2]));
                        lrds.Add(new ReportDataSource("BonusStatementDirectors_DistributorGroupMonthly", ds.Tables[3]));
                        lrds.Add(new ReportDataSource("BonusStatementDirectors_DistributorGroupMonthly1", ds.Tables[4]));
                        lrds.Add(new ReportDataSource("BonusStatementDirectors_DistributorTotal", ds.Tables[5]));
                        lrds.Add(new ReportDataSource("BonusStatementDirectors_DistributorPayOrderInfo", ds.Tables[6]));
                        lrds.Add(new ReportDataSource("BonusStatementDirectors_DistributorGiftVoucherInfo", ds.Tables[7]));
                        lrds.Add(new ReportDataSource("BonusStatementDirectors_DistributorPBAmountInfo", ds.Tables[8]));
                        lrds.Add(new ReportDataSource("BonusStatementDirectors_DistributorCarInfo", ds.Tables[9]));
                        lrds.Add(new ReportDataSource("BonusStatementDirectors_DistributorCurrentMnthCarInfo", ds.Tables[10]));
                        lrds.Add(new ReportDataSource("BonusStatementDirectors_DistributorViaChequeInfo", ds.Tables[11]));

                        lrds.Add(new ReportDataSource("BonusStatementDirectors_DistPerformanceBonus", ds.Tables[12]));
                        lrds.Add(new ReportDataSource("BonusStatementDirectors_BonusChkVoucher", ds.Tables[13]));
                        lrds.Add(new ReportDataSource("BonusStatementDirectors_ProductVoucher", ds.Tables[14]));
                        lrds.Add(new ReportDataSource("BonusStatementDirectors_QualPvNonPV", ds.Tables[15]));
                        lrds.Add(new ReportDataSource("BonusStatementDirectors_GroupArchiveSummary", ds.Tables[16]));
                        lrds.Add(new ReportDataSource("BonusStatementDirectors_DistributorTFInfo", ds.Tables[17]));
                        lrds.Add(new ReportDataSource("BonusStatementDirectors_Declaration", ds.Tables[18]));
                        lrds.Add(new ReportDataSource("BonusStatementDirectors_TotalBvAmount", ds.Tables[19]));
                        int VoucherID = 1;
                        for (int i = 20; i < 40; i++)
                        {                            
                            string strVoucherName = "BonusStatementDirectors_Voucher" + VoucherID.ToString();
                            lrds.Add(new ReportDataSource(strVoucherName,ds.Tables[i]));
                            VoucherID++;
                        }
                        //Table[5] --(BonusStatementDirectors_DistributorTotal)
                        for (int i = 0; i < ds.Tables[5].Rows.Count; i++)
                        {

                            //ds.Tables[4].Rows[i]["Amount"] = Math.Round(Convert.ToDecimal(ds.Tables[4].Rows[i]["Amount"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                            string str = Common.AmountToWords.AmtInWord(Convert.ToDecimal(ds.Tables[5].Rows[i]["Amount"]));
                            string substr = str.Remove(0, 6);
                            DataTable dsN = (DataTable)lrds.Find(p => p.Name == "BonusStatementDirectors_DistributorTotal").Value;
                            dsN.Rows[i]["BonusAmountWord"] = substr;
                        }

                    }
                }
            }
            catch (Exception ex)
            {

                Vinculum.Framework.Logging.LogManager.WriteExceptionLog(ex);
            }
            return lrds;
        }

        public static List<ReportDataSource> ConsolidatedSalesCollectionSearch(ref string errorMessage, int StateId, int DLCP, int BOlocationId, int PClocationId, int UserId, string dtFrom, string dtTo)
        {
            List<ReportDataSource> lrds = new List<ReportDataSource>();
            int iCount = 0;
            try
            {
                DBParameterList dbParam;

                Vinculum.Framework.Data.DataTaskManager dtManager = new DataTaskManager();

                // initialize the parameter list object
                dbParam = new DBParameterList();

                // add the relevant parameters
                dbParam.Add(new DBParameter("StateId", StateId, DbType.Int32));
                dbParam.Add(new DBParameter("DLCP", DLCP, DbType.Int32));
                dbParam.Add(new DBParameter("BolocationId", BOlocationId, DbType.Int32));
                dbParam.Add(new DBParameter("PClocationId", PClocationId, DbType.Int32));
                dbParam.Add(new DBParameter("UserId", UserId, DbType.Int32));
                dbParam.Add(new DBParameter("FromDate", dtFrom, DbType.String));
                dbParam.Add(new DBParameter("ToDate", dtTo, DbType.String));

                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                // executing procedure to save the record 
                DataSet ds = dtManager.ExecuteDataSet(SP_RPT_Consolidated_SALES_COLLECTION, dbParam);

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
                        ds.Tables[0].Columns.Add(new DataColumn("visible", Type.GetType("System.String")));
                        ds.Tables[0].Rows[0]["HeaderAddress"] = headerText;
                        ds.Tables[0].Rows[0]["AddressText"] = addressText;
                        ds.Tables[0].Rows[0]["visible"] = ds.Tables[1].Rows[0]["visible"].ToString();
                        //iCount = Convert.ToInt32(ds.Tables[1].Rows[0]["visible"]);
                        ds.Tables[0].Columns.Add(new DataColumn("InvoiceDateText", Type.GetType("System.String")));
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            // ds.Tables[0].Rows[i]["InvoiceDateText"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["InvoiceDate"]).ToString(Common.DTP_DATE_FORMAT);
                            ds.Tables[0].Rows[i]["FromDateText"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["FromDate"]).ToString(Common.DTP_DATE_FORMAT);
                            ds.Tables[0].Rows[i]["ToDateText"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["ToDate"]).ToString(Common.DTP_DATE_FORMAT);
                            ds.Tables[0].Rows[i]["Cash"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["Cash"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["CreditCard"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["CreditCard"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["Forex"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["Forex"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["Cheque"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["Cheque"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["BonusCheque"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["BonusCheque"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["Bank"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["Bank"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["COD"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["COD"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                            // ds.Tables[0].Rows[i]["AvailableBefore"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["AvailableBefore"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                            // ds.Tables[0].Rows[i]["DepositInBetween"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["DepositInBetween"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["ChangeAmt"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["ChangeAmt"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                        }
                        lrds.Add(new ReportDataSource("SalesCollection_SalesCollectionDataTable", ds.Tables[0]));
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
