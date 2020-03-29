namespace HappyThoughts.Services.Data.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using HappyThoughts.Data.Models;
    using HappyThoughts.Data.Repositories;
    using HappyThoughts.Services.Data.Tests.Common;
    using HappyThoughts.Services.Data.UserReports;
    using HappyThoughts.Web.ViewModels.InputModels.UserReports;
    using Xunit;

    public class UserReportsServiceTests
    {
        [Fact]
        public async Task SendAsync_ShouldSuccessfullyCreateTopicReport()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var userReportRepository = new EfDeletableEntityRepository<UserReport>(context);
            var userReportsService = new UserReportsService(userReportRepository);

            var inputModel = new CreateUserReportInputModel()
            {
                Title = "TestTitle",
                Description = "TestDescription",
            };

            // Act
            var expectedUserReportsCount = 1;
            await userReportsService.SendAsync(inputModel);
            var actualUserReportsCount = userReportRepository.All().Count();

            // Assert
            Assert.Equal(expectedUserReportsCount, actualUserReportsCount);
        }

        [Fact]
        public async Task DeleteByIdAsync_WithCorrectData_ShouldSuccessfullyDelete()
        {
            var testTitle = "TestTitle";

            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var userReportRepository = new EfDeletableEntityRepository<UserReport>(context);
            var userReportsService = new UserReportsService(userReportRepository);

            var inputModel = new CreateUserReportInputModel()
            {
                Title = "TestTitle",
                Description = "TestDescription",
            };

            await userReportsService.SendAsync(inputModel);
            var topicReport = userReportRepository.All().FirstOrDefault(c => c.Title == testTitle);

            // Act
            var expectedTopicReportsCount = 0;
            await userReportsService.DeleteByIdAsync(topicReport.Id);
            var actualCommentsCount = userReportRepository.All().Count();

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
            var userReportRepository = new EfDeletableEntityRepository<UserReport>(context);
            var userReportsService = new UserReportsService(userReportRepository);

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await userReportsService.DeleteByIdAsync(incorrectId);
            });
        }
    }
}
