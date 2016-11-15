using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CoreComponent.Core.BusinessObjects;
using System.Web.UI.WebControls;
using Ex = Microsoft.Office.Interop.Excel;
using CoreComponent.BusinessObject;
using Vinculum.Framework.DataTypes;
using Vinculum.Framework.Data;
using CoreComponent.BusinessObjects;
using XLUtilites;



namespace CoreComponent.UI
{
    public partial class ImportExcel : Form
    {
        string m_dirPath;
        private const string SP_UPDATE_XLS_DATA = "usp_UpdateExcelData";// "usp_UpdateDataFromExcel";
        string data;
        string path;
        public ImportExcel()
        {
            InitializeComponent();
            PoplateCombo();
            m_dirPath = Environment.CurrentDirectory;
            //LoadMonth();
            InitializeControls();
        }

        void InitializeControls()
        {
            try
            {
                DataTable dtMonth = Common.ParameterLookup(Common.ParameterType.AllMonths, new ParameterFilter("", 0, 0, 0));
                if (dtMonth.Rows.Count > 0)
                {
                    cmbMonth.DataSource = dtMonth;
                    cmbMonth.DisplayMember = "MonthName";
                    cmbMonth.ValueMember = "MonthId";
                }


                DataTable dtYear = Common.ParameterLookup(Common.ParameterType.YearsForBonus, new ParameterFilter("", 0, 0, 0));
                if (dtYear.Rows.Count > 0)
                {
                    cmbYear.DataSource = dtYear;
                    cmbYear.DisplayMember = "YearName1";
                    cmbYear.ValueMember = "YearId1";
                }
              

                DataTable dtPaymentMode = Common.ParameterLookup(Common.ParameterType.DistPaymentMode, new ParameterFilter("", 0, 0, 0));
                if (dtPaymentMode.Rows.Count > 0)
                {
                    cmbPaymentMode.DataSource = dtPaymentMode;
                    cmbPaymentMode.DisplayMember = "PaymentModeText";
                    cmbPaymentMode.ValueMember = "PaymentModeId";
                }
                ExportClick();
                rdbExport.Checked = true;
                rdbImport.Checked = false;

            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void ExportClick()
        {
            rdbImport.Checked = false;
            cmbMonth.Enabled = true;
            cmbYear.Enabled = true;
            cmbDataType.Enabled = false;
            //cmbDataType.SelectedValue = "-1";
            txtImportPath.Enabled = false;
            btnImportPath.Enabled = false;
            btnUpload.Enabled = false;
            txtExportPath.Enabled = false;
            btnShowDialogForExport.Enabled = true;
            btnExportToExcel.Enabled = true;
            cmbPaymentMode.Enabled = true;
        }

        void ImportClick()
        {
            rdbExport.Checked = false;
            cmbMonth.Enabled = false;
            cmbYear.Enabled = false;
            cmbDataType.Enabled = true;
            txtImportPath.Enabled = true;
            btnImportPath.Enabled = true;
            btnUpload.Enabled = true;
            txtExportPath.Enabled = false;
            btnShowDialogForExport.Enabled = false;
            btnExportToExcel.Enabled = false;
            cmbPaymentMode.Enabled = false;
            //cmbPaymentMode.SelectedValue = "-1";
        }

        private void PoplateCombo()
        {
            List<ListItem> items = new List<ListItem>();
            ListItem li = new ListItem();
            li.Text = "PAN";
            li.Value = "1";
            items.Add(li);

            ListItem li1 = new ListItem();
            li1.Text = "BANK";
            li1.Value = "2";
            items.Add(li1);

            ListItem li2 = new ListItem();
            li2.Text = "Cheque No";
            li2.Value = "3";

            items.Add(li2);


            cmbDataType.DataSource = items;
            cmbDataType.DisplayMember = "Text";
            cmbDataType.ValueMember = "Value";

        }    

        private void cmbDataType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDataType.SelectedValue.ToString() == "3")
            {

                cmbMonth.Enabled = true;

            }
            else
            {
                cmbMonth.Enabled = false;

            }
        }  

