using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using CoreComponent.Core.BusinessObjects;
using TaxComponent.BusinessObjects;
using AuthenticationComponent.BusinessObjects;

namespace TaxComponent.UI
{
    public partial class frmTaxCode : CoreComponent.Core.UI.Transaction
    {
        #region Variable Declaration
        
        // Global Variable Declaration
        TaxCode m_taxCodeObj;
        TaxGroup m_taxGroupObj;
        List<TaxCode> m_listTaxCodeobj;
        List<TaxGroup> m_listTaxGroupobj;
        List<TaxGroup> m_listSelectedTaxCodes;
        DateTime m_dtpValue;
        Boolean m_rowSelection = true;
        Boolean m_taxCodeStatusSelection;
        Boolean m_taxGroupStatusSelection;
        
        private Boolean m_isSaveAvailable = true;
        private Boolean m_isSearchAvailable = true;
               
        private int m_userId = Authenticate.LoggedInUser.UserId;
        private string strUserName = Authenticate.LoggedInUser.UserName;
       
        private string strLocationCode = Common.LocationCode;
        private int m_currentLocationId = Common.CurrentLocationId;
        private int m_locationType = Common.CurrentLocationTypeId;
           
        #endregion 

        #region Constructor

        public frmTaxCode()
        {
            m_isSaveAvailable = Authenticate.IsFunctionAccessible(strUserName, strLocationCode, TaxCode.MODULE_CODE, Common.FUNCTIONCODE_SAVE);
            m_isSearchAvailable = Authenticate.IsFunctionAccessible(strUserName, strLocationCode, TaxCode.MODULE_CODE, Common.FUNCTIONCODE_SEARCH);    
            InitializeComponent();                         
            // Initialize WinForm Controls
            InitializeTaxCodeControls();
            InitializeTaxGroupControls();            
            m_listTaxGroupobj = new List<TaxGroup>();            
            m_listSelectedTaxCodes = new List<TaxGroup>();
            lblPageTitle.Text = "Tax Group Code";
        }

        #endregion

        #region Events

        #region TaxCode Events
        
