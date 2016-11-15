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
//using java.io;
//using java.util;
//using java.util.zip;
using System.IO;
using System.Net;
using System.Collections;
using System.Data.OleDb;
using Excel = Microsoft.Office.Interop.Excel;
using Vinculum.Framework.Data;
using Vinculum.Framework.DataTypes;
using Microsoft.Office.Tools.Outlook;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.Office.Interop.Excel;
using System.Configuration;
using System.Diagnostics;
using AuthenticationComponent.BusinessObjects;

namespace CoreComponent.UI
{
    public partial class frmExecuteInOut : Form
    {
        string m_dirPath;
        string m_destPath = string.Empty;
        string m_invConsDate = string.Empty;
        string m_ftpLocation = string.Empty;
        string m_fileName = string.Empty;
        string m_userName = string.Empty;
        string m_password = string.Empty;
        string m_CashfileNames = string.Empty;
        bool m_distributorUploadFailed = false;
        private System.Windows.Forms.ErrorProvider epUOM;
        StringBuilder validationMessage;
        int m_bank = 0;
        int m_ccard = 0;
        int m_cheque = 0;
        int m_cash = 0;
        bool validationStatus;
        System.Collections.Hashtable m_distributorFileList;
        System.Collections.Hashtable m_invoiceFileList;

        #region CONSTANTS

        private const string CONST_JOB = "Job";
        private const string CONST_SCHEDULEDTASK = "Scheduled Task";
        private const string SP_EXCELDATA_SAVE = "usp_DCCDataImport";
        private const string SP_DBCDATA_SAVE = "usp_DBCDataSave";
        private const string SP_DayEndJobAudit = "usp_DayEndJobAudit";

        #endregion

        #region CONSTRUCTOR

        void InitializeDistributorHashTable()
        {

            m_distributorFileList = new System.Collections.Hashtable();
            m_distributorFileList.Add("1", "af.dbf");
            m_distributorFileList.Add("2", "afdet.dbf");
            m_distributorFileList.Add("3", "accno.dbf");
        }

        void InitializeInvoiceHashTable()
        {
            m_invoiceFileList = new System.Collections.Hashtable();
            m_invoiceFileList.Add("1", "bm.dbf");
            m_invoiceFileList.Add("2", "bs.dbf");
            m_invoiceFileList.Add("3", "loca.dbf");
            m_invoiceFileList.Add("4", "voucher.dbf");
            m_invoiceFileList.Add("5", "payorder.dbf");
            //m_invoiceFileList.Add("3", "cheque.dbf");
            //m_invoiceFileList.Add("3", "ccard.dbf");
            //m_invoiceFileList.Add("3", "bank.dbf");
            //m_invoiceFileList.Add("3", "cash.dbf");

        }

        void initializeFileType()
        {
            System.Data.DataTable dtIncrementalData = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter("INCREMENTALFILETYPE", 0, 0, 0));

            if (dtIncrementalData.Rows.Count > 0)
            {
                cmbFileType.DataSource = dtIncrementalData;
                cmbFileType.DisplayMember = "KeyValue1";
                cmbFileType.ValueMember = "KeyCode1";
                //cmbFileType.SelectedValue = "0";
            }

        }

