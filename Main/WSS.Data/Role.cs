//namespace WSS.Data
//{
//    using System.Collections.Generic;
//    using System.ComponentModel.DataAnnotations;
//    using System.ComponentModel.DataAnnotations.Schema;

//    [Table("wss.Roles")]
//    public partial class Role
//    {
//        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
//        public Role()
//        {
//            Users = new HashSet<User>();
//            Functions = new HashSet<Function>();
//        }
//        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//        [Key]
//        public int RoleId { get; set; }

//        [StringLength(50)]
//        public string RoleName { get; set; }

//        [StringLength(200)]
//        public string RoleDescription { get; set; }

//        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
//        public virtual ICollection<User> Users { get; set; }
//        public virtual ICollection<Function> Functions { get; set; }

//    }
//}