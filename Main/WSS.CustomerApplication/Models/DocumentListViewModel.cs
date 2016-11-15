using System;
using System.ComponentModel;

namespace WSS.CustomerApplication.Models
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
    }
}