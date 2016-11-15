using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CoreComponent.BusinessObjects;

namespace CoreComponent.UI
{
    public partial class DistributorPopup : Form//POSClient.UI.BaseChildForm
    {
        private List<Distributor> m_distributorList;
        private Distributor m_selectedDistributor;

        public List<Distributor> DistributorList
        {
            get { return m_distributorList; }
            set { m_distributorList = value; }
        }


        public Distributor SelectedDistributor
        {
            get { return m_selectedDistributor; }
            set { m_selectedDistributor = value; }
        }

        public DistributorPopup(List<Distributor> distributorList)
        {
            InitializeComponent();
            m_distributorList = distributorList;
            CreateGrid();
            dgvDistributorList.DataSource = distributorList;
        }

        private void CreateGrid()
        {
            dgvDistributorList.EnableHeadersVisualStyles = false;
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            //GET FROM Config
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(233)))), ((int)(((byte)(251)))));
            dgvDistributorList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;

            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Verdana", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dgvDistributorList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;

            dgvDistributorList.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(233)))), ((int)(((byte)(251)))));
            dgvDistributorList.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;


            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dgvDistributorList.DefaultCellStyle = dataGridViewCellStyle3;
            dgvDistributorList.Columns.Clear();
            dgvDistributorList.AutoGenerateColumns = false;
            DataGridViewTextBoxColumn dgvc = new DataGridViewTextBoxColumn();
            dgvDistributorList.ColumnHeadersVisible = false;
            dgvc.Name = "dgvcId";
            dgvc.HeaderText = "Distributor #";
            dgvc.Width = 90;
            dgvc.DataPropertyName = "DistributorId";
            dgvDistributorList.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "dgvcFirstName";
            dgvc.HeaderText = "Name";
            dgvc.Width = 190;
            dgvc.DataPropertyName = "DistributorFullName";
            dgvDistributorList.Columns.Add(dgvc);

            //dgvc = new DataGridViewTextBoxColumn();
            //dgvc.Name = "dgvcLastName";
            //dgvc.HeaderText = "Last Name";
            ////dgvc.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //dgvc.Width = 215;
            //dgvc.DataPropertyName = "DistributorLastName";
            //dgvDistributorList.Columns.Add(dgvc);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            AcceptEnter();
        }

        private void AcceptEnter()
        {
            if (dgvDistributorList.SelectedRows[0].Index >= 0)
            {
                m_selectedDistributor = (Distributor)dgvDistributorList.SelectedRows[0].DataBoundItem;
                DialogResult = DialogResult.OK;
            }
            else
            {
                DialogResult = DialogResult.Cancel;
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            m_selectedDistributor = null;
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void dgvDistributorList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvDistributorList.SelectedRows[0].Index > -1)
            {
                m_selectedDistributor = (Distributor)dgvDistributorList.SelectedRows[0].DataBoundItem;
                DialogResult = DialogResult.OK;
            }
            else
            {
                DialogResult = DialogResult.Cancel;
            }
        }

        private void dgvDistributorList_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void dgvDistributorList_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                AcceptEnter();
            }
        }
    }
}
