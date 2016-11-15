using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CoreComponent.BusinessObjects;

namespace POSClient.UI
{
    public partial class ShortItemsPopup : POSClient.UI.BaseChildForm
    {
        DataTable m_dtItems;

        public DataTable dtItems
        {
            get { return m_dtItems; }
            set { m_dtItems = value; }
        }
       
        public ShortItemsPopup(DataTable dtItems)
        {
            InitializeComponent();
            m_dtItems = dtItems;
            CreateGrid();
            dgvDistributorList.DataSource = dtItems;
        }

        public ShortItemsPopup(DataTable dtItems, bool teamOrder)     // Added by Kaushik Debnath
        {
            InitializeComponent();
            m_dtItems = dtItems;
            CreateGrid2();
            dgvDistributorList.DataSource = dtItems;
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
            dgvDistributorList.ColumnHeadersVisible = true;
            dgvc.Name = "ItemCode";
            dgvc.HeaderText = "Item Code";
            dgvc.Width = 90;
            dgvc.DataPropertyName = "ItemCode";
            dgvDistributorList.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "ItemName";
            dgvc.HeaderText = "Item Name";
            dgvc.Width = 161;
            dgvc.DataPropertyName = "ItemName";
            dgvDistributorList.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "AvailableQty";
            dgvc.HeaderText = "Available Qty";
            dgvc.Width = 100;
            dgvc.DataPropertyName = "AvailableQty";
            dgvDistributorList.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "Required Qty";
            dgvc.HeaderText = "Required Qty";
            dgvc.Width = 100;
            dgvc.DataPropertyName = "Qty";
            dgvDistributorList.Columns.Add(dgvc);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            AcceptEnter();
        }

        private void AcceptEnter()
        {          
             DialogResult = DialogResult.OK;           
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {            
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void CreateGrid2() // Added By Kaushik
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
            dgvDistributorList.ColumnHeadersVisible = true;
            dgvc.Name = "CustomerOrderNo";
            dgvc.HeaderText = "Customer OrderNo";
            dgvc.Width = 160;
            dgvc.DataPropertyName = "CustomerOrderNo";
            dgvDistributorList.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "ItemCode";
            dgvc.HeaderText = "Item Code";
            dgvc.Width = 90;
            dgvc.DataPropertyName = "ItemCode";
            dgvDistributorList.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "ItemName";
            dgvc.HeaderText = "Item Name";
            dgvc.Width = 161;
            dgvc.DataPropertyName = "ItemName";
            dgvDistributorList.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "AvailableQty";
            dgvc.HeaderText = "Available Qty";
            dgvc.Width = 100;
            dgvc.DataPropertyName = "AvailableQty";
            //dgvDistributorList.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "Required Qty";
            dgvc.HeaderText = "Required Qty";
            dgvc.Width = 100;
            dgvc.DataPropertyName = "Qty";
            dgvDistributorList.Columns.Add(dgvc);
        }
    }
}
