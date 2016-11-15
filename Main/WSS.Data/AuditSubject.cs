using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WSS.Data
{

    [Table("ref.vwDLU_AUDIT_SUBJECT")]
    public class AuditSubject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AuditSubjectId { get; set; }

        [Required]
        [StringLength(25)]
        public string AuditSubjectCode { get; set; }

        [Required]
        [StringLength(255)]
        public string AuditSubjectDesc { get; set; }
    }
}
