using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CoreComponent.BusinessObjects;
using CoreComponent.Controls;
using CoreComponent.Core.BusinessObjects;
namespace CoreComponent.UI
{
    public partial class DashBoard : Form
    {   
        List<UserPanel> PanelList = new List<UserPanel>();
        public DashBoard()
        {
            try
            {
                InitializeComponent();
                InitializeControls();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        private void InitializeControls()
        {
            try
            {
                CoreComponent.BusinessObjects.PanelMaster panel = new CoreComponent.BusinessObjects.PanelMaster();
                List<CoreComponent.BusinessObjects.PanelMaster> list = panel.Search();
                cmbPanelList.DataSource = list;
                cmbPanelList.DisplayMember = "Name";
                cmbPanelList.ValueMember = "PanelID";
                PanelList = GetPanelList();
                BindTable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<UserPanel> GetPanelList()
        {
            try
            {
                UserPanel panel = new UserPanel();
                panel.UserID = 1;
                panel.LocationID = 1;
                return panel.Search();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void BindTable()
        {
            try
            {
                foreach (UserPanel panel in PanelList)
                {
                    ucPanel control = new ucPanel(panel.UserPanelID);
                    control.RemoveControl += new CoreComponent.Controls.ucPanel.MyEventHandler(panel_RemoveControl);
                    control.CloseForm += new ucPanel.MyCloseEventHandler(control_CloseForm);
                    tlpBase.Controls.Add(control);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void control_CloseForm()
        {
           // this.Close();
        }

        private void btnAddColumns_Click(object sender, EventArgs e)
        {
            try
            {
                if (PanelList.Count == GetPanelTotalColumns())
                    MessageBox.Show("Cannot add More control");
                else
                {
                    AddPanel();
                    PanelList = GetPanelList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        private void AddPanel()
        {
            try
            {
                UserPanel panel = new UserPanel();
                panel.UserID = 1;
                panel.PanelID = Convert.ToInt32(cmbPanelList.SelectedValue);
                panel.LocationID = 1;
                string errorMessage = string.Empty;
                bool issaved = panel.Save(ref errorMessage);
                ucPanel control = new ucPanel(panel.UserPanelID);
                control.RemoveControl += new CoreComponent.Controls.ucPanel.MyEventHandler(panel_RemoveControl);
                tlpBase.Controls.Add(control);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void panel_RemoveControl(object sender)
        {
            try
            {
                ucPanel panel = (ucPanel)sender;
                UserPanel _upanel = new UserPanel();
                _upanel.UserPanelID = panel.UserPanelID;
                string errorMessage = string.Empty;
                _upanel.Remove(ref errorMessage);
                tlpBase.Controls.Remove((CoreComponent.Controls.ucPanel)sender);
                PanelList = GetPanelList();
            }            
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private int GetPanelTotalColumns()
        {
            return tlpBase.ColumnCount * tlpBase.RowCount;
        }
            
    }
}
