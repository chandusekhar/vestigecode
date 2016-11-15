using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoreComponent.BusinessObjects
{
    public interface IVendor
    {
        Int32 VendorId { get; set; }
        String VendorName { get; set; }
    }
}
