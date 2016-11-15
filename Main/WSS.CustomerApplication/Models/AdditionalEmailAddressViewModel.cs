using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WSS.CustomerApplication.Models
{
    public class AdditionalEmailAddressViewModel
    {
        public int AdditionalEmailAddressId { get; set; }
        public int? WssAccountId { get; set; }

        public string EmailAddress { get; set; }

        [ScaffoldColumn(false)]
        public bool ShowAdditionalEmail { get; set; }

    }
}