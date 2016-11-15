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

namespace CoreComponent.UI
{
    public partial class frmUOM : Form
    {
        #region Variables
        private List<ItemUOMDetails> m_itemUOMList = null;
        private List<ItemUOMDetails> m_originalItemUOMList = null;

        private Boolean m_isModified = false;
        private Int32 m_modifiedItemUOMId = Common.INT_DBNULL;
        private Int32 m_modifiedItemId = Common.INT_DBNULL;
        private Int32 m_modifiedUOMId = Common.INT_DBNULL;
        #endregion

        #region Constructors
        public frmUOM()
        {
            InitializeComponent();
            InitializeControls();
        }

        public frmUOM(List<ItemUOMDetails> selectedUOMs)
        {
            InitializeComponent();
            InitializeControls();

            NullifyGrid();
            m_itemUOMList = selectedUOMs;
            if (m_itemUOMList == null) m_itemUOMList = new List<ItemUOMDetails>();
            m_originalItemUOMList = new List<ItemUOMDetails>();
            CopyLists(m_itemUOMList, m_originalItemUOMList);
            BindGrid();
        }

        #endregion

        #region Properties


        public List<ItemUOMDetails> OriginalItemUOMList
        {
            get { return m_originalItemUOMList; }
            set { m_originalItemUOMList = value; }
        }

        public List<ItemUOMDetails> SelectedUOMs
        {
            get { return m_itemUOMList; }
            set { m_itemUOMList = value; }
        }
        #endregion

