using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Mvc;

namespace WSS.InternalApplication.Helper
{
    public static class ViewDisplayHelper
    {
        public static List<SelectListItem> GetRowCountSelectListItems(int selectedRowCount, int step = 1, int lowerBound = 1, int upperBound = 30)
        {
            var result = new List<SelectListItem>();
            for (var i = lowerBound; i <= upperBound; i += step)
            {
                var item = new SelectListItem
                {
                    Text = i.ToString(CultureInfo.InvariantCulture),
                    Value = i.ToString(CultureInfo.InvariantCulture),
                    Selected = (i == selectedRowCount)
                };
                result.Add(item);
            }
            return result;
        }
    }
}