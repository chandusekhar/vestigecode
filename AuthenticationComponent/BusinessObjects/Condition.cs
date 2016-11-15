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
    Purpose				    :	Class to manage Conditions.
     *                          
    Modified by			    :	Ajay Kumar Singh
    Date of Modification    :	17/June/2009
    Purpose of Modification	:	Write code for applicable methods in this class.
    Remarks                 :   Not in use.
    ------------------------------------------------------------------------    
    */

    [Serializable]
    public class Condition: ApplicationSchema
    {

        #region Constructor

        #endregion Constructor

        #region Constants

        #endregion Constants

        #region Properties

        private int m_conditionId;

        public int ConditionId
        {
            get { return m_conditionId; }
            set { m_conditionId = value; }
        }

        private System.String m_conditionValue;

        public System.String ConditionValue
        {
            get { return m_conditionValue; }
            set { m_conditionValue = value; }
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

        public List<IApplicationSchema> Search()
        {
            string errorMessage = string.Empty;
            return base.Search(this.ToString(), "", ref errorMessage);
        }

        #endregion Properties

        #region Methods
        #endregion Methods

    }
}
