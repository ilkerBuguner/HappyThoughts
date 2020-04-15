namespace HappyThoughts.Services.Data.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using HappyThoughts.Common;
    using HappyThoughts.Data.Models;
    using HappyThoughts.Data.Repositories;
    using HappyThoughts.Services.Data.Tests.Common;
    using HappyThoughts.Services.Data.Users;
    using HappyThoughts.Web.ViewModels.Users;
    using Xunit;

    public class UsersServiceTests
    {
        public UsersServiceTests()
        {
            MapperInitializer.InitializeMapper();
        }

        [Fact]
        public async Task GetUserAsViewModelByIdAsync_WithCorrectData_ShouldReturnUser()
        {
            var testUsername = "TestUsername";

            // Arrange
            var serviceFactory = new ServiceFactory();
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(serviceFactory.Context);
            var usersService = new UsersService(userRepository, serviceFactory.UserManager);

            var user = new ApplicationUser()
            {
                UserName = testUsername,
            };

            await userRepository.AddAsync(user);
            await userRepository.SaveChangesAsync();

            // Act
            var userId = userRepository.All().FirstOrDefault(x => x.UserName == testUsername).Id;
            var expectedUsername = testUsername;
            var userFromDb = await usersService.GetUserAsViewModelByIdAsync(userId);
            var actualUsername = userFromDb.UserName;

            // Assert
            Assert.Equal(expectedUsername, actualUsername);
            Assert.Equal(userFromDb.GetType().ToString(), typeof(ApplicationUserDetailsViewModel).ToString());
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("IncorrectId")]
        public async Task GetUserAsViewModelByIdAsync_WithIncorrectData_ShouldThrowArgumentException(string incorrectId)
        {
            // Arrange
            var serviceFactory = new ServiceFactory();
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(serviceFactory.Context);
            var usersService = new UsersService(userRepository, serviceFactory.UserManager);

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await usersService.GetUserAsViewModelByIdAsync(incorrectId);
            });
        }

        [Fact]
        public async Task GetUsernameById_WithCorrectData_ShouldReturnCorrectUsername()
        {
            var testUsername = "TestUsername";

            // Arrange
            var serviceFactory = new ServiceFactory();
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(serviceFactory.Context);
            var usersService = new UsersService(userRepository, serviceFactory.UserManager);

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
        [InlineData("IncorrectId")]
        public async Task GetUsernameById_WithIncorrectData_ShouldThrowArgumentException(string incorrectId)
        {
            // Arrange
            var serviceFactory = new ServiceFactory();
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(serviceFactory.Context);
            var usersService = new UsersService(userRepository, serviceFactory.UserManager);

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
            var serviceFactory = new ServiceFactory();
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(serviceFactory.Context);
            var usersService = new UsersService(userRepository, serviceFactory.UserManager);

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

        [Fact]
        public async Task BanAsync_ShouldSuccessfullyBanNormalUser()
        {
            var testUsername = "TestUsername";

            // Arrange
            var serviceFactory = new ServiceFactory();
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(serviceFactory.Context);
            var usersService = new UsersService(userRepository, serviceFactory.UserManager);

            await serviceFactory.SeedRoleAsync(GlobalConstants.BannedRoleName);

            var user = new ApplicationUser()
            {
                UserName = testUsername,
            };

            await userRepository.AddAsync(user);
            await userRepository.SaveChangesAsync();
            var userFromDb = userRepository.All().FirstOrDefault(x => x.UserName == testUsername);
            var userId = userFromDb.Id;

            // Act
            await usersService.BanAsync(userId);

            // Assert
            Assert.True(await serviceFactory.UserManager.IsInRoleAsync(userFromDb, GlobalConstants.BannedRoleName));
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("IncorrectId")]
        public async Task BanAsync_WithIncorrectData_ShouldThrowArgumentNullException(string incorrectId)
        {
            // Arrange
            var serviceFactory = new ServiceFactory();
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(serviceFactory.Context);
            var usersService = new UsersService(userRepository, serviceFactory.UserManager);

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await usersService.BanAsync(incorrectId);
            });
        }

        [Fact]
        public async Task UnbanAsync_ShouldSuccessfullyUnbanBannedUser()
        {
            var testUsername = "TestUsername";

            // Arrange
            var serviceFactory = new ServiceFactory();
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(serviceFactory.Context);
            var usersService = new UsersService(userRepository, serviceFactory.UserManager);

            await serviceFactory.SeedRoleAsync(GlobalConstants.BannedRoleName);

            var user = new ApplicationUser()
            {
                UserName = testUsername,
            };

            await userRepository.AddAsync(user);
            await userRepository.SaveChangesAsync();
            var userFromDb = userRepository.All().FirstOrDefault(x => x.UserName == testUsername);
            var userId = user.Id;
            await usersService.BanAsync(userId);

            // Act
            await usersService.UnbanAsync(userId);

            // Assert
            Assert.True(!await serviceFactory.UserManager.IsInRoleAsync(userFromDb, GlobalConstants.BannedRoleName));
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("IncorrectId")]
        public async Task UnbanAsync_WithIncorrectData_ShouldThrowArgumentNullException(string incorrectId)
        {
            // Arrange
            var serviceFactory = new ServiceFactory();
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(serviceFactory.Context);
            var usersService = new UsersService(userRepository, serviceFactory.UserManager);

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await usersService.UnbanAsync(incorrectId);
            });
        }

        [Fact]
        public async Task PromoteAsync_ShouldSuccessfullyPromoteUser()
        {
            var testUsername = "TestUsername";

            // Arrange
            var serviceFactory = new ServiceFactory();
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(serviceFactory.Context);
            var usersService = new UsersService(userRepository, serviceFactory.UserManager);

            await serviceFactory.SeedRoleAsync(GlobalConstants.BannedRoleName);
            await serviceFactory.SeedRoleAsync(GlobalConstants.ModeratorRoleName);

            var user = new ApplicationUser()
            {
                UserName = testUsername,
            };

            await userRepository.AddAsync(user);
            await userRepository.SaveChangesAsync();
            var userFromDb = userRepository.All().FirstOrDefault(x => x.UserName == testUsername);
            var userId = user.Id;

            // Act
            await usersService.PromoteAsync(userId);

            // Assert
            Assert.True(await serviceFactory.UserManager.IsInRoleAsync(userFromDb, GlobalConstants.ModeratorRoleName));
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("IncorrectId")]
        public async Task PromoteAsync_WithIncorrectData_ShouldThrowArgumentNullException(string incorrectId)
        {
            // Arrange
            var serviceFactory = new ServiceFactory();
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(serviceFactory.Context);
            var usersService = new UsersService(userRepository, serviceFactory.UserManager);

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await usersService.PromoteAsync(incorrectId);
            });
        }

        [Fact]
        public async Task DemoteAsync_ShouldSuccessfullyDemoteModerator()
        {
            var testUsername = "TestUsername";

            // Arrange
            var serviceFactory = new ServiceFactory();
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(serviceFactory.Context);
            var usersService = new UsersService(userRepository, serviceFactory.UserManager);

            await serviceFactory.SeedRoleAsync(GlobalConstants.ModeratorRoleName);

            var user = new ApplicationUser()
            {
                UserName = testUsername,
            };

            await userRepository.AddAsync(user);
            await userRepository.SaveChangesAsync();
            var userFromDb = userRepository.All().FirstOrDefault(x => x.UserName == testUsername);
            var userId = user.Id;

            // Act
            await usersService.Demote(userId);

            // Assert
            Assert.True(!await serviceFactory.UserManager.IsInRoleAsync(userFromDb, GlobalConstants.ModeratorRoleName));
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("IncorrectId")]
        public async Task DemoteAsync_WithIncorrectData_ShouldThrowArgumentNullException(string incorrectId)
        {
            // Arrange
            var serviceFactory = new ServiceFactory();
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(serviceFactory.Context);
            var usersService = new UsersService(userRepository, serviceFactory.UserManager);

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await usersService.Demote(incorrectId);
            });
        }

        [Fact]
        public async Task IsPromotedAsync_WhenUserIsPromoted_ShouldReturnCorrectResult()
        {
            var testUsername = "TestUsername";

            // Arrange
            var serviceFactory = new ServiceFactory();
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(serviceFactory.Context);
            var usersService = new UsersService(userRepository, serviceFactory.UserManager);

            await serviceFactory.SeedRoleAsync(GlobalConstants.ModeratorRoleName);

            var user = new ApplicationUser()
            {
                UserName = testUsername,
            };

            await userRepository.AddAsync(user);
            await userRepository.SaveChangesAsync();
            var userFromDb = userRepository.All().FirstOrDefault(x => x.UserName == testUsername);
            var userId = user.Id;
            await usersService.PromoteAsync(userId);

            // Act
            var actualResult = await usersService.IsPromotedAsync(userId);

            // Assert
            Assert.True(actualResult);
        }

        [Fact]
        public async Task IsPromotedAsync_WhenUserIsNotPromoted_ShouldReturnCorrectResult()
        {
            var testUsername = "TestUsername";

            // Arrange
            var serviceFactory = new ServiceFactory();
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(serviceFactory.Context);
            var usersService = new UsersService(userRepository, serviceFactory.UserManager);

            await serviceFactory.SeedRoleAsync(GlobalConstants.ModeratorRoleName);

            var user = new ApplicationUser()
            {
                UserName = testUsername,
            };

            await userRepository.AddAsync(user);
            await userRepository.SaveChangesAsync();
            var userFromDb = userRepository.All().FirstOrDefault(x => x.UserName == testUsername);
            var userId = user.Id;

            // Act
            var actualResult = await usersService.IsPromotedAsync(userId);

            // Assert
            Assert.False(actualResult);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("IncorrectId")]
        public async Task IsPromoted_WithIncorrectData_ShouldThrowArgumentNullException(string incorrectId)
        {
            // Arrange
            var serviceFactory = new ServiceFactory();
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(serviceFactory.Context);
            var usersService = new UsersService(userRepository, serviceFactory.UserManager);

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await usersService.IsPromotedAsync(incorrectId);
            });
        }

        [Fact]
        public async Task IsBannedAsync_WhenUserIsBanned_ShouldReturnCorrectResult()
        {
            var testUsername = "TestUsername";

            // Arrange
            var serviceFactory = new ServiceFactory();
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(serviceFactory.Context);
            var usersService = new UsersService(userRepository, serviceFactory.UserManager);

            await serviceFactory.SeedRoleAsync(GlobalConstants.BannedRoleName);

            var user = new ApplicationUser()
            {
                UserName = testUsername,
            };

            await userRepository.AddAsync(user);
            await userRepository.SaveChangesAsync();
            var userFromDb = userRepository.All().FirstOrDefault(x => x.UserName == testUsername);
            var userId = user.Id;
            await usersService.BanAsync(userId);

            // Act
            var actualResult = await usersService.IsBannedAsync(userId);

            // Assert
            Assert.True(actualResult);
        }

        [Fact]
        public async Task IsBannedAsync_WhenUserIsNotBanned_ShouldReturnCorrectResult()
        {
            var testUsername = "TestUsername";

            // Arrange
            var serviceFactory = new ServiceFactory();
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(serviceFactory.Context);
            var usersService = new UsersService(userRepository, serviceFactory.UserManager);

            await serviceFactory.SeedRoleAsync(GlobalConstants.ModeratorRoleName);

            var user = new ApplicationUser()
            {
                UserName = testUsername,
            };

            await userRepository.AddAsync(user);
            await userRepository.SaveChangesAsync();
            var userFromDb = userRepository.All().FirstOrDefault(x => x.UserName == testUsername);
            var userId = user.Id;

            // Act
            var actualResult = await usersService.IsBannedAsync(userId);

            // Assert
            Assert.False(actualResult);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("IncorrectId")]
        public async Task IsBannedAsync_WithIncorrectData_ShouldThrowArgumentNullException(string incorrectId)
        {
            // Arrange
            var serviceFactory = new ServiceFactory();
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(serviceFactory.Context);
            var usersService = new UsersService(userRepository, serviceFactory.UserManager);

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await usersService.IsBannedAsync(incorrectId);
            });
        }

        [Fact]
        public async Task IsAdminAsync_WhenUserIsAdmin_ShouldReturnCorrectResult()
        {
            var testUsername = "TestUsername";

            // Arrange
            var serviceFactory = new ServiceFactory();
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(serviceFactory.Context);
            var usersService = new UsersService(userRepository, serviceFactory.UserManager);

            await serviceFactory.SeedRoleAsync(GlobalConstants.AdministratorRoleName);

            var user = new ApplicationUser()
            {
                UserName = testUsername,
            };

            await userRepository.AddAsync(user);
            await userRepository.SaveChangesAsync();
            var userFromDb = userRepository.All().FirstOrDefault(x => x.UserName == testUsername);
            var userId = user.Id;
            await serviceFactory.UserManager.AddToRoleAsync(userFromDb, GlobalConstants.AdministratorRoleName);

            // Act
            var actualResult = await usersService.IsAdminAsync(userId);

            // Assert
            Assert.True(actualResult);
        }

        [Fact]
        public async Task IsAdminAsync_WhenUserIsNotAdmin_ShouldReturnCorrectResult()
        {
            var testUsername = "TestUsername";

            // Arrange
            var serviceFactory = new ServiceFactory();
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(serviceFactory.Context);
            var usersService = new UsersService(userRepository, serviceFactory.UserManager);

            await serviceFactory.SeedRoleAsync(GlobalConstants.ModeratorRoleName);

            var user = new ApplicationUser()
            {
                UserName = testUsername,
            };

            await userRepository.AddAsync(user);
            await userRepository.SaveChangesAsync();
            var userFromDb = userRepository.All().FirstOrDefault(x => x.UserName == testUsername);
            var userId = user.Id;

            // Act
            var actualResult = await usersService.IsAdminAsync(userId);

            // Assert
            Assert.False(actualResult);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("IncorrectId")]
        public async Task IsAdminAsync_WithIncorrectData_ShouldThrowArgumentNullException(string incorrectId)
        {
            // Arrange
            var serviceFactory = new ServiceFactory();
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(serviceFactory.Context);
            var usersService = new UsersService(userRepository, serviceFactory.UserManager);

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await usersService.IsAdminAsync(incorrectId);
            });
        }
    }
}
