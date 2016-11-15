using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CoreComponent.Core.BusinessObjects;
namespace POSClient.UI
{
    public partial class BonusCheque : BaseChildForm
    {
        public BonusCheque()
        {
            InitializeComponent();
        }
        public POSClient.BusinessObjects.CO Order { get; set; }
        private POSClient.BusinessObjects.BonusCheque m_Cheque;
        public POSClient.BusinessObjects.BonusCheque ReturnObject { get; set; }

        private void BonusCheque_Load(object sender, EventArgs e)
        {
            btnOk.Enabled = false;           
            btnCancel.CausesValidation = false;            
        }
            
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
     
        private void txtChequeNo_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (txtChequeNo.Text.Trim().Length > 0)
                {
                    m_Cheque = new POSClient.BusinessObjects.BonusCheque();
                    m_Cheque.ChequeNo = txtChequeNo.Text.Trim();                    
                    m_Cheque.GetChequeDetail();
                    
                    if (m_Cheque.DistributorId != 0)
                    {
                        if (Order.LogNo == string.Empty)
                        {
                            if (m_Cheque.DistributorId == Order.DistributorId)
                            {
                                if (m_Cheque.Status == (int)Common.BonusChequeStatus.New)
                                {
                                    SetDetails(m_Cheque);
                                }
                                else
                                {
                                    ClearFields();
                                    MessageBox.Show(Common.GetMessage("VAL0131"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            else
                            {
                                ClearFields();                                
                                MessageBox.Show(Common.GetMessage("VAL0112"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            int logType = 0;
                            DataTable dtLogType = Common.ParameterLookup(Common.ParameterType.LogType,new ParameterFilter(Order.LogValue,Common.INT_DBNULL,Common.INT_DBNULL,Common.INT_DBNULL));
                            if(dtLogType != null && dtLogType.Rows.Count == 1)
                                logType = Convert.ToInt32(dtLogType.Rows[0][0]);
                            
                            if(logType == (int)Common.COLogType.TeamOrder)
                            {                               
                                POSClient.BusinessObjects.CO objCO = new POSClient.BusinessObjects.CO();
                                objCO.LogNo = Order.LogValue;
                                objCO.Status = Common.INT_DBNULL;
                                string errorMessage = string.Empty;
                                List<POSClient.BusinessObjects.CO> lstCO = objCO.Search(ref errorMessage);
                                var query = from p in lstCO where p.DistributorId == m_Cheque.DistributorId && p.Status != (int)Common.OrderStatus.Cancelled && ((m_Cheque.TeamOrderNo == string.Empty)|| (p.LogNo == m_Cheque.TeamOrderNo)) select p;
                                if (query.ToList().Count > 0 && (m_Cheque.Status == (int)Common.BonusChequeStatus.New || m_Cheque.Status == (int)Common.BonusChequeStatus.PartialUsed))
                                {
                                    if(m_Cheque.CanBeUsedAgain == 1)
                                        SetDetails(m_Cheque);
                                    else
                                        MessageBox.Show(Common.GetMessage("VAL0131"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    ClearFields();  
                                    MessageBox.Show(Common.GetMessage("VAL0113"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            else if (logType == (int)Common.COLogType.Log)
                            {
                                if (m_Cheque.DistributorId == Order.DistributorId)
                                {
                                    if (m_Cheque.Status == (int)Common.BonusChequeStatus.New)
                                    {
                                        SetDetails(m_Cheque);
                                    }
                                    else
                                    {
                                        ClearFields();
                                        MessageBox.Show(Common.GetMessage("VAL0131"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                }
                                else
                                {
                                    ClearFields();
                                    MessageBox.Show(Common.GetMessage("VAL0112"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                        }
                    }
                    else
                    {
                        ClearFields();                       
                        MessageBox.Show(Common.GetMessage("VAL0114"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    EnableDisableButton(m_Cheque.ExpiryDate);  
                }
                else
                {
                    ClearFields();                    
                    MessageBox.Show(Common.GetMessage("VAL0114"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }                                     
            }            
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void SetDetails(POSClient.BusinessObjects.BonusCheque cheque)
        {
            txtBankName.Text = cheque.BankName;
            txtName.Text = cheque.Name;
            txtTotalAmount.Text = cheque.Amount.ToString("0.00");
            txtUsedAmount.Text = cheque.UsedAmount.ToString("0.00");
            txtBalanceAmount.Text = cheque.BalanceAmount.ToString("0.00");
            txtOrderNo.Text = cheque.OrderNo==string.Empty?cheque.TeamOrderNo:cheque.OrderNo;
            txtUseAmount.Text = (cheque.BalanceAmount > this.Order.TotalAmount ? this.Order.TotalAmount.ToString("0.00") : cheque.BalanceAmount.ToString("0.00"));
            chkCanBeUsed.Checked = Convert.ToBoolean(cheque.CanBeUsedAgain);
            txtExpiryDate.Text = (Convert.ToDateTime(cheque.ExpiryDate)).ToString(Common.DTP_DATE_FORMAT);

            if (Convert.ToDateTime(cheque.ExpiryDate) < DateTime.Today)
                MessageBox.Show(Common.GetMessage("VAL0115"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void EnableDisableButton(string expDate)

        {
            if ((m_Cheque.CanBeUsedAgain == -1 || m_Cheque.CanBeUsedAgain == 1) && Convert.ToDateTime(expDate) >= DateTime.Today && txtName.Text.Trim().Length > 0)
            {
                btnOk.Enabled = true;
                btnOk.Focus();
            }
            else
                btnOk.Enabled = false;
        }

        private void ClearFields()
        {
            txtBankName.Text = string.Empty;
            txtName.Text = string.Empty;
            txtTotalAmount.Text = string.Empty;
            txtBalanceAmount.Text = string.Empty;
            txtExpiryDate.Text = string.Empty;
            txtUsedAmount.Text = string.Empty;
            txtOrderNo.Text = string.Empty;
            txtUseAmount.Text = string.Empty;
            chkCanBeUsed.CheckState = CheckState.Unchecked;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (txtChequeNo.Text.Trim().Equals(string.Empty))
            {
                txtChequeNo.Focus();
                return;
            }
            if(!String.IsNullOrEmpty(txtBalanceAmount.Text.Trim()))
            {
                //add validation 
                if (Convert.ToDecimal(txtUseAmount.Text.Trim()) > Convert.ToDecimal(txtBalanceAmount.Text.Trim()))
                {
                    MessageBox.Show(Common.GetMessage("40031"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtUseAmount.Focus();
                    return;
                }
                if ((!m_Cheque.TeamOrderNo.Equals(string.Empty) && (m_Cheque.CanBeUsedAgain == 1)) && (string.IsNullOrEmpty(Order.LogNo) || (Order.LogNo != m_Cheque.TeamOrderNo)))
                {
                    MessageBox.Show(Common.GetMessage("40030", m_Cheque.TeamOrderNo), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                //1. Use amount can not be greater than balance amount
                //2. if kit order <> '' and m_currentorder type is not kit order, can not use
                //3. 
                m_Cheque.UseAmount = Convert.ToDecimal(txtUseAmount.Text.Trim());
                ReturnObject = m_Cheque;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }            
        }

        private void txtChequeNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtChequeNo_Validating(null, null);
            }
        }       
    }
}
