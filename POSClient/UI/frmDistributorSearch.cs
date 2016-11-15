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
using POSClient.BusinessObjects;

namespace POSClient.UI
{
    public partial class frmDistributorSearch : BaseChildForm
    {
        #region Variable Declaration

        private int m_distributorId = Common.INT_DBNULL;
        private Distributor m_distributorObj = null;
        private List<Distributor> m_distributorList = null;

        #endregion

        #region Property Declaration

        public Distributor distributorReturnObject
        {
            get
            {
                return m_distributorObj;
            }
            set
            {
                m_distributorObj = value;
            }
        }

        #endregion 
        
        #region Constructor

        public frmDistributorSearch()
        {
            InitializeComponent();
        }

        #endregion

        #region Events

        private void frmDistributorSearch_Load(object sender, EventArgs e)
        {
            try
            {
                dgvDistributorSearch = Common.GetDataGridViewColumns(dgvDistributorSearch, Environment.CurrentDirectory + "\\App_Data\\POSGridViewDefinition.xml");
                ResetControls();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {            
            try
            {
                if (Validators.IsInt32(txtDistributorId.Text.Trim() == string.Empty ? Common.INT_DBNULL.ToString() : (txtDistributorId.Text)))
                    SearchDistributor();
                else
                {
                    dgvDistributorSearch.DataSource = new List<Distributor>();
                    MessageBox.Show(Common.GetMessage("VAL0009", lblDistributorId.Text),Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvDistributorSearch.SelectedRows.Count == 1)
                {
                    SetDistributorObject(dgvDistributorSearch.CurrentRow.Index);
                    DialogResult = DialogResult.OK;                    
                }
                else
                {
                    distributorReturnObject = null;                                        
                }
                this.Close();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                distributorReturnObject = null;
                DialogResult = DialogResult.Cancel;
                this.Close();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvDistributorSearch_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1)
                {                    
                    SetDistributorObject(e.RowIndex);
                    DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Search Distributor Master And Accounts Details
        /// </summary>
        private void SearchDistributor()
        {
            string errorMessage = string.Empty;
            m_distributorObj = new Distributor();
            m_distributorList = new List<Distributor>();
            m_distributorObj.DistributorId = (txtDistributorId.Text.Trim() == string.Empty ? Common.INT_DBNULL : Convert.ToInt32(txtDistributorId.Text));
            m_distributorObj.SDistributorId = txtDistributorId.Text.Trim();
            m_distributorObj.DistributorFirstName = txtFirstName.Text;
            m_distributorObj.DistributorLastName = txtLastName.Text;
            m_distributorList = m_distributorObj.SearchDistributor(ref errorMessage);
            if (m_distributorList.Count > 0)
            {
                dgvDistributorSearch.DataSource = m_distributorList;
                dgvDistributorSearch.ClearSelection();
            }
            else
            {
                dgvDistributorSearch.DataSource = new List<Distributor>();
                MessageBox.Show(Common.GetMessage("8002"));
            }
        }

        /// <summary>
        /// Set DistributorReturnObject.
        /// </summary>
        private void SetDistributorObject(int index)
        {           
            distributorReturnObject = m_distributorList[index];
        }

        /// <summary>
        /// Reset Controls
        /// </summary>
        private void ResetControls()
        {
            txtDistributorId.Text = string.Empty;
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            dgvDistributorSearch.DataSource = new List<Distributor>();
            m_distributorId = Common.INT_DBNULL;
            distributorReturnObject = null;
            txtDistributorId.Focus();
        }

        #endregion         
       
    }
}
