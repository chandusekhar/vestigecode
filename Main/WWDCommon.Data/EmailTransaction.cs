namespace WWDCommon.Data
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("common.EmailTransactions")]
    public partial class EmailTransaction
    {
        public int EmailTransactionID { get; set; }

        [StringLength(50)]
        public string EmailTo { get; set; }

        public bool? IsProcessed { get; set; }

        [StringLength(10)]
        public string SourcePackage { get; set; }

        public int? TemplateId { get; set; }

        public string Parameters { get; set; }

        [StringLength(50)]
        public string ReferenceType { get; set; }

        [StringLength(50)]
        public string ReferenceValue { get; set; }
    }
}