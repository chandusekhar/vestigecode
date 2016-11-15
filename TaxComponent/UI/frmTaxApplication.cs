using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TaxComponent.BusinessObjects;
using CoreComponent.Core.BusinessObjects;
using AuthenticationComponent.BusinessObjects; 

namespace TaxComponent.UI
{
    public partial class frmTaxApplication : CoreComponent.Core.UI.HierarchyTemplate
    {
        #region Variables
        Int32 m_taxId = Common.INT_DBNULL;
        DateTime m_taxStartDate;
        string m_ModifiedDate = string.Empty;
        List<TaxApplication> m_listTaxApplication = new List<TaxApplication>();
        private Boolean m_isSaveAvailable = false;
        private Boolean m_isSearchAvailable = false;
        private Boolean m_DGVSelection = false;
        private int intUserId = Authenticate.LoggedInUser.UserId;
        private string strUserName = Authenticate.LoggedInUser.UserName;
        private string strLocationCode = Common.LocationCode;

        #endregion

        #region C'tor
        public frmTaxApplication()
        {
            try
            {
                m_isSaveAvailable = Authenticate.IsFunctionAccessible(strUserName, strLocationCode, TaxApplication.MODULE_CODE, Common.FUNCTIONCODE_SAVE);
                m_isSearchAvailable = Authenticate.IsFunctionAccessible(strUserName, strLocationCode, TaxApplication.MODULE_CODE, Common.FUNCTIONCODE_SEARCH);

                InitializeComponent();

                InitializeControls();
                ResetControl();
                lblPageTitle.Text = "Tax Application";
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// To Initialize Controls
        /// </summary>
        private void InitializeControls()
        {
            FillTaxCategory();
            FillTaxType();
            FillTaxGroup();
            FillTaxJurisdiction();
            FillGoodsDirection();
            FillStatus();

            dgvTax.AutoGenerateColumns = false;
            dgvTax.DataSource = null;
            DataGridView dgvSearchNew = Common.GetDataGridViewColumns(dgvTax, Environment.CurrentDirectory + "\\App_Data\\TaxGrid.xml");

        }
        /// <summary>
        /// To Fill Tax Category Drop Down List
        /// </summary>
        private void FillTaxCategory()
        {
            DataTable dtTax = Tax.TaxLookup(Tax.TaxEnum.TaxCategory);

            cmbTaxCategory.DataSource = dtTax;
            cmbTaxCategory.DisplayMember = "Name";
            cmbTaxCategory.ValueMember = "TaxId";
        }
        /// <summary>
        /// To Fill Tax Type Drop Down List
        /// </summary>
        private void FillTaxType()
        {
            DataTable dtTax = Tax.TaxLookup(Tax.TaxEnum.TaxType);
            cmbTaxType.DataSource = dtTax;
            cmbTaxType.DisplayMember = "Name";
            cmbTaxType.ValueMember = "TaxId";
        }

        /// <summary>
        /// To Fill Tax Group Drop Down List
        /// </summary>
        private void FillTaxGroup()
        {
            DataTable dtTax = Tax.TaxLookup(Tax.TaxEnum.TaxGroup);
            cmbTaxGroup.DataSource = dtTax;
            cmbTaxGroup.DisplayMember = "Code";
            cmbTaxGroup.ValueMember = "TaxId";
        }

        /// <summary>
        /// To Fill Tax Jurisdiction Drop Down List
        /// </summary>
        private void FillTaxJurisdiction()
        {
            DataTable dtStates = Common.ParameterLookup(Common.ParameterType.TaxJurisdiction, new ParameterFilter("", -1, -1, -1)); //Tax.TaxLookup(Tax.TaxEnum.TaxJurisdiction);

            chkStateList.DataSource = dtStates;
            chkStateList.DisplayMember = "StateName";
            chkStateList.ValueMember = "StateId";
        }

        /// <summary>
        /// To Fill Goods Direction Drop Down List
        /// </summary>
        private void FillGoodsDirection()
        {
            DataTable dt = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter("GOODDIRECTION", 0, 0, 0));
            cmbGoodsDirection.DataSource = dt;
            cmbGoodsDirection.DisplayMember = Common.KEYVALUE1;
            cmbGoodsDirection.ValueMember = Common.KEYCODE1;
        }

        /// <summary>
        /// To Select an item,
        /// </summary>
        private void ListTaxJurisdictionIndexChange()
        {
            if (m_taxId != Common.INT_DBNULL & chkStateList.SelectedIndex >= 0)
            {
                int i = chkStateList.SelectedIndex;

                for (int k = 1; k < chkStateList.Items.Count; k++)
                {
                    chkStateList.SetItemChecked(k, false);
                }

                chkStateList.SetItemChecked(0, false);

                if (i == 0)
                    chkStateList.SetItemChecked(0, false);
                else
                    chkStateList.SetItemChecked(i, true);
            }
        }

        private List<int> GetCheckedJurisdictionIds()
        {
            int stateId = Common.INT_DBNULL;
            List<int> stateList = new List<int>();
            for (int i = 0; i < chkStateList.CheckedIndices.Count; i++)
            {
                stateId = Convert.ToInt32(((System.Data.DataRowView)(chkStateList.Items[chkStateList.CheckedIndices[i]])).Row.ItemArray[0]);

                if (stateId != Common.INT_DBNULL)
                    stateList.Add(stateId);
            }
            return stateList;
        }

        /// <summary>
        /// Fill Status Drop Down List
        /// </summary>
        private void FillStatus()
        {
            DataTable dt = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter("STATUS", 0, 0, 0));
            cmbStatus.DataSource = dt;
            cmbStatus.DisplayMember = Common.KEYVALUE1;
            cmbStatus.ValueMember = Common.KEYCODE1;
            cmbStatus.SelectedValue = 1;
        }

