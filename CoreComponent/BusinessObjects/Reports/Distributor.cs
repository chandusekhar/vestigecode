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
    public class Distributor
    {
        #region SP Declaration

        private const string SP_RPTDISTRIBUTORBUSINESS = "usp_RptDistributorBusiness";
        private const string SP_RPTDISTRIBUTORBUSINESSCHEQUE = "usp_RptDistributorBusinessCheque";
        private const string SP_RPTDISTRIBUTORGIFTVOUCHER = "usp_RptDistributorGiftVoucher";
        private const string SP_RPTDISTRIBUTORADDRESS = "usp_Rptdistributoraddress";
        private const string SP_RPTDISTRIBUTORADDRESS_LABEL = "usp_RptDistributorAddressLabelReport";
        private const string SP_RPT_Pickupcentre = "usp_RptPickupCentre";
        private const string SP_RPT_DirectorGroupBonusCheque = "usp_RptDirectorGroupBonusCheque";
        private const string SP_RPT_DistributorHistory = "usp_rptDistributorPanBankHistory";
        private const string SP_RPT_DistributorPanBankMonthlyPayout = "Usp_RptDistListInfoPanBankMonthlyPayout";
        private const string SP_RPT_DistributorPanBanAcceptReject = "USP_RptDistributorAcceptReject";
        private const string SP_RPT_LocWiseDistributorModifiedList = "usp_rptGetModifiedDistributor";
        #endregion

        #region Variable Declaration

        private int m_distributorid;

        public int Distributorid
        {
            get { return m_distributorid; }
            set { m_distributorid = value; }
        }

        private DateTime m_fromdate;

        public DateTime Fromdate
        {
            get { return m_fromdate; }
            set { m_fromdate = value; }
        }

        private DateTime m_todate;

        public DateTime Todate
        {
            get { return m_todate; }
            set { m_todate = value; }
        }

        #endregion

        #region Validations

        public static bool ValidateDistributorAddress(ref String errorMessage, string distributorId, string fromDate, string toDate, int ReportType)
        {
            bool ret = true;
            StringBuilder sb = new StringBuilder();
            if (distributorId != string.Empty && (!Validators.IsNumeric(distributorId) || !Validators.IsInt32(distributorId)))
            {
                sb.Append(Common.GetMessage("VAL0071", "distributorId"));
                sb.AppendLine();
                ret = false;
            }
            if ((fromDate != string.Empty && Convert.ToDateTime(fromDate) > DateTime.Now))
            {
                sb.Append(Common.GetMessage("VAL0060", "Date From", "Current Date"));
                sb.AppendLine();
                ret = false;
            }
            if ((toDate != string.Empty && Convert.ToDateTime(toDate) > DateTime.Now))
            {
                sb.Append(Common.GetMessage("VAL0060", "Date To", "Current Date"));
                sb.AppendLine();
                ret = false;
            }
            if (fromDate != string.Empty && toDate != string.Empty && Convert.ToDateTime(fromDate) > Convert.ToDateTime(toDate))
            {
                sb.Append(Common.GetMessage("VAL0060", "Date From", "Date To"));
                sb.AppendLine();
                ret = false;
            }
            if (ReportType == -1)
            {
                sb.Append(Common.GetMessage("VAL0002", "Report Type"));
                sb.AppendLine();
                ret = false;
            }
            if (ret != true)
                MessageBox.Show(Common.ReturnErrorMessage(sb).ToString(), Common.VALIDATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return ret;           
        }

        public static bool ValidateDistributorBusiness(ref String errorMessage, string distributorId, string fromDate, string toDate, int period, int ReportType)
        {
            bool ret = true;
            StringBuilder sb = new StringBuilder();
            if (distributorId != string.Empty && (!Validators.IsNumeric(distributorId) || !Validators.IsInt32(distributorId)))
            {
                sb.Append(Common.GetMessage("VAL0071", "distributorId"));
                sb.AppendLine();
                ret = false;
            }
            if ((fromDate != string.Empty && Convert.ToDateTime(fromDate) > DateTime.Now))
            {
                sb.Append(Common.GetMessage("VAL0060", "Date From", "Current Date"));
                sb.AppendLine();
                ret = false;
            }
            if ((toDate != string.Empty && Convert.ToDateTime(toDate) > DateTime.Now))
            {
                sb.Append(Common.GetMessage("VAL0060", "Date To", "Current Date"));
                sb.AppendLine();
                ret = false;
            }
            if (fromDate != string.Empty && toDate != string.Empty && Convert.ToDateTime(fromDate) > Convert.ToDateTime(toDate))
            {
                sb.Append(Common.GetMessage("VAL0060", "Date From", "Date To"));
                sb.AppendLine();
                ret = false;
            }
            if (period == -1)
            {
                sb.Append(Common.GetMessage("VAL0002", "Period"));
                sb.AppendLine();
                ret = false;
            }
            if (ReportType == -1)
            {
                sb.Append(Common.GetMessage("VAL0002", "Report Type"));
                sb.AppendLine();
                ret = false;
            }
            if (ret != true)
            {
                sb = Common.ReturnErrorMessage(sb);
                MessageBox.Show(sb.ToString(), Common.VALIDATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return ret;
        }

        public static bool ValidateDistributorHistory(ref String errorMessage, string distributorId, string fromDate, string toDate)
        {
            bool ret = true;
            StringBuilder sb = new StringBuilder();
            if (distributorId != string.Empty && (!Validators.IsNumeric(distributorId) || !Validators.IsInt32(distributorId)))
            {
                sb.Append(Common.GetMessage("VAL0071", "distributorId"));
                sb.AppendLine();
                ret = false;
            }
            if ((fromDate != string.Empty && Convert.ToDateTime(fromDate) > DateTime.Now))
            {
                sb.Append(Common.GetMessage("VAL0060", "Date From", "Current Date"));
                sb.AppendLine();
                ret = false;
            }
            if ((toDate != string.Empty && Convert.ToDateTime(toDate) > DateTime.Now))
            {
                sb.Append(Common.GetMessage("VAL0060", "Date To", "Current Date"));
                sb.AppendLine();
                ret = false;
            }
            if (fromDate != string.Empty && toDate != string.Empty && Convert.ToDateTime(fromDate) > Convert.ToDateTime(toDate))
            {
                sb.Append(Common.GetMessage("VAL0060", "Date From", "Date To"));
                sb.AppendLine();
                ret = false;
            }
         /*   if (ReportType == -1)
            {
                sb.Append(Common.GetMessage("VAL0002", "Report Type"));
                sb.AppendLine();
                ret = false;
            }*/
            if (ret != true)
                MessageBox.Show(Common.ReturnErrorMessage(sb).ToString(), Common.VALIDATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            
            return ret;           
        }

        public static bool ValidateDistributorBusinessCheque(ref String errorMessage, string distributorId,string chequeno,int status, string fromDate, string toDate)
        {
            bool ret = true;
            StringBuilder sb = new StringBuilder();
            if (distributorId != string.Empty && (!Validators.IsNumeric(distributorId) || !Validators.IsInt32(distributorId)))
            {
                sb.Append(Common.GetMessage("VAL0071", "distributorId"));
                sb.AppendLine();
                ret = false;
            }
            if (chequeno != string.Empty && (!Validators.IsNumeric(chequeno) || !Validators.IsAlphaNumeric(chequeno)))
            {
                sb.Append(Common.GetMessage("VAL0071", "chequeno"));
                sb.AppendLine();
                ret = false;
            }

            if (status==10)
            {
                sb.Append(Common.GetMessage("VAL0060", "status"));
                sb.AppendLine();
                ret = false;
            }
            if ((fromDate != string.Empty && Convert.ToDateTime(fromDate) > DateTime.Now))
            {
                sb.Append(Common.GetMessage("VAL0060", "Date From", "Current Date"));
                sb.AppendLine();
                ret = false;
            }
            if ((toDate != string.Empty && Convert.ToDateTime(toDate)  > DateTime.Now))
            {
                sb.Append(Common.GetMessage("VAL0060", "Date To", "Current Date"));
                sb.AppendLine();
                ret = false;
            }
            if (fromDate == string.Empty && toDate == string.Empty )
            {
                sb.Append(Common.GetMessage("VAL0613", "Date From", "Date To"));
                sb.AppendLine();
                ret = false;
            }
           
            if (ret != true)
            {
                sb = Common.ReturnErrorMessage(sb);
                MessageBox.Show(sb.ToString(), Common.VALIDATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return ret;
        }

        public static bool ValidateDistributorGiftVoucher(ref String errorMessage, string voucherno, string distributorId, string itemcode, string fromDate, string toDate)
        {
            bool ret = true;
            StringBuilder sb = new StringBuilder();
            if (distributorId != string.Empty && (!Validators.IsNumeric(distributorId) || !Validators.IsInt32(distributorId)))
            {
                sb.Append(Common.GetMessage("VAL0071", "distributorId"));
                sb.AppendLine();
                ret = false;
            }
            if (voucherno != string.Empty && (!Validators.IsNumeric(voucherno) || !Validators.IsAlphaNumeric(voucherno)))
            {
                sb.Append(Common.GetMessage("VAL0071", "chequeno"));
                sb.AppendLine();
                ret = false;
            }

            if (itemcode != string.Empty && (!Validators.IsNumeric(itemcode) || !Validators.IsAlphaNumeric(itemcode)))
            {
                sb.Append(Common.GetMessage("VAL0071", "chequeno"));
                sb.AppendLine();
                ret = false;
            }
            if ((fromDate != string.Empty && Convert.ToDateTime(fromDate) > DateTime.Now))
            {
                sb.Append(Common.GetMessage("VAL0060", "Date From", "Current Date"));
                sb.AppendLine();
                ret = false;
            }
            if ((toDate != string.Empty && Convert.ToDateTime(toDate) > DateTime.Now))
            {
                sb.Append(Common.GetMessage("VAL0060", "Date To", "Current Date"));
                sb.AppendLine();
                ret = false;
            }
            if (fromDate == string.Empty && toDate == string.Empty)
            {
                sb.Append(Common.GetMessage("VAL0613", "Date From", "Date To"));
                sb.AppendLine();
                ret = false;
            }

            if (ret != true)
            {
                sb = Common.ReturnErrorMessage(sb);
                MessageBox.Show(sb.ToString(), Common.VALIDATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return ret;
        }
        public static bool ValidateDistributorAddressLabel(ref String errorMessage, string fromDistributorId, string toDistributorId)
        {
            bool ret = true;
            
            StringBuilder sb = new StringBuilder();
            if (fromDistributorId != string.Empty && (!Validators.IsNumeric(fromDistributorId) || !Validators.IsInt32(fromDistributorId)))
            {
                sb.Append(Common.GetMessage("VAL0071", "from Distributor ID"));
                sb.AppendLine();
                ret = false;
            }
            if (toDistributorId != string.Empty && (!Validators.IsNumeric(toDistributorId) || !Validators.IsInt32(toDistributorId)))
            {
                sb.Append(Common.GetMessage("VAL0071", "to Distributor ID"));
                sb.AppendLine();
                ret = false;
            }
            if (ret != true)
            {
                sb = Common.ReturnErrorMessage(sb);
                MessageBox.Show(sb.ToString(), Common.VALIDATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
           
            return ret;
        }

        public static bool ValidateDistributorSingleAddressLabel(ref String errorMessage, string fromDistributorId)
        {
            bool ret = true;
            StringBuilder sb = new StringBuilder();
            if ((string.IsNullOrEmpty(fromDistributorId)) ||(fromDistributorId != string.Empty && (!Validators.IsNumeric(fromDistributorId) || !Validators.IsInt32(fromDistributorId))))
            {
                sb.Append(Common.GetMessage("VAL0071", "Distributor ID"));
                sb.AppendLine();
                ret = false;
            }            
            if (ret != true)
            {
                sb = Common.ReturnErrorMessage(sb);
                MessageBox.Show(sb.ToString(), Common.VALIDATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return ret;
        }


        public static bool ValidatePickupCentre(ref string dbMessage, int BOlocationId, int PClocationId )
        {
            bool ret = true;
            StringBuilder sb = new StringBuilder();
                ret = true;
            if (ret == false)
                MessageBox.Show(Common.ReturnErrorMessage(sb).ToString(), Common.VALIDATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return ret;
        }



        public static bool ValidateDirectorGroupBonusCheque(ref String errorMessage, string distributorId, string fromDate, string toDate, int period, int ReportType, int chequenumber)
        {
            bool ret = true;
            StringBuilder sb = new StringBuilder();
            if (distributorId != string.Empty && (!Validators.IsNumeric(distributorId) || !Validators.IsInt32(distributorId)))
            {
                sb.Append(Common.GetMessage("VAL0071", "distributorId"));
                sb.AppendLine();
                ret = false;
            }
            if ((fromDate != string.Empty && Convert.ToDateTime(fromDate) > DateTime.Now))
            {
                sb.Append(Common.GetMessage("VAL0060", "Date From", "Current Date"));
                sb.AppendLine();
                ret = false;
            }
            if ((toDate != string.Empty && Convert.ToDateTime(toDate) > DateTime.Now))
            {
                sb.Append(Common.GetMessage("VAL0060", "Date To", "Current Date"));
                sb.AppendLine();
                ret = false;
            }
            if (fromDate != string.Empty && toDate != string.Empty && Convert.ToDateTime(fromDate) > Convert.ToDateTime(toDate))
            {
                sb.Append(Common.GetMessage("VAL0060", "Date From", "Date To"));
                sb.AppendLine();
                ret = false;
            }
            if (period == -1)
            {
                sb.Append(Common.GetMessage("VAL0002", "Period"));
                sb.AppendLine();
                ret = false;
            }
            if (ReportType == -1)
            {
                sb.Append(Common.GetMessage("VAL0002", "Report Type"));
                sb.AppendLine();
                ret = false;
            }
            if (ret != true)
            {
                sb = Common.ReturnErrorMessage(sb);
                MessageBox.Show(sb.ToString(), Common.VALIDATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return ret;
        }

        public static bool ValidateDistributorPanBankInforForMonthlyPayout(ref String errorMessage, string fromDistributorId,Int32 month,  Int32 year)
        {
            bool ret = true;

            StringBuilder sb = new StringBuilder();
            if (fromDistributorId != string.Empty && (!Validators.IsNumeric(fromDistributorId) || !Validators.IsInt32(fromDistributorId)))
            {
                sb.Append(Common.GetMessage("VAL0071", "Distributor ID"));
                sb.AppendLine();
                ret = false;
            }
            
            if (year > DateTime.Today.Year)
            {
                sb.Append(Common.GetMessage("VAL0060", "Year", "Current Year"));
                sb.AppendLine();
                ret = false;
            }
            
            /*if (toDistributorId != string.Empty && (!Validators.IsNumeric(toDistributorId) || !Validators.IsInt32(toDistributorId)))
            {
                sb.Append(Common.GetMessage("VAL0071", "to Distributor ID"));
                sb.AppendLine();
                ret = false;
            }*/


            if (ret != true)
            {
                sb = Common.ReturnErrorMessage(sb);
                MessageBox.Show(sb.ToString(), Common.VALIDATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return ret;
        }

        public static bool ValidateDistributorAcceptReject(ref String errorMessage, string distributorId, string fromDate, string toDate, int ReportType)
        {
            bool ret = true;
            StringBuilder sb = new StringBuilder();
            if (distributorId != string.Empty && (!Validators.IsNumeric(distributorId) || !Validators.IsInt32(distributorId)))
            {
                sb.Append(Common.GetMessage("VAL0071", "distributorId"));
                sb.AppendLine();
                ret = false;
            }
            if ((fromDate != string.Empty && Convert.ToDateTime(fromDate) > DateTime.Now))
            {
                sb.Append(Common.GetMessage("VAL0060", "Date From", "Current Date"));
                sb.AppendLine();
                ret = false;
            }
            if ((toDate != string.Empty && Convert.ToDateTime(toDate) > DateTime.Now))
            {
                sb.Append(Common.GetMessage("VAL0060", "Date To", "Current Date"));
                sb.AppendLine();
                ret = false;
            }
            if (fromDate != string.Empty && toDate != string.Empty && Convert.ToDateTime(fromDate) > Convert.ToDateTime(toDate))
            {
                sb.Append(Common.GetMessage("VAL0060", "Date From", "Date To"));
                sb.AppendLine();
                ret = false;
            }
            if (ReportType == -1)
            {
                sb.Append(Common.GetMessage("VAL0002", "Report Type"));
                sb.AppendLine();
                ret = false;
            }
            if (ret != true)
                MessageBox.Show(Common.ReturnErrorMessage(sb).ToString(), Common.VALIDATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return ret;
        }


        public static bool ValidateLocationwiseModifiedDist(ref String errorMessage, Int32  locationId, string fromDate, string toDate)
        {
            bool ret = true;
            StringBuilder sb = new StringBuilder();
            
            if ((fromDate != string.Empty && Convert.ToDateTime(fromDate) > DateTime.Now))
            {
                sb.Append(Common.GetMessage("VAL0060", "Date From", "Current Date"));
                sb.AppendLine();
                ret = false;
            }
            if ((toDate != string.Empty && Convert.ToDateTime(toDate) > DateTime.Now))
            {
                sb.Append(Common.GetMessage("VAL0060", "Date To", "Current Date"));
                sb.AppendLine();
                ret = false;
            }
            if (fromDate != string.Empty && toDate != string.Empty && Convert.ToDateTime(fromDate) > Convert.ToDateTime(toDate))
            {
                sb.Append(Common.GetMessage("VAL0060", "Date From", "Date To"));
                sb.AppendLine();
                ret = false;
            }
            /*   if (ReportType == -1)
               {
                   sb.Append(Common.GetMessage("VAL0002", "Report Type"));
                   sb.AppendLine();
                   ret = false;
               }*/
            if (ret != true)
                MessageBox.Show(Common.ReturnErrorMessage(sb).ToString(), Common.VALIDATION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Warning);

            return ret;
        }



        #endregion 

        #region Methods
                        
        public static List<ReportDataSource> SearchDistributorAddress( ref String errorMessage, string distributorId, string fromDate, string toDate, int ReportType)
        {
            List<ReportDataSource> listOfReportDataSource = new List<ReportDataSource>();
            DBParameterList dbParam;
            try
            {
                {
                    DataTaskManager dtManager = new DataTaskManager();

                    // initialize the parameter list object
                    dbParam = new DBParameterList();

                    // add the relevant parameters
                    dbParam.Add(new DBParameter("distributorId", distributorId, DbType.String));
                    dbParam.Add(new DBParameter("fromDate", fromDate, DbType.String));
                    dbParam.Add(new DBParameter("toDate", toDate, DbType.String));
                    dbParam.Add(new DBParameter("Type", ReportType, DbType.Int32));
                    dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, errorMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                    // executing procedure to fetch the record
                    DataTable dt = dtManager.ExecuteDataTable(SP_RPTDISTRIBUTORADDRESS, dbParam);

                    // update database message
                    errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();
                    
                    // if an error returned from the database
                    if (errorMessage != string.Empty)
                        return null;
                    else
                    {
                        if (dt.Rows.Count > 0)
                        {
                            string address = Common.ReportHeaderAddress();
                            string headerText = address.Substring(0, address.IndexOf("*|$|*"));
                            string addressText = address.Substring(address.IndexOf("*|$|*") + 5, address.Length - address.IndexOf("*|$|*") - 5);
                            addressText = addressText.Replace("*|$|*", Environment.NewLine);
                            dt.Columns.Add(new DataColumn("HeaderAddress", Type.GetType("System.String")));
                            dt.Columns.Add(new DataColumn("AddressText", Type.GetType("System.String")));
                            dt.Rows[0]["HeaderAddress"] = headerText;
                            dt.Rows[0]["AddressText"] = addressText;
                            listOfReportDataSource.Add(new ReportDataSource("DistrubutorAddressRptDataSet_DistributorAddressRptTable", dt));
                        }
                    }
                }
                return listOfReportDataSource;
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                throw ex;
            }
        }

        public static List<ReportDataSource> SearchDistributorHistory(ref String errorMessage, string distributorId, string fromDate, string toDate)
        {
            List<ReportDataSource> listOfReportDataSource = new List<ReportDataSource>();
            DBParameterList dbParam;
            try
            {
                {
                    DataTaskManager dtManager = new DataTaskManager();

                    // initialize the parameter list object
                    dbParam = new DBParameterList();

                    // add the relevant parameters
                    dbParam.Add(new DBParameter("distributorId", distributorId, DbType.String));
                    dbParam.Add(new DBParameter("fromDate", fromDate, DbType.String));
                    dbParam.Add(new DBParameter("toDate", toDate, DbType.String));
                    //dbParam.Add(new DBParameter("Type", ReportType, DbType.Int32));
                    dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, errorMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                    // executing procedure to fetch the record
                    DataTable dt = dtManager.ExecuteDataTable(SP_RPT_DistributorHistory, dbParam);

                    // update database message
                    errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();

                    // if an error returned from the database
                    if (errorMessage != string.Empty)
                        return null;
                    else
                    {
                        if (dt.Rows.Count > 0)
                        {
                            string address = Common.ReportHeaderAddress();
                            string headerText = address.Substring(0, address.IndexOf("*|$|*"));
                            string addressText = address.Substring(address.IndexOf("*|$|*") + 5, address.Length - address.IndexOf("*|$|*") - 5);
                            addressText = addressText.Replace("*|$|*", Environment.NewLine);
                            dt.Columns.Add(new DataColumn("HeaderAddress", Type.GetType("System.String")));
                            dt.Columns.Add(new DataColumn("AddressText", Type.GetType("System.String")));
                            dt.Rows[0]["HeaderAddress"] = headerText;
                            dt.Rows[0]["AddressText"] = addressText;
                            listOfReportDataSource.Add(new ReportDataSource("DistributorEditHistory_rptDistributorPanBankHistory", dt));
                        }
                    }
                }
                return listOfReportDataSource;
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                throw ex;
            }
        }

        public static List<ReportDataSource> SearchDistributorPanBankInfoForMonthlyPayout(ref String errorMessage, string fromDistributorId,Int32 month,Int32 year)
        {
            List<ReportDataSource> listOfReportDataSource = new List<ReportDataSource>();
            DBParameterList dbParam;
            try
            {
                {
                    DataTaskManager dtManager = new DataTaskManager();

                    // initialize the parameter list object
                    dbParam = new DBParameterList();

                    // add the relevant parameters
                    dbParam.Add(new DBParameter("@inputParam1", fromDistributorId, DbType.String));
                    dbParam.Add(new DBParameter("@inputParam2", month, DbType.Int32));
                    dbParam.Add(new DBParameter("@inputParam3", year, DbType.Int32));
                    dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, errorMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                    // executing procedure to fetch the record
                    DataTable dt = dtManager.ExecuteDataTable(SP_RPT_DistributorPanBankMonthlyPayout, dbParam);

                    // update database message
                    errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();

                    // if an error returned from the database
                    if (errorMessage != string.Empty)
                        return null;
                    else
                    {
                        if (dt.Rows.Count > 0)
                        {
                            string address = Common.ReportHeaderAddress();
                            string headerText = address.Substring(0, address.IndexOf("*|$|*"));
                            string addressText = address.Substring(address.IndexOf("*|$|*") + 5, address.Length - address.IndexOf("*|$|*") - 5);
                            addressText = addressText.Replace("*|$|*", Environment.NewLine);
                            dt.Columns.Add(new DataColumn("HeaderAddress", Type.GetType("System.String")));
                            dt.Columns.Add(new DataColumn("AddressText", Type.GetType("System.String")));
                            dt.Rows[0]["HeaderAddress"] = headerText;
                            dt.Rows[0]["AddressText"] = addressText;
                            listOfReportDataSource.Add(new ReportDataSource("DistPANBankInfoForMonthlyPayout_RptDistListInfoPanBankMonthlyPayout", dt));
                        }
                    }
                }
                return listOfReportDataSource;
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                throw ex;
            }
        }        
        public static List<ReportDataSource> DistributorBusinessReport(ref String errorMessage, string distributorId, string fromDate, string toDate, int period, int ReportType)
        {
            List<ReportDataSource> listOfReportDataSource = new List<ReportDataSource>();
            DBParameterList dbParam;
            try
            {
                {
                    DataTaskManager dtManager = new DataTaskManager();

                    // initialize the parameter list object
                    dbParam = new DBParameterList();

                    // add the relevant parameters
                    dbParam.Add(new DBParameter("distributorId", distributorId, DbType.String));
                    dbParam.Add(new DBParameter("fromDate", fromDate, DbType.String));
                    dbParam.Add(new DBParameter("toDate", toDate, DbType.String));
                    dbParam.Add(new DBParameter("period", period, DbType.Int32));
                    dbParam.Add(new DBParameter("Type", ReportType, DbType.Int32));
                    dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, errorMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                    // executing procedure to fetch the record
                    DataTable dt = dtManager.ExecuteDataTable(SP_RPTDISTRIBUTORBUSINESS, dbParam);

                    // update database message
                    errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();

                    // if an error returned from the database
                    if (errorMessage != string.Empty)
                        return null;
                    else
                    {
                        if (dt.Rows.Count > 0)
                        {
                            string address = Common.ReportHeaderAddress();
                            string headerText = address.Substring(0, address.IndexOf("*|$|*"));
                            string addressText = address.Substring(address.IndexOf("*|$|*") + 5, address.Length - address.IndexOf("*|$|*") - 5);
                            addressText = addressText.Replace("*|$|*", Environment.NewLine);
                            dt.Columns.Add(new DataColumn("HeaderAddress", Type.GetType("System.String")));
                            dt.Columns.Add(new DataColumn("AddressText", Type.GetType("System.String")));
                            dt.Rows[0]["HeaderAddress"] = headerText;
                            dt.Rows[0]["AddressText"] = addressText;
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                dt.Rows[i]["PrevCumPV"] = Math.Round(Convert.ToDecimal(dt.Rows[i]["PrevCumPV"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                                dt.Rows[i]["SelfPV"] = Math.Round(Convert.ToDecimal(dt.Rows[i]["SelfPV"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                                dt.Rows[i]["GroupPV"] = Math.Round(Convert.ToDecimal(dt.Rows[i]["GroupPV"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                                dt.Rows[i]["TotalPV"] = Math.Round(Convert.ToDecimal(dt.Rows[i]["TotalPV"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                                dt.Rows[i]["TotalCumPV"] = Math.Round(Convert.ToDecimal(dt.Rows[i]["TotalCumPV"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                                dt.Rows[i]["Level"] = Math.Round(Convert.ToDecimal(dt.Rows[i]["Level"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                               
                            }
                            listOfReportDataSource.Add(new ReportDataSource("DistributorBusiness_DistributorBusinessDataTable", dt));
                        }
                    }
                }
                return listOfReportDataSource;
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                throw ex;
            }
        }


        public static List<ReportDataSource> DistributorBusinessChequeReport(ref String errorMessage, string distributorId, string chequeno, int status, string fromDate, string toDate)
        {
            List<ReportDataSource> listOfReportDataSource = new List<ReportDataSource>();
            DBParameterList dbParam;
            try
            {
                {
                    DataTaskManager dtManager = new DataTaskManager();

                    // initialize the parameter list object
                    dbParam = new DBParameterList();

                    // add the relevant parameters
                    dbParam.Add(new DBParameter("distributorId", distributorId, DbType.String));
                    dbParam.Add(new DBParameter("chequeno", chequeno, DbType.String));
                    dbParam.Add(new DBParameter("status", status, DbType.Int32));
                    dbParam.Add(new DBParameter("fromDate", fromDate, DbType.String));
                    dbParam.Add(new DBParameter("toDate", toDate, DbType.String));
                    
                    dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, errorMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                    // executing procedure to fetch the record
                    DataTable dt = dtManager.ExecuteDataTable(SP_RPTDISTRIBUTORBUSINESSCHEQUE, dbParam);

                    // update database message
                    errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();

                    // if an error returned from the database
                    if (errorMessage != string.Empty)
                        return null;
                    else
                    {
                        if (dt.Rows.Count > 0)
                        {
                            string address = Common.ReportHeaderAddress();
                            string headerText = address.Substring(0, address.IndexOf("*|$|*"));
                            string addressText = address.Substring(address.IndexOf("*|$|*") + 5, address.Length - address.IndexOf("*|$|*") - 5);
                            addressText = addressText.Replace("*|$|*", Environment.NewLine);
                            dt.Columns.Add(new DataColumn("HeaderAddress", Type.GetType("System.String")));
                            dt.Columns.Add(new DataColumn("AddressText", Type.GetType("System.String")));
                            dt.Rows[0]["HeaderAddress"] = headerText;
                            dt.Rows[0]["AddressText"] = addressText;
                            
                            listOfReportDataSource.Add(new ReportDataSource("DistributorBusinessCheque_DistributorBusinessChequeDataTable", dt));
                        }
                    }
                }
                return listOfReportDataSource;
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                throw ex;
            }
        }

        public static List<ReportDataSource> DistributorGiftVoucher(ref String errorMessage, string voucherno, string distributorId, string itemcode, string fromDate, string toDate)
        {
            List<ReportDataSource> listOfReportDataSource = new List<ReportDataSource>();
            DBParameterList dbParam;
            try
            {
                {
                    DataTaskManager dtManager = new DataTaskManager();

                    // initialize the parameter list object
                    dbParam = new DBParameterList();

                    // add the relevant parameters
                    dbParam.Add(new DBParameter("voucherno", voucherno, DbType.String));
                    dbParam.Add(new DBParameter("distributorId", distributorId, DbType.String));
                    dbParam.Add(new DBParameter("itemcode", itemcode, DbType.String));
                    dbParam.Add(new DBParameter("fromDate", fromDate, DbType.String));
                    dbParam.Add(new DBParameter("toDate", toDate, DbType.String));

                    dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, errorMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                    // executing procedure to fetch the record
                    DataTable dt = dtManager.ExecuteDataTable(SP_RPTDISTRIBUTORGIFTVOUCHER, dbParam);

                    // update database message
                    errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();

                    // if an error returned from the database
                    if (errorMessage != string.Empty)
                        return null;
                    else
                    {
                        if (dt.Rows.Count > 0)
                        {
                            string address = Common.ReportHeaderAddress();
                            string headerText = address.Substring(0, address.IndexOf("*|$|*"));
                            string addressText = address.Substring(address.IndexOf("*|$|*") + 5, address.Length - address.IndexOf("*|$|*") - 5);
                            addressText = addressText.Replace("*|$|*", Environment.NewLine);
                            dt.Columns.Add(new DataColumn("HeaderAddress", Type.GetType("System.String")));
                            dt.Columns.Add(new DataColumn("AddressText", Type.GetType("System.String")));
                            dt.Rows[0]["HeaderAddress"] = headerText;
                            dt.Rows[0]["AddressText"] = addressText;

                            listOfReportDataSource.Add(new ReportDataSource("DistributorGiftVoucher_DistributorGiftVoucherDataTable", dt));
                        }
                    }
                }
                return listOfReportDataSource;
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                throw ex;
            }
        }
        public static List<ReportDataSource> PrintDistributorAddressLabel(ref String errorMessage, string fromDistributorId, string toDistributorId)
        {
            List<ReportDataSource> listOfReportDataSource = new List<ReportDataSource>();
            DBParameterList dbParam;
            try
            {
                {
                    DataTaskManager dtManager = new DataTaskManager();

                    // initialize the parameter list object
                    dbParam = new DBParameterList();

                    // add the relevant parameters
                    dbParam.Add(new DBParameter("fromDistributorId", fromDistributorId == "" ? "-1" : fromDistributorId, DbType.String));
                    dbParam.Add(new DBParameter("toDistributorId", toDistributorId == "" ? "-1" : toDistributorId, DbType.String));
                    dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, errorMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                    // executing procedure to fetch the record
                    DataTable dt = dtManager.ExecuteDataTable(SP_RPTDISTRIBUTORADDRESS_LABEL, dbParam);

                    // update database message
                    errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();

                    // if an error returned from the database
                    if (errorMessage != string.Empty)
                        return null;
                    else
                    {
                        if (dt.Rows.Count > 0)
                        {
                            DataTable dtTemp1 = new DataTable();
                            dtTemp1.Columns.Add(new DataColumn("Address", System.Type.GetType("System.String")));
                            
                            for (int count = 0; count < dt.Rows.Count; count++)
                            {
                                DataRow dRow = dtTemp1.NewRow();
                                string add = dt.Rows[count]["distributorId"].ToString()
                                             + Environment.NewLine + dt.Rows[count]["distributorName"].ToString();
                                if (dt.Rows[count]["Address1"].ToString().Trim() != string.Empty)
                                    add = add + Environment.NewLine + dt.Rows[count]["Address1"].ToString().Trim().Substring(0, dt.Rows[count]["Address1"].ToString().Trim().Length > 40 ? 40 : dt.Rows[count]["Address1"].ToString().Trim().Length);
                                if (dt.Rows[count]["Address2"].ToString().Trim() != string.Empty)
                                    add = add + Environment.NewLine + dt.Rows[count]["Address2"].ToString().Trim().Substring(0, dt.Rows[count]["Address2"].ToString().Trim().Length > 40 ? 40 : dt.Rows[count]["Address2"].ToString().Trim().Length);
                                if (dt.Rows[count]["Address34"].ToString().Trim() != string.Empty)
                                    add = add + Environment.NewLine + dt.Rows[count]["Address34"].ToString().Trim().Substring(0, dt.Rows[count]["Address34"].ToString().Trim().Length > 40 ? 40 : dt.Rows[count]["Address34"].ToString().Trim().Length);
                                add = add + Environment.NewLine + dt.Rows[count]["CityAddress"].ToString()
                                          + Environment.NewLine + dt.Rows[count]["Phone"].ToString();
                                dRow["Address"] = add.ToUpper();
                                dtTemp1.Rows.Add(dRow);
                            }

                            DataTable dtPrint = new DataTable();
                            dtPrint.Columns.Add(new DataColumn("AddressCol1", System.Type.GetType("System.String")));
                            dtPrint.Columns.Add(new DataColumn("AddressCol2", System.Type.GetType("System.String")));
                            
                            decimal RecCount = Math.Ceiling(Convert.ToDecimal(dtTemp1.Rows.Count)/2);
                            int currCount = 0;
                            for (int count = 0; count < RecCount; count++)
                            {
                                if (currCount < dtTemp1.Rows.Count)
                                {
                                    DataRow dRow = dtPrint.NewRow();
                                    dRow["AddressCol1"] = dtTemp1.Rows[currCount]["Address"].ToString();
                                    
                                    if (currCount+1 < dtTemp1.Rows.Count)
                                    {
                                        dRow["AddressCol2"] = dtTemp1.Rows[currCount+1]["Address"].ToString();
                                        currCount = currCount + 1;
                                    }
                                    currCount = currCount + 1;
                                    dtPrint.Rows.Add(dRow);
                                }                               
                            }
                            listOfReportDataSource.Add(new ReportDataSource("DistributorAddressLabelPrint_DistributorAddressLabelPrint_DataTable", dtPrint));
                        }
                    }
                }
                return listOfReportDataSource;
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                throw ex;
            }
        }

        public static List<ReportDataSource> PrintDistributorSingleAddressLabel(ref String errorMessage, string fromDistributorId)
        {
            List<ReportDataSource> listOfReportDataSource = new List<ReportDataSource>();
            DBParameterList dbParam;
            try
            {
                {
                    DataTaskManager dtManager = new DataTaskManager();

                    // initialize the parameter list object
                    dbParam = new DBParameterList();

                    // add the relevant parameters
                    dbParam.Add(new DBParameter("fromDistributorId", fromDistributorId, DbType.String));
                    dbParam.Add(new DBParameter("toDistributorId",fromDistributorId, DbType.String));
                    dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, errorMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                    // executing procedure to fetch the record
                    DataTable dt = dtManager.ExecuteDataTable(SP_RPTDISTRIBUTORADDRESS_LABEL, dbParam);

                    // update database message
                    errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();

                    // if an error returned from the database
                    if (errorMessage != string.Empty)
                        return null;
                    else
                    {
                        if (dt.Rows.Count > 0)
                        {
                            DataTable dtTemp1 = new DataTable();
                            dtTemp1.Columns.Add(new DataColumn("AddressCol1", System.Type.GetType("System.String")));

                            for (int count = 0; count < dt.Rows.Count; count++)
                            {
                                DataRow dRow = dtTemp1.NewRow();
                                string add = dt.Rows[count]["distributorId"].ToString()
                                             + Environment.NewLine + dt.Rows[count]["distributorName"].ToString();
                                if (dt.Rows[count]["Address1"].ToString().Trim() != string.Empty)
                                    add = add + Environment.NewLine + dt.Rows[count]["Address1"].ToString().Trim().Substring(0, dt.Rows[count]["Address1"].ToString().Trim().Length > 40 ? 40 : dt.Rows[count]["Address1"].ToString().Trim().Length);
                                if (dt.Rows[count]["Address2"].ToString().Trim() != string.Empty)
                                    add = add + Environment.NewLine + dt.Rows[count]["Address2"].ToString().Trim().Substring(0, dt.Rows[count]["Address2"].ToString().Trim().Length > 40 ? 40 : dt.Rows[count]["Address2"].ToString().Trim().Length);
                                if (dt.Rows[count]["Address34"].ToString().Trim() != string.Empty)
                                    add = add + Environment.NewLine + dt.Rows[count]["Address34"].ToString().Trim().Substring(0, dt.Rows[count]["Address34"].ToString().Trim().Length > 40 ? 40 : dt.Rows[count]["Address34"].ToString().Trim().Length);
                                add = add + Environment.NewLine + dt.Rows[count]["CityAddress"].ToString()
                                          + Environment.NewLine + dt.Rows[count]["Phone"].ToString();
                                dRow["AddressCol1"] = add.ToUpper();
                                dtTemp1.Rows.Add(dRow);
                            }

                            //DataTable dtPrint = new DataTable();
                            //dtPrint.Columns.Add(new DataColumn("AddressCol1", System.Type.GetType("System.String")));
                            //dtPrint.Columns.Add(new DataColumn("AddressCol2", System.Type.GetType("System.String")));

                            //decimal RecCount = Math.Ceiling(Convert.ToDecimal(dtTemp1.Rows.Count) / 2);
                            //int currCount = 0;
                            //for (int count = 0; count < RecCount; count++)
                            //{
                            //    if (currCount < dtTemp1.Rows.Count)
                            //    {
                            //        DataRow dRow = dtPrint.NewRow();
                            //        dRow["AddressCol1"] = dtTemp1.Rows[currCount]["Address"].ToString();

                            //        if (currCount + 1 < dtTemp1.Rows.Count)
                            //        {
                            //            dRow["AddressCol2"] = dtTemp1.Rows[currCount + 1]["Address"].ToString();
                            //            currCount = currCount + 1;
                            //        }
                            //        currCount = currCount + 1;
                            //        dtPrint.Rows.Add(dRow);
                            //    }
                            //}
                            listOfReportDataSource.Add(new ReportDataSource("DistributorAddressLabelPrint_DistributorAddressLabelPrint_DataTable", dtTemp1));
                        }
                    }
                }
                return listOfReportDataSource;
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                throw ex;
            }
        }

        public static List<ReportDataSource> PickupCentre(ref string errorMessage, int BOlocationId, int PClocationId)
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


                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                // executing procedure to save the record 
                DataSet ds = dtManager.ExecuteDataSet(SP_RPT_Pickupcentre, dbParam);

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
                    //ds.Tables[0].Rows[0]["HeaderAddress"] = headerText;
                    //ds.Tables[0].Rows[0]["AddressText"] = addressText;
                    //ds.Tables[0].Columns.Add(new DataColumn("InvoiceDateText", Type.GetType("System.String")));
                    //ds.Tables[0].Columns.Add(new DataColumn("FromDateText", Type.GetType("System.String")));
                    //ds.Tables[0].Columns.Add(new DataColumn("ToDateText", Type.GetType("System.String")));
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        //ds.Tables[0].Rows[i]["Quantity"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["Quantity"]), Common.DisplayQtyRounding, MidpointRounding.AwayFromZero);
                        //ds.Tables[0].Rows[i]["DistributorPrice"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["DistributorPrice"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                        //ds.Tables[0].Rows[i]["Amount"] = Math.Round((Convert.ToDecimal(ds.Tables[0].Rows[i]["Quantity"]) * Convert.ToDecimal(ds.Tables[0].Rows[i]["DistributorPrice"])), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                        //ds.Tables[0].Rows[i]["FromDateText"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["FromDate"]).ToString(Common.DTP_DATE_FORMAT);
                        //ds.Tables[0].Rows[i]["ToDateText"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["ToDate"]).ToString(Common.DTP_DATE_FORMAT);
                    }
                    lrds.Add(new ReportDataSource("PickupCentre_Pickup", ds.Tables[0]));
                }
            }
            catch (Exception ex)
            {
                Vinculum.Framework.Logging.LogManager.WriteExceptionLog(ex);
            }
            return lrds;

        }


        public static List<ReportDataSource> DirectorGroupBonusCheque(ref String errorMessage, string distributorId, string fromDate, string toDate, int period, int ReportType, int chequenumber)
        {
            List<ReportDataSource> listOfReportDataSource = new List<ReportDataSource>();
            DBParameterList dbParam;
            try
            {
                {
                    DataTaskManager dtManager = new DataTaskManager();

                    // initialize the parameter list object
                    dbParam = new DBParameterList();

                    // add the relevant parameters
                    dbParam.Add(new DBParameter("distributorId", distributorId, DbType.String));
                    dbParam.Add(new DBParameter("fromDate", fromDate, DbType.String));
                    dbParam.Add(new DBParameter("toDate", toDate, DbType.String));
                    dbParam.Add(new DBParameter("period", period, DbType.Int32));
                    dbParam.Add(new DBParameter("Type", ReportType, DbType.Int32));
                    dbParam.Add(new DBParameter("chequenumber", chequenumber, DbType.Int32));
                    dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, errorMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                    // executing procedure to fetch the record
                    DataTable dt = dtManager.ExecuteDataTable(SP_RPT_DirectorGroupBonusCheque, dbParam);

                    // update database message
                    errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();

                    // if an error returned from the database
                    if (errorMessage != string.Empty)
                        return null;
                    else
                    {
                        if (dt.Rows.Count > 0)
                        {
                            string address = Common.ReportHeaderAddress();
                            string headerText = address.Substring(0, address.IndexOf("*|$|*"));
                            string addressText = address.Substring(address.IndexOf("*|$|*") + 5, address.Length - address.IndexOf("*|$|*") - 5);
                            addressText = addressText.Replace("*|$|*", Environment.NewLine);
                            dt.Columns.Add(new DataColumn("HeaderAddress", Type.GetType("System.String")));
                            dt.Columns.Add(new DataColumn("AddressText", Type.GetType("System.String")));
                            dt.Rows[0]["HeaderAddress"] = headerText;
                            dt.Rows[0]["AddressText"] = addressText;
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                dt.Rows[i]["PrevCumPV"] = Math.Round(Convert.ToDecimal(dt.Rows[i]["PrevCumPV"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                                dt.Rows[i]["SelfPV"] = Math.Round(Convert.ToDecimal(dt.Rows[i]["SelfPV"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                                dt.Rows[i]["GroupPV"] = Math.Round(Convert.ToDecimal(dt.Rows[i]["GroupPV"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                                dt.Rows[i]["TotalPV"] = Math.Round(Convert.ToDecimal(dt.Rows[i]["TotalPV"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                                dt.Rows[i]["TotalCumPV"] = Math.Round(Convert.ToDecimal(dt.Rows[i]["TotalCumPV"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                                dt.Rows[i]["Level"] = Math.Round(Convert.ToDecimal(dt.Rows[i]["Level"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);

                            }
                            listOfReportDataSource.Add(new ReportDataSource("DirectorGroupBonusCheque_DirectorGroupBonusCheque", dt));
                        }
                    }
                }
                return listOfReportDataSource;
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                throw ex;
            }
        }
        public static List<ReportDataSource> SearchDistributorAcceptReject(ref String errorMessage, string distributorId, string fromDate, string toDate, int ReportType)
        {
            List<ReportDataSource> listOfReportDataSource = new List<ReportDataSource>();
            DBParameterList dbParam;
            try
            {
                {
                    DataTaskManager dtManager = new DataTaskManager();

                    // initialize the parameter list object
                    dbParam = new DBParameterList();

                    // add the relevant parameters
                    dbParam.Add(new DBParameter("distributorId", distributorId, DbType.String));
                    dbParam.Add(new DBParameter("fromDate", fromDate, DbType.String));
                    dbParam.Add(new DBParameter("toDate", toDate, DbType.String));
                    dbParam.Add(new DBParameter("Type", ReportType, DbType.Int32));
                    dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, errorMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                    // executing procedure to fetch the record
                    DataTable dt = dtManager.ExecuteDataTable(SP_RPT_DistributorPanBanAcceptReject, dbParam);

                    // update database message
                    errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();

                    // if an error returned from the database
                    if (errorMessage != string.Empty)
                        return null;
                    else
                    {
                        if (dt.Rows.Count > 0)
                        {
                            string address = Common.ReportHeaderAddress();
                            string headerText = address.Substring(0, address.IndexOf("*|$|*"));
                            string addressText = address.Substring(address.IndexOf("*|$|*") + 5, address.Length - address.IndexOf("*|$|*") - 5);
                            addressText = addressText.Replace("*|$|*", Environment.NewLine);
                            dt.Columns.Add(new DataColumn("HeaderAddress", Type.GetType("System.String")));
                            dt.Columns.Add(new DataColumn("AddressText", Type.GetType("System.String")));
                            dt.Rows[0]["HeaderAddress"] = headerText;
                            dt.Rows[0]["AddressText"] = addressText;
                            listOfReportDataSource.Add(new ReportDataSource("DistributorAcceptReject_RptDistributorAcceptReject", dt));
                        }
                    }
                }
                return listOfReportDataSource;
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                throw ex;
            }
        }

        public static List<ReportDataSource> SearchLocwiseDistributors(ref String errorMessage, Int32 locationid, string fromDate, string toDate)
        {
            List<ReportDataSource> listOfReportDataSource = new List<ReportDataSource>();
            DBParameterList dbParam;
            try
            {
                {
                    DataTaskManager dtManager = new DataTaskManager();

                    // initialize the parameter list object
                    dbParam = new DBParameterList();

                    // add the relevant parameters
                    dbParam.Add(new DBParameter("Locationid", locationid, DbType.Int32));
                    dbParam.Add(new DBParameter("fromDate", fromDate, DbType.String));
                    dbParam.Add(new DBParameter("toDate", toDate, DbType.String));
                    //dbParam.Add(new DBParameter("Type", ReportType, DbType.Int32));
                    dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, errorMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                    // executing procedure to fetch the record
                    DataTable dt = dtManager.ExecuteDataTable(SP_RPT_LocWiseDistributorModifiedList, dbParam);

                    // update database message
                    errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();

                    // if an error returned from the database
                    if (errorMessage != string.Empty)
                        return null;
                    else
                    {
                        if (dt.Rows.Count > 0)
                        {
                            string address = Common.ReportHeaderAddress();
                            string headerText = address.Substring(0, address.IndexOf("*|$|*"));
                            string addressText = address.Substring(address.IndexOf("*|$|*") + 5, address.Length - address.IndexOf("*|$|*") - 5);
                            addressText = addressText.Replace("*|$|*", Environment.NewLine);
                            dt.Columns.Add(new DataColumn("HeaderAddress", Type.GetType("System.String")));
                            dt.Columns.Add(new DataColumn("AddressText", Type.GetType("System.String")));
                            dt.Rows[0]["HeaderAddress"] = headerText;
                            dt.Rows[0]["AddressText"] = addressText;
                            listOfReportDataSource.Add(new ReportDataSource("LocwiseDistributorModifyList_rptGetModifiedDistributor", dt));
                        }
                    }
                }
                return listOfReportDataSource;
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                throw ex;
            }
        }

        #endregion
        
    }
}
