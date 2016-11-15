using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CoreComponent.BusinessObjects;
using CoreComponent.Core.BusinessObjects;
using POSClient.BusinessObjects;

namespace POSClient.UI
{
    public partial class OrderPopup : POSClient.UI.BaseChildForm
    {
        private List<CO> m_COList;
        private CO m_selectedOrder;

        public List<CO> COList
        {
            get { return m_COList; }
            set { m_COList = value; }
        }


        public CO SelectedOrder
        {
            get { return m_selectedOrder; }
            set { m_selectedOrder = value; }
        }

        public OrderPopup(List<CO> COList)
        {
            InitializeComponent();
            m_COList = COList;
            CreateGrid();
            dgvOrderList.DataSource = COList;
        }

        private void CreateGrid()
        {
            dgvOrderList.EnableHeadersVisualStyles = false;
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            //GET FROM Config
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(233)))), ((int)(((byte)(251)))));
            dgvOrderList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;

            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Verdana", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dgvOrderList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;

            dgvOrderList.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(233)))), ((int)(((byte)(251)))));
            dgvOrderList.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;


            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dgvOrderList.DefaultCellStyle = dataGridViewCellStyle3;
            dgvOrderList.Columns.Clear();
            dgvOrderList.AutoGenerateColumns = false;
            DataGridViewTextBoxColumn dgvc = new DataGridViewTextBoxColumn();
            dgvOrderList.ColumnHeadersVisible = false;
            dgvc.Name = "dgvcId";
            dgvc.HeaderText = "Order No";
            dgvc.Width = 140;
            dgvc.DataPropertyName = "CustomerOrderNo";
            dgvOrderList.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "dgvcFirstName";
            dgvc.HeaderText = "Date";
            dgvc.Width = 140;
            dgvc.DataPropertyName = "OrderDate";
            dgvOrderList.Columns.Add(dgvc);
            
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
            if (dgvOrderList.SelectedRows[0].Index >= 0)
            {
                m_selectedOrder = (CO)dgvOrderList.SelectedRows[0].DataBoundItem;
                DialogResult = DialogResult.OK;
            }
            else
            {
                DialogResult = DialogResult.Cancel;
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            m_selectedOrder = null;
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void dgvDistributorList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvOrderList.SelectedRows[0].Index > -1)
            {
                m_selectedOrder = (CO)dgvOrderList.SelectedRows[0].DataBoundItem;
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
