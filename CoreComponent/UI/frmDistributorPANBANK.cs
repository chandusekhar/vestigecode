using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using CoreComponent.Core.BusinessObjects;
using CoreComponent.Hierarchies.BusinessObjects;
using CoreComponent.Core.UI;
using CoreComponent.BusinessObjects;
using System.Xml;

namespace CoreComponent.UI
{
    public partial class frmDistributorPANBANK : HierarchyTemplate
    {
        public List<DistributorPANBank> listPanBank = null;
        private DataTable dtImage;
        private string m_Distributorid;
        int cntImage = 0;
        public frmDistributorPANBANK()
        {
            InitializeComponent();
            lblPageTitle.Text = "Distributor PAN/Bank Details";

        }
        public frmDistributorPANBANK(int Distributorid)
        {
            InitializeComponent();
            lblPageTitle.Text = "Distributor PAN/Bank Details";
            m_Distributorid = Distributorid.ToString();

            btnBankBrowse.Enabled = false;
            btnPanBrowse.Enabled = false;
            btnSave.Enabled = false;
            btnReset.Enabled = true;
            btnSearch.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            byte[] imgPanByte = ReadFile(lblPANImgPath.Text);
            byte[] imgBankByte = ReadFile(lblBankImgPath.Text);
            DistributorPANBank objDistPanBank = new DistributorPANBank();
            string err="";
            try
            {
                if (m_Distributorid == null || m_Distributorid == "")
                {
                    MessageBox.Show(Common.GetMessage("INF0001",m_Distributorid), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if ((lblPANImgPath.Text  == null || lblPANImgPath.Text =="") && (lblBankImgPath.Text  == null || lblBankImgPath.Text ==""))
                {
                    MessageBox.Show(Common.GetMessage("INF0119", "Image should be save","",""), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                DialogResult dgReasult = MessageBox.Show(Common.GetMessage("INF0027"), Common.GetMessage("10001"), MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (DialogResult.OK == dgReasult)
                {

                    Boolean isSuccess = objDistPanBank.Save(m_Distributorid, imgPanByte, imgBankByte, "Usp_DistPANBankSave", ref err);
                    this.SearchImage(m_Distributorid);
                
                    if (isSuccess == true)
                    {
                        MessageBox.Show(Common.GetMessage("8001"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnBankBrowse.Enabled = false;
                        btnPanBrowse.Enabled = false;
                        btnSave.Enabled = false;
                        btnReset.Enabled = true;
                        btnSearch.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show(err, Common.GetMessage("10001"), MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        btnBankBrowse.Enabled = true;
                        btnPanBrowse.Enabled = true;
                        btnSave.Enabled = true;
                        btnReset.Enabled = false;
                        btnSearch.Enabled = false;

                        return;
                    }
                }
            }
            catch (Exception ex) 
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPanBrowse_Click(object sender, EventArgs e)
        {
            lblPANImgPath.Text = getImageFileName();
            picPAN.Load(lblPANImgPath.Text);
        }

        private string getImageFileName()
        {
            try
            {
                string filepath = Environment.CurrentDirectory;
                opnFileDlgPan.FileName = "";
                opnFileDlgPan.Filter = "All Image files|*.jpg;*.jpeg;*.png;*.gif";
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

        private void btnBankBrowse_Click(object sender, EventArgs e)
        {
            lblBankImgPath.Text = getImageFileName();
            picBank.Load(lblBankImgPath.Text);
        }

        public byte[] ReadFile(String url)
        {
            try
            {
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


        private void frmDistributorPANBANK_Load(object sender, EventArgs e)
        {
            this.SearchImage(m_Distributorid);
            
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            picPAN.InitialImage = null;
            picBank.InitialImage = null;
            try
            {
                this.SearchImage(m_Distributorid);
                
                btnBankBrowse.Enabled = false;
                btnPanBrowse.Enabled = false;
                btnSave.Enabled = false;
                btnReset.Enabled = true;
                btnSearch.Enabled = true;

                this.Show();
                this.Focus();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            lblPANImgPath.Text = "";
            lblBankImgPath.Text = ""; 
            picPAN.Image = null;
            picBank.Image = null;

            btnBankBrowse.Enabled = true;
            btnPanBrowse.Enabled = true;
            btnSave.Enabled = true;
            btnReset.Enabled = false;
            btnSearch.Enabled = false;
            btnImgLast.Enabled = false;
            btnImgNext.Enabled = false;
            btnImgPrevious.Enabled = false;
            btnImgFirst.Enabled = false;
        }
        private void SearchImage(string Distributorid)
        {
            DistributorPANBank objDistPanBank = new DistributorPANBank();
            string errMessage = "";
            try
            {
                dtImage = objDistPanBank.Search(Distributorid, "Usp_DistributorPANBankSearch", ref errMessage);
                if (dtImage.Rows.Count > 0)
                {
                    this.picPAN.Image = setBmp((Byte[])dtImage.Rows[dtImage.Rows.Count - 1]["PANImage"]);
                    this.picBank.Image = setBmp((Byte[])dtImage.Rows[dtImage.Rows.Count - 1]["BankImage"]);

                    cntImage = dtImage.Rows.Count - 1;

                    btnImgLast.Enabled = false;
                    btnImgNext.Enabled = false;
                    btnImgPrevious.Enabled = true;
                    btnImgFirst.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private Bitmap setBmp(Byte [] byteRow)
        {
            MemoryStream mem = new MemoryStream(byteRow);
            Bitmap bmp = new Bitmap(mem);
            return bmp;
        }

        private void btnImgLast_Click(object sender, EventArgs e)
        {
            if (dtImage.Rows.Count > 0)
            {
                 
                this.picPAN.Image = setBmp((Byte[])dtImage.Rows[dtImage.Rows.Count - 1]["PANImage"]);
                this.picBank.Image = setBmp((Byte[])dtImage.Rows[dtImage.Rows.Count - 1]["BankImage"]);
                cntImage = dtImage.Rows.Count - 1;

                btnImgLast.Enabled = false;
                btnImgNext.Enabled = false;
                btnImgPrevious.Enabled = true;
                btnImgFirst.Enabled = true;
            }
        }

        private void btnImgNext_Click(object sender, EventArgs e)
        {
            if (dtImage.Rows.Count > 0)
            {
                cntImage++;
                if (cntImage < dtImage.Rows.Count)
                {
                    this.picPAN.Image = setBmp((Byte[])dtImage.Rows[cntImage]["PANImage"]);
                    this.picBank.Image = setBmp((Byte[])dtImage.Rows[cntImage]["BankImage"]);

                    if (cntImage == dtImage.Rows.Count - 1)
                    {
                        btnImgLast.Enabled = false;
                        btnImgNext.Enabled = false;
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
                    
                }
            }
        }

        private void btnImgFirst_Click(object sender, EventArgs e)
        {
            if (dtImage.Rows.Count > 0)
            {                
                this.picPAN.Image = setBmp((Byte[])dtImage.Rows[0]["PANImage"]);
                this.picBank.Image = setBmp((Byte[])dtImage.Rows[0]["BankImage"]);
                cntImage = 0;
                btnImgLast.Enabled = true;
                btnImgNext.Enabled = true;
                btnImgPrevious.Enabled = false;
                btnImgFirst.Enabled = false;
            }
        }

        private void btnImgPrevious_Click(object sender, EventArgs e)
        {
            if (dtImage.Rows.Count > 0)
            {
                cntImage--;
                if (cntImage >= 0)
                {
                    this.picPAN.Image = setBmp((Byte[])dtImage.Rows[cntImage]["PANImage"]);
                    this.picBank.Image = setBmp((Byte[])dtImage.Rows[cntImage]["BankImage"]);
                    if (cntImage == 0)
                    {
                        btnImgLast.Enabled = true;
                        btnImgNext.Enabled = true;
                        btnImgPrevious.Enabled = false;
                        btnImgFirst.Enabled = false;
                    }
                    else 
                    {
                        btnImgLast.Enabled = true;
                        btnImgNext.Enabled = true;
                        btnImgPrevious.Enabled = true;
                        btnImgFirst.Enabled = true;
                    }
                    
                }
            }
        }

        
       
    }
}
