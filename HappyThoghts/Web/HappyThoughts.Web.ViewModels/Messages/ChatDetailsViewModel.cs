using System.Collections.Generic;

namespace HappyThoughts.Web.ViewModels.Messages
{
    public class ChatDetailsViewModel
    {
        public string ReceiverId { get; set; }

        public string SenderId { get; set; }

        public IEnumerable<MessageInfoViewModel> Messages { get; set; }
    }
}
