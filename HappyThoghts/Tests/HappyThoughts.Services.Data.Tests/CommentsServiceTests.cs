namespace HappyThoughts.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using HappyThoughts.Data.Models;
    using HappyThoughts.Data.Repositories;
    using HappyThoughts.Services.Data.Comments;
    using HappyThoughts.Services.Data.Tests.Common;
    using HappyThoughts.Services.Data.Topics;
    using HappyThoughts.Web.ViewModels.Comments;
    using HappyThoughts.Web.ViewModels.InputModels;
    using HappyThoughts.Web.ViewModels.InputModels.Comments;
    using Xunit;

    public class CommentsServiceTests
    {
        public CommentsServiceTests()
        {
            MapperInitializer.InitializeMapper();
        }

        [Fact]
        public async Task CreateAsync_ShouldSuccessfullyCreate()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var commentRepository = new EfDeletableEntityRepository<Comment>(context);
            var commentsService = new CommentsService(commentRepository);

            var inputModel = new CreateCommentInputModel()
            {
                CommentContent = "TestContent",
            };

            // Act
            var expectedCommentsCount = 1;
            await commentsService.CreateAsync(inputModel);
            var actualCommentsCount = commentRepository.All().Count();

            // Assert
            Assert.Equal(expectedCommentsCount, actualCommentsCount);
        }

        [Fact]
        public async Task EditAsync_WithCorrectData_ShouldSuccessfullyEdit()
        {
            var testContent = "TestContent";

            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var commentRepository = new EfDeletableEntityRepository<Comment>(context);
            var commentsService = new CommentsService(commentRepository);

            var inputModel = new CreateCommentInputModel()
            {
                CommentContent = testContent,
            };

            await commentsService.CreateAsync(inputModel);
            var comment = commentRepository.All().FirstOrDefault(c => c.Content == testContent);

            // Act
            var expectedCommentContent = "Edited_TestContent";
            await commentsService.EditAsync(comment.Id, expectedCommentContent);
            var actualCommentsContent = comment.Content;

            // Assert
            comment = await commentRepository.GetByIdWithDeletedAsync(comment.Id);
            Assert.Equal(expectedCommentContent, actualCommentsContent);
        }

        [Fact]
        public async Task EditAsync_WithIncorrectData_ShouldThrowArgumentNullException()
        {
            var incorrectId = Guid.NewGuid().ToString();

            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var commentRepository = new EfDeletableEntityRepository<Comment>(context);
            var commentsService = new CommentsService(commentRepository);

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await commentsService.EditAsync(incorrectId, "RandomContent");
            });
        }

        [Fact]
        public async Task DeleteByIdAsync_WithCorrectData_ShouldSuccessfullyDelete()
        {
            var testContent = "TestContent";

            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var commentRepository = new EfDeletableEntityRepository<Comment>(context);
            var commentsService = new CommentsService(commentRepository);

            var inputModel = new CreateCommentInputModel()
            {
                CommentContent = testContent,
            };

            await commentsService.CreateAsync(inputModel);
            var comment = commentRepository.All().FirstOrDefault(c => c.Content == testContent);

            // Act
            var expectedCommentsCount = 0;
            await commentsService.DeleteByIdAsync(comment.Id);
            var actualCommentsCount = commentRepository.All().Count();

            // Assert
            Assert.Equal(expectedCommentsCount, actualCommentsCount);
        }

        [Fact]
        public async Task DeleteByIdAsync_WithIncorrectData_ShouldThrowArgumentNullException()
        {
            var incorrectId = Guid.NewGuid().ToString();

            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var commentRepository = new EfDeletableEntityRepository<Comment>(context);
            var commentsService = new CommentsService(commentRepository);

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await commentsService.DeleteByIdAsync(incorrectId);
            });
        }

        //[Fact]
        //public async Task GetAllCommentsOfTopic_ShouldReturnCorrectResult()
        //{
        //    var testContent = "TestContent";

        //    // Arrange
        //    var context = ApplicationDbContextInMemoryFactory.InitializeContext();
        //    var topicRepository = new EfDeletableEntityRepository<Topic>(context);
        //    var userRepository = new EfDeletableEntityRepository<ApplicationUser>(context);
        //    var topicsService = new TopicsService(topicRepository, userRepository);

        //    var commentRepository = new EfDeletableEntityRepository<Comment>(context);
        //    var commentsService = new CommentsService(commentRepository);

        //    var topicInputModel = new CreateTopicInputModel()
        //    {
        //        Title = "TestTitle",
        //        Content = "TestContent_TestContent_TestContent_TestContent_TestContent_TestContent",
        //    };

        //    await topicsService.CreateAsync(topicInputModel);
        //    var topic = topicRepository.All().FirstOrDefault(t => t.Title == "TestTitle");
        //    var commentInputModel = new CreateCommentInputModel()
        //    {
        //        CommentContent = testContent,
        //        TopicId = topic.Id,
        //    };

        //    await commentsService.CreateAsync(commentInputModel);

        //    // Act
        //    var expectedCommentsCount = 1;
        //    var actualResult = commentsService.GetAllCommentsOfTopic(topic.Id);
        //    var actualCommentsCount = actualResult.Count();

        //    // Assert
        //    Assert.True(actualResult is CommentInfoViewModel);
        //    Assert.Equal(expectedCommentsCount, actualCommentsCount);

        //}
    }
}
