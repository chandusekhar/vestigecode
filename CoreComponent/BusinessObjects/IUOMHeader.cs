using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoreComponent.BusinessObjects
{
    public interface IUOMHeader
    {
        Int32 UOMId { get; set; }
        String UOMName { get; set; }
    }
}
