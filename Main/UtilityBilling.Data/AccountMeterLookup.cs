namespace UtilityBilling.Data
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ubill.AccountMeterLookup")]
    public class AccountMeterLookup
    {
        [Key]
        public int AccountMeterLookupId { get; set; }

        [Required]
        [StringLength(10)]
        public string CcbAcctId { get; set; }

        [Required]
        [StringLength(30)]
        public string CcbBadgeNbr { get; set; }

        [Required]
        public int UtilityAccountId { get; set; }

        [Required]
        public int EtlInsRunNumber { get; set; }

        [Required]
        public DateTime EtlInsTimestamp { get; set; }

        [Required]
        [StringLength(50)]
        public string EtlInsProcessName { get; set; }
    }
}
