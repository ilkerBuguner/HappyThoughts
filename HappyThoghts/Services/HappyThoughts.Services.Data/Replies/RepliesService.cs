using HappyThoughts.Data.Common.Repositories;
using HappyThoughts.Data.Models;
using HappyThoughts.Web.ViewModels.InputModels.Replies;
using System.Threading.Tasks;

namespace HappyThoughts.Services.Data.Replies
{
    public class RepliesService : IRepliesService
    {
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
    }
}
