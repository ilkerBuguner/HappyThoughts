using HappyThoughts.Data.Models;
using HappyThoughts.Data.Repositories;
using HappyThoughts.Services.Data.Tests.Common;
using HappyThoughts.Services.Data.TopicReports;
using HappyThoughts.Web.ViewModels.InputModels.TopicReports;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace HappyThoughts.Services.Data.Tests
{
    public class TopicReportsServiceTests
    {
        [Fact]
        public async Task SendAsync_ShouldSuccessfullyCreateTopicReport()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var topicReportRepository = new EfDeletableEntityRepository<TopicReport>(context);
            var topicReportsService = new TopicReportsService(topicReportRepository);

            var inputModel = new CreateTopicReportInputModel()
            {
                Title = "TestTitle",
                Description = "TestDescription",
            };

            // Act
            var expectedTopicReportsCount = 1;
            await topicReportsService.SendAsync(inputModel);
            var actualTopicReportsCount = topicReportRepository.All().Count();

            // Assert
            Assert.Equal(expectedTopicReportsCount, actualTopicReportsCount);
        }

        [Fact]
        public async Task DeleteByIdAsync_WithCorrectData_ShouldSuccessfullyDelete()
        {
            var testTitle = "TestTitle";

            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var topicReportRepository = new EfDeletableEntityRepository<TopicReport>(context);
            var topicReportsService = new TopicReportsService(topicReportRepository);

            var inputModel = new CreateTopicReportInputModel()
            {
                Title = "TestTitle",
                Description = "TestDescription",
            };

            await topicReportsService.SendAsync(inputModel);
            var topicReport = topicReportRepository.All().FirstOrDefault(c => c.Title == testTitle);

            // Act
            var expectedTopicReportsCount = 0;
            await topicReportsService.DeleteByIdAsync(topicReport.Id);
            var actualCommentsCount = topicReportRepository.All().Count();

            // Assert
            Assert.Equal(expectedTopicReportsCount, actualCommentsCount);
        }

        [InlineData("")]
        [InlineData(null)]
        [InlineData("IncorrectId")]
        [Theory]
        public async Task DeleteByIdAsync_WithIncorrectData_ShouldThrowArgumentNullException(string incorrectId)
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var topicReportRepository = new EfDeletableEntityRepository<TopicReport>(context);
            var topicReportsService = new TopicReportsService(topicReportRepository);

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await topicReportsService.DeleteByIdAsync(incorrectId);
            });
        }
    }
}
