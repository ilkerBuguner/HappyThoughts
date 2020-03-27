using HappyThoughts.Data.Models;
using HappyThoughts.Data.Repositories;
using HappyThoughts.Services.Data.Replies;
using HappyThoughts.Services.Data.Tests.Common;
using HappyThoughts.Web.ViewModels.InputModels.Replies;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace HappyThoughts.Services.Data.Tests
{
    public class RepliesServiceTests
    {
        [Fact]
        public async Task CreateAsync_ShouldSuccessfullyCreate()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var replyRepository = new EfDeletableEntityRepository<Reply>(context);
            var repliesService = new RepliesService(replyRepository);

            var inputModel = new CreateReplyInputModel()
            {
                Content = "TestContent",
            };

            // Act
            var expectedRepliesCount = 1;
            await repliesService.CreateAsync(inputModel);
            var actualRepliesCount = replyRepository.All().Count();

            // Assert
            Assert.Equal(expectedRepliesCount, actualRepliesCount);
        }

        [Fact]
        public async Task DeleteByIdAsync_WithCorrectData_ShouldSuccessfullyDelete()
        {
            var testContent = "TestContent";

            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var replyRepository = new EfDeletableEntityRepository<Reply>(context);
            var repliesService = new RepliesService(replyRepository);

            var inputModel = new CreateReplyInputModel()
            {
                Content = testContent,
            };

            await repliesService.CreateAsync(inputModel);
            var reply = replyRepository.All().FirstOrDefault(c => c.Content == testContent);

            // Act
            var expectedCommentsCount = 0;
            await repliesService.DeleteByIdAsync(reply.Id);
            var actualCommentsCount = replyRepository.All().Count();

            // Assert
            Assert.Equal(expectedCommentsCount, actualCommentsCount);
        }

        [Fact]
        public async Task DeleteByIdAsync_WithIncorrectData_ShouldThrowArgumentNullException()
        {
            var incorrectId = Guid.NewGuid().ToString();

            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var replyRepository = new EfDeletableEntityRepository<Reply>(context);
            var repliesService = new RepliesService(replyRepository);

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await repliesService.DeleteByIdAsync(incorrectId);
            });
        }

        [Fact]
        public async Task EditAsync_WithCorrectData_ShouldSuccessfullyEdit()
        {
            var testContent = "TestContent";

            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var replyRepository = new EfDeletableEntityRepository<Reply>(context);
            var repliesService = new RepliesService(replyRepository);

            var inputModel = new CreateReplyInputModel()
            {
                Content = testContent,
            };

            await repliesService.CreateAsync(inputModel);
            var reply = replyRepository.All().FirstOrDefault(c => c.Content == testContent);

            // Act
            var expectedReplyContent = "Edited_TestContent";
            await repliesService.EditAsync(reply.Id, expectedReplyContent);
            var actualReplyContent = reply.Content;

            // Assert
            reply = await replyRepository.GetByIdWithDeletedAsync(reply.Id);
            Assert.Equal(expectedReplyContent, actualReplyContent);
        }

        [Fact]
        public async Task EditAsync_WithIncorrectData_ShouldThrowArgumentNullException()
        {
            var incorrectId = Guid.NewGuid().ToString();

            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var replyRepository = new EfDeletableEntityRepository<Reply>(context);
            var repliesService = new RepliesService(replyRepository);

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await repliesService.EditAsync(incorrectId, "RandomContent");
            });
        }
    }
}
