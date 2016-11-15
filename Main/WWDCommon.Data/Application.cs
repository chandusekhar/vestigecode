namespace WWDCommon.Data
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("dbo.Application")]
    public partial class Application
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string ApplicationName { get; set; }

        [StringLength(50)]
        public string ApplicationDescription { get; set; }
    }
}