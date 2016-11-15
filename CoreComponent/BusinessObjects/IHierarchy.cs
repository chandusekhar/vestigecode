using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace CoreComponent.Hierarchies.BusinessObjects
{
    public interface IHierarchy
    {
        int HierarchyId { get; set; }
        int HierarchyType { get; set; }
        int HierarchyLevel { get; set; }
        int ParentHierarchyId { get; set; }
        string HierarchyCode { get; set; }
        
        string HierarchyName { get; set; }
        string ParentHierarchyCode { get; set; }
        string ParentHierarchyName { get; set; }

        bool Save(string xmlDoc, string spName, ref string errorMessage);
    }
}
