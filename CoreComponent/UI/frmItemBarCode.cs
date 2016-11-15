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


namespace CoreComponent.UI
{
    public partial class frmItemBarCode : Form
    {
        #region Variable Declaration

        private int m_itemId = Common.INT_DBNULL;
        private int m_rowIndex = Common.INT_DBNULL;
        private List<ItemBarCode> m_itemBarCodeList;        
        ItemBarCode m_itemBarCodeObj = null;                
        Boolean m_statusSelection = false;
        Boolean m_rowSelection = false;
        Boolean m_barCodeState = false;        

        #endregion

        #region Constructor

        public frmItemBarCode()
        {
            InitializeComponent();
            InitializeControls();
        }
        
        public frmItemBarCode(int itemId, List<ItemBarCode> itemBarCodeList)
        {
            if (itemBarCodeList.Count == 0)
                m_itemBarCodeList = new List<ItemBarCode>();
            else
                m_itemBarCodeList = itemBarCodeList;
            InitializeComponent();
            InitializeControls();
            m_itemId = itemId;                        
        }

        #endregion

        #region Property Declaration
        public List<ItemBarCode> ItemBarCodeList
        {
            get { return m_itemBarCodeList; }
            set { m_itemBarCodeList = value; }
        }

        #endregion

        #region Events

