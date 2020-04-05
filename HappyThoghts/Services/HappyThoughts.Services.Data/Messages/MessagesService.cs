namespace HappyThoughts.Services.Data.Messages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using HappyThoughts.Data.Common.Repositories;
    using HappyThoughts.Data.Models;
    using HappyThoughts.Services.Mapping;
    using HappyThoughts.Web.ViewModels.InputModels.Messages;
    using HappyThoughts.Web.ViewModels.Messages;

    public class MessagesService : IMessagesService
    {
        private const string InvalidMessageIdErrorMessage = "Message with ID: {0} does not exist.";

        private readonly IDeletableEntityRepository<Message> messageRepository;

        public MessagesService(IDeletableEntityRepository<Message> messageRepository)
        {
            this.messageRepository = messageRepository;
        }

        public async Task<string> CreateAsync(string senderId, string receiverId, string content)
        {
            var message = new Message()
            {
                SenderId = senderId,
                ReceiverId = receiverId,
                Content = content,
            };

            await this.messageRepository.AddAsync(message);
            await this.messageRepository.SaveChangesAsync();

            return message.Id;
        }

        public async Task DeleteById(string messageId)
        {
            var message = await this.messageRepository.GetByIdWithDeletedAsync(messageId);

            if (message == null)
            {
                throw new ArgumentNullException(
                    string.Format(InvalidMessageIdErrorMessage, messageId));
            }

            this.messageRepository.Delete(message);
            await this.messageRepository.SaveChangesAsync();
        }

        public IEnumerable<MessageInfoViewModel> GetAllMessagesBySenderAndReceiverId(string senderId, string receiverId)
        {
            return this.messageRepository
                .All()
                .Where(x => (x.SenderId == senderId && x.ReceiverId == receiverId) || (x.SenderId == receiverId && x.ReceiverId == senderId))
                .To<MessageInfoViewModel>()
                .OrderBy(m => m.SentOn)
                .ToList();
        }

        public MessageInfoViewModel GetByIdAsViewModel(string messageId)
        {
            return this.messageRepository
                .All()
                .Where(m => m.Id == messageId)
                .To<MessageInfoViewModel>()
                .FirstOrDefault();
        }

        public IEnumerable<MessageInfoViewModel> GetLastInboxMessagesOfUser(string userId)
        {
            var inboxMessages = this.messageRepository.All()
                .Where(x => x.ReceiverId == userId)
                .To<MessageInfoViewModel>()
                .OrderByDescending(x => x.SentOn)
                .ToList();

            inboxMessages = inboxMessages.GroupBy(p => p.SenderId)
                                        .Select(g => g.First())
                                        .OrderByDescending(x => x.SentOn)
                                        .ToList();
            return inboxMessages;
        }

        public IEnumerable<MessageInfoViewModel> GetLastSentBoxMessagesOfUser(string userId)
        {
            var sentBoxMessages = this.messageRepository.All()
                .Where(x => x.SenderId == userId)
                .To<MessageInfoViewModel>()
                .OrderByDescending(x => x.SentOn)
                .ToList();

            sentBoxMessages = sentBoxMessages.GroupBy(p => p.ReceiverId)
                                        .Select(g => g.First())
                                        .OrderByDescending(x => x.SentOn)
                                        .ToList();

            return sentBoxMessages;
        }

    }
}
