using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WSS.Data
{
    [Table("ref.vwDLU_AUDIT_EVENT_WSS_ACCT")]
    public class AuditEventWssAccount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AuditEventWssAccountId { get; set; }

        [Required]
        [StringLength(25)]
        public string AuditEventWssAccountCode { get; set; }

        [Required]
        [StringLength(255)]
        public string AuditEventWssAccountDesc { get; set; }
    }
}
