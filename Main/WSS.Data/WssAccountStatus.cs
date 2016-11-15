using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSS.Data
{
    [Table("ref.vwDLU_WSS_ACCOUNT_STATUS")]
    public class WssAccountStatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WssAccountStatusId { get; set; }

        [Required]
        [StringLength(25)]
        public string WssAccountStatusCode { get; set; }

        [Required]
        [StringLength(255)]
        public string WssAccountStatusDesc { get; set; }
    }
}
