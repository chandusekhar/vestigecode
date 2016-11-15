namespace WSS.CustomerApplication.Models
{
    public class UnsubscribeViewModel
    {
        public string ActionToken { get; set; }

        public string EmailAddress { get; set; }

        public int WssAccountId { get; set; }

        public int WssAdditionalEmailId { get; set; }
    }
}