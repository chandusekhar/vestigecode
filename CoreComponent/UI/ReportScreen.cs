#region using old
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using CoreComponent.Core.BusinessObjects;
using Vinculum.Framework.Data;
using System.Runtime.InteropServices;
#endregion

namespace CoreComponent.UI
{
    public partial class ReportScreen : Form
    {
        #region Variables

        private int m_currentPageIndex = 0;
        private int m_reportId;
        private DataSet m_reportDs;
        private IList<Stream> m_streams = null;
        private List<String> m_reportNames = null;
        private bool isLandscape = false;
        #endregion

        #region Constructor

        public ReportScreen(int reportId, DataSet reportDs)
        {
            m_reportId = reportId;
            m_reportDs = reportDs;
            InitializeComponent();
        }

        public ReportScreen()
        {
            InitializeComponent();
        }

        #endregion

        #region Events

        public void ReportScreen_Load(object sender, EventArgs e)
        {
            try
            {
                SetReportViewer();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (rptViewer.LocalReport.DataSources.Count != 0)
                    Export(rptViewer.LocalReport);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                btnPrint.Enabled = false;
                if (rptViewer.LocalReport.DataSources.Count > 0)
                {
                    Export(rptViewer.LocalReport);
                    Print();
                }
                btnPrint.Enabled = true;
            }
            catch (Exception ex)
            {
                btnPrint.Enabled = true;
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Methods

        public void PrintReport()
        {
            try
            {
                SetReportViewer();
                if (rptViewer.LocalReport.DataSources.Count > 0)
                {
                    Export(rptViewer.LocalReport);
                    Print();
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void SetReportViewer()
        {
            string address = Common.ReportHeaderAddress();
            string rAddress = Common.RegisteredOfficeAddress();
            string headerText = address.Substring(0, address.IndexOf("*|$|*"));
            string addressText = address.Substring(address.IndexOf("*|$|*") + 5, address.Length - address.IndexOf("*|$|*") - 5);
            addressText = addressText.Replace("*|$|*", Environment.NewLine);

            string regAddress = rAddress.Substring(rAddress.IndexOf("*|$|*") + 5, rAddress.Length - rAddress.IndexOf("*|$|*") - 5);
            regAddress = regAddress.Replace("*|$|*", Environment.NewLine);

            List<ReportDataSource> lst = new List<ReportDataSource>();
            switch (m_reportId)
            {
                case (int)Common.ReportType.GRN:
                    {
                        lst.Add(new ReportDataSource("GRNScreen_GRNScreenHeaderDataTable", m_reportDs.Tables[0]));
                        lst.Add(new ReportDataSource("GRNScreen_GRNScreenDetailDataTable", m_reportDs.Tables[1]));
                        rptViewer.LocalReport.ReportPath = Application.StartupPath + "\\Reports\\ScreenReports\\" + (Common.ReportPath)(int)Common.ReportType.GRN + ".rdlc";
                        Microsoft.Reporting.WinForms.ReportParameter[] param = new Microsoft.Reporting.WinForms.ReportParameter[1];
                        param[0] = new Microsoft.Reporting.WinForms.ReportParameter("ReceivedByName", m_reportDs.Tables[0].Rows[0]["ReceivedBy"].ToString());
                        rptViewer.LocalReport.SetParameters(param);
                        isLandscape = false;
                        break;
                    }
                case (int)Common.ReportType.TOI:
                    {
                        lst.Add(new ReportDataSource("TOIScreen_TOIScreenHeaderDataTable", m_reportDs.Tables[0]));
                        lst.Add(new ReportDataSource("TOIScreen_TOIScreenDetailDataTable", m_reportDs.Tables[1]));
                        rptViewer.LocalReport.ReportPath = Application.StartupPath + "\\Reports\\ScreenReports\\" + (Common.ReportPath)(int)Common.ReportType.TOI + ".rdlc";
                        Microsoft.Reporting.WinForms.ReportParameter[] param = new Microsoft.Reporting.WinForms.ReportParameter[5];
                        param[0] = new Microsoft.Reporting.WinForms.ReportParameter("TOINumber", m_reportDs.Tables[1].Rows[0]["TOINumber"].ToString());
                        param[1] = new Microsoft.Reporting.WinForms.ReportParameter("HeaderAddress", headerText);
                        param[2] = new Microsoft.Reporting.WinForms.ReportParameter("AddressText", addressText);
                        param[3] = new Microsoft.Reporting.WinForms.ReportParameter("Isexported", m_reportDs.Tables[0].Rows[0]["Isexported"].ToString());
                        param[4] = new Microsoft.Reporting.WinForms.ReportParameter("CreatedBy", m_reportDs.Tables[0].Rows[0]["CreatedBy"].ToString());
                        rptViewer.LocalReport.SetParameters(param);
                        isLandscape = false;
                        break;
                    }
                case (int)Common.ReportType.TO:
                    {
                        lst.Add(new ReportDataSource("TOScreen_TOScreenHeaderDataTable", m_reportDs.Tables[0]));
                        lst.Add(new ReportDataSource("TOScreen_TOScreenDetailDataTable", m_reportDs.Tables[1]));
                        rptViewer.LocalReport.ReportPath = Application.StartupPath + "\\Reports\\ScreenReports\\" + (Common.ReportPath)(int)Common.ReportType.TO + ".rdlc";
                        Microsoft.Reporting.WinForms.ReportParameter[] param = new Microsoft.Reporting.WinForms.ReportParameter[2];
                        param[0] = new Microsoft.Reporting.WinForms.ReportParameter("PreparedByName", m_reportDs.Tables[0].Rows[0]["ModifiedByName"].ToString());
                        param[1] = new Microsoft.Reporting.WinForms.ReportParameter("SavedOn", m_reportDs.Tables[0].Rows[0]["ModifiedDate"].ToString());
                        rptViewer.LocalReport.SetParameters(param);
                        isLandscape = false;
                        break;
                    }
                case (int)Common.ReportType.TI:
                    {
                        lst.Add(new ReportDataSource("TIScreen_TIScreenHeaderDataTable", m_reportDs.Tables[0]));
                        lst.Add(new ReportDataSource("TIScreen_TIScreenDetailDataTable", m_reportDs.Tables[1]));
                        rptViewer.LocalReport.ReportPath = Application.StartupPath + "\\Reports\\ScreenReports\\" + (Common.ReportPath)(int)Common.ReportType.TI + ".rdlc";
                        Microsoft.Reporting.WinForms.ReportParameter[] param = new Microsoft.Reporting.WinForms.ReportParameter[4];
                        param[0] = new Microsoft.Reporting.WinForms.ReportParameter("TINumber", m_reportDs.Tables[1].Rows[0]["TINumber"].ToString());
                        param[1] = new Microsoft.Reporting.WinForms.ReportParameter("HeaderAddress", headerText);
                        param[2] = new Microsoft.Reporting.WinForms.ReportParameter("AddressText", addressText);
                        param[3] = new Microsoft.Reporting.WinForms.ReportParameter("Isexported", m_reportDs.Tables[0].Rows[0]["Isexported"].ToString());
                        rptViewer.LocalReport.SetParameters(param);
                        isLandscape = false;
                        break;
                    }
                case (int)Common.ReportType.PO:
                    {
                        lst.Add(new ReportDataSource("POScreen_POScreenHeaderDataTable", m_reportDs.Tables[0]));
                        lst.Add(new ReportDataSource("POScreen_POScreenDetailDataTable", m_reportDs.Tables[1]));
                        lst.Add(new ReportDataSource("POScreen_POScreenTaxTotalDataTable", m_reportDs.Tables[2]));
                        rptViewer.LocalReport.ReportPath = Application.StartupPath + "\\Reports\\ScreenReports\\" + (Common.ReportPath)(int)Common.ReportType.PO + ".rdlc";
                        Microsoft.Reporting.WinForms.ReportParameter[] param = new Microsoft.Reporting.WinForms.ReportParameter[7];
                        param[0] = new Microsoft.Reporting.WinForms.ReportParameter("PODate", m_reportDs.Tables[0].Rows[0]["PoDate"].ToString());
                        param[1] = new Microsoft.Reporting.WinForms.ReportParameter("PONumber", m_reportDs.Tables[0].Rows[0]["PoNumber"].ToString());
                        param[2] = new Microsoft.Reporting.WinForms.ReportParameter("AmendmentNumber", m_reportDs.Tables[0].Rows[0]["AmendmentNumber"].ToString());
                        param[3] = new Microsoft.Reporting.WinForms.ReportParameter("HeaderAddress", headerText);
                        param[4] = new Microsoft.Reporting.WinForms.ReportParameter("AddressText", addressText);
                        param[5] = new Microsoft.Reporting.WinForms.ReportParameter("CreatedBy", m_reportDs.Tables[0].Rows[0]["CreatedBy"].ToString());
                        param[6] = new Microsoft.Reporting.WinForms.ReportParameter("PrintedBy", m_reportDs.Tables[0].Rows[0]["PrintedBy"].ToString());
                        rptViewer.LocalReport.SetParameters(param);
                        isLandscape = false;
                        break;
                    }
                case (int)Common.ReportType.ManualIndent:
                    {
                        lst.Add(new ReportDataSource("IndentScreen_IndentScreenHeaderDataTable", m_reportDs.Tables[0]));
                        lst.Add(new ReportDataSource("IndentScreen_IndentScreenDetailDataTable", m_reportDs.Tables[1]));
                        if (Convert.ToInt32(m_reportDs.Tables[0].Rows[0]["IndentType"]) == 1)
                            rptViewer.LocalReport.ReportPath = Application.StartupPath + "\\Reports\\ScreenReports\\" + (Common.ReportPath)(int)Common.ReportType.ManualIndent + ".rdlc";
                        else
                            rptViewer.LocalReport.ReportPath = Application.StartupPath + "\\Reports\\ScreenReports\\" + (Common.ReportPath)(int)Common.ReportType.SuggestedIndent + ".rdlc";
                        Microsoft.Reporting.WinForms.ReportParameter[] param = new Microsoft.Reporting.WinForms.ReportParameter[5];
                        param[0] = new Microsoft.Reporting.WinForms.ReportParameter("IndentNo", m_reportDs.Tables[0].Rows[0]["IndentNo"].ToString());
                        param[1] = new Microsoft.Reporting.WinForms.ReportParameter("IndentDate", m_reportDs.Tables[0].Rows[0]["IndentDate"].ToString());
                        param[2] = new Microsoft.Reporting.WinForms.ReportParameter("CityName", m_reportDs.Tables[0].Rows[0]["CityName"].ToString());
                        param[3] = new Microsoft.Reporting.WinForms.ReportParameter("HeaderAddress", headerText);
                        param[4] = new Microsoft.Reporting.WinForms.ReportParameter("AddressText", addressText);
                        rptViewer.LocalReport.SetParameters(param);
                        isLandscape = false;
                        break;
                    }
                case (int)Common.ReportType.StockAdjustment:
                    {
                        lst.Add(new ReportDataSource("InventoryAdjScreen_InventoryAdjScreenHeaderDataTable", m_reportDs.Tables[0]));
                        lst.Add(new ReportDataSource("InventoryAdjScreen_InventoryAdjScreenDetailDataTable", m_reportDs.Tables[1]));
                        rptViewer.LocalReport.ReportPath = Application.StartupPath + "\\Reports\\ScreenReports\\" + (Common.ReportPath)(int)Common.ReportType.StockAdjustment + ".rdlc";
                        Microsoft.Reporting.WinForms.ReportParameter[] param = new Microsoft.Reporting.WinForms.ReportParameter[3];
                        param[0] = new Microsoft.Reporting.WinForms.ReportParameter("AdjustmentNo", m_reportDs.Tables[1].Rows[0]["AdjustmentNo"].ToString());
                        param[1] = new Microsoft.Reporting.WinForms.ReportParameter("HeaderAddress", headerText);
                        param[2] = new Microsoft.Reporting.WinForms.ReportParameter("AddressText", addressText);
                        rptViewer.LocalReport.SetParameters(param);
                        isLandscape = false;
                        break;
                    }
                case (int)Common.ReportType.StockCount:
                    {
                        lst.Add(new ReportDataSource("StockCountScreen_SCScreenHeaderDataTable", m_reportDs.Tables[0]));
                        lst.Add(new ReportDataSource("StockCountScreen_SCScreenDetailDataTable", m_reportDs.Tables[1]));
                        rptViewer.LocalReport.ReportPath = Application.StartupPath + "\\Reports\\ScreenReports\\" + (Common.ReportPath)(int)Common.ReportType.StockCount + ".rdlc";
                        Microsoft.Reporting.WinForms.ReportParameter[] param = new Microsoft.Reporting.WinForms.ReportParameter[3];
                        param[0] = new Microsoft.Reporting.WinForms.ReportParameter("SeqNumber", m_reportDs.Tables[0].Rows[0]["SeqNumber"].ToString());
                        param[1] = new Microsoft.Reporting.WinForms.ReportParameter("HeaderAddress", headerText);
                        param[2] = new Microsoft.Reporting.WinForms.ReportParameter("AddressText", addressText);
                        rptViewer.LocalReport.SetParameters(param);
                        isLandscape = false;
                        break;
                    }
                case (int)Common.ReportType.RTV:
                    {
                        lst.Add(new ReportDataSource("RTVScreen_RTVScreenHeaderDataTable", m_reportDs.Tables[0]));
                        lst.Add(new ReportDataSource("RTVScreen_RTVScreenDetailDataTable", m_reportDs.Tables[1]));
                        rptViewer.LocalReport.ReportPath = Application.StartupPath + "\\Reports\\ScreenReports\\" + (Common.ReportPath)(int)Common.ReportType.RTV + ".rdlc";
                        Microsoft.Reporting.WinForms.ReportParameter[] param = new Microsoft.Reporting.WinForms.ReportParameter[3];
                        param[0] = new Microsoft.Reporting.WinForms.ReportParameter("ReturnNumber", m_reportDs.Tables[0].Rows[0]["ReturnNumber"].ToString());
                        param[1] = new Microsoft.Reporting.WinForms.ReportParameter("HeaderAddress", headerText);
                        param[2] = new Microsoft.Reporting.WinForms.ReportParameter("AddressText", addressText);
                        rptViewer.LocalReport.SetParameters(param);
                        isLandscape = false;
                        break;
                    }
                case (int)Common.ReportType.ICMP:
                    {
                        lst.Add(new ReportDataSource("ICMP_ICMPDataTable", m_reportDs.Tables[0]));

                        rptViewer.LocalReport.ReportPath = Application.StartupPath + "\\Reports\\ScreenReports\\" + (Common.ReportPath)(int)Common.ReportType.ICMP + ".rdlc";
                        Microsoft.Reporting.WinForms.ReportParameter[] param = new Microsoft.Reporting.WinForms.ReportParameter[2];
                        param[0] = new Microsoft.Reporting.WinForms.ReportParameter("HeaderText", headerText);
                        param[1] = new Microsoft.Reporting.WinForms.ReportParameter("AddressText", addressText);
                        rptViewer.LocalReport.SetParameters(param);
                        isLandscape = false;
                        break;
                    }

                case (int)Common.ReportType.PackUnpack:
                    {
                        lst.Add(new ReportDataSource("PackUnpackScreen_PackUnpackScreenHeaderDataTable", m_reportDs.Tables[0]));
                        lst.Add(new ReportDataSource("PackUnpackScreen_PackUnpackScreenDetailDataTable", m_reportDs.Tables[1]));
                        rptViewer.LocalReport.ReportPath = Application.StartupPath + "\\Reports\\ScreenReports\\" + (Common.ReportPath)(int)Common.ReportType.PackUnpack + ".rdlc";
                        Microsoft.Reporting.WinForms.ReportParameter[] param = new Microsoft.Reporting.WinForms.ReportParameter[3];
                        param[0] = new Microsoft.Reporting.WinForms.ReportParameter("PUNo", m_reportDs.Tables[0].Rows[0]["PUNo"].ToString());
                        param[1] = new Microsoft.Reporting.WinForms.ReportParameter("HeaderAddress", headerText);
                        param[2] = new Microsoft.Reporting.WinForms.ReportParameter("AddressText", addressText);
                        rptViewer.LocalReport.SetParameters(param);
                        isLandscape = false;
                        break;
                    }
                case (int)Common.ReportType.CR:
                    {
                        lst.Add(new ReportDataSource("CRScreen_CRHeaderDataTable", m_reportDs.Tables[0]));
                        lst.Add(new ReportDataSource("CRScreen_CRDetailDataTable", m_reportDs.Tables[1]));
                        rptViewer.LocalReport.ReportPath = Application.StartupPath + "\\Reports\\ScreenReports\\" + (Common.ReportPath)(int)Common.ReportType.CR + ".rdlc";
                        Microsoft.Reporting.WinForms.ReportParameter[] param = new Microsoft.Reporting.WinForms.ReportParameter[3];
                        param[0] = new Microsoft.Reporting.WinForms.ReportParameter("ReturnNo", m_reportDs.Tables[0].Rows[0]["ReturnNo"].ToString());
                        param[1] = new Microsoft.Reporting.WinForms.ReportParameter("HeaderAddress", headerText);
                        param[2] = new Microsoft.Reporting.WinForms.ReportParameter("AddressText", addressText);
                        rptViewer.LocalReport.SetParameters(param);
                        isLandscape = false;
                        break;
                    }
                case (int)Common.ReportType.CRCreditNote:
                    {
                        lst.Add(new ReportDataSource("CRScreen_CRHeaderDataTable", m_reportDs.Tables[0]));
                        lst.Add(new ReportDataSource("CRScreen_CRDetailDataTable", m_reportDs.Tables[1]));
                        rptViewer.LocalReport.ReportPath = Application.StartupPath + "\\Reports\\ScreenReports\\" + (Common.ReportPath)(int)Common.ReportType.CRCreditNote + ".rdlc";
                        Microsoft.Reporting.WinForms.ReportParameter[] param = new Microsoft.Reporting.WinForms.ReportParameter[3];
                        param[0] = new Microsoft.Reporting.WinForms.ReportParameter("ReturnNo", m_reportDs.Tables[0].Rows[0]["ReturnNo"].ToString());
                        param[1] = new Microsoft.Reporting.WinForms.ReportParameter("HeaderAddress", headerText);
                        param[2] = new Microsoft.Reporting.WinForms.ReportParameter("AddressText", addressText);
                        rptViewer.LocalReport.SetParameters(param);
                        isLandscape = false;
                        break;
                    }
                case (int)Common.ReportType.RTVDebitNote:
                    {
                        lst.Add(new ReportDataSource("RTVScreen_RTVScreenHeaderDataTable", m_reportDs.Tables[0]));
                        lst.Add(new ReportDataSource("RTVScreen_RTVScreenDetailDataTable", m_reportDs.Tables[1]));
                        rptViewer.LocalReport.ReportPath = Application.StartupPath + "\\Reports\\ScreenReports\\" + (Common.ReportPath)(int)Common.ReportType.RTVDebitNote + ".rdlc";
                        Microsoft.Reporting.WinForms.ReportParameter[] param = new Microsoft.Reporting.WinForms.ReportParameter[3];
                        param[0] = new Microsoft.Reporting.WinForms.ReportParameter("ReturnNumber", m_reportDs.Tables[0].Rows[0]["ReturnNumber"].ToString());
                        param[1] = new Microsoft.Reporting.WinForms.ReportParameter("HeaderAddress", headerText);
                        param[2] = new Microsoft.Reporting.WinForms.ReportParameter("AddressText", addressText);
                        rptViewer.LocalReport.SetParameters(param);
                        isLandscape = false;
                        break;
                    }
                case (int)Common.ReportType.CustomerInvoice:
                    {
                        lst.Add(new ReportDataSource("CustomerInvoiceScreen_CustomerOrder", m_reportDs.Tables[0]));
                        lst.Add(new ReportDataSource("CustomerInvoiceScreen_CustomerOrderDetail", m_reportDs.Tables[1]));
                        lst.Add(new ReportDataSource("CustomerInvoiceScreen_PaymentDetail", m_reportDs.Tables[2]));
                        lst.Add(new ReportDataSource("CustomerInvoiceScreen_TaxDetail", m_reportDs.Tables[3]));


                        #region KitReport
                        if (m_reportDs.Tables[0].Rows[0]["UsedForRegistration"].ToString().Equals("True"))
                        {


                            rptViewer.LocalReport.ReportPath = Application.StartupPath + "\\Reports\\ScreenReports\\" + GetKITReportName() + ".rdlc";

                            string locationAddress = (m_reportDs.Tables[0].Rows[0]["DeliverFromAddress1"].ToString())
                                                         + " "
                                                         + (m_reportDs.Tables[0].Rows[0]["DeliverFromAddress2"].ToString().Trim()) + Environment.NewLine + (m_reportDs.Tables[0].Rows[0]["DeliverFromAddress3"].ToString().Trim())
                                                         + (((m_reportDs.Tables[0].Rows[0]["DeliverFromAddress3"].ToString().Trim()).Length) > 0 ? " " : "")
                                                         + (m_reportDs.Tables[0].Rows[0]["FromCity"].ToString().Trim()) + "  " + (m_reportDs.Tables[0].Rows[0]["DeliverFromPincode"].ToString()) + Environment.NewLine
                                                         + "Phone : " + (m_reportDs.Tables[0].Rows[0]["DeliverFromTelephone"].ToString()) + Environment.NewLine + "EMailID: " + Common.EMAILID.ToString();

                            //string emailAddress=  " EMailID: " + Common.EMAILID.ToString();

                            if (Common.LocationCode == "HO")
                                m_reportDs.Tables[1].Rows[0]["IsLocation"] = "1";
                            else
                                m_reportDs.Tables[1].Rows[0]["IsLocation"] = "0";
                            if (Common.CountryID == "2")
                            {
                                m_reportDs.Tables[1].Rows[0]["IsPromoRecord"] = GetPromoRecordNo();
                                m_reportDs.Tables[0].Rows[0]["DistributorAddress"] = m_reportDs.Tables[0].Rows[0]["DistributorAddress"] + Environment.NewLine +
                                   Environment.NewLine + "Customer Pan/Vat No : " + m_reportDs.Tables[0].Rows[0]["DistributorPANNumber"].ToString();
                                locationAddress += Environment.NewLine + Environment.NewLine +
                                                   "Seller Vat No :" + m_reportDs.Tables[0].Rows[0]["TINNo"].ToString();
                            }

                            Microsoft.Reporting.WinForms.ReportParameter[] param = new Microsoft.Reporting.WinForms.ReportParameter[10];
                            param[0] = new Microsoft.Reporting.WinForms.ReportParameter("HeaderAddress", headerText);
                            param[1] = new Microsoft.Reporting.WinForms.ReportParameter("AddressText", locationAddress);
                            param[2] = new Microsoft.Reporting.WinForms.ReportParameter("Header", m_reportDs.Tables[0].Rows[0]["Header"].ToString());
                            param[3] = new Microsoft.Reporting.WinForms.ReportParameter("Date", m_reportDs.Tables[0].Rows[0]["DateText"].ToString());
                            param[4] = new Microsoft.Reporting.WinForms.ReportParameter("OrderNo", m_reportDs.Tables[0].Rows[0]["CustomerOrderNo"].ToString());
                            param[5] = new Microsoft.Reporting.WinForms.ReportParameter("TINNo", m_reportDs.Tables[0].Rows[0]["TINNo"].ToString());
                            param[6] = new Microsoft.Reporting.WinForms.ReportParameter("UserName", m_reportDs.Tables[0].Rows[0]["UserName"].ToString());
                            param[7] = new Microsoft.Reporting.WinForms.ReportParameter("RegdOfficeAddress", regAddress.Replace(Environment.NewLine, " "));
                            param[8] = new Microsoft.Reporting.WinForms.ReportParameter("PANNo", m_reportDs.Tables[0].Rows[0]["PANNumber"].ToString());
                            //param[10] = new Microsoft.Reporting.WinForms.ReportParameter("EmailText", emailAddress);
                            param[9] = new Microsoft.Reporting.WinForms.ReportParameter("FromState", m_reportDs.Tables[0].Rows[0]["FromState"].ToString());
                            rptViewer.LocalReport.SetParameters(param);
                            isLandscape = false;



                        }
                        #endregion

                        #region RPTInvoice Full & Half
                        else
                        {
                            string locationAddress;
                            rptViewer.LocalReport.ReportPath = Application.StartupPath + "\\Reports\\ScreenReports\\" + GetReportName() + ".rdlc";


                            if (Common.CountryID == "4")
                            {
                                locationAddress = (m_reportDs.Tables[0].Rows[0]["DeliverFromAddress1"].ToString())
                                                             + " "
                                                             + (m_reportDs.Tables[0].Rows[0]["DeliverFromAddress2"].ToString().Trim()) + Environment.NewLine + (m_reportDs.Tables[0].Rows[0]["DeliverFromAddress3"].ToString().Trim())
                                                             + (((m_reportDs.Tables[0].Rows[0]["DeliverFromAddress3"].ToString().Trim()).Length) > 0 ? " " : "")
                                                             + (m_reportDs.Tables[0].Rows[0]["FromCity"].ToString().Trim()) + ", " + (m_reportDs.Tables[0].Rows[0]["FromCountry"].ToString().Trim()) + Environment.NewLine
                                                             + "Phone : " + (m_reportDs.Tables[0].Rows[0]["DeliverFromTelephone"].ToString()) + Environment.NewLine + "EMail-ID: " + Common.EMAILID.ToString(); ;
                            }
                            else
                            {
                                locationAddress = (m_reportDs.Tables[0].Rows[0]["DeliverFromAddress1"].ToString())
                                                             + " "
                                                             + (m_reportDs.Tables[0].Rows[0]["DeliverFromAddress2"].ToString().Trim()) + Environment.NewLine + (m_reportDs.Tables[0].Rows[0]["DeliverFromAddress3"].ToString().Trim())
                                                             + (((m_reportDs.Tables[0].Rows[0]["DeliverFromAddress3"].ToString().Trim()).Length) > 0 ? " " : "")
                                                             + (m_reportDs.Tables[0].Rows[0]["FromCity"].ToString().Trim()) + "  " + (m_reportDs.Tables[0].Rows[0]["DeliverFromPincode"].ToString()) + Environment.NewLine
                                                             + "Phone : " + (m_reportDs.Tables[0].Rows[0]["DeliverFromTelephone"].ToString()) + Environment.NewLine + "EMailID: " + (m_reportDs.Tables[0].Rows[0]["EmailId1"].ToString()); ;
                                //Common.EMAILID.ToString(); 
                            }

                            //string emailAddress = " EMailID: " + Common.EMAILID.ToString();

                            if (Common.LocationCode == "HO")
                                m_reportDs.Tables[1].Rows[0]["IsLocation"] = "1";
                            else
                                m_reportDs.Tables[1].Rows[0]["IsLocation"] = "0";

                            if (Common.CountryID == "2")
                            {
                                m_reportDs.Tables[1].Rows[0]["IsPromoRecord"] = GetPromoRecordNo();
                                m_reportDs.Tables[0].Rows[0]["DistributorAddress"] = m_reportDs.Tables[0].Rows[0]["DistributorAddress"] + Environment.NewLine +
                                   Environment.NewLine + "Customer Pan/Vat No : " + m_reportDs.Tables[0].Rows[0]["DistributorPANNumber"].ToString();
                                 locationAddress += Environment.NewLine + Environment.NewLine +
                                                   "Seller Vat No :" + m_reportDs.Tables[0].Rows[0]["TINNo"].ToString();
                            }


                            Microsoft.Reporting.WinForms.ReportParameter[] param = new Microsoft.Reporting.WinForms.ReportParameter[10];
                            param[0] = new Microsoft.Reporting.WinForms.ReportParameter("HeaderAddress", headerText);
                            param[1] = new Microsoft.Reporting.WinForms.ReportParameter("AddressText", locationAddress);
                            param[2] = new Microsoft.Reporting.WinForms.ReportParameter("Header", m_reportDs.Tables[0].Rows[0]["Header"].ToString());
                            param[3] = new Microsoft.Reporting.WinForms.ReportParameter("Date", m_reportDs.Tables[0].Rows[0]["DateText"].ToString());
                            param[4] = new Microsoft.Reporting.WinForms.ReportParameter("OrderNo", m_reportDs.Tables[0].Rows[0]["CustomerOrderNo"].ToString());
                            param[5] = new Microsoft.Reporting.WinForms.ReportParameter("TINNo", m_reportDs.Tables[0].Rows[0]["TINNo"].ToString());
                            param[6] = new Microsoft.Reporting.WinForms.ReportParameter("UserName", m_reportDs.Tables[0].Rows[0]["UserName"].ToString());
                            param[7] = new Microsoft.Reporting.WinForms.ReportParameter("RegdOfficeAddress", regAddress.Replace(Environment.NewLine, " "));
                            param[8] = new Microsoft.Reporting.WinForms.ReportParameter("PANNo", m_reportDs.Tables[0].Rows[0]["PANNumber"].ToString());
                            param[9] = new Microsoft.Reporting.WinForms.ReportParameter("FromState", m_reportDs.Tables[0].Rows[0]["FromState"].ToString());
                            //param[10] = new Microsoft.Reporting.WinForms.ReportParameter("EmailText", emailAddress);
                            //param[9] = new Microsoft.Reporting.WinForms.ReportParameter("StateId", m_reportDs.Tables[0].Rows[0]["StateId"].ToString());
                            rptViewer.LocalReport.SetParameters(param);
                            isLandscape = false;


                            //break;
                        }
                        #endregion


                        break;

                    }
                case (int)Common.ReportType.CustomerOrder:
                    {
                        lst.Add(new ReportDataSource("CustomerInvoiceScreen_CustomerOrder", m_reportDs.Tables[0]));
                        lst.Add(new ReportDataSource("CustomerInvoiceScreen_CustomerOrderDetail", m_reportDs.Tables[1]));
                        lst.Add(new ReportDataSource("CustomerInvoiceScreen_PaymentDetail", m_reportDs.Tables[2]));
                        rptViewer.LocalReport.ReportPath = Application.StartupPath + "\\Reports\\ScreenReports\\" + (Common.ReportPath)(int)Common.ReportType.CustomerOrder + ".rdlc";


                        string locationAddress = (m_reportDs.Tables[0].Rows[0]["DeliverFromAddress1"].ToString())
                                                 + " "
                                                 + (m_reportDs.Tables[0].Rows[0]["DeliverFromAddress2"].ToString().Trim()) + Environment.NewLine + (m_reportDs.Tables[0].Rows[0]["DeliverFromAddress3"].ToString().Trim())
                                                 + (((m_reportDs.Tables[0].Rows[0]["DeliverFromAddress3"].ToString().Trim()).Length) > 0 ? " " : "")
                                                 + (m_reportDs.Tables[0].Rows[0]["FromCity"].ToString().Trim()) + "  " + (m_reportDs.Tables[0].Rows[0]["DeliverFromPincode"].ToString()) + Environment.NewLine
                                                 + "Phone : " + (m_reportDs.Tables[0].Rows[0]["DeliverFromTelephone"].ToString());
                        
                        Microsoft.Reporting.WinForms.ReportParameter[] param = new Microsoft.Reporting.WinForms.ReportParameter[9];
                        param[0] = new Microsoft.Reporting.WinForms.ReportParameter("HeaderAddress", headerText);
                        param[1] = new Microsoft.Reporting.WinForms.ReportParameter("AddressText", locationAddress);
                        param[2] = new Microsoft.Reporting.WinForms.ReportParameter("Header", m_reportDs.Tables[0].Rows[0]["Header"].ToString());
                        param[3] = new Microsoft.Reporting.WinForms.ReportParameter("Date", m_reportDs.Tables[0].Rows[0]["DateText"].ToString());
                        param[4] = new Microsoft.Reporting.WinForms.ReportParameter("OrderNo", m_reportDs.Tables[0].Rows[0]["CustomerOrderNo"].ToString());
                        param[5] = new Microsoft.Reporting.WinForms.ReportParameter("TINNo", m_reportDs.Tables[0].Rows[0]["TINNo"].ToString());
                        param[6] = new Microsoft.Reporting.WinForms.ReportParameter("UserName", m_reportDs.Tables[0].Rows[0]["UserName"].ToString());
                        param[7] = new Microsoft.Reporting.WinForms.ReportParameter("RegdOfficeAddress", regAddress.Replace(Environment.NewLine, " "));
                        param[8] = new Microsoft.Reporting.WinForms.ReportParameter("PANNo", m_reportDs.Tables[0].Rows[0]["PANNo"].ToString());
                        rptViewer.LocalReport.SetParameters(param);
                        isLandscape = false;
                        break;
                    }
                case (int)Common.ReportType.BonusStatementDirectors:
                    {
                        lst.Add(new ReportDataSource("BonusStatementDirectors_DistributorInfo", m_reportDs.Tables[0]));
                        lst.Add(new ReportDataSource("BonusStatementDirectors_PerformanceBonus", m_reportDs.Tables[1]));
                        lst.Add(new ReportDataSource("BonusStatementDirectors_DownlineInfo", m_reportDs.Tables[2]));
                        lst.Add(new ReportDataSource("BonusStatementDirectors_DistributorGroupMonthly", m_reportDs.Tables[3]));
                        lst.Add(new ReportDataSource("BonusStatementDirectors_DistributorGroupMonthly1", m_reportDs.Tables[4]));
                        lst.Add(new ReportDataSource("BonusStatementDirectors_DistributorTotal", m_reportDs.Tables[5]));
                        lst.Add(new ReportDataSource("BonusStatementDirectors_DistributorPayOrderInfo", m_reportDs.Tables[6]));
                        lst.Add(new ReportDataSource("BonusStatementDirectors_DistributorGiftVoucherInfo", m_reportDs.Tables[7]));
                        lst.Add(new ReportDataSource("BonusStatementDirectors_DistributorPBAmountInfo", m_reportDs.Tables[8]));
                        lst.Add(new ReportDataSource("BonusStatementDirectors_DistributorCarInfo", m_reportDs.Tables[9]));
                        lst.Add(new ReportDataSource("BonusStatementDirectors_DistributorCurrentMnthCarInfo", m_reportDs.Tables[10]));
                        lst.Add(new ReportDataSource("BonusStatementDirectors_DistributorViaChequeInfo", m_reportDs.Tables[11]));

                        lst.Add(new ReportDataSource("BonusStatementDirectors_DistPerformanceBonus", m_reportDs.Tables[12]));
                        lst.Add(new ReportDataSource("BonusStatementDirectors_BonusChkVoucher", m_reportDs.Tables[13]));
                        lst.Add(new ReportDataSource("BonusStatementDirectors_ProductVoucher", m_reportDs.Tables[14]));
                        lst.Add(new ReportDataSource("BonusStatementDirectors_QualPvNonPV", m_reportDs.Tables[15]));
                        lst.Add(new ReportDataSource("BonusStatementDirectors_GroupArchiveSummary", m_reportDs.Tables[16]));
                        lst.Add(new ReportDataSource("BonusStatementDirectors_DistributorTFInfo", m_reportDs.Tables[17]));
                        lst.Add(new ReportDataSource("BonusStatementDirectors_Declaration", m_reportDs.Tables[18]));
                        lst.Add(new ReportDataSource("BonusStatementDirectors_TotalBvAmount", m_reportDs.Tables[19]));
                        int VoucherID = 1;
                        for (int i = 20; i < 40; i++)
                        {
                            string strVoucherName = "BonusStatementDirectors_Voucher" + VoucherID.ToString();
                            lst.Add(new ReportDataSource(strVoucherName, m_reportDs.Tables[i]));
                            VoucherID++;
                        }

                        rptViewer.LocalReport.ReportPath = Application.StartupPath + "\\Reports\\" + "BonusStatementDirectors" + ".rdlc";
                        //Add header
                        m_reportDs.Tables[0].Rows[0]["HeaderAddress"] = headerText;
                        m_reportDs.Tables[0].Rows[0]["AddressText"] = addressText;
                        //Table[5] --(BonusStatementDirectors_DistributorTotal)
                        for (int i = 0; i < m_reportDs.Tables[5].Rows.Count; i++)
                        {

                            //ds.Tables[4].Rows[i]["Amount"] = Math.Round(Convert.ToDecimal(ds.Tables[4].Rows[i]["Amount"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                            string str = Common.AmountToWords.AmtInWord(Convert.ToDecimal(m_reportDs.Tables[5].Rows[i]["Amount"]));
                            string substr = str.Remove(0, 6);
                            if (substr.ToString().Trim().Equals("Only"))
                                substr = "Zero Only";

                            DataTable dsN = (DataTable)lst.Find(p => p.Name == "BonusStatementDirectors_DistributorTotal").Value;
                            dsN.Rows[i]["BonusAmountWord"] = substr;
                        }
                        isLandscape = false;
                        break;                                                

                    }
                case (int)Common.ReportType.CarBonusReport:
                    {
                        //Add Dataset
                        lst.Add(new ReportDataSource("CarBonusReport_CarBonus", m_reportDs.Tables[0]));
                        lst.Add(new ReportDataSource("CarBonusReport_DistributorCarMaxAllowedAmount", m_reportDs.Tables[1]));
                        lst.Add(new ReportDataSource("CarBonusReport_CarBonusPartPayment", m_reportDs.Tables[2]));
                        lst.Add(new ReportDataSource("CarBonusReport_DistriCarFund", m_reportDs.Tables[3]));
                        rptViewer.LocalReport.ReportPath = Application.StartupPath + "\\Reports\\" + "CarBonusReport" + ".rdlc";

                        isLandscape = false;
                        break;

                    }
                case (int)Common.ReportType.TravelFundReport:
                    {
                        //Add Dataset
                        lst.Add(new ReportDataSource("TravelFund_TravelFund", m_reportDs.Tables[0]));
                        lst.Add(new ReportDataSource("TravelFund_DistriTravelFund", m_reportDs.Tables[1]));
                        rptViewer.LocalReport.ReportPath = Application.StartupPath + "\\Reports\\" + "TravelFundReport" + ".rdlc";

                        isLandscape = false;
                        break;

                    }
                case (int)Common.ReportType.TOExportInvoice:
                    {
                        //lst.Add(new ReportDataSource("TOScreen_TOScreenHeaderDataTable", m_reportDs.Tables[0]));
                        lst.Add(new ReportDataSource("TOExportInvoice_TOExportScreenHeaderDataTable", m_reportDs.Tables[0]));
                        lst.Add(new ReportDataSource("TOExportInvoice_TOExportScreenDetailDataTable", m_reportDs.Tables[1]));
                        //lst.Add(new ReportDataSource("TOScreen_TOExportScreenDetailDataTable", m_reportDs.Tables[4]));
                        rptViewer.LocalReport.ReportPath = Application.StartupPath + "\\Reports\\ScreenReports\\" + (Common.ReportPath)(int)Common.ReportType.TOExportInvoice + ".rdlc";
                        //Microsoft.Reporting.WinForms.ReportParameter[] param = new Microsoft.Reporting.WinForms.ReportParameter[2];
                        //param[0] = new Microsoft.Reporting.WinForms.ReportParameter("PreparedByName", m_reportDs.Tables[0].Rows[0]["ModifiedByName"].ToString());
                        //param[1] = new Microsoft.Reporting.WinForms.ReportParameter("SavedOn", m_reportDs.Tables[0].Rows[0]["ModifiedDate"].ToString());
                        //rptViewer.LocalReport.SetParameters(param);
                        isLandscape = false;
                        break;
                    }
                case (int)Common.ReportType.EOI:
                    {
                        lst.Add(new ReportDataSource("TOIScreen_TOIScreenHeaderDataTable", m_reportDs.Tables[0]));
                        lst.Add(new ReportDataSource("TOIScreen_TOIScreenDetailDataTable", m_reportDs.Tables[1]));
                        rptViewer.LocalReport.ReportPath = Application.StartupPath + "\\Reports\\ScreenReports\\" + (Common.ReportPath)(int)Common.ReportType.EOI + ".rdlc";
                        Microsoft.Reporting.WinForms.ReportParameter[] param = new Microsoft.Reporting.WinForms.ReportParameter[4];
                        param[0] = new Microsoft.Reporting.WinForms.ReportParameter("TOINumber", m_reportDs.Tables[1].Rows[0]["TOINumber"].ToString());
                        param[1] = new Microsoft.Reporting.WinForms.ReportParameter("HeaderAddress", headerText);
                        param[2] = new Microsoft.Reporting.WinForms.ReportParameter("AddressText", addressText);
                        param[3] = new Microsoft.Reporting.WinForms.ReportParameter("Isexported", m_reportDs.Tables[0].Rows[0]["Isexported"].ToString());
                        rptViewer.LocalReport.SetParameters(param);
                        isLandscape = false;
                        break;
                    }
                case (int)Common.ReportType.TOExportPackingList:
                    {
                        //lst.Add(new ReportDataSource("TOScreen_TOScreenHeaderDataTable", m_reportDs.Tables[0]));
                        lst.Add(new ReportDataSource("TOExportInvoice_TOExportScreenHeaderDataTable", m_reportDs.Tables[0]));
                        lst.Add(new ReportDataSource("TOExportInvoice_TOExportScreenDetailDataTable", m_reportDs.Tables[1]));
                        //lst.Add(new ReportDataSource("TOScreen_TOExportScreenDetailDataTable", m_reportDs.Tables[4]));
                        rptViewer.LocalReport.ReportPath = Application.StartupPath + "\\Reports\\ScreenReports\\" + (Common.ReportPath)(int)Common.ReportType.TOExportPackingList + ".rdlc";
                        //Microsoft.Reporting.WinForms.ReportParameter[] param = new Microsoft.Reporting.WinForms.ReportParameter[2];
                        //param[0] = new Microsoft.Reporting.WinForms.ReportParameter("PreparedByName", m_reportDs.Tables[0].Rows[0]["ModifiedByName"].ToString());
                        //param[1] = new Microsoft.Reporting.WinForms.ReportParameter("SavedOn", m_reportDs.Tables[0].Rows[0]["ModifiedDate"].ToString());
                        //rptViewer.LocalReport.SetParameters(param);
                        isLandscape = false;
                        break;
                    }
                case (int)Common.ReportType.DistributorDetail:
                    {
                        lst.Add(new ReportDataSource("DistributorPanBankDetails_RptDistributorDetail", m_reportDs.Tables[0]));
                        rptViewer.LocalReport.ReportPath = Application.StartupPath + "\\Reports\\DistributorPanBankDetail.rdlc";
                        isLandscape = false;
                        break;
                    }

            }
            rptViewer.ShowProgress = true;
            rptViewer.SetDisplayMode(DisplayMode.PrintLayout);
            rptViewer.ZoomMode = ZoomMode.Percent;
            rptViewer.ZoomPercent = 100;
            for (int i = 0; i < lst.Count; i++)
            {
                rptViewer.LocalReport.DataSources.Add(lst[i]);
            }
            //rptViewer.Refresh();
            rptViewer.RefreshReport();

        }

        private string GetPromoRecordNo()
        {
            DataRow[] objDtRow;
            StringBuilder objSb;
            string sRecordNo = "-1";
            try
            {
                objDtRow = m_reportDs.Tables[1].Select("ispromo = 1");
                if (objDtRow != null && objDtRow.Length > 0)
                {
                    objSb = new StringBuilder();
                    foreach (DataRow dtrow in objDtRow)
                    {
                        if (objSb.Length != 0)
                            objSb.Append(",");
                        objSb.Append(dtrow["RecordNo"]);
                    }
                    sRecordNo = objSb.ToString();
                }
                return sRecordNo;
            }
            finally
            {
            }
        }
        //private string GetLayoutReportName()
        //{
        //    string sReportName = "";
        //    DataTable dtData = Common.ParameterLookup(Common.ParameterType.InvoiceLayoutRpt, new ParameterFilter("LayoutInvoiceRpt", Convert.ToInt32(Common.CountryID), 0, 0));
        //    if (dtData != null && dtData.Rows.Count > 0)
        //        sReportName = dtData.Rows[0]["KeyValue1"].ToString();
        //    return sReportName;


        //}

        private string getKitReportNameErn()
        {
            string sReportName = "";
            DataTable dtData = Common.ParameterLookup(Common.ParameterType.KitInvoiceRptern, new ParameterFilter("KitRPTINVOICEern", Convert.ToInt32(Common.CurrentLocationId), 0, 0));
            if (dtData != null && dtData.Rows.Count > 0)
                sReportName = dtData.Rows[0]["KeyValue1"].ToString();


            return sReportName;


        }


        private string GetKITReportName()
        {
            string sReportName = "";
            DataTable dtData = Common.ParameterLookup(Common.ParameterType.KitInvoiceRpt, new ParameterFilter("KitInvoiceRpt", Convert.ToInt32(Common.CurrentLocationId), 0, 0));
            if (dtData != null && dtData.Rows.Count > 0)
                sReportName = dtData.Rows[0]["KeyValue1"].ToString();

            return sReportName;


        }


        private string GetReportName()
        {
            string sReportName = "";
            int printType = 0;

            if (Common.PageSize == Common.PrintPageSize.Half)
                printType = (int)Common.PrintPageSize.Half;
            else if (Common.PageSize == Common.PrintPageSize.Full)
                printType = (int)Common.PrintPageSize.Full;


            DataTable dtData = Common.ParameterLookup(Common.ParameterType.InvoiceRpt, new ParameterFilter("InvoiceRpt", Convert.ToInt32(Common.CountryID), printType, 0));
            if (dtData != null && dtData.Rows.Count > 0)
            {
                sReportName = dtData.Rows[0]["KeyValue1"].ToString();
            }
            return sReportName;
        }
        private void Export(LocalReport report)
        {
            try
            {
                string deviceInfo = "<DeviceInfo>" +
                                      "  <OutputFormat>EMF</OutputFormat>" +
                                      "  <PageWidth>8.5in</PageWidth>" +
                                      "  <PageHeight>11.69in</PageHeight>" +
                                      "  <MarginTop>0in</MarginTop>" +
                                      "  <MarginLeft>0in</MarginLeft>" +
                                      "  <MarginRight>0in</MarginRight>" +
                                      "  <MarginBottom>0in</MarginBottom>" +
                                      "  </DeviceInfo>";
                Warning[] warnings;
                m_streams = new List<Stream>();
                m_reportNames = new List<String>();
                rptViewer.LocalReport.Render("Image", deviceInfo, CreateStream, out warnings);
                foreach (Stream stream in m_streams)
                {
                    stream.Position = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                Common.LogException(ex);
            }
        }

        private Stream CreateStream(string name, string fileNameExtension, Encoding encoding, string mimeType, bool willSeek)
        {
            Stream stream = new FileStream(Environment.CurrentDirectory + "\\App_Data\\" + name + "." + fileNameExtension, FileMode.Create);
            m_streams.Add(stream);
            m_reportNames.Add(name + "." + fileNameExtension);
            return stream;
        }

        private void Print()
        {
            try
            {
                if (m_streams == null || m_streams.Count == 0)
                    return;
                PrintDocument printDoc = new PrintDocument();
                if (!printDoc.PrinterSettings.IsValid)
                {
                    string msg = String.Format("Can't find printer");
                    MessageBox.Show(msg, "Print Error");
                    return;
                }

                if (Common.PageSize == Common.PrintPageSize.Half)
                    SetHalfPagePrintLayOut(printDoc);

                printDoc.DefaultPageSettings.Landscape = isLandscape;
                printDoc.PrintPage += new PrintPageEventHandler(PrintPage);

                printDoc.Print();


                foreach (Stream stream in m_streams)
                {
                    stream.Close();
                    stream.Dispose();
                }
                foreach (string fname in m_reportNames)
                {
                    File.Delete(Environment.CurrentDirectory + "\\App_Data\\" + fname);
                }
                m_currentPageIndex = 0;
                m_reportId = 0;
                m_reportDs = null;
                m_streams = null;
                m_reportNames = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                Common.LogException(ex);
            }
        }

        private void SetHalfPagePrintLayOut(PrintDocument printDoc)
        {
            // string[] printConstants = Common.HalfPageSizePrintConstants.Split('#');
            string[] printConstants = GetHalfPagePrintSizeConstants().Split('#');

            string paperName = printConstants[0];
            int width = Convert.ToInt32(printConstants[1]);
            int height = Convert.ToInt32(printConstants[2]);
            int left = Convert.ToInt32(printConstants[3]);
            int right = Convert.ToInt32(printConstants[4]);
            int top = Convert.ToInt32(printConstants[5]);
            int bottom = Convert.ToInt32(printConstants[6]);

            PaperSize paperSize = new PaperSize(paperName, width, height);
            paperSize.RawKind = (int)PaperKind.Letter;

            printDoc.DefaultPageSettings.PaperSize = paperSize;

            printDoc.DefaultPageSettings.Margins = new Margins(left, right, top, bottom);

        }

        private void PrintPage(object sender, PrintPageEventArgs ev)
        {
            Metafile pageImage = new
               Metafile(m_streams[m_currentPageIndex]);
            // ev.Graphics.PageUnit = GraphicsUnit.Document;

            ev.Graphics.DrawImage(pageImage, ev.PageBounds);
            m_currentPageIndex++;
            ev.HasMorePages = (m_currentPageIndex < m_streams.Count);
        }

        private string GetHalfPagePrintSizeConstants()
        {
            DataTable dtPaperSizeConstants = Common.ParameterLookup(Common.ParameterType.HalfPagePrintSizeConstants, new ParameterFilter(string.Empty, 0, 0, 0));
            try
            {
                if (dtPaperSizeConstants != null && dtPaperSizeConstants.Rows.Count > 0 && dtPaperSizeConstants.Rows[0].ItemArray[0] != null && !string.IsNullOrEmpty(dtPaperSizeConstants.Rows[0].ItemArray[0].ToString()))
                    return dtPaperSizeConstants.Rows[0].ItemArray[0].ToString();
                else
                    return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void fncSaveReport(string strPath)
        {
            try
            {                
                string _sPathFilePDF = String.Empty;
                String v_mimetype;
                String v_encoding;
                String v_filename_extension;
                String[] v_streamids;
                Microsoft.Reporting.WinForms.Warning[] warnings;
                string _sSuggestedName = String.Empty;

                SetReportViewer();
                if (rptViewer.LocalReport.DataSources.Count > 0)
                {
                    byte[] byteViewer = rptViewer.LocalReport.Render("PDF", null, out v_mimetype, out v_encoding, out v_filename_extension, out v_streamids, out warnings);

                    FileStream newFile = new FileStream(strPath, FileMode.Create);
                    newFile.Write(byteViewer, 0, byteViewer.Length);
                    newFile.Close();
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                Common.LogException(ex);
            }
        }

        #endregion
    }


}
