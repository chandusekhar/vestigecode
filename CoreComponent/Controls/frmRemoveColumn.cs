using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CoreComponent.BusinessObjects;
namespace CoreComponent.Controls
{
    public partial class frmRemoveColumn : Form
    {
        ucPanel m_userPanel; List<UserColumn> ColumnsList;
        public frmRemoveColumn()
        {
            InitializeComponent();
        }
        public frmRemoveColumn(ucPanel Panel, List<UserColumn> listColumns)
        {
            try
            {
                InitializeComponent();
                m_userPanel = Panel;
                ColumnsList = listColumns;
                BindCombo();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void BindCombo()
        {
            try
            {
                string error = string.Empty;
                cmbColumList.DataSource = null;
                ColumnMaster column = new ColumnMaster();
                column.PanelID = m_userPanel.PanelID;
                cmbColumList.DataSource = column.GetToRemoveColumn(m_userPanel.UserPanelID,ref error);                
                cmbColumList.DisplayMember = "HeaderText";
                cmbColumList.ValueMember = "ColumnID";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                UserColumn column = new UserColumn();
                column.UserPanelID = m_userPanel.UserPanelID;
                column.Column = new ColumnMaster();
                column.Column.ColumnID = Convert.ToInt32(cmbColumList.SelectedValue);
                string errorMessage = string.Empty;
                bool isSave = column.Remove(ref errorMessage);
                if (isSave)
                {
                    //var query = (from v in ColumnsList where v.UserPanelID == m_userPanel.UserPanelID select v);
                    //if (query.ToList<UserColumn>().Count > 0)
                    //{
                    //    ColumnsList.Remove(query.ToList<UserColumn>()[0]);
                    //}
                    BindCombo();
                    m_userPanel.initailizeControls();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

    }
}
