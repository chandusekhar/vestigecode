using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WSS.AuditTransaction.Models
{
    public class AuditLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long AuditLogId { get; set; }

        public string UserName { get; set; }

        [Required]
        public DateTime EventDateUTC { get; set; }

        [Required]
        public EventType EventType { get; set; }

        [Required]
        [MaxLength(512)]
        public string TypeFullName { get; set; }

        [Required]
        [MaxLength(256)]
        public string RecordId { get; set; }

        public virtual ICollection<AuditLogDetail> LogDetails { get; set; }
    }
}