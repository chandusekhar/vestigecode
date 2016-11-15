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

/*
 ------------------------------------------------------------------------
 * Created by               :   Harsh Gupta
 * Created on               :   17 June 2009
 * Remarks                  :   Form to search, create and update
 *                              Merchandise Hierarchy records
 *                              
 * Modified by              :   
 * Modified on              :   
 * Remarks                  :
 ------------------------------------------------------------------------
 */
namespace CoreComponent.Hierarchies.UI
{
    public partial class frmMerchandiseNew : HierarchyTemplate
    {
        #region Form Level variables

        public int m_selectedParentId = Common.INT_DBNULL;
        public List<MerchandiseHierarchy> m_MerchList = null;
        public MerchandiseHierarchy m_MerchHierarchy = null;
        public string m_ModifiedDate = string.Empty;
        
        #region Authorization Check

        private Boolean m_isSaveAvailable = false;
        private Boolean m_isSearchAvailable = false;

        private int m_userId = Authenticate.LoggedInUser.UserId;
        private string strUserName = Authenticate.LoggedInUser.UserName;

        private string strLocationCode = Common.LocationCode;
        private int m_currentLocationId = Common.CurrentLocationId;
        private int m_locationType = Common.CurrentLocationTypeId;
        #endregion

        #endregion

        #region Constructor
        
