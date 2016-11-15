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
using CoreComponent.MasterData.BusinessObjects;

namespace CoreComponent.MasterData.UI
{
    public partial class frmCompositeItemsList : Form
    {
        #region Variables

        private DataTable m_dtItems;
        private List<CompositeItem> m_compItemsList = null;
        private List<CompositeItem> m_originalCompositeList = null;

        private DateTime m_modifiedDate = Common.DATETIME_NULL;
        private Boolean m_isModified = false;
        private Int32 m_compositeItemId = Common.INT_DBNULL;
        private String m_itemCode = String.Empty;
        private String m_CurrentItemCode= String.Empty;


        public List<CompositeItem> OriginalCompositeList
        {
            get { return m_originalCompositeList; }
            set { m_originalCompositeList = value; }
        }

        #endregion

        #region Properties
        public List<CompositeItem> SelectedCompositeItems
        {
            get { return m_compItemsList; }
            set { m_compItemsList = value; }
        }
        #endregion

        #region CTors
        public frmCompositeItemsList(string currentItemCode)
        {
            InitializeComponent();
            InitializeControls();
            m_compItemsList = new List<CompositeItem>();
            btnAdd.Enabled = true;
            m_CurrentItemCode = currentItemCode;
        }

        public frmCompositeItemsList(List<CompositeItem> selectedCompItem)
        {
            InitializeComponent();

            InitializeControls();

            NullifyGrid();
            m_compItemsList = selectedCompItem;

            m_originalCompositeList= new List<CompositeItem>();
            CopyLists(m_compItemsList, m_originalCompositeList);
            BindGrid();
            btnAdd.Enabled = true;
        }

        public frmCompositeItemsList(List<CompositeItem> selectedCompItem, bool isItemSelected)
        {
            InitializeComponent();

            InitializeControls();

            NullifyGrid();
            m_compItemsList = selectedCompItem;

            m_originalCompositeList = new List<CompositeItem>();
            CopyLists(m_compItemsList, m_originalCompositeList);
            BindGrid();
            if (isItemSelected)
            {                
                btnAdd.Enabled = false;
            }
        } 

        #endregion

        #region Initializing functions

        private void BuildDataGridView()
        {
            DataGridView dgv = Common.GetDataGridViewColumns(dgvCompositeItems, Environment.CurrentDirectory + "\\APP_DATA\\GridViewDefinition.xml");
            dgvCompositeItems.AutoGenerateColumns = false;
            dgvCompositeItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCompositeItems.AllowUserToAddRows = false;
            dgvCompositeItems.AllowUserToDeleteRows = false;
            dgvCompositeItems.RowHeadersVisible = false;
        }

        private void InitializeControls()
        {
            //Fill Items Combobox
            //DataTable datatableItems = Common.ParameterLookup(Common.ParameterType.Item, new ParameterFilter("ITEM", 0, 0, 0));
            //cmbItems.DataSource = datatableItems;
            //cmbItems.ValueMember = "ItemId";
            //cmbItems.DisplayMember = "ItemName";

            txtItem.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtItem.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            AutoCompleteStringCollection coll = new AutoCompleteStringCollection();
            m_dtItems = Common.ParameterLookup(Common.ParameterType.ItemCode, new ParameterFilter(string.Empty, 0, 0, 0));
            foreach (DataRow dr in m_dtItems.Rows)
            {
                txtItem.AutoCompleteCustomSource.Add(dr[0].ToString());
            }

            //Fill Status Combobox
            DataTable datatableUOM = Common.ParameterLookup(Common.ParameterType.UOM, new ParameterFilter(string.Empty, 0, 0, 0));
            cmbUOM.DataSource = datatableUOM;
            cmbUOM.ValueMember = ItemUOMDetails.UOM_VALUE_MEM;
            cmbUOM.DisplayMember = ItemUOMDetails.UOM_TEXT_MEM;

            m_compItemsList = new List<CompositeItem>();
            m_originalCompositeList = new List<CompositeItem>();


            BuildDataGridView();

            ResetControls();
        } 
        
        #endregion

        #region Events

