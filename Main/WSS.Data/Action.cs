using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WSS.Data
{
    [Table("wss.Actions")]
    public class Action
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ActionId { get; set; }

        [Required]
        public string ActionToken { get; set; }

        [Required]
        public string ActionName { get; set; }

        [ForeignKey("WssAccount")]
        public int WssAccountId { get; set; }

        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime? ExpiryDateTime { get; set; }

        public virtual WssAccount WssAccount { get; set; }

    }
}