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
using AuthenticationComponent.BusinessObjects;

namespace CoreComponent.UI
{
    public partial class frmTaxPayment : CoreComponent.Core.UI.Transaction
    {
        #region Global Variables

        List<TaxPaymentHeader> m_lstTaxPaymentHeader = null;
        int m_ChallanId = 0;
        bool m_dgvChallanSelect = true;
        bool m_isSaveAvailable = false;


        #endregion

        #region C'tor

        public frmTaxPayment()
        {
            InitializeComponent();
            this.tabControlTransaction.TabPages[0].Text = "Challan Info.";
            this.tabControlTransaction.TabPages[1].Text = "Payment Info.";
            this.lblPageTitle.Text = "Tax Payment";
            dtpDepositDate.Format = DateTimePickerFormat.Custom;
            dtpDepositDate.CustomFormat = Common.DTP_DATE_FORMAT;
            FillCombo();
            dgvChallan = Common.GetDataGridViewColumns(dgvChallan, Environment.CurrentDirectory + "\\App_Data\\GridViewDefinition.xml");
            m_lstTaxPaymentHeader = new List<TaxPaymentHeader>();
            m_isSaveAvailable = true;
        }


        #endregion 

        #region Methods

        private void FillCombo()
        {
            DataTable dtFinYears = Common.ParameterLookup(Common.ParameterType.FinancialYears, new ParameterFilter("", 0, 0, 0));
            cmbFinancialYear.DataSource = dtFinYears;
            cmbFinancialYear.DisplayMember = "YearName";
            cmbFinancialYear.ValueMember = "YearId";

            cmbAddFinancialYear.DataSource = dtFinYears;
            cmbAddFinancialYear.DisplayMember = "YearName";
            cmbAddFinancialYear.ValueMember = "YearId";

            DataTable dtQuarters = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter("FINANCIALQUARTERS", 0, 0, 0));
            cmbQuarters.DataSource = dtQuarters;
            cmbQuarters.DisplayMember = Common.KEYVALUE1;
            cmbQuarters.ValueMember = Common.KEYCODE1;

            cmbAddQuarters.DataSource = dtQuarters;
            cmbAddQuarters.DisplayMember = Common.KEYVALUE1;
            cmbAddQuarters.ValueMember = Common.KEYCODE1;
        }

