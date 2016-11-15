using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WSS.Data
{
    [Table("ref.vwDLU_SUBSCRIBE_TX_STATUS")]
    public class SubscriptionTransactionStatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SubscriptionTransactionStatusId { get; set; }

        [Required]
        [StringLength(25)]
        public string SubscriptionTransactionStatusCode { get; set; }

        [Required]
        [StringLength(255)]
        public string SubscriptionTransactionStatusDesc { get; set; }
    }
}
