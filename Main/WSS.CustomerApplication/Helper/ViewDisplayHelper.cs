using System.Collections.Generic;
using System.Globalization;
using System.Web.Mvc;
using WSS.CustomerApplication.Properties;

namespace WSS.CustomerApplication.Helper
{
    public class ViewDisplayHelper
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

        public static List<SelectListItem> GetAccountHolderRelationshipSelectListItems()
        {
            var accountHolderRelationshipSelectListItems = new List<SelectListItem>
            {

                 new SelectListItem()
                {
                    Text =Resources.LinkedUtilityAccountController_AboutAccountSelectListItems_PropertyOwner,
                    Value = Resources.LinkedUtilityAccountController_AboutAccountSelectListItems_PropertyOwner
                } ,
                 new SelectListItem()
                {
                    Text =Resources.LinkedUtilityAccountController_AboutAccountSelectListItems_PropertyManager,
                    Value = Resources.LinkedUtilityAccountController_AboutAccountSelectListItems_PropertyManager
                } ,
                 new SelectListItem()
                {
                    Text =Resources.LinkedUtilityAccountController_AboutAccountSelectListItems_CompanyAccount,
                    Value = Resources.LinkedUtilityAccountController_AboutAccountSelectListItems_CompanyAccount
                }

            };

            return accountHolderRelationshipSelectListItems;
        }
    }
}