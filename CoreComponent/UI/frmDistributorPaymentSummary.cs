using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CoreComponent.BusinessObjects;
using CoreComponent.Core.BusinessObjects;
using CoreComponent.MasterData.BusinessObjects;
using Ex = Microsoft.Office.Interop.Excel;


using System.Data.OleDb;

namespace CoreComponent.UI
{
    public partial class frmDistributorPaymentSummary : Form
    {
        string m_dirPath;
        public int RecordCount = 0;
        #region Constructors
        public frmDistributorPaymentSummary()
        {
            try
            {
                InitializeComponent();
                InitializeControls();
                m_dirPath = Environment.CurrentDirectory;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Events
        void CallValidations()
        {
            try
            {
                if (Validators.CheckForSelectedValue(cmbStatus.SelectedIndex))
                    Validators.SetErrorMessage(epUOM, cmbStatus, "VAL0002", lblMonth.Text);
                else
                    Validators.SetErrorMessage(epUOM, cmbStatus);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void DisplayHeaderCellInfo(ref Ex.Range xlCells)
        {
            List<DistributorPayment> lst = new List<DistributorPayment>();
            lst = DistributorPayment.GetDistributorPaymentColumn();// loc.GetDistributorPaymentColumn();

            for (int i = 0; i < lst.Count; i++)
            {
                xlCells[1, i + 1] = lst[i].Column_Name.ToString();
            }
        }

        /// <summary>
        /// Validate Text
        /// </summary>
        private void ValidatedText(Boolean yesNo)
        {
            epUOM.SetError(txtExportPath, string.Empty);
            epUOM.SetError(txtImportPath, string.Empty);

            if (rdoExport.Checked == true)
            {
                TextBox txt = txtExportPath;
                Label lbl = lblExportPath;
                ValidatedText(txt, lbl, yesNo);

            }
            else
            {
                TextBox txt = txtImportPath;
                Label lbl = lblImportPath;
                ValidatedText(txt, lbl, yesNo);
            }
        }
        /// <summary>
        /// Validate Text
        /// </summary>
        /// <param name="txt"></param>
        /// <param name="lbl"></param>
        private void ValidatedText(TextBox txt, Label lbl, Boolean yesNo)
        {
            bool isTextBoxEmpty = CoreComponent.Core.BusinessObjects.Validators.CheckForEmptyString(txt.Text.Length);
            if (isTextBoxEmpty == true && yesNo == false)
                epUOM.SetError(txt, Common.GetMessage("INF0019", lbl.Text.Trim().Substring(0, lbl.Text.Trim().Length - 2)));
            else
                epUOM.SetError(txt, string.Empty);
        }
        /// <summary>
        /// Validate Combo Box
        /// </summary>
        /// <param name="cmb"></param>
        /// <param name="lbl"></param>
        private void ValidateStatus(ComboBox cmb, Label lbl, Boolean yesNo)
        {
            epUOM.SetError(cmb, string.Empty);
        }
        /// <summary>
        /// Generate Location Contact Error 
        /// </summary>
        /// <returns></returns>
        private StringBuilder GenerateContactError()
        {
            StringBuilder sbError = new StringBuilder();
            if (epUOM.GetError(cmbStatus).Trim().Length > 0)
            {
                sbError.Append(epUOM.GetError(cmbStatus));
                sbError.AppendLine();
            }
            if (epUOM.GetError(cmbYear).Trim().Length > 0)
            {
                sbError.Append(epUOM.GetError(cmbYear));
                sbError.AppendLine();
            }
            if (epUOM.GetError(txtImportPath).Trim().Length > 0)
            {
                sbError.Append(epUOM.GetError(txtImportPath));
                sbError.AppendLine();
            }
            if (epUOM.GetError(txtExportPath).Trim().Length > 0)
            {
                sbError.Append(epUOM.GetError(txtExportPath));
                sbError.AppendLine();
            }
            return sbError;
        }

        static bool FileInUse(string path)
        {
            try
            {
                //Just opening the file as open/create
                //using (System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.OpenOrCreate))
                using (System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite, System.IO.FileShare.ReadWrite))
                {
                    fs.Flush();

                    fs.Close();
                    fs.Dispose();

                    //If required we can check for read/write by using fs.CanRead or fs.CanWrite
                }
                return false;
            }
            catch (System.IO.IOException ex)
            {
                //using (System.IO.FileStream fs = new System.IO.FileStream(path,System.IO.FileMode.Open,))
                //{
                //    fs.Close();
                //    fs.Dispose();
                //}

                //check if message is for a File IO
                string _message = ex.Message.ToString();
                if (_message.Contains("The process cannot access the file"))
                    return true;
                else
                    throw;
            }
        }

        void DistroyExcelInstance()
        {
            try
            {

                foreach (System.Diagnostics.Process c in System.Diagnostics.Process.GetProcesses())
                {
                    if (c.ProcessName.ToString() != string.Empty)
                    {
                        if (c.ProcessName.ToString().Contains("EXCEL"))
                        {
                            //MessageBox.Show(c.MainModule.ToString());
                            c.Kill();
                            //break;

                        }
                        //if (c.MainWindowTitle.Contains("EXCEL.EXE"))
                        //{
                        //    MessageBox.Show(c.ProcessName);
                        //    return;
                        //}
                    }
                }
            }
            catch (Exception ex1)
            {
            }
            finally
            {
            }

        }

        public bool ExportToExcel(string sFile, DataTable dt, int StartFrom, int RowCount)
        {
            Ex.Application exc = new Ex.Application();
            Ex.Workbooks workbooks = exc.Workbooks;
            Ex.Workbook workbook = exc.Workbooks.Add(Type.Missing);
            Ex.Sheets sheets = exc.Sheets;
            Ex._Worksheet worksheet = ((Ex._Worksheet)(sheets[1]));
            Ex.Range range1;
            worksheet.Name = "Distributor Payment Summary";
            Ex.Range xlCells;
            range1 = worksheet.Cells;
            xlCells = worksheet.Cells;

            worksheet.Columns.ColumnWidth = 15;
            worksheet.get_Range("A1", "K1").Font.Bold = true;


            worksheet.Activate();

            worksheet.Unprotect("");
            Ex.Range unProtectedRange = worksheet.get_Range("E2", "K9999");
            unProtectedRange.Locked = false;

            //worksheet.get_Range("A1", "K1").Font.Color=Color.White;
            worksheet.get_Range("A1", "K1").Font.ColorIndex = 0;
            worksheet.get_Range("A1", "K1").Interior.ColorIndex = 20;
            worksheet.get_Range("A1", "K1").Borders.ColorIndex = 0;

            ((Ex.Range)worksheet.Cells[2, "A"]).EntireColumn.Locked = true; //DistributorId
            ((Ex.Range)worksheet.Cells[2, "B"]).EntireColumn.Locked = true; //Amount
            ((Ex.Range)worksheet.Cells[2, "C"]).EntireColumn.Locked = true; //BonusId
            ((Ex.Range)worksheet.Cells[2, "D"]).EntireColumn.Locked = true; //BusinessMonth

            ((Ex.Range)worksheet.Cells[2, "F"]).EntireColumn.NumberFormat = "dd-mmm-yyyy";
            ((Ex.Range)worksheet.Cells[2, "I"]).EntireColumn.NumberFormat = "dd-mmm-yyyy";
            ((Ex.Range)worksheet.Cells[2, "H"]).EntireColumn.NumberFormat = "dd-mmm-yyyy";
            ((Ex.Range)worksheet.Cells[2, "D"]).EntireColumn.NumberFormat = "dd-mmm-yyyy"; //BusinessObjects Month

            xlCells.HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlLeft;

            DisplayHeaderCellInfo(ref xlCells);


            //string sFile = txtExportPath.Text + "\\DistributorPaymentSummary_" + k.ToString() + ".xls";



            ////////DataTable dt1 = new DataTable();
            ////////if (k == 0)
            ////////{
            ////////    var top100 = AllRecords.Take(500);

            ////////    dt1 = (DataTable)top100;
            ////////}
            ////////else
            ////////{
            ////////    var top400 = AllRecords.Skip(k * 500).Take(500);

            ////////    dt1 = (DataTable)top400;
            ////////}
            //
            int j = RecordCount;
            if (RecordCount != dt.Rows.Count)
            {
                RecordCount = RowCount + RecordCount;
                StartFrom = 0;

            }

            for (int i = StartFrom; i < dt.Rows.Count && i < RowCount; i++)
            {

                xlCells[i + 2, 1] = dt.Rows[j][0].ToString();
                xlCells[i + 2, 2] = dt.Rows[j][1].ToString();
                xlCells[i + 2, 3] = dt.Rows[j][2].ToString();
                xlCells[i + 2, 4] = dt.Rows[j][3].ToString();
                xlCells[i + 2, 5] = dt.Rows[j][4].ToString();
                xlCells[i + 2, 6] = dt.Rows[j][5].ToString();
                xlCells[i + 2, 7] = dt.Rows[j][6].ToString();
                xlCells[i + 2, 8] = dt.Rows[j][7].ToString();
                xlCells[i + 2, 9] = dt.Rows[j][8].ToString();
                xlCells[i + 2, 10] = dt.Rows[j][9].ToString();
                xlCells[i + 2, 11] = dt.Rows[j][10].ToString();


                //((Ex.Range)worksheet.Cells[i + 2, "A"]).Borders.ColorIndex = 0;
                //((Ex.Range)worksheet.Cells[i + 2, "B"]).Borders.ColorIndex = 0;
                //((Ex.Range)worksheet.Cells[i + 2, "C"]).Borders.ColorIndex = 0;
                //((Ex.Range)worksheet.Cells[i + 2, "D"]).Borders.ColorIndex = 0;
                //((Ex.Range)worksheet.Cells[i + 2, "A"]).Cells.Interior.ColorIndex = 19;
                //((Ex.Range)worksheet.Cells[i + 2, "B"]).Cells.Interior.ColorIndex = 19;
                //((Ex.Range)worksheet.Cells[i + 2, "C"]).Cells.Interior.ColorIndex = 19;
                //((Ex.Range)worksheet.Cells[i + 2, "D"]).Cells.Interior.ColorIndex = 19;
                j++;
            }

            Ex.Range protectedRange = worksheet.get_Range("A2", "D9999");
            protectedRange.Locked = true;
            worksheet.Protect("", Type.Missing, Type.Missing, Type.Missing, Type.Missing, false, false, false, false, false, false, false, false, false, false, Type.Missing);
            //worksheet.Protect("", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            //workbook.SaveCopyAs(sFile);

            if (System.IO.File.Exists(sFile))
            {
                System.IO.File.Delete(sFile);
            }
            //if (!FileInUse(sFile))
            //{
            workbook.SaveAs(sFile, Ex.XlFileFormat.xlWorkbookNormal, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Ex.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            //}


            exc.Quit();


            System.Runtime.InteropServices.Marshal.ReleaseComObject(exc);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(sheets);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(range1);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workbooks);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlCells);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);

            return true;

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateStatus(cmbStatus, lblMonth, false);
                ValidateStatus(cmbYear, lblYear, false);

                ValidatedText(false);

                DistroyExcelInstance();

                #region Check Location Errors
                StringBuilder sbError = new StringBuilder();
                sbError = GenerateContactError();
                #endregion

                if (!sbError.ToString().Trim().Equals(string.Empty))
                {
                    MessageBox.Show(sbError.ToString(), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (rdoExport.Checked == true && rdoImport.Checked == false)
                {

                    DataTable dt = new DataTable();
                    DistributorBusinessMonth dp = new DistributorBusinessMonth();
                    dp.Month = (cmbStatus.SelectedValue.ToString().Length < 2 ? "0" + cmbStatus.SelectedValue.ToString() : cmbStatus.SelectedValue.ToString());
                    dp.Year = cmbYear.SelectedValue.ToString();




                    dt = DistributorPayment.GetDistributorPaymentColumnValue(Common.ToXml(dp));// loc.GetDistributorPaymentColumn();
                    //var AllRecords = from item in dt.AsEnumerable() select item;
                    //List<DataRow> dr = dt.AsEnumerable().ToList();
                    //for (int k = 0; k < dt.Rows.Count / 500; k++)
                    //{
                    if (dt.Rows.Count % 2 == 0)
                    {
                        int TotalRecordsPerFile = dt.Rows.Count / 2;

                        for (int i = 0; i < 2; i++)
                        {

                            string sFile = txtExportPath.Text + "\\DistributorPaymentSummary" + (i + 1) + ".xls";
                            ExportToExcel(sFile, dt, i * TotalRecordsPerFile, TotalRecordsPerFile);


                            //{
                            //MessageBox.Show(Common.GetMessage("8012", "exported"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //}
                            //else { }
                            //MessageBox.Show(Common.GetMessage("8014"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }


                    }
                    else
                    {

                        int TotalRecordsPerFile = dt.Rows.Count / 2;
                        int TotalRcd = dt.Rows.Count - (TotalRecordsPerFile + TotalRecordsPerFile);

                        for (int i = 0; i < 2; i++)
                        {

                            string sFile = txtExportPath.Text + "\\DistributorPaymentSummary" + (i + 1) + ".xls";
                            ExportToExcel(sFile, dt, i * TotalRecordsPerFile, TotalRecordsPerFile);

                                                        
                        }
                        for (int i = 0; i < 1; i++)
                        {

                            string sFile = txtExportPath.Text + "\\DistributorPaymentSummary" + (i + 3) + ".xls";
                            ExportToExcel(sFile, dt, i * TotalRcd, TotalRcd);


                        }

                    }
                    /*else if (dt.Rows.Count % 3 == 0)
                    {
                        int TotalRecordsPerFile = dt.Rows.Count / 3;

                        for (int i = 0; i < 3; i++)
                        {

                            string sFile = txtExportPath.Text + "\\DistributorPaymentSummary" + (i + 1) + ".xls";
                            ExportToExcel(sFile, dt, i * TotalRecordsPerFile, TotalRecordsPerFile);


                            //{
                            //MessageBox.Show(Common.GetMessage("8012", "exported"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //}
                            //else { }
                            //MessageBox.Show(Common.GetMessage("8014"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }


                    }
                    else
                    {
                        MessageBox.Show("GONE CASE");
                    }
                    */

                    //for (int i = 0; i < cnt; i++)
                    //{
                    //    ((Ex.Range)worksheet.Cells[i + 2, "A"]).Borders.ColorIndex = 0;
                    //    ((Ex.Range)worksheet.Cells[i + 2, "B"]).Borders.ColorIndex = 0;
                    //    ((Ex.Range)worksheet.Cells[i + 2, "C"]).Borders.ColorIndex = 0;
                    //    ((Ex.Range)worksheet.Cells[i + 2, "D"]).Borders.ColorIndex = 0;
                    //    ((Ex.Range)worksheet.Cells[i + 2, "A"]).Cells.Interior.ColorIndex = 19;
                    //    ((Ex.Range)worksheet.Cells[i + 2, "B"]).Cells.Interior.ColorIndex = 19;
                    //    ((Ex.Range)worksheet.Cells[i + 2, "C"]).Cells.Interior.ColorIndex = 19;
                    //    ((Ex.Range)worksheet.Cells[i + 2, "D"]).Cells.Interior.ColorIndex = 19;
                    //}





                    GC.Collect();
                    GC.WaitForPendingFinalizers();

                    //protectedRange.Locked = true;
                    //worksheet.Protect("", Type.Missing, Type.Missing, Type.Missing, Type.Missing, false, false, false, false, false, false, false, false, false, false, Type.Missing);

                    //exc.Workbooks.Close();


                    DistroyExcelInstance();
                    System.IO.Directory.SetCurrentDirectory(m_dirPath);
                    //}

                }
                else
                {
                    DateTime dt;
                    List<DistributorPayment> lst = new List<DistributorPayment>();

                    Ex.Application exc = new Ex.Application();
                    Ex.Workbooks workbooks = exc.Workbooks;
                    Ex.Workbook theWorkbook = workbooks.Open(txtImportPath.Text, 0, true, 5, "", "", true, Ex.XlPlatform.xlWindows, "\t", false, false, 0, true, false, false);

                    Ex.Sheets sheets = theWorkbook.Worksheets;
                    Ex.Worksheet worksheet = (Ex.Worksheet)sheets.get_Item(1);

                    //For Import We need to Unprotect the Sheet First
                    worksheet.Unprotect("");
                    Ex.Range unProtectedRange = worksheet.get_Range("A2", "K9999");
                    unProtectedRange.Locked = false;

                    for (int i = 2; i <= worksheet.Rows.CurrentRegion.Count / 11; i++)
                    {
                        Ex.Range range = worksheet.get_Range("A" + i.ToString(), "K" + i.ToString());
                        System.Array myvalues = (System.Array)range.Cells.Value2;

                        DistributorPayment dp = new DistributorPayment(); ;
                        if (((object[,])(myvalues))[1, 1] != null)
                            dp.DistributorId = ((object[,])(myvalues))[1, 1].ToString();

                        if (((object[,])(myvalues))[1, 2] != null)
                            dp.Amount = ((object[,])(myvalues))[1, 2].ToString();

                        if (((object[,])(myvalues))[1, 3] != null)
                            dp.BonusId = ((object[,])(myvalues))[1, 3].ToString();

                        if (((object[,])(myvalues))[1, 4] != null)
                        {
                            dt = DateTime.FromOADate(Convert.ToDouble(((object[,])(myvalues))[1, 4]));
                            dp.BusinessMonth = dt.Year.ToString() + (dt.Month.ToString().Length < 2 ? "0" + dt.Month.ToString() : dt.Month.ToString()) + (dt.Day.ToString().Length < 2 ? "0" + dt.Day.ToString() : dt.Day.ToString());
                        }
                        if (((object[,])(myvalues))[1, 5] != null)
                            dp.ChequeNo = ((object[,])(myvalues))[1, 5].ToString();

                        if ((((object[,])(myvalues))[1, 6]) != null)
                        {
                            dt = DateTime.FromOADate(Convert.ToDouble(((object[,])(myvalues))[1, 6]));// DateTime.FromOADate(dp.PaymentDate);
                            dp.PaymentDate = dt.Year.ToString() + (dt.Month.ToString().Length < 2 ? "0" + dt.Month.ToString() : dt.Month.ToString()) + (dt.Day.ToString().Length < 2 ? "0" + dt.Day.ToString() : dt.Day.ToString());
                        }

                        if (((object[,])(myvalues))[1, 7] != null)
                            dp.BankName = ((object[,])(myvalues))[1, 7].ToString();

                        if (((object[,])(myvalues))[1, 8] != null)
                        {
                            dt = DateTime.FromOADate(Convert.ToDouble(((object[,])(myvalues))[1, 8]));
                            dp.ChequeIssueDate = dt.Year.ToString() + (dt.Month.ToString().Length < 2 ? "0" + dt.Month.ToString() : dt.Month.ToString()) + (dt.Day.ToString().Length < 2 ? "0" + dt.Day.ToString() : dt.Day.ToString());
                        }

                        if (((object[,])(myvalues))[1, 9] != null)
                        {
                            dt = DateTime.FromOADate(Convert.ToDouble(((object[,])(myvalues))[1, 9]));
                            dp.ChequeExpiryDate = dt.Year.ToString() + (dt.Month.ToString().Length < 2 ? "0" + dt.Month.ToString() : dt.Month.ToString()) + (dt.Day.ToString().Length < 2 ? "0" + dt.Day.ToString() : dt.Day.ToString());
                        }

                        if (((object[,])(myvalues))[1, 10] != null)
                            dp.FirstName = ((object[,])(myvalues))[1, 10].ToString();

                        if (((object[,])(myvalues))[1, 11] != null)
                            dp.LastName = ((object[,])(myvalues))[1, 11].ToString();

                        if (((object[,])(myvalues))[1, 1] != null)
                            lst.Add(dp);
                    }

                    String errorMessage = string.Empty;
                    exc.Workbooks.Close();
                    DistributorPayment.Save(Common.ToXml(lst), ref errorMessage);

                    System.IO.Directory.SetCurrentDirectory(m_dirPath);
                    if (errorMessage.Equals(string.Empty))
                        MessageBox.Show(Common.GetMessage("8012", "imported"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show(Common.GetMessage(errorMessage), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                System.IO.Directory.SetCurrentDirectory(m_dirPath);
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult = DialogResult.Cancel;
                VisitControls visitControls = new VisitControls();
                //visitControls.ResetAllControlsInPanel(epUOM);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                ResetControls();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Methods


        void InitializeControls()
        {
            try
            {
                DataTable dtMonth = Common.ParameterLookup(Common.ParameterType.AllMonths, new ParameterFilter("", 0, 0, 0));
                if (dtMonth.Rows.Count > 0)
                {
                    cmbStatus.DataSource = dtMonth;
                    cmbStatus.DisplayMember = "MonthName";
                    cmbStatus.ValueMember = "MonthId";
                }


                DataTable dtYear = Common.ParameterLookup(Common.ParameterType.YearsForInvoice, new ParameterFilter("", 0, 0, 0));
                if (dtYear.Rows.Count > 0)
                {
                    cmbYear.DataSource = dtYear;
                    cmbYear.DisplayMember = "YearName";
                    cmbYear.ValueMember = "YearId";
                }
                ExportClick();
                rdoExport.Checked = true;
                rdoImport.Checked = false;
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        void ResetControls()
        {
            try
            {
                if (cmbStatus.Items.Count > 0)
                    cmbStatus.SelectedValue = 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        String GetErrorMessages()
        {
            try
            {
                StringBuilder sbError = new StringBuilder();
                Validators.AppendToStringBuilder(Validators.GetErrorMessage(epUOM, cmbStatus), ref sbError);

                return sbError.ToString().Replace(Environment.NewLine + Environment.NewLine, String.Empty).Trim();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        private void btnExport_Click(object sender, EventArgs e)
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

        private void btnImportPath_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();

                ofd.InitialDirectory = @"C:\";

                ofd.Filter = "Excel files (*.xls)|*.xls";

                if (ofd.ShowDialog() == DialogResult.OK)
                    txtImportPath.Text = ofd.FileName;
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void rdoExport_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                ExportClick();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void rdoImport_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                ImportClick();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        void ExportClick()
        {
            txtExportPath.Enabled = true;
            btnExport.Enabled = true;
            txtImportPath.Enabled = false;
            btnImportPath.Enabled = false;
            cmbStatus.Enabled = true;
            cmbYear.Enabled = true;
        }
        void ImportClick()
        {
            txtExportPath.Enabled = false;
            btnExport.Enabled = false;
            txtImportPath.Enabled = true;
            btnImportPath.Enabled = true;
            cmbStatus.Enabled = false;
            cmbYear.Enabled = false;
        }

        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ValidateStatus(cmbStatus, lblMonth, true);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ValidateStatus(cmbYear, lblYear, true);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtImportPath_Validated(object sender, EventArgs e)
        {
            try
            {
                ValidatedText(true);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtExportPath_Validated(object sender, EventArgs e)
        {
            try
            {
                ValidatedText(true);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtExportPath_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ValidatedText(true);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtImportPath_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ValidatedText(true);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }


}
