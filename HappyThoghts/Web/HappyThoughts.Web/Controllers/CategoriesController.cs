namespace HappyThoughts.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using HappyThoughts.Services.Data.Categories;
    using HappyThoughts.Web.ViewModels.Categories;
    using HappyThoughts.Web.ViewModels.InputModels.Categories;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class CategoriesController : BaseController
    {
        private readonly ICategoriesService categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        [Authorize]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateCategoryInputModel input)
        {
            await this.categoriesService.CreateAsync(input);

            return this.Redirect("/");
        }

        [Authorize]
        public async Task<IActionResult> All()
        {
            var viewModel = await this.categoriesService.GetAllAsync<CategoryInfoViewModel>();
            return this.View(viewModel);
        }
    }
}
