using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace AuthenticationComponent.BusinessObjects
{
    /*
    ------------------------------------------------------------------------
    Created by			    :	Amit Bansal
    Created Date		    :	15/June/2009
    Purpose				    :	Interface to manage Roles.
     *                          
    Modified by			    :	Ajay Kumar Singh
    Date of Modification    :	17/June/2009
    Purpose of Modification	:	Declaration of applicable members and methods for Role.
    ------------------------------------------------------------------------    
    */

    public interface IRole
    {
        int RoleId { get; set; }
        string RoleCode { get; set; }
        string RoleName { get; set; }
        List<Module> AssignedModules { get; set; }

        #region Method Declaration

        bool RoleSave(string xmlDoc, string spName, ref string errorMessage);
        
        List<Role> RoleSearch(string xmlDoc, string spName, ref string errorMessage);

        #endregion Method Declaration
    }
}
