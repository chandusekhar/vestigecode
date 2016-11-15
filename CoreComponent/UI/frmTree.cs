using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CoreComponent.Hierarchies.BusinessObjects;
using CoreComponent.Core.BusinessObjects;

using CoreComponent.BusinessObjects;

namespace CoreComponent.Hierarchies.UI
{
    public partial class frmTree : Form
    {
        #region Variables
        List<OrganizationalHierarchy> m_orgList;
        List<OrganizationalHierarchy> m_orgListFilter = null;

        List<MerchandiseHierarchy> m_MerchList;
        List<MerchandiseHierarchy> m_MerchListFilter = null;

        string m_hierarchyType;
        Form m_objOrg = null;
        bool m_firstTime = true;

        #endregion Variables

        #region CTOR

        public frmTree()
        {
            InitializeComponent();
        }
        public frmTree(string hierarchyType, string spName, int hierarchyLevel, Form obj)
        {
            InitializeComponent();
            this.m_hierarchyType = hierarchyType;
            if ((hierarchyType.Equals("Organizational", StringComparison.InvariantCultureIgnoreCase)) || (hierarchyType.Equals("OrganizationalLocation", StringComparison.InvariantCultureIgnoreCase)))
            {
                this.m_firstTime = hierarchyLevel == 2 ? false : true;

                CoreComponent.Hierarchies.BusinessObjects.OrganizationalHierarchy org = new CoreComponent.Hierarchies.BusinessObjects.OrganizationalHierarchy();

                org.HierarchyCode = DBNull.Value.ToString();
                org.HierarchyName = DBNull.Value.ToString();
                org.ParentHierarchyName = DBNull.Value.ToString();
                org.ParentHierarchyId = Common.INT_DBNULL;
                org.HierarchyType = -1;
                org.Status = 1;

                m_orgList = org.Search();

                //Get Level
                //var id = (from p in m_orgList where p.HierarchyType == hierarchyLevel select p.HierarchyLevel).Max();

                if (hierarchyLevel != -1)
                {
                    var query = from p in m_orgList where p.HierarchyLevel < hierarchyLevel select p;
                    m_orgList = query.ToList();
                }
                m_orgListFilter = m_orgList;

                if (m_orgListFilter != null & m_orgListFilter.Count > 0)
                {
                    tvHierarchy.Nodes.Add(m_orgListFilter[0].HierarchyId.ToString(), m_orgListFilter[0].HierarchyName.ToString());
                    for (Int16 i = 1; i < m_orgListFilter.Count; i++)
                    {
                        TreeNode treenode = tvHierarchy.Nodes.Find(m_orgListFilter[i].ParentHierarchyId.ToString(), true)[0];
                        tvHierarchy.SelectedNode = treenode;
                        tvHierarchy.SelectedNode.Nodes.Add(m_orgListFilter[i].HierarchyId.ToString(), m_orgListFilter[i].HierarchyName.ToString());
                    }
                }

                if (hierarchyType.Equals("Organizational", StringComparison.InvariantCultureIgnoreCase))
                    m_objOrg = ((frmOrganizationNew)obj);
                else
                {
                    m_objOrg = ((frmLocation)obj);
                    tvHierarchy.ExpandAll();
                }
            }
            else if(hierarchyType.Equals("Merchandise", StringComparison.InvariantCultureIgnoreCase))
            {
                m_objOrg = ((frmMerchandiseNew)obj);

                this.m_firstTime = hierarchyLevel == 2 ? false : true;

                MerchandiseHierarchy merch = new MerchandiseHierarchy();

                merch.HierarchyCode = DBNull.Value.ToString();
                merch.HierarchyName = DBNull.Value.ToString();
                merch.ParentHierarchyName = DBNull.Value.ToString();
                merch.ParentHierarchyId = Common.INT_DBNULL;
                merch.HierarchyType = -1;
                merch.Status = 1;

                m_MerchList = merch.Search();

                //Get Level
                //var id = (from p in m_orgList where p.HierarchyType == hierarchyLevel select p.HierarchyLevel).Max();

                if (hierarchyLevel != -1)
                {
                    var query = from p in m_MerchList where p.HierarchyLevel < hierarchyLevel select p;
                    m_MerchList = query.ToList();
                }
                m_MerchListFilter = m_MerchList;

                if (m_MerchListFilter != null & m_MerchListFilter.Count > 0)
                {
                    tvHierarchy.Nodes.Add(m_MerchListFilter[0].HierarchyId.ToString(), m_MerchListFilter[0].HierarchyName.ToString());
                    for (Int16 i = 1; i < m_MerchListFilter.Count; i++)
                    {
                        TreeNode treenode = tvHierarchy.Nodes.Find(m_MerchListFilter[i].ParentHierarchyId.ToString(), true)[0];
                        tvHierarchy.SelectedNode = treenode;
                        tvHierarchy.SelectedNode.Nodes.Add(m_MerchListFilter[i].HierarchyId.ToString(), m_MerchListFilter[i].HierarchyName.ToString());
                    }
                }
            }
            else if (hierarchyType.Equals("Promotion", StringComparison.InvariantCultureIgnoreCase))
            {
                this.m_firstTime = hierarchyLevel == 2 ? false : true;

                MerchandiseHierarchy merch = new MerchandiseHierarchy();

                merch.HierarchyCode = DBNull.Value.ToString();
                merch.HierarchyName = DBNull.Value.ToString();
                merch.ParentHierarchyName = DBNull.Value.ToString();
                merch.ParentHierarchyId = Common.INT_DBNULL;
                merch.HierarchyType = -1;
                merch.Status = 1;
                //merch.IsTradable = -1;

                m_MerchList = merch.Search();

                //Get Level
                //var id = (from p in m_orgList where p.HierarchyType == hierarchyLevel select p.HierarchyLevel).Max();

                if (hierarchyLevel != -1)
                {
                    var query = from p in m_MerchList where p.HierarchyLevel < hierarchyLevel && p.IsTradable == true select p;
                    m_MerchList = query.ToList();
                }
                else
                {
                    var query = from p in m_MerchList where p.IsTradable == true select p;
                    m_MerchList = query.ToList();
                }
                m_MerchListFilter = m_MerchList;

                if (m_MerchListFilter != null & m_MerchListFilter.Count > 0)
                {
                    tvHierarchy.Nodes.Add(m_MerchListFilter[0].HierarchyId.ToString(), m_MerchListFilter[0].HierarchyName.ToString());
                    for (Int16 i = 1; i < m_MerchListFilter.Count; i++)
                    {
                        if (tvHierarchy.Nodes.Find(m_MerchListFilter[i].ParentHierarchyId.ToString(), true).Length > 0)
                        {
                            TreeNode treenode = tvHierarchy.Nodes.Find(m_MerchListFilter[i].ParentHierarchyId.ToString(), true)[0];
                            tvHierarchy.SelectedNode = treenode;
                            tvHierarchy.SelectedNode.Nodes.Add(m_MerchListFilter[i].HierarchyId.ToString(), m_MerchListFilter[i].HierarchyName.ToString());
                        }
                    }
                }
            }
        }

