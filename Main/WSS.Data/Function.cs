//namespace WSS.Data
//{
//    using System.Collections.Generic;
//    using System.ComponentModel.DataAnnotations;
//    using System.ComponentModel.DataAnnotations.Schema;

//    [Table("wss.Functions")]
//    public partial class Function
//    {
//        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]

//        public Function()
//        {
//            Roles = new List<Role>();
//        }
//        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//        [Key]
//        public int FunctionId { get; set; }

//        [StringLength(20)]
//        public string FunctionCode { get; set; }

//        [StringLength(50)]
//        public string FunctionName { get; set; }

//        [StringLength(200)]
//        public string FunctionDescription { get; set; }

//        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
//        public virtual ICollection<Role> Roles { get; set; }
//    }
//}