using WSS.Common.Utilities;

namespace WSS.CustomerApplication.Models
{
    public class UserMessageModel
    {
        public UserMessageType Type { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
    }
}