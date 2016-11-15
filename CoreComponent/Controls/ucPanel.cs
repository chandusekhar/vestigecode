using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CoreComponent.BusinessObjects;
using System.Reflection;
using CoreComponent.Core.BusinessObjects;
namespace CoreComponent.Controls
{
    
    public partial class ucPanel : UserControl
    {
        #region Property And Variables
        public delegate void MyEventHandler(object sender);
        public delegate void MyCloseEventHandler();
        private int m_userPanelID;
        private int m_PanelID;
        private UserPanel m_CurrPanel;
        public int UserPanelID
        {
            get { return m_userPanelID; }
            set { m_userPanelID = value; }
        }

        public int PanelID
        {
            get { return m_PanelID; }
            set { m_PanelID = value; }
        }

        [Description("Remove event.")]
        public event MyEventHandler RemoveControl;

        [Description("Close event.")]
        public event MyCloseEventHandler CloseForm;

        #endregion


        #region Constructor
        public ucPanel(int upanelID)
        {
            try
            {
                InitializeComponent();
                m_userPanelID = upanelID;
                initailizeControls();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }

        }
        #endregion

        /// <summary>
        /// Initialize Controls
        /// </summary>
        public void initailizeControls()
        {
            try
            {
                m_CurrPanel =  GetPanelDetail(UserPanelID);
                if (m_CurrPanel != null)
                {
                    lblPanelName.Text = m_CurrPanel.Panel.Name;
                    PanelID = m_CurrPanel.PanelID;
                    LoadGrid();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Set Grids Column
        /// </summary>
        private void LoadGrid()
        {
            try
            {
                dgvSearch.Columns.Clear();
                if (m_CurrPanel.ListColumn != null && m_CurrPanel.ListColumn.Count > 0)
                {
                    GetDataGridViewColumns(dgvSearch, m_CurrPanel.ListColumn);
                    SetDataSource();
                }              
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       
        /// <summary>
        /// Set Data Source of Grid
        /// </summary>
        public void SetDataSource()
        {
            try
            {
                string _error = string.Empty;
                bool isBind = false;
                foreach (DataGridViewColumn column in dgvSearch.Columns)
                {
                    if (column.Visible)
                    {
                        isBind = true;
                        break;
                    }
                }
                if(isBind)
                    dgvSearch.DataSource = PanelCommon.InvokeMethod(m_CurrPanel.Panel.SearchMethod, m_CurrPanel.ListSearchField, ref _error);                     
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private UserPanel GetPanelDetail(int upanelID)
        {
            try
            {
                UserPanel panel = new UserPanel();
                panel.UserPanelID = upanelID;
                List<UserPanel> Panels = panel.Search();
                if (Panels != null && Panels.Count > 0)
                    return Panels[0];
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                if (RemoveControl != null)
                    RemoveControl(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        public static void GetDataGridViewColumns(DataGridView dgv, List<UserColumn> Columns)
        {
            try
            {

                dgv.Parent.SuspendLayout();
                dgv.Columns.Clear();
                dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgv.AllowUserToResizeColumns = false;
                dgv.AllowUserToResizeRows = false;
                dgv.AutoGenerateColumns = false;              
                

                foreach (UserColumn uc in Columns)
                {
                    DataGridViewTextBoxColumn dgvcText = new DataGridViewTextBoxColumn();
                    dgvcText.Name = uc.Column.ColumnName;
                    dgvcText.HeaderText = uc.Column.HeaderText;
                    if (uc.Column.DataPropertyName != string.Empty)
                        dgvcText.DataPropertyName = uc.Column.DataPropertyName;
                    dgvcText.Visible = uc.Column.IsVisible;
                    dgvcText.Width = uc.Column.Width;
                    dgvcText.ReadOnly = true;
                    dgv.Columns.Add(dgvcText);
                }
                dgv.Parent.ResumeLayout(false);
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
                frmAddColumn frmAdd = new frmAddColumn(this);
                frmAdd.StartPosition = FormStartPosition.CenterParent;
                frmAdd.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                frmRemoveColumn frmRemove = new frmRemoveColumn(this, m_CurrPanel.ListColumn);
                frmRemove.StartPosition = FormStartPosition.CenterParent;
                frmRemove.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                frmPanelSearch search = new frmPanelSearch(this);
                search.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        private void dgvSearch_DoubleClick(object sender, EventArgs e)
        {
            try
            {  
                if(m_CurrPanel.Panel!=null && m_CurrPanel.Panel.ConstructorMethod!=null && m_CurrPanel.Panel.ConstructorMethod.AssemblyName!=null)
                {
                    Assembly asy = Assembly.Load(m_CurrPanel.Panel.ConstructorMethod.AssemblyName);
                    if (asy !=null && m_CurrPanel.Panel.ConstructorMethod.ClassName != null)
                    {
                        Type t = asy.GetType(m_CurrPanel.Panel.ConstructorMethod.ClassName);
                        if (t != null)
                        {
                            object[] cons = null;
                            // Set Constructor Parameter
                            if (m_CurrPanel.Panel.ConstructorMethod.ParameterList != null)
                            {
                                cons = new object[m_CurrPanel.Panel.ConstructorMethod.ParameterList.Count];
                                int i = 0;
                                foreach (MethodParameter param in m_CurrPanel.Panel.ConstructorMethod.ParameterList)
                                {
                                    switch (param.PropertyType)
                                    {
                                        case PanelCommon.PropertyType.None:
                                            {
                                                cons[i] = param.Value;
                                                break;
                                            }
                                        case PanelCommon.PropertyType.Column:
                                            {
                                                cons[i] = dgvSearch.SelectedRows[0].Cells[param.ValueFrom].Value;
                                                break;
                                            }
                                        case PanelCommon.PropertyType.Property:
                                            {
                                                break;
                                            }
                                        default:
                                            break;


                                    }
                                    i++;
                                }
                            }
                            // Create Form Instance
                            Form MyForm = (Form)Activator.CreateInstance(t, cons);
                            if (MyForm != null)
                            {
                                //Set Form Tag
                                MyForm.Tag = m_CurrPanel.Panel.Tag;
                                MyForm.Visible = false;

                                //Set Parameters for Show Method 
                                MethodInfo info = t.GetMethod(m_CurrPanel.Panel.ShowMethod.MethodName);
                                object[] obj;
                                obj = new object[m_CurrPanel.Panel.ShowMethod.ParameterList.Count];
                                int j = 0;
                                foreach (MethodParameter param in m_CurrPanel.Panel.ShowMethod.ParameterList)
                                {
                                    switch (param.PropertyType)
                                    {
                                        case PanelCommon.PropertyType.None:
                                            {
                                                obj[j] = param.Value;
                                                break;
                                            }
                                        case PanelCommon.PropertyType.Column:
                                            {
                                                if (dgvSearch.Columns.Contains(param.ValueFrom))
                                                    obj[j] = dgvSearch.SelectedRows[0].Cells[param.ValueFrom].Value;
                                                break;
                                            }
                                        case PanelCommon.PropertyType.Property:
                                            {
                                                foreach (DataGridViewColumn column in dgvSearch.Columns)
                                                {
                                                    if (column.DataPropertyName == param.ValueFrom)
                                                    {
                                                        obj[j] = dgvSearch.SelectedRows[0].Cells[column.Name].Value;
                                                        break;
                                                    }
                                                }
                                                break;
                                            }
                                        default:
                                            break;

                                    }
                                    j++;
                                }
                                //Call Form
                                MyForm.Show();
                                info.Invoke(MyForm, obj);
                                MyForm.Visible = true;
                                CloseForm();
                            }
                        }
                        else
                        {
                            MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Common.LogException(new Exception("Class Not Found:" + m_CurrPanel.Panel.ConstructorMethod.ClassName));
                        }
                    }
                    else
                    {
                        MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Common.LogException(new Exception("ClassName Not Found"));
                    }
                }
                else
                {
                    MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Common.LogException(new Exception("Assembly Not Found"));
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }
    

    }
}