        private void frmCompositeItemsList_Load(object sender, EventArgs e)
        {

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            //if (m_compItemsList.Count <= 1)
            {
               // MessageBox.Show(Common.GetMessage("VAL0043"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (m_compItemsList.FindAll(delegate(CompositeItem c) { return c.IsTradable == true; }).Count <= 0)
            {
                MessageBox.Show(Common.GetMessage("VAL0044"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                m_originalCompositeList = new List<CompositeItem>();
                CopyLists(m_compItemsList, m_originalCompositeList);
                DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void txtItem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBoxValidations(txtItem, lblItem, false, m_dtItems);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtQuantity_Validated(object sender, EventArgs e)
        {
            try
            {
                TextBoxValidations(txtQuantity, lblQuantity, true);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbUOM_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboBoxValidations(cmbUOM, lblUOM);
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
                string errMessage = InvokeValidations();
                if (errMessage.Length > 0)
                {
                    MessageBox.Show(errMessage, Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
               
                CompositeItem ci = m_compItemsList.Find(delegate(CompositeItem c) { return c.ItemCode == txtItem.Text.Trim(); });
                if (ci == null)
                {
                    AddRecords();
                }
                else
                {
                    ci.Quantity = Convert.ToInt32(txtQuantity.Text);
                    ci.UOMId = Convert.ToInt32(cmbUOM.SelectedValue);
                    ci.UOMName = cmbUOM.Text;
                    BindGrid();
                }
                //if (!m_isModified)
                //    AddRecords();
                //else
                //    AddRecords(m_itemCode, m_compositeItemId);
                //ResetControls();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvCompositeItems_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
                {
                    if (dgvCompositeItems.Columns[e.ColumnIndex].CellType == typeof(DataGridViewImageCell))
                    {
                        Int32 compItemId = Convert.ToInt32(dgvCompositeItems.Rows[e.RowIndex].Cells[CompositeItem.GRID_COMPITEMID].Value);

                        if (compItemId != Common.INT_DBNULL)
                        {
                            MessageBox.Show(Common.GetMessage("VAL0010", "Item"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        DialogResult result = MessageBox.Show(Common.GetMessage("INF0028"), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            RemoveRecord(dgvCompositeItems.Rows[e.RowIndex].Cells[CompositeItem.GRID_ITEMCODE].Value.ToString(), m_isModified);
                        }
                        return;
                    }

                    //if (dgvCompositeItems.Columns[e.ColumnIndex].IsDataBound == true && dgvCompositeItems.Columns[e.ColumnIndex].CellType == typeof(DataGridViewTextBoxCell))
                    //{
                    //    SelectRecord(e);
                    //}
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


        private void CopyLists(List<CompositeItem> fromList, List<CompositeItem> toList)
        {
            foreach (CompositeItem ci in fromList)
            {
                //m_originalItemUOMList = new List<ItemUOMDetails>();
                CompositeItem newCI = new CompositeItem();
                newCI.ItemCode = ci.ItemCode;
                newCI.IsTradable = ci.IsTradable;
                newCI.CompositeItemId = ci.CompositeItemId;
                newCI.ModifiedDate = ci.ModifiedDate;
                newCI.Quantity = ci.Quantity;
                newCI.UOMId = ci.UOMId;
                newCI.UOMName = ci.UOMName;
                toList.Add(newCI);
            }
        }


        void SelectRecord(DataGridView dgv)
        {
            try
            {
                CompositeItem citem = (from c in m_compItemsList where c.ItemCode == dgv.CurrentRow.Cells[CompositeItem.GRID_ITEMCODE].Value.ToString() select c).FirstOrDefault();

                if (citem != null)
                {
                    txtItem.Text = dgvCompositeItems.CurrentRow.Cells[CompositeItem.GRID_ITEMCODE].Value.ToString();
                    txtQuantity.Text = dgvCompositeItems.CurrentRow.Cells[CompositeItem.GRID_QTY].Value.ToString();
                    cmbUOM.SelectedValue = Convert.ToInt32(dgvCompositeItems.CurrentRow.Cells[CompositeItem.GRID_UOMID].Value);

                    m_isModified = true;
                    m_compositeItemId = citem.CompositeItemId;
                    m_itemCode = citem.ItemCode;
                    m_modifiedDate = citem.ModifiedDate;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void SelectRecord(DataGridViewCellMouseEventArgs e)
        {
            try
            {
                CompositeItem citem = (from c in m_compItemsList where c.ItemCode == dgvCompositeItems.Rows[e.RowIndex].Cells[CompositeItem.GRID_ITEMCODE].Value.ToString() select c).FirstOrDefault();

                if (citem != null)
                {
                    txtItem.Text = dgvCompositeItems.Rows[e.RowIndex].Cells[CompositeItem.GRID_ITEMCODE].Value.ToString();
                    txtQuantity.Text = dgvCompositeItems.Rows[e.RowIndex].Cells[CompositeItem.GRID_QTY].Value.ToString();
                    cmbUOM.SelectedValue = Convert.ToInt32(dgvCompositeItems.Rows[e.RowIndex].Cells[CompositeItem.GRID_UOMID].Value);

                    m_isModified = true;
                    m_compositeItemId = citem.CompositeItemId;
                    m_itemCode = citem.ItemCode;
                    m_modifiedDate = citem.ModifiedDate;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void RemoveRecord(String itemCode, Boolean isModified)
        {
            try
            {
                NullifyGrid();

                CompositeItem citem = (from c in m_compItemsList where c.ItemCode == itemCode select c).FirstOrDefault();

                if (citem != null)
                    m_compItemsList.Remove(citem);

                BindGrid();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Nullify and Bind Grid
        void NullifyGrid()
        {
            //dgvCompositeItems.DataSource = null;
        }

        void BindGrid()
        {
            dgvCompositeItems.DataSource = null;
            if (m_compItemsList.Count > 0)
            {
                dgvCompositeItems.DataSource = m_compItemsList;
                dgvCompositeItems.ClearSelection();
                ResetControls();
                txtItem.Focus();
            }
        }

        void BindGrid(List<CompositeItem> list)
        {
            dgvCompositeItems.DataSource = null;
            if (list.Count > 0)
            {
                dgvCompositeItems.DataSource = list;
                dgvCompositeItems.ClearSelection();
                ResetControls();
                txtItem.Focus();
            }
        }

        #endregion

        void ResetControls()
        {
            try
            {
                txtItem.Text = string.Empty;
                txtQuantity.Text = string.Empty;
                cmbUOM.SelectedIndex = 0;

                Validators.SetErrorMessage(epCompositeItems, txtItem);
                Validators.SetErrorMessage(epCompositeItems, txtQuantity);
                Validators.SetErrorMessage(epCompositeItems, cmbUOM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void AddRecords(String modifiedItemCode, Int32 modifiedCompId)
        {
            CompositeItem citem = null;
            if (Convert.ToInt32(txtItem.Text) != Common.INT_DBNULL && txtItem.Text == modifiedItemCode)
            {
                citem = CreateNewCompositeItem(modifiedItemCode, modifiedCompId);
            }

            RemoveRecord(modifiedItemCode, m_isModified);
            NullifyGrid();
            m_compItemsList.Add(citem);
            BindGrid();

            m_isModified = false;
            m_itemCode = string.Empty;
            m_compositeItemId = Common.INT_DBNULL;
            m_modifiedDate = Common.DATETIME_NULL;
        }

        void AddRecords()
        {
            try
            {
                CompositeItem citem = null;
                //if (!Validators.CheckForEmptyString(txtItem.Text.Length))
                //{
                //    if (CheckIfRecordExists(txtItem.Text))
                //    {
                //        MessageBox.Show(Common.GetMessage("VAL0007", "Record"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                //        return;
                //    }

                citem = CreateNewCompositeItem(txtItem.Text, Common.INT_DBNULL);
                //}
                NullifyGrid();
                m_compItemsList.Add(citem);
                BindGrid();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        CompositeItem CreateNewCompositeItem(String modifiedItemCode, Int32 modifiedCompId)
        {
            CompositeItem citem = new CompositeItem();
            citem.ItemCode = modifiedItemCode;
            DataRow dr = m_dtItems.Select("ItemCode = '" + modifiedItemCode + "'")[0];
            citem.ItemId = Convert.ToInt32(dr[1]);
            citem.IsTradable = Convert.ToBoolean(dr[3]);
            citem.CompositeItemId = modifiedCompId;
            citem.ModifiedDate = m_modifiedDate;
            citem.Quantity = Convert.ToInt32(txtQuantity.Text);
            citem.UOMId = Convert.ToInt32(cmbUOM.SelectedValue);
            citem.UOMName = cmbUOM.Text;
            return citem;
        }

        Boolean CheckIfRecordExists(String itemCode)
        {
            Int32 count = (from c in m_compItemsList where c.ItemCode == itemCode select c).Count();

            if (count > 0) return true;
            return false;
        }

        #endregion

        #region Validation functions

        private void TextBoxValidations(TextBox currentTextBox, Label associatedLabel, bool isNumeric, DataTable checkSource)
        {
            try
            {
                if (Validators.CheckForEmptyString(currentTextBox.Text.Length))
                {
                    Validators.SetErrorMessage(epCompositeItems, currentTextBox, "VAL0001", associatedLabel.Text);
                }
                else if (isNumeric == true & Validators.IsInt32(currentTextBox.Text) == false)
                {
                    Validators.SetErrorMessage(epCompositeItems, currentTextBox, "VAL0001", associatedLabel.Text);
                }
                else if (!Validators.CheckForListMatch(currentTextBox.Text.Trim(), checkSource))
                {
                    Validators.SetErrorMessage(epCompositeItems, currentTextBox, "VAL0001", associatedLabel.Text);
                }
                else
                    Validators.SetErrorMessage(epCompositeItems, currentTextBox);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void TextBoxValidations(TextBox currentTextBox, Label associatedLabel, bool isNumeric)
        {
            try
            {
                if (Validators.CheckForEmptyString(currentTextBox.Text.Length))
                {
                    Validators.SetErrorMessage(epCompositeItems, currentTextBox, "VAL0001", associatedLabel.Text);
                }
                else if (isNumeric == true & Validators.IsInt32(currentTextBox.Text) == false)
                {
                    Validators.SetErrorMessage(epCompositeItems, currentTextBox, "VAL0001", associatedLabel.Text);
                }
                else
                    Validators.SetErrorMessage(epCompositeItems, currentTextBox);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ComboBoxValidations(ComboBox currentComboBox, Label associatedLabel)
        {
            try
            {
                if (Validators.CheckForSelectedValue(currentComboBox.SelectedIndex))
                {
                    Validators.SetErrorMessage(epCompositeItems, currentComboBox, "VAL0002", associatedLabel.Text);
                }
                else
                {
                    Validators.SetErrorMessage(epCompositeItems, currentComboBox);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string InvokeValidations()
        {
            ComboBoxValidations(cmbUOM, lblUOM);
            TextBoxValidations(txtQuantity, lblQuantity, true);
            TextBoxValidations(txtItem, lblItem, false, m_dtItems);
            if ((m_CurrentItemCode.Trim().Length > 0) && (txtItem.Text.Trim().ToLower() == m_CurrentItemCode.Trim().ToLower()))
            {
                epCompositeItems.SetError(txtItem, Common.GetMessage("VAL0129"));
            }
            StringBuilder sbError = new StringBuilder();
            sbError.Append(epCompositeItems.GetError(txtItem));
            sbError.AppendLine();
            sbError.Append(epCompositeItems.GetError(txtQuantity));
            sbError.AppendLine();
            sbError.Append(epCompositeItems.GetError(cmbUOM));
            sbError.AppendLine();
            sbError = Common.ReturnErrorMessage(sbError);
            return sbError.ToString();
        } 

        #endregion

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            m_compItemsList = new List<CompositeItem>();
            CopyLists(m_originalCompositeList, m_compItemsList);
            BindGrid(m_originalCompositeList);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetControls();
        }

        private void dgvCompositeItems_SelectionChanged(object sender, EventArgs e)
        {
            SelectRecord(sender as DataGridView);
        }
    }
}
