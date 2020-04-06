namespace HappyThoughts.Services.Data.Messages
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using HappyThoughts.Data.Models;
    using HappyThoughts.Web.ViewModels.InputModels.Messages;
    using HappyThoughts.Web.ViewModels.Messages;

    public interface IMessagesService
    {
        Task<string> CreateAsync(string senderId, string receiverId, string content);

        Task DeleteByIdAsync(string messageId);

        MessageInfoViewModel GetByIdAsViewModel(string messageId);

        IEnumerable<MessageInfoViewModel> GetLastInboxMessagesOfUser(string userId);

        IEnumerable<MessageInfoViewModel> GetLastSentBoxMessagesOfUser(string userId);

        IEnumerable<MessageInfoViewModel> GetAllMessagesBySenderAndReceiverId(string senderId, string receiverId);
    }
}
