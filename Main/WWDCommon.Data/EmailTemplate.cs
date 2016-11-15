namespace WWDCommon.Data
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("common.EmailTemplates")]
    public partial class EmailTemplate
    {
        [Key]
        public int TemplateId { get; set; }

        [StringLength(50)]
        public string TemplateName { get; set; }

        [StringLength(100)]
        public string DefaultFrom { get; set; }

        [StringLength(100)]
        public string Defaultcc { get; set; }

        [StringLength(100)]
        public string Defaultbcc { get; set; }

        [StringLength(100)]
        public string Subject { get; set; }

        public string MessageBody { get; set; }

        [StringLength(50)]
        public string Version { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? LastChangedDate { get; set; }
    }
}