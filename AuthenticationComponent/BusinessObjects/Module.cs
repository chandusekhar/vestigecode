using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    Purpose				    :	Class to manage Modules.
     *                          
    Modified by			    :	Ajay Kumar Singh
    Date of Modification    :	17/June/2009
    Purpose of Modification	:	Write code for applicable methods in this class.
    ------------------------------------------------------------------------    
    */

    [Serializable]
    public class Module : ApplicationSchema
    {
        #region Constructor
        public Module()
        {
            this.Functions = new List<Function>();
        }
        #endregion Constructor

        #region Constants

        private const string GET_APP_MODULE_FUNCTION_LINK = "usp_GetAppModuleFunctionLink";        

        #endregion Constants

        #region Properties


        private int m_moduleId;

        public int ModuleId
        {
            get { return m_moduleId; }
            set { m_moduleId = value; }
        }

        private System.String m_description;

        public System.String Description
        {
            get { return m_description; }
            set { m_description = value; }
        }

        private System.Int32 m_status;

        public System.Int32 Status
        {
            get { return m_status; }
            set { m_status = value; }
        }

        private List<Function> m_functions;

        public List<Function> Functions
        {
            get { return m_functions; }
            set { m_functions = value; }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Method to search modules.
        /// </summary>
        /// <returns>List of IApplicationSchema</returns>
        public List<IApplicationSchema> Search()
        {
            string errorMessage = string.Empty;
            return base.Search(this.ToString(), "", ref errorMessage);
        }

        /// <summary>
        /// This method returns list of all modules and their 
        /// associated functions linked in a 'List of Module' business object.
        /// </summary>
        /// <returns>List of Module business object</returns>
        public List<Module> ModulesFunctionsLink()
        {            
            List<Module> lModule = new List<Module>();

            try
            {
                using (DataTaskManager dtManager = new DataTaskManager())
                {
                    DBParameterList dbParam = new DBParameterList();
                    using (DataSet dsModFuncLink = dtManager.ExecuteDataSet(GET_APP_MODULE_FUNCTION_LINK, dbParam))
                    {
                        if (dsModFuncLink != null && dsModFuncLink.Tables.Count == 2)
                        {
                            dsModFuncLink.Relations.Add("ModuleFunction", 
                                dsModFuncLink.Tables[0].Columns["ModuleId"], 
                                dsModFuncLink.Tables[1].Columns["ModuleId"]);
                            for (int index = 0; index < dsModFuncLink.Tables[0].Rows.Count; index++)
                            {
                                Module tModule = new Module();
                                tModule.ModuleId = Convert.ToInt32(dsModFuncLink.Tables[0].Rows[index]["ModuleId"]);
                                tModule.Name = Convert.ToString(dsModFuncLink.Tables[0].Rows[index]["ModuleName"]);
                                tModule.Code = Convert.ToString(dsModFuncLink.Tables[0].Rows[index]["ModuleCode"]);

                                //Module Functions
                                DataRow[] drFunctions;
                                drFunctions = dsModFuncLink.Tables[0].Rows[index].GetChildRows("ModuleFunction");
                                if (drFunctions != null)
                                {
                                    for (int jindex = 0; jindex < drFunctions.Length; jindex++)
                                    {
                                        Function tFunction = new Function();
                                        tFunction.FunctionId = Convert.ToInt32(drFunctions[jindex]["FunctionId"]);
                                        tFunction.Name = Convert.ToString(drFunctions[jindex]["FunctionName"]);
                                        tFunction.Code = Convert.ToString(drFunctions[jindex]["FunctionCode"]);                                      
                                        tModule.Functions.Add(tFunction);
                                    }
                                }
                                lModule.Add(tModule);
                            }
                        }
                        else
                        {
                            throw new Exception(Common.GetMessage("2001"));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lModule;
        }

        #endregion Methods
    }
}
