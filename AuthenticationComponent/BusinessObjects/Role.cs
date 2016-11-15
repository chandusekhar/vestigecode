using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Data;
using Vinculum.Framework.Data;
using Vinculum.Framework.DataTypes;
using CoreComponent.Core.BusinessObjects;


namespace AuthenticationComponent.BusinessObjects
{
    /*
    ------------------------------------------------------------------------
    Created by			    :	Amit Bansal
    Created Date		    :	15/June/2009
    Purpose				    :	Class to manage Role.
     *                          
    Modified by			    :	Ajay Kumar Singh
    Date of Modification    :	17/June/2009
    Purpose of Modification	:	Write code for applicable methods and members in this class.
    ------------------------------------------------------------------------    
    */

    [Serializable]
    public class Role:IRole
    {

        #region Constructor
        public Role()
        {
            this.m_assignedModules = new List<Module>();
        }
        #endregion Constructor

        #region Constants

        public const string ROLE_SAVE = "usp_RoleSave";
        public const string ROLE_SEARCH = "usp_RoleSearch";
        private const string GET_ALL_ROLES = "usp_GetAllRoles";

        #endregion Constants

        #region Properties

        private System.Int32 m_roleId;

        public System.Int32 RoleId
        {
            get { return m_roleId; }
            set { m_roleId = value; }
        }

        private System.String m_roleCode;

        public System.String RoleCode
        {
            get { return m_roleCode; }
            set { m_roleCode = value; }
        }

        private System.String m_description;

        public System.String Description
        {
            get { return m_description; }
            set { m_description = value; }
        }

        private System.String m_roleName;

        public System.String RoleName
        {
            get { return m_roleName; }
            set { m_roleName = value; }
        }

        private System.Int32 m_status;

        public System.Int32 Status
        {
            get { return m_status; }
            set { m_status = value; }
        }

        private string m_statusValue;

        public string StatusValue
        {
            get { return m_statusValue; }
            set { m_statusValue = value; }
        }

        private System.Int32 m_createdBy;

        public System.Int32 CreatedBy
        {
            get { return m_createdBy; }
            set { m_createdBy = value; }
        }

        private System.DateTime m_createdDate;

        public System.DateTime CreatedDate
        {
            get { return m_createdDate; }
            set { m_createdDate = value; }
        }

        private System.Int32 m_modifiedBy;

        public System.Int32 ModifiedBy
        {
            get { return m_modifiedBy; }
            set { m_modifiedBy = value; }
        }

        private System.DateTime m_modifiedDate;

        public System.DateTime ModifiedDate
        {
            get { return m_modifiedDate; }
            set { m_modifiedDate = value; }
        }        

        private List<Module> m_assignedModules;

        public List<Module> AssignedModules
        {
            get { return m_assignedModules; }
            set { m_assignedModules = value; }
        }

        #endregion Properties

        #region Methods

        #region RoleSearch - Not in use
        //public Role RoleSearch()
        //{
        //    Role tRole = new Role();
        //    //List<Role> lRole = new List<Role>();
        //    List<Module> lModule = new List<Module>();

        //    try
        //    {
        //        using (DataTaskManager dtManager = new DataTaskManager())
        //        {                    
        //            DBParameterList dbParam = new DBParameterList();
        //            using (DataSet dsRoleData = dtManager.ExecuteDataSet(ROLE_SEARCH, dbParam))
        //            {
        //                if (dsRoleData != null && dsRoleData.Tables.Count == 3)
        //                {
        //                    //Retrieve Role header data from dsRoleData.Tables[0]
        //                    //Role tRole = new Role();
        //                    tRole.RoleCode = dsRoleData.Tables[0].Rows[0]["RoleCode"].ToString();
        //                    tRole.RoleName = dsRoleData.Tables[0].Rows[0]["RoleName"].ToString();
        //                    tRole.Description = dsRoleData.Tables[0].Rows[0]["Description"].ToString();
        //                    tRole.Status = (int)dsRoleData.Tables[0].Rows[0]["Status"];

        //                    //Retrieve Module(s) and Function(s) associated with the Role.
        //                    //dsRoleData.Tables[1] - Module(s) associated with the Role.
        //                    //dsRoleData.Tables[2] - Mudule(s)-Function(s) associated with the Role.

