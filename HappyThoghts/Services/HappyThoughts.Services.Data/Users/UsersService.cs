namespace HappyThoughts.Services.Data.Users
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using HappyThoughts.Data.Common.Repositories;
    using HappyThoughts.Data.Models;
    using HappyThoughts.Services.Mapping;
    using HappyThoughts.Web.ViewModels.Users;
    using Microsoft.EntityFrameworkCore;

    public class UsersService : IUsersService
    {
        private const string InvalidUserIdErrorMessage = "User with ID: {0} does not exist.";

        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;

        public UsersService(IDeletableEntityRepository<ApplicationUser> userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<ApplicationUserDetailsViewModel> GetUserAsViewModelByIdAsync(string id)
        {
            var user = await this.userRepository
                .All()
                .Include(u => u.Topics)
                .To<ApplicationUserDetailsViewModel>()
                .FirstOrDefaultAsync(u => u.Id == id);

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

        public async Task<int> GetUsersCount()
        {
            var usersCount = await this.userRepository.All().CountAsync();

            return usersCount;
        }
    }
}
