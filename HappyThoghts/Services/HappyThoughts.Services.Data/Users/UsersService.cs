namespace HappyThoughts.Services.Data.Users
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using HappyThoughts.Data.Common.Repositories;
    using HappyThoughts.Data.Models;
    using HappyThoughts.Services.Mapping;
    using HappyThoughts.Web.ViewModels.Users;
    using Microsoft.EntityFrameworkCore;

    public class UsersService : IUsersService
    {
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
    }
}
