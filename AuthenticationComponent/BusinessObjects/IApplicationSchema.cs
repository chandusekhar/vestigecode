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
    Purpose				    :	Interface to manage Modules, Functions and Condition.
     *                          
    Modified by			    :	Ajay Kumar Singh
    Date of Modification    :	17/June/2009
    Purpose of Modification	:	Declaration for applicable methods and memebrs in this interface.
    ------------------------------------------------------------------------    
    */

    public interface IApplicationSchema
    {
        string Name { get; set; }
        string Code { get; set; }

        List<IApplicationSchema> Search(string xmlDoc, string spName, ref string errorMessage);
    }
}