        #endregion

        #region Properties
        private int m_SelectedValue;
        public int SelectedValue
        {
            get
            {
                return m_SelectedValue; 
            }
        }

        private string m_SelectedText;
        public string  SelectedText
        {
            get
            {
                return m_SelectedText;
            }
        }
        #endregion

        #region Method
        /// <summary>
        /// 
        /// </summary>
        private void TreeSelect()
        {
            if (!m_firstTime)
            {
                string selText = tvHierarchy.SelectedNode.Text;
                string selValue = tvHierarchy.SelectedNode.Name;
                if (m_hierarchyType == "Organizational")
                {
                    TextBox txtCreateParentName = (TextBox)(m_objOrg.Controls.Find("txtParentName", true)[0]);
                    txtCreateParentName.Text = selText;
                    ((frmOrganizationNew)m_objOrg).m_selectedParentId = Convert.ToInt32(selValue);
                }
                else if (m_hierarchyType == "Merchandise")
                {
                    TextBox txtCreateParentName = (TextBox)(m_objOrg.Controls.Find("txtParentName", true)[0]);
                    txtCreateParentName.Text = selText;
                    ((frmMerchandiseNew)m_objOrg).m_selectedParentId = Convert.ToInt32(selValue);
                }
                else if (m_hierarchyType == "Promotion")
                {
                    m_SelectedText = selText; 
                    m_SelectedValue = Convert.ToInt32(selValue);
                }

                else if (m_hierarchyType == "OrganizationalLocation")
                {
                    TextBox txtCreateParentName = (TextBox)(m_objOrg.Controls.Find("txtOrgLevel", true)[0]);
                    txtCreateParentName.Text = selText;

                    int hierarchyOrgLevel = (from p in m_orgList where p.HierarchyId == Convert.ToInt32(selValue) select p.HierarchyLevel).Max();
                    ((frmLocation)m_objOrg).m_selectedOrgHierarchyId= Convert.ToInt32(selValue);
                    ((frmLocation)m_objOrg).m_selectedOrgHierarchyLevel = hierarchyOrgLevel;
                }
                this.Close();
            }
            m_firstTime = false;
        }
        #endregion

        #region Event
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvHierarchy_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeSelect();
        }
        #endregion
        
    }
}
