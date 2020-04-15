namespace HappyThoughts.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using HappyThoughts.Services.Data.Messages;
    using HappyThoughts.Services.Data.Users;
    using HappyThoughts.Web.ViewModels.Messages;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class MessagesController : BaseController
    {
        private readonly IMessagesService messagesService;
        private readonly IUsersService usersService;

        public MessagesController(IMessagesService messagesService, IUsersService usersService)
        {
            this.messagesService = messagesService;
            this.usersService = usersService;
        }

        [Authorize]
        public async Task<IActionResult> Inbox()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var viewModel = this.messagesService.GetLastInboxMessagesOfUser(userId);

            return this.View(viewModel);
        }

        [Authorize]
        public async Task<IActionResult> SentBox()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var viewModel = this.messagesService.GetLastSentBoxMessagesOfUser(userId);

            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult Chat(string receiverId)
        {
            var senderId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var messages = this.messagesService.GetAllMessagesBySenderAndReceiverId(senderId, receiverId);

            var viewModel = new ChatDetailsViewModel()
            {
                ReceiverId = receiverId,
                SenderId = senderId,
                Messages = messages,
            };

            var receiverUserName = this.usersService.GetUsernameById(receiverId);
            this.ViewData["ReceiverName"] = receiverUserName;

            return this.View(viewModel);
        }

        public async Task<IActionResult> Delete(string messageId, string receiverId, string senderId)
        {
            var currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (currentUserId == senderId)
            {
                await this.messagesService.DeleteByIdAsync(messageId);
            }

            return this.RedirectToAction(nameof(this.Chat), new { receiverId = receiverId });
        }
    }
}