        private void rdbImport_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbImport.Checked)
            {
                ImportClick();
            }
        }

        private void rdbExport_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbExport.Checked)
            {
                ExportClick();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region Import

        private void btnImportPath_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();

                ofd.InitialDirectory = @"C:\";



                ofd.Filter = "All files (*.xls)|*.xls|All files (*.*)|*.*";//"Excel files (*.xls)|*.xls | (*.xlsx)|*.xlsx";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtImportPath.Text = ofd.FileName;
                    path = ofd.FileName;
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                string errorMessage = string.Empty;
                if (Convert.ToInt32(cmbDataType.SelectedValue.ToString()) == 1)
                {
                    string BusinessDate = string.Empty;
                    string xmlData = XlToXMLGenerator.GetExcelAsXML(txtImportPath.Text, true);
                    UpdateExcelData(xmlData, Convert.ToInt32(cmbDataType.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), Convert.ToInt32(cmbYear.SelectedValue.ToString()), SP_UPDATE_XLS_DATA,
                             ref  errorMessage);
                }

                else if (Convert.ToInt32(cmbDataType.SelectedValue.ToString()) == 2)
                {
                    string BusinessDate = string.Empty;
                    string xmlData = XlToXMLGenerator.GetExcelAsXML(txtImportPath.Text, true);
                    UpdateExcelData(xmlData, Convert.ToInt32(cmbDataType.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), Convert.ToInt32(cmbYear.SelectedValue.ToString()), SP_UPDATE_XLS_DATA,
                             ref  errorMessage);

                }

                else if (Convert.ToInt32(cmbDataType.SelectedValue.ToString()) == 3)
                {
                    string BusinessDate = string.Empty;
                    string xmlData = XlToXMLGenerator.GetExcelAsXML(txtImportPath.Text, true);
                    UpdateExcelData(xmlData, Convert.ToInt32(cmbDataType.SelectedValue.ToString()),  Convert.ToInt32(cmbMonth.SelectedValue.ToString()), Convert.ToInt32(cmbYear.SelectedValue.ToString()), SP_UPDATE_XLS_DATA,
                             ref  errorMessage);

                }
            }

            catch (Exception ex)
            {
                System.IO.Directory.SetCurrentDirectory(m_dirPath);
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        #endregion

        #region Export
        private void btnShowDialogForExport_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog fbd = new FolderBrowserDialog();

                if (fbd.ShowDialog() == DialogResult.OK)
                    txtExportPath.Text = fbd.SelectedPath;
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        void DTableToExcel(DataTable dt)
        {
            //string fileName =     "C:\\DistributorPaymentSummary"  + SetExcelNameSuffix() + ".xls";
            //fileName = "C:\\DistributorPaymentSummary_AK.xlsx";

            if (dt.Rows.Count > 0)
            {
                string fileName = txtExportPath.Text + "\\" + "DistributorPaymentSummary" + SetExcelNameSuffix() + ".xls";
                //  ExcelWriter.ExcelExport(dt, fileName, false, SetSheetNameSuffix());
                ExlGenerator.ExportXL(dt, fileName, false, SetSheetNameSuffix());
            }
            else
            {
                // MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);

                MessageBox.Show("No record is found.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtExportPath.Text.Trim()))
            {
                MessageBox.Show("Select Directory to export.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (cmbPaymentMode.SelectedValue.ToString() == Common.INT_DBNULL.ToString())
            {
                MessageBox.Show("Select payment mode to export.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string errorMessage = string.Empty;
                CoreComponent.BusinessObject.DistributorBusinessMonth dp = new CoreComponent.BusinessObject.DistributorBusinessMonth();

                dp.Month = cmbMonth.SelectedValue.ToString();
                dp.Year = cmbYear.SelectedValue.ToString();
                dp.PaymentMode = cmbPaymentMode.SelectedValue.ToString();

                string xmlDoc = Common.ToXml(dp);

                DTableToExcel(GetDataTableForExcel(xmlDoc, "usp_GetDistributorBonusPayments", ref  errorMessage));
            }
        }

        public DataTable UpdateExcelData(string xmlDoc, int mode, int month, int year , string spName, ref string errorMessage)
        {
            DBParameterList dbParam;


            try
            {
                Vinculum.Framework.Data.DataTaskManager dtManager = new DataTaskManager();

                // initialize the parameter list object
                dbParam = new DBParameterList();

                // add the relevant 3 parameters
                dbParam.Add(new DBParameter(Common.PARAM_MODE, mode, DbType.Int32));
                dbParam.Add(new DBParameter(Common.PARAM_DATA, xmlDoc, DbType.String));
                dbParam.Add(new DBParameter(Common.PARAM_DATA2, month.ToString(), DbType.String));
                dbParam.Add(new DBParameter(Common.PARAM_DATA3, year.ToString(), DbType.String));
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                // executing procedure to save the record 
                DataTable dt = dtManager.ExecuteDataTable(spName, dbParam);

                // update database message
                errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();

                // if an error returned from the database
                if (errorMessage != string.Empty)
                    return null;
                else
                {
                    return dt;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetDataTableForExcel(string xmlDoc, string spName, ref string errorMessage)
        {
            System.Data.DataTable dTable = new DataTable();
            dTable = GetSelectedRecords(xmlDoc, spName, ref errorMessage);
            return dTable;
        }

        public static DataTable GetSelectedRecords(string xmlDoc, string spName, ref string errorMessage)
        {
            DBParameterList dbParam;
            try
            {
                Vinculum.Framework.Data.DataTaskManager dtManager = new DataTaskManager();

                // initialize the parameter list object
                dbParam = new DBParameterList();

                // add the relevant 2 parameters
                dbParam.Add(new DBParameter(Common.PARAM_DATA, xmlDoc, DbType.String));
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                // executing procedure to save the record 
                DataTable dt = dtManager.ExecuteDataTable(spName, dbParam);

                // update database message
                errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();

                // if an error returned from the database
                if (errorMessage != string.Empty)
                    return null;
                else
                {
                    return dt;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        private string SetExcelNameSuffix()
        {
            string XLsheetNameSuffix = "";

            switch (cmbPaymentMode.SelectedValue.ToString())
            {

                case "2":
                    XLsheetNameSuffix = "_Hold";

                    break;

                case "3":
                    XLsheetNameSuffix = "_Transfer";
                    break;


                case "4":
                    XLsheetNameSuffix = "_Cheque";
                    break;


                case "5":
                    XLsheetNameSuffix = "_Payorder";
                    break;
            }

            return XLsheetNameSuffix;
        }

        private string SetSheetNameSuffix()
        {
            string XLsheetSuffix = "";

            switch (cmbPaymentMode.SelectedValue.ToString())
            {

                case "2":
                    XLsheetSuffix = "Hold";

                    break;

                case "3":
                    XLsheetSuffix = "Transfer";
                    break;


                case "4":
                    XLsheetSuffix = "Cheque";
                    break;


                case "5":
                    XLsheetSuffix = "Payorder";
                    break;


            }

            return XLsheetSuffix;
        }

        #region Not in Use
        private void txtImportPath_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblImportPath_Click(object sender, EventArgs e)
        {

        }

        private void btnUpload_Click33(object sender, EventArgs e)
        {
            try
            {

                //ValidateDataType(cmbDataType, lblDataFileType, false);
                //if (!sbError.ToString().Trim().Equals(string.Empty))
                //{
                //    MessageBox.Show(sbError.ToString(), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    return;
                //}


                DateTime dt;

                string errorMessage = string.Empty;
                Ex.Application exc = new Ex.Application();
                Ex.Workbooks workbooks = exc.Workbooks;
                Ex.Workbook theWorkbook = workbooks.Open(txtImportPath.Text, 0, true, 5, "", "", true, Ex.XlPlatform.xlWindows, "\t", false, false, 0, true, false, false);

                Ex.Sheets sheets = theWorkbook.Worksheets;
                Ex.Worksheet worksheet = (Ex.Worksheet)sheets.get_Item(1);

                //For Import We need to Unprotect the Sheet First
                worksheet.Unprotect("");
                Ex.Range unProtectedRange = worksheet.get_Range("A2", "K9999");
                unProtectedRange.Locked = false;

                if (Convert.ToInt32(cmbDataType.SelectedValue.ToString()) == 1)
                {
                    List<PAN> lst = new List<PAN>();
                    for (int i = 2; i <= worksheet.Rows.CurrentRegion.Count / 2; i++)
                    {

                        Ex.Range range = worksheet.get_Range("A" + i.ToString(), "B" + i.ToString());
                        System.Array myvalues = (System.Array)range.Cells.Value2;

                        PAN dp = new PAN();
                        if (((object[,])(myvalues))[1, 1] != null)
                            dp.DistributorId = ((object[,])(myvalues))[1, 1].ToString();

                        if (((object[,])(myvalues))[1, 2] != null)
                            dp.PanNo = ((object[,])(myvalues))[1, 2].ToString();

                        if (((object[,])(myvalues))[1, 1] != null)
                            lst.Add(dp);

                    }
                    string BusinessDate = string.Empty;
                    //if (dtpBusinessMonth.Enabled == true)
                    //{
                    //    DateTime dtBusinessDate = dtpBusinessMonth.Checked == true ? Convert.ToDateTime(dtpBusinessMonth.Value) : Common.DATETIME_NULL;
                    //    BusinessDate = Convert.ToDateTime(dtBusinessDate).ToString(Common.DATE_TIME_FORMAT);
                    //}


                    //WorkbookXmlImportEvents();
                    UpdateExcelData(Common.ToXml(lst), Convert.ToInt32(cmbDataType.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), Convert.ToInt32(cmbYear.SelectedValue.ToString()), SP_UPDATE_XLS_DATA,
                                ref  errorMessage);
                }

                else if (Convert.ToInt32(cmbDataType.SelectedValue.ToString()) == 2)
                {
                    List<BANK> lst = new List<BANK>();

                    for (int i = 2; i <= worksheet.Rows.CurrentRegion.Count / 5; i++)
                    {
                        Ex.Range range = worksheet.get_Range("A" + i.ToString(), "E" + i.ToString());
                        System.Array myvalues = (System.Array)range.Cells.Value2;

                        BANK dp = new BANK();
                        if (((object[,])(myvalues))[1, 1] != null)
                            dp.DistributorId = ((object[,])(myvalues))[1, 1].ToString();

                        if (((object[,])(myvalues))[1, 2] != null)
                            dp.DistributorBankName = ((object[,])(myvalues))[1, 2].ToString();

                        if (((object[,])(myvalues))[1, 3] != null)
                            dp.DistributorBankBranch = ((object[,])(myvalues))[1, 3].ToString();


                        if (((object[,])(myvalues))[1, 4] != null)
                            dp.DistributorBankAccNumber = ((object[,])(myvalues))[1, 4].ToString();


                        if (((object[,])(myvalues))[1, 5] != null)
                            dp.BankID = ((object[,])(myvalues))[1, 5].ToString();

                        if (((object[,])(myvalues))[1, 1] != null)
                            lst.Add(dp);


                    }
                    string BusinessDate = string.Empty;
                    //if (dtpBusinessMonth.Enabled == true)
                    //{
                    //    DateTime dtBusinessDate = dtpBusinessMonth.Checked == true ? Convert.ToDateTime(dtpBusinessMonth.Value) : Common.DATETIME_NULL;
                    //    BusinessDate = Convert.ToDateTime(dtBusinessDate).ToString(Common.DATE_TIME_FORMAT);
                    //}


                    //WorkbookXmlImportEvents();
                    UpdateExcelData(Common.ToXml(lst), Convert.ToInt32(cmbDataType.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), Convert.ToInt32(cmbYear.SelectedValue.ToString()), SP_UPDATE_XLS_DATA,
                                ref  errorMessage);
                }


                else if (Convert.ToInt32(cmbDataType.SelectedValue.ToString()) == 3)
                {
                    List<ChequeInformation> lst = new List<ChequeInformation>();

                    for (int i = 2; i <= worksheet.Rows.CurrentRegion.Count / 12; i++)
                    {
                        Ex.Range range = worksheet.get_Range("A" + i.ToString(), "L" + i.ToString());
                        System.Array myvalues = (System.Array)range.Cells.Value2;

                        ChequeInformation dp = new ChequeInformation(); ;
                        if (((object[,])(myvalues))[1, 1] != null)
                            dp.DistributorId = ((object[,])(myvalues))[1, 1].ToString();

                        if (((object[,])(myvalues))[1, 3] != null)
                            dp.ChequeNumber = ((object[,])(myvalues))[1, 3].ToString();

                        if (((object[,])(myvalues))[1, 4] != null)
                            dp.BonusId = ((object[,])(myvalues))[1, 4].ToString();

                        if (((object[,])(myvalues))[1, 5] != null)
                            dp.PaymentDate = ((object[,])(myvalues))[1, 5].ToString();

                        if (((object[,])(myvalues))[1, 6] != null)
                            dp.BankName = ((object[,])(myvalues))[1, 6].ToString();

                        if (((object[,])(myvalues))[1, 7] != null)
                            dp.ChqIssueDate = ((object[,])(myvalues))[1, 8].ToString();

                        if (((object[,])(myvalues))[1, 8] != null)
                            dp.ChqExpiryDate = ((object[,])(myvalues))[1, 8].ToString();

                        if (((object[,])(myvalues))[1, 1] != null)
                            lst.Add(dp);


                    }
                    string BusinessDate = string.Empty;
                    //if (dtpBusinessMonth.Enabled == true)
                    //{
                    //    DateTime dtBusinessDate = dtpBusinessMonth.Checked == true ? Convert.ToDateTime(dtpBusinessMonth.Value) : Common.DATETIME_NULL;
                    //    BusinessDate = Convert.ToDateTime(dtBusinessDate).ToString(Common.DATE_TIME_FORMAT);
                    //}


                    //WorkbookXmlImportEvents();
                    UpdateExcelData(Common.ToXml(lst), Convert.ToInt32(cmbDataType.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), Convert.ToInt32(cmbYear.SelectedValue.ToString()), SP_UPDATE_XLS_DATA,
                                ref  errorMessage);
                }



                #region  Commented
                //    if (((object[,])(myvalues))[1, 3] != null)
                //        dp.BonusId = ((object[,])(myvalues))[1, 3].ToString();

                //    if (((object[,])(myvalues))[1, 4] != null)
                //    {
                //        dt = DateTime.FromOADate(Convert.ToDouble(((object[,])(myvalues))[1, 4]));
                //        dp.BusinessMonth = dt.Year.ToString() + (dt.Month.ToString().Length < 2 ? "0" + dt.Month.ToString() : dt.Month.ToString()) + (dt.Day.ToString().Length < 2 ? "0" + dt.Day.ToString() : dt.Day.ToString());
                //    }
                //    if (((object[,])(myvalues))[1, 5] != null)
                //        dp.ChequeNo = ((object[,])(myvalues))[1, 5].ToString();

                //    if ((((object[,])(myvalues))[1, 6]) != null)
                //    {
                //        dt = DateTime.FromOADate(Convert.ToDouble(((object[,])(myvalues))[1, 6]));// DateTime.FromOADate(dp.PaymentDate);
                //        dp.PaymentDate = dt.Year.ToString() + (dt.Month.ToString().Length < 2 ? "0" + dt.Month.ToString() : dt.Month.ToString()) + (dt.Day.ToString().Length < 2 ? "0" + dt.Day.ToString() : dt.Day.ToString());
                //    }

                //    if (((object[,])(myvalues))[1, 7] != null)
                //        dp.BankName = ((object[,])(myvalues))[1, 7].ToString();

                //    if (((object[,])(myvalues))[1, 8] != null)
                //    {
                //        dt = DateTime.FromOADate(Convert.ToDouble(((object[,])(myvalues))[1, 8]));
                //        dp.ChequeIssueDate = dt.Year.ToString() + (dt.Month.ToString().Length < 2 ? "0" + dt.Month.ToString() : dt.Month.ToString()) + (dt.Day.ToString().Length < 2 ? "0" + dt.Day.ToString() : dt.Day.ToString());
                //    }

                //    if (((object[,])(myvalues))[1, 9] != null)
                //    {
                //        dt = DateTime.FromOADate(Convert.ToDouble(((object[,])(myvalues))[1, 9]));
                //        dp.ChequeExpiryDate = dt.Year.ToString() + (dt.Month.ToString().Length < 2 ? "0" + dt.Month.ToString() : dt.Month.ToString()) + (dt.Day.ToString().Length < 2 ? "0" + dt.Day.ToString() : dt.Day.ToString());
                //    }

                //    if (((object[,])(myvalues))[1, 10] != null)
                //        dp.FirstName = ((object[,])(myvalues))[1, 10].ToString();

                //    if (((object[,])(myvalues))[1, 11] != null)
                //        dp.LastName = ((object[,])(myvalues))[1, 11].ToString();

                //    if (((object[,])(myvalues))[1, 1] != null)
                //        lst.Add(dp);
                //} 
                #endregion

                // String errorMessage = string.Empty;
                exc.Workbooks.Close();


                System.IO.Directory.SetCurrentDirectory(m_dirPath);
                if (errorMessage.Equals(string.Empty))
                    MessageBox.Show(Common.GetMessage("8012", "imported"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show(Common.GetMessage(errorMessage), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            catch (Exception ex)
            {
                System.IO.Directory.SetCurrentDirectory(m_dirPath);
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpload_Click1(object sender, EventArgs e)
        {
            try
            {
                string errorMessage = string.Empty;
                Ex.Application app = new Ex.Application();
                app.Workbooks.Open(path,
                                   Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                   Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                   Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                   Type.Missing, Type.Missing);
                string data = string.Empty;
                Ex.XlXmlImportResult xml = app.ActiveWorkbook.XmlMaps[1].ImportXml(path, 1);
                app.Workbooks.Close();

                string BusinessDate = string.Empty;
                //if (dtpBusinessMonth.Enabled == true)
                //{
                //    DateTime dtBusinessDate = dtpBusinessMonth.Checked == true ? Convert.ToDateTime(dtpBusinessMonth.Value) : Common.DATETIME_NULL;
                //     BusinessDate = Convert.ToDateTime(dtBusinessDate).ToString(Common.DATE_TIME_FORMAT);
                //}


                //WorkbookXmlImportEvents();
                UpdateExcelData(xml.ToString(), Convert.ToInt32(cmbDataType.SelectedValue.ToString()), Convert.ToInt32(cmbMonth.SelectedValue.ToString()), Convert.ToInt32(cmbYear.SelectedValue.ToString()), SP_UPDATE_XLS_DATA,
                            ref  errorMessage);

            }
            catch (Exception ex)
            {
                System.IO.Directory.SetCurrentDirectory(m_dirPath);
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void ValidateDataType(ComboBox cmbDataType, System.Windows.Forms.Label lblDataFileType, bool p)
        {
            throw new NotImplementedException();
        }

        private void lblDataFileType_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void ImportExcel_Load(object sender, EventArgs e)
        {

        }

        private void cmbYear_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void lblBusiMonth_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbPaymentMode_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void cmbYear_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        #endregion

        
    }
}

namespace CoreComponent.BusinessObject
{
    [Serializable]
    public class PAN
    {
        public string DistributorId { get; set; }
        public string PanNo { get; set; }

    }
    [Serializable]
    public class BANK
    {
        public string DistributorId { get; set; }
        public string DistributorBankName { get; set; }
        public string DistributorBankBranch { get; set; }
        public string DistributorBankAccNumber { get; set; }
        public string BankID { get; set; }

        //public string CreatedDate { get; set; }
        // public int ModifiedBy { get; set; }
        // public string ModifiedDate { get; set; }
        //public int isprimary { get; set; }
    }

    [Serializable]
    public class ChequeInformation
    {
        public string DistributorId { get; set; }
        public string ChequeNumber { get; set; }
        public string BonusId { get; set; }
        public string PaymentDate { get; set; }
        public string BankName { get; set; }
        public string ChqIssueDate { get; set; }
        public string ChqExpiryDate { get; set; }

    }

    public class Data
    {
        public string Value;
        public string Text;
    }

    public class DistributorBusinessMonth
    {
        private string m_month = string.Empty;
        private string m_year = string.Empty;
        private string m_PaymentMode = string.Empty;

        public string Month
        {
            get { return m_month; }
            set { m_month = value; }
        }
        public string Year
        {
            get { return m_year; }
            set { m_year = value; }
        }

        public string PaymentMode
        {
            get { return m_PaymentMode; }
            set { m_PaymentMode = value; }
        }
    }


}