        //                    //Set relation between parent(modules) and child(functions) table.
        //                    dsRoleData.Relations.Add("RoleModuleFunction", 
        //                        dsRoleData.Tables[1].Columns["ModuleId"],
        //                        dsRoleData.Tables[2].Columns["ModuleId"]);
        //                    for (int index = 0; index < dsRoleData.Tables[1].Rows.Count; index++)
        //                    {
        //                        Module tModule = new Module();
        //                        tModule.ModuleId = Convert.ToInt32(dsRoleData.Tables[1].Rows[index]["ModuleId"]);
                                
        //                        //Module Functions
        //                        DataRow[] drFunctions;
        //                        drFunctions = dsRoleData.Tables[1].Rows[index].GetChildRows("RoleModuleFunction");
        //                        if (drFunctions != null)
        //                        {
        //                            for (int jindex = 0; jindex < drFunctions.Length; jindex++)
        //                            {
        //                                Function tFunction = new Function();
        //                                tFunction.FunctionId = Convert.ToInt32(drFunctions[jindex]["FunctionId"]);
        //                                tModule.Functions.Add(tFunction);
        //                            }
        //                        }                                
        //                        lModule.Add(tModule);
        //                    }
        //                    tRole.AssignedModules = lModule;
        //                    //lRole.Add(tRole);
        //                }
        //                else
        //                {
        //                    throw new Exception(Common.GetMessage("2002"));
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return tRole; //lRole;
        //}
        #endregion RoleSearch - Not in use

