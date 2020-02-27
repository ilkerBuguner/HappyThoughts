namespace HappyThoughts.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using HappyThoughts.Services.Data.Categories;
    using HappyThoughts.Services.Data.Topics;
    using HappyThoughts.Web.ViewModels.Categories;
    using HappyThoughts.Web.ViewModels.InputModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class TopicsController : BaseController
    {
        private readonly ITopicsService topicsService;
        private readonly ICategoriesService categoriesService;

        public TopicsController(ITopicsService topicsService, ICategoriesService categoriesService)
        {
            this.topicsService = topicsService;
            this.categoriesService = categoriesService;
        }

        [Authorize]
        public async Task<IActionResult> Create()
        {
            var categories = await this.categoriesService.GetAllAsync<CategoryInfoViewModel>();

            var viewModel = new CreateTopicInputModel()
            {
                Categories = categories,
            };
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateTopicInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            input.AuthorId = userId;
            input.CategoryId = this.categoriesService.GetIdByNameAsync(input.CategoryName);
            await this.topicsService.CreateAsync(input);

            return this.Redirect("/");
        }

        public async Task<IActionResult> Details(string topicId)
        {
            var viewModel = await this.topicsService.GetByIdAsViewModelAsync(topicId);

            return this.View(viewModel);
        }
    }
}
