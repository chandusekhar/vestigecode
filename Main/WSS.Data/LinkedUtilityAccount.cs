namespace WSS.Data
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("wss.LinkedUtilityAccounts")]
    public partial class LinkedUtilityAccount
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LinkedUtilityAccountId { get; set; }

        [StringLength(50)]
        public string NickName { get; set; }

        [Required]
        public int? WssAccountId { get; set; }

        [Required]
        public int? UtilityAccountId { get; set; }

        [Required]
        public bool? DefaultAccount { get; set; }

       public virtual WssAccount WssAccount { get; set; }

    }
}