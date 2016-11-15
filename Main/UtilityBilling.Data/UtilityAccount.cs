using System.ComponentModel;

namespace UtilityBilling.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ubill.UtilityAccounts")]
    public partial class UtilityAccount
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UtilityAccount()
        {
            DocumentHeaders = new HashSet<DocumentHeader>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UtilityAccountId { get; set; }

        [Required]
        [StringLength(100)]
        public string PrimaryAccountHolderName { get; set; }
        
        

        [Required]
        [StringLength(10)]
        public string ccb_acct_id { get; set; }

        [Required, StringLength(10), DefaultValue("WSS")]
        public string UtilityAccountSourceCode { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DocumentHeader> DocumentHeaders { get; set; }

        public int? EtlInsRunNumber { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? EtlInsTimestamp { get; set; }

        [StringLength(50)]
        public string EtlInsProcessName { get; set; }

        public int? EtlUpdRunNumber { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? EtlUpdTimestamp { get; set; }

        [StringLength(50)]
        public string EtlUpdProcessName { get; set; }
    }
}