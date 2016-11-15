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
    public class Inventory
    { 

        #region SP Declaration

        private const string SP_RPT_STNITEMWISE = "usp_rptStnItemWise";
        private const string SP_RPT_PRODUCTWISESALES = "usp_rptProductWiseSales";
        private const string SP_RPT_STOCKSUMMARY = "usp_rptStockSummary";
        private const string SP_RPT_BOWISESALES = "usp_rptBOWiseSales";
        private const string SP_RPT_FREEITEMSSALES = "usp_rptFreeItemsSales";
        private const string SP_RPT_STNGOODSINTRANSIT = "usp_rptGoodsInTransit";
        private const string SP_RPT_STNGOODSINTRANSIT1 = "usp_rptGoodsInTransit1";
        private const string SP_RPT_POITEMWISE = "usp_rptPOItemWise";
        private const string SP_RPT_VENDORITEMWISE = "usp_rptVendorItemWise";
       //private const string SP_RPT_BOSWISESALESTOCK = "Usp_BOWiseSalesStock";
        private const string SP_RPT_BOSWISESALESTOCK = "USP_BOWiseStockInTransit";

        #endregion

        #region Validations

        public static bool ValidateSTNItemWise(ref string errorMessage,string dtFrom, string dtTo, int from, int to)
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

        public static bool ValidateReceievdItemWise(ref string errorMessage, string dtFrom, string dtTo, int from)
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

        public static bool ValidateProductWiseSales(ref string errorMessage, string dtFrom, string dtTo, int location, string itemCode)
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

        public static bool ValidateStockPosition(ref string errorMessage,string dtTo)
        {
            bool ret = true;
            StringBuilder sb = new StringBuilder();
            
            if ((dtTo != string.Empty && Convert.ToDateTime(dtTo) > DateTime.Now))
            {
                sb.Append(Common.GetMessage("VAL0060", "Date To", "Current Date"));
                sb.AppendLine();
                ret = false;
            }            
            else
                ret = true;
            if (ret == false)
                MessageBox.Show(Common.ReturnErrorMessage(sb).ToString(), Common.VALIDATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return ret;
        }
        
        public static bool ValidateBOWiseSales(ref string errorMessage, string dtFrom, string dtTo)
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





        public static bool ValidateStockSummary(ref string errorMessage, string dtFrom, string dtTo, int from, int bucket)
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



        public static bool ValidateFreeItemsSales(ref string errorMessage, int BoLocationId, string dtFrom, string dtTo, string ItemCode, int IsFree)
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



        public static bool ValidateGoodsInTransit(ref string errorMessage, string dtFrom, string dtTo, int from, int to)
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



        public static bool ValidateGoodsInTransit1(ref string errorMessage, int Month, int Year, int from, int to, string ItemCode)
        {
            bool ret = true;
            StringBuilder sb = new StringBuilder();
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
                ret = true;
            if (ret == false)
                MessageBox.Show(Common.ReturnErrorMessage(sb).ToString(), Common.VALIDATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return ret;
        }



        public static bool ValidatePOItemWise(ref string errorMessage, string dtFrom, string dtTo, string PONumber)
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


        public static bool ValidateVendorItemWise(ref string errorMessage, string dtFrom, string dtTo, string PONumber, int VendorName)
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

        public static bool ValidateSaleStock(ref string errorMessage, int LocationId)
        {
            return true;
            
        }

        #endregion

        #region Methods

        public static List<ReportDataSource> STNItemWise(ref string errorMessage, string dtFrom, string dtTo, int from, int to)
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
                dbParam.Add(new DBParameter("@from", from, DbType.Int32));
                dbParam.Add(new DBParameter("@to", to, DbType.Int32));
                dbParam.Add(new DBParameter("@ReportType",(int)(Common.InventoryReportType.TO),DbType.Int32));
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                // executing procedure to save the record 
                DataSet ds = dtManager.ExecuteDataSet(SP_RPT_STNITEMWISE, dbParam);

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
                                ds.Tables[0].Rows[i]["TransDateText"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["TransDate"]).ToString(Common.DTP_DATE_FORMAT);
                                ds.Tables[0].Rows[i]["TransQty"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["TransQty"]), Common.DisplayQtyRounding);
                                ds.Tables[0].Rows[i]["FromDateText"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["dtFrom"]).ToString(Common.DTP_DATE_FORMAT);
                                ds.Tables[0].Rows[i]["ToDateText"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["dtTo"]).ToString(Common.DTP_DATE_FORMAT);
                            }
                            lrds.Add(new ReportDataSource("STNItemWise_TOItemWiseDataTable", ds.Tables[0]));            
                        }                   
                }
            }
            catch (Exception ex)
            {
                Vinculum.Framework.Logging.LogManager.WriteExceptionLog(ex);
            }
            return lrds;
        }

        public static List<ReportDataSource> ReceivedItemWise(ref string errorMessage, string dtFrom, string dtTo, int from)
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
                dbParam.Add(new DBParameter("@from", from, DbType.Int32));
                dbParam.Add(new DBParameter("@to", Common.INT_DBNULL, DbType.Int32));
                dbParam.Add(new DBParameter("@ReportType", (int)(Common.InventoryReportType.Receiving), DbType.Int32));
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                // executing procedure to save the record 
                DataSet ds = dtManager.ExecuteDataSet(SP_RPT_STNITEMWISE, dbParam);

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
                            ds.Tables[0].Rows[i]["TransDateText"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["TransDate"]).ToString(Common.DTP_DATE_FORMAT);
                            ds.Tables[0].Rows[i]["TransQty"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["TransQty"]), Common.DisplayQtyRounding);
                            ds.Tables[0].Rows[i]["FromDateText"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["dtFrom"]).ToString(Common.DTP_DATE_FORMAT);
                            ds.Tables[0].Rows[i]["ToDateText"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["dtTo"]).ToString(Common.DTP_DATE_FORMAT);
                        }
                        lrds.Add(new ReportDataSource("STNItemWise_TOItemWiseDataTable", ds.Tables[0]));
                    }
                }
            }
            catch (Exception ex)
            {
                Vinculum.Framework.Logging.LogManager.WriteExceptionLog(ex);
            }
            return lrds;
        }

        public static List<ReportDataSource> ProductWiseSales(ref string errorMessage, string dtFrom, string dtTo, int location, string itemCode)
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
                dbParam.Add(new DBParameter("@location", location, DbType.Int32));
                dbParam.Add(new DBParameter("@ItemCode", itemCode, DbType.String));
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                // executing procedure to save the record 
                DataSet ds = dtManager.ExecuteDataSet(SP_RPT_PRODUCTWISESALES, dbParam);

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
                        ds.Tables[0].Rows[0]["FromDateText"] = Convert.ToDateTime(ds.Tables[0].Rows[0]["dtFrom"]).ToString(Common.DTP_DATE_FORMAT);
                        ds.Tables[0].Rows[0]["ToDateText"] = Convert.ToDateTime(ds.Tables[0].Rows[0]["dtTo"]).ToString(Common.DTP_DATE_FORMAT);
                        ds.Tables[0].Columns.Add(new DataColumn("InvoiceDateText", Type.GetType("System.String")));
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            ds.Tables[0].Rows[i]["InvoiceDateText"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["InvoiceDate"]).ToString(Common.DTP_DATE_FORMAT);
                            ds.Tables[0].Rows[i]["Quantity"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["Quantity"]), Common.DisplayQtyRounding);                            
                        }
                        lrds.Add(new ReportDataSource("ProductWiseSales_ProductWiseSalesDataTable", ds.Tables[0]));
                    }
                }
            }
            catch (Exception ex)
            {
                Vinculum.Framework.Logging.LogManager.WriteExceptionLog(ex);
            }
            return lrds;
        }

        public static List<ReportDataSource> StockSummary(ref string errorMessage, string dtFrom, string dtTo, int from, int bucket)
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
                dbParam.Add(new DBParameter("@location", from, DbType.Int32));
                dbParam.Add(new DBParameter("@bucketId", bucket, DbType.Int32));
                dbParam.Add(new DBParameter("@reportType",Common.StockSummaryReportType.AllStockSummary, DbType.Int32));
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                // executing procedure to save the record 
                DataSet ds = dtManager.ExecuteDataSet(SP_RPT_STOCKSUMMARY, dbParam);

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
                            ds.Tables[0].Rows[i]["BalanceQty"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["BalanceQty"]), Common.DisplayQtyRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["OpeningQty"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["OpeningQty"]), Common.DisplayQtyRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["TIQty"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["TIQty"]), Common.DisplayQtyRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["STNQty"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["STNQty"]), Common.DisplayQtyRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["PackHeaderQty"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["PackHeaderQty"]), Common.DisplayQtyRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["PackChildQty"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["PackChildQty"]), Common.DisplayQtyRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["UnPackHeaderQty"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["UnPackHeaderQty"]), Common.DisplayQtyRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["UnPackChildQty"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["UnPackChildQty"]), Common.DisplayQtyRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["GRNQty"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["GRNQty"]), Common.DisplayQtyRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["AdjQty"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["AdjQty"]), Common.DisplayQtyRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["SoldQty"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["SoldQty"]), Common.DisplayQtyRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["RTVQty"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["RTVQty"]), Common.DisplayQtyRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["CustomerReturnQty"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["CustomerReturnQty"]), Common.DisplayQtyRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["FromDateText"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["dtFrom"]).ToString(Common.DTP_DATE_FORMAT);
                            ds.Tables[0].Rows[i]["ToDateText"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["dtTo"]).ToString(Common.DTP_DATE_FORMAT);
                            ds.Tables[0].Rows[i]["ExportSub"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["ExportSub"]), Common.DisplayQtyRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["ExportAdd"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["ExportAdd"]), Common.DisplayQtyRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["TOEXP"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["TOEXP"]), Common.DisplayQtyRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["TIEXP"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["TIEXP"]), Common.DisplayQtyRounding, MidpointRounding.AwayFromZero);
                        }
                        lrds.Add(new ReportDataSource("StockSummary_StockSummaryDataTable", ds.Tables[0]));
                    }
                }
            }
            catch (Exception ex)
            {
                Vinculum.Framework.Logging.LogManager.WriteExceptionLog(ex);
            }
            return lrds;
        }
        
        public static List<ReportDataSource> BOStockPosition(ref string errorMessage,string dtTo)
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

                dbParam.Add(new DBParameter("@dtFrom", "", DbType.String));
                dbParam.Add(new DBParameter("@dtTo", dtTo, DbType.String));
                dbParam.Add(new DBParameter("@location", -1, DbType.Int32));
                dbParam.Add(new DBParameter("@bucketId", -1, DbType.Int32));
                dbParam.Add(new DBParameter("@reportType", Common.StockSummaryReportType.BOStockSummary, DbType.Int32));
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                // executing procedure to save the record 
                DataSet ds = dtManager.ExecuteDataSet(SP_RPT_STOCKSUMMARY, dbParam);

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
                            ds.Tables[0].Rows[i]["BalanceQty"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["BalanceQty"]), Common.DisplayQtyRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["FromDateText"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["dtFrom"]).ToString(Common.DTP_DATE_FORMAT);
                            ds.Tables[0].Rows[i]["ToDateText"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["dtTo"]).ToString(Common.DTP_DATE_FORMAT);
                        }
                        lrds.Add(new ReportDataSource("StockPosition_StockPositionDataTable", ds.Tables[0]));
                    }
                }
            }
            catch (Exception ex)
            {
                Vinculum.Framework.Logging.LogManager.WriteExceptionLog(ex);
            }
            return lrds;
        }

        

        public static List<ReportDataSource> WHStockPosition(ref string errorMessage, string dtTo)
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

                dbParam.Add(new DBParameter("@dtFrom", "", DbType.String));
                dbParam.Add(new DBParameter("@dtTo", dtTo, DbType.String));
                dbParam.Add(new DBParameter("@location", -1, DbType.Int32));
                dbParam.Add(new DBParameter("@bucketId", -1, DbType.Int32));
                dbParam.Add(new DBParameter("@reportType", Common.StockSummaryReportType.WHStockSummary, DbType.Int32));
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                // executing procedure to save the record 
                DataSet ds = dtManager.ExecuteDataSet(SP_RPT_STOCKSUMMARY, dbParam);

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
                            ds.Tables[0].Rows[i]["BalanceQty"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["BalanceQty"]), Common.DisplayQtyRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["FromDateText"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["dtFrom"]).ToString(Common.DTP_DATE_FORMAT);
                            ds.Tables[0].Rows[i]["ToDateText"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["dtTo"]).ToString(Common.DTP_DATE_FORMAT);
                        }
                        lrds.Add(new ReportDataSource("StockPosition_StockPositionDataTable", ds.Tables[0]));
                    }
                }
            }
            catch (Exception ex)
            {
                Vinculum.Framework.Logging.LogManager.WriteExceptionLog(ex);
            }
            return lrds;
        }

        public static List<ReportDataSource> BOWiseSalesReport(ref string errorMessage, string dtFrom, string dtTo)
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
                dbParam.Add(new DBParameter("@locationType", Common.LocationConfigId.BO, DbType.Int32));
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                // executing procedure to save the record 
                DataSet ds = dtManager.ExecuteDataSet(SP_RPT_BOWISESALES, dbParam);

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
                            ds.Tables[0].Rows[i]["NetQuantity"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["NetQuantity"]), Common.DisplayQtyRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["FromDateText"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["dtFrom"]).ToString(Common.DTP_DATE_FORMAT);
                            ds.Tables[0].Rows[i]["ToDateText"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["dtTo"]).ToString(Common.DTP_DATE_FORMAT);
                        }
                        lrds.Add(new ReportDataSource("BOWiseSales_BOWiseSalesDataTable", ds.Tables[0]));
                    }
                }
            }
            catch (Exception ex)
            {
                Vinculum.Framework.Logging.LogManager.WriteExceptionLog(ex);
            }
            return lrds;
        }



        public static List<ReportDataSource> FreeItemsSales(ref string errorMessage, int BolocationId, string dtFrom, string dtTo, string ItemCode, int IsFree)
        {
            List<ReportDataSource> lrds = new List<ReportDataSource>();
            try
            {
                DBParameterList dbParam;

                Vinculum.Framework.Data.DataTaskManager dtManager = new DataTaskManager();

                // initialize the parameter list object
                dbParam = new DBParameterList();

                // add the relevant parameters
                dbParam.Add(new DBParameter("@BoLocationId", BolocationId, DbType.Int32));
                dbParam.Add(new DBParameter("@dtFrom", dtFrom, DbType.String));
                dbParam.Add(new DBParameter("@dtTo", dtTo, DbType.String));
                dbParam.Add(new DBParameter("@itemcode",ItemCode, DbType.String));
                dbParam.Add(new DBParameter("@IsFree", IsFree, DbType.Int32));
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                // executing procedure to save the record 
                DataSet ds = dtManager.ExecuteDataSet(SP_RPT_FREEITEMSSALES, dbParam);

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

                            // ds.Tables[0].Rows[i]["dtFrom"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["dtFrom"]).ToString(Common.DTP_DATE_FORMAT);
                            //ds.Tables[0].Rows[i]["dtTo"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["dtTo"]).ToString(Common.DTP_DATE_FORMAT);
                        }
                        lrds.Add(new ReportDataSource("FreeItemsSales_FreeItemsSales", ds.Tables[0]));
                    }
                }
            }
            catch (Exception ex)
            {
                Vinculum.Framework.Logging.LogManager.WriteExceptionLog(ex);
            }
            return lrds;
        }


        //public static List<ReportDataSource> GoodsInTransit(ref string errorMessage, string dtFrom, string dtTo, int from, int to)
        //{
        //    List<ReportDataSource> lrds = new List<ReportDataSource>();
        //    try
        //    {
        //        DBParameterList dbParam;

        //        Vinculum.Framework.Data.DataTaskManager dtManager = new DataTaskManager();

        //        // initialize the parameter list object
        //        dbParam = new DBParameterList();

        //        // add the relevant 2 parameters
        //        //dbParam.Add(new DBParameter("@InvoiceType", invoiceType, DbType.String));

        //        dbParam.Add(new DBParameter("@dtFrom", dtFrom, DbType.String));
        //        dbParam.Add(new DBParameter("@dtTo", dtTo, DbType.String));
        //        dbParam.Add(new DBParameter("@from", from, DbType.Int32));
        //        dbParam.Add(new DBParameter("@to", to, DbType.Int32));
        //        dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

        //        // executing procedure to save the record 
        //        DataSet ds = dtManager.ExecuteDataSet(SP_RPT_STNGOODSINTRANSIT, dbParam);

        //        // update database message
        //        errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();

        //        // if an error returned from the database
        //        if (errorMessage != string.Empty)
        //            return null;
        //        else
        //        {
        //            if (ds.Tables[0].Rows.Count > 0)
        //            {
        //                string address = Common.ReportHeaderAddress();
        //                string headerText = address.Substring(0, address.IndexOf("*|$|*"));
        //                string addressText = address.Substring(address.IndexOf("*|$|*") + 5, address.Length - address.IndexOf("*|$|*") - 5);
        //                addressText = addressText.Replace("*|$|*", Environment.NewLine);
        //                ds.Tables[0].Columns.Add(new DataColumn("FromDateText", Type.GetType("System.String")));
        //                ds.Tables[0].Columns.Add(new DataColumn("ToDateText", Type.GetType("System.String")));
        //                ds.Tables[0].Columns.Add(new DataColumn("HeaderAddress", Type.GetType("System.String")));
        //                ds.Tables[0].Columns.Add(new DataColumn("AddressText", Type.GetType("System.String")));
        //                ds.Tables[0].Rows[0]["HeaderAddress"] = headerText;
        //                ds.Tables[0].Rows[0]["AddressText"] = addressText;
        //                ds.Tables[0].Columns.Add(new DataColumn("TransDateText", Type.GetType("System.String")));
        //                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //                {
        //                    //ds.Tables[0].Rows[i]["TransDateText"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["TransDate"]).ToString(Common.DTP_DATE_FORMAT);
        //                    //ds.Tables[0].Rows[i]["TransQty"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["TransQty"]), Common.DisplayQtyRounding);
        //                    //ds.Tables[0].Rows[i]["FromDateText"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["dtFrom"]).ToString(Common.DTP_DATE_FORMAT);
        //                    //ds.Tables[0].Rows[i]["ToDateText"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["dtTo"]).ToString(Common.DTP_DATE_FORMAT);
        //                }
        //                lrds.Add(new ReportDataSource("GoodsInTransit_GoodsInTransit", ds.Tables[0]));
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Vinculum.Framework.Logging.LogManager.WriteExceptionLog(ex);
        //    }
        //    return lrds;
        //}



        public static List<ReportDataSource> GoodsInTransit1(ref string errorMessage, int Month, int Year, int from, int to, string ItemCode)
        {
            if(string.IsNullOrEmpty(ItemCode))
            { ItemCode = "-1"; }

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
                dbParam.Add(new DBParameter("@From", from, DbType.Int32));
                dbParam.Add(new DBParameter("@To", to, DbType.Int32));
                dbParam.Add(new DBParameter("@ItemCode",ItemCode, DbType.String));
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                // executing procedure to save the record 
                DataSet ds = dtManager.ExecuteDataSet(SP_RPT_STNGOODSINTRANSIT1, dbParam);

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
                            ds.Tables[0].Rows[i]["TransDateText"] = "2011-08-30"; //Convert.ToDateTime(ds.Tables[0].Rows[i]["TransDate"]).ToString(Common.DTP_DATE_FORMAT);
                            //ds.Tables[0].Rows[i]["TransQty"]  ="20"; // Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["TransQty"]), Common.DisplayQtyRounding);
                            ds.Tables[0].Rows[i]["FromDateText"] = "2011-08-30"; //Convert.ToDateTime(ds.Tables[0].Rows[i]["dtFrom"]).ToString(Common.DTP_DATE_FORMAT);
                            ds.Tables[0].Rows[i]["ToDateText"] = "2011-08-30"; //Convert.ToDateTime(ds.Tables[0].Rows[i]["dtTo"]).ToString(Common.DTP_DATE_FORMAT);
                        }
                       
                    }
                    lrds.Add(new ReportDataSource("GoodsInTransit1_GoodsInTransit1", ds.Tables[1]));
                }
            }
            catch (Exception ex)
            {
                Vinculum.Framework.Logging.LogManager.WriteExceptionLog(ex);
            }
            return lrds;
        }




        public static List<ReportDataSource> POItemWise(ref string errorMessage, string dtFrom, string dtTo, string PONumber)
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
                dbParam.Add(new DBParameter("@PONumber", PONumber, DbType.String));
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                // executing procedure to save the record 
                DataSet ds = dtManager.ExecuteDataSet(SP_RPT_POITEMWISE, dbParam);

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
                            //ds.Tables[0].Rows[i]["TransDateText"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["TransDate"]).ToString(Common.DTP_DATE_FORMAT);
                            //ds.Tables[0].Rows[i]["TransQty"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["TransQty"]), Common.DisplayQtyRounding);
                            //ds.Tables[0].Rows[i]["FromDateText"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["dtFrom"]).ToString(Common.DTP_DATE_FORMAT);
                            //ds.Tables[0].Rows[i]["ToDateText"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["dtTo"]).ToString(Common.DTP_DATE_FORMAT);
                        }
                        lrds.Add(new ReportDataSource("POItemwise_POItemwise", ds.Tables[0]));
                    }
                }
            }
            catch (Exception ex)
            {
                Vinculum.Framework.Logging.LogManager.WriteExceptionLog(ex);
            }
            return lrds;
        }


        public static List<ReportDataSource> VendorItemWise(ref string errorMessage, string dtFrom, string dtTo, string PONumber, int VendorName)
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
                dbParam.Add(new DBParameter("@PONumber", PONumber, DbType.String));
                dbParam.Add(new DBParameter("@VendorName", VendorName, DbType.Int32));
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                // executing procedure to save the record 
                DataSet ds = dtManager.ExecuteDataSet(SP_RPT_VENDORITEMWISE, dbParam);

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
                            //ds.Tables[0].Rows[i]["TransDateText"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["TransDate"]).ToString(Common.DTP_DATE_FORMAT);
                            //ds.Tables[0].Rows[i]["TransQty"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["TransQty"]), Common.DisplayQtyRounding);
                            //ds.Tables[0].Rows[i]["FromDateText"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["dtFrom"]).ToString(Common.DTP_DATE_FORMAT);
                            //ds.Tables[0].Rows[i]["ToDateText"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["dtTo"]).ToString(Common.DTP_DATE_FORMAT);
                        }
                        lrds.Add(new ReportDataSource("VendorItemwise_VendorItemwise", ds.Tables[0]));
                    }
                }
            }
            catch (Exception ex)
            {
                Vinculum.Framework.Logging.LogManager.WriteExceptionLog(ex);
            }
            return lrds;
        }

        public static List<ReportDataSource> BoWiseSaleStock(ref string errorMessage, int BoLocationid)
        {
            List<ReportDataSource> lrds = new List<ReportDataSource>();
            try
            {
                DBParameterList dbParam;

                Vinculum.Framework.Data.DataTaskManager dtManager = new DataTaskManager();

                // initialize the parameter list object
                dbParam = new DBParameterList();

                // add the relevant parameters
                dbParam.Add(new DBParameter("@LocationId", BoLocationid, DbType.Int32));
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));
                                
                // executing procedure to save the record 
                DataSet ds = dtManager.ExecuteDataSet(SP_RPT_BOSWISESALESTOCK, dbParam);
               

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
                        //ds.Tables[0].Columns.Add(new DataColumn("FromDateText", Type.GetType("System.String")));
                        //ds.Tables[0].Columns.Add(new DataColumn("ToDateText", Type.GetType("System.String")));
                        ds.Tables[0].Columns.Add(new DataColumn("HeaderAddress", Type.GetType("System.String")));
                        ds.Tables[0].Columns.Add(new DataColumn("AddressText", Type.GetType("System.String")));
                        ds.Tables[0].Rows[0]["HeaderAddress"] = headerText;
                        ds.Tables[0].Rows[0]["AddressText"] = addressText;
                        //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        //{

                        //    // ds.Tables[0].Rows[i]["dtFrom"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["dtFrom"]).ToString(Common.DTP_DATE_FORMAT);
                        //    //ds.Tables[0].Rows[i]["dtTo"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["dtTo"]).ToString(Common.DTP_DATE_FORMAT);
                        //}
                        lrds.Add(new ReportDataSource("BOWiseStockInTransit_StockInTransit", ds.Tables[0]));
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