        private void btnSearchReset_Click(object sender, EventArgs e)
        {
            try
            {
                ResetSearchForm();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ResetSearchForm()
        {
            dgvChallan.DataSource = null;
            cmbFinancialYear.SelectedValue = Common.INT_DBNULL;
            cmbQuarters.SelectedValue = Common.INT_DBNULL;
            txtChallanNO.Text = string.Empty;
            txtChequeNo.Text = string.Empty;
            txtBSRCode.Text = string.Empty;
            txtAcknowledgeNo.Text = string.Empty;
            txtDepositAmount.Text = string.Empty;
            dtpDepositDate.Value = DateTime.Today;
            btnChallanSave.Enabled = m_isSaveAvailable;
            errProvChallan.Clear();
        }

        private void btnChallanSave_Click(object sender, EventArgs e)
        {
            try
            {
                SaveChallan();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveChallan()
        {
            ValidateChallan();
            StringBuilder sb = getErrors();
            if (sb.ToString().Length > 0)
            {
                MessageBox.Show(sb.ToString(), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult dr = MessageBox.Show(Common.GetMessage("5010","save"), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                TaxPaymentHeader objHeader = new TaxPaymentHeader();
                objHeader.ChallanId = m_ChallanId;
                objHeader.FinancialYear = cmbFinancialYear.SelectedValue.ToString();
                objHeader.Quarter = Convert.ToInt32(cmbQuarters.SelectedValue);
                objHeader.ChallanNo = txtChallanNO.Text.Trim();
                objHeader.ChequeNo = txtChequeNo.Text.Trim();
                objHeader.BSRCode = txtBSRCode.Text.Trim();
                objHeader.Depositdate = dtpDepositDate.Value;
                objHeader.DepositedAmount = Convert.ToDecimal(txtDepositAmount.Text.Trim());
                objHeader.AcknowledgementNo = txtAcknowledgeNo.Text.Trim();
                
                string errorMessage = string.Empty;
                bool result = objHeader.SaveChallan(ref errorMessage);

                if (result)
                {
                    MessageBox.Show(Common.GetMessage("8001"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ResetSearchForm();
                    SearchChallan();
                }
                else
                {
                    MessageBox.Show(errorMessage);
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                SearchChallan();
            }

            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void SearchChallan()
        {
            TaxPaymentHeader objHeader = new TaxPaymentHeader();
            string errorMessage = string.Empty;
            if (m_lstTaxPaymentHeader == null)
                m_lstTaxPaymentHeader = new List<TaxPaymentHeader>();
            m_lstTaxPaymentHeader = objHeader.SearchChallan(cmbFinancialYear.SelectedValue.ToString(), Convert.ToInt32(cmbQuarters.SelectedValue), txtAcknowledgeNo.Text, txtChallanNO.Text, ref errorMessage);
            if (errorMessage.Trim().Length == 0)
            {
                if (m_lstTaxPaymentHeader != null && m_lstTaxPaymentHeader.Count > 0)
                {
                    m_dgvChallanSelect = false;
                    dgvChallan.DataSource = m_lstTaxPaymentHeader;
                    dgvChallan.ClearSelection();
                    m_dgvChallanSelect = true;
                }
                else
                {
                    MessageBox.Show(Common.GetMessage("8002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show(errorMessage);
            }
        }

        private void dgvChallan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && dgvChallan.Columns[e.ColumnIndex].CellType == typeof(DataGridViewImageCell))
            {
                EditChallan(e.RowIndex);
                btnChallanSave.Enabled = true;
            }
        }

        private void EditChallan(int iIndex)
        {
            if (m_dgvChallanSelect)
            {
                TaxPaymentHeader objHeader = m_lstTaxPaymentHeader[iIndex];
                m_ChallanId = objHeader.ChallanId;
                cmbFinancialYear.SelectedValue = objHeader.FinancialYear;
                cmbQuarters.SelectedValue = objHeader.Quarter;
                txtChallanNO.Text = objHeader.ChallanNo;
                txtChequeNo.Text = objHeader.ChequeNo;
                txtBSRCode.Text = objHeader.BSRCode;
                txtDepositAmount.Text = objHeader.DepositedAmount.ToString();
            }
            
        }

        private void ValidateChallan()
        {
            if (cmbFinancialYear.SelectedValue.ToString() == "-1")
            {
                errProvChallan.SetError(cmbFinancialYear, Common.GetMessage("VAL0002", lblFinancialYear.Text.Substring(0,lblFinancialYear.Text.Length - 1)));
            }
            else
                errProvChallan.SetError(cmbFinancialYear, string.Empty);

            if (Convert.ToInt32(cmbQuarters.SelectedValue) == -1)
            {
                errProvChallan.SetError(cmbQuarters, Common.GetMessage("VAL0002", lblQuarter.Text.Substring(0,lblQuarter.Text.Length-1)));
            }
            else
                errProvChallan.SetError(cmbQuarters, string.Empty);

            if (String.IsNullOrEmpty(txtChallanNO.Text.Trim()))
            {
                errProvChallan.SetError(txtChallanNO, Common.GetMessage("VAL0001", lblChallanNo.Text.Substring(0,lblChallanNo.Text.Length-2)));
            }
            else
                errProvChallan.SetError(txtChallanNO, string.Empty);

            if (String.IsNullOrEmpty(txtChequeNo.Text.Trim()))
            {
                errProvChallan.SetError(txtChequeNo, Common.GetMessage("VAL0001", lblChequeNo.Text.Substring(0,lblChequeNo.Text.Length - 1)));
            }
            else
                errProvChallan.SetError(txtChequeNo, string.Empty);

            if (String.IsNullOrEmpty(txtBSRCode.Text.Trim()))
            {
                errProvChallan.SetError(txtBSRCode, Common.GetMessage("VAL0001", lblBSRCode.Text.Substring(0,lblBSRCode.Text.Length - 1)));
            }
            else
                errProvChallan.SetError(txtBSRCode, string.Empty);

            if (!Validators.IsValidAmount(txtDepositAmount.Text))
            {
                errProvChallan.SetError(txtDepositAmount, Common.GetMessage("VAL0001", lblDepositAmount.Text.Substring(0,lblDepositAmount.Text.Length-1)));
            }
            else
                errProvChallan.SetError(txtDepositAmount, string.Empty);

            if (dtpDepositDate.Value > DateTime.Now)
            {
                errProvChallan.SetError(dtpDepositDate, Common.GetMessage("VAL0001", lblDepositDate.Text.Substring(0,lblDepositDate.Text.Length - 1)));
            }
            else
                errProvChallan.SetError(dtpDepositDate, string.Empty);
        }

        private StringBuilder getErrors()
        {
            StringBuilder sb = new StringBuilder();
            if (!String.IsNullOrEmpty(errProvChallan.GetError(cmbFinancialYear)))
            {
                sb.Append(errProvChallan.GetError(cmbFinancialYear));
                sb.AppendLine();
            }
            if (!String.IsNullOrEmpty(errProvChallan.GetError(cmbQuarters)))
            {
                sb.Append(errProvChallan.GetError(cmbQuarters));
                sb.AppendLine();
            }
            if (!String.IsNullOrEmpty(errProvChallan.GetError(txtChallanNO)))
            {
                sb.Append(errProvChallan.GetError(txtChallanNO));
                sb.AppendLine();
            }
            if (!String.IsNullOrEmpty(errProvChallan.GetError(txtBSRCode)))
            {
                sb.Append(errProvChallan.GetError(txtBSRCode));
                sb.AppendLine();
            }

            if (!String.IsNullOrEmpty(errProvChallan.GetError(txtChequeNo)))
            {
                sb.Append(errProvChallan.GetError(txtChequeNo));
                sb.AppendLine();
            }

            if (!String.IsNullOrEmpty(errProvChallan.GetError(dtpDepositDate)))
            {
                sb.Append(errProvChallan.GetError(dtpDepositDate));
                sb.AppendLine();
            }
           
            if (!String.IsNullOrEmpty(errProvChallan.GetError(txtDepositAmount)))
            {
                sb.Append(errProvChallan.GetError(txtDepositAmount));
                sb.AppendLine();
            }
            sb = Common.ReturnErrorMessage(sb);
            return sb;  
        }

        private void dgvChallan_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvChallan.CurrentRow.Index > -1)
            {
                EditChallan(dgvChallan.CurrentRow.Index);
                btnChallanSave.Enabled = false;
            }
        }

        #endregion
    }
}
