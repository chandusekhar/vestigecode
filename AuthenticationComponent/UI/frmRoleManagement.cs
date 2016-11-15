using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using CoreComponent.Core.BusinessObjects;
//using CoreComponent.Hierarchies.BusinessObjects;
using AuthenticationComponent.BusinessObjects;

namespace AuthenticationComponent.UI
{
    /*
    ------------------------------------------------------------------------
    Created by			    :	Ajay Kumar Singh
    Created Date		    :	09/July/2009
    Purpose				    :	This form is used to search/create/update 'Role' and then assign 'Module(s)' 
     *                          and 'Function(s)' to this 'Role'.
     *                          
    Modified by			    :	
    Date of Modification    :	
    Purpose of Modification	:	Change in Form design, look n feel.
    ------------------------------------------------------------------------    
    */

    public partial class frmRoleManagement : CoreComponent.Core.UI.HierarchyTemplate
    {        
        #region Global Variables

        Role m_objRole;
        List<Role> m_listOfRoles = new List<Role>();
        List<Module> m_listOfModules; //Master List of modules
        List<Module> m_listOfAvailableModules = new List<Module>(); //List of available modules, used in Update mode.
        List<Module> m_listOfAssignedModules = new List<Module>(); //List of assigned modules
        int m_roleId = 0;        
        DateTime m_modifiedDate = new DateTime(); //for concurrency control check
        ArrayList m_arrListOfAllModules = new ArrayList();
        ArrayList m_arrListOfAssignedModules = new ArrayList();
        private int m_userId = AuthenticationComponent.BusinessObjects.Authenticate.LoggedInUser.UserId;

        #endregion Global Variables

        #region Constants

        private const string EXPAND_ALL = "Expand All";
        private const string COLLAPSE_ALL = "Collapse All";

        #endregion Constants

