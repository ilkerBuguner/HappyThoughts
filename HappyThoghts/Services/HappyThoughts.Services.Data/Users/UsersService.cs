namespace HappyThoughts.Services.Data.Users
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using HappyThoughts.Common;
    using HappyThoughts.Data.Common.Repositories;
    using HappyThoughts.Data.Models;
    using HappyThoughts.Services.Mapping;
    using HappyThoughts.Web.ViewModels.Users;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public class UsersService : IUsersService
    {
        private const string InvalidUserIdErrorMessage = "User with ID: {0} does not exist.";

        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public UsersService(
            IDeletableEntityRepository<ApplicationUser> userRepository,
            UserManager<ApplicationUser> userManager)
        {
            this.userRepository = userRepository;
            this.userManager = userManager;
        }

        public async Task<ApplicationUserDetailsViewModel> GetUserAsViewModelByIdAsync(string id)
        {
            var user = await this.userRepository
                .All()
                .Where(u => u.Id == id)
                .Include(u => u.Topics)
                //.Include(u => u.Followers).ThenInclude(x => x.FollowingUser)
                //.Include(u => u.Following).ThenInclude(x => x.FollowedUser)
                .To<ApplicationUserDetailsViewModel>()
                .FirstOrDefaultAsync();

            return user;
        }

        public string GetUsernameById(string id)
        {
            var user = this.userRepository.All().FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                throw new ArgumentException(InvalidUserIdErrorMessage, id);
            }

            return user.UserName;
        }

        public async Task<int> GetUsersCountAsync()
        {
            var usersCount = await this.userRepository.All().CountAsync();

            return usersCount;
        }

        public async Task BanAsync(string userId)
        {
            var userFromDb = await this.userRepository.GetByIdWithDeletedAsync(userId);

            if (userFromDb == null)
            {
                throw new ArgumentNullException(InvalidUserIdErrorMessage, userId);
            }

            if (await this.userManager.IsInRoleAsync(userFromDb, GlobalConstants.ModeratorRoleName))
            {
                await this.userManager.RemoveFromRoleAsync(userFromDb, GlobalConstants.ModeratorRoleName);
                await this.userManager.AddToRoleAsync(userFromDb, GlobalConstants.BannedRoleName);
            }
            else if (!await this.userManager.IsInRoleAsync(userFromDb, GlobalConstants.BannedRoleName))
            {
                await this.userManager.AddToRoleAsync(userFromDb, GlobalConstants.BannedRoleName);
            }
        }

        public async Task UnbanAsync(string userId)
        {
            var userFromDb = await this.userRepository.GetByIdWithDeletedAsync(userId);

            if (userFromDb == null)
            {
                throw new ArgumentNullException(InvalidUserIdErrorMessage, userId);
            }

            if (await this.userManager.IsInRoleAsync(userFromDb, GlobalConstants.BannedRoleName))
            {
                await this.userManager.RemoveFromRoleAsync(userFromDb, GlobalConstants.BannedRoleName);
            }
        }

        public async Task PromoteAsync(string userId)
        {
            var userFromDb = await this.userRepository.GetByIdWithDeletedAsync(userId);

            if (userFromDb == null)
            {
                throw new ArgumentNullException(InvalidUserIdErrorMessage, userId);
            }

            if (await this.userManager.IsInRoleAsync(userFromDb, GlobalConstants.BannedRoleName))
            {
                await this.userManager.RemoveFromRoleAsync(userFromDb, GlobalConstants.BannedRoleName);
                await this.userManager.AddToRoleAsync(userFromDb, GlobalConstants.ModeratorRoleName);
            }
            else if (!await this.userManager.IsInRoleAsync(userFromDb, GlobalConstants.AdministratorRoleName))
            {
                await this.userManager.AddToRoleAsync(userFromDb, GlobalConstants.ModeratorRoleName);
            }
        }

        public async Task Demote(string userId)
        {
            var userFromDb = await this.userRepository.GetByIdWithDeletedAsync(userId);

            if (userFromDb == null)
            {
                throw new ArgumentNullException(InvalidUserIdErrorMessage, userId);
            }

            await this.userManager.RemoveFromRoleAsync(userFromDb, GlobalConstants.ModeratorRoleName);
        }

        public async Task<bool> IsPromotedAsync(string userId)
        {
            var userFromDb = await this.userRepository.GetByIdWithDeletedAsync(userId);

            if (userFromDb == null)
            {
                throw new ArgumentNullException(InvalidUserIdErrorMessage, userId);
            }

            if (await this.userManager.IsInRoleAsync(userFromDb, GlobalConstants.ModeratorRoleName))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> IsBannedAsync(string userId)
        {
            var userFromDb = await this.userRepository.GetByIdWithDeletedAsync(userId);

            if (userFromDb == null)
            {
                throw new ArgumentNullException(InvalidUserIdErrorMessage, userId);
            }

            if (await this.userManager.IsInRoleAsync(userFromDb, GlobalConstants.BannedRoleName))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> IsAdminAsync(string userId)
        {
            var userFromDb = await this.userRepository.GetByIdWithDeletedAsync(userId);

            if (userFromDb == null)
            {
                throw new ArgumentNullException(InvalidUserIdErrorMessage, userId);
            }

            if (await this.userManager.IsInRoleAsync(userFromDb, GlobalConstants.AdministratorRoleName))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public IEnumerable<ApplicationUserInfoViewModel> GetCurrentUsersFollowers(string userId)
        {
            var users = this.userRepository
                .All()
                .Where(u => u.Id == userId)
                .SelectMany(x => x.Followers.Select(x => x.FollowingUser))
                .To<ApplicationUserInfoViewModel>()
                .ToList();

            return users;
        }

        public IEnumerable<ApplicationUserInfoViewModel> GetCurrentUsersFollowingUsers(string userId)
        {
            var users = this.userRepository
                .All()
                .Where(u => u.Id == userId)
                .SelectMany(x => x.Following.Select(x => x.FollowedUser))
                .To<ApplicationUserInfoViewModel>()
                .ToList();

            return users;
        }
    }
}
