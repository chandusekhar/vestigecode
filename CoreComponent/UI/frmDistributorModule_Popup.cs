using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

//vinculum-framework namespace(s)
using CoreComponent.Core.BusinessObjects;


namespace CoreComponent.UI
{
    public partial class frmDistributorModule_Popup : Form
    {
        #region Properties
        public string DistributerId
        {
            get;
            set;
        }

        public string FirstName
        {
            get;
            set;
        }

        public string LastName
        {
            get;
            set;
        }

        public string CityName
        {
            get;
            set;
        }
        #endregion

        #region Constructor
        public frmDistributorModule_Popup()
        {
            InitializeComponent();
        }

        public frmDistributorModule_Popup(DataSet distributorList)
        {
            try
            {
                InitializeComponent();

                DistributerId = string.Empty;

                dgvDistributors.AutoGenerateColumns = false;
                dgvDistributors = Common.GetDataGridViewColumns(dgvDistributors, Environment.CurrentDirectory + "\\App_Data\\GridViewDefinition.xml");
                dgvDistributors.DataSource = distributorList.Tables[0];
                dgvDistributors.Select();

                lblPageTitle.Text = "Choose a distributor to fetch information for, from the following ones:";
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Events
        private void dgvDistributors_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    SetDistributorInfo();
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvDistributors_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                SetDistributorInfo();
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
                SetDistributorInfo();
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
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Methods
        private void SetDistributorInfo()
        {
            try
            {
                if ((dgvDistributors.SelectedRows.Count == 1) && (dgvDistributors.SelectedRows[0].Index >= 0))
                {
                    DistributerId = dgvDistributors.SelectedRows[0].Cells["distributorId"].Value.ToString();
                    FirstName = dgvDistributors.SelectedRows[0].Cells["distributorFName"].Value.ToString();
                    LastName = dgvDistributors.SelectedRows[0].Cells["distributorLName"].Value.ToString();
                    CityName = dgvDistributors.SelectedRows[0].Cells["distributorCity"].Value.ToString();

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
    }
}
