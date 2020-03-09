namespace HappyThoughts.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using HappyThoughts.Common;
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

        public async Task<IActionResult> ByName(string name, int page = GlobalConstants.DefaultPageNumber)
        {
            var topicsServiceModel = this.topicsService.GetTopicsByCategoryName(name, page);

            var category = this.categoriesService.GetCategoryByName(name);

            var viewModel = new TopicsByCategoryNameViewModel()
            {
                CategoryName = category.Name,
                PictureUrl = category.PictureUrl,
                CategoryTopics = new TopicsListingViewModel()
                {
                    TotalTopicsCount = topicsServiceModel.TotalTopicsCount,
                    CurrentPage = page,
                    Topics = topicsServiceModel.Topics,
                    Categories = await this.categoriesService.GetAllAsync<CategoryInfoViewModel>(),
                },
            };

            return this.View(viewModel);
        }
    }
}