        /// <summary>
        /// To Validate all controls on Save Click
        /// </summary>
        private void ValidateControls()
        {
            ValidateCombo(cmbTaxCategory, lblTaxCategory,true);
            ValidateCombo(cmbTaxType, lblTaxType,true);
            ValidateCombo(cmbTaxGroup, lblTaxGroup, true);
            ValidateListBox(chkStateList, lblState);
            ValidateCombo(cmbGoodsDirection, lblGoodsDirection, true);
            ValidateStartDate();
            ValidateCombo(cmbStatus, lblStatus, true);
        }

        /// <summary>
        /// Generate string for Error
        /// </summary>
        /// <returns></returns>
        private StringBuilder GenerateError()
        {
            StringBuilder sbError = new StringBuilder();
            if (errTax.GetError(cmbTaxCategory).Trim().Length > 0)
            {
                sbError.Append(errTax.GetError(cmbTaxCategory));
                sbError.AppendLine();
            }
            if (errTax.GetError(cmbTaxType).Trim().Length > 0)
            {
                sbError.Append(errTax.GetError(cmbTaxType));
                sbError.AppendLine();
            }
            if (errTax.GetError(cmbTaxGroup).Trim().Length > 0)
            {
                sbError.Append(errTax.GetError(cmbTaxGroup));
                sbError.AppendLine();
            }
            if (errTax.GetError(chkStateList).Trim().Length > 0)
            {
                sbError.Append(errTax.GetError(chkStateList));
                sbError.AppendLine();
            }
            if (errTax.GetError(cmbGoodsDirection).Trim().Length > 0)
            {
                sbError.Append(errTax.GetError(cmbGoodsDirection));
                sbError.AppendLine();
            }
            if (errTax.GetError(dtpStartDate).Trim().Length > 0)
            {
                sbError.Append(errTax.GetError(dtpStartDate));
                sbError.AppendLine();
            }
            if (errTax.GetError(cmbStatus).Trim().Length > 0)
            {
                sbError.Append(errTax.GetError(cmbStatus));
                sbError.AppendLine();
            }
            sbError = Common.ReturnErrorMessage(sbError);
            return sbError;
        }

