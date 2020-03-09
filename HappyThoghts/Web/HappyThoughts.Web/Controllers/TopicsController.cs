namespace HappyThoughts.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using HappyThoughts.Common;
    using HappyThoughts.Services;
    using HappyThoughts.Services.Data.Categories;
    using HappyThoughts.Services.Data.Comments;
    using HappyThoughts.Services.Data.Topics;
    using HappyThoughts.Web.ViewModels.Categories;
    using HappyThoughts.Web.ViewModels.Comments;
    using HappyThoughts.Web.ViewModels.InputModels;
    using HappyThoughts.Web.ViewModels.Topics;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class TopicsController : BaseController
    {
        private readonly ITopicsService topicsService;
        private readonly ICategoriesService categoriesService;
        private readonly ICloudinaryService cloudinaryService;
        private readonly ICommentsService commentsService;

        public TopicsController(
            ITopicsService topicsService,
            ICategoriesService categoriesService,
            ICloudinaryService cloudinaryService,
            ICommentsService commentsService)
        {
            this.topicsService = topicsService;
            this.categoriesService = categoriesService;
            this.cloudinaryService = cloudinaryService;
            this.commentsService = commentsService;
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
                input.Categories = await this.categoriesService.GetAllAsync<CategoryInfoViewModel>();
                return this.View(input);
            }

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            input.AuthorId = userId;
            input.CategoryId = this.categoriesService.GetIdByName(input.CategoryName);

            if (input.Picture != null)
            {
                var pictureUrl = await this.cloudinaryService.UploadPhotoAsync(
                input.Picture,
                $"{userId}-{input.Title}");

                input.PictureUrl = pictureUrl;
            }

            await this.topicsService.CreateAsync(input);

            var newTopicId = this.topicsService.GetIdByTitle(input.Title);

            return this.Redirect($"/Topics/Details?topicId={newTopicId}");
        }

        public async Task<IActionResult> Details(string topicId)
        {
            await this.topicsService.IncreaseViews(topicId);
            var viewModel = await this.topicsService
                .GetByIdAsViewModelAsync(topicId);

            viewModel.Comments = this.commentsService.GetAllAsQueryable<CommentInfoViewModel>()
                .Where(c => c.TopicId == topicId)
                .OrderByDescending(c => c.CreatedOn)
                .ToList();

            viewModel.Categories = await this.categoriesService
                .GetAllAsync<CategoryInfoViewModel>();

            return this.View(viewModel);
        }

        public async Task<IActionResult> Search(string searchTerm, int page = GlobalConstants.DefaultPageNumber)
        {
            if (string.IsNullOrEmpty(searchTerm) || string.IsNullOrWhiteSpace(searchTerm))
            {
                return this.Redirect("/");
            }

            var serviceModel = await this.topicsService.GetAllTopicsBySearchAsync(searchTerm);

            var viewModel = new TopicSearchViewModel()
            {
                SearchTerm = searchTerm,
                Topics = new TopicsListingViewModel()
                {
                    TotalTopicsCount = serviceModel.TotalTopicsCount,
                    CurrentPage = page,
                    Topics = serviceModel.Topics,
                    Categories = await this.categoriesService.GetAllAsync<CategoryInfoViewModel>(),
                },
            };

            return this.View(viewModel);
        }

        public async Task<IActionResult> Edit(string topicId, string categoryName)
        {
            var model = await this.topicsService.GetByIdAsViewModelAsync(topicId);
            var viewModel = new TopicEditViewModel()
            {
                Id = topicId,
                CategoryName = categoryName,
                Title = model.Title,
                Content = model.Content,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TopicEditViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Redirect($"/Topics/Edit?topicId={input.Id}&categoryName={input.CategoryName}");
            }

            await this.topicsService.EditAsync(input);

            return this.Redirect($"/Topics/Details?topicId={input.Id}");
        }

        public async Task<IActionResult> Delete(string id)
        {
            await this.topicsService.DeleteByIdAsync(id);
            return this.Redirect("/Home/Index");
        }
    }
}
