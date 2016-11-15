namespace WWDCommon.Data
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("dbo.ApplicationRoles")]
    public partial class ApplicationRoles
    {
        [Key]
        public int Id { get; set; }

        public int ApplicationId { get; set; }

        public int RoleId { get; set; }

        [ForeignKey("Id")]
        public Application Application { get; set; }

        [ForeignKey("Id")]
        public Roles Roles { get; set; }
    }
}