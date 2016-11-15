using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UtilityBilling.Data
{
    [Table("ref.DomainLookup")]
    public class DomainLookup
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DomainLookupId { get; set; }

        [Required]
        [StringLength(50)]
        public string DomainName { get; set; }

        [Required]
        [StringLength(25)]
        public string LookupCode { get; set; }

        [Required]
        [StringLength(255)]
        public string LookupValue { get; set; }
    }
}
