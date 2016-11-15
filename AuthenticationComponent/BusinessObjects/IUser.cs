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
    Created Date		    :	19/June/2009
    Purpose				    :	Interface to manage Roles.
     *                          
    Modified by			    :	Ajay Kumar Singh
    Date of Modification    :	22/June/2009
    Purpose of Modification	:	Declaration of applicable members and methods for User.
    ------------------------------------------------------------------------    
    */

    public interface IUser
    {
        int UserId { get; set; }
        string Password { get; set; }

        #region Method Declaration

        //Method to save and update user data.
        bool UserSave(string xmlDoc, string spName, ref string errorMessage);

        //Method to search the users.
        List<User> UserSearch(string xmlDoc, string spName, ref string errorMessage);

        #endregion Method Declaration
    }
}
