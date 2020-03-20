namespace HappyThoughts.Services.Data.Comments
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using HappyThoughts.Data.Common.Repositories;
    using HappyThoughts.Data.Models;
    using HappyThoughts.Services.Mapping;
    using HappyThoughts.Web.ViewModels.InputModels.Comments;
    using Microsoft.EntityFrameworkCore;

    public class CommentsService : ICommentsService
    {
        private const string InvalidCommentIdErrorMessage = "Comment with ID: {0} does not exist.";

        private readonly IDeletableEntityRepository<Comment> commentRepository;

        public CommentsService(IDeletableEntityRepository<Comment> commentRepository)
        {
            this.commentRepository = commentRepository;
        }

        public async Task CreateAsync(CreateCommentInputModel input)
        {
            var comment = new Comment()
            {
                Content = input.CommentContent,
                AuthorId = input.AuthorId,
                TopicId = input.TopicId,
            };

            await this.commentRepository.AddAsync(comment);
            await this.commentRepository.SaveChangesAsync();
        }

        public async Task<T[]> GetAllAsync<T>()
        {
            return await this.commentRepository
                .All()
                .To<T>()
                .ToArrayAsync();
        }

        public IQueryable<T> GetAllAsQueryable<T>()
        {
            return this.commentRepository
                .All()
                .To<T>();
        }

        public async Task DeleteByIdAsync(string commentId)
        {
            var comment = await this.commentRepository.GetByIdWithDeletedAsync(commentId);

            if (comment == null)
            {
                throw new ArgumentNullException(
                    string.Format(InvalidCommentIdErrorMessage, commentId));
            }

            this.commentRepository.Delete(comment);
            await this.commentRepository.SaveChangesAsync();
        }
    }
}
