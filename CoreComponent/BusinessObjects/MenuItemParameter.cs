using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoreComponent.BusinessObjects
{
    public class MenuItemParameter
    {
        public MenuItemParameter(string id, string value)
        {
            Id = id;
            Value = value;
        }
        public string Id { get; set; }
        public string Value { get; set; }
    }
}
