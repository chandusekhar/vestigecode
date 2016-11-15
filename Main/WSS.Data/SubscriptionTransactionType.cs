using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WSS.Data
{
    [Table("ref.vwDLU_SUBSCRIBE_TX_TYPE")]
    public class SubscriptionTransactionType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SubscriptionTransactionTypeId { get; set; }

        [Required]
        [StringLength(25)]
        public string SubscriptionTransactionTypeCode { get; set; }

        [Required]
        [StringLength(255)]
        public string SubscriptionTransactionTypeDesc { get; set; }
    }
}
