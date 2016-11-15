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
using CoreComponent.Hierarchies.BusinessObjects;

namespace CoreComponent.UI
{
    public delegate void ZoneStateHandler(object sender, ZoneStateEventArgs e);

    public partial class frmOrganizationZone : Form
    {
        public event ZoneStateHandler ZoneStateHandler;

        #region Variables
        private List<State> m_stateList = null;
        private State m_state = null;
        private List<State> m_originalStateList = null;
        //Boolean m_selectedRows = false;

        #endregion

        public frmOrganizationZone()
        {
            try
            {
                InitializeComponent();
                InitializeControls();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public frmOrganizationZone(int hierarchyId,List<State> lstStates)
        {
            try
            {
                InitializeComponent();
                InitializeControls();
                m_originalStateList = new List<State>();
                OrganizationalHierarchy lt = new OrganizationalHierarchy();
                if (!(lstStates != null && lstStates.Count > 0))
                    m_stateList = lt.ZoneStateSearch(hierarchyId);
                else
                    {
                        m_stateList = new List<State>();
                        CopyLists(lstStates, m_stateList);                         
                    }
                //if (hierarchyId != Common.INT_DBNULL)
                CopyLists(m_stateList, m_originalStateList);

                BindGrid();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void dgvOrganizationZoneState_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    if (dgvOrganizationZoneState.Columns[e.ColumnIndex].CellType == typeof(DataGridViewImageCell))
                    {
                        string terminalCode = dgvOrganizationZoneState.Rows[e.RowIndex].Cells[1].Value.ToString();

                        DialogResult result = MessageBox.Show(Common.GetMessage("INF0028"), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            State TerminalDet = new State();

                            TerminalDet.StateName = dgvOrganizationZoneState.Rows[e.RowIndex].Cells[0].Value.ToString();
                            TerminalDet.StatusName = dgvOrganizationZoneState.Rows[e.RowIndex].Cells[1].Value.ToString();
                            TerminalDet.StateId = Convert.ToInt32(dgvOrganizationZoneState.Rows[e.RowIndex].Cells[2].Value);
                            TerminalDet.Status = Convert.ToInt32(dgvOrganizationZoneState.Rows[e.RowIndex].Cells[3].Value);

                            RemoveRecord(TerminalDet);
                        }
                        return;
                    }

                    if (dgvOrganizationZoneState.Columns[e.ColumnIndex].CellType == typeof(DataGridViewTextBoxCell) && dgvOrganizationZoneState.Columns[e.ColumnIndex].IsDataBound == true)
                    {
                        SelectRecord();
                    }
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        void SelectRecord()
        {
            try
            {
                State citem = (from c in m_stateList where c.StateId == Convert.ToInt32(dgvOrganizationZoneState.CurrentRow.Cells[3].Value) select c).FirstOrDefault();

                if (citem != null)
                {
                    cmbCountry.SelectedValue = Convert.ToInt32(dgvOrganizationZoneState.CurrentRow.Cells[5].Value);
                    cmbState.SelectedValue = Convert.ToInt32(dgvOrganizationZoneState.CurrentRow.Cells[3].Value);
                    cmbStatus.SelectedValue = Convert.ToInt32(dgvOrganizationZoneState.CurrentRow.Cells[4].Value);
                    m_state = citem;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void dgvTerminal_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                SelectRecord();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CopyLists(List<State> fromList, List<State> toList)
        {
            try
            {

                if (fromList != null)
                {
                    foreach (State iud in fromList)
                    {
                        State newState = new State();
                        newState.CountryId = iud.CountryId;
                        newState.CountryName = iud.CountryName;
                        newState.StateId = iud.StateId;
                        newState.StateName = iud.StateName;
                        newState.StatusName = iud.StatusName;
                        newState.Status = iud.Status;
                        toList.Add(newState);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void CallValidations()
        {
            try
            {
                if (Validators.CheckForSelectedValue(cmbStatus.SelectedIndex))
                    Validators.SetErrorMessage(epState, cmbStatus, "VAL0002", lblStatus.Text);
                else
                    Validators.SetErrorMessage(epState, cmbStatus);

                if (Validators.CheckForSelectedValue(cmbState.SelectedIndex))
                    Validators.SetErrorMessage(epState, cmbState, "VAL0002", lblState.Text);
                else
                    Validators.SetErrorMessage(epState, cmbState);

                if (Validators.CheckForSelectedValue(cmbCountry.SelectedIndex))
                    Validators.SetErrorMessage(epState, cmbCountry, "VAL0002", lblCountry.Text);
                else
                    Validators.SetErrorMessage(epState, cmbCountry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        String GetErrorMessages()
        {
            try
            {
                StringBuilder sbError = new StringBuilder();
                Validators.AppendToStringBuilder(Validators.GetErrorMessage(epState, cmbCountry), ref sbError);
                Validators.AppendToStringBuilder(Validators.GetErrorMessage(epState, cmbState), ref sbError);
                Validators.AppendToStringBuilder(Validators.GetErrorMessage(epState, cmbStatus), ref sbError);

                return sbError.ToString().Replace(Environment.NewLine + Environment.NewLine, String.Empty).Trim();
            }
            catch (Exception ex)
            {
                throw ex;
            }
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

                if (CheckIfRecordExists(Convert.ToInt32(cmbState.SelectedValue), Convert.ToInt32(cmbStatus.SelectedValue)))
                {
                    //IEnumerable<LocationTerminal> Terminal = (from u in m_terminalList where u.TerminalCode == txtTerminals.Text && u.Status == Convert.ToInt32(cmbStatus.SelectedValue) select u);
                    //Terminal.ElementAt<LocationTerminal>(0).Status = Convert.ToInt32(cmbStatus.SelectedValue);
                    //BindGrid();
                    MessageBox.Show(Common.GetMessage("VAL0063", lblState.Text.Substring(0, lblState.Text.Length - 2)), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                AddRecords();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void RemoveRecord(State state)
        {
            try
            {
                State removeState = (from u in m_stateList where (u.StateId == state.StateId && u.Status == state.Status) select u).FirstOrDefault();

                if (removeState != null)
                    m_stateList.Remove(removeState);

                //BindGrid();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        State CreateNewState(int stateId, string stateName, int countryId, string countryName, int status, string statusName)
        {

            State newTerminal = new State();
            try
            {
                newTerminal.StateId = stateId;
                newTerminal.StateName = stateName;
                newTerminal.StatusName = statusName;
                newTerminal.Status = status;
                newTerminal.CountryId = countryId;
                newTerminal.CountryName = countryName;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return newTerminal;
        }
        void BindGrid()
        {
            try
            {
                dgvOrganizationZoneState.DataSource = null;
                dgvOrganizationZoneState.ClearSelection();
                if (m_stateList != null && m_stateList.Count > 0)
                {
                    dgvOrganizationZoneState.DataSource = m_stateList;
                    dgvOrganizationZoneState.ClearSelection();
                    ResetControls();
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void ResetControls()
        {

            cmbState.SelectedIndex = 0;
            cmbCountry.SelectedIndex = 0;
            cmbStatus.SelectedIndex = 0;

        }

        void AddRecords()
        {
            try
            {
                DialogResult saveResult = MessageBox.Show(Common.GetMessage("INF0027"), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (saveResult == DialogResult.Yes)
                {
                    State newState = null;

                    if (m_state != null)
                        RemoveRecord(m_state);

                    newState = CreateNewState(Convert.ToInt32(cmbState.SelectedValue), cmbState.Text, Convert.ToInt32(cmbCountry.SelectedValue), cmbCountry.Text, Convert.ToInt32(cmbStatus.SelectedValue), cmbStatus.Text);

                    if (m_stateList == null)
                        m_stateList = new List<State>();

                    m_stateList.Add(newState);
                    BindGrid();
                    m_state = null;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        Boolean CheckIfRecordExists(int stateId, int status)
        {
            try
            {
                if (m_stateList != null)
                {
                    Int32 count = (from u in m_stateList where u.StateId == stateId && u.Status == status select u).Count();

                    if (count > 0) return true;
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return false;
        }
        /// <summary>
        /// Adding Blank Item into List
        /// </summary>
        /// <param name="cmb"></param>
        private void AddItem_SelectOne(ComboBox cmb)
        {
            DataTable dtSelectOne = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter(Common.INT_DBNULL.ToString(), 0, 0, 0));
            cmb.DataSource = dtSelectOne;
            cmb.ValueMember = Common.KEYCODE1.ToString();
            cmb.DisplayMember = Common.KEYVALUE1;
        }

        /// <summary>
        /// Bind State
        /// </summary>
        private void CountryIndexChange(bool yesNo)
        {
            if (yesNo)
            {
                if (cmbCountry.SelectedIndex > 0)
                {
                    DataTable dtState = Common.ParameterLookup(Common.ParameterType.State, new ParameterFilter(string.Empty, Convert.ToInt32(cmbCountry.SelectedValue), 0, 0));

                    cmbState.DataSource = dtState;
                    cmbState.DisplayMember = "StateName";
                    cmbState.ValueMember = "StateId";
                }
                else if (cmbCountry.SelectedIndex == 0)
                {
                    AddItem_SelectOne(cmbState);
                }
            }
            ValidateLocationCombo(cmbCountry, lblCountry, yesNo);
        }
        private void ValidateLocationCombo(ComboBox cmb, Label lbl, bool isCtrlEnabled)
        {
            if (cmb.SelectedIndex == 0 && isCtrlEnabled == false)
                epState.SetError(cmb, Common.GetMessage("INF0026", lbl.Text.Trim().Substring(0, lbl.Text.Trim().Length - 2)));
            else
                epState.SetError(cmb, string.Empty);
        }
        /// <summary>
        /// Call function CountryIndexChange to validate Country
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CountryIndexChange(true);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Initialize Controls
        /// </summary>
        private void InitializeControls()
        {

            dgvOrganizationZoneState.AutoGenerateColumns = false;
            dgvOrganizationZoneState.DataSource = null;
            DataGridView dgvOrgZoneStateNew = Common.GetDataGridViewColumns(dgvOrganizationZoneState, Environment.CurrentDirectory + "\\App_Data\\GridViewDefinition.xml");

            DataTable dt = Common.ParameterLookup(Common.ParameterType.Country, new ParameterFilter(string.Empty, 0, 0, 0));
            cmbCountry.DataSource = dt;
            cmbCountry.DisplayMember = "CountryName";
            cmbCountry.ValueMember = "CountryId";

            DataTable dtStatus = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter("ACTIVEDELETED", 0, 0, 0));
            cmbStatus.DataSource = dtStatus;
            cmbStatus.DisplayMember = Common.KEYVALUE1;
            cmbStatus.ValueMember = Common.KEYCODE1;

            if (cmbStatus.Items.Count > 0)
                cmbStatus.SelectedValue = 1;
        }


        protected virtual void OnStateAdded(ZoneStateEventArgs e)
        {
            if (ZoneStateHandler != null)
            {
                ZoneStateHandler(this, e);
            }
        }

        protected virtual void OnZoneStateHandler(ZoneStateEventArgs e)
        {
            if (ZoneStateHandler != null)
            {
                ZoneStateHandler(this, e);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult = DialogResult.OK;
                this.Close();

                ZoneStateEventArgs sArgs = new ZoneStateEventArgs();
                sArgs.StateList = m_stateList;
                OnStateAdded(sArgs);
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
                dgvOrganizationZoneState.ClearSelection();
                cmbCountry.SelectedIndex = 0;
                cmbState.SelectedIndex = 0;
                if (cmbStatus.Items.Count > 0)
                    cmbStatus.SelectedValue = 1;
                m_state = null;
                
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvOrganizationZoneState_SelectionChanged(object sender, EventArgs e)
        {
            SelectRecord();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult = DialogResult.Cancel;
                this.Close();

                ZoneStateEventArgs sArgs = new ZoneStateEventArgs();
                sArgs.StateList = m_originalStateList;
                OnStateAdded(sArgs);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    public class ZoneStateEventArgs : EventArgs
    {
        public List<State> StateList
        {
            get;
            set;
        }
    }
}
