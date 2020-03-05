namespace HappyThoughts.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using HappyThoughts.Common;
    using HappyThoughts.Services.Data.Categories;
    using HappyThoughts.Web.Controllers;
    using HappyThoughts.Web.ViewModels.Categories;
    using HappyThoughts.Web.ViewModels.InputModels.Categories;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
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

            return this.Redirect("/Administration/Categories/All");
        }

        [Authorize]
        public async Task<IActionResult> All()
        {
            var viewModel = await this.categoriesService.GetAllAsync<CategoryInfoViewModel>();
            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult Edit(string id)
        {
            return this.View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(CategoryInfoViewModel input)
        {
            await this.categoriesService.EditAsync(input);
            return this.Redirect("/Administration/Categories/All");
        }

        [Authorize]
        public IActionResult Delete(string id)
        {
            var inputModel = new CategoryInfoViewModel()
            {
                Id = id,
                Name = this.categoriesService.GetNameById(id),
            };
            return this.View(inputModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Delete(CategoryInfoViewModel input)
        {
            await this.categoriesService.DeleteByIdAsync(input.Id);
            return this.Redirect("/Administration/Categories/All");
        }
    }
}
