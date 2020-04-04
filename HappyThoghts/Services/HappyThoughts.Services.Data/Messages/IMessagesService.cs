namespace HappyThoughts.Services.Data.Messages
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using HappyThoughts.Web.ViewModels.InputModels.Messages;
    using HappyThoughts.Web.ViewModels.Messages;

    public interface IMessagesService
    {
        Task CreateAsync(CreateMessageInputModel input);

        IEnumerable<MessageInfoViewModel> GetLastInboxMessagesOfUser(string userId);

        IEnumerable<MessageInfoViewModel> GetLastSentBoxMessagesOfUser(string userId);

        IEnumerable<MessageInfoViewModel> GetAllMessagesBySenderAndReceiverId(string senderId, string receiverId);
    }
}
