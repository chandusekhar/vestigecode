using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using CoreComponent.Core.BusinessObjects;
using CoreComponent.Hierarchies.BusinessObjects;
using CoreComponent.Core.UI;
using CoreComponent.BusinessObjects;


namespace CoreComponent.UI
{
    public partial class frmDistributorPANBankNew : Form
    {
        private string panPath;
        private string bankPath;
        private string documentType;
        private string panBankNo;
        private string bankName;
        private string ifscCode;
        private string branchName;
        private DataTable dtImage;
        private string m_Distributorid;
        
        int cntImage = 0;

        public string BankName
        {
            get 
            {
                return bankName;
            }
            set 
            {
                bankName = value;
            }
        }
        
        
        public string IFSCCode
        {
            set 
            {
                ifscCode = value;
            }
            get 
            {
                return ifscCode;
            }
        }
        public string BranchName
        {
            set 
            {
                branchName = value;
            }
            get 
            {
                return branchName;    
            }
        }
        public string PANPath
        {
            get 
            {
                return panPath;
            }
            set 
            {
                panPath = value;
            }
        }
        
        public string BankPath
        {
            get
            {
                return bankPath;
            }
            set
            {
                bankPath = value;
            }
        }
        public string DocumentType
        {
            set { 
                    documentType = value; 
                }
            get { 
                    return documentType; 
                }
        }
        public string PanBankNo
        {
            set { panBankNo = value; }
            get { return panBankNo; }
        }
        public string PanBankType
        {
            get;
            set;
        }
        public string EndDate
        {
            get;
            set;
        }
        public string CurrentHistory
        {
            get;
            set;
        }
        public string EditType
        {
            get;
            set;
        }
        public Int64 BatchNo
        {
            get;
            set;
        }
        public Distributor m_Distributor;
        public frmDistributorPANBankNew()
        {
            InitializeComponent();
        }
        
        public frmDistributorPANBankNew(int Distributorid, string type,string endDate,Distributor objDistributor,string currentHistory,string editType)
        {
            InitializeComponent();
            m_Distributor = objDistributor;
            m_Distributorid = Distributorid.ToString();
            PanBankType = type;
            EndDate = endDate;
            EditType = editType;
            
            CurrentHistory = currentHistory;
            if (type == "P")
            {
                m_Distributor.PANType = type;
                panBankNo = objDistributor.DistributorPANNumber;
                //ActiveForm.Text = "Distributor PAN Details";
                label1.Text = "Distributor PAN Details";
                grpImgPan.Text = "PAN Details";
            }
            else
            {
                m_Distributor.BankType = type;
                //ActiveForm.Text = "Distributor Bank Details";
                label1.Text = "Distributor Bank Details";
                grpImgPan.Text = "Bank Details";
                PanBankNo = objDistributor.AccountNumber;
                BankName = objDistributor.Bank;
                IFSCCode = objDistributor.BankBranchCode;
            }

            if (CurrentHistory == "C" )
            {
                btnSave.Visible = true;
                btnReset.Visible = true;
                btnBrowse.Visible = true;
                btnImgFirst.Visible = false;
                btnImgLast.Visible = false;
                btnImgNext.Visible = false;
                btnImgPrevious.Visible = false;
                if (editType != "E")
                {
                    btnBrowse.Enabled = false;
                    btnSave.Enabled = false;
                    btnReset.Enabled = true;
                }
                else 
                {
                    btnBrowse.Enabled = false;
                    btnSave.Enabled = false;
                    btnReset.Enabled = false ;
                }
            }
            else
            {
                btnSave.Visible = false;
                btnReset.Visible = false;
                btnBrowse.Visible = false;
                btnImgFirst.Visible = false ;
                btnImgLast.Visible = false ;
                btnImgNext.Visible = false ;
                btnImgPrevious.Visible = false ;
                btnBrowse.Enabled = false;
                btnSave.Enabled = false;
                btnReset.Enabled = true;
            }
            
         }

