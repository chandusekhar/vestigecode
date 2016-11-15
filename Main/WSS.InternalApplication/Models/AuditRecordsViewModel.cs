using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WSS.InternalApplication.Models
{
    /// <summary>
    ///     Used to display the WSS Account (Customer Account)
    /// </summary>
    public class AuditRecordsViewModel
    {
        [ScaffoldColumn(false)]
        public int id { get; set; }

        [DisplayName("Date/Time")]
        public string DateTime { get; set; }

        public DateTime DateTimeAsDate { get; set; }

        [StringLength(20)]
        public string Time { get; set; }

        [DisplayName("Utility Account")]
        public string UtilityAccount { get; set; }

        [DisplayName("User")]
        public string UserId { get; set; }

        [DisplayName("Event Type")]
        public string EventType { get; set; }

        [DisplayName("Field Name")]
        public string FieldName { get; set; }

        [DisplayName("Old Value")]
        public string OldValue { get; set; }

        [DisplayName("New Value")]
        public string NewValue { get; set; }

        [DisplayName("Description")]
        public string Description { get; set; }

        public int WssAccountId { get; set; }
    }
}