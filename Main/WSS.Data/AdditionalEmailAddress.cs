namespace WSS.Data
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("wss.AdditionalEmailAddress")]
    public partial class AdditionalEmailAddress
    {
       
        public int AdditionalEmailAddressId { get; set; }

        [Required]
        public int? WssAccountId { get; set; }

        [Required]
        [StringLength(100)]
        public string EmailAddress { get; set; }

        public virtual WssAccount WssAccount { get; set; }
    }
}