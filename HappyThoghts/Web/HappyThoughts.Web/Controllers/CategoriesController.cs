namespace HappyThoughts.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using HappyThoughts.Services.Data.Categories;
    using HappyThoughts.Services.Data.Topics;
    using HappyThoughts.Web.ViewModels.Categories;
    using HappyThoughts.Web.ViewModels.InputModels.Categories;
    using HappyThoughts.Web.ViewModels.Topics;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class CategoriesController : BaseController
    {
        private readonly ICategoriesService categoriesService;
        private readonly ITopicsService topicsService;

        public CategoriesController(ICategoriesService categoriesService, ITopicsService topicsService)
        {
            this.categoriesService = categoriesService;
            this.topicsService = topicsService;
        }

        public async Task<IActionResult> All()
        {
            var viewModel = await this.categoriesService.GetAllAsync<CategoryInfoViewModel>();
            return this.View(viewModel);
        }

        public async Task<IActionResult> ByName(string name)
        {
            var topics = this.topicsService.GetAllAsQueryable<TopicInfoViewModel>()
                .Where(t => t.CategoryName == name)
                .OrderByDescending(t => t.CreatedOn)
                .ToList();

            var viewModel = new TopicsByCategoryNameViewModel()
            {
                CategoryName = name,
                CategoryTopics = new TopicsListingViewModel()
                {
                    Topics = topics,
                    Categories = await this.categoriesService.GetAllAsync<CategoryInfoViewModel>(),
                },
            };

            return this.View(viewModel);
        }
    }
}