        /// <summary>
        /// Method to get list of all active roles.
        /// </summary>
        /// <returns>List of Roles</returns>
        public static List<Role> GetAllRoles()
        {
            List<Role> lRole = new List<Role>();
            try
            {
                using (DataTaskManager dtManager = new DataTaskManager())
                {
                    DBParameterList dbParam = new DBParameterList();
                    using (DataTable dtRoles = dtManager.ExecuteDataTable(GET_ALL_ROLES, dbParam))
                    {
                        if (dtRoles != null)
                        {
                            if (dtRoles.Rows.Count > 0)
                            {
                                for (int index = 0; index < dtRoles.Rows.Count; index++)
                                {
                                    Role tRole = new Role();
                                    tRole.RoleId = Convert.ToInt32(dtRoles.Rows[index]["RoleId"]);
                                    tRole.RoleName = Convert.ToString(dtRoles.Rows[index]["RoleName"]);
                                    lRole.Add(tRole);
                                }
                            }
                        }
                        else
                        {
                            throw new Exception(Common.GetMessage("2004"));
                        }
                    }
                }
                return lRole;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion Methods

        #region IRole Members

        /// <summary>
        /// This method is implemented to insert/update role and its details like associated
        /// modules and functions.
        /// </summary>
        /// <param name="xmlDoc">XML file containing values of db parameters.</param>
        /// <param name="spName">Name of stored procedure to invoke.</param>
        /// <param name="errorMessage">Error message (if any) returned from stored procedure.</param>
        /// <returns>true/false</returns>
        public bool RoleSave(string xmlDoc, string spName, ref string errorMessage)
        {
            bool isSuccess = false;
            try
                {
                    using (DataTaskManager dtManager = new DataTaskManager())
                    {
                        //Declare and initialize the parameter list object
                        DBParameterList dbParam = new DBParameterList();

                        //Add the relevant 2 parameters
                        dbParam.Add(new DBParameter(Common.PARAM_DATA, xmlDoc, DbType.String));
                        dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String,
                            ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                        try
                        {
                            //Begin the transaction and executing procedure to save the record(s) 
                            dtManager.BeginTransaction();
                            dtManager.ExecuteNonQuery(spName, dbParam);

                            //Update database message
                            errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();

                            //If an error returned from the database
                            if (errorMessage.Length > 0)
                            {
                                dtManager.RollbackTransaction();
                                isSuccess = false;
                            }
                            else
                            {
                                dtManager.CommitTransaction();
                                isSuccess = true;
                            }
                        }
                        catch (Exception ex)
                        {
                            dtManager.RollbackTransaction();
                            throw ex;
                        }
                    }
                }
            catch (Exception ex)
            {
                throw ex;
            }
            return isSuccess;
        }

        /// <summary>
        /// This method is implementd to search roles and return role details as list of role.
        /// Method will work for returning list of roles as well as returning details of particular role i.e. 
        /// associated modules and functions for a single role.
        /// </summary>
        /// <param name="xmlDoc">XML file containing values of search parameters.</param>
        /// <param name="spName">Name of stored procedure to invoke.</param>
        /// <param name="errorMessage">Error message (if any) returned from stored procedure.</param>
        /// <returns>List of Role(s)</returns>
        public List<Role> RoleSearch(string xmlDoc, string spName, ref string errorMessage)
        {
            List<Role> lRole = new List<Role>();
            try
                {
                    using (DataTaskManager dtManager = new DataTaskManager())
                    {
                        //Declare and initialize the parameter list object.
                        DBParameterList dbParam = new DBParameterList();

                        //Add the relevant 2 parameters
                        dbParam.Add(new DBParameter(Common.PARAM_DATA, xmlDoc, DbType.String));
                        dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String,
                            ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                        using (DataTable dtRoleData = dtManager.ExecuteDataTable(spName, dbParam))
                        {
                            // update database message
                            errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();
                            if (errorMessage.Length == 0 && dtRoleData.Rows.Count > 0) //No dbError
                            {
                                for (int i = 0; i < dtRoleData.Rows.Count; i++)
                                {
                                    Role tRole;
                                    int currentRoleId = Convert.ToInt32(dtRoleData.Rows[i]["RoleId"]);
                                    //Check duplicate RoleId via delegate
                                    Role tempRole = lRole.Find(delegate(Role role)
                                    {
                                        return role.RoleId == currentRoleId;                                             
                                    });
                                    if (tempRole == null) //If same RoleId does not exist.
                                    {
                                        tRole = new Role();
                                        tRole.RoleId = Convert.ToInt32(dtRoleData.Rows[i]["RoleId"]);
                                        tRole.RoleCode = Convert.ToString(dtRoleData.Rows[i]["RoleCode"]);
                                        tRole.RoleName = Convert.ToString(dtRoleData.Rows[i]["RoleName"]);
                                        tRole.Description = Convert.ToString(dtRoleData.Rows[i]["Description"]);
                                        tRole.Status = Convert.ToInt32(dtRoleData.Rows[i]["Status"]);
                                        tRole.StatusValue = Convert.ToString(dtRoleData.Rows[i]["StatusValue"]);
                                        tRole.ModifiedDate = Convert.ToDateTime(dtRoleData.Rows[i]["ModifiedDate"]);

                                        //Loop for Modules
                                        List<Module> lModule = new List<Module>();
                                        DataRow[] drModules = dtRoleData.Select("RoleId = " + currentRoleId);
                                        for (int j = 0; j < drModules.Length; j++)
                                        {
                                            Module tModule;
                                            int currentModuleId = Convert.ToInt32(drModules[j]["ModuleId"]);
                                            //Check duplicate ModuleId via delegate
                                            Module tempModule = lModule.Find(delegate(Module module)
                                            {
                                                return module.ModuleId == currentModuleId;
                                            });
                                            if (tempModule == null) //If same ModuleId does not exist.
                                            {
                                                tModule = new Module();
                                                tModule.ModuleId = Convert.ToInt32(drModules[j]["ModuleId"]);
                                                tModule.Name = Convert.ToString(drModules[j]["ModuleName"]);

                                                //Loop for Functions
                                                List<Function> lFunction = new List<Function>();
                                                DataRow[] drFunctions = dtRoleData.Select("RoleId = " + currentRoleId +
                                                    " AND ModuleId = " + currentModuleId);
                                                for (int k = 0; k < drFunctions.Length; k++)
                                                {
                                                    Function tFunction;
                                                    int currentFunctionId = Convert.ToInt32(drFunctions[k]["FunctionId"]);
                                                    //Check duplicate FunctionId via delegate
                                                    Function tempFunction = lFunction.Find(delegate(Function function)
                                                    {
                                                        return function.FunctionId == currentFunctionId;
                                                    });
                                                    if (tempFunction == null) //If same ModuleId does not exist.
                                                    {
                                                        tFunction = new Function();
                                                        tFunction.FunctionId = Convert.ToInt32(drFunctions[k]["FunctionId"]);
                                                        tFunction.Name = Convert.ToString(drFunctions[k]["FunctionName"]);

                                                        //Add one by one tFunction object to lFunction list                                                        
                                                        lFunction.Add(tFunction);
                                                    }
                                                } //Loop Functions
                                                //Assign list of functions to Module object
                                                tModule.Functions = lFunction;
                                                //Add one by one tModule object to lModule list
                                                lModule.Add(tModule);
                                            }
                                        } //Loop Modules                                           
                                        //Assign list of modules to Role object
                                        tRole.AssignedModules = lModule;
                                        //Add one by one tRole object to lRole list
                                        lRole.Add(tRole);
                                    }
                                } //Loop Roles
                            }                            
                        }
                    }
                }
            catch (Exception ex)
            {
                throw ex;
            }
            return lRole;
        }        
        #endregion
    }
}
