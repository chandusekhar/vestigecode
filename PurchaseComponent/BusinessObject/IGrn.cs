using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PurchaseComponent.BusinessObjects
{
    public interface IGrn
    {
        int CreatedBy { get; set; }
        string CreatedDate { get; set; }
        int ModifiedBy { get; set; }
        DateTime ModifiedDate { get; set; }
        bool Save(ref string errormsg);
       
    }
}
