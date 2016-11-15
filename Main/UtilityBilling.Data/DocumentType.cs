using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UtilityBilling.Data
{
    [Table("ref.vwDLU_DOCUMENT_TYPE")]
    public class DocumentType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DocumentTypeId { get; set; }

        [Required]
        [StringLength(25)]
        public string DocumentTypeCode { get; set; }

        [Required]
        [StringLength(255)]
        public string DocumentTypeValue { get; set; }
    }
}
