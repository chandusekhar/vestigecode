using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoreComponent.BusinessObjects
{
    public interface IStatus
    {
        Int32 Status { get; set; }
        String StatusName { get; set; }
    }
}
