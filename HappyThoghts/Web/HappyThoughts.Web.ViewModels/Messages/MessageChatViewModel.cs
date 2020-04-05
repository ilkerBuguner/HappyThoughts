namespace HappyThoughts.Web.ViewModels.Messages
{
    public class MessageChatViewModel
    {
        public string Text { get; set; }

        public string SentOn { get; set; }

        public string SenderName { get; set; }

        public bool IsCurrentUserMessage { get; set; }
    }
}
