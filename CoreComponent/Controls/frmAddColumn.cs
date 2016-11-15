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
    public partial class frmAddColumn : Form
    {
        //private int m_PanelID,m_userPanelID;
        ucPanel m_userPanel;
        List<ColumnMaster> m_ColumnList;
        public frmAddColumn(ucPanel _panel)
        {
            InitializeComponent();
            m_userPanel = _panel;            
            BindCombo();           
        }
     
        private void BindCombo()
        {
            cmbColumList.DataSource = null;
            CoreComponent.BusinessObjects.ColumnMaster column = new ColumnMaster();
            column.PanelID = m_userPanel.PanelID;
            string errorMessage = string.Empty;
            m_ColumnList= column.GetToAddColumn(m_userPanel.UserPanelID,ref errorMessage);
            if (m_ColumnList != null && m_ColumnList.Count > 0)
            {
                cmbColumList.DataSource = m_ColumnList;
                cmbColumList.DisplayMember = "HeaderText";
                cmbColumList.ValueMember = "ColumnID";
            }            
        }       

        private void btnAdd_Click(object sender, EventArgs e)
        {
            UserColumn column = new UserColumn();
            column.UserPanelID = m_userPanel.UserPanelID;
            column.Column = new ColumnMaster();
            column.Column.ColumnID = Convert.ToInt32(cmbColumList.SelectedValue);
            string errorMessage = string.Empty;
            bool isSave = column.Save(ref errorMessage);
            if (isSave)
            {
                m_userPanel.initailizeControls();
                BindCombo();
            }
        }        
    }
}
