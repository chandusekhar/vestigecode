using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoreComponent.BusinessObjects
{
    public interface ICarRegistration
    {
        #region Method Declaration

        /// <summary>
        /// Save or update 
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <param name="spName"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        bool CarRegistrationSave(string xmlDoc, string spName, ref string errorMessage);

        /// <summary>
        /// Get the list of car registrations
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <param name="spName"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        CarRegistration CarRegistrationSearch(string xmlDoc, string spName, ref string errorMessage);

        #endregion Method Declaration
    }
}
