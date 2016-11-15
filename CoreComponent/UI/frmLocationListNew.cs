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
    public partial class frmLocationListNew : Form
    {
        private List<LocationList> m_locationList;
        private Boolean m_isModified = false;
        private Int32 m_modifiedItemId = Common.INT_DBNULL;
        private Int32 m_modifiedLocationId = Common.INT_DBNULL;
        private DateTime m_modifiedDate = Common.DATETIME_NULL;
        private List<LocationList> m_originalLocationList;
        private int m_ItemId=Common.INT_DBNULL;

        public List<LocationList> OriginalLocationList
        {
            get { return m_originalLocationList; }
            set { m_originalLocationList = value; }
        }

        #region Constructors

        public frmLocationListNew(int itemId)
        {
            InitializeComponent();
            m_ItemId = itemId;
            InitializeControls();

            m_locationList = new List<LocationList>();
        }


        public frmLocationListNew()
        {
            InitializeComponent();

            InitializeControls();

            m_locationList = new List<LocationList>();
        }

        public frmLocationListNew(List<LocationList> selectedLocations, int itemId)
        {
            InitializeComponent();
            m_ItemId = itemId;
            InitializeControls();

            NullifyGrid();
            SelectedLocations = selectedLocations;
            m_originalLocationList = new List<LocationList>();
            CopyLists(selectedLocations, m_originalLocationList);
            if (m_originalLocationList != null && m_originalLocationList.Count > 0)
                m_ItemId = m_originalLocationList[0].ItemId;
            BindGrid();
        }
        #endregion

        private void CopyLists(List<LocationList> fromList, List<LocationList> toList)
        {
            foreach (LocationList ll in fromList)
            {
                //m_originalItemUOMList = new List<ItemUOMDetails>();
                LocationList newLocation = new LocationList();
                newLocation.ItemId = ll.ItemId;
                newLocation.LocationId = ll.LocationId;
                newLocation.LocationName = ll.LocationName;
                newLocation.LocationDisplayName = ll.LocationDisplayName;
                newLocation.ReorderLevel = ll.ReorderLevel;
                newLocation.Status = ll.Status;
                newLocation.StatusName = ll.StatusName;
                newLocation.ModifiedDate = ll.ModifiedDate;
                toList.Add(newLocation);
            }
        }

        void InitializeControls()
        {
            try
            {
                //Bind Locations Combobox
                DataTable dataTableLocations = Common.ParameterLookup(Common.ParameterType.Locations, new ParameterFilter(string.Empty, 0, 0, 0));
                cmbLocation.DataSource = dataTableLocations;
                cmbLocation.ValueMember = LocationList.LOC_VALUE_MEM;
                cmbLocation.DisplayMember = LocationList.LOC_TEXT_MEM;

                //Bind Status Combobox
                DataTable dataTableStatus = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter(Common.STATUS, 0, 0, 0));
                cmbStatus.DataSource = dataTableStatus;
                cmbStatus.ValueMember = Common.KEYCODE1;
                cmbStatus.DisplayMember = Common.KEYVALUE1;
                if (cmbStatus.Items.Count > 0)
                    cmbStatus.SelectedValue = 1;

                //Get Columns for DataGridview
                DataGridView dgv = Common.GetDataGridViewColumns(dgvItemLocation, Environment.CurrentDirectory + "\\App_Data\\GridViewDefinition.xml");
                dgvItemLocation.AutoGenerateColumns = false;
                dgvItemLocation.AllowUserToAddRows = false;
                dgvItemLocation.AllowUserToDeleteRows = false;
                dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvItemLocation.ReadOnly = true;

                m_locationList = new List<LocationList>();
                m_originalLocationList = new List<LocationList>();

                ResetValidator();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public List<LocationList> SelectedLocations
        {
            get { return m_locationList; }
            set { m_locationList = value; }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                //Validate Controls first
                txtROL_TextChanged(this, new EventArgs());
                if (string.IsNullOrEmpty(Validators.GetErrorMessage(epLocation, txtROL)))
                {
                    decimal rolNo = -9999;
                    if (Decimal.TryParse(txtROL.Text.Trim(), out rolNo))
                    {
                        if (rolNo == 0)
                        {
                            Validators.SetErrorMessage(epLocation, txtROL, "VAL0080", "Reorder Level", "1");
                        }
                       
                    }
                }
                if (string.IsNullOrEmpty(Validators.GetErrorMessage(epLocation, txtROL)))
                {
                        if(!Validators.IsValidQuantity(txtROL.Text.Trim()))
                        {
                            Validators.SetErrorMessage(epLocation, txtROL, "VAL0081", "Reorder Level");
                        }
                }
                cmbStatus_SelectedIndexChanged(this, new EventArgs());
                
                StringBuilder sbError = new StringBuilder();
                sbError.Append(Validators.GetErrorMessage(epLocation, txtROL));
                sbError.AppendLine(string.Empty);
                sbError.Append(Validators.GetErrorMessage(epLocation, cmbStatus));
                sbError = Common.ReturnErrorMessage(sbError);
                if (sbError.ToString().Trim().Length > 0)
                {                    
                    MessageBox.Show(sbError.ToString(),Common.GetMessage("10001"),MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    return;
                }

                LocationList loc = m_locationList.Find(delegate(LocationList ll) { return ll.LocationId == (int)cmbLocation.SelectedValue; });
                if ( loc == null)
                {
                    AddRecords();
                }
                else
                {
                    loc.ReorderLevel = Convert.ToDecimal(txtROL.Text.Trim());
                    loc.Status = (int)cmbStatus.SelectedValue;
                    loc.StatusName = cmbStatus.Text.Trim();
                    loc.ModifiedBy = AuthenticationComponent.BusinessObjects.Authenticate.LoggedInUser.UserId;

                    int rowIndex = Common.INT_DBNULL;
                    if (dgvItemLocation.SelectedRows.Count > 0)
                    {
                        rowIndex = dgvItemLocation.SelectedRows[0].Index;
                    }

                    BindGrid();
                    if (rowIndex > Common.INT_DBNULL)
                    {
                        dgvItemLocation.ClearSelection();
                        dgvItemLocation.FirstDisplayedScrollingRowIndex = rowIndex;
                        dgvItemLocation.Rows[rowIndex].Selected = true;
                    }
                }

                //if (!m_isModified)
                //    AddRecords();
                //else
                //    AddRecords(m_modifiedItemId);

                //ResetControls();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void ResetControls()
        {
            try
            {
                cmbLocation.SelectedIndex = 0;
                txtROL.Text = string.Empty;
                //cmbStatus.SelectedIndex = 0;
                if (cmbStatus.Items.Count > 0)
                    cmbStatus.SelectedValue = 1;
                ResetValidator();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void ResetValidator()
        {
            Validators.SetErrorMessage(epLocation, cmbStatus);
            Validators.SetErrorMessage(epLocation, txtROL);
        }

        #region AddRecords - 2 Overloaded methods

        void AddRecords(Int32 modifiedItemId)
        {
            try
            {
                LocationList newLocation = null;
                if (Convert.ToInt32(cmbLocation.SelectedValue) != Common.INT_DBNULL && Convert.ToInt32(cmbLocation.SelectedValue) == m_modifiedLocationId)
                {
                    newLocation = CreateNewLocation(modifiedItemId, m_modifiedLocationId, cmbLocation.Text);
                }

                RemoveRecord(m_modifiedLocationId, m_isModified);
                NullifyGrid();
                m_locationList.Add(newLocation);
                BindGrid();

                m_isModified = false;
                m_modifiedItemId = Common.INT_DBNULL;
                m_modifiedLocationId = Common.INT_DBNULL;
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
                LocationList newLocation = null;
                //NullifyGrid();
                if ((Int32)cmbLocation.SelectedValue == Common.INT_DBNULL)
                {
                    foreach (DataRowView objItem in cmbLocation.Items)
                    {
                        if (Convert.ToInt32(objItem.Row[LocationList.GRID_LOCID]) == Common.INT_DBNULL)
                            continue;

                        if (CheckIfRecordExists(Convert.ToInt32(objItem.Row[LocationList.GRID_LOCID])))
                        {
                            MessageBox.Show(Common.GetMessage("VAL0007", "Record"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            continue;
                        }
                        if (m_ItemId != Common.INT_DBNULL)
                        {
                            newLocation = CreateNewLocation(m_ItemId, Convert.ToInt32(objItem.Row[LocationList.GRID_LOCID]), Convert.ToString(objItem.Row[LocationList.GRID_LOCNAME]));
                        }
                        else
                        {
                            newLocation = CreateNewLocation(Convert.ToInt32(objItem.Row[LocationList.GRID_LOCID]), Convert.ToString(objItem.Row[LocationList.GRID_LOCNAME]));
                        }
                        m_locationList.Add(newLocation);
                    }
                }
                else
                {
                    if (CheckIfRecordExists(Convert.ToInt32(cmbLocation.SelectedValue)))
                    {
                        ModifyRecord(Convert.ToInt32(cmbLocation.SelectedValue), cmbLocation.Text);
                    }
                    else
                    {
                        if (m_ItemId != Common.INT_DBNULL)
                        {
                            newLocation = CreateNewLocation(m_ItemId, Convert.ToInt32(cmbLocation.SelectedValue), cmbLocation.Text);
                        }
                        else
                        {
                            newLocation = CreateNewLocation(Convert.ToInt32(cmbLocation.SelectedValue), cmbLocation.Text);
                        }
                        m_locationList.Add(newLocation);
                    }
                }
                BindGrid();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ModifyRecord(int locationId, string locationText)
        {
            IEnumerable<LocationList> loc = from l in m_locationList where l.LocationId == locationId select l;
            loc.First<LocationList>().ReorderLevel = Validators.CheckForDBNull(txtROL.Text, Convert.ToDecimal(0));
            loc.First<LocationList>().Status = Convert.ToInt32(cmbStatus.SelectedValue);
            loc.First<LocationList>().StatusName = cmbStatus.Text;
        }
        #endregion

        #region Nullify and Binding Grid

        void NullifyGrid()
        {
            //REMOVED by AMIT. MOVED TO BindGrid()
            //dgvItemLocation.DataSource = null;
        }

        void BindGrid()
        {
            dgvItemLocation.DataSource = null;
            if (m_locationList.Count > 0)
            {
                dgvItemLocation.DataSource = m_locationList;
                dgvItemLocation.ClearSelection();
                ResetControls();
                //dgvItemLocation.Select();
            }
        }


        private void BindGrid(List<LocationList> list)
        {
            dgvItemLocation.DataSource = null;
            if (m_locationList.Count > 0)
            {
                dgvItemLocation.DataSource = list;
                dgvItemLocation.ClearSelection();
                ResetControls();
                //dgvItemLocation.Select();
            }
        }

        #endregion

        #region Create New LocationList object
        
        LocationList CreateNewLocation(Int32 locationId, String locationName)
        {
            return CreateNewLocation(Common.INT_DBNULL, locationId, locationName);
        }
        
        LocationList CreateNewLocation(Int32 itemId, Int32 locationId, String locationName)
        {
            LocationList newLocation = new LocationList();
            newLocation.ItemId = itemId;
            newLocation.LocationId = locationId;
            newLocation.LocationDisplayName = locationName;
            newLocation.ReorderLevel = Validators.CheckForDBNull(txtROL.Text, Convert.ToDecimal(0));
            newLocation.Status = Convert.ToInt32(cmbStatus.SelectedValue);
            newLocation.StatusName = cmbStatus.Text;
            newLocation.CreatedBy = AuthenticationComponent.BusinessObjects.Authenticate.LoggedInUser.UserId;
            newLocation.ModifiedBy = AuthenticationComponent.BusinessObjects.Authenticate.LoggedInUser.UserId;
            newLocation.ModifiedDate = Convert.ToDateTime(m_modifiedDate.ToString(Common.DATE_TIME_FORMAT));

            return newLocation;
        }
        
        #endregion

        Boolean CheckIfRecordExists(Int32 locationId)
        {
            Int32 count = (from l in m_locationList where l.LocationId == locationId select l).Count();

            if (count > 0) return true;
            return false;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            m_originalLocationList = new List<LocationList>();
            CopyLists(m_locationList, m_originalLocationList);
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void dgvItemLocation_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
                {
                    if (dgvItemLocation.Columns[e.ColumnIndex].CellType == typeof(DataGridViewImageCell))
                    {
                        Int32 itemId = Convert.ToInt32(dgvItemLocation.Rows[e.RowIndex].Cells[LocationList.GRID_ITEMID].Value);

                        //if (itemId != Common.INT_DBNULL)
                        LocationList l = m_originalLocationList.Find(delegate(LocationList l1) { return l1.LocationId == Convert.ToInt32(dgvItemLocation.Rows[e.RowIndex].Cells[LocationList.GRID_LOCID].Value); });
                        if (l != null)
                        {
                            MessageBox.Show(Common.GetMessage("VAL0010", "location"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            return;
                        }

                        DialogResult result = MessageBox.Show(Common.GetMessage("INF0028"), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            RemoveRecord(Convert.ToInt32(dgvItemLocation.Rows[e.RowIndex].Cells["LocationId"].Value), m_isModified);
                        }
                        return;
                    }

                    //if (dgvItemLocation.Columns[e.ColumnIndex].CellType == typeof(DataGridViewTextBoxCell) && dgvItemLocation.Columns[e.ColumnIndex].IsDataBound == true)
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

        void SelectRecord(DataGridView dgv)
        {
            try
            {
                LocationList selectedLocation = (from l in m_locationList where l.LocationId == Convert.ToInt32(dgvItemLocation.CurrentRow.Cells["LocationId"].Value) select l).FirstOrDefault();
                if (selectedLocation != null)
                {
                    cmbLocation.SelectedValue = selectedLocation.LocationId;
                    cmbStatus.SelectedValue = selectedLocation.Status;
                    txtROL.Text = selectedLocation.DisplayReorderLevel.ToString();//selectedLocation.ReorderLevel.ToString();

                    if (selectedLocation.ItemId != Common.INT_DBNULL)
                    {
                        m_isModified = true;
                        m_modifiedItemId = selectedLocation.ItemId;
                        m_modifiedLocationId = selectedLocation.LocationId;
                        m_modifiedDate = selectedLocation.ModifiedDate;
                    }
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void RemoveRecord(Int32 locationId, Boolean isModified)
        {
            try
            {
                NullifyGrid();
                LocationList removeLocation = (from l in m_locationList where l.LocationId == locationId select l).FirstOrDefault();

                if (removeLocation != null)
                    m_locationList.Remove(removeLocation);

                BindGrid();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void dgvItemLocation_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                SelectRecord(sender as DataGridView);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                NullifyGrid();

                for (int cnt = 0; cnt < m_locationList.Count; )
                {
                    if (m_locationList[cnt].ModifiedDate == Common.DATETIME_NULL)
                    {
                        m_locationList.Remove(m_locationList[cnt]);
                    }
                    else
                    {
                        cnt = cnt + 1;
                    }
                }

                BindGrid();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtROL_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Validators.CheckForEmptyString(txtROL.Text.Length))
                {
                    Validators.SetErrorMessage(epLocation, txtROL, "VAL0001", lblROL.Text);
                }
                else
                {
                    Validators.SetErrorMessage(epLocation, txtROL);
                }
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
                if (Validators.CheckForSelectedValue(cmbStatus.SelectedIndex))
                {
                    Validators.SetErrorMessage(epLocation, cmbStatus, "VAL0002", lblStatus.Text);
                }
                else
                {
                    Validators.SetErrorMessage(epLocation, cmbStatus);
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvItemLocation_SelectionChanged(object sender, EventArgs e)
        {
            SelectRecord(sender as DataGridView);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            m_locationList = new List<LocationList>();
            CopyLists(m_originalLocationList, m_locationList);
            BindGrid(m_originalLocationList);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetControls();
        }

    }
}
