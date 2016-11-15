using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CoreComponent.Core.BusinessObjects;
using CoreComponent.BusinessObjects;
using PdfSharp;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using PdfSharp.Drawing;
using System.IO;
using System.Diagnostics;

namespace CoreComponent.UI
{
    public partial class frmDistributorsBonusRptScreen : CoreComponent.Core.UI.HierarchyTemplate
    {
        List<DistributorsBonusInfo> m_listOfDistBonus;
        private bool IsSingleFile;
        public frmDistributorsBonusRptScreen()
        {
            InitializeComponent();
            this.Size = new Size(870, 703);
            this.btnSave.Visible = false;
            this.lblPageTitle.Text = "Bonus Statement Directors ";
            PopulateIndtpBusinessMonth();
            PopulateCountryInCountryCombo();
            PopulateDistLevelInDistLevelCombo();

            GridInitialize();
        }

        private void PopulateIndtpBusinessMonth()
        {
            DataTable dtMonthEnd = Common.ParameterLookup(Common.ParameterType.DistributorMonthEnd, new ParameterFilter(string.Empty, 0, 0, 0));
            //cmbDistributorLevel.DataSource = dtDistributorLevel;
            if (dtMonthEnd.Rows[0].ItemArray.Length > 0)
                dtpBusinessMonth.Value = new DateTime(Convert.ToInt32(dtMonthEnd.Rows[0].ItemArray[1].ToString()), Convert.ToInt32(dtMonthEnd.Rows[0].ItemArray[0].ToString()), 1);
            else
                dtpBusinessMonth.Value = DateTime.Today;

        }

        private void PopulateDistLevelInDistLevelCombo()
        {
            DataTable dtDistributorLevel = Common.ParameterLookup(Common.ParameterType.DistributorLevel, new ParameterFilter(string.Empty, 0, 0, 0));
            cmbDistributorLevel.DataSource = dtDistributorLevel;
            cmbDistributorLevel.DisplayMember = "LevelName";
            cmbDistributorLevel.ValueMember = "LevelId";

        }