        #region Constructor
        public frmRoleManagement()
        {
            InitializeComponent();

            try
            {
                //btnSearch.Focus();
                InitializeControls();
                lblPageTitle.Text = "Role Management";
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"),
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion Constructor

        #region Local Functions

        /// <summary>
        /// Method to load all rights in a TreeView control.
        /// </summary>
        /// <param name="tView">Name of TreeView control</param>
        /// <param name="objModuleList">List of Modules with Functions.</param>
        private void LoadTreeView(TreeView tView, List<Module> objModuleList)
        {
            try
            {
                tView.Nodes.Clear();

                string moduleName = "Name Asc";
                objModuleList.Sort((new GenericComparer<BusinessObjects.Module>(moduleName.Split(' ')[0],
                    moduleName.Split(' ')[1] == "Asc" ? SortDirection.Ascending :
                    SortDirection.Descending)).Compare);

                if (objModuleList.Count > 0)
                {
                    //LINQ
                    var temp = (from p in objModuleList where p.ModuleId != 0 select p);
                    List<Module> tempListOfModule = new List<Module>();
                    tempListOfModule = temp.ToList();

                    foreach (Module tModule in tempListOfModule)
                    {
                        tView.Nodes.Add(tModule.ModuleId.ToString(), tModule.Name);
                        foreach (Function tFunction in tModule.Functions)
                        {
                            tView.Nodes[tModule.ModuleId.ToString()].Nodes.Add(tFunction.FunctionId.ToString(), tFunction.Name);
                            #region tFunction.AssignedConditions
                            //Place holder for 'AssignedConditions' code
                            #endregion tFunction.AssignedConditions
                        }
                    }
                }
            }
            catch { throw; }
            
        }

        /// <summary>
        /// Method to bind TreeView Control.
        /// </summary>
        private void BindTreeView_AllModules(TreeView treeViewControl)
        {
            try
            {
                Module objModule = new Module();
                m_listOfModules = new List<Module>();
                m_listOfModules = objModule.ModulesFunctionsLink();
                LoadTreeView(treeViewControl, m_listOfModules); //Load All Rights. 
            }
            catch { throw; }
        }

        /// <summary>
        /// Method to initialize the CreateRole screen.
        /// </summary>
        private void InitializeControls()
        {
            //Bind Status DropDown Control.
            Common.BindParamComboBox(cmbRoleStatus, Common.STATUS, 0, 0, 0);
            if(cmbRoleStatus.Items.Count>0)
                cmbRoleStatus.SelectedValue = 1;
    
            //Bind the TreeView Control.
            BindTreeView_AllModules(tvAvailableRights); //Actua            

            //Initialize GridView.
            this.DefineGridView_SearchRoles();
        }

        /// <summary>
        /// Method to define grid view on search Role form. i.e.
        /// add columns to grid view and set their properties.
        /// </summary>
        private void DefineGridView_SearchRoles()
        {
            try
            {
                dgvSearchRoles.AutoGenerateColumns = false;
                dgvSearchRoles.DataSource = null;
                DataGridView dgvSearchRolesTemp =
                    Common.GetDataGridViewColumns(dgvSearchRoles,
                    Environment.CurrentDirectory + User.GRIDVIEW_DEFINITION_XML_PATH);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method to bind search Role grid.
        /// </summary>
        private void BindSearchRoleGrid()
        {
            try
            {
                dgvSearchRoles.DataSource = new List<Role>();
                if (m_listOfRoles.Count > 0)
                {
                    dgvSearchRoles.DataSource = m_listOfRoles;
                    dgvSearchRoles.Rows[0].Selected = false;
                }
                else
                {
                    MessageBox.Show(Common.GetMessage("8002"), Common.GetMessage("10001"),
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }        

        /// <summary>
        /// Method to read all checked Modules/Functions in a TreeView control 
        /// and return List of Modules with Functions.
        /// </summary>
        /// <param name="tView"></param>
        /// <returns>List of Modules with Functions.</returns>
        private List<Module> ReadCheckedNodes(TreeView tView)
        {
            List<Module> lModule = new List<Module>();
            try
            {
                foreach (TreeNode mNode in tView.Nodes) //Read Checked Modules
                {
                    if (mNode.Checked)
                    {
                        Module tModule = new Module();
                        tModule.ModuleId = Convert.ToInt32(mNode.Name);
                        tModule.Name = mNode.Text;

                        foreach (TreeNode fNode in mNode.Nodes) //Read Checked Functions
                        {
                            if (fNode.Checked)
                            {
                                Function tFunction = new Function();
                                tFunction.FunctionId = Convert.ToInt32(fNode.Name);
                                tFunction.Name = fNode.Text;
                                tModule.Functions.Add(tFunction);
                                #region tFunction.AssignedConditions
                                //Place holder for 'AssignedConditions' code                                
                                #endregion tFunction.AssignedConditions
                            }
                            else
                            {
                                //MessageBox.Show("At least one function should be selected:\nModule Name - " + mNode.Text);
                                //break;
                            }
                        }
                        lModule.Add(tModule);
                    }
                    else
                    {
                        //MessageBox.Show("At least one module/function should be selected");
                        //break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return lModule;
        }

        /// <summary>
        /// Method to read all Modules/Functions in a TreeView control 
        /// and return List of Modules with Functions.
        /// This method is used to read assigned rights.
        /// </summary>
        /// <param name="tView"></param>
        /// <returns>List of Modules with Functions.</returns>
        private List<Module> ReadAllNodes(TreeView tView)
        {
            List<Module> lModule = new List<Module>();
            try
            {
                foreach (TreeNode mNode in tView.Nodes) 
                {
                    Module tModule = new Module();
                    tModule.ModuleId = Convert.ToInt32(mNode.Name);
                    tModule.Name = mNode.Text;

                    foreach (TreeNode fNode in mNode.Nodes) 
                    {                        
                        Function tFunction = new Function();
                        tFunction.FunctionId = Convert.ToInt32(fNode.Name);
                        tFunction.Name = fNode.Text;
                        tModule.Functions.Add(tFunction);
                        #region tFunction.AssignedConditions
                        //Place holder for 'AssignedConditions' code                                
                        #endregion tFunction.AssignedConditions                        
                    }
                    lModule.Add(tModule);                    
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return lModule;
        }

        /// <summary>
        /// Method to reset the TreeView control. This will not reload the TreeView
        /// control, but will uncheck all the checked parent/child node(s).
        /// </summary>
        /// <param name="tView">Name of TreeView control.</param>
        private void ResetTreeView(TreeView tView)
        {
            try
            {
                foreach (TreeNode mNode in tView.Nodes) //Read parent nodes
                {
                    mNode.Checked = false;
                    foreach (TreeNode fNode in mNode.Nodes) //Read child nodes
                    {
                        fNode.Checked = false;
                        #region tFunction.AssignedConditions
                        //Place holder for 'AssignedConditions' code                                
                        #endregion tFunction.AssignedConditions
                    }
                }
                tView.CollapseAll();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        #endregion Local Functions

        #region Form Load

        private void frmRoleManagement_Load(object sender, EventArgs e)
        {
           
        }

        #endregion Form Load

        #region Search Code

        /// <summary>
        /// Method to search roles.
        /// </summary>
        private void SearchRoleData()
        {
            try
            {
                m_objRole = new Role();
                m_objRole.RoleCode = txtRoleCode.Text.Trim();
                m_objRole.RoleName = txtRoleName.Text.Trim();
                m_objRole.Description = txtRoleDesc.Text.Trim();
                m_objRole.Status = Convert.ToInt32(cmbRoleStatus.SelectedValue);                

                string errMsg = string.Empty;
                m_listOfRoles = m_objRole.RoleSearch(Common.ToXml(m_objRole), Role.ROLE_SEARCH, ref errMsg);

                //Bind Grid
                BindSearchRoleGrid();
            }
            catch { throw; }
        }

        /// <summary>
        /// Method to hide error providers icon on click of 
        /// search and reset button.
        /// </summary>
        private void DisableValidation()
        {
            errCreateRole.SetError(txtRoleCode, string.Empty);
            errCreateRole.SetError(txtRoleName, string.Empty);
            errCreateRole.SetError(cmbRoleStatus, string.Empty);
        }

        /// <summary>
        /// Search records on basis of search criteria.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                DisableValidation();
                SearchRoleData();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"),
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        #endregion Search Code

        #region Save Code

        /// <summary>
        /// Generate Create Role Error 
        /// </summary>
        /// <returns></returns>
        private StringBuilder ValidateRoleData()
        {
            if (Validators.CheckForEmptyString(txtRoleCode.Text.Trim().Length))
            {
                Validators.SetErrorMessage(errCreateRole, txtRoleCode, "INF0019", lblRoleCode.Text);
            }
            ////Valid input for Role Code
            //else if (!Validators.IsAlphaNumHyphen(txtRoleCode.Text.Trim()))
            //{
            //    //errCreateRole.SetError(txtRoleCode, Common.GetMessage("VAL0016"));
            //    Validators.SetErrorMessage(errCreateRole, txtRoleCode, "VAL0016", lblRoleCode.Text);
            //}
            else
            {
                Validators.SetErrorMessage(errCreateRole, txtRoleCode);
            }

            if (Validators.CheckForEmptyString(txtRoleName.Text.Trim().Length))
            {
                Validators.SetErrorMessage(errCreateRole, txtRoleName, "INF0019", lblRoleName.Text);
            }
            ////Valid input for Role Name
            //else if (!Validators.IsAlphaSpace(txtRoleName.Text.Trim()))
            //{
            //    //errCreateRole.SetError(txtRoleName, Common.GetMessage("VAL0017"));
            //    Validators.SetErrorMessage(errCreateRole, txtRoleName, "VAL0017", lblRoleName.Text);
            //}
            else
            {
                Validators.SetErrorMessage(errCreateRole, txtRoleName);
            }

            //Status Combo
            if (cmbRoleStatus.SelectedIndex == 0)
            {
                errCreateRole.SetError(cmbRoleStatus, Common.GetMessage("INF0026",
                    lblRoleStatus.Text.Trim().Substring(0, lblRoleStatus.Text.Trim().Length - 2)));
            }
            else
            {
                errCreateRole.SetError(cmbRoleStatus, string.Empty);
            }

            //Append all error msgs
            StringBuilder sbError = new StringBuilder();
            sbError.Append(Validators.GetErrorMessage(errCreateRole, 
                txtRoleCode)).ToString().Trim();

            sbError.AppendLine();
            sbError.Append(Validators.GetErrorMessage(errCreateRole,
                txtRoleName)).ToString().Trim();

            sbError.AppendLine();
            sbError.Append(Validators.GetErrorMessage(errCreateRole,
                cmbRoleStatus)).ToString().Trim(); 
           
            return Common.ReturnErrorMessage(sbError);
        }

        /// <summary>
        /// Method to save Role data.
        /// </summary>
        private void SaveRoleData()
        {
            try
            {
                #region Validation Code

                StringBuilder sbError = new StringBuilder();
                sbError = ValidateRoleData();

                if (!sbError.ToString().Trim().Equals(string.Empty))
                {
                    MessageBox.Show(sbError.ToString(), Common.GetMessage("10001"), 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                #endregion Validation Code

                if (sbError.ToString().Trim().Equals(string.Empty))
                {
                    string msg=string.Empty;
                    if (m_roleId > 0)
                        msg = "Edit";
                    else
                        msg = "Save";

                    DialogResult saveResult = MessageBox.Show(Common.GetMessage("5010", msg), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (saveResult == DialogResult.Yes)
                    {
                        m_objRole = new Role();
                        m_objRole.RoleId = m_roleId; // Global variable
                        m_objRole.RoleCode = txtRoleCode.Text.Trim();
                        m_objRole.RoleName = txtRoleName.Text.Trim();
                        m_objRole.Description = txtRoleDesc.Text.Trim();
                        m_objRole.Status = Convert.ToInt32(cmbRoleStatus.SelectedValue);
                        m_objRole.CreatedBy = m_userId;
                        m_objRole.CreatedDate = Convert.ToDateTime(new DateTime(1900, 1, 1).ToString(Common.DATE_TIME_FORMAT));
                        m_objRole.ModifiedBy = m_userId;
                        if (m_roleId == 0)
                        {
                            m_objRole.ModifiedDate = Convert.ToDateTime(new DateTime(1900, 1, 1).ToString(Common.DATE_TIME_FORMAT));
                        }
                        else //m_roleId > 0
                        {
                            m_objRole.ModifiedDate = m_modifiedDate;
                        }

                        //Reset the global List of Modules/Functions
                        //to load checked one.
                        m_listOfModules = new List<Module>();

                        //List of Modules/Functions                    
                        m_listOfModules = ReadAllNodes(tvAssignedRights);
                        m_objRole.AssignedModules = m_listOfModules;

                        //objRole = objRole.RoleSearch();
                        string errMsg = string.Empty;
                        bool retVal = m_objRole.RoleSave(Common.ToXml(m_objRole), Role.ROLE_SAVE, ref errMsg);
                        if (retVal)
                        {
                            MessageBox.Show(Common.GetMessage("8001"), Common.GetMessage("10001"),
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                            if (m_roleId == 0)
                            {
                                //In case of 'Create', reset the form.
                                ResetForm();
                                //After successful Update, Refresh the default Search result.
                                SearchRoleData();
                            }
                            else
                            {
                                //After successful Update, Refresh the default Search result.
                                ResetForm();
                                SearchRoleData();
                            }
                            //Save successful, now enable the search.
                            //btnSearch.Enabled = true;

                        }
                        else if (errMsg.Equals("INF0020"))//Duplicate data
                        {
                            MessageBox.Show(Common.GetMessage(errMsg,
                                lblRoleCode.Text.Substring(0, lblRoleCode.Text.Trim().Length - 2),
                                lblRoleName.Text.Substring(0, lblRoleName.Text.Trim().Length - 2)),
                                Common.GetMessage("10001"),
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else if (errMsg.Equals("INF0022"))//Concurrency
                        {
                            MessageBox.Show(Common.GetMessage("INF0022"), Common.GetMessage("10001"),
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            Common.LogException(new Exception(errMsg));
                            MessageBox.Show(Common.GetMessage("2003"), Common.GetMessage("10001"),
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                } 
            }
            catch { throw; }
        }
        /// <summary>
        /// Event to save form data, both in create/update modes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                SaveRoleData();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"),
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion Save Code

        #region Reset Code

        /// <summary>
        /// Method to reset the form in create mode.
        /// </summary>
        private void ResetForm()
        {
            try
            {
                txtRoleCode.Text = string.Empty;
                txtRoleName.Text = string.Empty;
                txtRoleDesc.Text = string.Empty;
                //cmbRoleStatus.SelectedIndex = 0;
                if (cmbRoleStatus.Items.Count > 0)
                    cmbRoleStatus.SelectedValue = 1;

                //Reset treeview Control and its related resources also.                
                tvAssignedRights.Nodes.Clear();
                tvAvailableRights.Nodes.Clear();
                m_listOfAssignedModules = new List<Module>();
                m_listOfAvailableModules = new List<Module>();

                //Reset Tree state maintaining ArrayLists also.
                m_arrListOfAllModules.Clear();
                m_arrListOfAssignedModules.Clear();

                //Bind default TreeView.
                BindTreeView_AllModules(tvAvailableRights);

                //Reset Grid also.
                dgvSearchRoles.DataSource = null;

                btnSearch.Enabled = true;
                btnSave.Enabled = true;
                m_roleId = 0; //Ready for Create mode.
                m_modifiedDate = Convert.ToDateTime(new DateTime(1900, 1, 1).ToString(Common.DATE_TIME_FORMAT));
            }
            catch { throw; }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                DisableValidation();
                ResetForm();                
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"),
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }        

        #endregion Reset Code

        #region TreeView Expand/Collapse Area

        private void Toggle_MouseHover(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
        }
        private void Toggle_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }

        /// <summary>
        /// Method for Expand/Collapse of TreeView Control
        /// </summary>
        /// <param name="lblName"></param>
        /// <param name="tvName"></param>
        private void Toggle_Click(Label lblName, TreeView tvName)
        {
            try
            {
                if (lblName.Text == EXPAND_ALL)
                {
                    tvName.ExpandAll();
                    lblName.Text = COLLAPSE_ALL;
                }
                else //Collapse All
                {
                    tvName.CollapseAll();
                    lblName.Text = EXPAND_ALL;
                }
            }
            catch { throw; }
        }

        private void lblToggle_Click(object sender, EventArgs e)
        {
            Toggle_Click(lblToggle, tvAvailableRights);
        }

        private void lblToggleAssigned_Click(object sender, EventArgs e)
        {
            Toggle_Click(lblToggleAssigned, tvAssignedRights);
        }

        #endregion TreeView Expand/Collapse Area

        #region GridViewRow and Load Form data in update mode
        /// <summary>
        /// Fill form data in update mode including
        /// modules and functions in right side TreeView control.        
        /// </summary>
        private void LoadFormData_UpdateMode()
        {
            try
            {
                DisableValidation();
                txtRoleCode.Text = m_objRole.RoleCode;
                txtRoleName.Text = m_objRole.RoleName;
                txtRoleDesc.Text = m_objRole.Description;
                cmbRoleStatus.SelectedValue = m_objRole.Status;
                m_modifiedDate = m_objRole.ModifiedDate;

                #region Compare two trees - for future ref
                ////Bind TreeView with checked nodes as Assigned Rights.            
                //foreach (Module tModule in m_objRole.AssignedModules)
                //{
                //    if (tvAssignedRights.Nodes.ContainsKey(tModule.ModuleId.ToString()))
                //    {
                //        tvAssignedRights.Nodes[tModule.ModuleId.ToString()].Checked = true;
                //        foreach (Function tFunction in tModule.Functions)
                //        {
                //            if (tvAssignedRights.Nodes[tModule.ModuleId.ToString()].Nodes.ContainsKey(tFunction.FunctionId.ToString()))
                //            {
                //                tvAssignedRights.Nodes[tModule.ModuleId.ToString()].Nodes[tFunction.FunctionId.ToString()].Checked = true;
                //            }
                //        }
                //    }
                //}
                #endregion Compare two trees - for future ref

                tvAssignedRights.Nodes.Clear();
                m_listOfAssignedModules = new List<Module>();
                m_listOfAssignedModules = m_objRole.AssignedModules;
                LoadTreeView(tvAssignedRights, m_listOfAssignedModules);

                #region Display only Available rights in LHS tree

                //Take a copy of m_listOfModules in m_listOfAvailableModules
                //now remove those module-function from m_listOfAvailableModules
                //which are present in m_listOfAssignedModules.
                //Finally, m_listOfAvailableModules will be remains with only 
                //unassigned module-function
                //Bind this m_listOfAvailableModules with tvAvailableRights

                //On change of record, refresh LHS tree.
                tvAvailableRights.Nodes.Clear();
                BindTreeView_AllModules(tvAvailableRights);

                m_listOfAvailableModules = new List<Module>();
                m_listOfAvailableModules = m_listOfModules; //Preserve m_listOfModules

                //Compare two trees to display only available rights in LHS tree.
                //int ind = 0;
                foreach (Module tModule in m_listOfAssignedModules)
                {
                    Module tempModule = m_listOfAvailableModules.Find(delegate(Module mod)
                    {
                        return mod.ModuleId == tModule.ModuleId;
                    });

                    //Also findout index of that module in m_listOfAvailableModules
                    int modIndex = m_listOfAvailableModules.FindIndex(delegate(Module mod)
                    {
                        return mod.ModuleId == tModule.ModuleId;
                    });

                    if (tempModule != null) //Module found
                    {
                        foreach (Function tFunction in tModule.Functions)
                        {
                            Function tempFunction = tempModule.Functions.Find(delegate(Function func)
                            {
                                return func.FunctionId == tFunction.FunctionId;
                            });

                            if (tempFunction != null) //Function found
                            {
                                m_listOfAvailableModules[modIndex].Functions.Remove(tempFunction);

                                //Check if all functions removed from this matching module, 
                                //in parent list (m_listOfAvailableModules), 
                                //then also remove that particular module 
                                //from parent list (m_listOfAvailableModules).
                                if (m_listOfAvailableModules[modIndex].Functions.Count == 0)
                                {
                                    m_listOfAvailableModules.Remove(tempModule);
                                }
                            }
                        }
                    }                    
                }
                tvAvailableRights.Nodes.Clear();
                LoadTreeView(tvAvailableRights, m_listOfAvailableModules);

                #endregion Display only Available rights in LHS tree

                //Data loaded in Update mode, buttons enable/disable code here. 
                btnSearch.Enabled = false;
            }
            catch (Exception ex){ throw ex; }            
        }

        /// <summary>
        /// Show data in controls for Role
        /// </summary>
        /// <param name="e"></param>
        private void SelectRoleGridRow(DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    int roleId = Convert.ToInt32(dgvSearchRoles.Rows[e.RowIndex].Cells[Common.ROLE_ID].Value);
                    if (roleId != m_roleId)
                    {
                        m_roleId = roleId; //Assign this RoleId to Global Variable.
                        m_objRole = m_listOfRoles.Find(delegate(Role role)
                        {
                            return role.RoleId == roleId;
                        });

                        //Load Form data in update mode
                        LoadFormData_UpdateMode();
                    }
                }
            }
            catch { throw; }
        }

        /// <summary>
        /// Selected record in Update mode.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvSearchRoles_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        ///// <summary>
        ///// Show data in controls for Role
        ///// </summary>
        ///// <param name="e"></param>
        //private void SelectRoleGridRow(DataGridViewCellEventArgs e)
        //{
        //    try
        //    {
        //        if (e.RowIndex >= 0)
        //        {
        //            int roleId = Convert.ToInt32(dgvSearchRoles.Rows[e.RowIndex].Cells[Common.ROLE_ID].Value);
        //            if (roleId != m_roleId)
        //            {
        //                m_roleId = roleId; //Assign this RoleId to Global Variable.
        //                m_objRole = m_listOfRoles.Find(delegate(Role role)
        //                {
        //                    return role.RoleId == roleId;
        //                });

        //                //Load Form data in update mode
        //                LoadFormData_UpdateMode();
        //            }
        //        }
        //    }
        //    catch { throw; }
        //}

        ///// <summary>
        ///// Selected record in Update mode.
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void dgvSearchRoles_RowEnter(object sender, DataGridViewCellEventArgs e)
        //{
        //    try
        //    {                
        //        SelectRoleGridRow(e);
        //    }
        //    catch (Exception ex)
        //    {
        //        Common.LogException(ex);
        //        MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"),
        //            MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        #endregion GridViewRow and Load Form data in update mode

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

        private void tvAvailableRights_AfterCheck(object sender, TreeViewEventArgs e)
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

        private void tvAssignedRights_AfterCheck(object sender, TreeViewEventArgs e)
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

        #endregion TreeView Check/UnCheck Code

        #region Move TreeView Nodes

        /// <summary>
        /// Method to move checked nodes from one TreeView to other TreeView.
        /// </summary>
        /// <param name="fromTreeView">Source TreeView</param>
        /// <param name="toTreeView">Destination TreeView</param>
        /// <param name="fromList">Source List of Modules</param>
        /// <param name="toList">Destination List of Modules</param>
        private void MoveToTreeView(TreeView fromTreeView, TreeView toTreeView, List<Module> fromList, List<Module> toList)
        {
            try
            {
                for (int j = 0; j < fromTreeView.Nodes.Count; j++)
                {
                    TreeNode parent = fromTreeView.Nodes[j];
                    for (int cnt = 0; cnt < parent.Nodes.Count; cnt++)
                    {
                        if (parent.Nodes[cnt].Checked)
                        {
                            Module modToBeMoved = toList.Find(delegate(Module m) { return m.ModuleId == Convert.ToInt32(parent.Name); });
                            if (modToBeMoved == null)
                            {
                                Module tempMod = fromList.Find(delegate(Module m) { return m.ModuleId == Convert.ToInt32(parent.Name); });
                                Module newMod = CopyModule(tempMod);

                                Function func = tempMod.Functions.Find(delegate(Function f) { return f.FunctionId == Convert.ToInt32(parent.Nodes[cnt].Name); });
                                newMod.Functions.Add(func);
                                tempMod.Functions.Remove(func);
                                toList.Add(newMod);

                                //Check if tempMod has any remaining function, otherwise remove from fromList
                                if (tempMod.Functions.Count == 0)
                                {
                                    fromList.Remove(tempMod);
                                }
                            }
                            else
                            {
                                Module tempM = fromList.Find(delegate(Module m) { return m.ModuleId == Convert.ToInt32(parent.Name); });
                                Function func = tempM.Functions.Find(delegate(Function f) { return f.FunctionId == Convert.ToInt32(parent.Nodes[cnt].Name); });
                                modToBeMoved.Functions.Add(func);
                                tempM.Functions.Remove(func);

                                //Check if tempMod has any remaining function, otherwise remove from fromList
                                if (tempM.Functions.Count == 0)
                                {
                                    fromList.Remove(tempM);
                                }
                            }
                        }
                    }
                }

                string moduleName = "Name Asc";
                fromList.Sort((new GenericComparer<BusinessObjects.Module>(moduleName.Split(' ')[0], 
                    moduleName.Split(' ')[1] == "Asc" ? SortDirection.Ascending : 
                    SortDirection.Descending)).Compare);
                toList.Sort((new GenericComparer<BusinessObjects.Module>(moduleName.Split(' ')[0], 
                    moduleName.Split(' ')[1] == "Asc" ? SortDirection.Ascending : 
                    SortDirection.Descending)).Compare);

                BindTreeView(fromTreeView, fromList);
                BindTreeView(toTreeView, toList);
            }
            catch { throw; }
        }

        /// <summary>
        /// Method to bind the TreeView with given List of Modules.
        /// </summary>
        /// <param name="tvName">Name of TreeView to Bind</param>
        /// <param name="listM">List of Modules to bind with this TreeView</param>
        private void BindTreeView(TreeView tvName, List<Module> listM)
        {
            tvName.Nodes.Clear();

            //LINQ
            var temp = (from p in listM where p.ModuleId != 0 select p);
            List<Module> tempListOfModule = new List<Module>();
            tempListOfModule = temp.ToList();

            foreach (Module tModule in tempListOfModule)
            {
                tvName.Nodes.Add(tModule.ModuleId.ToString(), tModule.Name);
                foreach (Function tFunction in tModule.Functions)
                {
                    tvName.Nodes[tModule.ModuleId.ToString()].Nodes.Add(tFunction.FunctionId.ToString(), tFunction.Name);
                }
            }
        }

        /// <summary>
        /// Method to create and return copy of passed Module.
        /// </summary>
        /// <param name="m">Module to create copy of</param>
        /// <returns></returns>
        private Module CopyModule(Module m)
        {
            Module newMod = new Module();
            newMod.ModuleId = m.ModuleId;
            newMod.Code = m.Code;
            newMod.Description = m.Description;
            newMod.Name = m.Name;
            newMod.Status = m.Status;
            return newMod;
        }

        /// <summary>
        /// Method to maintain expand/collapsed state of both trees
        /// during nodes migration.
        /// </summary>
        private void MaintainTreeState()
        {
            //Maintain State for both Trees (Simultaneously)
            //Maintain TreeView State (for tvAvailableRights)
            if (m_arrListOfAllModules.Count > 0)
            {
                for (int i = 0; i < m_arrListOfAllModules.Count; i++)
                {
                    TreeNode tn = new TreeNode();
                    tn.Name = m_arrListOfAllModules[i].ToString();

                    if (tvAvailableRights.Nodes[tn.Name] != null)
                    {
                        tvAvailableRights.Nodes[tn.Name].Expand();
                    } 
                }
            }

            //Maintain TreeView State (for tvAssignedRights)
            if (m_arrListOfAssignedModules.Count > 0)
            {
                for (int i = 0; i < m_arrListOfAssignedModules.Count; i++)
                {                    
                    TreeNode tn = new TreeNode();
                    tn.Name = m_arrListOfAssignedModules[i].ToString();

                    if (tvAssignedRights.Nodes[tn.Name] !=null)
                    {
                        tvAssignedRights.Nodes[tn.Name].Expand();
                    }
                }
            }
        }

        /// <summary>
        /// Method to Add ModuleId of 'expanded and checked' Module Nodes 
        /// from tvAvailableRights to m_arrListOfAssignedModules.
        /// </summary>
        private void ReadLHSTree() //Left Hand Side Tree.
        {
            foreach (TreeNode tNode in tvAvailableRights.Nodes) //Read parent node(Module) only.
            {
                if (tNode.Checked)
                {
                    //if this node is checked and expanded.
                    //then its 'ModuleId' must exists in 'm_arrListOfAllModules'
                    m_arrListOfAllModules.Sort();
                    if (m_arrListOfAllModules.BinarySearch(tNode.Name) >= 0)
                    {
                        //Add ModuleId of this 'expanded and checked' Module Node
                        //from tvAvailableRights to m_arrListOfAssignedModules
                        m_arrListOfAssignedModules.Sort();
                        if (m_arrListOfAssignedModules.BinarySearch(tNode.Name) < 0)
                        {
                            //Above condition is to avoid duplication.
                            m_arrListOfAssignedModules.Add(tNode.Name);
                        }
                    }
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //Add ModuleId of 'expanded and checked' Module Nodes 
            //from tvAvailableRights to m_arrListOfAssignedModules
            ReadLHSTree();

            //Node moving code.
            if (m_roleId == 0) //Create mode
            {
                MoveToTreeView(tvAvailableRights, tvAssignedRights, m_listOfModules, m_listOfAssignedModules);
            }
            else //Update mode when 'm_RoleId > 0'
            {
                MoveToTreeView(tvAvailableRights, tvAssignedRights, m_listOfAvailableModules, m_listOfAssignedModules);
            }

            //Maintain State for both Trees (Simultaneously)
            MaintainTreeState();
            
        }

        /// <summary>
        /// Method to Add ModuleId of 'expanded and checked' Module Nodes 
        /// from tvAssignedRights to m_arrListOfAllModules
        /// </summary>
        private void ReadRHSTree() //Right Hand Side Tree
        {            
            foreach (TreeNode tNode in tvAssignedRights.Nodes) //Read parent node(Module) only.
            {
                if (tNode.Checked)
                {
                    //if this node is checked and expanded.
                    //then its 'ModuleId' must exists in 'm_arrListOfAssignedModules'
                    m_arrListOfAssignedModules.Sort();
                    if (m_arrListOfAssignedModules.BinarySearch(tNode.Name) >= 0)
                    {
                        //Add ModuleId of this 'expanded and checked' Module Node
                        //from tvAssignedRights to m_arrListOfAllModules
                        m_arrListOfAllModules.Sort();
                        if (m_arrListOfAllModules.BinarySearch(tNode.Name) < 0)
                        {
                            //Above condition is to avoid duplication.
                            m_arrListOfAllModules.Add(tNode.Name);
                        }
                    }
                }
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            //Add ModuleId of 'expanded and checked' Module Nodes 
            //from tvAssignedRights to m_arrListOfAllModules
            ReadRHSTree();

            //Node moving code
            if (m_roleId == 0) //Create mode
            {
                MoveToTreeView(tvAssignedRights, tvAvailableRights, m_listOfAssignedModules, m_listOfModules);
            }
            else //Update mode when 'm_RoleId > 0'
            {
                MoveToTreeView(tvAssignedRights, tvAvailableRights, m_listOfAssignedModules, m_listOfAvailableModules);
            }

            //Maintain State for both Trees (Simultaneously)
            MaintainTreeState();
        }

        #endregion Move TreeView Nodes

        #region Code to save Tree State

        /// <summary>
        /// Add the ModuleId of expanded nodes from tvAvailableRights.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvAvailableRights_AfterExpand(object sender, TreeViewEventArgs e)
        {
            TreeNode selNode = new TreeNode();
            selNode = e.Node;
            //MessageBox.Show("Expanded Node: Key = " + selNode.Name + " Text = " + selNode.Text);
            m_arrListOfAllModules.Sort();
            if (m_arrListOfAllModules.BinarySearch(selNode.Name) < 0) //Element not found in arrList
            {
                m_arrListOfAllModules.Add(selNode.Name);
            }
        }

        /// <summary>
        /// Remove the ModuleId of collapsed nodes from tvAvailableRights.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvAvailableRights_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            TreeNode selNode = new TreeNode();
            selNode = e.Node;
            //MessageBox.Show("Expanded Node: Key = " + selNode.Name + " Text = " + selNode.Text);
            m_arrListOfAllModules.Sort();
            if (m_arrListOfAllModules.BinarySearch(selNode.Name) >= 0) //Element found in arrList
            {
                m_arrListOfAllModules.Remove(selNode.Name);
            }
        }

        /// <summary>
        /// Add the ModuleId of expanded nodes from tvAssignedRights.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvAssignedRights_AfterExpand(object sender, TreeViewEventArgs e)
        {
            TreeNode selNode = new TreeNode();
            selNode = e.Node;
            //MessageBox.Show("Expanded Node: Key = " + selNode.Name + " Text = " + selNode.Text);
            m_arrListOfAssignedModules.Sort();
            if (m_arrListOfAssignedModules.BinarySearch(selNode.Name) < 0) //Element not found in arrList
            {
                m_arrListOfAssignedModules.Add(selNode.Name);
            }
        }

        /// <summary>
        /// Remove the ModuleId of collapsed nodes from tvAssignedRights.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvAssignedRights_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            TreeNode selNode = new TreeNode();
            selNode = e.Node;
            //MessageBox.Show("Expanded Node: Key = " + selNode.Name + " Text = " + selNode.Text);
            m_arrListOfAssignedModules.Sort();

            if (m_arrListOfAssignedModules.BinarySearch(selNode.Name) >= 0) //Element found in arrList
            {
                m_arrListOfAssignedModules.Remove(selNode.Name);
            }
        }

        #endregion Code to save Tree State
        /// <summary>
        /// Selected record in Update mode and capture both the event(mouse and keyboard)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void dgvSearchRoles_CurrentCellChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (dgvSearchRoles.SelectedRows.Count == 0) return;
        //        int roleId = Convert.ToInt32(dgvSearchRoles.SelectedRows[0].Cells[Common.ROLE_ID].Value);
        //        if (roleId > 0)
        //        {
                   
        //            if (roleId != m_roleId)
        //            {
        //                m_roleId = roleId; //Assign this RoleId to Global Variable.
        //                m_objRole = m_listOfRoles.Find(delegate(Role role)
        //                {
        //                    return role.RoleId == roleId;
        //                });

        //                //Load Form data in update mode
        //                LoadFormData_UpdateMode();
        //            }
        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        Common.LogException(ex);
        //        MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"),
        //            MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        private void dgvSearchRoles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if ((e.RowIndex >= 0) && (dgvSearchRoles.Rows[e.RowIndex].Cells[e.ColumnIndex].GetType() == typeof(DataGridViewImageCell)))
                {
                    
                    btnSave.Enabled = true;
                    btnSearch.Enabled = false;
                    int roleId = Convert.ToInt32(dgvSearchRoles.Rows[e.RowIndex].Cells[Common.ROLE_ID].Value);
                    if (roleId > 0)
                    {
                        if (roleId != m_roleId)
                        {
                            m_roleId = roleId; //Assign this RoleId to Global Variable.
                            m_objRole = m_listOfRoles.Find(delegate(Role role)
                            {
                                return role.RoleId == roleId;
                            });

                            //Load Form data in update mode
                            LoadFormData_UpdateMode();
                        }
                    }
                }
                
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvSearchRoles_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvSearchRoles.SelectedRows.Count == 0) return;
                int roleId = Convert.ToInt32(dgvSearchRoles.SelectedRows[0].Cells[Common.ROLE_ID].Value);
                if (roleId > 0)
                {
                    if (roleId != m_roleId)
                    {
                        m_roleId = roleId; //Assign this RoleId to Global Variable.
                        m_objRole = m_listOfRoles.Find(delegate(Role role)
                        {
                            return role.RoleId == roleId;
                        });

                        //Load Form data in update mode
                        LoadFormData_UpdateMode();
                    }
                }
                btnSave.Enabled = false;
                btnSearch.Enabled = true;
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       
    }
}
