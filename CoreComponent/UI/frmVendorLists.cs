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
    public partial class frmVendorLists : Form
    {
        private List<Int32> m_vendorList = null;
        private Boolean m_isAllChecked = false;
        private Int32 m_selectedRow = Common.INT_DBNULL;
        DataTable dt = null;

        public List<Int32> SelectedVendorList
        {
            get { return m_vendorList; }
            set { m_vendorList = value; }
        }

        public bool IsAllChecked
        {
            get { return m_isAllChecked; }
            set { m_isAllChecked = value; }
        }

        public frmVendorLists()
        {
            InitializeComponent();

            dgvVendorList.AutoGenerateColumns = false;
            BindGridView();
        }

        void BindGridView()
        {
            try
            {
                dt = GetData();
                dgvVendorList.DataSource = dt;
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        void BindGridView(DataTable dt)
        {
            try
            {
                dgvVendorList.DataSource = dt;
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        TreeNode CreateNewNode(String key, String text)
        {
            TreeNode newNode = new TreeNode();
            newNode.Name = key;
            newNode.Text = text;
            return newNode;
        }

        DataTable GetData()
        {
            return Common.ParameterLookup(Common.ParameterType.ItemVendor, new ParameterFilter(string.Empty, Common.INT_DBNULL, 0, 0));
        }


        void ReceiveSelectedNodes()
        {
            try
            {
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                ReceiveSelectedNodes();
                this.Close();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmVendorLists_Load(object sender, EventArgs e)
        {
        }

        private void dgvVendorList_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    if (dgvVendorList.Columns[e.ColumnIndex].CellType == typeof(DataGridViewCheckBoxCell) && e.ColumnIndex == 2 && Convert.ToInt32(dgvVendorList.Rows[e.RowIndex].Cells[2].Value) == 0)
                    {
                        if (m_selectedRow != Common.INT_DBNULL)
                        {
                            dgvVendorList.Rows[m_selectedRow].Cells[2].Value = 0;
                        }
                        m_selectedRow = e.RowIndex;
                    }
                    else if (dgvVendorList.Columns[e.ColumnIndex].CellType == typeof(DataGridViewCheckBoxCell) && e.ColumnIndex == 2 && Convert.ToInt32(dgvVendorList.Rows[e.RowIndex].Cells[2].Value) == 1)
                    {
                        dgvVendorList.Rows[e.RowIndex].Cells[2].Value = 1;
                    }
                    if (dgvVendorList.Columns[e.ColumnIndex].CellType == typeof(DataGridViewCheckBoxCell) && e.ColumnIndex == 0 && Convert.ToInt32(dgvVendorList.Rows[e.RowIndex].Cells[0].Value) == 0)
                    {
                        m_vendorList.Add(Convert.ToInt32(dgvVendorList.Rows[e.RowIndex].Cells["ItemId"].Value));
                    }
                    else if (dgvVendorList.Columns[e.ColumnIndex].CellType == typeof(DataGridViewCheckBoxCell) && e.ColumnIndex == 0 && Convert.ToInt32(dgvVendorList.Rows[e.RowIndex].Cells[0].Value) == 0)
                    {
                        m_vendorList.Remove(Convert.ToInt32(dgvVendorList.Rows[e.RowIndex].Cells["ItemId"].Value));
                    }
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvVendorList_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    dgvVendorList.Rows[e.RowIndex].Cells[2].Value = dgvVendorList.Rows[e.RowIndex].Cells[2].Value;
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
