using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AuthenticationComponent.BusinessObjects
{
    /*
    ------------------------------------------------------------------------
    Created by			    :	Amit Bansal
    Created Date		    :	15/June/2009
    Purpose				    :	Class to manage Functions.
     *                          
    Modified by			    :	Ajay Kumar Singh
    Date of Modification    :	17/June/2009
    Purpose of Modification	:	Write code for applicable methods in this class.
    ------------------------------------------------------------------------    
    */

    [Serializable]
    public class Function : ApplicationSchema
    {

        #region Constructor

        public Function()
        {
            m_assignedConditions = new List<Condition>();
        }

        #endregion Constructor

        #region Constants

        #endregion Constants

        #region Properties

        private int m_functionId;

        public int FunctionId
        {
            get { return m_functionId; }
            set { m_functionId = value; }
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

        private List<Condition> m_assignedConditions;

        public List<Condition> AssignedConditions
        {
            get { return m_assignedConditions; }
            set { m_assignedConditions = value; }
        }
        #endregion Properties

        #region Methods
        /// <summary>
        ///Method to search functions.
        /// </summary>
        /// <returns>List of IApplicationSchema</returns>
        public List<IApplicationSchema> Search()
        {
            string errorMessage = string.Empty;
            return base.Search(this.ToString(), "", ref errorMessage);
        }

        #endregion Methods

    }
}
