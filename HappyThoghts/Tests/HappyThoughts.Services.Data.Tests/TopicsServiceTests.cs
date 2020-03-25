namespace HappyThoughts.Services.Data.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using HappyThoughts.Data.Models;
    using HappyThoughts.Data.Repositories;
    using HappyThoughts.Services.Data.Tests.Common;
    using HappyThoughts.Services.Data.Topics;
    using HappyThoughts.Web.ViewModels.InputModels;
    using Xunit;

    public class TopicsServiceTests
    {
        [Fact]
        public async Task CreateAsync_ShouldSuccessfullyCreate()
        {
            // Arrange
            var expectedTopicsCount = 1;

            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var topicRepository = new EfDeletableEntityRepository<Topic>(context);
            var topicsService = new TopicsService(topicRepository);

            var inputModel = new CreateTopicInputModel()
            {
                Title = "TestTitle",
                Content = "TestContent_TestContent_TestContent_TestContent_TestContent_TestContent",
                AuthorId = Guid.NewGuid().ToString(),
                CategoryId = Guid.NewGuid().ToString(),
            };

            // Act
            await topicsService.CreateAsync(inputModel);

            // Assert
            Assert.Equal(expectedTopicsCount, topicRepository.All().Count());
        }

        [Fact]
        public async Task DeleteByIdAsync_WithCorrectData_ShouldSuccessfullyDelete()
        {
            // Arrange
            var expectedTopicsCount = 0;

            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var topicRepository = new EfDeletableEntityRepository<Topic>(context);
            var topicsService = new TopicsService(topicRepository);

            var inputModel = new CreateTopicInputModel()
            {
                Title = "TestTitle",
                Content = "TestContent_TestContent_TestContent_TestContent_TestContent_TestContent",
                AuthorId = Guid.NewGuid().ToString(),
                CategoryId = Guid.NewGuid().ToString(),
            };

            await topicsService.CreateAsync(inputModel);
            var topic = topicRepository.All().FirstOrDefault(t => t.Title == "TestTitle");

            // Act
            await topicsService.DeleteByIdAsync(topic.Id);

            // Assert
            Assert.Equal(expectedTopicsCount, topicRepository.All().Count());
        }

        [Fact]
        public async Task DeleteByIdAsync_WithIncorrectData_ShouldThrowArgumentNullException()
        {
            // Arrange
            var nonExistentId = Guid.NewGuid().ToString();

            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var topicRepository = new EfDeletableEntityRepository<Topic>(context);
            var topicsService = new TopicsService(topicRepository);

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await topicsService.DeleteByIdAsync(nonExistentId);
            });
        }
    }
}