        private void frmItemBarCode_Load(object sender, EventArgs e)
        {
            try
            {
                dtpStartsOn.Format = DateTimePickerFormat.Custom;
                dtpStartsOn.CustomFormat = Common.DTP_DATE_FORMAT;
                dgvItemBarCode = Common.GetDataGridViewColumns(dgvItemBarCode, Environment.CurrentDirectory + "\\App_Data\\GridViewDefinition.xml");
                SearchBarCode();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult = DialogResult.Cancel;
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult = DialogResult.OK;
                this.Close();
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
                txtBarCode.Focus();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtBarCode_Validated(object sender, EventArgs e)
        {
            try
            {
                ValidateItemBarCodeTextBox(true);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateAll(false))
                {
                    AddToGrid();
                    txtBarCode.Focus();
                }
                else 
                {
                    txtBarCode.Text = "";
                    txtBarCode.Focus();
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dtpStartsOn_Validated(object sender, EventArgs e)
        {
            try
            {
                ValidateStartsOnDTP(true);
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
                if(m_statusSelection == true)
                    ValidateStatusComboBox(true);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvItemBarCode_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (m_rowSelection == true)
                {
                    if (e.RowIndex > -1 && dgvItemBarCode.Columns[e.ColumnIndex].CellType == typeof(DataGridViewImageCell))
                    {
                        int i = e.RowIndex;
                        DeleteItemBarCode(i);
                    }
                    if (e.RowIndex > -1 && dgvItemBarCode.Columns[e.ColumnIndex].CellType != typeof(DataGridViewImageCell))
                    {
                        int i = e.RowIndex;
                        EditItemBarCode(i);
                    }
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvItemBarCode_SelectionChanged(object sender, EventArgs e)
        {
            try
            {                
                    if (m_rowSelection == true)
                    {

                        EditItemBarCode(dgvItemBarCode.SelectedRows[0].Index);
                    }
                
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
        /// Initialize Controls on form
        /// </summary>
        private void InitializeControls()
        {
            try
            {                
                // to add items in Status ComboBox
                m_statusSelection = false;
                DataTable dtStatus = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter("STATUS", 0, 0, 0));
                cmbStatus.DataSource = dtStatus;
                cmbStatus.DisplayMember = Common.KEYVALUE1;
                cmbStatus.ValueMember = Common.KEYCODE1;
                m_statusSelection = true;
                if (cmbStatus.Items.Count > 0)
                    cmbStatus.SelectedValue = 1;
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// if itemBarCodeList is empty then fetch records from DB
        /// </summary>
        private void SearchBarCode()
        {
            if(m_itemBarCodeList.Count == 0)
            {
                m_itemBarCodeObj = new ItemBarCode();
                string errorMsg = string.Empty;
                m_itemBarCodeList = m_itemBarCodeObj.SearchItemBarCode(m_itemId, ref errorMsg);
                m_itemBarCodeObj = null;
            }
            BindGrid();
        }

        /// <summary>
        /// Bind List to DataGridView
        /// </summary>
        private void BindGrid()
        {
            if (m_itemBarCodeList.Count > 0)
            {
                m_rowSelection = false;
                dgvItemBarCode.DataSource = new List<ItemBarCode>();
                dgvItemBarCode.DataSource = m_itemBarCodeList;
                dgvItemBarCode.ClearSelection();
                m_rowSelection = true;
            }
            else
            {
                m_rowSelection = false;
                dgvItemBarCode.DataSource = null;
                m_rowSelection = true;
            }

        }

        /// <summary>
        /// Add Barcode data to m_itemBarCodeList then bind with DataGridView
        /// </summary>
        private void AddToGrid()
        {
                m_itemBarCodeObj = new ItemBarCode();
                m_itemBarCodeObj.ItemId = m_itemId;
                m_itemBarCodeObj.ItemBarCodeVal = txtBarCode.Text;
                m_itemBarCodeObj.StartsOn = dtpStartsOn.Value.ToShortDateString();
                m_itemBarCodeObj.Status = Convert.ToInt32(cmbStatus.SelectedValue);
                m_itemBarCodeObj.StatusText = cmbStatus.Text;
                m_itemBarCodeObj.BarcodeState = m_barCodeState;
                
                var queryBarCode = (from p in m_itemBarCodeList where p.ItemBarCodeVal == txtBarCode.Text select p);
                var queryStartDate = (from q in m_itemBarCodeList where Convert.ToDateTime(q.StartsOn) > Convert.ToDateTime(dtpStartsOn.Value.ToShortDateString()) select q);

                if (m_rowIndex != -1 && ((m_barCodeState == true)||(m_barCodeState == false && Convert.ToInt32(cmbStatus.SelectedValue) != 2)))
                {
                   m_itemBarCodeList[m_rowIndex] = m_itemBarCodeObj;
                   ResetControls();
                }
                else if (queryBarCode.Count<ItemBarCode>() == 0 && queryStartDate.Count<ItemBarCode>() == 0 && m_rowIndex == -1 && Convert.ToInt32(cmbStatus.SelectedValue)!=2)
                {
                    m_itemBarCodeList.Add(m_itemBarCodeObj);
                    ResetControls();
                }
                else
                {
                    if(Convert.ToInt32(cmbStatus.SelectedValue)==2)
                        MessageBox.Show(Common.GetMessage("VAL0006",lblStatus.Text.Substring(0,lblStatus.Text.Length - 2)),Common.GetMessage("10001"),MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    else if (queryBarCode.Count<ItemBarCode>() != 0 && queryStartDate.Count<ItemBarCode>() == 0)
                        MessageBox.Show(Common.GetMessage("INF0094",lblBarCode.Text.Substring(0,lblBarCode.Text.Length - 2)),Common.GetMessage("10001"),MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    else if (queryBarCode.Count<ItemBarCode>() == 0 && queryStartDate.Count<ItemBarCode>() != 0)
                        MessageBox.Show(Common.GetMessage("INF0096", lblStartsOn.Text.Substring(0, lblStartsOn.Text.Length - 2) + " Date"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else
                        MessageBox.Show(Common.GetMessage("INF0094", lblBarCode.Text.Substring(0, lblBarCode.Text.Length - 2)) + " & " + Common.GetMessage("INF0096", lblStartsOn.Text.Substring(0, lblStartsOn.Text.Length - 2) + " Date"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                BindGrid();
                m_itemBarCodeObj = null;
        }

        /// <summary>
        /// Switch to Edit Mode for selected Itembarcode
        /// </summary>
        /// <param name="index"></param>
        private void EditItemBarCode(int index)
        {
            m_itemBarCodeObj = new ItemBarCode();
            m_itemBarCodeObj = m_itemBarCodeList[index];
            m_barCodeState = m_itemBarCodeObj.BarcodeState;
            txtBarCode.Enabled = false;
            dtpStartsOn.Enabled = false;
            //btnAdd.Text = "Update";
            txtBarCode.Text = m_itemBarCodeObj.ItemBarCodeVal;
            dtpStartsOn.Value = Convert.ToDateTime(m_itemBarCodeObj.StartsOn);
            dtpStartsOn.Checked = true;
            cmbStatus.SelectedValue = m_itemBarCodeObj.Status;
            m_rowIndex = index;
            m_itemBarCodeObj = null;
        }

        /// <summary>
        /// Delete selected Record if its Barcodestate is false i.e. barcode isnot saved.
        /// </summary>
        /// <param name="index"></param>
        private void DeleteItemBarCode(int index)
        {
            DialogResult dr = MessageBox.Show(Common.GetMessage("5012"), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                m_itemBarCodeObj = new ItemBarCode();
                m_itemBarCodeObj = m_itemBarCodeList[index];
                if (m_itemBarCodeObj.BarcodeState == true)
                {
                    MessageBox.Show(Common.GetMessage("VAL0073", lblBarCode.Text.Substring(0, lblBarCode.Text.Length - 2)), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    m_itemBarCodeList.RemoveAt(index);
                    BindGrid();
                }
                ResetControls();
                m_itemBarCodeObj = null;
            }
        }
       
        /// <summary>
        /// Reset All Controls on form
        /// </summary>
        private void ResetControls()
        {
            txtBarCode.Enabled = true;
            dtpStartsOn.Enabled = true;
            txtBarCode.Text = string.Empty;
            dtpStartsOn.Value = DateTime.Today;
            dtpStartsOn.Checked = false;
//            cmbStatus.SelectedValue = Common.INT_DBNULL;
            if (cmbStatus.Items.Count > 0)
                cmbStatus.SelectedValue = 1;
            m_rowSelection = false;
            btnAdd.Text = "Add";
            dgvItemBarCode.ClearSelection();
            m_rowSelection = true;
            m_barCodeState = false;
            m_rowIndex = -1;
            m_itemBarCodeObj = null;
            Validators.SetErrorMessage(errorProviderItemBarCode, txtBarCode);
            Validators.SetErrorMessage(errorProviderItemBarCode, dtpStartsOn);
            Validators.SetErrorMessage(errorProviderItemBarCode, cmbStatus);
        }

        /// <summary>
        /// Validate All controls
        /// </summary>
        /// <param name="yesNo"></param>
        /// <returns></returns>
        private Boolean ValidateAll(Boolean yesNo)
        {
            Boolean ret = true;
            StringBuilder errorMessage = new StringBuilder();
            ValidateItemBarCodeTextBox(yesNo);
            ValidateStartsOnDTP(yesNo);
            ValidateStatusComboBox(yesNo);
            
            if (Validators.GetErrorMessage(errorProviderItemBarCode, txtBarCode).Trim().Length > 0)
            {
                ret = false;
                errorMessage.Append(Validators.GetErrorMessage(errorProviderItemBarCode, txtBarCode));
                errorMessage.AppendLine();
            }
            if (Validators.GetErrorMessage(errorProviderItemBarCode, dtpStartsOn).Trim().Length > 0)
            {
                ret = false;
                errorMessage.Append(Validators.GetErrorMessage(errorProviderItemBarCode, dtpStartsOn));
                errorMessage.AppendLine();
            }
            if (Validators.GetErrorMessage(errorProviderItemBarCode, cmbStatus).Trim().Length > 0)
            {
                ret = false;
                errorMessage.Append(Validators.GetErrorMessage(errorProviderItemBarCode, cmbStatus));
                errorMessage.AppendLine();
            }

            if (ret == false)
            {
                errorMessage = Common.ReturnErrorMessage(errorMessage);
                MessageBox.Show(errorMessage.ToString(),Common.GetMessage("10001"),MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            
            return ret;

        }

        /// <summary>
        /// Validate ItemBarcode TextBox
        /// </summary>
        /// <param name="yesNo"></param>
        private void ValidateItemBarCodeTextBox(Boolean yesNo)
        {
            if (txtBarCode.Text.Trim().Length == 0 && yesNo == false)
                Validators.SetErrorMessage(errorProviderItemBarCode, txtBarCode, "VAL0001", lblBarCode.Text.Trim().Substring(0, lblBarCode.Text.Trim().Length));
            else if (txtBarCode.Text.Trim().Length > 0 && yesNo == false)
                errorProviderItemBarCode.SetError(txtBarCode, Common.CodeValidate(txtBarCode.Text, lblBarCode.Text.Substring(0, lblBarCode.Text.Length - 2)));
            else
                Validators.SetErrorMessage(errorProviderItemBarCode, txtBarCode);
        }

        /// <summary>
        /// Validate StartsOn DTP
        /// </summary>
        /// <param name="yesNo"></param>
        private void ValidateStartsOnDTP(Boolean yesNo)
        {
            if (dtpStartsOn.Checked == false && yesNo == false)
               Validators.SetErrorMessage(errorProviderItemBarCode, dtpStartsOn, "VAL0002",lblStartsOn.Text.Trim());
            else
                Validators.SetErrorMessage(errorProviderItemBarCode, dtpStartsOn);
        }

        /// <summary>
        /// Validate Status Combobox
        /// </summary>
        /// <param name="yesNo"></param>
        private void ValidateStatusComboBox(Boolean yesNo)
        {
            if (Convert.ToInt32(cmbStatus.SelectedValue) == -1 && yesNo == false)
                Validators.SetErrorMessage(errorProviderItemBarCode, cmbStatus, "VAL0002", lblStatus.Text.Trim());
            else
                Validators.SetErrorMessage(errorProviderItemBarCode, cmbStatus);
        }

        #endregion                        
            
    }
}