        #region Events
        private void frmUOM_Load(object sender, EventArgs e)
        {
            //CopyLists(m_itemUOMList, m_originalItemUOMList);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                CallValidations();

                String errMessage = GetErrorMessages();
                if (errMessage.Length > 0)
                {
                    MessageBox.Show(errMessage, Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                if (m_itemUOMList != null && m_itemUOMList.Count > 0)
                {
                    //Check if Primary Purchase already added
                    if (Convert.ToInt32(cmbTOM.SelectedValue) == 1 && chkPrimary.Checked)
                    {
                        Int32 primPurch = (from u in m_itemUOMList where u.IsPrimary == true && u.TOMId == 1 select u).Count();

                        if (primPurch == 1)
                        {
                            MessageBox.Show(Common.GetMessage("VAL0036"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                    //Check if Primary Sell already added
                    else if (Convert.ToInt32(cmbTOM.SelectedValue) == 2 && chkPrimary.Checked)
                    {
                        Int32 primSell = (from u in m_itemUOMList where u.IsPrimary == true && u.TOMId == 2 select u).Count();

                        if (primSell == 1)
                        {
                            MessageBox.Show(Common.GetMessage("VAL0037"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                }

                if (CheckIfRecordExists(m_modifiedItemUOMId, Convert.ToInt32(cmbUOM.SelectedValue), Convert.ToInt32(cmbTOM.SelectedValue), chkPrimary.Checked))
                {
                    IEnumerable<ItemUOMDetails> uom = (from u in m_itemUOMList where (u.UOMId == Convert.ToInt32(cmbUOM.SelectedValue) /*&& u.IsPrimary == isPrim*/ && Convert.ToInt32(cmbTOM.SelectedValue) == u.TOMId) || (u.ItemUOMId == m_modifiedItemUOMId && u.UOMId == Convert.ToInt32(cmbUOM.SelectedValue) /*&& u.IsPrimary == isPrim*/ && Convert.ToInt32(cmbTOM.SelectedValue) == u.TOMId) select u);
                    uom.ElementAt<ItemUOMDetails>(0).IsPrimary = chkPrimary.Checked;
                    BindGrid();
                    //MessageBox.Show(Common.GetMessage("VAL0007", "Record"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                //if (!m_isModified)
                AddRecords();
                //else
                //AddRecords(m_modifiedItemUOMId, m_modifiedItemId);

                //ResetControls();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_itemUOMList != null && m_itemUOMList.Count > 0)
                {
                    Int32 primCount = Common.INT_DBNULL;

                    primCount = (from u in m_itemUOMList where u.IsPrimary == true && u.TOMId == 1 select u).Count();
                    if (primCount <= 0)
                    {
                        MessageBox.Show(Common.GetMessage("VAL0035"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (primCount > 1)
                    {
                        MessageBox.Show(Common.GetMessage("VAL0036"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    primCount = (from u in m_itemUOMList where u.TOMId == 2 select u).Count();
                    if (primCount > 0)
                    {
                        primCount = (from u in m_itemUOMList where u.IsPrimary == true && u.TOMId == 2 select u).Count();
                        if (primCount <= 0)
                        {
                            MessageBox.Show(Common.GetMessage("VAL0041"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        else if (primCount > 1)
                        {
                            MessageBox.Show(Common.GetMessage("VAL0037"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }

                }
                m_originalItemUOMList = new List<ItemUOMDetails>();
                CopyLists(m_itemUOMList, m_originalItemUOMList);
                DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvUOM_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    if (dgvUOM.Columns[e.ColumnIndex].CellType == typeof(DataGridViewImageCell))
                    {
                        Int32 itemUOMId = Convert.ToInt32(dgvUOM.Rows[e.RowIndex].Cells[ItemUOMDetails.GRID_ITEMUOM_COL].Value);

                        if (itemUOMId != Common.INT_DBNULL)
                        {
                            MessageBox.Show(Common.GetMessage("VAL0010", "UOM"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            return;
                        }

                        DialogResult result = MessageBox.Show(Common.GetMessage("INF0028"), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            ItemUOMDetails uomDet = new ItemUOMDetails();
                            uomDet.ItemUOMId = Common.INT_DBNULL;
                            uomDet.ItemId = Convert.ToInt32(dgvUOM.Rows[e.RowIndex].Cells[ItemUOMDetails.GRID_ITEM_COL].Value);
                            uomDet.UOMId = Convert.ToInt32(dgvUOM.Rows[e.RowIndex].Cells[ItemUOMDetails.GRID_UOM_COL].Value);
                            uomDet.TOMId = Convert.ToInt32(dgvUOM.Rows[e.RowIndex].Cells[ItemUOMDetails.GRID_TOM_COL].Value);
                            uomDet.IsPrimary = Convert.ToBoolean(dgvUOM.Rows[e.RowIndex].Cells[ItemUOMDetails.GRID_PRIM_COL].Value);
                            RemoveRecord(uomDet, m_isModified);
                        }
                        return;
                    }

                    //if (dgvUOM.Columns[e.ColumnIndex].CellType == typeof(DataGridViewTextBoxCell) && dgvUOM.Columns[e.ColumnIndex].IsDataBound == true)
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

        private void dgvUOM_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                SelectRecord(e);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbTOM_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboBoxValidations(cmbTOM, lblTOM, epUOM);
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
                ComboBoxValidations(cmbUOM, lblUOM, epUOM);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvUOM_SelectionChanged(object sender, EventArgs e)
        {
            SelectRecord(sender as DataGridView);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            m_itemUOMList = new List<ItemUOMDetails>();
            CopyLists(m_originalItemUOMList, m_itemUOMList);
            BindGrid(m_originalItemUOMList);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetControls();
        }

        #endregion

        #region Methods
        void InitializeControls()
        {
            try
            {
                //Bind TOM combobox
                DataTable dataTableTOM = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter(Common.TOM, 0, 0, 0));
                cmbTOM.DataSource = dataTableTOM;
                cmbTOM.ValueMember = Common.KEYCODE1;
                cmbTOM.DisplayMember = Common.KEYVALUE1;

                //Bind UOM combobox
                DataTable dataTableUOM = Common.ParameterLookup(Common.ParameterType.UOM, new ParameterFilter(string.Empty, 0, 0, 0));
                cmbUOM.DataSource = dataTableUOM;
                cmbUOM.ValueMember = ItemUOMDetails.UOM_VALUE_MEM;
                cmbUOM.DisplayMember = ItemUOMDetails.UOM_TEXT_MEM;

                //Get Columns for DataGridView
                DataGridView dgv = Common.GetDataGridViewColumns(dgvUOM, Environment.CurrentDirectory + "\\App_Data\\GridViewDefinition.xml");
                dgvUOM.AutoGenerateColumns = false;
                dgvUOM.AllowUserToAddRows = false;
                dgvUOM.AllowUserToDeleteRows = false;
                dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvUOM.ReadOnly = true;

                m_itemUOMList = new List<ItemUOMDetails>();
                m_originalItemUOMList = new List<ItemUOMDetails>();
                ResetControls();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void SelectRecord(DataGridView dgv)
        {
            try
            {
                ItemUOMDetails selectedItemUOM = (from u in m_itemUOMList where u.ItemUOMId == Convert.ToInt32(dgv.CurrentRow.Cells[ItemUOMDetails.GRID_ITEMUOM_COL].Value) select u).FirstOrDefault();
                if (selectedItemUOM != null)
                {
                    cmbUOM.SelectedValue = (int)dgvUOM.CurrentRow.Cells["UOMId"].Value;
                    cmbTOM.SelectedValue = (int)dgvUOM.CurrentRow.Cells["TOMId"].Value;
                    chkPrimary.Checked = (bool)dgvUOM.CurrentRow.Cells["IsPrimary"].Value;

                    if (selectedItemUOM.ItemUOMId != Common.INT_DBNULL)
                    {
                        m_isModified = true;
                        m_modifiedItemId = selectedItemUOM.ItemId;
                        m_modifiedUOMId = selectedItemUOM.UOMId;
                        m_modifiedItemUOMId = selectedItemUOM.ItemUOMId;
                    }
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
                ItemUOMDetails selectedItemUOM = (from u in m_itemUOMList where u.ItemUOMId == Convert.ToInt32(dgvUOM.Rows[e.RowIndex].Cells[ItemUOMDetails.GRID_ITEMUOM_COL].Value) select u).FirstOrDefault();

                cmbUOM.SelectedValue = (int)dgvUOM.Rows[e.RowIndex].Cells["UOMId"].Value;
                cmbTOM.SelectedValue = (int)dgvUOM.Rows[e.RowIndex].Cells["TOMId"].Value;
                chkPrimary.Checked = (bool)dgvUOM.Rows[e.RowIndex].Cells["IsPrimary"].Value;

                if (selectedItemUOM.ItemUOMId != Common.INT_DBNULL)
                {
                    m_isModified = true;
                    m_modifiedItemId = selectedItemUOM.ItemId;
                    m_modifiedUOMId = selectedItemUOM.UOMId;
                    m_modifiedItemUOMId = selectedItemUOM.ItemUOMId;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void RemoveRecord(Int32 itemUOMId, Boolean isModified)
        {
            NullifyGrid();
            ItemUOMDetails removeUOM = (from u in m_itemUOMList where u.ItemUOMId == itemUOMId select u).FirstOrDefault();

            if (removeUOM != null)
                m_itemUOMList.Remove(removeUOM);

            BindGrid();
        }

        void RemoveRecord(ItemUOMDetails uomDet, Boolean isModified)
        {
            NullifyGrid();
            ItemUOMDetails removeUOM = (from u in m_itemUOMList where (u.ItemUOMId == Common.INT_DBNULL && u.ItemId == uomDet.ItemId && u.UOMId == uomDet.UOMId && u.TOMId == uomDet.TOMId && u.IsPrimary == uomDet.IsPrimary) select u).FirstOrDefault();

            if (removeUOM != null)
                m_itemUOMList.Remove(removeUOM);

            BindGrid();
        }

        void ResetControls()
        {
            try
            {
                cmbUOM.SelectedIndex = 0;
                cmbTOM.SelectedIndex = 0;
                chkPrimary.CheckState = CheckState.Unchecked;

                Validators.SetErrorMessage(epUOM, cmbTOM);
                Validators.SetErrorMessage(epUOM, cmbUOM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void AddRecords()
        {
            try
            {
                AddRecords(Common.INT_DBNULL, Common.INT_DBNULL);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void AddRecords(Int32 modifiedItemUOMId, Int32 modifiedItemId)
        {
            try
            {
                ItemUOMDetails newUOM = null;

                newUOM = CreateNewItemUOM(modifiedItemUOMId, modifiedItemId, Convert.ToInt32(cmbUOM.SelectedValue), cmbUOM.Text);

                if (modifiedItemUOMId != Common.INT_DBNULL)
                    RemoveRecord(modifiedItemUOMId, m_isModified);
                NullifyGrid();
                m_itemUOMList.Add(newUOM);
                BindGrid();

                if (modifiedItemUOMId != Common.INT_DBNULL && modifiedItemId != Common.INT_DBNULL)
                {
                    m_isModified = false;
                    m_modifiedItemUOMId = Common.INT_DBNULL;
                    m_modifiedItemId = Common.INT_DBNULL;
                    m_modifiedUOMId = Common.INT_DBNULL;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        ItemUOMDetails CreateNewItemUOM(Int32 selItemUOMId, Int32 selItemId, Int32 selUOM, String selUOMName)
        {
            ItemUOMDetails newUOM = new ItemUOMDetails();
            newUOM.ItemUOMId = selItemUOMId;
            newUOM.ItemId = selItemId;
            newUOM.UOMId = selUOM;
            newUOM.UOMName = selUOMName;
            newUOM.IsPrimary = chkPrimary.Checked;
            newUOM.TOMId = Convert.ToInt32(cmbTOM.SelectedValue);
            newUOM.TOMName = cmbTOM.Text;

            return newUOM;
        }

        void NullifyGrid()
        {
            //dgvUOM.DataSource = null;
        }

        void BindGrid()
        {
            dgvUOM.DataSource = null;
            if (m_itemUOMList.Count > 0)
            {
                dgvUOM.DataSource = m_itemUOMList;
                dgvUOM.ClearSelection();
                ResetControls();
            }
        }

        void BindGrid(List<ItemUOMDetails> itemUoms)
        {
            dgvUOM.DataSource = null;
            if (m_itemUOMList.Count > 0)
            {
                dgvUOM.DataSource = itemUoms;
                dgvUOM.ClearSelection();
                ResetControls();
            }
        }


        Boolean CheckIfRecordExists(Int32 itemUOMId, Int32 uomId, Int32 tomId, Boolean isPrim)
        {
            Int32 count = (from u in m_itemUOMList where (u.UOMId == uomId /*&& u.IsPrimary == isPrim*/ && tomId == u.TOMId) || (u.ItemUOMId == itemUOMId && u.UOMId == uomId /*&& u.IsPrimary == isPrim*/ && tomId == u.TOMId) select u).Count();

            if (count > 0) return true;
            return false;
        }

        void CallValidations()
        {
            try
            {
                ComboBoxValidations(cmbUOM, lblUOM, epUOM);
                ComboBoxValidations(cmbTOM, lblTOM, epUOM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void ComboBoxValidations(ComboBox cmb, Label lbl, ErrorProvider ep)
        {
            try
            {
                if (Validators.CheckForSelectedValue(cmb.SelectedIndex))
                    Validators.SetErrorMessage(ep, cmb, "VAL0002", lbl.Text);
                else
                    Validators.SetErrorMessage(ep, cmb);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CopyLists(List<ItemUOMDetails> fromList, List<ItemUOMDetails> toList)
        {
            foreach (ItemUOMDetails iud in fromList)
            {
                //m_originalItemUOMList = new List<ItemUOMDetails>();
                ItemUOMDetails newUOM = new ItemUOMDetails();
                newUOM.ItemUOMId = iud.ItemUOMId;
                newUOM.ItemId = iud.ItemId;
                newUOM.UOMId = iud.UOMId;
                newUOM.UOMName = iud.UOMName;
                newUOM.IsPrimary = iud.IsPrimary;
                newUOM.TOMId = iud.TOMId;
                newUOM.TOMName = iud.TOMName;
                toList.Add(newUOM);
            }
        }

        String GetErrorMessages()
        {
            try
            {
                StringBuilder sbError = new StringBuilder();

                Validators.AppendToStringBuilder(Validators.GetErrorMessage(epUOM, cmbUOM), ref sbError);
                Validators.AppendToStringBuilder(Validators.GetErrorMessage(epUOM, cmbTOM), ref sbError);
                sbError = Common.ReturnErrorMessage(sbError);
                return sbError.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

    }
}
