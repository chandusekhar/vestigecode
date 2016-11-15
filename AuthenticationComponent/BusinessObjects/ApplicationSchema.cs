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
    Purpose				    :	Class to manage Modules, Functions and Condition.
     *                          
    Modified by			    :	Ajay Kumar Singh
    Date of Modification    :	17/June/2009
    Purpose of Modification	:	Definition for common applicable methods and memebrs for 
     *                          Modules, Functions and Condition.
    ------------------------------------------------------------------------    
    */

    [Serializable]
    public class ApplicationSchema: IApplicationSchema
    {
        private string m_code;
        public string Code
        {
            get { return m_code; }
            set { m_code = value; }
        }

        private string m_name;
        public string Name
        {
            get { return m_name; }
            set { m_name = value; }
        }

        
        /// <summary>
        /// Method to perform search on modules/functions and conditions.
        /// </summary>
        /// <param name="xmlDoc">XML file containing values of search parameters.</param>
        /// <param name="spName">Name of stored procedure to invoke.</param>
        /// <param name="errorMessage">Error message (if any) returned from stored procedure.</param>
        /// <returns>List of IApplicationSchema</returns>
        public virtual List<IApplicationSchema> Search(string xmlDoc, string spName, ref string errorMessage)
        {
            return null;
        }
    }
}
