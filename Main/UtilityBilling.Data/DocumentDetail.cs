using System;
using System.ComponentModel.DataAnnotations;

namespace UtilityBilling.Data
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ubill.DocumentDetails")]
    public partial class DocumentDetail
    {
        public int DocumentDetailId { get; set; }

        public int DocumentHeaderId { get; set; }

        public byte[] DocumentPdf { get; set; }

        [Required]
        public int EtlInsRunNumber { get; set; }

        [Required]
        public DateTime EtlInsTimestamp { get; set; }

        [Required]
        [StringLength(50)]
        public string EtlInsProcessName { get; set; }

        public virtual DocumentHeader DocumentHeader { get; set; }
    }
}