        /// <summary>
        /// To Save Tax Application Record
        /// </summary>
        private void Save()
        {
            #region ValidateLocationControls
            ValidateControls();
            #endregion

            #region Check Location Errors

            StringBuilder sbError = new StringBuilder();
            sbError = GenerateError();
            #endregion

            if (!sbError.ToString().Trim().Equals(string.Empty))
            {
                MessageBox.Show(sbError.ToString(), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult saveResult = MessageBox.Show(Common.GetMessage("INF0027"), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (saveResult == DialogResult.Yes)
            {
                if ((m_taxId == Common.INT_DBNULL) && (Convert.ToInt32(cmbStatus.SelectedValue) == 2))
                {
                    MessageBox.Show(Common.GetMessage("VAL0020"), Common.GetMessage("10001"), MessageBoxButtons.OK,MessageBoxIcon.Information);
                    return;
                }

                TaxApplication objtaxApplication = new TaxApplication();
                objtaxApplication.TaxCategoryId = Convert.ToInt32(cmbTaxCategory.SelectedValue);
                objtaxApplication.TaxTypeId = Convert.ToInt32(cmbTaxType.SelectedValue);
                objtaxApplication.TaxGroupId = Convert.ToInt32(cmbTaxGroup.SelectedValue);

                List<int> taxJurisdiction = new List<int>();
                taxJurisdiction = GetCheckedJurisdictionIds();

                if (taxJurisdiction.Count == 0)
                {
                    MessageBox.Show(Common.GetMessage("VAL0019", lblState.Text.Substring(0, lblState.Text.Length - 2)), Common.GetMessage("10001"), MessageBoxButtons.OK,MessageBoxIcon.Information);
                    return;
                }
                objtaxApplication.States = taxJurisdiction;
                objtaxApplication.GoodsDirection = Convert.ToInt32(cmbGoodsDirection.SelectedValue);
                objtaxApplication.Status = Convert.ToInt32(cmbStatus.SelectedValue);
                objtaxApplication.ModifiedBy = intUserId;
                objtaxApplication.TaxId = m_taxId;
                objtaxApplication.ModifiedDate = m_ModifiedDate;
                objtaxApplication.TaxAuthority = txtAuthority.Text.Trim();
                objtaxApplication.FormCTax = chkFormCTax.Checked == true ? 1 : 0;

                DateTime startDate = dtpStartDate.Checked == true ? Convert.ToDateTime(dtpStartDate.Value) : Common.DATETIME_NULL;
                objtaxApplication.StartDate = Convert.ToDateTime(startDate).ToString(Common.DATE_TIME_FORMAT);

                string errorMesage = string.Empty;
                bool recordSaved = objtaxApplication.Save(ref errorMesage);

                if (errorMesage.Equals(string.Empty))
                {
                    BindGridOnSave();
                    ResetControlOnSave();
                    MessageBox.Show(Common.GetMessage("8001"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else if (errorMesage.IndexOf("INF0032") >= 0)
                {
                    BindGridOnSave();
                    MessageBox.Show(Common.GetMessage("INF0032", "Jurisdiction(s)- " + errorMesage.Substring("INF0032".Length + 1)), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (errorMesage.IndexOf("INF0033") >= 0)
                {
                    BindGridOnSave();
                    MessageBox.Show(Common.GetMessage("INF0033", "Jurisdiction(s)- " + errorMesage.Substring("INF0033".Length + 1)), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show(Common.GetMessage(errorMesage), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        void EnableButton()
        {
            btnSave.Enabled = m_isSaveAvailable;
            btnSearch.Enabled = m_isSearchAvailable;
        }

        void BindGridOnSave()
        {
            dgvTax.DataSource = new List<TaxApplication>();
            EnableButton();
            List<int> lstTaxJurisdiction = new List<int>();
            lstTaxJurisdiction.Add(Common.INT_DBNULL);
            m_listTaxApplication = Search(Common.INT_DBNULL, Common.INT_DBNULL, Common.INT_DBNULL, Common.INT_DBNULL, lstTaxJurisdiction, Common.INT_DBNULL, Convert.ToInt32(cmbStatus.SelectedValue));
            dgvTax.DataSource = m_listTaxApplication;
        }
        /// <summary>
        /// This function is used to show data in controls
        /// </summary>
        /// <param name="e"></param>
        private void SelectGridRow(DataGridViewCellMouseEventArgs e)
        {            
            if (e.RowIndex >= 0)
            {
                EditTax(e.RowIndex);
                if ((dgvTax.Rows[e.RowIndex].Cells[e.ColumnIndex].GetType() == typeof(DataGridViewImageCell)) && e.RowIndex > -1)
                {
                    btnSearch.Enabled = false;
                    btnSave.Enabled = m_isSaveAvailable;
                }
                else
                {
                    btnSearch.Enabled = m_isSearchAvailable;
                    btnSave.Enabled = false;
                }
            }
        }

        private void EditTax(int index)
        {
            {  
                m_taxId = Convert.ToInt32(dgvTax.Rows[index].Cells["TaxId"].Value.ToString().Trim());
               
                ////Get ParentId
                if (m_listTaxApplication == null)
                    return;

                var taxSelect = (from p in m_listTaxApplication where p.TaxId == m_taxId select p);

                if (taxSelect.ToList().Count == 0)
                    return;

                if (taxSelect.ToList()[0].StartDate.Length > 0)
                {
                    dtpStartDate.Checked = true;
                    dtpStartDate.Value = Convert.ToDateTime(taxSelect.ToList()[0].StartDate);
                    m_taxStartDate = dtpStartDate.Value;
                }
                else
                { dtpStartDate.Checked = false; }
                cmbTaxCategory.SelectedValue = Convert.ToInt32(taxSelect.ToList()[0].TaxCategoryId);
                cmbTaxType.SelectedValue = Convert.ToInt32(taxSelect.ToList()[0].TaxTypeId);
                cmbTaxGroup.SelectedValue = Convert.ToInt32(taxSelect.ToList()[0].TaxGroupId);
                cmbGoodsDirection.SelectedValue = Convert.ToInt32(taxSelect.ToList()[0].GoodsDirection);
                txtAuthority.Text = taxSelect.ToList()[0].TaxAuthority.ToString();
                chkFormCTax.Checked = taxSelect.ToList()[0].FormCTax == 0 ? false : true;

                for (int i = 0; i < chkStateList.Items.Count; i++)
                {
                    chkStateList.SetItemChecked(i, false);
                }

                chkStateList.SelectedValue = Convert.ToInt32(taxSelect.ToList()[0].StateId);
                chkStateList.SetItemChecked(chkStateList.SelectedIndex, true);
                cmbStatus.SelectedValue = Convert.ToInt32(taxSelect.ToList()[0].Status);
                m_ModifiedDate = taxSelect.ToList()[0].ModifiedDate;                
                
            }
            
        }


        /// <summary>
        /// To Search Tax Application Record
        /// </summary>
        /// <param name="taxId"></param>
        /// <param name="taxCategoryId"></param>
        /// <param name="taxTypeId"></param>
        /// <param name="taxGroupId"></param>
        /// <param name="taxJurisdiction"></param>
        /// <param name="goodsDirection"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        private List<TaxApplication> Search(int taxId, int taxCategoryId, int taxTypeId, int taxGroupId, List<int> taxJurisdiction, int goodsDirection, int status)
        {
            List<TaxApplication> m_listTaxApplication = new List<TaxApplication>();
            TaxApplication tax = new TaxApplication();

            //parentId = m_selectedParentId == Common.INT_DBNULL ? loc.ParentHierarchyId : m_selectedParentId;
            tax.TaxId = taxId;
            tax.TaxCategoryId = taxCategoryId;
            tax.TaxTypeId = taxTypeId;
            tax.TaxGroupId = taxGroupId;
            tax.States = taxJurisdiction;
            tax.GoodsDirection = goodsDirection;
            tax.Status = status;

            m_listTaxApplication = tax.Search();

            return m_listTaxApplication;
        }

        /// <summary>
        /// To reset controls
        /// </summary>
        private void ResetControl()
        {
            m_taxId = Common.INT_DBNULL;

            CoreComponent.Core.BusinessObjects.VisitControls visitControls = new CoreComponent.Core.BusinessObjects.VisitControls();
            visitControls.ResetAllControlsInPanel(errTax, pnlSearchHeader);
            m_DGVSelection = false;
            dgvTax.DataSource = new List<TaxApplication>();
            m_DGVSelection = true;
            cmbStatus.SelectedValue = 1;
            dtpStartDate.Value = DateTime.Today;
            EnableButton();
            cmbTaxCategory.Focus();
            for (int k = 0; k < chkStateList.Items.Count; k++)
            {
                chkStateList.SetItemChecked(k, true);
            }
            errTax.Clear();
        }
        private void ResetControlOnSave()
        {
            m_taxId = Common.INT_DBNULL;

            CoreComponent.Core.BusinessObjects.VisitControls visitControls = new CoreComponent.Core.BusinessObjects.VisitControls();
            visitControls.ResetAllControlsInPanel(errTax, pnlSearchHeader);            
            cmbStatus.SelectedValue = 1;
            dtpStartDate.Value = DateTime.Today;
            EnableButton();
            cmbTaxCategory.Focus();
            for (int k = 0; k < chkStateList.Items.Count; k++)
            {
                chkStateList.SetItemChecked(k, true);
            }
        }



        /// <summary>
        /// Validate Start Date, it can not be less than today date
        /// </summary>
        private void ValidateStartDate()
        {
            if (dtpStartDate.Checked == false)
                errTax.SetError(dtpStartDate, Common.GetMessage("VAL0002", lblStartDate.Text.Trim().Substring(0, lblStartDate.Text.Trim().Length - 2)));
            else if (dtpStartDate.Checked == true)
            {
                DateTime expectedDate = dtpStartDate.Checked == true ? Convert.ToDateTime(dtpStartDate.Value) : Common.DATETIME_NULL;
                DateTime dt = m_taxStartDate; //Convert.ToDateTime(DateTime.Now.ToShortDateString());
                TimeSpan ts = expectedDate - dt;
                if (ts.Days < 0)
                    errTax.SetError(dtpStartDate, Common.GetMessage("INF0010", lblStartDate.Text.Trim().Substring(0, lblStartDate.Text.Trim().Length - 2)));
                else
                    errTax.SetError(dtpStartDate, string.Empty);
            }
        }

        private void ValidateCombo(ComboBox cmb, Label lbl,bool yesNo)
        {
            if (cmb.SelectedIndex == 0 && yesNo == true)
                errTax.SetError(cmb, Common.GetMessage("INF0026", lbl.Text.Trim().Substring(0, lbl.Text.Trim().Length - 2)));
            else
                errTax.SetError(cmb, string.Empty);
        }
        /// <summary>
        /// To Validate ListBox
        /// </summary>
        /// <param name="cmb"></param>
        /// <param name="lbl"></param>
        private void ValidateListBox(CheckedListBox cmb, Label lbl)
        {
            List<int> taxJurisdiction = new List<int>();
            taxJurisdiction = GetCheckedJurisdictionIds();

            if (taxJurisdiction.Count == 0)
                errTax.SetError(cmb, Common.GetMessage("INF0026", lbl.Text.Trim().Substring(0, lbl.Text.Trim().Length - 2)));
            else
                errTax.SetError(cmb, string.Empty);
        }

        /// <summary>
        /// To check and UnCheck item 
        /// </summary>
        /// <param name="e"></param>
        private void CheckedListItemOnItemCheck(ItemCheckEventArgs e)
        {
            chkStateList.ItemCheck -= new ItemCheckEventHandler(chkLstTaxJurisdiction_ItemCheck);

            if (e.Index == 0 && e.CurrentValue == CheckState.Unchecked && m_taxId == Common.INT_DBNULL)
            {
                for (int i = 1; i < chkStateList.Items.Count; i++)
                {
                    chkStateList.SetItemChecked(i, true);
                }
            }
            else if (e.Index == 0 && e.CurrentValue == CheckState.Checked && m_taxId == Common.INT_DBNULL)
            {
                for (int i = 1; i < chkStateList.Items.Count; i++)
                {
                    chkStateList.SetItemChecked(i, false);
                }
            }
            else if(e.Index > 0 && e.NewValue == CheckState.Checked)
            {
                int i = 0;
                for (int j = 1; j < chkStateList.Items.Count; j++)
                {
                    if (chkStateList.GetItemChecked(j))
                        i++;
                }
                if (i == chkStateList.Items.Count - 2)
                    chkStateList.SetItemChecked(0, true);
            }
            else if (e.Index > 0 && e.CurrentValue == CheckState.Checked)
            {
                chkStateList.SetItemChecked(0, false);
            }
            else if (e.Index == 0 && e.CurrentValue == CheckState.Unchecked && m_taxId != Common.INT_DBNULL)
            {
                chkStateList.SetItemChecked(0, false);
            }
            chkStateList.ItemCheck += new ItemCheckEventHandler(chkLstTaxJurisdiction_ItemCheck);

        }
        #endregion

        #region Events

        #region Error Validators
        private void cmbTaxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ValidateCombo(cmbTaxCategory, lblTaxCategory,false);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbTaxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ValidateCombo(cmbTaxType, lblTaxType,false);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbTaxGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ValidateCombo(cmbTaxGroup, lblTaxGroup,false);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ValidateCombo(cmbStatus, lblStatus,false);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbGoodsDirection_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ValidateCombo(cmbGoodsDirection, lblGoodsDirection,false);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
        /// <summary>
        /// Call fn. Save() To Save Tax Application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Save();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        /// <summary>
        /// Call fn. Search() Search Tax Application records
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                List<int> taxJurisdiction = new List<int>();
                taxJurisdiction = GetCheckedJurisdictionIds();

                m_listTaxApplication = Search(Common.INT_DBNULL, Convert.ToInt32(cmbTaxCategory.SelectedValue), Convert.ToInt32(cmbTaxType.SelectedValue), Convert.ToInt32(cmbTaxGroup.SelectedValue), taxJurisdiction, Convert.ToInt32(cmbGoodsDirection.SelectedValue), Convert.ToInt32(cmbStatus.SelectedValue));
                if ((m_listTaxApplication != null) && (m_listTaxApplication.Count > 0))
                {
                    m_DGVSelection = false;
                    dgvTax.DataSource = m_listTaxApplication;
                    dgvTax.ClearSelection();
                    m_DGVSelection = true;
                }
                else
                {
                    dgvTax.DataSource = new List<Tax>();
                    MessageBox.Show(Common.GetMessage("8002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Call fn. ResetControl() to reset controls
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                ResetControl();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Call fn. SelectGridRow() Select Grid View Record
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvTax_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                SelectGridRow(e);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Call fn. ListTaxJurisdictionIndexChange
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkLstTaxJurisdiction_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ListTaxJurisdictionIndexChange();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Call fn. CheckedListItemOnItemCheckCheck To check and UnCheck item 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkLstTaxJurisdiction_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            try
            {
                CheckedListItemOnItemCheck(e);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Validate Start Date
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtpStartDate_Validated(object sender, EventArgs e)
        {
            try
            {
                ValidateStartDate();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
       
        private void dgvTax_SelectionChanged(object sender, EventArgs e)
        {
            if (m_DGVSelection)
            {
                EditTax(dgvTax.CurrentRow.Index);
            }
        } 

        #endregion
    }
}
