namespace WSS.Data
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("wss.SiteContent")]
    public partial class SiteContent
    {
        [Key]
        public int ContentId { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? PublishedDate { get; set; }

        [StringLength(50)]
        public string ContentKey { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? ExpiryDate { get; set; }

        public bool? IsActive { get; set; }

        [StringLength(50)]
        public string ContentType { get; set; }

        public string Contents { get; set; }

        public byte[] BinaryContent { get; set; }

        [StringLength(100)]
        public string Description { get; set; }
    }
}