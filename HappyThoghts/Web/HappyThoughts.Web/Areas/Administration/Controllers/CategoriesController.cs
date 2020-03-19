namespace HappyThoughts.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using HappyThoughts.Common;
    using HappyThoughts.Services;
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
        private readonly ICloudinaryService cloudinaryService;

        public CategoriesController(ICategoriesService categoriesService, ICloudinaryService cloudinaryService)
        {
            this.categoriesService = categoriesService;
            this.cloudinaryService = cloudinaryService;
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
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            if (input.Picture != null)
            {
                var pictureUrl = await this.cloudinaryService.UploadPhotoAsync(
                input.Picture,
                $"{input.Name}-{Guid.NewGuid().ToString()}");

                input.PictureUrl = pictureUrl;
            }

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
        public async Task<IActionResult> Edit(string id)
        {
            var categoryViewModel = await this.categoriesService.GetCategoryById(id);
            return this.View(categoryViewModel);
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
