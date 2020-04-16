using HappyThoughts.Common;
using HappyThoughts.Data.Common.Repositories;
using HappyThoughts.Data.Models;
using HappyThoughts.Web.ViewModels.Forums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyThoughts.Services.Data.Forums
{
    public class ForumsService : IForumsService
    {
        private readonly IDeletableEntityRepository<Topic> topicRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly IDeletableEntityRepository<Category> categoryRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public ForumsService(
            IDeletableEntityRepository<Topic> topicRepository,
            IDeletableEntityRepository<ApplicationUser> userRepository,
            IDeletableEntityRepository<Category> categoryRepository,
            UserManager<ApplicationUser> userManager)
        {
            this.topicRepository = topicRepository;
            this.userRepository = userRepository;
            this.categoryRepository = categoryRepository;
            this.userManager = userManager;
        }

        public async Task<ForumStatsViewModel> GetForumStatsAsync()
        {
            var totalTopicsCount = this.topicRepository.All().Count();
            var totalUsersCount = this.userRepository.All().Count();
            var totalCategoriesCount = this.categoryRepository.All().Count();
            var totalAdminsCount = 0;
            var totalModeratorsCount = 0;

            foreach (var user in this.userRepository.All())
            {
                if (await this.userManager.IsInRoleAsync(user, GlobalConstants.AdministratorRoleName))
                {
                    totalAdminsCount++;
                }

                if (await this.userManager.IsInRoleAsync(user, GlobalConstants.ModeratorRoleName))
                {
                    totalModeratorsCount++;
                }
            }

            var viewModel = new ForumStatsViewModel()
            {
                TotalTopicsCount = totalTopicsCount,
                TotalUsersCount = totalUsersCount,
                TotalAdminsCount = totalAdminsCount,
                TotalModeratorsCount = totalModeratorsCount,
                TotalCategoriesCount = totalCategoriesCount,
            };

            return viewModel;
        }
    }
}
