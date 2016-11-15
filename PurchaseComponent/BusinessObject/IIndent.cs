using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vinculum.Framework.Data;
using Vinculum.Framework.DataTypes;
using CoreComponent.BusinessObjects;

namespace PurchaseComponent.BusinessObjects
{
    public interface IIndent
    {
        int CreatedBy { get; set; }
        DateTime CreatedDate { get; set; }
        int ModifiedBy { get; set; }
        DateTime ModifiedDate { get; set; }
        bool Save(ref string errorMessage);
       
    }
}
