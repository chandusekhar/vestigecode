namespace WSS.Data
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Audit.AuditRecords")]
    public class AuditRecord
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AuditRecord()
        {
        }

        [Required]
        [StringLength(25)]
        public string AuditSubjectCode { get; set; }


        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int AuditRecordId { get; set; }

        [Required]
        public int? AuditEntityId { get; set; }

        [Required]

        public DateTime date { get; set; }

        //public string time { get; set; }
        [Required]
        public string PerformedBy { get; set; }

        [Required]
        [StringLength(25)]
        public string AuditEventWssAccountCode { get; set; }


        public string Description { get; set; }


        public virtual WssAccount WssAccount { get; set; }

    }
}