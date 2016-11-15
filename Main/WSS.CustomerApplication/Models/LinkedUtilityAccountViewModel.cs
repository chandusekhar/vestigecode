using System.ComponentModel.DataAnnotations;
using WSS.CustomerApplication.Properties;

namespace WSS.CustomerApplication.Models
{
    public class LinkedUtilityAccountViewModel
    {
        public LinkedUtilityAccountViewModel()
        {
            EmailAddress = "";
        }

        [Display(Name = "LinkedUtilityAccountViewModel_CcbAccountNumber_DisplayName", ResourceType = typeof(Resources))]
        public string CcbAccountNumber { get; set; }

        [Display(Name = "LinkedUtilityAccountViewModel_CustomerName_DisplayName", ResourceType = typeof(Resources))]
        public string CustomerName { get; set; }

        [Display(Name = "LinkedUtilityAccountViewModel_EmailAddress_DisplayName", ResourceType = typeof(Resources))]
        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "LinkedUtilityAccountViewModel_EmailAddress_RequiredMessage", AllowEmptyStrings = true)]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "LinkedUtilityAccountViewModel_EmailAddress_InvalidFormatMessage")]
        public string EmailAddress { get; set; }

        public string SearchUtilityAccountNumber { get; set; }

        public int UtilityAccountNumber { get; set; }

        [Display(Name = "LinkedUtilityAccountViewModel_MeterNumberForAccount_DisplayName", ResourceType = typeof(Resources))]
        public string MeterNumberForAccount { get; set; }

        [Display(Name = "LinkedUtilityAccountViewModel_IsUnmetered_DisplayName", ResourceType = typeof(Resources))]
        public bool IsUnmetered { get; set; }

        [Display(Name = "LinkedUtilityAccountViewModel_AccountHolderRelationship_DisplayName", ResourceType = typeof(Resources))]
        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "LinkedUtilityAccountViewModel_AccountHolderRelationship_RequiredMessage")]
        public string AccountHolderRelationship { get; set; }
    }
}