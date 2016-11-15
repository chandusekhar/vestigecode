using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Collections.Specialized;

namespace CoreComponent.Controls
{
    public partial class frmSearch: CoreComponent.Core.UI.HierarchyTemplate
    {
        private SearchForm m_searchForm = null;
        private object m_returnObject;

        public object ReturnObject
        {
            get { return m_returnObject; }
            set { m_returnObject = value; }
        }

        public bool ItemCodeMapping
        {
            get;
            set;
        }
        public string FromItemCode
        {
            get;
            set;
        }
        public frmSearch()
        {
            InitializeComponent();
        }

        public frmSearch(SearchTypes sType)
        {
            InitializeComponent();
            m_searchForm = new SearchForm(sType, dgvSearchResults, pnlSearchHeader, lblPageTitle);
            dgvSearchResults.DataSource = null;
        }

        public frmSearch(SearchTypes sType, NameValueCollection nvCollection)
        {
            InitializeComponent();
            m_searchForm = new SearchForm(sType, dgvSearchResults, pnlSearchHeader, nvCollection, lblPageTitle);
            dgvSearchResults.DataSource = null;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.ReturnObject = null;
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
             if (m_searchForm != null)
            {
                if (m_searchForm.ValidateInput())
                {
                    Type searchType = m_searchForm.SearchClass;
                    m_searchForm.ItemCodeMapping = ItemCodeMapping;
                    m_searchForm.FromItemCode = FromItemCode;
                    //MethodInfo realMethodInfo = Assembly.GetExecutingAssembly().GetType("BOSClient.SearchForm").GetMethod("Search").GetGenericMethodDefinition().MakeGenericMethod(searchType);
                    MethodInfo realMethodInfo = Assembly.GetExecutingAssembly().GetType("CoreComponent.Controls.SearchForm").GetMethod("Search").GetGenericMethodDefinition().MakeGenericMethod(searchType);
                    dgvSearchResults.DataSource = realMethodInfo.Invoke(m_searchForm, null);
                    if (dgvSearchResults.Rows.Count == 0)
                    {
                        MessageBox.Show("No Record(s) found.","",MessageBoxButtons.OK);
                    }
                    dgvSearchResults.Select();
                }
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            dgvSearchResults.DataSource = null;
            m_searchForm.ResetControls();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            SetSelection();
        }

        private void SetSelection()
        {
            if (dgvSearchResults.Rows.Count > 0 && dgvSearchResults.SelectedRows[0].Index >= 0)
            {
                m_returnObject = dgvSearchResults.SelectedRows[0].DataBoundItem;
                DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void dgvSearchResults_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            CheckAndSetSelection();

        }

        private void CheckAndSetSelection()
        {
            if (dgvSearchResults.Rows.Count > 0 && dgvSearchResults.SelectedRows[0].Index >= 0)
            {
                SetSelection();
            }
        }

        private void dgvSearchResults_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar == 13)
            //{
            //    CheckAndSetSelection();
            //    e.Handled = true;
            //}
        }

        private void dgvSearchResults_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CheckAndSetSelection();
                e.Handled = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }
    }
}
