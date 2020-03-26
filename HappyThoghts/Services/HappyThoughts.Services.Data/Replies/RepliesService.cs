namespace HappyThoughts.Services.Data.Replies
{
    using System;
    using System.Threading.Tasks;

    using HappyThoughts.Data.Common.Repositories;
    using HappyThoughts.Data.Models;
    using HappyThoughts.Web.ViewModels.InputModels.Replies;

    public class RepliesService : IRepliesService
    {
        private const string InvalidReplyIdErrorMessage = "Reply with ID: {0} does not exist.";

        private const int ContentMinLength = 2;

        private readonly IDeletableEntityRepository<Reply> replyRepository;

        public RepliesService(IDeletableEntityRepository<Reply> replyRepository)
        {
            this.replyRepository = replyRepository;
        }

        public async Task CreateAsync(CreateReplyInputModel input)
        {
            var comment = new Reply()
            {
                Content = input.Content,
                AuthorId = input.AuthorId,
                TopicId = input.TopicId,
                RootCommentId = input.RootCommentId,
            };

            await this.replyRepository.AddAsync(comment);
            await this.replyRepository.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(string replyId)
        {
            var reply = await this.replyRepository.GetByIdWithDeletedAsync(replyId);

            if (reply == null)
            {
                throw new ArgumentNullException(
                    string.Format(InvalidReplyIdErrorMessage, replyId));
            }

            this.replyRepository.Delete(reply);
            await this.replyRepository.SaveChangesAsync();
        }

        public async Task EditAsync(string replyId, string replyContent)
        {
            var commentFromDb = await this.replyRepository
                .GetByIdWithDeletedAsync(replyId);

            if (commentFromDb == null)
            {
                throw new ArgumentNullException(
                     string.Format(InvalidReplyIdErrorMessage, replyId));
            }

            if (replyContent != null && replyContent.Length >= ContentMinLength && replyContent != commentFromDb.Content)
            {
                commentFromDb.Content = replyContent;
            }

            this.replyRepository.Update(commentFromDb);
            await this.replyRepository.SaveChangesAsync();
        }
    }
}
