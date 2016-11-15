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
    public delegate void TerminalHandler(object sender, TerminalArgs e);

   

    public partial class frmLocationTerminal : Form
    {
        public event TerminalHandler TerminalAdded;

        #region Variables
        private List<LocationTerminal> m_terminalList = null;
        int m_selectedItemRowIndex=Common.INT_DBNULL;
        private List<LocationTerminal> m_originalTerminalList = null;

        private Int32 m_modifiedItemTerminalId = Common.INT_DBNULL;
        private Int32 m_modifiedItemId = Common.INT_DBNULL;
        private Int32 m_modifiedTerminalId = Common.INT_DBNULL;
        private LocationTerminal m_terminal = null;
        #endregion

        #region Constructors
        public frmLocationTerminal()
        {
            try
            {
                InitializeComponent();
                //InitializeControls();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public frmLocationTerminal(int locationId,List<LocationTerminal> lstLocTerminals)
        {
            try
            {
                InitializeComponent();
                InitializeControls();
                m_originalTerminalList = new List<LocationTerminal>();
                LocationTerminal lt = new LocationTerminal();
                m_terminal = null;
                if (!(lstLocTerminals != null && lstLocTerminals.Count > 0))
                    m_terminalList = lt.TerminalSearch(locationId);
                else
                {
                    m_terminalList = lstLocTerminals;
                }

                //if (locationId != Common.INT_DBNULL)
                CopyLists(m_terminalList, m_originalTerminalList);
                BindGrid();
                ResetControls();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Events
        void CallValidations()
        {
            try
            {
                if (Validators.CheckForSelectedValue(cmbStatus.SelectedIndex))
                    Validators.SetErrorMessage(epUOM, cmbStatus, "VAL0002", lblStatus.Text);
                else
                    Validators.SetErrorMessage(epUOM, cmbStatus);

                if (Validators.CheckForEmptyString(txtTerminals.Text.Length))
                    Validators.SetErrorMessage(epUOM, txtTerminals, "VAL0002", lblTerminalCode.Text);

                else if (Common.CodeValidate(txtTerminals.Text,lblTerminalCode.Text).Trim().Length > 0)
                   epUOM.SetError(txtTerminals, Common.CodeValidate(txtTerminals.Text, lblTerminalCode.Text.Substring(0,lblTerminalCode.Text.Length - 2)));
                else
                    Validators.SetErrorMessage(epUOM, txtTerminals);

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
                if (CheckIfRecordExists(txtTerminals.Text) && m_selectedItemRowIndex==Common.INT_DBNULL)
                {
                    //IEnumerable<LocationTerminal> Terminal = (from u in m_terminalList where u.TerminalCode == txtTerminals.Text && u.Status == Convert.ToInt32(cmbStatus.SelectedValue) select u);
                    //Terminal.ElementAt<LocationTerminal>(0).Status = Convert.ToInt32(cmbStatus.SelectedValue);
                    //BindGrid();
                    MessageBox.Show(Common.GetMessage("VAL0063", lblTerminalCode.Text.Substring(0, lblTerminalCode.Text.Length - 2)), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult = DialogResult.OK;
                this.Close();

                TerminalArgs sArgs = new TerminalArgs();
                sArgs.IsSuccess = true;
                sArgs.TerimalList = m_terminalList;
                OnTerminalAdded(sArgs);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvTerminal_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    if (dgvTerminal.Columns[e.ColumnIndex].CellType == typeof(DataGridViewImageCell))//typeof(DataGridViewButtonCell))
                    {
                        string terminalCode = dgvTerminal.Rows[e.RowIndex].Cells[1].Value.ToString();

                        DialogResult result = MessageBox.Show(Common.GetMessage("INF0028"), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            LocationTerminal TerminalDet = new LocationTerminal();

                            TerminalDet.TerminalCode = dgvTerminal.Rows[e.RowIndex].Cells[1].Value.ToString();
                            TerminalDet.Status = Convert.ToInt32(dgvTerminal.Rows[e.RowIndex].Cells[3].Value);

                            RemoveRecord(TerminalDet);
                        }
                        return;
                    }

                    if (dgvTerminal.Columns[e.ColumnIndex].CellType == typeof(DataGridViewTextBoxCell) && dgvTerminal.Columns[e.ColumnIndex].IsDataBound == true)
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult = DialogResult.Cancel;
                m_terminalList = new List<LocationTerminal>();

                TerminalArgs sArgs = new TerminalArgs();
                sArgs.IsSuccess = true;
                sArgs.TerimalList = m_originalTerminalList;
                OnTerminalAdded(sArgs);
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
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Methods
        void SelectRecord()
        {
            try
            {
                if (dgvTerminal.SelectedCells.Count > 0)
                {
                    int rowIndex = dgvTerminal.SelectedCells[0].RowIndex;
                    int columnIndex = dgvTerminal.SelectedCells[0].ColumnIndex;

                    if (rowIndex >= 0 && columnIndex >= 0)
                    {
                        m_selectedItemRowIndex = rowIndex;
                    }
                    LocationTerminal citem = (from c in m_terminalList where c.TerminalCode == dgvTerminal.CurrentRow.Cells["TerminalCode"].Value.ToString() select c).FirstOrDefault();

                    if (citem != null)
                    {
                        txtTerminals.Text = dgvTerminal.CurrentRow.Cells["TerminalCode"].Value.ToString();
                        cmbStatus.SelectedValue = Convert.ToInt32(dgvTerminal.CurrentRow.Cells["Status"].Value);
                        m_terminal = citem;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void InitializeControls()
        {
            try
            {
                //Get Columns for DataGridView
                DataGridView dgv = Common.GetDataGridViewColumns(dgvTerminal, Environment.CurrentDirectory + "\\App_Data\\GridViewDefinition.xml");
                dgvTerminal.AutoGenerateColumns = false;

                dgvTerminal.ReadOnly = true;

                Common.BindParamComboBox(cmbStatus, Common.STATUS, 0, 0, 0); //SEARCH_STATUS
                if (cmbStatus.Items.Count > 0)
                    cmbStatus.SelectedValue = 1;

                //ResetControls();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        void RemoveRecord(LocationTerminal TerminalDet)
        {
            try
            {
                LocationTerminal removeTerminal = (from u in m_terminalList where (u.TerminalCode == TerminalDet.TerminalCode) select u).FirstOrDefault();

                //if (removeTerminal != null)
                //    m_terminalList.Remove(removeTerminal);

                if ((m_selectedItemRowIndex != Common.INT_DBNULL) && (m_selectedItemRowIndex <= dgvTerminal.Rows.Count))
                    m_terminalList.RemoveAt(m_selectedItemRowIndex + 1);


                BindGrid();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void ResetControls()
        {
            m_terminal = null;
            txtTerminals.Text = string.Empty;
            m_selectedItemRowIndex = Common.INT_DBNULL;
            //cmbStatus.SelectedIndex = 0;
            if (cmbStatus.Items.Count > 0)
                cmbStatus.SelectedValue = 1;
            dgvTerminal.SelectionChanged -= new System.EventHandler(dgvTerminal_SelectionChanged);
            dgvTerminal.ClearSelection();
            dgvTerminal.SelectionChanged += new System.EventHandler(dgvTerminal_SelectionChanged);
        }

        void AddRecords()
        {
            try
            {

                LocationTerminal newTerminal;

              
                    newTerminal = CreateNewTerminal(txtTerminals.Text, Convert.ToInt32(cmbStatus.SelectedValue), cmbStatus.Text);


                    if (m_terminalList == null)
                        m_terminalList = new List<LocationTerminal>();

                    if ((m_selectedItemRowIndex != Common.INT_DBNULL) && (m_selectedItemRowIndex <= dgvTerminal.Rows.Count))
                    {
                        m_terminalList.Insert(m_selectedItemRowIndex, newTerminal);

                        if (m_terminal != null)
                            RemoveRecord(m_terminal);
                    }
                    else
                        m_terminalList.Add(newTerminal);
               

                BindGrid();

                ResetControls();

                m_terminal = null;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        LocationTerminal CreateNewTerminal(string terminalCode, int status, string statusName)
        {

            LocationTerminal newTerminal = new LocationTerminal();
            try
            {
                newTerminal.TerminalCode = terminalCode;
                newTerminal.StatusName = statusName;
                newTerminal.Status = status;
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
                if (m_terminalList != null && m_terminalList.Count > 0)
                {
                    dgvTerminal.SelectionChanged -= new System.EventHandler(dgvTerminal_SelectionChanged);
                    
                    dgvTerminal.DataSource=null;
                    dgvTerminal.DataSource = m_terminalList;

                    dgvTerminal.ClearSelection();

                    dgvTerminal.SelectionChanged += new System.EventHandler(dgvTerminal_SelectionChanged);
                    
                }
              //  dgvTerminal.ClearSelection();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void BindGrid(List<LocationTerminal> lstTerminals)
        {
            try
            {
                dgvTerminal.DataSource = null;
                if (m_terminalList.Count > 0)
                {
                    dgvTerminal.DataSource = lstTerminals;
                    dgvTerminal.ClearSelection();
                    ResetControls();
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        Boolean CheckIfRecordExists(string termianlCode)
        {
            try
            {
                if (m_terminalList != null)
                {
                    Int32 count = (from u in m_terminalList where u.TerminalCode == txtTerminals.Text select u).Count();

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


        Boolean CheckIfRecordExists(string termianlCode, int isPrim)
        {
            try
            {
                if (m_terminalList != null)
                {
                    Int32 count = (from u in m_terminalList where u.TerminalCode == txtTerminals.Text && u.Status == isPrim select u).Count();

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

        private void CopyLists(List<LocationTerminal> fromList, List<LocationTerminal> toList)
        {
            try
            {
                if (fromList != null)
                {
                    foreach (LocationTerminal iud in fromList)
                    {
                        LocationTerminal newTerminal = new LocationTerminal();
                        newTerminal.TerminalCode = iud.TerminalCode;
                        newTerminal.Status = iud.Status;
                        newTerminal.StatusName = iud.StatusName;
                        toList.Add(newTerminal);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected virtual void OnTerminalAdded(TerminalArgs e)
        {
            if (TerminalAdded != null)
            {
                TerminalAdded(this, e);
            }
        }

        String GetErrorMessages()
        {
            try
            {
                StringBuilder sbError = new StringBuilder();
                Validators.AppendToStringBuilder(Validators.GetErrorMessage(epUOM, txtTerminals), ref sbError);
                Validators.AppendToStringBuilder(Validators.GetErrorMessage(epUOM, cmbStatus), ref sbError);

                return sbError.ToString().Replace(Environment.NewLine + Environment.NewLine, String.Empty).Trim();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        private void dgvTerminal_SelectionChanged(object sender, EventArgs e)
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
        
    }

    public class TerminalArgs : EventArgs
    {
        private bool m_isSuccess;

        public List<LocationTerminal> TerimalList
        {
            get;
            set;
        }

        public bool IsSuccess
        {
            get { return m_isSuccess; }
            set { m_isSuccess = value; }
        }

        public TerminalArgs()
        {
        }
    }
}