        public frmDistributorPANBankNew(string Distributorid, string type, string endDate, string currentHistory, string strPanBankNo,Int64 batchno)
        { 
            InitializeComponent();
            m_Distributorid = Distributorid.ToString() ;
            BatchNo = batchno;
            EndDate = endDate;
            if (type == "P")
            {
                PanBankType = type;
                PanBankNo = strPanBankNo;
                label1.Text = "Distributor PAN Details";
                grpImgPan.Text = "PAN Details";
            }
            else
            {
                PanBankType = type;
                PanBankNo = strPanBankNo;
                label1.Text = "Distributor Bank Details";
                grpImgPan.Text = "Bank Details";
            }
            CurrentHistory = currentHistory;
            if (CurrentHistory == "C")
            {
                btnSave.Visible = true;
                btnReset.Visible = true;
                btnBrowse.Visible = true;
                btnImgFirst.Visible = false;
                btnImgLast.Visible = false;
                btnImgNext.Visible = false;
                btnImgPrevious.Visible = false;
                btnBrowse.Enabled = false;
                btnSave.Enabled = false;
                btnReset.Enabled = true;
                
            }
            else
            {
                btnSave.Visible = false;
                btnReset.Visible = false;
                btnBrowse.Visible = false ;
                btnImgFirst.Visible = false ;
                btnImgLast.Visible = false ;
                btnImgNext.Visible = false ;
                btnImgPrevious.Visible = false ;
                btnBrowse.Enabled = false;
                btnSave.Enabled = false;
                btnReset.Enabled = true;
            }
        }
        private void btnPanBrowse_Click(object sender, EventArgs e)
        {
            PANPath = getImageFileName();
            if (PANPath == "" || PANPath == null)
                return;

            picPAN.Load(PANPath);
        }

