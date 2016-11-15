using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UtilityBilling.Data
{
    [Table("ref.vwDLU_DOCUMENT_STATUS")]
    public class DocumentStatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DocumentStatusId { get; set; }

        [Required]
        [StringLength(25)]
        public string DocumentStatusCode { get; set; }

        [Required]
        [StringLength(255)]
        public string DocumentStatusValue { get; set; }
    }
}
