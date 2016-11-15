using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WSS.InternalApplication.Properties;

namespace WSS.InternalApplication.Models
{
    public class DocumentSearchViewModel
    {
        [StringLength(10)]
        [Display(ResourceType = typeof(Resources), Name = "InterceptDocument_DisplayName_InterceptCode")]
        public string InterceptCode { get; set; }

        [StringLength(25)]
        [Display(ResourceType = typeof(Resources), Name = "InterceptDocument_DisplayName_StatusCode")]
        public string StatusCode { get; set; }

        [StringLength(25)]
        [Display(ResourceType = typeof(Resources), Name = "InterceptDocument_DisplayName_RouteTypeCode")]
        public string RouteTypeCode { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "InterceptDocument_DisplayName_AccountNumber")]
        public string AccountNumber { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "InterceptDocument_DisplayName_CustomerName")]
        public string CustomerName { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "InterceptDocument_DisplayName_BillId")]
        [Required]
        public int? DocumentId { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "InterceptDocument_DisplayName_StartDate")]
        public DateTime? StartDate { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "InterceptDocument_DisplayName_EndDate")]
        public DateTime? EndDate { get; set; }

        public List<DocumentSearchResultViewModel> SearchResults { get; set; }
    }
}