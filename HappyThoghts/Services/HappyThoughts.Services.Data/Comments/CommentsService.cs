namespace HappyThoughts.Services.Data.Comments
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using HappyThoughts.Data.Common.Repositories;
    using HappyThoughts.Data.Models;
    using HappyThoughts.Services.Mapping;
    using HappyThoughts.Web.ViewModels.Comments;
    using HappyThoughts.Web.ViewModels.InputModels.Comments;

    public class CommentsService : ICommentsService
    {
        private const string InvalidCommentIdErrorMessage = "Comment with ID: {0} does not exist.";

        private const int ContentMinLength = 2;

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

        public async Task EditAsync(string commentId, string commentContent)
        {
            var commentFromDb = await this.commentRepository
                .GetByIdWithDeletedAsync(commentId);

            if (commentFromDb == null)
            {
                throw new ArgumentException(
                     string.Format(InvalidCommentIdErrorMessage, commentId));
            }

            if (commentContent != null && commentContent.Length >= ContentMinLength && commentContent != commentFromDb.Content)
            {
                commentFromDb.Content = commentContent;
            }

            this.commentRepository.Update(commentFromDb);
            await this.commentRepository.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(string commentId)
        {
            var comment = await this.commentRepository.GetByIdWithDeletedAsync(commentId);

            if (comment == null)
            {
                throw new ArgumentException(
                    string.Format(InvalidCommentIdErrorMessage, commentId));
            }

            this.commentRepository.Delete(comment);
            await this.commentRepository.SaveChangesAsync();
        }

        public IEnumerable<CommentInfoViewModel> GetAllCommentsOfTopic(string topicId)
        {
            var comments = this.commentRepository
                .All()
                .Where(c => c.TopicId == topicId)
                .To<CommentInfoViewModel>()
                .OrderByDescending(c => c.CreatedOn)
                .ToList();

            return comments;
        }
    }
}
