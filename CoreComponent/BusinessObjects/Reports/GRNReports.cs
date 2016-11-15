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
    public class GRNReports
    {
        #region SP Declaration
        private const string SP_RPT_GRNREGISTERPRODUCT = "usp_RptGRNRegisterProduct";
      
        #endregion

        #region Validations
        public static bool ValidateGRNReport(ref string dbMessage,string itemCode, string pono, string grn, string dtFrom, string dtTo, int locationId)
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
                
        
        #endregion

        #region Methods
        public static List<ReportDataSource> GRNRegisterProductwiseSearch(ref string errorMessage, string itemCode, string pono, string grn, string dtFrom, string dtTo, int locationId)
        {
            List<ReportDataSource> lrds = new List<ReportDataSource>();
            try
            {
                DBParameterList dbParam;

                Vinculum.Framework.Data.DataTaskManager dtManager = new DataTaskManager();

                // initialize the parameter list object
                dbParam = new DBParameterList();

                // add the relevant 2 parameters
                dbParam.Add(new DBParameter("@itemCode",itemCode,DbType.String));
                dbParam.Add(new DBParameter("@pono", pono, DbType.String));
                dbParam.Add(new DBParameter("@grn", grn, DbType.String));
                dbParam.Add(new DBParameter("@fromDate", dtFrom, DbType.String));
                dbParam.Add(new DBParameter("@toDate", dtTo, DbType.String));
                dbParam.Add(new DBParameter("@locationId", locationId, DbType.Int32));
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                // executing procedure to save the record 
                DataSet ds = dtManager.ExecuteDataSet(SP_RPT_GRNREGISTERPRODUCT, dbParam);
                
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
                        ds.Tables[0].Columns.Add(new DataColumn("ChallanDateText", Type.GetType("System.String")));
                        ds.Tables[0].Columns.Add(new DataColumn("GRNDateText", Type.GetType("System.String")));
                        ds.Tables[0].Columns.Add(new DataColumn("FromDateText", Type.GetType("System.String")));
                        ds.Tables[0].Columns.Add(new DataColumn("ToDateText", Type.GetType("System.String")));
                        ds.Tables[0].Columns.Add(new DataColumn("HeaderAddress", Type.GetType("System.String")));
                        ds.Tables[0].Columns.Add(new DataColumn("AddressText", Type.GetType("System.String")));
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            ds.Tables[0].Rows[i]["ChallanDateText"] = (Convert.ToDateTime(ds.Tables[0].Rows[i]["ChallanDate"])).ToString(Common.DTP_DATE_FORMAT);
                            ds.Tables[0].Rows[i]["GRNDateText"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["GRNDate"]).ToString(Common.DTP_DATE_FORMAT);
                            ds.Tables[0].Rows[i]["FromDateText"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["FromDate"]).ToString(Common.DTP_DATE_FORMAT);
                            ds.Tables[0].Rows[i]["ToDateText"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["ToDate"]).ToString(Common.DTP_DATE_FORMAT);
                            ds.Tables[0].Rows[i]["ChallanQty"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["ChallanQty"]), Common.DisplayQtyRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["InvoiceQty"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["InvoiceQty"]), Common.DisplayQtyRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["ReceivedQty"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["ReceivedQty"]), Common.DisplayQtyRounding, MidpointRounding.AwayFromZero);
                            ds.Tables[0].Rows[i]["HeaderAddress"] = headerText;
                            ds.Tables[0].Rows[i]["AddressText"] = addressText;
                        }
                        lrds.Add(new ReportDataSource("GRNRegisterProductwise_GRNRegisterProductwiseDataTable", ds.Tables[0]));
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
