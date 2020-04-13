namespace HappyThoughts.Services.Data.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using HappyThoughts.Data.Models;
    using HappyThoughts.Data.Repositories;
    using HappyThoughts.Services.Data.Tests.Common;
    using HappyThoughts.Services.Data.UsersFollowers;
    using Xunit;

    public class UsersFollowersServiceTests
    {
        [Fact]
        public async Task FollowAsync_ShouldSuccessfullyFollowUser()
        {
            string firstTestUserId = "FirstTestUserId";
            string secondTestUserId = "SecondTestUserId";

            // Arrange
            var serviceFactory = new ServiceFactory();
            var userFollowerRepository = new EfDeletableEntityRepository<UserFollower>(serviceFactory.Context);
            var usersFollowersService = new UsersFollowersService(userFollowerRepository);

            // Act
            await usersFollowersService.FollowAsync(firstTestUserId, secondTestUserId);
            var userFollower = userFollowerRepository.All().FirstOrDefault();

            // Asert
            Assert.Equal(userFollower.FollowingUserId, firstTestUserId);
            Assert.Equal(userFollower.FollowedUserId, secondTestUserId);
        }

        [Fact]
        public async Task UnfollowAsync_ShouldUnfollowUserSuccessfully()
        {
            string firstTestUserId = "FirstTestUserId";
            string secondTestUserId = "SecondTestUserId";

            // Arrange
            var serviceFactory = new ServiceFactory();
            var userFollowerRepository = new EfDeletableEntityRepository<UserFollower>(serviceFactory.Context);
            var usersFollowersService = new UsersFollowersService(userFollowerRepository);

            await usersFollowersService.FollowAsync(firstTestUserId, secondTestUserId);

            // Act
            await usersFollowersService.UnfollowAsync(firstTestUserId, secondTestUserId);
            var expectedAllUsersFollowersCount = 0;
            var actualAllUSersFollowersCount = userFollowerRepository.All().Count();

            // Asert
            Assert.Equal(expectedAllUsersFollowersCount, actualAllUSersFollowersCount);
        }

        [Theory]
        [InlineData("", "")]
        [InlineData(null, null)]
        [InlineData("IncorrectId", "IncorrectId")]
        public async Task UnfollowAsync_WithIncorrectData_ShouldThrowArgumentException(string incorrectFollowerId, string incorrectFollowedUserId)
        {

            // Arrange
            var serviceFactory = new ServiceFactory();
            var userFollowerRepository = new EfDeletableEntityRepository<UserFollower>(serviceFactory.Context);
            var usersFollowersService = new UsersFollowersService(userFollowerRepository);

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await usersFollowersService.UnfollowAsync(incorrectFollowerId, incorrectFollowedUserId);
            });
        }

        [Fact]
        public async Task IsFollowing_WhenFollowing_ShouldReturnCorrectResult()
        {
            string firstTestUserId = "FirstTestUserId";
            string secondTestUserId = "SecondTestUserId";

            // Arrange
            var serviceFactory = new ServiceFactory();
            var userFollowerRepository = new EfDeletableEntityRepository<UserFollower>(serviceFactory.Context);
            var usersFollowersService = new UsersFollowersService(userFollowerRepository);

            await usersFollowersService.FollowAsync(firstTestUserId, secondTestUserId);

            // Act
            bool actualIsFollowingResult = usersFollowersService.IsFollowing(firstTestUserId, secondTestUserId);

            // Asert
            Assert.True(actualIsFollowingResult);
        }

        [Fact]
        public void IsFollowing_WhenNotFollowing_ShouldReturnCorrectResult()
        {
            string firstTestUserId = "FirstTestUserId";
            string secondTestUserId = "SecondTestUserId";

            // Arrange
            var serviceFactory = new ServiceFactory();
            var userFollowerRepository = new EfDeletableEntityRepository<UserFollower>(serviceFactory.Context);
            var usersFollowersService = new UsersFollowersService(userFollowerRepository);

            // Act
            bool actualIsFollowingResult = usersFollowersService.IsFollowing(firstTestUserId, secondTestUserId);

            // Asert
            Assert.False(actualIsFollowingResult);
        }
    }
}
