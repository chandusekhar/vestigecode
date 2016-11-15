//namespace WSS.Data
//{
//    using System;
//    using System.Collections.Generic;
//    using System.ComponentModel.DataAnnotations;
//    using System.ComponentModel.DataAnnotations.Schema;
//    using System.Linq;
//    using System.Text;
//    using System.Threading.Tasks;

//    [Table("wss.Users")]
//    public partial class User
//    {
//        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]

//        public User()
//        {
//            Roles = new HashSet<Role>();
//        }

//        [Key]
//        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//        public int UserId { get; set; }

//        [StringLength(50)]
//        public string Username { get; set; }

//        [StringLength(50)]
//        public string FirstName { get; set; }

//        [StringLength(50)]
//        public string LastName { get; set; }

//        public bool isActive { get; set; }

//        public bool isDeleted { get; set; }

//        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
//        public virtual ICollection<Role> Roles { get; set; }

//    }
//}