        private void btnBankBrowse_Click(object sender, EventArgs e)
        {
            BankPath  = getImageFileName();
            if (BankPath == "" || BankPath == null)
                return;

            
        }
        private Bitmap setBmp(Byte[] byteRow)
        {
            MemoryStream mem = new MemoryStream(byteRow);
            Bitmap bmp = new Bitmap(mem);
            return bmp;
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            byte[] imgPanByte = ReadFile(PANPath);
            DistributorPANBank objDistPanBank = new DistributorPANBank();
            string err = "";
            try
            {
                if (m_Distributorid == null || m_Distributorid == "")
                {
                    MessageBox.Show(Common.GetMessage("INF0001", m_Distributorid), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if ((PANPath == null || PANPath == "") && (BankPath == null || BankPath == ""))
                {
                    if(PanBankType=="P")
                       MessageBox.Show(Common.GetMessage("VAL0002", "PAN"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show(Common.GetMessage("VAL0002", "Bank"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                    return;
                }

                DialogResult dgReasult = MessageBox.Show(Common.GetMessage("INF0027"), Common.GetMessage("10001"), MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (DialogResult.OK == dgReasult)
                {

                   // Boolean isSuccess = objDistPanBank.Save(m_Distributorid,PanBankType,PanBankNo,BankName,IFSCCode,imgPanByte, "Usp_DistPANBankSave", ref err);
                    if (PanBankType == "P")
                    {
                        m_Distributor.PanImage = imgPanByte;
                        m_Distributor.PANType = PanBankType;
                    }
                    else 
                    {
                        m_Distributor.BankImage = imgPanByte;
                        m_Distributor.BankType = PanBankType; 
                    }
                     //this.SearchImage(m_Distributorid);
                     this.Close();

                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string getImageFileName()
        {
            try
            {
                string filepath = Environment.CurrentDirectory;

                opnFileDlgPan.FileName = "";
                opnFileDlgPan.Filter = "All Image files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
                opnFileDlgPan.ShowDialog();
                Environment.CurrentDirectory = filepath;
                return opnFileDlgPan.FileName;
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public byte[] ReadFile(String url)
        {
            try
            {
                if (url == null)
                    return null;
                
                byte[] data = null;
                FileInfo fileInfo = new FileInfo(url);
                long pLength = fileInfo.Length;
                FileStream fStream = new FileStream(url, FileMode.Open, FileAccess.Read);
                BinaryReader bReader = new BinaryReader(fStream);
                data = bReader.ReadBytes((int)pLength);
                return data;
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                return null;
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            picPAN.Image = null;
            btnBrowse.Enabled = true;
            btnSave.Enabled = false;
        }

        private void btnImgFirst_Click(object sender, EventArgs e)
        {
        
            if (dtImage.Rows.Count > 0)
            {
                if (cntImage == 0)
                {
                    MessageBox.Show(Common.GetMessage("VAL0061", "first record"), Common.GetMessage("10001"), MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    return;
                }


                if (dtImage.Rows[0]["PANBankImage"] != DBNull.Value)
                    this.picPAN.Image = setBmp((Byte[])dtImage.Rows[0]["PANBankImage"]);
                else
                    this.picPAN.Image = null;

                
                cntImage = 0;
                btnImgLast.Enabled = true;
                btnImgNext.Enabled = true;
                btnImgPrevious.Enabled = true;
                btnImgFirst.Enabled = true;
            }
            else
            {
                MessageBox.Show(Common.GetMessage("8002"), Common.GetMessage("10001"), MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                return;
            }

        }

        private void btnImgPrevious_Click(object sender, EventArgs e)
        {
            if (dtImage.Rows.Count > 0)
            {
                if(cntImage > dtImage.Rows.Count-1 )
                {
                    cntImage = dtImage.Rows.Count - 1;  
                }
                if (cntImage < 0)
                {
                    MessageBox.Show(Common.GetMessage("VAL0061", "first record"), Common.GetMessage("10001"), MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    cntImage = 0;
                    return;
                }
                if (cntImage >= 0)
                {
                    if (dtImage.Rows[cntImage]["PANBankImage"] != DBNull.Value)
                        this.picPAN.Image = setBmp((Byte[])dtImage.Rows[cntImage]["PANBankImage"]);
                    else
                        this.picPAN.Image = null;

                    if (cntImage == 0)
                    {
                        btnImgLast.Enabled = true;
                        btnImgNext.Enabled = true;
                        btnImgPrevious.Enabled = true;
                        btnImgFirst.Enabled = true;
                    }
                    else
                    {
                        btnImgLast.Enabled = true;
                        btnImgNext.Enabled = true;
                        btnImgPrevious.Enabled = true;
                        btnImgFirst.Enabled = true;
                    }
                    cntImage--;
                }
                else
                {
                    MessageBox.Show(Common.GetMessage("VAL0061", "first record"), Common.GetMessage("10001"), MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    cntImage = 0;
                    return;
                }
            }
            else 
            {
                MessageBox.Show(Common.GetMessage("8002"), Common.GetMessage("10001"), MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                return;
            }
        }

        private void btnImgNext_Click(object sender, EventArgs e)
        {
            if (dtImage.Rows.Count > 0)
            {
                if (cntImage < 0)
                {
                    cntImage = 0;
                }
                if (cntImage <= dtImage.Rows.Count - 1)
                {
                    //this.picPAN.Image = setBmp((Byte[])dtImage.Rows[cntImage]["PANImage"]);
                    //this.picBank.Image = setBmp((Byte[])dtImage.Rows[cntImage]["BankImage"]);

                    if (dtImage.Rows[cntImage]["PANBankImage"] != DBNull.Value)
                        this.picPAN.Image = setBmp((Byte[])dtImage.Rows[cntImage]["PANBankImage"]);
                    else
                        this.picPAN.Image = null;

                    if (cntImage == dtImage.Rows.Count - 1)
                    {
                        btnImgLast.Enabled = true;
                        btnImgNext.Enabled = true;
                        btnImgPrevious.Enabled = true;
                        btnImgFirst.Enabled = true;
                    }
                    else
                    {
                        btnImgLast.Enabled = true;
                        btnImgNext.Enabled = true;
                        btnImgPrevious.Enabled = true;
                        btnImgFirst.Enabled = true;
                    }
                    cntImage++;
                }
                else
                {
                    MessageBox.Show(Common.GetMessage("VAL0061", "last record"), Common.GetMessage("10001"), MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    return;
                }
            }
            else 
            {
                MessageBox.Show(Common.GetMessage("8002"), Common.GetMessage("10001"), MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                return;
            }
        }

        private void btnImgLast_Click(object sender, EventArgs e)
        {
            //if (cntImage >= 0 || cntImage == dtImage.Rows.Count-1)
            if (cntImage < 0 || cntImage == dtImage.Rows.Count-1 )
            {
                MessageBox.Show(Common.GetMessage("VAL0061", "last record"), Common.GetMessage("10001"), MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                return;
            }
            if (dtImage.Rows.Count > 0)
            {

                //this.picPAN.Image = setBmp((Byte[])dtImage.Rows[dtImage.Rows.Count - 1]["PANImage"]);
                //this.picBank.Image = setBmp((Byte[])dtImage.Rows[dtImage.Rows.Count - 1]["BankImage"]);

                if (dtImage.Rows[dtImage.Rows.Count - 1]["PANBankImage"] != DBNull.Value)
                    this.picPAN.Image = setBmp((Byte[])dtImage.Rows[dtImage.Rows.Count - 1]["PANBankImage"]);
                else
                    this.picPAN.Image = null;

               cntImage = dtImage.Rows.Count - 1;

                btnImgLast.Enabled = true;
                btnImgNext.Enabled = true;
                btnImgPrevious.Enabled = true;
                btnImgFirst.Enabled = true;
            }
            else 
            {
                MessageBox.Show(Common.GetMessage("8002"), Common.GetMessage("10001"), MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                return;
            }
        }

        private void frmDistributorPANBankNew_Load(object sender, EventArgs e)
        {
            this.SearchImage(m_Distributorid);
        }
        private void SearchImage(string Distributorid)
        {
            DistributorPANBank objDistPanBank = new DistributorPANBank();
            string errMessage = "";
            try
            {
               if(m_Distributor!=null)
               {
                   if ((m_Distributor.BankImage != null) || (m_Distributor.PanImage != null))
                   {
                       if (PanBankType == "P")
                       {
                           
                           if (m_Distributor.PanImage != null)
                           {
                               picPAN.Image = m_Distributor.PanImage == null ? null : setBmp((Byte[])m_Distributor.PanImage);     
                               btnReset.Enabled = false;
                               btnBrowse.Enabled = true;
                               btnSave.Enabled = false;
                           }
                           else 
                           {
                               dtImage = objDistPanBank.Search(Distributorid, PanBankType, EndDate,PanBankNo,BatchNo , "Usp_DistributorPANBankSearch", ref errMessage);
                               if (dtImage.Rows.Count > 0)
                               {
                                   if (dtImage.Rows[dtImage.Rows.Count - 1]["PANBankImage"] != DBNull.Value)
                                   {
                                       this.picPAN.Image = setBmp((Byte[])dtImage.Rows[dtImage.Rows.Count - 1]["PANBankImage"]);

                                       btnReset.Enabled = true;
                                       btnBrowse.Enabled = false;
                                       btnSave.Enabled = false;
                                   }
                                   else
                                   {
                                       this.picPAN.Image = null;
                                       if (EditType == "E")
                                       {
                                           btnReset.Enabled = false;
                                           btnBrowse.Enabled = false;
                                           btnSave.Enabled = false;
                                       }
                                       else
                                       {
                                           btnReset.Enabled = false;
                                           btnBrowse.Enabled = true;
                                           btnSave.Enabled = false;
                                       }
                                   }

                                   cntImage = dtImage.Rows.Count - 1;

                                   btnImgLast.Enabled = true;
                                   btnImgNext.Enabled = true;
                                   btnImgPrevious.Enabled = true;
                                   btnImgFirst.Enabled = true;
                               }
                               else
                               {
                                   if (EditType != "E")
                                   {
                                       btnReset.Enabled = false;
                                       btnSave.Enabled = false;
                                       btnBrowse.Enabled = true;
                                   }
                                   else
                                   {
                                       btnSave.Enabled = false;
                                       btnBrowse.Enabled = false;
                                       btnReset.Enabled = false;
                                   }
                               }                           
                           
                           }
                       }
                       else
                       {
                           
                           if (m_Distributor.BankImage != null)
                           {
                               picPAN.Image = m_Distributor.BankImage == null ? null : setBmp((Byte[])m_Distributor.BankImage);
                               btnReset.Enabled = false;
                               btnBrowse.Enabled = true;
                               btnSave.Enabled = false;

                           }
                           else {
                               dtImage = objDistPanBank.Search(Distributorid, PanBankType, EndDate, PanBankNo, BatchNo, "Usp_DistributorPANBankSearch", ref errMessage);
                               if (dtImage.Rows.Count > 0)
                               {
                                   if (dtImage.Rows[dtImage.Rows.Count - 1]["PANBankImage"] != DBNull.Value)
                                   {
                                       this.picPAN.Image = setBmp((Byte[])dtImage.Rows[dtImage.Rows.Count - 1]["PANBankImage"]);

                                       btnReset.Enabled = true;
                                       btnBrowse.Enabled = false;
                                       btnSave.Enabled = false;
                                   }
                                   else
                                   {
                                       this.picPAN.Image = null;
                                       if (EditType == "E")
                                       {
                                           btnReset.Enabled = false;
                                           btnBrowse.Enabled = false;
                                           btnSave.Enabled = false;
                                       }
                                       else
                                       {
                                           btnReset.Enabled = false;
                                           btnBrowse.Enabled = true;
                                           btnSave.Enabled = false;
                                       }
                                   }

                                   cntImage = dtImage.Rows.Count - 1;

                                   btnImgLast.Enabled = true;
                                   btnImgNext.Enabled = true;
                                   btnImgPrevious.Enabled = true;
                                   btnImgFirst.Enabled = true;
                               }
                               else
                               {
                                   if (EditType != "E")
                                   {
                                       btnReset.Enabled = false;
                                       btnSave.Enabled = false;
                                       btnBrowse.Enabled = true;
                                   }
                                   else
                                   {
                                       btnSave.Enabled = false;
                                       btnBrowse.Enabled = false;
                                       btnReset.Enabled = false;
                                   }
                               }     
                           
                           }
                       }
                   }
                   else{
                       dtImage = objDistPanBank.Search(Distributorid, PanBankType, EndDate, PanBankNo, BatchNo, "Usp_DistributorPANBankSearch", ref errMessage);
                       if (dtImage.Rows.Count > 0)
                       {
                           if (dtImage.Rows[dtImage.Rows.Count - 1]["PANBankImage"] != DBNull.Value)
                           {
                               this.picPAN.Image = setBmp((Byte[])dtImage.Rows[dtImage.Rows.Count - 1]["PANBankImage"]);
                               if (EditType != "E")
                               {
                                   btnReset.Enabled = true;
                                   btnBrowse.Enabled = false;
                                   btnSave.Enabled = false;
                               }
                               else 
                               {
                                   btnReset.Enabled = false ;
                                   btnBrowse.Enabled = false;
                                   btnSave.Enabled = false;
                               
                               }
                           }
                           else
                           {
                               this.picPAN.Image = null;
                               if (EditType == "E")
                               {
                                   btnReset.Enabled = false;
                                   btnBrowse.Enabled = false;
                                   btnSave.Enabled = false;
                               }
                               else
                               {
                                   btnReset.Enabled = false;
                                   btnBrowse.Enabled = true ;
                                   btnSave.Enabled = false;
                               }
                           }

                           cntImage = dtImage.Rows.Count - 1;

                           btnImgLast.Enabled = true;
                           btnImgNext.Enabled = true;
                           btnImgPrevious.Enabled = true;
                           btnImgFirst.Enabled = true;
                       }
                       else
                       {
                           if (EditType != "E")
                           {
                               btnReset.Enabled = false;
                               btnSave.Enabled = false;
                               btnBrowse.Enabled = true;
                           }
                           else
                           {
                               btnSave.Enabled = false;
                               btnBrowse.Enabled = false;
                               btnReset.Enabled = false;
                           }
                       }
                   }
               }
               else
               {
                   dtImage = objDistPanBank.Search(Distributorid, PanBankType, EndDate, PanBankNo, BatchNo, "Usp_DistributorPANBankSearch", ref errMessage);
                    if (dtImage.Rows.Count > 0)
                    {
                        if (dtImage.Rows[dtImage.Rows.Count - 1]["PANBankImage"] != DBNull.Value)
                        {
                            this.picPAN.Image = setBmp((Byte[])dtImage.Rows[dtImage.Rows.Count - 1]["PANBankImage"]);

                            btnReset.Enabled = true;
                            btnBrowse.Enabled = false;
                            btnSave.Enabled = false;
                        }
                        else
                        {
                            this.picPAN.Image = null;
                            btnReset.Enabled = false;
                            btnBrowse.Enabled = true;
                            btnSave.Enabled = false;
                        }

                        cntImage = dtImage.Rows.Count - 1;

                        btnImgLast.Enabled = true;
                        btnImgNext.Enabled = true;
                        btnImgPrevious.Enabled = true;
                        btnImgFirst.Enabled = true;
                    }
                    else
                    {
                        if (EditType != "E")
                        {
                            btnReset.Enabled = false;
                            btnSave.Enabled = false;
                            btnBrowse.Enabled = true;
                        }
                        else
                        {
                            btnSave.Enabled = false;
                            btnBrowse.Enabled = false;
                            btnReset.Enabled = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBrowse_Click_1(object sender, EventArgs e)
        {

        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            PANPath = getImageFileName();
            if (PANPath == "" || PANPath == null)
            {
                btnReset.Enabled = false ;
                btnSave.Enabled = false;
                btnBrowse.Enabled = true;
                return;
            }
            string strExtension = Path.GetExtension(PANPath);
            if (!chkValidExtension(strExtension))
            {
                MessageBox.Show(Common.GetMessage("INF0010","File Format"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
 
            picPAN.Load(PANPath);

            btnReset.Enabled = true;
            btnSave.Enabled = true;
            btnBrowse.Enabled = false;
        }

        public bool chkValidExtension(string ext)
        {
            
            string[] PosterAllowedExtensions = new string[] { ".gif", ".jpeg", ".jpg", ".png",".bmp", ".GIF", ".JPEG", ".JPG", ".PNG" ,".BMP"};
            for (int i = 0; i < PosterAllowedExtensions.Length; i++)
            {
                if (ext == PosterAllowedExtensions[i])
                    return true;
            }
            return false;
        }

        private void picPAN_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
       
    }
}
