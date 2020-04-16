namespace HappyThoughts.Services.Data.Tests
{
    using System.Threading.Tasks;

    using HappyThoughts.Common;
    using HappyThoughts.Data.Models;
    using HappyThoughts.Data.Repositories;
    using HappyThoughts.Services.Data.Forums;
    using HappyThoughts.Services.Data.Tests.Common;
    using Xunit;

    public class ForumsServiceTests
    {
        [Fact]
        public async Task GetForumStatsAsync_ShouldReturnCorrectResult()
        {
            // Arrange
            var serviceFactory = new ServiceFactory();
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(serviceFactory.Context);
            var categoryRepository = new EfDeletableEntityRepository<Category>(serviceFactory.Context);
            var topicRepository = new EfDeletableEntityRepository<Topic>(serviceFactory.Context);

            var forumsService = new ForumsService(
                topicRepository,
                userRepository,
                categoryRepository,
                serviceFactory.UserManager);

            await serviceFactory.SeedRoleAsync(GlobalConstants.AdministratorRoleName);
            await serviceFactory.SeedRoleAsync(GlobalConstants.ModeratorRoleName);

            await topicRepository.AddAsync(new Topic()
            {
                Title = "TestTitle",
            });

            await topicRepository.AddAsync(new Topic()
            {
                Title = "TestTitle",
            });

            await topicRepository.SaveChangesAsync();

            var firstUser = new ApplicationUser()
            {
                UserName = "TestUsername",
            };

            var secondUser = new ApplicationUser()
            {
                UserName = "SecondTestUsername",
            };

            await userRepository.AddAsync(firstUser);
            await userRepository.AddAsync(secondUser);

            await userRepository.SaveChangesAsync();

            await categoryRepository.AddAsync(new Category()
            {
                Name = "TestName",
            });

            await categoryRepository.SaveChangesAsync();

            await serviceFactory.UserManager.AddToRoleAsync(firstUser, GlobalConstants.AdministratorRoleName);
            await serviceFactory.UserManager.AddToRoleAsync(secondUser, GlobalConstants.ModeratorRoleName);

            // Act
            var expectedTopicsCount = 2;
            var expectedCategoriesCount = 1;
            var expectedUsersCount = 2;
            var expectedAdminsCount = 1;
            var expectedModeratorsCount = 1;
            var actualResult = await forumsService.GetForumStatsAsync();

            // Assert
            Assert.Equal(expectedTopicsCount, actualResult.TotalTopicsCount);
            Assert.Equal(expectedCategoriesCount, actualResult.TotalCategoriesCount);
            Assert.Equal(expectedUsersCount, actualResult.TotalUsersCount);
            Assert.Equal(expectedAdminsCount, actualResult.TotalAdminsCount);
            Assert.Equal(expectedModeratorsCount, actualResult.TotalModeratorsCount);
        }
    }
}
