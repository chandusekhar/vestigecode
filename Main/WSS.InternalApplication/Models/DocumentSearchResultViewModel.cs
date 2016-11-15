using System;
using System.ComponentModel.DataAnnotations;
using WSS.InternalApplication.Properties;

namespace WSS.InternalApplication.Models
{
    public class DocumentSearchResultViewModel
    {
        [Display(ResourceType = typeof(Resources), Name = "InterceptDocument_DisplayName_InterceptCode")]
        [StringLength(10)]
        public string InterceptCode { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "InterceptDocument_DisplayName_RouteType")]
        [StringLength(25)]
        public string RouteTypeCode { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "InterceptDocument_DisplayName_AccountNumber")]
        [StringLength(10)]
        public string AccountNumber { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "InterceptDocument_DisplayName_DateIssued")]
        public DateTime DateIssued { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "InterceptDocument_DisplayName_CustomerName")]
        [StringLength(100)]
        public string CustomerName { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "InterceptDocument_DisplayName_BillId")]
        public int BillId { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "InterceptDocument_DisplayName_StatusCode")]
        [StringLength(25)]
        public string StatusCode { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "InterceptDocument_DisplayName_StatusDescription")]
        [StringLength(255)]
        public string StatusDescription { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "InterceptDocument_DisplayName_ProcessedBy")]
        [StringLength(8)]
        public string ProcessedBy { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "InterceptDocument_DisplayName_ProcessedDate")]
        public DateTime ProcessedDate { get; set; }
    }
}