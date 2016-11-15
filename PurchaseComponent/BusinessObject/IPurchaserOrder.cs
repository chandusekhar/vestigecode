using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PurchaseComponent.BusinessObjects
{
    public interface IPurchaserOrder
    {
        int CreatedBy { get; set; }
        DateTime CreatedDate { get; set; }
        int ModifiedBy { get; set; }
        DateTime ModifiedDate { get; set; }
        string Save(ref string errorMessage);
    }
}
