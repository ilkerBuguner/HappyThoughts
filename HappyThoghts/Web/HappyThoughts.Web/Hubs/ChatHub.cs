namespace HappyThoughts.Web.Hubs
{
    using System.Threading.Tasks;

    using HappyThoughts.Data.Models;
    using HappyThoughts.Services.Data.Messages;
    using HappyThoughts.Web.ViewModels.Messages;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.SignalR;

    public class ChatHub : Hub
    {
        private readonly IMessagesService messagesService;
        private readonly UserManager<ApplicationUser> userManager;

        public ChatHub(IMessagesService messagesService, UserManager<ApplicationUser> userManager)
        {
            this.messagesService = messagesService;
            this.userManager = userManager;
        }

        public async Task Send(string message, string receiverId)
        {
            var senderId = this.userManager.GetUserId(this.Context.User);
            var sender = await this.userManager.FindByIdAsync(senderId);

            var messageId = await this.messagesService.CreateAsync(senderId, receiverId, message);

            var messageViewModel = this.messagesService.GetByIdAsViewModel(messageId);

            var chatViewModelForCurrentUser = new MessageChatViewModel()
            {
                SenderName = sender.UserName,
                SentOn = messageViewModel.SentOn.ToLocalTime().ToString(),
                Text = message,
                IsCurrentUserMessage = true,
            };

            var chatViewModelForOtherUser = new MessageChatViewModel()
            {
                SenderName = sender.UserName,
                SentOn = messageViewModel.SentOn.ToLocalTime().ToString(),
                Text = message,
                IsCurrentUserMessage = false,
            };

            await this.Clients.Users(receiverId).SendAsync(
                "NewMessage",
                chatViewModelForOtherUser);

            await this.Clients.Caller.SendAsync(
                "NewMessage",
                chatViewModelForCurrentUser);

        }
    }
}