        private void frmTaxCode_Load(object sender, EventArgs e)
        {
            try
            {
                // Set Format of Datetimepicker Controls
                dtpStartDateTaxCode.Format = DateTimePickerFormat.Custom;
                dtpStartDateTaxCode.CustomFormat = Common.DTP_DATE_FORMAT;
                dtpStartDateTaxGroup.Format = DateTimePickerFormat.Custom;
                dtpStartDateTaxGroup.CustomFormat = Common.DTP_DATE_FORMAT;

                btnSearch.Enabled = m_isSearchAvailable;
                btnSaveTaxCode.Enabled = m_isSaveAvailable;
                btnSearchTaxGroup.Enabled = m_isSearchAvailable;
                btnSave.Enabled = m_isSaveAvailable;

                cmbStatusTaxCode.SelectedValue = 1;
                cmbstatusTaxGroup.SelectedValue = 1;
                //Get Columns of DataGridView from GridViewDefinition.xml
                dgvTaxCodeSearch = Common.GetDataGridViewColumns(dgvTaxCodeSearch, Environment.CurrentDirectory + "\\App_Data\\TaxGrid.xml");
                dgvTaxGroupSearch = Common.GetDataGridViewColumns(dgvTaxGroupSearch, Environment.CurrentDirectory + "\\App_Data\\TaxGrid.xml");
                dgvTaxGroupDetail = Common.GetDataGridViewColumns(dgvTaxGroupDetail, Environment.CurrentDirectory + "\\App_Data\\TaxGrid.xml");
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                SearchTaxCode();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSaveTaxCode_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_taxCodeObj == null)
                {
                    m_taxCodeObj = new TaxCode();
                }
                if (ValidateTaxCode(false))
                {
                    DialogResult saveResult = MessageBox.Show(Common.GetMessage("INF0027"), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (saveResult == DialogResult.Yes)
                    {
                        SaveTaxCode();
                    }
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvTaxCodeSearch_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (m_rowSelection == true)
                {
                    int i = dgvTaxCodeSearch.SelectedRows[0].Index;
                    if (i > -1)
                    {
                        EditTaxCode(i,false);
                    }
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearchReset_Click(object sender, EventArgs e)
        {
            try
            {
                ResetAllTaxCodeControls();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }               

        private void txtTaxCode_Validated(object sender, EventArgs e)
        {
            try
            {
                ValidateTaxCodeTextBox(true);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtPercentageTaxCode_Validated(object sender, EventArgs e)
        {
            try
            {
                ValidatePercentageTextCode(true);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dtpStartDateTaxCode_Validated(object sender, EventArgs e)
        {
            try
            {
                ValidateStartDateDTPTaxCode(true);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbStatusTaxCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (m_taxCodeStatusSelection)
                    ValidateStatusComboBoxTaxCode(true);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvTaxCodeSearch_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1 && (dgvTaxCodeSearch.Rows[e.RowIndex].Cells[e.ColumnIndex].GetType() == typeof(DataGridViewImageCell)))
                {
                    int i = e.RowIndex;
                    if (i > -1)
                    {
                        EditTaxCode(i,true);
                    }
                }
                if (e.RowIndex > -1 && (dgvTaxCodeSearch.Rows[e.RowIndex].Cells[e.ColumnIndex].GetType() != typeof(DataGridViewImageCell)))
                {
                    int i = e.RowIndex;
                    if (i > -1)
                    {
                        EditTaxCode(i,false);
                    }
                }

            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region TaxGroup Events

        private void btnSave_Click(object sender, EventArgs e)
        {
            
        }

        void btnSaveTaxGroup_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateBeforeSave())
                {
                    DialogResult saveResult = MessageBox.Show(Common.GetMessage("INF0027"), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (saveResult == DialogResult.Yes)
                    {
                        SaveTaxGroup();
                    }
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearchTaxGroup_Click(object sender, EventArgs e)
        {
            try
            {
                SearchTaxGroup();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvTaxGroupSearch_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1 && (dgvTaxGroupSearch.Rows[e.RowIndex].Cells[e.ColumnIndex].GetType() == typeof(DataGridViewImageCell)))
                {
                    int i = Convert.ToInt32(dgvTaxGroupSearch.CurrentRow.Cells["TaxGroupId"].Value);
                    if (i > -1)
                    {
                        EditTaxGroup(i,true);
                    }                    
                }
                if (e.RowIndex > -1 && (dgvTaxGroupSearch.Rows[e.RowIndex].Cells[e.ColumnIndex].GetType() != typeof(DataGridViewImageCell)))
                {
                    int i = Convert.ToInt32(dgvTaxGroupSearch.CurrentRow.Cells["TaxGroupId"].Value);
                    if (i > -1)
                    {
                        EditTaxGroup(i,false);
                    } 
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }      

        private void dgvTaxGroupSearch_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (m_rowSelection == true)
                {
                    int i = Convert.ToInt32(dgvTaxGroupSearch.CurrentRow.Cells["TaxGroupId"].Value);
                    if (i > -1)
                    {
                        EditTaxGroup(i,false);
                    }
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }        
              
        private void txtTaxGroupCode_Validated(object sender, EventArgs e)
        {
            try
            {
                ValidateTaxGroupCodeTextBox(true);                
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
                     
        }        

        private void txtGroupOrder_Validated(object sender, EventArgs e)
        {
            try
            {
                ValidateGroupOrderTextBox(true);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbTaxCode_Validated(object sender, EventArgs e)
        {
            try
            {
                ValidateTaxCodeCombo(true);                
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }        

        private void cmbstatusTaxGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (m_taxGroupStatusSelection == true)
                {
                    ValidateStatusTaxGroup(true);
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dtpStartDateTaxGroup_Validated(object sender, EventArgs e)
        {
            try
            {
                ValidateStartDateDTPTaxGroup(true);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnResetTaxGroup_Click(object sender, EventArgs e)
        {
            try
            {
                ResetAllTaxGroupControls();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddTGDetail_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateTaxGroup(false))
                {
                    //EnableDisableTGHeaderControls(false);
                    if (CheckTaxCodeRedundancy() && CheckGroupOrder())
                    {
                        AddTGDetails();
                    }
                    
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnResetTGDetail_Click(object sender, EventArgs e)
        {
            try
            {
                ResetTGHeaderAndDetail();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbTaxCodes_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if(m_rowSelection)
                ValidateTaxCodeCombo(true);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbTaxGroupAppliedOn_SelectedIndexChanged(object sender, EventArgs e)
        {
            try                
            {
                if(m_rowSelection)
                ValidateAppliedOnComboBox(true);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        private void tabControlTransaction_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (tabControlTransaction.SelectedIndex == 0)
                    ResetAllTaxCodeControls();
                else
                    ResetAllTaxGroupControls();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvTaxGroupDetail_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && (dgvTaxGroupDetail.Rows[e.RowIndex].Cells[e.ColumnIndex].GetType() == typeof(DataGridViewImageCell)))
            {
                if (m_listSelectedTaxCodes.Count == 0)
                {
                    MessageBox.Show(Common.GetMessage("VAL0073", lblAddDetails.Text), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    DialogResult dr = MessageBox.Show(Common.GetMessage("5012"), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        m_listSelectedTaxCodes.RemoveAt(e.RowIndex);
                        BindTaxGroupGrid(dgvTaxGroupDetail, m_listSelectedTaxCodes);
                    }
                }
            }
        }

        #endregion

        #region Methods

        #region Methods for TaxCode Tab

        /// <summary>
        /// Search TaxCode
        /// </summary>
        private void SearchTaxCode()
        {
            if (m_taxCodeObj == null)
                m_taxCodeObj = new TaxCode();
            m_listTaxCodeobj = new List<TaxCode>();
            m_rowSelection = false;
            string errorMsg = string.Empty;
            errProviderTaxCode.Clear();
            m_listTaxCodeobj.Clear();
            m_taxCodeObj.TaxCodeVal = txtTaxCode.Text;
            m_taxCodeObj.Status = Convert.ToInt32(cmbStatusTaxCode.SelectedValue.ToString());
            m_listTaxCodeobj =  m_taxCodeObj.SearchTaxCode(ref errorMsg);

            if (m_listTaxCodeobj.Count > 0)
            {                    
               dgvTaxCodeSearch.DataSource = m_listTaxCodeobj;
               dgvTaxCodeSearch.ClearSelection();
               m_rowSelection = true; 
            }
            else
            {
               dgvTaxCodeSearch.DataSource = new List<TaxCode>();
               MessageBox.Show(Common.GetMessage("8002"),Common.GetMessage("10001"),MessageBoxButtons.OK,MessageBoxIcon.Information);
            }                      
        }
        
        /// <summary>
        /// Initialize controls TaxCode Tab
        /// </summary>
        private void InitializeTaxCodeControls()
        {
                // Populate Items in Status ComboBox.
                m_taxCodeStatusSelection = false;
                DataTable dtstatus = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter("STATUS", 0, 0, 0));
                cmbStatusTaxCode.DataSource = dtstatus;
                cmbStatusTaxCode.DisplayMember = Common.KEYVALUE1;
                cmbStatusTaxCode.ValueMember = Common.KEYCODE1;
                m_taxCodeStatusSelection = true;                      
        }
               
        /// <summary>
        /// Save TaxCode
        /// </summary>
        private void SaveTaxCode()
        {
            
            bool isSuccess = false;
            string errorMsg = string.Empty;
            m_taxCodeObj.TaxCodeVal = txtTaxCode.Text;
            m_taxCodeObj.StartDate = dtpStartDateTaxCode.Value.ToShortDateString();
            m_taxCodeObj.TaxPercent = Math.Round(Convert.ToDecimal(txtPercentageTaxCode.Text),Common.DisplayAmountRounding);
            m_taxCodeObj.Description = txtDescriptionTaxCode.Text;
            m_taxCodeObj.Status = Convert.ToInt32(cmbStatusTaxCode.SelectedValue.ToString());
            if (m_taxCodeObj.TaxCodeId == 0)
              {
                 m_taxCodeObj.ModifiedBy = m_userId;                 
              }
            else
              {
                 m_taxCodeObj.ModifiedBy = m_userId;                 
              }
               
                isSuccess =  m_taxCodeObj.SaveTaxCode(ref errorMsg);

                if (isSuccess == true)
                {
                    MessageBox.Show(Common.GetMessage("8001"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SearchTaxCode();
                    ResetTaxCodeControls();
                }
                else
                {
                    if (errorMsg == "INF0089")
                        MessageBox.Show(Common.GetMessage(errorMsg, txtTaxCode.Text), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else
                        MessageBox.Show(Common.GetMessage(errorMsg), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }           
        }

        /// <summary>
        /// Validate All Controls on TaxCode Tab
        /// </summary>
        /// <returns></returns>
        private Boolean ValidateTaxCode(Boolean yesNo)
        {
            bool ret = true;
            StringBuilder msg = new StringBuilder();
            
                // Validate TaxCode TextBox
                ValidateTaxCodeTextBox(yesNo);
                
                // Validate DateTimePicker StartDateTaxCode
                ValidateStartDateDTPTaxCode(yesNo);

                // Validate Tax Percentage, should be numeric, between 0 and 100.
                ValidatePercentageTextCode(yesNo);
                
                // Validate checkState of checkBox.
                //ValidateIsFormCApplicableCheckBox(yesNo);
                
                // Validate Status ComboBox
                ValidateStatusComboBoxTaxCode(yesNo);

                // Generate Validation ErrorMessage String
                if (errProviderTaxCode.GetError(txtTaxCode).Trim().Length > 0)
                {
                    msg.Append(errProviderTaxCode.GetError(txtTaxCode));
                    msg.AppendLine();
                }
                if (errProviderTaxCode.GetError(dtpStartDateTaxCode).Trim().Length > 0)
                {
                    msg.Append(errProviderTaxCode.GetError(dtpStartDateTaxCode));
                    msg.AppendLine();
                }
                if (errProviderTaxCode.GetError(txtPercentageTaxCode).Trim().Length > 0)
                {
                    msg.Append(errProviderTaxCode.GetError(txtPercentageTaxCode));
                    msg.AppendLine();
                }
                if (errProviderTaxCode.GetError(txtDescriptionTaxCode).Trim().Length > 0)
                {
                    msg.Append(errProviderTaxCode.GetError(txtDescriptionTaxCode));
                    msg.AppendLine();         
                }
                //if (errProviderTaxCode.GetError(chkIsFormCApplicableTaxCode).Trim().Length > 0)
                //{
                //    msg.Append(errProviderTaxCode.GetError(chkIsFormCApplicableTaxCode));
                //    msg.AppendLine();
                //}
                if (errProviderTaxCode.GetError(cmbStatusTaxCode).Trim().Length > 0)
                {
                    msg.Append(errProviderTaxCode.GetError(cmbStatusTaxCode));
                    msg.AppendLine();
                }

                if (msg.ToString().Trim().Length > 0)
                {
                    ret = false;
                    msg = Common.ReturnErrorMessage(msg);
                    MessageBox.Show(msg.ToString(), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return ret;            
        }
               
        /// <summary>
        /// Switch to Edit Mode on TaxCode tab
        /// </summary>
        /// <param name="index"></param>
        private void EditTaxCode(int index, bool yesNo)
        {
            if (m_listTaxCodeobj != null)
            {
                //btnSearch.Enabled = false;   
                btnSaveTaxCode.Enabled = yesNo && m_isSaveAvailable;
                TaxCode taxCodeObj = new TaxCode();
                taxCodeObj = m_listTaxCodeobj[index];
                m_taxCodeObj.TaxCodeId = taxCodeObj.TaxCodeId;
                txtTaxCode.Text = taxCodeObj.TaxCodeVal;
                dtpStartDateTaxCode.Value = taxCodeObj.StartDateVal;
                m_dtpValue = taxCodeObj.StartDateVal;
                dtpStartDateTaxCode.Checked = true;
                txtPercentageTaxCode.Text = taxCodeObj.TaxPercent.ToString();
                txtDescriptionTaxCode.Text = taxCodeObj.Description;                
                cmbStatusTaxCode.SelectedValue = taxCodeObj.Status;
            }
        }

        /// <summary>
        /// Reset All Controls on TaxCode Tab
        /// </summary>
        private void ResetAllTaxCodeControls()
        {
            ResetTaxCodeControls();
            m_rowSelection = false;
            dgvTaxCodeSearch.DataSource = new List<TaxCode>();
            m_rowSelection = true;
        }

        /// <summary>
        /// Reset All TaxCode Tab Controls
        /// </summary>
        private void ResetTaxCodeControls()
        {
            if (m_taxCodeObj == null)
            {
                m_taxCodeObj = new TaxCode();
            }              
                txtTaxCode.Focus();
                m_taxCodeObj.TaxCodeId = 0;
                txtTaxCode.Text = string.Empty;
                dtpStartDateTaxCode.Value = DateTime.Today;
                dtpStartDateTaxCode.Checked = false;
                txtPercentageTaxCode.Text = string.Empty;
                txtDescriptionTaxCode.Text = string.Empty;               
                cmbStatusTaxCode.SelectedValue = 1;                
                btnSearch.Enabled = m_isSearchAvailable;
                btnSaveTaxCode.Enabled = m_isSaveAvailable;
                errProviderTaxCode.Clear();
                
        }


        #endregion
        
        #region Methods for TaxGroup Tab

        /// <summary>
        /// Initialize Controls on TaxGroup Tab
        /// </summary>
        private void InitializeTaxGroupControls()
        {
                // Populate TaxCode ComboBox.
                InitializeTaxCodeComboBox();

                // Populate AppliedOn ComboBox.
                m_rowSelection = false;
                DataTable dtAppliedOn = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter("TAXAPPLIEDON", 0, 0, 0));
                cmbTaxGroupAppliedOn.DataSource = dtAppliedOn;
                cmbTaxGroupAppliedOn.DisplayMember = Common.KEYVALUE1;
                cmbTaxGroupAppliedOn.ValueMember = Common.KEYCODE1;
                m_rowSelection = true;
                // Populate Status ComboBox.
                m_taxGroupStatusSelection = false;
                DataTable dtstatus = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter("STATUS", 0, 0, 0));
                cmbstatusTaxGroup.DataSource = dtstatus;
                cmbstatusTaxGroup.DisplayMember = Common.KEYVALUE1;
                cmbstatusTaxGroup.ValueMember = Common.KEYCODE1;
                m_taxGroupStatusSelection = true;                       
        }
        
        /// <summary>
        /// Populate TaxCodeListBox
        /// </summary>
        private void InitializeTaxCodeComboBox()
        {
            m_rowSelection = false;
            DataTable dtTaxCode = Common.ParameterLookup(Common.ParameterType.TaxCode, new ParameterFilter("", 0, 0, 0));
            cmbTaxCodes.DataSource = dtTaxCode;
            cmbTaxCodes.DisplayMember = Common.TAX_CODE;
            cmbTaxCodes.ValueMember = Common.TAX_CODE_ID;
            m_rowSelection = true;
        }
        
        /// <summary>
        /// Reset all controls on TaxGroup Tab
        /// </summary>
        private void ResetAllTaxGroupControls()
        {
            ResetTaxGroupControls();            
            m_rowSelection = false;
            dgvTaxGroupSearch.DataSource = new List<TaxGroup>();
            dgvTaxGroupDetail.DataSource = new List<TaxGroup>();
            m_rowSelection = true;
            m_listTaxGroupobj.Clear();
            m_listTaxGroupobj.Clear();
            btnSaveTaxGroup.Enabled = m_isSaveAvailable;
            btnSearchTaxGroup.Enabled = m_isSearchAvailable;
            dgvTaxGroupDetail.Columns[0].Visible = true;
        }

        /// <summary>
        /// Reset TaxGroup Controls
        /// </summary>
        private void ResetTaxGroupControls()
        {
            //Reset Objects
            if (m_taxGroupObj == null)
                m_taxGroupObj = new TaxGroup();
            m_taxGroupObj.TaxGroupId = 0;
            m_listSelectedTaxCodes.Clear();
            InitializeTaxCodeComboBox();            

            //Reset Controls            
            btnSaveTaxGroup.Enabled = false;
            btnAddTGDetail.Enabled = true;
            btnResetTGDetail.Enabled = true;
            txtTaxGroupCode.Enabled = true;
            txtGroupOrder.Enabled = true;
            cmbTaxGroupAppliedOn.Enabled = true;
            dtpStartDateTaxGroup.Enabled = true;
            cmbstatusTaxGroup.Enabled = true;
            cmbTaxCodes.Enabled = true;
            txtGroupOrder.Enabled = true;
            txtTaxGroupCode.Focus();
            txtTaxGroupCode.Text = string.Empty;
            txtGroupOrder.Text = string.Empty;
            cmbTaxGroupAppliedOn.SelectedValue = Common.INT_DBNULL;
            dtpStartDateTaxGroup.Value = DateTime.Now;
            dtpStartDateTaxGroup.Checked = false;                     
            errorProviderTaxGroup.Clear();
            cmbstatusTaxGroup.SelectedValue = 1;
        }

        /// <summary>
        /// Search TaxGroup
        /// </summary>
        private void SearchTaxGroup()
        {
           errorProviderTaxGroup.Clear();
           if (m_taxGroupObj == null)
              m_taxGroupObj = new TaxGroup();
           string errMsg = String.Empty;           
           string appliedOnText = cmbTaxGroupAppliedOn.SelectedValue.ToString();
           m_listTaxGroupobj.Clear();           
           m_taxGroupObj.TaxGroupCode = txtTaxGroupCode.Text;
           m_taxGroupObj.TaxCodeId = Convert.ToInt32(cmbTaxCodes.SelectedValue);
           m_taxGroupObj.AppliedOn = cmbTaxGroupAppliedOn.SelectedValue.ToString();
           m_taxGroupObj.Status = Convert.ToInt32(cmbstatusTaxGroup.SelectedValue);

           m_listTaxGroupobj =  m_taxGroupObj.SearchTaxGroup(ref errMsg);
           if (m_listTaxGroupobj.Count > 0)
           {              
               var query = (from q in m_listTaxGroupobj
                            select
                                          new
                                          {
                                                TaxGroupId = q.TaxGroupId,
                                                TaxGroupCode= q.TaxGroupCode.ToUpper(),
                                                AppliedOn=q.AppliedOn,
                                                AppliedOnText = q.AppliedOnText,
                                                StartDate=q.StartDate,
                                                Status=q.Status,
                                                StatusName=q.StatusName,
                                                StartDateVal = q.StartDateVal
                                           }
                        ).Distinct();
               if (query != null && query.ToList().Count > 0)
               {
                   m_rowSelection = false;                   
                   dgvTaxGroupSearch.DataSource = query.ToList();
                   dgvTaxGroupSearch.ClearSelection();
                   m_rowSelection = true;
               }               
           }
           else
           {
               dgvTaxGroupDetail.DataSource = null;
               m_rowSelection = false;
               dgvTaxGroupSearch.DataSource = null;
               m_rowSelection = true;
               MessageBox.Show(Common.GetMessage("8002"),Common.GetMessage("10001"),MessageBoxButtons.OK,MessageBoxIcon.Information);
           }          
        }
       
        /// <summary>
        /// Method to Bind DataGRidview 
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="lstTG"></param>      
        private void BindTaxGroupGrid(DataGridView dgv, List<TaxGroup> lstTG)
        {
            m_rowSelection = false;            
            dgv.DataSource = null;
            dgv.DataSource = lstTG;
            if (m_taxGroupObj.TaxGroupId > 0)
                dgvTaxGroupDetail.Columns[0].Visible = false;
            else
                dgvTaxGroupDetail.Columns[0].Visible = true;
            dgv.ClearSelection();
            m_rowSelection = true;

        }
               
        /// <summary>
        /// Save TaxGroup data 
        /// </summary>
        private void SaveTaxGroup()
        {
            if (m_taxGroupObj == null)
            {
                m_taxGroupObj = new TaxGroup();
            }
                bool isSuccess = false;
                string errorMsg = string.Empty;
                List<TaxGroup> lstTG = new List<TaxGroup>();
                for (int count = 0; count < m_listTaxGroupobj.Count; count++)
                    lstTG.Add((TaxGroup)m_listTaxGroupobj[count]);
                m_listTaxGroupobj.Clear();
                m_taxGroupObj.TaxGroupCode = txtTaxGroupCode.Text.Trim();
                m_taxGroupObj.AppliedOn = cmbTaxGroupAppliedOn.SelectedValue.ToString();
                m_taxGroupObj.StartDate = dtpStartDateTaxGroup.Value;
                m_taxGroupObj.Status = Convert.ToInt32(cmbstatusTaxGroup.SelectedValue);                
                if (m_taxGroupObj.TaxGroupId > 0)
                {
                    var query = (from p in m_listTaxGroupobj where p.TaxGroupId == m_taxGroupObj.TaxGroupId select p);
                    m_listTaxGroupobj = query.ToList<TaxGroup>();
                }
                for (int i = 0; i < m_listSelectedTaxCodes.Count; i++)
                {
                    TaxGroup obj = new TaxGroup();                    
                    obj.TaxGroupCode = txtTaxGroupCode.Text;
                    obj.AppliedOn = cmbTaxGroupAppliedOn.SelectedValue.ToString();
                    obj.StartDate = dtpStartDateTaxGroup.Value;
                    obj.Status = Convert.ToInt32(cmbstatusTaxGroup.SelectedValue);
                    obj.TaxCodeId = m_listSelectedTaxCodes[i].TaxCodeId;
                    obj.GroupOrder = m_listSelectedTaxCodes[i].GroupOrder;
                    m_listTaxGroupobj.Add(obj);
                }
                m_taxGroupObj.TaxGroupList = m_listTaxGroupobj;
                m_taxGroupObj.ModifiedBy = m_userId;  
                isSuccess = m_taxGroupObj.SaveTaxGroup(ref errorMsg);

                if (isSuccess)
                {
                    MessageBox.Show(Common.GetMessage("8001"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SearchTaxGroup();
                    BindTaxGroupDetailGrid();
                    ResetTaxGroupControls();
                }
                else
                {
                    MessageBox.Show(Common.GetMessage(errorMsg), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    m_listTaxGroupobj.Clear();
                    for (int count = 0; count < lstTG.Count; count++)
                        m_listTaxGroupobj.Add((TaxGroup)lstTG[count]);                                     
                }
        }
        
        /// <summary>
        /// Switch to Edit Mode on TaxGroup Tab
        /// </summary>
        /// <param name="index"></param>
        private void EditTaxGroup(int index,bool yesNo)
        {
            if (m_listTaxGroupobj != null)
            {
                //btnSearchTaxGroup.Enabled = false;
                btnAddTGDetail.Enabled = false;
                btnResetTGDetail.Enabled = false;
                txtTaxGroupCode.Enabled = !yesNo;
                cmbTaxGroupAppliedOn.Enabled = !yesNo;
                cmbTaxCodes.Enabled = false;
                txtGroupOrder.Enabled = false;
                btnSaveTaxGroup.Enabled = yesNo && m_isSaveAvailable;

                TaxGroup taxGroupObj = new TaxGroup();
                // Filter record for Tax Group header DataGrid
                var qry = (from p in m_listTaxGroupobj where p.TaxGroupId == index select p);
                if(qry.ToList().Count > 0)
                    taxGroupObj =  qry.ToList()[0];
                m_taxGroupObj.TaxGroupId = taxGroupObj.TaxGroupId;
                txtTaxGroupCode.Text = taxGroupObj.TaxGroupCode;
                cmbTaxGroupAppliedOn.SelectedValue = Convert.ToInt32(taxGroupObj.AppliedOn);
                m_dtpValue = taxGroupObj.StartDate;
                dtpStartDateTaxGroup.Value = taxGroupObj.StartDate;
                dtpStartDateTaxGroup.Checked = true;
                cmbstatusTaxGroup.SelectedValue = taxGroupObj.Status;
                BindTaxGroupDetailGrid(); 
            }
        }
        
        /// <summary>
        /// BInd Tax Group Detail Grid
        /// </summary>
        private void BindTaxGroupDetailGrid()
        {
            // Filter record for Tax Group Detail DataGrid
            var query = (from p in m_listTaxGroupobj
                         where p.TaxGroupId == Convert.ToInt32(dgvTaxGroupSearch.CurrentRow.Cells["TaxGroupId"].Value)
                         select
                                new
                                {
                                    TaxGroupId = p.TaxGroupId,
                                    TaxCodeId = p.TaxCodeId,
                                    TaxCode = p.TaxCode,
                                    GroupOrder = p.GroupOrder
                                }
                    );
            dgvTaxGroupDetail.DataSource = query.ToList();
            if (m_taxGroupObj.TaxGroupId > 0)
                dgvTaxGroupDetail.Columns[0].Visible = false;
            dgvTaxGroupDetail.ClearSelection();
        }
        
        /// <summary>
        /// Validate All controls on TaxGroup tab
        /// </summary>
        /// <returns></returns>
        private Boolean ValidateTaxGroup(Boolean yesNo)
        {
            bool ret = true;
            StringBuilder msg = new StringBuilder();
                
                ValidateTaxCodeCombo(yesNo);
                ValidateGroupOrderTextBox(yesNo);              
                
                if (errorProviderTaxGroup.GetError(cmbTaxCodes).Trim().Length > 0)
                {
                    msg.Append(errorProviderTaxGroup.GetError(cmbTaxCodes));
                    msg.AppendLine();
                }
                if (errorProviderTaxGroup.GetError(txtGroupOrder).Trim().Length > 0)
                {
                    msg.Append(errorProviderTaxGroup.GetError(txtGroupOrder));
                    msg.AppendLine();
                }

                if (msg.ToString().Trim().Length > 0)
                {
                    ret = false;
                    msg = Common.ReturnErrorMessage(msg);
                    MessageBox.Show(msg.ToString(),Common.GetMessage("10001"),MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }

                return ret;            
        }
        
        /// <summary>
        /// Method to add TaxGroup Detail to object
        /// </summary>
        private void AddTGDetails()
        {
            TaxGroup objTG = new TaxGroup();           
            objTG.TaxCodeId = Convert.ToInt32(cmbTaxCodes.SelectedValue);
            objTG.TaxCode = cmbTaxCodes.Text;
            objTG.GroupOrder = Convert.ToInt32(txtGroupOrder.Text);            
            m_listSelectedTaxCodes.Add(objTG);
            BindTaxGroupGrid(dgvTaxGroupDetail, m_listSelectedTaxCodes);
            cmbTaxCodes.SelectedValue = Common.INT_DBNULL;
            txtGroupOrder.Text = string.Empty;
            cmbTaxCodes.Focus();
        }

        /// <summary>
        /// Enable Disable Controls on tax group header
        /// </summary>
        /// <param name="yesNo"></param>
        private void EnableDisableTGHeaderControls(bool yesNo)
        {
            txtTaxGroupCode.Enabled = yesNo;
            cmbTaxGroupAppliedOn.Enabled = yesNo;
            dtpStartDateTaxGroup.Enabled = yesNo;
            cmbstatusTaxGroup.Enabled = yesNo;
        }

        /// <summary>
        /// Reset header details
        /// </summary>
        private void ResetTGHeaderAndDetail()
        {
            //EnableDisableTGHeaderControls(true);
            dgvTaxGroupDetail.DataSource = new List<TaxGroup>();
            cmbTaxCodes.SelectedValue = Common.INT_DBNULL;
            txtGroupOrder.Text = String.Empty;
            errorProviderTaxGroup.SetError(cmbTaxCodes, String.Empty);
            errorProviderTaxGroup.SetError(txtGroupOrder, String.Empty);
            m_listSelectedTaxCodes.Clear();
        }

        /// <summary>
        /// Check for TaxCode redundancy while creating tax group
        /// </summary>
        /// <returns></returns>
        private bool CheckTaxCodeRedundancy()
        {
            var query = (from p in m_listSelectedTaxCodes where p.TaxCodeId == Convert.ToInt32(cmbTaxCodes.SelectedValue) select p);
            if (query.Count() > 0)
            {
                MessageBox.Show(Common.GetMessage("INF0094", lblTaxCode.Text.Substring(0, lblTaxCode.Text.Length - 2)), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else
                return true;            
        }

        /// <summary>
        /// Check for valid group order.
        /// first order no should be 1
        /// and should increase in sequence.
        /// and can be repeated.
        /// </summary>
        /// <returns></returns>
        private bool CheckGroupOrder()
        {
            int max;
            var query = (from p in m_listSelectedTaxCodes select p.GroupOrder);
            if (query.Count() > 0)
            {
                max = query.Max();
                if (Convert.ToInt32(txtGroupOrder.Text)> 0 && Convert.ToInt32(txtGroupOrder.Text) <= max + 1)
                    return true;
                else
                {
                    MessageBox.Show(Common.GetMessage("INF0010", lblGroupOrder.Text.Substring(0, lblGroupOrder.Text.Length - 2)), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            else
            {
                if (Convert.ToInt32(txtGroupOrder.Text) == 1)
                    return true;
                else
                {
                    MessageBox.Show(Common.GetMessage("INF0010", lblGroupOrder.Text.Substring(0, lblGroupOrder.Text.Length - 2)), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
        }

        /// <summary>
        /// Validate Before Save taxgroup
        /// </summary>
        /// <returns></returns>
        private bool ValidateBeforeSave()
        {
            if( m_taxGroupObj == null)
                m_taxGroupObj = new TaxGroup();
            
                bool ret = true;
                StringBuilder msg = new StringBuilder();
                ValidateTaxGroupCodeTextBox(false);
                ValidateAppliedOnComboBox(false);
                ValidateStartDateDTPTaxGroup(false);
                ValidateStatusTaxGroup(false);
                if (errorProviderTaxGroup.GetError(txtTaxGroupCode).Trim().Length > 0)
                {
                    msg.Append(errorProviderTaxGroup.GetError(txtTaxGroupCode));
                    msg.AppendLine();
                }
                if (errorProviderTaxGroup.GetError(cmbTaxGroupAppliedOn).Trim().Length > 0)
                {
                    msg.Append(errorProviderTaxGroup.GetError(cmbTaxGroupAppliedOn));
                    msg.AppendLine();
                }
                if (errorProviderTaxGroup.GetError(dtpStartDateTaxGroup).Trim().Length > 0)
                {
                    msg.Append(errorProviderTaxGroup.GetError(dtpStartDateTaxGroup));
                    msg.AppendLine();
                }
                if (errorProviderTaxGroup.GetError(cmbstatusTaxGroup).Trim().Length > 0)
                {
                    msg.Append(errorProviderTaxGroup.GetError(cmbstatusTaxGroup));
                    msg.AppendLine();
                }
                if ((m_listSelectedTaxCodes.Count == 0 && m_taxGroupObj.TaxGroupId == 0) || (m_listTaxGroupobj.Count == 0 && m_taxGroupObj.TaxGroupId > 0))
                {
                    msg.Append(Common.GetMessage("VAL0002", lblAddDetails.Text));
                    msg.AppendLine();
                }
                if (m_taxGroupObj.TaxGroupId == 0 && m_listSelectedTaxCodes.Count > 0)
                {
                    int max;
                    int count=0;
                    var query = (from p in m_listSelectedTaxCodes select p.GroupOrder);
                    if (query.Count() > 0)
                    {
                        max = query.Max();
                        for (int x = 1; x <= max; x++)
                        {
                            count = 0;
                            for (int y = 0; y < m_listSelectedTaxCodes.Count; y++)
                            {
                                if (m_listSelectedTaxCodes[y].GroupOrder == x)
                                    count++;
                            }
                            if (count == 0)
                            {
                                msg.AppendLine(Common.GetMessage("INF0010", lblGroupOrder.Text.Substring(0, lblGroupOrder.Text.Length - 2)));
                                ret = false;
                                break;
                            }
                        }
                    }                    
                }

                if (msg.ToString().Trim().Length > 0)
                {
                    ret = false;
                    msg = Common.ReturnErrorMessage(msg);
                    MessageBox.Show(msg.ToString(), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return ret;
            }           
        

        #endregion


        #endregion

        #region Validations

        #region Methods to Validate TaxCode

        void ValidateTaxCodeTextBox(Boolean yesNo)
        {
            if (txtTaxCode.Text.Trim().Length == 0 && yesNo == false)
            {
                errProviderTaxCode.SetError(txtTaxCode, Common.GetMessage("VAL0001", lblCode.Text.Trim().Substring(0,lblCode.Text.Trim().Length - 2)));
            }
            else if(txtTaxCode.Text.Length > 0 && yesNo == false)
            {
                errProviderTaxCode.SetError(txtTaxCode, Common.CodeValidate(txtTaxCode.Text, lblCode.Text.Substring(0, lblCode.Text.Length - 2)));
            }
            else
            {
                errProviderTaxCode.SetError(txtTaxCode, string.Empty);
            }
        }

        void ValidateStartDateDTPTaxCode(Boolean yesNo)
        {
            if (dtpStartDateTaxCode.Checked == false && yesNo == false)
            {
                errProviderTaxCode.SetError(dtpStartDateTaxCode, Common.GetMessage("VAL0002", lblStartDate.Text.Trim().Substring(0,lblStartDate.Text.Trim().Length - 2)));
            }
            else
            {
              
              if (m_taxCodeObj != null && m_taxCodeObj.TaxCodeId == 0 && yesNo == false)
              {
                   if (dtpStartDateTaxCode.Value < DateTime.Today)
                       errProviderTaxCode.SetError(dtpStartDateTaxCode, Common.GetMessage("VAL0072", lblStartDate.Text.Trim().Substring(0, lblStartDate.Text.Trim().Length - 2), "Current Date"));
                   else
                       errProviderTaxCode.SetError(dtpStartDateTaxCode, string.Empty);
              }
              else if (m_taxCodeObj != null && m_taxCodeObj.TaxCodeId > 0 && yesNo == false)
              {
                   if (dtpStartDateTaxCode.Value < m_dtpValue && yesNo == false)
                      errProviderTaxCode.SetError(dtpStartDateTaxCode, Common.GetMessage("VAL0072", lblStartDate.Text.Trim().Substring(0, lblStartDate.Text.Trim().Length - 2), "Existing Start Date"));
                   else
                      errProviderTaxCode.SetError(dtpStartDateTaxCode, string.Empty);
              }
                
            }
        }

        void ValidatePercentageTextCode(Boolean yesNo)
        {
            if (((txtPercentageTaxCode.Text.Trim().Length == 0) || (!Validators.IsDecimal(txtPercentageTaxCode.Text)) || !Validators.IsValidAmount(txtPercentageTaxCode.Text) || (Convert.ToDecimal(txtPercentageTaxCode.Text) > 100) || (Convert.ToDecimal(txtPercentageTaxCode.Text) < 0)) && yesNo == false)
                errProviderTaxCode.SetError(txtPercentageTaxCode, Common.GetMessage("VAL0001", lblPercentage.Text.Trim().Substring(0, lblPercentage.Text.Trim().Length - 2)));
            else
                errProviderTaxCode.SetError(txtPercentageTaxCode, string.Empty);
        }
        
        void ValidateStatusComboBoxTaxCode(Boolean yesNo)
        {
            if (Convert.ToInt32(cmbStatusTaxCode.SelectedValue) == Common.INT_DBNULL && yesNo == false)
                errProviderTaxCode.SetError(cmbStatusTaxCode, Common.GetMessage("VAL0002", lblStatus.Text.Trim().Substring(0, lblStatus.Text.Trim().Length - 2)));
            else
                errProviderTaxCode.SetError(cmbStatusTaxCode, string.Empty);
        }

        #endregion

        #region Methods to Validate TaxGroup Tab

        void ValidateTaxGroupCodeTextBox(Boolean yesNo)
        {
            if (txtTaxGroupCode.Text.Trim().Length == 0 && yesNo == false)
                errorProviderTaxGroup.SetError(txtTaxGroupCode, Common.GetMessage("VAL0001", lblTaxGroupCode.Text.Trim().Substring(0, lblTaxGroupCode.Text.Trim().Length - 2)));
            else if (txtTaxGroupCode.Text.Length > 0 && yesNo == false)
                errorProviderTaxGroup.SetError(txtTaxGroupCode, Common.CodeValidate(txtTaxGroupCode.Text, lblTaxGroupCode.Text.Substring(0, lblTaxGroupCode.Text.Length - 2)));
            else
                errorProviderTaxGroup.SetError(txtTaxGroupCode, string.Empty);                         
        }

        void ValidateTaxCodeCombo(Boolean yesNo)
        {
            if (Convert.ToInt32(cmbTaxCodes.SelectedValue) == -1 && yesNo == false)
            {
                errorProviderTaxGroup.SetError(cmbTaxCodes, Common.GetMessage("VAL0002", lblTaxCode.Text.Trim().Substring(0, lblTaxCode.Text.Trim().Length - 2)));
            }
            else
            {
               errorProviderTaxGroup.SetError(cmbTaxCodes, string.Empty);
            }            
        }

        void ValidateGroupOrderTextBox(Boolean yesNo)
        {
            if (txtGroupOrder.Enabled == true)
            {
                if (((txtGroupOrder.Text.Trim().Length == 0) || (!Validators.IsInt32(txtGroupOrder.Text)) || (Convert.ToInt32(txtGroupOrder.Text) < 0) )&& yesNo == false)
                {
                    errorProviderTaxGroup.SetError(txtGroupOrder, Common.GetMessage("VAL0001", lblGroupOrder.Text.Trim().Substring(0, lblGroupOrder.Text.Trim().Length - 2)));
                }
                else
                {
                    errorProviderTaxGroup.SetError(txtGroupOrder, string.Empty);
                }
            }
        }

        void ValidateAppliedOnComboBox(Boolean yesNo)
        {
            if (Convert.ToInt32(cmbTaxGroupAppliedOn.SelectedValue) == Common.INT_DBNULL && yesNo == false)
            {
                errorProviderTaxGroup.SetError(cmbTaxGroupAppliedOn, Common.GetMessage("VAL0002", lblAppliedOn.Text.Trim().Substring(0, lblAppliedOn.Text.Trim().Length - 2)));
            }
            else
            {
                errorProviderTaxGroup.SetError(cmbTaxGroupAppliedOn, string.Empty);
            }
        }

        void ValidateStartDateDTPTaxGroup(Boolean yesNo)
        {            
            {
                if (dtpStartDateTaxGroup.Checked == false && yesNo == false)
                    errorProviderTaxGroup.SetError(dtpStartDateTaxGroup, Common.GetMessage("VAL0002", lblStartDateTaxGroup.Text.Trim().Substring(0, lblStartDateTaxGroup.Text.Trim().Length - 2)));
                else
                {
                    errorProviderTaxGroup.SetError(dtpStartDateTaxGroup, string.Empty);
                    if (m_taxGroupObj != null)
                    {
                        if (m_taxGroupObj.TaxGroupId == 0 && yesNo == false)
                        {
                            if (dtpStartDateTaxGroup.Value < DateTime.Today)
                                errorProviderTaxGroup.SetError(dtpStartDateTaxGroup, Common.GetMessage("VAL0072", lblStartDateTaxGroup.Text.Trim().Substring(0, lblStartDateTaxGroup.Text.Trim().Length - 2), "Current Date"));
                            else
                                errorProviderTaxGroup.SetError(dtpStartDateTaxGroup, string.Empty);
                        }
                        else
                        {
                            if (dtpStartDateTaxGroup.Value < m_dtpValue && yesNo == false)
                                errorProviderTaxGroup.SetError(dtpStartDateTaxGroup, Common.GetMessage("VAL0072", lblStartDateTaxGroup.Text.Trim().Substring(0, lblStartDateTaxGroup.Text.Trim().Length - 2), "Existing Start Date"));
                            else
                                errorProviderTaxGroup.SetError(dtpStartDateTaxGroup, string.Empty);
                        }
                    }
                }
            }
        }

        void ValidateStatusTaxGroup(Boolean yesNo )
        {
            if (Convert.ToInt32(cmbstatusTaxGroup.SelectedValue) == Common.INT_DBNULL && yesNo == false)
                errorProviderTaxGroup.SetError(cmbstatusTaxGroup, Common.GetMessage("VAL0002", lblStatusTaxGroup.Text.Trim().Substring(0, lblStatusTaxGroup.Text.Trim().Length - 2)));
            else
                errorProviderTaxGroup.SetError(cmbstatusTaxGroup, string.Empty);
        }               

        #endregion         
                 
        #endregion      
        
    }
}
