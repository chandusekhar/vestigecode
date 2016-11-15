using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WWDCommon.Data
{
    [Table("EmailQueue")]
    public class EmailQueue
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Emaild { get; set; }

        public string TemplateName { get; set; }

        public string Parameters { get; set; }

        public string EtlBatchNumber { get; set; }

        public bool IsActive { get; set; }
    }
}