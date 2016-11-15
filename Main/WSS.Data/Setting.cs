namespace WSS.Data
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("config.Settings")]
    public partial class Setting
    {
        public int SettingId { get; set; }

        [StringLength(50)]
        public string SettingName { get; set; }

        [StringLength(100)]
        public string Value { get; set; }

        public bool? IsEnabled { get; set; }

        [StringLength(100)]
        public string Description { get; set; }
    }
}