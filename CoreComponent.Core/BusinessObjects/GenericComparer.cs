using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace CoreComponent.Core.BusinessObjects
{

 /*
  ------------------------------------------------------------------------
  Created by			:	Punit Kumar
  Created Date		    :	26 Jun 2009
  ------------------------------------------------------------------------    
 */

    // Summary:
    //     Specifies the direction in which to sort a list of items.
    public enum SortDirection
    {
        // Summary:
        //     Sort from smallest to largest. For example, from A to Z.
        Ascending = 0,
        //
        // Summary:
        //     Sort from largest to smallest. For example, from Z to A.
        Descending = 1,
    }

    public class GenericComparer<T> : IComparer<T>
    {
        private string paramName;
        private SortDirection order;

        public GenericComparer(string paramName, SortDirection order)
        {
            this.paramName = paramName;
            this.order = order;
        }

        public int Compare(T x, T y)
        {
            PropertyInfo piAttr = x.GetType().GetProperty(paramName);

            if (order == SortDirection.Descending)
                return ((IComparable)piAttr.GetValue(y, null)).CompareTo((IComparable)piAttr.GetValue(x, null));
            else
                return ((IComparable)piAttr.GetValue(x, null)).CompareTo((IComparable)piAttr.GetValue(y, null));
        }
    }
}
