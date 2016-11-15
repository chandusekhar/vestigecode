using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CoreComponent.Core.BusinessObjects;
using CoreComponent.Core.UI;
using CoreComponent.BusinessObjects;
using AuthenticationComponent.BusinessObjects;


namespace CoreComponent.UI
{
    public partial class frmItemGroup : HierarchyTemplate
    {
        #region Variable Declaration

        List<ItemGroup> m_lstItemGroup = null;
        List<ItemGroup> m_lstItem = null;
        List<ItemWithSubCategory> m_lstAllItem = null;
        List<ItemWithSubCategory> m_lstSelectedItems = null;
        ItemGroup m_objItemGroup = null;        
        private Boolean m_dgvRowSelection = false;

        private int m_userId = Authenticate.LoggedInUser.UserId;
        private string m_strUserName = Authenticate.LoggedInUser.UserName;

        private string m_strLocationCode = Common.LocationCode;
        private int m_currentLocationId = Common.CurrentLocationId;
        private int m_locationType = Common.CurrentLocationTypeId;

        private bool m_isSearchAvailable = false;
        private bool m_isSaveAvailable = false;

        #endregion

        #region Constructor
        public frmItemGroup()
        {
            try
            {
                InitializeComponent();
                InitializeControls();
                lblPageTitle.Text = "Item Group";
                m_objItemGroup = new ItemGroup();
                m_lstSelectedItems = new List<ItemWithSubCategory>();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Methods

        /// <summary>
        /// Initialize ComboBox, DGV, Button Controls
        /// </summary>
        private void InitializeControls()
        {
            FillComboBox();
            InitializeItemList();            
            dgvItemGroup = Common.GetDataGridViewColumns(dgvItemGroup, Environment.CurrentDirectory + "\\App_Data\\GridViewDefinition.xml");
            m_isSearchAvailable = AuthenticationComponent.BusinessObjects.Authenticate.IsFunctionAccessible(m_strUserName, m_strLocationCode, "MDM10", Common.FUNCTIONCODE_SEARCH);
            m_isSaveAvailable = AuthenticationComponent.BusinessObjects.Authenticate.IsFunctionAccessible(m_strUserName, m_strLocationCode, "MDM10", Common.FUNCTIONCODE_SAVE);
            btnSave.Enabled = m_isSaveAvailable;
            btnSearch.Enabled = m_isSearchAvailable;
            cmbStatus.SelectedValue = 1;
            
        }

        /// <summary>
        /// Fill All ComboBox Controls
        /// </summary>
        private void FillComboBox()
        {
            // Initialize Status ComboBox
            DataTable dtStatus = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter("STATUS", 0, 0, 0));
            cmbStatus.DataSource = dtStatus;
            cmbStatus.DisplayMember = Common.KEYVALUE1;
            cmbStatus.ValueMember = Common.KEYCODE1;  
        }

        /// <summary>
        /// Fill ItemLists related to selected SubCategory
        /// </summary>
        private void InitializeItemList()
        {
            tvAllItems.Nodes.Clear();            
            ItemWithSubCategory objItemWithSubCategory = new ItemWithSubCategory();
            m_lstAllItem = objItemWithSubCategory.SearchItems();
            BindItemList(m_lstAllItem,tvAllItems);
        }

        /// <summary>
        /// Bind treeview AllItems with List
        /// </summary>
        /// <param name="dt"></param>
        private void BindItemList(List<ItemWithSubCategory> lst, TreeView tv)
        {
            tv.Nodes.Clear();
            string Config = "ItemId Asc";
            lst.Sort((new GenericComparer<ItemWithSubCategory>(Config.Split(' ')[0], Config.Split(' ')[1] == "Asc" ? SortDirection.Ascending : SortDirection.Descending)).Compare);

            for (int i = 0; i < lst.Count; i++)
            {
                if(!tv.Nodes.ContainsKey(lst[i].MHSubCategoryId.ToString()))
                {
                   tv.Nodes.Add(lst[i].MHSubCategoryId.ToString(), lst[i].MHSubCategoryName);
                }                
                {
                  if (!tv.Nodes.ContainsKey(lst[i].ItemId.ToString()))
                    tv.Nodes[lst[i].MHSubCategoryId.ToString()].Nodes.Add(lst[i].ItemId.ToString(), lst[i].ItemName);
                }
            }
            //tv.ExpandAll();            
        }

        /// <summary>
        /// Search Item group Details
        /// </summary>
        private void SearchItemGroup()
        {
            epItemGroup.Clear();
            string errorMessage = string.Empty;            
            m_lstItemGroup = m_objItemGroup.SearchItemGroup(1,0,txtGroupName.Text, Convert.ToInt32(cmbStatus.SelectedValue),-1, ref errorMessage);
        }

        /// <summary>
        /// Set  DataSource Of DataGridView
        /// </summary>
        private void SetDGVDataSource()
        {
            m_dgvRowSelection = false;
            if (m_lstItemGroup.Count > 0)
                dgvItemGroup.DataSource = m_lstItemGroup;
            else
            {
                dgvItemGroup.DataSource = new List<ItemGroup>();
                MessageBox.Show(Common.GetMessage("8002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            dgvItemGroup.ClearSelection();
            m_dgvRowSelection = true;
        }

        /// <summary>
        /// Edit Item Group Details
        /// </summary>
        private void EditItemGroup(bool yesNo)
        {
            if (m_dgvRowSelection == true)
            {
                string errorMessage = string.Empty;
                int index = dgvItemGroup.CurrentRow.Index;
                m_objItemGroup = m_lstItemGroup[index];
                m_lstItem = new List<ItemGroup>();
                m_lstItem = m_objItemGroup.SearchItemGroup(2, m_objItemGroup.GroupItemId, "", -1, -1, ref errorMessage);
                BindItemGroupObject();
                
            }
            if (yesNo)
            {
                btnSearch.Enabled = false;
                btnSave.Enabled = m_isSaveAvailable;
            }
            else
            {   
                btnSearch.Enabled = m_isSearchAvailable;
                btnSave.Enabled = false;
            }
        }

        /// <summary>
        /// Bind TextBox controls and Selected Tree with selected ItemGroup
        /// </summary>
        private void BindItemGroupObject()
        {
            txtGroupName.Text = m_objItemGroup.GroupName;
            cmbStatus.SelectedValue = m_objItemGroup.Status;
            m_lstSelectedItems.Clear();
            for (int i = 0; i < m_lstItem.Count; i++)
            {
                ItemWithSubCategory objIWS = new ItemWithSubCategory();
                objIWS.ItemId = m_lstItem[i].ItemId;
                objIWS.ItemName = m_lstItem[i].ItemName;
                objIWS.MHSubCategoryId = m_lstItem[i].MHSubCategoryId;
                objIWS.MHSubCategoryName = m_lstItem[i].MHSubCategoryName;
                m_lstSelectedItems.Add(objIWS);
            }
            tvSelectedItems.Nodes.Clear();
            BindItemList(m_lstSelectedItems, tvSelectedItems);
            
            InitializeItemList();            
            RemoveEmptyParents(tvAllItems);
        }

        /// <summary>
        /// Reset All Form Controls
        /// </summary>
        private void ResetForm()
        {
            ResetControls();
            m_dgvRowSelection = false;
            dgvItemGroup.DataSource = new List<ItemGroup>();
            m_dgvRowSelection = true;
        }

        /// <summary>
        /// Reset All controls except DGV. used in save method
        /// </summary>
        private void ResetControls()
        {
            btnSave.Enabled = m_isSaveAvailable;
            btnSearch.Enabled = m_isSearchAvailable;
            m_objItemGroup = new ItemGroup();
            txtGroupName.Text = string.Empty;
            cmbStatus.SelectedValue = -1;
            EnableDisableControls(true);           
            epItemGroup.Clear();
            txtGroupName.Focus();
            InitializeItemList();
            tvSelectedItems.Nodes.Clear();
            cmbStatus.SelectedValue = 1;
            m_lstSelectedItems.Clear();
        }

        /// <summary>
        /// Enable Disable Controls
        /// </summary>
        /// <param name="yesNo"></param>
        private void EnableDisableControls(bool yesNo)
        {
            txtGroupName.Enabled = yesNo;            
            cmbStatus.Enabled = yesNo;            
        }

        //<summary>
         //Creates ItemGroup object to Save Item Group Details
         //</summary>
        private void CreateItemGroupObject()
        {
            m_objItemGroup.ItemList = new List<ItemGroup>();
            ItemGroup objItemGroup = null;
            foreach (TreeNode tn in tvSelectedItems.Nodes)
            {
                foreach (TreeNode cn in tn.Nodes)
                {
                    objItemGroup = new ItemGroup();
                    objItemGroup.GroupItemId = m_objItemGroup.GroupItemId;
                    objItemGroup.GroupName = txtGroupName.Text;
                    objItemGroup.Quantity = 0;
                    objItemGroup.Status = Convert.ToInt32(cmbStatus.SelectedValue);
                    objItemGroup.MHSubCategoryId = Convert.ToInt32(tn.Name);
                    objItemGroup.ItemId = Convert.ToInt32(cn.Name);
                    objItemGroup.ModifiedBy = m_userId;
                    m_objItemGroup.ItemList.Add(objItemGroup);
               }
            }
        }
               
        /// <summary>
        /// Save Item group
        /// </summary> 
        private void SaveItemGroup()
        {
            if (ValidateControls())
            {
                DialogResult dr = MessageBox.Show(Common.GetMessage("INF0027"), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    string errorMessage = string.Empty;
                    CreateItemGroupObject();
                    bool result = m_objItemGroup.SaveItemGroup(ref errorMessage);
                    if (result)
                    {
                        MessageBox.Show(Common.GetMessage("8001"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        if (m_objItemGroup.ItemList[0].Status == 2)
                        {
                            ResetControls();
                        }
                        SearchItemGroup();
                        SetDGVDataSource();
                        ResetControls();
                    }
                    else
                    {
                        MessageBox.Show(Common.GetMessage(errorMessage), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        /// <summary>
        /// Validate All Controls on Save
        /// </summary>
        /// <returns></returns>
        private bool ValidateControls()
        {
            ValidateGroupName(true);            
            ValidateStatus(true);
            ValidateItems(true);
            return GetErrorMessage();
        }

        /// <summary>
        /// Get All error messages from error provider and displays a message if any error exists
        /// </summary>
        /// <returns></returns>
        private bool GetErrorMessage()
        {
            StringBuilder sb = new StringBuilder();
            if (Validators.GetErrorMessage(epItemGroup, txtGroupName).Length > 0)
            {
                sb.Append(Validators.GetErrorMessage(epItemGroup, txtGroupName));
                sb.AppendLine();
            }
            if (Validators.GetErrorMessage(epItemGroup, cmbStatus).Length > 0)
            {
                sb.Append(Validators.GetErrorMessage(epItemGroup, cmbStatus));
                sb.AppendLine();
            }
            if ((epItemGroup.GetError(tvSelectedItems)).Length > 0)
            {
                sb.Append(epItemGroup.GetError(tvSelectedItems));
                sb.AppendLine();
            }         
            if (sb.ToString().Trim().Length > 0)
            {
                sb = Common.ReturnErrorMessage(sb);
                MessageBox.Show(sb.ToString(),Common.GetMessage("10001"),MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return false;
            }
            else
                return true;
        }

        private List<ItemWithSubCategory> GetCheckedNodes(TreeView tv)
        {
            List<ItemWithSubCategory> lst = new List<ItemWithSubCategory>();
            foreach (TreeNode tNode in tv.Nodes)
            {
                foreach (TreeNode sNode in tNode.Nodes)
                {
                    if (sNode.Checked)
                    {
                        ItemWithSubCategory objIWS = new ItemWithSubCategory();
                        objIWS.ItemId = Convert.ToInt32(sNode.Name);
                        objIWS.ItemName = sNode.Text;
                        objIWS.MHSubCategoryId = Convert.ToInt32(tNode.Name);
                        objIWS.MHSubCategoryName = tNode.Text;
                        lst.Add(objIWS);
                    }
                }
            }
            return lst;
        }

        private void AddSelectedItems(TreeView fromTree, TreeView toTree, List<ItemWithSubCategory> fromList, List<ItemWithSubCategory> toList)
        {
            List<ItemWithSubCategory> lst = new List<ItemWithSubCategory>();            
            lst = GetCheckedNodes(fromTree);

            for (int i = 0; i < lst.Count; i++)
            {
                for (int j = 0; j < fromList.Count; j++)
                {
                    if (lst[i].ItemId == fromList[j].ItemId)
                    {
                        toList.Add((ItemWithSubCategory)fromList[j]);
                    }
                }
            }
            BindItemList(toList, toTree);
            RemoveSelectedRecords(fromTree,fromList);
            BindItemList(fromList, fromTree);
            RemoveEmptyParents(fromTree);
        }

        private void RemoveSelectedRecords(TreeView fromTree,List<ItemWithSubCategory> fromList)
        {
            List<ItemWithSubCategory> lst = new List<ItemWithSubCategory>();
            lst = GetCheckedNodes(fromTree);
            for (int i = 0; i < lst.Count; i++)
            {
                ItemWithSubCategory objIWS = fromList.Find(delegate(ItemWithSubCategory obj) { return obj.ItemId == lst[i].ItemId; });
                fromList.Remove(objIWS);
            }            
        }
       
        private void RemoveEmptyParents(TreeView tv)
        {
            foreach (TreeNode tn in tv.Nodes)
            {
                if (tn.Nodes.Count == 0)
                {
                    tn.Remove();
                }
            }
        }         
        
        #endregion

        #region Validations

        private void ValidateGroupName(bool yesNo)
        {
            if (yesNo)
            {
                if (Validators.CheckForEmptyString(txtGroupName.Text.Trim().Length))
                {
                    Validators.SetErrorMessage(epItemGroup, txtGroupName, "VAL0001", lblGroupName.Text);
                }
            }
            else
                epItemGroup.SetError(txtGroupName, String.Empty);
        }        

        private void ValidateStatus(bool yesNo)
        {
            if (yesNo)
            {
                if (Convert.ToInt32(cmbStatus.SelectedValue) == -1)
                {
                    Validators.SetErrorMessage(epItemGroup, cmbStatus, "VAL0002",lblStatus.Text);
                }
            }
            else
                epItemGroup.SetError(cmbStatus, String.Empty);
        }

        private void ValidateItems(bool yesNo)
        {
            if (yesNo)
            {
                if (tvSelectedItems.Nodes.Count<1)
                {
                    epItemGroup.SetError(tvSelectedItems, Common.GetMessage("VAL0002", lblSelectableItems.Text.Substring(0, lblSelectableItems.Text.Length - 1)));
                    epItemGroup.SetIconAlignment(tvSelectedItems, ErrorIconAlignment.TopRight);
                }
                else
                    epItemGroup.SetError(tvSelectedItems, String.Empty);
            }
            else
                epItemGroup.SetError(tvSelectedItems, String.Empty);
        }

        #endregion
        
        #region Events
        
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                SearchItemGroup();
                SetDGVDataSource();                
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }       

        private void dgvItemGroup_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (m_dgvRowSelection)
                {
                    EditItemGroup(false);
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }     

        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                ResetForm();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {                
                SaveItemGroup();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void txtGroupName_Validated(object sender, EventArgs e)
        {
            try
            {
                ValidateGroupName(false);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }     

        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ValidateStatus(false);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddItems_Click(object sender, EventArgs e)
        {
            try
            {
               AddSelectedItems(tvAllItems, tvSelectedItems,m_lstAllItem,m_lstSelectedItems);               
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRemoveItems_Click(object sender, EventArgs e)
        {
            try
            {
                AddSelectedItems(tvSelectedItems, tvAllItems,m_lstSelectedItems, m_lstAllItem);                
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tvAllItems_AfterCheck(object sender, TreeViewEventArgs e)
        {
            try
            {
                if (bChildTrigger)
                {
                    CheckAllChildren(e.Node, e.Node.Checked);
                }
                if (bParentTrigger)
                {
                    CheckMyParent(e.Node, e.Node.Checked);
                }               
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void tvSelectedItems_AfterCheck(object sender, TreeViewEventArgs e)
        {
            try
            {
                if (bChildTrigger)
                {
                    CheckAllChildren(e.Node, e.Node.Checked);
                }
                if (bParentTrigger)
                {
                    CheckMyParent(e.Node, e.Node.Checked);
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void dgvItemGroup_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1 && (dgvItemGroup.Rows[e.RowIndex].Cells[e.ColumnIndex].GetType() == typeof(DataGridViewImageCell)))
                    EditItemGroup(true);
                if (e.RowIndex > -1 && (dgvItemGroup.Rows[e.RowIndex].Cells[e.ColumnIndex].GetType() != typeof(DataGridViewImageCell)))
                    EditItemGroup(false);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion 
        
        #region TreeView Check/UnCheck Code

        bool bChildTrigger = true;
        bool bParentTrigger = true;

        void CheckAllChildren(TreeNode tn, bool bCheck)
        {
            bParentTrigger = false;
            foreach (TreeNode ctn in tn.Nodes)
            {
                bChildTrigger = false;
                ctn.Checked = bCheck;
                bChildTrigger = true;
                CheckAllChildren(ctn, bCheck);
            }
            bParentTrigger = true;
        }

        void CheckMyParent(TreeNode tn, bool bCheck)
        {
            if (tn == null) return;
            if (tn.Parent == null) return;
            bChildTrigger = false;
            bParentTrigger = false;
            int i = 0;
            foreach (TreeNode cn in tn.Parent.Nodes)
            {
                if (cn.Checked)
                    i++;                
            }
            if (i == tn.Parent.Nodes.Count)
                tn.Parent.Checked = true;
            else
                tn.Parent.Checked = false;

            CheckMyParent(tn.Parent, bCheck);
            bParentTrigger = true;
            bChildTrigger = true;
        }
        

        #endregion TreeView Check/UnCheck Code
       
    }
}
