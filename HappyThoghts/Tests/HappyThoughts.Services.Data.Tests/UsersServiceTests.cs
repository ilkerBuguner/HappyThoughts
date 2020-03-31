namespace HappyThoughts.Services.Data.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using HappyThoughts.Data.Models;
    using HappyThoughts.Data.Repositories;
    using HappyThoughts.Services.Data.Tests.Common;
    using HappyThoughts.Services.Data.Users;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Moq;
    using Xunit;

    public class UsersServiceTests
    {
        [Fact]
        public async Task GetUsernameById_WithCorrectData_ShouldReturnCorrectUsername()
        {
            var testUsername = "TestUsername";

            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(context);
            var userManager = new FakeUserManager();
            var usersService = new UsersService(userRepository, userManager);

            var user = new ApplicationUser()
            {
                UserName = testUsername,
            };

            await userRepository.AddAsync(user);
            await userRepository.SaveChangesAsync();
            var userId = userRepository.All().FirstOrDefault(x => x.UserName == testUsername).Id;

            // Act
            var expectedUsername = testUsername;
            var actualUsername = usersService.GetUsernameById(userId);

            // Assert
            Assert.Equal(expectedUsername, actualUsername);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("IncorectId")]
        public async Task GetUsernameById_WithIncorrectData_ShouldThrowArgumentException(string incorrectId)
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(context);
            var userManager = new FakeUserManager();
            var usersService = new UsersService(userRepository, userManager);

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                usersService.GetUsernameById(incorrectId);
            });
        }

        [Fact]
        public async Task GetUsersCountAsync_ShouldReturnCorrectUsersCount()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(context);
            var userManager = new FakeUserManager();
            var usersService = new UsersService(userRepository, userManager);

            for (int i = 0; i < 5; i++)
            {
                var user = new ApplicationUser()
                {
                    UserName = $"TestUsername + {i}",
                };

                await userRepository.AddAsync(user);
            }

            await userRepository.SaveChangesAsync();

            // Act
            var expectedUsersCount = 5;
            var actualUsersCount = await usersService.GetUsersCountAsync();

            // Assert
            Assert.Equal(expectedUsersCount, actualUsersCount);
        }

        //[Fact]
        //public async Task BanAsync_ShouldSuccessfullyBanNormalUser()
        //{
        //    var testUsername = "TestUsername";

        //    // Arrange
        //    var context = ApplicationDbContextInMemoryFactory.InitializeContext();
        //    var userRepository = new EfDeletableEntityRepository<ApplicationUser>(context);
        //    var userManager = new FakeUserManager();
        //    var usersService = new UsersService(userRepository, userManager);

        //    var user = new ApplicationUser()
        //    {
        //        UserName = testUsername,
        //    };

        //    await userRepository.AddAsync(user);
        //    await userRepository.SaveChangesAsync();
        //    var userId = userRepository.All().FirstOrDefault(x => x.UserName == testUsername).Id;

        //    // Act
        //    var expectedUserId = userId;
        //    await usersService.BanAsync(userId);
        //    user = userRepository.All().FirstOrDefault(x => x.UserName == testUsername);
        //    var actualUserId = user.Roles.FirstOrDefault().UserId;

        //    // Assert
        //    Assert.Equal(expectedUserId, actualUserId);
        //}
    }
}
