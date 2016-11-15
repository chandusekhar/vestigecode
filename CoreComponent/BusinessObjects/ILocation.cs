using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoreComponent.BusinessObjects
{
    public interface  ILocation
    {
        Int32 LocationId { get; set; }
        String LocationCode { get; set; }
        String LocationName { get; set; }
    }
}
