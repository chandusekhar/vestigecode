namespace WSS.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("wss.WssAccount")]
    public partial class WssAccount
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public WssAccount()
        {
            LinkedUtilityAccounts = new HashSet<LinkedUtilityAccount>();
            AdditionalEmailAddresses = new HashSet<AdditionalEmailAddress>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WSSAccountId { get; set; }

        [StringLength(100)]
        public string PrimaryEmailAddress { get; set; }

        [Required]
        [StringLength(25)]
        public string WssAccountStatusCode { get; set; }

        public int? FailedResendActivationAttempts { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? AgreeTermsAndConditionsDate { get; set; }

        [StringLength(128)]
        public string AuthenticationId { get; set; }

        [StringLength(500)]
        public string SecurityQuestion { get; set; }

        [StringLength(500)]
        public string SecurityQuestionAnswer { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LinkedUtilityAccount> LinkedUtilityAccounts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AdditionalEmailAddress> AdditionalEmailAddresses { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Action> Actions { get; set; }

        public string ActivationToken { get; set; }

        public bool IsActive { get; set; }
    }
}