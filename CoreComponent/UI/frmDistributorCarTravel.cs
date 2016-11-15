using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using CoreComponent.UI;
using CoreComponent.BusinessObjects;
using CoreComponent.Core.BusinessObjects;
using CoreComponent.Hierarchies.BusinessObjects;
using AuthenticationComponent.BusinessObjects;

namespace CoreComponent.UI
{

    public partial class frmDistributorCarTravel : CoreComponent.Core.UI.Transaction
    {

        private string m_distributorIds = string.Empty;
        List<DistributorPayment> m_lstDistributorPayment;
        DistributorPayment m_distributorPayment;
        string m_distributorId = string.Empty;
        int m_selectedRowIndex = Common.INT_DBNULL;
        string m_accAmount = string.Empty;

        public frmDistributorCarTravel()
        {
            try
            {
                InitializeComponent();

                lblPageTitle.Text = "Car / Travel Fund";

                DataGridView dgvSearchNew = Common.GetDataGridViewColumns(dgvDistributorCarFund, Environment.CurrentDirectory + "\\App_Data\\GridViewDefinition.xml");
                dgvDistributorCarFund.AutoGenerateColumns = false;
                dgvDistributorCarFund.DataSource = null;

                DataGridView dgvSearchNew1 = Common.GetDataGridViewColumns(dgvDistributorTravelFund, Environment.CurrentDirectory + "\\App_Data\\GridViewDefinition.xml");
                dgvDistributorTravelFund.AutoGenerateColumns = false;
                dgvDistributorTravelFund.DataSource = null;

                txtDistributorId.Focus();
                InitializeControls();
                m_lstDistributorPayment = new List<DistributorPayment>();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void SearchDistributor()
        {
            DistributorCarTravel dct = new DistributorCarTravel();
            dct.DistributorId = txtDistributorId.Text;
            dct.DistributorName = txtDistributorName.Text;

            List<DistributorCarTravel> lst = DistributorCarTravel.GetDistributorCarFundInfo(Common.ToXml(dct));

            CurrencyManager m_bindingMgr;
            if (lst != null)
            {
                m_bindingMgr = (CurrencyManager)this.BindingContext[lst];
                m_bindingMgr.Refresh();
            }
            dgvDistributorCarFund.DataSource = new List<DistributorCarTravel>();
            if (lst != null && lst.Count > 0)
            {
                dgvDistributorCarFund.DataSource = lst;
                dgvDistributorCarFund.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;

                //for (int i = 0; i < dgvDistributorCarFund.Rows.Count; i++)
                //{
                //    if (dgvDistributorCarFund.Rows[i].Cells[3].GetType() == typeof(DataGridViewCheckBoxCell))
                //    {
                //        if (Convert.ToInt32(dgvDistributorCarFund.Rows[i].Cells[3].Value) == 1)
                //            dgvDistributorCarFund.Rows[i].Cells[3].ReadOnly = true;

                //    }
                //}
                m_bindingMgr = (CurrencyManager)this.BindingContext[lst];
                m_bindingMgr.Refresh();
            }
        }

        /// <summary>
        /// Call fn. Search to Bind Grid 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                SearchDistributor();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Call fn. AddContact to Add Contact 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddDetails_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_distributorId != null && m_distributorId.Length > 0)
                {
                    string month, year;

                    if (ValidateTravel())
                    {
                        if (Convert.ToDecimal(txtAmountAccumulated.Text) > Convert.ToDecimal(txtAmountTotal.Text))
                        {
                            MessageBox.Show(Common.GetMessage("INF0034", "Accumulated Amount", "Total Amount"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        DistributorPayment dp = new DistributorPayment();
                        dp.DistributorId = string.Empty;
                        month = (cmbStatus.SelectedValue.ToString().Length < 2 ? "0" + cmbStatus.SelectedValue.ToString() : cmbStatus.SelectedValue.ToString());
                        year = cmbYear.SelectedValue.ToString();
                        dp.Amount = "0";

                        dp.Month = month;
                        dp.Year = year;
                        if (txtAmountAccumulated.Text.ToString().Length > 0)
                        {
                            //decimal d = Convert.ToDecimal(txtAmountAccumulated.Text);
                            m_distributorPayment.Amount = txtAmountTotal.Text;
                            m_distributorPayment.AccAmount = Convert.ToDecimal(txtAmountAccumulated.Text);
                            m_distributorPayment.Month = month;
                            m_distributorPayment.Year = year;


                            string errorMessage = string.Empty;
                            bool result = DistributorPayment.TravelFundSave(Common.ToXml(m_distributorPayment), ref errorMessage);

                            if (errorMessage.Equals(string.Empty))
                            {
                                m_lstDistributorPayment[m_selectedRowIndex] = m_distributorPayment;

                                MessageBox.Show(Common.GetMessage("8001"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);

                                dp = new DistributorPayment();
                                dp.DistributorId = m_distributorPayment.DistributorId;
                                dp.Month = (cmbStatus.SelectedValue.ToString().Length < 2 ? "0" + cmbStatus.SelectedValue.ToString() : cmbStatus.SelectedValue.ToString());
                                dp.Year = cmbYear.SelectedValue.ToString();
                                dp.Amount = "0";
                                SearchTravelFundInfo(Common.ToXml(dp));
                            }
                            else if (errorMessage == "INF0211")
                            {
                                m_distributorPayment.AccAmount = Convert.ToDecimal(m_accAmount);
                                m_lstDistributorPayment[m_selectedRowIndex] = m_distributorPayment;
                                MessageBox.Show(Common.GetMessage(errorMessage, cmbStatus.Text + "-" + cmbYear.Text), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                m_distributorPayment.AccAmount = Convert.ToDecimal(m_accAmount);
                                MessageBox.Show(Common.GetMessage(errorMessage), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
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

        /// <summary>
        ///  Call fn. ResetContactControl to Reset Contact Controls
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearDetails_Click(object sender, EventArgs e)
        {
            try
            {
                ClearTravelTab();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// reset Travel Tab
        /// </summary>
        private void ClearTravelTab()
        {
            m_accAmount = string.Empty;
            m_distributorId = string.Empty;
            m_distributorPayment = null;
            m_lstDistributorPayment = null;
            cmbStatus.SelectedIndex = 0;
            cmbYear.SelectedIndex = 0;
            txtSDistributorId.Text = string.Empty;
            txtSDistributorName.Text = string.Empty;
            txtAmountTotal.Text = string.Empty;
            txtAmountAccumulated.Text = string.Empty;
            m_lstDistributorPayment = new List<DistributorPayment>();
            dgvDistributorTravelFund.DataSource = new List<DistributorCarTravel>();

        }

        private bool ValidateTravel()
        {
            bool ret = true;
            StringBuilder sb = new StringBuilder();
            if (String.IsNullOrEmpty(txtSDistributorId.Text))
            {
                ret = false;
                errTravel.SetError(txtSDistributorId, Common.GetMessage("VAL0001", lblSearchDistributorId.Text.Trim().Substring(0, lblSearchDistributorId.Text.Trim().Length - 2)));
                sb.Append(Common.GetMessage("VAL0001", lblSearchDistributorId.Text.Trim()));
            }
            else if (!Validators.IsValidAmount(txtAmountAccumulated.Text.Trim()))
            {
                ret = false;
                errTravel.SetError(txtAmountAccumulated, Common.GetMessage("VAL0001", lblAmountAccumulated.Text.Trim().Substring(0, lblAmountAccumulated.Text.Trim().Length - 2)));
                sb.Append(Common.GetMessage("VAL0001", lblAmountAccumulated.Text.Trim()));
            }
            else if (!Validators.IsValidAmount(txtAmountTotal.Text.Trim()))
            {
                ret = false;
                errTravel.SetError(txtAmountAccumulated, Common.GetMessage("VAL0001", lblAmountTotal.Text.Trim().Substring(0, lblAmountTotal.Text.Trim().Length - 2)));
                sb.Append(Common.GetMessage("VAL0001", lblAmountTotal.Text.Trim()));
            }

            if (sb.Length > 0)
            {
                ret = false;
                MessageBox.Show(sb.ToString());
            }
            return ret;
        }

        /// <summary>
        /// Call fn. ResetControl to Reset Search Controls
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearchReset_Click(object sender, EventArgs e)
        {
            try
            {
                txtDistributorId.Text = string.Empty;
                txtDistributorName.Text = string.Empty;
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSaveDistributor_Click(object sender, EventArgs e)
        {
            string errorMessage = string.Empty;

            DistributorCarTravel.Save(m_distributorIds, ref errorMessage);
            if (errorMessage.Equals(string.Empty))
            {
                MessageBox.Show(Common.GetMessage("8001"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                SearchDistributor();
            }
            else
                MessageBox.Show(Common.GetMessage(errorMessage), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);


        }

        private void dgvDistributorCarFund_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if ((e.RowIndex >= 0) && (dgvDistributorCarFund.Rows[e.RowIndex].Cells[e.ColumnIndex].GetType() == typeof(DataGridViewCheckBoxCell)))
                {
                    string start_string = string.Empty;
                    string last_string = string.Empty;

                    string distributorId = dgvDistributorCarFund.Rows[e.RowIndex].Cells["DistributorId"].EditedFormattedValue.ToString();
                    if (dgvDistributorCarFund.Rows[e.RowIndex].Cells[e.ColumnIndex].EditedFormattedValue.ToString().ToLower() == "true".ToString().ToLower().Trim())
                    {
                        m_distributorIds = m_distributorIds + (m_distributorIds.Length > 0 ? "," : "") + dgvDistributorCarFund.Rows[e.RowIndex].Cells["DistributorId"].EditedFormattedValue.ToString();

                    }
                    else
                    {
                        int index = Common.INT_DBNULL;
                        index = m_distributorIds.IndexOf(distributorId);
                        if (index >= 0)
                        {
                            start_string = m_distributorIds.Substring(0, index);
                            m_distributorIds = start_string + m_distributorIds.Substring(index + distributorId.Length, m_distributorIds.Length - index - distributorId.Length);
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

        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                txtDistributorName.Text = string.Empty;
                txtDistributorId.Text = string.Empty;
                dgvDistributorCarFund.DataSource = null;
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvDistributorCarFund_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            //If first column is the checkbox column
            //if (e.ColumnIndex == this.dgvDistributorCarFund.Columns["PayCarFund"].Index && e.RowIndex > -1 && e.RowIndex != this.dgvDistributorCarFund.NewRowIndex)
            //{
            //    if (this.dgvDistributorCarFund[3, e.RowIndex].Value.ToString().ToLower() == "true")
            //    {
            //        e.PaintBackground(e.CellBounds, true);
            //        Rectangle r = e.CellBounds;
            //        r.Width = 13;
            //        r.Height = 13;
            //        r.X += e.CellBounds.Width / 2 - 7;
            //        r.Y += e.CellBounds.Height / 2 - 7;
            //        ControlPaint.DrawCheckBox(e.Graphics, r, ButtonState.Checked);
            //        ControlPaint.DrawCheckBox(e.Graphics, r, ButtonState.Inactive);
            //        e.Handled = true;
            //    }
            //}
        }

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

                DataTable dtMonthEnd = Common.ParameterLookup(Common.ParameterType.DistributorMonthEnd, new ParameterFilter(string.Empty, 0, 0, 0));

                if (dtMonthEnd != null && dtMonthEnd.Rows.Count > 0)
                {
                    cmbStatus.SelectedValue = dtMonthEnd.Rows[0]["Month"].ToString();
                    cmbYear.SelectedValue = dtMonthEnd.Rows[0]["Year"].ToString();
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void SearchTravelFundInfo(string xml)
        {

            m_lstDistributorPayment = DistributorPayment.GetDistributorTravelFund(xml);

            CurrencyManager m_bindingMgr;
            if (m_lstDistributorPayment != null)
            {
                m_bindingMgr = (CurrencyManager)this.BindingContext[m_lstDistributorPayment];
                m_bindingMgr.Refresh();
            }
            dgvDistributorTravelFund.DataSource = new List<DistributorCarTravel>();
            if (m_lstDistributorPayment != null && m_lstDistributorPayment.Count > 0)
            {
                dgvDistributorTravelFund.DataSource = m_lstDistributorPayment;
                dgvDistributorTravelFund.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;

                m_bindingMgr = (CurrencyManager)this.BindingContext[m_lstDistributorPayment];
                m_bindingMgr.Refresh();
            }
            else
                MessageBox.Show(Common.GetMessage("8002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSSearch_Click(object sender, EventArgs e)
        {
            try
            {
                DistributorPayment dp = new DistributorPayment();
                dp.DistributorId = string.Empty;
                dp.Month = (cmbStatus.SelectedValue.ToString().Length < 2 ? "0" + cmbStatus.SelectedValue.ToString() : cmbStatus.SelectedValue.ToString());
                dp.Year = cmbYear.SelectedValue.ToString();
                dp.Amount = "0";
                dp.DistributorId = txtSDistributorId.Text.Trim();
                dp.DistributorName = txtSDistributorName.Text.Trim();
                SearchTravelFundInfo(Common.ToXml(dp));
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void dgvDistributorTravelFund_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && m_lstDistributorPayment != null && m_lstDistributorPayment.Count > 0)
                {
                    m_distributorPayment = m_lstDistributorPayment[e.RowIndex];
                    m_distributorId = dgvDistributorTravelFund.Rows[e.RowIndex].Cells["DistributorId"].Value.ToString().Trim();
                    txtSDistributorId.Text = m_distributorId.ToString();
                    txtSDistributorName.Text = dgvDistributorTravelFund.Rows[e.RowIndex].Cells["DistributorName"].Value.ToString().Trim();
                    txtAmountTotal.Text = dgvDistributorTravelFund.Rows[e.RowIndex].Cells["Amount"].Value.ToString().Trim();
                    txtAmountAccumulated.Text = dgvDistributorTravelFund.Rows[e.RowIndex].Cells["AccAmount"].Value.ToString().Trim();
                    m_selectedRowIndex = e.RowIndex;
                    m_accAmount = txtAmountAccumulated.Text;
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtAmountAccumulated_Validated(object sender, EventArgs e)
        {
            //m_accAmount = txtAmountAccumulated.Text;
        }

    }
}
