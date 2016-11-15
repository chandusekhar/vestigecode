using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoreComponent.BusinessObjects
{
    public interface IModifierDetails
    {
        System.Int32 CreatedBy { get; }
        System.DateTime CreatedDate { get; }
        System.Int32 ModifiedBy { get; set; }
        System.DateTime ModifiedDate { get; set; }
    }
}
