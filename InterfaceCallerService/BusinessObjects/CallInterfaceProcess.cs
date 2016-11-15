using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

//vinculum framework namespace(s)
using CoreComponent.Core;
using CoreComponent.Core.BusinessObjects;
using Vinculum.Framework;
using Vinculum.Framework.Data;
using Vinculum.Framework.DataTypes;


namespace winsrvInterfaceInOut.BusinessObjects
{
    public class CallInterfaceProcess
    {
        #region Constants
        private const string CON_USP_INTERFACEIN = "USP_INT_Interface_Engine_IN";
        private const string CON_USP_INTERFACEOUT = "USP_INT_Interface_Engine";
        #endregion

        #region Constructor
        public CallInterfaceProcess()
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// Runs the Interface-IN stored-procedure
        /// </summary>
        public void RunInterfaceIn()
        {
            DBParameterList dbParam;
            using (DataTaskManager dtManager = new DataTaskManager())
            {
                dbParam = new DBParameterList();
                dtManager.ExecuteNonQuery(CON_USP_INTERFACEIN, dbParam);
            }
        }

        /// <summary>
        /// Runs the Interface-OUT stored-procedure
        /// </summary>
        public void RunInterfaceOut()
        {
            DBParameterList dbParam;
            using (DataTaskManager dtManager = new DataTaskManager())
            {
                dbParam = new DBParameterList();
                dtManager.ExecuteNonQuery(CON_USP_INTERFACEOUT, dbParam);
            }
        }
        #endregion
    }
}