        public frmExecuteInOut()
        {
            try
            {
                InitializeComponent();
                InitializeDistributorHashTable();
                initializeFileType();
                InitializeInvoiceHashTable();
                m_dirPath = Environment.CurrentDirectory;
                validationMessage = new StringBuilder();
                if (!Authenticate.LoggedInUser.UserId.Equals(1))
                {
                    grpBoxUpload.Enabled = false;
                    grpDBC.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region EVENTS

        private void frmExecuteInOut_Load(object sender, EventArgs e)
        {
            //if (Common.CurrentLocationTypeId == 1)
            //{
            //    grpBoxUpload.Visible = true;
            //}
            //else
            //{
            //    grpBoxUpload.Visible = false;
            //}            

            //imgProgress.Visible = false;
            dtpInvoiceConsiderDate.Format = DateTimePickerFormat.Custom;
            dtpInvoiceConsiderDate.CustomFormat = Common.DTP_DATE_FORMAT;
            lblDistributorInvoice.Visible = false;
            cmbFileType.Visible = false;
            lblExportPath.Visible = false;
            txtExportPath.Visible = false;
            btnExport.Visible = false;

        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            try
            {
                ExecuteInOutJob();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            try
            {
                Common.CloseThisForm(this);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();

                ofd.InitialDirectory = @"C:\";

                ofd.Filter = "Zip files (*.zip)|*.zip";

                if (ofd.ShowDialog() == DialogResult.OK)
                    txtExportPath.Text = ofd.FileName;
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                InitializeFTPVariables();

                if (txtExportPath.Text.Trim().Length > 0)
                {
                    List<string> downLoadFiles = GetFTPFileList();

                    if (downLoadFiles != null && downLoadFiles.Count > 0)
                    {
                        for (int i = 0; i < downLoadFiles.Count; i++)
                        {
                            if (downLoadFiles[i].ToLower().IndexOf(".dbf") >= 0)
                            {
                                if (downLoadFiles[i].ToLower() != "loca.dbf" && downLoadFiles[i].ToLower() != "location.dbf")
                                    DeleteFileAtFTP(downLoadFiles[i]);
                            }
                        }
                    }

                    if (UnzipAndUploadToFtp())
                    {

                        System.IO.Directory.SetCurrentDirectory(m_dirPath);

                        txtExportPath.Text = string.Empty;

                        String errorMessage = String.Empty;
                        m_distributorUploadFailed = false;
                        //Distributor
                        if (cmbFileType.SelectedValue.ToString() == "1")
                        {
                            ExecuteInOut objExecInOut = new ExecuteInOut();
                            if (objExecInOut.UploadDistributorMaster(m_destPath, ref errorMessage))
                            {
                                MessageBox.Show(Common.GetMessage("INF0055", "Distributors", "uploaded"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                m_distributorUploadFailed = true;
                                if (errorMessage.StartsWith("INVALID"))
                                {
                                    MessageBox.Show(Common.GetMessage("INF0010", ("DistributorID - " + errorMessage.Replace("INVALID ,", ""))), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                else if (errorMessage.StartsWith("MISSING"))
                                {
                                    MessageBox.Show(errorMessage.Trim(), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                else
                                    MessageBox.Show(Common.GetMessage("INF0218", "Distributor", " upload"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        errorMessage = String.Empty;
                        //Invoice
                        if (cmbFileType.SelectedValue.ToString() == "2")
                        {
                            if (m_distributorUploadFailed)
                            {
                                DialogResult dr = MessageBox.Show(Common.GetMessage("5015"), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (dr == DialogResult.No)
                                    return;
                            }
                            ExecuteInOut objExecInOut = new ExecuteInOut();
                            if (objExecInOut.UploadCI(m_destPath, m_cash, m_cheque, m_bank, m_ccard, m_invConsDate, m_CashfileNames.Substring(1), ref errorMessage))
                            {
                                MessageBox.Show(Common.GetMessage("INF0055", "Invoice", "uploaded"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                if (errorMessage == "INF0212")
                                {
                                    MessageBox.Show(Common.GetMessage("INF0212"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                else
                                    MessageBox.Show(Common.GetMessage("INF0218", "Invoice", "upload"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        //Pay Order - Bonus Cheque
                        if (cmbFileType.SelectedValue.ToString() == "3")
                        {
                            ExecuteInOut objExecInOut = new ExecuteInOut();
                            if (objExecInOut.UploadBonusCheque(m_destPath, dtpInvoiceConsiderDate.Value.ToString(), ref errorMessage))
                            {
                                MessageBox.Show(Common.GetMessage("INF0055", "Bonus Cheque", "uploaded"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show(Common.GetMessage("INF0218", "Bonus Cheque", " upload"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        errorMessage = String.Empty;

                        //GiftVoucher Upload
                        if (cmbFileType.SelectedValue.ToString() == "4")
                        {
                            ExecuteInOut objExecInOut = new ExecuteInOut();
                            if (objExecInOut.UploadGiftVoucher(m_destPath, dtpInvoiceConsiderDate.Value.ToString(), ref errorMessage))
                            {
                                MessageBox.Show(Common.GetMessage("INF0055", "Gift Vouchers", "uploaded"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show(Common.GetMessage("INF0218", "Gift Vouchers", " upload"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        errorMessage = String.Empty;

                        m_destPath = string.Empty;
                    }
                }
                if (txtdcc.Text.Trim().Length > 0)
                {
                    bool actionresult = ExceldataImport(txtdcc.Text);
                    if (actionresult == true)
                    {
                        MessageBox.Show(Common.GetMessage("8001"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(Common.GetMessage("8009"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                System.IO.Directory.SetCurrentDirectory(m_dirPath);
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
                txtExportPath.Text = string.Empty;
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbFileType_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbFileType.SelectedValue.ToString() == "-1")
                {
                    btnExport.Enabled = false;
                    btnOK.Enabled = false;
                }
                else
                {
                    btnExport.Enabled = true;
                    btnOK.Enabled = true;
                }

                if (cmbFileType.SelectedValue.ToString() == "2")
                {
                    lblDate.Visible = true;
                    dtpInvoiceConsiderDate.Visible = true;
                    lblDate.Text = "Invoice Consider Date:";
                    dtpInvoiceConsiderDate.MaxDate = DateTime.Today;
                    dtpInvoiceConsiderDate.Enabled = true;
                    string Date1 = (Convert.ToDateTime(DateTime.Today.Month + "/1/" + DateTime.Today.Year)).AddDays(-1).ToString();
                    dtpInvoiceConsiderDate.Value = Convert.ToDateTime(Date1);
                    m_invConsDate = dtpInvoiceConsiderDate.Value.ToString();

                }
                else if ((cmbFileType.SelectedValue.ToString() == "3"))
                {
                    lblDate.Visible = true;
                    dtpInvoiceConsiderDate.Visible = true;
                    lblDate.Text = "Pay-Orders From Date:";
                    dtpInvoiceConsiderDate.MaxDate = DateTime.Today;
                    dtpInvoiceConsiderDate.Enabled = true;
                    string Date1 = (Convert.ToDateTime(DateTime.Today.Month + "/1/" + DateTime.Today.Year)).AddMonths(-1).ToString();
                    dtpInvoiceConsiderDate.Value = Convert.ToDateTime(Date1);
                }
                else if ((cmbFileType.SelectedValue.ToString() == "4"))
                {
                    lblDate.Visible = true;
                    dtpInvoiceConsiderDate.Visible = true;
                    lblDate.Text = "Vouchers From Date:";
                    dtpInvoiceConsiderDate.MaxDate = DateTime.Today;
                    dtpInvoiceConsiderDate.Enabled = true;
                    string Date1 = (Convert.ToDateTime(DateTime.Today.Month + "/1/" + DateTime.Today.Year)).AddMonths(-1).ToString();
                    dtpInvoiceConsiderDate.Value = Convert.ToDateTime(Date1);
                }
                else
                {
                    lblDate.Visible = false;
                    dtpInvoiceConsiderDate.Visible = false;
                    lblDate.Text = "Invoice Consider Date:";
                    dtpInvoiceConsiderDate.Enabled = false;
                    dtpInvoiceConsiderDate.Value = DateTime.Today;
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Method to execute Job or Scheduled Task depending on Location Type
        /// </summary>
        private void ExecuteInOutJob()
        {
            String errorMessage = String.Empty;
            bool result = false;
            if (Common.CurrentLocationTypeId == 1)
            {
                ExecuteInOut objExecuteInOut = new ExecuteInOut();
                result = objExecuteInOut.ExecuteJob(ref errorMessage);
                if (result)
                {
                    MessageBox.Show(Common.GetMessage("INF0209", CONST_JOB), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(Common.GetMessage("INF0210", CONST_JOB), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                //Bikram:
                try
                {
                    DelRunFTP objFTP = new DelRunFTP(fncRunFTP);
                    IAsyncResult objRes = objFTP.BeginInvoke(null, null);

                    objPro = new FTPProcess();
                    objPro.StrMsg = Common.GetMessage("INF0265"); ;
                    objPro.strStatus = true;
                    objPro.FormBorderStyle = FormBorderStyle.None;
                    objPro.StartPosition = FormStartPosition.CenterParent;
                    objPro.ShowDialog();
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());

                }
            }
        }
        FTPProcess objPro;
        delegate int DelRunFTP();
        /// <summary>
        /// Run FTP process
        /// </summary>
        /// <returns></returns>
        private int fncRunFTP()
        {
            int exitCode = 1;
            bool isFileExist = false;
            string strDescription = "";
            try
            {                
                DBParameterList dbParam;
                try
                {
                    if (File.Exists(System.Configuration.ConfigurationManager.AppSettings["FTPPath"]))
                    {
                        Process[] ps = Process.GetProcessesByName("FTPService");
                        foreach (Process po in ps)
                        {
                            po.Kill();
                        }
                        System.Diagnostics.ProcessStartInfo p = new System.Diagnostics.ProcessStartInfo(System.Configuration.ConfigurationManager.AppSettings["FTPPath"]);
                        p.RedirectStandardError = true;
                        p.RedirectStandardOutput = true;
                        p.CreateNoWindow = true;
                        p.UseShellExecute = false;
                        System.Diagnostics.Process process = System.Diagnostics.Process.Start(p);
                        process.WaitForExit();
                        exitCode = process.ExitCode;
                        isFileExist = fncFTPFileExistForHO();

                        if ((exitCode > 0) || (isFileExist.Equals(true)))
                        {
                            exitCode = 1;
                            strDescription = Common.GetMessage("INF0263");
                        }
                        else
                        {
                            strDescription = Common.GetMessage("INF0264");
                        }
                    }
                    else
                    {
                        strDescription = Common.GetMessage("INF0266");
                    }
                }
                catch (Exception ex)
                {
                    strDescription = Common.GetMessage("10002");
                }

                objPro.BeginInvoke(new System.Action(() =>
                {
                    objPro.StrMsg = strDescription;
                    objPro.strStatus = false;
                    objPro.MinimizeBox = false;
                    objPro.MaximizeBox = false;
                    objPro.FormBorderStyle = FormBorderStyle.FixedDialog;
                }));
                //Keep Audit
                using (DataTaskManager dtManager = new DataTaskManager())
                {
                    dbParam = new DBParameterList();
                    dbParam.Add(new DBParameter("ExitCode", exitCode, DbType.Int32));
                    dbParam.Add(new DBParameter("Description", strDescription, DbType.String));
                    dbParam.Add(new DBParameter("CreatedBy",Authenticate.LoggedInUser.UserId,DbType.Int32));
                    dtManager.ExecuteNonQuery(SP_DayEndJobAudit, dbParam);                   
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
            }
            return exitCode;
        }
        /// <summary>
        /// Return False if ftp file exist for HO output.
        /// </summary>
        /// <returns></returns>
        private bool fncFTPFileExistForHO()
        {
            bool IsFileExist = false;
            System.Data.DataTable dtFTPdetail = ExecuteInOut.FTPServerPath();
            string LocalOUTPath = dtFTPdetail.Rows[0]["DirCommonPath"].ToString() + "\\OUT\\HO\\PROCESSING\\";
            if (Directory.Exists(LocalOUTPath))
            {
                string[] strFileCount = Directory.GetFiles(LocalOUTPath);
                if (strFileCount.Length > 0)
                    IsFileExist = true;
            }
            return IsFileExist;
        }

        bool UnzipAndUploadToFtp()
        {
            string filename = txtExportPath.Text.Split(("\\").ToCharArray()).LastOrDefault();

            string destinationPath = txtExportPath.Text.Substring(0, txtExportPath.Text.IndexOf(filename));
            //string foldername = filename.Split((".").ToCharArray()).FirstOrDefault();
            string foldername = DateTime.Now.ToString("ddMMyyyyHHmmss");

            destinationPath = destinationPath + "\\" + foldername;

            try
            {
                if (Directory.Exists(destinationPath))
                    Directory.Delete(destinationPath, true);
            }
            catch (Exception ex)
            {
                System.IO.Directory.SetCurrentDirectory(m_dirPath);
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("INF0217", destinationPath), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            //Extract(txtExportPath.Text, destinationPath);

            string[] files = Directory.GetFiles(destinationPath);
            if (files == null || files.Length == 0)
            {
                MessageBox.Show(Common.GetMessage("INF0216"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if ((cmbFileType.SelectedValue.ToString() == "1"))
            {
                //Check All Files Required For Distributor
                int iCount = 0;
                int totalCount = 0;
                foreach (string aKey in m_distributorFileList.Keys)
                {
                    totalCount = totalCount + 1;
                    for (int i = 0; i < files.Length; i++)
                    {
                        if (files[i].ToLower().IndexOf(".dbf") >= 0)
                        {
                            if (m_distributorFileList[aKey].ToString() == files[i].ToLower().Split(("\\").ToCharArray())[files[i].ToLower().Split(("\\").ToCharArray()).Length - 1])
                                iCount = iCount + 1;
                        }
                    }
                }

                if (iCount != totalCount)
                {
                    System.IO.Directory.SetCurrentDirectory(m_dirPath);
                    MessageBox.Show(Common.GetMessage("INF0213"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            m_bank = 0;
            m_cash = 0;
            m_ccard = 0;
            m_cheque = 0;

            m_CashfileNames = string.Empty;
            //Check All files required for Invoice
            if ((cmbFileType.SelectedValue.ToString() == "2"))
            {
                for (int i = 0; i < files.Length; i++)
                {
                    if (files[i].ToLower().IndexOf(".dbf") >= 0)
                    {
                        if (files[i].ToLower().Split(("\\").ToCharArray())[files[i].ToLower().Split(("\\").ToCharArray()).Length - 1].Replace(".dbf", "").Length > 4)
                        {
                            if (files[i].ToLower().Split(("\\").ToCharArray())[files[i].ToLower().Split(("\\").ToCharArray()).Length - 1].Replace(".dbf", "").Substring(0, 4) == "cash")
                            {
                                m_cash = m_cash + 1;
                                m_CashfileNames = m_CashfileNames + '|' + files[i].ToLower().Split(("\\").ToCharArray())[files[i].ToLower().Split(("\\").ToCharArray()).Length - 1].Replace(".dbf", "");
                            }
                        }
                        if (files[i].ToLower().Split(("\\").ToCharArray())[files[i].ToLower().Split(("\\").ToCharArray()).Length - 1].Replace(".dbf", "") == "cheque")
                            m_cheque = m_cheque + 1;


                        if (files[i].ToLower().Split(("\\").ToCharArray())[files[i].ToLower().Split(("\\").ToCharArray()).Length - 1].Replace(".dbf", "") == "ccard")
                            m_ccard = m_ccard + 1;


                        if (files[i].ToLower().Split(("\\").ToCharArray())[files[i].ToLower().Split(("\\").ToCharArray()).Length - 1].Replace(".dbf", "") == "bank")
                            m_bank = m_bank + 1;
                    }
                }

                if (m_cash == 0 && m_cheque == 0 && m_ccard == 0 && m_bank == 0)
                {
                    System.IO.Directory.SetCurrentDirectory(m_dirPath);
                    MessageBox.Show(Common.GetMessage("INF0215"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                //Check All Files Required For Distributor
                int iCount = 0;
                int totalCount = 0;
                foreach (string aKey in m_invoiceFileList.Keys)
                {
                    totalCount = totalCount + 1;
                    for (int i = 0; i < files.Length; i++)
                    {
                        if (files[i].ToLower().IndexOf(".dbf") >= 0)
                        {
                            if (m_invoiceFileList[aKey].ToString() == files[i].ToLower().Split(("\\").ToCharArray())[files[i].ToLower().Split(("\\").ToCharArray()).Length - 1])
                                iCount = iCount + 1;
                        }
                    }
                }

                if (iCount != totalCount)
                {
                    MessageBox.Show(Common.GetMessage("INF0214"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            if ((cmbFileType.SelectedValue.ToString() == "3"))
            {
                if (files[0].ToLower().Split(("\\").ToCharArray())[files[0].ToLower().Split(("\\").ToCharArray()).Length - 1] != "payorder.dbf")
                {
                    System.IO.Directory.SetCurrentDirectory(m_dirPath);
                    MessageBox.Show(Common.GetMessage("INF0214"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            if ((cmbFileType.SelectedValue.ToString() == "4"))
            {
                if (files[0].ToLower().Split(("\\").ToCharArray())[files[0].ToLower().Split(("\\").ToCharArray()).Length - 1] != "voucher.dbf")
                {
                    System.IO.Directory.SetCurrentDirectory(m_dirPath);
                    MessageBox.Show(Common.GetMessage("INF0214"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            UploadFilesToFtp(destinationPath);

            try
            {
                if (Directory.Exists(destinationPath))
                    Directory.Delete(destinationPath, true);
            }
            catch (Exception ex)
            {
                System.IO.Directory.SetCurrentDirectory(m_dirPath);
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("INF0217", destinationPath), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void UploadFilesToFtp(string extractedPath)
        {
            string[] files = Directory.GetFiles(extractedPath);
            if (files != null && files.Length > 0)
            {
                for (int i = 0; i < files.Length; i++)
                {
                    if (files[i].ToLower().IndexOf(".dbf") >= 0)
                    {
                        FileInfo fileInf = new FileInfo(files[i].ToString());
                        //if (!FTPFileExists(fileInf.Name))
                        //{
                        m_destPath = MoveFiletoFTPServer(extractedPath + "\\" + fileInf.Name);
                        //Upload(files[i],);
                        //}
                    }
                }
            }
        }

        public List<string> GetFTPFileList()
        {
            List<string> downloadFiles = new List<string>();
            FtpWebRequest reqFTP;
            try
            {
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + m_ftpLocation + "/"));
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(m_userName, m_password);
                reqFTP.Method = WebRequestMethods.Ftp.ListDirectory;

                WebResponse response = reqFTP.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());

                string line = reader.ReadLine();
                while (line != null)
                {
                    downloadFiles.Add(line);
                    line = reader.ReadLine();
                }
                reqFTP = null;
                reader.Close();
                response.Close();
                return downloadFiles;
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);

                downloadFiles = new List<string>();
                return downloadFiles;
            }
        }

        void InitializeFTPVariables()
        {
            System.Data.DataTable dt = ExecuteInOut.FTPServerPath();
            m_ftpLocation = dt.Rows[0]["FTPServer"].ToString() + "/Incremental/";
            m_userName = dt.Rows[0]["FTPUserName"].ToString();
            m_password = dt.Rows[0]["FTPPassword"].ToString();
        }

        public static FtpWebRequest FTPRequest(string ftpLocation, string fileName, string userName, string password)
        {
            FileInfo fileInf = new FileInfo(fileName);
            string uri = "ftp://" + ftpLocation + "/" + fileInf.Name;
            FtpWebRequest reqFTP;
            // Create FtpWebRequest object from the Uri provided
            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));

            // Provide the WebPermission Credintials
            reqFTP.Credentials = new NetworkCredential(userName, password);

            // By default KeepAlive is true, where the control connection
            // is not closed after a command is executed.
            reqFTP.KeepAlive = false;
            // Specify the data transfer type.
            reqFTP.UseBinary = true;

            return reqFTP;
        }

        void DeleteFileAtFTP(string fileName)
        {
            if (!fileName.ToLower().Contains(".dbf"))
                return;
            FtpWebRequest reqFTP;
            reqFTP = FTPRequest(m_ftpLocation, fileName, m_userName, m_password);

            // Specify the command to be executed.
            reqFTP.Method = WebRequestMethods.Ftp.DeleteFile;

            try
            {
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                response.Close();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        string MoveFiletoFTPServer(string fileName)
        {
            System.Data.DataTable dt = ExecuteInOut.FTPServerPath();
            if (dt.Rows.Count > 0)
            {
                Upload(m_ftpLocation, fileName, m_userName, m_password);
                return dt.Rows[0]["DirCommonPath"].ToString() + "Incremental\\";
            }
            return string.Empty;
        }

        private void Upload(string ftpUplocadLocation, string filename, string username, string password)
        {
            FileInfo fileInf = new FileInfo(filename);
            string uri = "ftp://" + ftpUplocadLocation + "/" + fileInf.Name;
            FtpWebRequest reqFTP;

            // Create FtpWebRequest object from the Uri provided
            //Commented on 21-12-2009
            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpUplocadLocation + "/" + fileInf.Name.ToLower()));
            //reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + m_UploadLocation + "/" + fileInf.Name.ToLower()));

            // Provide the WebPermission Credintials
            reqFTP.Credentials = new NetworkCredential(username, password);

            // By default KeepAlive is true, where the control connection
            // is not closed after a command is executed.
            reqFTP.KeepAlive = false;

            // Specify the command to be executed.
            reqFTP.Method = WebRequestMethods.Ftp.UploadFile;

            // Specify the data transfer type.
            reqFTP.UseBinary = true;

            // Notify the server about the size of the uploaded file
            reqFTP.ContentLength = fileInf.Length;

            // The buffer size is set to 2kb
            int buffLength = 2048;
            byte[] buff = new byte[buffLength];
            int contentLen;

            // Opens a file stream (System.IO.FileStream) to read the file
            // to be uploaded
            FileStream fs = fileInf.OpenRead();

            try
            {
                // Stream to which the file to be upload is written
                Stream strm = reqFTP.GetRequestStream();

                // Read from the file stream 2kb at a time
                contentLen = fs.Read(buff, 0, buffLength);

                // Till Stream content ends
                while (contentLen != 0)
                {
                    // Write Content from the file stream to the FTP Upload
                    // Stream
                    strm.Write(buff, 0, contentLen);
                    contentLen = fs.Read(buff, 0, buffLength);
                }

                // Close the file stream and the Request Stream
                strm.Close();
                fs.Close();
                reqFTP = null;
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //private List<ZipEntry> GetZipFiles(ZipFile zipfil)
        //{
        //    List<ZipEntry> lstZip = new List<ZipEntry>();
        //    //List lstZip = new List();
        //    Enumeration zipEnum = zipfil.entries();
        //    while (zipEnum.hasMoreElements())
        //    {
        //        ZipEntry zip = (ZipEntry)zipEnum.nextElement();
        //        lstZip.Add(zip);
        //    }
        //    return lstZip;
        //}

        //private void Extract(string zipFileName, string destinationPath)
        //{

        //    ZipFile zipfile = new ZipFile(zipFileName);

        //    List<ZipEntry> zipFiles = new List<ZipEntry>();
        //    zipFiles = GetZipFiles(zipfile);
        //    foreach (ZipEntry zipFile in zipFiles)
        //    {
        //        if (!zipFile.isDirectory())
        //        {
        //            InputStream s = zipfile.getInputStream(zipFile);
        //            try
        //            {
        //                Directory.CreateDirectory(destinationPath + "\\" + Path.GetDirectoryName(zipFile.getName()));
        //                FileOutputStream dest = new FileOutputStream(Path.Combine(destinationPath + "\\" + Path.GetDirectoryName(zipFile.getName()), Path.GetFileName(zipFile.getName())));
        //                try
        //                {
        //                    int len = 0;
        //                    sbyte[] buffer = new sbyte[7168];
        //                    while ((len = s.read(buffer)) >= 0)
        //                    {
        //                        dest.write(buffer, 0, len);
        //                    }
        //                }
        //                finally
        //                {
        //                    dest.close();
        //                }
        //            }
        //            finally
        //            {
        //                s.close();
        //            }
        //        }
        //    }
        //    zipfile.close();

        //    zipfile = null;
        //    zipFiles = null;

        //}
        private void ValidatedText(Boolean yesNo)
        {
            epUOM.SetError(txtdcc, string.Empty);


            System.Windows.Forms.TextBox txt = txtExportPath;
            System.Windows.Forms.Label lbl = lbldcc;
            ValidatedText(txt, lbl, yesNo);



        }
        private void ValidatedTextExcel(Boolean yesNo)
        {
            epUOM.SetError(txtdcc, string.Empty);


            System.Windows.Forms.TextBox txt = txtdcc;
            System.Windows.Forms.Label lbl = lbldcc;
            ValidatedText(txt, lbl, yesNo);



        }

        #endregion

        private void grpBoxUpload_Enter(object sender, EventArgs e)
        {

        }

        //private void txtdcc_TextChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        ValidatedTextExcel(true);
        //    }
        //    catch (Exception ex)
        //    {
        //        Common.LogException(ex);
        //        MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}
        private void ValidatedText(System.Windows.Forms.TextBox txt, System.Windows.Forms.Label lbl, Boolean yesNo)
        {
            bool isTextBoxEmpty = CoreComponent.Core.BusinessObjects.Validators.CheckForEmptyString(txt.Text.Length);
            if (isTextBoxEmpty == true && yesNo == false)
                epUOM.SetError(txt, Common.GetMessage("INF0019", lbl.Text.Trim().Substring(0, lbl.Text.Trim().Length - 2)));
            else
                epUOM.SetError(txt, string.Empty);
        }

        private void btndcc_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();

                ofd.InitialDirectory = @"C:\";

                ofd.Filter = "Excel files (*.xls)|*.xls";

                if (ofd.ShowDialog() == DialogResult.OK)
                    txtdcc.Text = ofd.FileName;
               
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private bool ExceldataImport(string excelpath)
        {
            //List<ExecuteInOut> eioList = new List<ExecuteInOut>();
            try
            {

                string strSheetName = Path.GetFileName(excelpath);
                //DataSet ds = GetDataTable(excelpath,strSheetName);
                Microsoft.Office.Interop.Excel.ApplicationClass app = new ApplicationClass();
                // create the workbook object by opening the excel file.
                Microsoft.Office.Interop.Excel.Workbook workBook = app.Workbooks.Open(excelpath,
                                                             0,
                                                             true,
                                                             5,
                                                             "",
                                                             "",
                                                             true,
                                                             Microsoft.Office.Interop.Excel.XlPlatform.xlWindows,
                                                             "\t",
                                                             false,
                                                             false,
                                                             0,
                                                             true,
                                                             1,
                                                             0);
                // get the active worksheet using sheet name or active sheet
                Microsoft.Office.Interop.Excel.Worksheet workSheet = (Microsoft.Office.Interop.Excel.Worksheet)workBook.ActiveSheet;
                int index = 0;
                // This row,column index should be changed as per your need.
                // i.e. which cell in the excel you are interesting to read.
                int rowIndex = 1;
                object colIndex1 = 1;
                object colIndex2 = 2;
                object colIndex3 = 3;
                ExecuteInOut objInOut;
                List<ExecuteInOut> eioList = new List<ExecuteInOut>();
                try
                {
                    //DataSet ds = GetDataTable(excelpath);

                    while (((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[rowIndex, colIndex1]).Value2 != null)
                    {
                        if (rowIndex == 1)
                        {
                            objInOut = new ExecuteInOut();
                            objInOut.Header1 = ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[rowIndex, colIndex1]).Value2.ToString();
                            objInOut.Header2 = ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[rowIndex, colIndex2]).Value2.ToString();
                            objInOut.Header3 = ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[rowIndex, colIndex3]).Value2.ToString();
                            eioList.Add(objInOut);
                        }
                        else if (rowIndex != 1)
                        {
                            objInOut = new ExecuteInOut();
                            rowIndex = 1 + index;
                            objInOut.BussinessMonth = ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[rowIndex, colIndex1]).Value2.ToString();
                            objInOut.PcCode = ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[rowIndex, colIndex2]).Value2.ToString();
                            objInOut.ChequeNo = ((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[rowIndex, colIndex3]).Value2.ToString();
                            eioList.Add(objInOut);
                        }
                        index++;
                        rowIndex++;

                    }

                    //bool result = IsXmlValidString(ToXml(eioList));
                    //XmlDocument xml = new XmlDocument();
                    //xml.Load(Environment.CurrentDirectory+"//App_Data//webxml.xml");
                    //XmlDocument xml = new XmlDocument();
                    ////xml.Load(Environment.CurrentDirectory + "//App_Data//XMLFile1.xml");
                    //xml.LoadXml(ToXml(eioList));
                    //bool result = IsXmlValidString(xml);
                    //if (result == true)
                    //{
                    //    MessageBox.Show(Common.GetMessage("VAL0612"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        
                    

                }

                catch (Exception ex)
                {
                    app.Quit();
                    //Console.WriteLine(ex.Message);
                    throw ex;
                }

                XmlDocument xml = new XmlDocument();
                //xml.Load(Environment.CurrentDirectory + "//App_Data//XMLFile1.xml");
                xml.LoadXml(ToXml(eioList));
                bool result = IsXmlValidString(xml);
                if (result == true)
                {

                    string errorMessage = string.Empty;

                    bool saveresult = SaveexcelData(CoreComponent.Core.BusinessObjects.Common.ToXml(eioList), SP_EXCELDATA_SAVE, ref errorMessage);
                    if (saveresult== true)
                    {
                        return true;
                    }
                    return false;

                }
                else
                {
                    MessageBox.Show(Common.GetMessage("VAL0612"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
            }
        
            catch (Exception ex)
            {
                CoreComponent.Core.BusinessObjects.Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


        public static string ToXml(object target)
        {
            string xmlstring = string.Empty;
            System.IO.StringWriter output = null;
            XmlSerializer xs = null;

            try
            {
                output = new System.IO.StringWriter(new StringBuilder());
                xs = new XmlSerializer(target.GetType());
                xs.Serialize(output, target);
                xmlstring = output.ToString().Replace("xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"", "").Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "  <?xml version=\"1.0\" encoding=\"utf-8\" ?>").Replace("\r\n", "").Trim();
                //xmlstring = output.ToString().Replace("xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"", "").Replace("xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"", "").Replace("\r\n", "").Trim();
                return xmlstring;
            }
            catch { throw; }
        }
        public void SchemaValidationEventHandler(object sender, ValidationEventArgs args)
        {

            validationMessage.Append(args.Exception.Message);

            string FailureMessage = validationMessage.ToString();
            validationStatus = false;

        }

        //Function use to check validity of requested orderXML with spcified schema.

        public bool IsXmlValidString(XmlDocument xmlString)
        {
            try
            {
                validationStatus = true;

                XmlReaderSettings settings = new XmlReaderSettings();

                settings.Schemas.Add(@"", XmlReader.Create(Environment.CurrentDirectory + "//App_Data//ExcelXml.xsd"));
                settings.ValidationType = ValidationType.Schema;
                settings.ValidationFlags = XmlSchemaValidationFlags.ReportValidationWarnings;
                settings.ValidationEventHandler += new ValidationEventHandler(SchemaValidationEventHandler);
                  settings.IgnoreWhitespace = true;
                  settings.IgnoreComments = true;
                using (System.IO.StringReader xmlStringReader = new System.IO.StringReader(xmlString.InnerXml))
                {
                    XmlReader reader = XmlReader.Create(xmlStringReader, settings);
                    while (reader.Read()) ;
                }
                return validationStatus;
            }

            catch (XmlException)
            {

                return false;

            }

        }
        //public void SchemaValidationEventHandler(object sender, ValidationEventArgs args)
        //{

        //    validationMessage.Append(args.Exception.Message);

        //    string FailureMessage = validationMessage.ToString();
        //    validationStatus = false;

        //}


        //public bool IsXmlValidString(string xmlString)
        //{
        //    try
        //    {


        //        validationStatus = true;

        //        XmlReaderSettings settings = new XmlReaderSettings();
        //        settings.Schemas.Add(@"http://www.Vestige.com", Environment.CurrentDirectory + "\\App_Data\\ExcelXml.xsd");
        //        settings.ValidationType = ValidationType.Schema;
        //        settings.ValidationFlags = XmlSchemaValidationFlags.ReportValidationWarnings;
        //        settings.ValidationEventHandler += new ValidationEventHandler(SchemaValidationEventHandler);
        //        using (System.IO.StringReader xmlStringReader = new System.IO.StringReader(xmlString))
        //        {
        //            XmlReader reader = System.Xml.XmlReader.Create(xmlStringReader, settings);
        //            while (reader.Read()) ;
        //        }
        //        return validationStatus;
        //    }

        //    catch (XmlException Ex)
        //    {

        //        return false;
        //        throw Ex;
        //    }

        //}
        public bool SaveexcelData(string xmlDoc, string spName, ref string errorMessage)
        {
            bool isSuccess = false;
            try
            {
                DBParameterList dbParam;
                using (DataTaskManager dtManager = new DataTaskManager())
                {
                    try
                    {
                        // initialize the parameter list object
                        dbParam = new DBParameterList();

                        // add the relevant 2 parameters
                        dbParam.Add(new DBParameter(Common.PARAM_DATA, xmlDoc, DbType.String));
                        dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                        // begin the transaction
                        dtManager.BeginTransaction();
                        // executing procedure to save the record 
                        System.Data.DataTable dt = dtManager.ExecuteDataTable(spName, dbParam);

                        // update database message
                        errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();

                        // if an error returned from the database
                        if (errorMessage != string.Empty)
                        {
                            isSuccess = false;
                            dtManager.RollbackTransaction();
                        }
                        else
                        {
                            isSuccess = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        Common.LogException(ex);
                        dtManager.RollbackTransaction();
                        return isSuccess;
                    }
                }

            }
            catch (Exception ex)
            {
                Common.LogException(ex);
            }
            return isSuccess;
        }

        private void txtdcc_TextChanged(object sender, EventArgs e)
        {
            if (txtdcc.TextLength > 0)
            {
                btnOK.Enabled = true;
            }
            else
            {
                btnOK.Enabled = false;
            }
        }

        private void btnDBC_Click(object sender, EventArgs e)
        {
            int  mode = 0;
            string errormessage = string.Empty;
            bool result = false;
            if (rdbTotalpv.Checked)
            {
                mode = 1;
            }
            else if (rdbdcc.Checked)
            {
                mode = 2;
            }
            if(mode>0)
               result =  QuerySave(mode,ref errormessage);
            if(result == true)
                MessageBox.Show(Common.GetMessage("8001"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show(Common.GetMessage("8009"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private bool QuerySave(int mode,ref string errorMessage)
        {
             bool isSuccess = false;
             try
             {
                 DBParameterList dbParam;
                 using (DataTaskManager dtManager = new DataTaskManager())
                 {
                     try
                     {
                         // initialize the parameter list object
                         dbParam = new DBParameterList();

                         // add the relevant 2 parameters
                         dbParam.Add(new DBParameter(Common.PARAM_DATA, mode, DbType.Int32));
                         dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));
                         dtManager.BeginTransaction();

                         System.Data.DataTable dt = dtManager.ExecuteDataTable(SP_DBCDATA_SAVE, dbParam);
                         errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();
                         if (errorMessage != string.Empty)
                         {
                             isSuccess = false;
                             dtManager.RollbackTransaction();
                         }
                         else
                         {
                             isSuccess = true;
                         }
                         return isSuccess;
                     }
                     catch (Exception ex)
                     {
                         Common.LogException(ex);
                         dtManager.RollbackTransaction();
                         return isSuccess;
                     }
                 }
             }
             catch (Exception ex)
             {
                 Common.LogException(ex);
              
                 return isSuccess;
             }
        }

        private void btnWait_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    


        //private bool validateExcel(string excelpathtovalidate)
        //{
        //    Excel.ApplicationClass app = new ApplicationClass();
        //    Excel.Workbook workBook = app.Workbooks.Open(excelpathtovalidate,
        //                                                 0,
        //                                                 true,
        //                                                 5,
        //                                                 "",
        //                                                 "",
        //                                                 true,
        //                                                 Excel.XlPlatform.xlWindows,
        //                                                 "\t",
        //                                                 false,
        //                                                 false,
        //                                                 0,
        //                                                 true,
        //                                                 1,
        //                                                 0);

        //    Excel.Worksheet workSheet = (Excel.Worksheet)workBook.ActiveSheet;
        //    int rowIndex = 0;
        //    object colIndex1 = 1;
        //    object colIndex2 = 2;
        //    object colIndex3 = 3;
        //    Date bussinessdate;
        //    try
        //    {
        //            string BussinessMonth = ((Excel.Range)workSheet.Cells[rowIndex, colIndex1]).Value2.ToString();
        //            string PcCode = ((Excel.Range)workSheet.Cells[rowIndex, colIndex2]).Value2.ToString();
        //            string ChequeNo = ((Excel.Range)workSheet.Cells[rowIndex, colIndex3]).Value2.ToString();
        //        if(BussinessMonth == "Bussiness Month" && PcCode  == "PC Code "&& ChequeNo == "Cheque No")
        //            if((Excel.Range)workSheet.Cells.GetType() is System.String)
        //            {

        //                bussinessdate.before();
        //            return true;
        //        }

        //    }
        //}
    //    public void GetDataTable(string path, string strSheetName)
    //    {
    //        try
    //        {
    //            //FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read);

    //            ////1. Reading from a binary Excel file ('97-2003 format; *.xls)
    //            //IExcelDataReader excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
    //            ////...
    //            ////2. Reading from a OpenXml Excel file (2007 format; *.xlsx)
    //            //IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
    //            ////...
    //            ////3. DataSet - The result of each spreadsheet will be created in the result.Tables
    //            //DataSet result = excelReader.AsDataSet();
    //            ////...
    //            ////4. DataSet - Create column names from first row
    //            //excelReader.IsFirstRowAsColumnNames = true;
    //            //DataSet result = excelReader.AsDataSet();

    //            ////5. Data Reader methods
    //            //while (excelReader.Read())
    //            //{
    //            //    //excelReader.GetInt32(0);
    //            //}

    //            ////6. Free resources (IExcelDataReader is IDisposable)
    //            //excelReader.Close();
    //        }
    //        //    string strComand;
    //        //    DataSet dt;
    //        //    if (strSheetName.IndexOf("|") > 0)
    //        //    {
    //        //        /* if Range is provided.*/
    //        //        string SheetName = strSheetName.Substring(0, strSheetName.IndexOf("|"));
    //        //        string Range = strSheetName.Substring(strSheetName.IndexOf("|") + 1);
    //        //        strComand = "select * from [" + SheetName + "$" + Range + "]";
    //        //    }
    //        //    else
    //        //    {
    //        //        strComand = "select * from [Crossword$]";
    //        //    }
    //        //    string makecon = @"provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=Excel 7.0;";
    //        //    OleDbConnection cn = new OleDbConnection(makecon);

    //        //    //cn.Open();
    //        //    OleDbDataAdapter daAdapter = new OleDbDataAdapter(strComand, cn);
    //        //    dt = new DataSet();
    //        //    //daAdapter.FillSchema(dt, SchemaType.Source);
    //        //    daAdapter.Fill(dt);
    //        //    //cn.Close();
    //        //    return dt;
    //        //}

    //        catch (Exception ex)
    //        {
    //            throw new Exception(ex.Message);
    //        }
    //    }
    }
}

