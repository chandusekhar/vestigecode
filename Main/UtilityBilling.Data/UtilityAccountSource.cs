using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UtilityBilling.Data
{
    [Table("ref.vwDLU_UTILITY_ACCOUNT_SOURCE")]
    public class UtilityAccountSource
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UtilityAccountSourceId { get; set; }

        [Required]
        [StringLength(25)]
        public string UtilityAccountSourceCode { get; set; }

        [Required]
        [StringLength(255)]
        public string UtilityAccountSourceValue { get; set; }
    }
}
