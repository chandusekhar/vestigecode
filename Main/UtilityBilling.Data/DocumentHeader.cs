namespace UtilityBilling.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ubill.DocumentHeaders")]
    public partial class DocumentHeader
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DocumentHeader()
        {
            DocumentDetails = new HashSet<DocumentDetail>();
        }

        public int DocumentHeaderId { get; set; }

        [Required]
        public int? UtilityAccountId { get; set; }

        [StringLength(25)]
        public string DocumentTypeCode { get; set; }

        [Column(TypeName = "datetime2")]
        [Required]
        public DateTime DocumentIssueDate  { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? PublishedDate { get; set; }

        [Required]
        [Column("ETLDocumentLoadKey")]
        public int EtlDocumentLoadKey { get; set; }

        [Required]
        [StringLength(25)]
        public string DocumentStatusCode { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? BillDueDate { get; set; }

        [Column(TypeName = "money")]
        public decimal? BillAmmountDue { get; set; }

        [StringLength(50)]
        public string BillInterceptCode { get; set; }

        [StringLength(20)]
        [Required]
        public string RouteTypeCode { get; set; }

        [Required]
        public int EtlInsRunNumber { get; set; }

        [Required]
        public DateTime EtlInsTimestamp { get; set; }

        [Required]
        [StringLength(50)]
        public string EtlInsProcessName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DocumentDetail> DocumentDetails { get; set; }

        //public virtual Status Status { get; set; }

        public virtual UtilityAccount UtilityAccount { get; set; }
    }
}