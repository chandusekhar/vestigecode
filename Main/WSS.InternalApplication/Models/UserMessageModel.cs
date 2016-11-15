using WSS.Common.Utilities;
using WSS.InternalApplication.Infrastructure;

namespace WSS.InternalApplication.Models
{
    public class UserMessageModel
    {
        public UserMessageType Type { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
    }
}