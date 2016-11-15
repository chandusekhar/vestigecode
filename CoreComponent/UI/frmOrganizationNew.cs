using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CoreComponent.UI; 
using CoreComponent.BusinessObjects;
using CoreComponent.Core.BusinessObjects;
using CoreComponent.Core.UI;
using CoreComponent.Hierarchies.BusinessObjects;
using AuthenticationComponent.BusinessObjects;

namespace CoreComponent.Hierarchies.UI
{
    public partial class frmOrganizationNew : HierarchyTemplate
    {
        #region Variables
        List<OrganizationalHierarchy> m_orgList;
        OrganizationalHierarchy m_orgHierarchy;
        public int m_selectedParentId = Common.INT_DBNULL;
        string m_modifiedDate;
        List<State> m_lstZoneState;

        private Boolean m_isSaveAvailable = false;
        private Boolean m_isSearchAvailable = false;

        private int m_userId = Authenticate.LoggedInUser.UserId;
        private string strUserName = Authenticate.LoggedInUser.UserName;

        private string strLocationCode = Common.LocationCode;
        private int m_currentLocationId = Common.CurrentLocationId;
        private int m_locationType = Common.CurrentLocationTypeId;

        #endregion Variables

        #region C'tor
        /// <summary>
        /// Constructor
        /// </summary>
        public frmOrganizationNew()
        {
            try
            {
                lblPageTitle.Text = "Organization Hierarchy";

                m_isSaveAvailable = Authenticate.IsFunctionAccessible(strUserName, strLocationCode, OrganizationalHierarchy.MODULE_CODE, Common.FUNCTIONCODE_SAVE);
                m_isSearchAvailable = Authenticate.IsFunctionAccessible(strUserName, strLocationCode, OrganizationalHierarchy.MODULE_CODE, Common.FUNCTIONCODE_SEARCH);

                InitializeComponent();
                InitializeControls();

                dgvOrgSearch.AutoGenerateColumns = false;
                dgvOrgSearch.DataSource = null;
                DataGridView dgvOrgSearchNew = Common.GetDataGridViewColumns(dgvOrgSearch, Environment.CurrentDirectory + "\\App_Data\\GridViewDefinition.xml");

                ResetControls();
                cmbType.Focus();
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
        /// Initialize Controls
        /// </summary>
        private void InitializeControls()
        {
            txtParentName.ReadOnly = true;
            DataTable dt = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter("STATUS", 0, 0, 0));
            cmbStatus.DataSource = dt;
            cmbStatus.DisplayMember = Common.KEYVALUE1;
            cmbStatus.ValueMember = Common.KEYCODE1;

            CoreComponent.Hierarchies.BusinessObjects.OrganizationalHierarchy org = new CoreComponent.Hierarchies.BusinessObjects.OrganizationalHierarchy();

            m_orgList = org.ConfigSearch();

            CoreComponent.Hierarchies.BusinessObjects.OrganizationalHierarchy orgItemSelect = new CoreComponent.Hierarchies.BusinessObjects.OrganizationalHierarchy();
            orgItemSelect.HierarchyLevel = Common.INT_DBNULL;
            orgItemSelect.HierarchyName = Common.SELECT_ONE;
            m_orgList.Add(orgItemSelect);

            string hierarchyLevel = "HierarchyLevel Asc";
            m_orgList.Sort((new GenericComparer<BusinessObjects.OrganizationalHierarchy>(hierarchyLevel.Split(' ')[0], hierarchyLevel.Split(' ')[1] == "Asc" ? SortDirection.Ascending : SortDirection.Descending)).Compare);

            cmbType.DataSource = m_orgList;
            cmbType.DisplayMember = Common.HIERARCHY_NAME;
            cmbType.ValueMember = Common.HIERARCHY_LEVEL;

        }

        /// <summary>
        /// To search records
        /// </summary>
        private void Search()
        {
            CoreComponent.Hierarchies.BusinessObjects.OrganizationalHierarchy org = new CoreComponent.Hierarchies.BusinessObjects.OrganizationalHierarchy();

            int parentId = Common.INT_DBNULL;
            org.HierarchyCode = DBNull.Value.ToString();
            org.HierarchyName = DBNull.Value.ToString();
            org.ParentHierarchyName = DBNull.Value.ToString();

            parentId = m_selectedParentId == Common.INT_DBNULL ? org.ParentHierarchyId : m_selectedParentId;

            org.HierarchyCode = txtCode.Text.Trim();
            org.HierarchyName = txtName.Text.Trim();
            org.ParentHierarchyName = txtName.Text.Trim();
            org.ParentHierarchyId = parentId;
            org.HierarchyType = Convert.ToInt32(cmbType.SelectedValue.ToString());
            org.Status = Convert.ToInt32(cmbStatus.SelectedValue.ToString());

            m_orgList = org.Search();

            if ((m_orgList != null) && (m_orgList.Count > 0))
                dgvOrgSearch.DataSource = m_orgList;
            else
            {
                dgvOrgSearch.DataSource = new List<OrganizationalHierarchy>();
                MessageBox.Show(Common.GetMessage("8002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Compare parent level and current level 
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="currentLevel"></param>
        /// <returns></returns>
        private bool CheckParentLevel(int parentId, int currentLevel)
        {
            CoreComponent.Hierarchies.BusinessObjects.OrganizationalHierarchy org = new CoreComponent.Hierarchies.BusinessObjects.OrganizationalHierarchy();

            org.HierarchyCode = DBNull.Value.ToString();
            org.HierarchyName = DBNull.Value.ToString();
            org.ParentHierarchyName = DBNull.Value.ToString();
            org.ParentHierarchyId = Common.INT_DBNULL;
            org.HierarchyType = Common.INT_DBNULL; ;
            org.Status = Common.INT_DBNULL; ;

            List<OrganizationalHierarchy> orgList;
            orgList = org.Search();

            int parentLevel = (from p in orgList where p.HierarchyId == parentId select p.HierarchyLevel).Max();

            if ((currentLevel - Convert.ToInt32(parentLevel) - 1) != 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// Save Organization
        /// </summary>
        private void Save()
        {
            ValidateType(false);
            ValidateCode(false);
            ValidateName(false);
            ValidateStatus(false);

            #region "Error Validator"
            StringBuilder sbError = new StringBuilder();
            if (errOrganization.GetError(cmbType).Length > 0)
            {
                sbError.Append(errOrganization.GetError(cmbType));
                sbError.AppendLine();
            }
            if (errOrganization.GetError(txtCode).Length > 0)
            {
                sbError.Append(errOrganization.GetError(txtCode));
                sbError.AppendLine();
            }
            if (errOrganization.GetError(txtName).Length > 0)
            {
                sbError.Append(errOrganization.GetError(txtName));
                sbError.AppendLine();
            }
            if (errOrganization.GetError(cmbStatus).Length > 0)
            {
                sbError.Append(errOrganization.GetError(cmbStatus));
                sbError.AppendLine();
            }

            string errMsg = CheckForValidLocations();
            if (!string.IsNullOrEmpty(errMsg))
            {
                sbError.Append(errMsg);
                sbError.AppendLine();
            }

            sbError = Common.ReturnErrorMessage(sbError);
            #endregion "Error Validator"

            if (!sbError.ToString().Trim().Equals(string.Empty))
            {
                MessageBox.Show(sbError.ToString(), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (cmbType.SelectedIndex > 1 & m_orgHierarchy == null & txtParentName.Text.Trim().Length == 0)
            {
                MessageBox.Show(Common.GetMessage("INF0019", lblSearchParentOrgName.Text.Trim().Substring(0, lblSearchParentOrgName.Text.Trim().Length - 1)), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (errOrganization.GetError(txtCode).Length == 0
              & errOrganization.GetError(txtName).Length == 0
              & errOrganization.GetError(cmbType).Length == 0
              & errOrganization.GetError(cmbStatus).Length == 0)
            {
                DialogResult saveResult = MessageBox.Show(Common.GetMessage("INF0027"), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (saveResult == DialogResult.Yes)
                {
                    int parentId = Common.INT_DBNULL;
                    int hierarchyId = Common.INT_DBNULL;

                    if ((m_orgHierarchy != null) && (cmbType.SelectedIndex > 0))
                        parentId = m_selectedParentId == 0 ? m_orgHierarchy.ParentHierarchyId : m_selectedParentId;
                    else if (m_orgHierarchy == null & cmbType.SelectedIndex > 0)
                        parentId = m_selectedParentId;
                    else
                        parentId = Common.INT_DBNULL;

                    if (m_orgHierarchy != null)
                        hierarchyId = m_orgHierarchy.HierarchyId;

                    if ((hierarchyId == Common.INT_DBNULL) && (Convert.ToInt32(cmbStatus.SelectedValue) == 2))
                    {
                        MessageBox.Show(Common.GetMessage("VAL0020"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (parentId != Common.INT_DBNULL)
                    {
                        //Check Parent Level 
                        bool checkParent = CheckParentLevel(parentId, Convert.ToInt32(cmbType.SelectedValue));
                        if (checkParent == false)
                        {
                            MessageBox.Show(Common.GetMessage("VAL0006", "parent"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    OrganizationalHierarchy orgHierarchy = new OrganizationalHierarchy();
                    orgHierarchy.ModifiedBy = m_userId;
                    orgHierarchy.HierarchyCode = txtCode.Text.Trim();
                    orgHierarchy.HierarchyName = txtName.Text.Trim();
                    orgHierarchy.ParentHierarchyId = parentId;
                    orgHierarchy.Description = txtDescription.Text.Trim();
                    orgHierarchy.HierarchyLevel = Convert.ToInt32(cmbType.SelectedValue);
                    orgHierarchy.Status = Convert.ToInt32(cmbStatus.SelectedValue);
                    orgHierarchy.StatusValue = cmbStatus.Text.Trim();
                    orgHierarchy.HierarchyId = hierarchyId;
                    orgHierarchy.LstState = m_lstZoneState;

                    if (m_orgHierarchy != null)
                        orgHierarchy.ModifiedDate = m_modifiedDate.ToString();

                    string errorMesage = string.Empty;
                    bool recordSaved = orgHierarchy.Save(ref errorMesage);

                    if (errorMesage.Equals(string.Empty))
                    {
                        ResetControls();
                        Search();
                        btnSearch.Enabled = m_isSearchAvailable;
                        MessageBox.Show(Common.GetMessage("8001"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (errorMesage.Equals("INF0020"))
                        MessageBox.Show(Common.GetMessage(errorMesage, lblSearchOrgCode.Text.Trim().Substring(0, lblSearchOrgCode.Text.Trim().Length - 2), lblSearchOrgName.Text.Trim().Substring(0, lblSearchOrgName.Text.Trim().Length - 2)), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                        MessageBox.Show(Common.GetMessage(errorMesage), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Returns error-message based on valid-location check
        /// </summary>
        /// <returns>Error-Message; in case of no error, an empty string is returned</returns>
        private string CheckForValidLocations()
        {
            string errMsg = string.Empty;

            if (Convert.ToInt32(cmbType.SelectedValue) == (int)Common.ORGSTATE.ZONE)
            {
                if (m_lstZoneState == null)
                {
                    errMsg = Common.GetMessage("VAL0024", "State");
                }
                else if (m_lstZoneState.Count == 0)
                {
                    errMsg = Common.GetMessage("VAL0024", "State");
                }
                else
                {
                    int count = (from p in m_lstZoneState
                                 where p.Status == 1
                                 select p).Count();
                    if (count == 0)
                    {
                        errMsg = Common.GetMessage("VAL0024", "Active-State");
                    }
                }
            }

            return errMsg;
        }

        /// <summary>
        /// This function is used to show data in controls
        /// </summary>
        /// <param name="e"></param>
        private void SelectGridRow(int rowIndex)
        {
            if (rowIndex >= 0)
            {
                txtCode.Text = dgvOrgSearch.Rows[rowIndex].Cells["Code"].Value.ToString().Trim();
                cmbType.SelectedText = dgvOrgSearch.Rows[rowIndex].Cells["Type"].Value.ToString();
                txtParentName.Text = dgvOrgSearch.Rows[rowIndex].Cells["ParentName"].Value.ToString().Trim();
                txtDescription.Text = dgvOrgSearch.Rows[rowIndex].Cells["Description"].Value.ToString().Trim();
                txtName.Text = dgvOrgSearch.Rows[rowIndex].Cells["Name"].Value.ToString().Trim();

                cmbType.Enabled = false;
                //Get ParentId
                var orgSelect = (from p in m_orgList where p.HierarchyCode.Trim() == txtCode.Text.Trim() select p);
                m_orgHierarchy = orgSelect.ToList()[0];
                m_selectedParentId = Convert.ToInt32(m_orgHierarchy.ParentHierarchyId);
                cmbStatus.SelectedValue = Convert.ToInt32(m_orgHierarchy.Status);
                cmbType.SelectedValue = Convert.ToInt32(m_orgHierarchy.HierarchyLevel);
                m_modifiedDate = m_orgHierarchy.ModifiedDate.ToString();

                //btnSave.Enabled = m_isSaveAvailable;
                //btnSearch.Enabled = false;
                
                m_lstZoneState = m_orgHierarchy.ZoneStateSearch(m_orgHierarchy.HierarchyId);

                //if (cmbType.SelectedIndex > 0 && Convert.ToInt32(cmbType.SelectedValue) == Convert.ToInt32(Common.ORGSTATE.ZONE))
                //    m_lstZoneState = m_orgHierarchy.ZoneStateSearch(m_orgHierarchy.HierarchyId);
            }
        }

        /// <summary>
        /// Validate Organization Code
        /// </summary>
        private void ValidateCode(Boolean yesNO)
        {
            bool isTextBoxEmpty = CoreComponent.Core.BusinessObjects.Validators.CheckForEmptyString(txtCode.Text.Length);
            if (isTextBoxEmpty == true && yesNO == false)
                errOrganization.SetError(txtCode, Common.GetMessage("INF0019", lblSearchOrgCode.Text.Trim().Substring(0, lblSearchOrgCode.Text.Trim().Length - 2)));
            else if (isTextBoxEmpty == false && yesNO == false)
            {
                errOrganization.SetError(txtCode, Common.CodeValidate(txtCode.Text, lblSearchOrgCode.Text.Trim().Substring(0, lblSearchOrgCode.Text.Trim().Length - 2)));
                //errOrganization.SetError(txtCode, Common.GetMessage("INF0019", lblSearchOrgCode.Text.Trim().Substring(0, lblSearchOrgCode.Text.Trim().Length - 2)));
            }
            else if (isTextBoxEmpty == false)
                errOrganization.SetError(txtCode, "");
        }

        /// <summary>
        /// Validate Organization Status
        /// </summary>
        private void ValidateStatus(Boolean yesNO)
        {
            if (cmbStatus.SelectedIndex == 0 && yesNO == false)
                errOrganization.SetError(cmbStatus, Common.GetMessage("INF0026", lblSearchStatus.Text.Trim().Substring(0, lblSearchStatus.Text.Trim().Length - 2)));
            else
                errOrganization.SetError(cmbStatus, "");
        }

        /// <summary>
        /// Validate Organization Name
        /// </summary>
        private void ValidateName(Boolean yesNO)
        {
            bool isTextBoxEmpty = CoreComponent.Core.BusinessObjects.Validators.CheckForEmptyString(txtName.Text.Length);
            if (isTextBoxEmpty == true && yesNO==false)
                errOrganization.SetError(txtName, Common.GetMessage("INF0019", lblSearchOrgName.Text.Trim().Substring(0, lblSearchOrgName.Text.Trim().Length - 2)));
            else //if (isTextBoxEmpty == false)
                errOrganization.SetError(txtName, "");
        }

        /// <summary>
        /// Validate Organization Type
        /// </summary>
        private void ValidateType(Boolean yesNO)
        {
            if (cmbType.SelectedIndex == 0 && yesNO == false)
                errOrganization.SetError(cmbType, Common.GetMessage("INF0026", lblOrganizationType.Text.Trim().Substring(0, lblOrganizationType.Text.Trim().Length - 3)));
            else
                errOrganization.SetError(cmbType, "");

            if (cmbType.SelectedIndex>0 && Convert.ToInt32(cmbType.SelectedValue) == Convert.ToInt32(Common.ORGSTATE.HO))
                btnShowTree.Visible  = false;
            else
                btnShowTree.Visible = true;

            if (cmbType.SelectedIndex > 0 && Convert.ToInt32(cmbType.SelectedValue) == Convert.ToInt32(Common.ORGSTATE.ZONE))
                btnZoneState.Visible = true;
            else
                btnZoneState.Visible = false;
        }

        /// <summary>
        /// Reset Controls
        /// </summary>
        private void ResetControls()
        {
            btnSearch.Enabled = m_isSearchAvailable;
            btnSave.Enabled = m_isSaveAvailable;
            cmbType.Focus();
            cmbType.Enabled = true;
            m_orgHierarchy = null;
            m_lstZoneState = null;
            m_selectedParentId = Common.INT_DBNULL;
            dgvOrgSearch.DataSource = null;
            dgvOrgSearch.DataSource = new List<OrganizationalHierarchy>();
            new VisitControls().ResetAllControlsInPanel(errOrganization, pnlSearchHeader);
            cmbStatus.SelectedValue = 1;
        }
        
        void Method_State_Add(object sender, ZoneStateEventArgs e)
        {
            m_lstZoneState = e.StateList;
        }

        #endregion

        #region Events

        /// <summary>
        /// Show organization structure in hierarchy window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnShowState_Click(object sender, EventArgs e)
        {
            try
            {
                int hierarchyId = Common.INT_DBNULL;
                if (m_orgHierarchy!= null)
                    hierarchyId = m_orgHierarchy.HierarchyId;

                frmOrganizationZone objFrmOrganization = new frmOrganizationZone(hierarchyId, m_lstZoneState);

                (objFrmOrganization as frmOrganizationZone).ZoneStateHandler += new ZoneStateHandler(Method_State_Add);
                DialogResult dResult = objFrmOrganization.ShowDialog();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Show organization structure in hierarchy window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnShowTree_Click(object sender, EventArgs e)
        {
            try
            {
                frmTree objTree = new frmTree("Organizational", "", Convert.ToInt32(cmbType.SelectedValue), this);
                Point pointTree = new Point();
                pointTree = this.PointToScreen(txtParentName.Location);
                pointTree.Y = pointTree.Y + Common.TREE_COMP_Y;
                pointTree.X = pointTree.X + Common.TREE_COMP_X;
                objTree.Location = pointTree;
                objTree.ShowDialog();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Call function SelectGridRow, when user select a row
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvOrgSearch_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                SelectGridRow(e.RowIndex);
                btnSave.Enabled = false;
                btnSearch.Enabled = m_isSearchAvailable;
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Call function SelectGridRow, when user select a row
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvOrgSearch_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                SelectGridRow(e.RowIndex);
                btnSave.Enabled = false;
                btnSearch.Enabled = m_isSearchAvailable;
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Call Search function
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                errOrganization.Clear();
                Search();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Used to Save Records
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Save();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// fn. to call Reset Controls
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                ResetControls();
                cmbType.Focus();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Call fn. to Validate Organization Code
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCode_Validated(object sender, EventArgs e)
        {
            try
            {
                ValidateCode(true);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Call fn. to Validate Organization Name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtName_Validated(object sender, EventArgs e)
        {
            try
            {
                ValidateName(true);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Call fn. to Validate Organization Type
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ValidateType(true);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Call fn. to Validate Organization Status
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ValidateStatus(true);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }        

        private void txtParentName_Validated(object sender, EventArgs e)
        {
            // try
            // {
            //  if (cmbType.SelectedIndex >1 &  m_orgHierarchy==null)
            //    errOrganization.SetError(txtParentName, Common.GetMessage("INF0019", lblSearchParentOrgName.Text));
            //  else
            //    errOrganization.SetError(txtParentName, "");
            // }
            // catch (Exception ex)
            //{
            //    Common.LogException(ex);
            //    MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }
        #endregion

        private void dgvOrgSearch_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                DataGridView ctrl = (DataGridView)sender;
                if (ctrl.SelectedRows.Count > 0)
                {
                    if (ctrl.SelectedRows[0].Index > Common.INT_DBNULL)
                    {
                        SelectGridRow(ctrl.SelectedRows[0].Index);
                        btnSearch.Enabled = m_isSearchAvailable;
                        btnSave.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvOrgSearch_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.RowIndex >= 0) && (dgvOrgSearch.Rows[e.RowIndex].Cells[e.ColumnIndex].GetType() == typeof(DataGridViewImageCell)))
            {
                SelectGridRow(e.RowIndex);
                btnSave.Enabled = m_isSaveAvailable;
                btnSearch.Enabled = false;
            }
        }

    }
}
