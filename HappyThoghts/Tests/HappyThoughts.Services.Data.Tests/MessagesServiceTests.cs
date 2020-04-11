namespace HappyThoughts.Services.Data.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using HappyThoughts.Data.Models;
    using HappyThoughts.Data.Repositories;
    using HappyThoughts.Services.Data.Messages;
    using HappyThoughts.Services.Data.Tests.Common;
    using Xunit;

    public class MessagesServiceTests
    {
        public MessagesServiceTests()
        {
            MapperInitializer.InitializeMapper();
        }

        [Fact]
        public async Task CreateAsync_ShouldSuccessfullyCreate()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var messageRepository = new EfDeletableEntityRepository<Message>(context);
            var messagesService = new MessagesService(messageRepository);

            // Act
            var expectedMessagesCount = 1;
            await messagesService.CreateAsync("testSenderId", "testReceiverId", "testContent");
            var actualMEssagesCount = messageRepository.All().Count();

            // Assert
            Assert.Equal(expectedMessagesCount, actualMEssagesCount);
        }

        [Fact]
        public async Task DeleteByIdAsync_WithCorrectData_ShouldSuccessfullyDelete()
        {
            var testContent = "TestContent";

            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var messageRepository = new EfDeletableEntityRepository<Message>(context);
            var messagesService = new MessagesService(messageRepository);

            await messagesService.CreateAsync("testSenderId", "testReceiverId", testContent);
            var comment = messageRepository.All().FirstOrDefault(c => c.Content == testContent);

            // Act
            var expectedCommentsCount = 0;
            await messagesService.DeleteByIdAsync(comment.Id);
            var actualCommentsCount = messageRepository.All().Count();

            // Assert
            Assert.Equal(expectedCommentsCount, actualCommentsCount);
        }

        [Fact]
        public async Task DeleteByIdAsync_WithIncorrectData_ShouldThrowArgumentNullException()
        {
            var incorrectId = Guid.NewGuid().ToString();

            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var messageRepository = new EfDeletableEntityRepository<Message>(context);
            var messagesService = new MessagesService(messageRepository);

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await messagesService.DeleteByIdAsync(incorrectId);
            });
        }
    }
}