        private void cmbState_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbState.SelectedValue.ToString() == "-1")
            {
                DataTable dtCity = Common.ParameterLookup(Common.ParameterType.City, new ParameterFilter(string.Empty, 0, 0, 0));
                cmbCity.DataSource = dtCity;
                cmbCity.DisplayMember = "CityName";
                cmbCity.ValueMember = "CityId";
            }
            else if (cmbState.SelectedValue.ToString() != "System.Data.DataRowView")
            {
                AddSelectStateCityInCombo(cmbCity);
            }
        }

        private void pnlSearchHeader_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void chkPB_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkDB_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkCF_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkHF_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkTF_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkLOB_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void PopulateCountryInCountryCombo()
        {
            DataTable dtCountry = Common.ParameterLookup(Common.ParameterType.Country, new ParameterFilter(string.Empty, 0, 0, 0));
            cmbCountry.DataSource = dtCountry;
            cmbCountry.DisplayMember = "CountryName";
            cmbCountry.ValueMember = "CountryId";
            cmbCountry.SelectedValue = -1;
            AddSelectItemInCombo(cmbState);
            AddSelectStateCityInCombo(cmbCity);
        }



        private void AddSelectItemInCombo(ComboBox cmb)
        {
            DataTable dtState = Common.ParameterLookup(Common.ParameterType.State, new ParameterFilter(string.Empty, Convert.ToInt32(cmbCountry.SelectedValue), 0, 0));
            cmb.DataSource = dtState;
            cmb.DisplayMember = "StateName";
            cmb.ValueMember = "StateId";
            cmb.SelectedValue = -1;
            AddSelectStateCityInCombo(cmbCity);
        }
        private void AddSelectStateCityInCombo(ComboBox cmb)
        {
            DataTable dtCity = Common.ParameterLookup(Common.ParameterType.City, new ParameterFilter(string.Empty, Convert.ToInt32(cmbState.SelectedValue), 0, 0));
            cmb.DataSource = dtCity;
            cmb.DisplayMember = "CityName";
            cmb.ValueMember = "CityID";            
        }

        private void cmbCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCountry.SelectedValue.ToString() == "-1")
            {
                DataTable dtState = Common.ParameterLookup(Common.ParameterType.State, new ParameterFilter(string.Empty, 0, 0, 0));
                cmbState.DataSource = dtState;
                cmbState.DisplayMember = "StateName";
                cmbState.ValueMember = "StateId";
            }
            else if (cmbCountry.SelectedValue.ToString() != "System.Data.DataRowView")
            {
                AddSelectItemInCombo(cmbState);
            }
        }

        private void dgvDistriBonusSearch_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            fncCheckOnOff();
        }

        void GridInitialize()
        {
            dgvDistriBonusSearch.AutoGenerateColumns = false;
            dgvDistriBonusSearch.DataSource = null;
            DataGridView dgvDetailsNew = Common.GetDataGridViewColumns(dgvDistriBonusSearch, Environment.CurrentDirectory + "\\App_Data\\GridViewDefinition.xml");


        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtDistributorId.Text.Trim()))
                {
                    if (!Validators.IsInt32(this.txtDistributorId.Text))
                    {
                        MessageBox.Show("Only Integer value is allowed in Distributor Id.", Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                SearchDistributorBonusData();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"),
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SearchDistributorBonusData()
        {
            try
            {
                #region Validation Code

                StringBuilder sbError = new StringBuilder();
                sbError = ValidateDistributorBonusRptData();

                if (!sbError.ToString().Trim().Equals(string.Empty))
                {
                    MessageBox.Show(sbError.ToString(), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                #endregion Validation Code

                DistributorsBonusReport m_objDistributorsBonusReport = new DistributorsBonusReport();
                bool isValidSearch = false;

                if (!string.IsNullOrEmpty(txtDistributorId.Text.Trim()))
                {
                    m_objDistributorsBonusReport.DistributorId = Convert.ToInt32(txtDistributorId.Text.Trim());
                    isValidSearch = true;
                }
                else
                {
                    m_objDistributorsBonusReport.DistributorId = Common.INT_DBNULL;
                }

                m_objDistributorsBonusReport.CountryId = Convert.ToInt32(cmbCountry.SelectedValue);
                if (m_objDistributorsBonusReport.CountryId > 0)
                {
                    isValidSearch = true;
                }

                m_objDistributorsBonusReport.StateId = Convert.ToInt32(cmbState.SelectedValue);
                if (m_objDistributorsBonusReport.StateId > 0)
                {
                    isValidSearch = true;
                }

                m_objDistributorsBonusReport.CityId = Convert.ToInt32(cmbCity.SelectedValue);
                if (m_objDistributorsBonusReport.CityId > 0)
                {
                    isValidSearch = true;
                }

                m_objDistributorsBonusReport.LevelId = Convert.ToInt32(cmbDistributorLevel.SelectedValue);
                if (m_objDistributorsBonusReport.LevelId > 0)
                {
                    isValidSearch = true;
                }


                if (!string.IsNullOrEmpty(txtBonusPercent.Text.Trim()))
                {
                    m_objDistributorsBonusReport.BonusPercent = Convert.ToInt32(txtBonusPercent.Text.Trim());
                    if (m_objDistributorsBonusReport.BonusPercent > 0)
                    {
                        isValidSearch = true;
                    }
                }

                else
                {
                    m_objDistributorsBonusReport.BonusPercent = Common.INT_DBNULL;

                }


                m_objDistributorsBonusReport.BonusTypes = GetBonusTypes();

                if (isValidSearch == false)
                {
                    MessageBox.Show("Select one more search creiteria besides businessmonth");
                    return;
                }

                DateTime dtBusinessMonth = dtpBusinessMonth.Checked == true ? Convert.ToDateTime(dtpBusinessMonth.Value) : Common.DATETIME_NULL;
                m_objDistributorsBonusReport.BusinessMonth = Convert.ToDateTime(dtBusinessMonth).ToString(Common.DATE_TIME_FORMAT);




                string errMsg = string.Empty;
                //  m_listOfDistBonus = m_objDistributorsBonusReport.SearchDistributorBonus(Common.ToXml(m_objDistributorsBonusReport), DistributorsBonusReport.DIST_BONUS_SEARCH, ref errMsg);



                m_listOfDistBonus = m_objDistributorsBonusReport.SearchDistributorBonus(Common.ToXml(m_objDistributorsBonusReport), DistributorsBonusReport.DIST_BONUS_SEARCH, ref errMsg);

                //Bind Grid
                BindGridView_SearchDistributorBonus();
            }
            catch { throw; }
        }

        private StringBuilder ValidateDistributorBonusRptData()
        {
            //errorProviderDistBonusRept.SetError(txtDistributorId, string.Empty);
            // errorProviderDistBonusRept.SetError(txtLevel, string.Empty);



            errorProviderDistBonusRept.Clear();

            if (string.IsNullOrEmpty(txtDistributorId.Text) == false && !Validators.IsInt32(txtDistributorId.Text))
            {
                errorProviderDistBonusRept.SetError(txtDistributorId, "Only Interger value is allowed");
            }
            if (string.IsNullOrEmpty(txtBonusPercent.Text) == false && !Validators.IsDecimal(txtBonusPercent.Text))
            {
                errorProviderDistBonusRept.SetError(txtBonusPercent, "Only 2 digit decimal value is allowed");
            }


            StringBuilder sbError = new StringBuilder();
            CheckBlankMessage(Validators.GetErrorMessage(errorProviderDistBonusRept, txtDistributorId), sbError);
            sbError.AppendLine();

            return Common.ReturnErrorMessage(sbError);
        }
        private void CheckBlankMessage(string strMessage, StringBuilder sb)
        {
            if (strMessage != "")
            {
                sb.Append(strMessage);
                sb.AppendLine();
            }
        }

        private string GetBonusTypes()
        {
            StringBuilder bonusIds = new StringBuilder();
            if (chkPB.Checked) { bonusIds.Append(((int)BonusType.PB).ToString()); }
            if (chkDB.Checked) { if (chkPB.Checked) { bonusIds.Append(','); } bonusIds.Append(((int)BonusType.DB).ToString()); }
            if (chkCF.Checked) { if (chkDB.Checked) { bonusIds.Append(','); } else if (chkPB.Checked) { bonusIds.Append(','); } bonusIds.Append(((int)BonusType.CF).ToString()); }
            if (chkHF.Checked) { if (chkCF.Checked) { bonusIds.Append(','); } else if (chkDB.Checked) { bonusIds.Append(','); } else if (chkPB.Checked) { bonusIds.Append(','); } bonusIds.Append(((int)BonusType.HF).ToString()); }
            if (chkTF.Checked) { if (chkHF.Checked) { bonusIds.Append(','); } else if (chkCF.Checked) { bonusIds.Append(','); } else if (chkDB.Checked) { bonusIds.Append(','); } else if (chkPB.Checked) { bonusIds.Append(','); } bonusIds.Append(((int)BonusType.TF).ToString()); }
            if (chkLOB.Checked) { if (chkTF.Checked) { bonusIds.Append(','); } else if (chkHF.Checked) { bonusIds.Append(','); } else if (chkCF.Checked) { bonusIds.Append(','); } else if (chkDB.Checked) { bonusIds.Append(','); } else if (chkPB.Checked) { bonusIds.Append(','); } bonusIds.Append(((int)BonusType.LOB).ToString()); }

            return bonusIds.ToString();
        }

        private void BindGridView_SearchDistributorBonus()
        {
            try
            {
                dgvDistriBonusSearch.DataSource = null;
                if (m_listOfDistBonus.Count > 0)
                {
                    dgvDistriBonusSearch.DataSource = m_listOfDistBonus;
                    dgvDistriBonusSearch.Rows[0].Selected = false;
                    btnPrint.Enabled = true;
                    chkPrintAll.Checked = true;
                    CheckAllItemsInlistOfDistBonus(m_listOfDistBonus);
                }
                else
                {
                    MessageBox.Show(Common.GetMessage("8002"), Common.GetMessage("10001"),
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CheckAllItemsInlistOfDistBonus(List<DistributorsBonusInfo> m_listOfDistBonus)
        {
            try
            {
                dgvDistriBonusSearch.DataSource = null;


                if (m_listOfDistBonus != null && m_listOfDistBonus.Count > 0)
                {
                    foreach (DistributorsBonusInfo item in m_listOfDistBonus)
                    {
                        if (chkPrintAll.Checked)
                            item.Print = true;
                        else
                            item.Print = false;
                    }
                    dgvDistriBonusSearch.DataSource = m_listOfDistBonus;
                    dgvDistriBonusSearch.Rows[0].Selected = false;
                }
                else
                {
                    MessageBox.Show(Common.GetMessage("8002"), Common.GetMessage("10001"),
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                PrintSelectedDistBonusReport();
                fncCheckOnOff();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);

            }
        }

        private void PrintSelectedDistBonusReport()
        {
            try
            {
                List<int> listToPrint = new List<int>();
                this.dgvDistriBonusSearch.EndEdit();
                foreach (DataGridViewRow row in dgvDistriBonusSearch.Rows)
                {
                    if (Convert.ToBoolean(row.Cells["Print"].Value) == true)
                    {
                        listToPrint.Add(m_listOfDistBonus[row.Index].DistributorId);
                    }
                }
                if (listToPrint.Count > 0)
                {
                    foreach (int distNo in listToPrint)
                    {
                        DateTime businessMonth = dtpBusinessMonth.Checked == true ? Convert.ToDateTime(dtpBusinessMonth.Value) : Common.DATETIME_NULL;
                        // string businessMonth = Convert.ToDateTime(dtBusinessMonth).ToString(Common.DATE_TIME_FORMAT);

                        DataSet dsReport = CreatePrintDataSet((int)Common.PrintType.PrintInvoice, distNo, businessMonth);
                        //Bikram
                        fncModifyDataSet(ref dsReport);
                        CoreComponent.UI.ReportScreen reportScreenObj = new CoreComponent.UI.ReportScreen((int)Common.ReportType.BonusStatementDirectors, dsReport);
                        reportScreenObj.PrintReport();                     
                        dsReport = null;
                    }
                    foreach (DataGridViewRow row in dgvDistriBonusSearch.Rows)
                    {
                        row.Cells["Print"].Value = false;
                    }
                    MessageBox.Show(Common.GetMessage("INF0242", listToPrint.Count.ToString()), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(Common.GetMessage("INF0241"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                fncCheckOnOff();

            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
        }

        private void fncCheckOnOff()
        {
            bool chkCheckALLOff = true;
            foreach (DataGridViewRow row in dgvDistriBonusSearch.Rows)
            {
                if (row.Cells["Print"].Value.ToString() == "True")
                {
                    chkCheckALLOff = false;
                    break;
                }                
            }
            if (chkCheckALLOff)
            {
                chkPrintAll.Checked = false;
            }          
        }

        private void fncModifyDataSet(ref DataSet ds)
        {
            ds.Tables[0].Columns.Add(new DataColumn("FromDateText", Type.GetType("System.String")));
            ds.Tables[0].Columns.Add(new DataColumn("ToDateText", Type.GetType("System.String")));
            ds.Tables[0].Columns.Add(new DataColumn("HeaderAddress", Type.GetType("System.String")));
            ds.Tables[0].Columns.Add(new DataColumn("AddressText", Type.GetType("System.String")));
            //ds.Tables[0].Rows[0]["HeaderAddress"] = headerText;
            //ds.Tables[0].Rows[0]["AddressText"] = addressText;
            ds.Tables[0].Columns.Add(new DataColumn("PayDateText", Type.GetType("System.String")));
            ds.Tables[3].Columns.Add(new DataColumn("BonusChequeinWords", Type.GetType("System.String")));
            ds.Tables[4].Columns.Add(new DataColumn("AmountinWords", Type.GetType("System.String")));
            //TABLE[1] ---Performance Bonus
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                ds.Tables[1].Rows[i]["SelfPv"] = Math.Round(Convert.ToDecimal(ds.Tables[1].Rows[i]["SelfPV"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                ds.Tables[1].Rows[i]["SelfBv"] = Math.Round(Convert.ToDecimal(ds.Tables[1].Rows[i]["SelfBV"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                ds.Tables[1].Rows[i]["TotalPV"] = Math.Round(Convert.ToDecimal(ds.Tables[1].Rows[i]["TotalPV"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                ds.Tables[1].Rows[i]["TotalBV"] = Math.Round(Convert.ToDecimal(ds.Tables[1].Rows[i]["TotalBV"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                ds.Tables[1].Rows[i]["CUMPV"] = Math.Round(Convert.ToDecimal(ds.Tables[1].Rows[i]["CUMPV"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                ds.Tables[1].Rows[i]["CUMBV"] = Math.Round(Convert.ToDecimal(ds.Tables[1].Rows[i]["CUMBV"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                ds.Tables[1].Rows[i]["GroupPV"] = Math.Round(Convert.ToDecimal(ds.Tables[1].Rows[i]["GroupPV"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                ds.Tables[1].Rows[i]["GroupBV"] = Math.Round(Convert.ToDecimal(ds.Tables[1].Rows[i]["GroupBV"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
            }
            //TABLE[2] --BonusStatementDirectors_DownlineInfo
            for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
            {

                ds.Tables[2].Rows[i]["PV"] = Math.Round(Convert.ToDecimal(ds.Tables[2].Rows[i]["PV"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                ds.Tables[2].Rows[i]["BV"] = Math.Round(Convert.ToDecimal(ds.Tables[2].Rows[i]["BV"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                ds.Tables[2].Rows[i]["BonusPercent"] = Math.Round(Convert.ToDecimal(ds.Tables[2].Rows[i]["BonusPercent"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
            }

            //Table[3] -- Check qualification for Bonuses (BonusStatementDirectors_DistributorGroupMonthly)
            //Table[4] --(BonusStatementDirectors_DistributorGroupMonthly1)

            //--Table[6] --(BonusStatementDirectors_DistributorPayOrderInfo)
            //--Table[7] --BonusStatementDirectors_DistributorGiftVoucherInfo
            //--Table[8] --BonusStatementDirectors_DistributorPBAmountInfo (Not used in report)
            //for (int i = 0; i < ds.Tables[8].Rows.Count; i++)
            //{

            //    ds.Tables[8].Rows[i]["Amount"] = Math.Round(Convert.ToDecimal(ds.Tables[8].Rows[i]["Amount"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);

            //}
            //--Table[9] --BonusStatementDirectors_DistributorCarInfo
            for (int i = 0; i < ds.Tables[9].Rows.Count; i++)
            {

                ds.Tables[9].Rows[i]["CumulativeCarFund"] = Math.Round(Convert.ToDecimal(ds.Tables[9].Rows[i]["CumulativeCarFund"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                ds.Tables[9].Rows[i]["PaidAmount"] = Math.Round(Convert.ToDecimal(ds.Tables[9].Rows[i]["PaidAmount"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                ds.Tables[9].Rows[i]["TotalPayable"] = Math.Round(Convert.ToDecimal(ds.Tables[9].Rows[i]["TotalPayable"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);

            }
            //--Table[10] --BonusStatementDirectors_DistributorCarInfo
            for (int i = 0; i < ds.Tables[10].Rows.Count; i++)
            {

                ds.Tables[10].Rows[i]["CurrentCarBonus"] = Math.Round(Convert.ToDecimal(ds.Tables[10].Rows[i]["CurrentCarBonus"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);

            }
            //--Table[11] --BonusStatementDirectors_DistributorViaChequeInfo
            for (int i = 0; i < ds.Tables[11].Rows.Count; i++)
            {
                ds.Tables[11].Rows[i]["ViaCheque"] = ds.Tables[11].Rows[i][0];
            }
            //--Table[12] --BonusStatementDirectors_DistPerformanceBonus
            for (int i = 0; i < ds.Tables[12].Rows.Count; i++)
            {
                ds.Tables[12].Rows[0]["PerformanceBonus"] = ds.Tables[12].Rows[0][0];
            }
            //--Table[13] --BonusStatementDirectors_BonusChkVoucher
            for (int i = 0; i < ds.Tables[13].Rows.Count; i++)
            {
                ds.Tables[13].Rows[0]["BonusChkVoucher"] = ds.Tables[13].Rows[0][0];

            }
            //--Table[14] --BonusStatementDirectors_ProductVoucher
            for (int i = 0; i < ds.Tables[14].Rows.Count; i++)
            {
                ds.Tables[14].Rows[0]["ProductVoucher"] = ds.Tables[14].Rows[0][0];
            }
            //--Table[15] --BonusStatementDirectors_QualPvNonPV
            for (int i = 0; i < ds.Tables[15].Rows.Count; i++)
            {
                ds.Tables[15].Rows[i]["TaxDeducted"] = Math.Round(Convert.ToDecimal(ds.Tables[15].Rows[i]["TaxDeducted"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                ds.Tables[15].Rows[i]["TotalPV"] = Math.Round(Convert.ToDecimal(ds.Tables[15].Rows[i]["TotalPV"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                ds.Tables[15].Rows[i]["ExclPV"] = Math.Round(Convert.ToDecimal(ds.Tables[15].Rows[i]["ExclPV"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                ds.Tables[15].Rows[i]["NonQualifiedPV"] = Math.Round(Convert.ToDecimal(ds.Tables[15].Rows[i]["NonQualifiedPV"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                ds.Tables[15].Rows[i]["QualifiedDirect"] = Math.Round(Convert.ToDecimal(ds.Tables[15].Rows[i]["QualifiedDirect"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
            }

            //--Table[16] --BonusStatementDirectors_GroupArchiveSummary
            for (int i = 0; i < ds.Tables[16].Rows.Count; i++)
            {
                ds.Tables[16].Rows[0]["GroupSummary"] = ds.Tables[16].Rows[0][0];
            }
            //--Table[17] --BonusStatementDirectors_DistributorTFInfo 
            for (int i = 0; i < ds.Tables[17].Rows.Count; i++)
            {

                ds.Tables[17].Rows[i]["CumulativeTravelFund"] = Math.Round(Convert.ToDecimal(ds.Tables[17].Rows[i]["CumulativeTravelFund"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                ds.Tables[17].Rows[i]["PaidTravelFund"] = Math.Round(Convert.ToDecimal(ds.Tables[17].Rows[i]["PaidTravelFund"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                ds.Tables[17].Rows[i]["PayableTravelFund"] = Math.Round(Convert.ToDecimal(ds.Tables[17].Rows[i]["PayableTravelFund"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                ds.Tables[17].Rows[i]["CurrentTravelFund"] = Math.Round(Convert.ToDecimal(ds.Tables[17].Rows[i]["CurrentTravelFund"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);

            }
            //--Table[18] --BonusStatementDirectors_Declaration
            //for (int i = 0; i < ds.Tables[18].Rows.Count; i++)
            //{
            //    ds.Tables[18].Rows[0]["Declaration"] = ds.Tables[18].Rows[0][0];
            //}
            for (int i = 0; i < ds.Tables[19].Rows.Count; i++)
            {
                ds.Tables[19].Rows[0]["TotalBvAmount"] = ds.Tables[19].Rows[0][0];
            }

            // Max allowed 20 Voucher

            for (int i = 20; i < 40; i++)
            {
                try
                {
                    if (ds.Tables[i] != null)
                    {
                        continue;
                    }
                }
                catch (IndexOutOfRangeException ex)
                {
                    DataTable dt = new DataTable();
                    dt.Clear();
                    dt.Columns.Add(new DataColumn("VoucherSrNo", typeof(string)));
                    dt.Columns.Add(new DataColumn("IssuedTo", typeof(string)));
                    dt.Columns.Add(new DataColumn("Name", typeof(string)));
                    dt.Columns.Add(new DataColumn("Expirydate", typeof(string)));
                    dt.Columns.Add(new DataColumn("Issuedate", typeof(string)));
                    dt.Columns.Add(new DataColumn("Quantity", typeof(string)));
                    dt.Columns.Add(new DataColumn("ItemId", typeof(string)));
                    dt.Columns.Add(new DataColumn("ItemName", typeof(string)));

                    ds.Tables.Add(dt);
                }


            }
        }

        private DataSet CreatePrintDataSet(int type, int distributorId, DateTime BusinessMonth)
        {
            string errorMessage = string.Empty;
            DataSet ds = DistributorsBonusReport.GetDistributorBonusReportForPrint(type, distributorId, BusinessMonth, ref errorMessage);
            if (errorMessage.Trim().Length == 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    //ds.Tables[0].Columns.Add(new DataColumn("Header", Type.GetType("System.String")));
                    //ds.Tables[0].Columns.Add(new DataColumn("DateText", Type.GetType("System.String")));
                    //ds.Tables[0].Columns.Add(new DataColumn("TINNo", Type.GetType("System.String")));
                    //ds.Tables[0].Columns.Add(new DataColumn("OrderAmountWords", Type.GetType("System.String")));
                    //ds.Tables[1].Columns.Add(new DataColumn("PriceInclTax", Type.GetType("System.String")));
                    //ds.Tables[0].Columns.Add(new DataColumn("PANNo", Type.GetType("System.String")));
                    //ds.Tables[1].Columns.Add(new DataColumn("IsLocation", Type.GetType("System.String")));
                    //ds.Tables[0].Rows[0]["Header"] = (type == 1 ? "Customer Order" : "Retail Invoice");
                    //ds.Tables[0].Rows[0]["TINNo"] = Common.TINNO;
                    //ds.Tables[0].Rows[0]["PANNo"] = Common.PANNO;

                }

            }
            return ds;
        }



        private void chkPrintAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                dgvDistriBonusSearch.DataSource = null;


                if (m_listOfDistBonus != null && m_listOfDistBonus.Count > 0)
                {
                    foreach (DistributorsBonusInfo item in m_listOfDistBonus)
                    {
                        if (chkPrintAll.Checked)
                            item.Print = true;
                        else
                            item.Print = false;
                    }
                    dgvDistriBonusSearch.DataSource = m_listOfDistBonus;
                    dgvDistriBonusSearch.Rows[0].Selected = false;
                }
                else
                {
                    MessageBox.Show(Common.GetMessage("8002"), Common.GetMessage("10001"),
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void cmbBonusPercent_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtDistributorId.Text = string.Empty;

            chkPB.Checked = true;
            chkTF.Checked = true;
            chkDB.Checked = true;
            chkCF.Checked = true;
            chkHF.Checked = true;
            chkLOB.Checked = true;
            txtBonusPercent.Text = string.Empty;
            PopulateCountryInCountryCombo();
            PopulateIndtpBusinessMonth();
            PopulateDistLevelInDistLevelCombo();
            m_listOfDistBonus = null;
            dgvDistriBonusSearch.DataSource = null;
            
            if (dgvDistriBonusSearch.DataSource == null)
            {
                btnPrint.Enabled = false;
            }
            // cmbCountry
            // cmbState

        }

        private void txtDistributorId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar > 31 && (e.KeyChar < '0' || e.KeyChar > '9'))
            {
                e.Handled = true;
            }
        }

        private void btnExportReport_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult objResult = MessageBox.Show(Common.GetMessage("INF0248"), Common.GetMessage("10004"), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (objResult == DialogResult.Yes)
                {
                    fncExportintoPDFintoSingleFile();
                }
                if (objResult == DialogResult.No)
                {
                    fncExportintoPDF();
                }

                fncCheckOnOff();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);            
            }   
        }

        private void fncExportintoPDF()
        {
            string strPath = "";
            FolderBrowserDialog objFBD = new FolderBrowserDialog();
            DialogResult result = objFBD.ShowDialog();
            if (result == DialogResult.OK)
            {
                strPath = objFBD.SelectedPath;
            }
            else
            {
                return;
            }
            if (strPath.Trim().Length > 4)
            {
                strPath = strPath + "\\";
            }
            fncExport(strPath);
        }

        private void fncExportintoPDFintoSingleFile()
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Title = "Save file as...";
            dialog.Filter = "Text files (*.Pdf)|*.Pdf";
            dialog.RestoreDirectory = true;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                IsSingleFile = true;
                fncExport(dialog.FileName);
                IsSingleFile = false;
            }
        }

        private void fncExport(string strExportPath)
        {
            try
            {
                string strPath = strExportPath.Substring(0, strExportPath.LastIndexOf("\\") + 1);
                List<int> listToPrint = new List<int>();
                this.dgvDistriBonusSearch.EndEdit();
                foreach (DataGridViewRow row in dgvDistriBonusSearch.Rows)
                {
                    if (Convert.ToBoolean(row.Cells["Print"].Value) == true)
                    {
                        listToPrint.Add(m_listOfDistBonus[row.Index].DistributorId);
                    }
                }
                if (listToPrint.Count > 0)
                {                   

                    foreach (int distNo in listToPrint)
                    {
                        string strFilePath = "";
                        DateTime businessMonth = dtpBusinessMonth.Checked == true ? Convert.ToDateTime(dtpBusinessMonth.Value) : Common.DATETIME_NULL;
                        DataSet dsReport = CreatePrintDataSet((int)Common.PrintType.PrintInvoice, distNo, businessMonth);
                        fncModifyDataSet(ref dsReport);
                        CoreComponent.UI.ReportScreen reportScreenObj = new CoreComponent.UI.ReportScreen((int)Common.ReportType.BonusStatementDirectors, dsReport);
                        strFilePath = strPath + distNo.ToString() + ".PDF";
                        reportScreenObj.fncSaveReport(strFilePath);
                        dsReport = null;
                    }
                    foreach (DataGridViewRow row in dgvDistriBonusSearch.Rows)
                    {
                        row.Cells["Print"].Value = false;
                    }
                    if (IsSingleFile)
                    {
                        string strfileName = "";
                        strfileName = strExportPath.Substring(strExportPath.LastIndexOf("\\") + 1);                        
                        fncMergePDF(listToPrint, strPath,strfileName);
                    }

                    MessageBox.Show(Common.GetMessage("INF0246", listToPrint.Count.ToString()), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(Common.GetMessage("INF0247"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
        }

        private void fncMergePDF(List<int> listToPrint, string strFilePath, string strFileName)
        {
            PdfDocument outPdf = new PdfDocument();
            foreach (int distNo in listToPrint)
            {
                using (PdfDocument one = PdfReader.Open(strFilePath + distNo.ToString() + ".PDF", PdfDocumentOpenMode.Import))
                CopyPages(one, outPdf);
                
            }
            outPdf.Save(strFilePath + strFileName);           
        }
        void CopyPages(PdfDocument from, PdfDocument to)
        {
            for (int i = 0; i < from.PageCount; i++)
            {
                to.AddPage(from.Pages[i]);
            }
        }

        private void dgvDistriBonusSearch_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.RowIndex >= 0) && (dgvDistriBonusSearch.Rows[e.RowIndex].Cells[e.ColumnIndex].GetType() == typeof(DataGridViewCheckBoxCell)))
            {
                if (dgvDistriBonusSearch.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "True")
                { 
                    dgvDistriBonusSearch.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = false;
                }
                else
                {
                    dgvDistriBonusSearch.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = true;
                
                }
                fncCheckOnOff();
            }
        }

    }
}
