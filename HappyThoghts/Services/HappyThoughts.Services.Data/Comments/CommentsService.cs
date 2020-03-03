namespace HappyThoughts.Services.Data.Comments
{
    using System.Threading.Tasks;

    using HappyThoughts.Data.Common.Repositories;
    using HappyThoughts.Data.Models;
    using HappyThoughts.Services.Mapping;
    using HappyThoughts.Web.ViewModels.InputModels.Comments;
    using Microsoft.EntityFrameworkCore;

    public class CommentsService : ICommentsService
    {
        private readonly IDeletableEntityRepository<Comment> commentRepository;

        public CommentsService(IDeletableEntityRepository<Comment> commentRepository)
        {
            this.commentRepository = commentRepository;
        }

        public async Task CreateAsync(CreateCommentInputModel input)
        {
            var comment = new Comment()
            {
                Content = input.Content,
                AuthorId = input.AuthorId,
                TopicId = input.TopicId,
            };

            await this.commentRepository.AddAsync(comment);
            await this.commentRepository.SaveChangesAsync();
        }

        public async Task<T[]> GetAllAsync<T>()
        {
            return await this.commentRepository
                .AllAsNoTracking()
                .To<T>()
                .ToArrayAsync();
        }
    }
}
