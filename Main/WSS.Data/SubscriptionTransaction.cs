using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WSS.Data
{
    [Table("wss.SubscriptionTransaction")]
    public class SubscriptionTransaction
    {
        [Key]
        [Required]
        [Column("SubscriptionTxId")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SubscriptionTransactionId { get; set; }

        [Required]
        [Column("SubscriptionTxDate", TypeName = "datetime2")]
        public DateTime? SubscriptionTransactionDate { get; set; }

        [Required]
        [StringLength(25)]
        [Column("SubscriptionTxTypeCode")]
        public string SubscriptionTransactionTypeCode { get; set; }

        [Required]
        [Column("SubscriptionTxStatusId")]
        public int SubscriptionTransactionStatusId { get; set; }

        [Required]
        [StringLength(10)]
        public string ccb_acct_id { get; set; }

        [Column("ETL_UpdateTimestamp")]
        public DateTime? EtlUpdateDate { get; set; }

        [StringLength(100)]
        [Column("ETL_UpdateProcessName")]
        public string EtlUpdateProcess { get; set; }

        [Column("ETL_UpdateProcessId")]
        public int? EtlUpdateProcessId { get; set; }
    }
}