        public frmMerchandiseNew()
        {
            try
            {
                lblPageTitle.Text = "Merchandise Hierarchy";

                m_isSaveAvailable = Authenticate.IsFunctionAccessible(strUserName, strLocationCode, MerchandiseHierarchy.MODULE_CODE, Common.FUNCTIONCODE_SAVE);
                m_isSearchAvailable = Authenticate.IsFunctionAccessible(strUserName, strLocationCode, MerchandiseHierarchy.MODULE_CODE, Common.FUNCTIONCODE_SEARCH);

                InitializeComponent();
                InitializeControls();

                LoadGridColumns();
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
        /// To prepare control(s) before using
        /// </summary>
        private void InitializeControls()
        {
            txtParentName.ReadOnly = true;

            DataTable datatableStatus = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter(Common.STATUS, 0, 0, 0));
            cmbStatus.DataSource = datatableStatus;
            cmbStatus.ValueMember = Common.KEYCODE1;
            cmbStatus.DisplayMember = Common.KEYVALUE1;
            cmbStatus.SelectedValue = 1;

            MerchandiseHierarchy merchHier = new MerchandiseHierarchy();
            List<MerchandiseHierarchy> merchConfigList = new List<MerchandiseHierarchy>();

            merchConfigList = merchHier.ConfigSearch();

            MerchandiseHierarchy merchItemSelect = new MerchandiseHierarchy();
            merchItemSelect.HierarchyLevel = Common.INT_DBNULL;
            merchItemSelect.HierarchyName = Common.SELECT_ONE;
            merchConfigList.Add(merchItemSelect);

            string hierarchyLevel = "HierarchyLevel ASC";
            merchConfigList.Sort((new CoreComponent.Core.BusinessObjects.GenericComparer<MerchandiseHierarchy>(hierarchyLevel.Split(' ')[0], hierarchyLevel.Split(' ')[1] == "ASC" ? SortDirection.Ascending : SortDirection.Descending)).Compare);

            cmbType.DataSource = merchConfigList;
            cmbType.DisplayMember = Common.HIERARCHY_NAME;
            cmbType.ValueMember = Common.HIERARCHY_LEVEL;

            ResetControls(pnlSearchHeader);
        }


        /// <summary>
        /// Save Merchandise Data to DB (both Insertion and Updation)
        /// </summary>
        private void SaveMerchandise()
        {
            try
            {
                CodeValidate(false);
                NameValidate(false);
                TypeValidate(false);
                StatusValidate(false);

                //txtCode_Validated(this, new EventArgs());
                //txtName_Validated(this, new EventArgs());
                //cmbType_SelectedIndexChanged(this, new EventArgs());
                //cmbStatus_SelectedIndexChanged(this, new EventArgs());

                if (cmbType.SelectedIndex > 1 & m_MerchHierarchy == null & txtParentName.Text.Trim().Length == 0)
                {
                    MessageBox.Show(Common.GetMessage("INF0019", lblParentName.Text.Substring(0, lblParentName.Text.Length - 2)));
                    return;
                }

                StringBuilder sbError = new StringBuilder();
                if (epMerchandise.GetError(cmbType).Trim().Length > 0)
                {
                    sbError.Append(Validators.GetErrorMessage(epMerchandise, cmbType));
                    sbError.AppendLine();
                }
                if (epMerchandise.GetError(txtCode).Trim().Length > 0)
                {
                    sbError.Append(Validators.GetErrorMessage(epMerchandise, txtCode));
                    sbError.AppendLine();
                }
                if (epMerchandise.GetError(txtName).Trim().Length > 0)
                {
                    sbError.Append(Validators.GetErrorMessage(epMerchandise, txtName));
                    sbError.AppendLine();
                }
                if (epMerchandise.GetError(cmbStatus).Trim().Length > 0)
                {
                    sbError.Append(Validators.GetErrorMessage(epMerchandise, cmbStatus));
                    sbError.AppendLine();
                }
                sbError = Common.ReturnErrorMessage(sbError);
                if (!sbError.ToString().Equals(string.Empty))
                {
                    MessageBox.Show(sbError.ToString(), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (sbError.ToString().Replace(Environment.NewLine, string.Empty).Trim().Equals(string.Empty))
                {
                    DialogResult saveResult = MessageBox.Show(Common.GetMessage("INF0027"), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (saveResult == DialogResult.Yes)
                    {
                        int parentId = Common.INT_DBNULL;
                        int hierarchyId = Common.INT_DBNULL;

                        if ((m_MerchHierarchy != null) && (cmbType.SelectedIndex > 0))
                            parentId = m_selectedParentId == 0 ? m_MerchHierarchy.ParentHierarchyId : m_selectedParentId;
                        else if (m_MerchHierarchy == null & cmbType.SelectedIndex > 0)
                            parentId = m_selectedParentId;
                        else
                            parentId = Common.INT_DBNULL;

                        if (m_MerchHierarchy != null)
                            hierarchyId = m_MerchHierarchy.HierarchyId;


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

                        if (hierarchyId == Common.INT_DBNULL && Convert.ToInt32(cmbStatus.SelectedValue) == 2)
                        {
                            MessageBox.Show(Common.GetMessage("VAL0020"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        MerchandiseHierarchy merchHierarchy = new MerchandiseHierarchy();
                        merchHierarchy.HierarchyCode = txtCode.Text.Trim();
                        merchHierarchy.HierarchyName = txtName.Text.Trim();
                        merchHierarchy.ParentHierarchyId = parentId;
                        merchHierarchy.Description = txtDesc.Text.Trim();
                        merchHierarchy.HierarchyLevel = Convert.ToInt32(cmbType.SelectedValue);
                        merchHierarchy.Status = Convert.ToInt32(cmbStatus.SelectedValue);
                        merchHierarchy.ModifiedBy = m_userId;
                        merchHierarchy.IsTradable = chkIsTradeable.Checked;
                        merchHierarchy.HierarchyId = hierarchyId;

                        if (m_MerchHierarchy != null)
                            merchHierarchy.ModifiedDate = m_ModifiedDate.ToString();

                        string errorMesage = string.Empty;
                        bool recordSaved = merchHierarchy.Save(ref errorMesage);

                        if (errorMesage.Equals(string.Empty))
                        {
                            ResetControls(pnlSearchHeader);
                            SearchMerchandise();
                            MessageBox.Show(Common.GetMessage("8001"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else if (errorMesage.Equals("INF0020"))
                            MessageBox.Show(Common.GetMessage(errorMesage, lblCode.Text.Trim(), lblName.Text.Trim()), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        else
                            MessageBox.Show(Common.GetMessage(errorMesage), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);

                        btnSearch.Enabled = m_isSaveAvailable;
                        cmbType.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Resets all controls on searchable panel
        /// </summary>
        /// <param name="currentPanel"></param>
        private void ResetControls(Panel currentPanel)
        {
            try
            {
                m_MerchHierarchy = null;
                m_selectedParentId = Common.INT_DBNULL;
                new VisitControls().ResetCurrentGrid(currentPanel, dgvMerchandise, epMerchandise);
                cmbStatus.SelectedValue = 1;
                btnSave.Enabled = m_isSaveAvailable;
                btnSearch.Enabled = m_isSearchAvailable;
                cmbType.Enabled = true;
                cmbType.Focus();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
      
        /// <summary>
        /// Search records for Merchandise Hierarchy
        /// </summary>
        private void SearchMerchandise()
        {
            try
            {

                dgvMerchandise.SelectionChanged -= dgvMerchandise_SelectionChanged;
                MerchandiseHierarchy merch = new MerchandiseHierarchy();

                int parentId = Common.INT_DBNULL;

                parentId = m_selectedParentId == Common.INT_DBNULL ? merch.ParentHierarchyId : m_selectedParentId;

                merch.HierarchyType = cmbType.SelectedIndex > 0 ? Convert.ToInt32(cmbType.SelectedValue) : Common.INT_DBNULL;
                merch.HierarchyCode = txtCode.Text.Trim().Length > 0 ? txtCode.Text.Trim() : Common.DBNULL_VAL;
                merch.HierarchyName = txtName.Text.Trim().Length > 0 ? txtName.Text.Trim() : Common.DBNULL_VAL;
                merch.ParentHierarchyName = txtParentName.Text.Trim().Length > 0 ? txtParentName.Text.Trim() : Common.DBNULL_VAL;
                merch.ParentHierarchyId = parentId;
                merch.Status = cmbStatus.SelectedIndex > 0 ? Convert.ToInt32(cmbStatus.SelectedValue) : Common.INT_DBNULL;

                m_MerchList = merch.Search();

                if ((m_MerchList != null) && (m_MerchList.Count > 0))
                    dgvMerchandise.DataSource = m_MerchList;
                else
                {
                    
                    dgvMerchandise.DataSource = new List<MerchandiseHierarchy>();
                    MessageBox.Show(Common.GetMessage("8002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                dgvMerchandise.ClearSelection();
//                ResetControls(pnlSearchHeader);
                dgvMerchandise.SelectionChanged += new EventHandler(dgvMerchandise_SelectionChanged);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// To check if Parent selected for Updation or Insertion is in accordance with Type selected
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="currentLevel"></param>
        /// <returns></returns>
        private bool CheckParentLevel(int parentId, int currentLevel)
        {
            try
            {
                MerchandiseHierarchy merch = new MerchandiseHierarchy();

                merch.HierarchyCode = DBNull.Value.ToString();
                merch.HierarchyName = DBNull.Value.ToString();
                merch.ParentHierarchyName = DBNull.Value.ToString();
                merch.ParentHierarchyId = Common.INT_DBNULL;
                merch.HierarchyType = Common.INT_DBNULL; ;
                merch.Status = Common.INT_DBNULL; ;

                List<MerchandiseHierarchy> merchList;
                merchList = merch.Search();

                int parentLevel = (from p in merchList where p.HierarchyId == parentId select p.HierarchyLevel).Max();

                if ((currentLevel - Convert.ToInt32(parentLevel) - 1) != 0)
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Loading Metadata info for DataGridView from XML File
        /// </summary>
        private void LoadGridColumns()
        {
            try
            {
                DataGridView dgv = Common.GetDataGridViewColumns(dgvMerchandise, Environment.CurrentDirectory + "\\APP_DATA\\GridViewDefinition.xml");
                dgvMerchandise.AutoGenerateColumns = false;
                dgvMerchandise.DataSource = null;
                dgvMerchandise.AllowUserToAddRows = false;
                dgvMerchandise.AllowUserToDeleteRows = false;
                dgvMerchandise.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvMerchandise.ReadOnly = true;
                dgvMerchandise.RowHeadersVisible = false;
                dgvMerchandise.AllowUserToOrderColumns = false;
                dgvMerchandise.AllowUserToResizeColumns = false;
                dgvMerchandise.AllowUserToResizeRows = false;
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// This function is used to show data in controls
        /// </summary>
        /// <param name="e"></param>
        private void SelectGridRow(DataGridViewCellMouseEventArgs e)
        {
            try
            {
                
                txtCode.Text = dgvMerchandise.Rows[e.RowIndex].Cells["Code"].Value.ToString().Trim();
                cmbStatus.SelectedValue = Convert.ToInt32(dgvMerchandise.Rows[e.RowIndex].Cells["Status"].Value);
                cmbType.SelectedText = dgvMerchandise.Rows[e.RowIndex].Cells["Type"].Value.ToString();
                txtParentName.Text = dgvMerchandise.Rows[e.RowIndex].Cells["ParentName"].Value.ToString().Trim();
                txtDesc.Text = dgvMerchandise.Rows[e.RowIndex].Cells["Description"].Value.ToString().Trim();
                txtName.Text = dgvMerchandise.Rows[e.RowIndex].Cells["Name"].Value.ToString().Trim();
                chkIsTradeable.Checked = Convert.ToBoolean(dgvMerchandise.Rows[e.RowIndex].Cells["IsTradable"].Value);
                //Get ParentId
                var merchSelect = (from p in m_MerchList where p.HierarchyCode.Trim() == txtCode.Text.Trim() select p);
                m_MerchHierarchy = merchSelect.ToList()[0];
                m_selectedParentId = Convert.ToInt32(m_MerchHierarchy.ParentHierarchyId);
                cmbType.SelectedValue = Convert.ToInt32(m_MerchHierarchy.HierarchyLevel);
                m_ModifiedDate = m_MerchHierarchy.ModifiedDate.ToString();
                btnSave.Enabled = m_isSaveAvailable;
                btnSearch.Enabled = false;
                cmbType.Enabled = false;
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SelectGridRow(int rowindex)
        {
            try
            {

                txtCode.Text = dgvMerchandise.Rows[rowindex].Cells["Code"].Value.ToString().Trim();
                cmbStatus.SelectedValue = Convert.ToInt32(dgvMerchandise.Rows[rowindex].Cells["Status"].Value);
                cmbType.SelectedText = dgvMerchandise.Rows[rowindex].Cells["Type"].Value.ToString();
                txtParentName.Text = dgvMerchandise.Rows[rowindex].Cells["ParentName"].Value.ToString().Trim();
                txtDesc.Text = dgvMerchandise.Rows[rowindex].Cells["Description"].Value.ToString().Trim();
                txtName.Text = dgvMerchandise.Rows[rowindex].Cells["Name"].Value.ToString().Trim();
                chkIsTradeable.Checked = Convert.ToBoolean(dgvMerchandise.Rows[rowindex].Cells["IsTradable"].Value);
                //Get ParentId
                var merchSelect = (from p in m_MerchList where p.HierarchyCode.Trim() == txtCode.Text.Trim() select p);
                m_MerchHierarchy = merchSelect.ToList()[0];
                m_selectedParentId = Convert.ToInt32(m_MerchHierarchy.ParentHierarchyId);
                cmbType.SelectedValue = Convert.ToInt32(m_MerchHierarchy.HierarchyLevel);
                m_ModifiedDate = m_MerchHierarchy.ModifiedDate.ToString();
                btnSave.Enabled = m_isSaveAvailable;
                btnSearch.Enabled = false;
                cmbType.Enabled = false;
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
        
        #region Events
        /// <summary>
        /// Saves data to DB
        /// Calls Local SaveMerchandise() method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                SaveMerchandise();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmMerchandiseNew_Load(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// button click event for Reset controls
        /// Calls local ResetControls() method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                ResetControls(pnlSearchHeader);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        

        /// <summary>
        /// Button click event for Searching Merchandise Hierarchy Data
        /// Calls local SearchMerchandise() method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                epMerchandise.Clear();
                SearchMerchandise();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Selects a row, grab the data and display in relevant edit box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvMerchandise_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if ((e.RowIndex >= 0) && (dgvMerchandise.Rows[e.RowIndex].Cells[e.ColumnIndex].GetType() == typeof(DataGridViewImageCell)))
                {
                    //pnlSearchHeader.Enabled = true;
                    btnSave.Enabled = m_isSaveAvailable;
                    btnSearch.Enabled = false;
                    SelectGridRow(e.RowIndex);
                }  
                //if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                //{
                //    SelectGridRow(e);
                //}
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Opens Tree form to select a parent
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearchParent_Click(object sender, EventArgs e)
        {
            try
            {
                frmTree objTree = new frmTree("Merchandise", "", Convert.ToInt32(cmbType.SelectedValue), this);
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
        #endregion

        #region Validations

        void TypeValidate(Boolean yesNo)
        {
            if (Validators.CheckForSelectedValue(cmbType.SelectedIndex) && yesNo == false)
                Validators.SetErrorMessage(epMerchandise, cmbType, "INF0026", lblType.Text);
            else
                Validators.SetErrorMessage(epMerchandise, cmbType);

            if (cmbType.SelectedIndex == 1)
                btnSearchParent.Enabled = false;
            else
                btnSearchParent.Enabled = true;
        }

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                TypeValidate(true);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void CodeValidate(Boolean yesNo)
        {
            if (Validators.CheckForEmptyString(txtCode.Text.Length) && yesNo == false)
                Validators.SetErrorMessage(epMerchandise, txtCode, "INF0019", lblCode.Text);
            else if (Validators.CheckForEmptyString(txtCode.Text.Length)==false && yesNo == false)
                epMerchandise.SetError(txtCode, Common.CodeValidate(txtCode.Text, lblCode.Text.Trim().Substring(0, lblCode.Text.Trim().Length - 2)));
            else
                Validators.SetErrorMessage(epMerchandise, txtCode);
        }
        private void txtCode_Validated(object sender, EventArgs e)
        {
            try
            {
                CodeValidate(true);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void NameValidate(Boolean yesNo)
        {
            if (Validators.CheckForEmptyString(txtName.Text.Length) && yesNo == false)
                Validators.SetErrorMessage(epMerchandise, txtName, "INF0019", lblName.Text);
            else
                Validators.SetErrorMessage(epMerchandise, txtName);
        }

        private void txtName_Validated(object sender, EventArgs e)
        {
            try
            {
                NameValidate(true);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void StatusValidate(Boolean yesNo)
        {
            if (Validators.CheckForSelectedValue(cmbStatus.SelectedIndex) && yesNo == false)
                Validators.SetErrorMessage(epMerchandise, cmbStatus, "INF0026", lblStatus.Text);
            else
                Validators.SetErrorMessage(epMerchandise, cmbStatus);
        }

        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                StatusValidate(true);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        private void dgvMerchandise_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvMerchandise.SelectedRows.Count>0)
                {
                    SelectGridRow(dgvMerchandise.SelectedRows[0].Index);
                    btnSave.Enabled = false;
                    btnSearch.Enabled = m_isSearchAvailable;
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
