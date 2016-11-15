using System;
using System.ComponentModel;

namespace WSS.InternalApplication.Models
{
    public class DocumentListViewModel
    {
        public int DocumentHeaderId { get; set; }

        [DisplayName("Date")]
        public DateTime DocumentDate { get; set; }

        [DisplayName("Amount")]
        public decimal AmountDue { get; set; }

        [DisplayName("Type")]
        public string DocumentType { get; set; }

        [DisplayName("Document Status")]
        public string Status { get; set; }

        [DisplayName("Document Status code")]
        public string DocumnetStatuscode { get; set; }

        [DisplayName("Main Customer Name")]
        public string MainCustomerName { get; set; }

        [DisplayName("Account Number")]
        public string ccbAccountNumber { get; set; }

        [DisplayName("Intercept Code")]
        public string InterceptCode { get; set; }

        [DisplayName("Processed Date")]
        public string ProcessedDate { get; set; }

        [DisplayName("Processed By")]
        public string ProcessedBy { get; set; }

        [DisplayName("Document Status")]
        public string DocumentStatus { get; set; }

        [DisplayName("Last 10 Days")]
        public DateTime Last10Days { get; set; }

        [DisplayName("> 10 Days")]
        public DateTime GreaterThan10Days { get; set; }

        [DisplayName("Route Type Code")]
        public string RouteTypeCode { get; set; }
    }
}